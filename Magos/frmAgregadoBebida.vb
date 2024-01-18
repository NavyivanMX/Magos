Public Class frmAgregadoBebida
    Dim LMP As New List(Of String)
    Dim NMP As New List(Of String)
    Dim UMP As New List(Of String)
    Dim TMP As New List(Of String)
    Dim PMP As New List(Of String)

    'Dim MINA As New List(Of String)
    'Dim MINB As New List(Of String)
    'Dim MAXA As New List(Of String)
    'Dim MAXB As New List(Of String)
    'Dim ACMB As New List(Of Boolean)
    'Dim DESC As New List(Of String)


    Dim DV As New DataView
    Public IMP As Integer
    Dim DT As New DataTable
    Dim NCUA As Integer
    Dim PBMP(10) As PictureBox
    Dim LBLP(10) As Label
    Private Sub frmAgregadoBebida_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NCUA = 9
        INICIALIZAR()
    End Sub
    Private Sub INICIALIZAR()
        Dim X As Integer
        For X = 0 To NCUA
            PBMP(X) = New PictureBox
            LBLP(X) = New Label
        Next
        Me.PBMP(1) = Me.PB1
        Me.PBMP(2) = Me.PB2
        Me.PBMP(3) = Me.PB3
        Me.PBMP(4) = Me.PB4
        Me.PBMP(5) = Me.PB5
        Me.PBMP(6) = Me.PB6
        Me.PBMP(7) = Me.PB7
        Me.PBMP(8) = Me.PB8
        Me.PBMP(9) = Me.PB9

        Me.LBLP(1) = Me.LBLP1
        Me.LBLP(2) = Me.LBLP2
        Me.LBLP(3) = Me.LBLP3
        Me.LBLP(4) = Me.LBLP4
        Me.LBLP(5) = Me.LBLP5
        Me.LBLP(6) = Me.LBLP6
        Me.LBLP(7) = Me.LBLP7
        Me.LBLP(8) = Me.LBLP8
        Me.LBLP(9) = Me.LBLP9

        For X = 0 To NCUA
            PBMP(X).Visible = False
            LBLP(X).Visible = False
        Next
    End Sub

    Public Sub CARGAMENUPRINCIPAL(ByVal TIPO As String)
        Dim MENU As String
        MENU = frmHCMP.CMP
        Dim i As Integer = 1
        Do While (i <= NCUA)
            Me.PBMP(i).Visible = False
            Me.LBLP(i).Visible = False
            i += 1
        Loop
        LMP.Clear()
        NMP.Clear()
        UMP.Clear()
        TMP.Clear()
        PMP.Clear()
        DV = New DataView(frmPrincipal.DTCOMBOPAQD, ("TIPO=" & TIPO + " AND PRODUCTO='" + frmHCMP.CPLA + "'"), "NOMBRE", DataViewRowState.CurrentRows)

        For X = 0 To DV.Count - 1
            Dim view As DataRowView = Me.DV.Item(X)
            LMP.Add(view(2))
            NMP.Add(view(3))
            UMP.Add(view(4))
            PMP.Add(view(5))
        Next
        IMP = 1
        If TIPO = "1" Then
            frmHCMP.LBLMP.Text = "Selecciona el agregado"
        Else
            frmHCMP.LBLMP.Text = "Selecciona la bebida"
        End If

        ACTUALIZAMENUPRINCIPAL()
    End Sub
    Public Sub ACTUALIZAMENUPRINCIPAL()
        If (Me.IMP > 1) Then
            frmHCMP.BTNANT.Visible = True
        Else
            frmHCMP.BTNANT.Visible = False
        End If


        'Dim num As Integer = CInt(Math.Round(CDbl((CDbl(Me.LMP.Count) / CDbl(Me.NCUA)))))
        If ((((Me.IMP - 1) * Me.NCUA) + Me.NCUA) < Me.LMP.Count) Then
            frmHCMP.BTNSIG.Visible = True
        Else
            frmHCMP.BTNSIG.Visible = False
        End If
        Me.ACOMODAMENUPRINCIPAL()
    End Sub

    Private Sub ACOMODAMENUPRINCIPAL()
        Dim INI, FIN As Integer
        INI = ((Me.IMP - 1) * Me.NCUA)
        FIN = INI + Me.NCUA

        If (FIN > Me.LMP.Count) Then
            FIN = Me.LMP.Count
        End If


        For X = 1 To NCUA
            Me.PBMP(X).Visible = False
            Me.LBLP(X).Visible = False
        Next
        For X = 1 To FIN - INI
            Me.PBMP(X).Visible = True
            Me.LBLP(X).Visible = True
        Next

        ''PONER IMAGENES
        Dim UBI As String
        'Dim UBIFIN As String
        For X = 0 To FIN - INI - 1
            UBI = Me.LMP.Item(((X + INI)))
            Me.PBMP(X + 1).ImageLocation = ("C:/FOTOSMAGOS/" & UBI & ".jpg")
            LBLP(X + 1).Text = "$" + FormatNumber(PMP(X + INI)).ToString
            'UBIFIN = frmPrincipal.UBIBASE & "/SELECCION/" & UBI & ".jpg"
        Next
    End Sub

    Private Sub PB1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PB1.Click, PB2.Click, PB3.Click, PB4.Click, PB5.Click, PB6.Click, PB7.Click, PB8.Click, PB9.Click
        Dim POS As Integer
        POS = (NCUA * (IMP - 1)) + (CType(sender.TAG, Integer) - 1)
        frmHCMP.C = LMP(POS)
        frmHCMP.N = NMP(POS)
        frmHCMP.U = UMP(POS)
        frmHCMP.P = PMP(POS)
        frmHCMP.OS = True
        frmArmaCombo.CARGAMENUPRINCIPAL()
        frmArmaCombo.BringToFront()
        Me.Close()
    End Sub
End Class