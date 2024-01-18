Public Class frmCancelaNotas
    Dim LMC As New List(Of String)
    Dim FOLIOFAC As String
    Private Sub frmCancelaNotas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon

        LLENACOMBOBOX2(CBMC, LMC, "SELECT CLAVE,NOMBRE FROM MOTIVOCANCELACION WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de Seleccionar", "")
    End Sub
    Private Function TIENEFACTURA() As Boolean
        If Not frmPrincipal.CHECACONX Then
            Return False
        End If
        Dim flag As Boolean = False
        Dim command As New SqlClient.SqlCommand("SELECT DBO.FOLIOFACCCF('" + frmPrincipal.SucursalBase + "','" + TXTNOTA.Text + "',0)", frmPrincipal.CONX)
        Dim reader As SqlClient.SqlDataReader = command.ExecuteReader
        If reader.Read Then
            Me.FOLIOFAC = reader.Item(0)
            flag = True
        End If
        reader.Close()
        command.Dispose()
        If FOLIOFAC = "N/A" Then
            Return False
        End If
        Return flag
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BTNELIMINAR.Click
        If DGV.Rows.Count <= 0 Then
            MessageBox.Show("No se ha cargado información de la nota", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        If CBMC.SelectedIndex = 0 Then
            MessageBox.Show("Debe seleccionar un motivo de cancelación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        If String.IsNullOrEmpty(Me.TXTOBS.Text) Then
            MessageBox.Show("Debe especificar una observación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Me.TXTOBS.Focus()
        ElseIf Me.TIENEFACTURA Then
            MessageBox.Show(("La Nota tiene una Factura Activa: " & Me.FOLIOFAC & ", se necesita cancalear la factura"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        Else
            Dim num2 As Short = CShort(MessageBox.Show("¿Desea Eliminar la nota?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            If ((num2 = 6) AndAlso frmPrincipal.CHECACONX) Then
                Dim num As Double = CType(TXTTOT.Text, Double)
                Dim command As New SqlClient.SqlCommand With {
                    .Connection = frmPrincipal.CONX,
                    .CommandType = CommandType.StoredProcedure
                }
                command.Parameters.Add("@TI", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
                command.Parameters.Add("@NOT", SqlDbType.Int).Value = Me.TXTNOTA.Text.ToString
                command.Parameters.Add("@TOT", SqlDbType.Float).Value = num.ToString
                command.Parameters.Add("@CAJA", SqlDbType.Int).Value = Me.CAJA.ToString
                command.Parameters.Add("@CAJE", SqlDbType.Int).Value = Me.CAJERA.ToString
                command.Parameters.Add("@USU", SqlDbType.VarChar).Value = frmPrincipal.Usuario
                command.Parameters.Add("@OBS", SqlDbType.VarChar).Value = Me.TXTOBS.Text.ToString
                command.Parameters.Add("@CLI", SqlDbType.Int).Value = Me.CLIENTE.ToString
                command.Parameters.Add("@MC", SqlDbType.Int).Value = LMC(CBMC.SelectedIndex)
                command.CommandText = "SPNOTASCANCELADAS"
                command.ExecuteNonQuery()
                MessageBox.Show("La nota ha sido eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Try
                    Dim command2 As New SqlClient.SqlCommand("SPELIMINASALIDAVTACANCELACION", frmPrincipal.CONX) With {
                        .CommandType = CommandType.StoredProcedure
                    }
                    command2.Parameters.Add("@TI", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
                    command2.Parameters.Add("@NOTA", SqlDbType.Int).Value = Me.TXTNOTA.Text.ToString
                    command2.Parameters.Add("@TIPO", SqlDbType.Bit).Value = 0
                    command2.ExecuteNonQuery()
                Catch exception1 As Exception

                    Dim exception As Exception = exception1
                    MessageBox.Show(("Error agregar inventario: " & exception.Message))
                End Try
                Try
                    Dim UNIVERSAL As String
                    UNIVERSAL = Encrypt(frmPrincipal.IDNU + "F" + TXTNOTA.Text).ToUpper
                    'Dim str As String = MODULOGENERAL.Encrypt((frmPrincipal.IDNU & "0" & Me.TXTNOTA.Text), "&%#@?,:*").ToUpper
                    Dim SQL As New SqlClient.SqlCommand(("DELETE FROM FEMAGOS.DBO.NOTASUNIVERSALES WHERE ID='" & UNIVERSAL & "'"), frmPrincipal.CONX)
                    SQL.ExecuteNonQuery()
                Catch exception3 As Exception
                    Dim exception2 As Exception = exception3
                End Try
                Me.DGV.DataSource = Nothing
                Me.CHECATABLA()
                Me.ACTIVAR(True)
            End If
        End If
    End Sub

    Private Sub BTNMOSTRAR_Click(sender As Object, e As EventArgs) Handles BTNMOSTRAR.Click
        Me.DGV.DataSource = Nothing
        Me.CHECATABLA()
        Me.ACTIVAR(True)
    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        Me.TXTNOTA.Enabled = V
        Me.BTNELIMINAR.Enabled = Not V
        Me.TXTOBS.Enabled = Not V
        If V Then
            Me.TXTNOTA.Focus()
        Else
            Me.TXTOBS.Focus()
        End If
    End Sub
    Dim CAJA, CAJERA, CLIENTE As String
    Private Sub CARGANOTA()
        If frmPrincipal.CHECACONX Then
            Dim reader As SqlClient.SqlDataReader = New SqlClient.SqlCommand(String.Concat(New String() {"SELECT E.NOMBRE EMPLEADO,N.FECHA,NOCAJA,N.CAJERA,C.NOMBRE,C.CLAVE FROM NOTASDEVENTA N INNER JOIN EMPLEADOSSUCURSALES E  ON N.SUCURSAL=E.SUCURSAL AND N.CAJERA=E.CLAVE  INNER JOIN ", frmPrincipal.TABLACLIENTES, " C ON C.CLAVE=N.CLIENTE WHERE N.SUCURSAL='", frmPrincipal.SucursalBase, "' AND N.NOTA=", Me.TXTNOTA.Text}), frmPrincipal.CONX).ExecuteReader
            If reader.Read Then
                Me.LBLEMP.Text = reader.Item(0).ToString
                Dim time As DateTime = reader.Item(1)
                Me.CAJA = reader.Item(2).ToString
                Me.CAJERA = reader.Item(3).ToString
                Me.LBLCLI.Text = reader.Item(4).ToString
                Me.CLIENTE = reader.Item(5).ToString
                reader.Close()
                If (DateTime.Compare(time.Date, DateAndTime.Now.Date) <> 0) Then
                    MessageBox.Show("La nota que intenta eliminar no coincide con la fecha actual, NO se permite modificar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Else
                    Me.TXTOBS.Focus()
                    Me.DGV.DataSource = LLENATABLA("SELECT D.PRODUCTO CLAVE,P.NOMBRE,D.CANTIDAD,PRECIO=DBO.DIVISIONSINCERO(D.TOTAL,D.CANTIDAD)  ,D.TOTAL FROM DETALLENOTASDEVENTA D INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE WHERE D.SUCURSAL='" + frmPrincipal.SucursalBase + "' AND  D.NOTA=" & Me.TXTNOTA.Text, frmPrincipal.CadenaConexion)
                    Me.DGV.Columns.Item(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    Me.ACTIVAR(False)
                    Me.CHECATABLA()
                End If
            Else
                MessageBox.Show("No se encontró el numero de nota", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                reader.Close()
            End If
        End If
    End Sub

    Private Sub CHECATABLA()
        If (Me.DGV.Rows.Count = 0) Then
            Me.TXTTOT.Text = "0"
        Else
            Dim left As Double = 0
            Dim num3 As Integer = (Me.DGV.Rows.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num3)
                left += DGV.Item(4, i).Value
                i += 1
            Loop
            Me.TXTTOT.Text = Strings.FormatNumber(left, -1, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault).ToString
        End If
    End Sub

    Private Sub TXTNOTA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTNOTA.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        If ((e.KeyChar = ChrW(13)) AndAlso (Me.TXTNOTA.Text <> "")) Then
            Me.CARGANOTA()
        End If
    End Sub
End Class