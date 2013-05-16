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
''' Código del Modulo:    1
''' Código de la Opció:  59
''' </remarks>

Partial Class Modulo_Enfermeria_ValidarDatosActualizadosFichaMedica
    Inherits System.Web.UI.Page

#Region "Evento"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Actualización de Ficha Medica")

            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                ViewState("SortExpression") = "NombreCompleto"
                ViewState("Direccion") = "ASC"
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                cargarCombosFamiliarAlumno()

                tbFechaSolicitudInicio.Text = Today.Date.AddMonths(-1)
                tbFechaSolicitudFin.Text = Today.ToShortDateString

                hidenCodigoPerfil.Value = Me.Master.Obtener_CodigoPerfil
                listarFichas()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click()
        Try
            Dim usp_mensaje As String = ""
            If validarBusqueda(usp_mensaje) Then
                listarFichas()
            Else
                MostrarAlertas(usp_mensaje)
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        limpiarFiltros()
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


    Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            CambiarCheckbox(sender)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCancelarFicha_Click()
        CancelarFicha()
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim strMensaje As String = ""
            If validarGrabar(strMensaje) Then
                GrabarFicha()
            Else
                MostrarSexyAlertBox(strMensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Métodos"

    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(1, 59)
        'CONTROLES DEL FORMULARIO



    End Sub

    ''' <summary>
    ''' Valida los parametros de busqueda antes de realizar la consulta
    ''' </summary>
    ''' <param name="str_Mensaje">Cadena de texto que tendra todos los mensajes de error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarBusqueda(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If IsDate(tbFechaSolicitudInicio.Text.Trim) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Solicitud Inicial")
            result = False
        End If

        If IsDate(tbFechaSolicitudFin.Text.Trim) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Solicitud Final")
            result = False
        End If

        If IsDate(tbFechaSolicitudInicio.Text.Trim) And IsDate(tbFechaSolicitudFin.Text.Trim) Then

            If (CType(tbFechaSolicitudInicio.Text, Date) > CType(tbFechaSolicitudFin.Text, Date)) Then

                str_alertas = Alertas.ObtenerAlerta(str_alertas, 7, "Fecha de Solicitud")
                result = False

            End If

        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Setea el estado de los campos y opciones de la ficha médica de alumno
    ''' </summary> 
    ''' <param name="str_Modo">Tipo de visualizacion que tendra los datos del formulario</param>  
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String)

        miTab1_1.Enabled = False
        miTab2_2.Enabled = True
        lbTab2_2.Text = str_Modo
        TabContainer1.ActiveTabIndex = 1

    End Sub

    ''' <summary>
    ''' Limpia los items de una lista desplegable
    ''' </summary>
    ''' <param name="combo">Nombre que identifica a la lista desplegable</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)
        Controles.limpiarCombo(combo, True, False)
    End Sub

    ''' <summary>
    ''' Setea los campos de busqueda del formulario a sus valores por defecto
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()
        tbFechaSolicitudInicio.Text = Today.ToShortDateString
        tbFechaSolicitudFin.Text = Today.ToShortDateString
        tbBuscarFamiliarApellidoPaterno.Text = ""
        tbBuscarFamiliarApellidoMaterno.Text = ""
        tbBuscarFamiliarNombre.Text = ""
        cargarCombosFamiliarAlumno()
        rbEstados.SelectedValue = 1

    End Sub

    ''' <summary>
    ''' Carga una serie de listas desplegables vinculadas a la busqueda de familiares del alumnos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
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
    ''' Limpia las listas desplegables de vinculadas a la busqueda de familiares del alumno
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
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
    ''' Carga el combo con la lista de Niveles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 59)
        Controles.llenarCombo(ddlBuscarFamiliarNivel, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de SubNiveles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoSubNivel()

        Dim obj_BL_SubNiveles As New bl_SubNiveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(CInt(ddlBuscarFamiliarNivel.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 59)
        Controles.llenarCombo(ddlBuscarFamiliarSubNivel, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Grados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(CInt(ddlBuscarFamiliarSubNivel.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 59)
        Controles.llenarCombo(ddlBuscarFamiliarGrado, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Aulas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(CInt(ddlBuscarFamiliarGrado.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 59)
        Controles.llenarCombo(ddlBuscarFamiliarAula, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Se listarán las fichas médica de alumno, que coincidan con los parametros de busqueda ingresados, para su posterior validación 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
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
        Dim dt_FechaRangoInicial As Date = tbFechaSolicitudInicio.Text.Trim
        Dim dt_FechaRangoFinal As Date = tbFechaSolicitudFin.Text.Trim
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

            Dim obj_BL_FichaMedica As New bl_FichaMedicasAlumnos
            ds_Lista = obj_BL_FichaMedica.FUN_LIS_FichaMedicaActualizacion(objMaestroPersona, dt_FechaRangoInicial, dt_FechaRangoFinal, hidenCodigoPerfil.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 59)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_FichaMedica As New bl_FichaMedicasAlumnos
                ds_Lista = obj_BL_FichaMedica.FUN_LIS_FichaMedicaActualizacion(objMaestroPersona, dt_FechaRangoInicial, dt_FechaRangoFinal, hidenCodigoPerfil.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 59)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista

    End Function

    ''' <summary>
    ''' Cambia el estado de todos los checkbox del formulario
    ''' </summary>
    ''' <param name="sender">Hace referencia al checkbox general del formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CambiarCheckbox(ByVal sender As Object)

        Dim gvRows As Integer = GVActualizarFichaMedica.Rows.Count

        If CType(sender, CheckBox).Checked Then
            For Each dr As GridViewRow In GVActualizarFichaMedica.Rows
                CType(dr.FindControl("chkActualizar"), CheckBox).Checked = True
            Next
            If GVEnfermedad.Rows.Count > 0 Then
                CType(GVEnfermedad.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If
            If GVVacuna.Rows.Count > 0 Then
                CType(GVVacuna.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If
            If GVAlergia.Rows.Count > 0 Then
                CType(GVAlergia.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If
            If GVCaracteristicaPiel.Rows.Count > 0 Then
                CType(GVCaracteristicaPiel.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If
            If GVMedicamento.Rows.Count > 0 Then
                CType(GVMedicamento.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If
            If GVMotivoHospitalizacion.Rows.Count > 0 Then
                CType(GVMotivoHospitalizacion.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If
            If GVTipoOperacion.Rows.Count > 0 Then
                CType(GVTipoOperacion.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If
            If GVTipoTiposControles.Rows.Count > 0 Then
                CType(GVTipoTiposControles.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If

        Else
            For Each dr As GridViewRow In GVActualizarFichaMedica.Rows
                CType(dr.FindControl("chkActualizar"), CheckBox).Checked = False
            Next

            If GVEnfermedad.Rows.Count > 0 Then
                CType(GVEnfermedad.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If
            If GVVacuna.Rows.Count > 0 Then
                CType(GVVacuna.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If
            If GVAlergia.Rows.Count > 0 Then
                CType(GVAlergia.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If
            If GVCaracteristicaPiel.Rows.Count > 0 Then
                CType(GVCaracteristicaPiel.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If
            If GVMedicamento.Rows.Count > 0 Then
                CType(GVMedicamento.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If
            If GVMotivoHospitalizacion.Rows.Count > 0 Then
                CType(GVMotivoHospitalizacion.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If
            If GVTipoOperacion.Rows.Count > 0 Then
                CType(GVTipoOperacion.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If
            If GVTipoTiposControles.Rows.Count > 0 Then
                CType(GVTipoTiposControles.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If

        End If
    End Sub

    ''' <summary>
    ''' Consulta la información de la ficha de alumno
    ''' </summary> 
    ''' <param name="int_CodigoFichaMedica">codigo de la ficha médica del alumno al cual se va a obtener la información de su ficha médica de alumno</param>
    ''' <param name="int_CodigoSolicitud">codigo de la solicitud de ficha médica de alumno que se esta validando</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerFicha(ByVal int_CodigoFichaMedica As Integer, ByVal int_CodigoSolicitud As Integer)

        Dim str_mensajeError As String = ""

        Dim obj_BL_FichaMedica As New bl_FichaMedicasAlumnos


        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_FichaMedica.FUN_GET_FichaMedicaActualizacion(int_CodigoFichaMedica, int_CodigoSolicitud, hidenCodigoPerfil.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 59)

        GVActualizarFichaMedica.DataSource = ds_Lista.Tables(0)
        GVActualizarFichaMedica.DataBind()

        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then ' Detalle Enfermedades 
            GVEnfermedad.DataSource = ds_Lista.Tables(1)
            GVEnfermedad.DataBind()
        Else
            miTablaEnfermedad.Visible = False
        End If

        If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then ' Detalle Vacunas  
            GVVacuna.DataSource = ds_Lista.Tables(2)
            GVVacuna.DataBind()
        Else
            miTablaVacuna.Visible = False
        End If

        If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then ' Detalle Alergias  
            GVAlergia.DataSource = ds_Lista.Tables(3)
            GVAlergia.DataBind()
        Else
            miTablaAlergia.Visible = False
        End If

        If ds_Lista.Tables(4).Rows(0).Item("CodigoRelacion") <> -1 Then ' Detalle Caracteristicas de la Piel 
            GVCaracteristicaPiel.DataSource = ds_Lista.Tables(4)
            GVCaracteristicaPiel.DataBind()
        Else
            miTablaCaractPiel.Visible = False
        End If

        If ds_Lista.Tables(5).Rows(0).Item("CodigoRelacion") <> -1 Then ' Detalle Hospitalizacion    
            GVMotivoHospitalizacion.DataSource = ds_Lista.Tables(5)
            GVMotivoHospitalizacion.DataBind()
        Else
            miTablaHosp.Visible = False
        End If

        If ds_Lista.Tables(6).Rows(0).Item("CodigoRelacion") <> -1 Then ' Detalle Operación   
            GVTipoOperacion.DataSource = ds_Lista.Tables(6)
            GVTipoOperacion.DataBind()
        Else
            miTablaOperac.Visible = False
        End If

        If ds_Lista.Tables(7).Rows(0).Item("CodigoRelacion") <> -1 Then ' Detalle Otros Controles   
            GVTipoTiposControles.DataSource = ds_Lista.Tables(7)
            GVTipoTiposControles.DataBind()
        Else
            miTablaTiposControles.Visible = False
        End If


        If ds_Lista.Tables(8).Rows(0).Item("CodigoRelacion") <> -1 Then ' Detalle Medicamento  al final
            GVMedicamento.DataSource = ds_Lista.Tables(8)
            GVMedicamento.DataBind()
        Else
            miTablaMedic.Visible = False
        End If



        lblFechaSolicitud.Text = ds_Lista.Tables(10).Rows(0).Item("FechaSolicitud").ToString
        lblNombreCompletoSolicitante.Text = ds_Lista.Tables(10).Rows(0).Item("NombreCompleto").ToString
        lblParentescoSolicitante.Text = ds_Lista.Tables(10).Rows(0).Item("Parentesco").ToString
        hidenCodigoSolicitud.Value = ds_Lista.Tables(10).Rows(0).Item("CodigoSolicitud").ToString

        img_FotoUsuario.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web").ToString() & ds_Lista.Tables(9).Rows(0).Item("rutaFoto").ToString
        hd_Codigo.Value = ds_Lista.Tables(9).Rows(0).Item("CodigoAlumno").ToString
        hidenCodigoPersona.Value = ds_Lista.Tables(9).Rows(0).Item("CodigoPersona").ToString
        lblNombreAlumno.Text = ds_Lista.Tables(9).Rows(0).Item("NombreCompleto").ToString
        lblSituacionAnio.Text = ds_Lista.Tables(9).Rows(0).Item("estadoAnioActualAlumno").ToString
        lblENSnGS.Text = ds_Lista.Tables(9).Rows(0).Item("ENSnGS").ToString

        VerRegistro("Actualización")
        chkAll.Checked = False

    End Sub

    ''' <summary>
    ''' Graba los datos que se han sido validados de la ficha médica de alumno
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarFicha()

        Dim str_mensajeError As String = ""

        'Dim intCodigoFichaAlumno As String = 
        Dim intCodigoPersona As Integer = hidenCodigoPersona.Value
        Dim intCodigoSolicitud As Integer = hidenCodigoSolicitud.Value
        Dim intCodigoPerfil As Integer = hidenCodigoPerfil.Value

        Dim objDT_Cabecera As New DataTable("DatosCabecera")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "Indice", "string")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "NombreoCampo", "string")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "CodigoCampo", "string")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "ValorCampo", "string")

        If GVActualizarFichaMedica.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVActualizarFichaMedica.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    Dim dr As DataRow
                    dr = objDT_Cabecera.NewRow
                    dr.Item(0) = CType(gvr.FindControl("lblIndiceCampo"), Label).Text
                    dr.Item(1) = CType(gvr.FindControl("lblNombreCampo"), Label).Text
                    dr.Item(2) = CType(gvr.FindControl("lblCodigoActualizado"), Label).Text
                    dr.Item(3) = CType(gvr.FindControl("ValorActualizado"), Label).Text
                    objDT_Cabecera.Rows.Add(dr)
                End If
            Next
        End If

        'Detalle
        Dim objDS_Detalle As New DataSet
        Dim miBool As Boolean = False

        'Detalle Enfermedades
        Dim objDT_Enfermedad As DataTable
        Dim CodigosActualizado As String
        Dim DescActualizar As String
        Dim EdadActualizar As String

        Dim arrStrCodigoActualizar() As String
        Dim arrStrDescActualizar() As String
        Dim arrStrEdadActualizar() As String

        objDT_Enfermedad = New DataTable("ListaEnfermedad")
        'objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "CodigoRelFichaMedEnEnfermedades", "String")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "CodigoEnfermedad", "String")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "Enfermedad", "String")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "Edad", "Integer")

        Dim dr_Enfermedad As DataRow


        If GVEnfermedad.Rows.Count > 0 Then
            For Each drv As GridViewRow In GVEnfermedad.Rows
                If CType(drv.FindControl("chkActualizar"), CheckBox).Checked Then
                    'dr_Enfermedad = objDT_Enfermedad.NewRow
                    'arrStrCodigosActualizado = Split(CType(drv.FindControl("lblListaCodigoActualizado"), Label).Text, ",")
                    ' dr_Enfermedad.Item("CodigoRelFichaMedEnEnfermedades") = CType(drv.FindControl("lblListaCodigoRelacionEnf"), Label).Text
                    CodigosActualizado = CType(drv.FindControl("lblListaCodigoActualizado"), Label).Text
                    DescActualizar = CType(drv.FindControl("lblListaDescActualizado"), Label).Text
                    EdadActualizar = CType(drv.FindControl("lblListaEdadActualizado"), Label).Text
                    miBool = True
                End If

            Next

            If miBool = True Then
                arrStrCodigoActualizar = Split(CodigosActualizado, ",")
                arrStrDescActualizar = Split(DescActualizar, ",")
                arrStrEdadActualizar = Split(EdadActualizar, ",")

                For i As Integer = 0 To arrStrCodigoActualizar.Length - 1
                    dr_Enfermedad = objDT_Enfermedad.NewRow
                    dr_Enfermedad.Item("CodigoEnfermedad") = arrStrCodigoActualizar(i)
                    dr_Enfermedad.Item("Enfermedad") = arrStrDescActualizar(i)
                    dr_Enfermedad.Item("Edad") = arrStrEdadActualizar(i)
                    objDT_Enfermedad.Rows.Add(dr_Enfermedad)
                Next

            End If
            miBool = False
        End If


        'Detalle Vacuna
        Dim objDT_Vacuna As DataTable
        Dim CodigoVacActualizar As String
        Dim CodigoDosisActualizar As String
        Dim FechaVacActualizar As String

        Dim arrStrCodigoVacActualizar() As String
        Dim arrStrCodigoDosisActualizar() As String
        Dim arrStrFechaVacActualizar() As String

        objDT_Vacuna = New DataTable("ListaVacuna")
        'objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "CodigoRelVacunasFichaMed", "String")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "CodigoVacuna", "String")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "CodigoDosis", "String")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "FechaVacunacion", "Date")

        Dim dr_Vacuna As DataRow

        If GVVacuna.Rows.Count > 0 Then

            For Each drv As GridViewRow In GVVacuna.Rows
                If CType(drv.FindControl("chkActualizar"), CheckBox).Checked Then
                    'dr_Vacuna = objDT_Vacuna.NewRow
                    CodigoVacActualizar = CType(drv.FindControl("lblListaCodigoVacActualizado"), Label).Text
                    CodigoDosisActualizar = CType(drv.FindControl("lblListaCodigoDosisActualizado"), Label).Text
                    FechaVacActualizar = CType(drv.FindControl("lblListaFechaVacActualizado"), Label).Text
                    miBool = True
                End If

            Next

            If miBool = True Then
                arrStrCodigoVacActualizar = Split(CodigoVacActualizar, ",")
                arrStrCodigoDosisActualizar = Split(CodigoDosisActualizar, ",")
                arrStrFechaVacActualizar = Split(FechaVacActualizar, ",")

                For i As Integer = 0 To arrStrCodigoVacActualizar.Length - 1
                    dr_Vacuna = objDT_Vacuna.NewRow
                    dr_Vacuna.Item("CodigoVacuna") = arrStrCodigoVacActualizar(i)
                    dr_Vacuna.Item("CodigoDosis") = arrStrCodigoDosisActualizar(i)
                    dr_Vacuna.Item("FechaVacunacion") = arrStrFechaVacActualizar(i)
                    objDT_Vacuna.Rows.Add(dr_Vacuna)
                Next
            End If
            miBool = False
        End If

        'Detalle CaracteristicasPiel
        Dim objDT_CaracteristicasPiel As DataTable
        Dim CodigoCaractPielActualizar As String
        Dim CaractPielActualizar As String
        Dim FechaCaractPielActualizar As String

        Dim arrStrCodigoCaractPielActualizar() As String
        Dim arrStrCaractPielActualizar() As String
        Dim arrStrFechaCaractPielActualizar() As String

        objDT_CaracteristicasPiel = New DataTable("ListaCaracteristicasPiel")
        objDT_CaracteristicasPiel = Datos.agregarColumna(objDT_CaracteristicasPiel, "CodigoCaractPiel", "String")
        objDT_CaracteristicasPiel = Datos.agregarColumna(objDT_CaracteristicasPiel, "Caracteristicapiel", "String")
        objDT_CaracteristicasPiel = Datos.agregarColumna(objDT_CaracteristicasPiel, "FechaRegistro", "Date")

        Dim dr_CaracteristicasPiel As DataRow

        If GVCaracteristicaPiel.Rows.Count > 0 Then
            For Each drv As GridViewRow In GVCaracteristicaPiel.Rows
                If CType(drv.FindControl("chkActualizar"), CheckBox).Checked Then
                    'dr_CaracteristicasPiel = objDT_CaracteristicasPiel.NewRow
                    CodigoCaractPielActualizar = CType(drv.FindControl("lblListaCodigoCaractPielActualizado"), Label).Text
                    CaractPielActualizar = CType(drv.FindControl("lblListaCaractPielActualizado"), Label).Text
                    FechaCaractPielActualizar = CType(drv.FindControl("lblListaFechaCaractPielActualizado"), Label).Text
                    miBool = True
                End If
            Next

            If miBool = True Then
                arrStrCodigoCaractPielActualizar = Split(CodigoCaractPielActualizar, ",")
                arrStrCaractPielActualizar = Split(CaractPielActualizar, ",")
                arrStrFechaCaractPielActualizar = Split(FechaCaractPielActualizar, ",")

                For i As Integer = 0 To arrStrCodigoCaractPielActualizar.Length - 1
                    dr_CaracteristicasPiel = objDT_CaracteristicasPiel.NewRow
                    dr_CaracteristicasPiel.Item("CodigoCaractPiel") = arrStrCodigoCaractPielActualizar(i)
                    dr_CaracteristicasPiel.Item("Caracteristicapiel") = arrStrCaractPielActualizar(i)
                    dr_CaracteristicasPiel.Item("FechaRegistro") = arrStrFechaCaractPielActualizar(i)
                    objDT_CaracteristicasPiel.Rows.Add(dr_CaracteristicasPiel)
                Next
            End If
            miBool = False
        End If


        'Detalle Medicamento
        Dim objDT_Medicamento As DataTable
        Dim CodigoMedicActualizar As String
        Dim CodigoFrecActualizar As String
        Dim MedicActualizar As String
        Dim FechaMedicActualizar As String

        Dim arrStrCodigoMedicActualizar() As String
        Dim arrStrCodigoFrecActualizar() As String
        Dim arrStrMedicActualizar() As String
        Dim arrStrFechaMedicActualizar() As String

        objDT_Medicamento = New DataTable("ListaMedicamento")
        'objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CodigoRelFichaAtenMedicamentos", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CodigoMedicamento", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CodigoFrecuenciaUso", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "FechaRegistro", "Date")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "Medicamento", "String")

        Dim dr_Medicamento As DataRow

        If GVMedicamento.Rows.Count > 0 Then

            For Each drv As GridViewRow In GVMedicamento.Rows
                If CType(drv.FindControl("chkActualizar"), CheckBox).Checked Then
                    'dr_Medicamento = objDT_Medicamento.NewRow
                    CodigoMedicActualizar = CType(drv.FindControl("lblListaCodigoMedicActualizado"), Label).Text
                    CodigoFrecActualizar = CType(drv.FindControl("lblListaCodigoFrecActualizado"), Label).Text
                    MedicActualizar = CType(drv.FindControl("lblListaMedicActualizado"), Label).Text
                    FechaMedicActualizar = CType(drv.FindControl("lblListaFechaMedicActualizado"), Label).Text
                    miBool = True
                End If
            Next

            If miBool = True Then
                arrStrCodigoMedicActualizar = Split(CodigoMedicActualizar, ",")
                arrStrCodigoFrecActualizar = Split(CodigoFrecActualizar, ",")
                arrStrMedicActualizar = Split(MedicActualizar, ",")
                arrStrFechaMedicActualizar = Split(FechaMedicActualizar, ",")

                For i As Integer = 0 To arrStrCodigoMedicActualizar.Length - 1
                    dr_Medicamento = objDT_Medicamento.NewRow
                    dr_Medicamento.Item("CodigoMedicamento") = arrStrCodigoMedicActualizar(i)
                    dr_Medicamento.Item("CodigoFrecuenciaUso") = arrStrCodigoFrecActualizar(i)
                    dr_Medicamento.Item("Medicamento") = arrStrMedicActualizar(i)
                    dr_Medicamento.Item("FechaRegistro") = arrStrFechaMedicActualizar(i)
                    objDT_Medicamento.Rows.Add(dr_Medicamento)
                Next
            End If
            miBool = False
        End If

        'Detalle Hospitalizacion
        Dim objDT_Hospitalizacion As DataTable
        Dim CodigoHospActualizar As String
        Dim HospActualizar As String
        Dim FechaHospActualizar As String

        Dim arrStrCodigoHospActualizar() As String
        Dim arrStrHospActualizar() As String
        Dim arrStrFechaHospActualizar() As String

        objDT_Hospitalizacion = New DataTable("ListaHospitalizacion")
        'objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "CodigoRelFichaMedMotivoHosp", "String")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "CodigoMotivoHospitalizacion", "Integer")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "FechaHospitalizacion", "Date")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "Hospitalizacion", "String")

        Dim dr_Hospitalizacion As DataRow

        If GVEnfermedad.Rows.Count > 0 Then
            For Each drv As GridViewRow In GVMotivoHospitalizacion.Rows
                If CType(drv.FindControl("chkActualizar"), CheckBox).Checked Then
                    'dr_Hospitalizacion = objDT_Hospitalizacion.NewRow
                    CodigoHospActualizar = CType(drv.FindControl("lblListaCodigoHospActualizado"), Label).Text
                    HospActualizar = CType(drv.FindControl("lblListaHospActualizado"), Label).Text
                    FechaHospActualizar = CType(drv.FindControl("lblListaFechaHospActualizado"), Label).Text
                    miBool = True
                End If
            Next

            If miBool = True Then
                arrStrCodigoHospActualizar = Split(CodigoHospActualizar, ",")
                arrStrHospActualizar = Split(HospActualizar, ",")
                arrStrFechaHospActualizar = Split(FechaHospActualizar, ",")

                For i As Integer = 0 To arrStrCodigoHospActualizar.Length - 1
                    dr_Hospitalizacion = objDT_Hospitalizacion.NewRow
                    dr_Hospitalizacion.Item("CodigoMotivoHospitalizacion") = arrStrCodigoHospActualizar(i)
                    dr_Hospitalizacion.Item("Hospitalizacion") = arrStrHospActualizar(i)
                    dr_Hospitalizacion.Item("FechaHospitalizacion") = arrStrFechaHospActualizar(i)
                    objDT_Hospitalizacion.Rows.Add(dr_Hospitalizacion)
                Next
            End If
            miBool = False
        End If

        'Detalle Operacion
        Dim objDT_Operacion As DataTable
        Dim CodigoOperacActualizar As String
        Dim OperacActualizar As String
        Dim FechaOperacActualizar As String

        Dim arrStrCodigoOperacActualizar() As String
        Dim arrStrOperacActualizar() As String
        Dim arrStrFechaOperacActualizar() As String

        objDT_Operacion = New DataTable("ListaOperacion")
        'objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "CodigoRelFichaMedOperaciones", "String")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "CodigoTipoOperaciones", "Integer")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "FechaOperacion", "Date")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "Operacion", "String")

        Dim dr_Operacion As DataRow

        If GVTipoOperacion.Rows.Count > 0 Then
            For Each drv As GridViewRow In GVTipoOperacion.Rows
                If CType(drv.FindControl("chkActualizar"), CheckBox).Checked Then
                    'dr_Operacion = objDT_Operacion.NewRow
                    CodigoOperacActualizar = CType(drv.FindControl("lblListaCodigoOperacActualizado"), Label).Text
                    OperacActualizar = CType(drv.FindControl("lblListaOperacActualizado"), Label).Text
                    FechaOperacActualizar = CType(drv.FindControl("lblListaFechaOperacActualizado"), Label).Text
                    miBool = True
                End If

            Next

            If miBool = True Then
                arrStrCodigoOperacActualizar = Split(CodigoOperacActualizar, ",")
                arrStrOperacActualizar = Split(OperacActualizar, ",")
                arrStrFechaOperacActualizar = Split(FechaOperacActualizar, ",")

                For i As Integer = 0 To arrStrCodigoOperacActualizar.Length - 1
                    dr_Operacion = objDT_Operacion.NewRow
                    dr_Operacion.Item("CodigoTipoOperaciones") = arrStrCodigoOperacActualizar(i)
                    dr_Operacion.Item("Operacion") = arrStrOperacActualizar(i)
                    dr_Operacion.Item("FechaOperacion") = arrStrFechaOperacActualizar(i)
                    objDT_Operacion.Rows.Add(dr_Operacion)
                Next

            End If
            miBool = False
        End If

        ''Otros Controles
        Dim objDT_TipoControl As DataTable
        Dim CodigoTipControlActualizar As String
        Dim ResultadoActualizar As String
        Dim FechaTipControlActualizar As String

        Dim arrStrCodigoTipControlActualizar() As String
        Dim arrStrResultadoActualizar() As String
        Dim arrStrFechaTipControlActualizar() As String

        objDT_TipoControl = New DataTable("ListaTipoControl")
        'objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "CodigoRelFichaMedTiposControles", "String")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "CodigoTipoControl", "Integer")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "FechaControl", "Date")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "Resultado", "String")

        Dim dr_TipoControl As DataRow

        If GVTipoTiposControles.Rows.Count > 0 Then
            For Each drv As GridViewRow In GVTipoTiposControles.Rows
                If CType(drv.FindControl("chkActualizar"), CheckBox).Checked Then
                    'dr_TipoControl = objDT_TipoControl.NewRow
                    CodigoTipControlActualizar = CType(drv.FindControl("lblListaCodigoTipControlActualizado"), Label).Text
                    ResultadoActualizar = CType(drv.FindControl("lblListaResultadoActualizado"), Label).Text
                    FechaTipControlActualizar = CType(drv.FindControl("lblListaFechaTipControlActualizado"), Label).Text
                    miBool = True
                End If

            Next

            If miBool = True Then
                arrStrCodigoTipControlActualizar = Split(CodigoTipControlActualizar, ",")
                arrStrResultadoActualizar = Split(ResultadoActualizar, ",")
                arrStrFechaTipControlActualizar = Split(FechaTipControlActualizar, ",")

                For i As Integer = 0 To arrStrCodigoTipControlActualizar.Length - 1
                    dr_TipoControl = objDT_TipoControl.NewRow
                    dr_TipoControl.Item("CodigoTipoControl") = arrStrCodigoTipControlActualizar(i)
                    dr_TipoControl.Item("Resultado") = arrStrResultadoActualizar(i)
                    dr_TipoControl.Item("FechaControl") = arrStrFechaTipControlActualizar(i)
                    objDT_TipoControl.Rows.Add(dr_TipoControl)
                Next

            End If
            miBool = False
        End If


        'Alergia
        Dim objDT_Alergia As DataTable
        Dim CodigoAlergActualizar As String
        Dim FechaAlergActualizar As String

        Dim arrStrCodigoAlergActualizar() As String
        Dim arrStrFechaAlergActualizar() As String

        objDT_Alergia = New DataTable("ListaAlergia")
        'objDT_Alergia = Datos.agregarColumna(objDT_Alergia, "CodigoRelFichaMedAlergias", "Integer")
        objDT_Alergia = Datos.agregarColumna(objDT_Alergia, "CodigoAlergia", "Integer")
        objDT_Alergia = Datos.agregarColumna(objDT_Alergia, "FechaRegistro", "Date")

        Dim dr_Alergia As DataRow

        If GVAlergia.Rows.Count > 0 Then
            For Each drv As GridViewRow In GVAlergia.Rows
                If CType(drv.FindControl("chkActualizar"), CheckBox).Checked Then
                    'dr_Alergia = objDT_Alergia.NewRow
                    CodigoAlergActualizar = CType(drv.FindControl("lblListaCodigoAlergActualizado"), Label).Text
                    FechaAlergActualizar = CType(drv.FindControl("lblListaFechaAlergActualizado"), Label).Text
                    miBool = True
                End If

            Next

            If miBool = True Then
                arrStrCodigoAlergActualizar = Split(CodigoAlergActualizar, ",")
                arrStrFechaAlergActualizar = Split(FechaAlergActualizar, ",")

                For i As Integer = 0 To arrStrCodigoAlergActualizar.Length - 1
                    dr_Alergia = objDT_Alergia.NewRow
                    dr_Alergia.Item("CodigoAlergia") = arrStrCodigoAlergActualizar(i)
                    dr_Alergia.Item("FechaRegistro") = arrStrFechaAlergActualizar(i)
                    objDT_Alergia.Rows.Add(dr_Alergia)
                Next
            End If
            miBool = False

        End If

        'Agrego las DataTable a mi DataSet
        objDS_Detalle.Tables.Add(objDT_Enfermedad)
        objDS_Detalle.Tables.Add(objDT_Vacuna)
        objDS_Detalle.Tables.Add(objDT_CaracteristicasPiel)
        objDS_Detalle.Tables.Add(objDT_Medicamento)
        objDS_Detalle.Tables.Add(objDT_Hospitalizacion)
        objDS_Detalle.Tables.Add(objDT_Operacion)
        objDS_Detalle.Tables.Add(objDT_TipoControl)
        objDS_Detalle.Tables.Add(objDT_Alergia)

        Dim obj_BL_FichaMedica As New bl_FichaMedicasAlumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer = 0

        usp_valor = obj_BL_FichaMedica.FUN_UPD_FichaMedicaActualizacion(hd_Codigo.Value, intCodigoPersona, _
                    intCodigoSolicitud, intCodigoPerfil, objDT_Cabecera, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 59)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnCancelarFicha_Click()
            btnBuscar_Click()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If


    End Sub

    ''' <summary>
    ''' Regresa al formulario de busqueda de solicitudes de validacion de fichas médicas de alumnos
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CancelarFicha()

        miTab1_1.Enabled = True
        miTab2_2.Enabled = False
        lbTab2_2.Text = "Actualización"
        TabContainer1.ActiveTabIndex = 0

    End Sub

    ''' <summary>
    ''' Valida la ficha médica de alumno antes de grabar
    ''' </summary>
    ''' <param name="strMensaje">Cadena de texto que tendra todos los mensajes de error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function validarGrabar(ByRef strMensaje As String) As Boolean

        Dim miBool As Boolean = False

        For Each gvr As GridViewRow In GVActualizarFichaMedica.Rows
            If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                miBool = True
            End If
        Next

        If GVEnfermedad.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVEnfermedad.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If

        If GVVacuna.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVVacuna.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If

        If GVAlergia.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVAlergia.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If

        If GVMedicamento.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVMedicamento.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If

        If GVMotivoHospitalizacion.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVMotivoHospitalizacion.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If

        If GVTipoOperacion.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVTipoOperacion.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If

        If GVTipoTiposControles.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVTipoTiposControles.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If

        If miBool = False Then
            'strMensaje = Alertas.ObtenerAlerta(3, "") '"Debe seleccionar por lo menos 1 check de algúnn registro."
        End If

        Return miBool

    End Function

#End Region

#Region "Eventos Gridview - Actualizacion"

    Protected Sub GVActualizarFichaMedica_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblIndiceCampo As Label = e.Row.FindControl("lblIndiceCampo")
                Dim _lblNombreCampo As Label = e.Row.FindControl("lblNombreCampo")
                Dim _ValorOriginal As Label = e.Row.FindControl("ValorOriginal")
                Dim _ValorActualizado As Label = e.Row.FindControl("ValorActualizado")
                Dim _chkActualizar As CheckBox = e.Row.FindControl("chkActualizar")

                If e.Row.DataItem("CodigoBloqueInformacion") = "0" Then
                    e.Row.Attributes.Add("class", "miHiddenStyle")
                Else
                    e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                    e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                    e.Row.Attributes.Add("class", "miRowBorder")
                End If

                If e.Row.DataItem("Indice") = "0" And e.Row.DataItem("CantRegistros") > 1 Then
                    e.Row.Attributes.Add("class", "miLegendRow")
                    _chkActualizar.Visible = False
                ElseIf e.Row.DataItem("Indice") = "0" And e.Row.DataItem("CantRegistros") = 1 Then
                    e.Row.Attributes.Add("class", "miHiddenStyle")
                ElseIf e.Row.DataItem("Indice") = "0" And e.Row.DataItem("CantRegistros") = 0 Then
                    e.Row.Attributes.Add("class", "miHiddenStyle")
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVEnfermedad_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblListaCodigoOriginal As Label = e.Row.FindControl("lblListaCodigoOriginal")
                Dim _lblListaCodigoActualizado As Label = e.Row.FindControl("lblListaCodigoActualizado")
                Dim _lblListaDescOriginal As Label = e.Row.FindControl("lblListaDescOriginal")
                Dim _lblListaDescActualizado As Label = e.Row.FindControl("lblListaDescActualizado")
                Dim _lblListaEdadOriginal As Label = e.Row.FindControl("lblListaEdadOriginal")
                Dim _lblListaEdadActualizado As Label = e.Row.FindControl("lblListaEdadActualizado")

                Dim _listValorOriginal As BulletedList = e.Row.FindControl("listValorOriginal")
                Dim _listValorActualizado As BulletedList = e.Row.FindControl("listValorActualizado")

                Dim arrStrCodigosOriginal() As String
                Dim arrStrCodigosActualizar() As String
                Dim arrStrDescOriginal() As String
                Dim arrStrDescActualizar() As String
                Dim arrStrEdadOriginal() As String
                Dim arrStrEdadActualizar() As String
                Dim ListadoOriginal As String
                Dim ListadoActualizado As String

                arrStrCodigosOriginal = Split(_lblListaCodigoOriginal.Text, ",")
                arrStrDescOriginal = Split(_lblListaDescOriginal.Text, ",")
                arrStrEdadOriginal = Split(_lblListaEdadOriginal.Text, ",")
                For i As Integer = 0 To arrStrCodigosOriginal.Length - 1
                    If arrStrEdadOriginal(i).Length = 0 Then
                        _listValorOriginal.Items.Add(arrStrDescOriginal(i))
                    Else
                        ListadoOriginal = arrStrDescOriginal(i) & " / " & arrStrEdadOriginal(i) & "  años"
                        _listValorOriginal.Items.Add(ListadoOriginal)
                    End If
                Next

                arrStrCodigosActualizar = Split(_lblListaCodigoActualizado.Text, ",")
                arrStrDescActualizar = Split(_lblListaDescActualizado.Text, ",")
                arrStrEdadActualizar = Split(_lblListaEdadActualizado.Text, ",")
                For i As Integer = 0 To arrStrCodigosActualizar.Length - 1
                    If arrStrEdadActualizar(i).Length = 0 Then
                        _listValorActualizado.Items.Add(arrStrDescActualizar(i))
                    Else
                        ListadoActualizado = arrStrDescActualizar(i) & " / " & arrStrEdadActualizar(i) & "  años"
                        _listValorActualizado.Items.Add(ListadoActualizado)
                    End If
                Next

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVVacuna_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblListaCodigoVacOriginal As Label = e.Row.FindControl("lblListaCodigoVacOriginal")
                Dim _lblListaCodigoVacActualizado As Label = e.Row.FindControl("lblListaCodigoVacActualizado")
                Dim _lblListaDescVacOriginal As Label = e.Row.FindControl("lblListaDescVacOriginal")
                Dim _lblListaDescVacActualizado As Label = e.Row.FindControl("lblListaDescVacActualizado")
                Dim _lblListaFechaVacOriginal As Label = e.Row.FindControl("lblListaFechaVacOriginal")
                Dim _lblListaFechaVacActualizado As Label = e.Row.FindControl("lblListaFechaVacActualizado")
                Dim _lblListaDosisOriginal As Label = e.Row.FindControl("lblListaDosisOriginal")
                Dim _lblListaDosisActualizado As Label = e.Row.FindControl("lblListaDosisActualizado")

                Dim _listValorOriginal As BulletedList = e.Row.FindControl("listValorOriginal")
                Dim _listValorActualizado As BulletedList = e.Row.FindControl("listValorActualizado")

                Dim arrStrCodigoVacOriginal() As String
                Dim arrStrCodigoVacActualizar() As String
                Dim arrStrDescVacOriginal() As String
                Dim arrStrDescVacActualizar() As String
                Dim arrStrFechaVacOriginal() As String
                Dim arrStrFechaVacActualizar() As String
                Dim arrStrDosisOriginal() As String
                Dim arrStrDosisActualizar() As String

                Dim ListadoOriginal As String
                Dim ListadoActualizado As String

                arrStrCodigoVacOriginal = Split(_lblListaCodigoVacOriginal.Text, ",")
                arrStrDescVacOriginal = Split(_lblListaDescVacOriginal.Text, ",")
                arrStrFechaVacOriginal = Split(_lblListaFechaVacOriginal.Text, ",")
                arrStrDosisOriginal = Split(_lblListaDosisOriginal.Text, ",")
                For i As Integer = 0 To arrStrCodigoVacOriginal.Length - 1
                    If arrStrDosisOriginal(i).Length = 0 Then
                        ListadoOriginal = arrStrFechaVacOriginal(i) & " / " & arrStrDescVacOriginal(i)
                        _listValorOriginal.Items.Add(arrStrDescVacOriginal(i))
                    Else
                        ListadoOriginal = arrStrFechaVacOriginal(i) & " / " & arrStrDescVacOriginal(i) & " / " & arrStrDosisOriginal(i)
                        _listValorOriginal.Items.Add(ListadoOriginal)
                    End If
                Next

                arrStrCodigoVacActualizar = Split(_lblListaCodigoVacActualizado.Text, ",")
                arrStrDescVacActualizar = Split(_lblListaDescVacActualizado.Text, ",")
                arrStrFechaVacActualizar = Split(_lblListaFechaVacActualizado.Text, ",")
                arrStrDosisActualizar = Split(_lblListaDosisActualizado.Text, ",")

                For i As Integer = 0 To arrStrCodigoVacActualizar.Length - 1
                    If arrStrDosisActualizar(i).Length = 0 Then
                        ListadoOriginal = arrStrFechaVacActualizar(i) & " / " & arrStrDescVacActualizar(i)
                        _listValorActualizado.Items.Add(ListadoOriginal)
                    Else
                        ListadoActualizado = arrStrFechaVacActualizar(i) & " / " & arrStrDescVacActualizar(i) & " / " & arrStrDosisActualizar(i)
                        _listValorActualizado.Items.Add(ListadoActualizado)
                    End If
                Next

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVAlergia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblListaCodigoAlergOriginal As Label = e.Row.FindControl("lblListaCodigoAlergOriginal")
                Dim _lblListaCodigoAlergActualizado As Label = e.Row.FindControl("lblListaCodigoAlergActualizado")
                Dim _lblListaAlergOriginal As Label = e.Row.FindControl("lblListaAlergOriginal")
                Dim _lblListaAlergActualizado As Label = e.Row.FindControl("lblListaAlergActualizado")

                Dim _listValorOriginal As BulletedList = e.Row.FindControl("listValorOriginal")
                Dim _listValorActualizado As BulletedList = e.Row.FindControl("listValorActualizado")

                Dim arrStrCodigoAlergOriginal() As String
                Dim arrStrCodigoAlergActualizar() As String
                Dim arrStrAlergOriginal() As String
                Dim arrStrAlergActualizar() As String

                Dim ListadoOriginal As String
                Dim ListadoActualizado As String

                arrStrCodigoAlergOriginal = Split(_lblListaCodigoAlergOriginal.Text, ",")
                arrStrAlergOriginal = Split(_lblListaAlergOriginal.Text, ",")

                For i As Integer = 0 To arrStrCodigoAlergOriginal.Length - 1
                    ListadoOriginal = arrStrAlergOriginal(i)
                    _listValorOriginal.Items.Add(ListadoOriginal)
                Next

                arrStrCodigoAlergActualizar = Split(_lblListaCodigoAlergActualizado.Text, ",")
                arrStrAlergActualizar = Split(_lblListaAlergActualizado.Text, ",")

                For i As Integer = 0 To arrStrCodigoAlergActualizar.Length - 1
                    ListadoActualizado = arrStrAlergActualizar(i)
                    _listValorActualizado.Items.Add(ListadoActualizado)
                Next

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVCaracteristicaPiel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblListaCodigoCaractPielOriginal As Label = e.Row.FindControl("lblListaCodigoCaractPielOriginal")
                Dim _lblListaCodigoCaractPielActualizado As Label = e.Row.FindControl("lblListaCodigoCaractPielActualizado")
                Dim _lblListaCaractPielOriginal As Label = e.Row.FindControl("lblListaCaractPielOriginal")
                Dim _lblListaCaractPielActualizado As Label = e.Row.FindControl("lblListaCaractPielActualizado")

                Dim _listValorOriginal As BulletedList = e.Row.FindControl("listValorOriginal")
                Dim _listValorActualizado As BulletedList = e.Row.FindControl("listValorActualizado")

                Dim arrStrCodigoCaractPielOriginal() As String
                Dim arrStrCodigoCaractPielActualizar() As String
                Dim arrStrCaractPielOriginal() As String
                Dim arrStrCaractPielActualizar() As String

                arrStrCodigoCaractPielOriginal = Split(_lblListaCodigoCaractPielOriginal.Text, ",")
                arrStrCaractPielOriginal = Split(_lblListaCaractPielOriginal.Text, ",")

                For i As Integer = 0 To arrStrCodigoCaractPielOriginal.Length - 1
                    _listValorOriginal.Items.Add(arrStrCaractPielOriginal(i))
                Next

                arrStrCodigoCaractPielActualizar = Split(_lblListaCodigoCaractPielActualizado.Text, ",")
                arrStrCaractPielActualizar = Split(_lblListaCaractPielActualizado.Text, ",")

                For i As Integer = 0 To arrStrCodigoCaractPielActualizar.Length - 1
                    _listValorActualizado.Items.Add(arrStrCaractPielActualizar(i))
                Next

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVMedicamento_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblListaCodigoMedicOriginal As Label = e.Row.FindControl("lblListaCodigoMedicOriginal")
                Dim _lblListaCodigoMedicActualizado As Label = e.Row.FindControl("lblListaCodigoMedicActualizado")
                Dim _lblListaMedicOriginal As Label = e.Row.FindControl("lblListaMedicOriginal")
                Dim _lblListaMedicActualizado As Label = e.Row.FindControl("lblListaMedicActualizado")
                Dim _lblListaFrecOriginal As Label = e.Row.FindControl("lblListaFrecOriginal")
                Dim _lblListaFrecActualizado As Label = e.Row.FindControl("lblListaFrecActualizado")

                Dim _listValorOriginal As BulletedList = e.Row.FindControl("listValorOriginal")
                Dim _listValorActualizado As BulletedList = e.Row.FindControl("listValorActualizado")

                Dim arrStrCodigoMedicOriginal() As String
                Dim arrStrCodigoMedicActualizar() As String
                Dim arrStrMedicOriginal() As String
                Dim arrStrMedicActualizar() As String
                Dim arrStrFrecOriginal() As String
                Dim arrStrFrecActualizar() As String


                Dim ListadoOriginal As String
                Dim ListadoActualizado As String

                arrStrCodigoMedicOriginal = Split(_lblListaCodigoMedicOriginal.Text, ",")
                arrStrMedicOriginal = Split(_lblListaMedicOriginal.Text, ",")
                arrStrFrecOriginal = Split(_lblListaFrecOriginal.Text, ",")

                For i As Integer = 0 To arrStrCodigoMedicOriginal.Length - 1
                    If arrStrFrecOriginal(i).Length = 0 Then
                        _listValorOriginal.Items.Add(arrStrMedicOriginal(i))
                    Else
                        ListadoOriginal = arrStrMedicOriginal(i) & " / " & arrStrFrecOriginal(i)
                        _listValorOriginal.Items.Add(ListadoOriginal)
                    End If
                Next

                arrStrCodigoMedicActualizar = Split(_lblListaCodigoMedicActualizado.Text, ",")
                arrStrMedicActualizar = Split(_lblListaMedicActualizado.Text, ",")
                arrStrFrecActualizar = Split(_lblListaFrecActualizado.Text, ",")

                For i As Integer = 0 To arrStrCodigoMedicActualizar.Length - 1
                    If arrStrFrecActualizar(i).Length = 0 Then
                        _listValorActualizado.Items.Add(arrStrMedicActualizar(i))
                    Else
                        ListadoActualizado = arrStrMedicActualizar(i) & " / " & arrStrFrecActualizar(i)
                        _listValorActualizado.Items.Add(ListadoActualizado)
                    End If
                Next

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVMotivoHospitalizacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblListaCodigoHospOriginal As Label = e.Row.FindControl("lblListaCodigoHospOriginal")
                Dim _lblListaCodigoHospActualizado As Label = e.Row.FindControl("lblListaCodigoHospActualizado")
                Dim _lblListaHospOriginal As Label = e.Row.FindControl("lblListaHospOriginal")
                Dim _lblListaHospActualizado As Label = e.Row.FindControl("lblListaHospActualizado")

                Dim _listValorOriginal As BulletedList = e.Row.FindControl("listValorOriginal")
                Dim _listValorActualizado As BulletedList = e.Row.FindControl("listValorActualizado")

                Dim arrStrCodigoHospOriginal() As String
                Dim arrStrCodigoHospActualizar() As String
                Dim arrStrHospOriginal() As String
                Dim arrStrHospActualizar() As String

                Dim ListadoOriginal As String
                Dim ListadoActualizado As String

                arrStrCodigoHospOriginal = Split(_lblListaCodigoHospOriginal.Text, ",")
                arrStrHospOriginal = Split(_lblListaHospOriginal.Text, ",")

                For i As Integer = 0 To arrStrCodigoHospOriginal.Length - 1
                    ListadoOriginal = arrStrHospOriginal(i)
                    _listValorOriginal.Items.Add(ListadoOriginal)
                Next

                arrStrCodigoHospActualizar = Split(_lblListaCodigoHospActualizado.Text, ",")
                arrStrHospActualizar = Split(_lblListaHospActualizado.Text, ",")

                For i As Integer = 0 To arrStrCodigoHospActualizar.Length - 1
                    ListadoActualizado = arrStrHospActualizar(i)
                    _listValorActualizado.Items.Add(ListadoActualizado)
                Next

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVTipoOperacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblListaCodigoOperacOriginal As Label = e.Row.FindControl("lblListaCodigoOperacOriginal")
                Dim _lblListaCodigoOperacActualizado As Label = e.Row.FindControl("lblListaCodigoOperacActualizado")
                Dim _lblListaOperacOriginal As Label = e.Row.FindControl("lblListaOperacOriginal")
                Dim _lblListaOperacActualizado As Label = e.Row.FindControl("lblListaOperacActualizado")

                Dim _listValorOriginal As BulletedList = e.Row.FindControl("listValorOriginal")
                Dim _listValorActualizado As BulletedList = e.Row.FindControl("listValorActualizado")

                Dim arrStrCodigoOperacOriginal() As String
                Dim arrStrCodigoOperacActualizar() As String
                Dim arrStrOperacOriginal() As String
                Dim arrStrOperacActualizar() As String

                Dim ListadoOriginal As String
                Dim ListadoActualizado As String

                arrStrCodigoOperacOriginal = Split(_lblListaCodigoOperacOriginal.Text, ",")
                arrStrOperacOriginal = Split(_lblListaOperacOriginal.Text, ",")

                For i As Integer = 0 To arrStrCodigoOperacOriginal.Length - 1
                    ListadoOriginal = arrStrOperacOriginal(i)
                    _listValorOriginal.Items.Add(ListadoOriginal)
                Next

                arrStrCodigoOperacActualizar = Split(_lblListaCodigoOperacActualizado.Text, ",")
                arrStrOperacActualizar = Split(_lblListaOperacActualizado.Text, ",")

                For i As Integer = 0 To arrStrCodigoOperacActualizar.Length - 1
                    ListadoActualizado = arrStrOperacActualizar(i)
                    _listValorActualizado.Items.Add(ListadoActualizado)
                Next

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVTipoTiposControles_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblListaCodigoTipControlOriginal As Label = e.Row.FindControl("lblListaCodigoTipControlOriginal")
                Dim _lblListaCodigoTipControlActualizado As Label = e.Row.FindControl("lblListaCodigoTipControlActualizado")
                Dim _lblListaTipControlOriginal As Label = e.Row.FindControl("lblListaTipControlOriginal")
                Dim _lblListaTipControlActualizado As Label = e.Row.FindControl("lblListaTipControlActualizado")
                Dim _lblListaResultadoOriginal As Label = e.Row.FindControl("lblListaResultadoOriginal")
                Dim _lblListaResultadoActualizado As Label = e.Row.FindControl("lblListaResultadoActualizado")

                Dim _listValorOriginal As BulletedList = e.Row.FindControl("listValorOriginal")
                Dim _listValorActualizado As BulletedList = e.Row.FindControl("listValorActualizado")

                Dim arrStrCodigoTipControlOriginal() As String
                Dim arrStrCodigoTipControlActualizar() As String
                Dim arrStrTipControlOriginal() As String
                Dim arrStrTipControlActualizar() As String
                Dim arrStrResultadoOriginal() As String
                Dim arrStrResultadoActualizar() As String
                Dim ListadoOriginal As String
                Dim ListadoActualizado As String

                arrStrCodigoTipControlOriginal = Split(_lblListaCodigoTipControlOriginal.Text, ",")
                arrStrTipControlOriginal = Split(_lblListaTipControlOriginal.Text, ",")
                arrStrResultadoOriginal = Split(_lblListaResultadoOriginal.Text, ",")

                For i As Integer = 0 To arrStrCodigoTipControlOriginal.Length - 1
                    If arrStrResultadoOriginal(i).Length = 0 Then
                        ListadoOriginal = arrStrTipControlOriginal(i)
                        _listValorOriginal.Items.Add(ListadoOriginal)
                    Else
                        ListadoOriginal = arrStrTipControlOriginal(i) & " / " & arrStrResultadoOriginal(i)
                        _listValorOriginal.Items.Add(ListadoOriginal)
                    End If
                Next

                arrStrCodigoTipControlActualizar = Split(_lblListaCodigoTipControlActualizado.Text, ",")
                arrStrTipControlActualizar = Split(_lblListaTipControlActualizado.Text, ",")
                arrStrResultadoActualizar = Split(_lblListaResultadoActualizado.Text, ",")
                For i As Integer = 0 To arrStrCodigoTipControlActualizar.Length - 1
                    If arrStrResultadoActualizar(i).Length = 0 Then
                        _listValorActualizado.Items.Add(arrStrTipControlActualizar(i))
                    Else
                        ListadoActualizado = arrStrTipControlActualizar(i) & " / " & arrStrResultadoActualizar(i)
                        _listValorActualizado.Items.Add(ListadoActualizado)
                    End If
                Next

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Eventos Gridview - Busqueda"

    Protected Sub GVListaFamiliar_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Then

                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Seleccionar" Then
                    int_CodigoAccion = 5
                    ObtenerFicha(codigo, CInt(CType(row.FindControl("lblCodigoSolicitud"), Label).Text))

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaFamiliar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnSeleccionar As ImageButton = e.Row.FindControl("btnSeleccionar")

            If e.Row.RowType = DataControlRowType.Pager Then
                Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
                _TotalPags.Text = GVListaFamiliar.PageCount.ToString

                Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
                _Registros.Text = InformacionPager(GVListaFamiliar, e.Row, Me)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.DataItem("Estado") = "Pendiente" Then
                    btnSeleccionar.Visible = True
                Else
                    btnSeleccionar.Visible = False
                End If

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

    Protected Sub GVListaFamiliar_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Pager Then
                CrearBotonesPager(GVListaFamiliar, e.Row, Me)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
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
            EnvioEmailError(0, ex.ToString)
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
    ''' Fecha de Creación:     26/01/2011
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
    ''' Fecha de Creación:     26/01/2011
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
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_NombreCompleto"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_Fecha"), ImageButton)
        Dim _btnSorting_d3 As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_Estado"), ImageButton)

        If _btnSorting.ID = _btnSorting_d1.ID Then

            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d2.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"

        Else

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"

        End If

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
    ''' Fecha de Creación:     26/01/2011
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
    ''' Fecha de Creación:     26/01/2011
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
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(1, 59, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region

End Class
