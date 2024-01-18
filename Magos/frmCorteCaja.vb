Public Class frmCorteCaja
    Dim CAJERA As Integer
    Dim NOMBRE As String
    Dim NOCAJA As Integer
    Dim DTTIPO As New DataTable
    Private Sub frmCorteCaja_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        LLAMACAJERA()
    End Sub
    Private Function CHECACORTE() As Boolean
        If Not frmPrincipal.CHECACONX() Then
            Exit Function
        End If

        Dim SQL As New SqlClient.SqlCommand("SELECT FECHAZ FROM CORTES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CAJA=" + NOCAJA.ToString, frmPrincipal.CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        Dim DTCOR As DateTime
        If LEC.Read Then
            DTCOR = LEC(0)
            LEC.Close()
        Else
            LEC.Close()
            MessageBox.Show("No Existe el Corte, Favor de Comunicarse con Structure", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If DTCOR.Date = Now.Date Then
            MessageBox.Show("No se permite Realizar un Corte Z en el Mismo Dia, en la misma Caja, Favor de Comunicarse con DeIP", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function
    Private Sub CARGANOMBRE()
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        Dim SQL As New SqlClient.SqlCommand("SELECT NOMBRE FROM EMPLEADOSSUCURSALES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CLAVE=" + CAJERA.ToString, frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQL.ExecuteReader
        If LECTOR.Read Then
            NOMBRE = LECTOR(0)
        End If
        LECTOR.Close()
        SQL.CommandText = "UPDATE SUCURSALES SET NOMCAJERA='" + NOMBRE + "' WHERE CLAVE='" + frmPrincipal.SucursalBase + "'"
        SQL.ExecuteNonQuery()
    End Sub

    Private Sub LLAMACAJERA()
        Dim VNV As New frmEntrarCaja
        VNV.ShowDialog()
        If VNV.DialogResult = Windows.Forms.DialogResult.Yes Then
            NOMBRE = VNV.NOMBRE
            CAJERA = VNV.CAJERA
            NOCAJA = 1
            If Not CHECACORTE() Then
                Me.Close()
            End If
            CARGANOMBRE()
            LBLCAJA.Text = NOCAJA.ToString + " " + NOMBRE
        Else
            Me.Close()
        End If
        Me.BringToFront()
    End Sub
    Private Sub BTNCORTE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCORTE.Click
        Dim xyz As Short
        xyz = MessageBox.Show("¿Deseas realizar el corte de caja de este día?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Sub
        End If




        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If

        Dim LPARA As New List(Of String)
        Dim LVALO As New List(Of DateTime)
        Dim LTIPO As New List(Of SqlDbType)
        ''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''
        Dim FZ, FQ As New DateTimePicker
        Dim SQL As New SqlClient.SqlCommand("SELECT FECHAZ FROM CORTES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CAJA=" + NOCAJA.ToString, frmPrincipal.CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        If LEC.Read Then
            FZ.Value = LEC(0)
        End If
        LEC.Close()

       
        FQ.Value = FZ.Value

        LPARA.Add("@FEC")
        LTIPO.Add(SqlDbType.DateTime)
        LVALO.Add(FQ.Value)
        ''''''''''''''''''HISTORIAL CORTES''''''''''''''
        Dim TOTALENCORTE As Double
        Dim TOTALNOTAS, REGISTRO As Integer

        Dim Q1 As New SqlClient.SqlCommand("SELECT SUM(TOTAL)TOTAL,COUNT(NOTAS)NOTAS FROM TOTALCORTE WHERE ESTADO=1 AND SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOCAJA=" + NOCAJA.ToString + " AND FECHA>=@FEC", frmPrincipal.CONX)
        Q1.Parameters.Add("@FEC", SqlDbType.DateTime)
        Q1.Parameters("@FEC").Value = FQ.Value
        Dim L1 As SqlClient.SqlDataReader
        L1 = Q1.ExecuteReader
        If L1.Read Then
            Dim NE As Double
            If L1(0) Is DBNull.Value Then
                TOTALENCORTE = 0
                TOTALNOTAS = 0
                NE = 0
            Else
                TOTALENCORTE = L1(0)
                TOTALNOTAS = L1(1)
                NE = L1(0)
            End If
            L1.Close()
            Dim Q2 As New SqlClient.SqlCommand("UPDATE CORTES SET EFECTIVO=" + NE.ToString + " WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CAJA=" + NOCAJA.ToString, frmPrincipal.CONX)
            Q2.ExecuteNonQuery()
        End If
        '''''''''''''HISTORIAL CORTES'''''''''''''''
        Dim SQLREG As New SqlClient.SqlCommand("SELECT MAX(REGISTRO)REG FROM HISTORIALCORTES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CAJA=" + NOCAJA.ToString + " AND TIPOCORTE='Z'", frmPrincipal.CONX)
        Dim LECREG As SqlClient.SqlDataReader
        LECREG = SQLREG.ExecuteReader
        If LECREG.Read Then
            If LECREG(0) Is DBNull.Value Then
                REGISTRO = 0
            Else
                REGISTRO = LECREG(0)
                REGISTRO = REGISTRO + 1
            End If
        End If
        LECREG.Close()

        Dim CZ As Integer
        SQLREG.CommandText = "SELECT CZ FROM CORTES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CAJA=" + NOCAJA.ToString
        LECREG = SQLREG.ExecuteReader
        If LECREG.Read Then
            CZ = LECREG(0)
        End If
        LECREG.Close()


        Dim SQLT As New SqlClient.SqlDataAdapter("SELECT CLAVE,NOMBRE,SUM (EFECTIVO)EFECTIVO FROM VISTATIPOPAGO WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOCAJA=" + NOCAJA.ToString + " AND FECHA>=@FEC GROUP BY CLAVE,NOMBRE", frmPrincipal.CONX)
        SQLT.SelectCommand.Parameters.Add("@FEC", SqlDbType.DateTime)
        SQLT.SelectCommand.Parameters("@FEC").Value = FQ.Value

        SQLT.Fill(DTTIPO)

        Dim EFECTIVO, TARJETA, CHEQUE As Double
        For x = 0 To DTTIPO.Rows.Count - 1
            If DTTIPO.Rows(x).Item(0) = 1 Then
                EFECTIVO = DTTIPO.Rows(x).Item(2)
            ElseIf DTTIPO.Rows(x).Item(0) = 2 Then
                TARJETA = DTTIPO.Rows(x).Item(2)
            ElseIf DTTIPO.Rows(x).Item(0) = 3 Then
                CHEQUE = DTTIPO.Rows(x).Item(2)
            End If
        Next

        Dim SQLHC As New SqlClient.SqlCommand("INSERT INTO HISTORIALCORTES (SUCURSAL,CAJA,CAJERA,TIPOCORTE,NOTAS,TOTAL,FECHA,REGISTRO,EFECTIVO,TARJETAS,CHEQUES,NOCORTE) VALUES ('" + frmPrincipal.SucursalBase + "'," + NOCAJA.ToString + ",'" + NOMBRE + "','Z'," + TOTALNOTAS.ToString + "," + TOTALENCORTE.ToString + ",GETDATE()," + REGISTRO.ToString + "," + EFECTIVO.ToString + "," + TARJETA.ToString + "," + CHEQUE.ToString + "," + CZ.ToString + ")", frmPrincipal.CONX)
        SQLHC.ExecuteNonQuery()

        '''''''''''''''''''''''''''''''''''aki termina historial cortes''''''''''''''''''''''''''''

        'Dim IMP As New clsImprimir
        Dim QUER As String

        'Dim REP1 As New rptCortes
        'QUER = "SELECT   N.FECHA, N.NOTA AS NOTAS, SUM(D.TOTAL)TOTAL, N.NOCAJA, T.NOMBRE AS TIPOPAGO, S.NOMBREFISCAL AS EMPRESA, S.DIRECCION, S.CIUDAD, S.RFC, S.TELEFONO, S.NOMCAJERA, C.CZ FROM  dbo.NOTASDEVENTA AS N INNER JOIN DETALLENOTASDEVENTA D ON D.NOTA=N.NOTA AND D.TIENDA=D.TIENDA INNER JOIN PRODUCTOS P ON P.CLAVE=D.PRODUCTO AND P.EMPRESA=" + frmPrincipal.Empresa + "  INNER JOIN dbo.TIPOSPAGOS AS T  ON N.TIPOPAGO = T.CLAVE INNER JOIN dbo.TIENDAS AS S  ON N.TIENDA = S.CLAVE INNER JOIN dbo.CORTES AS C  ON C.TIENDA = N.TIENDA WHERE N.TIENDA='" + frmPrincipal.SucursalBase + "' AND N.FECHA>=@FEC GROUP BY N.FECHA, N.NOTA ,  N.NOCAJA, T.NOMBRE , S.NOMBREFISCAL, S.DIRECCION, S.CIUDAD, S.RFC, S.TELEFONO, S.NOMCAJERA, C.CZ"
        'IMP.IMPRIMIR1(REP1, QUER, 1, frmPrincipal.CadenaConexion, LPARA, LTIPO, LVALO)

        'MessageBox.Show("Fin de corte, favor de cortar el ticket", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        'MessageBox.Show("El corte se ha realizado exitosamente, a continuaci�n se har� el corte fiscal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

        Dim REP2 As New rptCorteFiscal
        'QUER = "SELECT  S.NOMBRECOMUN, N.FECHA, N.NOTA AS NOTAS, n.TOTAL, N.NOCAJA, T.NOMBRE AS TIPOPAGO, S.NOMBREFISCAL AS EMPRESA, S.DIRECCION, S.CIUDAD, S.RFC, S.TELEFONO, S.NOMCAJERA, C.CZ,(CONVERT (NUMERIC(15,2),SUM(DBO.SUBTOTAL(D.PRODUCTO,D.TOTAL)),2)-(DBO.IVACREDITO3 ('" + frmPrincipal.SucursalBase + "'," + NOCAJA.ToString + ",@fec,dateadd(dd,1,@fec),N.TOTAL)))SUBTOTAL,(CONVERT(NUMERIC(15,2), SUM(DBO.IVA(D.PRODUCTO,D.TOTAL)*.16),2)+(DBO.IVACREDITO3 ('" + frmPrincipal.SucursalBase + "'," + NOCAJA.ToString + ",@fec,dateadd(dd,1,@fec),N.TOTAL)))IVA,ELTOTAL=SUM(d.TOTAL),FECHACORTE='" + Format(Now.Date, "yyyyMMdd") + "',HORACORTE=@FEC FROM  dbo.NOTASDEVENTA AS N INNER JOIN DETALLENOTASDEVENTA D  ON D.NOTA=N.NOTA AND D.TIENDA=n.TIENDA INNER JOIN PRODUCTOS P ON P.CLAVE=D.PRODUCTO AND P.EMPRESA=" + frmPrincipal.Empresa + " INNER JOIN dbo.TIPOSPAGOS AS T  ON N.TIPOPAGO = T.CLAVE INNER JOIN dbo.TIENDAS AS S  ON N.TIENDA = S.CLAVE AND D.TIENDA=S.CLAVE INNER JOIN dbo.CORTES AS C  ON C.TIENDA = N.TIENDa   WHERE N.TIENDA='" + frmPrincipal.SucursalBase + "' AND N.FECHA>=@fec  GROUP BY N.FECHA, N.NOTA ,  N.NOCAJA, T.NOMBRE , S.NOMBREFISCAL, S.DIRECCION, S.CIUDAD, S.RFC, S.TELEFONO, S.NOMCAJERA,  C.CZ,n.TOTAL,S.NOMBRECOMUN"
        'IMP.IMPRIMIR1(REP2, QUER, 1, frmPrincipal.CadenaConexion, LPARA, LTIPO, LVALO)


        QUER = "SELECT S.NOMBRECOMUN,N.FECHA,N.NOTA NOTAS,N.TOTAL,N.CAJA NOCAJA,T.NOMBRE TIPOPAGO,S.NOMBREFISCAL EMPRESA,S.DIRECCION,S.CIUDAD,S.RFC,S.TELEFONO,S.NOMCAJERA,C.CZ,SUBTOTAL,IVA,ELTOTAL=N.TOTAL,FECHACORTE=GETDATE(),HORACORTE=GETDATE() FROM VCORTES N INNER JOIN SUCURSALES S ON N.SUCURSAL =S.CLAVE INNER JOIN TIPOSPAGOS T ON N.TIPOPAGO =T.CLAVE INNER JOIN CORTES C ON N.SUCURSAL =C.SUCURSAL  AND N.CAJA=C.CAJA WHERE N.SUCURSAL='" + frmPrincipal.SucursalBase + "' AND N.FECHA>=@INI  AND N.FORMA<>'CREDITO' ORDER BY N.NOTA"
        IMPRIMIRREPORTE(REP2, LLENATABLAIF(QUER, frmPrincipal.CadenaConexion, FQ.Value, FQ.Value), 1, "")


        Dim SQLZ As New SqlClient.SqlCommand("UPDATE CORTES SET FECHAZ=GETDATE(),CZ=CZ+1 WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CAJA=" + NOCAJA.ToString, frmPrincipal.CONX)
        SQLZ.ExecuteNonQuery()

        Try
            Dim SQLCV As New SqlClient.SqlCommand("SPCORREOVENTASCORTE", frmPrincipal.CONX)
            SQLCV.CommandType = CommandType.StoredProcedure
            SQLCV.Parameters.Add("@TI", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
            SQLCV.Parameters.Add("@FEC", SqlDbType.DateTime).Value = FQ.Value
            SQLCV.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Detalle en sp correo ventas cortes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        'Dim REP3 As New rptCorteCredito
        'QUER = "select N.NOCAJA,N.FECHA,N.NOTA,N.TOTAL,T.NOMCAJERA FROM NOTASDEVENTACREDITO N INNER JOIN TIENDAS T ON N.TIENDA=T.CLAVE WHERE N.TIENDA='" + frmPrincipal.SucursalBase + "' AND N.NOCAJA='" + NOCAJA.ToString + "' AND N.FECHA>=@INI"
        'IMPRIMIRREPORTE(REP3, LLENATABLAIF(QUER, frmPrincipal.CadenaConexion, FQ.Value, FQ.Value), 1, "")

        'Dim REP4 As New rptCorteCobranza
        'QUER = "select N.NOCAJA,N.FECHA,N.NOTA,N.TOTAL,T.NOMCAJERA,TP.NOMBRE TIPOPAGO FROM NOTASCOBRANZAPRO N INNER JOIN TIENDAS T ON N.TIENDA=T.CLAVE INNER JOIN TIPOSPAGOS TP ON N.TIPOPAGO=TP.CLAVE WHERE N.TIENDA='" + frmPrincipal.SucursalBase + "' AND N.NOCAJA='" + NOCAJA.ToString + "' AND N.FECHA>=@INI"
        'IMPRIMIRREPORTE(REP4, LLENATABLAIF(QUER, frmPrincipal.CadenaConexion, FQ.Value, FQ.Value), 1, "")


        MessageBox.Show("El corte se ha realizado exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

        'Dim REP3 As New rptVentasEfectivo
        'QUER = "SELECT TI.NOMBRECOMUN TIENDA,T.NOMBRE TIPOPAGO,N.NOTA ,C.NOMBRE CLIENTE,sum (d.total)TOTAL,N.FECHA FROM NOTASDEVENTA N INNER JOIN CLIENTES C ON C.TIENDA=N.TIENDA AND C.CLAVE=N.CLIENTE INNER JOIN DETALLENOTASDEVENTA D ON D.TIENDA=N.TIENDA AND D.NOTA=N.NOTA INNER JOIN TIPOSPAGOS T ON T.CLAVE=N.TIPOPAGO INNER JOIN TIENDAS TI ON TI.CLAVE=N.TIENDA WHERE FECHA>=(SELECT DBO.LAFECHA()) AND D.PRODUCTO<>'CREDITO' AND T.CLAVE=1 AND n.tienda='" + frmPrincipal.SucursalBase + "' group by  TI.NOMBRECOMUN ,T.NOMBRE , N.NOTA,C.NOMBRE,N.FECHA  "
        'IMP.IMPRIMIR2(REP3, QUER, 1, frmPrincipal.CadenaConexion)

        'Dim REP4 As New rptVentasTarjeta
        'QUER = "SELECT TI.NOMBRECOMUN TIENDA,T.NOMBRE TIPOPAGO, N.NOTA,C.NOMBRE CLIENTE,N.TOTAL,N.FECHA FROM NOTASDEVENTA N INNER JOIN CLIENTES C ON C.TIENDA=N.TIENDA AND C.CLAVE=N.CLIENTE INNER JOIN DETALLENOTASDEVENTA D ON D.TIENDA=N.TIENDA AND D.NOTA=N.NOTA INNER JOIN TIPOSPAGOS T ON T.CLAVE=N.TIPOPAGO INNER JOIN TIENDAS TI ON TI.CLAVE=N.TIENDA WHERE FECHA>=(SELECT DBO.LAFECHA()) AND D.PRODUCTO<>'CREDITO' AND T.CLAVE=2 AND n.tienda='" + frmPrincipal.SucursalBase + "'  "
        'IMP.IMPRIMIR2(REP4, QUER, 1, frmPrincipal.CadenaConexion)

        'Dim REP9 As New rptVentasCheque
        'QUER = "SELECT TI.NOMBRECOMUN TIENDA,T.NOMBRE TIPOPAGO, N.NOTA,C.NOMBRE CLIENTE,N.TOTAL,N.FECHA FROM NOTASDEVENTA N INNER JOIN CLIENTES C ON C.TIENDA=N.TIENDA AND C.CLAVE=N.CLIENTE INNER JOIN DETALLENOTASDEVENTA D ON D.TIENDA=N.TIENDA AND D.NOTA=N.NOTA INNER JOIN TIPOSPAGOS T ON T.CLAVE=N.TIPOPAGO INNER JOIN TIENDAS TI ON TI.CLAVE=N.TIENDA WHERE FECHA>=(SELECT DBO.LAFECHA()) AND D.PRODUCTO<>'CREDITO' AND T.CLAVE=3 AND n.tienda='" + frmPrincipal.SucursalBase + "' "
        'IMP.IMPRIMIR2(REP9, QUER, 1, frmPrincipal.CadenaConexion)

        'Dim REP5 As New rptVentasCreditos
        'QUER = "SELECT TIPOPAGO='CREDITO',N.NOTA,C.NOMBRE CLIENTE,N.TOTAL,N.FECHA FROM NOTASDEVENTACREDITO N INNER JOIN CLIENTES C ON C.TIENDA=N.TIENDA AND C.CLAVE=N.CLIENTE WHERE FECHA>=(SELECT DBO.LAFECHA()) AND N.TIENDA='" + frmPrincipal.SucursalBase + "'  "
        'IMP.IMPRIMIR2(REP5, QUER, 1, frmPrincipal.CadenaConexion)

        'Dim REP6 As New rptVentaAbonos
        'QUER = "SELECT TIPOPAGO='ABONOS',N.NOTA,C.NOMBRE CLIENTE,N.TOTAL,N.FECHA FROM NOTASDEVENTA N INNER JOIN CLIENTES C ON C.TIENDA=N.TIENDA AND C.CLAVE=N.CLIENTE INNER JOIN DETALLENOTASDEVENTA D ON D.TIENDA=N.TIENDA AND D.NOTA=N.NOTA WHERE FECHA>=(SELECT DBO.LAFECHA())   AND D.PRODUCTO='CREDITO' AND N.TIENDA='" + frmPrincipal.SucursalBase + "' "
        'IMP.IMPRIMIR2(REP6, QUER, 1, frmPrincipal.CadenaConexion)

        'Dim REP7 As New rptVentasFacturas
        'QUER = "SELECT TIPOPAGO='FACTURAS',F.FACTURA,C.NOMBRE CLIENTE,F.TOTAL FROM FACTURASCLIENTES F INNER JOIN CLIENTES C ON C.TIENDA=F.TIENDA AND C.CLAVE=F.CLIENTE WHERE FECHAGUARDO>=(SELECT DBO.LAFECHA()) AND F.TIENDA='" + frmPrincipal.SucursalBase + "' "
        'IMP.IMPRIMIR2(REP7, QUER, 1, frmPrincipal.CadenaConexion)

        'Dim REP8 As New rptGasotCajaChica
        'QUER = "SELECT TIPO='GASTO DE CAJA CHICA',DBO.REGRESACONCEPTOSUBCONCEPTO(G.CONCEPTO,G.SUBCONCEPTO)CONCEPTO,P.NOMBRE PROVEEDOR,G.SUBTOTAL,G.IVA,G.TOTAL  FROM GASTOSCAJACHICA G INNER JOIN PROVEEDORES P ON P.CLAVE=G.PROVEEDOR WHERE G.TIENDA='" + frmPrincipal.SucursalBase + "' AND G.FECHA>=(SELECT DBO.LAFECHA()) "
        'IMP.IMPRIMIR2(REP8, QUER, 1, frmPrincipal.CadenaConexion)

        Me.Close()
    End Sub
End Class