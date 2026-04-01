Imports System.IO

Public Class cls_configuration

    Public Shared Function Read(ByVal FilePath As String, ByVal identify As String) As String
        Dim result As String = String.Empty
        Try
            If Not File.Exists(FilePath) Then Return String.Empty 'กันพังกรณีไม่มีไฟล์
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
        End Try
        Return result
    End Function

    ' อ่านข้อมูล SECUILL ทั้งหมดที่ระบุในไฟล์ (1, 2, 3...)
    Public Shared Function ReadGroup(ByVal FilePath As String, ByVal prefix As String) As Dictionary(Of String, String)
        Dim results As New Dictionary(Of String, String)
        Try
            If Not File.Exists(FilePath) Then Return results
            Dim lines As String() = File.ReadAllLines(FilePath)
            For Each line As String In lines
                Dim trimmedLine = line.Trim()

                If trimmedLine.StartsWith(";") OrElse Not trimmedLine.Contains("=") Then Continue For

                If trimmedLine.ToLower.StartsWith(prefix.ToLower()) Then
                    Dim parts = trimmedLine.Split("="c)
                    Dim key = parts(0).Trim()

                    Dim value = String.Join("=", parts.Skip(1)).Trim()

                    If Not results.ContainsKey(key) Then
                        results.Add(key, value)
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
        Return results
    End Function

    Public Shared Function Write(ByVal FilePath As String, ByVal identify As String, ByVal txt As String) As String
        Dim result As String = String.Empty
        Try
            Dim sf As New System.IO.StreamReader(FilePath)
            Do While (sf.Peek <> -1)
                Dim line As String = sf.ReadLine
                If line.ToLower.StartsWith(identify.ToLower & "=") Then
                    sf.Close()
                    Dim rte = IO.File.ReadAllText(FilePath)
                    rte = Replace(rte, line, identify & "=" & txt)
                    IO.File.WriteAllText(FilePath, rte)
                    Exit Do
                End If
            Loop
            sf.Close()
        Catch ex As Exception
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
        ' secuill-1, secuill-2...
        Public Shared ConSecuillPrefix As String = "connserver-secuill-"
        'เพิ่มตัวนี้เพื่ออ่านค่าการจับคู่วอร์ด
        Public Shared WardMachinePrefix As String = "ward-machine-"
        Public Shared Tomachine As String = "Middle-Tomachineno"
        Public Shared ConServer As String = "connserver-server"
        Public Shared location As String = "location"
        Public Shared PathLog As String = "logpath"
        Public Shared ward_code As String = "ward-code"
    End Structure

End Class