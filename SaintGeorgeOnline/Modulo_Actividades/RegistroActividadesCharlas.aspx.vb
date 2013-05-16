Imports SaintGeorgeOnline_BusinessEntities.ModuloActividades
Imports SaintGeorgeOnline_DataAccess.ModuloActividades
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloActividades
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Partial Class Modulo_Actividades_RegistroActividadesCharlas
    Inherits System.Web.UI.Page
    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1
    'ok

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Registro de Actividades")

            If Not Page.IsPostBack Then
                cargarComboActividades()
                Actualizar()
                ActualizarRegAsistentes()
            End If

        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Actualizar()
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            buscar()
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscarRegAsistentes_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            buscarRegAsistentes()
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validarGrabar(usp_mensaje) Then
                grabar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnGrabarRegAsistentes_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validarGrabarRegAsistentes(usp_mensaje) Then
                grabarRegAsistentes()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlActividad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Actualizar()
        'buscar()
        Dim dt As DataTable
        dt = New DataTable("listaDatos")
        dt = Datos.agregarColumna(dt, "CodigoConfirmacionAsistencia", "String")
        dt = Datos.agregarColumna(dt, "CodigoProgramacionActividad", "String")
        dt = Datos.agregarColumna(dt, "CodigoFamilia", "String")
        dt = Datos.agregarColumna(dt, "IdFila", "String")
        dt = Datos.agregarColumna(dt, "Familia", "String")
        dt = Datos.agregarColumna(dt, "Cantidad", "String")
        dt = Datos.agregarColumna(dt, "Observacion", "String")

        GridView1.DataSource = dt
        GridView1.DataBind()

    End Sub

    Protected Sub ddlActividadRegAsistentes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ActualizarRegAsistentes()
        'buscar()
        Dim dt As DataTable
        dt = New DataTable("listaDatos")
        dt = Datos.agregarColumna(dt, "CodigoConfirmacionAsistencia", "String")
        dt = Datos.agregarColumna(dt, "CodigoProgramacionActividad", "String")
        dt = Datos.agregarColumna(dt, "CodigoFamilia", "String")
        dt = Datos.agregarColumna(dt, "IdFila", "String")
        dt = Datos.agregarColumna(dt, "Familia", "String")
        dt = Datos.agregarColumna(dt, "Cantidad", "String")
        dt = Datos.agregarColumna(dt, "Observacion", "String")

        GridView2.DataSource = dt
        GridView2.DataBind()

    End Sub

#End Region

#Region "Metodo"

    ''' <summary>
    ''' Exporta los datos del gridView en formato WORD,EXCEL,HTML,PDF,HTML.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Exportar()

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim stream As Stream
        Dim writer As StreamWriter
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obj_BL_ConfirmacionParticipantes As New bl_ConfirmacionParticipantes
        Dim ds_Lista As DataSet = obj_BL_ConfirmacionParticipantes.FUN_REP_ConfirmacionParticipantes(ddlActividad.SelectedValue, rbTipo.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim str_cantidadTotalInscritosConfirmacion As String
        Dim str_cantidadTotalInscritosAsistentes As String
        Dim str_cantidadConfirmacion As String
        Dim str_cantidadAsistentes As String

        str_cantidadConfirmacion = ds_Lista.Tables(1).Rows(0).Item("cantidadConfirmacion")
        str_cantidadAsistentes = ds_Lista.Tables(2).Rows(0).Item("cantidadAsistentes")
        str_cantidadTotalInscritosConfirmacion = ds_Lista.Tables(3).Rows(0).Item("cantidadTotalInscritosConfirmacion")
        str_cantidadTotalInscritosAsistentes = ds_Lista.Tables(4).Rows(0).Item("cantidadTotalInscritosAsistentes")

        Dim dt As DataTable = New DataTable("ListaExportar")

        dt = Datos.agregarColumna(dt, "N°", "String")
        dt = Datos.agregarColumna(dt, "Actividad", "String")
        'dt = Datos.agregarColumna(dt, "Codigo de Familia", "String")
        dt = Datos.agregarColumna(dt, "Nombre Completo", "String")
        dt = Datos.agregarColumna(dt, "Cantidad de confirmados", "String")
        dt = Datos.agregarColumna(dt, "Asistio", "String")
        dt = Datos.agregarColumna(dt, "Cantidad de Asistentes", "String")
        dt = Datos.agregarColumna(dt, "Grado", "String")
        dt = Datos.agregarColumna(dt, "Observación", "String")
        dt = Datos.agregarColumna(dt, "Fecha de Registro", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("Actividad") = dr.Item("Actividad").ToString
            'auxDR.Item("Codigo de Familia") = dr.Item("CodigoFamilia").ToString
            auxDR.Item("Nombre Completo") = dr.Item("Familia").ToString
            auxDR.Item("Cantidad de confirmados") = dr.Item("CantidadConfirmacion").ToString
            auxDR.Item("Asistio") = dr.Item("Asistencia").ToString
            auxDR.Item("Cantidad de Asistentes") = dr.Item("CantidadAsistentes").ToString
            auxDR.Item("Grado") = dr.Item("Grado").ToString
            auxDR.Item("Observación") = dr.Item("Observacion").ToString
            auxDR.Item("Fecha de Registro") = dr.Item("FechaRegistro").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next

        NombreArchivo = Exportacion.ExportarReporteConfirmacionAsistencia(dt, "Confirmación de Asistentes", str_cantidadConfirmacion, str_cantidadAsistentes, str_cantidadTotalInscritosConfirmacion, str_cantidadTotalInscritosAsistentes)
        NombreArchivo = NombreArchivo & ".xls"
        rutamadre = Server.MapPath(".")
        rutamadre = rutamadre.Replace("\Modulo_Actividades", "\Reportes\")

        downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

        Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()



    End Sub

    Private Function validarGrabar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        Dim bool_RegistrosGV As Boolean = False
        For Each gvrS As GridViewRow In GridView1.Rows

            If CType(gvrS.FindControl("chk_Asistencia"), CheckBox).Checked Then 'Si esta seleccionado el check
                Dim tbCantidad As TextBox = CType(gvrS.FindControl("tbCantidad"), TextBox)

                If tbCantidad.Text <= 0 Then
                    bool_RegistrosGV = True
                End If

            End If
        Next



        If bool_RegistrosGV = True Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Cantidad")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Function validarGrabarRegAsistentes(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        Dim bool_RegistrosGV As Boolean = False
        For Each gvrS As GridViewRow In GridView2.Rows

            If CType(gvrS.FindControl("chk_AsistenciaRegAsistentes"), CheckBox).Checked Then 'Si esta seleccionado el check
                Dim tbCantidad As TextBox = CType(gvrS.FindControl("tbCantidadRegAsistentes"), TextBox)

                If tbCantidad.Text <= 0 Then
                    bool_RegistrosGV = True
                End If

            End If
        Next



        If bool_RegistrosGV = True Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "# de Asistentes")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub cargarComboActividades()

        Dim obj_BL_Actividad As New bl_ConfirmacionParticipantes
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Actividad.FUN_LIS_Actividades(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlActividad, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlActividadRegAsistentes, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    Private Sub buscar()

        Dim str_Familia As String = tbFamilia.Text.Trim
        Dim int_Tipo As Integer = rbTipo.SelectedValue
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obj_BL_ConfirmacionParticipantes As New bl_ConfirmacionParticipantes
        Dim ds_Lista As DataSet = obj_BL_ConfirmacionParticipantes.FUN_LIS_ConfirmacionParticipantes(ddlActividad.SelectedValue, int_Tipo, str_Familia, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

    End Sub

    Private Sub buscarRegAsistentes()

        Dim str_Apellido As String = tbApellidoRegAsistentes.Text.Trim
        Dim int_Tipo As Integer = rbTipoRegAsistentes.SelectedValue
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obj_BL_ConfirmacionParticipantes As New bl_ConfirmacionParticipantes
        Dim ds_Lista As DataSet = obj_BL_ConfirmacionParticipantes.FUN_LIS_RegAsistentes(ddlActividadRegAsistentes.SelectedValue, int_Tipo, str_Apellido, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView2.DataSource = ds_Lista.Tables(0)
        GridView2.DataBind()

    End Sub

    Private Sub Actualizar()

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obj_BL_ConfirmacionParticipantes As New bl_ConfirmacionParticipantes
        Dim ds_Lista As DataSet = obj_BL_ConfirmacionParticipantes.FUN_LIS_CantConfirmacionParticipantes(ddlActividad.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        lbl_cantRegistrados.Text = ds_Lista.Tables(0).Rows(0).Item("cantidad")

    End Sub

    Private Sub ActualizarRegAsistentes()

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obj_BL_ConfirmacionParticipantes As New bl_ConfirmacionParticipantes
        Dim ds_Lista As DataSet = obj_BL_ConfirmacionParticipantes.FUN_LIS_CantRegAsistentes(ddlActividadRegAsistentes.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        lblCantidadRegAsistentes.Text = ds_Lista.Tables(0).Rows(0).Item("cantidad")

    End Sub

    Private Sub grabar()
        Dim obj_BL_ConfirmacionParticipantes As New bl_ConfirmacionParticipantes
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""

        Dim dt_Registros As New DataTable
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoConfirmacionAsistencia", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoProgramacionActividad", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CantidadParticipantes", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "Observacion", "string")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoFamilia", "string")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoTrabajadorAsistente", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "Tipo", "integer")
        'dt_Registros = Datos.agregarColumna(dt_Registros, "Fecha", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoTrabajador", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoInvitado", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "Check", "integer")

        Dim dr_R As DataRow

        Dim int_CodigoConfirmacionAsistencia As Integer = 0
        Dim str_CodigoFamilia As String = ""
        Dim int_CodigoTrabajadorAsistente As Integer = 0
        Dim int_CodigoInvitado As Integer = 0
        Dim int_Tipo As Integer = 0
        Dim int_cantidad As Integer = 0
        Dim str_Observacion As String = ""
        Dim int_CodigoTrabajador As Integer = 0
        Dim contObj As Integer = 0

        For Each gvr As GridViewRow In GridView1.Rows

            Dim chk_Asistencia As CheckBox = gvr.FindControl("chk_Asistencia")

            int_CodigoConfirmacionAsistencia = CType(gvr.FindControl("lblCodigoConfirmacionAsistencia"), Label).Text
            str_CodigoFamilia = CType(gvr.FindControl("lblCodigoFamilia"), Label).Text
            int_CodigoTrabajadorAsistente = CType(gvr.FindControl("lblCodigoTrabajador"), Label).Text
            int_Tipo = CType(gvr.FindControl("lblTipo"), Label).Text
            int_cantidad = CType(gvr.FindControl("tbCantidad"), TextBox).Text
            str_Observacion = CType(gvr.FindControl("tbObservacion"), TextBox).Text
            int_CodigoInvitado = CType(gvr.FindControl("lblCodigoInvitado"), Label).Text

            dr_R = dt_Registros.NewRow
            dr_R.Item("CodigoConfirmacionAsistencia") = int_CodigoConfirmacionAsistencia
            dr_R.Item("CodigoProgramacionActividad") = ddlActividad.SelectedValue
            dr_R.Item("CantidadParticipantes") = int_cantidad
            dr_R.Item("Observacion") = str_Observacion
            dr_R.Item("CodigoFamilia") = str_CodigoFamilia
            dr_R.Item("CodigoTrabajadorAsistente") = int_CodigoTrabajadorAsistente
            dr_R.Item("CodigoInvitado") = int_CodigoInvitado
            dr_R.Item("Tipo") = int_Tipo
            'dr_R.Item("CantidadParticipantes") = str_cantidad
            dr_R.Item("CodigoTrabajador") = int_CodigoUsuario
            'dr_R.Item("CodigoFamilia") = str_CodigoFamilia



            If chk_Asistencia.Checked = True Then
                dr_R.Item("Check") = 1
            Else
                dr_R.Item("Check") = 0
            End If

            dt_Registros.Rows.Add(dr_R)

        Next

        usp_valor = obj_BL_ConfirmacionParticipantes.FUN_INS_ConfirmacionParticipantes(dt_Registros, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            buscar()
            Actualizar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "alert")
            'Actualizar()
        End If
    End Sub

    Private Sub grabarRegAsistentes()
        Dim obj_BL_ConfirmacionParticipantes As New bl_ConfirmacionParticipantes
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""

        Dim dt_Registros As New DataTable
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoConfirmacionAsistencia", "integer")
        'dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoProgramacionActividad", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CantidadAsistentes", "integer")
        'dt_Registros = Datos.agregarColumna(dt_Registros, "Observacion", "string")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoFamilia", "string")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoTrabajadorAsistente", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "Tipo", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoTrabajador", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "Check", "integer")
        'dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoTrabajadorAsistente", "integer")
        'dt_Registros = Datos.agregarColumna(dt_Registros, "CheckAsistio", "integer")

        Dim dr_R As DataRow

        Dim int_CodigoConfirmacionAsistencia As Integer = 0
        Dim str_CodigoFamilia As String = ""
        Dim int_CodigoTrabajadorAsistente As Integer = 0
        Dim int_Tipo As Integer = 0
        Dim int_cantidad As Integer = 0
        Dim str_Observacion As String = ""
        Dim int_CodigoTrabajador As Integer = 0
        Dim contObj As Integer = 0

        For Each gvr As GridViewRow In GridView2.Rows

            Dim chk_Asistencia As CheckBox = gvr.FindControl("chk_AsistenciaRegAsistentes")

            int_CodigoConfirmacionAsistencia = CType(gvr.FindControl("lblCodigoConfirmacionAsistenciaRegAsistentes"), Label).Text
            str_CodigoFamilia = CType(gvr.FindControl("lblCodigoFamiliaRegAsistentes"), Label).Text
            int_CodigoTrabajadorAsistente = CType(gvr.FindControl("lblCodigoTrabajadorRegAsistentes"), Label).Text
            int_Tipo = CType(gvr.FindControl("lblTipoRegAsistentes"), Label).Text
            int_cantidad = CType(gvr.FindControl("tbCantidadRegAsistentes"), TextBox).Text
            'str_Observacion = CType(gvr.FindControl("tbObservacion"), TextBox).Text

            dr_R = dt_Registros.NewRow
            dr_R.Item("CodigoConfirmacionAsistencia") = int_CodigoConfirmacionAsistencia
            'dr_R.Item("CodigoProgramacionActividad") = ddlActividad.SelectedValue
            dr_R.Item("CantidadAsistentes") = int_cantidad
            'dr_R.Item("Observacion") = str_Observacion
            dr_R.Item("CodigoFamilia") = str_CodigoFamilia
            dr_R.Item("CodigoTrabajadorAsistente") = int_CodigoTrabajadorAsistente
            dr_R.Item("Tipo") = int_Tipo
            'dr_R.Item("CantidadParticipantes") = str_cantidad
            dr_R.Item("CodigoTrabajador") = int_CodigoUsuario
            'dr_R.Item("CodigoFamilia") = str_CodigoFamilia



            If chk_Asistencia.Checked = True Then
                dr_R.Item("Check") = 1
            Else
                dr_R.Item("Check") = 0
            End If

            dt_Registros.Rows.Add(dr_R)

        Next

        usp_valor = obj_BL_ConfirmacionParticipantes.FUN_INS_RegAsistentes(dt_Registros, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            buscarRegAsistentes()
            ActualizarRegAsistentes()
        Else
            MostrarSexyAlertBox(usp_mensaje, "alert")
            'Actualizar()
        End If
    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     12/12/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     12/12/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    '''     Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub
#End Region


#Region "Eventos del Gridview"
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim chk_Asistencia As CheckBox = e.Row.FindControl("chk_Asistencia")
            Dim str_CodigoFamilia As String = CType(e.Row.FindControl("lblCodigoFamilia"), Label).Text
            Dim str_CodigoConfirmacionAsistencia As String = CType(e.Row.FindControl("lblCodigoConfirmacionAsistencia"), Label).Text

            If str_CodigoConfirmacionAsistencia > 0 Then
                chk_Asistencia.Checked = True
                Dim BGColor As String = "#dcff7d"
                e.Row.Style.Add("background", BGColor)
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim chk_Asistencia As CheckBox = e.Row.FindControl("chk_AsistenciaRegAsistentes")
            Dim str_CodigoFamilia As String = CType(e.Row.FindControl("lblCodigoFamiliaRegAsistentes"), Label).Text
            Dim str_CheckAsistio As String = CType(e.Row.FindControl("lblCheckAsistioRegAsistentes"), Label).Text

            If str_CheckAsistio > 0 Then
                chk_Asistencia.Checked = True
                Dim BGColor As String = "#dcff7d"
                e.Row.Style.Add("background", BGColor)
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If
    End Sub
#End Region


End Class

