Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports System.Data
Imports System.Data.SqlClient
Imports SaintGeorgeOnline_Utilities
Imports System.IO

''' <summary>
''' Módulo de Registro de las Atenciones en Enfermeria
''' </summary>
''' <remarks>
''' Código del Modulo:    2
''' Código de la Opción:  47
''' </remarks>


Partial Class Modulo_Matricula_FichaAlumnos
    Inherits System.Web.UI.Page

    'Actualizado  '30-05-2012 V4

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Ficha de Alumno")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                cargarCombos()
                ViewState("SortExpression") = "NombreCompleto"
                ViewState("Direccion") = "ASC"
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")

                popup_btnAgregar_NuevaEmpresa.Attributes.Add("onclick", "abrirPopupRegistroEmpresa('/SaintGeorgeOnline/Popups/agregarEmpresa.aspx');")
                'popup_btnAgregar_Retiro.Attributes.Add("onclick", "abrirPopupRegistroRetiro('/SaintGeorgeOnline/Popups/AgregarRetiro.aspx');")
            Else

                If Session("CodigoEmpresaRegistrado") IsNot Nothing Then

                    Dim int_CodigoEmpresa As Integer = Session("CodigoEmpresaRegistrado")

                    If int_CodigoEmpresa > 0 Then
                        cargarComboEmpresa()
                        ddlEmpresa.SelectedValue = int_CodigoEmpresa
                    End If

                    Session.Remove("CodigoEmpresaRegistrado")
                    Session("CodigoEmpresaRegistrado") = Nothing

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlNiveles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlNiveles.SelectedValue = 0 Then
                cargarCombos()
            Else
                limpiarCombos(ddlSubniveles)
                limpiarCombos(ddlGrados)
                limpiarCombos(ddlAulas)
                cargarComboSubNivel()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlSubniveles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlGrados)
            limpiarCombos(ddlAulas)
            cargarComboGrado()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlGrados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlAulas)
            cargarComboAulas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            verificarPais()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlDomicilioDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlDomicilioDistrito)
            cargarComboDomicilioProvincia()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlDomicilioProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarComboDomicilioDistrito()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlDistrito)
            cargarComboProvincia()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarComboDistrito()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub rbReligion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        verificarProfesaReligion()
    End Sub

    Protected Sub ddlReligion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        verificarReligionCatolica()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim usp_mensaje As String = ""
        Try
            If ValidarBusqueda(usp_mensaje) = True Then
                listar()
            Else
                'MENSAJE DE CRITERIOS MINIMOS DE BUSQUEDA
                listar()
            End If

        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCancelar_Click()
        CancelarFicha()
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        limpiarFiltros()
    End Sub
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            Dim bool_FichaCompleta As Boolean = True
            If Not validarFicha(usp_mensaje) Then
                'GrabarFicha()
                bool_FichaCompleta = False
            Else
                MostrarAlertas(usp_mensaje)
            End If

            GrabarFicha(bool_FichaCompleta)

        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If rb_TipoExportacion.SelectedIndex = 0 Then 'FICHA DE ALUMNO
            Exportacion_FichaAlumno()
        ElseIf rb_TipoExportacion.SelectedIndex = 2 Then 'Ficha de alumnos retirados
            Exportar_AlumnosRetirados()
        Else                    'FICHA UNICA DE MATRICULA
            Exportar_FichaUnicaMatricula()
        End If
    End Sub

    Protected Sub btnCerraFF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        pnl_ModalFichaFamiliares.Hide()
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub
#End Region

#Region "Métodos"

    Private Function ValidarBusqueda(ByRef str_Mensaje As String) As Boolean
        Dim existo As Boolean = True
        Dim str_Alertas As String = ""

        If ddlEstadoAlumno.SelectedValue <= 0 Then
            existo = False
            str_Alertas = Alertas.ObtenerAlerta(str_Alertas, 3, "Estado de Alumno")
        End If

        If ddlAnioAcademico1.SelectedValue <= 0 Or ddlAnioAcademico2.SelectedValue <= 0 Then
            existo = False
            str_Alertas = Alertas.ObtenerAlerta(str_Alertas, 3, "Periodo de Inicio y/o de Fin")
        Else
            If CInt(ddlAnioAcademico1.SelectedItem.Text) > CInt(ddlAnioAcademico2.SelectedItem.Text) Then
                existo = False
                str_Alertas = Alertas.ObtenerAlerta(str_Alertas, 46, "Periodo Inicial")
            End If
        End If

        str_Mensaje = str_Alertas
        Return existo
    End Function

    ''' <summary>
    ''' Limpia los campos de Busqueda
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        tbBuscarApellidoPaterno.Text = ""
        tbBuscarApellidoMaterno.Text = ""
        tbBuscarNombre.Text = ""
        ddlAnioAcademico1.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlAnioAcademico2.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlSede.SelectedValue = 0
        ddlNiveles.SelectedValue = 0
        ddlSubniveles.SelectedValue = 0
        ddlGrados.SelectedValue = 0
        ddlAulas.SelectedValue = 0
        ddlEstadoAlumno.SelectedValue = 1

    End Sub


    ''' <summary>
    ''' Limpia los campos de Busqueda
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     18/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()
        hd_AnioGrilla.Value = 0
        hd_Codigo.Value = 0
        'Situacion actual
        lblNombreCompleto.Text = ""
        lblSituacionAnio.Text = ""
        lblFormENSnGS.Text = ""
        lblHouse.Text = ""
        'Datos Personales
        lblCodigo.Text = ""
        lblUsuario.Text = ""
        tbCodigoEducando.Text = ""
        lblCodigoEducando.Text = ""
        lblApellidoPaterno.Text = ""
        tbApellidoPaterno.Text = ""
        lblApellidoMaterno.Text = ""
        tbApellidoMaterno.Text = ""
        lblNombre.Text = ""
        tbNombre.Text = ""
        rbSexo.SelectedValue = Nothing
        lblSexo.Text = ""
        lblTipoDocumento.Text = ""
        ddlTipoDocumento.SelectedValue = 0
        tbNumDocumento.Text = ""
        lblNumDocumento.Text = ""

        ' Datos de Nacimiento
        rbNacRegistrado.SelectedValue = Nothing
        lblNacRegistrado.Text = ""
        tbFechaNacimiento.Text = ""
        lblFechaNacimiento.Text = ""
        ddlPais.SelectedValue = 0
        lblPais.Text = ""
        ddlDepartamento.SelectedValue = 0
        lblDepartamento.Text = ""
        ddlProvincia.SelectedValue = 0
        lblProvincia.Text = ""
        ddlDistrito.SelectedValue = 0
        lblDistrito.Text = ""
        ddlNacionalidad1.SelectedValue = 0
        lblNacionalidad1.Text = 0
        ddlNacionalidad2.SelectedValue = 0
        lblNacionalidad2.Text = ""

        ' Datos Adicionales
        ddlLenguaMaterna1.SelectedValue = 0
        lblLenguaMaterna1.Text = ""
        ddlLenguaMaterna2.SelectedValue = 0
        lblLenguaMaterna2.Text = ""
        ddlEstadocivil.SelectedValue = 0
        lblEstadocivil.Text = ""
        tbCantidadHermanos.Text = ""
        lblCantidadHermanos.Text = ""
        tbPosicionHermanos.Text = ""
        lblPosicionHermanos.Text = ""
        tbCorreoElectronico.Text = ""
        lblCorreoElectronico.Text = ""
        tbCorreoElectronicoInstitucional.Text = ""
        lblCorreoElectronicoInstitucional.Text = ""
        tbCelular.Text = ""
        lblCelular.Text = ""
        rbReligion.SelectedValue = Nothing
        lblProfesaAlgunaReligion.Text = ""
        ddlReligion.SelectedValue = 0
        lblReligionProfesa.Text = ""
        rbBautizo.SelectedValue = Nothing
        lblBautizo.Text = ""
        tbLugarBautizo.Text = ""
        lblLugarBautizo.Text = ""
        tbAnioBautizo.Text = ""
        lblAnioBautizo.Text = ""
        rbPriComunion.SelectedValue = Nothing
        lblPriComunion.Text = ""
        tbLugarPriComunion.Text = ""
        lblLugarPriComunion.Text = ""
        tbAnioPriComunion.Text = ""
        lblAnioPriComunion.Text = ""
        rbConfirmado.SelectedValue = Nothing
        lblConfirmado.Text = ""
        tbLugarConfirmado.Text = ""
        lblLugarConfirmado.Text = ""
        tbAnioConfirmado.Text = ""
        lblConfirmado.Text = ""
        ''  Datos Familiares

        '' Datos de Domicilio

        ddlDomicilioDepartamento.SelectedValue = 0
        lblDomicilioDepartamento.Text = ""
        ddlDomicilioProvincia.SelectedValue = 0
        lblDomicilioProvincia.Text = ""
        ddlDomicilioDistrito.SelectedValue = 0
        lblDomicilioDistrito.Text = ""
        tbDomicilioUrbanizacion.Text = ""
        lblDomicilioUrbanizacion.Text = ""
        tbDomicilioDireccion.Text = ""
        lblDomicilioDireccion.Text = ""
        tbDomicilioReferencia.Text = ""
        lblDomicilioReferencia.Text = ""
        tbDomicilioTelefono.Text = ""
        lblDomicilioTelefono.Text = ""
        rbDomicilioAccesoInternet.SelectedValue = Nothing
        lblDomicilioAccesoInternet.Text = ""

        '' Datos Médicos
        tbNombreCompletoEmergencia.Text = ""
        lblNombreCompletoEmergencia.Text = ""
        tbTelfCasaEmergencia.Text = ""
        lblTelfCasaEmergencia.Text = ""
        tbTelfOficinaEmergencia.Text = ""
        lblTelfOficinaEmergencia.Text = ""
        tbTelfMovilEmergencia.Text = ""
        lblTelfMovilEmergencia.Text = ""

        ''Datos Especiales
        tbExperienciasTraumaticas.Text = ""
        lblExperienciasTraumaticas.Text = ""

        ''listas
        ViewState("ListaDiscapacidad") = Nothing

        ViewState.Remove("ListaDiscapacidad")

        ViewState("ListaFamilia") = Nothing

        ViewState.Remove("ListaFamilia")

        ViewState("ListaRetiro") = Nothing

        ViewState.Remove("ListaRetiro")

        ViewState("ListaProcedencia") = Nothing

        ViewState.Remove("ListaProcedencia")


    End Sub

    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(2, 47)

        'CONTROLES DEL FORMULARIO
        Master.BloqueoControles(btnBuscar, 1)
        Master.BloqueoControles(btnGrabar, 1)

        Master.SeteoPermisosAcciones(btnBuscar, 47)
        Master.SeteoPermisosAcciones(btnGrabar, 47)

    End Sub

    ''' <summary>
    ''' Se listaran las fichas de alumno que coincidan con los parametros de busqueda ingresados
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
        ImagenSorting(ViewState("SortExpression"))

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     01/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet

        Dim str_Codigo As String = tbBuscarCodigo.Text.Trim()
        Dim str_ApellidoPaterno As String = tbBuscarApellidoPaterno.Text.Trim()
        Dim str_ApellidoMaterno As String = tbBuscarApellidoMaterno.Text.Trim()
        Dim str_Nombre As String = tbBuscarNombre.Text.Trim()
        Dim int_EstadoAlumno As Integer = ddlEstadoAlumno.SelectedValue
        Dim int_Nivel As Integer = ddlNiveles.SelectedValue
        Dim int_Subnivel As Integer = ddlSubniveles.SelectedValue
        Dim int_Grado As Integer = ddlGrados.SelectedValue
        Dim int_Aula As Integer = ddlAulas.SelectedValue
        Dim int_PeriodoInicio As Integer = ddlAnioAcademico1.SelectedValue
        Dim int_PeriodoFin As Integer = ddlAnioAcademico2.SelectedValue
        Dim int_Sede As Integer = ddlSede.SelectedValue

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_FichaAlumno As New bl_Alumnos
            ds_Lista = obj_BL_FichaAlumno.FUN_LIS_FichaAlumno(str_Codigo, str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_EstadoAlumno, int_Nivel, int_Subnivel, int_Grado, int_Aula, int_PeriodoInicio, int_PeriodoFin, int_Sede, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_FichaAlumno As New bl_Alumnos
                ds_Lista = obj_BL_FichaAlumno.FUN_LIS_FichaAlumno(str_Codigo, str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_EstadoAlumno, int_Nivel, int_Subnivel, int_Grado, int_Aula, int_PeriodoInicio, int_PeriodoFin, int_Sede, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Carga una serie de listas desplegables
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombos()

        cargarComboSedesColegio()
        cargarComboEstadoAlumno()
        cargarComboAniosAcademicos()
        cargarComboNivel()
        cargarComboSubNivel()
        cargarComboGrado()
        cargarComboAulas()
        cargarComboTipoDocumento()
        cargarComboReligiones()
        cargarComboPais()
        cargarComboNacionalidades()
        cargarComboUbigeo()
        cargarComboIdiomas()
        cargarComboTipoDiscapaciadades()
        cargarComboEstadoCivil()
        cargarComboEmpresa()
        cargarComboColegio()
        cargarComboMotivoRetiro()

        'cargarComboParentesco()
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Sedes disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSedesColegio()

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlSede, ds_Lista, "Codigo", "NombreSede", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlAnioAcademico1, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlAnioAcademico2, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlAnioRetiro, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlAnioProcedencia, ds_Lista, "Codigo", "Descripcion", True, False)

        ddlAnioAcademico1.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlAnioAcademico2.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlAnioRetiro.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlAnioProcedencia.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Estados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboEstadoAlumno()

        Dim obj_BL_EstadoAlumno As New bl_EstadosAlumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_EstadoAlumno.FUN_LIS_EstadosAlumnos(int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlEstadoAlumno, ds_Lista, "Codigo", "Descripcion", True, False)
        ddlEstadoAlumno.SelectedValue = 1
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Estados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     24/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboMotivoRetiro()

        Dim obj_BL_MotivoRetiro As New bl_MotivoRetiro
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_MotivoRetiro.FUN_LIS_MotivoRetiro("", int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlMotivoRetiro, ds_Lista, "Codigo", "Descripcion", False, True)
        ddlMotivoRetiro.SelectedValue = 0
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Estados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     24/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboColegio()

        Dim obj_BL_Colegios As New bl_Colegio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Colegios.FUN_LIS_Colegios("", int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlColegioTraslado, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlColegioProcedencia, ds_Lista, "Codigo", "Descripcion", False, True)

        ddlColegioTraslado.SelectedValue = 0
        ddlColegioProcedencia.SelectedValue = 0
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Niveles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlNiveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de SubNiveles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSubNivel()

        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(CInt(ddlNiveles.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlSubniveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Grados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(CInt(ddlSubniveles.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlGrados, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Aulas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(CInt(ddlGrados.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlAulas, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Limpia los items de una lista desplegable
    ''' </summary>
    ''' <param name="combo">Nombre que identifica a la lista desplegable</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)

        Controles.limpiarCombo(combo, False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipo Documento disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoDocumento()

        Dim obj_BL_TipoDocIdentidad As New bl_TipoDocIdentidad
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TipoDocIdentidad.FUN_LIS_TipoDocIdentidad("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlTipoDocumento, ds_Lista, "Codigo", "Descripcion", False, True)
        'Controles.llenarCombo(ddlBuscarFamiliarTipoDocumento, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Religiones disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboReligiones()

        Dim obj_BL_Religiones As New bl_Religiones
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Religiones.FUN_LIS_Religiones("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlReligion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Nacionalidades disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNacionalidades()

        Dim obj_BL_Nacionalidades As New bl_Nacionalidades
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Nacionalidades.FUN_LIS_Nacionalidades("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlNacionalidad1, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlNacionalidad2, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Paises disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboPais()

        Dim obj_BL_Pais As New bl_Paises
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Pais.FUN_LIS_Pais("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlPais, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Desencadena metodos de cargar de combos Departamento y limpia los combos dependientes
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboUbigeo()

        cargarComboDomicilioDepartamentos()
        limpiarCombos(ddlDomicilioProvincia)
        limpiarCombos(ddlDomicilioDistrito)

        cargarComboDepartamentos()
        limpiarCombos(ddlProvincia)
        limpiarCombos(ddlDistrito)

    End Sub

    ''' <summary>
    ''' Carga el combo Departamento Domicilio
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDomicilioDepartamentos()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Departamentos(int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlDomicilioDepartamento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Idiomas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboIdiomas()

        Dim obj_BL_Idiomas As New bl_Idiomas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Idiomas.FUN_LIS_Idiomas("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlLenguaMaterna1, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlLenguaMaterna2, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipo Discapacidades disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoDiscapaciadades()

        Dim obj_BL_TipoDiscapaciadades As New bl_TiposDiscapacidades
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_TipoDiscapaciadades.FUN_LIS_TiposDiscapacidades("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlTipoDiscapacidad, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Estados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboEstadoCivil()

        Dim obj_BL_EstadosCiviles As New bl_EstadosCiviles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_EstadosCiviles.FUN_LIS_EstadosCiviles("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlEstadocivil, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Departamento Nacimiento
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDepartamentos()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Departamentos(int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlDepartamento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Provincia Domicilio
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDomicilioProvincia()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlDomicilioDepartamento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlDomicilioProvincia, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Distrito Domicilio
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDomicilioDistrito()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlDomicilioDepartamento.SelectedValue, ddlDomicilioProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlDomicilioDistrito, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Provincia Nacimiento
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboProvincia()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlDepartamento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlProvincia, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Distrito Nacimiento
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDistrito()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlDepartamento.SelectedValue, ddlProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlDistrito, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Empresa
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboEmpresa()

        Dim obj_BL_Empresas As New bl_Empresas
        Dim str_RazonSocial As String = ""
        Dim str_NombreComercial As String = ""
        Dim str_RUC As String = ""

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Empresas.FUN_LIS_Empresas(str_RazonSocial, str_NombreComercial, str_RUC, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlEmpresa, ds_Lista, "Codigo", "RazonSocial", False, True)

    End Sub

    ''' <summary>
    ''' Setea el estado de los campos y opciones de la ficha de alumno
    ''' </summary> 
    ''' <param name="str_Modo">Tipo de visualizacion que tendra los datos del formulario</param>  
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String)

        miTab1.Enabled = False
        miTab2.Enabled = True
        lbTab2.Text = str_Modo
        TabContainer1.ActiveTabIndex = 1

    End Sub

    ''' <summary>
    ''' Regresa a la pestaña de busqueda de fichas de alumno
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CancelarFicha()

        miTab1.Enabled = True
        miTab2.Enabled = False
        lbTab2.Text = "Actualización"
        TabContainer1.ActiveTabIndex = 0
        tbBuscarApellidoPaterno.Focus()
        ViewState("VerFicha") = False
        limpiarCampos()
        UpdatePanel1.Update()

    End Sub

    ''' <summary>
    ''' Seteara el estado de diferentes campos del bloque de información - Religión dependiendo de la religion que elija
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarReligionCatolica()
        If ddlReligion.SelectedValue <> 2 Then
            rbPriComunion.Enabled = False
            tbLugarPriComunion.Enabled = False
            tbAnioPriComunion.Enabled = False

            rbConfirmado.Enabled = False
            tbLugarConfirmado.Enabled = False
            tbAnioConfirmado.Enabled = False
        Else
            rbPriComunion.Enabled = True
            tbLugarPriComunion.Enabled = True
            tbAnioPriComunion.Enabled = True

            rbConfirmado.Enabled = True
            tbLugarConfirmado.Enabled = True
            tbAnioConfirmado.Enabled = True

        End If
    End Sub

    ''' <summary>
    ''' Seteara el estado de diferentes campos del bloque de información - Religió dependiendo de si profesa o no religión
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarProfesaReligion()

        If rbReligion.SelectedValue.ToString.Length > 0 Then


            If rbReligion.SelectedValue = 0 Then 'No
                ddlReligion.SelectedValue = 0
                ddlReligion.Enabled = False
                rbBautizo.SelectedValue = 0
                rbBautizo.Enabled = False
                tbLugarBautizo.Text = ""
                tbLugarBautizo.Enabled = False
                tbAnioBautizo.Text = ""
                tbAnioBautizo.Enabled = False

                rbPriComunion.SelectedValue = 0
                rbPriComunion.Enabled = False
                tbLugarPriComunion.Text = ""
                tbLugarPriComunion.Enabled = False
                tbAnioPriComunion.Text = ""
                tbAnioPriComunion.Enabled = False

                rbConfirmado.SelectedValue = 0
                rbConfirmado.Enabled = False
                tbLugarConfirmado.Text = ""
                tbLugarConfirmado.Enabled = False
                tbAnioConfirmado.Text = ""
                tbAnioConfirmado.Enabled = False

            Else 'Si
                ddlReligion.Enabled = True
                verificarReligionCatolica()
                rbBautizo.Enabled = True
                tbLugarBautizo.Enabled = True
                tbAnioBautizo.Enabled = True
            End If
        End If


    End Sub

    ''' <summary>
    ''' Seteara el estado de diferentes campos del bloque de información - Nacimiento dependiendo del pais de nacimiento
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarPais()

        Dim cod_Pais As Integer = 0

        ' Nacimiento
        cod_Pais = ddlPais.SelectedValue
        If cod_Pais = 1 Then ' Si el pais es PERU
            cargarComboUbigeo()
            ddlDepartamento.Enabled = True
            ddlProvincia.Enabled = True
            ddlDistrito.Enabled = True
        Else
            limpiarCombos(ddlDepartamento)
            limpiarCombos(ddlProvincia)
            limpiarCombos(ddlDistrito)

            ddlDepartamento.Enabled = False
            ddlProvincia.Enabled = False
            ddlDistrito.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Consulta la información de la ficha de alumno
    ''' </summary> 
    ''' <param name="str_Codigo">codigo del alumno al cual se va a obtener la informacion de su ficha de alumno</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal str_Codigo As String)

        Dim obj_BL_FichaAlumno As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_FichaAlumno.FUN_GET_Alumnos(str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)

        imgFotoAlumno.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & ds_Lista.Tables(0).Rows(0).Item("rutaFoto").ToString
        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString)
        hd_CodigoPersona.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersona").ToString)
        lblNombreCompleto.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString
        lblHouse.Text = ds_Lista.Tables(0).Rows(0).Item("House").ToString
        lblSituacionAnio.Text = ds_Lista.Tables(0).Rows(0).Item("estadoAnioActualAlumno").ToString
        lblFormENSnGS.Text = ds_Lista.Tables(0).Rows(0).Item("ENSnGS").ToString
        lblCodigo.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString
        lblUsuario.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoUsuario").ToString
        lblCodigoEducando.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoEducando").ToString
        tbCodigoEducando.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoEducando").ToString
        tbApellidoPaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString
        lblApellidoPaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString
        tbApellidoMaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString
        lblApellidoMaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString
        tbNombre.Text = ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString
        lblNombre.Text = ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString
        rbSexo.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoSexo").ToString
        lblSexo.Text = ds_Lista.Tables(0).Rows(0).Item("Sexo").ToString

        If ds_Lista.Tables(0).Rows(0).Item("codigoNacimientoRegistrado").ToString = "1" Then
            rbNacRegistrado.SelectedValue = 1
            lblNacRegistrado.Text = "Si"
        ElseIf ds_Lista.Tables(0).Rows(0).Item("codigoNacimientoRegistrado").ToString = "0" Then
            lblNacRegistrado.Text = "No"
        End If

        tbFechaNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString
        lblFechaNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString
        ddlTipoDocumento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoTipoDocIdentidad").ToString
        lblTipoDocumento.Text = ds_Lista.Tables(0).Rows(0).Item("TipoDocIdentidad").ToString
        tbNumDocumento.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString
        lblNumDocumento.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString

        If ds_Lista.Tables(1).Rows.Count > 1 Then
            ddlNacionalidad1.SelectedValue = ds_Lista.Tables(1).Rows(0).Item("CodigoNacionalidad").ToString
            lblNacionalidad1.Text = ds_Lista.Tables(1).Rows(0).Item("Nacionalidad").ToString
            ddlNacionalidad2.SelectedValue = ds_Lista.Tables(1).Rows(1).Item("CodigoNacionalidad").ToString
            lblNacionalidad2.Text = ds_Lista.Tables(1).Rows(1).Item("Nacionalidad").ToString
            hd_CodigoRelacionNacionalidadesPersonas1.Value = CInt(ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion").ToString)
            hd_CodigoRelacionNacionalidadesPersonas2.Value = CInt(ds_Lista.Tables(1).Rows(1).Item("CodigoRelacion").ToString)
        ElseIf ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") = -1 Then
            ddlNacionalidad1.SelectedValue = 0
            ddlNacionalidad2.SelectedValue = 0
            lblNacionalidad1.Text = "-"
            lblNacionalidad2.Text = "-"
            hd_CodigoRelacionNacionalidadesPersonas1.Value = 0
            hd_CodigoRelacionNacionalidadesPersonas2.Value = 0
        Else 'TRUE = 1
            ddlNacionalidad1.SelectedValue = ds_Lista.Tables(1).Rows(0).Item("CodigoNacionalidad").ToString
            ddlNacionalidad2.SelectedValue = 0
            lblNacionalidad1.Text = ds_Lista.Tables(1).Rows(0).Item("Nacionalidad").ToString
            lblNacionalidad2.Text = "-"
            hd_CodigoRelacionNacionalidadesPersonas1.Value = CInt(ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion").ToString)
            hd_CodigoRelacionNacionalidadesPersonas2.Value = 0
        End If

        If ds_Lista.Tables(2).Rows.Count > 1 Then 'Si tiene 2 registros

            If ds_Lista.Tables(2).Rows(0).Item("LenguaMaterna") = 1 Then 'El primer registro se carga en el combo superior

                ddlLenguaMaterna1.SelectedValue = ds_Lista.Tables(2).Rows(0).Item("CodigoIdioma").ToString
                ddlLenguaMaterna2.SelectedValue = ds_Lista.Tables(2).Rows(1).Item("CodigoIdioma").ToString
                hd_CodigoRelacionIdiomasPersonas1.Value = CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion").ToString)
                hd_CodigoRelacionIdiomasPersonas2.Value = CInt(ds_Lista.Tables(2).Rows(1).Item("CodigoRelacion").ToString)
                lblLenguaMaterna1.Text = ds_Lista.Tables(2).Rows(0).Item("Idioma").ToString
                lblLenguaMaterna2.Text = ds_Lista.Tables(2).Rows(1).Item("Idioma").ToString

            ElseIf ds_Lista.Tables(2).Rows(1).Item("LenguaMaterna") = 1 Then 'El segundo registro se carga en el combo superior

                ddlLenguaMaterna1.SelectedValue = ds_Lista.Tables(2).Rows(1).Item("CodigoIdioma").ToString
                ddlLenguaMaterna2.SelectedValue = ds_Lista.Tables(2).Rows(0).Item("CodigoIdioma").ToString
                hd_CodigoRelacionIdiomasPersonas1.Value = CInt(ds_Lista.Tables(2).Rows(1).Item("CodigoRelacion").ToString)
                hd_CodigoRelacionIdiomasPersonas2.Value = CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion").ToString)
                lblLenguaMaterna1.Text = ds_Lista.Tables(2).Rows(0).Item("Idioma").ToString
                lblLenguaMaterna2.Text = ds_Lista.Tables(2).Rows(1).Item("Idioma").ToString
            Else 'Si ninguno presenta el check LenguaMaterna, se carga según vienen los registros

                ddlLenguaMaterna1.SelectedValue = ds_Lista.Tables(2).Rows(0).Item("CodigoIdioma").ToString
                ddlLenguaMaterna2.SelectedValue = ds_Lista.Tables(2).Rows(1).Item("CodigoIdioma").ToString
                hd_CodigoRelacionIdiomasPersonas1.Value = CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion").ToString)
                hd_CodigoRelacionIdiomasPersonas2.Value = CInt(ds_Lista.Tables(2).Rows(1).Item("CodigoRelacion").ToString)
                lblLenguaMaterna1.Text = ds_Lista.Tables(2).Rows(0).Item("Idioma").ToString
                lblLenguaMaterna2.Text = ds_Lista.Tables(2).Rows(1).Item("Idioma").ToString
            End If

        ElseIf ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") = -1 Then 'Sino tiene registros de detalle
            ddlLenguaMaterna1.SelectedValue = 0
            ddlLenguaMaterna2.SelectedValue = 0
            lblLenguaMaterna1.Text = "-"
            lblLenguaMaterna2.Text = "-"
            hd_CodigoRelacionIdiomasPersonas1.Value = 0
            hd_CodigoRelacionIdiomasPersonas2.Value = 0
        Else 'Si tiene 1 solo registro

            If ds_Lista.Tables(2).Rows(0).Item("LenguaMaterna") = 1 Then ' Si el registro tiene el check se carga en el combo superior

                ddlLenguaMaterna1.SelectedValue = ds_Lista.Tables(2).Rows(0).Item("CodigoIdioma").ToString
                ddlLenguaMaterna2.SelectedValue = 0
                hd_CodigoRelacionIdiomasPersonas1.Value = CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion").ToString)
                hd_CodigoRelacionIdiomasPersonas2.Value = 0
                lblLenguaMaterna1.Text = ds_Lista.Tables(2).Rows(0).Item("Idioma").ToString
                lblLenguaMaterna2.Text = "-"
            Else ' Sino tiene el check se carga en el combo inferior

                ddlLenguaMaterna2.SelectedValue = ds_Lista.Tables(2).Rows(0).Item("CodigoIdioma").ToString
                ddlLenguaMaterna1.SelectedValue = 0
                hd_CodigoRelacionIdiomasPersonas2.Value = CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion").ToString)
                hd_CodigoRelacionIdiomasPersonas1.Value = 0
                lblLenguaMaterna1.Text = "-"
                lblLenguaMaterna2.Text = ds_Lista.Tables(2).Rows(0).Item("Idioma").ToString

            End If

        End If

        ddlEstadocivil.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoEstadocivil").ToString
        lblEstadocivil.Text = ds_Lista.Tables(0).Rows(0).Item("Estadocivil").ToString
        tbCantidadHermanos.Text = ds_Lista.Tables(0).Rows(0).Item("CantidadHermanos").ToString
        lblCantidadHermanos.Text = ds_Lista.Tables(0).Rows(0).Item("CantidadHermanos").ToString
        tbPosicionHermanos.Text = ds_Lista.Tables(0).Rows(0).Item("PosicionEntreHermanos").ToString
        lblPosicionHermanos.Text = ds_Lista.Tables(0).Rows(0).Item("PosicionEntreHermanos").ToString
        tbCorreoElectronico.Text = ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString
        lblCorreoElectronico.Text = ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString
        tbCorreoElectronicoInstitucional.Text = ds_Lista.Tables(0).Rows(0).Item("CorreoInstitucional").ToString
        lblCorreoElectronicoInstitucional.Text = ds_Lista.Tables(0).Rows(0).Item("CorreoInstitucional").ToString
        tbCelular.Text = ds_Lista.Tables(0).Rows(0).Item("Celular").ToString
        lblCelular.Text = ds_Lista.Tables(0).Rows(0).Item("Celular").ToString
        tbExperienciasTraumaticas.Text = ds_Lista.Tables(0).Rows(0).Item("ExperienciasTraumaticasDescripcion").ToString
        lblExperienciasTraumaticas.Text = ds_Lista.Tables(0).Rows(0).Item("ExperienciasTraumaticasDescripcion").ToString

        'If ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion").ToString = "1" Then
        '    rbReligion.SelectedValue = 1
        'ElseIf ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion").ToString = "0" Then
        '    rbReligion.SelectedValue = 0
        'End If

        'verificarProfesaReligion()

        If ds_Lista.Tables(0).Rows(0).Item("Bautizo").ToString = "1" Then
            rbBautizo.SelectedValue = 1
            lblBautizo.Text = "Si"
        ElseIf ds_Lista.Tables(0).Rows(0).Item("Bautizo").ToString = "0" Then
            rbBautizo.SelectedValue = 0
            lblBautizo.Text = "No"
        End If
        tbLugarBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoLugar").ToString
        lblLugarBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoLugar").ToString
        tbAnioBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoAnio").ToString
        lblAnioBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoAnio").ToString

        If ds_Lista.Tables(0).Rows(0).Item("PrimeraComunion").ToString = "1" Then
            rbPriComunion.SelectedValue = 1
            lblPriComunion.Text = "Si"
        ElseIf ds_Lista.Tables(0).Rows(0).Item("PrimeraComunion").ToString = "0" Then
            rbPriComunion.SelectedValue = 0
            lblPriComunion.Text = "No"
        End If
        tbLugarPriComunion.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionLugar").ToString
        lblLugarPriComunion.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionLugar").ToString
        tbAnioPriComunion.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionAnio").ToString
        lblAnioPriComunion.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionAnio").ToString

        If ds_Lista.Tables(0).Rows(0).Item("Confirmacion").ToString = "1" Then
            rbConfirmado.SelectedValue = 1
            lblConfirmado.Text = "Si"
        ElseIf ds_Lista.Tables(0).Rows(0).Item("Confirmacion").ToString = "0" Then
            rbConfirmado.SelectedValue = 0
            lblConfirmado.Text = "No"
        End If
        tbLugarConfirmado.Text = ds_Lista.Tables(0).Rows(0).Item("ConfirmacionLugar").ToString
        lblLugarConfirmado.Text = ds_Lista.Tables(0).Rows(0).Item("ConfirmacionLugar").ToString
        tbAnioConfirmado.Text = ds_Lista.Tables(0).Rows(0).Item("ComfirmacionAnio").ToString
        lblAnioConfirmado.Text = ds_Lista.Tables(0).Rows(0).Item("ComfirmacionAnio").ToString

        If Convert.ToInt16(rbConfirmado.SelectedValue.ToString.Length) > 0 Or Convert.ToInt16(rbPriComunion.SelectedValue.ToString.Length) > 0 Or Convert.ToInt16(rbBautizo.SelectedValue.ToString.Length) > 0 Then
            rbReligion.SelectedValue = 1
            lblProfesaAlgunaReligion.Text = "Si"
        
        End If


        'ddlReligion.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoReligion").ToString
        'If ds_Lista.Tables(0).Rows(0).Item("CodigoReligion") > 1 Then ' si tiene religion

        '    If ds_Lista.Tables(0).Rows(0).Item("Bautizo").ToString = "1" Then
        '        rbBautizo.SelectedValue = 1
        '        'lblTabiqueDesviado.Text = "Si"
        '    ElseIf ds_Lista.Tables(0).Rows(0).Item("Bautizo").ToString = "0" Then
        '        rbBautizo.SelectedValue = 0
        '        'lblTabiqueDesviado.Text = "No"
        '    End If
        '    tbLugarBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoLugar").ToString
        '    tbAnioBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoAnio").ToString

        '    If ds_Lista.Tables(0).Rows(0).Item("CodigoReligion") = 2 Then ' si su religion es catolica

        '        If ds_Lista.Tables(0).Rows(0).Item("PrimeraComunion").ToString = "1" Then
        '            rbPriComunion.SelectedValue = 1
        '            'lblTabiqueDesviado.Text = "Si"
        '        ElseIf ds_Lista.Tables(0).Rows(0).Item("PrimeraComunion").ToString = "0" Then
        '            rbPriComunion.SelectedValue = 0
        '            'lblTabiqueDesviado.Text = "No"
        '        End If
        '        tbLugarPriComunion.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionLugar").ToString
        '        tbAnioPriComunion.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionAnio").ToString

        '        If ds_Lista.Tables(0).Rows(0).Item("Confirmacion").ToString = "1" Then
        '            rbConfirmado.SelectedValue = 1
        '            'lblTabiqueDesviado.Text = "Si"
        '        ElseIf ds_Lista.Tables(0).Rows(0).Item("Confirmacion").ToString = "0" Then
        '            rbConfirmado.SelectedValue = 0
        '            'lblTabiqueDesviado.Text = "No"
        '        End If
        '        tbLugarConfirmado.Text = ds_Lista.Tables(0).Rows(0).Item("ConfirmacionLugar").ToString
        '        tbAnioConfirmado.Text = ds_Lista.Tables(0).Rows(0).Item("ComfirmacionAnio").ToString

        '    Else

        '    End If


        'Else ' Si no tiene religion

        'End If

        'ddlReligion.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoReligion").ToString

        'If ds_Lista.Tables(0).Rows(0).Item("Bautizo").ToString = True Then
        '    rbBautizo.SelectedValue = 1
        '    'lblTabiqueDesviado.Text = "Si"
        'ElseIf ds_Lista.Tables(0).Rows(0).Item("Bautizo").ToString = False Then
        '    rbBautizo.SelectedValue = 0
        '    'lblTabiqueDesviado.Text = "No"
        'End If
        'tbLugarBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoLugar").ToString
        'tbAnioBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoAnio").ToString

        'If ds_Lista.Tables(0).Rows(0).Item("PrimeraComunion").ToString = True Then
        '    rbPriComunion.SelectedValue = 1
        '    'lblTabiqueDesviado.Text = "Si"
        'ElseIf ds_Lista.Tables(0).Rows(0).Item("PrimeraComunion").ToString = False Then
        '    rbPriComunion.SelectedValue = 0
        '    'lblTabiqueDesviado.Text = "No"
        'End If
        'tbLugarPriComunion.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionLugar").ToString
        'tbAnioPriComunion.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionAnio").ToString

        'If ds_Lista.Tables(0).Rows(0).Item("Confirmacion").ToString = True Then
        '    rbConfirmado.SelectedValue = 1
        '    'lblTabiqueDesviado.Text = "Si"
        'ElseIf ds_Lista.Tables(0).Rows(0).Item("Confirmacion").ToString = False Then
        '    rbConfirmado.SelectedValue = 0
        '    'lblTabiqueDesviado.Text = "No"
        'End If
        'tbLugarConfirmado.Text = ds_Lista.Tables(0).Rows(0).Item("ConfirmacionLugar").ToString
        'tbAnioConfirmado.Text = ds_Lista.Tables(0).Rows(0).Item("ComfirmacionAnio").ToString

        tbDomicilioUrbanizacion.Text = ds_Lista.Tables(0).Rows(0).Item("Urbanizacion").ToString
        lblDomicilioUrbanizacion.Text = ds_Lista.Tables(0).Rows(0).Item("Urbanizacion").ToString
        tbDomicilioDireccion.Text = ds_Lista.Tables(0).Rows(0).Item("Direccion").ToString
        lblDomicilioDireccion.Text = ds_Lista.Tables(0).Rows(0).Item("Direccion").ToString
        tbDomicilioReferencia.Text = ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomiciliaria").ToString
        lblDomicilioReferencia.Text = ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomiciliaria").ToString
        tbDomicilioTelefono.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoCasa").ToString
        lblDomicilioTelefono.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoCasa").ToString

        If ds_Lista.Tables(0).Rows(0).Item("AccesoInternet").ToString = 1 Then
            rbDomicilioAccesoInternet.SelectedValue = 1
            lblDomicilioAccesoInternet.Text = "Si"
        ElseIf ds_Lista.Tables(0).Rows(0).Item("AccesoInternet").ToString = 0 Then
            rbDomicilioAccesoInternet.SelectedValue = 0
            lblDomicilioAccesoInternet.Text = "No"
        End If

        ddlDomicilioDepartamento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoDomicilioDepartamento").ToString
        lblDomicilioDepartamento.Text = ds_Lista.Tables(0).Rows(0).Item("DomicilioDepartamento").ToString
        cargarComboDomicilioProvincia()
        ddlDomicilioProvincia.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoDomicilioProvincia").ToString
        lblDomicilioProvincia.Text = ds_Lista.Tables(0).Rows(0).Item("DomicilioProvincia").ToString
        cargarComboDomicilioDistrito()
        ddlDomicilioDistrito.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoDomicilioDistrito").ToString
        lblDomicilioDistrito.Text = ds_Lista.Tables(0).Rows(0).Item("DomicilioDistrito").ToString

        ddlDepartamento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoDepartamento").ToString
        lblDepartamento.Text = ds_Lista.Tables(0).Rows(0).Item("Departamento").ToString
        cargarComboProvincia()
        ddlProvincia.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoProvincia").ToString
        lblProvincia.Text = ds_Lista.Tables(0).Rows(0).Item("Provincia").ToString
        cargarComboDistrito()
        ddlDistrito.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoDistrito").ToString
        lblDistrito.Text = ds_Lista.Tables(0).Rows(0).Item("Distrito").ToString
        ddlPais.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoPais").ToString
        lblPais.Text = ds_Lista.Tables(0).Rows(0).Item("Pais").ToString
        'En caso de emergencia
        tbNombreCompletoEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("NombreContactoAvisoEmergencia").ToString
        lblNombreCompletoEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("NombreContactoAvisoEmergencia").ToString
        tbTelfCasaEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("TelfCasaContactoAvisoEmergencia").ToString
        lblTelfCasaEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("TelfCasaContactoAvisoEmergencia").ToString
        tbTelfMovilEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("CellContactoAvisoEmergencia").ToString
        lblTelfMovilEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("CellContactoAvisoEmergencia").ToString
        tbTelfOficinaEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("TelfOficinaContactoAvisoEmergencia").ToString
        lblTelfOficinaEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("TelfOficinaContactoAvisoEmergencia").ToString

        ' Facturacion
        rbEmitirFactura.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("EmitirFactura"))
        ddlEmpresa.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoEmpresa").ToString

        'Detalle discapacidades

        If ds_Lista.Tables(3).Rows.Count > 0 Then
            If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then
                gvDetalleDiscapacidad.DataSource = ds_Lista.Tables(3)
                gvDetalleDiscapacidad.DataBind()
                ViewState("ListaDiscapacidad") = ds_Lista.Tables(3)
            End If
        End If

        'Detalle Familia
        If ds_Lista.Tables(4).Rows.Count > 0 Then
            If ds_Lista.Tables(4).Rows(0).Item("CodigoRelacion") <> -1 Then
                gvFamiliares.DataSource = ds_Lista.Tables(4)
                gvFamiliares.DataBind()
                ViewState("ListaFamilia") = ds_Lista.Tables(4)
            End If
        End If

        'Datos del Seguro
        If ds_Lista.Tables(5).Rows.Count > 0 Then
            If ds_Lista.Tables(5).Rows(0).Item("CodigoRelacion") <> -1 Then
                gvDetalleDatosSeguro.DataSource = ds_Lista.Tables(5)
                gvDetalleDatosSeguro.DataBind()
                ViewState("ListaDatosSeguro") = ds_Lista.Tables(5)
            Else
                gvDetalleDatosSeguro.DataBind()
            End If
        End If

        'Datos del Retiro
        If ds_Lista.Tables(6).Rows.Count > 0 Then
            If ds_Lista.Tables(6).Rows(0).Item("CodigoRelacion") <> -1 Then
                gvDetalleRetiro.DataSource = ds_Lista.Tables(6)
                gvDetalleRetiro.DataBind()
                ViewState("ListaRetiro") = ds_Lista.Tables(6)
            Else
                gvDetalleRetiro.DataBind()
            End If
        End If

        'Datos del Procedencia
        If ds_Lista.Tables(7).Rows.Count > 0 Then
            If ds_Lista.Tables(7).Rows(0).Item("CodigoRelacion") <> -1 Then
                gvDetalleProcedencia.DataSource = ds_Lista.Tables(7)
                gvDetalleProcedencia.DataBind()
                ViewState("ListaProcedencia") = ds_Lista.Tables(7)
            Else
                gvDetalleProcedencia.DataBind()
            End If
        End If

        'Datos de matriculas
        If ds_Lista.Tables(11).Rows.Count > 0 Then
            gvDetalleMatriculas.DataSource = ds_Lista.Tables(11)
            gvDetalleMatriculas.DataBind()
        Else
            gvDetalleMatriculas.DataBind()
        End If

    End Sub

    ''' <summary>
    ''' Todos los label estan visible y los demas objetos como el combo,textbox,.. se encuentran no visible,cuando se da click en la grilla la opcion de "Ver"
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     18/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub DatosDeshabilitados()

        btnGrabar.Visible = False
        tbCodigoEducando.Visible = False
        tbApellidoPaterno.Visible = False
        tbApellidoMaterno.Visible = False
        tbNombre.Visible = False
        rbSexo.Visible = False
        rbNacRegistrado.Visible = False
        tbFechaNacimiento.Visible = False
        ddlTipoDocumento.Visible = False
        tbNumDocumento.Visible = False
        ddlNacionalidad1.Visible = False
        ddlNacionalidad2.Visible = False
        ddlLenguaMaterna1.Visible = False
        ddlLenguaMaterna2.Visible = False

        ddlEstadocivil.Visible = False
        tbCantidadHermanos.Visible = False
        tbPosicionHermanos.Visible = False
        tbCorreoElectronico.Visible = False
        tbCorreoElectronicoInstitucional.Visible = False
        tbCelular.Visible = False
        tbExperienciasTraumaticas.Visible = False
        ddlReligion.Visible = False
        rbBautizo.Visible = False
        tbLugarBautizo.Visible = False
        tbAnioBautizo.Visible = False
        rbPriComunion.Visible = False
        tbLugarPriComunion.Visible = False
        tbAnioPriComunion.Visible = False
        rbConfirmado.Visible = False
        tbLugarConfirmado.Visible = False
        tbAnioConfirmado.Visible = False
        rbReligion.Visible = False
        tbDomicilioUrbanizacion.Visible = False
        tbDomicilioDireccion.Visible = False
        tbDomicilioReferencia.Visible = False
        tbDomicilioTelefono.Visible = False
        rbDomicilioAccesoInternet.Visible = False
        imageBF5.Visible = False
        ddlDomicilioDepartamento.Visible = False
        ddlDomicilioProvincia.Visible = False
        ddlDomicilioDistrito.Visible = False
        ddlDepartamento.Visible = False
        ddlProvincia.Visible = False
        ddlDistrito.Visible = False
        ddlPais.Visible = False
        'En caso de emergencia
        tbNombreCompletoEmergencia.Visible = False
        tbTelfCasaEmergencia.Visible = False
        tbTelfMovilEmergencia.Visible = False
        tbTelfOficinaEmergencia.Visible = False

        '''''''''
        'label
        ''''''
        lblCodigoEducando.Visible = True
        lblApellidoPaterno.Visible = True
        lblApellidoMaterno.Visible = True
        lblNombre.Visible = True
        lblSexo.Visible = True
        lblNacRegistrado.Visible = True
        lblFechaNacimiento.Visible = True
        lblTipoDocumento.Visible = True
        lblNumDocumento.Visible = True
        lblNacionalidad1.Visible = True
        lblNacionalidad2.Visible = True
        lblLenguaMaterna1.Visible = True
        lblLenguaMaterna2.Visible = True
        lblEstadocivil.Visible = True
        lblCantidadHermanos.Visible = True
        lblPosicionHermanos.Visible = True
        lblCorreoElectronico.Visible = True
        lblCorreoElectronicoInstitucional.Visible = True
        lblCelular.Visible = True
        lblExperienciasTraumaticas.Visible = True
        lblReligionProfesa.Visible = True
        lblBautizo.Visible = True
        lblLugarBautizo.Visible = True
        lblAnioBautizo.Visible = True
        lblPriComunion.Visible = True
        lblLugarPriComunion.Visible = True
        lblAnioPriComunion.Visible = True
        lblConfirmado.Visible = True
        lblLugarConfirmado.Visible = True
        lblAnioConfirmado.Visible = True
        lblProfesaAlgunaReligion.Visible = True
        lblDomicilioUrbanizacion.Visible = True
        lblDomicilioDireccion.Visible = True
        lblDomicilioReferencia.Visible = True
        lblDomicilioTelefono.Visible = True
        lblDomicilioAccesoInternet.Visible = True

        lblDomicilioDepartamento.Visible = True
        lblDomicilioProvincia.Visible = True
        lblDomicilioDistrito.Visible = True
        lblDepartamento.Visible = True
        lblProvincia.Visible = True
        lblDistrito.Visible = True
        lblPais.Visible = True
        'En caso de emergencia
        lblNombreCompletoEmergencia.Visible = True
        lblTelfCasaEmergencia.Visible = True
        lblTelfMovilEmergencia.Visible = True
        lblTelfOficinaEmergencia.Visible = True

        btn_Add_Discapacidad.Visible = False
        btn_Add_Retiro.Visible = False
        btn_Add_Procedencia.Visible = False

        ViewState("VerFicha") = True

    End Sub

    ''' <summary>
    ''' Todos los label se encuentran no visibles y los demas objetos como el combo,textbox... se encuentran visible
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub DatosHabilitados()
        btnGrabar.Visible = True
        tbCodigoEducando.Visible = True
        tbApellidoPaterno.Visible = True
        tbApellidoMaterno.Visible = True
        tbNombre.Visible = True
        rbSexo.Visible = True
        rbNacRegistrado.Visible = True
        tbFechaNacimiento.Visible = True
        ddlTipoDocumento.Visible = True
        tbNumDocumento.Visible = True
        ddlNacionalidad1.Visible = True
        ddlNacionalidad2.Visible = True
        ddlLenguaMaterna1.Visible = True
        ddlLenguaMaterna2.Visible = True
        imageBF5.Visible = True
        ddlEstadocivil.Visible = True
        tbCantidadHermanos.Visible = True
        tbPosicionHermanos.Visible = True
        tbCorreoElectronico.Visible = True
        tbCorreoElectronicoInstitucional.Visible = True
        tbCelular.Visible = True
        tbExperienciasTraumaticas.Visible = True
        ddlReligion.Visible = True
        rbBautizo.Visible = True
        tbLugarBautizo.Visible = True
        tbAnioBautizo.Visible = True
        rbPriComunion.Visible = True
        tbLugarPriComunion.Visible = True
        tbAnioPriComunion.Visible = True
        rbConfirmado.Visible = True
        tbLugarConfirmado.Visible = True
        tbAnioConfirmado.Visible = True
        rbReligion.Visible = True
        tbDomicilioUrbanizacion.Visible = True
        tbDomicilioDireccion.Visible = True
        tbDomicilioReferencia.Visible = True
        tbDomicilioTelefono.Visible = True
        rbDomicilioAccesoInternet.Visible = True

        ddlDomicilioDepartamento.Visible = True
        ddlDomicilioProvincia.Visible = True
        ddlDomicilioDistrito.Visible = True
        ddlDepartamento.Visible = True
        ddlProvincia.Visible = True
        ddlDistrito.Visible = True
        ddlPais.Visible = True
        'En caso de emergencia
        tbNombreCompletoEmergencia.Visible = True
        tbTelfCasaEmergencia.Visible = True
        tbTelfMovilEmergencia.Visible = True
        tbTelfOficinaEmergencia.Visible = True
       
        '''''''''
        'label
        ''''''
        lblCodigoEducando.Visible = False
        lblApellidoPaterno.Visible = False
        lblApellidoMaterno.Visible = False
        lblNombre.Visible = False
        lblSexo.Visible = False
        lblNacRegistrado.Visible = False
        lblFechaNacimiento.Visible = False
        lblTipoDocumento.Visible = False
        lblNumDocumento.Visible = False
        lblNacionalidad1.Visible = False
        lblNacionalidad2.Visible = False
        lblLenguaMaterna1.Visible = False
        lblLenguaMaterna2.Visible = False
        lblEstadocivil.Visible = False
        lblCantidadHermanos.Visible = False
        lblPosicionHermanos.Visible = False
        lblCorreoElectronico.Visible = False
        lblCorreoElectronicoInstitucional.Visible = False
        lblCelular.Visible = False
        lblExperienciasTraumaticas.Visible = False
        lblReligionProfesa.Visible = False
        lblBautizo.Visible = False
        lblLugarBautizo.Visible = False
        lblAnioBautizo.Visible = False
        lblPriComunion.Visible = False
        lblLugarPriComunion.Visible = False
        lblAnioPriComunion.Visible = False
        lblConfirmado.Visible = False
        lblLugarConfirmado.Visible = False
        lblAnioConfirmado.Visible = False
        lblProfesaAlgunaReligion.Visible = False
        lblDomicilioUrbanizacion.Visible = False
        lblDomicilioDireccion.Visible = False
        lblDomicilioReferencia.Visible = False
        lblDomicilioTelefono.Visible = False
        lblDomicilioAccesoInternet.Visible = False

        lblDomicilioDepartamento.Visible = False
        lblDomicilioProvincia.Visible = False
        lblDomicilioDistrito.Visible = False
        lblDepartamento.Visible = False
        lblProvincia.Visible = False
        lblDistrito.Visible = False
        lblPais.Visible = False
        'En caso de emergencia
        lblNombreCompletoEmergencia.Visible = False
        lblTelfCasaEmergencia.Visible = False
        lblTelfMovilEmergencia.Visible = False
        lblTelfOficinaEmergencia.Visible = False

        btn_Add_Discapacidad.Visible = True
        btn_Add_Retiro.Visible = True
        btn_Add_Procedencia.Visible = True

        ViewState("VerFicha") = False

    End Sub

    ''' <summary>
    ''' Graba los datos de la ficha de alumno
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarFicha(ByVal bool_FichaCompleta As Boolean)

        Dim obj_BE_Alumno As New be_Alumnos
        Dim obj_BL_Alumno As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        'Datos Personales

        obj_BE_Alumno.CodigoAlumno = hd_Codigo.Value
        obj_BE_Alumno.CodigoEducando = tbCodigoEducando.Text
        obj_BE_Alumno.CodigoPersona = hd_CodigoPersona.Value
        obj_BE_Alumno.CodigoRelacionIdiomasPersonas1 = hd_CodigoRelacionIdiomasPersonas1.Value
        obj_BE_Alumno.CodigoRelacionIdiomasPersonas2 = hd_CodigoRelacionIdiomasPersonas2.Value
        obj_BE_Alumno.CodigoRelacionNacionalidadesPersonas1 = hd_CodigoRelacionNacionalidadesPersonas1.Value
        obj_BE_Alumno.CodigoRelacionNacionalidadesPersonas2 = hd_CodigoRelacionNacionalidadesPersonas2.Value
        obj_BE_Alumno.ApellidoPaterno = tbApellidoPaterno.Text
        obj_BE_Alumno.ApellidoMaterno = tbApellidoMaterno.Text
        obj_BE_Alumno.Nombre = tbNombre.Text
        obj_BE_Alumno.CodigoSexo = rbSexo.SelectedValue

        obj_BE_Alumno.CodigoIdioma1 = ddlLenguaMaterna1.SelectedValue
        obj_BE_Alumno.CodigoIdioma2 = ddlLenguaMaterna2.SelectedValue

        obj_BE_Alumno.CodigoNacionalidades1 = ddlNacionalidad1.SelectedValue
        obj_BE_Alumno.CodigoNacionalidades2 = ddlNacionalidad2.SelectedValue

        obj_BE_Alumno.CodigoTipoDocIdentidad = ddlTipoDocumento.SelectedValue
        obj_BE_Alumno.NumeroDocIdentidad = tbNumDocumento.Text
        obj_BE_Alumno.NacimientoRegistrado = IIf(rbNacRegistrado.SelectedValue.ToString.Length > 0, rbNacRegistrado.SelectedValue, -1)
        obj_BE_Alumno.FechaNacimiento = tbFechaNacimiento.Text
        obj_BE_Alumno.CodigoPais = ddlPais.SelectedValue
        obj_BE_Alumno.CodigoNacimientoUbigeo = ddlDepartamento.SelectedValue.ToString & ddlProvincia.SelectedValue.ToString & ddlDistrito.SelectedValue.ToString
        obj_BE_Alumno.CodigoEstadoCivil = ddlEstadocivil.SelectedValue
        obj_BE_Alumno.CantidadHermanos = tbCantidadHermanos.Text
        obj_BE_Alumno.PosicionEntreHermanos = tbPosicionHermanos.Text
        obj_BE_Alumno.EmailPersonal = tbCorreoElectronico.Text
        obj_BE_Alumno.CorreoInstitucional = tbCorreoElectronicoInstitucional.Text
        obj_BE_Alumno.Celular = tbCelular.Text


        'If rbReligion.SelectedValue = 0 Then
        '    rbReligion.SelectedValue = False
        'ElseIf rbReligion.SelectedValue = 1 Then
        '    rbReligion.SelectedValue = True

        'If rbReligion.SelectedValue = 1 Then ' Si
        '    obj_BE_Alumno.ProfesaReligion = IIf(ddlReligion.SelectedValue = 0, Nothing, ddlReligion.SelectedValue)
        'ElseIf rbReligion.SelectedValue = 0 Then ' No
        '    obj_BE_Alumno.ProfesaReligion = Nothing
        'End If

        'End If


        obj_BE_Alumno.ProfesaReligion = IIf(rbReligion.SelectedValue.ToString.Length > 0, rbReligion.SelectedValue, -1)

        obj_BE_Alumno.CodigoReligion = IIf(ddlReligion.SelectedValue = 0, Nothing, ddlReligion.SelectedValue)


        'If rbBautizo.SelectedValue = 0 Then
        '    rbBautizo.SelectedValue = True
        'ElseIf rbBautizo.SelectedValue = 1 Then
        '    rbBautizo.SelectedValue = False
        'End If


        obj_BE_Alumno.Bautizo = IIf(rbBautizo.SelectedValue.ToString.Length > 0, rbBautizo.SelectedValue, -1)
        obj_BE_Alumno.BautizoLugar = tbLugarBautizo.Text
        obj_BE_Alumno.BautizoAnio = IIf(tbAnioBautizo.Text.Length > 0, tbAnioBautizo.Text, -1)

        'If rbPriComunion.SelectedValue = 0 Then
        '    rbPriComunion.SelectedValue = True
        'ElseIf rbPriComunion.SelectedValue = 1 Then
        '    rbPriComunion.SelectedValue = False
        'End If

        obj_BE_Alumno.PrimeraComunion = IIf(rbPriComunion.SelectedValue.ToString.Length > 0, rbPriComunion.SelectedValue, -1)
        obj_BE_Alumno.PrimeraComunionLugar = tbLugarPriComunion.Text
        obj_BE_Alumno.PrimeraComunionAnio = IIf(tbAnioPriComunion.Text.Length > 0, tbAnioPriComunion.Text, -1)

        'If rbConfirmado.SelectedValue = 0 Then
        '    rbConfirmado.SelectedValue = True
        'ElseIf rbConfirmado.SelectedValue = 1 Then
        '    rbConfirmado.SelectedValue = False
        'End If

        obj_BE_Alumno.Confirmacion = IIf(rbConfirmado.SelectedValue.ToString.Length > 0, rbConfirmado.SelectedValue, -1)
        obj_BE_Alumno.ConfirmacionLugar = tbLugarConfirmado.Text
        obj_BE_Alumno.ConfirmacionAnio = IIf(tbAnioConfirmado.Text.Length > 0, tbAnioConfirmado.Text, -1)

        obj_BE_Alumno.CodigoUbigeo = CStr(IIf(ddlDomicilioDepartamento.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioDepartamento.SelectedValue, ddlDomicilioDepartamento.SelectedValue)) & _
                                    CStr(IIf(ddlDomicilioProvincia.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioProvincia.SelectedValue, ddlDomicilioProvincia.SelectedValue)) & _
                                    CStr(IIf(ddlDomicilioDistrito.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioDistrito.SelectedValue, ddlDomicilioDistrito.SelectedValue))
        obj_BE_Alumno.Urbanizacion = tbDomicilioUrbanizacion.Text
        obj_BE_Alumno.Direccion = tbDomicilioDireccion.Text
        obj_BE_Alumno.ReferenciaDomiciliaria = tbDomicilioReferencia.Text
        obj_BE_Alumno.TelefonoCasa = tbDomicilioTelefono.Text

        obj_BE_Alumno.AccesoInternet = IIf(rbDomicilioAccesoInternet.SelectedValue.ToString.Length > 0, rbDomicilioAccesoInternet.SelectedValue, -1)
        obj_BE_Alumno.NombreContactoAvisoEmergencia = tbNombreCompletoEmergencia.Text
        obj_BE_Alumno.TelfCasaContactoAvisoEmergencia = tbTelfCasaEmergencia.Text
        obj_BE_Alumno.NombreContactoAvisoEmergencia = tbNombreCompletoEmergencia.Text
        obj_BE_Alumno.CellContactoAvisoEmergencia = tbTelfMovilEmergencia.Text
        obj_BE_Alumno.TelfOficinaContactoAvisoEmergencia = tbTelfOficinaEmergencia.Text
        obj_BE_Alumno.ExperienciasTraumaticasDescripcion = tbExperienciasTraumaticas.Text

        ' Facturación
        obj_BE_Alumno.EmitirFactura = rbEmitirFactura.SelectedValue

        If rbEmitirFactura.SelectedValue = 1 Then ' Emitir factura : Si
            obj_BE_Alumno.CodigoEmpresa = ddlEmpresa.SelectedValue
        Else
            obj_BE_Alumno.CodigoEmpresa = 0
        End If

        ''Detalle
        Dim objDS_Detalle As New DataSet

        ''Detalle Discapacidades
        Dim objDT_Discapacidad As DataTable

        objDT_Discapacidad = New DataTable("ListaDiscapacidad")
        objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "CodigoRelTipoDiscapAlum", "String")
        objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "CodigoTipoDiscapacidad", "String")
        objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "TipoDiscapacidad", "String")
        objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "DescripcionDiscapacidad", "String")

        Dim dr_Discapacidad As DataRow

        For Each drv As GridViewRow In gvDetalleDiscapacidad.Rows

            dr_Discapacidad = objDT_Discapacidad.NewRow
            dr_Discapacidad.Item("CodigoRelTipoDiscapAlum") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Discapacidad.Item("CodigoTipoDiscapacidad") = CType(drv.FindControl("lblCodigoDiscapacidad"), Label).Text
            dr_Discapacidad.Item("TipoDiscapacidad") = CType(drv.FindControl("lblDiscapacidad"), Label).Text
            dr_Discapacidad.Item("DescripcionDiscapacidad") = CType(drv.FindControl("lblDescripcionDiscapacidad"), Label).Text
            objDT_Discapacidad.Rows.Add(dr_Discapacidad)

        Next

        ''Detalle Retiro
        Dim objDT_Retiro As DataTable

        objDT_Retiro = New DataTable("ListaRetiro")
        objDT_Retiro = Datos.agregarColumna(objDT_Retiro, "CodigoRelacion", "String")
        objDT_Retiro = Datos.agregarColumna(objDT_Retiro, "CodigoAnio", "String")
        objDT_Retiro = Datos.agregarColumna(objDT_Retiro, "CodigoMotivo", "String")
        objDT_Retiro = Datos.agregarColumna(objDT_Retiro, "CodigoColegioTraslado", "String")
        objDT_Retiro = Datos.agregarColumna(objDT_Retiro, "FechaRegistroRetiro", "Date")
        objDT_Retiro = Datos.agregarColumna(objDT_Retiro, "Observacion", "String")
        objDT_Retiro = Datos.agregarColumna(objDT_Retiro, "CodigoModular", "String")

        Dim dr_Retiro As DataRow

        For Each drv As GridViewRow In gvDetalleRetiro.Rows

            dr_Retiro = objDT_Retiro.NewRow
            dr_Retiro.Item("CodigoRelacion") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Retiro.Item("CodigoAnio") = CType(drv.FindControl("lblCodigoAnio_grilla"), Label).Text
            dr_Retiro.Item("CodigoMotivo") = CType(drv.FindControl("lblCodigoMotivo_grilla"), Label).Text
            dr_Retiro.Item("CodigoColegioTraslado") = CType(drv.FindControl("lblCodigoColegioTraslado_grilla"), Label).Text
            dr_Retiro.Item("FechaRegistroRetiro") = CType(drv.FindControl("lblFechaRegistroRetiro_grilla"), Label).Text
            dr_Retiro.Item("Observacion") = CType(drv.FindControl("lblObservacion_grilla"), Label).Text
            dr_Retiro.Item("CodigoModular") = CType(drv.FindControl("lblCodigoModular_grilla"), Label).Text
            objDT_Retiro.Rows.Add(dr_Retiro)

        Next

        ''Detalle Retiro
        Dim objDT_Procedencia As DataTable

        objDT_Procedencia = New DataTable("ListaProcedencia")
        objDT_Procedencia = Datos.agregarColumna(objDT_Procedencia, "CodigoRelacion", "String")
        objDT_Procedencia = Datos.agregarColumna(objDT_Procedencia, "CodigoAnio", "String")
        'objDT_Procedencia = Datos.agregarColumna(objDT_Retiro, "CodigoMotivo", "String")
        objDT_Procedencia = Datos.agregarColumna(objDT_Procedencia, "CodigoColegioProcedencia", "String")
        'objDT_Procedencia = Datos.agregarColumna(objDT_Retiro, "FechaRegistroRetiro", "Date")
        'objDT_Procedencia = Datos.agregarColumna(objDT_Retiro, "Observacion", "String")
        'objDT_Procedencia = Datos.agregarColumna(objDT_Retiro, "CodigoModular", "String")

        Dim dr_Procedencia As DataRow

        For Each drv As GridViewRow In gvDetalleProcedencia.Rows

            dr_Procedencia = objDT_Procedencia.NewRow
            dr_Procedencia.Item("CodigoRelacion") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Procedencia.Item("CodigoAnio") = CType(drv.FindControl("lblCodigoAnioProcedencia_grilla"), Label).Text
            'dr_Retiro.Item("CodigoMotivo") = CType(drv.FindControl("lblCodigoMotivo_grilla"), Label).Text
            dr_Procedencia.Item("CodigoColegioProcedencia") = CType(drv.FindControl("lblCodigoColegioProcedencia_grilla"), Label).Text
            'dr_Retiro.Item("FechaRegistroRetiro") = CType(drv.FindControl("lblFechaRegistroRetiro_grilla"), Label).Text
            'dr_Retiro.Item("Observacion") = CType(drv.FindControl("lblObservacion_grilla"), Label).Text
            'dr_Retiro.Item("CodigoModular") = CType(drv.FindControl("lblCodigoModular_grilla"), Label).Text
            objDT_Procedencia.Rows.Add(dr_Procedencia)

        Next
        ''Detalle Familiares
        'Dim objDT_Familia As DataTable

        'objDT_Familia = New DataTable("ListaFamilia")
        'objDT_Familia = Datos.agregarColumna(objDT_Familia, "CodigoRelacion", "String")
        'objDT_Familia = Datos.agregarColumna(objDT_Familia, "CodigoFamiliar", "String")
        'objDT_Familia = Datos.agregarColumna(objDT_Familia, "CodigoParentesco", "String")
        'objDT_Familia = Datos.agregarColumna(objDT_Familia, "Parentesco", "String")
        'objDT_Familia = Datos.agregarColumna(objDT_Familia, "ViveConAlumno", "String")
        'Dim dr_Familia As DataRow

        'For Each drv As GridViewRow In gvDetalleFamilia.Rows

        '    dr_Familia = objDT_Familia.NewRow
        '    dr_Familia.Item("CodigoRelacion") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
        '    dr_Familia.Item("CodigoFamiliar") = CType(drv.FindControl("lblCodigoFamiliar"), Label).Text
        '    dr_Familia.Item("CodigoParentesco") = CType(drv.FindControl("lblCodigoParentesco"), Label).Text
        '    dr_Familia.Item("Parentesco") = CType(drv.FindControl("lblParentesco"), Label).Text
        '    dr_Familia.Item("ViveConAlumno") = CType(drv.FindControl("lblViveConAlumno"), Label).Text
        '    objDT_Familia.Rows.Add(dr_Familia)

        'Next

        ''Agrego las DataTable a mi DataSet
        objDS_Detalle.Tables.Add(objDT_Discapacidad)
        'objDS_Detalle.Tables.Add(objDT_Familia)
        objDS_Detalle.Tables.Add(objDT_Retiro)
        objDS_Detalle.Tables.Add(objDT_Procedencia)

        If BoolGrabar = 0 Then
            'usp_valor = obj_BL_FichaAtencion.FUN_INS_FichaAtencion(obj_BE_FichaAtencion, objDS_Detalle, usp_mensaje)
        Else
            obj_BE_Alumno.CodigoAlumno = CInt(BoolGrabar)
            usp_valor = obj_BL_Alumno.FUN_UPD_Alumno(bool_FichaCompleta, obj_BE_Alumno, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
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
    ''' Valida la ficha de alumno antes de grabar
    ''' </summary>
    ''' <param name="str_Mensaje">Cadena de texto que tendra todos los mensajes de error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarFicha(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If tbApellidoPaterno.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Apellido Paterno")
            result = False
        End If

        If tbNombre.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Nombre")
            result = False
        End If

        If ddlTipoDocumento.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Documento")
            result = False
        End If

        If tbFechaNacimiento.Text.Trim = "" Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Fecha de Nacimiento")
            result = False
        ElseIf IsDate(tbFechaNacimiento.Text.Trim) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Nacimiento")
            result = False
        ElseIf CDate(tbFechaNacimiento.Text) > CDate(Today.ToShortDateString) Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 8, "Fecha de Nacimiento")
            result = False
        End If

        If ddlPais.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Pais de Nacimiento")
            result = False
        End If

        If ddlDepartamento.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Departamento de Nacimiento")
            result = False
        End If

        If ddlProvincia.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Provincia de Nacimiento")
            result = False
        End If

        If ddlDistrito.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Distrito de Nacimiento")
            result = False
        End If

        If ddlNacionalidad1.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "1era Nacionalidad")
            result = False
        End If

        If ddlLenguaMaterna1.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Lengua Materna")
            result = False
        End If

        If ddlEstadocivil.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Estado Civil")
            result = False
        End If

        If tbCantidadHermanos.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Cantidad de Hermanos")
            result = False
        End If

        If tbCorreoElectronico.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Correo Electrónico Personal")
            result = False
        End If

        If tbCorreoElectronicoInstitucional.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Correo Electrónico de la Institución")
            result = False
        End If


        If rbReligion.SelectedValue.ToString.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Religión")
            result = False
        Else
            If rbReligion.SelectedValue = 1 Then
                If ddlReligion.SelectedValue = 0 Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Religión que profesa")
                    result = False
                End If
            End If
        End If


        If ddlNacionalidad1.SelectedValue = ddlNacionalidad2.SelectedValue Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 9, "Nacionalidad")
            result = False
        End If

        If ddlLenguaMaterna1.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "1era Lengua Materna")
            result = False
        End If

        If ddlLenguaMaterna1.SelectedValue = ddlLenguaMaterna2.SelectedValue Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 10, "Lengua Materna")
            result = False
        End If

        If tbPosicionHermanos.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Posición entre hermanos")
            result = False
        End If

        If tbCorreoElectronico.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Correo Electrónico")
            result = False
        End If


        Dim valorEmail As Boolean = True
        Dim objValidaEmail As New Validacion

        valorEmail = objValidaEmail.Validar_Email(tbCorreoElectronico.Text)
        If valorEmail = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 11, "Correo Electrónico")
            result = False
        End If

        valorEmail = True
        valorEmail = objValidaEmail.Validar_Email(tbCorreoElectronicoInstitucional.Text)
        If valorEmail = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Correo Electrónico de la Institución")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub Exportar_FichaUnicaMatricula()

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        Dim ds_FichaUnicaMatricula As New DataSet
        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim str_codigoAlumno As String
        str_codigoAlumno = hd_Codigo.Value
        Dim int_codigoAnio As Integer
        int_codigoAnio = hd_AnioGrilla.Value

        ds_FichaUnicaMatricula = obj_BL_Alumnos.FUN_GET_FichaUnicaMatriculaAlumno(str_codigoAlumno, int_codigoAnio, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 0)

        NombreArchivo = Exportacion.Exportar_FichaUnicaMatricula(ds_FichaUnicaMatricula)
        NombreArchivo = NombreArchivo & ".xls"
        rutamadre = Server.MapPath(".")
        rutamadre = rutamadre.Replace("\Modulo_Matricula", "\Reportes\")

        downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

        Response.AddHeader("content-disposition", "attachment;filename=test1.xls")
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()

    End Sub

    Private Sub Exportacion_FichaAlumno()

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim str_codigoAlumno As String

        str_codigoAlumno = hd_Codigo.Value

        Dim ds_Lista As DataSet = obj_BL_Alumnos.FUN_GET_Alumnos(str_codigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

        Dim reporte_html As String = ""
        reporte_html = ExportarReporteFichaMedica_Html(ds_Lista, "") 'Exportacion.ExportarReporteFichaMedica_Html(ds_Lista, "")
        Session("Exportaciones_RepFichaAlumnoHtml") = reporte_html
        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionFichaAlumno_html();</script>", False)
    End Sub

    Private Sub Exportar_AlumnosRetirados()
        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds As DataSet
        Dim int_codigoAnio As Integer = 2

        ds = obj_BL_Alumnos.FUN_REP_AlumnosRetirados(int_codigoAnio, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 0)

        Dim dt As DataTable = New DataTable("ListaExportar")

        dt = Datos.agregarColumna(dt, "N°", "String")
        dt = Datos.agregarColumna(dt, "Nombre Completo", "String")
        dt = Datos.agregarColumna(dt, "DNI", "String")
        dt = Datos.agregarColumna(dt, "Codigo Educando", "String")
        dt = Datos.agregarColumna(dt, "Grado", "String")
        dt = Datos.agregarColumna(dt, "Aula", "String")
        dt = Datos.agregarColumna(dt, "Fecha Retiro", "String")
        dt = Datos.agregarColumna(dt, "Motivos", "String")
        dt = Datos.agregarColumna(dt, "Observaciones", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("Nombre Completo") = dr.Item("NombreCompleto").ToString
            auxDR.Item("DNI") = dr.Item("DNI").ToString
            auxDR.Item("Codigo Educando") = dr.Item("CodigoEducando").ToString
            auxDR.Item("Grado") = dr.Item("Grado").ToString
            auxDR.Item("Aula") = dr.Item("Aula").ToString
            auxDR.Item("Fecha Retiro") = dr.Item("FechaRetiro").ToString
            auxDR.Item("Motivos") = dr.Item("Motivos").ToString
            auxDR.Item("Observaciones") = dr.Item("Observaciones").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next


        NombreArchivo = Exportacion.ExportarReporte(dt, "Alumnos Retirados")
        NombreArchivo = NombreArchivo & ".xls"
        rutamadre = Server.MapPath(".")
        rutamadre = rutamadre.Replace("\Modulo_Matricula", "\Reportes\")

        downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

        Response.AddHeader("content-disposition", "attachment;filename=test1.xls")
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()
    End Sub

#End Region

#Region "Eventos del Gridview"


    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Ver" Or e.CommandName = "Imprimir" Then
                Dim CodigoAlumno As String = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    DatosHabilitados()
                    obtener(CodigoAlumno)
                    'DesactivarCampos()
                    VerRegistro("Actualización")
                    'ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", "controlador.click();", True)
                ElseIf e.CommandName = "Ver" Then
                    int_CodigoAccion = 5
                    DatosDeshabilitados()
                    obtener(CodigoAlumno)
                    VerRegistro("Visualización")
                ElseIf e.CommandName = "Imprimir" Then
                    Dim lblAnioGrilla As Label = row.FindControl("lblAnioGrilla")
                    'Muestra PopUp de Impresion 
                    hd_Codigo.Value = CodigoAlumno
                    hd_AnioGrilla.Value = lblAnioGrilla.Text
                    int_CodigoAccion = 4
                    ModalPopupExtender1.Show()

                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
            Dim btnVer As ImageButton = e.Row.FindControl("btnEliminar")
            Dim btnImprimir As ImageButton = e.Row.FindControl("btnActivar")

            If e.Row.RowType = DataControlRowType.Pager Then

                Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
                _TotalPags.Text = GridView1.PageCount.ToString

                Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
                _Registros.Text = InformacionPager(GridView1, e.Row, Me)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                If ddlEstadoAlumno.SelectedValue = 0 Then

                    If e.Row.DataItem("EstadoAlumno") = "Activo" Then
                        btnActualizar.Visible = True
                        btnVer.Visible = True
                        btnImprimir.Visible = True
                    Else
                        btnActualizar.Visible = False
                        btnVer.Visible = True
                        btnImprimir.Visible = True
                        e.Row.ForeColor = Drawing.Color.DarkRed
                    End If

                ElseIf ddlEstadoAlumno.SelectedValue = 1 Then
                    btnActualizar.Visible = True
                    btnVer.Visible = True
                    btnImprimir.Visible = True
                ElseIf ddlEstadoAlumno.SelectedValue = 2 Or ddlEstadoAlumno.SelectedValue = 3 Then
                    btnActualizar.Visible = False
                    btnVer.Visible = True
                    btnImprimir.Visible = True
                End If

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GridView1.PageIndex = e.NewPageIndex
            End If

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
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

            ImagenSorting(e.SortExpression)
        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = System.Web.UI.WebControls.DataControlRowType.Pager Then
                CrearBotonesPager(GridView1, e.Row, Me)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

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
            'listar()
            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

#End Region

#Region "Metodos del Gridview"

    ''' <summary>
    ''' Lista las fichas de atención ordenadas por un campo especifico
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     25/01/2011
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
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     25/01/2011
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
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        If GridView1.Rows.Count > 0 Then
            Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
            Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_NombreCompleto"), ImageButton)
            Dim _btnSorting_d2 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_NombreCompletoFamilia"), ImageButton)
            Dim _btnSorting_d3 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_CodigoAlumno"), ImageButton)

            If ViewState("Direccion") = "ASC" Then
                _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
                _btnSorting.ToolTip = "Descendente"
            ElseIf ViewState("Direccion") = "DESC" Then
                _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
                _btnSorting.ToolTip = "Ascendente"
            End If
        End If
    End Sub

    ''' <summary>
    ''' Agrega el índice de páginas al combo de páginación. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     25/01/2011
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
    ''' Fecha de Creación:     21/01/2011
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

#End Region

#Region "Mantenimiento Detalle Discapacidad"

#Region "Eventos"
    Protected Sub btn_Add_Discapacidad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoDiscapacidad") = True
        pnDiscapacidad.Show()
    End Sub

    Protected Sub popup_btnAgregar_Discapacidad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If ViewState("NuevoDiscapacidad") = False Then
                int_CodigoAccion = 6
                editarDiscapacidad()
            ElseIf ViewState("NuevoDiscapacidad") = True Then
                int_CodigoAccion = 7
                agregarDiscapacidad()
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_Discapacidad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalDiscapacidad()
    End Sub
#End Region
#Region "Métodos"
    ''' <summary>
    ''' ACtualiza el registro de Discapacidad seleccionado
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarDiscapacidad()

        If ddlTipoDiscapacidad.SelectedValue = 0 Then
            pnDiscapacidad.Show()
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            Exit Sub
        End If

        Dim int_CodigoOriginal As Integer = hidencodigoDiscapacidad.Value
        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        dt = ViewState("ListaDiscapacidad")
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelTipoDiscapAlum").ToString = int_CodigoOriginal Then
            ElseIf auxdr.Item("CodigoTipoDiscapacidad").ToString = ddlTipoDiscapacidad.SelectedValue Then
                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                pnDiscapacidad.Show()
                Exit Sub
            End If
        Next

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelTipoDiscapAlum").ToString = int_CodigoOriginal Then
                auxdr.Item("CodigoRelTipoDiscapAlum") = int_CodigoOriginal
                auxdr.Item("CodigoTipoDiscapacidad") = ddlTipoDiscapacidad.SelectedValue
                auxdr.Item("TipoDiscapacidad") = ddlTipoDiscapacidad.SelectedItem.ToString
                auxdr.Item("DescripcionDiscapacidad") = tbDescipcionDiscapacidad.Text
            End If
        Next

        ViewState("ListaDiscapacidad") = dt
        gvDetalleDiscapacidad.DataSource = dt
        gvDetalleDiscapacidad.DataBind()
        upDiscapacidad.Update()

    End Sub

    ''' <summary>
    ''' Agrega 1 Discapacidad al detalle de Discapacidades
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarDiscapacidad()
        If ddlTipoDiscapacidad.SelectedValue = 0 Then
            pnDiscapacidad.Show()
            MostrarSexyAlertBox("Debe seleccionar una discapacidad valido.", "Alert")
            Exit Sub
        End If

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaDiscapacidad") Is Nothing Then
            dt = New DataTable("ListaDiscapacidad")
            dt = Datos.agregarColumna(dt, "CodigoRelTipoDiscapAlum", "Integer")
            'dt = Datos.agregarColumna(dt, "CodigoAlumno", "String")
            dt = Datos.agregarColumna(dt, "CodigoTipoDiscapacidad", "Integer")
            dt = Datos.agregarColumna(dt, "TipoDiscapacidad", "String")
            dt = Datos.agregarColumna(dt, "DescripcionDiscapacidad", "String")
        Else
            dt = ViewState("ListaDiscapacidad")
        End If

        If dt.Rows.Count > 0 Then
            For Each auxdr As DataRow In dt.Rows
                If auxdr.Item("CodigoTipoDiscapacidad").ToString = ddlTipoDiscapacidad.SelectedValue Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    'ddlEnfermedad.SelectedValue = 0
                    'tbEdad.Text = 0
                    pnDiscapacidad.Show()
                    Exit Sub
                End If
                id_codigo_fila = auxdr.Item("CodigoRelTipoDiscapAlum").ToString()
            Next
        End If

        If boolIncremento = False Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("CodigoRelTipoDiscapAlum") = id_codigo_fila + 1
            dr.Item("CodigoTipoDiscapacidad") = ddlTipoDiscapacidad.SelectedValue
            dr.Item("TipoDiscapacidad") = ddlTipoDiscapacidad.SelectedItem.ToString
            dr.Item("DescripcionDiscapacidad") = tbDescipcionDiscapacidad.Text
            dt.Rows.Add(dr)
        End If

        ViewState("ListaDiscapacidad") = dt
        gvDetalleDiscapacidad.DataSource = dt
        gvDetalleDiscapacidad.DataBind()
        ddlTipoDiscapacidad.SelectedValue = 0
        tbDescipcionDiscapacidad.Text = ""
        upDiscapacidad.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 registro de Discapacidad de el detalle de Discapacidades
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarRelTipoDiscapacidades(ByVal int_CodigoRelTipoDiscapAlum As Integer)
        Dim dt As DataTable
        dt = ViewState("ListaDiscapacidad")
        For Each auxdr As DataRow In dt.Rows
            If Val(auxdr.Item("CodigoRelTipoDiscapAlum").ToString) = int_CodigoRelTipoDiscapAlum Then
                auxdr.Delete()
                Exit For
            End If
        Next
        dt.AcceptChanges()
        ViewState("ListaDiscapacidad") = dt
        gvDetalleDiscapacidad.DataSource = dt
        gvDetalleDiscapacidad.DataBind()
        upDiscapacidad.Update()
    End Sub

    ''' <summary>
    ''' Carga los datos del registro seleccionado en los controles del popup
    ''' </summary>
    ''' <param name="int_Codigo">Codigo de discapacidad del registro seleccionado</param>
    ''' <param name="int_CodigoTipoDiscapacidad">Codigo del Tipo de discapacidad del registro seleccionado</param>
    ''' <param name="int_DescripcionDiscapacidad">Descripcion de la discapacidad seleccionada</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarDiscapacidad(ByVal int_Codigo As Integer, ByVal int_CodigoTipoDiscapacidad As Integer, ByVal int_DescripcionDiscapacidad As String)
        ddlTipoDiscapacidad.SelectedValue = int_CodigoTipoDiscapacidad
        hidencodigoDiscapacidad.Value = int_Codigo
        tbDescipcionDiscapacidad.Text = int_DescripcionDiscapacidad
        pnDiscapacidad.Show()
    End Sub

    ''' <summary>
    ''' Cierra el modal Discapacidad
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalDiscapacidad()
        pnDiscapacidad.Hide()
        ddlTipoDiscapacidad.SelectedValue = 0
        tbDescipcionDiscapacidad.Text = ""
    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleDiscapacidad_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim CodigoRelTipoDiscapAlum As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    ViewState("NuevoDiscapacidad") = False
                    activarEditarDiscapacidad(CodigoRelTipoDiscapAlum, CType(row.FindControl("lblCodigoDiscapacidad"), Label).Text, CType(row.FindControl("lblDescripcionDiscapacidad"), Label).Text)

                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 3
                    eliminarRelTipoDiscapacidades(CodigoRelTipoDiscapAlum)

                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleDiscapacidad_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleDiscapacidad.RowDataBound
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")

            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                If ViewState("VerFicha") = True Then
                    btnEliminar.Visible = False
                    btnActualizar.Visible = False
                ElseIf ViewState("VerFicha") = False Then
                    btnEliminar.Visible = True
                    btnActualizar.Visible = True
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento Detalle Familiares"
#Region "Eventos"

    'Protected Sub btn_Add_Familiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    'btn_Add_Familiar.Attributes.Add("onclick", "abrirPopupParams('/SaintGeorgeOnline/Modulo_Matricula/Fpanel.aspx','Agregar')")
    'End Sub

    Protected Sub popup_btnCancelar_FF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalFamiliares()
    End Sub
#End Region
#Region "Métodos"

    ''' <summary>
    ''' Cierra el modal Discapacidad
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalFamiliares()
        pnl_ModalFichaFamiliares.Hide()
    End Sub
#End Region
#Region "Gridview"

    'Protected Sub gvFamiliares_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
    '    Dim int_CodigoAccion As Integer = 0
    '    Try
    '        If e.CommandName = "Visualizar" Then
    '            Dim CodigoFamiliar As Integer = CInt(e.CommandArgument.ToString)
    '            Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
    '            Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
    '            Dim int_CodigoFamilia As Integer = CodigoFamiliar

    '            Session("FichaFamiliar_Panel") = CodigoFamiliar

    '            'Iframe1.Attributes.Add("src", "Fpanel.aspx?CodigoFamiliar=" & CodigoFamiliar.ToString)

    '            ''''btn.Attributes.Add("onclick", "abrirPopupParams('/SaintGeorgeOnline/Modulo_Matricula/Fpanel.aspx','Actualizar')")

    '            'pnl_ModalFichaFamiliares.Show()
    '            'Iframe1.Visible = True
    '            'Iframe1.Attributes.Add("src", "Fpanel.aspx")
    '            'ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionFichaf_html();</script>", False)
    '            'ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", "MostrarImpresionFichaf_html(" & CodigoFamiliar & ");", True)
    '            'ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", "abrirPopupParams( /SaintGeorgeOnline/Modulo_Matricula/Fpanel.aspx, " & CodigoFamiliar & ",Actualizar);", True)
    '        End If
    '    Catch ex As Exception
    '        EnvioEmailError(int_CodigoAccion, ex.ToString)
    '    End Try
    'End Sub

    Protected Sub gvFamiliares_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim btnVer As ImageButton = e.Row.FindControl("btnVer")
                Dim int_CodigoFamiliar As Integer = CType(e.Row.FindControl("lblCodigoFamiliar"), Label).Text


                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

                btnVer.Attributes.Add("onclick", "abrirPopupParams('/SaintGeorgeOnline/Modulo_Matricula/Fpanel.aspx?CodigoFamiliar=" & int_CodigoFamiliar & "' ,'Actualizar')")

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
#End Region
#End Region

#Region "Manejo de Alertas - Emails"

    ''' <summary>
    ''' Recibe mensajes y los deriva a otro metodo que los visualizara cno animación de JQuery
    ''' </summary>
    ''' <param name="str_alertas">Mensaje que se quiere visualizar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarAlertas(ByVal str_alertas As String)

        MostrarSexyAlertBox(str_alertas, "Alert")

    End Sub

    ''' <summary>
    ''' Muestra un mensaje usando la animación de JQuery
    ''' </summary>
    ''' <param name="str_Mensaje">Mensaje que se quiere visualizar</param>
    ''' <param name="str_TipoMensaje">Tipo de Mensaje</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(2, 47, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region


#Region "Mantenimiento Detalle Retiro"

#Region "Eventos"
    Protected Sub btn_Add_Retiro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoRetiro") = True
        pnRetiro.Show()
    End Sub

    Protected Sub popup_btnAgregar_Retiro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim int_CodigoAccion As Integer = 0
        Try
            If ViewState("NuevoRetiro") = False Then
                int_CodigoAccion = 6
                editarRetiro()
            ElseIf ViewState("NuevoRetiro") = True Then
                int_CodigoAccion = 7
                agregarRetiro()
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub popup_btnCancelar_Retiro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalRetiro()
    End Sub
#End Region
#Region "Métodos"
    ''' <summary>
    ''' ACtualiza el registro de Discapacidad seleccionado
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarRetiro()

        If ddlAnioRetiro.SelectedValue = 0 Then
            pnRetiro.Show()
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            Exit Sub
        End If

        If ddlMotivoRetiro.SelectedValue = 0 Then
            pnRetiro.Show()
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            Exit Sub
        End If

        Dim int_CodigoOriginal As Integer = hidencodigoRetiro.Value
        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        dt = ViewState("ListaRetiro")
        'For Each auxdr As DataRow In dt.Rows
        '    If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then
        '    ElseIf auxdr.Item("CodigoMotivo").ToString = ddlMotivoRetiro.SelectedValue Then
        '        MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
        '        pnRetiro.Show()
        '        Exit Sub
        '    End If
        'Next

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then
                auxdr.Item("CodigoRelacion") = int_CodigoOriginal
                auxdr.Item("CodigoMotivo") = ddlMotivoRetiro.SelectedValue
                auxdr.Item("CodigoAnio") = ddlAnioRetiro.SelectedValue
                auxdr.Item("CodigoColegioTraslado") = ddlColegioTraslado.SelectedValue
                auxdr.Item("Anio") = ddlAnioRetiro.SelectedItem.ToString
                auxdr.Item("Motivo") = ddlMotivoRetiro.SelectedItem.ToString
                auxdr.Item("Colegio") = ddlColegioTraslado.SelectedItem.ToString
                auxdr.Item("FechaRegistroRetiro") = tbFechaRegistroRetiro.Text
                auxdr.Item("Observacion") = tbObservacionRetiro.Text
                auxdr.Item("CodigoModular") = tbCodigoModular.Text
            End If
        Next

        ViewState("ListaRetiro") = dt
        gvDetalleRetiro.DataSource = dt
        gvDetalleRetiro.DataBind()
        upDatosRetiro.Update()

    End Sub

    ''' <summary>
    ''' Agrega 1 Discapacidad al detalle de Discapacidades
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarRetiro()
        If ddlAnioRetiro.SelectedValue = 0 Then
            pnRetiro.Show()
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            Exit Sub
        End If

        If ddlMotivoRetiro.SelectedValue = 0 Then
            pnRetiro.Show()
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            Exit Sub
        End If


        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaRetiro") Is Nothing Then
            dt = New DataTable("ListaRetiro")
            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            'dt = Datos.agregarColumna(dt, "CodigoAlumno", "String")
            dt = Datos.agregarColumna(dt, "CodigoAnio", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoMotivo", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoColegioTraslado", "Integer")

            dt = Datos.agregarColumna(dt, "Anio", "String")
            dt = Datos.agregarColumna(dt, "FechaRegistroRetiro", "String")
            dt = Datos.agregarColumna(dt, "Motivo", "String")
            dt = Datos.agregarColumna(dt, "Observacion", "String")
            dt = Datos.agregarColumna(dt, "Colegio", "String")
            dt = Datos.agregarColumna(dt, "CodigoModular", "String")
        Else
            dt = ViewState("ListaRetiro")
        End If

        If dt.Rows.Count > 0 Then
            For Each auxdr As DataRow In dt.Rows
                'If auxdr.Item("CodigoTipoDiscapacidad").ToString = ddlTipoDiscapacidad.SelectedValue Then
                '    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                '    'ddlEnfermedad.SelectedValue = 0
                '    'tbEdad.Text = 0
                '    pnDiscapacidad.Show()
                '    Exit Sub
                'End If
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next
        End If

        If boolIncremento = False Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("CodigoRelacion") = id_codigo_fila + 1
            dr.Item("CodigoAnio") = ddlAnioRetiro.SelectedValue
            dr.Item("CodigoMotivo") = ddlMotivoRetiro.SelectedValue
            dr.Item("CodigoColegioTraslado") = ddlColegioTraslado.SelectedValue
            dr.Item("Anio") = ddlAnioRetiro.SelectedItem.ToString
            dr.Item("FechaRegistroRetiro") = tbFechaRegistroRetiro.Text
            dr.Item("Motivo") = ddlMotivoRetiro.SelectedItem.ToString
            dr.Item("Observacion") = tbObservacionRetiro.Text
            dr.Item("Colegio") = ddlColegioTraslado.SelectedItem.ToString
            dr.Item("CodigoModular") = tbCodigoModular.Text

            dt.Rows.Add(dr)
        End If

        ViewState("ListaRetiro") = dt
        gvDetalleRetiro.DataSource = dt
        gvDetalleRetiro.DataBind()
        ddlAnioRetiro.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlMotivoRetiro.SelectedValue = 0
        ddlColegioTraslado.SelectedValue = 0
        tbFechaRegistroRetiro.Text = Now.Date
        tbObservacionRetiro.Text = ""
        tbCodigoModular.Text = ""

        upDatosRetiro.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 registro de Discapacidad de el detalle de Discapacidades
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarRetiro(ByVal int_CodigoRelRetiro As Integer)
        Dim dt As DataTable
        dt = ViewState("ListaRetiro")
        For Each auxdr As DataRow In dt.Rows
            If Val(auxdr.Item("CodigoRelacion").ToString) = int_CodigoRelRetiro Then
                auxdr.Delete()
                Exit For
            End If
        Next
        dt.AcceptChanges()
        ViewState("ListaRetiro") = dt
        gvDetalleRetiro.DataSource = dt
        gvDetalleRetiro.DataBind()
        upDatosRetiro.Update()
    End Sub

    ''' <summary>
    ''' Carga los datos del registro seleccionado en los controles del popup
    ''' </summary>
    ''' <param name="int_Codigo">Codigo de discapacidad del registro seleccionado</param>
    ''' <param name="int_CodigoTipoDiscapacidad">Codigo del Tipo de discapacidad del registro seleccionado</param>
    ''' <param name="int_DescripcionDiscapacidad">Descripcion de la discapacidad seleccionada</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarRetiro(ByVal int_Codigo As Integer, ByVal int_CodigoMotivo As Integer, ByVal int_CodigoAnio As Integer, ByVal int_ColegioTraslado As Integer, _
                                    ByVal str_Anio As String, ByVal str_FechaRegistroRetiro As String, ByVal str_MotivoRetiro As String, ByVal str_Observacion As String, ByVal str_Colegio As String, ByVal str_CodigoModular As String)
        ddlAnioRetiro.SelectedValue = int_CodigoAnio
        hidencodigoRetiro.Value = int_Codigo
        ddlMotivoRetiro.SelectedValue = int_CodigoMotivo
        ddlColegioTraslado.SelectedValue = int_ColegioTraslado
        tbFechaRegistroRetiro.Text = str_FechaRegistroRetiro
        tbCodigoModular.Text = str_CodigoModular
        tbObservacionRetiro.Text = str_Observacion
        pnRetiro.Show()
    End Sub

    ''' <summary>
    ''' Cierra el modal Discapacidad
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalRetiro()
        pnRetiro.Hide()
        'ddlTipoDiscapacidad.SelectedValue = 0
        'tbDescipcionDiscapacidad.Text = ""
    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleRetiro_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim CodigoRelRetiro As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    ViewState("NuevoRetiro") = False
                    activarEditarRetiro(CodigoRelRetiro, CType(row.FindControl("lblCodigoMotivo_grilla"), Label).Text, CType(row.FindControl("lblCodigoAnio_grilla"), Label).Text, _
                                        CType(row.FindControl("lblCodigoColegioTraslado_grilla"), Label).Text, CType(row.FindControl("lblAnio_grilla"), Label).Text, CType(row.FindControl("lblFechaRegistroRetiro_grilla"), Label).Text, _
                                        CType(row.FindControl("lblMotivoRetiro_grilla"), Label).Text, CType(row.FindControl("lblObservacion_grilla"), Label).Text, CType(row.FindControl("lblColegio_grilla"), Label).Text, CType(row.FindControl("lblCodigoModular_grilla"), Label).Text)


                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 3
                    eliminarRetiro(CodigoRelRetiro)

                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleRetiro_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
                Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")

                If e.Row.RowType = DataControlRowType.DataRow Then
                    e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                    e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                    btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                    If ViewState("VerFicha") = True Then
                        btnEliminar.Visible = False
                        btnActualizar.Visible = False
                    ElseIf ViewState("VerFicha") = False Then
                        btnEliminar.Visible = True
                        btnActualizar.Visible = True
                    End If
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento Detalle Procedencia"

#Region "Eventos"
    Protected Sub btn_Add_Procedencia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoProcedencia") = True
        pnProcedencia.Show()
    End Sub

    Protected Sub popup_btnAgregar_Procedencia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim int_CodigoAccion As Integer = 0
        Try
            If ViewState("NuevoProcedencia") = False Then
                int_CodigoAccion = 6
                editarProcedencia()
            ElseIf ViewState("NuevoProcedencia") = True Then
                int_CodigoAccion = 7
                agregarProcedencia()
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub popup_btnCancelar_Procedencia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalProcedencia()
    End Sub
#End Region
#Region "Métodos"
    ''' <summary>
    ''' ACtualiza el registro de Discapacidad seleccionado
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarProcedencia()

        If ddlAnioProcedencia.SelectedValue = 0 Then
            pnProcedencia.Show()
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            Exit Sub
        End If

        If ddlColegioProcedencia.SelectedValue = 0 Then
            pnProcedencia.Show()
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            Exit Sub
        End If

        Dim int_CodigoOriginal As Integer = hidencodigoProcedencia.Value
        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        dt = ViewState("ListaProcedencia")
        'For Each auxdr As DataRow In dt.Rows
        '    If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then
        '    ElseIf auxdr.Item("CodigoMotivo").ToString = ddlMotivoRetiro.SelectedValue Then
        '        MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
        '        pnRetiro.Show()
        '        Exit Sub
        '    End If
        'Next

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then
                auxdr.Item("CodigoRelacion") = int_CodigoOriginal
                'auxdr.Item("CodigoMotivo") = ddlMotivoRetiro.SelectedValue
                auxdr.Item("CodigoAnio") = ddlAnioProcedencia.SelectedValue
                auxdr.Item("CodigoColegioProcedencia") = ddlColegioProcedencia.SelectedValue
                auxdr.Item("Anio") = ddlAnioProcedencia.SelectedItem.ToString
                'auxdr.Item("Motivo") = ddlMotivoRetiro.SelectedItem.ToString
                auxdr.Item("ColegioProcedencia") = ddlColegioProcedencia.SelectedItem.ToString
                'auxdr.Item("FechaRegistroRetiro") = tbFechaRegistroRetiro.Text
                'auxdr.Item("Observacion") = tbObservacionRetiro.Text
                'auxdr.Item("CodigoModular") = tbCodigoModular.Text
            End If
        Next

        ViewState("ListaProcedencia") = dt
        gvDetalleProcedencia.DataSource = dt
        gvDetalleProcedencia.DataBind()
        upDatosProcedencia.Update()

    End Sub

    ''' <summary>
    ''' Agrega 1 Discapacidad al detalle de Discapacidades
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarProcedencia()
        If ddlAnioProcedencia.SelectedValue = 0 Then
            pnProcedencia.Show()
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            Exit Sub
        End If

        If ddlColegioProcedencia.SelectedValue = 0 Then
            pnProcedencia.Show()
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            Exit Sub
        End If


        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaProcedencia") Is Nothing Then
            dt = New DataTable("ListaProcedencia")
            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            'dt = Datos.agregarColumna(dt, "CodigoAlumno", "String")
            dt = Datos.agregarColumna(dt, "CodigoAnio", "Integer")
            'dt = Datos.agregarColumna(dt, "CodigoMotivo", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoColegioProcedencia", "Integer")

            dt = Datos.agregarColumna(dt, "Anio", "String")
            'dt = Datos.agregarColumna(dt, "FechaRegistroRetiro", "String")
            'dt = Datos.agregarColumna(dt, "Motivo", "String")
            'dt = Datos.agregarColumna(dt, "Observacion", "String")
            dt = Datos.agregarColumna(dt, "ColegioProcedencia", "String")
            'dt = Datos.agregarColumna(dt, "CodigoModular", "String")
        Else
            dt = ViewState("ListaProcedencia")
        End If

        If dt.Rows.Count > 0 Then
            For Each auxdr As DataRow In dt.Rows
                'If auxdr.Item("CodigoTipoDiscapacidad").ToString = ddlTipoDiscapacidad.SelectedValue Then
                '    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                '    'ddlEnfermedad.SelectedValue = 0
                '    'tbEdad.Text = 0
                '    pnDiscapacidad.Show()
                '    Exit Sub
                'End If
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next
        End If

        If boolIncremento = False Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("CodigoRelacion") = id_codigo_fila + 1
            dr.Item("CodigoAnio") = ddlAnioProcedencia.SelectedValue
            'dr.Item("CodigoMotivo") = ddlMotivoRetiro.SelectedValue
            dr.Item("CodigoColegioProcedencia") = ddlColegioProcedencia.SelectedValue
            dr.Item("Anio") = ddlAnioProcedencia.SelectedItem.ToString
            'dr.Item("FechaRegistroRetiro") = tbFechaRegistroRetiro.Text
            'dr.Item("Motivo") = ddlMotivoRetiro.SelectedItem.ToString
            'dr.Item("Observacion") = tbObservacionRetiro.Text
            dr.Item("ColegioProcedencia") = ddlColegioProcedencia.SelectedItem.ToString
            'dr.Item("CodigoModular") = tbCodigoModular.Text

            dt.Rows.Add(dr)
        End If

        ViewState("ListaProcedencia") = dt
        gvDetalleProcedencia.DataSource = dt
        gvDetalleProcedencia.DataBind()
        ddlAnioProcedencia.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlColegioProcedencia.SelectedValue = 0
        'tbFechaRegistroRetiro.Text = Now.Date
        'tbObservacionRetiro.Text = ""
        'tbCodigoModular.Text = ""

        upDatosProcedencia.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 registro de Procedencia de el detalle de Procedencia
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarProcedencia(ByVal int_CodigoRelProcedencia As Integer)
        Dim dt As DataTable
        dt = ViewState("ListaProcedencia")
        For Each auxdr As DataRow In dt.Rows
            If Val(auxdr.Item("CodigoRelacion").ToString) = int_CodigoRelProcedencia Then
                auxdr.Delete()
                Exit For
            End If
        Next
        dt.AcceptChanges()
        ViewState("ListaProcedencia") = dt
        gvDetalleProcedencia.DataSource = dt
        gvDetalleProcedencia.DataBind()
        upDatosProcedencia.Update()
    End Sub

    ''' <summary>
    ''' Carga los datos del registro seleccionado en los controles del popup
    ''' </summary>
    ''' <param name="int_Codigo">Codigo de discapacidad del registro seleccionado</param>
    ''' <param name="int_CodigoTipoDiscapacidad">Codigo del Tipo de discapacidad del registro seleccionado</param>
    ''' <param name="int_DescripcionDiscapacidad">Descripcion de la discapacidad seleccionada</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarProcedencia(ByVal int_Codigo As Integer, ByVal int_CodigoAnio As Integer, ByVal int_ColegioProcedencia As Integer)
        ddlAnioProcedencia.SelectedValue = int_CodigoAnio
        hidencodigoProcedencia.Value = int_Codigo
        'ddlMotivoRetiro.SelectedValue = int_CodigoMotivo
        ddlColegioProcedencia.SelectedValue = int_ColegioProcedencia
        'tbFechaRegistroRetiro.Text = str_FechaRegistroRetiro
        'tbCodigoModular.Text = str_CodigoModular
        'tbObservacionRetiro.Text = str_Observacion
        pnProcedencia.Show()
    End Sub

    ''' <summary>
    ''' Cierra el modal Discapacidad
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalProcedencia()
        pnProcedencia.Hide()
        'ddlTipoDiscapacidad.SelectedValue = 0
        'tbDescipcionDiscapacidad.Text = ""
    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleProcedencia_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim CodigoRelProcedencia As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    ViewState("NuevoProcedencia") = False
                    activarEditarProcedencia(CodigoRelProcedencia, CType(row.FindControl("lblCodigoAnioProcedencia_grilla"), Label).Text, CType(row.FindControl("lblCodigoColegioProcedencia_grilla"), Label).Text)


                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 3
                    eliminarProcedencia(CodigoRelProcedencia)

                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleProcedencia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
                Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")

                If e.Row.RowType = DataControlRowType.DataRow Then
                    e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                    e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                    btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                    If ViewState("VerFicha") = True Then
                        btnEliminar.Visible = False
                        btnActualizar.Visible = False
                    ElseIf ViewState("VerFicha") = False Then
                        btnEliminar.Visible = True
                        btnActualizar.Visible = True
                    End If
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region


    Public Function ExportarReporteFichaMedica_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try
            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaFichaAlumnoHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaFichaAlumno(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)
        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try
        Return fileReaderPlantilla

    End Function
    Private Function LlenarPlantillaFichaAlumno(ByVal Plantilla As String, ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String


        Dim obj_BL_FichaAlumno As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim str_codigo As String = hd_Codigo.Value

        Dim ds_Lista As DataSet = obj_BL_FichaAlumno.FUN_GET_Alumnos(str_codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)

        Dim sb_Ficha As New StringBuilder

        sb_Ficha.Append("<table border='0' align='center' cellpadding='0' cellspacing='0' style='width:800px; font-family:Arial,Helvetica,sans-serif; font-size:10pt;'>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle'>")
        sb_Ficha.Append("<img alt='' src='http://web.stgeorgescollege.edu.pe/Temporales_SanGeorgeOnline/Imagenes/Imagenes_Logos/St_Georges4.jpg' style='width: 156px; height: 50px' />")
        sb_Ficha.Append("</td></tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='center' valign='middle' class='titulo'>Ficha del Alumno</td></tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subtitulo'>Situación Actual</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Nombre Completo:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Estado/Periodo:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("estadoAnioActualAlumno").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'></td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("ENSnGS").ToString & "</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subtitulo'>Datos Personales</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Código:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Código Educando:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("CodigoEducando").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Apellido Paterno:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Apellido Materno:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Nombre:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Sexo:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Sexo").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Tipo Documento:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("TipoDocIdentidad").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Nro. Documento:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString & "</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subtitulo'>Datos de Nacimiento</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Nacimiento Registrado:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("NacimientoRegistrado").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Fecha:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Pais:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Pais").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Departamento:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Departamento").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Provincia:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Provincia").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Distrito:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Distrito").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")

        Dim str_nac1 As String = ""
        Dim str_nac2 As String = ""
        If ds_Lista.Tables(1).Rows.Count > 1 Then ' 1era y 2da nacionalidad 
            str_nac1 = ds_Lista.Tables(1).Rows(0).Item("Nacionalidad").ToString()
            str_nac2 = ds_Lista.Tables(1).Rows(1).Item("Nacionalidad").ToString()
        ElseIf ds_Lista.Tables(1).Rows.Count > 0 Then ' 1era nacionalidad
            str_nac1 = ds_Lista.Tables(1).Rows(0).Item("Nacionalidad").ToString()
            str_nac2 = ""
        End If

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>1era Nacionalidad:</td>")
        sb_Ficha.Append("<td class='celda2'>" & str_nac1 & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>2da Nacionalidad:</td>")
        sb_Ficha.Append("<td class='celda2'>" & str_nac2 & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subtitulo'>Datos Adicionales</td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subsubtitulo'>Otros Datos</td></tr>")

        Dim str_len1 As String = ""
        Dim str_len2 As String = ""
        If ds_Lista.Tables(2).Rows.Count > 1 Then ' 1era y 2da Lengua
            If ds_Lista.Tables(2).Rows(0).Item("LenguaMaterna") = 1 Then
                str_len1 = ds_Lista.Tables(2).Rows(0).Item("idioma").ToString()
                str_len2 = ds_Lista.Tables(2).Rows(1).Item("idioma").ToString()
            ElseIf ds_Lista.Tables(2).Rows(1).Item("LenguaMaterna") = 1 Then
                str_len1 = ds_Lista.Tables(2).Rows(1).Item("idioma").ToString()
                str_len2 = ds_Lista.Tables(2).Rows(0).Item("idioma").ToString()
            Else
                str_len1 = ds_Lista.Tables(2).Rows(0).Item("idioma").ToString()
                str_len2 = ds_Lista.Tables(2).Rows(1).Item("idioma").ToString()
            End If
        ElseIf ds_Lista.Tables(2).Rows.Count > 0 Then ' 1era Lengua
            str_len1 = ds_Lista.Tables(2).Rows(0).Item("idioma").ToString()
            str_len2 = ""
        End If

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>1era Lengua Materna:</td>")
        sb_Ficha.Append("<td class='celda2'>" & str_len1 & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>2da Lengua Materna:</td>")
        sb_Ficha.Append("<td class='celda2'>" & str_len2 & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Estado Civil:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Estadocivil").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Cantidad de Hermanos:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("CantidadHermanos").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Estado Civil:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Posición entre los Hermanos:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("PosicionEntreHermanos").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Correo electrónico:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Correo electrónico Institucional:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("CorreoInstitucional").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Celular:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Celular").ToString & "</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subsubtitulo'>Datos Religiosos</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>¿Profesa alguna Religión?:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("DescProfesaReligion").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Religión que profesa:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Religion").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>¿Ha sido bautizado?:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("DescBautizo").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1_sangria' align='left' valign='middle'>Lugar:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("BautizoLugar").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1_sangria' align='left' valign='middle'>Año:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("BautizoAnio").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>¿Ha dado la primera comunión?:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("DescPrimeraComunion").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1_sangria' align='left' valign='middle'>Lugar:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionLugar").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1_sangria' align='left' valign='middle'>Año:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionAnio").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>¿Se ha confirmado?:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("DescConfirmacion").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1_sangria' align='left' valign='middle'>Lugar:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("ConfirmacionLugar").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1_sangria' align='left' valign='middle'>Año:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("ComfirmacionAnio").ToString & "</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subsubtitulo'>Datos Retiro</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:800px; height:20px;' colspan='2' align='left' valign='middle'>")
        sb_Ficha.Append("<table border='1' align='center' cellpadding='0' cellspacing='0' style='width:800px; font-family:Arial,Helvetica,sans-serif; font-size:10pt;'>")

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:50px; height:20px;' align='center' valign='middle' class='headers'>Año</td>")
        sb_Ficha.Append("<td style='width:100px; height:20px;' align='center' valign='middle' class='headers'>Fec Registro</td>")
        sb_Ficha.Append("<td style='width:190px; height:20px;' align='center' valign='middle' class='headers'>Motivo</td>")
        sb_Ficha.Append("<td style='width:190px; height:20px;' align='center' valign='middle' class='headers'>Observación</td>")
        sb_Ficha.Append("<td style='width:180px; height:20px;' align='center' valign='middle' class='headers'>Colegio Procedencia</td>")
        sb_Ficha.Append("<td style='width:90px; height:20px;' align='center' valign='middle' class='headers'>Código Modular</td>")
        sb_Ficha.Append("</tr>")

        If ds_Lista.Tables(6).Rows.Count > 0 Then
            If ds_Lista.Tables(6).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(6).Rows
                    sb_Ficha.Append("<tr>")
                    sb_Ficha.Append("<td style='width:50px; height:20px;' align='center' valign='middle'>" & dr.Item("Anio").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:100px; height:20px;' align='center' valign='middle'>" & dr.Item("FechaRegistroRetiro").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:190px; height:20px;' align='left' valign='middle'>" & dr.Item("Motivo").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:190px; height:20px;' align='left' valign='middle'>" & dr.Item("Observacion").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:180px; height:20px;' align='left' valign='middle'>" & dr.Item("Colegio").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:90px; height:20px;' align='left' valign='middle'>" & dr.Item("CodigoModular").ToString & "</td>")
                    sb_Ficha.Append("</tr>")
                Next
            End If
        End If

        sb_Ficha.Append("</table>")
        sb_Ficha.Append("</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subsubtitulo'>Datos Procedencia</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:800px; height:20px;' colspan='2' align='left' valign='middle'>")
        sb_Ficha.Append("<table border='1' align='left' cellpadding='0' cellspacing='0' style='width:320px; font-family:Arial,Helvetica,sans-serif; font-size:10pt;'>")

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:50px; height:20px;' align='center' valign='middle' class='headers'>Año</td>")
        sb_Ficha.Append("<td style='width:180px; height:20px;' align='center' valign='middle' class='headers'>Colegio</td>")
        sb_Ficha.Append("<td style='width:90px; height:20px;' align='center' valign='middle' class='headers'>Código Modular</td>")
        sb_Ficha.Append("</tr>")

        If ds_Lista.Tables(7).Rows.Count > 0 Then
            If ds_Lista.Tables(7).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(7).Rows
                    sb_Ficha.Append("<tr>")
                    sb_Ficha.Append("<td style='width:50px; height:20px;' align='center' valign='middle'>" & dr.Item("Anio").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:180px; height:20px;' align='left' valign='middle'>" & dr.Item("ColegioProcedencia").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:90px; height:20px;' align='left' valign='middle'></td>")
                    sb_Ficha.Append("</tr>")
                Next
            End If
        End If

        sb_Ficha.Append("</table>")
        sb_Ficha.Append("</td>")
        sb_Ficha.Append("</tr>")




        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subsubtitulo'>Datos Matrículas</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:800px; height:20px;' colspan='2' align='left' valign='middle'>")
        sb_Ficha.Append("<table border='1' align='left' cellpadding='0' cellspacing='0' style='width:340px; font-family:Arial,Helvetica,sans-serif; font-size:10pt;'>")

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:70px; height:20px;' align='center' valign='middle' class='headers'>Año</td>")
        sb_Ficha.Append("<td style='width:70px; height:20px;' align='center' valign='middle' class='headers'>Oficial</td>")
        sb_Ficha.Append("<td style='width:100px; height:20px;' align='center' valign='middle' class='headers'>Grado</td>")
        sb_Ficha.Append("<td style='width:100px; height:20px;' align='center' valign='middle' class='headers'>Aula</td>")
        sb_Ficha.Append("</tr>")

        If ds_Lista.Tables(11).Rows.Count > 0 Then
            For Each dr As DataRow In ds_Lista.Tables(11).Rows
                sb_Ficha.Append("<tr>")
                sb_Ficha.Append("<td style='width:70px; height:20px;' align='center' valign='middle'>" & dr.Item("Anio").ToString & "</td>")
                sb_Ficha.Append("<td style='width:70px; height:20px;' align='center' valign='middle'>" & dr.Item("oficial").ToString & "</td>")
                sb_Ficha.Append("<td style='width:100px; height:20px;' align='center' valign='middle'>" & dr.Item("grado").ToString & "</td>")
                sb_Ficha.Append("<td style='width:100px; height:20px;' align='center' valign='middle'>" & dr.Item("aula").ToString & "</td>")
                sb_Ficha.Append("</tr>")
            Next
        End If

        sb_Ficha.Append("</table>")
        sb_Ficha.Append("</td>")
        sb_Ficha.Append("</tr>")






        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subtitulo'>Datos Familiares</td></tr>")

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:800px; height:20px;' colspan='2' align='left' valign='middle'>")
        sb_Ficha.Append("<table border='1' align='center' cellpadding='0' cellspacing='0' style='width:800px; font-family:Arial,Helvetica,sans-serif; font-size:10pt;'>")

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:250px; height:20px;' align='center' valign='middle' class='headers'>Nombre Completo</td>")
        sb_Ficha.Append("<td style='width:80px; height:20px;' align='center' valign='middle' class='headers'>Parentesco</td>")
        sb_Ficha.Append("<td style='width:100px; height:20px;' align='center' valign='middle' class='headers'>Telf. Casa</td>")
        sb_Ficha.Append("<td style='width:100px; height:20px;' align='center' valign='middle' class='headers'>Celular</td>")
        sb_Ficha.Append("<td style='width:70px; height:20px;' align='center' valign='middle' class='headers'>Apoderado</td>")
        sb_Ficha.Append("<td style='width:200px; height:20px;' align='center' valign='middle' class='headers'>Email</td>")
        sb_Ficha.Append("</tr>")

        If ds_Lista.Tables(4).Rows.Count > 0 Then
            If ds_Lista.Tables(4).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(4).Rows
                    sb_Ficha.Append("<tr>")
                    sb_Ficha.Append("<td style='width:250px; height:20px;' align='left' valign='middle'>" & dr.Item("NombreCompleto").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:80px; height:20px;' align='center' valign='middle'>" & dr.Item("Parentesco").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:100px; height:20px;' align='left' valign='middle'>" & dr.Item("TelfCasa").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:100px; height:20px;' align='left' valign='middle'>" & dr.Item("Celular").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:70px; height:20px;' align='center' valign='middle'>" & dr.Item("Apoderado").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:200px; height:20px;' align='left' valign='middle'>" & dr.Item("Email").ToString & "</td>")
                    sb_Ficha.Append("</tr>")
                Next
            End If
        End If

        sb_Ficha.Append("</table>")
        sb_Ficha.Append("</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subtitulo'>Datos de Domicilio</td></tr>")

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Departamento:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("DomicilioDepartamento").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Provincia:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("DomicilioProvincia").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Distrito:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("DomicilioDistrito").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Urbanización:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Urbanizacion").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Dirección:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Direccion").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Referencia domiciliaria:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomiciliaria").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Teléfono:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("TelefonoCasa").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>¿Tiene acceso a internet?:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("DescAccesoInternet").ToString & "</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subtitulo'>Datos Médicos</td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subsubtitulo'>Datos de Seguro Médico</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:800px; height:20px;' colspan='2' align='left' valign='middle'>")
        sb_Ficha.Append("<table border='1' align='center' cellpadding='0' cellspacing='0' style='width:800px; font-family:Arial,Helvetica,sans-serif; font-size:10pt;'>")

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:50px; height:20px;' align='center' valign='middle' class='headers'>Año</td>")
        sb_Ficha.Append("<td style='width:140px; height:20px;' align='center' valign='middle' class='headers'>Tipo</td>")
        sb_Ficha.Append("<td style='width:200px; height:20px;' align='center' valign='middle' class='headers'>Compañia</td>")
        sb_Ficha.Append("<td style='width:110px; height:20px;' align='center' valign='middle' class='headers'>Número Poliza</td>")
        sb_Ficha.Append("<td style='width:200px; height:20px;' align='center' valign='middle' class='headers'>Clinicas</td>")
        sb_Ficha.Append("<td style='width:100px; height:20px;' align='center' valign='middle' class='headers'>Vigencia</td>")
        sb_Ficha.Append("</tr>")

        If ds_Lista.Tables(5).Rows.Count > 0 Then
            If ds_Lista.Tables(5).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(5).Rows
                    sb_Ficha.Append("<tr>")
                    sb_Ficha.Append("<td style='width:50px; height:20px;' align='center' valign='middle'>" & dr.Item("AnioMatricula").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:140px; height:20px;' align='left' valign='middle'>" & dr.Item("Tipo").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:200px; height:20px;' align='left' valign='middle'>" & dr.Item("Compania").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:110px; height:20px;' align='left' valign='middle'>" & dr.Item("NumeroPoliza").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:200px; height:20px;' align='left' valign='middle'>" & dr.Item("Clinica").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:100px; height:20px;' align='left' valign='middle'>" & dr.Item("VigenciaTime").ToString & "</td>")
                    sb_Ficha.Append("</tr>")
                Next
            End If
        End If

        sb_Ficha.Append("</table>")
        sb_Ficha.Append("</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subsubtitulo'>Aviso en caso de emergencia y no poder ubicar a familiares registrados</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Nombre Completo:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("NombreContactoAvisoEmergencia").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Telefono Casa:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("TelfCasaContactoAvisoEmergencia").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Telefono Oficina:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("TelfOficinaContactoAvisoEmergencia").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Telefono Móvil:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("CellContactoAvisoEmergencia").ToString & "</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subsubtitulo'>Datos Especiales</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Experiencias Traumaticas:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("ExperienciasTraumaticasDescripcion").ToString & "</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:800px; height:20px;' colspan='2' align='left' valign='middle'>")
        sb_Ficha.Append("<table border='1' align='left' cellpadding='0' cellspacing='0' style='width:500px; font-family:Arial,Helvetica,sans-serif; font-size:10pt;'>")

        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td style='width:150px; height:20px;' align='center' valign='middle' class='headers'>Tipo Discapacidad</td>")
        sb_Ficha.Append("<td style='width:350px; height:20px;' align='center' valign='middle' class='headers'>Descripción</td>")
        sb_Ficha.Append("</tr>")

        If ds_Lista.Tables(3).Rows.Count > 0 Then
            If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(3).Rows
                    sb_Ficha.Append("<tr>")
                    sb_Ficha.Append("<td style='width:150px; height:20px;' align='left' valign='middle'>" & dr.Item("TipoDiscapacidad").ToString & "</td>")
                    sb_Ficha.Append("<td style='width:350px; height:20px;' align='left' valign='middle'>" & dr.Item("DescripcionDiscapacidad").ToString & "</td>")
                    sb_Ficha.Append("</tr>")
                Next
            End If
        End If

        sb_Ficha.Append("</table>")
        sb_Ficha.Append("</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")
        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2' align='left' valign='middle' class='subtitulo'>Datos de Facturación</td></tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Emitir factura:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("EmitirFacturaStr").ToString & "</td>")
        sb_Ficha.Append("</tr>")
        sb_Ficha.Append("<tr>")
        sb_Ficha.Append("<td class='celda1'>Empresa:</td>")
        sb_Ficha.Append("<td class='celda2'>" & ds_Lista.Tables(0).Rows(0).Item("Empresa").ToString & "</td>")
        sb_Ficha.Append("</tr>")

        sb_Ficha.Append("<tr><td style='width:800px; height:20px;' colspan='2'><br /></td></tr>")

        sb_Ficha.Append("</table>")

        Plantilla = sb_Ficha.ToString
        Return Plantilla

    End Function
    Protected Sub gvDetalleMatriculas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.RowType = DataControlRowType.DataRow Then
                    e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                    e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub


End Class
