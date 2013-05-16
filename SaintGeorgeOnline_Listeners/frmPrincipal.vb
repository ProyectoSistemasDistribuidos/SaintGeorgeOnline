Public Class frmPrincipal

#Region "Atributos"

    Dim objfrmExportacionCartas As frmExportacionCartas
    Dim objfrmImpresionTalonarios As frmImpresionTalonarios
    Dim objfrmReporteLibretasPrimaria As frmReporteLibretaPrimaria
    Dim objfrmLogin As frmLogin

    ''   Dim objfrmReporteLibretaSecundaria As frmReporteLibretaSecundaria

    Private int_CodigoPerfil As Integer

#End Region

#Region "Propiedades"

    Property CodigoPerfil() As Integer
        Get
            Return int_CodigoPerfil
        End Get
        Set(ByVal value As Integer)
            int_CodigoPerfil = value
        End Set
    End Property

#End Region

#Region "Evitar múltiples instancias"

    Private Shared Instancia As frmPrincipal = Nothing

    Public Shared Function Instance() As frmPrincipal
        If Instancia Is Nothing OrElse Instancia.IsDisposed = True Then
            Instancia = New frmPrincipal
        End If
        Instancia.BringToFront()
        Return Instancia
    End Function

#End Region

#Region "Eventos"

    Private Sub frmPrincipal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Top = 0
        Me.Left = 0
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height
        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Permisos()

    End Sub

    ' Exportacion
    Private Sub CartasDeMorosidadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CartasDeMorosidadToolStripMenuItem.Click
        objfrmExportacionCartas = frmExportacionCartas.Instance
        CargarForm(objfrmExportacionCartas)
    End Sub

    'Impresion
    Private Sub TalonariosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TalonariosToolStripMenuItem.Click
        objfrmImpresionTalonarios = frmImpresionTalonarios.Instance
        CargarForm(objfrmImpresionTalonarios)
    End Sub

    Private Sub LibretasprimariaSecundariaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LibretasprimariaSecundariaToolStripMenuItem.Click
        objfrmReporteLibretasPrimaria = frmReporteLibretaPrimaria.Instance

        CargarForm(objfrmReporteLibretasPrimaria)
    End Sub

    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        objfrmLogin = frmLogin.Instance
        Salir(objfrmLogin)
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarForm(ByRef pForm As Form)
        pForm.MdiParent = Me
        pForm.Show()
    End Sub

    Private Sub Salir(ByRef pForm As Form)
        pForm.Show()
        Me.Hide()
    End Sub

    Private Sub Permisos()

        If CodigoPerfil = 2 Then ' tesoreria
            ExportaciónToolStripMenuItem.Visible = True
            ImpresiónToolStripMenuItem.Visible = True
            ToolStripMenuItem1.Visible = False
        ElseIf CodigoPerfil = 5 Then 'profesores
            ExportaciónToolStripMenuItem.Visible = False
            ImpresiónToolStripMenuItem.Visible = False
            ToolStripMenuItem1.Visible = True
        End If
    End Sub

#End Region

    Private Sub LibretasSecundariaPruebaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '' CargarForm(objfrmReporteLibretaSecundaria)
    End Sub

    Private Sub ConsolidadosPriamariaSecundariaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CargarForm(objfrmReporteLibretasPrimaria)
    End Sub
End Class