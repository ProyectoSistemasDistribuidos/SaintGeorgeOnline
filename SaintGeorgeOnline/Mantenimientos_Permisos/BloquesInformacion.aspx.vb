Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos
Imports SaintGeorgeOnline_BusinessLogic.ModuloPermisos
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

''' <summary>
''' Modulo de Mantenimiento de Colegios
''' </summary>
''' <remarks>
''' Código del Modulo:    
''' Código de la Opción:  
''' </remarks>
''' 
Partial Class Mantenimientos_Permisos_BloquesInformacion
    Inherits System.Web.UI.Page


    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Bloques de Información")

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                ViewState("SortExpression") = "Descripcion"
                ViewState("Direccion") = "ASC"

                ViewState("SortExpression_ListaCampoInformacion") = "Descripcion"
                ViewState("Direccion_ListaCampoInformacion") = "ASC"

                ViewState("SortExpression_DetalleCampoInformacion") = "Descripcion"
                ViewState("Direccion_DetalleCampoInformacion") = "ASC"

                ViewState("SortExpression_ListaAcciones") = "Descripcion"
                ViewState("Direccion_ListaAcciones") = "ASC"

                ViewState("SortExpression_DetalleAcciones") = "Descripcion"
                ViewState("Direccion_DetalleAcciones") = "ASC"

                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                tbDescripcion.Attributes.Add("onkeypress", " ValidarLength(this, 100);")
                tbDescripcion.Attributes.Add("onkeyup", " ValidarLength(this, 100);")
                tbEntidad.Attributes.Add("onkeypress", " ValidarLength(this, 100);")
                tbEntidad.Attributes.Add("onkeyup", " ValidarLength(this, 100);")
                tbCodigoProgramacion.Attributes.Add("onkeypress", " ValidarLength(this, 100);")
                tbCodigoProgramacion.Attributes.Add("onkeyup", " ValidarLength(this, 100);")
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
        tbBuscarDescripcion.Focus()
        hd_Codigo.Value = 0

        ViewState("DetalleCampoInformacion") = Nothing
        ViewState.Remove("DetalleCampoInformacion")

        ViewState("DetalleAcciones") = Nothing
        ViewState.Remove("DetalleAcciones")
        limpiarCampos()

        'ViewState("ListaCampoInformacion") = Nothing
        'ViewState.Remove("ListaCampoInformacion")

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
    ''' Fecha de Creación:     21/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(cod_Modulo, cod_Opcion)

        'CONTROLES DEL FORMULARIO
        Master.BloqueoControles(btnBuscar, 1)
        Master.BloqueoControles(btnExportar, 1)
        Master.BloqueoControles(btnGrabar, 1)
        Master.BloqueoControles(btnNuevo, 1)

        Master.SeteoPermisosAcciones(btnBuscar, 7)
        Master.SeteoPermisosAcciones(btnExportar, 7)
        Master.SeteoPermisosAcciones(btnGrabar, 7)
        Master.SeteoPermisosAcciones(btnNuevo, 7)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     21/02/2011
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
    ''' Fecha de Creación:     21/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        tbBuscarDescripcion.Text = ""
        rbEstados.SelectedValue = 1
        tbBuscarDescripcion.Focus()

    End Sub

    ''' <summary>
    ''' Exporta los datos del gridView en formato WORD,EXCEL,HTML,PDF,HTML.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     21/02/2011
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
        dt = Datos.agregarColumna(dt, "Descripcion", "String")
        dt = Datos.agregarColumna(dt, "Tipo", "String")
        dt = Datos.agregarColumna(dt, "Codigo de Programacion", "String")
        dt = Datos.agregarColumna(dt, "Entidad", "String")
        dt = Datos.agregarColumna(dt, "Estado", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("Descripcion") = dr.Item("Descripcion").ToString
            auxDR.Item("Tipo") = dr.Item("Tipo").ToString
            auxDR.Item("Codigo de Programacion") = dr.Item("CodigoProgramacion").ToString
            auxDR.Item("Entidad") = dr.Item("Entidad").ToString
            auxDR.Item("Estado") = dr.Item("Estado").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next

        If rbExportar.SelectedValue = 0 Then 'WORD
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Bloques de Información")
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

            NombreArchivo = Exportacion.ExportarReporte(dt, "Bloques de Información")
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

            m = Exportacion.ExportarReporte_Pdf(dt, "Bloques de Información")

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

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Bloques de Información")
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
    ''' Fecha de Creación:     21/02/2011
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
    ''' Fecha de Creación:     21/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If rbTipo.SelectedValue = 1 Then 'modulo
            If tbDescripcion.Text.Trim.Length = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Descripcion")
                result = False
            End If
            If Validacion.ValidarCamposIngreso(tbDescripcion) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Descripcion")
                result = False
            End If
        ElseIf rbTipo.SelectedValue = 2 Then 'Grupo
            If tbDescripcion.Text.Trim.Length = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Descripcion")
                result = False
            End If
            If Validacion.ValidarCamposIngreso(tbDescripcion) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Descripcion")
                result = False
            End If

            If tbCodigoProgramacion.Text.Trim.Length = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Código Programación")
                result = False
            End If
            If Validacion.ValidarCamposIngreso(tbCodigoProgramacion) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Código Programación")
                result = False
            End If
        End If

     

        'If tbEntidad.Text.Trim.Length = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Entidad")
        '    result = False
        'End If
        'If Validacion.ValidarCamposIngreso(tbEntidad) = False Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Entidad")
        '    result = False
        'End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Limpia los campos de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     21/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()

        hd_Codigo.Value = 0
        tbDescripcion.Text = ""
        tbCodigoProgramacion.Text = ""
        rbTipo.SelectedValue = 1
        tbEntidad.Text = ""
        GVDetalleCampoInformacion.DataBind()

    End Sub

    ''' <summary>
    ''' Lista los datos      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     21/02/2011
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
            ImagenSorting()
        End If

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet

        Dim str_Descripcion As String = tbBuscarDescripcion.Text.Trim()
        Dim int_Estado As Integer = CInt(rbEstados.SelectedValue)
        Dim int_Tipo As Integer = 0

        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_BloquesInformacion As New bl_BloquesInformacion
            ds_Lista = obj_BL_BloquesInformacion.FUN_LIS_BloquesInformacion(str_Descripcion, int_Estado, int_Tipo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_BloquesInformacion As New bl_BloquesInformacion
                ds_Lista = obj_BL_BloquesInformacion.FUN_LIS_BloquesInformacion(str_Descripcion, int_Estado, int_Tipo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
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
    ''' <param name="int_Codigo">Código de diágnostico</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     21/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As Integer)

        Dim obj_BL_BloquesInformacion As New bl_BloquesInformacion
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_BloquesInformacion.FUN_GET_BloquesInformacion(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        'Setea Cabecera
        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("Codigo").ToString)
        tbDescripcion.Text = ds_Lista.Tables(0).Rows(0).Item("Descripcion").ToString
        tbCodigoProgramacion.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoProgramacion").ToString
        rbTipo.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("Tipo")
        tbEntidad.Text = ds_Lista.Tables(0).Rows(0).Item("Entidad").ToString

        'Seteo Detalles
        'Detalle de Campos de Informacion
        Dim dt_CInf As DataTable
        dt_CInf = New DataTable("DetalleCampoInformacion")
        dt_CInf = Datos.agregarColumna(dt_CInf, "CodigoCampoInformacion", "String")
        dt_CInf = Datos.agregarColumna(dt_CInf, "Descripcion", "String")
        dt_CInf = Datos.agregarColumna(dt_CInf, "CampoBD", "String")

        If ds_Lista.Tables(1).Rows.Count > 0 Then
            If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
                Dim dr As DataRow
                For Each r As DataRow In ds_Lista.Tables(1).Rows
                    dr = dt_CInf.NewRow
                    dr.Item("CodigoCampoInformacion") = r.Item("CodigoCampoInformacion")
                    dr.Item("Descripcion") = r.Item("Descripcion")
                    dr.Item("CampoBD") = r.Item("CampoBD")
                    dt_CInf.Rows.Add(dr)
                Next
                ViewState("DetalleCampoInformacion") = dt_CInf

                hfTotalRegsDetalleCampoInformacion.Value = CInt(ds_Lista.Tables(1).Rows.Count.ToString)

                GVDetalleCampoInformacion.DataSource = dt_CInf
                GVDetalleCampoInformacion.DataBind()
            End If
        End If

        VerRegistro("Actualización")

    End Sub

    ''' <summary>
    ''' Cambia el estado de la información.     
    ''' </summary>
    ''' <param name="int_Codigo">Código de diágnostico</param>
    '''  <param name="str_accion">nombre de la acción</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     21/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_BL_BloquesInformacion As New bl_BloquesInformacion
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado

        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_BloquesInformacion.FUN_DEL_BloquesInformaciones(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
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
    ''' Fecha de Creación:     21/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_BloquesInformacion As New be_BloquesInformaciones
        Dim obj_BL_BloquesInformacion As New bl_BloquesInformacion
        Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        obj_BE_BloquesInformacion.Descripcion = tbDescripcion.Text.Trim
        obj_BE_BloquesInformacion.CodigoGrupoProgramacion = tbCodigoProgramacion.Text.Trim
        obj_BE_BloquesInformacion.Tipo = rbTipo.SelectedValue
        obj_BE_BloquesInformacion.Entidad = tbEntidad.Text.Trim

        'Detalle
        Dim objDS_Detalle As New DataSet
        'Detalle Campo Informacion
        Dim objDT_CamposInformacion As DataTable

        If ViewState("DetalleCampoInformacion") Is Nothing Then
            objDT_CamposInformacion = New DataTable("DetalleCampoInformacion")
            objDT_CamposInformacion = Datos.agregarColumna(objDT_CamposInformacion, "CodigoCampoInformacion", "String")
            objDT_CamposInformacion = Datos.agregarColumna(objDT_CamposInformacion, "Descripcion", "String")
            objDT_CamposInformacion = Datos.agregarColumna(objDT_CamposInformacion, "CampoBD", "String")
        Else
            objDT_CamposInformacion = ViewState("DetalleCampoInformacion")
        End If
        objDS_Detalle.Tables.Add(objDT_CamposInformacion)

        If BoolGrabar = 0 Then
            usp_valor = obj_BL_BloquesInformacion.FUN_INS_BloquesInformaciones(obj_BE_BloquesInformacion, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Else

            obj_BE_BloquesInformacion.CodigoBloqueInformacion = CInt(BoolGrabar)
            usp_valor = obj_BL_BloquesInformacion.FUN_UPD_BloquesInformaciones(obj_BE_BloquesInformacion, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
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

    Private Function ExisteEnDetalleAcciones(ByVal codigo As Integer, ByVal dt As DataTable) As Boolean

        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("CodigoAccion") = codigo Then
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
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     21/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub


    ''' <summary>
    ''' Carga el combo con la lista de Campos de información disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarGrillaCamposInformacion()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda_ListaCamposInformacion(1)

        hfTotalRegsListaCampoInformacion.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GVListaCampoInformacion.DataSource = ds_Lista.Tables(0)
        GVListaCampoInformacion.DataBind()

        If ds_Lista.Tables(0).Rows.Count > 0 Then
            SortGridView_ListaCampoInformacion(ViewState("SortExpression_ListaCampoInformacion"), ViewState("Direccion_ListaCampoInformacion"))
            ImagenSorting_ListaCampoInformacion()
        End If

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda_ListaCamposInformacion(ByVal int_Modo As Integer) As DataSet

        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_CamposInformacion As New bl_CamposInformacion
            ds_Lista = obj_BL_CamposInformacion.FUN_LIS_CamposInformacion("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            ds_Lista.Tables(0).TableName = "ListaCampoInformacion"
            Dim dt As New DataTable()
            dt = ds_Lista.Tables(0).Copy

            Dim column As New DataColumn("Check")
            With column
                .DataType = System.Type.GetType("System.Int32")
                .DefaultValue = 0
            End With
            dt.Columns.Add(column)

            ds_Lista.Tables.Remove("ListaCampoInformacion")
            ds_Lista.Tables.Add(dt)

            ViewState("ListaCampoInformacion") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("ListaCampoInformacion") Is Nothing Then

                Dim obj_BL_CamposInformacion As New bl_CamposInformacion
                ds_Lista = obj_BL_CamposInformacion.FUN_LIS_CamposInformacion("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                ds_Lista.Tables(0).TableName = "ListaCampoInformacion"
                Dim dt As New DataTable()
                dt = ds_Lista.Tables(0).Copy

                Dim column As New DataColumn("Check")
                With column
                    .DataType = System.Type.GetType("System.Int32")
                    .DefaultValue = 0
                End With
                dt.Columns.Add(column)

                ds_Lista.Tables.Remove("ListaCampoInformacion")
                ds_Lista.Tables.Add(dt)

                ViewState("ListaCampoInformacion") = ds_Lista
            Else
                ds_Lista = ViewState("ListaCampoInformacion")
            End If
        End If

        Return ds_Lista
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
    Private Function ObtenerResultadoBusqueda_DetalleCamposInformacion() As DataSet
        Dim ds_Lista As New DataSet
        Dim dt As New DataTable
        If ViewState("DetalleCampoInformacion") Is Nothing Then
            dt = New DataTable("DetalleCampoInformacion")
            dt = Datos.agregarColumna(dt, "CodigoCampoInformacion", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
            dt = Datos.agregarColumna(dt, "CampoBD", "String")
        Else
            dt = ViewState("DetalleCampoInformacion")
        End If

        ds_Lista.Tables.Add(dt)

        Return ds_Lista

    End Function

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     22/02/2011
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

    ''' <summary>
    ''' Retorna el DataSet con la lista del detalle de acciones cargados en el ViewState(memoria)
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     23/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda_DetalleAcciones() As DataSet
        Dim ds_Lista As New DataSet
        Dim dt As New DataTable
        If ViewState("DetalleAcciones") Is Nothing Then
            dt = New DataTable("DetalleAcciones")
            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoAccion", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
        Else
            dt = ViewState("DetalleAcciones")
        End If

        ds_Lista.Tables.Add(dt)

        Return ds_Lista

    End Function

#End Region


#Region "Mantenimiento de detalle de Campos de Informacion"

#Region "Eventos"
    Protected Sub btnAgregarCampoInformacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim dt As DataTable

        If ViewState("DetalleCampoInformacion") Is Nothing Then
            dt = New DataTable("DetalleCampoInformacion")
            dt = Datos.agregarColumna(dt, "CodigoCampoInformacion", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
            dt = Datos.agregarColumna(dt, "CampoBD", "String")
        Else
            dt = ViewState("DetalleCampoInformacion")
        End If

        ViewState("DetalleCampoInformacion") = dt

        cargarGrillaCamposInformacion()
        pnModalCampoInformacion.Show()
    End Sub

    Protected Sub btnModalAceptarCampoInformacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            int_CodigoAccion = 7
            agregarCampoInformacion()
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarCampoInformacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalCampoInformacion()
    End Sub
#End Region
#Region "Métodos"
    Private Function validarAgregarDetalleCampoInformacion(ByVal codigo As Integer, ByVal dtOriginal As DataTable) As Boolean

        For i As Integer = 0 To dtOriginal.Rows.Count - 1
            If dtOriginal.Rows(i).Item("CodigoCampoInformacion") = codigo.ToString Then
                Return False
            End If
        Next
        Return True

    End Function

    Private Function validarCheckDetalleCampoInformacion(ByVal codigo As Integer, ByVal dtOriginal As DataTable) As Boolean
        For i As Integer = 0 To dtOriginal.Rows.Count - 1
            If dtOriginal.Rows(i).Item("CodigoCampoInformacion") = codigo.ToString Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Agrega 1 Campo de informacion al detalle 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarCampoInformacion()

        Dim dt As DataTable

        If ViewState("DetalleCampoInformacion") Is Nothing Then
            dt = New DataTable("DetalleCampoInformacion")
            dt = Datos.agregarColumna(dt, "CodigoCampoInformacion", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
            dt = Datos.agregarColumna(dt, "CampoBD", "String")
        Else
            dt = ViewState("DetalleCampoInformacion")
        End If

        Dim ds_Lista As New DataSet
        ds_Lista = ViewState("ListaCampoInformacion")

        If ds_Lista.Tables(0).Rows.Count > 0 Then
            For Each VSdr As DataRow In ds_Lista.Tables(0).Rows
                If VSdr.Item("Check") = 1 Then
                    Dim dr As DataRow
                    dr = dt.NewRow

                    Dim cod As Integer = VSdr.Item("Codigo")
                    dr.Item("CodigoCampoInformacion") = cod
                    dr.Item("Descripcion") = VSdr.Item("Descripcion")
                    dr.Item("CampoBD") = VSdr.Item("CampoBD")

                    If validarAgregarDetalleCampoInformacion(cod, dt) Then
                        dt.Rows.Add(dr)
                    End If

                End If
            Next
        End If

        GVListaCampoInformacion.DataBind()

        ViewState("DetalleCampoInformacion") = dt
        GVDetalleCampoInformacion.DataSource = dt
        GVDetalleCampoInformacion.DataBind()

        SortGridView_DetalleCampoInformacion(ViewState("SortExpression_DetalleCampoInformacion"), ViewState("Direccion_DetalleCampoInformacion"))
        ImagenSorting_DetalleCampoInformacion()

        upCampoInformacion.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 registro de Detalle de informacion de el detalle 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarCampoInformacion(ByVal int_Codigo As Integer)
        Dim dt As DataTable
        dt = ViewState("DetalleCampoInformacion")
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoCampoInformacion") = int_Codigo Then
                auxdr.Delete()
                Exit For
            End If
        Next
        dt.AcceptChanges()
        ViewState("DetalleCampoInformacion") = dt
        GVDetalleCampoInformacion.DataSource = dt
        GVDetalleCampoInformacion.DataBind()

        SortGridView_DetalleCampoInformacion(ViewState("SortExpression_DetalleCampoInformacion"), ViewState("Direccion_DetalleCampoInformacion"))
        ImagenSorting_DetalleCampoInformacion()

        upCampoInformacion.Update()
    End Sub

    ''' <summary>
    ''' Cierra el modal Campo Informacion
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalCampoInformacion()
        GVListaCampoInformacion.DataBind()
        pnModalCampoInformacion.Hide()
    End Sub
#End Region

#Region "Eventos del Gridview de Detalle de Campos de Informacion"

    Protected Sub ddlPageSelectorDetalleCampoInformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GVDetalleCampoInformacion.PageCount Then
                Me.GVDetalleCampoInformacion.PageIndex = _NumPag - 1
            Else
                Me.GVDetalleCampoInformacion.PageIndex = 0
            End If

            Me.GVDetalleCampoInformacion.SelectedIndex = -1

            SortGridView_DetalleCampoInformacion(ViewState("SortExpression_DetalleCampoInformacion"), ViewState("Direccion_DetalleCampoInformacion"))
            ImagenSorting_DetalleCampoInformacion()

        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDetalleCampoInformacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Eliminar" Then
                Dim int_Codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                int_CodigoAccion = 3
                eliminarCampoInformacion(int_Codigo)

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDetalleCampoInformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            If e.Row.RowType = DataControlRowType.Pager Then

                Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas_DetalleCampoInformacion")
                _TotalPags.Text = GVDetalleCampoInformacion.PageCount.ToString

                Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales_DetalleCampoInformacion")
                _Registros.Text = InformacionPager_DetalleCampoInformacion(GVDetalleCampoInformacion, e.Row, Me)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDetalleCampoInformacion_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GVDetalleCampoInformacion.PageIndex = e.NewPageIndex
            End If

            SortGridView_DetalleCampoInformacion(ViewState("SortExpression_DetalleCampoInformacion"), ViewState("Direccion_DetalleCampoInformacion"))
            ImagenSorting_DetalleCampoInformacion()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDetalleCampoInformacion_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            ViewState("SortExpression_DetalleCampoInformacion") = sortExpression

            If GridViewSortDirection_DetalleCampoInformacion = SortDirection.Ascending Then
                GridViewSortDirection_DetalleCampoInformacion = SortDirection.Descending
                SortGridView_DetalleCampoInformacion(sortExpression, "DESC")
                ViewState("Direccion_DetalleCampoInformacion") = "DESC"
            Else
                GridViewSortDirection_DetalleCampoInformacion = SortDirection.Ascending
                SortGridView_DetalleCampoInformacion(sortExpression, "ASC")
                ViewState("Direccion_DetalleCampoInformacion") = "ASC"
            End If

            ImagenSorting_DetalleCampoInformacion()
        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDetalleCampoInformacion_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager_DetalleCampoInformacion(GVDetalleCampoInformacion, e.Row, Me)
        End If

    End Sub

#End Region

#Region "Metodos del Gridview de Detalle de Campos de Informacion"

    ''' <summary>
    ''' Agrega el índice de páginas al combo de páginación. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CrearBotonesPager_DetalleCampoInformacion(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page)

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim ddlPageSelector As DropDownList = DirectCast(gvPagerRow.FindControl("ddlPageSelectorDetalleCampoInformacion"), DropDownList)
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
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function InformacionPager_DetalleCampoInformacion(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page) As String

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim pageSize As Integer = gridView.PageSize
        Dim rowCount As Integer = gridView.Rows.Count

        Dim currentPageFirstRow As Integer = ((pageIndex * pageSize) + 1)
        Dim currentPageLastRow As Integer = 0
        Dim lastPageRemainder As Integer = pageCount Mod pageSize

        currentPageLastRow = currentPageFirstRow + rowCount - 1

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegsDetalleCampoInformacion.Value)

    End Function

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Property GridViewSortDirection_DetalleCampoInformacion() As SortDirection

        Get
            If ViewState("sortDirection_DetalleCampoInformacion") Is Nothing Then
                ViewState("sortDirection_DetalleCampoInformacion") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("sortDirection_DetalleCampoInformacion"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection_DetalleCampoInformacion") = value
        End Set

    End Property

    ''' <summary>
    ''' Lista los datos de procedimientos realizados ordenados por Descripción.
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView_DetalleCampoInformacion(ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda_DetalleCamposInformacion()

        hfTotalRegsDetalleCampoInformacion.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        GVDetalleCampoInformacion.DataSource = dv
        GVDetalleCampoInformacion.DataBind()

    End Sub

    ''' <summary>
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting_DetalleCampoInformacion()

        Dim _btnSorting As ImageButton = CType(GVDetalleCampoInformacion.HeaderRow.FindControl("btnSorting"), ImageButton)

        If ViewState("Direccion_DetalleCampoInformacion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion_DetalleCampoInformacion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
            _btnSorting.ToolTip = "Ascendente"
        End If

    End Sub

#End Region

#End Region

#Region "Manejo de GridView Lista de Campos de Informacion"

#Region "Eventos del Gridview de Lista de Campos de Informacion"

    Protected Sub chkSeleccionarListaCampoInformacion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cambiarEstadoCheckBox_ListaCampoInformacion(sender, e)
            pnModalCampoInformacion.Show()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPageSelectorListaCampoInformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GVListaCampoInformacion.PageCount Then
                Me.GVListaCampoInformacion.PageIndex = _NumPag - 1
            Else
                Me.GVListaCampoInformacion.PageIndex = 0
            End If

            Me.GVListaCampoInformacion.SelectedIndex = -1

            SortGridView_ListaCampoInformacion(ViewState("SortExpression_ListaCampoInformacion"), ViewState("Direccion_ListaCampoInformacion"))
            ImagenSorting_ListaCampoInformacion()
            pnModalCampoInformacion.Show()

        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaCampoInformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim dt As DataTable
        Dim dtChk As DataTable
        Dim ds_Lista As DataSet

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas_ListaCampoInformacion")
            _TotalPags.Text = GVListaCampoInformacion.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales_ListaCampoInformacion")
            _Registros.Text = InformacionPager_ListaCampoInformacion(GVListaCampoInformacion, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            If ViewState("DetalleCampoInformacion") IsNot Nothing Then
                dt = ViewState("DetalleCampoInformacion")
                Dim cod As Integer = CInt(e.Row.Cells.Item(1).Text)
                Dim chk As CheckBox = CType(e.Row.FindControl("chkSeleccionar"), CheckBox)

                'Actualiza la columna "Check" del DataTable(ViewState) "ListaCampoInformacion"
                If validarCheckDetalleCampoInformacion(cod, dt) Then
                    Dim BGColor As String = "#dcff7d"
                    e.Row.Style.Add("background", BGColor)
                    chk.Checked = True

                    'Actualizo el Check del VS ListaBloqueInformacion
                    ds_Lista = ViewState("ListaCampoInformacion")

                    Dim VSdt As New DataTable()
                    VSdt = ds_Lista.Tables(0).Copy

                    ds_Lista.Tables.Remove("ListaCampoInformacion")

                    VSdt = MarcarCheckbox_ListaCampoInformacion(VSdt, cod, 1)

                    ds_Lista.Tables.Add(VSdt)

                    ViewState("ListaCampoInformacion") = ds_Lista
                End If

                'Marca los Checkbox del gridview
                ds_Lista = ViewState("ListaCampoInformacion")
                dtChk = ds_Lista.Tables(0).Copy
                If validarEstadoCheckDetalle_ListaCampoInformacion(cod, dtChk) Then
                    chk.Checked = True
                End If
            End If
            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If

    End Sub

    Protected Sub GVListaCampoInformacion_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GVListaCampoInformacion.PageIndex = e.NewPageIndex
            End If

            SortGridView_ListaCampoInformacion(ViewState("SortExpression_ListaCampoInformacion"), ViewState("Direccion_ListaCampoInformacion"))
            ImagenSorting_ListaCampoInformacion()
            pnModalCampoInformacion.Show()

        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaCampoInformacion_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression
            ViewState("SortExpression_ListaCampoInformacion") = sortExpression

            If GridViewSortDirection_ListaCampoInformacion = SortDirection.Ascending Then
                GridViewSortDirection_ListaCampoInformacion = SortDirection.Descending
                SortGridView_ListaCampoInformacion(sortExpression, "DESC")
                ViewState("Direccion_ListaCampoInformacion") = "DESC"
            Else
                GridViewSortDirection_ListaCampoInformacion = SortDirection.Ascending
                SortGridView_ListaCampoInformacion(sortExpression, "ASC")
                ViewState("Direccion_ListaCampoInformacion") = "ASC"
            End If

            ImagenSorting_ListaCampoInformacion()
            pnModalCampoInformacion.Show()

        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaCampoInformacion_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager_ListaCampoInformacion(GVListaCampoInformacion, e.Row, Me)
        End If

    End Sub
#End Region

#Region "Metodos del Gridview de Lista de Campos de Informacion"


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
    Private Sub cambiarEstadoCheckBox_ListaCampoInformacion(ByVal sender As Object, ByVal e As EventArgs)

        Dim chk As CheckBox = TryCast(sender, CheckBox)
        Dim estado As Boolean = chk.Checked
        Dim row As GridViewRow = TryCast(chk.NamingContainer, GridViewRow)
        Dim codigo As Integer = CInt(row.Cells.Item(1).Text)

        Dim ds_Lista As New DataSet
        ds_Lista = ViewState("ListaCampoInformacion")

        Dim dt As New DataTable()
        dt = ds_Lista.Tables(0).Copy
        ds_Lista.Tables.Remove("ListaCampoInformacion")

        If estado Then
            dt = MarcarCheckbox_ListaCampoInformacion(dt, codigo, 1)
        Else
            dt = MarcarCheckbox_ListaCampoInformacion(dt, codigo, 0)
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
    Private Function MarcarCheckbox_ListaCampoInformacion(ByVal dt As DataTable, ByVal codigo As Integer, ByVal accion As Integer) As DataTable

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
    Private Function validarEstadoCheckDetalle_ListaCampoInformacion(ByVal codigo As Integer, ByVal dtOriginal As DataTable) As Boolean
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
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CrearBotonesPager_ListaCampoInformacion(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page)

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim ddlPageSelector As DropDownList = DirectCast(gvPagerRow.FindControl("ddlPageSelectorListaCampoInformacion"), DropDownList)
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
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function InformacionPager_ListaCampoInformacion(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page) As String

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim pageSize As Integer = gridView.PageSize
        Dim rowCount As Integer = gridView.Rows.Count

        Dim currentPageFirstRow As Integer = ((pageIndex * pageSize) + 1)
        Dim currentPageLastRow As Integer = 0
        Dim lastPageRemainder As Integer = pageCount Mod pageSize

        currentPageLastRow = currentPageFirstRow + rowCount - 1

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegsListaCampoInformacion.Value)

    End Function

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Property GridViewSortDirection_ListaCampoInformacion() As SortDirection

        Get
            If ViewState("sortDirection_ListaCampoInformacion") Is Nothing Then
                ViewState("sortDirection_ListaCampoInformacion") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("sortDirection_ListaCampoInformacion"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection_ListaCampoInformacion") = value
        End Set

    End Property

    ''' <summary>
    ''' Lista los datos de procedimientos realizados ordenados por Descripción.
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView_ListaCampoInformacion(ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda_ListaCamposInformacion(2)

        hfTotalRegsListaCampoInformacion.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        GVListaCampoInformacion.DataSource = dv
        GVListaCampoInformacion.DataBind()

    End Sub

    ''' <summary>
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     22/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting_ListaCampoInformacion()

        Dim _btnSorting As ImageButton = CType(GVListaCampoInformacion.HeaderRow.FindControl("btnSorting"), ImageButton)

        If ViewState("Direccion_ListaCampoInformacion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion_ListaCampoInformacion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
            _btnSorting.ToolTip = "Ascendente"
        End If

    End Sub

#End Region

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
    ''' Fecha de Creación:     21/02/2011
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
    ''' Fecha de Creación:     21/02/2011
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
    ''' Fecha de Creación:     21/02/2011
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
    ''' Fecha de Creación:     21/02/2011
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
    ''' Fecha de Creación:     21/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting()

        Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting"), ImageButton)

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
            ImagenSorting()
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
                ElseIf e.CommandName = "Eliminar" And row.Cells(6).Text <> "Inactivo" Then
                    int_CodigoAccion = 3
                    cambiarEstado(codigo, "Eliminar")
                ElseIf e.CommandName = "Activar" And row.Cells(6).Text <> "Activo" Then
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
                btnActivar.Visible = False

                Master.SeteoPermisosAcciones(btnEliminar, 7)
                Master.SeteoPermisosAcciones(btnActualizar, 7)
            Else
                btnActivar.Attributes.Add("OnClick", "return confirm_activar();")
                btnActualizar.Visible = False
                btnEliminar.Visible = False
                e.Row.ForeColor = Drawing.Color.DarkRed

                Master.SeteoPermisosAcciones(btnActivar, 7)
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
            ImagenSorting()
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

            ImagenSorting()
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

