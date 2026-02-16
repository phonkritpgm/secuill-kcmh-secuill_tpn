Public Class SecuillPrescription

    ' ##################################################################
    ' ##################### TABLE DESIGN V5 ############################
    ' pres_runningno	    nvarchar(100)	Unchecked
    ' pres_no	            varchar(20)	    Unchecked
    ' pres_seq	            numeric(2, 0)	Unchecked
    ' pres_date	            varchar(10)	    Checked
    ' pres_barcode	        varchar(20)	    Checked
    ' pat_hn	            varchar(15)	    Checked
    ' pat_an	            varchar(15)	    Checked
    ' pat_name	            varchar(200)	Checked
    ' ward_code	            varchar(20)	    Checked
    ' ward_desc	            varchar(100)	Checked
    ' pat_roomcode	        varchar(20)	    Checked
    ' pat_roomdesc	        varchar(100)	Checked
    ' pat_bedcode	        varchar(20)	    Checked
    ' pat_beddesc	        varchar(100)	Checked
    ' drug_code	            varchar(15)	    Checked
    ' drug_name	            varchar(200)	Checked
    ' pres_orderqty	        decimal(10, 2)	Checked
    ' pres_orderunitcode	varchar(10)	    Checked
    ' pres_orderunitdesc	varchar(20)	    Checked
    ' pres_instructioncode	varchar(10)	    Checked
    ' pres_instructiondesc	varchar(200)	Checked
    ' pres_dosage	        decimal(10, 2)	Checked
    ' pres_dosageunit	    varchar(20)	    Checked
    ' pres_frequencycode	varchar(20)	    Checked
    ' pres_frequencydesc	varchar(200)	Checked
    ' pres_noteprocessing	varchar(500)	Checked
    ' pres_userorderby	    varchar(100)	Checked
    ' pres_useracceptby	    varchar(100)	Checked
    ' pres_ordercreatedate	varchar(10)	    Checked
    ' pres_ordercreatetime	varchar(5)	    Checked
    ' pres_orderacceptdate	varchar(10)	    Checked
    ' pres_orderaccepttime	varchar(5)	    Checked
    ' pres_fromlocationcode	varchar(20)	    Checked
    ' pres_fromlocationdesc	varchar(200)	Checked
    ' pres_status	        char(1)	        Checked
    ' pres_statusdesc	    varchar(200)	Checked
    ' pres_dispensedstatus	char(1)	        Checked
    ' pres_dispenseddesc	varchar(200)	Checked
    ' pick_orderqty	        decimal(10, 2)	Checked
    ' pick_ordertime	    datetime	    Checked
    ' lastmodified	        datetime	    Checked
    ' ##################################################################

#Region "Valiable"
    Private _pres_runningno As String = String.Empty
    Private _pres_no As String = String.Empty
    Private _pres_seq As String = String.Empty
    Private _pres_date As String = String.Empty
    Private _pres_barcode As String = String.Empty
    Private _pat_hn As String = String.Empty
    Private _pat_an As String = String.Empty
    Private _pat_name As String = String.Empty
    Private _ward_code As String = String.Empty
    Private _ward_desc As String = String.Empty
    Private _pat_roomcode As String = String.Empty
    Private _pat_roomdesc As String = String.Empty
    Private _pat_bedcode As String = String.Empty
    Private _pat_beddesc As String = String.Empty
    Private _drug_code As String = String.Empty
    Private _drug_name As String = String.Empty
    Private _pres_orderqty As String = String.Empty
    Private _pres_orderunitcode As String = String.Empty
    Private _pres_orderunitdesc As String = String.Empty
    Private _pres_instructioncode As String = String.Empty
    Private _pres_instructiondesc As String = String.Empty
    Private _pres_dosage As String = String.Empty
    Private _pres_dosageunit As String = String.Empty
    Private _pres_frequencycode As String = String.Empty
    Private _pres_frequencydesc As String = String.Empty
    Private _pres_noteprocessing As String = String.Empty
    Private _pres_userorderby As String = String.Empty
    Private _pres_useracceptby As String = String.Empty
    Private _pres_ordercreatedate As String = String.Empty
    Private _pres_ordercreatetime As String = String.Empty
    Private _pres_orderacceptdate As String = String.Empty
    Private _pres_orderaccepttime As String = String.Empty
    Private _pres_fromlocationcode As String = String.Empty
    Private _pres_fromlocationdesc As String = String.Empty
    Private _pres_crititallevel As String = String.Empty
    Private _pres_status As String = String.Empty
    Private _pres_statusdesc As String = String.Empty
    Private _pres_dispensedstatus As String = String.Empty
    Private _pres_dispenseddesc As String = String.Empty
    Private _pick_orderqty As String = String.Empty
    Private _pick_ordertime As String = String.Empty
    Private _lastmodified As String = String.Empty
    Private _allergy As String = String.Empty
    Private _f_doctorcode As String = String.Empty
    Private _f_doctorname As String = String.Empty
#End Region

#Region "Property"
    Public Property F_pres_runningno() As String
        Get
            Return Me._pres_runningno
        End Get
        Set(value As String)
            Me._pres_runningno = value
        End Set
    End Property

    Public Property F_pres_no() As String
        Get
            Return Me._pres_no
        End Get
        Set(value As String)
            Me._pres_no = value
        End Set
    End Property

    Public Property F_pres_seq() As String
        Get
            Return Me._pres_seq
        End Get
        Set(value As String)
            Me._pres_seq = value
        End Set
    End Property

    Public Property F_pres_date() As String
        Get
            Return Me._pres_date
        End Get
        Set(value As String)
            Me._pres_date = value
        End Set
    End Property

    Public Property F_pres_barcode() As String
        Get
            Return Me._pres_barcode
        End Get
        Set(value As String)
            Me._pres_barcode = value
        End Set
    End Property

    Public Property F_pat_hn() As String
        Get
            Return Me._pat_hn
        End Get
        Set(value As String)
            Me._pat_hn = value
        End Set
    End Property

    Public Property F_pat_an() As String
        Get
            Return Me._pat_an
        End Get
        Set(value As String)
            Me._pat_an = value
        End Set
    End Property

    Public Property F_pat_name() As String
        Get
            Return Me._pat_name
        End Get
        Set(value As String)
            Me._pat_name = value
        End Set
    End Property

    Public Property F_ward_code() As String
        Get
            Return Me._ward_code
        End Get
        Set(value As String)
            Me._ward_code = value
        End Set
    End Property

    Public Property F_ward_desc() As String
        Get
            Return Me._ward_desc
        End Get
        Set(value As String)
            Me._ward_desc = value
        End Set
    End Property

    Public Property F_pat_roomcode() As String
        Get
            Return Me._pat_roomcode
        End Get
        Set(value As String)
            Me._pat_roomcode = value
        End Set
    End Property

    Public Property F_pat_roomdesc() As String
        Get
            Return Me._pat_roomdesc
        End Get
        Set(value As String)
            Me._pat_roomdesc = value
        End Set
    End Property

    Public Property F_pat_bedcode() As String
        Get
            Return Me._pat_bedcode
        End Get
        Set(value As String)
            Me._pat_bedcode = value
        End Set
    End Property

    Public Property F_pat_beddesc() As String
        Get
            Return Me._pat_beddesc
        End Get
        Set(value As String)
            Me._pat_beddesc = value
        End Set
    End Property

    Public Property F_drug_code() As String
        Get
            Return Me._drug_code
        End Get
        Set(value As String)
            Me._drug_code = value
        End Set
    End Property

    Public Property F_drug_name() As String
        Get
            Return Me._drug_name
        End Get
        Set(value As String)
            Me._drug_name = value
        End Set
    End Property

    Public Property F_pres_orderqty() As String
        Get
            Return Me._pres_orderqty
        End Get
        Set(value As String)
            Me._pres_orderqty = value
        End Set
    End Property

    Public Property F_pres_orderunitcode() As String
        Get
            Return Me._pres_orderunitcode
        End Get
        Set(value As String)
            Me._pres_orderunitcode = value
        End Set
    End Property

    Public Property F_pres_orderunitdesc() As String
        Get
            Return Me._pres_orderunitdesc
        End Get
        Set(value As String)
            Me._pres_orderunitdesc = value
        End Set
    End Property

    Public Property F_pres_instructioncode() As String
        Get
            Return Me._pres_instructioncode
        End Get
        Set(value As String)
            Me._pres_instructioncode = value
        End Set
    End Property

    Public Property F_pres_instructiondesc() As String
        Get
            Return Me._pres_instructiondesc
        End Get
        Set(value As String)
            Me._pres_instructiondesc = value
        End Set
    End Property

    Public Property F_pres_dosage() As String
        Get
            Return Me._pres_dosage
        End Get
        Set(value As String)
            Me._pres_dosage = value
        End Set
    End Property

    Public Property F_pres_dosageunit() As String
        Get
            Return Me._pres_dosageunit
        End Get
        Set(value As String)
            Me._pres_dosageunit = value
        End Set
    End Property

    Public Property F_pres_frequencycode() As String
        Get
            Return Me._pres_frequencycode
        End Get
        Set(value As String)
            Me._pres_frequencycode = value
        End Set
    End Property

    Public Property F_pres_frequencydesc() As String
        Get
            Return Me._pres_frequencydesc
        End Get
        Set(value As String)
            Me._pres_frequencydesc = value
        End Set
    End Property

    Public Property F_pres_noteprocessing() As String
        Get
            Return Me._pres_noteprocessing
        End Get
        Set(value As String)
            Me._pres_noteprocessing = value
        End Set
    End Property

    Public Property F_pres_userorderby() As String
        Get
            Return Me._pres_userorderby
        End Get
        Set(value As String)
            Me._pres_userorderby = value
        End Set
    End Property

    Public Property F_pres_useracceptby() As String
        Get
            Return Me._pres_useracceptby
        End Get
        Set(value As String)
            Me._pres_useracceptby = value
        End Set
    End Property

    Public Property F_pres_ordercreatedate() As String
        Get
            Return Me._pres_ordercreatedate
        End Get
        Set(value As String)
            Me._pres_ordercreatedate = value
        End Set
    End Property

    Public Property F_pres_ordercreatetime() As String
        Get
            Return Me._pres_ordercreatetime
        End Get
        Set(value As String)
            Me._pres_ordercreatetime = value
        End Set
    End Property

    Public Property F_pres_orderacceptdate() As String
        Get
            Return Me._pres_orderacceptdate
        End Get
        Set(value As String)
            Me._pres_orderacceptdate = value
        End Set
    End Property

    Public Property F_pres_orderaccepttime() As String
        Get
            Return Me._pres_orderaccepttime
        End Get
        Set(value As String)
            Me._pres_orderaccepttime = value
        End Set
    End Property

    Public Property F_pres_fromlocationcode() As String
        Get
            Return Me._pres_fromlocationcode
        End Get
        Set(value As String)
            Me._pres_fromlocationcode = value
        End Set
    End Property

    Public Property F_pres_fromlocationdesc() As String
        Get
            Return Me._pres_fromlocationdesc
        End Get
        Set(value As String)
            Me._pres_fromlocationdesc = value
        End Set
    End Property

    Public Property F_pres_crititallevel() As String
        Get
            Return Me._pres_crititallevel
        End Get
        Set(value As String)
            Me._pres_crititallevel = value
        End Set
    End Property

    Public Property F_pres_status() As String
        Get
            Return Me._pres_status
        End Get
        Set(value As String)
            Me._pres_status = value
        End Set
    End Property

    Public Property F_pres_statusdesc() As String
        Get
            Return Me._pres_statusdesc
        End Get
        Set(value As String)
            Me._pres_statusdesc = value
        End Set
    End Property

    Public Property F_pres_dispensedstatus() As String
        Get
            Return Me._pres_dispensedstatus
        End Get
        Set(value As String)
            Me._pres_dispensedstatus = value
        End Set
    End Property

    Public Property F_pres_dispenseddesc() As String
        Get
            Return Me._pres_dispenseddesc
        End Get
        Set(value As String)
            Me._pres_dispenseddesc = value
        End Set
    End Property

    Public Property F_pick_orderqty() As String
        Get
            Return Me._pick_orderqty
        End Get
        Set(value As String)
            Me._pick_orderqty = value
        End Set
    End Property

    Public Property F_pick_ordertime() As String
        Get
            Return Me._pick_ordertime
        End Get
        Set(value As String)
            Me._pick_ordertime = value
        End Set
    End Property

    Public Property F_lastmodified() As String
        Get
            Return Me._lastmodified
        End Get
        Set(value As String)
            Me._lastmodified = value
        End Set
    End Property

    Public Property F_allergy() As String
        Get
            Return Me._allergy
        End Get
        Set(value As String)
            Me._allergy = value
        End Set
    End Property

    Public Property F_doctorcode() As String
        Get
            Return _f_doctorcode
        End Get
        Set(value As String)
            _f_doctorcode = value
        End Set
    End Property

    Public Property F_doctorname() As String
        Get
            Return _f_doctorname
        End Get
        Set(value As String)
            _f_doctorname = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Dim SQL As String = String.Empty

        SQL = " SET XACT_ABORT ON"
        SQL &= " BEGIN TRANSACTION"

        SQL &= " INSERT INTO T_Prescription("
        'SQL &= "  pres_runningno "
        SQL &= " pres_no"
        SQL &= " , pres_seq"
        SQL &= " , pres_date"
        SQL &= " , pres_barcode"
        SQL &= " , pat_hn"

        ' --------------- New version V5 ---------------
        SQL &= " , pat_an"
        SQL &= " , pat_name"
        SQL &= " , ward_code"
        SQL &= " , ward_desc"
        SQL &= " , pat_roomcode"
        SQL &= " , pat_roomdesc"
        SQL &= " , pat_bedcode"
        SQL &= " , pat_beddesc"
        ' --------------- New version V5 ---------------

        SQL &= " , drug_code"

        ' --------------- New version V5 ---------------
        SQL &= " , drug_name"
        ' --------------- New version V5 ---------------

        SQL &= " , pres_orderqty"
        SQL &= " , pres_orderunitcode"
        SQL &= " , pres_orderunitdesc"
        SQL &= " , pres_instructioncode"
        SQL &= " , pres_instructiondesc"
        SQL &= " , pres_dosage"
        SQL &= " , pres_dosageunit"
        SQL &= " , pres_frequencycode "
        SQL &= " , pres_frequencydesc"

        ' --------------- New version V5 ---------------
        SQL &= " , pres_noteprocessing"
        ' --------------- New version V5 ---------------

        SQL &= " , pres_userorderby"
        SQL &= " , pres_useracceptby"
        SQL &= " , pres_ordercreatedate"
        SQL &= " , pres_ordercreatetime"
        SQL &= " , pres_orderacceptdate"
        SQL &= " , pres_orderaccepttime"

        ' --------------- New version V5 ---------------
        SQL &= " , pres_fromlocationcode"
        SQL &= " , pres_fromlocationdesc"

        SQL &= " , pres_crititallevel"
        ' --------------- New version V5 ---------------

        SQL &= " , pres_status"
        SQL &= " , pres_statusdesc"
        SQL &= " , pres_dispensedstatus"
        SQL &= " , pres_dispenseddesc"

        ' --------------- New version V5 ---------------
        SQL &= " , pick_orderqty"
        SQL &= " , pick_ordertime"
        ' --------------- New version V5 ---------------

        SQL &= " , lastmodified"

        SQL &= " , f_doctorcode"
        SQL &= " , f_doctorname"

        SQL &= " )"
        SQL &= " VALUES("
        'SQL &= "   '" & Me._pres_runningno & "'"
        SQL &= "  '" & Me._pres_no & "'"
        SQL &= " , " & Me._pres_seq & ""
        SQL &= " , " & IIf((Me._pres_date = ""), "NULL", "'" & Me._pres_date & "'")
        SQL &= " , " & IIf((Me._pres_no = ""), "NULL", "'" & Me._pres_no & "'")   ' barcode
        SQL &= " , " & IIf((Me._pat_hn = ""), "NULL", "'" & Me._pat_hn & "'")

        ' --------------- New version V5 ---------------
        SQL &= " , " & IIf((Me._pat_an = ""), "NULL", "'" & Me._pat_an & "'")
        SQL &= " , " & IIf((Me._pat_name = ""), "NULL", "'" & Me._pat_name & "'")
        SQL &= " , " & IIf((Me._ward_code = ""), "NULL", "'" & Me._ward_code & "'")
        SQL &= " , " & IIf((Me._ward_desc = ""), "NULL", "'" & Me._ward_desc & "'")
        SQL &= " , " & IIf((Me._pat_roomcode = ""), "NULL", "'" & Me._pat_roomcode & "'")
        SQL &= " , " & IIf((Me._pat_roomdesc = ""), "NULL", "'" & Me._pat_roomdesc & "'")
        SQL &= " , " & IIf((Me._pat_bedcode = ""), "NULL", "'" & Me._pat_bedcode & "'")
        SQL &= " , " & IIf((Me._pat_beddesc = ""), "NULL", "'" & Me._pat_beddesc & "'")
        ' --------------- New version V5 ---------------

        SQL &= " ," & IIf((Me._drug_code = ""), "NULL", "'" & Me._drug_code & "'")

        ' --------------- New version V5 ---------------
        SQL &= " , " & IIf((Me._drug_name = ""), "NULL", "'" & Me._drug_name & "'")
        ' --------------- New version V5 ---------------

        SQL &= " , " & IIf((Me._pres_orderqty = ""), "0", "" & Me._pres_orderqty & "") 'DECIMAL
        SQL &= " , " & IIf((Me._pres_orderunitcode = ""), "NULL", "'" & Me._pres_orderunitcode & "'")
        SQL &= " , " & IIf((Me._pres_orderunitdesc = ""), "NULL", "'" & Me._pres_orderunitdesc & "'")
        SQL &= " , " & IIf((Me._pres_instructioncode = ""), "NULL", "'" & Me._pres_instructioncode & "'")
        SQL &= " , " & IIf((Me._pres_instructiondesc = ""), "NULL", "'" & Me._pres_instructiondesc & "'")
        SQL &= " , " & IIf((Me._pres_dosage = ""), "NULL", "'" & Me._pres_dosage & "'")
        SQL &= " , " & IIf((Me._pres_dosageunit = ""), "NULL", "'" & Me._pres_dosageunit & "'")
        SQL &= " , " & IIf((Me._pres_frequencycode = ""), "NULL", "'" & Me._pres_frequencycode & "'")
        SQL &= " , " & IIf((Me._pres_frequencydesc = ""), "NULL", "'" & Me._pres_frequencydesc & "'")

        ' --------------- New version V5 ---------------
        Dim tmpNoteProcessing = _pres_noteprocessing
        If tmpNoteProcessing.IndexOf("คำแนะนำ") > 0 Then
            tmpNoteProcessing = _pres_noteprocessing.Substring(1, tmpNoteProcessing.IndexOf("คำแนะนำ") - 1)
        End If
        If tmpNoteProcessing.Length >= 500 Then
            tmpNoteProcessing.Substring(1, 500)
        End If
        SQL &= " , " & IIf((tmpNoteProcessing = ""), "NULL", "'" & tmpNoteProcessing & "'")
        ' --------------- New version V5 ---------------

        SQL &= " , " & IIf((Me._pres_userorderby = ""), "NULL", "'" & Me._pres_userorderby & "'")
        SQL &= " , " & IIf((Me._pres_useracceptby = ""), "NULL", "'" & Me._pres_useracceptby & "'")

        Dim createdate = ""
        Dim createtime = ""
        If Me._pres_ordercreatedate <> "" Then
            Dim cadt = Me._pres_ordercreatedate.Split(" ")
            createdate = cadt(0)
            createtime = cadt(1)
        End If

        SQL &= " , " & IIf((createdate = ""), "NULL", "'" & createdate & "'")
        SQL &= " , " & IIf((createtime = ""), "NULL", "'" & Mid(createtime, 1, 5) & "'")

        SQL &= " , " & IIf((Me._pres_orderacceptdate = ""), "NULL", "'" & Me._pres_orderacceptdate & "'")
        SQL &= " , " & IIf((Me._pres_orderaccepttime = ""), "NULL", "'" & Mid(Me._pres_orderaccepttime, 1, 5) & "'")

        ' --------------- New version V5 ---------------
        SQL &= " , " & IIf((Me._pres_fromlocationcode = ""), "NULL", "'" & Me._pres_fromlocationcode & "'")
        SQL &= " , " & IIf((Me._pres_fromlocationdesc = ""), "NULL", "'" & Me._pres_fromlocationdesc & "'")

        SQL &= " , " & IIf((Me._pres_crititallevel = ""), "NULL", "'" & Me._pres_crititallevel & "'")
        ' --------------- New version V5 ---------------

        SQL &= " , " & IIf((Me._pres_status = ""), "A", "'" & Me._pres_status & "'")
        SQL &= " , " & IIf((Me._pres_statusdesc = ""), "Active", "'" & Me._pres_statusdesc & "'")
        SQL &= " , " & IIf((Me._pres_dispensedstatus = ""), "Active", "'" & Me._pres_dispensedstatus & "'")
        SQL &= " , " & IIf((Me._pres_dispenseddesc = ""), "Wait for dispensing", "'" & Me._pres_dispenseddesc & "'")
        SQL &= " , " & IIf((Me._pick_orderqty = ""), "0", "'" & Me._pick_orderqty & "'")
        SQL &= " , " & IIf((Me._pick_ordertime = ""), "NULL", "'" & Me._pick_ordertime & "'")
        SQL &= " , GETDATE()"

        SQL &= " , " & IIf(_f_doctorcode = "", "NULL", "'" & _f_doctorcode & "'")
        SQL &= " , " & IIf(_f_doctorname = "", "NULL", "'" & _f_doctorname & "'")
        SQL &= " )"

        SQL &= " COMMIT TRANSACTION"
        SQL &= " SET XACT_ABORT OFF"

        Try
            Using cmd As New SqlClient.SqlCommand(SQL)
                cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.InsertMiddleToSecuill())")
            End Using

            Return True
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.InsertMiddleToSecuill())"))

            Return False
        End Try

    End Function

End Class
