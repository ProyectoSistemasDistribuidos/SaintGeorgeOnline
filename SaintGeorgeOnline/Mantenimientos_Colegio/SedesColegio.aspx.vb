Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

''' <summary>
''' Modulo de Mantenimiento de Sedes Colegio
''' </summary>
''' <remarks>
''' Código del Modulo:    
''' Código de la Opción:  
''' </remarks>

Partial Class Mantenimientos_Colegio_SedesColegio
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Sedes Colegio")

            Controles.CaracteresEspeciales.agregarGrupo(ftbBuscarDescripcion, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbNombreSede, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbDireccion, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbNombreUgel, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbCodigoUgel, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbUrbanizacion, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbNumeroResolucion, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbGestion, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbGestionAbrv, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbForma, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbFormaAbrv, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbModalidad, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbModalidadAbrv, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbPrograma, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbProgramaAbrv, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbTurno, 1)
            Controles.CaracteresEspeciales.agregarGrupo(ftbTurnoAbrv, 1)

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                ViewState("SortExpression") = "NombreSede"
                ViewState("Direccion") = "ASC"
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")

                cargarComboColegios()
                cargarComboUbigeo()
                listar()

            Else ' Popup - Buscar Responsable Matricula - Direccion
                If Not Session("PersonaPopup") Is Nothing AndAlso Page.Session("ResetearPadre") = True Then
                    Dim objMaestroPersona As SaintGeorgeOnline_BusinessEntities.ModuloMatricula.be_MaestroPersonas = Session("PersonaPopup")

                    If Session("SedeColegioTipoBusqueda") = "matricula" Then ' Busqueda : Persona Responsable Matricula

                        hidenCodigoPersonaResponsableMatricula.Value = objMaestroPersona.CodigoPersona
                        tbPersonaResponsableMatricula.Text = objMaestroPersona.NombreCompleto

                    ElseIf Session("SedeColegioTipoBusqueda") = "directorGeneral" Then ' Busqueda : Persona Responsable Direccion general

                        hidenCodigoPersonaDirectorGeneral.Value = objMaestroPersona.CodigoPersona
                        tbPersonaDirectorGeneral.Text = objMaestroPersona.NombreCompleto

                    ElseIf Session("SedeColegioTipoBusqueda") = "directorNacional" Then ' Busqueda : Persona Responsable Direccion nacional

                        hidenCodigoPersonaDirectorNacional.Value = objMaestroPersona.CodigoPersona
                        tbPersonaDirectorNacional.Text = objMaestroPersona.NombreCompleto

                    ElseIf Session("SedeColegioTipoBusqueda") = "subdirector" Then ' Busqueda : Persona Responsable Direccion nacional

                        hidenCodigoPersonaSubDirector.Value = objMaestroPersona.CodigoPersona
                        tbPersonaSubDirector.Text = objMaestroPersona.NombreCompleto

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

        VerRegistro("Inserción")
        limpiarCampos()

    End Sub

    Protected Sub btnCancelar_Click()

        miTab1.Enabled = True
        miTab2.Enabled = False
        lbTab2.Text = "Inserción"
        TabContainer1.ActiveTabIndex = 0
        tbBuscarDescripcion.Focus()
        hd_Codigo.Value = 0

    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try
    End Sub

    Protected Sub btnEliminarSubDirector_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminarSubDirector.Click
        tbPersonaSubDirector.Text = ""
        hidenCodigoPersonaSubDirector.Value = 0
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Dim usp_mensaje As String = ""
            If validar(usp_mensaje) Then
                Grabar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
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

    Protected Sub ddlDepartamento_SelectedIndexChanged()
        Try
            limpiarCombos(ddlDistrito)
            cargarComboProvincia()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlProvincia_SelectedIndexChanged()
        Try
            cargarComboDistrito()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub


#End Region

#Region "Metodos"

    ''' <summary>
    ''' Setea las acciones de acceso del usuario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
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
    ''' Fecha de Creación:     11/02/2011
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
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        tbBuscarDescripcion.Text = ""
        tbBuscarDescripcion.Focus()

    End Sub

    ''' <summary>
    ''' Exporta los datos del gridView en formato WORD,EXCEL,HTML,PDF,HTML.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
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

        Dim str_TituloCabeceraArchivo As String = ""
        str_TituloCabeceraArchivo = "Sedes Colegios"

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        Dim dt As DataTable = New DataTable("ListaExportar")

        dt = Datos.agregarColumna(dt, "N°", "String")
        dt = Datos.agregarColumna(dt, "NombreSede", "String")
        dt = Datos.agregarColumna(dt, "NombreColegio", "String")
        dt = Datos.agregarColumna(dt, "NombrePersonaDirectorGeneral", "String")
        dt = Datos.agregarColumna(dt, "NombrePersonaDirectorNacional", "String")
        dt = Datos.agregarColumna(dt, "NombrePersonaResponsableMatricula", "String")
        dt = Datos.agregarColumna(dt, "NombreUgel", "String")
        dt = Datos.agregarColumna(dt, "NumeroResolucion", "String")
        dt = Datos.agregarColumna(dt, "Estado", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("NombreSede") = dr.Item("NombreSede").ToString
            auxDR.Item("NombreColegio") = dr.Item("NombreColegio").ToString
            auxDR.Item("NombrePersonaDirectorGeneral") = dr.Item("NombrePersonaDirectorGeneral").ToString
            auxDR.Item("NombrePersonaDirectorNacional") = dr.Item("NombrePersonaDirectorNacional").ToString
            auxDR.Item("NombrePersonaResponsableMatricula") = dr.Item("NombrePersonaResponsableMatricula").ToString
            auxDR.Item("NombreUgel") = dr.Item("NombreUgel").ToString
            auxDR.Item("NumeroResolucion") = dr.Item("NumeroResolucion").ToString
            auxDR.Item("Estado") = dr.Item("Estado").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next

        If rbExportar.SelectedValue = 0 Then 'WORD
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, str_TituloCabeceraArchivo)
            reporte_html = Arreglo_Datos(0)
            NombreArchivo = Arreglo_Datos(1)
            NombreArchivo = NombreArchivo & ".doc"

            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Mantenimientos_Colegio", "\Reportes\")


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

            NombreArchivo = Exportacion.ExportarReporte(dt, str_TituloCabeceraArchivo)
            NombreArchivo = NombreArchivo & ".xls"
            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Mantenimientos_Colegio", "\Reportes\")

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

            m = Exportacion.ExportarReporte_Pdf(dt, str_TituloCabeceraArchivo)

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

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, str_TituloCabeceraArchivo)
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
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String)

        miTab1.Enabled = False
        miTab2.Enabled = True
        lbTab2.Text = str_Modo
        TabContainer1.ActiveTabIndex = 1
        tbNombreSede.Focus()

    End Sub

    ''' <summary>
    ''' Valida el campo de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If tbNombreSede.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Nombre Sede")
            result = False
        End If

        If tbNombreSede.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Nombre Sede")
            result = False
        End If

        If ddlColegio.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Colegio")
            result = False
        End If

        If ddlDepartamento.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Departamento")
            result = False
        End If
        If ddlProvincia.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Provincia")
            result = False
        End If
        If ddlDistrito.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Distrito")
            result = False
        End If

        If tbPersonaResponsableMatricula.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Responsable de Matrícula")
            result = False
        End If

        If tbPersonaDirectorGeneral.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Director General")
            result = False
        End If

        If tbPersonaDirectorNacional.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Director Nacional")
            result = False
        End If

        If tbDireccion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Dirección")
            result = False
        End If

        If tbNombreUgel.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Nombre Ugel")
            result = False
        End If

        If tbCodigoUgel.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Código Ugel")
            result = False
        End If

        If tbNumeroUgel.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Número Ugel")
            result = False
        End If

        If tbUrbanizacion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Urbanización")
            result = False
        End If

        If tbNumeroResolucion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Número Resolución")
            result = False
        End If

        If tbGestion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Gestión")
            result = False
        End If

        If tbForma.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Forma")
            result = False
        End If

        If tbModalidad.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Modalidad")
            result = False
        End If

        If tbPrograma.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Programa")
            result = False
        End If

        If tbTurno.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Turno")
            result = False
        End If

        If tbGestionAbrv.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Abreviatura de Gestión")
            result = False
        End If

        If tbFormaAbrv.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Abreviatura de Forma")
            result = False
        End If

        If tbModalidadAbrv.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Abreviatura de Modalidad")
            result = False
        End If

        If tbProgramaAbrv.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Abreviatura de Programa")
            result = False
        End If

        If tbTurnoAbrv.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Abreviatura de Turno")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Limpia los campos de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()

        hd_Codigo.Value = 0
        hidenCodigoPersonaResponsableMatricula.Value = 0
        hidenCodigoPersonaDirectorGeneral.Value = 0
        hidenCodigoPersonaDirectorNacional.Value = 0
        hidenCodigoPersonaSubDirector.Value = 0

        tbNombreSede.Text = "" 
        ddlColegio.SelectedValue = 0
        ddlDepartamento.SelectedValue = 0
        ddlProvincia.SelectedValue = 0
        ddlDistrito.SelectedValue = 0
        tbPersonaResponsableMatricula.Text = ""
        tbPersonaDirectorGeneral.Text = ""
        tbPersonaDirectorNacional.Text = ""
        tbPersonaSubDirector.Text = ""
        tbDireccion.Text = ""
        tbNombreUgel.Text = ""
        tbCodigoUgel.Text = ""
        tbNumeroUgel.Text = ""
        tbUrbanizacion.Text = ""
        tbNumeroResolucion.Text = ""
        tbGestion.Text = ""
        tbForma.Text = ""
        tbModalidad.Text = ""
        tbPrograma.Text = ""
        tbTurno.Text = ""
        tbGestionAbrv.Text = ""
        tbFormaAbrv.Text = ""
        tbModalidadAbrv.Text = ""
        tbProgramaAbrv.Text = ""
        tbTurnoAbrv.Text = ""

    End Sub

    ''' <summary>
    ''' Carga el combo Ubigeo Departamento
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDepartamentos()

        Dim obj_BL_Ubigeo As New SaintGeorgeOnline_BusinessLogic.ModuloMatricula.bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Departamentos(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlDepartamento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Ubigeo Provincia, filtrando por Departamento
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboProvincia()

        Dim obj_BL_Ubigeo As New SaintGeorgeOnline_BusinessLogic.ModuloMatricula.bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlDepartamento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlProvincia, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Ubigeo Distrito, filtrando por Departamento y Provincia
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDistrito()

        Dim obj_BL_Ubigeo As New SaintGeorgeOnline_BusinessLogic.ModuloMatricula.bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlDepartamento.SelectedValue, ddlProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlDistrito, ds_Lista, "Codigo", "Descripcion", False, True)


    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Situación Laboral disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboColegios()

        Dim obj_BL_Colegios As New bl_Colegios
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Colegios.FUN_LIS_Colegios("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlColegio, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga y limpia los combos dependientes del Ubigeo Departamento
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboUbigeo()

        cargarComboDepartamentos()
        limpiarCombos(ddlProvincia)
        limpiarCombos(ddlDistrito)


    End Sub

    ''' <summary>
    ''' Elimina los elementos de la lista
    ''' </summary>
    ''' <param name="combo">Nombre del combobox</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)

        Controles.limpiarCombo(combo, False, True)

    End Sub

    ''' <summary>
    ''' Limpia los elementos de las listas de ubigeo (Departamento, Provincia y Distrito)
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombosUbigeo()

        limpiarCombos(ddlDepartamento)
        limpiarCombos(ddlProvincia)
        limpiarCombos(ddlDistrito)


    End Sub

    ''' <summary>
    ''' Lista los datos      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
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

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting()
        End If

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet
        Dim str_Descripcion As String = tbBuscarDescripcion.Text.Trim()
        Dim int_Estado As Integer = 1 'Activo
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_SedesColegio As New bl_SedesColegio
            ds_Lista = obj_BL_SedesColegio.FUN_LIS_SedesColegio(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_SedesColegio As New bl_SedesColegio
                ds_Lista = obj_BL_SedesColegio.FUN_LIS_SedesColegio(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
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
    ''' <param name="int_Codigo">Código de diágnostico</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As Integer)

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_GET_SedesColegio(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("Codigo").ToString)
        hidenCodigoPersonaResponsableMatricula.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaResponsableMatricula").ToString)
        hidenCodigoPersonaDirectorGeneral.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaDirectorGeneral").ToString)
        hidenCodigoPersonaDirectorNacional.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaDirectorNacional").ToString)
        hidenCodigoPersonaSubDirector.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaSubDirector").ToString)
        tbNombreSede.Text = ds_Lista.Tables(0).Rows(0).Item("NombreSede")
        ddlColegio.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoColegio").ToString)
        ddlDepartamento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDepartamento").ToString
        cargarComboProvincia()
        ddlProvincia.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoProvincia").ToString
        cargarComboDistrito()
        ddlDistrito.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDistrito").ToString
        tbPersonaResponsableMatricula.Text = ds_Lista.Tables(0).Rows(0).Item("NombrePersonaResponsableMatricula")
        tbPersonaDirectorGeneral.Text = ds_Lista.Tables(0).Rows(0).Item("NombrePersonaDirectorGeneral")
        tbPersonaDirectorNacional.Text = ds_Lista.Tables(0).Rows(0).Item("NombrePersonaDirectorNacional")
        tbPersonaSubDirector.Text = ds_Lista.Tables(0).Rows(0).Item("NombrePersonaSubDirector")
        tbDireccion.Text = ds_Lista.Tables(0).Rows(0).Item("Direccion")
        tbNombreUgel.Text = ds_Lista.Tables(0).Rows(0).Item("NombreUgel")
        tbCodigoUgel.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoUgel")
        tbNumeroUgel.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroUgel")
        tbUrbanizacion.Text = ds_Lista.Tables(0).Rows(0).Item("Urbanizacion")
        tbNumeroResolucion.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroResolucion")
        tbGestion.Text = ds_Lista.Tables(0).Rows(0).Item("Gestion")
        tbForma.Text = ds_Lista.Tables(0).Rows(0).Item("Forma")
        tbModalidad.Text = ds_Lista.Tables(0).Rows(0).Item("Modalidad")
        tbPrograma.Text = ds_Lista.Tables(0).Rows(0).Item("Programa")
        tbTurno.Text = ds_Lista.Tables(0).Rows(0).Item("Turno")
        tbGestionAbrv.Text = ds_Lista.Tables(0).Rows(0).Item("GestionAbrv")
        tbFormaAbrv.Text = ds_Lista.Tables(0).Rows(0).Item("FormaAbrv")
        tbModalidadAbrv.Text = ds_Lista.Tables(0).Rows(0).Item("ModalidadAbrv")
        tbProgramaAbrv.Text = ds_Lista.Tables(0).Rows(0).Item("ProgramaAbrv")
        tbTurnoAbrv.Text = ds_Lista.Tables(0).Rows(0).Item("TurnoAbrv")

        VerRegistro("Actualización")

    End Sub

    ''' <summary>
    ''' Cambia el estado de la información.     
    ''' </summary>
    ''' <param name="int_Codigo">Código de diágnostico</param>
    '''  <param name="str_accion">nombre de la acción</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado


        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_SedesColegio.FUN_DEL_SedesColegio(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
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
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_SedesColegio As New be_SedesColegio
        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        obj_BE_SedesColegio.NombreSede = tbNombreSede.Text.Trim
        obj_BE_SedesColegio.CodigoColegio = ddlColegio.SelectedValue
        obj_BE_SedesColegio.CodigoUbigeo = ddlDepartamento.SelectedValue.ToString  & ddlProvincia.SelectedValue.ToString &  ddlDistrito.SelectedValue.ToString
        obj_BE_SedesColegio.CodigoPersonaResponsableMatricula = hidenCodigoPersonaResponsableMatricula.Value
        obj_BE_SedesColegio.CodigoPersonaDirectorGeneral = hidenCodigoPersonaDirectorGeneral.Value
        obj_BE_SedesColegio.CodigoPersonaDirectorNacional = hidenCodigoPersonaDirectorNacional.Value
        obj_BE_SedesColegio.CodigoPersonaSubDirector = hidenCodigoPersonaSubDirector.Value
        obj_BE_SedesColegio.Direccion = tbDireccion.Text.Trim
        obj_BE_SedesColegio.NombreUgel = tbNombreUgel.Text.Trim
        obj_BE_SedesColegio.CodigoUgel = tbCodigoUgel.Text.Trim
        obj_BE_SedesColegio.NumeroUgel = tbNumeroUgel.Text.Trim
        obj_BE_SedesColegio.Urbanizacion = tbUrbanizacion.Text.Trim
        obj_BE_SedesColegio.NumeroResolucion = tbNumeroResolucion.Text.Trim
        obj_BE_SedesColegio.Gestion = tbGestion.Text.Trim
        obj_BE_SedesColegio.Forma = tbForma.Text.Trim
        obj_BE_SedesColegio.Modalidad = tbModalidad.Text.Trim
        obj_BE_SedesColegio.Programa = tbPrograma.Text.Trim
        obj_BE_SedesColegio.Turno = tbTurno.Text.Trim
        obj_BE_SedesColegio.GestionAbrv = tbGestionAbrv.Text.Trim
        obj_BE_SedesColegio.FormaAbrv = tbFormaAbrv.Text.Trim
        obj_BE_SedesColegio.ModalidadAbrv = tbModalidadAbrv.Text.Trim
        obj_BE_SedesColegio.ProgramaAbrv = tbProgramaAbrv.Text.Trim
        obj_BE_SedesColegio.TurnoAbrv = tbTurnoAbrv.Text.Trim

        If BoolGrabar = 0 Then
            usp_valor = obj_BL_SedesColegio.FUN_INS_SedesColegio(obj_BE_SedesColegio, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Else
            obj_BE_SedesColegio.CodigoSede = CInt(BoolGrabar)
            usp_valor = obj_BL_SedesColegio.FUN_UPD_SedesColegio(obj_BE_SedesColegio, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
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
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

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
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CrearBotonesPager(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page)

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim ddlPageSelector As DropDownList = DirectCast(gvPagerRow.FindControl("ddlPageSelector"), DropDownList)
        ddlPageSelector.Items.Clear()

        For i As Integer = 1 To gridView.PageCount
            ddlPageSelector.Items.Add(i.ToString())
        Next

        ddlPageSelector.SelectedIndex = pageIndex

    End Sub

    ''' <summary>
    ''' Muestra la numeración de registros por página y cantidad total de registros del listado actual. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function InformacionPager(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page) As String

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim pageSize As Integer = gridView.PageSize
        Dim rowCount As Integer = gridView.Rows.Count

        Dim currentPageFirstRow As Integer = ((pageIndex * pageSize) + 1)
        Dim currentPageLastRow As Integer = 0
        Dim lastPageRemainder As Integer = pageCount Mod pageSize

        currentPageLastRow = currentPageFirstRow + rowCount - 1

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegs.Value)

    End Function

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Property GridViewSortDirection() As SortDirection

        Get
            If ViewState("sortDirection") Is Nothing Then
                ViewState("sortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("sortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection") = value
        End Set

    End Property

    ''' <summary>
    ''' Lista los datos de procedimientos realizados ordenados por Descripción.
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(2)

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        GridView1.DataSource = dv
        GridView1.DataBind()

    End Sub

    ''' <summary>
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting()

        Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting"), ImageButton)

        If ViewState("Direccion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
            _btnSorting.ToolTip = "Ascendente"
        End If

    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub ddlPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GridView1.PageCount Then
                Me.GridView1.PageIndex = _NumPag - 1
            Else
                Me.GridView1.PageIndex = 0
            End If

            Me.GridView1.SelectedIndex = -1

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

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

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = GridView1.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.DataItem("Estado") = "Activo" Then
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                btnActivar.Visible = False
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

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GridView1.PageIndex = e.NewPageIndex
            End If

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression

            ViewState("SortExpression") = sortExpression

            If GridViewSortDirection = SortDirection.Ascending Then
                GridViewSortDirection = SortDirection.Descending
                SortGridView(sortExpression, "DESC")
                ViewState("Direccion") = "DESC"
            Else
                GridViewSortDirection = SortDirection.Ascending
                SortGridView(sortExpression, "ASC")
                ViewState("Direccion") = "ASC"
            End If

            ImagenSorting()
        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager(GridView1, e.Row, Me)
        End If

    End Sub

#End Region

    
End Class
