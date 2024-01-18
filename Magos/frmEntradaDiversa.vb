Imports System.Data.SqlClient

Public Class frmEntradaDiversa
    Dim DTP As New DataTable
    Dim DV2 As New DataView
    Dim DT2 As New DataTable
    Dim LGRU As New List(Of String)
    Dim LPRO As New List(Of String)

    Dim LDES As New List(Of String)
    Dim LCON As New List(Of String)
    Dim LPROVE As New List(Of String)
    Dim LUNI As New List(Of String)

    Dim LAREA As New List(Of String)

    Dim CEDIS, NCEDIS, EMP As String
    Dim DV As New DataView
    Dim VLOTE As Integer
    Private Sub frmEntradaDiversa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        Me.DTP.Columns.Add("CANTIDAD")
        Me.DTP.Columns.Add("PRODUCTO")
        Me.DTP.Columns.Add("UNIDAD")
        Me.DTP.Columns.Add("TOTAL")
        Me.DTP.Columns.Add("COSTO UNITARIO")
        Me.DTP.Columns.Add("CPRODUCTO")
        Me.DTP.Columns.Add("CUNIDAD")

        If Me.LLAMACEDIS Then
 
            Me.Icon = frmPrincipal.Icon
            Dim QUERY As String
            LLENACOMBOBOX2(CBPROVE, LPROVE, "SELECT CLAVE,NOMBRE FROM PROVEEDORES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
            LLENACOMBOBOX(CBCON, Me.LCON, "SELECT CLAVE,NOMBRE FROM MOVIMIENTOSINVENTARIO WHERE ORIENTACION=1 AND ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
            QUERY = "SELECT P.CLAVE,P.NOMBRE,P.NOMBRECORTO,P.GRUPOINVENTARIO FAMILIA FROM PRODUCTOS P WHERE P.ACTIVO=1"
            frmPrincipal.DT = LLENATABLA((QUERY), frmPrincipal.CadenaConexion)

            QUERY = "SELECT E.PRODUCTO,E.UNIDAD,U.NOMBRE FROM EQUIVALENCIAPRODUCTO E INNER JOIN PRODUCTOS P ON E.UNIDAD2=P.UNIDADINV AND P.CLAVE=E.PRODUCTO INNER JOIN UNIDADES U ON E.UNIDAD=U.CLAVE"
            Me.DT2 = LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
            LLENACOMBOBOX(CBGRU, LGRU, "SELECT CLAVE,NOMBRE FROM GRUPOSINV WHERE ACTIVO=1 AND CLAVE IN (SELECT DISTINCT(GRUPOINVENTARIO) FROM PRODUCTOS WHERE ACTIVO=1) ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
            CARGANOENTRADA()
            ACTUALIZADGV()
        Else
            Me.Close()
        End If
    End Sub

    Private Function LLAMACEDIS() As Boolean
        Me.Opacity = 25
        Dim almacen As New frmLlamaAlmacen
        almacen.MOSTRAR(0)
        If (almacen.DialogResult = DialogResult.Yes) Then
            Me.CEDIS = almacen.ALM
            Me.NCEDIS = almacen.NALM
            Me.EMP = almacen.EMP
            Me.Text = ("Entrada Diversa para: " & Me.NCEDIS)
            Return True
        End If
               Return False
    End Function

    Private Function CARGANOENTRADA() As Integer
        Dim num As Integer
        If Not frmPrincipal.CHECACONX Then
            Return num
        End If
        Dim reader As SqlDataReader = New SqlCommand(("SELECT MAX(NOORDEN)ORDEN FROM ENTRADASALMACENES WHERE ALMACEN='" & Me.CEDIS & "'"), frmPrincipal.CONX).ExecuteReader
        If Not reader.Read Then
            Return num
        End If
        If (reader.Item(0) Is DBNull.Value) Then
            reader.Close()
            Me.LBLPED.Text = "1"
            Return 1
        End If
        Dim num2 As Integer = (Convert.ToInt32(reader.Item(0)) + 1)
        Me.LBLPED.Text = num2.ToString
        reader.Close()
        Return num2
    End Function

    Private Function CARGADATAVIEW(ByVal GRUPO As String) As Boolean
        Me.DV = New DataView(frmPrincipal.DT, ("FAMILIA='" & GRUPO & "'"), "NOMBRE", DataViewRowState.CurrentRows)
        Return Me.CARGAPRODUCTOS
    End Function

    Private Function CARGAPRODUCTOS() As Boolean
        Dim flag As Boolean
        Me.CBPROD.Items.Clear()
        Me.LPRO.Clear()
        LDES.Clear()

        Dim num3 As Integer = (Me.DV.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num3)
            Dim view As DataRowView = Me.DV.Item(i)
            Me.CBPROD.Items.Add(view.Item(2))
            Me.LPRO.Add(view.Item(0).ToString)
            Me.LDES.Add(view.Item(1).ToString)
            i += 1
        Loop
        Dim count As Integer = Me.CBPROD.Items.Count
        Try
            Me.CBPROD.SelectedIndex = 0
            flag = True
        Catch exception1 As Exception
            'MessageBox.Show(exception1.Message)
            flag = False
            Return flag
        End Try
        Return flag
    End Function

    Private Sub ACTIVAR(ByVal V As Boolean)
        Try
            Me.TXTCANT.Enabled = V
            'Me.CBUNI.Enabled = V
            Me.BTNAGREGAR.Enabled = V
        Catch exception1 As Exception
            'frmPrincipal.CE.ESCRIBIR("Sailda inventario restaurantes", DateAndTime.Now, "ventana:salidainventariorestaurante metodo: ACTIVAR", exception1.Message.ToString)
            Return
        End Try
    End Sub

    Private Sub CBGRU_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBGRU.SelectedIndexChanged
        Me.ACTIVAR(True)
        If Not Me.CARGADATAVIEW(Me.LGRU.Item(Me.CBGRU.SelectedIndex)) Then
            MessageBox.Show("No Se encuentra Asignado Ningún Producto en esta familia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Me.CBPROD.Enabled = False
            Me.ACTIVAR(False)
        Else
            Me.CBPROD.Enabled = True
            If Not Me.CARGADATAVIEW2(Me.LGRU.Item(Me.CBGRU.SelectedIndex), Me.LPRO.Item(Me.CBPROD.SelectedIndex)) Then
                MessageBox.Show("No Se encuentra Asignado Ninguna Equivalencia que Coincida con las Unidades de Entrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Me.ACTIVAR(False)
            Else
                Me.ACTIVAR(True)
            End If
        End If
    End Sub
    Private Function CARGADATAVIEW2(ByVal GRUPO As String, ByVal PRO As String) As Boolean
        Me.DV2 = New DataView(Me.DT2, ("PRODUCTO='" & PRO & "'"), "NOMBRE", DataViewRowState.CurrentRows)
        Return Me.CARGAEQUIVALENCIAS
    End Function


    Private Function CARGAEQUIVALENCIAS() As Boolean
        Dim flag As Boolean
        Me.LUNI.Clear()
        Me.CBUNI.Items.Clear()
        Dim num2 As Integer = (Me.DV2.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num2)
            Dim view As DataRowView = Me.DV2.Item(i)
            Me.CBUNI.Items.Add(view.Item(2))
            Me.LUNI.Add(view.Item(1).ToString)
            i += 1
        Loop
        Try
            Me.CBUNI.SelectedIndex = 0
            flag = True
        Catch exception1 As Exception
            flag = False
            Return flag
        End Try
        Return flag
    End Function
    Private Function PRODUCTOAGREGADO(ByVal PRO As String) As Boolean
        Dim num2 As Integer = (Me.DTP.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num2)
            If Me.DTP.Rows.Item(i).Item(5) = PRO Then
                Return True
            End If
            i += 1
        Loop
        Return False
    End Function

    Private Sub BTNQUITAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNQUITAR.Click
        Me.DTP.Rows.RemoveAt(Me.DGV.CurrentRow.Index)
        Me.CHECATABLA()
    End Sub

    Dim TOT As Double
    Private Sub CHECATABLA()
        TOT = 0
        If DGV.Rows.Count = 0 Then
            BTNQUITAR.Enabled = False
            BTNELIMINAR.Enabled = False
            BTNGUARDAR.Enabled = False
        Else
            Dim X As Integer
            For X = 0 To DGV.Rows.Count - 1

            Next

            BTNGUARDAR.Enabled = True
            BTNQUITAR.Enabled = True
            BTNELIMINAR.Enabled = True
        End If
    End Sub

    Private Sub BTNELIMINAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNELIMINAR.Click
        Dim num As Short = CShort(MessageBox.Show("¿Esta seguro que desea Eliminar TODOS los elementos Agregados?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
        If (num = 6) Then
            Me.DTP.Rows.Clear()
            Me.DGV.Refresh()
            Me.CHECATABLA()
        End If
    End Sub

    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        If CBPROVE.SelectedIndex = 0 Then
            MessageBox.Show("Debe seleccionar un proveedor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If Not VALIDACANTIDADES() Then
            MessageBox.Show("Favor de verificar las cantidades", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim num As Short = CShort(MessageBox.Show("¿Desea Guardar la Información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
        If (num = 6) Then
            Me.GUARDAR()
        End If
    End Sub
    Private Function VALIDACANTIDADES() As Boolean
        Dim X As Integer
        Dim CANT, TOT As Double
        For X = 0 To DGV.Rows.Count - 1
            Try
                CANT = DGV.Item(3, X).Value
            Catch ex As Exception
                CANT = 0
            End Try
            Try
                TOT = DGV.Item(5, X).Value
            Catch ex As Exception
                TOT = 0
            End Try
            If CANT <= 0 Then
                DGV.CurrentCell = DGV.Rows(X).Cells(3)
                Return False
            End If
            If TOT < 0 Then
                DGV.CurrentCell = DGV.Rows(X).Cells(5)
                Return False
            End If
        Next
        Return True
    End Function
    Private Function CARGALOTE(ByVal PRO As String) As String
        If Not frmPrincipal.CHECACONX Then
            Return ""
        End If
        Dim str2 As String = ""
        Dim command As New SqlCommand(("SELECT DBO.LOTEPRODUCTO('" & PRO & "')"), frmPrincipal.CONX)
        Dim reader As SqlDataReader = command.ExecuteReader
        If reader.Read Then
            str2 = reader.Item(0).ToString
        End If
        reader.Close()
        command.Dispose()
        Return str2
    End Function

    Private Function CARGALOTECON(ByVal PRO As String) As Integer
        If Not frmPrincipal.CHECACONX Then
            Return 0
        End If
        Dim num2 As Integer = 1
        Dim command As New SqlCommand(("SELECT DBO.LOTEPRODUCTOCON('" & PRO & "')"), frmPrincipal.CONX)
        Dim reader As SqlDataReader = command.ExecuteReader
        If reader.Read Then
            num2 = Convert.ToInt32(reader.Item(0))
        End If
        reader.Close()
        command.Dispose()
        Return num2
    End Function
    Dim NOO As Integer
    Private Sub GUARDAR()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If


        NOO = CARGANOENTRADA()


        Dim SQL As New SqlClient.SqlCommand("INSERT INTO ENTRADASALMACENES (ALMACEN,NOORDEN,CONCEPTO,TOTAL,FECHA,USUARIO,OBSERVACION,PROVEEDOR) VALUES (@ALM,@NOO,@CON,@TOT,@FEC,@USU,@OBS,@PROVE)", frmPrincipal.CONX)
        SQL.Parameters.Add("@ALM", SqlDbType.Int).Value = CEDIS
        SQL.Parameters.Add("@NOO", SqlDbType.Int).Value = NOO
        SQL.Parameters.Add("@CON", SqlDbType.Int).Value = LCON(CBCON.SelectedIndex)
        SQL.Parameters.Add("@TOT", SqlDbType.Float).Value = TOT
        SQL.Parameters.Add("@FEC", SqlDbType.DateTime).Value = DTFECHA.Value
        SQL.Parameters.Add("@USU", SqlDbType.VarChar).Value = frmPrincipal.Usuario
        SQL.Parameters.Add("@OBS", SqlDbType.VarChar).Value = TXTOBS.Text
        SQL.Parameters.Add("@PROVE", SqlDbType.Int).Value = LPROVE(CBPROVE.SelectedIndex)
        SQL.ExecuteNonQuery()

        Dim SQLL As New SqlCommand("INSERT INTO LOTES (CLAVE,PRODUCTO,FECHA,COSTO,PRECIO,PROVEEDOR,AÑO,SEMANA,REGISTRO,COSTOU,PRECIOU) VALUES (@LOTE,@PRO,GETDATE(),@COS,@PRE,@PROO,DATEPART(yy,GETDATE()),DATEPART(wk,GETDATE()),@REG,@CU,@PU)", frmPrincipal.CONX)
        SQLL.Parameters.Add("@LOTE", SqlDbType.VarChar)
        SQLL.Parameters.Add("@PRO", SqlDbType.VarChar)
        SQLL.Parameters.Add("@COS", SqlDbType.VarChar)
        SQLL.Parameters.Add("@PRE", SqlDbType.VarChar)
        SQLL.Parameters.Add("@PROO", SqlDbType.Int).Value = 0
        SQLL.Parameters.Add("@REG", SqlDbType.Int)
        SQLL.Parameters.Add("@CU", SqlDbType.Money)
        SQLL.Parameters.Add("@PU", SqlDbType.Money)

        Dim SQLD As New SqlCommand("INSERT INTO DETALLEENTRADASALMACENES (ALMACEN,NOORDEN,LOTE,CANTIDAD,UNIDAD,TOTAL,REGISTRO) VALUES (@ALM,@NOO,@LOTE,@CANT,@UNI,@TOT,@REG)", frmPrincipal.CONX)
        SQLD.Parameters.Add("@ALM", SqlDbType.Int).Value = CEDIS
        SQLD.Parameters.Add("@NOO", SqlDbType.Int).Value = NOO
        SQLD.Parameters.Add("@UNI", SqlDbType.Int).Value = 1

        SQLD.Parameters.Add("@LOTE", SqlDbType.VarChar)
        SQLD.Parameters.Add("@CANT", SqlDbType.Float)
        SQLD.Parameters.Add("@TOT", SqlDbType.Float)
        SQLD.Parameters.Add("@REG", SqlDbType.Int)
        Dim X As Integer
        Dim COS As Double
        For I = 0 To DGV.Rows.Count - 1

            Dim str2 As String = Me.CARGALOTE(DGV.Item(5, I).Value)
            Dim num As Integer = Me.CARGALOTECON(DGV.Item(5, I).Value)
            SQLL.Parameters.Item("@LOTE").Value = str2
            SQLL.Parameters.Item("@REG").Value = num
            SQLL.Parameters.Item("@PRO").Value = Me.DGV.Item(5, I).Value
            SQLL.Parameters.Item("@COS").Value = Me.DGV.Item(3, I).Value
            SQLL.Parameters.Item("@PRE").Value = Me.DGV.Item(3, I).Value
            SQLL.Parameters.Item("@CU").Value = Me.DGV.Item(4, I).Value
            SQLL.Parameters.Item("@PU").Value = Me.DGV.Item(4, I).Value

            Try
                SQLL.ExecuteNonQuery()
            Catch exception1 As Exception
            End Try

            SQLD.Parameters.Item("@LOTE").Value = str2
            SQLD.Parameters.Item("@CANT").Value = Me.DGV.Item(0, I).Value
            SQLD.Parameters.Item("@UNI").Value = Me.DGV.Item(6, I).Value
            SQLD.Parameters.Item("@TOT").Value = Me.DGV.Item(3, I).Value
            SQLD.Parameters.Item("@REG").Value = I
            SQLD.ExecuteNonQuery()
        Next
        Dim SQLK As New SqlClient.SqlCommand("KARDEXPORENTRADA", frmPrincipal.CONX)
        SQLK.CommandType = CommandType.StoredProcedure
        SQLK.CommandTimeout = 300
        SQLK.Parameters.Add("@ALM", SqlDbType.Int).Value = CEDIS
        SQLK.Parameters.Add("@NOC", SqlDbType.Int).Value = NOO
        SQLK.ExecuteNonQuery()

        MessageBox.Show("La información ha sido guardada con éxito", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        IMPRIMIR()
        LIMPIAR()
        DTP.Rows.Clear()
        ACTUALIZADGV()
        TXTCANT.Text = ""
        TXTTOT.Text = ""

        CARGANOENTRADA()

        CBPROD.Focus()
    End Sub

    Private Sub LIMPIAR()
        MODULOGENERAL.LimpiarForm(Me.Controls)
        Me.TXTCANT.Text = ""
        Me.TXTOBS.Text = ""
        Me.TXTTOT.Text = ""
    End Sub

    Private Sub IMPRIMIR()
        Dim rEP As New rptEntradaDiversa
        Dim QUERY As String
        QUERY = "SELECT A.NOMBRE ALMACEN,E.NOORDEN,C.NOMBRE CONCEPTO,E.FECHA,US.NOMBRE USUARIO,P.NOMBRE PRODUCTO,D.CANTIDAD,U.NOMBRE UNIDAD,D.TOTAL,E.OBSERVACION OBSERVACIONES FROM ENTRADASALMACENES E INNER JOIN ALMACENES A ON E.ALMACEN=A.CLAVE INNER JOIN DETALLEENTRADASALMACENES D ON E.ALMACEN=D.ALMACEN AND E.NOORDEN=D.NOORDEN INNER JOIN MOVIMIENTOSINVENTARIO C ON E.CONCEPTO=C.CLAVE INNER JOIN LOTES L ON D.LOTE=L.CLAVE INNER JOIN PRODUCTOS P ON L.PRODUCTO=P.CLAVE INNER JOIN UNIDADES U ON D.UNIDAD=U.CLAVE INNER JOIN USUARIOS US ON E.USUARIO=US.USUARIO INNER JOIN FAMILIAS G ON P.FAMILIA=G.CLAVE WHERE E.ALMACEN='" + CEDIS + "' AND E.NOORDEN = " + NOO.ToString
        MODULOGENERAL.MOSTRARREPORTE(rEP, ("Entrada Diversa " & Me.LBLPED.Text), MODULOGENERAL.LLENATABLA(QUERY, frmPrincipal.CadenaConexion), "")
    End Sub

    Private Sub frmEntradaDiversa_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.F3) Then
            frmClsBusqueda.BUSCAR("SELECT CLAVE,Nombre,FAMILIA,ACTIVO FROM PRODUCTOS", " WHERE NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Productos", "Nombre del Producto", "Productos(s)", 1, frmPrincipal.CadenaConexion, False)
            If (frmClsBusqueda.DialogResult = DialogResult.Yes) Then
                Dim cBGRU As ComboBox = Me.CBGRU
                MODULOGENERAL.CARGAX(Me.LGRU, cBGRU, frmClsBusqueda.TREG.Rows.Item(0).Item(2).ToString)
                Me.CBGRU = cBGRU
                cBGRU = Me.CBPROD
                MODULOGENERAL.CARGAX(Me.LPRO, cBGRU, frmClsBusqueda.TREG.Rows.Item(0).Item(0).ToString)
                Me.CBPROD = cBGRU
                Me.TXTCANT.Focus()
                Me.TXTCANT.SelectAll()
            End If
        End If
    End Sub

    Private Sub BTNMOSTRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNMOSTRAR.Click
        frmClsBusqueda.BUSCAR("SELECT CLAVE,Nombre,GRUPOINVENTARIO FAMILIA,ACTIVO FROM PRODUCTOS", " WHERE NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Productos", "Nombre del Producto", "Productos(s)", 1, frmPrincipal.CadenaConexion, False)
        If (frmClsBusqueda.DialogResult = DialogResult.Yes) Then
            Dim cBGRU As ComboBox = Me.CBGRU
            MODULOGENERAL.CARGAX(Me.LGRU, cBGRU, frmClsBusqueda.TREG.Rows.Item(0).Item(2).ToString)
            Me.CBGRU = cBGRU
            cBGRU = Me.CBPROD
            MODULOGENERAL.CARGAX(Me.LPRO, cBGRU, frmClsBusqueda.TREG.Rows.Item(0).Item(0).ToString)
            Me.CBPROD = cBGRU
            Me.TXTCANT.Focus()
            Me.TXTCANT.SelectAll()
        End If
    End Sub

    Private Sub ACTUALIZADGV()
        DGV.DataSource = DTP
        'DGV.Columns(11).Visible = False
        'DGV.Columns(10).Visible = False
        DGV.Columns(6).Visible = False
        DGV.Columns(5).Visible = False
        DGV.Columns(0).ReadOnly = True
        DGV.Columns(1).ReadOnly = True
        DGV.Columns(2).ReadOnly = True
        DGV.Columns(3).ReadOnly = True
        DGV.Columns(4).ReadOnly = True

        DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        'DGV.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        DGV.Columns(0).DefaultCellStyle = FORMATONUMERICOND(3)
        DGV.Columns(3).DefaultCellStyle = FORMATONUMERICO()
        DGV.Columns(4).DefaultCellStyle = FORMATONUMERICO()

        CHECATABLA()
    End Sub

    Private Sub CBGRU_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CBPROD.KeyPress, CBGRU.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub CBPROD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBPROD.SelectedIndexChanged
        Try
            LBLDES.Text = LDES(CBPROD.SelectedIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BTNAGREGAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNAGREGAR.Click
        AGREGAR()
    End Sub
    Private Function CALCULACOSTOU() As Double
        Dim num As Double
        Try
            Dim num2 As Double
            Dim num3 As Double
            Dim reader As SqlDataReader = New SqlCommand(String.Concat(New String() {"SELECT E.UNIDAD,U.NOMBRE,E.CANTIDAD,E.CANTIDAD2 FROM EQUIVALENCIAPRODUCTO E INNER JOIN UNIDADES U  ON E.UNIDAD=U.CLAVE INNER JOIN PRODUCTOS P ON E.PRODUCTO=P.CLAVE AND E.UNIDAD2=P.UNIDADINV WHERE E.PRODUCTO='", Me.LPRO.Item(Me.CBPROD.SelectedIndex), "' AND E.UNIDAD='", Me.LUNI.Item(Me.CBUNI.SelectedIndex), "'"}), frmPrincipal.CONX).ExecuteReader
            If reader.Read Then
                num2 = Convert.ToDouble(reader.Item(2))
                num3 = Convert.ToDouble(reader.Item(3))
            End If
            reader.Close()
            Dim num5 As Double = Convert.ToDouble(Me.TXTTOT.Text)
            Dim num4 As Double = Convert.ToDouble(Me.TXTCANT.Text)
            Dim num6 As Double = (num5 / ((num4 / num2) * num3))
            num = Math.Round(num6, 3, MidpointRounding.AwayFromZero)
        Catch exception1 As Exception
            'frmPrincipal.CE.ESCRIBIR("inventario restaurantes", DateAndTime.Now, "ventana:compras restaurantes metodo:CALCULACOSTOU()", exception1.Message.ToString)
            Return num
        End Try
        Return num
    End Function
    Private Sub AGREGAR()
        If PRODUCTOAGREGADO(LPRO(CBPROD.SelectedIndex)) Then
            MessageBox.Show("Producto ya agregado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim CANT, COS As Double
        Try
            CANT = CType(TXTCANT.Text, Double)
            COS = CType(TXTTOT.Text, Double)
        Catch ex As Exception
            MessageBox.Show("Cantidades no válidas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        If CANT <= 0 Then
            MessageBox.Show("Cantidades no válidas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim DOW As System.Data.DataRow = DTP.NewRow
        DOW(0) = CANT
        DOW(1) = CBPROD.Text
        DOW(2) = CBUNI.Text
        DOW(3) = COS
        DOW(4) = CALCULACOSTOU()
        DOW(5) = LPRO(CBPROD.SelectedIndex)
        DOW(6) = LUNI(CBUNI.SelectedIndex)


        DTP.Rows.Add(DOW)
        ACTUALIZADGV()
        TXTCANT.Text = ""
        TXTTOT.Text = ""
        CBPROD.Focus()
    End Sub

    Private Sub TOTALES()
        Dim X As Integer
        For X = 0 To DGV.Rows.Count - 1
            DGV.Item(5, X).Value = DGV.Item(3, X).Value * DGV.Item(4, X).Value
        Next
    End Sub
    Private Sub DGV_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV.CellEndEdit
        TOTALES()
    End Sub
End Class