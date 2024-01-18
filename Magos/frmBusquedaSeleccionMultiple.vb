Public Class frmBusquedaSeleccionMultiple
    Dim DT As New DataTable
    Dim DTE As New DataTable
    Dim DA As SqlClient.SqlDataAdapter
    Public TREG As New DataTable
    Dim VQUERY, VCAMPOBUS, VQEXTRA, VNOMVEN, VLABELREG, VCADENA As String
    Dim VCOLUMNA As Integer
    Dim CONZ As New SqlClient.SqlConnection
    Public LEMP As New List(Of String)
    Private Sub frmBusquedaSeleccionMultiple_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        LEMP.Clear()
        CLBEMP.Items.Clear()
        PRECARGADT(DTE)
        frmPrincipal.CHECACONX()
        TXTBUS.SelectAll()
        TXTBUS.Clear()
        TXTBUS.Focus()
    End Sub
    Public Sub BUSCAR(ByVal QUERY As String, ByVal CAMPOBUS As String, ByVal QEXTRA As String, ByVal NOMVEN As String, ByVal LABELBUS As String, ByVal LABELREG As String, ByVal COLFILL As Integer, ByVal CADENA As String, ByVal MOSTRARINFO As Boolean, ByVal DTO As DataTable)
        VCADENA = CADENA
        VLABELREG = LABELREG
        DTE = DTO
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
        PRECARGADT(DTE)
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



    Private Sub ACEPTAR(ByVal B As Boolean)
        If LEMP.Count = 0 Then
            MessageBox.Show("Debes seleccionar un registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TXTBUS.SelectAll()
            TXTBUS.Clear()
            TXTBUS.Focus()
            Exit Sub
        Else
            TREG = New DataTable
            TREG.Columns.Clear()
            TREG.Rows.Clear()
            TREG.Columns.Add("A")
            TREG.Columns.Add("B")
            TREG.Columns.Add("C")
            TREG.Columns.Add("D")


            For X = 0 To LEMP.Count - 1
                Dim DOW As System.Data.DataRow = TREG.NewRow
                DOW(0) = LEMP(X)
                DOW(1) = CLBEMP.Items(X)
                DOW(2) = CLBEMP.GetItemChecked(X)
                TREG.Rows.Add(DOW)
            Next
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
            If String.IsNullOrEmpty(TXTBUS.Text) Then
                DT.Rows.Clear()
                DT.Columns.Clear()
                DA = New SqlClient.SqlDataAdapter(VQUERY + VQEXTRA, frmPrincipal.CONX)
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
    End Sub

    Private Sub DGV_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DGV.KeyPress
        If e.KeyChar = Chr(13) Then
            ACEPTAR(True)
        End If
        If e.KeyChar = Chr(32) Then
            If DGV.Rows.Count >= 1 Then
                AGREGAR(DGV.Item(0, DGV.CurrentRow.Index).Value, DGV.Item(1, DGV.CurrentRow.Index).Value)
            End If
        End If
    End Sub
    Dim LNUM As New List(Of String)
    Private Sub PRECARGADT(ByVal DT As DataTable)
        Dim X As Integer
        LEMP.Clear()
        LNUM.Clear()
        CLBEMP.Items.Clear()
        For X = 0 To DT.Rows.Count - 1
            LEMP.Add(DT.Rows(X).Item(0).ToString)
            LNUM.Add(DT.Rows(X).Item(1).ToString)
            CLBEMP.Items.Add(DT.Rows(X).Item(2).ToString, CType(DT.Rows(X).Item(2).ToString, Boolean))
        Next
    End Sub
    Private Sub AGREGAR(ByVal CLA As String, ByVal NOM As String)
        Dim X As Integer
        Dim ENC As Boolean
        ENC = False
        For X = 0 To LEMP.Count - 1
            If LEMP(X) = CLA Then
                ENC = True
            End If
        Next
        If Not ENC Then
            LEMP.Add(CLA)
            CLBEMP.Items.Add(NOM, True)
        End If
    End Sub
    Private Sub frmClsBusqueda_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 Then
            ACEPTAR(False)
        End If
        If e.KeyCode = Keys.F3 Then
            If DGV.Rows.Count >= 1 Then
                AGREGAR(DGV.Item(0, DGV.CurrentRow.Index).Value, DGV.Item(1, DGV.CurrentRow.Index).Value)
            End If

        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub DGV_DoubleClick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGV.DoubleClick
        If DGV.Rows.Count <= 0 Then
            Exit Sub
        End If
        If DGV.Rows.Count >= 1 Then
            AGREGAR(DGV.Item(0, DGV.CurrentRow.Index).Value, DGV.Item(1, DGV.CurrentRow.Index).Value)
        End If
    End Sub
End Class