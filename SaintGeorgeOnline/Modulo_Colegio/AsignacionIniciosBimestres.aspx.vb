Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

''' <summary>
''' Modulo de Mantenimiento de Relacion Anios AcademicosAulas
''' </summary>
''' <remarks>
''' Código del Modulo:   
''' Código de la Opción: 
''' </remarks>

Partial Class Modulo_Colegio_AsignacionIniciosBimestres
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Asignación de Inicos de Bimestre")

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                ViewState("SortExpression") = "CodigoGrado"
                ViewState("Direccion") = "ASC"
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                'btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")

                cargarComboBuscarAnioAcademico()
                cargarComboBuscarNivel()
                cargarComboBuscarSubNivel()
                cargarComboBuscarGrados()
                cargarComboBuscarBimestre()


                listar()

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

   
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportar.Click
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLimpiar.Click
        limpiarFiltros()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        Try
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Setea las acciones de acceso del usuario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     02/03/2011
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
    ''' Fecha de Creación:     02/03/2011
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
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        ddlBuscarAnioAcademico.SelectedIndex = 0
        ddlBuscarBimestre.SelectedValue = 0
        ddlBuscarNiveles.SelectedValue = 0
        ddlBuscarSubNiveles.SelectedValue = 0
        ddlBuscarGrado.SelectedValue = 0
        'rbEstados.SelectedValue = 1
        ddlBuscarAnioAcademico.Focus()

    End Sub

    ''' <summary>
    ''' Exporta los datos del gridView en formato WORD,EXCEL,HTML,PDF,HTML.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     02/03/2011
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
        dt = Datos.agregarColumna(dt, "Año Académico", "String")
        dt = Datos.agregarColumna(dt, "Nivel", "String")
        dt = Datos.agregarColumna(dt, "SubNivel", "String")
        dt = Datos.agregarColumna(dt, "Grado", "String")
        dt = Datos.agregarColumna(dt, "Bimestre", "String")
        dt = Datos.agregarColumna(dt, "FechaInicioStr", "String")
        dt = Datos.agregarColumna(dt, "FechaFinStr", "String")
        dt = Datos.agregarColumna(dt, "Estado", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("Año Académico") = dr.Item("AnioAcademico").ToString
            auxDR.Item("Nivel") = dr.Item("Nivel").ToString
            auxDR.Item("SubNivel") = dr.Item("SubNivel").ToString
            auxDR.Item("Grado") = dr.Item("Grado").ToString
            auxDR.Item("Bimestre") = dr.Item("Bimestre").ToString
            auxDR.Item("FechaInicioStr") = dr.Item("FechaInicioStr").ToString
            auxDR.Item("FechaFinStr") = dr.Item("FechaFinStr").ToString
            auxDR.Item("Estado") = dr.Item("Estado").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next

        If rbExportar.SelectedValue = 0 Then 'WORD
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Inicios de Bimestres")
            reporte_html = Arreglo_Datos(0)
            NombreArchivo = Arreglo_Datos(1)
            NombreArchivo = NombreArchivo & ".doc"

            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Colegio", "\Reportes\")


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

            NombreArchivo = Exportacion.ExportarReporte(dt, "Inicios de Bimestres")
            NombreArchivo = NombreArchivo & ".xls"
            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Colegio", "\Reportes\")

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

            m = Exportacion.ExportarReporte_Pdf(dt, "Inicios de Bimestres")

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

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Inicios de Bimestres")
            reporte_html = Arreglo_Datos(0)
            Session("Exportaciones_RepHtml") = reporte_html
            ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresion_html();</script>", False)
        End If

    End Sub

    ''' <summary>
    ''' Carga la información de los Años Academicos activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboBuscarAnioAcademico()
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    ''' <summary>
    ''' Limpia los campos de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()

        hd_Codigo.Value = 0
        hiddenCodigoAnioAcademico.Value = 0

    End Sub

    ''' <summary>
    ''' Lista los datos      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     02/03/2011
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
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet

        Dim int_CodigoAnioAcademico As Integer = ddlBuscarAnioAcademico.SelectedValue
        Dim int_CodigoNiveles As Integer = ddlBuscarNiveles.SelectedValue
        Dim int_CodigoSubNiveles As Integer = ddlBuscarSubNiveles.SelectedValue
        Dim int_CodigoGrado As Integer = ddlBuscarGrado.SelectedValue
        Dim int_CodigoBimestre As Integer = ddlBuscarBimestre.SelectedValue

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_AsignacionIniciosBimestres As New bl_AsignacionIniciosBimestres
            ds_Lista = obj_BL_AsignacionIniciosBimestres.FUN_LIS_AsignacionIniciosBimestres( _
            int_CodigoAnioAcademico, int_CodigoNiveles, int_CodigoSubNiveles, int_CodigoGrado, int_CodigoBimestre, _
            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_AsignacionIniciosBimestres As New bl_AsignacionIniciosBimestres
                ds_Lista = obj_BL_AsignacionIniciosBimestres.FUN_LIS_AsignacionIniciosBimestres( _
                int_CodigoAnioAcademico, int_CodigoNiveles, int_CodigoSubNiveles, int_CodigoGrado, int_CodigoBimestre, _
                int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Cambia el estado de la información.     
    ''' </summary>
    ''' <param name="int_Codigo">Código de Motivo de Beca</param>
    '''  <param name="str_accion">nombre de la acción</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_BL_AsignacionIniciosBimestres As New bl_AsignacionIniciosBimestres
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado


        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_AsignacionIniciosBimestres.FUN_DEL_AsignacionIniciosBimestres(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
       End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        listar()

    End Sub

    Private Sub cargarComboBuscarNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlBuscarNiveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboBuscarSubNivel()
        Dim int_CodigoNivel As Integer
        int_CodigoNivel = ddlBuscarNiveles.SelectedValue
        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlBuscarSubNiveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga la información de las monedas
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     29/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboBuscarBimestre()
        Dim obj_BL_Bimestres As New bl_Bimestres
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Bimestres.FUN_LIS_Bimestres("", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarBimestre, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Llena el combo "ddlBuscarGrado" con la lista de grados activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     03/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboBuscarGrados()
        Dim int_CodigoSubNivel As Integer
        int_CodigoSubNivel = ddlBuscarSubNiveles.SelectedValue
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(int_CodigoSubNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarGrado, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Protected Sub ddlBuscarNiveles_SelectedIndexChanged()
        Try
            limpiarCombos(ddlBuscarSubNiveles)
            limpiarCombos(ddlBuscarGrado)
            cargarComboBuscarSubNivel()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarSubNiveles_SelectedIndexChanged()
        Try
            limpiarCombos(ddlBuscarGrado)
            cargarComboBuscarGrados()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlNiveles_SelectedIndexChanged()
        Try
            limpiarCombos(ddlSubNiveles)
            cargarComboSubNivel()
            cargarGrillaGrados()
            pnModalAgregarRegistro.Show()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlSubNiveles_SelectedIndexChanged()
        Try
            cargarGrillaGrados()
            pnModalAgregarRegistro.Show()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    '''' <summary>
    '''' Valida el formulario para el ingreso de 1 nuevo registro
    '''' </summary>
    '''' <remarks>
    '''' Creador:               Juan Vento 
    '''' Fecha de Creación:     03/03/2011
    '''' Modificado por:        _____________
    '''' Fecha de modificación: _____________ 
    '''' </remarks>
    'Private Function validarNuevoRegistro(ByRef str_Mensaje As String) As Boolean

    '    Dim result As Boolean = True
    '    Dim str_alertas As String = ""

    '    If ddlAnioAcademico.SelectedValue = 0 Then
    '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Periodo Académico")
    '        result = False
    '    End If

    '    str_Mensaje = str_alertas
    '    Return result

    'End Function

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    ''' <summary>
    ''' Elimina los elementos de la lista
    ''' </summary>
    ''' <param name="combo">Nombre del combobox</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     28/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)

        Controles.limpiarCombo(combo, False, True)

    End Sub

#End Region

#Region "Registros de Asignación de Bimestres"

#Region "Eventos"

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
            Dim usp_mensaje As String = ""
           
            limpiarCampos()
            'tbAnioAcademico.Text = ddlBuscarAnioAcademico.SelectedItem.ToString
            'hiddenCodigoAnioAcademico.Value = ddlBuscarAnioAcademico.SelectedValue
            cargarComboNivel()
            cargarComboSubNivel()
            cargarComboAnioAcademico()
            cargarComboBimestre()
            cargarGrillaGrados()
            pnModalAgregarRegistro.Show()
            

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub btnCancelar_Click()

        miTab1.Enabled = True
        TabContainer1.ActiveTabIndex = 0
        'hd_Codigo.Value = 0
        'pnModalAgregarRegistro.Hide()

    End Sub

    Protected Sub btnCerrarModal_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        'hd_Codigo.Value = 0
        'pnModalAgregarRegistro.Hide()

    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validar(usp_mensaje) Then
                Grabar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
                pnModalAgregarRegistro.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlNiveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboSubNivel()
        Dim int_CodigoNivel As Integer
        int_CodigoNivel = ddlNiveles.SelectedValue
        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlSubNiveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboAnioAcademico()
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    Private Sub cargarComboBimestre()
        Dim obj_BL_Bimestres As New bl_Bimestres
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Bimestres.FUN_LIS_Bimestres("", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBimestre, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Llena el gridview "GVListaGrados" con la lista de grados activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarGrillaGrados()
        Dim int_CodigoNivel As Integer
        int_CodigoNivel = ddlNiveles.SelectedValue
        Dim int_CodigoSubNivel As Integer
        int_CodigoSubNivel = ddlSubNiveles.SelectedValue
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_GradosXCodigoNivel(int_CodigoNivel, int_CodigoSubNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        GVListaMeses.DataSource = ds_Lista.Tables(0)
        GVListaMeses.DataBind()

    End Sub

    ''' <summary>
    ''' Graba los datos del formulario 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_AsignacionIniciosBimestres As New be_AsignacionIniciosBimestres
        Dim obj_BL_AsignacionIniciosBimestres As New bl_AsignacionIniciosBimestres

        Dim usp_mensaje As String = ""
        Dim usp_mensajeDetalle As String = ""
        Dim usp_mensajeFinal As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim bool_Grabar As Boolean = False

        Dim dt_NoRegistrados As New DataTable("ListaNoRegistrados")
        dt_NoRegistrados = Datos.agregarColumna(dt_NoRegistrados, "Idx", "Integer")
        dt_NoRegistrados = Datos.agregarColumna(dt_NoRegistrados, "Grado", "Integer")
        dt_NoRegistrados = Datos.agregarColumna(dt_NoRegistrados, "DescGrado", "String")

        Dim cont_NoRegistrados As Integer = 1
        Dim contIni As Integer = 0
        Dim contFin As Integer = 0
        Dim fechaIni As String = ""
        Dim fechaFin As String = ""
        obj_BE_AsignacionIniciosBimestres.CodigoAnioAcademico = ddlAnioAcademico.SelectedValue
        obj_BE_AsignacionIniciosBimestres.CodigoBimestre = ddlBimestre.SelectedValue

        For Each gvrM As GridViewRow In GVListaMeses.Rows
            usp_valor = 0
            If CType(gvrM.FindControl("hd_FechaInicio"), HiddenField).Value > 0 And CType(gvrM.FindControl("hd_FechaFin"), HiddenField).Value > 0 And CType(gvrM.FindControl("chkSeleccionar"), CheckBox).Checked = False Then
                'obj_BE_AsignacionIniciosBimestres.CodigoGrado = gvrM.Cells(1).Text
                If contIni = 0 Then
                    fechaIni = CType(gvrM.FindControl("tbFechaInicio"), TextBox).Text.Trim()
                    fechaFin = CType(gvrM.FindControl("tbFechaFin"), TextBox).Text.Trim()
                End If
                obj_BE_AsignacionIniciosBimestres.FecInicio = fechaIni
                obj_BE_AsignacionIniciosBimestres.FecFin = fechaFin
            ElseIf CType(gvrM.FindControl("hd_FechaInicio"), HiddenField).Value > 0 And CType(gvrM.FindControl("hd_FechaFin"), HiddenField).Value > 0 And CType(gvrM.FindControl("chkSeleccionar"), CheckBox).Checked Then
                obj_BE_AsignacionIniciosBimestres.CodigoGrado = gvrM.Cells(1).Text
                If contIni = 0 Then
                    fechaIni = CType(gvrM.FindControl("tbFechaInicio"), TextBox).Text.Trim()
                    fechaFin = CType(gvrM.FindControl("tbFechaFin"), TextBox).Text.Trim()
                End If
                obj_BE_AsignacionIniciosBimestres.FecInicio = fechaIni
                obj_BE_AsignacionIniciosBimestres.FecFin = fechaFin

                usp_valor = obj_BL_AsignacionIniciosBimestres.FUN_INS_AsignacionIniciosBimestres(obj_BE_AsignacionIniciosBimestres, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                If usp_valor = 0 Then
                ElseIf usp_valor > 0 Then
                    bool_Grabar = True

                End If
            ElseIf CType(gvrM.FindControl("hd_FechaInicio"), HiddenField).Value > 0 And CType(gvrM.FindControl("chkSeleccionar"), CheckBox).Checked Then
                obj_BE_AsignacionIniciosBimestres.CodigoGrado = gvrM.Cells(1).Text
                If contFin = 0 Then
                    fechaIni = CType(gvrM.FindControl("tbFechaInicio"), TextBox).Text.Trim
                End If
                obj_BE_AsignacionIniciosBimestres.FecInicio = fechaIni
                obj_BE_AsignacionIniciosBimestres.FecFin = CType(gvrM.FindControl("tbFechaFin"), TextBox).Text.Trim
                usp_valor = obj_BL_AsignacionIniciosBimestres.FUN_INS_AsignacionIniciosBimestres(obj_BE_AsignacionIniciosBimestres, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                If usp_valor = 0 Then
                ElseIf usp_valor > 0 Then
                    bool_Grabar = True

                End If
            ElseIf CType(gvrM.FindControl("hd_FechaFin"), HiddenField).Value > 0 And CType(gvrM.FindControl("chkSeleccionar"), CheckBox).Checked Then
                obj_BE_AsignacionIniciosBimestres.CodigoGrado = gvrM.Cells(1).Text
                If contIni = 0 Then
                    fechaFin = CType(gvrM.FindControl("tbFechaFin"), TextBox).Text.Trim
                End If
                obj_BE_AsignacionIniciosBimestres.FecInicio = CType(gvrM.FindControl("tbFechaInicio"), TextBox).Text.Trim
                obj_BE_AsignacionIniciosBimestres.FecFin = fechaFin
                usp_valor = obj_BL_AsignacionIniciosBimestres.FUN_INS_AsignacionIniciosBimestres(obj_BE_AsignacionIniciosBimestres, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                If usp_valor = 0 Then
                ElseIf usp_valor > 0 Then
                    bool_Grabar = True

                End If
            ElseIf CType(gvrM.FindControl("chkSeleccionar"), CheckBox).Checked And CType(gvrM.FindControl("hd_FechaInicio"), HiddenField).Value = 0 And CType(gvrM.FindControl("hd_FechaFin"), HiddenField).Value = 0 Then

                usp_valor = 0
                obj_BE_AsignacionIniciosBimestres.CodigoGrado = gvrM.Cells(1).Text
                obj_BE_AsignacionIniciosBimestres.FecInicio = CType(gvrM.FindControl("tbFechaInicio"), TextBox).Text.Trim
                obj_BE_AsignacionIniciosBimestres.FecFin = CType(gvrM.FindControl("tbFechaFin"), TextBox).Text.Trim

                usp_valor = obj_BL_AsignacionIniciosBimestres.FUN_INS_AsignacionIniciosBimestres(obj_BE_AsignacionIniciosBimestres, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                If usp_valor = 0 Then
                ElseIf usp_valor > 0 Then
                    bool_Grabar = True

                End If
                'If usp_valor = 0 Then
                '    Dim dr As DataRow
                '    dr = dt_NoRegistrados.NewRow
                '    dr.Item("Idx") = cont_NoRegistrados
                '    'dr.Item("Grado") = obj_BE_AsignacionIniciosBimestres.Mes
                '    'dr.Item("Meses") = obj_BE_AsignacionIniciosBimestres.CodigoGrado
                '    'dr.Item("DescGrado") = gvrG.Cells(2).Text
                '    dr.Item("DescGrado") = gvrM.Cells(2).Text
                '    dt_NoRegistrados.Rows.Add(dr)
                '    cont_NoRegistrados += 1

                'ElseIf usp_valor > 0 Then
                '    bool_Grabar = True

                'End If

            End If
            contIni = contIni + 1
            contFin = contFin + 1
        Next

       
        If bool_Grabar = True Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            'usp_mensaje = "Operacion exitosa."
            btnCancelar_Click()
            limpiarCampos()
            listar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
            'usp_mensaje = "No se grabo ningún registro."
            pnModalAgregarRegistro.Show()
        End If

        'If dt_NoRegistrados.Rows.Count > 0 Then
        '    usp_mensajeDetalle = "No se grabaron los siguientes registros :<br />"
        '    usp_mensajeDetalle += "<ol>"
        '    For Each dr As DataRow In dt_NoRegistrados.Rows
        '        usp_mensajeDetalle += "<li><em>" & dr.Item("DescGrado").ToString & "</em> .</li>"
        '    Next
        '    usp_mensajeDetalle += "</ol>"

        '    usp_mensajeFinal = usp_mensaje & "<br />" & usp_mensajeDetalle
        'Else
        '    usp_mensajeFinal = usp_mensaje
        'End If

        'MostrarSexyAlertBox(usp_mensajeFinal, "Info")
        'btnCancelar_Click()
        'limpiarCampos()
        'listar()

    End Sub

    ''' <summary>
    ''' Valida los campos del formulario antes de proceder a "Grabar"
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        Dim boolGrados As Boolean = False
        Dim boolMeses As Boolean = False

        Dim boolFechaEmision As Boolean = True
        Dim boolFechaVencimiento As Boolean = True
        Dim boolDiferenciaFecha As Boolean = True
        Dim boolCodigoBanco As Boolean = True
        Dim contIni As Integer = 0

        If ddlAnioAcademico.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "año academico")
            result = False
        End If

        If ddlBimestre.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Bimestre")
            result = False
        End If

        For Each gvrM As GridViewRow In GVListaMeses.Rows

            If CType(gvrM.FindControl("hd_FechaInicio"), HiddenField).Value > 0 And CType(gvrM.FindControl("hd_FechaFin"), HiddenField).Value > 0 And CType(gvrM.FindControl("chkSeleccionar"), CheckBox).Checked = True Then
                Dim tbFechaInicio As TextBox = CType(gvrM.FindControl("tbFechaInicio"), TextBox)
                Dim tbFechaFin As TextBox = CType(gvrM.FindControl("tbFechaFin"), TextBox)

                If contIni = 0 Then
                    If IsDate(tbFechaInicio.Text.Trim) = False Then
                        boolFechaEmision = False
                    End If

                    If IsDate(tbFechaFin.Text.Trim) = False Then
                        boolFechaVencimiento = False
                    End If

                    If IsDate(tbFechaInicio.Text.Trim) And IsDate(tbFechaFin.Text.Trim) Then
                        If (CType(tbFechaInicio.Text, Date) > CType(tbFechaFin.Text, Date)) Then
                            boolDiferenciaFecha = False
                        End If
                    End If
                End If
                boolMeses = True

            ElseIf CType(gvrM.FindControl("chkSeleccionar"), CheckBox).Checked = True And CType(gvrM.FindControl("hd_FechaInicio"), HiddenField).Value = 0 And CType(gvrM.FindControl("hd_FechaFin"), HiddenField).Value = 0 Then

                Dim tbFechaInicio As TextBox = CType(gvrM.FindControl("tbFechaInicio"), TextBox)
                Dim tbFechaFin As TextBox = CType(gvrM.FindControl("tbFechaFin"), TextBox)
             
                If IsDate(tbFechaInicio.Text.Trim) = False Then
                    boolFechaEmision = False
                End If

                If IsDate(tbFechaFin.Text.Trim) = False Then
                    boolFechaVencimiento = False
                End If

                If IsDate(tbFechaInicio.Text.Trim) And IsDate(tbFechaFin.Text.Trim) Then
                    If (CType(tbFechaInicio.Text, Date) > CType(tbFechaFin.Text, Date)) Then
                        boolDiferenciaFecha = False
                    End If
                End If

                boolMeses = True
            End If
            contIni = contIni + 1
        Next


        If boolMeses = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 12, "Grados")
            result = False
        End If

        If boolFechaEmision = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha Inicio")
            result = False
        End If

        If boolFechaVencimiento = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha Fin")
            result = False
        End If

        'If boolDiferenciaFecha = False Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 7, "Fecha de Pago")
        '    result = False
        'End If

        str_Mensaje = str_alertas
        Return result

    End Function

#End Region

#Region "Gridview"

    'Protected Sub GVListaGrados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

    '    If e.Row.RowType = DataControlRowType.DataRow Then

    '        e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
    '        e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

    '    End If

    'End Sub

    Protected Sub GVListaMeses_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim atk_cal1 As AjaxControlToolkit.CalendarExtender = CType(e.Row.FindControl("CalendarExtender1"), AjaxControlToolkit.CalendarExtender)
        Dim atk_cal2 As AjaxControlToolkit.CalendarExtender = CType(e.Row.FindControl("CalendarExtender2"), AjaxControlToolkit.CalendarExtender)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim int_Anio As Integer = ddlAnioAcademico.SelectedValue
            Dim int_Bimestre As Integer = ddlBimestre.SelectedValue

            'Select Case int_Mes
            '    Case 1 : atk_cal1.SelectedDate = "01/01/" & str_Anio : atk_cal2.SelectedDate = "01/01/" & str_Anio : codBank.Text = str_Anio & "011"
            '    Case 2 : atk_cal1.SelectedDate = "01/02/" & str_Anio : atk_cal2.SelectedDate = "01/02/" & str_Anio : codBank.Text = str_Anio & "021"
            '    Case 3 : atk_cal1.SelectedDate = "01/03/" & str_Anio : atk_cal2.SelectedDate = "01/03/" & str_Anio : codBank.Text = str_Anio & "031"
            '    Case 4 : atk_cal1.SelectedDate = "01/04/" & str_Anio : atk_cal2.SelectedDate = "01/04/" & str_Anio : codBank.Text = str_Anio & "041"
            '    Case 5 : atk_cal1.SelectedDate = "01/05/" & str_Anio : atk_cal2.SelectedDate = "01/05/" & str_Anio : codBank.Text = str_Anio & "051"
            '    Case 6 : atk_cal1.SelectedDate = "01/06/" & str_Anio : atk_cal2.SelectedDate = "01/06/" & str_Anio : codBank.Text = str_Anio & "061"
            '    Case 7 : atk_cal1.SelectedDate = "01/07/" & str_Anio : atk_cal2.SelectedDate = "01/07/" & str_Anio : codBank.Text = str_Anio & "071"
            '    Case 8 : atk_cal1.SelectedDate = "01/08/" & str_Anio : atk_cal2.SelectedDate = "01/08/" & str_Anio : codBank.Text = str_Anio & "081"
            '    Case 9 : atk_cal1.SelectedDate = "01/09/" & str_Anio : atk_cal2.SelectedDate = "01/09/" & str_Anio : codBank.Text = str_Anio & "091"
            '    Case 10 : atk_cal1.SelectedDate = "01/10/" & str_Anio : atk_cal2.SelectedDate = "01/10/" & str_Anio : codBank.Text = str_Anio & "101"
            '    Case 11 : atk_cal1.SelectedDate = "01/11/" & str_Anio : atk_cal2.SelectedDate = "01/11/" & str_Anio : codBank.Text = str_Anio & "111"
            '    Case 12 : atk_cal1.SelectedDate = "01/12/" & str_Anio : atk_cal2.SelectedDate = "01/12/" & str_Anio : codBank.Text = str_Anio & "121"
            'End Select

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

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
        pnModalAgregarRegistro.Show()
    End Sub

    Protected Sub chkAll_FI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim gv As GridView = CType(chk.Parent.Parent.Parent.Parent, GridView)
        Dim cont As Integer = 0
        If chk.Checked Then
            For Each gvr As GridViewRow In gv.Rows
                Dim txt_FechaInicio As TextBox = CType(gvr.FindControl("tbFechaInicio"), TextBox)
                Dim btn_calendarioInicio As ImageButton = CType(gvr.FindControl("Image1"), ImageButton)
                Dim hd_fecIni As HiddenField = CType(gvr.FindControl("hd_FechaInicio"), HiddenField)
                hd_fecIni.Value = 1
                If cont > 0 Then
                    txt_FechaInicio.Text = ""

                    btn_calendarioInicio.Enabled = False
                    txt_FechaInicio.Enabled = False
                    txt_FechaInicio.BackColor = Drawing.Color.SlateGray

                End If
                cont = cont + 1
            Next
        Else
            For Each gvr As GridViewRow In gv.Rows
               
                Dim txt_FechaInicio As TextBox = CType(gvr.FindControl("tbFechaInicio"), TextBox)
                Dim btn_calendarioInicio As ImageButton = CType(gvr.FindControl("Image1"), ImageButton)
                Dim hd_fecIni As HiddenField = CType(gvr.FindControl("hd_FechaInicio"), HiddenField)
                hd_fecIni.Value = 0
                If cont > 0 Then
                    btn_calendarioInicio.Enabled = True
                    txt_FechaInicio.Enabled = True
                    txt_FechaInicio.BackColor = Drawing.Color.White
                End If
                cont = cont + 1
            Next
        End If
        pnModalAgregarRegistro.Show()
    End Sub

    Protected Sub chkAll_FF_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim gv As GridView = CType(chk.Parent.Parent.Parent.Parent, GridView)
        Dim cont As Integer = 0
        If chk.Checked Then
            For Each gvr As GridViewRow In gv.Rows
                Dim txt_FechaFin As TextBox = CType(gvr.FindControl("tbFechaFin"), TextBox)
                Dim btn_calendarioFin As ImageButton = CType(gvr.FindControl("Image2"), ImageButton)
                Dim hd_fecFin As HiddenField = CType(gvr.FindControl("hd_FechaFin"), HiddenField)
                hd_fecFin.Value = 1
                If cont > 0 Then
                    txt_FechaFin.Text = ""
                    btn_calendarioFin.Enabled = False
                    txt_FechaFin.Enabled = False
                    txt_FechaFin.BackColor = Drawing.Color.SlateGray

                End If
                cont = cont + 1
            Next
        Else
            For Each gvr As GridViewRow In gv.Rows
               
                Dim btn_calendarioFin As ImageButton = CType(gvr.FindControl("Image2"), ImageButton)
                Dim txt_FechaFin As TextBox = CType(gvr.FindControl("tbFechaFin"), TextBox)
                Dim hd_fecFin As HiddenField = CType(gvr.FindControl("hd_FechaFin"), HiddenField)
                hd_fecFin.Value = 0

                If cont > 0 Then
                    btn_calendarioFin.Enabled = True
                    txt_FechaFin.Enabled = True
                    txt_FechaFin.BackColor = Drawing.Color.White
                End If
                cont = cont + 1
            Next
        End If
        pnModalAgregarRegistro.Show()
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
    ''' Fecha de Creación:     02/03/2011
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
    ''' Fecha de Creación:     02/03/2011
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
    ''' Fecha de Creación:     02/03/2011
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
    ''' Fecha de Creación:     02/03/2011
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
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_AnioAcademico"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_CodigoNivel"), ImageButton)
        Dim _btnSorting_d3 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_CodigoGrado"), ImageButton)
        Dim _btnSorting_d4 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_CodigoBimestre"), ImageButton)
        Dim _btnSorting_d5 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_FechaInicioDt"), ImageButton)
        Dim _btnSorting_d6 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_FechaFinDt"), ImageButton)

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
            If e.CommandName = "Eliminar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Eliminar" And row.Cells(5).Text <> "Inactivo" Then
                    int_CodigoAccion = 3
                    cambiarEstado(codigo, "Eliminar")
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = GridView1.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.DataItem("Estado") = "Activo" Then
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                btnEliminar.Visible = True
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

            ImagenSorting(ViewState("SortExpression"))
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
