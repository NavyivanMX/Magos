Imports System.Data.SqlClient

Public Class frmAutorizacionCredito
    Dim nivel As Integer
    Dim LANOTA As Integer
    Public USU As String
    Public CLI As String

    Private Sub frmAutorizacionCredito_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
    End Sub
    Private Sub VALIDAR()
        If Not frmPrincipal.CHECACONX Then

        End If
        nivel = 0
        Dim SQLUSER As New SqlClient.SqlCommand("SELECT USUARIO,NIVEL FROM USUARIOS WHERE USUARIO='" + TXTUSER.Text + "' AND PASSWORD='" + TXTPWD.Text + "' AND SUCURSAL='" + frmPrincipal.SucursalBase + "'", frmPrincipal.CONX)
        Dim LECUSER As SqlClient.SqlDataReader
        LECUSER = SQLUSER.ExecuteReader
        If frmPrincipal.CONX.State = ConnectionState.Closed Or frmPrincipal.CONX.State = ConnectionState.Broken Then
            Try
                frmPrincipal.CONX.Open()
            Catch ex As Exception
                MessageBox.Show("La conexión NO esta realizada, la información no se ha procesado, intente en un momento por favor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End Try
        End If
        If LECUSER.Read() Then
            USU = LECUSER(0)
            nivel = LECUSER(1)
            LECUSER.Close()
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            LIMPIAR()
            frmPrincipal.MenuStrip1.Focus()
        Else
            MessageBox.Show("Nombre de usuario y contraseña NO encontrados favor de verificar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            LECUSER.Close()
            LIMPIAR()
        End If
        If nivel <= 5 Then
            MessageBox.Show("El usuario no es administrador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            LIMPIAR()
            Exit Sub
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            LIMPIAR()
            Me.Close()
        End If

    End Sub
    Private Sub LIMPIAR()
        TXTUSER.Text = ""
        TXTPWD.Text = ""
        TXTUSER.Focus()
    End Sub
    Public Sub mostrar(ByVal usu As String, ByVal NV As String)
        CLI = usu
        Me.Text = NV
        Me.ShowDialog()

    End Sub
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        VALIDAR()
    End Sub

    Private Sub TXTUSER_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTUSER.KeyPress, TXTPWD.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        LIMPIAR()
        Me.Close()
    End Sub
End Class