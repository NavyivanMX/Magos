Public Class clsPreNota
    Dim DT As New Data.DataTable
    Dim DT2X1 As New DataTable
    Dim DT3X2 As New DataTable
    Public DESCGENERAL As Double
    Public PUNTOSCANJ As Double
    Public Sub New()
        DT.Columns.Add("Código")
        DT.Columns.Add("Descripción")
        DT.Columns.Add("Cantidad")
        DT.Columns.Add("Descuento %")
        DT.Columns.Add("Precio")
        DT.Columns.Add("Total")
        DT.Columns.Add("PrecioOriginal")
        DT.Columns.Add("IDCombo")
        DT.Columns.Add("PromoA")
        DT.Columns.Add("PlatilloPrincipal")
        DT.Columns.Add("IDCupon")
        DESCGENERAL = 0
    End Sub
    Public Sub ActualizarCombo(ByVal Registro As Integer, ByVal IDCOMBO As String)
        DT.Rows(Registro).Item(7) = IDCOMBO
    End Sub
    Public Sub PreciosMenMay(ByVal May As Boolean)
        Dim PRE As Double
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(10) = 0 And DT.Rows(X).Item(7) = 0 Then
                If May Then
                    Dim SQL As New SqlClient.SqlCommand("SELECT PRECIOMAY FROM PRECIOSF WHERE PRODUCTO='" + DT.Rows(X).Item(0).ToString + "' AND TIENDA='" + frmPrincipal.SucursalBase + "'", frmPrincipal.CONX)
                    Dim LEC As SqlClient.SqlDataReader
                    LEC = SQL.ExecuteReader
                    If LEC.Read Then
                        PRE = LEC(0)
                    End If
                    LEC.Close()
                    SQL.Dispose()
                    DT.Rows(X).Item(4) = PRE
                    DT.Rows(X).Item(5) = PRE * DT.Rows(X).Item(2) * ((100 - DT.Rows(X).Item(3)) / 100)
                Else
                    Dim SQL As New SqlClient.SqlCommand("SELECT PRECIOPG FROM PRECIOSF WHERE PRODUCTO='" + DT.Rows(X).Item(0).ToString + "' AND TIENDA='" + frmPrincipal.SucursalBase + "'", frmPrincipal.CONX)
                    Dim LEC As SqlClient.SqlDataReader
                    LEC = SQL.ExecuteReader
                    If LEC.Read Then
                        PRE = LEC(0)
                    End If
                    LEC.Close()
                    SQL.Dispose()
                    DT.Rows(X).Item(4) = PRE
                    DT.Rows(X).Item(5) = PRE * DT.Rows(X).Item(2) * ((100 - DT.Rows(X).Item(3)) / 100)
                End If
            Else
            End If
        Next


    End Sub
    Public Sub AgregaQuita(ByVal Platillo As String, ByVal Cantidad As Double, ByVal Sumar As Boolean)
        Dim Cant As Double
        Cant = 0
        Dim Precio, Total, Descuento As Double
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(10) <> 0 Then
            Else
                If DT.Rows(X).Item(0) = Platillo And DT.Rows(X).Item(7) = 0 Then
                    If Sumar Then
                        Cant = Cant + Cantidad + CType(DT.Rows(X).Item(2), Double)
                    Else
                        Cant = Cant - Cantidad + CType(DT.Rows(X).Item(2), Double)
                    End If
                    Descuento = DT.Rows(X).Item(3)
                    Precio = DT.Rows(X).Item(4)
                    Total = Cant * Precio
                    Total = Total - ((Total * Descuento) / 100)
                    DT.Rows(X).Item(2) = Cant
                    DT.Rows(X).Item(5) = Total
                End If
            End If
        Next
        VerificarDT()
    End Sub
    Public Function BUSCARPRODUCTO(ByVal CLA As Integer) As Boolean
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(0) = CLA.ToString Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub AplicarPuntos(ByVal PTS As Integer)
        'If PTS <= 0 Then
        '    Exit Sub
        'End If
        Dim DESC, DESCA, TOTT As Double


        DESCA = Total()
        TOTT = TotalSinPuntos()
        If PTS <= 0 Then
            If DESCGENERAL > 0 Then
                For X = 0 To DT.Rows.Count - 1
                    If DT.Rows(X).Item(8) = 0 And DT.Rows(X).Item(7) = 0 Then
                        DT.Rows(X).Item(3) = DESCGENERAL
                        DT.Rows(X).Item(5) = CType(DT.Rows(X).Item(2), Double) * CType(DT.Rows(X).Item(4), Double) * (1 - (DESCGENERAL / 100))
                    End If
                Next

                For X = 0 To DT.Rows.Count - 1
                    If DT.Rows(X).Item(7) <> 0 Then
                        DT.Rows(X).Item(3) = DESCGENERAL
                        DESC = CType(DT.Rows(X).Item(2), Double)
                        DESCA = CType(DT.Rows(X).Item(5), Double)
                        TOTT = (1 - (DESCGENERAL / 100))
                        DT.Rows(X).Item(5) = CType(DT.Rows(X).Item(2), Double) * CType(DT.Rows(X).Item(4), Double) * (1 - (DESCGENERAL / 100))
                    End If
                Next

                'For X = 0 To DT.Rows.Count - 1

                '    DT.Rows(X).Item(5) = CType(DT.Rows(X).Item(5), Double) * DESC
                '    DT.Rows(X).Item(3) = DT.Rows(X).Item(3) + DESCA
                'Next
            End If

        Else
            If PTS >= TOTT Then
                For X = 0 To DT.Rows.Count - 1
                    DT.Rows(X).Item(5) = 0
                    DT.Rows(X).Item(3) = 100
                Next
            Else

                DESC = 1
                If Total() > 0 Then
                    DESC = 1 - (PTS / TOTT)
                    DESCA = (PTS / TOTT) * 100
                End If
                For X = 0 To DT.Rows.Count - 1
                    DT.Rows(X).Item(5) = CType(DT.Rows(X).Item(5), Double) * DESC
                    DT.Rows(X).Item(3) = DT.Rows(X).Item(3) + DESCA
                Next
            End If
        End If
   
    End Sub

    Public Sub DTPRODUCTOS2X1(ByVal DT As DataTable)
        DT2X1 = DT
    End Sub
    Private Function Es2x1(ByVal Platillo As String) As Boolean
        Dim X As Integer
        For X = 0 To DT2X1.Rows.Count - 1
            If DT2X1.Rows(X).Item(2) = Platillo Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function Tot2x1(ByVal Precio As Double) As Double
        Dim Platillo As String
        Dim X, Y As Integer
        Dim REG As Double
        REG = 0
        For X = 0 To DT2X1.Rows.Count - 1
            If DT2X1.Rows(X).Item(3) = Precio Then
                Platillo = DT2X1.Rows(X).Item(2)
                For Y = 0 To DT.Rows.Count - 1
                    If Platillo = DT.Rows(Y).Item(0) Then
                        REG += DT.Rows(Y).Item(2)
                    End If
                Next
            End If
        Next
        Return REG
    End Function
    Private Sub ActualizaPrecio(ByVal Precio As Double, ByVal Precio2 As Double)
        Dim Platillo As String
        Dim X, Y As Integer
        Dim Cant As Double

        For X = 0 To DT2X1.Rows.Count - 1
            If DT2X1.Rows(X).Item(3) = Precio Then
                Platillo = DT2X1.Rows(X).Item(2)
                For Y = 0 To DT.Rows.Count - 1
                    If Platillo = DT.Rows(Y).Item(0) Then
                        Cant = DT.Rows(Y).Item(2)
                        DT.Rows(Y).Item(4) = Precio2
                        DT.Rows(Y).Item(5) = Cant * Precio2
                    End If
                Next
            End If
        Next
    End Sub
    Public Sub Agregar(ByVal Platillo As String, ByVal Descripcion As String, ByVal Cantidad As Double, ByVal Precio As Double, ByVal Descuento As Double, ByVal PrecioOriginal As Double, ByVal IdCombo As Integer, Optional ByVal PA As Integer = 0, Optional ByVal PP As Integer = 0, Optional ByVal Cupon As Integer = 0)
        If DT.Columns.Count <= 0 Then
            DT.Columns.Add("Código")
            DT.Columns.Add("Descripción")
            DT.Columns.Add("Cantidad")
            DT.Columns.Add("Descuento %")
            DT.Columns.Add("Precio")
            DT.Columns.Add("Total")
            DT.Columns.Add("PrecioOriginal")
            DT.Columns.Add("IDCombo")
            DT.Columns.Add("PromoA")
            DT.Columns.Add("PlatilloPrincipal")
            DT.Columns.Add("IDCupon")
        End If

        Dim Encontrado As Boolean
        Encontrado = False
        Dim X, MIT, RES As Integer
        Dim Cant, Tot As Double
        Cant = 0
        Dim Total As Double
        If Cupon <> 0 Then
            Encontrado = False
        Else
            For X = 0 To DT.Rows.Count - 1
                If DT.Rows(X).Item(0) = Platillo And DT.Rows(X).Item(7) = IdCombo Then
                    If Precio <> DT.Rows(X).Item(4) Then
                        If Es2x1(Platillo) Then
                            Cant = Cant + Cantidad + CType(DT.Rows(X).Item(2), Double)
                            DT.Rows(X).Item(2) = Cant
                            If Cant >= 2 Then
                                MIT = Math.Floor(Cant / 2)
                                RES = Cant Mod 2
                                Tot = (MIT * Precio) + (RES * Precio)
                                ActualizaPrecio(Precio, (Tot / Cant))
                            End If
                            Encontrado = True
                        End If
                    Else
                        If Es2x1(Platillo) Then
                            Cant = Cant + Cantidad + CType(DT.Rows(X).Item(2), Double)
                            DT.Rows(X).Item(2) = Cant
                            Cant = Tot2x1(Precio)
                            If Cant >= 2 Then
                                MIT = Math.Floor(Cant / 2)
                                RES = Cant Mod 2
                                Tot = (MIT * Precio) + (RES * Precio)
                                ActualizaPrecio(Precio, (Tot / Cant))
                            End If
                            Encontrado = True
                        Else
                            Encontrado = True
                            Cant = Cant + Cantidad + CType(DT.Rows(X).Item(2), Double)
                            If Descuento > DT.Rows(X).Item(3) Then
                                DT.Rows(X).Item(4) = Precio
                            End If
                            If Precio > DT.Rows(X).Item(4) Then
                                Precio = DT.Rows(X).Item(4)
                            End If
                            Total = Cant * Precio
                            Total = Total - ((Total * Descuento) / 100)
                            DT.Rows(X).Item(2) = Cant
                            DT.Rows(X).Item(3) = Descuento
                            DT.Rows(X).Item(5) = Total
                            'DT.Rows(X).Item(6) = PrecioOriginal
                        End If
                    End If
                End If
            Next
        End If

      
        If Not Encontrado Then
            'Dim SQL As New SqlClient.SqlCommand("", frmPrincipal.CONX)
            'Dim LEC As SqlClient.SqlDataReader
            'Dim MD As Double
            'If Not frmPrincipal.CHECACONX Then
            '    Exit Sub
            'End If

            'SQL.CommandText = "SELECT DBO.MAXIMODESCUENTO('" + Platillo + "')"
            'LEC = SQL.ExecuteReader
            'If LEC.Read Then
            '    MD = LEC(0)
            'End If
            'LEC.Close()
            'If Descuento > MD And MD > 0 Then
            '    Descuento = MD
            'End If
            Dim ROW As System.Data.DataRow = DT.NewRow

            ROW(0) = Platillo
            ROW(1) = Descripcion
            ROW(2) = Cantidad
            ROW(3) = Descuento
            ROW(4) = Precio
            Total = (Cantidad * Precio) - ((Cantidad * Precio) * Descuento / 100)
            ROW(5) = Total
            ROW(6) = PrecioOriginal
            ROW(7) = IdCombo
            ROW(8) = PA

            ROW.Item(9) = PP
            ROW(10) = Cupon
            DT.Rows.Add(ROW)
        End If
    End Sub
    Private Sub VerificarDT()
        Dim X As Integer
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(2) <= 0 Then
                DT.Rows.RemoveAt(X)
                Exit For
                Exit Sub
            End If
        Next
    End Sub
    Public Sub Quitar(ByVal Platillo As String)
        Dim Encontrado As Boolean
        Encontrado = False
        Dim X As Integer
        Dim Pos As Integer
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(0) = Platillo Then
                Encontrado = True
                Pos = X
            End If
        Next
        If Encontrado Then
            'DT.Rows(Pos).Item(2) = DT.Rows(Pos).Item(2) - 1
            'DT.Rows(Pos).Item(5) = (DT.Rows(Pos).Item(2) * DT.Rows(Pos).Item(4)) - ((DT.Rows(Pos).Item(2) * DT.Rows(Pos).Item(4)) * DT.Rows(Pos).Item(3) / 100)
            DT.Rows(Pos).Item(2) = 0
            DT.Rows(Pos).Item(5) = 0
            VerificarDT()
        End If
    End Sub
    Public Sub ModificaPrecio(ByVal Articulo As String, ByVal Precio As Double, ByVal PA As Integer)
        Dim Encontrado As Boolean
        Encontrado = False
        Dim X As Integer
        Dim Pos As Integer
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(0) = Articulo And DT.Rows(X).Item(4) <> 0 Then
                Encontrado = True
                Pos = X
            End If
        Next
        ''Ultimo Costo
        If Encontrado Then
            If Not frmPrincipal.CHECACONX Then
                Exit Sub
            End If
            'Dim ULTPRE As Double
            'Dim SQL As New SqlClient.SqlCommand("SELECT DBO.ELULTIMOCOSTOPRODUCTO('" + Articulo + "')", frmPrincipal.CONX)
            'Dim LEC As SqlClient.SqlDataReader
            'LEC = SQL.ExecuteReader
            'If LEC.Read Then
            '    ULTPRE = LEC(0)
            'End If
            'LEC.Close()
            'SQL.Dispose()
            ''DT.Columns.Add("Código")
            ''DT.Columns.Add("Descripción")
            ''DT.Columns.Add("Cantidad")
            ''DT.Columns.Add("Descuento")
            ''DT.Columns.Add("Precio")
            ''DT.Columns.Add("Total")
            'If Precio < ULTPRE Then
            '    Precio = ULTPRE
            'End If

            DT.Rows(Pos).Item(3) = 0
            DT.Rows(Pos).Item(4) = Precio
            DT.Rows(Pos).Item(5) = (DT.Rows(Pos).Item(2) * Precio)
            DT.Rows(Pos).Item(8) = PA
            'VerificarDT()
        End If
    End Sub
    Public Function DescuentoSinDescuento() As Double
        Dim Tot As Double
        Tot = 0
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(8) = 0 And DT.Rows(X).Item(7) = 0 And DT.Rows(X).Item(10) = 0 Then
                Tot += DT.Rows(X).Item(5)
            End If
        Next

        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(7) <> 0 Then
                Tot += DT.Rows(X).Item(2) * DT.Rows(X).Item(4)
            End If
        Next
        'Dim a, b As Double
        If DESCGENERAL > 100 Then
            DESCGENERAL = 100
        End If
        Return (Tot * ((DESCGENERAL) / 100))
    End Function
    Public Function TotalSinPuntos() As Double
        Dim Tot As Double
        Tot = 0
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(8) = 0 And DT.Rows(X).Item(7) = 0 Then
                Tot += DT.Rows(X).Item(5)
            Else
                If DESCGENERAL > 0 Then
                    If DT.Rows(X).Item(7) <> 0 Then
                        Tot += DT.Rows(X).Item(2) * DT.Rows(X).Item(4)
                    Else

                        Tot = Tot + CType(DT.Rows(X).Item(5), Double)
                    End If
                Else
                    Tot = Tot + CType(DT.Rows(X).Item(5), Double)
                End If

            End If
        Next
        'Dim a, b As Double
        If DESCGENERAL > 100 Then
            DESCGENERAL = 100
        End If
        'a = (Tot * ((100 - DESCGENERAL) / 100))
        'b = Tot - (Tot * ((100 - DESCGENERAL) / 100))
        'Return a - b
        Return (Tot - DescuentoSinDescuento())
    End Function
    Public Function Total() As Double
        Dim Tot As Double
        Tot = 0
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(8) = 0 And DT.Rows(X).Item(7) = 0 Then
                Tot += DT.Rows(X).Item(5)
            Else
                If DESCGENERAL > 0 Then
                    If DT.Rows(X).Item(10) <> 0 Then

                    Else
                        If DT.Rows(X).Item(7) <> 0 Then
                            Tot += DT.Rows(X).Item(2) * DT.Rows(X).Item(4)
                        Else

                            Tot = Tot + CType(DT.Rows(X).Item(5), Double)
                        End If
                    End If
  
                Else
                    Tot = Tot + CType(DT.Rows(X).Item(5), Double)
                End If

            End If
        Next
        'Dim a, b As Double
        If DESCGENERAL > 100 Then
            DESCGENERAL = 100
        End If
        'a = (Tot * ((100 - DESCGENERAL) / 100))
        'b = Tot - (Tot * ((100 - DESCGENERAL) / 100))
        'Return a - b
        Return (Tot - DescuentoSinDescuento() - PUNTOSCANJ)
    End Function
    Public Function CuentaElementos() As Double
        Try
            If DT.Rows.Count <= 0 Then
                Return 0
            Else
                Dim Elementos As Double
                Elementos = 0

                For X = 0 To DT.Rows.Count - 1
                    Elementos = Elementos + CType(DT.Rows(X).Item(2), Double)
                Next
                Return Elementos
            End If
        Catch ex As Exception

        End Try

    End Function
    Public Sub Aplicar2X1(ByVal Articulo As String, ByVal PA As Integer)
        Dim Precio, Cant As Double
        Dim MIT, RES As Integer
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(0) = Articulo Then
                Cant = DT.Rows(X).Item(2)
                Precio = DT.Rows(X).Item(4)
                If Cant >= 2 Then
                    MIT = Math.Floor(Cant / 2)
                    RES = Cant Mod 2
                    DT.Rows(X).Item(5) = (MIT * Precio) + (RES * Precio)
                    DT.Rows(X).Item(8) = PA
                End If
            End If
        Next
    End Sub
    Public Sub Aplicar3X2(ByVal Articulo As String, ByVal PA As Integer)
        Dim Precio, Cant As Double
        Dim MIT, RES As Integer
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(0) = Articulo Then
                Cant = DT.Rows(X).Item(2)
                Precio = DT.Rows(X).Item(4)
                If Cant >= 3 Then
                    MIT = Math.Floor(Cant / 3)
                    RES = Cant Mod 3
                    DT.Rows(X).Item(5) = (MIT * (Precio * 2)) + (RES * Precio)
                    DT.Rows(X).Item(8) = PA
                End If
            End If
        Next
    End Sub

    Public Sub Aplicar3X2Adv(ByVal TDT As DataTable)
        'Dim num6 As Integer
        'Dim num7 As Integer
        'Dim right As Double = 9999999
        Dim PENC As Integer = 0
        'Dim num9 As Integer = (TDT.Rows.Count - 1)
        'num7 = 0


        Dim N, M, O As String
        Dim X, A, B, PA As Integer
        Dim CPA As Integer
        CPA = 0
        For X = 0 To TDT.Rows.Count - 1
            CPA = TDT.Rows.Item(X).Item(17).ToString
            For A = 0 To DT.Rows.Count - 1
                N = TDT.Rows.Item(X).Item(2).ToString
                M = Me.DT.Rows(A).Item(0).ToString
                O = DT.Rows(A).Item(2).ToString
                If (TDT.Rows.Item(X).Item(2).ToString = Me.DT.Rows(A).Item(0).ToString) And DT.Rows(A).Item(3).ToString = "0" Then
                    DT.Rows(A).Item(4) = DT.Rows(A).Item(6)
                    DT.Rows(A).Item(5) = DT.Rows(A).Item(6) * DT.Rows(A).Item(2)
                End If
            Next
        Next

        For X = 0 To TDT.Rows.Count - 1
            For A = 0 To DT.Rows.Count - 1
                N = TDT.Rows.Item(X).Item(2).ToString
                M = Me.DT.Rows(A).Item(0).ToString
                O = DT.Rows(A).Item(2).ToString
                If (TDT.Rows.Item(X).Item(2).ToString = Me.DT.Rows(A).Item(0).ToString) And DT.Rows(A).Item(3).ToString = "0" And DT.Rows.Item(A).Item(7).ToString = "0" Then
                    PENC += CType(Me.DT.Rows.Item(A).Item(2).ToString, Integer)
                    DT.Rows(A).Item(8) = 0
                End If
            Next
        Next



        'Dim LLPPAA As New List(Of String)
        'LLPPAA.Clear()
        Dim ALGOA, ALGOB, c, d, e As String
        'Dim PRE As Double
        'PRE = 9999
        If PENC >= 3 Then
            PA = CInt(Math.Floor(PENC / 3))
            Dim DV As New DataView
            Dim DTP As New DataTable
            DTP = New DataView(Me.DT, "IDCombo=0", "PrecioOriginal", DataViewRowState.CurrentRows).ToTable
            'For A = 0 To DTP.Rows.Count - 1
            '    LLPPAA.Add(DTP.Rows(A).Item(0).ToString + "-" + DTP.Rows(A).Item(1).ToString + "-" + DTP.Rows(A).Item(2).ToString + "-" + DTP.Rows(A).Item(6).ToString)
            'Next
            For A = 0 To DTP.Rows.Count - 1
                For X = 0 To TDT.Rows.Count - 1
                    If (TDT.Rows.Item(X).Item(2).ToString = DTP.Rows(A).Item(0).ToString) Then
                        For B = 0 To DT.Rows.Count - 1
                            ALGOA = DT.Rows.Item(B).Item(0).ToString
                            ALGOB = DT.Rows(A).Item(3).ToString
                            If DTP.Rows(A).Item(0).ToString = DT.Rows.Item(B).Item(0).ToString And DT.Rows(B).Item(3).ToString = "0" And DT.Rows.Item(B).Item(7).ToString = "0" Then
                                'If DTP.Rows(A).Item(0).ToString = DT.Rows.Item(B).Item(0).ToString And DT.Rows.Item(B).Item(7).ToString = "0" Then
                                Me.DT.Rows.Item(B).Item(5) = DT.Rows.Item(B).Item(2) * DT.Rows.Item(B).Item(6)
                                If PA > 0 Then
                                    'ALGO = DTP.Rows(A).Item(0).ToString + "-" + DTP.Rows(A).Item(1).ToString + "-" + DTP.Rows(A).Item(2).ToString + "-" + DTP.Rows(A).Item(6).ToString
                                    'PRE = CType(Me.DT.Rows.Item(B).Item(6).ToString, Double)
                                    If PA >= CType(DT.Rows(B).Item(2).ToString, Double) Then
                                        Me.DT.Rows.Item(B).Item(5) = 0
                                        Me.DT.Rows.Item(B).Item(9) = 0
                                        Me.DT.Rows.Item(B).Item(8) = CPA
                                        PA = PA - CType(DT.Rows(B).Item(2).ToString, Double)
                                    Else
                                        Me.DT.Rows.Item(B).Item(5) = CType(Me.DT.Rows.Item(B).Item(6).ToString, Double) * ((CType(DT.Rows(B).Item(2).ToString, Double) - PA))
                                        'c = CType(Me.DT.Rows.Item(B).Item(6).ToString, Double)
                                        'd = ((CType(DT.Rows(B).Item(2).ToString, Double) - PA))
                                        'e = CType(Me.DT.Rows.Item(B).Item(6).ToString, Double) * ((CType(DT.Rows(B).Item(2).ToString, Double) - PA))
                                        'PA = 0
                                        Me.DT.Rows.Item(B).Item(8) = CPA
                                        PA = PA - CType(DT.Rows(B).Item(2).ToString, Double)
                                    End If
                                End If
                            End If
                        Next

                    End If
                Next
            Next
        End If
        'Do While (num7 <= num9)
        '    Dim num10 As Integer = (Me.DT.Rows.Count - 1)
        '    num6 = 0
        '    Do While (num6 <= num10)
        '        If (TDT.Rows.Item(num7).Item(2).ToString = Me.DT.Rows.Item(num6).Item(0).ToString) Then
        '            left = Conversions.ToInteger(Operators.AddObject(left, Me.DT.Rows.Item(num6).Item(2)))
        '        End If
        '        num6 += 1
        '    Loop
        '    num7 += 1
        'Loop

        'If (left >= 3) Then
        '    Dim view As New DataView
        '    Dim table As New DataTable
        '    table = New DataView(Me.DT, "IDCombo=0", "PRECIO", DataViewRowState.CurrentRows).ToTable
        '    Dim num As Integer = CInt(Math.Round(Math.Floor(CDbl((CDbl(left) / 3)))))
        '    Dim num11 As Integer = (TDT.Rows.Count - 1)
        '    num7 = 0
        '    Do While (num7 <= num11)
        '        Dim num12 As Integer = (table.Rows.Count - 1)
        '        num6 = 0
        '        Do While (num6 <= num12)
        '            If Conversions.ToBoolean(Operators.AndObject(Operators.AndObject((TDT.Rows.Item(num7).Item(2).ToString = table.Rows.Item(num6).Item(0).ToString), Operators.CompareObjectEqual(table.Rows.Item(num6).Item(7), 0, False)), Operators.CompareObjectGreater(table.Rows.Item(num6).Item(2), 0, False))) Then
        '                Dim num13 As Integer = (Me.DT.Rows.Count - 1)
        '                Dim i As Integer = 0
        '                Do While (i <= num13)
        '                    If ((num > 0) AndAlso Conversions.ToBoolean(Operators.AndObject(Operators.AndObject((table.Rows.Item(num6).Item(0).ToString = Me.DT.Rows.Item(i).Item(0).ToString), Operators.CompareObjectEqual(Me.DT.Rows.Item(num6).Item(7), 0, False)), Operators.CompareObjectGreater(Me.DT.Rows.Item(num6).Item(2), 0, False)))) Then
        '                        If Operators.ConditionalCompareObjectLessEqual(num, Me.DT.Rows.Item(i).Item(2), False) Then
        '                            right = Conversions.ToDouble(Me.DT.Rows.Item(i).Item(4).ToString)
        '                            Me.DT.Rows.Item(i).Item(5) = Operators.MultiplyObject(Operators.SubtractObject(Me.DT.Rows.Item(i).Item(2), num), right)
        '                            num = 0
        '                        Else
        '                            Me.DT.Rows.Item(i).Item(5) = 0
        '                            num = Conversions.ToInteger(Operators.SubtractObject(num, Me.DT.Rows.Item(i).Item(2)))
        '                        End If
        '                    End If
        '                    i += 1
        '                Loop
        '            End If
        '            num6 += 1
        '        Loop
        '        num7 += 1
        '    Loop
        'End If
    End Sub

    Public Function UltimoCombo() As Integer
        Dim UC As Integer
        UC = 0
        Dim X As Integer
        For X = 0 To DT.Rows.Count - 1
            If CType(DT.Rows(X).Item(7).ToString, Integer) > UC Then
                UC = CType(DT.Rows(X).Item(7).ToString, Integer)
            End If
        Next
        Return UC
    End Function
    Public Sub PoneDescuento(ByVal Desc As Double)
        Dim X As Integer
        Dim SQL As New SqlClient.SqlCommand("", frmPrincipal.CONX)
        Dim LEC As SqlClient.SqlDataReader
        Dim MD, PRE As Double
        If Not frmPrincipal.CHECACONX Then
            Exit Sub
        End If
        For X = 0 To DT.Rows.Count - 1
            DT.Rows(X).Item(3) = Desc


            'SQL.CommandText = "SELECT DBO.MAXIMODESCUENTO('" + DT.Rows(X).Item(0).ToString + "')"
            'LEC = SQL.ExecuteReader
            'If LEC.Read Then
            '    MD = LEC(0)
            'End If
            'LEC.Close()
            'If Desc > MD And MD > 0 Then
            '    DT.Rows(X).Item(3) = MD
            'Else
            '    DT.Rows(X).Item(3) = Desc
            'End If
            'SQL.CommandText = "SELECT PRECIO FROM PRODUCTOSVTA WHERE CLAVE='" + DT.Rows(X).Item(0).ToString + "'"
            'LEC = SQL.ExecuteReader
            'If LEC.Read Then
            '    PRE = LEC(0)
            'End If
            'LEC.Close()
            'DT.Rows(X).Item(4) = PRE
        Next
        For X = 0 To DT.Rows.Count - 1
            DT.Rows(X).Item(5) = (DT.Rows(X).Item(2) * DT.Rows(X).Item(4)) - ((DT.Rows(X).Item(2) * DT.Rows(X).Item(4)) * DT.Rows(X).Item(3) / 100)
        Next
    End Sub

    Public Sub AgregarDescuento()
        If DESCGENERAL = 0 Then
            Exit Sub
        End If
        Dim X As Integer
        'Dim ALGO As String
        'For X = 0 To DT.Rows.Count - 1
        'SQL.CommandText = "SELECT DBO.MAXIMODESCUENTO('" + DT.Rows(X).Item(0).ToString + "')"
        'LEC = SQL.ExecuteReader
        'If LEC.Read Then
        '    MD = LEC(0)
        'End If
        'LEC.Close()
        'ALGO = DT.Rows(X).Item(8).ToString
        'If DT.Rows(X).Item(8).ToString = "0" Then
        '    If Desc > MD And MD > 0 Then
        '        DT.Rows(X).Item(3) += MD
        '    Else
        '        DT.Rows(X).Item(3) += Desc
        '    End If

        'End If


        'SQL.CommandText = "SELECT PRECIO FROM PRODUCTOSVTA WHERE CLAVE='" + DT.Rows(X).Item(0).ToString + "'"
        'LEC = SQL.ExecuteReader
        'If LEC.Read Then
        '    PRE = LEC(0)
        'End If
        'LEC.Close()
        'DT.Rows(X).Item(4) = PRE
        'Next
        Dim TTEMP, TNUEVO, a, b, c, d As Double
        For X = 0 To DT.Rows.Count - 1
            'TTEMP = 0
            'TNUEVO = 0
            'a = DT.Rows(X).Item(5)
            'b = DT.Rows(X).Item(3)
            'TTEMP = DT.Rows(X).Item(5) / (1 - (DT.Rows(X).Item(3) / 100))
            DT.Rows(X).Item(3) += DESCGENERAL

            DT.Rows(X).Item(5) = DT.Rows(X).Item(5) * ((100 - DESCGENERAL) / 100)

            If DT.Rows(X).Item(3) > 100 Then
                DT.Rows(X).Item(3) = 100
            End If
            'a = DT.Rows(X).Item(3)
            'b = TTEMP * (1 - ((100 - DT.Rows(X).Item(3)) / 100))
            'c = (100 - DT.Rows(X).Item(3))
            'd = ((100 - DT.Rows(X).Item(3)) / 100)
            'TNUEVO = (1 - ((100 - DT.Rows(X).Item(3)) / 100))

        Next
        ''' aki reviso el nuevo total  10% =  90    --->   20%  =
        'For X = 0 To DT.Rows.Count - 1
        '    DT.Rows(X).Item(5) = (DT.Rows(X).Item(2) * DT.Rows(X).Item(4)) - ((DT.Rows(X).Item(2) * DT.Rows(X).Item(4)) * DT.Rows(X).Item(3) / 100)
        'Next
    End Sub
    Public Sub QuitarDescuentos()
        Dim X As Integer
        Dim Cant, Precio As Double
        Cant = 0
        Dim Total As Double
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(7) = 0 Then
                Cant = DT.Rows(X).Item(2)
                Precio = DT.Rows(X).Item(4)
                Total = Cant * Precio
                DT.Rows(X).Item(3) = 0
                DT.Rows(X).Item(5) = Total
            Else

            End If

        Next
    End Sub
    Public Sub QuitarUltimo()
        If (Me.DT.Rows.Count > 0) Then
            Dim num2 As Integer = (Me.DT.Rows.Count - 1)
            Dim right As Integer = CType(Me.DT.Rows.Item(num2).Item(7), Integer)
            If (right = 0) Then
                Me.DT.Rows.Item(num2).Item(2) = 0
                Me.DT.Rows.Item(num2).Item(5) = 0
                Me.VerificarDT()
            Else
                Dim num6 As Integer
                Dim num4 As Integer = 1
                Dim RI As Integer
                RI = DT.Rows.Count - 1
                Dim X, Y As Integer
                For X = 0 To RI
                    'Dim num5 As Integer = (Me.DT.Rows.Count - 1)
                    'Dim i As Integer = 0
                    'Do While (i <= num5)
                    '    If CType(Me.DT.Rows.Item(i).Item(7), Integer) <> right Then
                    '        Me.DT.Rows.Item(i).Item(2) = 0
                    '        Me.DT.Rows.Item(i).Item(5) = 0
                    '        Me.VerificarDT()
                    '        Exit Do
                    '    End If
                    '    i += 1
                    'Loop

                    Do
                        Dim num5 As Integer = (Me.DT.Rows.Count - 1)
                        Dim i As Integer = 0
                        Do While (i <= num5)
                            If CType(Me.DT.Rows.Item(i).Item(7), Integer) = right Then
                                Me.DT.Rows.Item(i).Item(2) = 0
                                Me.DT.Rows.Item(i).Item(5) = 0
                                Me.VerificarDT()
                                Exit Do
                            End If
                            i += 1
                        Loop
                        num4 += 1
                        num6 = 5
                    Loop While (num4 <= num6)
                Next


                'Do
                '    Dim num5 As Integer = (Me.DT.Rows.Count - 1)
                '    Dim i As Integer = 0
                '    Do While (i <= num5)
                '        If CType(Me.DT.Rows.Item(i).Item(7), Integer) <> right Then
                '            Me.DT.Rows.Item(i).Item(2) = 0
                '            Me.DT.Rows.Item(i).Item(5) = 0
                '            Me.VerificarDT()
                '            Exit Do
                '        End If
                '        i += 1
                '    Loop
                '    num4 += 1
                '    num6 = 5
                'Loop While (num4 <= num6)
            End If
        End If
    End Sub
    Public Sub PoneDescuentoProducto(ByVal Platillo As String, ByVal Descuento As Double, ByVal PA As Integer)
        If DT.Columns.Count <= 0 Then
            DT.Columns.Add("Código")
            DT.Columns.Add("Descripción")
            DT.Columns.Add("Cantidad")
            DT.Columns.Add("Descuento %")
            DT.Columns.Add("Precio")
            DT.Columns.Add("Total")
            DT.Columns.Add("PrecioOriginal")
            DT.Columns.Add("IDCombo")
            DT.Columns.Add("PromoA")
            DT.Columns.Add("PlatilloPrincipal")
        End If
        Dim Encontrado As Boolean
        Encontrado = False
        Dim X As Integer
        Dim Cant, Precio As Double
        Cant = 0
        Dim Total As Double
        For X = 0 To DT.Rows.Count - 1
            If DT.Rows(X).Item(0) = Platillo And DT.Rows(X).Item(7) = 0 Then
                Encontrado = True
                Cant = DT.Rows(X).Item(2)
                Precio = DT.Rows(X).Item(4)
                Total = Cant * Precio
                If Descuento > 0 Then
                    Total = Total - ((Total * Descuento) / 100)
                End If
                DT.Rows(X).Item(3) = Descuento
                DT.Rows(X).Item(5) = Total
                DT.Rows(X).Item(8) = PA
            End If
        Next
    End Sub
    Public Sub EliminarNota()
        DT.Rows.Clear()
    End Sub
    Public Function PRODUCTOSVTAAgregados() As Data.DataTable
        Dim DTT As Data.DataTable
        DTT = DT
        DTT.Columns(0).SetOrdinal(5)
        Return DTT
    End Function
    Public Function ElementosAgregados() As Data.DataTable
        Return DT

        'Return DT
    End Function
End Class
