Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_Utilities
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Imports ClosedXML
Imports ClosedXML.Excel

Partial Class Modulo_Reportes_GenerarLibretas
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Generación de Libretas")

            If Not Page.IsPostBack Then

                cargarComboAnioAcademico()
                cargarComboBimestres()
                ddlAnioAcademico.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar


                'cargarComboAsignacionAula(5)
                'ddlSalon.SelectedValue = 57

                'ddlSalon_SelectedIndexChanged()
                chkAll1.Checked = False

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlSalon_SelectedIndexChanged()
        Try
            cargarGrillaAlumnos()
            chkAll1.Checked = False
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub lstPresentacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPresentacion.SelectedIndexChanged


        cargarComboAsignacionAula(lstPresentacion.SelectedValue)

        'Public tipoReportePrimaria As Integer = 4
        'Public tipoReporteSecundaria As Integer = 2
        'Public tipoReporteIncial As Integer = 3


    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboAnioAcademico()

        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    Private Sub cargarComboBimestres()

        Dim str_Descripcion As String = ""
        Dim obj_BL_Bimestres As New bl_Bimestres
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Bimestres.FUN_LIS_Bimestres(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBimestre, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    Private Sub cargarComboAsignacionAula(ByVal int_TipoNota As Integer)

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim int_CodigoTrabajador As Integer = 0 ' todas las aulas de secundaria, sin filtrar por profesor
        Dim int_CodigoAnioAcademico As Integer = ddlAnioAcademico.SelectedValue
        Dim int_CodigoSede As Integer = 1
        Dim int_Estado As Integer = 1

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
          int_CodigoTrabajador, int_TipoNota, int_CodigoAnioAcademico, int_CodigoSede, _
          int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

        Controles.llenarCombo(ddlSalon, ds_Lista, "Codigo", "DescAulaCompuestaCortaEsp", False, False)

    End Sub

    Private Sub cargarGrillaAlumnos()

        Dim obl_bl_alumnos As New bl_Alumnos
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 

        Dim int_CodigoAnioAcademico As Integer = ddlAnioAcademico.SelectedValue
        Dim int_CodigoAsignacionAula As Integer = ddlSalon.SelectedValue()

        Dim ds_Lista As DataSet = obl_bl_alumnos.FUN_LIS_AlumnosPorAulaGradoyAnioAcademico( _
            int_CodigoAnioAcademico, int_CodigoAsignacionAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

    End Sub


#End Region

#Region "Mensajes"

    ''' <summary>
    ''' Setea las acciones de acceso del usuario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     06/09/2012
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
    ''' Fecha de Creación:     06/09/2012
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
    ''' Fecha de Creación:     06/09/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblidx As Label = e.Row.FindControl("lblidx")
            lblidx.Text = e.Row.RowIndex + 1


            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If

    End Sub

#End Region

#Region "Generacion Libreta"

    Private currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    Private Function GetNewName() As String
        Dim sName As String = Convert.ToString(DateTime.Now.Ticks)
        Return sName
    End Function

    Private Sub Exportar()

        Dim CodAnio As Integer = CInt(ddlAnioAcademico.SelectedValue)
        Dim CodBimestre As Integer = CInt(ddlBimestre.SelectedValue)
        Dim CodSalon As Integer = CInt(ddlSalon.SelectedValue)
        Dim int_idioma As Integer = rbList.SelectedValue

        'LLenado de reporte
        Dim NombreArchivo As String = ""
        Dim RutaMadre As String = ""
        Dim downloadBytes As Byte()
        NombreArchivo = generarLibretaSecundaria(CodAnio, CodBimestre, CodSalon, NombreArchivo, int_idioma)

        'MsgBox(NombreArchivo)

        Dim str_Nombre As String = ddlAnioAcademico.SelectedItem.ToString
        downloadBytes = File.ReadAllBytes(NombreArchivo)

        Response.Clear()
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=Libreta" & str_Nombre & ".xlsx; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()

    End Sub

    Private Function generarLibretaSecundaria(ByVal CodAnio As Integer, _
                                              ByVal CodBimestre As Integer, _
                                              ByVal CodSalon As Integer, _
                                              ByVal NombreArchivo As String, _
                                              ByVal int_idioma As Integer) As String

        Dim rutaTempDest As String = ""
        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaLibretaSecundariaWeb").ToString()
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
        Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
        File.Copy(rutaPlantillas, rutaREpositorioTemporales, True)

        Dim int_Total As Integer = 0

        For Each gvr As GridViewRow In GridView1.Rows
            If CType(gvr.FindControl("chk"), CheckBox).Checked Then
                int_Total += 1
            End If
        Next

        Try

            Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
            Dim ds_Lista As DataSet
            Dim str_CodigoAlumno As String = ""

            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)
            Dim int_PosSheet As Integer = 0

            Dim fila As Integer = 5
            Dim columna As Integer = 2
            Dim cont_columnas As Integer = 0
            Dim cont_filas As Integer = 0
            Dim str_Fila As String = ""

            For ix As Integer = 0 To GridView1.Rows.Count - 1

                If CType(GridView1.Rows(ix).FindControl("Chk"), CheckBox).Checked = False Then
                    Continue For
                End If

                int_PosSheet = int_PosSheet + 1
                str_CodigoAlumno = CType(GridView1.Rows(ix).FindControl("lblCodigoAlumno"), Label).Text
                ds_Lista = obl_rep_libretaNotas.FUN_LIS_REP_LibretaNotasSecundariaImp( _
                            str_CodigoAlumno, CodBimestre, CodAnio, int_idioma, 0, 0, 0, 0)

                Dim ws = workbook.Worksheet(int_PosSheet)

                fila = 5
                columna = 2
                cont_columnas = 0
                cont_filas = 0
                str_Fila = ""

                cont_columnas = 0
                cont_filas = 0
                columna += 1

                ' Pintado de Titulo
                Dim str_GradoAula As String = ds_Lista.Tables(4).Rows(0).Item("DescIngles").ToString
                Dim str_NombreTutor As String = ds_Lista.Tables(4).Rows(0).Item("NombreTutor").ToString
                Dim str_NombreAlumno As String = ds_Lista.Tables(4).Rows(0).Item("NombreAlumno").ToString

                ws.Row(1).Height = 40 ' Listado de Cursos
                With ws.Range(ws.Cell(1, 6), ws.Cell(1, 24))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 20
                    .Style.Font.Bold = True
                    .Value = "REPORT CARD"
                End With

                With ws.Range(ws.Cell(2, 6), ws.Cell(2, 8))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = "NAME"
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.RightBorderColor = XLColor.Black
                End With

                ws.Row(2).Height = 20 ' Listado de Cursos
                With ws.Range(ws.Cell(2, 9), ws.Cell(2, 24))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = str_NombreAlumno
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                ws.Row(3).Height = 20
                With ws.Range(ws.Cell(3, 6), ws.Cell(3, 8))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = "CLASS"
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.RightBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(3, 9), ws.Cell(3, 24))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = str_GradoAula
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                ws.Row(4).Height = 20
                With ws.Range(ws.Cell(4, 6), ws.Cell(4, 8))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = "TUTOR"
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.RightBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(4, 9), ws.Cell(4, 24))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = str_NombreTutor
                End With

                With ws.Range(ws.Cell(2, 6), ws.Cell(4, 24))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                'Pintado de Fecha 
                With ws.Cell(2, 30)
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Font.Bold = True
                    .Value = "Date: " & Now.ToString("MMMM d, yyyy")
                End With

                Dim colIni As Integer = 0
                Dim colFin As Integer = 0
                Dim lstPosCursos As New List(Of Integer)

                For i As Integer = 0 To 60 ' Pintado de Cursos
                    colIni = columna + i ' (i * 3)
                    colFin = colIni + 2
                    ws.Column(colIni).Width = 3
                Next

                colIni = 0
                colFin = 0

                ws.Row(5).Height = 30 ' Listado de Cursos
                For i As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos
                    colIni = columna + (i * 3)
                    colFin = colIni + 2
                    lstPosCursos.Add(colIni) ' agrego la columna de la posicion inicial
                    With ws.Range(ws.Cell(fila, colIni), _
                                  ws.Cell(fila, colFin))
                        .Merge()
                        .Value = ds_Lista.Tables(3).Rows(i).Item("NombreCurso")
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 8
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.WrapText = True
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorderColor = XLColor.Black
                    End With
                    With ws.Range(ws.Cell(fila + 1, colIni), _
                                  ws.Cell(fila + 1, colFin)) ' Fila 6 : Lista de Codigos Asignacion de Grupo
                        .Merge()
                        .Value = ds_Lista.Tables(3).Rows(i).Item("CodigoAsignacionGrupo")
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 8
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.WrapText = True
                    End With
                Next

                columna -= 1

                Dim dt As System.Data.DataTable = ds_Lista.Tables(0)
                Dim sql = From s In ds_Lista.Tables(0).AsEnumerable() _
                          Select CodigoAsignacionGrupo = s.Field(Of Integer)("CodigoGrupoCriterio") _
                          Distinct

                Dim int_NumGrupo As Integer = sql.Count
                Dim int_NumCriterios As Integer = ds_Lista.Tables(0).Rows.Count
                Dim int_NumCalificativos As Integer = ds_Lista.Tables(1).Rows.Count
                Dim int_NumCursos As Integer = ds_Lista.Tables(3).Rows.Count

                Dim int_UltimaFila As Integer = fila + int_NumCriterios + int_NumGrupo + 2 ' 2 grupos de criterios extras
                Dim int_UltimaColumna As Integer = columna + (int_NumCursos * int_NumCalificativos) ' 4 columnas con campos calculados

                fila += 1
                fila += 1

                Dim lstPos As New List(Of Integer)
                Dim str_Grupo As String = ""
                Dim bool_PintadoGrupo As Boolean = False

                Dim int_CodigoAsignacionGrupo As Integer = 0
                Dim int_CodigoCalificativo As Integer = 0
                Dim int_Idx As Integer = 0
                Dim str_Nota As String = ""
                Dim bool_NotaCriterio As Boolean = False

                Dim int_CodigoCriterio As Integer = 0

                For i As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1 ' Pintado de Criterios
                    colIni = 0
                    If str_Grupo = "" Or str_Grupo <> ds_Lista.Tables(0).Rows(i).Item("GrupoCriterio") Then

                        str_Grupo = ds_Lista.Tables(0).Rows(i).Item("GrupoCriterio")
                        With ws.Cell(fila + i, columna)
                            .Style.Font.Bold = True
                            .Value = str_Grupo
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorderColor = XLColor.Black
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.RightBorderColor = XLColor.Black
                        End With

                        For j2 As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1
                            colIni = columna + 1 + (j2 * 3)
                            For kkk = 0 To 2
                                With ws.Cell(fila + i, colIni + kkk)
                                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                    .Style.Border.TopBorderColor = XLColor.Black
                                    If kkk = 0 Then
                                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                        .Style.Border.LeftBorderColor = XLColor.Black
                                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                        .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                                    ElseIf kkk = 1 Then
                                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                        .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo      
                                    End If
                                End With
                            Next
                        Next

                        If bool_PintadoGrupo = False Then
                            For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos
                                colIni = columna + 1 + (j * 3)
                                For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1 ' Pintado de Calificativos
                                    With ws.Cell(fila + i, colIni + k)
                                        .Style.Font.Bold = True
                                        .Value = ds_Lista.Tables(1).Rows(k).Item("Abreviatura")
                                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                        .Style.Border.BottomBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                                        If k = 0 Then
                                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                            .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                            .Style.Border.LeftBorderColor = XLColor.Black '.FromArgb(191, 191, 191) 'Plomo
                                        ElseIf k = 1 Then
                                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                            .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo                                         
                                        End If
                                    End With
                                    ws.Cell(fila + i + 1, colIni + k).Value = ds_Lista.Tables(1).Rows(k).Item("CodigoCalificativo")
                                Next
                            Next
                            bool_PintadoGrupo = True
                            fila += 1
                        End If
                        lstPos.Add(fila + i)
                        fila += 1
                    End If

                    ws.Cell(fila + i, columna).Value = ds_Lista.Tables(0).Rows(i).Item("Criterio")
                    int_CodigoCriterio = ds_Lista.Tables(0).Rows(i).Item("CodigoCriterio")
                    int_CodigoAsignacionGrupo = 0
                    colIni = 0

                    For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1
                        colIni = columna + 1 + (j * 3)
                        int_CodigoAsignacionGrupo = ws.Cell(6, colIni).Value
                        int_CodigoCalificativo = 0
                        For kk = 0 To 2
                            With ws.Cell(fila + i, colIni + kk)
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                                If kk = 0 Then
                                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                    .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                    .Style.Border.LeftBorderColor = XLColor.Black
                                ElseIf kk = 1 Then
                                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                    .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo                                         
                                End If
                            End With
                        Next

                        If bool_NotaCriterio Then : bool_NotaCriterio = False : End If
                        For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1
                            int_CodigoCalificativo = ws.Cell(8, columna + 1 + k).Value
                            For l As Integer = 0 To ds_Lista.Tables(2).Rows.Count - 1
                                If ds_Lista.Tables(2).Rows(l).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo And _
                                    ds_Lista.Tables(2).Rows(l).Item("CodigoCalificativo") = int_CodigoCalificativo And _
                                    ds_Lista.Tables(2).Rows(l).Item("CodigoCriterio") = int_CodigoCriterio Then
                                    With ws.Cell(fila + i, colIni + k)
                                        .Value = "X"
                                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    End With
                                    bool_NotaCriterio = True
                                    Exit For
                                End If
                            Next
                            If bool_NotaCriterio Then : Exit For : End If
                        Next
                    Next
                Next

                ws.Column(2).Width = 43 ' Listado de Criterios

                With ws.Cell(int_UltimaFila + 1, 2)
                    .Value = "ACADEMIC PERFORMANCE"
                    .Style.Font.Bold = True
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                End With

                With ws.Cell(int_UltimaFila + 2, 2)
                    .Value = "OVERALL ATTAINMENT"
                    .Style.Font.Bold = True
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With

                ws.Row(int_UltimaFila + 2).Height = 26 ' fila:24

                colIni = 0
                colFin = 0

                For i As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1 ' Pintado de Nota de Cursos 
                    colIni = 0
                    colIni = columna + 1 + (i * 3)
                    colFin = colIni + 2
                    int_CodigoAsignacionGrupo = ws.Cell(6, colIni).Value()
                    For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                        If ds_Lista.Tables(5).Rows(j).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo Then
                            With ws.Range(ws.Cell(int_UltimaFila + 1, colIni), ws.Cell(int_UltimaFila + 1, colFin))
                                .Merge()
                                .Value = ds_Lista.Tables(5).Rows(j).Item("NotaBimestre")
                                .Style.Font.FontName = "Arial"
                                .Style.Font.FontSize = 8
                                .Style.Font.Bold = True
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Style.Alignment.WrapText = True
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorderColor = XLColor.Black
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                            End With

                            With ws.Range(ws.Cell(int_UltimaFila + 1 + 1, colIni), ws.Cell(int_UltimaFila + 1 + 1, colFin))
                                .Merge()
                                .Value = ds_Lista.Tables(5).Rows(j).Item("Observacion")
                                .Style.Font.FontName = "Arial"
                                .Style.Font.FontSize = 8
                                .Style.Font.Bold = True
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Style.Alignment.WrapText = True
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorderColor = XLColor.Black
                            End With
                            Exit For
                        End If
                    Next
                Next

                With ws.Range(ws.Cell(5, 2), ws.Cell(5, colFin))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(6, 2), ws.Cell(int_UltimaFila, colFin))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_UltimaFila + 1, 2), ws.Cell(int_UltimaFila + 2, colFin))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                ' PINTADO DE NOTAS 
                Dim int_FilaPintadoNotas As Integer = int_UltimaFila + 4 '51
                Dim int_FilaNotas As Integer = int_FilaPintadoNotas ' int_UltimaFilaComentario + 2
                Dim int_UltimaFilaNotas As Integer = int_FilaNotas + ds_Lista.Tables(5).Rows.Count
                Dim int_UltimaColumnaNotas As Integer = 2 + ds_Lista.Tables(5).Columns.Count - 6

                With ws.Range(ws.Cell(int_FilaNotas, 2), _
                              ws.Cell(int_FilaNotas, 9))
                    .Merge()
                    .Value = "TERM AND ANNUAL MARK"
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With
                int_FilaNotas += 1

                For i As Integer = 0 To ds_Lista.Tables(5).Columns.Count - 1
                    If i > 3 And i < 13 - 2 Then
                        If i = 12 - 2 Then
                            With ws.Range(ws.Cell(int_FilaNotas, 2 + i - 4), _
                                          ws.Cell(int_FilaNotas, 2 + i - 4 + 1))
                                .Merge()
                                .Value = ds_Lista.Tables(5).Columns(i).ColumnName
                                .Style.Font.FontName = "Arial"
                                .Style.Font.FontSize = 9
                                .Style.Font.Bold = True
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorderColor = XLColor.Black
                            End With
                            For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                                With ws.Range(ws.Cell(int_FilaNotas + 1 + j, 2 + i - 4), _
                                              ws.Cell(int_FilaNotas + 1 + j, 2 + i - 4 + 1))
                                    .Merge()
                                    .Value = ds_Lista.Tables(5).Rows(j).Item(i)
                                    .Style.Font.FontName = "Arial"
                                    .Style.Font.FontSize = 8
                                    .Style.Font.Bold = True
                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                    .Style.Border.BottomBorderColor = XLColor.Black
                                End With
                            Next
                        Else
                            With ws.Range(ws.Cell(int_FilaNotas, 2 + i - 4), _
                                          ws.Cell(int_FilaNotas, 2 + i - 4))
                                .Value = ds_Lista.Tables(5).Columns(i).ColumnName
                                .Style.Font.FontName = "Arial"
                                .Style.Font.FontSize = 9
                                .Style.Font.Bold = True
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.RightBorderColor = XLColor.Black
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorderColor = XLColor.Black
                            End With
                            For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                                With ws.Range(ws.Cell(int_FilaNotas + 1 + j, 2 + i - 4), _
                                              ws.Cell(int_FilaNotas + 1 + j, 2 + i - 4))
                                    .Value = ds_Lista.Tables(5).Rows(j).Item(i)
                                    .Style.Font.Bold = True
                                    If i = 4 Then
                                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                        .Style.Font.FontSize = 10
                                    Else
                                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                        .Style.Font.FontSize = 8
                                    End If
                                    .Style.Font.FontName = "Arial"
                                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                    .Style.Border.RightBorderColor = XLColor.Black
                                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                    .Style.Border.BottomBorderColor = XLColor.Black
                                End With
                            Next
                        End If
                    End If
                Next

                With ws.Range(ws.Cell(int_FilaNotas - 1, 2), _
                              ws.Cell(int_UltimaFilaNotas + 1, int_UltimaColumnaNotas))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_UltimaFilaNotas + 2, 2), _
                              ws.Cell(int_UltimaFilaNotas + 2, int_UltimaColumnaNotas))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Value = "Note: The note indicates 'P' value test is pending."
                    .Style.Font.Bold = True
                End With

                ' PINTADO DE STUDENT PROFILE 
                Dim int_FilaProfile As Integer = int_FilaPintadoNotas '  int_UltimaFilaComentario + 2
                Dim int_ColumnaProfile As Integer = int_UltimaColumnaNotas + 2
                Dim int_UltimaFilaProfile As Integer = int_FilaNotas - 2 + 8 + ds_Lista.Tables(7).Rows.Count * 2
                Dim int_UltimaColumnaProfile As Integer = int_ColumnaProfile + ds_Lista.Tables(8).Rows.Count * 2

                Dim lstPosProfile As New List(Of posicionCelda)
                Dim posCelda As posicionCelda

                Dim int_PosProfileFila As Integer = 0
                Dim int_PosProfileColumna As Integer = 0

                With ws.Range(ws.Cell(int_FilaProfile, int_ColumnaProfile), _
                              ws.Cell(int_FilaProfile + 7 + (ds_Lista.Tables(7).Rows.Count) * 2, int_ColumnaProfile + 8 + (ds_Lista.Tables(8).Rows.Count) * 2))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaProfile, int_ColumnaProfile), _
                              ws.Cell(int_FilaProfile + 7, int_ColumnaProfile + 8))
                    .Merge()
                    .Value = "STUDENT PROFILE"
                    .Style.Font.Bold = True
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                For i As Integer = 0 To ds_Lista.Tables(8).Rows.Count - 1

                    With ws.Cell(int_FilaProfile - 1, _
                                 int_ColumnaProfile + 8 + 1 + i * 2)
                        .Value = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
                        posCelda = New posicionCelda
                        posCelda.posFila = int_FilaProfile - 1
                        posCelda.posColumna = int_ColumnaProfile + 8 + 1 + i * 2
                        posCelda.Codigo = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
                        lstPosProfile.Add(posCelda)
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile, int_ColumnaProfile + 8 + 1 + i * 2), _
                                  ws.Cell(int_FilaProfile + 7, int_ColumnaProfile + 8 + 1 + i * 2))
                        .Merge()
                        .Value = ds_Lista.Tables(8).Rows(i).Item("Calificativo")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom
                        .Style.Alignment.WrapText = True
                        .Style.Alignment.TextRotation = 90
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 9
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorderColor = XLColor.Black
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile, int_ColumnaProfile + 8 + 2 + i * 2), _
                                  ws.Cell(int_FilaProfile + 7, int_ColumnaProfile + 8 + 2 + i * 2))
                        .Merge()
                        .Value = ds_Lista.Tables(8).Rows(i).Item("CalificativoES")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom
                        .Style.Alignment.WrapText = True
                        .Style.Alignment.TextRotation = 90
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 9
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorderColor = XLColor.Black
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile, int_ColumnaProfile + 8 + 1 + i * 2), _
                                  ws.Cell(int_FilaProfile + 7 + (ds_Lista.Tables(7).Rows.Count) * 2, int_ColumnaProfile + 8 + 1 + i * 2))
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorderColor = XLColor.Black
                    End With
                Next

                Dim int_codCriterio As Integer = 0
                Dim int_codCalificativo As Integer = 0
                Dim int_codCalificativoAux As Integer = 0
                Dim int_codCalificativoPos As Integer = 0

                For i As Integer = 0 To ds_Lista.Tables(7).Rows.Count - 1

                    With ws.Cell(int_FilaProfile + 8 + i * 2, _
                                 int_ColumnaProfile - 1)
                        .Value = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
                        int_codCriterio = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
                        posCelda = New posicionCelda
                        posCelda.posFila = int_FilaProfile + 8 + i * 2
                        posCelda.posColumna = int_ColumnaProfile - 1
                        posCelda.Codigo = 0
                        lstPosProfile.Add(posCelda)
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile + 8 + i * 2, int_ColumnaProfile), _
                                  ws.Cell(int_FilaProfile + 8 + i * 2, int_ColumnaProfile + 8))
                        .Merge()
                        .Value = ds_Lista.Tables(7).Rows(i).Item("Criterio")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.WrapText = True
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 9
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.RightBorderColor = XLColor.Black
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile + 9 + i * 2, int_ColumnaProfile), _
                                  ws.Cell(int_FilaProfile + 9 + i * 2, int_ColumnaProfile + 8))
                        .Merge()
                        .Value = ds_Lista.Tables(7).Rows(i).Item("CriterioES")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.WrapText = True
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 9
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.RightBorderColor = XLColor.Black
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorderColor = XLColor.Black
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile + 9 + i * 2, int_ColumnaProfile + 8), _
                                  ws.Cell(int_FilaProfile + 9 + i * 2, int_ColumnaProfile + 8 + (ds_Lista.Tables(8).Rows.Count) * 2))
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorderColor = XLColor.Black
                    End With

                    For k As Integer = 0 To ds_Lista.Tables(9).Rows.Count - 1
                        If int_codCriterio = ds_Lista.Tables(9).Rows(k).Item("CodigoCriterio") Then
                            int_codCalificativo = ds_Lista.Tables(9).Rows(k).Item("CodigoCalificativo")
                            For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
                                If posCel.Codigo > 0 Then
                                    If posCel.Codigo = int_codCalificativo Then
                                        With ws.Range(ws.Cell(int_FilaProfile + 8 + i * 2, posCel.posColumna), _
                                                      ws.Cell(int_FilaProfile + 9 + i * 2, posCel.posColumna + 1))
                                            .Merge()
                                            .Value = "X"
                                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                            .Style.Font.FontName = "Arial"
                                            .Style.Font.FontSize = 9
                                            .Style.Font.Bold = True
                                        End With
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                    Next
                Next

                For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
                    With ws.Cell(posCel.posFila, posCel.posColumna)
                        .Value = ""
                    End With
                Next

                ' PINTADO DE ASISTENCIA
                Dim int_FilaAsistencia As Integer = int_FilaPintadoNotas ' int_UltimaFilaComentario + 2
                Dim int_ColumnaAsistencia As Integer = int_UltimaColumnaProfile + 1 + 9

                Dim int_UltimaFilaAsistencia As Integer = int_FilaAsistencia - 1 + 8 + 14
                'Dim int_UltimaColumnaAsistencia As Integer = int_ColumnaAsistencia + 8 + 10

                Dim lstPosAsistencia As New List(Of posicionCelda)
                Dim posCelda2 As posicionCelda

                Dim int_PosAsistenciaFila As Integer = 0
                Dim int_PosAsistenciaColumna As Integer = 0

                With ws.Range(ws.Cell(int_FilaAsistencia, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 7 + (ds_Lista.Tables(10).Rows.Count) * 2, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "ATTENDANCE                                  Asistencia"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Style.Alignment.WrapText = True
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 7, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                Dim str_Bimestre As String = ""
                For i As Integer = 0 To 4
                    Select Case i
                        Case 0 : str_Bimestre = "TERM I"
                        Case 1 : str_Bimestre = "TERM II"
                        Case 2 : str_Bimestre = "TERM III"
                        Case 3 : str_Bimestre = "TERM IV"
                        Case 4 : str_Bimestre = "AVERAGE"
                    End Select

                    With ws.Cell(int_FilaAsistencia - 1, int_ColumnaAsistencia + 8 + 1 + i * 2)
                        .Value = "" 'i + 1
                        posCelda2 = New posicionCelda
                        posCelda2.posFila = int_FilaAsistencia - 1
                        posCelda2.posColumna = int_ColumnaAsistencia + 8 + 1 + i * 2
                        posCelda2.Codigo = i + 1
                        lstPosAsistencia.Add(posCelda2)
                    End With

                    With ws.Range(ws.Cell(int_FilaAsistencia, int_ColumnaAsistencia + 8 + 1 + i * 2), _
                                  ws.Cell(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 2 + i * 2))
                        .Merge()
                        .Value = str_Bimestre
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom
                        .Style.Alignment.WrapText = True
                        .Style.Alignment.TextRotation = 90
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 10
                        .Style.Font.Bold = True
                    End With

                    With ws.Range(ws.Cell(int_FilaAsistencia, int_ColumnaAsistencia + 8 + 1 + i * 2), _
                                  ws.Cell(int_FilaAsistencia + 7 + (ds_Lista.Tables(10).Rows.Count) * 2, int_ColumnaAsistencia + 8 + 2 + i * 2))
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorderColor = XLColor.Black
                    End With
                Next

                Dim int_codBimestre As Integer = 0
                Dim int_codBimestreAux As Integer = 0

                Dim lstPosAsis As New List(Of posicionCelda)
                Dim posCelda3 As posicionCelda

                ' TARDANZAS Y CONDUCTA
                ' TARDANZAS
                With ws.Range(ws.Cell(int_FilaAsistencia + 8, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 11, int_ColumnaAsistencia + 4))
                    .Merge()
                    .Value = "LATES          Tardanzas"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 11, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 11, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 9, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "Justified          Justificado"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 9
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 9, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 9, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 8 + 8, int_ColumnaAsistencia + 5))
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 10, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 11, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "Unjustified          Injustificado"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 9
                End With

                ' FALTAS
                With ws.Range(ws.Cell(int_FilaAsistencia + 12, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 15, int_ColumnaAsistencia + 4))
                    .Merge()
                    .Value = "ABSENCES          Ausencias"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 15, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 15, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 12, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 13, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "Justified          Justificado"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 9
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 13, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 13, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 14, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 15, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "Unjustified          Injustificado"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 9
                End With

                ' DEMERITOS
                With ws.Range(ws.Cell(int_FilaAsistencia + 16, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 17, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "DEMERITS                                            Deméritos"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 17, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 17, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                ' MERITOS
                With ws.Range(ws.Cell(int_FilaAsistencia + 18, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 19, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "MERITS                                                      Méritos"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 19, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 19, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                ' CONDUCT MARK
                With ws.Range(ws.Cell(int_FilaAsistencia + 20, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 21, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "CONDUCT MARK                                            Nota de conducta"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 21, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 21, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                Dim pos_Bimestre As Integer = 0
                Dim int_filapos As Integer = 0
                Dim int_colpos As Integer = 0
                Dim int_filaini As Integer = 0
                Dim str_Rango As String = ""

                If ds_Lista.Tables(10).Rows.Count > 0 Then
                    For i As Integer = 0 To ds_Lista.Tables(10).Rows.Count - 1

                        int_filapos = 8 + i * 2 : int_colpos = 9 : pos_Bimestre = 0
                        With ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                            .Merge()
                            .Value = ds_Lista.Tables(10).Rows(i).Item("Bim1")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 10
                            .Style.Font.Bold = True
                        End With

                        pos_Bimestre = 2
                        With ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                            .Merge()
                            .Value = ds_Lista.Tables(10).Rows(i).Item("Bim2")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 10
                            .Style.Font.Bold = True
                        End With

                        pos_Bimestre = 4
                        With ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                            .Merge()
                            .Value = ds_Lista.Tables(10).Rows(i).Item("Bim3")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 10
                            .Style.Font.Bold = True
                        End With

                        pos_Bimestre = 6
                        With ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                            .Merge()
                            .Value = ds_Lista.Tables(10).Rows(i).Item("Bim4")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 10
                            .Style.Font.Bold = True
                        End With

                        str_Rango = DevLetraColumna(int_ColumnaAsistencia + int_colpos) + (int_FilaAsistencia + int_filapos).ToString + ":" + _
                                    DevLetraColumna(int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre) + (int_FilaAsistencia + int_filapos + 1).ToString

                        pos_Bimestre = 8

                        'Dim cellWithFormulaA1 = ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                        '                                 ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))

                        'cellWithFormulaA1.FormulaA1 = ds_Lista.Tables(10).Rows(i).Item("operacion") & "(" + str_Rango + ")"

                        With ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                            .Merge()
                            .Value = ds_Lista.Tables(10).Rows(i).Item("operacion")
                            .Style.NumberFormat.Format = "0"
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 10
                            .Style.Font.Bold = True
                        End With

                    Next
                End If

                ' PINTADO DE COMENTARIOS 
                Dim int_FilaComentario As Integer = 56 '62 '54 ' 51 ' int_UltimaFila + 3
                Dim int_MaxNumFilasComentario As Integer = 13 * 2 '13 * 2
                Dim int_UltimaFilaComentario As Integer = int_FilaComentario + int_MaxNumFilasComentario + 1  'ds_Lista.Tables(6).Rows.Count
                int_FilaComentario += 1
                With ws.Cell(int_FilaComentario, 2)
                    .Value = "COMMENTS"
                    .Style.Font.Bold = True
                End With
                int_FilaComentario += 1

                Dim pos_ComentAux As Integer = 0
                Dim int_FilasPintadas As Integer = 0

                For i As Integer = 0 To ds_Lista.Tables(6).Rows.Count - 1 ' Pintado de Oservaciones de Cursos 

                    int_FilasPintadas += 1
                    ws.Row(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1).Height = 40

                    If ds_Lista.Tables(6).Rows(i).Item("Observacion") IsNot DBNull.Value Then

                        With ws.Range(ws.Cell(int_FilaComentario - 2 + int_FilasPintadas * 2, 2), _
                                      ws.Cell(int_FilaComentario - 2 + int_FilasPintadas * 2, int_UltimaColumna + 12))
                            .Merge()
                            .Value = ds_Lista.Tables(6).Rows(i).Item("Curso") & ":"
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 11 '10
                            .Style.Font.Bold = True
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                        End With

                        ws.Row(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1).Height = 40
                        With ws.Range(ws.Cell(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, 2), _
                                      ws.Cell(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, int_UltimaColumna + 12))
                            .Merge()
                            .Value = ds_Lista.Tables(6).Rows(i).Item("Observacion")
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 11 ' 9
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                        End With

                    End If
                Next

                int_FilaAsistencia = int_UltimaFilaComentario + 10 - 25
                With ws.Range(ws.Cell(int_FilaComentario, 2), _
                              ws.Cell(int_FilaComentario + int_FilasPintadas * 2, int_UltimaColumna + 12))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                ' FIRMAS
                With ws.Range(ws.Cell(int_FilaAsistencia + 23, 2), ws.Cell(int_FilaAsistencia + 23, 5))
                    .Merge()
                    .Value = "SIGNATURE OF TUTOR"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 23, 8), ws.Cell(int_FilaAsistencia + 23, 23))
                    .Merge()
                    .Value = "SIGNATURE OF PARENT"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorderColor = XLColor.Black
                End With

                ws.Rows(6).Height = 0 ' Elimino la fila de codigos de asignacion de grupos
                ws.Rows(8).Height = 0 ' Elimino la fila de codigos de los calificativos

                ws.Row(4).InsertRowsBelow(1)
                ws.Rows(5).Height = 5 ' Listado de Cursos

                'borrarPintado(ws, ws.Range(ws.Cell(5, 2), ws.Cell(5, int_UltimaColumna)))
                'cuadradoCompleto(ws, ws.Range(ws.Cell(2, 6), ws.Cell(4, 24)))
                'ws.Columns("A:A").Delete()

                ws.Column(1).Width = 3

                ws.PageSetup.AdjustTo(63)
                ws.PageSetup.PageOrientation = XLPageOrientation.Landscape
                ws.PageSetup.Margins.Top = 0.5 '1.9
                ws.PageSetup.Margins.Bottom = 0.5 '1.9
                ws.PageSetup.Margins.Left = 0 '0.6
                ws.PageSetup.Margins.Right = 0 '0.6
                ws.PageSetup.Margins.Header = 0 '0.8
                ws.PageSetup.Margins.Footer = 0 '0.8


            Next



            'For idel As Integer = int_PosSheet + 1 To 35
            '    workbook.Worksheet(idel).Delete()
            'Next

            'Dim ite As Integer = 35 - (int_PosSheet + 1)
            'For id As Integer = 0 To ite - 1
            '    workbook.Worksheet(int_PosSheet + 1).Delete()
            'Next

            ''For idel As Integer = 35 To int_PosSheet + 1 Step -1
            ''    workbook.Worksheet(idel).Delete()
            ''    workbook.Save()
            ''Next


            '' ''workbook.Worksheet(3).Delete()
            ' ''workbook.Worksheets.Delete(2)
            '' ''workbook.Save()
            ' ''workbook.Worksheets.Delete(3)
            '' '' workbook.Save()

            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales

            Return rutaTempDest

        Catch ex As Exception
            Return rutaTempDest
        End Try

    End Function

    Public Class posicionCelda
        Public posFila As Integer
        Public posColumna As Integer
        Public Codigo As Integer
    End Class

    Private Function DevLetraColumna(ByVal idColumna As Integer) As String
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

        ElseIf idColumna = 27 Then
            letra = "AA"
        ElseIf idColumna = 28 Then
            letra = "AB"
        ElseIf idColumna = 29 Then
            letra = "AC"
        ElseIf idColumna = 30 Then
            letra = "AD"
        ElseIf idColumna = 31 Then
            letra = "AE"
        ElseIf idColumna = 32 Then
            letra = "AF"
        ElseIf idColumna = 33 Then
            letra = "AG"
        ElseIf idColumna = 34 Then
            letra = "AH"
        ElseIf idColumna = 35 Then
            letra = "AI"
        ElseIf idColumna = 36 Then
            letra = "AJ"
        ElseIf idColumna = 37 Then
            letra = "AK"
        ElseIf idColumna = 38 Then
            letra = "AL"
        ElseIf idColumna = 39 Then
            letra = "AM"
        ElseIf idColumna = 40 Then
            letra = "AN"
        ElseIf idColumna = 41 Then
            letra = "AO"
        ElseIf idColumna = 42 Then
            letra = "AP"
        ElseIf idColumna = 43 Then
            letra = "AQ"
        ElseIf idColumna = 44 Then
            letra = "AR"
        ElseIf idColumna = 45 Then
            letra = "AS"
        ElseIf idColumna = 46 Then
            letra = "AT"
        ElseIf idColumna = 47 Then
            letra = "AU"
        ElseIf idColumna = 48 Then
            letra = "AV"
        ElseIf idColumna = 49 Then
            letra = "AW"
        ElseIf idColumna = 50 Then
            letra = "AX"
        ElseIf idColumna = 51 Then
            letra = "AY"
        ElseIf idColumna = 52 Then
            letra = "AZ"
        End If

        Return letra
    End Function

    'Public Function ExportarLibreta(ByVal ds_Reporte As System.Data.DataSet, _
    '                                              ByVal str_NombreEntidadReporte As String, _
    '                                              ByVal str_PeriodoAcademico As String, _
    '                                              ByVal dt_fecha As Date) As String
    '    Dim nombreRep As String
    '    nombreRep = GetNewName()
    '    Dim rutaTemporal As String = ""
    '    LlenarPlantillaLibreta(ds_Reporte, str_NombreEntidadReporte, rutaTemporal, str_PeriodoAcademico, dt_fecha)
    '    Return rutaTemporal
    'End Function

    'Private Function LlenarPlantillaLibreta(ByVal dsReporte As System.Data.DataSet, _
    '    ByVal str_NombreEntidadReporte As String, ByRef rutaTempDest As String, ByVal str_PeriodoAcademidoRep As String, _
    '    ByVal dt_fecha As Date) As String

    '    Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_PagosServicios").ToString()
    '    Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
    '    Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
    '    File.Copy(rutaPlantillas, rutaREpositorioTemporales, True)

    '    Try

    '        Dim workbook As New XLWorkbook(rutaREpositorioTemporales)
    '        Dim ws = workbook.Worksheet(1)

    '        Dim fila As Integer = 5
    '        Dim columna As Integer = 2
    '        Dim cont_columnas As Integer = 0
    '        Dim cont_filas As Integer = 0
    '        Dim str_Fila As String = ""

    '        ws.Row(2).Height = 30
    '        With ws.Range(ws.Cell(2, 3), ws.Cell(2, 3))
    '            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
    '            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '            .Style.Font.Bold = True
    '            .Style.Font.FontSize = 20
    '            .Value = "Reporte de Alumnos Deudores por Servicio de Enseñanza " & str_PeriodoAcademidoRep
    '        End With

    '        With ws.Range(ws.Cell(3, 3), ws.Cell(3, 3))
    '            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
    '            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '            .Style.Font.Bold = True
    '            .Value = "" '"Solo deudas por conceptos académicos en Soles hasta el " & dt_fecha.Date
    '        End With

    '        With ws.Range(ws.Cell(4, 3), ws.Cell(4, 3))
    '            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
    '            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '            .Style.Font.Bold = True
    '            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
    '        End With

    '        fila = 6 : columna = 2 : cont_columnas = 0 : cont_filas = 0

    '        ws.Column(1).Width = 3
    '        ws.Column(columna).Width = 45 'apellidos y nombre
    '        ws.Column(columna + 1).Width = 30 'concepto
    '        ws.Column(columna + 2).Width = 10 'ene
    '        ws.Column(columna + 3).Width = 10 'feb
    '        ws.Column(columna + 4).Width = 10 'mar
    '        ws.Column(columna + 5).Width = 10 'abr
    '        ws.Column(columna + 6).Width = 10 'may
    '        ws.Column(columna + 7).Width = 10 'jun
    '        ws.Column(columna + 8).Width = 10 'jul
    '        ws.Column(columna + 9).Width = 10 'ago
    '        ws.Column(columna + 10).Width = 10 'set
    '        ws.Column(columna + 11).Width = 10 'oct
    '        ws.Column(columna + 12).Width = 10 'nov
    '        ws.Column(columna + 13).Width = 10 'dic
    '        ws.Column(columna + 14).Width = 15 'total

    '        Dim str_Cabecera As String = ""

    '        For i = 0 To 14
    '            Select Case i
    '                Case 0 : str_Cabecera = "Apellidos y Nombres"
    '                Case 1 : str_Cabecera = "Concepto"
    '                Case 2 : str_Cabecera = "ENE"
    '                Case 3 : str_Cabecera = "FEB"
    '                Case 4 : str_Cabecera = "MAR"
    '                Case 5 : str_Cabecera = "ABR"
    '                Case 6 : str_Cabecera = "MAY"
    '                Case 7 : str_Cabecera = "JUN"
    '                Case 8 : str_Cabecera = "JUL"
    '                Case 9 : str_Cabecera = "AGO"
    '                Case 10 : str_Cabecera = "SET"
    '                Case 11 : str_Cabecera = "OCT"
    '                Case 12 : str_Cabecera = "NOV"
    '                Case 13 : str_Cabecera = "DIC"
    '                Case 14 : str_Cabecera = "TOTAL"
    '            End Select

    '            With ws.Range(ws.Cell(fila, columna + i), ws.Cell(fila, columna + i))
    '                .Value = str_Cabecera
    '                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
    '                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                .Style.Alignment.WrapText = True
    '                .Style.Font.Bold = True
    '                .Style.Font.FontSize = 10
    '                .Style.Fill.BackgroundColor = XLColor.FromHtml("#95b3d7")
    '            End With
    '        Next

    '        Dim dt_aulas As DataTable = dsReporte.Tables(0)
    '        Dim dt_alumnos As DataTable = dsReporte.Tables(1)
    '        Dim dt_conceptos As DataTable = dsReporte.Tables(2)
    '        Dim dt_deudas As DataTable = dsReporte.Tables(3)
    '        Dim dt_total As DataTable = dsReporte.Tables(4)

    '        Dim lstAulas As IEnumerable(Of cla_aula2)
    '        Dim lstReport As IEnumerable(Of cla_report)

    '        lstReport = _
    '        From sql4 In dt_total.AsEnumerable() _
    '        Select New cla_report With { _
    '            .aula = sql4.Field(Of Integer)("aula"), _
    '            .codigoalumno = sql4.Field(Of String)("alumno"), _
    '            .mes = sql4.Field(Of Integer)("mes"), _
    '            .monto = sql4.Field(Of Decimal)("monto") _
    '        }

    '        lstAulas = _
    '        From sql1 In dt_aulas.AsEnumerable() _
    '        Select New cla_aula2 With { _
    '               .orden = sql1.Field(Of Integer)("fila"), _
    '               .codigogrado = sql1.Field(Of Integer)("grado"), _
    '               .codigoaula = sql1.Field(Of Integer)("aula"), _
    '               .descripcion = sql1.Field(Of String)("gradoaula"), _
    '               .lstalumno = (From sql2 In dt_alumnos.AsEnumerable() _
    '                              Where sql2.Field(Of Integer)("aula") = sql1.Field(Of Integer)("aula") _
    '                              Select New cla_alumno2 With { _
    '                                    .codigoaula = sql2.Field(Of Integer)("aula"), _
    '                                    .codigoalumno = sql2.Field(Of String)("alumno"), _
    '                                    .nombre = sql2.Field(Of String)("nombrealumno"), _
    '                                    .conceptos = sql2.Field(Of Integer)("conceptos"), _
    '                                    .lstconceptos = (From sql3 In dt_conceptos.AsEnumerable() _
    '                                                     Where sql3.Field(Of String)("alumno") = sql2.Field(Of String)("alumno") _
    '                                                     Select New cla_concepto With { _
    '                                                        .codigoconcepto = sql3.Field(Of Integer)("codconcepto"), _
    '                                                        .descripcion = sql3.Field(Of String)("concepto"), _
    '                                                        .lstdeudas = (From sql4 In dt_deudas.AsEnumerable() _
    '                                                                       Where sql4.Field(Of String)("alumno") = sql3.Field(Of String)("alumno") _
    '                                                                       And sql4.Field(Of Integer)("codconcepto") = sql3.Field(Of Integer)("codconcepto") _
    '                                                                       Select New cla_deuda2 With { _
    '                                                                       .codigoalumno = sql4.Field(Of String)("alumno"), _
    '                                                                       .codigoconcepto = sql4.Field(Of Integer)("codconcepto"), _
    '                                                                       .mes = sql4.Field(Of Integer)("mes"), _
    '                                                                       .monto = sql4.Field(Of Decimal)("monto") _
    '                                                                    }) _
    '                                                     }) _
    '                              }) _
    '                }

    '        Dim filAux As Integer = 0
    '        Dim colAux As Integer = 0
    '        fila = 7 : columna = 2 : cont_columnas = 0 : cont_filas = 0

    '        Dim iteaula As Integer = 0
    '        Dim itealu As Integer = 0
    '        Dim itecon As Integer = 0
    '        Dim itedeu As Integer = 0
    '        Dim iteaux As Integer = 0
    '        Dim codaula As Integer = 0

    '        filAux = fila

    '        For Each aula In lstAulas
    '            fila += 1
    '            codaula = aula.codigoaula
    '            With ws.Range(ws.Cell(fila + iteaula, columna), _
    '                          ws.Cell(fila + iteaula, columna))
    '                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
    '                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                .Style.Font.Bold = True
    '                .Value = aula.descripcion
    '                .Style.Font.FontSize = 9

    '                For Each alumno In aula.lstalumno
    '                    fila += 1
    '                    With ws.Range(ws.Cell(fila, columna), _
    '                                  ws.Cell(fila, columna))
    '                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
    '                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                        .Style.Alignment.Indent = 3
    '                        .Value = alumno.nombre
    '                        .Style.Font.FontSize = 9

    '                        itecon = 0
    '                        iteaux = 0
    '                        For Each concepto In alumno.lstconceptos
    '                            iteaux += 1
    '                            With ws.Range(ws.Cell(fila, columna + 1), _
    '                                          ws.Cell(fila, columna + 1))
    '                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
    '                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                                .Value = concepto.descripcion
    '                            End With
    '                            For i As Integer = 1 To 12
    '                                With ws.Range(ws.Cell(fila, columna + 1 + i), _
    '                                              ws.Cell(fila, columna + 1 + i))
    '                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
    '                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                                    .Value = "0.00"
    '                                    .Style.NumberFormat.Format = "#,##0.00"
    '                                    .Style.Font.FontSize = 9
    '                                End With
    '                            Next
    '                            'total por alumno y concepto 
    '                            Dim mifun As Func(Of cla_deuda2, Decimal)
    '                            mifun = Function(a) a.monto
    '                            With ws.Range(ws.Cell(fila, columna + 1 + 12 + 1), _
    '                                          ws.Cell(fila, columna + 1 + 12 + 1))
    '                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
    '                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                                .Value = concepto.lstdeudas.Sum(mifun)
    '                                .Style.NumberFormat.Format = "#,##0.00"
    '                                .Style.Font.FontSize = 9
    '                                .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
    '                            End With
    '                            For Each deuda In concepto.lstdeudas
    '                                With ws.Range(ws.Cell(fila, columna + 1 + deuda.mes), _
    '                                              ws.Cell(fila, columna + 1 + deuda.mes))
    '                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
    '                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                                    .Value = deuda.monto
    '                                    .Style.NumberFormat.Format = "#,##0.00"
    '                                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00")
    '                                    .Style.Font.FontSize = 9
    '                                End With
    '                            Next
    '                            If alumno.conceptos > 1 And iteaux < alumno.conceptos Then
    '                                fila += 1
    '                            End If
    '                        Next
    '                    End With
    '                Next

    '                fila += 1
    '                ' total de alumnos por aula
    '                Dim mifun2 As Func(Of cla_report, Decimal)
    '                mifun2 = Function(a) a.monto
    '                Dim tot = From t In dt_total.AsEnumerable() _
    '                          Select codigoaula = t.Field(Of Integer)("aula"), _
    '                                 codigoalumno = t.Field(Of String)("alumno") _
    '                                 Distinct
    '                With ws.Range(ws.Cell(fila, columna), _
    '                              ws.Cell(fila, columna))
    '                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
    '                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                    .Value = "Total Alumnos: " & CStr(tot.Where(Function(b) b.codigoaula = codaula).Count)
    '                    .Style.Font.FontSize = 9
    '                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
    '                End With
    '                With ws.Range(ws.Cell(fila, columna + 1), _
    '                              ws.Cell(fila, columna + 1))
    '                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
    '                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                    .Value = "Total Por Nivel:"
    '                    .Style.Font.FontSize = 9
    '                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
    '                End With
    '                For i As Integer = 1 To 12
    '                    With ws.Range(ws.Cell(fila, columna + 1 + i), _
    '                                  ws.Cell(fila, columna + 1 + i))
    '                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
    '                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                        .Value = lstReport.Where(Function(a) a.aula = codaula And a.mes = i).Sum(mifun2)
    '                        .Style.NumberFormat.Format = "#,##0.00"
    '                        .Style.Font.FontSize = 9
    '                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
    '                    End With
    '                Next
    '                With ws.Range(ws.Cell(fila, columna + 1 + 13), _
    '                              ws.Cell(fila, columna + 1 + 13))
    '                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
    '                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                    .Value = lstReport.Where(Function(a) a.aula = codaula).Sum(mifun2)
    '                    .Style.NumberFormat.Format = "#,##0.00"
    '                    .Style.Font.FontSize = 9
    '                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
    '                End With
    '                fila += 1
    '            End With
    '        Next

    '        Dim totg = From t In dt_total.AsEnumerable() _
    '                   Select codigoalumno = t.Field(Of String)("alumno") _
    '                   Distinct
    '        With ws.Range(ws.Cell(fila + 1, columna), _
    '                      ws.Cell(fila + 1, columna))
    '            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
    '            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '            .Value = "Total Alumnos: " & CStr(totg.Count)
    '            .Style.Font.FontSize = 9
    '            .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
    '        End With
    '        With ws.Range(ws.Cell(fila + 1, columna + 1), _
    '                      ws.Cell(fila + 1, columna + 1))
    '            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
    '            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '            .Value = "Total General:"
    '            .Style.Font.FontSize = 9
    '            .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
    '        End With
    '        Dim mifun3 As Func(Of cla_report, Decimal)
    '        mifun3 = Function(a) a.monto

    '        For i As Integer = 1 To 12
    '            With ws.Range(ws.Cell(fila + 1, columna + 1 + i), _
    '                          ws.Cell(fila + 1, columna + 1 + i))
    '                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
    '                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '                .Value = lstReport.Where(Function(a) a.mes = i).Sum(mifun3)
    '                .Style.NumberFormat.Format = "#,##0.00"
    '                .Style.Font.FontSize = 9
    '                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
    '            End With
    '        Next
    '        With ws.Range(ws.Cell(fila + 1, columna + 1 + 13), _
    '                      ws.Cell(fila + 1, columna + 1 + 13))
    '            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
    '            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '            .Value = lstReport.Sum(mifun3)
    '            .Style.NumberFormat.Format = "#,##0.00"
    '            .Style.Font.FontSize = 9
    '            .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
    '        End With

    '        ws.SheetView.Freeze(6, 0)

    '        workbook.Save()
    '        rutaTempDest = rutaREpositorioTemporales

    '    Catch ex As Exception
    '    End Try

    'End Function

#End Region

#Region "Clases auxiliares"

    Public Class cla_aula
        Public orden As Integer
        Public codigogrado As Integer
        Public codigoaula As Integer
        Public descripcion As String
        Public lstalumno As IEnumerable(Of cla_alumno)
    End Class
    Public Class cla_alumno
        Public codigoaula As Integer
        Public codigoalumno As String
        Public nombre As String
        Public lstdeudas As IEnumerable(Of cla_deuda)
    End Class
    Public Class cla_deuda
        Public codigoalumno As String
        Public concepto As String
        Public mes As Integer
        Public monto As Decimal
    End Class
    Public Class cla_report
        Public aula As Integer
        Public codigoalumno As String
        Public mes As Integer
        Public monto As Decimal
    End Class

    Public Class cla_aula2
        Public orden As Integer
        Public codigogrado As Integer
        Public codigoaula As Integer
        Public descripcion As String
        Public lstalumno As IEnumerable(Of cla_alumno2)
    End Class
    Public Class cla_alumno2
        Public codigoaula As Integer
        Public codigoalumno As String
        Public nombre As String
        Public conceptos As Integer
        Public lstconceptos As IEnumerable(Of cla_concepto)
    End Class
    Public Class cla_concepto
        Public codigoconcepto As Integer
        Public descripcion As String
        Public lstdeudas As IEnumerable(Of cla_deuda2)
    End Class
    Public Class cla_deuda2
        Public codigoalumno As String
        Public codigoconcepto As Integer
        Public mes As Integer
        Public monto As Decimal
    End Class

#End Region

End Class
