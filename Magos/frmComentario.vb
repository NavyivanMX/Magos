Public Class frmComentario
    Dim COM As String
    Dim LDOM As New List(Of String)
    Public DOM As String
    Public AH As Double
    Public HE As DateTime

    Dim VCLACLI, VIDDOM As Integer
    Dim VOYR As Boolean
    Private Sub frmComentario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
    End Sub
    Public Sub MOSTRAR(ByVal VCOM As String, ByVal NOMVEN As String, ByVal CLACLI As Integer, ByVal FORMA As Integer, ByVal IDDOM As Integer, ByVal OYR As Boolean)
        CHK1.Checked = False
        DTHORA.Value = Now
        COM = VCOM
        TXTCOM.Text = COM
        VCLACLI = CLACLI
        VIDDOM = IDDOM
        VOYR = OYR
        Me.Text = NOMVEN
        If FORMA = 1 Then
            Me.BackColor = Color.Orange
            CBCF.Visible = True
            CBSF.Visible = True
        Else
            Me.BackColor = Color.Yellow
            CBCF.Visible = False
            CBSF.Visible = False
        End If
        If String.IsNullOrEmpty(COM) Then
            TXTCOM.Clear()
            TXTCOM.Focus()
            Me.Text = "Comentario Servicio Domicilio"
        End If
        Label1.Text = Me.Text
        LLENACOMBOBOX2(CBDOM, LDOM, "SELECT REGISTRO=0, D.CALLE+' '+D.NOEXT+' '+ C.NOMBRE FROM CLIENTES D INNER JOIN COLONIAS C ON D.COLONIA=C.CLAVE WHERE D.CLAVE='" + CLACLI.ToString + "' ORDER BY D.CALLE", frmPrincipal.CadenaConexion, "Favor de Seleccionar", "NA")
        If LDOM.Count = 2 Then
            CBDOM.SelectedIndex = 1
            CBDOM.Enabled = False
            If IDDOM <> -1 Then
                Try
                    CBDOM.SelectedIndex = 1
                    CARGAX(LDOM, CBDOM, IDDOM)
                Catch ex As Exception

                End Try
            Else
                If OYR Then
                    Try
                        CBDOM.SelectedIndex = 1
                        CARGAX(LDOM, CBDOM, 1)
                    Catch ex As Exception

                    End Try
                End If

            End If

        Else
            CBDOM.Enabled = True
        End If
        Me.ShowDialog()
    End Sub
    Private Sub BTNYES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNYES.Click
        Try
            If LDOM(CBDOM.SelectedIndex) = "NA" Then
                MessageBox.Show("Debe seleccionar un Domicilio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Catch ex As Exception

        End Try
        DOM = LDOM(CBDOM.SelectedIndex)
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        TXTCOM.Focus()
        AH = CHK1.Checked
        HE = DTHORA.Value
        Me.Close()
    End Sub

    Private Sub BTNNO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNNO.Click
        Try
            If LDOM(CBDOM.SelectedIndex) = "NA" Then
                MessageBox.Show("Debe seleccionar un Domicilio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Catch ex As Exception

        End Try
        Me.DialogResult = Windows.Forms.DialogResult.No
        TXTCOM.Focus()
        Me.Close()
    End Sub

    Private Sub CBDOM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBDOM.SelectedIndexChanged
        DOM = CBDOM.Text
    End Sub

    Private Sub CBSF_Click(sender As Object, e As EventArgs) Handles CBSF.Click
        TXTCOM.Text += "**** SIN FACTURA ****"
    End Sub

    Private Sub CBCF_Click(sender As Object, e As EventArgs) Handles CBCF.Click
        TXTCOM.Text += "**** CON FACTURA ****"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LLENACOMBOBOX2(CBDOM, LDOM, "SELECT D.REGISTRO, D.CALLE+' '+D.NOEXT+' '+ C.NOMBRE FROM DOMICILIOSCLIENTESPRO D INNER JOIN COLONIASSMI C ON D.COLONIA=C.CLAVE WHERE D.CLAVE='" + VCLACLI.ToString + "' ORDER BY D.CALLE", frmPrincipal.CadenaConexion, "Favor de Seleccionar", "NA")
        If LDOM.Count = 2 Then
            CBDOM.SelectedIndex = 1
            CBDOM.Enabled = False
            If VIDDOM <> -1 Then
                Try
                    CBDOM.SelectedIndex = 1
                    CARGAX(LDOM, CBDOM, VIDDOM)
                Catch ex As Exception

                End Try
            Else
                If VOYR Then
                    Try
                        CBDOM.SelectedIndex = 1
                        CARGAX(LDOM, CBDOM, 1)
                    Catch ex As Exception

                    End Try
                End If

            End If

        Else
            CBDOM.Enabled = True
        End If
    End Sub
End Class