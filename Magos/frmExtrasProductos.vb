Public Class frmExtrasProductos
    Dim DTPRIN As New DataTable
    Dim DTA As New DataTable
    Private Sub frmExtrasProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        DTPRIN.Columns.Add("Clave")
        DTPRIN.Columns.Add("Nombre")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,ACTIVO FROM PRODUCTOS ", " WHERE NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Productos de Venta", "Nombre del Producto", "Producto(s)", 1, frmPrincipal.CadenaConexion, False)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTPV.Text = frmClsBusqueda.TREG.Rows(0).Item(0)
            CARGAPRODUCTOVTA()
        End If
    End Sub
    Private Function CARGAPRODUCTOVTA() As Boolean
        If Not frmPrincipal.CHECACONX Then
            Exit Function
        End If
        Dim ENC As Boolean
        ENC = False
        Dim SQL As New SqlClient.SqlCommand("SELECT NOMBRE,CLAVE FROM PRODUCTOS WHERE CLAVE=@CLA", frmPrincipal.CONX)
        SQL.Parameters.Add("@CLA", SqlDbType.VarChar).Value = TXTPV.Text
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        If LEC.Read Then
            LBLPV.Text = LEC(0)
            ENC = True
        End If
        LEC.Close()
        SQL.Dispose()
        If ENC Then

        End If
        Return ENC
    End Function

    Private Sub CARGAEXTRAS()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim QUERY As String
        QUERY = "SELECT P.CLAVE,P.NOMBRE FROM PRODUCTOSEXTRAS D INNER JOIN PRODUCTOS P ON D.CLAVE=P.CLAVE WHERE D.PRODUCTO='" + TXTPV.Text + "' ORDER BY P.NOMBRE"
        Dim DTTEMP As DataTable
        DTTEMP = LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
        Dim X As Integer
        For X = 0 To DTTEMP.Rows.Count - 1
            Dim DOW As DataRow = DTTEMP.NewRow()
            DOW(0) = DTTEMP.Rows(X).Item(0).ToString
            DOW(1) = DTTEMP.Rows(X).Item(1).ToString
            DTPRIN.Rows.Add(DOW)
        Next
        ACTUALIZAGRID()
    End Sub
    Private Sub ACTUALIZAGRID()
        DGV.DataSource = DTPRIN
        DGV.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim DTP As New DataTable
        frmBusquedaSeleccionMultiple.BUSCAR("SELECT CLAVE,P.NOMBRE FROM PRODUCTOS P ", " WHERE P.NOMBRE", " ORDER BY P.NOMBRE", "Selección de Productos", "Nombre del Producto", "Producto(s)", 1, frmPrincipal.CadenaConexion, False, DTP)

        If frmBusquedaSeleccionMultiple.DialogResult = Windows.Forms.DialogResult.Yes Then
            DTA = New DataTable
            DTA = frmBusquedaSeleccionMultiple.TREG
            DGV.DataSource = DTA
            ACTUALIZASELECCION()
        End If
    End Sub
    Private Sub ACTUALIZASELECCION()

        Dim X, Y As Integer
        Dim ENC As Boolean
        For X = 0 To DTA.Rows.Count - 1
            ENC = False

            For Y = 0 To DTPRIN.Rows.Count - 1
                If DTA.Rows(X).Item(0).ToString = DTPRIN.Rows(Y).Item(0).ToString Then
                    ENC = True
                End If
            Next
            If Not ENC Then
                Dim DOW As System.Data.DataRow = DTPRIN.NewRow
                DOW(0) = DTA.Rows(X).Item(0).ToString
                DOW(1) = DTA.Rows(X).Item(1).ToString
                DTPRIN.Rows.Add(DOW)
            End If
        Next
        ACTUALIZAGRID()
    End Sub

    Private Sub BTNGUARDAR_Click(sender As Object, e As EventArgs) Handles BTNGUARDAR.Click
        Dim CP As Integer
        Try

            CP = CType(TXTPV.Text, Integer)
        Catch ex As Exception
            MessageBox.Show("Clave del producto no válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End Try
        If CP <= 0 Then
            MessageBox.Show("Clave del producto no válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        Dim xyz As Short
            xyz = MessageBox.Show("¿Desea guardar la información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If xyz <> 6 Then
                Exit Sub
            End If
            GUARDAR()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub GUARDAR()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim X As Integer
        Dim SQL As New SqlClient.SqlCommand("SPPRODUCTOSEXTRAS", frmPrincipal.CONX)
        SQL.CommandType = CommandType.StoredProcedure
        SQL.CommandTimeout = 300
        SQL.Parameters.Add("@PRO", SqlDbType.Int).Value = TXTPV.Text
        SQL.Parameters.Add("@ACT", SqlDbType.Bit).Value = 1
        SQL.Parameters.Add("@CLA", SqlDbType.Int)
        For X = 0 To DTPRIN.Rows.Count - 1
            SQL.Parameters("@CLA").Value = DTPRIN.Rows(X).Item(0).ToString
            SQL.ExecuteNonQuery()
        Next
        MessageBox.Show("La información ha sido guardada correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub
    Private Sub BTNELIMINAR_Click(sender As Object, e As EventArgs) Handles BTNELIMINAR.Click
        Dim xyz As Short
        xyz = MessageBox.Show("¿Esta seguro que desea eliminar?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If xyz = 6 Then
            Try
                If Not frmPrincipal.CHECACONX() Then
                    Exit Sub
                End If
                Dim SQLELIMINAR As New SqlClient.SqlCommand
                SQLELIMINAR.Connection = frmPrincipal.CONX
                SQLELIMINAR.CommandText = "DELETE FROM PRODUCTOSEXTRAS WHERE PRODUCTO=" + TXTPV.Text
                SQLELIMINAR.ExecuteNonQuery()
                DTPRIN.Rows.Clear()
                MessageBox.Show("La información ha sido eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
End Class