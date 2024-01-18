Public Class frmPagos
    Dim LTP As New List(Of String)
    Dim TOT As Double
    Dim LBAN As New List(Of String)
    Public CWA, COYR, FF As Boolean
    Public BANCO, DIGITOS, AUTORIZACION, TIPOTARJETA, EFECTIVO, CAMBIO, TIPOPAGO As String
    Private Sub frmPagos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        TXTEFECTIVO.Text = ""
        TXTEFECTIVO.Focus()
        FA = True
        FB = False
        FC = False
        TA = False
        TB = False
        TC = False
        CHECAFOCUS()
        TXTEFECTIVO.Focus()
    End Sub
    Public Sub MOSTRAR(ByVal VTOT As Double, ByVal VLTP As List(Of String), ByRef VCB As ComboBox, ByVal VLBAN As List(Of String), ByRef VCBB As ComboBox)
        LTP = VLTP
        LBAN = VLBAN
        TOT = VTOT
        TXTEFECTIVO.Text = ""
        TXTTOT.Text = FormatNumber(TOT, 2).ToString
        CBTP.Items.Clear()
        CBBANCO.Items.Clear()
        Dim X As Integer
        For X = 0 To VCB.Items.Count - 1
            CBTP.Items.Add(VCB.Items(X))
        Next

        For X = 0 To VCBB.Items.Count - 1
            CBBANCO.Items.Add(VCBB.Items(X))
        Next
        Try
            CBTP.SelectedIndex = 0
            CBBANCO.SelectedIndex = 0
        Catch ex As Exception

        End Try
        TXTEFECTIVO.Focus()
        TXTEFECTIVO.SelectAll()
        CHECACANTIDADES()
        TXTEFECTIVO.Focus()
        Me.ShowDialog()
    End Sub

    Private Sub BTNCOBRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCOBRAR.Click
        Dim TP As Integer
        TP = LTP(CBTP.SelectedIndex)
        TIPOTARJETA = "0"
        If TP = 2 Or TP = 3 Then
            If frmPrincipal.PagoTarjeta = False Then
                MessageBox.Show("Por el momento el pago con tarjeta ha sido desactivado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            Else
                If String.IsNullOrEmpty(TXTAUT.Text) Then
                    MessageBox.Show("No. AutorizaciDón No Especificado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    TXTAUT.Focus()
                    Exit Sub
                End If
                If String.IsNullOrEmpty(TXTDIG.Text) Then
                    MessageBox.Show("Digitos No Especificado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    TXTAUT.Focus()
                    Exit Sub
                End If
            End If
        End If
        DIGITOS = TXTDIG.Text
        AUTORIZACION = TXTAUT.Text
        BANCO = "0"
        If TP = 2 Or TP = 3 Or TP = 4 Or TP = 5 Then
            BANCO = LBAN(CBBANCO.SelectedIndex)
        End If

        If TP = 2 Or TP = 3 Then
            If frmPrincipal.PagoTarjeta = False Then
                MessageBox.Show("Por el momento el pago con tarjeta ha sido desactivado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            Else
                If String.IsNullOrEmpty(TXTAUT.Text) Then
                    MessageBox.Show("No. Autorización No Especificado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    TXTAUT.Focus()
                    Exit Sub
                End If
            End If
        End If
        FF = True
        CWA = True
        COYR = True

        EFECTIVO = CType(TXTEFECTIVO.Text, Double).ToString
        CAMBIO = CType(TXTCAMBIO.Text, Double).ToString
        TIPOPAGO = TP

        Me.DialogResult = Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub
    Private Function CHECACANTIDADES() As Boolean
        'If DESDECLIENTEFRECUENTE Then
        '    'TXTEFECTIVO.Text = TOTALCLIENTEFRECUENTE.ToString
        '    TXTTOT.Text = TOTALCLIENTEFRECUENTE.ToString
        '    TXTCAMBIO.Text = FormatNumber(CType(TXTEFECTIVO.Text, Double) - CType(TXTTOT.Text, Double)).ToString()
        '    Return True
        'End If
        BTNCOBRAR.Enabled = False
        TXTCAMBIO.Text = "0"
        Dim TOTAL, EFECTIVO As Double
        Try
            TOTAL = CType(TXTTOT.Text, Double)
            EFECTIVO = CType(TXTEFECTIVO.Text, Double)
        Catch ex As Exception
            Return False
            Exit Function
        End Try
        If EFECTIVO >= TOTAL Then
            BTNCOBRAR.Enabled = True
            TXTCAMBIO.Text = FormatNumber(EFECTIVO - TOTAL).ToString()
            Return True
        Else
            TXTCAMBIO.Text = 0
            Return False
        End If
        BTNCOBRAR.Enabled = True
    End Function

    Private Sub TXTEFECTIVO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTEFECTIVO.KeyPress

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        If Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        End If
        CHECACANTIDADES()
        If e.KeyChar = Chr(13) Then
            BTNCOBRAR.Focus()
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.No
        Me.Close()
    End Sub
    Dim FA, FB, FC, TA, TB, TC As Boolean
    Private Function CHECAFOCUS() As Boolean
        TXTEFECTIVO.BackColor = Color.White
        TXTDIG.BackColor = Color.White
        TXTAUT.BackColor = Color.White
        If FA Then
            TXTEFECTIVO.BackColor = Color.PaleTurquoise
            TXTEFECTIVO.Focus()            
        End If
        If FB Then
            TXTDIG.BackColor = Color.PaleTurquoise
            TXTDIG.Focus()
        End If
        If FC Then
            TXTAUT.Focus()
            TXTAUT.BackColor = Color.PaleTurquoise
        End If
    End Function
    Private Sub BTN1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN1.Click, BTN2.Click, BTN3.Click, BTN4.Click, BTN5.Click, BTN6.Click, BTN7.Click, BTN8.Click, BTN9.Click, BTN0.Click, BTNPUNTO.Click
        MODIFICACANTIDAD(CType(sender.TAG, String))
        CHECACANTIDADES()
    End Sub

    Private Sub MODIFICACANTIDAD(ByVal NUM As String)
        If FA Then
            If TA Then
                TXTEFECTIVO.Text += NUM
            Else
                TXTEFECTIVO.Text = NUM
                TA = True
            End If
        End If
        If FB Then
            If NUM = "." Then
                Exit Sub
            End If
            If TB Then
                TXTDIG.Text += NUM
            Else
                TXTDIG.Text = NUM
                TB = True
            End If
        End If
        If FC Then
            If NUM = "." Then
                Exit Sub
            End If
            If TC Then
                TXTAUT.Text += NUM
            Else
                TXTAUT.Text = NUM
                TC = True
            End If
        End If
    End Sub

    Private Sub TXTEFECTIVO_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTEFECTIVO.Enter

        If Not FA Then
            TA = False
        End If
        FA = True
        FB = False
        FC = False
        CHECAFOCUS()
    End Sub

    Private Sub TXTDIG_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTDIG.Enter
        If Not FB Then
            TB = False
        End If
        FB = True
        FA = False
        FC = False
        CHECAFOCUS()
    End Sub

    Private Sub TXTAUT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTAUT.Enter
        If Not FC Then
            TC = False
        End If
        FC = True
        FB = False
        FA = False
        CHECAFOCUS()
    End Sub

    Private Sub CBTP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBTP.SelectedIndexChanged
        Dim TP As Integer
        TP = LTP(CBTP.SelectedIndex)
        Label15.Visible = False
        Label16.Visible = False
        LBLAUT.Visible = False
        'GB.Visible = False
        TXTAUT.Visible = False
        TXTDIG.Visible = False
        CBBANCO.Visible = False
        If TP <> 1 Then
            TXTEFECTIVO.Enabled = False
            TXTEFECTIVO.Text = TXTTOT.Text
        Else
            TXTEFECTIVO.Enabled = True
        End If
        If TP = 2 Or TP = 3 Then
            Label15.Visible = True
            Label16.Visible = True
            LBLAUT.Visible = True
            TXTAUT.Visible = True
            TXTDIG.Visible = True
            CBBANCO.Visible = True
            'GB.Visible = True
        End If
        If TP = 4 Then
            Label15.Visible = True
            Label16.Visible = True
            TXTDIG.Visible = True
            CBBANCO.Visible = True
        End If
        If TP = 5 Then
            Label15.Visible = True
            Label16.Visible = True
            TXTDIG.Visible = True
            CBBANCO.Visible = True
        End If
        CHECACANTIDADES()

    End Sub

    Private Sub BTNCANCELAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        If FA Then
            TXTEFECTIVO.Text = ""
            TXTEFECTIVO.Focus()
        End If
        If FB Then
            TXTDIG.Text = ""
            TXTDIG.Focus()
        End If
        If FC Then
            TXTAUT.Text = ""
            TXTAUT.Focus()
        End If
        CHECACANTIDADES()
    End Sub

    Private Sub TXTEFECTIVO_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTEFECTIVO.TextChanged
        CHECACANTIDADES()
    End Sub
End Class