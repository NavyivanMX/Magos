Public Class frmCambioIP

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If String.IsNullOrEmpty(TXTIP.Text) Then
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            TXTIP.Focus()
            Exit Sub
        End If
        If TXTPWD.Text.ToUpper = "TRIUNFO" Then
            frmPrincipal.CadenaConexion = "Data Source=" + TXTIP.Text + ",1433;Network Library=DBMSSOCN;Initial Catalog=CHINALOA;User ID=dbaadmin;Password=masterkey"
            frmPrincipal.IP = TXTIP.Text
            frmPrincipal.CONX.ConnectionString = frmPrincipal.CadenaConexion
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            Me.Close()
        Else
            TXTPWD.Focus()
            TXTPWD.SelectAll()
        End If
    End Sub

    Private Sub frmCambioIP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
    End Sub
End Class