Public Class cls_condbserver
    Private Shared errExecute As Boolean = False

    Public Shared Function chk_connect_db() As Boolean

        Dim resp As Boolean = False

        Using conndb As New System.Data.SqlClient.SqlConnection(md.ConnServer)
            Try
                conndb.Open()
                If conndb.State = ConnectionState.Closed Then

                    resp = False
                Else
                    resp = True
                End If

            Catch ex As Exception

                'md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
                'MessageBox.Show("เห้ยเชื่อต่อเบสไม่ได้รอสักครู่นะ")
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

        Using conndb As New System.Data.SqlClient.SqlConnection(md.ConnServer)
            Try
                'cls_condbsecuill.condbsecuill = New System.Data.SqlClient.SqlConnection(cls_condbsecuill.constrsecuill)

                ' Open connection
                If conndb.State = ConnectionState.Closed Then
                    conndb.Open()
                End If
                'cmd = New SqlClient.SqlCommand(sql_query, conndb)
                Using da As New System.Data.SqlClient.SqlDataAdapter(sql_query, conndb)

                    If ds.Tables.Contains(tname) Or Not ds.Tables(tname) Is Nothing Then ds.Tables(tname).Clear()

                    da.Fill(ds, tname)

                    Return ds

                End Using

            Catch ex As Exception

                'md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)

                Return Nothing

            Finally

                conndb.Close()

                conndb.Dispose()

            End Try
        End Using

    End Function

    Public Shared Function ExecuteNonQuery(ByVal SQL As String)
        If errExecute Then
            Return False
        End If

        Using cmd As New SqlClient.SqlCommand(SQL)
            Try
                Return ExecuteNonQuery(cmd)
            Catch ex As Exception
                cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_condbserver.ExecuteNonQuery())"))
                Return False
            End Try
        End Using

    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmd As System.Data.SqlClient.SqlCommand, Optional err_class As String = "") As Boolean

        Dim ret As Boolean

        Using conndb As New System.Data.SqlClient.SqlConnection(md.ConnServer)
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

                'md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
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
