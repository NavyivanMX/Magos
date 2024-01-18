Imports System.Diagnostics
Imports System.Drawing.Printing
Imports System.Globalization
Public Class frmNotaDeVenta
    Dim COLORGRUPO, COLORPLATILLOS As Integer
    Dim LTP As New List(Of String)
    Dim LGRU As New List(Of String)
    Dim LPRO As New List(Of String)
    Dim NGRU As New List(Of String)
    Dim NPRO As New List(Of String)
    Dim IPRO As New List(Of String)
    Dim PPRE As New List(Of Double)
    Dim PGRU(12) As PictureBox
    Dim PPRO(12) As PictureBox
    Dim LBG(12) As Label
    Dim LBP(12) As Label
    Dim DT As New DataTable
    Dim DV As New DataView
    Dim NCUAG, NCUAP, INDG, INDP As Integer
    Dim TOCADO, TOCADO2, CANTFOCUS As Boolean
    Dim CNUM As New num2text
    Dim CBBANCO As New ComboBox
    Dim LBAN As New List(Of String)
    Dim CBTP As New ComboBox
    Dim CLIENTEBASE As String
    Dim NOMBRECLIENTEBASE As String
    Dim SW As New Stopwatch()
    Dim TT As Double
    Dim DOM As String
    Dim COM As String
    Dim AH As Double
    Dim HE As DateTime
    Public CAJA As Integer
    Public NE As New Integer
    Dim NOMEMP As String
    Dim FZ As DateTime
    Dim DC As Integer
    Dim LNC As Integer
    Public LISTAPRENOTAS As New List(Of Integer)
    Private Sub frmNotaDeVenta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        'COLORGRUPO = -612518
        'COLORPLATILLOS = -6473327
        COLORGRUPO = -6473327
        COLORPLATILLOS = -612518
        ''lila:-6473327
        ''naranja: -612518
        NCUAG = 10
        NCUAP = 8
        INDG = 1
        INDP = 1
        PANELG.BackColor = Color.FromArgb(COLORGRUPO)
        PANELP.BackColor = Color.FromArgb(COLORPLATILLOS)
        LLENACOMBOBOX(CBTP, LTP, "SELECT CLAVE,NOMBRE FROM TIPOSPAGOS WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
        LLENACOMBOBOX(CBBANCO, LBAN, "SELECT CLAVE,NOMBRE FROM BANCOSVENTA WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
        'CARGANOTA()
        INICIALIZAR()
        CARGAGRUPOS()
        TOCADO = False
        TOCADO2 = False
        CANTFOCUS = True
        CHECAFOCUS()
        CHECACANTIDADES()
        Label10.Text = CNUM.Letras(TXTTOT.Text)
        If Not LLAMAEMPLEADO() Then
            Me.Close()
        Else
            LLAMACLIENTE()
        End If
        'Dim SIZE As New Size
        'SIZE.Height = 918
        'SIZE.Width = 1359
        'Me.Size = SIZE
        ' Me.StartPosition = FormStartPosition.Manual

    End Sub
    Private Sub ACOMODAR()

    End Sub
    Private Sub LLAMACLIENTE()
        CARGANOTA()
        Me.Opacity = 25
        Dim VNV As New frmSeleccionaCliente
        VNV.ShowDialog()
        If VNV.DialogResult = Windows.Forms.DialogResult.Yes Then

            ' frmPrincipal.CONT = 90
            CLIENTEBASE = VNV.CLIENTEBASE
            DC = VNV.DIASCREDITO
            NOMBRECLIENTEBASE = VNV.NOMBRECLIENTEBASE
            TXTNOTA.Text = CARGANOTA.ToString
            LBLNC.Text = NOMBRECLIENTEBASE

            CHECATABLA()
            DGV.Refresh()

            DGV.DataSource = Nothing
            DGV.Refresh()
            DGV.DataSource = frmPrincipal.PRE.ElementosAgregados
            ACOMODARDGV()
            CHECATABLA()
        Else
            Me.Close()
        End If
    End Sub
    Private Function CARGANOTA() As Integer
        'Try
        If Not frmPrincipal.CHECACONX() Then
            Exit Function
        End If
        Dim NUM As Integer
        NUM = 1
        Dim SQLMOV As New SqlClient.SqlCommand("SELECT DBO.SIGUIENTENOTA('" + frmPrincipal.SucursalBase + "')", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLMOV.ExecuteReader
        If LECTOR.Read Then
            NUM = LECTOR(0)
            LECTOR.Close()
            Return NUM
        Else
            LECTOR.Close()
            Return NUM
        End If
        'Catch ex As Exception
        '    Me.Close()
        'End Try
    End Function
    Private Function LLAMAEMPLEADO() As Boolean
        CARGANOTA()
        Me.Opacity = 25
        Dim VNV As New frmEntrarCaja
        VNV.ShowDialog()
        If VNV.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTNOTA.Text = CARGANOTA.ToString
            NOMEMP = VNV.NOMBRE
            NE = VNV.CAJERA
            CAJA = VNV.NOCAJA
            ''CAJA = 1

            Me.Opacity = 100
            CHECATABLA()
            Me.Text = "Nota de Venta. " + frmPrincipal.NombreComun + " Caja # " + CAJA.ToString + " Cajera: " + NOMEMP
            If CAJA = 1 Then
                Me.BackColor = Color.AliceBlue
            End If
            If CAJA = 2 Then
                Me.BackColor = Color.DeepSkyBlue
            End If
            If CAJA = 3 Then
                Me.BackColor = Color.MediumTurquoise
            End If
            If CAJA = 4 Then
                Me.BackColor = Color.RosyBrown
            End If
            If CAJA = 5 Then
                Me.BackColor = Color.LightSeaGreen
            End If
            If CAJA = 6 Then
                Me.BackColor = Color.LightPink
            End If
            If CAJA = 7 Then
                Me.BackColor = Color.Orchid
            End If

            If HAYZ() Then
                MessageBox.Show("No se puede registrar venta en esta caja, ya se ha realizado un corte Z el día de hoy", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Return False
            End If
            Return True
        Else
            Return False
            Me.Close()
        End If
    End Function
    Private Function HAYZ() As Boolean
        If Not frmPrincipal.CHECACONX() Then
            Return False
        End If

        Try
            Dim QUERY As String
            QUERY = "SELECT FECHAZ FROM CORTES WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CAJA=" + CAJA.ToString + ""
            Dim SQL As New SqlClient.SqlCommand(QUERY, frmPrincipal.CONX)
            Dim LEC As SqlClient.SqlDataReader
            LEC = SQL.ExecuteReader
            If LEC.Read Then
                FZ = LEC(0)
                LEC.Close()
            Else
                LEC.Close()
                MessageBox.Show("No se Puede Cobrar en esta Caja, No esta Correcta la Información, Favor de Comunicarse con Sistemas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Me.Close()
            End If

            If FZ.Date < Now.Date Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            MessageBox.Show("Hubo un conflicto con las cajas favor de comunicarse con Sistemas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End Try
    End Function
    Private Sub INICIALIZAR()
        For X = 1 To 10
            PGRU(X) = New PictureBox
            PPRO(X) = New PictureBox
            LBG(X) = New Label
            LBP(X) = New Label
        Next
        PGRU(1) = BTNG1
        PGRU(2) = BTNG2
        PGRU(3) = BTNG3
        PGRU(4) = BTNG4
        PGRU(5) = BTNG5
        PGRU(6) = BTNG6
        PGRU(7) = BTNG7
        PGRU(8) = BTNG8
        PGRU(9) = BTNG9
        PGRU(10) = BTNG10

        PPRO(1) = BTNP1
        PPRO(2) = BTNP2
        PPRO(3) = BTNP3
        PPRO(4) = BTNP4
        PPRO(5) = BTNP5
        PPRO(6) = BTNP6
        PPRO(7) = BTNP7
        PPRO(8) = BTNP8

        LBG(1) = LB1
        LBG(2) = LB2
        LBG(3) = LB3
        LBG(4) = LB4
        LBG(5) = LB5
        LBG(6) = LB6
        LBG(7) = LB7
        LBG(8) = LB8
        LBG(9) = LB9
        LBG(10) = LB10

        LBP(1) = LBL1
        LBP(2) = LBL2
        LBP(3) = LBL3
        LBP(4) = LBL4
        LBP(5) = LBL5
        LBP(6) = LBL6
        LBP(7) = LBL7
        LBP(8) = LBL8
        For X = 1 To 10
            LBP(X).Visible = False
            PPRO(X).Visible = False
        Next
    End Sub
    Private Sub CARGAGRUPOS()
        LGRU.Clear()
        Dim SQL As New SqlClient.SqlCommand("SELECT G.CLAVE,G.NOMBRE FROM GRUPOTOUCH G WHERE G.ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        While LEC.Read()
            LGRU.Add(LEC(0))
            NGRU.Add(LEC(1))
        End While
        LEC.Close()
        INDG = 1
        ACTUALIZAG()
        PANELP.Enabled = False
        SQL.Dispose()
    End Sub
    Private Sub CARGAPLATILLOS()
        LPRO.Clear()
        NPRO.Clear()
        PPRE.Clear()
        IPRO.Clear()
        Dim X As Integer
        Dim DRV As DataRowView
        For X = 0 To DV.Count - 1
            DRV = DV.Item(X)
            LPRO.Add(DRV.Item(2))
            NPRO.Add(DRV.Item(3))
            PPRE.Add(DRV.Item(4))
            IPRO.Add(DRV.Item(2))
        Next
        INDP = 1
        PANELP.Enabled = True
        ACTUALIZAP()
    End Sub
    Private Sub ACTUALIZAP()
        If INDP > 1 Then
            BTNANTP.Enabled = True
        Else
            BTNANTP.Enabled = False
        End If

        'Dim MAXINDG As Integer
        'MAXINDG = CType((LGRU.Count / 10), Integer)
        If ((INDP - 1) * NCUAP + NCUAP) < LPRO.Count Then
            BTNSIGP.Enabled = True
        Else
            BTNSIGP.Enabled = False
        End If
        ACOMODAPLATILLOS()
    End Sub
    Dim MAXINDG As Integer
    Private Sub ACTUALIZAG()
        If INDG > 1 Then
            BTNANTG.Enabled = True
        Else
            BTNANTG.Enabled = False
        End If
        MAXINDG = CType((LGRU.Count / 10), Integer)
        If ((INDG - 1) * 10 + 10) < LGRU.Count Then
            BTNSIGG.Enabled = True
        Else
            BTNSIGG.Enabled = False
        End If
        ACOMODAGRUPOS()
        'MessageBox.Show(MAXINDG.ToString, MAXINDG.ToString, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub
    Dim INICIO As Integer
    Dim FIN As Integer
    Dim FF As Integer
    Dim UBI As String
    Private Sub ACOMODAGRUPOS()
        INICIO = (INDG - 1) * 10
        FIN = INICIO + 10
        If FIN > LGRU.Count Then
            FIN = LGRU.Count
        Else

        End If
        FF = FIN
        While FF > 10
            FF = FF - 10
        End While
        For X = 1 To 10
            PGRU(X).Visible = True
            ''REVISAR ESTO SI NO HAY GENERAR BOTONES
            LBG(X).Visible = False
            PGRU(X).Image = PBIMG.Image
        Next
        UBI = ""
        For X = 1 To FF
            UBI = LGRU(INICIO + X - 1)
            LBG(X).Text = NGRU(INICIO + X - 1)
            'LBP(X).Text = NPRO(INICIO + X - 1)
            PGRU(X).ImageLocation = "C:/FOTOSMAGOS/GB" + UBI + ".JPG"
            PGRU(X).SizeMode = PictureBoxSizeMode.StretchImage
        Next
        If FF < 10 Then
            For X = FF + 1 To 10
                PGRU(X).Visible = False
                LBG(X).Visible = False
                PGRU(X).Text = ""
            Next
        End If
    End Sub
    Private Sub ACOMODAPLATILLOS()
        INICIO = (INDP - 1) * NCUAP
        FIN = INICIO + NCUAP
        If FIN > LPRO.Count Then
            FIN = LPRO.Count
        Else
        End If
        FF = FIN
        While FF > NCUAP
            FF = FF - NCUAP
        End While
        For X = 1 To NCUAP
            ''VISIBLE=TRUE
            PPRO(X).Visible = True
            LBP(X).Visible = False
            PPRO(X).Image = PBIMG.Image
        Next
        UBI = ""
        For X = 1 To FF
            'UBI = IPRO(INICIO + X - 1)
            UBI = LPRO(INICIO + X - 1).ToString
            LBP(X).Text = NPRO(INICIO + X - 1)
            PPRO(X).ImageLocation = "C:/FOTOSMAGOS/" + UBI + ".JPG"
            PPRO(X).SizeMode = PictureBoxSizeMode.StretchImage
        Next
        If FF < NCUAP Then
            For X = FF + 1 To NCUAP
                PPRO(X).Visible = False
                LBP(X).Visible = False
                PPRO(X).Text = ""
            Next
        Else
        End If
        Try
            BTNP1.Focus()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CARGADATAVIEW(ByVal GRUPO As String)
        DV = New DataView(frmPrincipal.DTPLATILLOS, "CGRUPO='" + GRUPO + "'", "ORDEN", DataViewRowState.CurrentRows)
        'DGV2.DataSource = DT
        CARGAPLATILLOS()
    End Sub
    Private Sub BTNSIGG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNSIGG.Click
        INDG = INDG + 1
        ACTUALIZAG()
    End Sub
    Private Sub BTNANTG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNANTG.Click
        INDG = INDG - 1
        ACTUALIZAG()
    End Sub
    Private Sub BTNSIGP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNSIGP.Click
        INDP = INDP + 1
        ACTUALIZAP()
    End Sub
    Private Sub BTNANTP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNANTP.Click
        BTNP1.Focus()
        INDP = INDP - 1
        ACTUALIZAP()
    End Sub
    Private Sub BTNG1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNG1.Click, BTNG1.Click, BTNG2.Click, BTNG3.Click, BTNG4.Click, BTNG5.Click, BTNG6.Click, BTNG7.Click, BTNG8.Click, BTNG9.Click, BTNG10.Click
        'CARGAPLATILLOS(LGRU(((INDG - 1) * 10) + CType(sender.tag, Integer) - 1))
        'Dim AA As Integer
        'AA = LGRU.Count
        CARGADATAVIEW(LGRU(((INDG - 1) * 10) + CType(sender.tag, Integer) - 1))
    End Sub


    Private Sub BTNP1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNP1.Click, BTNP2.Click, BTNP3.Click, BTNP4.Click, BTNP5.Click, BTNP6.Click, BTNP7.Click, BTNP8.Click
        If String.IsNullOrEmpty(LBLCANT.Text) Then
            MessageBox.Show("Favor de Especificar una Cantidad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            LBLCANT.Text = "1"
            Exit Sub
        End If
        If LBLCANT.Text = "" Or LBLCANT.Text = "0" Then
            MessageBox.Show("Debe Especificar una Cantidad Mayor o Igual que 1 (Uno)", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        Dim POS As Integer
        POS = (INDP - 1) * NCUAP + CType(sender.TAG, Integer) - 1
        Dim X As Integer
        Dim ENC As Boolean
        ENC = False
        For X = 0 To frmPrincipal.DTCOMBOPAQ.Rows.Count - 1
            If LPRO(POS).ToString = frmPrincipal.DTCOMBOPAQ.Rows(X).Item(0).ToString Then
                ENC = True
            End If
        Next
        If ENC Then
            frmArmaCombo.MOSTRAR(LBLCANT.Text, LPRO(POS))
        Else
            frmPrincipal.PRE.Agregar(LPRO(POS), NPRO(POS), CType(LBLCANT.Text, Integer), PPRE(POS), 0, PPRE(POS), 0)
        End If


        DGV.DataSource = frmPrincipal.PRE.ElementosAgregados
        LBLCANT.Text = "1"
        TOCADO = False
        'lo quite carlos lerma 
        'If AGREGARMAS(COD) Then
        '    AGREGAR(ACOD, ADES, CANT, APRE)
        'End If
        ACOMODARDGV()
        CHECATABLA()
        ' TXTEFECTIVO.SelectAll()
        LBLCANT.Focus()
        CHECAFOCUS()
        Label10.Text = CNUM.Letras(TXTTOT.Text)
    End Sub
    Private Sub ACTUALIZAINFOVENTANA()
        DGV.DataSource = frmPrincipal.PRE.ElementosAgregados
        LBLCANT.Text = "1"
        TOCADO = False
        'lo quite carlos lerma 
        'If AGREGARMAS(COD) Then
        '    AGREGAR(ACOD, ADES, CANT, APRE)
        'End If
        ACOMODARDGV()
        CHECATABLA()
        ' TXTEFECTIVO.SelectAll()
        LBLCANT.Focus()
        CHECAFOCUS()
        Label10.Text = CNUM.Letras(TXTTOT.Text)
    End Sub
    Private Sub ACOMODARDGV()
 
        DGV.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        DGV.Columns(0).ReadOnly = True
        DGV.Columns(1).ReadOnly = True
        DGV.Columns(2).ReadOnly = True
        DGV.Columns(3).ReadOnly = True
        DGV.Columns(5).ReadOnly = True
        DGV.Columns(0).Width = 5

        DGV.Columns(2).DefaultCellStyle = FORMATONUMERICO()
        DGV.Columns(3).DefaultCellStyle = FORMATONUMERICO()
        DGV.Columns(4).DefaultCellStyle = FORMATONUMERICO()
        DGV.Columns(5).DefaultCellStyle = FORMATONUMERICO()
        'DGV.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        DGV.Columns(10).Visible = False
        DGV.Columns(9).Visible = False
        DGV.Columns(8).Visible = False
        DGV.Columns(7).Visible = False
        DGV.Columns(6).Visible = False

        'DGV.Columns(0).SortMode = DataGridViewColumnSortMode.Programmatic
        'DGV.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
        'DGV.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
        'DGV.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
        'DGV.Columns(4).SortMode = DataGridViewColumnSortMode.Programmatic
        'DGV.Columns(5).SortMode = DataGridViewColumnSortMode.Programmatic

        ' MARCAPRECIOSPROMOCIONES()
    End Sub
    Private Function CHECACANTIDADES() As Boolean
        BTNCOBRAR.Enabled = True
        'TXTCAMBIO.Text = "0"
        Dim TOTAL, EFECTIVO As Double
        Try
            TOTAL = CType(TXTTOT.Text, Double)
            'EFECTIVO = CType(TXTEFECTIVO.Text, Double)
        Catch ex As Exception
            Return False
            Exit Function
        End Try
        If EFECTIVO >= TOTAL Then
            'BTNCOBRAR.Enabled = True
            'TXTCAMBIO.Text = FormatNumber(EFECTIVO - TOTAL).ToString()
            Return True
        Else
            'TXTCAMBIO.Text = 0
            Return False
        End If
    End Function
    Public Sub CHECATABLA()
        Try
            DGV.Columns(4).ReadOnly = True
        Catch ex As Exception

        End Try

        If frmPrincipal.PRE.CuentaElementos <= 0 Then
            TXTTOT.Text = 0
            BTNQUITAR.Enabled = False
        Else
            TXTTOT.Text = FormatNumber(frmPrincipal.PRE.Total, 2).ToString
            BTNQUITAR.Enabled = True
          
        End If
        If DGV.Rows.Count <= 0 Then
        Else
            Dim CR As Integer
            CR = DGV.Rows.Count - 1
            Try
                DGV.CurrentCell = DGV.Rows(CR).Cells(2)
            Catch ex As Exception

            End Try

        End If
        CHECACANTIDADES()
    End Sub
    Private Sub BTN1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN1.Click, BTN0.Click, BTN2.Click, BTN3.Click, BTN4.Click, BTN5.Click, BTN6.Click, BTN7.Click, BTN8.Click, BTN9.Click

        If CHECAFOCUS() Then
            MODIFICACANTIDAD(CType(sender.TAG, String))
            TOCADO = True
        Else
            MODIFICAEFECTIVO(CType(sender.TAG, String))
            TOCADO2 = True
        End If
    End Sub
    Private Sub MODIFICACANTIDAD(ByVal NUM As String)
        If LBLCANT.Text.Length >= 3 Then
            Exit Sub
        End If
        If TOCADO Then
            LBLCANT.Text = LBLCANT.Text + NUM
        Else
            LBLCANT.Text = NUM
        End If
    End Sub
    Private Sub MODIFICAEFECTIVO(ByVal NUM As String)
        'If TXTEFECTIVO.Text.Length >= 10 Then
        '    Exit Sub
        'End If
        'If TOCADO2 Then
        '    TXTEFECTIVO.Text = TXTEFECTIVO.Text + NUM
        'Else
        '    TXTEFECTIVO.Text = NUM
        'End If
    End Sub
    Private Function CHECAFOCUS() As Boolean
        If CANTFOCUS Then
            LBLCANT.BackColor = Drawing.Color.Lime
            'TXTEFECTIVO.BackColor = Drawing.Color.White
            'BTNPUNTO.Enabled = False
            Return True
        Else
            'BTNPUNTO.Enabled = True
            LBLCANT.BackColor = Drawing.Color.White
            'TXTEFECTIVO.BackColor = Drawing.Color.Lime
            Return False
        End If
    End Function

    Private Sub BTNCANCELAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        LBLCANT.Text = "1"
        TOCADO = False
    End Sub

    Private Sub BTNQUITAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNQUITAR.Click
        If DGV.Rows.Count >= 1 Then
            If DGV.Item(7, DGV.CurrentRow.Index).Value.ToString <> "0" Then
                MessageBox.Show("Producto pertenece a un combo/paquete, no se puede modificar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
        LBLCANT.Text = "1"
        Sumar(False)
        Label10.Text = CNUM.Letras(TXTTOT.Text)
    End Sub

    Private Sub Sumar(ByVal V As Boolean)

        frmPrincipal.PRE.AgregaQuita(DGV.Item(0, DGV.CurrentRow.Index).Value.ToString, 1, V)

        DGV.DataSource = frmPrincipal.PRE.ElementosAgregados
        ACOMODARDGV()
        CHECATABLA()
    End Sub

    Private Sub BTNELIMINAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNELIMINAR.Click
        Dim xyz As Short
        xyz = MessageBox.Show("¿Desea cancelar la información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Sub
        End If
        frmPrincipal.PRE.EliminarNota()
        DGV.DataSource = frmPrincipal.PRE.ElementosAgregados
        ACOMODARDGV()
        CHECATABLA()
        frmPrincipal.PRE.DESCGENERAL = 0
        DGV.Refresh()
        LBLCANT.Text = "1"
        Label10.Text = CNUM.Letras(TXTTOT.Text)
    End Sub



    Private Sub BTNCANCELAR2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'TXTEFECTIVO.Text = ""
        TOCADO2 = False
        'CHECACANTIDADES()
    End Sub

    'Private Sub TXTEFECTIVO_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTEFECTIVO.Enter
    '    CANTFOCUS = False
    '    CHECAFOCUS()
    'End Sub

    Private Sub LBLCANT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LBLCANT.Enter
        CANTFOCUS = True
        CHECAFOCUS()
    End Sub
    Private Function CHECAEXISTENCIA(ByVal PRO As String, ByVal CANT As Double) As Boolean
        Return True
        If Not frmPrincipal.CHECACONX Then
            Return False
        End If
        Dim EXIS As Boolean
        EXIS = False
        Dim REG As Double
        Dim RES As String
        RES = "Sin Relación"
        Dim QUERY As String

        QUERY = "SELECT DBO.EXISTENCIAPVPROVCF('" + frmPrincipal.SucursalBase + "','" + PRO + "')"
        Dim SQLSELECT As New SqlClient.SqlCommand(QUERY, frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLSELECT.ExecuteReader
        If LECTOR.Read Then
            RES = LECTOR(0)
            'LBLEXIS.Text = LECTOR(0)
        End If
        LECTOR.Close()
        SQLSELECT.Dispose()
        'End If
        If RES = "Sin Relación" Then
            Return True
        Else
            REG = CType(RES, Double)
        End If
        If REG >= CANT Then
            Return True
        End If

        Return False
    End Function
    Private Function VERIFICAREXISTENCIA() As Boolean
        Dim X As Integer
        For X = 0 To DGV.Rows.Count - 1
            If Not CHECAEXISTENCIA(DGV.Item(0, X).Value, DGV.Item(2, X).Value) Then
                MessageBox.Show("Existencia Insuiciente en: " + DGV.Item(1, X).Value.ToString, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Return False
            End If
        Next
        Return True

    End Function
    Private Sub BTNCOBRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCOBRAR.Click
        If frmPrincipal.PRE.ElementosAgregados.Rows.Count <= 0 Then
            MessageBox.Show("Favor de agregar productos a la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If frmPrincipal.VentaSinResguardo >= 2 Then
            MessageBox.Show("Debe realizar un resguardo, no se permite continuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Exit Sub
        End If
        GUARDAR()
    End Sub
    Private Function IMPRIMIRNOTA(ByVal LANOTA As Integer) As Boolean
        If Not frmPrincipal.CHECACONX Then
            Return False
        End If
        Dim QUER As String
        QUER = "SELECT TP.NOMBRE TIPOPAGO,S.NOMBRECOMUN,S.NOMBREFISCAL,N.NOTA,D.PRODUCTO CODIGO,P.NOMBRECORTO PRODUCTO,D.CANTIDAD, D.DESCUENTO,DBO.PRECIODESC(D.TOTAL,D.CANTIDAD,D.DESCUENTO) PRECIO,D.TOTAL,SUBTOTAL=DBO.VALORESFAC(D.CANTIDAD,P.FACTORIVA,P.TASAIVA,P.FACTORIEPS,P.TASAIEPS,D.DESCUENTO,D.TOTAL,5),IVA=DBO.VALORESFAC(D.CANTIDAD,P.FACTORIVA,P.TASAIVA,P.FACTORIEPS,P.TASAIEPS,D.DESCUENTO,D.TOTAL,2),AHORRO=DBO.VALORESFAC(D.CANTIDAD,P.FACTORIVA,P.TASAIVA,P.FACTORIEPS,P.TASAIEPS,D.DESCUENTO,D.TOTAL,4), E.NOMBRE EMPLEADO,N.FECHA,S.DIRECCION,S.CIUDAD,S.RFC,S.TELEFONO,N.PAGACON PAGA,N.CAMBIO,N.NOCAJA,TIPO='Ticket',VNOM='',VUNI='',D.DESCUENTO,CL.NOMBRE,S.COMENTARIO1,S.COMENTARIO2,S.COMENTARIO3,S.CP,CL.TELEFONO TEL,DIR=CL.CALLE+' '+CL.NOEXT +' INT '+CL.NOINT +' REF '+CL.REFERENCIA,R='',TV.NOMBRE TIPOVENTA,CL.CLAVE CLACLI,N.COMENTARIO ,S.MSGFACTURA,S.MSGPRIVACIDAD,S.REGIMEN,N.UNIVERSAL,S.MSGFACTURA2,MENSAJEPUNTOS='',NOTAAPP=CONVERT(BIT,0),AHORROENTICKET='',PAGADOCONECTA='',N.NOTA IDINT,FF=DBO.BIT2TEXT(15,0),N.TIPOVENTA ORIGENVTA FROM NOTASDEVENTA N INNER JOIN DETALLENOTASDEVENTA D ON N.SUCURSAL=D.SUCURSAL AND N.NOTA=D.NOTA INNER JOIN SUCURSALES S ON N.SUCURSAL=S.CLAVE INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE INNER JOIN TIPOSPAGOS TP ON TP.CLAVE=N.TIPOPAGO INNER JOIN EMPLEADOSSUCURSALES E ON N.SUCURSAL=E.SUCURSAL AND N.CAJERA=E.CLAVE INNER JOIN CORTES C ON N.SUCURSAL=C.SUCURSAL AND N.NOCAJA=C.CAJA INNER JOIN CLIENTES CL ON CL.CLAVE=N.CLIENTE INNER JOIN TIPOVENTA TV ON N.TIPOVENTA=TV.CLAVE  WHERE N.SUCURSAL='" + frmPrincipal.SucursalBase + "' AND N.NOTA=" + LANOTA.ToString
        sb.Items.Add("Ejecución de imprimir nota")
        Dim REPI As New rptNotaDeVenta2
        'IMPRIMIRREPORTE(REPI, DTIMP, NC, "\\sd2\surtido")
        IMPRIMIRREPORTE(REPI, LLENATABLA(QUER, frmPrincipal.CadenaConexion), 1, "")
        If RB1.Checked Then
        Else
            IMPRIMESECCIONES(LANOTA)
        End If

        '' secciones de impresion
    End Function
    Private Function IMPRIMESECCIONES(ByVal LANOTA As Integer) As Boolean
        Dim QUERY As String
        QUERY = "SELECT D.NOTA,D.CANTIDAD,P.NOMBRE,PRECIO=0,D.TOTAL,S.NOMBRE SECCION,NOORDEN=0,S.IMAGEN,T.NOMBRE TIPO FROM NOTASDEVENTA N INNER JOIN DETALLENOTASDEVENTA D ON N.SUCURSAL=D.SUCURSAL INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE INNER JOIN SECCIONESIMPRESION S ON P.SECCIONIMPRESION =S.CLAVE INNER JOIN TIPOVENTA T ON N.TIPOVENTA =T.CLAVE WHERE N.SUCURSAL='" + frmPrincipal.SucursalBase + "' AND N.NOTA='" + LANOTA.ToString + "' AND S.CLAVE<>0"
        Dim DTT As New DataTable
        DTT = LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
        If DTT.Rows.Count <= 0 Then
            Dim RPT As New rptSeccionImpresion
            IMPRIMIRREPORTE(RPT, DTT, 1, "")
        End If
    End Function

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        LLAMACLIENTE()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim X As Integer
        For X = 0 To frmPrincipal.DTPLATILLOS.Rows.Count - 1
            If frmPrincipal.DTPLATILLOS.Rows(X).Item(2).ToString = "3" Then
                frmPrincipal.PRE.Agregar("3", frmPrincipal.DTPLATILLOS.Rows(X).Item(3).ToString, 1, frmPrincipal.DTPLATILLOS.Rows(X).Item(4).ToString, 0, frmPrincipal.DTPLATILLOS.Rows(X).Item(4).ToString, 0)
            End If
        Next

        ACTUALIZAINFOVENTANA()
    End Sub

    Private Sub BTNEXTRAS_Click(sender As Object, e As EventArgs) Handles BTNEXTRAS.Click
        If DGV.Rows.Count <= 0 Then
            Exit Sub
        Else
            Dim ENC As Boolean
            ENC = False
            Dim X As Integer
            For X = 0 To frmPrincipal.DTPLATILLOSEXTRA.Rows.Count - 1
                If frmPrincipal.DTPLATILLOSEXTRA.Rows(X).Item(0).ToString = DGV.Item(0, DGV.CurrentRow.Index).Value.ToString Then
                    ENC = True
                End If
            Next
            If ENC Then
                frmAgregarExtras.MOSTRAR(DGV.Item(0, DGV.CurrentRow.Index).Value.ToString, DGV.Item(7, DGV.CurrentRow.Index).Value.ToString, DGV.CurrentRow.Index, DGV.Item(1, DGV.CurrentRow.Index).Value.ToString)
                ACTUALIZAINFOVENTANA()
            End If
        End If
    End Sub

    Private Sub GUARDAR()

        sb.Items.Clear()
        Dim DESC As Double
        DESC = 0
        sb.Items.Add("Aplicando Descuento")
        Try
            DESC = CType(TXTDESC.Text, Double)
        Catch ex As Exception
            TXTDESC.Text = "0"
        End Try

        If TXTDESC.Text <> 0 Then
            frmPrincipal.PRE.PoneDescuento(DESC)
        End If
        sb.Items.Add("Descuento aplicado")
        CHECATABLA()

        Dim LANOTA As Integer
        'If Not CHECACANTIDADES() Then
        '    Exit Sub
        'End If
        If Not VERIFICAREXISTENCIA() Then
            Exit Sub
        End If

        Dim TP As Integer


        DOM = 1
        COM = ""



        AH = False
        HE = Now


        DOM = "1"

        If RB2.Checked Or RB2.Checked Then
            frmComentario.MOSTRAR("", "", CLIENTEBASE, 1, -1, False)
            If frmComentario.DialogResult = Windows.Forms.DialogResult.Yes Then
                COM = frmComentario.TXTCOM.Text
                DOM = frmComentario.DOM
                AH = frmComentario.AH
                HE = frmComentario.HE
            End If

        End If



        LNC = 0
        'If TP = 6 Then
        '    Dim VSF As New frmSeleccionaNotaCredito
        '    VSF.MOSTRAR(frmPrincipal.EmisorBase, CLIENTEBASE, NOMBRECLIENTEBASE, RFCCLIENTEBASE, frmPrincipal.SucursalBase)
        '    If VSF.DialogResult = Windows.Forms.DialogResult.Yes Then
        '        If VSF.CTOT < CType(TXTTOT.Text, Double) Then
        '            MessageBox.Show("El total de la nota de crédito referenciada es menor que la nota de venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit Sub
        '        End If
        '        Dim SQLCNC As New SqlClient.SqlCommand("SPCARGOSNOTACREDITOPRO", frmPrincipal.CONX)
        '        SQLCNC.CommandType = CommandType.StoredProcedure
        '        LNC = VSF.CNC
        '        SQLCNC.Parameters.Add("@NC", SqlDbType.VarChar).Value = VSF.CNC
        '        SQLCNC.Parameters.Add("@TI", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
        '        SQLCNC.Parameters.Add("@NOTA", SqlDbType.VarChar).Value = LANOTA
        '        SQLCNC.Parameters.Add("@TOT", SqlDbType.Float).Value = CType(TXTTOT.Text, Double)
        '        SQLCNC.Parameters.Add("@ESCOB", SqlDbType.Bit).Value = 0
        '        SQLCNC.Parameters.Add("@USU", SqlDbType.VarChar).Value = frmPrincipal.Usuario
        '        SQLCNC.ExecuteNonQuery()
        '    Else
        '        Exit Sub
        '    End If
        'End If
        Cursor.Show()
        Cursor.Current = Cursors.WaitCursor
        sb.Items.Add("Procesando...")
        TT = 0
        SW.Reset()
        SW.Start()



        Dim AWA, AOYR As Boolean
        Dim BANCO, DIGITOS, AUTORIZACION, EFECTIVO, CAMBIO As String


        Dim VPAGOS As New frmPagos
        VPAGOS.MOSTRAR(TXTTOT.Text, LTP, CBTP, LBAN, CBBANCO)
        If VPAGOS.DialogResult = Windows.Forms.DialogResult.Yes Then
            BANCO = VPAGOS.BANCO
            DIGITOS = VPAGOS.DIGITOS
            AUTORIZACION = VPAGOS.AUTORIZACION
            EFECTIVO = VPAGOS.EFECTIVO
            CAMBIO = VPAGOS.CAMBIO
            TP = VPAGOS.TIPOPAGO
        Else
            Exit Sub
        End If

        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If




        ''Dim SQLIMP As New SqlClient.SqlCommand("UPDATE NOTASDEVENTAPRO SET ESTADO=1 WHERE TIENDA='" + frmPrincipal.SucursalBase + "' AND NOCAJA=" + CAJA.ToString + " AND ESTADO=3 AND FECHA>=(SELECT DBO.LAFECHA())", frmPrincipal.CONX)
        Dim SQLIMP As New SqlClient.SqlCommand("UPDATE NOTASDEVENTA SET ESTADO=1 WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND NOCAJA=" + CAJA.ToString + " AND ESTADO=3 AND FECHA>=(SELECT DBO.LAFECHA())", frmPrincipal.CONX)
        SQLIMP.CommandTimeout = 300
        SQLIMP.ExecuteNonQuery()
        SQLIMP.Dispose()



        'If CHKCP.Checked Then
        '    frmPrincipal.PRE.AplicarPuntos(PUNTOSCLIENTES())
        'Else
        '    frmPrincipal.PRE.AplicarPuntos(0)
        'End If
        Dim TV As Integer
        If RB1.Checked Then
            TV = 1
        End If
        If RB2.Checked Then
            TV = 2
        End If
        If RB3.Checked Then
            TV = 3
        End If

        Dim SQLGUARDA1 As New SqlClient.SqlCommand("SPNOTASDEVENTA", frmPrincipal.CONX)
        If HAYZ() Then
            Dim xyz As Short
            xyz = MessageBox.Show("Se ha realizado un corte Z este día, la(s) nota(s) cobrada(s) aparecerán en el corte del día siguiente, ¿Desea continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If xyz <> 6 Then
                Exit Sub
            End If
            SQLGUARDA1.Parameters.Add("@FEC", SqlDbType.DateTime).Value = FZ.Date.AddHours(33)

            'Else
            '    SQLGUARDA1.CommandText = "INSERT INTO NOTASDEVENTA(TIENDA,NOTA,TOTAL,ESTADO,NOCAJA,CAJERA,TIPOPAGO,CLIENTE,SD,BANCO,REF,AUT,TIPOTARJETA,COMENTARIO,EMPLEADO,VOLANTE) VALUES (@TI,@NOTA,@TOT,@EDO,@NOCAJA,@EMP,@TP,@CLIE,@SD,@BAN,@REF,@AUT,@TT,@COM,@EMPV,@VOL)"
        End If
        ''' aqui ya necesito saber si es ff
        ''' 

        LANOTA = CARGANOTA()

        Dim UNIVERSAL As String
        UNIVERSAL = Encrypt(frmPrincipal.IDNU + "F" + LANOTA.ToString).ToUpper
        TXTNOTA.Text = LANOTA.ToString
        SQLGUARDA1.CommandTimeout = 300
        SQLGUARDA1.CommandType = CommandType.StoredProcedure
        SQLGUARDA1.Parameters.Add("@TI", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
        SQLGUARDA1.Parameters.Add("@NOTA", SqlDbType.Int).Value = LANOTA
        SQLGUARDA1.Parameters.Add("@TOT", SqlDbType.Float).Value = CType(TXTTOT.Text, Double)
        SQLGUARDA1.Parameters.Add("@EMP", SqlDbType.VarChar).Value = NE
        SQLGUARDA1.Parameters.Add("@UNI", SqlDbType.VarChar).Value = UNIVERSAL
        SQLGUARDA1.Parameters.Add("@EDO", SqlDbType.Int).Value = 1
        SQLGUARDA1.Parameters.Add("@NOCAJA", SqlDbType.Int).Value = CAJA
        SQLGUARDA1.Parameters.Add("@BAN", SqlDbType.Int).Value = BANCO
        SQLGUARDA1.Parameters.Add("@DOM", SqlDbType.Int).Value = 1
        SQLGUARDA1.Parameters("@BAN").Value = BANCO



        SQLGUARDA1.Parameters.Add("@REF", SqlDbType.VarChar).Value = DIGITOS
        SQLGUARDA1.Parameters.Add("@AUT", SqlDbType.VarChar).Value = AUTORIZACION
        SQLGUARDA1.Parameters.Add("@COM", SqlDbType.VarChar).Value = COM
        SQLGUARDA1.Parameters.Add("@TV", SqlDbType.Int).Value = TV
        SQLGUARDA1.Parameters.Add("@AH", SqlDbType.Bit).Value = AH

        If AH Then
            SQLGUARDA1.Parameters.Add("@HE", SqlDbType.DateTime).Value = HE
        End If

        SQLGUARDA1.Parameters.Add("@CLIE", SqlDbType.Int).Value = CLIENTEBASE

        If RB2.Checked Then
            SQLGUARDA1.Parameters.Add("@SD", SqlDbType.Bit).Value = 1
        Else
            SQLGUARDA1.Parameters.Add("@SD", SqlDbType.Bit).Value = 0
        End If


        SQLGUARDA1.Parameters.Add("@TP", SqlDbType.Int).Value = TP


        SQLGUARDA1.Parameters.Add("@PAGA", SqlDbType.Float).Value = EFECTIVO
        SQLGUARDA1.Parameters.Add("@CAMBIO", SqlDbType.Float).Value = CAMBIO

        SQLGUARDA1.ExecuteNonQuery()
        SQLGUARDA1.Dispose()

        'Dim SQLNU As New SqlClient.SqlCommand("EXEC FEBAJAMAR.DBO.SPNOTASUNIVERSALES '" + UNIVERSAL + "','" + frmPrincipal.SucursalBase + "','" + LANOTA.ToString + "',0,'" + ELID.ToString + "'", frmPrincipal.CONX)
        'SQLNU.ExecuteNonQuery()
        sb.Items.Add("Información Nota Guardada")


        sb.Items.Add("Guardando Detalle de Nota")

        '            Dim SQLDETALLES As New SqlClient.SqlCommand("INSERT INTO DETALLENOTASDEVENTAF(TIENDA,NOTA,PRODUCTO,CANTIDAD,TOTAL,REGISTRO,DESCUENTO,PA,IDCOMBO) VALUES (@TI,@NOTA,@PROD,@CANT,@TOT,@REG,@DES,@PA,@IDC)", frmPrincipal.CONX)
        Dim SQLDETALLES As New SqlClient.SqlCommand("INSERT INTO DETALLENOTASDEVENTA(SUCURSAL,NOTA,PRODUCTO,CANTIDAD,TOTAL,REGISTRO,DESCUENTO,PA,IDCOMBO) VALUES (@SUC,@NOTA,@PROD,@CANT,@TOT,@REG,@DES,@PA,@IDC)", frmPrincipal.CONX)
        SQLDETALLES.CommandTimeout = 300
        SQLDETALLES.Parameters.Add("@SUC", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
        SQLDETALLES.Parameters.Add("@NOTA", SqlDbType.Int).Value = LANOTA
        SQLDETALLES.Parameters.Add("@PROD", SqlDbType.VarChar)
        SQLDETALLES.Parameters.Add("@CANT", SqlDbType.Float)
        SQLDETALLES.Parameters.Add("@TOT", SqlDbType.Float)
        SQLDETALLES.Parameters.Add("@DES", SqlDbType.Float)
        SQLDETALLES.Parameters.Add("@REG", SqlDbType.VarChar)
        SQLDETALLES.Parameters.Add("@PA", SqlDbType.VarChar)
        SQLDETALLES.Parameters.Add("@IDC", SqlDbType.VarChar)
        SQLDETALLES.CommandTimeout = 300
        Dim X As Integer
        For X = 0 To DGV.Rows.Count - 1
            Dim p As String
            Dim ca As Integer
            Dim t As Double
            p = DGV.Item(3, X).Value
            ca = DGV.Item(2, X).Value
            t = DGV.Item(5, X).Value
            SQLDETALLES.Parameters("@PROD").Value = DGV.Item(0, X).Value
            SQLDETALLES.Parameters("@CANT").Value = DGV.Item(2, X).Value
            SQLDETALLES.Parameters("@TOT").Value = DGV.Item(5, X).Value
            SQLDETALLES.Parameters("@DES").Value = DGV.Item(3, X).Value

            SQLDETALLES.Parameters("@PA").Value = DGV.Item(8, X).Value
            SQLDETALLES.Parameters("@IDC").Value = DGV.Item(7, X).Value

            SQLDETALLES.Parameters("@REG").Value = X
            SQLDETALLES.ExecuteNonQuery()
        Next
        SQLDETALLES.Dispose()

        sb.Items.Add("Información Detalle de Nota Guardada")
        Dim SQLVT As New SqlClient.SqlCommand("VERIFICATOTAL", frmPrincipal.CONX)
        SQLVT.CommandType = CommandType.StoredProcedure
        SQLVT.CommandTimeout = 300
        SQLVT.Parameters.Add("@SUC", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase  '' NECESITO EL ID
        SQLVT.Parameters.Add("@NOTA", SqlDbType.Int).Value = LANOTA   '' NECESITO EL ID
        SQLVT.ExecuteNonQuery()

        sb.Items.Add("Guardando Combos")


        Dim VCAJA As Integer
        VCAJA = CAJA
        Dim SQLGC As New SqlClient.SqlCommand("UPDATE CORTES SET PAGA='" + EFECTIVO.ToString + "',CAMBIO='" + CAMBIO.ToString + "',TOTAL='" + frmPrincipal.PRE.Total.ToString + "' WHERE SUCURSAL='" + frmPrincipal.SucursalBase + "' AND CAJA=" + VCAJA.ToString + "", frmPrincipal.CONX)
        SQLGC.CommandTimeout = 300
        SQLGC.ExecuteNonQuery()
        SQLGC.Dispose()
        'Try
        '    Shell("c:\caja.exe", AppWinStyle.Hide)
        'Catch ex As Exception

        'End Try

        'Try
        '    Shell("c:\caja.exe", AppWinStyle.Hide)
        'Catch ex As Exception

        'End Try
        SW.Stop()
        sb.Items.Add(String.Format("Tiempo empleado Guardar Nota: {0} milisegundos.", SW.ElapsedMilliseconds))
        TT = TT + SW.ElapsedMilliseconds
        SW.Reset()
        SW.Start()

        'If CHKFOL.Checked Then
        '    frmFoliadas.MOSTRAR(frmPrincipal.SucursalBase, LANOTA.ToString, ELID.ToString)
        '    If frmFoliadas.DialogResult <> Windows.Forms.DialogResult.Yes Then
        '        MessageBox.Show("No se guardó correctamente la información de la relación foliada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Sub
        '    End If
        'End If
        'frmPrincipal.CI.Abrir()
        sb.Items.Add("Imprimiendo Nota")
        IMPRIMIRNOTA(LANOTA)
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If

        SW.Stop()
        sb.Items.Add(String.Format("Tiempo empleado Imprimir Nota: {0} milisegundos.", SW.ElapsedMilliseconds))
        TT = TT + SW.ElapsedMilliseconds
        sb.Items.Add(String.Format("Tiempo Total en Nota: {0} milisegundos.", TT))

        Try
            If Not frmPrincipal.CHECACONX Then
                sb.Items.Add("Error Descargando Inventario")
                GBITACORA(frmPrincipal.Usuario, 2, "Descarga Venta Contado No Realizada", "", "", "Descarga de venta de Contado NO REALIZADA" + frmPrincipal.SucursalBase + " Nota: " + LANOTA.ToString)
            Else
                sb.Items.Add("Descargando Inventario")
                Dim SQLGUARDAR As New SqlClient.SqlCommand() With {.Connection = frmPrincipal.CONX, .CommandTimeout = 300, .CommandType = CommandType.StoredProcedure}
                SQLGUARDAR.Parameters.Add("@SUC", SqlDbType.VarChar).Value = frmPrincipal.SucursalBase
                SQLGUARDAR.Parameters.Add("@NOTA", SqlDbType.Int).Value = LANOTA
                SQLGUARDAR.Parameters.Add("@CRE", SqlDbType.Bit).Value = 0


                SQLGUARDAR.CommandText = "SPDESCARGAINVENTARIONOTA"
                SQLGUARDAR.ExecuteNonQuery()
                SQLGUARDAR.Dispose()
                GBITACORA(frmPrincipal.Usuario, 2, "Descarga Venta Contado", "", "", "Descarga de venta de Contado " + frmPrincipal.SucursalBase + " Nota: " + LANOTA.ToString)
            End If

        Catch ex As Exception
            sb.Items.Add("Error Descargando Inventario")
            GBITACORA(frmPrincipal.Usuario, 2, "Descarga Venta Contado No Realizada", "", "", "Descarga de venta de Contado NO REALIZADA" + frmPrincipal.SucursalBase + " Nota: " + LANOTA.ToString)
            'MessageBox.Show(ex.Message)
        End Try

        frmCambio.CAMBIO(CType(CAMBIO, Double))



        frmPrincipal.PRE.EliminarNota()

        LISTAPRENOTAS.Clear()
        frmPrincipal.PRE.DESCGENERAL = 0

        DGV.Refresh()
        Dim VSR As Double
        VSR = CHECARESGUARDO()
        If frmPrincipal.SucursalBase = "BMLM1" Then
            If VSR >= 3500 Then
                frmPrincipal.VentaSinResguardo += 1
                MessageBox.Show("Se le solicita que se realice un RESGUARDO", " A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            If VSR >= 3500 Then
                frmPrincipal.VentaSinResguardo += 1
                MessageBox.Show("Se le solicita que se realice un RESGUARDO", " A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If



        frmPrincipal.PRE.EliminarNota()
        CHECATABLA()
        DGV.Refresh()

        RB1.Checked = True
        RB2.Checked = False
        RB3.Checked = False
        TXTNOTA.Text = CARGANOTA()
        CBTP.SelectedIndex = 0
        TXTDESC.Text = 0

        'sb.Items.Clear()
        LLAMACLIENTE()
    End Sub
    Private Function CHECARESGUARDO() As Double
        If Not frmPrincipal.CHECACONX Then
            Return 0
        End If
        Dim REG As Double
        REG = 0
        Dim SQL As New SqlClient.SqlCommand("SELECT DBO.VENTASINRESGUARDAR('" + frmPrincipal.SucursalBase + "','" + CAJA.ToString + "')", frmPrincipal.CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        If LEC.Read Then
            REG = LEC(0)
        End If
        LEC.Close()
        SQL.Dispose()
        Return REG
    End Function
    Private Sub TXTDESC_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTDESC.Enter
        frmAutorizacionCredito.mostrar(1, "Autorización Descuento")
        If frmAutorizacionCredito.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTDESC.Focus()
            TXTDESC.SelectAll()
        Else
            TXTDESC.Text = "0"
            Exit Sub
        End If
    End Sub

    Private Sub TXTDESC_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTDESC.Leave
        Dim DESC As Double
        Try
            DESC = CType(TXTDESC.Text, Double)
            'If DESC > 10 Then
            '    MessageBox.Show("No se permiten descuentos mayores a 10%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TXTDESC.Text = "10"
            'End If
            Try
                DESC = CType(TXTDESC.Text, Double)
            Catch ex As Exception
                TXTDESC.Text = "0"
            End Try
            If TXTDESC.Text <> 0 Then
                frmPrincipal.PRE.PoneDescuento(DESC)
            End If
            ACOMODARDGV()
            CHECATABLA()
            ' TXTEFECTIVO.SelectAll()
            LBLCANT.Focus()
            CHECAFOCUS()
            Label10.Text = CNUM.Letras(TXTTOT.Text)
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub frmNotaDeVenta_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            RB1.Checked = True
        End If
        If e.KeyCode = Keys.F2 Then
            RB2.Checked = True
        End If
        If e.KeyCode = Keys.F3 Then
            RB3.Checked = True
        End If
        If e.KeyCode = Keys.F4 Then
            LLAMACLIENTE()
        End If

        If e.KeyData = Keys.Control + Keys.A Then
            ''cargar familiar peperoni
            '61          FAM PEPERONI
            Dim X As Integer
            For X = 0 To frmPrincipal.DTPLATILLOS.Rows.Count - 1
                If frmPrincipal.DTPLATILLOS.Rows(X).Item(2).ToString = "61" Then
                    frmPrincipal.PRE.Agregar("61", frmPrincipal.DTPLATILLOS.Rows(X).Item(3).ToString, 1, frmPrincipal.DTPLATILLOS.Rows(X).Item(4).ToString, 0, frmPrincipal.DTPLATILLOS.Rows(X).Item(4).ToString, 0)
                End If
            Next
        End If
        If e.KeyData = Keys.Control + Keys.B Then
            ''cargar personal peperoni
            '82          IND PEPERONI
            Dim X As Integer
            For X = 0 To frmPrincipal.DTPLATILLOS.Rows.Count - 1
                If frmPrincipal.DTPLATILLOS.Rows(X).Item(2).ToString = "82" Then
                    frmPrincipal.PRE.Agregar("82", frmPrincipal.DTPLATILLOS.Rows(X).Item(3).ToString, 1, frmPrincipal.DTPLATILLOS.Rows(X).Item(4).ToString, 0, frmPrincipal.DTPLATILLOS.Rows(X).Item(4).ToString, 0)
                End If
            Next
        End If
        If e.KeyCode = Keys.F5 Then
            Dim VTR As New frmTicketRepartidor
            VTR.MOSTRAR(CAJA, NE, NOMEMP)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmResguardos.MOSTRAR(CAJA, NE, NOMEMP, False)
    End Sub
End Class