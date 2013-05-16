
'Fecha 12/07/2012
''hora :03:30pm

Imports System.Data
Imports System.Configuration
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula

Imports SaintGeorgeOnline_Utilities
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices.Marshal
Imports Microsoft.Office.Interop.Excel
Imports System.Runtime.Remoting.Messaging
Imports ClosedXML
Imports ClosedXML.Excel

Delegate Sub generarExcel()

Public Class frmReporteLibretaPrimaria
    ' Public LstMatematicaGb As New List(Of matematica)
    Dim dtOpcionesTipoReportes As New System.Data.DataTable
    Dim dtOpcionesPresentacion As New System.Data.DataTable

    Dim fechaReporte As String = ""

    Dim tipoReporteConsolidadoLibreta As Integer
    Dim tipoPresentacion As Integer
    Dim CodSalon As Integer
    Dim CodBimestre As Integer
    Dim estadoOperacion As Boolean
    Dim estadoProceso As Integer
    Dim alumnosProcesado As Integer
    Dim cantidadAlumnos As Integer
    Dim estadoDetenerProceso As Boolean
    ''

    ''variables globales dentro de la aplicacion 
    Dim tipoReporte As Integer = 0
    Dim presentacion As Integer = 0
    Dim nombreArchivo As String = ""

    Dim salonLast As Integer = 0

    ''

    Dim CodAnio As Integer = 2
    Dim dt_ListaAlumnos As System.Data.DataTable

    Dim int_idioma As Integer = 1
    ' 1 - ES
    ' 2 - EN


#Region "Variables de Posiciones en Excel"

    Private Shared int_HA_Left As Integer = 2 ' Alineación Horizontal Izquierda
    Private Shared int_HA_Center As Integer = 3 ' Alineación Horizontal Centrada
    Private Shared int_HA_Right As Integer = 4 ' Alineación Horizontal Derecha

    Private Shared int_VA_Top As Integer = 1 ' Alineación Vertical Superior
    Private Shared int_VA_Middle As Integer = 2 ' Alineación Vertical Media
    Private Shared int_VA_Bottom As Integer = 3 ' Alineación Vertical Inferior

#End Region

    Private Sub frmReporteLibretaPrimaria_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '' cargar tipo de reporte 


        crearListaOpcionesTipoReporte()
        crearListaOpcionesTipoPresentacion()

        GroupBox1.Visible = False
        lstReportes.Visible = False
        lstPresentacion.Visible = False

        cargarBimestre()
        ocultarLoading()

        cargarComboAnioAcademico()
        cmbPeriodo.SelectedValue = CInt(System.Configuration.ConfigurationManager.AppSettings("codigoPeriodo").ToString)



        'update 17/12/2012

        ''  cargarComboAulas()


        rbidiomaES.Checked = True

        cargarTipoReporte()

    End Sub
    ''secundaria
    Sub cargarComboAulas()
        Dim int_CodigoTrabajador As Integer = 0
        Dim int_TipoNota As Integer = 0
        Dim int_CodigoAnioAcademico As Integer = cmbPeriodo.SelectedValue
        Dim int_CodigoSede As Integer = 0
        Dim int_Estado As Integer = 0
        Dim int_CodigoUsuario As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = 0
        Dim cod_Modulo As Integer = 0
        Dim cod_Opcion As Integer = 0
        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim value As Object = lstPresentacion.SelectedValue

        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
              int_CodigoTrabajador, Constantes.tipoReporteSecundaria, int_CodigoAnioAcademico, int_CodigoSede, _
              int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        cmbSalon.DataSource = ds_Lista.Tables(0)
        cmbSalon.DisplayMember = "DescAulaCompuesta"
        cmbSalon.ValueMember = "Codigo"



    End Sub
    ''Primaria
    Sub cargarComboAulasPrimaria()
        Dim int_CodigoTrabajador As Integer = 0
        Dim int_TipoNota As Integer = 0
        Dim int_CodigoAnioAcademico As Integer = cmbPeriodo.SelectedValue
        Dim int_CodigoSede As Integer = 0
        Dim int_Estado As Integer = 0
        Dim int_CodigoUsuario As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = 0
        Dim cod_Modulo As Integer = 0
        Dim cod_Opcion As Integer = 0
        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim value As Object = lstPresentacion.SelectedValue

        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
              int_CodigoTrabajador, Constantes.tipoReportePrimaria, int_CodigoAnioAcademico, int_CodigoSede, _
              int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        cmbSalon.DataSource = ds_Lista.Tables(0)
        cmbSalon.DisplayMember = "DescAulaCompuesta"
        cmbSalon.ValueMember = "Codigo"



    End Sub
    ''Inicial
    Sub cargarComboAulasInicial()
        Dim int_CodigoTrabajador As Integer = 0
        Dim int_TipoNota As Integer = 0
        Dim int_CodigoAnioAcademico As Integer = cmbPeriodo.SelectedValue
        Dim int_CodigoSede As Integer = 0
        Dim int_Estado As Integer = 0
        Dim int_CodigoUsuario As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = 0
        Dim cod_Modulo As Integer = 0
        Dim cod_Opcion As Integer = 0
        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim value As Object = lstPresentacion.SelectedValue

        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
              int_CodigoTrabajador, Constantes.tipoReporteIncial, int_CodigoAnioAcademico, int_CodigoSede, _
              int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        cmbSalon.DataSource = ds_Lista.Tables(0)
        cmbSalon.DisplayMember = "DescAulaCompuesta"
        cmbSalon.ValueMember = "Codigo"



    End Sub


    Sub ocultarLoading()
        pcbLoading.Visible = False
    End Sub

    Sub crearListaOpcionesTipoReporte()
        Dim dc As DataColumn
        Dim dr As DataRow
        dc = New DataColumn("codTipoReporte", GetType(Integer))
        dtOpcionesTipoReportes.Columns.Add(dc)
        dc = New DataColumn("nombreTipoReporte", GetType(String))
        dtOpcionesTipoReportes.Columns.Add(dc)

        'dr = dtOpcionesTipoReportes.NewRow()
        'dr("codTipoReporte") = 1
        'dr("nombreTipoReporte") = "Consolidado"
        'dtOpcionesTipoReportes.Rows.Add(dr)

        dr = dtOpcionesTipoReportes.NewRow()
        dr("codTipoReporte") = 2
        dr("nombreTipoReporte") = "Libretas"
        dtOpcionesTipoReportes.Rows.Add(dr)

        lstReportes.DataSource = dtOpcionesTipoReportes
        lstReportes.DisplayMember = "nombreTipoReporte"
        lstReportes.ValueMember = "codTipoReporte"

        Try

        Catch ex As Exception

        Finally

        End Try



    End Sub

    Sub cargarBimestre()
        Try

            Dim str_Descripcion As String = ""
            Dim obj_BL_Bimestres As New bl_Bimestres
            Dim int_CodigoTipoUsuario As Integer = 1
            Dim int_CodigoUsuario As Integer = 1
            Dim cod_Modulo As Integer = 1
            Dim cod_Opcion As Integer = 1


            Dim ds_Lista As DataSet = obj_BL_Bimestres.FUN_LIS_Bimestres(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            cmbBimestre.DataSource = ds_Lista.Tables(0)
            cmbBimestre.ValueMember = "Codigo"
            cmbBimestre.DisplayMember = "Descripcion"
        Catch ex As Exception
        Finally

        End Try
    End Sub
    Sub crearListaOpcionesTipoPresentacion()
        Dim dc As DataColumn
        Dim dr As DataRow
        dc = New DataColumn("codTipoPresentacion", GetType(Integer))
        dtOpcionesPresentacion.Columns.Add(dc)
        dc = New DataColumn("nombreTipoPresentacion", GetType(String))
        dtOpcionesPresentacion.Columns.Add(dc)

        dr = dtOpcionesPresentacion.NewRow()
        dr("codTipoPresentacion") = 4
        dr("nombreTipoPresentacion") = "Primaria"
        dtOpcionesPresentacion.Rows.Add(dr)

        dr = dtOpcionesPresentacion.NewRow()
        dr("codTipoPresentacion") = 3
        dr("nombreTipoPresentacion") = "Inicial"
        dtOpcionesPresentacion.Rows.Add(dr)

        dr = dtOpcionesPresentacion.NewRow()
        dr("codTipoPresentacion") = 2
        dr("nombreTipoPresentacion") = "Secundaria"
        dtOpcionesPresentacion.Rows.Add(dr)

        lstPresentacion.DataSource = dtOpcionesPresentacion
        lstPresentacion.DisplayMember = "nombreTipoPresentacion"
        lstPresentacion.ValueMember = "codTipoPresentacion"

        '        [11:39:53 a.m.] Juan José Vento Sevilla: inicial 3
        '[11:39:55 a.m.] Juan José Vento Sevilla: primario 4
        '[11:39:58 a.m.] Juan José Vento Sevilla: secundaria 2

        Try

        Catch ex As Exception

        Finally

        End Try



    End Sub


    Function crearListaPresentacion() As System.Data.DataTable
        Try

        Catch ex As Exception
        Finally

        End Try
        Return Nothing
    End Function
    Public Class alumnos
        Public nombre As String
        Public codigo As String
        Public lstNotas As New List(Of notasConsolidado)

    End Class
    Public Class notasConsolidado
        Public notaPromedio As String
        Public pos As Integer
        Public codAlumno As Integer

    End Class
    ''

  

    Function crearConsolidadoEvaluacion(ByVal codSalon As Integer, ByVal codBimestre As Integer, ByVal nombreArchivo As String) As String
        Try
            Dim abrBimestre As String = ""
            If codBimestre = 1 Then
                abrBimestre = "I"
            End If
            If codBimestre = 2 Then
                abrBimestre = "II"
            End If
            If codBimestre = 3 Then
                abrBimestre = "III"
            End If
            If codBimestre = 4 Then
                abrBimestre = "IV"
            End If
            Dim anio As String = ""


            'nombreArchivo


            anio = Year(Now).ToString()


            Dim rutaApp As String = ""

            rutaApp = Environment.CurrentDirectory()


            Dim rutaPlantillas As String = System.Configuration.ConfigurationManager.AppSettings.Item("ConsolidadoInicial")

            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            ''  <add key="RutaPlantillaLibretaInicial" value="\Plantillas\ExportacionLibreta\libretaInicial.xlsx"/>

            Dim rutaREpositorioTemporales As String = System.Configuration.ConfigurationManager.AppSettings.Item("Temporales").ToString.Trim & rutaTemp & ".xlsx"

            File.Copy(rutaApp & rutaPlantillas, rutaApp & rutaREpositorioTemporales)

            Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim dt As New System.Data.DataTable
            Dim descripcionAula As New System.Data.DataTable

            Dim dst As New DataSet

            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaInicial(codSalon, codBimestre, 1, 1, 1, 1)
            dt = dst.Tables(0)
            descripcionAula = dst.Tables(6)
            Dim descripcionGrado As String = ""
            Dim descAula As String = ""
            'GD_Descripcion	GD_CodigoGrado	GD_Abrev	AU_Descripcion	AAP_CodigoAsignacionAula	GD_DescripcionEspaniol
            'Nursery	1	N	Bunnies	1	Nursery
            descripcionGrado = descripcionAula.Rows(0)("GD_Descripcion").ToString()
            descAula = descripcionAula.Rows(0)("AU_Descripcion").ToString()

            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)


            cantidadAlumnos = lst.Count

            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range
            ''  File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            wbkWorkbook = excel.Workbooks.Open(rutaApp & rutaREpositorioTemporales)


            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()

            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).MergeCells = True

            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).Font.Size = 16
            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).Value = " Consolidado  de Evaluación -" & descripcionGrado & " " & descAula & " (Asignaturas Totales )" & abrBimestre & " Bimestre - " & anio


            CType(excel.ActiveSheet.Cells(1, 12), Range).Value = Now.Day.ToString() & "/" & Month(Now).ToString() & "/" & Year(Now).ToString()
            CType(excel.ActiveSheet.Cells(1, 12), Range).WrapText = True
            CType(excel.ActiveSheet.Cells(2, 12), Range).Value = Hour(Now).ToString() & ":" & Minute(Now).ToString() & ":" & Second(Now).ToString()



            Dim columnas As Integer = 3
            Dim filas As Integer = 8 ''6 ''4 
            Dim nombreCursoTemp As String = ""
            Dim nombreCurso As String = ""
            Dim lstContador As New List(Of Integer)
            Dim lstNombre As New List(Of String)
            For Each opersonaLibreta In lst
                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente
                    nombreCurso = olibretaComponente.nombreCurso
                    If nombreCursoTemp <> olibretaComponente.nombreCurso Then
                        Dim count = From i In opersonaLibreta.lstLibretaComponente _
                                    Where i.nombreCurso = nombreCurso _
                                    Select u = i.lstIndicador.Count
                        Dim s As Integer = count.Sum()
                        lstContador.Add(s)
                        lstNombre.Add(olibretaComponente.nombreCurso)
                        nombreCursoTemp = olibretaComponente.nombreCurso
                    End If
                Next
                Exit For
            Next


            Dim colCont As Integer = 3
            Dim esPrimero As Integer = 0
            Dim finalInicial As Integer = 0
            For indice = 0 To lstContador.Count - 1
                esPrimero += 1
                If esPrimero = 1 Then
                    colCont += lstContador(indice) - 1
                    finalInicial = colCont - lstContador(indice) + 1
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Value = lstNombre(indice).ToUpper()
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                Else
                    colCont += lstContador(indice)
                    finalInicial = colCont - lstContador(indice) + 1
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Value = lstNombre(indice).ToUpper()
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End If
            Next
            Dim sumaTotalColumnas As Integer = lstContador.Sum()
            cantidadAlumnos = lst.Count


            Dim indiceFilas As Integer = 0
            Dim contColumnas As Integer = 2


            Dim filasNombreIndicador As Integer = 8 ''6 ''4
            Dim contCol As Integer = 2

            CType(excel.ActiveSheet.Cells(8, 1), Range).Value = "Nro."
            CType(excel.ActiveSheet.Cells(8, 1), Range).Borders.LineStyle = XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(8, 2), Range).Value = "Apellidos y Nombres "
            CType(excel.ActiveSheet.Cells(8, 2), Range).Borders.LineStyle = XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(8, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


            CType(excel.ActiveSheet.Cells(8, 2), Range).Borders.LineStyle = XlLineStyle.xlContinuous

            For Each opersonaLibreta In lst


                If Not estadoDetenerProceso Then
                    Exit For
                End If

                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente

                    For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador
                        contCol += 1
                        If olibretaIndicador.nombreIndicador.Length > 50 Then
                            CType(excel.ActiveSheet.Cells(filasNombreIndicador, contCol), Range).Value = olibretaIndicador.nombreIndicador.Substring(0, 50)

                        Else
                            CType(excel.ActiveSheet.Cells(filasNombreIndicador, contCol), Range).Value = olibretaIndicador.nombreIndicador
                        End If

                        CType(excel.ActiveSheet.Cells(filasNombreIndicador, contCol), Range).Orientation = 90
                        CType(excel.ActiveSheet.Cells(filasNombreIndicador, contCol), Range).Borders.LineStyle = XlLineStyle.xlContinuous


                    Next



                Next

                Exit For

            Next





            For Each opersonaLibreta In lst
                contColumnas = 2
                filas += 1
                indiceFilas += 1
                CType(excel.ActiveSheet.Cells(filas, 2), Range).Value = opersonaLibreta.nombreAlumno

                CType(excel.ActiveSheet.Cells(filas, 2), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas, 1), Range).Value = indiceFilas.ToString()

                CType(excel.ActiveSheet.Cells(filas, 1), Range).Borders.LineStyle = XlLineStyle.xlContinuous

                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente

                    For Each olibretaIndicador In olibretaComponente.lstIndicador
                        contColumnas += 1
                        CType(excel.ActiveSheet.Cells(filas, contColumnas), Range).Value = olibretaIndicador.notaIndicador.ToUpper()
                        CType(excel.ActiveSheet.Cells(filas, contColumnas), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(filas, contColumnas), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    Next


                Next



            Next
            Dim empiezanContarColumnas As Integer = 2
            Dim empiezanFilas As Integer = 9 ''7 ''5
            Dim cantidadA As Integer = 0
            Dim cantidadB As Integer = 0
            Dim cantidadC As Integer = 0
            Dim contColumnasSumar As Integer = 2

            For colEmp As Integer = 1 To sumaTotalColumnas
                contColumnasSumar += 1
                empiezanFilas = 8 ''6 ''4
                cantidadA = 0
                cantidadB = 0
                cantidadC = 0
                ''
                For indice = 1 To cantidadAlumnos + 1
                    empiezanFilas += 1
                    If CType(excel.ActiveSheet.Cells(empiezanFilas, contColumnasSumar), Range).Value = "A" Then
                        cantidadA += 1
                    End If
                    If CType(excel.ActiveSheet.Cells(empiezanFilas, contColumnasSumar), Range).Value = "B" Then
                        cantidadB += 1
                    End If
                    If CType(excel.ActiveSheet.Cells(empiezanFilas, contColumnasSumar), Range).Value = "C" Then
                        cantidadC += 1
                    End If

                Next
                CType(excel.ActiveSheet.Cells(filas + 1, 2), Range).Value = "TOTAL A:  "
                CType(excel.ActiveSheet.Cells(filas + 1, 2), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(filas + 1, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                CType(excel.ActiveSheet.Cells(filas + 1, 2), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas + 1, 1), Range).Borders.LineStyle = XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(filas + 2, 2), Range).Value = "TOTAL B:  "
                CType(excel.ActiveSheet.Cells(filas + 2, 2), Range).Font.Bold = True

                CType(excel.ActiveSheet.Cells(filas + 2, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                CType(excel.ActiveSheet.Cells(filas + 2, 2), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas + 2, 1), Range).Borders.LineStyle = XlLineStyle.xlContinuous


                CType(excel.ActiveSheet.Cells(filas + 3, 2), Range).Value = "TOTAL C:  "
                CType(excel.ActiveSheet.Cells(filas + 3, 2), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(filas + 3, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                CType(excel.ActiveSheet.Cells(filas + 3, 2), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas + 3, 1), Range).Borders.LineStyle = XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(filas + 1, contColumnasSumar), Range).Value = cantidadA.ToString()
                CType(excel.ActiveSheet.Cells(filas + 1, contColumnasSumar), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(filas + 1, contColumnasSumar), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas + 2, contColumnasSumar), Range).Value = cantidadB.ToString()
                CType(excel.ActiveSheet.Cells(filas + 2, contColumnasSumar), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                CType(excel.ActiveSheet.Cells(filas + 2, contColumnasSumar), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas + 3, contColumnasSumar), Range).Value = cantidadC.ToString()
                CType(excel.ActiveSheet.Cells(filas + 3, contColumnasSumar), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(filas + 3, contColumnasSumar), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                ''
                ''filas


                alumnosProcesado += 1
                If Not estadoDetenerProceso Then
                    Exit For
                End If
            Next

            Dim carpeta As String = ""


            With excel.ActiveSheet.PageSetup
                .LeftHeader = ""
                .CenterHeader = ""
                .RightHeader = ""
                .LeftFooter = ""
                .CenterFooter = ""
                .RightFooter = ""
                .LeftMargin = excel.Application.InchesToPoints(0.7)
                .RightMargin = excel.Application.InchesToPoints(0.7)
                .TopMargin = excel.Application.InchesToPoints(0.75)
                .BottomMargin = excel.Application.InchesToPoints(0.75)
                .HeaderMargin = excel.Application.InchesToPoints(0.3)
                .FooterMargin = excel.Application.InchesToPoints(0.3)
                .PrintHeadings = False
                .PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                .PrintQuality = 600
                .CenterHorizontally = False
                .CenterVertically = False
                .Orientation = 1
                .Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = OrderedDictionary xlDownThenOver
                .BlackAndWhite = False
                .Zoom = False
                .FitToPagesWide = 1
                .FitToPagesTall = False
                '.PrintErrors = xlPrintErrorsDisplayed
                .OddAndEvenPagesHeaderFooter = False
                .DifferentFirstPageHeaderFooter = False
                .ScaleWithDocHeaderFooter = True
                .AlignMarginsHeaderFooter = True
                .EvenPage.LeftHeader.Text = ""
                .EvenPage.CenterHeader.Text = ""
                .EvenPage.RightHeader.Text = ""
                .EvenPage.LeftFooter.Text = ""
                .EvenPage.CenterFooter.Text = ""
                .EvenPage.RightFooter.Text = ""
                .FirstPage.LeftHeader.Text = ""
                .FirstPage.CenterHeader.Text = ""
                .FirstPage.RightHeader.Text = ""
                .FirstPage.LeftFooter.Text = ""
                .FirstPage.CenterFooter.Text = ""
                .FirstPage.RightFooter.Text = ""
            End With



            wbkWorkbook.SaveAs(nombreArchivo)


            'wbkWorkbook.SaveAs(nombreArchivo)

            'Dim nombreArchivoGuardado As String = ""

            'wbkWorkbook.SaveAs(nombreArchivo)
            carpeta = nombreArchivo '' wbkWorkbook.Path()


            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

            Return carpeta
        Catch ex As Exception


        Finally

        End Try

    End Function


    ''IMPRESION DE LIBRETAS DE SEGUNDO BIMESTRE 
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="codSalon"></param>
    ''' <param name="int_bimestre"></param>
    ''' <param name="nombreArchivo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Function crearLibretaInicial1SegundoBimestre(ByVal codSalon As Integer, ByVal int_bimestre As Integer, ByVal nombreArchivo As String) As String
        Dim dt_ausencias As New System.Data.DataTable
        Try
            ''codSalon = 8
            '<add key="LibretaPrimaria"  value="\Plantillas\ExportacionLibreta\libretaPrimaria.xlsx"/>
            ' <add key="LibretaInicial" value="\Plantillas\ExportacionLibreta\libretaInicial.xlsx"/>
            Dim rutaApp As String = ""
            rutaApp = Environment.CurrentDirectory()
            'Dim rutaPlantillas As String = System.Configuration.ConfigurationManager.AppSettings.Item("LibretaPrimaria").ToString.Trim
            'Dim rutaREpositorioTemporales As String = System.Configuration.ConfigurationManager.AppSettings.Item("Temporales").ToString.Trim & rutaTemp & ".xlsx"
            'File.Copy(rutaApp & rutaPlantillas, rutaApp & rutaREpositorioTemporales)
            'wbkWorkbook = excel.Workbooks.Open(rutaApp & rutaREpositorioTemporales)
            Dim abrBimestre As String = ""
            If CodBimestre = 1 Then
                abrBimestre = "I"
            End If
            If CodBimestre = 2 Then
                abrBimestre = "II"
            End If
            If CodBimestre = 3 Then
                abrBimestre = "III"
            End If
            If CodBimestre = 4 Then
                abrBimestre = "IV"
            End If
            Dim rutaPlantillas As String = System.Configuration.ConfigurationManager.AppSettings.Item("LibretaInicial")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            ''  <add key="RutaPlantillaLibretaInicial" value="\Plantillas\ExportacionLibreta\libretaInicial.xlsx"/>
            Dim rutaREpositorioTemporales As String = System.Configuration.ConfigurationManager.AppSettings.Item("Temporales").ToString.Trim & rutaTemp & ".xlsx"
            File.Copy(rutaApp & rutaPlantillas, rutaApp & rutaREpositorioTemporales)

            Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim dt As New System.Data.DataTable
            Dim dst As New DataSet



            ' dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaInicialSegundoBimestre(codSalon, int_bimestre, 1, 1, 1, 1)
            dt = dst.Tables(0)
            dt_ausencias = dst.Tables(3)

            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)

            Dim Int_nueva As Integer = (From h In dt_ListaAlumnos.AsEnumerable() Where h.Field(Of Boolean)("Chk") = True Select h).ToList().Count

            cantidadAlumnos = Int_nueva '' lst.Count

            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range

            wbkWorkbook = excel.Workbooks.Open(rutaApp & rutaREpositorioTemporales)

            'Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")


            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()
            Dim fil As Integer = 8

            Dim acIndicadorDerecha As Integer = 0
            Dim acIndicadorIzquierda As Integer = 0

            Dim filDerecha As Integer = 8

            Dim nombreCursoTemp As String = ""
            Dim contadorIndicador As Integer = 0
            Dim filaCount As Integer = 0
            Dim iniciaIndicador As Integer = 0
            Dim indiceHojas As Integer = 0
            'For iii = 0 To lst.Count - 1
            '    wbkWorkbook.Sheets.Add()
            'Next

            Dim contadorFilasCurso As Integer = 0
            Dim filasInicioComentario As Integer = 0

            Dim boolEstado As Boolean

            Dim filaj42 As Boolean
            Dim filaj81 As Boolean
            Dim filaj125 As Boolean
            Dim filaj191 As Boolean

            For Each opersonaLibreta As personaLibreta In lst
                fil = 8
                filDerecha = 8
                indiceHojas += 1
                oSheet = wbkWorkbook.Worksheets(indiceHojas)
                '' oSheet.Name = opersonaLibreta.codAlumno
                oSheet.Name = opersonaLibreta.codAlumno
                oSheet.Activate()
                oSheet.Select()


                ''
                excel.ActiveWindow.Zoom = 75


                With excel.ActiveSheet.PageSetup
                    .LeftHeader = ""
                    .CenterHeader = ""
                    .RightHeader = ""
                    .LeftFooter = ""
                    .CenterFooter = ""
                    .RightFooter = ""
                    .LeftMargin = excel.Application.InchesToPoints(0.7)
                    .RightMargin = excel.Application.InchesToPoints(0.7)
                    .TopMargin = excel.Application.InchesToPoints(0.75)
                    .BottomMargin = excel.Application.InchesToPoints(0.75)
                    .HeaderMargin = excel.Application.InchesToPoints(0.3)
                    .FooterMargin = excel.Application.InchesToPoints(0.3)
                    .PrintHeadings = False
                    .PrintGridlines = False
                    '.PrintComments = xlPrintNoComments
                    .PrintQuality = 600
                    .CenterHorizontally = False
                    .CenterVertically = False
                    .Orientation = 1
                    .Draft = False
                    '.PaperSize = xlPaperLetter
                    '.FirstPageNumber = xlAutomatic
                    '.Order = OrderedDictionary xlDownThenOver
                    .BlackAndWhite = False
                    .Zoom = False
                    .FitToPagesWide = 1
                    .FitToPagesTall = False
                    '.PrintErrors = xlPrintErrorsDisplayed
                    .OddAndEvenPagesHeaderFooter = False
                    .DifferentFirstPageHeaderFooter = False
                    .ScaleWithDocHeaderFooter = True
                    .AlignMarginsHeaderFooter = True
                    .EvenPage.LeftHeader.Text = ""
                    .EvenPage.CenterHeader.Text = ""
                    .EvenPage.RightHeader.Text = ""
                    .EvenPage.LeftFooter.Text = ""
                    .EvenPage.CenterFooter.Text = ""
                    .EvenPage.RightFooter.Text = ""
                    .FirstPage.LeftHeader.Text = ""
                    .FirstPage.CenterHeader.Text = ""
                    .FirstPage.RightHeader.Text = ""
                    .FirstPage.LeftFooter.Text = ""
                    .FirstPage.CenterFooter.Text = ""
                    .FirstPage.RightFooter.Text = ""
                End With
                ''


                For i As Integer = 0 To dt_ListaAlumnos.Rows.Count - 1
                    If dt_ListaAlumnos.Rows(i).Item(0) = opersonaLibreta.codAlumno Then
                        'If Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item(2)) = False Then
                        boolEstado = Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item(2))
                        Exit For
                        'End If
                    End If
                Next

                If boolEstado = False Then
                    If Not estadoDetenerProceso Then
                        Exit For
                    End If
                    Continue For
                End If



                Dim ci As Globalization.CultureInfo = New Globalization.CultureInfo("en-US")
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 1), Range), CType(excel.ActiveSheet.Cells(8, 3), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 1), Range), CType(excel.ActiveSheet.Cells(8, 3), Range)).Value = "SUBJECT AREAS -TERM " & abrBimestre
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 1), Range), CType(excel.ActiveSheet.Cells(8, 3), Range)).Font.Size = 16
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 1), Range), CType(excel.ActiveSheet.Cells(8, 3), Range)).Font.Bold = True


                ''

                excel.Application.Range(CType(excel.ActiveSheet.Cells(7, 6), Range), CType(excel.ActiveSheet.Cells(7, 7), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(7, 6), Range), CType(excel.ActiveSheet.Cells(7, 7), Range)).Value = "Date : " & Date.Now.ToString("MMMM", ci) & " , " & Now().Year().ToString()
                excel.Application.Range(CType(excel.ActiveSheet.Cells(7, 6), Range), CType(excel.ActiveSheet.Cells(7, 7), Range)).Font.Bold = True

                ''
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 6), Range), CType(excel.ActiveSheet.Cells(8, 7), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 6), Range), CType(excel.ActiveSheet.Cells(8, 7), Range)).Value = "COMMENTS"
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 6), Range), CType(excel.ActiveSheet.Cells(8, 7), Range)).Font.Size = 18

                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 6), Range), CType(excel.ActiveSheet.Cells(8, 7), Range)).Font.Bold = True
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 6), Range), CType(excel.ActiveSheet.Cells(8, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 1), Range), CType(excel.ActiveSheet.Cells(2, 8), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 1), Range), CType(excel.ActiveSheet.Cells(2, 8), Range)).Value = "REPORT CARD"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 1), Range), CType(excel.ActiveSheet.Cells(2, 8), Range)).Font.Size = 20
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 1), Range), CType(excel.ActiveSheet.Cells(2, 8), Range)).Font.Bold = True

                    'excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    CType(excel.ActiveSheet.Cells(3, 3), Range).Value = "NAME"
                    CType(excel.ActiveSheet.Cells(3, 3), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(3, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Value = opersonaLibreta.nombreAlumno
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    CType(excel.ActiveSheet.Cells(4, 3), Range).Value = "CLASS"
                    CType(excel.ActiveSheet.Cells(4, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    CType(excel.ActiveSheet.Cells(4, 3), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    CType(excel.ActiveSheet.Cells(5, 3), Range).Value = "TUTOR"
                    CType(excel.ActiveSheet.Cells(5, 3), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(5, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Value = dst.Tables(2).Rows(0)("nombre").ToString()
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Borders.LineStyle = XlLineStyle.xlContinuous

                    If olibretaComponente.columna Or Not olibretaComponente.columna Then
                        If olibretaComponente.nombreCurso <> nombreCursoTemp Then
                            fil += 1

                            ''
                            Dim cantidadInidcador As Integer = 0
                            Dim listaLibreta As IEnumerable(Of libretaComponente)
                            listaLibreta = (From h In opersonaLibreta.lstLibretaComponente Where h.nombreCurso = olibretaComponente.nombreCurso)
                            Dim sumasIndicador As Integer = 0
                            For Each olibretaComponenteTemp As libretaComponente In listaLibreta
                                sumasIndicador += olibretaComponenteTemp.lstIndicador.Count()
                            Next
                            '
                            Dim espacioSobra As Integer = 0
                            If fil < 43 Then
                                espacioSobra = 42 - fil

                                If espacioSobra < sumasIndicador Then
                                    fil += (espacioSobra) + 2 ' + 12

                                End If

                            ElseIf fil < 81 And fil > 42 Then
                                espacioSobra = 80 - fil

                                If espacioSobra < sumasIndicador Then
                                    fil += (espacioSobra) + 2 '+ 12

                                End If
                            ElseIf fil > 81 And fil < 121 Then
                                espacioSobra = 120 - fil

                                If espacioSobra < sumasIndicador Then
                                    fil += (espacioSobra) + 2 '+ 12

                                End If
                            End If
                            ' excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).RowHeight = 25

                            filasInicioComentario = fil
                            contadorFilasCurso = 0

                            For Each olibretaComponente1 As libretaComponente In opersonaLibreta.lstLibretaComponente
                                ''
                                If olibretaComponente1.nombreCurso = olibretaComponente.nombreCurso Then
                                    For Each olibretaIndicador As libretaIndicador In olibretaComponente1.lstIndicador
                                        contadorFilasCurso += 1
                                    Next
                                End If
                                ''
                            Next



                            ''
                            ''cambio para generar libreta de inicial para segundo bimestre
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filasInicioComentario, 5), Range), CType(excel.ActiveSheet.Cells(filasInicioComentario + contadorFilasCurso, 7), Range)).MergeCells = True


                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filasInicioComentario + contadorFilasCurso, 1), Range), CType(excel.ActiveSheet.Cells(filasInicioComentario + contadorFilasCurso, 3), Range)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous

                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filasInicioComentario, 5), Range), CType(excel.ActiveSheet.Cells(filasInicioComentario + contadorFilasCurso, 7), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filasInicioComentario, 5), Range), CType(excel.ActiveSheet.Cells(filasInicioComentario + contadorFilasCurso, 7), Range)).Value = olibretaComponente.observacionCurso
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filasInicioComentario, 5), Range), CType(excel.ActiveSheet.Cells(filasInicioComentario + contadorFilasCurso, 7), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                            ''
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filasInicioComentario, 5), Range), CType(excel.ActiveSheet.Cells(filasInicioComentario + contadorFilasCurso, 7), Range)).WrapText = True

                            ''acIndicadorIzquierda = olibretaComponente.lstIndicador.Count
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Merge(True)
                            ''excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Value = olibretaComponente.nombreCurso.ToUpper()
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Value = olibretaComponente.nombreCurso.ToUpper()
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop


                            CType(excel.ActiveSheet.Cells(fil, 1), Range).IndentLevel = 3
                            CType(excel.ActiveSheet.Cells(fil, 1), Range).Font.Bold = True
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous
                            ''
                            ''
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Font.Size = 16
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).RowHeight = 25

                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Borders(XlBordersIndex.xlEdgeLeft).LineStyle = XlLineStyle.xlContinuous
                            ''
                            ''
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Borders(XlBordersIndex.xlEdgeRight).LineStyle = XlLineStyle.xlContinuous
                            ''
                            '' excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 2), Range), CType(excel.ActiveSheet.Cells(fil + 5, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous
                        End If

                        contadorFilasCurso = 0
                        For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador
                            ''acumularIndicador
                            contadorFilasCurso += 1
                            fil += 1
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Merge(True)
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Value = olibretaIndicador.nombreIndicador
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).RowHeight = 25
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Font.Size = 8
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Font.Name = "Arial"
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                            'CType(excel.Cells(f, c), Range).Font.Name = "Arial"
                            'CType(excel.Cells(f, c), Range).Font.Size = 9
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).WrapText = True
                            With excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range))
                                .Borders(XlBordersIndex.xlEdgeLeft).LineStyle = XlLineStyle.xlContinuous
                            End With
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).Value = olibretaIndicador.notaIndicador.ToUpper()
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).Font.Bold = True
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                        Next

                        ' excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous



                        ''  excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous

                        nombreCursoTemp = olibretaComponente.nombreCurso
                    Else

                        If olibretaComponente.nombreCurso <> nombreCursoTemp Then
                            filDerecha += 1
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Merge(True)
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Value = olibretaComponente.nombreCurso.ToUpper()



                            'If filDerecha Then



                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Font.Size = 16


                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).RowHeight = 25

                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                            CType(excel.ActiveSheet.Cells(filDerecha, 5), Range).IndentLevel = 3
                            CType(excel.ActiveSheet.Cells(filDerecha, 5), Range).Font.Bold = True
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Borders(XlBordersIndex.xlEdgeLeft).LineStyle = XlLineStyle.xlContinuous
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Borders(XlBordersIndex.xlEdgeRight).LineStyle = XlLineStyle.xlContinuous
                            CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                            '' excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 2), Range), CType(excel.ActiveSheet.Cells(fil + 5, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous
                        End If

                        For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador

                            filDerecha += 1
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Merge(True)
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Value = olibretaIndicador.nombreIndicador
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).RowHeight = 25
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Font.Size = 8
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Font.Name = "Arial"
                            'excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Font.Size = 8
                            'excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Font.Name = "Arial"
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).WrapText = True
                            With excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range))
                                .Borders(XlBordersIndex.xlEdgeLeft).LineStyle = XlLineStyle.xlContinuous
                            End With
                            CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).Value = olibretaIndicador.notaIndicador.ToUpper()
                            CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).Font.Bold = True
                            CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                            CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                            CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).RowHeight = 25
                            ''CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                            '' VerticalAlignment = xlVAlignCenter

                            ''   CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).a = Microsoft.Office.Interop.Excel.Constants.xlTop
                        Next
                        ''excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 10, 5), Range), CType(excel.ActiveSheet.Cells(fil + 10, 7), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous
                        '' excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous
                        nombreCursoTemp = olibretaComponente.nombreCurso
                    End If
                Next

                ''cambiado

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous

                ''


                If True Then
                    '    filDerecha -= 2

                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 4, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 4, 3), Range)).Merge(True)
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 4, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 4, 3), Range)).Value = "CONDUCTA"
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 4, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 4, 3), Range)).Borders.LineStyle = XlLineStyle.xlContinuous

                    '    CType(excel.ActiveSheet.Cells(filDerecha + 4, 4), Range).Value = opersonaLibreta.conductaBimestral
                    '    CType(excel.ActiveSheet.Cells(filDerecha + 4, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


                    '    CType(excel.ActiveSheet.Cells(filDerecha + 4, 4), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    '    ''
                    '    'excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 5, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 5, 3), Range)).Merge(True)
                    '    'excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 5, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 5, 3), Range)).Value = "Comentario de la tutora"
                    '    'excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 5, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 5, 3), Range)).Font.Bold = True

                    '    'For Each olibretaComponenteT As libretaComponente In opersonaLibreta.lstLibretaComponente
                    '    '    If olibretaComponenteT.observacionCurso <> "" And olibretaComponenteT.observacionCurso.Trim().Length >= 5 Then
                    '    '        excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 7, 6), Range)).MergeCells = True
                    '    '        excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 7, 6), Range)).Value = olibretaComponenteT.observacionCurso
                    '    '        excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 7, 6), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    '    '        filDerecha += 2
                    '    '        Exit For
                    '    '    End If
                    '    'Next

                    '    ''

                    '    '' excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous
                    '    '' excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous
                    '    filDerecha += 8
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 1, 4), Range)).MergeCells = True
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 1, 4), Range)).Value = "ABSENCES"
                    '    ''
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 1, 4), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    '    CType(excel.ActiveSheet.Cells(filDerecha, 5), Range).Value = "Justified"
                    '    CType(excel.ActiveSheet.Cells(filDerecha, 5), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    '    CType(excel.ActiveSheet.Cells(filDerecha, 6), Range).Value = "Not justified"
                    '    CType(excel.ActiveSheet.Cells(filDerecha, 6), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    '    CType(excel.ActiveSheet.Cells(filDerecha + 1, 5), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    '    CType(excel.ActiveSheet.Cells(filDerecha + 1, 6), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 4), Range)).Merge(True)
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 4), Range)).Value = "Lateness"
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 4), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 6), Range)).Merge(True)
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 6), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    '    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                    '        If opersonaLibreta.codAlumno = filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then
                    '            CType(excel.ActiveSheet.Cells(filDerecha + 1, 5), Range).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                    '            CType(excel.ActiveSheet.Cells(filDerecha + 1, 5), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    '            CType(excel.ActiveSheet.Cells(filDerecha + 1, 6), Range).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                    '            CType(excel.ActiveSheet.Cells(filDerecha + 1, 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    '            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 6), Range)).Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                    '            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    '            Exit For
                    '        End If
                    '        'CodigoAlumno	NombreAlumno	1TardanzaJustificada	1TardanzaSinJustificar	1FaltaJustificada	1FaltaSinJustificar	2TardanzaJustificada	2TardanzaSinJustificar	2FaltaJustificada	2FaltaSinJustificar	3TardanzaJustificada	3TardanzaSinJustificar	3FaltaJustificada	3FaltaSinJustificar	4TardanzaJustificada	4TardanzaSinJustificar	4FaltaJustificada	4FaltaSinJustificar
                    '        '20090135	INGA BARRERA, Enzo Jesús	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0
                    '    Next
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 6, 3), Range)).Merge(True)
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 6, 3), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    '    ''
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 9, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 9, 3), Range)).Merge(True)
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 9, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 9, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 9, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 9, 3), Range)).Value = "TUTORA"

                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 9, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 9, 3), Range)).Font.Bold = True
                    '    ''.Font.Bold = True

                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 9, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 9, 3), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 9, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 9, 7), Range)).Merge(True)
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 9, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 9, 7), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 9, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 9, 7), Range)).Value = "PARENTS"


                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 9, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 9, 7), Range)).Font.Bold = True
                    '    ''.Font.Bold = True
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 9, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 9, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    '    ''
                    '    ''
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 3), Range)).Merge(True)
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 3), Range)).Value = "AD:Archieved with Distinction / Actuación destacada"
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 11, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 11, 3), Range)).Merge(True)
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 11, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 11, 3), Range)).Value = "A:Archieved  / Aprobado"
                    '    ''
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 7), Range)).Merge(True)
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 7), Range)).Value = "B:Needs inprovement/ Bases en  Proceso / Desaprobado"
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 11, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 11, 7), Range)).Merge(True)
                    '    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 11, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 11, 7), Range)).Value = "Initial Stage  / Calificado Insuficiente / Desaprobado"
                    '    ''


                    'Else
                    fil -= 2
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 4, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 4, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 3), Range)).Value = "CONDUCTA"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 4, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 3), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(fil + 4, 4), Range).Value = opersonaLibreta.conductaBimestral

                    CType(excel.ActiveSheet.Cells(fil + 4, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    CType(excel.ActiveSheet.Cells(fil + 4, 4), Range).Borders.LineStyle = XlLineStyle.xlContinuous





                    fil += 6  ' fil += 8 'modificado
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 1, 4), Range)).MergeCells = True

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 1, 4), Range)).Value = "ABSENCES"

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 1, 4), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(fil, 5), Range).Value = "Justified"
                    CType(excel.ActiveSheet.Cells(fil, 5), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(fil, 6), Range).Value = "Not justified"
                    CType(excel.ActiveSheet.Cells(fil, 6), Range).Borders.LineStyle = XlLineStyle.xlContinuous

                    CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(fil + 1, 6), Range).Borders.LineStyle = XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 1), Range), CType(excel.ActiveSheet.Cells(fil + 2, 4), Range)).Merge(True)

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 1), Range), CType(excel.ActiveSheet.Cells(fil + 2, 4), Range)).Value = "Lateness"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 1), Range), CType(excel.ActiveSheet.Cells(fil + 2, 4), Range)).Borders.LineStyle = XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 5), Range), CType(excel.ActiveSheet.Cells(fil + 2, 6), Range)).Merge(True)

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 5), Range), CType(excel.ActiveSheet.Cells(fil + 2, 6), Range)).Borders.LineStyle = XlLineStyle.xlContinuous

                    'For Each filt As System.Data.DataRow In dt_ausencias.Rows


                    '    If filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then

                    '        CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Value = "p1" '' Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                    '        CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).Value = "p1" '' Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                    '        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 4), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range)).Value = "p1" '' Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                    '        CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Value = "prueba"
                    '        Exit For

                    '    End If




                    'Next



                    'excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 8), Range)).MergeCells = True
                    fil -= 2 ''modificado filas 

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 7, 2), Range), CType(excel.ActiveSheet.Cells(fil + 7, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 7, 2), Range), CType(excel.ActiveSheet.Cells(fil + 7, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 7, 2), Range), CType(excel.ActiveSheet.Cells(fil + 7, 3), Range)).Value = "TUTORA"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 7, 2), Range), CType(excel.ActiveSheet.Cells(fil + 7, 3), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 7, 5), Range), CType(excel.ActiveSheet.Cells(fil + 7, 7), Range)).Merge(True)


                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 7, 5), Range), CType(excel.ActiveSheet.Cells(fil + 7, 7), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 7, 5), Range), CType(excel.ActiveSheet.Cells(fil + 7, 7), Range)).Value = "PARENTS"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 7, 5), Range), CType(excel.ActiveSheet.Cells(fil + 7, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


                    ''
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 8, 2), Range), CType(excel.ActiveSheet.Cells(fil + 8, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 8, 2), Range), CType(excel.ActiveSheet.Cells(fil + 8, 3), Range)).Value = "AD:Archieved with Distinction / Actuación destacada"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 9, 2), Range), CType(excel.ActiveSheet.Cells(fil + 9, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 9, 2), Range), CType(excel.ActiveSheet.Cells(fil + 9, 3), Range)).Value = "A:Archieved  / Aprobado"
                    ''
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 8, 5), Range), CType(excel.ActiveSheet.Cells(fil + 8, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 8, 5), Range), CType(excel.ActiveSheet.Cells(fil + 8, 7), Range)).Value = "B:Needs inprovement/ Bases en  Proceso / Desaprobado"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 9, 5), Range), CType(excel.ActiveSheet.Cells(fil + 9, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 9, 5), Range), CType(excel.ActiveSheet.Cells(fil + 9, 7), Range)).Value = "Initial Stage  / Calificado Insuficiente / Desaprobado"
                    ''

                End If


                If filDerecha > fil Then
                    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                        If opersonaLibreta.codAlumno = filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then
                            CType(excel.ActiveSheet.Cells(filDerecha + 1, 5), Range).Value = Convert.ToInt32(filt("2FaltaJustificada").ToString()) 'Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                            CType(excel.ActiveSheet.Cells(filDerecha + 1, 5), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                            CType(excel.ActiveSheet.Cells(filDerecha + 1, 6), Range).Value = Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) ' Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())

                            CType(excel.ActiveSheet.Cells(filDerecha + 1, 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 6), Range)).Value = Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) ' Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter



                            Exit For
                        End If
                    Next
                Else
                    fil += 2 ''modificado filas 
                    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                        If filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then

                            CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).Value = Convert.ToInt32(filt("2FaltaJustificada").ToString()) ' Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                            CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                            CType(excel.ActiveSheet.Cells(fil + 1, 6), Range).Value = Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) ' Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                            CType(excel.ActiveSheet.Cells(fil + 1, 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 5), Range), CType(excel.ActiveSheet.Cells(fil + 2, 6), Range)).Value = Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) 'Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 5), Range), CType(excel.ActiveSheet.Cells(fil + 2, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                            '' CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Value = "prueba"
                            Exit For
                        End If
                    Next
                End If



                excel.ActiveSheet.Cells.Columns(4).ColumnWidth = 4
                excel.ActiveSheet.Cells.Columns(8).ColumnWidth = 4
                excel.ActiveSheet.Cells.Columns(3).ColumnWidth = 24
                excel.ActiveSheet.Cells.Columns(7).ColumnWidth = 30


                excel.ActiveSheet.Cells.Columns(5).ColumnWidth = 8
                excel.ActiveSheet.Cells.Columns(6).ColumnWidth = 13

                excel.ActiveSheet.Cells.Columns(3).ColumnWidth = 41





                excel.ActiveWindow.Zoom = 75




                alumnosProcesado += 1

                If Not estadoDetenerProceso Then

                    Exit For
                End If

            Next
            Dim rutaExplorer As String = ""



            ''


            '.LeftMargin = Application.InchesToPoints(0.7)
            '.RightMargin = Application.InchesToPoints(0.7)
            '.TopMargin = Application.InchesToPoints(0.75)
            '.BottomMargin = Application.InchesToPoints(0.75)
            '.HeaderMargin = Application.InchesToPoints(0.3)
            '.FooterMargin = Application.InchesToPoints(0.3)

            With excel.ActiveSheet.PageSetup
                .LeftHeader = ""
                .CenterHeader = ""
                .RightHeader = ""
                .LeftFooter = ""
                .CenterFooter = ""
                .RightFooter = ""
                .LeftMargin = excel.Application.InchesToPoints(0.7)
                .RightMargin = excel.Application.InchesToPoints(0.7)
                .TopMargin = excel.Application.InchesToPoints(0.75)
                .BottomMargin = excel.Application.InchesToPoints(0.75)
                .HeaderMargin = excel.Application.InchesToPoints(0.3)
                .FooterMargin = excel.Application.InchesToPoints(0.3)
                .PrintHeadings = False
                .PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                .PrintQuality = 600
                .CenterHorizontally = False
                .CenterVertically = False
                .Orientation = 1
                .Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = OrderedDictionary xlDownThenOver
                .BlackAndWhite = False
                .Zoom = False
                .FitToPagesWide = 1
                .FitToPagesTall = False
                '.PrintErrors = xlPrintErrorsDisplayed
                .OddAndEvenPagesHeaderFooter = False
                .DifferentFirstPageHeaderFooter = False
                .ScaleWithDocHeaderFooter = True
                .AlignMarginsHeaderFooter = True
                .EvenPage.LeftHeader.Text = ""
                .EvenPage.CenterHeader.Text = ""
                .EvenPage.RightHeader.Text = ""
                .EvenPage.LeftFooter.Text = ""
                .EvenPage.CenterFooter.Text = ""
                .EvenPage.RightFooter.Text = ""
                .FirstPage.LeftHeader.Text = ""
                .FirstPage.CenterHeader.Text = ""
                .FirstPage.RightHeader.Text = ""
                .FirstPage.LeftFooter.Text = ""
                .FirstPage.CenterFooter.Text = ""
                .FirstPage.RightFooter.Text = ""
            End With

            '.CenterHeader = ""
            '.RightHeader = ""
            '.LeftFooter = ""
            '.CenterFooter = ""
            '.RightFooter = ""
            '.LeftMargin = Application.InchesToPoints(0.7)
            '.RightMargin = Application.InchesToPoints(0.7)
            '.TopMargin = Application.InchesToPoints(0.75)
            '.BottomMargin = Application.InchesToPoints(0.75)
            '.HeaderMargin = Application.InchesToPoints(0.3)
            '.FooterMargin = Application.InchesToPoints(0.3)
            '.PrintHeadings = False
            '.PrintGridlines = False
            '.PrintComments = xlPrintNoComments
            '.PrintQuality = 600
            '.CenterHorizontally = False
            '.CenterVertically = False
            '.Orientation = xlPortrait
            '.Draft = False
            '.PaperSize = xlPaperLetter
            '.FirstPageNumber = xlAutomatic
            '.Order = xlDownThenOver
            '.BlackAndWhite = False
            '.Zoom = False
            '.FitToPagesWide = 1
            '.FitToPagesTall = 0
            '.PrintErrors = xlPrintErrorsDisplayed
            '.OddAndEvenPagesHeaderFooter = False
            '.DifferentFirstPageHeaderFooter = False
            '.ScaleWithDocHeaderFooter = True
            '.AlignMarginsHeaderFooter = True
            '.EvenPage.LeftHeader.Text = ""
            '.EvenPage.CenterHeader.Text = ""
            '.EvenPage.RightHeader.Text = ""
            '.EvenPage.LeftFooter.Text = ""
            '.EvenPage.CenterFooter.Text = ""
            '.EvenPage.RightFooter.Text = ""
            '.FirstPage.LeftHeader.Text = ""
            '.FirstPage.CenterHeader.Text = ""
            '.FirstPage.RightHeader.Text = ""
            '.FirstPage.LeftFooter.Text = ""
            '.FirstPage.CenterFooter.Text = ""
            '.FirstPage.RightFooter.Text = ""


            ''
            wbkWorkbook.SaveAs(nombreArchivo)
            estadoOperacion = True
            rutaExplorer = nombreArchivo '' wbkWorkbook.Path()

            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

            Return rutaExplorer

        Catch ex As Exception

        Finally


        End Try
    End Function

    ''
    Function crearListaPersonas1(ByVal dt As System.Data.DataTable) As List(Of alumnos)
        Dim listasAlumno As New List(Of alumnos)
        Dim oalumnos As alumnos
        'nombres	RNBL_NotaFinalBimestre	curso	codCurso	pos	AL_CodigoAlumno
        Dim codaL As Integer
        Dim onotasConsolidado As notasConsolidado

        For Each fil As System.Data.DataRow In dt.Rows
            oalumnos = New alumnos
            oalumnos.codigo = Convert.ToInt32(fil("AL_CodigoAlumno").ToString())
            oalumnos.nombre = fil("nombres").ToString()

            If oalumnos.codigo <> codaL Then
                For Each fi As System.Data.DataRow In dt.Rows
                    onotasConsolidado = New notasConsolidado
                    onotasConsolidado.codAlumno = Convert.ToInt32(fi("AL_CodigoAlumno").ToString())
                    onotasConsolidado.notaPromedio = fi("RNBL_NotaFinalBimestre").ToString()
                    onotasConsolidado.pos = Convert.ToInt32(fi("pos").ToString())
                    If onotasConsolidado.codAlumno = oalumnos.codigo Then
                        oalumnos.lstNotas.Add(onotasConsolidado)
                    End If
                Next

                listasAlumno.Add(oalumnos)
            End If
            codaL = oalumnos.codigo

        Next
        Return listasAlumno

    End Function
    ''
    ''
    Function crearLibretaPrimaria1(ByVal codigoAula As Integer, ByVal int_bimestre As Integer, ByVal nombreArchivo As String) As String
        '     <add key="LibretaPrimaria"  value="\Plantillas\ExportacionLibreta\libretaPrimaria.xlsx"/>
        '<add key="LibretaInicial" value="\Plantillas\ExportacionLibreta\libretaInicial.xlsx"/>

        ''   <add key="Temporales" value="\Temporales\"/>
        Dim rutaApp As String = ""

        rutaApp = Environment.CurrentDirectory()

        Dim rutaPlantillas As String = System.Configuration.ConfigurationManager.AppSettings.Item("LibretaPrimaria").ToString.Trim

        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

        Dim tb_Asistencias As New System.Data.DataTable '' extraer las inasistencias del alumno
        Dim tb_demeritos As New System.Data.DataTable '' extrar los meritos y demeritos del alumno 
        Dim tb_conducta As New System.Data.DataTable

        Dim oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Try
            ''
            Dim dt As New System.Data.DataTable
            Dim dst As New DataSet
            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaPrimaria(codigoAula, int_bimestre, 1, 1, 1, 1)
            dt = dst.Tables(0)
            ''
            tb_Asistencias = dst.Tables(3)
            tb_demeritos = dst.Tables(4)
            tb_conducta = dst.Tables(5)

            ''
            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)



            '  lst.OrderBy(Function(iperosona) iperosona.nombreAlumno)
            'fun=(int m)={return 4}
            ' fun()
            'fun
            'dt_ListaAlumnos.Rows(i).Item(0) = lst(iPerosona).codAlumno Then
            ''If Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item(2)) = False Then


            Dim Int_nueva As Integer = (From h In dt_ListaAlumnos.AsEnumerable() Where h.Field(Of Boolean)("Chk") = True Select h).ToList().Count

            cantidadAlumnos = Int_nueva 'lst.Count

            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range


            Dim componenteTemp As libretaComponente


            ''   <add key="Temporales" value="\Temporales\"/>
            Dim rutaREpositorioTemporales As String = System.Configuration.ConfigurationManager.AppSettings.Item("Temporales").ToString.Trim & rutaTemp & ".xlsx"

            File.Copy(rutaApp & rutaPlantillas, rutaApp & rutaREpositorioTemporales)

            wbkWorkbook = excel.Workbooks.Open(rutaApp & rutaREpositorioTemporales)

            'Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = True 'XlSheetVisibility.xlSheetVisible

            wshWorksheet.Activate()
            Dim fil As Integer = 8
            Dim nombreCursoTemp As String = ""
            Dim contadorIndicador As Integer = 0
            Dim filaCount As Integer = 0
            Dim iniciaIndicador As Integer = 0
            Dim indiceHojas As Integer = 0
            'For iii = 0 To lst.Count - 1
            '    wbkWorkbook.Sheets.Add()
            'Next


            ''
            Dim abrBimestre As String = ""
            If CodBimestre = 1 Then
                abrBimestre = "I"
            End If
            If CodBimestre = 2 Then
                abrBimestre = "II"
            End If
            If CodBimestre = 3 Then
                abrBimestre = "III"
            End If
            If CodBimestre = 4 Then
                abrBimestre = "IV"
            End If


            Dim ci As Globalization.CultureInfo = New Globalization.CultureInfo("en-US")


            Date.Now.ToString("MMMM", ci)
            ''


            Dim agregoFilas As Integer = 0
            Dim agregoFilas1 As Integer = 0

            Dim boolEstado As Boolean = True
            For iPerosona As Integer = 0 To lst.Count - 1

                For i As Integer = 0 To dt_ListaAlumnos.Rows.Count - 1
                    If dt_ListaAlumnos.Rows(i).Item(0) = lst(iPerosona).codAlumno Then
                        'If Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item(2)) = False Then
                        boolEstado = Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item(2))
                        Exit For
                        'End If
                    End If
                Next

                If boolEstado = False Then
                    If Not estadoDetenerProceso Then
                        Exit For
                    End If
                    Continue For
                End If



                agregoFilas = 0
                agregoFilas1 = 0
                fil = 8
                indiceHojas += 1
                oSheet = wbkWorkbook.Worksheets(indiceHojas)
                '' oSheets = wbkWorkbook.Worksheets
                oSheet.Activate()
                oSheet.Select()

                '' oSheet.Name = ""
                oSheet.Name = lst(iPerosona).codAlumno


                ''

                excel.ActiveWindow.Zoom = 75


                With excel.ActiveSheet.PageSetup
                    .LeftHeader = ""
                    .CenterHeader = ""
                    .RightHeader = ""
                    .LeftFooter = ""
                    .CenterFooter = ""
                    .RightFooter = ""
                    .LeftMargin = excel.Application.InchesToPoints(0.7)
                    .RightMargin = excel.Application.InchesToPoints(0.7)
                    .TopMargin = excel.Application.InchesToPoints(0.75)
                    .BottomMargin = excel.Application.InchesToPoints(0.75)
                    .HeaderMargin = excel.Application.InchesToPoints(0.3)
                    .FooterMargin = excel.Application.InchesToPoints(0.3)
                    .PrintHeadings = False
                    .PrintGridlines = False
                    '.PrintComments = xlPrintNoComments
                    .PrintQuality = 600
                    .CenterHorizontally = False
                    .CenterVertically = False
                    .Orientation = 1
                    .Draft = False
                    '.PaperSize = xlPaperLetter
                    '.FirstPageNumber = xlAutomatic
                    '.Order = OrderedDictionary xlDownThenOver
                    .BlackAndWhite = False
                    .Zoom = False
                    .FitToPagesWide = 1
                    .FitToPagesTall = False
                    '.PrintErrors = xlPrintErrorsDisplayed
                    .OddAndEvenPagesHeaderFooter = False
                    .DifferentFirstPageHeaderFooter = False
                    .ScaleWithDocHeaderFooter = True
                    .AlignMarginsHeaderFooter = True
                    .EvenPage.LeftHeader.Text = ""
                    .EvenPage.CenterHeader.Text = ""
                    .EvenPage.RightHeader.Text = ""
                    .EvenPage.LeftFooter.Text = ""
                    .EvenPage.CenterFooter.Text = ""
                    .EvenPage.RightFooter.Text = ""
                    .FirstPage.LeftHeader.Text = ""
                    .FirstPage.CenterHeader.Text = ""
                    .FirstPage.RightHeader.Text = ""
                    .FirstPage.LeftFooter.Text = ""
                    .FirstPage.CenterFooter.Text = ""
                    .FirstPage.RightFooter.Text = ""
                End With
                ''


                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 1), Range), CType(excel.ActiveSheet.Cells(8, 2), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 1), Range), CType(excel.ActiveSheet.Cells(8, 2), Range)).Value = "SUBJECT AREAS -" & Now().Year().ToString()
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 1), Range), CType(excel.ActiveSheet.Cells(8, 2), Range)).Font.Bold = True
                excel.Application.Range(CType(excel.ActiveSheet.Cells(8, 1), Range), CType(excel.ActiveSheet.Cells(8, 2), Range)).Font.Size = 16

                CType(excel.ActiveSheet.Cells(8, 11), Range).Value = "TERM " & abrBimestre

                CType(excel.ActiveSheet.Cells(8, 11), Range).Font.Bold = True

                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 9), Range), CType(excel.ActiveSheet.Cells(4, 10), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 9), Range), CType(excel.ActiveSheet.Cells(4, 10), Range)).Value = "Date : " & Date.Now.ToString("MMMM", ci) & " " & Date.Now.Day.ToString() & " ," & Date.Now.Year().ToString
                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 9), Range), CType(excel.ActiveSheet.Cells(4, 10), Range)).Font.Bold = True

                excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Value = "REPORT CARD"

                excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Font.Bold = True

                'excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                CType(excel.ActiveSheet.Cells(3, 3), Range).Value = "NAME"
                CType(excel.ActiveSheet.Cells(3, 3), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(3, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Merge(True)

                excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Borders.LineStyle = XlLineStyle.xlContinuous

                excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Value = lst(iPerosona).nombreAlumno

                excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                CType(excel.ActiveSheet.Cells(4, 3), Range).Value = "CLASS"
                CType(excel.ActiveSheet.Cells(4, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                CType(excel.ActiveSheet.Cells(4, 3), Range).Borders.LineStyle = XlLineStyle.xlContinuous

                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                CType(excel.ActiveSheet.Cells(5, 3), Range).Value = "TUTOR"
                CType(excel.ActiveSheet.Cells(5, 3), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(5, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Value = dst.Tables(2).Rows(0)("nombre").ToString()

                excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Borders.LineStyle = XlLineStyle.xlContinuous


                For Each olibretaComponente As libretaComponente In lst(iPerosona).lstLibretaComponente
                    fil += 1
                    'If fil = 67 And agregoFilas = 0 Then
                    '    fil += 10
                    '    agregoFilas = 1
                    'End If

                    'If fil = 135 And agregoFilas1 = 0 Then
                    '    fil += 18
                    '    agregoFilas1 = 1
                    'End If


                    If nombreCursoTemp <> olibretaComponente.nombreCurso Then
                        Dim nombreTemp As String = ""
                        nombreTemp = olibretaComponente.nombreCurso

                        Dim count = From o In lst(iPerosona).lstLibretaComponente Where o.nombreCurso = nombreTemp _
                            Select o.lstIndicador
                        Dim cntListas As Integer = 0
                        'cntListas = count.Count()

                        Dim contador As Integer = 0

                        For Each el In count
                            contador += el.Count() + 1
                        Next

                        Dim cnt As Integer = contador + 2


                        If Not fil + cnt <= 75 And fil < 150 And agregoFilas = 0 Then
                            Dim diferencias As Integer = 0
                            diferencias = 77 - fil
                            fil += diferencias
                            agregoFilas = 1

                        End If
                        If Not fil + cnt <= 150 And fil > 75 And agregoFilas1 = 0 Then
                            Dim diferencias As Integer = 0
                            diferencias = 150 - fil
                            fil += diferencias + 3
                            agregoFilas1 = 1

                        End If

                        filaCount = fil + 1
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(filaCount, 6), Range)).MergeCells = True
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(filaCount, 6), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(filaCount, 6), Range)).Value = olibretaComponente.nombreCurso.ToUpper() '' & " nombre alumno " & opersonaLibreta.nombreAlumno
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(filaCount, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(filaCount, 6), Range)).Font.Bold = True
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).Merge(True)
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).Value = "PERFORMANCE"
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil, 11), Range).Value = "AVERAGE"
                        CType(excel.ActiveSheet.Cells(fil, 11), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                        CType(excel.ActiveSheet.Cells(fil, 11), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        fil = filaCount
                        CType(excel.ActiveSheet.Cells(fil, 7), Range).Value = "C"
                        CType(excel.ActiveSheet.Cells(fil, 7), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil, 7), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil, 8), Range).Value = "B"
                        CType(excel.ActiveSheet.Cells(fil, 8), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil, 8), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil, 9), Range).Value = "A"
                        CType(excel.ActiveSheet.Cells(fil, 9), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil, 9), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil, 10), Range).Value = "AD"
                        CType(excel.ActiveSheet.Cells(fil, 10), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil, 10), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil, 11), Range).Value = olibretaComponente.promedioComponente.ToUpper()
                        CType(excel.ActiveSheet.Cells(fil, 11), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil, 11), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                        fil += 1
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).Merge(True)
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    End If

                    'If olibretaComponente.nombreComponente = "READING" Then
                    '    fil += 5
                    'End If


                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Value = olibretaComponente.nombreComponente.ToUpper()

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).WrapText = True

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Font.Bold = True

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    CType(excel.ActiveSheet.Cells(fil, 11), Range).Value = olibretaComponente.notaComponente.ToUpper()

                    CType(excel.ActiveSheet.Cells(fil, 11), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    CType(excel.ActiveSheet.Cells(fil, 11), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                    iniciaIndicador = fil + 1
                    contadorIndicador = 0


                    For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador

                        fil += 1
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Merge(True)

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Value = olibretaIndicador.nombreIndicador

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).WrapText = True

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).IndentLevel = 2
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                        If olibretaIndicador.notaIndicador.ToUpper() = "C" Then
                            CType(excel.ActiveSheet.Cells(fil, 7), Range).Value = " * "
                            CType(excel.ActiveSheet.Cells(fil, 7), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "B" Then
                            CType(excel.ActiveSheet.Cells(fil, 8), Range).Value = " * "
                            CType(excel.ActiveSheet.Cells(fil, 8), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "A" Then
                            CType(excel.ActiveSheet.Cells(fil, 9), Range).Value = " * "
                            CType(excel.ActiveSheet.Cells(fil, 9), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "AD" Then
                            CType(excel.ActiveSheet.Cells(fil, 10), Range).Value = " * "
                            CType(excel.ActiveSheet.Cells(fil, 10), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        End If
                        contadorIndicador += 1


                        For ii As Integer = 7 To 10
                            CType(excel.ActiveSheet.Cells(fil, ii), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        Next
                    Next

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(iniciaIndicador, 11), Range), CType(excel.ActiveSheet.Cells(iniciaIndicador + contadorIndicador - 1, 11), Range)).MergeCells = True
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(iniciaIndicador, 11), Range), CType(excel.ActiveSheet.Cells(iniciaIndicador + contadorIndicador - 1, 11), Range)).Borders.LineStyle = XlLineStyle.xlContinuous




                    nombreCursoTemp = olibretaComponente.nombreCurso
                Next



                ''creando inasistencias del alumno 


                '' CType(excel.ActiveSheet.Cells(iniciaIndicador, 8), Range).
                ''

                fil += 3
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Borders.LineStyle = XlLineStyle.xlContinuous

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Value = "ABSENCES"
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Borders.LineStyle = XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(fil + 1, 1), Range).Value = ""
                CType(excel.ActiveSheet.Cells(fil + 1, 1), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 2), Range).Value = "Term I"
                CType(excel.ActiveSheet.Cells(fil + 1, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                CType(excel.ActiveSheet.Cells(fil + 1, 2), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 3), Range).Value = "Term II"
                CType(excel.ActiveSheet.Cells(fil + 1, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(fil + 1, 3), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Value = "Term III"
                CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).Value = "Term IV"
                CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 6), Range).Value = "Term Total / Average"
                CType(excel.ActiveSheet.Cells(fil + 1, 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(fil + 1, 6), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                CType(excel.ActiveSheet.Cells(fil + 2, 1), Range).Value = "Justified"
                CType(excel.ActiveSheet.Cells(fil + 2, 1), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(fil + 2, 1), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 3, 1), Range).Value = "Unjustified"
                CType(excel.ActiveSheet.Cells(fil + 3, 1), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(fil + 3, 1), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 4, 1), Range).Value = "Lateness"
                CType(excel.ActiveSheet.Cells(fil + 4, 1), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(fil + 4, 1), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 4, 1), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                For Each filaTb As System.Data.DataRow In tb_Asistencias.Rows
                    If Convert.ToInt32(filaTb("CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                        CType(excel.ActiveSheet.Cells(fil + 2, 2), Range).Value = filaTb("1FaltaJustificada").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 2, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 2, 2), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 2), Range).Value = filaTb("1FaltaSinJustificar").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 3, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 3, 2), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 4, 2), Range).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 4, 2), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 4, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 2, 3), Range).Value = filaTb("2FaltaJustificada").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 2, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 2, 3), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 3), Range).Value = filaTb("2FaltaSinJustificar").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 3, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 3, 3), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 4, 3), Range).Value = Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 4, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 4, 3), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 2, 4), Range).Value = filaTb("3FaltaJustificada").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 2, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 2, 4), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 4), Range).Value = filaTb("3FaltaSinJustificar").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 3, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 3, 4), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 4, 4), Range).Value = Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 4, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 4, 4), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 2, 5), Range).Value = filaTb("4FaltaJustificada").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 2, 5), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 2, 5), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 5), Range).Value = filaTb("4FaltaSinJustificar").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 3, 5), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 3, 5), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 4, 5), Range).Value = Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 4, 5), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 4, 5), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 2, 6), Range).Value = Convert.ToInt32(filaTb("1FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("2FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("3FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("4FaltaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 2, 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 2, 6), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 6), Range).Value = Convert.ToInt32(filaTb("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4FaltaSinJustificar").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 3, 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 3, 6), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 4, 6), Range).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 4, 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(fil + 4, 6), Range).Borders.LineStyle = XlLineStyle.xlContinuous


                        Exit For
                    End If

                Next


                'RCB_NotaBimestralCualitativa	BM_CodigoBimestre	AL_CodigoAlumno
                'AD	1	20090083
                'AD	1	20090174




                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 8), Range), CType(excel.ActiveSheet.Cells(fil, 11), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 8), Range), CType(excel.ActiveSheet.Cells(fil, 11), Range)).Value = "CONDUCTA"
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 8), Range), CType(excel.ActiveSheet.Cells(fil, 11), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


                CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).Value = "Term I"
                CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(fil + 1, 9), Range).Value = "Term II"
                CType(excel.ActiveSheet.Cells(fil + 1, 9), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(fil + 1, 9), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 10), Range).Value = "Term III"
                CType(excel.ActiveSheet.Cells(fil + 1, 10), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(fil + 1, 10), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 11), Range).Value = "Term IV"
                CType(excel.ActiveSheet.Cells(fil + 1, 11), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(fil + 1, 11), Range).Borders.LineStyle = XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 11), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


                CType(excel.ActiveSheet.Cells(fil + 2, 8), Range).Borders.LineStyle = XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(fil + 2, 9), Range).Borders.LineStyle = XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(fil + 2, 10), Range).Borders.LineStyle = XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(fil + 2, 11), Range).Borders.LineStyle = XlLineStyle.xlContinuous

                For Each fill As System.Data.DataRow In tb_conducta.Rows
                    If Convert.ToInt32(fill("AL_CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                        If fill("BM_CodigoBimestre").ToString() = "1" Then
                            CType(excel.ActiveSheet.Cells(fil + 2, 8), Range).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            CType(excel.ActiveSheet.Cells(fil + 2, 8), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "2" Then
                            CType(excel.ActiveSheet.Cells(fil + 2, 9), Range).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            CType(excel.ActiveSheet.Cells(fil + 2, 9), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "3" Then
                            CType(excel.ActiveSheet.Cells(fil + 2, 10), Range).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            CType(excel.ActiveSheet.Cells(fil + 2, 10), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "4" Then
                            CType(excel.ActiveSheet.Cells(fil + 2, 11), Range).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            CType(excel.ActiveSheet.Cells(fil + 2, 11), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        End If
                    End If
                Next

                'RCB_NotaBimestralCualitativa	BM_CodigoBimestre	AL_CodigoAlumno
                'A	1	20100051

                'CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).Value = ""
                'CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).Value = ""
                'CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).Value = ""
                'CType(excel.ActiveSheet.Cells(fil, 8), Range).Value = ""
                fil += 4
                Dim nombreCurso As String = ""
                fil += 1
                CType(excel.ActiveSheet.Cells(fil, 1), Range).Value = "COMMENTS"
                CType(excel.ActiveSheet.Cells(fil, 1), Range).Font.Bold = True
                fil += 1
                CType(excel.ActiveSheet.Cells(fil, 1), Range).Value = "TUTOR"



                Dim estadoFilasMayor225 As Boolean = False
                For iComp As Integer = 0 To lst(iPerosona).lstLibretaComponente.Count - 1
                    If lst(iPerosona).lstLibretaComponente(iComp).nombreCurso <> nombreCurso Then
                        If lst(iPerosona).lstLibretaComponente(iComp).observacionCurso = "" Then
                            Continue For
                        End If
                        fil += 1

                        If 225 - (fil + 6) < 0 And Not estadoFilasMayor225 Then
                            fil = fil + (225 - fil) + 2
                            estadoFilasMayor225 = True
                        End If

                        CType(excel.ActiveSheet.Cells(fil, 1), Range).Value = lst(iPerosona).lstLibretaComponente(iComp).nombreCurso
                        CType(excel.ActiveSheet.Cells(fil, 1), Range).Font.Bold = True
                        fil += 1
                        ''CType(excel.ActiveSheet.Cells(fil, 1), Range).Value = olibretaComponenteTemp.observacionCurso
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 11), Range)).MergeCells = True
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 11), Range)).Value = lst(iPerosona).lstLibretaComponente(iComp).observacionCurso
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 11), Range)).Font.Size = 14
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 11), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 11), Range)).WrapText = True
                        fil += 5
                    End If
                    nombreCurso = lst(iPerosona).lstLibretaComponente(iComp).nombreCurso
                Next

                ''excel.Application.Range(CType(excel.ActiveSheet.Cells(iniciaIndicador, 8), Range), CType(excel.ActiveSheet.Cells(iniciaIndicador + contadorIndicador - 1, 8), Range)).MergeCells = True
                ''excel.Application.Range(CType(excel.ActiveSheet.Cells(iniciaIndicador, 8), Range), CType(excel.ActiveSheet.Cells(iniciaIndicador + contadorIndicador - 1, 8), Range)).Borders.LineStyle = XlLineStyle.xlContinuous
                ''
                fil += 1
                '' excel.Application.Range(CType(excel.ActiveSheet.Cells(iniciaIndicador, 8), Range), CType(excel.ActiveSheet.Cells(iniciaIndicador + contadorIndicador - 1, 8), Range)).Borders.LineStyle = XlLineStyle.xlContinuous


                'CType(excel.ActiveSheet.Cells(fil + 4, 2), Range).Borders(XlBordersIndex.xlInsideVertical).LineStyle = XlLineStyle.xlContinuous
                'CType(excel.ActiveSheet.Cells(fil + 4, 2), Range).Value = "XlBordersIndex.xlInsideVertical"


                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 2), Range), CType(excel.ActiveSheet.Cells(fil + 2, 3), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 2), Range), CType(excel.ActiveSheet.Cells(fil + 2, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 2), Range), CType(excel.ActiveSheet.Cells(fil + 2, 3), Range)).Value = "TUTOR"

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 2), Range), CType(excel.ActiveSheet.Cells(fil + 2, 3), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 5), Range), CType(excel.ActiveSheet.Cells(fil + 2, 7), Range)).Merge(True)

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 5), Range), CType(excel.ActiveSheet.Cells(fil + 2, 7), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 5), Range), CType(excel.ActiveSheet.Cells(fil + 2, 7), Range)).Value = "PARENTS"

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 5), Range), CType(excel.ActiveSheet.Cells(fil + 2, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter




                ''
                excel.ActiveSheet.Cells.Columns(1).ColumnWidth = 12

                excel.ActiveSheet.Cells.Columns(2).ColumnWidth = 17
                excel.ActiveSheet.Cells.Columns(3).ColumnWidth = 17
                excel.ActiveSheet.Cells.Columns(4).ColumnWidth = 17
                excel.ActiveSheet.Cells.Columns(5).ColumnWidth = 17

                excel.ActiveSheet.Cells.Columns(11).ColumnWidth = 8
                excel.ActiveSheet.Cells.Columns(6).ColumnWidth = 19


                excel.ActiveSheet.Cells.Columns(7).ColumnWidth = 7
                excel.ActiveSheet.Cells.Columns(8).ColumnWidth = 7
                excel.ActiveSheet.Cells.Columns(9).ColumnWidth = 7
                excel.ActiveSheet.Cells.Columns(10).ColumnWidth = 8


                ''

                'excel.ActiveWindow.Zoom = 75


                'With excel.ActiveSheet.PageSetup
                '    .LeftHeader = ""
                '    .CenterHeader = ""
                '    .RightHeader = ""
                '    .LeftFooter = ""
                '    .CenterFooter = ""
                '    .RightFooter = ""
                '    .LeftMargin = excel.Application.InchesToPoints(0.7)
                '    .RightMargin = excel.Application.InchesToPoints(0.7)
                '    .TopMargin = excel.Application.InchesToPoints(0.75)
                '    .BottomMargin = excel.Application.InchesToPoints(0.75)
                '    .HeaderMargin = excel.Application.InchesToPoints(0.3)
                '    .FooterMargin = excel.Application.InchesToPoints(0.3)
                '    .PrintHeadings = False
                '    .PrintGridlines = False
                '    '.PrintComments = xlPrintNoComments
                '    .PrintQuality = 600
                '    .CenterHorizontally = False
                '    .CenterVertically = False
                '    .Orientation = 1
                '    .Draft = False
                '    '.PaperSize = xlPaperLetter
                '    '.FirstPageNumber = xlAutomatic
                '    '.Order = OrderedDictionary xlDownThenOver
                '    .BlackAndWhite = False
                '    .Zoom = False
                '    .FitToPagesWide = 1
                '    .FitToPagesTall = False
                '    '.PrintErrors = xlPrintErrorsDisplayed
                '    .OddAndEvenPagesHeaderFooter = False
                '    .DifferentFirstPageHeaderFooter = False
                '    .ScaleWithDocHeaderFooter = True
                '    .AlignMarginsHeaderFooter = True
                '    .EvenPage.LeftHeader.Text = ""
                '    .EvenPage.CenterHeader.Text = ""
                '    .EvenPage.RightHeader.Text = ""
                '    .EvenPage.LeftFooter.Text = ""
                '    .EvenPage.CenterFooter.Text = ""
                '    .EvenPage.RightFooter.Text = ""
                '    .FirstPage.LeftHeader.Text = ""
                '    .FirstPage.CenterHeader.Text = ""
                '    .FirstPage.RightHeader.Text = ""
                '    .FirstPage.LeftFooter.Text = ""
                '    .FirstPage.CenterFooter.Text = ""
                '    .FirstPage.RightFooter.Text = ""
                'End With





                '' Exit For

                alumnosProcesado += 1

                If Not estadoDetenerProceso Then

                    Exit For

                End If



            Next

            Dim nombreArchivoGuardado As String = ""

            wbkWorkbook.SaveAs(nombreArchivo)
            nombreArchivoGuardado = nombreArchivo '' wbkWorkbook.Path()

            estadoOperacion = True

            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

            ''
            Return nombreArchivoGuardado

        Catch ex As Exception

        End Try
    End Function
    ''


    Function crearReportePrimaria(ByVal int_codAsignascionAula As Integer, ByVal int_bimestre As Integer, ByVal nombreArchivo As String) As String
        Try
            Dim dtInformacionAdicional As New System.Data.DataTable

            Dim rutaApp As String = ""
            rutaApp = Environment.CurrentDirectory()

            'Dim rutaPlantillas As String = System.Configuration.ConfigurationManager.AppSettings.Item("LibretaPrimaria").ToString.Trim
            Dim abrBimestre As String = ""
            If CodBimestre = 1 Then
                abrBimestre = "I"
            End If
            If CodBimestre = 2 Then
                abrBimestre = "II"
            End If
            If CodBimestre = 3 Then
                abrBimestre = "III"
            End If
            If CodBimestre = 4 Then
                abrBimestre = "IV"
            End If

            Dim rutaPlantillas As String = System.Configuration.ConfigurationManager.AppSettings.Item("ConsolidadoPrimaria")



            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range



            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

            '<add key="ConsolidadoPrimaria"  value="\Plantillas\ExportacionLibreta\plnConsolidadoPrimaria.xls"/>
            '<add key="ConsolidadoInicial"  value="\Plantillas\ExportacionLibreta\consolidadoEvaluacion.xls"/>


            'Dim rutaREpositorioTemporales As String = System.Configuration.ConfigurationManager.AppSettings.Item("Temporales").ToString.Trim & rutaTemp & ".xlsx"

            'File.Copy(rutaApp & rutaPlantillas, rutaApp & rutaREpositorioTemporales)

            'wbkWorkbook = excel.Workbooks.Open(rutaApp & rutaREpositorioTemporales)

            Dim anio As String = ""


            'nombreArchivo


            anio = Year(Now).ToString()

            Dim dst As New DataSet
            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ConsolidadoNotasPrimaria(int_codAsignascionAula, int_bimestre, 1, 1, 1, 1)
            Dim bimestre As Integer = 1
            Dim lp As New List(Of alumnos)

            dtInformacionAdicional = dst.Tables(4)
            Dim descripcionGrado As String = ""
            Dim descAula As String = ""
            descripcionGrado = dtInformacionAdicional.Rows(0)("GD_Descripcion").ToString()
            descAula = dtInformacionAdicional.Rows(0)("AU_Descripcion").ToString()

            lp = crearListaPersonas1(dst.Tables(1))

            cantidadAlumnos = lp.Count
            '' cantidadAlumnos = lst.Count

            Dim rutaREpositorioTemporales As String = System.Configuration.ConfigurationManager.AppSettings.Item("Temporales").ToString.Trim & rutaTemp & ".xlsx"


            File.Copy(rutaApp & rutaPlantillas, rutaApp & rutaREpositorioTemporales)



            wbkWorkbook = excel.Workbooks.Open(rutaApp & rutaREpositorioTemporales)




            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).MergeCells = True

            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).Font.Size = 16
            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).Value = " Consolidado  de Evaluación -" & descripcionGrado & " " & descAula & " (Asignaturas Totales )" & abrBimestre & " Bimestre - " & anio


            CType(excel.ActiveSheet.Cells(1, 12), Range).Value = Now.Day.ToString() & "/" & Month(Now).ToString() & "/" & Year(Now).ToString()
            CType(excel.ActiveSheet.Cells(1, 12), Range).Orientation = 0
            CType(excel.ActiveSheet.Cells(1, 12), Range).WrapText = True
            CType(excel.ActiveSheet.Cells(2, 12), Range).Value = Hour(Now).ToString() & ":" & Minute(Now).ToString() & ":" & Second(Now).ToString()


            CType(excel.ActiveSheet.Cells(2, 12), Range).Orientation = 0


            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()
            Dim fi As Integer = 9
            Dim cont As Integer = 0
            CType(excel.Cells(8, 1), Microsoft.Office.Interop.Excel.Range).Value = "Nro."
            CType(excel.Cells(8, 1), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(8, 2), Microsoft.Office.Interop.Excel.Range).Value = "Apellidos y nombres "
            CType(excel.Cells(8, 2), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(8, 3), Microsoft.Office.Interop.Excel.Range).Value = "Codigo"
            CType(excel.Cells(8, 3), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            ''.Borders.LineStyle = XlLineStyle.xlContinuous
            For Each ff As System.Data.DataRow In dst.Tables(0).Rows
                CType(excel.Cells(8, CInt(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value = ff("nomCurso")
                CType(excel.Cells(8, CInt(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(8, CInt(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Orientation = 90
                'codCurso	nomCurso	posCurso
                '1:          Maths(4)
            Next
            Dim sumasFilas As Integer = dst.Tables(0).Rows.Count + 3
            CType(excel.Cells(8, sumasFilas + 1), Microsoft.Office.Interop.Excel.Range).Value = "AD"
            CType(excel.Cells(8, sumasFilas + 1), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(8, sumasFilas + 2), Microsoft.Office.Interop.Excel.Range).Value = "A"
            CType(excel.Cells(8, sumasFilas + 2), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(8, sumasFilas + 3), Microsoft.Office.Interop.Excel.Range).Value = "B"
            CType(excel.Cells(8, sumasFilas + 3), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(8, sumasFilas + 4), Microsoft.Office.Interop.Excel.Range).Value = "C"
            CType(excel.Cells(8, sumasFilas + 4), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            Dim posCol As Integer = 3
            Dim sumasA As Integer = 0
            Dim sumasB As Integer = 0
            Dim sumasC As Integer = 0
            Dim sumasAD As Integer = 0
            For Each oal As alumnos In lp



                alumnosProcesado += 1



                cont += 1
                CType(excel.Cells(fi, 1), Microsoft.Office.Interop.Excel.Range).Value = cont
                CType(excel.Cells(fi, 1), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi, 2), Microsoft.Office.Interop.Excel.Range).Value = oal.nombre
                CType(excel.Cells(fi, 2), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi, 3), Microsoft.Office.Interop.Excel.Range).Value = oal.codigo
                CType(excel.Cells(fi, 3), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                posCol = 3

                sumasA = 0
                sumasB = 0
                sumasC = 0
                sumasAD = 0
                For Each onotasConsolidado In oal.lstNotas

                    CType(excel.Cells(fi, onotasConsolidado.pos), Microsoft.Office.Interop.Excel.Range).Value = onotasConsolidado.notaPromedio.ToString().ToUpper()
                    If onotasConsolidado.notaPromedio.ToString().ToUpper() = "A" Then
                        sumasA += 1
                    End If
                    If onotasConsolidado.notaPromedio.ToString().ToUpper() = "B" Then
                        sumasB += 1
                    End If
                    If onotasConsolidado.notaPromedio.ToString().ToUpper() = "C" Then
                        sumasC += 1
                    End If
                    If onotasConsolidado.notaPromedio.ToString().ToUpper() = "AD" Then
                        sumasAD += 1
                    End If
                Next

                CType(excel.Cells(fi, sumasFilas + 1), Microsoft.Office.Interop.Excel.Range).Value = sumasAD
                CType(excel.Cells(fi, sumasFilas + 1), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi, sumasFilas + 2), Microsoft.Office.Interop.Excel.Range).Value = sumasA
                CType(excel.Cells(fi, sumasFilas + 2), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi, sumasFilas + 3), Microsoft.Office.Interop.Excel.Range).Value = sumasB
                CType(excel.Cells(fi, sumasFilas + 3), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi, sumasFilas + 4), Microsoft.Office.Interop.Excel.Range).Value = sumasC
                CType(excel.Cells(fi, sumasFilas + 4), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                For i = 4 To dst.Tables(0).Rows.Count - 1 + 4
                    CType(excel.Cells(fi, i), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                Next
                fi += 1
            Next

            Dim cantidadFilas As Integer = fi
            Dim catidadColumnas As Integer = dst.Tables(0).Rows.Count - 1
            Dim cantidadA As Integer = 0
            Dim cantidadB As Integer = 0
            Dim cantidadC As Integer = 0
            Dim cantidadAD As Integer = 0
            Dim letras As String = ""

            For Each ff As System.Data.DataRow In dst.Tables(0).Rows
                For contFilas As Integer = 7 To cantidadFilas
                    letras = CType(excel.Cells(contFilas, Convert.ToInt32(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value
                    If letras Is Nothing Then
                        letras = ""
                    End If
                    If letras = "A" Then
                        cantidadA += 1
                    End If
                    If letras = "B" Then
                        cantidadB += 1
                    End If
                    If letras = "C" Then
                        cantidadC += 1
                    End If
                    If letras = "AD" Then
                        cantidadAD += 1
                    End If

                Next
                CType(excel.Cells(fi, Convert.ToInt32(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value = cantidadAD
                CType(excel.Cells(fi + 1, Convert.ToInt32(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value = cantidadA
                CType(excel.Cells(fi + 2, Convert.ToInt32(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value = cantidadB
                CType(excel.Cells(fi + 3, Convert.ToInt32(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value = cantidadC
                ''
                CType(excel.Cells(fi, 3), Microsoft.Office.Interop.Excel.Range).Value = "TOTAL AD"
                CType(excel.Cells(fi + 1, 3), Microsoft.Office.Interop.Excel.Range).Value = "TOTAL A"
                CType(excel.Cells(fi + 2, 3), Microsoft.Office.Interop.Excel.Range).Value = "TOTAL B"
                CType(excel.Cells(fi + 3, 3), Microsoft.Office.Interop.Excel.Range).Value = "TOTAL C"
                ''
                cantidadA = 0
                cantidadB = 0
                cantidadC = 0
                cantidadAD = 0
            Next
            For i = 3 To dst.Tables(0).Rows.Count - 1 + 4
                CType(excel.Cells(fi, i), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi + 1, i), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi + 2, i), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi + 3, i), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            Next
            CType(excel.Cells(5, 2), Microsoft.Office.Interop.Excel.Range).Value = dst.Tables(2).Rows(0)("tutor").ToString()
            '' CType(excel.Cells(5, 4), Microsoft.Office.Interop.Excel.Range).Value = dst.Tables(3).Rows(0)("AU_Descripcion").ToString()
            ''CType(excel.Cells(2, 11), Microsoft.Office.Interop.Excel.Range).Value = bimestre.ToString()
            '' CType(excel.Cells(2, 11), Microsoft.Office.Interop.Excel.Range).Orientation = 0
            Dim carpeta As String = ""


            With excel.ActiveSheet.PageSetup
                .LeftHeader = ""
                .CenterHeader = ""
                .RightHeader = ""
                .LeftFooter = ""
                .CenterFooter = ""
                .RightFooter = ""
                .LeftMargin = excel.Application.InchesToPoints(0.7)
                .RightMargin = excel.Application.InchesToPoints(0.7)
                .TopMargin = excel.Application.InchesToPoints(0.75)
                .BottomMargin = excel.Application.InchesToPoints(0.75)
                .HeaderMargin = excel.Application.InchesToPoints(0.3)
                .FooterMargin = excel.Application.InchesToPoints(0.3)
                .PrintHeadings = False
                .PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                .PrintQuality = 600
                .CenterHorizontally = False
                .CenterVertically = False
                .Orientation = 1
                .Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = OrderedDictionary xlDownThenOver
                .BlackAndWhite = False
                .Zoom = False
                .FitToPagesWide = 1
                .FitToPagesTall = False
                '.PrintErrors = xlPrintErrorsDisplayed
                .OddAndEvenPagesHeaderFooter = False
                .DifferentFirstPageHeaderFooter = False
                .ScaleWithDocHeaderFooter = True
                .AlignMarginsHeaderFooter = True
                .EvenPage.LeftHeader.Text = ""
                .EvenPage.CenterHeader.Text = ""
                .EvenPage.RightHeader.Text = ""
                .EvenPage.LeftFooter.Text = ""
                .EvenPage.CenterFooter.Text = ""
                .EvenPage.RightFooter.Text = ""
                .FirstPage.LeftHeader.Text = ""
                .FirstPage.CenterHeader.Text = ""
                .FirstPage.RightHeader.Text = ""
                .FirstPage.LeftFooter.Text = ""
                .FirstPage.CenterFooter.Text = ""
                .FirstPage.RightFooter.Text = ""
            End With



            wbkWorkbook.SaveAs(nombreArchivo)

            'Dim nombreArchivoGuardado As String = ""

            'wbkWorkbook.SaveAs(nombreArchivo)
            carpeta = nombreArchivo


            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()
            Return carpeta
        Catch ex As Exception
        Finally
        End Try
    End Function




    'Public Class personaLibreta
    '    Public nombreAlumno As String
    '    Public codAlumno As String
    '    Public conductaBimestral As String
    '    Public lstLibretaComponente As New List(Of libretaComponente)
    '    '  Public lstCursoLibreta As New List(Of CursoLibreta)
    'End Class

    'Public Class libretaComponente
    '    Public codAlumno As String
    '    Public codRegComponente As String
    '    Public nombreCurso As String
    '    Public columna As Boolean
    '    Public nombreComponente As String
    '    Public notaComponente As String
    '    Public promedioComponente As String
    '    Public observacionCurso As String
    '    Public lstIndicador As New List(Of libretaIndicador)
    'End Class

    'Public Class libretaIndicador
    '    Public codComponente As String
    '    Public nombreIndicador As String
    '    Public notaIndicador As String
    '    Public codIndicador As String
    'End Class

    'Public Function crearListaLibreta(ByVal dt_tb As System.Data.DataTable) As List(Of personaLibreta)
    '    Dim entro1 As Boolean = False
    '    Dim entro2 As Boolean = False

    '    Dim LstMatematica As New List(Of matematica)
    '    Dim omatematica As matematica

    '    Dim libretaComponenteTemP As New libretaComponente

    '    Dim libretaComponenteTemP2 As New libretaComponente


    '    Dim lstLibretaAlumno As New List(Of personaLibreta)
    '    Dim lstLibretasComponente As New List(Of libretaComponente)
    '    Dim lstLibretaIndicador As New List(Of libretaIndicador)

    '    Dim lstLibretaAlumnoRes As New List(Of personaLibreta)
    '    lstLibretaAlumno = crearListaAlumnos(dt_tb)
    '    lstLibretasComponente = creaListaComponente(dt_tb)
    '    lstLibretaIndicador = crearListaLibretaIndicador(dt_tb)


    '    Dim opersonaLibretaT As personaLibreta
    '    Dim oLibretaComponenteT As libretaComponente
    '    Dim oLibretaIndicadorT As libretaIndicador
    '    Dim contEntro1 As Integer = 0
    '    Dim contEntro2 As Integer = 0

    '    For Each opersonaLibreta As personaLibreta In lstLibretaAlumno

    '        opersonaLibretaT = New personaLibreta
    '        opersonaLibretaT.codAlumno = opersonaLibreta.codAlumno
    '        opersonaLibretaT.nombreAlumno = opersonaLibreta.nombreAlumno
    '        opersonaLibretaT.conductaBimestral = opersonaLibreta.conductaBimestral

    '        For Each oLibretaComponente As libretaComponente In lstLibretasComponente
    '            oLibretaComponenteT = New libretaComponente
    '            oLibretaComponenteT.codRegComponente = oLibretaComponente.codRegComponente
    '            oLibretaComponenteT.nombreComponente = oLibretaComponente.nombreComponente
    '            oLibretaComponenteT.notaComponente = oLibretaComponente.notaComponente
    '            oLibretaComponenteT.codAlumno = oLibretaComponente.codAlumno
    '            oLibretaComponenteT.nombreCurso = oLibretaComponente.nombreCurso
    '            oLibretaComponenteT.promedioComponente = oLibretaComponente.promedioComponente

    '            oLibretaComponenteT.columna = oLibretaComponente.columna

    '            oLibretaComponenteT.observacionCurso = oLibretaComponente.observacionCurso

    '            For Each oLibretaIndicador As libretaIndicador In lstLibretaIndicador
    '                oLibretaIndicadorT = New libretaIndicador
    '                oLibretaIndicadorT.nombreIndicador = oLibretaIndicador.nombreIndicador
    '                oLibretaIndicadorT.notaIndicador = oLibretaIndicador.notaIndicador
    '                oLibretaIndicadorT.codComponente = oLibretaIndicador.codComponente
    '                If oLibretaComponenteT.codRegComponente = oLibretaIndicadorT.codComponente Then
    '                    oLibretaComponenteT.lstIndicador.Add(oLibretaIndicadorT)
    '                End If

    '            Next

    '            If oLibretaComponenteT.nombreCurso.ToUpper() = "MATEMÁTICA" And oLibretaComponenteT.codAlumno = opersonaLibretaT.codAlumno And oLibretaComponenteT.nombreComponente.ToUpper() = "ACTITUD" Then
    '                entro1 = True
    '                libretaComponenteTemP = oLibretaComponenteT
    '                omatematica = New matematica
    '                omatematica.oMate = libretaComponenteTemP
    '                omatematica.coAlumno = opersonaLibretaT.codAlumno
    '                LstMatematica.Add(omatematica)
    '            End If
    '            '
    '            If oLibretaComponenteT.nombreCurso.ToUpper() = "MATEMÁTICA" And oLibretaComponenteT.codAlumno = opersonaLibretaT.codAlumno And oLibretaComponenteT.nombreComponente.ToUpper() = "NÚMEROS, RELACIONES Y OPERACIONES" Then
    '                entro2 = True
    '                libretaComponenteTemP2 = oLibretaComponenteT
    '                'omatematica = New matematica
    '                'omatematica.oMate = libretaComponenteTemP
    '                'omatematica.coAlumno = opersonaLibretaT.codAlumno
    '                'LstMatematica.Add(omatematica)
    '            End If
    '            '
    '            ''libretaComponenteTemP2
    '            ''NÚMEROS, RELACIONES Y OPERACIONES
    '            If opersonaLibretaT.codAlumno = oLibretaComponenteT.codAlumno Then
    '                If entro1 And contEntro1 = 0 Then
    '                    contEntro1 += 1
    '                    Continue For
    '                End If
    '                If entro2 And contEntro2 = 0 Then
    '                    contEntro2 += 1
    '                    Continue For
    '                End If
    '                opersonaLibretaT.lstLibretaComponente.Add(oLibretaComponenteT)
    '            End If
    '        Next

    '        If entro1 Then
    '            Dim c As Integer = -1
    '            Dim conMat As Integer = 0

    '            Dim can = From j In opersonaLibretaT.lstLibretaComponente Where j.nombreCurso.ToUpper() = "MATEMÁTICA"

    '            For Each oLibretaComponenteTT As libretaComponente In opersonaLibretaT.lstLibretaComponente
    '                c += 1
    '                If oLibretaComponenteTT.nombreCurso.ToUpper() = "MATEMÁTICA" Then
    '                    opersonaLibretaT.lstLibretaComponente.Insert(c + can.Count(), libretaComponenteTemP)
    '                    ''  opersonaLibretaT.lstLibretaComponente.Add(libretaComponenteTemP)
    '                    Exit For
    '                End If
    '            Next

    '            entro1 = False
    '        End If

    '        If entro2 Then
    '            Dim c1 As Integer = -1
    '            Dim conMat As Integer = 0
    '            Dim can1 = From j In opersonaLibretaT.lstLibretaComponente Where j.nombreCurso.ToUpper() = "MATEMÁTICA"
    '            For Each oLibretaComponenteTT As libretaComponente In opersonaLibretaT.lstLibretaComponente
    '                c1 += 1
    '                If oLibretaComponenteTT.nombreCurso.ToUpper() = "MATEMÁTICA" Then
    '                    opersonaLibretaT.lstLibretaComponente.Insert(c1, libretaComponenteTemP2)
    '                    ''  opersonaLibretaT.lstLibretaComponente.Add(libretaComponenteTemP)
    '                    Exit For
    '                End If
    '            Next
    '            entro2 = False
    '        End If
    '        lstLibretaAlumnoRes.Add(opersonaLibretaT)

    '        '' LstMatematica.Add()
    '        contEntro1 = 0
    '        contEntro2 = 0

    '        entro1 = False
    '        entro2 = False

    '    Next



    '    'Dim lstCursoOrdenar As New List(Of curso)
    '    'lstCursoOrdenar = From lstOrdenado In lstCurso Order By lstOrdenado.orderCurso Ascending Select lstOrdenado
    '    ''
    '    '' Dim lstTemp As IEnumerable(Of personaLibreta)

    '    ''  lstTemp = From h In lstLibretaAlumnoRes Order By h.lstLibretaComponente

    '    LstMatematicaGb = LstMatematica
    '    Return lstLibretaAlumnoRes




    'End Function
    'Public Class matematica
    '    Public oMate As New libretaComponente
    '    Public coAlumno As String
    'End Class

    'Function creaListaComponente(ByVal dt As System.Data.DataTable) As List(Of libretaComponente)

    '    Dim olibretaComponente As libretaComponente
    '    Dim codComponenteTemp As String = ""
    '    Dim lstRegistroNotaComponente As New List(Of libretaComponente)
    '    For Each fila As System.Data.DataRow In dt.Rows

    '        olibretaComponente = New libretaComponente
    '        olibretaComponente.codAlumno = fila("AL_CodigoAlumno").ToString()
    '        olibretaComponente.nombreCurso = fila("NC_Descripcion").ToString()
    '        olibretaComponente.codRegComponente = fila("RNC_CodigoRegistroNotaComponente").ToString()
    '        olibretaComponente.nombreComponente = fila("CP_Descripcion").ToString()
    '        olibretaComponente.notaComponente = fila("RNC_NotaComponente").ToString()
    '        olibretaComponente.promedioComponente = fila("RNBL_NotaFinalBimestre").ToString()

    '        olibretaComponente.columna = Convert.ToBoolean(fila("grupoLibreta").ToString())

    '        olibretaComponente.observacionCurso = fila("RNBL_ObservacionCurso").ToString()





    '        If olibretaComponente.codRegComponente <> codComponenteTemp Then

    '            lstRegistroNotaComponente.Add(olibretaComponente)
    '        End If

    '        codComponenteTemp = olibretaComponente.codRegComponente
    '    Next

    '    Return lstRegistroNotaComponente
    '    ''RNC_CodigoRegistroNotaComponente RNI_CodigoRegistroNotaIndicador AL_CodigoAlumno AGC_CodigoAsignacionGrupo pComponente CP_Descripcion	ID_Descripcion	BM_CodigoBimestre	RNBL_CodigoRegistroBimestralL	RNC_NotaComponente	RNI_NotaIndicador	RNBL_ObservacionCurso	RNBL_NotaFinalBimestre	RC_CodigoRegistroComponentes	RI_CodigoRegistroIndicadores	NC_Descripcion	CS_CodigoCurso
    '    ''26561	87805	20100052	687	26561	DOMINIO CORPORAL Y EXPRESIÓN CREATIVA.	Controla movimientos de su  cuerpo durante actividades de  habilidad y  destreza.	1	42509	A	A		A	578	1068	Educación Física	44

    'End Function


    'Function crearListaAlumnos(ByVal dt As System.Data.DataTable) As List(Of personaLibreta)
    '    Dim opersonaLibreta As personaLibreta
    '    Dim codTempAlumno As String = ""
    '    Dim lstPersonaLibreta As New List(Of personaLibreta)
    '    For Each fila As System.Data.DataRow In dt.Rows
    '        opersonaLibreta = New personaLibreta
    '        opersonaLibreta.codAlumno = fila("AL_CodigoAlumno").ToString()
    '        opersonaLibreta.nombreAlumno = fila("nombre").ToString()
    '        opersonaLibreta.conductaBimestral = fila("conductaBimestral").ToString()


    '        If opersonaLibreta.codAlumno <> codTempAlumno Then
    '            lstPersonaLibreta.Add(opersonaLibreta)
    '        End If
    '        codTempAlumno = opersonaLibreta.codAlumno
    '    Next


    '    Return lstPersonaLibreta

    'End Function
    'Public Function crearListaLibretaIndicador(ByVal dt As System.Data.DataTable) As List(Of libretaIndicador)
    '    Dim lstLibretaIndicador As New List(Of libretaIndicador)
    '    Dim olibretaIndicador As libretaIndicador
    '    Dim idTempIndicador As String = ""


    '    For Each fila As System.Data.DataRow In dt.Rows
    '        olibretaIndicador = New libretaIndicador

    '        olibretaIndicador.codComponente = fila("RNC_CodigoRegistroNotaComponente").ToString()
    '        olibretaIndicador.notaIndicador = fila("RNI_NotaIndicador").ToString()
    '        olibretaIndicador.nombreIndicador = fila("ID_Descripcion").ToString()
    '        olibretaIndicador.codIndicador = fila("RNI_CodigoRegistroNotaIndicador").ToString()

    '        If olibretaIndicador.codIndicador <> idTempIndicador Then
    '            lstLibretaIndicador.Add(olibretaIndicador)
    '        End If

    '        idTempIndicador = olibretaIndicador.codIndicador

    '    Next
    '    Return lstLibretaIndicador
    'End Function



    Shared Sub EiminaReferencias(ByRef Referencias As Object)
        Try
            'Bucle de eliminacion
            Do Until _
                      System.Runtime.InteropServices.Marshal.ReleaseComObject(Referencias) <= 0
            Loop
        Catch
        Finally
            Referencias = Nothing
        End Try
    End Sub


    ''
    Private Sub cargarComboAsignacionAulaLibretas(ByVal int_TipoNota As Integer)

        Dim int_CodigoTipoUsuario As Integer = 1 ''Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 '' Me.Master.Obtener_CodigoUsuarioLogueado

        Dim int_CodigoTrabajador As Integer = 0 ' todas las aulas de secundaria, sin filtrar por profesor
        Dim int_CodigoAnioAcademico As Integer = 1 '' Me.Master.Obtener_CodigoPeriodoEscolar
        Dim int_CodigoSede As Integer = 1
        Dim int_Estado As Integer = 1

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
          int_CodigoTrabajador, int_TipoNota, int_CodigoAnioAcademico, int_CodigoSede, _
          int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

        '' Controles.llenarCombo(ddlSalonRepPrimaria, ds_Lista, "Codigo", "DescAulaCompuestaCorta", False, False)

    End Sub
    ''
    ''
    ''
    Private Sub btnExportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportar.Click

        fechaReporte = txtFechaImpresion.Text
        tmrControlador.Enabled = True
        lblHoja.Text = ""
        estadoOperacion = False
        CodBimestre = cmbBimestre.SelectedValue
        cantidadAlumnos = 0

        Dim str_FileName As String = "Libreta"



        ''If lstReportes.SelectedValue = Constantes.ClaseReporteLibreta Then ' 2
        ''    str_FileName = "Libreta"
        ''ElseIf lstReportes.SelectedValue = Constantes.ClaseReporteConsolidado Then ' 1
        ''    str_FileName = "Consolidado"
        ''End If

        'Dim SaveFile As New SaveFileDialog
        'Dim str_FileName As String = ""

        With SaveFile
            .Title = "Grabar archivo como"
            .FileName = str_FileName & "_" & Format(Now, "yyyyMMdd") & "_" & Format(Now, "hhmmss")
            .DefaultExt = "xls"
            .Filter = "Excel File|*.xls|Todos los archivos (*.*)|*.*"
            .InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            .OverwritePrompt = True
        End With

        ''
        If SaveFile.ShowDialog = System.Windows.Forms.DialogResult.OK Then

            btnExportar.Enabled = False
            lblMensajePros.Text = "Procesando reporte ..."
            pcbLoading.Visible = True
            estadoOperacion = False
            estadoDetenerProceso = True
            tmrControlador.Enabled = True
            nombreArchivo = SaveFile.FileName

            pgbEstadoProceso.Value = 0
            pgbEstadoProceso.Minimum = 0
            alumnosProcesado = 0
            Dim ocrear As New generarExcel(AddressOf crearReporte)

            ocrear.BeginInvoke(New AsyncCallback(AddressOf finalizarOperacion), Nothing)


        End If


        ''
    End Sub

    Private Sub tmrControlador_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrControlador.Tick



        If cantidadAlumnos > 0 And alumnosProcesado <= cantidadAlumnos Then

            pgbEstadoProceso.Maximum = cantidadAlumnos
            pgbEstadoProceso.Value = alumnosProcesado

            If tipoReporteConsolidadoLibreta = Constantes.ClaseReporteLibreta Then
                lblHoja.Text = alumnosProcesado.ToString()
            End If
            lblHoja.Text = alumnosProcesado.ToString()

        End If

        If estadoOperacion = True Then
            btnExportar.Enabled = True
            tmrControlador.Enabled = False
            pcbLoading.Visible = False
            lblMensajePros.Text = ""
            pgbEstadoProceso.Value = 0
        End If


    End Sub

    Sub crearReporte()
        Try
            Dim alumnos = ""

            Dim codAlumnos = (From al In dt_ListaAlumnos.AsEnumerable() Where al("Chk") = True _
                               Select CStr(al("CodigoAlumno"))).ToList


            'update 17/12/2012

            'CodBimestre = CInt(cmbBimestre.SelectedValue)
            'CodSalon = CInt(cmbSalon.SelectedValue)
            'Dim codAlumnos = (From al In dt_ListaAlumnos.AsEnumerable() Where al("Chk") = True _
            '                 Select CStr(al("CodigoAlumno"))).ToList

            'Dim alumnos = codAlumnos.Aggregate(Function(prev, curr) prev + "," + curr)

            'Dim nombreArchivo As String = crearLibretaPrimariaUnaSolaHoja(CodSalon, CodBimestre, alumnos)

            'dtTipoReporte.Rows.Add(New Object() {"3", "Inicial"})
            'dtTipoReporte.Rows.Add(New Object() {"4", "Primaria"})
            'dtTipoReporte.Rows.Add(New Object() {"2", "Secudaria"})

            Dim nombreArchivo As String = ""

            If tipoReporte = 3 Then 'Inicial
                alumnos = codAlumnos.Aggregate(Function(prev, curr) prev + "," + curr)
                If CodBimestre = 1 Or CodBimestre = 3 Then
                    nombreArchivo = crearLibretaInicial1(CodSalon, CodBimestre, alumnos)
                End If

                Process.Start("explorer.exe", nombreArchivo)
            End If

            ''-------------------------------------------------
            If tipoReporte = 4 Then 'Primaria
                alumnos = codAlumnos.Aggregate(Function(prev, curr) prev + "," + curr)

                If CodBimestre = 1 Or CodBimestre = 3 Then
                    nombreArchivo = crearLibretaPrimariaUnaSolaHoja(CodSalon, CodBimestre, alumnos)
                End If

                If CodBimestre = 2 Or CodBimestre = 4 Then
                    nombreArchivo = crearLibretaPrimariaUnaSolaHojaCuatroHojas(CodSalon, CodBimestre, alumnos)
                End If


                Process.Start("explorer.exe", nombreArchivo)

            End If
            ''-------------------------------------------------
            If tipoReporte = 2 Then
                nombreArchivo = generarLibretaSecundaria2(CodSalon, CodBimestre, nombreArchivo)
            End If

            Process.Start("explorer.exe", nombreArchivo & "\")




            ''If tipoReporteConsolidadoLibreta = Constantes.ClaseReporteLibreta Then
            ''    If Convert.ToInt32(tipoPresentacion) = Constantes.tipoReportePrimaria Then
            ''        nombreArchivo = crearLibretaPrimaria1(CodSalon, CodBimestre, nombreArchivo)
            ''        Process.Start("explorer.exe", nombreArchivo & "\")
            ''    End If

            ''    If Convert.ToInt32(tipoPresentacion) = Constantes.tipoReporteIncial Then
            ''        If CodBimestre <> 2 Then
            ''            nombreArchivo = crearLibretaInicial1(CodSalon, CodBimestre, nombreArchivo)
            ''        Else
            ''            nombreArchivo = crearLibretaInicial1SegundoBimestre(CodSalon, CodBimestre, nombreArchivo)
            ''        End If
            ''        Process.Start("explorer.exe", nombreArchivo & "\")
            ''    End If

            ''    If Convert.ToInt32(tipoPresentacion) = Constantes.tipoReporteSecundaria Then

            ''        'libreta secundaria
            ''        nombreArchivo = generarLibretaSecundaria2(CodSalon, CodBimestre, nombreArchivo)
            ''        Process.Start("explorer.exe", nombreArchivo & "\")
            ''    End If
            ''ElseIf tipoReporteConsolidadoLibreta = Constantes.ClaseReporteConsolidado Then

            ''    If tipoPresentacion = Constantes.tipoReportePrimaria Then
            ''        nombreArchivo = crearReportePrimaria(CodSalon, CodBimestre, nombreArchivo)
            ''        Process.Start("explorer.exe", nombreArchivo & "\")
            ''    End If

            ''    If tipoPresentacion = Constantes.tipoReporteIncial Then
            ''        nombreArchivo = crearConsolidadoEvaluacion(CodSalon, CodBimestre, nombreArchivo)
            ''        Process.Start("explorer.exe", nombreArchivo & "\")
            ''    End If

            ''    If tipoPresentacion = Constantes.tipoReporteSecundaria Then
            ''        nombreArchivo = generarConsolidadoSecundaria2(CodSalon, CodBimestre, nombreArchivo)
            ''        Process.Start("explorer.exe", nombreArchivo & "\")
            ''    End If

            ''End If


        Catch ex As Exception
            MsgBox("Mensaje: " & ex.Message)
        Finally


            '' salonLast = CodSalon
        End Try
    End Sub

    Private Sub finalizarOperacion(ByVal rs As IAsyncResult)
        Dim a As AsyncResult = CType(rs, AsyncResult)
        Dim oc As generarExcel = CType(a.AsyncDelegate, generarExcel)
        oc.EndInvoke(rs)
        estadoOperacion = True

    End Sub


    Private Sub lstPresentacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPresentacion.SelectedIndexChanged
        Dim int_CodigoTrabajador As Integer = 0
        Dim int_TipoNota As Integer = 0
        Dim int_CodigoAnioAcademico As Integer = cmbPeriodo.SelectedValue
        Dim int_CodigoSede As Integer = 0
        Dim int_Estado As Integer = 0
        Dim int_CodigoUsuario As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = 0
        Dim cod_Modulo As Integer = 0
        Dim cod_Opcion As Integer = 0
        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim value As Object = lstPresentacion.SelectedValue



        Dim textO As String = ""


        textO = lstPresentacion.SelectedValue.ToString()
        If textO = "System.Data.DataRowView" Then
            Exit Sub
        End If



        If Convert.ToInt32(lstPresentacion.SelectedValue.ToString()) = Constantes.tipoReportePrimaria Then

        End If

        If Convert.ToInt32(lstPresentacion.SelectedValue.ToString()) = Constantes.tipoReportePrimaria Then
            Dim tipoPrimaria As Integer = Constantes.tipoReportePrimaria


            Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
               int_CodigoTrabajador, tipoPrimaria, int_CodigoAnioAcademico, int_CodigoSede, _
               int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)


            cmbSalon.DataSource = ds_Lista.Tables(0)
            cmbSalon.DisplayMember = "DescAulaCompuesta"
            cmbSalon.ValueMember = "Codigo"

        ElseIf Convert.ToInt32(lstPresentacion.SelectedValue.ToString()) = Constantes.tipoReporteIncial Then

            Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
              int_CodigoTrabajador, Constantes.tipoReporteIncial, int_CodigoAnioAcademico, int_CodigoSede, _
              int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)


            cmbSalon.DataSource = ds_Lista.Tables(0)
            cmbSalon.DisplayMember = "DescAulaCompuesta"
            cmbSalon.ValueMember = "Codigo"

        ElseIf Convert.ToInt32(lstPresentacion.SelectedValue.ToString()) = Constantes.tipoReporteSecundaria Then

            Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
              int_CodigoTrabajador, Constantes.tipoReporteSecundaria, int_CodigoAnioAcademico, int_CodigoSede, _
              int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            cmbSalon.DataSource = ds_Lista.Tables(0)
            cmbSalon.DisplayMember = "DescAulaCompuesta"
            cmbSalon.ValueMember = "Codigo"

        End If


        tipoPresentacion = CInt(Me.lstPresentacion.SelectedValue)

    End Sub

    Private Shared Instancia As frmReporteLibretaPrimaria = Nothing

    Public Shared Function Instance() As frmReporteLibretaPrimaria
        If Instancia Is Nothing OrElse Instancia.IsDisposed = True Then
            Instancia = New frmReporteLibretaPrimaria
        End If
        Instancia.BringToFront()
        Return Instancia
    End Function

    Private Sub lstReportes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstReportes.SelectedIndexChanged
        Dim textDefault As String = ""
        textDefault = lstReportes.SelectedValue.ToString()
        If textDefault = "System.Data.DataRowView" Then
            Exit Sub
        End If
        tipoReporteConsolidadoLibreta = Convert.ToInt32(lstReportes.SelectedValue)



    End Sub

#Region "Secundaria"

    Private Function generarLibretaSecundaria2(ByVal CodSalon As Integer, ByVal CodBimestre As Integer, ByVal NombreArchivo As String) As String

        'Dim ds_Lista As New DataSet
        Dim obl_rep_libretaNotas As New bl_rep_libretaNotas

        Dim int_CodigoAnioAcademico As Integer = CodAnio ' 2

        Dim ds_ListaAlumnos As New DataSet
        Dim dt_aux As System.Data.DataTable = dt_ListaAlumnos.Copy
        ds_ListaAlumnos.Tables.Add(dt_aux) 'obl_rep_libretaNotas.FUN_LIS_REP_AlumnosLibreta(CodSalon, CodBimestre, 0, 0, 0, 0)


        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks
        Dim oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets
        Dim oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String


        Dim rutaLibreta As String = System.Configuration.ConfigurationManager.AppSettings("rutaLibreta").ToString()
        Dim rutaPlantilla As String = System.Configuration.ConfigurationManager.AppSettings("LibretaSecundaria").ToString()

        Dim rutamadre As String = Environment.CurrentDirectory
        nombreRep = NombreArchivo

        ' Archivo excel a grabar
        sFile = nombreRep

        ' Plantilla a cargar
        sTemplate = rutamadre & rutaPlantilla

        oExcel.Visible = False
        oExcel.DisplayAlerts = False

        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)

        oSheets = oBook.Worksheets

        Dim str_CodigoAlumno As String = ""
        'Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
        Dim ds_Lista As DataSet
        Dim int_TotalCheck As Integer = 0
        For i As Integer = 0 To ds_ListaAlumnos.Tables(0).Rows.Count - 1
            If Convert.ToBoolean(ds_ListaAlumnos.Tables(0).Rows(i).Item("Chk")) = True Then
                int_TotalCheck += 1
            End If
        Next
        Dim int_NumAlumno As Integer = int_TotalCheck 'ds_ListaAlumnos.Tables(0).Rows.Count
        cantidadAlumnos = int_NumAlumno
        Dim int_PosSheet As Integer = 0

        For ix As Integer = 0 To ds_ListaAlumnos.Tables(0).Rows.Count - 1

            If Convert.ToBoolean(ds_ListaAlumnos.Tables(0).Rows(ix).Item("Chk")) = False Then
                If Not estadoDetenerProceso Then
                    Exit For
                End If
                Continue For
            End If

            int_PosSheet = int_PosSheet + 1
            str_CodigoAlumno = ds_ListaAlumnos.Tables(0).Rows(ix).Item("CodigoAlumno")
            ds_Lista = obl_rep_libretaNotas.FUN_LIS_REP_LibretaNotasSecundariaImp( _
                        str_CodigoAlumno, CodBimestre, int_CodigoAnioAcademico, int_idioma, 0, 0, 0, 0)

            oBook.Worksheets(int_PosSheet).Name = "Codigo " & str_CodigoAlumno
            oBook.Worksheets(int_PosSheet).Select()
            oSheet = oSheets.Item(int_PosSheet)
            oCells = oSheet.Cells

            Dim dtReporte As System.Data.DataTable = ds_Lista.Tables(0)

            Dim fila As Integer = 5
            Dim columna As Integer = 2
            Dim cont_columnas As Integer = 0
            Dim cont_filas As Integer = 0
            Dim str_Fila As String = ""

            cont_columnas = 0
            cont_filas = 0

            columna += 1

            ' Pintado de Titulo
            Dim str_GradoAula As String = ds_Lista.Tables(4).Rows(0).Item("DescIngles").ToString
            Dim str_NombreTutor As String = ds_Lista.Tables(4).Rows(0).Item("NombreTutor").ToString
            Dim str_NombreAlumno As String = ds_Lista.Tables(4).Rows(0).Item("NombreAlumno").ToString

            With oExcel.Range(oCells(1, 6), oCells(1, 24)) ' NAME
                .Merge()
                .Font.Name = "Arial"
                .Font.Size = 20
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .Value = "REPORT CARD"
            End With
            oExcel.Rows("1:1").RowHeight = 40 ' Listado de Cursos

            With oExcel.Range(oCells(2, 6), oCells(2, 8)) ' NAME
                .Merge()
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .Value = "NAME"
            End With

            With oExcel.Range(oCells(2, 9), oCells(2, 24)) ' NAME
                .Merge()
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .Value = str_NombreAlumno
            End With
            oExcel.Rows("2:2").RowHeight = 20

            With oExcel.Range(oCells(3, 6), oCells(3, 8)) ' NAME
                .Merge()
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .Value = "CLASS"
            End With

            With oExcel.Range(oCells(3, 9), oCells(3, 24)) ' CLASS
                .Merge()
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .Value = str_GradoAula
            End With
            oExcel.Rows("3:3").RowHeight = 20

            With oExcel.Range(oCells(4, 6), oCells(4, 8)) ' NAME
                .Merge()
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .Value = "TUTOR"
            End With

            With oExcel.Range(oCells(4, 9), oCells(4, 24)) ' TUTOR
                .Merge()
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .Value = str_NombreTutor
            End With
            oExcel.Rows("4:4").RowHeight = 20

            cuadradoCompleto(oExcel, oExcel.Range(oCells(2, 6), oCells(4, 24)))

            'Pintado de Fecha 
            With oCells(2, 30)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
                .HorizontalAlignment = int_HA_Left
                .Font.Bold = True
                .Value = "Date: " & StrConv(Format(Now, "MMMM d,yyyy").ToString, VbStrConv.ProperCase)  'Today.ToString("MMMM dd, yyyy")
            End With

            Dim colIni As Integer = 0
            Dim colFin As Integer = 0
            Dim lstPosCursos As New List(Of Integer)

            'oExcel.Range("A:A").ColumnWidth = 45
            For i As Integer = 0 To 60 ' Pintado de Cursos
                colIni = columna + (i * 3)
                colFin = colIni + 2
                With oExcel.Range(oCells(fila, colIni), oCells(fila, colFin))
                    .ColumnWidth = 3
                End With
            Next


            colIni = 0
            colFin = 0

            For i As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos

                colIni = columna + (i * 3)
                colFin = colIni + 2

                lstPosCursos.Add(colIni) ' agrego la columna de la posicion inicial

                With oExcel.Range(oCells(fila, colIni), oCells(fila, colFin))
                    .Merge()
                    .Value = ds_Lista.Tables(3).Rows(i).Item("NombreCurso")
                    .Font.Name = "Arial"
                    .Font.Size = 8
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Middle
                    .ColumnWidth = 3
                    .WrapText = True
                End With

                With oExcel.Range(oCells(fila + 1, colIni), oCells(fila + 1, colFin)) ' Fila 6 : Lista de Codigos Asignacion de Grupo
                    .Merge()
                    .Value = ds_Lista.Tables(3).Rows(i).Item("CodigoAsignacionGrupo")
                    .Font.Name = "Arial"
                    .Font.Size = 8
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Middle
                    .ColumnWidth = 3
                    .WrapText = True
                End With
            Next
            oExcel.Rows("5:5").RowHeight = 30 ' Listado de Cursos

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

                    With oCells(fila + i, columna)
                        .Font.Bold = True
                        .Value = str_Grupo
                    End With
                    If bool_PintadoGrupo = False Then
                        For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos
                            colIni = columna + 1 + (j * 3)
                            For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1 ' Pintado de Calificativos
                                With oCells(fila + i, colIni + k)
                                    .Font.Bold = True
                                    .Value = ds_Lista.Tables(1).Rows(k).Item("Abreviatura")
                                    .HorizontalAlignment = int_HA_Center
                                    .VerticalAlignment = int_VA_Middle
                                End With
                                oCells(fila + i + 1, colIni + k).Value = ds_Lista.Tables(1).Rows(k).Item("CodigoCalificativo")
                            Next
                        Next
                        bool_PintadoGrupo = True
                        fila += 1
                    End If
                    lstPos.Add(fila + i)
                    fila += 1
                End If

                oCells(fila + i, columna) = ds_Lista.Tables(0).Rows(i).Item("Criterio")

                int_CodigoCriterio = ds_Lista.Tables(0).Rows(i).Item("CodigoCriterio")
                int_CodigoAsignacionGrupo = 0
                colIni = 0

                For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1
                    colIni = columna + 1 + (j * 3)
                    int_CodigoAsignacionGrupo = oCells(6, colIni).Text()
                    int_CodigoCalificativo = 0
                    If bool_NotaCriterio Then : bool_NotaCriterio = False : End If
                    For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1
                        int_CodigoCalificativo = oCells(8, columna + 1 + k).Text()
                        For l As Integer = 0 To ds_Lista.Tables(2).Rows.Count - 1

                            If ds_Lista.Tables(2).Rows(l).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo And _
                                ds_Lista.Tables(2).Rows(l).Item("CodigoCalificativo") = int_CodigoCalificativo And _
                                ds_Lista.Tables(2).Rows(l).Item("CodigoCriterio") = int_CodigoCriterio Then

                                With oCells(fila + i, colIni + k)
                                    .value = "X"
                                    .HorizontalAlignment = int_HA_Center
                                    .VerticalAlignment = int_VA_Middle
                                End With
                                bool_NotaCriterio = True
                                Exit For

                            End If

                        Next
                        If bool_NotaCriterio Then : Exit For : End If
                    Next
                Next
            Next

            oExcel.Range(oCells(5, columna), oCells(fila - 1, columna)).EntireColumn.AutoFit() ' Listado de Criterios

            Dim objColor0 As Object = RGB(0, 0, 0) 'Negro
            Dim objColor1 As Object = RGB(191, 191, 191) 'Plomo

            pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor0)
            pintadoInterior(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor1)
            pintadoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(5, int_UltimaColumna)), objColor0)
            pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila, 2)), objColor0)

            pintadoCelda(oExcel, oExcel.Range(oCells(lstPos(1), 2), oCells(lstPos(1), int_UltimaColumna)), objColor0, 2) ' separador de grupos de criterio
            pintadoCelda(oExcel, oExcel.Range(oCells(int_UltimaFila, 2), oCells(int_UltimaFila, int_UltimaColumna)), objColor0, 4) ' separador Notas

            For i As Integer = 0 To lstPosCursos.Count - 1
                pintadoCelda(oExcel, oExcel.Range(oCells(5, lstPosCursos(i)), oCells(int_UltimaFila + 2, lstPosCursos(i))), objColor0, 1) ' separador de cursos
            Next

            With oCells(int_UltimaFila + 1, 2)
                .Value = "ACADEMIC PERFORMANCE"
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
            End With

            With oCells(int_UltimaFila + 2, 2)
                .Value = "OVERALL ATTAINMENT"
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
            End With

            oExcel.Rows((int_UltimaFila + 2).ToString & ":" & (int_UltimaFila + 2).ToString).RowHeight = 24 ' Listado de Cursos

            colIni = 0
            colFin = 0

            For i As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1 ' Pintado de Nota de Cursos 
                colIni = 0
                colIni = columna + 1 + (i * 3)
                colFin = colIni + 2
                int_CodigoAsignacionGrupo = IIf(oCells(6, colIni).Text().ToString.Length = 0, 0, oCells(6, colIni).Text())
                For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                    If ds_Lista.Tables(5).Rows(j).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo Then
                        With oExcel.Range(oCells(int_UltimaFila + 1, colIni), oCells(int_UltimaFila + 1, colFin))
                            .Merge()
                            .Value = ds_Lista.Tables(5).Rows(j).Item("NotaBimestre")
                            .Font.Name = "Arial"
                            .Font.Size = 8
                            .Font.Bold = True
                            .HorizontalAlignment = int_HA_Center
                            .VerticalAlignment = int_VA_Middle
                            .WrapText = True
                        End With
                        With oExcel.Range(oCells(int_UltimaFila + 1 + 1, colIni), oCells(int_UltimaFila + 1 + 1, colFin))
                            .Merge()
                            .Value = ds_Lista.Tables(5).Rows(j).Item("Observacion")
                            .Font.Name = "Arial"
                            .Font.Size = 8
                            .Font.Bold = True
                            .HorizontalAlignment = int_HA_Center
                            .VerticalAlignment = int_VA_Middle
                            .WrapText = True
                        End With
                        Exit For
                    End If
                Next
            Next

            ' PINTADO DE NOTAS 
            Dim int_FilaPintadoNotas As Integer = int_UltimaFila + 4 '51

            Dim int_FilaNotas As Integer = int_FilaPintadoNotas ' int_UltimaFilaComentario + 2
            Dim int_UltimaFilaNotas As Integer = int_FilaNotas + ds_Lista.Tables(5).Rows.Count
            Dim int_UltimaColumnaNotas As Integer = 2 + ds_Lista.Tables(5).Columns.Count - 6

            With oExcel.Range(oCells(int_FilaNotas, 2), oCells(int_FilaNotas, 9))
                .Merge()
                .Value = "TERM AND ANNUAL MARK"
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
            End With
            int_FilaNotas += 1

            For i As Integer = 0 To ds_Lista.Tables(5).Columns.Count - 1
                If i > 3 And i < 13 - 2 Then
                    If i = 12 - 2 Then
                        With oExcel.Range(oCells(int_FilaNotas, 2 + i - 4), oCells(int_FilaNotas, 2 + i - 4 + 1))
                            .Merge()
                            .Value = ds_Lista.Tables(5).Columns(i).ColumnName
                            .Font.Bold = True
                            .HorizontalAlignment = int_HA_Center
                            .VerticalAlignment = int_VA_Middle
                            .Font.Name = "Arial"
                            .Font.Size = 9
                        End With
                        For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                            With oExcel.Range(oCells(int_FilaNotas + 1 + j, 2 + i - 4), oCells(int_FilaNotas + 1 + j, 2 + i - 4 + 1))
                                .Merge()
                                .Value = ds_Lista.Tables(5).Rows(j).Item(i)
                                .Font.Bold = True
                                .HorizontalAlignment = int_HA_Center
                                .VerticalAlignment = int_VA_Middle
                                .Font.Name = "Arial"
                                .Font.Size = 8
                            End With
                        Next
                    Else
                        With oExcel.Range(oCells(int_FilaNotas, 2 + i - 4), oCells(int_FilaNotas, 2 + i - 4))
                            .Value = ds_Lista.Tables(5).Columns(i).ColumnName
                            .Font.Bold = True
                            .HorizontalAlignment = int_HA_Center
                            .VerticalAlignment = int_VA_Middle
                            .Font.Name = "Arial"
                            .Font.Size = 9
                        End With
                        For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                            With oExcel.Range(oCells(int_FilaNotas + 1 + j, 2 + i - 4), oCells(int_FilaNotas + 1 + j, 2 + i - 4))
                                .Value = ds_Lista.Tables(5).Rows(j).Item(i)
                                .Font.Bold = True
                                If i = 4 Then
                                    .HorizontalAlignment = int_HA_Left
                                    .VerticalAlignment = int_VA_Middle
                                    .Font.Size = 10
                                Else
                                    .HorizontalAlignment = int_HA_Center
                                    .VerticalAlignment = int_VA_Middle
                                    .Font.Size = 8
                                End If
                                .Font.Name = "Arial"
                            End With
                        Next
                    End If
                End If
            Next
            pintadoCompleto(oExcel, oExcel.Range(oCells(int_FilaNotas - 1, 2), oCells(int_UltimaFilaNotas + 1, int_UltimaColumnaNotas)), objColor0)

            With oExcel.Range(oCells(int_UltimaFilaNotas + 2, 2), oCells(int_UltimaFilaNotas + 2, int_UltimaColumnaNotas)) 'oCells(int_UltimaFilaNotas + 2, 2)
                .Merge()
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .Value = "Note: The note indicates 'P' value test is pending."
                .Font.Bold = True
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

            With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8))
                .Merge()
                .Value = "STUDENT PROFILE"
                .Font.Bold = True
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .Font.Name = "Arial"
                .Font.Size = 10
            End With

            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8)), objColor0)

            For i As Integer = 0 To ds_Lista.Tables(8).Rows.Count - 1

                With oCells(int_FilaProfile - 1, int_ColumnaProfile + 8 + 1 + i * 2)
                    .Value = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
                    posCelda = New posicionCelda
                    posCelda.posFila = int_FilaProfile - 1
                    posCelda.posColumna = int_ColumnaProfile + 8 + 1 + i * 2
                    posCelda.Codigo = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
                    lstPosProfile.Add(posCelda)
                End With

                With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile + 8 + 1 + i * 2), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8 + 1 + i * 2))
                    .Merge()
                    .Value = ds_Lista.Tables(8).Rows(i).Item("Calificativo")
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Bottom
                    .WrapText = True
                    .Orientation = 90
                    .AddIndent = False
                    .IndentLevel = 0
                    .ShrinkToFit = False
                    .Font.Name = "Arial"
                    .Font.Size = 9
                End With
                With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile + 8 + 2 + i * 2), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8 + 2 + i * 2))
                    .Merge()
                    .Value = ds_Lista.Tables(8).Rows(i).Item("CalificativoES")
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Bottom
                    .WrapText = True
                    .Orientation = 90
                    .AddIndent = False
                    .IndentLevel = 0
                    .ShrinkToFit = False
                    .Font.Name = "Arial"
                    .Font.Size = 9
                End With
            Next

            Dim int_codCriterio As Integer = 0
            Dim int_codCalificativo As Integer = 0
            Dim int_codCalificativoAux As Integer = 0
            Dim int_codCalificativoPos As Integer = 0

            For i As Integer = 0 To ds_Lista.Tables(7).Rows.Count - 1

                With oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile - 1)
                    .Value = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
                    int_codCriterio = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
                    posCelda = New posicionCelda
                    posCelda.posFila = int_FilaProfile + 8 + i * 2
                    posCelda.posColumna = int_ColumnaProfile - 1
                    posCelda.Codigo = 0
                    lstPosProfile.Add(posCelda)
                End With

                With oExcel.Range(oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile), oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile + 8))
                    .Merge()
                    .Value = ds_Lista.Tables(7).Rows(i).Item("Criterio")
                    .HorizontalAlignment = int_HA_Left
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .Font.Name = "Arial"
                    .Font.Size = 9
                End With
                With oExcel.Range(oCells(int_FilaProfile + 9 + i * 2, int_ColumnaProfile), oCells(int_FilaProfile + 9 + i * 2, int_ColumnaProfile + 8))
                    .Merge()
                    .Value = ds_Lista.Tables(7).Rows(i).Item("CriterioES")
                    .HorizontalAlignment = int_HA_Left
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .Font.Name = "Arial"
                    .Font.Size = 9
                End With

                For k As Integer = 0 To ds_Lista.Tables(9).Rows.Count - 1
                    If int_codCriterio = ds_Lista.Tables(9).Rows(k).Item("CodigoCriterio") Then
                        int_codCalificativo = ds_Lista.Tables(9).Rows(k).Item("CodigoCalificativo")
                        For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
                            If posCel.Codigo > 0 Then
                                If posCel.Codigo = int_codCalificativo Then
                                    With oExcel.Range(oCells(int_FilaProfile + 8 + i * 2, posCel.posColumna), oCells(int_FilaProfile + 9 + i * 2, posCel.posColumna + 1))
                                        .Merge()
                                        .Value = "X"
                                        .Font.Bold = True
                                        .HorizontalAlignment = int_HA_Center
                                        .VerticalAlignment = int_VA_Middle
                                        .Font.Name = "Arial"
                                        .Font.Size = 9
                                    End With
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next
            Next

            pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor0)

            For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
                With oCells(posCel.posFila, posCel.posColumna)
                    .value = ""
                End With
                If posCel.Codigo = 0 Then
                    pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila, posCel.posColumna + 1), oCells(posCel.posFila + 1, 8 + int_UltimaColumnaProfile)), objColor0)
                End If
                If posCel.Codigo > 0 Then
                    pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila + 1, posCel.posColumna), oCells(int_UltimaFilaProfile, posCel.posColumna + 1)), objColor0)
                End If
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

            With oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8))
                .Merge()
                .Value = "ATTENDANCE                                  Asistencia"
                .Font.Bold = True
                .WrapText = True
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .Font.Name = "Arial"
                .Font.Size = 10
            End With

            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8)), objColor0)
            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 10)), objColor0)

            Dim str_Bimestre As String = ""

            For i As Integer = 0 To 4
                Select Case i
                    Case 0 : str_Bimestre = "TERM I"
                    Case 1 : str_Bimestre = "TERM II"
                    Case 2 : str_Bimestre = "TERM III"
                    Case 3 : str_Bimestre = "TERM IV"
                    Case 4 : str_Bimestre = "AVERAGE"
                End Select
                With oCells(int_FilaAsistencia - 1, int_ColumnaAsistencia + 8 + 1 + i * 2)
                    .Value = "" 'i + 1
                    posCelda2 = New posicionCelda
                    posCelda2.posFila = int_FilaAsistencia - 1
                    posCelda2.posColumna = int_ColumnaAsistencia + 8 + 1 + i * 2
                    posCelda2.Codigo = i + 1
                    lstPosAsistencia.Add(posCelda2)
                End With
                With oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia + 8 + 1 + i * 2), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 2 + i * 2))
                    .Merge()
                    .Value = str_Bimestre
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Bottom
                    .WrapText = True
                    .Orientation = 90
                    .AddIndent = False
                    .IndentLevel = 0
                    .ShrinkToFit = False
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                End With
            Next

            Dim int_codBimestre As Integer = 0
            Dim int_codBimestreAux As Integer = 0

            Dim lstPosAsis As New List(Of posicionCelda)
            Dim posCelda3 As posicionCelda

            ' TARDANZAS Y CONDUCTA
            ' TARDANZAS
            With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 4))
                .Merge()
                .Value = "LATES          Tardanzas"
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
            End With
            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 4)), objColor0)

            With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 9, int_ColumnaAsistencia + 8))
                .Merge()
                .Value = "Justified          Justificado"
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 9
            End With
            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 9, int_ColumnaAsistencia + 8 + 10)), objColor0)

            With oExcel.Range(oCells(int_FilaAsistencia + 10, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 8))
                .Merge()
                .Value = "Unjustified          Injustificado"
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 9
            End With
            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 10, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 8 + 10)), objColor0)

            ' FALTAS
            With oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 4))
                .Merge()
                .Value = "ABSENCES          Ausencias"
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
            End With
            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 4)), objColor0)

            With oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 13, int_ColumnaAsistencia + 8))
                .Merge()
                .Value = "Justified          Justificado"
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 9
            End With
            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 13, int_ColumnaAsistencia + 8 + 10)), objColor0)

            With oExcel.Range(oCells(int_FilaAsistencia + 14, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 8))
                .Merge()
                .Value = "Unjustified          Injustificado"
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 9
            End With
            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 14, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 8 + 10)), objColor0)

            ' DEMERITOS
            With oExcel.Range(oCells(int_FilaAsistencia + 16, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 17, int_ColumnaAsistencia + 8))
                .Merge()
                .Value = "DEMERITS                                            Deméritos"
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
            End With
            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 16, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 17, int_ColumnaAsistencia + 18)), objColor0)

            ' MERITOS
            With oExcel.Range(oCells(int_FilaAsistencia + 18, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 19, int_ColumnaAsistencia + 8))
                .Merge()
                .Value = "MERITS                                                      Méritos"
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
            End With
            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 18, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 19, int_ColumnaAsistencia + 18)), objColor0)

            ' CONDUCT MARK
            With oExcel.Range(oCells(int_FilaAsistencia + 20, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 21, int_ColumnaAsistencia + 8))
                .Merge()
                .Value = "CONDUCT MARK                                            Nota de conducta"
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
            End With
            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 20, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 21, int_ColumnaAsistencia + 18)), objColor0)

            Dim pos_Bimestre As Integer = 0
            Dim int_filapos As Integer = 0
            Dim int_colpos As Integer = 0
            Dim int_filaini As Integer = 0
            Dim str_Rango As String = ""

            If ds_Lista.Tables(10).Rows.Count > 0 Then
                For i As Integer = 0 To ds_Lista.Tables(10).Rows.Count - 1

                    int_filapos = 8 + i * 2 : int_colpos = 9 : pos_Bimestre = 0
                    With oExcel.Range(oCells(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      oCells(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                        .Merge()
                        .Value = ds_Lista.Tables(10).Rows(i).Item("Bim1")
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With

                    pos_Bimestre = 2
                    With oExcel.Range(oCells(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      oCells(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                        .Merge()
                        .Value = ds_Lista.Tables(10).Rows(i).Item("Bim2")
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With

                    pos_Bimestre = 4
                    With oExcel.Range(oCells(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      oCells(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                        .Merge()
                        .Value = ds_Lista.Tables(10).Rows(i).Item("Bim3")
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With

                    pos_Bimestre = 6
                    With oExcel.Range(oCells(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      oCells(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                        .Merge()
                        .Value = ds_Lista.Tables(10).Rows(i).Item("Bim4")
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With

                    str_Rango = DevLetraColumna(int_ColumnaAsistencia + int_colpos) + (int_FilaAsistencia + int_filapos).ToString + ":" + _
                                DevLetraColumna(int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre) + (int_FilaAsistencia + int_filapos + 1).ToString
                    pos_Bimestre = 8
                    With oExcel.Range(oCells(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      oCells(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                        .Merge()
                        .Value = ds_Lista.Tables(10).Rows(i).Item("operacion") '"=" & ds_Lista.Tables(10).Rows(i).Item("operacion") & "(" + str_Rango + ")"
                        .NumberFormat = "0"
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With

                Next
            End If

            For Each posCel As posicionCelda In lstPosAsistencia ' Limpio todos los codigos pintados previamente
                If posCel.Codigo > 0 Then
                    pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila + 1, posCel.posColumna), oCells(int_UltimaFilaAsistencia, posCel.posColumna + 1)), objColor0)
                End If
            Next

            ' PINTADO DE COMENTARIOS 
            Dim int_FilaComentario As Integer = 60 ' 58 ' 54 ' 51 ' int_UltimaFila + 3
            Dim int_MaxNumFilasComentario As Integer = 13 * 2 '13 * 2
            Dim int_UltimaFilaComentario As Integer = int_FilaComentario + int_MaxNumFilasComentario + 1 'ds_Lista.Tables(6).Rows.Count
            int_FilaComentario += 1
            With oCells(int_FilaComentario, 2)
                .Value = "COMMENTS"
                .Font.Bold = True
            End With
            int_FilaComentario += 1

            Dim pos_ComentAux As Integer = 0
            Dim int_FilasPintadas As Integer = 0

            'maximo 17 cursos
            int_UltimaColumna = 53

            For i As Integer = 0 To ds_Lista.Tables(6).Rows.Count - 1 ' Pintado de Oservaciones de Cursos 

                'Num maximo de cursos y comentarios 9
                int_FilasPintadas += 1
                With oExcel.Range(oCells(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, 2), _
                                  oCells(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, int_UltimaColumna))
                    .RowHeight = 40 ' 30
                End With

                If ds_Lista.Tables(6).Rows(i).Item("Observacion") IsNot DBNull.Value Then

                    With oExcel.Range(oCells(int_FilaComentario - 2 + int_FilasPintadas * 2, 2), _
                                      oCells(int_FilaComentario - 2 + int_FilasPintadas * 2, int_UltimaColumna))
                        .Merge()
                        .Value = ds_Lista.Tables(6).Rows(i).Item("Curso") & ":"
                        .Font.Name = "Arial"
                        .Font.Size = 11 '10
                        .Font.Bold = True
                        .HorizontalAlignment = int_HA_Left
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                    End With

                    With oExcel.Range(oCells(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, 2), _
                                      oCells(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, int_UltimaColumna))
                        .Merge()
                        .Value = ds_Lista.Tables(6).Rows(i).Item("Observacion")
                        .Font.Name = "Arial"
                        .Font.Size = 11 ' 9
                        .HorizontalAlignment = int_HA_Left
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .RowHeight = 40
                    End With

                End If
            Next

            'pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaComentario, 2), _
            '                                   oCells(int_UltimaFilaComentario, int_UltimaColumna)), objColor0)

            pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaComentario, 2), _
                                             oCells(int_UltimaFilaComentario, int_UltimaColumna)), objColor0)

            int_FilaAsistencia = int_UltimaFilaComentario + 10 - 25


            ' FIRMAS
            With oExcel.Range(oCells(int_FilaAsistencia + 23, 2), oCells(int_FilaAsistencia + 23, 5))
                .Merge()
                .Value = "SIGNATURE OF TUTOR"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With
            pintadoCelda(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 23, 2), oCells(int_FilaAsistencia + 23, 5)), objColor0, 2)

            With oExcel.Range(oCells(int_FilaAsistencia + 23, 8), oCells(int_FilaAsistencia + 23, 23))
                .Merge()
                .Value = "SIGNATURE OF PARENT"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With
            pintadoCelda(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 23, 8), oCells(int_FilaAsistencia + 23, 23)), objColor0, 2)

            oExcel.Range("B:B").ColumnWidth = 45

            oExcel.Rows("6:6").Delete() ' Elimino la fila de codigos de asignacion de grupos
            oExcel.Rows("7:7").Delete() ' Elimino la fila de codigos de los calificativos
            oExcel.Rows("5:5").Insert()
            oExcel.Rows("5:5").RowHeight = 5 ' Listado de Cursos
            borrarPintado(oExcel, oExcel.Range(oCells(5, 2), oCells(5, int_UltimaColumna)))
            cuadradoCompleto(oExcel, oExcel.Range(oCells(2, 6), oCells(4, 24)))
            'oExcel.Columns("A:A").Delete()
            oExcel.Range("A:A").ColumnWidth = 3
            oExcel.ActiveWindow.Zoom = 75

            With oExcel.ActiveSheet.PageSetup
                .LeftHeader = ""
                .CenterHeader = ""
                .RightHeader = ""
                .LeftFooter = ""
                .CenterFooter = ""
                .RightFooter = ""
                .LeftMargin = oExcel.Application.InchesToPoints(0.25)
                .RightMargin = oExcel.Application.InchesToPoints(0.25)
                .TopMargin = oExcel.Application.InchesToPoints(0.75)
                .BottomMargin = oExcel.Application.InchesToPoints(0.75)
                .HeaderMargin = oExcel.Application.InchesToPoints(0.3)
                .FooterMargin = oExcel.Application.InchesToPoints(0.3)


                .PrintHeadings = False
                .PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                .PrintQuality = 600
                .CenterHorizontally = False
                .CenterVertically = False
                .Orientation = 2
                .Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = OrderedDictionary xlDownThenOver
                .BlackAndWhite = False
                .Zoom = False
                .FitToPagesWide = 1
                .FitToPagesTall = False
                '.PrintErrors = xlPrintErrorsDisplayed
                .OddAndEvenPagesHeaderFooter = False
                .DifferentFirstPageHeaderFooter = False
                .ScaleWithDocHeaderFooter = True
                .AlignMarginsHeaderFooter = True
                .EvenPage.LeftHeader.Text = ""
                .EvenPage.CenterHeader.Text = ""
                .EvenPage.RightHeader.Text = ""
                .EvenPage.LeftFooter.Text = ""
                .EvenPage.CenterFooter.Text = ""
                .EvenPage.RightFooter.Text = ""
                .FirstPage.LeftHeader.Text = ""
                .FirstPage.CenterHeader.Text = ""
                .FirstPage.RightHeader.Text = ""
                .FirstPage.LeftFooter.Text = ""
                .FirstPage.CenterFooter.Text = ""
                .FirstPage.RightFooter.Text = ""


            End With

            alumnosProcesado += 1

            If Not estadoDetenerProceso Then
                Exit For
            End If

        Next

        oBook.SaveAs(sFile)
        estadoOperacion = True
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


    Private Function generarConsolidadoSecundaria2(ByVal CodSalon As Integer, ByVal CodBimestre As Integer, ByVal nombreArchivo As String) As String

        Dim ds_Lista As New DataSet
        Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
        Dim int_TipoRep As Integer = 1 ' update 05/12/2012
        ds_Lista = obl_rep_libretaNotas.FUN_LIS_REP_ConsolidadoNotasSecundaria(CodSalon, CodBimestre, int_TipoRep, 0, 0, 0, 0)

        'LLenado de reporte
        'nombreArchivo = ExportarReporteconsolidadoSecundaria(nombreArchivo, ds_Lista)



        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        Dim rutaLibreta As String = System.Configuration.ConfigurationManager.AppSettings("rutaLibreta").ToString()
        Dim rutaPlantilla As String = System.Configuration.ConfigurationManager.AppSettings("ConsolidadoSecundaria").ToString()

        Dim rutamadre As String = Environment.CurrentDirectory
        nombreRep = nombreArchivo


        MsgBox("rutamadre: " & rutamadre)


        ' Archivo excel a grabar
        sFile = nombreRep

        ' Plantilla a cargar
        sTemplate = rutamadre & rutaPlantilla

        oExcel.Visible = True
        oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Consolidado"
        oCells = oSheet.Cells

        'LlenarPlantillaReporteConsolidadoSecundaria(ds_Lista, oCells, oExcel)

        Dim dtReporte As System.Data.DataTable = ds_Lista.Tables(0)

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        ' Pintado de Titulo
        Dim str_GradoAula As String = ds_Lista.Tables(3).Rows(0).Item("DescEspaniol").ToString
        Dim str_BimestreAnio As String = ds_Lista.Tables(3).Rows(0).Item("DescBimestreAnio").ToString
        Dim str_TipoReporte As String = "(Asignaturas Oficiales)"

        'oCells(3, 3) = "Consolidado de Evaluación - " + str_Titulo
        With oExcel.Range(oCells(3, 2), oCells(3, 27))
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 16
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .Value = "Consolidado de Evaluación - " + str_GradoAula + " " + str_TipoReporte + " " + str_BimestreAnio
        End With
        oExcel.Rows("3:3").RowHeight = 40

        'Pintado de Fecha 
        With oCells(1, 24)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
            .HorizontalAlignment = int_HA_Left
            .Font.Bold = True
            .Value = "Fecha: " & Now.Date
        End With

        'Pintado de Hora 
        With oCells(2, 24)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
            .HorizontalAlignment = int_HA_Left
            .Font.Bold = True
            .Value = "Hora: " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
        End With


        ' Pintado de Cabeceras
        oCells(fila, columna) = "Nro"
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Font.Name = "Arial"
            .Font.Size = 11
            .HorizontalAlignment = 3
        End With
        columna += 1

        oCells(fila, columna) = "Apellidos y Nombres"
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Font.Name = "Arial"
            .Font.Size = 11
            .HorizontalAlignment = 3
        End With

        cont_columnas = 0
        cont_filas = 0
        columna += 1

        For i As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1 ' Pintado de Cursos
            oCells(fila, columna + i) = ds_Lista.Tables(0).Rows(i).Item("NombreCurso")
            With oExcel.Range(oCells(fila, columna + i), oCells(fila, columna + i))
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 11
                .HorizontalAlignment = 3
                .ColumnWidth = 4
            End With
            oCells(fila + 1, columna + i) = ds_Lista.Tables(0).Rows(i).Item("CodigoAsignacionGrupo") ' Fila 6
        Next

        For i As Integer = 0 To 35
            With oExcel.Range(oCells(fila, columna + i), oCells(fila, columna + i))
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 11
                .HorizontalAlignment = 3
                If i >= 20 Then
                    If i = 20 Or i = 21 Then
                        .ColumnWidth = 8
                    Else
                        .ColumnWidth = 6
                    End If
                Else
                    .ColumnWidth = 6
                End If
            End With

            If i >= 20 Then
                If i = 20 Then
                    oCells(fila, columna + i) = "PROMEDIO"
                ElseIf i = 21 Then
                    oCells(fila, columna + i) = "PUNTAJE TOTAL"
                ElseIf i = 22 Then
                    oCells(fila, columna + i) = "APROBADOS"
                ElseIf i = 23 Then
                    oCells(fila, columna + i) = "DESAPROBADOS"
                ElseIf i = 24 Then
                    oCells(fila, columna + i) = "CONDUCTA"
                ElseIf i = 25 Then
                    oCells(fila, columna + i) = ""
                ElseIf i = 26 Then
                    oCells(fila, columna + i) = "Inasist. Injustificadas"
                ElseIf i = 27 Then
                    oCells(fila, columna + i) = "Inasist. Justificadas"
                ElseIf i = 28 Then
                    oCells(fila, columna + i) = "Tardanzas Injustificadas"
                ElseIf i = 29 Then
                    oCells(fila, columna + i) = "Tardanzas Justificadas"
                ElseIf i = 30 Then
                    oCells(fila, columna + i) = "Tercio"
                ElseIf i = 31 Then
                    oCells(fila, columna + i) = "Orden de Mérito"
                End If
            End If

        Next
        oExcel.Rows("5:5").RowHeight = 129

        cont_columnas = 0
        cont_filas = 0
        fila += 1
        fila += 1
        columna = 2

        Dim int_CodigoAsignacionGrupo As Integer = 0
        Dim str_CodigoAlumno As String = ""
        Dim int_Idx As Integer = 0
        Dim str_Nota As String = ""

        For i As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1 ' Pintado de Alumnos
            int_Idx = i + 1
            str_CodigoAlumno = ds_Lista.Tables(1).Rows(i).Item("CodigoAlumno")
            oCells(fila + i, columna) = int_Idx
            oCells(fila + i, columna + 1) = ds_Lista.Tables(1).Rows(i).Item("NombreCompleto")
            With oExcel.Range(oCells(fila + i, columna), oCells(fila + i, columna))
                .Font.Name = "Arial"
                .Font.Size = 11
                .RowHeight = 15
            End With
            int_CodigoAsignacionGrupo = 0
            For j As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1
                int_CodigoAsignacionGrupo = oCells(6, j + 4).Text()
                For k As Integer = 0 To ds_Lista.Tables(2).Rows.Count - 1
                    If ds_Lista.Tables(2).Rows(k).Item("CodigoAlumno") = str_CodigoAlumno And ds_Lista.Tables(2).Rows(k).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo Then
                        oCells(fila + i, j + 4) = ds_Lista.Tables(2).Rows(k).Item("NotaFinal")
                        With oCells(fila + i, j + 4)
                            .NumberFormat = "00"
                            .HorizontalAlignment = 3
                            .font.bold = True
                            If ds_Lista.Tables(2).Rows(k).Item("NotaFinal") IsNot DBNull.Value Then
                                If Convert.ToInt16(ds_Lista.Tables(2).Rows(k).Item("NotaFinal")) > 10 Then
                                    .Font.Color = RGB(22, 54, 92)
                                Else
                                    .Font.Color = RGB(255, 0, 0)
                                End If
                            End If
                        End With
                        Exit For
                    End If
                Next
            Next

            For x As Integer = 0 To ds_Lista.Tables(4).Rows.Count - 1 ' Pintado de Nota de Conducta Alumnos
                If ds_Lista.Tables(4).Rows(x).Item("CodigoAlumno") = str_CodigoAlumno Then

                    With oCells(fila + i, columna + 22) ' Promedio
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Promedio")
                        '.NumberFormat = "00.00"
                    End With
                    With oCells(fila + i, columna + 23) ' Puntaje total
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("PuntajeTotal")
                    End With
                    With oCells(fila + i, columna + 24) ' Cursos aprobados
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Aprobados")
                    End With
                    With oCells(fila + i, columna + 25) ' Cursos desaprobados
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Desaprobados")
                    End With
                    With oCells(fila + i, columna + 26) ' Nota Conducta
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Conducta")
                    End With
                    With oCells(fila + i, columna + 27) ' 
                        .HorizontalAlignment = 3
                        .Value = ""
                    End With
                    With oCells(fila + i, columna + 28) ' inasistencias injustificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("FaltasSinJustificar")
                    End With
                    With oCells(fila + i, columna + 29) ' inasistencias justificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("FaltasJustificadas")
                    End With
                    With oCells(fila + i, columna + 30) ' tardanzas injustificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("TardanzasSinJustificar")
                    End With
                    With oCells(fila + i, columna + 31) ' tardanzas justificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("TardanzasJustificadas")
                    End With
                    With oCells(fila + i, columna + 32) ' tercio
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Tercio")
                    End With
                    With oCells(fila + i, columna + 33) ' orden de merito
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("OrdenMerito")
                    End With

                    Exit For
                End If
            Next
        Next

        oExcel.Range(oCells(5, columna), oCells(fila - 1, columna)).EntireColumn.AutoFit() ' Codigo alumno
        oExcel.Range(oCells(5, columna + 1), oCells(fila - 1, columna + 1)).EntireColumn.AutoFit() ' Nombre alumno

        oExcel.Rows("6:6").Delete() ' Elimino la fila de codigos de asignacion de grupos

        Dim int_NumAlumnos As Integer = ds_Lista.Tables(1).Rows.Count
        Dim int_NumCursos As Integer = ds_Lista.Tables(0).Rows.Count
        Dim int_MaxNumCursos As Integer = 20
        Dim int_UltimaFila As Integer = fila - 2 + int_NumAlumnos
        Dim int_UltimaColumna As Integer = columna + 1 + int_NumCursos + (int_MaxNumCursos - int_NumCursos) + 12 ' 12 columnas con campos calculados

        cuadradoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila, int_UltimaColumna)))

        ' Campos calculados
        With oCells(int_UltimaFila + 2, columna + 1)
            .value = "Promedio del Curso"
        End With
        Dim str_Rango As String = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 2, 4 + i)
                .Value = "=PROMEDIO(" + str_Rango + ")"
                .NumberFormat = "0,0"
                .HorizontalAlignment = 3
            End With
        Next

        Dim cond1 As String = "," & """>" & "10""" & ")"
        Dim cond2 As String = "," & """<" & "11""" & ")"

        With oCells(int_UltimaFila + 3, columna + 1)
            .value = "Nro de Alumnos Aprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 3, 4 + i)
                .Value = "=CONTAR.SI(" + str_Rango + cond1
                .NumberFormat = "0"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 4, columna + 1)
            .value = "Nro de Alumnos Desaprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 4, 4 + i)
                .Value = "=CONTAR.SI(" + str_Rango + cond2
                .NumberFormat = "0"
                .HorizontalAlignment = 3
            End With
        Next

        Dim cond3 As String = ")*100/" & int_NumAlumnos.ToString

        With oCells(int_UltimaFila + 5, columna + 1)
            .value = "% de Alumnos Aprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 5, 4 + i)
                .Value = "=(CONTAR.SI(" + str_Rango + cond1 & cond3
                .NumberFormat = "0,0"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 6, columna + 1)
            .value = "% de Alumnos Desaprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 6, 4 + i)
                .Value = "=100-(CONTAR.SI(" + str_Rango + cond1 & cond3
                .NumberFormat = "0,0"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 7, columna + 1)
            .value = "Nota Máxima"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 7, 4 + i)
                .Value = "=MAX(" + str_Rango + ")"
                .NumberFormat = "00"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 8, columna + 1)
            .value = "Nota Mínima"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 8, 4 + i)
                .Value = "=MIN(" + str_Rango + ")"
                .NumberFormat = "00"
                .HorizontalAlignment = 3
            End With
        Next

        cuadradoCompleto(oExcel, oExcel.Range(oCells(int_UltimaFila + 2, columna + 1), oCells(int_UltimaFila + 8, 4 + int_NumCursos - 1)))

        oExcel.Columns("A:A").Delete()

        oExcel.ActiveWindow.Zoom = 75



        With oExcel.ActiveSheet.PageSetup
            .LeftHeader = ""
            .CenterHeader = ""
            .RightHeader = ""
            .LeftFooter = ""
            .CenterFooter = ""
            .RightFooter = ""
            .LeftMargin = oExcel.Application.InchesToPoints(0)
            .RightMargin = oExcel.Application.InchesToPoints(0)
            .TopMargin = oExcel.Application.InchesToPoints(0)
            .BottomMargin = oExcel.Application.InchesToPoints(0)
            .HeaderMargin = oExcel.Application.InchesToPoints(0)
            .FooterMargin = oExcel.Application.InchesToPoints(0)
            .PrintHeadings = False
            .PrintGridlines = False
            '.PrintComments = xlPrintNoComments
            .PrintQuality = 600
            .CenterHorizontally = False
            .CenterVertically = False
            .Orientation = 2
            .Draft = False
            '.PaperSize = xlPaperLetter
            '.FirstPageNumber = xlAutomatic
            '.Order = OrderedDictionary xlDownThenOver
            .BlackAndWhite = False
            .Zoom = False
            .FitToPagesWide = 1
            .FitToPagesTall = False
            '.PrintErrors = xlPrintErrorsDisplayed
            .OddAndEvenPagesHeaderFooter = False
            .DifferentFirstPageHeaderFooter = False
            .ScaleWithDocHeaderFooter = True
            .AlignMarginsHeaderFooter = True
            .EvenPage.LeftHeader.Text = ""
            .EvenPage.CenterHeader.Text = ""
            .EvenPage.RightHeader.Text = ""
            .EvenPage.LeftFooter.Text = ""
            .EvenPage.CenterFooter.Text = ""
            .EvenPage.RightFooter.Text = ""
            .FirstPage.LeftHeader.Text = ""
            .FirstPage.CenterHeader.Text = ""
            .FirstPage.RightHeader.Text = ""
            .FirstPage.LeftFooter.Text = ""
            .FirstPage.CenterFooter.Text = ""
            .FirstPage.RightFooter.Text = ""
        End With


        MsgBox("sFile: " & sFile)

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

        MsgBox("nombreRep: " & nombreRep)

        Return nombreRep



        'Return nombreArchivo

    End Function



    ' Libreta
    Public Shared Function ExportarReporteLibretaSecundaria( _
        ByVal NombreArchivo As String, _
        ByVal ds_ListaAlumnos As System.Data.DataSet, _
        ByVal int_CodigoAsignacionAula As Integer, _
        ByVal int_CodigoBimestre As Integer, _
        ByVal int_CodigoAnioAcademico As Integer) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks
        Dim oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets
        Dim oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        Try

            Dim rutaLibreta As String = System.Configuration.ConfigurationManager.AppSettings("rutaLibreta").ToString()
            Dim rutaPlantilla As String = System.Configuration.ConfigurationManager.AppSettings("LibretaSecundaria").ToString()

            Dim rutamadre As String = Environment.CurrentDirectory
            nombreRep = NombreArchivo

            ' Archivo excel a grabar
            sFile = nombreRep

            ' Plantilla a cargar
            sTemplate = rutamadre & rutaPlantilla

            oExcel.Visible = False
            oExcel.DisplayAlerts = False

            oBooks = oExcel.Workbooks
            oBooks.Open(sTemplate) 'Load colorful template with graph
            oBook = oBooks.Item(1)

            oSheets = oBook.Worksheets

            Dim str_CodigoAlumno As String = ""
            Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
            Dim ds_Lista As DataSet
            Dim int_NumAlumno As Integer = ds_ListaAlumnos.Tables(0).Rows.Count


            For i As Integer = 0 To ds_ListaAlumnos.Tables(0).Rows.Count - 1
                str_CodigoAlumno = ds_ListaAlumnos.Tables(0).Rows(i).Item("CodigoAlumno")
                ds_Lista = obl_rep_libretaNotas.FUN_LIS_REP_LibretaNotasSecundaria(str_CodigoAlumno, int_CodigoBimestre, int_CodigoAnioAcademico, 0, 0, 0, 0)

                oBook.Worksheets(i + 1).Name = "Codigo " & str_CodigoAlumno
                oBook.Worksheets(i + 1).Select()
                oSheet = oSheets.Item(i + 1)
                oCells = oSheet.Cells
                LlenarPlantillaReporteLibretaSecundaria(ds_Lista, oCells, oExcel)

                With oExcel.ActiveSheet.PageSetup
                    .LeftHeader = ""
                    .CenterHeader = ""
                    .RightHeader = ""
                    .LeftFooter = ""
                    .CenterFooter = ""
                    .RightFooter = ""
                    .LeftMargin = oExcel.Application.InchesToPoints(0.25)
                    .RightMargin = oExcel.Application.InchesToPoints(0.25)
                    .TopMargin = oExcel.Application.InchesToPoints(0.75)
                    .BottomMargin = oExcel.Application.InchesToPoints(0.75)
                    .HeaderMargin = oExcel.Application.InchesToPoints(0.3)
                    .FooterMargin = oExcel.Application.InchesToPoints(0.3)
                    .PrintHeadings = False
                    .PrintGridlines = False
                    '.PrintComments = xlPrintNoComments
                    .PrintQuality = 600
                    .CenterHorizontally = False
                    .CenterVertically = False
                    .Orientation = 2
                    .Draft = False
                    '.PaperSize = xlPaperLetter
                    '.FirstPageNumber = xlAutomatic
                    '.Order = OrderedDictionary xlDownThenOver
                    .BlackAndWhite = False
                    .Zoom = False
                    .FitToPagesWide = 1
                    .FitToPagesTall = False
                    '.PrintErrors = xlPrintErrorsDisplayed
                    .OddAndEvenPagesHeaderFooter = False
                    .DifferentFirstPageHeaderFooter = False
                    .ScaleWithDocHeaderFooter = True
                    .AlignMarginsHeaderFooter = True
                    .EvenPage.LeftHeader.Text = ""
                    .EvenPage.CenterHeader.Text = ""
                    .EvenPage.RightHeader.Text = ""
                    .EvenPage.LeftFooter.Text = ""
                    .EvenPage.CenterFooter.Text = ""
                    .EvenPage.RightFooter.Text = ""
                    .FirstPage.LeftHeader.Text = ""
                    .FirstPage.CenterHeader.Text = ""
                    .FirstPage.RightHeader.Text = ""
                    .FirstPage.LeftFooter.Text = ""
                    .FirstPage.CenterFooter.Text = ""
                    .FirstPage.RightFooter.Text = ""
                End With

            Next

            oBook.SaveAs(sFile)
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

        Catch ex As Exception

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

        End Try

    End Function

    Private Shared Function LlenarPlantillaReporteLibretaSecundaria( _
        ByVal ds_Lista As System.Data.DataSet, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application) As String

        Dim dtReporte As System.Data.DataTable = ds_Lista.Tables(0)

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        cont_columnas = 0
        cont_filas = 0

        columna += 1

        ' Pintado de Titulo
        Dim str_GradoAula As String = ds_Lista.Tables(4).Rows(0).Item("DescIngles").ToString
        Dim str_NombreTutor As String = ds_Lista.Tables(4).Rows(0).Item("NombreTutor").ToString
        Dim str_NombreAlumno As String = ds_Lista.Tables(4).Rows(0).Item("NombreAlumno").ToString

        With oExcel.Range(oCells(1, 6), oCells(1, 24)) ' NAME
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 20
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .Value = "REPORT CARD"
        End With
        oExcel.Rows("1:1").RowHeight = 40 ' Listado de Cursos

        With oExcel.Range(oCells(2, 6), oCells(2, 8)) ' NAME
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = "NAME"
        End With

        With oExcel.Range(oCells(2, 9), oCells(2, 24)) ' NAME
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = str_NombreAlumno
        End With
        oExcel.Rows("2:2").RowHeight = 20

        With oExcel.Range(oCells(3, 6), oCells(3, 8)) ' NAME
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = "CLASS"
        End With

        With oExcel.Range(oCells(3, 9), oCells(3, 24)) ' CLASS
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = str_GradoAula
        End With
        oExcel.Rows("3:3").RowHeight = 20

        With oExcel.Range(oCells(4, 6), oCells(4, 8)) ' NAME
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = "TUTOR"
        End With

        With oExcel.Range(oCells(4, 9), oCells(4, 24)) ' TUTOR
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = str_NombreTutor
        End With
        oExcel.Rows("4:4").RowHeight = 20

        cuadradoCompleto(oExcel, oExcel.Range(oCells(2, 6), oCells(4, 24)))

        'Pintado de Fecha 
        With oCells(2, 30)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
            .HorizontalAlignment = int_HA_Left
            .Font.Bold = True
            .Value = "Date: " & StrConv(Format(Now, "MMMM d,yyyy").ToString, VbStrConv.ProperCase)  'Today.ToString("MMMM dd, yyyy")
        End With

        Dim colIni As Integer = 0
        Dim colFin As Integer = 0
        Dim lstPosCursos As New List(Of Integer)

        For i As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos

            colIni = columna + (i * 3)
            colFin = colIni + 2

            lstPosCursos.Add(colIni) ' agrego la columna de la posicion inicial

            With oExcel.Range(oCells(fila, colIni), oCells(fila, colFin))
                .Merge()
                .Value = ds_Lista.Tables(3).Rows(i).Item("NombreCurso")
                .Font.Name = "Arial"
                .Font.Size = 8
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .ColumnWidth = 3
                .WrapText = True
            End With

            With oExcel.Range(oCells(fila + 1, colIni), oCells(fila + 1, colFin)) ' Fila 6 : Lista de Codigos Asignacion de Grupo
                .Merge()
                .Value = ds_Lista.Tables(3).Rows(i).Item("CodigoAsignacionGrupo")
                .Font.Name = "Arial"
                .Font.Size = 8
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .ColumnWidth = 3
                .WrapText = True
            End With
        Next
        oExcel.Rows("5:5").RowHeight = 30 ' Listado de Cursos

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

        For i As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1 ' Pintado de Criterios
            colIni = 0
            If str_Grupo = "" Or str_Grupo <> ds_Lista.Tables(0).Rows(i).Item("GrupoCriterio") Then
                str_Grupo = ds_Lista.Tables(0).Rows(i).Item("GrupoCriterio")
                With oCells(fila + i, columna)
                    .Font.Bold = True
                    .Value = str_Grupo
                End With
                If bool_PintadoGrupo = False Then
                    For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos
                        colIni = columna + 1 + (j * 3)
                        For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1 ' Pintado de Calificativos
                            With oCells(fila + i, colIni + k)
                                .Font.Bold = True
                                .Value = ds_Lista.Tables(1).Rows(k).Item("Abreviatura")
                                .HorizontalAlignment = int_HA_Center
                                .VerticalAlignment = int_VA_Middle
                            End With
                            oCells(fila + i + 1, colIni + k).Value = ds_Lista.Tables(1).Rows(k).Item("CodigoCalificativo")
                        Next
                    Next
                    bool_PintadoGrupo = True
                    fila += 1
                End If
                lstPos.Add(fila + i)
                fila += 1
            End If

            oCells(fila + i, columna) = ds_Lista.Tables(0).Rows(i).Item("Criterio")
            int_CodigoAsignacionGrupo = 0
            colIni = 0

            For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1
                colIni = columna + 1 + (j * 3)
                int_CodigoAsignacionGrupo = oCells(6, colIni).Text()
                int_CodigoCalificativo = 0
                If bool_NotaCriterio Then : bool_NotaCriterio = False : End If
                For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1
                    int_CodigoCalificativo = oCells(8, columna + 1 + k).Text()
                    For l As Integer = 0 To ds_Lista.Tables(2).Rows.Count - 1
                        If ds_Lista.Tables(2).Rows(l).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo And ds_Lista.Tables(2).Rows(l).Item("CodigoCalificativo") = int_CodigoCalificativo Then
                            With oCells(fila + i, colIni + k)
                                .value = "X"
                                .HorizontalAlignment = int_HA_Center
                                .VerticalAlignment = int_VA_Middle
                            End With
                            bool_NotaCriterio = True
                            Exit For
                        End If
                    Next
                    If bool_NotaCriterio Then : Exit For : End If
                Next
            Next
        Next

        oExcel.Range(oCells(5, columna), oCells(fila - 1, columna)).EntireColumn.AutoFit() ' Listado de Criterios


        Dim objColor0 As Object = RGB(0, 0, 0) 'Negro
        Dim objColor1 As Object = RGB(191, 191, 191) 'Plomo

        pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor0)
        pintadoInterior(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor1)
        pintadoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(5, int_UltimaColumna)), objColor0)
        pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila, 2)), objColor0)

        pintadoCelda(oExcel, oExcel.Range(oCells(lstPos(1), 2), oCells(lstPos(1), int_UltimaColumna)), objColor0, 2) ' separador de grupos de criterio
        pintadoCelda(oExcel, oExcel.Range(oCells(int_UltimaFila, 2), oCells(int_UltimaFila, int_UltimaColumna)), objColor0, 4) ' separador Notas

        For i As Integer = 0 To lstPosCursos.Count - 1
            pintadoCelda(oExcel, oExcel.Range(oCells(5, lstPosCursos(i)), oCells(int_UltimaFila + 2, lstPosCursos(i))), objColor0, 1) ' separador de cursos
        Next

        With oCells(int_UltimaFila + 1, 2)
            .Value = "ACADEMIC PERFORMANCE"
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        With oCells(int_UltimaFila + 2, 2)
            .Value = "OVERALL ATTAINMENT"
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        oExcel.Rows((int_UltimaFila + 2).ToString & ":" & (int_UltimaFila + 2).ToString).RowHeight = 24 ' Listado de Cursos

        colIni = 0
        colFin = 0

        For i As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1 ' Pintado de Nota de Cursos 
            colIni = 0
            colIni = columna + 1 + (i * 3)
            colFin = colIni + 2
            int_CodigoAsignacionGrupo = oCells(6, colIni).Text()
            For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                If ds_Lista.Tables(5).Rows(j).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo Then
                    With oExcel.Range(oCells(int_UltimaFila + 1, colIni), oCells(int_UltimaFila + 1, colFin))
                        .Merge()
                        .Value = ds_Lista.Tables(5).Rows(j).Item("NotaBimestre")
                        .Font.Name = "Arial"
                        .Font.Size = 8
                        .Font.Bold = True
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                    End With
                    With oExcel.Range(oCells(int_UltimaFila + 1 + 1, colIni), oCells(int_UltimaFila + 1 + 1, colFin))
                        .Merge()
                        .Value = ds_Lista.Tables(5).Rows(j).Item("Observacion")
                        .Font.Name = "Arial"
                        .Font.Size = 8
                        .Font.Bold = True
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                    End With
                    Exit For
                End If
            Next
        Next

        ' PINTADO DE COMENTARIOS 
        Dim int_FilaComentario As Integer = int_UltimaFila + 3
        Dim int_MaxNumFilasComentario As Integer = 15 * 2
        Dim int_UltimaFilaComentario As Integer = int_FilaComentario + int_MaxNumFilasComentario 'ds_Lista.Tables(6).Rows.Count
        int_FilaComentario += 1
        With oCells(int_FilaComentario, 2)
            .Value = "COMMENTS"
            .Font.Bold = True
        End With
        int_FilaComentario += 1

        Dim pos_ComentAux As Integer = 0
        Dim int_FilasPintadas As Integer = 0

        For i As Integer = 0 To ds_Lista.Tables(6).Rows.Count - 1 ' Pintado de Oservaciones de Cursos 
            If ds_Lista.Tables(6).Rows(i).Item("Observacion") IsNot DBNull.Value Then

                If ds_Lista.Tables(6).Rows(i).Item("Observacion").ToString.Length > 0 Then
                    int_FilasPintadas += 1
                End If

                'With oExcel.Range(oCells(int_FilaComentario + i * 2, 2), oCells(int_FilaComentario + i * 2, int_UltimaColumna))
                With oExcel.Range(oCells(int_FilaComentario - 2 + int_FilasPintadas * 2, 2), _
                                  oCells(int_FilaComentario - 2 + int_FilasPintadas * 2, int_UltimaColumna))
                    .Merge()
                    .Value = ds_Lista.Tables(6).Rows(i).Item("Curso") & ":"
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                    .HorizontalAlignment = int_HA_Left
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                End With

                'With oExcel.Range(oCells(int_FilaComentario + i * 2 + 1, 2), oCells(int_FilaComentario + i * 2 + 1, int_UltimaColumna))
                With oExcel.Range(oCells(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, 2), _
                                  oCells(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, int_UltimaColumna))
                    .Merge()
                    .Value = ds_Lista.Tables(6).Rows(i).Item("Observacion")
                    .Font.Name = "Arial"
                    .Font.Size = 9
                    .HorizontalAlignment = int_HA_Left
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                End With

            End If
        Next
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaComentario, 2), oCells(int_UltimaFilaComentario, int_UltimaColumna)), objColor0)

        ' PINTADO DE NOTAS 
        Dim int_FilaNotas As Integer = int_UltimaFilaComentario + 2
        Dim int_UltimaFilaNotas As Integer = int_FilaNotas + ds_Lista.Tables(5).Rows.Count
        Dim int_UltimaColumnaNotas As Integer = 2 + ds_Lista.Tables(5).Columns.Count - 6

        With oExcel.Range(oCells(int_FilaNotas, 2), oCells(int_FilaNotas, 11))
            .Merge()
            .Value = "TERM AND ANNUAL MARK"
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
        End With
        int_FilaNotas += 1

        For i As Integer = 0 To ds_Lista.Tables(5).Columns.Count - 1
            If i > 3 And i < 13 Then
                If i = 12 Then
                    With oExcel.Range(oCells(int_FilaNotas, 2 + i - 4), oCells(int_FilaNotas, 2 + i - 4 + 1))
                        .Merge()
                        .Value = ds_Lista.Tables(5).Columns(i).ColumnName
                        .Font.Bold = True
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .Font.Name = "Arial"
                        .Font.Size = 9
                    End With
                    For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                        With oExcel.Range(oCells(int_FilaNotas + 1 + j, 2 + i - 4), oCells(int_FilaNotas + 1 + j, 2 + i - 4 + 1))
                            .Merge()
                            .Value = ds_Lista.Tables(5).Rows(j).Item(i)
                            .Font.Bold = True
                            .HorizontalAlignment = int_HA_Center
                            .VerticalAlignment = int_VA_Middle
                            .Font.Name = "Arial"
                            .Font.Size = 8
                        End With
                    Next
                Else
                    With oExcel.Range(oCells(int_FilaNotas, 2 + i - 4), oCells(int_FilaNotas, 2 + i - 4))
                        .Value = ds_Lista.Tables(5).Columns(i).ColumnName
                        .Font.Bold = True
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .Font.Name = "Arial"
                        .Font.Size = 9
                    End With
                    For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                        With oExcel.Range(oCells(int_FilaNotas + 1 + j, 2 + i - 4), oCells(int_FilaNotas + 1 + j, 2 + i - 4))
                            .Value = ds_Lista.Tables(5).Rows(j).Item(i)
                            .Font.Bold = True
                            If i = 4 Then
                                .HorizontalAlignment = int_HA_Left
                                .VerticalAlignment = int_VA_Middle
                                .Font.Size = 10
                            Else
                                .HorizontalAlignment = int_HA_Center
                                .VerticalAlignment = int_VA_Middle
                                .Font.Size = 8
                            End If
                            .Font.Name = "Arial"
                        End With
                    Next
                End If
            End If
        Next
        pintadoCompleto(oExcel, oExcel.Range(oCells(int_FilaNotas - 1, 2), oCells(int_UltimaFilaNotas + 1, int_UltimaColumnaNotas)), objColor0)

        With oExcel.Range(oCells(int_UltimaFilaNotas + 2, 2), oCells(int_UltimaFilaNotas + 2, int_UltimaColumnaNotas)) 'oCells(int_UltimaFilaNotas + 2, 2)
            .Merge()
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = "Note: The note indicates 'P' value test is pending."
            .Font.Bold = True
        End With

        ' PINTADO DE STUDENT PROFILE 
        Dim int_FilaProfile As Integer = int_UltimaFilaComentario + 2
        Dim int_ColumnaProfile As Integer = int_UltimaColumnaNotas + 2
        Dim int_UltimaFilaProfile As Integer = int_FilaNotas - 2 + 8 + ds_Lista.Tables(7).Rows.Count * 2
        Dim int_UltimaColumnaProfile As Integer = int_ColumnaProfile + ds_Lista.Tables(8).Rows.Count * 2

        Dim lstPosProfile As New List(Of posicionCelda)
        Dim posCelda As posicionCelda

        Dim int_PosProfileFila As Integer = 0
        Dim int_PosProfileColumna As Integer = 0

        With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8))
            .Merge()
            .Value = "STUDENT PROFILE"
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .Font.Name = "Arial"
            .Font.Size = 10
        End With

        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8)), objColor0)

        For i As Integer = 0 To ds_Lista.Tables(8).Rows.Count - 1

            With oCells(int_FilaProfile - 1, int_ColumnaProfile + 8 + 1 + i * 2)
                .Value = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
                posCelda = New posicionCelda
                posCelda.posFila = int_FilaProfile - 1
                posCelda.posColumna = int_ColumnaProfile + 8 + 1 + i * 2
                posCelda.Codigo = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
                lstPosProfile.Add(posCelda)
            End With

            With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile + 8 + 1 + i * 2), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8 + 1 + i * 2))
                .Merge()
                .Value = ds_Lista.Tables(8).Rows(i).Item("Calificativo")
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Bottom
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 9
            End With
            With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile + 8 + 2 + i * 2), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8 + 2 + i * 2))
                .Merge()
                .Value = ds_Lista.Tables(8).Rows(i).Item("CalificativoES")
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Bottom
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 9
            End With
        Next

        Dim int_codCriterio As Integer = 0
        Dim int_codCalificativo As Integer = 0
        Dim int_codCalificativoAux As Integer = 0
        Dim int_codCalificativoPos As Integer = 0

        For i As Integer = 0 To ds_Lista.Tables(7).Rows.Count - 1

            With oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile - 1)
                .Value = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
                int_codCriterio = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
                posCelda = New posicionCelda
                posCelda.posFila = int_FilaProfile + 8 + i * 2
                posCelda.posColumna = int_ColumnaProfile - 1
                posCelda.Codigo = 0
                lstPosProfile.Add(posCelda)
            End With

            With oExcel.Range(oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile), oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile + 8))
                .Merge()
                .Value = ds_Lista.Tables(7).Rows(i).Item("Criterio")
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 9
            End With
            With oExcel.Range(oCells(int_FilaProfile + 9 + i * 2, int_ColumnaProfile), oCells(int_FilaProfile + 9 + i * 2, int_ColumnaProfile + 8))
                .Merge()
                .Value = ds_Lista.Tables(7).Rows(i).Item("CriterioES")
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 9
            End With

            For k As Integer = 0 To ds_Lista.Tables(9).Rows.Count - 1
                If int_codCriterio = ds_Lista.Tables(9).Rows(k).Item("CodigoCriterio") Then
                    int_codCalificativo = ds_Lista.Tables(9).Rows(k).Item("CodigoCalificativo")
                    For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
                        If posCel.Codigo > 0 Then
                            If posCel.Codigo = int_codCalificativo Then
                                With oExcel.Range(oCells(int_FilaProfile + 8 + i * 2, posCel.posColumna), oCells(int_FilaProfile + 9 + i * 2, posCel.posColumna + 1))
                                    .Merge()
                                    .Value = "X"
                                    .Font.Bold = True
                                    .HorizontalAlignment = int_HA_Center
                                    .VerticalAlignment = int_VA_Middle
                                    .Font.Name = "Arial"
                                    .Font.Size = 9
                                End With
                                Exit For
                            End If
                        End If
                    Next
                End If
            Next
        Next

        pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor0)

        For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
            With oCells(posCel.posFila, posCel.posColumna)
                .value = ""
            End With
            If posCel.Codigo = 0 Then
                pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila, posCel.posColumna + 1), oCells(posCel.posFila + 1, 8 + int_UltimaColumnaProfile)), objColor0)
            End If
            If posCel.Codigo > 0 Then
                pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila + 1, posCel.posColumna), oCells(int_UltimaFilaProfile, posCel.posColumna + 1)), objColor0)
            End If
        Next

        ' PINTADO DE ASISTENCIA
        Dim int_FilaAsistencia As Integer = int_UltimaFilaComentario + 2
        Dim int_ColumnaAsistencia As Integer = int_UltimaColumnaProfile + 1 + 9

        Dim int_UltimaFilaAsistencia As Integer = int_FilaAsistencia - 1 + 8 + 14
        'Dim int_UltimaColumnaAsistencia As Integer = int_ColumnaAsistencia + 8 + 10

        Dim lstPosAsistencia As New List(Of posicionCelda)
        Dim posCelda2 As posicionCelda

        Dim int_PosAsistenciaFila As Integer = 0
        Dim int_PosAsistenciaColumna As Integer = 0

        With oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "ATTENDANCE                                  Asistencia"
            .Font.Bold = True
            .WrapText = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Font.Name = "Arial"
            .Font.Size = 10
        End With

        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8)), objColor0)
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 10)), objColor0)

        Dim str_Bimestre As String = ""

        For i As Integer = 0 To 4
            Select Case i
                Case 0 : str_Bimestre = "TERM I"
                Case 1 : str_Bimestre = "TERM II"
                Case 2 : str_Bimestre = "TERM III"
                Case 3 : str_Bimestre = "TERM IV"
                Case 4 : str_Bimestre = "AVERAGE"
            End Select
            With oCells(int_FilaAsistencia - 1, int_ColumnaAsistencia + 8 + 1 + i * 2)
                .Value = "" 'i + 1
                posCelda2 = New posicionCelda
                posCelda2.posFila = int_FilaAsistencia - 1
                posCelda2.posColumna = int_ColumnaAsistencia + 8 + 1 + i * 2
                posCelda2.Codigo = i + 1
                lstPosAsistencia.Add(posCelda2)
            End With
            With oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia + 8 + 1 + i * 2), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 2 + i * 2))
                .Merge()
                .Value = str_Bimestre
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Bottom
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With
        Next

        Dim int_codBimestre As Integer = 0
        Dim int_codBimestreAux As Integer = 0

        Dim lstPosAsis As New List(Of posicionCelda)
        Dim posCelda3 As posicionCelda

        ' TARDANZAS Y CONDUCTA

        ' TARDANZAS
        With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 4))
            .Merge()
            .Value = "LATES          Tardanzas"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 4)), objColor0)

        With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 9, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "Justified          Justificado"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 9
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 9, int_ColumnaAsistencia + 8 + 10)), objColor0)

        With oExcel.Range(oCells(int_FilaAsistencia + 10, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "Unjustified          Injustificado"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 9
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 10, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 8 + 10)), objColor0)

        ' FALTAS
        With oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 4))
            .Merge()
            .Value = "ABSENCES          Ausencias"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 4)), objColor0)

        With oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 13, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "Justified          Justificado"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 9
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 13, int_ColumnaAsistencia + 8 + 10)), objColor0)

        With oExcel.Range(oCells(int_FilaAsistencia + 14, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "Unjustified          Injustificado"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 9
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 14, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 8 + 10)), objColor0)

        ' DEMERITOS
        With oExcel.Range(oCells(int_FilaAsistencia + 16, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 17, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "DEMERITS                                            Deméritos"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 16, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 17, int_ColumnaAsistencia + 18)), objColor0)

        ' MERITOS
        With oExcel.Range(oCells(int_FilaAsistencia + 18, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 19, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "MERITS                                                      Méritos"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 18, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 19, int_ColumnaAsistencia + 18)), objColor0)

        ' CONDUCT MARK
        With oExcel.Range(oCells(int_FilaAsistencia + 20, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 21, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "CONDUCT MARK                                            Nota de conducta"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 20, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 21, int_ColumnaAsistencia + 18)), objColor0)

        Dim pos_Bimestre As Integer = 0

        If ds_Lista.Tables(10).Rows.Count > 0 Then
            For Each dr As DataRow In ds_Lista.Tables(10).Rows
                If dr.Item("CodigoBimestre") = 1 Then
                    pos_Bimestre = 0
                ElseIf dr.Item("CodigoBimestre") = 2 Then
                    pos_Bimestre = 2
                ElseIf dr.Item("CodigoBimestre") = 3 Then
                    pos_Bimestre = 4
                ElseIf dr.Item("CodigoBimestre") = 4 Then
                    pos_Bimestre = 6
                End If
                With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 9, int_ColumnaAsistencia + 10 + pos_Bimestre))
                    .Merge()
                    .Value = dr.Item("TotalTardanzasJustificadas")
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                End With
                With oExcel.Range(oCells(int_FilaAsistencia + 10, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 10 + pos_Bimestre))
                    .Merge()
                    .Value = dr.Item("TotalTardanzasSinJustificar")
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                End With
                With oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia + +9 + pos_Bimestre), oCells(int_FilaAsistencia + 13, int_ColumnaAsistencia + 10 + pos_Bimestre))
                    .Merge()
                    .Value = dr.Item("TotalFaltasJustificadas")
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                End With
                With oExcel.Range(oCells(int_FilaAsistencia + 14, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 10 + pos_Bimestre))
                    .Merge()
                    .Value = dr.Item("TotalFaltasSinJustificar")
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                End With
            Next


            pos_Bimestre = 8
            Dim str_Rango As String = ""
            'SUMA DE TARDANZAS JUSTIFICADAS
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 8).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 9).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 9, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'SUMA DE TARDANZAS SIN JUSTIFICAR
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 10).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 11).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 10, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'SUMA DE FALTAS JUSTIFICADAS
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 12).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 13).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 13, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'SUMA DE FALTAS SIN JUSTIFICAR
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 14).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 15).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 14, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'SUMA DE DEMERITOS
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 16).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 17).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 16, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 17, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'SUMA DE MERITOS
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 18).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 19).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 18, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 19, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'PROMEDIO DE NOTA
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 20).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 21).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 20, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 21, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=PROMEDIO(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            oExcel.Range(DevLetraColumna(int_ColumnaAsistencia + 9 + pos_Bimestre) & ":" & _
                         DevLetraColumna(int_ColumnaAsistencia + 9 + pos_Bimestre)).ColumnWidth = 3
            oExcel.Range(DevLetraColumna(int_ColumnaAsistencia + 10 + pos_Bimestre) & ":" & _
                         DevLetraColumna(int_ColumnaAsistencia + 10 + pos_Bimestre)).ColumnWidth = 3

            pos_Bimestre = 0

            If ds_Lista.Tables(11).Rows.Count > 0 Then
                For Each dr As DataRow In ds_Lista.Tables(11).Rows
                    If dr.Item("CodigoBimestre") = 1 Then
                        pos_Bimestre = 0
                    ElseIf dr.Item("CodigoBimestre") = 2 Then
                        pos_Bimestre = 2
                    ElseIf dr.Item("CodigoBimestre") = 3 Then
                        pos_Bimestre = 4
                    ElseIf dr.Item("CodigoBimestre") = 4 Then
                        pos_Bimestre = 6
                    End If
                    With oExcel.Range(oCells(int_FilaAsistencia + 16, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 17, int_ColumnaAsistencia + 10 + pos_Bimestre))
                        .Merge()
                        .Value = dr.Item("TotalDemeritos")
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With
                    With oExcel.Range(oCells(int_FilaAsistencia + 18, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 19, int_ColumnaAsistencia + 10 + pos_Bimestre))
                        .Merge()
                        .Value = dr.Item("TotalMeritos")
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With
                    With oExcel.Range(oCells(int_FilaAsistencia + 20, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 21, int_ColumnaAsistencia + 10 + pos_Bimestre))
                        .Merge()
                        .Value = dr.Item("NotaConducta")
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With
                Next

            End If
        End If

        For Each posCel As posicionCelda In lstPosAsistencia ' Limpio todos los codigos pintados previamente
            If posCel.Codigo > 0 Then
                pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila + 1, posCel.posColumna), oCells(int_UltimaFilaAsistencia, posCel.posColumna + 1)), objColor0)
            End If
        Next

        ' FIRMAS
        With oExcel.Range(oCells(int_FilaAsistencia + 25, 2), oCells(int_FilaAsistencia + 25, 5))
            .Merge()
            .Value = "SIGNATURE OF TUTOR"
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
        End With
        pintadoCelda(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 25, 2), oCells(int_FilaAsistencia + 25, 5)), objColor0, 2)

        With oExcel.Range(oCells(int_FilaAsistencia + 25, 8), oCells(int_FilaAsistencia + 25, 23))
            .Merge()
            .Value = "SIGNATURE OF PARENT"
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
        End With
        pintadoCelda(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 25, 8), oCells(int_FilaAsistencia + 25, 23)), objColor0, 2)


        oExcel.Rows("6:6").Delete() ' Elimino la fila de codigos de asignacion de grupos
        oExcel.Rows("7:7").Delete() ' Elimino la fila de codigos de los calificativos
        oExcel.Rows("5:5").Insert()
        oExcel.Rows("5:5").RowHeight = 5 ' Listado de Cursos
        borrarPintado(oExcel, oExcel.Range(oCells(5, 2), oCells(5, int_UltimaColumna)))
        cuadradoCompleto(oExcel, oExcel.Range(oCells(2, 6), oCells(4, 24)))
        'oExcel.Columns("A:A").Delete()
        oExcel.Range("A:A").ColumnWidth = 3


        oExcel.ActiveWindow.Zoom = 75
        Return str_Fila

    End Function



    ' Consolidado
    Public Shared Function ExportarReporteconsolidadoSecundaria( _
        ByVal NombreArchivo As String, _
        ByVal ds_Lista As System.Data.DataSet) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        Try


            Dim rutaLibreta As String = System.Configuration.ConfigurationManager.AppSettings("rutaLibreta").ToString()
            Dim rutaPlantilla As String = System.Configuration.ConfigurationManager.AppSettings("ConsolidadoSecundaria").ToString()

            Dim rutamadre As String = Environment.CurrentDirectory
            nombreRep = NombreArchivo




            ' Archivo excel a grabar
            sFile = nombreRep

            ' Plantilla a cargar
            sTemplate = rutamadre & rutaPlantilla

            oExcel.Visible = False
            oExcel.DisplayAlerts = False

            ''Start a new workbook 
            oBooks = oExcel.Workbooks
            oBooks.Open(sTemplate) 'Load colorful template with graph
            oBook = oBooks.Item(1)
            oSheets = oBook.Worksheets
            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "Consolidado"
            oCells = oSheet.Cells

            LlenarPlantillaReporteConsolidadoSecundaria(ds_Lista, oCells, oExcel)

            With oExcel.ActiveSheet.PageSetup
                .LeftHeader = ""
                .CenterHeader = ""
                .RightHeader = ""
                .LeftFooter = ""
                .CenterFooter = ""
                .RightFooter = ""
                .LeftMargin = oExcel.Application.InchesToPoints(0)
                .RightMargin = oExcel.Application.InchesToPoints(0)
                .TopMargin = oExcel.Application.InchesToPoints(0)
                .BottomMargin = oExcel.Application.InchesToPoints(0)
                .HeaderMargin = oExcel.Application.InchesToPoints(0)
                .FooterMargin = oExcel.Application.InchesToPoints(0)
                .PrintHeadings = False
                .PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                .PrintQuality = 600
                .CenterHorizontally = False
                .CenterVertically = False
                .Orientation = 2
                .Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = OrderedDictionary xlDownThenOver
                .BlackAndWhite = False
                .Zoom = False
                .FitToPagesWide = 1
                .FitToPagesTall = False
                '.PrintErrors = xlPrintErrorsDisplayed
                .OddAndEvenPagesHeaderFooter = False
                .DifferentFirstPageHeaderFooter = False
                .ScaleWithDocHeaderFooter = True
                .AlignMarginsHeaderFooter = True
                .EvenPage.LeftHeader.Text = ""
                .EvenPage.CenterHeader.Text = ""
                .EvenPage.RightHeader.Text = ""
                .EvenPage.LeftFooter.Text = ""
                .EvenPage.CenterFooter.Text = ""
                .EvenPage.RightFooter.Text = ""
                .FirstPage.LeftHeader.Text = ""
                .FirstPage.CenterHeader.Text = ""
                .FirstPage.RightHeader.Text = ""
                .FirstPage.LeftFooter.Text = ""
                .FirstPage.CenterFooter.Text = ""
                .FirstPage.RightFooter.Text = ""
            End With

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


        Catch ex As Exception

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

        End Try

    End Function

    Private Shared Function LlenarPlantillaReporteConsolidadoSecundaria( _
        ByVal ds_Lista As System.Data.DataSet, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application) As String

        Dim dtReporte As System.Data.DataTable = ds_Lista.Tables(0)

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        ' Pintado de Titulo
        Dim str_GradoAula As String = ds_Lista.Tables(3).Rows(0).Item("DescEspaniol").ToString
        Dim str_BimestreAnio As String = ds_Lista.Tables(3).Rows(0).Item("DescBimestreAnio").ToString
        Dim str_TipoReporte As String = "(Asignaturas Oficiales)"

        'oCells(3, 3) = "Consolidado de Evaluación - " + str_Titulo
        With oExcel.Range(oCells(3, 2), oCells(3, 27))
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 16
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .Value = "Consolidado de Evaluación - " + str_GradoAula + " " + str_TipoReporte + " " + str_BimestreAnio
        End With
        oExcel.Rows("3:3").RowHeight = 40

        'Pintado de Fecha 
        With oCells(1, 24)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
            .HorizontalAlignment = int_HA_Left
            .Font.Bold = True
            .Value = "Fecha: " & Now.Date
        End With

        'Pintado de Hora 
        With oCells(2, 24)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
            .HorizontalAlignment = int_HA_Left
            .Font.Bold = True
            .Value = "Hora: " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
        End With


        ' Pintado de Cabeceras
        oCells(fila, columna) = "Nro"
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Font.Name = "Arial"
            .Font.Size = 11
            .HorizontalAlignment = 3
        End With
        columna += 1

        oCells(fila, columna) = "Apellidos y Nombres"
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Font.Name = "Arial"
            .Font.Size = 11
            .HorizontalAlignment = 3
        End With

        cont_columnas = 0
        cont_filas = 0
        columna += 1

        For i As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1 ' Pintado de Cursos
            oCells(fila, columna + i) = ds_Lista.Tables(0).Rows(i).Item("NombreCurso")
            With oExcel.Range(oCells(fila, columna + i), oCells(fila, columna + i))
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 11
                .HorizontalAlignment = 3
                .ColumnWidth = 4
            End With
            oCells(fila + 1, columna + i) = ds_Lista.Tables(0).Rows(i).Item("CodigoAsignacionGrupo") ' Fila 6
        Next

        For i As Integer = 0 To 35
            With oExcel.Range(oCells(fila, columna + i), oCells(fila, columna + i))
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 11
                .HorizontalAlignment = 3
                If i >= 20 Then
                    If i = 20 Or i = 21 Then
                        .ColumnWidth = 8
                    Else
                        .ColumnWidth = 6
                    End If
                Else
                    .ColumnWidth = 6
                End If
            End With

            If i >= 20 Then
                If i = 20 Then
                    oCells(fila, columna + i) = "PROMEDIO"
                ElseIf i = 21 Then
                    oCells(fila, columna + i) = "PUNTAJE TOTAL"
                ElseIf i = 22 Then
                    oCells(fila, columna + i) = "APROBADOS"
                ElseIf i = 23 Then
                    oCells(fila, columna + i) = "DESAPROBADOS"
                ElseIf i = 24 Then
                    oCells(fila, columna + i) = "CONDUCTA"
                ElseIf i = 25 Then
                    oCells(fila, columna + i) = ""
                ElseIf i = 26 Then
                    oCells(fila, columna + i) = "Inasist. Injustificadas"
                ElseIf i = 27 Then
                    oCells(fila, columna + i) = "Inasist. Justificadas"
                ElseIf i = 28 Then
                    oCells(fila, columna + i) = "Tardanzas Injustificadas"
                ElseIf i = 29 Then
                    oCells(fila, columna + i) = "Tardanzas Justificadas"
                ElseIf i = 30 Then
                    oCells(fila, columna + i) = "Tercio"
                ElseIf i = 31 Then
                    oCells(fila, columna + i) = "Orden de Mérito"
                End If
            End If

        Next
        oExcel.Rows("5:5").RowHeight = 129

        cont_columnas = 0
        cont_filas = 0
        fila += 1
        fila += 1
        columna = 2

        Dim int_CodigoAsignacionGrupo As Integer = 0
        Dim str_CodigoAlumno As String = ""
        Dim int_Idx As Integer = 0
        Dim str_Nota As String = ""

        For i As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1 ' Pintado de Alumnos
            int_Idx = i + 1
            str_CodigoAlumno = ds_Lista.Tables(1).Rows(i).Item("CodigoAlumno")
            oCells(fila + i, columna) = int_Idx
            oCells(fila + i, columna + 1) = ds_Lista.Tables(1).Rows(i).Item("NombreCompleto")
            With oExcel.Range(oCells(fila + i, columna), oCells(fila + i, columna))
                .Font.Name = "Arial"
                .Font.Size = 11
                .RowHeight = 15
            End With
            int_CodigoAsignacionGrupo = 0
            For j As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1
                int_CodigoAsignacionGrupo = oCells(6, j + 4).Text()
                For k As Integer = 0 To ds_Lista.Tables(2).Rows.Count - 1
                    If ds_Lista.Tables(2).Rows(k).Item("CodigoAlumno") = str_CodigoAlumno And ds_Lista.Tables(2).Rows(k).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo Then
                        oCells(fila + i, j + 4) = ds_Lista.Tables(2).Rows(k).Item("NotaFinal")
                        With oCells(fila + i, j + 4)
                            .NumberFormat = "00"
                            .HorizontalAlignment = 3
                            .font.bold = True
                            If ds_Lista.Tables(2).Rows(k).Item("NotaFinal") IsNot DBNull.Value Then
                                If Convert.ToInt16(ds_Lista.Tables(2).Rows(k).Item("NotaFinal")) > 10 Then
                                    .Font.Color = RGB(22, 54, 92)
                                Else
                                    .Font.Color = RGB(255, 0, 0)
                                End If
                            End If
                        End With
                        Exit For
                    End If
                Next
            Next

            For x As Integer = 0 To ds_Lista.Tables(4).Rows.Count - 1 ' Pintado de Nota de Conducta Alumnos
                If ds_Lista.Tables(4).Rows(x).Item("CodigoAlumno") = str_CodigoAlumno Then

                    With oCells(fila + i, columna + 22) ' Promedio
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Promedio")
                        '.NumberFormat = "00.00"
                    End With
                    With oCells(fila + i, columna + 23) ' Puntaje total
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("PuntajeTotal")
                    End With
                    With oCells(fila + i, columna + 24) ' Cursos aprobados
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Aprobados")
                    End With
                    With oCells(fila + i, columna + 25) ' Cursos desaprobados
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Desaprobados")
                    End With
                    With oCells(fila + i, columna + 26) ' Nota Conducta
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Conducta")
                    End With
                    With oCells(fila + i, columna + 27) ' 
                        .HorizontalAlignment = 3
                        .Value = ""
                    End With
                    With oCells(fila + i, columna + 28) ' inasistencias injustificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("FaltasSinJustificar")
                    End With
                    With oCells(fila + i, columna + 29) ' inasistencias justificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("FaltasJustificadas")
                    End With
                    With oCells(fila + i, columna + 30) ' tardanzas injustificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("TardanzasSinJustificar")
                    End With
                    With oCells(fila + i, columna + 31) ' tardanzas justificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("TardanzasJustificadas")
                    End With
                    With oCells(fila + i, columna + 32) ' tercio
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Tercio")
                    End With
                    With oCells(fila + i, columna + 33) ' orden de merito
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("OrdenMerito")
                    End With

                    Exit For
                End If
            Next
        Next

        oExcel.Range(oCells(5, columna), oCells(fila - 1, columna)).EntireColumn.AutoFit() ' Codigo alumno
        oExcel.Range(oCells(5, columna + 1), oCells(fila - 1, columna + 1)).EntireColumn.AutoFit() ' Nombre alumno

        oExcel.Rows("6:6").Delete() ' Elimino la fila de codigos de asignacion de grupos

        Dim int_NumAlumnos As Integer = ds_Lista.Tables(1).Rows.Count
        Dim int_NumCursos As Integer = ds_Lista.Tables(0).Rows.Count
        Dim int_MaxNumCursos As Integer = 20
        Dim int_UltimaFila As Integer = fila - 2 + int_NumAlumnos
        Dim int_UltimaColumna As Integer = columna + 1 + int_NumCursos + (int_MaxNumCursos - int_NumCursos) + 12 ' 12 columnas con campos calculados

        cuadradoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila, int_UltimaColumna)))

        ' Campos calculados
        With oCells(int_UltimaFila + 2, columna + 1)
            .value = "Promedio del Curso"
        End With
        Dim str_Rango As String = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 2, 4 + i)
                .Value = "=PROMEDIO(" + str_Rango + ")"
                .NumberFormat = "0,0"
                .HorizontalAlignment = 3
            End With
        Next

        Dim cond1 As String = ";" & """>" & "10""" & ")"
        Dim cond2 As String = ";" & """<" & "11""" & ")"

        With oCells(int_UltimaFila + 3, columna + 1)
            .value = "Nro de Alumnos Aprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 3, 4 + i)
                .Value = "=CONTAR.SI(" + str_Rango + cond1
                .NumberFormat = "0"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 4, columna + 1)
            .value = "Nro de Alumnos Desaprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 4, 4 + i)
                .Value = "=CONTAR.SI(" + str_Rango + cond2
                .NumberFormat = "0"
                .HorizontalAlignment = 3
            End With
        Next

        Dim cond3 As String = ")*100/" & int_NumAlumnos.ToString

        With oCells(int_UltimaFila + 5, columna + 1)
            .value = "% de Alumnos Aprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 5, 4 + i)
                .Value = "=(CONTAR.SI(" + str_Rango + cond1 & cond3
                .NumberFormat = "0,0"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 6, columna + 1)
            .value = "% de Alumnos Desaprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 6, 4 + i)
                .Value = "=100-(CONTAR.SI(" + str_Rango + cond1 & cond3
                .NumberFormat = "0,0"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 7, columna + 1)
            .value = "Nota Máxima"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 7, 4 + i)
                .Value = "=MAX(" + str_Rango + ")"
                .NumberFormat = "00"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 8, columna + 1)
            .value = "Nota Mínima"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 8, 4 + i)
                .Value = "=MIN(" + str_Rango + ")"
                .NumberFormat = "00"
                .HorizontalAlignment = 3
            End With
        Next

        cuadradoCompleto(oExcel, oExcel.Range(oCells(int_UltimaFila + 2, columna + 1), oCells(int_UltimaFila + 8, 4 + int_NumCursos - 1)))

        oExcel.Columns("A:A").Delete()

        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function

#End Region


#Region "Excel"

    Private Shared Sub cuadradoCompleto(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                     ByVal objRango As Microsoft.Office.Interop.Excel.Range)
        Try

            objRango.Select()
            With mexcel

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

    Private Shared Sub pintadoBordes(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range, _
                         ByVal objColor As Object)
        Try

            objRango.Select()
            With mexcel

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub pintadoInterior(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range, _
                         ByVal objColor As Object)
        Try

            objRango.Select()
            With mexcel

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

            End With
        Catch ex As Exception

        End Try
    End Sub

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

    Private Shared Sub borrarPintado(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range)
        Try

            objRango.Select()
            With mexcel

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                End With

                'With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                '    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                'End With

                'With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                '    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                'End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                End With

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub pintadoCompleto(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range, _
                         ByVal objColor As Object)
        Try

            objRango.Select()
            With mexcel

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

            End With
        Catch ex As Exception

        End Try
    End Sub

    ' Valores 1-52
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

    ' Valores 1-26
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

    Public Class posicionCelda
        Public posFila As Integer
        Public posColumna As Integer
        Public Codigo As Integer
    End Class

#End Region

    Private Sub cmbSalon_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSalon.SelectedIndexChanged
        Dim textDefecto As String = ""
        textDefecto = cmbSalon.SelectedValue.ToString()

        If textDefecto = "System.Data.DataRowView" Then
            Exit Sub
        End If

        CodSalon = CInt(cmbSalon.SelectedValue)

        listarAlumnosPorSalon()

    End Sub



    Private Sub btnDetenerProceso_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetenerProceso.Click
        If tipoReporteConsolidadoLibreta = Constantes.ClaseReporteLibreta Then
            estadoDetenerProceso = False
        Else
            MsgBox("Solo se puede detener la generacion de libretas")
        End If



    End Sub


#Region "Grilla alumnos"

    Private Sub CheckBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.Click
        If DataGridView1.Rows.Count > 0 Then
            For Each dr As DataRow In dt_ListaAlumnos.Rows
                dr.Item("Chk") = CheckBox1.Checked
            Next
            If CheckBox1.Checked = True Then : lblTotalChk.Text = dt_ListaAlumnos.Rows.Count : Else : lblTotalChk.Text = 0 : End If
            DataGridView1.DataSource = dt_ListaAlumnos
        Else
            Exit Sub
        End If
    End Sub

    Private Sub listarAlumnosPorSalon()
        Dim obj_bl_alumnos As New bl_Alumnos
        Dim ds_lista As DataSet
        ds_lista = obj_bl_alumnos.FUN_LIS_AlumnosPorAulayAnioAcademicoLibreta(CodSalon, CodAnio, 1, 1, 1, 1)
        DataGridView1.DataSource = ds_lista.Tables(0)
        dt_ListaAlumnos = ds_lista.Tables(0).Copy
        dt_ListaAlumnos.TableName = "Lista"
        CheckBox1.Checked = True
        lblTotalChk.Text = dt_ListaAlumnos.Rows.Count
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            If DataGridView1.CurrentCell Is Nothing Then
                Exit Sub
            End If
            If e.ColumnIndex = 2 Then
                Dim codAlumno As String = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                For i As Integer = 0 To dt_ListaAlumnos.Rows.Count - 1
                    If dt_ListaAlumnos.Rows(i).Item(0) = codAlumno Then
                        If Convert.ToBoolean(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = True Then
                            dt_ListaAlumnos.Rows(i).Item(2) = False
                        Else
                            dt_ListaAlumnos.Rows(i).Item(2) = True
                        End If
                        Exit For
                    End If
                Next

                Dim int_TotalCheck As Integer = 0
                For i As Integer = 0 To dt_ListaAlumnos.Rows.Count - 1
                    If Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item("Chk")) = True Then
                        int_TotalCheck += 1
                    End If
                Next
                If int_TotalCheck <> dt_ListaAlumnos.Rows.Count Then
                    CheckBox1.Checked = False
                Else
                    CheckBox1.Checked = True
                End If
                lblTotalChk.Text = int_TotalCheck

            Else
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

    Private Sub rbidiomaES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbidiomaES.Click
        int_idioma = 1
    End Sub

    Private Sub rbidiomaEN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbidiomaEN.Click
        int_idioma = 2
    End Sub

    Private Sub cargarComboAnioAcademico()
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, 0, 0, 0, 0)
        Controles.llenarCombo(cmbPeriodo, ds_Lista, "Codigo", "Descripcion", False, False)
    End Sub

    Private Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        Dim textDefecto As String = ""
        textDefecto = cmbPeriodo.SelectedValue.ToString()
        If textDefecto = "System.Data.DataRowView" Then
            Exit Sub
        End If

        CodAnio = cmbPeriodo.SelectedValue



        If cmbTipoReporte.SelectedValue = 3 Then
            tipoReporte = 3
            cargarComboAulasInicial()
        End If

        If cmbTipoReporte.SelectedValue = 4 Then
            cargarComboAulasPrimaria()
            tipoReporte = 4
        End If
        If cmbTipoReporte.SelectedValue = 2 Then
            cargarComboAulas()
            tipoReporte = 2
        End If

        ' cargarComboAulas()
    End Sub
#Region "cargar tipo reporte "

    Private Sub cargarTipoReporte()
        Try
            Dim dtTipoReporte As New System.Data.DataTable
            Dim dc As DataColumn

            dc = New DataColumn("codTipoReporte")
            dtTipoReporte.Columns.Add(dc)
            dc = New DataColumn("descripcionTipoReporte")
            dtTipoReporte.Columns.Add(dc)

            dtTipoReporte.Rows.Add(New Object() {"3", "Inicial"})
            dtTipoReporte.Rows.Add(New Object() {"4", "Primaria"})
            dtTipoReporte.Rows.Add(New Object() {"2", "Secudaria"})


            cmbTipoReporte.ValueMember = "codTipoReporte"
            cmbTipoReporte.DisplayMember = "descripcionTipoReporte"
            cmbTipoReporte.DataSource = dtTipoReporte


        Catch ex As Exception

        End Try
    End Sub

#End Region

    Private Sub cmbTipoReporte_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoReporte.SelectedIndexChanged


        If cmbTipoReporte.SelectedValue = 3 Then

            tipoReporte = 3
            cargarComboAulasInicial()
        End If

        If cmbTipoReporte.SelectedValue = 4 Then
            cargarComboAulasPrimaria()
            tipoReporte = 4
        End If
        If cmbTipoReporte.SelectedValue = 2 Then
            cargarComboAulas()
            tipoReporte = 2
        End If

    End Sub

#Region "Crear libreta primaria En una sola hoja "
    ''

    ''
    Function crearLibretaPrimariaUnaSolaHoja(ByVal codigoAula As Integer, ByVal int_bimestre As Integer, ByVal codAlumno As String) As String
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
        Dim rutaLibreta As String = System.Configuration.ConfigurationManager.AppSettings("LibretaPrimaria").ToString()
        Dim rutaPlantilla As String = System.Configuration.ConfigurationManager.AppSettings("LibretaSecundaria").ToString()
        Dim temp As String = System.Configuration.ConfigurationManager.AppSettings("Temporales").ToString()
        Dim rutaApp As String = ""
        rutaApp = Environment.CurrentDirectory()

        File.Copy(rutaApp + rutaLibreta, rutaApp + temp & rutaTemp & ".xlsx")
        Dim nombreArchivo As String = rutaApp + temp & rutaTemp & ".xlsx"

        alumnosProcesado = 0
        cantidadAlumnos = 0


        Dim tb_Asistencias As New System.Data.DataTable '' extraer las inasistencias del alumno
        Dim tb_demeritos As New System.Data.DataTable '' extrar los meritos y demeritos del alumno 
        Dim tb_conducta As New System.Data.DataTable


        Try
            ''
            Dim dt As New System.Data.DataTable
            Dim dst As New DataSet
            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaPrimaria_1(codigoAula, int_bimestre, codAlumno, 1, 1, 1, 1)
            dt = dst.Tables(0)
            ''
            tb_Asistencias = dst.Tables(3)
            tb_demeritos = dst.Tables(4)
            tb_conducta = dst.Tables(5)

            ''
            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)
            cantidadAlumnos = lst.Count

            ''  cantidadAlumnos = lst.Count
            Dim contadorPaginas As Integer = 0
            Dim paginaInicialInferior As Integer = -74
            Dim paginaIinicialSuperior As Integer = 0

            Dim flagPrimero As Integer = 0
            ''
            For Each opersona As personaLibreta In lst



                paginaInicialInferior += 75
                paginaIinicialSuperior += 75

                opersona.opaginacion.pag1.inferior = paginaInicialInferior
                opersona.opaginacion.pag1.superior = paginaIinicialSuperior

                paginaInicialInferior += 75
                paginaIinicialSuperior += 75

                opersona.opaginacion.pag2.inferior = paginaInicialInferior
                opersona.opaginacion.pag2.superior = paginaIinicialSuperior


                paginaInicialInferior += 75
                paginaIinicialSuperior += 75

                opersona.opaginacion.pag3.inferior = paginaInicialInferior
                opersona.opaginacion.pag3.superior = paginaIinicialSuperior




            Next
            'Exit Function

            ''

            Dim workbook As New ClosedXML.Excel.XLWorkbook(nombreArchivo)

            workbook.CalculateMode = XLCalculateMode.Auto

            '' seleccionar la primera hoja 
            Dim ws = workbook.Worksheet(1)



            Dim nombreCursoTemp As String = ""
            Dim contadorIndicador As Integer = 0
            Dim filaCount As Integer = 0
            Dim iniciaIndicador As Integer = 0
            Dim indiceHojas As Integer = 0

            ''
            Dim abrBimestre As String = ""
            If int_bimestre = 1 Then
                abrBimestre = "I"
            End If
            If int_bimestre = 2 Then
                abrBimestre = "II"
            End If
            If int_bimestre = 3 Then
                abrBimestre = "III"
            End If
            If int_bimestre = 4 Then
                abrBimestre = "IV"
            End If


            Dim lstPaginas As New List(Of Integer)
            Dim sumaPag As Integer = 0
            For inicio As Integer = 1 To 100
                sumaPag += 75
                lstPaginas.Add(sumaPag)
            Next

            Dim ci As Globalization.CultureInfo = New Globalization.CultureInfo("en-US")

            Date.Now.ToString("MMMM", ci)
            ''
            Dim agregoFilas As Integer = 0
            Dim agregoFilas1 As Integer = 0

            Dim boolEstado As Boolean = True
            ''iterando la lista de alumnmos 

            ''variables  globales
            Dim fil As Integer = 2
            Dim contadorFilas As Integer = 0
            Dim empiezanFilas As Integer = 0
            ''
            ''============================================
            ''
            ''--------------------------------------------
            '' 
            ''
            ''
            ''--------------------------------------------

            Dim contadoPaginas As Integer = 0

            Dim primeraFilas As Integer = 0

            For iPerosona As Integer = 0 To lst.Count - 1
                primeraFilas += 1

                contadorFilas = 0
                agregoFilas = 0
                agregoFilas1 = 0

                indiceHojas += 1
                contadoPaginas += 75

                ''
                ''1.pintado del reports card 
                ''------------------------------------------------------
                If primeraFilas > 1 Then
                    fil += 1
                End If

                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Merge()
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Value = "REPORT CARD"
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Style.Font.Bold = True
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ''------------------------------------------------------

                ''2.pintado del nombre 
                ''--------------------------------------------------------
                ''2.1 pintado de la etiqueta  del nombre

                empiezanFilas = fil
                fil += 1 ''  
                contadoPaginas += 75

                contadorFilas += 1



                ws.Cell(fil, 3).Value = "NAME"
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                ''2.2 pintado del nombre  del alumno
                ''---------------------------------------------------------
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = lst(iPerosona).nombreAlumno
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                ''pintado de class
                ''---------------------------------------------------------
                fil += 1
                contadorFilas += 1
                ws.Cell(fil, 3).Value = "CLASS"
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                ''pintado del nombre del tutor 
                ''---------------------------------------------------------

                ''

                ''pintado del "DATE"
                ''--------------------------------------
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Merge()

                If fechaReporte.Trim <> "" Then
                    ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Value = fechaReporte
                Else
                    ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Value = "Date: " & Date.Now.ToString("MMMM", ci) & " " & Date.Now.Day.ToString() & "," & Date.Now.Year().ToString
                End If



                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Style.Alignment.Indent = 2


                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Style.Font.Bold = True
                ''---------------------------------------
                ''
                fil += 1
                contadorFilas += 1
                ws.Cell(fil, 3).Value = "TUTOR"
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = dst.Tables(2).Rows(0)("nombre").ToString()

                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ''---------------------------------------------------------
                fil += 1
                contadorFilas += 1
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Merge()
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Value = "SUBJECT AREAS -" & Now().Year().ToString()
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Style.Font.Bold = True
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Style.Font.FontSize = 16

                ''pintado de "TERM"
                ws.Cell(fil, 11).Value = "TERM " & abrBimestre
                ws.Cell(fil, 11).Style.Font.Bold = True
                '

                '
                'pintado de cursos
                '---------------------------------------------------------------------
                Dim estadoFilas As Boolean = False
                For Each olibretaComponente As libretaComponente In lst(iPerosona).lstLibretaComponente
                    fil += 1
                    contadorFilas += 1

                    If nombreCursoTemp <> olibretaComponente.nombreCurso Then


                        Dim nombreTemp As String = ""
                        nombreTemp = olibretaComponente.nombreCurso
                        Dim count = From o In lst(iPerosona).lstLibretaComponente Where o.nombreCurso = nombreTemp _
                            Select o.lstIndicador
                        Dim cntListas As Integer = 0
                        'cntListas = count.Count()
                        Dim cantidadComponente = From o In lst(iPerosona).lstLibretaComponente Where o.nombreCurso = nombreTemp Select o.nombreComponente Distinct
                        Dim contador As Integer = 0
                        For Each el In count
                            Dim exis = From i In el Where i.size = 1

                            If exis.Count = 1 Then
                                contador += el.Count() + exis.Count
                            Else
                                contador += el.Count()
                            End If


                        Next
                        Dim cnt As Integer = contador + 2 + (cantidadComponente.Count)







                        'If Not fil + cnt <= contadoPaginas And fil < contadoPaginas + 75 And agregoFilas = 0 Then
                        '    Dim diferencias As Integer = 0
                        '    diferencias = contadoPaginas + 2 - fil
                        '    fil += diferencias
                        '    agregoFilas = 1
                        'End If
                        'If Not fil + cnt <= contadoPaginas + 75 And fil > contadoPaginas And agregoFilas1 = 0 Then

                        '    Dim diferencias As Integer = 0
                        '    diferencias = contadoPaginas * 2 - fil
                        '    fil += diferencias + 3
                        '    agregoFilas1 = 1
                        'End If
                        'If Not fil + cnt <= contadoPaginas * 3 And fil > contadoPaginas * 2 And agregoFilas1 = 0 Then

                        '    Dim diferencias As Integer = 0
                        '    diferencias = contadoPaginas * 3 - fil
                        '    fil += diferencias + 3
                        '    agregoFilas1 = 1
                        'End If
                        Dim diferencias As Integer = 0


                        'lst(iPerosona)


                        If fil >= lst(iPerosona).opaginacion.pag1.inferior And fil <= lst(iPerosona).opaginacion.pag1.superior Then

                            If fil + cnt > lst(iPerosona).opaginacion.pag1.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag1.superior - fil
                                fil += diferencias + 2
                            End If

                        End If

                        If fil >= lst(iPerosona).opaginacion.pag2.inferior And fil <= lst(iPerosona).opaginacion.pag2.superior Then

                            If fil + cnt > lst(iPerosona).opaginacion.pag2.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag2.superior - fil
                                fil += diferencias + 2
                            End If

                        End If
                        If fil >= lst(iPerosona).opaginacion.pag3.inferior And fil <= lst(iPerosona).opaginacion.pag3.superior Then

                            If fil + cnt > lst(iPerosona).opaginacion.pag3.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag3.superior - fil
                                fil += diferencias + 2
                            End If

                        End If


                        filaCount = fil + 1


                        'If Not fil + cnt <= 75 And fil < 150 And agregoFilas = 0 Then
                        '    Dim diferencias As Integer = 0
                        '    diferencias = 77 - fil
                        '    fil += diferencias
                        '    agregoFilas = 1

                        'End If
                        'If Not fil + cnt <= 150 And fil > 75 And agregoFilas1 = 0 Then
                        '    Dim diferencias As Integer = 0
                        '    diferencias = 150 - fil
                        '    fil += diferencias + 3
                        '    agregoFilas1 = 1

                        'End If




                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Merge()
                        With ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Value = olibretaComponente.nombreCurso.ToUpper() '' & " nombre alumno " & opersonaLibreta.nombreAlumno
                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Style.Font.Bold = True
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Merge()
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Value = "PERFORMANCE"
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 11).Value = "AVERAGE"
                        ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        ' ws.Cell(fil, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 11)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        fil = filaCount
                        contadorFilas += 1

                        ws.Cell(fil, 7).Value = "C"
                        ws.Cell(fil, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        'ws.Cell(fil, 7).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                        With ws.Cell(fil, 7)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 8).Value = "B"
                        ws.Cell(fil, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil, 8).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 8)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil, 9).Value = "A"
                        ws.Cell(fil, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil, 9).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 9)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With




                        ws.Cell(fil, 10).Value = "AD"
                        ws.Cell(fil, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil, 10).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 10)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 11).Value = olibretaComponente.promedioComponente.ToUpper()
                        ' ws.Cell(fil, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil, 11)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        fil += 1
                        contadorFilas += 1

                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Merge()
                        'ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                    End If

                    'If olibretaComponente.nombreComponente = "READING" Then
                    '    fil += 5
                    'End If


                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Merge()
                    ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Value = olibretaComponente.nombreComponente.ToUpper()






                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.WrapText = True

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Font.Bold = True

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    ws.Cell(fil, 11).Value = olibretaComponente.notaComponente.ToUpper()

                    ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center



                    With ws.Cell(fil, 11)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    iniciaIndicador = fil + 1
                    contadorIndicador = 0
                    ''------------------------------------------------------------------------------

                    Dim RowsPan As Integer = 0
                    For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador
                        RowsPan = 0
                        fil += 1
                        contadorFilas += 1
                        If olibretaIndicador.size = 1 Then
                            RowsPan += fil + 1
                            contadorIndicador += 1

                        Else
                            RowsPan = fil
                        End If

                        ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6)).Merge()
                        ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6)).Value = olibretaIndicador.nombreIndicador
                        ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6)).Style.Alignment.WrapText = True
                        ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6)).Style.Alignment.Indent = 2

                        With ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With
                        If olibretaIndicador.notaIndicador.ToUpper() = "C" Then
                            ws.Range(ws.Cell(fil, 7), ws.Cell(RowsPan, 7)).Merge()
                            ws.Range(ws.Cell(fil, 7), ws.Cell(RowsPan, 7)).Value = " * "
                            ws.Range(ws.Cell(fil, 7), ws.Cell(RowsPan, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "B" Then

                            'ws.Cell(fil, 8).Value = " * "
                            'ws.Cell(fil, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                            ws.Range(ws.Cell(fil, 8), ws.Cell(RowsPan, 8)).Merge()
                            ws.Range(ws.Cell(fil, 8), ws.Cell(RowsPan, 8)).Value = " * "
                            ws.Range(ws.Cell(fil, 8), ws.Cell(RowsPan, 8)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "A" Then
                            'ws.Cell(fil, 9).Value = " * "
                            'ws.Cell(fil, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                            ws.Range(ws.Cell(fil, 9), ws.Cell(RowsPan, 9)).Merge()
                            ws.Range(ws.Cell(fil, 9), ws.Cell(RowsPan, 9)).Value = " * "
                            ws.Range(ws.Cell(fil, 9), ws.Cell(RowsPan, 9)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "AD" Then
                            'ws.Cell(fil, 10).Value = " * "
                            'ws.Cell(fil, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                            ws.Range(ws.Cell(fil, 10), ws.Cell(RowsPan, 10)).Merge()
                            ws.Range(ws.Cell(fil, 10), ws.Cell(RowsPan, 10)).Value = " * "
                            ws.Range(ws.Cell(fil, 10), ws.Cell(RowsPan, 10)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        End If



                        contadorIndicador += 1
                        For ii As Integer = 7 To 10
                            ' ws.Cell(fil, ii).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                            With ws.Range(ws.Cell(fil, ii), ws.Cell(RowsPan, ii)).Merge()
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With

                        Next
                        If olibretaIndicador.size = 1 Then
                            fil += 1
                        End If
                    Next
                    ''-------------------------------------------------------------------------------------------------
                    ws.Range(ws.Cell(iniciaIndicador, 11), ws.Cell(iniciaIndicador + contadorIndicador - 1, 11)).Merge()
                    With ws.Range(ws.Cell(iniciaIndicador, 11), ws.Cell(iniciaIndicador + contadorIndicador - 1, 11))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With
                    ''---------------------------------------------------------------------------------------------------
                    nombreCursoTemp = olibretaComponente.nombreCurso
                Next
                fil += 4
                contadorFilas += 4



                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Merge()


                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With




                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center



                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Value = "ABSENCES"
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center






                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With




                ws.Cell(fil + 1, 1).Value = ""
                'ws.Cell(fil + 1, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 2).Value = "Term I"
                ws.Cell(fil + 1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 2)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 3).Value = "Term II"
                ws.Cell(fil + 1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 4).Value = "Term III"
                ws.Cell(fil + 1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 4)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 5).Value = "Term IV"
                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 5)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 6).Value = "Term Total / Average"
                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 1, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 6)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ws.Cell(fil + 2, 1).Value = "Justified"
                ws.Cell(fil + 2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 2, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 2, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 3, 1).Value = "Unjustified"
                ws.Cell(fil + 3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 3, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 3, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 4, 1).Value = "Lateness"
                ws.Cell(fil + 4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 4, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 4, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                For Each filaTb As System.Data.DataRow In tb_Asistencias.Rows
                    If Convert.ToInt32(filaTb("CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                        ws.Cell(fil + 2, 2).Value = filaTb("1FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 2).Value = filaTb("1FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Cell(fil + 4, 2).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString())
                        'ws.Cell(fil + 4, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil + 4, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 4, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Cell(fil + 2, 3).Value = filaTb("2FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 3).Value = filaTb("2FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 4, 3).Value = Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil + 4, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 2, 4).Value = filaTb("3FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ''  ws.Cell(fil + 2, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 3, 4).Value = filaTb("3FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        '					  ws.Cell(fil + 3, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 4, 4).Value = Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 4, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 2, 5).Value = filaTb("4FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 5).Value = filaTb("4FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil + 4, 5).Value = Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center



                        With ws.Cell(fil + 4, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 2, 6).Value = Convert.ToInt32(filaTb("1FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("2FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("3FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("4FaltaJustificada").ToString())
                        ws.Cell(fil + 2, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                        With ws.Cell(fil + 2, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 3, 6).Value = Convert.ToInt32(filaTb("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4FaltaSinJustificar").ToString())
                        ws.Cell(fil + 3, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 3, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 4, 6).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 4, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With
                        Exit For
                    End If
                Next
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Merge()
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Value = "CONDUCTA"
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                ws.Cell(fil + 1, 8).Value = "Term I"

                With ws.Cell(fil + 1, 8)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil + 1, 8).Value = "Term I"
                ws.Cell(fil + 1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ws.Cell(fil + 1, 8).Value = "Term I"
                ws.Cell(fil + 1, 9).Value = "Term II"
                ws.Cell(fil + 1, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 9).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 9)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil + 1, 10).Value = "Term III"
                ws.Cell(fil + 1, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 10).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 10)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 11).Value = "Term IV"
                ws.Cell(fil + 1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 11)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil + 1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center




                With ws.Cell(fil + 2, 8)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                With ws.Cell(fil + 2, 9)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                With ws.Cell(fil + 2, 10)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                With ws.Cell(fil + 2, 11)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                For Each fill As System.Data.DataRow In tb_conducta.Rows
                    If Convert.ToInt32(fill("AL_CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                        If fill("BM_CodigoBimestre").ToString() = "1" Then
                            ws.Cell(fil + 2, 8).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "2" Then
                            ws.Cell(fil + 2, 9).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "3" Then
                            ws.Cell(fil + 2, 10).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "4" Then
                            ws.Cell(fil + 2, 11).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                    End If
                Next



                fil += 4
                Dim nombreCurso As String = ""
                fil += 2
                ''crear numero de visitas a la enfermeria 
                ''
                ''------------------------------------------------------------------------
                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 2, 1))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = "Number of visits to the School Nurse"
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(fil, 2)

                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Value = "Term I"
                End With
                With ws.Cell(fil, 3)

                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Value = "Term II"
                End With
                With ws.Cell(fil, 4)

                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = "Term III"
                End With
                With ws.Cell(fil, 5)

                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Value = "Term IV"
                End With
                With ws.Cell(fil, 6)

                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Value = "Average"
                End With

                Dim ContadorASistenciasIBimestre As Integer = 0
                Dim ContadorASistenciasIIBimestre As Integer = 0
                Dim ContadorASistenciasIIIBimestre As Integer = 0
                Dim ContadorASistenciasIVBimestre As Integer = 0

                '  If Convert.ToInt32(fill("AL_CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                Dim sqlBimestre1 As DataRow() = Nothing
                Dim sqlBimestre2 As DataRow() = Nothing
                Dim sqlBimestre3 As DataRow() = Nothing
                Dim sqlBimestre4 As DataRow() = Nothing
                '   sqlBimestre=dt
                sqlBimestre1 = dst.Tables(dst.Tables.Count - 1).Select("AL_CodigoAlumno=" & lst(iPerosona).codAlumno & " and codBimestre=1")
                ContadorASistenciasIBimestre = sqlBimestre1.Count

                sqlBimestre2 = dst.Tables(dst.Tables.Count - 1).Select("AL_CodigoAlumno=" & lst(iPerosona).codAlumno & " and codBimestre=2")
                ContadorASistenciasIIBimestre = sqlBimestre2.Count

                sqlBimestre3 = dst.Tables(dst.Tables.Count - 1).Select("AL_CodigoAlumno=" & lst(iPerosona).codAlumno & " and codBimestre=3")
                ContadorASistenciasIIIBimestre = sqlBimestre3.Count

                sqlBimestre4 = dst.Tables(dst.Tables.Count - 1).Select("AL_CodigoAlumno=" & lst(iPerosona).codAlumno & " and codBimestre=4")
                ContadorASistenciasIVBimestre = sqlBimestre4.Count
                ''
                ''------------------------------------------------------
                With ws.Range(ws.Cell(fil + 1, 2), ws.Cell(fil + 2, 2))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = ContadorASistenciasIBimestre.ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentHorizontalValues.Center



                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With
                With ws.Range(ws.Cell(fil + 1, 3), ws.Cell(fil + 2, 3))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = ContadorASistenciasIIBimestre.ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentHorizontalValues.Center

                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With
                With ws.Range(ws.Cell(fil + 1, 4), ws.Cell(fil + 2, 4))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = ContadorASistenciasIIIBimestre.ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentHorizontalValues.Center
                End With
                With ws.Range(ws.Cell(fil + 1, 5), ws.Cell(fil + 2, 5))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = ContadorASistenciasIVBimestre.ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentHorizontalValues.Center
                End With
                With ws.Range(ws.Cell(fil + 1, 6), ws.Cell(fil + 2, 6))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = (ContadorASistenciasIBimestre + ContadorASistenciasIIBimestre + ContadorASistenciasIIIBimestre + ContadorASistenciasIVBimestre).ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentHorizontalValues.Center
                End With
                ''------------------------------------------------------

                ''
                fil += 4
                ws.Cell(fil, 1).Value = "COMMENTS"
                ws.Cell(fil, 1).Style.Font.Bold = True
                fil += 1

                'ws.Cell(fil, 1).Value = "TUTOR"' modificado para que   no salga a



                Dim estadoFilasMayor225 As Boolean = False


                For iComp As Integer = 0 To lst(iPerosona).lstLibretaComponente.Count - 1
                    If lst(iPerosona).lstLibretaComponente(iComp).nombreCurso <> nombreCurso Then
                        If lst(iPerosona).lstLibretaComponente(iComp).observacionCurso = "" Then
                            Continue For
                        End If
                        fil += 1

                        If lst(iPerosona).opaginacion.pag3.superior - (fil + 6) < 0 And Not estadoFilasMayor225 Then
                            fil = fil + (lst(iPerosona).opaginacion.pag3.superior * 3 - fil) + 2
                            estadoFilasMayor225 = True
                        End If

                        ws.Cell(fil, 1).Value = lst(iPerosona).lstLibretaComponente(iComp).nombreCurso
                        ws.Cell(fil, 1).Style.Font.Bold = True
                        fil += 1
                        ''ws.Cell(fil, 1).Value = olibretaComponenteTemp.observacionCurso
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Merge()
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Value = lst(iPerosona).lstLibretaComponente(iComp).observacionCurso
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Style.Font.FontSize = 14
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Style.Alignment.WrapText = True
                        fil += 5
                    End If
                    nombreCurso = lst(iPerosona).lstLibretaComponente(iComp).nombreCurso
                Next
                fil += 1

                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Merge()
                With ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3))
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                End With



                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Value = "TUTOR"
                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Merge()

                With ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7))
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                End With
                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Value = "PARENTS"

                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center






                ''
                fil = lst(iPerosona).opaginacion.pag3.superior + 1
                alumnosProcesado += 1
            Next

            ''
            ws.Column(1).Width = 12
            ws.Column(2).Width = 17
            ws.Column(3).Width = 17
            ws.Column(4).Width = 17
            ws.Column(5).Width = 17
            ws.Column(11).Width = 8
            ws.Column(6).Width = 19
            ws.Column(7).Width = 7
            ws.Column(8).Width = 7
            ws.Column(9).Width = 7
            ws.Column(10).Width = 8
            ws.PageSetup.AdjustTo(60)

            ws.PageSetup.Margins.Top = 0.75 '1.9
            ws.PageSetup.Margins.Bottom = 0.75 '1.9
            ws.PageSetup.Margins.Left = 0.7 '0.6
            ws.PageSetup.Margins.Right = 0.7 '0.6
            ws.PageSetup.Margins.Header = 0.3 '0.8
            ws.PageSetup.Margins.Footer = 0.3 '0.8

            ' 

            ws.PageSetup.PrintAreas.Add("A1:K" & lst(lst.Count - 1).opaginacion.pag3.superior.ToString())

            ''ws.PageSetup.PrintAreas.Add("A1:K" & fil.ToString())


            ws.PageSetup.PagesWide = 1
            workbook.Save()

            Return nombreArchivo


        Catch ex As Exception
            MessageBox.Show("error")
        End Try
    End Function
    ''
    ''



    Function crearLibretaPrimariaUnaSolaHojaCuatroHojas(ByVal codigoAula As Integer, ByVal int_bimestre As Integer, ByVal codAlumno As String) As String

        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")


        Dim rutaLibreta As String = System.Configuration.ConfigurationManager.AppSettings("LibretaPrimariaCuatroHojas").ToString()

        Dim temp As String = System.Configuration.ConfigurationManager.AppSettings("Temporales").ToString()
        Dim rutaApp As String = ""
        rutaApp = Environment.CurrentDirectory()

        File.Copy(rutaApp + rutaLibreta, rutaApp + temp & rutaTemp & ".xlsx")
        Dim nombreArchivo As String = rutaApp + temp & rutaTemp & ".xlsx"


        Dim tb_Asistencias As New System.Data.DataTable '' extraer las inasistencias del alumno
        Dim tb_demeritos As New System.Data.DataTable '' extrar los meritos y demeritos del alumno 
        Dim tb_conducta As New System.Data.DataTable


        Try
            ''
            Dim dt As New System.Data.DataTable
            Dim dst As New DataSet
            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaPrimaria_1(codigoAula, int_bimestre, codAlumno, 1, 1, 1, 1)
            dt = dst.Tables(0)
            ''
            tb_Asistencias = dst.Tables(3)
            tb_demeritos = dst.Tables(4)
            tb_conducta = dst.Tables(5)

            ''
            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)


            Dim contadorPaginas As Integer = 0
            Dim paginaInicialInferior As Integer = -74
            Dim paginaIinicialSuperior As Integer = 0

            Dim flagPrimero As Integer = 0
            ''
            For Each opersona As personaLibreta In lst
                paginaInicialInferior += 75
                paginaIinicialSuperior += 75

                opersona.opaginacion.pag1.inferior = paginaInicialInferior
                opersona.opaginacion.pag1.superior = paginaIinicialSuperior

                paginaInicialInferior += 75
                paginaIinicialSuperior += 75

                opersona.opaginacion.pag2.inferior = paginaInicialInferior
                opersona.opaginacion.pag2.superior = paginaIinicialSuperior

                paginaInicialInferior += 75
                paginaIinicialSuperior += 75

                opersona.opaginacion.pag3.inferior = paginaInicialInferior
                opersona.opaginacion.pag3.superior = paginaIinicialSuperior


                paginaInicialInferior += 75
                paginaIinicialSuperior += 75

                opersona.opaginacion.pag4.inferior = paginaInicialInferior
                opersona.opaginacion.pag4.superior = paginaIinicialSuperior


            Next
            'Exit Function

            ''

            Dim workbook As New ClosedXML.Excel.XLWorkbook(nombreArchivo)

            workbook.CalculateMode = XLCalculateMode.Auto

            '' seleccionar la primera hoja 
            Dim ws = workbook.Worksheet(1)



            Dim nombreCursoTemp As String = ""
            Dim contadorIndicador As Integer = 0
            Dim filaCount As Integer = 0
            Dim iniciaIndicador As Integer = 0
            Dim indiceHojas As Integer = 0

            ''
            Dim abrBimestre As String = ""
            If int_bimestre = 1 Then
                abrBimestre = "I"
            End If
            If int_bimestre = 2 Then
                abrBimestre = "II"
            End If
            If int_bimestre = 3 Then
                abrBimestre = "III"
            End If
            If int_bimestre = 4 Then
                abrBimestre = "IV"
            End If


            Dim lstPaginas As New List(Of Integer)
            Dim sumaPag As Integer = 0
            For inicio As Integer = 1 To 100
                sumaPag += 75
                lstPaginas.Add(sumaPag)
            Next

            Dim ci As Globalization.CultureInfo = New Globalization.CultureInfo("en-US")

            Date.Now.ToString("MMMM", ci)
            ''
            Dim agregoFilas As Integer = 0
            Dim agregoFilas1 As Integer = 0

            Dim boolEstado As Boolean = True
            ''iterando la lista de alumnmos 

            ''variables  globales
            Dim fil As Integer = 2
            Dim contadorFilas As Integer = 0
            Dim empiezanFilas As Integer = 0
            ''
            ''============================================
            ''
            ''--------------------------------------------
            '' 
            ''
            ''
            ''--------------------------------------------

            Dim contadoPaginas As Integer = 0

            For iPerosona As Integer = 0 To lst.Count - 1
                contadorFilas = 0
                agregoFilas = 0
                agregoFilas1 = 0

                indiceHojas += 1
                contadoPaginas += 75

                ''
                ''1.pintado del reports card 
                ''------------------------------------------------------
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Merge()
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Value = "REPORT CARD"
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Style.Font.Bold = True
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ''------------------------------------------------------

                ''2.pintado del nombre 
                ''--------------------------------------------------------
                ''2.1 pintado de la etiqueta  del nombre

                empiezanFilas = fil
                fil += 1 ''  
                contadoPaginas += 75

                contadorFilas += 1



                ws.Cell(fil, 3).Value = "NAME"
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                ''2.2 pintado del nombre  del alumno
                ''---------------------------------------------------------
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = lst(iPerosona).nombreAlumno
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                ''pintado de class
                ''---------------------------------------------------------
                fil += 1
                contadorFilas += 1
                ws.Cell(fil, 3).Value = "CLASS"
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                ''pintado del nombre del tutor 
                ''---------------------------------------------------------

                ''

                ''pintado del "DATE"
                ''--------------------------------------
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Merge()
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Value = "Date: " & Date.Now.ToString("MMMM", ci) & " " & Date.Now.Day.ToString() & "," & Date.Now.Year().ToString

                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Style.Alignment.Indent = 2


                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Style.Font.Bold = True
                ''---------------------------------------
                ''
                fil += 1
                contadorFilas += 1
                ws.Cell(fil, 3).Value = "TUTOR"
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = dst.Tables(2).Rows(0)("nombre").ToString()

                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ''---------------------------------------------------------
                fil += 1
                contadorFilas += 1
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Merge()
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Value = "SUBJECT AREAS -" & Now().Year().ToString()
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Style.Font.Bold = True
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Style.Font.FontSize = 16

                ''pintado de "TERM"
                ws.Cell(fil, 11).Value = "TERM " & abrBimestre
                ws.Cell(fil, 11).Style.Font.Bold = True
                '

                '
                'pintado de cursos
                '---------------------------------------------------------------------
                Dim estadoFilas As Boolean = False
                For Each olibretaComponente As libretaComponente In lst(iPerosona).lstLibretaComponente
                    fil += 1
                    contadorFilas += 1

                    If nombreCursoTemp <> olibretaComponente.nombreCurso Then


                        Dim nombreTemp As String = ""
                        nombreTemp = olibretaComponente.nombreCurso
                        Dim count = From o In lst(iPerosona).lstLibretaComponente Where o.nombreCurso = nombreTemp _
                            Select o.lstIndicador
                        Dim cntListas As Integer = 0
                        'cntListas = count.Count()
                        Dim cantidadComponente = From o In lst(iPerosona).lstLibretaComponente Where o.nombreCurso = nombreTemp Select o.nombreComponente Distinct
                        Dim contador As Integer = 0
                        For Each el In count
                            Dim exis = From i In el Where i.size = 1

                            If exis.Count = 1 Then
                                contador += el.Count() + exis.Count
                            Else
                                contador += el.Count()
                            End If


                        Next
                        Dim cnt As Integer = contador + 2 + (cantidadComponente.Count)







                        'If Not fil + cnt <= contadoPaginas And fil < contadoPaginas + 75 And agregoFilas = 0 Then
                        '    Dim diferencias As Integer = 0
                        '    diferencias = contadoPaginas + 2 - fil
                        '    fil += diferencias
                        '    agregoFilas = 1
                        'End If
                        'If Not fil + cnt <= contadoPaginas + 75 And fil > contadoPaginas And agregoFilas1 = 0 Then

                        '    Dim diferencias As Integer = 0
                        '    diferencias = contadoPaginas * 2 - fil
                        '    fil += diferencias + 3
                        '    agregoFilas1 = 1
                        'End If
                        'If Not fil + cnt <= contadoPaginas * 3 And fil > contadoPaginas * 2 And agregoFilas1 = 0 Then

                        '    Dim diferencias As Integer = 0
                        '    diferencias = contadoPaginas * 3 - fil
                        '    fil += diferencias + 3
                        '    agregoFilas1 = 1
                        'End If
                        Dim diferencias As Integer = 0


                        'lst(iPerosona)


                        If fil >= lst(iPerosona).opaginacion.pag1.inferior And fil <= lst(iPerosona).opaginacion.pag1.superior Then

                            If fil + cnt > lst(iPerosona).opaginacion.pag1.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag1.superior - fil
                                fil += diferencias + 2
                            End If

                        End If

                        If fil >= lst(iPerosona).opaginacion.pag2.inferior And fil <= lst(iPerosona).opaginacion.pag2.superior Then

                            If fil + cnt > lst(iPerosona).opaginacion.pag2.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag2.superior - fil
                                fil += diferencias + 2
                            End If

                        End If
                        If fil >= lst(iPerosona).opaginacion.pag3.inferior And fil <= lst(iPerosona).opaginacion.pag3.superior Then

                            If fil + cnt > lst(iPerosona).opaginacion.pag3.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag3.superior - fil
                                fil += diferencias + 2
                            End If

                        End If
                        If fil >= lst(iPerosona).opaginacion.pag4.inferior And fil <= lst(iPerosona).opaginacion.pag4.superior Then

                            If fil + cnt > lst(iPerosona).opaginacion.pag4.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag4.superior - fil
                                fil += diferencias + 2
                            End If

                        End If



                        filaCount = fil + 1


                        'If Not fil + cnt <= 75 And fil < 150 And agregoFilas = 0 Then
                        '    Dim diferencias As Integer = 0
                        '    diferencias = 77 - fil
                        '    fil += diferencias
                        '    agregoFilas = 1

                        'End If
                        'If Not fil + cnt <= 150 And fil > 75 And agregoFilas1 = 0 Then
                        '    Dim diferencias As Integer = 0
                        '    diferencias = 150 - fil
                        '    fil += diferencias + 3
                        '    agregoFilas1 = 1

                        'End If




                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Merge()
                        With ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Value = olibretaComponente.nombreCurso.ToUpper() '' & " nombre alumno " & opersonaLibreta.nombreAlumno
                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Style.Font.Bold = True
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Merge()
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Value = "PERFORMANCE"
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 11).Value = "AVERAGE"
                        ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        ' ws.Cell(fil, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 11)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        fil = filaCount
                        contadorFilas += 1

                        ws.Cell(fil, 7).Value = "C"
                        ws.Cell(fil, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        'ws.Cell(fil, 7).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                        With ws.Cell(fil, 7)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 8).Value = "B"
                        ws.Cell(fil, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil, 8).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 8)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil, 9).Value = "A"
                        ws.Cell(fil, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil, 9).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 9)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With




                        ws.Cell(fil, 10).Value = "AD"
                        ws.Cell(fil, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil, 10).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 10)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 11).Value = olibretaComponente.promedioComponente.ToUpper()
                        ' ws.Cell(fil, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil, 11)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        fil += 1
                        contadorFilas += 1

                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Merge()
                        'ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                    End If

                    'If olibretaComponente.nombreComponente = "READING" Then
                    '    fil += 5
                    'End If


                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Merge()
                    ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Value = olibretaComponente.nombreComponente.ToUpper()






                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.WrapText = True

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Font.Bold = True

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    ws.Cell(fil, 11).Value = olibretaComponente.notaComponente.ToUpper()

                    ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center



                    With ws.Cell(fil, 11)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    iniciaIndicador = fil + 1
                    contadorIndicador = 0
                    ''------------------------------------------------------------------------------

                    Dim RowsPan As Integer = 0
                    For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador
                        RowsPan = 0
                        fil += 1
                        contadorFilas += 1
                        If olibretaIndicador.size = 1 Then
                            RowsPan += fil + 1
                            contadorIndicador += 1

                        Else
                            RowsPan = fil
                        End If

                        ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6)).Merge()
                        ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6)).Value = olibretaIndicador.nombreIndicador
                        ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6)).Style.Alignment.WrapText = True
                        ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6)).Style.Alignment.Indent = 2

                        With ws.Range(ws.Cell(fil, 1), ws.Cell(RowsPan, 6))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With
                        If olibretaIndicador.notaIndicador.ToUpper() = "C" Then
                            ws.Range(ws.Cell(fil, 7), ws.Cell(RowsPan, 7)).Merge()
                            ws.Range(ws.Cell(fil, 7), ws.Cell(RowsPan, 7)).Value = " * "
                            ws.Range(ws.Cell(fil, 7), ws.Cell(RowsPan, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "B" Then

                            'ws.Cell(fil, 8).Value = " * "
                            'ws.Cell(fil, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                            ws.Range(ws.Cell(fil, 8), ws.Cell(RowsPan, 8)).Merge()
                            ws.Range(ws.Cell(fil, 8), ws.Cell(RowsPan, 8)).Value = " * "
                            ws.Range(ws.Cell(fil, 8), ws.Cell(RowsPan, 8)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "A" Then
                            'ws.Cell(fil, 9).Value = " * "
                            'ws.Cell(fil, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                            ws.Range(ws.Cell(fil, 9), ws.Cell(RowsPan, 9)).Merge()
                            ws.Range(ws.Cell(fil, 9), ws.Cell(RowsPan, 9)).Value = " * "
                            ws.Range(ws.Cell(fil, 9), ws.Cell(RowsPan, 9)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "AD" Then
                            'ws.Cell(fil, 10).Value = " * "
                            'ws.Cell(fil, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                            ws.Range(ws.Cell(fil, 10), ws.Cell(RowsPan, 10)).Merge()
                            ws.Range(ws.Cell(fil, 10), ws.Cell(RowsPan, 10)).Value = " * "
                            ws.Range(ws.Cell(fil, 10), ws.Cell(RowsPan, 10)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        End If



                        contadorIndicador += 1
                        For ii As Integer = 7 To 10
                            ' ws.Cell(fil, ii).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                            With ws.Range(ws.Cell(fil, ii), ws.Cell(RowsPan, ii)).Merge()
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With

                        Next
                        If olibretaIndicador.size = 1 Then
                            fil += 1
                        End If
                    Next
                    ''-------------------------------------------------------------------------------------------------
                    ws.Range(ws.Cell(iniciaIndicador, 11), ws.Cell(iniciaIndicador + contadorIndicador - 1, 11)).Merge()
                    With ws.Range(ws.Cell(iniciaIndicador, 11), ws.Cell(iniciaIndicador + contadorIndicador - 1, 11))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With
                    ''---------------------------------------------------------------------------------------------------
                    nombreCursoTemp = olibretaComponente.nombreCurso
                Next
                fil += 4
                contadorFilas += 4



                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Merge()


                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With




                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center



                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Value = "ABSENCES"
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center






                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With




                ws.Cell(fil + 1, 1).Value = ""
                'ws.Cell(fil + 1, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 2).Value = "Term I"
                ws.Cell(fil + 1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 2)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 3).Value = "Term II"
                ws.Cell(fil + 1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 4).Value = "Term III"
                ws.Cell(fil + 1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 4)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 5).Value = "Term IV"
                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 5)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 6).Value = "Term Total / Average"
                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 1, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 6)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ws.Cell(fil + 2, 1).Value = "Justified"
                ws.Cell(fil + 2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 2, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 2, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 3, 1).Value = "Unjustified"
                ws.Cell(fil + 3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 3, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 3, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 4, 1).Value = "Lateness"
                ws.Cell(fil + 4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 4, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 4, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                For Each filaTb As System.Data.DataRow In tb_Asistencias.Rows
                    If Convert.ToInt32(filaTb("CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                        ws.Cell(fil + 2, 2).Value = filaTb("1FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 2).Value = filaTb("1FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Cell(fil + 4, 2).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString())
                        'ws.Cell(fil + 4, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil + 4, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 4, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Cell(fil + 2, 3).Value = filaTb("2FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 3).Value = filaTb("2FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 4, 3).Value = Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil + 4, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 2, 4).Value = filaTb("3FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ''  ws.Cell(fil + 2, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 3, 4).Value = filaTb("3FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        '					  ws.Cell(fil + 3, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 4, 4).Value = Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 4, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 2, 5).Value = filaTb("4FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 5).Value = filaTb("4FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil + 4, 5).Value = Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center



                        With ws.Cell(fil + 4, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 2, 6).Value = Convert.ToInt32(filaTb("1FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("2FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("3FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("4FaltaJustificada").ToString())
                        ws.Cell(fil + 2, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                        With ws.Cell(fil + 2, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 3, 6).Value = Convert.ToInt32(filaTb("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4FaltaSinJustificar").ToString())
                        ws.Cell(fil + 3, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 3, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 4, 6).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 4, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With
                        Exit For
                    End If
                Next
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Merge()
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Value = "CONDUCTA"
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                ws.Cell(fil + 1, 8).Value = "Term I"

                With ws.Cell(fil + 1, 8)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil + 1, 8).Value = "Term I"
                ws.Cell(fil + 1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ws.Cell(fil + 1, 8).Value = "Term I"
                ws.Cell(fil + 1, 9).Value = "Term II"
                ws.Cell(fil + 1, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 9).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 9)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil + 1, 10).Value = "Term III"
                ws.Cell(fil + 1, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 10).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 10)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 11).Value = "Term IV"
                ws.Cell(fil + 1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 11)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil + 1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center




                With ws.Cell(fil + 2, 8)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                With ws.Cell(fil + 2, 9)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                With ws.Cell(fil + 2, 10)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                With ws.Cell(fil + 2, 11)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                For Each fill As System.Data.DataRow In tb_conducta.Rows
                    If Convert.ToInt32(fill("AL_CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                        If fill("BM_CodigoBimestre").ToString() = "1" Then
                            ws.Cell(fil + 2, 8).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "2" Then
                            ws.Cell(fil + 2, 9).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "3" Then
                            ws.Cell(fil + 2, 10).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "4" Then
                            ws.Cell(fil + 2, 11).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                    End If
                Next



                fil += 4
                Dim nombreCurso As String = ""
                fil += 2
                ''crear numero de visitas a la enfermeria 
                ''
                ''------------------------------------------------------------------------
                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 2, 1))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = "Number of visits to the School Nurse"
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(fil, 2)

                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Value = "Term I"
                End With
                With ws.Cell(fil, 3)

                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Value = "Term II"
                End With
                With ws.Cell(fil, 4)

                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = "Term III"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                End With
                With ws.Cell(fil, 5)

                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Value = "Term IV"
                End With
                With ws.Cell(fil, 6)

                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Value = "Average"
                End With

                Dim ContadorASistenciasIBimestre As Integer = 0
                Dim ContadorASistenciasIIBimestre As Integer = 0
                Dim ContadorASistenciasIIIBimestre As Integer = 0
                Dim ContadorASistenciasIVBimestre As Integer = 0

                '  If Convert.ToInt32(fill("AL_CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                Dim sqlBimestre1 As DataRow() = Nothing
                Dim sqlBimestre2 As DataRow() = Nothing
                Dim sqlBimestre3 As DataRow() = Nothing
                Dim sqlBimestre4 As DataRow() = Nothing
                '   sqlBimestre=dt
                sqlBimestre1 = dst.Tables(dst.Tables.Count - 1).Select("AL_CodigoAlumno=" & lst(iPerosona).codAlumno & " and codBimestre=1")
                ContadorASistenciasIBimestre = sqlBimestre1.Count

                sqlBimestre2 = dst.Tables(dst.Tables.Count - 1).Select("AL_CodigoAlumno=" & lst(iPerosona).codAlumno & " and codBimestre=2")
                ContadorASistenciasIIBimestre = sqlBimestre2.Count

                sqlBimestre3 = dst.Tables(dst.Tables.Count - 1).Select("AL_CodigoAlumno=" & lst(iPerosona).codAlumno & " and codBimestre=3")
                ContadorASistenciasIIIBimestre = sqlBimestre3.Count

                sqlBimestre4 = dst.Tables(dst.Tables.Count - 1).Select("AL_CodigoAlumno=" & lst(iPerosona).codAlumno & " and codBimestre=4")
                ContadorASistenciasIVBimestre = sqlBimestre4.Count
                ''
                ''------------------------------------------------------
                With ws.Range(ws.Cell(fil + 1, 2), ws.Cell(fil + 2, 2))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = ContadorASistenciasIBimestre.ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With
                With ws.Range(ws.Cell(fil + 1, 3), ws.Cell(fil + 2, 3))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = ContadorASistenciasIIBimestre.ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentHorizontalValues.Center


                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                End With
                With ws.Range(ws.Cell(fil + 1, 4), ws.Cell(fil + 2, 4))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = ContadorASistenciasIIIBimestre.ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentHorizontalValues.Center


                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                End With
                With ws.Range(ws.Cell(fil + 1, 5), ws.Cell(fil + 2, 5))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = ContadorASistenciasIVBimestre.ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentHorizontalValues.Center


                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With
                With ws.Range(ws.Cell(fil + 1, 6), ws.Cell(fil + 2, 6))
                    .Merge()
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Value = (ContadorASistenciasIBimestre + ContadorASistenciasIIBimestre + ContadorASistenciasIIIBimestre + ContadorASistenciasIVBimestre).ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentHorizontalValues.Center


                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With
                ''------------------------------------------------------

                ''
                fil += 4
                ws.Cell(fil, 1).Value = "COMMENTS"
                ws.Cell(fil, 1).Style.Font.Bold = True
                fil += 1
                ws.Cell(fil, 1).Value = "TUTOR"



                Dim estadoFilasMayor225 As Boolean = False


                For iComp As Integer = 0 To lst(iPerosona).lstLibretaComponente.Count - 1
                    If lst(iPerosona).lstLibretaComponente(iComp).nombreCurso <> nombreCurso Then
                        If lst(iPerosona).lstLibretaComponente(iComp).observacionCurso = "" Then
                            Continue For
                        End If
                        fil += 1

                        'If lst(iPerosona).opaginacion.pag4.superior - (fil + 6) < 0 And Not estadoFilasMayor225 Then
                        '    fil = fil + (lst(iPerosona).opaginacion.pag4.superior * 3 - fil) + 2
                        '    estadoFilasMayor225 = True
                        'End If

                        'If lst(iPerosona).opaginacion.pag4.superior - (fil + 6) < 0 And Not estadoFilasMayor225 Then
                        '    fil = fil + (lst(iPerosona).opaginacion.pag4.superior * 3 - fil) + 2
                        '    estadoFilasMayor225 = True
                        'End If
                        Dim diferencias As Integer = 0

                        ''-------------------
                        If fil >= lst(iPerosona).opaginacion.pag1.inferior And fil <= lst(iPerosona).opaginacion.pag1.superior Then

                            If fil + 8 > lst(iPerosona).opaginacion.pag1.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag1.superior - fil
                                fil += diferencias + 2
                            End If

                        End If

                        If fil >= lst(iPerosona).opaginacion.pag2.inferior And fil <= lst(iPerosona).opaginacion.pag2.superior Then

                            If fil + 8 > lst(iPerosona).opaginacion.pag2.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag2.superior - fil
                                fil += diferencias + 2
                            End If

                        End If
                        If fil >= lst(iPerosona).opaginacion.pag3.inferior And fil <= lst(iPerosona).opaginacion.pag3.superior Then

                            If fil + 8 > lst(iPerosona).opaginacion.pag3.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag3.superior - fil
                                fil += diferencias + 2
                            End If

                        End If
                        If fil >= lst(iPerosona).opaginacion.pag4.inferior And fil <= lst(iPerosona).opaginacion.pag4.superior Then

                            If fil + 8 > lst(iPerosona).opaginacion.pag4.superior Then
                                diferencias = lst(iPerosona).opaginacion.pag4.superior - fil
                                fil += diferencias + 2
                            End If

                        End If
                        ''-------------------
                        Dim masFilas As Integer = 0
                        If lst(iPerosona).lstLibretaComponente(iComp).nombreCurso.ToLower = ("Tutoría").ToLower Then
                            masFilas = 7
                        Else
                            masFilas = 5

                        End If

                        ws.Cell(fil, 1).Value = lst(iPerosona).lstLibretaComponente(iComp).nombreCurso
                        ws.Cell(fil, 1).Style.Font.Bold = True
                        fil += 1
                        ''ws.Cell(fil, 1).Value = olibretaComponenteTemp.observacionCurso
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + masFilas, 11)).Merge()
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + masFilas, 11)).Value = lst(iPerosona).lstLibretaComponente(iComp).observacionCurso
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + masFilas, 11)).Style.Font.FontSize = 14
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + masFilas, 11)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + masFilas, 11)).Style.Alignment.WrapText = True
                        fil += masFilas + 2
                    End If
                    nombreCurso = lst(iPerosona).lstLibretaComponente(iComp).nombreCurso
                Next
                fil += 1

                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Merge()
                With ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3))
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                End With



                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Value = "TUTOR"
                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Merge()

                With ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7))
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                End With
                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Value = "PARENTS"

                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center






                ''
                fil = lst(iPerosona).opaginacion.pag4.superior + 3

            Next

            ''
            ws.Column(1).Width = 12
            ws.Column(2).Width = 17
            ws.Column(3).Width = 17
            ws.Column(4).Width = 17
            ws.Column(5).Width = 17
            ws.Column(11).Width = 8
            ws.Column(6).Width = 19
            ws.Column(7).Width = 7
            ws.Column(8).Width = 7
            ws.Column(9).Width = 7
            ws.Column(10).Width = 8
            ws.PageSetup.AdjustTo(60)

            ws.PageSetup.Margins.Top = 0.75 '1.9
            ws.PageSetup.Margins.Bottom = 0.75 '1.9
            ws.PageSetup.Margins.Left = 0.7 '0.6
            ws.PageSetup.Margins.Right = 0.7 '0.6
            ws.PageSetup.Margins.Header = 0.3 '0.8
            ws.PageSetup.Margins.Footer = 0.3 '0.8

            ' 

            ws.PageSetup.PrintAreas.Add("A1:K" & lst(lst.Count - 1).opaginacion.pag4.superior.ToString())

            ''ws.PageSetup.PrintAreas.Add("A1:K" & fil.ToString())


            ws.PageSetup.PagesWide = 1
            workbook.Save()

            Return nombreArchivo


        Catch ex As Exception

        End Try
    End Function
#End Region


#Region "Funciones crear libreta primaria inicial"
    Public Class paginacion
        Public pag1 As New paginaInferiorSuperior
        Public pag2 As New paginaInferiorSuperior
        Public pag3 As New paginaInferiorSuperior
        Public pag4 As New paginaInferiorSuperior

    End Class


    Public Class paginaInferiorSuperior
        Public inferior As Integer
        Public superior As Integer
    End Class
    Public Class personaLibreta
        Public nombreAlumno As String
        Public codAlumno As String
        Public conductaBimestral As String
        Public lstLibretaComponente As New List(Of libretaComponente)
        Public cantidadComponente As Integer
        Public cantidadCurso As Integer
        Public cantidadIndicador As Integer
        Public sumaTotal As Integer
        Public cabezera As Integer
        Public pieReporte As Integer
        Public opaginacion As New paginacion
        '  Public lstCursoLibreta As New List(Of CursoLibreta)
    End Class

    Public Class libretaComponente
        Public codAlumno As String
        Public codRegComponente As String
        Public nombreCurso As String

        Public columna As Boolean

        Public nombreComponente As String
        Public notaComponente As String
        Public promedioComponente As String
        Public observacionCurso As String
        Public lstIndicador As New List(Of libretaIndicador)
        Public cantidadIndicador As Integer
        Public cantidadComponente As Integer
    End Class

    Public Class libretaIndicador
        Public codComponente As String
        Public nombreIndicador As String
        Public notaIndicador As String
        Public codIndicador As String
        Public size As Integer
    End Class

    Public Function crearListaLibreta(ByVal dt_tb As System.Data.DataTable) As List(Of personaLibreta)
        Dim lstLibretaAlumno As New List(Of personaLibreta)
        Dim lstLibretasComponente As New List(Of libretaComponente)
        Dim lstLibretaIndicador As New List(Of libretaIndicador)

        Dim lstLibretaAlumnoRes As New List(Of personaLibreta)
        lstLibretaAlumno = crearListaAlumnos(dt_tb)
        lstLibretasComponente = creaListaComponente(dt_tb)
        lstLibretaIndicador = crearListaLibretaIndicador(dt_tb)


        Dim opersonaLibretaT As personaLibreta
        Dim oLibretaComponenteT As libretaComponente
        Dim oLibretaIndicadorT As libretaIndicador
        Dim totalFilasPersonas As Integer = 0

        ' Dim cantidadCursos
        For Each opersonaLibreta As personaLibreta In lstLibretaAlumno
            opersonaLibretaT = New personaLibreta
            opersonaLibretaT.codAlumno = opersonaLibreta.codAlumno
            opersonaLibretaT.nombreAlumno = opersonaLibreta.nombreAlumno
            opersonaLibretaT.conductaBimestral = opersonaLibreta.conductaBimestral
            For Each oLibretaComponente As libretaComponente In lstLibretasComponente
                oLibretaComponenteT = New libretaComponente
                oLibretaComponenteT.codRegComponente = oLibretaComponente.codRegComponente
                oLibretaComponenteT.nombreComponente = oLibretaComponente.nombreComponente
                oLibretaComponenteT.notaComponente = oLibretaComponente.notaComponente
                oLibretaComponenteT.codAlumno = oLibretaComponente.codAlumno
                oLibretaComponenteT.nombreCurso = oLibretaComponente.nombreCurso
                oLibretaComponenteT.promedioComponente = oLibretaComponente.promedioComponente
                oLibretaComponenteT.columna = oLibretaComponente.columna
                oLibretaComponenteT.observacionCurso = oLibretaComponente.observacionCurso
                For Each oLibretaIndicador As libretaIndicador In lstLibretaIndicador

                    oLibretaIndicadorT = New libretaIndicador
                    oLibretaIndicadorT.nombreIndicador = oLibretaIndicador.nombreIndicador
                    oLibretaIndicadorT.notaIndicador = oLibretaIndicador.notaIndicador
                    oLibretaIndicadorT.codComponente = oLibretaIndicador.codComponente
                    oLibretaIndicadorT.size = oLibretaIndicador.size

                    If oLibretaComponenteT.codRegComponente = oLibretaIndicadorT.codComponente Then
                        oLibretaComponenteT.lstIndicador.Add(oLibretaIndicadorT)
                    End If
                Next

                If opersonaLibretaT.codAlumno = oLibretaComponenteT.codAlumno Then

                    oLibretaComponenteT.cantidadIndicador = oLibretaComponenteT.lstIndicador.Count

                    '  from sqlCantidadIndicadorCurso in oLibretaComponenteT Select 
                    Dim listaCurso = From h In opersonaLibretaT.lstLibretaComponente Select h.nombreCurso Distinct


                    Dim catidadComponente = From sq In opersonaLibretaT.lstLibretaComponente Where sq.nombreCurso = listaCurso(0)
                    oLibretaComponenteT.cantidadComponente = catidadComponente.Count + 1
                    opersonaLibretaT.lstLibretaComponente.Add(oLibretaComponenteT)
                End If

            Next

            '
            ' Dim cantidadComponenteCurso = From sqlCantidaCompoenenteCuso In opersonaLibretaT.lstLibretaComponente Select sqlCantidaCompoenenteCuso.Count()




            Dim cantidadIndicadores = (From sq In opersonaLibretaT.lstLibretaComponente Select sq.lstIndicador.Count()).Sum()

            opersonaLibretaT.cantidadIndicador = cantidadIndicadores
            opersonaLibretaT.cantidadComponente = opersonaLibretaT.lstLibretaComponente.Count

            Dim lstCursos = (From sql In opersonaLibretaT.lstLibretaComponente Distinct Select sql.nombreCurso)

            opersonaLibretaT.cantidadCurso = lstCursos.Distinct().Count

            '' calcular numero de filas por alumno
            opersonaLibretaT.cabezera = 7
            opersonaLibretaT.pieReporte = 20
            Dim cantidadFilaIndicador As Integer = 0
            Dim cantidadComponente As Integer = 0
            ''
            opersonaLibretaT.sumaTotal = (opersonaLibretaT.cantidadCurso * 2) + (opersonaLibretaT.cantidadComponente + opersonaLibretaT.cantidadIndicador)
            lstLibretaAlumnoRes.Add(opersonaLibretaT)
        Next


        REM cursos * 2 + cantidadCompoenente + totalIndicador+10+25



        'Dim lstCursoOrdenar As New List(Of curso)
        'lstCursoOrdenar = From lstOrdenado In lstCurso Order By lstOrdenado.orderCurso Ascending Select lstOrdenado


        Return lstLibretaAlumnoRes


        'Dim lPersona1 = Nothing

        'lPersona1 = From lp In lPersona Order By lp.nombrepersona Ascending Select lp
    End Function
    Function creaListaComponente(ByVal dt As System.Data.DataTable) As List(Of libretaComponente)

        Dim olibretaComponente As libretaComponente
        Dim codComponenteTemp As String = ""
        Dim lstRegistroNotaComponente As New List(Of libretaComponente)
        For Each fila As System.Data.DataRow In dt.Rows

            olibretaComponente = New libretaComponente
            olibretaComponente.codAlumno = fila("AL_CodigoAlumno").ToString()
            olibretaComponente.nombreCurso = fila("NC_Descripcion").ToString()
            olibretaComponente.codRegComponente = fila("RNC_CodigoRegistroNotaComponente").ToString()
            olibretaComponente.nombreComponente = fila("CP_Descripcion").ToString()
            olibretaComponente.notaComponente = fila("RNC_NotaComponente").ToString()
            olibretaComponente.promedioComponente = fila("RNBL_NotaFinalBimestre").ToString()

            olibretaComponente.columna = Convert.ToBoolean(fila("grupoLibreta").ToString())

            olibretaComponente.observacionCurso = fila("RNBL_ObservacionCurso").ToString()





            If olibretaComponente.codRegComponente <> codComponenteTemp Then

                lstRegistroNotaComponente.Add(olibretaComponente)
            End If

            codComponenteTemp = olibretaComponente.codRegComponente
        Next

        Return lstRegistroNotaComponente
        ''RNC_CodigoRegistroNotaComponente RNI_CodigoRegistroNotaIndicador AL_CodigoAlumno AGC_CodigoAsignacionGrupo pComponente CP_Descripcion	ID_Descripcion	BM_CodigoBimestre	RNBL_CodigoRegistroBimestralL	RNC_NotaComponente	RNI_NotaIndicador	RNBL_ObservacionCurso	RNBL_NotaFinalBimestre	RC_CodigoRegistroComponentes	RI_CodigoRegistroIndicadores	NC_Descripcion	CS_CodigoCurso
        ''26561	87805	20100052	687	26561	DOMINIO CORPORAL Y EXPRESIÓN CREATIVA.	Controla movimientos de su  cuerpo durante actividades de  habilidad y  destreza.	1	42509	A	A		A	578	1068	Educación Física	44

    End Function
    Function crearListaAlumnos(ByVal dt As System.Data.DataTable) As List(Of personaLibreta)
        Dim opersonaLibreta As personaLibreta
        Dim codTempAlumno As String = ""
        Dim lstPersonaLibreta As New List(Of personaLibreta)
        For Each fila As System.Data.DataRow In dt.Rows
            opersonaLibreta = New personaLibreta
            opersonaLibreta.codAlumno = fila("AL_CodigoAlumno").ToString()
            opersonaLibreta.nombreAlumno = fila("nombre").ToString()
            opersonaLibreta.conductaBimestral = fila("conductaBimestral").ToString()


            If opersonaLibreta.codAlumno <> codTempAlumno Then
                lstPersonaLibreta.Add(opersonaLibreta)
            End If
            codTempAlumno = opersonaLibreta.codAlumno
        Next


        Return lstPersonaLibreta

    End Function
    Public Function crearListaLibretaIndicador(ByVal dt As System.Data.DataTable) As List(Of libretaIndicador)
        Dim lstLibretaIndicador As New List(Of libretaIndicador)
        Dim olibretaIndicador As libretaIndicador
        Dim idTempIndicador As String = ""


        For Each fila As System.Data.DataRow In dt.Rows
            olibretaIndicador = New libretaIndicador

            olibretaIndicador.codComponente = fila("RNC_CodigoRegistroNotaComponente").ToString()
            olibretaIndicador.notaIndicador = fila("RNI_NotaIndicador").ToString()
            olibretaIndicador.nombreIndicador = fila("ID_Descripcion").ToString()
            olibretaIndicador.codIndicador = fila("RNI_CodigoRegistroNotaIndicador").ToString()
            olibretaIndicador.size = CInt(fila("size").ToString())

            If olibretaIndicador.codIndicador <> idTempIndicador Then
                lstLibretaIndicador.Add(olibretaIndicador)
            End If

            idTempIndicador = olibretaIndicador.codIndicador

        Next
        Return lstLibretaIndicador
    End Function




#End Region


    '' region para la  impresion de libretas de inicial

#Region "Crear libreta inicial "
    ''
    ''
    ''ahora 

    Public Function crearLibretaInicial1(ByVal codSalon As Integer, ByVal CodBimestre As Integer, ByVal codsAlumnos As String) As String
        alumnosProcesado = 0
        Dim dt_ausencias As New System.Data.DataTable
        '  Dim cantidadAlumnos As Integer = 0
        Try
            Dim abrBimestre As String = ""
            If CodBimestre = 1 Then
                abrBimestre = "I"
            End If
            If CodBimestre = 2 Then
                abrBimestre = "II"
            End If
            If CodBimestre = 3 Then
                abrBimestre = "III"
            End If
            If CodBimestre = 4 Then
                abrBimestre = "IV"
            End If


            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")


            Dim rutaLibreta As String = System.Configuration.ConfigurationManager.AppSettings("LibretaInicial").ToString()


            Dim temp As String = System.Configuration.ConfigurationManager.AppSettings("Temporales").ToString()
            Dim rutaApp As String = ""
            rutaApp = Environment.CurrentDirectory()

            File.Copy(rutaApp + rutaLibreta, rutaApp + temp & rutaTemp & ".xlsx")
            Dim nombreArchivo As String = rutaApp + temp & rutaTemp & ".xlsx"



            '    File.Copy(rutaApp & rutaPlantillas, rutaApp & rutaREpositorioTemporales)

            'Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet






            Dim dt As New System.Data.DataTable

            Dim dst As New DataSet

            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaInicial_1(codSalon, CodBimestre, codsAlumnos, 1, 1, 1, 1)
            dt = dst.Tables(0)
            dt_ausencias = dst.Tables(3)

            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)
            Dim tempCantidad As Integer = 0
            tempCantidad = lst.Count
            cantidadAlumnos = tempCantidad
            ''
            Dim contadorPaginas As Integer = 0
            Dim paginaInicialInferior As Integer = -37
            Dim paginaIinicialSuperior As Integer = 1
            Dim flagPrimero As Integer = 0
            ''
            For Each opersona As personaLibreta In lst

                paginaInicialInferior += 39
                paginaIinicialSuperior += 39

                opersona.opaginacion.pag1.inferior = paginaInicialInferior
                opersona.opaginacion.pag1.superior = paginaIinicialSuperior

                paginaInicialInferior += 39
                paginaIinicialSuperior += 39

                opersona.opaginacion.pag2.inferior = paginaInicialInferior
                opersona.opaginacion.pag2.superior = paginaIinicialSuperior
                paginaInicialInferior += 39
                paginaIinicialSuperior += 39

                opersona.opaginacion.pag3.inferior = paginaInicialInferior
                opersona.opaginacion.pag3.superior = paginaIinicialSuperior
            Next
            ''
            Dim workbook As New ClosedXML.Excel.XLWorkbook(nombreArchivo)

            workbook.CalculateMode = XLCalculateMode.Auto
            Dim fil As Integer = 8

            Dim acIndicadorDerecha As Integer = 0
            Dim acIndicadorIzquierda As Integer = 0

            Dim filDerecha As Integer = 8
            Dim nombreCursoTemp As String = ""
            Dim contadorIndicador As Integer = 0
            Dim filaCount As Integer = 0
            Dim iniciaIndicador As Integer = 0
            Dim indiceHojas As Integer = 0
            'For iii = 0 To lst.Count - 1
            '    wbkWorkbook.Sheets.Add()
            'Next

            ''variables para obtener la fila de la posicion de la fila de pintado asistencia 
            Dim posFilaDerechaLatenes As Integer = 0
            Dim tempFilasDerechaJustified As Integer = 0

            Dim espacioUsar As Integer = 0
            Dim espacioUsar1 As Integer = 0

            Dim espacioUsarD As Integer = 0
            Dim espacioUsar1D As Integer = 0

            Dim esMayorIndicadores As Boolean = False
            Dim esMayorIndicadoresD As Boolean = False


            Dim boolEstado As Boolean = True
            fil = 1
            filDerecha = 1
            Dim contadorPersonas As Integer = 0
            Dim ws = workbook.Worksheet(1)


            For indice As Integer = 1 To lst(lst.Count - 1).opaginacion.pag3.superior
                ws.Rows(indice).Height = 25
            Next
            '
            For Each opersonaLibreta As personaLibreta In lst

                contadorPersonas += 1

                filDerecha += 1

                fil += 1
                filDerecha += 1
                indiceHojas += 1
                ' Dim ws As New Worksheet


                Dim filasTemp As Integer = 0
                Dim sumasIndicador As Integer = 0
                Dim ci As Globalization.CultureInfo = New Globalization.CultureInfo("en-US")

                '---------
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 8)).Merge()
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 8)).Value = "REPORT CARD"
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 8)).Style.Font.FontSize = 20
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 8)).Style.Font.Bold = True
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 8)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                '---------
                ''
                'excel.Application.Range(ws.Cell(2, 3), ws.Cell(2, 7)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                ws.Range(ws.Cell(2, 3), ws.Cell(2, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                ''------------------------------------------------------------------------------
                fil += 1
                filDerecha += 1
                ws.Cell(fil, 3).Value = "NAME"
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = opersonaLibreta.nombreAlumno
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ''-------------------------------------------------------------------------------

                ''-------------------------------------------------------------------------------

                fil += 1
                filDerecha += 1
                ws.Cell(fil, 3).Value = "CLASS"
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                ''----------------------------------------------------
                fil += 1
                filDerecha += 1
                ws.Cell(fil, 3).Value = "TUTOR"
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = dst.Tables(2).Rows(0)("nombre").ToString()
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                ''------------------------------------------------------

                fil += 3
                filDerecha += 3
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Merge()
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = "SUBJECT AREAS -TERM " & abrBimestre
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.FontSize = 16
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.Bold = True
                ''------------------------------------------------------


                ''------------------------------------------------------
                ws.Range(ws.Cell(fil, 6), ws.Cell(fil, 7)).Merge()


                If fechaReporte.Trim <> "" Then
                    ws.Range(ws.Cell(fil, 6), ws.Cell(fil, 7)).Value = fechaReporte
                Else
                    ws.Range(ws.Cell(fil, 6), ws.Cell(fil, 7)).Value = "Date : " & Date.Now.ToString("MMMM", ci) & " , " & Now().Year().ToString()
                End If




                ws.Range(ws.Cell(fil, 6), ws.Cell(fil, 7)).Style.Font.Bold = True
                ''------------------------------------------------------

                filDerecha -= 1
                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente
                    If olibretaComponente.columna Then
                        If olibretaComponente.nombreCurso <> nombreCursoTemp Then
                            fil += 1

                            Dim cantidadInidcador As Integer = 0
                            Dim listaLibreta As IEnumerable(Of libretaComponente)
                            listaLibreta = (From h In opersonaLibreta.lstLibretaComponente Where h.nombreCurso = olibretaComponente.nombreCurso)
                            sumasIndicador = 0

                            For Each olibretaComponenteTemp As libretaComponente In listaLibreta
                                sumasIndicador += olibretaComponenteTemp.lstIndicador.Count()
                            Next
                            sumasIndicador += 1

                            Dim diferencias As Integer = 0
                            If fil >= opersonaLibreta.opaginacion.pag1.inferior And fil <= opersonaLibreta.opaginacion.pag1.superior And sumasIndicador < 40 Then
                                If fil + sumasIndicador + 1 > opersonaLibreta.opaginacion.pag1.superior Then
                                    diferencias = opersonaLibreta.opaginacion.pag1.superior - fil
                                    fil += diferencias + 2
                                End If
                            End If
                            If fil >= opersonaLibreta.opaginacion.pag2.inferior And fil <= opersonaLibreta.opaginacion.pag2.superior And sumasIndicador < 40 Then
                                If fil + sumasIndicador + 1 > opersonaLibreta.opaginacion.pag2.superior Then
                                    diferencias = opersonaLibreta.opaginacion.pag2.superior - fil
                                    fil += diferencias + 2
                                End If
                            End If
                            If fil >= opersonaLibreta.opaginacion.pag3.inferior And fil <= opersonaLibreta.opaginacion.pag3.superior And sumasIndicador < 40 Then
                                If fil + sumasIndicador + 1 > opersonaLibreta.opaginacion.pag3.superior Then
                                    diferencias = opersonaLibreta.opaginacion.pag3.superior - fil
                                    fil += diferencias + 2
                                End If
                            End If




                            If sumasIndicador > 34 Then
                                espacioUsar = (fil + 32)
                                espacioUsar1 = (fil + 32)
                                ws.Range(ws.Cell(espacioUsar, 1), ws.Cell(espacioUsar, 4)).Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                esMayorIndicadores = True
                            Else
                                ws.Range(ws.Cell((fil + sumasIndicador - 1), 1), ws.Cell(fil + sumasIndicador - 1, 3)).Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            End If
                            ''
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Merge()
                            ''excel.Application.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = olibretaComponente.nombreCurso.ToUpper()
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = olibretaComponente.nombreCurso.ToUpper()
                            '' ws.Cell(fil, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            With ws.Cell(fil, 4)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                            ws.Cell(fil, 1).Style.Font.Bold = True
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Border.TopBorder = XLBorderStyleValues.Thin
                            ''
                            ''
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.FontSize = 16
                            ws.Rows(fil).Height = 25
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Border.RightBorder = XLBorderStyleValues.Thin

                        End If

                        For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador
                            filasTemp = fil
                            fil += 1

                            If esMayorIndicadores Then
                                If fil = espacioUsar1 Then
                                    ''
                                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Merge()
                                    ''excel.Application.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = olibretaComponente.nombreCurso.ToUpper()
                                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = olibretaComponente.nombreCurso.ToUpper()
                                    With ws.Cell(fil, 4)
                                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                    End With
                                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                                    ws.Cell(fil, 1).Style.Font.Bold = True
                                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.FontSize = 16
                                    ws.Rows(fil).Height = 25
                                    ''
                                    ''
                                    fil += 1
                                    esMayorIndicadores = False
                                End If

                            End If
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Merge()
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = olibretaIndicador.nombreIndicador
                            'dev
                            ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).RowHeight = 25
                            ws.Rows(fil).Height = 25
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.FontSize = 8
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.FontName = "Arial"
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Alignment.WrapText = True
                            With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3))
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With
                            ws.Cell(fil, 4).Value = olibretaIndicador.notaIndicador.ToUpper()
                            ws.Cell(fil, 4).Style.Font.Bold = True

                            With ws.Cell(fil, 4)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With
                            ws.Cell(fil, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                            ''
                            '  ws.Cell(fil, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                            ''

                            ws.Cell(fil, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        Next
                        'ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 4)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        nombreCursoTemp = olibretaComponente.nombreCurso
                    Else
                        If olibretaComponente.nombreCurso <> nombreCursoTemp Then
                            filDerecha += 1

                            ''
                            Dim cantidadInidcador As Integer = 0
                            Dim listaLibreta As IEnumerable(Of libretaComponente)
                            listaLibreta = (From h In opersonaLibreta.lstLibretaComponente Where h.nombreCurso = olibretaComponente.nombreCurso)
                            sumasIndicador = 0
                            For Each olibretaComponenteTemp As libretaComponente In listaLibreta
                                sumasIndicador += olibretaComponenteTemp.lstIndicador.Count()
                            Next
                            ' sumasIndicador += 1
                            Dim diferencias As Integer = 0
                            If filDerecha >= opersonaLibreta.opaginacion.pag1.inferior And filDerecha <= opersonaLibreta.opaginacion.pag1.superior And sumasIndicador < 34 Then

                                If filDerecha + sumasIndicador + 1 > opersonaLibreta.opaginacion.pag1.superior Then
                                    diferencias = opersonaLibreta.opaginacion.pag1.superior - filDerecha
                                    filDerecha += diferencias + 2
                                End If
                            End If
                            If filDerecha >= opersonaLibreta.opaginacion.pag2.inferior And filDerecha <= opersonaLibreta.opaginacion.pag2.superior And sumasIndicador < 40 Then
                                If filDerecha + sumasIndicador + 1 > opersonaLibreta.opaginacion.pag2.superior Then
                                    diferencias = opersonaLibreta.opaginacion.pag2.superior - filDerecha
                                    filDerecha += diferencias + 2
                                End If
                            End If
                            If filDerecha >= opersonaLibreta.opaginacion.pag3.inferior And filDerecha <= opersonaLibreta.opaginacion.pag3.superior And sumasIndicador < 40 Then
                                If filDerecha + sumasIndicador + 1 > opersonaLibreta.opaginacion.pag3.superior Then
                                    diferencias = opersonaLibreta.opaginacion.pag3.superior - filDerecha
                                    filDerecha += diferencias + 2
                                End If
                            End If

                            ws.Range(ws.Cell((filDerecha + sumasIndicador), 5), ws.Cell(filDerecha + sumasIndicador, 8)).Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Merge()
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Value = olibretaComponente.nombreCurso.ToUpper()
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Font.FontSize = 16
                            'dev
                            '  ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).RowHeight = 25
                            '  ws.Rows(filDerecha).Height = 25
                            ''.Height = 30;
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                            'dev
                            'ws.Cell(filDerecha, 5).IndentLevel = 3
                            ws.Cell(filDerecha, 5).Style.Font.Bold = True
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Border.TopBorder = XLBorderStyleValues.Thin
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Border.RightBorder = XLBorderStyleValues.Thin
                            ' ws.Cell(filDerecha, 8).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            With ws.Cell(filDerecha, 8)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With
                            ''ws.Range(ws.Cell(fil + 5, 2), ws.Cell(fil + 5, 3)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        End If

                        For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador
                            filasTemp = filDerecha
                            filDerecha += 1

                            Dim diferenciasII As Integer = 0
                            ''-----
                            If filDerecha >= opersonaLibreta.opaginacion.pag1.inferior And filDerecha <= opersonaLibreta.opaginacion.pag1.superior Then

                                If filDerecha + 1 = opersonaLibreta.opaginacion.pag1.superior Then
                                    With ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7))
                                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                    End With

                                    diferenciasII = opersonaLibreta.opaginacion.pag1.superior - filDerecha



                                    filDerecha += diferenciasII + 2

                                    ws.Range(ws.Cell(filDerecha - 1, 5), ws.Cell(filDerecha - 1, 7)).Value = olibretaComponente.nombreCurso.ToUpper()
                                    ws.Range(ws.Cell(filDerecha - 1, 5), ws.Cell(filDerecha - 1, 7)).Merge()
                                    With ws.Range(ws.Cell(filDerecha - 1, 5), ws.Cell(filDerecha - 1, 7))
                                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                        '.Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                        .Style.Font.FontSize = 16
                                        .Style.Font.Bold = True
                                    End With
                                    ''
                                    With ws.Range(ws.Cell(filDerecha - 1 + 2, 5), ws.Cell(filDerecha - 1 + 2, 7))
                                        .Style.Border.RightBorder = XLBorderStyleValues.None
                                        .Style.Border.TopBorder = XLBorderStyleValues.None
                                        .Style.Border.BottomBorder = XLBorderStyleValues.None
                                        .Style.Border.LeftBorder = XLBorderStyleValues.None
                                        '.Style.Font.FontSize = 16
                                        '.Style.Font.Bold = True
                                    End With

                                    ''

                                    With ws.Range(ws.Cell(filDerecha - 1, 8), ws.Cell(filDerecha - 1, 8))
                                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin

                                    End With





                                End If

                            End If
                            If filDerecha >= opersonaLibreta.opaginacion.pag2.inferior And filDerecha <= opersonaLibreta.opaginacion.pag2.superior Then
                                If filDerecha + 1 = opersonaLibreta.opaginacion.pag2.superior Then
                                    With ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7))
                                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                    End With
                                    diferenciasII = opersonaLibreta.opaginacion.pag2.superior - filDerecha
                                    filDerecha += diferenciasII + 2
                                End If
                            End If
                            If filDerecha >= opersonaLibreta.opaginacion.pag3.inferior And filDerecha <= opersonaLibreta.opaginacion.pag3.superior Then
                                If filDerecha + 1 = opersonaLibreta.opaginacion.pag3.superior Then
                                    With ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7))
                                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                    End With
                                    diferenciasII = opersonaLibreta.opaginacion.pag3.superior - filDerecha
                                    filDerecha += diferenciasII + 2
                                End If
                            End If
                            ''-----

                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Merge()
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Value = olibretaIndicador.nombreIndicador
                            ws.Rows(filDerecha).Height = 25
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Font.FontSize = 8
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Font.FontName = "Arial"
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Alignment.WrapText = True
                            ws.Cell(filDerecha, 8).Value = olibretaIndicador.notaIndicador.ToUpper()
                            ws.Cell(filDerecha, 8).Style.Font.Bold = True
                            With ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7))
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With
                            With ws.Cell(filDerecha, 8)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With
                            ws.Cell(filDerecha, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            ws.Rows(filDerecha).Height = 25
                        Next
                        nombreCursoTemp = olibretaComponente.nombreCurso
                    End If
                Next








                If filDerecha > fil Then
                    filDerecha -= 2
                    ''
                    filDerecha += 4
                    ''
                    ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha, 3)).Merge()
                    ''
                    ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha, 3)).Value = "CONDUCTA"
                    'ws.Range(ws.Cell(filDerecha + 4, 1), ws.Cell(filDerecha + 4, 3)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    With ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha, 3))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With
                    ws.Cell(filDerecha, 4).Value = opersonaLibreta.conductaBimestral
                    ws.Cell(filDerecha, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ''ws.Cell(filDerecha + 4, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    ''
                    With ws.Cell(filDerecha, 4)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With
                    filDerecha += 1
                    If filDerecha + 5 = 80 Then
                        filDerecha += 3
                    End If

                    Dim temFilas As Integer = 0
                    ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha, 3)).Merge()
                    ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha, 3)).Value = "Comentario de la tutora"
                    ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha, 3)).Style.Font.Bold = True

                    temFilas = filDerecha
                    For Each olibretaComponenteT As libretaComponente In opersonaLibreta.lstLibretaComponente
                        If olibretaComponenteT.observacionCurso <> "" And olibretaComponenteT.observacionCurso.Trim().Length >= 5 Then
                            ws.Range(ws.Cell(filDerecha + 1, 1), ws.Cell(filDerecha + 5, 6)).Merge()
                            ws.Range(ws.Cell(filDerecha + 1, 1), ws.Cell(filDerecha + 5, 6)).Value = olibretaComponenteT.observacionCurso
                            '' ws.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 7, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            With ws.Range(ws.Cell(filDerecha + 1, 1), ws.Cell(filDerecha + 5, 6))
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                .Style.Font.FontSize = 12
                                .Style.Alignment.WrapText = True
                            End With

                            Exit For
                        End If
                    Next
                    filDerecha += 7


                    'If filDerecha = temFilas Then
                    '    filDerecha += 5
                    'End If
                    ''

                    'filDerecha += 2




                    ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha + 1, 4)).Merge()
                    ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha + 1, 4)).Value = "ABSENCES"
                    ''
                    ' ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha + 1, 4)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                    With ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha + 1, 4))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ws.Cell(filDerecha, 5).Value = "Justified"
                    tempFilasDerechaJustified = filDerecha
                    With ws.Cell(filDerecha, 5)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ws.Cell(filDerecha, 6).Value = "Not justified"

                    With ws.Cell(filDerecha, 6)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    With ws.Cell(filDerecha + 1, 5)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    With ws.Cell(filDerecha + 1, 6)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    ws.Range(ws.Cell(filDerecha + 2, 1), ws.Cell(filDerecha + 2, 4)).Merge()


                    ws.Range(ws.Cell(filDerecha + 2, 1), ws.Cell(filDerecha + 2, 4)).Value = "Lateness"
                    posFilaDerechaLatenes = filDerecha + 2 '' posicion de la  fila de latenes
                    ''////////////////////////////////////////////////////////



                    ''--------------------------------------------------------------
                    ''  crear cuadro de asistencias a enfermeria 
                    ''---------------------------------------------------------------------
                    ws.Range(ws.Cell(filDerecha + 3, 1), ws.Cell(filDerecha + 3, 4)).Merge() 'ws.Cell(filDerecha + 2, 4)).Merge()
                    ws.Range(ws.Cell(filDerecha + 3, 1), ws.Cell(filDerecha + 3, 4)).Value = " Number of Visits to the  School Nurse  "

                    With ws.Range(ws.Cell(filDerecha + 3, 1), ws.Cell(filDerecha + 3, 4))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        .Style.Font.FontSize = 10
                    End With
                    ''inicio  | union de las celdas de para poner  el valor de numero de visitas  |
                    ws.Range(ws.Cell(filDerecha + 3, 5), ws.Cell(filDerecha + 3, 6)).Merge()
                    With ws.Range(ws.Cell(filDerecha + 3, 5), ws.Cell(filDerecha + 3, 6))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With
                    ''|Fin|

                    filDerecha += 1


                    With ws.Range(ws.Cell(filDerecha + 2, 1), ws.Cell(filDerecha + 2, 4))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With
                    ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Merge()

                    With ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With
                    ''
                    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                        If opersonaLibreta.codAlumno = filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then
                            'ws.Cell(filDerecha + 1, 5).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                            ws.Cell(tempFilasDerechaJustified + 1, 5).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())

                            'ws.Cell(filDerecha + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            ws.Cell(tempFilasDerechaJustified + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                            'ws.Cell(filDerecha + 1, 6).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                            ws.Cell(tempFilasDerechaJustified + 1, 6).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())

                            'ws.Cell(filDerecha + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            ws.Cell(tempFilasDerechaJustified + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                            'ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())

                            With ws.Range(ws.Cell(tempFilasDerechaJustified + 2, 5), ws.Cell(tempFilasDerechaJustified + 2, 6))
                                .Merge()
                                .Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End With


                            'ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center




                            'With ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6))
                            '    .Merge()
                            'End With
                            Exit For
                        End If

                    Next ''

                    ''

                    ws.Range(ws.Cell(filDerecha + 9, 2), ws.Cell(filDerecha + 9, 3)).Merge()
                    ws.Range(ws.Cell(filDerecha + 9, 2), ws.Cell(filDerecha + 9, 3)).Style.Border.TopBorder = XLBorderStyleValues.Thin
                    ws.Range(ws.Cell(filDerecha + 9, 2), ws.Cell(filDerecha + 9, 3)).Value = "TUTORA"
                    ws.Range(ws.Cell(filDerecha + 9, 2), ws.Cell(filDerecha + 9, 3)).Style.Font.Bold = True
                    ''.Style.Font.Bold = True
                    ws.Range(ws.Cell(filDerecha + 9, 2), ws.Cell(filDerecha + 9, 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ws.Range(ws.Cell(filDerecha + 9, 5), ws.Cell(filDerecha + 9, 7)).Merge()
                    ws.Range(ws.Cell(filDerecha + 9, 5), ws.Cell(filDerecha + 9, 7)).Style.Border.TopBorder = XLBorderStyleValues.Thin
                    ws.Range(ws.Cell(filDerecha + 9, 5), ws.Cell(filDerecha + 9, 7)).Value = "PARENTS"
                    ws.Range(ws.Cell(filDerecha + 9, 5), ws.Cell(filDerecha + 9, 7)).Style.Font.Bold = True
                    ''.Style.Font.Bold = True
                    ws.Range(ws.Cell(filDerecha + 9, 5), ws.Cell(filDerecha + 9, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ws.Range(ws.Cell(filDerecha + 10, 2), ws.Cell(filDerecha + 10, 3)).Merge()
                    ws.Range(ws.Cell(filDerecha + 10, 2), ws.Cell(filDerecha + 10, 3)).Value = "AD:Archieved with Distinction / Actuación destacada"
                    ws.Range(ws.Cell(filDerecha + 11, 2), ws.Cell(filDerecha + 11, 3)).Merge()
                    ws.Range(ws.Cell(filDerecha + 11, 2), ws.Cell(filDerecha + 11, 3)).Value = "A:Archieved  / Aprobado"
                    ''
                    ws.Range(ws.Cell(filDerecha + 10, 5), ws.Cell(filDerecha + 10, 7)).Merge()
                    ws.Range(ws.Cell(filDerecha + 10, 5), ws.Cell(filDerecha + 10, 7)).Value = "B:Needs inprovement/ Bases en  Proceso / Desaprobado"
                    ws.Range(ws.Cell(filDerecha + 11, 5), ws.Cell(filDerecha + 11, 7)).Merge()
                    ws.Range(ws.Cell(filDerecha + 11, 5), ws.Cell(filDerecha + 11, 7)).Value = "Initial Stage  / Calificado Insuficiente / Desaprobado"
                    ''
                Else
                    fil -= 2
                    ws.Range(ws.Cell(fil + 4, 1), ws.Cell(fil + 4, 3)).Merge()



                    ws.Range(ws.Cell(fil + 4, 1), ws.Cell(fil + 4, 3)).Value = "CONDUCTA"


                    With ws.Range(ws.Cell(fil + 4, 1), ws.Cell(fil + 4, 3))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With
                    ws.Cell(fil + 4, 4).Value = opersonaLibreta.conductaBimestral

                    ws.Cell(fil + 4, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    With ws.Cell(fil + 4, 4)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With
                    If fil + 5 = 80 Then
                        fil += 3
                    End If
                    ws.Range(ws.Cell(fil + 5, 1), ws.Cell(fil + 5, 6)).Merge()
                    ws.Range(ws.Cell(fil + 5, 1), ws.Cell(fil + 5, 6)).Value = "Comentario de la tutora"
                    ws.Range(ws.Cell(fil + 5, 1), ws.Cell(fil + 5, 6)).Style.Font.Bold = True
                    ws.Range(ws.Cell(fil + 5, 1), ws.Cell(fil + 5, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    For Each olibretaComponenteT As libretaComponente In opersonaLibreta.lstLibretaComponente
                        If olibretaComponenteT.observacionCurso <> "" And olibretaComponenteT.observacionCurso.Trim().Length >= 5 Then
                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Merge()
                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Style.Alignment.WrapText = True
                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Merge()
                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Value = olibretaComponenteT.observacionCurso
                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Style.Font.FontSize = 12
                            With ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8))
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With
                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top


                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Style.Alignment.WrapText = True
                            'ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).
                            Exit For
                            ''
                        End If
                    Next



                    fil += 11



                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 1, 4)).Merge()

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 1, 4)).Value = "ABSENCES"


                    With ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 1, 4))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    ws.Cell(fil, 5).Value = "Justified"
                    With ws.Cell(fil, 5)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    ws.Cell(fil, 6).Value = "Not justified"

                    With ws.Cell(fil, 6)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    With ws.Cell(fil + 1, 5)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    With ws.Cell(fil + 1, 6)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With






                    ws.Range(ws.Cell(fil + 2, 1), ws.Cell(fil + 2, 4)).Merge()

                    ws.Range(ws.Cell(fil + 2, 1), ws.Cell(fil + 2, 4)).Value = "Lateness"

                    With ws.Range(ws.Cell(fil + 2, 1), ws.Cell(fil + 2, 4))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With
                    ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Merge()

                    With ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ''--------------------------------------------------------------
                    ''  crear cuadro de asistencias a enfermeria 
                    ''---------------------------------------------------------------------
                    ws.Range(ws.Cell(fil + 3, 1), ws.Cell(fil + 3, 4)).Merge()
                    ws.Range(ws.Cell(fil + 3, 1), ws.Cell(fil + 3, 4)).Value = " Number of Visits to the  School Nurse  "
                    With ws.Range(ws.Cell(fil + 3, 1), ws.Cell(fil + 3, 4))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        .Style.Font.FontSize = 10
                    End With
                    ws.Range(ws.Cell(fil + 3, 5), ws.Cell(fil + 3, 6)).Merge()
                    With ws.Range(ws.Cell(fil + 3, 5), ws.Cell(fil + 3, 6))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    ''---------------------------------------------------------------------


                    ''   ws.Range(ws.Cell(fil + 2, 1), ws.Cell(fil + 2, 4)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                    ws.Range(ws.Cell(fil + 8, 2), ws.Cell(fil + 8, 3)).Merge()
                    ws.Range(ws.Cell(fil + 8, 2), ws.Cell(fil + 8, 3)).Style.Border.TopBorder = XLBorderStyleValues.Thin
                    ws.Range(ws.Cell(fil + 8, 2), ws.Cell(fil + 8, 3)).Value = "TUTORA"
                    ws.Range(ws.Cell(fil + 8, 2), ws.Cell(fil + 8, 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ws.Range(ws.Cell(fil + 8, 5), ws.Cell(fil + 8, 7)).Merge()
                    ws.Range(ws.Cell(fil + 8, 5), ws.Cell(fil + 8, 7)).Style.Border.TopBorder = XLBorderStyleValues.Thin
                    ws.Range(ws.Cell(fil + 8, 5), ws.Cell(fil + 8, 7)).Value = "PARENTS"
                    ws.Range(ws.Cell(fil + 8, 5), ws.Cell(fil + 8, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ''
                    ws.Range(ws.Cell(fil + 9, 2), ws.Cell(fil + 9, 3)).Merge()
                    ws.Range(ws.Cell(fil + 9, 2), ws.Cell(fil + 9, 3)).Value = "AD:Archieved with Distinction / Actuación destacada"
                    ws.Range(ws.Cell(fil + 10, 2), ws.Cell(fil + 10, 3)).Merge()
                    ws.Range(ws.Cell(fil + 10, 2), ws.Cell(fil + 10, 3)).Value = "A:Archieved  / Aprobado"
                    ''
                    ws.Range(ws.Cell(fil + 9, 5), ws.Cell(fil + 9, 7)).Merge()
                    ws.Range(ws.Cell(fil + 9, 5), ws.Cell(fil + 9, 7)).Value = "B:Needs inprovement/ Bases en  Proceso / Desaprobado"
                    ws.Range(ws.Cell(fil + 10, 5), ws.Cell(fil + 10, 7)).Merge()
                    ws.Range(ws.Cell(fil + 10, 5), ws.Cell(fil + 10, 7)).Value = "Initial Stage  / Calificado Insuficiente / Desaprobado"
                    ''
                End If
                If filDerecha > fil Then
                    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                        If opersonaLibreta.codAlumno = filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then
                            If CodBimestre = 1 Then
                                ws.Cell(tempFilasDerechaJustified + 1, 5).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())

                                ws.Cell(tempFilasDerechaJustified + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                                ws.Cell(tempFilasDerechaJustified + 1, 6).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) '+ Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())

                                ws.Cell(tempFilasDerechaJustified + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(tempFilasDerechaJustified + 2, 5), ws.Cell(tempFilasDerechaJustified + 2, 6)).Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())

                                ws.Range(ws.Cell(tempFilasDerechaJustified + 2, 5), ws.Cell(tempFilasDerechaJustified + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            If CodBimestre = 2 Then
                                ws.Cell(tempFilasDerechaJustified + 1, 5).Value = Convert.ToInt32(filt("2FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())

                                ws.Cell(tempFilasDerechaJustified + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                                ws.Cell(tempFilasDerechaJustified + 1, 6).Value = Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) '+ Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())

                                ws.Cell(tempFilasDerechaJustified + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(tempFilasDerechaJustified + 2, 5), ws.Cell(tempFilasDerechaJustified + 2, 6)).Value = Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(tempFilasDerechaJustified + 2, 5), ws.Cell(tempFilasDerechaJustified + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            If CodBimestre = 3 Then
                                ws.Cell(tempFilasDerechaJustified + 1, 5).Value = Convert.ToInt32(filt("3FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("4FaltaJustificada").ToString())

                                ws.Cell(tempFilasDerechaJustified + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                                ws.Cell(tempFilasDerechaJustified + 1, 6).Value = Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) ' + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(tempFilasDerechaJustified + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                                ws.Range(ws.Cell(tempFilasDerechaJustified + 2, 5), ws.Cell(tempFilasDerechaJustified + 2, 6)).Value = Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())

                                ws.Range(ws.Cell(tempFilasDerechaJustified + 2, 5), ws.Cell(tempFilasDerechaJustified + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            If CodBimestre = 4 Then
                                ws.Cell(tempFilasDerechaJustified + 1, 5).Value = Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(tempFilasDerechaJustified + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(tempFilasDerechaJustified + 1, 6).Value = Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(tempFilasDerechaJustified + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(tempFilasDerechaJustified + 2, 5), ws.Cell(tempFilasDerechaJustified + 2, 6)).Value = Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(tempFilasDerechaJustified + 2, 5), ws.Cell(tempFilasDerechaJustified + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            Exit For
                        End If
                    Next

                    ''

                    Dim sqlBimestre1 As DataRow() = Nothing
                    Dim ContadorASistenciasIBimestre As Integer = 0
                    '   pintar celda de visitas de enfermeria 
                    sqlBimestre1 = dst.Tables(dst.Tables.Count - 1).Select("AL_CodigoAlumno=" & opersonaLibreta.codAlumno & " and codBimestre=" & CodBimestre)
                    ContadorASistenciasIBimestre = sqlBimestre1.Count
                    ws.Range(ws.Cell(tempFilasDerechaJustified + 3, 5), ws.Cell(tempFilasDerechaJustified + 3, 6)).Value = ContadorASistenciasIBimestre
                    ws.Range(ws.Cell(tempFilasDerechaJustified + 3, 5), ws.Cell(tempFilasDerechaJustified + 3, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ''
                Else


                    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                        If filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then
                            If CodBimestre = 1 Then
                                ws.Cell(fil + 1, 5).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(fil + 1, 6).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) '+ Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            If CodBimestre = 2 Then
                                ws.Cell(fil + 1, 5).Value = Convert.ToInt32(filt("2FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(fil + 1, 6).Value = Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) '+ Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Value = Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            If CodBimestre = 3 Then
                                ws.Cell(fil + 1, 5).Value = Convert.ToInt32(filt("3FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(fil + 1, 6).Value = Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) ' + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Value = Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            If CodBimestre = 4 Then
                                ws.Cell(fil + 1, 5).Value = Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(fil + 1, 6).Value = Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Value = Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If

                            Exit For
                        End If





                    Next
                    '' pintar la celda de viistas enfermeria 

                    Dim sqlBimestre1 As DataRow() = Nothing
                    Dim ContadorASistenciasIBimestre As Integer = 0
                    '   sqlBimestre=dt
                    sqlBimestre1 = dst.Tables(dst.Tables.Count - 1).Select("AL_CodigoAlumno=" & opersonaLibreta.codAlumno & " and codBimestre=" & CodBimestre)
                    ContadorASistenciasIBimestre = sqlBimestre1.Count
                    ws.Range(ws.Cell(fil + 3, 5), ws.Cell(fil + 3, 6)).Value = ContadorASistenciasIBimestre

                    ws.Range(ws.Cell(fil + 3, 5), ws.Cell(fil + 3, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ''


                End If
                With ws
                    ws.PageSetup.AdjustTo(60)
                    ws.PageSetup.Margins.Top = 0.75 '1.9
                    ws.PageSetup.Margins.Bottom = 0.75 '1.9
                    ws.PageSetup.Margins.Left = 0.7 '0.6
                    ws.PageSetup.Margins.Right = 0.7 '0.6
                    ws.PageSetup.Margins.Header = 0.3 '0.8
                    ws.PageSetup.Margins.Footer = 0.3 '0.8
                    ws.PageSetup.PagesWide = 1
                End With
                ws.Column(4).Width = 4
                ws.Column(8).Width = 4
                ws.Column(3).Width = 24
                ws.Column(7).Width = 30
                ws.Column(5).Width = 8
                ws.Column(6).Width = 13
                ws.Column(3).Width = 41




                'fil += 25
                'filDerecha += 25


                'If fil > opersonaLibreta.opaginacion.pag1.superior Then
                '    fil = opersonaLibreta.opaginacion.pag3.superior
                '    filDerecha = opersonaLibreta.opaginacion.pag3.superior
                'End If
                'If fil > opersonaLibreta.opaginacion.pag2.superior Then
                '    fil = opersonaLibreta.opaginacion.pag3.superior
                '    filDerecha = opersonaLibreta.opaginacion.pag3.superior
                'End If
                'If fil > opersonaLibreta.opaginacion.pag3.superior Then
                '    fil = opersonaLibreta.opaginacion.pag3.superior
                '    filDerecha = opersonaLibreta.opaginacion.pag3.superior
                'Else
                '    fil = opersonaLibreta.opaginacion.pag2.superior
                '    filDerecha = opersonaLibreta.opaginacion.pag2.superior
                'End If

                fil = opersonaLibreta.opaginacion.pag3.superior
                filDerecha = opersonaLibreta.opaginacion.pag3.superior
                'fil += 1
                'filDerecha += 1

                alumnosProcesado += 1
            Next
            Dim rutaExplorer As String = ""







            ''
            workbook.Save()

            ''
            Return nombreArchivo

        Catch ex As Exception

        Finally


        End Try
    End Function

    ''
    ''



#End Region


   
End Class