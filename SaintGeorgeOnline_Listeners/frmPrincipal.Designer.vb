<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrincipal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrincipal))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ExportaciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CartasDeMorosidadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImpresiónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TalonariosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.LibretasprimariaSecundariaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportaciónToolStripMenuItem, Me.ImpresiónToolStripMenuItem, Me.ToolStripMenuItem1, Me.SalirToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(824, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ExportaciónToolStripMenuItem
        '
        Me.ExportaciónToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CartasDeMorosidadToolStripMenuItem})
        Me.ExportaciónToolStripMenuItem.Name = "ExportaciónToolStripMenuItem"
        Me.ExportaciónToolStripMenuItem.Size = New System.Drawing.Size(81, 20)
        Me.ExportaciónToolStripMenuItem.Text = "Exportación"
        '
        'CartasDeMorosidadToolStripMenuItem
        '
        Me.CartasDeMorosidadToolStripMenuItem.Name = "CartasDeMorosidadToolStripMenuItem"
        Me.CartasDeMorosidadToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.CartasDeMorosidadToolStripMenuItem.Text = "Cartas de Morosidad"
        '
        'ImpresiónToolStripMenuItem
        '
        Me.ImpresiónToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TalonariosToolStripMenuItem})
        Me.ImpresiónToolStripMenuItem.Name = "ImpresiónToolStripMenuItem"
        Me.ImpresiónToolStripMenuItem.Size = New System.Drawing.Size(72, 20)
        Me.ImpresiónToolStripMenuItem.Text = "Impresión"
        '
        'TalonariosToolStripMenuItem
        '
        Me.TalonariosToolStripMenuItem.Name = "TalonariosToolStripMenuItem"
        Me.TalonariosToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.TalonariosToolStripMenuItem.Text = "Talonarios"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LibretasprimariaSecundariaToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(113, 20)
        Me.ToolStripMenuItem1.Text = "Impresion libretas"
        '
        'LibretasprimariaSecundariaToolStripMenuItem
        '
        Me.LibretasprimariaSecundariaToolStripMenuItem.Name = "LibretasprimariaSecundariaToolStripMenuItem"
        Me.LibretasprimariaSecundariaToolStripMenuItem.Size = New System.Drawing.Size(233, 22)
        Me.LibretasprimariaSecundariaToolStripMenuItem.Text = "Libretas (Primaria/Secundaria)"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.SalirToolStripMenuItem.Text = "Salir"
        '
        'frmPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 399)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmPrincipal"
        Me.Text = "SGO Utilties"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ExportaciónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CartasDeMorosidadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImpresiónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TalonariosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LibretasprimariaSecundariaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
