Public Class frmReporteFacturasElectronicas
    Dim CADENA As String
    Private Sub frmReporteFacturasElectronicas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        CADENA = "Data Source=" + frmPrincipal.IPFE + ",1433;Network Library=DBMSSOCN;Initial Catalog=FEMAGOS;User ID=dbaadmin;Password=masterkey"
        CHECATABLA()
    End Sub
    Private Sub CARGADATOS()
        If Not CHECAFECHAS() Then
            Exit Sub
        End If
        DGV.DataSource = LLENATABLAIF("SELECT F.SERIE,F.FOLIO,F.RFCCLIENTE,F.NOMBRE,F.TOTAL,F.FECHA,F.ESTADO,F.VCFD VERSION FROM FACTURAS F INNER JOIN CLIENTES C ON F.RFCCLIENTE=C.RFC WHERE F.RFCEMISOR='" + frmPrincipal.EmisorBase + "' AND NEGOCIO='" + frmPrincipal.SucursalBase + "' AND F.FECHA>=@INI AND F.FECHA<=@FIN AND NTIPOCOMPROBANTE='I'", CADENA, DTDE.Value.Date, DTHASTA.Value.Date.AddDays(1))
        DGV.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(4).DefaultCellStyle = FORMATONUMERICO()
        CHECATABLA()
    End Sub
    Private Function CHECAFECHAS() As Boolean
        If DTDE.Value.Date > DTHASTA.Value.Date Then
            Return False
        End If
        Return True
    End Function
    Private Sub CHECATABLA()
        If DGV.Rows.Count <= 0 Then
            BTNIMPRIMIR.Enabled = False
            BTNMAIL.Enabled = False
        Else
            BTNIMPRIMIR.Enabled = True
            BTNMAIL.Enabled = True
        End If
    End Sub
    Private Sub MOSTRAR()
        If DGV.Item(6, DGV.CurrentRow.Index).Value = 3 Then
            MessageBox.Show("La factura no esta activada para Mostrarse (Cancelada)", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        Dim NI As String
        NI = ""
        Dim VSI As New frmSeleccionarImpresora
        VSI.ShowDialog()
        If VSI.DialogResult = Windows.Forms.DialogResult.Yes Then
            NI = VSI.NIMPRESORA
        End If
        Dim QUERY As String
        QUERY = "SELECT F.RFCEMISOR,E.NOMBRE NOMBREFISCAL,F.FECHA,F.SERIE,F.SUBTOTAL,F.IVA,F.DESCUENTO,F.TOTAL,F.CCLETRA CANTIDADCONLETRA,CON.NOCERTIFICADO,F.FOLIO,DBO.DOMICILIOMATRIZ(F.RFCEMISOR,F.SERIE,F.FOLIO)DOMICILIOMATRIZ,DBO.DOMICILIOEXPEDICION(F.RFCEMISOR,F.SERIE,F.FOLIO)DOMICILIOEXPEDICION,E.LOGOTIPO,E.LOGORFC,F.RFCCLIENTE,F.NOMBRE NOMBRECLIENTE,DBO.DIRECCIONCLI1(F.RFCEMISOR,F.SERIE,F.FOLIO)DOMICLIOCLI1,DBO.DIRECCIONCLI2(F.RFCEMISOR,F.SERIE,F.FOLIO)DOMICLIOCLI2,M.NOMBRE METODOPAGO,FP.NOMBRE FORMAPAGO,F.CADENAORIGINAL,F.SELLO SELLODIGITAL,F.SELLOSAT, F.UUID, P.CODIGO, P.CANTIDAD,P.UNIDAD, P.DESCRIPCION, P.VALORUNITARIO,P.IMPORTE,F.DIGITOSTC,E.REGIMEN,TXTPAGARE=DBO.TXTPAGARE(F.RFCEMISOR,F.SERIE,F.FOLIO),F.CBB CBBFACTURA,P.IEPS,F.COMENTARIO,P.IVA DIVA,IMPORTELIC=(P.IMPORTE+P.IVA+P.IEPS),E.CURP FROM FACTURAS F INNER JOIN EMISORES E ON F.RFCEMISOR=E.RFC INNER JOIN FORMASPAGO FP ON F.FORMAPAGO=FP.CLAVE INNER JOIN METODOSPAGOS M ON F.METODOPAGO=M.CLAVE INNER JOIN CONFIGURACION CON ON E.RFC=CON.RFC INNER JOIN NEGOCIOS N ON F.RFCEMISOR=N.RFC AND F.NEGOCIO=N.CLAVE INNER JOIN CLIENTES C ON F.RFCCLIENTE=C.RFC INNER JOIN DOMICILIOSCLIENTES D ON F.RFCCLIENTE=D.RFC AND F.DOMICILIOCLIENTE=D.REGISTRO INNER JOIN DETALLEFACTURAS P ON F.RFCEMISOR=P.RFCEMISOR AND F.SERIE=P.SERIE AND F.FOLIO=P.FOLIO  where F.RFCEMISOR='" + frmPrincipal.EmisorBase + "' AND F.SERIE='" + DGV.Item(0, DGV.CurrentRow.Index).Value.ToString + "' AND F.FOLIO=" + DGV.Item(1, DGV.CurrentRow.Index).Value.ToString
        If DGV.Item(7, DGV.CurrentRow.Index).Value = "3.3" Then
            QUERY = "SELECT * FROM VRFACTURA33 F where F.RFCEMISOR='" + frmPrincipal.EmisorBase + "' AND F.SERIE='" + DGV.Item(0, DGV.CurrentRow.Index).Value.ToString + "' AND F.FOLIO=" + DGV.Item(1, DGV.CurrentRow.Index).Value.ToString

            If Not frmPrincipal.CHECACONX() Then
                Exit Sub
            End If
            Dim NTC As String
            NTC = ""
            Dim CONZ As New SqlClient.SqlConnection
            CONZ.ConnectionString = CADENA
            Try
                CONZ.Open()
                Dim SQL As New SqlClient.SqlCommand("SELECT NTIPOCOMPROBANTE FROM FACTURAS WHERE RFCEMISOR='" + frmPrincipal.EmisorBase + "' AND SERIE='" + DGV.Item(0, DGV.CurrentRow.Index).Value.ToString + "' AND FOLIO=" + DGV.Item(1, DGV.CurrentRow.Index).Value.ToString, CONZ)
                Dim LEC As SqlClient.SqlDataReader
                LEC = SQL.ExecuteReader
                If LEC.Read Then
                    NTC = LEC(0)
                End If
                LEC.Close()
                SQL.Dispose()
            Catch ex As Exception

            End Try
        

            If NTC = "P" Then
                Dim REP As New rptRecepcionPagos
                MOSTRARREPORTE(REP, "Factura Electronica", LLENATABLA(QUERY, CADENA), NI)
            Else
                Dim REP As New rptFEBajamar33
                MOSTRARREPORTE(REP, "Factura Electronica", LLENATABLA(QUERY, CADENA), NI)
            End If
   

        Else
            If DGV.Item(7, DGV.CurrentRow.Index).Value = "3.2" Then
                Dim REP As New rptFacturaPM
                MOSTRARREPORTE(REP, "Factura Electronica", LLENATABLA(QUERY, CADENA), NI)
            Else
                Dim REP As New rptFacturaPM
                MOSTRARREPORTE(REP, "Factura Electronica", LLENATABLA(QUERY, CADENA), NI)
            End If
        End If

        'Dim REP As New rptFacturaElectronica3
        'MOSTRARREPORTE(REP, "Factura Electronica", LLENATABLA(QUERY, CADENA), "Enviar a OneNote 2007")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CARGADATOS()
    End Sub

    Private Sub BTNIMPRIMIR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNIMPRIMIR.Click
        MOSTRAR()
    End Sub

    Private Sub ENVIARCORREO()
        If DGV.Item(6, DGV.CurrentRow.Index).Value = 3 Then
            MessageBox.Show("La factura no esta activada para Enviarse (Cancelada)", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        Dim VCC As New frmCorreosClientesFacturas
        VCC.MOSTRARYENVIAR(DGV.Item(0, DGV.CurrentRow.Index).Value.ToString, DGV.Item(1, DGV.CurrentRow.Index).Value)
    End Sub

    Private Sub BTNMAIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNMAIL.Click
        ENVIARCORREO()
    End Sub
End Class