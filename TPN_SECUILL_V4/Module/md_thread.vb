Module md_thread

    Public Sub runSubByThread(act As Action)
        Dim t = New Threading.Thread(New Threading.ThreadStart(Sub() act()))
        t.IsBackground = True
        t.Start()
    End Sub
End Module
