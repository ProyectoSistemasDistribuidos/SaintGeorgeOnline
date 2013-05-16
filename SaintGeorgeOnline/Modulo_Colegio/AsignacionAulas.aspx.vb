Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

''' <summary>
''' Modulo de Mantenimiento de Relacion Anios Academicos con Aulas
''' </summary>
''' <remarks>
''' Código del Modulo:    
''' Código de la Opción:  
''' </remarks>

Partial Class Mantenimientos_Colegio_AsignacionAulas
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Asignación de Aulas por Periodo")

            If Not Page.IsPostBack Then

                'SetearAccionesAcceso()
                'ViewState("SortExpression") = "CodGrado"
                'ViewState("Direccion") = "ASC"
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")

                cargarComboAniosAcademicos()
                cargarComboSedesColegio()
                cargarComboGrados()
                limpiarCombos(ddlAulas)
                cargarComboAulasMinisterio()
                Controles.limpiarCombo(ddlBuscarAulas, True, False)
                limpiarCombos(ddlAmbiente)

                ddlBuscarAnioAcademico.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                listar()

            Else ' Popup - Buscar Responsable Matricula - Direccion
                If Not Session("PersonaPopup") Is Nothing AndAlso Page.Session("ResetearPadre") = True Then
                    Dim objMaestroPersona As SaintGeorgeOnline_BusinessEntities.ModuloMatricula.be_MaestroPersonas = Session("PersonaPopup")

                    If Session("TutorAulaTipoBusqueda") = "tutor" Then ' Busqueda : Persona Tutor de Aula

                        hidenCodigoPersonaTutor.Value = objMaestroPersona.CodigoPersona
                        tbPersonaTutor.Text = objMaestroPersona.NombreCompleto
                        ModalPopupExtender2.Show()
                    ElseIf Session("TutorAulaTipoBusqueda") = "ResponsableActa" Then ' Busqueda : Persona Responsable de Acta

                        hidenCodigoPersonaResponsableActa.Value = objMaestroPersona.CodigoPersona
                        tbPersonaResponsableActa.Text = objMaestroPersona.NombreCompleto
                        ModalPopupExtender2.Show()
                    ElseIf Session("TutorAulaTipoBusqueda") = "ResponsableSalon" Then ' Busqueda : Persona Responsable de Salon

                        hidenCodigoPersonaResponsableSalon.Value = objMaestroPersona.CodigoPersona
                        tbPersonaResponsableSalon.Text = objMaestroPersona.NombreCompleto
                        ModalPopupExtender2.Show()
                    End If

                    Page.Session("ResetearPadre") = False

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        limpiarCampos()
        ModalPopupExtender2.Show()
    End Sub

    Protected Sub btnCancelar_Click()
        miTab1.Enabled = True
        TabContainer1.ActiveTabIndex = 0
        ddlAnioAcademico.Focus()
        hd_Codigo.Value = 0
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validar(usp_mensaje) Then
                Grabar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
                ModalPopupExtender2.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender1.Dispose()
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        limpiarFiltros()
    End Sub

    Protected Sub btnEstadoBimestre_Click()
        Try
            GrabarEstadoBimestre()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Setea las acciones de acceso del usuario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
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
    ''' Fecha de Creación:     15/02/2011
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
    ''' Limpia los filtros de busqueda.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        ddlBuscarAnioAcademico.SelectedValue = 0
        ddlBuscarSede.SelectedValue = 0
        ddlBuscarAulas.SelectedValue = 0
        ddlBuscarAnioAcademico.Focus()

    End Sub

    ''' <summary>
    ''' Exporta los datos del gridView en formato WORD,EXCEL,HTML,PDF,HTML.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Exportar()

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim stream As Stream
        Dim writer As StreamWriter
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        Dim dt As DataTable = New DataTable("ListaExportar")

        dt = Datos.agregarColumna(dt, "N°", "String")
        dt = Datos.agregarColumna(dt, "Grado", "String")
        dt = Datos.agregarColumna(dt, "Aula", "String")
        dt = Datos.agregarColumna(dt, "Aula Ministerio", "String")
        dt = Datos.agregarColumna(dt, "Año Académico", "String")
        dt = Datos.agregarColumna(dt, "Sede", "String")
        dt = Datos.agregarColumna(dt, "Capacidad", "String")
        dt = Datos.agregarColumna(dt, "Tutor", "String")
        dt = Datos.agregarColumna(dt, "Responsable de Acta", "String")
        dt = Datos.agregarColumna(dt, "Responsable de Salón", "String")
        dt = Datos.agregarColumna(dt, "Estado", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("Grado") = dr.Item("DescGrado").ToString
            auxDR.Item("Aula") = dr.Item("DescAula").ToString
            auxDR.Item("Aula Ministerio") = dr.Item("DescAulaMinisterio").ToString
            auxDR.Item("Año Académico") = dr.Item("DescAnioAcademico").ToString
            auxDR.Item("Sede") = dr.Item("DescSede").ToString
            auxDR.Item("Capacidad") = dr.Item("Capacidad").ToString
            auxDR.Item("Tutor") = dr.Item("NombrePersona").ToString
            auxDR.Item("Responsable de Acta") = dr.Item("NombrePersonaResponsableActa").ToString
            auxDR.Item("Responsable de Salón") = dr.Item("NombrePersonaResponsableSalon").ToString
            auxDR.Item("Estado") = dr.Item("Estado").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next

        If rbExportar.SelectedValue = 0 Then 'WORD
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Anios Academicos - Aula")
            reporte_html = Arreglo_Datos(0)
            NombreArchivo = Arreglo_Datos(1)
            NombreArchivo = NombreArchivo & ".doc"

            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Colegio", "\Reportes\")


            stream = File.OpenWrite(rutamadre & "\" & NombreArchivo)
            writer = New StreamWriter(stream, System.Text.Encoding.UTF8)

            Using (writer)
                writer.Write(reporte_html)
                writer.Flush()
            End Using

            writer.Close()
            downloadBytes = File.ReadAllBytes(rutamadre & "\" & NombreArchivo)

            Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            response.Clear()
            response.AddHeader("Content-Type", "binary/octet-stream")
            response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            response.Flush()
            response.BinaryWrite(downloadBytes)
            response.Flush()
            response.End()

        ElseIf rbExportar.SelectedValue = 1 Then 'EXCEL

            NombreArchivo = Exportacion.ExportarReporte(dt, "Anios Academicos - Aula")
            NombreArchivo = NombreArchivo & ".xls"
            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Colegio", "\Reportes\")

            downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

            Response.AddHeader("content-disposition", "attachment;filename=test1.xls")
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()

        ElseIf rbExportar.SelectedValue = 2 Then 'PDF
            Dim m As System.IO.MemoryStream = New System.IO.MemoryStream

            m = Exportacion.ExportarReporte_Pdf(dt, "Anios Academicos - Aula")

            'Exportar
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.OutputStream.Close()
            Response.End()

        ElseIf rbExportar.SelectedValue = 3 Then 'HTML
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Anios Academicos - Aula")
            reporte_html = Arreglo_Datos(0)
            Session("Exportaciones_RepHtml") = reporte_html
            ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresion_html();</script>", False)
        End If

    End Sub

    ''' <summary>
    ''' Habilita el TabPanel del formulario
    ''' </summary>
    ''' <param name="str_Modo">Nombre del label del tabPanel</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String)

        miTab1.Enabled = False
        TabContainer1.ActiveTabIndex = 1
        ddlAulas.Focus()

    End Sub

    ''' <summary>
    ''' Valida el campo de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlGrados.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Grados")
            result = False
        End If

        If ddlAulas.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Aula")
            result = False
        End If

        If ddlAnioAcademico.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Anio Academico")
            result = False
        End If

        If ddlSede.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Sede")
            result = False
        End If

        If tbPersonaTutor.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Tutor")
            result = False
        End If

        If tbCapacidad.Text.Trim.Length = 0 Or Val(tbCapacidad.Text.Trim) <= 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Capacidad")
            result = False
        End If

        If tbPersonaResponsableActa.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Responsable Acta")
            result = False
        End If

        If tbPersonaResponsableSalon.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Responsable Salon")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Elimina los elementos de la lista
    ''' </summary>
    ''' <param name="combo">Nombre del combobox</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     28/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)
        Controles.limpiarCombo(combo, False, True)
    End Sub

    ''' <summary>
    ''' Limpia los campos de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()

        hd_Codigo.Value = 0
        hidenCodigoPersonaTutor.Value = 0
        hidenCodigoPersonaResponsableActa.Value = 0
        hidenCodigoPersonaResponsableSalon.Value = 0

        ddlGrados.SelectedValue = 0
        ddlAulas.SelectedValue = 0
        ddlAulaMinisterio.SelectedValue = 0
        ddlSede.SelectedValue = 0
        ddlAnioAcademico.SelectedValue = 0
        cargarComboAmbientes()
        ddlAmbiente.SelectedValue = 0
        tbPersonaTutor.Text = ""
        tbCapacidad.Text = ""
        tbPersonaResponsableActa.Text = ""
        tbPersonaResponsableSalon.Text = ""

    End Sub

    ''' <summary>
    ''' Carga el combo Aulas
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAulas()

        Dim int_CodigoGrado As Integer
        int_CodigoGrado = ddlGrados.SelectedValue
        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAulas, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    Private Sub cargarComboBuscarAulas()

        Dim int_CodigoGrado As Integer
        int_CodigoGrado = ddlBuscarGrados.SelectedValue
        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAulas, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboGrados()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlGrados, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlBuscarGrados, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    Private Sub cargarComboAulasMinisterio()

        Dim obj_bl_AulasMinisterio As New bl_AulasMinisterio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_bl_AulasMinisterio.FUN_LIS_AulasMinisterio("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAulaMinisterio, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo AniosAcademicos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AniosAcademicos.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlBuscarAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Sedes
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSedesColegio()

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlSede, ds_Lista, "Codigo", "NombreSede", False, True)
        Controles.llenarCombo(ddlBuscarSede, ds_Lista, "Codigo", "NombreSede", False, True)

    End Sub

    Protected Sub ddlSede_SelectedIndexChanged()
        Try
            limpiarCombos(ddlAmbiente)
            cargarComboAmbientes()
            ModalPopupExtender2.Show()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlGrados_SelectedIndexChanged()
        Try
            limpiarCombos(ddlAulas)
            cargarComboAulas()
            ModalPopupExtender2.Show()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarGrados_SelectedIndexChanged()
        Try
            limpiarCombos(ddlBuscarAulas)
            cargarComboBuscarAulas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Carga el combo Ambientes
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     28/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAmbientes()

        Dim int_CodigoSede As Integer
        int_CodigoSede = ddlSede.SelectedValue
        Dim obj_BL_Ambientes As New bl_Ambientes
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ambientes.FUN_LIS_Ambientes(int_CodigoSede, "", 0, 0, 0, -1, -1, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAmbiente, ds_Lista, "Codigo", "NombreAmbiente", False, True)

    End Sub

    ''' <summary>
    ''' Lista los datos      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        If ds_Lista.Tables(0).Rows.Count = 0 Then
            btnExportar.Enabled = False
            rbExportar.Enabled = False
        Else
            btnExportar.Enabled = True
            rbExportar.Enabled = True

            'SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            'ImagenSorting(ViewState("SortExpression"))
        End If

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet

        Dim int_AnioAcademico As Integer = ddlBuscarAnioAcademico.SelectedValue
        Dim int_Sede As Integer = ddlBuscarSede.SelectedValue
        Dim int_Aula As Integer = ddlBuscarAulas.SelectedValue

        Dim int_Estado As Integer = 1
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
            ds_Lista = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulas(int_AnioAcademico, int_Sede, int_Aula, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
                ds_Lista = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulas(int_AnioAcademico, int_Sede, int_Aula, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Obtiene y setea los datos en el Formulario.     
    ''' </summary>
    ''' <param name="int_Codigo">Código de Relacion AnioAcademico - Aula</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As Integer)

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_GET_AsignacionAulas(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("Codigo").ToString)
        hidenCodigoPersonaTutor.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaTutor").ToString)
        hidenCodigoPersonaResponsableActa.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaResponsableActa").ToString)
        hidenCodigoPersonaResponsableSalon.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaResponsableSalon").ToString)

        ddlGrados.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoGrado").ToString
        ddlGrados_SelectedIndexChanged()
        ddlAulas.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoAula").ToString)
        ddlAulaMinisterio.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoAulaMinisterio").ToString)
        ddlAnioAcademico.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoAnioAcademico").ToString
        ddlSede.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoSede").ToString
        ddlSede_SelectedIndexChanged()
        ddlAmbiente.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoAmbiente").ToString

        tbCapacidad.Text = ds_Lista.Tables(0).Rows(0).Item("CapacidadAlumnos")
        tbPersonaTutor.Text = ds_Lista.Tables(0).Rows(0).Item("NombrePersonaTutor")
        tbPersonaResponsableActa.Text = ds_Lista.Tables(0).Rows(0).Item("NombrePersonaResponsableActa")
        tbPersonaResponsableSalon.Text = ds_Lista.Tables(0).Rows(0).Item("NombrePersonaResponsableSalon")

        ModalPopupExtender2.Show()

    End Sub

    ''' <summary>
    ''' Cambia el estado de la información.     
    ''' </summary>
    ''' <param name="int_Codigo">Código de Relacion AnioAcademico - Aula</param>
    '''  <param name="str_accion">nombre de la acción</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_AsignacionAulas.FUN_DEL_AsignacionAulas(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        listar()

    End Sub

    ''' <summary>
    ''' Graba los datos del formulario 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_AsignacionAulas As New be_AsignacionAulas
        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        obj_BE_AsignacionAulas.CodigoAulaMinisterio = ddlAulaMinisterio.SelectedValue
        obj_BE_AsignacionAulas.CodigoAula = ddlAulas.SelectedValue
        obj_BE_AsignacionAulas.CodigoAnioAcademico = ddlAnioAcademico.SelectedValue
        obj_BE_AsignacionAulas.CodigoSede = ddlSede.SelectedValue
        obj_BE_AsignacionAulas.CodigoAmbiente = ddlAmbiente.SelectedValue
        obj_BE_AsignacionAulas.CodigoPersonaTutor = hidenCodigoPersonaTutor.Value
        obj_BE_AsignacionAulas.CapacidadAlumnos = tbCapacidad.Text.Trim
        obj_BE_AsignacionAulas.CodigoPersonaResponsableActa = hidenCodigoPersonaResponsableActa.Value
        obj_BE_AsignacionAulas.CodigoPersonaResponsableSalon = hidenCodigoPersonaResponsableSalon.Value

        If BoolGrabar = 0 Then
            usp_valor = obj_BL_AsignacionAulas.FUN_INS_AsignacionAulas(obj_BE_AsignacionAulas, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Else
            obj_BE_AsignacionAulas.Codigo = CInt(BoolGrabar)
            usp_valor = obj_BL_AsignacionAulas.FUN_UPD_AsignacionAulas(obj_BE_AsignacionAulas, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnCancelar_Click()
            limpiarCampos()
            listar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

    Private Sub GrabarEstadoBimestre()


        Dim dt_Lista As New DataTable
        dt_Lista = Datos.agregarColumna(dt_Lista, "CodigoAsignacionAula", "Integer")
        dt_Lista = Datos.agregarColumna(dt_Lista, "EstBim1", "Integer")
        dt_Lista = Datos.agregarColumna(dt_Lista, "EstBim2", "Integer")
        dt_Lista = Datos.agregarColumna(dt_Lista, "EstBim3", "Integer")
        dt_Lista = Datos.agregarColumna(dt_Lista, "EstBim4", "Integer")
        dt_Lista = Datos.agregarColumna(dt_Lista, "EstCon1", "Integer")
        dt_Lista = Datos.agregarColumna(dt_Lista, "EstCon2", "Integer")
        dt_Lista = Datos.agregarColumna(dt_Lista, "EstCon3", "Integer")
        dt_Lista = Datos.agregarColumna(dt_Lista, "EstCon4", "Integer")

        Dim dr As DataRow

        For Each gvr As GridViewRow In GridView1.Rows
            dr = dt_Lista.NewRow
            dr.Item("CodigoAsignacionaula") = CType(gvr.FindControl("lblCodigoAsignacionAula"), Label).Text
            dr.Item("EstBim1") = Convert.ToInt32(CType(gvr.FindControl("chkNotaBim1"), CheckBox).Checked)
            dr.Item("EstBim2") = Convert.ToInt32(CType(gvr.FindControl("chkNotaBim2"), CheckBox).Checked)
            dr.Item("EstBim3") = Convert.ToInt32(CType(gvr.FindControl("chkNotaBim3"), CheckBox).Checked)
            dr.Item("EstBim4") = Convert.ToInt32(CType(gvr.FindControl("chkNotaBim4"), CheckBox).Checked)
            dr.Item("EstCon1") = Convert.ToInt32(CType(gvr.FindControl("chkconductaBim1"), CheckBox).Checked)
            dr.Item("EstCon2") = Convert.ToInt32(CType(gvr.FindControl("chkconductaBim2"), CheckBox).Checked)
            dr.Item("EstCon3") = Convert.ToInt32(CType(gvr.FindControl("chkconductaBim3"), CheckBox).Checked)
            dr.Item("EstCon4") = Convert.ToInt32(CType(gvr.FindControl("chkconductaBim4"), CheckBox).Checked)
            dt_Lista.Rows.Add(dr)
        Next

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado


        usp_valor = obj_BL_AsignacionAulas.FUN_UPD_CierreAulas(dt_Lista, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

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
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    'Private Sub CrearBotonesPager(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page)

    '    Dim pageIndex As Integer = gridView.PageIndex
    '    Dim pageCount As Integer = gridView.PageCount
    '    Dim ddlPageSelector As DropDownList = DirectCast(gvPagerRow.FindControl("ddlPageSelector"), DropDownList)
    '    ddlPageSelector.Items.Clear()

    '    For i As Integer = 1 To gridView.PageCount
    '        ddlPageSelector.Items.Add(i.ToString())
    '    Next

    '    ddlPageSelector.SelectedIndex = pageIndex

    'End Sub

    ''' <summary>
    ''' Muestra la numeración de registros por página y cantidad total de registros del listado actual. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    'Private Function InformacionPager(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page) As String

    '    Dim pageIndex As Integer = gridView.PageIndex
    '    Dim pageCount As Integer = gridView.PageCount
    '    Dim pageSize As Integer = gridView.PageSize
    '    Dim rowCount As Integer = gridView.Rows.Count

    '    Dim currentPageFirstRow As Integer = ((pageIndex * pageSize) + 1)
    '    Dim currentPageLastRow As Integer = 0
    '    Dim lastPageRemainder As Integer = pageCount Mod pageSize

    '    currentPageLastRow = currentPageFirstRow + rowCount - 1

    '    Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegs.Value)

    'End Function

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    'Public Property GridViewSortDirection() As SortDirection

    '    Get
    '        If ViewState("sortDirection") Is Nothing Then
    '            ViewState("sortDirection") = SortDirection.Ascending
    '        End If
    '        Return DirectCast(ViewState("sortDirection"), SortDirection)
    '    End Get
    '    Set(ByVal value As SortDirection)
    '        ViewState("sortDirection") = value
    '    End Set

    'End Property

    ''' <summary>
    ''' Lista los datos de procedimientos realizados ordenados por Descripción.
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    'Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)

    '    Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(2)

    '    hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

    '    Dim dv As New Data.DataView(ds_Lista.Tables(0))
    '    dv.Sort = sortExpression + " " + direction

    '    GridView1.DataSource = dv
    '    GridView1.DataBind()

    'End Sub

    ''' <summary>
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    'Private Sub ImagenSorting(ByVal nombreBoton As String)

    '    Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
    '    Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_CodGrado"), ImageButton)
    '    Dim _btnSorting_d2 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_DescAula"), ImageButton)

    '    If _btnSorting.ID = _btnSorting_d1.ID Then
    '        _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
    '        _btnSorting_d2.ToolTip = "Descendente"

    '    Else
    '        _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
    '        _btnSorting_d1.ToolTip = "Descendente"

    '    End If

    '    If ViewState("Direccion") = "ASC" Then
    '        _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
    '        _btnSorting.ToolTip = "Descendente"
    '    ElseIf ViewState("Direccion") = "DESC" Then
    '        _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
    '        _btnSorting.ToolTip = "Ascendente"
    '    End If

    'End Sub

#End Region

#Region "Eventos del Gridview"

    'Protected Sub ddlPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
    '        Dim _NumPag As Integer

    '        If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GridView1.PageCount Then
    '            Me.GridView1.PageIndex = _NumPag - 1
    '        Else
    '            Me.GridView1.PageIndex = 0
    '        End If

    '        Me.GridView1.SelectedIndex = -1

    '        SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
    '        ImagenSorting(ViewState("SortExpression"))
    '    Catch ex As Exception
    '        EnvioEmailError(111, ex.ToString)
    '    End Try
    'End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Or e.CommandName = "Activar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    obtener(codigo)
                ElseIf e.CommandName = "Eliminar" And row.Cells(5).Text <> "Inactivo" Then
                    int_CodigoAccion = 3
                    cambiarEstado(codigo, "Eliminar")
                ElseIf e.CommandName = "Activar" And row.Cells(5).Text <> "Activo" Then
                    int_CodigoAccion = 2
                    cambiarEstado(codigo, "Activar")
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
        Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
        Dim btnActivar As ImageButton = e.Row.FindControl("btnActivar")
        Dim btnLink As ImageButton = e.Row.FindControl("btnActivar")
        Dim btnVerFotoTutor As HtmlAnchor = e.Row.FindControl("btnLinkVerFotoTutor")
        Dim btnVerFotoRespSalon As HtmlAnchor = e.Row.FindControl("btnLinkVerFotoRespSalon")
        Dim btnVerFotoRespActa As HtmlAnchor = e.Row.FindControl("btnLinkVerFotoRespActa")

        If e.Row.RowType = DataControlRowType.Pager Then

            'Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            '_TotalPags.Text = GridView1.PageCount.ToString

            'Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            '_Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then


            Dim chkNotaBim1 As CheckBox = e.Row.FindControl("chkNotaBim1")
            Dim chkNotaBim2 As CheckBox = e.Row.FindControl("chkNotaBim2")
            Dim chkNotaBim3 As CheckBox = e.Row.FindControl("chkNotaBim3")
            Dim chkNotaBim4 As CheckBox = e.Row.FindControl("chkNotaBim4")
            Dim chkconductaBim1 As CheckBox = e.Row.FindControl("chkconductaBim1")
            Dim chkconductaBim2 As CheckBox = e.Row.FindControl("chkconductaBim2")
            Dim chkconductaBim3 As CheckBox = e.Row.FindControl("chkconductaBim3")
            Dim chkconductaBim4 As CheckBox = e.Row.FindControl("chkconductaBim4")

            chkNotaBim1.Checked = Convert.ToInt32(e.Row.DataItem("NotaBimestre1"))
            chkNotaBim2.Checked = Convert.ToInt32(e.Row.DataItem("NotaBimestre2"))
            chkNotaBim3.Checked = Convert.ToInt32(e.Row.DataItem("NotaBimestre3"))
            chkNotaBim4.Checked = Convert.ToInt32(e.Row.DataItem("NotaBimestre4"))
            chkconductaBim1.Checked = Convert.ToInt32(e.Row.DataItem("ConductaBimestre1"))
            chkconductaBim2.Checked = Convert.ToInt32(e.Row.DataItem("ConductaBimestre2"))
            chkconductaBim3.Checked = Convert.ToInt32(e.Row.DataItem("ConductaBimestre3"))
            chkconductaBim4.Checked = Convert.ToInt32(e.Row.DataItem("ConductaBimestre4"))


            If e.Row.DataItem("RutaFotoTutora").ToString = "" Then
                btnVerFotoTutor.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Admin").ToString() & "noPhotoMsg.gif"
            Else
                btnVerFotoTutor.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Admin").ToString() & e.Row.DataItem("RutaFotoTutora")
            End If

            If e.Row.DataItem("RutaFotoRespSalon").ToString = "" Then
                btnVerFotoRespSalon.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Admin").ToString() & "noPhotoMsg.gif"
            Else
                btnVerFotoRespSalon.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Admin").ToString() & e.Row.DataItem("RutaFotoRespSalon")
            End If

            If e.Row.DataItem("RutaFotoRespActa").ToString = "" Then
                btnVerFotoRespActa.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Admin").ToString() & "noPhotoMsg.gif"
            Else
                btnVerFotoRespActa.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Admin").ToString() & e.Row.DataItem("RutaFotoRespActa")
            End If

            If e.Row.DataItem("Estado") = "Activo" Then
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                btnActivar.Visible = False
                btnEliminar.Visible = True
               
            Else
                btnActivar.Attributes.Add("OnClick", "return confirm_activar();")
                btnActualizar.Visible = False
                btnEliminar.Visible = False
                e.Row.ForeColor = Drawing.Color.DarkRed

            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

    'Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
    '    Try
    '        If e.NewPageIndex >= 0 Then
    '            Me.GridView1.PageIndex = e.NewPageIndex
    '        End If

    '        SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
    '        ImagenSorting(ViewState("SortExpression"))
    '    Catch ex As Exception
    '        EnvioEmailError(111, ex.ToString)
    '    End Try
    'End Sub

    'Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
    '    Try
    '        Dim sortExpression As String = e.SortExpression

    '        ViewState("SortExpression") = sortExpression

    '        If GridViewSortDirection = SortDirection.Ascending Then
    '            GridViewSortDirection = SortDirection.Descending
    '            SortGridView(sortExpression, "DESC")
    '            ViewState("Direccion") = "DESC"
    '        Else
    '            GridViewSortDirection = SortDirection.Ascending
    '            SortGridView(sortExpression, "ASC")
    '            ViewState("Direccion") = "ASC"
    '        End If

    '        ImagenSorting(e.SortExpression)
    '    Catch ex As Exception
    '        EnvioEmailError(112, ex.ToString)
    '    End Try
    'End Sub

    'Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

    '    If e.Row.RowType = DataControlRowType.Pager Then
    '        CrearBotonesPager(GridView1, e.Row, Me)
    '    End If

    'End Sub

#End Region

  
End Class

