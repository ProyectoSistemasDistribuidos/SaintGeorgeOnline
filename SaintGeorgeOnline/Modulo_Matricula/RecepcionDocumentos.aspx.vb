Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices.Marshal

''' <summary>
''' Modulo de Descarga De Informacion Sobre Pagos Al Banco
''' </summary>
''' <remarks>
''' Código del Modulo:    1
''' Código de la Opción:  7
''' </remarks>

Partial Class Modulo_Matricula_RecepcionDocumentos
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Variables de Posiciones en Excel"

    Private Shared int_HA_Left As Integer = 2 ' Alineación Horizontal Izquierda
    Private Shared int_HA_Center As Integer = 3 ' Alineación Horizontal Centrada
    Private Shared int_HA_Right As Integer = 4 ' Alineación Horizontal Derecha

    Private Shared int_VA_Top As Integer = 1 ' Alineación Vertical Superior
    Private Shared int_VA_Middle As Integer = 2 ' Alineación Vertical Media
    Private Shared int_VA_Bottom As Integer = 3 ' Alineación Vertical Inferior

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Recepción de Documentos de Matrícula")

            If Not Page.IsPostBack Then

                cargarComboPeriodoMatricula()
                ddlPeriodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar

            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validarBuscar(usp_mensaje) Then
                buscar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            grabar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            GenerarReporte()
        Catch ex As Exception
            'EnvioEmailError(4, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboPeriodoMatricula()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlPeriodo, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    Private Function validarBuscar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If tbFiltro.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Filtro")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub buscar()

        Dim int_TipoFiltro As Integer = rbListFiltro.SelectedValue
        Dim str_Filtro As String = tbFiltro.Text.Trim
        Dim int_CodigoAnioMatricula As Integer = ddlPeriodo.SelectedValue

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obj_BL_Matricula As New bl_Matricula
        Dim ds_Lista As DataSet = obj_BL_Matricula.FUN_LIS_RecepcionDocumentosMatricula(int_TipoFiltro, str_Filtro, int_CodigoAnioMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("ListaAlumnos") = ds_Lista.Tables(0)
        ViewState("ListaDocumentos") = ds_Lista.Tables(1)
        ViewState("ListaDocumentosEntregados") = ds_Lista.Tables(2)

        ' Lista de alumnos
        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        ' Lista de Docuemntos
        dl_Documentos.DataSource = ds_Lista.Tables(1)
        dl_Documentos.DataBind()

    End Sub

    Private Sub grabar()

        Dim obj_BL_Matricula As New bl_Matricula
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""

        Dim dt_Registros As New DataTable
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoAlumno", "string")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoAsignacion", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "Check", "integer")

        Dim dr_R As DataRow

        Dim int_CodigoAsignacion As Integer = 0
        Dim str_CodigoAlumno As String = ""
        Dim contObj As Integer = 0

        For Each gvr As GridViewRow In GridView1.Rows

            Dim chk_Documentos As CheckBoxList = gvr.FindControl("chk_Documentos")
            While contObj <= chk_Documentos.Items.Count - 1
                int_CodigoAsignacion = chk_Documentos.Items(contObj).Value
                str_CodigoAlumno = CType(gvr.FindControl("lblCodigoAlumno"), Label).Text

                dr_R = dt_Registros.NewRow
                dr_R.Item("CodigoAlumno") = str_CodigoAlumno
                dr_R.Item("CodigoAsignacion") = int_CodigoAsignacion

                If chk_Documentos.Items(contObj).Selected Then
                    dr_R.Item("Check") = 1
                Else
                    dr_R.Item("Check") = 0
                End If

                dt_Registros.Rows.Add(dr_R)
                contObj = contObj + 1
            End While
            contObj = 0
        Next

        usp_valor = obj_BL_Matricula.FUN_INS_RecepcionDocumentosMatricula(dt_Registros, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            buscar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "alert")
        End If

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     12/12/2011
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
    ''' Fecha de Creación:     12/12/2011
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

            Dim chk_Documentos As CheckBoxList = e.Row.FindControl("chk_Documentos")
            Dim str_CodigoAlumno As String = CType(e.Row.FindControl("lblCodigoAlumno"), Label).Text

            Dim dt_Documentos As New DataTable
            Dim dt_DocumentosAlumno As New DataTable

            dt_Documentos = ViewState("ListaDocumentos")
            dt_DocumentosAlumno = ViewState("ListaDocumentosEntregados")

            Dim dv As DataView = dt_DocumentosAlumno.DefaultView
            dv.RowFilter = "CodigoAlumno = '" & str_CodigoAlumno & "'"

            setearDetalleTabla(dt_Documentos, dv, str_CodigoAlumno, chk_Documentos)

            Dim lblIndex As Label = e.Row.FindControl("lblIndex")
            lblIndex.Text = e.Row.RowIndex + 1

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

#Region "Métodos del Gridview"

    Private Function setearDetalleTabla(ByVal dt_Documentos As DataTable, ByVal dv_DocumentosAlumno As DataView, ByVal str_CodigoAlumno As String, ByVal chkDocumentos As WebControls.CheckBoxList) As Boolean

        chkDocumentos.DataSource = dt_Documentos
        chkDocumentos.DataTextField = "DescripcionChk"
        chkDocumentos.DataValueField = "CodigoAsignacion"
        chkDocumentos.DataBind()

        Dim int_cantDocumentos As Integer = 0
        Dim contObj As Integer = 0

        For Each dr As DataRowView In dv_DocumentosAlumno
            If dr.Item("CodigoAlumno") = str_CodigoAlumno Then
                While contObj <= chkDocumentos.Items.Count - 1
                    If chkDocumentos.Items(contObj).Value = dr.Item("CodigoAsignacion") Then
                        chkDocumentos.Items(contObj).Selected = True
                    End If
                    contObj = contObj + 1
                End While
                contObj = 0
            End If
        Next

    End Function

#End Region

#Region "Exportacion"

    Private Sub GenerarReporte()

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim int_TipoFiltro As Integer = rbListFiltro.SelectedValue
        Dim str_Filtro As String = tbFiltro.Text.Trim
        Dim int_CodigoAnioMatricula As Integer = ddlPeriodo.SelectedValue

        Dim obj_BL_Matricula As New bl_Matricula
        '        Dim ds_Lista As DataSet = obj_BL_Matricula.FUN_REP_RecepcionDocumentosMatricula(int_TipoFiltro, str_Filtro, int_CodigoAnioMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim str_TituloReporte As String = "Reporte de Recepción de Documentos"

        'LLenado de reporte
        Dim NombreArchivo As String = ""
        Dim RutaMadre As String = ""
        Dim downloadBytes As Byte()

        ' If Not ds_Lista.Tables(0).Rows.Count > 0 Then
        'Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
        ' Exit Sub
        '  End If

        'NombreArchivo = ExportarReporte(ds_Lista, str_TituloReporte)
        NombreArchivo = NombreArchivo & ".xls"

        RutaMadre = Server.MapPath(".")
        RutaMadre = RutaMadre.Replace("\Modulo_Matricula", "\Reportes\")

        downloadBytes = File.ReadAllBytes(RutaMadre & NombreArchivo)

        Response.Clear()
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()

    End Sub

#End Region


#Region "Exportacion Reportes"

    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    Private Shared Function GetNewName() As String
        Dim sName As String = Convert.ToString(DateTime.Now.Ticks)
        Return sName
    End Function

    Public Shared Function ExportarReporte(ByVal ds_Reporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteEnfermeria1_3").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets

        ' Selección de la primera hoja
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte_Recepcion_Documentos" ' str_NombreEntidadReporte
        oCells = oSheet.Cells

        oExcel.ActiveWindow.Zoom = 75
        fila = LlenarPlantilla(ds_Reporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)
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

        Return nombreRep
    End Function

    Private Shared Function LlenarPlantilla( _
        ByVal dsReporte As System.Data.DataSet, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        Dim objColor0 As Object = RGB(0, 0, 0) 'Negro

        With oExcel.Range(oCells(2, 3), oCells(2, 5))
            .Merge()
            .RowHeight = 30
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Font.Bold = True
            .Font.Size = 20
            .Value = str_NombreEntidadReporte
        End With

        With oExcel.Range(oCells(3, 3), oCells(3, 5))
            .Merge()
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        fila = 5 : columna = 5 : cont_columnas = 0 : cont_filas = 0
        Dim cont_colAux As Integer = 0
        Dim pos_aux As Integer = 0

        With oExcel.Range(oCells(fila, 2), oCells(fila, 2))
            .HorizontalAlignment = int_HA_Right
            .VerticalAlignment = int_VA_Bottom
            .Font.Bold = True
            .Value = "#"
            .ColumnWidth = 5
        End With
        With oExcel.Range(oCells(fila, 3), oCells(fila, 3))
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Bottom
            .Font.Bold = True
            .Value = "Matrícula"
            .ColumnWidth = 10
        End With
        With oExcel.Range(oCells(fila, 4), oCells(fila, 4))
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Bottom
            .Font.Bold = True
            .Value = "Código"
            .ColumnWidth = 10
        End With
        With oExcel.Range(oCells(fila, 5), oCells(fila, 5))
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Bottom
            .Font.Bold = True
            .Value = "Apellidos y Nombres"
            .ColumnWidth = 50
        End With

        While cont_filas <= dsReporte.Tables(0).Rows.Count - 1
            pos_aux = dsReporte.Tables(0).Rows(cont_filas).Item("Orden")
            With oExcel.Range(oCells(fila, columna + pos_aux), oCells(fila, columna + pos_aux))
                .Value = dsReporte.Tables(0).Rows(cont_filas).Item("Documento")
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Bold = True
                .ColumnWidth = 25
                .RowHeight = 80
                .Interior.Color = 49407
            End With
            cont_filas += 1
        End While

        cuadradoCompleto(oExcel, oExcel.Range(oCells(fila, columna + 1), oCells(fila, columna + pos_aux)))

        fila += 1

        Dim posCel_01 As New posicionCelda
        With posCel_01
            .posfilaini = fila
            .poscolini = 1
            .posfilafin = fila
            .poscolfin = 1
        End With

        columna = 2 : cont_columnas = 0 : cont_filas = 0
        Dim cont_filAux As Integer = 0
        Dim codigo As String = ""
        Dim int_NoMatriculados As Integer = 0
        Dim int_Matriculados As Integer = 0

        While cont_filas <= dsReporte.Tables(1).Rows.Count - 1
            While cont_columnas <= dsReporte.Tables(1).Columns.Count - 1
                With oExcel.Range(oCells(fila + cont_filas, columna + cont_columnas), _
                                  oCells(fila + cont_filas, columna + cont_columnas))
                    .Value = dsReporte.Tables(1).Rows(cont_filas).Item(cont_columnas)
                    If cont_columnas = 1 Then
                        .HorizontalAlignment = int_HA_Center
                        If dsReporte.Tables(1).Rows(cont_filas).Item(cont_columnas) = "No" Then
                            .Interior.Color = 13311
                            int_NoMatriculados += 1
                        Else
                            .Interior.Color = 5296274
                            int_Matriculados += 1
                        End If
                    End If
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .RowHeight = 25
                End With
                If cont_columnas = dsReporte.Tables(1).Columns.Count - 1 Then
                    cont_filAux = 0
                    codigo = oCells(fila + cont_filas, columna + cont_columnas - 1).value ' Codigo del alumno pintado en la celda
                    While cont_filAux <= dsReporte.Tables(2).Rows.Count - 1
                        If dsReporte.Tables(2).Rows(cont_filAux).Item("Codigo") = codigo Then
                            pos_aux = dsReporte.Tables(2).Rows(cont_filAux).Item("Orden").ToString
                            With oExcel.Range(oCells(fila + cont_filas, columna + cont_columnas + pos_aux), _
                                              oCells(fila + cont_filas, columna + cont_columnas + pos_aux))
                                .Value = dsReporte.Tables(2).Rows(cont_filAux).Item("Chk")
                                .HorizontalAlignment = int_HA_Center
                                .VerticalAlignment = int_VA_Middle
                                .WrapText = True
                                .Interior.Color = 5296274
                            End With
                        End If
                        cont_filAux += 1
                    End While
                End If
                cont_columnas += 1
            End While
            cont_columnas = 0
            cont_filas += 1
        End While

        fila += cont_filas
        Dim ultcol As Integer = columna - 1 + dsReporte.Tables(1).Columns.Count + dsReporte.Tables(0).Rows.Count ' ultima columna
        cuadradoCompleto(oExcel, oExcel.Range(oCells(posCel_01.posfilaini, columna), oCells(fila - 1, ultcol)))

        'pintadoCelda(oExcel, oExcel.Range(oCells(fila, columna), oCells(fila, ultcol)), objColor0, 2)
        columna = 5 : cont_columnas = 0 : cont_filas = 0 : pos_aux = 0
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Value = "Total Matrículados"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Bold = True
        End With
        With oExcel.Range(oCells(fila, columna + 1), oCells(fila, columna + 1))
            .Value = int_Matriculados
            .HorizontalAlignment = int_HA_Right
            .VerticalAlignment = int_VA_Middle
            .Font.Bold = True
        End With

        fila += 1
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Value = "Total No Matrículados"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Bold = True
        End With
        With oExcel.Range(oCells(fila, columna + 1), oCells(fila, columna + 1))
            .Value = int_NoMatriculados
            .HorizontalAlignment = int_HA_Right
            .VerticalAlignment = int_VA_Middle
            .Font.Bold = True
        End With

        Dim objRangoAjustePanel As Microsoft.Office.Interop.Excel.Range = oExcel.Range(oCells(posCel_01.posfilaini, posCel_01.poscolini), _
                                                                                       oCells(posCel_01.posfilaini, posCel_01.poscolfin))
        objRangoAjustePanel.Select()
        oExcel.ActiveWindow.FreezePanes = True

        Return str_Fila
    End Function

    Private Shared Sub pintadoCelda(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range, _
                         ByVal objColor As Object, _
                         ByVal int_Posicion As Integer)
        Try

            objRango.Select()
            With mexcel

                If int_Posicion = 1 Then ' LEFT
                    With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                        .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                        .ColorIndex = objColor
                    End With
                End If
                If int_Posicion = 2 Then ' TOP
                    With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                        .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                        .ColorIndex = objColor
                    End With
                End If
                If int_Posicion = 3 Then ' RIGHT
                    With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                        .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                        .ColorIndex = objColor
                    End With
                End If
                If int_Posicion = 4 Then ' BOTTOM
                    With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                        .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                        .ColorIndex = objColor
                    End With
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

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

    Public Class posicionCelda
        Public posfilaini As Integer
        Public poscolini As Integer
        Public posfilafin As Integer
        Public poscolfin As Integer
    End Class

#End Region

End Class
