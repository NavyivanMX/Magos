Public Class frmStockAlmacenes
    Dim LALM As New List(Of String)
    Dim LGRU As New List(Of String)
    Private Sub frmStockAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        LLENACOMBOBOX(CBALM, LALM, "SELECT A.CLAVE,A.NOMBRE FROM ALMACENES A INNER JOIN EMPRESASGASTOS E ON A.EMPRESA=E.CLAVE WHERE A.ACTIVO=1 AND E.GRUPO='" + frmPrincipal.Empresa + "' ORDER BY A.NOMBRE", frmPrincipal.CadenaConexion)
        LLENACOMBOBOX2(CBGRU, LGRU, "SELECT CLAVE,NOMBRE FROM GRUPOSINV ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Todos los Grupos", "")
    End Sub
    Private Sub CARGADATOS()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim QUERY As String
        QUERY = "SELECT A.NOMBRE ALMACEN,G.NOMBRE GRUPO,P.NOMBRE PRODUCTO,SUM(D.MAXIMO)MAXIMO,SUM(D.MINIMO)MINIMO,U.NOMBRE UNIDAD,CONVERT(BIT,MAX(D.ACTIVO))ACTIVO,GUARDAR=CONVERT(BIT,1),D.PRODUCTO,D.GRUPO FROM VSTOCKSMAXIMOSYMINIMOS D INNER JOIN ALMACENES A ON D.ALMACEN=A.CLAVE INNER JOIN GRUPOSINV G ON D.GRUPO=G.CLAVE INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE INNER JOIN  UNIDADES U ON P.UNIDADINV=U.CLAVE WHERE D.ALMACEN='" + LALM(CBALM.SelectedIndex) + "'"
        If CBGRU.SelectedIndex <> 0 Then
            QUERY += " AND D.GRUPO='" + LGRU(CBGRU.SelectedIndex) + "'"
        End If
        QUERY += " GROUP BY A.NOMBRE,G.NOMBRE,P.NOMBRE,U.NOMBRE,D.PRODUCTO,D.GRUPO ORDER BY A.NOMBRE,G.NOMBRE,P.NOMBRE"
        DGV.DataSource = LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
        DGV.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(3).DefaultCellStyle = FORMATONUMERICO()
        DGV.Columns(4).DefaultCellStyle = FORMATONUMERICO()

        DGV.Columns(0).ReadOnly = True
        DGV.Columns(1).ReadOnly = True
        DGV.Columns(2).ReadOnly = True
        DGV.Columns(5).ReadOnly = True
        DGV.Columns(9).Visible = False
        DGV.Columns(8).Visible = False

    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        If V Then
            DGV.DataSource = Nothing
            DGV.Refresh()
        End If
        BTNMOSTRAR.Enabled = V
        BTNGUARDAR.Enabled = Not V
    End Sub

    Private Sub BTNMOSTRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNMOSTRAR.Click
        CARGADATOS()
    End Sub
    Private Sub CHECATABLA()

    End Sub
    Private Sub GUARDAR()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim SQL As New SqlClient.SqlCommand("SPSTOCKALMACENES", frmPrincipal.CONX)
        SQL.CommandType = CommandType.StoredProcedure
        SQL.Parameters.Add("@ALM", SqlDbType.VarChar).Value = LALM(CBALM.SelectedIndex)
        SQL.Parameters.Add("@PRO", SqlDbType.VarChar)
        SQL.Parameters.Add("@MAX", SqlDbType.Float)
        SQL.Parameters.Add("@MIN", SqlDbType.Float)
        SQL.Parameters.Add("@ACT", SqlDbType.Bit)
        For X = 0 To DGV.Rows.Count - 1
            If CType(DGV.Item(7, X).Value, Boolean) Then
                SQL.Parameters("@PRO").Value = DGV.Item(8, X).Value
                SQL.Parameters("@MAX").Value = DGV.Item(3, X).Value
                SQL.Parameters("@MIN").Value = DGV.Item(4, X).Value
                SQL.Parameters("@ACT").Value = DGV.Item(6, X).Value
                SQL.ExecuteNonQuery()
            End If
        Next
        MessageBox.Show("El stock de inventario ha sido guardado correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        CARGADATOS()
        'DGV.Refresh()
    End Sub

    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        Dim xyz As Short
        xyz = MessageBox.Show("¿Desea Guardar la Información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Sub
        End If
        GUARDAR()
    End Sub

End Class