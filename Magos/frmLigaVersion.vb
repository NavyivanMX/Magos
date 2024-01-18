Imports System.IO
Public Class frmLigaVersion
    Dim CONT As Integer
    Dim LINK, UBI, NOM As String
    Private Sub frmLigaVersion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
    End Sub
    Public Sub MOSTRAR(ByVal LINK2 As String, ByVal UBI2 As String, ByVal NOM2 As String)
        If Not MODULOGENERAL.EsAdministrador Then
            MessageBox.Show("Favor de correr el programa en modo Administrador", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
        Else
            frmPrincipal.CA.Ejecutar("DEL C:\*.RAR")
            frmPrincipal.CA.Ejecutar("DEL C:\*.ZIP")
            frmPrincipal.CA.Ejecutar("DEL C:\NUEVAVERSION\*.exe")
            frmPrincipal.CA.Ejecutar("DEL C:\NUEVAVERSION\*.dll")
            Me.LINK = LINK2
            Me.UBI = UBI2
            Me.NOM = NOM2
            Me.LNK.Text = Me.LINK
            Me.LBLUBI.Text = ("Se Descargar automaticamente a: " & Me.UBI)
            Me.CONT = 3
            Me.Timer1.Start()
            Me.BackColor = Color.Azure
            Me.ShowDialog()
        End If
    End Sub

    Private Sub INICIARDESCARGA()
        If Not VERIFICAUBICACION(UBI) Then
            DESCARGAR(LINK, UBI, NOM)
        Else
            Try
                System.IO.File.Delete(UBI)
                DESCARGAR(LINK, UBI, NOM)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        MessageBox.Show("La Actualización ha Sido Descargada Exitosamente en: " + UBI + ", Favor de descomprimirla y usarla", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        'System.IO.File.Open(UBI, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.None)
        Shell("explorer.exe c:\NUEVAVERSION", AppWinStyle.MaximizedFocus)
        frmPrincipal.CA.DESCOMPRIMIR(UBI, "C:/NUEVAVERSION")
        frmPrincipal.CA.Ejecutar("DEL C:\NUEVAVERSION\*.RAR")
        frmPrincipal.CA.Ejecutar("DEL C:\NUEVAVERSION\*.ZIP")
        Me.Close()
    End Sub
    Public Sub DESCARGAR(ByVal LINK As String, ByVal UBI As String, ByVal NOM As String)
        Try
            'MessageBox.Show("Se Empezar� la Descarga", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            'My.Computer.Network.DownloadFile(LINK, UBI)
            My.Computer.Network.DownloadFile(LINK, UBI, "", "", True, 6000, True)
            MessageBox.Show("Actualizacion " + NOM + " Descargada Exitosamente", "Felicidades", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function VERIFICAUBICACION(ByVal SFD As String) As Boolean
        If System.IO.File.Exists(SFD) = True Then
            Return True
        End If
        Return False
    End Function
    Private Sub LNK_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LNK.LinkClicked
        'Try
        '    System.IO.File.Delete("c:\Deip.sfx.exe")
        'Catch ex As Exception
        '    ' MessageBox.Show(ex.Message)
        'End Try
        'Try
        '    My.Computer.Network.DownloadFile(LNK.Text, "c:\Deip.sfx.exe")
        '    MessageBox.Show("Actualizacion Descargada Exitosamente", "Felicidades", MessageBoxButtons.OK, MessageBoxIcon.Information)

        '    'Try
        '    '    Shell("c:\Deip.sfx.exe", AppWinStyle.Hide)
        '    'Catch ex As Exception
        '    'End Try
        '    MessageBox.Show("Actualizacion Descargada Exitosamente, Favor de ir a la Unidad C y ejecutar el Archivo Deip.sfx", "Felicidades", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        'Me.Close()
        System.Diagnostics.Process.Start(sender.text)
        Me.Close()
    End Sub

    Private Sub LNK2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LNK2.LinkClicked
        System.Diagnostics.Process.Start("http://" + frmPrincipal.IP + "/MANUALPARAACTUALIZACIONES.doc")
    End Sub

    Private Sub LNK_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LNK.MouseUp
        'Me.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CONT = CONT - 1
        GB.Visible = True
        If CONT <= 0 Then
            CONT = 3
            Timer1.Enabled = False
            INICIARDESCARGA()
        End If
    End Sub
End Class