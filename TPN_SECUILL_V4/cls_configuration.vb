Public Class cls_configuration

    Public Shared Function Read(ByVal FilePath As String, ByVal identify As String) As String
        Dim result As String = String.Empty

        Try
            Dim sf As New System.IO.StreamReader(FilePath)

            Do While (sf.Peek <> -1)
                Dim line As String = sf.ReadLine
                If line.ToLower.StartsWith(identify.ToLower & "=") Then
                    result = line.Substring(identify.Length + 1).Trim()
                    Exit Do
                ElseIf line.ToLower.StartsWith(identify.ToLower & " =") Then
                    result = line.Substring(identify.Length + 2).Trim()
                    Exit Do
                End If
            Loop

            sf.Close()
        Catch ex As Exception
            'MessageBox.Show(ex.ToString())
        End Try

        Return result
    End Function

    ''Application.StartupPath & "\setting\Setting.txt"
    Dim codelines(0 To 1) As String
    Public Shared Function Write(ByVal FilePath As String, ByVal identify As String, ByVal txt As String) As String
        Dim result As String = String.Empty

        Try
            Dim sf As New System.IO.StreamReader(FilePath)

            Dim countLine As Integer = 0
            Do While (sf.Peek <> -1)
                Dim line As String = sf.ReadLine
                If line.ToLower.StartsWith(identify.ToLower & "=") Then

                    sf.Close()

                    Dim rte = IO.File.ReadAllText(FilePath)

                    rte = Replace(rte, line, identify & "=" & txt)

                    IO.File.WriteAllText(FilePath, rte)

                    Exit Do
                End If

                countLine = countLine + 1
            Loop

            sf.Close()
        Catch ex As Exception
            'MessageBox.Show(ex.ToString())
        End Try

        Return result
    End Function

    Public Shared Function DeserializeObject(filePath As String) As JSONSqlConnection()
        Dim jsonString As String = IO.File.ReadAllText(filePath)
        Return Newtonsoft.Json.JsonConvert.DeserializeObject(Of JSONSqlConnection())(jsonString)
    End Function

    Public Structure Items
        Public Shared ConMidle As String = "connserver-middle"
        Public Shared ConSecuill As String = "connserver-secuill"
        Public Shared Tomachine As String = "Middle-Tomachineno"
        Public Shared ConServer As String = "connserver-server"
        Public Shared location As String = "location"

        Public Shared PathLog As String = "logpath"

        Public Shared ward_code As String = "ward-code"
    End Structure

End Class
