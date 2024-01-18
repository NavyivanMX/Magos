Public Class frmArmaCombo
 
    Dim SUBT As Double
    Dim DESC As Double
    Dim PRE(12) As Label
    Dim PLA(12) As PictureBox
    Dim QUI(12) As PictureBox
    Dim TIPO(12) As Integer
    Dim PROOPC(12) As String
    Dim NOPC(12) As String
    Dim CANTOPC(12) As Double
    Dim TOTOPC(12) As Double
    Dim INCOPC(12) As Boolean
    Private Sub frmArmaCombo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        'VISUALIZACION(Me)
        'PBA1.Controls.Add(PBQA1)
        'PBA1.Controls.Add(LBLA1)
        'PBA2.Controls.Add(PBQA2)
        'PBA2.Controls.Add(LBLA2)
        'PBA3.Controls.Add(PBQA3)
        'PBA3.Controls.Add(LBLA3)
        'PBA4.Controls.Add(PBQA4)
        'PBA4.Controls.Add(LBLA4)
        'PBA5.Controls.Add(PBQA5)
        'PBA5.Controls.Add(LBLA5)
        'PBA6.Controls.Add(PBQA6)
        'PBA6.Controls.Add(LBLA6)
        'PBA7.Controls.Add(PBQA7)
        'PBA7.Controls.Add(LBLA7)
        'PBA8.Controls.Add(PBQA8)
        'PBA8.Controls.Add(LBLA8)
        'PBA9.Controls.Add(PBQA9)
        'PBA9.Controls.Add(LBLA9)
        'PBA10.Controls.Add(PBQA10)
        'PBA10.Controls.Add(LBLA10)
        Me.BackgroundImage = Magos.My.Resources.Resources.FondoVentanas21


        PBQA1.BringToFront()
        PBQA2.BringToFront()
        PBQA3.BringToFront()
        PBQA4.BringToFront()
        PBQA5.BringToFront()
        PBQA6.BringToFront()
        PBQA7.BringToFront()
        PBQA8.BringToFront()
        PBQA9.BringToFront()
        PBQA10.BringToFront()



        LBLA1.BringToFront()
        LBLA2.BringToFront()
        LBLA3.BringToFront()
        LBLA4.BringToFront()
        LBLA5.BringToFront()
        LBLA6.BringToFront()
        LBLA7.BringToFront()
        LBLA8.BringToFront()
        LBLA9.BringToFront()
        LBLA10.BringToFront()
        INFOPRODUCTO()
        INICIALIZAR()
        ACTUALIZAR()
    End Sub
    Dim PPRIN, NPRIN As String
    Dim CPRIN As Double
    Dim AMAX, AMIN, BMAX, BMIN, OPCMAX As Integer
    Dim PRECIO, DESCUENTO As Double
    Private Sub INFOPRODUCTO()
        Dim X As Integer
        AMAX = 0
        BMAX = 0
        For X = 0 To frmPrincipal.DTCOMBOPAQ.Rows.Count - 1
            If frmPrincipal.DTCOMBOPAQ.Rows(X).Item(0).ToString = PPRIN Then
                NPRIN = frmPrincipal.DTCOMBOPAQ.Rows(X).Item(1).ToString
                AMIN = frmPrincipal.DTCOMBOPAQ.Rows(X).Item(2).ToString
                AMAX = frmPrincipal.DTCOMBOPAQ.Rows(X).Item(3).ToString
                BMIN = frmPrincipal.DTCOMBOPAQ.Rows(X).Item(4).ToString
                BMAX = frmPrincipal.DTCOMBOPAQ.Rows(X).Item(5).ToString
                PRECIO = frmPrincipal.DTCOMBOPAQ.Rows(X).Item(6).ToString
                DESCUENTO = frmPrincipal.DTCOMBOPAQ.Rows(X).Item(7).ToString

            End If
        Next
        OPCMAX = AMAX + BMAX
        For X = 1 To AMAX
            TIPO(X) = 1
        Next
        For X = 1 To BMAX
            TIPO(X + AMAX) = 2
        Next
        LBLNCP.Text = NPRIN
        LBLTOT.Text = FormatNumber((CPRIN * PRECIO) * (1 - (DESCUENTO / 100)), 2).ToString
    End Sub
    Private Function VALIDAR() As Boolean
        Dim SA, SB As Integer
        SA = 0
        SB = 0
        Dim X As Integer
        For X = 1 To OPCMAX
            If PROOPC(X) = "" Then
            Else
                If TIPO(X) = 1 Then
                    SA += 1
                Else
                    SB += 1
                End If                
            End If
        Next
        If SA >= AMIN And SB >= BMIN Then
            Return True
        End If
        Return False
    End Function
    Private Sub PBOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBOK.Click
        'VALIDAR
        If Not VALIDAR() Then
            Exit Sub
        End If
        ' AGREGAR
        Dim idCombo As Integer
        idCombo = frmPrincipal.PRE.UltimoCombo + 1

        frmPrincipal.PRE.Agregar(PPRIN, NPRIN, CPRIN, PRECIO, DESCUENTO, PRECIO, idCombo, 0, 0)
        Dim X As Integer
        For X = 1 To OPCMAX
            If PROOPC(X) = "" Then
            Else
                frmPrincipal.PRE.Agregar(PROOPC(X), NOPC(X), CANTOPC(X), TOTOPC(X) / CANTOPC(X), 0, TOTOPC(X) / CANTOPC(X), idCombo, 0, 0)
            End If
        Next

        Me.Close()
    End Sub
    Public Sub MOSTRAR(ByVal CANT As Double, ByVal PRODUCTO As String)
        CPRIN = CANT
        PPRIN = PRODUCTO
        Me.ShowDialog()
    End Sub

    Public Sub INICIALIZAR()
        Dim X As Integer
        For X = 1 To 10
            QUI(X) = New PictureBox
            PLA(X) = New PictureBox
            PRE(X) = New Label
            PROOPC(X) = ""
            CANTOPC(X) = 0
            NOPC(X) = ""
            TOTOPC(X) = 0
            INCOPC(X) = False

        Next
        QUI(1) = PBQA1
        QUI(2) = PBQA2
        QUI(3) = PBQA3
        QUI(4) = PBQA4
        QUI(5) = PBQA5
        QUI(6) = PBQA6
        QUI(7) = PBQA7
        QUI(8) = PBQA8
        QUI(9) = PBQA9
        QUI(10) = PBQA10

        PLA(1) = PBA1
        PLA(2) = PBA2
        PLA(3) = PBA3
        PLA(4) = PBA4
        PLA(5) = PBA5
        PLA(6) = PBA6
        PLA(7) = PBA7
        PLA(8) = PBA8
        PLA(9) = PBA9
        PLA(10) = PBA10

        PRE(1) = LBLA1
        PRE(2) = LBLA2
        PRE(3) = LBLA3
        PRE(4) = LBLA4
        PRE(5) = LBLA5
        PRE(6) = LBLA6
        PRE(7) = LBLA7
        PRE(8) = LBLA8
        PRE(9) = LBLA9
        PRE(10) = LBLA10

        For X = OPCMAX + 1 To 10
            PRE(X).Visible = False
            PLA(X).Visible = False
            QUI(X).Visible = False
        Next
    End Sub

    Private Sub LBLNCP_Click(sender As Object, e As EventArgs) Handles LBLNCP.Click

    End Sub

    Private Sub ACTUALIZAR()
        Dim ACU As Double
        ACU = 0
        For X = 1 To OPCMAX
            If INCOPC(X) Then
            Else
                ACU += (CPRIN * CANTOPC(X) * TOTOPC(X))
            End If

            If PROOPC(X) = "" Then
                PRE(X).Visible = False
                QUI(X).Visible = False
                If TIPO(X) = 1 Then
                    PLA(X).Image = PBBA.Image
                Else
                    PLA(X).Image = PBBB.Image
                End If
            Else
                PRE(X).Visible = True
                PRE(X).BringToFront()
                PRE(X).Text = "$" + FormatNumber(TOTOPC(X), 2).ToString
                QUI(X).Visible = True
                QUI(X).BringToFront()
                PLA(X).ImageLocation = "C:/FOTOSMAGOS/" + PROOPC(X) + ".JPG"
            End If
        Next

        LBLTOT.Text = FormatNumber(((CPRIN * PRECIO) + ACU), 2).ToString

        LBLTOT.Text = FormatNumber(((CPRIN * PRECIO) + ACU) * (1 - (DESCUENTO / 100)), 2).ToString
    End Sub
    Dim UPOS As Integer
    Private Sub PBA1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBA1.Click, PBA2.Click, PBA3.Click, PBA4.Click, PBA5.Click, PBA6.Click, PBA7.Click, PBA8.Click, PBA9.Click, PBA10.Click

        '' SELECCIONAR AGREGADO
        Dim POS As Integer
        POS = CType(sender.TAG, Integer)
        UPOS = POS
        frmHCMP.CPLA = PPRIN
        frmHCMP.POS = POS
        frmHCMP.CARGAAGREGADOBEBIDA(TIPO(POS))

    End Sub
    Public Sub CARGAMENUPRINCIPAL()
        PROOPC(UPOS) = frmHCMP.C
        CANTOPC(UPOS) = frmHCMP.U * CPRIN
        TOTOPC(UPOS) = frmHCMP.P
        NOPC(UPOS) = frmHCMP.N
        Me.BringToFront()
        ACTUALIZAR()
    End Sub
    Private Sub PBQA1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBQA1.Click, PBQA2.Click, PBQA3.Click, PBQA4.Click, PBQA5.Click, PBQA6.Click, PBQA7.Click, PBQA8.Click, PBQA9.Click, PBQA10.Click
        ''QUITAR AGREGADO
        Dim POS As Integer
        POS = CType(sender.TAG, Integer)

        PROOPC(POS) = ""
        CANTOPC(POS) = 0
        TOTOPC(POS) = 0
        INCOPC(POS) = False

        ACTUALIZAR()
    End Sub



    Private Sub PBCANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBCANCEL.Click
        Me.Close()
    End Sub

 
End Class