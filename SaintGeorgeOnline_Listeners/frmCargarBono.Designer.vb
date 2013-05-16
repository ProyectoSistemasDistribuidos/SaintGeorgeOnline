<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCargarBono
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.tbRuta = New System.Windows.Forms.TextBox
        Me.btnCargar = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.btnGrabar = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboAnioAcademico = New System.Windows.Forms.ComboBox
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 74)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(837, 254)
        Me.DataGridView1.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Archivo:"
        '
        'tbRuta
        '
        Me.tbRuta.Location = New System.Drawing.Point(64, 48)
        Me.tbRuta.Name = "tbRuta"
        Me.tbRuta.ReadOnly = True
        Me.tbRuta.Size = New System.Drawing.Size(525, 20)
        Me.tbRuta.TabIndex = 11
        '
        'btnCargar
        '
        Me.btnCargar.Location = New System.Drawing.Point(595, 45)
        Me.btnCargar.Name = "btnCargar"
        Me.btnCargar.Size = New System.Drawing.Size(124, 23)
        Me.btnCargar.TabIndex = 10
        Me.btnCargar.Text = "Cargar Archivo"
        Me.btnCargar.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnGrabar
        '
        Me.btnGrabar.Location = New System.Drawing.Point(725, 45)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(124, 23)
        Me.btnGrabar.TabIndex = 14
        Me.btnGrabar.Text = "Grabar Bono"
        Me.btnGrabar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Periodo:"
        '
        'cboAnioAcademico
        '
        Me.cboAnioAcademico.FormattingEnabled = True
        Me.cboAnioAcademico.Location = New System.Drawing.Point(64, 17)
        Me.cboAnioAcademico.Name = "cboAnioAcademico"
        Me.cboAnioAcademico.Size = New System.Drawing.Size(121, 21)
        Me.cboAnioAcademico.TabIndex = 15
        '
        'frmCargarBono
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(986, 340)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboAnioAcademico)
        Me.Controls.Add(Me.btnGrabar)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbRuta)
        Me.Controls.Add(Me.btnCargar)
        Me.Name = "frmCargarBono"
        Me.Text = "frmCargarBono"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbRuta As System.Windows.Forms.TextBox
    Friend WithEvents btnCargar As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboAnioAcademico As System.Windows.Forms.ComboBox
End Class
