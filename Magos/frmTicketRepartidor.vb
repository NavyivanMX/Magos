Public Class frmTicketRepartidor
    Dim DT As New DataTable
    Dim CAJA As Integer
    Dim CAJERA As Integer
    Dim INDG As Integer
    Dim LB(7) As Button
    Dim NCAJERA As String
    Private Sub frmTicketRepartidor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)

        BTNDC.Enabled = False
        BTNCC.Enabled = False
        TXTCANT.Enabled = False
        MARCARHORAS()
    End Sub
    Public Sub MOSTRAR(ByVal NOCAJA As Integer, ByVal VCAJERA As Integer, ByVal VNCAJERA As String)
        CAJA = NOCAJA
        CAJERA = VCAJERA
        NCAJERA = VNCAJERA
        INICIALIZAR()
        CARGADATOS()
        MARCARHORAS()
        Me.ShowDialog()
    End Sub

    Private Sub INICIALIZAR()
        Dim X As Integer
        For X = 1 To 6
            LB(X) = New Button
        Next
        LB(1) = LB1
        LB(2) = LB2
        LB(3) = LB3
        LB(4) = LB4
        LB(5) = LB5
        LB(6) = LB6
    End Sub
    Private Sub CARGADATOS()
        Dim QUERY, QUERY1, QUERY2 As String
        QUERY = "SELECT E.CLAVE,E.NOMBRE REPARTIDOR,DBO.ADEUDOREPARTIDOR(E.SUCURSAL,E.CLAVE," + CAJA.ToString + ")PAGOS,DBO.ADEUDOCAMBIOREPARTIDOR(E.SUCURSAL,E.CLAVE," + CAJA.ToString + ")FERIA FROM  EMPLEADOSSUCURSALES E WHERE E.SUCURSAL='" + frmPrincipal.SucursalBase + "' AND E.REPARTIDOR=1 AND E.ACTIVO=1"
        DT = LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
        QUERY1 = "SELECT CONVERT (BIT,0)SELECCIONAR,N.NOTA TICKET,C.NOMBRE CLIENTE,N.FECHA,E.NOMBRE CAJERA,N.TOTAL,Tipo='Contado',N.APLICAHORA FROM NOTASDEVENTA N INNER JOIN CLIENTES C ON N.CLIENTE=C.CLAVE INNER JOIN EMPLEADOSSUCURSALES E ON N.SUCURSAL=E.SUCURSAL AND N.CAJERA=E.CLAVE WHERE N.SUCURSAL='" + frmPrincipal.SucursalBase + "' AND N.SD=1 AND N.REP=0 AND N.PAGOSD=0 AND N.FECHA>=DBO.LAFECHA() AND N.NOCAJA=" + CAJA.ToString + " "
        DGV.DataSource = LLENATABLA(QUERY1, frmPrincipal.CadenaConexion)
        ' DGV.Columns(8).Visible = False
        DGV.Columns(7).Visible = False
        'DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        QUERY2 = "SELECT CONVERT (BIT,0)SELECCIONAR,N.NOTA TICKET,C.NOMBRE CLIENTE,N.FECHAASIGNA,E.NOMBRE CAJERA,N.TOTAL,Tipo='Contado' FROM NOTASDEVENTA N INNER JOIN CLIENTES C ON N.CLIENTE=C.CLAVE INNER JOIN EMPLEADOSSUCURSALES E ON N.SUCURSAL=E.SUCURSAL AND N.REP=E.CLAVE WHERE N.SUCURSAL='" + frmPrincipal.SucursalBase + "' AND N.SD=1 AND N.REP<>0 AND N.PAGOSD=0 AND N.FECHA>=DBO.LAFECHA() AND N.NOCAJA=" + CAJA.ToString + ""
        DGV2.DataSource = LLENATABLA(QUERY2, frmPrincipal.CadenaConexion)
        CARGAREPARTIDORES()
        CHECATABLA2()
        MARCARHORAS()
    End Sub
    Private Sub MARCARHORAS()
        Dim X As Integer
        For X = 0 To DGV.Rows.Count - 1
            If CType(DGV.Item(7, X).Value, Boolean) Then
                DGV.Item(0, X).Style = ESTILO(Color.Yellow, Color.Black)
                DGV.Item(1, X).Style = ESTILO(Color.Yellow, Color.Black)
                DGV.Item(2, X).Style = ESTILO(Color.Yellow, Color.Black)
                DGV.Item(3, X).Style = ESTILO(Color.Yellow, Color.Black)
                DGV.Item(4, X).Style = ESTILO(Color.Yellow, Color.Black)
                DGV.Item(5, X).Style = ESTILO(Color.Yellow, Color.Black)
                DGV.Item(6, X).Style = ESTILO(Color.Yellow, Color.Black)
            End If
        Next
    End Sub
    Dim LCLA As New List(Of String)
    Dim LNOM As New List(Of String)
    Dim LPAG As New List(Of Double)
    Dim LFER As New List(Of Double)
    Private Sub CARGAREPARTIDORES()
        LCLA.Clear()
        LNOM.Clear()
        LPAG.Clear()
        LFER.Clear()
        Dim X As Integer
        For X = 0 To DT.Rows.Count - 1
            LCLA.Add(DT.Rows(X).Item(0))
            LNOM.Add(DT.Rows(X).Item(1))
            LPAG.Add(DT.Rows(X).Item(2))
            LFER.Add(DT.Rows(X).Item(3))
        Next
        INDG = 1
        ACTUALIZAG()
        PANELG.Enabled = True
    End Sub
    Private Sub ACTUALIZAG()
        If INDG > 1 Then
            BTNANTG.Enabled = True
        Else
            BTNANTG.Enabled = False
        End If

        Dim MAXINDG As Integer
        MAXINDG = CType((LCLA.Count / 6), Integer)
        If ((INDG - 1) * 6 + 6) < LCLA.Count Then
            BTNSIGG.Enabled = True
        Else
            BTNSIGG.Enabled = False
        End If
        ACOMODAGRUPOS()
        'MessageBox.Show(MAXINDG.ToString, MAXINDG.ToString, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub
    Private Sub ACOMODAGRUPOS()

        Dim INICIO As Integer
        Dim FIN As Integer
        INICIO = (INDG - 1) * 6
        FIN = INICIO + 6
        If FIN > LCLA.Count Then
            FIN = LCLA.Count
        Else

        End If
        Dim FF As Integer
        FF = FIN
        While FF > 6
            FF = FF - 6            
        End While
        Dim X As Integer
        For X = 1 To 6
            LB(X).BackColor = Me.BackColor
            LB(X).Visible = True
        Next
        Dim UBI As String
        UBI = ""
        For X = 1 To FF
            'UBI = IGRU(INICIO + X - 1)
            LB(X).Text = LNOM(INICIO + X - 1) + ControlChars.NewLine + LPAG(INICIO + X - 1).ToString + ControlChars.NewLine + LFER(INICIO + X - 1).ToString
            If LPAG(INICIO + X - 1) <> 0 Or LFER(INICIO + X - 1) <> 0 Then
                LB(X).BackColor = Color.Green
            End If
            'LBP(X).Text = NPRO(INICIO + X - 1)
            'PGRU(X).ImageLocation = "C:/FOTOSBAJAMAR/" + UBI + ".JPG"
        Next
        If FF < 6 Then
            For X = FF + 1 To 6
                LB(X).Visible = False             
            Next
        End If
    End Sub

    Private Sub BTNSIGG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNSIGG.Click
        INDG = INDG + 1
        ACTUALIZAG()
    End Sub

    Private Sub BTNANTG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNANTG.Click
        INDG = INDG - 1
        ACTUALIZAG()
    End Sub
    Dim POS As Integer
    Private Sub DGV_CellMouseLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub DGV_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''DGV_CellEndEdit(sender, e)
    End Sub

    Private Sub DGV_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub
    Dim UECS As Integer
    Dim UENS As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LB1.Click, LB2.Click, LB3.Click, LB4.Click, LB5.Click, LB6.Click
        POS = (INDG - 1) * 6 + CType(sender.TAG, Integer) - 1
        BTNDC.Enabled = True
        BTNCC.Enabled = True
        TXTCANT.Enabled = True
        UECS = LCLA(POS)
        UENS = LNOM(POS)
        LBLEMP.Text = UENS
        'POS = 1
        Dim X As Integer
        Dim CONT As Integer
        CONT = 0
        For X = 0 To DGV.Rows.Count - 1
            If DGV.Item(0, X).Value = True Then
                CONT += 1
            End If
        Next
        If CONT > 0 Then
            Dim xyz As Short
            xyz = MessageBox.Show("¿Desea asignar el Repartidor: " + LNOM(POS) + " ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If xyz <> 6 Then
                Exit Sub
            Else
                Dim SQL As New SqlClient.SqlCommand("", frmPrincipal.CONX)
                For X = 0 To DGV.Rows.Count - 1
                    If DGV.Item(0, X).Value = True Then
                        If DGV.Item(6, X).Value = "Contado" Then
                            SQL.CommandText = "UPDATE NOTASDEVENTA SET REP='" + LCLA(POS) + "', FECHAASIGNA=GETDATE() WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOTA='" + DGV.Item(1, X).Value.ToString + "'"
                            SQL.ExecuteNonQuery()
                        Else
                            SQL.CommandText = "UPDATE NOTASDEVENTA SET REP='" + LCLA(POS) + "', FECHAASIGNA=GETDATE() WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOTA='" + DGV.Item(1, X).Value.ToString + "'"
                            SQL.ExecuteNonQuery()
                        End If
                        
                    End If
                Next
                SQL.Dispose()
            End If
        End If
        CARGADATOS()
        MARCARHORAS()
    End Sub

    Private Sub SIIMP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SIIMP.Click
        For X = 0 To DGV2.Rows.Count - 1
            DGV2.Item(0, X).Value = True
        Next
        CHECATABLA2()
    End Sub

    Private Sub NOIMP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NOIMP.Click
        For X = 0 To DGV2.Rows.Count - 1
            DGV2.Item(0, X).Value = False
        Next
        CHECATABLA2()
    End Sub

    Private Sub DGV2_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV2.CellEndEdit
        CHECATABLA2()
    End Sub
    Dim TOT As Double
    Private Sub CHECATABLA2()
        Dim X As Integer
        Dim CONT As Integer
        TOT = 0
        CONT = 0
        For X = 0 To DGV2.Rows.Count - 1
            If DGV2.Item(0, X).Value = True Then
                CONT += 1
                If DGV2.Item(6, X).Value = "Contado" Then
                    TOT += DGV2.Item(5, X).Value
                End If
            End If
        Next
        If CONT > 0 Then
            BTNQUITAR.Enabled = True
            BTNCOBRAR.Enabled = True
        Else
            BTNQUITAR.Enabled = False
            BTNCOBRAR.Enabled = False
        End If
        LBLTOT.Text = "Total: " + FormatNumber(TOT, 2)
    End Sub

    Private Sub BTNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNDC.Click
        GUARDACAMBIO(True)
    End Sub
    Private Sub GUARDACAMBIO(ByVal PRES As Boolean)
        Dim CANT As Double
        Try
            CANT = CType(TXTCANT.Text, Double)
        Catch ex As Exception
            MessageBox.Show("Cantidad No Válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        If CANT <= 0 Then
            MessageBox.Show("Cantidad No Válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim QUERY As String
        Dim PRE As Integer
        If PRES Then
            PRE = 1
            QUERY = String.Concat(New String() {"SELECT SUCURSAL='", frmPrincipal.NombreComun, "',TITULO='ASIGNACIÓN DE VALE DE FERIAS',CAJA=", Me.CAJA.ToString, ",ENTREGA='", NCAJERA, "',RECIBE='", Me.UENS, "',CONCEPTO='PARA FERIA DE PEDIDO',CANTIDAD=", CANT.ToString})

        Else
            PRE = 0
            QUERY = String.Concat(New String() {"SELECT SUCURSAL='", frmPrincipal.NombreComun, "',TITULO='LIQUIDACIÓN DE FERIAS',CAJA=", Me.CAJA.ToString, ",ENTREGA='", Me.UENS, "',RECIBE='", Me.NCAJERA, "',CONCEPTO='LIQUIDACIÓN DE FERIAS P/PEDIDO',CANTIDAD=", CANT.ToString})

        End If

        Dim rEP As New rptTicketPrestamoRepartidor
        MODULOGENERAL.IMPRIMIRREPORTE(rEP, MODULOGENERAL.LLENATABLA(QUERY, frmPrincipal.CadenaConexion), 1, "")
        Dim repartidor2 As New rptTicketPrestamoRepartidor
        MODULOGENERAL.IMPRIMIRREPORTE(repartidor2, MODULOGENERAL.LLENATABLA(QUERY, frmPrincipal.CadenaConexion), 1, "")

        Dim SQL As New SqlClient.SqlCommand("SPCAMBIOREPARTIDOR", frmPrincipal.CONX)
        SQL.CommandType = CommandType.StoredProcedure
        SQL.Parameters.Add("@TI", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
        SQL.Parameters.Add("@REP", SqlDbType.Int).Value = UECS
        SQL.Parameters.Add("@NOC", SqlDbType.Int).Value = CAJA
        SQL.Parameters.Add("@CAJ", SqlDbType.Int).Value = CAJERA
        SQL.Parameters.Add("@CANT", SqlDbType.Float).Value = CANT
        SQL.Parameters.Add("@PRES", SqlDbType.Bit).Value = PRE
        SQL.ExecuteNonQuery()
        CARGADATOS()
        TXTCANT.Text = ""
        MessageBox.Show("Información Guardada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub

    Private Sub BTNCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCC.Click
        GUARDACAMBIO(False)
    End Sub

    Private Sub BTNCOBRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCOBRAR.Click
        CHECATABLA2()
        Dim PAGA As Double
        Try
            PAGA = CType(TXTPAGO.Text, Double)
        Catch ex As Exception
            MessageBox.Show("Pago incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End Try
        PAGA = Math.Round(PAGA, 2)
        If PAGA < TOT Then
            MessageBox.Show("Pago incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim X As Integer
        Dim SQLG As New SqlClient.SqlCommand("INSERT INTO IMPRESIONCOBRO (SUCURSAL,CAJA,CAJERA,NOTA,ESCRE)VALUES ('" + frmPrincipal.SucursalBase + "'," + CAJA.ToString + "," + CAJERA.ToString + ",@NOTA,@ESCRE)", frmPrincipal.CONX)
        SQLG.Parameters.Add("@NOTA", SqlDbType.Int)
        SQLG.Parameters.Add("@ESCRE", SqlDbType.Bit)
        Dim SQL As New SqlClient.SqlCommand("UPDATE NOTASDEVENTA SET FECHAPAGO=GETDATE(),PAGOSD=1 WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOTA=@NOTA", frmPrincipal.CONX)
        SQL.Parameters.Add("@NOTA", SqlDbType.Int)
        For X = 0 To DGV2.Rows.Count - 1
            If DGV2.Item(0, X).Value = True Then
                If DGV2.Item(6, X).Value = "Contado" Then
                    SQL.CommandText = "UPDATE NOTASDEVENTA SET FECHAPAGO=GETDATE(),PAGOSD=1 WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOTA=@NOTA"
                    SQL.Parameters("@NOTA").Value = DGV2.Item(7, X).Value
                    SQLG.Parameters("@NOTA").Value = DGV2.Item(7, X).Value
                    SQLG.Parameters("@ESCRE").Value = 0
                    SQL.ExecuteNonQuery()
                    SQLG.ExecuteNonQuery()
                Else
                    SQL.CommandText = "UPDATE NOTASDEVENTACREDITO SET FECHAPAGO=GETDATE(),PAGOSD=1 WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOTA=@NOTA"
                    SQL.Parameters("@NOTA").Value = DGV2.Item(1, X).Value
                    SQLG.Parameters("@NOTA").Value = DGV2.Item(1, X).Value
                    SQLG.Parameters("@ESCRE").Value = 1
                    SQL.ExecuteNonQuery()
                    SQLG.ExecuteNonQuery()
                End If
            End If
        Next
        ''update repartidor ya llego
        Dim SQLU As New SqlClient.SqlCommand("UPDATE EMPLEADOSSUCURSALES SET YALLEGO=1 WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CLAVE IN (SELECT DISTINCT(REP) FROM NOTASDEVENTA WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOTA IN (SELECT NOTA FROM IMPRESIONCOBROPRO WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND IMPRESO=0) )", frmPrincipal.CONX)
        SQLU.ExecuteNonQuery()
        Dim QUERY As String
        QUERY = "SELECT T.NOMBRECOMUN SUCURSAL,D.CAJA,E.NOMBRE CAJERA,D.NOTA,N.TOTAL,TIPO='Contado',R.NOMBRE REPARTIDOR,PAGACON='" + PAGA.ToString + "',CAMBIO='" + (PAGA - TOT).ToString + "' FROM IMPRESIONCOBRO D INNER JOIN NOTASDEVENTA N ON D.SUCURSAL=N.SUCURSAL AND N.NOTA=D.NOTA INNER JOIN SUCURSALES T ON D.SUCURSAL=T.CLAVE INNER JOIN EMPLEADOSSUCURSALES E ON D.SUCURSAL=E.SUCURSAL AND D.CAJERA=E.CLAVE INNER JOIN EMPLEADOSSUCURSALES R ON N.SUCURSAL=R.SUCURSAL AND N.REP=R.CLAVE WHERE D.SUCURSAL='" + frmPrincipal.SucursalBase + "' AND D.ESCRE = 0 And IMPRESO = 0 "
        Dim REP As New rptTicketCobroRepartidor
        IMPRIMIRREPORTE(REP, LLENATABLA(QUERY, frmPrincipal.CadenaConexion), 1, "")

        Dim REP2 As New rptTicketCobroRepartidor
        IMPRIMIRREPORTE(REP2, LLENATABLA(QUERY, frmPrincipal.CadenaConexion), 1, "")
        Dim SQLI As New SqlClient.SqlCommand("UPDATE IMPRESIONCOBRO SET IMPRESO=1 WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "'", frmPrincipal.CONX)
        SQLI.ExecuteNonQuery()
        CARGADATOS()
    End Sub

    Private Sub BTNQUITAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNQUITAR.Click
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim X As Integer
        Dim SQL As New SqlClient.SqlCommand("UPDATE NOTASDEVENTA SET REP=0 WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOTA=@NOTA", frmPrincipal.CONX)
        SQL.Parameters.Add("@NOTA", SqlDbType.Int)
        For X = 0 To DGV2.Rows.Count - 1
            If DGV2.Item(0, X).Value = True Then
                If DGV2.Item(6, X).Value = "Contado" Then
                    SQL.CommandText = "UPDATE NOTASDEVENTA SET REP=0 WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOTA=@NOTA"
                    SQL.Parameters("@NOTA").Value = DGV2.Item(7, X).Value
                    SQL.ExecuteNonQuery()
                Else
                    SQL.CommandText = "UPDATE NOTASDEVENTA SET REP=0 WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOTA=@NOTA"
                    SQL.Parameters("@NOTA").Value = DGV2.Item(1, X).Value
                    SQL.ExecuteNonQuery()
                End If
            End If
        Next
        CARGADATOS()
        MARCARHORAS()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CARGADATOS()
        MARCARHORAS()
    End Sub
End Class