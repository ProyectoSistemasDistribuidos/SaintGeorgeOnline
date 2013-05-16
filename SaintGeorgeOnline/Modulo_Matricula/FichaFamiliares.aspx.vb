Imports System.Security.Cryptography
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Módulo de Registro de las Atenciones en Enfermeria
''' </summary>
''' <remarks>
''' Código del Modulo:    2
''' Código de la Opción:  48
''' </remarks>

Partial Class Modulo_Matricula_FichaFamiliares
    Inherits System.Web.UI.Page

    'updatew 17/01/1986

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Bloque_DatosPersonales.Visible = False
        'Bloque_DatosNacimiento.Visible = False
        'Bloque_DatosAdicionales.Visible = False
        'Bloque_DatosDomicilio.Visible = False
        'Bloque_DatosLaborales.Visible = False
        'Bloque_DatosEstudios.Visible = False

        Try
            Me.Master.MostrarTitulo("Ficha de Familiar")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()

                ViewState("SortExpression") = "NombreCompleto"
                ViewState("Direccion") = "ASC"
                cargarOpcionesMantenimiento()
                cargarCombosFamiliarAlumno()
                btnFichaCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                'listarFichas()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click()
        Try
            listarFichas()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        limpiarFiltros()
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        LimpiarCamposFicha()
        VerRegistro("Inserción", 1)
        DesactivarCampos()

    End Sub

    Protected Sub ddlBuscarFamiliarNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarFamiliarSubNivel)
            limpiarCombos(ddlBuscarFamiliarGrado)
            limpiarCombos(ddlBuscarFamiliarAula)
            cargarComboFamiliarAlumnoSubNivel()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarFamiliarSubNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarFamiliarGrado)
            limpiarCombos(ddlBuscarFamiliarAula)
            cargarComboFamiliarAlumnoGrado()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarFamiliarGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarFamiliarAula)
            cargarComboFamiliarAlumnoAulas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub


    Protected Sub rbvive_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        verificarFechaDefuncion()
    End Sub

    Protected Sub rbAdicionalesProfesaReligion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        verificarProfesaReligion()
    End Sub

    Protected Sub ddlSituacionLaboral_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        verificarSituacionLaboral()
    End Sub

    Protected Sub rbEstudiosExAlumno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        verificarExAlumno()
    End Sub

    Protected Sub ddlDomicilioPais_SelectedIndexChanged()
        verificarPais(1)
    End Sub

    Protected Sub ddlDomicilioDepartamento_SelectedIndexChanged()
        Try
            limpiarCombos(ddlDomicilioDistrito)
            cargarComboProvincia(1)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlDomicilioProvincia_SelectedIndexChanged()
        Try
            cargarComboDistrito(1)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlTrabajoPais_SelectedIndexChanged()
        verificarPais(2)
    End Sub

    Protected Sub ddlTrabajoDepartamento_SelectedIndexChanged()
        Try
            limpiarCombos(ddlTrabajoDistrito)
            cargarComboProvincia(2)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlTrabajoProvincia_SelectedIndexChanged()
        Try
            cargarComboDistrito(2)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlAdicionalesRadio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        verificarRadio(1)

    End Sub

    Protected Sub ddlTrabajoRadio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        verificarRadio(2)

    End Sub

    Protected Sub btnDisponibilidad_click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validarDisponibilidad(usp_mensaje) Then
                Disponibilidad()
            Else
                MostrarAlertas(usp_mensaje)
            End If
        Catch ex As Exception
            EnvioEmailError(5, ex.ToString)
        End Try
    End Sub

    Protected Sub btnFichaGrabar_click()
        Try
            Dim usp_mensaje As String = ""
            Dim bool_FichaCompleta As Boolean = True

            If Not validarFicha(usp_mensaje) Then
                '    GrabarFicha()
                'Else
                bool_FichaCompleta = False
                MostrarAlertas(usp_mensaje)
            End If

            GrabarFicha(bool_FichaCompleta)
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnFichaCancelar_Click()
        CancelarFicha()
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Carga y setea los campos del formulario a sus valores por defecto
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarOpcionesMantenimiento()

        verificarFechaDefuncion()
        cargarComboTipoDocumento()
        cargarComboEstadoCivil()
        cargarComboNacionalidades()
        cargarComboReligiones()
        cargarComboPaises()

        limpiarCombosUbigeo(1)
        estadoCombosUbigeo(1, False)

        limpiarCombosUbigeo(2)
        estadoCombosUbigeo(2, False)

        cargarComboSituacionLaboral()
        cargarComboNivelesInstruccion()
        cargarComboEscolaridadMinisterio()

        cargarCombosServiciosRadio()
        estadoNumeroRadio(False)

        cargarComboIdiomas()
        cargarComboProfesiones()

        cargarComboAnioEgreso()

    End Sub

    ''' <summary>
    ''' Limpia los campos de Busqueda y los setea a sus valores por defecto
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        tbBuscarApellidoPaterno.Text = ""
        tbBuscarApellidoMaterno.Text = ""
        tbBuscarNombre.Text = ""
        tbBuscarFamiliarApellidoPaterno.Text = ""
        tbBuscarFamiliarApellidoMaterno.Text = ""
        tbBuscarFamiliarNombre.Text = ""
        limpiarCombosFamiliarAlumno()
        rbEstados.SelectedValue = 1

    End Sub

    ''' <summary>
    ''' Carga el combo de Alumno - Nivel para la consulta de Familiares
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombosFamiliarAlumno()

        cargarComboFamiliarAlumnoNivel()
        limpiarCombos(ddlBuscarFamiliarSubNivel)
        limpiarCombos(ddlBuscarFamiliarGrado)
        limpiarCombos(ddlBuscarFamiliarAula)

    End Sub

    ''' <summary>
    ''' Limpia los campos de Busqueda y los setea a sus valores por defecto
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombosFamiliarAlumno()

        limpiarCombos(ddlBuscarFamiliarNivel)
        limpiarCombos(ddlBuscarFamiliarSubNivel)
        limpiarCombos(ddlBuscarFamiliarGrado)
        limpiarCombos(ddlBuscarFamiliarAula)

    End Sub

    ''' <summary>
    ''' Carga el combo Familiar Alumno - Nivel
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        Controles.llenarCombo(ddlBuscarFamiliarNivel, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo Familiar Alumno - SubNivel, filtrando los subniveles por el nivel
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoSubNivel()

        Dim obj_BL_SubNiveles As New bl_SubNiveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(CInt(ddlBuscarFamiliarNivel.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        Controles.llenarCombo(ddlBuscarFamiliarSubNivel, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo Familiar Alumno - Grado, filtrando los grados por el subnivel.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(CInt(ddlBuscarFamiliarSubNivel.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        Controles.llenarCombo(ddlBuscarFamiliarGrado, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo Familiar Alumno - Aulas, filtrando las aulas por el grado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(CInt(ddlBuscarFamiliarGrado.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        Controles.llenarCombo(ddlBuscarFamiliarAula, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Limpia los valores de los campos ocultos (codigo Familiar y codigo Persona)
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub LimpiarCamposFicha()

        hidenCodigoFamiliar.Value = 0
        hidenCodigoPersona.Value = 0

    End Sub

    ''' <summary>
    ''' Configura el estado de los campos del formulario dependiendo de los parametros que recibe
    ''' </summary> 
    ''' <param name="str_Modo">Indica el modo de visualizacion de los datos del formulario</param>
    ''' <param name="int_Modo">Indica el nombre que tendra la cabecera de la pestaña</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String, ByVal int_Modo As Integer)

        miTab1.Enabled = False
        miTab2.Enabled = True

        If int_Modo = 1 Then ' 1 : Nuevo / 2 : Actualizar / 3 : Ver

            ModoMantenimiento(1)
            btnAgregarDetalleIdioma.Style.Remove("display")
            btnAgregarDetalleProfesion.Style.Remove("display")
            btnFichaGrabar.Visible = True
            btnDisponibilidad.Visible = True
            image1.Visible = True
            image2.Visible = True

        ElseIf int_Modo = 2 Then

            ModoMantenimiento(2)
            btnAgregarDetalleIdioma.Style.Remove("display")
            btnAgregarDetalleProfesion.Style.Remove("display")
            btnFichaGrabar.Visible = True
            btnDisponibilidad.Visible = True
            image1.Visible = True
            image2.Visible = True

        ElseIf int_Modo = 3 Then

            ModoMantenimiento(3)
            btnAgregarDetalleIdioma.Style.Add("display", "none")
            btnAgregarDetalleProfesion.Style.Add("display", "none")
            btnFichaGrabar.Visible = False
            btnDisponibilidad.Visible = False
            image1.Visible = False
            image2.Visible = False

        End If

        lbTab2.Text = str_Modo
        TabContainer1.ActiveTabIndex = 1
        TabContainer2.ActiveTabIndex = 0

        DivDisponibilidad.InnerText = ""
        DivDisponibilidad.Attributes.Remove("miDivDisponible")
        DivDisponibilidad.Attributes.Remove("miDivExiste")

    End Sub

    ''' <summary>
    ''' Configura el estado de los campos del formulario dependiendo de los parametros que recibe
    ''' </summary> 
    ''' <param name="int_Modo">Indica el nombre que tendra la cabecera de la pestaña</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ModoMantenimiento(ByVal int_Modo As Integer)

        Dim bool_Etiketas As Boolean = True
        Dim bool_Controles As Boolean = True

        If int_Modo = 1 Then ' Nuevo
            bool_Etiketas = False
            bool_Controles = True
        ElseIf int_Modo = 2 Then 'Editar
            bool_Etiketas = False
            bool_Controles = True
        ElseIf int_Modo = 3 Then 'Ver
            bool_Etiketas = True
            bool_Controles = False
        End If

        'Etiketas
        lblVerApellidoPaterno.Visible = bool_Etiketas
        lblVerApellidoMaterno.Visible = bool_Etiketas
        lblVerNombre.Visible = bool_Etiketas
        lblVerSexo.Visible = bool_Etiketas
        lblVerTipoDocumento.Visible = bool_Etiketas
        lblVerNumDocumento.Visible = bool_Etiketas
        lblVerEstadoCivil.Visible = bool_Etiketas
        lblVerVive.Visible = bool_Etiketas
        lblVerFechaDefuncion.Visible = bool_Etiketas

        lblVerFechaNacimiento.Visible = bool_Etiketas
        lblVerNacionalidad.Visible = bool_Etiketas
        lblVerAdicionalesProfesaReligion.Visible = bool_Etiketas
        lblVerAdicionalesReligion.Visible = bool_Etiketas
        lblVerAdicionalesNombreIglesia.Visible = bool_Etiketas
        lblVerAdicionalesCelular.Visible = bool_Etiketas
        lblVerAdicionalesRadio.Visible = bool_Etiketas
        lblVerAdicionalesNumeroRadio.Visible = bool_Etiketas
        lblVerAdicionalesEmail.Visible = bool_Etiketas

        lblVerDomicilioPais.Visible = bool_Etiketas
        lblVerDomicilioDepartamento.Visible = bool_Etiketas
        lblVerDomicilioProvincia.Visible = bool_Etiketas
        lblVerDomicilioDistrito.Visible = bool_Etiketas
        lblVerDomicilioUrbanizacion.Visible = bool_Etiketas
        lblVerDomicilioDireccion.Visible = bool_Etiketas
        lblVerDomicilioReferencia.Visible = bool_Etiketas
        lblVerDomicilioTelefono.Visible = bool_Etiketas
        lblVerDomicilioAccesoInternet.Visible = bool_Etiketas

        lblVerSituacionLaboral.Visible = bool_Etiketas
        lblVerOcupacion.Visible = bool_Etiketas
        lblVerCentroTrabajo.Visible = bool_Etiketas
        lblVerTrabajoDireccion.Visible = bool_Etiketas
        lblVerTrabajoPais.Visible = bool_Etiketas
        lblVerTrabajoDepartamento.Visible = bool_Etiketas
        lblVerTrabajoProvincia.Visible = bool_Etiketas
        lblVerTrabajoDistrito.Visible = bool_Etiketas
        lblVerTrabajoTelefono.Visible = bool_Etiketas
        lblVerTrabajoCelular.Visible = bool_Etiketas
        lblVerTrabajoRadio.Visible = bool_Etiketas
        lblVerTrabajoNumeroRadio.Visible = bool_Etiketas
        lblVerTrabajoEmail.Visible = bool_Etiketas
        lblVerTrabajoAccesoInternet.Visible = bool_Etiketas

        lblVerEstudiosExAlumno.Visible = bool_Etiketas
        lblVerEstudiosColegioEgreso.Visible = bool_Etiketas
        lblVerEstudiosAnioEgreso.Visible = bool_Etiketas
        lblVerEstudiosContinuo.Visible = bool_Etiketas
        lblVerEstudiosNivelInstruccion.Visible = bool_Etiketas
        lblVerEstudiosEscolaridadMinisterio.Visible = bool_Etiketas

        'Controles
        tbApellidoPaterno.Visible = bool_Controles
        tbApellidoMaterno.Visible = bool_Controles
        tbNombre.Visible = bool_Controles
        rbSexo.Visible = bool_Controles
        ddlTipoDocumento.Visible = bool_Controles
        tbNumDocumento.Visible = bool_Controles
        ddlEstadoCivil.Visible = bool_Controles
        rbVive.Visible = bool_Controles
        tbFechaDefuncion.Visible = bool_Controles

        tbFechaNacimiento.Visible = bool_Controles
        ddlNacionalidad.Visible = bool_Controles
        rbAdicionalesProfesaReligion.Visible = bool_Controles
        ddlAdicionalesReligion.Visible = bool_Controles
        tbAdicionalesNombreIglesia.Visible = bool_Controles
        tbAdicionalesCelular.Visible = bool_Controles
        ddlAdicionalesRadio.Visible = bool_Controles
        tbAdicionalesNumeroRadio.Visible = bool_Controles
        tbAdicionalesEmail.Visible = bool_Controles

        ddlDomicilioPais.Visible = bool_Controles
        ddlDomicilioDepartamento.Visible = bool_Controles
        ddlDomicilioProvincia.Visible = bool_Controles
        ddlDomicilioDistrito.Visible = bool_Controles
        tbDomicilioUrbanizacion.Visible = bool_Controles
        tbDomicilioDireccion.Visible = bool_Controles
        tbDomicilioReferencia.Visible = bool_Controles
        tbDomicilioTelefono.Visible = bool_Controles
        rbDomicilioAccesoInternet.Visible = bool_Controles

        ddlSituacionLaboral.Visible = bool_Controles
        tbOcupacion.Visible = bool_Controles
        tbCentroTrabajo.Visible = bool_Controles
        tbTrabajoDireccion.Visible = bool_Controles
        ddlTrabajoPais.Visible = bool_Controles
        ddlTrabajoDepartamento.Visible = bool_Controles
        ddlTrabajoProvincia.Visible = bool_Controles
        ddlTrabajoDistrito.Visible = bool_Controles
        tbTrabajoTelefono.Visible = bool_Controles
        tbTrabajoCelular.Visible = bool_Controles
        ddlTrabajoRadio.Visible = bool_Controles
        tbTrabajoNumeroRadio.Visible = bool_Controles
        tbTrabajoEmail.Visible = bool_Controles
        rbTrabajoAccesoInternet.Visible = bool_Controles

        rbEstudiosExAlumno.Visible = bool_Controles
        tbEstudiosColegioEgreso.Visible = bool_Controles
        'tbEstudiosAnioEgreso.Visible = bool_Controles
        ddlEstudiosAnioEgreso.Visible = bool_Controles
        tbEstudiosContinuo.Visible = bool_Controles
        ddlEstudiosNivelInstruccion.Visible = bool_Controles
        ddlEstudiosEscolaridadMinisterio.Visible = bool_Controles

    End Sub

    ''' <summary>
    ''' Limpia todos los controles del formlario
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarControlesMantenimiento()
        lblVerApellidoPaterno.Text = ""
        lblVerApellidoMaterno.Text = ""
        lblVerNombre.Text = ""
        lblVerSexo.Text = ""
        lblVerTipoDocumento.Text = ""
        lblVerNumDocumento.Text = ""
        lblVerEstadoCivil.Text = ""
        lblVerVive.Text = ""
        lblVerFechaDefuncion.Text = ""

        lblVerFechaNacimiento.Text = ""
        lblVerNacionalidad.Text = ""
        lblVerAdicionalesProfesaReligion.Text = ""
        lblVerAdicionalesReligion.Text = ""
        lblVerAdicionalesNombreIglesia.Text = ""
        lblVerAdicionalesCelular.Text = ""
        lblVerAdicionalesRadio.Text = ""
        lblVerAdicionalesNumeroRadio.Text = ""
        lblVerAdicionalesEmail.Text = ""

        lblVerDomicilioPais.Text = ""
        lblVerDomicilioDepartamento.Text = ""
        lblVerDomicilioProvincia.Text = ""
        lblVerDomicilioDistrito.Text = ""
        lblVerDomicilioUrbanizacion.Text = ""
        lblVerDomicilioDireccion.Text = ""
        lblVerDomicilioReferencia.Text = ""
        lblVerDomicilioTelefono.Text = ""
        lblVerDomicilioAccesoInternet.Text = ""

        lblVerSituacionLaboral.Text = ""
        lblVerOcupacion.Text = ""
        lblVerCentroTrabajo.Text = ""
        lblVerTrabajoDireccion.Text = ""
        lblVerTrabajoPais.Text = ""
        lblVerTrabajoDepartamento.Text = ""
        lblVerTrabajoProvincia.Text = ""
        lblVerTrabajoDistrito.Text = ""
        lblVerTrabajoTelefono.Text = ""
        lblVerTrabajoCelular.Text = ""
        lblVerTrabajoRadio.Text = ""
        lblVerTrabajoNumeroRadio.Text = ""
        lblVerTrabajoEmail.Text = ""
        lblVerTrabajoAccesoInternet.Text = ""

        lblVerEstudiosExAlumno.Text = ""
        lblVerEstudiosColegioEgreso.Text = ""
        lblVerEstudiosAnioEgreso.Text = ""
        lblVerEstudiosContinuo.Text = ""
        lblVerEstudiosNivelInstruccion.Text = ""
        lblVerEstudiosEscolaridadMinisterio.Text = ""

        'Controles
        tbApellidoPaterno.Text = ""
        tbApellidoMaterno.Text = ""
        tbNombre.Text = ""
        rbSexo.SelectedValue = 1
        ddlTipoDocumento.SelectedValue = 0
        tbNumDocumento.Text = ""
        ddlEstadoCivil.SelectedValue = 0
        rbVive.SelectedValue = 1
        tbFechaDefuncion.Text = ""

        tbFechaNacimiento.Text = ""
        ddlNacionalidad.SelectedValue = 0
        rbAdicionalesProfesaReligion.SelectedValue = 0
        ddlAdicionalesReligion.SelectedValue = 0
        tbAdicionalesNombreIglesia.Text = ""
        tbAdicionalesCelular.Text = ""
        ddlAdicionalesRadio.SelectedValue = 0
        tbAdicionalesNumeroRadio.Text = ""
        tbAdicionalesEmail.Text = ""

        ddlDomicilioPais.SelectedValue = 0
        ddlDomicilioDepartamento.SelectedValue = 0
        ddlDomicilioProvincia.SelectedValue = 0
        ddlDomicilioDistrito.SelectedValue = 0
        tbDomicilioUrbanizacion.Text = ""
        tbDomicilioDireccion.Text = ""
        tbDomicilioReferencia.Text = ""
        tbDomicilioTelefono.Text = ""
        rbDomicilioAccesoInternet.SelectedValue = 0

        ddlSituacionLaboral.SelectedValue = 0
        tbOcupacion.Text = ""
        tbCentroTrabajo.Text = ""
        tbTrabajoDireccion.Text = ""
        ddlTrabajoPais.SelectedValue = 0
        ddlTrabajoDepartamento.SelectedValue = 0
        ddlTrabajoProvincia.SelectedValue = 0
        ddlTrabajoDistrito.SelectedValue = 0
        tbTrabajoTelefono.Text = ""
        tbTrabajoCelular.Text = ""
        ddlTrabajoRadio.SelectedValue = 0
        tbTrabajoNumeroRadio.Text = ""
        tbTrabajoEmail.Text = ""
        rbTrabajoAccesoInternet.SelectedValue = 0

        rbEstudiosExAlumno.SelectedValue = 0
        tbEstudiosColegioEgreso.Text = ""
        'tbEstudiosAnioEgreso.text = ""
        ddlEstudiosAnioEgreso.SelectedValue = 0
        tbEstudiosContinuo.Text = ""
        ddlEstudiosNivelInstruccion.SelectedValue = 0
        ddlEstudiosEscolaridadMinisterio.SelectedValue = 0

        GVListaIdiomas.DataBind()
        GVListaProfesiones.DataBind()
        GVListaFichaAutos.DataBind()
    End Sub

    ''' <summary>
    ''' Activa la pestaña que contiene los datos del formulario, asi como los controles del detalle 
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ActivarCampos()
        TabContainer2.Enabled = True
        EstadoDatosFamiliar(True)
    End Sub

    ''' <summary>
    ''' Desactiva la pestaña que contiene los datos del formulario, asi como los controles del detalle 
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub DesactivarCampos()

        TabContainer2.Enabled = False
        EstadoDatosFamiliar(False)

    End Sub

    ''' <summary>
    ''' Lista las fichas de familiares que coinciden ocn los parámetros de busqueda enviados
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listarFichas()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GVListaFamiliar.DataSource = ds_Lista.Tables(0)
        GVListaFamiliar.DataBind()

        SortGridView(ViewState("SortExpression"), ViewState("Direccion"))

        If hfTotalRegs.Value > 0 Then
            ImagenSorting(ViewState("SortExpression"))
        End If

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

        Dim objMaestroPersona As New be_MaestroPersonas

        objMaestroPersona.ApellidoPaterno = tbBuscarApellidoPaterno.Text.Trim
        objMaestroPersona.ApellidoMaterno = tbBuscarApellidoMaterno.Text.Trim
        objMaestroPersona.Nombre = tbBuscarNombre.Text.Trim
        objMaestroPersona.AlumnoFamiliarApellidoPaterno = tbBuscarFamiliarApellidoPaterno.Text.Trim
        objMaestroPersona.AlumnoFamiliarApellidoMaterno = tbBuscarFamiliarApellidoMaterno.Text.Trim
        objMaestroPersona.AlumnoFamiliarNombres = tbBuscarFamiliarNombre.Text.Trim
        objMaestroPersona.AlumnoFamiliarNivel = ddlBuscarFamiliarNivel.SelectedValue
        objMaestroPersona.AlumnoFamiliarSubnivel = ddlBuscarFamiliarSubNivel.SelectedValue
        objMaestroPersona.AlumnoFamiliarGrado = ddlBuscarFamiliarGrado.SelectedValue
        objMaestroPersona.AlumnoFamiliarAula = ddlBuscarFamiliarAula.SelectedValue
        objMaestroPersona.EstadoPersona = rbEstados.SelectedValue
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Familiares As New bl_Familiares
            ds_Lista = obj_BL_Familiares.FUN_LIS_Familiar(objMaestroPersona, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_Familiares As New bl_Familiares
                ds_Lista = obj_BL_Familiares.FUN_LIS_Familiar(objMaestroPersona, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

#Region "Cargas"

    ''' <summary>
    ''' Carga el combo con la lista de Tipo de Documentos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoDocumento()

        Dim obj_BL_TipoDocIdentidad As New bl_TipoDocIdentidad
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_TipoDocIdentidad.FUN_LIS_TipoDocIdentidad("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 57)
        Controles.llenarCombo(ddlTipoDocumento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Estado Civil disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboEstadoCivil()

        Dim obj_BL_EstadosCiviles As New bl_EstadosCiviles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_EstadosCiviles.FUN_LIS_EstadosCiviles("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 57)
        Controles.llenarCombo(ddlEstadoCivil, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Nacionalidades disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNacionalidades()

        Dim obj_BL_Nacionalidades As New bl_Nacionalidades
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Nacionalidades.FUN_LIS_Nacionalidades("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 57)
        Controles.llenarCombo(ddlNacionalidad, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Religiones disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboReligiones()

        Dim obj_BL_Religiones As New bl_Religiones
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Religiones.FUN_LIS_Religiones("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 57)
        Controles.llenarCombo(ddlAdicionalesReligion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Paises disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboPaises()

        Dim obj_BL_Paises As New bl_Paises
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Paises.FUN_LIS_Pais("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 57)

        Controles.llenarCombo(ddlDomicilioPais, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlTrabajoPais, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga y limpia los combos dependientes del Ubigeo Departamento
    ''' </summary>
    ''' <param name="tab">Indica si se cargara el combo Ubigeo domicilio o trabajo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboUbigeo(ByVal tab As Integer)

        If tab = 1 Then ' Domicilio

            cargarComboDepartamentos(1)
            limpiarCombos(ddlDomicilioProvincia)
            limpiarCombos(ddlDomicilioDistrito)

        ElseIf tab = 2 Then  ' Trabajo

            cargarComboDepartamentos(2)
            limpiarCombos(ddlTrabajoProvincia)
            limpiarCombos(ddlTrabajoDistrito)

        End If

    End Sub

    ''' <summary>
    ''' Carga el combo Ubigeo Departamento(domicilio o trabajo)
    ''' </summary>
    ''' <param name="tab">Indica si se cargara el combo Ubigeo domicilio o trabajo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDepartamentos(ByVal tab As Integer)

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Departamentos(int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)

        If tab = 1 Then ' Domicilio

            Controles.llenarCombo(ddlDomicilioDepartamento, ds_Lista, "Codigo", "Descripcion", False, True)

        ElseIf tab = 2 Then  ' Trabajo

            Controles.llenarCombo(ddlTrabajoDepartamento, ds_Lista, "Codigo", "Descripcion", False, True)

        End If

    End Sub

    ''' <summary>
    ''' Carga el combo Ubigeo Provincia(domicilio o trabajo), filtrando por Departamento
    ''' </summary>
    ''' <param name="tab">Indica si se cargara el combo Ubigeo domicilio o trabajo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboProvincia(ByVal tab As Integer)

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        If tab = 1 Then ' Domicilio

            Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlDomicilioDepartamento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
            Controles.llenarCombo(ddlDomicilioProvincia, ds_Lista, "Codigo", "Descripcion", False, True)

        ElseIf tab = 2 Then  ' Trabajo

            Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlTrabajoDepartamento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
            Controles.llenarCombo(ddlTrabajoProvincia, ds_Lista, "Codigo", "Descripcion", False, True)

        End If

    End Sub

    ''' <summary>
    ''' Carga el combo Ubigeo Distrito(domicilio o trabajo), filtrando por Departamento y Provincia
    ''' </summary>
    ''' <param name="tab">Indica si se cargara el combo Ubigeo domicilio o trabajo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDistrito(ByVal tab As Integer)

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        If tab = 1 Then ' Domicilio

            Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlDomicilioDepartamento.SelectedValue, ddlDomicilioProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
            Controles.llenarCombo(ddlDomicilioDistrito, ds_Lista, "Codigo", "Descripcion", False, True)

        ElseIf tab = 2 Then  ' Trabajo

            Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlTrabajoDepartamento.SelectedValue, ddlTrabajoProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
            Controles.llenarCombo(ddlTrabajoDistrito, ds_Lista, "Codigo", "Descripcion", False, True)

        End If

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Situación Laboral disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSituacionLaboral()

        Dim obj_BL_SituacionesLaborales As New bl_SituacionesLaborales
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SituacionesLaborales.FUN_LIS_SituacionLaboral("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        Controles.llenarCombo(ddlSituacionLaboral, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Nivel de Instrucción disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNivelesInstruccion()

        Dim obj_BL_NivelesInstruccion As New bl_NivelesInstruccion
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_NivelesInstruccion.FUN_LIS_NivelesInstruccion("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        Controles.llenarCombo(ddlEstudiosNivelInstruccion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Escolaridad Ministerio disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboEscolaridadMinisterio()

        Dim obj_BL_EscolaridadesMinisterio As New bl_EscolaridadesMinisterio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_EscolaridadesMinisterio.FUN_LIS_EscolaridadMinisterio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        Controles.llenarCombo(ddlEstudiosEscolaridadMinisterio, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Servicios Radio disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombosServiciosRadio()

        Dim obj_BL_ServiciosRadio As New bl_ServiciosRadio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_ServiciosRadio.FUN_LIS_ServicioRadio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        Controles.llenarCombo(ddlAdicionalesRadio, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlTrabajoRadio, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Idiomas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboIdiomas()

        Dim obj_BL_Idiomas As New bl_Idiomas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Idiomas.FUN_LIS_Idiomas("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        Controles.llenarCombo(ddlIdioma, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub
    ''' <summary>
    ''' Carga el combo con la lista de Profesiones disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboProfesiones()

        Dim obj_BL_Profesiones As New bl_Profesiones
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Profesiones.FUN_LIS_Profesion("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        Controles.llenarCombo(ddlProfesion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con una serie de años
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAnioEgreso()
        Controles.llenarComboNumerico(ddlEstudiosAnioEgreso, 1950, Today.Year, False, True)
    End Sub

#End Region

#Region "Verificacion de campos"

    ''' <summary>
    ''' Dependiendo del pais, se activarán o desactivarán otros campos del formulario
    ''' </summary>
    ''' <param name="tab">Indica si se tomaran los datos del bloque de informacion de domicilio o trabajo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarPais(ByVal tab As Integer)

        Dim cod_Pais As Integer = 0

        If tab = 1 Then ' Domicilio
            cod_Pais = ddlDomicilioPais.SelectedValue
            If cod_Pais = 1 Then  ' Si el pais es PERU
                cargarComboUbigeo(1)
                estadoCombosUbigeo(1, True)
            Else
                limpiarCombosUbigeo(1)
                estadoCombosUbigeo(1, False)
            End If

        ElseIf tab = 2 Then  ' Trabajo
            cod_Pais = ddlTrabajoPais.SelectedValue
            If cod_Pais = 1 Then  ' Si el pais es PERU
                cargarComboUbigeo(2)
                estadoCombosUbigeo(2, True)
            Else
                limpiarCombosUbigeo(2)
                estadoCombosUbigeo(2, False)
            End If
        End If

    End Sub

    ''' <summary>
    ''' Dependiendo del tipo de radio, se activarán o desactivarán otros campos del formulario
    ''' </summary>
    ''' <param name="tab">Indica si se tomaran los datos del bloque de informacion de domicilio o trabajo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarRadio(ByVal tab As Integer)

        If tab = 1 Then ' Domicilio

            If ddlAdicionalesRadio.SelectedValue = 0 Then
                tbAdicionalesNumeroRadio.Enabled = False
            Else
                tbAdicionalesNumeroRadio.Enabled = True
            End If

        ElseIf tab = 2 Then  ' Trabajo

            If ddlTrabajoRadio.SelectedValue = 0 Then
                tbTrabajoNumeroRadio.Enabled = False
            Else
                tbTrabajoNumeroRadio.Enabled = True
            End If

        End If

    End Sub

    ''' <summary>
    ''' Dependiendo de si posee o no fecha de defuncion, se activarán o desactivarán otros campos del formulario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarFechaDefuncion()

        If rbVive.SelectedValue = 1 Then
            tbFechaDefuncion.Enabled = False
            image1.Enabled = False
            CalendarExtender1.Enabled = False
        Else
            tbFechaDefuncion.Enabled = True
            image1.Enabled = True
            CalendarExtender1.Enabled = True
        End If


    End Sub

    ''' <summary>
    ''' Dependiendo de si profesa religión o no, se activarán o desactivarán otros campos del formulario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarProfesaReligion()

        If rbAdicionalesProfesaReligion.SelectedValue = 0 Then 'No
            ddlAdicionalesReligion.Enabled = False
            tbAdicionalesNombreIglesia.Enabled = False
        Else 'Si
            ddlAdicionalesReligion.Enabled = True
            tbAdicionalesNombreIglesia.Enabled = True
        End If

    End Sub

    ''' <summary>
    ''' Dependiendo de su situacion laboral, se activarán o desactivarán otros campos del formulario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarSituacionLaboral()
        Select Case ddlSituacionLaboral.SelectedValue
            Case 0, 1, 2, 5
                estadoCamposTrabajo(False)
            Case 3, 4
                estadoCamposTrabajo(True)
        End Select
    End Sub

    ''' <summary>
    ''' Dependiendo de si ex-alumno o no, se activarán o desactivarán otros campos del formulario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarExAlumno()
        If rbEstudiosExAlumno.SelectedValue = 1 Then ' Si
            tbEstudiosColegioEgreso.Enabled = False
        Else
            tbEstudiosColegioEgreso.Enabled = True
        End If
    End Sub

#End Region

#Region "Disponibilidad"

    Protected Sub btnCerraFichaTemporal_Click()
        pnModalFichaFamiliarDisponible.Hide()
    End Sub

    ''' <summary>
    ''' Se valida que cumpla con los datos minimos para poder buscar la disponibilidad del nombre en la base de datos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarDisponibilidad(ByRef str_Mensaje As String) As Boolean

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

        'If ddlTipoDocumento.SelectedValue = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Documento")
        '    result = False
        'End If

        'If tbNumDocumento.Text.Trim.Length = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Número de Documento")
        '    result = False
        'End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Se consulta si el nombre del familiar ingresado, no existe para otro registro en la base de datos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Disponibilidad()

        Dim obj_BE_Personas As New be_Personas
        Dim obj_BL_MaestroPersonas As New bl_MaestroPersonas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        obj_BE_Personas.ApellidoPaterno = tbApellidoPaterno.Text.Trim
        obj_BE_Personas.ApellidoMaterno = tbApellidoMaterno.Text.Trim
        obj_BE_Personas.Nombre = tbNombre.Text.Trim
        obj_BE_Personas.CodigoTipoDocIdentidad = ddlTipoDocumento.SelectedValue
        obj_BE_Personas.NumeroDocIdentidad = tbNumDocumento.Text.Trim

        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer = 0

        Dim ds_Lista As DataSet = obj_BL_MaestroPersonas.FUN_LIS_PersonaDisponibilidad(obj_BE_Personas, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        usp_valor = CInt(ds_Lista.Tables(0).Rows(0).Item("p_Valor").ToString)
        usp_mensaje = ds_Lista.Tables(0).Rows(0).Item("p_Mensaje").ToString

        'usp_valor = obj_BL_MaestroPersonas.FUN_CON_PersonaDisponibilidad(obj_BE_Personas, usp_mensaje)

        DivDisponibilidad.InnerText = usp_mensaje

        If usp_valor = 1 Then

            DivDisponibilidad.Attributes.Add("class", "miDivExiste")
            DesactivarCampos()

            'Muestro listado Posibles coincidencias
            If ds_Lista.Tables(1).Rows.Count > 0 Then
                MostrarListaDisponibilidad(ds_Lista.Tables(1))
            End If

        Else

            'Nombre de persona disponible para registro
            DivDisponibilidad.Attributes.Add("class", "miDivDisponible")
            ActivarCampos()

        End If

    End Sub

    ''' <summary>
    ''' Muestra la lista de posibles coincidencias de nombres de familiares
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarListaDisponibilidad(ByVal ds_Lista As DataTable)

        pnModalFichaFamiliarDisponible.Show()
        GVListaFichaFamiliarDisponible.DataSource = ds_Lista
        GVListaFichaFamiliarDisponible.DataBind()

    End Sub

    Protected Sub GVListaFichaFamiliarDisponible_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Or e.CommandName = "Eliminar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Seleccionar" Then
                    int_CodigoAccion = 5
                    pnModalFichaFamiliarDisponible.Hide()
                    ViewState("VerFicha") = False
                    ActivarCampos()
                    ObtenerFichaPorPersona(codigo)

                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaFichaFamiliarDisponible_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(2, 48)

        'CONTROLES DEL FORMULARIO
        'Master.BloqueoControles(btnBuscar, 1)
        'Master.BloqueoControles(btnGrabar, 1)

        'Master.SeteoPermisosAcciones(btnBuscar, 48)
        'Master.SeteoPermisosAcciones(btnGrabar, 48)

    End Sub

    ''' <summary>
    ''' Elimina los elementos de la lista
    ''' </summary>
    ''' <param name="combo">Nombre del combobox</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)
        Controles.limpiarCombo(combo, True, False)
    End Sub

    ''' <summary>
    ''' Limpia los elementos de las listas de ubigeo (Departamento, Provincia y Distrito)
    ''' </summary>
    ''' <param name="tab">Indica si el combo pertenece al bloque de información de domicilio o trabajo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombosUbigeo(ByVal tab As Integer)

        If tab = 1 Then ' Domicilio

            limpiarCombos(ddlDomicilioDepartamento)
            limpiarCombos(ddlDomicilioProvincia)
            limpiarCombos(ddlDomicilioDistrito)

        ElseIf tab = 2 Then  ' Trabajo

            limpiarCombos(ddlTrabajoDepartamento)
            limpiarCombos(ddlTrabajoProvincia)
            limpiarCombos(ddlTrabajoDistrito)

        End If


    End Sub

    ''' <summary>
    ''' Setea el estado de los campos del formulario del bloque de información de trabajo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub estadoCamposTrabajo(ByVal bool_estado As Boolean)

        tbOcupacion.Enabled = bool_estado
        tbCentroTrabajo.Enabled = bool_estado
        tbTrabajoDireccion.Enabled = bool_estado
        ddlTrabajoPais.Enabled = bool_estado

        If bool_estado = False Then
            ddlTrabajoDepartamento.Enabled = bool_estado
            ddlTrabajoProvincia.Enabled = bool_estado
            ddlTrabajoDistrito.Enabled = bool_estado
        Else
            Dim stR_Pais As String = ddlTrabajoPais.SelectedItem.ToString
            If stR_Pais = "Perú" Then
                estadoCombosUbigeo(2, True)
            Else
                estadoCombosUbigeo(2, False)
            End If
        End If

        tbTrabajoTelefono.Enabled = bool_estado
        tbTrabajoCelular.Enabled = bool_estado
        ddlTrabajoRadio.Enabled = bool_estado
        tbTrabajoNumeroRadio.Enabled = bool_estado
        tbTrabajoEmail.Enabled = bool_estado
        rbTrabajoAccesoInternet.Enabled = bool_estado

        If bool_estado = False And ddlSituacionLaboral.SelectedValue <> 0 Then
            rbTrabajoAccesoInternet.SelectedValue = 0
        End If

    End Sub

    ''' <summary>
    ''' Activa o desactiva el estado de los combos de ubigeo dependiendo a que bloque de informacón pertenescan
    ''' </summary>
    ''' <param name="tab">indica a que bloque sera aplicado el cambio</param>
    ''' <param name="bool_estado">el estado al que seran seteados los controles</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub estadoCombosUbigeo(ByVal tab As Integer, ByVal bool_estado As Boolean)

        If tab = 1 Then ' Domicilio

            ddlDomicilioDepartamento.Enabled = bool_estado
            ddlDomicilioProvincia.Enabled = bool_estado
            ddlDomicilioDistrito.Enabled = bool_estado

        ElseIf tab = 2 Then  ' Trabajo

            ddlTrabajoDepartamento.Enabled = bool_estado
            ddlTrabajoProvincia.Enabled = bool_estado
            ddlTrabajoDistrito.Enabled = bool_estado

        End If

    End Sub

    ''' <summary>
    ''' Setea el estado de los campos de radio
    ''' </summary>
    ''' <param name="bool_estado">indica el estado al que seran seteados</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub estadoNumeroRadio(ByVal bool_estado As Boolean)

        tbTrabajoNumeroRadio.Enabled = bool_estado
        tbAdicionalesNumeroRadio.Enabled = bool_estado

    End Sub

    ''' <summary>
    ''' Obtiene todos los datos de la ficha de 1 persona
    ''' </summary>
    ''' <param name="int_CodigoPersona">indica el codigo de la persona a la cual se va a consultar la ficha</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerFichaPorPersona(ByVal int_CodigoPersona As Integer)

        Dim str_mensajeError As String = ""

        Dim obj_BL_Familiares As New bl_Familiares
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Familiares.FUN_GET_FamiliarPorPersona(int_CodigoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)

        If ds_Lista.Tables.Count = 3 Then

            'Obtengo y listo datos solo de la persona
            hidenCodigoPersona.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoPersona").ToString
            tbApellidoPaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString
            tbApellidoMaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString
            tbNombre.Text = ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString
            rbSexo.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoSexo").ToString
            ddlTipoDocumento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoTipoDocIdentidad").ToString
            tbNumDocumento.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString
            ddlEstadoCivil.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoEstadoCivil").ToString

            'Datos Nacimiento
            tbFechaNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString
            If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
                ddlNacionalidad.SelectedValue = ds_Lista.Tables(1).Rows(0).Item("Codigo").ToString
            Else
                ddlNacionalidad.SelectedValue = 0
            End If

            'Datos Adicionales
            If CBool(ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion").ToString) Then
                rbAdicionalesProfesaReligion.SelectedValue = 1
            Else
                rbAdicionalesProfesaReligion.SelectedValue = 0
            End If
            ddlAdicionalesReligion.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoReligion").ToString
            verificarProfesaReligion()
            tbAdicionalesCelular.Text = ds_Lista.Tables(0).Rows(0).Item("CelularPersonal").ToString
            tbAdicionalesEmail.Text = ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString

            'Datos Domicilio
            ddlDomicilioPais.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoPaisDomicilio").ToString
            ddlDomicilioPais_SelectedIndexChanged()
            'cargarComboUbigeo(1)
            'estadoCombosUbigeo(1, True)
            If ddlDomicilioDepartamento.Enabled Then
                ddlDomicilioDepartamento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioDepartamento").ToString
                ddlDomicilioDepartamento_SelectedIndexChanged()
                ddlDomicilioProvincia.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioProvincia").ToString
                ddlDomicilioProvincia_SelectedIndexChanged()
                ddlDomicilioDistrito.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioDistrito").ToString
            End If

            tbDomicilioUrbanizacion.Text = ds_Lista.Tables(0).Rows(0).Item("UrbanizacionDomicilio").ToString
            tbDomicilioDireccion.Text = ds_Lista.Tables(0).Rows(0).Item("DireccionDomicilio").ToString
            tbDomicilioReferencia.Text = ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomicilio").ToString
            tbDomicilioTelefono.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoDomicilio").ToString

            'Detalle Idioma
            Dim objDT_Idioma As DataTable

            objDT_Idioma = New DataTable("ListaIdiomas")
            objDT_Idioma = ds_Lista.Tables(2).Clone

            If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then

                For Each dr As DataRow In ds_Lista.Tables(2).Rows
                    objDT_Idioma.ImportRow(dr)
                Next

                ViewState("ListaIdiomas") = objDT_Idioma
                GVListaIdiomas.DataSource = objDT_Idioma
                GVListaIdiomas.DataBind()

            End If

            VerRegistro("Inserción", 1)
            'VerRegistro("Actualización", 2)

        Else 'ds_Lista.Tables.Count = 1

            'Otengo el codigo del familiar y listo datos del familiar
            Dim int_CodigoFamiliar As Integer = ds_Lista.Tables(0).Rows(0).Item("CodigoFamiliar").ToString
            ObtenerFicha(int_CodigoFamiliar)
        End If

    End Sub

    ''' <summary>
    ''' Setea el estado de los botones de detalle del formulario
    ''' </summary>
    ''' <param name="bool_Estado">indica el estado al que seran seteados</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EstadoDatosFamiliar(ByVal bool_Estado As Boolean)

        If bool_Estado Then
            btnAgregarDetalleIdioma.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
            btnAgregarDetalleProfesion.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
        Else
            btnAgregarDetalleIdioma.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_0.png"
            btnAgregarDetalleProfesion.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_0.png"
        End If

    End Sub
    ''' <summary>
    ''' Valida la ficha antes de proceder a grabarla
    ''' </summary>
    ''' <param name="str_Mensaje">variable de cadena que acumulara todos los mensajes de la validación</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
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

        If tbNumDocumento.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Número de Documento")
            result = False
        End If

        If ddlEstadoCivil.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Estado Civil")
            result = False
        End If

        If rbVive.SelectedValue = 0 Then
            If IsDate(tbFechaDefuncion.Text) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Defunción")
                result = False
            Else
                Dim comp As Integer = DateTime.Compare(tbFechaDefuncion.Text, Today.ToShortDateString)

                If comp > 0 Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 8, "Fecha de Defunción")
                    result = False
                End If
            End If
        End If

        If IsDate(tbFechaNacimiento.Text) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Nacimiento")
            result = False
        Else
            Dim comp As Integer = DateTime.Compare(tbFechaNacimiento.Text, Today.ToShortDateString)

            If comp > 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 8, "Fecha de Nacimiento")
                result = False
            End If
        End If

        If ddlNacionalidad.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Nacionalidad")
            result = False
        End If

        If rbAdicionalesProfesaReligion.SelectedValue = 1 Then
            If ddlAdicionalesReligion.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Religión")
                result = False
            End If
        End If

        If ddlDomicilioPais.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "País Domicilio")
            result = False
        ElseIf ddlDomicilioPais.SelectedItem.ToString = "Perú" Then

            If ddlDomicilioDepartamento.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Departamento Domicilio")
                result = False
            End If
            If ddlDomicilioProvincia.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Provincia Domicilio")
                result = False
            End If
            If ddlDomicilioDistrito.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Distrito Domicilio")
                result = False
            End If

        End If

        If tbDomicilioUrbanizacion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Urbanización Domicilio")
            result = False
        End If

        If tbDomicilioDireccion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Dirección Domicilio")
            result = False
        End If

        If ddlSituacionLaboral.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Situación Laboral")
            result = False

        ElseIf ddlSituacionLaboral.SelectedValue = 3 Or ddlSituacionLaboral.SelectedValue = 4 Then

            If tbOcupacion.Text.Trim.Length = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Ocupación / Cargo")
                result = False
            End If

            If ddlTrabajoPais.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "País Trabajo")
                result = False
            ElseIf ddlTrabajoPais.SelectedItem.ToString = "Perú" Then

                If ddlTrabajoDepartamento.SelectedValue = 0 Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Departamento Trabajo")
                    result = False
                End If
                If ddlTrabajoProvincia.SelectedValue = 0 Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Provincia Trabajo")
                    result = False
                End If
                If ddlTrabajoDistrito.SelectedValue = 0 Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Distrito Trabajo")
                    result = False
                End If

            End If

        End If

        If ddlEstudiosNivelInstruccion.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Nivel de instrucción")
            result = False
        End If

        If ddlEstudiosEscolaridadMinisterio.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Escolaridad Ministerio")
            result = False
        End If

        Dim dt As DataTable
        If ViewState("ListaIdiomas") Is Nothing Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Idiomas")
            result = False
        Else
            dt = ViewState("ListaIdiomas")
            If dt.Rows.Count = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Idiomas")
                result = False
            End If
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Graba la ficha del familiar
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarFicha(ByVal bool_FichaCompleta As Boolean)

        Dim str_mensajeError As String = ""

        Dim obj_BE_Familiar As New be_Familiares
        Dim obj_BL_Familiar As New bl_Familiares
        Dim obj_BE_FichaAutos As New be_FichaAutos
        Dim obj_BL_FichaAutos As New bl_FichaAutos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim BoolGrabar As Integer = hidenCodigoFamiliar.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer = 0
        Dim usp_valorFichaAuto As Integer = 0

        Dim outparamCodigoPersona As Integer
        Dim outparamCodigoFamilia As Integer

        obj_BE_Familiar.CodigoPersona = hidenCodigoPersona.Value

        'Datos Personales
        obj_BE_Familiar.ApellidoPaterno = IIf(tbApellidoPaterno.Text.Trim.Length = 0, Nothing, tbApellidoPaterno.Text.Trim)
        obj_BE_Familiar.ApellidoMaterno = IIf(tbApellidoMaterno.Text.Trim.Length = 0, Nothing, tbApellidoMaterno.Text.Trim)
        obj_BE_Familiar.Nombre = IIf(tbNombre.Text.Trim.Length = 0, Nothing, tbNombre.Text.Trim)
        obj_BE_Familiar.CodigoSexo = IIf(rbSexo.SelectedValue = 0, -1, rbSexo.SelectedValue)
        obj_BE_Familiar.CodigoTipoDocIdentidad = IIf(ddlTipoDocumento.SelectedValue = 0, -1, ddlTipoDocumento.SelectedValue)
        obj_BE_Familiar.NumeroDocIdentidad = IIf(tbNumDocumento.Text.Trim.Length = 0, Nothing, tbNumDocumento.Text.Trim)
        obj_BE_Familiar.CodigoEstadoCivil = IIf(ddlEstadoCivil.SelectedValue = 0, -1, ddlEstadoCivil.SelectedValue)
        obj_BE_Familiar.Vive = rbVive.SelectedValue

        If rbVive.SelectedValue = 1 Then 'Vive : Si
            obj_BE_Familiar.FechaDefuncion = "01/01/1753"
        ElseIf rbVive.SelectedValue = 0 Then 'Vive : No
            obj_BE_Familiar.FechaDefuncion = IIf(tbFechaDefuncion.Text.Trim.Length = 0, Nothing, tbFechaDefuncion.Text.Trim)
        End If
        Dim dt_fecha As Date

        'obj_BE_Familiar.FechaDefuncion = tbFechaDefuncion.Text.Trim
        If tbFechaNacimiento.Text = "00/00/0000" Or tbFechaNacimiento.Text.Trim.Length <= 9 Then
            dt_fecha = Nothing
        Else
            dt_fecha = tbFechaNacimiento.Text.Trim
        End If
        'Datos nacimiento / nacionalidad tbFechaNacimiento.Text.Trim.Length
        obj_BE_Familiar.FechaNacimiento = dt_fecha ' IIf(IIf(tbFechaNacimiento.Text, "00/00/0000", 2) <= 9, Nothing, tbFechaNacimiento.Text.Trim)
        obj_BE_Familiar.CodigoNacionalidad = IIf(ddlNacionalidad.SelectedValue = 0, Nothing, ddlNacionalidad.SelectedValue)

        'Datos Adicionales
        obj_BE_Familiar.ProfesaReligion = rbAdicionalesProfesaReligion.SelectedValue

        If rbAdicionalesProfesaReligion.SelectedValue = 1 Then ' Si
            obj_BE_Familiar.CodigoReligion = IIf(ddlAdicionalesReligion.SelectedValue = 0, -1, ddlAdicionalesReligion.SelectedValue)
        ElseIf rbAdicionalesProfesaReligion.SelectedValue = 0 Then ' No
            obj_BE_Familiar.CodigoReligion = -1
        End If

        obj_BE_Familiar.NombreIglesia = IIf(tbAdicionalesNombreIglesia.Text.Trim.Length = 0, Nothing, tbAdicionalesNombreIglesia.Text.Trim)
        obj_BE_Familiar.Celular = IIf(tbAdicionalesCelular.Text.Trim.Length = 0, Nothing, tbAdicionalesCelular.Text.Trim)
        obj_BE_Familiar.CodigoServicioRadioDomicilio = IIf(ddlAdicionalesRadio.SelectedValue = 0, -1, ddlAdicionalesRadio.SelectedValue)
        obj_BE_Familiar.NumeroServicioRadioPersonal = IIf(tbAdicionalesNumeroRadio.Text.Trim.Length = 0, Nothing, tbAdicionalesNumeroRadio.Text.Trim)
        obj_BE_Familiar.EmailPersonal = IIf(tbAdicionalesEmail.Text.Trim.Length = 0, Nothing, tbAdicionalesEmail.Text.Trim)

        'Datos Domicilio
        obj_BE_Familiar.CodigoPaisDomicilio = IIf(ddlDomicilioPais.SelectedValue = 0, -1, ddlDomicilioPais.SelectedValue)
        'Validar para paises <> a Perú
        If ddlDomicilioPais.SelectedItem.ToString = "Perú" Then
            obj_BE_Familiar.CodigoUbigeo = ddlDomicilioDepartamento.SelectedValue.ToString & _
                                        ddlDomicilioProvincia.SelectedValue.ToString & _
                                        ddlDomicilioDistrito.SelectedValue.ToString
        Else
            obj_BE_Familiar.CodigoUbigeo = "0000000"
        End If

        obj_BE_Familiar.Urbanizacion = IIf(tbDomicilioUrbanizacion.Text.Trim.Length = 0, Nothing, tbDomicilioUrbanizacion.Text.Trim)
        obj_BE_Familiar.Direccion = IIf(tbDomicilioDireccion.Text.Trim.Length = 0, Nothing, tbDomicilioDireccion.Text.Trim)
        obj_BE_Familiar.ReferenciaDomiciliaria = IIf(tbDomicilioReferencia.Text.Trim.Length = 0, Nothing, tbDomicilioReferencia.Text.Trim)
        obj_BE_Familiar.TelefonoCasa = IIf(tbDomicilioTelefono.Text.Trim.Length = 0, Nothing, tbDomicilioTelefono.Text.Trim)
        obj_BE_Familiar.AccesoInternet = rbDomicilioAccesoInternet.SelectedValue

        'Datos Laborales
        obj_BE_Familiar.codigosituacionlaboral = IIf(ddlSituacionLaboral.SelectedValue = 0, -1, ddlSituacionLaboral.SelectedValue)

        If ddlSituacionLaboral.SelectedValue = 3 Or ddlSituacionLaboral.SelectedValue = 4 Then

            obj_BE_Familiar.OcupacionCargo = IIf(tbOcupacion.Text.Trim.Length = 0, Nothing, tbOcupacion.Text.Trim)
            obj_BE_Familiar.CentroTrabajo = IIf(tbCentroTrabajo.Text.Trim.Length = 0, Nothing, tbCentroTrabajo.Text.Trim)
            obj_BE_Familiar.DireccionCentroTrabajo = IIf(tbTrabajoDireccion.Text.Trim.Length = 0, Nothing, tbTrabajoDireccion.Text.Trim)
            obj_BE_Familiar.CodigoPaisCentroTrabajo = IIf(ddlTrabajoPais.SelectedValue = 0, -1, ddlTrabajoPais.SelectedValue)
            'Validar para paises <> a Perú
            If ddlTrabajoPais.SelectedItem.ToString = "Perú" Then
                obj_BE_Familiar.CodigoUbigeoCentroTrabajo = ddlTrabajoDepartamento.SelectedValue.ToString & _
                                                         ddlTrabajoProvincia.SelectedValue.ToString & _
                                                         ddlTrabajoDistrito.SelectedValue.ToString
            Else
                obj_BE_Familiar.CodigoUbigeoCentroTrabajo = "000000"
            End If

            obj_BE_Familiar.TelefonoOficina = IIf(tbTrabajoTelefono.Text.Trim.Length = 0, Nothing, tbTrabajoTelefono.Text.Trim)
            obj_BE_Familiar.CelularOficina = IIf(tbTrabajoCelular.Text.Trim.Length = 0, Nothing, tbTrabajoCelular.Text.Trim)
            obj_BE_Familiar.CodigoServicioRadioOficina = IIf(ddlTrabajoRadio.SelectedValue = 0, -1, ddlTrabajoRadio.SelectedValue)
            obj_BE_Familiar.NumeroServicioRadioOficina = IIf(tbTrabajoNumeroRadio.Text.Trim.Length = 0, Nothing, tbTrabajoNumeroRadio.Text.Trim)
            obj_BE_Familiar.EmailOficina = IIf(tbTrabajoEmail.Text.Trim.Length = 0, Nothing, tbTrabajoEmail.Text.Trim)
            obj_BE_Familiar.AccesoInternetOficina = rbTrabajoAccesoInternet.SelectedValue

        Else

            obj_BE_Familiar.OcupacionCargo = Nothing
            obj_BE_Familiar.CentroTrabajo = Nothing
            obj_BE_Familiar.DireccionCentroTrabajo = Nothing
            obj_BE_Familiar.CodigoPaisCentroTrabajo = -1
            obj_BE_Familiar.CodigoUbigeoCentroTrabajo = "000000"
            obj_BE_Familiar.TelefonoOficina = Nothing
            obj_BE_Familiar.CelularOficina = Nothing
            obj_BE_Familiar.CodigoServicioRadioOficina = -1
            obj_BE_Familiar.NumeroServicioRadioOficina = Nothing
            obj_BE_Familiar.EmailOficina = Nothing
            obj_BE_Familiar.AccesoInternetOficina = -1

        End If

        'Datos Estudios
        obj_BE_Familiar.ExAlumno = rbEstudiosExAlumno.SelectedValue

        If rbEstudiosExAlumno.SelectedValue = 0 Then ' No
            obj_BE_Familiar.ColegioEgreso = IIf(tbEstudiosColegioEgreso.Text.Trim.Length = 0, Nothing, tbEstudiosColegioEgreso.Text.Trim)
        ElseIf rbEstudiosExAlumno.SelectedValue = 1 Then ' Si
            obj_BE_Familiar.ColegioEgreso = Nothing
        End If

        obj_BE_Familiar.ExAlumnoAnioEgreso = IIf(ddlEstudiosAnioEgreso.SelectedValue = 0, Nothing, ddlEstudiosAnioEgreso.SelectedValue)
        obj_BE_Familiar.ContinuaEstudios = IIf(tbEstudiosContinuo.Text.Trim.Length = 0, Nothing, tbEstudiosContinuo.Text.Trim)
        obj_BE_Familiar.CodigoNivelInstruccion = IIf(ddlEstudiosNivelInstruccion.SelectedValue = 0, -1, ddlEstudiosNivelInstruccion.SelectedValue)
        obj_BE_Familiar.CodigoEscolaridadMinisterio = IIf(ddlEstudiosEscolaridadMinisterio.SelectedValue = 0, -1, ddlEstudiosEscolaridadMinisterio.SelectedValue)


        'Detalle
        Dim objDS_Detalle As New DataSet
        'Detalle Idiomas
        Dim objDT_Idioma As DataTable

        objDT_Idioma = New DataTable("ListaIdiomas")
        objDT_Idioma = Datos.agregarColumna(objDT_Idioma, "Codigo", "String")
        objDT_Idioma = Datos.agregarColumna(objDT_Idioma, "Descripcion", "String")

        Dim dr_Idioma As DataRow
        For Each drv As GridViewRow In GVListaIdiomas.Rows
            dr_Idioma = objDT_Idioma.NewRow
            dr_Idioma.Item("Codigo") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Idioma.Item("Descripcion") = CType(drv.FindControl("Label2"), Label).Text
            objDT_Idioma.Rows.Add(dr_Idioma)
        Next

        'Detalle Profesiones
        Dim objDT_Profesion As DataTable

        objDT_Profesion = New DataTable("ListaProfesions")
        objDT_Profesion = Datos.agregarColumna(objDT_Profesion, "Codigo", "String")
        objDT_Profesion = Datos.agregarColumna(objDT_Profesion, "Descripcion", "String")

        Dim dr_Profesion As DataRow
        For Each drv As GridViewRow In GVListaProfesiones.Rows
            dr_Profesion = objDT_Profesion.NewRow
            dr_Profesion.Item("Codigo") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Profesion.Item("Descripcion") = CType(drv.FindControl("Label2"), Label).Text
            objDT_Profesion.Rows.Add(dr_Profesion)
        Next

        'Detalle Fichas Autos
        Dim objDT_FichaAutos As DataTable

        If ViewState("ListaFichaAutos") Is Nothing Then
            objDT_FichaAutos = New DataTable("ListaAutos")
            objDT_FichaAutos = Datos.agregarColumna(objDT_FichaAutos, "Codigo", "String")
            objDT_FichaAutos = Datos.agregarColumna(objDT_FichaAutos, "CodigoFamiliar", "String")
            objDT_FichaAutos = Datos.agregarColumna(objDT_FichaAutos, "Marca", "String")
            objDT_FichaAutos = Datos.agregarColumna(objDT_FichaAutos, "Modelo", "String")
            objDT_FichaAutos = Datos.agregarColumna(objDT_FichaAutos, "Placa", "String")
        Else
            objDT_FichaAutos = ViewState("ListaFichaAutos")
        End If


        'Agrego las DataTable a mi DataSet
        objDS_Detalle.Tables.Add(objDT_Idioma)
        objDS_Detalle.Tables.Add(objDT_Profesion)
        objDS_Detalle.Tables.Add(objDT_FichaAutos)
        'DS.Tables(0) : FichaAutos
        'If objDT_FichaAutos.Rows(0) IsNot Nothing Then

        'End If



        obj_BE_Familiar.Contrasenia = autogenerarPassword(tbApellidoPaterno.Text.Trim)


        If BoolGrabar = 0 Then ' Nuevo
            usp_valor = obj_BL_Familiar.FUN_INS_Familiares(bool_FichaCompleta, obj_BE_Familiar, objDS_Detalle, usp_mensaje, outparamCodigoPersona, outparamCodigoFamilia, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)

        Else ' Update
            obj_BE_Familiar.CodigoFamiliar = CInt(BoolGrabar)
            obj_BE_Familiar.CodigoPersona = hidenCodigoPersona.Value
            usp_valor = obj_BL_Familiar.FUN_UPD_Familiares(bool_FichaCompleta, obj_BE_Familiar, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)

        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnFichaCancelar_Click()
            listarFichas()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub


    ' Autogenerar password y encriptarlo
    Private Function autogenerarPassword(ByVal str_ApellidoPaterno As String) As String

        Randomize()
        Dim int_Aleatorio As Integer = CInt(Int((999 * Rnd()) + 100))

        Dim int_PwLength As Integer = str_ApellidoPaterno.Length
        Dim str_Password As String

        If int_PwLength >= 5 Then ' Si el apellido paterno tiene más de 5 caracteres
            str_Password = str_ApellidoPaterno.Substring(0, 5)
        Else ' si tiene menos de 5 caracteres
            str_Password = str_ApellidoPaterno.Substring(0, int_PwLength)
        End If

        str_Password = str_Password & int_Aleatorio.ToString

        Dim cr As New Cripto
        str_Password = cr.Encriptar(New RC2CryptoServiceProvider, str_Password)

        Return str_Password

    End Function

    ''' <summary>
    ''' Obtiene los datos de la ficha del familiar
    ''' </summary>
    ''' <param name="int_CodigoFamiliar">codigo del familiar al que se le van a consultar los datos de la ficha</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerFicha(ByVal int_CodigoFamiliar As Integer)

        Dim str_mensajeError As String = ""

        Dim obj_BL_Familiares As New bl_Familiares
        Dim obj_BL_FichaAutos As New bl_FichaAutos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Familiares.FUN_GET_Familiar(int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        'Dim ds_ListaFichaAuto As DataSet = obj_BL_FichaAutos.FUN_GET_FichaAulas(int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)

        hidenCodigoFamiliar.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoFamiliar").ToString
        hidenCodigoPersona.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoPersona").ToString

        'Datos Personales
        tbApellidoPaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString
        tbApellidoMaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString
        tbNombre.Text = ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString
        rbSexo.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoSexo").ToString
        ddlTipoDocumento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoTipoDocIdentidad").ToString
        tbNumDocumento.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString
        ddlEstadoCivil.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoEstadoCivil").ToString
        If CBool(ds_Lista.Tables(0).Rows(0).Item("Vive").ToString) Then
            rbVive.SelectedValue = 1
            tbFechaDefuncion.Text = ""
        Else
            rbVive.SelectedValue = 0
            tbFechaDefuncion.Text = ds_Lista.Tables(0).Rows(0).Item("FechaDefuncion").ToString
        End If
        verificarFechaDefuncion()

        'Datos Nacimiento
        tbFechaNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString
        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") = -1 Then
            ddlNacionalidad.SelectedValue = 0
        Else
            ddlNacionalidad.SelectedValue = ds_Lista.Tables(1).Rows(0).Item("Codigo").ToString
        End If

        'Datos Adicionales
        If CBool(ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion").ToString) Then
            rbAdicionalesProfesaReligion.SelectedValue = 1
        Else
            rbAdicionalesProfesaReligion.SelectedValue = 0
        End If
        ddlAdicionalesReligion.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoReligion").ToString
        tbAdicionalesNombreIglesia.Text = ds_Lista.Tables(0).Rows(0).Item("NombreIglesia").ToString
        verificarProfesaReligion()

        tbAdicionalesCelular.Text = ds_Lista.Tables(0).Rows(0).Item("CelularPersonal").ToString

        ddlAdicionalesRadio.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoServicioRadioDomicilio").ToString
        tbAdicionalesNumeroRadio.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroRadioPersonal").ToString
        verificarRadio(1)

        tbAdicionalesEmail.Text = ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString

        'Datos Domicilio
        ddlDomicilioPais.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoPaisDomicilio").ToString
        ddlDomicilioPais_SelectedIndexChanged()
        If ddlDomicilioDepartamento.Enabled Then
            ddlDomicilioDepartamento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioDepartamento").ToString
            ddlDomicilioDepartamento_SelectedIndexChanged()
            ddlDomicilioProvincia.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioProvincia").ToString
            ddlDomicilioProvincia_SelectedIndexChanged()
            ddlDomicilioDistrito.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioDistrito").ToString
        End If

        tbDomicilioUrbanizacion.Text = ds_Lista.Tables(0).Rows(0).Item("UrbanizacionDomicilio").ToString
        tbDomicilioDireccion.Text = ds_Lista.Tables(0).Rows(0).Item("DireccionDomicilio").ToString
        tbDomicilioReferencia.Text = ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomicilio").ToString
        tbDomicilioTelefono.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoDomicilio").ToString
        If CBool(ds_Lista.Tables(0).Rows(0).Item("AccesoInternetDomicilio").ToString) Then
            rbDomicilioAccesoInternet.SelectedValue = 1
        Else
            rbDomicilioAccesoInternet.SelectedValue = 0
        End If

        'Datos Laborales
        ddlSituacionLaboral.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoSituacionLaboral").ToString
        verificarSituacionLaboral()
        If ddlSituacionLaboral.SelectedValue = 3 Or ddlSituacionLaboral.SelectedValue = 4 Then
            tbOcupacion.Text = ds_Lista.Tables(0).Rows(0).Item("Ocupacion").ToString
            tbCentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("CentroTrabajo").ToString
            tbTrabajoDireccion.Text = ds_Lista.Tables(0).Rows(0).Item("DireccionTrabajo").ToString
            ddlTrabajoPais.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoPaisTrabajo").ToString
            ddlTrabajoPais_SelectedIndexChanged()
            If ddlTrabajoDepartamento.Enabled Then
                ddlTrabajoDepartamento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoDepartamento").ToString
                ddlTrabajoDepartamento_SelectedIndexChanged()
                ddlTrabajoProvincia.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoProvincia").ToString
                ddlTrabajoProvincia_SelectedIndexChanged()
                ddlTrabajoDistrito.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoDistrito").ToString
            End If
            tbTrabajoTelefono.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoTrabajo").ToString
            tbTrabajoCelular.Text = ds_Lista.Tables(0).Rows(0).Item("CelularTrabajo").ToString

            ddlTrabajoRadio.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoServicioRadioTrabajo").ToString
            tbTrabajoNumeroRadio.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroRadioTrabajo").ToString
            verificarRadio(2)

            tbTrabajoEmail.Text = ds_Lista.Tables(0).Rows(0).Item("EmailTrabajo").ToString
            If CBool(ds_Lista.Tables(0).Rows(0).Item("AccesoInternetTrabajo").ToString) Then
                rbTrabajoAccesoInternet.SelectedValue = 1
            Else
                rbTrabajoAccesoInternet.SelectedValue = 0
            End If
        End If

        'Datos Estudios
        If CBool(ds_Lista.Tables(0).Rows(0).Item("ExAlumno").ToString) Then
            rbEstudiosExAlumno.SelectedValue = 1
        Else
            rbEstudiosExAlumno.SelectedValue = 0
        End If
        tbEstudiosColegioEgreso.Text = ds_Lista.Tables(0).Rows(0).Item("ColegioEgreso").ToString
        verificarExAlumno()

        ddlEstudiosAnioEgreso.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("AnioEgreso").ToString()

        tbEstudiosContinuo.Text = ds_Lista.Tables(0).Rows(0).Item("ContinuoEstudios").ToString
        ddlEstudiosNivelInstruccion.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoNivelInstruccion").ToString
        ddlEstudiosEscolaridadMinisterio.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoEscolaridadMinisterio").ToString

        'Detalle Idioma
        Dim objDT_Idioma As DataTable

        objDT_Idioma = New DataTable("ListaIdiomas")
        objDT_Idioma = ds_Lista.Tables(2).Clone

        If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then

            For Each dr As DataRow In ds_Lista.Tables(2).Rows
                objDT_Idioma.ImportRow(dr)
            Next

            ViewState("ListaIdiomas") = objDT_Idioma
            GVListaIdiomas.DataSource = objDT_Idioma
            GVListaIdiomas.DataBind()

        End If

        'Detalle Profesion
        Dim objDT_Profesion As DataTable

        objDT_Profesion = New DataTable("ListaProfesiones")
        objDT_Profesion = ds_Lista.Tables(3).Clone

        If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then

            For Each dr As DataRow In ds_Lista.Tables(3).Rows
                objDT_Profesion.ImportRow(dr)
            Next

            ViewState("ListaProfesiones") = objDT_Profesion
            GVListaProfesiones.DataSource = objDT_Profesion
            GVListaProfesiones.DataBind()

        End If

        'Detalle Ficha Autos
        Dim objDT_FichaAutos As DataTable
        objDT_FichaAutos = New DataTable("ListaFichaAutos")
        objDT_FichaAutos = ds_Lista.Tables(5).Clone
        If ds_Lista.Tables(5).Rows(0).Item("CodigoRelacion") <> -1 Then
            For Each dr As DataRow In ds_Lista.Tables(5).Rows
                objDT_FichaAutos.ImportRow(dr)
            Next

            ViewState("ListaFichaAutos") = objDT_FichaAutos
            GVListaFichaAutos.DataSource = objDT_FichaAutos
            GVListaFichaAutos.DataBind()
        End If

        VerRegistro("Actualización", 2)

    End Sub

    ''' <summary>
    ''' Obtiene los datos de la ficha del familiar, pero se visualizan como solo lectura
    ''' </summary>
    ''' <param name="int_CodigoFamiliar">codigo del familiar al que se le van a consultar los datos de la ficha</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerFicha(ByVal int_CodigoFamiliar As Integer)

        Dim str_mensajeError As String = ""

        Dim obj_BL_Familiares As New bl_Familiares
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Familiares.FUN_GET_Familiar(int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)

        hidenCodigoFamiliar.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoFamiliar").ToString
        hidenCodigoPersona.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoPersona").ToString

        'Datos Personales
        lblVerApellidoPaterno.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString)
        lblVerApellidoMaterno.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString)
        lblVerNombre.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString)
        lblVerSexo.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescSexo").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescSexo").ToString)
        lblVerTipoDocumento.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescTipoDocIdentidad").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescTipoDocIdentidad").ToString)
        lblVerNumDocumento.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString)
        lblVerEstadoCivil.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescEstadoCivil").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescEstadoCivil").ToString)
        lblVerVive.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescVive").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescVive").ToString)
        lblVerFechaDefuncion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("FechaDefuncionStr").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("FechaDefuncionStr").ToString)

        'Datos Nacimiento
        lblVerFechaNacimiento.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString)
        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
            lblVerNacionalidad.Text = IIf(ds_Lista.Tables(1).Rows(0).Item("Descripcion").ToString.Length = 0, "-", ds_Lista.Tables(1).Rows(0).Item("Descripcion").ToString)

        End If

        'Datos Adicionales
        lblVerAdicionalesProfesaReligion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescProfesaReligion").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescProfesaReligion").ToString)
        lblVerAdicionalesReligion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescReligion").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescReligion").ToString)
        lblVerAdicionalesNombreIglesia.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("NombreIglesia").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("NombreIglesia").ToString)
        lblVerAdicionalesCelular.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("CelularPersonal").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("CelularPersonal").ToString)
        lblVerAdicionalesRadio.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescServicioRadioDomicilio").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescServicioRadioDomicilio").ToString)
        lblVerAdicionalesNumeroRadio.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("NumeroRadioPersonal").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("NumeroRadioPersonal").ToString)
        lblVerAdicionalesEmail.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString)

        'Datos Domicilio
        lblVerDomicilioPais.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescPaisDomicilio").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescPaisDomicilio").ToString)
        lblVerDomicilioDepartamento.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescUbigeoDomicilioDepartamento").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescUbigeoDomicilioDepartamento").ToString)
        lblVerDomicilioProvincia.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescUbigeoDomicilioProvincia").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescUbigeoDomicilioProvincia").ToString)
        lblVerDomicilioDistrito.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescUbigeoDomicilioDistrito").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescUbigeoDomicilioDistrito").ToString)
        lblVerDomicilioUrbanizacion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("UrbanizacionDomicilio").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("UrbanizacionDomicilio").ToString)
        lblVerDomicilioDireccion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DireccionDomicilio").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DireccionDomicilio").ToString)
        lblVerDomicilioReferencia.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomicilio").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomicilio").ToString)
        lblVerDomicilioTelefono.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("TelefonoDomicilio").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("TelefonoDomicilio").ToString)
        lblVerDomicilioAccesoInternet.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescAccesoInternetDomicilio").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescAccesoInternetDomicilio").ToString)

        'Datos Laborales
        lblVerSituacionLaboral.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescSituacionLaboral").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescSituacionLaboral").ToString)
        lblVerOcupacion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("Ocupacion").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("Ocupacion").ToString)
        lblVerCentroTrabajo.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("CentroTrabajo").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("CentroTrabajo").ToString)
        lblVerTrabajoDireccion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DireccionTrabajo").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DireccionTrabajo").ToString)
        lblVerTrabajoPais.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescPaisTrabajo").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescPaisTrabajo").ToString)
        lblVerTrabajoDepartamento.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescUbigeoTrabajoDepartamento").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescUbigeoTrabajoDepartamento").ToString)
        lblVerTrabajoProvincia.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescUbigeoTrabajoProvincia").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescUbigeoTrabajoProvincia").ToString)
        lblVerTrabajoDistrito.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescUbigeoTrabajoDistrito").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescUbigeoTrabajoDistrito").ToString)
        lblVerTrabajoTelefono.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("TelefonoTrabajo").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("TelefonoTrabajo").ToString)
        lblVerTrabajoCelular.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("CelularTrabajo").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("CelularTrabajo").ToString)
        lblVerTrabajoRadio.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescServicioRadioTrabajo").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescServicioRadioTrabajo").ToString)
        lblVerTrabajoNumeroRadio.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("NumeroRadioTrabajo").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("NumeroRadioTrabajo").ToString)
        lblVerTrabajoEmail.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EmailTrabajo").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("EmailTrabajo").ToString)
        lblVerTrabajoAccesoInternet.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescAccesoInternetTrabajo").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescAccesoInternetTrabajo").ToString)

        'Datos Estudios
        lblVerEstudiosExAlumno.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescExAlumno").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescExAlumno").ToString)
        lblVerEstudiosColegioEgreso.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("ColegioEgreso").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("ColegioEgreso").ToString)
        lblVerEstudiosAnioEgreso.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("AnioEgreso").ToString = 0, "-", ds_Lista.Tables(0).Rows(0).Item("AnioEgreso").ToString)
        lblVerEstudiosContinuo.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("ContinuoEstudios").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("ContinuoEstudios").ToString)
        lblVerEstudiosNivelInstruccion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescNivelInstruccion").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescNivelInstruccion").ToString)
        lblVerEstudiosEscolaridadMinisterio.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescEscolaridadMinisterio").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescEscolaridadMinisterio").ToString)

        'Detalle Idioma
        Dim objDT_Idioma As DataTable

        objDT_Idioma = New DataTable("ListaIdiomas")
        objDT_Idioma = ds_Lista.Tables(2).Clone

        If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then

            For Each dr As DataRow In ds_Lista.Tables(2).Rows
                objDT_Idioma.ImportRow(dr)
            Next

            ViewState("ListaIdiomas") = objDT_Idioma
            GVListaIdiomas.DataSource = objDT_Idioma
            GVListaIdiomas.DataBind()

        End If

        'Detalle Profesion
        Dim objDT_Profesion As DataTable

        objDT_Profesion = New DataTable("ListaProfesiones")
        objDT_Profesion = ds_Lista.Tables(3).Clone

        If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then

            For Each dr As DataRow In ds_Lista.Tables(3).Rows
                objDT_Profesion.ImportRow(dr)
            Next

            ViewState("ListaProfesiones") = objDT_Profesion
            GVListaProfesiones.DataSource = objDT_Profesion
            GVListaProfesiones.DataBind()

        End If

        VerRegistro("Consulta", 3)

    End Sub

    ''' <summary>
    ''' Elimina los datos temporales de la ficha y regresa a la pestaña de busqueda
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CancelarFicha()

        miTab1.Enabled = True
        miTab2.Enabled = False

        ViewState.Remove("ListaIdiomas")
        ViewState.Remove("ListaProfesiones")
        ViewState.Remove("ListaIntegranteFamilia")
        ViewState.Remove("ListaFichaAutos")

        ViewState("ListaIdiomas") = Nothing
        ViewState("ListaProfesiones") = Nothing
        ViewState("ListaIntegranteFamilia") = Nothing
        ViewState("ListaFichaAutos") = Nothing

        lbTab2.Text = "Inserción"
        TabContainer1.ActiveTabIndex = 0

        limpiarControlesMantenimiento()

    End Sub

    ''' <summary>
    ''' Cambia el estado de la ficha del familiar
    ''' </summary>
    ''' <param name="int_Codigo">codigo del familiar al cual se le quiere cambiar la ficha</param>
    ''' <param name="str_accion">estado al que se desea cambiar la ficha</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_BL_Familiares As New bl_Familiares
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_Familiares.FUN_DEL_Familiares(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        listarFichas()

    End Sub
    ''' <summary>
    ''' Edita 1 Registro Marca, Modelo y Placa del detalle de Control Ficha Auto
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     15/16/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarControlFichaAutos()
        Dim Marca As String
        Dim Modelo As String
        Dim Placa As String
        Dim dt As DataTable
        Dim boolIncremento As Boolean = True
        Dim str_CodigoPlacaOriginal As String

        Marca = tbMarca.Text
        Modelo = tbModelo.Text
        Placa = tbPlaca.Text

        dt = ViewState("ListaFichaAutos")
        str_CodigoPlacaOriginal = hiddenCodigoPlaca.Value

        Dim int_Contador As Integer = 0
        Dim bool_Actualizar As Boolean = False
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("Placa").ToString <> Placa Or Placa = str_CodigoPlacaOriginal Then
                bool_Actualizar = True
            Else
                bool_Actualizar = False
                Exit For
            End If
        Next

        If bool_Actualizar Then


            If Placa <> "" Then
                For Each auxdr As DataRow In dt.Rows

                    If auxdr.Item("Placa").ToString = str_CodigoPlacaOriginal And Placa <> "" Then
                        auxdr.Item("Marca") = Marca
                        auxdr.Item("Modelo") = Modelo
                        auxdr.Item("Placa") = Placa
                        Exit For
                    End If
                Next
            Else
                pnModalFichaAutos.Show()
                MostrarSexyAlertBox("Debe de Ingresar la Placa.", "Alert")
                Exit Sub
            End If


            pnModalFichaAutos.Hide()

            ViewState("ListaFichaAutos") = dt
            Dim dv As DataView = dt.DefaultView
            dv.RowFilter = "1=1 and EstadoRegistro = 1"
            GVListaFichaAutos.DataSource = dv
            GVListaFichaAutos.DataBind()
            UpFichaAutos.Update()
        Else
            pnModalFichaAutos.Show()
            MostrarSexyAlertBox("El número de placa no puede repetirse.", "Alert")
            Exit Sub
        End If


    End Sub
#End Region

#Region "Mantenimiento de Detalle Idioma"

#Region "Eventos"
    Protected Sub btnAgregarDetalleIdioma_Click()
        pnModalIdioma.Show()
    End Sub

    Protected Sub btnModalAceptarIdioma_Click()
        Try
            Dim resulado As Boolean
            agregarIdioma(resulado)
            If resulado = False Then
                pnModalIdioma.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(200, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarIdioma_Click()
        cerrarModalIdioma()
    End Sub

#End Region

#Region "Métodos"
    ''' <summary>
    ''' Agrega 1 idioma al detalle de idiomas
    ''' </summary>
    ''' <param name="resultado">Valor de la variable resultado, indica si se agrego o no el nuevo registro de idioma al detalle</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarIdioma(ByRef resultado As Boolean)

        If ddlIdioma.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            ddlIdioma.SelectedValue = 0
            Exit Sub

        End If

        Dim dt As DataTable

        If ViewState("ListaIdiomas") Is Nothing Then

            dt = New DataTable("ListaIdiomas")

            dt = Datos.agregarColumna(dt, "Codigo", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")

        Else

            dt = ViewState("ListaIdiomas")

        End If

        Dim boolContinuar As Boolean = True

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("Codigo").ToString = ddlIdioma.SelectedValue Then
                    MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                    ddlIdioma.SelectedValue = 0
                    boolContinuar = False
                End If

            Next

        End If

        resultado = boolContinuar

        If boolContinuar Then

            If dt.Rows.Count = 3 Then
                MostrarSexyAlertBox("No puede ingresar mas de 3 idiomas.", "Alert")
                ddlIdioma.SelectedValue = 0
                resultado = True
                Exit Sub
            End If

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("Codigo") = ddlIdioma.SelectedValue
            dr.Item("Descripcion") = ddlIdioma.SelectedItem.ToString

            dt.Rows.Add(dr)

            ViewState("ListaIdiomas") = dt

            GVListaIdiomas.DataSource = dt
            GVListaIdiomas.DataBind()

            ddlIdioma.SelectedValue = 0
            upIdioma.Update()

        End If

    End Sub

    ''' <summary>
    ''' Cierra el popup idioma
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalIdioma()

        pnModalIdioma.Hide()
        ddlIdioma.SelectedValue = 0

    End Sub

    ''' <summary>
    ''' Elimina 1 idioma del detalle de idiomas
    ''' </summary>
    ''' <param name="int_CodigoIdioma">Codigo del idioma que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarIdioma(ByVal int_CodigoIdioma As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaIdiomas")
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("Codigo").ToString = int_CodigoIdioma Then
                auxdr.Delete()
                Exit For
            End If
        Next
        dt.AcceptChanges()
        ViewState("ListaIdiomas") = dt
        GVListaIdiomas.DataSource = dt
        GVListaIdiomas.DataBind()
        upIdioma.Update()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub GVListaIdiomas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) 'Handles GVListaIdiomas.RowCommand
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Eliminar" Then

                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarIdioma(codigo)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub
    Protected Sub GVListaIdiomas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) 'Handles GVListaIdiomas.RowDataBound
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")

            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")

                If ViewState("VerFicha") = True Then
                    btnEliminar.Visible = False
                ElseIf ViewState("VerFicha") = False Then
                    btnEliminar.Visible = True
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub

#End Region

#End Region
#Region "Mantenimiento de Detalle Profesiones"

#Region "Eventos"
    Protected Sub btnAgregarDetalleProfesion_Click()
        pnModalProfesion.Show()
    End Sub

    Protected Sub btnModalAceptarProfesion_Click()
        Try
            Dim resulado As Boolean
            agregarProfesion(resulado)
            If resulado = False Then
                pnModalProfesion.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(200, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarProfesion_Click()
        cerrarModalProfesion()
    End Sub
#End Region
#Region "Métodos"
    ''' <summary>
    ''' Agrega 1 profesión al detalle de profesiones
    ''' </summary>
    ''' <param name="resultado">Valor de la variable resultado, indica si se agrego o no el nuevo registro profesión al detalle</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarProfesion(ByRef resultado As Boolean)

        If ddlProfesion.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            ddlProfesion.SelectedValue = 0
            Exit Sub

        End If

        Dim dt As DataTable

        If ViewState("ListaProfesiones") Is Nothing Then

            dt = New DataTable("ListaProfesiones")

            dt = Datos.agregarColumna(dt, "Codigo", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")

        Else

            dt = ViewState("ListaProfesiones")

        End If

        Dim boolContinuar As Boolean = True

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("Codigo").ToString = ddlProfesion.SelectedValue Then

                    MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                    ddlProfesion.SelectedValue = 0
                    boolContinuar = False

                End If

            Next

        End If

        resultado = boolContinuar

        If boolContinuar Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("Codigo") = ddlProfesion.SelectedValue
            dr.Item("Descripcion") = ddlProfesion.SelectedItem.ToString

            dt.Rows.Add(dr)

            ViewState("ListaProfesiones") = dt

            GVListaProfesiones.DataSource = dt
            GVListaProfesiones.DataBind()

            ddlProfesion.SelectedValue = 0
            upProfesion.Update()

        End If

    End Sub

    ''' <summary>
    ''' Cierra el popup profesión
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalProfesion()

        pnModalProfesion.Hide()
        ddlProfesion.SelectedValue = 0

    End Sub

    ''' <summary>
    ''' Elimina 1 profesión del detalle de profesiones
    ''' </summary>
    ''' <param name="int_CodigoProfesion">Codigo de profesión que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarProfesion(ByVal int_CodigoProfesion As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaProfesiones")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("Codigo").ToString = int_CodigoProfesion Then

                auxdr.Delete()
                Exit For

            End If

        Next

        dt.AcceptChanges()

        ViewState("ListaProfesions") = dt

        GVListaProfesiones.DataSource = dt
        GVListaProfesiones.DataBind()
        upProfesion.Update()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub GVListaProfesiones_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Eliminar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarProfesion(codigo)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaProfesiones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")

            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")

                If ViewState("VerFicha") = True Then
                    btnEliminar.Visible = False
                ElseIf ViewState("VerFicha") = False Then
                    btnEliminar.Visible = True
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region
#End Region
#Region "Mantenimiento de Detalle Ficha Autos"

#Region "Eventos"
    Protected Sub btnAgregarDetalleFichaAutos_Click()
        ViewState("NuevoFichaAutos") = True
        pnModalFichaAutos.Show()
        tbMarca.Text = ""
        tbModelo.Text = ""
        tbPlaca.Text = ""
        hiddenCodigoPlaca.Value = ""
    End Sub

    Protected Sub btnModalAceptarFichaAutos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            ' If validarFicha(usp_mensaje) Then
            If ViewState("NuevoFichaAutos") = False Then
                int_CodigoAccion = 201
                editarControlFichaAutos()
            ElseIf ViewState("NuevoFichaAutos") = True Then
                Dim resultado As Boolean
                int_CodigoAccion = 200
                agregarFichaResultado(resultado)
            End If
            'Else
            'MostrarAlertas(usp_mensaje)
            'End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarFichaAutos_Click()
        cerrarModalFichaAutos()
    End Sub

#End Region
#Region "Métodos"
    ''' <summary>
    ''' Agrega 1 Registro Marca, Modelo y Placa del detalle de Control Ficha Auto
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     15/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarFichaResultado(ByRef resultado As Boolean)

        Dim dt As DataTable

        If ViewState("ListaFichaAutos") Is Nothing Then

            dt = New DataTable("ListaFichaAutos")
            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            dt = Datos.agregarColumna(dt, "Codigo", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoFamiliar", "String")
            dt = Datos.agregarColumna(dt, "Marca", "String")
            dt = Datos.agregarColumna(dt, "Modelo", "String")
            dt = Datos.agregarColumna(dt, "Placa", "String")
            dt = Datos.agregarColumna(dt, "Estado", "Integer")
            dt = Datos.agregarColumna(dt, "Tipo", "String")
            dt = Datos.agregarColumna(dt, "EstadoRegistro", "Integer")

        Else

            dt = ViewState("ListaFichaAutos")

        End If

        Dim boolContinuar As Boolean = True

        If Not tbPlaca.Text.Length > 0 Then
            pnModalFichaAutos.Show()
            MostrarSexyAlertBox("Debe de Ingresar la Placa.", "Alert")
            boolContinuar = False
            Exit Sub
        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows
                If auxdr.Item("Placa").ToString = tbPlaca.Text Then
                    pnModalFichaAutos.Show()
                    MostrarSexyAlertBox("El número de placa no puede repetirse.", "Alert")
                    boolContinuar = False
                    Exit Sub
                End If
            Next

        End If

        resultado = boolContinuar

        If boolContinuar Then

            Dim dr As DataRow
            dr = dt.NewRow
            Dim int_CodigoFamiliar As Integer = hidenCodigoFamiliar.Value
            Dim str_Mensaje As String = ""
            dr.Item("CodigoRelacion") = 0
            dr.Item("Codigo") = 0
            dr.Item("CodigoFamiliar") = int_CodigoFamiliar
            dr.Item("Marca") = tbMarca.Text
            dr.Item("Modelo") = tbModelo.Text
            dr.Item("Placa") = tbPlaca.Text
            dr.Item("Estado") = 1
            dr.Item("Tipo") = "T"
            dr.Item("EstadoRegistro") = 1
            dt.Rows.Add(dr)

            ViewState("ListaFichaAutos") = dt
            Dim dv As DataView = dt.DefaultView
            dv.RowFilter = "1=1 and EstadoRegistro = 1"
            GVListaFichaAutos.DataSource = dv
            GVListaFichaAutos.DataBind()
            UpFichaAutos.Update()
        End If

    End Sub

    ''' <summary>
    ''' Cierra el popup Ficha Autos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     29/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalFichaAutos()

        pnModalFichaAutos.Hide()
        tbMarca.Text = ""
        tbModelo.Text = ""
        tbPlaca.Text = ""

    End Sub
    ''' <summary>
    ''' Elimina 1 Ficha Autos del detalle de FichaAutos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     10/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>    
    Private Sub eliminarFichaAutos(ByVal str_Placa As String)

        Dim dt As DataTable

        dt = ViewState("ListaFichaAutos")
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("Placa").ToString = str_Placa And auxdr.Item("Tipo") = "R" And auxdr.Item("EstadoRegistro") = 1 Then
                auxdr.Item("EstadoRegistro") = 0
                Exit For
            ElseIf auxdr.Item("Placa").ToString = str_Placa And auxdr.Item("Tipo") = "T" Then
                auxdr.Delete()
                Exit For
            End If
        Next
        dt.AcceptChanges()
        ViewState("ListaFichaAutos") = dt
        Dim dv As DataView = dt.DefaultView
        dv.RowFilter = "1=1 and EstadoRegistro = 1"
        GVListaFichaAutos.DataSource = dv
        GVListaFichaAutos.DataBind()
        UpFichaAutos.Update()
    End Sub
    ''' <summary>
    ''' Setea los valores de la grilla detalle de Control Ficha Autos al popup Control Ficha Autos
    ''' </summary>
    ''' <param name="Marca">Marca</param>
    ''' <param name="Modelo">Modelo</param>
    ''' <param name="Placa">Placa</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     15/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarFichaAutos(ByVal int_CodigoFichaAutos As Integer, ByVal Marca As String, ByVal Modelo As String, ByVal Placa As String)
        hiddenCodigoPlaca.Value = Placa
        tbMarca.Text = Marca
        tbModelo.Text = Modelo
        tbPlaca.Text = Placa
        pnModalFichaAutos.Show()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub GVListaFichaAutos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim int_CodigoFichaAuto As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                Dim str_Placa As String = CType(row.FindControl("lbPlaca"), Label).Text
                Dim str_Marca As String = CType(row.FindControl("lbMarca"), Label).Text
                Dim str_Modelo As String = CType(row.FindControl("lbModelo"), Label).Text
                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoFichaAutos") = False
                    activarEditarFichaAutos(int_CodigoFichaAuto, str_Marca, str_Modelo, str_Placa)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarFichaAutos(str_Placa)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub GVListaFichaAutos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) 'Handles GVListaFichaAutos.RowDataBound
        Try
            Dim str_btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                str_btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Eventos Gridview - Busqueda"

    Protected Sub GVListaFamiliar_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Or e.CommandName = "Activar" Or e.CommandName = "Imprimir" Or e.CommandName = "Ver" Then

                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then

                    int_CodigoAccion = 6
                    ViewState("VerFicha") = False
                    ActivarCampos()
                    ObtenerFicha(codigo)

                ElseIf e.CommandName = "Eliminar" And row.Cells(6).Text <> "Inactivo" Then

                    int_CodigoAccion = 3
                    cambiarEstado(codigo, "Eliminar")

                ElseIf e.CommandName = "Activar" And row.Cells(6).Text <> "Activo" Then

                    int_CodigoAccion = 2
                    cambiarEstado(codigo, "Activar")

                ElseIf e.CommandName = "Ver" Then

                    int_CodigoAccion = 5
                    ViewState("VerFicha") = True
                    ActivarCampos()
                    VerFicha(codigo)

                ElseIf e.CommandName = "Imprimir" Then

                    Dim obj_BL_Familiares As New bl_Familiares
                    Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
                    Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
                    Dim ds_Lista As DataSet = obj_BL_Familiares.FUN_GET_Familiar(codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 48)

                    Dim reporte_html As String = ""
                    reporte_html = Exportacion.ExportarReporteFichaFamiliar_Html(ds_Lista, "")
                    Session("Exportaciones_RepFichaFamiliarHtml") = reporte_html
                    ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionFichaFamiliar_html();</script>", False)

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaFamiliar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            Dim btnActivar As ImageButton = e.Row.FindControl("btnActivar")
            Dim btnImprimir As ImageButton = e.Row.FindControl("btnImprimir")
            Dim btnVer As ImageButton = e.Row.FindControl("btnVer")

            If e.Row.RowType = DataControlRowType.Pager Then
                Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
                _TotalPags.Text = GVListaFamiliar.PageCount.ToString

                Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
                _Registros.Text = InformacionPager(GVListaFamiliar, e.Row, Me)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.DataItem("Estado") = "Activo" Then

                    btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                    btnActualizar.Visible = True
                    btnEliminar.Visible = True
                    btnActivar.Visible = False

                Else
                    btnActivar.Attributes.Add("OnClick", "return confirm_activar();")
                    btnActualizar.Visible = False
                    btnEliminar.Visible = False
                    btnActivar.Visible = True
                    e.Row.ForeColor = Drawing.Color.DarkRed
                End If

                btnVer.Visible = True
                btnImprimir.Visible = True

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaFamiliar_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GVListaFamiliar.PageIndex = e.NewPageIndex
            End If

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaFamiliar_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
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

    Protected Sub GVListaFamiliar_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Pager Then
                CrearBotonesPager(GVListaFamiliar, e.Row, Me)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GVListaFamiliar.PageCount Then
                Me.GVListaFamiliar.PageIndex = _NumPag - 1
            Else
                Me.GVListaFamiliar.PageIndex = 0
            End If

            Me.GVListaFamiliar.SelectedIndex = -1

            'listarFichas()
            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos del Gridview Busqueda"

    ''' <summary>
    ''' Lista las fichas de atención ordenadas por un campo especifico
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(2)

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        GVListaFamiliar.DataSource = dv
        GVListaFamiliar.DataBind()

    End Sub

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     24/01/2011
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
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        'Dim _btnSorting_d1 As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_NombreCompleto"), ImageButton)
        'Dim _btnSorting_d2 As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_DescTipoPaciente"), ImageButton)
        'Dim _btnSorting_d3 As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_DescSede"), ImageButton)
        'Dim _btnSorting_d4 As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_FechaHoraAtencionDt"), ImageButton)

        'If _btnSorting.ID = _btnSorting_d1.ID Then

        '    _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d2.ToolTip = "Descendente"
        '    _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d3.ToolTip = "Descendente"
        '    _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d4.ToolTip = "Descendente"

        'ElseIf _btnSorting.ID = _btnSorting_d2.ID Then

        '    _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d1.ToolTip = "Descendente"
        '    _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d3.ToolTip = "Descendente"
        '    _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d4.ToolTip = "Descendente"

        'ElseIf _btnSorting.ID = _btnSorting_d3.ID Then

        '    _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d1.ToolTip = "Descendente"
        '    _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d2.ToolTip = "Descendente"
        '    _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d4.ToolTip = "Descendente"

        'Else

        '    _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d1.ToolTip = "Descendente"
        '    _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d2.ToolTip = "Descendente"
        '    _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d3.ToolTip = "Descendente"

        'End If

        If ViewState("Direccion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
            _btnSorting.ToolTip = "Ascendente"
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
    ''' Fecha de Creación:     24/01/2011
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
    ''' Fecha de Creación:     24/01/2011
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

#Region "Manejo de Alertas - Emails"

    ''' <summary>
    ''' Recibe mensajes y los deriva a otro metodo que los visualizara cno animación de JQuery
    ''' </summary>
    ''' <param name="str_alertas">Mensaje que se quiere visualizar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
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
    ''' Fecha de Creación:     26/01/2011
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
    ''' Fecha de Creación:     24/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(2, 48, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region




End Class
