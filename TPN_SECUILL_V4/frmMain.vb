Imports System.Reflection
Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Threading

Public Class frmMain

    ' --- Background Workers ---
    Private bgwGetMiddle As New System.ComponentModel.BackgroundWorker
    Private bgwRunStatusText As New System.ComponentModel.BackgroundWorker
    Private bgwRunEventText As New System.ComponentModel.BackgroundWorker

    ' --- Queues & UI Objects ---
    Private Q_LbStatusTxt As New Queue(Of Label)
    Private p As Point
    Private IsMouseDown As Boolean
    Private autoConnect As Boolean = True
    Private StatusConnect As Boolean = False
    Private TPN_SECUILL_V4_NotifyIcon As New System.Windows.Forms.NotifyIcon
    Private frmWidthEventSize As Integer = 468
    Dim timedate As DateTime = DateAndTime.Today

    ' ตัวแปรสำหรับตรวจสอบการเปลี่ยนสถานะจำนวนเครื่อง
    Private lastConnectedCount As Integer = -1
    Private strPoint As String = " ."

#Region "Database Field Name"
    Private Const userUpdateDate As String = "user_updatedate"
    Private Const userUpdateTime As String = "user_updatetime"
    Private Const userLoginUpdateDate As String = "ulogin_updatedate"
    Private Const userLoginUpdateTime As String = "ulogin_updatetime"
#End Region

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        timeStamp.startProgramTimeStamp()

        Me.Width = 510 : Me.Height = 280
        p = Me.Location

        Me.pbStatusConMiddle.Image = Image.FromFile(md.imgCircleOrange)
        Me.pbStatusConSecuill.Image = Image.FromFile(md.imgCircleOrange)

        Me.lbStatusConMiddleTxt.ForeColor = md.mColorstatusWarning
        Me.lbStatusConSecuillTxt.ForeColor = md.mColorstatusWarning

        Me.lbStatusConMiddleTxt.Text = "กำลังเชื่อมต่อ Middle"
        ' แสดงจำนวนเครื่องทั้งหมดจากไฟล์ Config
        Me.lbStatusConSecuillTxt.Text = "กำลังเชื่อมต่อ Secuill (" & md.ConnSecuillList.Count & ")"

        Me.lbStatusTxt1.Text = ""
        Me.lbStatusTxt2.Text = ""

        Me.Lbtime1.Text = "-"
        Me.Lbtime2.Text = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")

        md.EventText.SetText(EventText.TextState.Init, "#########################################", EventText.TextColor.Green, EventText.WriteLog.Yes)
        md.EventText.SetText(EventText.TextState.Init, "เริ่มต้นการทำงานโปรแกรม (Broadcasting Mode)", EventText.TextColor.Green, EventText.WriteLog.Yes)

        '' Setting Window Notify
        Me.TPN_SECUILL_V4_NotifyIcon.Icon = New Icon(md.imgIconSecuill, New Size(16, 16))
        Me.TPN_SECUILL_V4_NotifyIcon.BalloonTipIcon = ToolTipIcon.None
        Me.TPN_SECUILL_V4_NotifyIcon.Text = "TPN SECUILL V4"
        Me.TPN_SECUILL_V4_NotifyIcon.Visible = True

        Me.TPN_SECUILL_V4_NotifyIcon.ContextMenuStrip = New ContextMenuStrip
        AddHandler Me.TPN_SECUILL_V4_NotifyIcon.Click, AddressOf TPN_SECUILL_V4_NotifyIcon_Click

        '' Start text status queue
        Me.Q_LbStatusTxt.Enqueue(Me.lbStatusTxt1)
        Me.Q_LbStatusTxt.Enqueue(Me.lbStatusTxt2)

        '' Start BackgroundWorkers
        AddHandler Me.bgwRunStatusText.DoWork, AddressOf bgwRunStatusText_DoWork
        Me.bgwRunStatusText.RunWorkerAsync()

        AddHandler Me.bgwGetMiddle.DoWork, AddressOf BgwGetMiddle_DoWork

        If Me.autoConnect = True Then
            md.EventText.SetText(EventText.TextState.Init, "Auto connection . ", EventText.TextColor.Green, EventText.WriteLog.Yes)
            Me.btnStartConnect.Image = Image.FromFile(md.imgStop)
            Me.btnStartConnect.ImageAlign = ContentAlignment.MiddleCenter
            Me.bgwGetMiddle.RunWorkerAsync()
        End If
    End Sub

    'เชื่อมต่อ (Multi-DB Support)
    Private Sub CheckConnectMiddleAndSecuill()
        Try
            ' 1. Check Middle DB
            Dim chkconnmiddle As Boolean = cls_condbmiddle.chk_connect_db()
            If chkconnmiddle Then
                If md.statusConnMiddle = False Then
                    Me.setPictureBoxImg(md.imgCircleGreen, Me.pbStatusConMiddle)
                    Me.setlbStatusTxt("เชื่อมต่อฐานข้อมูล Middle", Me.lbStatusConMiddleTxt, "G")
                    md.EventText.SetText(EventText.TextState.Status, "เชื่อมต่อ Middle สำเร็จ", EventText.TextColor.Green, EventText.WriteLog.Yes)
                End If
                md.statusConnMiddle = True
            Else
                md.statusConnMiddle = False
                Me.setPictureBoxImg(md.imgCircleRed, Me.pbStatusConMiddle)
                Me.setlbStatusTxt("ไม่สามารถเชื่อมต่อ Middle", Me.lbStatusConMiddleTxt, "R")
            End If

            ' 2. Check Multiple Secuill DBs
            Dim connectedCount As Integer = 0
            For Each db In md.ConnSecuillList
                If cls_condbsecuill.chk_connect_db_custom(db.Value) Then
                    connectedCount += 1
                End If
            Next

            If connectedCount > 0 Then
                ' แสดงผล: "เชื่อมต่อฐานข้อมูล Secuill (3) database"
                Dim statusTxt As String = "เชื่อมต่อฐานข้อมูล Secuill (" & connectedCount & ")"
                Me.setPictureBoxImg(md.imgCircleGreen, Me.pbStatusConSecuill)
                Me.setlbStatusTxt(statusTxt, Me.lbStatusConSecuillTxt, "G")

                If connectedCount <> lastConnectedCount Then
                    md.EventText.SetText(EventText.TextState.Status, "เชื่อมต่อ Secuill (" & connectedCount & ")", EventText.TextColor.Green, EventText.WriteLog.Yes)
                End If
                md.statusConnSecuill = True
            Else
                md.statusConnSecuill = False
                Me.setPictureBoxImg(md.imgCircleRed, Me.pbStatusConSecuill)
                Me.setlbStatusTxt("ไม่สามารถเชื่อมต่อ Secuill", Me.lbStatusConSecuillTxt, "R")
            End If
            lastConnectedCount = connectedCount

        Catch ex As Exception
            md.EventText.SetText(EventText.TextState._Error, ex.Message, EventText.TextColor.Red, EventText.WriteLog.Yes)
        End Try
    End Sub

    'ดึงและโอนข้อมูล
    Private Sub BgwGetMiddle_DoWork(sender As Object, e As EventArgs)
        Const fetchTime = 180
        Const _10_sec = 10000
        Dim countTime As UInt16 = fetchTime
        Dim stopWatch As New Stopwatch

        Me.setlbStatusTxt("เริ่มต้นการทำงานดึงข้อมูล . . .", Me.lbStatusProcess, "G")

        Do
            stopWatch.Restart()
            Me.CheckConnectMiddleAndSecuill()

            If md.statusConnMiddle And md.statusConnSecuill Then
                ' ดึงยอดงานรวมทุกเครื่อง
                Dim countMiddle As Integer = cls_getmiddletosecuill.CountDataMiddleAll()

                If countMiddle > 0 Then
                    Me.strPoint = " ."
                    Me.setlbStatusTxt("ข้อมูล Middle " & countMiddle & " Rows", Me.lbStatusProcess, "G")
                    md.EventText.SetText(EventText.TextState.Process, "พบงานรอส่ง " & countMiddle & " รายการ กำลังเริ่ม Broadcast...", EventText.TextColor.Green, EventText.WriteLog.Yes)

                    Thread.Sleep(500)
                    cls_getmiddletosecuill.RunProcess() ' โอนข้อมูลเข้าทุกลูก

                    Me.setlbStatusTxt(Date.Now.ToString("yyyy-MM-dd HH:mm:ss"), Me.Lbtime1, "O")
                Else
                    Me.strPoint &= " ."
                    If Me.strPoint.Length > 15 Then Me.strPoint = " ."
                    Me.setlbStatusTxt("กำลังรอข้อมูลจาก Middle (" & lastConnectedCount & " DB Online)" & Me.strPoint, Me.lbStatusProcess, "O")
                End If
            End If

            If countTime Mod 6 = 0 Then runSubByThread(AddressOf timeStamp.updateTimeStamp)
            If countTime >= fetchTime Then
                countTime = 0
                fetchUser()
            Else
                countTime += 1
            End If

            setTextLable($"{stopWatch.ElapsedMilliseconds} ms.", lbProcessTime)
            Thread.Sleep(_10_sec)
        Loop
    End Sub

    Private Sub bgwRunStatusText_DoWork(sender As Object, e As DoWorkEventArgs)
        Threading.Thread.Sleep(200)
        Me.MeHide(Me)
        Do
            Try
                If md.Q_StatusTxt.Count > 0 Then
                    Dim lb As Label = Me.Q_LbStatusTxt.Dequeue()
                    Dim lb2 As Label = Me.Q_LbStatusTxt.Dequeue()
                    Dim txt As String() = md.Q_StatusTxt.Dequeue()
                    Dim sw As String = "S1"
                    Dim paddingTop As Integer = 3

                    For i As Integer = 0 To (CInt(Math.Ceiling(lb.Height)))
                        Dim tmSpeedUp As Integer = (20 - (md.Q_StatusTxt.Count * 2))
                        If tmSpeedUp <= 0 Then tmSpeedUp = 1

                        Me.setlbStatusTxt("(" & tmSpeedUp & ") (" & md.Q_StatusTxt.Count & ") " & txt(0), Me.lbHeadStatusTxt, txt(2))

                        If Me.lbStatusTxt1.Location.Y = paddingTop Then sw = "S1" Else If Me.lbStatusTxt2.Location.Y = paddingTop Then sw = "S2"

                        If sw = "S1" Then
                            Me.setlbStatusLocation(New Point(3, ((-i) + paddingTop)), lb)
                            Me.setlbStatusTxt(txt(1), lb2, txt(2))
                            Thread.Sleep(tmSpeedUp)
                            Me.setlbStatusLocation(New Point(3, lb.Location.Y + lb.Height), lb2)
                        ElseIf sw = "S2" Then
                            Me.setlbStatusLocation(New Point(3, ((-i) + 4)), lb2)
                            Me.setlbStatusTxt(txt(1), lb, txt(2))
                            Thread.Sleep(tmSpeedUp)
                            Me.setlbStatusLocation(New Point(3, lb2.Location.Y + lb2.Height), lb)
                        End If
                        Thread.Sleep(tmSpeedUp)
                    Next
                    Me.Q_LbStatusTxt.Enqueue(lb)
                    Me.Q_LbStatusTxt.Enqueue(lb2)

                    If txt(3) = "Yes" Then cls_logfile.Write(txt(0) & vbTab & txt(1) & vbTab & txt(4))
                End If
            Catch ex As Exception
                md.EventText.SetText(EventText.TextState._Error, ex.Message, EventText.TextColor.Red, EventText.WriteLog.Yes)
            End Try
            Thread.Sleep(500)
        Loop
    End Sub

    Private Sub fetchUser()
        If Not cls_condbsecuill.chk_connect_db() Then Exit Sub
        Try
            For Each connection In md.jsonConnection
                Dim sourceConnStr = createConnectionString(connection.dataSource, connection.databaseName, connection.user, connection.password)
                Dim query = "SELECT *, (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'M_User') as column_count FROM M_User FULL JOIN M_UserLogin ON M_User.user_id = M_UserLogin.user_id WHERE M_User.user_location_id = '2'"
                Dim dsSource = Fill(sourceConnStr, query)
                If dsSource IsNot Nothing Then

                End If
            Next
        Catch ex As Exception
            cls_logfile.Write("ERROR" & vbTab & ex.Message & vbTab & "fetchUser")
        End Try
    End Sub

#Region "UI Control Helpers"
    Private Sub setlbStatusTxt(ByVal text As String, ByVal lbl As Label, ByVal mColorTxt As String)
        If lbl.InvokeRequired Then
            lbl.Invoke(New setlbStatusTxtInvoker(AddressOf setlbStatusTxt), text, lbl, mColorTxt)
        Else
            lbl.Text = text
            If mColorTxt = "G" Or mColorTxt = "Green" Then lbl.ForeColor = md.mColorstatusConn
            If mColorTxt = "R" Or mColorTxt = "Red" Then lbl.ForeColor = md.mColorstatusError
            If mColorTxt = "O" Or mColorTxt = "Orange" Then lbl.ForeColor = md.mColorstatusWarning
        End If
    End Sub
    Private Delegate Sub setlbStatusTxtInvoker(ByVal text As String, ByVal lbl As Label, ByVal mColorTxt As String)

    Private Sub setPictureBoxImg(ByVal mPath As String, ByVal pb As PictureBox)
        If pb.InvokeRequired Then
            pb.Invoke(New setPictureBoxInvoker(AddressOf setPictureBoxImg), mPath, pb)
        Else
            pb.Image = Image.FromFile(mPath) : pb.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub
    Private Delegate Sub setPictureBoxInvoker(ByVal mPath As String, ByVal pb As PictureBox)

    Private Sub setlbStatusLocation(ByVal p As Point, ByVal lbl As Label)
        If lbl.InvokeRequired Then : lbl.Invoke(New setlbStatusLocationInvoker(AddressOf setlbStatusLocation), p, lbl)
        Else : lbl.Location = p : End If
    End Sub
    Private Delegate Sub setlbStatusLocationInvoker(ByVal p As Point, ByVal lbl As Label)

    Private Sub setTextLable(ByVal text As String, ByVal lb As Label)
        If lb.InvokeRequired Then : lb.Invoke(New setTextLable_Delegate(AddressOf setTextLable), text, lb)
        Else : lb.Text = text : End If
    End Sub
    Delegate Sub setTextLable_Delegate(ByVal text As String, ByVal lb As Label)
#End Region

#Region "Form Interaction & Events"
    Private Sub MyBase_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, pbSecuill.MouseDown, lbSecuill.MouseDown, lbHeaderTxt.MouseDown
        If e.Button = MouseButtons.Left Then : IsMouseDown = True : p = New Point(e.X, e.Y) : Me.Opacity = 0.5 : End If
    End Sub
    Private Sub MyBase_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove, pbSecuill.MouseMove, lbSecuill.MouseMove, lbHeaderTxt.MouseMove
        If IsMouseDown Then
            Dim curP As Point = Me.PointToScreen(New Point(e.X, e.Y))
            curP.Offset(-p.X, -p.Y) : Me.Location = curP
        End If
    End Sub
    Private Sub MyBase_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp, pbSecuill.MouseUp, lbSecuill.MouseUp, lbHeaderTxt.MouseUp
        IsMouseDown = False : Me.Opacity = 1
    End Sub
    Private Sub TPN_SECUILL_V4_NotifyIcon_Click(sender As Object, e As EventArgs)
        Me.Show() : Me.WindowState = FormWindowState.Normal
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Me.TPN_SECUILL_V4_NotifyIcon.ShowBalloonTip(100, "SECUILL Interface", "โปรแกรมกำลังทำงานอยู่เบื้องหลัง", ToolTipIcon.Info)
    End Sub
    Private Sub btn_exitprogram_Click(sender As Object, e As EventArgs) Handles btn_exitprogram.Click
        If MessageBox.Show("ต้องการปิดโปรแกรมใช่หรือไม่ ?", "ปิดโปรแกรม", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.TPN_SECUILL_V4_NotifyIcon.Dispose()
            Application.Exit()
        End If
    End Sub
    Private Sub MeHide(ByVal m As Form)
        If m.InvokeRequired Then : m.Invoke(New SetMeHide(AddressOf MeHide), m)
        Else : m.Hide() : End If
    End Sub
    Private Delegate Sub SetMeHide(ByVal m As Form)
    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        timeStamp.endProgramTimeStamp()
    End Sub

    Private Sub lbStatusConSecuillTxt_Click(sender As Object, e As EventArgs) Handles lbStatusConSecuillTxt.Click

    End Sub
#End Region

End Class