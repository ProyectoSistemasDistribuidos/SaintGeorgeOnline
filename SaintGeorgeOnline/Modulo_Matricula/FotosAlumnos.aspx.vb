Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloBancoLibros
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports System.Data
Imports System.Data.SqlClient
Imports SaintGeorgeOnline_Utilities
Imports System.IO

Partial Class Modulo_Matricula_FotosAlumnos
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 2
    Private cod_Opcion As Integer = 63
#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Fotos de alumnos")

            If Not Page.IsPostBack Then
                cargarCombos()
                limpiarCombos(ddlBuscarAula, True, False)
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
            Dim usp_mensaje As String = ""
            If Not validar(usp_mensaje) Then
                GrabarFicha()
            Else
                MostrarAlertas(usp_mensaje)
            End If

        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'listar()
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If ddlBuscarGrado.SelectedValue > 0 Then
            cargarComboAulas()
            'listar()
        Else
            'Dim str_alertas As String = ""
            'str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Grado")
            'MostrarAlertas(str_alertas)
            'listar()
        End If

    End Sub

    Protected Sub ddlBuscarAula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If ddlBuscarAula.SelectedValue > 0 Then
            'cargarComboAulas()
            'listar()
            'Else
            'Dim str_alertas As String = ""
            'str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Grado")
            'MostrarAlertas(str_alertas)

        End If

    End Sub

#End Region

#Region "Metodos"

    Private Sub limpiarCombos(ByVal combo As DropDownList, ByVal bool_Todos As Boolean, ByVal bool_Seleccione As Boolean)

        Controles.limpiarCombo(combo, bool_Todos, bool_Seleccione)

    End Sub
    '''' <summary>
    '''' Exporta los datos del gridView en formato WORD,EXCEL,HTML,PDF,HTML.
    '''' </summary>
    '''' <remarks>
    '''' Creador:               Fanny Salinas 
    '''' Fecha de Creación:     19/03/2012
    '''' Modificado por:        _____________
    '''' Fecha de modificación: _____________ 
    '''' </remarks>
    'Private Sub Exportar()
    '    Dim rutamadre As String = ""
    '    Dim downloadBytes As Byte()
    '    Dim contenido_exportar As String = ""
    '    Dim NombreArchivo As String = ""


    '    Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

    '    Dim dt As DataTable = New DataTable("ListaExportar")

    '    dt = Datos.agregarColumna(dt, "N°", "String")
    '    dt = Datos.agregarColumna(dt, "Codigo Alumno", "String")
    '    dt = Datos.agregarColumna(dt, "Codigo Educando", "String")
    '    dt = Datos.agregarColumna(dt, "Nombre Completo", "String")
    '    dt = Datos.agregarColumna(dt, "Grado", "String")
    '    dt = Datos.agregarColumna(dt, "Seccion", "String")
    '    dt = Datos.agregarColumna(dt, "Localizacion Ministerio", "String")

    '    Dim cont As Integer = 1
    '    Dim auxDR As DataRow

    '    For Each dr As DataRow In ds_Lista.Tables(0).Rows
    '        auxDR = dt.NewRow
    '        auxDR.Item("N°") = cont
    '        auxDR.Item("Codigo Alumno") = dr.Item("CodigoAlumno").ToString
    '        auxDR.Item("Codigo Educando") = dr.Item("CodigoEducando").ToString
    '        auxDR.Item("Nombre Completo") = dr.Item("NombreAlumno").ToString
    '        auxDR.Item("Grado") = dr.Item("Grado").ToString
    '        auxDR.Item("Seccion") = dr.Item("Aula").ToString
    '        auxDR.Item("Localizacion Ministerio") = dr.Item("LocalizacionMinisterio").ToString
    '        dt.Rows.Add(auxDR)
    '        cont += 1
    '    Next

    '    NombreArchivo = Exportacion.ExportarReporte(dt, "Aulas y Alumnos")
    '    NombreArchivo = NombreArchivo & ".xls"
    '    rutamadre = Server.MapPath(".")
    '    rutamadre = rutamadre.Replace("\Modulo_Matricula", "\Reportes\")

    '    downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

    '    Response.Charset = ""
    '    Response.ContentType = "binary/octet-stream"
    '    Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
    '    Response.Flush()
    '    Response.BinaryWrite(downloadBytes)
    '    Response.End()

    'End Sub

    'j

    Private Sub exportar()
        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim obj_BL_Libros As New bl_Alumnos
        Dim int_CodigoAnio As Integer = ddlBuscarAnioAcademico.SelectedValue()
        Dim int_CodigoGrado As Integer = ddlBuscarGrado.SelectedValue()
        Dim int_CodigoAula As Integer = ddlBuscarAula.SelectedValue()

        Dim ds_Lista As DataSet = obj_BL_Libros.FUN_LIS_FotosAlumnos(ddlBuscarAnioAcademico.SelectedValue, ddlBuscarGrado.SelectedValue, ddlBuscarAula.SelectedValue, "", "", "", "", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim reporte_html As String = ""
        reporte_html = ExportarReporteFotosAlumnos_Html(ds_Lista, "")
        Session("Exportaciones_FotosAlumnosHtml") = reporte_html
        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionFichaMedica_html();</script>", False)

    End Sub

    Public Shared Function ExportarReporteFotosAlumnos_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try

            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaFotosAlumnosHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaFotosAlumnosHtml(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)

        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try

        Return fileReaderPlantilla

    End Function

    Private Shared Function LlenarPlantillaFotosAlumnosHtml(ByVal Plantilla As String, ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim plantillaFila As String = ""
        Dim plantillaColumna As String = ""
        Dim int_cont As Integer = 0

        plantillaFila = "<tr>" & _
                        "[ColumnaFoto]" & _
                        "</tr>" & _
                        "<tr>" & _
                            "<td colspan='3'><br /></td>" & _
                        "</tr>[ListaFotosAlumnos]"

        plantillaColumna = "<td  style='width :200px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>" & _
                        "<table cellpadding='0' cellspacing='0' border='0' style='width: 200px; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 11px; vertical-align: top;'>" & _
                           " <tr><td colspan ='2' style='width :200px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='center' valign='top'>" & _
                                  "<img alt='' src='[RutaFoto]'  width ='100px' height='100px' /> </td></tr>" & _
                            "<tr><td style='padding-left :10px; width :20px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>Codigo : </td>" & _
                                "<td style='width :180px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>[CodigoAlumno]</td></tr>" & _
                            "<tr><td style='padding-left :10px; width :20px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>Apellidos:</td>" & _
                                "<td style='width :180px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>[NombreCompleto]</td></tr>" & _
                            "<tr><td style='padding-left :10px; width :20px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>Aula:</td> " & _
                                "<td style='width :180px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>[GradoAula]</td></tr>" & _
                        "</table> " & _
                     "</td>[ColumnaFoto]"

        'dsReporte.Tables(0).Rows(int_cont).Item("Titulo")
        'dsReporte.Tables(0).Rows(int_cont).Item("Editorial")
        'dsReporte.Tables(0).Rows(int_cont).Item("ISBN")

        If dsReporte.Tables(0).Rows.Count > 0 Then

            While cont_filas <= dsReporte.Tables(0).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaFotosAlumnos]", plantillaFila)

                While cont_columnas < 3

                    If cont_filas + cont_columnas <= dsReporte.Tables(0).Rows.Count - 1 Then
                        Plantilla = Plantilla.Replace("[ColumnaFoto]", plantillaColumna)
                        Plantilla = Plantilla.Replace("[RutaFoto]", ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("CodigoAlumno").ToString & ".jpg") '& "/" & dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("RutaPortada").ToString)
                        Plantilla = Plantilla.Replace("[CodigoAlumno]", dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("CodigoAlumno"))
                        Plantilla = Plantilla.Replace("[NombreCompleto]", dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("NombreCompleto"))
                        Plantilla = Plantilla.Replace("[GradoAula]", dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("GradoAula"))

                    End If

                    cont_columnas = cont_columnas + 1
                End While

                Plantilla = Plantilla.Replace("[ColumnaFoto]", "")
                cont_columnas = 0
                cont_filas = cont_filas + 3
            End While
            Plantilla = Plantilla.Replace("[ListaFotosAlumnos]", "")

        Else
            Plantilla = Plantilla.Replace("[ListaFotosAlumnos]", "<tr><td colspan='3' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>")
        End If

        Return Plantilla
    End Function

    Private Sub cargarCombos()
        cargarComboAniosAcademicos()
        cargarComboGrado()
    End Sub
    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)
        ddlBuscarAnioAcademico.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
    End Sub
    ''' <summary>
    ''' Carga el combo con el listado de grados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboGrado()
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlBuscarGrado, ds_Lista, "Codigo", "DescripcionCompuestaEspaniol", False, True)
    End Sub
    ''' <summary>
    ''' Carga el combo con la lista de Aulas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(ddlBuscarGrado.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAula, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Setear permisos de acciones sobre el formulario según la configuración del usuario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        'Me.Master.RegistrarAccesoPagina(cod_Modulo, cod_Opcion)
    End Sub

    Private Sub GrabarFicha()
        'Dim obj_BL_Alumnos As New bl_Alumnos

        'Dim usp_mensaje As String = ""
        'Dim usp_valor As Integer

        'Dim int_codigoAnioAcademico As Integer = 0
        'Dim int_codigoAula As Integer = 0
        'Dim int_codigoAlumno As String = ""

        'Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        'Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        'Dim bool_Grabar As Boolean = False

        'For Each gvr As GridViewRow In GridView1.Rows

        '    usp_valor = 0
        '    int_codigoAnioAcademico = ddlBuscarAnioAcademico.SelectedValue
        '    int_codigoAula = CType(gvr.FindControl("ddlAula"), DropDownList).SelectedValue
        '    int_codigoAlumno = CType(gvr.FindControl("lblCodigoAlumno"), Label).Text

        '    'If int_codigoAula > 0 Then
        '    usp_valor = obj_BL_Alumnos.FUN_UPD_AsignacionAulaAlumno(int_codigoAnioAcademico, int_codigoAula, int_codigoAlumno, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        '    'End If

        '    If usp_valor = 0 Then

        '    ElseIf usp_valor > 0 Then
        '        bool_Grabar = True
        '    End If

        'Next

        'If bool_Grabar = True Then
        '    usp_mensaje = "Operacion exitosa."
        '    listar()
        'Else
        '    usp_mensaje = "No se grabo ningún registro."
        'End If


        'MostrarSexyAlertBox(usp_mensaje, "Info")

    End Sub


    ''' <summary>
    ''' Valida el campo de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""
        Dim int_codigoAula As Integer = 0
        Dim int_cantidad As Integer = 0

        'For Each gvr As GridViewRow In GridView1.Rows

        '    int_codigoAula = CType(gvr.FindControl("ddlAula"), DropDownList).SelectedValue

        '    If int_codigoAula > 0 Then
        '        int_cantidad = int_cantidad + 1
        '    End If
        'Next

        'If int_cantidad > 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Aula")
        '    result = False
        'End If

        str_Mensaje = str_alertas
        Return result

    End Function


    ''' <summary>
    ''' Lista los datos de la busqueda .
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' </remarks>
    Private Sub listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)
        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        'GridView1.DataSource = ds_Lista.Tables(0)
        'GridView1.DataBind()


    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Alumnos As New bl_Alumnos

            ds_Lista = obj_BL_Alumnos.FUN_LIS_FotosAlumnos(ddlBuscarAnioAcademico.SelectedValue, ddlBuscarGrado.SelectedValue, ddlBuscarAula.SelectedValue, "", "", "", "", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_Alumnos As New bl_Alumnos
                ds_Lista = obj_BL_Alumnos.FUN_LIS_FotosAlumnos(ddlBuscarAnioAcademico.SelectedValue, ddlBuscarGrado.SelectedValue, ddlBuscarAula.SelectedValue, "", "", "", "", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function



    ''' <summary>
    ''' Limpia los filtros de búsqueda del formulario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks> 
    Private Sub limpiarFiltros()

        ddlBuscarAnioAcademico.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlBuscarAnioAcademico.Focus()

        'listar()
    End Sub

#End Region


#Region "Eventos de Grilla"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then


            'If e.Row.DataItem("CodigoAula") <> 0 Then

            '    Dim BGColor As String = "#dcff7d"
            '    e.Row.Style.Add("background", BGColor)

            'End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If



    End Sub

#End Region


#Region "Manejo de Alertas - Emails"

    ''' <summary>
    ''' Recibe mensajes y los deriva a otro metodo que los visualizara cno animación de JQuery
    ''' </summary>
    ''' <param name="str_alertas">Mensaje que se quiere visualizar</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarAlertas(ByVal str_alertas As String)

        MostrarSexyAlertBox(str_alertas, "Alert")

    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub
#End Region

End Class
