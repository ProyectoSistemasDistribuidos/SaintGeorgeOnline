Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Diagnostics
Imports System.Runtime.InteropServices.Marshal
Imports System.Threading
Imports System.IO
'________________

Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Web.HttpContext
Imports System.Configuration
Imports System.Web.HttpServerUtility
Imports System.Web.UI.Page
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas

'________________________

Partial Class Modulo_Matricula_SubidaNotasBimestrales
    Inherits System.Web.UI.Page
    Dim cod_Modulo As Integer = 6
    Dim cod_Opcion As Integer = 1
    Dim ds_ListaNotasBimestrales As DataSet
#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Subida de Notas Bimestrales")
            btnBuscar.Attributes.Add("onclick", "ShowMyModalPopup()")
            If Not Page.IsPostBack Then
                cargarCombos()

            End If

            If FiUpNominaSIAGE.HasFile Then
                Dim nombreArchivo As String = ""
                nombreArchivo = FiUpNominaSIAGE.FileName

                inicializarControles(nombreArchivo)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub inicializarControles(ByVal nombreArchivo As String)
        Try
            Dim matriz As String() = nombreArchivo.Split("_")

            Dim campos As String = ""
            campos = matriz(3)



            'txtNombreArchivo.Text = nombreArchivo
            Dim codgrado As String = ""
            Dim codBimestre As String = ""
            Dim codAula As String = ""
            Dim codAnio As String = ""

            codBimestre = campos.Substring(7, 1)
            codgrado = campos.Substring(8, 2)
            codAula = campos.Substring(10, 2)
            codAnio = campos.Substring(2, 4)


            Dim dtCodigos As New System.Data.DataTable
            Dim Obl_rep_libretaNotas As New bl_rep_libretaNotas


            Dim dcPAram As New Dictionary(Of String, Object)
            dcPAram.Add("codAulaministerio", CInt(codAula))
            dcPAram.Add("GDM_CodigoGradoMinisterio", CInt(codgrado))
            dcPAram.Add("AC_Descripcion", CInt(codAnio))
            dcPAram.Add("codBimestre", CInt(codBimestre))

            Session("nombreArchivo") = FiUpNominaSIAGE



            Dim dst As New DataSet
            Dim nParam As String = "USP_lisCodigos"


            dtCodigos = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dcPAram, nParam).Tables(0)


            'Dim int_CodGrado As Integer = hiddenCodigoGrado.Value  ' CStr(ddlGradosMinisterios.SelectedValue)
            'Dim int_CodAula As Integer = ddlAulas.SelectedValue  ' CStr(ddlAulasMinisterio.SelectedValue)
            'Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            'Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado



            Dim AC_CodigoAnioAcademico As Integer = dtCodigos.Rows(0)("cdo")
            Dim AAP_CodigoAsignacionAula As Integer = dtCodigos.Rows(1)("cdo")
            Dim GD_CodigoGrado As Integer = dtCodigos.Rows(2)("cdo")
            Dim codBimestreSet As Integer = dtCodigos.Rows(3)("cdo")

            ddlAnioAcademico1.SelectedValue = AC_CodigoAnioAcademico
            ddlBimestre.SelectedValue = codBimestreSet
            hiddenCodigoGrado.Value = GD_CodigoGrado
            ddlAulas.SelectedValue = AAP_CodigoAsignacionAula

            ddlAulas_SelectedIndexChanged()

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Valida los campos del formulario de AgregarDocumento antes de proceder a "Grabar"
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Edga Chang
    ''' Fecha de Creación:    13/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarDocumento(ByRef str_Mensaje As String) As Boolean
        Dim result As Boolean = True
        Dim str_alertas As String = ""
        If Session("nombreArchivo") Is Nothing Then
            Exit Function
        End If
        If CType(Session("nombreArchivo"), FileUpload).PostedFile.ContentLength < 1 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 44, "- Archivo:")
            result = False
        End If

        Dim PesoMax As Integer = ConfigurationManager.AppSettings.Item("CantidadPesoMaximoArchivos").ToString()
        If CType(Session("nombreArchivo"), FileUpload).PostedFile.ContentLength > 0 Then
            If (CType(Session("nombreArchivo"), FileUpload).PostedFile.ContentLength > PesoMax) Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 45, "- Archivo:")
                result = False
            End If
        End If

        'If ddlSede.SelectedValue = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "- Sede:")
        '    result = False
        'End If

        If ddlAnioAcademico1.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "- Año Académico:")
            result = False
        End If

        If ddlAulas.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "- Aula:")
            result = False
        End If


        str_Mensaje = str_alertas
        Return result

    End Function
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim str_Mensaje As String = ""
            If validarDocumento(str_Mensaje) Then
                ExportarNotasBimestrales_SIAGE()
            Else
                Master.MostrarMensajeAlert(str_Mensaje)
            End If
        Catch ex As Exception
            Master.MostrarMensajeAlert("Error al Buscar.")
        End Try
    End Sub
    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Limpiar()
    End Sub

    Protected Sub ddlAulas_SelectedIndexChanged()
        Try
            Dim int_CodigoAula As Integer = ddlAulas.SelectedValue

            hiddenCodigoGrado.Value = 0
            Dim int_CodigoAnioAcademico As Integer = Master.Obtener_CodigoPeriodoEscolar
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

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region
#Region "Métodos"

    ''' <summary>
    ''' Llena el combo "ddlAulas" con la lista de grados activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAulas()

        Dim int_Estado As Integer = 1
        Dim str_Descripcion As String = ""
        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        ViewState("ListaAulas") = ds_Lista.Tables(0)
        Controles.llenarCombo(ddlAulas, ds_Lista, "Codigo", "DescAulaCompuesta2", False, True)

    End Sub

    Private Sub cargarCombos()
        'cargarComboSede()
        cargarComboAniosAcademicos()
        cargarComboAulas()
        cargarComboBimestre()
    End Sub
    'Private Sub cargarComboSede()

    '    Dim obj_BL_SedesColegio As New bl_SedesColegio
    '    Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
    '    Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
    '    Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
    '    Controles.llenarCombo(ddlSede, ds_Lista, "Codigo", "NombreSede", False, True)

    'End Sub

    ''' <summary>
    ''' Carga la información de los Bimestres,con la lista de grados activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     19/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboBimestre()

        Dim obj_BL_Bimestres As New bl_Bimestres
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Bimestres.FUN_LIS_Bimestres("", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlBimestre, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlAnioAcademico1, ds_Lista, "Codigo", "Descripcion", False, True)
        ddlAnioAcademico1.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
    End Sub


    Private Sub ExportarNotasBimestrales_SIAGE()
        '***********************************************
        '               GRABAR ARCHIVO
        '***********************************************
        'Dim sNombCod As String = GetNewName()
        Dim sName As String = FiUpNominaSIAGE.FileName
        Dim sFileDir As String = ConfigurationManager.AppSettings("RutaDocumentoNotasBimestralesSIAGIE_Local").ToString() 'RutaDocumentoNominaSIAGE_Local
        Dim sFullName As String = sFileDir & CType(Session("nombreArchivo"), FileUpload).FileName

        Dim fileUi As FileUpload = CType(Session("nombreArchivo"), FileUpload)

        ' Exit Sub

        fileUi.PostedFile.SaveAs(sFullName)
        '***********************************************
        '                   EXPORTAR
        '***********************************************
        Dim int_CodBimestre As Integer = ddlBimestre.SelectedValue
        Dim int_CodAnio As Integer = ddlAnioAcademico1.SelectedValue
        'Dim str_CodNivel As String = CStr(ddlNivelesMinisterio.SelectedValue)
        Dim int_CodGrado As Integer = hiddenCodigoGrado.Value  ' CStr(ddlGradosMinisterios.SelectedValue)
        Dim int_CodAula As Integer = ddlAulas.SelectedValue  ' CStr(ddlAulasMinisterio.SelectedValue)
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim objReportes As New bl_Matricula
        'ds_ListaNomiMatr = objReportes.FUN_LIS_NominaMatricula(1, int_CodAnio, int_CodGrado, int_CodAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ds_ListaNotasBimestrales = objReportes.FUN_LIS_NotasPlantillaSIAGIE(int_CodAnio, int_CodGrado, int_CodAula, int_CodBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        If ds_ListaNotasBimestrales.Tables(1).Rows.Count > 0 Then
            Dim rutamadre As String = ""
            Dim downloadBytes As Byte()
            Dim contenido_exportar As String = ""
            Dim NombreArchivo As String = ""

            'NombreArchivo = Exportacion.ExportarLlenarPlantillaNominaSIAGE(ds_ListaNomiMatr, sFullName)
            NombreArchivo = ExportarLlenarPlantillaNotasBimestralesSIAGE(ds_ListaNotasBimestrales, sFullName)

            'NombreArchivo = NombreArchivo & ".xls"
            'rutamadre = Server.MapPath(".")
            '   rutamadre = rutamadre.Replace("\Modulo_Matricula", "\Reportes\")

            downloadBytes = File.ReadAllBytes(NombreArchivo)

            Response.AddHeader("content-disposition", "attachment;filename=" & CType(Session("nombreArchivo"), FileUpload).FileName)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            '  Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()
        Else
            MsgBox("No se encontraron registros.", MsgBoxStyle.Exclamation, "Notas de Matrícula.")
        End If
    End Sub
    Private Sub Limpiar()
        'ddlSede.SelectedValue = 0
        ddlAnioAcademico1.SelectedValue = 0
    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     12/04/2012
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
    ''' Fecha de Creación:     12/04/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub
#End Region

#Region "Exportacion"
#Region "Atributos"

    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

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

    Private Shared int_HA_Left As Integer = 2 ' Alineación Horizontal Izquierda
    Private Shared int_HA_Center As Integer = 3 ' Alineación Horizontal Centrada
    Private Shared int_HA_Right As Integer = 4 ' Alineación Horizontal Derecha

    Private Shared int_VA_Top As Integer = 1 ' Alineación Vertical Superior
    Private Shared int_VA_Middle As Integer = 2 ' Alineación Vertical Media
    Private Shared int_VA_Bottom As Integer = 3 ' Alineación Vertical Inferior

#End Region



#Region "Metodos Excel"

    Public Shared Function ExportarLlenarPlantillaNotasBimestralesSIAGE(ByVal ds_Consulta As System.Data.DataSet, ByVal sFullName As String) As String
        Try

       
            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
            Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim oCells As Microsoft.Office.Interop.Excel.Range
            Dim sFile As String, sTemplate As String
            Dim nombreRep As String
            Dim dt_ListadoCursosOficiales As DataTable
            Dim dt_ListadoNotasCursosOficialesPorAlumnoSIAGE As DataTable
            Dim str_AbreviaturaMinisterio As String = ""
            Dim Fila As String = ""

            dt_ListadoCursosOficiales = ds_Consulta.Tables(0)
            dt_ListadoNotasCursosOficialesPorAlumnoSIAGE = ds_Consulta.Tables(1)
            nombreRep = GetNewName()
            sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
            sTemplate = sFullName

            oExcel.Visible = False : oExcel.DisplayAlerts = False

            ''Start a new workbook 
            oBooks = oExcel.Workbooks
            oBooks.Open(sTemplate) 'Load colorful template with graph
            oBook = oBooks.Item(1)
            oSheets = oBook.Worksheets

            '**************************************************************
            '           HojaCalculo: ESTUDIANTES MATRICULADOS
            '**************************************************************
            Dim int_NumHojaActiva As Integer = 2
            Dim int_cantidadDescriptores As Integer = 0
            Dim int_cantNumHojaActiva As Integer = 0


            'oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            Dim str_nombreHoja As String = ""
            Dim dt As DataTable
            dt = New DataTable("ListaExcel")
            dt = Datos.agregarColumna(dt, "Numero", "Integer")
            dt = Datos.agregarColumna(dt, "AbreviaturaMinisterio", "String")

            Dim dr As DataRow

            While int_cantNumHojaActiva <= 14
                oSheet = CType(oSheets.Item(int_NumHojaActiva), Microsoft.Office.Interop.Excel.Worksheet)
                str_nombreHoja = oSheet.Name

                dr = dt.NewRow
                dr.Item("Numero") = int_NumHojaActiva
                dr.Item("AbreviaturaMinisterio") = str_nombreHoja
                dt.Rows.Add(dr)

                int_cantNumHojaActiva = int_cantNumHojaActiva + 1
            End While

            int_cantNumHojaActiva = 0

            While int_cantNumHojaActiva <= dt_ListadoCursosOficiales.Rows.Count - 1


                str_AbreviaturaMinisterio = dt_ListadoCursosOficiales.Rows(int_cantNumHojaActiva).Item("AbrevMinisterio")
                int_cantidadDescriptores = dt_ListadoCursosOficiales.Rows(int_cantNumHojaActiva).Item("CantidadDescriptores")


                oExcel.ActiveWorkbook.Sheets(str_AbreviaturaMinisterio).Select()

                LlenarPlantillaNotasCursosOficialesPorAlumnoSIAGE(dt_ListadoNotasCursosOficialesPorAlumnoSIAGE, str_AbreviaturaMinisterio, int_cantidadDescriptores, oExcel)

                int_cantNumHojaActiva = int_cantNumHojaActiva + 1
                int_NumHojaActiva = int_NumHojaActiva + int_cantNumHojaActiva
            End While

            'LlenarPlantillaNominaSIAGE
            oBook.SaveAs(sFile)
            oBook.Close()

            'Quit Excel and thoroughly deallocate everything
            oExcel.Quit()
            'ReleaseComObject(oCells)
            'ReleaseComObject(oSheet)
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

            Return sFile
        Catch ex As Exception

        End Try
    End Function

    Private Shared Sub LlenarPlantillaNotasCursosOficialesPorAlumnoSIAGE(ByVal dt_ListadoNotasCursosOficialesPorAlumnoSIAGE As System.Data.DataTable, ByVal str_AbreviaturaMinisterio As String, ByVal int_cantidadDescriptores As Integer, _
                                                                                                                   ByVal oExcel As Microsoft.Office.Interop.Excel.Application)
        Dim Int_Fila As Integer = 2
        Dim Int_Columna As Integer = 4
        Dim Int_ContColum As Integer = 0
        Dim Int_ContFilas As Integer = 0
        Dim str_Fila As String = ""
        Dim dt As DataTable
        Dim dv As DataView
        Dim int_cont_dv As Integer = 0
        Dim str_codigoEducando As String = ""

        dt = dt_ListadoNotasCursosOficialesPorAlumnoSIAGE.Copy

        dv = dt.DefaultView

        dv.RowFilter = "1=1 and AbrevMinisterio= '" & str_AbreviaturaMinisterio & "'" '& " and AU_CodigoAula =" & lbl_codAula.Text

        If dv.Count > 0 Then

            While Int_ContFilas <= dv.Count - 1

                str_codigoEducando = oExcel.ActiveSheet.cells(Int_Fila + Int_ContFilas, 2).value
                Dim dv1 As DataView
                dv1 = dt_ListadoNotasCursosOficialesPorAlumnoSIAGE.DefaultView

                dv1.RowFilter = "1=1 and AbrevMinisterio = '" & str_AbreviaturaMinisterio & "' and CodigoEstudiante = '" & str_codigoEducando & "'"

                If dv1.Count > 0 Then

                    If str_codigoEducando.Equals(dv1.Item(0).Item("CodigoEstudiante")) Then

                        While Int_ContColum <= int_cantidadDescriptores - 1
                            oExcel.ActiveSheet.cells(Int_Fila + Int_ContFilas, Int_Columna + Int_ContColum).value = dv1.Item(0).Item("NotaFinalBimestre") ' dt_ListadoNotasCursosOficialesPorAlumnoSIAGE.Rows(Int_ContFilas).Item("NotaFinalBimestre")
                            Int_ContColum = Int_ContColum + 1
                        End While

                    End If

                End If

                Int_ContColum = 0
                Int_ContFilas = Int_ContFilas + 1

            End While

        End If

        'cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(2, 1), oExcel.Cells(1 + dt_ListadoNotasCursosOficialesPorAlumnoSIAGE.Rows.Count, 1 + dt_ListadoNotasCursosOficialesPorAlumnoSIAGE.Columns.Count)))
        oExcel.ActiveWindow.Zoom = 75
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

#End Region

#End Region
End Class
