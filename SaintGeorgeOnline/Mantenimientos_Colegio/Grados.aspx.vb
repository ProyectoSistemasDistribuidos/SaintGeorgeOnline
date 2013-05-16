Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

''' <summary>
''' Modulo de Mantenimiento de Grados
''' </summary>
''' <remarks>
''' Código del Modulo:    
''' Código de la Opción:  
''' </remarks>

Partial Class Mantenimientos_Colegio_Grados
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Grados")

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                ViewState("SortExpression") = "Codigo"
                ViewState("Direccion") = "ASC"
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")

                tbDescripcion.Attributes.Add("onkeypress", " ValidarLength(this, 100);")
                tbDescripcion.Attributes.Add("onkeyup", " ValidarLength(this, 100);")
                tbDescripcionEspaniol.Attributes.Add("onkeypress", " ValidarLength(this, 100);")
                tbDescripcionEspaniol.Attributes.Add("onkeyup", " ValidarLength(this, 100);")

                cargarComboSubnivel()
                cargarComboGradoMinisterio()
                listar()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
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
        ddlSubnivel.SelectedValue = 0
        ddlGradoMinisterio.SelectedValue = 0
        tbDescripcion.Text = ""
        tbDescripcionEspaniol.Text = ""
        hd_Codigo.Value = 0
        tbBuscarDescripcion.Focus()

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

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        ModalPopupExtender1.Dispose()

    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        limpiarFiltros()

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Setea las acciones de acceso del usuario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(cod_Modulo, cod_Opcion)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
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

    ''' <summary>
    ''' Limpia los filtros de busqueda.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        tbBuscarDescripcion.Text = ""
        'rbEstados.SelectedValue = 1
        tbBuscarDescripcion.Focus()

    End Sub

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

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        Dim dt As DataTable = New DataTable("ListaExportar")

        dt = Datos.agregarColumna(dt, "N°", "String")
        dt = Datos.agregarColumna(dt, "Descripcion en Ingles", "String")
        dt = Datos.agregarColumna(dt, "Descripcion en Español", "String")
        dt = Datos.agregarColumna(dt, "Nivel", "String")
        dt = Datos.agregarColumna(dt, "Subnivel", "String")
        dt = Datos.agregarColumna(dt, "GradoMinisterio", "String")
        dt = Datos.agregarColumna(dt, "Estado", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("Descripcion en Ingles") = dr.Item("Descripcion").ToString
            auxDR.Item("Descripcion en Español") = dr.Item("DescripcionEspaniol").ToString
            auxDR.Item("Nivel") = dr.Item("DescNivel").ToString
            auxDR.Item("Subnivel") = dr.Item("DescSubnivel").ToString
            auxDR.Item("GradoMinisterio") = dr.Item("DescGradoMinisterio").ToString
            auxDR.Item("Estado") = dr.Item("Estado").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next

        If rbExportar.SelectedValue = 0 Then 'WORD
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Grados")
            reporte_html = Arreglo_Datos(0)
            NombreArchivo = Arreglo_Datos(1)
            NombreArchivo = NombreArchivo & ".doc"

            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Mantenimientos_Colegio", "\Reportes\")


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

            NombreArchivo = Exportacion.ExportarReporte(dt, "Grados")
            NombreArchivo = NombreArchivo & ".xls"
            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Mantenimientos_Colegio", "\Reportes\")

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

            m = Exportacion.ExportarReporte_Pdf(dt, "Grados")

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

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Grados")
            reporte_html = Arreglo_Datos(0)
            Session("Exportaciones_RepHtml") = reporte_html
            ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresion_html();</script>", False)
        End If

    End Sub

    ''' <summary>
    ''' Habilita el TabPanel del formulario
    ''' </summary>
    ''' <param name="str_Modo">Nombre del label del tabPanel</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String)

        miTab1.Enabled = False
        miTab2.Enabled = True
        lbTab2.Text = str_Modo
        TabContainer1.ActiveTabIndex = 1
        ddlSubnivel.Focus()

    End Sub

    ''' <summary>
    ''' Valida el campo de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlSubnivel.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Subnivel")
            result = False
        End If

        If ddlGradoMinisterio.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Grado Ministerio")
            result = False
        End If

        If tbDescripcion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Descripción en Ingles")
            result = False
        End If
        If Validacion.ValidarCamposIngreso(tbDescripcion) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Descripción en Ingles")
            result = False
        End If

        If tbDescripcionEspaniol.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Descripción en Español")
            result = False
        End If
        If Validacion.ValidarCamposIngreso(tbDescripcionEspaniol) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Descripción en Español")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Limpia los campos de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()

        hd_Codigo.Value = 0
        ddlSubnivel.SelectedValue = 0
        ddlGradoMinisterio.SelectedValue = 0
        tbDescripcion.Text = ""
        tbDescripcionEspaniol.Text = ""

    End Sub

    ''' <summary>
    ''' Carga el combo SubNivel
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSubnivel()

        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlSubnivel, ds_Lista, "Codigo", "DescSubnivel", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Grado Ministerio
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboGradoMinisterio()

        Dim obj_bl_GradosMinisterio As New bl_GradosMinisterio
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_bl_GradosMinisterio.FUN_LIS_GradosMinisterio(0, 0, -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlGradoMinisterio, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Lista los datos      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
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
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet
        Dim str_Descripcion As String = tbBuscarDescripcion.Text.Trim()
        Dim int_Estado As Integer = 1 'CInt(rbEstados.SelectedValue)
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Grados As New bl_Grados
            ds_Lista = obj_BL_Grados.FUN_LIS_Grados(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_Grados As New bl_Grados
                ds_Lista = obj_BL_Grados.FUN_LIS_Grados(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Obtiene y setea los datos en el Formulario.     
    ''' </summary>
    ''' <param name="int_Codigo">Código de grado ministerio </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As Integer)

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_GET_Grados(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("Codigo").ToString)
        ddlSubnivel.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoSubnivel").ToString
        ddlGradoMinisterio.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoGradoMinisterio").ToString
        tbDescripcion.Text = ds_Lista.Tables(0).Rows(0).Item("Descripcion").ToString
        tbDescripcionEspaniol.Text = ds_Lista.Tables(0).Rows(0).Item("DescripcionEspaniol").ToString
        tbAbreviatura.Text = ds_Lista.Tables(0).Rows(0).Item("Abrev").ToString

        VerRegistro("Actualización")

    End Sub

    ''' <summary>
    ''' Cambia el estado de la información.     
    ''' </summary>
    ''' <param name="int_Codigo">Código de Grado</param>
    '''  <param name="str_accion">nombre de la acción</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_bl_Grados As New bl_Grados
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado


        If str_accion = "Eliminar" Then
            usp_valor = obj_bl_Grados.FUN_DEL_Grados(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        listar()

    End Sub

    ''' <summary>
    ''' Graba los datos del formulario 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_Grados As New be_Grados
        Dim obj_BL_Grados As New bl_Grados
        Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        obj_BE_Grados.CodigoSubNivel = ddlSubnivel.SelectedValue
        obj_BE_Grados.CodigoGradoMinisterio = ddlGradoMinisterio.SelectedValue
        obj_BE_Grados.Descripcion = tbDescripcion.Text.Trim
        obj_BE_Grados.DescripcionEspaniol = tbDescripcionEspaniol.Text.Trim
        obj_BE_Grados.Abrev = tbAbreviatura.Text.Trim

        If BoolGrabar = 0 Then
            usp_valor = obj_BL_Grados.FUN_INS_Grados(obj_BE_Grados, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Else
            obj_BE_Grados.CodigoGrado = CInt(BoolGrabar)
            usp_valor = obj_BL_Grados.FUN_UPD_Grados(obj_BE_Grados, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnCancelar_Click()
            limpiarCampos()
            listar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

#End Region

#Region "Metodos del Gridview"

    ''' <summary>
    ''' Agrega el índice de páginas al combo de páginación. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
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
    ''' Muestra la numeración de registros por página y cantidad total de registros del listado actual. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
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
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
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
    ''' Lista los datos de procedimientos realizados ordenados por Descripción.
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
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
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Codigo"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_DescNivel"), ImageButton)
        Dim _btnSorting_d3 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_DescSubnivel"), ImageButton)
        Dim _btnSorting_d4 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_DescGradoMinisterio"), ImageButton)
        Dim _btnSorting_d5 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_DescripcionEspaniol"), ImageButton)
        Dim _btnSorting_d6 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Abreviatura"), ImageButton)

        If _btnSorting.ID = _btnSorting_d1.ID Then
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d2.ID Then
            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"
        ElseIf _btnSorting.ID = _btnSorting_d3.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d4.ID Then
            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d5.ID Then
            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"

        Else
            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
          
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
                ElseIf e.CommandName = "Eliminar" And row.Cells(5).Text <> "Inactivo" Then
                    int_CodigoAccion = 3
                    cambiarEstado(codigo, "Eliminar")
                ElseIf e.CommandName = "Activar" And row.Cells(5).Text <> "Activo" Then
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

            If e.Row.DataItem("Estado") = "Activo" Then
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                btnActivar.Visible = False
                btnEliminar.Visible = True
            Else
                btnActivar.Attributes.Add("OnClick", "return confirm_activar();")
                btnActualizar.Visible = False
                btnEliminar.Visible = False
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


