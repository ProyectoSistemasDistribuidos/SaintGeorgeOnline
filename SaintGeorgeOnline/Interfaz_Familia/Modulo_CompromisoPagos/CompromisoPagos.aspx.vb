Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports System.Data

''' <summary>
''' Modulo de Consulta de Compromiso de Pago
''' </summary>
''' <remarks>
''' Código del Modulo:    5
''' Código de la Opción:  65
''' </remarks>

Partial Class Interfaz_Familia_Copia_de_Modulo_CronogramaPagos_CompromisoPago
    Inherits System.Web.UI.Page

    Dim int_CodigoModulo As Integer = 1
    Dim int_CodigoOpcion As Integer = 1

#Region "Evento"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Master.MostrarTitulo("Compromiso de Pago")
            If Not Page.IsPostBack Then
                ObtenerCompromisoPagoXcodFamiliaFamiliar()
            End If
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodo"

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     07/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerCompromisoPagoXcodFamiliaFamiliar()
        Dim bl_CompromisoPago As New bl_CompromisosPagos
        Dim ds_Lista As DataSet
        Dim int_CodigoFamilia As Integer = 20090148 'Master.Obtener_CodigoFamiliaActiva 
        Dim int_CodigoFamiliar As Integer = 2 'Master.Obtener_CodigoFamiliarLogueado 
        Dim int_CodigoUsuario As Integer = Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Master.Obtener_CodigoTipoUsuarioLogueado

        ds_Lista = bl_CompromisoPago.FUN_LIS_CompromisoPagoXFamiliaFamiliar(int_CodigoFamilia, int_CodigoFamiliar, _
                                                                                               int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, _
                                                                                               int_CodigoOpcion)
        ViewState("ObtenerCompromisoPagoXcodFamiliaFamiliar") = ds_Lista
        GridView1.DataSource = ds_Lista
        GridView1.DataBind()
    End Sub

    Private Sub imprimir(ByVal int_codigoCompromisoPago As Integer)
        Dim obj_BL_CompromisosPagos As New bl_CompromisosPagos
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_CompromisosPagos.FUN_GET_CompromisosPagos(int_codigoCompromisoPago, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Session("Exportaciones_RepCompromisoPagoHtml") = ds_Lista
        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionCompromisoPago_html();</script>", False)
    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "ExportarWord" Then
                Dim CodCompPago As Integer = CInt(e.CommandArgument.ToString)
                imprimir(CodCompPago)
            End If
        Catch ex As Exception
            'EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

#End Region

End Class
