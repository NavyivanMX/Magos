<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeleccionaCliente
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.TXTC = New System.Windows.Forms.TextBox
        Me.LBLC = New System.Windows.Forms.Label
        Me.TXTTEL = New System.Windows.Forms.MaskedTextBox
        Me.BTNCE = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.BTNBUSCAR = New System.Windows.Forms.Button
        Me.BTNACEPTAR = New System.Windows.Forms.Button
        Me.TXTNT = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(84, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 24)
        Me.Label1.TabIndex = 1075
        Me.Label1.Text = "Cliente"
        '
        'TXTC
        '
        Me.TXTC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTC.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTC.Location = New System.Drawing.Point(50, 51)
        Me.TXTC.MaxLength = 30
        Me.TXTC.Name = "TXTC"
        Me.TXTC.Size = New System.Drawing.Size(151, 38)
        Me.TXTC.TabIndex = 0
        Me.TXTC.Text = "0"
        Me.TXTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLC
        '
        Me.LBLC.AutoSize = True
        Me.LBLC.BackColor = System.Drawing.Color.Transparent
        Me.LBLC.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLC.Location = New System.Drawing.Point(5, 108)
        Me.LBLC.Name = "LBLC"
        Me.LBLC.Size = New System.Drawing.Size(16, 24)
        Me.LBLC.TabIndex = 1079
        Me.LBLC.Text = "."
        '
        'TXTTEL
        '
        Me.TXTTEL.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTEL.Location = New System.Drawing.Point(417, 51)
        Me.TXTTEL.Mask = "999-9-99-99-99"
        Me.TXTTEL.Name = "TXTTEL"
        Me.TXTTEL.Size = New System.Drawing.Size(156, 29)
        Me.TXTTEL.TabIndex = 1080
        '
        'BTNCE
        '
        Me.BTNCE.BackColor = System.Drawing.Color.White
        Me.BTNCE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNCE.Location = New System.Drawing.Point(362, 226)
        Me.BTNCE.Name = "BTNCE"
        Me.BTNCE.Size = New System.Drawing.Size(156, 51)
        Me.BTNCE.TabIndex = 1081
        Me.BTNCE.Text = "Alta de Cliente Especial"
        Me.BTNCE.UseVisualStyleBackColor = False
        Me.BTNCE.Visible = False
        '
        'Button1
        '
        Me.Button1.Image = Global.Magos.My.Resources.Resources._07
        Me.Button1.Location = New System.Drawing.Point(256, 28)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 81)
        Me.Button1.TabIndex = 1082
        '
        'BTNBUSCAR
        '
        Me.BTNBUSCAR.BackColor = System.Drawing.SystemColors.Control
        Me.BTNBUSCAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 1.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNBUSCAR.ForeColor = System.Drawing.Color.Transparent
        Me.BTNBUSCAR.Image = Global.Magos.My.Resources.Resources._04
        Me.BTNBUSCAR.Location = New System.Drawing.Point(417, 178)
        Me.BTNBUSCAR.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BTNBUSCAR.Name = "BTNBUSCAR"
        Me.BTNBUSCAR.Size = New System.Drawing.Size(85, 77)
        Me.BTNBUSCAR.TabIndex = 2
        Me.BTNBUSCAR.Text = "&b"
        Me.BTNBUSCAR.UseVisualStyleBackColor = False
        '
        'BTNACEPTAR
        '
        Me.BTNACEPTAR.Image = Global.Magos.My.Resources.Resources._11
        Me.BTNACEPTAR.Location = New System.Drawing.Point(50, 156)
        Me.BTNACEPTAR.Name = "BTNACEPTAR"
        Me.BTNACEPTAR.Size = New System.Drawing.Size(123, 112)
        Me.BTNACEPTAR.TabIndex = 1
        '
        'TXTNT
        '
        Me.TXTNT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTNT.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNT.Location = New System.Drawing.Point(362, 239)
        Me.TXTNT.MaxLength = 30
        Me.TXTNT.Name = "TXTNT"
        Me.TXTNT.Size = New System.Drawing.Size(214, 38)
        Me.TXTNT.TabIndex = 1083
        Me.TXTNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTNT.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(362, 253)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 24)
        Me.Label2.TabIndex = 1084
        Me.Label2.Text = "No. Tarjeta"
        Me.Label2.Visible = False
        '
        'frmSeleccionaCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 296)
        Me.Controls.Add(Me.BTNBUSCAR)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TXTNT)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BTNCE)
        Me.Controls.Add(Me.TXTTEL)
        Me.Controls.Add(Me.LBLC)
        Me.Controls.Add(Me.TXTC)
        Me.Controls.Add(Me.BTNACEPTAR)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSeleccionaCliente"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Seleccionar al Cliente"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BTNACEPTAR As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BTNBUSCAR As System.Windows.Forms.Button
    Friend WithEvents TXTC As System.Windows.Forms.TextBox
    Friend WithEvents LBLC As System.Windows.Forms.Label
    Friend WithEvents TXTTEL As System.Windows.Forms.MaskedTextBox
    Friend WithEvents BTNCE As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TXTNT As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
