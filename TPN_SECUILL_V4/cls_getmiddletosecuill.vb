
Imports System.Threading

Public Class cls_getmiddletosecuill

    Public Shared Sub RunProcess()
        'นับเครื่อง
        Dim countdata As Integer = cls_getmiddletosecuill.CountDataMiddleAll()

        If countdata > 0 Then
            cls_logfile.Write(String.Format("{0}{1}{2}{3}{4}", "EVENT", vbTab, "พบงานรอส่งรวมทุกตู้: " & countdata, vbTab, "cls_getmiddletosecuill.RunProcess()"))

            ' ดึงข้อมูลจาก Middle มาพักไว้ (ดึงงานที่ f_dispensestatus_secuill = 0 ทั้งหมด)
            Dim dtmiddle As DataTable = cls_getmiddletosecuill.GetDataMidleAll()

            If Not dtmiddle Is Nothing AndAlso dtmiddle.Rows.Count > 0 Then

                'วนลูปทีละใบสั่งยา (Row) จากตาราง Middle
                For i1 As Integer = 0 To dtmiddle.Rows.Count - 1
                    Dim middleObj As cls_middleObject = New cls_middleObject()
                    middleObj.GenObjectMiddle(dtmiddle, i1)

                    ' ส่งเข้าฐานข้อมูลลูก
                    Dim successCount As Integer = 0

                    For Each unit In md.ConnSecuillList
                        Dim machineKey As String = unit.Key   ' ชื่อเครื่องใน Config เช่น connserver-secuill-31
                        Dim connString As String = unit.Value ' Connection String ของเครื่องนั้น

                        Dim machineNo As String = machineKey.Replace("connserver-secuill-", "").Trim()

                        Dim assignedWards As String = cls_configuration.Read(My.Settings.ConfigPath.ToString(), "ward-machine-" & machineNo)
                        If Not assignedWards.Contains("'" & middleObj.F_wardcode & "'") Then
                            'ข้ามไปดูเครื่องถัดไป
                            Continue For
                        End If

                        Try

                            cls_condbsecuill.ConnectionString = connString

                            cls_getmiddletosecuill.GetWardToMaster(middleObj.F_wardcode, middleObj.F_warddesc)
                            cls_getmiddletosecuill.GetBedToMaster(middleObj.F_bedcode, middleObj.F_beddesc, middleObj.F_roomcode)
                            cls_getmiddletosecuill.GetRoomToMaster(middleObj.F_roomcode, middleObj.F_roomdesc, middleObj.F_wardcode)
                            cls_getmiddletosecuill.GetDrugMaster(middleObj.F_orderitemcode, middleObj.F_orderunitcode, middleObj.F_orderunitdesc, middleObj.F_orderitemname)
                            cls_getmiddletosecuill.GetDrugAllergyToMaster(middleObj.F_hn, middleObj.F_orderitemcode, middleObj.F_allergy)

                            cls_getmiddletosecuill.GetPatientInfo(middleObj.F_hn, middleObj.F_an, middleObj.F_vn, "", middleObj.F_patientname, middleObj.F_sex, "", middleObj.F_patientdob, "", "", "", "", middleObj.F_wardcode, middleObj.F_bedcode, middleObj.F_roomcode, "", "", middleObj.F_doctorcode, middleObj.F_doctorname, "", "")

                            Dim status As String = "A"
                            Dim statusdesc As String = "ACTIVE"
                            Dim presc_status As String = "W"

                            If middleObj.F_status = 2 Then
                                status = "C"
                                statusdesc = "CANCLE | ยกเลิก"
                                cls_getmiddletosecuill.UpdateStatusPresc(middleObj.F_prescriptionno, middleObj.F_seq, status, statusdesc)
                            End If

                            If CDec(middleObj.F_orderqty) <= 0 Then
                                Continue For
                            End If


                            Dim func = Function(ByVal drug_code As String, ByVal qty As String, ByVal unitcode As String) As Object
                                           Dim resp As Object = {qty, unitcode}
                                           Dim SQL_Conv As String = " SELECT unitqty, convto, convtounitcode FROM M_ConvertDrugUnit WHERE drug_code = '" & drug_code & "'"
                                           Dim dsSeq = cls_condbsecuill.Fill(SQL_Conv, "ConvUnit", Nothing, "CheckDrugConvert")
                                           Try
                                               If dsSeq.Tables(0).Rows.Count > 0 Then
                                                   Dim unitqty = CInt(dsSeq.Tables(0).Rows(0).Item("unitqty").ToString)
                                                   Dim conv = CInt(dsSeq.Tables(0).Rows(0).Item("convto").ToString)
                                                   Dim convtounitcode = dsSeq.Tables(0).Rows(0).Item("convtounitcode").ToString
                                                   If CInt(qty) Mod unitqty <> 0 Then Return resp
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

                            If ObjSecuill.Insert() = True Then
                                successCount += 1
                                cls_logfile.Write("SUCCESS" & vbTab & "Order " & middleObj.F_prescriptionno & " sent to Machine " & machineNo)
                            End If

                        Catch ex As Exception
                            cls_logfile.Write("ERROR" & vbTab & "เครื่อง " & machineNo & " พัง: " & ex.Message)
                        End Try
                    Next

                    If successCount > 0 Then
                        cls_getmiddletosecuill.UpdateStatusMiddle(middleObj.F_prescriptiondate, middleObj.F_prescriptionno, middleObj.F_seq, middleObj.F_hn, middleObj.F_orderitemcode, 1)
                    End If

                    Thread.Sleep(50)
                Next
            End If
        End If
    End Sub

    Public Shared Function CountDataMiddleAll() As Integer
        Dim resp As Integer = 0
        Dim SQL As String = ""
        SQL = " SELECT COUNT(*) AS count FROM tb_thaneshosp_middle "
        SQL &= " WHERE f_dispensestatus_secuill = 0 "
        SQL &= " AND f_wardcode IN ( " & md.WardCode.Replace("""", "'") & " ) "

        Try
            Dim ds As DataSet = cls_condbmiddle.Fill(SQL, 0, Nothing, "cls_getmiddletosecuill.CountDataMiddleAll()")
            If Not ds Is Nothing Then resp = ds.Tables(0).Rows(0).Item("count")
        Catch ex As Exception
            cls_logfile.Write("ERROR" & vbTab & ex.Message & vbTab & "CountDataMiddleAll")
        End Try
        Return resp
    End Function

    Public Shared Function CountDataMiddle() As Integer
        Return CountDataMiddleAll()
    End Function


    Public Shared Function GetDataMidleAll() As DataTable 'กรองข้อมูลจาก CONHIS_MIDDLE
        Dim dt As DataTable = Nothing
        Dim today As String = Date.Now().ToString("yyyyMMdd")
        Dim yesterday As String = DateAdd(DateInterval.Day, -1, Date.Now()).ToString("yyyyMMdd")

        Dim SQL As String = " SELECT * FROM tb_thaneshosp_middle "
        SQL &= " WHERE f_dispensestatus_secuill = 0 "
        SQL &= " AND f_wardcode IN ( " & md.WardCode.Replace("""", "'") & " ) "
        SQL &= " AND f_prescriptiondate IN ('" & today & "', '" & yesterday & "')"

        Try
            Dim ds As DataSet = cls_condbmiddle.Fill(SQL, 0, Nothing, " (cls_getmiddletosecuill.GetDataMidleAll())")
            If Not ds Is Nothing Then dt = ds.Tables(0)
        Catch ex As Exception
            cls_logfile.Write("ERROR" & vbTab & ex.Message & vbTab & "GetDataMidleAll")
        End Try
        Return dt
    End Function


    Public Shared Function GendAllDrugMasterSecuill() As String
        Dim resp As String = 0
        Dim SQL As String = "SELECT SUBSTRING((SELECT ',''' + drug_code + '''' FROM M_Drug FOR XML PATH ('')), 2, LEN((SELECT ',''' + drug_code + '''' FROM M_Drug FOR XML PATH ('')))) AS alldrug"
        Dim dsSeq = cls_condbsecuill.Fill(SQL, "AllDrug", Nothing, "GendAllDrugMasterSecuill")
        Return dsSeq.Tables(0).Rows(0).Item("alldrug").ToString
    End Function

    Public Shared Function CheckDrugMasterSecuill(ByVal drugcode As String) As Integer
        Dim resp As Integer = 0
        Dim SQL As String = " SELECT COUNT(*) AS countdrug FROM M_Drug WHERE drug_code = '" & drugcode & "' AND drug_status = 1"
        Dim dsSeq = cls_condbsecuill.Fill(SQL, "CheckDrug", Nothing, "CheckDrugMasterSecuill")
        Try : resp = CInt(dsSeq.Tables(0).Rows(0).Item("countdrug").ToString) : Catch : End Try
        Return resp
    End Function

    Public Shared Function GetDataSecuillPrescription(ByVal pres_no As String) As DataSet
        Dim SQL As String = " SELECT TOP 1 * FROM T_Prescription WHERE pres_dispensedstatus = '1' AND pres_no ='" & pres_no & "' AND pres_date = '" & Date.Now().ToString("yyyyMMdd") & "' ORDER BY lastmodified DESC"
        Return cls_condbsecuill.Fill(SQL, "GetPres", Nothing, "GetDataSecuillPrescription")
    End Function

    Private Shared Function UpdateStatusPresc(ByVal pres_no As String, ByVal pres_seq As String, ByVal status As String, ByVal statusdesc As String) As Boolean
        Dim SQL As String = " UPDATE T_Prescription SET pres_status = '" & status & "', pres_statusdesc = '" & statusdesc & "' WHERE pres_no = '" & pres_no & "' AND pres_seq = '" & pres_seq & "'"
        Try
            Using cmd As New SqlClient.SqlCommand(SQL) : Return cls_condbsecuill.ExecuteNonQuery(cmd, "UpdateStatusPresc") : End Using
        Catch : Return False : End Try
    End Function

    Private Shared Sub UpdateStatusMiddle(ByVal pres_date As String, ByVal pres_no As String, ByVal seq As String, ByVal hn As String, ByVal orderitemcode As String, ByVal statusUpdate As String)
        Dim SQL As String = " UPDATE tb_thaneshosp_middle SET f_dispensestatus_secuill = " & statusUpdate & " WHERE f_prescriptiondate = '" & pres_date & "' AND f_prescriptionno = '" & pres_no & "' AND f_seq = '" & seq & "' AND f_hn = '" & hn & "' AND f_orderitemcode = '" & orderitemcode & "'"
        Try : Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbmiddle.ExecuteNonQuery(cmd, "UpdateStatusMiddle") : End Using : Catch : End Try
    End Sub

    Private Shared Sub GetWardToMaster(ByVal f_warcd As String, ByVal f_warddesc As String)
        Dim SQL As String = " SELECT * FROM M_Ward WHERE ward_code = '" & f_warcd & "'"
        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, "Ward", Nothing, "GetWardToMaster")
            If Not ds Is Nothing AndAlso ds.Tables(0).Rows.Count <= 0 Then
                SQL = " INSERT INTO M_Ward(ward_code, ward_seq, ward_name, ward_status, lastmodified) VALUES('" & f_warcd & "', 1, '" & f_warddesc & "', 0, GETDATE())"
                Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbsecuill.ExecuteNonQuery(cmd, "InsertWard") : End Using
            Else
                SQL = " UPDATE M_Ward SET ward_name = '" & f_warddesc & "' WHERE ward_code = '" & f_warcd & "'"
                Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbsecuill.ExecuteNonQuery(cmd, "UpdateWard") : End Using
            End If
        Catch : End Try
    End Sub

    Private Shared Sub GetBedToMaster(ByVal f_bedcode As String, ByVal f_beddesc As String, ByVal f_roomcode As String)
        Dim SQL As String = " SELECT * FROM M_Bed WHERE bed_code = '" & f_bedcode & "'"
        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, "Bed", Nothing, "GetBedToMaster")
            If Not ds Is Nothing AndAlso ds.Tables(0).Rows.Count <= 0 Then
                SQL = " INSERT INTO M_Bed(bed_code, bed_seq, room_code, bed_name, bed_status, lastmodified) VALUES('" & f_bedcode & "', 1, '" & f_roomcode & "', '" & f_beddesc & "', 0, GETDATE())"
                Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbsecuill.ExecuteNonQuery(cmd, "InsertBed") : End Using
            Else
                SQL = " UPDATE M_Bed SET bed_name = '" & f_bedcode & "' WHERE bed_code = '" & f_bedcode & "'"
                Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbsecuill.ExecuteNonQuery(cmd, "UpdateBed") : End Using
            End If
        Catch : End Try
    End Sub

    Private Shared Sub GetRoomToMaster(ByVal f_roomcode As String, ByVal roomname As String, ByVal f_wardcode As String)
        Dim SQL As String = " SELECT * FROM M_Room WHERE room_code = '" & f_roomcode & "'"
        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, "Room", Nothing, "GetRoomToMaster")
            If Not ds Is Nothing AndAlso ds.Tables(0).Rows.Count <= 0 Then
                SQL = " INSERT INTO M_Room(room_code, room_seq, ward_code, room_name, room_status, lastmodified) VALUES('" & f_roomcode & "', 1, '" & f_wardcode & "', '" & roomname & "', 0, GETDATE())"
                Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbsecuill.ExecuteNonQuery(cmd, "InsertRoom") : End Using
            Else
                SQL = " UPDATE M_Room SET room_name = '" & roomname & "' WHERE room_code = '" & f_roomcode & "'"
                Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbsecuill.ExecuteNonQuery(cmd, "UpdateRoom") : End Using
            End If
        Catch : End Try
    End Sub

    Private Shared Sub GetPatientInfo(ByVal pat_hn As String, ByVal pat_an As String, ByVal pat_vn As String, ByVal pat_title As String, ByVal pat_name As String, ByVal pat_sex As String, ByVal pat_idcard As String, ByVal pat_patientdob As String, ByVal pat_blood As String, ByVal pat_congenital_disease As String, ByVal pat_diagnosis As String, ByVal pat_rights As String, ByVal pat_wardcode As String, ByVal pat_roomcode As String, ByVal pat_bedcode As String, ByVal pat_admitteddate As String, ByVal pat_dischargeddate As String, ByVal pat_doctorcode As String, ByVal pat_doctorname As String, ByVal pat_image As String, ByVal pat_onlinestatus As String)
        Dim SQL As String = " SELECT * FROM M_Patient WHERE pat_hn = '" & pat_hn & "'"
        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, "Patient", Nothing, "GetPatientInfo")
            If Not ds Is Nothing AndAlso ds.Tables(0).Rows.Count <= 0 Then
                SQL = " INSERT INTO M_Patient(pat_runningno, pat_hn, pat_an, pat_vn, pat_name, pat_wardcode, pat_roomcode, pat_bedcode, pat_status, lastmodified) "
                SQL &= " VALUES(1, '" & pat_hn & "', '" & pat_an & "', '" & pat_vn & "', '" & pat_name & "', '" & pat_wardcode & "', '" & pat_roomcode & "', '" & pat_bedcode & "', 0, GETDATE())"
                Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbsecuill.ExecuteNonQuery(cmd, "InsertPatient") : End Using
            Else
                SQL = " UPDATE M_Patient SET pat_an = '" & pat_an & "', pat_name = '" & pat_name & "', pat_wardcode = '" & pat_wardcode & "', pat_bedcode = '" & pat_bedcode & "', lastmodified = GETDATE() WHERE pat_hn = '" & pat_hn & "'"
                Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbsecuill.ExecuteNonQuery(cmd, "UpdatePatient") : End Using
            End If
        Catch : End Try
    End Sub

    Private Shared Sub GetDrugAllergyToMaster(ByVal pat_hn As String, ByVal drug_code As String, ByVal drugallergy_desc As String)
        Dim SQL As String = " SELECT pat_hn FROM M_PatientDrugAllergy WHERE pat_hn = '" & pat_hn & "'"
        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, "Allergy", Nothing, "GetDrugAllergy")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count <= 0 And drugallergy_desc.Trim() <> "-" Then
                    SQL = "INSERT INTO M_PatientDrugAllergy(pat_hn, drugallergy_desc, lastmodified) VALUES('" & pat_hn & "', '" & drugallergy_desc & "', GETDATE())"
                Else
                    SQL = "UPDATE M_PatientDrugAllergy SET drugallergy_desc = '" & drugallergy_desc & "', lastmodified = GETDATE() WHERE pat_hn = '" & pat_hn & "'"
                End If
                Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbsecuill.ExecuteNonQuery(cmd, "GetDrugAllergy") : End Using
            End If
        Catch : End Try
    End Sub

    Private Shared Sub GetDrugMaster(ByVal drug_code As String, ByVal drug_unit As String, ByVal drug_desc As String, ByVal drug_name As String)
        Dim SQL As String = " UPDATE M_Drug SET drug_unit = '" & drug_unit & "', drug_name_en = '" & drug_name & "' WHERE drug_code = '" & drug_code & "'"
        Try : Using cmd As New SqlClient.SqlCommand(SQL) : cls_condbsecuill.ExecuteNonQuery(cmd, "UpdateDrugMaster") : End Using : Catch : End Try
    End Sub

    Public Shared Function GetDataMiddleStatusByPrescription(ByVal pres_no As String) As DataTable
        Dim dt As DataTable = Nothing

        Dim SQL As String = String.Empty

        SQL = " SELECT  *"
        SQL &= " FROM tb_thaneshosp_middle"
        SQL &= " WHERE f_tomachineno = '" & md.Tomachineno & "'"
        SQL &= " AND f_wardcode IN( " & md.WardCode & " )"
        SQL &= " AND f_prescriptiondate = '" & pres_no & "'"
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


End Class