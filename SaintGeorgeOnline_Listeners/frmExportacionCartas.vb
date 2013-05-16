Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_Utilities

Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Runtime.InteropServices.Marshal
Imports System.Threading

Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports Ionic.Zip

Public Class frmExportacionCartas

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1
    Private int_CodigoUsuario As Integer = 1
    Private int_CodigoTipoUsuario As Integer = 1

#Region "Evitar múltiples instancias"

    Private Shared Instancia As frmExportacionCartas = Nothing

    Public Shared Function Instance() As frmExportacionCartas
        If Instancia Is Nothing OrElse Instancia.IsDisposed = True Then
            Instancia = New frmExportacionCartas
        End If
        Instancia.BringToFront()
        Return Instancia
    End Function

#End Region

#Region "Atributos"

    Private HiloExportar As Thread

#End Region

#Region "Eventos"

    Private Sub frmExportacionCartas_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim str_Error As String = ""
        Try

            cargarPagina()
            CheckForIllegalCrossThreadCalls = False

        Catch ex As Exception
            str_Error = ex.Message
            MsgBox("Error : " & str_Error)
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstCartas.SelectedIndexChanged

        If lstCartas.SelectedIndex = 0 Then
            Panel1.Visible = False
            Panel2.Visible = False
        ElseIf lstCartas.SelectedIndex = 1 Then
            Panel1.Visible = False
            Panel2.Visible = False
        ElseIf lstCartas.SelectedIndex = 2 Then
            Panel1.Visible = True
            Panel2.Visible = False
            Panel1.Top = 125
            Panel1.Left = 0
        ElseIf lstCartas.SelectedIndex = 3 Then
            Panel1.Visible = False
            Panel2.Visible = True
            Panel2.Top = 125
            Panel2.Left = 0
        End If

    End Sub

    Private Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
      
        PanelExportar.Visible = True
        lblMensajeExportacion.Text = "Mensaje: Generando archivo"

        HiloExportar = New Thread(AddressOf exportar)
        HiloExportar.Start()

    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarPagina()

        cargarListaTipoCartas()
        cargarComboAniosAcademicos()
        rbFormatoFamilia.Checked = True

        dtpFechaVcto.Value = Today.ToShortDateString
        cboAnioAcademico.SelectedValue = 2
        lstCartas.SelectedIndex = 0

        'cargarComboCantidadDeudas()

        cargarComboCantidadDeudasCitacion()
        cargarComboCantidadDeudasSuspension()

        cargarComboGradosIni()
        cargarComboGradosFin()

        lblRuta.Text = ""

        Panel1.Visible = False
        Panel2.Visible = False

        Me.PanelExportar.Visible = False

    End Sub

    Private Sub cargarListaTipoCartas()

        lstCartas.Items.Clear()
        lstCartas.Items.Add("Cartas por Morosidad(mes)")
        lstCartas.Items.Add("Cartas por Morosidad(2 meses)")
        lstCartas.Items.Add("Cartas de Citación")
        lstCartas.Items.Add("Cartas de Suspención")

    End Sub

    Private Sub cargarComboAniosAcademicos()

        cboAnioAcademico.Items.Clear()
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(cboAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    Private Sub cargarComboGradosIni()

        cboGradoFin.Items.Clear()
        Dim obj_Grados As New bl_Grados
        Dim ds_Lista As DataSet = obj_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(cboGradoIni, ds_Lista, "Codigo", "DescripcionEspaniol", False, False)

    End Sub

    Private Sub cargarComboGradosFin()

        cboGradoFin.Items.Clear()
        Dim obj_Grados As New bl_Grados
        Dim ds_Lista As DataSet = obj_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(cboGradoFin, ds_Lista, "Codigo", "DescripcionEspaniol", False, False)

    End Sub

    Private Sub cargarComboCantidadDeudasCitacion()

        Dim dt As New DataTable
        Dim ds As New DataSet
        dt = Datos.agregarColumna(dt, "Codigo", "integer")
        dt = Datos.agregarColumna(dt, "Descripcion", "string")

        Dim dr1 As DataRow
        Dim dr2 As DataRow
        Dim dr3 As DataRow
        Dim dr4 As DataRow

        dr1 = dt.NewRow
        dr1.Item("Codigo") = 1
        dr1.Item("Descripcion") = "1"

        dr2 = dt.NewRow
        dr2.Item("Codigo") = 2
        dr2.Item("Descripcion") = "2"

        dr3 = dt.NewRow
        dr3.Item("Codigo") = 3
        dr3.Item("Descripcion") = "3"

        dr4 = dt.NewRow
        dr4.Item("Codigo") = 4
        dr4.Item("Descripcion") = "4 a más"

        dt.Rows.Add(dr1)
        dt.Rows.Add(dr2)
        dt.Rows.Add(dr3)
        dt.Rows.Add(dr4)

        ds.Tables.Add(dt)

        cboCantDeudas1.Items.Clear()
        Controles.llenarCombo(cboCantDeudas1, ds, "Codigo", "Descripcion", False, False)

    End Sub

    Private Sub cargarComboCantidadDeudasSuspension()

        Dim dt As New DataTable
        Dim ds As New DataSet
        dt = Datos.agregarColumna(dt, "Codigo", "integer")
        dt = Datos.agregarColumna(dt, "Descripcion", "string")

        Dim dr1 As DataRow
        Dim dr2 As DataRow
        Dim dr3 As DataRow
        Dim dr4 As DataRow

        dr1 = dt.NewRow
        dr1.Item("Codigo") = 1
        dr1.Item("Descripcion") = "1"

        dr2 = dt.NewRow
        dr2.Item("Codigo") = 2
        dr2.Item("Descripcion") = "2"

        dr3 = dt.NewRow
        dr3.Item("Codigo") = 3
        dr3.Item("Descripcion") = "3"

        dr4 = dt.NewRow
        dr4.Item("Codigo") = 4
        dr4.Item("Descripcion") = "4 a más"

        dt.Rows.Add(dr1)
        dt.Rows.Add(dr2)
        dt.Rows.Add(dr3)
        dt.Rows.Add(dr4)

        ds.Tables.Add(dt)

        cboCantDeudas2.Items.Clear()
        Controles.llenarCombo(cboCantDeudas2, ds, "Codigo", "Descripcion", False, False)

    End Sub

    Private Sub exportar()

        Dim int_CodigoAnioAcademico As Integer = cboAnioAcademico.SelectedValue()
        Dim int_CodigoFamilia As Integer = 0
        Dim int_CodigoAlumno As Integer = 0

        Dim dt_FechaVcto As String = dtpFechaVcto.Value

        Dim int_TipoReporte As Integer = lstCartas.SelectedValue()
        Dim int_TipoFormato As Integer = 0 'rbTipoFormato.SelectedValue()

        Dim int_CantidadDeudasCitacion As Integer = cboCantDeudas1.SelectedValue
        Dim int_CantidadDeudasSuspension As Integer = cboCantDeudas2.SelectedValue

        Dim ds_Lista As New DataSet
        Dim obj_BL_ReportesWord As New bl_ReportesWord

        Dim str_NombreRepExportar As String = ""
        Dim str_TipoDocumento As String = ""

        Dim int_CodigoGradoIni As Integer = cboGradoIni.SelectedValue
        Dim int_CodigoGradoFin As Integer = cboGradoFin.SelectedValue


        If lstCartas.SelectedIndex = 0 Then      'CARTA DE MOROSIDAD - 1 MES

            ds_Lista = obj_BL_ReportesWord.FUN_LIS_ReportesWordPorGrados( _
                int_CodigoAnioAcademico, int_CodigoAlumno, int_CodigoFamilia, int_CodigoGradoIni, int_CodigoGradoFin, _
                dt_FechaVcto, int_TipoReporte, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 53)

            If ds_Lista.Tables(0).Rows.Count > 0 Then
                str_NombreRepExportar = GenerarEsquela_Formato1(ds_Lista)
                Process.Start("explorer.exe", str_NombreRepExportar & "\")
                lblMensajeExportacion.Text = "Mensaje: Operación terminada."
            Else
                lblMensajeExportacion.Text = "No se encontraron registros."
            End If


        ElseIf lstCartas.SelectedIndex = 1 Then  'CARTA DE MOROSIDAD - 2 MESES

            ds_Lista = obj_BL_ReportesWord.FUN_LIS_ReportesWordPorGrados( _
            int_CodigoAnioAcademico, int_CodigoAlumno, int_CodigoFamilia, int_CodigoGradoIni, int_CodigoGradoFin, _
            dt_FechaVcto, int_TipoReporte, 2, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 53)


            If ds_Lista.Tables(0).Rows.Count > 0 Then
                str_NombreRepExportar = GenerarEsquela_Formato2(ds_Lista)
                Process.Start("explorer.exe", str_NombreRepExportar & "\")
                lblMensajeExportacion.Text = "Mensaje: Operación terminada."
            Else
                lblMensajeExportacion.Text = "No se encontraron registros."
            End If

        ElseIf lstCartas.SelectedIndex = 2 Then  'CARTA DE CITACIÓN

            ds_Lista = obj_BL_ReportesWord.FUN_LIS_ReportesWordPorGrados( _
            int_CodigoAnioAcademico, int_CodigoAlumno, int_CodigoFamilia, int_CodigoGradoIni, int_CodigoGradoFin, _
            dt_FechaVcto, int_TipoReporte, int_CantidadDeudasCitacion, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 53)

            If ds_Lista.Tables(0).Rows.Count > 0 Then
                Dim str_FechaCitacion As Date = dtpFechaCitacion.Text.Trim
                Dim str_HoraInicio As String = dtpHoraCitacion1.Value.Hour.ToString & ":" & dtpHoraCitacion1.Value.Minute
                Dim str_HoraFin As String = dtpHoraCitacion2.Value.Hour.ToString & ":" & dtpHoraCitacion2.Value.Minute
                str_NombreRepExportar = GenerarCarta_Citacion(ds_Lista, str_FechaCitacion, str_HoraInicio, str_HoraFin)
                Process.Start("explorer.exe", str_NombreRepExportar & "\")
                lblMensajeExportacion.Text = "Mensaje: Operación terminada."
            Else
                lblMensajeExportacion.Text = "No se encontraron registros."
            End If

        ElseIf lstCartas.SelectedIndex = 3 Then  'CARTA DE SUSPENSIÓN

            Dim str_FechaSuspension As Date = dtpFechaSuspension.Text.Trim
            ds_Lista = obj_BL_ReportesWord.FUN_LIS_ReportesWordPorGrados( _
            int_CodigoAnioAcademico, int_CodigoAlumno, int_CodigoFamilia, int_CodigoGradoIni, int_CodigoGradoFin, _
            dt_FechaVcto, int_TipoReporte, int_CantidadDeudasSuspension, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 53)

            If ds_Lista.Tables(0).Rows.Count > 0 Then
                str_NombreRepExportar = GenerarCarta_Suspension(ds_Lista, str_FechaSuspension)
                Process.Start("explorer.exe", str_NombreRepExportar & "\")
                lblMensajeExportacion.Text = "Mensaje: Operación terminada."
            Else
                lblMensajeExportacion.Text = "No se encontraron registros."
            End If

        End If


        Dim dt_Familias As New DataTable
        dt_Familias = Datos.agregarColumna(dt_Familias, "N°", "String")
        dt_Familias = Datos.agregarColumna(dt_Familias, "Código", "string")
        dt_Familias = Datos.agregarColumna(dt_Familias, "Familia", "string")

        Dim drF As DataRow
        Dim cont As Integer = 1

        For Each dr As DataRow In ds_Lista.Tables(1).Rows
            drF = dt_Familias.NewRow
            drF.Item("N°") = cont
            drF.Item("Código") = dr.Item("CodigoFamilia")
            drF.Item("Familia") = dr.Item("NombreFamilia")
            dt_Familias.Rows.Add(drF)
            cont += 1
        Next

        'Lista de Familias
        Dim rutamadreFamilias As String = ""
        Dim contenido_exportar As String = ""
        Dim NombreArchivoFamilias As String = ""

        NombreArchivoFamilias = ExportarReporteSinFormato(dt_Familias, "Familias", "ListadoFamilias")
        'NombreArchivoFamilias = NombreArchivoFamilias & ".xls"
        Process.Start("explorer.exe", NombreArchivoFamilias & "\")

        Me.PanelExportar.Visible = False

    End Sub

    Private Function GenerarEsquela_Formato1(ByVal ds_Data As DataSet) As String

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing
        Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTable As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTableCronograma As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oRng As Microsoft.Office.Interop.Word.Range = Nothing
        Dim oShape As Microsoft.Office.Interop.Word.InlineShape = Nothing
        Dim oPara1 As Microsoft.Office.Interop.Word.Paragraph, oPara2 As Microsoft.Office.Interop.Word.Paragraph, oPara3, oPara4, oPara5, oPara6, oPara7, oPara8, oPara9, oPara10, oPara11, oPara12, oPara13, oPara14, oPara15 As Microsoft.Office.Interop.Word.Paragraph
        Dim oChart As Object = Nothing
        Dim Pos As Double = 0
        Dim numAlum As String = ""

        Dim dt_Consolidado As DataTable
        Dim dt_Familias As DataTable
        Dim dv_Alumnos As DataView
        Dim cont As Integer = 0
        Dim cont_Hermanos As Integer = 0

        dt_Consolidado = ds_Data.Tables(0)
        dt_Familias = ds_Data.Tables(1)

        dv_Alumnos = dt_Consolidado.DefaultView

        For i As Integer = 0 To dt_Familias.Rows.Count - 1

            If i = 0 Then

                Dim rutamadre As String = Application.StartupPath()
                'rutamadre = Replace(rutamadre, "bin\Debug", "plantillas")
                Dim saArchivo As String = ""
                saArchivo = rutamadre.ToString & System.Configuration.ConfigurationSettings.AppSettings("RutaPlantillaEsquelas1").ToString

                oWord = New Microsoft.Office.Interop.Word.Application
                oWord.Visible = False
                oDoc = oWord.Documents.Add(saArchivo)
                oDoc.Content.Copy()

            End If

            If i > dt_Familias.Rows.Count - 1 Then
                Exit For
            End If

            If dt_Familias.Rows(i).Item("Exportar") = 1 Then

                dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i).Item("CodigoFamilia")

                'COLUMNA "A"
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                oPara1.Range.Font.Name = "Arial Narrow"
                oPara1.Format.SpaceAfter = 6
                oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                oPara1.Range.Font.Bold = True
                oPara1.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i).Item("NombreFamilia").ToString
                oPara2.Range.Font.Name = "Arial Narrow"
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oWord.Selection.Find.ClearFormatting()
                With oWord.Selection.Find
                    .Text = dt_Familias.Rows(i).Item("NombreFamilia").ToString
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
                oWord.Selection.Find.Execute()
                oWord.Selection.Font.Bold = True

                cont = 0
                cont_Hermanos = dv_Alumnos.Count

                oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                oTable.Range.ParagraphFormat.SpaceAfter = 3
                oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                oTable.Cell(1, 2).Range.Text = "Armada que adeuda:"
                oTable.Cell(1, 1).Range.Font.Name = "Arial Narrow"
                oTable.Cell(1, 2).Range.Font.Name = "Arial Narrow"

                While cont <= dv_Alumnos.Count - 1

                    oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                    oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                    oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                    oTable.Cell(cont + 2, 2).Range.Font.Bold = True
                    cont = cont + 1
                End While

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = " "
                oPara5.Range.Font.Bold = False
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

                oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente el pago de la armada arriba mencionada."
                oPara6.Range.Font.Name = "Arial Narrow"
                oPara6.Range.Font.Bold = False
                oPara6.Format.SpaceAfter = 6
                oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara6.Range.InsertParagraphAfter()

                oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara12.Range.Text = " "
                oPara12.Format.SpaceAfter = 6
                oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara12.Range.InsertParagraphAfter()

                oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara7.Range.Text = "Sírvase cancelar la deuda pendiente en las oficinas del Banco Interbank ó del Banco de Crédito, ya que la morosidad de las armadas afecta el normal desenvolvimiento de nuestra institución."
                oPara7.Range.Font.Name = "Arial Narrow"
                oPara7.Format.SpaceAfter = 6
                oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara7.Range.InsertParagraphAfter()

                oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara13.Range.Text = " "
                oPara13.Format.SpaceAfter = 6
                oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara13.Range.InsertParagraphAfter()

                oPara8 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara8.Range.Text = "Si se encuentra al día en sus pagos, ignore esta deuda que figura como pendiente en el sistema hasta el día de hoy."
                oPara8.Range.Font.Name = "Arial Narrow"
                oPara8.Format.SpaceAfter = 6
                oPara8.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara8.Range.InsertParagraphAfter()

                oPara14 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara14.Range.Text = " "
                oPara14.Format.SpaceAfter = 6
                oPara14.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara14.Range.InsertParagraphAfter()

                oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara9.Range.Text = "Atentamente."
                oPara9.Range.Font.Name = "Arial Narrow"
                oPara9.Format.SpaceAfter = 6
                oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara9.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = " "
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara10.Range.Text = "Janeth Velasco"
                oPara10.Range.Font.Name = "Arial Narrow"
                oPara10.Format.SpaceAfter = 6
                oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara10.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = "Tesorería"
                oPara11.Range.Font.Name = "Arial Narrow"
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                If cont_Hermanos = 1 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()

                    oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara15.Range.Text = " "
                    oPara15.Format.SpaceAfter = 6
                    oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara15.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 2 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 3 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()
                End If

                If i + 1 <= dt_Familias.Rows.Count - 1 Then

                    If dt_Familias.Rows(i + 1).Item("Exportar") = 1 Then

                        dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i + 1).Item("CodigoFamilia")

                        'COLUMNA "B"
                        oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                        oPara1.Range.Font.Bold = True
                        oPara1.Format.SpaceAfter = 6
                        oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                        oPara1.Range.InsertParagraphAfter()

                        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
                        oPara2.Range.Font.Bold = False
                        oPara2.Format.SpaceAfter = 6
                        oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara2.Range.InsertParagraphAfter()

                        oWord.Selection.Find.ClearFormatting()
                        With oWord.Selection.Find
                            .Text = dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
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
                        oWord.Selection.Find.Execute()
                        oWord.Selection.Font.Bold = True

                        cont = 0
                        cont_Hermanos = dv_Alumnos.Count

                        oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                        oTable.Range.ParagraphFormat.SpaceAfter = 3
                        oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                        oTable.Cell(1, 1).Range.Font.Bold = False
                        oTable.Cell(1, 2).Range.Text = "Armada que adeuda:"
                        oTable.Cell(1, 2).Range.Font.Bold = False

                        While cont <= dv_Alumnos.Count - 1

                            oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                            oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                            oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                            oTable.Cell(cont + 2, 2).Range.Font.Bold = True
                            cont = cont + 1
                        End While

                        oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara5.Range.Text = " "
                        oPara5.Range.Font.Bold = False
                        oPara5.Format.SpaceAfter = 6
                        oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara5.Range.InsertParagraphAfter()

                        oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente el pago de la armada arriba mencionada."
                        oPara6.Range.Font.Bold = False
                        oPara6.Format.SpaceAfter = 6
                        oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara6.Range.InsertParagraphAfter()

                        oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara12.Range.Text = " "
                        oPara12.Range.Font.Bold = False
                        oPara12.Format.SpaceAfter = 6
                        oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara12.Range.InsertParagraphAfter()

                        oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara7.Range.Text = "Sírvase cancelar la deuda pendiente en las oficinas del Banco Interbank ó del Banco de Crédito, ya que la morosidad de las armadas afecta el normal desenvolvimiento de nuestra institución."
                        oPara7.Range.Font.Bold = False
                        oPara7.Format.SpaceAfter = 6
                        oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara7.Range.InsertParagraphAfter()

                        oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara13.Range.Text = " "
                        oPara13.Range.Font.Bold = False
                        oPara13.Format.SpaceAfter = 6
                        oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara13.Range.InsertParagraphAfter()

                        oPara8 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara8.Range.Text = "Si se encuentra al día en sus pagos, ignore esta deuda que figura como pendiente en el sistema hasta el día de hoy."
                        oPara8.Range.Font.Bold = False
                        oPara8.Format.SpaceAfter = 6
                        oPara8.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara8.Range.InsertParagraphAfter()

                        oPara14 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara14.Range.Text = " "
                        oPara14.Range.Font.Bold = False
                        oPara14.Format.SpaceAfter = 6
                        oPara14.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara14.Range.InsertParagraphAfter()

                        oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara9.Range.Text = "Atentamente."
                        oPara9.Range.Font.Bold = False
                        oPara9.Format.SpaceAfter = 6
                        oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara9.Range.InsertParagraphAfter()

                        oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara15.Range.Text = " "
                        oPara15.Range.Font.Bold = False
                        oPara15.Format.SpaceAfter = 6
                        oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara15.Range.InsertParagraphAfter()

                        oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara10.Range.Font.Bold = False
                        oPara10.Range.Text = "Janeth Velasco"
                        oPara10.Format.SpaceAfter = 6
                        oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara10.Range.InsertParagraphAfter()

                        oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara11.Range.Font.Bold = False
                        oPara11.Range.Text = "Tesorería"
                        oPara11.Format.SpaceAfter = 6
                        oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara11.Range.InsertParagraphAfter()

                        If cont_Hermanos = 1 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()

                            oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara15.Range.Text = " "
                            oPara15.Format.SpaceAfter = 6
                            oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara15.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 2 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 3 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()
                        End If

                    End If

                End If

            End If

            cont = 0
            i = i + 1
        Next

        Dim str_nombreDoc As String = ""
        str_nombreDoc = "Esquela1_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"

        Dim str_RutaGuardar As String = ""
        str_RutaGuardar = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & str_nombreDoc

        oDoc.SaveAs(str_RutaGuardar)
        oDoc.Close()
        oWord.Quit()
        System.GC.Collect()

        Return str_nombreDoc

    End Function

    Private Function GenerarEsquela_Formato2(ByVal ds_Data As DataSet) As String

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing
        Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTable As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTableCronograma As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oRng As Microsoft.Office.Interop.Word.Range = Nothing
        Dim oShape As Microsoft.Office.Interop.Word.InlineShape = Nothing
        Dim oPara1 As Microsoft.Office.Interop.Word.Paragraph, oPara2 As Microsoft.Office.Interop.Word.Paragraph, oPara3, oPara4, oPara5, oPara6, oPara7, oPara8, oPara9, oPara10, oPara11, oPara12, oPara13, oPara14, oPara15, _
        oPara16, oPara17, oPara18, oPara19, oPara20, oPara21 As Microsoft.Office.Interop.Word.Paragraph
        Dim oChart As Object = Nothing
        Dim Pos As Double = 0
        Dim numAlum As String = ""

        Dim dt_Consolidado As DataTable
        Dim dt_Familias As DataTable
        Dim dv_Alumnos As DataView
        Dim cont As Integer = 0
        Dim cont_Hermanos As Integer = 0

        dt_Consolidado = ds_Data.Tables(0)
        dt_Familias = ds_Data.Tables(1)

        dv_Alumnos = dt_Consolidado.DefaultView

        For i As Integer = 0 To dt_Familias.Rows.Count - 1

            If i = 0 Then

                Dim rutamadre As String = Application.StartupPath()
                'rutamadre = Replace(rutamadre, "bin\Debug", "plantillas")
                Dim saArchivo As String = ""
                saArchivo = rutamadre.ToString & System.Configuration.ConfigurationSettings.AppSettings("RutaPlantillaEsquelas2").ToString

                oWord = New Microsoft.Office.Interop.Word.Application
                oWord.Visible = False
                oDoc = oWord.Documents.Add(saArchivo)
                oDoc.Content.Copy()

            End If

            If i > dt_Familias.Rows.Count - 1 Then
                Exit For
            End If

            If dt_Familias.Rows(i).Item("Exportar") = 1 Then

                dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i).Item("CodigoFamilia")

                'COLUMNA "A"
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                oPara1.Range.Font.Name = "Arial Narrow"
                oPara1.Format.SpaceAfter = 6
                oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                oPara1.Range.Font.Bold = True
                oPara1.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i).Item("NombreFamilia").ToString
                oPara2.Range.Font.Name = "Arial Narrow"
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oWord.Selection.Find.ClearFormatting()
                With oWord.Selection.Find
                    .Text = dt_Familias.Rows(i).Item("NombreFamilia").ToString
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
                oWord.Selection.Find.Execute()
                oWord.Selection.Font.Bold = True

                cont = 0
                cont_Hermanos = dv_Alumnos.Count

                oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                oTable.Range.ParagraphFormat.SpaceAfter = 3
                oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"
                oTable.Cell(1, 1).Range.Font.Name = "Arial Narrow"
                oTable.Cell(1, 2).Range.Font.Name = "Arial Narrow"

                While cont <= dv_Alumnos.Count - 1

                    oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                    oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                    oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                    oTable.Cell(cont + 2, 2).Range.Font.Bold = True

                    cont = cont + 1
                End While

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = " "
                oPara5.Range.Font.Bold = False
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

                oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente de pago las armadas arriba mencionadas."
                oPara6.Range.Font.Name = "Arial Narrow"
                oPara6.Range.Font.Bold = False
                oPara6.Format.SpaceAfter = 6
                oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara6.Range.InsertParagraphAfter()

                oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara12.Range.Text = " "
                oPara12.Format.SpaceAfter = 6
                oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara12.Range.InsertParagraphAfter()

                oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara7.Range.Text = "Sírvase cancelar la deuda pendiente en las oficinas del Banco Interbank ó del Banco de Crédito, ya que la morosidad de las armadas afecta el normal desenvolvimiento de nuestra institución."
                oPara7.Range.Font.Name = "Arial Narrow"
                oPara7.Format.SpaceAfter = 6
                oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara7.Range.InsertParagraphAfter()

                oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara13.Range.Text = " "
                oPara13.Format.SpaceAfter = 6
                oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara13.Range.InsertParagraphAfter()

                oPara8 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara8.Range.Text = "Si se encuentra al día en sus pagos, ignore esta deuda que figura como pendiente en el sistema hasta el día de hoy."
                oPara8.Range.Font.Name = "Arial Narrow"
                oPara8.Format.SpaceAfter = 6
                oPara8.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara8.Range.InsertParagraphAfter()

                oPara14 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara14.Range.Text = " "
                oPara14.Format.SpaceAfter = 6
                oPara14.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara14.Range.InsertParagraphAfter()

                oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara9.Range.Text = "Atentamente."
                oPara9.Range.Font.Name = "Arial Narrow"
                oPara9.Format.SpaceAfter = 6
                oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara9.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = " "
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara10.Range.Text = "Janeth Velasco"
                oPara10.Range.Font.Name = "Arial Narrow"
                oPara10.Format.SpaceAfter = 6
                oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara10.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = "Tesorería"
                oPara11.Range.Font.Name = "Arial Narrow"
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                oPara16 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara16.Range.Text = " "
                oPara16.Format.SpaceAfter = 6
                oPara16.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara16.Range.InsertParagraphAfter()

                oPara17 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara17.Range.Text = " "
                oPara17.Format.SpaceAfter = 6
                oPara17.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara17.Range.InsertParagraphAfter()

                'oPara18 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                'oPara18.Range.Text = " "
                'oPara18.Format.SpaceAfter = 6
                'oPara18.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                'oPara18.Range.InsertParagraphAfter()

                If cont_Hermanos = 1 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()

                    oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara15.Range.Text = " "
                    oPara15.Format.SpaceAfter = 6
                    oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara15.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 2 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 3 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()
                End If

                If i + 1 <= dt_Familias.Rows.Count - 1 Then

                    If dt_Familias.Rows(i + 1).Item("Exportar") = 1 Then

                        dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i + 1).Item("CodigoFamilia")

                        'COLUMNA "B"
                        oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                        oPara1.Format.SpaceAfter = 6
                        oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                        oPara1.Range.Font.Bold = True
                        oPara1.Range.InsertParagraphAfter()

                        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
                        oPara2.Range.Font.Bold = False
                        oPara2.Format.SpaceAfter = 6
                        oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara2.Range.InsertParagraphAfter()

                        oWord.Selection.Find.ClearFormatting()
                        With oWord.Selection.Find
                            .Text = dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
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
                        oWord.Selection.Find.Execute()
                        oWord.Selection.Font.Bold = True

                        cont = 0
                        cont_Hermanos = dv_Alumnos.Count

                        oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                        oTable.Range.ParagraphFormat.SpaceAfter = 3
                        oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                        oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"

                        While cont <= dv_Alumnos.Count - 1

                            oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                            oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                            oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                            oTable.Cell(cont + 2, 2).Range.Font.Bold = True
                            cont = cont + 1
                        End While

                        oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara5.Range.Text = " "
                        oPara5.Range.Font.Bold = False
                        oPara5.Format.SpaceAfter = 6
                        oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara5.Range.InsertParagraphAfter()

                        oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente de pago las armadas arriba mencionadas."
                        oPara6.Range.Font.Bold = False
                        oPara6.Format.SpaceAfter = 6
                        oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara6.Range.InsertParagraphAfter()

                        oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara12.Range.Text = " "
                        oPara12.Format.SpaceAfter = 6
                        oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara12.Range.InsertParagraphAfter()

                        oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara7.Range.Text = "Sírvase cancelar la deuda pendiente en las oficinas del Banco Interbank ó del Banco de Crédito, ya que la morosidad de las armadas afecta el normal desenvolvimiento de nuestra institución."
                        oPara7.Format.SpaceAfter = 6
                        oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara7.Range.InsertParagraphAfter()

                        oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara13.Range.Text = " "
                        oPara13.Format.SpaceAfter = 6
                        oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara13.Range.InsertParagraphAfter()

                        oPara8 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara8.Range.Text = "Si se encuentra al día en sus pagos, ignore esta deuda que figura como pendiente en el sistema hasta el día de hoy."
                        oPara8.Format.SpaceAfter = 6
                        oPara8.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara8.Range.InsertParagraphAfter()

                        oPara14 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara14.Range.Text = " "
                        oPara14.Format.SpaceAfter = 6
                        oPara14.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara14.Range.InsertParagraphAfter()

                        oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara9.Range.Text = "Atentamente."
                        oPara9.Format.SpaceAfter = 6
                        oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara9.Range.InsertParagraphAfter()

                        oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara15.Range.Text = " "
                        oPara15.Format.SpaceAfter = 6
                        oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara15.Range.InsertParagraphAfter()

                        oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara10.Range.Text = "Janeth Velasco"
                        oPara10.Format.SpaceAfter = 6
                        oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara10.Range.InsertParagraphAfter()

                        oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara11.Range.Text = "Tesorería"
                        oPara11.Format.SpaceAfter = 6
                        oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara11.Range.InsertParagraphAfter()

                        oPara19 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara19.Range.Text = " "
                        oPara19.Format.SpaceAfter = 6
                        oPara19.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara19.Range.InsertParagraphAfter()

                        oPara20 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara20.Range.Text = " "
                        oPara20.Format.SpaceAfter = 6
                        oPara20.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara20.Range.InsertParagraphAfter()

                        'oPara21 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        'oPara21.Range.Text = " "
                        'oPara21.Format.SpaceAfter = 6
                        'oPara21.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        'oPara21.Range.InsertParagraphAfter()

                        If cont_Hermanos = 1 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()

                            oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara15.Range.Text = " "
                            oPara15.Format.SpaceAfter = 6
                            oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara15.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 2 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 3 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()
                        End If

                    End If

                End If

            End If

            cont = 0
            i = i + 1
        Next

        Dim str_nombreDoc As String = ""
        str_nombreDoc = "Esquela2_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"

        Dim str_RutaGuardar As String = ""
        str_RutaGuardar = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & str_nombreDoc

        oDoc.SaveAs(str_RutaGuardar)
        oDoc.Close()
        oWord.Quit()
        System.GC.Collect()

        Return str_nombreDoc
    End Function

    Private Function GenerarCarta_Suspension(ByVal ds_Data As DataSet, ByVal str_FechaSuspension As Date) As String

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing
        Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTable As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTableCronograma As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oRng As Microsoft.Office.Interop.Word.Range = Nothing
        Dim oShape As Microsoft.Office.Interop.Word.InlineShape = Nothing
        Dim oPara1 As Microsoft.Office.Interop.Word.Paragraph, oPara2 As Microsoft.Office.Interop.Word.Paragraph, oPara3, oPara4, oPara5, oPara6, oPara7, oPara8, oPara9, oPara10, oPara11, oPara12, oPara13, oPara14, oPara15 As Microsoft.Office.Interop.Word.Paragraph
        Dim oChart As Object = Nothing
        Dim Pos As Double = 0
        Dim numAlum As String = ""

        Dim dt_Consolidado As DataTable
        Dim dt_Familias As DataTable
        Dim dt_DetalleDeudas As DataTable

        Dim dv_Alumnos As DataView
        Dim dv_DetalleDeudas As DataView
        Dim cont As Integer = 0
        Dim cont_Hermanos As Integer = 0

        dt_Consolidado = ds_Data.Tables(0)
        dt_Familias = ds_Data.Tables(1)
        dt_DetalleDeudas = ds_Data.Tables(2)

        dv_Alumnos = dt_Consolidado.DefaultView
        dv_DetalleDeudas = dt_DetalleDeudas.DefaultView

        For i As Integer = 0 To dt_Familias.Rows.Count - 1

            If i = 0 Then

                Dim rutamadre As String = Application.StartupPath()
                'rutamadre = Replace(rutamadre, "bin\Debug", "plantillas")
                Dim saArchivo As String = ""
                saArchivo = rutamadre.ToString & System.Configuration.ConfigurationSettings.AppSettings("RutaPlantillaSuspension").ToString

                oWord = New Microsoft.Office.Interop.Word.Application
                oWord.Visible = False
                oDoc = oWord.Documents.Add(saArchivo)
                oDoc.Content.Copy()

            End If

            If i > dt_Familias.Rows.Count - 1 Then
                Exit For
            End If

            If dt_Familias.Rows(i).Item("Exportar") = 1 Then

                dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i).Item("CodigoFamilia")
                dv_DetalleDeudas.RowFilter = "1=1 AND CodigoFamilia =" & dt_Familias.Rows(i).Item("CodigoFamilia")

                'COLUMNA "A"
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                oPara1.Range.Font.Name = "Arial Narrow"
                oPara1.Format.SpaceAfter = 6
                oPara1.Range.Font.Bold = True
                oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                oPara1.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = " "
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i).Item("NombreFamilia").ToString
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oWord.Selection.Find.ClearFormatting()
                With oWord.Selection.Find
                    .Text = dt_Familias.Rows(i).Item("NombreFamilia").ToString
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
                oWord.Selection.Find.Execute()
                oWord.Selection.Font.Bold = True

                cont = 0
                cont_Hermanos = dv_DetalleDeudas.Count

                oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                oTable.Range.ParagraphFormat.SpaceAfter = 3
                oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"
                oTable.Cell(1, 1).Range.Font.Name = "Arial Narrow"
                oTable.Cell(1, 2).Range.Font.Name = "Arial Narrow"

                Dim int_contDetalleDeuda As Integer = 0

                Dim str_ListaHermanos As String = ""
                Dim str_NombreAlumno As String = ""
                Dim int_NombreLength As Integer = 0
                Dim int_IndexNombreAlumno As Integer

                While cont <= dv_Alumnos.Count - 1

                    oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                    oTable.Cell(cont + 2, 1).Range.Font.Bold = True

                    dv_DetalleDeudas.RowFilter = "1=1 AND CodigoAlumno = " & dv_Alumnos.Item(cont).Item("CodigoAlumno").ToString

                    While int_contDetalleDeuda <= dv_DetalleDeudas.Count - 1

                        oTable.Cell(int_contDetalleDeuda + 2, 2).Range.Text = dv_DetalleDeudas.Item(int_contDetalleDeuda).Item("armada").ToString & _
                        " " & dv_DetalleDeudas.Item(int_contDetalleDeuda).Item("FechaVencimiento").ToString
                        oTable.Cell(int_contDetalleDeuda + 2, 2).Range.Font.Bold = True

                        int_contDetalleDeuda = int_contDetalleDeuda + 1
                    End While

                    int_contDetalleDeuda = 0

                    str_NombreAlumno = dv_DetalleDeudas.Item(int_contDetalleDeuda).Item("NombreAlumno").ToString
                    int_NombreLength = str_NombreAlumno.Length
                    str_NombreAlumno = str_NombreAlumno.Substring(0, str_NombreAlumno.IndexOf(" - "))

                    int_IndexNombreAlumno = 0
                    int_IndexNombreAlumno = str_ListaHermanos.IndexOf(str_NombreAlumno)

                    If int_IndexNombreAlumno < 0 Then
                        str_ListaHermanos = str_ListaHermanos + str_NombreAlumno + ", "
                    End If

                    cont = cont + 1
                End While

                str_ListaHermanos = str_ListaHermanos.Substring(0, str_ListaHermanos.Length - 2)

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = " "
                oPara5.Range.Font.Bold = False
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

                oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara6.Range.Text = "Referencia: Incumplimiento de PAGO DE ARMADAS POR ENSEÑANZA."
                oPara6.Range.Font.Name = "Arial Narrow"
                oPara6.Format.SpaceAfter = 6
                oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara6.Range.InsertParagraphAfter()

                oWord.Selection.Find.ClearFormatting()
                With oWord.Selection.Find
                    .Text = "Referencia"
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
                oWord.Selection.Find.Execute()
                oWord.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle

                oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara12.Range.Text = " "
                oPara12.Format.SpaceAfter = 6
                oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara12.Range.InsertParagraphAfter()

                oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara7.Range.Text = "De nuestra consideración:"
                oPara7.Range.Font.Name = "Arial Narrow"
                oPara7.Format.SpaceAfter = 6
                oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara7.Range.InsertParagraphAfter()

                oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara13.Range.Text = " "
                oPara13.Format.SpaceAfter = 6
                oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara13.Range.InsertParagraphAfter()

                oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara9.Range.Text = "Mediante la presente cumplimos con comunicarle lo siguiente:"
                oPara9.Range.Font.Name = "Arial Narrow"
                oPara9.Format.SpaceAfter = 6
                oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara9.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = " "
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara10.Range.Text = "1." & vbTab & "Según es de su conocimiento, la Cláusula 7ª del Contrato de Servicio Educativo, suscrito entre ustedes Y el Colegio, el pasado 15 de Febrero del 2010, el Centro Educativo tiene la facultad de suspender el servicio educativo de su hija, si ustedes incumplieran en el pago oportuno de dos armadas del servicio educativo anual."
                oPara10.Range.Font.Name = "Arial Narrow"
                oPara10.Format.SpaceAfter = 6
                oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara10.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = " "
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara13.Range.Text = "2." & vbTab & "En este caso en particular, el Colegio les ha enviado reiteradamente notas informativas para recordarles del incumplimiento en el pago aludido."
                oPara13.Range.Font.Name = "Arial Narrow"
                oPara13.Format.SpaceAfter = 6
                oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara13.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = " "
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = "3. " & vbTab & "Los días 18 y 25 de Junio, se envió una  invitación para encontrar una fecha propicia para que ustedes cumplieran con la obligación asumida con nosotros. Pero no se han acercado, ignorando sin justificación alguna nuestro ofrecimiento para acordar una fecha en la cual ustedes pudieran cumplir con sus obligaciones."
                oPara15.Range.Font.Name = "Arial Narrow"
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara15.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = " "
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                Dim str_AlumnosTexto As String = ""
                Dim str_HijosTexto As String = ""

                If str_ListaHermanos.IndexOf(",") > 0 Then
                    str_HijosTexto = "sus hijos " & str_ListaHermanos & ", "
                    str_AlumnosTexto = "los alumnos(as) " & str_ListaHermanos & ", "
                Else
                    str_HijosTexto = "su hijo(a) " & str_ListaHermanos & ", "
                    str_AlumnosTexto = "el/la alumno(a) " & str_ListaHermanos & ", "
                End If

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = "4." & vbTab & "En consecuencia, al haber  transcurrido con exceso el plazo en el que ustedes debieron pagar las armadas vencidas antes mencionadas correspondientes al Servicio Educativo de " + _
                                    str_HijosTexto + _
                                    "y en cumplimiento del artículo 7º del Contrato de Servicio Educativo, cumplimos con poner en su conocimiento la decisión de SUSPENDER EL SERVICIO EDUCATIVO a " + _
                                     str_AlumnosTexto + _
                                     "a partir del día " & Dev_NombreDia(str_FechaSuspension.DayOfWeek) & " " & str_FechaSuspension.Day & " de " & Dev_NombreMes(str_FechaSuspension.Month) & " en aplicación del Art. 3 del D.S. 005-2002-ED, en concordancia con la Ley de Protección de la Economía Familiar y el Reglamento de las Instituciones Privadas de Educación Básica y Técnico Productiva, sobre las condiciones económicas que rigen el servicio educativo particular."
                oPara15.Range.Font.Name = "Arial Narrow"
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara15.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = " "
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = "Sin otro particular,"
                oPara15.Range.Font.Name = "Arial Narrow"
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = " "
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

                oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara12.Range.Text = "Atentamente,"
                oPara12.Range.Font.Name = "Arial Narrow"
                oPara12.Format.SpaceAfter = 6
                oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara12.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = "Janeth Velasco E."
                oPara15.Range.Font.Name = "Arial Narrow"
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = "Tesorería"
                oPara5.Range.Font.Name = "Arial Narrow"
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

            End If

            'i = i + 1
        Next

        Dim str_nombreDoc As String = ""
        str_nombreDoc = "CartaSuspension_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"

        Dim str_RutaGuardar As String = ""
        str_RutaGuardar = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & str_nombreDoc

        oDoc.SaveAs(str_RutaGuardar)
        oDoc.Close()
        oWord.Quit()
        System.GC.Collect()

        Return str_nombreDoc

    End Function

    Private Function GenerarCarta_Citacion(ByVal ds_Data As DataSet, ByVal str_FechaCitacion As Date, ByVal str_HoraInicio As String, ByVal str_HoraFin As String) As String

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing
        Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTable As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTableCronograma As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oRng As Microsoft.Office.Interop.Word.Range = Nothing
        Dim oShape As Microsoft.Office.Interop.Word.InlineShape = Nothing
        Dim oPara1 As Microsoft.Office.Interop.Word.Paragraph, oPara2 As Microsoft.Office.Interop.Word.Paragraph, oPara3, oPara4, oPara5, oPara6, oPara7, oPara8, oPara9, oPara10, oPara11, oPara12, oPara13, oPara14, oPara15 As Microsoft.Office.Interop.Word.Paragraph
        Dim oChart As Object = Nothing
        Dim Pos As Double = 0
        Dim numAlum As String = ""

        Dim dt_Consolidado As DataTable
        Dim dt_Familias As DataTable
        Dim dv_Alumnos As DataView
        Dim cont As Integer = 0
        Dim cont_Hermanos As Integer = 0

        dt_Consolidado = ds_Data.Tables(0)
        dt_Familias = ds_Data.Tables(1)

        dv_Alumnos = dt_Consolidado.DefaultView

        For i As Integer = 0 To dt_Familias.Rows.Count - 1

            If i = 0 Then

                Dim rutamadre As String = Application.StartupPath()
                'rutamadre = Replace(rutamadre, "bin\Debug", "plantillas")
                Dim saArchivo As String = ""
                saArchivo = rutamadre.ToString & System.Configuration.ConfigurationSettings.AppSettings("RutaPlantillaCitacion").ToString

                oWord = New Microsoft.Office.Interop.Word.Application
                oWord.Visible = False
                oDoc = oWord.Documents.Add(saArchivo)
                oDoc.Content.Copy()

            End If

            If i > dt_Familias.Rows.Count - 1 Then
                Exit For
            End If

            If dt_Familias.Rows(i).Item("Exportar") = 1 Then

                dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i).Item("CodigoFamilia")

                'COLUMNA "A"
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                oPara1.Range.Font.Name = "Arial Narrow"
                oPara1.Format.SpaceAfter = 6
                oPara1.Range.Font.Bold = True
                oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                oPara1.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = " "
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i).Item("NombreFamilia").ToString
                oPara2.Range.Font.Name = "Arial Narrow"
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oWord.Selection.Find.ClearFormatting()
                With oWord.Selection.Find
                    .Text = dt_Familias.Rows(i).Item("NombreFamilia").ToString
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
                oWord.Selection.Find.Execute()
                oWord.Selection.Font.Bold = True

                cont = 0
                cont_Hermanos = dv_Alumnos.Count

                oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                oTable.Range.ParagraphFormat.SpaceAfter = 3
                oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"
                oTable.Cell(1, 1).Range.Font.Name = "Arial Narrow"
                oTable.Cell(1, 2).Range.Font.Name = "Arial Narrow"

                While cont <= dv_Alumnos.Count - 1

                    oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                    oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                    oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                    oTable.Cell(cont + 2, 2).Range.Font.Bold = True

                    cont = cont + 1
                End While

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = " "
                oPara5.Range.Font.Bold = False
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

                oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente de pago las armadas arriba mencionadas."
                oPara6.Range.Font.Name = "Arial Narrow"
                oPara6.Format.SpaceAfter = 6
                oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara6.Range.InsertParagraphAfter()

                oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara12.Range.Text = " "
                oPara12.Format.SpaceAfter = 6
                oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara12.Range.InsertParagraphAfter()

                oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara7.Range.Text = "Es por ello que se cita a usted para el día " & Dev_NombreDia(str_FechaCitacion.DayOfWeek) & " " & str_FechaCitacion.Day & " de " & Dev_NombreMes(str_FechaCitacion.Month) & " del presente desde las " & str_HoraInicio & " hasta " & str_HoraFin & " en la oficina de tesorería del colegio"
                oPara7.Range.Font.Name = "Arial Narrow"
                oPara7.Format.SpaceAfter = 6
                oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara7.Range.InsertParagraphAfter()

                oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara13.Range.Text = " "
                oPara13.Format.SpaceAfter = 6
                oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara13.Range.InsertParagraphAfter()

                oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara9.Range.Text = "Atentamente."
                oPara9.Range.Font.Name = "Arial Narrow"
                oPara9.Format.SpaceAfter = 6
                oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara9.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = " "
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara10.Range.Text = "Janeth Velasco"
                oPara10.Range.Font.Name = "Arial Narrow"
                oPara10.Format.SpaceAfter = 6
                oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara10.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = "Tesorería"
                oPara11.Range.Font.Name = "Arial Narrow"
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                If cont_Hermanos = 1 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()

                    oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara13.Range.Text = " "
                    oPara13.Format.SpaceAfter = 6
                    oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara13.Range.InsertParagraphAfter()

                    oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara15.Range.Text = " "
                    oPara15.Format.SpaceAfter = 6
                    oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara15.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 2 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()

                    oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara15.Range.Text = " "
                    oPara15.Format.SpaceAfter = 6
                    oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara15.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 3 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara15.Range.Text = " "
                    oPara15.Format.SpaceAfter = 6
                    oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara15.Range.InsertParagraphAfter()
                End If

                If i + 1 <= dt_Familias.Rows.Count - 1 Then

                    If dt_Familias.Rows(i + 1).Item("Exportar") = 1 Then

                        dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i + 1).Item("CodigoFamilia")

                        'COLUMNA "B"
                        oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                        oPara1.Range.Font.Bold = True
                        oPara1.Format.SpaceAfter = 6
                        oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                        oPara1.Range.InsertParagraphAfter()

                        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara2.Range.Text = " "
                        oPara2.Range.Font.Bold = False
                        oPara2.Format.SpaceAfter = 6
                        oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara2.Range.InsertParagraphAfter()

                        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
                        oPara2.Format.SpaceAfter = 6
                        oPara2.Range.Font.Bold = False
                        oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara2.Range.InsertParagraphAfter()

                        oWord.Selection.Find.ClearFormatting()
                        With oWord.Selection.Find
                            .Text = dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
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
                        oWord.Selection.Find.Execute()
                        oWord.Selection.Font.Bold = True

                        cont = 0
                        cont_Hermanos = dv_Alumnos.Count

                        oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                        oTable.Range.ParagraphFormat.SpaceAfter = 3
                        oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                        oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"

                        While cont <= dv_Alumnos.Count - 1

                            oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                            oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                            oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                            oTable.Cell(cont + 2, 2).Range.Font.Bold = True

                            cont = cont + 1
                        End While

                        oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara5.Range.Text = " "
                        oPara5.Range.Font.Bold = False
                        oPara5.Format.SpaceAfter = 6
                        oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara5.Range.InsertParagraphAfter()

                        oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente de pago las armadas arriba mencionadas."
                        oPara6.Format.SpaceAfter = 6
                        oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara6.Range.InsertParagraphAfter()

                        oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara12.Range.Text = " "
                        oPara12.Format.SpaceAfter = 6
                        oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara12.Range.InsertParagraphAfter()

                        oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara7.Range.Text = "Es por ello que se cita a usted para el día " & Dev_NombreDia(str_FechaCitacion.DayOfWeek) & " " & str_FechaCitacion.Day & " de " & Dev_NombreMes(str_FechaCitacion.Month) & " del presente desde las " & str_HoraInicio & " hasta " & str_HoraFin & " en la oficina de tesorería del colegio"
                        oPara7.Format.SpaceAfter = 6
                        oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara7.Range.InsertParagraphAfter()

                        oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara13.Range.Text = " "
                        oPara13.Format.SpaceAfter = 6
                        oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara13.Range.InsertParagraphAfter()

                        oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara9.Range.Text = "Atentamente."
                        oPara9.Format.SpaceAfter = 6
                        oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara9.Range.InsertParagraphAfter()

                        oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara15.Range.Text = " "
                        oPara15.Format.SpaceAfter = 6
                        oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara15.Range.InsertParagraphAfter()

                        oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara10.Range.Text = "Janeth Velasco"
                        oPara10.Format.SpaceAfter = 6
                        oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara10.Range.InsertParagraphAfter()

                        oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara11.Range.Text = "Tesorería"
                        oPara11.Format.SpaceAfter = 6
                        oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara11.Range.InsertParagraphAfter()

                        If cont_Hermanos = 1 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()

                            oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara13.Range.Text = " "
                            oPara13.Format.SpaceAfter = 6
                            oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara13.Range.InsertParagraphAfter()

                            oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara15.Range.Text = " "
                            oPara15.Format.SpaceAfter = 6
                            oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara15.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 2 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()

                            oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara15.Range.Text = " "
                            oPara15.Format.SpaceAfter = 6
                            oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara15.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 3 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara15.Range.Text = " "
                            oPara15.Format.SpaceAfter = 6
                            oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara15.Range.InsertParagraphAfter()
                        End If

                    End If

                End If

            End If

            cont = 0
            i = i + 1
        Next

        Dim str_nombreDoc As String = ""
        str_nombreDoc = "CartaCitacion_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"

        Dim str_RutaGuardar As String = ""
        str_RutaGuardar = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & str_nombreDoc

        oDoc.SaveAs(str_RutaGuardar)
        oDoc.Close()
        oWord.Quit()
        System.GC.Collect()

        Return str_nombreDoc

    End Function

    Private Function Dev_NombreMes(ByVal int_CodigoMes As Integer) As String
        Dim str_NombreMes As String = ""

        If int_CodigoMes = 1 Then
            str_NombreMes = "Enero"
        ElseIf int_CodigoMes = 2 Then
            str_NombreMes = "Febrero"
        ElseIf int_CodigoMes = 3 Then
            str_NombreMes = "Marzo"
        ElseIf int_CodigoMes = 4 Then
            str_NombreMes = "Abril"
        ElseIf int_CodigoMes = 5 Then
            str_NombreMes = "Mayo"
        ElseIf int_CodigoMes = 6 Then
            str_NombreMes = "Junio"
        ElseIf int_CodigoMes = 7 Then
            str_NombreMes = "Julio"
        ElseIf int_CodigoMes = 8 Then
            str_NombreMes = "Agosto"
        ElseIf int_CodigoMes = 9 Then
            str_NombreMes = "Septiembre"
        ElseIf int_CodigoMes = 10 Then
            str_NombreMes = "Octubre"
        ElseIf int_CodigoMes = 11 Then
            str_NombreMes = "Noviembre"
        ElseIf int_CodigoMes = 12 Then
            str_NombreMes = "Diciembre"
        End If

        Return str_NombreMes
    End Function

    Private Function Dev_NombreDia(ByVal int_CodigoDia As Integer) As String
        Dim str_NombreDia As String = ""

        If int_CodigoDia = 1 Then
            str_NombreDia = "Lunes"
        ElseIf int_CodigoDia = 2 Then
            str_NombreDia = "Martes"
        ElseIf int_CodigoDia = 3 Then
            str_NombreDia = "Miercoles"
        ElseIf int_CodigoDia = 4 Then
            str_NombreDia = "Jueves"
        ElseIf int_CodigoDia = 5 Then
            str_NombreDia = "Viernes"
        ElseIf int_CodigoDia = 6 Then
            str_NombreDia = "Sábado"
        ElseIf int_CodigoDia = 7 Then
            str_NombreDia = "Domingo"
        End If

        Return str_NombreDia
    End Function


    Public Shared Function ExportarReporteSinFormato(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String, ByVal NombreArchivo As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim nombreRep As String

        Dim str_RutaGuardar As String = ""
        str_RutaGuardar = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & NombreArchivo
        str_RutaGuardar = str_RutaGuardar & "_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".xls"

        Dim rutamadre As String = Application.StartupPath()
        Dim saArchivo As String = ""
        saArchivo = rutamadre.ToString & System.Configuration.ConfigurationSettings.AppSettings("RutaPlantillaExcelSinFormato").ToString

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(saArchivo) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        LlenarPlantillaSinFormato(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(str_RutaGuardar)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return str_RutaGuardar
    End Function

    Private Shared Sub LlenarPlantillaSinFormato(ByVal dtReporte As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim dt_reporte As New DataTable
        Dim fila As Integer = 8
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        dt_reporte = dtReporte

        'Pintado de cabecera
        While cont_columnas <= dt_reporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dt_reporte.Columns(cont_columnas).ColumnName
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Font.Bold = True
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).HorizontalAlignment = 3

            cont_columnas = cont_columnas + 1
        End While

        'Pintado de detalle
        cont_columnas = 0
        fila = 9

        While cont_columnas <= dt_reporte.Columns.Count - 1
            While cont_filas <= dt_reporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dt_reporte.Rows(cont_filas).Item(cont_columnas)
                cont_filas = cont_filas + 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While



        Dim str_letra As String = ""
        Dim int_fila As Integer

        For i As Integer = 0 To dt_reporte.Columns.Count
            str_letra = DevLetraColumna(i + 4)
            For j As Integer = 0 To dt_reporte.Rows.Count
                int_fila = 9 + j
                oCells.Range(str_letra & int_fila).RowHeight = 21
                oCells.Range(str_letra & int_fila).VerticalAlignment = 2 'VerticalAlign.Middle
            Next
            oCells.Range(str_letra & 9.ToString).EntireColumn.AutoFit()
        Next

        Dim total_columnas As Integer = dt_reporte.Columns.Count
        Dim ultimaPosicion As Integer = DevIDColumna("E") + total_columnas - 2 ' (idx + E)

        oCells(3, 5) = "Relación de " & str_NombreEntidadReporte
        oCells.Range("E3:E3").Font.Size = 15
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").Merge()
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").HorizontalAlignment = 3 'HorizontalAlign.Right


        Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("D" & 8 & ":" & DevLetraColumna(dt_reporte.Columns.Count + 3) & (fila + dt_reporte.Rows.Count - 1))
        cuadradoCompleto(oExcel, objCelda)

        oExcel.ActiveWindow.Zoom = 75

    End Sub

    Private Shared Function DevLetraColumna(ByVal idColumna As Integer) As String
        Dim letra As String = ""

        If idColumna = 1 Then
            letra = "A"
        ElseIf idColumna = 2 Then
            letra = "B"
        ElseIf idColumna = 3 Then
            letra = "C"
        ElseIf idColumna = 4 Then
            letra = "D"
        ElseIf idColumna = 5 Then
            letra = "E"
        ElseIf idColumna = 6 Then
            letra = "F"
        ElseIf idColumna = 7 Then
            letra = "G"
        ElseIf idColumna = 8 Then
            letra = "H"
        ElseIf idColumna = 9 Then
            letra = "I"
        ElseIf idColumna = 10 Then
            letra = "J"
        ElseIf idColumna = 11 Then
            letra = "K"
        ElseIf idColumna = 12 Then
            letra = "L"
        ElseIf idColumna = 13 Then
            letra = "M"
        ElseIf idColumna = 14 Then
            letra = "N"
        ElseIf idColumna = 15 Then
            letra = "O"
        ElseIf idColumna = 16 Then
            letra = "P"
        ElseIf idColumna = 17 Then
            letra = "Q"
        ElseIf idColumna = 18 Then
            letra = "R"
        ElseIf idColumna = 19 Then
            letra = "S"
        ElseIf idColumna = 20 Then
            letra = "T"
        ElseIf idColumna = 21 Then
            letra = "U"
        ElseIf idColumna = 22 Then
            letra = "V"
        ElseIf idColumna = 23 Then
            letra = "W"
        ElseIf idColumna = 24 Then
            letra = "X"
        ElseIf idColumna = 25 Then
            letra = "Y"
        ElseIf idColumna = 26 Then
            letra = "Z"
        End If

        Return letra
    End Function

    Private Shared Function DevIDColumna(ByVal strLetra As String) As Integer
        Dim idx As Integer = 0

        If strLetra = "A" Then
            idx = 1
        ElseIf strLetra = "B" Then
            idx = 2
        ElseIf strLetra = "C" Then
            idx = 3
        ElseIf strLetra = "D" Then
            idx = 4
        ElseIf strLetra = "E" Then
            idx = 5
        ElseIf strLetra = "F" Then
            idx = 6
        ElseIf strLetra = "G" Then
            idx = 7
        ElseIf strLetra = "H" Then
            idx = 8
        ElseIf strLetra = "I" Then
            idx = 9
        ElseIf strLetra = "J" Then
            idx = 10
        ElseIf strLetra = "K" Then
            idx = 11
        ElseIf strLetra = "L" Then
            idx = 12
        ElseIf strLetra = "M" Then
            idx = 13
        ElseIf strLetra = "N" Then
            idx = 14
        ElseIf strLetra = "O" Then
            idx = 15
        ElseIf strLetra = "P" Then
            idx = 16
        ElseIf strLetra = "Q" Then
            idx = 17
        ElseIf strLetra = "R" Then
            idx = 18
        ElseIf strLetra = "S" Then
            idx = 19
        ElseIf strLetra = "T" Then
            idx = 20
        ElseIf strLetra = "U" Then
            idx = 21
        ElseIf strLetra = "V" Then
            idx = 22
        ElseIf strLetra = "W" Then
            idx = 23
        ElseIf strLetra = "X" Then
            idx = 24
        ElseIf strLetra = "Y" Then
            idx = 25
        ElseIf strLetra = "Z" Then
            idx = 26
        End If

        Return idx
    End Function

    Private Shared Sub cuadradoCompleto(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range)
        Try


            objRango.Select()
            With mexcel
                '.Range(Rango).Select()
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
            End With
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class