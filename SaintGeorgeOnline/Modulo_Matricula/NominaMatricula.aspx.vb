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

Partial Class Modulo_Matricula_NominaMatricula
    Inherits System.Web.UI.Page
    Dim cod_Modulo As Integer = 6
    Dim cod_Opcion As Integer = 1
    Dim ds_ListaNomiMatr As DataSet
#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Nómina Matrícula")
            btnBuscar.Attributes.Add("onclick", "ShowMyModalPopup()")
            If Not Page.IsPostBack Then
                cargarCombos()
            End If
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

        If FiUpNominaSIAGE.PostedFile.ContentLength < 1 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 44, "- Archivo:")
            result = False
        End If

        Dim PesoMax As Integer = ConfigurationManager.AppSettings.Item("CantidadPesoMaximoArchivos").ToString()
        If FiUpNominaSIAGE.PostedFile.ContentLength > 0 Then
            If (FiUpNominaSIAGE.PostedFile.ContentLength > PesoMax) Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 45, "- Archivo:")
                result = False
            End If
        End If

        If ddlSede.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "- Sede:")
            result = False
        End If

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
                ExportarNomina_SIAGE()
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
        cargarComboSede()
        cargarComboAniosAcademicos()
        cargarComboAulas()
    End Sub
    Private Sub cargarComboSede()

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlSede, ds_Lista, "Codigo", "NombreSede", False, True)

    End Sub
    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlAnioAcademico1, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub
   
    Private Function GetNewName() As String
        Dim Sname As String = Convert.ToString(DateTime.Now.Ticks)
        Return Sname
    End Function
    Private Sub ExportarNomina_SIAGE()
        '***********************************************
        '               GRABAR ARCHIVO
        '***********************************************
        'Dim sNombCod As String = GetNewName()
        Dim sName As String = FiUpNominaSIAGE.FileName
        Dim sFileDir As String = ConfigurationManager.AppSettings("RutaDocumentoNominaSIAGE_Local").ToString()
        Dim sFullName As String = sFileDir & sName
        FiUpNominaSIAGE.PostedFile.SaveAs(sFullName)
        '***********************************************
        '                   EXPORTAR
        '***********************************************
        Dim int_CodSede As Integer = ddlSede.SelectedValue
        Dim int_CodAnio As Integer = ddlAnioAcademico1.SelectedValue
        'Dim str_CodNivel As String = CStr(ddlNivelesMinisterio.SelectedValue)
        Dim int_CodGrado As Integer = hiddenCodigoGrado.Value  ' CStr(ddlGradosMinisterios.SelectedValue)
        Dim int_CodAula As Integer = ddlAulas.SelectedValue  ' CStr(ddlAulasMinisterio.SelectedValue)
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim objReportes As New bl_Matricula

        ds_ListaNomiMatr = objReportes.FUN_LIS_NominaMatricula(int_CodSede, int_CodAnio, int_CodGrado, int_CodAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        If ds_ListaNomiMatr.Tables(0).Rows.Count > 0 Then
            Dim rutamadre As String = ""
            Dim downloadBytes As Byte()
            Dim contenido_exportar As String = ""
            Dim NombreArchivo As String = ""

            NombreArchivo = Exportacion.ExportarLlenarPlantillaNominaSIAGE(ds_ListaNomiMatr, sFullName)

            NombreArchivo = NombreArchivo & ".xls"
            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Matricula", "\Reportes\")

            downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

            Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()
        Else
            MsgBox("No se encontraron registros.", MsgBoxStyle.Exclamation, "Nómina Matrícula.")
        End If
    End Sub
    Private Sub Limpiar()
        ddlSede.SelectedValue = 0
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
End Class
