Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloComunicado

Imports System.Data.SqlClient
Imports System.IO

Imports System.Security.Cryptography

Partial Class Controles_ConsultaComunicados
    Inherits System.Web.UI.UserControl

    Dim cod_Modulo As Integer = 1
    Dim cod_Opcion As Integer = 1


#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Page.IsPostBack Then
               cargarDatos
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Public Sub cargarDatos()
        ObtenerMesActual()
        If ViewState("compoControl") Is Nothing Then
            Dim oenumeratorCalendario As New enumeratorCalendario(5, 8)
            oenumeratorCalendario.setAnio(CInt(Year(Date.Now)))
            lblAnio.Text = oenumeratorCalendario.CurrentAnio.ToString
            ViewState("compoControl") = oenumeratorCalendario
        End If
    End Sub

    Protected Sub btnMes01_Click()
        Try
            lblCodigoMesGV.Text = 1
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnMes02_Click()
        Try
            lblCodigoMesGV.Text = 2
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnMes03_Click()
        Try
            lblCodigoMesGV.Text = 3
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnMes04_Click()
        Try
            lblCodigoMesGV.Text = 4
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try

    End Sub

    Protected Sub btnMes05_Click()
        Try
            lblCodigoMesGV.Text = 5
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnMes06_Click()
        Try
            lblCodigoMesGV.Text = 6
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnMes07_Click()
        Try
            lblCodigoMesGV.Text = 7
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnMes08_Click()
        Try
            lblCodigoMesGV.Text = 8
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnMes09_Click()
        Try
            lblCodigoMesGV.Text = 9
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub
    Protected Sub btnMes10_Click()
        Try
            lblCodigoMesGV.Text = 10
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnMes11_Click()
        Try
            lblCodigoMesGV.Text = 11
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub


    Protected Sub btnMes12_Click()
        Try
            lblCodigoMesGV.Text = 12
            lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnAtras_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAtras.Click
        Try
            Dim codigoMesActual As Integer
            codigoMesActual = lblCodigoMesGV.Text

            If codigoMesActual > 1 And codigoMesActual <= 12 Then
                lblCodigoMesGV.Text = codigoMesActual - 1
                lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)
                listar()
            ElseIf codigoMesActual = 1 Then
                lblNombreMesGV.Text = ObtenerNombreMes(codigoMesActual)
                listar()
            End If

        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnAvanzar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAvanzar.Click
        Try
            Dim codigoMesActual As Integer
            codigoMesActual = lblCodigoMesGV.Text

            If codigoMesActual >= 1 And codigoMesActual < 12 Then
                lblCodigoMesGV.Text = codigoMesActual + 1
                lblNombreMesGV.Text = ObtenerNombreMes(lblCodigoMesGV.Text)

                listar()
            ElseIf codigoMesActual = 12 Then
                lblNombreMesGV.Text = ObtenerNombreMes(codigoMesActual)
                listar()
            End If

        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub lb_Adjunto_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ExportarAdjunto(sender)
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Método"

    ''' <summary>
    ''' Lista los datos      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     21/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listar()
        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda()
        Dim dt_Adjuntos As New DataTable

        dt_Adjuntos = ds_Lista.Tables(1)
        ViewState("Adjuntos") = dt_Adjuntos

        GVConsultaComunicado.DataSource = ds_Lista.Tables(0)
        GVConsultaComunicado.DataBind()
    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     21/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda() As DataSet
        Dim oenumeratorCalendario As enumeratorCalendario
        oenumeratorCalendario = CType(ViewState("compoControl"), enumeratorCalendario)



        Dim str_AnioAcademico As String = oenumeratorCalendario.CurrentAnio().ToString ' Obtener_CodigoPeriodoActivo()


     

        Dim int_CodigoMes As Integer = lblCodigoMesGV.Text
        Dim int_CodigoTipoPersona = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As New DataSet

        Dim obj_BL_ComunicadosAcademicos As New bl_ComunicadosAcademicos
        ds_Lista = obj_BL_ComunicadosAcademicos.FUN_LIS_ConsultaComunicadosAcademicos( _
          str_AnioAcademico, int_CodigoMes, int_CodigoTipoPersona, int_CodigoUsuario, int_CodigoTipoPersona, cod_Modulo, cod_Opcion)

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Obtiene el mes actual
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     21/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerMesActual()

        Dim int_CodigoMes As Integer = Now.Month
        lblCodigoMesGV.Text = int_CodigoMes
        lblNombreMesGV.Text = ObtenerNombreMes(int_CodigoMes)

        If int_CodigoMes = 1 Then
            btnMes01_Click()
        ElseIf int_CodigoMes = 2 Then
            btnMes02_Click()
        ElseIf int_CodigoMes = 3 Then
            btnMes03_Click()
        ElseIf int_CodigoMes = 4 Then
            btnMes04_Click()
        ElseIf int_CodigoMes = 5 Then
            btnMes05_Click()
        ElseIf int_CodigoMes = 6 Then
            btnMes06_Click()
        ElseIf int_CodigoMes = 7 Then
            btnMes07_Click()
        ElseIf int_CodigoMes = 8 Then
            btnMes08_Click()
        ElseIf int_CodigoMes = 9 Then
            btnMes09_Click()
        ElseIf int_CodigoMes = 10 Then
            btnMes10_Click()
        ElseIf int_CodigoMes = 11 Then
            btnMes11_Click()
        ElseIf int_CodigoMes = 12 Then
            btnMes12_Click()
        End If

    End Sub

    Private Function ObtenerNombreMes(ByVal int_CodigoMes As Integer) As String
        Dim str_mes As String = ""

        If int_CodigoMes = 1 Then
            str_mes = "Enero"
        ElseIf int_CodigoMes = 2 Then
            str_mes = "Febrero"
        ElseIf int_CodigoMes = 3 Then
            str_mes = "Marzo"
        ElseIf int_CodigoMes = 4 Then
            str_mes = "Abril"
        ElseIf int_CodigoMes = 5 Then
            str_mes = "Mayo"
        ElseIf int_CodigoMes = 6 Then
            str_mes = "Junio"
        ElseIf int_CodigoMes = 7 Then
            str_mes = "Julio"
        ElseIf int_CodigoMes = 8 Then
            str_mes = "Agosto"
        ElseIf int_CodigoMes = 9 Then
            str_mes = "Septiembre"
        ElseIf int_CodigoMes = 10 Then
            str_mes = "Octubre"
        ElseIf int_CodigoMes = 11 Then
            str_mes = "Noviembre"
        ElseIf int_CodigoMes = 12 Then
            str_mes = "Diciembre"
        End If

        Return str_mes
    End Function

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     21/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim str_NombreUsuario As String = ""

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(0, 8, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     21/09/2011
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

    Private Sub ExportarAdjunto(ByVal sender As Object)
        Dim rutaTemp As String = ConfigurationManager.AppSettings.Item("RutaDocumentoComunicados_Local").ToString()
        Dim lb_Adjunto As New LinkButton
        Dim lbl_CodigoComunicado As Label
        Dim gv_Contenedor As New GridView
        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""

        lb_Adjunto = sender
        lbl_CodigoComunicado = lb_Adjunto.Parent.Parent.Parent.FindControl("lblCodigoComAdj")

        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim int_codigoComunicado As Integer = lbl_CodigoComunicado.Text
        Dim str_NombreArchivo As String = lb_Adjunto.ValidationGroup
        Dim obj_OL_ComunicadosAcademicos As New bl_ComunicadosAcademicos

        NombreArchivo = str_NombreArchivo

        downloadBytes = File.ReadAllBytes(rutaTemp & int_codigoComunicado.ToString & "\" & NombreArchivo)

        obj_OL_ComunicadosAcademicos.FUN_INS_ConfirmacionLecturaComunicado(int_codigoComunicado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        'Obtiene la respuesta actual
        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

        'Borra la respuesta
        response.Clear()
        response.ClearContent()
        response.ClearHeaders()

        'Tipo de contenido para forzar la descarga
        'Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        response.Charset = ""
        response.ContentType = "binary/octet-stream"
        response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        'response.Flush()
        'Envia los bytes
        response.BinaryWrite(downloadBytes)
        response.Flush()
        response.Close()
    End Sub

#End Region

#Region "Metodos del GridView"

    Protected Sub GVConsultaComunicado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim lblCod As Label = e.Row.FindControl("lblCodigoComunicadoGrilla")
        Dim lb_Descrip As Label = e.Row.FindControl("lblDescripcion")
        Dim dl_Adjuntos As DataList = e.Row.FindControl("dlVersiones")

        Dim dv_Adjuntos As New DataView
        Dim dt_Adjuntos As New DataTable
        Dim int_Contador As Integer = 0

        If e.Row.RowType = DataControlRowType.DataRow Then

            dt_Adjuntos = ViewState("Adjuntos")
            dv_Adjuntos = dt_Adjuntos.DefaultView

            If e.Row.DataItem("CodigoDetalle") = "0" Then

                e.Row.Style.Value = "background-color:#1ac4ff"
                e.Row.ForeColor = Drawing.Color.White
                lb_Descrip.Style.Value = "padding-left:5px;"
                lb_Descrip.Font.Bold = True
                lb_Descrip.Font.Size = 11
                lb_Descrip.Font.Name = "Arial"
            Else
                lb_Descrip.Style.Value = "color:#827b7b;"
                lb_Descrip.Font.Size = 9
                lb_Descrip.Font.Name = "Arial"
                lb_Descrip.Font.Bold = True
                dv_Adjuntos.RowFilter = "1=1 AND CodigoComunicado = " & lblCod.Text
                e.Row.Cells(1).Style.Value = "padding-left:55px;"
                dl_Adjuntos.DataSource = dv_Adjuntos
                dl_Adjuntos.DataBind()

            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

#Region "Metodos Generales"

    Private Function Obtener_CodigoUsuarioLogueado() As Integer

        Dim int_CodigoUsuarioLogueado As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_CodigoUsuarioLogueado = str_ArrayDatos(0)

        Catch ex As Exception
            int_CodigoUsuarioLogueado = -1
        End Try

        Return int_CodigoUsuarioLogueado

    End Function

    Private Function Obtener_CodigoTipoUsuarioLogueado() As Integer

        Dim int_CodigoUsuarioLogueado As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_CodigoUsuarioLogueado = str_ArrayDatos(1)

        Catch ex As Exception
            int_CodigoUsuarioLogueado = -1
        End Try

        Return int_CodigoUsuarioLogueado

    End Function

    Private Function Obtener_CodigoPeriodoActivo() As Integer

        Dim int_CodigoUsuarioLogueado As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_CodigoUsuarioLogueado = str_ArrayDatos(11)

        Catch ex As Exception
            int_CodigoUsuarioLogueado = -1
        End Try

        Return int_CodigoUsuarioLogueado

    End Function

#End Region



#Region "metodos nuevos anio "
    Private Sub crearListaAnios()
        Dim lstAnios = Enumerable.Range(CInt(Year(Date.Now)) - 5, CInt(Year(Date.Now)) + 5)
        Try

        Catch ex As Exception

        End Try

    End Sub

#End Region


    Protected Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        '  ViewState("compoControl")
        Dim oenumeratorCalendario As enumeratorCalendario
        oenumeratorCalendario = CType(ViewState("compoControl"), enumeratorCalendario)
        oenumeratorCalendario.PrevItem()
        lblAnio.Text = oenumeratorCalendario.CurrentAnio
        ViewState("compoControl") = oenumeratorCalendario

        btnMes01_Click()
        btnMes02_Click()
        btnMes03_Click()
        btnMes04_Click()
        btnMes05_Click()
        btnMes06_Click()
        ''
        btnMes07_Click()
        btnMes08_Click()
        btnMes09_Click()
        btnMes10_Click()
        btnMes11_Click()
        btnMes12_Click()
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim oenumeratorCalendario As enumeratorCalendario
        oenumeratorCalendario = CType(ViewState("compoControl"), enumeratorCalendario)

        oenumeratorCalendario.NextItem()
        lblAnio.Text = oenumeratorCalendario.CurrentAnio
        ViewState("compoControl") = oenumeratorCalendario

        btnMes01_Click()
        btnMes02_Click()
        btnMes03_Click()
        btnMes04_Click()
        btnMes05_Click()
        btnMes06_Click()
        ''
        btnMes07_Click()
        btnMes08_Click()
        btnMes09_Click()
        btnMes10_Click()
        btnMes11_Click()
        btnMes12_Click()

    End Sub

    
  
End Class

#Region "Implementar enumeracion"
<Serializable()> _
Public Class enumeratorCalendario

    Private _lstAnios As List(Of Integer) = Nothing
    Private _anioActual As Integer
    Private _posDefault As Integer



    Sub New(ByVal dtAnios As DataTable, ByVal nombreCampo As String, ByVal anioInicio As Integer)

        Me._lstAnios = (From f In dtAnios.AsEnumerable() Select CInt(f(nombreCampo))).ToList

        Me._anioActual = Me._lstAnios.Where(Function(anio, index) anio = anioInicio).First()

        Me._posDefault = Me._lstAnios.FindIndex(Function(anio) anio = anioInicio)

    End Sub

    Sub New(ByVal aniosAntes As Integer, ByVal aniosDespues As Integer)
        Me._lstAnios = New List(Of Integer)
        For limIinf As Integer = CInt(Year(Date.Now)) - aniosAntes To CInt(Year(Date.Now)) + aniosDespues
            Me._lstAnios.Add(limIinf)
        Next
        Me._posDefault = 0
        Me._anioActual = Me._lstAnios(Me._posDefault)
    End Sub
    Sub New(ByVal aniosAntes As Integer, ByVal aniosDespues As Integer, ByVal posItemDefault As Integer)
        Me._lstAnios = New List(Of Integer)
        For limIinf As Integer = CInt(Year(Date.Now)) - aniosAntes To CInt(Year(Date.Now)) + aniosDespues
            Me._lstAnios.Add(limIinf)
        Next
        Me._posDefault = posItemDefault
        Me._anioActual = Me._lstAnios(Me._posDefault)
    End Sub
    Public Sub NextItem()
        If Me._posDefault < Me._lstAnios.Count - 1 Then
            Me._posDefault += 1
            Me._anioActual = Me._lstAnios(Me._posDefault)
        End If
    End Sub
    Public Sub PrevItem()
        If Me._posDefault > 0 Then
            Me._posDefault -= 1
            Me._anioActual = Me._lstAnios(Me._posDefault)

        End If
    End Sub
    Public Function CurrentAnio() As Integer
        Return Me._lstAnios(Me._posDefault)
    End Function
    Public Sub setAnio(ByVal anioActual As Integer)
        Me._posDefault = Me._lstAnios.FindIndex(Function(anio) anio = anioActual)

    End Sub


End Class

#End Region

