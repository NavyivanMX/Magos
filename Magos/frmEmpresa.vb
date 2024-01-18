Public Class frmEmpresa
    Inherits System.Windows.Forms.Form
    ''---> PROGRAMALO PARA K CON ENTER SE PASE AL SIGUIENTE... PERO NOOOOOO COMO MI ABUELA...
    Private Sub BTNGUARDAR_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.MouseEnter
        BTNGUARDAR.BackColor = System.Drawing.Color.Olive
        BTNGUARDAR.Text = "GUARDAR"
        BTNGUARDAR.ForeColor = System.Drawing.Color.Green
    End Sub

    Private Sub BTNGUARDAR_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.MouseLeave
        BTNGUARDAR.BackColor = System.Drawing.Color.Transparent
        BTNGUARDAR.Text = ""
        BTNGUARDAR.ForeColor = System.Drawing.Color.White
    End Sub

    Private Sub frmEmpresa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        CARGARDATOS()
    End Sub
    Private Sub CARGARDATOS()
        frmPrincipal.CHECACONX()
        Dim SQLSELECT As New SqlClient.SqlCommand("SELECT CLAVE,NOMBREFISCAL,NOMBRECOMUN,DIRECCION,CP,CIUDAD,RFC,TELEFONO,FAX,EMAIL,PAGOTARJETA,COMENTARIO1,COMENTARIO2,COMENTARIO3,COMENTARIO4,COMENTARIO5,COMENTARIO6,COMENTARIO7,COMENTARIO8,COMENTARIO9,COMENTARIO10,COMENTARIO11,COMENTARIO12,REGIMEN FROM SUCURSALES WHERE CLAVE='" + frmPrincipal.SucursalBase + "'", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLSELECT.ExecuteReader
        If LECTOR.Read Then
            TXTNF.Text = LECTOR(1)
            TXTNOM.Text = LECTOR(2)
            TXTDIR.Text = LECTOR(3)
            TXTCP.Text = LECTOR(4)
            TXTCD.Text = LECTOR(5)
            TXTRFC.Text = LECTOR(6)
            TXTTEL.Text = LECTOR(7)
            TXTFAX.Text = LECTOR(8)
            TXTMAIL.Text = LECTOR(9)
            If CType(LECTOR(10), Boolean) Then
                CBPT.SelectedIndex = 0
            Else
                CBPT.SelectedIndex = 1
            End If
            TXTC1.Text = LECTOR(11)
            TXTC2.Text = LECTOR(12)
            TXTC3.Text = LECTOR(13)
            TXTC4.Text = LECTOR(14)
            TXTC5.Text = LECTOR(15)
            TXTC6.Text = LECTOR(16)
            TXTC7.Text = LECTOR(17)
            TXTC8.Text = LECTOR(18)
            TXTC9.Text = LECTOR(19)
            TXTC10.Text = LECTOR(20)
            TXTC11.Text = LECTOR(21)
            TXTC12.Text = LECTOR(22)
            TXTRFIS.Text = LECTOR(23)
        End If
        LECTOR.Close()
    End Sub
    Private Sub GUARDARDATOS()
        Dim SQLUPDATE As New SqlClient.SqlCommand("SPSUCURSALES", frmPrincipal.CONX)
        SQLUPDATE.CommandType = CommandType.StoredProcedure
        SQLUPDATE.Parameters.Add("@CLA", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
        SQLUPDATE.Parameters.Add("@NF", SqlDbType.VarChar).Value = TXTNF.Text
        SQLUPDATE.Parameters.Add("@NOM", SqlDbType.VarChar).Value = TXTNOM.Text
        SQLUPDATE.Parameters.Add("@DIR", SqlDbType.VarChar).Value = TXTDIR.Text
        SQLUPDATE.Parameters.Add("@CD", SqlDbType.VarChar).Value = TXTCD.Text
        SQLUPDATE.Parameters.Add("@RFC", SqlDbType.VarChar).Value = TXTRFC.Text
        SQLUPDATE.Parameters.Add("@CP", SqlDbType.VarChar).Value = TXTCP.Text
        SQLUPDATE.Parameters.Add("@TEL", SqlDbType.VarChar).Value = TXTTEL.Text
        SQLUPDATE.Parameters.Add("@FAX", SqlDbType.VarChar).Value = TXTFAX.Text
        SQLUPDATE.Parameters.Add("@MAIL", SqlDbType.VarChar).Value = TXTMAIL.Text
        SQLUPDATE.Parameters.Add("@C1", SqlDbType.VarChar).Value = TXTC1.Text
        SQLUPDATE.Parameters.Add("@C2", SqlDbType.VarChar).Value = TXTC2.Text
        SQLUPDATE.Parameters.Add("@C3", SqlDbType.VarChar).Value = TXTC3.Text
        SQLUPDATE.Parameters.Add("@C4", SqlDbType.VarChar).Value = TXTC4.Text
        SQLUPDATE.Parameters.Add("@C5", SqlDbType.VarChar).Value = TXTC5.Text
        SQLUPDATE.Parameters.Add("@C6", SqlDbType.VarChar).Value = TXTC6.Text
        SQLUPDATE.Parameters.Add("@C7", SqlDbType.VarChar).Value = TXTC7.Text
        SQLUPDATE.Parameters.Add("@C8", SqlDbType.VarChar).Value = TXTC8.Text
        SQLUPDATE.Parameters.Add("@C9", SqlDbType.VarChar).Value = TXTC9.Text
        SQLUPDATE.Parameters.Add("@C10", SqlDbType.VarChar).Value = TXTC10.Text
        SQLUPDATE.Parameters.Add("@C11", SqlDbType.VarChar).Value = TXTC11.Text
        SQLUPDATE.Parameters.Add("@C12", SqlDbType.VarChar).Value = TXTC12.Text
        SQLUPDATE.Parameters.Add("@REGI", SqlDbType.VarChar).Value = TXTRFIS.Text

        If CBPT.SelectedIndex = 0 Then
            SQLUPDATE.Parameters.Add("@PT", SqlDbType.Bit).Value = 1
        Else
            SQLUPDATE.Parameters.Add("@PT", SqlDbType.Bit).Value = 0
        End If

        SQLUPDATE.ExecuteNonQuery()
        MessageBox.Show("Los datos han sido guardados exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub

    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        GUARDARDATOS()
    End Sub

    Private Sub TXTNF_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTNF.KeyPress, TXTNOM.KeyPress, TXTDIR.KeyPress, TXTRFC.KeyPress, TXTCP.KeyPress, TXTCD.KeyPress, TXTTEL.KeyPress, TXTFAX.KeyPress, TXTMAIL.KeyPress, TXTC1.KeyPress, TXTC2.KeyPress, TXTC3.KeyPress, TXTC4.KeyPress, TXTC5.KeyPress, TXTC6.KeyPress, TXTC7.KeyPress, TXTC8.KeyPress, TXTC9.KeyPress, TXTC10.KeyPress, TXTC11.KeyPress, TXTC12.KeyPress, TXTRFIS.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmEmpresa_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Alt + Keys.G Then
            GUARDARDATOS()
        End If

    End Sub
End Class