Imports System.Text.RegularExpressions
Public Class frmCorreosClientesFacturas
    Dim LREG As New List(Of String)
    Dim LDOM As New List(Of String)
    Dim CONZ As New SqlClient.SqlConnection
    Dim CADENA As String
    Dim FORMA As String
    Private Sub frmCorreosClientesFacturas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        'Try
        '    CADENA = "Data Source=" + frmPrincipal.IP + ",1433;Network Library=DBMSSOCN;Initial Catalog=FACTURACIONELECTRONICA;User ID=DBAADMIN;Password=masterkey"
        '    CONZ.ConnectionString = CADENA
        'Catch ex As Exception

        'End Try
        'CHECACONZ()
        'ACTIVAR(True)
    End Sub
    Public Sub MOSTRAR(ByVal RFC As String)
        TXTRFC.Text = RFC
        Try
            CADENA = "Data Source=" + frmPrincipal.IPFE + ",1433;Network Library=DBMSSOCN;Initial Catalog=FEMAGOS;User ID=dbaadmin;Password=masterkey"
            CONZ.ConnectionString = CADENA
        Catch ex As Exception

        End Try
        FORMA = "GUARDAR"
        CHECACONZ()
        ACTIVAR(True)
        VERIFICADOMICILIOS()
        Me.ShowDialog()
    End Sub
    Dim LASERIE As String
    Dim ELFOLIO As Integer
    Public Sub MOSTRARYENVIAR(ByVal SERIE As String, ByVal FOLIO As Integer)
        LASERIE = SERIE
        ELFOLIO = FOLIO
        BTNBUSCAR.Enabled = False
        BTNCANCELAR.Enabled = False
        Try
            CADENA = "Data Source=" + frmPrincipal.IPFE + ",1433;Network Library=DBMSSOCN;Initial Catalog=FEMAGOS;User ID=dbaadmin;Password=masterkey"
            CONZ.ConnectionString = CADENA
        Catch ex As Exception

        End Try
        If Not CHECACONZ() Then
            Exit Sub
        End If
        Dim RFC As String
        RFC = ""
        Dim DOMCLIENTE As Integer
        DOMCLIENTE = 0
        Dim SQL As New SqlClient.SqlCommand("SELECT RFCCLIENTE,DOMICILIOCLIENTE FROM FACTURAS WHERE RFCEMISOR='" + frmPrincipal.EmisorBase + "' AND SERIE='" + SERIE + "' AND FOLIO='" + FOLIO.ToString + "'", CONZ)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        If LEC.Read Then
            RFC = LEC(0)
            DOMCLIENTE = LEC(1)
        End If
        LEC.Close()
        TXTRFC.Text = RFC
        FORMA = "ENVIAR"
        LBLFORMA.Text = "Guardar y Enviar"
        CHECACONZ()
        ACTIVAR(True)
        VERIFICADOMICILIOS()
        CARGAX(LREG, CBCALLE, DOMCLIENTE.ToString)
        CBCALLE.Enabled = False
        Me.ShowDialog()
    End Sub
    Private Sub POSICIONADOMICILIO()

    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        TXTRFC.Enabled = V
        CBCALLE.Enabled = Not V
        TXTM1.Enabled = Not V
        TXTM2.Enabled = Not V
        TXTM3.Enabled = Not V
        TXTM4.Enabled = Not V
        TXTM5.Enabled = Not V
        BTNGUARDAR.Enabled = Not V
    End Sub
    Private Sub LIMPIAR()
        ACTIVAR(True)
        TXTM1.Text = ""
        TXTM2.Text = ""
        TXTM3.Text = ""
        TXTM4.Text = ""
        TXTM5.Text = ""
        TXTRFC.Text = ""
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

    Private Sub GUARDAR()
        If Not CHECACONZ() Then
            Exit Sub
        End If
        Dim SQL As New SqlClient.SqlCommand("AMCORREOCLIENTE", CONZ)
        SQL.CommandType = CommandType.StoredProcedure
        SQL.Parameters.Add("@RFC", SqlDbType.VarChar).Value = TXTRFC.Text
        SQL.Parameters.Add("@REG", SqlDbType.Int).Value = LREG(CBCALLE.SelectedIndex)
        SQL.Parameters.Add("@C1", SqlDbType.VarChar).Value = TXTM1.Text
        SQL.Parameters.Add("@C2", SqlDbType.VarChar).Value = TXTM2.Text
        SQL.Parameters.Add("@C3", SqlDbType.VarChar).Value = TXTM3.Text
        SQL.Parameters.Add("@C4", SqlDbType.VarChar).Value = TXTM4.Text
        SQL.Parameters.Add("@C5", SqlDbType.VarChar).Value = TXTM5.Text
        SQL.CommandTimeout = 300
        SQL.ExecuteNonQuery()
        If FORMA = "ENVIAR" Then
            If Not CHECACONZ() Then
                Exit Sub
            End If
            Dim SQLEF As New SqlClient.SqlCommand("ENVIARMAILFACTURAELECTRONICA", CONZ)
            SQLEF.CommandType = CommandType.StoredProcedure
            SQLEF.CommandTimeout = 300
            SQLEF.Parameters.Add("@RFC", SqlDbType.VarChar).Value = frmPrincipal.EmisorBase
            SQLEF.Parameters.Add("@SERIE", SqlDbType.VarChar).Value = LASERIE
            SQLEF.Parameters.Add("@FOLIO", SqlDbType.Int).Value = ELFOLIO
            Try
                SQLEF.ExecuteNonQuery()
                MessageBox.Show("Email de Factura Enviado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MessageBox.Show("La Información ha sido Guardada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If
    End Sub
    Private Sub CARGADATOS()
        If Not CHECACONZ() Then
            Exit Sub
        End If
        ACTIVAR(False)
        TXTM1.Text = ""
        TXTM2.Text = ""
        TXTM3.Text = ""
        TXTM4.Text = ""
        TXTM5.Text = ""
        Dim SQL As New SqlClient.SqlCommand("SELECT CORREO1,CORREO2,CORREO3,CORREO4,CORREO5 FROM CORREOSCLIENTES WHERE RFC='" + TXTRFC.Text + "' AND REGISTRO='" + LREG(CBCALLE.SelectedIndex) + "'", CONZ)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        If LEC.Read Then
            TXTM1.Text = LEC(0)
            TXTM2.Text = LEC(1)
            TXTM3.Text = LEC(2)
            TXTM4.Text = LEC(3)
            TXTM5.Text = LEC(4)
        End If
        LEC.Close()
    End Sub
    Private Function VERIFICADOMICILIOS() As Boolean
        LLENACOMBOBOX2LISTAS(CBCALLE, LREG, LDOM, "SELECT REGISTRO,DOMICILIO=CALLE+' '+NOEXT+' '+ NOINT+' '+COLONIA+' '+LOCALIDAD+' '+ESTADO+' '+CP,CALLE FROM DOMICILIOSCLIENTES WHERE RFC='" + TXTRFC.Text + "'", CADENA)
        Try
            CBCALLE.SelectedIndex = 0
            Return True
        Catch ex As Exception
            MessageBox.Show("Este Cliente no tiene domicilios dados de alta, primero ingrese al cliente con Domiciliios", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Return False
        End Try
    End Function
    Private Sub TXTRFC_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRFC.KeyPress
        If e.KeyChar = Chr(13) Then
            If String.IsNullOrEmpty(TXTRFC.Text) Then
            Else
                If TXTRFC.Text.Length < 12 Then
                    MessageBox.Show("RFC Incompleto, faltan Caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    Exit Sub
                End If
                If TXTRFC.Text.Length = 13 Then
                    If Regex.IsMatch(Me.TXTRFC.Text, "^([A-Z,&,ñ,Ñ\s]{4})\d{6}([A-Z\w]{3})$") Then
                        VERIFICADOMICILIOS()
                    Else
                        MessageBox.Show("Teclee un RFC valido", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If
                If TXTRFC.Text.Length = 12 Then
                    If Regex.IsMatch(Me.TXTRFC.Text, "^([A-Z,&,ñ,Ñ\s]{4})\d{6}([A-Z\w]{3})$") Then
                        VERIFICADOMICILIOS()
                    Else
                        MessageBox.Show("Teclee un RFC valido", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub CBCALLE_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCALLE.SelectedIndexChanged
        CARGADATOS()
        LBLDD.Text = LDOM(CBCALLE.SelectedIndex)
    End Sub

    Private Sub BTNBUSCAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNBUSCAR.Click
        frmClsBusqueda.BUSCAR("SELECT RFC,NOMBRE,NOMBRECOMERCIAL [NOMBRE COMERCIAL] FROM CLIENTES ", " WHERE NOMBRECOMERCIAL", " ORDER BY NOMBRE", "Búsqueda de Clientes", "Nombre del Cliente", "Cliente(s)", 2, CADENA, True)
        If frmClsBusqueda.DialogResult = Windows.Forms.DialogResult.Yes Then
            TXTRFC.Text = frmClsBusqueda.TREG.Rows(0).Item(0)
            VERIFICADOMICILIOS()
        End If
    End Sub

    Private Sub BTNCANCELAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNCANCELAR.Click
        LIMPIAR()
    End Sub

    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        Dim xyz As Short
        xyz = MessageBox.Show("¿Desea Guardar la Información?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Sub
        End If
        GUARDAR()
    End Sub
End Class