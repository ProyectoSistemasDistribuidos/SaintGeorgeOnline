Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloAcademico
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML
Imports ClosedXML.Excel

Partial Class Modulo_Notas_CertificadoDeEstudios
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Impresión de Certificados de Estudio")
            If Not Page.IsPostBack Then
                'btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                cargarComboAnioAcademico()
                ddlPeriodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                cargarComboGrados()
                limpiarCombo(ddlAula, False, True)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlGrado.SelectedValue > 0 And ddlGrado.SelectedValue > 0 Then
                cargarComboAsignacionAulas()
            Else
                GridView1.DataBind()
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlGrado.SelectedValue > 0 And ddlGrado.SelectedValue > 0 Then
                cargarComboAsignacionAulas()
            Else
                GridView1.DataBind()
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlAula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlGrado.SelectedValue > 0 And ddlPeriodo.SelectedValue > 0 And ddlAula.SelectedValue > 0 Then
                cargarGrillaAlumnos()
            Else
                GridView1.DataBind()
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub limpiarCombo(ByVal ddl As DropDownList, ByVal bTodos As Boolean, ByVal bSeleccion As Boolean)
        Controles.limpiarCombo(ddl, bTodos, bSeleccion)
    End Sub

    Private Sub cargarComboAnioAcademico()
        Dim int_CodigoAnioTop As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlPeriodo, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    Private Sub cargarComboGrados()
        Dim int_CodigoNivel As Integer = 2 ' secundaria
        Dim int_Top As Integer = 6 ' 6 grados
        Dim int_orden As Integer = 2 ' grados desc

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        'Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_GradosXNivel(int_CodigoNivel, int_Top, int_orden, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        'Controles.llenarCombo(ddlGrado, ds_Lista, "Codigo", "DescripcionEspaniol", False, True)

        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlGrado, ds_Lista, "Codigo", "DescripcionCompuestaEspaniol2", False, True)

    End Sub

    Private Sub cargarComboAsignacionAulas()
        Dim int_CodigoPeriodo As Integer = ddlPeriodo.SelectedValue
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasParaGrupos( _
            int_CodigoPeriodo, int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAula, ds_Lista, "Codigo", "DescAula", False, True)
    End Sub

    Private Sub cargarGrillaAlumnos()

        Dim obl_bl_alumnos As New bl_Alumnos
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 

        Dim int_CodigoAnioAcademico As Integer = ddlPeriodo.SelectedValue
        Dim int_CodigoAsignacionAula As Integer = ddlAula.SelectedValue()

        Dim ds_Lista As DataSet = obl_bl_alumnos.FUN_LIS_AlumnosPorAulaGradoyAnioAcademico( _
            int_CodigoAnioAcademico, int_CodigoAsignacionAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

    End Sub

    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region

#Region "Exportación"

    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    Private Shared Function GetNewName() As String
        Dim sName As String = Convert.ToString(DateTime.Now.Ticks)
        Return sName
    End Function

    Private Sub Exportar()

        Dim int_PeriodoAcademico As String = ddlPeriodo.SelectedValue
        'Dim str_codigoAlumno As String = "20030024"

        If ddlGrado.SelectedValue > 3 And ddlGrado.SelectedValue < 9 Then
            Dim codigos As String = ""

            For Each gvr As GridViewRow In GridView1.Rows
                If CType(gvr.FindControl("chk"), CheckBox).Checked Then
                    codigos += (CType(gvr.FindControl("lblCodigoAlumno"), Label).Text) & ","
                End If
            Next

            reporteCertificadosPrimaria(codigos)

        Else
            Dim lstCodAlumno As New List(Of String)
            For Each gvr As GridViewRow In GridView1.Rows
                If CType(gvr.FindControl("chk"), CheckBox).Checked Then
                    lstCodAlumno.Add(CType(gvr.FindControl("lblCodigoAlumno"), Label).Text)
                End If
            Next

            Dim NombreArchivo As String = ""
            Dim RutaMadre As String = ""
            Dim downloadBytes As Byte()

            Dim nombreRep As String = GetNewName()
            Dim rutaTemporal As String = ""

            rutaTemporal = GenerarCertificadoSecundaria(int_PeriodoAcademico, lstCodAlumno)
            NombreArchivo = rutaTemporal
            downloadBytes = File.ReadAllBytes(NombreArchivo)

            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=CertificadoOficial.xlsx; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()
        End If

       

    End Sub

    Private Shared Function GenerarCertificadoSecundaria(ByVal int_PeriodoAcademico As Integer, ByVal lstCodAlumno As List(Of String)) As String


        Try

            'Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_CertificadoOficial").ToString()
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaRepositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            'File.Copy(rutaPlantillas, rutaRepositorioTemporales, True)

            Dim workbook As New XLWorkbook()
            Dim ite As Integer = 1
            Dim obl_CertificadoEstudio As New bl_CertificadoEstudio

            For Each alumno As String In lstCodAlumno

                Dim ws = workbook.Worksheets.Add("Hoja" & ite.ToString)

                Dim dsReporte As DataSet
                dsReporte = obl_CertificadoEstudio.FUN_LIS_CertificadoEstudio(int_PeriodoAcademico, alumno, 0, 0, 0, 0)

                Dim dt_info As System.Data.DataTable = dsReporte.Tables(0)
                Dim dt_anios As System.Data.DataTable = dsReporte.Tables(1)
                Dim dt_grados As System.Data.DataTable = dsReporte.Tables(2)
                Dim dt_nomgrados As System.Data.DataTable = dsReporte.Tables(3)
                Dim dt_curricula As System.Data.DataTable = dsReporte.Tables(4)
                Dim dt_matriz As System.Data.DataTable = dsReporte.Tables(5)
                Dim dt_subsa As System.Data.DataTable = dsReporte.Tables(6)
                Dim dt_colegios As System.Data.DataTable = dsReporte.Tables(7)

                Dim fila As Integer = 5
                Dim columna As Integer = 3
                Dim cont_columnas As Integer = 0
                Dim cont_filas As Integer = 0

                ' ANCHO
                ws.Column(1).Width = 4 '
                ws.Column(2).Width = 4
                ws.Column(3).Width = 26 '
                ws.Column(4).Width = 7.57 '
                ws.Column(5).Width = 7.86 '
                ws.Column(6).Width = 7.86 '
                ws.Column(7).Width = 7.86 '
                ws.Column(8).Width = 7.86 '
                ws.Column(9).Width = 3.29 '
                ws.Column(10).Width = 3.29 '
                ws.Column(11).Width = 3.29 '
                ws.Column(12).Width = 3.29 '
                ws.Column(13).Width = 3.29 '

                ' ALTO 
                ws.Row(1).Height = 46 '48 ' 
                ws.Row(4).Height = 30 ' 20
                ws.Row(6).Height = 5 ' 
                ws.Row(8).Height = 17
                ws.Row(9).Height = 23 ' 
                ws.Row(10).Height = 5
                ws.Row(11).Height = 22
                ws.Row(12).Height = 22 ' 
                ws.Row(13).Height = 0
                ws.Row(14).Height = 17 ' 
                ws.Row(15).Height = 23 ' 30 anios
                ws.Row(16).Height = 16 '
                ws.Row(48).Height = 8
                ws.Row(49).Height = 19.5
                ws.Row(50).Height = 15.75

                fila = 5 : columna = 4 : cont_columnas = 0 : cont_filas = 0
                ws.Cell(fila, columna).Value = dt_info.Rows(0).Item("Dato")

                columna = 9
                ws.Cell(fila, columna).Value = dt_info.Rows(0).Item("numUgel")

                fila = 8 : columna = 3
                With ws.Range(ws.Cell(fila, columna), _
                              ws.Cell(fila, columna))
                    '.Merge()
                    .Value = "'" & dt_info.Rows(0).Item("codModular").ToString
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Indent = 6
                End With
                columna = 8
                With ws.Range(ws.Cell(fila, columna), _
                              ws.Cell(fila, columna))
                    .Value = dt_info.Rows(0).Item("departamento")
                End With

                fila = 9 : columna = 3
                With ws.Range(ws.Cell(fila, columna), _
                              ws.Cell(fila, columna))
                    .Value = dt_info.Rows(0).Item("provincia")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Indent = 2
                End With
                columna = 4
                With ws.Range(ws.Cell(fila, columna), _
                              ws.Cell(fila, columna + 3))
                    .Merge()
                    .Value = dt_info.Rows(0).Item("distrito")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Indent = 5
                End With

                columna = 9
                ws.Cell(fila, columna).Value = dt_info.Rows(0).Item("urbanizacion")

                fila = 12 : columna = 2
                With ws.Range(ws.Cell(fila, columna), _
                              ws.Cell(fila, columna))
                    .Value = dt_info.Rows(0).Item("nomAlumno")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Indent = 1
                End With
                columna = 7
                With ws.Range(ws.Cell(fila, columna), _
                              ws.Cell(fila, columna + 1))
                    .Merge()
                    .Value = "'" & dt_info.Rows(0).Item("dni").ToString
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    '.Style.Alignment.Indent = 1
                End With

                fila = 14 : columna = 2
                With ws.Range(ws.Cell(fila, columna), _
                              ws.Cell(fila, columna))
                    .Value = dt_nomgrados.Rows(0).Item("ListaGrados")
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    '.Style.Alignment.Indent = 1
                End With

                fila = 15 '6
                columna = 3

                Dim int_colAux As Integer = 0
                For i As Integer = 1 To 5
                    ws.Cell(fila + 1, columna + i).Value = 0
                    ws.Cell(fila + 1, columna + i).Style.Font.FontColor = XLColor.White
                Next

                For Each dr As DataRow In dt_grados.Rows
                    int_colAux = dr.Item("col")
                    With ws.Range(ws.Cell(fila, columna + int_colAux), _
                                  ws.Cell(fila, columna + int_colAux))
                        .Value = dr.Item("dAnio")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    End With
                    With ws.Range(ws.Cell(fila + 1, columna + int_colAux), _
                                  ws.Cell(fila + 1, columna + int_colAux))
                        .Value = dr.Item("cAnio") ' fila codigo anio 18
                        .Style.Font.FontColor = XLColor.White
                    End With
                Next

                With ws.Range(ws.Cell(1, 1), _
                              ws.Cell(15, 10))
                    .Style.Font.FontSize = 10
                End With

                ' seteo general
                ' i:31
                ' j:5
                fila = 16 '7
                columna = 3
                For i As Integer = 1 To 31

                    'If i < 6 Then
                    '    ws.Row(fila + i).Height = 15.6
                    'Else
                    '    ws.Row(fila + i).Height = 15.9
                    'End If

                    ws.Row(fila + i).Height = 14.5 ' 15 '16.0 '15.4

                    For j As Integer = 1 To 5
                        With ws.Range(ws.Cell(fila + i, columna + j), _
                                      ws.Cell(fila + i, columna + j))
                            .Value = "-"
                            .Style.Font.FontSize = 8
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            '.Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                            '.Style.Border.OutsideBorderColor = XLColor.FromHtml("#000000")
                        End With
                    Next
                Next

                fila = 17 '8
                Dim codCMaux As Integer = 0
                Dim codAnioaux As Integer = 0

                For Each dr As DataRow In dt_curricula.Rows
                    If dr.Item("Orden") = 12 Then
                        cont_filas += 4
                    End If

                    codCMaux = dr.Item("cCCE") ' codigo curso certificado

                    With ws.Range(ws.Cell(fila + cont_filas, columna - 1), _
                                  ws.Cell(fila + cont_filas, columna - 1))
                        .Value = dr.Item("cCCE")
                        .Style.Font.FontSize = 8
                        .Style.Font.FontColor = XLColor.White
                    End With

                    With ws.Range(ws.Cell(fila + cont_filas, columna), _
                                  ws.Cell(fila + cont_filas, columna))
                        .Value = dr.Item("NomC")
                        .Style.Font.FontSize = 8
                        '.Style.Alignment.Indent = 1

                        If dr.Item("Orden") >= 12 Then
                            .Style.Font.FontColor = XLColor.Black
                            .Style.Alignment.Indent = 4
                        Else
                            .Style.Font.FontColor = XLColor.White
                        End If

                    End With

                    For j As Integer = 1 To 5
                        codAnioaux = ws.Cell(16, columna + j).Value ' x cada año busco las notas

                        For Each drN As DataRow In dt_matriz.Rows
                            If drN.Item("cCCE") = codCMaux And drN.Item("cAnio") = codAnioaux Then
                                With ws.Range(ws.Cell(fila + cont_filas, columna + j), _
                                              ws.Cell(fila + cont_filas, columna + j))
                                    .Value = drN.Item("Nota")
                                    .Style.Font.FontSize = 8
                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                End With
                                Exit For
                            End If
                        Next
                    Next
                    cont_filas += 1
                Next

                'colegio procedencia
                columna = 9 ' col aux
                cont_filas = 12
                Dim colAux As Integer = 0
                For Each drC As DataRow In dt_colegios.Rows
                    colAux = drC.Item("col")

                    With ws.Range(ws.Cell(fila, columna + colAux), _
                                  ws.Cell(fila + 10, columna + colAux))
                        .Merge()
                        .Value = drC.Item("ncolegio")
                        .Style.Alignment.TextRotation = 90
                        .Style.Font.FontSize = 8
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom
                        '.Style.Alignment.Indent = 1
                    End With

                    With ws.Range(ws.Cell(fila + cont_filas - 1, columna + colAux), _
                                  ws.Cell(fila + cont_filas - 1 + 2, columna + colAux))
                        .Merge()
                        .Value = drC.Item("dAnio")
                        .Style.Alignment.TextRotation = 90
                        .Style.Font.FontSize = 8
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        '.Style.Alignment.Indent = 2
                    End With

                Next





                'notas subsa
                columna = 9 : cont_filas = 17 ' 16
                For Each drN As DataRow In dt_subsa.Rows
                    With ws.Range(ws.Cell(fila + cont_filas, columna), _
                                  ws.Cell(fila + cont_filas, columna))
                        '.Merge()
                        .Value = drN.Item("dGr") & " " & _
                                drN.Item("dAnio") & " " & _
                                drN.Item("nomC") & " " & _
                                drN.Item("FechaEx")

                        .Style.Font.FontSize = 8
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.Indent = 1
                    End With
                    cont_filas += 1
                Next



                'ws.Row(48).Height = 20
                columna = 5 : cont_filas = 32 '33 '31
                With ws.Range(ws.Cell(fila + cont_filas, columna), _
                              ws.Cell(fila + cont_filas, columna)) '+ 2))
                    '.Merge()
                    .Value = dt_info.Rows(0).Item("distrito") & ", " & Format(Now, "MM")
                    .Style.Font.FontSize = 10
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left 'Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.Indent = 8
                End With

                columna = 8
                With ws.Range(ws.Cell(fila + cont_filas, columna), _
                              ws.Cell(fila + cont_filas, columna))
                    .Value = Format(Now, "MMMM").ToUpper
                    .Style.Font.FontSize = 10
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.Indent = 8 '5
                End With

                columna = 13
                With ws.Range(ws.Cell(fila + cont_filas, columna), _
                              ws.Cell(fila + cont_filas, columna))
                    .Value = Now.Year.ToString.Substring(2, 2)
                    .Style.Font.FontSize = 10
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.Indent = 1
                End With

                ite += 1

                ws.PageSetup.PaperSize = XLPaperSize.LetterPaper
                ws.PageSetup.PageOrientation = XLPageOrientation.Portrait
                ws.PageSetup.AdjustTo(100)
                ws.PageSetup.FitToPages(1, 1)

                ws.PageSetup.Margins.Top = 0 '1.9
                ws.PageSetup.Margins.Bottom = 0 '1.9
                ws.PageSetup.Margins.Left = 0 '0.6
                ws.PageSetup.Margins.Right = 0 '0.6
                ws.PageSetup.Margins.Header = 0 '0.8
                ws.PageSetup.Margins.Footer = 0 '0.8

                ws.PageSetup.PrintAreas.Add("A1:M50")
                'ws.PageSetup.PrintAreas.Add("A1:M48")

            Next

            workbook.SaveAs(rutaRepositorioTemporales)



            'Dim workbook As New XLWorkbook(rutaRepositorioTemporales)
            'Dim ws = workbook.Worksheet(1)
            'workbook.Save()
            'rutaTempDest = rutaREpositorioTemporales
            Return rutaRepositorioTemporales

        Catch ex As Exception
        End Try

    End Function

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblidx As System.Web.UI.WebControls.Label = e.Row.FindControl("lblidx")
            lblidx.Text = e.Row.RowIndex + 1
            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If

    End Sub

#End Region



#Region " exportar  certificado de primaria"
    ''' <summary>
    ''' exportar los certificados de los alumnos de primaria
    ''' </summary>
    ''' <param name="codALumnos">lista de codigos separados por comas 
    ''</param>
    ''' <remarks></remarks>
    Private Sub reporteCertificadosPrimaria(ByVal codALumnos As String)
        Try
            ''------------------------------------------------------
            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim dc As New Dictionary(Of String, Object)
            Dim dtCertificado As New System.Data.DataTable
            Dim nParam As String = "USP_ReporteCertificadoPrimaria"
            dc("codAsignAula") = 0
            dc("codALumno") = codALumnos
            dc("codGrado") = 0

            dtCertificado = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("certificadoPrimaria")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            'File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            Dim filas As Integer = 4
            Dim filasTemp As Integer = 0
            Dim workbook As New XLWorkbook()


            ''------------------------------------------------------------------------------------------------------------''
            Dim sqlALumnos = From alumnos In dtCertificado.AsEnumerable() Group alumnos By _
                             codAlumno = alumnos("codAlumno"), _
                             nombreAlumno = alumnos("nombreAlumno") Into detalle = Group _
                             Select New With { _
                           .codAlumno = codAlumno, .nombreAlumno = nombreAlumno, .listaGrados = (From notas In detalle.AsEnumerable() Group notas By codGrado = notas("codGrado") Into listaGrados = Group Select New With {.grado = codGrado}), _
                             .anios = (From notas In detalle.AsEnumerable() Group notas By anio = notas("anio"), codGrado = notas("codGrado") _
                                       Into grados = Group _
                                       Select New With { _
                                       .codGrado = codGrado, .anio = anio, _
                                       .notas = (From nots In grados.AsEnumerable() Order By nots("orden") Ascending Select New With { _
                                                .nota = nots("notaAnual"), _
                                                .orden = nots("orden")})})}
            ''------------------------------------------------------------------------------------------------------------''




        
            Dim ws = Nothing

            Dim contadorHojas As Integer = 1

            Dim esPrimeroII As Integer = 0

            For Each osql In sqlALumnos
                'Dim ws = workbook.Worksheet(contadorHojas)
                '  esPrimeroII += 1
                '  If esPrimeroII = 1 Then
                ' ws = workbook.Worksheets(1)
                'Else

                ws = workbook.Worksheets.Add(osql.codAlumno.ToString)

                ' End If



                Dim filasInicia As Integer = 17
                With ws.Range(ws.Cell(4, 4), ws.Cell(4, 10))
                    .Merge()
                    .Value = "      LIMA     METROPOLITANA"
                    .Style.Alignment.Indent = 3
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontSize = 19
                End With


                For columnas As Integer = 6 To 11
                    ws.Column(columnas).Width = 5.14
                Next
                ''-----
                With ws.Cell(15, 6)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                With ws.Cell(15, 7)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                With ws.Cell(15, 8)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                With ws.Cell(15, 9)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                With ws.Cell(15, 10)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                With ws.Cell(15, 11)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With

                ''-----
                For filasDefault As Integer = 17 To 23
                    For columnas As Integer = 6 To 11
                        With ws.Cell(filasDefault, columnas)
                            .Value = "--"
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        End With
                    Next
                Next
                ''
                Dim listaGrados As String = ""
                Dim esPrimero As Integer = 0

                For Each oGra In osql.listaGrados
                    esPrimero += 1
                    If esPrimero = osql.listaGrados.Count Then
                        If osql.listaGrados.Count > 1 Then
                            listaGrados &= oGra.grado.ToString & "° y "
                        Else
                            If osql.listaGrados.Count = 1 Then
                                listaGrados &= oGra.grado.ToString & "° "
                            Else
                                listaGrados &= oGra.grado.ToString & "°, "
                            End If
                        End If
                    Else
                        If osql.listaGrados.Count = 1 Then
                            listaGrados &= oGra.grado.ToString & "° "
                        Else
                            listaGrados &= oGra.grado.ToString & "°, "
                        End If
                    End If
                Next
                With ws.Range(ws.Cell(12, 11), ws.Cell(12, 13))
                    .Merge()
                    .Value = listaGrados
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    '.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With

                ''
                With ws.Range(ws.Cell(12, 2), ws.Cell(12, 8))

                    .Merge()
                    .Value = osql.nombreAlumno
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    '.Style.Alignment.Vertical=
                    '.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top

                End With
                ''
                For Each oAnios In osql.anios
                    With ws.Cell(15, CInt(oAnios.codGrado) + 5)
                        .Value = oAnios.anio
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    End With

                    For Each oNtas In oAnios.notas
                        ws.Row(filasInicia).Height = 13
                        With ws.Cell(filasInicia, CInt(oAnios.codGrado) + 5)
                            .Value = oNtas.nota
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        End With
                        filasInicia += 1

                    Next


                Next



                ''ancho de  columnas 
                With ws.Column(5)
                    .Width = 5.14 ' 3.25
                End With
                With ws.Column(4)
                    .Width = 5.43
                End With

                '' configuracion de impresion 
                ''-------------------------------------------
                ws.Row(1).Height = 15
                ws.Row(2).Height = 12
                ws.Row(3).Height = 61.5
                ws.Row(4).Height = 29.25
                For fila As Integer = 5 To 10
                    ws.Row(fila).Height = 15
                Next

                ws.Row(11).Height = 24
                ws.Row(12).Height = 24
                ws.Row(13).Height = 15
                ws.Row(14).Height = 12.75
                ws.Row(15).Height = 25.5
                ws.Row(16).Height = 11.25



                ''-------------------------------------------
                ws.PageSetup.PaperSize = XLPaperSize.LetterPaper
                ws.PageSetup.PageOrientation = XLPageOrientation.Portrait
                ws.PageSetup.AdjustTo(100)
                ws.PageSetup.FitToPages(1, 1)
                ws.PageSetup.Margins.Top = 0 '1.9
                ws.PageSetup.Margins.Bottom = 0 '1.9
                ws.PageSetup.Margins.Left = 0 '0.6
                ws.PageSetup.Margins.Right = 0 '0.6
                ws.PageSetup.Margins.Header = 0 '0.8
                ws.PageSetup.Margins.Footer = 0 '0.8
                '''''''''''''''''''''''''''''''''''''''

                ws.PageSetup.PrintAreas.Add("A1:M45")
                contadorHojas += 1
            Next


            'Dim contHojas As Integer = 1
            'For Each hojas In workbook.Worksheets
            '    contHojas += 1
            '    If hojas.Name = "Hoja2" Then
            '        '  hojas.Hide()
            '        workbook.Worksheets(contHojas).Hide()
            '    End If
            'Next
            ''-------------------------------------------
            ' workbook.Worksheets(0).Hide()



            workbook.SaveAs(rutaREpositorioTemporales)

            Dim downloadBytes1 As Byte()
            downloadBytes1 = File.ReadAllBytes(rutaREpositorioTemporales)

            ' Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "certificadoPrimaria" & rutaTemp.ToString() & ".xlsx" + "; size=" + downloadBytes1.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes1)
            Response.End()


        Catch ex As Exception

        End Try
    End Sub

#End Region
End Class
