Public Class frmEntrarCaja
    Public CAJERA As Integer
    Public NOMBRE As String
    Public NOCAJA As Integer
    Private Sub frmEntrarCaja_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        CARGACAJAS()
    End Sub
    Private Sub CARGACAJAS()
        Dim NCAJ, X As Integer
        If Not frmPrincipal.CHECACONX() Then
            Exit Sub
        End If
        Dim SQL As New SqlClient.SqlCommand("SELECT CAJAS FROM SUCURSALES WHERE CLAVE='" + frmPrincipal.SucursalBase + "'", frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQL.ExecuteReader
        If LECTOR.Read Then
            NCAJ = LECTOR(0)
        Else
            Me.Close()
        End If
        LECTOR.Close()
        CBCAJA.Items.Clear()
        For X = 1 To NCAJ
            CBCAJA.Items.Add(X)
        Next
        CBCAJA.SelectedIndex = 0
    End Sub
    Private Sub CHECACAJERA()
        If frmPrincipal.CHECACONX() Then
        Else
            Exit Sub
        End If
        If TXTEMP.Text = "" Then
            Exit Sub
        End If
        Dim PWD As String
        PWD = ""
        Dim SQL As New SqlClient.SqlCommand("SELECT C.PWD,E.NOMBRE FROM CAJERAS C INNER JOIN EMPLEADOSSUCURSALES E ON C.SUCURSAL=E.SUCURSAL AND C.EMPLEADO=E.CLAVE WHERE C.SUCURSAL='" + frmPrincipal.SucursalBase + "' AND C.EMPLEADO=" + TXTEMP.Text, frmPrincipal.CONX)
        Dim LECTOR As SqlClient.SqlDataReader
        LECTOR = SQL.ExecuteReader
        If LECTOR.Read Then
            PWD = LECTOR(0)
            CAJERA = CType(TXTEMP.Text, Integer)
            NOMBRE = LECTOR(1)
            LECTOR.Close()
        Else
            LECTOR.Close()
            Exit Sub
        End If

        NOCAJA = CBCAJA.Text
        If PWD = TXTPWD.Text Then
            Me.DialogResult = Windows.Forms.DialogResult.Yes
        Else
            MessageBox.Show("Datos incorrectos, favor de verificar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If
    End Sub
    Private Sub BTNACEPTAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNACEPTAR.Click
        CHECACAJERA()
    End Sub

    Private Sub TXTEMP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTEMP.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(13) Then
            TXTPWD.Focus()
            TXTPWD.SelectAll()
        End If
    End Sub

    Private Sub TXTPWD_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPWD.KeyPress
        If e.KeyChar = Chr(13) Then
            If TXTPWD.Text = "" Then
            Else
                CHECACAJERA()
            End If
        End If
    End Sub
End Class