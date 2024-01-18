<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGenerarBotones
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmbExa1 = New System.Windows.Forms.Button()
        Me.txtRutaXML = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbExa4 = New System.Windows.Forms.Button()
        Me.cmbExa2 = New System.Windows.Forms.Button()
        Me.TXTFUENTE = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.CBGRU = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NOIMP = New System.Windows.Forms.Button()
        Me.SIIMP = New System.Windows.Forms.Button()
        Me.PBAR = New System.Windows.Forms.ProgressBar()
        Me.sb = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.NUDH = New System.Windows.Forms.NumericUpDown()
        Me.NUDW = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.BTNGUARDAR = New System.Windows.Forms.Button()
        Me.BTNMOSTRAR = New System.Windows.Forms.Button()
        Me.PB = New System.Windows.Forms.PictureBox()
        Me.LBLIMG = New System.Windows.Forms.Label()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbExa1
        '
        Me.cmbExa1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbExa1.Location = New System.Drawing.Point(592, 495)
        Me.cmbExa1.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbExa1.Name = "cmbExa1"
        Me.cmbExa1.Size = New System.Drawing.Size(109, 28)
        Me.cmbExa1.TabIndex = 467
        Me.cmbExa1.Tag = "0"
        Me.cmbExa1.Text = "Examinar"
        Me.cmbExa1.UseVisualStyleBackColor = True
        '
        'txtRutaXML
        '
        Me.txtRutaXML.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRutaXML.Location = New System.Drawing.Point(287, 527)
        Me.txtRutaXML.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRutaXML.Name = "txtRutaXML"
        Me.txtRutaXML.ReadOnly = True
        Me.txtRutaXML.Size = New System.Drawing.Size(439, 26)
        Me.txtRutaXML.TabIndex = 469
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(283, 503)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(247, 20)
        Me.Label20.TabIndex = 468
        Me.Label20.Text = "Ruta para guardar Imagenes"
        '
        'cmbExa4
        '
        Me.cmbExa4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbExa4.Location = New System.Drawing.Point(1092, 530)
        Me.cmbExa4.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbExa4.Name = "cmbExa4"
        Me.cmbExa4.Size = New System.Drawing.Size(71, 28)
        Me.cmbExa4.TabIndex = 470
        Me.cmbExa4.Tag = "0"
        Me.cmbExa4.Text = "..."
        Me.cmbExa4.UseVisualStyleBackColor = True
        '
        'cmbExa2
        '
        Me.cmbExa2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbExa2.Location = New System.Drawing.Point(1092, 495)
        Me.cmbExa2.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbExa2.Name = "cmbExa2"
        Me.cmbExa2.Size = New System.Drawing.Size(71, 28)
        Me.cmbExa2.TabIndex = 473
        Me.cmbExa2.Tag = "0"
        Me.cmbExa2.Text = "..."
        Me.cmbExa2.UseVisualStyleBackColor = True
        '
        'TXTFUENTE
        '
        Me.TXTFUENTE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFUENTE.Location = New System.Drawing.Point(723, 640)
        Me.TXTFUENTE.Margin = New System.Windows.Forms.Padding(4)
        Me.TXTFUENTE.Name = "TXTFUENTE"
        Me.TXTFUENTE.ReadOnly = True
        Me.TXTFUENTE.Size = New System.Drawing.Size(503, 26)
        Me.TXTFUENTE.TabIndex = 475
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(1011, 498)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(66, 20)
        Me.Label21.TabIndex = 474
        Me.Label21.Text = "Fuente"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1011, 535)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 20)
        Me.Label1.TabIndex = 476
        Me.Label1.Text = "Fondo"
        '
        'DGV
        '
        Me.DGV.AllowUserToAddRows = False
        Me.DGV.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.LightSkyBlue
        Me.DGV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Location = New System.Drawing.Point(16, 112)
        Me.DGV.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV.Name = "DGV"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DGV.Size = New System.Drawing.Size(1211, 364)
        Me.DGV.TabIndex = 1379
        '
        'CBGRU
        '
        Me.CBGRU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBGRU.Font = New System.Drawing.Font("Arial Rounded MT Bold", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBGRU.FormattingEnabled = True
        Me.CBGRU.Items.AddRange(New Object() {"Si", "No"})
        Me.CBGRU.Location = New System.Drawing.Point(120, 31)
        Me.CBGRU.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CBGRU.Name = "CBGRU"
        Me.CBGRU.Size = New System.Drawing.Size(411, 30)
        Me.CBGRU.TabIndex = 1380
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial Rounded MT Bold", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(29, 41)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 22)
        Me.Label2.TabIndex = 1381
        Me.Label2.Text = "Grupos"
        '
        'NOIMP
        '
        Me.NOIMP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NOIMP.Location = New System.Drawing.Point(853, 18)
        Me.NOIMP.Margin = New System.Windows.Forms.Padding(4)
        Me.NOIMP.Name = "NOIMP"
        Me.NOIMP.Size = New System.Drawing.Size(132, 73)
        Me.NOIMP.TabIndex = 1384
        Me.NOIMP.Tag = "0"
        Me.NOIMP.Text = "Desmarcar Todos"
        Me.NOIMP.UseVisualStyleBackColor = True
        '
        'SIIMP
        '
        Me.SIIMP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SIIMP.Location = New System.Drawing.Point(699, 18)
        Me.SIIMP.Margin = New System.Windows.Forms.Padding(4)
        Me.SIIMP.Name = "SIIMP"
        Me.SIIMP.Size = New System.Drawing.Size(132, 73)
        Me.SIIMP.TabIndex = 1383
        Me.SIIMP.Tag = "0"
        Me.SIIMP.Text = "Marcar Todos"
        Me.SIIMP.UseVisualStyleBackColor = True
        '
        'PBAR
        '
        Me.PBAR.Location = New System.Drawing.Point(283, 562)
        Me.PBAR.Margin = New System.Windows.Forms.Padding(4)
        Me.PBAR.Name = "PBAR"
        Me.PBAR.Size = New System.Drawing.Size(444, 28)
        Me.PBAR.TabIndex = 1387
        '
        'sb
        '
        Me.sb.Location = New System.Drawing.Point(16, 640)
        Me.sb.Margin = New System.Windows.Forms.Padding(4)
        Me.sb.Name = "sb"
        Me.sb.Size = New System.Drawing.Size(692, 22)
        Me.sb.TabIndex = 1388
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1011, 571)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 20)
        Me.Label3.TabIndex = 1390
        Me.Label3.Text = "Color"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(1092, 566)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(71, 28)
        Me.Button1.TabIndex = 1389
        Me.Button1.Tag = "0"
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'NUDH
        '
        Me.NUDH.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NUDH.Location = New System.Drawing.Point(355, 598)
        Me.NUDH.Margin = New System.Windows.Forms.Padding(4)
        Me.NUDH.Maximum = New Decimal(New Integer() {156, 0, 0, 0})
        Me.NUDH.Minimum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NUDH.Name = "NUDH"
        Me.NUDH.Size = New System.Drawing.Size(109, 26)
        Me.NUDH.TabIndex = 1391
        Me.NUDH.Value = New Decimal(New Integer() {91, 0, 0, 0})
        '
        'NUDW
        '
        Me.NUDW.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NUDW.Location = New System.Drawing.Point(572, 598)
        Me.NUDW.Margin = New System.Windows.Forms.Padding(4)
        Me.NUDW.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.NUDW.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.NUDW.Name = "NUDW"
        Me.NUDW.Size = New System.Drawing.Size(109, 26)
        Me.NUDW.TabIndex = 1392
        Me.NUDW.Value = New Decimal(New Integer() {156, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(279, 606)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 20)
        Me.Label4.TabIndex = 1393
        Me.Label4.Text = "Alto"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(496, 606)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 20)
        Me.Label5.TabIndex = 1394
        Me.Label5.Text = "Ancho"
        '
        'BTNGUARDAR
        '
        Me.BTNGUARDAR.BackColor = System.Drawing.SystemColors.Control
        Me.BTNGUARDAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNGUARDAR.ForeColor = System.Drawing.Color.White
        Me.BTNGUARDAR.Image = Global.Magos.My.Resources.Resources._03
        Me.BTNGUARDAR.Location = New System.Drawing.Point(1023, 6)
        Me.BTNGUARDAR.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNGUARDAR.Name = "BTNGUARDAR"
        Me.BTNGUARDAR.Size = New System.Drawing.Size(107, 98)
        Me.BTNGUARDAR.TabIndex = 1385
        Me.BTNGUARDAR.UseVisualStyleBackColor = False
        '
        'BTNMOSTRAR
        '
        Me.BTNMOSTRAR.BackColor = System.Drawing.SystemColors.Control
        Me.BTNMOSTRAR.ForeColor = System.Drawing.Color.Black
        Me.BTNMOSTRAR.Image = Global.Magos.My.Resources.Resources._06
        Me.BTNMOSTRAR.Location = New System.Drawing.Point(568, 6)
        Me.BTNMOSTRAR.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNMOSTRAR.Name = "BTNMOSTRAR"
        Me.BTNMOSTRAR.Size = New System.Drawing.Size(103, 91)
        Me.BTNMOSTRAR.TabIndex = 1382
        Me.BTNMOSTRAR.UseVisualStyleBackColor = False
        '
        'PB
        '
        Me.PB.Location = New System.Drawing.Point(33, 495)
        Me.PB.Margin = New System.Windows.Forms.Padding(4)
        Me.PB.Name = "PB"
        Me.PB.Size = New System.Drawing.Size(212, 112)
        Me.PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB.TabIndex = 466
        Me.PB.TabStop = False
        '
        'LBLIMG
        '
        Me.LBLIMG.BackColor = System.Drawing.Color.White
        Me.LBLIMG.Font = New System.Drawing.Font("Arial Rounded MT Bold", 16.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLIMG.ForeColor = System.Drawing.Color.DarkMagenta
        Me.LBLIMG.Image = Global.Magos.My.Resources.Resources.FondoVentanas233
        Me.LBLIMG.Location = New System.Drawing.Point(773, 495)
        Me.LBLIMG.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBLIMG.Name = "LBLIMG"
        Me.LBLIMG.Size = New System.Drawing.Size(208, 112)
        Me.LBLIMG.TabIndex = 465
        Me.LBLIMG.Text = "EJEMPLO DE BOTON"
        Me.LBLIMG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmGenerarBotones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1243, 679)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.NUDW)
        Me.Controls.Add(Me.NUDH)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.sb)
        Me.Controls.Add(Me.PBAR)
        Me.Controls.Add(Me.BTNGUARDAR)
        Me.Controls.Add(Me.NOIMP)
        Me.Controls.Add(Me.SIIMP)
        Me.Controls.Add(Me.BTNMOSTRAR)
        Me.Controls.Add(Me.CBGRU)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DGV)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbExa2)
        Me.Controls.Add(Me.TXTFUENTE)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.cmbExa4)
        Me.Controls.Add(Me.cmbExa1)
        Me.Controls.Add(Me.txtRutaXML)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.PB)
        Me.Controls.Add(Me.LBLIMG)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGenerarBotones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generar Botones"
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LBLIMG As System.Windows.Forms.Label
    Friend WithEvents PB As System.Windows.Forms.PictureBox
    Friend WithEvents cmbExa1 As System.Windows.Forms.Button
    Friend WithEvents txtRutaXML As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbExa4 As System.Windows.Forms.Button
    Friend WithEvents cmbExa2 As System.Windows.Forms.Button
    Friend WithEvents TXTFUENTE As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DGV As System.Windows.Forms.DataGridView
    Friend WithEvents BTNMOSTRAR As System.Windows.Forms.Button
    Friend WithEvents CBGRU As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NOIMP As System.Windows.Forms.Button
    Friend WithEvents SIIMP As System.Windows.Forms.Button
    Friend WithEvents BTNGUARDAR As System.Windows.Forms.Button
    Friend WithEvents PBAR As System.Windows.Forms.ProgressBar
    Friend WithEvents sb As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents NUDH As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUDW As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
