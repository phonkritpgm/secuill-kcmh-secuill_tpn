Module md

    '' path file setting
    Public ConnMiddle As String = cls_configuration.Read(My.Settings.ConfigPath.ToString(), cls_configuration.Items.ConMidle)
    Public ConnSecuill As String = cls_configuration.Read(My.Settings.ConfigPath.ToString(), cls_configuration.Items.ConSecuill)
    Public Tomachineno As String = cls_configuration.Read(My.Settings.ConfigPath.ToString(), cls_configuration.Items.Tomachine)
    Public ConnServer As String = cls_configuration.Read(My.Settings.ConfigPath.ToString(), cls_configuration.Items.ConServer)

    Public PathLog As String = cls_configuration.Read(My.Settings.ConfigPath.ToString(), cls_configuration.Items.PathLog)
    Public WardCode As String = cls_configuration.Read(My.Settings.ConfigPath.ToString(), cls_configuration.Items.ward_code)
    Public location As String = cls_configuration.Read(My.Settings.ConfigPath.ToString(), cls_configuration.Items.location)

    Public programName As String = Application.ProductName
    Public version As String = Application.ProductVersion
    Public clientName = Net.Dns.GetHostName
    Public ipAddress = timeStamp.getIP()

    '' status connect database middle and secuill
    Public statusConnMiddle As Boolean = False
    Public statusConnSecuill As Boolean = False

    '' setting font master
    Public mFont As String = "TH SarabunPSK"
    Public mFontSize As Integer = 15
    Public mFontHeaderSize As Integer = 20
    Public mFontStatusSize As Integer = 15

    '' color header
    Public mColorHeader As Color = Color.LightBlue
    Public mColorBorder As Color = Color.LavenderBlush

    '' color status text & Connection server
    Public mColorstatusConn As Color = Color.MediumSpringGreen
    Public mColorstatusError As Color = Color.Orange
    Public mColorstatusWarning As Color = Color.NavajoWhite

    '' Queue text event
    Public Q_StatusTxt As New Queue(Of String())

    '' Path file image
    Public imgStart As String = Application.StartupPath & "\TPN_icon\Play.png"
    Public imgStop As String = Application.StartupPath & "\TPN_icon\Pause.png"
    Public imgPowerOff As String = Application.StartupPath & "\TPN_icon\Power_Off.png"

    Public imgCircleRed As String = Application.StartupPath & "\TPN_icon\s_red.png"
    Public imgCircleOrange As String = Application.StartupPath & "\TPN_icon\s_orange.png"
    Public imgCircleGreen As String = Application.StartupPath & "\TPN_icon\s_green.png"

    Public imgSecuill As String = Application.StartupPath & "\TPN_icon\Secuill.png"
    Public imgIconSecuill As String = Application.StartupPath & "\TPN_icon\icon.ico"

    Public jsonConnection As JSONSqlConnection() = cls_configuration.DeserializeObject($"{Application.StartupPath}\TPN_config\config_fetch_user.json")

    '' Stat process status event
    Public Structure EventText
        Public Shared Sub SetText(ByVal mState As TextState, ByVal mEvent As String, ByVal mColor As TextColor, ByVal mWriteLog As WriteLog)
            Dim mStateStr As String = ""
            Select Case mState
                Case TextState.Init : mStateStr = "Init"
                Case TextState.Process : mStateStr = "Process"
                Case TextState.Status : mStateStr = "Status"
                Case TextState._Error : mStateStr = "_Error"
            End Select

            Dim method As System.Reflection.MethodBase
            method = New System.Diagnostics.StackTrace().GetFrame(1).GetMethod()
            Dim StrMethod As String = method.ReflectedType.Name & "." & method.Name

            md.Q_StatusTxt.Enqueue({mStateStr, mEvent, mColor, mWriteLog, StrMethod})
        End Sub

        Public Enum TextState
            Init
            Process
            Status
            _Error
            Insert
            Update
        End Enum

        Public Enum TextColor
            Green
            Orange
            Red
        End Enum

        Public Enum WriteLog
            Yes
            No
        End Enum

    End Structure

    Public Function Fill(connectinSting As String, ByVal query As String, Optional err_class As String = "") As DataSet
        Dim ds As New DataSet

        Using conndb As New System.Data.SqlClient.SqlConnection(connectinSting)
            Try
                ' Open connection
                If conndb.State = ConnectionState.Closed Then
                    conndb.Open()
                End If

                Using da As New System.Data.SqlClient.SqlDataAdapter(query, conndb)
                    da.Fill(ds)
                    Return ds
                End Using

            Catch ex As System.Data.SqlClient.SqlException
                'md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)

                Dim str As String = ""

                Select Case ex.Number
                    Case 53
                        str = String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.InnerException.Message + $" '{conndb.DataSource}'", vbTab, "md.Fill()")
                    Case Else
                        str = String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.Replace(vbCrLf, " "), vbTab, "md.Fill()")
                End Select
                cls_logfile.Write(str)
                Return Nothing

            Finally
                conndb.Close()
                conndb.Dispose()
            End Try
        End Using

    End Function

    Public Sub ExecuteNonQuery(connectionString As String, query As String, Optional err_class As String = "")
        Using conndb As New System.Data.SqlClient.SqlConnection(connectionString)
            ' Open connection
            If conndb.State = ConnectionState.Closed Then
                conndb.Open()
            End If

            Dim trans As System.Data.SqlClient.SqlTransaction = conndb.BeginTransaction
            Dim cmd As SqlClient.SqlCommand
            cmd = conndb.CreateCommand()
            cmd.CommandText = query
            cmd.Transaction = trans

            Try
                cmd.ExecuteNonQuery()
                trans.Commit()
            Catch ex As System.Data.SqlClient.SqlException
                cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, "md.ExecuteNonQuery()" & err_class))
                trans.Rollback()
            Finally
                conndb.Close()
                conndb.Dispose()
            End Try
        End Using

    End Sub

    Public Function createConnectionString(dataSource, databaseName, userId, password) As String
        Return $"Data Source={dataSource};
                Initial Catalog={databaseName};
                User Id={userId};
                Password={password};
                Asynchronous Processing=True;"
    End Function

End Module

Public Class JSONSqlConnection
    Public dataSource As String
    Public databaseName As String
    Public user As String
    Public password As String
End Class