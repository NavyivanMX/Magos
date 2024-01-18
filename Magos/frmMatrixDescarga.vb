Public Class frmMatrixDescarga
    Dim LTI As New List(Of String)
    Dim LALM As New List(Of String)
    Dim LPRO As New List(Of String)
    Dim LGRU As New List(Of String)
    Dim DT As New DataTable
    Private Sub frmMatrixDescarga_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        LLENACOMBOBOX(CBTI, LTI, "SELECT CLAVE,NOMBRECOMUN  FROM SUCURSALES WHERE ACTIVO=1 AND CLAVE<>'PRUEBAS' ORDER BY NOMBRECOMUN", frmPrincipal.CadenaConexion)
        LLENACOMBOBOX(CBALM, LALM, "SELECT A.CLAVE,A.NOMBRE FROM ALMACENES A INNER JOIN EMPRESASGASTOS E ON A.EMPRESA=E.CLAVE WHERE A.ACTIVO=1 AND E.GRUPO='" + frmPrincipal.Empresa + "' ORDER BY A.NOMBRE", frmPrincipal.CadenaConexion)
        DT = LLENATABLA("SELECT P.CLAVE,P.NOMBRE,P.GRUPOINVENTARIO FAMILIA,U.NOMBRE UNIDAD FROM PRODUCTOS P INNER JOIN UNIDADES U ON P.UNIDADINV=U.CLAVE WHERE P.ACTIVO = 1", frmPrincipal.CadenaConexion)
        LLENACOMBOBOX(CBGRU, LGRU, "SELECT CLAVE,NOMBRE FROM GRUPOSINV WHERE CLAVE IN (SELECT GRUPOINVENTARIO FROM PRODUCTOS) ORDER BY NOMBRE", frmPrincipal.CadenaConexion)

        ACTIVAR(True)
    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        CBTI.Enabled = V
        TXTPV.Enabled = V
        Button2.Enabled = V
        CBGRU.Enabled = Not V
        CBPROD.Enabled = Not V
        BTNAGREGAR.Enabled = Not V
        BTNQUITAR.Enabled = Not V
        Button1.Enabled = Not V
        BTNAGREGAR.Enabled = Not V
        TXTCANT.Enabled = Not V
        CBALM.Enabled = Not V
        BTNELIMINAR.Enabled = Not V
        CHECATABLA()
    End Sub
    Private Sub CHECATABLA()
        If DGV.Rows.Count <= 0 Then
        Else
            BTNQUITAR.Enabled = True
            BTNELIMINAR.Enabled = True
        End If
    End Sub
    Dim DV As New DataView
    Private Function CARGADATAVIEW(ByVal GRUPO As String) As Boolean
        DV = New DataView(DT, "FAMILIA='" + GRUPO + "'", "NOMBRE", DataViewRowState.CurrentRows)
        Return CARGAPRODUCTOS()
    End Function
    Dim LIEPS As New List(Of Integer)
    Dim LTIEPS As New List(Of Double)
    Dim LUDC As New List(Of String)
    Private Function CARGAPRODUCTOS() As Boolean
        'Try
        CBPROD.Items.Clear()
        LPRO.Clear()
        'LIVA.Clear()
        LIEPS.Clear()
        LTIEPS.Clear()
        LUDC.Clear()
        'LGRU.Clear()
        Dim X As Integer
        Dim DRV As DataRowView
        For X = 0 To DV.Count - 1
            DRV = DV.Item(X)
            CBPROD.Items.Add(DRV.Item(1))
            LPRO.Add(DRV.Item(0))
            LUDC.Add(DRV.Item(3))
        Next

        Try
            CBPROD.SelectedIndex = 0
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        'Catch ex As Exception
        '    frmPrincipal.CE.ESCRIBIR("inventario restaurantes", Now, "ventana:compras restaurantes metodo:CARGAPRODUCTOS()", ex.Message.ToString)
        '    Exit Function
        'End Try

    End Function
    Private Sub CBGRU_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBGRU.SelectedIndexChanged
        Try
            If Not CARGADATAVIEW(LGRU(CBGRU.SelectedIndex)) Then
                MessageBox.Show("No Se encuentra Asignado Ningún Producto en este Grupo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                CBPROD.Enabled = False
                'ACTIVAR(False)
            Else
                CBPROD.Enabled = True
                'If Not CARGADATAVIEW2(LGRU(CBGRU.SelectedIndex), LPRO(CBPROD.SelectedIndex)) Then
                '    MessageBox.Show("No Se encuentra Asignado Ninguna Equivalencia que Coincida con las Unidades de Entrada de Bodega", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                '    ACTIVAR(False)
                'Else
                '    ACTIVAR(True)
                'End If
            End If
            'DESPLIEGAPRECIOU()
        Catch ex As Exception
            'frmPrincipal.CE.ESCRIBIR("inventario restaurantes", Now, "ventana:compras restaurantes metodo:CBGRU_SelectedIndexChanged", ex.Message.ToString)
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,ACTIVO FROM PRODUCTOS ", " WHERE NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Productos de Venta", "Nombre del Producto", "Producto(s)", 1, frmPrincipal.CadenaConexion, False)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTPV.Text = frmClsBusqueda.TREG.Rows(0).Item(0)
            CARGAPRODUCTOVTA()
        End If
    End Sub
    Private Function CARGAPRODUCTOVTA() As Boolean
        If Not frmPrincipal.CHECACONX Then
            Exit Function
        End If
        Dim ENC As Boolean
        ENC = False
        Dim SQL As New SqlClient.SqlCommand("SELECT NOMBRE,FAMILIA,CLAVE FROM PRODUCTOS WHERE CLAVE=@CLA", frmPrincipal.CONX)
        SQL.Parameters.Add("@CLA", SqlDbType.VarChar).Value = TXTPV.Text
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        If LEC.Read Then
            LBLPV.Text = LEC(0)
            CARGAX(LGRU, CBGRU, LEC(1))
            CARGAX(LPRO, CBPROD, LEC(2))
            ENC = True
        End If
        LEC.Close()
        SQL.Dispose()
        If ENC Then
            CARGAMATRIX()
        End If
        Return ENC
    End Function
    Private Sub CARGAMATRIX()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        DGV.DataSource = LLENATABLA("SELECT D.CANTIDAD,U.NOMBRE UNIDAD,P.NOMBRE NPRODUCTO,A.NOMBRE NALMACEN,D.ALMACEN ,D.PV,D.PD FROM MATRIXDESCARGA D INNER JOIN SUCURSALES T ON D.SUCURSAL=T.CLAVE INNER JOIN PRODUCTOS PV ON D.PV=PV.CLAVE INNER JOIN ALMACENES A ON D.ALMACEN=A.CLAVE INNER JOIN PRODUCTOS P ON D.PD=P.CLAVE INNER JOIN UNIDADES U ON P.UNIDADINV=U.CLAVE  WHERE D.SUCURSAL='" + LTI(CBTI.SelectedIndex) + "' AND D.PV='" + TXTPV.Text + "' ORDER BY P.NOMBRE ", frmPrincipal.CadenaConexion)
        DGV.Columns(6).Visible = False
        DGV.Columns(5).Visible = False
        DGV.Columns(4).Visible = False
        ACTIVAR(False)
    End Sub
    Private Sub CBPROD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBPROD.SelectedIndexChanged
        Try          
            LBLUNI.Text = LUDC(CBPROD.SelectedIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TXTPV_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPV.KeyPress
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
            If String.IsNullOrEmpty(TXTPV.Text) Then
            Else
                CARGAPRODUCTOVTA()
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmClsBusqueda.BUSCAR("SELECT GRUPOINVENTARIO FAMILIA,CLAVE,NOMBRE,ACTIVO FROM PRODUCTOS ", " WHERE NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Productos de Inventario", "Nombre del Producto", "Producto(s)", 2, frmPrincipal.CadenaConexion, False)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            CARGAX(LGRU, CBGRU, frmClsBusqueda.TREG.Rows(0).Item(0))
            CARGAX(LPRO, CBPROD, frmClsBusqueda.TREG.Rows(0).Item(1))
        End If
    End Sub

    Private Sub BTNASIGNAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNASIGNAR.Click
        frmPrevisualizarMatrixDescarga.MOSTRAR(LTI(CBTI.SelectedIndex), CBTI.Text, "1")
    End Sub

    Private Sub BTNAGREGAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNAGREGAR.Click
        Dim CANT As Double
        Try
            CANT = CType(TXTCANT.Text, Double)
        Catch ex As Exception
            MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End Try
        If CANT <= 0 Then
            MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        'If Not CARGAPRODUCTOVTA() Then
        '    Exit Sub
        'End If
        Dim SQL As New SqlClient.SqlCommand("SPMATRIXDESCARGA", frmPrincipal.CONX)
        SQL.CommandType = CommandType.StoredProcedure
        SQL.Parameters.Add("@SUC", SqlDbType.VarChar).Value = LTI(CBTI.SelectedIndex)
        SQL.Parameters.Add("@PV", SqlDbType.VarChar).Value = TXTPV.Text
        SQL.Parameters.Add("@CANT", SqlDbType.Float).Value = CANT
        SQL.Parameters.Add("@ALM", SqlDbType.VarChar).Value = LALM(CBALM.SelectedIndex)
        SQL.Parameters.Add("@PD", SqlDbType.VarChar).Value = LPRO(CBPROD.SelectedIndex)
        SQL.Parameters.Add("@USU", SqlDbType.VarChar).Value = frmPrincipal.Usuario
        SQL.ExecuteNonQuery()
        CARGAMATRIX()
        CHECATABLA()
    End Sub

    Private Sub BTNCANCELAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        DGV.DataSource = Nothing
        ACTIVAR(True)
    End Sub

    Private Sub BTNQUITAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNQUITAR.Click
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim SQL As New SqlClient.SqlCommand("DELETE FROM MATRIXDESCARGA WHERE SUCURSAL='" + LTI(CBTI.SelectedIndex) + "' AND PV='" + DGV.Item(5, DGV.CurrentRow.Index).Value.ToString + "' AND PD='" + DGV.Item(6, DGV.CurrentRow.Index).Value.ToString + "'", frmPrincipal.CONX)
        SQL.ExecuteNonQuery()
        CARGAMATRIX()
        CHECATABLA()
    End Sub
End Class