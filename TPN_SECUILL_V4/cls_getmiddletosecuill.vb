Imports System.Runtime.Remoting.Messaging

Public Class cls_getmiddletosecuill

    Public Shared Sub RunProcess()
        Dim countdata As Integer = cls_getmiddletosecuill.CountDataMiddle()
        If countdata > 0 Then
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "EVENT", vbTab, "Count prescription: " & countdata, vbTab, "cls_getmiddletosecuill.GetDataMidle()"))
            Dim dtmiddle As DataTable = cls_getmiddletosecuill.GetDataMidle()

            If Not dtmiddle Is Nothing Then
                For i1 As Integer = 0 To dtmiddle.Rows.Count - 1
                    Dim middleObj As cls_middleObject = New cls_middleObject()
                    middleObj.GenObjectMiddle(dtmiddle, i1)

                    cls_getmiddletosecuill.GetWardToMaster(middleObj.F_wardcode,
                                                               middleObj.F_warddesc)

                        cls_getmiddletosecuill.GetBedToMaster(middleObj.F_bedcode,
                                                          middleObj.F_beddesc,
                                                          middleObj.F_roomcode)

                        cls_getmiddletosecuill.GetRoomToMaster(middleObj.F_roomcode,
                                                           middleObj.F_roomdesc,
                                                           middleObj.F_wardcode)

                        cls_getmiddletosecuill.GetDrugMaster(middleObj.F_orderitemcode,
                                                          middleObj.F_orderunitcode,
                                                          middleObj.F_orderunitdesc,
                                                          middleObj.F_orderitemname)

                        cls_getmiddletosecuill.GetDrugAllergyToMaster(middleObj.F_hn,
                                                                  middleObj.F_orderitemcode,
                                                                  middleObj.F_allergy) ' middleObj.F_drugallergy

                        cls_getmiddletosecuill.GetPatientInfo(middleObj.F_hn,
                                                          middleObj.F_an,
                                                          middleObj.F_vn,
                                                          "", ' middleObj.F_title
                                                          middleObj.F_patientname,
                                                          middleObj.F_sex,
                                                          "", ' middleObj.F_idcard
                                                          middleObj.F_patientdob,
                                                          "", ' middleObj.F_blood
                                                          "", ' middleObj.F_congenital_disease
                                                          "", ' middleObj.F_diagnosis
                                                          "", ' middleObj.F_patient_rights
                                                          middleObj.F_wardcode,
                                                          middleObj.F_bedcode,
                                                          middleObj.F_roomcode,
                                                          "", ' middleObj.F_admitteddate
                                                          "", ' middleObj.F_dischargeddate
                                                          middleObj.F_doctorcode,
                                                          middleObj.F_doctorname,
                                                          "", ' middleObj.F_image
                                                          "") 'middleObj.F_onlinestatus

                        Dim status As String = "A"
                        Dim statusdesc As String = "ACTIVE"
                        Dim presc_status As String = "W"

                        If middleObj.F_status = 2 Then

                            status = "C"
                            statusdesc = "CANCLE | ยกเลิก"

                            cls_getmiddletosecuill.UpdateStatusPresc(middleObj.F_prescriptionno,
                                                                  middleObj.F_seq,
                                                                status,
                                                                statusdesc)
                        End If

                        Dim ObjSecuill As New SecuillPrescription

                        ObjSecuill.F_pres_runningno = "Identity Increment"
                        ObjSecuill.F_pres_no = middleObj.F_prescriptionno
                        ObjSecuill.F_pres_seq = middleObj.F_seq
                        ObjSecuill.F_pres_date = middleObj.F_prescriptiondate
                        ObjSecuill.F_pres_barcode = middleObj.F_prescriptionno
                        ObjSecuill.F_pat_hn = middleObj.F_hn
                        ObjSecuill.F_pat_an = middleObj.F_an
                        ObjSecuill.F_pat_name = middleObj.F_patientname
                        ObjSecuill.F_ward_code = middleObj.F_wardcode
                        ObjSecuill.F_ward_desc = middleObj.F_warddesc
                        ObjSecuill.F_pat_roomcode = middleObj.F_roomcode
                        ObjSecuill.F_pat_roomdesc = middleObj.F_roomdesc
                        ObjSecuill.F_pat_bedcode = middleObj.F_bedcode
                        ObjSecuill.F_pat_beddesc = middleObj.F_beddesc
                        ObjSecuill.F_drug_code = middleObj.F_orderitemcode
                        ObjSecuill.F_drug_name = middleObj.F_orderitemname

                        If CDec(middleObj.F_orderqty) <= 0 Then
                            cls_getmiddletosecuill.UpdateStatusMiddle(middleObj.F_prescriptiondate,
                                          middleObj.F_prescriptionno,
                                          middleObj.F_seq,
                                          middleObj.F_hn,
                                          middleObj.F_orderitemcode,
                                          2)
                            Return
                        End If

                        Dim func = Function(ByVal drug_code As String, ByVal qty As String, ByVal unitcode As String) As Object
                                       Dim resp As Object = {qty, unitcode}
                                       Dim SQL As String = ""
                                       SQL = " SELECT unitqty, convto, convtounitcode FROM M_ConvertDrugUnit WHERE drug_code = '" & drug_code & "'"
                                       Dim dsSeq = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.CheckDrugConvertQtyPackage())")
                                       Try
                                           If dsSeq.Tables(0).Rows.Count > 0 Then
                                               Dim unitqty = CInt(dsSeq.Tables(0).Rows(0).Item("unitqty").ToString)
                                               Dim conv = CInt(dsSeq.Tables(0).Rows(0).Item("convto").ToString)
                                               Dim convtounitcode = dsSeq.Tables(0).Rows(0).Item("convtounitcode").ToString
                                               If CInt(qty) Mod unitqty <> 0 Then
                                                   Return resp
                                               End If

                                               resp(0) = (CInt(qty) / CInt(unitqty)) * conv
                                               resp(1) = convtounitcode
                                               Return resp

                                           Else
                                               Return resp
                                           End If
                                       Catch ex As Exception
                                       End Try
                                       Return resp
                                   End Function

                        Dim resultConvQty As Object = func(middleObj.F_orderitemcode, middleObj.F_orderqty, middleObj.F_orderunitcode)
                        Dim resultQty = resultConvQty(0)
                        Dim resultUnitCode = resultConvQty(1)

                        If resultQty < 0 Then
                            cls_getmiddletosecuill.UpdateStatusMiddle(middleObj.F_prescriptiondate,
                                          middleObj.F_prescriptionno,
                                          middleObj.F_seq,
                                          middleObj.F_hn,
                                          middleObj.F_orderitemcode,
                                          3)
                            Return
                        End If

                        ObjSecuill.F_pres_orderqty = resultQty

                        ObjSecuill.F_pres_orderunitcode = resultUnitCode
                        ObjSecuill.F_pres_orderunitdesc = middleObj.F_orderunitdesc
                        ObjSecuill.F_pres_instructioncode = middleObj.F_instructioncode
                        ObjSecuill.F_pres_instructiondesc = middleObj.F_instructiondesc
                        ObjSecuill.F_pres_dosage = middleObj.F_dosage
                        ObjSecuill.F_pres_dosageunit = middleObj.F_dosageunit
                        ObjSecuill.F_pres_frequencycode = middleObj.F_frequencycode
                        ObjSecuill.F_pres_frequencydesc = middleObj.F_frequencydesc
                        ObjSecuill.F_pres_noteprocessing = middleObj.F_noteprocessing
                        ObjSecuill.F_pres_userorderby = middleObj.F_userorderby
                        ObjSecuill.F_pres_useracceptby = middleObj.F_useracceptby
                        ObjSecuill.F_pres_ordercreatedate = middleObj.F_ordercreatedate
                        ObjSecuill.F_pres_ordercreatetime = middleObj.F_ordercreatetime
                        ObjSecuill.F_pres_orderacceptdate = middleObj.F_orderacceptdate
                        ObjSecuill.F_pres_orderaccepttime = IIf(middleObj.F_orderaccepttime.Trim = "", Date.Now().ToString("HH:mm:ss"), middleObj.F_orderaccepttime)
                        ObjSecuill.F_pres_fromlocationcode = middleObj.F_pharmacylocationcode
                        ObjSecuill.F_pres_fromlocationdesc = middleObj.F_pharmacylocationdesc
                        ObjSecuill.F_pres_crititallevel = middleObj.F_freetext2
                        ObjSecuill.F_pres_status = status
                        ObjSecuill.F_pres_statusdesc = statusdesc
                        ObjSecuill.F_pres_dispensedstatus = presc_status
                        ObjSecuill.F_pres_dispenseddesc = "Wait for dispensing"
                        ObjSecuill.F_pick_orderqty = ""
                        ObjSecuill.F_pick_ordertime = ""
                        ObjSecuill.F_lastmodified = "GETDATE()"
                        ObjSecuill.F_doctorcode = middleObj.F_doctorcode
                        ObjSecuill.F_doctorname = middleObj.F_doctorname

                        Dim resultInsertSecuill As Boolean = ObjSecuill.Insert()
                        If resultInsertSecuill = True Then
                            cls_getmiddletosecuill.UpdateStatusMiddle(middleObj.F_prescriptiondate,
                                          middleObj.F_prescriptionno,
                                          middleObj.F_seq,
                                          middleObj.F_hn,
                                          middleObj.F_orderitemcode,
                                          1)
                        End If
                        System.Threading.Thread.Sleep(100)


                    'If middleObj.F_status = 0 Then
                    '    cls_getmiddletosecuill.GetFreeDispensing(middleObj.F_prescriptionno, middleObj.F_seq, middleObj.F_hn, middleObj.F_orderitemcode, middleObj.F_orderqty)
                    'End If


                    ' Siriraj Only
                    'Dim chkdrugmaster As Integer = cls_getmiddletosecuill.CheckDrugMasterSecuill(middleObj.F_orderitemcode)
                    'If chkdrugmaster <= 0 Then

                    '    '' Update urgergent middle
                    '    Dim returnnot As String = middleObj.F_noteprocessing.Replace("*f", "")
                    '    cls_getmiddletosecuill.UpdateUrgentStatusMiddle(middleObj.F_prescriptionno,
                    '                                                    middleObj.F_seq,
                    '                                                    middleObj.F_prescriptiondate,
                    '                                                    middleObj.F_orderitemcode,
                    '                                                    middleObj.F_hn,
                    '                                                    middleObj.F_an,
                    '                                                    middleObj.F_runningno,
                    '                                                    returnnot)

                    'End If

                Next

            End If

        End If

    End Sub

    Public Shared Function GendAllDrugMasterSecuill() As String
        Dim resp As String = 0
        Dim SQL As String = ""

        SQL = "SELECT SUBSTRING((SELECT ',''' + drug_code + '''' FROM M_Drug FOR XML PATH ('')), 2, LEN((SELECT ',''' + drug_code + '''' FROM M_Drug FOR XML PATH ('')))) AS alldrug"

        Dim dsSeq = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GendAllDrugMasterSecuill())")
        resp = dsSeq.Tables(0).Rows(0).Item("alldrug").ToString

        Return resp
    End Function

    Public Shared Function CheckDrugMasterSecuill(ByVal drugcode As String) As Integer
        Dim resp As Integer = 0
        Dim SQL As String = ""

        SQL = " SELECT COUNT(*) AS countdrug FROM M_Drug WHERE drug_code = '" & drugcode & "' AND drug_status = 1"

        Dim dsSeq = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.CheckDrugMasterSecuill())")
        Try
            resp = CInt(dsSeq.Tables(0).Rows(0).Item("countdrug").ToString)
        Catch ex As Exception

        End Try

        Return resp
    End Function

    Public Shared Function CountDataMiddle() As Integer
        Dim resp As Integer = 0
        Dim SQL As String = String.Empty
        Dim today As String = Date.Now().ToString("yyyyMMdd")
        Dim yesterday As String = DateAdd(DateInterval.Day, -1, Date.Now()).ToString("yyyyMMdd")

        SQL = " SELECT COUNT(*) AS count"
        SQL &= " FROM tb_thaneshosp_middle"
        SQL &= " WHERE f_tomachineno  = '" & md.Tomachineno & "'"
        'SQL &= " AND f_wardcode IN( " & md.WardCode & " )"
        SQL &= " AND f_dispensestatus_secuill = 0"
        'SQL &= " AND f_orderitemcode IN (" & cls_getmiddletosecuill.GendAllDrugMasterSecuill() & ")"
        SQL &= " AND f_prescriptiondate IN ('" & today & "', '" & yesterday & "')"

        Try
            Dim ds As DataSet = cls_condbmiddle.Fill(SQL, 0, Nothing, "cls_getmiddletosecuill.CountDataMiddle()")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    resp = ds.Tables(0).Rows(0).Item("count")
                End If
            End If
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, "cls_getmiddletosecuill.CountDataMiddle()"))
        End Try

        Return resp
    End Function

    ''' <summary>
    ''' SELECT * FROM middle
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetDataMidle() As DataTable
        Dim dt As DataTable = Nothing

        Dim SQL As String = String.Empty
        Dim today As String = Date.Now().ToString("yyyyMMdd")
        Dim yesterday As String = DateAdd(DateInterval.Day, -1, Date.Now()).ToString("yyyyMMdd")

        SQL = " SELECT *"
        SQL &= " FROM tb_thaneshosp_middle"
        SQL &= " WHERE f_tomachineno  = '" & md.Tomachineno & "'"
        'SQL &= " AND f_wardcode IN( " & md.WardCode & " )"
        SQL &= " AND f_dispensestatus_secuill = 0"
        SQL &= " AND f_prescriptiondate IN ('" & today & "', '" & yesterday & "')"

        Try
            Dim ds As DataSet = cls_condbmiddle.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetDataMidle())")
            If Not ds Is Nothing Then
                dt = ds.Tables(0)
            End If
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetDataMidle())"))
        End Try

        Return dt
    End Function

    Public Shared Function GetDataDrugInterAction(ByVal runningno As String) As DataTable
        Dim dt As DataTable = Nothing

        Dim SQL As String = String.Empty


        SQL = " SELECT  * "
        SQL &= " FROM M_DrugInterActionHeader"
        SQL &= " LEFT JOIN M_DrugInteraction"
        SQL &= " ON M_DrugInterActionHeader.interaction_no = M_DrugInteraction.interaction_no"
        SQL &= " WHERE pres_runningno = '" & runningno & "'"

        Try
            Dim ds As DataSet = cls_condbmiddle.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetDataDrugInterAction())")
            If Not ds Is Nothing Then
                dt = ds.Tables(0)
            End If
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetDataDrugInterAction())"))
        End Try

        Return dt
    End Function

    Public Shared Function GetDataSecuillPrescription(ByVal pres_no As String) As DataSet
        Dim dt As DataSet = Nothing

        Dim SQL As String = String.Empty

        SQL = " SELECT TOP 1 *"
        SQL &= " FROM T_Prescription"
        SQL &= " WHERE pres_dispensedstatus = '1'"
        SQL &= " AND pres_no ='" & pres_no & "'"
        SQL &= " AND pres_date = '" & Date.Now().ToString("yyyyMMdd") & "'"
        SQL &= " ORDER BY lastmodified DESC"

        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetDataPrescription())")
            If Not ds Is Nothing Then
                dt = ds
            End If
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetDataPrescription())"))
        End Try
        Return dt
    End Function

    Public Shared Function GetDataMiddleStatusByPrescription(ByVal presno As String) As DataTable
        Dim dt As DataTable = Nothing

        Dim SQL As String = String.Empty

        SQL = " SELECT  *"
        SQL &= " FROM tb_thaneshosp_middle"
        SQL &= " WHERE f_tomachineno = '" & md.Tomachineno & "'"
        SQL &= " AND f_wardcode IN( " & md.WardCode & " )"
        SQL &= " AND f_prescriptiondate = '" & presno & "'"
        SQL &= " AND f_dispensestatus_secuill = 0"
        SQL &= " AND f_prescriptiondate = '" & Date.Now().ToString("yyyyMMdd") & "'"

        Try
            Dim ds As DataSet = cls_condbmiddle.Fill(SQL, 0, Nothing, "cls_getmiddletosecuill.CountDataMiddle()")
            If Not ds Is Nothing Then
                dt = ds.Tables(0)
            End If
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, "cls_getmiddletosecuill.GetDataMiddleStatusByPrescription()"))
        End Try
        Return dt
    End Function

    Public Shared Function ComparePresProcess(ByVal presno As String, ByVal lastmodified As DateTime) As Boolean
        Dim result As Boolean = False

        If presno <> "" Then
            Dim ds As DataSet = GetDataSecuillPrescription(presno)
            If ds.Tables(0).Rows.Count <> 0 Then
                Dim secuill_lastmodifed As DateTime = CDate(ds.Tables(0).Rows(0).Item("lastmodified"))
                If lastmodified > secuill_lastmodifed Then
                    result = UpdateStatusPrescSecuill(presno)
                End If
            End If
        End If

        Return result
    End Function

    Private Shared Function UpdateStatusPrescSecuill(ByVal presno As String) As Boolean
        Dim result As Boolean = False

        Dim SQL As String = String.Empty

        SQL &= "UPDATE T_Prescription SET "
        SQL &= "pres_dispensedstatus = 4" '===========
        SQL &= "WHERE pres_no = '" & presno & "' "
        SQL &= "AND pres_date = '" & Date.Now().ToString("yyyyMMdd") & "'"
        Try
            Using cmd As New SqlClient.SqlCommand(SQL)
                result = cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.UpdateStatusPrescSecuill())")
            End Using
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.UpdateStatusPrescSecuill())"))
        End Try
        Return result
    End Function

    Private Shared Function UpdateStatusPresc(ByVal presno As String, ByVal pres_seq As String, ByVal status As String, ByVal statusdesc As String) As Boolean
        Dim result As Boolean = False

        Dim SQL As String = String.Empty

        SQL &= " UPDATE T_Prescription "
        SQL &= " SET pres_status = '" & status & "'"
        SQL &= " , pres_statusdesc = '" & statusdesc & "'"
        SQL &= " WHERE pres_no = '" & presno & "' "
        SQL &= " AND pres_seq = '" & pres_seq & "'"

        Try
            Using cmd As New SqlClient.SqlCommand(SQL)
                result = cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.UpdateStatusPrescSecuill())")
            End Using
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.UpdateStatusPrescSecuill())"))
        End Try
        Return result
    End Function

    Private Shared Sub UpdateStatusMiddle(ByVal pres_date As String,
                                   ByVal pres_no As String,
                                   ByVal seq As String,
                                   ByVal hn As String,
                                   ByVal orderitemcode As String,
                                   ByVal statusUpdate As String)
        Dim SQL As String = String.Empty

        SQL = " SET XACT_ABORT ON"
        SQL &= " BEGIN TRANSACTION"

        SQL &= " UPDATE tb_thaneshosp_middle"
        SQL &= " SET f_dispensestatus_secuill = " & statusUpdate
        SQL &= " WHERE f_prescriptiondate = '" & pres_date & "'"
        SQL &= " AND f_prescriptionno = '" & pres_no & "'"
        SQL &= " AND f_seq = '" & seq & "'"
        SQL &= " AND f_hn = '" & hn & "'"
        SQL &= " AND f_orderitemcode = '" & orderitemcode & "'"

        SQL &= " COMMIT TRANSACTION"
        SQL &= " SET XACT_ABORT OFF"

        Try
            Using cmd As New SqlClient.SqlCommand(SQL)
                cls_condbmiddle.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.UpdateStatusMiddle())")
            End Using

        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.UpdateStatusMiddle())"))
        End Try

    End Sub

    Public Shared Sub ReloadData()
        Dim SQL As String = String.Empty

        SQL = " SET XACT_ABORT ON"
        SQL &= " BEGIN TRANSACTION"

        SQL &= " DELETE FROM T_Prescription"
        SQL &= " WHERE pres_date = '" & Date.Now().ToString("yyyyMMdd") & "'"

        SQL &= " COMMIT TRANSACTION"
        SQL &= " SET XACT_ABORT OFF"

        Try
            Using cmd As New SqlClient.SqlCommand(SQL)
                cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.UpdateStatusMiddle())")
            End Using

        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.UpdateStatusMiddle())"))
        End Try

        SQL = " SET XACT_ABORT ON"
        SQL &= " BEGIN TRANSACTION"

        SQL &= " UPDATE tb_thaneshosp_middle"
        SQL &= " SET f_dispensestatus_secuill = 0"
        SQL &= " WHERE f_dispensestatus_secuill = 1"
        SQL &= " AND f_noteprocessing NOT LIKE '%*f%'"
        SQL &= " AND f_prescriptiondate = '" & Date.Now().ToString("yyyyMMdd") & "'"

        SQL &= " COMMIT TRANSACTION"
        SQL &= " SET XACT_ABORT OFF"

        Try
            Using cmd As New SqlClient.SqlCommand(SQL)
                cls_condbmiddle.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.UpdateStatusMiddle())")
            End Using

        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.UpdateStatusMiddle())"))
        End Try
    End Sub

    Private Shared Sub GetWardToMaster(ByVal f_warcd As String, ByVal f_warddesc As String)

        Dim SQL As String = String.Empty
        Dim seq As Integer = GendSeqWard(f_warcd)

        SQL = " SELECT *"
        SQL &= " FROM M_Ward"
        SQL &= " WHERE ward_code = '" & f_warcd & "'"

        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetWardToMaster())")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count <= 0 Then

                    SQL = " INSERT INTO M_Ward("
                    SQL &= " ward_code"
                    SQL &= " , ward_seq"
                    SQL &= " , ward_name"
                    SQL &= " , ward_location"
                    SQL &= " , ward_status"
                    SQL &= " , lastmodified"
                    SQL &= " )"
                    SQL &= " VALUES("
                    SQL &= " '" & f_warcd & "'"
                    SQL &= " , " & seq & ""
                    SQL &= " , '" & f_warddesc & "'"
                    SQL &= " , NULL"
                    SQL &= " , 0"
                    SQL &= " , GETDATE()"
                    SQL &= " )"

                    Try
                        Using cmd As New SqlClient.SqlCommand(SQL)
                            cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetWardToMaster())")
                        End Using

                    Catch ex As Exception
                        Throw New Exception(ex.ToString())
                    End Try

                Else
                    SQL = " UPDATE M_Ward"
                    SQL &= " SET ward_name = '" & f_warddesc & "'"
                    SQL &= " WHERE ward_code = '" & f_warcd & "'"

                    Try
                        Using cmd As New SqlClient.SqlCommand(SQL)
                            cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetWardToMaster())")
                        End Using

                    Catch ex As Exception
                        Throw New Exception(ex.ToString())
                    End Try

                End If
            End If
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetWardToMaster())"))
        End Try

    End Sub

    Private Shared Sub GetDrugInterActionToMaster(ByVal interaction_no As String, _
                                                  ByVal drugcode As String, _
                                                  ByVal interaction_desc As String, _
                                                  ByVal drugcode_combine As String, _
                                                  ByVal degree_desc As String, _
                                                  ByVal interaction_drugcd1 As String, _
                                                  ByVal interaction_drugcd2 As String)

        Dim SQL As String = String.Empty

        SQL = " SELECT *"
        SQL &= " FROM M_DrugInterAction"
        SQL &= " WHERE interaction_no = '" & interaction_no & "'"


        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetDrugInterActionToMaster())")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count <= 0 Then


                    SQL = " INSERT INTO M_DrugInterAction("
                    SQL &= " interaction_no"
                    SQL &= " , drugcode"
                    SQL &= " , interaction_desc"
                    SQL &= " , drugcode_combine"
                    SQL &= " , degree_desc"
                    SQL &= " , interaction_drugcd1"
                    SQL &= " , interaction_drugcd2"
                    SQL &= " )"
                    SQL &= " VALUES("
                    SQL &= " '" & interaction_no & "'"
                    SQL &= " , '" & drugcode & "'"
                    SQL &= " , '" & interaction_desc & "'"
                    SQL &= " , '" & drugcode_combine & "'"
                    SQL &= " , '" & degree_desc & "'"
                    SQL &= " , '" & interaction_drugcd1 & "'"
                    SQL &= " , '" & interaction_drugcd2 & "'"
                    SQL &= " )"

                    Try
                        Using cmd As New SqlClient.SqlCommand(SQL)
                            cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetDrugInterActionToMaster())")
                        End Using

                    Catch ex As Exception
                        Throw New Exception(ex.ToString())
                    End Try

                Else
                    SQL = " UPDATE M_DrugInterAction"
                    SQL &= " SET drugcode = '" & drugcode & "'"
                    SQL &= " , interaction_desc = '" & interaction_desc & "'"
                    SQL &= " , drugcode_combine = '" & drugcode_combine & "'"
                    SQL &= " , degree_desc = '" & degree_desc & "'"
                    SQL &= " , interaction_drugcd1 = '" & interaction_drugcd1 & "'"
                    SQL &= " , interaction_drugcd2 = '" & interaction_drugcd2 & "'"
                    SQL &= " WHERE interaction_no = '" & interaction_no & "'"

                    Try
                        Using cmd As New SqlClient.SqlCommand(SQL)
                            cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetDrugInterActionToMaster())")
                        End Using

                    Catch ex As Exception
                        Throw New Exception(ex.ToString())
                    End Try

                End If
            End If
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetWardToMaster())"))
        End Try

    End Sub
    Private Shared Sub GetBedToMaster(ByVal f_bedcode As String, ByVal f_beddesc As String, ByVal f_roomcode As String)
        Dim SQL As String = String.Empty
        Dim seq As Integer = GendSeqBed(f_bedcode, "")

        SQL = " SELECT *"
        SQL &= " FROM M_Bed"
        SQL &= " WHERE bed_code = '" & f_bedcode & "'"

        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetBedToMaster())")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count <= 0 Then

                    SQL = " INSERT INTO M_Bed("
                    SQL &= " bed_code"
                    SQL &= " , bed_seq"
                    SQL &= " , room_code"
                    SQL &= " , bed_name"
                    SQL &= " , bed_status"
                    SQL &= " , lastmodified"
                    SQL &= " )"
                    SQL &= " VALUES("
                    SQL &= " '" & f_bedcode & "'"
                    SQL &= " , " & seq & ""
                    SQL &= " , '" & f_roomcode & "'"
                    SQL &= " , '" & f_beddesc & "'"
                    SQL &= " , 0"
                    SQL &= " , GETDATE()"
                    SQL &= " )"

                    Try
                        Using cmd As New SqlClient.SqlCommand(SQL)
                            cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetBedToMaster())")
                        End Using

                    Catch ex As Exception
                        Throw New Exception(ex.ToString())
                    End Try

                Else
                    SQL = " UPDATE M_Bed"
                    SQL &= " SET bed_name = '" & f_bedcode & "'"
                    SQL &= " WHERE bed_code = '" & f_bedcode & "'"

                    Try
                        Using cmd As New SqlClient.SqlCommand(SQL)
                            cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetBedToMaster())")
                        End Using

                    Catch ex As Exception
                        Throw New Exception(ex.ToString())
                    End Try

                End If
            End If
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetBedToMaster())"))
        End Try

    End Sub
    Private Shared Sub GetRoomToMaster(ByVal f_roomcode As String, ByVal roomname As String, ByVal f_wardcode As String)
        Dim SQL As String = String.Empty
        Dim seq As Integer = GendSeqRoom(f_roomcode, "")

        SQL = " SELECT *"
        SQL &= " FROM M_Room"
        SQL &= " WHERE room_code = '" & f_roomcode & "'"

        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetRoomToMaster())")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count <= 0 Then

                    SQL = " INSERT INTO M_Room("
                    SQL &= " room_code"
                    SQL &= " , room_seq"
                    SQL &= " , ward_code"
                    SQL &= " , room_name"
                    SQL &= " , room_status"
                    SQL &= " , lastmodified"
                    SQL &= " )"
                    SQL &= " VALUES("
                    SQL &= " '" & f_roomcode & "'"
                    SQL &= " , " & seq & ""
                    SQL &= " , '" & f_wardcode & "'"
                    SQL &= " , '" & roomname & "'"
                    SQL &= " , 0"
                    SQL &= " , GETDATE()"
                    SQL &= " )"

                    Try
                        Using cmd As New SqlClient.SqlCommand(SQL)
                            cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetRoomToMaster())")
                        End Using

                    Catch ex As Exception
                        Throw New Exception(ex.ToString())
                    End Try

                Else
                    SQL = " UPDATE M_Room"
                    SQL &= " SET room_name = '" & roomname & "'"
                    SQL &= " WHERE room_code = '" & f_roomcode & "'"

                    Try
                        Using cmd As New SqlClient.SqlCommand(SQL)
                            cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetRoomToMaster())")
                        End Using

                    Catch ex As Exception
                        Throw New Exception(ex.ToString())
                    End Try

                End If
            End If
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetRoomToMaster())"))
        End Try

    End Sub

    Private Shared Sub GetPatientInfo(ByVal pat_hn As String, _
                                      ByVal pat_an As String, _
                                      ByVal pat_vn As String, _
                                      ByVal pat_title As String,
                                      ByVal pat_name As String, _
                                      ByVal pat_sex As String, _
                                      ByVal pat_idcard As String, _
                                      ByVal pat_patientdob As String, _
                                      ByVal pat_blood As String, _
                                      ByVal pat_congenital_disease As String, _
                                      ByVal pat_diagnosis As String, _
                                      ByVal pat_rights As String, _
                                      ByVal pat_wardcode As String, _
                                      ByVal pat_roomcode As String, _
                                      ByVal pat_bedcode As String, _
                                      ByVal pat_admitteddate As String, _
                                      ByVal pat_dischargeddate As String, _
                                      ByVal pat_doctorcode As String, _
                                      ByVal pat_doctorname As String, _
                                      ByVal pat_image As String, _
                                      ByVal pat_onlinestatus As String)
        Dim SQL As String = String.Empty

        Dim RunNo As Integer = GendRunningnoPatient("")

        Dim patientdob As String = String.Empty
        If pat_patientdob <> "" Then
            'patientdob = CStr(CDate(pat_patientdob).ToString("yyyy-MM-dd HH:mm:ss"))
            Try
                patientdob = Date.ParseExact(pat_patientdob, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.CurrentInfo).ToString()
            Catch ex As Exception
            End Try
        End If

        Dim admitteddate As String = ""
        If pat_admitteddate = "" Then
            admitteddate = ""
        Else
            admitteddate = CStr(CDate(pat_admitteddate).ToString("yyyy-MM-dd HH:mm:ss"))
        End If


        Dim dischargeddate As String = ""
        If pat_dischargeddate = "" Then
            dischargeddate = ""
        Else
            dischargeddate = CStr(CDate(pat_dischargeddate).ToString("yyyy-MM-dd HH:mm:ss"))
        End If


        SQL = " SELECT *"
        SQL &= " FROM M_Patient"
        SQL &= " WHERE pat_hn = '" & pat_hn & "'"

        '' where status = 0

        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetPatientInfo())")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count <= 0 Then

                    SQL = " INSERT INTO M_Patient("
                    SQL &= " pat_runningno"
                    SQL &= " , pat_hn"
                    SQL &= " , pat_an"
                    SQL &= " , pat_vn"
                    SQL &= " , pat_title"
                    SQL &= " , pat_name"
                    SQL &= " , pat_sex"
                    SQL &= " , pat_idcard"
                    SQL &= " , pat_patientdob"
                    SQL &= " , pat_blood"
                    SQL &= " , pat_congenital_disease"
                    SQL &= " , pat_diagnosis"
                    SQL &= " , pat_rights"
                    SQL &= " , pat_wardcode"
                    SQL &= " , pat_roomcode"
                    SQL &= " , pat_bedcode"
                    SQL &= " , pat_admitteddate"
                    SQL &= " , pat_dischargeddate"
                    SQL &= " , pat_doctorcode"
                    SQL &= " , pat_doctorname"
                    SQL &= " , pat_image"
                    SQL &= " , pat_status"
                    SQL &= " , pat_statusdesc"
                    SQL &= " , lastmodified"
                    SQL &= " )"
                    SQL &= " VALUES("
                    SQL &= " '" & RunNo & "'"
                    SQL &= " , '" & pat_hn & "'"
                    SQL &= " , '" & pat_an & "'"
                    SQL &= " , '" & pat_vn & "'"
                    SQL &= " , '" & pat_title & "'"
                    SQL &= " , '" & pat_name & "'"
                    SQL &= " , '" & pat_sex & "'"
                    SQL &= " , '" & pat_idcard & "'"
                    'SQL &= " , CASE WHEN '" & patientdob & "' = '' THEN NULL ELSE '" & pat_patientdob & "' END"
                    SQL &= " , " & IIf(IsDate(patientdob), "'" & patientdob & "'", "NULL")
                    SQL &= " , '" & pat_blood & "'"
                    SQL &= " , '" & pat_congenital_disease & "'"
                    SQL &= " , '" & pat_diagnosis & "'"
                    SQL &= " , '" & pat_rights & "'"
                    SQL &= " , '" & pat_wardcode & "'"
                    SQL &= " , '" & pat_roomcode & "'"
                    SQL &= " , '" & pat_bedcode & "'"
                    'SQL &= " , '" & admitteddate & "'"
                    If (admitteddate = "") Then SQL &= " ,NULL" Else SQL &= " , '" & admitteddate & "'"
                    'SQL &= " , '" & dischargeddate & "'"
                    If (dischargeddate = "") Then SQL &= " ,NULL" Else SQL &= " , '" & dischargeddate & "'"
                    SQL &= " , '" & pat_doctorcode & "'"
                    SQL &= " , '" & pat_doctorname & "'"
                    SQL &= " , '" & pat_image & "'"
                    'SQL &= " , '" & pat_onlinestatus & "'" '------ 1 online  2 ÃÔªÒ·
                    SQL &= " , " & IIf(IsNumeric(pat_onlinestatus) = False, "0", pat_onlinestatus)
                    SQL &= " , NULL"
                    SQL &= " , GETDATE()"
                    SQL &= " )"

                    Try
                        Using cmd As New SqlClient.SqlCommand(SQL)
                            cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetPatientInfo())")
                        End Using

                    Catch ex As Exception
                        Throw New Exception(ex.ToString())
                    End Try

                Else

                    SQL = " UPDATE M_Patient"
                    SQL &= " SET pat_an = '" & pat_an & "'"
                    SQL &= " , pat_name = '" & pat_name & "'"
                    SQL &= " , pat_wardcode = '" & pat_wardcode & "'"
                    SQL &= " , pat_bedcode = '" & pat_bedcode & "'"
                    SQL &= " , pat_roomcode = '" & pat_roomcode & "'"
                    SQL &= " , lastmodified = GETDATE()"
                    SQL &= " WHERE pat_hn = '" & pat_hn & "'"
                    'SQL &= " AND pat_an = '" & pat_an & "'"

                    Try
                        Using cmd As New SqlClient.SqlCommand(SQL)
                            cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetPatientInfo())")
                        End Using

                    Catch ex As Exception
                        Throw New Exception(ex.ToString())
                    End Try

                End If
            End If
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetPatientInfo())"))
        End Try
    End Sub


    Private Shared Sub GetDrugAllergyToMaster(ByVal pat_hn As String,
                                              ByVal drug_code As String,
                                              ByVal drugallergy_desc As String)
        Dim SQL As String = String.Empty
        SQL = " SELECT pat_hn FROM M_PatientDrugAllergy WHERE pat_hn = '" & pat_hn & "'"
        Try

            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetDrugAllergyToMaster())")
            If ds IsNot Nothing Then
                If ds.Tables(0).Rows.Count <= 0 And drugallergy_desc.Trim() <> "-" Then
                    SQL =
                        $"INSERT INTO M_PatientDrugAllergy(
                            pat_hn
                            , drugallergy_desc
                            , lastmodified
                        )
                        VALUES(
                            '{pat_hn}'
                            , '{drugallergy_desc}'
                            , GETDATE()
                        )"
                Else
                    SQL =
                        $"UPDATE M_PatientDrugAllergy
                        SET drugallergy_desc = {IIf(drugallergy_desc.Trim() <> "-", $"'{drugallergy_desc}'", "NULL")}
                            , lastmodified = GETDATE()
                        WHERE pat_hn = '{pat_hn}'"
                End If

                Try
                    Using cmd As New SqlClient.SqlCommand(SQL)
                        cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetDrugAllergyToMaster())")
                    End Using

                Catch ex As Exception
                    Throw New Exception(ex.ToString())
                End Try
            End If
            cls_logfile.Write($"Elergy{vbTab} {pat_hn}, {drugallergy_desc}")
        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetDrugAllergyToMaster())"))
        End Try
    End Sub

    Private Shared Sub GetDrugMaster(ByVal drug_code As String,
                                        ByVal drug_unit As String,
                                        ByVal drug_desc As String,
                                        ByVal drug_name As String)

        Dim SQL As String = String.Empty

        SQL = " UPDATE M_Drug"
        SQL &= " SET drug_unit = '" & drug_unit & "'"
        SQL &= " , drug_desc = '" & drug_desc & "'"
        SQL &= " , drug_name_en = '" & drug_name & "'"
        SQL &= " WHERE drug_code = '" & drug_code & "'"

        Try
            Using cmd As New SqlClient.SqlCommand(SQL)
                cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetDrugMaster())")
            End Using

        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetDrugMaster())"))
        End Try

    End Sub

#Region "Gen Seq To Secuill"

    Public Shared Function GendSeqPrescription(ByVal presno As String, ByVal drugcd As String) As Integer
        Dim seq As Integer = 0
        Dim SQL As String = ""

        SQL = " SELECT (case when count(pres_no) = 0 then '1' "
        SQL &= " else count(pres_no) + 1 end)  as seq"
        SQL &= " FROM T_Prescription"
        SQL &= " WHERE pres_no = '" & presno & "'"
        SQL &= " AND drug_code = '" & drugcd & "'"

        Dim dsSeq = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GendSeqPrescription())")
        seq = CInt(dsSeq.Tables(0).Rows(0).Item("seq").ToString)

        Return seq
    End Function

    Public Shared Function GendSeqWard(ByVal f_warcd As String) As Integer
        Dim seq As Integer = 0
        Dim SQL As String = ""

        SQL = " SELECT (case when count(ward_code) = 0 then '1' "
        SQL &= " else count(ward_code) + 1 end)  as seq"
        SQL &= " FROM M_Ward"
        'SQL &= " WHERE ward_code = '" & f_warcd & "'"

        Dim dsSeq = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GendSeqWard())")
        seq = CInt(dsSeq.Tables(0).Rows(0).Item("seq").ToString)

        Return seq
    End Function
    Public Shared Function GendSeqBed(ByVal f_bedcode As String, ByVal f_roomcode As String) As Integer
        Dim seq As Integer = 0
        Dim SQL As String = ""


        SQL = " SELECT (case when count(bed_code) = 0 then '1' "
        SQL &= " else count(bed_code) + 1 end)  as seq"
        SQL &= " FROM M_Bed"
        SQL &= " WHERE bed_code = '" & f_bedcode & "'"
        'SQL &= " AND room_code = '" & f_roomcode & "'"

        Dim dsSeq = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GendSeqBed())")
        seq = CInt(dsSeq.Tables(0).Rows(0).Item("seq").ToString)

        Return seq
    End Function
    Public Shared Function GendSeqRoom(ByVal f_roomcode As String, ByVal f_wardcode As String) As Integer
        Dim seq As Integer = 0
        Dim SQL As String = ""

        SQL = " SELECT (case when count(room_code) = 0 then '1' "
        SQL &= " else count(room_code) + 1 end)  as seq"
        SQL &= " FROM M_Room"
        SQL &= " WHERE room_code = '" & f_roomcode & "'"
        'SQL &= " AND ward_code = '" & f_wardcode & "'"

        Dim dsSeq = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GendSeqRoom())")
        seq = CInt(dsSeq.Tables(0).Rows(0).Item("seq").ToString)

        Return seq
    End Function
    Public Shared Function gendrugallergyid(ByVal presno As String) As Integer
        Dim seq As Integer = 0
        Dim SQL As String = ""

        SQL = " SELECT (case when count(drugallergy_id) = 0 then '1' "
        SQL &= " else count(drugallergy_id) + 1 end)  as seq"
        SQL &= " FROM M_PatientDrugAllergy"
        'SQL &= " WHERE pat_hn = '" & presno & "'"

        Dim dsSeq = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.gendrugallergyid())")
        seq = CInt(dsSeq.Tables(0).Rows(0).Item("seq").ToString)

        Return seq
    End Function
    Public Shared Function GendRunningnoPatient(ByVal pat_hn As String) As Integer
        Dim Run As Integer = 0
        Dim SQL As String = ""

        SQL = " SELECT (case when count(pat_runningno) = 0 then '1' "
        SQL &= " else count(pat_runningno) + 1 end)  as seq"
        SQL &= " FROM M_Patient"
        'SQL &= " WHERE pat_hn = '" & pat_hn & "'"

        Dim dsSeq = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GendRunningnoPatient())")
        Run = CInt(dsSeq.Tables(0).Rows(0).Item("seq").ToString)

        Return Run
    End Function

#End Region

    Private Shared Function CheckDrugDuo(ByVal drugcode As String, ByVal order_qty As String) As DataTable
        Dim dt As DataTable = Nothing

        Dim SQL As String = String.Empty
        Dim DrugAllergy As Integer = gendrugallergyid("")

        SQL = " SELECT md.drug_code"
        SQL &= " , CONCAT('D',md.drug_name_en) AS drug_name_en"
        SQL &= " , md.drug_unit"
        SQL &= " , (mdo.duo_qty * " & order_qty & ") AS duo_qty"
        SQL &= " , 'D' AS pres_dispensedstatus"
        SQL &= " , msh.shelf_safebox"
        SQL &= " FROM M_DrugDuo mdo"
        SQL &= " LEFT JOIN M_Drug md"
        SQL &= " ON mdo.duo_drug_code = md.drug_code"
        SQL &= " LEFT JOIN M_Slot msl"
        SQL &= " ON  mdo.duo_drug_code = msl.drug_code"
        SQL &= " LEFT JOIN M_Shelf msh"
        SQL &= " ON msl.shelf_no = msh.shelf_no"
        SQL &= " WHERE mdo.drug_code = '" & drugcode & "'"
        SQL &= " GROUP BY md.drug_code, md.drug_name_en, mdo.duo_qty, msh.shelf_safebox, md.drug_unit"

        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetDrugAllergyToMaster())")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            End If

        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.CheckDrugDuo())"))
        End Try

        Return dt
    End Function

    Private Shared Function SwaithToBox(ByVal prescno As String, presseq As String, ByVal drug_code As String, ByVal pres_qty As String, ByVal unit As String) As DataTable
        Dim dt As New DataTable

        dt.Columns.Clear()
        dt.Columns.Add("pres_no", GetType(String))
        dt.Columns.Add("pres_seq", GetType(String))
        dt.Columns.Add("drug_code", GetType(String))
        dt.Columns.Add("pres_qty", GetType(String))
        dt.Columns.Add("pres_unit", GetType(String))
        dt.Rows.Clear()

        Dim SQL As String = String.Empty
        Dim DrugAllergy As Integer = gendrugallergyid("")

        SQL = " SELECT mdw.drug_code, mdw.sw_drug_code, ISNULL(mdw.sw_qty, 0) AS sw_qty"
        SQL &= " FROM M_DrugSwitch mdw"
        SQL &= " LEFT JOIN M_Drug md"
        SQL &= " ON mdw.drug_code = md.drug_code"
        SQL &= " WHERE mdw.drug_code = '" & drug_code & "'"

        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.SwaithToBox())")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim sw_drug_code As String = ds.Tables(0).Rows(i).Item("sw_drug_code").ToString()
                        Dim mQty As String = ds.Tables(0).Rows(i).Item("sw_qty").ToString()

                        If CInt(pres_qty) < CInt(mQty) Then
                            Exit For
                        Else
                            If CInt(pres_qty) = CInt(mQty) Then
                                Dim box As Double = Math.Round(CInt(pres_qty) / CInt(mQty))
                                Dim aa As Double = CInt(pres_qty) Mod CInt(mQty)

                                dt.Rows.Add(prescno, presseq + (presseq + 20), sw_drug_code, CStr(box), "Box")
                            Else
                                Dim box As Double = Math.Round(CInt(pres_qty) / CInt(mQty))
                                Dim aa As Double = CInt(pres_qty) Mod CInt(mQty)

                                dt.Rows.Add(prescno, presseq + (presseq + 20) + i, sw_drug_code, CStr(box), "Box")
                                dt.Rows.Add(prescno, presseq + (presseq + 20) + (i + 1), drug_code, CStr(aa), unit)
                            End If
                        End If

                    Next

                End If
            End If

        Catch ex As Exception
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetDrugMaster())"))
        End Try

        Return dt
    End Function

    '' µÃÇ¨ÊÍºÂÒ·ÕèËÂÔºâ´ÂäÁèÃÐºØãºÊÑè§ÂÒ ¢Í§¤¹¹Ñé¹ æ
    '' ÇèÒËÂÔºÂÒä»¡èÍ¹¨Ð¤ÕÂìãºÊÑè§ÂÒËÃ×ÍäÁè

    '' Get freedispensing
    'Private Shared Sub GetFreeDispensing(ByVal pres_no As String, ByVal pres_seq As String, ByVal pat_hn As String, ByVal drug_code As String, ByVal pres_qty As String)

    '    Dim SQL As String = String.Empty

    '    SQL &= " SELECT freepick_id"
    '    SQL &= " , freepick_no"
    '    SQL &= " , SUM(freepick_qty) AS freepick_qty"
    '    SQL &= " , user_id"
    '    SQL &= " , modular_code"
    '    SQL &= " , freepick_date"
    '    SQL &= " , freepick_time"
    '    SQL &= " FROM T_SummaryFreePick"
    '    SQL &= " WHERE pat_hn = '" & pat_hn & "'"
    '    SQL &= " AND drug_code = '" & drug_code & "'"
    '    SQL &= " AND freepick_qty <> 0"
    '    SQL &= " AND CONVERT(VARCHAR(16), CONCAT(freepick_date, ' ', freepick_time), 120) >= CONVERT(VARCHAR(16), DATEADD(HOUR, -120, CONVERT(VARCHAR, GETDATE())), 120)"
    '    ' SQL &= " AND freepick_date = '" & Date.Now().ToString("yyyy-MM-dd") & "'"
    '    SQL &= " GROUP BY freepick_id, freepick_no, freepick_date, freepick_time, user_id, modular_code"

    '    Try
    '        Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetDrugAllergyToMaster())")
    '        If Not ds Is Nothing Then
    '            If ds.Tables(0).Rows.Count > 0 Then

    '                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '                    Dim fid As String = ds.Tables(0).Rows(i).Item("freepick_id").ToString()
    '                    Dim fno As String = ds.Tables(0).Rows(i).Item("freepick_no").ToString()
    '                    Dim fqty As String = ds.Tables(0).Rows(i).Item("freepick_qty").ToString()
    '                    Dim fuid As String = ds.Tables(0).Rows(i).Item("user_id").ToString()
    '                    Dim fdate As String = ds.Tables(0).Rows(i).Item("freepick_date").ToString()
    '                    Dim ftime As String = ds.Tables(0).Rows(i).Item("freepick_time").ToString()
    '                    Dim fmd As String = ds.Tables(0).Rows(i).Item("modular_code").ToString()
    '                    Dim fuser As String = ds.Tables(0).Rows(i).Item("user_id").ToString()

    '                    Dim rs1 As Boolean = cls_getmiddletosecuill.InsertToDispensing(fno, pres_no, pres_seq, pat_hn, drug_code, fqty, fuid, fdate, ftime, fmd)
    '                    If rs1 = True Then
    '                        '' update pres = C
    '                        cls_getmiddletosecuill.UpdatePrescription(pres_no, pres_seq, pat_hn, drug_code, fqty, pres_qty, fuser)

    '                        '' update free qty = 0
    '                        cls_getmiddletosecuill.UpdateFreeDispensing(pres_no, pres_seq, pat_hn, drug_code, fqty)

    '                    End If
    '                Next
    '            End If
    '        End If

    '    Catch ex As Exception
    '        cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "Error", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.CheckDrugDuo())"))
    '    End Try



    'End Sub


    '' Inert data to Prescription dispensing
    'Private Shared Function InsertToDispensing(ByVal pick_no As String, ByVal pres_no As String, ByVal pres_seq As String, ByVal pat_hn As String, ByVal drug_code As String, ByVal pick_qty As String, ByVal user_id As String, ByVal sum_date As String, ByVal sum_time As String, ByVal modulelar As String) As Boolean
    '    Dim SQL As String = String.Empty
    '    Dim resp As Boolean = False

    '    SQL = " INSERT INTO T_SummaryPickPrescription("
    '    SQL &= " sum_id"
    '    SQL &= " , pick_no"
    '    SQL &= " , pres_no"
    '    SQL &= " , pres_seq"
    '    SQL &= " , pat_hn"
    '    SQL &= " , drug_code"
    '    SQL &= " , pick_qty"
    '    SQL &= " , user_id"
    '    SQL &= " , pick_errorstatus"
    '    SQL &= " , pick_desc"
    '    SQL &= " , modular_code"
    '    SQL &= " , sum_date"
    '    SQL &= " , sum_time"
    '    SQL &= " )"
    '    SQL &= " VALUES("
    '    SQL &= " FORMAT(GETDATE(), 'yyyyMMddHHmmssfff', 'en-us')"
    '    SQL &= " , '" & pick_no & "'"
    '    SQL &= " , '" & pres_no & "'"
    '    SQL &= " , '" & pres_seq & "'"
    '    SQL &= " , '" & pat_hn & "'"
    '    SQL &= " , '" & drug_code & "'"
    '    SQL &= " , '" & pick_qty & "'"
    '    SQL &= " , '" & user_id & "'"
    '    SQL &= " , '0'"
    '    SQL &= " , '¢éÍÁÙÅ¡ÒÃËÂÔºÂÒ¨Ò¡ Free dispensing'"
    '    SQL &= " , '" & modulelar & "'"
    '    SQL &= " , '" & sum_date & "'"
    '    SQL &= " , '" & sum_time & "'"
    '    SQL &= " )"

    '    Try
    '        Using cmd As New SqlClient.SqlCommand(SQL)
    '            Dim result As Boolean = cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetDrugMaster())")
    '            If result = True Then
    '                resp = True
    '            End If
    '        End Using

    '    Catch ex As Exception
    '        cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetDrugMaster())"))
    '    End Try

    '    Return resp
    'End Function

    'Private Shared Sub UpdateFreeDispensing(ByVal pres_no As String, ByVal pres_seq As String, ByVal pat_hn As String, ByVal drug_code As String, ByVal freepick_qty As String)
    '    Dim SQL As String = String.Empty
    '    Dim SQL_freepickqty As String = String.Empty

    '    SQL_freepickqty = " ISNULL((SELECT freepick_qty FROM T_SummaryFreePick WHERE pat_hn = '" & pat_hn & "' AND drug_code = '" & drug_code & "' AND freepick_qty <> 0 AND freepick_date = '" & Date.Now().ToString("yyyy-MM-dd") & "'), 0)"

    '    SQL = " UPDATE T_SummaryFreePick"
    '    SQL &= " SET freepick_qty = 0"
    '    SQL &= " , freepick_noteprocess = CONCAT('ÍÑ¾à´·ä»ÂÑ§ ãºÊÑè§ÂÒàÅ¢·Õè ( " & pres_no & " )( " & pres_seq & " ) ¨Ó¹Ç¹ÂÒ ', " & SQL_freepickqty & ")"
    '    SQL &= " WHERE pat_hn = '" & pat_hn & "'"
    '    SQL &= " AND drug_code = '" & drug_code & "'"
    '    SQL &= " AND freepick_qty <> 0"
    '    SQL &= " AND freepick_date = '" & Date.Now().ToString("yyyy-MM-dd") & "'"

    '    Try
    '        Using cmd As New SqlClient.SqlCommand(SQL)
    '            Dim result As Boolean = cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetDrugMaster())")
    '            If result = True Then

    '            End If
    '        End Using

    '    Catch ex As Exception
    '        cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetDrugMaster())"))
    '    End Try
    'End Sub

    'Private Shared Sub UpdatePrescription(ByVal pres_no As String, ByVal pres_seq As String, ByVal pat_hn As String, ByVal drug_code As String, ByVal freepick_qty As String, ByVal pres_qty As String, ByVal user_id As String)
    '    Dim SQL As String = String.Empty
    '    Dim SQL_freepickqty As String = String.Empty
    '    Dim SQL_User As String = String.Empty

    '    SQL_freepickqty = " ISNULL((SELECT freepick_qty FROM T_SummaryFreePick WHERE pat_hn = '" & pat_hn & "' AND drug_code = '" & drug_code & "' AND freepick_qty <> 0 AND freepick_date = '" & Date.Now().ToString("yyyy-MM-dd") & "'), 0)"
    '    SQL_User = " ISNULL((SELECT user_fullname FROM M_User WHERE user_id = '" & user_id & "'), '')"

    '    Dim StrStatus As String = "E"
    '    Dim StrDesc As String = "ERROR"
    '    Try
    '        If CInt(pres_qty) = CInt(freepick_qty) Then
    '            StrStatus = "C"
    '            StrDesc = "CONCAT('Dispensing complete | ËÂÔºÂÒ¤ÃºµÒÁ¨Ó¹Ç¹ | ËÂÔºÂÒ¨Ò¡ FreeDispensing ( " & pres_no & " )( " & pres_seq & " ) ¨Ó¹Ç¹ÂÒ ', " & SQL_freepickqty & ", ' | ', " & SQL_User & ")"
    '        Else
    '            StrStatus = "I"
    '            StrDesc = "CONCAT('Dispensing incomplete | ËÂÔºÂÒäÁè¤Ãº| ËÂÔºÂÒ¨Ò¡ FreeDispensing ( " & pres_no & " )( " & pres_seq & " ) ¨Ó¹Ç¹ÂÒ ', " & SQL_freepickqty & ", ' | ', " & SQL_User & ")"
    '        End If

    '    Catch ex As Exception

    '    End Try

    '    SQL = " UPDATE T_Prescription"
    '    SQL &= " SET pres_dispensedstatus = '" & StrStatus & "'"
    '    SQL &= " , pres_dispenseddesc = " & StrDesc & ""
    '    SQL &= " WHERE pres_no = '" & pres_no & "'"
    '    SQL &= " AND pres_seq = '" & pres_seq & "'"
    '    SQL &= " AND pat_hn = '" & pat_hn & "'"
    '    SQL &= " AND drug_code = '" & drug_code & "'"
    '    SQL &= " AND pres_status <> 'C'"
    '    SQL &= " AND pres_date = '" & Date.Now().ToString("yyyyMMdd") & "'"

    '    Try
    '        Using cmd As New SqlClient.SqlCommand(SQL)
    '            Dim result As Boolean = cls_condbsecuill.ExecuteNonQuery(cmd, " (cls_getmiddletosecuill.GetDrugMaster())")
    '            If result = True Then

    '            End If
    '        End Using

    '    Catch ex As Exception
    '        cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.GetDrugMaster())"))
    '    End Try
    'End Sub

    'Private Shared Function GetDrugInSecuill() As String
    '    Dim resp As String = String.Empty

    '    Dim SQL As String = String.Empty
    '    Dim DrugAllergy As Integer = gendrugallergyid("")

    '    SQL = " SELECT SUBSTRING(druglist, 2, LEN(druglist) - 1) AS druglist"
    '    SQL &= " FROM("
    '    SQL &= " SELECT (SELECT ',' + CHAR(39) + drug_code + CHAR(39) FROM M_Drug FOR XML PATH ('')) AS druglist"
    '    SQL &= " ) AS result"

    '    Try
    '        Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetDrugAllergyToMaster())")
    '        If Not ds Is Nothing Then
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                resp = ds.Tables(0).Rows(0).Item("druglist").ToString()
    '            End If
    '        End If

    '    Catch ex As Exception
    '        cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "ERROR", vbTab, ex.Message.ToString(), vbTab, " (cls_getmiddletosecuill.CheckDrugDuo())"))
    '    End Try

    '    Return resp
    'End Function

End Class
