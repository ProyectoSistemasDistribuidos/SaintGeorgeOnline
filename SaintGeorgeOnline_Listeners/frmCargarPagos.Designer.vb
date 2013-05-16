<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCargarPagos
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
        Me.tbRuta = New System.Windows.Forms.TextBox
        Me.btnCargar = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboConceptos = New System.Windows.Forms.ComboBox
        Me.btnCargarConcepto = New System.Windows.Forms.Button
        Me.btnRegistrarPago = New System.Windows.Forms.Button
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Archivo:"
        '
        'tbRuta
        '
        Me.tbRuta.Location = New System.Drawing.Point(65, 12)
        Me.tbRuta.Name = "tbRuta"
        Me.tbRuta.ReadOnly = True
        Me.tbRuta.Size = New System.Drawing.Size(517, 20)
        Me.tbRuta.TabIndex = 5
        '
        'btnCargar
        '
        Me.btnCargar.Location = New System.Drawing.Point(588, 9)
        Me.btnCargar.Name = "btnCargar"
        Me.btnCargar.Size = New System.Drawing.Size(124, 23)
        Me.btnCargar.TabIndex = 4
        Me.btnCargar.Text = "Cargar Archivo"
        Me.btnCargar.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(16, 51)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1229, 108)
        Me.DataGridView1.TabIndex = 7
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(16, 205)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(1229, 345)
        Me.DataGridView2.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Data Cruda"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 189)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Data Procesada"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 170)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Conceptos"
        '
        'cboConceptos
        '
        Me.cboConceptos.FormattingEnabled = True
        Me.cboConceptos.Location = New System.Drawing.Point(77, 165)
        Me.cboConceptos.Name = "cboConceptos"
        Me.cboConceptos.Size = New System.Drawing.Size(276, 21)
        Me.cboConceptos.TabIndex = 12
        '
        'btnCargarConcepto
        '
        Me.btnCargarConcepto.Location = New System.Drawing.Point(359, 165)
        Me.btnCargarConcepto.Name = "btnCargarConcepto"
        Me.btnCargarConcepto.Size = New System.Drawing.Size(124, 23)
        Me.btnCargarConcepto.TabIndex = 13
        Me.btnCargarConcepto.Text = "Cargar Por Concepto"
        Me.btnCargarConcepto.UseVisualStyleBackColor = True
        '
        'btnRegistrarPago
        '
        Me.btnRegistrarPago.Location = New System.Drawing.Point(489, 165)
        Me.btnRegistrarPago.Name = "btnRegistrarPago"
        Me.btnRegistrarPago.Size = New System.Drawing.Size(124, 23)
        Me.btnRegistrarPago.TabIndex = 14
        Me.btnRegistrarPago.Text = "Registrar Pagos"
        Me.btnRegistrarPago.UseVisualStyleBackColor = True
        '
        'frmCargarPagos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1257, 562)
        Me.Controls.Add(Me.btnRegistrarPago)
        Me.Controls.Add(Me.btnCargarConcepto)
        Me.Controls.Add(Me.cboConceptos)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbRuta)
        Me.Controls.Add(Me.btnCargar)
        Me.Name = "frmCargarPagos"
        Me.Text = "frmCargarPagos"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbRuta As System.Windows.Forms.TextBox
    Friend WithEvents btnCargar As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboConceptos As System.Windows.Forms.ComboBox
    Friend WithEvents btnCargarConcepto As System.Windows.Forms.Button
    Friend WithEvents btnRegistrarPago As System.Windows.Forms.Button
End Class
