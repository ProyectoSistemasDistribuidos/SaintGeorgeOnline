Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloReportes
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_BusinessLogic.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloBancoLibros
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Imports System.Runtime.InteropServices.Marshal
Imports System.Threading
Partial Class Modulo_Reportes_ReportesLibros
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1
#Region "Atributos"

    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

#End Region
#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Formatos / Reportes")
            If Not Page.IsPostBack Then
                cargarCombos()
                cargarListaReportes()
                cargarListaPresentacion()
                pnlAniosUtilidad.Visible = False
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim usp_mensaje As String = ""
        Try
            If validar(usp_mensaje) Then
                If lstPresentacion.SelectedValue = 11 Then
                    btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                    Exportar()
                ElseIf lstPresentacion.SelectedValue = 13 Then
                    btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                    ExportarDevolucion()
                    'ExportarDeudoresBancoLibros()
                ElseIf lstPresentacion.SelectedValue = 12 Then
                    btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                    ExportarDinamico()
                ElseIf lstPresentacion.SelectedValue = 14 Then
                    ExportarAniosUtilidad()
                ElseIf lstPresentacion.SelectedValue = 15 Then
                    ExportarFotosLibros()
                ElseIf lstPresentacion.SelectedValue = 40 Then
                    btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                    ExportarDeudoresBancoLibros()
                End If
            Else
                MostrarAlertBox(usp_mensaje)
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

    End Sub

    Protected Sub lstReportes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarListaPresentacion()
            mostrarPanelParametros()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender2.Hide()
    End Sub


#End Region

#Region "Metodos"
   
    Private Sub mostrarPanelParametros()
        If lstReportes.SelectedValue = 9 Or lstReportes.SelectedValue = 10 Or lstReportes.SelectedValue = 23 Then
            pnlPrestamos.Visible = True
            pnlAniosUtilidad.Visible = False

        ElseIf lstReportes.SelectedValue = 11 Then
            pnlAniosUtilidad.Visible = True
            pnlPrestamos.Visible = False
            ddlPeriodoFin.SelectedValue = 0
            ddlPeriodoInicio.SelectedValue = 0
        Else
            'ElseIf lstReportes.SelectedValue = 12 Then
            pnlAniosUtilidad.Visible = False
            pnlPrestamos.Visible = False


            '    pnlReporte1.Visible = False
            '    pnlReporte2.Visible = False
            '    pnlReporte3.Visible = False

        End If

    End Sub

    Private Sub cargarListaReportes()

        Dim int_CodigoTipoReporte As Integer = 3 ' Reportes de Enfermería
        Dim obj_BL_Reportes As New bl_Reportes
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Reportes.FUN_LIS_Reportes(int_CodigoTipoReporte, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("ListaReportes") = ds_Lista

        lstReportes.DataSource = ds_Lista.Tables(0)
        lstReportes.DataTextField = "Nombre"
        lstReportes.DataValueField = "Codigo"
        lstReportes.DataBind()

        lstReportes.SelectedIndex = 0

    End Sub

    Private Sub cargarListaPresentacion()

        Dim dt As DataTable = CType(ViewState("ListaReportes"), DataSet).Tables(1)
        Dim int_CodigoReporte As Integer = lstReportes.SelectedValue

        Dim dv As DataView = dt.DefaultView

        With dv
            .RowFilter = "1=1 and CodigoReporte = " & int_CodigoReporte
        End With

        lstPresentacion.DataSource = dt
        lstPresentacion.DataTextField = "Descripcion"
        lstPresentacion.DataValueField = "CodigoDetalle"
        lstPresentacion.DataBind()


        lstPresentacion.SelectedIndex = 0

    End Sub

    ''' <summary>
    ''' Exporte el listado de la información filtrada en los diferentes formatos indicados.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               
    ''' Fecha de Creación:     19/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ExportarAniosUtilidad()

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoUsuarioLogueado
        Dim obj_BL_AsignacionVigenciaTitulos As New bl_AsignacionVigenciaTitulos
        Dim int_PeriodoInicio As String = CInt(ddlPeriodoInicio.SelectedItem.Text)
        Dim int_PeriodoFin As Integer = CInt(ddlPeriodoFin.SelectedItem.Text)

        Dim ds_Lista As DataSet = obj_BL_AsignacionVigenciaTitulos.FUN_REP_DinamicoAniosUtilidad(int_PeriodoInicio, int_PeriodoFin, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        NombreArchivo = Exportacion.ExportarReporteAniosUtilidad(ds_Lista, int_PeriodoInicio, int_PeriodoFin, "Años Utilidad")
        NombreArchivo = NombreArchivo & ".xls"
        rutamadre = Server.MapPath(".")
        rutamadre = rutamadre.Replace("\Modulo_Reportes", "\Reportes\")

        downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

        Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()
    End Sub
    'LISTADOS
    ''' <summary>
    ''' Obtiene una cadena de caracteres aleatorio (de tipo numero) que será el nombre del documento.
    ''' </summary>
    ''' <returns>Retorna descripcion de nombre de documento a generar</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function GetNewName() As String
        Dim sName As String = Convert.ToString(DateTime.Now.Ticks)
        Return sName
    End Function
    Public Shared Function LlenarDataTable(ByVal dtReporte As System.Data.DataTable, ByVal int_CodigoGrado As Integer) As DataTable
        Dim int_fila As Integer = 0
        'Dim int_columna As Integer = 0
        Dim dt As DataTable
        Dim dv As DataView
        dt = dtReporte.Copy
        dt.Rows.Clear()
        dv = dtReporte.DefaultView
        Dim dr As DataRow

        dv.RowFilter = "1=1 and GD_CodigoGrado=" & int_CodigoGrado 'dtRepetidos.Rows(cont_1).Item("CodigoLibro")

        While int_fila <= dv.Count - 1
            dr = dt.NewRow
            If dtReporte.Columns.Count = 7 Then
                dr.Item(0) = dv.Item(int_fila).Item(0)
                dr.Item(1) = dv.Item(int_fila).Item(1)
                dr.Item(2) = dv.Item(int_fila).Item(2)
                dr.Item(3) = dv.Item(int_fila).Item(3)
                dr.Item(4) = dv.Item(int_fila).Item(4)
                dr.Item(5) = dv.Item(int_fila).Item(5)
                dr.Item(6) = dv.Item(int_fila).Item(6)
            ElseIf dtReporte.Columns.Count > 7 Then
                dr.Item(0) = dv.Item(int_fila).Item(0)
                dr.Item(1) = dv.Item(int_fila).Item(1)
                dr.Item(2) = dv.Item(int_fila).Item(2)
                dr.Item(3) = dv.Item(int_fila).Item(3)
                dr.Item(4) = dv.Item(int_fila).Item(4)
                dr.Item(5) = dv.Item(int_fila).Item(5)
                dr.Item(6) = dv.Item(int_fila).Item(6)
                dr.Item(7) = dv.Item(int_fila).Item(7)
                dr.Item(8) = dv.Item(int_fila).Item(8)
                dr.Item(9) = dv.Item(int_fila).Item(9)
                dr.Item(10) = dv.Item(int_fila).Item(10)
                dr.Item(11) = dv.Item(int_fila).Item(11)
                dr.Item(12) = dv.Item(int_fila).Item(12)
            End If
            dt.Rows.Add(dr)
            int_fila = int_fila + 1
        End While

        Return dt
    End Function
    ''' <summary>
    ''' Insertar los bordes de las celdas según el rango indicado.
    ''' </summary>
    ''' <param name="mexcel">Instancia de documento excel</param>
    ''' <param name="objRango">Rando de celdas a insertar sus bordes</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
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

    ''' <summary>
    ''' Exporte el listado de la información filtrada en los diferentes formatos indicados.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ExportarDinamico()

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoUsuarioLogueado
        Dim obj_BL_Prestamos As New bl_Prestamos
        Dim int_CodigoAnio As Integer = ddlAnioAcademico.SelectedValue()
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue()
        Dim int_CodigoAula As Integer = ddlAula.SelectedValue()

        Dim ds_Lista As DataSet = obj_BL_Prestamos.FUN_REP_DinamicoCantidadLibrosVendidosPorAula(int_CodigoAnio, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        NombreArchivo = Exportacion.ExportarReporteDinamicoCantidadLibrosVendidosPorAula(ds_Lista, int_CodigoGrado, int_CodigoAula, "PrestamoPorAlumno")
        NombreArchivo = NombreArchivo & ".xls"
        rutamadre = Server.MapPath(".")
        rutamadre = rutamadre.Replace("\Modulo_Reportes", "\Reportes\")

        downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

        Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()
    End Sub

    Private Sub ExportarFotosLibros()

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoUsuarioLogueado
        Dim obj_BL_Libros As New bl_Libros
        Dim int_CodigoAnio As Integer = ddlAnioAcademico.SelectedValue()
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue()
        Dim int_CodigoAula As Integer = ddlAula.SelectedValue()

        Dim ds_Lista As DataSet = obj_BL_Libros.FUN_LIS_Libros(0, "", 0, "", "", 0, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim reporte_html As String = ""
        reporte_html = ExportarReporteFotosLibros_Html(ds_Lista, "")
        Session("Exportaciones_FotosLibrosHtml") = reporte_html
        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionFichaMedica_html();</script>", False)


        'NombreArchivo = Exportacion.ExportarReporteDinamicoCantidadLibrosVendidosPorAula(ds_Lista, int_CodigoGrado, int_CodigoAula, "PrestamoPorAlumno")

        'NombreArchivo = NombreArchivo & ".html"
        'rutamadre = Server.MapPath(".")
        'rutamadre = rutamadre.Replace("\Modulo_Reportes", "\Reportes\")

        'downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

        'Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        'Response.Charset = ""
        'Response.ContentType = "binary/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        'Response.Flush()
        'Response.BinaryWrite(downloadBytes)
        'Response.End()
    End Sub

    Public Shared Function ExportarReporteFotosLibros_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try

            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaFotosLibrosHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaFotosLibrosHtml(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)

        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try

        Return fileReaderPlantilla

    End Function

    Private Shared Function LlenarPlantillaFotosLibrosHtml(ByVal Plantilla As String, ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

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
                        "</tr>[ListaFotosLibros]"

        plantillaColumna = "<td  style='width :200px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>" & _
                        "<table cellpadding='0' cellspacing='0' border='0' style='width: 200px; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 11px; vertical-align: top;'>" & _
                           " <tr><td colspan ='2' style='width :200px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='center' valign='top'>" & _
                                  "<img alt='' src='[IMG_FOTO]'  width ='100px' height='100px' /> </td></tr>" & _
                            "<tr><td style='width :20px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>Título: </td>" & _
                                "<td style='width :180px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>[TITULO]</td></tr>" & _
                            "<tr><td style='width :20px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>Editorial:</td>" & _
                                "<td style='width :180px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>[EDITORIAL]</td></tr>" & _
                            "<tr><td style='width :20px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>ISBN:</td> " & _
                                "<td style='width :180px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>[ISBN]</td></tr>" & _
                        "</table> " & _
                     "</td>[ColumnaFoto]"

        'dsReporte.Tables(0).Rows(int_cont).Item("Titulo")
        'dsReporte.Tables(0).Rows(int_cont).Item("Editorial")
        'dsReporte.Tables(0).Rows(int_cont).Item("ISBN")

        If dsReporte.Tables(0).Rows.Count > 0 Then

            While cont_filas <= dsReporte.Tables(0).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaFotosLibros]", plantillaFila)

                While cont_columnas < 3

                    If cont_filas + cont_columnas <= dsReporte.Tables(0).Rows.Count - 1 Then
                        Plantilla = Plantilla.Replace("[ColumnaFoto]", plantillaColumna)
                        Plantilla = Plantilla.Replace("[IMG_FOTO]", ConfigurationManager.AppSettings("RutaImagenesBancoLibro_Web").ToString() & dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("CodigoLibro").ToString & "/" & dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("RutaPortada").ToString)
                        Plantilla = Plantilla.Replace("[TITULO]", dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("Titulo"))
                        Plantilla = Plantilla.Replace("[EDITORIAL]", dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("Editorial"))
                        Plantilla = Plantilla.Replace("[ISBN]", dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("ISBN"))

                    End If

                    cont_columnas = cont_columnas + 1
                End While

                Plantilla = Plantilla.Replace("[ColumnaFoto]", "")
                cont_columnas = 0
                cont_filas = cont_filas + 3
            End While
            Plantilla = Plantilla.Replace("[ListaFotosLibros]", "")

        Else
            Plantilla = Plantilla.Replace("[ListaFotosLibros]", "<tr><td colspan='3' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>")
        End If

        Return Plantilla
    End Function

    ''' <summary>
    ''' Exporte el listado de la información filtrada en los diferentes formatos indicados.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Exportar()

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoUsuarioLogueado
        Dim obj_BL_Prestamos As New bl_Prestamos
        Dim int_CodigoAnio As Integer = ddlAnioAcademico.SelectedValue()
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue()
        Dim int_CodigoAula As Integer = ddlAula.SelectedValue()

        Dim ds_Lista As DataSet = obj_BL_Prestamos.FUN_REP_PrestamoPorAlumno(int_CodigoAnio, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        NombreArchivo = Exportacion.ExportarReporteBancoLibros(ds_Lista, int_CodigoGrado, "PrestamoPorAlumno")
        NombreArchivo = NombreArchivo & ".xls"
        rutamadre = Server.MapPath(".")
        rutamadre = rutamadre.Replace("\Modulo_Reportes", "\Reportes\")

        downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

        Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()
    End Sub


    ''' <summary>
    ''' Exporte el listado de la información filtrada en los diferentes formatos indicados.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ExportarDevolucion()

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim obj_BL_Prestamos As New bl_Prestamos
        Dim int_CodigoAnio As Integer = ddlAnioAcademico.SelectedValue()
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue()
        Dim int_CodigoAula As Integer = ddlAula.SelectedValue()

        Dim ds_Lista As DataSet = obj_BL_Prestamos.FUN_REP_DevolucionPorAlumno(int_CodigoAnio, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        NombreArchivo = Exportacion.ExportarReporteDevolucion(ds_Lista, int_CodigoGrado, "DevolucionPorAlumno")
        NombreArchivo = NombreArchivo & ".xls"
        rutamadre = Server.MapPath(".")
        rutamadre = rutamadre.Replace("\Modulo_Reportes", "\Reportes\")

        downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

        Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()
    End Sub

    Private Sub ExportarDeudoresBancoLibros()

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim obj_BL_Prestamos As New bl_Prestamos
        Dim int_CodigoAnio As Integer = ddlAnioAcademico.SelectedValue()
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue()
        Dim int_CodigoAula As Integer = ddlAula.SelectedValue()

        Dim ds_Lista As DataSet = obj_BL_Prestamos.FUN_REP_DeudoresBancoLibros(int_CodigoAnio, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        NombreArchivo = Exportacion.ExportarReporteDeudoresBancoLibros(ds_Lista, int_CodigoGrado, ddlAnioAcademico.SelectedItem.ToString, "DeudoresBancoLibros")
        NombreArchivo = NombreArchivo & ".xls"
        rutamadre = Server.MapPath(".")
        rutamadre = rutamadre.Replace("\Modulo_Reportes", "\Reportes\")

        downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

        Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()
    End Sub

    Private Sub cargarCombos()
        cargarComboAnioAcademico()
        cargarComboGrado()
        cargarComboAulas()
    End Sub
    ''' <summary>
    ''' Carga el combo con la lista de Grados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:             Fanny Salinas
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboGrado()
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlGrado, ds_Lista, "Codigo", "Descripcion", True, False)
        'Controles.llenarCombo(ddlGrado2, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Aulas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(CInt(ddlGrado.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAula, ds_Lista, "Codigo", "Descripcion", True, False)
        'Controles.llenarCombo(ddlAula2, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub
    Protected Sub ddlGrado_SelectedIndexChanged()
        Try
            limpiarCombos(ddlAula)
            cargarComboAulas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub


    Private Sub cargarComboAnioAcademico()
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlPeriodoInicio, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlPeriodoFin, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    ''' <summary>
    ''' Método que Limpia los comboBox.
    ''' </summary>
    ''' <param name="combo">Nombre de Combo</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     07/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)
        Controles.limpiarCombo(combo, True, False)
    End Sub

    Private Function validar(ByRef str_Mensaje As String) As Boolean
        Dim result As Boolean = True
        Dim str_alertas As String = ""
        Dim int_check As Integer = 0
        Dim boolFecha As Boolean = True
        Dim boolFechaCompromiso1 As Boolean = True
        Dim boolFechaVencimiento As Boolean = True

        Dim c As Integer = 0

        If pnlAniosUtilidad.Visible = True Then
            If ddlPeriodoInicio.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "PeriodoInicio")
                result = False
            End If
            If ddlPeriodoFin.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "PeriodoFin")
                result = False
            End If
            If ddlPeriodoInicio.SelectedValue <> 0 And ddlPeriodoFin.SelectedValue <> 0 Then
                If CInt(ddlPeriodoInicio.SelectedItem.Text) > CInt(ddlPeriodoFin.SelectedItem.Text) And ddlPeriodoFin.SelectedValue > 0 Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Periodo Inicio no puede ser mayor al Periodo Fin. Periodo Inicio y Fin")
                    result = False
                End If
            End If
        ElseIf pnlPrestamos.Visible = True Then
            If ddlAnioAcademico.SelectedValue = 0 And pnlAniosUtilidad.Visible = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Año")
                result = False
            End If
        End If

        If lstReportes.SelectedValue = "" Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Reporte")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     12/05/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

    Protected Sub MostrarAlertBox(ByVal str_Mensaje As String)

        Me.Master.MostrarMensajeAlert(str_Mensaje)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     14/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub
#End Region

#Region "Filtro 1"

#Region "Eventos"

    Protected Sub btnCerrarFiltro1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ViewState("ListaExportar") = Nothing
            ViewState.Remove("ListaExportar")
            'pnModalFiltro1.Hide()
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#End Region

    'Protected Sub lstReportes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If lstReportes.SelectedIndex = 0 Or lstReportes.SelectedValue = 1 Then
    '        pnlPrestamos.Visible = True
    '        pnlEstadisticaDinamica.Visible = False
    '    ElseIf lstReportes.SelectedValue = 2 Then
    '        pnlPrestamos.Visible = False
    '        pnlEstadisticaDinamica.Visible = True
    '    End If
    'End Sub
End Class
