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
'j

Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports ClosedXML
Imports ClosedXML.Excel


Partial Class Modulo_Reportes_ReportesSeguimiento
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Reportes de Seguimiento")

            If Not Page.IsPostBack Then

                cargarComboMes()
                cargarComboGrado()
                limpiarCombo(ddlAula)
                limpiarCombo(ddlAlumno)


                btnConsultar.Attributes.Add("OnClick", "ShowMyModalPopup()")

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim rutas As String = ""

            Dim usp_mensaje As String = ""
            If validarConsultar(usp_mensaje) Then
                consultar()
            Else
                MostrarAlertBox(usp_mensaje)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender1.Hide()
    End Sub

    Protected Sub ddlGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlGrado.SelectedValue > 0 Then
                cargarComboAula()
            Else
                limpiarCombo(ddlAula)
            End If
            limpiarCombo(ddlAlumno)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlAula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarComboAlumnos()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub limpiarCombo(ByVal ddl As DropDownList)

        Controles.limpiarCombo(ddl, True, False)

    End Sub

    ''' <summary>
    ''' Llena el combo "ddlMes" con la lista de Meses segun la programacion de MidTerm activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     22/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboMes()

        Dim int_AnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim obj_BL_ProgramacionMidTermReport As New bl_ProgramacionMidTermReport
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_ProgramacionMidTermReport.FUN_LIS_ProgramacionMidTermReport(int_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAsignacion, ds_Lista, "Codigo", "DescCompuesta", False, True)

        Dim bool_Seleccion As Boolean = False
        Dim int_CodigoProgramacion As Integer
        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            If dr.Item("EstadoProgramacion") = "Aperturado" Then
                int_CodigoProgramacion = dr.Item("Codigo")
                bool_Seleccion = True
                Exit For
            End If
        Next

        If bool_Seleccion Then
            ddlAsignacion.SelectedValue = int_CodigoProgramacion
        Else
            ddlAsignacion.SelectedValue = 0
        End If

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Grados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboGrado()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlGrado, ds_Lista, "Codigo", "DescripcionCompuesta", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Aulas de un grado, disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAula()

        Dim int_CodigoGrado As Integer
        int_CodigoGrado = ddlGrado.SelectedValue

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAula, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboAlumnos()

        Dim int_CodigoGrado As Integer
        Dim int_CodigoAula As Integer
        int_CodigoGrado = ddlGrado.SelectedValue
        int_CodigoAula = ddlAula.SelectedValue

        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Alumnos.FUN_LIS_AlumnosMidTermReportPorGradoYAula(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoAula, _
                                                                                           int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAlumno, ds_Lista, "CodigoAlumno", "NombreCompleto", True, False)

    End Sub

    Private Function validarConsultar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlAsignacion.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Mes")
            result = False
        End If

        If ddlGrado.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Grado")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub consultar()

        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim int_CodigoProgramacion As Integer = ddlAsignacion.SelectedValue
        Dim int_TipoDocumento As Integer = 1

        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue
        Dim int_CodigoAula As Integer = ddlAula.SelectedValue
        Dim str_CodigoAlumno As String = ddlAlumno.SelectedValue

        Dim obj_BL_MidTermReport As New bl_MidTermReport
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim prueba As String = ""


        Dim ds_Lista As DataSet = obj_BL_MidTermReport.FUN_LIS_MidTermReportPorGradoAulaAlumno(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoAula, str_CodigoAlumno, int_TipoDocumento, int_CodigoProgramacion, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        '   prueba = CrearReporteNuevo(ds_Lista)

        'prueba = GenerarReporte(ds_Lista, int_CodigoGrado)


        'Exit Sub

        If ds_Lista.Tables.Count >= 6 Then


            If Not (ds_Lista.Tables(0).Rows.Count > 0 And ds_Lista.Tables(5).Rows.Count > 0) Then

                MostrarAlertBox("No existen registros de notas para el(los) alumno(s) consultados con los parámetros de busqueda ingresados.")

            Else

                'Archivo Word : Mid Term Report
                Dim rutamadre As String = ""
                rutamadre = Server.MapPath(".")
                rutamadre = rutamadre.Replace("\Modulo_Reportes", "\Reportes\")

                Dim downloadBytes As Byte()
                Dim NombreArchivo As String = ""



                NombreArchivo = CrearReporteNuevo(ds_Lista, int_CodigoGrado)



                downloadBytes = File.ReadAllBytes(NombreArchivo)



                Dim str_FileName As String
                str_FileName = "MidTermReport.doc"

                Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
                Response.Clear()
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.word"
                Response.AddHeader("content-disposition", "attachment;filename=" & "sdsdf.xls" + "; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()

            End If

        End If

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     22/07/2011
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
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     22/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    Protected Sub MostrarAlertBox(ByVal str_Mensaje As String)

        Me.Master.MostrarMensajeAlert(str_Mensaje)

    End Sub

    'Public Shared Function obtenerNota(ByVal dt_Notas As DataTable, ByVal int_CodigoCurso As Integer, ByVal int_CodigoCriterio As Integer) As String
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

    Public Shared Function GenerarReporte(ByVal ds As DataSet, ByVal int_CodigoGrado As Integer) As String

        Dim dt_Alumnos As DataTable = ds.Tables(0)
        Dim dt_Cursos As DataTable = ds.Tables(1)
        Dim dt_Criterios As DataTable = ds.Tables(2)
        Dim dt_CriteriosYCalificativos As DataTable = ds.Tables(3)
        Dim dt_Notas As DataTable = ds.Tables(4)
        Dim dt_Observacion As DataTable = ds.Tables(5)
        Dim dt_ObservacionDetalle As DataTable = ds.Tables(6)

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing


        Dim str_var1 As String = "D"
        Dim str_var2 As String = "E"

        Dim str_CodigoAlumno As String = ""

        'Iniciamos el Word 
        Dim saArchivo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaReportesSeguimiento").ToString()
        oWord = New Microsoft.Office.Interop.Word.Application
        oWord.Visible = False

        oDoc = oWord.Documents.Add(saArchivo)
        oDoc.Content.Copy()


        Try

      

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

                Dim dv_Observacion As DataView = dt_Observacion.DefaultView
                dv_Observacion.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

                Dim dv_ObservacionesCursos As DataView = dt_ObservacionDetalle.DefaultView
                dv_ObservacionesCursos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"




                'Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing



                Dim oTabla As Microsoft.Office.Interop.Word.Table = Nothing
                Dim oTablaNotas As Microsoft.Office.Interop.Word.Table = Nothing
                Dim oTablaCriteriosYCalificativos As Microsoft.Office.Interop.Word.Table = Nothing
                Dim oTablaFirma As Microsoft.Office.Interop.Word.Table = Nothing
                Dim oPara1, oPara2, oPara3, oPara4, oPara5, oPara6, oPara7, oPara8, oParaVoid1, oParaVoid2, oParaVoid3, oParaPageBreak, oParaDocIni As Microsoft.Office.Interop.Word.Paragraph
                Dim sel As Microsoft.Office.Interop.Word.Selection





                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "MID - TERM REPORT"
                oPara1.Range.Font.Name = "Arial"
                oPara1.Range.Font.Size = 14
                oPara1.Range.Font.Bold = True
                oPara1.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle
                oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter

                oPara1.Range.InsertParagraphAfter()


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

                ' Tabla datos del Alumno
                oTabla = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 2, 2)
                oTabla.Range.Font.Name = "Arial"
                oTabla.Range.Font.Size = 10
                oTabla.Range.Font.Bold = False
                oTabla.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
                oTabla.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oTabla.Borders.Enable = True
                oTabla.Columns(1).SetWidth(230, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
                oTabla.Columns(2).SetWidth(270, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
                oTabla.Cell(1, 1).Merge(oTabla.Cell(1, 2))

                oTabla.Cell(1, 1).Range.Text = "Student Name: " & dv_Observacion(0).Item("NombreCompleto").ToString
                oTabla.Cell(2, 1).Range.Text = "Class: " & dv_Observacion(0).Item("DescGrado").ToString & " " & dv_Observacion(0).Item("DescAula").ToString
                oTabla.Cell(2, 2).Range.Text = "Term: " & dv_Observacion(0).Item("DescMesProgramacion").ToString


                For i As Integer = 1 To 2

                    oTabla.Cell(i, 1).Select()
                    sel = oWord.Selection
                    sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
                    sel.Rows.Height = oWord.CentimetersToPoints(0.5)
                    sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

                    With oTabla.Cell(i, 1).Range.ParagraphFormat
                        .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        .SpaceBefore = 1
                        .SpaceBeforeAuto = False
                        .SpaceAfter = 1
                        .SpaceAfterAuto = False
                        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                    End With
                Next

                With oTabla.Cell(2, 2).Range.ParagraphFormat
                    .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    .SpaceBefore = 1
                    .SpaceBeforeAuto = False
                    .SpaceAfter = 1
                    .SpaceAfterAuto = False
                    .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                End With

                oParaVoid2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oParaVoid2.Range.Font.Name = "Arial"
                oParaVoid2.Range.Font.Size = 10
                oParaVoid2.Range.Font.Bold = True
                With oParaVoid2.Range.ParagraphFormat
                    .SpaceBefore = 1
                    .SpaceBeforeAuto = False
                    .SpaceAfter = 1
                    .SpaceAfterAuto = False
                    .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                End With
                oParaVoid2.Range.InsertParagraph()

                If int_CodigoGrado = 4 Or int_CodigoGrado = 5 Or int_CodigoGrado = 6 Or int_CodigoGrado = 6 Or int_CodigoGrado = 7 Or int_CodigoGrado = 8 Then
                    str_var1 = "F"
                    str_var2 = "VW"
                ElseIf int_CodigoGrado = 9 Or int_CodigoGrado = 10 Or int_CodigoGrado = 11 Or int_CodigoGrado = 12 Or int_CodigoGrado = 13 Or int_CodigoGrado = 14 Then
                    str_var1 = "D"
                    str_var2 = "E"
                End If

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Font.Name = "Arial"
                oPara2.Range.Text = "If your son/daughter has 3 or less in Attainment, or a " & str_var1 & " or " & str_var2 & " in Effort please ask for an interview with the teacher."
                oPara2.Range.Font.Size = 8
                oPara2.Range.Font.Bold = False
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                oPara2.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
                With oPara2.Range.ParagraphFormat
                    .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                    .SpaceBefore = 1
                    .SpaceBeforeAuto = False
                    .SpaceAfter = 1
                    .SpaceAfterAuto = False
                    .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                End With
                oPara2.Range.InsertParagraphAfter()

                oParaVoid3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oParaVoid3.Range.Font.Name = "Arial"
                oParaVoid3.Range.Font.Size = 10
                oParaVoid3.Range.Font.Bold = True
                With oParaVoid3.Range.ParagraphFormat
                    .SpaceBefore = 1
                    .SpaceBeforeAuto = False
                    .SpaceAfter = 1
                    .SpaceAfterAuto = False
                    .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                End With
                oParaVoid3.Range.InsertParagraph()

                ' Tabla Notas
                oTablaNotas = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, dv_Cursos.Count + 1, dv_Criterios.Count + 2)
                oTablaNotas.Range.Font.Name = "Arial"
                oTablaNotas.Range.Font.Size = 8
                oTablaNotas.Range.Font.Bold = False
                oTablaNotas.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
                oTablaNotas.Borders.Enable = True

                oTablaNotas.Range.InsertParagraphBefore()

                ' Estructura y Cabecera de la Tabla Notas
                oTablaNotas.Columns(1).SetWidth(170, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
                oTablaNotas.Cell(1, 1).Range.Text = "Subject"
                oTablaNotas.Cell(1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter

                oTablaNotas.Cell(1, 1).Select()
                sel = oWord.Selection
                sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
                sel.Rows.Height = oWord.CentimetersToPoints(0.5)
                sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

                For i As Integer = 0 To dv_Criterios.Count - 1
                    oTablaNotas.Columns(i + 2).SetWidth(30, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
                    oTablaNotas.Cell(1, i + 2).Range.Text = dv_Criterios(i).Item("AbreviaturaCriterio").ToString
                    With oTablaNotas.Cell(1, i + 2).Range.ParagraphFormat
                        .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                        .SpaceBefore = 1
                        .SpaceBeforeAuto = False
                        .SpaceAfter = 1
                        .SpaceAfterAuto = False
                        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                    End With
                Next

                oTablaNotas.Columns(dv_Criterios.Count + 2).SetWidth(270, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
                oTablaNotas.Cell(1, dv_Criterios.Count + 2).Range.Text = "Tutor's Comment"
                With oTablaNotas.Cell(1, dv_Criterios.Count + 2).Range.ParagraphFormat
                    .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                    .SpaceBefore = 1
                    .SpaceBeforeAuto = False
                    .SpaceAfter = 1
                    .SpaceAfterAuto = False
                    .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                End With

                Dim str_OBSProfesores As String = ""
                Dim str_ComentTutor As String = ""

                For dtObsDet As Integer = 0 To dv_ObservacionesCursos.Count - 1

                    str_OBSProfesores = str_OBSProfesores & dv_ObservacionesCursos(dtObsDet).Item("Curso").ToString & ": " & dv_ObservacionesCursos(dtObsDet).Item("ObservacionProfesor").ToString + vbCrLf + vbCrLf
                    'oPara8 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    'oPara8.Range.Text = dt_ObservacionDetalle.Rows(dtObsDet).Item("Curso").ToString & ":" & dt_ObservacionDetalle.Rows(dtObsDet).Item("ObservacionProfesor").ToString & "   "

                Next

                If dv_Observacion(0).Item("Observacion").ToString.Length > 5 Then
                    str_ComentTutor = "Tutoría: " + dv_Observacion(0).Item("Observacion").ToString + vbCrLf + vbCrLf
                End If


                oTablaNotas.Cell(2, dv_Criterios.Count + 2).Merge(oTablaNotas.Cell(dv_Cursos.Count + 1, dv_Criterios.Count + 2))
                oTablaNotas.Cell(2, dv_Criterios.Count + 2).Range.Text = dv_Observacion(0).Item("Observacion") & str_OBSProfesores
                With oTablaNotas.Cell(2, dv_Criterios.Count + 2).Range.ParagraphFormat
                    .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                    .SpaceBefore = 1
                    .SpaceBeforeAuto = False
                    .SpaceAfter = 1
                    .SpaceAfterAuto = False
                    .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                End With
                Dim int_filaObs As Integer = 2


                If dt_ObservacionDetalle.Rows.Count > 0 Then
                    Dim int_canCurso As Integer = 0

                    While int_canCurso <= dv_Cursos.Count - 1
                        'str_curso1 = dt_Cursos.Rows(int_canCurso).Item("DescNombreCurso").ToString

                        With sel.Find
                            .Text = dt_Cursos.Rows(int_canCurso).Item("DescNombreCurso").ToString & ":"
                            .Replacement.Text = ""
                            .Forward = True
                            .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                            .Format = False
                            .MatchCase = False
                            .MatchWholeWord = False
                            .MatchWildcards = False
                            .MatchSoundsLike = False
                            .MatchAllWordForms = False
                        End With
                        sel.Find.Execute()
                        sel.Font.Bold = True
                        int_canCurso = int_canCurso + 1
                        sel.Find.ClearFormatting()
                    End While


                End If



                'Detalle Notas
                Dim int_CodigoCurso, int_CodigoCriterio, int_Filas, int_Columnas As Integer
                Dim str_DescCurso As String = ""

                int_Filas = 0
                int_Columnas = 0

                While int_Filas <= dv_Cursos.Count - 1 ' Filas Cursos

                    int_CodigoCurso = dv_Cursos(int_Filas).Item("CodigoCurso").ToString
                    str_DescCurso = dv_Cursos(int_Filas).Item("DescNombreCurso").ToString

                    If int_CodigoGrado = 4 Or int_CodigoGrado = 5 Or int_CodigoGrado = 6 Or int_CodigoGrado = 6 Or int_CodigoGrado = 7 Or int_CodigoGrado = 8 Then
                        If int_Filas = dv_Cursos.Count - 1 Then
                            oTablaNotas.Cell(int_Filas + 2, 1).Range.Text = str_DescCurso + vbCrLf + vbCrLf + vbCrLf
                        Else
                            oTablaNotas.Cell(int_Filas + 2, 1).Range.Text = str_DescCurso
                        End If
                    ElseIf int_CodigoGrado = 9 Or int_CodigoGrado = 10 Or int_CodigoGrado = 11 Or int_CodigoGrado = 12 Or int_CodigoGrado = 13 Or int_CodigoGrado = 14 Then
                        oTablaNotas.Cell(int_Filas + 2, 1).Range.Text = str_DescCurso
                    End If

                    oTablaNotas.Cell(int_Filas + 2, 1).Select()
                    sel = oWord.Selection
                    sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly


                    If int_CodigoGrado = 4 Or int_CodigoGrado = 5 Or int_CodigoGrado = 6 Or int_CodigoGrado = 6 Or int_CodigoGrado = 7 Or int_CodigoGrado = 8 Then
                        If int_Filas = dv_Cursos.Count - 1 Then
                            sel.Rows.Height = oWord.CentimetersToPoints(2.0)
                        Else
                            sel.Rows.Height = oWord.CentimetersToPoints(0.5)
                        End If
                    ElseIf int_CodigoGrado = 9 Or int_CodigoGrado = 10 Or int_CodigoGrado = 11 Or int_CodigoGrado = 12 Or int_CodigoGrado = 13 Or int_CodigoGrado = 14 Then
                        sel.Rows.Height = oWord.CentimetersToPoints(0.5)
                    End If

                    sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

                    With oTablaNotas.Cell(int_Filas + 2, 1).Range.ParagraphFormat
                        .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        .LeftIndent = oWord.CentimetersToPoints(0.2)
                        .RightIndent = oWord.CentimetersToPoints(0)
                        .SpaceBefore = 1
                        .SpaceBeforeAuto = False
                        .SpaceAfter = 1
                        .SpaceAfterAuto = False
                        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                    End With

                    While int_Columnas <= dv_Criterios.Count - 1 ' Columnas Criterios
                        int_CodigoCriterio = dv_Criterios(int_Columnas).Item("CodigoCriterio")
                        oTablaNotas.Cell(int_Filas + 2, int_Columnas + 2).Range.Text = obtenerNota(dv_Notas, int_CodigoCurso, int_CodigoCriterio)
                        With oTablaNotas.Cell(int_Filas + 2, int_Columnas + 2).Range.ParagraphFormat
                            .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                            .SpaceBefore = 1
                            .SpaceBeforeAuto = False
                            .SpaceAfter = 1
                            .SpaceAfterAuto = False
                            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                        End With
                        int_Columnas += 1
                    End While

                    str_DescCurso = ""
                    int_CodigoCriterio = 0
                    int_Columnas = 0
                    int_Filas += 1
                End While

                oPara3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara3.Range.Text = ""
                oPara3.Range.InsertParagraphAfter()

                ' Tabla Criterios y Calificativos
                oTablaCriteriosYCalificativos = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 2, dv_Criterios.Count)
                oTablaCriteriosYCalificativos.Range.Font.Name = "Arial"
                oTablaCriteriosYCalificativos.Range.Font.Size = 10
                oTablaCriteriosYCalificativos.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
                oTablaCriteriosYCalificativos.Borders.Enable = False

                int_CodigoCriterio = 0

                ' Estructura y Cabecera de la Tabla Criterios y Calificativos
                For i As Integer = 0 To dv_Criterios.Count - 1
                    oTablaCriteriosYCalificativos.Columns(i + 1).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
                    oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.Text = dv_Criterios(i).Item("Criterio")
                    oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                    oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle

                    int_CodigoCriterio = dv_Criterios(i).Item("CodigoCriterio")

                    Dim dtAux As New DataTable
                    dtAux = dv_CriteriosYCalificativos.ToTable

                    Dim dv As DataView = dtAux.DefaultView
                    With dv
                        .RowFilter = "1=1 and CodigoCriterio = '" & int_CodigoCriterio & "' and CodigoAlumno = '" & str_CodigoAlumno & "'"
                        .Sort = "OrdenCalificativo ASC"
                    End With

                    oTablaCriteriosYCalificativos.Cell(2, i + 1).Select()
                    sel = oWord.Selection

                    With sel.Range.ParagraphFormat
                        .LeftIndent = oWord.CentimetersToPoints(0)
                        .RightIndent = oWord.CentimetersToPoints(1)
                        .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                        .SpaceBefore = 1
                        .SpaceBeforeAuto = False
                        .SpaceAfter = 1
                        .SpaceAfterAuto = False
                        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
                    End With

                    sel.Range.Font.Name = "Arial"
                    sel.Range.Font.Size = 8
                    sel.Range.Font.Bold = False

                    For j As Integer = 0 To dv.Count - 1
                        sel.TypeText(dv(j).Item("Nota") & " - " & dv(j).Item("Calificativo") & " : " & dv(j).Item("LeyendaIngles") & " (" & dv(j).Item("LeyendaEspaniol") & ")")
                        sel.TypeParagraph()
                        If j < dv.Count - 1 Then
                            sel.TypeParagraph()
                        End If
                    Next

                    int_CodigoCriterio = 0
                Next

                oTablaCriteriosYCalificativos.Select()
                sel = oWord.Selection
                sel.Find.ClearFormatting()

                Dim dtAux2 As New DataTable
                dtAux2 = dv_CriteriosYCalificativos.ToTable

                For i As Integer = 0 To dtAux2.Rows.Count - 1
                    With sel.Find
                        .Text = dtAux2.Rows(i).Item("LeyendaEspaniol").ToString
                        .Replacement.Text = ""
                        .Forward = True
                        .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                        .Format = False
                        .MatchCase = False
                        .MatchWholeWord = False
                        .MatchWildcards = False
                        .MatchSoundsLike = False
                        .MatchAllWordForms = False
                    End With
                    sel.Find.Execute()
                    sel.Font.Bold = True
                Next

                oPara4 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara4.Range.Text = ""
                oPara4.Range.InsertParagraphAfter()

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = ""
                oPara5.Range.InsertParagraphAfter()

                Dim str_Firma As String = ""
                For i As Integer = 0 To 39 '39
                    str_Firma = str_Firma + "_"
                Next

                Dim str_FirmaTutor As String = ""
                'For i As Integer = 0 To 20
                '    str_FirmaTutor = str_FirmaTutor + "_"
                'Next

                Dim NombrePadreFamilia As String = ""
                Dim Padrefamilia As String = ""

                If int_CodigoGrado = 4 Or int_CodigoGrado = 5 Or int_CodigoGrado = 6 Or int_CodigoGrado = 6 Or int_CodigoGrado = 7 Or int_CodigoGrado = 8 Then
                    NombrePadreFamilia = " "
                    Padrefamilia = " "
                    str_FirmaTutor = str_Firma
                    str_Firma = " "

                ElseIf int_CodigoGrado = 9 Or int_CodigoGrado = 10 Or int_CodigoGrado = 11 Or int_CodigoGrado = 12 Or int_CodigoGrado = 13 Or int_CodigoGrado = 14 Then
                    NombrePadreFamilia = "Nombre del Padre de Familia"
                    Padrefamilia = "(Padre de Familia)"
                    str_FirmaTutor = str_Firma


                    oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara6.Range.Text = ""
                    oPara6.Range.InsertParagraphAfter()

                    oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara7.Range.Text = ""
                    oPara7.Range.InsertParagraphAfter()
                End If


                ' Tabla de Firma del tutor
                oTablaFirma = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 3, 2)
                oTablaFirma.Range.Font.Name = "Arial"
                oTablaFirma.Range.Font.Size = 10
                oTablaFirma.Range.Font.Bold = False
                oTablaFirma.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
                oTablaFirma.Borders.Enable = False

                If int_CodigoGrado = 4 Or int_CodigoGrado = 5 Or int_CodigoGrado = 6 Or int_CodigoGrado = 6 Or int_CodigoGrado = 7 Or int_CodigoGrado = 8 Then

                    oTablaFirma.Columns(1).SetWidth(500, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

                    oTablaFirma.Cell(1, 1).Range.Text = str_FirmaTutor
                    oTablaFirma.Cell(2, 1).Range.Text = "Nombre del Tutor"
                    oTablaFirma.Cell(3, 1).Range.Text = "(Tutor)"

                ElseIf int_CodigoGrado = 9 Or int_CodigoGrado = 10 Or int_CodigoGrado = 11 Or int_CodigoGrado = 12 Or int_CodigoGrado = 13 Or int_CodigoGrado = 14 Then

                    oTablaFirma.Columns(1).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
                    oTablaFirma.Columns(2).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

                    oTablaFirma.Cell(1, 1).Range.Text = str_Firma
                    oTablaFirma.Cell(2, 1).Range.Text = NombrePadreFamilia
                    oTablaFirma.Cell(3, 1).Range.Text = Padrefamilia

                    oTablaFirma.Cell(1, 2).Range.Text = str_FirmaTutor
                    oTablaFirma.Cell(2, 2).Range.Text = "Nombre del Tutor"
                    oTablaFirma.Cell(3, 2).Range.Text = "(Tutor)"

                End If

                For i As Integer = 1 To 3
                    oTablaFirma.Cell(i, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                Next

                If cont < dt_Alumnos.Rows.Count - 1 Then
                    oParaPageBreak = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oParaPageBreak.Range.Select()
                    sel = oWord.Selection
                    sel.Range.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdSectionBreakNextPage)
                End If

            Next

            ' Grabar el reporte Word()
            Dim sTempFolderPath As String = System.IO.Path.GetTempPath()
            Dim str_RutaGuardar As String = ""
            Dim str_nombreDoc As String = ""

            str_nombreDoc = "MidTermReport_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"
            str_RutaGuardar = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & str_nombreDoc
            oDoc.SaveAs(str_RutaGuardar)
            oDoc.Close()

            'Quit Word and thoroughly deallocate everything
            oWord.Quit()
            System.GC.Collect()
            Return str_nombreDoc
        Catch ex As Exception

        End Try




    End Function

    Public Shared Function CrearReporteNuevo(ByVal dtLista As System.Data.DataSet, ByVal int_CodigoGrado As Integer) As String
        Try

            Dim dtAlumnos As DataTable = dtLista.Tables(0)

            Dim dtCursos As DataTable = dtLista.Tables(1)

            Dim dtaObservacionCurso As DataTable = dtLista.Tables(6)

            Dim dtNotaCurso As DataTable = dtLista.Tables(4)

            Dim dtLeyeandas As DataTable = dtLista.Tables(3)

            Dim dtCriterios As DataTable = dtLista.Tables(7)


            Dim filasInformacionSalon As DataRow = dtLista.Tables(5).Rows(0)


            Dim ArrCursos As DataRow()
            Dim ArrNotaCurso As DataRow()

            Dim arrObservacionCurso As DataRow()

            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaMidTermReport")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales, True)

            ''
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)


            Dim ws = workbook.Worksheet(1)


            Dim filasContador As Integer = 3
            Dim sumasFila As Integer = 0
            Dim contadorFilasPos As Integer = 0

            Dim empiezaFilas As Integer = 0


            Dim esPrimero As Boolean = False

            Dim lstPoicisionInf As New List(Of Integer)
            Dim lstPosUsperior As New List(Of Integer)

            Dim valorInicialInferior As Integer = 0
            Dim valorInicialSuperior As Integer = 0

            valorInicialInferior = 6
            valorInicialSuperior = 67

            Dim contInicial As Integer = 0
            For Each filasAlumnos As DataRow In dtAlumnos.Rows

                If contInicial = 0 Then

                    lstPoicisionInf.Add(6)
                    lstPosUsperior.Add(67)
                Else
                    valorInicialInferior += 62
                    valorInicialSuperior += 62

                    lstPoicisionInf.Add(valorInicialInferior)
                    lstPosUsperior.Add(valorInicialSuperior)

                End If
                contInicial += 1

            Next

            Dim contadorListasPocision As Integer = -1

            For Each filasAlumnos As DataRow In dtAlumnos.Rows
                contadorListasPocision += 1

                filasContador = lstPoicisionInf(contadorListasPocision)

                filasContador += 4
                ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 10)).Merge()
                ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 10)).Value = "MID-TERM REPORT "
                With ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 10))
                    .Style.Font.Bold = True
                    .Style.Font.Underline = XLFontUnderlineValues.Single
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontSize = 15
                End With


             
                filasContador += 2



                ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 10)).Merge()

                ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 10)).Value = "Student Name :" & filasAlumnos("NombreCompleto").ToString()

                With ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 10))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                filasContador += 1
                ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 9)).Merge()
                With ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 9))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = " Class " & filasInformacionSalon("DescGrado") & " " & filasInformacionSalon("DescAula")
                    'DescGrado(DescAula)
                    'First(Puppies)

                End With
                ws.Range(ws.Cell(filasContador, 9), ws.Cell(filasContador, 10)).Merge()

                With ws.Range(ws.Cell(filasContador, 9), ws.Cell(filasContador, 10))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = "Term: " & filasInformacionSalon("DescMesProgramacion")
                End With
                With ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 9))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin

                End With

                'DescGrado(DescAula)
                'First(Puppies)


                filasContador += 2
                ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 10)).Merge()
                With ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 10))
                    .Value = "If your son/daughter has 3 or less in Attainment, or a F or VW in Effort please ask for an interview with the teacher."
                   
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontSize = 9
                End With
                filasContador += 1

                ArrCursos = dtCursos.Select("CodigoAlumno=" & filasAlumnos("CodigoAlumno").ToString())

                Dim cantidadCursos As Integer = 0
                cantidadCursos = ArrCursos.Length

                Dim cantidadFilasRowSpan As Integer = 0
                cantidadFilasRowSpan = filasContador + 1

                Dim x As IXLRange
                Dim limSuperiorFilas As Integer = 0
                limSuperiorFilas = cantidadFilasRowSpan + cantidadCursos - 1

                ws.Range(ws.Cell(cantidadFilasRowSpan + 1, 10), ws.Cell(limSuperiorFilas + 1, 10)).Merge()


                x = ws.Range(ws.Cell(cantidadFilasRowSpan + 1, 10), ws.Cell(limSuperiorFilas + 1, 10))

                With x
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                filasContador += 1
                ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 7)).Merge()
                ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 7)).Value = "Subject"


                With ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 7))
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Cell(filasContador, 8).Value = "Attn."
                With ws.Cell(filasContador, 8)
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Cell(filasContador, 9).Value = "Effort."
                With ws.Cell(filasContador, 9)
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(filasContador, 10).Value = "Tutor's Comment"

                With ws.Cell(filasContador, 10)
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                End With
                With ws.Cell(filasContador, 10)
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                For Each filasCursos As DataRow In ArrCursos
                    filasContador += 1
                    contadorFilasPos += 1
                    ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 7)).Merge()
                    ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 7)).Value = filasCursos("DescNombreCurso").ToString()

                    With ws.Range(ws.Cell(filasContador, 2), ws.Cell(filasContador, 7))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    ArrNotaCurso = dtNotaCurso.Select("CodigoCurso=" & CInt(filasCursos("CodigoCurso").ToString()) & "and CodigoAlumno =" & _
                                                      CInt(filasAlumnos("CodigoAlumno").ToString()))


                    For Each filasNotaCurso As DataRow In ArrNotaCurso
                        If CInt(filasNotaCurso("OrdenCriterio").ToString()) = 1 Then

                            ws.Cell(filasContador, 8).Value = filasNotaCurso("Nota").ToString()
                            With ws.Cell(filasContador, 8)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            End With

                        ElseIf CInt(filasNotaCurso("OrdenCriterio").ToString()) = 2 Then
                            ws.Cell(filasContador, 9).Value = filasNotaCurso("Nota").ToString()

                            With ws.Cell(filasContador, 9)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            End With

                        End If
                    Next

                    arrObservacionCurso = dtaObservacionCurso.Select("CodigoCurso=" & CInt(filasCursos("CodigoCurso").ToString()) & " and  CodigoAlumno=" _
                                                                     & CInt(filasAlumnos("CodigoAlumno").ToString()))
                    Dim DescripcionCursos As String = ""
                    For Each filasArrObservacion As DataRow In arrObservacionCurso

                        'chr(13) + chr(10)
                        DescripcionCursos &= "-" & filasArrObservacion("ObservacionProfesor").ToString() '& Environment.NewLine
                    Next
                    x.Value = DescripcionCursos
                    With x
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        .Style.Alignment.WrapText = True
                        .Style.Font.FontSize = 10
                    End With


                Next


                Dim codCriterioTemp As Integer = 0
                Dim arrCriterio As DataRow() = Nothing
                Dim contadorColumnas As Integer = 1
                ''--------------------
                Dim inicianFilas As Integer = filasContador
                Dim limColumnaInferior As Integer = 0
                Dim limColumnaSuperior As Integer = 0

                Dim lisColumnasLimInf As New List(Of Integer)
                Dim lisColumnasSuperior As New List(Of Integer)

                Dim lisMaximo As List(Of Integer) = Nothing
                lisColumnasLimInf.Add(2)
                lisColumnasSuperior.Add(5)

                

                lisColumnasLimInf.Add(7)
                lisColumnasSuperior.Add(10)

                
                Dim indiceCriterioColumnas As Integer = -1

                filasContador += 2
                For Each filasCriterios As DataRow In dtCriterios.Rows


                    If codCriterioTemp <> CInt(filasCriterios("CE_CodigoCriterio").ToString()) And CInt(filasCriterios("CE_CodigoCriterio").ToString()) <> 8 Then
                        indiceCriterioColumnas += 1
                        contadorColumnas += 1
                        inicianFilas = filasContador
                        inicianFilas += 1
                        arrCriterio = dtCriterios.Select("CE_CodigoCriterio=" & CInt(filasCriterios("CE_CodigoCriterio").ToString()))

                        ' ws.Cell(inicianFilas, contadorColumnas).Value = filasCriterios("CE_DescripcionCriterio").ToString()



                        ws.Range(ws.Cell(inicianFilas, lisColumnasLimInf(indiceCriterioColumnas)), ws.Cell(inicianFilas, lisColumnasSuperior(indiceCriterioColumnas))).Merge()
                        ws.Range(ws.Cell(inicianFilas, lisColumnasLimInf(indiceCriterioColumnas)), ws.Cell(inicianFilas, lisColumnasSuperior(indiceCriterioColumnas))).Value = filasCriterios("CE_DescripcionCriterio").ToString()

                        With ws.Range(ws.Cell(inicianFilas, lisColumnasLimInf(indiceCriterioColumnas)), ws.Cell(inicianFilas, lisColumnasSuperior(indiceCriterioColumnas)))
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center



                        End With

                        With ws.Range(ws.Cell(inicianFilas, lisColumnasLimInf(indiceCriterioColumnas)), ws.Cell(inicianFilas, lisColumnasSuperior(indiceCriterioColumnas)))
                            .Style.Font.Bold = True
                            .Style.Font.Underline = XLFontUnderlineValues.Single
                        End With


                        'Dim colCont As Integer = 3
                        'Dim esPrimero As Integer = 0
                        'Dim finalInicial As Integer = 0

                        Dim esPrimeraColumna As Integer = 0
                        inicianFilas += 1

                        Dim filaIni As Integer = inicianFilas
                        Dim contadorFilasSpan As Integer = 0

                        Dim criterios As String = ""
                        Dim contadoFilas As Integer = 0
                        For Each filasCriterio As DataRow In arrCriterio
                            inicianFilas += 1
                            contadorFilasSpan += 1
                            ' ws.Cell(inicianFilas, contadorColumnas).Value =
                            criterios += " " & CStr(filasCriterio("ACC_Nota").ToString()) & " - " & filasCriterio("ACC_LeyendaIngles").ToString() & "(" & filasCriterio("ACC_LeyendaEspaniol").ToString() & ")" & Environment.NewLine & Environment.NewLine
                            contadoFilas += inicianFilas
                        Next

                        'lisMaximo.Add(contadoFilas + 8)

                        ws.Range(ws.Cell(filaIni, lisColumnasLimInf(indiceCriterioColumnas)), ws.Cell(filaIni + contadorFilasSpan + 8, lisColumnasSuperior(indiceCriterioColumnas))).Merge()
                        ws.Range(ws.Cell(filaIni, lisColumnasLimInf(indiceCriterioColumnas)), ws.Cell(filaIni + contadorFilasSpan + 8, lisColumnasSuperior(indiceCriterioColumnas))).Value = criterios







                        With ws.Range(ws.Cell(filaIni, lisColumnasLimInf(indiceCriterioColumnas)), ws.Cell(filaIni + contadorFilasSpan + 8, lisColumnasSuperior(indiceCriterioColumnas)))
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                            .Style.Alignment.WrapText = True
                            .Style.Font.FontSize = 10
                        End With






                    End If

                    'CE_DescripcionCriterio	ACC_LeyendaIngles	ACC_LeyendaEspaniol	CE_CodigoCriterio	CF_CodigoCalificativo	CE_AbreviaturaCriterio	CE_DescripcionEspaniolCriterio	CF_Descripcion	ACC_Nota
                    'Comments : Attainment	Producing consistently outstanding work.	Sobresaliente	1	1	Attn	NULL	Excellent	7
                    codCriterioTemp = CInt(filasCriterios("CE_CodigoCriterio").ToString())
                Next
                'Dim maximoFilas As Integer = 0
                'maximoFilas = lisMaximo.Max()

                filasContador += 25


                ws.Range(ws.Cell(filasContador, 5), ws.Cell(filasContador, 9)).Merge()
                ws.Range(ws.Cell(filasContador, 5), ws.Cell(filasContador, 9)).Value = "Nombre del Tutor"

                With ws.Range(ws.Cell(filasContador, 5), ws.Cell(filasContador, 9))
                    ' .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    '.Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    '.Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With
             
                filasContador += 1

                ws.Range(ws.Cell(filasContador, 5), ws.Cell(filasContador, 9)).Merge()
                With ws.Range(ws.Cell(filasContador, 5), ws.Cell(filasContador, 9))
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With
                ws.Range(ws.Cell(filasContador, 5), ws.Cell(filasContador, 9)).Value = "(Tutor)"

                With ws.Range(ws.Cell(filasContador, 5), ws.Cell(filasContador, 9))
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With

                ''---------------

            Next



            ws.Columns(10).Width = 40
            ws.PageSetup.PagesWide = 1

            ws.PageSetup.AdjustTo(85)

            workbook.Save()


            Return rutaREpositorioTemporales

        Catch ex As Exception

        End Try
    End Function




    'Public Shared Function GenerarReporte(ByVal ds As DataSet) As String

    '    Dim dt_Cursos As DataTable = ds.Tables(0)
    '    Dim dt_Criterios As DataTable = ds.Tables(1)
    '    Dim dt_CriteriosYCalificativos As DataTable = ds.Tables(2)
    '    Dim dt_Notas As DataTable = ds.Tables(3)
    '    Dim dt_Observacion As DataTable = ds.Tables(4)

    '    Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
    '    Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing
    '    Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
    '    Dim oTabla As Microsoft.Office.Interop.Word.Table = Nothing
    '    Dim oTablaNotas As Microsoft.Office.Interop.Word.Table = Nothing
    '    Dim oTablaCriteriosYCalificativos As Microsoft.Office.Interop.Word.Table = Nothing
    '    Dim oTablaFirma As Microsoft.Office.Interop.Word.Table = Nothing

    '    Dim oRng As Microsoft.Office.Interop.Word.Range = Nothing
    '    Dim oShape As Microsoft.Office.Interop.Word.InlineShape = Nothing
    '    Dim oPara1, oPara2, oPara3, oPara4, oParaVoid1, oParaVoid2, oParaVoid3 As Microsoft.Office.Interop.Word.Paragraph
    '    Dim oChart As Object = Nothing
    '    Dim Pos As Double = 0
    '    Dim numAlum As String = ""
    '    Dim sel As Microsoft.Office.Interop.Word.Selection

    '    'Iniciamos el Word 
    '    Dim saArchivo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaReportesSeguimiento").ToString()
    '    oWord = New Microsoft.Office.Interop.Word.Application
    '    oWord.Visible = False
    '    oDoc = oWord.Documents.Add(saArchivo)
    '    oDoc.Content.Copy()

    '    oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '    oPara1.Range.Text = "MID - TERM REPORT"
    '    oPara1.Range.Font.Name = "Arial"
    '    oPara1.Range.Font.Size = 14
    '    oPara1.Range.Font.Bold = True
    '    oPara1.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle
    '    oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '    oPara1.Range.InsertParagraphAfter()

    '    oParaVoid1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '    oParaVoid1.Range.Text = " "
    '    oParaVoid1.Range.Font.Name = "Arial"
    '    oParaVoid1.Range.Font.Size = 10
    '    oParaVoid1.Range.Font.Bold = True
    '    With oParaVoid1.Range.ParagraphFormat
    '        .SpaceBefore = 1
    '        .SpaceBeforeAuto = False
    '        .SpaceAfter = 1
    '        .SpaceAfterAuto = False
    '        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '    End With
    '    oParaVoid1.Range.InsertParagraph()

    '    ' Tabla datos del Alumno
    '    oTabla = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 2, 2)
    '    oTabla.Range.Font.Name = "Arial"
    '    oTabla.Range.Font.Size = 10
    '    oTabla.Range.Font.Bold = False
    '    oTabla.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '    oTabla.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '    oTabla.Borders.Enable = True
    '    oTabla.Cell(1, 1).Merge(oTabla.Cell(1, 2))

    '    oTabla.Cell(1, 1).Range.Text = "Student Name: " & dt_Observacion.Rows(0).Item("NombreCompleto")
    '    oTabla.Cell(2, 1).Range.Text = "Class: " & dt_Observacion.Rows(0).Item("DescGrado") & " " & dt_Observacion.Rows(0).Item("DescAula")
    '    oTabla.Cell(2, 2).Range.Text = "Term: " & dt_Observacion.Rows(0).Item("DescMesProgramacion")

    '    For i As Integer = 1 To 2

    '        oTabla.Cell(i, 1).Select()
    '        sel = oWord.Selection
    '        sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
    '        sel.Rows.Height = oWord.CentimetersToPoints(0.5)
    '        sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

    '        With oTabla.Cell(i, 1).Range.ParagraphFormat
    '            .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With
    '    Next

    '    With oTabla.Cell(2, 2).Range.ParagraphFormat
    '        .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '        .SpaceBefore = 1
    '        .SpaceBeforeAuto = False
    '        .SpaceAfter = 1
    '        .SpaceAfterAuto = False
    '        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '    End With

    '    oParaVoid2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '    oParaVoid2.Range.Font.Name = "Arial"
    '    oParaVoid2.Range.Font.Size = 10
    '    oParaVoid2.Range.Font.Bold = True
    '    With oParaVoid2.Range.ParagraphFormat
    '        .SpaceBefore = 1
    '        .SpaceBeforeAuto = False
    '        .SpaceAfter = 1
    '        .SpaceAfterAuto = False
    '        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '    End With
    '    oParaVoid2.Range.InsertParagraph()

    '    oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '    oPara2.Range.Font.Name = "Arial"
    '    oPara2.Range.Text = "If your son/daughter has 3 or less in Attainment, or a D or E in Effort please ask for an interview with the teacher."
    '    oPara2.Range.Font.Size = 8
    '    oPara2.Range.Font.Bold = False
    '    oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '    oPara2.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '    With oPara2.Range.ParagraphFormat
    '        .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '        .SpaceBefore = 1
    '        .SpaceBeforeAuto = False
    '        .SpaceAfter = 1
    '        .SpaceAfterAuto = False
    '        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '    End With
    '    oPara2.Range.InsertParagraphAfter()

    '    oParaVoid3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '    oParaVoid3.Range.Font.Name = "Arial"
    '    oParaVoid3.Range.Font.Size = 10
    '    oParaVoid3.Range.Font.Bold = True
    '    With oParaVoid3.Range.ParagraphFormat
    '        .SpaceBefore = 1
    '        .SpaceBeforeAuto = False
    '        .SpaceAfter = 1
    '        .SpaceAfterAuto = False
    '        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '    End With
    '    oParaVoid3.Range.InsertParagraph()

    '    ' Tabla Notas
    '    oTablaNotas = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, dt_Cursos.Rows.Count + 1, dt_Criterios.Rows.Count + 2)
    '    oTablaNotas.Range.Font.Name = "Arial"
    '    oTablaNotas.Range.Font.Size = 8
    '    oTablaNotas.Range.Font.Bold = False
    '    oTablaNotas.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '    oTablaNotas.Borders.Enable = True

    '    oTablaNotas.Range.InsertParagraphBefore()

    '    ' Estructura y Cabecera de la Tabla Notas
    '    oTablaNotas.Columns(1).SetWidth(200, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Cell(1, 1).Range.Text = "Subject"
    '    oTablaNotas.Cell(1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter

    '    oTablaNotas.Cell(1, 1).Select()
    '    sel = oWord.Selection
    '    sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
    '    sel.Rows.Height = oWord.CentimetersToPoints(0.5)
    '    sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

    '    For i As Integer = 0 To dt_Criterios.Rows.Count - 1
    '        oTablaNotas.Columns(i + 2).SetWidth(50, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '        oTablaNotas.Cell(1, i + 2).Range.Text = dt_Criterios.Rows(i).Item("AbreviaturaCriterio")
    '        With oTablaNotas.Cell(1, i + 2).Range.ParagraphFormat
    '            .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With
    '    Next

    '    oTablaNotas.Columns(dt_Criterios.Rows.Count + 2).SetWidth(200, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Cell(1, dt_Criterios.Rows.Count + 2).Range.Text = "Tutor's Comment"
    '    With oTablaNotas.Cell(1, dt_Criterios.Rows.Count + 2).Range.ParagraphFormat
    '        .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '        .SpaceBefore = 1
    '        .SpaceBeforeAuto = False
    '        .SpaceAfter = 1
    '        .SpaceAfterAuto = False
    '        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '    End With

    '    oTablaNotas.Cell(2, dt_Criterios.Rows.Count + 2).Merge(oTablaNotas.Cell(dt_Cursos.Rows.Count + 1, dt_Criterios.Rows.Count + 2))
    '    oTablaNotas.Cell(2, dt_Criterios.Rows.Count + 2).Range.Text = dt_Observacion.Rows(0).Item("Observacion")
    '    With oTablaNotas.Cell(2, dt_Criterios.Rows.Count + 2).Range.ParagraphFormat
    '        .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
    '        .SpaceBefore = 1
    '        .SpaceBeforeAuto = False
    '        .SpaceAfter = 1
    '        .SpaceAfterAuto = False
    '        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '    End With

    '    'Detalle Notas
    '    Dim int_CodigoCurso, int_CodigoCriterio, int_Filas, int_Columnas As Integer
    '    Dim str_DescCurso As String = ""

    '    int_Filas = 0
    '    int_Columnas = 0

    '    While int_Filas <= dt_Cursos.Rows.Count - 1 ' Filas Cursos
    '        int_CodigoCurso = dt_Cursos.Rows(int_Filas).Item("CodigoCurso")
    '        str_DescCurso = dt_Cursos.Rows(int_Filas).Item("DescNombreCurso")
    '        oTablaNotas.Cell(int_Filas + 2, 1).Range.Text = str_DescCurso

    '        oTablaNotas.Cell(int_Filas + 2, 1).Select()
    '        sel = oWord.Selection
    '        sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
    '        sel.Rows.Height = oWord.CentimetersToPoints(0.5)
    '        sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

    '        With oTablaNotas.Cell(int_Filas + 2, 1).Range.ParagraphFormat
    '            .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '            .LeftIndent = oWord.CentimetersToPoints(0.2)
    '            .RightIndent = oWord.CentimetersToPoints(0)
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With

    '        While int_Columnas <= dt_Criterios.Rows.Count - 1 ' Columnas Criterios
    '            int_CodigoCriterio = dt_Criterios.Rows(int_Columnas).Item("CodigoCriterio")
    '            oTablaNotas.Cell(int_Filas + 2, int_Columnas + 2).Range.Text = obtenerNota(dt_Notas, int_CodigoCurso, int_CodigoCriterio)
    '            With oTablaNotas.Cell(int_Filas + 2, int_Columnas + 2).Range.ParagraphFormat
    '                .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '                .SpaceBefore = 1
    '                .SpaceBeforeAuto = False
    '                .SpaceAfter = 1
    '                .SpaceAfterAuto = False
    '                .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '            End With
    '            int_Columnas += 1
    '        End While

    '        str_DescCurso = ""
    '        int_CodigoCriterio = 0
    '        int_Columnas = 0
    '        int_Filas += 1
    '    End While

    '    oPara3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '    oPara3.Range.Text = ""
    '    oPara3.Range.InsertParagraphAfter()

    '    ' Tabla Criterios y Calificativos
    '    oTablaCriteriosYCalificativos = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 2, dt_Criterios.Rows.Count)
    '    oTablaCriteriosYCalificativos.Range.Font.Name = "Arial"
    '    oTablaCriteriosYCalificativos.Range.Font.Size = 10
    '    oTablaCriteriosYCalificativos.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '    oTablaCriteriosYCalificativos.Borders.Enable = False

    '    int_CodigoCriterio = 0

    '    ' Estructura y Cabecera de la Tabla Criterios y Calificativos
    '    For i As Integer = 0 To dt_Criterios.Rows.Count - 1
    '        oTablaCriteriosYCalificativos.Columns(i + 1).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '        oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.Text = dt_Criterios.Rows(i).Item("Criterio")
    '        oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '        oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle

    '        int_CodigoCriterio = dt_Criterios.Rows(i).Item("CodigoCriterio")
    '        Dim dv As DataView = dt_CriteriosYCalificativos.DefaultView
    '        With dv
    '            .RowFilter = "1=1 and CodigoCriterio = '" & int_CodigoCriterio & "'"
    '            .Sort = "OrdenCalificativo ASC"
    '        End With

    '        oTablaCriteriosYCalificativos.Cell(2, i + 1).Select()
    '        sel = oWord.Selection

    '        With sel.Range.ParagraphFormat
    '            .LeftIndent = oWord.CentimetersToPoints(0)
    '            .RightIndent = oWord.CentimetersToPoints(1)
    '            .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With

    '        sel.Range.Font.Name = "Arial"
    '        sel.Range.Font.Size = 8
    '        sel.Range.Font.Bold = False

    '        For j As Integer = 0 To dv.Count - 1
    '            sel.TypeText(dv(j).Item("Nota") & " - " & dv(j).Item("Calificativo") & " : " & dv(j).Item("LeyendaIngles") & " (" & dv(j).Item("LeyendaEspaniol") & ")")
    '            sel.TypeParagraph()
    '            If j < dv.Count - 1 Then
    '                sel.TypeParagraph()
    '            End If
    '        Next

    '        int_CodigoCriterio = 0
    '    Next

    '    oDoc.Select()
    '    sel = oWord.Selection
    '    sel.Find.ClearFormatting()

    '    For Each dr As DataRow In dt_CriteriosYCalificativos.Rows
    '        With sel.Find
    '            .Text = dr.Item("LeyendaEspaniol").ToString
    '            .Replacement.Text = ""
    '            .Forward = True
    '            .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
    '            .Format = False
    '            .MatchCase = False
    '            .MatchWholeWord = False
    '            .MatchWildcards = False
    '            .MatchSoundsLike = False
    '            .MatchAllWordForms = False
    '        End With
    '        oWord.Selection.Find.Execute()
    '        oWord.Selection.Font.Bold = True
    '    Next

    '    Dim str_Firma As String = ""
    '    For i As Integer = 0 To 39
    '        str_Firma = str_Firma + "_"
    '    Next

    '    oPara4 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '    oPara4.Range.Text = ""
    '    oPara4.Range.InsertParagraphAfter()

    '    ' Tabla de Firma del tutor
    '    oTablaFirma = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 3, 2)
    '    oTablaFirma.Range.Font.Name = "Arial"
    '    oTablaFirma.Range.Font.Size = 10
    '    oTablaFirma.Range.Font.Bold = False
    '    oTablaFirma.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '    oTablaFirma.Borders.Enable = False

    '    oTablaFirma.Columns(1).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaFirma.Columns(2).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

    '    oTablaFirma.Cell(1, 2).Range.Text = str_Firma
    '    oTablaFirma.Cell(2, 2).Range.Text = "Nombre del Tutor"
    '    oTablaFirma.Cell(3, 2).Range.Text = "(Tutor)"

    '    For i As Integer = 1 To 3
    '        oTablaFirma.Cell(i, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '    Next


    '    ' Grabar el reporte Word()
    '    Dim sTempFolderPath As String = System.IO.Path.GetTempPath()
    '    Dim str_RutaGuardar As String = ""
    '    Dim str_nombreDoc As String = ""

    '    str_nombreDoc = "CompromisoPago_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"
    '    str_RutaGuardar = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & str_nombreDoc
    '    oDoc.SaveAs(str_RutaGuardar)
    '    oDoc.Close()

    '    'Quit Word and thoroughly deallocate everything
    '    oWord.Quit()
    '    System.GC.Collect()

    '    Return str_nombreDoc
    'End Function

#End Region

#End Region

End Class
