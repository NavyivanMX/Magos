Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Mail
Public Class clsActualizacion
    Private Declare Function GetShortPathName Lib "kernel32" Alias "GetShortPathNameA" (ByVal lpszLongPath As String, ByVal lpszShortPath As String, ByVal lBuffer As Long) As Long
    Dim WinRar As String, WinZip As String
    Dim CONZ As New SqlClient.SqlConnection
    Dim CADENA As String
    Public Function MACaddress() As String
        Dim nets As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
        Dim mac As String = ""
        For Each adapter As NetworkInterface In nets
            Dim properties As IPInterfaceProperties = adapter.GetIPProperties()
            Dim address As PhysicalAddress = adapter.GetPhysicalAddress()
            Dim bytes As Byte() = address.GetAddressBytes()
            For i As Integer = 0 To bytes.Length - 1
                mac += bytes(i).ToString("X2")
                If i <> bytes.Length - 1 Then
                    mac += "-"
                End If
            Next
        Next
        'MessageBox.Show(mac, "MAC Adresses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return mac
    End Function
    Public Sub Ejecutar3(ByVal COMANDO As String)
        Dim objEscritor As StreamWriter
        If System.IO.File.Exists("C:\ARCHIVO.BAT") = True Then
            System.IO.File.Delete("C:\ARCHIVO.BAT")
        End If
        objEscritor = File.AppendText("C:\ARCHIVO.BAT")
        objEscritor.WriteLine(COMANDO)
        objEscritor.Close()
        Shell("C:\ARCHIVO.BAT", AppWinStyle.NormalFocus, True, 10000)

        If System.IO.File.Exists("C:\ARCHIVO.BAT") = True Then
            System.IO.File.Delete("C:\ARCHIVO.BAT")
        End If
    End Sub
    Public Sub Ejecutar2(ByVal COMANDO As String)
        Shell(COMANDO, AppWinStyle.NormalFocus)
    End Sub
    Public Function DNSLookup(ByVal domain As String) As String
        Try
            'performs the DNS lookup
            Dim he As IPHostEntry = Dns.GetHostEntry(domain)
            Dim ip_addrs As IPAddress() = he.AddressList
            For Each ip As IPAddress In ip_addrs
                Return ip.ToString()
            Next ip
        Catch ex As System.Exception
            MessageBox.Show(ex.Message)
        End Try
        Return ""
    End Function

    Public Sub Ejecutar(ByVal COMANDO As String)
        Dim objEscritor As StreamWriter
        If System.IO.File.Exists("C:\ARCHIVO.BAT") = True Then
            System.IO.File.Delete("C:\ARCHIVO.BAT")
        End If
        objEscritor = File.AppendText("C:\ARCHIVO.BAT")
        objEscritor.WriteLine(COMANDO)
        objEscritor.Close()
        Shell("C:\ARCHIVO.BAT", AppWinStyle.NormalFocus, True)

        If System.IO.File.Exists("C:\ARCHIVO.BAT") = True Then
            System.IO.File.Delete("C:\ARCHIVO.BAT")
        End If
    End Sub
    Public Function NombreEquipo() As String
        'MessageBox.Show(SystemInformation.ComputerName, "Nombre del Equipo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return SystemInformation.ComputerName
    End Function
    Public Function IPEquipo() As String
        Dim REG As String
        REG = "No Asignado"
        Dim DIRECCIONES() As IPAddress = Dns.GetHostAddresses(Dns.GetHostName())
        For Each IP As IPAddress In DIRECCIONES
            'Console.WriteLine(IP.ToString())
            REG = IP.ToString
        Next
        'MessageBox.Show(REG, "Direccion Ip", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return REG
    End Function
    Public Sub PONERMAC()
        Dim CONY As New SqlClient.SqlConnection()
        Dim CADE As String
        CADE = "Data Source=" + frmPrincipal.IP + ",1433;Network Library=DBMSSOCN;Initial Catalog=INVENTARIOSDEIP;User ID=dbaadmin;Password=masterkey"
        CONY.ConnectionString = CADE
        Try
            CONY.Open()
            Dim ACTUALIZAR, CORREO As Boolean
            CORREO = False
            CORREO = False
            Dim SQL As New SqlClient.SqlCommand("SELECT CLAVE,MAC FROM EQUIPOS WHERE CLAVE='" + NombreEquipo() + "'", CONY)
            Dim LEC As SqlClient.SqlDataReader
            LEC = SQL.ExecuteReader
            If LEC.Read Then
                If LEC(1) <> MACaddress() Then
                    ACTUALIZAR = True
                End If
            Else
                CORREO = True
            End If
            LEC.Close()
            If ACTUALIZAR Then
                SQL.CommandText = "UPDATE EQUIPOS SET MAC='" + MACaddress() + "' WHERE CLAVE='" + NombreEquipo() + "'"
                SQL.ExecuteNonQuery()
            End If
            SQL.Dispose()
            CONY.Close()
            CONY.Dispose()
            If CORREO Then
                Try
                    ENVIARMAIL("FAVOR DE DAR DE ALTA AL EQUIPO: " + NombreEquipo() + " , CON LA DIRECCION MAC: " + MACaddress() + " Usuario: " + frmPrincipal.Usuario + " Sistema: " + frmPrincipal.Sistema, "DAR DE ALTA AL EQUIPO")
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub ENVIARMAIL(ByVal BODY As String, ByVal SUBJ As String)
        'Dim ATT As New System.Net.Mail.Attachment("C:\DEIP.ZIP")
        Try
            Dim MSG As New System.Net.Mail.MailMessage
            MSG.From = New System.Net.Mail.MailAddress("narcisomoditelas@gmail.com")
            MSG.To.Add("narciso_ivan@hotmail.com,omardiaz_trejo@hotmail.com,jaimelopezcamargo@hotmail.com,jorge_deip@hotmail.com")
            'MSG.Attachments.Add(ATT)
            'MSG.To.Add("juanochoamaciel@hotmail.com")
            'MSG.To.Add("alejandroct_21@hotmail.com")
            'MSG.To.Add("panchito_s@hotmail.com")
            'MSG.To.Add("narcisomoditelas@gmail.com")
            'MSG.To.Add("omardiazmoditelas@gmail.com")
            'MSG.SubjectEncoding = System.Text.Encoding.UTF8
            MSG.Subject = SUBJ
            MSG.Body = BODY
            MSG.IsBodyHtml = True
            Dim SMTP As New System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
            SMTP.Credentials = New System.Net.NetworkCredential("narcisomoditelas@gmail.com", "uncoantraseña")
            SMTP.EnableSsl = True
            SMTP.Send(MSG)
            MessageBox.Show("Mensaje Enviado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try

    End Sub
    Public Sub COMPROBAR()
        Try
            CADENA = frmPrincipal.CadenaConexion
            CONZ.ConnectionString = CADENA
            CONZ.Open()
            Dim DA As New SqlClient.SqlDataAdapter("SELECT A.UBICACION,A.DESCARGA,A.NOMBRE,A.FORZOSO,A.REEMPLAZAR,A.PREGUNTAR FROM ACTUALIZACIONES A where A.ACTIVO=1 AND A.FORZOSO=1", CONZ)
            DA.SelectCommand.CommandTimeout = 300
            Dim DT As New DataTable
            DA.Fill(DT)
            CONZ.Close()
            Dim X As Integer
            Dim xyz As Short
            Dim PREGUNTAR As Boolean
            Dim FORZOSO As Boolean
            Dim REEMPLAZAR As Boolean
            For X = 0 To DT.Rows.Count - 1
                FORZOSO = CType(DT.Rows(X).Item(3), Boolean)
                REEMPLAZAR = CType(DT.Rows(X).Item(4), Boolean)
                PREGUNTAR = CType(DT.Rows(X).Item(5), Boolean)
                If PREGUNTAR Then
                    xyz = MessageBox.Show("¿Desea Descargar la Actualización " + DT.Rows(X).Item(2) + "?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If xyz <> 6 Then
                    Else
                        DESCARGAR(DT.Rows(X).Item(1).ToString, DT.Rows(X).Item(0).ToString, DT.Rows(X).Item(2).ToString)
                    End If
                End If

                If FORZOSO Then
                    If Not VERIFICAUBICACION(DT.Rows(X).Item(0).ToString) Then
                        DESCARGAR(DT.Rows(X).Item(1).ToString, DT.Rows(X).Item(0).ToString, DT.Rows(X).Item(2).ToString)
                    End If
                End If
                If REEMPLAZAR Then
                    If Not VERIFICAUBICACION(DT.Rows(X).Item(0).ToString) Then
                        DESCARGAR(DT.Rows(X).Item(1).ToString, DT.Rows(X).Item(0).ToString, DT.Rows(X).Item(2).ToString)
                    Else
                        Try
                            System.IO.File.Delete(DT.Rows(X).Item(0).ToString)
                            DESCARGAR(DT.Rows(X).Item(1).ToString, DT.Rows(X).Item(0).ToString, DT.Rows(X).Item(2).ToString)
                        Catch ex As Exception

                        End Try
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub REALIZAREJECUCIONES()
        Try
            CADENA = frmPrincipal.CadenaConexion
            CONZ.ConnectionString = CADENA
            CONZ.Open()
            Dim DA As New SqlClient.SqlDataAdapter("SELECT NOMBRE,EJECUCION,PREGUNTAR,MOSTRAR,FORZOSO FROM EJECUCIONES WHERE ACTIVO=1", CONZ)
            DA.SelectCommand.CommandTimeout = 300
            Dim DT As New DataTable
            DA.Fill(DT)
            CONZ.Close()
            Dim X As Integer
            Dim xyz As Short
            Dim PREGUNTAR As Boolean
            Dim FORZOSO As Boolean
            Dim MOSTRAR As Boolean
            For X = 0 To DT.Rows.Count - 1
                FORZOSO = CType(DT.Rows(X).Item(4), Boolean)
                MOSTRAR = CType(DT.Rows(X).Item(3), Boolean)
                PREGUNTAR = CType(DT.Rows(X).Item(2), Boolean)
                If PREGUNTAR Then
                    xyz = MessageBox.Show("¿Desea Ejecutar la Instrucción " + DT.Rows(X).Item(0) + "?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If xyz <> 6 Then
                    Else
                        Ejecutar3(DT.Rows(X).Item(1).ToString)
                    End If
                End If

                If FORZOSO Then
                    Ejecutar3(DT.Rows(X).Item(1).ToString)
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub DESCOMPRIMIR(ByVal ARCHIVO As String, ByVal RUTA As String)
        'Dim RutaWinZip As String
        'RutaWinZip = "C:\Archivos de programa\WinZip\WZUNZIP.EXE"
        'System.Diagnostics.Process.Start(sender.text)
        Dim MiObjeto As Object, Ret As String
        MiObjeto = CreateObject("Wscript.Shell")
        Try
            Ret = MiObjeto.RegRead("HKEY_CLASSES_ROOT\WinRAR\shell\open\command\")
            WinRar = Left(Ret, InStrRev(Ret, " "))
        Catch ex As Exception

        End Try
        Try
            Ret = MiObjeto.RegRead("HKEY_CLASSES_ROOT\WinZip\shell\open\command\")
            WinZip = Left(Ret, InStrRev(Ret, " "))
        Catch ex As Exception

        End Try

        MiObjeto = Nothing

        If WinRar = "" And WinZip = "" Then
            MsgBox("No se encuentra instalado ni WinZip ni WinRar :(")
            Exit Sub
        End If
        'WinRar = WinRar.Trim(Chr(34))
        'WinRar = WinRar.Substring(0, WinRar.Length - 2)
        Dim FullPathZip As String, FolderToDescompress As String
        'Ruta existente del archivo comprimirdo
        'FullPathZip = "C:\DeiP.rar"
        FullPathZip = ARCHIVO
        FolderToDescompress = RUTA
        If System.IO.File.Exists(ARCHIVO) = True Then
        Else
            Exit Sub
        End If
        'carpeta existente que se va a descomprimir el zip
        'FolderToDescompress = "c:/"

        'Dim SF As String
        'SF = " -d -o -yo " + FullPathZip + " " + FullPathZip.Substring(0, FullPathZip.LastIndexOf("\") + 1)
        'System.Diagnostics.Process.Start(WinRar, "e" + FullPathZip + " " + FullPathZip.Substring(0, FullPathZip.LastIndexOf("\") + 1))
        'System.Diagnostics.Process.Start(WinRar, WinRar + "eC:\DeiP.zip C:\")
        'MessageBox.Show(WinRar)
        Ejecutar(WinRar + " e " + FullPathZip + " " + FolderToDescompress)
    End Sub
    'Public Sub Decompress(ByVal fi As FileInfo)
    '    ' Get the stream of the source file.
    '    Using inFile As FileStream = fi.OpenRead()
    '        ' Get orignial file extension, for example "doc" from report.doc.gz.
    '        Dim curFile As String = fi.FullName
    '        Dim origName = curFile.Remove(curFile.Length - fi.Extension.Length)

    '        ' Create the decompressed file.
    '        Using outFile As FileStream = File.Create(origName)
    '            Using Decompress As GZipStream = New GZipStream(inFile, _
    '             CompressionMode.Decompress)

    '                ' Copy the decompression stream 
    '                ' into the output file.
    '                Decompress.CopyTo(outFile)

    '                Console.WriteLine("Decompressed: {0}", fi.Name)

    '            End Using
    '        End Using
    '    End Using
    'End Sub
    Public Function VERIFICAUBICACION(ByVal SFD As String) As Boolean
        If System.IO.File.Exists(SFD) = True Then
            Return True
        End If
        Return False
    End Function
    Public Sub DESCARGAR(ByVal LINK As String, ByVal UBI As String, ByVal NOM As String)
        Try
            My.Computer.Network.DownloadFile(LINK, UBI)
            MessageBox.Show("Actualizacion " + NOM + " Descargada Exitosamente", "Felicidades", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub DESCARGAR2(ByVal LINK As String, ByVal UBI As String, ByVal NOM As String)
        Try
            My.Computer.Network.DownloadFile(LINK, UBI)
            MessageBox.Show("Actualizacion " + NOM + " Descargada Exitosamente", "Felicidades", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Archivo (" + NOM + ") NO Encontrado para Descarga: " + LINK, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class
