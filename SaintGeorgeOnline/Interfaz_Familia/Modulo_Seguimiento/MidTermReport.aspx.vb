Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloSeguimiento

Imports System.Data.SqlClient
Imports System.IO

Partial Class Interfaz_Familia_Modulo_Seguimiento_MidTermReport
    Inherits System.Web.UI.Page

    Dim cod_Modulo As Integer = 1
    Dim cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Master.MostrarTitulo("Mid term Report")
        Try
            If Not Page.IsPostBack Then
                ViewState("SortExpression") = "Mes"
                ViewState("Direccion") = "ASC"
                AlumnosPorCodigoFamilia()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub dl_DatosAlumno_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs)
        Dim int_CodigoAccion As Integer
        Try

            If e.CommandName = "Ver" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As DataListItem = CType(btn.NamingContainer, DataListItem)
                Dim table As HtmlTable = CType(btn.Parent.Parent.Parent, HtmlTable)
                Dim int_contItems As Integer = 0
                Dim btn_Contenedor As ImageButton
                Dim table_Contenedor As HtmlTable

                While int_contItems <= dl_DatosAlumno.Items.Count - 1
                    btn_Contenedor = dl_DatosAlumno.Items(int_contItems).FindControl("btnVer_dl")
                    table_Contenedor = CType(btn_Contenedor.Parent.Parent.Parent, HtmlTable)
                    table_Contenedor.Style.Value = "background-color:#17c4fc;"
                    int_contItems = int_contItems + 1
                End While

                If e.CommandName = "Ver" Then
                    int_CodigoAccion = 6
                    obtenerDatos(codigo)
                    table.Style.Value = "background-color:#215386;"
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub dl_DatosAlumno_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)

        Dim img As Image = e.Item.FindControl("img_Foto_dl")
        img.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & e.Item.DataItem("RutaFoto")

    End Sub

#End Region

#Region "Método"

    Public Sub exportar(ByVal int_CodigoProgramacion As Integer)

        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        'Dim int_TipoDocumento As Integer = 1

        Dim int_CodigoGrado As Integer = lblCodigoGrado.Text
        Dim int_CodigoAula As Integer = lblCodigoSeccion.Text
        Dim str_CodigoAlumno As String = lblCodigoAlumno.Text

        Dim obj_BL_MidTermReport As New bl_ProgramacionMidTermReport
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

        Dim ds_Lista As DataSet = obj_BL_MidTermReport.FUN_LIS_MidTermReportFamilia(int_CodigoAnioAcademico, str_CodigoAlumno, int_CodigoGrado, int_CodigoAula, int_CodigoProgramacion, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If ds_Lista.Tables.Count = 7 Then

            If Not (ds_Lista.Tables(0).Rows.Count > 0 And ds_Lista.Tables(5).Rows.Count > 0) Then

                'MostrarAlertBox("No existen registros de notas para el(los) alumno(s) consultados con los parámetros de busqueda ingresados.")
                Me.Master.MostrarMensajeAlert("No existen registros de notas para el(los) alumno(s) consultados con los parámetros de busqueda ingresados.")
                Exit Sub
            Else

                'Archivo Word : Mid Term Report

                Dim reporte_html As String = ""


                reporte_html = Exportacion.ExportarReporteMidTermReport_IF_Html(ds_Lista, "Mid Term Report", int_CodigoGrado)
                Session("Exportaciones_RepMidTermReportHtml") = reporte_html
                ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionMidTermR_html();</script>", False)


                'Dim rutamadre As String = ""
                'rutamadre = Server.MapPath(".")
                'rutamadre = rutamadre.Replace("\Interfaz_Familia\Modulo_Seguimiento", "\Reportes\")

                'Dim downloadBytes As Byte()
                'Dim NombreArchivo As String = ""

                'NombreArchivo = GenerarReporte(ds_Lista)
                'downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

                'Dim str_FileName As String
                'str_FileName = "MidTermReport.doc"

                'Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
                'Response.Clear()
                'Response.BufferOutput = False
                'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                'Response.ContentType = "application/vnd.word"
                'Response.AddHeader("content-disposition", "attachment;filename=" & str_FileName + "; size=" + downloadBytes.Length.ToString())
                'Response.BinaryWrite(downloadBytes)
                'Response.Flush()
                'Response.Close()
                'Response.End()

            End If

        End If
    End Sub


#Region "Generacion Word - Compromiso Pago"

    'Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    'Public Shared Function GenerarReporte(ByVal ds As DataSet) As String

    '    Dim dt_Alumnos As DataTable = ds.Tables(0)
    '    Dim dt_Cursos As DataTable = ds.Tables(1)
    '    Dim dt_Criterios As DataTable = ds.Tables(2)
    '    Dim dt_CriteriosYCalificativos As DataTable = ds.Tables(3)
    '    Dim dt_Notas As DataTable = ds.Tables(4)
    '    Dim dt_Observacion As DataTable = ds.Tables(5)

    '    Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
    '    Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing

    '    Dim str_CodigoAlumno As String = ""

    '    'Iniciamos el Word 
    '    Dim saArchivo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaReportesSeguimiento").ToString()
    '    oWord = New Microsoft.Office.Interop.Word.Application
    '    oWord.Visible = False

    '    oDoc = oWord.Documents.Add(saArchivo)
    '    oDoc.Content.Copy()

    '    For cont As Integer = 0 To dt_Alumnos.Rows.Count - 1

    '        str_CodigoAlumno = dt_Alumnos.Rows(cont).Item("CodigoAlumno")

    '        Dim dv_Cursos As DataView = dt_Cursos.DefaultView
    '        dv_Cursos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '        Dim dv_Criterios As DataView = dt_Criterios.DefaultView
    '        dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '        Dim dv_CriteriosYCalificativos As DataView = dt_CriteriosYCalificativos.DefaultView
    '        dv_CriteriosYCalificativos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '        Dim dv_Notas As DataView = dt_Notas.DefaultView
    '        dv_Notas.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '        Dim dv_Observacion As DataView = dt_Observacion.DefaultView
    '        dv_Observacion.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '        'Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
    '        Dim oTabla As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oTablaNotas As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oTablaCriteriosYCalificativos As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oTablaFirma As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oPara1, oPara2, oPara3, oPara4, oParaVoid1, oParaVoid2, oParaVoid3, oParaPageBreak, oParaDocIni As Microsoft.Office.Interop.Word.Paragraph
    '        Dim sel As Microsoft.Office.Interop.Word.Selection

    '        If cont = 0 Then
    '            oParaDocIni = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '            oParaDocIni.Range.Text = " "
    '            oParaDocIni.Range.Font.Name = "Arial"
    '            oParaDocIni.Range.Font.Size = 10
    '            oParaDocIni.Range.Font.Bold = True
    '            With oParaDocIni.Range.ParagraphFormat
    '                .SpaceBefore = 1
    '                .SpaceBeforeAuto = False
    '                .SpaceAfter = 1
    '                .SpaceAfterAuto = False
    '                .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '            End With
    '            oParaDocIni.Range.InsertParagraph()
    '        End If


    '        oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '        oPara1.Range.Text = "MID - TERM REPORT"
    '        oPara1.Range.Font.Name = "Arial"
    '        oPara1.Range.Font.Size = 14
    '        oPara1.Range.Font.Bold = True
    '        oPara1.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle
    '        oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '        oPara1.Range.InsertParagraphAfter()

    '        oParaVoid1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '        oParaVoid1.Range.Text = " "
    '        oParaVoid1.Range.Font.Name = "Arial"
    '        oParaVoid1.Range.Font.Size = 10
    '        oParaVoid1.Range.Font.Bold = True
    '        With oParaVoid1.Range.ParagraphFormat
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With
    '        oParaVoid1.Range.InsertParagraph()

    '        ' Tabla datos del Alumno
    '        oTabla = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 2, 2)
    '        oTabla.Range.Font.Name = "Arial"
    '        oTabla.Range.Font.Size = 10
    '        oTabla.Range.Font.Bold = False
    '        oTabla.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '        oTabla.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '        oTabla.Borders.Enable = True
    '        oTabla.Cell(1, 1).Merge(oTabla.Cell(1, 2))

    '        oTabla.Cell(1, 1).Range.Text = "Student Name: " & dv_Observacion(0).Item("NombreCompleto").ToString
    '        oTabla.Cell(2, 1).Range.Text = "Class: " & dv_Observacion(0).Item("DescGrado").ToString & " " & dv_Observacion(0).Item("DescAula").ToString
    '        oTabla.Cell(2, 2).Range.Text = "Term: " & dv_Observacion(0).Item("DescMesProgramacion").ToString

    '        For i As Integer = 1 To 2

    '            oTabla.Cell(i, 1).Select()
    '            sel = oWord.Selection
    '            sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
    '            sel.Rows.Height = oWord.CentimetersToPoints(0.5)
    '            sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

    '            With oTabla.Cell(i, 1).Range.ParagraphFormat
    '                .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '                .SpaceBefore = 1
    '                .SpaceBeforeAuto = False
    '                .SpaceAfter = 1
    '                .SpaceAfterAuto = False
    '                .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '            End With
    '        Next

    '        With oTabla.Cell(2, 2).Range.ParagraphFormat
    '            .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With

    '        oParaVoid2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '        oParaVoid2.Range.Font.Name = "Arial"
    '        oParaVoid2.Range.Font.Size = 10
    '        oParaVoid2.Range.Font.Bold = True
    '        With oParaVoid2.Range.ParagraphFormat
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With
    '        oParaVoid2.Range.InsertParagraph()

    '        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '        oPara2.Range.Font.Name = "Arial"
    '        oPara2.Range.Text = "If your son/daughter has 3 or less in Attainment, or a D or E in Effort please ask for an interview with the teacher."
    '        oPara2.Range.Font.Size = 8
    '        oPara2.Range.Font.Bold = False
    '        oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '        oPara2.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '        With oPara2.Range.ParagraphFormat
    '            .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With
    '        oPara2.Range.InsertParagraphAfter()

    '        oParaVoid3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '        oParaVoid3.Range.Font.Name = "Arial"
    '        oParaVoid3.Range.Font.Size = 10
    '        oParaVoid3.Range.Font.Bold = True
    '        With oParaVoid3.Range.ParagraphFormat
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With
    '        oParaVoid3.Range.InsertParagraph()

    '        ' Tabla Notas
    '        oTablaNotas = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, dv_Cursos.Count + 1, dv_Criterios.Count + 2)
    '        oTablaNotas.Range.Font.Name = "Arial"
    '        oTablaNotas.Range.Font.Size = 8
    '        oTablaNotas.Range.Font.Bold = False
    '        oTablaNotas.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '        oTablaNotas.Borders.Enable = True

    '        oTablaNotas.Range.InsertParagraphBefore()

    '        ' Estructura y Cabecera de la Tabla Notas
    '        oTablaNotas.Columns(1).SetWidth(200, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '        oTablaNotas.Cell(1, 1).Range.Text = "Subject"
    '        oTablaNotas.Cell(1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter

    '        oTablaNotas.Cell(1, 1).Select()
    '        sel = oWord.Selection
    '        sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
    '        sel.Rows.Height = oWord.CentimetersToPoints(0.5)
    '        sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

    '        For i As Integer = 0 To dv_Criterios.Count - 1
    '            oTablaNotas.Columns(i + 2).SetWidth(50, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '            oTablaNotas.Cell(1, i + 2).Range.Text = dv_Criterios(i).Item("AbreviaturaCriterio").ToString
    '            With oTablaNotas.Cell(1, i + 2).Range.ParagraphFormat
    '                .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '                .SpaceBefore = 1
    '                .SpaceBeforeAuto = False
    '                .SpaceAfter = 1
    '                .SpaceAfterAuto = False
    '                .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '            End With
    '        Next

    '        oTablaNotas.Columns(dv_Criterios.Count + 2).SetWidth(200, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '        oTablaNotas.Cell(1, dv_Criterios.Count + 2).Range.Text = "Tutor's Comment"
    '        With oTablaNotas.Cell(1, dv_Criterios.Count + 2).Range.ParagraphFormat
    '            .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With

    '        oTablaNotas.Cell(2, dv_Criterios.Count + 2).Merge(oTablaNotas.Cell(dv_Cursos.Count + 1, dv_Criterios.Count + 2))
    '        oTablaNotas.Cell(2, dv_Criterios.Count + 2).Range.Text = dv_Observacion(0).Item("Observacion")
    '        With oTablaNotas.Cell(2, dv_Criterios.Count + 2).Range.ParagraphFormat
    '            .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With

    '        'Detalle Notas
    '        Dim int_CodigoCurso, int_CodigoCriterio, int_Filas, int_Columnas As Integer
    '        Dim str_DescCurso As String = ""

    '        int_Filas = 0
    '        int_Columnas = 0

    '        While int_Filas <= dv_Cursos.Count - 1 ' Filas Cursos
    '            int_CodigoCurso = dv_Cursos(int_Filas).Item("CodigoCurso").ToString
    '            str_DescCurso = dv_Cursos(int_Filas).Item("DescNombreCurso").ToString
    '            oTablaNotas.Cell(int_Filas + 2, 1).Range.Text = str_DescCurso

    '            oTablaNotas.Cell(int_Filas + 2, 1).Select()
    '            sel = oWord.Selection
    '            sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
    '            sel.Rows.Height = oWord.CentimetersToPoints(0.5)
    '            sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

    '            With oTablaNotas.Cell(int_Filas + 2, 1).Range.ParagraphFormat
    '                .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '                .LeftIndent = oWord.CentimetersToPoints(0.2)
    '                .RightIndent = oWord.CentimetersToPoints(0)
    '                .SpaceBefore = 1
    '                .SpaceBeforeAuto = False
    '                .SpaceAfter = 1
    '                .SpaceAfterAuto = False
    '                .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '            End With

    '            While int_Columnas <= dv_Criterios.Count - 1 ' Columnas Criterios
    '                int_CodigoCriterio = dv_Criterios(int_Columnas).Item("CodigoCriterio")
    '                oTablaNotas.Cell(int_Filas + 2, int_Columnas + 2).Range.Text = obtenerNota(dv_Notas, int_CodigoCurso, int_CodigoCriterio)
    '                With oTablaNotas.Cell(int_Filas + 2, int_Columnas + 2).Range.ParagraphFormat
    '                    .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '                    .SpaceBefore = 1
    '                    .SpaceBeforeAuto = False
    '                    .SpaceAfter = 1
    '                    .SpaceAfterAuto = False
    '                    .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '                End With
    '                int_Columnas += 1
    '            End While

    '            str_DescCurso = ""
    '            int_CodigoCriterio = 0
    '            int_Columnas = 0
    '            int_Filas += 1
    '        End While

    '        oPara3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '        oPara3.Range.Text = ""
    '        oPara3.Range.InsertParagraphAfter()

    '        ' Tabla Criterios y Calificativos
    '        oTablaCriteriosYCalificativos = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 2, dv_Criterios.Count)
    '        oTablaCriteriosYCalificativos.Range.Font.Name = "Arial"
    '        oTablaCriteriosYCalificativos.Range.Font.Size = 10
    '        oTablaCriteriosYCalificativos.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '        oTablaCriteriosYCalificativos.Borders.Enable = False

    '        int_CodigoCriterio = 0

    '        ' Estructura y Cabecera de la Tabla Criterios y Calificativos
    '        For i As Integer = 0 To dv_Criterios.Count - 1
    '            oTablaCriteriosYCalificativos.Columns(i + 1).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '            oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.Text = dv_Criterios(i).Item("Criterio")
    '            oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '            oTablaCriteriosYCalificativos.Cell(1, i + 1).Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle

    '            int_CodigoCriterio = dv_Criterios(i).Item("CodigoCriterio")

    '            Dim dtAux As New DataTable
    '            dtAux = dv_CriteriosYCalificativos.ToTable

    '            Dim dv As DataView = dtAux.DefaultView
    '            With dv
    '                .RowFilter = "1=1 and CodigoCriterio = '" & int_CodigoCriterio & "' and CodigoAlumno = '" & str_CodigoAlumno & "'"
    '                .Sort = "OrdenCalificativo ASC"
    '            End With

    '            oTablaCriteriosYCalificativos.Cell(2, i + 1).Select()
    '            sel = oWord.Selection

    '            With sel.Range.ParagraphFormat
    '                .LeftIndent = oWord.CentimetersToPoints(0)
    '                .RightIndent = oWord.CentimetersToPoints(1)
    '                .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
    '                .SpaceBefore = 1
    '                .SpaceBeforeAuto = False
    '                .SpaceAfter = 1
    '                .SpaceAfterAuto = False
    '                .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '            End With

    '            sel.Range.Font.Name = "Arial"
    '            sel.Range.Font.Size = 8
    '            sel.Range.Font.Bold = False

    '            For j As Integer = 0 To dv.Count - 1
    '                sel.TypeText(dv(j).Item("Nota") & " - " & dv(j).Item("Calificativo") & " : " & dv(j).Item("LeyendaIngles") & " (" & dv(j).Item("LeyendaEspaniol") & ")")
    '                sel.TypeParagraph()
    '                If j < dv.Count - 1 Then
    '                    sel.TypeParagraph()
    '                End If
    '            Next

    '            int_CodigoCriterio = 0
    '        Next

    '        oTablaCriteriosYCalificativos.Select()
    '        sel = oWord.Selection
    '        sel.Find.ClearFormatting()

    '        Dim dtAux2 As New DataTable
    '        dtAux2 = dv_CriteriosYCalificativos.ToTable

    '        For i As Integer = 0 To dtAux2.Rows.Count - 1
    '            With sel.Find
    '                .Text = dtAux2.Rows(i).Item("LeyendaEspaniol").ToString
    '                .Replacement.Text = ""
    '                .Forward = True
    '                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
    '                .Format = False
    '                .MatchCase = False
    '                .MatchWholeWord = False
    '                .MatchWildcards = False
    '                .MatchSoundsLike = False
    '                .MatchAllWordForms = False
    '            End With
    '            sel.Find.Execute()
    '            sel.Font.Bold = True
    '        Next

    '        Dim str_Firma As String = ""
    '        For i As Integer = 0 To 39
    '            str_Firma = str_Firma + "_"
    '        Next

    '        oPara4 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '        oPara4.Range.Text = ""
    '        oPara4.Range.InsertParagraphAfter()

    '        ' Tabla de Firma del tutor
    '        oTablaFirma = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 3, 2)
    '        oTablaFirma.Range.Font.Name = "Arial"
    '        oTablaFirma.Range.Font.Size = 10
    '        oTablaFirma.Range.Font.Bold = False
    '        oTablaFirma.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '        oTablaFirma.Borders.Enable = False

    '        oTablaFirma.Columns(1).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '        oTablaFirma.Columns(2).SetWidth(250, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

    '        oTablaFirma.Cell(1, 2).Range.Text = str_Firma
    '        oTablaFirma.Cell(2, 2).Range.Text = "Nombre del Tutor"
    '        oTablaFirma.Cell(3, 2).Range.Text = "(Tutor)"

    '        For i As Integer = 1 To 3
    '            oTablaFirma.Cell(i, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '        Next

    '        If cont < dt_Alumnos.Rows.Count - 1 Then
    '            oParaPageBreak = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '            oParaPageBreak.Range.Select()
    '            sel = oWord.Selection
    '            sel.Range.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdSectionBreakNextPage)
    '        End If

    '    Next

    '    ' Grabar el reporte Word()
    '    Dim sTempFolderPath As String = System.IO.Path.GetTempPath()
    '    Dim str_RutaGuardar As String = ""
    '    Dim str_nombreDoc As String = ""

    '    str_nombreDoc = "MidTermReport_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"
    '    str_RutaGuardar = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & str_nombreDoc
    '    oDoc.SaveAs(str_RutaGuardar)
    '    oDoc.Close()

    '    'Quit Word and thoroughly deallocate everything
    '    oWord.Quit()
    '    System.GC.Collect()

    '    Return str_nombreDoc

    'End Function

    'Public Shared Function obtenerNota(ByVal dv_Notas As DataView, ByVal int_CodigoCurso As Integer, ByVal int_CodigoCriterio As Integer) As String

    'Dim str_Nota As String = ""

    '    For Each drv As DataRowView In dv_Notas
    '        If drv.Item("CodigoCurso") = int_CodigoCurso And drv.Item("CodigoCriterio") = int_CodigoCriterio Then
    '            str_Nota = drv.Item("Nota")
    '            Exit For
    '        End If
    '    Next

    '    Return str_Nota

    'End Function
#End Region

    ''' <summary>
    ''' Lista los de alumnos por el codigo de familia      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     16/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub AlumnosPorCodigoFamilia()

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva
        Dim ds_Lista As New DataSet

        If int_CodigoTipoUsuario = 1 Then ' Alumnos

            ds_Lista = obj_BL_Alumnos.FUN_GET_AlumnosPorCodigoAlumno(int_CodigoFamilia, int_CodigoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 65)
            'ddl_Alumno.Enabled = False

        ElseIf int_CodigoTipoUsuario = 3 Then ' Familiares

            ds_Lista = obj_BL_Alumnos.FUN_LIS_AlumnosPorCodigoFamilia(int_CodigoFamilia, int_CodigoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 8)
            'ddl_Alumno.Enabled = True

        End If

        dl_DatosAlumno.DataSource = ds_Lista.Tables(1)
        dl_DatosAlumno.DataBind()
        ViewState("ListaDatosAlumno") = ds_Lista.Tables(1)

        If ds_Lista.Tables(1).Rows.Count > 0 Then

            img_Foto.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & ds_Lista.Tables(1).Rows(0).Item("RutaFoto").ToString
            lblCodigoAlumno.Text = ds_Lista.Tables(1).Rows(0).Item("CodigoAlumno")
            lblNombre.Text = ds_Lista.Tables(1).Rows(0).Item("NombreCompleto")
            lblGrado.Text = ds_Lista.Tables(1).Rows(0).Item("GradoAcad")
            lblSeccion.Text = ds_Lista.Tables(1).Rows(0).Item("AulaAcad")
            lblCodigoGrado.Text = ds_Lista.Tables(1).Rows(0).Item("CodigoGrado")
            lblCodigoSeccion.Text = ds_Lista.Tables(1).Rows(0).Item("CodigoAula")
            listar()

            Dim btn_Contenedor As ImageButton = dl_DatosAlumno.Items(0).FindControl("btnVer_dl")
            Dim table_Contenedor As HtmlTable

            table_Contenedor = CType(btn_Contenedor.Parent.Parent.Parent, HtmlTable)
            table_Contenedor.Style.Value = "background-color:#215386;"

            If int_CodigoTipoUsuario = 1 Then

                obtenerDatos(int_CodigoUsuario)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Lista los de alumnos por el codigo de familia      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     16/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtenerDatos(ByVal codigo As Integer)
        Dim dt As DataTable
        dt = ViewState("ListaDatosAlumno")

        Dim dv As DataView
        dv = dt.DefaultView
        dv.RowFilter = "1=1 and CodigoAlumno =" & codigo.ToString

        img_Foto.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & dv.Item(0).Item("RutaFoto").ToString
        lblCodigoAlumno.Text = dv.Item(0).Item("CodigoAlumno")
        lblNombre.Text = dv.Item(0).Item("NombreCompleto")
        lblGrado.Text = dv.Item(0).Item("GradoAcad")
        lblSeccion.Text = dv.Item(0).Item("AulaAcad")
        lblCodigoGrado.Text = dv.Item(0).Item("CodigoGrado")
        lblCodigoSeccion.Text = dv.Item(0).Item("CodigoAula")
        listar()

    End Sub

    ''' <summary>
    ''' Lista los datos      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     06/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listar()
        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)
        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
        ImagenSorting(ViewState("SortExpression"))
    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet
        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar '1 
        Dim str_CodigoAlumno As String = lblCodigoAlumno.Text '"20110203"
        'Dim int_CodigoGrado As Integer = CInt(lblCodigoGrado.Text)
        'Dim int_CodigoSeccion As Integer = CInt(lblCodigoSeccion.Text)

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_ProgramacionMidTermReport As New bl_ProgramacionMidTermReport
            ds_Lista = obj_BL_ProgramacionMidTermReport.FUN_LIS_MidTermFamilia(int_CodigoAnioAcademico, str_CodigoAlumno, _
                                                                               int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_ProgramacionMidTermReport As New bl_ProgramacionMidTermReport
                ds_Lista = obj_BL_ProgramacionMidTermReport.FUN_LIS_MidTermFamilia( _
         int_CodigoAnioAcademico, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
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
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(0, 8, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_mensaje As String, ByVal str_tipoMensaje As String)
        Dim SexyAlertScript As String = ""
        Select Case str_tipoMensaje
            Case "Alert"
                SexyAlertScript = "Sexy.alert('" & str_mensaje & "');"
            Case "Info"
                SexyAlertScript = "Sexy.info('" & str_mensaje & "');"
            Case "Error"
                SexyAlertScript = "Sexy.error('" & str_mensaje & "');"
        End Select
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", SexyAlertScript, True)
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
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
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
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
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
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
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
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
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

    '''' <summary>
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        If GridView1.Rows.Count > 0 Then
            Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
            Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Mes"), ImageButton)

            If _btnSorting.ID = _btnSorting_d1.ID Then
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

        End If


    End Sub

#End Region

    Protected Sub MostrarAlertBox(ByVal str_Mensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, "")

    End Sub

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

        'Try
        If e.CommandName = "Visualizar" Then

            Dim codigo As Integer = CInt(e.CommandArgument.ToString)
            Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
            Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
            Dim int_CodigoProgramacion As Integer = row.Cells(0).Text 'CInt(row.Cells("lblCodigoBimestre").Text)

            If e.CommandName = "Visualizar" Then

                exportar(int_CodigoProgramacion)
                UpdatePanel1.Update()

            End If

        End If
        'Catch ex As Exception
        'EnvioEmailError(int_CodigoAccion, ex.ToString)
        'End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim btnVer As ImageButton = e.Row.FindControl("btnVer")

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = GridView1.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            btnVer.Attributes.Add("OnClick", " ShowMyModalPopup()")

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

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            Dim btnVer As ImageButton = e.Row.FindControl("btnVer")
            ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(btnVer)

        End If

    End Sub

#End Region

End Class
