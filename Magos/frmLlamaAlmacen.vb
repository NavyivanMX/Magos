Public Class frmLlamaAlmacen
    Dim LALM As New List(Of String)
    Public ALM As String
    Dim LEMP As New List(Of String)
    Public NALM As String
    Public EMP As String
    Dim OPC As Integer
    Dim DT As New DataTable
    Private Sub frmLlamaAlmacen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        If OPC = 1 Then
            DT = LLENATABLA("SELECT A.CLAVE, A.EMPRESA, A.NOMBRE FROM USUARIOSALMACENES D INNER JOIN ALMACENES A ON D.ALMACEN=A.CLAVE INNER JOIN EMPRESASGASTOS E ON A.EMPRESA=E.CLAVE WHERE D.USUARIO='" + frmPrincipal.Usuario + "' AND A.ACTIVO=1 AND E.GRUPO='" + frmPrincipal.Empresa + "' AND A.CLAVE IN (SELECT ALMACENDESTINO FROM DEVOLUCIONESALMACENES WHERE ESTADO=0) ORDER BY A.NOMBRE", frmPrincipal.CadenaConexion)
            If Not LLENACOMBOBOX2LISTAS(CBALM, LALM, LEMP, "SELECT A.CLAVE, A.EMPRESA, A.NOMBRE FROM USUARIOSALMACENES D INNER JOIN ALMACENES A ON D.ALMACEN=A.CLAVE INNER JOIN EMPRESASGASTOS E ON A.EMPRESA=E.CLAVE WHERE D.USUARIO='" + frmPrincipal.Usuario + "' AND A.ACTIVO=1 AND E.GRUPO='" + frmPrincipal.Empresa + "' AND A.CLAVE IN (SELECT ALMACENDESTINO FROM DEVOLUCIONESALMACENES WHERE ESTADO=0) ORDER BY A.NOMBRE", frmPrincipal.CadenaConexion) Then
                'MessageBox.Show("No se encuentran devoluciones para resolver de sus almacenes", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = Windows.Forms.DialogResult.No
                Me.Close()
            Else

            End If
        Else
            DT = LLENATABLA("SELECT A.CLAVE, A.EMPRESA, A.NOMBRE  FROM USUARIOSALMACENES D INNER JOIN ALMACENES A ON D.ALMACEN=A.CLAVE INNER JOIN EMPRESASGASTOS E ON A.EMPRESA=E.CLAVE WHERE D.USUARIO='" + frmPrincipal.Usuario + "' AND A.ACTIVO=1 AND E.GRUPO='" + frmPrincipal.Empresa + "' ORDER BY A.NOMBRE", frmPrincipal.CadenaConexion)
            If Not LLENACOMBOBOX2LISTAS(CBALM, LALM, LEMP, "SELECT A.CLAVE, A.EMPRESA, A.NOMBRE, A.NOMBRE  FROM USUARIOSALMACENES D INNER JOIN ALMACENES A ON D.ALMACEN=A.CLAVE INNER JOIN EMPRESASGASTOS E ON A.EMPRESA=E.CLAVE WHERE D.USUARIO='" + frmPrincipal.Usuario + "' AND A.ACTIVO=1 AND E.GRUPO='" + frmPrincipal.Empresa + "' ORDER BY A.NOMBRE", frmPrincipal.CadenaConexion) Then
                MessageBox.Show("Usted no cuenta con Almacenes asignados para realizar este movimiento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = Windows.Forms.DialogResult.No
                Me.Close()
            Else

            End If
        End If

    End Sub
    Public Sub MOSTRAR(ByVal VOPC As Integer)
        OPC = VOPC
        Me.ShowDialog()
    End Sub
    Private Sub BTNACEPTAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNACEPTAR.Click
        ALM = LALM(CBALM.SelectedIndex)
        EMP = LEMP(CBALM.SelectedIndex)
        NALM = CBALM.Text
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub
End Class