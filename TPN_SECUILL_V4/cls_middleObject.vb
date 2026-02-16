Public Class cls_middleObject

    Private _f_prescriptionno As String = String.Empty
    Private _f_seq As String = String.Empty
    Private _f_seqmax As String = String.Empty
    Private _f_prescriptiondate As String = String.Empty
    Private _f_hn As String = String.Empty
    Private _f_an As String = String.Empty
    Private _f_patientname As String = String.Empty
    Private _f_sex As String = String.Empty
    Private _f_patientepisodedate As String = String.Empty
    Private _f_prioritycode As String = String.Empty
    Private _f_prioritydesc As String = String.Empty
    Private _f_ordertargetdate As String = String.Empty
    Private _f_ordertargettime As String = String.Empty
    Private _f_ordercreatedate As String = String.Empty
    Private _f_ordercreatetime As String = String.Empty
    Private _f_orderitemcode As String = String.Empty
    Private _f_orderitemname As String = String.Empty
    Private _f_orderqty As String = String.Empty
    Private _f_orderunitcode As String = String.Empty
    Private _f_orderunitdesc As String = String.Empty
    Private _f_instructioncode As String = String.Empty
    Private _f_instructiondesc As String = String.Empty
    Private _f_dosage As String = String.Empty
    Private _f_dosageunit As String = String.Empty
    Private _f_frequencycode As String = String.Empty
    Private _f_frequencydesc As String = String.Empty
    Private _f_durationcode As String = String.Empty
    Private _f_durationdesc As String = String.Empty
    Private _f_noteprocessing As String = String.Empty
    Private _f_fromlocationname As String = String.Empty
    Private _f_userorderby As String = String.Empty
    Private _f_useracceptby As String = String.Empty
    Private _f_orderacceptdate As String = String.Empty
    Private _f_orderaccepttime As String = String.Empty
    Private _f_orderacceptfromip As String = String.Empty
    Private _f_patientdob As String = String.Empty
    Private _f_itemlotcode As String = String.Empty
    Private _f_itemlotexpire As String = String.Empty
    Private _f_doctorcode As String = String.Empty
    Private _f_doctorname As String = String.Empty
    Private _f_wardcode As String = String.Empty
    Private _f_warddesc As String = String.Empty
    Private _f_roomcode As String = String.Empty
    Private _f_roomdesc As String = String.Empty
    Private _f_bedcode As String = String.Empty
    Private _f_beddesc As String = String.Empty
    Private _f_pharmacylocationcode As String = String.Empty
    Private _f_pharmacylocationdesc As String = String.Empty
    Private _f_pharmacyitemcode As String = String.Empty
    Private _f_pharmacyitemdesc As String = String.Empty
    Private _f_freetext1 As String = String.Empty
    Private _f_freetext2 As String = String.Empty
    Private _f_itemidentify As String = String.Empty
    Private _f_tomachineno As String = String.Empty
    Private _f_dispensestatus As String = String.Empty
    Private _f_status As String = String.Empty
    Private _f_lastmodified As String = String.Empty
    Private _f_PRN As String = String.Empty
    Private _f_frequencyTime As String = String.Empty
    Private _f_language As String = String.Empty
    Private _f_dosagedispense As String = String.Empty
    Private _f_comment As String = String.Empty
    Private _f_dispensestatus_secuill As String = String.Empty
    Private _f_allergy As String = String.Empty

#Region "Property"
    Public Property F_prescriptionno() As String
        Get
            Return _f_prescriptionno
        End Get
        Set(value As String)
            _f_prescriptionno = value
        End Set
    End Property

    Public Property F_seq() As String
        Get
            Return _f_seq
        End Get
        Set(value As String)
            _f_seq = value
        End Set
    End Property

    Public Property F_seqmax() As String
        Get
            Return _f_seqmax
        End Get
        Set(value As String)
            _f_seqmax = value
        End Set
    End Property

    Public Property F_prescriptiondate() As String
        Get
            Return _f_prescriptiondate
        End Get
        Set(value As String)
            _f_prescriptiondate = value
        End Set
    End Property

    Public Property F_hn() As String
        Get
            Return _f_hn
        End Get
        Set(value As String)
            _f_hn = value
        End Set
    End Property

    Public Property F_an() As String
        Get
            Return _f_an
        End Get
        Set(value As String)
            _f_an = value
        End Set
    End Property

    Public ReadOnly Property F_vn() As String
        Get
            Return ""
        End Get
    End Property

    Public Property F_patientname() As String
        Get
            Return _f_patientname
        End Get
        Set(value As String)
            _f_patientname = value
        End Set
    End Property

    Public Property F_sex() As String
        Get
            Return _f_sex
        End Get
        Set(value As String)
            _f_sex = value
        End Set
    End Property

    Public Property F_patientepisodedate() As String
        Get
            Return _f_patientepisodedate
        End Get
        Set(value As String)
            _f_patientepisodedate = value
        End Set
    End Property

    Public Property F_prioritycode() As String
        Get
            Return _f_prioritycode
        End Get
        Set(value As String)
            _f_prioritycode = value
        End Set
    End Property

    Public Property F_prioritydesc() As String
        Get
            Return _f_prioritydesc
        End Get
        Set(value As String)
            _f_prioritydesc = value
        End Set
    End Property

    Public Property F_ordertargetdate() As String
        Get
            Return _f_ordertargetdate
        End Get
        Set(value As String)
            _f_ordertargetdate = value
        End Set
    End Property

    Public Property F_ordertargettime() As String
        Get
            Return _f_ordertargettime
        End Get
        Set(value As String)
            _f_ordertargettime = value
        End Set
    End Property

    Public Property F_ordercreatedate() As String
        Get
            Return _f_ordercreatedate
        End Get
        Set(value As String)
            _f_ordercreatedate = value
        End Set
    End Property

    Public Property F_ordercreatetime() As String
        Get
            Return _f_ordercreatetime
        End Get
        Set(value As String)
            _f_ordercreatetime = value
        End Set
    End Property

    Public Property F_orderitemcode() As String
        Get
            Return _f_orderitemcode
        End Get
        Set(value As String)
            _f_orderitemcode = value
        End Set
    End Property

    Public Property F_orderitemname() As String
        Get
            Return _f_orderitemname
        End Get
        Set(value As String)
            _f_orderitemname = value
        End Set
    End Property

    Public Property F_orderqty() As String
        Get
            Return _f_orderqty
        End Get
        Set(value As String)
            _f_orderqty = value
        End Set
    End Property

    Public Property F_orderunitcode() As String
        Get
            Return _f_orderunitcode
        End Get
        Set(value As String)
            _f_orderunitcode = value
        End Set
    End Property

    Public Property F_orderunitdesc() As String
        Get
            Return _f_orderunitdesc
        End Get
        Set(value As String)
            _f_orderunitdesc = value
        End Set
    End Property

    Public Property F_instructioncode() As String
        Get
            Return _f_instructioncode
        End Get
        Set(value As String)
            _f_instructioncode = value
        End Set
    End Property

    Public Property F_instructiondesc() As String
        Get
            Return _f_instructiondesc
        End Get
        Set(value As String)
            _f_instructiondesc = value
        End Set
    End Property

    Public Property F_dosage() As String
        Get
            Return _f_dosage
        End Get
        Set(value As String)
            _f_dosage = value
        End Set
    End Property

    Public Property F_dosageunit() As String
        Get
            Return _f_dosageunit
        End Get
        Set(value As String)
            _f_dosageunit = value
        End Set
    End Property

    Public Property F_frequencycode() As String
        Get
            Return _f_frequencycode
        End Get
        Set(value As String)
            _f_frequencycode = value
        End Set
    End Property

    Public Property F_frequencydesc() As String
        Get
            Return _f_frequencydesc
        End Get
        Set(value As String)
            _f_frequencydesc = value
        End Set
    End Property

    Public Property F_durationcode() As String
        Get
            Return _f_durationcode
        End Get
        Set(value As String)
            _f_durationcode = value
        End Set
    End Property

    Public Property F_durationdesc() As String
        Get
            Return _f_durationdesc
        End Get
        Set(value As String)
            _f_durationdesc = value
        End Set
    End Property

    Public Property F_noteprocessing() As String
        Get
            Return _f_noteprocessing
        End Get
        Set(value As String)
            _f_noteprocessing = value
        End Set
    End Property

    Public Property F_fromlocationname() As String
        Get
            Return _f_fromlocationname
        End Get
        Set(value As String)
            _f_fromlocationname = value
        End Set
    End Property

    Public Property F_userorderby() As String
        Get
            Return _f_userorderby
        End Get
        Set(value As String)
            _f_userorderby = value
        End Set
    End Property

    Public Property F_useracceptby() As String
        Get
            Return _f_useracceptby
        End Get
        Set(value As String)
            _f_useracceptby = value
        End Set
    End Property

    Public Property F_orderacceptdate() As String
        Get
            Return _f_orderacceptdate
        End Get
        Set(value As String)
            _f_orderacceptdate = value
        End Set
    End Property

    Public Property F_orderaccepttime() As String
        Get
            Return _f_orderaccepttime
        End Get
        Set(value As String)
            _f_orderaccepttime = value
        End Set
    End Property

    Public Property F_orderacceptfromip() As String
        Get
            Return _f_orderacceptfromip
        End Get
        Set(value As String)
            _f_orderacceptfromip = value
        End Set
    End Property

    Public Property F_patientdob() As String
        Get
            Return _f_patientdob
        End Get
        Set(value As String)
            _f_patientdob = value
        End Set
    End Property

    Public Property F_itemlotcode() As String
        Get
            Return _f_itemlotcode
        End Get
        Set(value As String)
            _f_itemlotcode = value
        End Set
    End Property

    Public Property F_itemlotexpire() As String
        Get
            Return _f_itemlotexpire
        End Get
        Set(value As String)
            _f_itemlotexpire = value
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

    Public Property F_wardcode() As String
        Get
            Return _f_wardcode
        End Get
        Set(value As String)
            _f_wardcode = value
        End Set
    End Property

    Public Property F_warddesc() As String
        Get
            Return _f_warddesc
        End Get
        Set(value As String)
            _f_warddesc = value
        End Set
    End Property

    Public Property F_roomcode() As String
        Get
            Return _f_roomcode
        End Get
        Set(value As String)
            _f_roomcode = value
        End Set
    End Property

    Public Property F_roomdesc() As String
        Get
            Return _f_roomdesc
        End Get
        Set(value As String)
            _f_roomdesc = value
        End Set
    End Property

    Public Property F_bedcode() As String
        Get
            Return _f_bedcode
        End Get
        Set(value As String)
            _f_bedcode = value
        End Set
    End Property

    Public Property F_beddesc() As String
        Get
            Return _f_beddesc
        End Get
        Set(value As String)
            _f_beddesc = value
        End Set
    End Property

    Public Property F_pharmacylocationcode() As String
        Get
            Return _f_pharmacylocationcode
        End Get
        Set(value As String)
            _f_pharmacylocationcode = value
        End Set
    End Property

    Public Property F_pharmacylocationdesc() As String
        Get
            Return _f_pharmacylocationdesc
        End Get
        Set(value As String)
            _f_pharmacylocationdesc = value
        End Set
    End Property

    Public Property F_pharmacyitemcode() As String
        Get
            Return _f_pharmacyitemcode
        End Get
        Set(value As String)
            _f_pharmacyitemcode = value
        End Set
    End Property

    Public Property F_pharmacyitemdesc() As String
        Get
            Return _f_pharmacyitemdesc
        End Get
        Set(value As String)
            _f_pharmacyitemdesc = value
        End Set
    End Property

    Public Property F_freetext1() As String
        Get
            Return _f_freetext1
        End Get
        Set(value As String)
            _f_freetext1 = value
        End Set
    End Property

    Public Property F_freetext2() As String
        Get
            Return _f_freetext2
        End Get
        Set(value As String)
            _f_freetext2 = value
        End Set
    End Property

    Public Property F_itemidentify() As String
        Get
            Return _f_itemidentify
        End Get
        Set(value As String)
            _f_itemidentify = value
        End Set
    End Property

    Public Property F_tomachineno() As String
        Get
            Return _f_tomachineno
        End Get
        Set(value As String)
            _f_tomachineno = value
        End Set
    End Property

    Public Property F_dispensestatus() As String
        Get
            Return _f_dispensestatus
        End Get
        Set(value As String)
            _f_dispensestatus = value
        End Set
    End Property

    Public Property F_status() As String
        Get
            Return _f_status
        End Get
        Set(value As String)
            _f_status = value
        End Set
    End Property

    Public Property F_lastmodified() As String
        Get
            Return _f_lastmodified
        End Get
        Set(value As String)
            _f_lastmodified = value
        End Set
    End Property

    Public Property F_PRN() As String
        Get
            Return _f_PRN
        End Get
        Set(value As String)
            _f_PRN = value
        End Set
    End Property

    Public Property F_frequencyTime() As String
        Get
            Return _f_frequencyTime
        End Get
        Set(value As String)
            _f_frequencyTime = value
        End Set
    End Property

    Public Property F_language() As String
        Get
            Return _f_language
        End Get
        Set(value As String)
            _f_language = value
        End Set
    End Property

    Public Property F_dosagedispense() As String
        Get
            Return _f_dosagedispense
        End Get
        Set(value As String)
            _f_dosagedispense = value
        End Set
    End Property

    Public Property F_comment() As String
        Get
            Return _f_comment
        End Get
        Set(value As String)
            _f_comment = value
        End Set
    End Property

    Public Property F_dispensestatus_secuill() As String
        Get
            Return _f_dispensestatus_secuill
        End Get
        Set(value As String)
            _f_dispensestatus_secuill = value
        End Set
    End Property

    Public Property F_allergy() As String
        Get
            Return _f_allergy
        End Get
        Set(value As String)
            _f_allergy = value
        End Set
    End Property
#End Region

    Public Sub GenObjectMiddle(ByVal dt As DataTable, ByVal rowindex As Integer)
        Try

            If dt.Rows.Count > 0 Then
                _f_prescriptionno = dt.Rows(rowindex).Item("f_prescriptionno").ToString()
                _f_seq = dt.Rows(rowindex).Item("f_seq").ToString()
                _f_seqmax = dt.Rows(rowindex).Item("f_seqmax").ToString()
                _f_prescriptiondate = dt.Rows(rowindex).Item("f_prescriptiondate").ToString()
                _f_hn = dt.Rows(rowindex).Item("f_hn").ToString()
                _f_an = dt.Rows(rowindex).Item("f_an").ToString()
                _f_patientname = dt.Rows(rowindex).Item("f_patientname").ToString()
                _f_sex = dt.Rows(rowindex).Item("f_sex").ToString()
                '_f_patientepisodedate = dt.Rows(rowindex).Item("f_patientepisodedate").ToString()
                _f_prioritycode = dt.Rows(rowindex).Item("f_prioritycode").ToString()
                _f_prioritydesc = dt.Rows(rowindex).Item("f_prioritydesc").ToString()
                _f_ordertargetdate = dt.Rows(rowindex).Item("f_ordertargetdate").ToString()
                _f_ordertargettime = dt.Rows(rowindex).Item("f_ordertargettime").ToString()
                _f_ordercreatedate = dt.Rows(rowindex).Item("f_ordercreatedate").ToString()
                '_f_ordercreatetime = dt.Rows(rowindex).Item("f_ordercreatetime").ToString()
                '_f_ordercreatedate = dt.Rows(rowindex).Item("f_ordercreatedate").ToString()
                _f_orderitemcode = dt.Rows(rowindex).Item("f_orderitemcode").ToString()
                _f_orderitemname = dt.Rows(rowindex).Item("f_orderitemname").ToString()
                _f_orderqty = dt.Rows(rowindex).Item("f_orderqty").ToString()
                _f_orderunitdesc = dt.Rows(rowindex).Item("f_orderunitdesc").ToString()
                _f_orderunitcode = dt.Rows(rowindex).Item("f_orderunitcode").ToString()
                _f_instructioncode = dt.Rows(rowindex).Item("f_instructioncode").ToString()
                _f_instructiondesc = dt.Rows(rowindex).Item("f_instructiondesc").ToString()
                _f_dosage = dt.Rows(rowindex).Item("f_dosage").ToString()
                _f_dosageunit = dt.Rows(rowindex).Item("f_dosageunit").ToString()
                _f_frequencycode = dt.Rows(rowindex).Item("f_frequencycode").ToString()
                _f_frequencydesc = dt.Rows(rowindex).Item("f_frequencydesc").ToString()
                _f_durationcode = dt.Rows(rowindex).Item("f_durationcode").ToString()
                '_f_durationdesc = dt.Rows(rowindex).Item("f_durationdesc").ToString()
                _f_noteprocessing = dt.Rows(rowindex).Item("f_noteprocessing").ToString()
                '_f_fromlocationname = dt.Rows(rowindex).Item("f_fromlocationname").ToString()
                _f_userorderby = dt.Rows(rowindex).Item("f_userorderby").ToString()
                _f_useracceptby = dt.Rows(rowindex).Item("f_useracceptby").ToString()

                Dim accept = dt.Rows(rowindex).Item("f_orderacceptdate").ToString()
                Dim accdt = accept.Split(" ")
                _f_orderacceptdate = accdt(0)
                _f_orderaccepttime = accdt(1)

                _f_orderacceptfromip = dt.Rows(rowindex).Item("f_orderacceptfromip").ToString()
                _f_patientdob = dt.Rows(rowindex).Item("f_patientdob").ToString()
                '_f_itemlotcode = dt.Rows(rowindex).Item("f_itemlotcode").ToString()
                '_f_itemlotexpire = dt.Rows(rowindex).Item("f_itemlotexpire").ToString()
                _f_doctorcode = dt.Rows(rowindex).Item("f_doctorcode").ToString()
                _f_doctorname = dt.Rows(rowindex).Item("f_doctorname").ToString()
                _f_wardcode = dt.Rows(rowindex).Item("f_wardcode").ToString()
                _f_warddesc = dt.Rows(rowindex).Item("f_warddesc").ToString()
                _f_roomcode = dt.Rows(rowindex).Item("f_roomcode").ToString()
                _f_roomdesc = dt.Rows(rowindex).Item("f_roomdesc").ToString()
                _f_bedcode = dt.Rows(rowindex).Item("f_bedcode").ToString()
                _f_beddesc = dt.Rows(rowindex).Item("f_beddesc").ToString()
                _f_pharmacylocationcode = dt.Rows(rowindex).Item("f_pharmacylocationcode").ToString()
                _f_pharmacylocationdesc = dt.Rows(rowindex).Item("f_pharmacylocationdesc").ToString()
                '_f_pharmacyitemcode = dt.Rows(rowindex).Item("f_pharmacyitemcode").ToString()
                '_f_pharmacyitemdesc = dt.Rows(rowindex).Item("f_pharmacyitemdesc").ToString()
                '_f_freetext1 = dt.Rows(rowindex).Item("f_freetext1").ToString()
                '_f_freetext2 = dt.Rows(rowindex).Item("f_freetext2").ToString()
                '_f_itemidentify = dt.Rows(rowindex).Item("f_itemidentify").ToString()
                _f_tomachineno = dt.Rows(rowindex).Item("f_tomachineno").ToString()
                _f_dispensestatus = dt.Rows(rowindex).Item("f_dispensestatus").ToString()
                _f_status = dt.Rows(rowindex).Item("f_status").ToString()
                _f_lastmodified = dt.Rows(rowindex).Item("f_lastmodified").ToString()
                '_f_PRN = dt.Rows(rowindex).Item("f_PRN").ToString()
                _f_frequencyTime = dt.Rows(rowindex).Item("f_frequencyTime").ToString()
                _f_language = dt.Rows(rowindex).Item("f_language").ToString()
                _f_dosagedispense = dt.Rows(rowindex).Item("f_dosagedispense").ToString()
                _f_comment = dt.Rows(rowindex).Item("f_comment").ToString()
                _f_dispensestatus_secuill = dt.Rows(rowindex).Item("f_dispensestatus_secuill").ToString()
                '_f_allergy = dt.Rows(rowindex).Item("f_allergy").ToString()
                _f_doctorcode = dt.Rows(rowindex).Item("f_doctorcode").ToString()
                _f_doctorname = dt.Rows(rowindex).Item("f_doctorname").ToString()
            End If

        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, "cls_middleObject.GenObjectMiddle()"))
        End Try

    End Sub

End Class
