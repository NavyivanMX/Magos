Imports System.Drawing.Printing
Public Class frmSeleccionarImpresora
    Dim LIMP As New List(Of String)
    Public NIMPRESORA As String
    Public IMPRESORADEFAULT As String
    Private Sub frmSeleccionarImpresora_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        If Not CARGAIMPRESORAS() Then
            NIMPRESORA = ""
            Me.DialogResult = Windows.Forms.DialogResult.No
            Me.Close()
        End If
    End Sub
    Private Function CARGAIMPRESORAS() As Boolean
        LBIMP.Items.Clear()
        LIMP.Clear()
        For i As Integer = 0 To PrinterSettings.InstalledPrinters.Count - 1
            Dim a As New PrinterSettings()
            a.PrinterName = PrinterSettings.InstalledPrinters(i).ToString()
            If a.IsValid Then
                LIMP.Add(a.PrinterName)
                LBIMP.Items.Add(a.PrinterName)
                If a.IsDefaultPrinter Then
                    IMPRESORADEFAULT = a.PrinterName
                    LBIMP.SelectedIndex = i
                End If
            End If
        Next
        If LIMP.Count <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub LBIMP_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LBIMP.DoubleClick
        If LBIMP.Items.Count <= 0 Then
        Else
            NIMPRESORA = LIMP(LBIMP.SelectedIndex)
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            Me.Close()
        End If
    End Sub

    Private Sub BTNACEPTAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNACEPTAR.Click
        If LBIMP.Items.Count <= 0 Then
        Else
            NIMPRESORA = LIMP(LBIMP.SelectedIndex)
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If LBIMP.Items.Count <= 0 Then
        Else
            NIMPRESORA = LIMP(LBIMP.SelectedIndex)
      
        End If
        Me.DialogResult = Windows.Forms.DialogResult.No
        Me.Close()
    End Sub
End Class