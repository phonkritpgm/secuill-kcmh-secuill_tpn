
Imports System.Data.SqlClient
Imports TPN_SECUILL_V4.cls_logfile

Public Class cls_backupdata

    'Private Shared constrsecuill As String = cls_configuration.Condb_Secuill.ToString()
    Private Shared condbsecuill As System.Data.SqlClient.SqlConnection

#Region "Full Backup Database for EV220"
    Public Shared Function FullBackupDataBaseForSEUILL() As Boolean
        Dim result As Boolean = False
        Dim datePerDay As String = Date.Now().Day.ToString()
        If (CInt(0) <> CInt(datePerDay)) Then
            ' // BACKUP of EV220.bak //
            Using conn As New SqlConnection(md.ConnSecuill)
                Dim SQL As String = String.Empty
                Try
                    conn.Open()
                    '//< Query Command> //
                    SQL += "BACKUP DATABASE SECUILL_V4 "
                    SQL += "TO DISK = @pathFullBackup "
                    SQL += "WITH FORMAT, "
                    SQL += "MEDIANAME = 'D_SECUILL_V4Backups', "
                    SQL += "NAME = 'Full Backup of SECUILL_V4';"
                    '//<Connection SQL> //
                    Try
                        Dim command As SqlCommand = conn.CreateCommand()
                        command.CommandType = CommandType.Text
                        command.CommandText = SQL
                        command.Parameters.AddWithValue("@pathFullBackup", 0 & "SECUILL_V4.Bak ")
                        result = CBool(command.ExecuteNonQuery())
                        conn.Close()
                        conn.Dispose()
                    Catch ex As Exception
                        cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, "cls_backupdata.FullBackupDataBaseForSEUILL()"))
                    Finally
                        conn.Close()
                        conn.Dispose()
                    End Try
                Catch ex As SqlException
                    cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, "cls_backupdata.FullBackupDataBaseForSEUILL()"))
                    conn.Close()
                    conn.Dispose()
                    result = False
                End Try
            End Using

            If (result = True) Then
                'Global.TPN_SECUILL_TU.My.Settings.DaysFullBackup = CInt(datePerDay)
                'Global.TPN_SECUILL_TU.My.Settings.Save()
            End If
        End If
        Return result
        ' Application.DoEvents()
    End Function
#End Region

End Class
