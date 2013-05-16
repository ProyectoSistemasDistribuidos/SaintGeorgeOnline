﻿Imports System.Security.Cryptography
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo

Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Imports System.Data
Imports System.Data.SqlClient
Imports SaintGeorgeOnline_Utilities

''' <summary>
''' Modulo de Mantenimiento de Enfermedades
''' </summary>
''' <remarks>
''' Código del Modulo:    4
''' Código de la Opción:  73
''' </remarks>
Partial Class Interfaz_Familia_Modulo_SolicitudesActualizacionInformacion_SolicitudFichaFamiliar
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 0
    Private cod_Opcion As Integer = 11

#Region "Pagina"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then

                btnFichaCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                btnFichaGrabar.Attributes.Add("OnClick", "return confirm_grabar();")

                btnIraPaso2.Visible = False
                SetearAccionesAcceso()
         
                cargarOpcionesMantenimiento()
                ViewState("VerFicha") = False
                ActivarCampos()

                If Not Request.QueryString("codigoFamilia") Is Nothing Then

                    ' Guardo los datos referente a la matrícula del alumno
                    Dim arr_Datos As String() = Session("ActualizacionDatosMatricula").ToString.Split(",")

                    hiddenCodigoAlumno.Value = arr_Datos(0).ToString
                    hiddenCodigoAnioAcademico.Value = arr_Datos(1).ToString
                    hiddenCodigoFamiliar.Value = arr_Datos(2).ToString
                    hiddenCodigoNivel.Value = arr_Datos(3).ToString
                    hiddenCodigoGrado.Value = arr_Datos(4).ToString
                    hiddenNivel.Value = arr_Datos(5).ToString
                    hiddenGrado.Value = arr_Datos(6).ToString
                    hiddenNombreCompleto.Value = arr_Datos(7).ToString
                    hiddenFoto.Value = arr_Datos(8).ToString
                    hiddenCodigoFamilia.Value = arr_Datos(9).ToString

                    Dim int_CodigoFamilia As Integer = Convert.ToInt32(Request.QueryString("codigoFamilia"))
                    obtenerDatosFamilia(int_CodigoFamilia)

                Else
                    MostrarSexyAlertBox("Debe seleccionar un Familiar", "Alert")
                End If

                ' Activo la grilla de lista de Famiiares y bloqueo el panel de actualización de datos
                gvDetalleIntegrantesFamilia.Enabled = True
                miPanelFamiliar.Enabled = False

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Private Sub SetearAccionesAcceso()
        RegistrarAccesoPagina(4, 73)
    End Sub

    Protected Sub btnIraPaso2_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            ' Si se registra la solicitud, registro en el log de pasos de matrícula
            Dim int_CodigoPasoMatricula As Integer = 6
            RegistrarPasoMatricula(int_CodigoPasoMatricula, hiddenCodigoAnioAcademico.Value, hiddenCodigoAlumno.Value, hiddenCodigoFamiliar.Value, 0)

            btnFichaCancelar_Click()

            ' Recargo la página padre
            CargarPaginaPadre()

        Catch ex As Exception

        End Try

    End Sub



#Region "Profesion"

    Protected Sub btnCerrarModalDocumento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
     
        pnModalAgregarDocumento.Hide()
        pnModalProfesion.Show()

    End Sub


    Protected Sub btnGrabarDocumento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validarDocumento(usp_mensaje) Then
                GrabarDescProfesiones()
            Else
                MostrarMensajeAlert(usp_mensaje)
                pnModalProfesion.Show()
                pnModalAgregarDocumento.Show()
                Exit Sub
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try


    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        pnModalProfesion.Show()
        pnModalAgregarDocumento.Show()
    End Sub
#End Region

#End Region

#Region "Eventos"

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
        Try
            verificarPais(1)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
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
        Try
            verificarPais(2)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
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

        'Dim usp_mensaje As String = ""

        'If validarDisponibilidad(usp_mensaje) Then

        '    Disponibilidad()

        'Else

        '    MostrarAlertas(usp_mensaje)

        'End If

    End Sub

    Protected Sub btnFichaGrabar_click()
        Try
            Dim usp_mensaje As String = ""
            If validarFicha(usp_mensaje) Then
                GrabarFicha()
            Else
                MostrarAlertas(usp_mensaje)
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnFichaCancelar_Click()

        Cancelar()
        ' Activo la grilla de lista de Famiiares y bloqueo el panel de actualización de datos
        gvDetalleIntegrantesFamilia.Enabled = True
        miPanelFamiliar.Enabled = False

        'CargarPaginaPadre()

    End Sub

#End Region

#Region "Métodos"

#Region "Profesion"

    Private Function validarDocumento(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If tbDescripcion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Descripción")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function


    Public Sub GrabarDescProfesiones()

        Dim obj_BE_Profesiones As New be_Profesiones
        Dim obj_BL_Profesiones As New bl_Profesiones
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

        obj_BE_Profesiones.Descripcion = Me.tbDescripcion.Text.Trim
        usp_valor = obj_BL_Profesiones.FUN_INS_Profesion(obj_BE_Profesiones, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then

            MostrarSexyAlertBox(usp_mensaje, "Info")
            pnModalProfesion.Show()
            cargarComboProfesiones()
            ddlProfesion.SelectedValue = usp_valor
            tbDescripcion.Text = ""
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
            pnModalProfesion.Show()
            pnModalAgregarDocumento.Show()

        End If

        'If Me.tieneModalPadre Then
        '    Me.mostrarModalPadre()
        'End If


    End Sub

#End Region

    Private Sub CamposFamiliarVive(ByVal int_Modo As Integer)

        Dim bool_Controles As Boolean = True

        If int_Modo = 1 Then 'Habilitar  
            bool_Controles = True
        ElseIf int_Modo = 2 Then 'Deshabilitar
            bool_Controles = False
        End If

        tbFechaNacimiento.Enabled = bool_Controles
        ddlNacionalidad.Enabled = bool_Controles
        rbAdicionalesProfesaReligion.Enabled = bool_Controles
        ddlAdicionalesReligion.Enabled = bool_Controles
        tbAdicionalesNombreIglesia.Enabled = bool_Controles
        tbAdicionalesCelular.Enabled = bool_Controles
        ddlAdicionalesRadio.Enabled = bool_Controles
        tbAdicionalesNumeroRadio.Enabled = bool_Controles
        tbAdicionalesEmail.Enabled = bool_Controles

        ddlDomicilioPais.Enabled = bool_Controles
        ddlDomicilioDepartamento.Enabled = bool_Controles
        ddlDomicilioProvincia.Enabled = bool_Controles
        ddlDomicilioDistrito.Enabled = bool_Controles
        tbDomicilioUrbanizacion.Enabled = bool_Controles
        tbDomicilioDireccion.Enabled = bool_Controles
        tbDomicilioReferencia.Enabled = bool_Controles
        tbDomicilioTelefono.Enabled = bool_Controles
        rbDomicilioAccesoInternet.Enabled = bool_Controles

        ddlSituacionLaboral.Enabled = bool_Controles
        tbOcupacion.Enabled = bool_Controles
        tbCentroTrabajo.Enabled = bool_Controles
        tbTrabajoDireccion.Enabled = bool_Controles
        ddlTrabajoPais.Enabled = bool_Controles
        ddlTrabajoDepartamento.Enabled = bool_Controles
        ddlTrabajoProvincia.Enabled = bool_Controles
        ddlTrabajoDistrito.Enabled = bool_Controles
        tbTrabajoTelefono.Enabled = bool_Controles
        tbTrabajoCelular.Enabled = bool_Controles
        ddlTrabajoRadio.Enabled = bool_Controles
        tbTrabajoNumeroRadio.Enabled = bool_Controles
        tbTrabajoEmail.Enabled = bool_Controles
        rbTrabajoAccesoInternet.Enabled = bool_Controles

        rbEstudiosExAlumno.Enabled = bool_Controles
        tbEstudiosColegioEgreso.Enabled = bool_Controles
        ddlEstudiosAnioEgreso.Enabled = bool_Controles
        tbEstudiosContinuo.Enabled = bool_Controles
        ddlEstudiosNivelInstruccion.Enabled = bool_Controles

        EstadoDatosFamiliar(bool_Controles)

    End Sub


    ''' <summary>
    ''' Carga y setea los campos del formulario a sus valores por defecto
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
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
        'cargarComboEscolaridadMinisterio()

        cargarCombosServiciosRadio()
        estadoNumeroRadio(False)

        cargarComboIdiomas()
        cargarComboProfesiones()

        cargarComboAnioEgreso()

    End Sub

    ''' <summary>
    ''' Activa la pestaña que contiene los datos del formulario, asi como los controles del detalle 
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ActivarCampos()

        'TabContainer2.Enabled = True
        EstadoDatosFamiliar(True)

    End Sub

    ''' <summary>
    ''' Configura el estado de los campos del formulario dependiendo de los parametros que recibe
    ''' </summary> 
    ''' <param name="str_Modo">Indica el modo de visualizacion de los datos del formulario</param>
    ''' <param name="int_Modo">Indica el nombre que tendra la cabecera de la pestaña</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String, ByVal int_Modo As Integer)

        If int_Modo = 1 Then ' 1 : Nuevo / 2 : Actualizar / 3 : Ver

            ModoMantenimiento(1)
            btnAgregarDetalleIdioma.Style.Remove("display")
            btnAgregarDetalleProfesion.Style.Remove("display")
            btnFichaGrabar.Visible = True
            image1.Visible = True
            image2.Visible = True

        ElseIf int_Modo = 2 Then

            ModoMantenimiento(2)
            btnAgregarDetalleIdioma.Style.Remove("display")
            btnAgregarDetalleProfesion.Style.Remove("display")
            btnFichaGrabar.Visible = True
            image1.Visible = True
            image2.Visible = True

        ElseIf int_Modo = 3 Then

            ModoMantenimiento(3)
            btnAgregarDetalleIdioma.Style.Add("display", "none")
            btnAgregarDetalleProfesion.Style.Add("display", "none")
            btnFichaGrabar.Visible = False
            image1.Visible = False
            image2.Visible = False

        End If

    End Sub

    ''' <summary>
    ''' Configura el estado de los campos del formulario dependiendo de los parametros que recibe
    ''' </summary> 
    ''' <param name="int_Modo">Indica el nombre que tendra la cabecera de la pestaña</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
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
        'lblVerApellidoPaterno.Visible = bool_Etiketas
        'lblVerApellidoMaterno.Visible = bool_Etiketas
        'lblVerNombre.Visible = bool_Etiketas
        'lblVerSexo.Visible = bool_Etiketas
        'lblVerTipoDocumento.Visible = bool_Etiketas
        'lblVerNumDocumento.Visible = bool_Etiketas
        'lblVerEstadoCivil.Visible = bool_Etiketas
        'lblVerVive.Visible = bool_Etiketas
        'lblVerFechaDefuncion.Visible = bool_Etiketas

        'lblVerFechaNacimiento.Visible = bool_Etiketas
        'lblVerNacionalidad.Visible = bool_Etiketas
        'lblVerAdicionalesProfesaReligion.Visible = bool_Etiketas
        'lblVerAdicionalesReligion.Visible = bool_Etiketas
        'lblVerAdicionalesNombreIglesia.Visible = bool_Etiketas
        'lblVerAdicionalesCelular.Visible = bool_Etiketas
        'lblVerAdicionalesRadio.Visible = bool_Etiketas
        'lblVerAdicionalesNumeroRadio.Visible = bool_Etiketas
        'lblVerAdicionalesEmail.Visible = bool_Etiketas

        'lblVerDomicilioPais.Visible = bool_Etiketas
        'lblVerDomicilioDepartamento.Visible = bool_Etiketas
        'lblVerDomicilioProvincia.Visible = bool_Etiketas
        'lblVerDomicilioDistrito.Visible = bool_Etiketas
        'lblVerDomicilioUrbanizacion.Visible = bool_Etiketas
        'lblVerDomicilioDireccion.Visible = bool_Etiketas
        'lblVerDomicilioReferencia.Visible = bool_Etiketas
        'lblVerDomicilioTelefono.Visible = bool_Etiketas
        'lblVerDomicilioAccesoInternet.Visible = bool_Etiketas

        'lblVerSituacionLaboral.Visible = bool_Etiketas
        'lblVerOcupacion.Visible = bool_Etiketas
        'lblVerCentroTrabajo.Visible = bool_Etiketas
        'lblVerTrabajoDireccion.Visible = bool_Etiketas
        'lblVerTrabajoPais.Visible = bool_Etiketas
        'lblVerTrabajoDepartamento.Visible = bool_Etiketas
        'lblVerTrabajoProvincia.Visible = bool_Etiketas
        'lblVerTrabajoDistrito.Visible = bool_Etiketas
        'lblVerTrabajoTelefono.Visible = bool_Etiketas
        'lblVerTrabajoCelular.Visible = bool_Etiketas
        'lblVerTrabajoRadio.Visible = bool_Etiketas
        'lblVerTrabajoNumeroRadio.Visible = bool_Etiketas
        'lblVerTrabajoEmail.Visible = bool_Etiketas
        'lblVerTrabajoAccesoInternet.Visible = bool_Etiketas

        'lblVerEstudiosExAlumno.Visible = bool_Etiketas
        'lblVerEstudiosColegioEgreso.Visible = bool_Etiketas
        'lblVerEstudiosAnioEgreso.Visible = bool_Etiketas
        'lblVerEstudiosContinuo.Visible = bool_Etiketas
        'lblVerEstudiosNivelInstruccion.Visible = bool_Etiketas
        'lblVerEstudiosEscolaridadMinisterio.Visible = bool_Etiketas

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
        ddlEstudiosAnioEgreso.Visible = bool_Controles
        tbEstudiosContinuo.Visible = bool_Controles
        ddlEstudiosNivelInstruccion.Visible = bool_Controles
        'ddlEstudiosEscolaridadMinisterio.Visible = bool_Controles

    End Sub

    ''' <summary>
    ''' Elimina los elementos de la lista
    ''' </summary>
    ''' <param name="combo">Nombre del combobox</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)

        Controles.limpiarCombo(combo, False, True)

    End Sub

    ''' <summary>
    ''' Limpia los elementos de las listas de ubigeo (Departamento, Provincia y Distrito)
    ''' </summary>
    ''' <param name="tab">Indica si el combo pertenece al bloque de información de domicilio o trabajo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub estadoNumeroRadio(ByVal bool_estado As Boolean)

        tbTrabajoNumeroRadio.Enabled = bool_estado
        tbAdicionalesNumeroRadio.Enabled = bool_estado

    End Sub

#Region "Ficha Familiar"

    ''' <summary>
    ''' Cambia el estado de los botones de detalle del formulario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EstadoDatosFamiliar(ByVal bool_Estado As Boolean)

        If bool_Estado Then
            btnAgregarDetalleIdioma.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
            btnAgregarDetalleProfesion.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
            btnAgregarDetalleAuto.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
            btnAgregarDetalleIdioma.Enabled = True
            btnAgregarDetalleProfesion.Enabled = True
            btnAgregarDetalleAuto.Enabled = True
        Else
            btnAgregarDetalleIdioma.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_0.png"
            btnAgregarDetalleProfesion.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_0.png"
            btnAgregarDetalleAuto.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_0.png"
            btnAgregarDetalleIdioma.Enabled = False
            btnAgregarDetalleProfesion.Enabled = False
            btnAgregarDetalleAuto.Enabled = False
        End If

    End Sub



    ''' <summary>
    ''' Elimina las listas en memoria(ViewState) y cierra el formulario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Cancelar()

        ViewState.Remove("ListaIdiomas")
        ViewState.Remove("ListaProfesiones")
        ViewState.Remove("ListaAutos")

        ViewState("ListaIdiomas") = Nothing
        ViewState("ListaProfesiones") = Nothing
        ViewState("ListaAutos") = Nothing

        GVListaIdiomas.DataBind()
        GVListaProfesiones.DataBind()
        GVListaAutos.DataBind()
    End Sub

    ''' <summary>
    ''' Verifica si el codigo enviado ya existe en el arreglo
    ''' </summary>
    ''' <param name="arrList">Arreglo de códigos</param>
    ''' <param name="item">Código a buscar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function verificarArreglo(ByVal arrList As ArrayList, ByVal item As Integer) As ArrayList

        Dim addTo As Boolean = True

        If arrList.Count = 0 Then
            addTo = True
        Else
            For i As Integer = 0 To arrList.Count - 1
                If arrList.Item(i) = item Then
                    addTo = False
                End If
            Next
        End If

        If addTo Then
            arrList.Add(item)
        End If

        Return arrList

    End Function

    ''' <summary>
    ''' Devuelve una cadena con los códigos de los perfiles de usuario
    ''' </summary>
    ''' <param name="dtPerfilBloque">DataTable que contiene los bloques de información vinculados al código de perfil</param>
    ''' <param name="arrListaBloques">Arreglo de códigos de bloques de información</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function obtenerPerfiles(ByVal dtPerfilBloque As DataTable, ByVal arrListaBloques As ArrayList) As String

        Dim str_ListaPerfiles As String = ""
        Dim arrPerfiles As New ArrayList

        For i As Integer = 0 To arrListaBloques.Count - 1
            For Each dr As DataRow In dtPerfilBloque.Rows
                If dr.Item("CodigoBloqueInformacion") = arrListaBloques(i) Then
                    arrPerfiles = verificarArreglo(arrPerfiles, dr.Item("CodigoPerfil"))
                End If
            Next
        Next

        For i As Integer = 0 To arrPerfiles.Count - 1
            If i = 0 Then
                str_ListaPerfiles = arrPerfiles.Item(i)
            Else
                str_ListaPerfiles = str_ListaPerfiles & "," & arrPerfiles.Item(i)
            End If
        Next

        Return str_ListaPerfiles

    End Function

    ''' <summary>
    ''' Valida la ficha del familiar antes de grabar
    ''' </summary>
    ''' <param name="str_Mensaje">Cadena de texto que tendra todos los mensajes de error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
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
        ElseIf rbVive.SelectedValue = 1 Then


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

        End If



        'If ddlEstudiosEscolaridadMinisterio.SelectedValue = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Escolaridad Ministerio")
        '    result = False
        'End If

        'Dim dt As DataTable
        'If ViewState("ListaIdiomas") Is Nothing Then

        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Idiomas")
        '    result = False

        'Else

        '    dt = ViewState("ListaIdiomas")
        '    If dt.Rows.Count = 0 Then

        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Idiomas")
        '        result = False

        '    End If

        'End If


        str_Mensaje = str_alertas
        Return result

    End Function


    Private Sub obtenerDatosFamilia(ByVal int_CodigoFamilia As Integer)

        Dim obj_BL_Familia As New bl_Familia
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()

        Dim int_CodigoAnioAcademico As Integer = hiddenCodigoAnioAcademico.Value

        Dim ds_Lista As DataSet = obj_BL_Familia.FUN_GET_FamiliaresConSolicitudActualizacion(int_CodigoFamilia, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("ListaIntegrantes") = ds_Lista.Tables(0)

        gvDetalleIntegrantesFamilia.DataSource = ds_Lista.Tables(0)
        gvDetalleIntegrantesFamilia.DataBind()

        Dim int_CountFamiliaresValidados As Integer = ds_Lista.Tables(0).Compute("sum(TieneSolicitud)", "")
        Dim int_CountFamiliares As Integer = ds_Lista.Tables(0).Rows.Count

        If int_CountFamiliaresValidados > 0 Then

            If int_CountFamiliaresValidados = int_CountFamiliares Then
                btnIraPaso2.Visible = True
            End If

        Else
            btnIraPaso2.Visible = False
        End If

    End Sub


    Private Sub obtenerDatosFamiliaRecarga(ByVal int_CodigoFamilia As Integer)

        Dim obj_BL_Familia As New bl_Familia
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()

        Dim int_CodigoAnioAcademico As Integer = hiddenCodigoAnioAcademico.Value

        Dim ds_Lista As DataSet = obj_BL_Familia.FUN_GET_FamiliaresConSolicitudActualizacion(int_CodigoFamilia, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("ListaIntegrantes") = ds_Lista.Tables(0)

        gvDetalleIntegrantesFamilia.DataSource = ds_Lista.Tables(0)
        gvDetalleIntegrantesFamilia.DataBind()

        Dim int_CountFamiliaresValidados As Integer = ds_Lista.Tables(0).Compute("sum(TieneSolicitud)", "")
        Dim int_CountFamiliares As Integer = ds_Lista.Tables(0).Rows.Count

        If int_CountFamiliaresValidados > 0 Then

            'btnIraPaso2.Visible = True

            If int_CountFamiliaresValidados = int_CountFamiliares Then
                CargarPaginaPadre()
            End If

        Else
            btnIraPaso2.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Obtiene los datos de la ficha del familiar
    ''' </summary>
    ''' <param name="int_CodigoFamiliar">codigo del familiar al que se le van a consultar los datos de la ficha</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerFicha(ByVal int_CodigoFamiliar As Integer)

        Dim str_mensajeError As String = ""

        Dim BGColor As String = "#dcff7d"

        Dim obj_BL_Familiares As New bl_Familiares
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_Familiares.FUN_GET_FamiliarVisualizacionActualizacionFamiliar(int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("DatosOriginales") = ds_Lista

        hidenCodigoFamiliar.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoFamiliar").ToString
        hidenCodigoPersona.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoPersona").ToString

        lblNombreCompletoFamiliar.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString

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
        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
            ddlNacionalidad.SelectedValue = ds_Lista.Tables(1).Rows(0).Item("Codigo").ToString
        End If

        'Datos Adicionales
        rbAdicionalesProfesaReligion.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion"))
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
            If ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioDepartamento").ToString <> "0" Then
                ddlDomicilioDepartamento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioDepartamento").ToString
                ddlDomicilioDepartamento_SelectedIndexChanged()
            End If

            If ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioProvincia").ToString <> "0" Then
                ddlDomicilioProvincia.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioProvincia").ToString
                ddlDomicilioProvincia_SelectedIndexChanged()
            End If


            If ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioDistrito").ToString <> "0" Then
                ddlDomicilioDistrito.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioDistrito").ToString
            End If

        End If

        tbDomicilioUrbanizacion.Text = ds_Lista.Tables(0).Rows(0).Item("UrbanizacionDomicilio").ToString
        tbDomicilioDireccion.Text = ds_Lista.Tables(0).Rows(0).Item("DireccionDomicilio").ToString
        tbDomicilioReferencia.Text = ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomicilio").ToString
        tbDomicilioTelefono.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoDomicilio").ToString
        rbDomicilioAccesoInternet.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("AccesoInternetDomicilio"))

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
                If ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoDepartamento").ToString <> "00" Then 'Or ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoDepartamento").ToString <> "00" Then
                    ddlTrabajoDepartamento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoDepartamento").ToString
                    ddlTrabajoDepartamento_SelectedIndexChanged()
                End If
                If ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoProvincia").ToString <> "00" Then 'Or ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoProvincia").ToString <> "00" Then
                    ddlTrabajoProvincia.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoProvincia").ToString
                    ddlTrabajoProvincia_SelectedIndexChanged()
                End If
                If ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoDistrito").ToString <> "00" Then ' Or ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoDistrito").ToString <> "00" Then
                    ddlTrabajoDistrito.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajoDistrito").ToString
                End If

            End If
            tbTrabajoTelefono.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoTrabajo").ToString
            tbTrabajoCelular.Text = ds_Lista.Tables(0).Rows(0).Item("CelularTrabajo").ToString

            ddlTrabajoRadio.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoServicioRadioTrabajo").ToString
            tbTrabajoNumeroRadio.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroRadioTrabajo").ToString
            verificarRadio(2)

            tbTrabajoEmail.Text = ds_Lista.Tables(0).Rows(0).Item("EmailTrabajo").ToString
            rbTrabajoAccesoInternet.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("AccesoInternetTrabajo"))
        End If

        'Datos Estudios
        rbEstudiosExAlumno.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("ExAlumno"))
        tbEstudiosColegioEgreso.Text = ds_Lista.Tables(0).Rows(0).Item("ColegioEgreso").ToString
        verificarExAlumno()

        ddlEstudiosAnioEgreso.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("AnioEgreso").ToString()
        tbEstudiosContinuo.Text = ds_Lista.Tables(0).Rows(0).Item("ContinuoEstudios").ToString
        ddlEstudiosNivelInstruccion.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoNivelInstruccion").ToString
        'ddlEstudiosEscolaridadMinisterio.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoEscolaridadMinisterio").ToString

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
            GridviewFillColor(ds_Lista.Tables(2), GVListaIdiomas)

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
            GridviewFillColor(ds_Lista.Tables(3), GVListaProfesiones)

        End If

        'Detalle Autos
        Dim objDT_Autos As DataTable

        objDT_Autos = New DataTable("ListaAutos")
        objDT_Autos = ds_Lista.Tables(6).Clone

        If ds_Lista.Tables(6).Rows(0).Item("CodigoRelacion") <> -1 Then

            For Each dr As DataRow In ds_Lista.Tables(6).Rows
                objDT_Autos.ImportRow(dr)
            Next

            ViewState("ListaAutos") = objDT_Autos
            GVListaAutos.DataSource = objDT_Autos
            GVListaAutos.DataBind()
            GridviewFillColor(ds_Lista.Tables(6), GVListaAutos)

        End If

        VerRegistro("Actualización", 2)

    End Sub

    ''' <summary>
    ''' Graba la ficha del familiar
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarFicha()

        Dim str_mensajeError As String = ""

        Dim obj_BE_Familiar As New be_Familiares
        Dim obj_BL_Familiar As New bl_Familiares

        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()

        Dim ojb_BE_SolicitudActualizacionFichaFamiliares As New be_SolicitudActualizacionFichaFamiliares

        'Dataset que contiene los valores originales provenientes de la base de datos
        Dim ds_Lista As DataSet
        ds_Lista = ViewState("DatosOriginales")

        'Variable para guardar los codigos de los perfiles que realizan actualizacion
        Dim str_Perfiles As String = ""
        Dim arrBloques As New ArrayList

        'Variable para comparar los detalles
        Dim miDetalle As Integer = 0

        'Variable que controla si se graba el registro o no
        Dim BoolGrabar As Boolean = False

        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer = 0

        'Seteo de valores dependientes de los controles
        'Datos Personales
        If rbVive.SelectedValue = 1 Then
            tbFechaDefuncion.Text = ""


        Else


        End If

        'Datos Adicionales
        If rbAdicionalesProfesaReligion.SelectedValue = 0 Then ' No
            ddlAdicionalesReligion.SelectedValue = 1
            tbAdicionalesNombreIglesia.Text = ""
        End If
        If ddlAdicionalesRadio.SelectedValue = 0 Then ' No tiene servicio de radio
            tbAdicionalesNumeroRadio.Text = ""
        End If

        'Datos Domicilio
        If ddlDomicilioPais.SelectedValue <> 1 Then ' Diferente de Peru
            ddlDomicilioDepartamento.SelectedValue = 0
            ddlDomicilioProvincia.SelectedValue = 0
            ddlDomicilioDistrito.SelectedValue = 0
        End If


        'Datos Laborales
        If ddlSituacionLaboral.SelectedValue = 0 Or ddlSituacionLaboral.SelectedValue = 1 Or _
            ddlSituacionLaboral.SelectedValue = 2 Or ddlSituacionLaboral.SelectedValue = 5 Then
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
        Else
            If ddlTrabajoPais.SelectedItem.ToString <> "Perú" Then
                ddlTrabajoDepartamento.SelectedValue = 0
                ddlTrabajoProvincia.SelectedValue = 0
                ddlTrabajoDistrito.SelectedValue = 0
            End If
            If ddlTrabajoRadio.SelectedValue = 0 Then ' No tiene servicio de radio
                tbTrabajoNumeroRadio.Text = ""
            End If
        End If

        'Datos Estudios
        If rbEstudiosExAlumno.SelectedValue = 1 Then
            tbEstudiosColegioEgreso.Text = ""
        End If

        'Seteo de valores para registrar
        ojb_BE_SolicitudActualizacionFichaFamiliares.CodigoPeronsaSolicitante = Obtener_CodigoFamiliarLogueado() 'hidenCodigoPersona.Value
        obj_BE_Familiar.CodigoPersona = hidenCodigoPersona.Value
        obj_BE_Familiar.CodigoFamiliar = hidenCodigoFamiliar.Value

        'Datos Personales
        'If ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString = tbApellidoPaterno.Text.Trim Then
        '    obj_BE_Familiar.ApellidoPaterno = Nothing
        'Else
        obj_BE_Familiar.ApellidoPaterno = tbApellidoPaterno.Text.Trim
        BoolGrabar = True
        arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ApellidoPaterno").ToString))
        'End If

        If ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString = tbApellidoMaterno.Text.Trim Then
            obj_BE_Familiar.ApellidoMaterno = Nothing
        Else
            obj_BE_Familiar.ApellidoMaterno = tbApellidoMaterno.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ApellidoMaterno").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString = tbNombre.Text.Trim Then
            obj_BE_Familiar.Nombre = Nothing
        Else
            obj_BE_Familiar.Nombre = tbNombre.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_Nombre").ToString))
        End If

        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("CodigoSexo")) = rbSexo.SelectedValue Then
            obj_BE_Familiar.CodigoSexo = -1
        Else
            obj_BE_Familiar.CodigoSexo = rbSexo.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoSexo").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CodigoTipoDocIdentidad").ToString = ddlTipoDocumento.SelectedValue Then
            obj_BE_Familiar.CodigoTipoDocIdentidad = -1
        Else
            obj_BE_Familiar.CodigoTipoDocIdentidad = ddlTipoDocumento.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoTipoDocIdentidad").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString = tbNumDocumento.Text.Trim Then
            obj_BE_Familiar.NumeroDocIdentidad = Nothing
        Else
            obj_BE_Familiar.NumeroDocIdentidad = tbNumDocumento.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_NumeroDocIdentidad").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CodigoEstadoCivil").ToString = ddlEstadoCivil.SelectedValue Then
            obj_BE_Familiar.CodigoEstadoCivil = -1
        Else
            obj_BE_Familiar.CodigoEstadoCivil = ddlEstadoCivil.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoEstadoCivil").ToString))
        End If

        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("Vive")) = rbVive.SelectedValue Then
            obj_BE_Familiar.Vive = -1
        Else
            obj_BE_Familiar.Vive = rbVive.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_Vive").ToString))
        End If

        If IsDBNull(ds_Lista.Tables(0).Rows(0).Item("FechaDefuncion")) Then 'Si el valor es nulo en la BD
            If tbFechaDefuncion.Text.Trim = "" Then
                obj_BE_Familiar.FechaDefuncion = "01/01/1753" ' null
            Else
                obj_BE_Familiar.FechaDefuncion = Convert.ToDateTime(tbFechaDefuncion.Text.Trim)
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_FechaDefuncion").ToString))
            End If
        Else 'Si el valor NO es nulo en la BD
            If tbFechaDefuncion.Text.Trim = "" Then ' actualizo a null 
                obj_BE_Familiar.FechaDefuncion = "01/01/1753"
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_FechaDefuncion").ToString))
            Else
                If ds_Lista.Tables(0).Rows(0).Item("FechaDefuncion") = Convert.ToDateTime(tbFechaDefuncion.Text.Trim) Then
                    obj_BE_Familiar.FechaDefuncion = "01/01/1753"
                Else
                    obj_BE_Familiar.FechaDefuncion = Convert.ToDateTime(tbFechaDefuncion.Text.Trim)
                    BoolGrabar = True
                    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_FechaDefuncion").ToString))
                End If
            End If
        End If

        'Datos nacimiento / nacionalidad
        If IsDBNull(ds_Lista.Tables(0).Rows(0).Item("FechaNacimientoDt")) Then 'Si el valor es nulo en la BD
            If tbFechaNacimiento.Text.Trim = "" Or tbFechaNacimiento.Text.Trim = "00/00/0" Or tbFechaNacimiento.Text.Trim = "00/00/0000" Then

                obj_BE_Familiar.FechaNacimiento = "01/01/1753" ' null

            Else
                obj_BE_Familiar.FechaNacimiento = Convert.ToDateTime(tbFechaNacimiento.Text.Trim)
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_FechaNacimientoDt").ToString))
            End If
        Else 'Si el valor NO es nulo en la BD
            If tbFechaNacimiento.Text.Trim = "" Then ' actualizo a null 
                obj_BE_Familiar.FechaNacimiento = "01/01/1753"
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_FechaNacimientoDt").ToString))
            Else
                If ds_Lista.Tables(0).Rows(0).Item("FechaNacimientoDt") = Convert.ToDateTime(tbFechaNacimiento.Text.Trim) Then
                    obj_BE_Familiar.FechaNacimiento = "01/01/1753"
                Else
                    obj_BE_Familiar.FechaNacimiento = Convert.ToDateTime(tbFechaNacimiento.Text.Trim)
                    BoolGrabar = True
                    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_FechaNacimientoDt").ToString))
                End If
            End If
        End If

        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
            If ds_Lista.Tables(1).Rows(0).Item("Codigo").ToString = ddlNacionalidad.SelectedValue Then
                obj_BE_Familiar.CodigoNacionalidad = -1
            Else
                obj_BE_Familiar.CodigoNacionalidad = ddlNacionalidad.SelectedValue
                BoolGrabar = True
            End If
        Else
            obj_BE_Familiar.CodigoNacionalidad = ddlNacionalidad.SelectedValue
            BoolGrabar = True
        End If

        'Datos Domicilio
        If ds_Lista.Tables(0).Rows(0).Item("CodigoPaisDomicilio") = ddlDomicilioPais.SelectedValue Then
            obj_BE_Familiar.CodigoPaisDomicilio = -1
        Else
            obj_BE_Familiar.CodigoPaisDomicilio = ddlDomicilioPais.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoUbigeoDomicilio").ToString))
        End If

        'Consulto los códigos de ubigeo 
        If IIf(ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilio").ToString.Length = 0, "000000", ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilio").ToString) = _
            IIf(ddlDomicilioDepartamento.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioDepartamento.SelectedValue.ToString, ddlDomicilioDepartamento.SelectedValue.ToString) & _
            IIf(ddlDomicilioProvincia.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioProvincia.SelectedValue.ToString, ddlDomicilioProvincia.SelectedValue.ToString) & _
            IIf(ddlDomicilioDistrito.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioDistrito.SelectedValue.ToString, ddlDomicilioDistrito.SelectedValue.ToString) _
        Then
            obj_BE_Familiar.CodigoUbigeo = Nothing
        Else 'códigos ubigeo diferentes, entonces grabo el nuevo código ubigeo               
            obj_BE_Familiar.CodigoUbigeo = IIf(ddlDomicilioDepartamento.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioDepartamento.SelectedValue.ToString, ddlDomicilioDepartamento.SelectedValue.ToString) & _
                                            IIf(ddlDomicilioProvincia.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioProvincia.SelectedValue.ToString, ddlDomicilioProvincia.SelectedValue.ToString) & _
                                            IIf(ddlDomicilioDistrito.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioDistrito.SelectedValue.ToString, ddlDomicilioDistrito.SelectedValue.ToString)
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoUbigeoDomicilio").ToString))
        End If


        If ds_Lista.Tables(0).Rows(0).Item("UrbanizacionDomicilio").ToString = tbDomicilioUrbanizacion.Text.Trim Then
            obj_BE_Familiar.Urbanizacion = Nothing
        Else
            obj_BE_Familiar.Urbanizacion = tbDomicilioUrbanizacion.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_UrbanizacionDomicilio").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("DireccionDomicilio").ToString = tbDomicilioDireccion.Text.Trim Then
            obj_BE_Familiar.Direccion = Nothing
        Else
            obj_BE_Familiar.Direccion = tbDomicilioDireccion.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_DireccionDomicilio").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomicilio").ToString = tbDomicilioReferencia.Text.Trim Then
            obj_BE_Familiar.ReferenciaDomiciliaria = Nothing
        Else
            obj_BE_Familiar.ReferenciaDomiciliaria = tbDomicilioReferencia.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ReferenciaDomicilio").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("TelefonoDomicilio").ToString = tbDomicilioTelefono.Text.Trim Then
            obj_BE_Familiar.TelefonoCasa = Nothing
        Else
            obj_BE_Familiar.TelefonoCasa = tbDomicilioTelefono.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_TelefonoDomicilio").ToString))
        End If

        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("AccesoInternetDomicilio")) = rbDomicilioAccesoInternet.SelectedValue Then
            obj_BE_Familiar.AccesoInternet = -1
        Else
            obj_BE_Familiar.AccesoInternet = rbDomicilioAccesoInternet.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_AccesoInternetDomicilio").ToString))
        End If

        'Datos Laborales
        If ds_Lista.Tables(0).Rows(0).Item("CodigoSituacionLaboral").ToString = ddlSituacionLaboral.SelectedValue Then
            obj_BE_Familiar.codigosituacionlaboral = -1
        Else
            obj_BE_Familiar.codigosituacionlaboral = ddlSituacionLaboral.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoSituacionLaboral").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("Ocupacion").ToString = tbOcupacion.Text.Trim Then
            obj_BE_Familiar.OcupacionCargo = Nothing
        Else
            obj_BE_Familiar.OcupacionCargo = tbOcupacion.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_Ocupacion").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CentroTrabajo").ToString = tbCentroTrabajo.Text.Trim Then
            obj_BE_Familiar.CentroTrabajo = Nothing
        Else
            obj_BE_Familiar.CentroTrabajo = tbCentroTrabajo.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CentroTrabajo").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("DireccionTrabajo").ToString = tbTrabajoDireccion.Text.Trim Then
            obj_BE_Familiar.DireccionCentroTrabajo = Nothing
        Else
            obj_BE_Familiar.DireccionCentroTrabajo = tbTrabajoDireccion.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_DireccionTrabajo").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CodigoPaisTrabajo") = ddlTrabajoPais.SelectedValue Then
            obj_BE_Familiar.CodigoPaisCentroTrabajo = -1
        Else
            obj_BE_Familiar.CodigoPaisCentroTrabajo = ddlTrabajoPais.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoPaisTrabajo").ToString))
        End If

        'Consulto los códigos de ubigeo 
        If IIf(ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajo").ToString.Length = 0, "000000", ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoTrabajo").ToString) = _
            IIf(ddlTrabajoDepartamento.SelectedValue.ToString.Length = 1, "0" & ddlTrabajoDepartamento.SelectedValue.ToString, ddlTrabajoDepartamento.SelectedValue.ToString) & _
            IIf(ddlTrabajoProvincia.SelectedValue.ToString.Length = 1, "0" & ddlTrabajoProvincia.SelectedValue.ToString, ddlTrabajoProvincia.SelectedValue.ToString) & _
            IIf(ddlTrabajoDistrito.SelectedValue.ToString.Length = 1, "0" & ddlTrabajoDistrito.SelectedValue.ToString, ddlTrabajoDistrito.SelectedValue.ToString) _
        Then
            obj_BE_Familiar.CodigoUbigeoCentroTrabajo = Nothing
        Else 'códigos ubigeo diferentes, entonces grabo el nuevo código ubigeo               
            obj_BE_Familiar.CodigoUbigeoCentroTrabajo = IIf(ddlTrabajoDepartamento.SelectedValue.ToString.Length = 1, "0" & ddlTrabajoDepartamento.SelectedValue.ToString, ddlTrabajoDepartamento.SelectedValue.ToString) & _
                                                        IIf(ddlTrabajoProvincia.SelectedValue.ToString.Length = 1, "0" & ddlTrabajoProvincia.SelectedValue.ToString, ddlTrabajoProvincia.SelectedValue.ToString) & _
                                                        IIf(ddlTrabajoDistrito.SelectedValue.ToString.Length = 1, "0" & ddlTrabajoDistrito.SelectedValue.ToString, ddlTrabajoDistrito.SelectedValue.ToString)
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoUbigeoTrabajo").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("TelefonoTrabajo").ToString = tbTrabajoTelefono.Text.Trim Then
            obj_BE_Familiar.TelefonoOficina = Nothing
        Else
            obj_BE_Familiar.TelefonoOficina = tbTrabajoTelefono.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_TelefonoTrabajo").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CelularTrabajo").ToString = tbTrabajoCelular.Text.Trim Then
            obj_BE_Familiar.CelularOficina = Nothing
        Else
            obj_BE_Familiar.CelularOficina = tbTrabajoCelular.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CelularTrabajo").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CodigoServicioRadioTrabajo").ToString = ddlTrabajoRadio.SelectedValue Then
            obj_BE_Familiar.CodigoServicioRadioOficina = -1
        Else
            obj_BE_Familiar.CodigoServicioRadioOficina = ddlTrabajoRadio.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoServicioRadioTrabajo").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("NumeroRadioTrabajo").ToString = tbTrabajoNumeroRadio.Text.Trim Then
            obj_BE_Familiar.NumeroServicioRadioOficina = Nothing
        Else
            obj_BE_Familiar.NumeroServicioRadioOficina = tbTrabajoNumeroRadio.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_NumeroRadioTrabajo").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("EmailTrabajo").ToString = tbTrabajoEmail.Text.Trim Then
            obj_BE_Familiar.EmailOficina = Nothing
        Else
            obj_BE_Familiar.EmailOficina = tbTrabajoEmail.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_EmailTrabajo").ToString))
        End If

        If IIf(ds_Lista.Tables(0).Rows(0).Item("AccesoInternetTrabajo").ToString.Length = 0, 0, ds_Lista.Tables(0).Rows(0).Item("AccesoInternetTrabajo")) = rbTrabajoAccesoInternet.SelectedValue Then
            obj_BE_Familiar.AccesoInternetOficina = -1
        Else
            obj_BE_Familiar.AccesoInternetOficina = rbTrabajoAccesoInternet.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_AccesoInternetTrabajo").ToString))
        End If

        'Datos Estudios
        If IIf(ds_Lista.Tables(0).Rows(0).Item("ExAlumno").ToString.Length = 0, 0, ds_Lista.Tables(0).Rows(0).Item("ExAlumno")) = rbEstudiosExAlumno.SelectedValue Then
            obj_BE_Familiar.ExAlumno = -1
        Else
            obj_BE_Familiar.ExAlumno = rbEstudiosExAlumno.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ExAlumno").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("ColegioEgreso").ToString = tbEstudiosColegioEgreso.Text.Trim Then
            obj_BE_Familiar.ColegioEgreso = Nothing
        Else
            obj_BE_Familiar.ColegioEgreso = tbEstudiosColegioEgreso.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ColegioEgreso").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("AnioEgreso").ToString = ddlEstudiosAnioEgreso.SelectedValue Then
            obj_BE_Familiar.ExAlumnoAnioEgreso = -1
        Else
            obj_BE_Familiar.ExAlumnoAnioEgreso = ddlEstudiosAnioEgreso.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_AnioEgreso").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("ContinuoEstudios").ToString = tbEstudiosContinuo.Text.Trim Then
            obj_BE_Familiar.ContinuaEstudios = Nothing
        Else
            obj_BE_Familiar.ContinuaEstudios = tbEstudiosContinuo.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ContinuoEstudios").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CodigoNivelInstruccion").ToString = ddlEstudiosNivelInstruccion.SelectedValue Then
            obj_BE_Familiar.CodigoNivelInstruccion = -1
        Else
            obj_BE_Familiar.CodigoNivelInstruccion = ddlEstudiosNivelInstruccion.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoNivelInstruccion").ToString))
        End If
        obj_BE_Familiar.CodigoEscolaridadMinisterio = -1
        'If ds_Lista.Tables(0).Rows(0).Item("CodigoEscolaridadMinisterio").ToString = ddlEstudiosEscolaridadMinisterio.SelectedValue Then
        '    obj_BE_Familiar.CodigoEscolaridadMinisterio = -1
        'Else
        '    obj_BE_Familiar.CodigoEscolaridadMinisterio = ddlEstudiosEscolaridadMinisterio.SelectedValue
        '    BoolGrabar = True
        '    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoEscolaridadMinisterio").ToString))
        'End If

        'Datos Adicionales
        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion")) = rbAdicionalesProfesaReligion.SelectedValue Then
            obj_BE_Familiar.ProfesaReligion = -1
        Else
            obj_BE_Familiar.ProfesaReligion = rbAdicionalesProfesaReligion.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ProfesaReligion").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CodigoReligion").ToString = ddlAdicionalesReligion.SelectedValue Then
            obj_BE_Familiar.CodigoReligion = -1
        Else
            obj_BE_Familiar.CodigoReligion = ddlAdicionalesReligion.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoReligion").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("NombreIglesia").ToString = tbAdicionalesNombreIglesia.Text.Trim Then
            obj_BE_Familiar.NombreIglesia = Nothing
        Else
            obj_BE_Familiar.NombreIglesia = tbAdicionalesNombreIglesia.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_NombreIglesia").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CelularPersonal").ToString = tbAdicionalesCelular.Text.Trim Then
            obj_BE_Familiar.Celular = Nothing
        Else
            obj_BE_Familiar.Celular = tbAdicionalesCelular.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CelularPersonal").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CodigoServicioRadioDomicilio").ToString = ddlAdicionalesRadio.SelectedValue Then
            obj_BE_Familiar.CodigoServicioRadioDomicilio = -1
        Else
            obj_BE_Familiar.CodigoServicioRadioDomicilio = ddlAdicionalesRadio.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoServicioRadioDomicilio").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("NumeroRadioPersonal").ToString = tbAdicionalesNumeroRadio.Text.Trim Then
            obj_BE_Familiar.NumeroServicioRadioPersonal = Nothing
        Else
            obj_BE_Familiar.NumeroServicioRadioPersonal = tbAdicionalesNumeroRadio.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_NumeroRadioPersonal").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString = tbAdicionalesEmail.Text.Trim Then
            obj_BE_Familiar.EmailPersonal = Nothing
        Else
            obj_BE_Familiar.EmailPersonal = tbAdicionalesEmail.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_EmailPersonal").ToString))
        End If

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

        miDetalle = Comparar2DataTable(ds_Lista.Tables(2), objDT_Idioma)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else

            If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") > 0 Then
                If ds_Lista.Tables(2).Rows(0).Item("Origen") = "T" Then
                    BoolGrabar = True
                    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoBloqueInformacion").ToString))
                Else
                    objDT_Idioma.Rows.Clear()
                End If
            Else
                objDT_Idioma.Rows.Clear()
            End If

            ' ''If ds_Lista.Tables(2).Rows(0).Item("Origen") = "T" Then
            ' ''    BoolGrabar = True
            ' ''    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoBloqueInformacion").ToString))
            ' ''Else
            ' ''    objDT_Idioma.Rows.Clear()
            ' ''End If
        End If

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

        miDetalle = Comparar2DataTable(ds_Lista.Tables(3), objDT_Profesion)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(3).Rows(0).Item("CodigoBloqueInformacion").ToString))

            If objDT_Profesion.Rows.Count = 0 Then 'Si no hay detalle
                dr_Profesion = objDT_Profesion.NewRow
                dr_Profesion.Item("Codigo") = -1
                dr_Profesion.Item("Descripcion") = ""
                objDT_Profesion.Rows.Add(dr_Profesion)
            End If

        Else

            If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") > 0 Then
                If ds_Lista.Tables(3).Rows(0).Item("Origen") = "T" Then
                    BoolGrabar = True
                    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(3).Rows(0).Item("CodigoBloqueInformacion").ToString))
                Else
                    objDT_Profesion.Rows.Clear()
                End If
            Else
                objDT_Profesion.Rows.Clear()
            End If

            ' ''If ds_Lista.Tables(3).Rows(0).Item("Origen") = "T" Then
            ' ''    BoolGrabar = True
            ' ''    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(3).Rows(0).Item("CodigoBloqueInformacion").ToString))
            ' ''Else
            ' ''    objDT_Profesion.Rows.Clear()
            ' ''End If
        End If



        'Detalle Autos
        Dim objDT_Autos As DataTable

        objDT_Autos = New DataTable("ListaAutos")
        objDT_Autos = Datos.agregarColumna(objDT_Autos, "Codigo", "Integer")
        objDT_Autos = Datos.agregarColumna(objDT_Autos, "Marca", "String")
        objDT_Autos = Datos.agregarColumna(objDT_Autos, "Modelo", "String")
        objDT_Autos = Datos.agregarColumna(objDT_Autos, "Placa", "String")

        Dim dr_Auto As DataRow
        For Each drv As GridViewRow In GVListaAutos.Rows
            dr_Auto = objDT_Autos.NewRow
            dr_Auto.Item("Codigo") = CType(drv.FindControl("Label1"), Label).Text
            dr_Auto.Item("Marca") = CType(drv.FindControl("Label2"), Label).Text
            dr_Auto.Item("Modelo") = CType(drv.FindControl("Label3"), Label).Text
            dr_Auto.Item("Placa") = CType(drv.FindControl("Label4"), Label).Text
            objDT_Autos.Rows.Add(dr_Auto)
        Next

        Dim bool_RegistrarAuto As Boolean = True

        If Not ds_Lista.Tables(6).Rows(0).Item("CodigoRelacion") > 0 And objDT_Autos.Rows.Count = 0 Then ' listas vacias
            bool_RegistrarAuto = False
        End If

        If bool_RegistrarAuto Then
            If ds_Lista.Tables(6).Rows.Count <> objDT_Autos.Rows.Count Then
                bool_RegistrarAuto = True
            Else
                If ds_Lista.Tables(6).Rows(0).Item("CodigoRelacion") = -1 Then ' Indica que este DataTable tiene 1 sola fila pero no referencia a ningun registro
                    bool_RegistrarAuto = True ' Los DataTables son diferentes
                Else
                    For i As Integer = 0 To ds_Lista.Tables(6).Rows.Count - 1
                        If ds_Lista.Tables(6).Rows(i).Item("Placa") <> objDT_Autos.Rows(i).Item("Placa") Or _
                            ds_Lista.Tables(6).Rows(i).Item("Marca") <> objDT_Autos.Rows(i).Item("Marca") Or _
                            ds_Lista.Tables(6).Rows(i).Item("Modelo") <> objDT_Autos.Rows(i).Item("Modelo") Then
                            bool_RegistrarAuto = True ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                            Exit For
                        End If
                    Next
                    bool_RegistrarAuto = False
                End If
            End If
        End If

        If bool_RegistrarAuto = False Then
            objDT_Autos.Rows.Clear()
        End If

        If BoolGrabar Then
            'Agrego las DataTable a mi DataSet
            objDS_Detalle.Tables.Add(objDT_Idioma)
            objDS_Detalle.Tables.Add(objDT_Profesion)
            objDS_Detalle.Tables.Add(objDT_Autos)

            str_Perfiles = obtenerPerfiles(ds_Lista.Tables(5), arrBloques)

            ' Tipo de Solicitud por Matrícula
            ojb_BE_SolicitudActualizacionFichaFamiliares.TipoSolicitud = 1

            ' Codigo Anio de Solicitud
            ojb_BE_SolicitudActualizacionFichaFamiliares.CodigoAnioSolicitud = hiddenCodigoAnioAcademico.Value

            usp_valor = obj_BL_Familiar.FUN_INS_FamiliaresTemp(ojb_BE_SolicitudActualizacionFichaFamiliares, _
                                                               obj_BE_Familiar, _
                                                               str_Perfiles, _
                                                               objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Else
            'MostrarSexyAlertBox(Alertas.ObtenerAlerta(4, ""), "Alert")
        End If

        If usp_valor > 0 Then

            ' Si se registra la solicitud, registro en el log de pasos de matrícula
            Dim int_CodigoPasoMatricula As Integer = 6
            RegistrarPasoMatricula(int_CodigoPasoMatricula, hiddenCodigoAnioAcademico.Value, hiddenCodigoAlumno.Value, hiddenCodigoFamiliar.Value, 0)

            ' Envio de email a responsables de validación
            'enviarEmailResponsablesValidacion(str_Perfiles, usp_valor)

            'MostrarSexyAlertBox(usp_mensaje, "Info")
            MostrarMensajeAlert(usp_mensaje & "\n En caso requiera actualizar los datos de otro familiar de clic en el boton 'Actualizar' al lado izquierdo del nombre del familiar que desea.")
            btnFichaCancelar_Click()

            ' Recargo la página padre
            'CargarPaginaPadre()
            Dim int_CodigoFamilia As Integer = hiddenCodigoFamilia.Value
            obtenerDatosFamiliaRecarga(int_CodigoFamilia)

        Else
            If usp_mensaje = "" Then
                usp_mensaje = "Debe actualizar al menos un campo para realizar el envío"
            End If
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Cambia el estado de la ficha del familiar
    ''' </summary>
    ''' <param name="int_Codigo">codigo del familiar al cual se le quiere cambiar la ficha</param>
    ''' <param name="str_accion">estado al que se desea cambiar la ficha</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_BL_Familiares As New bl_Familiares
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado

        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_Familiares.FUN_DEL_Familiares(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Compara 2 conjuntos de datos (DataTable)
    ''' </summary>
    ''' <param name="dtOriginal">Conjunto de datos original (proveninente de la base de datos)</param>
    ''' <param name="dtActualizar">Conjunto de datos ha aztualizar (modificado en el formulario)</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function Comparar2DataTable(ByVal dtOriginal As DataTable, ByVal dtActualizar As DataTable) As Integer

        'Comparo los DataTables
        Dim rpta As Integer = 0

        If Not dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows.Count = 0 Then ' ambas listas están vacias
            Return 0
        End If

        If dtOriginal.Rows.Count <> dtActualizar.Rows.Count Then

            Return 1 ' Los DataTables son diferentes

        Else 'Si tienen el mismo numero de elementos los comparo fila por fila

            If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 Then ' Indica que este DataTable tiene 1 sola fila pero no referencia a ningun registro
                Return 1 ' Los DataTables son diferentes
            Else
                For i As Integer = 0 To dtOriginal.Rows.Count - 1
                    If dtOriginal.Rows(i).Item("Codigo") <> dtActualizar.Rows(i).Item("Codigo") Then
                        Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                    End If
                Next
                Return 0
            End If

        End If

    End Function

    ''' <summary>
    ''' Busca el valor de origen del conjunto de datos y dependiendo de este pinta o no la grilla
    ''' </summary>
    ''' <param name="dt">Conjunto de datos a consultar</param>
    ''' <param name="gv">grilla a pintar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GridviewFillColor(ByVal dt As DataTable, ByVal gv As GridView)

        If dt.Rows(0).Item("Origen") = "R" Then
            gv.CssClass = "gridview_body"
        Else
            gv.CssClass = "gridview_body_Temporal"
        End If

    End Sub

    Private Sub enviarEmailResponsablesValidacion(ByVal str_CodigoPerfiles As String, ByVal int_CodigoSolicitud As Integer)

        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()

        'Tipos de Solicitud 
        'Ficha Familiar : 1
        'Ficha Alumno   : 2
        'Ficha Médica   : 3
        Dim int_TipoSolicitud As Integer = 1

        Dim obj_BL_SolicitudActualizacionDatos As New bl_SolicitudActualizacionDatos
        Dim ds_Lista As DataSet = obj_BL_SolicitudActualizacionDatos.FUN_LIS_DatosSolicitudes(int_CodigoSolicitud, int_TipoSolicitud, str_CodigoPerfiles, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        'Dim obj_EnvioEmail As New EnvioEmail
        Dim int_ExitoEnvio As Integer = 0

        Dim arr_Emails As New ArrayList
        Dim str_Asunto As String = "Solicitud de Actualización de Datos de la Ficha de Familiar"

        Dim sb_Cuerpo As New StringBuilder
        sb_Cuerpo.Append("<div style='font-family: Arial; font-size: 11px;'>")
        sb_Cuerpo.Append("Se notifica que tiene una nueva solicitud de validación.<br />")
        sb_Cuerpo.Append("La solicitud fue enviada el <i><b>" & ds_Lista.Tables(1).Rows(0).Item("FechaSolicitud") & "</b></i>")
        sb_Cuerpo.Append("por <i><b>" & ds_Lista.Tables(1).Rows(0).Item("NombreCompletoSolicitante") & "</b></i>")
        sb_Cuerpo.Append("y solicita la actualización de datos del familiar <i><b>" & ds_Lista.Tables(1).Rows(0).Item("NombreCompletoFicha") & "</b></i>.")
        sb_Cuerpo.Append("</div>")

        Dim str_EmailCopia As String = ""

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            arr_Emails.Add(dr.Item("CorreoCorporativo").ToString)
        Next

        int_ExitoEnvio = EnvioEmail.SendEmail(arr_Emails, sb_Cuerpo.ToString, str_Asunto)

    End Sub

#End Region

#Region "Cargas"

    ''' <summary>
    ''' Carga el combo con la lista de Tipo de Documentos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoDocumento()

        Dim obj_BL_TipoDocIdentidad As New bl_TipoDocIdentidad
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_TipoDocIdentidad.FUN_LIS_TipoDocIdentidad("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
        Controles.llenarCombo(ddlTipoDocumento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Estado Civil disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboEstadoCivil()

        Dim obj_BL_EstadosCiviles As New bl_EstadosCiviles
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_EstadosCiviles.FUN_LIS_EstadosCiviles("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
        Controles.llenarCombo(ddlEstadoCivil, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Nacionalidades disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNacionalidades()

        Dim obj_BL_Nacionalidades As New bl_Nacionalidades
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_Nacionalidades.FUN_LIS_Nacionalidades("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
        Controles.llenarCombo(ddlNacionalidad, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Religiones disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboReligiones()

        Dim obj_BL_Religiones As New bl_Religiones
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_Religiones.FUN_LIS_Religiones("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
        Controles.llenarCombo(ddlAdicionalesReligion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Paises disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboPaises()

        Dim obj_BL_Paises As New bl_Paises
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_Paises.FUN_LIS_Pais("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)

        Controles.llenarCombo(ddlDomicilioPais, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlTrabajoPais, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga y limpia los combos dependientes del Ubigeo Departamento
    ''' </summary>
    ''' <param name="tab">Indica si se cargara el combo Ubigeo domicilio o trabajo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDepartamentos(ByVal tab As Integer)

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Departamentos(int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)

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
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboProvincia(ByVal tab As Integer)

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado

        If tab = 1 Then ' Domicilio

            Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlDomicilioDepartamento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
            Controles.llenarCombo(ddlDomicilioProvincia, ds_Lista, "Codigo", "Descripcion", False, True)

        ElseIf tab = 2 Then  ' Trabajo

            Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlTrabajoDepartamento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
            Controles.llenarCombo(ddlTrabajoProvincia, ds_Lista, "Codigo", "Descripcion", False, True)

        End If

    End Sub

    ''' <summary>
    ''' Carga el combo Ubigeo Distrito(domicilio o trabajo), filtrando por Departamento y Provincia
    ''' </summary>
    ''' <param name="tab">Indica si se cargara el combo Ubigeo domicilio o trabajo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDistrito(ByVal tab As Integer)

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado

        If tab = 1 Then ' Domicilio

            Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlDomicilioDepartamento.SelectedValue, ddlDomicilioProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
            Controles.llenarCombo(ddlDomicilioDistrito, ds_Lista, "Codigo", "Descripcion", False, True)

        ElseIf tab = 2 Then  ' Trabajo

            Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlTrabajoDepartamento.SelectedValue, ddlTrabajoProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
            Controles.llenarCombo(ddlTrabajoDistrito, ds_Lista, "Codigo", "Descripcion", False, True)

        End If

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
    Private Sub cargarComboSituacionLaboral()

        Dim obj_BL_SituacionesLaborales As New bl_SituacionesLaborales
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_SituacionesLaborales.FUN_LIS_SituacionLaboral("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
        Controles.llenarCombo(ddlSituacionLaboral, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Nivel de Instrucción disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNivelesInstruccion()

        Dim obj_BL_NivelesInstruccion As New bl_NivelesInstruccion
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_NivelesInstruccion.FUN_LIS_NivelesInstruccion("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
        Controles.llenarCombo(ddlEstudiosNivelInstruccion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    '''' <summary>
    '''' Carga el combo con la lista de Escolaridad Ministerio disponibles en estado activo
    '''' </summary>
    '''' <remarks>
    '''' Creador:               Juan Vento
    '''' Fecha de Creación:     27/01/2011
    '''' Modificado por:        _____________
    '''' Fecha de modificación: _____________ 
    '''' </remarks>
    'Private Sub cargarComboEscolaridadMinisterio()

    '    Dim obj_BL_EscolaridadesMinisterio As New bl_EscolaridadesMinisterio
    '    Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
    '    Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
    '    Dim ds_Lista As DataSet = obj_BL_EscolaridadesMinisterio.FUN_LIS_EscolaridadMinisterio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
    '    Controles.llenarCombo(ddlEstudiosEscolaridadMinisterio, ds_Lista, "Codigo", "Descripcion", False, True)

    'End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Servicios Radio disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombosServiciosRadio()

        Dim obj_BL_ServiciosRadio As New bl_ServiciosRadio
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_ServiciosRadio.FUN_LIS_ServicioRadio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
        Controles.llenarCombo(ddlAdicionalesRadio, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlTrabajoRadio, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Idiomas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboIdiomas()

        Dim obj_BL_Idiomas As New bl_Idiomas
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_Idiomas.FUN_LIS_Idiomas("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
        Controles.llenarCombo(ddlIdioma, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Profesiones disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboProfesiones()

        Dim obj_BL_Profesiones As New bl_Profesiones

        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Lista As DataSet = obj_BL_Profesiones.FUN_LIS_Profesion("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 73)
        Controles.llenarCombo(ddlProfesion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con una serie de años
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarPais(ByVal tab As Integer)

        Dim cod_Pais As Integer = 0

        If tab = 1 Then ' Domicilio

            cod_Pais = ddlDomicilioPais.SelectedValue
            If cod_Pais = 1 Then ' Si el pais es PERU
                cargarComboUbigeo(1)
                estadoCombosUbigeo(1, True)
            Else
                limpiarCombosUbigeo(1)
                estadoCombosUbigeo(1, False)
            End If

        ElseIf tab = 2 Then  ' Trabajo

            cod_Pais = ddlTrabajoPais.SelectedValue
            If cod_Pais = 1 Then ' Si el pais es PERU
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
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarFechaDefuncion()

        If rbVive.SelectedValue = 1 Then
            tbFechaDefuncion.Enabled = False
            image1.Enabled = False
            CalendarExtender1.Enabled = False
            CamposFamiliarVive(1)
        Else
            tbFechaDefuncion.Enabled = True
            image1.Enabled = True
            CalendarExtender1.Enabled = True
            CamposFamiliarVive(2)
        End If

    End Sub

    ''' <summary>
    ''' Dependiendo de si profesa religión o no, se activarán o desactivarán otros campos del formulario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
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

    'Private Sub BlanquearBotones()
    '    Dim int_Contador As Integer = 0
    '    Dim buton_Opcion As Button

    '    While int_Contador <= dgv_GrupoInformacion.Rows.Count - 1

    '        buton_Opcion = dgv_GrupoInformacion.Rows(int_Contador).FindControl("btn_GrupoInformacion")
    '        buton_Opcion.Style.Value = "cursor:pointer;background: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/menu/grupoInformacion_itemMenu.jpg') right no-repeat; "
    '        int_Contador = int_Contador + 1
    '    End While
    'End Sub

#End Region

#Region "Mantenimiento Detalle Idioma"

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
    ''' Fecha de Creación:     27/01/2011
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
                MostrarSexyAlertBox("No puede ingresar más de 3 idiomas.", "Alert")
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
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
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
    Protected Sub GVListaIdiomas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try

            If e.CommandName = "Eliminar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 3
                    eliminarIdioma(codigo)
                End If
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaIdiomas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento Detalle Profesion"

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
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
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
                    int_CodigoAccion = 3
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
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento Detalle Auto"

#Region "Eventos"

    Protected Sub btnAgregarDetalleAuto_Click()
        pnModalAuto.Show()
    End Sub

    Protected Sub btnModalAceptarAuto_Click()
        Try
            Dim resulado As Boolean
            agregarAuto(resulado)
            If resulado = False Then
                pnModalAuto.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(200, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarAuto_Click()
        cerrarModalAuto()
    End Sub

#End Region
#Region "Métodos"

    ''' <summary>
    ''' Agrega 1 idioma al detalle de idiomas
    ''' </summary>
    ''' <param name="resultado">Valor de la variable resultado, indica si se agrego o no el nuevo registro de idioma al detalle</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarAuto(ByRef resultado As Boolean)

        If tbPlaca.Text.Length = 0 Then

            MostrarSexyAlertBox("Debe ingresar una placa.", "Alert")
            tbPlaca.Text = ""
            Exit Sub

        End If

        If tbMarca.Text.Length = 0 Then

            MostrarSexyAlertBox("Debe ingresar una marca.", "Alert")
            tbMarca.Text = ""
            Exit Sub

        End If

        If tbModelo.Text.Length = 0 Then

            MostrarSexyAlertBox("Debe ingresar un módelo.", "Alert")
            tbModelo.Text = ""
            Exit Sub

        End If

        Dim dt As DataTable

        If ViewState("ListaAutos") Is Nothing Then

            dt = New DataTable("ListaAutos")

            dt = Datos.agregarColumna(dt, "Codigo", "Integer")
            dt = Datos.agregarColumna(dt, "Marca", "String")
            dt = Datos.agregarColumna(dt, "Modelo", "String")
            dt = Datos.agregarColumna(dt, "Placa", "String")

        Else

            dt = ViewState("ListaAutos")

        End If

        Dim boolContinuar As Boolean = True

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("Placa").ToString = tbPlaca.Text.Trim Then

                    MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                    tbPlaca.Text = ""
                    tbMarca.Text = ""
                    tbModelo.Text = ""
                    boolContinuar = False

                End If

            Next

        End If

        resultado = boolContinuar

        If boolContinuar Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("Codigo") = 0

            dr.Item("Marca") = tbMarca.Text.Trim
            dr.Item("Modelo") = tbModelo.Text.Trim
            dr.Item("Placa") = tbPlaca.Text.Trim

            dt.Rows.Add(dr)

            ViewState("ListaAutos") = dt

            GVListaAutos.DataSource = dt
            GVListaAutos.DataBind()

            tbMarca.Text = ""
            tbModelo.Text = ""
            tbPlaca.Text = ""
            upAuto.Update()

        End If

    End Sub

    ''' <summary>
    ''' Cierra el popup idioma
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/12/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalAuto()

        pnModalAuto.Hide()
        tbPlaca.Text = ""
        tbModelo.Text = ""
        tbMarca.Text = ""

    End Sub

    ''' <summary>
    ''' Elimina 1 idioma del detalle de idiomas
    ''' </summary>
    ''' <param name="str_Placa">Codigo del auto que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/12/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarAuto(ByVal str_Placa As String) 'ByVal int_CodigoAuto As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaAutos")

        For Each auxdr As DataRow In dt.Rows

            'If auxdr.Item("Codigo").ToString = int_CodigoAuto

            If auxdr.Item("Placa").ToString = str_Placa Then

                auxdr.Delete()
                Exit For

            End If

        Next

        dt.AcceptChanges()

        ViewState("ListaAutos") = dt

        GVListaAutos.DataSource = dt
        GVListaAutos.DataBind()
        upAuto.Update()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub GVListaAutos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try

            If e.CommandName = "Eliminar" Then
                'Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 3
                    Dim str_Placa As String = CType(row.FindControl("Label4"), Label).Text
                    eliminarAuto(str_Placa)
                End If
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaAutos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
#End Region

#End Region


#Region "Eventos de Grilla"

    Protected Sub gvDetalleIntegrantesFamilia_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Seleccionar" Then
                Dim int_codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                int_CodigoAccion = 3
                ObtenerFicha(int_codigo)

                ' Bloqueo la grilla de lista de Famiiares y activo el panel de actualización de datos
                gvDetalleIntegrantesFamilia.Enabled = False
                miPanelFamiliar.Enabled = True

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub gvDetalleIntegrantesFamilia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btnSeleccionar As ImageButton = e.Row.FindControl("btnSeleccionar")

            If e.Row.DataItem("TieneSolicitud") = 1 Then
                btnSeleccionar.Visible = False
            Else
                btnSeleccionar.Visible = True
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region


#Region "Manejo de Alertas - Emails"

    ''' <summary>
    ''' Recibe mensajes y los deriva a otro metodo que los visualizara cno animación de JQuery
    ''' </summary>
    ''' <param name="str_alertas">Mensaje que se quiere visualizar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
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
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_CodigoUsuario As String = Obtener_CodigoFamiliarLogueado()
        Dim int_TipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(5, 73, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region

#Region "Métodos IFrame"

    Private Sub CargarPaginaPadre()

        Dim str_Script As String = "parent.location.href = parent.location.href;"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", str_Script, True)

    End Sub


    ''' <summary>
    ''' Registra el Log de pasos de matricula.
    ''' </summary>
    ''' <param name="int_CodigoPasoMatricula">Codigo del paso de la matricula</param>
    ''' <param name="int_PeriodoAcademico">Periodo academico de la matricula</param>
    ''' <param name="int_CodigoAlumno">Codigo del alumno a matricular</param>
    ''' <param name="int_CodigoFamiliar">Codigo del familiar que esta matriculando</param>
    ''' <param name="int_AceptacionEtapa"></param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     30/12/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    ''' 
    Private Sub RegistrarPasoMatricula(ByVal int_CodigoPasoMatricula As Integer, ByVal int_PeriodoAcademico As Integer, ByVal int_CodigoAlumno As Integer, ByVal int_CodigoFamiliar As Integer, ByVal int_AceptacionEtapa As Integer)

        Dim obj_BE_Matricula As New be_Matricula
        Dim obj_BL_Matricula As New bl_Matricula
        Dim int_Resultado As Integer = -1
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()

        obj_BE_Matricula.PeriodoAcademico = int_PeriodoAcademico
        obj_BE_Matricula.CodigoPasoMatricula = int_CodigoPasoMatricula
        obj_BE_Matricula.CodigoAlumno = int_CodigoAlumno
        obj_BE_Matricula.CodigoFamiliar = int_CodigoFamiliar
        obj_BE_Matricula.AceptacionEtapa = int_AceptacionEtapa

        int_Resultado = obj_BL_Matricula.FUN_INS_PasoMatricula(obj_BE_Matricula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

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
    Private Function Obtener_CodigoFamiliarLogueado() As Integer

        Dim int_CodigoFamiliarLogueado As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String

            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_CodigoFamiliarLogueado = str_ArrayDatos(0)

        Catch ex As Exception
            int_CodigoFamiliarLogueado = -1
        End Try

        Return int_CodigoFamiliarLogueado

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
    Private Sub RegistrarAccesoPagina(ByVal int_CodigoModulo As Integer, ByVal int_CodigoSubBloque As Integer)
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
    Private Sub MostrarMensaje(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

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
    Private Function Obtener_CodigoTipoUsuarioLogueado() As Integer

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
    ''' Fecha de Creación:     03/01/2011
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
