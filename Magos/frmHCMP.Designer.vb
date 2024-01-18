<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHCMP
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
        Me.LBLMP = New System.Windows.Forms.Label()
        Me.PANELP = New System.Windows.Forms.Panel()
        Me.BTNANT = New System.Windows.Forms.PictureBox()
        Me.BTNSIG = New System.Windows.Forms.PictureBox()
        Me.BTNVOLVER = New System.Windows.Forms.PictureBox()
        CType(Me.BTNANT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BTNSIG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BTNVOLVER, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LBLMP
        '
        Me.LBLMP.BackColor = System.Drawing.Color.Transparent
        Me.LBLMP.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLMP.Location = New System.Drawing.Point(129, 11)
        Me.LBLMP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBLMP.Name = "LBLMP"
        Me.LBLMP.Size = New System.Drawing.Size(880, 54)
        Me.LBLMP.TabIndex = 10
        Me.LBLMP.Text = "Menu Principal"
        Me.LBLMP.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'PANELP
        '
        Me.PANELP.BackColor = System.Drawing.Color.Transparent
        Me.PANELP.Location = New System.Drawing.Point(4, 86)
        Me.PANELP.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PANELP.Name = "PANELP"
        Me.PANELP.Size = New System.Drawing.Size(1059, 732)
        Me.PANELP.TabIndex = 11
        '
        'BTNANT
        '
        Me.BTNANT.Image = Global.Magos.My.Resources.Resources.ANTERIOR
        Me.BTNANT.Location = New System.Drawing.Point(775, 853)
        Me.BTNANT.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTNANT.Name = "BTNANT"
        Me.BTNANT.Size = New System.Drawing.Size(85, 78)
        Me.BTNANT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BTNANT.TabIndex = 13
        Me.BTNANT.TabStop = False
        '
        'BTNSIG
        '
        Me.BTNSIG.Image = Global.Magos.My.Resources.Resources.SIGUIENTE
        Me.BTNSIG.Location = New System.Drawing.Point(912, 853)
        Me.BTNSIG.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTNSIG.Name = "BTNSIG"
        Me.BTNSIG.Size = New System.Drawing.Size(85, 78)
        Me.BTNSIG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BTNSIG.TabIndex = 12
        Me.BTNSIG.TabStop = False
        '
        'BTNVOLVER
        '
        Me.BTNVOLVER.Image = Global.Magos.My.Resources.Resources.ACTUALIZAR
        Me.BTNVOLVER.Location = New System.Drawing.Point(16, 1)
        Me.BTNVOLVER.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTNVOLVER.Name = "BTNVOLVER"
        Me.BTNVOLVER.Size = New System.Drawing.Size(85, 78)
        Me.BTNVOLVER.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BTNVOLVER.TabIndex = 0
        Me.BTNVOLVER.TabStop = False
        '
        'frmHCMP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.Magos.My.Resources.Resources.Plantilla1
        Me.ClientSize = New System.Drawing.Size(1080, 945)
        Me.Controls.Add(Me.BTNANT)
        Me.Controls.Add(Me.BTNSIG)
        Me.Controls.Add(Me.LBLMP)
        Me.Controls.Add(Me.BTNVOLVER)
        Me.Controls.Add(Me.PANELP)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHCMP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmHCMP"
        CType(Me.BTNANT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BTNSIG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BTNVOLVER, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BTNVOLVER As System.Windows.Forms.PictureBox
    Friend WithEvents LBLMP As System.Windows.Forms.Label
    Friend WithEvents PANELP As System.Windows.Forms.Panel
    Friend WithEvents BTNSIG As System.Windows.Forms.PictureBox
    Friend WithEvents BTNANT As System.Windows.Forms.PictureBox
End Class
