Public Class frmCambio

    Private Sub BTNACEPTAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNACEPTAR.Click
        Me.Close()
    End Sub
    Public Sub CAMBIO(ByVal CMB As Double)
        Label1.Text = "$ " + FormatNumber(CMB, 2).ToString
        Me.ShowDialog()
    End Sub

    Private Sub frmCambio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        Me.StartPosition = FormStartPosition.Manual
    End Sub
End Class