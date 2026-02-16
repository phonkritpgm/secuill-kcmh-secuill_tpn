Public Class cls_intoSecuill

    '' insert ward

    Private Function InsertWard(ByVal ward_code As String,
                                ByVal ward_seq As String,
                                ByVal ward_name As String,
                                ByVal ward_location As String,
                                ByVal ward_status As String,
                                ByVal lastmodified As String) As Boolean


        Dim resp As Boolean = False

        Dim SQL As String = ""
        SQL = " INSERT INTO M_Ward("
        SQL &= " ward_code"
        SQL &= " , ward_seq"
        SQL &= " , ward_name"
        SQL &= " , ward_location"
        SQL &= " , ward_status"
        SQL &= " , lastmodified"
        SQL &= " )"
        SQL &= " VALUES("
        SQL &= " " & ward_code & ""
        SQL &= " , " & ward_seq & ""
        SQL &= " , " & ward_name & ""
        SQL &= " , " & ward_location & ""
        SQL &= " , " & ward_status & ""
        SQL &= " , " & lastmodified & ""
        SQL &= " )"

        Try

            Dim cmd As New SqlClient.SqlCommand(SQL)
            cls_condbsecuill.ExecuteNonQuery(cmd, "")

            resp = True

        Catch ex As Exception

            md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)

            resp = False

        End Try

        Return resp
    End Function

    '' insert room
    Private Function InsertRoom(ByVal room_code As String,
                                ByVal room_seq As String,
                                ByVal ward_code As String,
                                ByVal room_name As String,
                                ByVal room_status As String,
                                ByVal lastmodified As String) As Boolean

        Dim resp As Boolean = False

        Dim SQL As String = ""
        SQL = " INSERT INTO M_Room("
        SQL &= " room_code"
        SQL &= " , room_seq"
        SQL &= " , ward_code"
        SQL &= " , room_name"
        SQL &= " , room_status"
        SQL &= " , lastmodified"
        SQL &= " )"
        SQL &= " VALUES("
        SQL &= " " & room_code & ""
        SQL &= " , " & room_seq & ""
        SQL &= " , " & ward_code & ""
        SQL &= " , " & room_name & ""
        SQL &= " , " & room_status & ""
        SQL &= " , " & lastmodified & ""
        SQL &= " )"

        Try

            Dim cmd As New SqlClient.SqlCommand(SQL)
            cls_condbsecuill.ExecuteNonQuery(cmd, "")

            resp = True

        Catch ex As Exception

            md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)

            resp = False

        End Try

        Return resp
    End Function

    '' insert bed
    Private Function InsertBed(ByVal bed_code As String,
                               ByVal bed_seq As String,
                               ByVal room_code As String,
                               ByVal bed_name As String,
                               ByVal bed_status As String,
                               ByVal lastmodified As String) As Boolean

        Dim resp As Boolean = False

        Dim SQL As String = ""
        SQL = " INSERT INTO M_Bed("
        SQL &= " bed_code"
        SQL &= " , bed_seq"
        SQL &= " , room_code"
        SQL &= " , bed_name"
        SQL &= " , bed_status"
        SQL &= " , lastmodified"
        SQL &= " )"
        SQL &= " VALUES("
        SQL &= " " & bed_code & ""
        SQL &= " , " & bed_seq & ""
        SQL &= " , " & room_code & ""
        SQL &= " , " & bed_name & ""
        SQL &= " , " & bed_status & ""
        SQL &= " , " & lastmodified & ""
        SQL &= " )"

        Try

            Dim cmd As New SqlClient.SqlCommand(SQL)
            cls_condbsecuill.ExecuteNonQuery(cmd, "")

            resp = True

        Catch ex As Exception

            md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)

            resp = False

        End Try

        Return resp
    End Function

    '' insert patient
    Private Function InsertPatient(ByVal pat_runningno As String,
                                   ByVal pat_hn As String,
                                   ByVal pat_an As String,
                                   ByVal pat_vn As String,
                                   ByVal pat_title As String,
                                   ByVal pat_name As String,
                                   ByVal pat_sex As String,
                                   ByVal pat_idcard As String,
                                   ByVal pat_patientdob As String,
                                   ByVal pat_blood As String,
                                   ByVal pat_congenital_disease As String,
                                   ByVal pat_diagnosis As String,
                                   ByVal pat_rights As String,
                                   ByVal pat_wardcode As String,
                                   ByVal pat_roomcode As String,
                                   ByVal pat_bedcode As String,
                                   ByVal pat_admitteddate As String,
                                   ByVal pat_dischargeddate As String,
                                   ByVal pat_doctorcode As String,
                                   ByVal pat_doctorname As String,
                                   ByVal pat_image As String,
                                   ByVal pat_status As String,
                                   ByVal pat_statusdesc As String,
                                   ByVal lastmodified As String) As Boolean

        Dim resp As Boolean = False

        Dim SQL As String = ""
        SQL = " INSERT INTO M_Patient("
        SQL &= "  pat_runningno"
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
        SQL &= " " & pat_runningno & ""
        SQL &= " , " & pat_hn & ""
        SQL &= " , " & pat_an & ""
        SQL &= " , " & pat_vn & ""
        SQL &= " , " & pat_title & ""
        SQL &= " , " & pat_name & ""
        SQL &= " , " & pat_sex & ""
        SQL &= " , " & pat_idcard & ""
        SQL &= " , " & pat_patientdob & ""
        SQL &= " , " & pat_blood & ""
        SQL &= " , " & pat_congenital_disease & ""
        SQL &= " , " & pat_diagnosis & ""
        SQL &= " , " & pat_rights & ""
        SQL &= " , " & pat_wardcode & ""
        SQL &= " , " & pat_roomcode & ""
        SQL &= " , " & pat_bedcode & ""
        SQL &= " , " & pat_admitteddate & ""
        SQL &= " , " & pat_dischargeddate & ""
        SQL &= " , " & pat_doctorcode & ""
        SQL &= " , " & pat_doctorname & ""
        SQL &= " , " & pat_image & ""
        SQL &= " , " & pat_status & ""
        SQL &= " , " & pat_statusdesc & ""
        SQL &= " , " & lastmodified & ""
        SQL &= " )"

        Try

            Dim cmd As New SqlClient.SqlCommand(SQL)
            cls_condbsecuill.ExecuteNonQuery(cmd, "")

            resp = True

        Catch ex As Exception

            md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)

            resp = False

        End Try

        Return resp
    End Function
    '' insert DrugInterAction
    Private Function InsertDrugInterAction(ByVal interaction_no As String,
                                            ByVal drugcode As String,
                                            ByVal interaction_desc As String,
                                            ByVal drugcode_combine As String,
                                            ByVal degree_desc As String,
                                            ByVal interaction_drugcd1 As String,
                                            ByVal interaction_drugcd2 As String) As Boolean

        Dim resp As Boolean = False

        Dim SQL As String = ""

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
        SQL &= " " & interaction_no & ""
        SQL &= " , " & drugcode & ""
        SQL &= " , " & interaction_desc & ""
        SQL &= " , " & drugcode_combine & ""
        SQL &= " , " & degree_desc & ""
        SQL &= " , " & interaction_drugcd1 & ""
        SQL &= " , " & interaction_drugcd2 & ""
        SQL &= " )"

        Try

            Dim cmd As New SqlClient.SqlCommand(SQL)
            cls_condbsecuill.ExecuteNonQuery(cmd, "")

            resp = True

        Catch ex As Exception

            md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)

            resp = False

        End Try

        Return resp
    End Function

    '' insert drug allergy
    Private Function InsertDrugAllergy(ByVal drugallergy_id As String,
                                       ByVal pat_hn As String,
                                       ByVal drug_code As String,
                                       ByVal drugallergy_desc As String,
                                       ByVal lastmodified As String) As Boolean

        Dim resp As Boolean = False

        Dim SQL As String = ""
        SQL = " INSERT INTO M_PatientDrugAllergy("
        SQL &= " drugallergy_id"
        SQL &= " , pat_hn"
        SQL &= " , drug_code"
        SQL &= " , drugallergy_desc"
        SQL &= " , lastmodified"
        SQL &= " )"
        SQL &= " VALUES("
        SQL &= " " & drugallergy_id & "'"
        SQL &= " , " & pat_hn & "'"
        SQL &= " , " & drug_code & "'"
        SQL &= " , " & drugallergy_desc & "'"
        SQL &= " , " & lastmodified & "'"
        SQL &= " )"

        Try

            Dim cmd As New SqlClient.SqlCommand(SQL)
            cls_condbsecuill.ExecuteNonQuery(cmd, "")

            resp = True

        Catch ex As Exception

            md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)

            resp = False

        End Try

        Return resp
    End Function

    '' insert Prescription
    Private Function InsertPrescription(ByVal pres_no As String,
                                        ByVal pres_seq As String,
                                        ByVal pres_date As String,
                                        ByVal pres_barcode As String,
                                        ByVal pat_hn As String,
                                        ByVal drug_code As String,
                                        ByVal pres_orderqty As String,
                                        ByVal pres_orderunitcode As String,
                                        ByVal pres_orderunitdesc As String,
                                        ByVal pres_instructioncode As String,
                                        ByVal pres_instructiondesc As String,
                                        ByVal pres_dosage As String,
                                        ByVal pres_dosageunit As String,
                                        ByVal pres_frequencycode As String,
                                        ByVal pres_frequencydesc As String,
                                        ByVal pres_userorderby As String,
                                        ByVal pres_useracceptby As String,
                                        ByVal pres_ordercreatedate As String,
                                        ByVal pres_ordercreatetime As String,
                                        ByVal pres_orderacceptdate As String,
                                        ByVal pres_orderaccepttime As String,
                                        ByVal pres_fromlocation As String,
                                        ByVal pres_status As String,
                                        ByVal pres_statusdesc As String,
                                        ByVal pres_dispensedstatus As String,
                                        ByVal pres_dispenseddesc As String,
                                        ByVal lastmodified As String) As Boolean

        Dim resp As Boolean = False

        Dim SQL As String = ""
        SQL = " INSERT INTO T_Prescription("
        ''SQL &= " pres_runningno"
        SQL &= "  pres_no"
        SQL &= " , pres_seq"
        SQL &= " , pres_date"
        SQL &= " , pres_barcode"
        SQL &= " , pat_hn"
        SQL &= " , drug_code"
        SQL &= " , pres_orderqty"
        SQL &= " , pres_orderunitcode"
        SQL &= " , pres_orderunitdesc"
        SQL &= " , pres_instructioncode"
        SQL &= " , pres_instructiondesc"
        SQL &= " , pres_dosage"
        SQL &= " , pres_dosageunit"
        SQL &= " , pres_frequencycode"
        SQL &= " , pres_frequencydesc"
        SQL &= " , pres_userorderby"
        SQL &= " , pres_useracceptby"
        SQL &= " , pres_ordercreatedate"
        SQL &= " , pres_ordercreatetime"
        SQL &= " , pres_orderacceptdate"
        SQL &= " , pres_orderaccepttime"
        SQL &= " , pres_fromlocation"
        SQL &= " , pres_status"
        SQL &= " , pres_statusdesc"
        SQL &= " , pres_dispensedstatus"
        SQL &= " , pres_dispenseddesc"
        SQL &= " , lastmodified"
        SQL &= " )"
        SQL &= " VALUES("
        ''SQL &= " " & pres_runningno & "'"
        SQL &= "  " & pres_no & "'"
        SQL &= " , " & pres_seq & "'"
        SQL &= " , " & pres_date & "'"
        SQL &= " , " & pres_barcode & "'"
        SQL &= " , " & pat_hn & "'"
        SQL &= " , " & drug_code & "'"
        SQL &= " , " & pres_orderqty & "'"
        SQL &= " , " & pres_orderunitcode & "'"
        SQL &= " , " & pres_orderunitdesc & "'"
        SQL &= " , " & pres_instructioncode & "'"
        SQL &= " , " & pres_instructiondesc & "'"
        SQL &= " , " & pres_dosage & "'"
        SQL &= " , " & pres_dosageunit & "'"
        SQL &= " , " & pres_frequencycode & "'"
        SQL &= " , " & pres_frequencydesc & "'"
        SQL &= " , " & pres_userorderby & "'"
        SQL &= " , " & pres_useracceptby & "'"
        SQL &= " , " & pres_ordercreatedate & "'"
        SQL &= " , " & pres_ordercreatetime & "'"
        SQL &= " , " & pres_orderacceptdate & "'"
        SQL &= " , " & pres_orderaccepttime & "'"
        SQL &= " , " & pres_fromlocation & "'"
        SQL &= " , " & pres_status & "'"
        SQL &= " , " & pres_statusdesc & "'"
        SQL &= " , " & pres_dispensedstatus & "'"
        SQL &= " , " & pres_dispenseddesc & "'"
        SQL &= " , " & lastmodified & "'"
        SQL &= " )"

        Try

            Dim cmd As New SqlClient.SqlCommand(SQL)
            cls_condbsecuill.ExecuteNonQuery(cmd, "")

            md.EventText.SetText(EventText.TextState.Insert, "[pres_no : " & pres_no & "] [pres_date : " & pres_date & "] [pat_hn : " & pat_hn & "] [drug_code : " & drug_code & "] ", EventText.TextColor.Red, EventText.WriteLog.Yes)

            resp = True

        Catch ex As Exception

            md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)

            resp = False

        End Try

        Return resp
    End Function

    ''##################################################  Check update data ######################################################
    Private Sub CheckPrescription(ByVal pres_no As String,
                                  ByVal pres_seq As String,
                                  ByVal pres_date As String,
                                  ByVal pat_hn As String,
                                  ByVal drug_code As String)

        Dim SQL As String = ""
        '' มีใบยา และยาตัวเดิม
        SQL = " SELECT COUNT(*) AS count"
        SQL &= " FROM T_Prescription"
        SQL &= " WHERE pres_no = '" & pres_no & "'"
        SQL &= " AND pres_seq = '" & pres_seq & "'"
        SQL &= " AND pres_date = '" & pres_date & "'"
        SQL &= " AND pat_hn = '" & pat_hn & "'"
        SQL &= " AND drug_code = '" & drug_code & "'"

        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, "")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                End If
            End If
        Catch ex As Exception

            md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
        End Try

        '' update 
    End Sub

    Public Function GetPrescriptionReturn() As DataTable

        Dim SQL As String = ""
        '' มีใบยา และยาตัวเดิม
        SQL = " SELECT "
        SQL &= " RunningNo"
        SQL &= " , PrescriptionNo"
        SQL &= " , PrescriptionDesc"
        SQL &= " , PrescriptionDate"
        SQL &= " , CreateDT"
        SQL &= " , TargetDate"
        SQL &= " , TargetTime"
        SQL &= " , Seq"
        SQL &= " , SeqMax"
        SQL &= " , PatHN"
        SQL &= " , PatAN"
        SQL &= " , PatNm"
        SQL &= " , WardCd"
        SQL &= " , WardNm"
        SQL &= " , DrugCd"
        SQL &= " , DrugNm"
        SQL &= " , OrderQty"
        SQL &= " , UnitNm"
        SQL &= " , Dosage"
        SQL &= " , DosageUnit"
        SQL &= " , InstructionCd"
        SQL &= " , InstructionNm"
        SQL &= " , PriorityCd"
        SQL &= " , PriorityNm"
        SQL &= " , StatPrn"
        SQL &= " , HialertDrug"
        SQL &= " , FrequencyCd"
        SQL &= " , FrequencyTime"
        SQL &= " , NotProcessing"
        SQL &= " , MachLocationID"
        SQL &= " , MachLocationDesc"
        SQL &= " , ProcessNo"
        SQL &= " , ProcessID"
        SQL &= " , ProcessDesc"
        SQL &= " , UserID"
        SQL &= " , UserNm"
        SQL &= " , UserID2"
        SQL &= " , UserNm2"
        SQL &= " , PickDate"
        SQL &= " , PickTime"
        SQL &= " , ReturnStatus"
        SQL &= " , ReturnDesc"
        SQL &= " , LastModified"
        SQL &= " FROM T_PrescriptionReturn"
        SQL &= " WHERE ReturnStatus = 0"
        SQL &= " AND PrescriptionDate BETWEEN '" & Date.Now().AddHours(-3).ToString("yyyyMMdd") & "' AND '" & Date.Now().ToString("yyyyMMdd") & "'"

        Try
            Dim ds As DataSet = cls_condbsecuill.Fill(SQL, 0, Nothing, "")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0)
                Else
                    Return ds.Tables(0)
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            md.EventText.SetText(EventText.TextState._Error, ex.Message.ToString(), EventText.TextColor.Red, EventText.WriteLog.Yes)
            Return Nothing
        End Try

        '' update 
    End Function

End Class
