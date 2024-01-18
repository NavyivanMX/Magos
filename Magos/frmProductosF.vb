Imports System.Data.SqlClient

Public Class frmProductosF
    Dim LFAM As New List(Of String)
    Dim LDEP As New List(Of String)
    Dim LESP As New List(Of String)
    Dim LCAL As New List(Of String)
    Dim LMED As New List(Of String)
    Dim LTPRO As New List(Of String)
    Dim LGCC As New List(Of String)
    Dim LGIEPS As New List(Of String)
    Dim LSIM As New List(Of String)
    Dim LGINV As New List(Of String)
    Dim LGO As New List(Of String)
    Dim LGV As New List(Of String)
    Dim LGT As New List(Of String)
    Dim LUV As New List(Of String)
    Dim LUGEN As New List(Of String)
    Dim LUI As New List(Of String)
    Dim LUCC As New List(Of String)
    Dim LUC As New List(Of String)
    Dim LCED As New List(Of String)
    Dim LAS As New List(Of String)
    Dim LMP As New List(Of String)
    Dim LBT As New List(Of String)
    Dim LGP As New List(Of String)
    Dim LPRE As New List(Of String)
    Dim LCLA As New List(Of String)
    Dim LNOM As New List(Of String)
    Dim LTI As New List(Of String)
    Dim LALM1 As New List(Of String)
    Dim LALM2 As New List(Of String)
    Dim LALM3 As New List(Of String)
    Dim LTA As New List(Of String)
    Dim LGTP As New List(Of String)
    Dim IND As Integer

    Dim LFIVA As New List(Of String)
    Dim LFIEPS As New List(Of String)
    Dim LPYSSAT As New List(Of String)
    Private Sub frmProductosF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim engine As New SkinEngine
        'MyProject.Forms.frmPrincipal.SENGINE.AddForm(Me)
        'Me.Refresh()
        Me.Icon = frmPrincipal.Icon
        LLENA2LISTAS(LCLA, LNOM, "SELECT CLAVE,NOMBRE FROM PRODUCTOS ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
        IND = 0
        CHECAIND()
        LLENACLB(CLBTI, LTI, "SELECT CLAVE,NOMBRECOMUN FROM SUCURSALES WHERE ACTIVO=1 ORDER BY NOMBRECOMUN", frmPrincipal.CadenaConexion)
        LLENACLB(CLBALM1, LALM1, "SELECT CLAVE,NOMBRE FROM ALMACENES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
        LLENACLB(CLBALM2, LALM2, "SELECT CLAVE,NOMBRE FROM ALMACENES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
        LLENACLB(CLBALM3, LALM3, "SELECT CLAVE,NOMBRE FROM ALMACENES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
        LLENACOMBOBOX(CBTA, LTA, "SELECT CLAVE,NOMBRE FROM TIPOSAGREGADOSCOMBOS WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion)
        LLENACOMBOBOX2(CBFAM, LFAM, "SELECT CLAVE,NOMBRE FROM FAMILIAS  ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
        LLENACOMBOBOX2(CBCC, LGCC, "SELECT CLAVE,NOMBRE FROM CCPRODUCTOS WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
        'LLENACOMBOBOX2(CBGO, LGO, "SELECT CLAVE,NOMBRE FROM BARRAPRODUCTO WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
        LLENACOMBOBOX2(CBGV, LGV, "SELECT CLAVE,NOMBRE FROM GRUPOVENTAS WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")

        LLENACOMBOBOX2(CBSI, LSIM, "SELECT CLAVE,NOMBRE FROM SECCIONESIMPRESION WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
        LLENACOMBOBOX2(CBGT, LGT, "SELECT CLAVE,NOMBRE FROM GRUPOTOUCH ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
        LLENACOMBOBOX2(CBUCIC, LUCC, "SELECT CLAVE,NOMBRE FROM UNIDADES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
        LLENACOMBOBOX2(CBUCOM, LUC, "SELECT CLAVE,NOMBRE FROM UNIDADES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
        LLENACOMBOBOX2(CBUINV, LUI, "SELECT CLAVE,NOMBRE FROM UNIDADES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
        LLENACOMBOBOX2(CBUVEN, LUV, "SELECT CLAVE,NOMBRE FROM UNIDADES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
        'LLENACOMBOBOX2(CBUGEN, LUGEN, "SELECT CLAVE,NOMBRE FROM UNIDADES WHERE ACTIVO=1 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")

        LLENACOMBOBOX2(CBGINV, LGINV, "SELECT CLAVE,NOMBRE FROM GRUPOSINV ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")

        LimpiarForm(Controls)

        LLENACOMBOBOX2(CBFIVA, LFIVA, "SELECT CLAVE,NOMBRE FROM CSATTIPOFACTOR WHERE CLAVE<>2 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")
        LLENACOMBOBOX2(CBFIEPS, LFIEPS, "SELECT CLAVE,NOMBRE FROM CSATTIPOFACTOR ORDER BY NOMBRE ", frmPrincipal.CadenaConexion, "Favor de seleccionar", "")

        ACTIVAR(True)
        LIMPIAR()
    End Sub
    Private Sub ACTIVAR(ByVal V As Boolean)
        Me.TXTCLA.Enabled = V
        Me.TXTCP.Enabled = Not V
        Me.TXTCON.Enabled = Not V
        Me.TXTNOM.Enabled = Not V
        Me.TXTDES.Enabled = Not V
        Me.TXTNC.Enabled = Not V
        Me.TXTNI.Enabled = Not V
        Me.CBFAM.Enabled = Not V

        Me.CBCC.Enabled = Not V

        'CBUGEN.Enabled = Not V
        CHKAS.Enabled = Not V
        CHKAC.Enabled = Not V
        CHKAP.Enabled = Not V
        Me.CHKAR.Enabled = Not V
        'Me.CHKEP.Enabled = Not V

        Me.CLBTI.Enabled = Not V
        Me.CLBALM3.Enabled = Not V
        Me.BTNMOSTRAR.Enabled = Not V
        Me.CLBALM1.Enabled = Not V
        Me.CLBALM2.Enabled = Not V

        Me.CBACT.Enabled = Not V
        Me.TXTCV.Enabled = Not V
        Me.CBPESO.Enabled = Not V

        Me.CBSI.Enabled = Not V
        Me.CBSI.Enabled = Not V
        'Me.CBGO.Enabled = Not V
        Me.CBGV.Enabled = Not V
        Me.CBGT.Enabled = Not V

        Me.CBGINV.Enabled = Not V
        Me.CBUINV.Enabled = Not V
        Me.CBUCIC.Enabled = Not V
        Me.CBUCOM.Enabled = Not V

        Me.CHKAR.Enabled = Not V
        'Me.CHKEP.Enabled = Not V
        Me.CHKCG.Enabled = Not V

        Me.NUDMAXA.Enabled = Not V
        Me.NUDMINA.Enabled = Not V
        Me.NUDMINB.Enabled = Not V
        Me.NUDMAXB.Enabled = Not V
        Me.CBCMB.Enabled = Not V
        Me.CBTA.Enabled = Not V
        Me.BTNAGRE.Enabled = Not V
        Me.BTNQUITAR.Enabled = Not V
        Me.BTNBUSPRO.Enabled = Not V
        Me.TXTCLAPRO.Enabled = Not V
        Me.TXTCANT.Enabled = Not V
        Me.CHKINC.Enabled = Not V
        Me.TXTDES.Enabled = Not V
        Me.DGV.Enabled = Not V
        Me.DGV3.Enabled = Not V
        TXTDESC.Enabled = Not V
        'Me.DGV3.Enabled = Not V
        Me.TXTCP.Enabled = Not V
        Me.CBUVEN.Enabled = Not V
        Me.BTNGUARDAR.Enabled = Not V
        CBFIEPS.Enabled = Not V
        CBFIVA.Enabled = Not V
        TXTCPYSSAT.Enabled = Not V
        TXTCOSTO.Enabled = Not V
        TXTPRE.Enabled = Not V

        TXTCLAEXTRA.Enabled = Not V
        BTNBUSEXTRA.Enabled = Not V
        BTNAGREGAREXTRA.Enabled = Not V
        BTNQUITAREXTRA.Enabled = Not V

    End Sub

    Private Sub CARGADATOS()
        If frmPrincipal.CHECACONX Then
            Me.ACTIVAR(False)
            Dim command As New SqlCommand("SELECT CP,CONSECUTIVO,NOMBRE,NOMBRECORTO,DESCRIPCION,IMAGEN,FAMILIA,GRUPOCC,TASAIEPS,ACTIVO,UNIDADVTA,SECCIONIMPRESION,GRUPOTOUCHVENTA,GRUPOVENTA,UNIDADINV,UNIDADCICLO,UNIDADCOMPRA,DESCUENTO,APLICACOMBO, MINA, MAXA, MINB, MAXB, FACTORIVA, FACTORIEPS, TASAIVA,TASAIEPS, CPYSSAT, COSTO,GRUPOINVENTARIO FROM PRODUCTOS WHERE CLAVE=@CLA", frmPrincipal.CONX)
            command.Parameters.Add("@CLA", SqlDbType.Int).Value = Me.TXTCLA.Text
            Dim reader As SqlDataReader = command.ExecuteReader
            If reader.Read Then
                TXTCP.Text = reader.Item(0).ToString
                TXTCON.Text = reader.Item(1).ToString
                TXTNOM.Text = reader.Item(2).ToString
                TXTDES.Text = reader.Item(3).ToString
                TXTNC.Text = reader.Item(4).ToString
                TXTNI.Text = reader.Item(5).ToString

                MODULOGENERAL.CARGAX(Me.LFAM, CBFAM, reader.Item(6).ToString)

                MODULOGENERAL.CARGAX(LGCC, CBCC, reader.Item(7).ToString)


                If CType(reader.Item(9), Boolean) Then
                    Me.CBACT.SelectedIndex = 1
                Else
                    Me.CBACT.SelectedIndex = 0
                End If
                'Me.TXTCV.Text = reader.Item(18).ToString
                'If CType(reader.Item(19), Boolean) Then
                '    Me.CBPESO.SelectedIndex = 1
                'Else
                '    Me.CBPESO.SelectedIndex = 0
                'End If
                CARGAX(LUV, CBUVEN, reader.Item(10).ToString)
                CARGAX(LSIM, CBSI, reader.Item(11).ToString)
                ' CARGAX(LGO, CBGO, reader.Item(13).ToString)
                CARGAX(LGV, CBGV, reader.Item(13).ToString)
                CARGAX(LGT, CBGT, reader.Item(12).ToString)

                ' CARGAX(LGINV, CBGINV, reader.Item(26).ToString)
                CARGAX(LUI, CBUINV, reader.Item(14).ToString)
                CARGAX(LUCC, CBUCIC, reader.Item(15).ToString)
                CARGAX(LUC, CBUCOM, reader.Item(16).ToString)


                'If CType(reader.Item(31), Boolean) Then
                '    Me.CHKAR.Checked = True
                'Else
                '    Me.CHKAR.Checked = False
                'End If
                'If CType(reader.Item(32), Boolean) Then
                '    Me.CHKEP.Checked = True
                'Else
                '    Me.CHKEP.Checked = False
                'End If
                'If CType(reader.Item(33), Boolean) Then
                '    Me.CHKCG.Checked = True
                'Else
                '    Me.CHKCG.Checked = False
                'End If

                Me.TXTDESC.Text = reader.Item(17).ToString
                If CType(reader.Item(18), Boolean) Then
                    Me.CBCMB.SelectedIndex = 1
                Else
                    Me.CBCMB.SelectedIndex = 0
                End If
                Me.NUDMINA.Value = CType(reader.Item(19), Integer)
                Me.NUDMAXA.Value = CType(reader.Item(20), Integer)
                Me.NUDMINB.Value = CType(reader.Item(21), Integer)
                Me.NUDMAXB.Value = CType(reader.Item(22), Integer)
                'If CType(reader.Item(45), Boolean) Then
                '    Me.CBCAN.SelectedIndex = 1
                'Else
                '    Me.CBCAN.SelectedIndex = 0
                'End If
                ''Me.TXTPC.Text = Conversions.ToString(Conversions.ToInteger(reader.Item(&H2E)))
                CARGAX(LFIVA, CBFIVA, reader(23))
                CARGAX(LFIEPS, CBFIEPS, reader(24))
                TXTTIVA.Text = reader(25)
                TXTTCIEPS.Text = reader.Item(26).ToString
                TXTCPYSSAT.Text = reader.Item(27)

                'CARGAX(LUGEN, CBUGEN, reader.Item(52).ToString)

                TXTCOSTO.Text = reader.Item(28).ToString

                CARGAX(LGINV, CBGINV, reader(29))


                Dim VAL As Integer
                VAL = reader(20)

                NUDMINA.Value = reader(19)
                NUDMAXA.Value = reader(20)
                NUDMINB.Value = reader(21)
                NUDMAXB.Value = reader(22)
                NUDMINA.Value += 1
                NUDMINA.Value -= 1

                NUDMAXA.Value += 1
                NUDMAXA.Value -= 1

                NUDMINB.Value += 1
                NUDMINB.Value -= 1

                NUDMAXB.Value += 1
                NUDMAXB.Value -= 1

                NUDMINA.Refresh()
                NUDMAXA.Refresh()
                NUDMINB.Refresh()
                NUDMAXB.Refresh()
            End If
            reader.Close()
            command.Dispose()
            Me.MARCATIENDAS()
            Me.MARCAALMACENES()
            Me.CARGADESCARGA()
            Me.CARGAPRECIOS()
            Me.CARGAMENUAT()
            CARGAAPLICAEXTRAS()
        End If
    End Sub

    Private Sub CARGADESCARGA()
        Try
            Me.DGV3.DataSource = MODULOGENERAL.LLENATABLA("SELECT T.NOMBRECOMUN TIENDA,CONVERT(BIT,0)DESCARGA,CANTIDAD=1.000,T.CLAVE FROM SUCURSALES T WHERE T.ACTIVO=1", frmPrincipal.CadenaConexion)
            Dim dataGridViewColumn As New DataGridViewComboBoxColumn With { _
                .ValueType = GetType(Integer), _
                .Name = "Almacen", _
                .HeaderText = "Almacen", _
                .DataSource = MODULOGENERAL.LLENATABLA("SELECT cast(CLAVE AS INT) AS CLAVE,NOMBRE FROM ALMACENES WHERE ACTIVO=1 AND EMPRESA=2", frmPrincipal.CadenaConexion), _
                .DataPropertyName = "CLAVE", _
                .ValueMember = "CLAVE", _
                .DisplayMember = "NOMBRE" _
            }
            Me.DGV3.Columns.Add(dataGridViewColumn)
            Me.DGV3.Columns.Item(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Catch exception1 As Exception

        End Try
    End Sub

    Private Sub CARGAMENUAT()
        If frmPrincipal.CHECACONX Then
            Dim qUERY As String = ("SELECT P.NOMBRE PRODUCTO,D.CANTIDAD,D.TOTAL,T.NOMBRE TIPO,D.INCLUIDO,D.ACTIVO,D.PRODUCTO FROM CONFGPAQUETESPRODUCTOS D INNER JOIN PRODUCTOS P ON D.PRODUCTOOPC=P.CLAVE INNER JOIN TIPOSAGREGADOSCOMBOS T ON D.TIPO=T.CLAVE WHERE PRODUCTO='" & Me.TXTCLA.Text & "'")
            Me.DGV3.DataSource = MODULOGENERAL.LLENATABLA(qUERY, frmPrincipal.CadenaConexion)
            Me.DGV3.Columns.Item(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Me.DGV3.Columns.Item(1).DefaultCellStyle = MODULOGENERAL.FORMATONUMERICO
            Me.DGV3.Columns.Item(2).DefaultCellStyle = MODULOGENERAL.FORMATONUMERICO
            Me.DGV3.Columns.Item(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Me.DGV3.Columns.Item(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Me.DGV3.Columns.Item(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Me.DGV3.Columns.Item(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Me.DGV3.Columns.Item(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Me.DGV3.Columns.Item(6).Visible = False
        End If
    End Sub

    Private Sub CARGAPRECIOS()
        Try
            Me.DGV.DataSource = MODULOGENERAL.LLENATABLA("SELECT T.NOMBRECOMUN TIENDA,[PRECIO]=1000.00,T.CLAVE FROM SUCURSALES T", frmPrincipal.CadenaConexion)
            Me.DGV.Columns.Item(2).Visible = False
            Me.DGV.Columns.Item(0).ReadOnly = True
            Me.DGV.Columns.Item(1).DefaultCellStyle = MODULOGENERAL.FORMATONUMERICO
            Me.DGV.Columns.Item(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Me.DGV.Columns.Item(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Dim table As New DataTable
            table = MODULOGENERAL.LLENATABLA(("SELECT SUCURSAL,PRECIO FROM PRECIOSSUCURSALES WHERE PRODUCTO='" & Me.TXTCLA.Text & "'"), frmPrincipal.CadenaConexion)
            Dim num3 As Integer = (table.Rows.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num3)
                Dim num4 As Integer = (Me.DGV.Rows.Count - 1)
                Dim j As Integer = 0
                Do While (j <= num4)
                    If (table.Rows.Item(i).Item(0).ToString = Me.DGV.Item(2, j).Value.ToString) Then
                        Me.DGV.Item(1, j).Value = table.Rows.Item(i).Item(1).ToString
                    End If
                    j += 1
                Loop
                i += 1
            Loop
        Catch exception1 As Exception
            'ProjectData.SetProjectError(exception1)
            'Dim exception As Exception = exception1
            'MessageBox.Show(exception.Message)
            'ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub CARGAPRODUCTO()
        If frmPrincipal.CHECACONX Then
            Dim command As New SqlCommand(("SELECT CLAVE,NOMBRE FROM PRODUCTOS WHERE CLAVE='" & Me.TXTCLAPRO.Text & "'"), frmPrincipal.CONX)
            Dim reader As SqlDataReader = command.ExecuteReader
            If reader.Read Then
                Me.LBLNP.Text = reader.Item(1).ToString
                LBLNEXTRA.Text = reader.Item(1).ToString
            End If
            reader.Close()
            command.Dispose()
        End If
    End Sub

    Private Sub CHECAIND()
        If (Me.IND <= 0) Then
            Me.BTNANTG.Enabled = False
        Else
            Me.BTNANTG.Enabled = True
        End If
        If (Me.IND > (Me.LCLA.Count - 1)) Then
            Me.BTNSIGG.Enabled = False
        Else
            Me.BTNSIGG.Enabled = True
        End If
    End Sub
    Private Sub CHECARCLB(ByVal IND As Integer, ByVal V As Boolean)
        Dim num As Integer
        If (IND = 1) Then
            Dim num2 As Integer = (Me.CLBALM1.Items.Count - 1)
            num = 0
            Do While (num <= num2)
                Me.CLBALM1.SetItemChecked(num, V)
                num += 1
            Loop
        End If
        If (IND = 2) Then
            Dim num3 As Integer = (Me.CLBALM2.Items.Count - 1)
            num = 0
            Do While (num <= num3)
                Me.CLBALM2.SetItemChecked(num, V)
                num += 1
            Loop
        End If
        If (IND = 3) Then
            Dim num4 As Integer = (Me.CLBALM3.Items.Count - 1)
            num = 0
            Do While (num <= num4)
                Me.CLBALM3.SetItemChecked(num, V)
                num += 1
            Loop
        End If
    End Sub


    Private Sub GUARDAR()
        If frmPrincipal.CHECACONX Then
            Dim num As Integer
            Dim str As String = Me.LFAM.Item(Me.CBFAM.SelectedIndex)

            Dim str10 As String = Me.LSIM.Item(Me.CBSI.SelectedIndex)
            'Dim str11 As String = Me.LGO.Item(Me.CBGO.SelectedIndex)
            Dim str12 As String = Me.LGV.Item(Me.CBGV.SelectedIndex)
            Dim str13 As String = Me.LGT.Item(Me.CBGT.SelectedIndex)
            Dim str14 As String = Me.LUI.Item(Me.CBUINV.SelectedIndex)
            Dim str15 As String = Me.LUCC.Item(Me.CBUCIC.SelectedIndex)
            Dim str16 As String = Me.LUC.Item(Me.CBUCOM.SelectedIndex)

            Dim command4 As New SqlCommand With { _
                .Connection = frmPrincipal.CONX, _
                .CommandType = CommandType.StoredProcedure _
            }
            command4.Parameters.Add("@CLA", SqlDbType.Int).Value = Me.TXTCLA.Text
            command4.Parameters.Add("@CP", SqlDbType.VarChar).Value = Me.TXTCP.Text
            command4.Parameters.Add("@CON", SqlDbType.VarChar).Value = Me.TXTCON.Text
            command4.Parameters.Add("@NOM", SqlDbType.VarChar).Value = Me.TXTNOM.Text
            command4.Parameters.Add("@NOMC", SqlDbType.VarChar).Value = Me.TXTNC.Text
            command4.Parameters.Add("@DES", SqlDbType.VarChar).Value = Me.TXTDES.Text
            command4.Parameters.Add("@IMG", SqlDbType.VarChar).Value = Me.TXTNI.Text
            command4.Parameters.Add("@FAM", SqlDbType.Int).Value = Me.LFAM.Item(Me.CBFAM.SelectedIndex)
    
            command4.Parameters.Add("@GCC", SqlDbType.Int).Value = Me.LGCC.Item(Me.CBCC.SelectedIndex)
            'command4.Parameters.Add("@GIEPS", SqlDbType.Int).Value = 0 ' Me.LGIEPS.Item(Me.CBIEPS.SelectedIndex)

            'command4.Parameters.Add("@IEPS", SqlDbType.Float).Value = Me.TXTTASA.Text
            command4.Parameters.Add("@DESC", SqlDbType.Float).Value = Me.TXTDESC.Text
            command4.Parameters.Add("@ACT", SqlDbType.Bit).Value = Me.CBACT.SelectedIndex
            'command4.Parameters.Add("@CVTA", SqlDbType.VarChar).Value = Me.TXTCV.Text
            'command4.Parameters.Add("@PESO", SqlDbType.Bit).Value = Me.CBPESO.SelectedIndex
            command4.Parameters.Add("@UVTA", SqlDbType.Int).Value = Me.LUV.Item(Me.CBUVEN.SelectedIndex)
            'command4.Parameters.Add("@SI", SqlDbType.Int).Value = Me.LSIM.Item(Me.CBSI.SelectedIndex)
            'command4.Parameters.Add("@GO", SqlDbType.Int).Value = 0 'Me.LGO.Item(Me.CBGO.SelectedIndex)
            command4.Parameters.Add("@GV", SqlDbType.Int).Value = Me.LGV.Item(Me.CBGV.SelectedIndex)
            command4.Parameters.Add("@GTV", SqlDbType.Int).Value = Me.LGT.Item(Me.CBGT.SelectedIndex)

            command4.Parameters.Add("@GINV", SqlDbType.VarChar).Value = Me.LGINV.Item(Me.CBGINV.SelectedIndex)
            command4.Parameters.Add("@UINV", SqlDbType.Int).Value = Me.LUI.Item(Me.CBUINV.SelectedIndex)
            command4.Parameters.Add("@UCIC", SqlDbType.Int).Value = Me.LUCC.Item(Me.CBUCIC.SelectedIndex)
            command4.Parameters.Add("@UCOM", SqlDbType.Int).Value = Me.LUC.Item(Me.CBUCOM.SelectedIndex)
            'command4.Parameters.Add("@UGEN", SqlDbType.Int).Value = 0 ' Me.LUGEN.Item(Me.CBUGEN.SelectedIndex)

            'command4.Parameters.Add("@AREC", SqlDbType.Bit).Value = Me.CHKAR.Checked
            'command4.Parameters.Add("@PREP", SqlDbType.Bit).Value = 0 ' Me.CHKEP.Checked
            'command4.Parameters.Add("@GAS", SqlDbType.Bit).Value = Me.CHKCG.Checked

            command4.Parameters.Add("@COMBO", SqlDbType.Bit).Value = Me.CBCMB.SelectedIndex
            command4.Parameters.Add("@MINA", SqlDbType.Int).Value = Me.NUDMINA.Value
            command4.Parameters.Add("@MAXA", SqlDbType.Int).Value = Me.NUDMAXA.Value
            command4.Parameters.Add("@MINB", SqlDbType.Int).Value = Me.NUDMINB.Value
            command4.Parameters.Add("@MAXB", SqlDbType.Int).Value = Me.NUDMAXB.Value

            'command4.Parameters.Add("@CAN", SqlDbType.Bit).Value = 1
            'command4.Parameters.Add("@PC", SqlDbType.Int).Value = 0

            command4.Parameters.Add("@FIVA", SqlDbType.Int).Value = LFIVA(CBFIVA.SelectedIndex)
            command4.Parameters.Add("@FIEPS", SqlDbType.Int).Value = LFIEPS(CBFIEPS.SelectedIndex)


            If TXTTIVA.Enabled Then
                command4.Parameters.Add("@TIVA", SqlDbType.Float).Value = Me.TXTTIVA.Text
            Else
                command4.Parameters.Add("@TIVA", SqlDbType.Float).Value = 0
            End If
            If TXTTCIEPS.Enabled Then
                command4.Parameters.Add("@IEPS", SqlDbType.Float).Value = Me.TXTTCIEPS.Text
            Else
                command4.Parameters.Add("@IEPS", SqlDbType.Float).Value = 0
            End If

            command4.Parameters.Add("@PYSSAT", SqlDbType.VarChar).Value = TXTCPYSSAT.Text
            command4.Parameters.Add("@COSTO", SqlDbType.Float).Value = TXTCOSTO.Text

            command4.CommandText = "SPPRODUCTOSFV2"



            command4.ExecuteNonQuery()
            'Dim command3 As New SqlCommand(("DELETE FROM PRODUCTOALMACENSOLICITUD WHERE PRODUCTO='" & Me.TXTCLA.Text & "'"), frmPrincipal.CONX)
            'command3.ExecuteNonQuery()
            'command3.CommandText = "INSERT INTO PRODUCTOALMACENSOLICITUD (ALMACEN, PRODUCTO,ACTIVO) VALUES (@ALM,@PRO,@ACT)"
            'command3.Parameters.Add("@ALM", SqlDbType.VarChar)
            'command3.Parameters.Add("@PRO", SqlDbType.Int).Value = Me.TXTCLA.Text
            'command3.Parameters.Add("@ACT", SqlDbType.Bit)
            'Dim num2 As Integer = (Me.CLBALM1.Items.Count - 1)
            'num = 0
            'Do While (num <= num2)
            '    command3.Parameters.Item("@ALM").Value = Me.LALM1.Item(num)
            '    command3.Parameters.Item("@ACT").Value = Me.CLBALM1.GetItemChecked(num)
            '    command3.ExecuteNonQuery()
            '    num += 1
            'Loop
            Dim command As New SqlCommand(("DELETE FROM PRODUCTOALMACENCOMPRA WHERE PRODUCTO='" & Me.TXTCLA.Text & "'"), frmPrincipal.CONX)
            command.ExecuteNonQuery()
            command.CommandText = "INSERT INTO PRODUCTOALMACENCOMPRA (ALMACEN, PRODUCTO,ACTIVO) VALUES (@ALM,@PRO,@ACT)"
            command.Parameters.Add("@ALM", SqlDbType.VarChar)
            command.Parameters.Add("@PRO", SqlDbType.Int).Value = Me.TXTCLA.Text
            command.Parameters.Add("@ACT", SqlDbType.Bit)
            Dim num3 As Integer = (Me.CLBALM2.Items.Count - 1)
            num = 0
            Do While (num <= num3)
                command.Parameters.Item("@ALM").Value = Me.LALM2.Item(num)
                command.Parameters.Item("@ACT").Value = Me.CLBALM2.GetItemChecked(num)
                command.ExecuteNonQuery()
                num += 1
            Loop
            'Dim command2 As New SqlCommand(("DELETE FROM PRODUCTOALMACENPRODUCE WHERE PRODUCTO='" & Me.TXTCLA.Text & "'"), frmPrincipal.CONX)
            'command2.ExecuteNonQuery()
            'command2.CommandText = "INSERT INTO PRODUCTOALMACENPRODUCE (ALMACEN, PRODUCTO,ACTIVO) VALUES (@ALM,@PRO,@ACT)"
            'command2.Parameters.Add("@ALM", SqlDbType.VarChar)
            'command2.Parameters.Add("@PRO", SqlDbType.Int).Value = Me.TXTCLA.Text
            'command2.Parameters.Add("@ACT", SqlDbType.Bit)
            'Dim num4 As Integer = (Me.CLBALM3.Items.Count - 1)
            'num = 0
            'Do While (num <= num4)
            '    command2.Parameters.Item("@ALM").Value = Me.LALM3.Item(num)
            '    command2.Parameters.Item("@ACT").Value = Me.CLBALM3.GetItemChecked(num)
            '    command2.ExecuteNonQuery()
            '    num += 1
            'Loop
            Dim command5 As New SqlCommand(("DELETE FROM PRECIOSSUCURSALES WHERE PRODUCTO='" & Me.TXTCLA.Text & "'"), frmPrincipal.CONX)
            command5.ExecuteNonQuery()
            command5.CommandText = "INSERT INTO PRECIOSSUCURSALES (SUCURSAL, PRODUCTO,PRECIO) VALUES (@TI,@PRO,@PPG)"
            command5.Parameters.Add("@TI", SqlDbType.VarChar)
            command5.Parameters.Add("@PRO", SqlDbType.Int).Value = Me.TXTCLA.Text
            command5.Parameters.Add("@PPG", SqlDbType.Float)
            Dim num5 As Integer = (Me.DGV.Rows.Count - 1)
            num = 0
            Do While (num <= num5)
                command5.Parameters.Item("@TI").Value = Me.DGV.Item(2, num).Value
                command5.Parameters.Item("@PPG").Value = Me.DGV.Item(1, num).Value
                command5.ExecuteNonQuery()
                num += 1
            Loop
            Dim command6 As New SqlCommand(("DELETE FROM PRODUCTOSSUCURSALES WHERE PRODUCTO='" & Me.TXTCLA.Text & "'"), frmPrincipal.CONX)
            command6.ExecuteNonQuery()
            command6.CommandText = "INSERT INTO PRODUCTOSSUCURSALES (SUCURSAL, PRODUCTO,ACTIVO) VALUES (@TI,@PRO,@ACT)"
            command6.Parameters.Add("@TI", SqlDbType.VarChar)
            command6.Parameters.Add("@PRO", SqlDbType.Int).Value = Me.TXTCLA.Text
            command6.Parameters.Add("@ACT", SqlDbType.Bit)
            Dim num6 As Integer = (Me.CLBTI.Items.Count - 1)
            num = 0
            Do While (num <= num6)
                command6.Parameters.Item("@TI").Value = Me.LTI.Item(num)
                command6.Parameters.Item("@ACT").Value = Me.CLBTI.GetItemChecked(num)
                command6.ExecuteNonQuery()
                num += 1
            Loop
            MessageBox.Show("La Información ha sido Guardada correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.LIMPIAR()
            Me.ACTIVAR(True)
        End If
    End Sub

    Private Sub LIMPIAR()
        MODULOGENERAL.LimpiarForm(Me.Controls)
        NUDMAXA.Value = 0
        NUDMAXB.Value = 0
        NUDMINA.Value = 0
        NUDMINB.Value = 0
        TXTTIVA.Text = "0"
        TXTTCIEPS.Text = "0"
        TXTCPYSSAT.Text = "01010101"
        TXTDESC.Text = "0"
        CBCC.SelectedIndex = 1
        CBACT.SelectedIndex = 1
        TXTCOSTO.Text = "0"
        DGV3.DataSource = Nothing
    End Sub
    Private Sub MARCAALMACENES()
        Dim num As Integer
        Dim box As CheckedListBox
        Dim num2 As Integer = (Me.CLBALM1.Items.Count - 1)
        num = 0
        Do While (num <= num2)
            Me.CLBALM1.SetItemChecked(num, False)
            Me.CLBALM2.SetItemChecked(num, False)
            Me.CLBALM3.SetItemChecked(num, False)
            num += 1
        Loop
        Dim table As New DataTable
        Dim table2 As New DataTable
        Dim table3 As New DataTable
        'table = MODULOGENERAL.LLENATABLA(("SELECT ALMACEN FROM PRODUCTOALMACENSOLICITUD WHERE PRODUCTO='" & Me.TXTCLA.Text & "' AND ACTIVO=1"), frmPrincipal.CadenaConexion)
        table2 = MODULOGENERAL.LLENATABLA(("SELECT ALMACEN FROM PRODUCTOALMACENCOMPRA WHERE PRODUCTO='" & Me.TXTCLA.Text & "' AND ACTIVO=1"), frmPrincipal.CadenaConexion)
        'table3 = MODULOGENERAL.LLENATABLA(("SELECT ALMACEN FROM PRODUCTOALMACENPRODUCE WHERE PRODUCTO='" & Me.TXTCLA.Text & "' AND ACTIVO=1"), frmPrincipal.CadenaConexion)
        Dim num3 As Integer = (table.Rows.Count - 1)
        num = 0
        Do While (num <= num3)
            box = Me.CLBALM1
            MODULOGENERAL.CARGAXCLB(Me.LALM1, box, table.Rows.Item(num).Item(0).ToString)
            Me.CLBALM1 = box
            num += 1
        Loop
        Dim num4 As Integer = (table2.Rows.Count - 1)
        num = 0
        Do While (num <= num4)
            box = Me.CLBALM2
            MODULOGENERAL.CARGAXCLB(Me.LALM2, box, table2.Rows.Item(num).Item(0).ToString)
            Me.CLBALM2 = box
            num += 1
        Loop
        Dim num5 As Integer = (table3.Rows.Count - 1)
        num = 0
        Do While (num <= num5)
            box = Me.CLBALM3
            MODULOGENERAL.CARGAXCLB(Me.LALM3, box, table3.Rows.Item(num).Item(0).ToString)
            Me.CLBALM3 = box
            num += 1
        Loop
    End Sub

    Private Sub MARCATIENDAS()
        Dim num As Integer
        Dim num2 As Integer = (Me.CLBTI.Items.Count - 1)
        num = 0
        Do While (num <= num2)
            Me.CLBTI.SetItemChecked(num, False)
            num += 1
        Loop
        Dim table As New DataTable
        table = MODULOGENERAL.LLENATABLA(("SELECT SUCURSAL FROM PRODUCTOSSUCURSALES WHERE PRODUCTO='" & Me.TXTCLA.Text & "' AND ACTIVO=1"), frmPrincipal.CadenaConexion)
        Dim num3 As Integer = (table.Rows.Count - 1)
        num = 0
        Do While (num <= num3)
            Dim cLBTI As CheckedListBox = Me.CLBTI
            MODULOGENERAL.CARGAXCLB(Me.LTI, cLBTI, table.Rows.Item(num).Item(0).ToString)
            Me.CLBTI = cLBTI
            num += 1
        Loop
    End Sub
    Private Function CONSECUTIVO() As String
        Dim str2 As String = "NA"
        Dim command As New SqlCommand("SELECT DBO.SIGPRODUCTO()", frmPrincipal.CONX)
        Dim reader As SqlDataReader = command.ExecuteReader
        If reader.Read Then
            str2 = reader.Item(0).ToString
        End If
        reader.Close()
        command.Dispose()
        Me.TXTCLA.Text = str2
        Return str2
    End Function
    Private Sub BTNAGREGAR_Click(sender As Object, e As EventArgs) Handles BTNAGREGAR.Click
        'Me.ACTIVAR(False)
        LIMPIAR()
        Me.CONSECUTIVO()
        CARGADATOS()
    End Sub

    Private Sub BTNBUSCAR_Click(sender As Object, e As EventArgs) Handles BTNBUSCAR.Click
        frmClsBusqueda.BUSCAR("SELECT P.CLAVE CLAVE,P.NOMBRE,P.ACTIVO FROM PRODUCTOS P", " WHERE P.NOMBRE", " ORDER BY P.NOMBRE", "Búsqueda de Productos", "Nombre del Producto", "Producto(s)", 1, frmPrincipal.CadenaConexion, False)
        If (frmClsBusqueda.DialogResult = DialogResult.Yes) Then
            Me.TXTCLA.Text = frmClsBusqueda.TREG.Rows.Item(0).Item(0).ToString
            Me.CARGADATOS()
        End If
    End Sub

    Private Sub BTNGUARDAR_Click(sender As Object, e As EventArgs) Handles BTNGUARDAR.Click
        If ((((Me.TXTNOM.Text = "") Or (Me.TXTDES.Text = "")) Or (Me.TXTNI.Text = "")) Or (Me.TXTNC.Text = "")) Then
            MessageBox.Show("Falta ingresar datos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            'Dim num2 As Double
            'Try
            '    num2 = CType(Me.TXTTASA.Text, Double)
            'Catch exception1 As Exception
            '    Dim exception As Exception = exception1
            '    MessageBox.Show("IEPS No Válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            '    Return
            'End Try

            Dim ABC As Double
            If TXTTIVA.Enabled Then
                Try
                    ABC = CType(TXTTIVA.Text, Double)
                    If ABC < 0 Or ABC > 0.16 Then
                        MessageBox.Show("Tasa Iva No Válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                        Exit Sub
                    End If
                Catch ex As Exception
                    MessageBox.Show("Tasa Iva No Válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    Exit Sub
                End Try
            End If
 
            If TXTTCIEPS.Enabled Then
                Try
                    ABC = CType(TXTTCIEPS.Text, Double)
                    If ABC < 0 Or ABC > 43.77 Then
                        MessageBox.Show("Tasa IEPS No Válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                        Exit Sub
                    End If
                Catch ex As Exception
                    MessageBox.Show("Tasa IEPS No Válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    Exit Sub
                End Try
            End If


                Dim num As Double
                Try
                    num = CType(Me.TXTDESC.Text, Double)
                Catch exception3 As Exception
                    Dim exception2 As Exception = exception3
                    MessageBox.Show("Descuento en combo No Válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    Exit Sub
                End Try
                If (num < 0) Then
                    MessageBox.Show("Descuento en combo No Válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                ElseIf Not VERIFICASELECCIONCB(Me.TabPage1.Controls) Then
                    MessageBox.Show("Debe seleccionar clasificadores", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Not VERIFICASELECCIONCB(Me.TabPage2.Controls) Then
                MessageBox.Show("Debe seleccionar clasificadores", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Not VERIFICASELECCIONCB(Me.TabPage3.Controls) Then
                MessageBox.Show("Debe seleccionar clasificadores", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Not VERIFICASELECCIONCB(Me.TabPage4.Controls) Then
                MessageBox.Show("Debe seleccionar clasificadores", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Not VERIFICASELECCIONCB(Me.TabPage5.Controls) Then
                MessageBox.Show("Debe seleccionar clasificadores", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    Me.GUARDAR()
                End If

            End If
    End Sub

    Private Sub CBFIVA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBFIVA.SelectedIndexChanged
        TXTTIVA.Enabled = False
        If LFIVA(CBFIVA.SelectedIndex) = "1" Then
            TXTTIVA.Enabled = True
        End If
    End Sub

    Private Sub CBFIEPS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBFIEPS.SelectedIndexChanged
        TXTTCIEPS.Enabled = False
        If LFIEPS(CBFIEPS.SelectedIndex) = "1" Or LFIEPS(CBFIEPS.SelectedIndex) = "2" Then
            TXTTCIEPS.Enabled = True
        End If
    End Sub

    Private Sub BTNANTG_Click(sender As Object, e As EventArgs) Handles BTNANTG.Click
        Me.IND -= 1
        If (Me.IND >= 0) Then
            Me.TXTCLA.Text = Me.LCLA.Item(Me.IND)
            Me.CARGADATOS()
        End If
        Me.CHECAIND()
    End Sub

    Private Sub BTNSIGG_Click(sender As Object, e As EventArgs) Handles BTNSIGG.Click
        Me.IND += 1
        If (Me.IND <= (Me.LCLA.Count - 1)) Then
            Me.TXTCLA.Text = Me.LCLA.Item(Me.IND)
            Me.CARGADATOS()
        End If
        Me.CHECAIND()
    End Sub

    Private Sub BTNCANCELAR_Click(sender As Object, e As EventArgs) Handles BTNCANCELAR.Click
        Me.ACTIVAR(True)
        Me.LIMPIAR()
    End Sub

    Private Sub LBLPYSSAT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LBLPYSSAT.LinkClicked
        System.Diagnostics.Process.Start("http://200.57.3.46:443/PyS/catPyS.aspx")
    End Sub

    Private Sub TXTCLA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTCLA.KeyPress
        If e.KeyChar = Chr(13) Then
            CARGADATOS()
        End If

    End Sub

    Private Sub BTNAGRE_Click(sender As Object, e As EventArgs) Handles BTNAGRE.Click
        If Not VALIDACANTIDAD(TXTCANT.Text, 2) Or Not VALIDACANTIDAD(TXTPRE.Text, 0) Then
            MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        Else
            AGREGARPRO()
        End If

    End Sub
    Private Sub AGREGARPRO()
        If frmPrincipal.CHECACONX Then
            Dim command As New SqlCommand("SPMENUPRINCIPALPRODUCTO", frmPrincipal.CONX) With {
                .CommandType = CommandType.StoredProcedure,
                .CommandTimeout = 300
            }
            command.Parameters.Add("@MENU", SqlDbType.Int).Value = Me.TXTCLA.Text
            command.Parameters.Add("@TIPO", SqlDbType.Int).Value = Me.LTA.Item(Me.CBTA.SelectedIndex)
            command.Parameters.Add("@PRO", SqlDbType.Int).Value = Me.TXTCLAPRO.Text
            command.Parameters.Add("@CANT", SqlDbType.Float).Value = Me.TXTCANT.Text
            command.Parameters.Add("@TOT", SqlDbType.Float).Value = Me.TXTPRE.Text
            command.Parameters.Add("@INC", SqlDbType.Bit).Value = Me.CHKINC.Checked
            command.Parameters.Add("@ACT", SqlDbType.Bit).Value = 1
            command.ExecuteNonQuery()
            CARGAMENUAT()
        End If
    End Sub
    Private Sub AGREGAEXTRA()
        If frmPrincipal.CHECACONX Then
            Dim command As New SqlCommand("SPMENUPRINCIPALPRODUCTO", frmPrincipal.CONX) With {
                .CommandType = CommandType.StoredProcedure,
                .CommandTimeout = 300
            }
            command.Parameters.Add("@MENU", SqlDbType.Int).Value = Me.TXTCLA.Text
            command.Parameters.Add("@TIPO", SqlDbType.Int).Value = Me.LTA.Item(Me.CBTA.SelectedIndex)
            command.Parameters.Add("@PRO", SqlDbType.Int).Value = Me.TXTCLAPRO.Text
            command.Parameters.Add("@CANT", SqlDbType.Float).Value = Me.TXTCANT.Text
            command.Parameters.Add("@TOT", SqlDbType.Float).Value = Me.TXTPRE.Text
            command.Parameters.Add("@INC", SqlDbType.Bit).Value = Me.CHKINC.Checked
            command.Parameters.Add("@ACT", SqlDbType.Bit).Value = 1
            command.ExecuteNonQuery()
            CARGAMENUAT()
        End If
    End Sub

    Private Sub BTNQUITAR_Click(sender As Object, e As EventArgs) Handles BTNQUITAR.Click

        If DGV3.Rows.Count <= 0 Then
            Exit Sub
        End If

        Dim SQL As New SqlClient.SqlCommand("DELETE FROM CONFGPAQUETESPRODUCTOS WHERE PRODUCTO='" + TXTCLA.Text + "' AND PRODUCTOOPC='" + Me.DGV3.Item(6, Me.DGV3.CurrentRow.Index).Value.ToString + "' AND TIPO='" + Me.DGV3.Item(5, Me.DGV3.CurrentRow.Index).Value.ToString + "'", frmPrincipal.CONX)
        SQL.ExecuteNonQuery()
        Me.CARGAMENUAT()
    End Sub

    Private Sub TXTNOM_TextChanged(sender As Object, e As EventArgs) Handles TXTNOM.TextChanged
        Me.LBLNP2.Text = Me.TXTNOM.Text
        Me.LBLNP3.Text = Me.TXTNOM.Text
        Me.LBLNP4.Text = Me.TXTNOM.Text
        Me.LBLNP5.Text = Me.TXTNOM.Text
        Me.LBLNP7.Text = Me.TXTNOM.Text
    End Sub

    Private Sub BTNBUSPRO_Click(sender As Object, e As EventArgs) Handles BTNBUSPRO.Click
        frmClsBusqueda.BUSCAR("SELECT P.CLAVE CLAVE,P.NOMBRE,P.ACTIVO FROM PRODUCTOS P", " WHERE P.NOMBRE", " ORDER BY P.NOMBRE", "Búsqueda de Productos", "Nombre del Producto", "Producto(s)", 1, frmPrincipal.CadenaConexion, False)
        If (frmClsBusqueda.DialogResult = DialogResult.Yes) Then
            Me.TXTCLAPRO.Text = frmClsBusqueda.TREG.Rows.Item(0).Item(0).ToString
            Me.LBLNP.Text = frmClsBusqueda.TREG.Rows.Item(0).Item(1).ToString
            Me.CARGAPRODUCTO()
        End If
    End Sub

    Private Sub TXTCLAPRO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTCLAPRO.KeyPress
        If e.KeyChar = Chr(13) Then
            Me.CARGAPRODUCTO()
        End If
    End Sub

    Private Sub CBCMB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBCMB.SelectedIndexChanged

    End Sub

    Private Sub CHKAS_CheckedChanged(sender As Object, e As EventArgs) Handles CHKAS.CheckedChanged
        CHECARCLB(CType(sender.tAG, Integer), CHKAS.Checked)
    End Sub

    Private Sub CHKAC_CheckedChanged(sender As Object, e As EventArgs) Handles CHKAC.CheckedChanged
        CHECARCLB(CType(sender.tAG, Integer), CHKAC.Checked)
    End Sub

    Private Sub CHKAP_CheckedChanged(sender As Object, e As EventArgs) Handles CHKAP.CheckedChanged
        CHECARCLB(CType(sender.tAG, Integer), CHKAP.Checked)
    End Sub

    Private Sub BTNMOSTRAR_Click(sender As Object, e As EventArgs) Handles BTNMOSTRAR.Click
        Dim VEQUI As New frmEquivalenciasProductos
        VEQUI.MOSTRAR(LFAM(CBFAM.SelectedIndex), TXTCLA.Text)
    End Sub

    Private Sub CBGV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBGV.SelectedIndexChanged

    End Sub

    Private Sub BTNBUSEXTRA_Click(sender As Object, e As EventArgs) Handles BTNBUSEXTRA.Click
        frmClsBusqueda.BUSCAR("SELECT P.CLAVE CLAVE,P.NOMBRE,P.ACTIVO FROM PRODUCTOS P", " WHERE P.NOMBRE", " ORDER BY P.NOMBRE", "Búsqueda de Productos", "Nombre del Producto", "Producto(s)", 1, frmPrincipal.CadenaConexion, False)
        If (frmClsBusqueda.DialogResult = DialogResult.Yes) Then
            Me.TXTCLAEXTRA.Text = frmClsBusqueda.TREG.Rows.Item(0).Item(0).ToString
            Me.LBLNEXTRA.Text = frmClsBusqueda.TREG.Rows.Item(0).Item(1).ToString
            Me.CARGAPRODUCTO()
        End If
    End Sub

    Private Sub BTNAGREGAREXTRA_Click(sender As Object, e As EventArgs) Handles BTNAGREGAREXTRA.Click
        Dim SQL As New SqlClient.SqlCommand("SPPRODUCTOSEXTRAS", frmPrincipal.CONX)
        SQL.CommandType = CommandType.StoredProcedure
        SQL.CommandTimeout = 300
        SQL.Parameters.Add("@PRO", SqlDbType.Int).Value = TXTCLA.Text
        SQL.Parameters.Add("@ACT", SqlDbType.Bit).Value = 1
        SQL.Parameters.Add("@CLA", SqlDbType.Int)
        SQL.Parameters("@CLA").Value = TXTCLAEXTRA.Text
        SQL.ExecuteNonQuery()
        CARGAAPLICAEXTRAS()
    End Sub
    Private Sub CARGAAPLICAEXTRAS()
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        Dim QUERY As String
        QUERY = "SELECT P.CLAVE,P.NOMBRE FROM PRODUCTOSEXTRAS D INNER JOIN PRODUCTOS P ON D.CLAVE=P.CLAVE WHERE D.PRODUCTO='" + TXTCLA.Text + "' ORDER BY P.NOMBRE"
        DGVEXT.DataSource = LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
        ACTUALIZAGRID()
    End Sub
    Private Sub ACTUALIZAGRID()
        'DGV.DataSource = DTPRIN
        DGVEXT.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DGVEXT.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub
    Private Sub BTNQUITAREXTRA_Click(sender As Object, e As EventArgs) Handles BTNQUITAREXTRA.Click
        CARGAAPLICAEXTRAS()
    End Sub
End Class