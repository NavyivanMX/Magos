Public Class frmReporteNotaCancelada
    Dim LSUC As New List(Of String)
    Private Sub frmReporteNotaCancelada_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        LLENACLB(CLBEMP, LSUC, "SELECT CLAVE,NOMBRECOMUN FROM SUCURSALES WHERE CLAVE<>'PRUEBAS' AND CLAVE<>'SBMLM1' AND ACTIVO=1 ORDER BY NOMBRECOMUN", frmPrincipal.CadenaConexion)
    End Sub
    Private Function CHECAFECHAS() As Boolean
        If DTDE.Value.Date > DTHASTA.Value.Date Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Sub CARGADATOS()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        If Not CHECAFECHAS() Then
            MessageBox.Show("El rango de fechas esta incorrecto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim Q1, Q2 As String
        Q1 = "SELECT T.NOMBRECOMUN SUCURSAL,D.NOTA TICKET,C.NOMBRE CLIENTE,FACTURA=DBO.FOLIOFACCCF(D.SUCURSAL,D.NOTA,0),D.TOTAL,U.NOMBRE USUARIO,D.OBSERVACION,D.FECHA,TIPOVENTA='CONTADO',E.NOMBRE CAJERA FROM NOTASCANCELADAS D INNER JOIN SUCURSALES T ON D.SUCURSAL=T.CLAVE INNER JOIN EMPLEADOSSUCURSALES E ON D.SUCURSAL=E.SUCURSAL AND D.CAJERA=E.CLAVE INNER JOIN CLIENTES C ON D.CLIENTE=C.CLAVE INNER JOIN USUARIOS U ON D.USUARIO=U.USUARIO WHERE FECHA>=@INI AND FECHA<=@FIN "
        Q2 = ""
        ' Q2 = " UNION ALL SELECT T.NOMBRECOMUN TIENDA,D.NOTA TICKET,C.NOMBRE CLIENTE,FACTURA=DBO.FOLIOFACCC(D.TIENDA,D.NOTA,0),D.TOTAL,U.NOMBRE USUARIO,D.OBSERVACION,D.FECHA,TIPOVENTA='CREDITO',E.NOMBRE CAJERA FROM NOTASCANCELADASCREDITOPRO D INNER JOIN TIENDASPRO T ON D.TIENDA=T.CLAVE INNER JOIN EMPLEADOSTIENDASPRO E ON D.TIENDA=E.TIENDA AND D.CAJERA=E.CLAVE INNER JOIN CLIENTESF C ON D.CLIENTE=C.CLAVE  INNER JOIN USUARIOSPRO U ON D.USUARIO=U.USUARIO WHERE FECHA>=@INI AND FECHA<=@FIN "

        Dim SEC As Boolean
        SEC = False
        Q1 += " AND ("
        ' Q2 += " AND ("
        Dim X As Integer
        For X = 0 To CLBEMP.Items.Count - 1
            If CLBEMP.GetItemChecked(X) Then
                If SEC Then
                    Q1 += " OR D.SUCURSAL='" + LSUC(X) + "'"
                    'Q2 += " OR D.TIENDA='" + LSUC(X) + "'"
                Else
                    Q1 += " D.SUCURSAL='" + LSUC(X) + "'"
                    'Q2 += " D.TIENDA='" + LSUC(X) + "'"
                    SEC = True
                End If
            End If
        Next
        Q1 += ")"
        ' Q2 += ")"
        DGV.DataSource = LLENATABLAIF(Q1 + Q2, frmPrincipal.CadenaConexion, DTDE.Value.Date, DTHASTA.Value.Date.AddDays(1))
        DGV.Refresh()
        DGV.Columns(3).DefaultCellStyle = FORMATONUMERICO()
        'DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        'DGV.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        CHECATABLA()
    End Sub

    Private Sub BTNMOSTRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNMOSTRAR.Click
        CARGADATOS()
    End Sub

    Private Sub CHECATABLA()
        Dim X As Integer
        Dim TC As Double
        TC = 0
        For X = 0 To DGV.Rows.Count - 1
            TC += DGV.Item(4, X).Value
        Next
        LBLNT.Text = "Notas Canceladas: " + DGV.Rows.Count.ToString
        LBLTC.Text = "Monto Cancelado: " + TC.ToString
    End Sub

    Private Sub CHKESP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKESP.CheckedChanged
        CHECARCLB(CType(sender.TAG, Integer), CType(sender, CheckBox).Checked)
    End Sub
    Private Sub CHECARCLB(ByVal IND As Integer, ByVal V As Boolean)
        Dim X As Integer
        If IND = 1 Then
            For X = 0 To CLBEMP.Items.Count - 1
                CLBEMP.SetItemChecked(X, V)
            Next
        End If
    End Sub
End Class