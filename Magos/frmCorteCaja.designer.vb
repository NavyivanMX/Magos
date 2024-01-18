<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCorteCaja
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCorteCaja))
        Me.LBLCAJA = New System.Windows.Forms.Label
        Me.BTNCORTE = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LBLCAJA
        '
        Me.LBLCAJA.AutoSize = True
        Me.LBLCAJA.BackColor = System.Drawing.Color.Transparent
        Me.LBLCAJA.Font = New System.Drawing.Font("Arial Rounded MT Bold", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLCAJA.Location = New System.Drawing.Point(12, 210)
        Me.LBLCAJA.Name = "LBLCAJA"
        Me.LBLCAJA.Size = New System.Drawing.Size(57, 17)
        Me.LBLCAJA.TabIndex = 1045
        Me.LBLCAJA.Text = "Cajera"
        '
        'BTNCORTE
        '
        Me.BTNCORTE.Font = New System.Drawing.Font("Arial Rounded MT Bold", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNCORTE.Image = CType(resources.GetObject("BTNCORTE.Image"), System.Drawing.Image)
        Me.BTNCORTE.Location = New System.Drawing.Point(54, 38)
        Me.BTNCORTE.Name = "BTNCORTE"
        Me.BTNCORTE.Size = New System.Drawing.Size(160, 160)
        Me.BTNCORTE.TabIndex = 1044
        Me.BTNCORTE.Text = "Corte de Caja"
        Me.BTNCORTE.UseVisualStyleBackColor = True
        '
        'frmCorteCaja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(276, 258)
        Me.Controls.Add(Me.LBLCAJA)
        Me.Controls.Add(Me.BTNCORTE)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCorteCaja"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Corte de Caja"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BTNCORTE As System.Windows.Forms.Button
    Friend WithEvents LBLCAJA As System.Windows.Forms.Label
End Class
