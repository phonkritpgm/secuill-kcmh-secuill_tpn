Public Class cls_condbsecuill
    Private Shared condbsecuill As System.Data.SqlClient.SqlConnection

    Public Shared Property ConnectionString As String = ""

    Private Shared Function GetCurrentConnectionString() As String
        If Not String.IsNullOrEmpty(ConnectionString) Then
            Return ConnectionString
        Else
            Return md.ConnSecuill
        End If
    End Function

    Public Shared Function chk_connect_db_custom(ByVal connStr As String) As Boolean
        Try
            Using conndb As New System.Data.SqlClient.SqlConnection(connStr)
                conndb.Open()
                Return True
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Function chk_connect_db() As Boolean
        Dim resp As Boolean = False

        Using conndb As New System.Data.SqlClient.SqlConnection(GetCurrentConnectionString())
            Try
                conndb.Open()
                If conndb.State = ConnectionState.Closed Then
                    resp = False
                Else
                    resp = True
                End If
            Catch ex As Exception
                md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
                resp = False
            Finally
                conndb.Close()
                conndb.Dispose()
            End Try
        End Using
        Return resp
    End Function

    Public Shared Function Fill(ByVal sql_query As String, ByVal tname As String, ByVal param As Object, Optional err_class As String = "") As DataSet
        Dim ds As New DataSet

        Using conndb As New System.Data.SqlClient.SqlConnection(GetCurrentConnectionString())
            Try
                ' Open connection
                If conndb.State = ConnectionState.Closed Then
                    conndb.Open()
                End If

                Using da As New System.Data.SqlClient.SqlDataAdapter(sql_query, conndb)
                    If ds.Tables.Contains(tname) Then
                        If Not ds.Tables(tname) Is Nothing Then ds.Tables(tname).Clear()
                    End If
                    da.Fill(ds, tname)
                    Return ds
                End Using
            Catch ex As Exception
                md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
                Return Nothing
            Finally
                conndb.Close()
                conndb.Dispose()
            End Try
        End Using
    End Function

    Public Shared Function ExecuteNonQuery(ByVal SQL As String, Optional err_class As String = "") As Boolean
        Dim result As Boolean = False
        Using cmd As New SqlClient.SqlCommand(SQL)
            result = ExecuteNonQuery(cmd, err_class)
        End Using
        Return result
    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmd As System.Data.SqlClient.SqlCommand, Optional err_class As String = "") As Boolean
        Dim ret As Boolean

        Using conndb As New System.Data.SqlClient.SqlConnection(GetCurrentConnectionString())
            Try
                ' Open connection
                If conndb.State = ConnectionState.Closed Then
                    conndb.Open()
                End If

                Dim trans As System.Data.SqlClient.SqlTransaction = conndb.BeginTransaction
                cmd.Connection = conndb
                cmd.Transaction = trans

                Try
                    cmd.ExecuteNonQuery()
                    trans.Commit()
                    ret = True
                Catch ex As System.Data.SqlClient.SqlException
                    md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
                    trans.Rollback()
                    ret = False
                End Try
            Catch ex As Exception
                md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
                ret = False
            Finally
                conndb.Close()
                conndb.Dispose()
            End Try
        End Using
        Return ret
    End Function
End Class