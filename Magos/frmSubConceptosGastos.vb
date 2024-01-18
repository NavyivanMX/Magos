Public Class frmSubConceptosGastos
    Dim LCON As New List(Of String)
    Private Sub frmSubConceptosGastos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        LLENACOMBOBOX2(CBCON, LCON, "SELECT CLAVE,NOMBRE FROM CONCEPTOSGASTOS WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de Seleccionar", "")
        ACTIVAR(True)
        CBACT.SelectedIndex = 0
        CARGASIGSUBCON()
        TXTCLA.Focus()
        TXTCLA.SelectAll()
    End Sub
    Private Sub CARGASIGSUBCON()
        Try
            frmPrincipal.CHECACONX()
            Dim NUM As Integer
            NUM = 1
            Dim SQLMOV As New SqlClient.SqlCommand("SELECT DBO.SIGSUBCONCEPTOSGASTOS()", frmPrincipal.CONX)
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
        CBCON.Enabled = Not V

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

        CBACT.SelectedIndex = 0
    End Sub
    Private Sub CARGADATOS()
        ACTIVAR(False)

        Dim SQLSELECT As New SqlClient.SqlCommand("SELECT NOMBRE,ACTIVO,CONCEPTO FROM SUBCONCEPTOSGASTOS WHERE CLAVE='" + TXTCLA.Text + "' AND EMPRESA=" + frmPrincipal.Empresa + "", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLSELECT.ExecuteReader
        If LECTOR.Read Then

            TXTNOM.Text = LECTOR(0)
            If CType(LECTOR(1), Boolean) Then
                CBACT.SelectedIndex = 0
            Else
                CBACT.SelectedIndex = 1
            End If
            CARGAX(LCON, CBCON, LECTOR(2))
            BTNELIMINAR.Enabled = True

        End If

        LECTOR.Close()
        TXTNOM.Focus()
    End Sub
    Private Sub TXTCLA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCLA.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        End If
        If TXTCLA.Text = "" Then
            TXTCLA.Focus()
            Exit Sub
        End If
        If e.KeyChar = Chr(13) Then
            CARGADATOS()
        End If
    End Sub
    Private Sub GUARDAR()

        Dim SQL As New SqlClient.SqlCommand("SPSUBCONCEPTOSGASTOS", frmPrincipal.CONX)
        SQL.CommandType = CommandType.StoredProcedure
        SQL.Parameters.Add("@CON", SqlDbType.VarChar).Value = frmPrincipal.Empresa
        SQL.Parameters.Add("@CLA", SqlDbType.VarChar).Value = TXTCLA.Text
        SQL.Parameters.Add("@NOM", SqlDbType.VarChar).Value = TXTNOM.Text
        SQL.Parameters.Add("@ACT", SqlDbType.Bit)

        If CBACT.SelectedIndex = 0 Then
            SQL.Parameters("@ACT").Value = 1
        Else
            SQL.Parameters("@ACT").Value = 0
        End If

        SQL.ExecuteNonQuery()
        MessageBox.Show("La informacin ha sido guardada correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ACTIVAR(True)
        LIMPIAR()
        CARGASIGSUBCON()
    End Sub
    
    Private Sub BTNBUSCAR_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNBUSCAR.Click

        frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,ACTIVO FROM SUBCONCEPTOSGASTOS WHERE EMPRESA=" + frmPrincipal.Empresa + " ", " AND NOMBRE", " ORDER BY NOMBRE", "Búsqueda de SubConceptos", "Nombre de SubConcepto", "SubConcepto(s)", 1, frmPrincipal.CadenaConexion, True)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTCLA.Text = frmClsBusqueda.TREG.Rows(0).Item(0)
            CARGADATOS()
        End If
    End Sub

    Private Sub BTNGUARDAR_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        If CBCON.SelectedIndex = 0 Then
            MessageBox.Show("Debe seleccionar un concepto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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

                Dim SQLELIMINAR As New SqlClient.SqlCommand
                SQLELIMINAR.Connection = frmPrincipal.CONX
                SQLELIMINAR.CommandText = "DELETE FROM SUBCONCEPTOSGASTOS WHERE CLAVE='" + TXTCLA.Text + "'"
                SQLELIMINAR.ExecuteNonQuery()
                MessageBox.Show("La información ha sido eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            LIMPIAR()
            ACTIVAR(True)
            CARGASIGSUBCON()
        End If
    End Sub

    Private Sub BTNCANCELAR_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        ACTIVAR(True)
        LIMPIAR()
        CARGASIGSUBCON()
    End Sub

    Private Sub frmSubConceptosGastos_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Alt + Keys.G Then
            If TXTNOM.Enabled = False Then
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

                    Dim SQLELIMINAR As New SqlClient.SqlCommand
                    SQLELIMINAR.Connection = frmPrincipal.CONX
                    SQLELIMINAR.CommandText = "DELETE FROM SUBCONCEPTOSGASTOS WHERE CLAVE='" + TXTCLA.Text + "'"
                    SQLELIMINAR.ExecuteNonQuery()
                    MessageBox.Show("La informaci�n ha sido eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                LIMPIAR()
                ACTIVAR(True)
                CARGASIGSUBCON()
            End If
        End If

        If e.KeyCode = Keys.Alt + Keys.C Then
            ACTIVAR(True)
            LIMPIAR()
            CARGASIGSUBCON()
        End If


        If e.KeyCode = Keys.Alt + Keys.B Then
            frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,ACTIVO FROM SUBCONCEPTOSGASTOS  ", " WHERE NOMBRE", " ORDER BY NOMBRE", "Búsqueda de SubConceptos", "Nombre de SubConcepto", "SubConcepto(s)", 1, frmPrincipal.CadenaConexion, True)
            If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
                TXTCLA.Text = frmClsBusqueda.TREG.Rows(0).Item(0)
                CARGADATOS()
            End If
        End If

    End Sub
End Class