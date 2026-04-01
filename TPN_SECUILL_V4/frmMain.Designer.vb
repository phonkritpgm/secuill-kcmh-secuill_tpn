<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BgwLoaded = New System.ComponentModel.BackgroundWorker()
        Me.TmLoaded = New System.Windows.Forms.Timer(Me.components)
        Me.LbPercent = New System.Windows.Forms.Label()
        Me.lbStatusProcess = New System.Windows.Forms.Label()
        Me.lbStatusConMiddleTxt = New System.Windows.Forms.Label()
        Me.pgLoadding = New System.Windows.Forms.ProgressBar()
        Me.btnStartConnect = New System.Windows.Forms.Button()
        Me.TmDataCompare = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbTxtProcess = New System.Windows.Forms.Label()
        Me.Lbtime2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Lbtime1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lbSecuill = New System.Windows.Forms.Label()
        Me.lbHeaderTxt = New System.Windows.Forms.Label()
        Me.pbSecuill = New System.Windows.Forms.PictureBox()
        Me.btn_exitprogram = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lbProcessTime = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.pbStatusConSecuill = New System.Windows.Forms.PictureBox()
        Me.lbStatusConSecuillTxt = New System.Windows.Forms.Label()
        Me.pbStatusConMiddle = New System.Windows.Forms.PictureBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lbHeadStatusTxt = New System.Windows.Forms.Label()
        Me.panelStatusTxt = New System.Windows.Forms.Panel()
        Me.lbStatusTxt2 = New System.Windows.Forms.Label()
        Me.lbStatusTxt1 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnViewLog = New System.Windows.Forms.Button()
        Me.rtbEvent = New System.Windows.Forms.RichTextBox()
        Me.Panel1.SuspendLayout()
        CType(Me.pbSecuill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.pbStatusConSecuill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStatusConMiddle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.panelStatusTxt.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(318, -73)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(100, 10)
        Me.Panel2.TabIndex = 13
        '
        'TmLoaded
        '
        Me.TmLoaded.Interval = 1000
        '
        'LbPercent
        '
        Me.LbPercent.AutoSize = True
        Me.LbPercent.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbPercent.ForeColor = System.Drawing.Color.White
        Me.LbPercent.Location = New System.Drawing.Point(346, 8)
        Me.LbPercent.Name = "LbPercent"
        Me.LbPercent.Size = New System.Drawing.Size(41, 34)
        Me.LbPercent.TabIndex = 6
        Me.LbPercent.Text = "0%"
        Me.LbPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbStatusProcess
        '
        Me.lbStatusProcess.AutoSize = True
        Me.lbStatusProcess.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbStatusProcess.ForeColor = System.Drawing.Color.White
        Me.lbStatusProcess.Location = New System.Drawing.Point(92, 8)
        Me.lbStatusProcess.Name = "lbStatusProcess"
        Me.lbStatusProcess.Size = New System.Drawing.Size(150, 34)
        Me.lbStatusProcess.TabIndex = 5
        Me.lbStatusProcess.Text = "กำลังรอการเชื่อมต่อ"
        '
        'lbStatusConMiddleTxt
        '
        Me.lbStatusConMiddleTxt.AutoSize = True
        Me.lbStatusConMiddleTxt.BackColor = System.Drawing.Color.Transparent
        Me.lbStatusConMiddleTxt.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbStatusConMiddleTxt.ForeColor = System.Drawing.Color.Aquamarine
        Me.lbStatusConMiddleTxt.Location = New System.Drawing.Point(26, 1)
        Me.lbStatusConMiddleTxt.Name = "lbStatusConMiddleTxt"
        Me.lbStatusConMiddleTxt.Size = New System.Drawing.Size(162, 34)
        Me.lbStatusConMiddleTxt.TabIndex = 4
        Me.lbStatusConMiddleTxt.Text = "หยุดเชื่อมต่อ Middle"
        '
        'pgLoadding
        '
        Me.pgLoadding.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.pgLoadding.Location = New System.Drawing.Point(8, 44)
        Me.pgLoadding.Name = "pgLoadding"
        Me.pgLoadding.Size = New System.Drawing.Size(373, 18)
        Me.pgLoadding.TabIndex = 3
        '
        'btnStartConnect
        '
        Me.btnStartConnect.BackColor = System.Drawing.Color.Teal
        Me.btnStartConnect.FlatAppearance.BorderSize = 0
        Me.btnStartConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStartConnect.ForeColor = System.Drawing.Color.Red
        Me.btnStartConnect.Image = CType(resources.GetObject("btnStartConnect.Image"), System.Drawing.Image)
        Me.btnStartConnect.Location = New System.Drawing.Point(8, 141)
        Me.btnStartConnect.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnStartConnect.Name = "btnStartConnect"
        Me.btnStartConnect.Size = New System.Drawing.Size(100, 100)
        Me.btnStartConnect.TabIndex = 2
        Me.btnStartConnect.Text = " "
        Me.btnStartConnect.UseVisualStyleBackColor = False
        '
        'TmDataCompare
        '
        Me.TmDataCompare.Interval = 2000
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Teal
        Me.Panel1.Controls.Add(Me.lbTxtProcess)
        Me.Panel1.Controls.Add(Me.LbPercent)
        Me.Panel1.Controls.Add(Me.lbStatusProcess)
        Me.Panel1.Controls.Add(Me.pgLoadding)
        Me.Panel1.Location = New System.Drawing.Point(111, 173)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(399, 68)
        Me.Panel1.TabIndex = 10
        '
        'lbTxtProcess
        '
        Me.lbTxtProcess.AutoSize = True
        Me.lbTxtProcess.BackColor = System.Drawing.Color.Transparent
        Me.lbTxtProcess.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTxtProcess.ForeColor = System.Drawing.Color.NavajoWhite
        Me.lbTxtProcess.Location = New System.Drawing.Point(4, 7)
        Me.lbTxtProcess.Name = "lbTxtProcess"
        Me.lbTxtProcess.Size = New System.Drawing.Size(96, 34)
        Me.lbTxtProcess.TabIndex = 7
        Me.lbTxtProcess.Text = "Proocess :"
        '
        'Lbtime2
        '
        Me.Lbtime2.AutoSize = True
        Me.Lbtime2.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbtime2.ForeColor = System.Drawing.Color.LavenderBlush
        Me.Lbtime2.Location = New System.Drawing.Point(150, 31)
        Me.Lbtime2.Name = "Lbtime2"
        Me.Lbtime2.Size = New System.Drawing.Size(145, 34)
        Me.Lbtime2.TabIndex = 3
        Me.Lbtime2.Text = "2019-07-24 10:06"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.LavenderBlush
        Me.Label6.Location = New System.Drawing.Point(150, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 34)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "เปิดใช้งาน"
        '
        'Lbtime1
        '
        Me.Lbtime1.AutoSize = True
        Me.Lbtime1.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbtime1.ForeColor = System.Drawing.Color.LavenderBlush
        Me.Lbtime1.Location = New System.Drawing.Point(6, 31)
        Me.Lbtime1.Name = "Lbtime1"
        Me.Lbtime1.Size = New System.Drawing.Size(145, 34)
        Me.Lbtime1.TabIndex = 19
        Me.Lbtime1.Text = "2019-07-24 10:06"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.LavenderBlush
        Me.Label3.Location = New System.Drawing.Point(6, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 34)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "เวลาข้อมูลล่าสุด"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Teal
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.LavenderBlush
        Me.Button1.Location = New System.Drawing.Point(348, 5)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(69, 67)
        Me.Button1.TabIndex = 26
        Me.Button1.Text = " Hide"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'lbSecuill
        '
        Me.lbSecuill.AutoSize = True
        Me.lbSecuill.Font = New System.Drawing.Font("TH SarabunPSK", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbSecuill.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbSecuill.Location = New System.Drawing.Point(81, 44)
        Me.lbSecuill.Name = "lbSecuill"
        Me.lbSecuill.Size = New System.Drawing.Size(259, 45)
        Me.lbSecuill.TabIndex = 21
        Me.lbSecuill.Text = " SECUILL V4 (Ward ALL)"
        '
        'lbHeaderTxt
        '
        Me.lbHeaderTxt.AutoSize = True
        Me.lbHeaderTxt.Font = New System.Drawing.Font("TH SarabunPSK", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbHeaderTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbHeaderTxt.Location = New System.Drawing.Point(87, 3)
        Me.lbHeaderTxt.Name = "lbHeaderTxt"
        Me.lbHeaderTxt.Size = New System.Drawing.Size(255, 45)
        Me.lbHeaderTxt.TabIndex = 22
        Me.lbHeaderTxt.Text = "โปรแกรมเชื่อมต่อข้อมูล"
        '
        'pbSecuill
        '
        Me.pbSecuill.Image = CType(resources.GetObject("pbSecuill.Image"), System.Drawing.Image)
        Me.pbSecuill.Location = New System.Drawing.Point(18, 3)
        Me.pbSecuill.Name = "pbSecuill"
        Me.pbSecuill.Size = New System.Drawing.Size(70, 69)
        Me.pbSecuill.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbSecuill.TabIndex = 25
        Me.pbSecuill.TabStop = False
        '
        'btn_exitprogram
        '
        Me.btn_exitprogram.BackColor = System.Drawing.Color.Teal
        Me.btn_exitprogram.FlatAppearance.BorderColor = System.Drawing.Color.DimGray
        Me.btn_exitprogram.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_exitprogram.ForeColor = System.Drawing.Color.Red
        Me.btn_exitprogram.Image = CType(resources.GetObject("btn_exitprogram.Image"), System.Drawing.Image)
        Me.btn_exitprogram.Location = New System.Drawing.Point(436, 5)
        Me.btn_exitprogram.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btn_exitprogram.Name = "btn_exitprogram"
        Me.btn_exitprogram.Size = New System.Drawing.Size(69, 67)
        Me.btn_exitprogram.TabIndex = 24
        Me.btn_exitprogram.Text = " "
        Me.btn_exitprogram.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Teal
        Me.Panel4.Controls.Add(Me.lbProcessTime)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.Lbtime1)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Lbtime2)
        Me.Panel4.Location = New System.Drawing.Point(8, 78)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(502, 60)
        Me.Panel4.TabIndex = 27
        '
        'lbProcessTime
        '
        Me.lbProcessTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbProcessTime.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbProcessTime.ForeColor = System.Drawing.Color.White
        Me.lbProcessTime.Location = New System.Drawing.Point(422, 31)
        Me.lbProcessTime.Name = "lbProcessTime"
        Me.lbProcessTime.Size = New System.Drawing.Size(77, 27)
        Me.lbProcessTime.TabIndex = 21
        Me.lbProcessTime.Text = "- ms"
        Me.lbProcessTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Teal
        Me.Panel5.Controls.Add(Me.pbStatusConSecuill)
        Me.Panel5.Controls.Add(Me.lbStatusConSecuillTxt)
        Me.Panel5.Controls.Add(Me.pbStatusConMiddle)
        Me.Panel5.Controls.Add(Me.lbStatusConMiddleTxt)
        Me.Panel5.Location = New System.Drawing.Point(111, 141)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(399, 29)
        Me.Panel5.TabIndex = 28
        '
        'pbStatusConSecuill
        '
        Me.pbStatusConSecuill.Image = CType(resources.GetObject("pbStatusConSecuill.Image"), System.Drawing.Image)
        Me.pbStatusConSecuill.Location = New System.Drawing.Point(195, 5)
        Me.pbStatusConSecuill.Name = "pbStatusConSecuill"
        Me.pbStatusConSecuill.Size = New System.Drawing.Size(20, 20)
        Me.pbStatusConSecuill.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbStatusConSecuill.TabIndex = 23
        Me.pbStatusConSecuill.TabStop = False
        '
        'lbStatusConSecuillTxt
        '
        Me.lbStatusConSecuillTxt.AutoSize = True
        Me.lbStatusConSecuillTxt.BackColor = System.Drawing.Color.Transparent
        Me.lbStatusConSecuillTxt.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbStatusConSecuillTxt.ForeColor = System.Drawing.Color.Aquamarine
        Me.lbStatusConSecuillTxt.Location = New System.Drawing.Point(220, 1)
        Me.lbStatusConSecuillTxt.Name = "lbStatusConSecuillTxt"
        Me.lbStatusConSecuillTxt.Size = New System.Drawing.Size(159, 34)
        Me.lbStatusConSecuillTxt.TabIndex = 22
        Me.lbStatusConSecuillTxt.Text = "หยุดเชื่อมต่อ Secuill"
        '
        'pbStatusConMiddle
        '
        Me.pbStatusConMiddle.Image = CType(resources.GetObject("pbStatusConMiddle.Image"), System.Drawing.Image)
        Me.pbStatusConMiddle.Location = New System.Drawing.Point(4, 5)
        Me.pbStatusConMiddle.Name = "pbStatusConMiddle"
        Me.pbStatusConMiddle.Size = New System.Drawing.Size(20, 20)
        Me.pbStatusConMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbStatusConMiddle.TabIndex = 21
        Me.pbStatusConMiddle.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Teal
        Me.Panel3.Controls.Add(Me.lbHeadStatusTxt)
        Me.Panel3.Location = New System.Drawing.Point(8, 244)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(100, 29)
        Me.Panel3.TabIndex = 28
        '
        'lbHeadStatusTxt
        '
        Me.lbHeadStatusTxt.AutoSize = True
        Me.lbHeadStatusTxt.BackColor = System.Drawing.Color.Transparent
        Me.lbHeadStatusTxt.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbHeadStatusTxt.ForeColor = System.Drawing.Color.Cyan
        Me.lbHeadStatusTxt.Location = New System.Drawing.Point(-2, 4)
        Me.lbHeadStatusTxt.Name = "lbHeadStatusTxt"
        Me.lbHeadStatusTxt.Size = New System.Drawing.Size(78, 34)
        Me.lbHeadStatusTxt.TabIndex = 4
        Me.lbHeadStatusTxt.Text = "สถานะ >"
        '
        'panelStatusTxt
        '
        Me.panelStatusTxt.BackColor = System.Drawing.Color.Teal
        Me.panelStatusTxt.Controls.Add(Me.lbStatusTxt2)
        Me.panelStatusTxt.Controls.Add(Me.lbStatusTxt1)
        Me.panelStatusTxt.Location = New System.Drawing.Point(111, 244)
        Me.panelStatusTxt.Name = "panelStatusTxt"
        Me.panelStatusTxt.Size = New System.Drawing.Size(359, 29)
        Me.panelStatusTxt.TabIndex = 5
        '
        'lbStatusTxt2
        '
        Me.lbStatusTxt2.AutoSize = True
        Me.lbStatusTxt2.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbStatusTxt2.ForeColor = System.Drawing.Color.Tomato
        Me.lbStatusTxt2.Location = New System.Drawing.Point(63, 3)
        Me.lbStatusTxt2.Name = "lbStatusTxt2"
        Me.lbStatusTxt2.Size = New System.Drawing.Size(64, 34)
        Me.lbStatusTxt2.TabIndex = 0
        Me.lbStatusTxt2.Text = "s txt 2"
        Me.lbStatusTxt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbStatusTxt1
        '
        Me.lbStatusTxt1.AutoSize = True
        Me.lbStatusTxt1.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbStatusTxt1.ForeColor = System.Drawing.Color.NavajoWhite
        Me.lbStatusTxt1.Location = New System.Drawing.Point(4, 3)
        Me.lbStatusTxt1.Name = "lbStatusTxt1"
        Me.lbStatusTxt1.Size = New System.Drawing.Size(64, 34)
        Me.lbStatusTxt1.TabIndex = 0
        Me.lbStatusTxt1.Text = "s txt 1"
        Me.lbStatusTxt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Panel6.ForeColor = System.Drawing.Color.LightBlue
        Me.Panel6.Location = New System.Drawing.Point(92, 38)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(182, 5)
        Me.Panel6.TabIndex = 29
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(4, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 34)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "T"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(4, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 34)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "P"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("TH SarabunPSK", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(4, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 34)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "N"
        '
        'btnViewLog
        '
        Me.btnViewLog.Enabled = False
        Me.btnViewLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewLog.Location = New System.Drawing.Point(474, 244)
        Me.btnViewLog.Margin = New System.Windows.Forms.Padding(0)
        Me.btnViewLog.Name = "btnViewLog"
        Me.btnViewLog.Size = New System.Drawing.Size(33, 29)
        Me.btnViewLog.TabIndex = 33
        Me.btnViewLog.Text = ">>"
        Me.btnViewLog.UseVisualStyleBackColor = True
        '
        'rtbEvent
        '
        Me.rtbEvent.Font = New System.Drawing.Font("TH SarabunPSK", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbEvent.Location = New System.Drawing.Point(516, 5)
        Me.rtbEvent.Name = "rtbEvent"
        Me.rtbEvent.Size = New System.Drawing.Size(446, 268)
        Me.rtbEvent.TabIndex = 34
        Me.rtbEvent.Text = ""
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(968, 280)
        Me.Controls.Add(Me.rtbEvent)
        Me.Controls.Add(Me.btnViewLog)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.panelStatusTxt)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lbSecuill)
        Me.Controls.Add(Me.btnStartConnect)
        Me.Controls.Add(Me.lbHeaderTxt)
        Me.Controls.Add(Me.pbSecuill)
        Me.Controls.Add(Me.btn_exitprogram)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Constantia", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.pbSecuill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.pbStatusConSecuill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStatusConMiddle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.panelStatusTxt.ResumeLayout(False)
        Me.panelStatusTxt.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BgwLoaded As System.ComponentModel.BackgroundWorker
    Friend WithEvents TmLoaded As Timer
    Friend WithEvents LbPercent As Label
    Friend WithEvents lbStatusProcess As Label
    Friend WithEvents lbStatusConMiddleTxt As Label
    Friend WithEvents pgLoadding As ProgressBar
    Friend WithEvents btnStartConnect As Button
    Friend WithEvents TmDataCompare As Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Lbtime2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Lbtime1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents lbSecuill As Label
    Friend WithEvents lbHeaderTxt As Label
    Friend WithEvents pbSecuill As PictureBox
    Friend WithEvents btn_exitprogram As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents panelStatusTxt As Panel
    Friend WithEvents lbHeadStatusTxt As Label
    Friend WithEvents lbTxtProcess As Label
    Friend WithEvents pbStatusConMiddle As PictureBox
    Friend WithEvents lbStatusTxt1 As Label
    Friend WithEvents lbStatusTxt2 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents pbStatusConSecuill As PictureBox
    Friend WithEvents lbStatusConSecuillTxt As Label
    Friend WithEvents btnViewLog As Button
    Friend WithEvents rtbEvent As RichTextBox
    Friend WithEvents lbProcessTime As Label
End Class
