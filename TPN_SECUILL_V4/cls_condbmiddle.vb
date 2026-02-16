Public Class cls_condbmiddle
    'Private Shared constrmiddle As String = cls_configuration.Condb_Middle.ToString()
    Private Shared condbmiddle As System.Data.SqlClient.SqlConnection
    'Private Shared cmd As System.Data.SqlClient.SqlCommand

    Public Shared Function chk_connect_db() As Boolean
        Dim resp As Boolean = False
        cls_condbmiddle.condbmiddle = New System.Data.SqlClient.SqlConnection(md.ConnMiddle)
        Try
            cls_condbmiddle.condbmiddle.Open()
            If cls_condbmiddle.condbmiddle.State = ConnectionState.Closed Then
                resp = False
            Else
                resp = True
            End If
        Catch ex As Exception

            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, "cls_condbmiddle.chk_connect_db()"))

            resp = False

        Finally

            cls_condbmiddle.condbmiddle.Close()

            cls_condbmiddle.condbmiddle.Dispose()
        End Try

        Return resp
    End Function

    Public Shared Function Fill(ByVal sql_query As String, ByVal tname As String, ByVal param As Object, Optional err_class As String = "") As DataSet
        Dim ds As New DataSet

        Using conndb As New System.Data.SqlClient.SqlConnection(md.ConnMiddle)
            Try

                ' Open connection
                If conndb.State = ConnectionState.Closed Then
                    conndb.Open()
                End If
                'cmd = New SqlClient.SqlCommand(sql_query, conndb)
                Using da As New System.Data.SqlClient.SqlDataAdapter(sql_query, conndb)
                    'da.SelectCommand.CommandTimeout = 60
                    If param IsNot Nothing Then
                        da.SelectCommand.Parameters.Clear()
                        For i As Integer = 0 To param.Length - 1
                            da.SelectCommand.Parameters.AddWithValue("@" & i.ToString, param(i))
                        Next
                    End If

                    If ds.Tables.Contains(tname) Or Not ds.Tables(tname) Is Nothing Then ds.Tables(tname).Clear()

                    da.Fill(ds, tname)

                    Return ds

                End Using

            Catch ex As Exception

                cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, "cls_condbmiddle.Fill()" & err_class))

                Return Nothing

            Finally

                conndb.Close()

                conndb.Dispose()

            End Try
        End Using

    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmd As System.Data.SqlClient.SqlCommand, Optional err_class As String = "") As Boolean

        Dim ret As Boolean

        Using conndb As New System.Data.SqlClient.SqlConnection(md.ConnMiddle)
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

                cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, "cls_condbmiddle.ExecuteNonQuery()" & err_class))
                trans.Rollback()

                ret = False

            Finally

                conndb.Close()

                conndb.Dispose()

            End Try

        End Using

        Return ret

    End Function
End Class
