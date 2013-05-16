Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos
Imports SaintGeorgeOnline_BusinessLogic.ModuloPermisos
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing

''' <summary>
''' Modulo de Mantenimiento de Bloques de Menus
''' </summary>
''' <remarks>
''' Código del Modulo:    3
''' Código de la Opción:  39
''' </remarks>
Partial Class Modulo_Permisos_BloquesMenus
    Inherits System.Web.UI.Page


#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Me.Master.MostrarTitulo("Bloques de Menus")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                ViewState("SortExpression") = "Descripcion"
                ViewState("Direccion") = "ASC"
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                listar()

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
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
            If validar() Then
                Grabar()
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender1.Dispose()
    End Sub

    Protected Sub btn_SubirImagen_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        VerificarImagen()
    End Sub

    Protected Sub rbl_TipoBloque_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If rbl_TipoBloque.SelectedValue = 0 Then
            pnl_Link.Visible = True
        Else
            pnl_Link.Visible = False
        End If
    End Sub

#End Region

#Region "Metodos"

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

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(3, 39, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

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
        Me.Master.RegistrarAccesoPagina(3, 39)

        'CONTROLES DEL FORMULARIO
        Master.BloqueoControles(btnBuscar, 1)
        Master.BloqueoControles(btnExportar, 1)
        Master.BloqueoControles(btnGrabar, 1)
        Master.BloqueoControles(btnNuevo, 1)

        Master.SeteoPermisosAcciones(btnBuscar, 39)
        Master.SeteoPermisosAcciones(btnExportar, 39)
        Master.SeteoPermisosAcciones(btnGrabar, 39)
        Master.SeteoPermisosAcciones(btnNuevo, 39)

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
        dt = Datos.agregarColumna(dt, "Menú", "String")
        dt = Datos.agregarColumna(dt, "Tipo de Menú", "String")
        dt = Datos.agregarColumna(dt, "Estado", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("Menú") = dr.Item("Descripcion").ToString
            auxDR.Item("Tipo de Menú") = dr.Item("TipoBloque").ToString
            auxDR.Item("Estado") = dr.Item("Estado").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next

        If rbExportar.SelectedValue = 0 Then 'WORD
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Bloque Menú")
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
            NombreArchivo = Exportacion.ExportarReporte(dt, "Bloque Menú")
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

            m = Exportacion.ExportarReporte_Pdf(dt, "Bloque Menú")

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

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Bloque Menú")
            reporte_html = Arreglo_Datos(0)
            Session("Exportaciones_RepHtml") = reporte_html
            ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresion_html();</script>", False)
        End If

    End Sub

    ''' <summary>
    ''' Bloquea el formulario de busqueda cuando se selecciona la opción de Nuevo y Modificación de Registro.
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
    ''' Valida el nombre del bloque de menu.
    ''' </summary>
    ''' <returns>indicador sobre la validación del nombre del bloque de menu</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar() As Boolean
        If tbDescripcion.Text.Trim.Length <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function

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
        tbDescripcion.Text = ""
        TbRutaIcono.Text = ""
    End Sub

    ''' <summary>
    ''' Lista la relación de bloques de menus.
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
        Dim str_Descripcion As String = tbBuscarDescripcion.Text.Trim()
        Dim int_Estado As Integer = CInt(rbEstados.SelectedValue)
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_BloqueMenu As New bl_BloquesMenus
            ds_Lista = obj_BL_BloqueMenu.FUN_LIS_BloqueMenu(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 39)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_BloqueMenu As New bl_BloquesMenus
                ds_Lista = obj_BL_BloqueMenu.FUN_LIS_BloqueMenu(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 39)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Obtiene la información sobre un registro y lo muestra en el formulario.
    ''' </summary>
    ''' <param name="int_Codigo">Código de bloque de menu</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As Integer)
        Dim obj_BL_BloqueMenu As New bl_BloquesMenus
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_BloqueMenu.FUN_GET_BloqueMenu(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 39)

        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("Codigo").ToString)
        tbDescripcion.Text = ds_Lista.Tables(0).Rows(0).Item("Descripcion").ToString
        TbRutaIcono.Text = ds_Lista.Tables(0).Rows(0).Item("RutaIcono").ToString

        rbl_TipoBloque.SelectedValue = IIf(ds_Lista.Tables(0).Rows(0).Item("TipoBloque").ToString = True, 1, 0)

        If rbl_TipoBloque.SelectedValue = 0 Then
            pnl_Link.Visible = True
        Else
            pnl_Link.Visible = False
        End If

        TbRutaLink.Text = ds_Lista.Tables(0).Rows(0).Item("Link").ToString

        VerRegistro("Actualización")
    End Sub

    ''' <summary>
    ''' Llama al metodo de Eliminar o Activar según la acción seleccionada.
    ''' </summary>
    ''' <param name="int_Codigo">codigo de bloque de menu</param>
    ''' <param name="str_accion">tipo de acción a realizar (Activar o Eliminar)</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)
        Dim obj_BL_BloqueMenu As New bl_BloquesMenus
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_BloqueMenu.FUN_DEL_BloqueMenu(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 39)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        listar()
    End Sub

    ''' <summary>
    ''' Graba o Actualiza el registro del bloque de menu
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()
        Dim obj_BE_BloqueMenu As New be_BloquesMenus
        Dim obj_BL_BloqueMenu As New bl_BloquesMenus
        Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        obj_BE_BloqueMenu.Descripcion = tbDescripcion.Text.Trim
        obj_BE_BloqueMenu.RutaIcono = TbRutaIcono.Text.Trim
        obj_BE_BloqueMenu.TipoBloque = rbl_TipoBloque.SelectedValue
        obj_BE_BloqueMenu.RutaLink = TbRutaLink.Text.Trim

        If BoolGrabar = 0 Then
            usp_valor = obj_BL_BloqueMenu.FUN_INS_BloqueMenu(obj_BE_BloqueMenu, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 39)
            GuardarImagen()
        Else
            obj_BE_BloqueMenu.CodigoBloque = CInt(BoolGrabar)
            usp_valor = obj_BL_BloqueMenu.FUN_UPD_BloqueMenu(obj_BE_BloqueMenu, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 39)
            GuardarImagen()
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        btnCancelar_Click()
        limpiarCampos()
        listar()
    End Sub

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
    ''' Sube en memoria la imagen a adjuntar
    ''' </summary>
    ''' <returns>Indicador sobre el exito de la operación</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function VerificarImagen() As Boolean
        Dim sFileName As String = ""
        Dim FullFileName As String = ""
        Dim ruta As String = ConfigurationManager.AppSettings.Item("RutaImagenMenu_Local").ToString()
        Dim valida As Boolean = True

        Dim file As HttpPostedFile = FileUpload1.PostedFile

        If FileUpload1.PostedFile.ContentLength > 0 Then
            If (FileUpload1.PostedFile.ContentLength < 204800) Then
                sFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
                TbRutaIcono.Text = sFileName
                Dim bm As New Bitmap(FileUpload1.PostedFile.InputStream)
                ViewState("ImagenMenu") = bm
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
    ''' Guarda la imagen adjuntada por el usuario para el registro indicado.
    ''' </summary>
    ''' <returns>Indicador de exito de la operación</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function GuardarImagen() As Boolean
        Dim sFileName As String = ""
        Dim FullFileName As String = ""
        Dim ruta As String = ConfigurationManager.AppSettings.Item("RutaImagenMenu_Local").ToString()
        Dim valida As Boolean = True

        If Not ViewState("ImagenMenu") Is Nothing Then
            Dim bm As Bitmap
            bm = ViewState("ImagenMenu")
            bm.Save(ruta + TbRutaIcono.Text)

        End If

        Return valida
    End Function

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
        Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Descripcion"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_TipoBloque"), ImageButton)

        If _btnSorting.ID = _btnSorting_d1.ID Then
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
        Else
            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
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

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
        Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
        Dim btnActivar As ImageButton = e.Row.FindControl("btnActivar")

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = GridView1.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            'SETEO DE PERMISOS DE ACCIONES---------------
            Master.BloqueoControles(btnEliminar, 1)
            Master.BloqueoControles(btnActualizar, 1)
            Master.BloqueoControles(btnActivar, 1)
            '---------------------------------------------

            If e.Row.DataItem("Estado") = "Activo" Then
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                'btnActualizar.Visible = True
                'btnEliminar.Visible = True
                btnActivar.Visible = False

                Master.SeteoPermisosAcciones(btnEliminar, 39)
                Master.SeteoPermisosAcciones(btnActualizar, 39)
            Else
                btnActualizar.Visible = False
                btnEliminar.Visible = False
                'btnActivar.Visible = True

                Master.SeteoPermisosAcciones(btnActivar, 39)

                btnActivar.Attributes.Add("OnClick", "return confirm_activar();")
                e.Row.ForeColor = Drawing.Color.DarkRed
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

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
