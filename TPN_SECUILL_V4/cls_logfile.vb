Public Class cls_logfile
    Private Shared _dateFormat As String = "yyyy-MM-dd"
    Private Shared _dateTimeFormat As String = "yyyy-MM-dd HH:mm:ss:fff"

    Public Shared Property DateFormat() As String
        Get
            Return _dateFormat
        End Get
        Set(value As String)
            _dateFormat = value
        End Set
    End Property

    Public Shared Property DateTimeFormat() As String
        Get
            Return _dateTimeFormat
        End Get
        Set(value As String)
            _dateTimeFormat = value
        End Set
    End Property

    Private Shared tmp_msg As String = String.Empty
    Private Shared counter As Integer = 0

    Public Shared Sub Write(ByRef mess As String, Optional filename As String = "")
        Try

            If mess = tmp_msg Then
                counter = counter + 1
            Else
                tmp_msg = mess
            End If

            If counter > 1000 Then

                Application.Restart()

                counter = 0
            End If

            Dim _now As DateTime = DateTime.Now
            Dim _date As String = cls_logfile.GetDate(_now)
            Dim _dateTime As String = cls_logfile.GetDateTime(_now)
            Dim _logPath As String = cls_logfile.GetLogPath(md.PathLog, filename & _date)

            Try
                cls_logfile.AppendMessageToFile(mess, _dateTime, _logPath)
            Catch ex As System.IO.IOException
                Throw New Exception(ex.Message.ToString())
            End Try

        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub AppendMessageToFile(ByVal mess As String, ByVal dtf As String, ByVal path As String)
        Try
            '' ถ้าไม่เจอ ที่อยู่ไฟล์ จะทำการสร้างโฟล์เดอใหม่ใน 
            If (Not System.IO.Directory.Exists(md.PathLog)) Then _
                System.IO.Directory.CreateDirectory(md.PathLog)

            If (Not System.IO.File.Exists(path)) Then

                'Dim _text As StreamWriter = File.CreateText(path)
                Using _text As System.IO.StreamWriter = New System.IO.StreamWriter(path, False, System.Text.UTF8Encoding.UTF8)
                    _text.WriteLine(String.Format("{0}" & vbTab & "{1}", dtf, mess))
                End Using
            Else
                'Dim _streamWriter As StreamWriter = File.AppendText(path)
                Using _streamWriter As System.IO.StreamWriter = New System.IO.StreamWriter(path, True, System.Text.UTF8Encoding.UTF8)
                    _streamWriter.WriteLine(String.Format("{0}" & vbTab & "{1}", dtf, mess))
                End Using
            End If

        Catch

        End Try
    End Sub

    Private Shared Function GetLogPath(ByVal path As String, ByVal df As String) As String
        ''return string.Format("{0}\{1}\{2}.txt", (object)Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), (object)WriteLog.AppName, (object)df);
        Return String.Format("{0}\{1}\{2}.log", Application.StartupPath, md.PathLog, df)
    End Function

    Private Shared Function GetDate(ByVal _datetime As DateTime)
        Return _datetime.ToString(cls_logfile.DateFormat)
    End Function

    Private Shared Function GetDateTime(ByVal _datetime As DateTime) As String
        Return _datetime.ToString(cls_logfile.DateTimeFormat)
    End Function

    Private Shared Sub ClearLogFile(ByVal _datetoclear As Date)



    End Sub

End Class
