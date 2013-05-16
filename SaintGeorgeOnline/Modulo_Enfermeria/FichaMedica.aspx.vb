Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Imports System.Data
Imports System.Data.SqlClient
Imports SaintGeorgeOnline_Utilities

''' <summary>
''' Modulo de Mantenimiento de Ficha Medica
''' </summary>
''' <remarks>
''' Código del Modulo:    1
''' Código de la Opción:  1
''' </remarks>
''' 07-02-2012
Partial Class Modulo_Enfermeria_FichaMedica
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Ficha de Médica de Alumno")
            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                ViewState("SortExpression") = "NombreCompleto"
                ViewState("Direccion") = "ASC"
                btnFichaCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                tbObservaciones.Attributes.Add("onkeypress", " ValidarLength(this, 700);")
                tbObservaciones.Attributes.Add("onkeyup", " ValidarLength(this, 700);")
                tbObservacionesOftalmologicas.Attributes.Add("onkeypress", " ValidarLength(this, 700);")
                tbObservacionesOftalmologicas.Attributes.Add("onkeyup", " ValidarLength(this, 700);")
                tbObservacionesDental.Attributes.Add("onkeypress", " ValidarLength(this, 700);")
                tbObservacionesDental.Attributes.Add("onkeyup", " ValidarLength(this, 700);")
                tbEdad.Attributes.Add("onkeypress", " ValidarLength(this, 2);")
                tbEdad.Attributes.Add("onkeyup", " ValidarLength(this, 2);")
                tbEdadVacuna.Attributes.Add("onkeypress", " ValidarLength(this, 2);")
                tbEdadVacuna.Attributes.Add("onkeyup", " ValidarLength(this, 2);")
                tbCantidadPres.Attributes.Add("onkeypress", " ValidarLength(this, 10);")
                tbCantidadPres.Attributes.Add("onkeyup", " ValidarLength(this, 10);")
                tbDosisMedi.Attributes.Add("onkeypress", " ValidarLength(this, 200);")
                tbDosisMedi.Attributes.Add("onkeyup", " ValidarLength(this, 200);")
                tbObservacionMedi.Attributes.Add("onkeypress", " ValidarLength(this, 200);")
                tbObservacionMedi.Attributes.Add("onkeyup", " ValidarLength(this, 200);")
                tbObservacionTallaPeso.Attributes.Add("onkeypress", " ValidarLength(this, 200);")
                tbObservacionTallaPeso.Attributes.Add("onkeyup", " ValidarLength(this, 200);")
                tbResultadoTipoControl.Attributes.Add("onkeypress", " ValidarLength(this, 200);")
                tbResultadoTipoControl.Attributes.Add("onkeyup", " ValidarLength(this, 200);")
                cargarCombos()
                ddlAnioDatosSeguro.Enabled = False
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim usp_mensaje As String = ""
        Try
            If ValidarBusqueda(usp_mensaje) = True Then
                listar()
            Else
                'MENSAJE DE CRITERIOS MINIMOS DE BUSQUEDA

                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If

            tbEdad.Text = 0
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        VerRegistro("Actualización")
    End Sub

    Protected Sub btnFichaCancelar_Click()
        CancelarFicha()
    End Sub

    Protected Sub btnGrabar_click()
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim id_codigo As Integer = 0
            id_codigo = hd_Codigo.Value

            Dim obj_BL_FichaMedicaAlumno As New bl_FichaMedicasAlumnos
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

            int_CodigoAccion = 8
            Dim ds_Lista As DataSet = obj_BL_FichaMedicaAlumno.FUN_GET_FichaMedicasAlumnos(id_codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

            Dim gradoActual As Integer = 0
            gradoActual = ds_Lista.Tables(0).Rows(0).Item("GradoActual").ToString()

            Dim usp_mensaje As String = ""
            If validarFicha(gradoActual, usp_mensaje) Then
                int_CodigoAccion = 1
                GrabarFicha()
            Else
                MostrarAlertas(usp_mensaje)
            End If

            'GrabarFicha()

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        limpiarFiltros()
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

    Protected Sub ddlEstadoAlumno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoAlumno.SelectedIndexChanged
        Try
            If ddlEstadoAlumno.SelectedValue = 0 Then
                ddlSede.Enabled = True
                ddlNiveles.Enabled = True
            ElseIf ddlEstadoAlumno.SelectedValue = 1 Then

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlTipoAlergia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        cargarComboAlergia()
        pnModalAlergia.Show()
    End Sub
#End Region

#Region "Metodos"

#Region "Carga los ComboBox"

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
    ''' Carga la informacion en el seleccionable de familiares.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     20/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Listar_Familiares(ByVal str_CodigoAlumno As String)

        Dim bl_obj_Familiares As New bl_Familiares
        Dim be_obj_MaestroPersona As New be_MaestroPersonas
        Dim ds_lista As DataSet
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        ds_lista = bl_obj_Familiares.FUN_LIS_FamiliaresPorAlumno(str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 63)
        Controles.llenarCombo(ddl_Familiar_PriTitular, ds_lista, "CodigoFamiliar", "NombreCompleto", False, True)
        Controles.llenarCombo(ddl_Familiar_SegTitular, ds_lista, "CodigoFamiliar", "NombreCompleto", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)
        Controles.llenarCombo(ddlAnioAcademico1, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlAnioAcademico2, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlAnioDatosSeguro, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlAnioRentaEst, ds_Lista, "Codigo", "Descripcion", False, True)

        ddlAnioRentaEst.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlAnioDatosSeguro.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlAnioAcademico1.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlAnioAcademico2.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Sedes disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSedesColegio()

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlSede, ds_Lista, "Codigo", "NombreSede", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Estado Alumno disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        Johnatan Matta
    ''' Fecha de modificación: 29/09/2011
    ''' </remarks>
    Private Sub cargarComboEstadoAlumno()

        Dim obj_BL_EstadoAlumno As New bl_EstadosAlumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_EstadoAlumno.FUN_LIS_EstadosAlumnos(int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlEstadoAlumno, ds_Lista, "Codigo", "Descripcion", False, True)

        ddlEstadoAlumno.SelectedValue = 1
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Enfermedades disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboEnfermedad()

        Dim obj_BL_Enfermedad As New bl_Enfermedades
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Enfermedad.FUN_LIS_Enfermedad("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlEnfermedad, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Vacunas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboVacuna()

        Dim obj_BL_Vacuna As New bl_Vacunas
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Vacuna.FUN_LIS_Vacuna("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlTipoVacuna, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Dosis disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDosisVacuna()

        Dim obj_BL_DosisVacuna As New bl_DosisVacunas
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_DosisVacuna.FUN_LIS_DosisVacuna("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlDosis, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Medicamentos de alumnos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     21/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboMedicamentoAlumno()

        Dim obj_BL_MedicamentosAlumnos As New bl_MedicamentosAlumnos
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_MedicamentosAlumnos.FUN_LIS_MedicamentosAlumnos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlMedicamento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Presentacion de medicamentos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     21/07/2011
    ''' Modificado por:        __________
    ''' Fecha de modificación: __________
    ''' </remarks>
    Private Sub cargarComboPresentacion()

        Dim obj_BL_PresentacionMedicamento As New bl_PresentacionesMedicamentos
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_PresentacionMedicamento.FUN_LIS_PresentacionMedicamento("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlPresentacion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Alergias disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAlergia()

        Dim obj_BL_Alergia As New bl_Alergias
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Alergia.FUN_LIS_Alergia("", ddlTipoAlergia.SelectedValue, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlAlergia, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Alergias disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipAlergia()

        Dim obj_BL_TiposAlergias As New bl_TiposAlergias
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TiposAlergias.FUN_LIS_TipoAlergia("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlTipoAlergia, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Caracteristicas de la Piel disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboCaracteristicasPiel()

        Dim obj_BL_TiposCaracteristicasPiel As New bl_TiposCaracteristicasPiel
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TiposCaracteristicasPiel.FUN_LIS_TipoCaracteristicaPiel("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlCaracteristicaPiel, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Hospitalizaciones disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboMotivosHospitalizaciones()

        Dim obj_BL_MotivosHospitalizaciones As New bl_MotivosHospitalizaciones
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_MotivosHospitalizaciones.FUN_LIS_MotivoHospitalizacion("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlHospitalizacion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Operaciones Médicas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTiposOperacionesMedicas()

        Dim obj_BL_TiposOperacionesMedicas As New bl_TiposOperacionesMedicas
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TiposOperacionesMedicas.FUN_LIS_TipoOperacionMedica("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlOperacion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipos Controles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTiposControles()

        Dim obj_BL_TiposControles As New bl_TiposControles
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TiposControles.FUN_LIS_TipoControl("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlTipoControl, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipos Controles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     23/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoSeguro()

        Dim obj_BL_TiposSeguro As New bl_TiposSeguro
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TiposSeguro.FUN_LIS_TipoSeguro("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlTipoSeguro, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipos Controles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     23/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CargarComboCompañia()

        Dim obj_BL_CompaniaSeguro As New bl_CompaniaSeguro
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_CompaniaSeguro.FUN_LIS_CompaniaSeguro("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlCompania, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipos Controles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     23/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CargarComboClinicas()

        Dim obj_BL_Clinicas As New bl_Clinicas
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Clinicas.FUN_LIS_ClinicasXCodigoTipoSeguro("", CInt(ddlTipoSeguro.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlClinica1, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlClinica2, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlClinica3, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Niveles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlNiveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de SubNiveles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSubNivel()

        Dim obj_BL_SubNiveles As New bl_SubNiveles
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(CInt(ddlNiveles.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlSubniveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Grados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(CInt(ddlSubniveles.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlGrados, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Aulas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(CInt(ddlGrados.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlAulas, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipos de Nacimiento disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoNacimiento()

        Dim obj_BL_TipoNacimiento As New bl_TiposNacimientos
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TipoNacimiento.FUN_LIS_TipoNacimiento("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlTipoNacimiento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipos de Sangre disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:                Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoSangre()

        Dim obj_BL_TipoSangre As New bl_TiposSangre
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TipoSangre.FUN_LIS_TipoSangre("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlTipoSangre, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de los meses
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombosMeses()

        Dim ds_Lista As DataSet
        Dim int_Inicio As Integer = 0
        Dim int_Fin As Integer = 12
        ds_Lista = Controles.ListaNumerica(int_Inicio, int_Fin)
        Controles.llenarCombo(ddlMesesLevCabeza, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlMesesSento, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlMesesParo, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlMesesCamino, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlMesesControloEsfinteres, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlMesesHabloPrimerasPalabras, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlMesesHabloFluidez, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de los años
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombosEdad()

        Dim ds_Lista As DataSet
        Dim int_Inicio As Integer = 0
        Dim int_Fin As Integer = 6
        ds_Lista = Controles.ListaNumerica(int_Inicio, int_Fin)
        Controles.llenarCombo(ddlEdadLevCabeza, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlEdadSento, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlEdadParo, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlEdadCamino, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlEdadControloEsfinteres, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlEdadHabloPrimerasPalabras, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlEdadHabloFluidez, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

#End Region

    ''' <summary>
    ''' Setea las acciones de acceso del usuario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(1, 1)

        'CONTROLES DEL FORMULARIO

        Master.BloqueoControles(btnBuscar, 1)
        Master.BloqueoControles(btnGrabar, 1)

        Master.SeteoPermisosAcciones(btnBuscar, 1)
        Master.SeteoPermisosAcciones(btnGrabar, 1)

        'GRUPOS DE INFORMACION
        Master.BloqueoControles(Bloque_ControlSalud, 2)
        Master.BloqueoControles(Bloque_DesarrolloInfantil, 2)
        Master.BloqueoControles(Bloque_DatosAlumno, 2)
        Master.BloqueoControles(Bloque_EstadoSalud, 2)
        Master.BloqueoControles(Bloque_OtrosDatosMedicos, 2)

        Master.SeteoBloquesInformacion(Bloque_ControlSalud, 1)
        Master.SeteoBloquesInformacion(Bloque_DesarrolloInfantil, 1)
        Master.SeteoBloquesInformacion(Bloque_DatosAlumno, 1)
        Master.SeteoBloquesInformacion(Bloque_EstadoSalud, 1)
        Master.SeteoBloquesInformacion(Bloque_OtrosDatosMedicos, 1)

    End Sub

    ''' <summary>
    ''' Obtiene y setea los datos de la Ficha Médica en el Formulario.    
    ''' </summary>
    ''' <param name="int_Codigo">Codigo del alumno</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As Integer)

        Dim obj_BL_FichaMedicaAlumno As New bl_FichaMedicasAlumnos
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_FichaMedicaAlumno.FUN_GET_FichaMedicasAlumnos(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

        If ds_Lista.Tables(0).Rows.Count > 0 Then

            img_FotoUsuario.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & ds_Lista.Tables(0).Rows(0).Item("rutaFoto").ToString
            hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString)
            lblNombreAlumno.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString
            'lblSede.Text = ds_Lista.Tables(0).Rows(0).Item("Sede").ToString
            lblSituacionAnio.Text = ds_Lista.Tables(0).Rows(0).Item("estadoAnioActualAlumno").ToString
            lblENSnGS.Text = ds_Lista.Tables(0).Rows(0).Item("ENSnGS").ToString
            ddlTipoNacimiento.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoNacimiento").ToString)
            lblTipoNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("Nacimiento").ToString
            tbObservaciones.Text = ds_Lista.Tables(0).Rows(0).Item("Observacion").ToString
            lblObservaciones.Text = ds_Lista.Tables(0).Rows(0).Item("Observacion").ToString
            ddlEdadLevCabeza.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadLevantoCabeza").ToString)
            ddlMesesLevCabeza.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString)
            lblEdadLevCabeza.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadLevantocabeza").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadLevantocabeza").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadLevantocabeza") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadLevantocabeza") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadLevantocabeza").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " meses ")))
            ddlEdadSento.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadSento").ToString)
            ddlMesesSento.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesSento").ToString)
            lblEdadSento.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadSento").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadSento").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadSento") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadSento") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesSento").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadSento").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesSento").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesSento") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesSento") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesSento").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesSento") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesSento") & " meses ")))
            ddlEdadParo.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadParo").ToString)
            ddlMesesParo.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesParo").ToString)
            lblEdadParo.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadParo").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadParo").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadParo") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadParo") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesParo").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadParo").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesParo").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesParo") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesParo") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesParo").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesParo") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesParo") & " meses ")))
            ddlEdadCamino.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadCamino").ToString)
            ddlMesesCamino.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesCamino").ToString)
            lblEdadCamino.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadCamino").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadCamino").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadCamino") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadCamino") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesCamino").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadCamino").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesCamino").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesCamino") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesCamino") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesCamino").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesCamino") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesCamino") & " meses ")))
            ddlEdadControloEsfinteres.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString)
            ddlMesesControloEsfinteres.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString)
            lblEdadControloEsfinteres.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " meses ")))
            ddlEdadHabloPrimerasPalabras.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString)
            ddlMesesHabloPrimerasPalabras.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString)
            lblEdadHabloPrimerasPalabras.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " meses ")))
            ddlEdadHabloFluidez.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString)
            ddlMesesHabloFluidez.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString)
            lblEdadHabloFluidez.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez") & " meses ")))
            ddlTipoSangre.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoTipoSangre").ToString)
            lblTipoSangre_ver.Text = ds_Lista.Tables(0).Rows(0).Item("TipoSangre").ToString

            'If ds_Lista.Tables(0).Rows(0).Item("TabiqueDesviado").ToString = "True" Then
            '    rbTabiqueDesviado.SelectedValue = 1
            '    lblTabiqueDesviado.Text = "Si"
            'ElseIf ds_Lista.Tables(0).Rows(0).Item("TabiqueDesviado").ToString = "False" Then
            '    rbTabiqueDesviado.SelectedValue = 0
            '    lblTabiqueDesviado.Text = "No"
            'End If

            If ds_Lista.Tables(0).Rows(0).Item("TabiqueDesviado").ToString = "1" Then
                rbTabiqueDesviado.SelectedValue = 1
                lblTabiqueDesviado.Text = "Si"
            ElseIf ds_Lista.Tables(0).Rows(0).Item("TabiqueDesviado").ToString = "0" Then
                rbTabiqueDesviado.SelectedValue = 0
                lblTabiqueDesviado.Text = "No"
            End If

            'If CBool(ds_Lista.Tables(0).Rows(0).Item("SangradoNasal").ToString) Then
            '    rbSangradoNasal.SelectedValue = 1
            '    lblSangradoNasal.Text = "Si"
            'Else
            '    rbSangradoNasal.SelectedValue = 0
            '    lblSangradoNasal.Text = "No"
            'End If
            If ds_Lista.Tables(0).Rows(0).Item("SangradoNasal").ToString = "1" Then
                rbSangradoNasal.SelectedValue = 1
                lblSangradoNasal.Text = "Si"
            ElseIf ds_Lista.Tables(0).Rows(0).Item("SangradoNasal").ToString = "0" Then
                rbSangradoNasal.SelectedValue = 0
                lblSangradoNasal.Text = "No"
            End If

            tbObservacionesOftalmologicas.Text = ds_Lista.Tables(0).Rows(0).Item("ObservacionesOftalmologicas").ToString
            lblObservacionesOftamologicas.Text = ds_Lista.Tables(0).Rows(0).Item("ObservacionesOftalmologicas").ToString

            'If CBool(ds_Lista.Tables(0).Rows(0).Item("UsaLentes").ToString) Then
            '    rbUsaLentes.SelectedValue = 1
            '    lblUsaLentes.Text = "Si"
            'Else
            '    rbUsaLentes.SelectedValue = 0
            '    lblUsaLentes.Text = "No"
            'End If
            If ds_Lista.Tables(0).Rows(0).Item("UsaLentes").ToString = "1" Then
                rbUsaLentes.SelectedValue = 1
                lblUsaLentes.Text = "Si"
            ElseIf ds_Lista.Tables(0).Rows(0).Item("UsaLentes").ToString = "0" Then
                rbUsaLentes.SelectedValue = 0
                lblUsaLentes.Text = "No"
            End If

            tbObservacionesDental.Text = ds_Lista.Tables(0).Rows(0).Item("ObservacionesDental").ToString
            lblObservacionesDental.Text = ds_Lista.Tables(0).Rows(0).Item("ObservacionesDental").ToString

            'If CBool(ds_Lista.Tables(0).Rows(0).Item("UsaOrtodoncia").ToString) Then
            '    rbUsaOrtodoncia.SelectedValue = 1
            '    lblUsaOrtodoncia.Text = "Si"
            'Else
            '    rbUsaOrtodoncia.SelectedValue = 0
            '    lblUsaOrtodoncia.Text = "No"
            'End If
            If ds_Lista.Tables(0).Rows(0).Item("UsaOrtodoncia").ToString = "1" Then
                rbUsaOrtodoncia.SelectedValue = 1
                lblUsaOrtodoncia.Text = "Si"
            ElseIf ds_Lista.Tables(0).Rows(0).Item("UsaOrtodoncia").ToString = "0" Then
                rbUsaOrtodoncia.SelectedValue = 0
                lblUsaOrtodoncia.Text = "No"
            End If
        End If

        'Detalle Enfermedad
        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelFichaMedEnEnfermedades") <> -1 Then
            'If ds_Lista.Tables(1).Rows.Count > 0 Then
            gvDetalleEnfermedad.DataSource = ds_Lista.Tables(1)
            gvDetalleEnfermedad.DataBind()
            ViewState("ListaEnfermedad") = ds_Lista.Tables(1)
        Else
            gvDetalleEnfermedad.DataBind()
        End If


        'Detalle Vacuna
        If ds_Lista.Tables(2).Rows(0).Item("CodigoRelVacunasFichaMed") <> -1 Then
            gvDetalleVacuna.DataSource = ds_Lista.Tables(2)
            gvDetalleVacuna.DataBind()
            ViewState("ListaVacuna") = ds_Lista.Tables(2)
        Else
            gvDetalleVacuna.DataBind()
        End If


        'Detalle Alergias
        If ds_Lista.Tables(3).Rows(0).Item("CodigoRelFichaMedAlergias") <> -1 Then
            gvDetalleAlergia.DataSource = ds_Lista.Tables(3)
            gvDetalleAlergia.DataBind()
            ViewState("ListaAlergia") = ds_Lista.Tables(3)
        Else
            gvDetalleAlergia.DataBind()
        End If


        'Detalle Caracteristicas de la piel
        If ds_Lista.Tables(4).Rows(0).Item("CodigoRelFichaMedCaractPiel") <> -1 Then
            gvDetalleCaracteristicaPiel.DataSource = ds_Lista.Tables(4)
            gvDetalleCaracteristicaPiel.DataBind()
            ViewState("ListaCaracteristicasPiel") = ds_Lista.Tables(4)
        Else
            gvDetalleCaracteristicaPiel.DataBind()
        End If


        'Detalle Medicamento
        If ds_Lista.Tables(5).Rows(0).Item("CodigoRelFichaAtenMedicamentos") <> -1 Then
            gvDetalleMedicamento.DataSource = ds_Lista.Tables(5)
            gvDetalleMedicamento.DataBind()
            ViewState("ListaMedicamentos") = ds_Lista.Tables(5)
        Else
            gvDetalleMedicamento.DataBind()
        End If

        'Hospitalizacion
        If ds_Lista.Tables(6).Rows(0).Item("CodigoRelFichaMedMotivoHosp") <> -1 Then
            gvDetalleHospitalizacion.DataSource = ds_Lista.Tables(6)
            gvDetalleHospitalizacion.DataBind()
            ViewState("ListaHospitalizacion") = ds_Lista.Tables(6)
        Else
            gvDetalleHospitalizacion.DataBind()
        End If


        'Operacion
        If ds_Lista.Tables(7).Rows(0).Item("CodigoRelFichaMedOperaciones") <> -1 Then
            gvDetalleOperacion.DataSource = ds_Lista.Tables(7)
            gvDetalleOperacion.DataBind()
            ViewState("ListaOperacion") = ds_Lista.Tables(7)
        Else
            gvDetalleOperacion.DataBind()
        End If

        'ControlPesoTalla
        If ds_Lista.Tables(8).Rows(0).Item("CodigoControlPesoTalla") <> -1 Then
            gvDetalleControlPesoTalla.DataSource = ds_Lista.Tables(8)
            gvDetalleControlPesoTalla.DataBind()
            ViewState("ListaControlPesoTalla") = ds_Lista.Tables(8)
        Else
            gvDetalleControlPesoTalla.DataBind()
        End If

        'Otros Controles
        If ds_Lista.Tables(9).Rows(0).Item("CodigoRelFichaMedTiposControles") <> -1 Then
            gvDetalleTipoControl.DataSource = ds_Lista.Tables(9)
            gvDetalleTipoControl.DataBind()
            ViewState("ListaTipoControl") = ds_Lista.Tables(9)
        Else
            gvDetalleTipoControl.DataBind()
        End If

        'Datos del Seguro
        If ds_Lista.Tables(10).Rows(0).Item("CodigoRelFichaMedDatosSeguro") <> -1 Then
            gvDetalleDatosSeguro.DataSource = ds_Lista.Tables(10)
            gvDetalleDatosSeguro.DataBind()
            ViewState("ListaDatosSeguro") = ds_Lista.Tables(10)
        Else
            gvDetalleDatosSeguro.DataBind()
        End If

        'Datos de la Renta Estudiantil
        If ds_Lista.Tables(11).Rows(0).Item("CodigoRelFichaMedRentaEstudiantil") <> -1 Then
            gvDetalleRentaEstudiantil.DataSource = ds_Lista.Tables(11)
            gvDetalleRentaEstudiantil.DataBind()
            ViewState("ListaRentaEstudiantil") = ds_Lista.Tables(11)
        Else
            gvDetalleRentaEstudiantil.DataBind()
        End If

    End Sub

    ''' <summary>
    ''' Lista los datos de la Ficha medica      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        SortGridView(ViewState("SortExpression"), ViewState("Direccion"))

        If hfTotalRegs.Value > 0 Then
            ImagenSorting(ViewState("SortExpression"))
        End If

    End Sub

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


        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_FichaMedicaAlumno As New bl_FichaMedicasAlumnos
            ds_Lista = obj_BL_FichaMedicaAlumno.FUN_LIS_FichaMedicaAlumno(str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_EstadoAlumno, int_Nivel, int_Subnivel, int_Grado, int_Aula, int_PeriodoInicio, int_PeriodoFin, int_Sede, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_FichaMedicaAlumno As New bl_FichaMedicasAlumnos
                ds_Lista = obj_BL_FichaMedicaAlumno.FUN_LIS_FichaMedicaAlumno(str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_EstadoAlumno, int_Nivel, int_Subnivel, int_Grado, int_Aula, int_PeriodoInicio, int_PeriodoFin, int_Sede, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Valida la ficha antes de proceder a grabarla
    ''' </summary>
    ''' <param name="int_GradoActual">Grado Actual del alumno(considerado para el tipo de validación ha aplicar)</param>
    ''' <param name="str_Mensaje">variable de cadena que acumulara todos los mensajes de la validación</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarFicha(ByVal int_GradoActual As Integer, ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        'If int_GradoActual = 1 Or int_GradoActual = 2 Or int_GradoActual = 3 Then

        '    If ddlTipoNacimiento.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Nacimiento")
        '        result = False
        '    End If

        '    If ddlEdadLevCabeza.SelectedValue = 0 And ddlMesesLevCabeza.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Edad que levantó la cabeza")
        '        result = False
        '    End If

        '    If ddlEdadSento.SelectedValue = 0 And ddlMesesSento.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Edad que se sentó")
        '        result = False
        '    End If

        '    If ddlEdadParo.SelectedValue = 0 And ddlMesesParo.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Edad que se paró")
        '        result = False
        '    End If

        '    If ddlEdadCamino.SelectedValue = 0 And ddlMesesCamino.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Edad que caminó")
        '        result = False
        '    End If

        '    If ddlEdadControloEsfinteres.SelectedValue = 0 And ddlMesesControloEsfinteres.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Edad que controló sus esfínteres")
        '        result = False
        '    End If

        '    If ddlEdadHabloPrimerasPalabras.SelectedValue = 0 And ddlMesesHabloPrimerasPalabras.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Edad que pronunció las primeras palabras")
        '        result = False
        '    End If

        '    If ddlEdadHabloFluidez.SelectedValue = 0 And ddlMesesHabloFluidez.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Edad que se comunicó con fluídez")
        '        result = False
        '    End If

        '    If ddlTipoSangre.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Sangre")
        '        result = False
        '    End If
        '    GrabarFicha()
        'Else
        '    If ddlTipoSangre.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Sangre")
        '        result = False
        '    End If
        '    GrabarFicha()
        'End If

        If (tbObservaciones.Text.Trim).Length > 0 Then
            If Validacion.ValidarCamposIngreso(tbObservaciones) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Observaciones de Nacimiento")
                result = False
            End If
        End If

        If (tbObservacionesOftalmologicas.Text.Trim).Length > 0 Then
            If Validacion.ValidarCamposIngreso(tbObservacionesOftalmologicas) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Descripción Oftamologica")
                result = False
            End If
        End If

        If (tbObservacionesDental.Text.Trim).Length > 0 Then
            If Validacion.ValidarCamposIngreso(tbObservacionesDental) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Descripción Dental")
                result = False
            End If
        End If

        str_Mensaje = str_alertas
        Return result

    End Function


    ''' <summary>
    ''' Valida el medicamento antes de proceder a grabar
    ''' </summary>
    ''' <param name="str_Mensaje">variable de cadena que acumulara todos los mensajes de la validación</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     21/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarMedicamento(ByRef str_Mensaje As String) As Boolean
        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlMedicamento.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Medicamento")
            result = False
        End If

        If ddlPresentacion.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Presentación")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Graba los datos de la Ficha Médica y sus respectivos detalles   
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarFicha()

        Dim obj_BE_FichaMedica As New be_FichaMedica
        Dim obj_BL_FichaMedica As New bl_FichaMedicasAlumnos
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        'Datos Generales
        obj_BE_FichaMedica.CodigoAlumno = hd_Codigo.Value

        ''Desarrollo Infantil

        obj_BE_FichaMedica.CodigoTipoNacimiento = ddlTipoNacimiento.SelectedValue
        obj_BE_FichaMedica.TipoNacimientoObservaciones = tbObservaciones.Text
        obj_BE_FichaMedica.EdadLevantoCabeza = ddlEdadLevCabeza.Text
        obj_BE_FichaMedica.MesesLevantoCabeza = ddlMesesLevCabeza.Text
        obj_BE_FichaMedica.EdadSento = ddlEdadSento.SelectedValue
        obj_BE_FichaMedica.MesesSento = ddlMesesSento.SelectedValue
        obj_BE_FichaMedica.EdadParo = ddlEdadParo.SelectedValue
        obj_BE_FichaMedica.MesesParo = ddlMesesParo.SelectedValue
        obj_BE_FichaMedica.EdadCamino = ddlEdadCamino.SelectedValue
        obj_BE_FichaMedica.MesesCamino = ddlMesesCamino.SelectedValue
        obj_BE_FichaMedica.EdadControloEsfinteres = ddlEdadControloEsfinteres.SelectedValue
        obj_BE_FichaMedica.MesesControloEsfinteres = ddlMesesControloEsfinteres.SelectedValue
        obj_BE_FichaMedica.EdadHabloPrimerasPalabras = ddlEdadHabloPrimerasPalabras.SelectedValue
        obj_BE_FichaMedica.MesesHabloPrimerasPalabras = ddlMesesHabloPrimerasPalabras.SelectedValue
        obj_BE_FichaMedica.EdadHabloFluidez = ddlEdadHabloFluidez.SelectedValue
        obj_BE_FichaMedica.MesesHabloFluidez = ddlMesesHabloFluidez.SelectedValue

        'Estado de Salud
        obj_BE_FichaMedica.CodigoTipoSangre = ddlTipoSangre.SelectedValue

        'Otros Datos medicos

        obj_BE_FichaMedica.TabiqueDesviado = IIf(rbTabiqueDesviado.SelectedValue.ToString.Length > 0, rbTabiqueDesviado.SelectedValue, -1)
        obj_BE_FichaMedica.SangradoNasal = IIf(rbSangradoNasal.SelectedValue.ToString.Length > 0, rbSangradoNasal.SelectedValue, -1)
        obj_BE_FichaMedica.ObservacionesOftalmologicas = tbObservacionesOftalmologicas.Text
        obj_BE_FichaMedica.UsaLentes = IIf(rbUsaLentes.SelectedValue.ToString.Length > 0, rbUsaLentes.SelectedValue, -1)
        obj_BE_FichaMedica.ObservacionesDental = tbObservacionesDental.Text
        obj_BE_FichaMedica.UsaOrtodoncia = IIf(rbUsaOrtodoncia.SelectedValue.ToString.Length > 0, rbUsaOrtodoncia.SelectedValue, -1)

        ''Detalle
        Dim objDS_Detalle As New DataSet

        ''Detalle Enfermedades
        Dim objDT_Enfermedad As DataTable

        objDT_Enfermedad = New DataTable("ListaEnfermedad")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "CodigoRelFichaMedEnEnfermedades", "String")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "CodigoEnfermedad", "String")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "Enfermedad", "String")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "Edad", "Integer")

        Dim dr_Enfermedad As DataRow

        For Each drv As GridViewRow In gvDetalleEnfermedad.Rows

            dr_Enfermedad = objDT_Enfermedad.NewRow
            dr_Enfermedad.Item("CodigoRelFichaMedEnEnfermedades") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Enfermedad.Item("CodigoEnfermedad") = CType(drv.FindControl("lblCodigoEnfermedad"), Label).Text
            dr_Enfermedad.Item("Enfermedad") = CType(drv.FindControl("lblEnfermedad"), Label).Text
            dr_Enfermedad.Item("Edad") = CType(drv.FindControl("lblEdadEnfermedad_grilla"), Label).Text
            objDT_Enfermedad.Rows.Add(dr_Enfermedad)

        Next

        ''Detalle Vacuna
        Dim objDT_Vacuna As DataTable

        objDT_Vacuna = New DataTable("ListaVacuna")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "CodigoRelVacunasFichaMed", "String")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "CodigoVacuna", "String")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "CodigoDosis", "String")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "Edad", "String")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "FechaVacunacion", "Date")

        Dim dr_Vacuna As DataRow

        For Each drv As GridViewRow In gvDetalleVacuna.Rows

            dr_Vacuna = objDT_Vacuna.NewRow
            dr_Vacuna.Item("CodigoRelVacunasFichaMed") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Vacuna.Item("CodigoVacuna") = CType(drv.FindControl("lblCodigoVacuna"), Label).Text
            dr_Vacuna.Item("CodigoDosis") = CType(drv.FindControl("lblCodigoDosis"), Label).Text
            dr_Vacuna.Item("Edad") = CType(drv.FindControl("lblEdadVacuna"), Label).Text
            dr_Vacuna.Item("FechaVacunacion") = CType(drv.FindControl("lblFechaVacunacion"), Label).Text
            objDT_Vacuna.Rows.Add(dr_Vacuna)

        Next

        ''Detalle CaracteristicasPiel
        Dim objDT_CaracteristicasPiel As DataTable

        objDT_CaracteristicasPiel = New DataTable("ListaCaracteristicasPiel")
        objDT_CaracteristicasPiel = Datos.agregarColumna(objDT_CaracteristicasPiel, "CodigoRelFichaMedCaractPiel", "String")
        objDT_CaracteristicasPiel = Datos.agregarColumna(objDT_CaracteristicasPiel, "CodigoCaracteristicapiel", "String")
        objDT_CaracteristicasPiel = Datos.agregarColumna(objDT_CaracteristicasPiel, "FechaRegistro", "Date")

        Dim dr_CaracteristicasPiel As DataRow

        For Each drv As GridViewRow In gvDetalleCaracteristicaPiel.Rows

            dr_CaracteristicasPiel = objDT_CaracteristicasPiel.NewRow
            dr_CaracteristicasPiel.Item("CodigoRelFichaMedCaractPiel") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_CaracteristicasPiel.Item("CodigoCaracteristicapiel") = CType(drv.FindControl("lblCodigoCaracteristicapiel"), Label).Text
            dr_CaracteristicasPiel.Item("FechaRegistro") = CType(drv.FindControl("lblFechaRegistroCaracteristicapiel"), Label).Text
            objDT_CaracteristicasPiel.Rows.Add(dr_CaracteristicasPiel)

        Next

        ''Detalle Medicamento
        Dim objDT_Medicamento As DataTable

        objDT_Medicamento = New DataTable("ListaMedicamento")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CodigoRelFichaAtenMedicamentos", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CodigoMedicamento", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CodigoPresentacion", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CantidadPresentacion", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "DosisMedicamento", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "Observaciones", "String")
        Dim dr_Medicamento As DataRow

        For Each drv As GridViewRow In gvDetalleMedicamento.Rows

            dr_Medicamento = objDT_Medicamento.NewRow
            dr_Medicamento.Item("CodigoRelFichaAtenMedicamentos") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Medicamento.Item("CodigoMedicamento") = CType(drv.FindControl("lblCodigoMedicamento"), Label).Text
            dr_Medicamento.Item("CodigoPresentacion") = CType(drv.FindControl("lblCodigoPresentacion"), Label).Text
            dr_Medicamento.Item("CantidadPresentacion") = CType(drv.FindControl("lblCantidadPresentacion"), Label).Text
            dr_Medicamento.Item("DosisMedicamento") = CType(drv.FindControl("lblDosisMedicamento"), Label).Text
            dr_Medicamento.Item("Observaciones") = CType(drv.FindControl("lblObservaciones"), Label).Text
            objDT_Medicamento.Rows.Add(dr_Medicamento)

        Next

        ''Detalle Hospitalizacion
        Dim objDT_Hospitalizacion As DataTable

        objDT_Hospitalizacion = New DataTable("ListaHospitalizacion")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "CodigoRelFichaMedMotivoHosp", "String")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "CodigoMotivoHospitalizacion", "Integer")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "FechaHospitalizacion", "Date")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "Hospitalizacion", "String")

        Dim dr_Hospitalizacion As DataRow

        For Each drv As GridViewRow In gvDetalleHospitalizacion.Rows

            dr_Hospitalizacion = objDT_Hospitalizacion.NewRow
            dr_Hospitalizacion.Item("CodigoRelFichaMedMotivoHosp") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Hospitalizacion.Item("CodigoMotivoHospitalizacion") = CType(drv.FindControl("lblCodigoMotivoHospitalizacion"), Label).Text
            dr_Hospitalizacion.Item("FechaHospitalizacion") = CType(drv.FindControl("lblFechaHospitalizacion"), Label).Text
            dr_Hospitalizacion.Item("Hospitalizacion") = CType(drv.FindControl("lblHospitalizacion"), Label).Text
            objDT_Hospitalizacion.Rows.Add(dr_Hospitalizacion)

        Next

        ''Detalle Operacion
        Dim objDT_Operacion As DataTable

        objDT_Operacion = New DataTable("ListaOperacion")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "CodigoRelFichaMedOperaciones", "String")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "CodigoTipoOperaciones", "Integer")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "FechaOperacion", "Date")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "Operacion", "String")

        Dim dr_Operacion As DataRow

        For Each drv As GridViewRow In gvDetalleOperacion.Rows

            dr_Operacion = objDT_Operacion.NewRow
            dr_Operacion.Item("CodigoRelFichaMedOperaciones") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Operacion.Item("CodigoTipoOperaciones") = CType(drv.FindControl("lblCodigoTipoOperaciones"), Label).Text
            dr_Operacion.Item("FechaOperacion") = CType(drv.FindControl("lblFechaOperacion"), Label).Text
            dr_Operacion.Item("Operacion") = CType(drv.FindControl("lblOperacion"), Label).Text
            objDT_Operacion.Rows.Add(dr_Operacion)

        Next

        ''Otros Controles
        Dim objDT_TipoControl As DataTable

        objDT_TipoControl = New DataTable("ListaTipoControl")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "CodigoRelFichaMedTiposControles", "String")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "CodigoTipoControl", "Integer")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "FechaControl", "Date")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "Resultado", "String")

        Dim dr_TipoControl As DataRow

        For Each drv As GridViewRow In gvDetalleTipoControl.Rows

            dr_TipoControl = objDT_TipoControl.NewRow
            dr_TipoControl.Item("CodigoRelFichaMedTiposControles") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_TipoControl.Item("CodigoTipoControl") = CType(drv.FindControl("lblCodigoTipoControl"), Label).Text
            dr_TipoControl.Item("FechaControl") = CType(drv.FindControl("lblFechaControl"), Label).Text
            dr_TipoControl.Item("Resultado") = CType(drv.FindControl("lblResultado"), Label).Text
            objDT_TipoControl.Rows.Add(dr_TipoControl)

        Next

        ''Control Peso -Talla
        Dim objDT_ControlPesoTalla As DataTable

        objDT_ControlPesoTalla = New DataTable("ListaControlPesoTalla")
        objDT_ControlPesoTalla = Datos.agregarColumna(objDT_ControlPesoTalla, "CodigoControlPesoTalla", "Integer")
        objDT_ControlPesoTalla = Datos.agregarColumna(objDT_ControlPesoTalla, "Talla", "Decimal")
        objDT_ControlPesoTalla = Datos.agregarColumna(objDT_ControlPesoTalla, "Peso", "Decimal")
        objDT_ControlPesoTalla = Datos.agregarColumna(objDT_ControlPesoTalla, "FechaControl", "Date")
        objDT_ControlPesoTalla = Datos.agregarColumna(objDT_ControlPesoTalla, "Observaciones", "String")

        Dim dr_ControlPesoTalla As DataRow

        For Each drv As GridViewRow In gvDetalleControlPesoTalla.Rows

            dr_ControlPesoTalla = objDT_ControlPesoTalla.NewRow
            dr_ControlPesoTalla.Item("CodigoControlPesoTalla") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_ControlPesoTalla.Item("Talla") = IIf(CType(drv.FindControl("lblTalla"), Label).Text = "", -1, CType(drv.FindControl("lblTalla"), Label).Text)
            dr_ControlPesoTalla.Item("Peso") = IIf(CType(drv.FindControl("lblPeso"), Label).Text = "", -1, CType(drv.FindControl("lblPeso"), Label).Text)
            dr_ControlPesoTalla.Item("FechaControl") = CType(drv.FindControl("lblFechaControlPesoTalla"), Label).Text
            dr_ControlPesoTalla.Item("Observaciones") = CType(drv.FindControl("lblObservacionesPesoTalla"), Label).Text
            objDT_ControlPesoTalla.Rows.Add(dr_ControlPesoTalla)

        Next

        ''Alergia
        Dim objDT_Alergia As DataTable

        objDT_Alergia = New DataTable("ListaAlergia")
        objDT_Alergia = Datos.agregarColumna(objDT_Alergia, "CodigoRelFichaMedAlergias", "Integer")
        objDT_Alergia = Datos.agregarColumna(objDT_Alergia, "CodigoAlergia", "Integer")
        objDT_Alergia = Datos.agregarColumna(objDT_Alergia, "FechaRegistro", "Date")

        Dim dr_Alergia As DataRow

        For Each drv As GridViewRow In gvDetalleAlergia.Rows

            dr_Alergia = objDT_Alergia.NewRow
            dr_Alergia.Item("CodigoRelFichaMedAlergias") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Alergia.Item("CodigoAlergia") = CType(drv.FindControl("lblCodigoAlergia"), Label).Text
            dr_Alergia.Item("FechaRegistro") = CType(drv.FindControl("lblFechaRegistroAlergia"), Label).Text
            objDT_Alergia.Rows.Add(dr_Alergia)

        Next

        'Detalle Ficha Seguro
        Dim objDT_FichaSeguro As DataTable

        If ViewState("ListaDatosSeguro") Is Nothing Then
            objDT_FichaSeguro = New DataTable("ListaDatosSeguro")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "CodigoRelFichaMedDatosSeguro", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "CodigoTipoSeguro", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "CodigoCompania", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "CodigoAnio", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "Vigencia", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "FechaInicio", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "FechaFin", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "CodigoClinica", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "AmbulanciaCompania", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "TelefonoAmbulancia", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "CopiaCarnetSeguro", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "AnioMatricula", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "Tipo", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "Compania", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "NumeroPoliza", "String")
            objDT_FichaSeguro = Datos.agregarColumna(objDT_FichaSeguro, "Clinica", "String")
        Else
            objDT_FichaSeguro = ViewState("ListaDatosSeguro")
        End If

        'Detalle Renta Estudiantil
        Dim objDT_FichaRentaEstudiantil As DataTable

        If ViewState("ListaRentaEstudiantil") Is Nothing Then
            objDT_FichaRentaEstudiantil = New DataTable("ListaRentaEstudiantil")
            objDT_FichaRentaEstudiantil = Datos.agregarColumna(objDT_FichaRentaEstudiantil, "CodigoRelFichaMedRentaEstudiantil", "String")
            objDT_FichaRentaEstudiantil = Datos.agregarColumna(objDT_FichaRentaEstudiantil, "CodigoFamiliarPrimerTitular", "String")
            objDT_FichaRentaEstudiantil = Datos.agregarColumna(objDT_FichaRentaEstudiantil, "CodigoFamiliarSegundoTitular", "String")
            objDT_FichaRentaEstudiantil = Datos.agregarColumna(objDT_FichaRentaEstudiantil, "CodigoAnioAcademico", "String")
            objDT_FichaRentaEstudiantil = Datos.agregarColumna(objDT_FichaRentaEstudiantil, "AnioAcademico", "String")
            objDT_FichaRentaEstudiantil = Datos.agregarColumna(objDT_FichaRentaEstudiantil, "FamiliarPrimerTitular", "String")
            objDT_FichaRentaEstudiantil = Datos.agregarColumna(objDT_FichaRentaEstudiantil, "FamiliarSegundoTitular", "String")
        Else
            objDT_FichaRentaEstudiantil = ViewState("ListaRentaEstudiantil")
        End If

        ''Agrego las DataTable a mi DataSet
        objDS_Detalle.Tables.Add(objDT_Enfermedad)
        objDS_Detalle.Tables.Add(objDT_Vacuna)
        objDS_Detalle.Tables.Add(objDT_CaracteristicasPiel)
        objDS_Detalle.Tables.Add(objDT_Medicamento)
        objDS_Detalle.Tables.Add(objDT_Hospitalizacion)
        objDS_Detalle.Tables.Add(objDT_Operacion)
        objDS_Detalle.Tables.Add(objDT_TipoControl)
        objDS_Detalle.Tables.Add(objDT_ControlPesoTalla)
        objDS_Detalle.Tables.Add(objDT_Alergia)
        objDS_Detalle.Tables.Add(objDT_FichaSeguro)
        objDS_Detalle.Tables.Add(objDT_FichaRentaEstudiantil)

        If BoolGrabar = 0 Then
            'usp_valor = obj_BL_FichaAtencion.FUN_INS_FichaAtencion(obj_BE_FichaAtencion, objDS_Detalle, usp_mensaje)
        Else
            obj_BE_FichaMedica.CodigoAlumno = CInt(BoolGrabar)
            usp_valor = obj_BL_FichaMedica.FUN_UPD_FichaMedicaAlumno(obj_BE_FichaMedica, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnFichaCancelar_Click()
            limpiarCampos()
            listar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Habilita el TabPanel del formulario
    ''' </summary>
    ''' <param name="str_Modo">Nombre del label del tabPanel</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     07/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String)

        miTab1_1.Enabled = False
        miTab2_2.Enabled = True
        lbTab2_2.Text = str_Modo
        TabContainer1.ActiveTabIndex = 0

    End Sub

    ''' <summary>
    ''' Llamada de métodos de los combobox y seteo de las fechas de los formularios.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     07/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombos()

        cargarComboAniosAcademicos()
        cargarComboSedesColegio()
        cargarComboNivel()
        cargarComboSubNivel()
        cargarComboGrado()
        cargarComboAulas()
        cargarComboEnfermedad()
        cargarComboVacuna()
        cargarComboDosisVacuna()
        cargarComboEstadoAlumno()
        cargarComboTipoNacimiento()
        cargarComboTipoSangre()
        tbFechaVacunacion.Text = Now.Date
        tbFechaRegistroAlergia.Text = Now.Date
        tbFechaRegistroCaracteristicasPiel.Text = Now.Date
        tbFechaControlPesoTalla.Text = Now.Date
        'tbFechaMedicamentos.Text = Now.Date
        tbFechaHospitalizacion.Text = Now.Date
        tbFechaOperacion.Text = Now.Date
        tbFechaTipoControl.Text = Now.Date
        cargarComboTipAlergia()
        cargarComboAlergia()
        cargarComboCaracteristicasPiel()
        cargarComboMedicamentoAlumno()
        cargarComboPresentacion()
        cargarComboMotivosHospitalizaciones()
        cargarComboTiposOperacionesMedicas()
        cargarComboTiposControles()
        cargarComboTipoSeguro()
        CargarComboCompañia()
        CargarComboClinicas()
        cargarCombosMeses()
        cargarCombosEdad()
    End Sub

    ''' <summary>
    ''' Método que Limpia los comboBox.
    ''' </summary>
    ''' <param name="combo">Nombre de Combo</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     07/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)

        Controles.limpiarCombo(combo, True, False)

    End Sub

    ''' <summary>
    ''' Habilita el TabPanel del formulario "Busqueda" 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CancelarFicha()

        miTab1_1.Enabled = True
        miTab2_2.Enabled = False
        lbTab2_2.Text = "Actualización"
        TabContainer1.ActiveTabIndex = 0
        tbBuscarApellidoPaterno.Focus()
        limpiarCampos()
        ViewState("VerFicha") = False
    End Sub

    ''' <summary>
    ''' Limpia los campos de Busqueda
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     02/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()
        'Nacimiento
        ddlTipoNacimiento.SelectedValue = 0
        tbObservaciones.text = ""
        'Desarrollo Motor
        ddlEdadLevCabeza.selectedValue = 0
        ddlMesesLevCabeza.selectedValue = 0
        ddlEdadSento.selectedValue = 0
        ddlMesesSento.selectedValue = 0
        ddlEdadParo.selectedValue = 0
        ddlMesesParo.selectedValue = 0
        ddlEdadCamino.selectedValue = 0
        ddlMesesCamino.selectedValue = 0
        'Esfinteres
        ddlEdadControloEsfinteres.selectedValue = 0
        ddlMesesControloEsfinteres.selectedValue = 0
        'Lenguaje
        ddlEdadHabloPrimerasPalabras.selectedValue = 0
        ddlMesesHabloPrimerasPalabras.selectedValue = 0
        ddlEdadHabloFluidez.selectedValue = 0
        ddlMesesHabloFluidez.selectedValue = 0
        'Sangre
        ddlTipoSangre.SelectedValue = 0
        'rb tabique
        rbTabiqueDesviado.SelectedValue = Nothing
        rbSangradoNasal.SelectedValue = Nothing
        rbUsaLentes.SelectedValue = Nothing
        rbUsaOrtodoncia.SelectedValue = Nothing
        'listas
        ViewState("ListaEnfermedad") = Nothing
        ViewState("ListaVacuna") = Nothing
        ViewState("ListaAlergia") = Nothing
        ViewState("ListaCaracteristicasPiel") = Nothing
        ViewState("ListaMedicamentos") = Nothing
        ViewState("ListaHospitalizacion") = Nothing
        ViewState("ListaOperacion") = Nothing
        ViewState("ListaControlPesoTalla") = Nothing
        ViewState("ListaTipoControl") = Nothing
        ViewState("ListaDatosSeguro") = Nothing
        ViewState("ListaRentaEstudiantil") = Nothing

        ViewState.Remove("ListaEnfermedad")
        ViewState.Remove("ListaVacuna")
        ViewState.Remove("ListaAlergia")
        ViewState.Remove("ListaCaracteristicasPiel")
        ViewState.Remove("ListaMedicamentos")
        ViewState.Remove("ListaHospitalizacion")
        ViewState.Remove("ListaOperacion")
        ViewState.Remove("ListaControlPesoTalla")
        ViewState.Remove("ListaTipoControl")
        ViewState.Remove("ListaDatosSeguro")
        ViewState.Remove("ListaRentaEstudiantil")

    End Sub

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
    ''' Todos los label estan visible y los demas objetos como el combo,textbox,.. se encuentran no visible,cuando se da click en la grilla la opcion de "Ver"
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub DatosDeshabilitados()

        btnGrabar.Visible = False
        lblTipoNacimiento.Visible = True
        ddlTipoNacimiento.Visible = False
        lblObservaciones.Visible = True
        tbObservaciones.Visible = False
        lblTipoSangre_ver.Visible = True
        ddlTipoSangre.Visible = False
        lblSangradoNasal.Visible = True
        rbSangradoNasal.Visible = False
        lblTabiqueDesviado.Visible = True
        rbTabiqueDesviado.Visible = False
        lblObservacionesOftamologicas.Visible = True
        tbObservacionesOftalmologicas.Visible = False
        lblUsaLentes.Visible = True
        rbUsaLentes.Visible = False
        lblObservacionesDental.Visible = True
        lblUsaOrtodoncia.Visible = True
        rbUsaOrtodoncia.Visible = False
        tbObservacionesDental.Visible = False
        lblEdadLevCabeza.Visible = True
        ddlEdadLevCabeza.Visible = False
        ddlMesesLevCabeza.Visible = False
        lblAñoLevCabeza.Visible = False
        lblMesesLevCabeza.Visible = False
        lblEdadSento.Visible = True
        ddlEdadSento.Visible = False
        ddlMesesSento.Visible = False
        lblAñoSento.Visible = False
        lblMesesSento.Visible = False
        lblEdadParo.Visible = True
        ddlEdadParo.Visible = False
        ddlMesesParo.Visible = False
        lblAñoParo.Visible = False
        lblMesesParo.Visible = False
        lblEdadCamino.Visible = True
        ddlEdadCamino.Visible = False
        ddlMesesCamino.Visible = False
        lblAñoCamino.Visible = False
        lblMesesCamino.Visible = False
        lblEdadControloEsfinteres.Visible = True
        ddlEdadControloEsfinteres.Visible = False
        ddlMesesControloEsfinteres.Visible = False
        lblAñoControloEsfinteres.Visible = False
        lblMesesControloEsfinteres.Visible = False
        lblEdadHabloPrimerasPalabras.Visible = True
        ddlEdadHabloPrimerasPalabras.Visible = False
        ddlMesesHabloPrimerasPalabras.Visible = False
        lblAñoHabloPrimerasPalabras.Visible = False
        lblMesesHabloPrimerasPalabras.Visible = False
        lblEdadHabloFluidez.Visible = True
        ddlEdadHabloFluidez.Visible = False
        ddlMesesHabloFluidez.Visible = False
        lblAñoHabloFluidez.Visible = False
        lblMesesHabloFluidez.Visible = False
        btn_Add_Enfermedad.Visible = False
        btn_Add_Vacuna.Visible = False
        btn_Add_Alergia.Visible = False
        btn_Add_CaracteristicasPiel.Visible = False
        btn_Add_ControlPesoTalla.Visible = False
        btn_Add_Medicamentos.Visible = False
        btn_Add_Hospitalizacion.Visible = False
        btn_Add_Operacion.Visible = False
        btn_Add_OtrosControles.Visible = False
        ViewState("VerFicha") = True
        'gvDetalleEnfermedad.Columns(0).Visible = False
        'gvDetalleEnfermedad.Columns(1).Visible = False
        'gvDetalleVacuna.Columns(0).Visible = False
        'gvDetalleVacuna.Columns(1).Visible = False
        'gvDetalleAlergia.Columns(0).Visible = False
        'gvDetalleAlergia.Columns(1).Visible = False
        'gvDetalleCaracteristicaPiel.Columns(0).Visible = False
        'gvDetalleCaracteristicaPiel.Columns(1).Visible = False
        'gvDetalleMedicamento.Columns(0).Visible = False
        'gvDetalleMedicamento.Columns(1).Visible = False
        'gvDetalleHospitalizacion.Columns(0).Visible = False
        'gvDetalleHospitalizacion.Columns(1).Visible = False
        'gvDetalleOperacion.Columns(0).Visible = False
        'gvDetalleOperacion.Columns(1).Visible = False
        'gvDetalleControlPesoTalla.Columns(0).Visible = False
        'gvDetalleControlPesoTalla.Columns(1).Visible = False
        'gvDetalleTipoControl.Columns(0).Visible = False
        'gvDetalleTipoControl.Columns(1).Visible = False

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

        tbFechaRegistroAlergia.Enabled = False
        tbFechaRegistroCaracteristicasPiel.Enabled = False
        'tbFechaMedicamentos.Enabled = False
        tbFechaControlPesoTalla.Enabled = False
        tbFechaTipoControl.Enabled = False

        lblTipoNacimiento.Visible = False
        ddlTipoNacimiento.Visible = True
        lblObservaciones.Visible = False
        tbObservaciones.Visible = True
        lblTipoSangre_ver.Visible = False
        ddlTipoSangre.Visible = True
        lblSangradoNasal.Visible = False
        rbSangradoNasal.Visible = True
        lblTabiqueDesviado.Visible = False
        rbTabiqueDesviado.Visible = True
        lblObservacionesOftamologicas.Visible = False
        tbObservacionesOftalmologicas.Visible = True
        lblUsaLentes.Visible = False
        rbUsaLentes.Visible = True
        lblObservacionesDental.Visible = False
        tbObservacionesDental.Visible = True
        lblUsaOrtodoncia.Visible = False
        rbUsaOrtodoncia.Visible = True
        lblEdadLevCabeza.Visible = False
        ddlEdadLevCabeza.Visible = True
        ddlMesesLevCabeza.Visible = True
        lblAñoLevCabeza.Visible = True
        lblMesesLevCabeza.Visible = True
        lblEdadSento.Visible = False
        ddlEdadSento.Visible = True
        ddlMesesSento.Visible = True
        lblAñoSento.Visible = True
        lblMesesSento.Visible = True
        lblEdadParo.Visible = False
        ddlEdadParo.Visible = True
        ddlMesesParo.Visible = True
        lblAñoParo.Visible = True
        lblMesesParo.Visible = True
        lblEdadCamino.Visible = False
        ddlEdadCamino.Visible = True
        ddlMesesCamino.Visible = True
        lblAñoCamino.Visible = True
        lblMesesCamino.Visible = True
        lblEdadControloEsfinteres.Visible = False
        ddlEdadControloEsfinteres.Visible = True
        ddlMesesControloEsfinteres.Visible = True
        lblAñoControloEsfinteres.Visible = True
        lblMesesControloEsfinteres.Visible = True
        lblEdadHabloPrimerasPalabras.Visible = False
        ddlEdadHabloPrimerasPalabras.Visible = True
        ddlMesesHabloPrimerasPalabras.Visible = True
        lblAñoHabloPrimerasPalabras.Visible = True
        lblMesesHabloPrimerasPalabras.Visible = True
        lblEdadHabloFluidez.Visible = False
        ddlEdadHabloFluidez.Visible = True
        ddlMesesHabloFluidez.Visible = True
        lblAñoHabloFluidez.Visible = True
        lblMesesHabloFluidez.Visible = True
        btn_Add_Enfermedad.Visible = True
        btn_Add_Vacuna.Visible = True
        btn_Add_Alergia.Visible = True
        btn_Add_CaracteristicasPiel.Visible = True
        btn_Add_ControlPesoTalla.Visible = True
        btn_Add_Medicamentos.Visible = True
        btn_Add_Hospitalizacion.Visible = True
        btn_Add_Operacion.Visible = True
        btn_Add_OtrosControles.Visible = True
        ViewState("VerFicha") = False

        'gvDetalleEnfermedad.Columns(0).Visible = True
        'gvDetalleEnfermedad.Columns(1).Visible = True
        'gvDetalleVacuna.Columns(0).Visible = True
        'gvDetalleVacuna.Columns(1).Visible = True
        'gvDetalleAlergia.Columns(0).Visible = True
        'gvDetalleAlergia.Columns(1).Visible = True
        'gvDetalleCaracteristicaPiel.Columns(0).Visible = True
        'gvDetalleCaracteristicaPiel.Columns(1).Visible = True
        'gvDetalleMedicamento.Columns(0).Visible = True
        'gvDetalleMedicamento.Columns(1).Visible = True
        'gvDetalleHospitalizacion.Columns(0).Visible = True
        'gvDetalleHospitalizacion.Columns(1).Visible = True
        'gvDetalleOperacion.Columns(0).Visible = True
        'gvDetalleOperacion.Columns(1).Visible = True
        'gvDetalleControlPesoTalla.Columns(0).Visible = True
        'gvDetalleControlPesoTalla.Columns(1).Visible = True
        'gvDetalleTipoControl.Columns(0).Visible = True
        'gvDetalleTipoControl.Columns(1).Visible = True

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
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Visualizar" Or e.CommandName = "Imprimir" Then
                Dim CodigoAlumno As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    DatosHabilitados()
                    Listar_Familiares(CodigoAlumno)
                    obtener(CodigoAlumno)
                    VerRegistro("Actualización")
                    'ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", "controlador.click();", True)
                ElseIf e.CommandName = "Visualizar" Then
                    int_CodigoAccion = 5
                    DatosDeshabilitados()
                    obtener(CodigoAlumno)
                    VerRegistro("Visualización")
                ElseIf e.CommandName = "Imprimir" Then
                    int_CodigoAccion = 9
                    Dim obj_BL_FichaMedica As New bl_FichaMedicasAlumnos
                    Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
                    Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

                    Dim ds_Lista As DataSet = obj_BL_FichaMedica.FUN_GET_FichaMedicasAlumnos(CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

                    Dim reporte_html As String = ""
                    reporte_html = Exportacion.ExportarReporteFichaMedica_Html(ds_Lista, "")
                    Session("Exportaciones_RepFichaMedicaHtml") = reporte_html
                    ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionFichaMedica_html();</script>", False)

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try

            Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
            Dim btnVer As ImageButton = e.Row.FindControl("btnVisualizar")
            Dim btnImprimir As ImageButton = e.Row.FindControl("btnImprimir")

            If e.Row.RowType = DataControlRowType.Pager Then

                Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
                _TotalPags.Text = GridView1.PageCount.ToString

                Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
                _Registros.Text = InformacionPager(GridView1, e.Row, Me)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                'SETEO DE PERMISOS DE ACCIONES---------------
                Master.BloqueoControles(btnActualizar, 1)
                Master.BloqueoControles(btnImprimir, 1)
                Master.BloqueoControles(btnVer, 1)
                '---------------------------------------------

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

                If ddlEstadoAlumno.SelectedValue = 0 Then

                    If e.Row.DataItem("EstadoAlumno") = "Activo" Then
                        Master.SeteoPermisosAcciones(btnActualizar, 1)
                        Master.SeteoPermisosAcciones(btnVer, 1)
                        Master.SeteoPermisosAcciones(btnImprimir, 1)
                    Else
                        btnActualizar.Visible = False

                        Master.SeteoPermisosAcciones(btnVer, 1)
                        Master.SeteoPermisosAcciones(btnImprimir, 1)
                        e.Row.ForeColor = Drawing.Color.DarkRed
                    End If

                ElseIf ddlEstadoAlumno.SelectedValue = 1 Then
                    Master.SeteoPermisosAcciones(btnVer, 1)
                    Master.SeteoPermisosAcciones(btnActualizar, 1)
                    Master.SeteoPermisosAcciones(btnImprimir, 1)
                ElseIf ddlEstadoAlumno.SelectedValue = 2 Or ddlEstadoAlumno.SelectedValue = 3 Then
                    btnActualizar.Visible = False
                    Master.SeteoPermisosAcciones(btnVer, 1)
                    Master.SeteoPermisosAcciones(btnImprimir, 1)
                End If
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

#End Region

#Region "Metodos del Gridview"

    ''' <summary>
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_NombreCompleto"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_ENSnGS"), ImageButton)

        'If _btnSorting.ID = _btnSorting_d1.ID Then
        '    _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d2.ToolTip = "Descendente"
        'Else
        '    _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d1.ToolTip = "Descendente"
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
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     12/01/2011
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
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     12/01/2011
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
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     12/01/2011
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
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     12/01/2011
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

#End Region

#Region "Mantenimientos Detalles"

#Region "Mantenimiento de Detalle Enfermedad"

#Region "Eventos"
    Protected Sub btn_Add_Enfermedad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoEnfermedad") = True
        modal_xxx.Show()
    End Sub

    Protected Sub popup_btnAgregar_Enfermedad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            If validarEnfermedad(usp_mensaje) Then
                If ViewState("NuevoEnfermedad") = False Then
                    int_CodigoAccion = 201
                    editarEnfermedad()
                ElseIf ViewState("NuevoEnfermedad") = True Then
                    int_CodigoAccion = 200
                    agregarEnfermedad()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                modal_xxx.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub
    Protected Sub popup_btnCancelar_Enfermedad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalEnfermedad()
        limpiarCamposEnfermedad()
    End Sub
    Protected Sub btnAgregarEnfermedad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregarEnfermedad.Click
        Dim str_miDDL As String = ddlEnfermedad.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = modal_xxx.UniqueID.ToString

        ucIngresarEnfermedad.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarEnfermedad.mostrarModal()
    End Sub
    Protected Sub btnAgregarVacuna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregarVacuna.Click
        Dim str_miDDL As String = ddlTipoVacuna.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = pnModalVacuna.UniqueID.ToString

        ucIngresarVacuna.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarVacuna.mostrarModal()
    End Sub
    Protected Sub btnAgregarDosis_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregarDosis.Click
        Dim str_miDDL As String = ddlDosis.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = pnModalVacuna.UniqueID.ToString

        ucIngresarDosis.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarDosis.mostrarModal()
    End Sub
    Protected Sub btnAgregarAlergia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregarAlergia.Click
        Dim str_miDDL As String = ddlAlergia.UniqueID.ToString
        Dim str_miDDLTipo As String = ddlTipoAlergia.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = pnModalAlergia.UniqueID.ToString

        ucIngresarAlergia.setearParametros(str_miDDL, str_miDDLTipo, bool_miModal, str_miModal)
        ucIngresarAlergia.mostrarModal()
    End Sub
    Protected Sub btnAgregarTipoCaracteristica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregarTipoCaracteristica.Click
        Dim str_miDDL As String = ddlCaracteristicaPiel.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = pnModalCaracteristicasPiel.UniqueID.ToString

        ucIngresarTiposCaracteristicasPiel.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarTiposCaracteristicasPiel.mostrarModal()
    End Sub
    Protected Sub btnHospitalizacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHospitalizacion.Click
        Dim str_miDDL As String = ddlHospitalizacion.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = pnModalHospitalizacion.UniqueID.ToString

        ucIngresarHospitalizacion.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarHospitalizacion.mostrarModal()
    End Sub
    Protected Sub btnAgregarTipoAlergia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregarTipoAlergia.Click
        Dim str_miDDL As String = ddlTipoAlergia.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = pnModalAlergia.UniqueID.ToString
        ucIngresarTipoAlergia.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarTipoAlergia.mostrarModal()
    End Sub
    Protected Sub btnAgregarOperaciones_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregarOperaciones.Click
        Dim str_miDDL As String = ddlOperacion.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = pnModalOperacion.UniqueID.ToString
        ucIngresarOperaciones.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarOperaciones.mostrarModal()
    End Sub
#End Region
#Region "Métodos"

    Private Function validarEnfermedad(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlEnfermedad.SelectedValue = 0 Then

            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Enfermedad")
            result = False

        End If

        If tbEdad.Text = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Edad")
            result = False
        End If

        If CInt(tbEdad.Text) > 30 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 40, "Edad")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Cierra el popup Enfermedad
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalEnfermedad()
        modal_xxx.Hide()
        limpiarCamposEnfermedad()
    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Enfermedad al detalle de Enfermedades
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarEnfermedad()

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaEnfermedad") Is Nothing Then
            dt = New DataTable("ListaEnfermedad")
            dt = Datos.agregarColumna(dt, "CodigoRelFichaMedEnEnfermedades", "Integer")
            'dt = Datos.agregarColumna(dt, "CodigoAlumno", "String")
            dt = Datos.agregarColumna(dt, "CodigoEnfermedad", "Integer")
            dt = Datos.agregarColumna(dt, "Enfermedad", "String")
            dt = Datos.agregarColumna(dt, "Edad", "Integer")
        Else
            dt = ViewState("ListaEnfermedad")
        End If

        If dt.Rows.Count > 0 Then
            For Each auxdr As DataRow In dt.Rows
                If auxdr.Item("CodigoEnfermedad").ToString = ddlEnfermedad.SelectedValue And auxdr.Item("Edad") = Val(tbEdad.Text) Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    'ddlEnfermedad.SelectedValue = 0
                    'tbEdad.Text = 0
                    modal_xxx.Show()
                    Exit Sub
                End If
                id_codigo_fila = auxdr.Item("CodigoRelFichaMedEnEnfermedades").ToString()
            Next
        End If

        If boolIncremento = False Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("CodigoRelFichaMedEnEnfermedades") = id_codigo_fila + 1
            dr.Item("CodigoEnfermedad") = ddlEnfermedad.SelectedValue
            dr.Item("Enfermedad") = ddlEnfermedad.SelectedItem.ToString
            dr.Item("Edad") = Val(tbEdad.Text)
            dt.Rows.Add(dr)
        End If

        ViewState("ListaEnfermedad") = dt
        gvDetalleEnfermedad.DataSource = dt
        gvDetalleEnfermedad.DataBind()
        limpiarCamposEnfermedad()
        upEnfermedad.Update()
    End Sub

    ''' <summary>
    ''' Edita 1 Registro Enfermedad del detalle de Enfermedades
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarEnfermedad()

        Dim int_CodigoOriginal As Integer = hidencodigoEnfermedad.Value
        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        dt = ViewState("ListaEnfermedad")

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoEnfermedad").ToString = ddlEnfermedad.SelectedValue And auxdr.Item("Edad").ToString = Val(tbEdad.Text) Then
                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                'ddlEnfermedad.SelectedValue = 0
                'tbEdad.Text = 0
                modal_xxx.Show()
                Exit Sub
            End If
        Next

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelFichaMedEnEnfermedades").ToString = int_CodigoOriginal Then
                auxdr.Item("CodigoRelFichaMedEnEnfermedades") = int_CodigoOriginal
                auxdr.Item("CodigoEnfermedad") = ddlEnfermedad.SelectedValue
                auxdr.Item("Enfermedad") = ddlEnfermedad.SelectedItem.ToString
                auxdr.Item("Edad") = Val(tbEdad.Text)
            End If
        Next

        ViewState("ListaEnfermedad") = dt
        gvDetalleEnfermedad.DataSource = dt
        gvDetalleEnfermedad.DataBind()
        upEnfermedad.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro Enfermedad del detalle de Enfermedades
    ''' </summary>
    ''' <param name="int_CodigoEnfermedad">Codigo del idioma que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarEnfermedad(ByVal int_CodigoEnfermedad As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaEnfermedad")
        For Each auxdr As DataRow In dt.Rows
            If Val(auxdr.Item("CodigoRelFichaMedEnEnfermedades").ToString) = int_CodigoEnfermedad Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaEnfermedad") = dt
        gvDetalleEnfermedad.DataSource = dt
        gvDetalleEnfermedad.DataBind()
        upEnfermedad.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle Enfermedad al popup Enfermedad
    ''' </summary>
    ''' <param name="int_Codigo">Codigo Detalle Relacion Enfermedad</param>
    ''' <param name="int_CodigoEnfermedad">Código de la Enfermedad</param>
    ''' <param name="int_EdadEnfermedad">Edad en que tuvo la enfermedad</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarEnfermedad(ByVal int_Codigo As Integer, ByVal int_CodigoEnfermedad As Integer, ByVal int_EdadEnfermedad As Integer)

        ddlEnfermedad.SelectedValue = int_CodigoEnfermedad
        hidencodigoEnfermedad.Value = int_Codigo
        tbEdad.Text = int_EdadEnfermedad
        modal_xxx.Show()

    End Sub

    Private Sub limpiarCamposEnfermedad()
        ddlEnfermedad.SelectedValue = 0
        tbEdad.Text = 0
    End Sub

#End Region
#Region "Gridview"
    Protected Sub gvDetalleEnfermedad_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim CodigoRelFichaMedEnEnfermedades As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoEnfermedad") = False
                    activarEditarEnfermedad(CodigoRelFichaMedEnEnfermedades, CType(row.FindControl("lblCodigoEnfermedad"), Label).Text, CType(row.FindControl("lblEdadEnfermedad_grilla"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarEnfermedad(CodigoRelFichaMedEnEnfermedades)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleEnfermedad_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento de Detalle Vacuna"

#Region "Eventos"
    Protected Sub btn_Add_Vacuna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoVacuna") = True
        pnModalVacuna.Show()

    End Sub

    Protected Sub popup_btnAgregar_Vacuna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            If validarVacuna(usp_mensaje) Then
                If ViewState("NuevoVacuna") = False Then
                    int_CodigoAccion = 201
                    editarVacuna()
                ElseIf ViewState("NuevoVacuna") = True Then
                    int_CodigoAccion = 200
                    agregarVacuna()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                pnModalVacuna.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub popup_btnCancelar_Vacuna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalVacuna()
        limpiarCamposVacuna()
    End Sub
#End Region
#Region "Métodos"
    Private Function validarVacuna(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If CDate(tbFechaVacunacion.Text) > CDate(Today.ToShortDateString) Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 8, "Fecha")
            result = False
        End If

        If ddlTipoVacuna.SelectedValue = 0 Then

            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo Vacuna")
            result = False

        End If

        If ddlDosis.SelectedValue = 0 Then

            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Dosis")
            result = False

        End If

        If tbEdadVacuna.Text = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Edad Vacuna")
            result = False
        End If

        If CInt(tbEdadVacuna.Text) > 30 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 40, "Edad Vacuna")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub limpiarCamposVacuna()
        ddlTipoVacuna.SelectedValue = 0
        tbFechaVacunacion.Text = Now.Date
        ddlDosis.SelectedValue = 0
        tbEdadVacuna.Text = 0
    End Sub

    ''' <summary>
    ''' Cierra el popup Vacuna
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalVacuna()

        pnModalVacuna.Hide()
        limpiarCamposVacuna()
    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Vacuna al detalle de Vacunas
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarVacuna()

        Dim tbfecha As Date
        Dim tipoVacuna As Integer
        Dim dosis As Integer
        Dim intEdadVacuna As Integer

        tbfecha = tbFechaVacunacion.Text
        tipoVacuna = ddlTipoVacuna.SelectedValue
        dosis = ddlDosis.SelectedValue
        intEdadVacuna = tbEdadVacuna.Text

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaVacuna") Is Nothing Then

            dt = New DataTable("ListaVacuna")

            dt = Datos.agregarColumna(dt, "CodigoRelVacunasFichaMed", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoVacuna", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoDosis", "Integer")
            dt = Datos.agregarColumna(dt, "FechaVacunacion", "date")
            dt = Datos.agregarColumna(dt, "Vacuna", "String")
            dt = Datos.agregarColumna(dt, "Dosis", "String")
            dt = Datos.agregarColumna(dt, "Edad", "String")

        Else

            dt = ViewState("ListaVacuna")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoVacuna").ToString = ddlTipoVacuna.SelectedValue And IIf(auxdr.Item("Edad").ToString = "", 0, auxdr.Item("Edad")) = Val(tbEdadVacuna.Text) Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    'ddlTipoVacuna.SelectedValue = 0
                    'ddlDosis.SelectedValue = 0
                    'tbFechaVacunacion.Text = Now.Date
                    pnModalVacuna.Show()
                    Exit Sub

                End If
                id_codigo_fila = auxdr.Item("CodigoRelVacunasFichaMed").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelVacunasFichaMed") = id_codigo_fila + 1
            dr.Item("CodigoVacuna") = ddlTipoVacuna.SelectedValue
            dr.Item("Vacuna") = ddlTipoVacuna.SelectedItem.ToString
            dr.Item("CodigoDosis") = ddlDosis.SelectedValue
            dr.Item("Dosis") = ddlDosis.SelectedItem.ToString
            dr.Item("FechaVacunacion") = tbFechaVacunacion.Text
            dr.Item("Edad") = tbEdadVacuna.Text

            dt.Rows.Add(dr)

        End If

        ViewState("ListaVacuna") = dt

        gvDetalleVacuna.DataSource = dt
        gvDetalleVacuna.DataBind()

        limpiarCamposVacuna()
        upVacuna.Update()

    End Sub

    ''' <summary>
    ''' Edita 1 Registro Vacuna del detalle de Vacunas
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarVacuna()
        Dim tbfecha As Date
        Dim tipoVacuna As Integer
        Dim dosis As Integer
        Dim intEdadVacuna As Integer

        tbfecha = tbFechaVacunacion.Text
        tipoVacuna = ddlTipoVacuna.SelectedValue
        dosis = ddlDosis.SelectedValue
        intEdadVacuna = tbEdadVacuna.Text

        Dim int_CodigoOriginal As Integer = hidencodigoVacuna.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaVacuna")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoVacuna").ToString = int_CodigoOriginal Then

                'If auxdr.Item("FechaVacunacion").ToString = tbFechaVacunacion.Text And auxdr.Item("CodigoVacuna").ToString = ddlTipoVacuna.SelectedValue And auxdr.Item("CodigoDosis").ToString = ddlDosis.SelectedValue Then
            ElseIf auxdr.Item("CodigoVacuna").ToString = ddlTipoVacuna.SelectedValue Then
                If auxdr.Item("CodigoDosis").ToString = ddlDosis.SelectedValue Then
                    If auxdr.Item("Edad") = Val(tbEdadVacuna.Text) Then
                        MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                        'ddlTipoVacuna.SelectedValue = 0
                        'ddlDosis.SelectedValue = 0
                        'tbFechaVacunacion.Text = Now.Date
                        pnModalVacuna.Show()
                        Exit Sub
                    End If
                End If
            End If
        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelVacunasFichaMed").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelVacunasFichaMed") = int_CodigoOriginal
                auxdr.Item("CodigoVacuna") = ddlTipoVacuna.SelectedValue
                auxdr.Item("Vacuna") = ddlTipoVacuna.SelectedItem.ToString
                auxdr.Item("CodigoDosis") = ddlDosis.SelectedValue
                auxdr.Item("Dosis") = ddlDosis.SelectedItem.ToString
                auxdr.Item("FechaVacunacion") = tbFechaVacunacion.Text
                auxdr.Item("Edad") = tbEdadVacuna.Text
            End If

        Next

        ViewState("ListaVacuna") = dt

        gvDetalleVacuna.DataSource = dt
        gvDetalleVacuna.DataBind()

        limpiarCamposVacuna()
        upVacuna.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro Vacuna del detalle de Vacunas
    ''' </summary>
    ''' <param name="int_CodigoVacuna">Codigo de la Vacuna que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarVacuna(ByVal int_CodigoVacuna As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaVacuna")
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelVacunasFichaMed").ToString = int_CodigoVacuna Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaVacuna") = dt
        gvDetalleVacuna.DataSource = dt
        gvDetalleVacuna.DataBind()
        upVacuna.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle de Vacunas al popup Vacuna
    ''' </summary>
    ''' <param name="int_Codigo">Codigo Detalle Relacion Vacuna</param>
    ''' <param name="int_CodigoVacuna">Código de la Vacuna</param>
    ''' <param name="int_CodigoDosis">Código de la Dosis</param>
    ''' <param name="dt_fecha">Fecha en que se le aplico la Vacuna</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarVacuna(ByVal int_Codigo As Integer, ByVal int_CodigoVacuna As Integer, ByVal int_CodigoDosis As Integer, ByVal int_EdadVacuna As Integer, ByVal dt_fecha As Date)

        ddlTipoVacuna.SelectedValue = int_CodigoVacuna
        hidencodigoVacuna.Value = int_Codigo
        tbFechaVacunacion.Text = dt_fecha
        ddlDosis.SelectedValue = int_CodigoDosis
        tbEdadVacuna.Text = int_EdadVacuna
        pnModalVacuna.Show()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleVacuna_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim CodigoRelVacunasFichaMed As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoVacuna") = False
                    activarEditarVacuna(CodigoRelVacunasFichaMed, CType(row.FindControl("lblCodigoVacuna"), Label).Text, CType(row.FindControl("lblCodigoDosis"), Label).Text, CType(row.FindControl("lblEdadVacuna"), Label).Text, CType(row.FindControl("lblFechaVacunacion"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarVacuna(CodigoRelVacunasFichaMed)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleVacuna_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento de Detalle Alergias"

#Region "Eventos"
    Protected Sub btn_Add_Alergia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoAlergia") = True
        pnModalAlergia.Show()
        'cargarComboTipAlergia()
    End Sub

    Protected Sub popup_btnAgregar_Alergia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            If validarAlergia(usp_mensaje) Then
                If ViewState("NuevoAlergia") = False Then
                    int_CodigoAccion = 201
                    editarAlergia()
                ElseIf ViewState("NuevoAlergia") = True Then
                    int_CodigoAccion = 200
                    agregarAlergia()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                pnModalAlergia.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_Alergia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        limpiarCamposAlergia()
        pnModalAlergia.Hide()
    End Sub
#End Region
#Region "Métodos"


    Private Function validarAlergia(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If CDate(tbFechaRegistroAlergia.Text) > CDate(Today.ToShortDateString) Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 8, "Fecha")
            result = False
        End If

        If ddlAlergia.SelectedValue = 0 Then

            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Alergia")
            result = False

        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub limpiarCamposAlergia()

        ddlTipoAlergia.SelectedValue = 0
        cargarComboAlergia()
        ddlAlergia.SelectedValue = 0
        tbFechaRegistroAlergia.Text = Now.Date
    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Alergía al detalle de Alergias
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarAlergia()

        Dim alergia As Integer

        alergia = ddlAlergia.SelectedValue

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaAlergia") Is Nothing Then

            dt = New DataTable("ListaAlergia")

            dt = Datos.agregarColumna(dt, "CodigoRelFichaMedAlergias", "String")
            dt = Datos.agregarColumna(dt, "CodigoAlergia", "String")
            dt = Datos.agregarColumna(dt, "CodigoTipoAlergia", "Integer")
            dt = Datos.agregarColumna(dt, "FechaRegistro", "date")
            dt = Datos.agregarColumna(dt, "Alergia", "String")
            dt = Datos.agregarColumna(dt, "TipoAlergia", "String")

        Else

            dt = ViewState("ListaAlergia")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoAlergia").ToString = ddlAlergia.SelectedValue Then

                    MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                    ddlAlergia.SelectedValue = auxdr.Item("CodigoAlergia").ToString
                    tbFechaRegistroAlergia.Text = Now.Date
                    pnModalAlergia.Show()
                    Exit Sub


                End If
                id_codigo_fila = auxdr.Item("CodigoRelFichaMedAlergias").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim obj_BL_Alergia As New bl_Alergias
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

            Dim dt_descriTipoAlergia As DataSet = obj_BL_Alergia.FUN_GET_DescripcionAlergia(ddlAlergia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelFichaMedAlergias") = id_codigo_fila + 1
            dr.Item("CodigoAlergia") = ddlAlergia.SelectedValue
            dr.Item("Alergia") = ddlAlergia.SelectedItem.ToString
            dr.Item("CodigoTipoAlergia") = dt_descriTipoAlergia.Tables(0).Rows(0).Item("TA_CodigoTipoAlergia")
            dr.Item("TipoAlergia") = dt_descriTipoAlergia.Tables(0).Rows(0).Item("TA_Descripcion")
            dr.Item("FechaRegistro") = tbFechaRegistroAlergia.Text

            dt.Rows.Add(dr)

        End If

        ViewState("ListaAlergia") = dt

        gvDetalleAlergia.DataSource = dt
        gvDetalleAlergia.DataBind()

        limpiarCamposAlergia()

        upAlergia.Update()

    End Sub

    ''' <summary>
    ''' Edita 1 Registro Alergía del detalle de Alergias
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarAlergia()

        Dim alergia As Integer

        alergia = ddlAlergia.SelectedValue


        Dim int_CodigoOriginal As Integer = hidencodigoAlergia.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        Dim obj_BL_Alergia As New bl_Alergias
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim dt_descriTipoAlergia As DataSet = obj_BL_Alergia.FUN_GET_DescripcionAlergia(ddlAlergia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

        dt = ViewState("ListaAlergia")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoAlergia").ToString = ddlAlergia.SelectedValue Then

                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                ddlAlergia.SelectedValue = auxdr.Item("CodigoAlergia").ToString
                tbFechaRegistroAlergia.Text = auxdr.Item("FechaRegistro").ToString
                pnModalAlergia.Show()
                Exit Sub

            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelFichaMedAlergias").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelFichaMedAlergias") = int_CodigoOriginal
                auxdr.Item("CodigoAlergia") = ddlAlergia.SelectedValue
                auxdr.Item("Alergia") = ddlAlergia.SelectedItem.ToString
                auxdr.Item("CodigoTipoAlergia") = dt_descriTipoAlergia.Tables(0).Rows(0).Item("TA_CodigoTipoAlergia")
                auxdr.Item("TipoAlergia") = dt_descriTipoAlergia.Tables(0).Rows(0).Item("TA_Descripcion")
                auxdr.Item("FechaRegistro") = tbFechaRegistroAlergia.Text

            End If

        Next

        ViewState("ListaAlergia") = dt

        gvDetalleAlergia.DataSource = dt
        gvDetalleAlergia.DataBind()

        limpiarCamposAlergia()
        upAlergia.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro Alergia del detalle de Alergias
    ''' </summary>
    ''' <param name="int_CodigoAlergia">Codigo de la Alergia que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarAlergia(ByVal int_CodigoAlergia As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaAlergia")
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelFichaMedAlergias").ToString = int_CodigoAlergia Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaAlergia") = dt
        gvDetalleAlergia.DataSource = dt
        gvDetalleAlergia.DataBind()
        upAlergia.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle de Alergia al popup Alergia
    ''' </summary>
    ''' <param name="int_Codigo">Codigo Detalle Relacion Alergia</param>
    ''' <param name="int_CodigoAlergia">Código de la Alergia</param>
    ''' <param name="dt_fecha">Fecha en que se le aplico la Vacuna</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarAlergia(ByVal int_Codigo As Integer, ByVal int_CodigoTipoAlergia As Integer, ByVal int_CodigoAlergia As Integer, ByVal dt_fecha As Date)
        ddlTipoAlergia.SelectedValue = int_CodigoTipoAlergia
        cargarComboAlergia()
        'limpiarCombos(ddlAlergia)
        ddlAlergia.SelectedValue = int_CodigoAlergia
        hidencodigoAlergia.Value = int_Codigo
        tbFechaRegistroAlergia.Text = dt_fecha
        pnModalAlergia.Show()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleAlergia_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim CodigoRelFichaMedAlergias As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoAlergia") = False
                    activarEditarAlergia(CodigoRelFichaMedAlergias, CType(row.FindControl("lblCodigoTipoAlergia"), Label).Text, CType(row.FindControl("lblCodigoAlergia"), Label).Text, CType(row.FindControl("lblFechaRegistroAlergia"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarAlergia(CodigoRelFichaMedAlergias)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleAlergia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento de Detalle Caracteristicas de la piel"

#Region "Eventos"
    Protected Sub btn_Add_CaracteristicasPiel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoCaracteristicaPiel") = True
        pnModalCaracteristicasPiel.Show()
    End Sub

    Protected Sub popup_btnAgregar_CaracteristicaPiel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            If validarCaracteristicaPiel(usp_mensaje) Then
                If ViewState("NuevoCaracteristicaPiel") = False Then
                    int_CodigoAccion = 201
                    editarCaracteristicasPiel()
                ElseIf ViewState("NuevoCaracteristicaPiel") = True Then
                    int_CodigoAccion = 200
                    agregarCaracteristicasPiel()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                pnModalCaracteristicasPiel.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_CaracteristicaPiel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalCaracteristicaPiel()
        limpiarCamposCaracteristicaPiel()
    End Sub
#End Region
#Region "Métodos"
    Private Function validarCaracteristicaPiel(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If CDate(tbFechaRegistroCaracteristicasPiel.Text) > CDate(Today.ToShortDateString) Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 8, "Fecha")
            result = False
        End If

        If ddlCaracteristicaPiel.SelectedValue = 0 Then

            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Caracteristicas de la piel")
            result = False

        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub limpiarCamposCaracteristicaPiel()
        ddlCaracteristicaPiel.SelectedValue = 0
        tbFechaRegistroCaracteristicasPiel.Text = Now.Date
    End Sub
    ''' <summary>
    ''' Cierra el popup Caracteristicas de Piel
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalCaracteristicaPiel()
        pnModalCaracteristicasPiel.Hide()
        limpiarCamposCaracteristicaPiel()
    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Caracteristica de Piel al detalle de Caracteristicas de Piel
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarCaracteristicasPiel()

        Dim tbfecha As Date
        Dim caracteristicaPiel As Integer

        tbfecha = tbFechaRegistroCaracteristicasPiel.Text
        caracteristicaPiel = ddlCaracteristicaPiel.SelectedValue

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaCaracteristicasPiel") Is Nothing Then

            dt = New DataTable("ListaCaracteristicasPiel")

            dt = Datos.agregarColumna(dt, "CodigoRelFichaMedCaractPiel", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoCaracteristicapiel", "Integer")
            dt = Datos.agregarColumna(dt, "FechaRegistro", "date")
            dt = Datos.agregarColumna(dt, "CaracteristicaPiel", "String")

        Else

            dt = ViewState("ListaCaracteristicasPiel")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoCaracteristicapiel").ToString = ddlCaracteristicaPiel.SelectedValue Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    ddlCaracteristicaPiel.SelectedValue = 0
                    tbFechaRegistroCaracteristicasPiel.Text = Now.Date
                    pnModalCaracteristicasPiel.Show()
                    Exit Sub
                End If
                id_codigo_fila = auxdr.Item("CodigoRelFichaMedCaractPiel").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelFichaMedCaractPiel") = id_codigo_fila + 1
            dr.Item("CodigoCaracteristicapiel") = ddlCaracteristicaPiel.SelectedValue
            dr.Item("Caracteristicapiel") = ddlCaracteristicaPiel.SelectedItem.ToString
            dr.Item("FechaRegistro") = tbFechaRegistroCaracteristicasPiel.Text

            dt.Rows.Add(dr)

        End If

        ViewState("ListaCaracteristicasPiel") = dt

        gvDetalleCaracteristicaPiel.DataSource = dt
        gvDetalleCaracteristicaPiel.DataBind()

        limpiarCamposCaracteristicaPiel()

        upCaracteristicaPiel.Update()

    End Sub

    ''' <summary>
    ''' Edita 1 Registro Caracteristica de Piel  del detalle de Caracteristicas de Piel 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     22/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarCaracteristicasPiel()

        Dim tbfecha As Date
        Dim caracteristicaPiel As Integer

        tbfecha = tbFechaRegistroCaracteristicasPiel.Text
        caracteristicaPiel = ddlCaracteristicaPiel.SelectedValue

        'If CDate(tbFechaRegistroCaracteristicasPiel.Text) > CDate(Today.ToShortDateString) Then
        '    MostrarSexyAlertBox("incorrecta. La fecha de registro no puede ser mayor a la fecha actual.", "Alert")
        '    tbFechaRegistroCaracteristicasPiel.Text = tbfecha
        '    ddlCaracteristicaPiel.SelectedValue = caracteristicaPiel
        '    pnModalCaracteristicasPiel.Show()
        '    Exit Sub
        'End If

        'If ddlCaracteristicaPiel.SelectedValue = 0 Then

        '    MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
        '    'ddlCaracteristicaPiel.SelectedValue = 0
        '    'tbFechaRegistroCaracteristicasPiel.Text = Now.Date
        '    pnModalCaracteristicasPiel.Show()
        '    Exit Sub

        'End If

        Dim int_CodigoOriginal As Integer = hdCodigoCaracteristicasPiel.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaCaracteristicasPiel")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoCaracteristicapiel").ToString = ddlCaracteristicaPiel.SelectedValue Then

                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")

                pnModalCaracteristicasPiel.Show()
                Exit Sub

            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelFichaMedCaractPiel").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelFichaMedCaractPiel") = int_CodigoOriginal
                auxdr.Item("CodigoCaracteristicapiel") = ddlCaracteristicaPiel.SelectedValue
                auxdr.Item("CaracteristicaPiel") = ddlCaracteristicaPiel.SelectedItem.ToString
                auxdr.Item("FechaRegistro") = tbFechaRegistroCaracteristicasPiel.Text

            End If

        Next

        ViewState("ListaCaracteristicasPiel") = dt

        gvDetalleCaracteristicaPiel.DataSource = dt
        gvDetalleCaracteristicaPiel.DataBind()

        limpiarCamposCaracteristicaPiel()
        upCaracteristicaPiel.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro Caracteristica de Piel del detalle de Caracteristicas de Piel
    ''' </summary>
    ''' <param name="int_CodigoCaracteristicasPiel">Codigo de la Caracteristica de Piel que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarCaracteristicaPiel(ByVal int_CodigoCaracteristicasPiel As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaCaracteristicasPiel")
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelFichaMedCaractPiel").ToString = int_CodigoCaracteristicasPiel Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaCaracteristicasPiel") = dt
        gvDetalleCaracteristicaPiel.DataSource = dt
        gvDetalleCaracteristicaPiel.DataBind()
        upCaracteristicaPiel.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle de Alergia al popup Alergia
    ''' </summary>
    ''' <param name="int_Codigo">Codigo Detalle Relacion Caracteristica de Piel</param>
    ''' <param name="int_CodigoCaracteristicasPiel">Código de la Caracteristica de Piel</param>
    ''' <param name="dt_fecha">Fecha de Registro</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarCaracteristicaPiel(ByVal int_codigo As Integer, ByVal int_CodigoCaracteristicasPiel As Integer, ByVal dt_fecha As Date)
        ddlCaracteristicaPiel.SelectedValue = int_CodigoCaracteristicasPiel
        hdCodigoCaracteristicasPiel.Value = int_codigo
        tbFechaRegistroCaracteristicasPiel.Text = dt_fecha
        pnModalCaracteristicasPiel.Show()
    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleCaracteristicaPiel_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then

                Dim CodigoRelFichaMedCaractPiel As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoCaracteristicaPiel") = False
                    activarEditarCaracteristicaPiel(CodigoRelFichaMedCaractPiel, CType(row.FindControl("lblCodigoCaracteristicapiel"), Label).Text, CType(row.FindControl("lblFechaRegistroCaracteristicapiel"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarCaracteristicaPiel(CodigoRelFichaMedCaractPiel)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleCaracteristicaPiel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento de Detalle Medicamento"

#Region "Eventos"
    Protected Sub btn_Add_Medicamentos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoMedicamentos") = True
        pnModalMedicamentos.Show()
    End Sub

    Protected Sub popup_btnAgregar_Medicamentos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            Dim usp_mensaje As String = ""
            If validarMedicamentos(usp_mensaje) Then
                If ViewState("NuevoMedicamentos") = False Then
                    int_CodigoAccion = 201
                    editarMedicamentos()
                ElseIf ViewState("NuevoMedicamentos") = True Then
                    int_CodigoAccion = 200
                    agregarMedicamento()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                pnModalMedicamentos.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_Medicamentos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalMedicamentos()
        limpiarCamposMedicamentos()
    End Sub
#End Region
#Region "Métodos"

    Private Function validarMedicamentos(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlMedicamento.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Medicamento")
            result = False
        End If

        If ddlPresentacion.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Presentación")
            result = False
        End If

        If tbDosisMedi.Text.Trim.Length > 0 Then
            If Validacion.ValidarCamposIngreso(tbDosisMedi) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Dosis")
                result = False
            End If
        End If

        If tbObservacionMedi.Text.Trim.Length > 0 Then
            If Validacion.ValidarCamposIngreso(tbObservacionMedi) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Observaciones")
                result = False
            End If
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub limpiarCamposMedicamentos()
        ddlMedicamento.SelectedValue = 0
        ddlPresentacion.SelectedValue = 0
        tbCantidadPres.Text = ""
        tbDosisMedi.Text = ""
        tbObservacionMedi.Text = ""
    End Sub

    ''' <summary>
    ''' Cierra el popup Medicamento
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalMedicamentos()

        pnModalMedicamentos.Hide()
        limpiarCamposMedicamentos()
    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Medicamento al detalle de Medicamentos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     21/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarMedicamento()

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaMedicamentos") Is Nothing Then

            dt = New DataTable("ListaMedicamentos")

            dt = Datos.agregarColumna(dt, "CodigoRelFichaAtenMedicamentos", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoMedicamento", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoPresentacion", "Integer")
            dt = Datos.agregarColumna(dt, "Medicamento", "String")
            dt = Datos.agregarColumna(dt, "Presentacion", "String")
            dt = Datos.agregarColumna(dt, "CantidadPresentacion", "String")
            dt = Datos.agregarColumna(dt, "PresentCant", "String")
            dt = Datos.agregarColumna(dt, "DosisMedicamento", "String")
            dt = Datos.agregarColumna(dt, "Observaciones", "String")

        Else

            dt = ViewState("ListaMedicamentos")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoMedicamento").ToString = ddlMedicamento.SelectedValue And auxdr.Item("CodigoPresentacion").ToString = ddlPresentacion.SelectedValue Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    'ddlMedicamento.SelectedValue = 0
                    'ddlFrecuenciaUso.SelectedValue = 0
                    'tbFechaMedicamentos.Text = Now.Date
                    pnModalMedicamentos.Show()
                    Exit Sub

                End If
                id_codigo_fila = auxdr.Item("CodigoRelFichaAtenMedicamentos").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelFichaAtenMedicamentos") = id_codigo_fila + 1
            dr.Item("CodigoMedicamento") = ddlMedicamento.SelectedValue
            dr.Item("Medicamento") = ddlMedicamento.SelectedItem.ToString
            dr.Item("CodigoPresentacion") = ddlPresentacion.SelectedValue
            dr.Item("Presentacion") = ddlPresentacion.SelectedItem.ToString
            dr.Item("CantidadPresentacion") = tbCantidadPres.Text
            dr.Item("PresentCant") = ddlPresentacion.SelectedItem.ToString & " / " & tbCantidadPres.Text
            dr.Item("DosisMedicamento") = tbDosisMedi.Text
            dr.Item("Observaciones") = tbObservacionMedi.Text

            dt.Rows.Add(dr)

        End If

        ViewState("ListaMedicamentos") = dt

        gvDetalleMedicamento.DataSource = dt
        gvDetalleMedicamento.DataBind()

        limpiarCamposMedicamentos()

        upMedicamentos.Update()

    End Sub

    ''' <summary>
    ''' Edita 1 Registro Medicamento del detalle de Medicamentos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarMedicamentos()



        Dim int_CodigoOriginal As Integer = hidencodigoMedicamento.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaMedicamentos")

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelFichaAtenMedicamentos").ToString = int_CodigoOriginal Then
            Else
                If auxdr.Item("CodigoMedicamento").ToString = ddlMedicamento.SelectedValue And auxdr.Item("CodigoPresentacion").ToString = ddlPresentacion.SelectedValue Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    'ddlMedicamento.SelectedValue = 0
                    'ddlFrecuenciaUso.SelectedValue = 0
                    'tbFechaMedicamentos.Text = Now.Date
                    pnModalMedicamentos.Show()
                    Exit Sub

                End If
            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelFichaAtenMedicamentos").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelFichaAtenMedicamentos") = int_CodigoOriginal
                auxdr.Item("CodigoMedicamento") = ddlMedicamento.SelectedValue
                auxdr.Item("Medicamento") = ddlMedicamento.SelectedItem.ToString
                auxdr.Item("CodigoPresentacion") = ddlPresentacion.SelectedValue
                auxdr.Item("Presentacion") = ddlPresentacion.SelectedItem.ToString
                auxdr.Item("CantidadPresentacion") = tbCantidadPres.Text
                auxdr.Item("PresentCant") = ddlPresentacion.SelectedItem.ToString & " / " & tbCantidadPres.Text
                auxdr.Item("DosisMedicamento") = tbDosisMedi.Text
                auxdr.Item("Observaciones") = tbObservacionMedi.Text

            End If

        Next

        ViewState("ListaMedicamentos") = dt

        gvDetalleMedicamento.DataSource = dt
        gvDetalleMedicamento.DataBind()

        limpiarCamposMedicamentos()

        upMedicamentos.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro Medicamento del detalle de Medicamentos
    ''' </summary>
    ''' <param name="int_CodigoMedicamento">Codigo de la Medicamento que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarMedicamento(ByVal int_CodigoMedicamento As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaMedicamentos")

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelFichaAtenMedicamentos").ToString = int_CodigoMedicamento Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaMedicamentos") = dt
        gvDetalleMedicamento.DataSource = dt
        gvDetalleMedicamento.DataBind()
        upMedicamentos.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle de Medicamento al popup Medicamento
    ''' </summary>
    ''' <param name="int_Codigo">Codigo Detalle Relacion Alergia</param>
    ''' <param name="int_CodigoMedicamento">Código de Medicamento</param>
    ''' <param name="int_frecuenciaUso">Codigo de Frecuencia de Uso</param>
    ''' <param name="dt_fecha">Fecha de registro</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarMedicamento(ByVal int_Codigo As Integer, ByVal int_CodigoMedicamento As Integer, ByVal int_Presentacion As Integer, ByVal str_CantidadPres As String, ByVal str_DosisMedi As String, ByVal str_ObservacionMedi As String)

        ddlMedicamento.SelectedValue = int_CodigoMedicamento
        hidencodigoMedicamento.Value = int_Codigo
        ddlPresentacion.SelectedValue = int_Presentacion
        tbCantidadPres.Text = str_CantidadPres
        tbDosisMedi.Text = str_DosisMedi
        tbObservacionMedi.Text = str_ObservacionMedi
        pnModalMedicamentos.Show()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleMedicamento_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim CodigoRelFichaAtenMedicamentos As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoMedicamentos") = False
                    activarEditarMedicamento(CodigoRelFichaAtenMedicamentos, CType(row.FindControl("lblCodigoMedicamento"), Label).Text, CType(row.FindControl("lblCodigoPresentacion"), Label).Text, CType(row.FindControl("lblCantidadPresentacion"), Label).Text, CType(row.FindControl("lblDosisMedicamento"), Label).Text, CType(row.FindControl("lblObservaciones"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarMedicamento(CodigoRelFichaAtenMedicamentos)
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleMedicamento_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento de Detalle Hospitalizacion"

#Region "Eventos"
    Protected Sub btn_Add_Hospitalizacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoHospitalizacion") = True
        pnModalHospitalizacion.Show()
    End Sub

    Protected Sub popup_btnAgregar_Hospitalizacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            If validarHospitalizacion(usp_mensaje) Then
                If ViewState("NuevoHospitalizacion") = False Then
                    int_CodigoAccion = 201
                    editarHospitalizacion()
                ElseIf ViewState("NuevoHospitalizacion") = True Then
                    int_CodigoAccion = 200
                    agregarHospitalizacion()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                pnModalHospitalizacion.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_Hospitalizacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalHospitalizacion()
    End Sub
#End Region
#Region "Métodos"

    Private Function validarHospitalizacion(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If IsDate(tbFechaHospitalizacion.Text) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha ")
            result = False
        Else
            If CDate(tbFechaHospitalizacion.Text) > CDate(Today.ToShortDateString) Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 8, "Fecha ")
                result = False
            End If
        End If

        If ddlHospitalizacion.SelectedValue = 0 Then

            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Hospitalización")
            result = False

        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Cierra el popup Hospitalizacion
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalHospitalizacion()

        pnModalHospitalizacion.Hide()
        ddlHospitalizacion.SelectedValue = 0
        tbFechaHospitalizacion.Text = Now.Date

    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Hospitalizacion al detalle de Hospitalizaciones
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarHospitalizacion()

        Dim hospitalizacion As Integer
        Dim fecha As Date

        hospitalizacion = ddlHospitalizacion.SelectedValue
        fecha = tbFechaHospitalizacion.Text

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaHospitalizacion") Is Nothing Then

            dt = New DataTable("ListaHospitalizacion")

            dt = Datos.agregarColumna(dt, "CodigoRelFichaMedMotivoHosp", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoMotivoHospitalizacion", "Integer")
            dt = Datos.agregarColumna(dt, "FechaHospitalizacion", "date")
            dt = Datos.agregarColumna(dt, "Hospitalizacion", "String")

        Else

            dt = ViewState("ListaHospitalizacion")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoMotivoHospitalizacion").ToString = ddlHospitalizacion.SelectedValue And auxdr.Item("FechaHospitalizacion").ToString = tbFechaHospitalizacion.Text Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    ddlHospitalizacion.SelectedValue = 0
                    tbFechaHospitalizacion.Text = Now.Date
                    pnModalHospitalizacion.Show()
                    Exit Sub

                End If
                id_codigo_fila = auxdr.Item("CodigoRelFichaMedMotivoHosp").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("CodigoRelFichaMedMotivoHosp") = id_codigo_fila + 1
            dr.Item("CodigoMotivoHospitalizacion") = ddlHospitalizacion.SelectedValue
            dr.Item("Hospitalizacion") = ddlHospitalizacion.SelectedItem.ToString
            dr.Item("FechaHospitalizacion") = tbFechaHospitalizacion.Text

            dt.Rows.Add(dr)

        End If

        ViewState("ListaHospitalizacion") = dt

        gvDetalleHospitalizacion.DataSource = dt
        gvDetalleHospitalizacion.DataBind()

        ddlHospitalizacion.SelectedValue = 0
        tbFechaHospitalizacion.Text = Now.Date

        upHospitalizacion.Update()

    End Sub

    ''' <summary>
    ''' Edita 1 Registro Hospitalizacion del detalle de Hospitalizaciones
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarHospitalizacion()

        Dim hospitalizacion As Integer
        Dim fecha As Date

        hospitalizacion = ddlHospitalizacion.SelectedValue
        fecha = tbFechaHospitalizacion.Text

        Dim int_CodigoOriginal As Integer = hidencodigoHospitalizacion.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaHospitalizacion")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoMotivoHospitalizacion").ToString = ddlHospitalizacion.SelectedValue And auxdr.Item("FechaHospitalizacion").ToString = tbFechaHospitalizacion.Text Then

                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                ddlHospitalizacion.SelectedValue = 0
                tbFechaHospitalizacion.Text = Now.Date
                pnModalHospitalizacion.Show()
                Exit Sub

            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelFichaMedMotivoHosp").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelFichaMedMotivoHosp") = int_CodigoOriginal
                auxdr.Item("CodigoMotivoHospitalizacion") = ddlHospitalizacion.SelectedValue
                auxdr.Item("Hospitalizacion") = ddlHospitalizacion.SelectedItem.ToString
                auxdr.Item("FechaHospitalizacion") = tbFechaHospitalizacion.Text

            End If

        Next

        ViewState("ListaHospitalizacion") = dt

        gvDetalleHospitalizacion.DataSource = dt
        gvDetalleHospitalizacion.DataBind()

        ddlHospitalizacion.SelectedValue = 0
        tbFechaHospitalizacion.Text = Now.Date
        upHospitalizacion.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro Hospitalizacion del detalle de Hospitalizaciones
    ''' </summary>
    ''' <param name="int_CodigoHospitalizacion">Codigo de la Medicamento que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarHospitalizacion(ByVal int_CodigoHospitalizacion As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaHospitalizacion")

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelFichaMedMotivoHosp").ToString = int_CodigoHospitalizacion Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaHospitalizacion") = dt
        gvDetalleHospitalizacion.DataSource = dt
        gvDetalleHospitalizacion.DataBind()
        upHospitalizacion.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle de Hospitalizacion al popup Hospitalizacion
    ''' </summary>
    ''' <param name="int_Codigo">Codigo Detalle Relacion Hospitalizacion</param>
    ''' <param name="int_CodigoHospitalizacion">Código de Hospitalizacion</param>
    ''' <param name="dt_fecha">Fecha de registro</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarHospitalizacion(ByVal int_Codigo As Integer, ByVal int_CodigoHospitalizacion As Integer, ByVal dt_fecha As Date)
        ddlHospitalizacion.SelectedValue = int_CodigoHospitalizacion
        hidencodigoHospitalizacion.Value = int_Codigo
        tbFechaHospitalizacion.Text = dt_fecha
        pnModalHospitalizacion.Show()
    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleHospitalizacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then

                Dim CodigoRelFichaMedMotivoHosp As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoHospitalizacion") = False
                    activarEditarHospitalizacion(CodigoRelFichaMedMotivoHosp, CType(row.FindControl("lblCodigoMotivoHospitalizacion"), Label).Text, CType(row.FindControl("lblFechaHospitalizacion"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarHospitalizacion(CodigoRelFichaMedMotivoHosp)
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleHospitalizacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento de Detalle Operacion"

#Region "Eventos"
    Protected Sub btn_Add_Operacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoOperacion") = True
        pnModalOperacion.Show()
    End Sub

    Protected Sub popup_btnAgregar_Operacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            If validarOperacion(usp_mensaje) Then
                If ViewState("NuevoOperacion") = False Then
                    int_CodigoAccion = 201
                    editarOperacion()
                ElseIf ViewState("NuevoOperacion") = True Then
                    int_CodigoAccion = 200
                    agregarOperacion()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                pnModalOperacion.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_Operacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalOperacion()
    End Sub
#End Region
#Region "Métodos"

    Private Function validarOperacion(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If IsDate(tbFechaOperacion.Text) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha ")
            result = False
        Else
            If CDate(tbFechaOperacion.Text) > CDate(Today.ToShortDateString) Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 8, "Fecha ")
                result = False
            End If
        End If

        If ddlOperacion.SelectedValue = 0 Then

            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Operación")
            result = False

        End If

        str_Mensaje = str_alertas
        Return result

    End Function
    ''' <summary>
    ''' Cierra el popup Operacion
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalOperacion()

        pnModalOperacion.Hide()
        ddlOperacion.SelectedValue = 0
        tbFechaOperacion.Text = Now.Date

    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Operacion al detalle de Operaciones
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarOperacion()

        Dim operacion As Integer
        Dim fecha As Date

        operacion = ddlOperacion.SelectedValue
        fecha = tbFechaOperacion.Text

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaOperacion") Is Nothing Then

            dt = New DataTable("ListaOperacion")

            dt = Datos.agregarColumna(dt, "CodigoRelFichaMedOperaciones", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoTipoOperaciones", "Integer")
            dt = Datos.agregarColumna(dt, "FechaOperacion", "date")
            dt = Datos.agregarColumna(dt, "Operacion", "String")

        Else

            dt = ViewState("ListaOperacion")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoTipoOperaciones").ToString = ddlOperacion.SelectedValue And auxdr.Item("FechaOperacion").ToString = tbFechaHospitalizacion.Text Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    ddlOperacion.SelectedValue = 0
                    tbFechaOperacion.Text = Now.Date
                    pnModalOperacion.Show()
                    Exit Sub


                End If
                id_codigo_fila = auxdr.Item("CodigoRelFichaMedOperaciones").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelFichaMedOperaciones") = id_codigo_fila + 1
            dr.Item("CodigoTipoOperaciones") = ddlOperacion.SelectedValue
            dr.Item("Operacion") = ddlOperacion.SelectedItem.ToString
            dr.Item("FechaOperacion") = tbFechaOperacion.Text

            dt.Rows.Add(dr)

        End If

        ViewState("ListaOperacion") = dt

        gvDetalleOperacion.DataSource = dt
        gvDetalleOperacion.DataBind()

        ddlOperacion.SelectedValue = 0
        tbFechaOperacion.Text = Now.Date

        upOperacion.Update()

    End Sub

    ''' <summary>
    ''' Edita 1 Registro Operacion del detalle de Operaciones
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarOperacion()

        Dim operacion As Integer
        Dim fecha As Date

        operacion = ddlOperacion.SelectedValue
        fecha = tbFechaOperacion.Text

        'If CDate(tbFechaOperacion.Text) > CDate(Today.ToShortDateString) Then
        '    MostrarSexyAlertBox("incorrecta. La fecha de operación no puede ser mayor a la fecha actual.", "Alert")
        '    ddlOperacion.SelectedValue = operacion
        '    tbFechaOperacion.Text = fecha
        '    pnModalOperacion.Show()
        '    Exit Sub
        'End If

        'If ddlOperacion.SelectedValue = 0 Then

        '    MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
        '    'ddlOperacion.SelectedValue = 0
        '    'tbFechaOperacion.Text = Now.Date
        '    pnModalOperacion.Show()
        'End If

        Dim int_CodigoOriginal As Integer = hidencodigoOperacion.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaOperacion")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoTipoOperaciones").ToString = ddlOperacion.SelectedValue And auxdr.Item("FechaOperacion").ToString = tbFechaOperacion.Text Then

                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                ddlOperacion.SelectedValue = 0
                tbFechaOperacion.Text = Now.Date
                pnModalOperacion.Show()
                Exit Sub

            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelFichaMedOperaciones").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelFichaMedOperaciones") = int_CodigoOriginal
                auxdr.Item("CodigoTipoOperaciones") = ddlOperacion.SelectedValue
                auxdr.Item("Operacion") = ddlOperacion.SelectedItem.ToString
                auxdr.Item("FechaOperacion") = tbFechaOperacion.Text

            End If

        Next

        ViewState("ListaOperacion") = dt

        gvDetalleOperacion.DataSource = dt
        gvDetalleOperacion.DataBind()

        ddlOperacion.SelectedValue = 0
        tbFechaOperacion.Text = Now.Date
        upOperacion.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro Operacion del detalle de Operaciones
    ''' </summary>
    ''' <param name="int_CodigoOperacion">Codigo de la Operacion que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarOperacion(ByVal int_CodigoOperacion As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaOperacion")
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelFichaMedOperaciones").ToString = int_CodigoOperacion Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaOperacion") = dt
        gvDetalleOperacion.DataSource = dt
        gvDetalleOperacion.DataBind()
        upOperacion.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle de Operacion al popup Operacion
    ''' </summary>
    ''' <param name="int_Codigo">Codigo Detalle Relacion Operacion</param>
    ''' <param name="int_CodigoOperacion">Código de Operacion</param>
    ''' <param name="dt_fecha">Fecha de registro</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarOperacion(ByVal int_Codigo As Integer, ByVal int_CodigoOperacion As Integer, ByVal dt_fecha As Date)

        ddlOperacion.SelectedValue = int_CodigoOperacion
        hidencodigoOperacion.Value = int_Codigo
        tbFechaOperacion.Text = dt_fecha
        pnModalOperacion.Show()
    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleOperacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then

                Dim CodigoRelFichaMedOperaciones As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoOperacion") = False
                    activarEditarOperacion(CodigoRelFichaMedOperaciones, CType(row.FindControl("lblCodigoTipoOperaciones"), Label).Text, CType(row.FindControl("lblFechaOperacion"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarOperacion(CodigoRelFichaMedOperaciones)
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleOperacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento de Detalle TipoControl"

#Region "Eventos"
    Protected Sub btn_Add_OtrosControles_click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Add_OtrosControles.Click
        ViewState("NuevoTipoControl") = True
        pnModalOtrosControles.Show()
    End Sub

    Protected Sub popup_btnAgregar_OtrosControles_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            If validarOtrosControles(usp_mensaje) Then
                If ViewState("NuevoTipoControl") = False Then
                    int_CodigoAccion = 201
                    editarTipoControl()
                ElseIf ViewState("NuevoTipoControl") = True Then
                    int_CodigoAccion = 200
                    agregarTipoControl()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                pnModalOtrosControles.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_OtrosControles_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalTipoControl()
    End Sub
#End Region
#Region "Métodos"

    Private Function validarOtrosControles(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If CDate(tbFechaTipoControl.Text) > CDate(Today.ToShortDateString) Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 8, "Fecha")
            result = False
        End If

        If ddlTipoControl.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo control")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Cierra el popup Tipo Control
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalTipoControl()

        pnModalOtrosControles.Hide()
        ddlTipoControl.SelectedValue = 0
        tbFechaTipoControl.Text = Now.Date
        tbResultadoTipoControl.Text = ""

    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Tipo Control al detalle de Tipo Control
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarTipoControl()

        Dim tipoControl As Integer
        Dim fecha As Date
        Dim resultado As String

        tipoControl = ddlTipoControl.SelectedValue
        fecha = tbFechaTipoControl.Text
        resultado = tbResultadoTipoControl.Text

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaTipoControl") Is Nothing Then

            dt = New DataTable("ListaTipoControl")

            dt = Datos.agregarColumna(dt, "CodigoRelFichaMedTiposControles", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoTipoControl", "Integer")
            dt = Datos.agregarColumna(dt, "FechaControl", "date")
            dt = Datos.agregarColumna(dt, "TipoControl", "String")
            dt = Datos.agregarColumna(dt, "Resultado", "String")

        Else

            dt = ViewState("ListaTipoControl")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoTipoControl").ToString = ddlTipoControl.SelectedValue And auxdr.Item("FechaControl").ToString = tbFechaTipoControl.Text Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    ddlTipoControl.SelectedValue = 0
                    tbFechaTipoControl.Text = Now.Date
                    tbResultadoTipoControl.Text = ""
                    pnModalOtrosControles.Show()
                    Exit Sub

                End If
                id_codigo_fila = auxdr.Item("CodigoRelFichaMedTiposControles").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelFichaMedTiposControles") = id_codigo_fila + 1
            dr.Item("CodigoTipoControl") = ddlTipoControl.SelectedValue
            dr.Item("TipoControl") = ddlTipoControl.SelectedItem.ToString
            dr.Item("FechaControl") = tbFechaTipoControl.Text
            dr.Item("Resultado") = tbResultadoTipoControl.Text

            dt.Rows.Add(dr)

        End If

        ViewState("ListaTipoControl") = dt

        gvDetalleTipoControl.DataSource = dt
        gvDetalleTipoControl.DataBind()

        ddlTipoControl.SelectedValue = 0
        tbFechaTipoControl.Text = Now.Date

        upOtrosControles.Update()

    End Sub

    ''' <summary>
    ''' Edita 1 Registro Tipo Control del detalle de Tipo Control
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarTipoControl()

        Dim tipoControl As Integer
        Dim fecha As Date
        Dim resultado As String

        tipoControl = ddlTipoControl.SelectedValue
        fecha = tbFechaTipoControl.Text
        resultado = tbResultadoTipoControl.Text

        'If CDate(tbFechaTipoControl.Text) > CDate(Today.ToShortDateString) Then
        '    MostrarSexyAlertBox("incorrecta. La fecha de registro no puede ser mayor a la fecha actual.", "Alert")
        '    ddlTipoControl.SelectedValue = tipoControl
        '    tbFechaTipoControl.Text = fecha
        '    tbResultadoTipoControl.Text = resultado
        '    pnModalOtrosControles.Show()
        '    Exit Sub
        'End If

        'If ddlTipoControl.SelectedValue = 0 Then

        '    MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
        '    'ddlTipoControl.SelectedValue = 0
        '    'tbFechaTipoControl.Text = Now.Date
        '    'tbResultadoTipoControl.Text = ""
        '    pnModalOtrosControles.Show()
        '    Exit Sub

        'End If

        'If tbResultadoTipoControl.Text.Trim.Length > 0 Then
        '    If Validacion.ValidarCamposIngreso(tbResultadoTipoControl) = False Then
        '        Dim str_alertas As String
        '        str_alertas = "<li><em> Resultado</em> incorrecta. No puede ingresar palabras que tengan más de 50 caracteres seguidos.</li>"
        '        MostrarSexyAlertBox(str_alertas, "Alert")
        '        pnModalOtrosControles.Show()
        '        Exit Sub
        '    End If
        'End If

        Dim int_CodigoOriginal As Integer = hidenCodigoTipoControl.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaTipoControl")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoTipoControl").ToString = ddlTipoControl.SelectedValue And auxdr.Item("FechaControl").ToString = tbFechaTipoControl.Text And auxdr.Item("Resultado").ToString = tbResultadoTipoControl.Text Then

                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                ddlTipoControl.SelectedValue = 0
                tbFechaTipoControl.Text = Now.Date
                tbResultadoTipoControl.Text = ""
                pnModalOtrosControles.Show()
                Exit Sub

            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelFichaMedTiposControles").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelFichaMedTiposControles") = int_CodigoOriginal
                auxdr.Item("CodigoTipoControl") = ddlTipoControl.SelectedValue
                auxdr.Item("TipoControl") = ddlTipoControl.SelectedItem.ToString
                auxdr.Item("FechaControl") = tbFechaTipoControl.Text
                auxdr.Item("Resultado") = tbResultadoTipoControl.Text

            End If

        Next

        ViewState("ListaTipoControl") = dt

        gvDetalleTipoControl.DataSource = dt
        gvDetalleTipoControl.DataBind()

        ddlTipoControl.SelectedValue = 0
        tbFechaTipoControl.Text = Now.Date
        tbResultadoTipoControl.Text = ""
        upOtrosControles.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro Tipo Control del detalle de Tipo Control
    ''' </summary>
    ''' <param name="int_TipoControl">Codigo de Tipo Control que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarTipoControl(ByVal int_TipoControl As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaTipoControl")

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelFichaMedTiposControles").ToString = int_TipoControl Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaTipoControl") = dt
        gvDetalleTipoControl.DataSource = dt
        gvDetalleTipoControl.DataBind()
        upOtrosControles.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle de Tipo Control al popup Tipo Control
    ''' </summary>
    ''' <param name="int_Codigo">Codigo Detalle Relacion Tipo Control</param>
    ''' <param name="int_TipoControl">Código de Tipo Control</param>
    ''' <param name="dt_fecha">Fecha de registro</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarTipoControl(ByVal int_codigo As Integer, ByVal int_TipoControl As Integer, ByVal dt_fecha As Date, ByVal resultado As String)
        ddlTipoControl.SelectedValue = int_TipoControl
        hidenCodigoTipoControl.Value = int_codigo
        tbFechaTipoControl.Text = dt_fecha
        tbResultadoTipoControl.Text = resultado
        pnModalOtrosControles.Show()
    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleTipoControl_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDetalleTipoControl.RowCommand
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then

                Dim CodigoRelFichaMedTiposControles As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoTipoControl") = False
                    activarEditarTipoControl(CodigoRelFichaMedTiposControles, CType(row.FindControl("lblCodigoTipoControl"), Label).Text, CType(row.FindControl("lblFechaControl"), Label).Text, CType(row.FindControl("lblResultado"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarTipoControl(CodigoRelFichaMedTiposControles)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub gvDetalleTipoControl_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleTipoControl.RowDataBound
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento de Detalle Control de Peso y Talla"

#Region "Eventos"
    Protected Sub btn_Add_ControlPesoTalla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Add_ControlPesoTalla.Click
        ViewState("NuevoControlPesoTalla") = True
        pnModalControlPesoTalla.Show()
    End Sub

    Protected Sub popup_btnAgregar_ControlPesoTalla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            If validarControlPesoTalla(usp_mensaje) Then
                If ViewState("NuevoControlPesoTalla") = False Then
                    int_CodigoAccion = 201
                    editarControlPesoTalla()
                ElseIf ViewState("NuevoControlPesoTalla") = True Then
                    int_CodigoAccion = 200
                    agregarControlPesoTalla()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                pnModalControlPesoTalla.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_ControlPesoTalla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalControlPesoTalla()
    End Sub
#End Region
#Region "Métodos"

    ''' <summary>
    ''' Cierra el popup Control Peso y Talla
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalControlPesoTalla()

        pnModalControlPesoTalla.Hide()
        tbFechaControlPesoTalla.Text = Now.Date
        tbTalla.Text = CDec(0.0)
        tbPeso.Text = CDec(0.0)
        tbObservacionTallaPeso.Text = ""

    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Control Peso y Talla al detalle de Control Peso y Talla
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarControlPesoTalla()

        Dim fecha As Date
        Dim talla As Decimal
        Dim peso As Decimal
        Dim observaciones As String

        fecha = tbFechaControlPesoTalla.Text
        talla = tbTalla.Text
        peso = tbPeso.Text
        observaciones = tbObservacionTallaPeso.Text

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaControlPesoTalla") Is Nothing Then

            dt = New DataTable("ListaControlPesoTalla")

            dt = Datos.agregarColumna(dt, "CodigoControlPesoTalla", "Integer")
            dt = Datos.agregarColumna(dt, "Talla", "Decimal")
            dt = Datos.agregarColumna(dt, "Peso", "Decimal")
            dt = Datos.agregarColumna(dt, "FechaControl", "Date")
            dt = Datos.agregarColumna(dt, "Observaciones", "String")

        Else

            dt = ViewState("ListaControlPesoTalla")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("FechaControl").ToString = tbFechaControlPesoTalla.Text Then

                    MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                    Exit Sub

                End If
                id_codigo_fila = auxdr.Item("CodigoControlPesoTalla").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow
            'Format(Convert.ToDecimal(tbPeso.Text), "00.00")
            dr.Item("CodigoControlPesoTalla") = id_codigo_fila + 1
            dr.Item("Talla") = Convert.ToDecimal(tbTalla.Text)
            If ExistePuntoDecimal(tbPeso) = True Then
                dr.Item("Peso") = Convert.ToDecimal(tbPeso.Text)
            Else
                dr.Item("Peso") = Format(Convert.ToDecimal(tbPeso.Text), "000.00")
            End If

            dr.Item("FechaControl") = tbFechaControlPesoTalla.Text
            dr.Item("Observaciones") = tbObservacionTallaPeso.Text

            dt.Rows.Add(dr)

        End If

        ViewState("ListaControlPesoTalla") = dt

        gvDetalleControlPesoTalla.DataSource = dt
        gvDetalleControlPesoTalla.DataBind()

        tbFechaControlPesoTalla.Text = Now.Date
        tbTalla.Text = CDec(0.0)
        tbPeso.Text = CDec(0.0)
        tbObservacionTallaPeso.Text = ""

        upControlPesoTalla.Update()

    End Sub

    Private Function ExistePuntoDecimalIncorrecto(ByVal txt_CampoIngreso As System.Web.UI.WebControls.TextBox, ByVal int_tipo As Integer) As Boolean
        Dim texto As String = txt_CampoIngreso.Text.Trim
        Dim int_cont As Integer
        Dim str_palabra As String
        Dim int_contpalabra As Integer
        Dim alert As Boolean = True

        While int_cont <= texto.Length - 1

            str_palabra = texto.Substring(int_cont, 1)

            If int_tipo = 1 Then
                If str_palabra = "." Then
                    int_contpalabra = int_contpalabra + 1
                    If int_cont = 4 Then
                        alert = False
                    End If
                Else
                    int_contpalabra = 0
                End If
            ElseIf int_tipo = 2 Then
                If str_palabra = "." Then
                    int_contpalabra = int_contpalabra + 1
                    If int_cont = 2 Then
                        alert = False
                    End If
                Else
                    int_contpalabra = 0
                End If
            End If

            int_cont = int_cont + 1
        End While

        If int_contpalabra > 1 Then
            alert = False
        End If

        Return alert
    End Function

    Private Function ExistePuntoDecimal(ByVal txt_CampoIngreso As System.Web.UI.WebControls.TextBox) As Boolean
        Dim texto As String = txt_CampoIngreso.Text.Trim
        Dim int_cont As Integer
        Dim str_palabra As String
        Dim alert As Boolean = False

        While int_cont <= texto.Length - 1

            str_palabra = texto.Substring(int_cont, 1)

            If str_palabra = "." Then
                alert = True
            End If

            int_cont = int_cont + 1
        End While

        Return alert
    End Function

    Private Function ValidarPuntoDecimalIzq(ByVal txt_CampoIngreso As System.Web.UI.WebControls.TextBox) As Boolean
        Dim texto As String = txt_CampoIngreso.Text.Trim
        Dim int_cont As Integer
        Dim str_palabra As String
        Dim int_contpalabra As Integer
        Dim alert As Boolean = True

        If texto.Length = 1 Or texto.Length = 2 Or texto.Length = 3 Then
            alert = False
        ElseIf texto.Length > 3 Then
            While int_cont <= texto.Length - 1

                str_palabra = texto.Substring(int_cont, 1)

                If str_palabra = " " Then
                    int_contpalabra = 0
                Else
                    int_contpalabra = int_contpalabra + 1
                End If

                If int_contpalabra = 2 Then
                    If str_palabra = "." Then
                        'alert = False
                        'Exit While
                    Else
                        alert = False
                        Exit While
                    End If
                ElseIf int_contpalabra = 3 Or int_contpalabra = 4 Then
                    If str_palabra = "." Then
                        alert = False
                        Exit While
                    End If
                End If

                int_cont = int_cont + 1
            End While
        End If

        Return alert
    End Function

    Private Function validarControlPesoTalla(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If CDate(tbFechaControlPesoTalla.Text) > CDate(Today.ToShortDateString) Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 8, "Fecha")
            result = False
        End If

        If tbTalla.Text = "" Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Talla")
            result = False
        ElseIf ExistePuntoDecimalIncorrecto(tbTalla, 2) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 39, "Talla")
            result = False
        Else
            If CDec(tbTalla.Text) < 0.5 Or CDec(tbTalla.Text) > 9.0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 41, "Talla")
                result = False
            Else
                If ValidarPuntoDecimalIzq(tbTalla) = False Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 38, "Talla")
                    result = False
                End If
            End If
        End If

        If tbPeso.Text = "" Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Peso")
            result = False
        ElseIf ExistePuntoDecimalIncorrecto(tbPeso, 1) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 39, "Peso")
            result = False
        Else
            If CDec(tbPeso.Text) < 10.0 Or CDec(tbPeso.Text) > 150.0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 42, "Peso")
                result = False
            End If
        End If

        If tbObservacionTallaPeso.Text.Trim.Length > 0 Then
            If Validacion.ValidarCamposIngreso(tbObservacionTallaPeso) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Observación")
                result = False
            End If
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Edita 1 Registro Control Peso y Talla del detalle de Control Peso y Talla
    ''' </summary>
    ''' <remarks>
    ''' Creador:        Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarControlPesoTalla()

        Dim fecha As Date
        Dim talla As Decimal
        Dim peso As Decimal
        Dim observaciones As String

        fecha = tbFechaControlPesoTalla.Text
        talla = tbTalla.Text
        peso = tbPeso.Text
        observaciones = tbObservacionTallaPeso.Text

        Dim int_CodigoOriginal As Integer = hidenCodigoControlPesoTalla.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = True

        dt = ViewState("ListaControlPesoTalla")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoControlPesoTalla").ToString = int_CodigoOriginal Then

            ElseIf auxdr.Item("FechaControl").ToString = tbFechaControlPesoTalla.Text Then
                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                Exit Sub

            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoControlPesoTalla").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoControlPesoTalla") = int_CodigoOriginal
                auxdr.Item("Talla") = Convert.ToDecimal(tbTalla.Text)
                If ExistePuntoDecimal(tbPeso) = True Then
                    auxdr.Item("Peso") = Convert.ToDecimal(tbPeso.Text)
                Else
                    auxdr.Item("Peso") = Format(Convert.ToDecimal(tbPeso.Text), "000.00")
                End If
                auxdr.Item("FechaControl") = tbFechaControlPesoTalla.Text
                auxdr.Item("Observaciones") = tbObservacionTallaPeso.Text

            End If

        Next

        ViewState("ListaControlPesoTalla") = dt

        gvDetalleControlPesoTalla.DataSource = dt
        gvDetalleControlPesoTalla.DataBind()

        tbFechaControlPesoTalla.Text = Now.Date
        tbTalla.Text = CDec(0.0)
        tbPeso.Text = CDec(0.0)
        tbObservacionTallaPeso.Text = ""

        upControlPesoTalla.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro Control Peso y Talla del detalle de Control Peso y Talla
    ''' </summary>
    ''' <param name="int_CodigoControlPesoTalla">Codigo de Control Peso y Talla que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarControlPesoTalla(ByVal int_CodigoControlPesoTalla As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaControlPesoTalla")

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoControlPesoTalla").ToString = int_CodigoControlPesoTalla Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaControlPesoTalla") = dt
        gvDetalleControlPesoTalla.DataSource = dt
        gvDetalleControlPesoTalla.DataBind()
        upControlPesoTalla.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle de Control Peso y Talla al popup Control Peso y Talla
    ''' </summary>
    ''' <param name="int_CodigoControlPesoTalla">Codigo Detalle Relacion Tipo Control</param>
    ''' <param name="peso">Peso</param>
    ''' <param name="talla">Talla</param>
    ''' <param name="Resultado">Resultado</param>
    ''' <param name="dt_fecha">Fecha de registro</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarControlPesoTalla(ByVal int_CodigoControlPesoTalla As Integer, ByVal peso As Decimal, ByVal talla As Decimal, ByVal Resultado As String, ByVal dt_fecha As Date)
        hidenCodigoControlPesoTalla.Value = int_CodigoControlPesoTalla
        tbFechaControlPesoTalla.Text = dt_fecha
        tbPeso.Text = peso
        tbTalla.Text = talla
        tbObservacionTallaPeso.Text = Resultado

        pnModalControlPesoTalla.Show()
    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleControlPesoTalla_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDetalleControlPesoTalla.RowCommand
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then

                Dim CodigoControlPesoTalla As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoControlPesoTalla") = False
                    activarEditarControlPesoTalla(CodigoControlPesoTalla, CType(row.FindControl("lblPeso"), Label).Text, CType(row.FindControl("lblTalla"), Label).Text, CType(row.FindControl("lblObservacionesPesoTalla"), Label).Text, CType(row.FindControl("lblFechaControlPesoTalla"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarControlPesoTalla(CodigoControlPesoTalla)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub
    Protected Sub gvDetalleControlPesoTalla_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleControlPesoTalla.RowDataBound
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento de Datos del Seguro"

#Region "Eventos"
    Protected Sub btn_Add_DatosSeguro_click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        LimpiarDatosSeguro()
        ViewState("NuevoDatosSeguro") = True

        pnModalDatosSeguro.Show()
    End Sub

    Protected Sub popup_btnAgregar_DatosSeguro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            If validarDatosSeguro(usp_mensaje) Then
                If ViewState("NuevoDatosSeguro") = False Then
                    int_CodigoAccion = 201
                    editarDatosSeguro()
                ElseIf ViewState("NuevoDatosSeguro") = True Then
                    int_CodigoAccion = 200
                    agregarDatosSeguro()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                pnModalDatosSeguro.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_DatosSeguro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalDatosSeguro()
    End Sub
#End Region
#Region "Métodos"

    Protected Sub ddlTipoSeguro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            CargarComboClinicas()

            pnModalDatosSeguro.Show()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Private Sub LimpiarDatosSeguro()
        ddlAnioDatosSeguro.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlTipoSeguro.SelectedValue = 0
        ddlCompania.SelectedValue = 0
        tbNumeroPoliza.Text = ""
        rbVigencia.SelectedValue = 0
        tbRep1_FechaInicio.Text = ""
        tbRep1_FechaFin.Text = ""
        ddlClinica1.SelectedValue = 0
        ddlClinica2.SelectedValue = 0
        ddlClinica3.SelectedValue = 0
        tbNombCompaniaAmb.Text = ""
        tbTelfAmbulancia.Text = ""
        rdCarnetSeguro.SelectedValue = 0
    End Sub
    Private Function validarDatosSeguro(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlTipoSeguro.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo Seguro")
            result = False
        End If

        If ddlTipoSeguro.SelectedValue = 2 Then
            If ddlCompania.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Compañia")
                result = False
            End If
            If ddlClinica1.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Clínica")
                result = False
            End If

            If tbNumeroPoliza.Text.Trim.Length = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Número de Poliza")
                result = False
            End If

            If Validacion.ValidarCamposIngreso(tbNumeroPoliza) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Número de Poliza")
                result = False
            End If


        End If




        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Cierra el popup Tipo Control
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     23/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalDatosSeguro()

        pnModalDatosSeguro.Hide()
        LimpiarDatosSeguro()
        'ddlTipoControl.SelectedValue = 0
        'tbFechaTipoControl.Text = Now.Date
        'tbResultadoTipoControl.Text = ""

    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Tipo Control al detalle de Tipo Control
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarDatosSeguro()

        Dim dt As DataTable
        'Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaDatosSeguro") Is Nothing Then

            dt = New DataTable("ListaDatosSeguro")

            dt = Datos.agregarColumna(dt, "CodigoRelFichaMedDatosSeguro", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoTipoSeguro", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoCompania", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoAnio", "String")
            dt = Datos.agregarColumna(dt, "Vigencia", "String")
            dt = Datos.agregarColumna(dt, "FechaInicio", "String")
            dt = Datos.agregarColumna(dt, "FechaFin", "String")
            dt = Datos.agregarColumna(dt, "CodigoClinica", "String")
            dt = Datos.agregarColumna(dt, "AmbulanciaCompania", "String")
            dt = Datos.agregarColumna(dt, "TelefonoAmbulancia", "String")
            dt = Datos.agregarColumna(dt, "CopiaCarnetSeguro", "String")
            dt = Datos.agregarColumna(dt, "AnioMatricula", "String")
            dt = Datos.agregarColumna(dt, "Tipo", "String")
            dt = Datos.agregarColumna(dt, "Compania", "String")
            dt = Datos.agregarColumna(dt, "NumeroPoliza", "String")
            dt = Datos.agregarColumna(dt, "Clinica", "String")
        Else

            dt = ViewState("ListaDatosSeguro")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                '        'If auxdr.Item("CodigoTipoControl").ToString = ddlTipoControl.SelectedValue And auxdr.Item("FechaControl").ToString = tbFechaTipoControl.Text Then
                '        '    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                '        '    ddlTipoControl.SelectedValue = 0
                '        '    tbFechaTipoControl.Text = Now.Date
                '        '    tbResultadoTipoControl.Text = ""
                '        '    pnModalOtrosControles.Show()
                '        '    Exit Sub

                '        'End If
                id_codigo_fila = auxdr.Item("CodigoRelFichaMedDatosSeguro").ToString()
            Next

        End If

        'If boolIncremento = False Then
        Dim int_CopiaCarnetSeg As Integer = 0

        If rdCarnetSeguro.SelectedValue = False Then
            int_CopiaCarnetSeg = 0
        Else
            int_CopiaCarnetSeg = 1
        End If

        Dim dr As DataRow
        dr = dt.NewRow

        dr.Item("CodigoRelFichaMedDatosSeguro") = id_codigo_fila + 1
        dr.Item("CodigoTipoSeguro") = ddlTipoSeguro.SelectedValue
        dr.Item("CodigoCompania") = ddlCompania.SelectedValue

        dr.Item("CodigoAnio") = ddlAnioDatosSeguro.SelectedValue
        dr.Item("Vigencia") = rbVigencia.SelectedValue
        If rbVigencia.SelectedValue = 0 Or rbVigencia.SelectedValue = 1 Then '0=indefinido ,1=Segun contrato
            dr.Item("FechaInicio") = "01/01/1753"
            dr.Item("FechaFin") = "01/01/1753"
        ElseIf rbVigencia.SelectedValue = 2 Then 'por fechas
            dr.Item("FechaInicio") = IIf(tbRep1_FechaInicio.Text.Trim.Length = 0, Nothing, tbRep1_FechaInicio.Text.Trim)
            dr.Item("FechaFin") = IIf(tbRep1_FechaFin.Text.Trim.Length = 0, Nothing, tbRep1_FechaFin.Text.Trim)
        End If

        dr.Item("CodigoClinica") = MostrarCodigoCadenasClinicas()
        dr.Item("AmbulanciaCompania") = tbNombCompaniaAmb.Text
        dr.Item("TelefonoAmbulancia") = tbTelfAmbulancia.Text
        dr.Item("CopiaCarnetSeguro") = int_CopiaCarnetSeg

        dr.Item("AnioMatricula") = ddlAnioDatosSeguro.SelectedItem.ToString
        dr.Item("Tipo") = ddlTipoSeguro.SelectedItem.ToString
        dr.Item("Compania") = IIf(ddlCompania.SelectedItem.ToString = "--Seleccione--", "", ddlCompania.SelectedItem.ToString)
        dr.Item("NumeroPoliza") = tbNumeroPoliza.Text
        dr.Item("Clinica") = MostrarCadenasClinicas()
        dt.Rows.Add(dr)

        'End If

        ViewState("ListaDatosSeguro") = dt

        gvDetalleDatosSeguro.DataSource = dt
        gvDetalleDatosSeguro.DataBind()

        LimpiarDatosSeguro()

        upDatosSeguro.Update()

    End Sub

    ''' <summary>
    ''' Edita 1 Registro Tipo Control del detalle de Tipo Control
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarDatosSeguro()


        Dim int_CodigoOriginal As Integer = hidenCodigoDatosSeguro.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaDatosSeguro")

        'For Each auxdr As DataRow In dt.Rows

        '    If auxdr.Item("CodigoTipoControl").ToString = ddlTipoControl.SelectedValue And auxdr.Item("FechaControl").ToString = tbFechaTipoControl.Text And auxdr.Item("Resultado").ToString = tbResultadoTipoControl.Text Then

        '        MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
        '        ddlTipoControl.SelectedValue = 0
        '        tbFechaTipoControl.Text = Now.Date
        '        tbResultadoTipoControl.Text = ""
        '        pnModalOtrosControles.Show()
        '        Exit Sub

        '    End If

        'Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelFichaMedDatosSeguro").ToString = int_CodigoOriginal Then

                Dim int_CopiaCarnetSeg As Integer = 0

                If rdCarnetSeguro.SelectedValue = False Then
                    int_CopiaCarnetSeg = 0
                Else
                    int_CopiaCarnetSeg = 1
                End If
                auxdr.Item("CodigoRelFichaMedDatosSeguro") = int_CodigoOriginal
                auxdr.Item("CodigoTipoSeguro") = ddlTipoSeguro.SelectedValue
                auxdr.Item("CodigoCompania") = ddlCompania.SelectedValue
                auxdr.Item("CodigoAnio") = ddlAnioDatosSeguro.SelectedValue
                auxdr.Item("Vigencia") = rbVigencia.SelectedValue

                If rbVigencia.SelectedValue = 0 Or rbVigencia.SelectedValue = 1 Then '0=indefinido ,1=Segun contrato
                    auxdr.Item("FechaInicio") = "01/01/1753"
                    auxdr.Item("FechaFin") = "01/01/1753"
                ElseIf rbVigencia.SelectedValue = 2 Then 'por fechas
                    auxdr.Item("FechaInicio") = IIf(tbRep1_FechaInicio.Text.Trim.Length = 0, "01/01/1753", tbRep1_FechaInicio.Text.Trim)
                    auxdr.Item("FechaFin") = IIf(tbRep1_FechaFin.Text.Trim.Length = 0, "01/01/1753", tbRep1_FechaFin.Text.Trim)
                End If

                auxdr.Item("CodigoClinica") = MostrarCodigoCadenasClinicas() 'ddlClinica1.SelectedValue 'SelectedItem.ToString
                auxdr.Item("AmbulanciaCompania") = tbNombCompaniaAmb.Text
                auxdr.Item("TelefonoAmbulancia") = tbTelfAmbulancia.Text
                auxdr.Item("CopiaCarnetSeguro") = int_CopiaCarnetSeg
                auxdr.Item("AnioMatricula") = ddlAnioDatosSeguro.SelectedItem.ToString
                auxdr.Item("Tipo") = ddlTipoSeguro.SelectedItem.ToString
                auxdr.Item("Compania") = IIf(ddlCompania.SelectedItem.ToString = "--Seleccione--", "", ddlCompania.SelectedItem.ToString)
                auxdr.Item("NumeroPoliza") = tbNumeroPoliza.Text
                auxdr.Item("Clinica") = MostrarCadenasClinicas()
            End If

        Next

        ViewState("ListaDatosSeguro") = dt

        gvDetalleDatosSeguro.DataSource = dt
        gvDetalleDatosSeguro.DataBind()

        LimpiarDatosSeguro()
        upDatosSeguro.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro Tipo Control del detalle de Tipo Control
    ''' </summary>
    ''' <param name="int_FichaSeguro">Codigo de Tipo Control que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarDatosSeguro(ByVal int_FichaSeguro As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaDatosSeguro")

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelFichaMedDatosSeguro").ToString = int_FichaSeguro Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaDatosSeguro") = dt
        gvDetalleDatosSeguro.DataSource = dt
        gvDetalleDatosSeguro.DataBind()
        upDatosSeguro.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle de Tipo Control al popup Tipo Control
    ''' </summary>
    ''' <param name="int_Codigo">Codigo Detalle Relacion Tipo Control</param>
    ''' <param name="int_TipoControl">Código de Tipo Control</param>
    ''' <param name="dt_fecha">Fecha de registro</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarDatosSeguro(ByVal int_codigo As Integer, ByVal int_CodigoAnio As Integer, ByVal int_CodigoTipoSeguro As Integer, ByVal int_CodigoCompania As Integer, ByVal str_NumeroPoliza As String, _
                                         ByVal int_Vigencia As Integer, ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, ByVal str_CodigoClinica As String, ByVal str_AmbulanciaCompania As String, _
                                         ByVal str_TelefonoAmbulancia As String, ByVal int_CopiaCarnetSeguro As Integer)
        hidenCodigoDatosSeguro.Value = int_codigo
        ddlAnioDatosSeguro.SelectedValue = int_CodigoAnio
        ddlTipoSeguro.SelectedValue = int_CodigoTipoSeguro
        ddlCompania.SelectedValue = int_CodigoCompania
        tbNumeroPoliza.Text = str_NumeroPoliza
        rbVigencia.SelectedValue = int_Vigencia

        tbRep1_FechaInicio.Text = dt_FechaInicio
        tbRep1_FechaFin.Text = dt_FechaFin
        tbNombCompaniaAmb.Text = str_AmbulanciaCompania
        tbTelfAmbulancia.Text = str_TelefonoAmbulancia
        rdCarnetSeguro.SelectedValue = int_CopiaCarnetSeguro

        ViewState("ListaClinica") = str_CodigoClinica

        Obtenerclinicas()

        pnModalDatosSeguro.Show()
    End Sub


    Private Sub Obtenerclinicas()
        Dim arr_Datos As String() = ViewState("ListaClinica").ToString.Split(",")
        'For Each clinicas As String In arr_Datos

        For i As Integer = 0 To arr_Datos.Count - 1
            If i = 0 Then
                'ddlClinica1.SelectedValue = IIf(arr_Datos(0) = "", 0, CInt(arr_Datos(0))) ' CInt(arr_Datos(0))
                If Len(arr_Datos(0)) = 0 Then
                    ddlClinica1.SelectedValue = 0
                Else
                    ddlClinica1.SelectedValue = CInt(arr_Datos(0))
                End If
            End If
            If i = 1 Then
                'ddlClinica2.SelectedValue = CInt(arr_Datos(1))
                If Len(arr_Datos(1)) = 0 Then
                    ddlClinica2.SelectedValue = 0
                Else
                    ddlClinica2.SelectedValue = CInt(arr_Datos(1))
                End If
            End If
            If i = 2 Then
                'ddlClinica3.SelectedValue = CInt(arr_Datos(2))
                If Len(arr_Datos(2)) = 0 Then
                    ddlClinica3.SelectedValue = 0
                Else
                    ddlClinica3.SelectedValue = CInt(arr_Datos(2))
                End If
            End If
        Next


    End Sub

    Private Function MostrarCadenasClinicas() As String
        Dim str_clinicas As String

        str_clinicas = IIf(ddlClinica1.SelectedItem.ToString = "--Seleccione--", "", ddlClinica1.SelectedItem.ToString) & "," & IIf(ddlClinica2.SelectedItem.ToString = "--Seleccione--", "", ddlClinica2.SelectedItem.ToString) & "," & IIf(ddlClinica3.SelectedItem.ToString = "--Seleccione--", "", ddlClinica3.SelectedItem.ToString)

        Return str_clinicas
    End Function
    Private Function MostrarCodigoCadenasClinicas() As String
        Dim str_Codigoclinicas As String

        str_Codigoclinicas = IIf(ddlClinica1.SelectedValue = 0, "", ddlClinica1.SelectedValue) & "," & IIf(ddlClinica2.SelectedValue = 0, "", ddlClinica2.SelectedValue) & "," & IIf(ddlClinica3.SelectedValue = 0, "", ddlClinica3.SelectedValue)

        Return str_Codigoclinicas
    End Function


#End Region
#Region "Gridview"
    Protected Sub gvDetalleDatosSeguro_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then

                Dim CodigoRelFichaMedDatosSeguro As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                Dim int_Vigencia As Integer = 0
                Dim int_CopiaCarnetSeguro As Integer = 0

                If e.CommandName = "Actualizar" Then
                    If CType(row.FindControl("lblVigencia"), Label).Text = 0 Then
                        int_Vigencia = 0
                    ElseIf CType(row.FindControl("lblVigencia"), Label).Text = 1 Then
                        int_Vigencia = 1
                    ElseIf CType(row.FindControl("lblVigencia"), Label).Text = 2 Then
                        int_Vigencia = 2
                    End If

                    If CType(row.FindControl("lblCopiaCarnetSeguro"), Label).Text = False Then
                        int_CopiaCarnetSeguro = 0
                    Else
                        int_CopiaCarnetSeguro = 1
                    End If
                    int_CodigoAccion = 201
                    ViewState("NuevoDatosSeguro") = False
                    activarEditarDatosSeguro(CodigoRelFichaMedDatosSeguro, CType(row.FindControl("lblCodigoAnio"), Label).Text, CType(row.FindControl("lblCodigoTipoSeguro"), Label).Text, CType(row.FindControl("lblCodigoCompania"), Label).Text, CType(row.FindControl("lblNumeroPoliza"), Label).Text, _
                                         int_Vigencia, CType(row.FindControl("lblFechaInicio"), Label).Text, CType(row.FindControl("lblFechaFin"), Label).Text, CType(row.FindControl("lblCodigoClinica"), Label).Text, _
                                             CType(row.FindControl("lblAmbulanciaCompania"), Label).Text, CType(row.FindControl("lblTelefonoAmbulancia"), Label).Text, int_CopiaCarnetSeguro)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarDatosSeguro(CodigoRelFichaMedDatosSeguro)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub gvDetalleDatosSeguro_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region


#End Region

#Region "Mantenimiento de Datos de la Renta Estudiantil"

#Region "Eventos"
    Protected Sub btn_Add_RentaEstudiantil_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        LimpiarRentaEstudiantil()
        ViewState("NuevoRentaEstudiantil") = True

        pnModalRentaEstudiantil.Show()
    End Sub

    Protected Sub popup_btnAgregar_RentaEstudiantil_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            Dim usp_mensaje As String = ""
            If validarRentaEstudiantil(usp_mensaje) Then
                If ViewState("NuevoRentaEstudiantil") = False Then
                    int_CodigoAccion = 201
                    editarRentaEstudiantil()
                ElseIf ViewState("NuevoRentaEstudiantil") = True Then
                    int_CodigoAccion = 200
                    agregarRentaEstudiantil()
                End If
            Else
                MostrarAlertas(usp_mensaje)
                pnModalRentaEstudiantil.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_RentaEstudiantil_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalRentaEstudiantil()
    End Sub
#End Region
#Region "Métodos"



    Private Sub LimpiarRentaEstudiantil()
        ddl_Familiar_PriTitular.SelectedValue = 0
        ddl_Familiar_SegTitular.SelectedValue = 0
    End Sub
    Private Function validarRentaEstudiantil(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddl_Familiar_PriTitular.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Primer Titular")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Cierra el popup Renta Estudiantil
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     20/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalRentaEstudiantil()

        pnModalRentaEstudiantil.Hide()
        LimpiarRentaEstudiantil()

    End Sub

    ''' <summary>
    ''' Agrega 1 Registro Tipo Control al detalle de Tipo Control
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     20/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarRentaEstudiantil()

        Dim dt As DataTable
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaRentaEstudiantil") Is Nothing Then

            dt = New DataTable("ListaRentaEstudiantil")

            dt = Datos.agregarColumna(dt, "CodigoRelFichaMedRentaEstudiantil", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoFamiliarPrimerTitular", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoFamiliarSegundoTitular", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoAnioAcademico", "Integer")
            dt = Datos.agregarColumna(dt, "AnioAcademico", "String")
            dt = Datos.agregarColumna(dt, "FamiliarPrimerTitular", "String")
            dt = Datos.agregarColumna(dt, "FamiliarSegundoTitular", "String")
        Else

            dt = ViewState("ListaRentaEstudiantil")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                id_codigo_fila = auxdr.Item("CodigoRelFichaMedRentaEstudiantil").ToString()
            Next

        End If

        Dim dr As DataRow
        dr = dt.NewRow

        dr.Item("CodigoRelFichaMedRentaEstudiantil") = id_codigo_fila + 1
        dr.Item("CodigoFamiliarPrimerTitular") = ddl_Familiar_PriTitular.SelectedValue
        dr.Item("CodigoFamiliarSegundoTitular") = ddl_Familiar_SegTitular.SelectedValue
        dr.Item("CodigoAnioAcademico") = ddlAnioRentaEst.SelectedValue
        dr.Item("AnioAcademico") = ddlAnioRentaEst.SelectedItem.ToString
        dr.Item("FamiliarPrimerTitular") = IIf(ddl_Familiar_PriTitular.SelectedItem.ToString = "--Seleccione--", "", ddl_Familiar_PriTitular.SelectedItem.ToString)
        dr.Item("FamiliarSegundoTitular") = IIf(ddl_Familiar_SegTitular.SelectedItem.ToString = "--Seleccione--", "", ddl_Familiar_SegTitular.SelectedItem.ToString)
        dt.Rows.Add(dr)

        'End If

        ViewState("ListaRentaEstudiantil") = dt

        gvDetalleRentaEstudiantil.DataSource = dt
        gvDetalleRentaEstudiantil.DataBind()

        LimpiarRentaEstudiantil()

        upRentaEstudiantil.Update()

    End Sub

    ''' <summary>
    ''' Edita 1 Registro del detalle de Renta Estudiantil
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     20/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarRentaEstudiantil()


        Dim int_CodigoOriginal As Integer = hidenCodigoSeguroRentaEstudiantil.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaRentaEstudiantil")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelFichaMedRentaEstudiantil").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelFichaMedRentaEstudiantil") = int_CodigoOriginal
                auxdr.Item("CodigoFamiliarPrimerTitular") = ddl_Familiar_PriTitular.SelectedValue
                auxdr.Item("CodigoFamiliarSegundoTitular") = ddl_Familiar_SegTitular.SelectedValue
                auxdr.Item("CodigoAnioAcademico") = ddlAnioRentaEst.SelectedValue
                auxdr.Item("AnioAcademico") = ddlAnioRentaEst.SelectedItem.ToString
                auxdr.Item("FamiliarPrimerTitular") = IIf(ddl_Familiar_PriTitular.SelectedItem.ToString = "--Seleccione--", "", ddl_Familiar_PriTitular.SelectedItem.ToString)
                auxdr.Item("FamiliarSegundoTitular") = IIf(ddl_Familiar_SegTitular.SelectedItem.ToString = "--Seleccione--", "", ddl_Familiar_SegTitular.SelectedItem.ToString)
            End If

        Next

        ViewState("ListaRentaEstudiantil") = dt

        gvDetalleRentaEstudiantil.DataSource = dt
        gvDetalleRentaEstudiantil.DataBind()

        LimpiarRentaEstudiantil()
        upRentaEstudiantil.Update()

    End Sub

    ''' <summary>
    ''' Elimina 1 Registro del detalle de Renta Estudiantil
    ''' </summary>
    ''' <param name="int_FichaRentaEstudiantil">Codigo de Renta estudiantil que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas Flores
    ''' Fecha de Creación:     20/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarRentaEstudiantil(ByVal int_FichaRentaEstudiantil As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaRentaEstudiantil")

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelFichaMedRentaEstudiantil").ToString = int_FichaRentaEstudiantil Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaRentaEstudiantil") = dt
        gvDetalleRentaEstudiantil.DataSource = dt
        gvDetalleRentaEstudiantil.DataBind()
        upRentaEstudiantil.Update()

    End Sub

    ''' <summary>
    ''' Setea los valores de la grilla detalle de Tipo Control al popup Tipo Control
    ''' </summary>
    ''' <param name="int_Codigo">Codigo Detalle Relacion Tipo Control</param>
    ''' <param name="int_TipoControl">Código de Tipo Control</param>
    ''' <param name="dt_fecha">Fecha de registro</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     20/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarRentaEstudiantil(ByVal int_codigo As Integer, ByVal int_CodigoAnio As Integer, ByVal int_CodigoPrimTitular As Integer, ByVal int_CodigoSegTitular As Integer)
        hidenCodigoSeguroRentaEstudiantil.Value = int_codigo
        ddlAnioRentaEst.SelectedValue = int_CodigoAnio
        ddl_Familiar_PriTitular.SelectedValue = int_CodigoPrimTitular
        ddl_Familiar_SegTitular.SelectedValue = int_CodigoSegTitular

        pnModalRentaEstudiantil.Show()
    End Sub

#End Region
#Region "Gridview"
    Protected Sub gvDetalleRentaEstudiantil_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then

                Dim CodigoRelFichaMedRentaEstudiantil As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then

                    int_CodigoAccion = 201
                    ViewState("NuevoRentaEstudiantil") = False
                    activarEditarRentaEstudiantil(CodigoRelFichaMedRentaEstudiantil, CType(row.FindControl("lblCodigoAnioAcademico"), Label).Text, CType(row.FindControl("lblCodigoPrimerTitular"), Label).Text, CType(row.FindControl("lblCodigoSegundoTitular"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarRentaEstudiantil(CodigoRelFichaMedRentaEstudiantil)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub gvDetalleRentaEstudiantil_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region


#End Region

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
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(1, 1, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region


End Class
