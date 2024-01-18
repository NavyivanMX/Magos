Public Class frmProveedores
    Dim LCTP As New List(Of Integer)
    Private Sub frmProveedores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        ACTIVAR(True)
        LIMPIAR()
        CARGASIGCLI()
    End Sub
    
    Private Sub CARGASIGCLI()
        ' Try
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        Dim NUM As Integer
        NUM = 1
        Dim SQLMOV As New SqlClient.SqlCommand("SELECT DBO.SIGPROVEEDORES()", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLMOV.ExecuteReader
        If LECTOR.Read Then
            NUM = LECTOR(0)
            LECTOR.Close()
            TXTCLA.Text = NUM
        Else
            LECTOR.Close()
        End If
        'Catch ex As Exception
        '    Me.Close()
        'End Try
    End Sub

    Private Sub GUARDAR()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        'Try
        Dim SQL As New SqlClient.SqlCommand("", frmPrincipal.CONX)
        SQL.CommandType = CommandType.StoredProcedure
        SQL.Parameters.Add("@EMP", SqlDbType.Int).Value = frmPrincipal.Empresa
        SQL.Parameters.Add("@CLA", SqlDbType.VarChar).Value = TXTCLA.Text
        SQL.Parameters.Add("@NOM", SqlDbType.VarChar).Value = TXTNOM.Text
        SQL.Parameters.Add("@DIR", SqlDbType.VarChar).Value = TXTDIR.Text
        SQL.Parameters.Add("@CD", SqlDbType.VarChar).Value = TXTCD.Text
        SQL.Parameters.Add("@TEL", SqlDbType.VarChar).Value = TXTTEL.Text
        SQL.Parameters.Add("@CEL", SqlDbType.VarChar).Value = TXTCEL.Text
        SQL.Parameters.Add("@FAX", SqlDbType.VarChar).Value = TXTFAX.Text
        SQL.Parameters.Add("@RFC", SqlDbType.VarChar).Value = TXTRFC.Text
        SQL.Parameters.Add("@CB", SqlDbType.VarChar).Value = TXTBANCO.Text
        SQL.Parameters.Add("@ACT", SqlDbType.Bit)
        SQL.Parameters.Add("@NI", SqlDbType.Bit)
        SQL.Parameters.Add("@FI", SqlDbType.Bit)

        If CBACT.SelectedIndex = 0 Then
            SQL.Parameters("@ACT").Value = 1
        Else
            SQL.Parameters("@ACT").Value = 0
        End If

        If CBNI.SelectedIndex = 0 Then
            SQL.Parameters("@NI").Value = 1
        Else
            SQL.Parameters("@NI").Value = 0
        End If

        If CBFI.SelectedIndex = 0 Then
            SQL.Parameters("@FI").Value = 1
        Else
            SQL.Parameters("@FI").Value = 0
        End If

        SQL.CommandText = "SPPROVEEDORES"

        SQL.ExecuteNonQuery()

        MessageBox.Show("La Información ha sido Guardada correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ACTIVAR(True)
        LIMPIAR()
        CARGASIGCLI()
        ''Catch ex As Exception

        ''End Try
    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        TXTCLA.Enabled = V
        TXTNOM.Enabled = Not V
        TXTDIR.Enabled = Not V
        TXTCD.Enabled = Not V
        TXTRFC.Enabled = Not V
        TXTTEL.Enabled = Not V
        TXTCEL.Enabled = Not V
        TXTBANCO.Enabled = Not V
        TXTFAX.Enabled = Not V
       
        CBACT.Enabled = Not V
        CBNI.Enabled = Not V
        CBFI.Enabled = Not V

        BTNELIMINAR.Enabled = False
        BTNGUARDAR.Enabled = Not V
        If V Then
            TXTCLA.Focus()
            TXTCLA.SelectAll()
        Else
            TXTNOM.Focus()
            TXTNOM.SelectAll()
        End If
    End Sub
    Private Sub LIMPIAR()
        TXTCLA.Text = ""
        TXTNOM.Text = ""
        TXTBANCO.Text = ""
        TXTCEL.Text = ""
        TXTDIR.Text = ""
        TXTCD.Text = ""
        TXTRFC.Text = ""
        TXTTEL.Text = ""
        TXTFAX.Text = ""
        CBACT.SelectedIndex = 0
        CBNI.SelectedIndex = 0
        CBFI.SelectedIndex = 0
        TXTCLA.Focus()
        TXTCLA.SelectAll()
    End Sub
    Private Sub CARGADATOS()
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        ACTIVAR(False)
        Try
            Dim SQLSELECT As New SqlClient.SqlCommand("SELECT CLAVE,NOMBRE,DIRECCION,CIUDAD,TELEFONO,CELULAR,FAX,RFC,CUENTABANCO,ACTIVO,NOTAIVA,FACTURAIVA FROM PROVEEDORES WHERE CLAVE='" + TXTCLA.Text + "' ", frmPrincipal.CONX)
            Dim LECTOR As SqlClient.SqlDataReader
            LECTOR = SQLSELECT.ExecuteReader
            If LECTOR.Read Then
                TXTNOM.Text = LECTOR(1)
                TXTDIR.Text = LECTOR(2)
                TXTCD.Text = LECTOR(3)
                TXTTEL.Text = LECTOR(4)
                TXTCEL.Text = LECTOR(5)
                TXTFAX.Text = LECTOR(6)
                TXTRFC.Text = LECTOR(7)
                TXTBANCO.Text = LECTOR(8)
                If CType(LECTOR(9), Boolean) = True Then
                    CBACT.SelectedIndex = 0
                Else
                    CBACT.SelectedIndex = 1
                End If

                If CType(LECTOR(10), Boolean) = True Then
                    CBNI.SelectedIndex = 0
                Else
                    CBNI.SelectedIndex = 1
                End If
                If CType(LECTOR(11), Boolean) = True Then
                    CBFI.SelectedIndex = 0
                Else
                    CBFI.SelectedIndex = 1
                End If


                BTNELIMINAR.Enabled = True
            End If
            LECTOR.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TXTCLA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCLA.KeyPress
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
        If e.KeyChar = Chr(13) Then
            If TXTCLA.Text = "" Then
            Else
                CARGADATOS()
            End If
        End If
    End Sub

    Private Sub BTNGUARDAR_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        If TXTNOM.Text = "" Or TXTDIR.Text = "" Or TXTCD.Text = "" Or TXTTEL.Text = "" Or TXTCEL.Text = "" Or TXTFAX.Text = "" Or TXTRFC.Text = "" Then
            MessageBox.Show("Falta ingresar datos del proveedor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim xyz As Short
        xyz = MessageBox.Show("¿Desea guardar la información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Sub
        End If
        GUARDAR()
    End Sub

    Private Sub BTNELIMINAR_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNELIMINAR.Click
        Dim xyz As Short
        xyz = MessageBox.Show("¿Esta seguro que desea eliminar?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If xyz = 6 Then
            Try
                If Not frmPrincipal.CHECACONX Then
                    Exit Sub
                End If
                Dim SQLELIMINAR As New SqlClient.SqlCommand
                SQLELIMINAR.Connection = frmPrincipal.CONX
                SQLELIMINAR.CommandText = "DELETE FROM PROVEEDORES WHERE CLAVE='" + TXTCLA.Text + "' AND EMPRESA=" + frmPrincipal.Empresa + " "
                SQLELIMINAR.ExecuteNonQuery()
                MessageBox.Show("La información ha sido eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                CARGASIGCLI()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            LIMPIAR()
            ACTIVAR(True)
            CARGASIGCLI()
        End If
    End Sub

    Private Sub BTNCANCELAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        ACTIVAR(True)
        LIMPIAR()
        CARGASIGCLI()
    End Sub

    Private Sub BTNMOSTRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNMOSTRAR.Click
        Try
            frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,ACTIVO FROM PROVEEDORES WHERE EMPRESA=" + frmPrincipal.Empresa + " ", " AND NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Proveedores", "Nombre del proveedor", "Cliente(s)", 1, frmPrincipal.CadenaConexion, True)
            If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
                TXTCLA.Text = frmClsBusqueda.TREG.Rows(0).Item(0)
                CARGADATOS()
                TXTNOM.Focus()
                TXTNOM.SelectAll()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TXTNOM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTNOM.KeyPress, TXTDIR.KeyPress, TXTCD.KeyPress, TXTTEL.KeyPress, TXTCEL.KeyPress, TXTFAX.KeyPress, TXTRFC.KeyPress, TXTBANCO.KeyPress, CBACT.KeyPress, CBNI.KeyPress, CBFI.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
#Region "CHECA EL DE EMPLEADOS"
   

#End Region

    Private Sub frmProveedores_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Alt + Keys.G Then
            If TXTNOM.Enabled = False Then
                Exit Sub
            End If
            If TXTNOM.Text = "" Or TXTDIR.Text = "" Or TXTCD.Text = "" Or TXTTEL.Text = "" Or TXTCEL.Text = "" Or TXTFAX.Text = "" Or TXTRFC.Text = "" Then
                MessageBox.Show("Falta ingresar datos del proveedor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Dim xyz As Short
            xyz = MessageBox.Show("¿Desea guardar la información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If xyz <> 6 Then
                Exit Sub
            End If
            GUARDAR()
        End If

        If e.KeyCode = Keys.Alt + Keys.E Then
            If BTNELIMINAR.Enabled = False Then
                Exit Sub
            End If
            Dim xyz As Short
            xyz = MessageBox.Show("¿Esta seguro que desea eliminar?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If xyz = 6 Then
                Try
                    If Not frmPrincipal.CHECACONX Then
                        Exit Sub
                    End If
                    Dim SQLELIMINAR As New SqlClient.SqlCommand
                    SQLELIMINAR.Connection = frmPrincipal.CONX
                    SQLELIMINAR.CommandText = "DELETE FROM PROVEEDORES WHERE CLAVE='" + TXTCLA.Text + "' AND EMPRESA=" + frmPrincipal.Empresa + " "
                    SQLELIMINAR.ExecuteNonQuery()
                    MessageBox.Show("La información ha sido eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    CARGASIGCLI()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                LIMPIAR()
                ACTIVAR(True)
                CARGASIGCLI()
            End If
        End If

        If e.KeyCode = Keys.Alt + Keys.C Then
            ACTIVAR(True)
            LIMPIAR()
            CARGASIGCLI()
        End If


        If e.KeyCode = Keys.Alt + Keys.B Then
            Try
                frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,ACTIVO FROM PROVEEDORES WHERE EMPRESA=" + frmPrincipal.Empresa + " ", " AND NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Proveedores", "Nombre del proveedor", "Cliente(s)", 1, frmPrincipal.CadenaConexion, True)
                If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
                    TXTCLA.Text = frmClsBusqueda.TREG.Rows(0).Item(0)
                    CARGADATOS()
                    TXTNOM.Focus()
                    TXTNOM.SelectAll()
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class