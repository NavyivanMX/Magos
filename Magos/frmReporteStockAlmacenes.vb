Public Class frmReporteStockAlmacenes
    Dim LALM As New List(Of String)
    Dim LGRU As New List(Of String)
    Private Sub frmReporteStockAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        MODULOGENERAL.LLENACOMBOBOX(CBALM, Me.LALM, ("SELECT A.CLAVE,A.NOMBRE FROM ALMACENES A INNER JOIN EMPRESASGASTOS E ON A.EMPRESA=E.CLAVE WHERE A.ACTIVO=1 AND E.GRUPO='" & frmPrincipal.Empresa & "' ORDER BY A.NOMBRE"), frmPrincipal.CadenaConexion)
        MODULOGENERAL.LLENACOMBOBOX2(CBGRU, Me.LGRU, "SELECT CLAVE,NOMBRE FROM GRUPOSINV ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Todos los Grupos", "")

    End Sub
    Private Sub CARGADATOS()
        If frmPrincipal.CHECACONX Then
            Dim QUERY As String = ("select G.NOMBRE GRUPO,P.NOMBRE,D.MINIMO,D.MAXIMO,DBO.EXISTENCIAALMACEN(D.ALMACEN,D.PRODUCTO)EXISTENCIA,U.NOMBRE UNIDAD FROM STOCKALMACENES D INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE INNER JOIN GRUPOSINV G ON P.FAMILIA=G.CLAVE INNER JOIN UNIDADES U ON P.UNIDADINV=U.CLAVE WHERE D.ALMACEN='" & Me.LALM.Item(Me.CBALM.SelectedIndex) & "' AND D.ACTIVO=1")
            If (Me.CBGRU.SelectedIndex <> 0) Then
                QUERY = (QUERY & " AND G.CLAVE='" & Me.LGRU.Item(Me.CBGRU.SelectedIndex) & "'")
            End If
            QUERY = (QUERY & " ORDER BY G.NOMBRE,P.NOMBRE")
            Me.DGV.DataSource = MODULOGENERAL.LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
            Me.DGV.Columns.Item(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Me.DGV.Columns.Item(2).DefaultCellStyle = MODULOGENERAL.FORMATONUMERICO
            Me.DGV.Columns.Item(3).DefaultCellStyle = MODULOGENERAL.FORMATONUMERICO
            Me.DGV.Columns.Item(4).DefaultCellStyle = MODULOGENERAL.FORMATONUMERICO
            Me.CHECATABLA()
        End If

    End Sub

    Private Sub BTNMOSTRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNMOSTRAR.Click
        CARGADATOS()
    End Sub
    Private Sub CHECATABLA()
        Dim num5 As Integer = (Me.DGV.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num5)
            Dim num As Double = Convert.ToDouble(Me.DGV.Item(2, i).Value)
            Dim num2 As Double = Convert.ToDouble(Me.DGV.Item(3, i).Value)
            Dim num3 As Double = Convert.ToDouble(Me.DGV.Item(4, i).Value)
            If (num3 > num2) Then
                Me.DGV.Rows.Item(i).Cells.Item(4).Style = MODULOGENERAL.ESTILO(Color.DarkBlue, Color.White)
            End If
            If (num3 <= num) Then
                Me.DGV.Rows.Item(i).Cells.Item(4).Style = MODULOGENERAL.ESTILO(Color.Red, Color.Black)
            End If
            i += 1
        Loop
    End Sub



End Class