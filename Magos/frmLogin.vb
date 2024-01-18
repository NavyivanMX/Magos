Public Class frmLogin

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        Me.BackgroundImage = frmPrincipal.BackgroundImage
        BackgroundImageLayout = ImageLayout.Stretch
        Try
            If frmPrincipal.CONX.State = ConnectionState.Closed Or frmPrincipal.CONX.State = ConnectionState.Broken Then
                frmPrincipal.CONX.Open()
            End If
            TXTUSER.Text = My.Settings.usuario
            TXTPWD.Text = My.Settings.contraseña
        Catch ex As Exception

        End Try
    End Sub

    Private Sub VALIDAR()
        Dim QUER As String
        QUER = "SELECT SUCURSAL,U.NIVEL, S.NOMBREFISCAL,CIUDAD,NOMBRECOMUN,U.NOMBRE,U.PERFIL,U.ACTIVO,S.PAGOTARJETA  FROM USUARIOS U INNER JOIN SUCURSALES S ON U.SUCURSAL=S.CLAVE WHERE U.USUARIO='" + TXTUSER.Text + "' AND U.PASSWORD='" + TXTPWD.Text + "'"
        Dim SQLUSER As New SqlClient.SqlCommand(QUER, frmPrincipal.CONX)
        Dim LECUSER As SqlClient.SqlDataReader
        LECUSER = SQLUSER.ExecuteReader
        If frmPrincipal.CONX.State = ConnectionState.Closed Or frmPrincipal.CONX.State = ConnectionState.Broken Then
            Try
                frmPrincipal.CONX.Open()
            Catch ex As Exception
                MessageBox.Show("La Conexión NO esta realizada, La Información No se ha Guardado, Intente en un momento por Favor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End Try
        End If
        Dim ACT As Boolean
        If LECUSER.Read() Then
            frmPrincipal.SucursalBase = LECUSER(0)
            frmPrincipal.NivelBase = LECUSER(1)
            frmPrincipal.NombreSucursal = LECUSER(2)
            frmPrincipal.Usuario = TXTUSER.Text
            frmPrincipal.Ciudad = LECUSER(3)
            'frmPrincipal.NumCajas = LECUSER(4)
            'frmPrincipal.IVA = LECUSER(5)

            'If LECUSER(6) = 0 Then
            '    frmPrincipal.PAGOTARJETA = False
            'Else
            '    frmPrincipal.PAGOTARJETA = True
            'End If
            frmPrincipal.Empresa = 1
            frmPrincipal.NombreComun = LECUSER(4)
            frmPrincipal.NombreUsuario = LECUSER(5)
            frmPrincipal.Usuario = TXTUSER.Text
            frmPrincipal.Perfil = LECUSER(6)
            frmPrincipal.PagoTarjeta = CType(LECUSER(8), Boolean)
            ACT = CType(LECUSER(7), Boolean)
            LECUSER.Close()
            If Not ACT Then
                MessageBox.Show("Usuario desactivado, favor de comunicarse con su jefe inmediato", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Exit Sub
            End If
            Dim myProcesses() As Process
            Dim myProcess As Process
            ' Returns array containing all instances of "Notepad".
            myProcesses = Process.GetProcessesByName("OSK")
            For Each myProcess In myProcesses
                myProcess.CloseMainWindow()
            Next
            'System.Diagnostics.Process.("osk")
            My.Settings.usuario = TXTUSER.Text
            My.Settings.contraseña = ""
            My.Settings.Save()
            My.Settings.Reload()
            Me.DialogResult = Windows.Forms.DialogResult.Yes
        Else
            MessageBox.Show("Nombre de Usuario y Contraseña NO encontrados favor de Verificar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            LECUSER.Close()
        End If
    End Sub
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        VALIDAR()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Dim xyz As Short
        xyz = MessageBox.Show("Deseas realmente Salir del Sistema?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If xyz <> 6 Then
            Exit Sub
        Else
            Me.DialogResult = Windows.Forms.DialogResult.No
        End If
    End Sub

    Private Sub TXTUSER_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTUSER.KeyPress
        If e.KeyChar = Chr(13) Then
            TXTPWD.Focus()
        End If
    End Sub

    Private Sub TXTPWD_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPWD.KeyPress
        If e.KeyChar = Chr(13) Then
            If TXTUSER.Text = "" Or TXTPWD.Text = "" Then
            Else
                VALIDAR()
            End If
        End If
    End Sub

    Private Sub BTNBUSCAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNBUSCAR.Click
        System.Diagnostics.Process.Start("osk")
    End Sub
End Class
