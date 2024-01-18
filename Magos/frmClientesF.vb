Public Class frmClientesF
    Public ELCLIENTE As String
    Dim LTC As New List(Of String)
    Dim LDES As New List(Of String)
    Dim LVA As New List(Of String)
    Dim LZON As New List(Of String)
    Dim LRUT As New List(Of String)
    Dim LFC As New List(Of String)
    Dim LDOM As New List(Of String)
    Dim LLOC As New List(Of String)
    Dim LCOL As New List(Of String)
    Dim LCAJ As New List(Of String)
    Dim LLL As New List(Of String)
    Dim LCLA As New List(Of String)
    Dim LNOM As New List(Of String)
    Dim LMOT As New List(Of String)
    Dim DTTC As New DataTable
    Dim TC As Integer
    Dim VA, VM, OBS As String
    Dim DESDEBUSQUEDA As Boolean
    Dim VTEL As String
    Dim IND As Integer
    Dim ENC As Boolean
    Public CLIENTEDOM As String
    Private Sub frmClientesF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon

        LLENA2LISTAS(LCLA, LNOM, "SELECT CLAVE,NOMBRE FROM CLIENTES ORDER BY NOMBRE", frmPrincipal.CadenaConexion)

        IND = 0
        CHECAIND()
        If Not DESDEBUSQUEDA Then
            LLENACOMBOBOX(CBCOL, LCOL, "SELECT CLAVE,NOMBRE FROM COLONIAS WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)

            SIGCLIENTE()
            ACTIVAR(True)
            If Not String.IsNullOrEmpty(CLIENTEDOM) Then
                TXTCLA.Text = CLIENTEDOM
                CARGADATOS()
            End If
        End If
 

    End Sub
    Private Sub CHECAIND()
        If IND <= 0 Then
            BTNANTG.Enabled = False
        Else
            BTNANTG.Enabled = True
        End If
        If IND > LCLA.Count - 1 Then
            BTNSIGG.Enabled = False
        Else
            BTNSIGG.Enabled = True
        End If
    End Sub

    Private Sub ACTIVAR(ByVal V As Boolean)
        TXTCLA.Enabled = V
        TABCLI.Enabled = Not V
    End Sub
    Private Sub LIMPIAR()
        TXTNOM.Text = ""
        TXTTEL.Text = ""
        CBACT.SelectedIndex = 1
    End Sub
    Private Function SIGCLIENTE() As Integer
        If Not frmPrincipal.CHECACONX Then
            Return 0
        End If
        Dim REG As Integer
        REG = 0
        Dim SQL As New SqlClient.SqlCommand("SELECT DBO.SIGCLIENTE()", frmPrincipal.CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        If LEC.Read Then
            REG = LEC(0)
        End If
        LEC.Close()
        SQL.Dispose()
        TXTCLA.Text = REG.ToString
        Return REG
    End Function
    Private Sub CARGADATOS()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        ACTIVAR(False)
        TC = 0
        VA = ""
        Dim SQLG As New SqlClient.SqlCommand("SELECT NOMBRE,CALLE,TELEFONO,NOEXT,NOINT,ENTRE,YENTRE,REFERENCIA,COLONIA FROM CLIENTES C WHERE CLAVE=@CLA", frmPrincipal.CONX)
        SQLG.Parameters.Add("@CLA", SqlDbType.Int).Value = TXTCLA.Text
        Dim LECG As SqlClient.SqlDataReader
        LECG = SQLG.ExecuteReader
        If LECG.Read Then
            TXTNOM.Text = LECG(0)
            TXTCA.Text = LECG(1)
            TXTTEL.Text = LECG(2)

            TXTNE.Text = LECG(3)
            TXTNI.Text = LECG(4)
            TXTENTRE.Text = LECG(5)
            TXTYENTRE.Text = LECG(6)
            TXTREF.Text = LECG(7)
            CARGAX(LCOL, CBCOL, LECG(8))
        End If
        LECG.Close()
        SQLG.Dispose()


        ULTIMOSCONSUMOS(False)

    End Sub
    Private Sub ULTIMOSCONSUMOS(ByVal AF As Boolean)
        Dim DTUC1 As New DataTable

        If AF Then
            DGV3.DataSource = LLENATABLAIF("SELECT DBO.DIAMESAÑO(D.FECHA)FECHA,SUM(D.CANTIDAD)CANTIDAD,P.NOMBRE PRODUCTO,SUM(D.TOTAL)TOTAL FROM VVENTASTIENDAS D INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE WHERE D.CLIENTE=" + TXTCLA.Text + " AND D.FECHA>=@INI AND FECHA<=@FIN GROUP BY DBO.DIAMESAÑO(D.FECHA),P.NOMBRE ORDER BY DBO.DIAMESAÑO(D.FECHA)", frmPrincipal.CadenaConexion, DTDEC.Value.Date, DTHASTAC.Value.Date.AddDays(1))
            DGV4.DataSource = LLENATABLAIF("SELECT DBO.DIAMESAÑO(D.FECHA)FECHA,SUM(D.CANTIDAD)CANTIDAD,P.NOMBRE PRODUCTO,SUM(D.TOTAL)TOTAL FROM VVENTASTIENDAS D INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE WHERE D.CLIENTE=" + TXTCLA.Text + " AND D.FECHA>=DATEADD(MM,-1,@INI) AND D.FECHA<=DATEADD(MM,-1,@FIN) GROUP BY DBO.DIAMESAÑO(D.FECHA),P.NOMBRE ORDER BY DBO.DIAMESAÑO(D.FECHA)", frmPrincipal.CadenaConexion, DTDEC.Value.Date, DTHASTAC.Value.Date.AddDays(1))

        Else
            DGV3.DataSource = LLENATABLA("SELECT DBO.DIAMESAÑO(D.FECHA)FECHA,SUM(D.CANTIDAD)CANTIDAD,P.NOMBRE PRODUCTO,SUM(D.TOTAL)TOTAL FROM VVENTASTIENDAS D INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE WHERE D.CLIENTE=" + TXTCLA.Text + " AND D.FECHA>=DBO.INICIOMES(DBO.LAFECHA()) GROUP BY DBO.DIAMESAÑO(D.FECHA),P.NOMBRE ORDER BY DBO.DIAMESAÑO(D.FECHA)", frmPrincipal.CadenaConexion)
            DGV4.DataSource = LLENATABLA("SELECT DBO.DIAMESAÑO(D.FECHA)FECHA,SUM(D.CANTIDAD)CANTIDAD,P.NOMBRE PRODUCTO,SUM(D.TOTAL)TOTAL FROM VVENTASTIENDAS D INNER JOIN PRODUCTOS P ON D.PRODUCTO=P.CLAVE WHERE D.CLIENTE=" + TXTCLA.Text + " AND D.FECHA>=DATEADD(MM,-1,DBO.INICIOMES(DBO.LAFECHA())) AND D.FECHA<=DBO.INICIOMES(DBO.LAFECHA()) GROUP BY DBO.DIAMESAÑO(D.FECHA),P.NOMBRE ORDER BY DBO.DIAMESAÑO(D.FECHA)", frmPrincipal.CadenaConexion)
        End If

        DGV3.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV4.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        DGV3.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV4.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        DGV3.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV4.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        DGV3.Columns(1).DefaultCellStyle = FORMATONUMERICO()
        DGV3.Columns(3).DefaultCellStyle = FORMATONUMERICO()
        DGV4.Columns(1).DefaultCellStyle = FORMATONUMERICO()
        DGV4.Columns(3).DefaultCellStyle = FORMATONUMERICO()
        CHECACONSUMOS()
    End Sub
    Private Sub CHECACONSUMOS()
        Dim A, B As Double
        A = 0
        B = 0
        Dim X As Integer
        For X = 0 To DGV3.Rows.Count - 1
            A += DGV3.Item(3, X).Value
        Next
        For X = 0 To DGV4.Rows.Count - 1
            B += DGV4.Item(3, X).Value
        Next

        LBLVO.Text = "Total: " + FormatNumber(A, 2).ToString
        LBLVA.Text = "Total: " + FormatNumber(B, 2).ToString
    End Sub

    Private Sub CARGACONSUMOS()

    End Sub
    Dim VC As String
    Private Sub CARGADATOS2()
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        ACTIVAR(False)
        VC = TXTTEL.Text
        VA = ""
        OBS = "Alta de Cliente"
        VC = TXTTEL.Text
        ' Dim SQLG As New SqlClient.SqlCommand("SELECT C.NOMBRE,C.TELEFONO,C.GRUPOCLIENTE,C.TIPOCLIENTE,C.DIASCREDITO,C.CREDITOBASE,C.CREDITO,C.VALOR,C.ACTIVO,C.VENDEDORA,C.ZONA,C.RUTA,C.FRECUENCIA,C.CLIENTETOP,C.CC,C.ULTIMOCONSUMO,CLAVE,C.TARJETACE FROM CLIENTESF C WHERE TELEFONO=@CLA", frmPrincipal.CONX)
        Dim SQLG As New SqlClient.SqlCommand("SELECT NOMBRE,CALLE,TELEFONO,NOEXT,NOINT,ENTRE,YENTRE,REFERENCIA,COLONIA,CLAVE FROM CLIENTES C WHERE TELEFONO=@CLA", frmPrincipal.CONX)

        SQLG.Parameters.Add("@CLA", SqlDbType.VarChar).Value = TXTTEL.Text
        Dim LECG As SqlClient.SqlDataReader
        LECG = SQLG.ExecuteReader
        If LECG.Read Then
            TXTNOM.Text = LECG(0)
            TXTCA.Text = LECG(1)
            TXTTEL.Text = LECG(2)
            TXTNE.Text = LECG(3)
            TXTNI.Text = LECG(4)
            TXTENTRE.Text = LECG(5)
            TXTYENTRE.Text = LECG(6)
            TXTREF.Text = LECG(7)
            CARGAX(LCOL, CBCOL, LECG(8))
            TXTCLA.Text = LECG(9)
            ENC = True
        End If
        LECG.Close()
        SQLG.Dispose()
        VC = TXTTEL.Text

    End Sub

    Public Sub MOSTRAR(ByVal TEL As String)
        SIGCLIENTE()
        DESDEBUSQUEDA = True
        LLENACOMBOBOX(CBCOL, LCOL, "SELECT CLAVE,NOMBRE FROM COLONIAS WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)

        CBACT.SelectedIndex = 0
        TXTTEL.Text = TEL
        VTEL = TEL
        CARGADATOS2()
        TABCLI.SelectedTab = TABCLI.TabPages(1)
        Me.ShowDialog()
    End Sub

    Private Sub TXTCLA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTCLA.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(13) Then
            CARGADATOS()
        End If
    End Sub


    Private Sub BTNCOL_Click(sender As Object, e As EventArgs) Handles BTNCOL.Click
        frmClsBusqueda.BUSCAR("SELECT C.CLAVE,C.NOMBRE,C.ACTIVO FROM COLONIAS C ", " WHERE C.NOMBRE", " ORDER BY C.NOMBRE", "Búsqueda de Colonias", "Nombre de la colonia", "Colonia(s)", 1, frmPrincipal.CadenaConexion, 1)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            CARGAX(LCOL, CBCOL, frmClsBusqueda.TREG.Rows(0).Item(0).ToString)
        End If
    End Sub

    Private Sub BTNCANCELAR_Click(sender As Object, e As EventArgs) Handles BTNCANCELAR.Click
        ACTIVAR(True)
    End Sub

    Private Sub TXTNOM_TextChanged(sender As Object, e As EventArgs) Handles TXTNOM.TextChanged
        LBLNC1.Text = TXTNOM.Text
        'LBLNC2.Text = TXTNOM.Text
        LBLNC3.Text = TXTNOM.Text
        'LBLNC4.Text = TXTNOM.Text
        'LBLNC5.Text = TXTNOM.Text
        'LBLNC6.Text = TXTNOM.Text        
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ULTIMOSCONSUMOS(True)
    End Sub
    Dim CENC, NENC As String

    Private Function VERIFICAGUARDADO() As Boolean
        If Not frmPrincipal.CHECACONX Then
            Return False
        End If
        If ENC Then
            Return True
        End If
        Dim TEL, NOM, CLA As String
        CLA = ""
        NOM = ""
        TEL = "SIN TELEFONO"
        Dim SQL As New SqlClient.SqlCommand("SELECT CLAVE,NOMBRE,TELEFONO FROM CLIENTES WHERE TELEFONO=@CLA", frmPrincipal.CONX)
        SQL.Parameters.Add("@CLA", SqlDbType.VarChar).Value = TXTTEL.Text
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        If LEC.Read Then
            TEL = LEC(2)
            NOM = LEC(1)
            CLA = LEC(0)
        End If
        LEC.Close()
        SQL.Dispose()
        If TEL = "SIN TELEFONO" Then
            Return True
        Else
            If TEL = TXTTEL.Text Then
                If TXTCLA.Text = CLA Then
                    Return True
                End If
                Dim xyz As Short
                xyz = MessageBox.Show("Se ha encontrado un registro con el telefono:" + TEL + " Clave: " + CLA + ", de: " + NOM + ". ¿Desea Generar Nuevo Registro antes de Guardar la información?", "Aviso", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If xyz = 6 Then
                    SIGCLIENTE()
                    'CARGASIGCLIENTE()
                    Return True
                End If
                If xyz = 7 Then
                    Return True
                End If
                If xyz = 2 Then
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Sub BTNGUARDAR_Click(sender As Object, e As EventArgs) Handles BTNGUARDAR.Click

        If TXTNOM.Text = "" Then
            MessageBox.Show("Falta ingresar Nombre", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If TXTTEL.Text = "" Then
            MessageBox.Show("Falta ingresar Teléfono", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If TXTTEL.Text.Length <> 10 Then
            MessageBox.Show("Favor de registrar Teléfono a 10 dígitos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim xyz As Short
        xyz = MessageBox.Show("¿Desea guardar la información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Sub
        End If
        GUARDAR()
    End Sub
    Private Sub GUARDAR()
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If

        If Not VERIFICAGUARDADO() Then
            Exit Sub
        End If
        Dim SQLGUARDAR As New SqlClient.SqlCommand("SPCLIENTE", frmPrincipal.CONX)
        SQLGUARDAR.CommandType = CommandType.StoredProcedure
        SQLGUARDAR.CommandTimeout = 60
        SQLGUARDAR.Parameters.Add("@CLA", SqlDbType.Int).Value = TXTCLA.Text
        SQLGUARDAR.Parameters.Add("@NOM", SqlDbType.VarChar).Value = TXTNOM.Text
        SQLGUARDAR.Parameters.Add("@ACT", SqlDbType.Bit)
        If CBACT.SelectedIndex = 0 Then
            SQLGUARDAR.Parameters("@ACT").Value = 0
        Else
            SQLGUARDAR.Parameters("@ACT").Value = 1
        End If
        SQLGUARDAR.Parameters.Add("@CA", SqlDbType.VarChar).Value = TXTCA.Text
        SQLGUARDAR.Parameters.Add("@TEL", SqlDbType.VarChar).Value = TXTTEL.Text
        SQLGUARDAR.Parameters.Add("@NOE", SqlDbType.VarChar).Value = TXTNE.Text
        SQLGUARDAR.Parameters.Add("@NOI", SqlDbType.VarChar).Value = TXTNI.Text
        SQLGUARDAR.Parameters.Add("@ENTRE", SqlDbType.VarChar).Value = TXTENTRE.Text
        SQLGUARDAR.Parameters.Add("@YENTRE", SqlDbType.VarChar).Value = TXTYENTRE.Text
        SQLGUARDAR.Parameters.Add("@REF", SqlDbType.VarChar).Value = TXTREF.Text
        SQLGUARDAR.Parameters.Add("@COL", SqlDbType.VarChar).Value = LCOL(CBCOL.SelectedIndex)
        SQLGUARDAR.ExecuteNonQuery()

        If DESDEBUSQUEDA Then
            ELCLIENTE = TXTCLA.Text
            Me.DialogResult = Windows.Forms.DialogResult.Yes

        Else
            MessageBox.Show("La información ha sido guardada correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ACTIVAR(True)
            LIMPIAR()
            SIGCLIENTE()

        End If
        VTEL = ""
    End Sub
    Dim SIGANT As Boolean
    Private Sub BTNANTG_Click(sender As Object, e As EventArgs) Handles BTNANTG.Click
        IND -= 1
        If IND < 0 Then
        Else
            SIGANT = True
            TXTCLA.Text = LCLA(IND)
            CARGADATOS()
        End If
        CHECAIND()
    End Sub

    Private Sub BTNSIGG_Click(sender As Object, e As EventArgs) Handles BTNSIGG.Click
        IND += 1
        If IND > LCLA.Count - 1 Then
        Else
            SIGANT = True
            TXTCLA.Text = LCLA(IND)
            CARGADATOS()
        End If
        CHECAIND()

    End Sub

    Private Sub BTNBUSCAR_Click(sender As Object, e As EventArgs) Handles BTNBUSCAR.Click
        frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,CALLE+' '+NOEXT +' ' +NOINT DOMICILIO,ACTIVO FROM CLIENTES", " WHERE NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Clientes", "Nombre del cliente", "Cliente(s)", 2, frmPrincipal.CadenaConexion, False)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTCLA.Text = frmClsBusqueda.TREG.Rows(0).Item(0).ToString
            CARGADATOS()
        End If


    End Sub

    Private Sub BTNMAPA_Click(sender As Object, e As EventArgs) Handles BTNMAPA.Click
        'Dim mapa As New frmUbicacionMapa
        'mapa.MOSTRAR(Conversions.ToInteger(Me.TXTCLA.Text), Me.TXTNOM.Text, String.Concat(New String() {Me.TXTCA.Text, " ", Me.CBCOL.Text, " ", Me.TXTNE.Text}))
        'mapa.Dispose()
    End Sub

End Class
