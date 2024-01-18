<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTicketRepartidor
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGV2 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PANELG = New System.Windows.Forms.Panel()
        Me.LB6 = New System.Windows.Forms.Button()
        Me.LB5 = New System.Windows.Forms.Button()
        Me.LB4 = New System.Windows.Forms.Button()
        Me.LB3 = New System.Windows.Forms.Button()
        Me.LB2 = New System.Windows.Forms.Button()
        Me.LB1 = New System.Windows.Forms.Button()
        Me.BTNANTG = New System.Windows.Forms.Button()
        Me.BTNSIGG = New System.Windows.Forms.Button()
        Me.BTNCOBRAR = New System.Windows.Forms.Button()
        Me.BTNQUITAR = New System.Windows.Forms.Button()
        Me.BTNDC = New System.Windows.Forms.Button()
        Me.BTNCC = New System.Windows.Forms.Button()
        Me.LBLTOT = New System.Windows.Forms.Label()
        Me.TXTCANT = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.LBLEMP = New System.Windows.Forms.Label()
        Me.NOIMP = New System.Windows.Forms.Button()
        Me.SIIMP = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.TXTPAGO = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.DGV2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PANELG.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV2
        '
        Me.DGV2.AllowUserToAddRows = False
        Me.DGV2.AllowUserToDeleteRows = False
        Me.DGV2.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.DGV2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV2.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGV2.Location = New System.Drawing.Point(16, 391)
        Me.DGV2.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV2.Name = "DGV2"
        Me.DGV2.Size = New System.Drawing.Size(901, 276)
        Me.DGV2.TabIndex = 292
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(123, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(166, 20)
        Me.Label1.TabIndex = 293
        Me.Label1.Text = "Tickets Sin Cobrar"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 352)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(198, 20)
        Me.Label2.TabIndex = 294
        Me.Label2.Text = "Tickets Seleccionados"
        '
        'PANELG
        '
        Me.PANELG.BackColor = System.Drawing.SystemColors.Control
        Me.PANELG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PANELG.Controls.Add(Me.LB6)
        Me.PANELG.Controls.Add(Me.LB5)
        Me.PANELG.Controls.Add(Me.LB4)
        Me.PANELG.Controls.Add(Me.LB3)
        Me.PANELG.Controls.Add(Me.LB2)
        Me.PANELG.Controls.Add(Me.LB1)
        Me.PANELG.Controls.Add(Me.BTNANTG)
        Me.PANELG.Controls.Add(Me.BTNSIGG)
        Me.PANELG.Location = New System.Drawing.Point(925, 15)
        Me.PANELG.Margin = New System.Windows.Forms.Padding(4)
        Me.PANELG.Name = "PANELG"
        Me.PANELG.Size = New System.Drawing.Size(407, 536)
        Me.PANELG.TabIndex = 295
        '
        'LB6
        '
        Me.LB6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB6.Location = New System.Drawing.Point(203, 293)
        Me.LB6.Margin = New System.Windows.Forms.Padding(4)
        Me.LB6.Name = "LB6"
        Me.LB6.Size = New System.Drawing.Size(179, 143)
        Me.LB6.TabIndex = 1068
        Me.LB6.Tag = "6"
        Me.LB6.Text = "Button5"
        Me.LB6.UseVisualStyleBackColor = True
        '
        'LB5
        '
        Me.LB5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB5.Location = New System.Drawing.Point(16, 293)
        Me.LB5.Margin = New System.Windows.Forms.Padding(4)
        Me.LB5.Name = "LB5"
        Me.LB5.Size = New System.Drawing.Size(179, 143)
        Me.LB5.TabIndex = 1067
        Me.LB5.Tag = "5"
        Me.LB5.Text = "Button6"
        Me.LB5.UseVisualStyleBackColor = True
        '
        'LB4
        '
        Me.LB4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB4.Location = New System.Drawing.Point(203, 150)
        Me.LB4.Margin = New System.Windows.Forms.Padding(4)
        Me.LB4.Name = "LB4"
        Me.LB4.Size = New System.Drawing.Size(179, 143)
        Me.LB4.TabIndex = 1066
        Me.LB4.Tag = "4"
        Me.LB4.Text = "Button3"
        Me.LB4.UseVisualStyleBackColor = True
        '
        'LB3
        '
        Me.LB3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB3.Location = New System.Drawing.Point(16, 150)
        Me.LB3.Margin = New System.Windows.Forms.Padding(4)
        Me.LB3.Name = "LB3"
        Me.LB3.Size = New System.Drawing.Size(179, 143)
        Me.LB3.TabIndex = 1065
        Me.LB3.Tag = "3"
        Me.LB3.Text = "Button4"
        Me.LB3.UseVisualStyleBackColor = True
        '
        'LB2
        '
        Me.LB2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB2.Location = New System.Drawing.Point(203, 7)
        Me.LB2.Margin = New System.Windows.Forms.Padding(4)
        Me.LB2.Name = "LB2"
        Me.LB2.Size = New System.Drawing.Size(179, 143)
        Me.LB2.TabIndex = 1064
        Me.LB2.Tag = "2"
        Me.LB2.Text = "Button2"
        Me.LB2.UseVisualStyleBackColor = True
        '
        'LB1
        '
        Me.LB1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB1.Location = New System.Drawing.Point(16, 7)
        Me.LB1.Margin = New System.Windows.Forms.Padding(4)
        Me.LB1.Name = "LB1"
        Me.LB1.Size = New System.Drawing.Size(179, 143)
        Me.LB1.TabIndex = 296
        Me.LB1.Tag = "1"
        Me.LB1.Text = "Button1"
        Me.LB1.UseVisualStyleBackColor = True
        '
        'BTNANTG
        '
        Me.BTNANTG.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold)
        Me.BTNANTG.Image = Global.Magos.My.Resources.Resources.ANTERIOR
        Me.BTNANTG.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BTNANTG.Location = New System.Drawing.Point(16, 438)
        Me.BTNANTG.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNANTG.Name = "BTNANTG"
        Me.BTNANTG.Size = New System.Drawing.Size(143, 86)
        Me.BTNANTG.TabIndex = 1063
        Me.BTNANTG.Tag = "7"
        Me.BTNANTG.UseVisualStyleBackColor = False
        '
        'BTNSIGG
        '
        Me.BTNSIGG.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold)
        Me.BTNSIGG.Image = Global.Magos.My.Resources.Resources.SIGUIENTE
        Me.BTNSIGG.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BTNSIGG.Location = New System.Drawing.Point(239, 438)
        Me.BTNSIGG.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNSIGG.Name = "BTNSIGG"
        Me.BTNSIGG.Size = New System.Drawing.Size(143, 86)
        Me.BTNSIGG.TabIndex = 1062
        Me.BTNSIGG.Tag = "7"
        Me.BTNSIGG.UseVisualStyleBackColor = False
        '
        'BTNCOBRAR
        '
        Me.BTNCOBRAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold)
        Me.BTNCOBRAR.Image = Global.Magos.My.Resources.Resources._14
        Me.BTNCOBRAR.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BTNCOBRAR.Location = New System.Drawing.Point(591, 674)
        Me.BTNCOBRAR.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNCOBRAR.Name = "BTNCOBRAR"
        Me.BTNCOBRAR.Size = New System.Drawing.Size(159, 86)
        Me.BTNCOBRAR.TabIndex = 1064
        Me.BTNCOBRAR.Tag = "7"
        Me.BTNCOBRAR.UseVisualStyleBackColor = False
        '
        'BTNQUITAR
        '
        Me.BTNQUITAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNQUITAR.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BTNQUITAR.Location = New System.Drawing.Point(775, 674)
        Me.BTNQUITAR.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNQUITAR.Name = "BTNQUITAR"
        Me.BTNQUITAR.Size = New System.Drawing.Size(143, 86)
        Me.BTNQUITAR.TabIndex = 1065
        Me.BTNQUITAR.Tag = "7"
        Me.BTNQUITAR.Text = "Quitar"
        Me.BTNQUITAR.UseVisualStyleBackColor = False
        '
        'BTNDC
        '
        Me.BTNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNDC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BTNDC.Location = New System.Drawing.Point(980, 674)
        Me.BTNDC.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNDC.Name = "BTNDC"
        Me.BTNDC.Size = New System.Drawing.Size(143, 86)
        Me.BTNDC.TabIndex = 1066
        Me.BTNDC.Tag = "7"
        Me.BTNDC.Text = "Dar Cambio"
        Me.BTNDC.UseVisualStyleBackColor = False
        '
        'BTNCC
        '
        Me.BTNCC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNCC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BTNCC.Location = New System.Drawing.Point(1153, 674)
        Me.BTNCC.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNCC.Name = "BTNCC"
        Me.BTNCC.Size = New System.Drawing.Size(143, 86)
        Me.BTNCC.TabIndex = 1067
        Me.BTNCC.Tag = "7"
        Me.BTNCC.Text = "Cobrar Cambio"
        Me.BTNCC.UseVisualStyleBackColor = False
        '
        'LBLTOT
        '
        Me.LBLTOT.AutoSize = True
        Me.LBLTOT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOT.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOT.Location = New System.Drawing.Point(3, 684)
        Me.LBLTOT.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBLTOT.Name = "LBLTOT"
        Me.LBLTOT.Size = New System.Drawing.Size(186, 69)
        Me.LBLTOT.TabIndex = 1068
        Me.LBLTOT.Text = "Total:"
        '
        'TXTCANT
        '
        Me.TXTCANT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCANT.Font = New System.Drawing.Font("Arial Rounded MT Bold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCANT.Location = New System.Drawing.Point(1057, 609)
        Me.TXTCANT.Margin = New System.Windows.Forms.Padding(4)
        Me.TXTCANT.MaxLength = 20
        Me.TXTCANT.Name = "TXTCANT"
        Me.TXTCANT.Size = New System.Drawing.Size(251, 35)
        Me.TXTCANT.TabIndex = 1069
        Me.TXTCANT.Text = "0.00"
        Me.TXTCANT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Arial Rounded MT Bold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(920, 618)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(117, 28)
        Me.Label13.TabIndex = 1070
        Me.Label13.Text = "Cantidad"
        '
        'LBLEMP
        '
        Me.LBLEMP.AutoSize = True
        Me.LBLEMP.BackColor = System.Drawing.Color.Transparent
        Me.LBLEMP.Font = New System.Drawing.Font("Arial Rounded MT Bold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLEMP.Location = New System.Drawing.Point(925, 554)
        Me.LBLEMP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBLEMP.Name = "LBLEMP"
        Me.LBLEMP.Size = New System.Drawing.Size(128, 28)
        Me.LBLEMP.TabIndex = 1071
        Me.LBLEMP.Text = "Empleado"
        '
        'NOIMP
        '
        Me.NOIMP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NOIMP.Location = New System.Drawing.Point(509, 341)
        Me.NOIMP.Margin = New System.Windows.Forms.Padding(4)
        Me.NOIMP.Name = "NOIMP"
        Me.NOIMP.Size = New System.Drawing.Size(240, 43)
        Me.NOIMP.TabIndex = 1073
        Me.NOIMP.Text = "Desmarcar Todos"
        Me.NOIMP.UseVisualStyleBackColor = True
        '
        'SIIMP
        '
        Me.SIIMP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SIIMP.Location = New System.Drawing.Point(257, 340)
        Me.SIIMP.Margin = New System.Windows.Forms.Padding(4)
        Me.SIIMP.Name = "SIIMP"
        Me.SIIMP.Size = New System.Drawing.Size(228, 43)
        Me.SIIMP.TabIndex = 1072
        Me.SIIMP.Text = "Marcar Todos"
        Me.SIIMP.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button1.Location = New System.Drawing.Point(775, 342)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(124, 42)
        Me.Button1.TabIndex = 1074
        Me.Button1.Tag = "7"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'DGV
        '
        Me.DGV.AllowUserToAddRows = False
        Me.DGV.AllowUserToDeleteRows = False
        Me.DGV.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.DGV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV.DefaultCellStyle = DataGridViewCellStyle6
        Me.DGV.Location = New System.Drawing.Point(5, 39)
        Me.DGV.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV.Name = "DGV"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DGV.Size = New System.Drawing.Size(912, 293)
        Me.DGV.TabIndex = 1075
        '
        'TXTPAGO
        '
        Me.TXTPAGO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTPAGO.Font = New System.Drawing.Font("Arial Rounded MT Bold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPAGO.Location = New System.Drawing.Point(421, 716)
        Me.TXTPAGO.Margin = New System.Windows.Forms.Padding(4)
        Me.TXTPAGO.MaxLength = 20
        Me.TXTPAGO.Name = "TXTPAGO"
        Me.TXTPAGO.Size = New System.Drawing.Size(160, 35)
        Me.TXTPAGO.TabIndex = 1076
        Me.TXTPAGO.Text = "0.00"
        Me.TXTPAGO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial Rounded MT Bold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(425, 684)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 28)
        Me.Label3.TabIndex = 1077
        Me.Label3.Text = "Pago"
        '
        'frmTicketRepartidor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1344, 767)
        Me.Controls.Add(Me.TXTPAGO)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DGV)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.NOIMP)
        Me.Controls.Add(Me.SIIMP)
        Me.Controls.Add(Me.LBLEMP)
        Me.Controls.Add(Me.TXTCANT)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.LBLTOT)
        Me.Controls.Add(Me.BTNCC)
        Me.Controls.Add(Me.BTNDC)
        Me.Controls.Add(Me.BTNQUITAR)
        Me.Controls.Add(Me.BTNCOBRAR)
        Me.Controls.Add(Me.PANELG)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DGV2)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmTicketRepartidor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ticket Repartidor"
        CType(Me.DGV2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PANELG.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGV2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PANELG As System.Windows.Forms.Panel
    Friend WithEvents BTNANTG As System.Windows.Forms.Button
    Friend WithEvents BTNSIGG As System.Windows.Forms.Button
    Friend WithEvents LB1 As System.Windows.Forms.Button
    Friend WithEvents LB6 As System.Windows.Forms.Button
    Friend WithEvents LB5 As System.Windows.Forms.Button
    Friend WithEvents LB4 As System.Windows.Forms.Button
    Friend WithEvents LB3 As System.Windows.Forms.Button
    Friend WithEvents LB2 As System.Windows.Forms.Button
    Friend WithEvents BTNCOBRAR As System.Windows.Forms.Button
    Friend WithEvents BTNQUITAR As System.Windows.Forms.Button
    Friend WithEvents BTNDC As System.Windows.Forms.Button
    Friend WithEvents BTNCC As System.Windows.Forms.Button
    Friend WithEvents LBLTOT As System.Windows.Forms.Label
    Friend WithEvents TXTCANT As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents LBLEMP As System.Windows.Forms.Label
    Friend WithEvents NOIMP As System.Windows.Forms.Button
    Friend WithEvents SIIMP As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DGV As System.Windows.Forms.DataGridView
    Friend WithEvents TXTPAGO As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
