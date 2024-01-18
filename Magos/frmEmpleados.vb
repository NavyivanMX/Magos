Public Class frmEmpleados
  
    Private Sub frmEmpleados_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        CBACT.SelectedIndex = 0
        ACTIVAR(True)
        INICIALIZARTTT()
        CARGASIGEMP()
        CBTS.SelectedIndex = 0
    End Sub
    Private Sub INICIALIZARTTT()
        TTT.SetToolTip(BTNGUARDAR, "Guardar")
        TTT.SetToolTip(BTNCANCELAR, "Cancelar")
        TTT.SetToolTip(BTNELIMINAR, "Eliminar")
        TTT.SetToolTip(BTNBUSCAR, "Buscar")
    End Sub
    Private Sub CARGASIGEMP()
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        Dim QUERY As String
        Try
            QUERY = "SELECT MAX(CLAVE) FROM EMPLEADOSSUCURSALES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "'" ''---> Y PARA K TE SIRVE EL CAMPO TIENDA EN LA TABLA EMPLEADOS???? WHERE E.TIENDA='"+FRMPRINCIPAL.SUCURSALBASE+"'"
            Dim SQLSELECT As New SqlClient.SqlCommand(QUERY, frmPrincipal.CONX)
            Dim LECTOR As SqlClient.SqlDataReader
            LECTOR = SQLSELECT.ExecuteReader
            If LECTOR.Read Then
                If LECTOR(0) Is DBNull.Value Then
                    TXTCLA.Text = "1"
                Else
                    TXTCLA.Text = LECTOR(0) + 1
                End If
            End If
            LECTOR.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        TXTCLA.Enabled = V
        TXTNOM.Enabled = Not V
        TXTDIR.Enabled = Not V
        TXTTEL.Enabled = Not V
        TXTCD.Enabled = Not V
        TXTPUESTO.Enabled = Not V
        DTNAC.Enabled = Not V
        DTFING.Enabled = Not V
        DTFSAL.Enabled = Not V
        TXTSS.Enabled = Not V
        CBTS.Enabled = Not V
        TXTFAM.Enabled = Not V
        CBACT.Enabled = Not V
        CBREP.Enabled = Not V
        BTNGUARDAR.Enabled = Not V
        BTNELIMINAR.Enabled = False
        If V Then
            TXTCLA.SelectAll()
            TXTCLA.Focus()
        Else
            TXTNOM.SelectAll()
            TXTNOM.Focus()
        End If
    End Sub
    Private Sub LIMPIAR()
        TXTNOM.Text = ""
        TXTDIR.Text = ""
        TXTTEL.Text = ""
        TXTCD.Text = ""
        TXTPUESTO.Text = ""
        DTNAC.Text = ""
        DTFING.Text = ""
        DTFSAL.Text = ""
        TXTSS.Text = ""
        CBTS.SelectedIndex = 0
        TXTFAM.Text = ""
        CBACT.Text = ""
        CARGASIGEMP()
    End Sub

    Private Sub CARGADATOS()
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        ACTIVAR(False)
        Dim SQLSELECT As New SqlClient.SqlCommand("SELECT CLAVE,NOMBRE,DIRECCION,TELEFONO,CIUDAD,PUESTO,FECNAC,FECING,FECSAL,NOSS,TIPOSANGRE,FAMILIAR,ACTIVO FROM EMPLEADOSSUCURSALES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CLAVE=" + TXTCLA.Text, frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLSELECT.ExecuteReader
        If LECTOR.Read Then
            TXTNOM.Text = LECTOR(1).ToString
            TXTDIR.Text = LECTOR(2).ToString
            TXTTEL.Text = LECTOR(3).ToString
            TXTCD.Text = LECTOR(4).ToString
            TXTPUESTO.Text = LECTOR(5).ToString
            DTNAC.Value = LECTOR(6)
            DTFING.Value = LECTOR(7)
            DTFSAL.Value = LECTOR(8)
            TXTSS.Text = LECTOR(9).ToString
            CBTS.Text = LECTOR(10).ToString
            TXTFAM.Text = LECTOR(11).ToString

            If LECTOR(12) = 1 Then
                CBACT.SelectedIndex = 1
            Else
                CBACT.SelectedIndex = 0
            End If
            BTNELIMINAR.Enabled = True
        End If
        LECTOR.Close()
    End Sub
    Private Sub TXTNOM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub BTNCANCELAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        LIMPIAR()
        ACTIVAR(True)
        CARGASIGEMP()
    End Sub

    Private Sub TXTCLA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCLA.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(13) Then
            CARGADATOS()
        End If
    End Sub

    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        If TXTNOM.Text = "" Or TXTDIR.Text = "" Or TXTTEL.Text = "" Or TXTCD.Text = "" Or TXTPUESTO.Text = "" Or TXTSS.Text = "" Or TXTFAM.Text = "" Then
            MessageBox.Show("Falta ingresar infomacin importante del emplead@", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim xyz As Short
        xyz = MessageBox.Show("Desea Guardar la Información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Sub
        End If
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        Dim SQLGUARDAR As New SqlClient.SqlCommand
        SQLGUARDAR.Connection = frmPrincipal.CONX
        SQLGUARDAR.CommandType = CommandType.StoredProcedure
        SQLGUARDAR.Parameters.Add("@TI", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
        SQLGUARDAR.Parameters.Add("@CLA", SqlDbType.Int).Value = CType(TXTCLA.Text, Integer)
        SQLGUARDAR.Parameters.Add("@NOM", SqlDbType.VarChar, 100).Value = TXTNOM.Text
        SQLGUARDAR.Parameters.Add("@DIR", SqlDbType.VarChar, 200).Value = TXTDIR.Text
        SQLGUARDAR.Parameters.Add("@TEL", SqlDbType.VarChar, 200).Value = TXTTEL.Text
        SQLGUARDAR.Parameters.Add("@CD", SqlDbType.VarChar, 200).Value = TXTCD.Text
        SQLGUARDAR.Parameters.Add("@PUE", SqlDbType.VarChar, 50).Value = TXTPUESTO.Text
        SQLGUARDAR.Parameters.Add("@FN", SqlDbType.DateTime).Value = DTNAC.Text
        SQLGUARDAR.Parameters.Add("@FI", SqlDbType.DateTime).Value = DTFING.Text
        SQLGUARDAR.Parameters.Add("@FS", SqlDbType.DateTime).Value = DTFSAL.Text
        SQLGUARDAR.Parameters.Add("@NOSS", SqlDbType.VarChar, 20).Value = TXTSS.Text
        SQLGUARDAR.Parameters.Add("@TS", SqlDbType.VarChar, 5).Value = CBTS.Text
        SQLGUARDAR.Parameters.Add("@FAM", SqlDbType.VarChar, 100).Value = TXTFAM.Text
        SQLGUARDAR.Parameters.Add("@ACT", SqlDbType.Bit)
        SQLGUARDAR.Parameters.Add("@REP", SqlDbType.Bit)

        If CBACT.SelectedIndex = 0 Then
            SQLGUARDAR.Parameters("@ACT").Value = 1
        Else
            SQLGUARDAR.Parameters("@ACT").Value = 0
        End If
        If CBREP.SelectedIndex = 0 Then
            SQLGUARDAR.Parameters("@REP").Value = 0
        Else
            SQLGUARDAR.Parameters("@REP").Value = 1
        End If
        frmPrincipal.CHECACONX()
        SQLGUARDAR.CommandText = "SPEMPLEADO"
        SQLGUARDAR.ExecuteNonQuery()
        MessageBox.Show("La Información ha sido Guardada correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ACTIVAR(True)
        LIMPIAR()
    End Sub

    Private Sub BTNELIMINAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNELIMINAR.Click
        Dim xyz As Short
        xyz = MessageBox.Show("Esta seguro que desea eliminar?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If xyz = 6 Then
            Try
                If Not frmPrincipal.CHECACONX() Then
                    Exit Sub
                End If
                Dim SQLELIMINAR As New SqlClient.SqlCommand
                SQLELIMINAR.Connection = frmPrincipal.CONX
                SQLELIMINAR.CommandText = "DELETE FROM EMPLEADOSSUCURSALES WHERE TIENDA='" + frmPrincipal.SucursalBase + "' AND CLAVE=" + TXTCLA.Text
                SQLELIMINAR.ExecuteNonQuery()
                MessageBox.Show("La Información ha sido eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            LIMPIAR()
            ACTIVAR(True)
            CARGASIGEMP()
        End If
    End Sub

    Private Sub BTNBUSCAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNBUSCAR.Click
        frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,PUESTO,ACTIVO FROM EMPLEADOSSUCURSALES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "'", " AND NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Empleados", "Nombre del Empleado", "Empleado(s)", 1, frmPrincipal.CadenaConexion, True)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTCLA.Text = frmClsBusqueda.TREG.Rows(0).Item(0).ToString
            CARGADATOS()
        End If
    End Sub

   
    Private Sub frmEmpleados_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Alt + Keys.G Then
            If TXTNOM.Enabled = False Then
                Exit Sub
            End If
            If TXTNOM.Text = "" Or TXTDIR.Text = "" Or TXTTEL.Text = "" Or TXTCD.Text = "" Or TXTPUESTO.Text = "" Or TXTSS.Text = "" Or TXTFAM.Text = "" Then
                MessageBox.Show("Falta ingresar infomación importante del emplead@", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Dim xyz As Short
            xyz = MessageBox.Show("Desea Guardar la Información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If xyz <> 6 Then
                Exit Sub
            End If
            If Not frmPrincipal.CHECACONX() Then
                Exit Sub
            End If
            Dim SQLGUARDAR As New SqlClient.SqlCommand
            SQLGUARDAR.Connection = frmPrincipal.CONX
            SQLGUARDAR.CommandType = CommandType.StoredProcedure
            SQLGUARDAR.Parameters.Add("@TI", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
            SQLGUARDAR.Parameters.Add("@CLA", SqlDbType.Int).Value = CType(TXTCLA.Text, Integer)
            SQLGUARDAR.Parameters.Add("@NOM", SqlDbType.VarChar, 100).Value = TXTNOM.Text
            SQLGUARDAR.Parameters.Add("@DIR", SqlDbType.VarChar, 200).Value = TXTDIR.Text
            SQLGUARDAR.Parameters.Add("@TEL", SqlDbType.VarChar, 200).Value = TXTTEL.Text
            SQLGUARDAR.Parameters.Add("@CD", SqlDbType.VarChar, 200).Value = TXTCD.Text
            SQLGUARDAR.Parameters.Add("@PUE", SqlDbType.VarChar, 50).Value = TXTPUESTO.Text
            SQLGUARDAR.Parameters.Add("@FN", SqlDbType.DateTime).Value = DTNAC.Text
            SQLGUARDAR.Parameters.Add("@FI", SqlDbType.DateTime).Value = DTFING.Text
            SQLGUARDAR.Parameters.Add("@FS", SqlDbType.DateTime).Value = DTFSAL.Text
            SQLGUARDAR.Parameters.Add("@NOSS", SqlDbType.VarChar, 20).Value = TXTSS.Text
            SQLGUARDAR.Parameters.Add("@TS", SqlDbType.VarChar, 5).Value = CBTS.Text
            SQLGUARDAR.Parameters.Add("@FAM", SqlDbType.VarChar, 100).Value = TXTFAM.Text
            SQLGUARDAR.Parameters.Add("@ACT", SqlDbType.Bit)

            If CBACT.SelectedIndex = 0 Then
                SQLGUARDAR.Parameters("@ACT").Value = 1
            Else
                SQLGUARDAR.Parameters("@ACT").Value = 0
            End If
            frmPrincipal.CHECACONX()
            SQLGUARDAR.CommandText = "SPEMPLEADO"
            SQLGUARDAR.ExecuteNonQuery()
            MessageBox.Show("La Información ha sido Guardada correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ACTIVAR(True)
            LIMPIAR()
        End If

        'If e.KeyCode = Keys.Alt + Keys.E Then
        '    If BTNELIMINAR.Enabled = False Then
        '        Exit Sub
        '    End If
        '    Dim xyz As Short
        '    xyz = MessageBox.Show("Esta seguro que desea eliminar?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        '    If xyz = 6 Then
        '        Try
        '            If Not frmPrincipal.CHECACONX() Then
        '                Exit Sub
        '            End If
        '            Dim SQLELIMINAR As New SqlClient.SqlCommand
        '            SQLELIMINAR.Connection = frmPrincipal.CONX
        '            SQLELIMINAR.CommandText = "DELETE FROM EMPLEADOS WHERE TIENDA='" + frmPrincipal.SucursalBase + "' AND CLAVE=" + TXTCLA.Text
        '            SQLELIMINAR.ExecuteNonQuery()
        '            MessageBox.Show("La información ha sido eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '        End Try
        '        LIMPIAR()
        '        ACTIVAR(True)
        '        CARGASIGEMP()
        '    End If
        'End If

        If e.KeyCode = Keys.Alt + Keys.C Then
            LIMPIAR()
            ACTIVAR(True)
            CARGASIGEMP()
        End If


        If e.KeyCode = Keys.Alt + Keys.B Then
            frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,PUESTO,ACTIVO FROM EMPLEADOS WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "'", " AND NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Empleados", "Nombre del Empleado", "Empleado(s)", 1, frmPrincipal.CadenaConexion, True)
            If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
                TXTCLA.Text = frmClsBusqueda.TREG.Rows(0).Item(0).ToString
                CARGADATOS()
            End If
        End If

    End Sub

    Private Sub TXTNOM_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTNOM.KeyPress, TXTDIR.KeyPress, TXTTEL.KeyPress, TXTCD.KeyPress, TXTPUESTO.KeyPress, DTNAC.KeyPress, DTFING.KeyPress, DTFSAL.KeyPress, TXTSS.KeyPress, CBTS.KeyPress, TXTFAM.KeyPress, CBACT.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class