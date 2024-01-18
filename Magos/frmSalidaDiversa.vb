Imports System.Data.SqlClient

Public Class frmSalidaDiversa
    Dim LEMP As New List(Of String)

    Dim DTP As New DataTable
    Dim DV2 As New DataView
    Dim DT2 As New DataTable
    Dim LGRU As New List(Of String)
    Dim LPRO As New List(Of String)
    Dim LCON As New List(Of String)
    Dim DT As New DataTable
    Dim CEDIS, NCEDIS, EMP As String

    Dim DV As New DataView

    Dim LEXIS As New List(Of String)
    Dim LUNIEX As New List(Of String)
    Dim LDES As New List(Of String)
    Dim LUDS As New List(Of String)
    Dim LUNI As New List(Of String)

    Private Sub frmSalidaDiversa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        Me.Icon = frmPrincipal.Icon
        If Not Me.LLAMACEDIS Then
            Me.Close()
        Else
            Me.DT.Columns.Add("Grupo")
            Me.DT.Columns.Add("Producto")
            Me.DT.Columns.Add("Cantidad")
            Me.DT.Columns.Add("Unidad")
            Me.DT.Columns.Add("Descripción")
            Me.DT.Columns.Add("CGrupo")
            Me.DT.Columns.Add("CProducto")
            Me.DT.Columns.Add("CUnidad")
            Dim empresa As String = frmPrincipal.Empresa
            Me.DT2 = LLENATABLA("SELECT E.PRODUCTO,E.UNIDAD,U.NOMBRE FROM EQUIVALENCIAPRODUCTO E INNER JOIN PRODUCTOS P ON E.UNIDAD2=P.UNIDADINV AND P.CLAVE=E.PRODUCTO INNER JOIN UNIDADES U ON E.UNIDAD=U.CLAVE", frmPrincipal.CadenaConexion)
            Me.Cursor = Cursors.WaitCursor
            Me.DTP = LLENATABLA(("SELECT P.CLAVE,P.NOMBRE,P.UNIDADINV,P.FAMILIA,C.EXISTENCIA,C.UNIDAD,P.DESCRIPCION,P.UNIDADINV FROM PRODUCTOS P FULL JOIN VEXISTENCIA C ON P.CLAVE=C.PRODUCTO AND C.CLAVE='" & Me.CEDIS & "' WHERE P.ACTIVO=1"), frmPrincipal.CadenaConexion)
            Me.Cursor = Cursors.Default

            LLENACOMBOBOX(CBCON, LCON, "SELECT CLAVE,NOMBRE FROM MOVIMIENTOSINVENTARIO WHERE ORIENTACION=2 AND ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
            LLENACOMBOBOX(CBGRU, LGRU, "SELECT CLAVE,NOMBRE FROM FAMILIAS WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
            Me.CHECATABLA()
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
            Me.Text = ("Salida Diversa para: " & Me.NCEDIS)
            Return True
        End If
        Return False
    End Function

    Private Function CARGANOSALIDA() As Integer
        Dim num As Integer
        If Not frmPrincipal.CHECACONX Then
            Return num
        End If
        Dim reader As SqlDataReader = New SqlCommand(("SELECT MAX(NOSALIDA)ORDEN FROM SALIDASALMACENES WHERE ALMACEN='" & Me.CEDIS & "'"), frmPrincipal.CONX).ExecuteReader
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
        Me.DV = New DataView(Me.DTP, ("FAMILIA='" & GRUPO & "'"), "NOMBRE", DataViewRowState.CurrentRows)
        Return Me.CARGAPRODUCTOS
    End Function



    Private Function CARGAPRODUCTOS() As Boolean
        Dim flag As Boolean
        Try
            Me.CBPROD.Items.Clear()
            Me.LPRO.Clear()
            Me.LEXIS.Clear()
            Me.LUNIEX.Clear()
            Me.LDES.Clear()
            Me.LUDS.Clear()
            Dim num2 As Integer = (Me.DV.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num2)
                Dim view As DataRowView = Me.DV.Item(i)
                Me.CBPROD.Items.Add(view.Item(1).ToString)
                Me.LPRO.Add(view.Item(0).ToString)
                If (view.Item(4) Is DBNull.Value) Then
                    Me.LEXIS.Add(0)
                Else
                    Me.LEXIS.Add(view.Item(4).ToString)
                End If
                If (view.Item(5) Is DBNull.Value) Then
                    Me.LUNIEX.Add("N/A")
                Else
                    Me.LUNIEX.Add(view.Item(5).ToString)
                End If
                Me.LDES.Add(view.Item(6).ToString)
                Me.LUDS.Add(view.Item(7).ToString)
                i += 1
            Loop
            Try
                Me.CBPROD.SelectedIndex = 0
                Return True
            Catch exception1 As Exception
                ' MessageBox.Show(exception1.Message)
                flag = False
                Return flag
            End Try
        Catch exception3 As Exception

            Return flag
        End Try
        Return flag
    End Function

    Private Sub ACTIVAR(ByVal V As Boolean)
        Try
            Me.TXTCANT.Enabled = V
            Me.CBUNI.Enabled = V
            Me.BTNAGREGAR.Enabled = V
        Catch exception1 As Exception
            Return
        End Try
    End Sub

    Private Sub CBGRU_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBGRU.SelectedIndexChanged
        If Not Me.CARGADATAVIEW(Me.LGRU.Item(Me.CBGRU.SelectedIndex)) Then
            MessageBox.Show("No Se encuentra Asignado Ningún Producto en este Grupo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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
        Try
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
                Return True
            Catch exception1 As Exception
                flag = False
                Return flag
            End Try
        Catch exception3 As Exception
            'frmPrincipal.CE.ESCRIBIR("Sailda inventario restaurantes", DateAndTime.Now, "ventana:salidainventariorestaurante metodo: CARGAEQUIVALENCIAS()", exception3.Message.ToString)
            Return flag
        End Try
        Return flag
    End Function

    Private Sub CBPROD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBPROD.SelectedIndexChanged
        Me.LBLDES.Text = Me.LDES.Item(Me.CBPROD.SelectedIndex)
        If Not Me.CARGADATAVIEW2(Me.LGRU.Item(Me.CBGRU.SelectedIndex), Me.LPRO.Item(Me.CBPROD.SelectedIndex)) Then
            MessageBox.Show("No Se encuentra Asignado Ninguna Equivalencia que Coincida con las Unidades de Entrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Me.ACTIVAR(False)
        Else
            Me.ACTIVAR(True)
            Me.LBLCANTEXIS.Text = Me.LEXIS.Item(Me.CBPROD.SelectedIndex).ToString
            Me.LBLUNIEXIS.Text = Me.LUNIEX.Item(Me.CBPROD.SelectedIndex).ToString
        End If
    End Sub

    Private Function CARGAEXISTENCIA(ByVal GRU As String, ByVal PRO As String, ByVal CANT As Double, ByVal UNI As String) As Boolean
        If Not frmPrincipal.CHECACONX Then
            Return False
        End If
        Dim cBGRU As ComboBox = Me.CBGRU
        MODULOGENERAL.CARGAX(Me.LGRU, cBGRU, GRU)
        Me.CBGRU = cBGRU
        cBGRU = Me.CBPROD
        MODULOGENERAL.CARGAX(Me.LPRO, cBGRU, PRO)
        Me.CBPROD = cBGRU
        If (UNI <> Me.LUNIEX.Item(Me.CBPROD.SelectedIndex)) Then
            Dim num As Double
            Dim reader As SqlDataReader = New SqlCommand(String.Concat(New String() {"SELECT DBO.TOTALUNIDADMINIMA2('", PRO, "',", CANT.ToString, ",'", UNI, "')"}), frmPrincipal.CONX).ExecuteReader
            If reader.Read Then
                num = Convert.ToDouble(reader.Item(0))
            End If
            reader.Close()
            If (num > Convert.ToDouble(Me.LEXIS.Item(Me.CBPROD.SelectedIndex))) Then
                MessageBox.Show("No cuenta con suficiente existencia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Me.TXTCANT.Focus()
                Me.TXTCANT.SelectAll()
                Return False
            End If
        ElseIf (CANT > Convert.ToDouble(Me.LEXIS.Item(Me.CBPROD.SelectedIndex))) Then
            MessageBox.Show("No cuenta con suficiente existencia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Me.TXTCANT.Focus()
            Me.TXTCANT.SelectAll()
            Return False
        End If
        Return True
    End Function

    Private Function PRODUCTOAGREGADO(ByVal GRU As String, ByVal PRO As String) As Boolean
        Dim num2 As Integer = (Me.DT.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num2)
            If DT.Rows.Item(i).Item(5) = GRU And DT.Rows.Item(i).Item(6) = PRO Then
                Return True
            End If
            i += 1
        Loop
        Return False
    End Function

    Private Sub BTNAGREGAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNAGREGAR.Click
        If (Me.TXTCANT.Text = "") Then
            MessageBox.Show("Especifique una Cantidad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Me.TXTCANT.Focus()
            Me.TXTCANT.SelectAll()
        Else
            Dim cANT As Double = Convert.ToDouble(Me.TXTCANT.Text)
            If (cANT <= 0) Then
                MessageBox.Show("Especifique una Cantidad Mayor a Cero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Me.TXTCANT.Focus()
                Me.TXTCANT.SelectAll()
            ElseIf (Convert.ToDouble(Me.LEXIS.Item(Me.CBPROD.SelectedIndex)) <= 0) Then
                MessageBox.Show("El producto No cuenta con existencia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Me.TXTCANT.Focus()
                Me.TXTCANT.SelectAll()
            ElseIf Me.CARGAEXISTENCIA(Me.LGRU.Item(Me.CBGRU.SelectedIndex), Me.LPRO.Item(Me.CBPROD.SelectedIndex), cANT, Me.LUNI.Item(Me.CBUNI.SelectedIndex)) Then
                If Me.PRODUCTOAGREGADO(Me.LGRU.Item(Me.CBGRU.SelectedIndex), Me.LPRO.Item(Me.CBPROD.SelectedIndex)) Then
                    MessageBox.Show("Este Producto Ya Ha Sido Agregado a esta salida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Else
                    Me.AGREGAR(Me.LGRU.Item(Me.CBGRU.SelectedIndex), Me.CBGRU.Text, Me.LPRO.Item(Me.CBPROD.SelectedIndex), Me.CBPROD.Text, Convert.ToDouble(Me.TXTCANT.Text), Me.CBUNI.Text, Me.LBLDES.Text, Me.LUNI.Item(Me.CBUNI.SelectedIndex))
                    Me.CHECATABLA()
                    Me.CBGRU.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub AGREGAR(ByVal GRUPO As String, ByVal NGRU As String, ByVal PRO As String, ByVal NPRO As String, ByVal CANT As Double, ByVal UNI As String, ByVal DES As String, ByVal CLAUNI As String)
        Try
            Dim DOW As System.Data.DataRow = DT.NewRow
            DOW(0) = NGRU
            DOW(1) = NPRO
            DOW(2) = CANT
            DOW(3) = UNI
            DOW(4) = DES
            DOW(5) = GRUPO
            DOW(6) = PRO
            DOW(7) = CLAUNI
            DT.Rows.Add(DOW)

            DGV.DataSource = DT
            DGV.Columns(7).Visible = False
            DGV.Columns(6).Visible = False
            DGV.Columns(5).Visible = False
            DGV.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGV.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGV.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            DGV.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGV.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            DGV.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            DGV.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            DGV.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
            DGV.Refresh()
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub BTNQUITAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNQUITAR.Click
        Me.DT.Rows.RemoveAt(Me.DGV.CurrentRow.Index)
        Me.CHECATABLA()
    End Sub

    Private Sub CHECATABLA()

        If (Me.DGV.Rows.Count = 0) Then
            Me.BTNQUITAR.Enabled = False
            Me.BTNELIMINAR.Enabled = False
            Me.BTNGUARDAR.Enabled = False
        Else
            Dim num2 As Integer = (Me.DGV.Rows.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num2)
                i += 1
            Loop
            Me.BTNGUARDAR.Enabled = True
            Me.BTNQUITAR.Enabled = True
            Me.BTNELIMINAR.Enabled = True
        End If
    End Sub

    Private Sub BTNELIMINAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNELIMINAR.Click
        Dim num As Short = CShort(MessageBox.Show("¿Esta seguro que desea Eliminar TODOS los elementos Agregados?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
        If (num = 6) Then
            Me.DT.Rows.Clear()
            Me.DGV.Refresh()
            Me.CHECATABLA()
        End If
    End Sub

    Private Function VERIFICAREXISTENCIAS() As Boolean
        Dim flag As Boolean = True
        Dim num2 As Integer = (Me.DT.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num2)
            If Not Me.CARGAEXISTENCIA(DT.Rows.Item(i).Item(5).ToString, DT.Rows.Item(i).Item(6).ToString, Convert.ToDouble(Me.DGV.Item(2, i).Value), DT.Rows.Item(i).Item(7).ToString) Then
                flag = False
            End If
            i += 1
        Loop
        Return flag
    End Function

    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        Dim num As Short = CShort(MessageBox.Show("¿Desea Guardar la Información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
        If (num = 6) Then
            Me.GUARDAR()
        End If
    End Sub

    Private Sub GUARDAR()
        If frmPrincipal.CHECACONX Then
            Dim num As Integer = Me.CARGANOSALIDA
            Dim command As New SqlCommand("INSERT INTO SALIDASALMACENES (ALMACEN,NOSALIDA,CONCEPTO,FECHA,USUARIO,OBSERVACION)VALUES (@ALM,@NOO,@CON,GETDATE(),@USU,@OBS)", frmPrincipal.CONX)
            command.Parameters.Add("@ALM", SqlDbType.VarChar).Value = Me.CEDIS
            command.Parameters.Add("@NOO", SqlDbType.Int).Value = num
            command.Parameters.Add("@CON", SqlDbType.Int).Value = Me.LCON.Item(Me.CBCON.SelectedIndex)
            command.Parameters.Add("@USU", SqlDbType.VarChar).Value = frmPrincipal.Usuario
            command.Parameters.Add("@OBS", SqlDbType.VarChar).Value = Me.TXTOBS.Text
            command.ExecuteNonQuery()
            Dim command2 As New SqlCommand("INSERT INTO DETALLESALIDASALMACENES (ALMACEN,NOSALIDA,PRODUCTO,CANTIDAD,UNIDAD,REGISTRO) VALUES (@ALM,@NOO,@PRO,@CANT,@UNI,@REG)", frmPrincipal.CONX)
            command2.Parameters.Add("@ALM", SqlDbType.Int).Value = Me.CEDIS
            command2.Parameters.Add("@NOO", SqlDbType.Int).Value = num
            command2.Parameters.Add("@PRO", SqlDbType.VarChar)
            command2.Parameters.Add("@CANT", SqlDbType.Float)
            command2.Parameters.Add("@UNI", SqlDbType.Int)
            command2.Parameters.Add("@REG", SqlDbType.Int)
            Dim num3 As Integer = (Me.DGV.Rows.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num3)

                command2.Parameters.Item("@PRO").Value = Me.DT.Rows.Item(i).Item(6).ToString
                command2.Parameters.Item("@CANT").Value = Me.DGV.Item(2, i).Value
                command2.Parameters.Item("@UNI").Value = Me.DT.Rows.Item(i).Item(7).ToString
                command2.Parameters.Item("@REG").Value = i
                command2.ExecuteNonQuery()
                i += 1
            Loop
            Dim command4 As New SqlCommand("KARDEXPORSALIDA", frmPrincipal.CONX) With {
                .CommandType = CommandType.StoredProcedure
            }
            command4.Parameters.Add("@ALM", SqlDbType.VarChar).Value = Me.CEDIS
            command4.Parameters.Add("@NOS", SqlDbType.Int).Value = num
            command4.ExecuteNonQuery()
            'Dim command3 As New SqlCommand("SPNOTIFICACIONMOVIMIENTOF", frmPrincipal.CONX) With {
            '    .CommandType = CommandType.StoredProcedure,
            '    .CommandTimeout = 600
            '}
            'command3.Parameters.Add("@MOV", SqlDbType.Int).Value = 3
            'command3.Parameters.Add("@ALM", SqlDbType.VarChar).Value = Me.CEDIS
            'command3.Parameters.Add("@NOO", SqlDbType.Int).Value = num
            'command3.ExecuteNonQuery()
            Me.DT.Rows.Clear()

            Me.DGV.Refresh()
            Me.CHECATABLA()
            Me.IMPRIMIR()

            Me.CARGANOSALIDA()
            Me.DTP = LLENATABLA(("SELECT P.CLAVE,P.NOMBRE,P.UNIDADINV,P.FAMILIA,C.EXISTENCIA,C.UNIDAD,P.DESCRIPCION,P.UNIDADINV FROM PRODUCTOS P FULL JOIN VEXISTENCIA C ON P.CLAVE=C.PRODUCTO AND C.CLAVE='" & Me.CEDIS & "' WHERE P.ACTIVO=1"), frmPrincipal.CadenaConexion)
            Me.LIMPIAR()
        End If
    End Sub

    Private Sub LIMPIAR()
        MODULOGENERAL.LimpiarForm(Me.Controls)
    End Sub

    Private Sub IMPRIMIR()
        Dim qUERY As String = String.Concat(New String() {"SELECT A.NOMBRE ALMACEN,E.NOSALIDA NOORDEN,C.NOMBRE CONCEPTO,AREA='',E.FECHA,US.NOMBRE USUARIO,P.NOMBRE PRODUCTO,SUM(D.CANTIDAD)CANTIDAD,U.NOMBRE UNIDAD,SUM(D.CANTIDAD*D.COSTOPROMEDIO) TOTAL,E.OBSERVACION OBSERVACIONES FROM SALIDASALMACENES E INNER JOIN ALMACENES A ON E.ALMACEN=A.CLAVE INNER JOIN SALIDASALMACENESLOTES D ON E.ALMACEN=D.ALMACEN AND E.NOSALIDA=D.NOSALIDA INNER JOIN MOVIMIENTOSINVENTARIO C ON E.CONCEPTO=C.CLAVE INNER JOIN LOTES L ON D.LOTE=L.CLAVE INNER JOIN PRODUCTOS P ON L.PRODUCTO=P.CLAVE  INNER JOIN UNIDADES U ON P.UNIDADINV=U.CLAVE INNER JOIN USUARIOS US ON E.USUARIO=US.USUARIO WHERE E.ALMACEN='", Me.CEDIS, "' AND E.NOSALIDA='", Me.LBLPED.Text, "' GROUP BY A.NOMBRE,E.NOSALIDA,C.NOMBRE,E.FECHA,US.NOMBRE,P.NOMBRE,U.NOMBRE,E.OBSERVACION"})
        Dim rEP As New rptSalidaDiversa
        MOSTRARREPORTE(rEP, ("Salida Diversa: " & Me.LBLPED.Text), MODULOGENERAL.LLENATABLA(qUERY, frmPrincipal.CadenaConexion), "")
    End Sub

    Private Sub BTNMOSTRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNMOSTRAR.Click
        frmClsBusqueda.BUSCAR("SELECT CLAVE,Nombre,FAMILIA,DESCRIPCION,ACTIVO FROM PRODUCTOS", " WHERE NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Productos", "Nombre del Producto", "Productos(s)", 1, frmPrincipal.CadenaConexion, False)
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

    Private Sub frmSalidaDiversa_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.F3) Then
            frmClsBusqueda.BUSCAR("SELECT CLAVE,Nombre,FAMILIA,DESCRIPCION,ACTIVO FROM PRODUCTOS", " WHERE NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Productos", "Nombre del Producto", "Productos(s)", 1, frmPrincipal.CadenaConexion, False)
            If (frmClsBusqueda.DialogResult = DialogResult.Yes) Then
                MODULOGENERAL.CARGAX(Me.LGRU, CBGRU, frmClsBusqueda.TREG.Rows.Item(0).Item(2).ToString)
                MODULOGENERAL.CARGAX(Me.LPRO, CBPROD, frmClsBusqueda.TREG.Rows.Item(0).Item(0).ToString)
                Me.TXTCANT.Focus()
                Me.TXTCANT.SelectAll()
            End If
        End If
    End Sub

    Private Sub CBGRU_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCANT.KeyPress, CBUNI.KeyPress, CBPROD.KeyPress, CBGRU.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim table As New DataTable
        'table = MODULOGENERAL.LLENATABLA(("select P.GRUPO,G.NOMBRE,P.CLAVE,P.NOMBRE,SUM(CANTIDAD),U.NOMBRE UNIDAD,P.DESCRIPCION,P.UNIDAD FROM INVALMACENES D INNER JOIN LOTESF L ON D.PRODUCTO=L.CLAVE INNER JOIN PRODUCTOSF P ON L.PRODUCTO=P.CLAVE  INNER JOIN GRUPOSINV G ON P.GRUPO=G.CLAVE INNER JOIN UNIDADES U ON P.UNIDAD=U.CLAVE WHERE D.ALMACEN='" & Me.CEDIS & "' GROUP BY P.GRUPO,G.NOMBRE,P.CLAVE,P.NOMBRE,U.NOMBRE,P.UNIDAD,P.DESCRIPCION"), frmPrincipal.CadenaConexion)
        'Dim num2 As Integer = (table.Rows.Count - 1)
        'Dim i As Integer = 0
        'Do While (i <= num2)
        '    Me.AGREGAR(table.Rows.Item(i).Item(0).ToString, table.Rows.Item(i).Item(1).ToString, table.Rows.Item(i).Item(2).ToString, table.Rows.Item(i).Item(3).ToString, Convert.ToDouble(table.Rows.Item(i).Item(4).ToString), table.Rows.Item(i).Item(5).ToString, table.Rows.Item(i).Item(6).ToString, table.Rows.Item(i).Item(7).ToString)
        '    i += 1
        'Loop
        'Me.CHECATABLA()
        'Me.CBGRU.Focus()
    End Sub
End Class