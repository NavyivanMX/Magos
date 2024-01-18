Public Class frmReporteInventario

    Private Sub frmReporteInventario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        CARGARDATOS()
    End Sub
    Private Sub CARGARDATOS()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim QUERY As String
        QUERY = "SELECT P.NOMBRE PRODUCTO,SUM(D.CANTIDAD)EXISTENCIA,P.CLAVE FROM INVALMACENES D INNER JOIN LOTES L ON D.PRODUCTO=L.CLAVE INNER JOIN PRODUCTOS P ON L.PRODUCTO=P.CLAVE WHERE ALMACEN=1 GROUP BY P.NOMBRE,P.CLAVE"
        DGV.DataSource = LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
        DGV.Columns(2).Visible = False
        DGV.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(1).DefaultCellStyle = FORMATONUMERICOND(3)
    End Sub
    Private Sub CARGARDETALLE(ByVal CP As String)
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim QUERY As String
        QUERY = "SELECT P.NOMBRECORTO PRODUCTO,D.CANTIDAD,U.NOMBRE UNIDAD,D.CANTIDAD*L.COSTO TOTAL FROM INVALMACENES D INNER JOIN LOTES L ON D.PRODUCTO=L.CLAVE INNER JOIN PRODUCTOS P ON L.PRODUCTO=P.CLAVE INNER JOIN UNIDADES U ON U.CLAVE=1 INNER JOIN ALMACENES A ON D.ALMACEN=A.CLAVE WHERE D.ALMACEN=1 AND P.CLAVE=" + CP
        DGV2.DataSource = LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
        DGV2.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV2.Columns(1).DefaultCellStyle = FORMATONUMERICOND(3)

        DGV2.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV2.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV2.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV2.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        'DGV2.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        'DGV2.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        'DGV2.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        'DGV2.Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV2.Columns(1).DefaultCellStyle = FORMATONUMERICOND(3)
        DGV2.Columns(3).DefaultCellStyle = FORMATONUMERICOND(3)
    End Sub

    Private Sub DGV_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV.CellDoubleClick
        If DGV.Rows.Count <= 0 Then
        Else
            CARGARDETALLE(DGV.Item(2, DGV.CurrentRow.Index).Value.ToString)
        End If
    End Sub

    Private Sub BTNMOSTRAR_Click(sender As Object, e As EventArgs) Handles BTNMOSTRAR.Click

    End Sub

    'Private Sub BTNIMPRIMIR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNIMPRIMIR.Click
    '    Dim REP As New rptReporteInventario
    '    Dim QUERY As String
    '    QUERY = "SELECT * FROM VRINVENTARIO ORDER BY PRODUCTO,TEMPORADA,LOTE"
    '    MOSTRARREPORTE(REP, "Reporte de Inventario", LLENATABLA(QUERY, frmPrincipal.CadenaConexion), "")
    'End Sub
End Class