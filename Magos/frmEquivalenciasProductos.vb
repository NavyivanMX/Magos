Public Class frmEquivalenciasProductos
    Dim CLAPROD As New List(Of String)
    Dim CLAGPO As New List(Of String)
    Dim CLAUNI1 As New List(Of String)
    Dim CLAUNI2 As New List(Of String)
    Dim TABLAPRIN As New DataTable
    Dim X As Integer
    Dim PEDIDO As Integer
    Dim ACTUALIZA As Boolean
    Dim ESRESTAURANTE As Boolean
    Dim VMOSTRAR As Boolean
    Dim VGPO, VPRO As String
    Public Sub MOSTRAR(ByVal GPO As String, ByVal PRO As String)
        VGPO = GPO
        VPRO = PRO
        VMOSTRAR = True
        Me.ShowDialog()
    End Sub
    Private Sub frmEquivalenciasProductos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        CARGAGRUPO()
        CARGAPRODUCTO()
        CARGAUNIDAD1()
        'CargaNoPedido()
        TABLAPRIN.Columns.Clear()
        TABLAPRIN.Columns.Add("Grupo")
        TABLAPRIN.Columns.Add("Producto")
        TABLAPRIN.Columns.Add("Cantidad1")
        TABLAPRIN.Columns.Add("Unidad1")
        TABLAPRIN.Columns.Add("Cantidad2")
        TABLAPRIN.Columns.Add("Unidad2")
        CHECATABLA()
        ACTIVAR(False)
        If VMOSTRAR Then
            CARGAX(CLAGPO, CBGPO, VGPO)
            CARGAX(CLAPROD, CBPROD, VPRO)
            CARGADATOS()
        End If
    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        TXTCANT1.Enabled = V
        TXTCANT2.Enabled = V
        CBUNI1.Enabled = V
        CBUNI2.Enabled = V
        BTNAGREGAR.Enabled = V
        BTNQUITAR.Enabled = V
        BTNELIMINAR.Enabled = V
        BTNGUARDAR.Enabled = V
        If V Then
            CHECATABLA()
        End If
    End Sub
    Private Function CARGAPRODUCTO() As Boolean
        If Not frmPrincipal.CHECACONX() Then
            Exit Function
        End If
        CBPROD.Items.Clear()
        CLAPROD.Clear()

        Dim SQLCARGAPROD As New SqlClient.SqlCommand("SELECT CLAVE,NOMBRE FROM PRODUCTOS WHERE FAMILIA='" + CLAGPO(CBGPO.SelectedIndex) + "'  AND ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CONX)
        Dim LECTOR2 As SqlClient.SqlDataReader
        LECTOR2 = SQLCARGAPROD.ExecuteReader
        While LECTOR2.Read
            CLAPROD.Add(LECTOR2(0))
            CBPROD.Items.Add(LECTOR2(1))
        End While
        LECTOR2.Close()

        Try
            CBPROD.SelectedIndex = 0
            Return True
        Catch ex As Exception
        End Try
        Return False
    End Function
    Private Sub CHECATABLA()
        If DGV.Rows.Count = 0 Then
            BTNQUITAR.Enabled = False
            BTNELIMINAR.Enabled = False
            BTNGUARDAR.Enabled = False
        Else
            BTNGUARDAR.Enabled = True
            BTNQUITAR.Enabled = True
            BTNELIMINAR.Enabled = True
        End If
        'Dim X As Integer
        'Dim TOT As Double
        'For X = 0 To DGV.Rows.Count - 1
        '    TOT = TOT + CType(DGV.Item(6, X).Value, Double)
        'Next
        'LBLTOT.Text = TOT.ToString
        'LBLCUANTOS.Text = DGV.Rows.Count
    End Sub
    Private Sub CBPROD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBPROD.SelectedIndexChanged
        LBLPROD.Text = CLAPROD(CBPROD.SelectedIndex)
    End Sub
    Private Function CARGAGRUPO() As Boolean
        If Not frmPrincipal.CHECACONX() Then
            Exit Function
        End If
        CBPROD.Items.Clear()
        CLAGPO.Clear()

        Dim SQLCARGAGPO As New SqlClient.SqlCommand("SELECT CLAVE,NOMBRE FROM FAMILIAS WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLCARGAGPO.ExecuteReader
        While LECTOR.Read
            CLAGPO.Add(LECTOR(0))
            CBGPO.Items.Add(LECTOR(1))
        End While
        LECTOR.Close()
        Try
            CBGPO.SelectedIndex = 0
            Return True
        Catch ex As Exception
        End Try
        Return False

    End Function

    Private Sub CBGPO_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBGPO.SelectedIndexChanged
        LBLGPO.Text = CLAGPO(CBGPO.SelectedIndex)
        CARGAPRODUCTO()
    End Sub
    Private Function CARGAUNIDAD1() As Boolean
        If Not frmPrincipal.CHECACONX() Then
            Exit Function
        End If
        CBUNI1.Items.Clear()
        CLAUNI1.Clear()
        CBUNI2.Items.Clear()
        CLAUNI2.Clear()

        Dim SQLCARGAUNI1 As New SqlClient.SqlCommand("SELECT CLAVE,NOMBRE FROM UNIDADES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLCARGAUNI1.ExecuteReader
        While LECTOR.Read
            CLAUNI1.Add(LECTOR(0))
            CBUNI1.Items.Add(LECTOR(1))
            CLAUNI2.Add(LECTOR(0))
            CBUNI2.Items.Add(LECTOR(1))
        End While
        LECTOR.Close()

        Try
            CBUNI1.SelectedIndex = 0
            CBUNI2.SelectedIndex = 0
            Return True
        Catch ex As Exception
        End Try
        Return False

    End Function

    Private Function CLAVEEQUIVALENCIA(ByVal UNI As String)
        Dim X As Integer
        For X = 0 To CBUNI1.Items.Count - 1
            If CBUNI1.Items.Item(X) = UNI Then
                Return CLAUNI1(X)
            End If
        Next
        Return 0
    End Function


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBUNI1.SelectedIndexChanged
        LBLUNI1.Text = CLAUNI1(CBUNI1.SelectedIndex)
    End Sub

    Private Sub CBUNI2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBUNI2.SelectedIndexChanged
        LBLUNI2.Text = CLAUNI2(CBUNI2.SelectedIndex)
    End Sub
    Private Sub AGREGAR()
        If TXTCANT1.Text = "" Or TXTCANT2.Text = "" Then
            MessageBox.Show("Especifique una cantidad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If CType(TXTCANT1.Text, Double) = 0 Or CType(TXTCANT2.Text, Double) = 0 Then
            MessageBox.Show("Especifique una cantidad mayor a Cero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim DOW As System.Data.DataRow = TABLAPRIN.NewRow
        DOW(0) = CBGPO.Text
        DOW(1) = CBPROD.Text

        DOW(2) = TXTCANT1.Text
        DOW(3) = CBUNI1.Text

        DOW(4) = TXTCANT2.Text
        DOW(5) = CBUNI2.Text

        TABLAPRIN.Rows.Add(DOW)
        DGV.DataSource = TABLAPRIN
        DGV.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Refresh()
        CHECATABLA()
        TXTCANT1.Text = 0
        TXTCANT2.Text = 0
        CBGPO.Enabled = False
        CBPROD.Enabled = False

        'TXTTELA.Focus()
        'TXTTELA.SelectAll()
        'ACTIVAR(True)
        BTNGUARDAR.Enabled = True
    End Sub
    'Private Sub CargaNoPedido()
    '    frmPrincipal.CHECACONX()
    '    Dim SQLCARGAPEDIDO As New SqlClient.SqlCommand("SELECT MAX(REGISTRO)MAXIMOREGISTRO FROM EQUIVALENCIAPRODUCTO", frmPrincipal.CONX)
    '    Dim LECTOR As SqlClient.SqlDataReader
    '    LECTOR = SQLCARGAPEDIDO.ExecuteReader
    '    If LECTOR.Read Then
    '        If LECTOR(0) Is DBNull.Value Then
    '            LBLPED.Text = 1
    '        Else
    '            Dim NUM As Integer
    '            NUM = CType(LECTOR(0), Integer)
    '            NUM = NUM + 1
    '            LBLPED.Text = NUM.ToString
    '            PEDIDO = NUM
    '        End If
    '    End If
    '    LECTOR.Close()
    'End Sub

  

    Private Sub BTNELIMINAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNELIMINAR.Click
        Dim xyz As Short
        xyz = MessageBox.Show("¿Esta seguro que desea Eliminar TODOS los elementos Agregados?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If xyz = 6 Then
            TABLAPRIN.Rows.Clear()
            DGV.Refresh()
            CHECATABLA()
            ACTUALIZA = False
        End If
    End Sub

   

    Private Sub TXTCANT1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCANT1.KeyPress
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
    End Sub

    Private Sub TXTCANT2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCANT2.KeyPress
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
    End Sub
    Private Function CARGAREGISTRO() As Integer
        'Try
        frmPrincipal.CHECACONX()
        Dim NUM As Integer
        NUM = 1
        Dim SQLMOV As New SqlClient.SqlCommand("SELECT DBO.SIGUIENTEREGISTROEQUIVALENCIA('" + CLAPROD(CBPROD.SelectedIndex) + "')", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLMOV.ExecuteReader
        If LECTOR.Read Then
            NUM = LECTOR(0)
            LECTOR.Close()
            Return NUM
        Else
            LECTOR.Close()
        End If
        Return NUM
        'Catch ex As Exception
        '    Me.Close()
        'End Try
    End Function
    Private Sub GUARDAR()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If

        Dim SQLGUARDA As New SqlClient.SqlCommand("", frmPrincipal.CONX)
        Dim SQLBORRA As New SqlClient.SqlCommand("", frmPrincipal.CONX)
        SQLGUARDA.CommandType = CommandType.StoredProcedure

        SQLBORRA.CommandType = CommandType.Text
        SQLBORRA.CommandText = "DELETE FROM  EQUIVALENCIAPRODUCTO WHERE PRODUCTO=" + LBLPROD.Text


        SQLBORRA.ExecuteNonQuery()


        SQLGUARDA.Parameters.Add("@PROD", SqlDbType.VarChar).Value = LBLPROD.Text
        SQLGUARDA.Parameters.Add("@CANT", SqlDbType.Float)
        SQLGUARDA.Parameters.Add("@UNI", SqlDbType.VarChar)
        SQLGUARDA.Parameters.Add("@CANT2", SqlDbType.Float)
        SQLGUARDA.Parameters.Add("@UNI2", SqlDbType.VarChar)
        SQLGUARDA.Parameters.Add("@REG", SqlDbType.Int)

        Dim REG As Integer
        REG = CARGAREGISTRO()


        For X = 0 To DGV.Rows.Count - 1
            SQLGUARDA.Parameters("@CANT").Value = DGV.Item(2, X).Value.ToString
            SQLGUARDA.Parameters("@UNI").Value = CLAVEEQUIVALENCIA(DGV.Item(3, X).Value.ToString)
            SQLGUARDA.Parameters("@CANT2").Value = DGV.Item(4, X).Value.ToString
            SQLGUARDA.Parameters("@UNI2").Value = CLAVEEQUIVALENCIA(DGV.Item(5, X).Value.ToString)
            SQLGUARDA.Parameters("@REG").Value = (REG + X).ToString
            SQLGUARDA.CommandText = "AGREGAEQUIVALENCIA"
            SQLGUARDA.ExecuteNonQuery()
        Next
        TABLAPRIN.Rows.Clear()
        MessageBox.Show("La información ha sido guardada correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


        CBGPO.Enabled = True
        CBPROD.Enabled = True
        TXTCANT1.Focus()
        BTNBUSCAR.Enabled = True
        ACTIVAR(False)
    End Sub

    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        Dim x As Short
        x = MessageBox.Show("¿Desea guardar la información?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If x = 6 Then
            GUARDAR()
        End If

    End Sub

    Private Sub CARGADATOS()
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        ACTIVAR(True)
        Dim SQLSELECT As New SqlClient.SqlCommand("", frmPrincipal.CONX)

        SQLSELECT.CommandText = "SELECT G.NOMBRE,P.NOMBRE,E.CANTIDAD,U.NOMBRE,E.CANTIDAD2,L.NOMBRE  FROM EQUIVALENCIAPRODUCTO E INNER JOIN PRODUCTOS P  ON P.CLAVE=E.PRODUCTO  INNER JOIN FAMILIAS G  ON G.CLAVE=p.FAMILIA  INNER JOIN UNIDADES U ON E.UNIDAD=U.CLAVE INNER JOIN LASUNIDADES L  ON E.UNIDAD2=L.CLAVE WHERE  E.PRODUCTO='" + CLAPROD(CBPROD.SelectedIndex) + "'"
       

        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLSELECT.ExecuteReader
        While LECTOR.Read
            Dim DOW As System.Data.DataRow = TABLAPRIN.NewRow
            DOW(0) = LECTOR(0)
            DOW(1) = LECTOR(1)
            DOW(2) = LECTOR(2)
            DOW(3) = LECTOR(3)
            DOW(4) = LECTOR(4)
            DOW(5) = LECTOR(5)
            TABLAPRIN.Rows.Add(DOW)
        End While
        DGV.DataSource = TABLAPRIN
        DGV.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Refresh()

        LECTOR.Close()

        CBGPO.Enabled = False
        CBPROD.Enabled = False
        TXTCANT1.Focus()
        BTNBUSCAR.Enabled = False
        CHECATABLA()
    End Sub
    Private Sub BTNBUSCAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNBUSCAR.Click
        CARGADATOS()
    End Sub

    Private Sub BTNCANCELAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        Dim x As Short
        x = MessageBox.Show("¿Esta seguro que desea cancelar?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If x = 6 Then
            CHECATABLA()
            TABLAPRIN.Rows.Clear()
            CBGPO.Enabled = True
            CBPROD.Enabled = True
            BTNBUSCAR.Enabled = True
            ACTIVAR(False)
        End If
    End Sub

    Private Sub BTNAGREGAR_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNAGREGAR.Click
        AGREGAR()
    End Sub

    Private Sub BTNQUITAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNQUITAR.Click
        TXTCANT1.Text = DGV.Item(2, DGV.CurrentRow.Index).Value
        CBUNI1.SelectedIndex = 0
        TXTCANT2.Text = DGV.Item(4, DGV.CurrentRow.Index).Value
        CBUNI2.SelectedIndex = 0
        TABLAPRIN.Rows.RemoveAt(DGV.CurrentRow.Index)
        CHECATABLA()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmClsBusqueda.BUSCAR("SELECT P.CLAVE,P.FAMILIA,P.NOMBRE,U.NOMBRE [UNIDAD INVENTARIO],LU.NOMBRE [UNIDAD COMPRA],P.IVA,P.ACTIVO   FROM PRODUCTOS P INNER JOIN UNIDADES U ON U.CLAVE=P.UNIDADINV INNER JOIN UNIDADES LU ON LU.CLAVE=P.UNIDADCOMPRA ", " WHERE P.NOMBRE", " ORDER BY P.NOMBRE", "Búsqueda de productos", "Nombre del producto", "Producto(s)", 2, frmPrincipal.CadenaConexion, False)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            CARGAX(CLAGPO, CBGPO, frmClsBusqueda.TREG.Rows(0).Item(1).ToString)
            CARGAX(CLAPROD, CBPROD, frmClsBusqueda.TREG.Rows(0).Item(0).ToString)
            CARGADATOS()
        End If
    End Sub
End Class