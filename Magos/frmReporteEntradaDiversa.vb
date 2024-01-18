Public Class frmReporteEntradaDiversa
    Dim LMOV As New List(Of String)
    Dim LCAT As New List(Of String)
    Dim LALM As New List(Of String)
    Dim DT As New DataTable
    Private Sub frmReporteEntradaDiversavb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Icon = frmPrincipal.Icon
        VISUALIZACION(Me)
        If Not LLENACOMBOBOX(CBALM, LALM, "SELECT A.CLAVE, A.NOMBRE, A.NOMBRE  FROM ALMACENES A INNER JOIN EMPRESASGASTOS E ON A.EMPRESA=E.CLAVE WHERE A.ACTIVO=1 AND E.GRUPO='" + frmPrincipal.Empresa + "' ORDER BY A.NOMBRE", frmPrincipal.CadenaConexion) Then
            MessageBox.Show("Usted no cuenta con Almacenes asignados para visualizar este reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Me.Close()
        Else
            LLENACLB(CLBEMP, Me.LMOV, "SELECT CLAVE,NOMBRE FROM MOVIMIENTOSINVENTARIO WHERE ORIENTACION=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
            LLENACLB(CLBG, Me.LCAT, "SELECT CLAVE,NOMBRE FROM PROVEEDORES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
        End If

    End Sub
    Private Function VERIFICAMARCADO() As Boolean
        If CLBEMP.CheckedItems.Count <= 0 Or CLBG.CheckedItems.Count <= 0 Then
            MessageBox.Show("Debe Seleccionar al menos una opción en cada sección", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Return False
        End If
        Return True
    End Function
    Private Sub CHECATABLA()
        'Dim TOT As Double
        'TOT = 0
        'Dim X, COL As Integer
        'If RB2.Checked Then
        '    COL = 7
        'Else
        '    COL = 5
        'End If

        'For X = 0 To DGV.Rows.Count - 1
        '    TOT += DGV.Item(COL, X).Value
        'Next
        'LBLVT.Text = "Venta Total: " + FormatNumber(TOT, 2).ToString
    End Sub
    Private Sub CARGADATOS()
        If (Me.VERIFICAMARCADO AndAlso frmPrincipal.CHECACONX) Then
            Cursor.Show()
            Cursor.Current = Cursors.WaitCursor
            Dim qUERY As String = ("SELECT E.NOORDEN,M.NOMBRE CONCEPTO,AR.NOMBRE PROVEEDOR,L.CLAVE LOTE,P.NOMBRE PRODUCTO,D.CANTIDAD,U.NOMBRE UNIDAD,E.FECHA FROM ENTRADASALMACENES E INNER JOIN DETALLEENTRADASALMACENES D ON E.ALMACEN=D.ALMACEN AND E.NOORDEN=D.NOORDEN INNER JOIN MOVIMIENTOSINVENTARIO M ON E.CONCEPTO=M.CLAVE INNER JOIN PROVEEDORES AR ON E.PROVEEDOR=AR.CLAVE INNER JOIN UNIDADES U ON D.UNIDAD=U.CLAVE INNER JOIN LOTES L ON D.LOTE=L.CLAVE INNER JOIN PRODUCTOS P ON L.PRODUCTO=P.CLAVE  WHERE D.ALMACEN ='" & Me.LALM.Item(Me.CBALM.SelectedIndex) & "' AND E.FECHA>=@INI AND E.FECHA<=@FIN")
            Dim flag As Boolean = False
            qUERY = (qUERY & " AND (")
            Dim num3 As Integer = (Me.CLBEMP.Items.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num3)
                If Me.CLBEMP.GetItemChecked(i) Then
                    If flag Then
                        qUERY = (qUERY & " OR M.CLAVE='" & Me.LMOV.Item(i) & "'")
                    Else
                        qUERY = (qUERY & " M.CLAVE='" & Me.LMOV.Item(i) & "'")
                        flag = True
                    End If
                End If
                i += 1
            Loop
            qUERY = (qUERY & ")")
            flag = False
            flag = False
            qUERY = (qUERY & " AND (")
            Dim num4 As Integer = (Me.CLBG.Items.Count - 1)
            Dim j As Integer = 0
            Do While (j <= num4)
                If Me.CLBG.GetItemChecked(j) Then
                    If flag Then
                        qUERY = (qUERY & " OR E.PROVEEDOR='" & Me.LCAT.Item(j) & "'")
                    Else
                        qUERY = (qUERY & " E.PROVEEDOR='" & Me.LCAT.Item(j) & "'")
                        flag = True
                    End If
                End If
                j += 1
            Loop
            qUERY = (qUERY & ") ORDER BY E.NOORDEN")
            DT = MODULOGENERAL.LLENATABLAIF(qUERY, frmPrincipal.CadenaConexion, Me.DTDE.Value.Date, Me.DTHASTA.Value.Date.AddDays(1))
            Me.DGV.DataSource = DT ' MODULOGENERAL.LLENATABLAIF(qUERY, frmPrincipal.CadenaConexion, Me.DTDE.Value.Date, Me.DTHASTA.Value.Date.AddDays(1))
            Me.DGV.Columns.Item(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Me.DGV.Columns.Item(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Me.DGV.Columns.Item(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Me.DGV.Columns.Item(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Me.DGV.Columns.Item(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Me.DGV.Columns.Item(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Me.DGV.Columns.Item(5).DefaultCellStyle = FORMATONUMERICOND(3)
            Me.CHECATABLA()
            Cursor.Current = Cursors.Default
        End If
    End Sub
    Private Sub CHECARCLB(ByVal IND As Integer, ByVal V As Boolean)
        Dim X As Integer
        If IND = 1 Then
            For X = 0 To CLBEMP.Items.Count - 1
                CLBEMP.SetItemChecked(X, V)
            Next
        End If
        If IND = 5 Then
            For X = 0 To CLBG.Items.Count - 1
                CLBG.SetItemChecked(X, V)
            Next
        End If
    End Sub
    Private Sub CHKESP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKESP.CheckedChanged, CheckBox4.CheckedChanged
        CHECARCLB(CType(sender.TAG, Integer), CType(sender, CheckBox).Checked)
    End Sub

    Private Sub BTNMOSTRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNMOSTRAR.Click
        CARGADATOS()
    End Sub

    Private Sub BTNIMPRIMIR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNIMPRIMIR.Click
        If DGV.Rows.Count <= 0 Then
            Exit Sub
        End If
        Dim rEP As New rptEntradaDiversa
        Dim QUERY As String
        QUERY = "SELECT A.NOMBRE ALMACEN,E.NOORDEN,C.NOMBRE CONCEPTO,AREA='',E.FECHA,US.NOMBRE USUARIO,P.NOMBRECORTO PRODUCTO,D.CANTIDAD,U.NOMBRE UNIDAD,D.TOTAL,OBSERVACION OBSERVACIONES,L.NOMBRECORTO,L.RENDIMIENTO CONTEO,L.UBICACION,PROVE.NOMBRE PROVEEDOR,L.COSTO FROM ENTRADASALMACENES E INNER JOIN ALMACENES A ON E.ALMACEN=A.CLAVE INNER JOIN DETALLEENTRADASALMACENES D ON E.ALMACEN=D.ALMACEN AND E.NOORDEN=D.NOORDEN INNER JOIN MOVIMIENTOSINVENTARIO C ON E.CONCEPTO=C.CLAVE  INNER JOIN LOTESP L ON D.LOTE=L.CLAVE INNER JOIN PRODUCTOS P ON L.PRODUCTO=P.CLAVE INNER JOIN UNIDADES U ON D.UNIDAD=U.CLAVE INNER JOIN USUARIOS US ON E.USUARIO=US.USUARIO INNER JOIN PROVEEDORES PROVE ON E.PROVEEDOR=PROVE.CLAVE WHERE E.ALMACEN='" + LALM(CBALM.SelectedIndex) + "' AND E.NOORDEN = " + DGV.Item(0, Me.DGV.CurrentRow.Index).Value.ToString
        MOSTRARREPORTE(rEP, ("Entrada Diversa " & Me.DGV.Item(0, Me.DGV.CurrentRow.Index).Value.ToString), MODULOGENERAL.LLENATABLA(QUERY, frmPrincipal.CadenaConexion), "")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If DT.Rows.Count <= 0 Then
            MessageBox.Show("Favor de primero cargar información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            ExportarExcel(DT, "Reporte Entradas Diversas " + DTDE.Value.Date.ToString("ddMMyyyy") + " hasta " + DTHASTA.Value.Date.ToString("ddMMyyyy"), True)
        End If
    End Sub
End Class