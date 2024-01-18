Imports System.Threading

Public Class frmGenerarBotones
    Dim CLAGRU As New List(Of String)
    Dim RUTAIMAGEN As String
    Dim X As Integer
    Dim Alto, Ancho As Integer
    Private Sub frmGenerarBotones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmPrincipal.Icon
        txtRutaXML.Text = "C:\FOTOSMAGOS\"
        LLENACOMBOBOX2(CBGRU, CLAGRU, "SELECT CLAVE,NOMBRE FROM GRUPOTOUCH WHERE CLAVE<>0 ORDER BY NOMBRE", frmPrincipal.CadenaConexion, "Todos los Grupos", "")
        TXTFUENTE.Text = LBLIMG.Font.ToString
        NUDH.Value = LBLIMG.Height
        NUDW.Value = LBLIMG.Width
    End Sub
    Private Sub CARGADATOS()
        Dim QUERY As String
        QUERY = "SELECT CLAVE,NOMBRECORTO,PRECIO=0.00,IMG='',CONVERT(BIT,0)MARCAR FROM PRODUCTOS"
        If CBGRU.SelectedIndex <> 0 Then
            QUERY += " AND GRUPOTOUCH='" + CLAGRU(CBGRU.SelectedIndex) + "'"
        End If
        QUERY += " ORDER BY NOMBRECORTO"
        DGV.DataSource = LLENATABLA(QUERY, frmPrincipal.CadenaConexion)
        DGV.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGV.Columns(0).ReadOnly = True
        DGV.Columns(1).ReadOnly = True
        DGV.Columns(2).ReadOnly = True
    End Sub

    Public Function ThumbnailCallback() As Boolean
        Return False
    End Function
    'Private Sub RECTANGULO(ByVal NOMBREPLATILLO As String)
    '    'Captura solo un parte de la pantalla.
    '    'Crea un rectangulo y objeto bitmap.
    '    LBLIMG.Text = NOMBREPLATILLO


    '    Dim oBounds As Rectangle
    '    Dim oScreenShot As System.Drawing.Bitmap
    '    'Crea un objeto garfico.
    '    Dim oGraph As Graphics

    '    'Obtiene el rectangulo de la etiqueta que tiene los datos.
    '    oBounds = LBLIMG.Bounds

    '    'Obtiene la posición del área a capturar. Punto superior izquierdo. Utiliza cajas numericas para ajustar bien la captura.
    '    Dim iX As Integer = Me.Left + 538 + 4  'se suma 3 por el borde la ventana y 1 por el birdo de la label.
    '    Dim iY As Integer = Me.Top + 397 + 26  'se suma 25 + 1 pro la barra de titulo que mide 25 y 1 por el marco de la label.

    '    'Crea un nuevo bitmap con los datos del rectangulo.
    '    oScreenShot = New System.Drawing.Bitmap(oBounds.Width, oBounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)


    '    'Asigna el bitmap a graph, como lienso para cargar la imagen.
    '    oGraph = Graphics.FromImage(oScreenShot)
    '    oGraph.CopyFromScreen(iX, iY, 0, 0, oBounds.Size, CopyPixelOperation.SourceCopy) 'hace la captura en el lienso oScreenShot.

    '    'Asigna la captura.
    '    'Dim instance As Control
    '    Dim bmp As Bitmap
    '    'Dim targetBounds As Rectangle
    '    PB.Image = oScreenShot
    '    'PictureBox1.Image = LBLIMG.DrawToBitmap(bitmap, oBounds)
    '    Dim c As Control
    '    c = LBLIMG
    '    bmp = New System.Drawing.Bitmap(c.Width, c.Height)
    '    c.DrawToBitmap(bmp, c.ClientRectangle)
    '    PictureBox1.Image = bmp
    'End Sub

    Private Sub GUARDARURLIMAGEN()
        Dim RUTA As String
        RUTA = txtRutaXML.Text + RUTAIMAGEN + ".jpg"
        Dim IMGTEM As Image
        Dim myCallback As New Image.GetThumbnailImageAbort(AddressOf ThumbnailCallback)
        IMGTEM = PB.Image.GetThumbnailImage(LBLIMG.Width, LBLIMG.Height, myCallback, IntPtr.Zero)
        IMGTEM.Save(RUTA)
    End Sub
    Private Sub ProcesodeFondo() 'este seria la funcion que maneja el subproceso
        PB.Refresh()
    End Sub
    Dim CT As Integer
    Private Sub GUARDARIMAGEN(ByVal NOMBREIMAGEN As String, ByVal NOMBREPLATILLO As String)
        'CAPTURARPANTALLA()
        LBLIMG.Text = NOMBREPLATILLO
        'RECTANGULO(NOMBREPLATILLO)
        Dim bmp As Bitmap
        Dim c As Control
        c = LBLIMG
        bmp = New System.Drawing.Bitmap(c.Width, c.Height)
        c.DrawToBitmap(bmp, c.ClientRectangle)        
        PB.Image = bmp
        'Dim t As Thread 'declaracion del hilo
        't = New Thread(AddressOf Me.ProcesodeFondo) 'creamos el hilo con nombre "t"
        't.Start()
        'If t.ThreadState <> ThreadState.Unstarted And t.ThreadState <> ThreadState.Stopped Then
        '    t.Suspend()
        'End If
        PB.Refresh()
        PB.Refresh()
        'MessageBox.Show("ESPERANDO")
        RUTAIMAGEN = NOMBREIMAGEN
        'PB.Refresh()
        CT = 0
        GUARDARURLIMAGEN()
        'Timer1.Start()

        'Timer1.Stop()
    End Sub

    Private Sub cmbExa1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbExa1.Click
        Dim Folder As New FolderBrowserDialog
        If Folder.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtRutaXML.Text = Folder.SelectedPath & "\"
        End If
    End Sub

    Private Sub cmbExa4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbExa4.Click
        Dim ofd As New OpenFileDialog
        ofd.Title = "Archivos de imagen JPG"
        ofd.DefaultExt = ".jpg"
        ofd.Filter = "Archivos de imagen(*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
        ofd.FilterIndex = 1
        ofd.FileName = ""

        If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim IMG As Image
            IMG = Bitmap.FromFile(ofd.FileName)
            LBLIMG.Image = IMG
            'Config.Empresa.RutaLogotipo = ofd.FileName
        End If
    End Sub

    Private Sub cmbExa2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbExa2.Click
        Dim FUENTE As New FontDialog
        If FUENTE.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.TXTFUENTE.Text = FUENTE.Font.ToString
            LBLIMG.Font = FUENTE.Font
        End If

    End Sub

    Private Sub SIIMP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SIIMP.Click
        Dim XYZ As Short
        XYZ = MessageBox.Show("Desea Marcar Todos", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If XYZ = 6 Then
            For X = 0 To DGV.Rows.Count - 1
                DGV.Item(4, X).Value = True
            Next
        End If
    End Sub

    Private Sub NOIMP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NOIMP.Click
        Dim XYZ As Short
        XYZ = MessageBox.Show("Desea Desmarcar Todos", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If XYZ = 6 Then
            For X = 0 To DGV.Rows.Count - 1
                DGV.Item(4, X).Value = False
            Next
        End If
    End Sub

    Private Sub BTNMOSTRAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNMOSTRAR.Click
        CARGADATOS()
    End Sub
    Private Sub GUARDAR()
        For X = 0 To CLAGRU.Count - 1
            GUARDARIMAGEN("GB" + CLAGRU(X), CBGRU.Items(X).ToString)
        Next
        Dim SW As New Stopwatch()
        SW.Reset()
        SW.Start()
        PBAR.Maximum = DGV.Rows.Count - 1
        PBAR.Minimum = 0
        For X = 0 To DGV.Rows.Count - 1
            If DGV.Item(4, X).Value = True Then
                GUARDARIMAGEN(DGV.Item(0, X).Value.ToString, DGV.Item(1, X).Value.ToString)
            End If
            PBAR.Value = X
        Next
        SW.Stop()
        sb.Text = (String.Format("Tiempo empleado en Generar {0} Botones: {1} milisegundos.", DGV.Rows.Count, SW.ElapsedMilliseconds))
        MessageBox.Show("Botones Generados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub
    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        If Not System.IO.Directory.Exists(txtRutaXML.Text) Then
            Try
                System.IO.Directory.CreateDirectory(txtRutaXML.Text)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

        Dim XYZ As Short
        XYZ = MessageBox.Show("Desea Guardar la Información", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If XYZ = 6 Then
            GUARDAR()
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CT += 1
        If CT >= 3000 Then
            Timer1.Stop()
            GUARDARURLIMAGEN()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim CD As New ColorDialog
        If CD.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.TXTFUENTE.ForeColor = CD.Color
            LBLIMG.ForeColor = CD.Color
        End If
    End Sub

    Private Sub NUDH_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NUDH.ValueChanged, NUDW.ValueChanged
        LBLIMG.Height = NUDH.Value
        PB.Height = NUDH.Value
        LBLIMG.Width = NUDW.Value
        PB.Width = NUDW.Value
    End Sub
End Class
