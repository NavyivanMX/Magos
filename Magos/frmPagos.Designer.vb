<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPagos
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
        Me.LBLAUT = New System.Windows.Forms.Label
        Me.TXTAUT = New System.Windows.Forms.TextBox
        Me.LBLBAN = New System.Windows.Forms.Label
        Me.CBBANCO = New System.Windows.Forms.ComboBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.TXTDIG = New System.Windows.Forms.TextBox
        Me.CBTP = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TXTTOT = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TXTCAMBIO = New System.Windows.Forms.TextBox
        Me.TXTEFECTIVO = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.BTNPUNTO = New System.Windows.Forms.Button
        Me.BTN7 = New System.Windows.Forms.Button
        Me.BTN0 = New System.Windows.Forms.Button
        Me.BTN9 = New System.Windows.Forms.Button
        Me.BTN8 = New System.Windows.Forms.Button
        Me.BTN6 = New System.Windows.Forms.Button
        Me.BTN5 = New System.Windows.Forms.Button
        Me.BTN4 = New System.Windows.Forms.Button
        Me.BTN3 = New System.Windows.Forms.Button
        Me.BTN2 = New System.Windows.Forms.Button
        Me.BTN1 = New System.Windows.Forms.Button
        Me.BTNCANCELAR = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.BTNCOBRAR = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LBLAUT
        '
        Me.LBLAUT.AutoSize = True
        Me.LBLAUT.BackColor = System.Drawing.Color.Transparent
        Me.LBLAUT.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLAUT.Location = New System.Drawing.Point(258, 487)
        Me.LBLAUT.Name = "LBLAUT"
        Me.LBLAUT.Size = New System.Drawing.Size(102, 18)
        Me.LBLAUT.TabIndex = 1118
        Me.LBLAUT.Text = "Autorización"
        Me.LBLAUT.Visible = False
        '
        'TXTAUT
        '
        Me.TXTAUT.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTAUT.Location = New System.Drawing.Point(255, 517)
        Me.TXTAUT.MaxLength = 10
        Me.TXTAUT.Name = "TXTAUT"
        Me.TXTAUT.Size = New System.Drawing.Size(182, 31)
        Me.TXTAUT.TabIndex = 1117
        Me.TXTAUT.Visible = False
        '
        'LBLBAN
        '
        Me.LBLBAN.AutoSize = True
        Me.LBLBAN.BackColor = System.Drawing.Color.Transparent
        Me.LBLBAN.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLBAN.Location = New System.Drawing.Point(32, 245)
        Me.LBLBAN.Name = "LBLBAN"
        Me.LBLBAN.Size = New System.Drawing.Size(56, 18)
        Me.LBLBAN.TabIndex = 1116
        Me.LBLBAN.Text = "Banco"
        Me.LBLBAN.Visible = False
        '
        'CBBANCO
        '
        Me.CBBANCO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBBANCO.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBBANCO.FormattingEnabled = True
        Me.CBBANCO.Items.AddRange(New Object() {"Efectivo", "Tarjeta", "Mixto", "Cheque"})
        Me.CBBANCO.Location = New System.Drawing.Point(32, 266)
        Me.CBBANCO.Name = "CBBANCO"
        Me.CBBANCO.Size = New System.Drawing.Size(405, 39)
        Me.CBBANCO.TabIndex = 1115
        Me.CBBANCO.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(29, 496)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(152, 18)
        Me.Label16.TabIndex = 1114
        Me.Label16.Text = "de cheque o tarjeta"
        Me.Label16.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(29, 478)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(140, 18)
        Me.Label15.TabIndex = 1113
        Me.Label15.Text = "Ultimos 4 digitos "
        Me.Label15.Visible = False
        '
        'TXTDIG
        '
        Me.TXTDIG.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTDIG.Location = New System.Drawing.Point(32, 517)
        Me.TXTDIG.MaxLength = 4
        Me.TXTDIG.Name = "TXTDIG"
        Me.TXTDIG.Size = New System.Drawing.Size(182, 31)
        Me.TXTDIG.TabIndex = 1112
        Me.TXTDIG.Visible = False
        '
        'CBTP
        '
        Me.CBTP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTP.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBTP.FormattingEnabled = True
        Me.CBTP.Items.AddRange(New Object() {"Efectivo", "Tarjeta", "Cheque"})
        Me.CBTP.Location = New System.Drawing.Point(32, 190)
        Me.CBTP.Name = "CBTP"
        Me.CBTP.Size = New System.Drawing.Size(405, 39)
        Me.CBTP.TabIndex = 1145
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(32, 162)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 16)
        Me.Label6.TabIndex = 1146
        Me.Label6.Text = "Tipo de Pago"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(32, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(106, 16)
        Me.Label8.TabIndex = 1148
        Me.Label8.Text = "Total a pagar:"
        '
        'TXTTOT
        '
        Me.TXTTOT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTTOT.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTOT.Location = New System.Drawing.Point(32, 39)
        Me.TXTTOT.MaxLength = 50
        Me.TXTTOT.Name = "TXTTOT"
        Me.TXTTOT.ReadOnly = True
        Me.TXTTOT.Size = New System.Drawing.Size(405, 38)
        Me.TXTTOT.TabIndex = 1147
        Me.TXTTOT.Text = "0"
        Me.TXTTOT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(186, 332)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 16)
        Me.Label3.TabIndex = 1152
        Me.Label3.Text = "Cambio"
        '
        'TXTCAMBIO
        '
        Me.TXTCAMBIO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCAMBIO.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCAMBIO.Location = New System.Drawing.Point(32, 351)
        Me.TXTCAMBIO.MaxLength = 50
        Me.TXTCAMBIO.Name = "TXTCAMBIO"
        Me.TXTCAMBIO.ReadOnly = True
        Me.TXTCAMBIO.Size = New System.Drawing.Size(405, 38)
        Me.TXTCAMBIO.TabIndex = 1151
        Me.TXTCAMBIO.Text = "0"
        Me.TXTCAMBIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTEFECTIVO
        '
        Me.TXTEFECTIVO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTEFECTIVO.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTEFECTIVO.Location = New System.Drawing.Point(32, 108)
        Me.TXTEFECTIVO.MaxLength = 10
        Me.TXTEFECTIVO.Name = "TXTEFECTIVO"
        Me.TXTEFECTIVO.Size = New System.Drawing.Size(405, 38)
        Me.TXTEFECTIVO.TabIndex = 1153
        Me.TXTEFECTIVO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(32, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 16)
        Me.Label4.TabIndex = 1154
        Me.Label4.Text = "Efectivo"
        '
        'BTNPUNTO
        '
        Me.BTNPUNTO.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNPUNTO.Location = New System.Drawing.Point(559, 274)
        Me.BTNPUNTO.Name = "BTNPUNTO"
        Me.BTNPUNTO.Size = New System.Drawing.Size(52, 74)
        Me.BTNPUNTO.TabIndex = 1174
        Me.BTNPUNTO.Tag = "."
        Me.BTNPUNTO.Text = "."
        Me.BTNPUNTO.UseVisualStyleBackColor = True
        '
        'BTN7
        '
        Me.BTN7.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN7.Location = New System.Drawing.Point(523, 18)
        Me.BTN7.Name = "BTN7"
        Me.BTN7.Size = New System.Drawing.Size(88, 78)
        Me.BTN7.TabIndex = 1168
        Me.BTN7.Tag = "7"
        Me.BTN7.Text = "7"
        Me.BTN7.UseVisualStyleBackColor = True
        '
        'BTN0
        '
        Me.BTN0.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN0.Location = New System.Drawing.Point(617, 271)
        Me.BTN0.Name = "BTN0"
        Me.BTN0.Size = New System.Drawing.Size(88, 78)
        Me.BTN0.TabIndex = 1171
        Me.BTN0.Tag = "0"
        Me.BTN0.Text = "0"
        Me.BTN0.UseVisualStyleBackColor = True
        '
        'BTN9
        '
        Me.BTN9.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN9.Location = New System.Drawing.Point(711, 18)
        Me.BTN9.Name = "BTN9"
        Me.BTN9.Size = New System.Drawing.Size(88, 78)
        Me.BTN9.TabIndex = 1170
        Me.BTN9.Tag = "9"
        Me.BTN9.Text = "9"
        Me.BTN9.UseVisualStyleBackColor = True
        '
        'BTN8
        '
        Me.BTN8.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN8.Location = New System.Drawing.Point(617, 18)
        Me.BTN8.Name = "BTN8"
        Me.BTN8.Size = New System.Drawing.Size(88, 78)
        Me.BTN8.TabIndex = 1169
        Me.BTN8.Tag = "8"
        Me.BTN8.Text = "8"
        Me.BTN8.UseVisualStyleBackColor = True
        '
        'BTN6
        '
        Me.BTN6.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN6.Location = New System.Drawing.Point(711, 102)
        Me.BTN6.Name = "BTN6"
        Me.BTN6.Size = New System.Drawing.Size(88, 78)
        Me.BTN6.TabIndex = 1167
        Me.BTN6.Tag = "6"
        Me.BTN6.Text = "6"
        Me.BTN6.UseVisualStyleBackColor = True
        '
        'BTN5
        '
        Me.BTN5.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN5.Location = New System.Drawing.Point(617, 102)
        Me.BTN5.Name = "BTN5"
        Me.BTN5.Size = New System.Drawing.Size(88, 78)
        Me.BTN5.TabIndex = 1166
        Me.BTN5.Tag = "5"
        Me.BTN5.Text = "5"
        Me.BTN5.UseVisualStyleBackColor = True
        '
        'BTN4
        '
        Me.BTN4.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN4.Location = New System.Drawing.Point(523, 102)
        Me.BTN4.Name = "BTN4"
        Me.BTN4.Size = New System.Drawing.Size(88, 78)
        Me.BTN4.TabIndex = 1165
        Me.BTN4.Tag = "4"
        Me.BTN4.Text = "4"
        Me.BTN4.UseVisualStyleBackColor = True
        '
        'BTN3
        '
        Me.BTN3.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN3.Location = New System.Drawing.Point(711, 186)
        Me.BTN3.Name = "BTN3"
        Me.BTN3.Size = New System.Drawing.Size(88, 78)
        Me.BTN3.TabIndex = 1164
        Me.BTN3.Tag = "3"
        Me.BTN3.Text = "3"
        Me.BTN3.UseVisualStyleBackColor = True
        '
        'BTN2
        '
        Me.BTN2.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN2.Location = New System.Drawing.Point(617, 186)
        Me.BTN2.Name = "BTN2"
        Me.BTN2.Size = New System.Drawing.Size(88, 78)
        Me.BTN2.TabIndex = 1163
        Me.BTN2.Tag = "2"
        Me.BTN2.Text = "2"
        Me.BTN2.UseVisualStyleBackColor = True
        '
        'BTN1
        '
        Me.BTN1.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN1.Location = New System.Drawing.Point(523, 186)
        Me.BTN1.Name = "BTN1"
        Me.BTN1.Size = New System.Drawing.Size(88, 78)
        Me.BTN1.TabIndex = 1162
        Me.BTN1.Tag = "1"
        Me.BTN1.Text = "1"
        Me.BTN1.UseVisualStyleBackColor = True
        '
        'BTNCANCELAR
        '
        Me.BTNCANCELAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNCANCELAR.Location = New System.Drawing.Point(711, 271)
        Me.BTNCANCELAR.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BTNCANCELAR.Name = "BTNCANCELAR"
        Me.BTNCANCELAR.Size = New System.Drawing.Size(88, 77)
        Me.BTNCANCELAR.TabIndex = 1172
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = Global.Magos.My.Resources.Resources._09
        Me.Button1.Location = New System.Drawing.Point(479, 404)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(162, 144)
        Me.Button1.TabIndex = 1150
        Me.Button1.UseVisualStyleBackColor = False
        '
        'BTNCOBRAR
        '
        Me.BTNCOBRAR.BackColor = System.Drawing.SystemColors.Control
        Me.BTNCOBRAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNCOBRAR.ForeColor = System.Drawing.Color.White
        Me.BTNCOBRAR.Image = Global.Magos.My.Resources.Resources._14
        Me.BTNCOBRAR.Location = New System.Drawing.Point(668, 404)
        Me.BTNCOBRAR.Name = "BTNCOBRAR"
        Me.BTNCOBRAR.Size = New System.Drawing.Size(162, 144)
        Me.BTNCOBRAR.TabIndex = 1149
        Me.BTNCOBRAR.UseVisualStyleBackColor = False
        '
        'frmPagos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(855, 575)
        Me.ControlBox = False
        Me.Controls.Add(Me.BTNPUNTO)
        Me.Controls.Add(Me.BTNCANCELAR)
        Me.Controls.Add(Me.BTN7)
        Me.Controls.Add(Me.BTN0)
        Me.Controls.Add(Me.BTN9)
        Me.Controls.Add(Me.BTN8)
        Me.Controls.Add(Me.BTN6)
        Me.Controls.Add(Me.BTN5)
        Me.Controls.Add(Me.BTN4)
        Me.Controls.Add(Me.BTN3)
        Me.Controls.Add(Me.BTN2)
        Me.Controls.Add(Me.BTN1)
        Me.Controls.Add(Me.TXTEFECTIVO)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TXTCAMBIO)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BTNCOBRAR)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TXTTOT)
        Me.Controls.Add(Me.CBTP)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.LBLAUT)
        Me.Controls.Add(Me.TXTAUT)
        Me.Controls.Add(Me.LBLBAN)
        Me.Controls.Add(Me.CBBANCO)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.TXTDIG)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPagos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pagos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LBLAUT As System.Windows.Forms.Label
    Friend WithEvents TXTAUT As System.Windows.Forms.TextBox
    Friend WithEvents LBLBAN As System.Windows.Forms.Label
    Friend WithEvents CBBANCO As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TXTDIG As System.Windows.Forms.TextBox
    Friend WithEvents CBTP As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TXTTOT As System.Windows.Forms.TextBox
    Friend WithEvents BTNCOBRAR As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TXTCAMBIO As System.Windows.Forms.TextBox
    Friend WithEvents TXTEFECTIVO As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents BTNPUNTO As System.Windows.Forms.Button
    Private WithEvents BTNCANCELAR As System.Windows.Forms.Button
    Friend WithEvents BTN7 As System.Windows.Forms.Button
    Friend WithEvents BTN0 As System.Windows.Forms.Button
    Friend WithEvents BTN9 As System.Windows.Forms.Button
    Friend WithEvents BTN8 As System.Windows.Forms.Button
    Friend WithEvents BTN6 As System.Windows.Forms.Button
    Friend WithEvents BTN5 As System.Windows.Forms.Button
    Friend WithEvents BTN4 As System.Windows.Forms.Button
    Friend WithEvents BTN3 As System.Windows.Forms.Button
    Friend WithEvents BTN2 As System.Windows.Forms.Button
    Friend WithEvents BTN1 As System.Windows.Forms.Button
End Class
