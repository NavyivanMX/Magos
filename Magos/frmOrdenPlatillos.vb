Public Class frmOrdenPlatillos
    Dim LCLA As New List(Of String)
    Dim CLAGRU As New List(Of String)
    Private Sub frmOrdenPlatillos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)

        LLENACOMBOBOX(CBGRU, CLAGRU, "SELECT CLAVE,NOMBRE FROM GRUPOTOUCH WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
        CARGAR()
    End Sub
    Private Sub CARGAR()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        LB.Items.Clear()
        LCLA.Clear()
        Dim QUERY As String
        QUERY = "SELECT CLAVE,NOMBRE FROM PRODUCTOS WHERE GRUPOTOUCHVENTA='" + CLAGRU(CBGRU.SelectedIndex) + "'  AND ACTIVO=1 ORDER BY ORDEN"
        Dim SQL As New SqlClient.SqlCommand(QUERY, frmPrincipal.CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        While LEC.Read
            LB.Items.Add(LEC(1))
            LCLA.Add(LEC(0))
        End While
        LEC.Close()
    End Sub
    Private Sub SUBIR()
        Dim POS As Integer
        POS = LB.SelectedIndex
        If POS = 0 Then
            Exit Sub
        End If
        Dim CLA, NOM As String
        CLA = LCLA(POS)
        NOM = LB.Items.Item(POS)
        LB.Items.RemoveAt(POS)
        LCLA.RemoveAt(POS)
        LB.Items.Insert(POS - 1, NOM)
        LCLA.Insert(POS - 1, CLA)
        LB.SelectedIndex = POS - 1
    End Sub
    Private Sub BAJAR()
        Dim POS As Integer
        POS = LB.SelectedIndex
        If POS = LB.Items.Count - 1 Then
            Exit Sub
        End If
        Dim CLA, NOM As String
        CLA = LCLA(POS)
        NOM = LB.Items.Item(POS)
        LB.Items.RemoveAt(POS)
        LCLA.RemoveAt(POS)
        LB.Items.Insert(POS + 1, NOM)
        LCLA.Insert(POS + 1, CLA)
        LB.SelectedIndex = POS + 1
    End Sub

    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        Dim xyz As Short
        xyz = MessageBox.Show("¿Desea Guardar la Información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Sub
        End If
        Dim SQL As New SqlClient.SqlCommand("", frmPrincipal.CONX)
        SQL.CommandTimeout = 300
        Dim X As Integer
        For X = 0 To LCLA.Count - 1
            SQL.CommandText = "UPDATE PRODUCTOS SET ORDEN='" + X.ToString + "' WHERE CLAVE='" + LCLA(X) + "'"
            SQL.ExecuteNonQuery()
        Next
        MessageBox.Show("La Información Ha Sido Guardada Exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SUBIR()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        BAJAR()
    End Sub

    Private Sub CBGRU_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBGRU.SelectedIndexChanged
        CARGAR()
    End Sub
End Class