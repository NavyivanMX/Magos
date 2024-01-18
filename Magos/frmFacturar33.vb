Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Net.Sockets

Public Class frmFacturar33
    Dim LNEG As New List(Of String)
    Dim LREG As New List(Of String)
    Dim LDOM As New List(Of String)
    Dim LDE As New List(Of String)
    Dim LMP As New List(Of String)
    Dim LFP As New List(Of String)
    Dim LUC As New List(Of String)
    Dim CONZ As New SqlClient.SqlConnection
    Dim CADENA As String
    Dim VMOSTRAR As Boolean
    Dim DERIVADA As Boolean
    Private Sub frmFacturar33_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        CADENA = "Data Source=" + frmPrincipal.IPFE + ",1433;Network Library=DBMSSOCN;Initial Catalog=FEMAGOS;User ID=dbaadmin;Password=masterkey"
        CONZ.ConnectionString = CADENA
        CHECACONZ()

        LLENACOMBOBOX(CBNEG, LNEG, "SELECT CLAVE,NOMBRE FROM NEGOCIOS WHERE RFC='" + frmPrincipal.EmisorBase + "' AND CLAVE='" + frmPrincipal.SucursalBase + "'  ORDER BY NOMBRE", CADENA)

        ACTIVAR(True)
        CHECATABLA()

        LLENACOMBOBOX2(CBFP, LFP, "SELECT CLAVE,NOMBRE FROM CSATFORMAPAGO WHERE ACTIVO=1 ORDER BY NOMBRE", CADENA, "Favor de Seleccionar", "")
        LLENACOMBOBOX2(CBMP, LMP, "SELECT CLAVE,NOMBRE FROM CSATMETODOPAGO WHERE ACTIVO=1 ORDER BY NOMBRE", CADENA, "Favor de Seleccionar", "")
        LLENACOMBOBOX2(CBUSO, LUC, "SELECT CLAVE,NOMBRE FROM CSATUSOCOMPROBANTE WHERE ACTIVO=1 ORDER BY NOMBRE", CADENA, "Favor de Seleccionar", "")
        'PG = False
        If VMOSTRAR Then
            If TXTRFC.Text <> "1Il6G2Z0Oq95S" Then
                CARGADATOS()
                CARGARNOTA(frmPrincipal.SucursalBase, TXTNOTA.Text)
            End If
        Else
            LIMPIAR()
        End If
        'LIMPIAR()
        LBLFOL.Text = SERIE() + FOLIO(SERIE()).ToString
    End Sub
    Private Function CHECACONZ() As Boolean
        If Me.CONZ.State = ConnectionState.Closed Or Me.CONZ.State = ConnectionState.Broken Then
            Try
                Me.CONZ.Open()
            Catch ex As Exception
                MessageBox.Show("La Conexión NO esta realizada, La Informacion No se ha Procesado, Intente en un momento por Favor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End Try
        End If
        Return True
    End Function
    Public Sub MOSTRAR(ByVal RFC As String, ByVal NOTA As String, ByVal CRE As Boolean)
        LIMPIAR()
        VMOSTRAR = True
        TXTRFC.Text = RFC
        TXTNOTA.Text = NOTA
        CHKCRE.Checked = CRE
        Me.ShowDialog()
    End Sub
    Private Sub CARGADATOS()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        ACTIVAR(False)
        Dim USO As String
        USO = ""
        Dim SQL As New SqlClient.SqlCommand("SELECT NOMBRE,NOMBRECOMERCIAL,VN,USO FROM CLIENTES WHERE RFC='" + TXTRFC.Text + "'", CONZ)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        If LEC.Read Then
            TXTNOM.Text = LEC(0)
            TXTNCOM.Text = LEC(1)
            TXTNOM.Enabled = CType(LEC(2), Boolean)
            TXTNCOM.Enabled = CType(LEC(2), Boolean)
            USO = LEC(3)
        End If
        LEC.Close()
        SQL.Dispose()

        If TXTRFC.Text.Length = 13 Then
            LLENACOMBOBOX2(CBUSO, LUC, "SELECT CLAVE,NOMBRE FROM CSATUSOCOMPROBANTE WHERE ACTIVO=1 AND FISICA=1 ORDER BY NOMBRE", CADENA, "Favor de Seleccionar", "")
        Else
            LLENACOMBOBOX2(CBUSO, LUC, "SELECT CLAVE,NOMBRE FROM CSATUSOCOMPROBANTE WHERE ACTIVO=1 AND MORAL=1 ORDER BY NOMBRE", CADENA, "Favor de Seleccionar", "")
        End If

        LLENACOMBOBOX2LISTAS(CBCALLE, LREG, LDOM, "SELECT REGISTRO,dbo.DIRCLIENTE(RFC,REGISTRO),CALLE FROM DOMICILIOSCLIENTES WHERE RFC='" + TXTRFC.Text + "' AND ACTIVO=1", CADENA)
        CARGAX(LUC, CBUSO, USO)

        If CBCALLE.Items.Count <= 0 Then
            MessageBox.Show("Este cliente no cuenta con direcciones para la Factura", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            ACTIVAR(True)
            Exit Sub
        Else
            CBCALLE.Items.Insert(0, "Favor de seleccionar")
            LREG.Insert(0, "")
            LDOM.Insert(0, "")
            If CBCALLE.Items.Count = 2 Then
                CBCALLE.SelectedIndex = 1
                CBCALLE.Visible = False
            Else
                CBCALLE.SelectedIndex = 0
                CBCALLE.Visible = True
            End If

            CHECATABLA()
        End If
    End Sub
    Dim S, I, T, VIEPS, VDESC, ST As Double
    Dim CLETRAS As New num2text
    Private Sub CHECATABLA()
        S = 0
        I = 0
        T = 0
        VIEPS = 0
        VDESC = 0
        ST = 0
        If DGV.Rows.Count <= 0 Then
            BTNGUARDAR.Enabled = False
        Else
            BTNGUARDAR.Enabled = True
            Dim X As Integer
            For X = 0 To DGV.Rows.Count - 1
                S += Math.Round(DGV.Item(4, X).Value, 2)
                I += Math.Round(DGV.Item(6, X).Value, 2)
                VIEPS += Math.Round(DGV.Item(7, X).Value, 2)
                VDESC += Math.Round(DGV.Item(11, X).Value, 2)
                ST += Math.Round(DGV.Item(4, X).Value, 2)
            Next
        End If
        'I = S * 0.16
        S = Math.Round(S, 2)
        I = Math.Round(I, 2)
        T = ST + I + VIEPS - VDESC
        T = Math.Round(T, 2)
        LBLSUB.Text = "SubTotal $ " + FormatNumber(S, 2).ToString
        LBLIVA.Text = "IVA $ " + FormatNumber(I, 2).ToString
        LBLIEPS.Text = "IEPS $" + FormatNumber(VIEPS, 2).ToArray
        LBLTOT.Text = "Total $ " + FormatNumber(T, 2).ToString
        LBLDESC.Text = "Descuento $ " + FormatNumber(VDESC, 2).ToString
        LBLCCL.Text = CLETRAS.Letras(T.ToString)
    End Sub
    Private Sub CBCALLE_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCALLE.SelectedIndexChanged
        LBLDD.Text = LDOM(CBCALLE.SelectedIndex)
    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        TXTRFC.Enabled = V
        TXTNOM.Enabled = False
        TXTNCOM.Enabled = False
        CBCALLE.Enabled = Not V
        TXTNOTA.Enabled = Not V
        BTNCORREO.Enabled = Not V
        TXTCOM.Enabled = Not V
    End Sub

    Private Sub BTNBUSCAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNBUSCAR.Click
        frmClsBusqueda.BUSCAR("SELECT RFC,NOMBRE,NOMBRECOMERCIAL [NOMBRE COMERCIAL] FROM CLIENTES ", " WHERE NOMBRECOMERCIAL", " ORDER BY NOMBRE", "Búsqueda de Clientes", "Nombre Comercial del Cliente", "Cliente(s)", 1, CADENA, True)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTRFC.Text = frmClsBusqueda.TREG.Rows(0).Item(0)
            CARGADATOS()
        End If
    End Sub
    Private Function NOTAYAGUARDADA(ByVal RES As String, ByVal NOTA As String, ByVal ESCRE As Boolean) As Boolean
        If Not CHECACONZ() Then
            Return True
        End If
        Dim RESULTADO As Boolean
        RESULTADO = False
        Dim BSERIE, BFOLIO, BFECHA As String
        BSERIE = ""
        BFOLIO = ""
        BFECHA = ""
        Dim BTS As String
        If ESCRE Then
            BTS = "1"
        Else
            BTS = "0"
        End If
        Dim SQL As New SqlClient.SqlCommand("SELECT M.SERIE,M.FOLIO,F.FECHA FROM FACTURAS F INNER JOIN MULTIPLESNOTAS M ON F.RFCEMISOR=M.RFC AND F.SERIE=M.SERIE AND F.FOLIO=M.FOLIO WHERE F.RFCEMISOR='" + frmPrincipal.EmisorBase + "' AND F.NEGOCIO='" + RES + "' AND M.NOTA='" + NOTA + "' AND ESCRE='" + BTS + "' AND M.ESTADO<>3", CONZ)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        If LEC.Read Then
            BSERIE = LEC(0)
            BFOLIO = LEC(1)
            BFECHA = LEC(2)
            RESULTADO = True
        End If
        LEC.Close()
        If RESULTADO Then
            MessageBox.Show("Esta nota ya se ha utilizado para Facturar. Serie: " + BSERIE + ", Folio: " + BFOLIO + ", Fecha: " + BFECHA + ". Favor de utilizar la opción de Re Imprimir Factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Return RESULTADO
    End Function
    Private Function CARGAPAGONOTA(ByVal RES As String, ByVal NOTA As String, ByVal ESCRE As Boolean) As Boolean
        If Not frmPrincipal.CHECACONX Then
            Return True
        End If
        If ESCRE Then
            CBMP.SelectedIndex = 0
            'CARGAX(LMP, CBMP, "PPD") TODOS EN PUE
            CARGAX(LMP, CBMP, "PUE")
            CARGAX(LFP, CBFP, "01")
        Else
            CARGAX(LMP, CBMP, "PUE")
            Dim FP As Integer
            Dim TT As Integer
            Dim SQL As New SqlClient.SqlCommand("SELECT TIPOPAGO FROM NOTASDEVENTA WHERE SUCURSAL='" + RES + "' AND NOTA='" + NOTA + "'", frmPrincipal.CONX)
            Dim LEC As SqlClient.SqlDataReader
            LEC = SQL.ExecuteReader
            If LEC.Read Then
                FP = LEC(0)
                'TXTTAR.Text = LEC(1)
                'TT = LEC(2)
                'DERIVADA = LEC(3)
            End If
            LEC.Close()
            If FP = 1 Then
                'CARGAX(LMP, CBMP, 1)
                CARGAX(LFP, CBFP, "01")
            End If
            If FP = 2 Then
                'CARGAX(LMP, CBMP, 1)
                CARGAX(LFP, CBFP, "04")
            End If
            If FP = 6 Then
                'CARGAX(LMP, CBMP, 1)
                CARGAX(LFP, CBFP, "28")
            End If

            If FP = 3 Then
                'CARGAX(LMP, CBMP, 4)
                CARGAX(LFP, CBFP, "02")
            End If

            If FP = 5 Then
                'CARGAX(LMP, CBMP, 5)
                CARGAX(LFP, CBFP, "03")
            End If
            SQL.Dispose()
        End If
        LBLFOL.Text = SERIE() + FOLIO(SERIE()).ToString
    End Function
    Dim LISTANOTAS As New List(Of Integer)
    Dim LISTAESCREDITO As New List(Of Boolean)
    Private Function NOTAYAAGREGADA(ByVal NOTA As Integer, ByVal ESCRE As Boolean) As Boolean
        Dim X As Integer
        If LISTANOTAS.Count = 0 Then
            Return False
        Else
            For X = 0 To LISTANOTAS.Count - 1
                If LISTANOTAS(X) = NOTA And LISTAESCREDITO(X) = ESCRE Then
                    MessageBox.Show("Esta Nota ya ha sido Agregada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    TXTNOTA.SelectAll()
                    TXTNOTA.Focus()
                    Return True
                End If
            Next
        End If
        Return False
    End Function
    Private Sub NOTASAGREGADAS()
        'DGV.Rows.Clear()
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If

        If LISTAESCREDITO.Count > 0 Then
        Else
            DGV.DataSource = Nothing
            DGV.Refresh()
            CHECATABLA()
            Exit Sub
        End If
        Dim QUERY As String
        QUERY = "SELECT D.PRODUCTO,SUM(D.CANTIDAD)CANTIDAD,P.NOMBRE DESCRIPCION,PRECIO=DBO.VALORESFAC(D.CANTIDAD,P.FACTORIVA,P.TASAIVA,P.FACTORIEPS,P.TASAIEPS,D.DESCUENTO,D.TOTAL,1),TOTAL=SUM(DBO.VALORESFAC(D.CANTIDAD,P.FACTORIVA,P.TASAIVA,P.FACTORIEPS,P.TASAIEPS,D.DESCUENTO,D.TOTAL,5)),U.NOMBRE UNIDAD,IVA=SUM(DBO.VALORESFAC(D.CANTIDAD,P.FACTORIVA,P.TASAIVA,P.FACTORIEPS,P.TASAIEPS,D.DESCUENTO,D.TOTAL,2)),IEPS=SUM(DBO.VALORESFAC(D.CANTIDAD,P.FACTORIVA,P.TASAIVA,P.FACTORIEPS,P.TASAIEPS,D.DESCUENTO,D.TOTAL,3)),P.TASAIVA,P.TASAIEPS,GRUPOIEPS='0',DESCN=SUM(DBO.VALORESFAC(D.CANTIDAD,P.FACTORIVA,P.TASAIVA,P.FACTORIEPS,P.TASAIEPS,D.DESCUENTO,D.TOTAL,4)),P.FACTORIVA,P.FACTORIEPS,P.CPYSSAT,U.CSAT "
        If LISTAESCREDITO(0) Then
            ' QUERY = "SELECT D.PRODUCTO,SUM(D.CANTIDAD)CANTIDAD,P.NOMBRE DESCRIPCION,PRECIO=DBO.PRECIOFACPRO(D.PRODUCTO,SUM(D.CANTIDAD),SUM(D.TOTAL),D.DESCUENTO),TOTAL=SUM(DBO.TOTALFACPRO(D.PRODUCTO,D.TOTAL,D.CANTIDAD,D.DESCUENTO)),U.NOMBRE UNIDAD,IVA=SUM(DBO.IVAFACPRO(D.PRODUCTO,D.TOTAL,D.CANTIDAD,D.DESCUENTO)),SUM(P.IEPS*D.CANTIDAD) IEPS,P.TASAIEPS,P.GRUPOIEPS "
            QUERY += " FROM DETALLENOTASDEVENTACREDITO D"
        Else
            QUERY += " FROM DETALLENOTASDEVENTA D"
        End If
        'QUERY += " INNER JOIN PRODUCTOSF P ON D.PRODUCTO=P.CLAVE INNER JOIN UNIDADES U ON P.UNIDADVTA=U.CLAVE WHERE D.TOTAL>0 AND D.TIENDA='" + frmPrincipal.SucursalBase + "' AND ("
        QUERY += " INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE INNER JOIN UNIDADES U ON P.UNIDADVTA=U.CLAVE WHERE D.SUCURSAL='" + frmPrincipal.SucursalBase + "' AND ("
        Dim X As Integer
        For X = 0 To LISTANOTAS.Count - 1
            QUERY += " D.NOTA=" + LISTANOTAS(X).ToString + " OR"
            'Dim SQL As New SqlClient.SqlCommand("SELECT D.PRODUCTO,D.CANTIDAD,P.NOMBRE DESCRIPCION,PRECIO=DBO.PRECIOFAC(D.PRODUCTO,D.CANTIDAD,D.TOTAL),TOTAL=DBO.TOTALFAC(D.PRODUCTO,D.TOTAL,D.CANTIDAD),U.NOMBRE UNIDAD,IVA=DBO.IVAFAC(D.PRODUCTO,D.TOTAL,D.CANTIDAD),P.IEPS*D.CANTIDAD IEPS,P.TASAIEPS,P.GRUPOIEPS FROM DETALLENOTASDEVENTA D INNER JOIN PRODUCTOSVTA P ON D.PRODUCTO=P.CLAVE INNER JOIN UNIDADES U ON P.UVENTA=U.CLAVE WHERE D.TIENDA='" + frmPrincipal.SucursalBase + "' AND D.NOTA=" + LISTANOTAS(X).ToString + " ", frmPrincipal.CONX)
            'If LISTAESCREDITO(X) Then
            '    SQL.CommandText = "SELECT D.PRODUCTO,D.CANTIDAD,P.NOMBRE DESCRIPCION,PRECIO=DBO.PRECIOFAC(D.PRODUCTO,D.CANTIDAD,D.TOTAL),TOTAL=DBO.TOTALFAC(D.PRODUCTO,D.TOTAL,D.CANTIDAD),U.NOMBRE UNIDAD,IVA=DBO.IVAFAC(D.PRODUCTO,D.TOTAL,D.CANTIDAD),P.IEPS*D.CANTIDAD IEPS,P.TASAIEPS,P.GRUPOIEPS FROM DETALLENOTASDEVENTACREDITO D INNER JOIN PRODUCTOSVTA P ON D.PRODUCTO=P.CLAVE INNER JOIN UNIDADES U ON P.UVENTA=U.CLAVE WHERE D.TIENDA='" + frmPrincipal.SucursalBase + "' AND D.NOTA=" + LISTANOTAS(X).ToString + " "
            'End If
            'Dim LECTOR As SqlClient.SqlDataReader
            'LECTOR = SQL.ExecuteReader
            'Dim ITEMS As Integer
            'While LECTOR.Read
            '    DGV.Rows.Add(1)
            '    ITEMS = DGV.Rows.Count - 1
            '    DGV.Item(0, ITEMS).Value = LECTOR(0)
            '    DGV.Item(1, ITEMS).Value = LECTOR(1)
            '    DGV.Item(2, ITEMS).Value = LECTOR(2)
            '    DGV.Item(3, ITEMS).Value = LECTOR(3)
            '    DGV.Item(4, ITEMS).Value = LECTOR(4)
            '    DGV.Item(5, ITEMS).Value = LECTOR(5)
            '    DGV.Item(6, ITEMS).Value = LECTOR(6)
            '    DGV.Item(7, ITEMS).Value = LECTOR(7)
            '    DGV.Item(8, ITEMS).Value = LECTOR(8)
            '    DGV.Item(9, ITEMS).Value = LECTOR(9)
            'End While
            'LECTOR.Close()
        Next
        Dim IND As Integer
        IND = QUERY.LastIndexOf(" OR")
        QUERY = QUERY.Remove(IND, 3)
        QUERY += ") GROUP BY D.PRODUCTO,P.NOMBRE,U.NOMBRE,P.TASAIEPS,D.DESCUENTO,D.TOTAL,D.CANTIDAD,P.FACTORIVA,P.FACTORIEPS,P.CPYSSAT,U.CSAT,P.TASAIVA "
        DGV.DataSource = LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
        DGV.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(9).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(10).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(11).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(12).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(13).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(14).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(15).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        CHECATABLA()
    End Sub
    Private Sub QUITARNOTA(ByVal IND As Integer)
        'Dim X, POS As Integer
        'Dim ENCONTRADO As Boolean
        'ENCONTRADO = False
        'For X = 0 To LISTANOTAS.Count - 1
        '    If LISTANOTAS(X) = NOTA Then
        '        ENCONTRADO = True
        '        POS = X
        '    End If
        'Next
        'If ENCONTRADO Then
        If IND < 0 Then
            Exit Sub
        End If
        LISTANOTAS.RemoveAt(IND)
        LISTAESCREDITO.RemoveAt(IND)
        'End If
        NOTASAGREGADAS()
        ACTUALIZALB()
    End Sub
    Private Sub ACTUALIZALB()
        LB.Items.Clear()
        Dim X As Integer
        If LISTANOTAS.Count <= 0 Then
            BTNQUITAR.Enabled = False
        Else
            BTNQUITAR.Enabled = True
        End If
        For X = 0 To LISTANOTAS.Count - 1
            If LISTAESCREDITO(X) Then
                LB.Items.Add(LISTANOTAS(X).ToString + " -C")
            Else
                LB.Items.Add(LISTANOTAS(X).ToString + " -N")
            End If
        Next
    End Sub
    Private Function NOTAACTIVA(ByVal TIENDA As String, ByVal NOTA As Integer, ByVal CRE As Boolean) As Boolean
        If Not frmPrincipal.CHECACONX Then
            Return False
        End If
        Dim REG As Boolean
        REG = False
        Try
            Dim QUERY As String
            If CRE Then
                QUERY = "SELECT NOTA FROM NOTASDEVENTACREDITO WHERE SUCURSAL='" + TIENDA + "' AND NOTA=" + NOTA.ToString
            Else
                QUERY = "SELECT NOTA FROM NOTASDEVENTA WHERE SUCURSAL='" + TIENDA + "' AND NOTA=" + NOTA.ToString
            End If
            Dim SQL As New SqlClient.SqlCommand(QUERY, frmPrincipal.CONX)
            Dim LEC As SqlClient.SqlDataReader
            LEC = SQL.ExecuteReader
            If LEC.Read Then
                If LEC(0) Is DBNull.Value Then
                Else
                    REG = True
                End If
            End If
            LEC.Close()
            SQL.Dispose()
        Catch ex As Exception

        End Try  
        Return REG
    End Function
    Private Function NOTADIAANTERIOR(ByVal RES As String, ByVal NOTA As Integer)
        'Return False
        If Not frmPrincipal.CHECACONX Then
            Return False
        End If
        If frmPrincipal.FacturaLibre Then
            Return False
        Else
            Dim REG As Boolean
            REG = True
            Dim RESULTADO As String
            RESULTADO = ""
            Dim SQL As New SqlClient.SqlCommand("SELECT DBO.FECHANOTA('" + RES + "'," + NOTA.ToString + ",0)", frmPrincipal.CONX)
            If CHKCRE.Checked Then
                SQL.CommandText = "SELECT DBO.FECHANOTA ('" + RES + "'," + NOTA.ToString + ",1)"
            End If

            Dim LEC As SqlClient.SqlDataReader
            LEC = SQL.ExecuteReader
            If LEC.Read Then
                RESULTADO = LEC(0)
            End If
            LEC.Close()
            SQL.Dispose()
            If RESULTADO = "VALIDA" Then
                Return False
            Else
                MessageBox.Show("Nota del Día: " + RESULTADO + ". Solo se facturará en el mismo día", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
                REG = True
            End If
            Return REG
        End If

    End Function
    Private Sub CARGARNOTA(ByVal RES As String, ByVal NOTA As String)
        'Exit Sub
        If String.IsNullOrEmpty(NOTA) Then
            Exit Sub
        End If
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        If Not NOTAACTIVA(RES, NOTA, CHKCRE.Checked) Then
            MessageBox.Show("Nota NO existe o fue Cancelada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        If NOTADIAANTERIOR(RES, NOTA) Then
            Exit Sub
        End If
        If NOTAYAAGREGADA(NOTA, CHKCRE.Checked) Then
            Exit Sub
        End If
        If NOTAYAGUARDADA(RES, NOTA, CHKCRE.Checked) Then
            Exit Sub
        End If
        CARGAPAGONOTA(RES, NOTA, CHKCRE.Checked)
        LISTANOTAS.Add(CType(NOTA, Integer))
        LISTAESCREDITO.Add(CHKCRE.Checked)
        ACTUALIZALB()
        NOTASAGREGADAS()
        TXTNOTA.Focus()
        TXTNOTA.SelectAll()
        'DGV.DataSource = LLENATABLA("SELECT D.TELA,D.EQUIVALENCIA CANTIDAD,P.DESCRIPCION DESCRIPCION,PRECIO=(CONVERT(NUMERIC(15,2),(D.TOTAL/D.EQUIVALENCIA)/1.16,2)),TOTAL=CONVERT(NUMERIC(15,2),(D.TOTAL/1.16),2),U.NOMBRECORTO UNIDAD FROM DETALLENOTASDEVENTA D INNER JOIN PRODUCTOSVTA P ON D.TELA=P.CLAVE INNER JOIN UNIDADES U ON P.UNIDAD=U.CLAVE WHERE D.SUCURSAL='" + RES + "' AND NOTA=" + NOTA.ToString + " ", frmPrincipal.CadenaConexion)
        'CHECATABLA()
    End Sub
    Private Sub TXTNOTA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTNOTA.KeyPress
        If e.KeyChar = Chr(13) Then
            If String.IsNullOrEmpty(TXTRFC.Text) Then
            Else
                If frmPrincipal.FacturaLibre Then
                Else
                    If LISTANOTAS.Count <= 0 Then
                    Else
                        MessageBox.Show("Solo se puede Facturar un Movimiento a la vez", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End If

                If String.IsNullOrEmpty(TXTRFC.Text) Then
                Else
                    CARGARNOTA(frmPrincipal.SucursalBase, TXTNOTA.Text)
                End If
            End If
        End If
    End Sub
    Private Function SERIE() As String
        If Not CHECACONZ() Then
            Exit Function
        End If
        Dim SQL As New SqlClient.SqlCommand("SELECT SERIE FROM NEGOCIOS WHERE RFC='" + frmPrincipal.EmisorBase + "' AND CLAVE='" + LNEG(CBNEG.SelectedIndex) + "'", CONZ)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        Dim REG As String
        REG = "GRAL"
        If LEC.Read Then
            REG = LEC(0)
        End If
        LEC.Close()
        SQL.Dispose()
        Return REG
    End Function
    Private Function FOLIO(ByVal SERIE As String) As Integer
        If Not frmPrincipal.CHECACONX Then
            Exit Function
        End If
        Dim SQL As New SqlClient.SqlCommand("SELECT DBO.SIGFOLIO('" + frmPrincipal.EmisorBase + "','" + SERIE + "')", CONZ)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        Dim REG As Integer
        REG = 1
        If LEC.Read Then
            If LEC(0) Is DBNull.Value Then
                REG = 1
            Else
                REG = LEC(0)
            End If
        End If
        LEC.Close()
        'Dim FF As Integer
        'FF = 0
        'SQL.CommandText = "SELECT FOLIOFINAL FROM NEGOCIOS WHERE RFC='" + frmPrincipal.EmisorBase + "' AND CLAVE='" + frmPrincipal.SucursalBase + "'"
        'LEC = SQL.ExecuteReader
        'If LEC.Read Then
        '    Try
        '        FF = LEC(0)
        '    Catch ex As Exception

        '    End Try
        'End If
        'LEC.Close()
        'SQL.Dispose()
        'If REG > FF Then
        '    MessageBox.Show("Folios excedidos, favor de comunicarse al departamenteo de Contabilidad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Me.Close()
        'End If
        Return REG
    End Function

    Private Sub GUARDAR()

        If Not CHECACONZ() Then
            Exit Sub
        End If
        Dim ELFOLIO As Integer
        Dim VSERIE As String
        VSERIE = SERIE()
        ELFOLIO = FOLIO(VSERIE)
        Dim CONDPAG As String
        CONDPAG = "1"
        Dim X As Integer
        For X = 0 To LISTAESCREDITO.Count - 1
            If LISTAESCREDITO(X) Then
                CONDPAG = "2"
            End If
        Next
        Dim SQLUC As New SqlClient.SqlCommand("UPDATE CLIENTES SET NOMBRE='" + TXTNOM.Text + "',NOMBRECOMERCIAL='" + TXTNCOM.Text + "', USO='" + LUC(CBUSO.SelectedIndex) + "' WHERE RFC='" + TXTRFC.Text + "'", CONZ)
        SQLUC.ExecuteNonQuery()
        Dim SQL As New SqlClient.SqlCommand("INSERT INTO FACTURAS (RFCEMISOR,SERIE,FOLIO,NEGOCIO,RFCCLIENTE,DOMICILIOCLIENTE,TIPOCOMPROBANTE,METODOPAGO,FORMAPAGO,REFNOTA,SUBTOTAL,IVA,TOTAL,CCLETRA,TIPOFACTURA,NOMBRE,DIGITOSTC,COMENTARIO,CONDICIONESDEPAGO,DESCUENTO,MONEDA,TIPOCAMBIO,NMONEDA,NFORMAPAGO,NMETODOPAGO,NTIPOCOMPROBANTE,NUSOCFDI,VCFD) VALUES ('" + frmPrincipal.EmisorBase + "','" + VSERIE + "'," + ELFOLIO.ToString + ",'" + LNEG(CBNEG.SelectedIndex) + "','" + TXTRFC.Text + "','" + LREG(CBCALLE.SelectedIndex).ToString + "','1','0','" + LFP(CBFP.SelectedIndex).ToString + "','" + TXTNOTA.Text + "'," + S.ToString + "," + I.ToString + "," + T.ToString + ",'" + LBLCCL.Text + "',1,'" + TXTNOM.Text + "','" + DIGITOSTC + "','" + TXTCOM.Text + "','" + CONDPAG + "','" + VDESC.ToString + "','1',1,'MXN','" + LFP(CBFP.SelectedIndex) + "','" + LMP(CBMP.SelectedIndex) + "','I','" + LUC(CBUSO.SelectedIndex) + "','3.3')", CONZ)
        SQL.ExecuteNonQuery()
        Dim SQLD As New SqlClient.SqlCommand("INSERT INTO DETALLEFACTURAS (RFCEMISOR,SERIE,FOLIO,REFNOTA,CODIGO,CANTIDAD,DESCRIPCION,VALORUNITARIO,IMPORTE,REGISTRO,UNIDAD,IEPS,TASAIEPS,IVA,TASAIVA,TIPOFACTORIVA,TIPOFACTORIEPS,PYSSAT,UNISAT,DESCUENTO,GRUPOIEPS) VALUES ('" + frmPrincipal.EmisorBase + "','" + VSERIE + "','" + ELFOLIO.ToString + "','" + TXTNOTA.Text + "',@COD,@CANT,@DES,@PRE,@IMP,@REG,@UNI,@IEPS,@TIEPS,@IVA,@TIVA,@FIVA,@FIEPS,@PYSSAT,@UNISAT,@DESC,@GIEPS)", CONZ)
        SQLD.Parameters.Add("@COD", SqlDbType.VarChar)
        SQLD.Parameters.Add("@CANT", SqlDbType.Float)
        SQLD.Parameters.Add("@DES", SqlDbType.VarChar)
        SQLD.Parameters.Add("@UNI", SqlDbType.VarChar)
        SQLD.Parameters.Add("@PRE", SqlDbType.Float)
        SQLD.Parameters.Add("@IMP", SqlDbType.Float)
        SQLD.Parameters.Add("@REG", SqlDbType.Int)

        SQLD.Parameters.Add("@IEPS", SqlDbType.Float)
        SQLD.Parameters.Add("@TIEPS", SqlDbType.Float)
        SQLD.Parameters.Add("@IVA", SqlDbType.Float)
        SQLD.Parameters.Add("@TIVA", SqlDbType.Float)

        SQLD.Parameters.Add("@FIVA", SqlDbType.Int)
        SQLD.Parameters.Add("@FIEPS", SqlDbType.Int)

        SQLD.Parameters.Add("@PYSSAT", SqlDbType.VarChar)
        SQLD.Parameters.Add("@UNISAT", SqlDbType.VarChar)

        SQLD.Parameters.Add("@GIEPS", SqlDbType.VarChar)

        SQLD.Parameters.Add("@DESC", SqlDbType.Float)
        For X = 0 To DGV.Rows.Count - 1
            SQLD.Parameters("@COD").Value = DGV.Item(0, X).Value.ToString
            SQLD.Parameters("@CANT").Value = DGV.Item(1, X).Value
            SQLD.Parameters("@DES").Value = DGV.Item(2, X).Value.ToString
            SQLD.Parameters("@PRE").Value = DGV.Item(3, X).Value
            SQLD.Parameters("@IMP").Value = DGV.Item(4, X).Value
            SQLD.Parameters("@REG").Value = X.ToString
            SQLD.Parameters("@UNI").Value = DGV.Item(5, X).Value
            SQLD.Parameters("@IVA").Value = DGV.Item(6, X).Value
            SQLD.Parameters("@IEPS").Value = DGV.Item(7, X).Value
            SQLD.Parameters("@TIVA").Value = DGV.Item(8, X).Value
            SQLD.Parameters("@TIEPS").Value = DGV.Item(9, X).Value
            SQLD.Parameters("@GIEPS").Value = DGV.Item(10, X).Value
            SQLD.Parameters("@DESC").Value = DGV.Item(11, X).Value

            SQLD.Parameters("@FIVA").Value = DGV.Item(12, X).Value
            SQLD.Parameters("@FIEPS").Value = DGV.Item(13, X).Value

            SQLD.Parameters("@PYSSAT").Value = DGV.Item(14, X).Value
            SQLD.Parameters("@UNISAT").Value = DGV.Item(15, X).Value

            SQLD.ExecuteNonQuery()
        Next
        Dim SQLMN As New SqlClient.SqlCommand("INSERT INTO MULTIPLESNOTAS (RFC,SERIE,FOLIO,NOTA,ESCRE,TIENDA) VALUES ('" + frmPrincipal.EmisorBase + "','" + VSERIE + "'," + ELFOLIO.ToString + ",@NOTA,@ESCRE,'" + frmPrincipal.SucursalBase + "')", CONZ)
        SQLMN.Parameters.Add("@NOTA", SqlDbType.Int)
        SQLMN.Parameters.Add("@ESCRE", SqlDbType.Bit)
        'Dim SQLNU As New SqlClient.SqlCommand("UPDATE NOTASUNIVERSALES SET ESTADO=2 WHERE ID=@ID", CONZ)
        'SQLNU.Parameters.Add("@ID", SqlDbType.VarChar)
        'Dim UNIVERSAL As String
        For X = 0 To LISTANOTAS.Count - 1
            SQLMN.Parameters("@NOTA").Value = LISTANOTAS(X)
            SQLMN.Parameters("@ESCRE").Value = LISTAESCREDITO(X)
            SQLMN.ExecuteNonQuery()
            'If LISTAESCREDITO(X) Then
            '    UNIVERSAL = Encrypt(frmPrincipal.SucursalBase + "1" + LISTANOTAS(X).ToString).ToUpper
            'Else
            '    UNIVERSAL = Encrypt(frmPrincipal.SucursalBase + "0" + LISTANOTAS(X).ToString).ToUpper
            'End If
            'SQLNU.Parameters("@ID").Value = UNIVERSAL
            'SQLNU.ExecuteNonQuery()
        Next
        SQL.Dispose()
        SQLD.Dispose()
        SQLMN.Dispose()
        MessageBox.Show("La informacion ha sido Guardada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        'Dim VMF As New frmMensajeFactura
        'VMF.ShowDialog()
        'VMF.Dispose()
        'Dim VER As New frmEsperaRespuesta
        'VER.MOSTRAR(frmPrincipal.EmisorBase, VSERIE, ELFOLIO)
        'VER.Dispose()
        ACTIVAR(True)
        LIMPIAR()
    End Sub
    Dim DIGITOSTC As String
    Private Sub GUARDARPREVIO()
        'If Not CHECACONZ() Then
        '    Exit Sub
        'End If
        'Dim ELFOLIO As Integer
        'Dim VSERIE As String
        'VSERIE = SERIE()
        'ELFOLIO = FOLIO(VSERIE)
        'Dim NOTAS As String
        'NOTAS = ""
        'Dim X As Integer
        'For X = 0 To LB.Items.Count - 1
        '    NOTAS += LB.Items(X).ToString
        'Next
        'Dim SQL As New SqlClient.SqlCommand("SPTEMPVOBOCLIENTE", CONZ)
        'SQL.CommandType = CommandType.StoredProcedure
        'SQL.CommandTimeout = 300
        'SQL.Parameters.Add("@RFC", SqlDbType.VarChar).Value = frmPrincipal.EmisorBase
        'SQL.Parameters.Add("@SERIE", SqlDbType.VarChar).Value = VSERIE
        'SQL.Parameters.Add("@FOLIO", SqlDbType.Int).Value = ELFOLIO
        'SQL.Parameters.Add("@RFCCLI", SqlDbType.VarChar).Value = TXTRFC.Text
        'SQL.Parameters.Add("@NF", SqlDbType.VarChar).Value = TXTNOM.Text
        'SQL.Parameters.Add("@DOM", SqlDbType.VarChar).Value = LBLDD.Text
        'SQL.Parameters.Add("@MP", SqlDbType.VarChar).Value = CBMP.Text
        'SQL.Parameters.Add("@FP", SqlDbType.VarChar).Value = CBFP.Text
        'SQL.Parameters.Add("@DTC", SqlDbType.VarChar).Value = TXTTAR.Text
        'SQL.Parameters.Add("@NOTAS", SqlDbType.VarChar).Value = NOTAS
        'SQL.Parameters.Add("@SUB", SqlDbType.Float).Value = S
        'SQL.Parameters.Add("@IVA", SqlDbType.Float).Value = I
        'SQL.Parameters.Add("@IEPS", SqlDbType.Float).Value = VIEPS
        'SQL.Parameters.Add("@TOT", SqlDbType.Float).Value = T
        'SQL.ExecuteNonQuery()
        'Dim REP As New rptVoboCliente
        'MOSTRARREPORTE(REP, "Confirmación de Información de Cliente para Facturación", LLENATABLA("SELECT * FROM TEMPVOBOCLIENTE WHERE RFC='" + frmPrincipal.EmisorBase + "' AND SERIE='" + VSERIE + "' AND FOLIO=" + ELFOLIO.ToString, CADENA), "")
    End Sub
    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        If CBMP.SelectedIndex = 0 Then
            MessageBox.Show("Favor de Seleccionar a un Método de Pago", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If CBFP.SelectedIndex = 0 Then
            MessageBox.Show("Favor de Seleccionar una Forma de Pago", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If CBCALLE.SelectedIndex = 0 Then
            MessageBox.Show("Favor de Seleccionar a un Domicilio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If DGV.Rows.Count <= 0 Then
            Exit Sub
        End If
        'If frmPrincipal.TipoTienda = 0 Then
        '    If CBNEG.SelectedIndex = 0 Then
        '        MessageBox.Show("Favor de Seleccionar a un Negocio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Sub
        '    End If
        'Else
        'End If

        If CBUSO.SelectedIndex = 0 Then
            MessageBox.Show("Favor de Seleccionar el uso del CFDI", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        DIGITOSTC = "No Identificado"
        'If LMP(CBMP.SelectedIndex) <> "1" Then
        '    If TXTTAR.TextLength <> 4 Then
        '        MessageBox.Show("Debe escribir los últimos 4 dígitos de Tarjeta / Cheque", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Else
        '        DIGITOSTC = TXTTAR.Text
        '    End If
        'End If
        'GUARDARPREVIO()
        Dim xyz As Short
        xyz = MessageBox.Show("¿Desea Guardar la Información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Sub
        End If
        GUARDAR()
        BGW.RunWorkerAsync()
        Me.Close()
    End Sub
    Private Sub LIMPIAR()
        TXTNOTA.Text = ""
        DERIVADA = False
        'DGV.Rows.Clear()
        LISTANOTAS.Clear()
        LISTAESCREDITO.Clear()
        CHKCRE.Checked = False
        CBFP.SelectedIndex = 0
        CBMP.SelectedIndex = 0
        ACTUALIZALB()
        DGV.DataSource = Nothing
        DGV.Refresh()
        CHECATABLA()
    End Sub
    Private Sub BTNCANCELAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        ACTIVAR(True)
        LIMPIAR()
    End Sub

    Private Sub BTNCORREO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCORREO.Click
        Dim VCCRFC As New frmCorreosClientesFacturas
        VCCRFC.MOSTRAR(TXTRFC.Text)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNQUITAR.Click
        'Dim POS As Integer
        'POS = LB.SelectedIndex - 1
        QUITARNOTA(LB.SelectedIndex)
    End Sub

    Private Sub CBMP_BackColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBMP.BackColorChanged

    End Sub

    'Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim ELFOLIO As Integer
    '    Dim VSERIE As String
    '    VSERIE = SERIE()


    '    Dim Y As Integer
    '    For Y = 1 To 100
    '        S = 13.84 * Y
    '        I = (13.84 * Y) * 0.16
    '        T = S + I
    '        Dim SQL As New SqlClient.SqlCommand("INSERT INTO FACTURAS (RFCEMISOR,SERIE,FOLIO,NEGOCIO,RFCCLIENTE,DOMICILIOCLIENTE,TIPOCOMPROBANTE,METODOPAGO,FORMAPAGO,REFNOTA,SUBTOTAL,IVA,TOTAL,CCLETRA,TIPOFACTURA) VALUES ('" + frmPrincipal.EmisorBase + "','" + VSERIE + "'," + Y.ToString + ",'" + frmPrincipal.SucursalBase + "','" + TXTRFC.Text + "','" + LREG(CBCALLE.SelectedIndex).ToString + "','1','" + LMP(CBMP.SelectedIndex).ToString + "','" + LFP(CBFP.SelectedIndex).ToString + "','" + TXTNOTA.Text + "'," + S.ToString + "," + I.ToString + "," + T.ToString + ",'" + LBLCCL.Text + "',2)", CONZ)
    '        SQL.ExecuteNonQuery()
    '        Dim SQLD As New SqlClient.SqlCommand("INSERT INTO DETALLEFACTURAS (RFCEMISOR,SERIE,FOLIO,REFNOTA,CODIGO,CANTIDAD,DESCRIPCION,VALORUNITARIO,IMPORTE,REGISTRO,UNIDAD) VALUES ('" + frmPrincipal.EmisorBase + "','" + VSERIE + "','" + Y.ToString + "','" + TXTNOTA.Text + "',@COD,@CANT,@DES,@PRE,@IMP,@REG,@UNI)", CONZ)
    '        SQLD.Parameters.Add("@COD", SqlDbType.VarChar)
    '        SQLD.Parameters.Add("@CANT", SqlDbType.Float)
    '        SQLD.Parameters.Add("@DES", SqlDbType.VarChar)
    '        SQLD.Parameters.Add("@UNI", SqlDbType.VarChar)
    '        SQLD.Parameters.Add("@PRE", SqlDbType.Float)
    '        SQLD.Parameters.Add("@IMP", SqlDbType.Float)
    '        SQLD.Parameters.Add("@REG", SqlDbType.Int)

    '        SQLD.Parameters("@COD").Value = "COD" + Y.ToString
    '        SQLD.Parameters("@CANT").Value = Y
    '        SQLD.Parameters("@DES").Value = "VENTA DE PRODUCTO X" + Y.ToString
    '        SQLD.Parameters("@PRE").Value = 13.84
    '        SQLD.Parameters("@IMP").Value = 13.84 * Y
    '        SQLD.Parameters("@REG").Value = 0
    '        SQLD.Parameters("@UNI").Value = "PIEZA"
    '        SQLD.ExecuteNonQuery()
    '    Next

    'End Sub

    Private Sub CBMP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBMP.SelectedIndexChanged
        Try
            If LMP(CBMP.SelectedIndex) <> "1" Then
                TXTTAR.Enabled = True
            Else
                TXTTAR.Text = ""
                TXTTAR.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TXTRFC_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRFC.KeyPress
        If e.KeyChar = Chr(13) Then
            If String.IsNullOrEmpty(TXTRFC.Text) Then
            Else
                If TXTRFC.Text.Length < 12 Then
                    MessageBox.Show("RFC Incompleto, faltan Caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    Exit Sub
                End If
                If TXTRFC.Text.Length = 13 Then
                    If RegularExpressions.Regex.IsMatch(Me.TXTRFC.Text, "^([A-Z,&,ñ,Ñ\s]{4})\d{6}([A-Z\w]{3})$") Then
                        CARGADATOS()
                    Else
                        MessageBox.Show("Teclee un RFC valido", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If
                If TXTRFC.Text.Length = 12 Then
                    If RegularExpressions.Regex.IsMatch(Me.TXTRFC.Text, "^([A-Z,&,ñ,Ñ\s]{3})\d{6}([A-Z\w]{3})$") Then
                        CARGADATOS()
                    Else
                        MessageBox.Show("Teclee un RFC valido", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub BTNAGREGAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim VNCLIENTES As New frmClientesFacturasElectronicas
        VNCLIENTES.ShowDialog()
    End Sub

    Private Sub CBNEG_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBNEG.SelectedIndexChanged
        LBLFOL.Text = SERIE() + FOLIO(SERIE()).ToString
    End Sub

    Private Sub BGW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGW.DoWork
        Try
            Dim clientSocket As New System.Net.Sockets.TcpClient()
            clientSocket.Connect("bahamut.grupobajamar.com", 1107)
            'clientSocket.Connect("192.168.2.84", 8888)
            Dim serverStream As NetworkStream
            'Label1.Text = "Client Socket Program - Server Connected ..."
            serverStream = clientSocket.GetStream()

            Dim outStream As Byte() = Encoding.ASCII.GetBytes("Timbralaaaaa  ")
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
            clientSocket.Close()
        Catch ex As Exception
            '  MessageBox.Show("Avisar del Robot Timbrador a Sistemas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End Try
    End Sub

    Private Sub BGW_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGW.RunWorkerCompleted

    End Sub
End Class