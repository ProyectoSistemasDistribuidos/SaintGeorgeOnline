Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloSeguimiento
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloCursos
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Imports Ionic.Zip

Imports Microsoft.VisualBasic
Imports System
Imports System.Runtime.InteropServices.Marshal
Imports System.Threading
Imports System.Web
Imports System.Web.HttpContext
Imports System.Configuration
Imports System.Diagnostics
Imports System.Web.HttpServerUtility
Imports System.Web.UI.Page

Partial Class Modulo_Reportes_ReportesWeeklyReport
    Inherits System.Web.UI.Page


    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

    '#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Reportes de Seguimiento")

            If Not Page.IsPostBack Then
                cargarComboAniosAcademicos()
                cargarComboBimesres()
                cargarComboSemanas()
                cargarComboAula()
                'cargarComboAlumnos()
                'cargarComboGrado()
                'limpiarCombo(ddlAula)
                limpiarCombo(ddlAlumno)


                'btnConsultar.Attributes.Add("OnClick", "ShowMyModalPopup()")

            End If
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim usp_mensaje As String = ""
            'If validarConsultar(usp_mensaje) Then
            consultar()
            'Else
            '    MostrarAlertBox(usp_mensaje)
            'End If
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender1.hide()
    End Sub

    Protected Sub ddlBimestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlBimestre.SelectedValue > 0 Then
                cargarComboSemanas()
            Else
                limpiarCombo(ddlSemana)
            End If
            limpiarCombo(ddlAula)
            limpiarCombo(ddlAlumno)
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlSemana.SelectedValue > 0 Then
                cargarComboAula()
            Else
                limpiarCombo(ddlAula)
                limpiarCombo(ddlBimestre)
            End If
            '
            limpiarCombo(ddlAlumno)
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlAula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim int_CodigoAula As Integer = ddlAula.SelectedValue

            hiddenCodigoGrado.Value = 0
            Dim int_CodigoAnioAcademico As Integer = ddlAnioAcademico.SelectedValue
            Dim int_CodigoGrado As Integer = 0

            For Each dr As DataRow In CType(ViewState("ListaAulas"), DataTable).Rows
                If int_CodigoAula = dr.Item("Codigo") Then
                    hiddenCodigoGrado.Value = dr.Item("CodigoGrado")
                    int_CodigoGrado = hiddenCodigoGrado.Value

                    tbNivel.Text = dr.Item("DescNivelMinisterio")
                    tbGrado.Text = dr.Item("DescGradoMinisterio")
                    tbSeccion.Text = dr.Item("DescAulaMinisterio")
                    Exit For
                End If
            Next

            If int_CodigoGrado > 0 Then
                cargarComboAlumnos()
            Else
                limpiarCombo(ddlAlumno)
            End If
        
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    '#End Region

    '#Region "Metodos"

    '    ''' <summary>
    '    ''' Llena el combo "ddlAulas" con la lista de grados activos
    '    ''' </summary>
    '    ''' <remarks>
    '    ''' Creador:               Fanny Salinas
    '    ''' Fecha de Creación:     09/08/2011
    '    ''' Modificado por:        _____________
    '    ''' Fecha de modificación: _____________ 
    '    ''' </remarks>
    Private Sub cargarComboBimesres()

        Dim obj_BL_Bimestres As New bl_Bimestres
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Bimestres.FUN_LIS_Bimestres("", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlBimestre, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     09/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    Private Sub limpiarCombo(ByVal ddl As DropDownList)

        Controles.limpiarCombo(ddl, False, True)

    End Sub

    '    ''' <summary>
    '    ''' Carga el combo con la lista de Grados disponibles en estado activo
    '    ''' </summary>
    '    ''' <remarks>
    '    ''' Creador:               Juan Vento
    '    ''' Fecha de Creación:     27/07/2011
    '    ''' Modificado por:        _____________
    '    ''' Fecha de modificación: _____________ 
    '    ''' </remarks>
    Private Sub cargarComboAula()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        ViewState("ListaAulas") = ds_Lista.Tables(0)
        Controles.llenarCombo(ddlAula, ds_Lista, "Codigo", "DescAulaCompuesta2", False, True)

    End Sub
    ''' <summary>
    ''' Llena el combo "ddlCurso" con la lista de grados activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     18/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSemanas()
        Dim obj_BL_SemanasAcademicas As New bl_SemanasAcademicas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SemanasAcademicas.FUN_LIS_SemanasAcademicas("", "", "", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlSemana, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    '    ''' <summary>
    '    ''' Carga el combo con la lista de Aulas de un grado, disponibles en estado activo
    '    ''' </summary>
    '    ''' <remarks>
    '    ''' Creador:               Juan Vento
    '    ''' Fecha de Creación:     27/07/2011
    '    ''' Modificado por:        _____________
    '    ''' Fecha de modificación: _____________ 
    '    ''' </remarks>
    '    Private Sub cargarComboAula()

    '        Dim int_CodigoGrado As Integer
    '        int_CodigoGrado = ddlGrado.SelectedValue

    '        Dim obj_BL_Aulas As New bl_Aulas
    '        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
    '        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
    '        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
    '        Controles.llenarCombo(ddlAula, ds_Lista, "Codigo", "Descripcion", True, False)

    '    End Sub

    Private Sub cargarComboAlumnos()

        Dim int_anioAcademico As Integer = ddlAnioAcademico.SelectedValue
        Dim int_Bimestre As Integer = ddlBimestre.SelectedValue
        Dim int_Semana As Integer = ddlSemana.SelectedValue
        Dim int_Aula As Integer = ddlAula.SelectedValue

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        If int_anioAcademico > 0 And int_Bimestre > 0 And int_Semana > 0 And int_Aula > 0 Then
            Dim ds_Lista As DataSet = obj_BL_Alumnos.FUN_LIS_AlumnosWeeklyReportPorGradoYAula(int_anioAcademico, int_Bimestre, int_Semana, int_Aula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            Controles.llenarCombo(ddlAlumno, ds_Lista, "CodigoAlumno", "NombreCompleto", False, True)

        Else
            limpiarCombo(ddlAlumno)
        End If



    End Sub

    '    Private Function validarConsultar(ByRef str_Mensaje As String) As Boolean

    '        Dim result As Boolean = True
    '        Dim str_alertas As String = ""

    '        If ddlAsignacion.SelectedValue = 0 Then
    '            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Mes")
    '            result = False
    '        End If

    '        If ddlGrado.SelectedValue = 0 Then
    '            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Grado")
    '            result = False
    '        End If

    '        str_Mensaje = str_alertas
    '        Return result

    '    End Function

    Private Sub consultar()

        Dim int_CodigoAnioAcademico As Integer = ddlAnioAcademico.SelectedValue
        Dim int_CodigoBimestre As Integer = ddlBimestre.SelectedValue
        Dim int_CodigoSemana As Integer = ddlSemana.SelectedValue

        Dim int_CodigoGrado As Integer = hiddenCodigoGrado.Value
        Dim int_CodigoAula As Integer = ddlAula.SelectedValue
        Dim str_CodigoAlumno As String = ddlAlumno.SelectedValue

        Dim obj_BL_WeeklyReport As New bl_ProgramacionWeekly
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_WeeklyReport.FUN_LIS_WeeklyReportPorGradoAulaAlumno(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoAula, str_CodigoAlumno, int_CodigoBimestre, int_CodigoSemana, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If ds_Lista.Tables.Count = 8 Then

            If Not (ds_Lista.Tables(0).Rows.Count > 0 And ds_Lista.Tables(5).Rows.Count > 0) Then

                MostrarAlertBox("No existen registros de notas para el(los) alumno(s) consultados con los parámetros de busqueda ingresados.")

            Else

                Dim reporte_html As String = ""
                reporte_html = Exportacion.ExportarReporteWeeklyReport_Html(ds_Lista, "Weekly Report")
                Session("Exportaciones_RepWeeklyReportHtml") = reporte_html
                ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionWeeklyR_html();</script>", False)

                ''Archivo Word : Weekly Report
                'Dim rutamadre As String = ""
                'rutamadre = Server.MapPath(".")
                'rutamadre = rutamadre.Replace("\Modulo_Reportes", "\Reportes\")

                'Dim downloadBytes As Byte()
                'Dim NombreArchivo As String = ""

                'NombreArchivo = GenerarReporte(ds_Lista)
                'downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

                'Dim str_FileName As String
                'str_FileName = "WeeklyReport.doc"

                'Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
                'Response.Clear()
                'Response.Charset = ""
                'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                'Response.ContentType = "application/vnd.word"
                'Response.AddHeader("content-disposition", "attachment;filename=" & str_FileName + "; size=" + downloadBytes.Length.ToString())
                'Response.Flush()
                'Response.BinaryWrite(downloadBytes)
                'Response.End()

            End If

        End If

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Fanny salinas
    ''' Fecha de Creación:     12/08/2011
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
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:    12/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    Protected Sub MostrarAlertBox(ByVal str_Mensaje As String)

        Me.Master.MostrarMensajeAlert(str_Mensaje)

    End Sub

    '    'Public Shared Function obtenerNota(ByVal dt_Notas As DataTable, ByVal int_CodigoCurso As Integer, ByVal int_CodigoCriterio As Integer) As String
    Public Shared Function obtenerNota(ByVal dv_Notas As DataView, ByVal int_CodigoCurso As Integer, ByVal int_CodigoCriterio As Integer) As String

        Dim str_Nota As String = ""

        For Each drv As DataRowView In dv_Notas
            If drv.Item("CodigoCurso") = int_CodigoCurso And drv.Item("CodigoCriterio") = int_CodigoCriterio Then
                str_Nota = drv.Item("Nota")
                Exit For
            End If
        Next

        Return str_Nota

    End Function

#Region "Generacion Word - Compromiso Pago"

    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    Public Shared Function GenerarReporte(ByVal ds As DataSet) As String

        Dim dt_Alumnos As DataTable = ds.Tables(0)
        Dim dt_Cursos As DataTable = ds.Tables(1)
        Dim dt_Criterios As DataTable = ds.Tables(2)
        Dim dt_CriteriosYCalificativos As DataTable = ds.Tables(3)
        Dim dt_Notas As DataTable = ds.Tables(4)
        Dim dt_CabeceraTutor As DataTable = ds.Tables(5)
        Dim dt_Leyenda As DataTable = ds.Tables(6)
        Dim dt_GrupoCriterioEvaluacion As DataTable = ds.Tables(7)

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing

        Dim str_CodigoAlumno As String = ""

        'Iniciamos el Word 
        Dim saArchivo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaReportesSeguimientoWeekly").ToString()
        oWord = New Microsoft.Office.Interop.Word.Application
        oWord.Visible = False

        oDoc = oWord.Documents.Add(saArchivo)
        oDoc.Content.Copy()

        For cont As Integer = 0 To dt_Alumnos.Rows.Count - 1

            str_CodigoAlumno = dt_Alumnos.Rows(cont).Item("CodigoAlumno")

            Dim dv_Cursos As DataView = dt_Cursos.DefaultView
            dv_Cursos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

            Dim dv_Criterios As DataView = dt_Criterios.DefaultView
            dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

            Dim dv_CriteriosYCalificativos As DataView = dt_CriteriosYCalificativos.DefaultView
            dv_CriteriosYCalificativos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

            Dim dv_Notas As DataView = dt_Notas.DefaultView
            dv_Notas.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

            Dim dv_CabeceraTutor As DataView = dt_CabeceraTutor.DefaultView
            dv_CabeceraTutor.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

            'Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
            Dim oTablaCabecera As Microsoft.Office.Interop.Word.Table = Nothing
            Dim oTablaPie As Microsoft.Office.Interop.Word.Table = Nothing
            Dim oTabla As Microsoft.Office.Interop.Word.Table = Nothing
            Dim oTablaNotas As Microsoft.Office.Interop.Word.Table = Nothing
            Dim oTablaComentarioCopiaPadreFamilia As Microsoft.Office.Interop.Word.Table = Nothing
            Dim oTablaComentarioTutor As Microsoft.Office.Interop.Word.Table = Nothing
            Dim oTablaLinea As Microsoft.Office.Interop.Word.Table = Nothing
            Dim oPara1, oPara2, oPara3, oPara4, oParaVoid1, oParaVoid2, oParaVoid3, oParaPageBreak, oParaDocIni As Microsoft.Office.Interop.Word.Paragraph
            Dim sel As Microsoft.Office.Interop.Word.Selection

            If cont = 0 Then
                oParaDocIni = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oParaDocIni.Range.Text = " "
                oParaDocIni.Range.Font.Name = "Arial"
                oParaDocIni.Range.Font.Size = 10
                oParaDocIni.Range.Font.Bold = True
                With oParaDocIni.Range.ParagraphFormat
                    .SpaceBefore = 1
                    .SpaceBeforeAuto = False
                    .SpaceAfter = 1
                    .SpaceAfterAuto = False
                    .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                End With
                oParaDocIni.Range.InsertParagraph()
            End If

            'Aqui va el detalle
            obtenerFormatoInicial(oDoc, oWord, ds, str_CodigoAlumno)
            'Aqui termina el detalle

            ' Tabla Comentario Copia para el Padre de Familia
            oTablaComentarioCopiaPadreFamilia = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 2, 3)
            oTablaComentarioCopiaPadreFamilia.Range.Font.Name = "Arial"
            oTablaComentarioCopiaPadreFamilia.Range.Font.Size = 7
            oTablaComentarioCopiaPadreFamilia.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
            oTablaComentarioCopiaPadreFamilia.Borders.Enable = True

            'Columna de la tabla
            oTablaComentarioCopiaPadreFamilia.Columns(1).SetWidth(298, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
            oTablaComentarioCopiaPadreFamilia.Columns(2).SetWidth(18, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
            oTablaComentarioCopiaPadreFamilia.Columns(3).SetWidth(241, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

            oTablaComentarioCopiaPadreFamilia.Cell(1, 2).Select()
            sel = oWord.Selection
            sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            sel.Rows.Height = oWord.CentimetersToPoints(1.4)

            oTablaComentarioCopiaPadreFamilia.Cell(2, 2).Select()
            sel = oWord.Selection
            sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            sel.Rows.Height = oWord.CentimetersToPoints(1.9)

            Dim str_Firma As String = ""
            For i As Integer = 0 To 97
                str_Firma = str_Firma + "_"
            Next

            oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
            oPara1.Range.Text = ""
            oPara1.Range.InsertParagraphAfter()

            ' Tabla de Linea de separación
            oTablaLinea = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 1, 1)
            oTablaLinea.Range.Font.Name = "Arial"
            oTablaLinea.Range.Font.Size = 10
            oTablaLinea.Range.Font.Bold = False
            oTablaLinea.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
            oTablaLinea.Borders.Enable = False

            oTablaLinea.Columns(1).SetWidth(560, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

            oTablaLinea.Cell(1, 1).Range.Text = str_Firma


            oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
            oPara2.Range.Text = ""
            oPara2.Range.InsertParagraphAfter()

            obtenerFormatoInicial(oDoc, oWord, ds, str_CodigoAlumno)

            ' Tabla Comentario Tutor
            oTablaComentarioTutor = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 1, 3)
            oTablaComentarioTutor.Range.Font.Name = "Arial"
            oTablaComentarioTutor.Range.Font.Size = 7
            oTablaComentarioTutor.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
            oTablaComentarioTutor.Borders.Enable = True

            'Columna de la tabla
            oTablaComentarioTutor.Columns(1).SetWidth(298, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
            oTablaComentarioTutor.Columns(2).SetWidth(18, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
            oTablaComentarioTutor.Columns(3).SetWidth(241, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

            oTablaComentarioTutor.Cell(1, 2).Select()
            sel = oWord.Selection
            sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            sel.Rows.Height = oWord.CentimetersToPoints(1.4)

            oTablaComentarioTutor.Cell(2, 2).Select()
            sel = oWord.Selection
            sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
            sel.Rows.Height = oWord.CentimetersToPoints(1.9)




            'int_CodigoCriterio = 0

            '' Estructura y Cabecera de la Tabla Criterios y Calificativos
            'For i As Integer = 0 To dv_Criterios.Count - 1
            '    oTablaCriteriosYCalificativos.Columns(i + 1).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
            '    oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.Text = dv_Criterios(i).Item("Criterio")
            '    oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
            '    oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle

            '    int_CodigoCriterio = dv_Criterios(i).Item("CodigoCriterio")

            '    Dim dtAux As New DataTable
            '    dtAux = dv_CriteriosYCalificativos.ToTable

            '    Dim dv As DataView = dtAux.DefaultView
            '    With dv
            '        .RowFilter = "1=1 and CodigoCriterio = '" & int_CodigoCriterio & "' and CodigoAlumno = '" & str_CodigoAlumno & "'"
            '        .Sort = "OrdenCalificativo ASC"
            '    End With

            '    oTablaCriteriosYCalificativos.Cell(2, i + 1).Select()
            '    sel = oWord.Selection

            '    With sel.Range.ParagraphFormat
            '        .LeftIndent = oWord.CentimetersToPoints(0)
            '        .RightIndent = oWord.CentimetersToPoints(1)
            '        .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
            '        .SpaceBefore = 1
            '        .SpaceBeforeAuto = False
            '        .SpaceAfter = 1
            '        .SpaceAfterAuto = False
            '        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
            '    End With

            '    sel.Range.Font.Name = "Arial"
            '    sel.Range.Font.Size = 8
            '    sel.Range.Font.Bold = False

            '    For j As Integer = 0 To dv.Count - 1
            '        sel.TypeText(dv(j).Item("Nota") & " - " & dv(j).Item("Calificativo") & " : " & dv(j).Item("LeyendaIngles") & " (" & dv(j).Item("LeyendaEspaniol") & ")")
            '        sel.TypeParagraph()
            '        If j < dv.Count - 1 Then
            '            sel.TypeParagraph()
            '        End If
            '    Next

            '    int_CodigoCriterio = 0
            'Next

            'oTablaCriteriosYCalificativos.Select()
            'sel = oWord.Selection
            'sel.Find.ClearFormatting()

            'Dim dtAux2 As New DataTable
            'dtAux2 = dv_CriteriosYCalificativos.ToTable

            'For i As Integer = 0 To dtAux2.Rows.Count - 1
            '    With sel.Find
            '        .Text = dtAux2.Rows(i).Item("LeyendaEspaniol").ToString
            '        .Replacement.Text = ""
            '        .Forward = True
            '        .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
            '        .Format = False
            '        .MatchCase = False
            '        .MatchWholeWord = False
            '        .MatchWildcards = False
            '        .MatchSoundsLike = False
            '        .MatchAllWordForms = False
            '    End With
            '    sel.Find.Execute()
            '    sel.Font.Bold = True
            'Next

            'Dim str_Firma As String = ""
            'For i As Integer = 0 To 39
            '    str_Firma = str_Firma + "_"
            'Next

            'oPara4 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
            'oPara4.Range.Text = ""
            'oPara4.Range.InsertParagraphAfter()

            '' Tabla de Firma del tutor
            'oTablaFirma = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 3, 2)
            'oTablaFirma.Range.Font.Name = "Arial"
            'oTablaFirma.Range.Font.Size = 10
            'oTablaFirma.Range.Font.Bold = False
            'oTablaFirma.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
            'oTablaFirma.Borders.Enable = False

            'oTablaFirma.Columns(1).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
            'oTablaFirma.Columns(2).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

            'oTablaFirma.Cell(1, 2).Range.Text = "" 'str_Firma
            'oTablaFirma.Cell(2, 2).Range.Text = "Nombre del Tutor"
            'oTablaFirma.Cell(3, 2).Range.Text = "(Tutor)"

            'For i As Integer = 1 To 3
            '    oTablaFirma.Cell(i, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
            'Next

            'If cont < dt_Alumnos.Rows.Count - 1 Then
            '    oParaPageBreak = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
            '    oParaPageBreak.Range.Select()
            '    sel = oWord.Selection
            '    sel.Range.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdSectionBreakNextPage)
            'End If

        Next

        ' Grabar el reporte Word()
        Dim sTempFolderPath As String = System.IO.Path.GetTempPath()
        Dim str_RutaGuardar As String = ""
        Dim str_nombreDoc As String = ""

        str_nombreDoc = "WeeklyReport_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"
        str_RutaGuardar = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & str_nombreDoc
        oDoc.SaveAs(str_RutaGuardar)
        oDoc.Close()

        'Quit Word and thoroughly deallocate everything
        oWord.Quit()
        System.GC.Collect()

        Return str_nombreDoc

    End Function

    Public Shared Sub obtenerFormatoInicial(ByVal oDoc As Microsoft.Office.Interop.Word.Document, ByVal oWord As Microsoft.Office.Interop.Word.Application, ByVal ds As DataSet, ByVal str_CodigoAlumno As String)

        Dim dt_Cursos As DataTable = ds.Tables(1)
        Dim dt_Criterios As DataTable = ds.Tables(2)
        Dim dt_CriteriosYCalificativos As DataTable = ds.Tables(3)
        Dim dt_Notas As DataTable = ds.Tables(4)
        Dim dt_CabeceraTutor As DataTable = ds.Tables(5)
        Dim dt_Leyenda As DataTable = ds.Tables(6)
        Dim dt_GrupoCriterioEvaluacion As DataTable = ds.Tables(7)

        Dim oTablaCabecera As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTablaPie As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTabla As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTablaNotas As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTablaComentarioCopiaPadreFamilia As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTablaLinea As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oPara1, oPara2, oPara3, oPara4, oParaVoid1, oParaVoid2, oParaVoid3, oParaPageBreak, oParaDocIni As Microsoft.Office.Interop.Word.Paragraph
        Dim sel As Microsoft.Office.Interop.Word.Selection
        Dim dv_Cursos As DataView = dt_Cursos.DefaultView
        dv_Cursos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        'Dim dv_Criterios As DataView = dt_Criterios.DefaultView
        'dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_CriteriosYCalificativos As DataView = dt_CriteriosYCalificativos.DefaultView
        dv_CriteriosYCalificativos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_Notas As DataView = dt_Notas.DefaultView
        dv_Notas.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_CabeceraTutor As DataView = dt_CabeceraTutor.DefaultView
        dv_CabeceraTutor.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        ' Tabla datos del Alumno

        oTablaCabecera = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 4, 5)
        oTablaCabecera.Range.Font.Name = "Arial"
        oTablaCabecera.Range.Font.Size = 9
        oTablaCabecera.Range.Font.Bold = False
        oTablaCabecera.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
        oTablaCabecera.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
        oTablaCabecera.Borders.Enable = True

        oTablaCabecera.Range.InsertParagraphBefore()

        oTablaCabecera.Columns(1).SetWidth(50, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaCabecera.Columns(2).SetWidth(40.8, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaCabecera.Columns(3).SetWidth(180.8, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaCabecera.Columns(4).SetWidth(130.8, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaCabecera.Columns(5).SetWidth(150.4, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

        oTablaCabecera.Cell(1, 2).Merge(oTablaCabecera.Cell(1, 4))
        oTablaCabecera.Cell(1, 2).Range.Text = "WEEKLY REPORT"
        oTablaCabecera.Cell(1, 2).Range.Font.Size = 16
        oTablaCabecera.Cell(1, 2).Range.Font.Bold = True
        oTablaCabecera.Cell(1, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
        oTablaCabecera.Cell(1, 2).Select()
        sel = oWord.Selection
        sel.Rows.Height = oWord.CentimetersToPoints(0.2)
        'sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone

        'imagen del escudo
        oTablaCabecera.Cell(1, 1).Merge(oTablaCabecera.Cell(4, 1))
        Dim str_ImagenEscudoLogo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaReportesSeguimientoWeekly").ToString()

        oTablaCabecera.Cell(1, 1).Range.InlineShapes.AddPicture(FileName:=str_ImagenEscudoLogo, LinkToFile:=False, SaveWithDocument:=True)
        oTablaCabecera.Cell(1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
        oTablaCabecera.Cell(1, 1).Select()
        sel = oWord.Selection
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalBottom

        oParaVoid1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oParaVoid1.Range.Text = " "
        oParaVoid1.Range.Font.Name = "Arial"
        oParaVoid1.Range.Font.Size = 10
        oParaVoid1.Range.Font.Bold = True
        With oParaVoid1.Range.ParagraphFormat
            .SpaceBefore = 1
            .SpaceBeforeAuto = False
            .SpaceAfter = 1
            .SpaceAfterAuto = False
            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
        End With
        oParaVoid1.Range.InsertParagraph()

        'medio
        oTablaCabecera.Cell(1, 3).Select()
        oTablaCabecera.Cell(2, 2).Range.Text = "Name: "
        oTablaCabecera.Cell(3, 2).Range.Text = "Class: "
        oTablaCabecera.Cell(4, 2).Range.Text = "Tutor: "
        oTablaCabecera.Cell(2, 3).Range.Text = dv_CabeceraTutor(0).Item("NombreCompleto").ToString
        oTablaCabecera.Cell(3, 3).Range.Text = dv_CabeceraTutor(0).Item("DescGrado").ToString & " " & dv_CabeceraTutor(0).Item("DescAula").ToString
        oTablaCabecera.Cell(4, 3).Range.Text = dv_CabeceraTutor(0).Item("DescPersonaTutor").ToString
        oTablaCabecera.Cell(2, 4).Range.Text = dt_Leyenda.Rows(0).Item("DescCalificativoWeekly").ToString
        oTablaCabecera.Cell(3, 4).Range.Text = dt_Leyenda.Rows(1).Item("DescCalificativoWeekly").ToString
        oTablaCabecera.Cell(4, 4).Range.Text = dt_Leyenda.Rows(2).Item("DescCalificativoWeekly").ToString

        'fecha

        oTablaCabecera.Cell(1, 3).Select()
        sel = oWord.Selection
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone

        oTablaCabecera.Cell(2, 5).Select()
        sel = oWord.Selection
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone

        oTablaCabecera.Cell(3, 5).Select()
        sel = oWord.Selection
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        oTablaCabecera.Cell(3, 5).Range.Text = dv_CabeceraTutor(0).Item("DescFecha").ToString
        oTablaCabecera.Cell(3, 5).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight

        oTablaCabecera.Cell(4, 5).Select()
        sel = oWord.Selection
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        oTablaCabecera.Cell(4, 5).Range.Text = dv_CabeceraTutor(0).Item("DescBimestre").ToString & "        " & dv_CabeceraTutor(0).Item("DesSemana").ToString
        oTablaCabecera.Cell(4, 5).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
        oTablaCabecera.Cell(4, 5).Range.Font.Size = 10
        oTablaCabecera.Cell(4, 5).Range.Font.Bold = True
        'Selection.TypeText(Text:="        ")
        ' Tabla Notas
        oTablaNotas = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, dt_Criterios.Rows.Count + dt_GrupoCriterioEvaluacion.Rows.Count + 1, 12)
        oTablaNotas.Range.Font.Name = "Arial"
        oTablaNotas.Range.Font.Size = 6
        oTablaNotas.Range.Font.Bold = False
        oTablaNotas.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
        oTablaNotas.Borders.Enable = True

        oTablaNotas.Range.InsertParagraphBefore()

        'Estructura y Cabecera de la Tabla Notas
        oTablaNotas.Columns(1).SetWidth(150, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(2).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(3).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(4).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(5).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(6).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(7).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(8).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(9).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(10).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(11).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
        oTablaNotas.Columns(12).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

        oTablaNotas.Cell(1, 1).Range.Text = "Subject"
        oTablaNotas.Cell(1, 1).Range.Font.Size = 10
        oTablaNotas.Cell(1, 1).Range.Font.Bold = True
        oTablaNotas.Cell(1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter

        oTablaNotas.Cell(1, 1).Select()
        sel = oWord.Selection
        sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
        sel.Rows.Height = oWord.CentimetersToPoints(0.9)
        sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

        For i As Integer = 0 To dv_Cursos.Count - 1
            'oTablaNotas.Columns(i + 2).SetWidth(50, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
            oTablaNotas.Cell(1, i + 2).Range.Text = dv_Cursos(i).Item("DescNombreCurso").ToString
            'oTablaNotas.Cell(1, 1).Range.Font.Size = 8
            With oTablaNotas.Cell(1, i + 2).Range.ParagraphFormat
                .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                .SpaceBefore = 1
                .SpaceBeforeAuto = False
                .SpaceAfter = 1
                .SpaceAfterAuto = False
                .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
            End With
        Next

        'Detalle Notas
        Dim int_CodigoCriterio, int_CodigoCurso, int_Filas, int_Columnas As Integer
        Dim str_DescCriterio As String = ""

        int_Filas = 0
        int_Columnas = 0

        Dim int_CodigoGrupoCriterio As Integer = 0
        Dim int_CantidadCriterios As Integer = 0
        Dim str_CodigoGrupoCriterio As String = ""
        Dim int_cantAumFila = 3
        Dim dv_Criterios As DataView = dt_Criterios.DefaultView

        For i As Integer = 0 To dt_GrupoCriterioEvaluacion.Rows.Count - 1
            int_CodigoGrupoCriterio = dt_GrupoCriterioEvaluacion.Rows(i).Item("CodigoGrupoCriterio")
            str_CodigoGrupoCriterio = dt_GrupoCriterioEvaluacion.Rows(i).Item("GrupoCriterioEvaluacion")

            oTablaNotas.Cell(2 + int_CantidadCriterios, 1).Range.Text = str_CodigoGrupoCriterio
            oTablaNotas.Cell(2 + int_CantidadCriterios, 1).Range.Font.Size = 10
            oTablaNotas.Cell(2 + int_CantidadCriterios, 1).Range.Font.Bold = True
            dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "' and CodigoGrupoCriterio = '" & CStr(int_CodigoGrupoCriterio) & "'"

            While int_Filas <= dv_Criterios.Count - 1 ' Filas Criterios

                int_CodigoCriterio = dv_Criterios(int_Filas).Item("CodigoCriterio").ToString
                str_DescCriterio = dv_Criterios(int_Filas).Item("Criterio").ToString
                oTablaNotas.Cell(int_Filas + int_cantAumFila, 1).Range.Text = str_DescCriterio

                oTablaNotas.Cell(int_Filas + int_cantAumFila, 1).Select()
                sel = oWord.Selection
                sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
                'sel.Rows.Height = oWord.CentimetersToPoints(0.5)
                sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

                With oTablaNotas.Cell(int_Filas + int_cantAumFila, 1).Range.ParagraphFormat
                    .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    .LeftIndent = oWord.CentimetersToPoints(0.2)
                    .RightIndent = oWord.CentimetersToPoints(0)
                    .SpaceBefore = 1
                    .SpaceBeforeAuto = False
                    .SpaceAfter = 1
                    .SpaceAfterAuto = False
                    .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                End With

                While int_Columnas <= dv_Cursos.Count - 1 ' Columnas cursos
                    int_CodigoCurso = dv_Cursos(int_Columnas).Item("CodigoCurso")
                    oTablaNotas.Cell(int_Filas + int_cantAumFila, int_Columnas + 2).Range.Text = obtenerNota(dv_Notas, int_CodigoCurso, int_CodigoCriterio)
                    With oTablaNotas.Cell(int_Filas + int_cantAumFila, int_Columnas + 2).Range.ParagraphFormat
                        .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                        .SpaceBefore = 1
                        .SpaceBeforeAuto = False
                        .SpaceAfter = 1
                        .SpaceAfterAuto = False
                        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                    End With
                    int_Columnas += 1
                End While

                int_CantidadCriterios = dv_Criterios.Count
                str_DescCriterio = ""
                int_CodigoCurso = 0
                int_Columnas = 0
                int_Filas += 1
            End While
            int_Filas = 0
            int_CantidadCriterios = int_CantidadCriterios + 1
            int_cantAumFila = int_cantAumFila + int_CantidadCriterios
        Next
        oPara3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara3.Range.Text = ""
        oPara3.Range.InsertParagraphAfter()




        'Return int_Fila

    End Sub

#End Region

    '#End Region

End Class
