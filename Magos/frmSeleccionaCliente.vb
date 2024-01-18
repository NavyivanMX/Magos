Public Class frmSeleccionaCliente
    'Dim CLACLI As New List(Of String)
    Public CLIENTEBASE As String
    Public NOMBRECLIENTEBASE, RFCCLIENTEBASE As String
    Public DIASCREDITO As Integer
    Public TIPOCLIENTE As Integer
    Public CE As Boolean
    Private Sub frmSeleccionaCliente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        ' LLENACOMBOBOX(CBCLI, CLACLI, "SELECT CLAVE,NOMBRE FROM CLIENTES WHERE TIENDA='" + frmPrincipal.SucursalBase + "' AND ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
        TXTTEL.Text = frmPrincipal.LADA
        CE = False
        CARGADATOS()
    End Sub
    Private Sub ACEPTAR(ByVal CB As String, ByVal NCB As String, ByVal TC As Integer)
        CLIENTEBASE = CB
        NOMBRECLIENTEBASE = NCB
        TIPOCLIENTE = TC
        Me.DialogResult = Windows.Forms.DialogResult.Yes
    End Sub

    Private Sub frmSeleccionaCliente_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub BTNBUSCAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNBUSCAR.Click
        frmClsBusqueda.BUSCAR("SELECT CLAVE,NOMBRE,CALLE+' '+NOEXT+' '+NOINT  DOMICILIO,ACTIVO,TELEFONO FROM CLIENTES WHERE ACTIVO=1", " AND NOMBRE", " ORDER BY NOMBRE", "Búsqueda de Clientes", "Nombre del cliente", "Cliente(s)", 2, frmPrincipal.CadenaConexion, False)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            CLIENTEBASE = frmClsBusqueda.TREG.Rows(0).Item(0).ToString
            NOMBRECLIENTEBASE = frmClsBusqueda.TREG.Rows(0).Item(1).ToString
            RFCCLIENTEBASE = 22 'frmClsBusqueda.TREG.Rows(0).Item(3).ToString
            DIASCREDITO = 0 'FrmClsBusqueda.TREG.Rows(0).Item(4).ToString
            TIPOCLIENTE = 0 'FrmClsBusqueda.TREG.Rows(0).Item(5).ToString
            ACEPTAR(CLIENTEBASE, NOMBRECLIENTEBASE, TIPOCLIENTE)
        End If
    End Sub

    Private Sub BTNACEPTAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNACEPTAR.Click
        If TXTC.Text = "" Then
            MessageBox.Show("Introduzca una clave de cliente", "aviso", MessageBoxButtons.OK)
            TXTC.Focus()
            Exit Sub
        End If
        If LBLC.Text = "." Then
            MessageBox.Show("Introduzca una clave de cliente", "aviso", MessageBoxButtons.OK)
            TXTC.Focus()
            Exit Sub
        End If
        ACEPTAR(TXTC.Text, LBLC.Text, TIPOCLIENTE)
    End Sub

    Private Sub TXTEFECTIVO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTC.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        If Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(13) Then
            CARGADATOS()
            BTNACEPTAR.Focus()
        End If
    End Sub
    Private Sub CARGADATOS()
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If

        Dim SQLSELECT As New SqlClient.SqlCommand("SELECT CLAVE,NOMBRE,DIASCREDITO,GRUPOCLIENTE=1 FROM " + frmPrincipal.TABLACLIENTES + " WHERE CLAVE='" + TXTC.Text + "'", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLSELECT.ExecuteReader
        If LECTOR.Read Then
            LBLC.Text = LECTOR(1).ToString
            DIASCREDITO = LECTOR(2)
            TIPOCLIENTE = LECTOR(3)
        End If
        LECTOR.Close()
    End Sub
    Private Function CARGADATOSNT() As Boolean
        If Not frmPrincipal.CHECACONX() Then
            Exit Function
        End If
        Dim ENC As Boolean
        ENC = False
        Dim SQLSELECT As New SqlClient.SqlCommand("SELECT CLAVE,NOMBRE,DIASCREDITO,GRUPOCLIENTE=1 FROM " + frmPrincipal.TABLACLIENTES + " WHERE TARJETACE='" + TXTNT.Text + "'", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQLSELECT.ExecuteReader
        If LECTOR.Read Then
            TXTC.Text = LECTOR(0)
            LBLC.Text = LECTOR(1).ToString
            DIASCREDITO = LECTOR(2)
            TIPOCLIENTE = LECTOR(3)
            ENC = True
        End If
        LECTOR.Close()
        Return ENC
    End Function
    Private Sub TXTTEL_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTEL.KeyPress
        If e.KeyChar = Chr(13) Then
            Me.TXTTEL.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            CARGACLIENTE()
            'Dim CLAVE As String
            'CLAVE = MaskFormat.ExcludePromptAndLiterals
            'MessageBox.Show(CLAVE, "", MessageBoxButtons.OK)
            'TXTTEL.TextMaskFormat.ExcludePromptAndLiterals()
            'TXTTEL.TextMaskFormat.ExcludePromptAndLiterals()
        End If
    End Sub
    Private Sub CARGACLIENTE()
        Me.TXTTEL.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        If Not TXTTEL.MaskCompleted Then
            MessageBox.Show("# de Teléfono No Válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        If String.IsNullOrEmpty(TXTTEL.Text) Then
            MessageBox.Show("# de Teléfono No Válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        Dim VCLI As New frmClientesF
        VCLI.MOSTRAR(TXTTEL.Text)
        If VCLI.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTC.Text = VCLI.ELCLIENTE
            CARGADATOS()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim VCLI As New frmClientesF
        VCLI.ShowDialog()
    End Sub

    'Private Sub BTNCE_Click(sender As Object, e As EventArgs) Handles BTNCE.Click
    '    Dim VSCE As New frmClienteEspecial
    '    VSCE.ShowDialog()
    '    If VSCE.DialogResult = Windows.Forms.DialogResult.Yes Then
    '        CE = True
    '        TXTC.Text = VSCE.ELCLIENTE
    '        CARGADATOS()
    '        ACEPTAR(TXTC.Text, LBLC.Text, TIPOCLIENTE)
    '    End If
    'End Sub

    Private Sub TXTTEL_KeyDown(sender As Object, e As KeyEventArgs) Handles TXTTEL.KeyDown

    End Sub

    Private Sub TXTNT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTNT.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        If Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(13) Then
            If CARGADATOSNT() Then
                ACEPTAR(TXTC.Text, LBLC.Text, TIPOCLIENTE)
            End If            
        End If
    End Sub
End Class