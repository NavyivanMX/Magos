Public Class frmPrevisualizarMatrixDescarga

    Private Sub frmPrevisualizarMatrixDescarga_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
    End Sub
    Public Sub MOSTRAR(ByVal CTI As String, ByVal NTI As String, ByVal EMP As String)
        Dim str As String
        Me.LBLNTI.Text = NTI
        If (EMP = "MBM") Then
            str = ("SELECT T.NOMBRECOMUN TIENDA,D.PRODUCTOVTA CODIGOVTA,PV.NOMBRE PRODUCTOVTA,D.CANTIDAD,U.NOMBRE UNIDAD,D.ALMACEN,A.NOMBRE NALMACEN,D.PRODUCTO,D.GRUPO,P.NOMBRE NPRODUCTO,D.GENERAPRODUCCION, D.DESRECETA, D.ALMACENDES FROM MATRIXDESCARGA D INNER JOIN TIENDAS T ON D.TIENDA=T.CLAVE INNER JOIN PRODUCTOSVTA PV ON D.PRODUCTOVTA=PV.CLAVE INNER JOIN ALMACENES A ON D.ALMACEN=A.CLAVE INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE AND D.GRUPO=P.GRUPO INNER JOIN UNIDADES U ON P.UNIDAD=U.CLAVE WHERE D.TIENDA='" & CTI & "' ORDER BY PV.NOMBRE")
        Else
            str = (" SELECT D.PV CODIGOVTA,PV.NOMBRE PRODUCTOVTA,D.CANTIDAD,U.NOMBRE UNIDAD,P.NOMBRE NPRODUCTO,A.NOMBRE ALMACEN FROM MATRIXDESCARGA D INNER JOIN PRODUCTOS PV ON D.PV=PV.CLAVE INNER JOIN PRODUCTOS P ON D.PD=P.CLAVE INNER JOIN UNIDADES U ON P.UNIDADINV=U.CLAVE INNER JOIN ALMACENES A ON D.ALMACEN=A.CLAVE  WHERE D.SUCURSAL='" & CTI & "' ORDER BY PV.NOMBRE")
        End If
        Me.DGV.DataSource = MODULOGENERAL.LLENATABLA(str, frmPrincipal.CadenaConexion)
        Dim num2 As Integer = (Me.DGV.Columns.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num2)
            Me.DGV.Columns.Item(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            i += 1
        Loop
        DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Me.ShowDialog()
    End Sub
End Class