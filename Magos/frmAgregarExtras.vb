Public Class frmAgregarExtras
    Dim LEXT As New List(Of String)
    Dim LNOM As New List(Of String)
    Dim LPRE As New List(Of String)
    Dim NCUAG As Integer
    Dim INDG As Integer
    Dim PGRU(10) As PictureBox
    Dim LBLPRE(10) As Label
    Dim DV As New DataView
    Dim LELE As New List(Of clsExtra)
    Dim TOT As Double
    Dim VIDCOMBO As Integer
    Dim VIDREG As Integer
    Private Sub frmAgregarExtras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
    End Sub
    Public Sub MOSTRAR(ByVal PLA As Integer, ByVal IDCOMBO As Integer, ByVal IDREG As Integer, ByVal NPLA As String)
        NCUAG = 9
        VIDCOMBO = IDCOMBO
        VIDREG = IDREG
        Label2.ForeColor = Color.FromArgb(frmPrincipal.COLORBASE)
        INDG = 1
        FLP.Controls.Clear()
        LELE.Clear()
        INICIALIZAR()
        CARGADATAVIEW(PLA)
        Me.Text = "Seleccionar Extras para: " + NPLA
        LLENARLB()
        Me.ShowDialog()
    End Sub
    Private Sub CARGADATAVIEW(ByVal GRUPO As String)
        DV = New DataView(frmPrincipal.DTPLATILLOSEXTRA, "PRODUCTO='" + GRUPO + "'", "ORDEN", DataViewRowState.CurrentRows)
        'DGV2.DataSource = DT
        CARGAPLATILLOS()
    End Sub
    Private Sub CARGAPLATILLOS()
        LEXT.Clear()
        LNOM.Clear()
        LPRE.Clear()

        Dim X As Integer
        Dim DRV As DataRowView
        For X = 0 To DV.Count - 1
            DRV = DV.Item(X)
            LEXT.Add(DRV.Item(1))
            LNOM.Add(DRV.Item(2))
            LPRE.Add(DRV.Item(3))

        Next
        INDG = 1

        ACTUALIZAP()
    End Sub
    Private Sub ACTUALIZAP()
        If INDG > 1 Then
            BTNANTG.Enabled = True
        Else
            BTNANTG.Enabled = False
        End If

        'Dim MAXINDG As Integer
        'MAXINDG = CType((LGRU.Count / 10), Integer)
        If ((INDG - 1) * NCUAG + NCUAG) < LEXT.Count Then
            BTNSIGG.Enabled = True
        Else
            BTNSIGG.Enabled = False
        End If
        ACOMODAPLATILLOS()
    End Sub
    Dim INICIO As Integer
    Dim FIN As Integer
    Dim FF As Integer
    Dim UBI As String
    Private Sub ACOMODAPLATILLOS()
        INICIO = (INDG - 1) * NCUAG
        FIN = INICIO + NCUAG
        If FIN > LEXT.Count Then
            FIN = LEXT.Count
        Else
        End If
        FF = FIN
        While FF > NCUAG
            FF = FF - NCUAG
        End While
        For X = 1 To NCUAG
            ''VISIBLE=TRUE
            PGRU(X).Visible = True
            LBLPRE(X).Visible = True
            PGRU(X).Image = PBIMG.Image
        Next
        UBI = ""
        For X = 1 To FF
            'UBI = IPRO(INICIO + X - 1)
            UBI = LEXT(INICIO + X - 1).ToString
            LBLPRE(X).Text = LPRE(INICIO + X - 1)
            PGRU(X).ImageLocation = "C:/FOTOSMAGOS/" + UBI + ".JPG"
            PGRU(X).SizeMode = PictureBoxSizeMode.StretchImage
        Next
        If FF < NCUAG Then
            For X = FF + 1 To NCUAG
                PGRU(X).Visible = False
                LBLPRE(X).Visible = False
                LBLPRE(X).Text = ""
            Next
        Else
        End If
    End Sub
    Private Sub INICIALIZAR()
        For X = 1 To 10
            PGRU(X) = New PictureBox
            LBLPRE(X) = New Label
        Next
        PGRU(1) = PB1
        PGRU(2) = PB2
        PGRU(3) = PB3
        PGRU(4) = PB4
        PGRU(5) = PB5
        PGRU(6) = PB6
        PGRU(7) = PB7
        PGRU(8) = PB8
        PGRU(9) = PB9

        LBLPRE(1) = LBLP1
        LBLPRE(2) = LBLP2
        LBLPRE(3) = LBLP3
        LBLPRE(4) = LBLP4
        LBLPRE(5) = LBLP5
        LBLPRE(6) = LBLP6
        LBLPRE(7) = LBLP7
        LBLPRE(8) = LBLP8
        LBLPRE(9) = LBLP9

    End Sub
    Private Sub LLENARLB()
        TOT = 0
        FLP.Controls.Clear()
        Dim X As Integer
        For X = 0 To LELE.Count - 1
            Me.FLP.Controls.Add(CREAELEMENTO(LELE(X).Nombre, LELE(X).Precio.ToString))
            TOT += LELE(X).Precio
        Next

        LBLTA.Text = "$ " + FormatNumber(TOT, 2).ToString
    End Sub

    Private Sub BTNSIGG_Click(sender As Object, e As EventArgs) Handles BTNSIGG.Click
        INDG = INDG + 1
        ACTUALIZAP()
    End Sub

    Private Sub BTNANTG_Click(sender As Object, e As EventArgs) Handles BTNANTG.Click
        INDG = INDG - 1
        ACTUALIZAP()
    End Sub

    Private Sub BTNVOLVER_Click(sender As Object, e As EventArgs) Handles BTNVOLVER.Click
        Me.Close()
    End Sub

    Private Sub PB1_Click(sender As Object, e As EventArgs) Handles PB1.Click, PB2.Click, PB3.Click, PB4.Click, PB5.Click, PB6.Click, PB7.Click, PB8.Click, PB9.Click
        Dim POS As Integer
        POS = (INDG - 1) * NCUAG + CType(sender.TAG, Integer) - 1
        Dim ELE As New clsExtra
        ELE.Clave = LEXT(POS)
        ELE.Nombre = LNOM(POS)
        ELE.Precio = LPRE(POS)
        LELE.Add(ELE)
        LLENARLB()
    End Sub

    Private Sub BTNOK_Click(sender As Object, e As EventArgs) Handles BTNOK.Click
        If LELE.Count <= 0 Then
        Else
            If VIDCOMBO = 0 Then
                VIDCOMBO = frmPrincipal.PRE.UltimoCombo + 1
            End If
            frmPrincipal.PRE.ActualizarCombo(VIDREG, VIDCOMBO)
            Dim X As Integer
            For X = 0 To LELE.Count - 1
                frmPrincipal.PRE.Agregar(LELE(X).Clave, LELE(X).Nombre, 1, LELE(X).Precio, 0, LELE(X).Precio, VIDCOMBO)
            Next
        End If
        Me.Close()
    End Sub

    Private Sub BTNQUITAR_Click(sender As Object, e As EventArgs) Handles BTNQUITAR.Click
        If LELE.Count <= 0 Then
            Exit Sub
        Else
            LELE.RemoveAt(LELE.Count - 1)
            LLENARLB()
        End If
    End Sub
End Class