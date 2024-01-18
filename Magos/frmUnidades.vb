Public Class frmUnidades
    Private Sub frmUnidades_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        CBACT.SelectedIndex = 0
        ACTIVAR(True)
        CARGASIGUNI()
    End Sub
    Private Sub CARGASIGUNI()
        Try
            frmPrincipal.CHECACONX()
            Dim NUM As Integer
            NUM = 1
            Dim SQLMOV As New SqlClient.SqlCommand("SELECT DBO.SIGUIENTEUNIDAD()", frmPrincipal.CONX)
            Dim LECTOR As SqlClient.SqlDataReader
            LECTOR = SQLMOV.ExecuteReader
            If LECTOR.Read Then
                NUM = LECTOR(0)
                LECTOR.Close()
                TXTCLA.Text = NUM
            Else
                LECTOR.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        TXTCLA.Enabled = V
        TXTNOM.Enabled = Not V

        CBACT.Enabled = Not V
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
    Private Sub CARGADATOS()
        frmPrincipal.CHECACONX()
        ACTIVAR(False)
        Dim SQLSELECT As New SqlClient.SqlCommand("SELECT CLAVE,NOMBRE,ACTIVO FROM UNIDADES WHERE CLAVE=" + TXTCLA.Text + "", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLSELECT.ExecuteReader
        If LECTOR.Read Then
            TXTNOM.Text = LECTOR(1).ToString
            If CType(LECTOR(2), Boolean) = True Then
                CBACT.SelectedIndex = 0
            Else
                CBACT.SelectedIndex = 1
            End If
            BTNELIMINAR.Enabled = True
        End If
        LECTOR.Close()
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
        If TXTNOM.Text = "" Then
            MessageBox.Show("Falta ingresar el nombre", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TXTNOM.Focus()
            Exit Sub
        End If
        Dim xyz As Short
        xyz = MessageBox.Show("¿Desea guardar la información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Sub
        End If

        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If

        Dim SQLGUARDAR As New SqlClient.SqlCommand
        SQLGUARDAR.Connection = frmPrincipal.CONX
        SQLGUARDAR.CommandType = CommandType.StoredProcedure
        SQLGUARDAR.Parameters.Add("@CLA", SqlDbType.Int).Value = TXTCLA.Text
        SQLGUARDAR.Parameters.Add("@NOM", SqlDbType.VarChar, 100).Value = TXTNOM.Text
        SQLGUARDAR.Parameters.Add("@ACT", SqlDbType.Bit)

        If CBACT.SelectedIndex = 0 Then
            SQLGUARDAR.Parameters("@ACT").Value = 1
        Else
            SQLGUARDAR.Parameters("@ACT").Value = 0
        End If

        SQLGUARDAR.CommandText = "SPUNIDADES"
        SQLGUARDAR.ExecuteNonQuery()
        MessageBox.Show("La información ha sido guardada correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ACTIVAR(True)
        LIMPIAR()
        CARGASIGUNI()
    End Sub
    Private Sub LIMPIAR()
        TXTNOM.Text = ""
        CBACT.SelectedIndex = 0
    End Sub

    Private Sub BTNELIMINAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNELIMINAR.Click
        Dim xyz As Short
        xyz = MessageBox.Show("¿Esta seguro que desea eliminar?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If xyz = 6 Then
            Try
                If Not frmPrincipal.CHECACONX() Then
                    Exit Sub
                End If
                Dim SQLELIMINAR As New SqlClient.SqlCommand
                SQLELIMINAR.Connection = frmPrincipal.CONX
                SQLELIMINAR.CommandText = "DELETE FROM UNIDADES WHERE CLAVE=" + TXTCLA.Text
                SQLELIMINAR.ExecuteNonQuery()
                MessageBox.Show("La información ha sido eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            LIMPIAR()
            ACTIVAR(True)
            CARGASIGUNI()
        End If
    End Sub

    Private Sub BTNCANCELAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        ACTIVAR(True)
        LIMPIAR()
        CARGASIGUNI()
    End Sub

    Private Sub BTNBUSCAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNBUSCAR.Click
        frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,ACTIVO FROM UNIDADES", " AND NOMBRE", " ORDER BY NOMBRE", "Búsqueda de unidades", "Nombre de la unidad", "Unidad(es)", 1, frmPrincipal.CadenaConexion, True)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTCLA.Text = frmClsBusqueda.TREG.Rows(0).Item(0).ToString
            CARGADATOS()
        End If
    End Sub

    Private Sub TXTNOM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTNOM.KeyPress, CBACT.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmUnidades_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Alt + Keys.G Then
            If TXTNOM.Enabled = False Then
                Exit Sub
            End If
            If TXTNOM.Text = "" Then
                MessageBox.Show("Falta ingresar el nombre", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TXTNOM.Focus()
                Exit Sub
            End If
            Dim xyz As Short
            xyz = MessageBox.Show("¿Desea guardar la información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If xyz <> 6 Then
                Exit Sub
            End If

            If Not frmPrincipal.CHECACONX() Then
                Exit Sub
            End If

            Dim SQLGUARDAR As New SqlClient.SqlCommand
            SQLGUARDAR.Connection = frmPrincipal.CONX
            SQLGUARDAR.CommandType = CommandType.StoredProcedure
            SQLGUARDAR.Parameters.Add("@CLA", SqlDbType.Int).Value = TXTCLA.Text
            SQLGUARDAR.Parameters.Add("@NOM", SqlDbType.VarChar, 100).Value = TXTNOM.Text
            SQLGUARDAR.Parameters.Add("@ACT", SqlDbType.Bit)

            If CBACT.SelectedIndex = 0 Then
                SQLGUARDAR.Parameters("@ACT").Value = 1
            Else
                SQLGUARDAR.Parameters("@ACT").Value = 0
            End If

            SQLGUARDAR.CommandText = "SPUNIDADES"
            SQLGUARDAR.ExecuteNonQuery()
            MessageBox.Show("La información ha sido guardada correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ACTIVAR(True)
            LIMPIAR()
            CARGASIGUNI()
        End If

        If e.KeyCode = Keys.Alt + Keys.E Then
            If BTNELIMINAR.Enabled = False Then
                Exit Sub
            End If
            Dim xyz As Short
            xyz = MessageBox.Show("¿Esta seguro que desea eliminar?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If xyz = 6 Then
                Try
                    If Not frmPrincipal.CHECACONX() Then
                        Exit Sub
                    End If
                    Dim SQLELIMINAR As New SqlClient.SqlCommand
                    SQLELIMINAR.Connection = frmPrincipal.CONX
                    SQLELIMINAR.CommandText = "DELETE FROM UNIDADES WHERE CLAVE=" + TXTCLA.Text
                    SQLELIMINAR.ExecuteNonQuery()
                    MessageBox.Show("La información ha sido eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                LIMPIAR()
                ACTIVAR(True)
                CARGASIGUNI()
            End If
        End If

        If e.KeyCode = Keys.Alt + Keys.C Then
            ACTIVAR(True)
            LIMPIAR()
            CARGASIGUNI()
        End If


        If e.KeyCode = Keys.Alt + Keys.B Then
            frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,ACTIVO FROM UNIDADES", " AND NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Grupos", "Nombre del Grupo", "Grupo(s)", 1, frmPrincipal.CadenaConexion, True)
            If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
                TXTCLA.Text = frmClsBusqueda.TREG.Rows(0).Item(0).ToString
                CARGADATOS()
            End If
        End If

    End Sub
End Class