<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmComentario
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
        Me.BTNNO = New System.Windows.Forms.Button()
        Me.TXTCOM = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LBLNC1 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.CHK1 = New System.Windows.Forms.CheckBox()
        Me.DTHORA = New System.Windows.Forms.DateTimePicker()
        Me.CBCF = New System.Windows.Forms.Button()
        Me.CBSF = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BTNYES = New System.Windows.Forms.Button()
        Me.CBDOM = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'BTNNO
        '
        Me.BTNNO.Image = Global.Magos.My.Resources.Resources._09
        Me.BTNNO.Location = New System.Drawing.Point(1092, 465)
        Me.BTNNO.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTNNO.Name = "BTNNO"
        Me.BTNNO.Size = New System.Drawing.Size(204, 171)
        Me.BTNNO.TabIndex = 2
        Me.BTNNO.Tag = "0"
        Me.BTNNO.Text = " "
        Me.BTNNO.UseVisualStyleBackColor = True
        '
        'TXTCOM
        '
        Me.TXTCOM.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOM.Location = New System.Drawing.Point(16, 105)
        Me.TXTCOM.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TXTCOM.Multiline = True
        Me.TXTCOM.Name = "TXTCOM"
        Me.TXTCOM.Size = New System.Drawing.Size(1279, 312)
        Me.TXTCOM.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(348, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Comentario de Servicio a Domicilio"
        '
        'LBLNC1
        '
        Me.LBLNC1.BackColor = System.Drawing.Color.Transparent
        Me.LBLNC1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLNC1.ForeColor = System.Drawing.Color.Red
        Me.LBLNC1.Location = New System.Drawing.Point(28, 578)
        Me.LBLNC1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBLNC1.Name = "LBLNC1"
        Me.LBLNC1.Size = New System.Drawing.Size(736, 30)
        Me.LBLNC1.TabIndex = 1133
        Me.LBLNC1.Text = "FAVOR DE SELECCIONAR EL DOMICILIO"
        Me.LBLNC1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(13, 442)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(88, 20)
        Me.Label27.TabIndex = 1134
        Me.Label27.Text = "Domicilio"
        '
        'CHK1
        '
        Me.CHK1.AutoSize = True
        Me.CHK1.BackColor = System.Drawing.Color.Transparent
        Me.CHK1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHK1.Location = New System.Drawing.Point(409, 22)
        Me.CHK1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CHK1.Name = "CHK1"
        Me.CHK1.Size = New System.Drawing.Size(167, 28)
        Me.CHK1.TabIndex = 1136
        Me.CHK1.Text = "Entregar a las:"
        Me.CHK1.UseVisualStyleBackColor = False
        Me.CHK1.Visible = False
        '
        'DTHORA
        '
        Me.DTHORA.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTHORA.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTHORA.Location = New System.Drawing.Point(604, 22)
        Me.DTHORA.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DTHORA.Name = "DTHORA"
        Me.DTHORA.ShowUpDown = True
        Me.DTHORA.Size = New System.Drawing.Size(169, 29)
        Me.DTHORA.TabIndex = 1137
        Me.DTHORA.Visible = False
        '
        'CBCF
        '
        Me.CBCF.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBCF.Location = New System.Drawing.Point(812, 15)
        Me.CBCF.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CBCF.Name = "CBCF"
        Me.CBCF.Size = New System.Drawing.Size(221, 53)
        Me.CBCF.TabIndex = 1138
        Me.CBCF.Tag = "0"
        Me.CBCF.Text = "CON FACTURA"
        Me.CBCF.UseVisualStyleBackColor = True
        '
        'CBSF
        '
        Me.CBSF.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBSF.Location = New System.Drawing.Point(1055, 15)
        Me.CBSF.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CBSF.Name = "CBSF"
        Me.CBSF.Size = New System.Drawing.Size(221, 53)
        Me.CBSF.TabIndex = 1139
        Me.CBSF.Tag = "0"
        Me.CBSF.Text = "SIN FACTURA"
        Me.CBSF.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(679, 501)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(151, 53)
        Me.Button1.TabIndex = 1140
        Me.Button1.Text = "+ Dom"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BTNYES
        '
        Me.BTNYES.Image = Global.Magos.My.Resources.Resources._11
        Me.BTNYES.Location = New System.Drawing.Point(867, 465)
        Me.BTNYES.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTNYES.Name = "BTNYES"
        Me.BTNYES.Size = New System.Drawing.Size(204, 171)
        Me.BTNYES.TabIndex = 1
        Me.BTNYES.Tag = "0"
        Me.BTNYES.Text = " "
        Me.BTNYES.UseVisualStyleBackColor = True
        '
        'CBDOM
        '
        Me.CBDOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBDOM.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBDOM.FormattingEnabled = True
        Me.CBDOM.Location = New System.Drawing.Point(20, 513)
        Me.CBDOM.Margin = New System.Windows.Forms.Padding(4)
        Me.CBDOM.Name = "CBDOM"
        Me.CBDOM.Size = New System.Drawing.Size(627, 32)
        Me.CBDOM.TabIndex = 1135
        '
        'frmComentario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1312, 651)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CBSF)
        Me.Controls.Add(Me.CBCF)
        Me.Controls.Add(Me.DTHORA)
        Me.Controls.Add(Me.CHK1)
        Me.Controls.Add(Me.LBLNC1)
        Me.Controls.Add(Me.CBDOM)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TXTCOM)
        Me.Controls.Add(Me.BTNNO)
        Me.Controls.Add(Me.BTNYES)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmComentario"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comentario Servicio Domicilio"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BTNNO As System.Windows.Forms.Button
    Friend WithEvents BTNYES As System.Windows.Forms.Button
    Friend WithEvents TXTCOM As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LBLNC1 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents CHK1 As System.Windows.Forms.CheckBox
    Friend WithEvents DTHORA As System.Windows.Forms.DateTimePicker
    Friend WithEvents CBCF As System.Windows.Forms.Button
    Friend WithEvents CBSF As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CBDOM As ComboBox
End Class
