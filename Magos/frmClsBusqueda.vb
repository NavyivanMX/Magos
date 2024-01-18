Public Class frmClsBusqueda
    Dim DT As New DataTable
    Dim DA As SqlClient.SqlDataAdapter
    Public TREG As New DataTable
    Dim VQUERY, VCAMPOBUS, VQEXTRA, VNOMVEN, VLABELREG, VCADENA As String
    Dim VCOLUMNA As Integer
    Dim CONZ As New SqlClient.SqlConnection

    Private Sub frmBusqueda_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        frmPrincipal.CHECACONX()
        TXTBUS.SelectAll()
        TXTBUS.Clear()
        TXTBUS.Focus()
    End Sub
    Public Sub BUSCAR(ByVal QUERY As String, ByVal CAMPOBUS As String, ByVal QEXTRA As String, ByVal NOMVEN As String, ByVal LABELBUS As String, ByVal LABELREG As String, ByVal COLFILL As Integer, ByVal CADENA As String, ByVal MOSTRARINFO As Boolean)
        VCADENA = CADENA
        VLABELREG = LABELREG

        TXTBUS.Focus()
        Try
            CONZ.Close()
        Catch ex As Exception

        End Try
        CONZ.ConnectionString = VCADENA
        VQUERY = QUERY
        VCAMPOBUS = CAMPOBUS
        VQEXTRA = QEXTRA
        VCOLUMNA = COLFILL
        Try
            CONZ.Open()
        Catch ex As Exception

        End Try

        DT = New DataTable
        DT.Rows.Clear()
        DT.Columns.Clear()
        DGV.DataSource = Nothing
        LBL2.Text = DT.Rows.Count.ToString + " " + VLABELREG
        TXTBUS.Text = ""
        Me.Text = NOMVEN
        If MOSTRARINFO Then
            frmPrincipal.CHECACONX()
            DA = New SqlClient.SqlDataAdapter(VQUERY + VQEXTRA, CONZ)
            DA.Fill(DT)
            DGV.DataSource = DT
            DGV.Columns(VCOLUMNA).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            LBL2.Text = DT.Rows.Count.ToString + " " + VLABELREG
        End If
        ACOMODACOLUMNAS()
        TXTBUS.Focus()
        LBL1.Text = LABELBUS
        TXTBUS.SelectAll()
        TXTBUS.Focus()
        TXTBUS.Focus()
        TXTBUS.Focus()
        TXTBUS.Clear()
        TXTBUS.SelectAll()
        TXTBUS.Clear()
        TXTBUS.Focus()
        Me.ShowDialog()


    End Sub
    Private Sub ACOMODACOLUMNAS()
        Try
            Dim X As Integer
            For X = 0 To DGV.Columns.Count - 1                
                If X = VCOLUMNA Then
                    DGV.Columns(X).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Else
                    DGV.Columns(X).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ACEPTAR(ByVal B As Boolean)
        If DT.Rows.Count = 0 Then
            MessageBox.Show("Debes seleccionar un registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TXTBUS.SelectAll()
            TXTBUS.Clear()
            TXTBUS.Focus()
            Exit Sub
        Else
            TREG = New DataTable
            TREG.Columns.Clear()
            TREG.Rows.Clear()
            Dim X As Integer
            For X = 0 To DGV.Columns.Count - 1
                TREG.Columns.Add("" + X.ToString)
            Next
            Dim DOW As System.Data.DataRow = TREG.NewRow
            For X = 0 To DGV.Columns.Count - 1
                If B Then
                    DOW(X) = DGV.Item(X, DGV.CurrentRow.Index - 1).Value.ToString
                Else
                    DOW(X) = DGV.Item(X, DGV.CurrentRow.Index).Value.ToString
                End If
            Next
            TREG.Rows.Add(DOW)
            DGV.DataSource = Nothing
            DT = New DataTable
            Try
                CONZ.Close()
            Catch ex As Exception

            End Try
            TXTBUS.SelectAll()
            TXTBUS.Clear()
            TXTBUS.Focus()
            Me.DialogResult = Windows.Forms.DialogResult.Yes
        End If
    End Sub

    Private Sub DGV_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ACEPTAR(False)
    End Sub

    Private Sub BTNACEPTAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNACEPTAR.Click
        ACEPTAR(False)
    End Sub
    Dim LTENC As New List(Of String)
    Private Function BUSAVAN() As String
        LTENC.Clear()
        Dim EXTRA As String
        EXTRA = VQUERY

        Dim CBUSSW As String
        Dim NEB As Integer
        Dim TIENEWHERE As Boolean
        TIENEWHERE = False
        NEB = VCAMPOBUS.IndexOf("WHERE")
        If NEB <> -1 Then
            TIENEWHERE = True
            CBUSSW = VCAMPOBUS.Substring(NEB + 6, VCAMPOBUS.Length - (NEB + 6))
        End If


        Dim TBUS As String
        TBUS = TXTBUS.Text
        Do
            NEB = TBUS.IndexOf(" ")
            If NEB >= 0 Then
                LTENC.Add(TBUS.Substring(0, NEB))
                TBUS = TBUS.Substring(NEB + 1, TBUS.Length - (NEB + 1))
            Else
                LTENC.Add(TBUS)
            End If
        Loop While NEB >= 0

        Dim PRIMERO As Boolean
        PRIMERO = True
        For X = 0 To LTENC.Count - 1
            If PRIMERO Then
                EXTRA += VCAMPOBUS + " LIKE '%" + LTENC(X) + "%' "
                PRIMERO = False
            Else
                If TIENEWHERE Then
                    EXTRA += " AND " + CBUSSW + " LIKE '%" + LTENC(X) + "%' "
                Else
                    EXTRA += VCAMPOBUS + " LIKE '%" + LTENC(X) + "%' "
                End If

            End If
        Next
        EXTRA += VQEXTRA

        Return EXTRA
    End Function
    Private Sub TXTBUS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTBUS.KeyPress
        If e.KeyChar = Chr(13) Then
            frmPrincipal.CHECACONX()
            Try
                CONZ.Open()
            Catch ex As Exception

            End Try
            If String.IsNullOrEmpty(TXTBUS.Text) Then
                DT.Rows.Clear()
                DT.Columns.Clear()
                DA = New SqlClient.SqlDataAdapter(VQUERY + VQEXTRA, CONZ)
                DA.Fill(DT)
                DGV.DataSource = DT
                DGV.Columns(VCOLUMNA).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                LBL2.Text = DT.Rows.Count.ToString + " " + VLABELREG
            Else
                DT = New DataTable
                DT = LLENATABLA(BUSAVAN, VCADENA)
                DGV.DataSource = DT
                DGV.Columns(VCOLUMNA).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                LBL2.Text = DT.Rows.Count.ToString + " " + VLABELREG
            End If
        End If
        ACOMODACOLUMNAS()
    End Sub
    Protected Overloads Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (Not DGV.Focused) Then
            Return MyBase.ProcessCmdKey(msg, keyData)
        End If
        If keyData <> Keys.Enter Then
            Return MyBase.ProcessCmdKey(msg, keyData)
        End If
        ACEPTAR(False)
        Return True
    End Function

    Private Sub frmClsBusqueda_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 Then
            ACEPTAR(False)
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DGV.Rows.Count <= 0 Then
            Exit Sub
        End If
        ExportarExcel(DT, Me.Text + " " + TXTBUS.Text, True)
        'GridAExcel(DGV)
        'DataTableToExcel(DT)

    End Sub
End Class