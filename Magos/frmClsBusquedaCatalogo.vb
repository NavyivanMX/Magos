Public Class frmClsBusquedaCatalogo
    Dim DT As New DataTable
    Dim DA As SqlClient.SqlDataAdapter
    Public TREG As New DataTable
    Dim VQUERY, VCAMPOBUS, VQEXTRA, VNOMVEN, VLABELREG, VCADENA As String
    Dim VCOLUMNA As Integer
    Dim CONZ As New SqlClient.SqlConnection
    Dim COLIMG As Integer
    Dim PathImagen As String

    Private Sub frmClsBusquedaCatalogo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        TXTBUS.Focus()
        PONEFOCUS()
    End Sub
    Private Sub PONEFOCUS()
        TXTBUS.Focus()
    End Sub
    Public Sub BUSCAR(ByVal QUERY As String, ByVal CAMPOBUS As String, ByVal QEXTRA As String, ByVal NOMVEN As String, ByVal LABELBUS As String, ByVal LABELREG As String, ByVal COLFILL As Integer, ByVal CADENA As String, ByVal MOSTRARINFO As Boolean, ByVal COLUMNAIMAGEN As Integer, ByVal PathImg As String)
        VCADENA = CADENA
        VLABELREG = LABELREG
        DGV.Columns.Clear()
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
        PathImagen = PathImg
        COLIMG = COLUMNAIMAGEN
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
            'DGV.Columns(COLIMG).Visible = False
            LBL2.Text = DT.Rows.Count.ToString + " " + VLABELREG
            PONERIMAGENES(COLIMG)
        End If
        TXTBUS.Focus()
        LBL1.Text = LABELBUS


        Me.ShowDialog()
        'TXTBUS.Focus()
        TXTBUS.Focus()
        PONEFOCUS()
    End Sub
    Private Sub PONERIMAGENES(ByVal COL As Integer)
        DGV.Columns(COLIMG).Visible = False
        Dim COLUMNAENCONTRADA As Boolean

        COLUMNAENCONTRADA = False
        For CONCOL = 0 To DGV.Columns.Count - 1
            If DGV.Columns(CONCOL).Name = "IMAGEN" Then
                COLUMNAENCONTRADA = True
            End If
        Next
        Dim COLFINAL As Integer
        'DGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        'Dim col As DataGridViewColumn = DGV.Columns(COLFINAL)
        'col.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        If Not COLUMNAENCONTRADA Then
            Dim colImagen As DataGridViewImageColumn = New DataGridViewImageColumn()
            colImagen.Name = "IMAGEN"
            colImagen.HeaderText = "IMAGEN"
            Me.DGV.Columns.Add(colImagen)
        End If

        Dim RUTA As String

        COLFINAL = DGV.Columns.Count - 1
        DGV.Columns(COLFINAL).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        For X = 0 To DGV.Rows.Count - 1
            Try
                If COLUMNAENCONTRADA Then
                    RUTA = PathImagen + DGV.Item(COL + 1, X).Value.ToString + ".JPG"
                Else
                    RUTA = PathImagen + DGV.Item(COL, X).Value.ToString + ".JPG"
                End If
                'RUTA = PathImagen + DGV.Item(COL - 1, X).Value.ToString + ".JPG"
                'RUTA = PathImagen + DGV.Item(COL, X).Value.ToString + ".JPG"
                'RUTA = PathImagen + DGV.Item(COL + 1, X).Value.ToString + ".JPG"
                DGV.Item(COLFINAL, X).Value = New Bitmap(RUTA)
            Catch ex As Exception
                DGV.Item(COLFINAL, X).Value = New Bitmap("C:/FOTOSMITAS/PANASONIC.JPG")
            End Try
        Next
        DGV.Refresh()
    End Sub
    Private Sub ACEPTAR(ByVal B As Boolean)
        If DT.Rows.Count = 0 Then
            MessageBox.Show("Debes seleccionar un registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
            If B Then
                If DGV.Rows.Count <= 1 Then
                    For X = 0 To DGV.Columns.Count - 1
                        DOW(X) = DGV.Item(X, DGV.CurrentRow.Index).Value.ToString
                    Next
                Else
                    For X = 0 To DGV.Columns.Count - 1
                        DOW(X) = DGV.Item(X, DGV.CurrentRow.Index - 1).Value.ToString
                    Next
                End If

            Else
                For X = 0 To DGV.Columns.Count - 1
                    DOW(X) = DGV.Item(X, DGV.CurrentRow.Index).Value.ToString
                Next
            End If

            TREG.Rows.Add(DOW)
            DGV.DataSource = Nothing
            DT = New DataTable
            Try
                CONZ.Close()
            Catch ex As Exception

            End Try
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            PONEFOCUS()
        End If
    End Sub

    Private Sub DGV_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ACEPTAR(False)
    End Sub

    Private Sub BTNACEPTAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNACEPTAR.Click
        ACEPTAR(False)
    End Sub

    Private Sub TXTBUS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTBUS.KeyPress
        If e.KeyChar = Chr(13) Then
            If String.IsNullOrEmpty(TXTBUS.Text) Then
                DT.Rows.Clear()
                DT.Columns.Clear()
                DA = New SqlClient.SqlDataAdapter(VQUERY + VQEXTRA, frmPrincipal.CONX)
                DA.Fill(DT)
                DGV.DataSource = DT
                DGV.Columns(VCOLUMNA).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                'DGV.Columns(COLIMG).Visible = False
                LBL2.Text = DT.Rows.Count.ToString + " " + VLABELREG
            Else
                DT = New DataTable
                DT.Rows.Clear()
                DT.Columns.Clear()
                Dim EXTRA As String
                EXTRA = VQUERY + VCAMPOBUS + " LIKE '%" + TXTBUS.Text + "%' " + VQEXTRA
                DA = New SqlClient.SqlDataAdapter(EXTRA, CONZ)
                DA.Fill(DT)
                DGV.DataSource = DT
                DGV.Columns(VCOLUMNA).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                'DGV.Columns(COLIMG).Visible = False
                LBL2.Text = DT.Rows.Count.ToString + " " + VLABELREG
                DGV.Focus()
            End If
            PONERIMAGENES(COLIMG)
        End If
    End Sub

    Private Sub DGV_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV.CellEnter

    End Sub

    Private Sub DGV_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DGV.KeyPress
        If e.KeyChar = Chr(13) Then
            ACEPTAR(True)
        End If
    End Sub
End Class