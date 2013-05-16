Imports System.Security.Cryptography
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Módulo de Registro de Fichas de Alumnos
''' </summary>
''' <remarks>
''' Código del Modulo:    2
''' Código de la Opción:  47
''' </remarks>

Partial Class Modulo_Matricula_frameFichaAlumno
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                cargarCombos()

                verificarPais()
                verificarProfesaReligion()

                If Session("CodigoAlumnoRegistrado") Is Nothing Then
                    Session("CodigoAlumnoRegistrado") = 0
                End If

            Else

                If Session("CodigoAlumnoRegistrado") Is Nothing Then
                    Session("CodigoAlumnoRegistrado") = 0
                End If

            End If
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

    Protected Sub btnCancelar_Click()
        'CancelarFicha()
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            Dim bool_FichaCompleta As Boolean = True
            If Not validarFicha(usp_mensaje) Then
                'GrabarFicha()
                bool_FichaCompleta = False
                'Else
                '    MostrarAlertas(usp_mensaje)
            End If

            GrabarFicha(bool_FichaCompleta)

        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Métodos"


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

        'Datos Personales
        tbApellidoPaterno.Text = ""
        tbApellidoMaterno.Text = ""
        tbNombre.Text = ""
        rbSexo.SelectedValue = Nothing
        ddlTipoDocumento.SelectedValue = 0
        tbNumDocumento.Text = ""

        ' Datos de Nacimiento
        rbNacRegistrado.SelectedValue = Nothing
        tbFechaNacimiento.Text = ""
        ddlPais.SelectedValue = 0
        ddlDepartamento.SelectedValue = 0
        ddlProvincia.SelectedValue = 0
        ddlDistrito.SelectedValue = 0
        ddlNacionalidad1.SelectedValue = 0
        ddlNacionalidad2.SelectedValue = 0

        ' Datos Adicionales
        ddlLenguaMaterna1.SelectedValue = 0
        ddlLenguaMaterna2.SelectedValue = 0
        ddlEstadocivil.SelectedValue = 0
        tbCantidadHermanos.Text = ""
        tbPosicionHermanos.Text = ""
        tbCorreoElectronico.Text = ""
        tbCorreoElectronicoInstitucional.Text = ""
        tbCelular.Text = ""
        rbReligion.SelectedValue = Nothing
        ddlReligion.SelectedValue = 0
        rbBautizo.SelectedValue = Nothing
        tbLugarBautizo.Text = ""
        tbAnioBautizo.Text = ""
        rbPriComunion.SelectedValue = Nothing
        tbLugarPriComunion.Text = ""
        tbAnioPriComunion.Text = ""
        rbConfirmado.SelectedValue = Nothing
        tbLugarConfirmado.Text = ""
        tbAnioConfirmado.Text = ""
        ''  Datos Familiares

        '' Datos de Domicilio

        ddlDomicilioDepartamento.SelectedValue = 0
        ddlDomicilioProvincia.SelectedValue = 0
        ddlDomicilioDistrito.SelectedValue = 0
        tbDomicilioUrbanizacion.Text = ""
        tbDomicilioDireccion.Text = ""
        tbDomicilioReferencia.Text = ""
        tbDomicilioTelefono.Text = ""
        rbDomicilioAccesoInternet.SelectedValue = Nothing

        '' Datos Médicos
        tbNombreCompletoEmergencia.Text = ""
        tbTelfCasaEmergencia.Text = ""
        tbTelfOficinaEmergencia.Text = ""
        tbTelfMovilEmergencia.Text = ""

        ''Datos Especiales
        tbExperienciasTraumaticas.Text = ""

        ''listas
        ViewState("ListaDiscapacidad") = Nothing
        ViewState.Remove("ListaDiscapacidad")

        ViewState("ListaFamilia") = Nothing
        ViewState.Remove("ListaFamilia")

    End Sub

    Private Sub SetearAccionesAcceso()
        RegistrarAccesoPagina(2, 47)

        'CONTROLES DEL FORMULARIO
        'Master.BloqueoControles(btnBuscar, 1)
        'Master.BloqueoControles(btnGrabar, 1)

        'Master.SeteoPermisosAcciones(btnBuscar, 47)
        'Master.SeteoPermisosAcciones(btnGrabar, 47)

    End Sub

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

        cargarComboAnioAcademico()
        cargarComboGrado()
        cargarComboTipoDocumento()
        cargarComboReligiones()
        cargarComboPais()
        cargarComboNacionalidades()
        cargarComboUbigeo()
        cargarComboIdiomas()
        cargarComboEstadoCivil()

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

    Private Sub cargarComboGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlGrados, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    Private Sub cargarComboAnioAcademico()
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)

        'ddlAnioAcademico.SelectedItem.Text = Today.Year

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
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_BL_TipoDocIdentidad.FUN_LIS_TipoDocIdentidad("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlTipoDocumento, ds_Lista, "Codigo", "Descripcion", False, True)

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
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

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
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

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
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

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
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
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
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Idiomas.FUN_LIS_Idiomas("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlLenguaMaterna1, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlLenguaMaterna2, ds_Lista, "Codigo", "Descripcion", False, True)

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
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
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
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
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
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
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
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
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
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
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
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlDepartamento.SelectedValue, ddlProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlDistrito, ds_Lista, "Codigo", "Descripcion", False, True)

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

        Else

            ddlReligion.Enabled = False
            rbBautizo.Enabled = False
            tbLugarBautizo.Enabled = False
            tbAnioBautizo.Enabled = False

            rbPriComunion.Enabled = False
            tbLugarPriComunion.Enabled = False
            tbAnioPriComunion.Enabled = False

            rbConfirmado.Enabled = False
            tbLugarConfirmado.Enabled = False
            tbAnioConfirmado.Enabled = False

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
            'limpiarCombos(ddlDepartamento)
            'limpiarCombos(ddlProvincia)
            'limpiarCombos(ddlDistrito)

            ddlDepartamento.Enabled = False
            ddlProvincia.Enabled = False
            ddlDistrito.Enabled = False
        End If
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
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()

        'Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        Dim int_CodigoAnioIngresa As Integer = ddlAnioAcademico.SelectedValue
        Dim int_CodigoGradoActual As Integer = ddlGrados.SelectedValue

        'Seteo de valores dependientes de RadioButtonList
        If rbReligion.SelectedValue.ToString.Trim.Length = 0 Then

            rbReligion.SelectedValue = 0
            ddlReligion.SelectedValue = 0
            rbBautizo.SelectedValue = 0
            tbLugarBautizo.Text = ""
            tbAnioBautizo.Text = 0
            rbPriComunion.SelectedValue = 0
            tbLugarPriComunion.Text = ""
            tbAnioPriComunion.Text = 0
            rbConfirmado.SelectedValue = 0
            tbLugarConfirmado.Text = ""
            tbAnioConfirmado.Text = 0

        ElseIf rbReligion.SelectedValue = 0 Then 'No

            ddlReligion.SelectedValue = 0
            rbBautizo.SelectedValue = 0
            tbLugarBautizo.Text = ""
            tbAnioBautizo.Text = 0
            rbPriComunion.SelectedValue = 0
            tbLugarPriComunion.Text = ""
            tbAnioPriComunion.Text = 0
            rbConfirmado.SelectedValue = 0
            tbLugarConfirmado.Text = ""
            tbAnioConfirmado.Text = 0

        ElseIf rbReligion.SelectedValue = 1 Then 'Si

            If ddlReligion.SelectedItem.ToString = "Ninguno" Then 'Sino pertenece a ninguna religion

                rbBautizo.SelectedValue = 0
                tbLugarBautizo.Text = ""
                tbAnioBautizo.Text = 0
                rbPriComunion.SelectedValue = 0
                tbLugarPriComunion.Text = ""
                tbAnioPriComunion.Text = 0
                rbConfirmado.SelectedValue = 0
                tbLugarConfirmado.Text = ""
                tbAnioConfirmado.Text = 0

            Else

                If rbBautizo.SelectedValue = 0 Then
                    tbLugarBautizo.Text = ""
                    tbAnioBautizo.Text = 0 : End If

                If ddlReligion.SelectedItem.ToString = "Católica" Or ddlReligion.SelectedItem.ToString = "Cristiana" Then ' Si su religion es Católica o Cristiana

                    If rbPriComunion.SelectedValue = 0 Then
                        tbLugarPriComunion.Text = ""
                        tbAnioPriComunion.Text = 0 : End If

                    If rbConfirmado.SelectedValue = 0 Then
                        tbLugarConfirmado.Text = ""
                        tbAnioConfirmado.Text = 0 : End If

                Else ' Si su religion no es Católica ni Cristiana
                    rbPriComunion.SelectedValue = 0
                    tbLugarPriComunion.Text = ""
                    tbAnioPriComunion.Text = 0
                    rbConfirmado.SelectedValue = 0
                    tbLugarConfirmado.Text = ""
                    tbAnioConfirmado.Text = 0
                End If
            End If

        End If



        'Datos Personales
        obj_BE_Alumno.ApellidoPaterno = tbApellidoPaterno.Text
        obj_BE_Alumno.ApellidoMaterno = tbApellidoMaterno.Text
        obj_BE_Alumno.Nombre = tbNombre.Text
        obj_BE_Alumno.CodigoSexo = rbSexo.SelectedValue
        obj_BE_Alumno.CodigoTipoDocIdentidad = ddlTipoDocumento.SelectedValue
        obj_BE_Alumno.NumeroDocIdentidad = tbNumDocumento.Text
        obj_BE_Alumno.NacimientoRegistrado = IIf(rbNacRegistrado.SelectedValue.ToString.Length > 0, rbNacRegistrado.SelectedValue, -1)
        obj_BE_Alumno.FechaNacimiento = tbFechaNacimiento.Text
        obj_BE_Alumno.CodigoPais = ddlPais.SelectedValue
        obj_BE_Alumno.CodigoNacimientoUbigeo = ddlDepartamento.SelectedValue.ToString & ddlProvincia.SelectedValue.ToString & ddlDistrito.SelectedValue.ToString
        obj_BE_Alumno.CodigoEstadoCivil = ddlEstadocivil.SelectedValue
        obj_BE_Alumno.CantidadHermanos = Val(tbCantidadHermanos.Text)
        obj_BE_Alumno.PosicionEntreHermanos = Val(tbPosicionHermanos.Text)
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


        obj_BE_Alumno.CodigoIdioma1 = ddlLenguaMaterna1.SelectedValue
        obj_BE_Alumno.CodigoIdioma2 = ddlLenguaMaterna2.SelectedValue

        obj_BE_Alumno.CodigoNacionalidades1 = ddlNacionalidad1.SelectedValue
        obj_BE_Alumno.CodigoNacionalidades2 = ddlNacionalidad2.SelectedValue


        ''Detalle
        'Dim objDS_Detalle As New DataSet

        ''Detalle Discapacidades
        'Dim objDT_Discapacidad As DataTable

        'objDT_Discapacidad = New DataTable("ListaDiscapacidad")
        'objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "CodigoRelTipoDiscapAlum", "String")
        'objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "CodigoTipoDiscapacidad", "String")
        'objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "TipoDiscapacidad", "String")
        'objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "DescripcionDiscapacidad", "String")

        'Dim dr_Discapacidad As DataRow

        'For Each drv As GridViewRow In gvDetalleDiscapacidad.Rows

        '    dr_Discapacidad = objDT_Discapacidad.NewRow
        '    dr_Discapacidad.Item("CodigoRelTipoDiscapAlum") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
        '    dr_Discapacidad.Item("CodigoTipoDiscapacidad") = CType(drv.FindControl("lblCodigoDiscapacidad"), Label).Text
        '    dr_Discapacidad.Item("TipoDiscapacidad") = CType(drv.FindControl("lblDiscapacidad"), Label).Text
        '    dr_Discapacidad.Item("DescripcionDiscapacidad") = CType(drv.FindControl("lblDescripcionDiscapacidad"), Label).Text
        '    objDT_Discapacidad.Rows.Add(dr_Discapacidad)

        'Next

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
        'objDS_Detalle.Tables.Add(objDT_Discapacidad)
        'objDS_Detalle.Tables.Add(objDT_Familia)


        obj_BE_Alumno.Contrasenia = autogenerarPassword(tbApellidoPaterno.Text.Trim)


        usp_valor = obj_BL_Alumno.FUN_INS_Alumno(bool_FichaCompleta, int_CodigoAnioIngresa, int_CodigoGradoActual, _
            obj_BE_Alumno, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            Session("CodigoAlumnoRegistrado") = usp_valor
            'limpiarCampos()
            Cerrar()
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

    Private Sub Cerrar()

        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "alert_cerrar", "window.close();", True)

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

        If rbReligion.SelectedValue.ToString.Trim.Length > 0 Then
            If rbReligion.SelectedValue = 1 Then
                If ddlReligion.SelectedValue = 0 Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Religión que profesa")
                    result = False
                End If
            End If
        Else
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Religión que profesa")
            result = False
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

#End Region

#Region "Mantenimiento Detalle Familiares"

#Region "Gridview"

    Protected Sub gvFamiliares_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Dim btnVer As ImageButton = e.Row.FindControl("btnVer")
                'Dim int_CodigoFamiliar As Integer = CType(e.Row.FindControl("lblCodigoFamiliar"), Label).Text

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

                'btnVer.Attributes.Add("onclick", "abrirPopupParams('/SaintGeorgeOnline/Modulo_Matricula/Fpanel.aspx?CodigoFamiliar=" & int_CodigoFamiliar & "' ,'Actualizar')")

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
    ''' Fecha de Creación:     23/01/2012
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
    ''' Fecha de Creación:     23/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     23/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_CodigoUsuario As String = Obtener_CodigoUsuarioLogueado()
        Dim int_TipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(2, 48, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region

#Region "Métodos Master Page"

    ''' <summary>
    ''' Obtiene el código del usuario logueado al sistema
    ''' </summary>
    ''' <returns>código de usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_CodigoUsuarioLogueado() As Integer

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

    ''' <summary>
    ''' Registra el acceso al formulario. (Log de Accesos)
    ''' </summary>
    ''' <param name="int_CodigoSubBloque">Codigo del SubBloque de Menú.</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub RegistrarAccesoPagina(ByVal int_CodigoModulo As Integer, ByVal int_CodigoSubBloque As Integer)
        Dim obj_BL_Usuario As New bl_Logueo
        Dim Info_Acceso As String = ""
        Dim encript As New SaintGeorgeOnline_Utilities.Cripto
        Dim int_CodigoSession As Integer = 0
        Dim str_Info As String = ""
        Dim str_ArrayDatos() As String
        Dim int_CodigoUsuario As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim ticket As FormsAuthenticationTicket = identity.Ticket

            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")
            int_CodigoSession = str_ArrayDatos(5)
            int_CodigoUsuario = str_ArrayDatos(0)
            int_CodigoTipoUsuario = str_ArrayDatos(1)

            obj_BL_Usuario.FUN_INS_AccesoUsuarioDetalle(int_CodigoSession, int_CodigoModulo, int_CodigoSubBloque, int_CodigoUsuario, int_CodigoTipoUsuario)
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub MostrarMensaje(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Dim str_Script As String = ""
        str_Script = SaintGeorgeOnline_Utilities.Alertas.ObtenerMensaje(str_Mensaje, str_TipoMensaje)
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", str_Script, True)

    End Sub

    ''' <summary>
    ''' Obtiene el código del tipo de usuario logueado al sistema
    ''' </summary>
    ''' <returns>código de tipo de usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_CodigoTipoUsuarioLogueado() As Integer

        Dim int_CodigoTipoUsuarioLogueado As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_CodigoTipoUsuarioLogueado = str_ArrayDatos(1)

        Catch ex As Exception
            int_CodigoTipoUsuarioLogueado = -1
        End Try

        Return int_CodigoTipoUsuarioLogueado

    End Function

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     23/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub MostrarMensajeAlert(ByVal str_Mensaje As String)

        Dim str_AlertMsn As String = str_Mensaje

        str_AlertMsn = str_AlertMsn.Replace("<ul>", "\n")
        str_AlertMsn = str_AlertMsn.Replace("<li>", "\n")
        str_AlertMsn = str_AlertMsn.Replace("</li>", "")
        str_AlertMsn = str_AlertMsn.Replace("<em>", "")
        str_AlertMsn = str_AlertMsn.Replace("</em>", "")
        str_AlertMsn = str_AlertMsn.Replace("</ul>", "\n")

        Dim str_Script As String = ""
        str_Script = "alert(' " & str_AlertMsn & " ');"
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", str_Script, True)

    End Sub

#End Region

End Class
