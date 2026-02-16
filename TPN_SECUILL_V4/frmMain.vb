Imports System.Reflection
Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Threading

Public Class frmMain

    Private bgwGetMiddle As New System.ComponentModel.BackgroundWorker

    Private bgwRunStatusText As New System.ComponentModel.BackgroundWorker

    Private bgwRunEventText As New System.ComponentModel.BackgroundWorker

    Private Q_LbStatusTxt As New Queue(Of Label)

    Private p As Point
    Private IsMouseDown As Boolean

    Private autoConnect As Boolean = True

    Private StatusConnect As Boolean = False

    Private TPN_SECUILL_V4_NotifyIcon As New System.Windows.Forms.NotifyIcon

    Private frmWidthEventSize As Integer = 468

    Dim timedate As DateTime = DateAndTime.Today

#Region "Database Field Name"
    Private Const userUpdateDate As String = "user_updatedate"
    Private Const userUpdateTime As String = "user_updatetime"
    Private Const userLoginUpdateDate As String = "ulogin_updatedate"
    Private Const userLoginUpdateTime As String = "ulogin_updatetime"
#End Region

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        timeStamp.startProgramTimeStamp()

        Me.Width = 500 : Me.Height = 280
        p = Me.Location

        Me.pbStatusConMiddle.Image = Image.FromFile(md.imgCircleOrange)
        Me.pbStatusConSecuill.Image = Image.FromFile(md.imgCircleOrange)

        Me.lbStatusConMiddleTxt.ForeColor = md.mColorstatusWarning
        Me.lbStatusConSecuillTxt.ForeColor = md.mColorstatusWarning

        Me.lbStatusConMiddleTxt.Text = "กำลังเชื่อมต่อ Middle"
        Me.lbStatusConSecuillTxt.Text = "กำลังเชื่อมต่อ Secuill"

        Me.lbStatusTxt1.Text = ""
        Me.lbStatusTxt2.Text = ""

        Me.Lbtime1.Text = "-"
        Me.Lbtime2.Text = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")

        'md.Q_StatusTxt.Enqueue({"Init", "เริ่มต้นการทำงานของโปรแกรม . . .", "C"})
        md.EventText.SetText(EventText.TextState.Init, "#########################################", EventText.TextColor.Green, EventText.WriteLog.Yes)
        md.EventText.SetText(EventText.TextState.Init, "เริ่มต้นการทำงานของโปรแกรม . . .", EventText.TextColor.Green, EventText.WriteLog.Yes)

        '' Setting Window Notjify
        Me.TPN_SECUILL_V4_NotifyIcon.Icon = New Icon(md.imgIconSecuill, New Size(16, 16))
        Me.TPN_SECUILL_V4_NotifyIcon.BalloonTipIcon = ToolTipIcon.None
        Me.TPN_SECUILL_V4_NotifyIcon.Text = "TPN SECUILL V4"
        Me.TPN_SECUILL_V4_NotifyIcon.Visible = True

        Me.TPN_SECUILL_V4_NotifyIcon.ContextMenuStrip = New ContextMenuStrip

        AddHandler Me.TPN_SECUILL_V4_NotifyIcon.Click, AddressOf TPN_SECUILL_V4_NotifyIcon_Click

        '' start text status 
        Me.Q_LbStatusTxt.Enqueue(Me.lbStatusTxt1)
        Me.Q_LbStatusTxt.Enqueue(Me.lbStatusTxt2)

        '' setting BackgroundWorker bgwRunStatusText
        AddHandler Me.bgwRunStatusText.DoWork, AddressOf bgwRunStatusText_DoWork
        Me.bgwRunStatusText.RunWorkerAsync()

        '' setting BackgroundWorker bgwGetMiddle
        AddHandler Me.bgwGetMiddle.DoWork, AddressOf BgwGetMiddle_DoWork

        If Me.autoConnect = True Then
            '' run process connect db auto
            md.EventText.SetText(EventText.TextState.Init, "Auto connection . ", EventText.TextColor.Green, EventText.WriteLog.Yes)

            Me.btnStartConnect.Image = Image.FromFile(md.imgStop)
            Me.btnStartConnect.ImageAlign = ContentAlignment.MiddleCenter

            md.EventText.SetText(EventText.TextState.Init, "เริ่มต้นการเชื่อมต่อกับ Server . . . ", EventText.TextColor.Green, EventText.WriteLog.Yes)

            Me.bgwGetMiddle.RunWorkerAsync()
        End If

    End Sub

    Private Sub btn_exitprogram_Click(sender As Object, e As EventArgs) Handles btn_exitprogram.Click

        If MessageBox.Show("ต้องการปิดโปรแกรมใช่หรือไม่ ?", "ปิดโปรแกรม", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            md.EventText.SetText(EventText.TextState.Status, "ปิดโปรแกรม !", EventText.TextColor.Green, EventText.WriteLog.Yes)

            Me.TPN_SECUILL_V4_NotifyIcon.Dispose()
            Application.ExitThread()
            Application.Exit()

        End If

    End Sub

    Private Sub MyBase_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, pbSecuill.MouseDown, lbSecuill.MouseDown, lbHeaderTxt.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.IsMouseDown = True
            Me.p = New Point(e.X, e.Y)
            Me.Opacity = 0.5
        End If
    End Sub

    Private Sub MyBase_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove, pbSecuill.MouseMove, lbSecuill.MouseMove, lbHeaderTxt.MouseMove
        If Me.IsMouseDown = True Then
            Dim x = e.X
            Dim y = e.Y
            Dim currentPoint As New Point(x, y)
            Dim NewP As Point
            NewP = Me.PointToScreen(currentPoint)
            NewP.Offset(-Me.p.X, -Me.p.Y)
            Me.Location = NewP
        End If
    End Sub

    Private Sub MyBase_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp, pbSecuill.MouseUp, lbSecuill.MouseUp, lbHeaderTxt.MouseUp
        Me.IsMouseDown = False
        Me.Opacity = 1
    End Sub

    'Private Sub ExitsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitsToolStripMenuItem.Click
    '    TmLoaded.Enabled = False : TmLoaded.Stop()
    '    Application.ExitThread()
    '    Application.Exit()
    'End Sub

    Private Sub TPN_SECUILL_V4_NotifyIcon_Click(sender As Object, e As EventArgs)
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Me.TPN_SECUILL_V4_NotifyIcon.ShowBalloonTip(100, "โปรแกรมดึงทำงานอยู่", "ไม่ควรปิดโปรแกรมเพราะจะดึงข้อมูลมา SECUILL ไม่ได้เตือนเฉยๆ", ToolTipIcon.Info)
    End Sub

    Private Sub MeHide(ByVal m As Form)
        If m.InvokeRequired Then
            m.Invoke(New SetMeHide(AddressOf MeHide), m)
        Else
            m.Hide()
        End If
    End Sub
    Private Delegate Sub SetMeHide(ByVal m As Form)

    Private Sub bgwRunStatusText_DoWork(sender As Object, e As DoWorkEventArgs)
        Threading.Thread.Sleep(200)
        Me.MeHide(Me)
        Me.TPN_SECUILL_V4_NotifyIcon.ShowBalloonTip(100, "SECUILL Interface", "ไม่ควรปิดโปรแกรม", ToolTipIcon.Info)
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
                        If tmSpeedUp <= 0 Then
                            tmSpeedUp = 1
                        End If

                        Me.setlbStatusTxt("(" & tmSpeedUp & ") (" & md.Q_StatusTxt.Count & ") " & txt(0), Me.lbHeadStatusTxt, txt(2))

                        If Me.lbStatusTxt1.Location.Y = paddingTop Then
                            sw = "S1"
                        ElseIf Me.lbStatusTxt2.Location.Y = paddingTop Then
                            sw = "S2"
                        End If

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

                    If txt(3) = md.EventText.WriteLog.Yes Then
                        cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", txt(0), vbTab, txt(1), vbTab, txt(4)))
                    End If

                End If

            Catch ex As Exception
                Me.Q_LbStatusTxt.Clear()
                Me.Q_LbStatusTxt.Enqueue(Me.lbStatusTxt1)
                Me.Q_LbStatusTxt.Enqueue(Me.lbStatusTxt2)

                md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
            End Try

            Dim tmLoopSpeed As Integer = (500 - (md.Q_StatusTxt.Count * 30))
            If tmLoopSpeed <= 0 Then
                tmLoopSpeed = 10
            End If

            Me.setlbStatusTxt(CStr(tmLoopSpeed), LbPercent, EventText.TextColor.Green)

            Thread.Sleep(tmLoopSpeed)

        Loop

    End Sub

    Private Sub setTextLable(ByVal text As String, ByVal lb As Label)
        If lb.InvokeRequired Then
            lb.Invoke(New setTextLable_Delegate(AddressOf setTextLable), text, lb)
        Else
            lb.Text = text
        End If
    End Sub
    Delegate Sub setTextLable_Delegate(ByVal text As String, ByVal lb As Label)

    Private Sub setlbStatusTxt(ByVal text As String, ByVal lbl As Label, ByVal mColorTxt As String)
        If lbl.InvokeRequired Then
            lbl.Invoke(New setlbStatusTxtInvoker(AddressOf setlbStatusTxt), text, lbl, mColorTxt)
        Else
            lbl.Text = text
            Select Case mColorTxt
                Case md.EventText.TextColor.Green
                    lbl.ForeColor = md.mColorstatusConn
                Case md.EventText.TextColor.Red
                    lbl.ForeColor = md.mColorstatusError
                Case md.EventText.TextColor.Orange
                    lbl.ForeColor = md.mColorstatusWarning
                Case Else
                    lbl.ForeColor = md.mColorBorder
            End Select

        End If
    End Sub
    Private Delegate Sub setlbStatusTxtInvoker(ByVal text As String, ByVal lbl As Label, ByVal mColorTxt As String)

    Private Sub setlbStatusLocation(ByVal p As Point, ByVal lbl As Label)
        If lbl.InvokeRequired Then
            lbl.Invoke(New setlbStatusLocationInvoker(AddressOf setlbStatusLocation), p, lbl)
        Else
            lbl.Location = p
        End If
    End Sub
    Private Delegate Sub setlbStatusLocationInvoker(ByVal p As Point, ByVal lbl As Label)

    Private Sub setPictureBoxImg(ByVal mPath As String, ByVal pb As PictureBox)
        If pb.InvokeRequired Then
            pb.Invoke(New setPictureBoxInvoker(AddressOf setPictureBoxImg), mPath, pb)
        Else
            pb.Image = Image.FromFile(mPath)
            pb.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub
    Private Delegate Sub setPictureBoxInvoker(ByVal mPath As String, ByVal pb As PictureBox)

    Private Sub btnStartConnect_Click(sender As Object, e As EventArgs) Handles btnStartConnect.Click
        'Me.btnStartConnect.Image = Image.FromFile(md.imgStop)
        'Me.btnStartConnect.ImageAlign = ContentAlignment.MiddleCenter

        'md.Q_StatusTxt.Enqueue({"สถานะ", "เริ่มต้นการเชื่อมต่อกับ Server . . . ", "C"})

    End Sub


    '' check connect
    Private strPointConn As String = " ."
    Private satusRecheck As Integer = 0
    Private Sub CheckConnectMiddleAndSecuill()

        '' check connect secuill
        Try

            Dim chkconnmiddle As Boolean = cls_condbmiddle.chk_connect_db()
            If chkconnmiddle = True Then

                If statusConnMiddle = False Then
                    '' Set status
                    Me.setPictureBoxImg(md.imgCircleGreen, Me.pbStatusConMiddle)
                    Me.setlbStatusTxt("เชื่อมต่อฐานข้อมูล Middle", Me.lbStatusConMiddleTxt, EventText.TextColor.Green)

                    md.EventText.SetText(EventText.TextState.Status, "เชื่อมต่อ Middle สำเร็จ", EventText.TextColor.Green, EventText.WriteLog.Yes)
                End If

                md.statusConnMiddle = True

            Else

                md.statusConnMiddle = False

                '' Set status
                Me.setPictureBoxImg(md.imgCircleRed, Me.pbStatusConMiddle)
                Me.setlbStatusTxt("ไม่สามารถเชื่อมต่อ Middle", Me.lbStatusConMiddleTxt, EventText.TextColor.Red)

                md.EventText.SetText(EventText.TextState._Error, "ไม่สามารถเชื่อมต่อ Server Middle", EventText.TextColor.Red, EventText.WriteLog.Yes)

                Thread.Sleep(2000)

                '' Set status
                Me.setPictureBoxImg(md.imgCircleOrange, Me.pbStatusConMiddle)
                Me.setlbStatusTxt("กำลังเชื่อมต่อ Middle", Me.lbStatusConMiddleTxt, EventText.TextColor.Orange)

                md.EventText.SetText(EventText.TextState.Status, "กำลังเชื่อมต่อใหม่อีกครั้ง Server Middle", EventText.TextColor.Orange, EventText.WriteLog.Yes)

                Thread.Sleep(2000)

            End If

            '' check connect middle
            Try

                Dim chkconnsecuill As Boolean = cls_condbsecuill.chk_connect_db()
                If chkconnsecuill = True Then

                    If md.statusConnSecuill = False Then
                        '' Set status 
                        Me.setPictureBoxImg(md.imgCircleGreen, Me.pbStatusConSecuill)
                        Me.setlbStatusTxt("เชื่อมต่อฐานข้อมูล Secuill", Me.lbStatusConSecuillTxt, EventText.TextColor.Green)

                        md.EventText.SetText(EventText.TextState.Status, "เชื่อมต่อ Secuill สำเร็จ", EventText.TextColor.Green, EventText.WriteLog.Yes)
                    End If

                    md.statusConnSecuill = True

                Else

                    md.statusConnSecuill = False

                    '' Set status
                    Me.setPictureBoxImg(md.imgCircleRed, Me.pbStatusConSecuill)
                    Me.setlbStatusTxt("ไม่สามารถเชื่อมต่อ Secuill", Me.lbStatusConSecuillTxt, EventText.TextColor.Red)
                    md.EventText.SetText(EventText.TextState._Error, "ไม่สามารถเชื่อมต่อ Server secuill", EventText.TextColor.Red, EventText.WriteLog.Yes)

                    Thread.Sleep(2000)

                    '' Set status 
                    Me.setPictureBoxImg(md.imgCircleOrange, Me.pbStatusConSecuill)
                    Me.setlbStatusTxt("กำลังเชื่อมต่อ Secuill", Me.lbStatusConSecuillTxt, EventText.TextColor.Orange)

                    md.EventText.SetText(EventText.TextState.Status, "กำลังเชื่อมต่อใหม่อีกครั้ง Server secuill", EventText.TextColor.Orange, EventText.WriteLog.Yes)

                    Thread.Sleep(2000)

                End If


            Catch ex As Exception
                md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
            End Try

        Catch ex As Exception
            md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
        End Try

        Thread.Sleep(10)

    End Sub

    '' get middle

    Private strPoint As String = " ."
    Private Sub BgwGetMiddle_DoWork(sender As Object, e As EventArgs)

        Const fetchTime = 180 ' fetch user every 30 min. (30 min = (180 * 10,000[loopWaitTime]) ms
        Const _10_sec = 10000 ' ms

        Me.setlbStatusTxt("เริ่มต้นการทำงานดึงข้อมูล . . .", Me.lbStatusProcess, EventText.TextColor.Green)
        md.EventText.SetText(EventText.TextState.Status, "เริ่มต้นการทำงานดึงข้อมูล . . .", EventText.TextColor.Green, EventText.WriteLog.Yes)
        Dim threadFetch As New Thread(New ThreadStart(AddressOf fetchUser))

        Thread.Sleep(7000)
        Dim countTime As UInt16 = fetchTime
        Dim stopWatch As New Stopwatch
        Do
            stopWatch.Restart()
            '' check connection
            Me.CheckConnectMiddleAndSecuill()

            If md.statusConnMiddle = True And md.statusConnSecuill = True Then

                '' count data in table middle > 0
                Dim countMiddle As Integer = cls_getmiddletosecuill.CountDataMiddle()

                If countMiddle > 0 Then

                    Me.strPoint = " ."

                    '' process data insert to secuill
                    Me.setlbStatusTxt("ข้อมูล Middle " & countMiddle & " Rows", Me.lbStatusProcess, EventText.TextColor.Green)
                    md.EventText.SetText(EventText.TextState.Process, "ข้อมูล Middle " & countMiddle & " Rows", EventText.TextColor.Green, EventText.WriteLog.Yes)

                    Thread.Sleep(500)

                    cls_getmiddletosecuill.RunProcess()

                    Me.setlbStatusTxt(Date.Now.ToString("yyyy-MM-dd HH:mm:ss"), Me.Lbtime1, EventText.TextColor.Orange)

                Else

                    Me.strPoint &= " ."
                    If Me.strPoint.Length > 20 Then

                        Me.strPoint = " ."

                    End If

                    Me.setlbStatusTxt("กำลังรอข้อมูลจาก Middle" & Me.strPoint, Me.lbStatusProcess, EventText.TextColor.Orange)

                End If
#Region "Code Comment"
                '' Get prescription return to middle / goto smt

                'Dim ObjSecuill As New cls_intoSecuill
                'Dim DtResult As DataTable = ObjSecuill.GetPrescriptionReturn()
                'If Not DtResult Is Nothing Then
                '    If DtResult.Rows.Count > 0 Then

                '        Me.setlbStatusTxt("กำลังส่งข้อมูลให้ Middle" & Me.strPoint, Me.lbStatusProcess, EventText.TextColor.Orange)
                '        System.Threading.Thread.Sleep(1000)
                '        For i As Integer = 0 To DtResult.Rows.Count - 1
                '            Try

                '                Dim RunningNo As String = DtResult.Rows(i).Item("RunningNo").ToString()
                '                Dim PrescriptionNo As String = DtResult.Rows(i).Item("PrescriptionNo").ToString()
                '                Dim PrescriptionDesc As String = DtResult.Rows(i).Item("PrescriptionDesc").ToString()
                '                Dim PrescriptionDate As String = DtResult.Rows(i).Item("PrescriptionDate").ToString()
                '                Dim CreateDT As String = DtResult.Rows(i).Item("CreateDT").ToString()
                '                Dim TargetDate As String = DtResult.Rows(i).Item("TargetDate").ToString()
                '                Dim TargetTime As String = DtResult.Rows(i).Item("TargetTime").ToString()
                '                Dim Seq As String = DtResult.Rows(i).Item("Seq").ToString()
                '                Dim SeqMax As String = DtResult.Rows(i).Item("SeqMax").ToString()
                '                Dim PatHN As String = DtResult.Rows(i).Item("PatHN").ToString()
                '                Dim PatAN As String = DtResult.Rows(i).Item("PatAN").ToString()
                '                Dim PatNm As String = DtResult.Rows(i).Item("PatNm").ToString()
                '                Dim WardCd As String = DtResult.Rows(i).Item("WardCd").ToString()
                '                Dim WardNm As String = DtResult.Rows(i).Item("WardNm").ToString()
                '                Dim DrugCd As String = DtResult.Rows(i).Item("DrugCd").ToString()
                '                Dim DrugNm As String = DtResult.Rows(i).Item("DrugNm").ToString()
                '                Dim OrderQty As String = DtResult.Rows(i).Item("OrderQty").ToString()
                '                Dim UnitNm As String = DtResult.Rows(i).Item("UnitNm").ToString()
                '                Dim Dosage As String = DtResult.Rows(i).Item("Dosage").ToString()
                '                Dim DosageUnit As String = DtResult.Rows(i).Item("DosageUnit").ToString()
                '                Dim InstructionCd As String = DtResult.Rows(i).Item("InstructionCd").ToString()
                '                Dim InstructionNm As String = DtResult.Rows(i).Item("InstructionNm").ToString()
                '                Dim PriorityCd As String = DtResult.Rows(i).Item("PriorityCd").ToString()
                '                Dim PriorityNm As String = DtResult.Rows(i).Item("PriorityNm").ToString()
                '                Dim StatPrn As String = DtResult.Rows(i).Item("StatPrn").ToString()
                '                Dim HialertDrug As String = DtResult.Rows(i).Item("HialertDrug").ToString()
                '                Dim FrequencyCd As String = DtResult.Rows(i).Item("FrequencyCd").ToString()
                '                Dim FrequencyTime As String = DtResult.Rows(i).Item("FrequencyTime").ToString()
                '                Dim NotProcessing As String = DtResult.Rows(i).Item("NotProcessing").ToString()
                '                Dim MachLocationID As String = DtResult.Rows(i).Item("MachLocationID").ToString()
                '                Dim MachLocationDesc As String = DtResult.Rows(i).Item("MachLocationDesc").ToString()
                '                Dim ProcessNo As String = DtResult.Rows(i).Item("ProcessNo").ToString()
                '                Dim ProcessID As String = DtResult.Rows(i).Item("ProcessID").ToString()
                '                Dim ProcessDesc As String = DtResult.Rows(i).Item("ProcessDesc").ToString()
                '                Dim UserID As String = DtResult.Rows(i).Item("UserID").ToString()
                '                Dim UserNm As String = DtResult.Rows(i).Item("UserNm").ToString()
                '                Dim UserID2 As String = DtResult.Rows(i).Item("UserID2").ToString()
                '                Dim UserNm2 As String = DtResult.Rows(i).Item("UserNm2").ToString()
                '                Dim PickDate As String = DtResult.Rows(i).Item("PickDate").ToString()
                '                Dim PickTime As String = DtResult.Rows(i).Item("PickTime").ToString()
                '                Dim ReturnStatus As String = DtResult.Rows(i).Item("ReturnStatus").ToString()
                '                Dim ReturnDesc As String = DtResult.Rows(i).Item("ReturnDesc").ToString()
                '                Dim LastModified As String = DtResult.Rows(i).Item("LastModified").ToString()

                '                Dim ObjMiddle As New cls_middleObject

                '                ObjMiddle.F_prescriptionno = PrescriptionDesc
                '                ObjMiddle.F_seq = Seq
                '                ObjMiddle.F_seqmax = SeqMax
                '                ObjMiddle.F_runningno = RunningNo
                '                ObjMiddle.F_prescriptiondate = PrescriptionDate
                '                ObjMiddle.F_ordercreatedate = CreateDT
                '                ObjMiddle.F_ordertargetdate = TargetDate
                '                ObjMiddle.F_ordertargettime = TargetTime
                '                ObjMiddle.F_pharmacylocationcode = MachLocationID
                '                ObjMiddle.F_pharmacylocationdesc = MachLocationDesc
                '                ObjMiddle.F_userorderby = UserNm
                '                ObjMiddle.F_useracceptby = UserNm
                '                ObjMiddle.F_orderacceptdate = LastModified
                '                ObjMiddle.F_orderacceptfromip = Me.GetIP()
                '                ObjMiddle.F_dispensestatus = 0
                '                ObjMiddle.F_status = 0
                '                ObjMiddle.F_hn = PatHN
                '                ObjMiddle.F_an = PatAN
                '                ObjMiddle.F_patientname = PatNm
                '                ObjMiddle.F_wardcode = WardCd
                '                ObjMiddle.F_warddesc = WardNm
                '                ObjMiddle.F_tomachineno = 1
                '                ObjMiddle.F_orderitemcode = DrugCd
                '                ObjMiddle.F_orderitemname = DrugNm
                '                ObjMiddle.F_orderqty = OrderQty
                '                ObjMiddle.F_orderunitcode = UnitNm
                '                ObjMiddle.F_orderunitdesc = ""
                '                ObjMiddle.F_dosage = IIf(IsNumeric(Dosage) = False, 0.0, Dosage)
                '                ObjMiddle.F_dosageunit = DosageUnit
                '                ObjMiddle.F_instructioncode = InstructionCd
                '                ObjMiddle.F_instructiondesc = InstructionNm
                '                ObjMiddle.F_highalertdrug = HialertDrug
                '                ObjMiddle.F_prnstat = "2"
                '                ObjMiddle.F_prioritycode = PriorityCd
                '                ObjMiddle.F_prioritydesc = PriorityNm
                '                ObjMiddle.F_frequencycode = FrequencyCd
                '                ObjMiddle.F_frequencydesc = ProcessDesc
                '                ObjMiddle.F_frequencyTime = FrequencyTime
                '                ObjMiddle.F_durationcode = 1
                '                ObjMiddle.F_noteprocessing = NotProcessing
                '                ObjMiddle.F_comment = "Prescription return to middle / goto SMT"
                '                ObjMiddle.f_dispensestatus_lgs = "0"
                '                ObjMiddle.f_dispensestatus_secuill = "1"
                '                ObjMiddle.f_Absolute = "0"
                '                ObjMiddle.f_dispensestatus_smt = "0"
                '                ObjMiddle.f_status_scancharge = "1"

                '                Dim insertreslut As Boolean = ObjMiddle.InsertIntoMiddle()
                '                If insertreslut = True Then
                '                    Me.UpdatePrescriptionReturn(1, ObjMiddle.F_runningno, ObjMiddle.F_prescriptiondate)

                '                    Me.setlbStatusTxt("กำลังส่งข้อมูลให้ Middle สำเร็จ > " & PrescriptionDesc, Me.lbStatusProcess, EventText.TextColor.Orange)
                '                Else
                '                    Me.UpdatePrescriptionReturn(2, ObjMiddle.F_runningno, ObjMiddle.F_prescriptiondate)

                '                    Me.setlbStatusTxt("กำลังส่งข้อมูลให้ Middle ERROR > " & PrescriptionDesc, Me.lbStatusProcess, EventText.TextColor.Orange)
                '                End If

                '            Catch ex As Exception
                '                Me.setlbStatusTxt("กำลังส่งข้อมูลให้ Middle ERROR > " & ex.ToString(), Me.lbStatusProcess, EventText.TextColor.Orange)
                '            End Try

                '            System.Threading.Thread.Sleep(100)

                '        Next
                '    End If
                'End If
#End Region

            End If

            If countTime Mod 6 = 0 Then
                runSubByThread(AddressOf timeStamp.updateTimeStamp)
            End If


            If countTime >= fetchTime Then

                countTime = 0

                Try
                    '' Fetch data in table from another database
                    If threadFetch.ThreadState = Threading.ThreadState.Stopped Then
                        threadFetch = New Thread(New ThreadStart(AddressOf fetchUser))
                    End If

                    If threadFetch.ThreadState = Threading.ThreadState.Unstarted Then
                        threadFetch.IsBackground = True
                        threadFetch.Start()
                    End If

                Catch ex As Exception
                End Try
            Else
                countTime = countTime + 1
            End If

            setTextLable($"{stopWatch.ElapsedMilliseconds} ms.", lbProcessTime)
            Thread.Sleep(_10_sec)
        Loop

    End Sub

    '' -------------------------------------------- EVNT TEXT --------------------------------------------------
    Private Sub btnViewLog_Click(sender As Object, e As EventArgs) Handles btnViewLog.Click

        If Me.btnViewLog.Text = ">>" Then

            Me.btnViewLog.Text = "<<"

            Me.Width = Me.Width + Me.frmWidthEventSize

            Me.Location = New Point(Me.Location.X - (Me.frmWidthEventSize / 2), Me.Location.Y)

            RemoveHandler Me.bgwRunEventText.DoWork, AddressOf bgwRunEventText_DoWork
            AddHandler Me.bgwRunEventText.DoWork, AddressOf bgwRunEventText_DoWork
            Me.bgwRunEventText.WorkerSupportsCancellation = True
            If Me.bgwRunEventText.IsBusy Then
                Me.bgwRunEventText.RunWorkerAsync()
            End If

        ElseIf Me.btnViewLog.Text = "<<" Then

            Me.btnViewLog.Text = ">>"

            Me.Width = Me.Width - Me.frmWidthEventSize

            Me.Location = New Point(Me.Location.X + (Me.frmWidthEventSize / 2), Me.Location.Y)

            Me.bgwRunEventText.CancelAsync()
            RemoveHandler Me.bgwRunEventText.DoWork, AddressOf bgwRunEventText_DoWork

        End If

    End Sub

    Private Sub bgwRunEventText_DoWork(sender As Object, e As EventArgs)

        Dim bwk As BackgroundWorker = sender

        Do


            Me.setRtbEventTxt("AAAAAAAAAAAAAAAAAAAAAAAA", Me.rtbEvent, md.EventText.TextColor.Green)

            Thread.Sleep(1000)

        Loop

    End Sub

    '' decaled 
    Private Sub setRtbEventTxt(ByVal text As String, ByVal rtb As RichTextBox, ByVal mColorTxt As String)
        If rtb.InvokeRequired Then
            rtb.Invoke(New setRtbEventTxtInvoker(AddressOf setRtbEventTxt), text, rtb, mColorTxt)
        Else

            Select Case mColorTxt
                Case md.EventText.TextColor.Green
                    rtb.SelectionColor = md.mColorstatusConn
                Case md.EventText.TextColor.Red
                    rtb.SelectionColor = md.mColorstatusError
                Case md.EventText.TextColor.Orange
                    rtb.SelectionColor = md.mColorstatusWarning
                Case Else
                    rtb.SelectionColor = md.mColorBorder
            End Select

            rtb.AppendText(text & Environment.NewLine)
            rtb.ScrollToCaret()

        End If
    End Sub
    Private Delegate Sub setRtbEventTxtInvoker(ByVal text As String, ByVal lbl As RichTextBox, ByVal mColorTxt As String)

    Private Function GetIP() As String
        Dim resp As String = String.Empty

        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                resp = ipheal.ToString()
            End If
        Next

        Return resp
    End Function

    Private Function UpdatePrescriptionReturn(ByVal st As String, ByVal f_runningno As String, ByVal pres_date As String) As Boolean
        Dim SQL As String = String.Empty
        SQL = " SET XACT_ABORT ON"
        SQL &= " BEGIN TRANSACTION"

        SQL &= " UPDATE T_PrescriptionReturn"
        SQL &= " SET ReturnStatus = '" & st & "'"
        SQL &= " WHERE RunningNo = '" & f_runningno & "'"
        SQL &= " AND PrescriptionDate = '" & pres_date & "'"
        SQL &= " AND ReturnStatus = '0'"

        SQL &= " COMMIT TRANSACTION"
        SQL &= " SET XACT_ABORT OFF"

        Try
            Using cmd As New SqlClient.SqlCommand(SQL)
                cls_condbsecuill.ExecuteNonQuery(cmd, " (UpdatePrescriptionReturn())")
                Return True
            End Using

        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (UpdatePrescriptionReturn())"))
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Set update date from table "M_User" and "M_UserLogin" to ref variable for check update.
    ''' </summary>
    ''' <param name="userDateUpdate">Keep date from M_User</param>
    ''' <param name="userLoginDateUpdate">Keep date from M_UserLogin</param>
    Private Sub setDateUpdate(row As DataRow, ByRef userDateUpdate As Date, ByRef userLoginDateUpdate As Date)
        With row
            If .Item(userUpdateDate).GetType() <> GetType(DBNull) And .Item(userUpdateTime).GetType() <> GetType(DBNull) Then
                userDateUpdate = Convert.ToDateTime($"{ .Item(userUpdateDate)} { .Item(userUpdateTime)}")
            End If

            If .Item(userLoginUpdateDate).GetType() <> GetType(DBNull) And .Item(userLoginUpdateTime).GetType() <> GetType(DBNull) Then
                userLoginDateUpdate = Convert.ToDateTime($"{ .Item(userLoginUpdateDate)} { .Item(userLoginUpdateTime)}")
            End If
        End With
    End Sub

    ''' <summary>
    ''' ดึงข้อมูลจากเครื่องอื่น แล้วนำมาเทียบกับ local 
    ''' หากมีข้อมูลที่ต่างกัน ทำการ update
    ''' หากไม่มีข้อมูล local ทำการ Insert
    ''' </summary>
    Private Sub fetchUser()
        If Not cls_condbsecuill.chk_connect_db() Then
            Exit Sub
        End If

        For Each connection As JSONSqlConnection In jsonConnection

            Dim sourceConnectionString As String = createConnectionString(connection.dataSource, connection.databaseName, connection.user, connection.password)

            Dim dsSource As DataSet

            Dim query As String =
                $"SELECT *, (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'M_User') as column_count
                FROM M_User
                FULL JOIN M_UserLogin
                ON M_User.user_id = M_UserLogin.user_id
                WHERE M_User.user_location_id = '2'"

            dsSource = Fill(sourceConnectionString, query)

            If dsSource Is Nothing Then
                Continue For
            End If

            For rowIndex = 0 To dsSource.Tables(0).Rows.Count - 1
                Try
                    '' Select data by user_id
                    query =
                    $"SELECT M_User.user_id, M_UserLogin.ulogin_id, user_updatedate, user_updatetime, ulogin_updatedate, ulogin_updatetime
                    FROM M_User
                    FULL JOIN M_UserLogin
                        ON M_User.user_id = M_UserLogin.user_id
                    WHERE M_User.user_id = '{dsSource.Tables(0).Rows(rowIndex).Item("user_id")}'
                        OR M_UserLogin.ulogin_id = '{dsSource.Tables(0).Rows(rowIndex).Item("ulogin_id")}'"


                    Dim dsLocal As DataSet = cls_condbsecuill.Fill(query, 0, Nothing)

                    If dsLocal Is Nothing Then
                        Continue For
                    ElseIf dsLocal.Tables(0).Rows.Count = 0 Then
                        Dim values As String() = createSQLValuesInsert(dsSource.Tables(0).Rows(rowIndex))
                        query =
                            $"INSERT INTO M_User
                        VALUES({values(0)});
                        INSERT INTO M_UserLogin
                        VALUES({values(1)})"

                        cls_condbsecuill.ExecuteNonQuery(query)

                    Else
                        Dim userDateSource As New Date
                        Dim userLoginDateSource As New Date

                        Dim userDateLocal As New Date
                        Dim userLoginDateLocal As New Date

                        With dsSource.Tables(0).Rows(rowIndex)
                            If .Item(userUpdateDate).GetType() = GetType(DBNull) And .Item(userUpdateTime).GetType() = GetType(DBNull) And
                                .Item(userLoginUpdateDate).GetType() = GetType(DBNull) And .Item(userLoginUpdateTime).GetType() = GetType(DBNull) Then
                                '' User update date time is null next loop
                                Continue For
                            End If
                        End With

                        setDateUpdate(dsSource.Tables(0).Rows(rowIndex), userDateSource, userLoginDateSource)
                        setDateUpdate(dsLocal.Tables(0).Rows(0), userDateLocal, userLoginDateLocal)

                        If DateDiff(DateInterval.Minute, userDateLocal, userDateSource) > 0 Or
                            DateDiff(DateInterval.Minute, userLoginDateLocal, userLoginDateSource) > 0 Then

                            Dim values As String() = createSQLUpdateValue(dsSource.Tables(0).Columns, dsSource.Tables(0).Rows(rowIndex))
                            query =
                                $"UPDATE M_User
                                SET {values(0)}
                                WHERE user_id = '{dsSource.Tables(0).Rows(rowIndex).Item("user_id")}'
                                UPDATE M_UserLogin
                                SET {values(1)}
                                WHERE ulogin_id = '{dsSource.Tables(0).Rows(rowIndex).Item("ulogin_id")}'"

                            cls_condbsecuill.ExecuteNonQuery(query, "(frmMain.fetchUser)")
                        End If
                    End If

                Catch ex As Exception
                    cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (fetchUser())"))
                End Try

            Next
        Next
    End Sub

    Private Function createSQLValuesInsert(row As DataRow) As String()
        '' Set SQL insert values
        Dim values As String() = {"", ""}
        Dim itemArray As Object() = row.ItemArray
        Dim index As Byte = 0
        Dim userColumnCnt As Byte = Convert.ToByte(row("column_count"))

        For i = 0 To row.ItemArray.Count - 2
            If i = userColumnCnt Then
                index = 1
            End If
            Select Case itemArray(i).GetType()
                Case GetType(String)
                    values(index) = values(index) + $"N'{itemArray(i)}',"
                Case GetType(DBNull)
                    values(index) = values(index) + $"NULL,"
                Case GetType(Decimal), GetType(Byte)
                    values(index) = values(index) + $"{itemArray(i)},"
                Case GetType(DateTime)
                    values(index) = values(index) + $"CAST(N'{itemArray(i)}' AS DateTime),"
            End Select
        Next
#Region "Comment Code"
        'For Each column In row.ItemArray
        '    Select Case column.GetType()
        '        Case GetType(String)
        '            values = values + $"N'{column}',"
        '        Case GetType(DBNull)
        '            values = values + $"NULL,"
        '        Case GetType(Decimal), GetType(Byte)
        '            values = values + $"{column},"
        '        Case GetType(DateTime)
        '            values = values + $"CAST(N'{column}' AS DateTime),"
        '    End Select
        'Next
#End Region

        For i = 0 To 1
            '' Return remove ',' at last
            values(i) = values(i).Remove(values(i).Length - 1)
        Next

        Return values
    End Function

    ''' <summary>
    ''' Create query "UPDATE Table_name SET [...create this...]"
    ''' </summary>
    ''' <param name="column"></param>
    ''' <param name="row"></param>
    ''' <returns>Index 0: M_User, 1: M_UserLogin</returns>
    Private Function createSQLUpdateValue(column As DataColumnCollection, row As DataRow) As String()
        '' Set SQL update values
        Dim values As String() = {"", ""}
        Dim itemArray As Object() = row.ItemArray
        Dim index As Byte = 0   '' 0 = table User, 1 = table UserLogin
        Dim userColumnCnt As Byte = Convert.ToByte(row("column_count"))

        For i = 0 To column.Count - 2
            If i = userColumnCnt Then
                index = 1
            End If
            Dim columName As String = column(i).ColumnName
            If columName.Last = "1" Then
                columName = columName.Remove(columName.Length - 1)
            End If
            Select Case itemArray(i).GetType()
                Case GetType(String)
                    values(index) = $"{values(index)}{columName}=N'{row(i)}',"
                Case GetType(DBNull)
                    values(index) = $"{values(index)}{columName}=NULL,"
                Case GetType(Decimal), GetType(Byte)
                    values(index) = $"{values(index)}{columName}={row(i)},"
                Case GetType(DateTime)
                    values(index) = $"{values(index)}{columName}=CAST(N'{row(i)}' AS DateTime),"
            End Select
        Next
#Region "Comment Code"
        'For index = 0 To column.Count - 1
        '    Select Case row(index).GetType()
        '        Case GetType(String)
        '            values = values + $"{column(index).ColumnName}=N'{row(index)}',"
        '        Case GetType(DBNull)
        '            values = values + $"{column(index).ColumnName}=NULL,"
        '        Case GetType(Decimal), GetType(Byte)
        '            values = values + $"{column(index).ColumnName}={row(index)},"
        '        Case GetType(DateTime)
        '            values = values + $"{column(index).ColumnName}=CAST(N'{row(index)}' AS DateTime),"
        '    End Select
        'Next
#End Region

        For i = 0 To 1
            '' remove ',' at last
            values(i) = values(i).Remove(values(i).Length - 1)
        Next

        Return values
    End Function

    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        timeStamp.endProgramTimeStamp()
    End Sub

End Class
