Imports System.Net.NetworkInformation
Imports System.Threading

Module timeStamp

    Public Function getIP() As String

        Dim networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()

        For Each networkInterface In networkInterfaces
            If networkInterface.OperationalStatus = OperationalStatus.Up Then
                For Each information In networkInterface.GetIPProperties().UnicastAddresses
                    If information.Address.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then

                        Return information.Address.ToString()
                    End If
                Next information
            End If
        Next networkInterface

        Return ""
    End Function

    Private Sub threadExecuteNonQuery(SQL As String)
        Dim thread As New Thread(New ThreadStart(Function() cls_condbserver.ExecuteNonQuery(SQL)))
        thread.IsBackground = True
        thread.Start()
    End Sub

    Public Sub startProgramTimeStamp()
        Dim SQL As String = String.Empty
        SQL =
            $"IF EXISTS (SELECT programName FROM T_ProgramTimestamp
			    WHERE programName = '{md.programName}' 
				    AND ip = '{md.ipAddress}'
				    AND clientName = '{md.clientName}')
            BEGIN
                UPDATE T_ProgramTimestamp
                SET version = '{md.version}'
                    , location = N'{md.location}'
                    , timestamp = GETDATE()
                    , programStartDt = GETDATE()
                WHERE programName = '{md.programName}' 
				    AND ip = '{md.ipAddress}'
				    AND clientName = '{md.clientName}' 
                END
            ELSE
                INSERT INTO T_ProgramTimestamp(programName
                    , location
                    , version
                    , ip
                    , clientName
                    , timestamp
                    , programStartDt)
                VALUES('{md.programName}'
                    , N'{md.location}'
                    , '{md.version}'
                    , '{md.ipAddress}'
                    , '{md.clientName}'
                    , GETDATE()
                    , GETDATE())"

        Try
            threadExecuteNonQuery(SQL)
        Catch ex As Exception
        End Try

    End Sub

    Public Sub updateTimeStamp()
        Dim SQL As String = String.Empty
        SQL =
            $"UPDATE T_ProgramTimestamp
            SET version = '{md.version}'
                , location = N'{md.location}'
                , timestamp = GETDATE()
            WHERE programName = '{md.programName}'
                AND ip = '{md.ipAddress}'
                AND clientName = '{md.clientName}'"

        threadExecuteNonQuery(SQL)
    End Sub

    Public Sub endProgramTimeStamp()
        Dim SQL As String = String.Empty
        SQL =
            $"UPDATE T_ProgramTimestamp
            SET programEndDT = GETDATE()
            WHERE programName = '{md.programName}'
                AND ip = '{md.ipAddress}'
                AND clientName = '{md.clientName}'"

        threadExecuteNonQuery(SQL)
    End Sub

End Module
