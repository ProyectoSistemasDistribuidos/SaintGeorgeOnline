Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos
Imports SaintGeorgeOnline_BusinessLogic.ModuloPermisos
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

''' <summary>
''' Modulo de SubBloque de Menus
''' </summary>
''' <remarks>
''' Código del Modulo:    
''' Código de la Opción:  
''' </remarks>
Partial Class Modulo_Permisos_SubbloquesMenus
    Inherits System.Web.UI.Page

#Region "Eventos"

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("SubBloques de Menus")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                ViewState("SortExpression") = "Descripcion"
                ViewState("Direccion") = "ASC"

                ViewState("SortExpression_ListaBloqueInformacion") = "Descripcion"
                ViewState("Direccion_ListaBloqueInformacion") = "ASC"

                ViewState("SortExpression_DetalleBloqueInformacion") = "Descripcion"
                ViewState("Direccion_DetalleBloqueInformacion") = "ASC"

                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                cargarComboBloqueMenu()
                cargarComboEstadosProcesosProyectos()
                cargarComboSubBloquesPadres(0)

                ViewState("NuevaGrupoInfo") = True
                listar()

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
            MostrarSexyAlertBox(ex.ToString, "Alert")
        End Try

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try

    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        VerRegistro("Inserción")
        limpiarCampos()

    End Sub

    Protected Sub btnCancelar_Click()
        miTab1.Enabled = True
        miTab2.Enabled = False
        lbTab2.Text = "Inserción"
        TabContainer1.ActiveTabIndex = 0
        tbBuscarDescripcion.Focus()
        hd_Codigo.Value = 0

        ViewState("DetalleBloqueInformacion") = Nothing
        ViewState.Remove("DetalleBloqueInformacion")

        ViewState("DetalleAcciones") = Nothing
        ViewState.Remove("DetalleAcciones")

        ViewState("DetalleBloqueInformacion_Original") = Nothing
        ViewState.Remove("DetalleBloqueInformacion_Original")

        ViewState("DetalleAcciones_Original") = Nothing
        ViewState.Remove("DetalleAcciones_Original")

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
            If validar(usp_mensaje) Then
                Grabar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
        
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        limpiarFiltros()

    End Sub

    Protected Sub btn_SubirImagen_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        VerificarArchivo()
    End Sub

    Protected Sub rbTipoSubBloque_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        If rbTipoSubBloque.SelectedValue = 3 Then
            pnl_ListaPadres.Visible = True
        Else
            pnl_ListaPadres.Visible = False
        End If
    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender1.Dispose()
    End Sub

    Protected Sub ddlMenu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMenu.SelectedIndexChanged
        Dim int_BloqueMenu As Integer = IIf(ddlMenu.SelectedValue = "", 0, ddlMenu.SelectedValue)
        cargarComboSubBloquesPadres(int_BloqueMenu)
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Setear permisos de acciones sobre el formulario según la configuración del usuario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(3, 40)

    End Sub

    ''' <summary>
    ''' Exporte el listado de la información filtradaen los diferentes formatos indicados.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
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

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)
        Dim dt As DataTable = New DataTable("ListaExportar")

        dt = Datos.agregarColumna(dt, "N°", "String")
        dt = Datos.agregarColumna(dt, "Sub Bloque", "String")
        dt = Datos.agregarColumna(dt, "Tipo de Bloque", "String")
        dt = Datos.agregarColumna(dt, "Menú", "String")
        dt = Datos.agregarColumna(dt, "Estado de Proceso", "String")
        dt = Datos.agregarColumna(dt, "Estado", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("Sub Bloque") = dr.Item("Descripcion").ToString
            auxDR.Item("Tipo de Bloque") = dr.Item("TipoSubBloque").ToString
            auxDR.Item("Menú") = dr.Item("BloqueMenu").ToString
            auxDR.Item("Estado de Proceso") = dr.Item("EstadoProceso").ToString
            auxDR.Item("Estado") = dr.Item("Estado").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next

        If rbExportar.SelectedValue = 0 Then 'WORD
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Sub Bloques de Menú")
            reporte_html = Arreglo_Datos(0)
            NombreArchivo = Arreglo_Datos(1)
            NombreArchivo = NombreArchivo & ".doc"

            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Mantenimientos_Permisos", "\Reportes\")


            stream = File.OpenWrite(rutamadre & "\" & NombreArchivo)
            writer = New StreamWriter(stream, System.Text.Encoding.UTF8)

            Using (writer)
                writer.Write(reporte_html)
                writer.Flush()
            End Using

            writer.Close()
            downloadBytes = File.ReadAllBytes(rutamadre & "\" & NombreArchivo)

            Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            response.Clear()
            response.AddHeader("Content-Type", "binary/octet-stream")
            response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            response.Flush()
            response.BinaryWrite(downloadBytes)
            response.Flush()
            response.End()

        ElseIf rbExportar.SelectedValue = 1 Then 'EXCEL
            NombreArchivo = Exportacion.ExportarReporte(dt, "Sub Bloques de Menú")
            NombreArchivo = NombreArchivo & ".xls"
            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Mantenimientos_Permisos", "\Reportes\")

            downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

            Response.AddHeader("content-disposition", "attachment;filename=test1.xls")
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()

        ElseIf rbExportar.SelectedValue = 2 Then 'PDF
            Dim m As System.IO.MemoryStream = New System.IO.MemoryStream

            m = Exportacion.ExportarReporte_Pdf(dt, "Sub Bloques de Menú")

            'Exportar
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.OutputStream.Close()
            Response.End()
        ElseIf rbExportar.SelectedValue = 3 Then 'HTML
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Sub Bloques de Menú")
            reporte_html = Arreglo_Datos(0)
            Session("Exportaciones_RepHtml") = reporte_html
            ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresion_html();</script>", False)
        End If

    End Sub

    ''' <summary>
    ''' Bloquea el formulario de busqueda cuando se selecciona la opción de Nuevo Registro.
    ''' </summary>
    ''' <param name="str_Modo"></param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String)
        miTab1.Enabled = False
        miTab2.Enabled = True
        lbTab2.Text = str_Modo
        TabContainer1.ActiveTabIndex = 1
        tbDescripcion.Focus()
    End Sub

    ''' <summary>
    ''' Valida el campo de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     28/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If tbDescripcion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Descripcion")
            result = False
        End If
        If Validacion.ValidarCamposIngreso(tbDescripcion) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Descripcion")
            result = False
        End If

        If ddlEstadoProceso.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Estado Proceso")
            result = False
        End If

        If ddlMenu.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Menú")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Carga la información del seleccionable de bloque de menu
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboBloqueMenu()
        Dim obj_BL_SubbloqueMenu As New bl_BloquesMenus
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_SubbloqueMenu.FUN_LIS_BloqueMenu("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)

        Controles.llenarCombo(ddlBuscarBloqueMenu, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlMenu, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga la información del seleccionable de SubBloques de Menus
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSubBloquesPadres(ByVal Int_CodigoSubBloque As Integer)
        Dim obj_BL_SubbloqueMenu As New bl_SubbloquesMenus
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_SubbloqueMenu.FUN_LIS_SubbloqueMenu(Int_CodigoSubBloque, "", 1, 0, 2, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)
        Controles.llenarCombo(ddlSubMenuPadre, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    ''' <summary>
    ''' Carga la información del seleccionable de Estados de Procesos de Proyectos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboEstadosProcesosProyectos()
        Dim obj_BL_EstadoProcesoProyecto As New bl_EstadosProcesosProyectos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_EstadoProcesoProyecto.FUN_LIS_EstadoProcesoProyecto("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)

        Controles.llenarCombo(ddlEstadoProceso, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlBuscarEstadoProceso, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Limpia los filtros de busqueda.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        tbBuscarDescripcion.Text = ""
        rbEstados.SelectedValue = 1
        tbBuscarDescripcion.Focus()

    End Sub

    ''' <summary>
    ''' Limpia los campos del formulario de registro.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()
        hd_Codigo.Value = 0
        ddlMenu.SelectedValue = 0
        tbDescripcion.Text = ""
        tbLink.Text = ""
        tbRutaDocumentacion.Text = ""
        ddlEstadoProceso.SelectedValue = 0
        GVDetalleBloqueInformacion.DataBind()

    End Sub

    ''' <summary>
    ''' Lista la relación de subbloques de menus.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listar()
       
        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)
        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        If ds_Lista.Tables(0).Rows.Count = 0 Then
            btnExportar.Enabled = False
            rbExportar.Enabled = False
        Else
            btnExportar.Enabled = True
            rbExportar.Enabled = True

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        End If

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet

        Dim int_BloqueMenu As Integer = CInt(ddlBuscarBloqueMenu.SelectedValue)
        Dim str_Descripcion As String = tbBuscarDescripcion.Text.Trim()
        Dim int_Estado As Integer = CInt(rbEstados.SelectedValue)
        Dim int_EstadoProceso As Integer = CInt(ddlBuscarEstadoProceso.SelectedValue)
        Dim int_TipoSubBloque As Integer = CInt(rbBuscarTipoSubBloque.SelectedValue)
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_SubbloqueMenu As New bl_SubbloquesMenus
            ds_Lista = obj_BL_SubbloqueMenu.FUN_LIS_SubbloqueMenu(int_BloqueMenu, str_Descripcion, int_Estado, int_EstadoProceso, int_TipoSubBloque, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_SubbloqueMenu As New bl_SubbloquesMenus
                ds_Lista = obj_BL_SubbloqueMenu.FUN_LIS_SubbloqueMenu(int_BloqueMenu, str_Descripcion, int_Estado, int_EstadoProceso, int_TipoSubBloque, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(3, 40, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Obtiene la información sobre un registro y lo muestra en el formulario.
    ''' </summary>
    ''' <param name="int_Codigo">Codigo de subbloque de menu</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As Integer)

        Dim obj_BL_SubbloqueMenu As New bl_SubbloquesMenus
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_SubbloqueMenu.FUN_GET_SubbloqueMenu(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)

        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("Codigo").ToString)
        ddlMenu.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoBloque").ToString)
        tbDescripcion.Text = ds_Lista.Tables(0).Rows(0).Item("Descripcion").ToString
        tbLink.Text = ds_Lista.Tables(0).Rows(0).Item("LinkPagina").ToString
        tbRutaDocumentacion.Text = ds_Lista.Tables(0).Rows(0).Item("LinkDocumento").ToString
        ddlEstadoProceso.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EstadoProceso").ToString)
        rbTipoSubBloque.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("TipoSubBloque").ToString)
        If rbTipoSubBloque.SelectedValue = 3 Then
            pnl_ListaPadres.Visible = True
        Else
            pnl_ListaPadres.Visible = False
        End If
        ddlSubMenuPadre.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoSubBloquePadre").ToString)

        'Detalle de Bloque de Informacion
        Dim dt_BInf As DataTable
        dt_BInf = New DataTable("DetalleBloqueInformacion")
        dt_BInf = Datos.agregarColumna(dt_BInf, "CodigoRelacion", "Integer")
        dt_BInf = Datos.agregarColumna(dt_BInf, "CodigoBloqueInformacion", "String")
        dt_BInf = Datos.agregarColumna(dt_BInf, "Descripcion", "String")
        dt_BInf = Datos.agregarColumna(dt_BInf, "DescripcionAcciones", "String")
        dt_BInf = Datos.agregarColumna(dt_BInf, "TipoDato", "String")

        If ds_Lista.Tables(1).Rows.Count > 0 Then
            If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
                Dim dr As DataRow
                For Each r As DataRow In ds_Lista.Tables(1).Rows
                    dr = dt_BInf.NewRow
                    dr.Item("CodigoRelacion") = r.Item("CodigoRelacion")
                    dr.Item("CodigoBloqueInformacion") = r.Item("CodigoBloqueInformacion")
                    dr.Item("Descripcion") = r.Item("Descripcion")
                    dr.Item("DescripcionAcciones") = r.Item("DescripcionAcciones")
                    dr.Item("TipoDato") = r.Item("TipoDato")
                    dt_BInf.Rows.Add(dr)
                Next
                ViewState("DetalleBloqueInformacion") = dt_BInf

                hfTotalRegsDetalleBloqueInformacion.Value = CInt(ds_Lista.Tables(1).Rows.Count.ToString)

                GVDetalleBloqueInformacion.DataSource = dt_BInf
                GVDetalleBloqueInformacion.DataBind()
            End If
        End If
        ViewState("DetalleBloqueInformacion_Original") = dt_BInf
        GVDetalleBloqueInformacion.DataSource = dt_BInf
        GVDetalleBloqueInformacion.DataBind()

        'Detalle de Acciones
        Dim dt_Ac As DataTable
        dt_Ac = New DataTable("DetalleAcciones")
        dt_Ac = Datos.agregarColumna(dt_Ac, "CodigoRelacion", "Integer") 'CodigoBloqueInformacion
        dt_Ac = Datos.agregarColumna(dt_Ac, "CodigoBloqueInformacion", "Integer")
        dt_Ac = Datos.agregarColumna(dt_Ac, "CodigoAccion", "Integer")
        dt_Ac = Datos.agregarColumna(dt_Ac, "Descripcion", "String")
        dt_Ac = Datos.agregarColumna(dt_Ac, "TipoDato", "String")

        If ds_Lista.Tables(2).Rows.Count > 0 Then
            If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then
                Dim dr As DataRow
                For Each r As DataRow In ds_Lista.Tables(2).Rows
                    dr = dt_Ac.NewRow
                    dr.Item("CodigoRelacion") = r.Item("CodigoRelacion")
                    dr.Item("CodigoBloqueInformacion") = r.Item("CodigoBloqueInformacion")
                    dr.Item("CodigoAccion") = r.Item("CodigoAccion")
                    dr.Item("Descripcion") = r.Item("Descripcion")
                    dr.Item("TipoDato") = r.Item("TipoDato")
                    dt_Ac.Rows.Add(dr)
                Next
                ViewState("DetalleAcciones") = dt_Ac

                hfTotalRegsListaAcciones.Value = CInt(ds_Lista.Tables(2).Rows.Count.ToString)

            End If
        End If
        ViewState("DetalleAcciones_Original") = dt_Ac

        VerRegistro("Actualización")

    End Sub

    ''' <summary>
    ''' Llama al metodo de Eliminar o Activar según la acción seleccionada.
    ''' </summary>
    ''' <param name="int_Codigo">Codigo de subbloque de menu</param>
    ''' <param name="str_accion">tipo de acción a realizar (Activar o Eliminar)</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)
        Dim obj_BL_SubbloqueMenu As New bl_SubbloquesMenus
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        If str_accion = "Eliminar" Then
            'usp_valor = obj_BL_SubbloqueMenu.FUN_DEL_SubbloqueMenu(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)

            Dim ds_Lista As DataSet = obj_BL_SubbloqueMenu.FUN_DEL_ValidarSubbloqueMenu(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)
            usp_valor = ds_Lista.Tables(0).Rows(0).Item("Codigo")

            If usp_valor > 0 Then

                'usp_valor = obj_BL_SubbloqueMenu.FUN_DEL_SubbloqueMenu(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)

                'Detalle de Bloque de Informacion
                Dim objDT_BloqueInformacion As DataTable
                objDT_BloqueInformacion = New DataTable("DetalleBloqueInformacion")
                objDT_BloqueInformacion = Datos.agregarColumna(objDT_BloqueInformacion, "CodigoRelacion", "Integer")
                objDT_BloqueInformacion = Datos.agregarColumna(objDT_BloqueInformacion, "CodigoBloqueInformacion", "String")
                objDT_BloqueInformacion = Datos.agregarColumna(objDT_BloqueInformacion, "Descripcion", "String")
                objDT_BloqueInformacion = Datos.agregarColumna(objDT_BloqueInformacion, "DescripcionAcciones", "String")
                objDT_BloqueInformacion = Datos.agregarColumna(objDT_BloqueInformacion, "TipoDato", "String")

                If ds_Lista.Tables(1).Rows.Count > 0 Then
                    If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
                        Dim dr As DataRow
                        For Each r As DataRow In ds_Lista.Tables(1).Rows
                            dr = objDT_BloqueInformacion.NewRow
                            dr.Item("CodigoRelacion") = r.Item("CodigoRelacion")
                            dr.Item("CodigoBloqueInformacion") = r.Item("CodigoBloqueInformacion")
                            dr.Item("Descripcion") = r.Item("Descripcion")
                            dr.Item("DescripcionAcciones") = r.Item("DescripcionAcciones")
                            dr.Item("TipoDato") = r.Item("TipoDato")
                            objDT_BloqueInformacion.Rows.Add(dr)
                        Next
                    End If
                End If

                'Detalle de Acciones
                Dim objDT_Acciones As DataTable
                objDT_Acciones = New DataTable("DetalleAcciones")
                objDT_Acciones = Datos.agregarColumna(objDT_Acciones, "CodigoRelacion", "Integer")
                objDT_Acciones = Datos.agregarColumna(objDT_Acciones, "CodigoBloqueInformacion", "Integer")
                objDT_Acciones = Datos.agregarColumna(objDT_Acciones, "CodigoAccion", "Integer")
                objDT_Acciones = Datos.agregarColumna(objDT_Acciones, "Descripcion", "String")
                objDT_Acciones = Datos.agregarColumna(objDT_Acciones, "TipoDato", "String")

                If ds_Lista.Tables(2).Rows.Count > 0 Then
                    If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then
                        Dim dr As DataRow
                        For Each r As DataRow In ds_Lista.Tables(2).Rows
                            dr = objDT_Acciones.NewRow
                            dr.Item("CodigoRelacion") = r.Item("CodigoRelacion")
                            dr.Item("CodigoBloqueInformacion") = r.Item("CodigoBloqueInformacion")
                            dr.Item("CodigoAccion") = r.Item("CodigoAccion")
                            dr.Item("Descripcion") = r.Item("Descripcion")
                            dr.Item("TipoDato") = r.Item("TipoDato")
                            objDT_Acciones.Rows.Add(dr)
                        Next
                    End If
                End If

                'Construyo 5 DataTable
                Dim objDT_BloqueInformacion_AcInsert As New DataTable()
                Dim objDT_BloqueInformacion_AcDelete As New DataTable()
                Dim objDT_BloqueInformacion_AcNoChange As New DataTable()
                Dim objDT_Acciones_AcInsert As New DataTable()
                Dim objDT_Acciones_AcDelete As New DataTable()

                'Clono la estructura del DataTable original
                objDT_BloqueInformacion_AcInsert = objDT_BloqueInformacion.Clone
                objDT_BloqueInformacion_AcDelete = objDT_BloqueInformacion.Clone
                objDT_BloqueInformacion_AcNoChange = objDT_BloqueInformacion.Clone
                objDT_Acciones_AcInsert = objDT_Acciones.Clone
                objDT_Acciones_AcDelete = objDT_Acciones.Clone

                For Each drBI As DataRow In objDT_BloqueInformacion.Rows
                    objDT_BloqueInformacion_AcDelete.ImportRow(drBI)
                Next

                For Each drA As DataRow In objDT_Acciones.Rows
                    objDT_Acciones_AcDelete.ImportRow(drA)
                Next


                objDT_BloqueInformacion_AcInsert.TableName = "Detalle_BloqueInformacion_Insert"
                objDT_BloqueInformacion_AcDelete.TableName = "Detalle_BloqueInformacion_Delete"
                objDT_BloqueInformacion_AcNoChange.TableName = "Detalle_BloqueInformacion_NoChange"
                objDT_Acciones_AcInsert.TableName = "Detalle_Acciones_Insert"
                objDT_Acciones_AcDelete.TableName = "Detalle_Acciones_Delete"

                'Detalle
                Dim objDS_Detalle As New DataSet
                objDS_Detalle.Tables.Add(objDT_BloqueInformacion_AcInsert) 'DataTable.Tables(0) - Detalle de Bloque de Informacion : Registros Nuevos
                objDS_Detalle.Tables.Add(objDT_BloqueInformacion_AcDelete) 'DataTable.Tables(1) - Detalle de Bloque de Informacion : Registros Eliminados
                objDS_Detalle.Tables.Add(objDT_Acciones_AcInsert) 'DataTable.Tables(2) - Detalle de Acciones : Registros Nuevos
                objDS_Detalle.Tables.Add(objDT_Acciones_AcDelete) 'DataTable.Tables(3) - Detalle de Acciones : Registros Eliminados
                objDS_Detalle.Tables.Add(objDT_BloqueInformacion_AcNoChange) 'DataTable.Tables(4) - Detalle de Bloque de Informacion : Registros Constantes

                Dim obj_BE_SubbloqueMenu As New be_SubbloquesMenus
                obj_BE_SubbloqueMenu.CodigoSubBloque = int_Codigo
                usp_valor = obj_BL_SubbloqueMenu.FUN_DEL_SubbloqueMenu(obj_BE_SubbloqueMenu, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)

            Else
                usp_mensaje = ds_Lista.Tables(0).Rows(0).Item("Mensaje")
            End If

        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        listar()
    End Sub

    ''' <summary>
    ''' Graba o Actualiza el registro del subbloque de menu.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_SubbloqueMenu As New be_SubbloquesMenus
        Dim obj_BL_SubbloqueMenu As New bl_SubbloquesMenus
        Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        obj_BE_SubbloqueMenu.CodigoBloque = CInt(ddlMenu.SelectedValue)
        obj_BE_SubbloqueMenu.Descripcion = tbDescripcion.Text.Trim
        obj_BE_SubbloqueMenu.Link = tbLink.Text.Trim
        obj_BE_SubbloqueMenu.RutaDocumentacion = tbRutaDocumentacion.Text.Trim
        obj_BE_SubbloqueMenu.EstadoProceso = CInt(ddlEstadoProceso.SelectedValue)
        obj_BE_SubbloqueMenu.TipoSubBloque = CInt(rbTipoSubBloque.SelectedValue)

        If rbTipoSubBloque.SelectedValue = 3 Then
            obj_BE_SubbloqueMenu.CodigoSubBloquePadre = CInt(ddlSubMenuPadre.SelectedValue)
        Else
            obj_BE_SubbloqueMenu.CodigoSubBloquePadre = 0
        End If

        'Detalle
        Dim objDS_Detalle As New DataSet
        'Detalle de Bloque de Informacion
        Dim objDT_BloqueInformacion As DataTable

        If ViewState("DetalleBloqueInformacion") Is Nothing Then
            objDT_BloqueInformacion = New DataTable("DetalleBloqueInformacion")
            objDT_BloqueInformacion = Datos.agregarColumna(objDT_BloqueInformacion, "CodigoRelacion", "Integer")
            objDT_BloqueInformacion = Datos.agregarColumna(objDT_BloqueInformacion, "CodigoBloqueInformacion", "String")
            objDT_BloqueInformacion = Datos.agregarColumna(objDT_BloqueInformacion, "Descripcion", "String")
            objDT_BloqueInformacion = Datos.agregarColumna(objDT_BloqueInformacion, "DescripcionAcciones", "String")
            objDT_BloqueInformacion = Datos.agregarColumna(objDT_BloqueInformacion, "TipoDato", "String")
        Else
            objDT_BloqueInformacion = ViewState("DetalleBloqueInformacion")
        End If

        'Detalle de Acciones
        Dim objDT_Acciones As DataTable

        If ViewState("DetalleAcciones") Is Nothing Then
            objDT_Acciones = New DataTable("DetalleAcciones")
            objDT_Acciones = Datos.agregarColumna(objDT_Acciones, "CodigoRelacion", "Integer") 'CodigoBloqueInformacion
            objDT_Acciones = Datos.agregarColumna(objDT_Acciones, "CodigoBloqueInformacion", "Integer")
            objDT_Acciones = Datos.agregarColumna(objDT_Acciones, "CodigoAccion", "Integer")
            objDT_Acciones = Datos.agregarColumna(objDT_Acciones, "Descripcion", "String")
            objDT_Acciones = Datos.agregarColumna(objDT_Acciones, "TipoDato", "String")
        Else
            objDT_Acciones = ViewState("DetalleAcciones")
        End If

        If BoolGrabar = 0 Then ' Insert
            objDS_Detalle.Tables.Add(objDT_BloqueInformacion) 'DataTable.Tables(0) - Detalle de Bloque de Informacion : Registros Nuevos
            objDS_Detalle.Tables.Add(objDT_Acciones)          'DataTable.Tables(1) - Detalle de Acciones : Registros Nuevos
            usp_valor = obj_BL_SubbloqueMenu.FUN_INS_SubbloqueMenu(obj_BE_SubbloqueMenu, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)
            GuardarArchivo()
        Else ' Update

            'Construyo 5 DataTable
            Dim objDT_BloqueInformacion_AcInsert As New DataTable()
            Dim objDT_BloqueInformacion_AcDelete As New DataTable()
            Dim objDT_BloqueInformacion_AcNoChange As New DataTable()
            Dim objDT_Acciones_AcInsert As New DataTable()
            Dim objDT_Acciones_AcDelete As New DataTable()

            'Clono la estructura del DataTable original
            objDT_BloqueInformacion_AcInsert = objDT_BloqueInformacion.Clone
            objDT_BloqueInformacion_AcDelete = objDT_BloqueInformacion.Clone
            objDT_BloqueInformacion_AcNoChange = objDT_BloqueInformacion.Clone
            objDT_Acciones_AcInsert = objDT_Acciones.Clone
            objDT_Acciones_AcDelete = objDT_Acciones.Clone

            Dim objDT_BloqueInformacion_Original As DataTable
            objDT_BloqueInformacion_Original = ViewState("DetalleBloqueInformacion_Original")

            Dim objDT_Acciones_Original As DataTable
            objDT_Acciones_Original = ViewState("DetalleAcciones_Original")

            Dim codB, codA As Integer

            'Agrego los registros : "Bloque Informacion Nuevos", al datatable Detalle_BloqueInformacion_Insert
            codB = 0
            For Each dr As DataRow In objDT_BloqueInformacion.Rows
                codB = CInt(dr.Item("CodigoBloqueInformacion").ToString)
                If ExisteEnDetalleBloqueInformacion(codB, objDT_BloqueInformacion_Original) = False Then 'existen en el dt_Final, pero no en el dt_Original
                    Dim newDr As DataRow
                    newDr = objDT_BloqueInformacion_AcInsert.NewRow
                    newDr.Item("CodigoRelacion") = dr.Item("CodigoRelacion")
                    newDr.Item("CodigoBloqueInformacion") = dr.Item("CodigoBloqueInformacion")
                    newDr.Item("Descripcion") = dr.Item("Descripcion")
                    newDr.Item("TipoDato") = dr.Item("TipoDato")
                    objDT_BloqueInformacion_AcInsert.Rows.Add(newDr)
                End If
            Next
            objDT_BloqueInformacion_AcInsert.TableName = "Detalle_BloqueInformacion_Insert"

            'Agrego los registros : "Bloque Informacion Eliminados", al datatable Detalle_BloqueInformacion_Delete
            codB = 0
            For Each dr As DataRow In objDT_BloqueInformacion_Original.Rows
                codB = CInt(dr.Item("CodigoBloqueInformacion").ToString)
                If ExisteEnDetalleBloqueInformacion(codB, objDT_BloqueInformacion) = False Then 'existen en el dt_Original, pero no en el dt_Final 
                    Dim newDr As DataRow
                    newDr = objDT_BloqueInformacion_AcDelete.NewRow
                    newDr.Item("CodigoRelacion") = dr.Item("CodigoRelacion")
                    newDr.Item("CodigoBloqueInformacion") = dr.Item("CodigoBloqueInformacion")
                    newDr.Item("Descripcion") = dr.Item("Descripcion")
                    newDr.Item("TipoDato") = dr.Item("TipoDato")
                    objDT_BloqueInformacion_AcDelete.Rows.Add(newDr)
                End If
            Next
            objDT_BloqueInformacion_AcDelete.TableName = "Detalle_BloqueInformacion_Delete"

            'Agrego los registros : "Bloque Informacion Constantes", al datatable Detalle_BloqueInformacion_NoChange
            codB = 0
            For Each dr As DataRow In objDT_BloqueInformacion_Original.Rows
                codB = CInt(dr.Item("CodigoBloqueInformacion").ToString)
                If ExisteEnDetalleBloqueInformacion(codB, objDT_BloqueInformacion) = True Then 'existen en el dt_Original, y existe en el dt_Final 
                    Dim newDr As DataRow
                    newDr = objDT_BloqueInformacion_AcNoChange.NewRow
                    newDr.Item("CodigoRelacion") = dr.Item("CodigoRelacion")
                    newDr.Item("CodigoBloqueInformacion") = dr.Item("CodigoBloqueInformacion")
                    newDr.Item("Descripcion") = dr.Item("Descripcion")
                    newDr.Item("TipoDato") = dr.Item("TipoDato")
                    objDT_BloqueInformacion_AcNoChange.Rows.Add(newDr)
                End If
            Next
            objDT_BloqueInformacion_AcNoChange.TableName = "Detalle_BloqueInformacion_NoChange"

            'Agrego los registros : "Acciones Nuevos", al datatable Detalle_Insert
            codB = 0
            codA = 0
            For Each dr As DataRow In objDT_Acciones.Rows
                codB = CInt(dr.Item("CodigoBloqueInformacion").ToString)
                codA = CInt(dr.Item("CodigoAccion").ToString)
                If ExisteEnDetalleAcciones(codB, codA, objDT_Acciones_Original) = False Then 'existen en el dt_Final, pero no en el dt_Original
                    Dim newDr As DataRow
                    newDr = objDT_Acciones_AcInsert.NewRow
                    newDr.Item("CodigoRelacion") = dr.Item("CodigoRelacion")
                    newDr.Item("CodigoBloqueInformacion") = dr.Item("CodigoBloqueInformacion")
                    newDr.Item("CodigoAccion") = dr.Item("CodigoAccion")
                    newDr.Item("Descripcion") = dr.Item("Descripcion")
                    newDr.Item("TipoDato") = dr.Item("TipoDato")
                    objDT_Acciones_AcInsert.Rows.Add(newDr)
                End If
            Next
            objDT_Acciones_AcInsert.TableName = "Detalle_Acciones_Insert"

            'Agrego los registros : "Acciones Eliminados", al datatable Detalle_Acciones_Delete
            codB = 0
            codA = 0
            For Each dr As DataRow In objDT_Acciones_Original.Rows
                codB = CInt(dr.Item("CodigoBloqueInformacion").ToString)
                codA = CInt(dr.Item("CodigoAccion").ToString)
                If ExisteEnDetalleAcciones(codB, codA, objDT_Acciones) = False Then 'existen en el dt_Original, pero no en el dt_Final 
                    Dim newDr As DataRow
                    newDr = objDT_Acciones_AcDelete.NewRow
                    newDr.Item("CodigoRelacion") = dr.Item("CodigoRelacion")
                    newDr.Item("CodigoBloqueInformacion") = dr.Item("CodigoBloqueInformacion")
                    newDr.Item("CodigoAccion") = dr.Item("CodigoAccion")
                    newDr.Item("Descripcion") = dr.Item("Descripcion")
                    newDr.Item("TipoDato") = dr.Item("TipoDato")
                    objDT_Acciones_AcDelete.Rows.Add(newDr)
                End If
            Next
            objDT_Acciones_AcDelete.TableName = "Detalle_Acciones_Delete"

            objDS_Detalle.Tables.Add(objDT_BloqueInformacion_AcInsert) 'DataTable.Tables(0) - Detalle de Bloque de Informacion : Registros Nuevos
            objDS_Detalle.Tables.Add(objDT_BloqueInformacion_AcDelete) 'DataTable.Tables(1) - Detalle de Bloque de Informacion : Registros Eliminados
            objDS_Detalle.Tables.Add(objDT_Acciones_AcInsert) 'DataTable.Tables(2) - Detalle de Acciones : Registros Nuevos
            objDS_Detalle.Tables.Add(objDT_Acciones_AcDelete) 'DataTable.Tables(3) - Detalle de Acciones : Registros Eliminados

            objDS_Detalle.Tables.Add(objDT_BloqueInformacion_AcNoChange) 'DataTable.Tables(4) - Detalle de Bloque de Informacion : Registros Constantes
           
            obj_BE_SubbloqueMenu.CodigoSubBloque = CInt(BoolGrabar)
            usp_valor = obj_BL_SubbloqueMenu.FUN_UPD_SubbloqueMenu(obj_BE_SubbloqueMenu, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 40)
            GuardarArchivo()
        End If

        If usp_valor > 0 Then

            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnCancelar_Click()
            limpiarCampos()
            listar()

            ViewState.Remove("DetalleBloqueInformacion")
            ViewState.Remove("DetalleAcciones")
            ViewState.Remove("DetalleBloqueInformacion_Original")
            ViewState.Remove("DetalleAcciones_Original")

        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    Private Function ExisteEnDetalleBloqueInformacion(ByVal codigo As Integer, ByVal dt As DataTable) As Boolean

        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("CodigoBloqueInformacion") = codigo Then
                Return True
            End If
        Next
        Return False

    End Function

    Private Function ExisteEnDetalleAcciones(ByVal codB As Integer, ByVal codA As Integer, ByVal dt As DataTable) As Boolean

        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("CodigoBloqueInformacion") = codB And dt.Rows(i).Item("CodigoAccion") = codA Then
                Return True
            End If
        Next
        Return False

    End Function

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

    ''' <summary>
    ''' Exporta la documentación adjunta el subbloque de menu.
    ''' </summary>
    ''' <param name="NombreDocumento">Nombre de documento a exportar</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ExportarDocumento(ByVal NombreDocumento As String)

        Dim downloadBytes As Byte()
        Dim rutamadre As String = ""
        Dim reporte_html As String = ""
        Dim NombreArchivo As String = ""

        rutamadre = ConfigurationManager.AppSettings.Item("RutaDocuMenu_Local").ToString()
        NombreArchivo = NombreDocumento

        downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

        Response.AddHeader("content-disposition", "attachment;filename=documentacion.vsd")
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()

    End Sub

    ''' <summary>
    ''' Verifica y sube el archivo seleccionado en memoria para su proximo copiado al servidor.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function VerificarArchivo() As Boolean
        Dim sFileName As String = ""
        Dim FullFileName As String = ""
        Dim ruta As String = ConfigurationManager.AppSettings.Item("RutaDocuMenu_Local").ToString()
        Dim valida As Boolean = True

        If FileUpload1.PostedFile.ContentLength > 0 Then
            If (FileUpload1.PostedFile.ContentLength < 1048576) Then
                sFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
                tbRutaDocumentacion.Text = sFileName

                Dim filelen As Long = FileUpload1.PostedFile.InputStream.Length
                Dim buffer(filelen) As Byte

                FileUpload1.PostedFile.InputStream.Read(buffer, 0, filelen)

                ViewState("ImagenMenu") = buffer
                GuardarArchivo()
            Else
                valida = False
                ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", "alert('Imagen Adjunta no puede pasar de los 200 Kb!!!')", True)
            End If
        Else
            valida = False
            ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", "alert('Debe seleccionar una imagen!!!')", True)
        End If

        Return valida

    End Function

    ''' <summary>
    ''' Garda el archivo en memoria en el servidor.
    ''' </summary>
    ''' <returns>indicador del exito de la operación</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function GuardarArchivo() As Boolean
        Dim sFileName As String = ""
        Dim FullFileName As String = ""
        Dim ruta As String = ConfigurationManager.AppSettings.Item("RutaDocuMenu_Local").ToString()
        Dim valida As Boolean = True
        Dim NombreArchivo As String = ""

        If Not ViewState("ImagenMenu") Is Nothing Then
            Dim buffer() As Byte
            buffer = ViewState("ImagenMenu")
            NombreArchivo = tbRutaDocumentacion.Text.Trim

            Dim file As System.IO.FileStream = New System.IO.FileStream(ruta + NombreArchivo, FileMode.Create, FileAccess.Write)

            For Each b As Byte In buffer
                file.WriteByte(b)
            Next
            file.Close()

        End If

        Return valida
    End Function



    ''' <summary>
    ''' Retorna el DataSet con la lista del detalle de campos de informacion cargados en el ViewState(memoria)
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda_DetalleBloquesInformacion() As DataSet
        Dim ds_Lista As New DataSet
        Dim dt As New DataTable
        If ViewState("DetalleBloqueInformacion") Is Nothing Then
            dt = New DataTable("DetalleBloqueInformacion")
            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoBloqueInformacion", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
            dt = Datos.agregarColumna(dt, "DescripcionAcciones", "String")
            dt = Datos.agregarColumna(dt, "TipoDato", "String")
        Else
            dt = ViewState("DetalleBloqueInformacion")
        End If

        ds_Lista.Tables.Add(dt)

        Return ds_Lista

    End Function


    ''' <summary>
    ''' Carga el combo con la lista de Campos de información disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarGrillaBloquesInformacion()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda_ListaBloquesInformacion(1)

        hfTotalRegsListaBloqueInformacion.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GVListaBloqueInformacion.DataSource = ds_Lista.Tables(0)
        GVListaBloqueInformacion.DataBind()

        If ds_Lista.Tables(0).Rows.Count > 0 Then
            SortGridView_ListaBloqueInformacion(ViewState("SortExpression_ListaBloqueInformacion"), ViewState("Direccion_ListaBloqueInformacion"))
            ImagenSorting_ListaBloqueInformacion()
        End If

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda_ListaBloquesInformacion(ByVal int_Modo As Integer) As DataSet

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim int_Tipo As Integer = 0

        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_BloquesInformacion As New bl_BloquesInformacion
            ds_Lista = obj_BL_BloquesInformacion.FUN_LIS_BloquesInformacion(str_Descripcion, int_Estado, int_Tipo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            ds_Lista.Tables(0).TableName = "ListaBloqueInformacion"
            Dim dt As New DataTable()
            dt = ds_Lista.Tables(0).Copy

            Dim column As New DataColumn("Check")
            With column
                .DataType = System.Type.GetType("System.Int32")
                .DefaultValue = 0
            End With
            dt.Columns.Add(column)

            ds_Lista.Tables.Remove("ListaBloqueInformacion")
            ds_Lista.Tables.Add(dt)

            ViewState("ListaBloqueInformacion") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("ListaBloqueInformacion") Is Nothing Then

                Dim obj_BL_BloquesInformacion As New bl_BloquesInformacion
                ds_Lista = obj_BL_BloquesInformacion.FUN_LIS_BloquesInformacion(str_Descripcion, int_Estado, int_Tipo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                ds_Lista.Tables(0).TableName = "ListaBloqueInformacion"
                Dim dt As New DataTable()
                dt = ds_Lista.Tables(0).Copy

                Dim column As New DataColumn("Check")
                With column
                    .DataType = System.Type.GetType("System.Int32")
                    .DefaultValue = 0
                End With
                dt.Columns.Add(column)

                ds_Lista.Tables.Remove("ListaBloqueInformacion")
                ds_Lista.Tables.Add(dt)

                ViewState("ListaBloqueInformacion") = ds_Lista
            Else
                ds_Lista = ViewState("ListaBloqueInformacion")
            End If
        End If

        Return ds_Lista
    End Function

#End Region


#Region "Mantenimiento de detalle de Bloque de Informacion"

#Region "Eventos"
    Protected Sub btnAgregarBloqueInformacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim dt, dtA As DataTable

        'En caso no exista el VS DetalleBloqueInformacion, le creo la estructura y lo subo a memoria
        If ViewState("DetalleBloqueInformacion") Is Nothing Then
            dt = New DataTable("DetalleBloqueInformacion")
            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoBloqueInformacion", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
            dt = Datos.agregarColumna(dt, "DescripcionAcciones", "String")
            dt = Datos.agregarColumna(dt, "TipoDato", "String")
        Else
            dt = ViewState("DetalleBloqueInformacion")
        End If

        ViewState("DetalleBloqueInformacion") = dt

        cargarGrillaBloquesInformacion()
        pnModalBloqueInformacion.Show()

        cargarViewStateAcciones() 'ViewState("ListaAcciones") : dt
        'dtA = ViewState("ListaAcciones")

        'GVListaAcciones.DataSource = dtA
        'GVListaAcciones.DataBind()

    End Sub

    Protected Sub btnModalAceptarBloqueInformacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            int_CodigoAccion = 7
            agregarBloqueInformacion()
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarBloqueInformacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalBloqueInformacion()
    End Sub
#End Region
#Region "Métodos"
    Private Function validarAgregarDetalle(ByVal codigo As Integer, ByVal dtOriginal As DataTable) As Boolean

        For i As Integer = 0 To dtOriginal.Rows.Count - 1
            If dtOriginal.Rows(i).Item("CodigoBloqueInformacion") = codigo.ToString Then
                Return False
            End If
        Next
        Return True

    End Function

    Private Function validarCheckDetalle(ByVal codigo As Integer, ByVal dtOriginal As DataTable) As Boolean
        For i As Integer = 0 To dtOriginal.Rows.Count - 1
            If dtOriginal.Rows(i).Item("CodigoBloqueInformacion") = codigo.ToString Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Agrega 1 Bloque de informacion al detalle 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarBloqueInformacion()

        Dim dt As DataTable

        If ViewState("DetalleBloqueInformacion") Is Nothing Then
            dt = New DataTable("DetalleBloqueInformacion")
            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoBloqueInformacion", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
            dt = Datos.agregarColumna(dt, "DescripcionAcciones", "String")
            dt = Datos.agregarColumna(dt, "TipoDato", "String")
        Else
            dt = ViewState("DetalleBloqueInformacion")
        End If

        Dim ds_Lista As New DataSet
        ds_Lista = ViewState("ListaBloqueInformacion")

        If ds_Lista.Tables(0).Rows.Count > 0 Then
            For Each VSdr As DataRow In ds_Lista.Tables(0).Rows
                If VSdr.Item("Check") = 1 Then
                    Dim dr As DataRow
                    dr = dt.NewRow

                    Dim autoCode As Integer
                    If dt.Rows.Count = 0 Then
                        autoCode = 1
                    Else ' >0
                        If dt.Rows.Count = 1 Then
                            autoCode = IIf(dt.Rows(0).Item("CodigoRelacion") = -1, 1, dt.Compute("Max(CodigoRelacion)", "") + 1)
                        Else
                            autoCode = dt.Compute("Max(CodigoRelacion)", "") + 1
                        End If
                    End If

                    Dim cod As Integer = VSdr.Item("Codigo") 'Codigo del Bloque de Informacion de la lista ViewState("ListaBloqueInformacion")
                    dr.Item("CodigoRelacion") = autoCode
                    dr.Item("CodigoBloqueInformacion") = cod
                    dr.Item("Descripcion") = VSdr.Item("Descripcion")
                    dr.Item("DescripcionAcciones") = ""
                    dr.Item("TipoDato") = "T"

                    If validarAgregarDetalle(cod, dt) Then 'Si el codigo del Bloque de Informacion no esta en mi lista ViewState("DetalleBloqueInformacion")
                        dt.Rows.Add(dr)
                    End If

                End If
            Next
        End If

        GVListaBloqueInformacion.DataBind()

        ViewState("DetalleBloqueInformacion") = dt
        GVDetalleBloqueInformacion.DataSource = dt
        GVDetalleBloqueInformacion.DataBind()

        SortGridView_DetalleBloqueInformacion(ViewState("SortExpression_DetalleBloqueInformacion"), ViewState("Direccion_DetalleBloqueInformacion"))
        ImagenSorting_DetalleBloqueInformacion()

        upBloqueInformacion.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 registro de Detalle de informacion de el detalle 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarBloqueInformacion(ByVal int_Codigo As Integer)
        Dim dt As DataTable
        dt = ViewState("DetalleBloqueInformacion")
        For Each auxdr As DataRow In dt.Rows
            'If auxdr.Item("CodigoBloqueInformacion") = int_Codigo Then
            If auxdr.Item("CodigoRelacion") = int_Codigo Then
                auxdr.Delete()
                Exit For
            End If
        Next
        dt.AcceptChanges()
        ViewState("DetalleBloqueInformacion") = dt
        GVDetalleBloqueInformacion.DataSource = dt
        GVDetalleBloqueInformacion.DataBind()

        'Elimino todos las acciones vinculadas al bloque de Informacion
        Dim dtA As DataTable

        If ViewState("DetalleAcciones") Is Nothing Then
            dtA = New DataTable("DetalleAcciones")
            dtA = Datos.agregarColumna(dtA, "CodigoRelacion", "Integer")
            dtA = Datos.agregarColumna(dtA, "CodigoBloqueInformacion", "Integer")
            dtA = Datos.agregarColumna(dtA, "CodigoAccion", "Integer")
            dtA = Datos.agregarColumna(dtA, "Descripcion", "String")
            dtA = Datos.agregarColumna(dtA, "TipoDato", "String")
        Else
            dtA = ViewState("DetalleAcciones")
        End If

        Dim arrayAccion As New ArrayList
        For Each auxdr As DataRow In dtA.Rows
            If auxdr.Item("CodigoBloqueInformacion") = int_Codigo Then
                arrayAccion.Add(auxdr.Item("CodigoRelacion"))
            End If
        Next

        For i As Integer = 0 To arrayAccion.Count - 1
            For Each auxdr As DataRow In dtA.Rows
                If auxdr.Item("CodigoRelacion") = arrayAccion.Item(i) Then
                    auxdr.Delete()
                    Exit For
                End If
            Next
            dtA.AcceptChanges()
        Next

        ViewState("DetalleAcciones") = dtA
        upBloqueInformacion.Update()

        SortGridView_DetalleBloqueInformacion(ViewState("SortExpression_DetalleBloqueInformacion"), ViewState("Direccion_DetalleBloqueInformacion"))
        ImagenSorting_DetalleBloqueInformacion()

        upBloqueInformacion.Update()
    End Sub

    ''' <summary>
    ''' Cierra el modal Bloque Informacion
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalBloqueInformacion()
        GVListaBloqueInformacion.DataBind()
        pnModalBloqueInformacion.Hide()
    End Sub
#End Region

#Region "Eventos del Gridview de Detalle de Bloque de Informacion"

    Protected Sub ddlPageSelectorDetalleBloqueInformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GVDetalleBloqueInformacion.PageCount Then
                Me.GVDetalleBloqueInformacion.PageIndex = _NumPag - 1
            Else
                Me.GVDetalleBloqueInformacion.PageIndex = 0
            End If

            Me.GVDetalleBloqueInformacion.SelectedIndex = -1

            SortGridView_DetalleBloqueInformacion(ViewState("SortExpression_DetalleBloqueInformacion"), ViewState("Direccion_DetalleBloqueInformacion"))
            ImagenSorting_DetalleBloqueInformacion()

        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDetalleBloqueInformacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Eliminar" Or e.CommandName = "AsignarAccion" Then
                Dim int_Codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Eliminar" Then 'CodigoBloqueInformacion
                    int_CodigoAccion = 3
                    eliminarBloqueInformacion(int_Codigo)
                ElseIf e.CommandName = "AsignarAccion" Then 'CodigoRelacion
                    Dim btnAccion As ImageButton = CType(row.FindControl("btnAcciones"), ImageButton)
                    Dim int_CodigoRelacion As Integer = btnAccion.CommandArgument.ToString
                    MostrarPanelAcciones(int_CodigoRelacion)
                    'MostrarPanelAcciones(int_Codigo)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDetalleBloqueInformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            If e.Row.RowType = DataControlRowType.Pager Then

                Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas_DetalleBloqueInformacion")
                _TotalPags.Text = GVDetalleBloqueInformacion.PageCount.ToString

                Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales_DetalleBloqueInformacion")
                _Registros.Text = InformacionPager_DetalleBloqueInformacion(GVDetalleBloqueInformacion, e.Row, Me)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDetalleBloqueInformacion_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GVDetalleBloqueInformacion.PageIndex = e.NewPageIndex
            End If

            SortGridView_DetalleBloqueInformacion(ViewState("SortExpression_DetalleBloqueInformacion"), ViewState("Direccion_DetalleBloqueInformacion"))
            ImagenSorting_DetalleBloqueInformacion()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDetalleBloqueInformacion_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            ViewState("SortExpression_DetalleBloqueInformacion") = sortExpression

            If GridViewSortDirection_DetalleBloqueInformacion = SortDirection.Ascending Then
                GridViewSortDirection_DetalleBloqueInformacion = SortDirection.Descending
                SortGridView_DetalleBloqueInformacion(sortExpression, "DESC")
                ViewState("Direccion_DetalleBloqueInformacion") = "DESC"
            Else
                GridViewSortDirection_DetalleBloqueInformacion = SortDirection.Ascending
                SortGridView_DetalleBloqueInformacion(sortExpression, "ASC")
                ViewState("Direccion_DetalleBloqueInformacion") = "ASC"
            End If

            ImagenSorting_DetalleBloqueInformacion()
        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDetalleBloqueInformacion_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager_DetalleBloqueInformacion(GVDetalleBloqueInformacion, e.Row, Me)
        End If

    End Sub

#End Region

#Region "Metodos del Gridview de Detalle de Bloque de Informacion"

    ''' <summary>
    ''' Agrega el índice de páginas al combo de páginación. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CrearBotonesPager_DetalleBloqueInformacion(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page)

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim ddlPageSelector As DropDownList = DirectCast(gvPagerRow.FindControl("ddlPageSelectorDetalleBloqueInformacion"), DropDownList)
        ddlPageSelector.Items.Clear()

        For i As Integer = 1 To gridView.PageCount
            ddlPageSelector.Items.Add(i.ToString())
        Next

        ddlPageSelector.SelectedIndex = pageIndex

    End Sub

    ''' <summary>
    ''' Muestra la numeración de registros por página y cantidad total de registros del listado actual. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function InformacionPager_DetalleBloqueInformacion(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page) As String

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim pageSize As Integer = gridView.PageSize
        Dim rowCount As Integer = gridView.Rows.Count

        Dim currentPageFirstRow As Integer = ((pageIndex * pageSize) + 1)
        Dim currentPageLastRow As Integer = 0
        Dim lastPageRemainder As Integer = pageCount Mod pageSize

        currentPageLastRow = currentPageFirstRow + rowCount - 1

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegsDetalleBloqueInformacion.Value)

    End Function

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Property GridViewSortDirection_DetalleBloqueInformacion() As SortDirection

        Get
            If ViewState("sortDirection_DetalleBloqueInformacion") Is Nothing Then
                ViewState("sortDirection_DetalleBloqueInformacion") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("sortDirection_DetalleBloqueInformacion"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection_DetalleBloqueInformacion") = value
        End Set

    End Property

    ''' <summary>
    ''' Lista los datos de procedimientos realizados ordenados por Descripción.
    ''' </summary>
    ''' <param name="sortExpression">Bloque por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView_DetalleBloqueInformacion(ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda_DetalleBloquesInformacion()

        hfTotalRegsDetalleBloqueInformacion.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        GVDetalleBloqueInformacion.DataSource = dv
        GVDetalleBloqueInformacion.DataBind()

    End Sub

    ''' <summary>
    ''' Cambia la imagen dependiendo el Bloque y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting_DetalleBloqueInformacion()

        Dim _btnSorting As ImageButton = CType(GVDetalleBloqueInformacion.HeaderRow.FindControl("btnSorting"), ImageButton)

        If ViewState("Direccion_DetalleBloqueInformacion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion_DetalleBloqueInformacion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
            _btnSorting.ToolTip = "Ascendente"
        End If

    End Sub

#End Region

#End Region

#Region "Manejo de GridView Lista de Bloque de Informacion"

#Region "Eventos del Gridview de Lista de Bloque de Informacion"

    Protected Sub chkSeleccionar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cambiarEstadoCheckBox(sender, e)
            pnModalBloqueInformacion.Show()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPageSelectorListaBloqueInformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GVListaBloqueInformacion.PageCount Then
                Me.GVListaBloqueInformacion.PageIndex = _NumPag - 1
            Else
                Me.GVListaBloqueInformacion.PageIndex = 0
            End If

            Me.GVListaBloqueInformacion.SelectedIndex = -1

            SortGridView_ListaBloqueInformacion(ViewState("SortExpression_ListaBloqueInformacion"), ViewState("Direccion_ListaBloqueInformacion"))
            ImagenSorting_ListaBloqueInformacion()
            pnModalBloqueInformacion.Show()

        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaBloqueInformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim dt As DataTable
        Dim dtChk As DataTable
        Dim ds_Lista As DataSet

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas_ListaBloqueInformacion")
            _TotalPags.Text = GVListaBloqueInformacion.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales_ListaBloqueInformacion")
            _Registros.Text = InformacionPager_ListaBloqueInformacion(GVListaBloqueInformacion, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            If ViewState("DetalleBloqueInformacion") IsNot Nothing Then
                dt = ViewState("DetalleBloqueInformacion")
                Dim cod As Integer = CInt(e.Row.Cells.Item(1).Text)
                Dim chk As CheckBox = CType(e.Row.FindControl("chkSeleccionar"), CheckBox)

                'Actualiza la columna "Check" del DataTable(ViewState) "ListaBloqueInformacion"
                If validarCheckDetalle(cod, dt) Then
                    Dim BGColor As String = "#dcff7d"
                    e.Row.Style.Add("background", BGColor)
                    chk.Checked = True

                    'Actualizo el Check del VS ListaBloqueInformacion
                    ds_Lista = ViewState("ListaBloqueInformacion")

                    Dim VSdt As New DataTable()
                    VSdt = ds_Lista.Tables(0).Copy

                    ds_Lista.Tables.Remove("ListaBloqueInformacion")

                    VSdt = MarcarCheckbox(VSdt, cod, 1)

                    ds_Lista.Tables.Add(VSdt)

                    ViewState("ListaBloqueInformacion") = ds_Lista
                End If

                'Marca los Checkbox del gridview
                ds_Lista = ViewState("ListaBloqueInformacion")
                dtChk = ds_Lista.Tables(0).Copy
                If validarEstadoCheckDetalle(cod, dtChk) Then
                    chk.Checked = True
                End If

            End If
            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If

    End Sub

    Protected Sub GVListaBloqueInformacion_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GVListaBloqueInformacion.PageIndex = e.NewPageIndex
            End If

            SortGridView_ListaBloqueInformacion(ViewState("SortExpression_ListaBloqueInformacion"), ViewState("Direccion_ListaBloqueInformacion"))
            ImagenSorting_ListaBloqueInformacion()
            pnModalBloqueInformacion.Show()

        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaBloqueInformacion_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            ViewState("SortExpression_ListaBloqueInformacion") = sortExpression

            If GridViewSortDirection_ListaBloqueInformacion = SortDirection.Ascending Then
                GridViewSortDirection_ListaBloqueInformacion = SortDirection.Descending
                SortGridView_ListaBloqueInformacion(sortExpression, "DESC")
                ViewState("Direccion_ListaBloqueInformacion") = "DESC"
            Else
                GridViewSortDirection_ListaBloqueInformacion = SortDirection.Ascending
                SortGridView_ListaBloqueInformacion(sortExpression, "ASC")
                ViewState("Direccion_ListaBloqueInformacion") = "ASC"
            End If

            ImagenSorting_ListaBloqueInformacion()
            pnModalBloqueInformacion.Show()

        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaBloqueInformacion_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager_ListaBloqueInformacion(GVListaBloqueInformacion, e.Row, Me)
        End If

    End Sub
#End Region

#Region "Metodos del Gridview de Lista de Bloque de Informacion"

    ''' <summary>
    ''' Cambia el estado de checkbox clickeado
    ''' </summary>
    ''' <param name="sender">Hace referencia al checkbox que ha sido clickeado</param>
    ''' <param name="e">Argumentos usados durante el Evento</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cambiarEstadoCheckBox(ByVal sender As Object, ByVal e As EventArgs)

        Dim chk As CheckBox = TryCast(sender, CheckBox)
        Dim estado As Boolean = chk.Checked
        Dim row As GridViewRow = TryCast(chk.NamingContainer, GridViewRow)
        Dim codigo As Integer = CInt(row.Cells.Item(1).Text)

        Dim ds_Lista As New DataSet
        ds_Lista = ViewState("ListaBloqueInformacion")

        Dim dt As New DataTable()
        dt = ds_Lista.Tables(0).Copy

        ds_Lista.Tables.Remove("ListaBloqueInformacion")       

        If estado Then
            dt = MarcarCheckbox(dt, codigo, 1)
        Else
            dt = MarcarCheckbox(dt, codigo, 0)
        End If

        ds_Lista.Tables.Add(dt)

        ViewState("ListaBloqueInformacion") = ds_Lista

    End Sub

    ''' <summary>
    ''' Actualiza el estado del checkbox clickeado en el DataTable(ViewState) "ListaBloquesInformacion"
    ''' </summary>
    ''' <param name="dt">DataTable en donde realizare la actualizacion del valor del campo "Check"</param>
    ''' <param name="codigo">codigo del checkbox a ser buscado</param>
    ''' <param name="accion">Especifica si el checkbox sera marcado o desmarcado</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function MarcarCheckbox(ByVal dt As DataTable, ByVal codigo As Integer, ByVal accion As Integer) As DataTable

        For Each dr As DataRow In dt.Rows
            If dr.Item("Codigo") = codigo Then

                If accion = 1 Then 'Marcar
                    If dr.Item("Check") = 0 Then
                        dr.Item("Check") = 1
                    End If
                Else 'Desmarcar
                    If dr.Item("Check") = 1 Then
                        dr.Item("Check") = 0
                    End If
                End If

            End If
        Next

        Return dt

    End Function

    ''' <summary>
    ''' Verifico si el checkbox fue "Seleccionado" en el DataTable enviado
    ''' </summary>
    ''' <param name="codigo">Codigo del checkbox que ha sido clickeado</param>
    ''' <param name="dtOriginal">DataTable donde realizare la verificacion</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarEstadoCheckDetalle(ByVal codigo As Integer, ByVal dtOriginal As DataTable) As Boolean
        For i As Integer = 0 To dtOriginal.Rows.Count - 1
            If dtOriginal.Rows(i).Item("Codigo") = codigo And dtOriginal.Rows(i).Item("Check") = 1 Then
                Return True
            End If
        Next
        Return False
    End Function


    ''' <summary>
    ''' Agrega el índice de páginas al combo de páginación. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CrearBotonesPager_ListaBloqueInformacion(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page)

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim ddlPageSelector As DropDownList = DirectCast(gvPagerRow.FindControl("ddlPageSelectorListaBloqueInformacion"), DropDownList)
        ddlPageSelector.Items.Clear()

        For i As Integer = 1 To gridView.PageCount
            ddlPageSelector.Items.Add(i.ToString())
        Next

        ddlPageSelector.SelectedIndex = pageIndex

    End Sub

    ''' <summary>
    ''' Muestra la numeración de registros por página y cantidad total de registros del listado actual. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function InformacionPager_ListaBloqueInformacion(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page) As String

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim pageSize As Integer = gridView.PageSize
        Dim rowCount As Integer = gridView.Rows.Count

        Dim currentPageFirstRow As Integer = ((pageIndex * pageSize) + 1)
        Dim currentPageLastRow As Integer = 0
        Dim lastPageRemainder As Integer = pageCount Mod pageSize

        currentPageLastRow = currentPageFirstRow + rowCount - 1

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegsListaBloqueInformacion.Value)

    End Function

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Property GridViewSortDirection_ListaBloqueInformacion() As SortDirection

        Get
            If ViewState("sortDirection_ListaBloqueInformacion") Is Nothing Then
                ViewState("sortDirection_ListaBloqueInformacion") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("sortDirection_ListaBloqueInformacion"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection_ListaBloqueInformacion") = value
        End Set

    End Property

    ''' <summary>
    ''' Lista los datos de procedimientos realizados ordenados por Descripción.
    ''' </summary>
    ''' <param name="sortExpression">Bloque por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView_ListaBloqueInformacion(ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda_ListaBloquesInformacion(2)

        hfTotalRegsListaBloqueInformacion.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        GVListaBloqueInformacion.DataSource = dv
        GVListaBloqueInformacion.DataBind()

    End Sub

    ''' <summary>
    ''' Cambia la imagen dependiendo el Bloque y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting_ListaBloqueInformacion()

        Dim _btnSorting As ImageButton = CType(GVListaBloqueInformacion.HeaderRow.FindControl("btnSorting"), ImageButton)

        If ViewState("Direccion_ListaBloqueInformacion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion_ListaBloqueInformacion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
            _btnSorting.ToolTip = "Ascendente"
        End If

    End Sub

#End Region

#End Region


#Region "Mantenimiento de detalle de Acciones"

#Region "Eventos"

    Protected Sub btnModalAceptarAcciones_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            int_CodigoAccion = 7
            agregarAcciones()
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarAcciones_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        pnModalAcciones.Hide()
    End Sub


#End Region

#Region "Metodos"

    'Private Sub MostrarPanelAcciones(ByVal int_CodigoBloqueInformacion As Integer)
    Private Sub MostrarPanelAcciones(ByVal int_CodigoRelacion As Integer)

        If ViewState("ListaAcciones") Is Nothing Then
            cargarViewStateAcciones()
        End If

        pnModalAcciones.Show()
        'hiddenCodigoBloqueInformacion.Value = int_CodigoBloqueInformacion
        hiddenCodigoBloqueInformacion.Value = int_CodigoRelacion 'Guarda el codigo de la asignacion del bloque de informacion (CodigoRelacion)

        Dim dt As DataTable

        If ViewState("DetalleAcciones") Is Nothing Then
            dt = New DataTable("DetalleAcciones")
            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoBloqueInformacion", "Integer") 'CodigoBloqueInformacion
            dt = Datos.agregarColumna(dt, "CodigoAccion", "Integer")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
            dt = Datos.agregarColumna(dt, "TipoDato", "String")
        Else
            dt = ViewState("DetalleAcciones")
        End If

        ViewState("DetalleAcciones") = dt

        GVListaAcciones.DataSource = ViewState("ListaAcciones")
        GVListaAcciones.DataBind()

        marcarHeaderCheckGVListaAcciones()

        upBloqueInformacion.Update()

    End Sub

    ''' <summary>
    ''' Agrega 1 Accion al detalle 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarAcciones()

        Dim dt As DataTable

        If ViewState("DetalleAcciones") Is Nothing Then
            dt = New DataTable("DetalleAcciones")
            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoBloqueInformacion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoAccion", "Integer")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
            dt = Datos.agregarColumna(dt, "TipoDato", "String")
        Else
            dt = ViewState("DetalleAcciones")
        End If

        Dim dtA As DataTable
        dtA = ViewState("ListaAcciones")

        If dtA.Rows.Count > 0 Then

            Dim codB As Integer = hiddenCodigoBloqueInformacion.Value

            For Each gvr As GridViewRow In GVListaAcciones.Rows

                Dim codA As Integer = gvr.Cells(1).Text

                If CType(gvr.FindControl("chkSeleccionar"), CheckBox).Checked Then
                    Dim dr As DataRow
                    dr = dt.NewRow

                    'Dim autoCode As Integer = dt.Compute("Max(CodigoRelacion)", "")
                    Dim autoCode As Integer
                    If dt.Rows.Count = 0 Then
                        autoCode = 1
                    Else ' >0
                        If dt.Rows.Count = 1 Then
                            autoCode = IIf(dt.Rows(0).Item("CodigoRelacion") = -1, 1, dt.Compute("Max(CodigoRelacion)", "") + 1)
                        Else
                            autoCode = dt.Compute("Max(CodigoRelacion)", "") + 1
                        End If
                    End If

                    dr.Item("CodigoRelacion") = autoCode
                    dr.Item("CodigoBloqueInformacion") = codB
                    dr.Item("CodigoAccion") = codA
                    dr.Item("Descripcion") = CType(gvr.FindControl("Label1"), Label).Text
                    dr.Item("TipoDato") = "T"

                    If validarAgregarDetalleAcciones(codB, codA, dt) Then
                        dt.Rows.Add(dr)
                    End If
                Else 'Si no esta marcada verifico si esta en mi detalleAcciones y la elimino

                    For Each auxdr As DataRow In dt.Rows

                        If auxdr.Item("CodigoBloqueInformacion") = codB And auxdr.Item("CodigoAccion") = codA Then
                            auxdr.Delete()
                            Exit For
                        End If
                    Next
                    dt.AcceptChanges()

                    ViewState("DetalleAcciones") = dt
                    upBloqueInformacion.Update()

                End If

            Next

            'Actualizo el campo descripcion Acciones del gridview "Bloques Informacion"
            Dim str_DescAcciones As String = ""

            For Each auxdr As DataRow In dt.Rows
                If auxdr.Item("CodigoBloqueInformacion") = codB Then
                    str_DescAcciones += auxdr.Item("Descripcion") + ","
                End If
            Next

            If str_DescAcciones.Trim.Length > 0 Then
                str_DescAcciones = str_DescAcciones.Substring(0, str_DescAcciones.Trim.Length - 1)
            End If

            Dim dt_BInf As New DataTable
            dt_BInf = ViewState("DetalleBloqueInformacion")
            dt_BInf = actualizarDescDetalleAcciones(codB, str_DescAcciones, dt_BInf)
            ViewState("DetalleBloqueInformacion") = dt_BInf
            GVDetalleBloqueInformacion.DataSource = dt_BInf
            GVDetalleBloqueInformacion.DataBind()

        End If

        GVListaAcciones.DataBind()

        ViewState.Remove("DetalleAcciones")
        ViewState("DetalleAcciones") = dt

        pnModalAcciones.Hide()
        upBloqueInformacion.Update()

    End Sub

    Private Function validarAgregarDetalleAcciones(ByVal codigoBloque As Integer, ByVal codigoAccion As Integer, ByVal dtOriginal As DataTable) As Boolean

        For i As Integer = 0 To dtOriginal.Rows.Count - 1
            If dtOriginal.Rows(i).Item("CodigoBloqueInformacion") = codigoBloque.ToString And _
               dtOriginal.Rows(i).Item("CodigoAccion") = codigoAccion.ToString Then
                Return False
            End If
        Next
        Return True

    End Function

    Private Function validarCheckListaAcciones(ByVal codigoBloque As Integer, ByVal codigoAccion As Integer, ByVal dtOriginal As DataTable) As Boolean

        For i As Integer = 0 To dtOriginal.Rows.Count - 1
            If dtOriginal.Rows(i).Item("CodigoBloqueInformacion") = codigoBloque.ToString And _
               dtOriginal.Rows(i).Item("CodigoAccion") = codigoAccion.ToString Then
                Return True
            End If
        Next
        Return False

    End Function

    Private Function actualizarDescDetalleAcciones(ByVal codigoBloque As Integer, ByVal str_Descripcion As String, ByVal dtOriginal As DataTable) As DataTable

        For i As Integer = 0 To dtOriginal.Rows.Count - 1
            'If dtOriginal.Rows(i).Item("CodigoBloqueInformacion") = codigoBloque Then
            If dtOriginal.Rows(i).Item("CodigoRelacion") = codigoBloque Then
                dtOriginal.Rows(i).Item("DescripcionAcciones") = str_Descripcion
            End If
        Next
        Return dtOriginal

    End Function

    ''' <summary>
    ''' Carga el combo con la lista de Acciones disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     01/04/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarViewStateAcciones()

        If ViewState("ListaAcciones") Is Nothing Then
            Dim ds_Lista As DataSet = ObtenerResultadoBusqueda_ListaAcciones(1)
            hfTotalRegsListaAcciones.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)
            ViewState("ListaAcciones") = ds_Lista.Tables(0)
        End If

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     01/04/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda_ListaAcciones(ByVal int_Modo As Integer) As DataSet

        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_NombresAcciones As New bl_NombresAcciones
            ds_Lista = obj_BL_NombresAcciones.FUN_LIS_NombresAcciones("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            ds_Lista.Tables(0).TableName = "ListaAcciones"
            Dim dt As New DataTable()
            dt = ds_Lista.Tables(0).Copy

            Dim column As New DataColumn("Check")
            With column
                .DataType = System.Type.GetType("System.Int32")
                .DefaultValue = 0
            End With
            dt.Columns.Add(column)

            ds_Lista.Tables.Remove("ListaAcciones")
            ds_Lista.Tables.Add(dt)

            ViewState("ListaAcciones") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("ListaAcciones") Is Nothing Then

                Dim obj_BL_NombresAcciones As New bl_NombresAcciones
                ds_Lista = obj_BL_NombresAcciones.FUN_LIS_NombresAcciones("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                ds_Lista.Tables(0).TableName = "ListaAcciones"
                Dim dt As New DataTable()
                dt = ds_Lista.Tables(0).Copy

                Dim column As New DataColumn("Check")
                With column
                    .DataType = System.Type.GetType("System.Int32")
                    .DefaultValue = 0
                End With
                dt.Columns.Add(column)

                ds_Lista.Tables.Remove("ListaAcciones")
                ds_Lista.Tables.Add(dt)

                ViewState("ListaAcciones") = ds_Lista
            Else
                ds_Lista = ViewState("ListaAcciones")
            End If
        End If

        Return ds_Lista
    End Function

#End Region

#Region "Gridview"

    Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim gv As GridView = CType(chk.Parent.Parent.Parent.Parent, GridView)

        If chk.Checked Then
            For Each gvr As GridViewRow In gv.Rows
                Dim chkS As CheckBox = CType(gvr.FindControl("chkSeleccionar"), CheckBox)
                chkS.Checked = True
            Next
        Else
            For Each gvr As GridViewRow In gv.Rows
                Dim chkS As CheckBox = CType(gvr.FindControl("chkSeleccionar"), CheckBox)
                chkS.Checked = False
            Next
        End If
        pnModalAcciones.Show()

    End Sub

    Protected Sub marcarHeaderCheckGVListaAcciones()

        Dim bool_Check As Boolean = True
        For Each gvr As GridViewRow In GVListaAcciones.Rows
            If CType(gvr.FindControl("chkSeleccionar"), CheckBox).Checked = False Then
                bool_Check = False
                Exit For
            End If
        Next

        If bool_Check Then
            CType(Me.GVListaAcciones.HeaderRow.FindControl("chkAll"), CheckBox).Checked = True
        Else
            CType(Me.GVListaAcciones.HeaderRow.FindControl("chkAll"), CheckBox).Checked = False
        End If

    End Sub

    Protected Sub GVListaAcciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim dt As DataTable
        Dim dtChk As DataTable
        Dim ds_Lista As DataSet

        If e.Row.RowType = DataControlRowType.DataRow Then
            If ViewState("DetalleAcciones") IsNot Nothing Then
                dt = ViewState("DetalleAcciones")

                Dim codB As Integer = hiddenCodigoBloqueInformacion.Value
                Dim codA As Integer = e.Row.Cells(1).Text
                Dim chk As CheckBox = CType(e.Row.FindControl("chkSeleccionar"), CheckBox)

                If validarCheckListaAcciones(codB, codA, dt) Then
                    Dim BGColor As String = "#dcff7d"
                    e.Row.Style.Add("background", BGColor)
                    chk.Checked = True
                End If

            End If
            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If

    End Sub

    Protected Sub chkSeleccionarListaAcciones_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'cambiarEstadoCheckBox_ListaAcciones(sender, e)
            pnModalAcciones.Show()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#End Region


#Region "Metodos del Gridview"

    ''' <summary>
    ''' Crea los botones de siguientex, atraz, etc de la lista de información.
    ''' </summary>
    ''' <param name="gridView">Control de tipo griedview</param>
    ''' <param name="gvPagerRow">Fila de la grilla</param>
    ''' <param name="page">Página</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CrearBotonesPager(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page)

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim ddlPageSelector As DropDownList = DirectCast(gvPagerRow.FindControl("ddlPageSelector"), DropDownList)
        ddlPageSelector.Items.Clear()

        For i As Integer = 1 To gridView.PageCount
            ddlPageSelector.Items.Add(i.ToString())
        Next

        ddlPageSelector.SelectedIndex = pageIndex

    End Sub

    ''' <summary>
    ''' Información referencial del listado de información de la grilla.
    ''' </summary>
    ''' <param name="gridView">control tipo grilla</param>
    ''' <param name="gvPagerRow">fila de la grilla</param>
    ''' <param name="page">página</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function InformacionPager(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page) As String

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim pageSize As Integer = gridView.PageSize
        Dim rowCount As Integer = gridView.Rows.Count

        Dim currentPageFirstRow As Integer = ((pageIndex * pageSize) + 1)
        Dim currentPageLastRow As Integer = 0
        Dim lastPageRemainder As Integer = pageCount Mod pageSize

        currentPageLastRow = currentPageFirstRow + rowCount - 1

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegs.Value)

    End Function

    ''' <summary>
    ''' Setea los indicadores (ViewState) de la dirección del ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Property GridViewSortDirection() As SortDirection

        Get
            If ViewState("sortDirection") Is Nothing Then
                ViewState("sortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("sortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection") = value
        End Set

    End Property

    ''' <summary>
    ''' Ordena el listado de información de la grilla según la columa y dirección seleccionada.
    ''' </summary>
    ''' <param name="sortExpression">Columna por la cual se ordenará el listado</param>
    ''' <param name="direction">Dirección ASC o DESC por el cual se ordenará el listado</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)
        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(2)
        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        GridView1.DataSource = dv
        GridView1.DataBind()

    End Sub

    ''' <summary>
    ''' Dirección del grafico indicador del ordenamiento basado en columnas.
    ''' </summary>
    ''' <param name="nombreBoton">Descripción del nombre del grafico</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_BloqueMenu"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_TipoSubBloque"), ImageButton)
        Dim _btnSorting_d3 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_EstadoProceso"), ImageButton)
        Dim _btnSorting_d4 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Descripcion"), ImageButton)


        If _btnSorting.ID = _btnSorting_d1.ID Then
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"

            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"

            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
        ElseIf _btnSorting.ID = _btnSorting_d2.ID Then
            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"

            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"

            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
        ElseIf _btnSorting.ID = _btnSorting_d3.ID Then
            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"

            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"

            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
        Else
            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"

            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"

            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
        End If


        If ViewState("Direccion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
            _btnSorting.ToolTip = "Ascendente"
        End If

    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub ddlPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GridView1.PageCount Then
                Me.GridView1.PageIndex = _NumPag - 1
            Else
                Me.GridView1.PageIndex = 0
            End If

            Me.GridView1.SelectedIndex = -1

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Or e.CommandName = "Activar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    obtener(codigo)
                ElseIf e.CommandName = "Eliminar" And row.Cells(4).Text <> "Inactivo" Then
                    int_CodigoAccion = 3
                    cambiarEstado(codigo, "Eliminar")
                ElseIf e.CommandName = "Activar" And row.Cells(4).Text <> "Activo" Then
                    int_CodigoAccion = 2
                    cambiarEstado(codigo, "Activar")
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

        Try
            If e.CommandName = "ExportarDocu" Then
                Dim NombreDoc As String = e.CommandArgument.ToString
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                ExportarDocumento(NombreDoc)

            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", "alert('" & ex.Message & "')", True)
        End Try

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
        Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
        Dim btnActivar As ImageButton = e.Row.FindControl("btnActivar")
        Dim btnDocumentacion As Image = e.Row.FindControl("imglink")

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = GridView1.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.DataItem("Estado") = "Activo" Then
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                'btnActualizar.Visible = True
                'btnEliminar.Visible = True
                btnActivar.Visible = False
            Else
                btnActivar.Attributes.Add("OnClick", "return confirm_activar();")
                btnActualizar.Visible = False
                btnEliminar.Visible = False
                'btnActivar.Visible = True
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            If e.Row.DataItem("LinkDocumentacion").ToString.Length > 4 Then
                'btnDocumentacion.Attributes.Add("OnClick", "window.open('" + e.Row.DataItem("LinkDocumentacion") + "','','')")
                btnDocumentacion.Style.Value = "Cursor:pointer"
                btnDocumentacion.Visible = True

            Else
                btnDocumentacion.Visible = False
            End If

        End If

    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GridView1.PageIndex = e.NewPageIndex
            End If

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try

    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression

            ViewState("SortExpression") = sortExpression

            If GridViewSortDirection = SortDirection.Ascending Then
                GridViewSortDirection = SortDirection.Descending
                SortGridView(sortExpression, "DESC")
                ViewState("Direccion") = "DESC"
            Else
                GridViewSortDirection = SortDirection.Ascending
                SortGridView(sortExpression, "ASC")
                ViewState("Direccion") = "ASC"
            End If

            ImagenSorting(e.SortExpression)
        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try

    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager(GridView1, e.Row, Me)
        End If

    End Sub

#End Region

    
End Class
