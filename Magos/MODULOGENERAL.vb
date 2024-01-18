Imports System.IO
Imports System.Net.Mail
Imports System.Data.OleDb
Imports System.Net
Imports System.Windows
Imports System.Net.Mime
Imports System.Drawing.Printing

Imports System.Runtime.InteropServices
Imports System.Net.Sockets
Imports System.Security.Cryptography
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Threading
Imports Microsoft.Office.Interop

Module MODULOGENERAL
    Public Sub LimpiarForm(ByVal Controls As System.Windows.Forms.Control.ControlCollection)
        For Each c As Control In Controls
            If Not c.Controls Is Nothing AndAlso c.Controls.Count > 0 Then
                LimpiarForm(c.Controls)
                LimpiarControles(c)
            ElseIf c.GetType().Equals(GetType(TextBox)) Or c.GetType().Equals(GetType(RichTextBox)) Or c.GetType().Equals(GetType(MaskedTextBox)) Then
                If (c.Tag > 0 And c.Tag < 4) Then
                    c.Text = ""
                Else
                    If c.TabIndex = 0 Then
                        c.Text = ""
                        c.Focus()
                    End If
                End If
            End If
        Next
    End Sub

    Public Function CREAELEMENTO(ByVal NPRO As String, ByVal TOTAL As String) As GroupBox
        Dim TAMA As New Size
        Dim PUNTO As New Point
        Dim GB As New GroupBox
        Dim PB As New PictureBox
        Dim LBL1 As New Label
        Dim LBL2 As New Label
        Dim LBL3 As New Label
        Dim PB2 As New PictureBox
        PB2.Image = My.Resources.Resources.itemCarrito
        GB.BackgroundImage = My.Resources.Resources.itemCarrito
        GB.Font = New Font("Tellural", 12.0!, FontStyle.Bold)
        LBL1.Font = New Font("Tellural", 12.0!, FontStyle.Bold)
        LBL2.Font = New Font("Tellural", 12.0!, FontStyle.Bold)
        LBL3.Font = New Font("Tellural", 12.0!, FontStyle.Bold)



        TAMA.Height = 50
        TAMA.Width = 340
        GB.Size = TAMA

        'PUNTO.X = 5
        'PUNTO.Y = 20
        'TAMA.Height = 89
        'TAMA.Width = 89
        'PB.Size = TAMA
        'PB.Location = PUNTO
        'PB.ImageLocation = RUTA
        'PB.SizeMode = PictureBoxSizeMode.Zoom

        PUNTO.Y = 8
        PUNTO.X = 7

        TAMA.Height = 59
        TAMA.Width = 330
        LBL1.AutoSize = False

        Dim TAMA2 As New Size
        TAMA2.Width = 330
        TAMA2.Height = 50
        LBL1.MaximumSize = TAMA2
        LBL1.Size = TAMA2
        LBL1.Location = PUNTO
        LBL1.Text = NPRO
        LBL1.ForeColor = Color.FromArgb(frmPrincipal.COLORBASE)
        LBL1.SendToBack()
        LBL1.TextAlign = ContentAlignment.TopLeft


        'PUNTO.Y = 87
        'PUNTO.X = 97
        'TAMA.Height = 18
        'TAMA.Width = 85

        'LBL2.Text = CANT
        'LBL2.Location = PUNTO
        'LBL2.TextAlign = ContentAlignment.MiddleCenter
        'LBL2.ForeColor = Color.Red
        'LBL2.BringToFront()

        PUNTO.Y = 39
        PUNTO.X = 230
        TAMA.Height = 28
        TAMA.Width = 109


        LBL3.Size = TAMA
        LBL3.Location = PUNTO
        LBL3.TextAlign = ContentAlignment.BottomRight
        LBL3.ForeColor = Color.Red
        LBL3.Text = "$ " + FormatNumber(TOTAL, 2).ToString
        LBL3.BringToFront()

        GB.ForeColor = Color.Navy
        GB.Controls.Add(LBL3)
        'GB.Controls.Add(LBL2)
        GB.Controls.Add(LBL1)

        'GB.Controls.Add(PB)
        GB.Controls.Item(0).BringToFront()

        TAMA.Height = 0
        TAMA.Width = 340
        GB.AutoSize = True
        TAMA2 = New Size(TAMA.Width, 70)
        GB.MaximumSize = TAMA2
        TAMA2 = New Size(TAMA.Width, 50)
        GB.MinimumSize = TAMA2

        Return GB
    End Function
    Public Sub VISUALIZACION(ByRef FRM As Windows.Forms.Form)
        Try
            FRM.BackgroundImage = frmPrincipal.BackgroundImage
            FRM.BackgroundImageLayout = ImageLayout.Stretch
        Catch ex As Exception

        End Try
        'Try
        '    FRM.StartPosition = FormStartPosition.CenterScreen
        '    FRM.ShowInTaskbar = False
        '    FRM.MinimizeBox = False
        '    FRM.MaximizeBox = False
        '    FRM.FormBorderStyle = FormBorderStyle.FixedSingle
        'Catch ex As Exception

        'End Try
        Dim ALGO As String
        'Dim TAMABOTON As Size
        'TAMABOTON.Width = 80
        'TAMABOTON.Height = 80
        Try
            For Each Btn As Button In FRM.Controls
                If Btn.Tag = "" Then
                    'Btn.Size = TAMABOTON
                End If

            Next
            For Each c As Control In FRM.Controls
                ALGO = c.Name
                Try
                    If TypeOf c Is TextBox Then
                        c.BackColor = Color.Transparent
                    End If
                Catch ex As Exception

                End Try
                Try
                    If TypeOf c Is Button Then
                        'c.Size = TAMABOTON

                    End If
                Catch ex As Exception

                End Try
                Try
                    If TypeOf c Is GroupBox Then
                        c.BackColor = Color.Transparent
                    End If
                Catch ex As Exception

                End Try
                Try
                    If TypeOf c Is Label Then
                        c.BackColor = Color.Transparent
                    End If
                Catch ex As Exception

                End Try
                Try
                    If TypeOf c Is CheckBox Then
                        c.BackColor = Color.Transparent
                    End If
                Catch ex As Exception

                End Try
                Try
                    If TypeOf c Is PictureBox Then
                        c.BackColor = Color.Transparent
                    End If
                Catch ex As Exception

                End Try

                Try
                    If TypeOf c Is TabControl Then
                        c.BackColor = Color.Transparent
                    End If
                Catch ex As Exception

                End Try
                Try
                    If TypeOf c Is TabPage Then
                        c.BackColor = Color.Transparent
                    End If
                Catch ex As Exception

                End Try

            Next
        Catch ex As Exception

        End Try

    End Sub
    Public Sub BORRATEMP()
        For Each subDirectory As String In Directory.GetDirectories("D:\Temp")
            System.IO.Directory.Delete("D:\Temp\" + subDirectory)
        Next
    End Sub
    Public Function EsAdministrador() As Boolean
        Thread.GetDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal)
        Dim currentPrincipal As WindowsPrincipal = TryCast(Thread.CurrentPrincipal, WindowsPrincipal)
        Return currentPrincipal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function
    'Converts the DataGridView to DataTable
    Public Function CHECARCLB(ByRef CLB As CheckedListBox, ByVal V As Boolean)
        For X As Integer = 0 To CLB.Items.Count - 1
            CLB.SetItemChecked(X, V)
        Next
        Return True
    End Function
    Public Sub GBITACORA(ByVal USU As String, ByVal ACC As Integer, ByVal ELE As String, ByVal VA As String, ByVal VM As String, ByVal OBS As String)
        Dim SQL As New SqlClient.SqlCommand("SPBITACORA", frmPrincipal.CONX)
        SQL.CommandType = CommandType.StoredProcedure
        SQL.Parameters.Add("@USU", SqlDbType.VarChar).Value = USU
        SQL.Parameters.Add("@PC", SqlDbType.VarChar).Value = SystemInformation.ComputerName
        SQL.Parameters.Add("@ELE", SqlDbType.VarChar).Value = ELE
        SQL.Parameters.Add("@VA", SqlDbType.VarChar).Value = VA
        SQL.Parameters.Add("@VM", SqlDbType.VarChar).Value = VM
        SQL.Parameters.Add("@OBS", SqlDbType.VarChar).Value = OBS
        SQL.Parameters.Add("@ACC", SqlDbType.Int).Value = ACC
        SQL.ExecuteNonQuery()
        SQL.Dispose()
    End Sub
    Public Function DataGridView2DataTable(ByVal dgv As DataGridView) As DataTable

        Dim dt As New DataTable()

        ' Header columns
        For Each column As DataGridViewColumn In dgv.Columns
            Dim dc As New DataColumn(column.Name.ToString(), column.CellType)
            dt.Columns.Add(dc)
        Next

        ' Data cells
        For i As Integer = 0 To dgv.Rows.Count - 1
            Dim row As DataGridViewRow = dgv.Rows(i)
            Dim dr As DataRow = dt.NewRow()
            For j As Integer = 0 To dgv.Columns.Count - 1
                dr(j) = If((row.Cells(j).Value Is Nothing), "", row.Cells(j).Value.ToString())
            Next
            dt.Rows.Add(dr)
        Next

        ' Related to the bug arround min size when using ExcelLibrary for export
        'For i As Integer = 0 To dgv.Rows.Count - 1
        '    Dim dr As DataRow = dt.NewRow()
        '    For j As Integer = 0 To dt.Columns.Count - 1
        '        dr(j) = "  "
        '    Next
        '    dt.Rows.Add(dr)
        'Next
        Return dt
    End Function
    Public Function FECHA2TEXT(ByVal DT As DateTime) As String
        Dim DIA, MES, AÑO, REG As String

        DIA = DT.Day
        MES = DT.Month
        AÑO = DT.Year
        If DIA < 10 Then
            REG = "0" + DIA
        Else
            REG = DIA
        End If
        If MES = "1" Then
            REG = REG + " DE ENERO"
        End If
        If MES = "2" Then
            REG = REG + " DE FEBRERO"
        End If
        If MES = "3" Then
            REG = REG + " DE MARZO"
        End If
        If MES = "4" Then
            REG = REG + " DE ABRIL"
        End If
        If MES = "5" Then
            REG = REG + " DE MAYO"
        End If
        If MES = "6" Then
            REG = REG + " DE JUNIO"
        End If
        If MES = "7" Then
            REG = REG + " DE JULIO"
        End If
        If MES = "8" Then
            REG = REG + " DE AGOSTO"
        End If
        If MES = "9" Then
            REG = REG + " DE SEPTIEMBRE"
        End If
        If MES = "10" Then
            REG = REG + " DE OCTUBRE"
        End If
        If MES = "11" Then
            REG = REG + " DE NOVIEMBRE"
        End If
        If MES = "12" Then
            REG = REG + " DE DICIEMBRE"
        End If
        REG = REG + " " + AÑO
        Return REG
    End Function
    Public Function GenerateGUID() As String
        Dim sGUID As String
        sGUID = System.Guid.NewGuid.ToString()
        Return sGUID
    End Function
    Public Function FORMATOCADENA(ByVal TEXTO As String, ByVal LONGITUD As Integer) As String
        Dim REG As String
        REG = TEXTO
        If TEXTO.Length > LONGITUD Then
            REG = TEXTO.Remove(LONGITUD, TEXTO.Length - LONGITUD)
        End If
        If TEXTO.Length < LONGITUD Then
            Dim X As Integer
            For X = TEXTO.Length To LONGITUD
                REG = REG.Insert(X, " ")
            Next
        End If
        Return REG
    End Function
    Public Function FORMATONUMERICO() As DataGridViewCellStyle
        Dim CS As New DataGridViewCellStyle
        CS.Alignment = DataGridViewContentAlignment.MiddleRight
        CS.Format = "###,###,###,##0.00"
        Return CS
    End Function
    Public Function FORMATONUMERICOND(ByVal NumeroDecimales As Integer) As DataGridViewCellStyle
        Dim CS As New DataGridViewCellStyle
        CS.Alignment = DataGridViewContentAlignment.MiddleRight

        Dim TEXTO As String
        TEXTO = "###,###,###,##0."
        Dim X As Integer
        For X = 0 To NumeroDecimales - 1
            TEXTO = TEXTO.Insert(16 + X, "0")
        Next
        CS.Format = TEXTO
        Return CS
    End Function
    Public Function FORMATOHORA() As DataGridViewCellStyle
        Dim CS As New DataGridViewCellStyle
        CS.Alignment = DataGridViewContentAlignment.MiddleRight
        CS.Format = "hh:mm:ss tt"
        Return CS
    End Function
    Public Function FORMATOFECHA() As DataGridViewCellStyle
        Dim CS As New DataGridViewCellStyle
        CS.Alignment = DataGridViewContentAlignment.MiddleRight
        CS.Format = "dd/MM/yyyy"
        Return CS
    End Function
    Public Function FORMATOPCT() As DataGridViewCellStyle
        Dim CS As New DataGridViewCellStyle
        CS.Alignment = DataGridViewContentAlignment.MiddleRight
        CS.Format = "###,###,###,##0.00%"
        Return CS
    End Function
    Public Function ESTILO(ByVal FONDO As Color, ByVal FUENTE As Color) As DataGridViewCellStyle
        Dim RESTILO As New DataGridViewCellStyle
        RESTILO.BackColor = FONDO
        RESTILO.ForeColor = FUENTE
        Return RESTILO
    End Function

    Public Sub LimpiarControles(ByVal c1 As Control)
        'Limpia todos los controles dentro del control principal (c1)
        'aunque esten dentro de otros controles
        For Each c As Control In c1.Controls
            If TypeOf c Is TextBox Then
                c.Text = "" ' eliminar el texto 
            Else
                LimpiarControles(c)
            End If
            If TypeOf c Is ComboBox Then
                c.Text = "" ' eliminar el texto 
            Else
                LimpiarControles(c)
            End If
            If TypeOf c Is DateTimePicker Then
                c.Text = Date.Today ' Pone la fecha actual 
            Else
                LimpiarControles(c)
            End If
            If (TypeOf (c) Is CheckBox) Then
                CType(c, CheckBox).Checked = False 'Quita el check
            Else
                LimpiarControles(c)
            End If
            If (TypeOf (c) Is ComboBox) Then
                Try
                    CType(c, ComboBox).SelectedIndex = 0 'Pongo el primero
                Catch ex As Exception

                End Try
            Else
                LimpiarControles(c)
            End If
            If (TypeOf (c) Is RadioButton) Then
                CType(c, RadioButton).Checked = False 'Quita el check
            Else
                LimpiarControles(c)
            End If
        Next
    End Sub
    Public Function PRUEBAPING(ByVal DIRECCIONoIP As String, ByVal NombreComun As String, ByVal Mostrar As Boolean) As Boolean
        Try


            Dim ECO As New System.Net.NetworkInformation.Ping
            Dim RES As System.Net.NetworkInformation.PingReply
            Dim I As Integer
            Dim RESS As String
            RESS = ""
            Dim REG As Boolean
            For I = 0 To 3
                RES = ECO.Send(DIRECCIONoIP)
                If RES.Status = NetworkInformation.IPStatus.Success Then
                    RESS += "Tiempo de Respuesta desde " + NombreComun + " aproximado en milisegundos : " & RES.RoundtripTime.ToString + "." + ControlChars.NewLine
                    REG = True
                Else
                    RESS += "No se puede acceder al sitio: " + NombreComun + ". Verifique su conexión a Internet" + ControlChars.NewLine
                    REG = False
                End If
            Next
            If Mostrar Then
                MessageBox.Show(RESS + " Tiempos mayores a 250 se consideran lentos", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            End If
            Return REG
        Catch ex As Exception
            If Mostrar Then
                MessageBox.Show(ex.ToString, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            End If
            Return False
        End Try

    End Function
    Public Function CARGAX(ByRef LISTA As List(Of String), ByRef CB As ComboBox, ByVal VALOR As String) As Boolean
        Dim X As Integer
        For X = 0 To LISTA.Count - 1
            If LISTA(X) = VALOR Then
                CB.SelectedIndex = X
                Return True
            End If
        Next
        Try
            CB.SelectedIndex = 0
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function
    Public Sub CARGAXCLB(ByRef LISTA As List(Of String), ByRef CLB As CheckedListBox, ByVal VALOR As String)
        Dim X As Integer
        For X = 0 To LISTA.Count - 1
            If LISTA(X) = VALOR Then
                CLB.SetItemChecked(X, True)
                CLB.SelectedIndex = X
                Exit Sub
            End If
        Next
    End Sub
    Public Function GUARDARREPORTEDIRECTO(ByVal REP As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal DT As DataTable, ByVal TIPOEXPORTAR As CrystalDecisions.Shared.ExportFormatType, ByVal RutaNombreArchivo As String, ByVal NombreImpresora As String) As Boolean
        Try
            If System.IO.File.Exists(RutaNombreArchivo) = True Then
                System.IO.File.Delete(RutaNombreArchivo)
            End If
        Catch ex As Exception
            MessageBox.Show("La informacion No se puede Guardar, Archivo en Uso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
            Exit Function
        End Try
        If NombreImpresora = "" Then
        Else
            Dim ImpSettings As String
            ImpSettings = ImpresoraParaSettings(NombreImpresora)
            If ImpSettings = "" Then
            Else
                REP.PrintOptions.PrinterName = ImpSettings
            End If
        End If
        REP.SetDataSource(DT)
        REP.ExportToDisk(TIPOEXPORTAR, RutaNombreArchivo)
        MessageBox.Show("El archivo ha sido Guardado Exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        Return False
    End Function
    Public Function LLENACOMBOBOX(ByRef CB As ComboBox, ByRef LISTA As List(Of String), ByVal QUERY As String, ByVal CADENA As String) As Boolean
        CB.Items.Clear()
        LISTA.Clear()
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim SQL As New SqlClient.SqlCommand(QUERY, CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        CB.Items.Clear()
        LISTA.Clear()
        While LEC.Read
            LISTA.Add(LEC(0))
            CB.Items.Add(LEC(1))
        End While
        LEC.Close()
        SQL.Dispose()
        CONX.Close()
        Try
            CB.SelectedIndex = 0
            Return True
        Catch ex As Exception

        End Try
        Return False
    End Function
    Public Function LLENALISTBOX(ByRef LB As ListBox, ByRef LISTA As List(Of String), ByVal QUERY As String, ByVal CADENA As String) As Boolean
        LB.Items.Clear()
        LISTA.Clear()
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim SQL As New SqlClient.SqlCommand(QUERY, CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        LB.Items.Clear()
        LISTA.Clear()
        While LEC.Read
            LISTA.Add(LEC(0))
            LB.Items.Add(LEC(1))
        End While
        LEC.Close()
        SQL.Dispose()
        CONX.Close()
        Try
            LB.SelectedIndex = 0
            Return True
        Catch ex As Exception

        End Try
        Return False
    End Function
    Public Function LLENA2LISTAS(ByRef LISTA As List(Of String), ByRef LISTA2 As List(Of String), ByVal QUERY As String, ByVal CADENA As String) As Boolean
        LISTA.Clear()
        LISTA2.Clear()
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim SQL As New SqlClient.SqlCommand(QUERY, CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        LISTA.Clear()
        While LEC.Read
            LISTA.Add(LEC(0))
            LISTA2.Add(LEC(1))
        End While
        LEC.Close()
        SQL.Dispose()
        CONX.Close()
        Try
            Return True
        Catch ex As Exception

        End Try
        Return False
    End Function
    Public RESULTADORFC As String
    Public Function EsRFCValido(ByVal RFC As String) As Boolean
        RESULTADORFC = ""
        If PersonaFisica_RFC(RFC) AndAlso ValidarUltimaLetraRFC(RFC) AndAlso ValidarFechaRFC(RFC) Then
            Return True
        End If

        If General_RFC(RFC) AndAlso ValidarUltimaLetraRFC(RFC) AndAlso ValidarFechaRFC(RFC) Then
            Return True
        End If

        Return False
    End Function
    Private Function ValidarUltimaLetraRFC(ByVal RFC As String) As Boolean
        Try
            If RFC.EndsWith("A") OrElse RFC.EndsWith("0") OrElse RFC.EndsWith("1") OrElse RFC.EndsWith("2") OrElse RFC.EndsWith("3") OrElse RFC.EndsWith("4") OrElse RFC.EndsWith("5") OrElse RFC.EndsWith("6") OrElse RFC.EndsWith("7") OrElse RFC.EndsWith("8") OrElse RFC.EndsWith("9") Then
                Return True
            Else
                RESULTADORFC = "Formato de RFC Incorrecto, debe terminar en: A,0,1,2,3,4,5,6,7,8,9"
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine(String.Format("ExpRegulares.ValidarUltimaLetraRFC: {0}", ex.Message))
            RESULTADORFC = String.Format("ExpRegulares.PersonaFisica_RFC: {0}", ex.Message)
            Return False
        End Try
    End Function
    Private Function ValidarFechaRFC(ByVal RFC As String) As Boolean
        Dim fecha As String = String.Empty
        Dim esFecha As Boolean = False
        Dim resultado As DateTime

        fecha = RFC.Substring(RFC.Length - 9, 6)
        esFecha = DateTime.TryParseExact(fecha, "yyMMdd", Nothing, DateTimeStyles.None, resultado)
        RESULTADORFC = String.Format("FECHA INCORRECTA: " + RFC.Substring(RFC.Length - 9, 6))
        Return esFecha
    End Function
    Private Function PersonaFisica_RFC(ByVal RFC As String) As Boolean
        Try
            If RFC.Trim().Length = 13 OrElse RFC.Trim().Length = 12 Then
                Dim pattern As String = "^[A-Z&]{3,4}(\d{6})(([A-Z0-9]){3})?$"

                Dim RFCMatch As Match = Regex.Match(RFC, pattern)
                If RFC <> String.Empty Then
                    If Not RFCMatch.Success Then
                        RESULTADORFC = "Formato de RFC Incorrecto AAAA######NNN"
                        Return False
                    End If
                End If
            Else
                RESULTADORFC = "Longitud de RFC Incorrecta: (12) o (13)"
                Return False
            End If

            Return True
        Catch ex As Exception
            Console.WriteLine(String.Format("ExpRegulares.PersonaFisica_RFC: {0}", ex.Message))
            RESULTADORFC = String.Format("ExpRegulares.PersonaFisica_RFC: {0}", ex.Message)
            Return False
        End Try
    End Function
    Private Function General_RFC(ByVal RFC As String) As Boolean
        Try
            Dim pattern As String = "^([A-Z\s]{3,4})\d{6}(([A-Z\w]|[0-9]){3})$"
            Dim RFCMatch As Match = Regex.Match(RFC.Trim(), pattern)
            If RFC.Trim() <> String.Empty Then
                If Not RFCMatch.Success Then
                    RESULTADORFC = "Formato de RFC Incorrecto AAA######NNN"
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Console.WriteLine(String.Format("ExpRegulares.General_RFC: {0}", ex.Message))
            RESULTADORFC = String.Format("ExpRegulares.PersonaFisica_RFC: {0}", ex.Message)
            Return False
        End Try
    End Function
    Public Function LLENACOMBOBOX2LISTAS(ByRef CB As ComboBox, ByRef LISTA As List(Of String), ByRef LISTA2 As List(Of String), ByVal QUERY As String, ByVal CADENA As String) As Boolean
        CB.Items.Clear()
        LISTA.Clear()
        LISTA2.Clear()
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim SQL As New SqlClient.SqlCommand(QUERY, CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        CB.Items.Clear()
        LISTA.Clear()
        While LEC.Read
            LISTA.Add(LEC(0))
            LISTA2.Add(LEC(1))
            CB.Items.Add(LEC(2))
        End While
        LEC.Close()
        SQL.Dispose()
        CONX.Close()
        Try
            CB.SelectedIndex = 0
            Return True
        Catch ex As Exception

        End Try
        Return False
    End Function
    Public Function Encrypt(ByVal strText As String, Optional ByVal strEncrKey As String = "&%#@?,:*") As String
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Try
            Dim bykey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Strings.Left(strEncrKey, 8))
            Dim InputByteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(strText)
            Dim des As New DESCryptoServiceProvider
            Dim ms As New MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write)
            cs.Write(InputByteArray, 0, InputByteArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray())
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function Decrypt(ByVal strText As String, Optional ByVal sDecrKey As String = "&%#@?,:*") As String
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte
        Try
            Dim byKey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Strings.Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Return encoding.GetString(ms.ToArray())
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    Public Function LLENACOMBOBOX3LISTAS(ByRef CB As ComboBox, ByRef LISTA As List(Of String), ByRef LISTA2 As List(Of String), ByRef LISTA3 As List(Of String), ByVal QUERY As String, ByVal CADENA As String) As Boolean
        CB.Items.Clear()
        LISTA.Clear()
        LISTA2.Clear()
        LISTA3.Clear()
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim SQL As New SqlClient.SqlCommand(QUERY, CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        CB.Items.Clear()
        LISTA.Clear()
        While LEC.Read
            LISTA.Add(LEC(0))
            LISTA2.Add(LEC(1))
            LISTA3.Add(LEC(2))
            CB.Items.Add(LEC(3))
        End While
        LEC.Close()
        SQL.Dispose()
        CONX.Close()
        Try
            CB.SelectedIndex = 0
            Return True
        Catch ex As Exception

        End Try
        Return False
    End Function
    Public Function LLENACOMBOBOX2(ByRef CB As ComboBox, ByRef LISTA As List(Of String), ByVal QUERY As String, ByVal CADENA As String, ByVal TODOSLOS As String, ByVal CLAPRIM As String) As Boolean
        CB.Items.Clear()
        LISTA.Clear()
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim SQL As New SqlClient.SqlCommand(QUERY, CONX)
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        CB.Items.Clear()
        LISTA.Clear()
        CB.Items.Add(TODOSLOS)
        LISTA.Add(CLAPRIM)
        Dim CL As Integer
        CL = 0
        While LEC.Read
            CL += 1
            LISTA.Add(LEC(0))
            CB.Items.Add(LEC(1))
        End While
        LEC.Close()
        SQL.Dispose()
        CONX.Close()
        Try
            CB.SelectedIndex = 0
            If CL <= 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
        Return False
    End Function
    Public Function LLENACOMBOBOX2IF(ByRef CB As ComboBox, ByRef LISTA As List(Of String), ByVal QUERY As String, ByVal CADENA As String, ByVal TODOSLOS As String, ByVal CLAPRIM As String, ByVal INI As DateTime, ByVal FIN As DateTime) As Boolean
        CB.Items.Clear()
        LISTA.Clear()
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim SQL As New SqlClient.SqlCommand(QUERY, CONX)
        SQL.Parameters.Add("@INI", SqlDbType.DateTime).Value = INI
        SQL.Parameters.Add("@FIN", SqlDbType.DateTime).Value = FIN
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        CB.Items.Clear()
        LISTA.Clear()
        CB.Items.Add(TODOSLOS)
        LISTA.Add(CLAPRIM)
        While LEC.Read
            LISTA.Add(LEC(0))
            CB.Items.Add(LEC(1))
        End While
        LEC.Close()
        SQL.Dispose()
        CONX.Close()
        Try
            CB.SelectedIndex = 0
            Return True
        Catch ex As Exception

        End Try
        Return False
    End Function
    Public Function LLENATABLAIF(ByVal QUERY As String, ByVal CADENA As String, ByVal INI As DateTime, ByVal FIN As DateTime) As DataTable
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim DA As New SqlClient.SqlDataAdapter(QUERY, CONX)
        DA.SelectCommand.Parameters.Add("@INI", SqlDbType.DateTime).Value = INI
        DA.SelectCommand.Parameters.Add("@FIN", SqlDbType.DateTime).Value = FIN
        DA.SelectCommand.CommandTimeout = 600
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function
    Public Function LLENATABLA4F(ByVal QUERY As String, ByVal CADENA As String, ByVal INI As DateTime, ByVal FIN As DateTime, ByVal INI2 As DateTime, ByVal FIN2 As DateTime) As DataTable
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim DA As New SqlClient.SqlDataAdapter(QUERY, CONX)
        DA.SelectCommand.Parameters.Add("@INI", SqlDbType.DateTime).Value = INI
        DA.SelectCommand.Parameters.Add("@FIN", SqlDbType.DateTime).Value = FIN
        DA.SelectCommand.Parameters.Add("@INI2", SqlDbType.DateTime).Value = INI2
        DA.SelectCommand.Parameters.Add("@FIN2", SqlDbType.DateTime).Value = FIN2
        DA.SelectCommand.CommandTimeout = 300
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function
    Public Function LLENATABLA(ByVal QUERY As String, ByVal CADENA As String) As DataTable
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim DA As New SqlClient.SqlDataAdapter(QUERY, CONX)
        DA.SelectCommand.CommandTimeout = 300
        Dim DT As New DataTable
        DA.Fill(DT)
        CONX.Close()
        Return DT
    End Function

    Public Function SetDefPrinter(ByVal sNombreImpresora As String) As Boolean
        'Parámetro especifica nombre de impresora para poner por defecto.
        'La pongo por defecto y la quito.

        Dim WshNetwork As Object
        Dim pd As New PrintDocument
        WshNetwork = Microsoft.VisualBasic.CreateObject("WScript.Network")
        Try
            WshNetwork.SetDefaultPrinter(sNombreImpresora)
            pd.PrinterSettings.PrinterName = sNombreImpresora
            If pd.PrinterSettings.IsValid Then
                Return True
            Else
                WshNetwork.SetDefaultPrinter(sNombreImpresora)
                Return False
            End If
            'Catch exptd As Exception
            'WshNetwork.SetDefaultPrinter(sNombreImpresora)
            Return False
        Finally
            WshNetwork = Nothing
            pd = Nothing
        End Try
    End Function

    Private Function ImpresoraDefault() As String
        For i As Integer = 0 To PrinterSettings.InstalledPrinters.Count - 1
            Dim a As New PrinterSettings()
            a.PrinterName = PrinterSettings.InstalledPrinters(i).ToString()
            If a.IsDefaultPrinter Then
                Return a.PrinterName
            End If
        Next
        Return ""
    End Function
    Private Function ImpresoraParaSettings(ByVal NOMBREIMPRESORA As String) As String
        If NOMBREIMPRESORA = "" Then
            Return ""
        Else
            For i As Integer = 0 To PrinterSettings.InstalledPrinters.Count - 1 '' ciclo de todas las impresoras k tengo instalada en la pc
                Dim a As New PrinterSettings() ''' variable de propiedades de impresora
                a.PrinterName = PrinterSettings.InstalledPrinters(i).ToString() '' la propiedad de impresora PrinterName= a la impresoras instalada en la posicion i
                If a.PrinterName.ToUpper = NOMBREIMPRESORA.ToUpper Then '' si el nombre de la impresora instalada en la posicion i = a la impresora k le mando como parametro
                    Return PrinterSettings.InstalledPrinters(i).ToString() '' nombre de impresora=a la impresora instalada
                End If
            Next
            Dim TEMPNI
            Dim QUITA As Integer
            QUITA = NOMBREIMPRESORA.LastIndexOfAny("\")
            TEMPNI = NOMBREIMPRESORA.Substring(QUITA + 1)
            QUITA = NOMBREIMPRESORA.LastIndexOfAny("/")
            TEMPNI = TEMPNI.Substring(QUITA + 1)
            For i As Integer = 0 To PrinterSettings.InstalledPrinters.Count - 1
                Dim a As New PrinterSettings()
                a.PrinterName = PrinterSettings.InstalledPrinters(i).ToString()
                If a.PrinterName.ToUpper = TEMPNI.ToUpper Then
                    Return PrinterSettings.InstalledPrinters(i).ToString()
                End If
            Next
            Return ""
        End If
    End Function
    Public Function IMPRIMIRREPORTE(ByVal REP As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal DT As DataTable, ByVal NCOPIAS As Integer, ByVal NombreImpresora As String) As Boolean
        Try
            Dim ImpSettings As String
            If NombreImpresora = "" Then
                ImpSettings = ImpresoraParaSettings(ImpresoraDefault)
                If ImpSettings = "" Then
                    ImpSettings = ImpresoraParaSettings(ImpresoraDefault)
                Else
                    Try
                        REP.PrintOptions.PrinterName = ImpSettings
                    Catch ex As Exception
                        MsgBox("Revisar Impresora: " + ImpSettings)
                    End Try
                End If
            Else
                ImpSettings = ImpresoraParaSettings(NombreImpresora)
                If ImpSettings = "" Then
                    ImpSettings = ImpresoraParaSettings(ImpresoraDefault)
                Else
                    Try
                        REP.PrintOptions.PrinterName = ImpSettings
                    Catch ex As Exception
                        MsgBox("Revisar Impresora: " + ImpSettings)
                    End Try
                End If
            End If
            REP.SetDataSource(DT)
            If NCOPIAS <= 0 Then
                NCOPIAS = 1
            End If
            For X = 1 To NCOPIAS
                REP.PrintToPrinter(1, False, 0, 0)
            Next

            REP.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return False
    End Function

    Public Function MOSTRARREPORTE(ByVal REP As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal NombreVentana As String, ByVal DT As DataTable, ByVal NombreImpresora As String) As Boolean
        Dim FRM As New System.Windows.Forms.Form
        Dim CRV As New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Dim ImpSettings As String
        If NombreImpresora = "" Then
        Else
            ImpSettings = IMPRESORAPARASETTINGS(NombreImpresora)
            If ImpSettings = "" Then
            Else
                REP.PrintOptions.PrinterName = ImpSettings
            End If
        End If
        REP.SetDataSource(DT)
        CRV.ReportSource = REP
        CRV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        CRV.Dock = System.Windows.Forms.DockStyle.Fill
        FRM.Controls.Add(CRV)
        FRM.Icon = frmPrincipal.Icon
        FRM.Text = NombreVentana
        FRM.WindowState = FormWindowState.Maximized
        FRM.ShowDialog()
        REP.Dispose()
        CRV.Dispose()
        FRM.Dispose()
        Return False
    End Function
    Public Function MOSTRARMULTIREPORTE(ByRef REPORTECONTENEDOR As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal NombreVentana As String, ByVal CANTIDADSUBREPORTES As Integer, ByVal LDT As List(Of DataTable), ByVal NombreImpresora As String) As Boolean
        Dim FRM As New System.Windows.Forms.Form
        Dim CRV As New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Dim ImpSettings As String
        If NombreImpresora = "" Then
        Else
            ImpSettings = IMPRESORAPARASETTINGS(NombreImpresora)
            If ImpSettings = "" Then
            Else
                REPORTECONTENEDOR.PrintOptions.PrinterName = ImpSettings
            End If
        End If
        Dim X As Integer
        For X = 0 To CANTIDADSUBREPORTES - 1
            REPORTECONTENEDOR.Subreports(X).SetDataSource(LDT(X))
        Next
        CRV.ReportSource = REPORTECONTENEDOR
        CRV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        CRV.Dock = System.Windows.Forms.DockStyle.Fill
        FRM.Controls.Add(CRV)
        FRM.Text = NombreVentana
        FRM.WindowState = FormWindowState.Maximized
        FRM.ShowDialog()
        REPORTECONTENEDOR.Dispose()
        CRV.Dispose()
        FRM.Dispose()
        Return False
    End Function


    Public Function GUARDARREPORTE(ByVal REP As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal DT As DataTable, ByVal TIPOEXPORTAR As CrystalDecisions.Shared.ExportFormatType, ByVal ProgramaDefault As String, ByVal ExtensionArchivo As String, ByVal MensajePregunta As String, ByVal NombreArchivo As String, ByVal NombreImpresora As String) As Boolean
        Dim xyz As Short
        xyz = MessageBox.Show(MensajePregunta, "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If xyz <> 6 Then
            Exit Function
        End If
        Dim SFD As New System.Windows.Forms.SaveFileDialog
        SFD.FileName = NombreArchivo
        SFD.Filter = "Archivos de " + ProgramaDefault + " (*." + ExtensionArchivo + ")|*." + ExtensionArchivo + "|" + Chr(34) + "All files (*.*)|*.*;"
        If SFD.ShowDialog = DialogResult.OK Then
            Try
                If System.IO.File.Exists(SFD.FileName) = True Then
                    System.IO.File.Delete(SFD.FileName)
                End If
            Catch ex As Exception
                MessageBox.Show("La informacion No se puede Guardar, Archivo en Uso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
                Exit Function
            End Try
            Dim ImpSettings As String
            If NombreImpresora = "" Then
            Else
                ImpSettings = IMPRESORAPARASETTINGS(NombreImpresora)
                If ImpSettings = "" Then
                Else
                    REP.PrintOptions.PrinterName = ImpSettings
                End If
            End If
            REP.SetDataSource(DT)
            REP.ExportToDisk(TIPOEXPORTAR, SFD.FileName)
            MessageBox.Show("El archivo ha sido Guardado Exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If

        Return False
    End Function
    Public Sub ENVIARMAIL(ByVal LDESTINATARIOS As List(Of String), ByVal ArchivoAdjunto As String, ByVal SUBJECT As String, ByVal BODY As String)
        Dim MSG As New MailMessage
        Dim SMTP As New SmtpClient("smtp.gmail.com", 587)
        'Dim disposition As ContentDisposition
        'disposition = ATT.ContentDisposition
        MSG.From = New MailAddress("correosadjuntos@gmail.com", "correosadjuntos@gmail.com", System.Text.Encoding.UTF8)
        Dim X As Integer
        For X = 0 To LDESTINATARIOS.Count - 1
            MSG.To.Add(LDESTINATARIOS(X))
        Next

        SMTP.Credentials = New System.Net.NetworkCredential("correosadjuntos@gmail.com", "abretesesamo")
        SMTP.Host = "smtp.gmail.com"
        SMTP.EnableSsl = True
        SMTP.Timeout = 300

        If ArchivoAdjunto = "" Then
        Else
            If System.IO.File.Exists(ArchivoAdjunto) = True Then
                Dim ATT As New Attachment(ArchivoAdjunto)
                Try
                    MSG.Attachments.Add(ATT)
                Catch ex As SmtpException
                    Exit Sub
                End Try
            End If
        End If
        MSG.Subject = SUBJECT
        MSG.SubjectEncoding = System.Text.Encoding.UTF8
        MSG.Body = BODY
        Try
            SMTP.Send(MSG)
            MessageBox.Show("Mail Enviado Con Exito ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        Catch ex As SmtpException
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Public Function SUMARTIEMPOAFECHA(ByVal FECHA As DateTime, ByVal HORA As DateTime) As DateTime
        Dim DT As DateTime
        DT = FECHA.Date
        DT = DT.AddHours(HORA.Hour)
        DT = DT.AddMinutes(HORA.Minute)
        DT = DT.AddSeconds(HORA.Second)
        Return DT
    End Function
    Public Function Image2Bytes(ByVal img As Image) As Byte()
        Dim sTemp As String = Path.GetTempFileName()
        Dim fs As New FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite)
        img.Save(fs, System.Drawing.Imaging.ImageFormat.Png)
        fs.Position = 0
        '
        Dim imgLength As Integer = CInt(fs.Length)
        Dim bytes(0 To imgLength - 1) As Byte
        fs.Read(bytes, 0, imgLength)
        fs.Close()
        Try
            System.IO.File.Delete(sTemp)
        Catch ex As Exception
            ''MessageBox.Show(ex.Message)
        End Try
        Return bytes
    End Function

    Public Function Bytes2Image(ByVal bytes() As Byte) As Image
        If bytes Is Nothing Then Return Nothing
        '
        Dim ms As New MemoryStream(bytes)
        Dim bm As Bitmap = Nothing
        Try
            bm = New Bitmap(ms)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
        End Try
        Return bm
    End Function
    Public Sub ABRIRCAJON()
        'Try
        '    Shell("c:\caja.exe", AppWinStyle.Hide)
        '    Shell("c:\caja.exe", AppWinStyle.Hide)
        'Catch ex As Exception
        '    'MessageBox.Show("Le Falta el componente Caja.EXE, favor de Comunicarse con DeIP", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'End Try
        'Try
        '    Shell("c:\caja.exe", AppWinStyle.Hide)
        '    Shell("c:\caja.exe", AppWinStyle.Hide)
        'Catch ex As Exception
        '    'MessageBox.Show("Le Falta el componente Caja.EXE, favor de Comunicarse con DeIP", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'End Try
    End Sub
    Public Function LLENACLB(ByRef CLB As CheckedListBox, ByRef LISTA As List(Of String), ByVal QUERY As String, ByVal CADENA As String) As Boolean
        CLB.Items.Clear()
        LISTA.Clear()
        Dim CONX As New SqlClient.SqlConnection(CADENA)
        CONX.Open()
        Dim SQL As New SqlClient.SqlCommand(QUERY, CONX)
        SQL.CommandTimeout = 600
        Dim LEC As SqlClient.SqlDataReader
        LEC = SQL.ExecuteReader
        CLB.Items.Clear()
        LISTA.Clear()
        While LEC.Read
            LISTA.Add(LEC(0))
            CLB.Items.Add(LEC(1))
        End While
        LEC.Close()
        SQL.Dispose()
        CONX.Close()
        Try
            CLB.SelectedIndex = 0
            Return True
        Catch ex As Exception
        End Try
        Return False
    End Function

    Public Sub DataTableToExcel(ByVal pDataTable As DataTable)

        Dim vFileName As String = Path.GetTempFileName()

        FileOpen(1, vFileName, OpenMode.Output)

        Dim sb As String
        Dim dc As DataColumn
        For Each dc In pDataTable.Columns
            sb &= dc.Caption & Microsoft.VisualBasic.ControlChars.Tab
        Next
        PrintLine(1, sb)

        Dim i As Integer = 0
        Dim dr As DataRow
        For Each dr In pDataTable.Rows
            i = 0 : sb = ""
            For Each dc In pDataTable.Columns
                If Not IsDBNull(dr(i)) Then
                    sb &= CStr(dr(i)) & Microsoft.VisualBasic.ControlChars.Tab
                Else
                    sb &= Microsoft.VisualBasic.ControlChars.Tab
                End If
                i += 1
            Next

            PrintLine(1, sb)
        Next

        FileClose(1)
        TextToExcel(vFileName)

    End Sub

    Public Sub TextToExcel(ByVal pFileName As String)

        Dim vFormato As Excel.XlRangeAutoFormat

        Dim vCultura As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture()

        'Es importante definirle la cultura al sistema
        'ya que podria generar errores
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        Dim Exc As Excel.Application = New Excel.Application
        Exc.Workbooks.OpenText(pFileName, , , , Excel.XlTextQualifier.xlTextQualifierNone, , True)

        Dim Wb As Excel.Workbook = Exc.ActiveWorkbook
        Dim Ws As Excel.Worksheet = Wb.ActiveSheet

        vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatSimple
        'En el ejemplo vienen otros formatos posibles

        Ws.Range(Ws.Cells(1, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).AutoFormat(vFormato)

        pFileName = Path.GetTempFileName.Replace("tmp", "xls")
        File.Delete(pFileName)

        Exc.ActiveWorkbook.SaveAs(pFileName, Excel.XlTextQualifier.xlTextQualifierNone - 1)

        Exc.Quit()

        Ws = Nothing
        Wb = Nothing
        Exc = Nothing

        GC.Collect()
        'If valor > -1 Then
        Dim p As System.Diagnostics.Process = New System.Diagnostics.Process
        p.EnableRaisingEvents = False
        p.Start("Excel.exe", pFileName)
        ' End If
        System.Threading.Thread.CurrentThread.CurrentCulture = vCultura
    End Sub

    Public Function GridAExcel(ByVal ElGrid As DataGridView) As Boolean
        'Creamos las variables
        Dim exApp As New Microsoft.Office.Interop.Excel.Application
        Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
        Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
        Try
            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()
            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = ElGrid.ColumnCount
            Dim NRow As Integer = ElGrid.RowCount
            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exHoja.Cells.Item(1, i) = ElGrid.Columns(i - 1).Name.ToString
                exHoja.Cells.Item(1, i).HorizontalAlignment = 3
            Next
            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    exHoja.Cells.Item(Fila + 2, Col + 1) = ElGrid.Rows(Fila).Cells(Col).Value.ToString

                Next
            Next
            'Titulo en negrita, Alineado al centro y que el tamaño de la columna seajuste al texto
            exHoja.Name = "Navysoluciones"
            exHoja.Rows.Item(1).Font.Bold = 1
            exHoja.Rows.Item(1).HorizontalAlignment = 3
            exHoja.Columns.AutoFit()
            'Aplicación visible
            exApp.Application.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMinimized
            exApp.Application.Visible = True
            exHoja = Nothing
            exLibro = Nothing
            exApp = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel " + ex.Message)
            Return False
        End Try
        Return True
    End Function
    Public Function ExportarExcel(ByVal DT As DataTable, ByVal NombreArchivo As String, ByVal Abrirlo As Boolean) As Boolean
        Dim SFD As New System.Windows.Forms.SaveFileDialog
        SFD.FileName = NombreArchivo
        SFD.Filter = "Archivos de Excel (*.xls)|*.xls|" + Chr(34) + "All files (*.*)|*.*;"
        If SFD.ShowDialog = DialogResult.OK Then
            Try
                If System.IO.File.Exists(SFD.FileName) = True Then
                    System.IO.File.Delete(SFD.FileName)
                End If
                'Dim xls As New Microsoft.Office.Interop.Excel.
                Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
                Dim wBook As Microsoft.Office.Interop.Excel.Workbook
                Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

                wBook = excel.Workbooks.Add()
                wSheet = wBook.ActiveSheet()

                Dim dc As System.Data.DataColumn
                Dim dr As System.Data.DataRow
                Dim colIndex As Integer = 0
                Dim rowIndex As Integer = 0

                For Each dc In DT.Columns
                    colIndex = colIndex + 1
                    excel.Cells(1, colIndex) = dc.ColumnName
                    'excel.Columns.FormatConditions = TextBox
                Next

                For Each dr In DT.Rows
                    rowIndex = rowIndex + 1
                    colIndex = 0
                    For Each dc In DT.Columns
                        colIndex = colIndex + 1
                        excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)

                    Next
                Next

                'Titulo en negrita, Alineado al centro y que el tamaño de la columna seajuste al texto
                wSheet.Name = "Navysoluciones"
                wSheet.Rows.Item(1).Font.Bold = 1
                wSheet.Rows.Item(1).HorizontalAlignment = 3
                wSheet.Columns.AutoFit()
                wSheet.Columns.AutoFit()
                Dim strFileName As String = SFD.FileName
                Dim blnFileOpen As Boolean = False
                Try
                    Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
                    fileTemp.Close()
                Catch ex As Exception
                    blnFileOpen = False
                End Try

                If System.IO.File.Exists(strFileName) Then
                    System.IO.File.Delete(strFileName)
                End If

                wBook.SaveAs(strFileName)
                excel.Workbooks.Open(strFileName)
                excel.Visible = True
                If Abrirlo Then
                    excel.Workbooks.Open(strFileName)
                    excel.Visible = True
                End If
            Catch ex As Exception
                MessageBox.Show("La información No se puede Guardar, Archivo en Uso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
                Exit Function
            End Try
        End If

    End Function
    Public Function ExportarExcelMDT(ByVal LDT As List(Of DataTable), ByVal NombreArchivo As String, ByVal Abrirlo As Boolean) As Boolean
        Dim SFD As New System.Windows.Forms.SaveFileDialog
        SFD.FileName = NombreArchivo
        SFD.Filter = "Archivos de Excel (*.xls)|*.xls|" + Chr(34) + "All files (*.*)|*.*;"
        If SFD.ShowDialog = DialogResult.OK Then
            Try
                If System.IO.File.Exists(SFD.FileName) = True Then
                    System.IO.File.Delete(SFD.FileName)
                End If
                'Dim xls As New Microsoft.Office.Interop.Excel.
                Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
                Dim wBook As Microsoft.Office.Interop.Excel.Workbook
                Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

                wBook = excel.Workbooks.Add()
                wSheet = wBook.ActiveSheet()
                Dim DT As DataTable
                Dim dc As System.Data.DataColumn
                Dim dr As System.Data.DataRow
                Dim colIndex As Integer = 0
                Dim rowIndex As Integer = 0
                For Each DT In LDT
                    For Each dc In DT.Columns
                        colIndex = colIndex + 1
                        excel.Cells(1, colIndex) = dc.ColumnName
                    Next

                    For Each dr In DT.Rows
                        rowIndex = rowIndex + 1
                        colIndex = 0
                        For Each dc In DT.Columns
                            colIndex = colIndex + 1
                            excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
                        Next
                    Next

                    wSheet.Columns.AutoFit()



                Next
                Dim strFileName As String = SFD.FileName
                Dim blnFileOpen As Boolean = False
                Try
                    Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
                    fileTemp.Close()
                Catch ex As Exception
                    blnFileOpen = False
                End Try

                If System.IO.File.Exists(strFileName) Then
                    System.IO.File.Delete(strFileName)
                End If
                wBook.SaveAs(strFileName)
                If Abrirlo Then
                    excel.Workbooks.Open(strFileName)
                    excel.Visible = True
                End If
            Catch ex As Exception
                MessageBox.Show("La información No se puede Guardar, Archivo en Uso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
                Exit Function
            End Try
        End If

    End Function
    Public Function VALIDACANTIDAD(ByVal TEXTO As String, ByVal OPC As Integer) As Boolean
        Dim NUM As Double
        Dim REG As Boolean
        REG = False
        Try
            NUM = CType(TEXTO, Double)
        Catch ex As Exception
            Return False
        End Try
        If OPC = 1 Then  '' SIN DECIMALES
            If (NUM Mod 0.01) <> 0 Then
                Return False
            End If
            Return True
        End If
        If OPC = 2 Then '' NUMERO DEBE SER MAYOR A CERO
            If NUM <= 0 Then
                Return False
            End If
        End If
        Return True
    End Function
    Public Function VERIFICASELECCIONCB(ByRef CONTROLES As System.Windows.Forms.Control.ControlCollection) As Boolean
        Dim ALGO As String
        For Each c As Control In CONTROLES
            If Not c.Controls Is Nothing AndAlso c.Controls.Count > 0 Then
            ElseIf c.GetType().Equals(GetType(ComboBox)) Then
                ALGO = c.Name
                Try
                    If CType(c.Tag, Boolean) And CType(c, ComboBox).SelectedIndex = 0 Then
                        Return False
                    End If
                Catch ex As Exception

                End Try

            End If
        Next
        Return True
    End Function
End Module
