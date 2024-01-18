Imports System.Data.OleDb

Public Class frmAyuda

    Private Sub frmAyuda_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        'If YAEXISTE("NOMBRE") Then
        '    Exit Sub
        'End If
        'Dim MiConexion As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\Servidor\Usuarios\bd1.mdb")
        'Dim MiAdaptador As New OleDbDataAdapter("SELECT * FROM Campo", MiConexion)
        'Dim MiDataTabla As New DataTable
        'MiAdaptador.Fill(MiDataTabla)

        'Dim MiDataSet As New DataSet()
    End Sub
    Private Function YAEXISTE(ByVal NOMBRE As String) As Boolean
        'Dim DT As New DataTable
        'Dim DGV As New DataGridView
        'DGV.DataSource = DT
        'Dim X As Integer
        'For X = 0 To DGV.Rows.Count - 1
        '    If DGV.Item(1, 1).Value = NOMBRE Then
        '        MessageBox.Show("El Usuario ya esta registrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Return True
        '    End If
        'Next
        'Return False
    End Function
End Class