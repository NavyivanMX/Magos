Imports System.Text

Public Class frmHCMP

    Public CMP As String
    Public NMP As String
    Public TIPO As Integer


    Dim MAB As Boolean


    Dim VAGREGADOBEBIDA As New frmAgregadoBebida

    Public CPLA, NPLA, CMPRESENTACION, PRESENTACION, IMGUBI, NGPLA As String
    Public MINA, MAXA, MINB, MAXB As Integer
    Public APLICACOMBO As Boolean
    Public PPLA, DCOMBO As Double

    Public C, N, P, U As String

    Private Sub LBLMP_Click(sender As Object, e As EventArgs) Handles LBLMP.Click

    End Sub

    Public POS As Integer
    Public TIPOAB As String
    Public OS As Boolean

    Private Sub frmHCMP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        INICIALIZARVARIABLES()

        'VAGREGADOBEBIDA.TopLevel = False
        'VAGREGADOBEBIDA.Location = New Point(0, 0)
        'VAGREGADOBEBIDA.WindowState = FormWindowState.Maximized
        'VAGREGADOBEBIDA.Visible = True
        'MAB = True
        'OS = False
    End Sub

    Public Sub INICIALIZARVARIABLES()
        CPLA = ""
        NPLA = ""
        CMPRESENTACION = ""
        PRESENTACION = ""
        MINA = 0
        MAXA = 0
        MINB = 0
        MAXB = 0
        APLICACOMBO = False
        'CMP = 0
        NMP = ""
        TIPO = 0
        IMGUBI = ""
        NGPLA = ""

        PPLA = 0
        DCOMBO = 0


        C = ""
        N = ""
        P = ""
        U = ""
        POS = 0
        TIPOAB = ""
        OS = False
        ' LBLMP.Text = "Menú Principal"

    End Sub

    Public Sub CARGAAGREGADOBEBIDA(ByVal OPC As String)
        'OS = False
        'MAB = True
        VAGREGADOBEBIDA = New frmAgregadoBebida
        VAGREGADOBEBIDA.TopLevel = False
        VAGREGADOBEBIDA.Location = New Point(0, 0)
        VAGREGADOBEBIDA.WindowState = FormWindowState.Maximized
        VAGREGADOBEBIDA.Visible = True
        MAB = True
        OS = False
        If OPC = "1" Then
            LBLMP.Text = "Selecciona el agregado"
        Else
            LBLMP.Text = "Selecciona la bebida"
        End If
        VAGREGADOBEBIDA.CARGAMENUPRINCIPAL(OPC)
        PANELP.Controls.Clear()
        PANELP.Controls.Add(VAGREGADOBEBIDA)
        Me.TopMost = True
        Me.ShowDialog()
    End Sub


    Private Sub BTNVOLVER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNVOLVER.Click

        BTNANT.Visible = True
        BTNSIG.Visible = True
        If MAB Then
            Me.Close()
        End If

    End Sub

    Private Sub BTNSIG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNSIG.Click
        If MAB Then
            VAGREGADOBEBIDA.IMP += 1
            VAGREGADOBEBIDA.ACTUALIZAMENUPRINCIPAL()
        End If
    End Sub

    Private Sub BTNANT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNANT.Click
        If MAB Then
            VAGREGADOBEBIDA.IMP -= 1
            VAGREGADOBEBIDA.ACTUALIZAMENUPRINCIPAL()
        End If
    End Sub


    Private Sub PictureBox3_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

End Class