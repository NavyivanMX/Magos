Public Class frmCajeras
    Dim PWD As String
    Private Sub frmCajeras_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        NUEVO(True)
        ACTIVAR(True)
    End Sub
    Private Sub NUEVO(ByVal V As Boolean)
        TXTANT.Text = ""
        TXTNVA.Text = ""
        TXTPWD.Text = ""
        LBLANT.Visible = Not V
        TXTANT.Visible = Not V
        LBLNVA.Visible = Not V
        TXTNVA.Visible = Not V
        If V Then
            LBLPWD.Text = "Contraseña"
            TXTPWD.Focus()
        Else
            LBLPWD.Text = "Repetir Contraseña"
            TXTANT.Focus()
        End If
    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        TXTEMP.Enabled = V
        TXTNVA.Enabled = Not V
        TXTANT.Enabled = Not V
        BTNGUARDAR.Enabled = Not V
        TXTPWD.Enabled = Not V
        If V Then
            TXTEMP.Focus()
            TXTEMP.SelectAll()
        End If
    End Sub
    Private Sub CARGAR()
        If CHECAEMPLEADO() Then
            If Not frmPrincipal.CHECACONX() Then
                Exit Sub
            End If
            Dim SQL As New SqlClient.SqlCommand("SELECT PWD FROM CAJERAS WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "'AND EMPLEADO=" + TXTEMP.Text, frmPrincipal.CONX)
            Dim LECTOR As SqlClient.SqlDataReader
            LECTOR = SQL.ExecuteReader
            PWD = ""
            If LECTOR.Read Then
                PWD = LECTOR(0)
                LECTOR.Close()
                ACTIVAR(False)
                NUEVO(False)
                Exit Sub
            End If
            LECTOR.Close()
            ACTIVAR(False)
            NUEVO(True)
        End If
    End Sub

    Private Sub BTNCANCELAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ACTIVAR(True)
        NUEVO(True)
    End Sub
    Private Function CHECAEMPLEADO() As Boolean
        If Not frmPrincipal.CHECACONX() Then
            Return False
        End If
        Dim SQL As New SqlClient.SqlCommand("SELECT NOMBRE FROM EMPLEADOSSUCURSALES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CLAVE=" + TXTEMP.Text, frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQL.ExecuteReader
        If LECTOR.Read Then
            LBLEMP.Text = LECTOR(0)
            LECTOR.Close()
            Return True
        Else
            MessageBox.Show("El No. de Empleado " + TXTEMP.Text + " NO esta dado de Alta ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LECTOR.Close()
            Return False
        End If
    End Function
    Private Function VALIDAR() As Boolean
        If TXTANT.Visible = False Then
            If TXTPWD.Text = "" Then
                MessageBox.Show("Introduzca una contraseña", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Return False
            Else
                Return True
            End If
        Else
            If TXTANT.Text = PWD Then
                If TXTNVA.Text = TXTPWD.Text Then
                    Return True
                Else
                    MessageBox.Show("Las contraseñas escritas NO coinciden", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    Return False
                End If
            Else
                MessageBox.Show("La contraseña anterior NO coincide", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Return False
            End If
        End If
    End Function
    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If VALIDAR() Then
        Else
            Exit Sub
        End If
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        If TXTANT.Visible Then
            Dim SQL As New SqlClient.SqlCommand("UPDATE CAJERAS SET PWD='" + TXTPWD.Text + "' WHERE TIENDA='" + frmPrincipal.SucursalBase + "' AND CLAVE=" + TXTEMP.Text, frmPrincipal.CONX)
            SQL.ExecuteNonQuery()
        Else
            Dim SQL As New SqlClient.SqlCommand("INSERT INTO CAJERAS (TIENDA,EMPLEADO,PWD) VALUES ('" + frmPrincipal.SucursalBase + "'," + TXTEMP.Text + ",'" + TXTPWD.Text + "')", frmPrincipal.CONX)
            SQL.ExecuteNonQuery()
        End If
        MessageBox.Show("La informacion de Cajer@ ha sido guardada exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        ACTIVAR(True)
        NUEVO(True)
    End Sub

    Private Sub TXTEMP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTEMP.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(13) Then
            If TXTEMP.Text = "" Then
            Else
                CARGAR()
            End If
        End If
    End Sub

    Private Sub BTNBUSCAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNBUSCAR.Click
        frmClsBusqueda.BUSCAR("SELECT CLAVE ,NOMBRE FROM EMPLEADOSSUCURSALES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' ", " AND NOMBRE", " ORDER BY NOMBRE", "Búsqueda de empleados", "Nombre del Empleado", "Empleado(s)", 1, frmPrincipal.CadenaConexion, True)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTEMP.Text = frmClsBusqueda.TREG.Rows(0).Item(0).ToString
            CARGAR()
        End If
    End Sub

    Private Sub BTNGUARDAR_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        If VALIDAR() Then
        Else
            Exit Sub
        End If
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        If TXTANT.Visible Then
            Dim SQL As New SqlClient.SqlCommand("UPDATE CAJERAS SET PWD='" + TXTPWD.Text + "' WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND EMPLEADO=" + TXTEMP.Text, frmPrincipal.CONX)
            SQL.ExecuteNonQuery()
        Else
            Dim SQL As New SqlClient.SqlCommand("INSERT INTO CAJERAS (SUCURSAL,EMPLEADO,PWD) VALUES ('" + frmPrincipal.SucursalBase + "'," + TXTEMP.Text + ",'" + TXTPWD.Text + "')", frmPrincipal.CONX)
            SQL.ExecuteNonQuery()
        End If
        MessageBox.Show("La informacion de cajer@ ha sido guardada exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        ACTIVAR(True)
        NUEVO(True)
    End Sub
    Private Sub LIMPIAR()
        TXTEMP.Clear()
        TXTANT.Clear()
        TXTNVA.Clear()
        TXTPWD.Clear()
    End Sub

    Private Sub BTNCANCELAR_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        LIMPIAR()
        ACTIVAR(True)
        NUEVO(True)
    End Sub

    Private Sub frmCajeras_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Alt + Keys.G Then
            If BTNGUARDAR.Enabled = False Then
                Exit Sub
            End If
            If VALIDAR() Then
            Else
                Exit Sub
            End If
            If Not frmPrincipal.CHECACONX() Then
                Exit Sub
            End If
            If TXTANT.Visible Then
                Dim SQL As New SqlClient.SqlCommand("UPDATE CAJERAS SET PWD='" + TXTPWD.Text + "' WHERE TIENDA='" + frmPrincipal.SucursalBase + "' AND EMPLEADO=" + TXTEMP.Text, frmPrincipal.CONX)
                SQL.ExecuteNonQuery()
            Else
                Dim SQL As New SqlClient.SqlCommand("INSERT INTO CAJERAS (TIENDA,EMPLEADO,PWD) VALUES ('" + frmPrincipal.SucursalBase + "'," + TXTEMP.Text + ",'" + TXTPWD.Text + "')", frmPrincipal.CONX)
                SQL.ExecuteNonQuery()
            End If
            MessageBox.Show("La informacion de cajer@ ha sido guardada exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            ACTIVAR(True)
            NUEVO(True)
        End If

       

        If e.KeyCode = Keys.Alt + Keys.C Then
            LIMPIAR()
        End If


        If e.KeyCode = Keys.Alt + Keys.B Then
            frmClsBusqueda.BUSCAR("SELECT CLAVE ,NOMBRE FROM EMPLEADOS WHERE TIENDA='" + frmPrincipal.SucursalBase + "' ", " AND NOMBRE", " ORDER BY NOMBRE", "Búsqueda de empleados", "Nombre del Empleado", "Empleado(s)", 1, frmPrincipal.CadenaConexion, True)
            If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
                TXTEMP.Text = frmClsBusqueda.TREG.Rows(0).Item(0).ToString
                CARGAR()
            End If
        End If
    End Sub

    Private Sub TXTANT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTANT.KeyPress, TXTNVA.KeyPress, TXTPWD.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class