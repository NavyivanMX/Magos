<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCambio
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
        Me.LCAMBIO = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.BTNACEPTAR = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LCAMBIO
        '
        Me.LCAMBIO.AutoSize = True
        Me.LCAMBIO.BackColor = System.Drawing.Color.Transparent
        Me.LCAMBIO.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LCAMBIO.ForeColor = System.Drawing.Color.Black
        Me.LCAMBIO.Location = New System.Drawing.Point(107, 19)
        Me.LCAMBIO.Name = "LCAMBIO"
        Me.LCAMBIO.Size = New System.Drawing.Size(147, 31)
        Me.LCAMBIO.TabIndex = 1
        Me.LCAMBIO.Text = "Su Cambio"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(132, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 25)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Label1"
        '
        'BTNACEPTAR
        '
        Me.BTNACEPTAR.Image = Global.Magos.My.Resources.Resources._11
        Me.BTNACEPTAR.Location = New System.Drawing.Point(113, 121)
        Me.BTNACEPTAR.Name = "BTNACEPTAR"
        Me.BTNACEPTAR.Size = New System.Drawing.Size(122, 76)
        Me.BTNACEPTAR.TabIndex = 1047
        Me.BTNACEPTAR.Tag = "0"
        '
        'frmCambio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(365, 209)
        Me.Controls.Add(Me.BTNACEPTAR)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LCAMBIO)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(267, 101)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCambio"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Cambio"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LCAMBIO As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BTNACEPTAR As System.Windows.Forms.Button
End Class
