Imports System.Security.Cryptography
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

Partial Class Interfaz_Familia_Modulo_SolicitudesActualizacionInformacion_SolicitudFichaMedicaAlumno
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 0
    Private cod_Opcion As Integer = 13
    ' actualizado 20/01/2012
#Region "Pagina"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Page.IsPostBack Then

                'btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                btnGrabar.Attributes.Add("OnClick", "return confirm_grabar();")

                'mv_GrupoInformacion.ActiveViewIndex = 0
                'BlanquearBotones()
                'Dim int_Contador As Integer = 0
                'Dim buton_Opcion As Button

                'While int_Contador <= dgv_GrupoInformacion.Rows.Count - 1

                '    If int_Contador = 0 Then
                '        buton_Opcion = dgv_GrupoInformacion.Rows(int_Contador).FindControl("btn_GrupoInformacion")
                '        buton_Opcion.Style.Value = "cursor:pointer;background: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/menu/grupoInformacion_itemMenu_seleccionado.jpg') right no-repeat; "

                '    End If
                '    int_Contador = int_Contador + 1
                'End While

                SetearAccionesAcceso()
                'cargar_MenuInformacion()

                cargarCombos()
                ViewState("SortExpression") = "NombreCompleto"
                ViewState("Direccion") = "ASC"

                If Not Request.QueryString("codigoAlumno") Is Nothing Then
                    Dim str_CodigoAlumno As String = Request.QueryString("codigoAlumno")
                    obtener(str_CodigoAlumno)

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

                Else
                    MostrarSexyAlertBox("Debe seleccionar un Alumno", "Alert")
                End If

                'obtener("20100023")


            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    'Private Sub cargar_MenuInformacion()
    '    Dim dt As DataTable
    '    Dim cont_grupo As Integer

    '    dt = New DataTable("GruposInformacion")
    '    dt = Datos.agregarColumna(dt, "CodigoGrupo", "Integer")
    '    dt = Datos.agregarColumna(dt, "NombreGrupo", "String")
    '    dt = Datos.agregarColumna(dt, "CodigoProgramacion", "String")

    '    Dim dr As DataRow
    '    dr = dt.NewRow
    '    dr.Item("CodigoGrupo") = 1
    '    dr.Item("NombreGrupo") = "Generales"
    '    dr.Item("CodigoProgramacion") = "Bloque_DatosAlumno"
    '    dt.Rows.Add(dr)

    '    Dim dr_2 As DataRow
    '    dr_2 = dt.NewRow
    '    dr_2.Item("CodigoGrupo") = 2
    '    dr_2.Item("NombreGrupo") = "Desarrollo Infantil"
    '    dr_2.Item("CodigoProgramacion") = "Bloque_DesarrolloInfantil"
    '    dt.Rows.Add(dr_2)

    '    Dim dr_3 As DataRow
    '    dr_3 = dt.NewRow
    '    dr_3.Item("CodigoGrupo") = 3
    '    dr_3.Item("NombreGrupo") = "Estado Salud"
    '    dr_3.Item("CodigoProgramacion") = "Bloque_EstadoSalud"
    '    dt.Rows.Add(dr_3)

    '    Dim dr_4 As DataRow
    '    dr_4 = dt.NewRow
    '    dr_4.Item("CodigoGrupo") = 4
    '    dr_4.Item("NombreGrupo") = "Datos Medicos"
    '    dr_4.Item("CodigoProgramacion") = "Bloque_OtrosDatosMedicos"
    '    dt.Rows.Add(dr_4)

    '    Dim dr_5 As DataRow
    '    dr_5 = dt.NewRow
    '    dr_5.Item("CodigoGrupo") = 5
    '    dr_5.Item("NombreGrupo") = "Controles"
    '    dr_5.Item("CodigoProgramacion") = "Bloque_ControlSalud"
    '    dt.Rows.Add(dr_5)

    '    dgv_GrupoInformacion.DataSource = dt
    '    dgv_GrupoInformacion.DataBind()

    'End Sub

    'Protected Sub Mostrar_GrupoInformacion(ByVal sender As Object, _
    '      ByVal e As MenuEventArgs)

    '    'Dim i As Integer
    '    'Dim entro As Boolean = False

    '    'For i = 0 To mn_GrupoInformacion.Items.Count - 1
    '    '    If i = e.Item.Value Then

    '    '        If entro = False Then

    '    '            'Image1.ImageUrl = "/Web/Imagenes/fondopanelarriba.gif"
    '    '            entro = True
    '    '        End If

    '    '    Else

    '    '        If entro = False Then
    '    '            'Image1.ImageUrl = "/Web/Imagenes/fondopanelarriba2e.gif"
    '    '            'llamarImpresionPrevia()
    '    '            entro = True
    '    '        End If

    '    '    End If
    '    'Next
    'End Sub

    Protected Sub dgv_GrupoInformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

    End Sub

    'Protected Sub btn_GrupoInformacion_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim cont_views As Integer = 0
    '    Dim str_codigoProgramacionID As String = ""
    '    Dim int_IndiceVista As Integer = -1
    '    Dim btn_Boton As Button = DirectCast(sender, System.Web.UI.WebControls.Button)
    '    BlanquearBotones()
    '    btn_Boton.Style.Value = "cursor:pointer;background: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/menu/grupoInformacion_itemMenu_seleccionado.jpg') right no-repeat; "

    '    str_codigoProgramacionID = sender.ValidationGroup

    '    While cont_views <= mv_GrupoInformacion.Views.Count - 1

    '        If mv_GrupoInformacion.Views(cont_views).ID.ToString = str_codigoProgramacionID Then
    '            mv_GrupoInformacion.ActiveViewIndex = cont_views

    '            If lbl_Bloque_DatosAlumno.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_DatosAlumno.Text = sender.text
    '            End If

    '            If lbl_Bloque_DesarrolloInfantil.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_DesarrolloInfantil.Text = sender.text
    '            End If

    '            If lbl_Bloque_EstadoSalud.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_EstadoSalud.Text = sender.text
    '            End If

    '            If lbl_Bloque_OtrosDatosMedicos.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_OtrosDatosMedicos.Text = sender.text
    '            End If

    '            If lbl_Bloque_ControlSalud.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_ControlSalud.Text = sender.text
    '            End If

    '            Exit Sub
    '        End If

    '        cont_views = cont_views + 1
    '    End While

    'End Sub

    Private Sub cargarComboEdades()

        Controles.llenarComboNumerico(ddlEdadLevCabeza, 0, 6, False, False)
        Controles.llenarComboNumerico(ddlEdadSento, 0, 6, False, False)
        Controles.llenarComboNumerico(ddlEdadParo, 0, 6, False, False)
        Controles.llenarComboNumerico(ddlEdadCamino, 0, 6, False, False)
        Controles.llenarComboNumerico(ddlEdadControloEsfinteres, 0, 6, False, False)
        Controles.llenarComboNumerico(ddlEdadHabloPrimerasPalabras, 0, 6, False, False)
        Controles.llenarComboNumerico(ddlEdadHabloFluidez, 0, 6, False, False)

    End Sub

    Private Sub cargarComboMeses()

        Controles.llenarComboNumerico(ddlMesesLevCabeza, 1, 11, False, False)
        Controles.llenarComboNumerico(ddlMesesSento, 1, 11, False, False)
        Controles.llenarComboNumerico(ddlMesesParo, 1, 11, False, False)
        Controles.llenarComboNumerico(ddlMesesCamino, 1, 11, False, False)
        Controles.llenarComboNumerico(ddlMesesControloEsfinteres, 1, 11, False, False)
        Controles.llenarComboNumerico(ddlMesesHabloPrimerasPalabras, 1, 11, False, False)
        Controles.llenarComboNumerico(ddlMesesHabloFluidez, 1, 11, False, False)

    End Sub

#End Region

#Region "Eventos"

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
    End Sub

    Protected Sub ddlTipoAlergia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        cargarComboAlergia()
        pnModalAlergia.Show()
    End Sub

#End Region

#Region "Metodos"

    Private Sub SetearAccionesAcceso()
        RegistrarAccesoPagina(5, 82)
        'CONTROLES DEL FORMULARIO

        'Master.BloqueoControles(btnBuscar, 1)
        'Master.BloqueoControles(btnGrabar, 1)

        ''Master.SeteoPermisosAcciones(btnBuscar, 1)
        'Master.SeteoPermisosAcciones(btnGrabar, 1)

        ''GRUPOS DE INFORMACION
        'Master.BloqueoControles(Bloque_ControlSalud, 2)
        'Master.BloqueoControles(Bloque_DesarrolloInfantil, 2)
        'Master.BloqueoControles(Bloque_DatosAlumno, 2)
        'Master.BloqueoControles(Bloque_EstadoSalud, 2)
        'Master.BloqueoControles(Bloque_OtrosDatosMedicos, 2)

        'Master.SeteoBloquesInformacion(Bloque_ControlSalud, 1)
        'Master.SeteoBloquesInformacion(Bloque_DesarrolloInfantil, 1)
        'Master.SeteoBloquesInformacion(Bloque_DatosAlumno, 1)
        'Master.SeteoBloquesInformacion(Bloque_EstadoSalud, 1)
        'Master.SeteoBloquesInformacion(Bloque_OtrosDatosMedicos, 1)

    End Sub

#Region "Carga Combos"

    ''' <summary>
    ''' Llamada de métodos de los combobox y seteo de las fechas de los formularios.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombos()

        cargarComboEnfermedad()
        cargarComboVacuna()
        cargarComboDosisVacuna()
        cargarComboTipoNacimiento()
        cargarComboTipoSangre()
        tbFechaVacunacion.Text = Now.Date

        tbFechaControlPesoTalla.Text = Now.Date
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

        cargarComboEdades()
        cargarComboMeses()

    End Sub

    ''' <summary>
    ''' Método que Limpia los comboBox.
    ''' </summary>
    ''' <param name="combo">Nombre de Combo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)

        Controles.limpiarCombo(combo, True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Enfermedades disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboEnfermedad()

        Dim obj_BL_Enfermedad As New bl_Enfermedades
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_Enfermedad.FUN_LIS_Enfermedad("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlEnfermedad, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Vacunas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboVacuna()

        Dim obj_BL_Vacuna As New bl_Vacunas
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_Vacuna.FUN_LIS_Vacuna("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlTipoVacuna, ds_Lista, "Codigo", "Descripcion", False, True)

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
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_BL_TiposAlergias.FUN_LIS_TipoAlergia("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlTipoAlergia, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Dosis disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDosisVacuna()

        Dim obj_BL_DosisVacuna As New bl_DosisVacunas
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_DosisVacuna.FUN_LIS_DosisVacuna("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlDosis, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Medicamentos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboMedicamento()

        Dim obj_BL_Medicamentos As New bl_Medicamentos
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_Medicamentos.FUN_LIS_Medicamento(0, 0, 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlMedicamento, ds_Lista, "Codigo", "NombreCompleto", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Frecuencias de Uso disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
 

    ''' <summary>
    ''' Carga el combo con la lista de Alergias disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAlergia()

        Dim obj_BL_Alergia As New bl_Alergias
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_Alergia.FUN_LIS_Alergia("", ddlTipoAlergia.SelectedValue, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAlergia, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Caracteristicas de la Piel disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboCaracteristicasPiel()

        Dim obj_BL_TiposCaracteristicasPiel As New bl_TiposCaracteristicasPiel
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_TiposCaracteristicasPiel.FUN_LIS_TipoCaracteristicaPiel("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlCaracteristicaPiel, ds_Lista, "Codigo", "Descripcion", False, True)

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
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_BL_MedicamentosAlumnos.FUN_LIS_MedicamentosAlumnos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
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
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_BL_PresentacionMedicamento.FUN_LIS_PresentacionMedicamento("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlPresentacion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Hospitalizaciones disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboMotivosHospitalizaciones()

        Dim obj_BL_MotivosHospitalizaciones As New bl_MotivosHospitalizaciones
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_MotivosHospitalizaciones.FUN_LIS_MotivoHospitalizacion("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlHospitalizacion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Operaciones Médicas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTiposOperacionesMedicas()

        Dim obj_BL_TiposOperacionesMedicas As New bl_TiposOperacionesMedicas
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_TiposOperacionesMedicas.FUN_LIS_TipoOperacionMedica("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlOperacion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipos Controles disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTiposControles()

        Dim obj_BL_TiposControles As New bl_TiposControles
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_TiposControles.FUN_LIS_TipoControl("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlTipoControl, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipos de Nacimiento disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoNacimiento()

        Dim obj_BL_TipoNacimiento As New bl_TiposNacimientos
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_TipoNacimiento.FUN_LIS_TipoNacimiento("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlTipoNacimiento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipos de Sangre disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoSangre()

        Dim obj_BL_TipoSangre As New bl_TiposSangre
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim ds_Lista As DataSet = obj_BL_TipoSangre.FUN_LIS_TipoSangre("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlTipoSangre, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

#End Region

    ''' <summary>
    ''' Elimina las listas en memoria(ViewState) y cierra el formulario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Cancelar()

        ViewState.Remove("ListaEnfermedad")
        ViewState.Remove("ListaVacuna")
        ViewState.Remove("ListaAlergia")
        ViewState.Remove("ListaCaracteristicasPiel")
        ViewState.Remove("ListaMedicamentos")
        ViewState.Remove("ListaHospitalizacion")
        ViewState.Remove("ListaOperacion")
        ViewState.Remove("ListaControlPesoTalla")
        ViewState.Remove("ListaTipoControl")

    End Sub

    ''' <summary>
    ''' Valida la ficha del familiar antes de grabar
    ''' </summary>
    ''' <param name="str_Mensaje">Cadena de texto que tendra todos los mensajes de error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarFicha(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""
        Dim gradoActual As Integer = 0

        gradoActual = hd_GradoActual.Value

        If gradoActual = 1 Or gradoActual = 2 Or gradoActual = 3 Then
            If ddlTipoNacimiento.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Nacimiento")
                result = False
            End If

            If ddlEdadLevCabeza.SelectedValue = 0 And ddlMesesLevCabeza.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "¿A qué edad levantó la cabeza?")
                result = False
            End If

            If ddlEdadSento.SelectedValue = 0 And ddlMesesSento.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "¿A qué edad se sentó?")
                result = False
            End If

            If ddlEdadParo.SelectedValue = 0 And ddlMesesParo.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "¿A qué edad se paró?")
                result = False
            End If

            If ddlEdadCamino.SelectedValue = 0 And ddlMesesCamino.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "¿A qué edad caminó?")
                result = False
            End If

            If ddlEdadControloEsfinteres.SelectedValue = 0 And ddlMesesControloEsfinteres.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "¿A qué edad controló sus esfínteres?")
                result = False
            End If

            If ddlEdadHabloPrimerasPalabras.SelectedValue = 0 And ddlMesesHabloPrimerasPalabras.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "¿A que edad pronunció las primeras palabras?")
                result = False
            End If

            If ddlEdadHabloFluidez.SelectedValue = 0 And ddlMesesHabloFluidez.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "¿A que edad se comunicó con fluídez?")
                result = False
            End If

            If ddlTipoSangre.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Sangre")
                result = False
            End If

        End If

        If ddlTipoSangre.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Sangre")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Verifica si el codigo enviado ya existe en el arreglo
    ''' </summary>
    ''' <param name="arrList">Arreglo de códigos</param>
    ''' <param name="item">Código a buscar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
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
    ''' Fecha de Creación:     31/01/2011
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
    ''' Graba Solicitud de Actualziación de ficha Médica de Alumno
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarFicha()

        Dim obj_BE_FichaMedica As New be_FichaMedica
        Dim obj_BL_FichaMedica As New bl_FichaMedicasAlumnos
        Dim ojb_BE_SolicitudActualizacionFichaMedicaAlumno As New be_SolicitudActualizacionFichaMedicaAlumno

        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()

        'Dataset que contiene los valores originales provenientes de la base de datos
        Dim ds_Lista As DataSet
        ds_Lista = ViewState("DatosOriginalesFMA")

        'Variable para guardar los codigos de los perfiles que realizan actualizacion
        Dim str_Perfiles As String = ""
        Dim arrBloques As New ArrayList

        'Variable para comparar los detalles
        Dim miDetalle As Integer = 0

        'Variable que controla si se graba el registro o no
        Dim BoolGrabar As Boolean = False

        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer = 0

        'Datos Generales
        ojb_BE_SolicitudActualizacionFichaMedicaAlumno.CodigoPeronsaSolicitante = hd_CodigoPersonaSolicitante.Value
        obj_BE_FichaMedica.CodigoAlumno = hd_Codigo.Value

        'Desarrollo Infantil
        If ds_Lista.Tables(0).Rows(0).Item("CodigoNacimiento").ToString = ddlTipoNacimiento.SelectedValue Then

            obj_BE_FichaMedica.CodigoTipoNacimiento = -1
        Else
            obj_BE_FichaMedica.CodigoTipoNacimiento = ddlTipoNacimiento.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoNacimiento").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("Observacion").ToString = tbObservaciones.Text.Trim Then
            obj_BE_FichaMedica.TipoNacimientoObservaciones = Nothing
        Else
            obj_BE_FichaMedica.TipoNacimientoObservaciones = tbObservaciones.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_Observacion").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("EdadLevantoCabeza").ToString = ddlEdadLevCabeza.SelectedValue Then
            obj_BE_FichaMedica.EdadLevantoCabeza = -1
        Else
            obj_BE_FichaMedica.EdadLevantoCabeza = ddlEdadLevCabeza.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_EdadLevantoCabeza").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString = ddlMesesLevCabeza.SelectedValue Then
            obj_BE_FichaMedica.MesesLevantoCabeza = -1
        Else
            obj_BE_FichaMedica.MesesLevantoCabeza = ddlMesesLevCabeza.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_MesesLevantoCabeza").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("EdadSento").ToString = ddlEdadSento.SelectedValue Then
            obj_BE_FichaMedica.EdadSento = -1
        Else
            obj_BE_FichaMedica.EdadSento = ddlEdadSento.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_EdadSento").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("MesesSento").ToString = ddlMesesSento.SelectedValue Then
            obj_BE_FichaMedica.MesesSento = -1
        Else
            obj_BE_FichaMedica.MesesSento = ddlMesesSento.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_MesesSento").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("EdadParo").ToString = ddlEdadParo.SelectedValue Then
            obj_BE_FichaMedica.EdadParo = -1
        Else
            obj_BE_FichaMedica.EdadParo = ddlEdadParo.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_EdadParo").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("MesesParo").ToString = ddlMesesParo.SelectedValue Then
            obj_BE_FichaMedica.MesesParo = -1
        Else
            obj_BE_FichaMedica.MesesParo = ddlMesesParo.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_MesesParo").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("EdadCamino").ToString = ddlEdadCamino.SelectedValue Then
            obj_BE_FichaMedica.EdadCamino = -1
        Else
            obj_BE_FichaMedica.EdadCamino = ddlEdadCamino.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_EdadCamino").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("MesesCamino").ToString = ddlMesesCamino.SelectedValue Then
            obj_BE_FichaMedica.MesesCamino = -1
        Else
            obj_BE_FichaMedica.MesesCamino = ddlMesesCamino.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_MesesCamino").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString = ddlEdadControloEsfinteres.SelectedValue Then
            obj_BE_FichaMedica.EdadControloEsfinteres = -1
        Else
            obj_BE_FichaMedica.EdadControloEsfinteres = ddlEdadControloEsfinteres.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_EdadControloEsfinteres").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString = ddlMesesControloEsfinteres.SelectedValue Then
            obj_BE_FichaMedica.MesesControloEsfinteres = -1
        Else
            obj_BE_FichaMedica.MesesControloEsfinteres = ddlMesesControloEsfinteres.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_MesesControloEsfinteres").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString = ddlEdadHabloPrimerasPalabras.SelectedValue Then
            obj_BE_FichaMedica.EdadHabloPrimerasPalabras = -1
        Else
            obj_BE_FichaMedica.EdadHabloPrimerasPalabras = ddlEdadHabloPrimerasPalabras.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_EdadHabloPrimerasPalabras").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString = ddlMesesHabloPrimerasPalabras.SelectedValue Then
            obj_BE_FichaMedica.MesesHabloPrimerasPalabras = -1
        Else
            obj_BE_FichaMedica.MesesHabloPrimerasPalabras = ddlMesesHabloPrimerasPalabras.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_MesesHabloPrimerasPalabras").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString = ddlEdadHabloFluidez.SelectedValue Then
            obj_BE_FichaMedica.EdadHabloFluidez = -1
        Else
            obj_BE_FichaMedica.EdadHabloFluidez = ddlEdadHabloFluidez.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_EdadHabloFluidez").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString = ddlMesesHabloFluidez.SelectedValue Then
            obj_BE_FichaMedica.MesesHabloFluidez = -1
        Else
            obj_BE_FichaMedica.MesesHabloFluidez = ddlMesesHabloFluidez.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_MesesHabloFluidez").ToString))
        End If

        'Estado de Salud
        If ds_Lista.Tables(0).Rows(0).Item("CodigoTipoSangre").ToString = ddlTipoSangre.SelectedValue Then
            obj_BE_FichaMedica.CodigoTipoSangre = -1
        Else
            obj_BE_FichaMedica.CodigoTipoSangre = ddlTipoSangre.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoTipoSangre").ToString))
        End If

        'Otros Datos medicos
        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("TabiqueDesviado")) = rbTabiqueDesviado.SelectedValue Then
            obj_BE_FichaMedica.TabiqueDesviado = -1
        Else
            obj_BE_FichaMedica.TabiqueDesviado = rbTabiqueDesviado.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_TabiqueDesviado").ToString))
        End If

        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("SangradoNasal")) = rbSangradoNasal.SelectedValue Then
            obj_BE_FichaMedica.SangradoNasal = -1
        Else
            obj_BE_FichaMedica.SangradoNasal = rbSangradoNasal.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_SangradoNasal").ToString))
        End If

        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("UsaLentes")) = rbUsaLentes.SelectedValue Then
            obj_BE_FichaMedica.UsaLentes = -1
        Else
            obj_BE_FichaMedica.UsaLentes = rbUsaLentes.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_UsaLentes").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("ObservacionesOftalmologicas").ToString = tbObservacionesOftalmologicas.Text.Trim Then
            obj_BE_FichaMedica.ObservacionesOftalmologicas = Nothing
        Else
            obj_BE_FichaMedica.ObservacionesOftalmologicas = tbObservacionesOftalmologicas.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ObservacionesOftalmologicas").ToString))
        End If

        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("UsaOrtodoncia")) = rbUsaOrtodoncia.SelectedValue Then
            obj_BE_FichaMedica.UsaOrtodoncia = -1
        Else
            obj_BE_FichaMedica.UsaOrtodoncia = rbUsaOrtodoncia.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_UsaOrtodoncia").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("ObservacionesDental").ToString = tbObservacionesDental.Text.Trim Then
            obj_BE_FichaMedica.ObservacionesDental = Nothing
        Else
            obj_BE_FichaMedica.ObservacionesDental = tbObservacionesDental.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ObservacionesDental").ToString))
        End If

        'Detalle
        Dim objDS_Detalle As New DataSet

        'Detalle Enfermedades
        Dim objDT_Enfermedad As DataTable

        objDT_Enfermedad = New DataTable("ListaEnfermedad")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "CodigoRelacion", "String")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "CodigoEnfermedad", "String")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "Enfermedad", "String")
        objDT_Enfermedad = Datos.agregarColumna(objDT_Enfermedad, "Edad", "Integer")

        Dim dr_Enfermedad As DataRow

        For Each drv As GridViewRow In gvDetalleEnfermedad.Rows

            dr_Enfermedad = objDT_Enfermedad.NewRow
            dr_Enfermedad.Item("CodigoRelacion") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Enfermedad.Item("CodigoEnfermedad") = CType(drv.FindControl("lblCodigoEnfermedad"), Label).Text
            dr_Enfermedad.Item("Enfermedad") = CType(drv.FindControl("lblEnfermedad"), Label).Text
            dr_Enfermedad.Item("Edad") = CType(drv.FindControl("lblEdadEnfermedad_grilla"), Label).Text
            objDT_Enfermedad.Rows.Add(dr_Enfermedad)

        Next

        miDetalle = Comparar2DataTable(ds_Lista.Tables(1), objDT_Enfermedad, 1)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(1).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(1).Rows(0).Item("Origen") = "T" Then
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(1).Rows(0).Item("CodigoBloqueInformacion").ToString))
            Else
                objDT_Enfermedad.Rows.Clear()
            End If
        End If

        'Detalle Vacuna
        Dim objDT_Vacuna As DataTable

        objDT_Vacuna = New DataTable("ListaVacuna")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "CodigoRelacion", "String")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "CodigoVacuna", "String")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "CodigoDosis", "String")
        objDT_Vacuna = Datos.agregarColumna(objDT_Vacuna, "FechaVacunacion", "Date")

        Dim dr_Vacuna As DataRow

        For Each drv As GridViewRow In gvDetalleVacuna.Rows

            dr_Vacuna = objDT_Vacuna.NewRow
            dr_Vacuna.Item("CodigoRelacion") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Vacuna.Item("CodigoVacuna") = CType(drv.FindControl("lblCodigoVacuna"), Label).Text
            dr_Vacuna.Item("CodigoDosis") = CType(drv.FindControl("lblCodigoDosis"), Label).Text
            dr_Vacuna.Item("FechaVacunacion") = CType(drv.FindControl("lblFechaVacunacion"), Label).Text
            objDT_Vacuna.Rows.Add(dr_Vacuna)

        Next

        miDetalle = Comparar2DataTable(ds_Lista.Tables(2), objDT_Vacuna, 2)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(2).Rows(0).Item("Origen") = "T" Then
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoBloqueInformacion").ToString))
            Else
                objDT_Vacuna.Rows.Clear()
            End If
        End If

        'Detalle Alergia
        Dim objDT_Alergia As DataTable

        objDT_Alergia = New DataTable("ListaAlergia")
        objDT_Alergia = Datos.agregarColumna(objDT_Alergia, "Codigo", "Integer")
        objDT_Alergia = Datos.agregarColumna(objDT_Alergia, "CodigoAlergia", "Integer")
        objDT_Alergia = Datos.agregarColumna(objDT_Alergia, "CodigoTipoAlergia", "Integer")
        'objDT_Alergia = Datos.agregarColumna(objDT_Alergia, "FechaRegistro", "Date")

        Dim dr_Alergia As DataRow

        For Each drv As GridViewRow In gvDetalleAlergia.Rows

            dr_Alergia = objDT_Alergia.NewRow
            dr_Alergia.Item("Codigo") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Alergia.Item("CodigoAlergia") = CType(drv.FindControl("lblCodigoAlergia"), Label).Text
            dr_Alergia.Item("CodigoTipoAlergia") = CType(drv.FindControl("lblCodigoTipoAlergia"), Label).Text
            'dr_Alergia.Item("FechaRegistro") = CType(drv.FindControl("lblFechaRegistroAlergia"), Label).Text
            objDT_Alergia.Rows.Add(dr_Alergia)

        Next

        miDetalle = Comparar2DataTable(ds_Lista.Tables(3), objDT_Alergia, 3)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(3).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(3).Rows(0).Item("Origen") = "T" Then
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(3).Rows(0).Item("CodigoBloqueInformacion").ToString))
            Else
                objDT_Alergia.Rows.Clear()
            End If
        End If


        'Detalle CaracteristicasPiel
        Dim objDT_CaracteristicasPiel As DataTable

        objDT_CaracteristicasPiel = New DataTable("ListaCaracteristicasPiel")
        objDT_CaracteristicasPiel = Datos.agregarColumna(objDT_CaracteristicasPiel, "CodigoRelacion", "String")
        objDT_CaracteristicasPiel = Datos.agregarColumna(objDT_CaracteristicasPiel, "CodigoCaracteristicapiel", "String")
        'objDT_CaracteristicasPiel = Datos.agregarColumna(objDT_CaracteristicasPiel, "FechaRegistro", "Date")

        Dim dr_CaracteristicasPiel As DataRow

        For Each drv As GridViewRow In gvDetalleCaracteristicaPiel.Rows

            dr_CaracteristicasPiel = objDT_CaracteristicasPiel.NewRow
            dr_CaracteristicasPiel.Item("CodigoRelacion") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_CaracteristicasPiel.Item("CodigoCaracteristicapiel") = CType(drv.FindControl("lblCodigoCaracteristicapiel"), Label).Text
            'dr_CaracteristicasPiel.Item("FechaRegistro") = CType(drv.FindControl("lblFechaRegistroCaracteristicapiel"), Label).Text
            objDT_CaracteristicasPiel.Rows.Add(dr_CaracteristicasPiel)

        Next

        miDetalle = Comparar2DataTable(ds_Lista.Tables(4), objDT_CaracteristicasPiel, 4)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(4).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(4).Rows(0).Item("Origen") = "T" Then
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(4).Rows(0).Item("CodigoBloqueInformacion").ToString))
            Else
                objDT_CaracteristicasPiel.Rows.Clear()
            End If
        End If

        '''''''''''''''''''''''
        ''Detalle Medicamento
        Dim objDT_Medicamento As DataTable

        objDT_Medicamento = New DataTable("ListaMedicamento")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CodigoRelacion", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CodigoMedicamento", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CodigoPresentacion", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "CantidadPresentacion", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "DosisMedicamento", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "Observaciones", "String")
        Dim dr_Medicamento As DataRow

        For Each drv As GridViewRow In gvDetalleMedicamento.Rows

            dr_Medicamento = objDT_Medicamento.NewRow
            dr_Medicamento.Item("CodigoRelacion") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Medicamento.Item("CodigoMedicamento") = CType(drv.FindControl("lblCodigoMedicamento"), Label).Text
            dr_Medicamento.Item("CodigoPresentacion") = CType(drv.FindControl("lblCodigoPresentacion"), Label).Text
            dr_Medicamento.Item("CantidadPresentacion") = CType(drv.FindControl("lblCantidadPresentacion"), Label).Text
            dr_Medicamento.Item("DosisMedicamento") = CType(drv.FindControl("lblDosisMedicamento"), Label).Text
            dr_Medicamento.Item("Observaciones") = CType(drv.FindControl("lblObservaciones"), Label).Text
            objDT_Medicamento.Rows.Add(dr_Medicamento)

        Next
     
        miDetalle = Comparar2DataTable(ds_Lista.Tables(5), objDT_Medicamento, 5)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(5).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(5).Rows(0).Item("Origen") = "T" Then
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(5).Rows(0).Item("CodigoBloqueInformacion").ToString))
            Else
                objDT_Medicamento.Rows.Clear()
            End If
        End If

        'Detalle Hospitalizacion
        Dim objDT_Hospitalizacion As DataTable

        objDT_Hospitalizacion = New DataTable("ListaHospitalizacion")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "CodigoRelacion", "String")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "CodigoMotivoHospitalizacion", "Integer")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "FechaHospitalizacion", "Date")
        objDT_Hospitalizacion = Datos.agregarColumna(objDT_Hospitalizacion, "Hospitalizacion", "String")

        Dim dr_Hospitalizacion As DataRow

        For Each drv As GridViewRow In gvDetalleHospitalizacion.Rows

            dr_Hospitalizacion = objDT_Hospitalizacion.NewRow
            dr_Hospitalizacion.Item("CodigoRelacion") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Hospitalizacion.Item("CodigoMotivoHospitalizacion") = CType(drv.FindControl("lblCodigoMotivoHospitalizacion"), Label).Text
            dr_Hospitalizacion.Item("FechaHospitalizacion") = CType(drv.FindControl("lblFechaHospitalizacion"), Label).Text
            dr_Hospitalizacion.Item("Hospitalizacion") = CType(drv.FindControl("lblHospitalizacion"), Label).Text
            objDT_Hospitalizacion.Rows.Add(dr_Hospitalizacion)

        Next

        miDetalle = Comparar2DataTable(ds_Lista.Tables(6), objDT_Hospitalizacion, 6)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(6).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(6).Rows(0).Item("Origen") = "T" Then
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(6).Rows(0).Item("CodigoBloqueInformacion").ToString))
            Else
                objDT_Hospitalizacion.Rows.Clear()
            End If
        End If

        'Detalle Operacion
        Dim objDT_Operacion As DataTable

        objDT_Operacion = New DataTable("ListaOperacion")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "CodigoRelacion", "String")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "CodigoTipoOperaciones", "Integer")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "FechaOperacion", "Date")
        objDT_Operacion = Datos.agregarColumna(objDT_Operacion, "Operacion", "String")

        Dim dr_Operacion As DataRow

        For Each drv As GridViewRow In gvDetalleOperacion.Rows

            dr_Operacion = objDT_Operacion.NewRow
            dr_Operacion.Item("CodigoRelacion") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Operacion.Item("CodigoTipoOperaciones") = CType(drv.FindControl("lblCodigoTipoOperaciones"), Label).Text
            dr_Operacion.Item("FechaOperacion") = CType(drv.FindControl("lblFechaOperacion"), Label).Text
            dr_Operacion.Item("Operacion") = CType(drv.FindControl("lblOperacion"), Label).Text
            objDT_Operacion.Rows.Add(dr_Operacion)

        Next

        miDetalle = Comparar2DataTable(ds_Lista.Tables(7), objDT_Operacion, 7)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(7).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(7).Rows(0).Item("Origen") = "T" Then
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(7).Rows(0).Item("CodigoBloqueInformacion").ToString))
            Else
                objDT_Operacion.Rows.Clear()
            End If
        End If


        'Otros Controles
        Dim objDT_TipoControl As DataTable

        objDT_TipoControl = New DataTable("ListaTipoControl")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "CodigoRelacion", "String")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "CodigoTipoControl", "Integer")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "FechaControl", "Date")
        objDT_TipoControl = Datos.agregarColumna(objDT_TipoControl, "Resultado", "String")

        Dim dr_TipoControl As DataRow

        For Each drv As GridViewRow In gvDetalleTipoControl.Rows

            dr_TipoControl = objDT_TipoControl.NewRow
            dr_TipoControl.Item("CodigoRelacion") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_TipoControl.Item("CodigoTipoControl") = CType(drv.FindControl("lblCodigoTipoControl"), Label).Text
            dr_TipoControl.Item("FechaControl") = CType(drv.FindControl("lblFechaControl"), Label).Text
            dr_TipoControl.Item("Resultado") = CType(drv.FindControl("lblResultado"), Label).Text
            objDT_TipoControl.Rows.Add(dr_TipoControl)

        Next

        miDetalle = Comparar2DataTable(ds_Lista.Tables(9), objDT_TipoControl, 8)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(9).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(9).Rows(0).Item("Origen") = "T" Then
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(9).Rows(0).Item("CodigoBloqueInformacion").ToString))
            Else
                objDT_TipoControl.Rows.Clear()
            End If
        End If


        If BoolGrabar Then
            'Agrego las DataTable a mi DataSet
            objDS_Detalle.Tables.Add(objDT_Enfermedad)
            objDS_Detalle.Tables.Add(objDT_Vacuna)
            objDS_Detalle.Tables.Add(objDT_Alergia)
            objDS_Detalle.Tables.Add(objDT_CaracteristicasPiel)
            objDS_Detalle.Tables.Add(objDT_Medicamento)
            objDS_Detalle.Tables.Add(objDT_Hospitalizacion)
            objDS_Detalle.Tables.Add(objDT_Operacion)
            objDS_Detalle.Tables.Add(objDT_TipoControl)

            str_Perfiles = obtenerPerfiles(ds_Lista.Tables(10), arrBloques)

            usp_valor = obj_BL_FichaMedica.FUN_INS_FichaMedicasAlumnosTemp( _
                                ojb_BE_SolicitudActualizacionFichaMedicaAlumno, _
                                obj_BE_FichaMedica, _
                                str_Perfiles, _
                                objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Else
            'MostrarSexyAlertBox(Alertas.ObtenerAlerta(4, ""), "Alert")
        End If

        If usp_valor > 0 Then

            ' Si se registra la solicitud, registro en el log de pasos de matrícula
            Dim int_CodigoPasoMatricula As Integer = 8
            RegistrarPasoMatricula(int_CodigoPasoMatricula, hiddenCodigoAnioAcademico.Value, hiddenCodigoAlumno.Value, hiddenCodigoFamiliar.Value, 0)

            ' Envio de email a responsables de validación
            enviarEmailResponsablesValidacion(str_Perfiles, usp_valor)

            'MostrarSexyAlertBox(usp_mensaje, "Info")
            MostrarMensajeAlert(usp_mensaje)
            btnFichaCancelar_Click()

            ' Recargo la página padre
            CargarPaginaPadre()

        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Obtiene los datos de la ficha médica del alumno
    ''' </summary>
    ''' <param name="str_Codigo">codigo del Alumno al que se le van a consultar los datos de la ficha</param>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     29/12/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal str_Codigo As String)
        Try

            Dim BGColor As String = "#dcff7d"

            Dim obj_BL_FichaMedicaAlumno As New bl_FichaMedicasAlumnos
            Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
            Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
            Dim ds_Lista As DataSet = obj_BL_FichaMedicaAlumno.FUN_GET_FichaMedicaAlumnoVisualizacionActualizacionFamiliar(str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("DatosOriginalesFMA") = ds_Lista


            hd_CodigoPersonaSolicitante.Value = Obtener_CodigoFamiliarLogueado()
            'hd_CodigoPersonaSolicitante.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoPersona").ToString  '1425 '18

            lblNombreCompletoAlumno.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString

            'img_FotoUsuario.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web").ToString() & ds_Lista.Tables(0).Rows(0).Item("rutaFoto").ToString
            hd_CodigoPersona.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersona").ToString)
            hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString)
            lblNombreAlumno.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString
            'lblSede.Text = ds_Lista.Tables(0).Rows(0).Item("Sede").ToString
            lblSituacionAnio.Text = ds_Lista.Tables(0).Rows(0).Item("estadoAnioActualAlumno").ToString
            lblENSnGS.Text = ds_Lista.Tables(0).Rows(0).Item("NSnGS").ToString

            hd_GradoActual.Value = ds_Lista.Tables(0).Rows(0).Item("GradoActual")

            'Desarrollo infantil

            ddlTipoNacimiento.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoNacimiento").ToString)

            tbObservaciones.Text = ds_Lista.Tables(0).Rows(0).Item("Observacion").ToString

            ddlEdadLevCabeza.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadLevantoCabeza").ToString)

            ddlMesesLevCabeza.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString)

            ddlEdadSento.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadSento").ToString)

            ddlMesesSento.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesSento").ToString)

            ddlEdadParo.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadParo").ToString)

            ddlMesesParo.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesParo").ToString)

            ddlEdadCamino.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadCamino").ToString)

            ddlMesesCamino.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesCamino").ToString)

            ddlEdadControloEsfinteres.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString)

            ddlMesesControloEsfinteres.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString)

            ddlEdadHabloPrimerasPalabras.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString)

            ddlMesesHabloPrimerasPalabras.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString)

            ddlEdadHabloFluidez.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString)

            ddlMesesHabloFluidez.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString)

            ddlTipoSangre.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoTipoSangre").ToString)

            rbTabiqueDesviado.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("TabiqueDesviado"))

            rbSangradoNasal.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("SangradoNasal"))

            tbObservacionesOftalmologicas.Text = ds_Lista.Tables(0).Rows(0).Item("ObservacionesOftalmologicas").ToString

            rbUsaLentes.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("UsaLentes"))

            tbObservacionesDental.Text = ds_Lista.Tables(0).Rows(0).Item("ObservacionesDental").ToString

            rbUsaOrtodoncia.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("UsaOrtodoncia"))

            'Detalle Enfermedad
            Dim objDT_Enfermedad As DataTable
            objDT_Enfermedad = New DataTable("ListaEnfermedad")
            objDT_Enfermedad = ds_Lista.Tables(1).Clone

            If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(1).Rows
                    objDT_Enfermedad.ImportRow(dr)
                Next

                ViewState("ListaEnfermedad") = objDT_Enfermedad
                gvDetalleEnfermedad.DataSource = objDT_Enfermedad
                gvDetalleEnfermedad.DataBind()
                GridviewFillColor(ds_Lista.Tables(1), gvDetalleEnfermedad)
            End If

            'Detalle Vacuna
            Dim objDT_Vacuna As DataTable
            objDT_Vacuna = New DataTable("ListaVacuna")
            objDT_Vacuna = ds_Lista.Tables(2).Clone

            If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(2).Rows
                    objDT_Vacuna.ImportRow(dr)
                Next

                ViewState("ListaVacuna") = objDT_Vacuna
                gvDetalleVacuna.DataSource = objDT_Vacuna
                gvDetalleVacuna.DataBind()
                GridviewFillColor(ds_Lista.Tables(2), gvDetalleVacuna)
            End If

            'Detalle Alergias
            Dim objDT_Alergia As DataTable
            objDT_Alergia = New DataTable("ListaAlergia")
            objDT_Alergia = ds_Lista.Tables(3).Clone

            If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(3).Rows
                    objDT_Alergia.ImportRow(dr)
                Next

                ViewState("ListaAlergia") = objDT_Alergia
                gvDetalleAlergia.DataSource = objDT_Alergia
                gvDetalleAlergia.DataBind()
                GridviewFillColor(ds_Lista.Tables(3), gvDetalleAlergia)
            End If

            'Detalle Caracteristicas de la piel           
            Dim objDT_CaracteristicaPiel As DataTable
            objDT_CaracteristicaPiel = New DataTable("ListaCaracteristicasPiel")
            objDT_CaracteristicaPiel = ds_Lista.Tables(4).Clone

            If ds_Lista.Tables(4).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(4).Rows
                    objDT_CaracteristicaPiel.ImportRow(dr)
                Next

                ViewState("ListaCaracteristicasPiel") = objDT_CaracteristicaPiel
                gvDetalleCaracteristicaPiel.DataSource = objDT_CaracteristicaPiel
                gvDetalleCaracteristicaPiel.DataBind()
                GridviewFillColor(ds_Lista.Tables(4), gvDetalleCaracteristicaPiel)
            End If

            'Detalle Medicamento
            Dim objDT_Medicamentos As DataTable
            objDT_Medicamentos = New DataTable("ListaMedicamentos")
            objDT_Medicamentos = ds_Lista.Tables(5).Clone

            If ds_Lista.Tables(5).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(5).Rows
                    objDT_Medicamentos.ImportRow(dr)
                Next

                ViewState("ListaMedicamentos") = objDT_Medicamentos
                gvDetalleMedicamento.DataSource = objDT_Medicamentos
                gvDetalleMedicamento.DataBind()
                GridviewFillColor(ds_Lista.Tables(5), gvDetalleMedicamento)
            End If

            'Hospitalizacion
            Dim objDT_Hospitalizacion As DataTable
            objDT_Hospitalizacion = New DataTable("ListaHospitalizacion")
            objDT_Hospitalizacion = ds_Lista.Tables(6).Clone

            If ds_Lista.Tables(6).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(6).Rows
                    objDT_Hospitalizacion.ImportRow(dr)
                Next

                ViewState("ListaHospitalizacion") = objDT_Hospitalizacion
                gvDetalleHospitalizacion.DataSource = objDT_Hospitalizacion
                gvDetalleHospitalizacion.DataBind()
                GridviewFillColor(ds_Lista.Tables(6), gvDetalleHospitalizacion)
            End If

            'Operacion
            Dim objDT_Operacion As DataTable
            objDT_Operacion = New DataTable("ListaOperacion")
            objDT_Operacion = ds_Lista.Tables(7).Clone

            If ds_Lista.Tables(7).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(7).Rows
                    objDT_Operacion.ImportRow(dr)
                Next

                ViewState("ListaOperacion") = objDT_Operacion
                gvDetalleOperacion.DataSource = objDT_Operacion
                gvDetalleOperacion.DataBind()
                GridviewFillColor(ds_Lista.Tables(7), gvDetalleOperacion)
            End If

            'ControlPesoTalla
            Dim objDT_ControlPesoTalla As DataTable
            objDT_ControlPesoTalla = New DataTable("ListaControlPesoTalla")
            objDT_ControlPesoTalla = ds_Lista.Tables(8).Clone

            If ds_Lista.Tables(8).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(8).Rows
                    objDT_ControlPesoTalla.ImportRow(dr)
                Next

                ViewState("ListaControlPesoTalla") = objDT_ControlPesoTalla
                gvDetalleControlPesoTalla.DataSource = objDT_ControlPesoTalla
                gvDetalleControlPesoTalla.DataBind()
            End If

            'Otros Controles
            Dim objDT_TipoControl As DataTable
            objDT_TipoControl = New DataTable("ListaTipoControl")
            objDT_TipoControl = ds_Lista.Tables(9).Clone

            If ds_Lista.Tables(9).Rows(0).Item("CodigoRelacion") <> -1 Then
                For Each dr As DataRow In ds_Lista.Tables(9).Rows
                    objDT_TipoControl.ImportRow(dr)
                Next

                ViewState("ListaTipoControl") = objDT_TipoControl
                gvDetalleTipoControl.DataSource = objDT_TipoControl
                gvDetalleTipoControl.DataBind()
                GridviewFillColor(ds_Lista.Tables(9), gvDetalleTipoControl)
            End If


            'VerRegistro("Actualización")

        Catch ex As Exception

            'MostrarSexyAlertBox(Alertas.ObtenerMensajaError(2), "Error")

        End Try

    End Sub

    ''' <summary>
    ''' Compara 2 conjuntos de datos (DataTable)
    ''' </summary>
    ''' <param name="dtOriginal">Conjunto de datos original (proveninente de la base de datos)</param>
    ''' <param name="dtActualizar">Conjunto de datos ha aztualizar (modificado en el formulario)</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function Comparar2DataTable(ByVal dtOriginal As DataTable, ByVal dtActualizar As DataTable, ByVal caso As String) As Integer

        'Comparo los DataTables
        Dim rpta As Integer = 0

        If dtOriginal.Rows.Count <> dtActualizar.Rows.Count Then

            Return 1 ' Los DataTables son diferentes

        Else 'Si tienen el mismo numero de elementos los comparo 1 por 1

            Select Case caso
                Case 1 ' Detalle Enfermedad
                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoEnfermedad") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoEnfermedad") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoEnfermedad") > 0 Then

                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If (dtOriginal.Rows(i).Item("CodigoEnfermedad") <> dtActualizar.Rows(i).Item("CodigoEnfermedad")) _
                                Or (dtOriginal.Rows(i).Item("Edad") <> dtActualizar.Rows(i).Item("Edad")) Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next

                        End If
                        Return 0
                    End If
                Case 2 ' Detalle Vacuna
                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoVacuna") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoVacuna") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoVacuna") > 0 Then

                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If (dtOriginal.Rows(i).Item("CodigoVacuna") <> dtActualizar.Rows(i).Item("CodigoVacuna")) _
                                Or (dtOriginal.Rows(i).Item("CodigoDosis") <> dtActualizar.Rows(i).Item("CodigoDosis")) _
                                Or (dtOriginal.Rows(i).Item("FechaVacunacion").ToString <> dtActualizar.Rows(i).Item("FechaVacunacion").ToString) Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next

                        End If
                        Return 0
                    End If
                Case 3 ' Detalle Alergia
                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoAlergia") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoAlergia") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoAlergia") > 0 Then

                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If (dtOriginal.Rows(i).Item("CodigoAlergia") <> dtActualizar.Rows(i).Item("CodigoAlergia")) _
                                Or (dtOriginal.Rows(i).Item("CodigoTipoAlergia") <> dtActualizar.Rows(i).Item("CodigoTipoAlergia")) Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next

                        End If
                        Return 0
                    End If
                Case 4 ' Detalle Caracteristicas de la Piel
                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoCaracteristicapiel") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoCaracteristicapiel") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoCaracteristicapiel") > 0 Then

                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If (dtOriginal.Rows(i).Item("CodigoCaracteristicapiel") <> dtActualizar.Rows(i).Item("CodigoCaracteristicapiel")) Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next

                        End If
                        Return 0
                    End If
                Case 5 ' Detalle Medicamento
                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoMedicamento") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoMedicamento") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoMedicamento") > 0 Then

                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If (dtOriginal.Rows(i).Item("CodigoMedicamento") <> dtActualizar.Rows(i).Item("CodigoMedicamento")) _
                                Or (dtOriginal.Rows(i).Item("CodigoPresentacion") <> dtActualizar.Rows(i).Item("CodigoPresentacion")) Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next

                        End If
                        Return 0
                    End If
                Case 6 ' Detalle Hospitalizacion
                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoMotivoHospitalizacion") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoMotivoHospitalizacion") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoMotivoHospitalizacion") > 0 Then

                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If (dtOriginal.Rows(i).Item("CodigoMotivoHospitalizacion") <> dtActualizar.Rows(i).Item("CodigoMotivoHospitalizacion")) _
                                Or (dtOriginal.Rows(i).Item("FechaHospitalizacion").ToString <> dtActualizar.Rows(i).Item("FechaHospitalizacion").ToString) Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next

                        End If
                        Return 0
                    End If
                Case 7 ' Detalle Operacion
                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoTipoOperaciones") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoTipoOperaciones") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoTipoOperaciones") > 0 Then

                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If (dtOriginal.Rows(i).Item("CodigoTipoOperaciones") <> dtActualizar.Rows(i).Item("CodigoTipoOperaciones")) _
                                Or (dtOriginal.Rows(i).Item("FechaOperacion").ToString <> dtActualizar.Rows(i).Item("FechaOperacion").ToString) Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next

                        End If
                        Return 0
                    End If
                Case 8 ' Detalle Controles
                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoTipoControl") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoTipoControl") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoTipoControl") > 0 Then

                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If (dtOriginal.Rows(i).Item("CodigoTipoControl") <> dtActualizar.Rows(i).Item("CodigoTipoControl")) _
                                Or (dtOriginal.Rows(i).Item("FechaControl").ToString <> dtActualizar.Rows(i).Item("FechaControl").ToString) Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next

                        End If
                        Return 0
                    End If
            End Select

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

    'Private Sub BlanquearBotones()
    '    Dim int_Contador As Integer = 0
    '    Dim buton_Opcion As Button

    '    While int_Contador <= dgv_GrupoInformacion.Rows.Count - 1

    '        buton_Opcion = dgv_GrupoInformacion.Rows(int_Contador).FindControl("btn_GrupoInformacion")
    '        buton_Opcion.Style.Value = "cursor:pointer;background: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/menu/grupoInformacion_itemMenu.jpg') right no-repeat; "
    '        int_Contador = int_Contador + 1
    '    End While
    'End Sub

    Private Sub enviarEmailResponsablesValidacion(ByVal str_CodigoPerfiles As String, ByVal int_CodigoSolicitud As Integer)

        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()

        'Tipos de Solicitud 
        'Ficha Familiar : 1
        'Ficha Alumno   : 2
        'Ficha Médica   : 3
        Dim int_TipoSolicitud As Integer = 3

        Dim obj_BL_SolicitudActualizacionDatos As New bl_SolicitudActualizacionDatos
        Dim ds_Lista As DataSet = obj_BL_SolicitudActualizacionDatos.FUN_LIS_DatosSolicitudes(int_CodigoSolicitud, int_TipoSolicitud, str_CodigoPerfiles, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        'Dim obj_EnvioEmail As New EnvioEmail
        Dim int_ExitoEnvio As Integer = 0

        Dim arr_Emails As New ArrayList
        Dim str_Asunto As String = "Solicitud de Actualización de Datos de la Ficha Médica del Alumno"

        Dim sb_Cuerpo As New StringBuilder
        sb_Cuerpo.Append("<div style='font-family: Arial; font-size: 11px;'>")
        sb_Cuerpo.Append("Se notifica que tiene una nueva solicitud de validación.<br />")
        sb_Cuerpo.Append("La solicitud fue enviada el <i><b>" & ds_Lista.Tables(1).Rows(0).Item("FechaSolicitud") & "</b></i>")
        sb_Cuerpo.Append("por <i><b>" & ds_Lista.Tables(1).Rows(0).Item("NombreCompletoSolicitante") & "</b></i>")
        sb_Cuerpo.Append("y solicita la actualización de datos del alumno <i><b>" & ds_Lista.Tables(1).Rows(0).Item("NombreCompletoFicha") & "</b></i>.")
        sb_Cuerpo.Append("</div>")

        Dim str_EmailCopia As String = ""

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            arr_Emails.Add(dr.Item("CorreoCorporativo").ToString)
        Next

        int_ExitoEnvio = EnvioEmail.SendEmail(arr_Emails, sb_Cuerpo.ToString, str_Asunto)

    End Sub

#End Region

#Region "Mantenimientos Detalles"

#Region "Mantenimiento de Detalle Enfermedad"

#Region "Eventos"
    Protected Sub btn_Add_Enfermedad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        ViewState("NuevoEnfermedad") = True
        pnModalEnfermedad.Show()

    End Sub

    Protected Sub popup_btnCancelar_Enfermedad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        cerrarModalEnfermedad()

    End Sub

    Protected Sub popup_btnAgregar_Enfermedad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If ViewState("NuevoEnfermedad") = False Then
                int_CodigoAccion = 201
                editarEnfermedad()
            ElseIf ViewState("NuevoEnfermedad") = True Then
                int_CodigoAccion = 200
                agregarEnfermedad()
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub
#End Region
#Region "Métodos"
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

        pnModalEnfermedad.Hide()
        ddlEnfermedad.SelectedValue = 0
        tbEdad.Text = 0

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

        If ddlEnfermedad.SelectedValue = 0 Then
            pnModalEnfermedad.Show()
            MostrarSexyAlertBox("Debe seleccionar una enfermedad valido.", "Alert")
            Exit Sub

        End If

        If tbEdad.Text = 0 Then
            pnModalEnfermedad.Show()
            MostrarSexyAlertBox("Debe ingresar una edad.", "Alert")

            Exit Sub

        End If

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaEnfermedad") Is Nothing Then

            dt = New DataTable("ListaEnfermedad")

            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
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
                    pnModalEnfermedad.Show()
                    Exit Sub

                End If
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("CodigoRelacion") = id_codigo_fila + 1
            dr.Item("CodigoEnfermedad") = ddlEnfermedad.SelectedValue
            dr.Item("Enfermedad") = ddlEnfermedad.SelectedItem.ToString
            dr.Item("Edad") = Val(tbEdad.Text)

            dt.Rows.Add(dr)

        End If

        ViewState("ListaEnfermedad") = dt

        gvDetalleEnfermedad.DataSource = dt
        gvDetalleEnfermedad.DataBind()

        ddlEnfermedad.SelectedValue = 0
        tbEdad.Text = 0

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

        If ddlEnfermedad.SelectedValue = 0 Then
            pnModalEnfermedad.Show()
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            Exit Sub

        End If

        If tbEdad.Text = 0 Then
            pnModalEnfermedad.Show()
            MostrarSexyAlertBox("Debe registrar una edad.", "Alert")
            Exit Sub

        End If

        Dim int_CodigoOriginal As Integer = hidencodigoEnfermedad.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaEnfermedad")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoEnfermedad").ToString = ddlEnfermedad.SelectedValue And auxdr.Item("Edad").ToString = Val(tbEdad.Text) Then

                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                'ddlEnfermedad.SelectedValue = 0
                'tbEdad.Text = 0
                pnModalEnfermedad.Show()
                Exit Sub

            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelacion") = int_CodigoOriginal
                auxdr.Item("CodigoEnfermedad") = ddlEnfermedad.SelectedValue
                auxdr.Item("Enfermedad") = ddlEnfermedad.SelectedItem.ToString
                auxdr.Item("Edad") = Val(tbEdad.Text)

            End If

        Next

        ViewState("ListaEnfermedad") = dt

        gvDetalleEnfermedad.DataSource = dt
        gvDetalleEnfermedad.DataBind()

        ddlEnfermedad.SelectedValue = 0
        tbEdad.Text = 0

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
    Private Sub eliminarRelFichaMedEnEnfermedades(ByVal int_CodigoEnfermedad As Integer)

        Dim dt As DataTable

        dt = ViewState("ListaEnfermedad")

        For Each auxdr As DataRow In dt.Rows

            If Val(auxdr.Item("CodigoRelacion").ToString) = int_CodigoEnfermedad Then
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
        pnModalEnfermedad.Show()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleEnfermedad_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim Codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoEnfermedad") = False
                    activarEditarEnfermedad(Codigo, CType(row.FindControl("lblCodigoEnfermedad"), Label).Text, CType(row.FindControl("lblEdadEnfermedad_grilla"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarRelFichaMedEnEnfermedades(Codigo)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleEnfermedad_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
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

    Protected Sub popup_btnCancelar_Vacuna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalVacuna()
    End Sub

    Protected Sub popup_btnAgregar_Vacuna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If ViewState("NuevoVacuna") = False Then
                int_CodigoAccion = 201
                editarVacuna()
            ElseIf ViewState("NuevoVacuna") = True Then
                int_CodigoAccion = 200
                agregarVacuna()
            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub
#End Region
#Region "Métodos"
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
        ddlTipoVacuna.SelectedValue = 0
        tbFechaVacunacion.Text = Now.Date
        ddlDosis.SelectedValue = 0

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

        tbfecha = tbFechaVacunacion.Text
        tipoVacuna = ddlTipoVacuna.SelectedValue
        dosis = ddlDosis.SelectedValue

        If CDate(tbFechaVacunacion.Text) > CDate(Today.ToShortDateString) Then
            MostrarSexyAlertBox("incorrecta. La fecha de vacunación no puede ser mayor a la fecha actual.", "Alert")
            ddlTipoVacuna.SelectedValue = tipoVacuna
            ddlDosis.SelectedValue = dosis
            tbFechaVacunacion.Text = tbfecha
            pnModalVacuna.Show()
            Exit Sub
        End If

        If ddlTipoVacuna.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un Tipo de Vacuna.", "Alert")
            'ddlTipoVacuna.SelectedValue = 0
            'ddlDosis.SelectedValue = 0
            'tbFechaVacunacion.Text = Now.Date
            pnModalVacuna.Show()
            Exit Sub

        End If
        If ddlDosis.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar una Dosis.", "Alert")
            'ddlTipoVacuna.SelectedValue = 0
            'ddlDosis.SelectedValue = 0
            'tbFechaVacunacion.Text = Now.Date
            pnModalVacuna.Show()
            Exit Sub

        End If

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaVacuna") Is Nothing Then

            dt = New DataTable("ListaVacuna")

            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoVacuna", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoDosis", "Integer")
            dt = Datos.agregarColumna(dt, "FechaVacunacion", "date")
            dt = Datos.agregarColumna(dt, "Vacuna", "String")
            dt = Datos.agregarColumna(dt, "Dosis", "String")

        Else

            dt = ViewState("ListaVacuna")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoVacuna").ToString = ddlTipoVacuna.SelectedValue Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    'ddlTipoVacuna.SelectedValue = 0
                    'ddlDosis.SelectedValue = 0
                    'tbFechaVacunacion.Text = Now.Date
                    pnModalVacuna.Show()
                    Exit Sub

                End If
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelacion") = id_codigo_fila + 1
            dr.Item("CodigoVacuna") = ddlTipoVacuna.SelectedValue
            dr.Item("Vacuna") = ddlTipoVacuna.SelectedItem.ToString
            dr.Item("CodigoDosis") = ddlDosis.SelectedValue
            dr.Item("Dosis") = ddlDosis.SelectedItem.ToString
            dr.Item("FechaVacunacion") = tbFechaVacunacion.Text

            dt.Rows.Add(dr)

        End If

        ViewState("ListaVacuna") = dt

        gvDetalleVacuna.DataSource = dt
        gvDetalleVacuna.DataBind()

        ddlTipoVacuna.SelectedValue = 0
        ddlDosis.SelectedValue = 0
        tbFechaVacunacion.Text = Now.Date

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

        tbfecha = tbFechaVacunacion.Text
        tipoVacuna = ddlTipoVacuna.SelectedValue
        dosis = ddlDosis.SelectedValue

        If CDate(tbFechaVacunacion.Text) > CDate(Today.ToShortDateString) Then
            MostrarSexyAlertBox("incorrecta. La fecha de vacunación no puede ser mayor a la fecha actual.", "Alert")
            ddlTipoVacuna.SelectedValue = tipoVacuna
            ddlDosis.SelectedValue = dosis
            tbFechaVacunacion.Text = tbfecha
            pnModalVacuna.Show()
            Exit Sub
        End If

        If ddlTipoVacuna.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            ddlTipoVacuna.SelectedValue = tipoVacuna
            ddlDosis.SelectedValue = dosis
            tbFechaVacunacion.Text = tbfecha
            pnModalVacuna.Show()
            Exit Sub

        End If
        If ddlDosis.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar una Dosis.", "Alert")
            ddlTipoVacuna.SelectedValue = tipoVacuna
            ddlDosis.SelectedValue = dosis
            tbFechaVacunacion.Text = tbfecha
            pnModalVacuna.Show()
            Exit Sub

        End If


        Dim int_CodigoOriginal As Integer = hidencodigoVacuna.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaVacuna")

        For Each auxdr As DataRow In dt.Rows


            If auxdr.Item("CodigoVacuna").ToString = int_CodigoOriginal Then

                'If auxdr.Item("FechaVacunacion").ToString = tbFechaVacunacion.Text And auxdr.Item("CodigoVacuna").ToString = ddlTipoVacuna.SelectedValue And auxdr.Item("CodigoDosis").ToString = ddlDosis.SelectedValue Then
            ElseIf auxdr.Item("CodigoVacuna").ToString = ddlTipoVacuna.SelectedValue Then
                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                'ddlTipoVacuna.SelectedValue = 0
                'ddlDosis.SelectedValue = 0
                'tbFechaVacunacion.Text = Now.Date
                pnModalVacuna.Show()
                Exit Sub

            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelacion") = int_CodigoOriginal
                auxdr.Item("CodigoVacuna") = ddlTipoVacuna.SelectedValue
                auxdr.Item("Vacuna") = ddlTipoVacuna.SelectedItem.ToString
                auxdr.Item("CodigoDosis") = ddlDosis.SelectedValue
                auxdr.Item("Dosis") = ddlDosis.SelectedItem.ToString
                auxdr.Item("FechaVacunacion") = tbFechaVacunacion.Text

            End If

        Next

        ViewState("ListaVacuna") = dt

        gvDetalleVacuna.DataSource = dt
        gvDetalleVacuna.DataBind()

        ddlTipoVacuna.SelectedValue = 0
        ddlDosis.SelectedValue = 0
        tbFechaVacunacion.Text = Now.Date
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
            If auxdr.Item("CodigoRelacion").ToString = int_CodigoVacuna Then
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
    Private Sub activarEditarVacuna(ByVal int_Codigo As Integer, ByVal int_CodigoVacuna As Integer, ByVal int_CodigoDosis As Integer, ByVal dt_fecha As Date)

        ddlTipoVacuna.SelectedValue = int_CodigoVacuna
        hidencodigoVacuna.Value = int_Codigo
        tbFechaVacunacion.Text = dt_fecha
        ddlDosis.SelectedValue = int_CodigoDosis
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
                    activarEditarVacuna(CodigoRelVacunasFichaMed, CType(row.FindControl("lblCodigoVacuna"), Label).Text, CType(row.FindControl("lblCodigoDosis"), Label).Text, CType(row.FindControl("lblFechaVacunacion"), Label).Text)
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
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
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
    End Sub

    Protected Sub popup_btnCancelar_Alergia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles popup_btnCancelar_Alergia.Click
        cerrarModalAlergia()
    End Sub

    Protected Sub popup_btnAgregar_Alergia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If ViewState("NuevoAlergia") = False Then
                int_CodigoAccion = 201
                editarAlergia()
            ElseIf ViewState("NuevoAlergia") = True Then
                int_CodigoAccion = 200
                agregarAlergia()
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub
#End Region
#Region "Métodos"
    ''' <summary>
    ''' Cierra el popup Alergía
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalAlergia()

        pnModalAlergia.Hide()
        ddlAlergia.SelectedValue = 0

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

        If ddlAlergia.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            'ddlAlergia.SelectedValue = 0
            'ddlTipoAlergia.SelectedValue = 0
            pnModalAlergia.Show()
            Exit Sub

        End If

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaAlergia") Is Nothing Then

            dt = New DataTable("ListaAlergia")

            dt = Datos.agregarColumna(dt, "CodigoRelacion", "String")
            dt = Datos.agregarColumna(dt, "CodigoAlergia", "String")
            dt = Datos.agregarColumna(dt, "CodigoTipoAlergia", "Integer")
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
                    pnModalAlergia.Show()
                    Exit Sub


                End If
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim obj_BL_Alergia As New bl_Alergias
            Dim dt_descriTipoAlergia As DataSet = obj_BL_Alergia.FUN_GET_DescripcionAlergia(ddlAlergia.SelectedValue, 1, 1, 1, 1)

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelacion") = id_codigo_fila + 1
            dr.Item("CodigoAlergia") = ddlAlergia.SelectedValue
            dr.Item("Alergia") = ddlAlergia.SelectedItem.ToString
            dr.Item("CodigoTipoAlergia") = dt_descriTipoAlergia.Tables(0).Rows(0).Item("TA_CodigoTipoAlergia")
            dr.Item("TipoAlergia") = dt_descriTipoAlergia.Tables(0).Rows(0).Item("TA_Descripcion")

            dt.Rows.Add(dr)

        End If

        ViewState("ListaAlergia") = dt

        gvDetalleAlergia.DataSource = dt
        gvDetalleAlergia.DataBind()

        ddlAlergia.SelectedValue = 0

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

        If ddlAlergia.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            'ddlAlergia.SelectedValue = 0
            'tbFechaRegistroAlergia.Text = Now.Date
            pnModalAlergia.Show()
            Exit Sub

        End If

        Dim int_CodigoOriginal As Integer = hidencodigoAlergia.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        Dim obj_BL_Alergia As New bl_Alergias
        Dim dt_descriTipoAlergia As DataSet = obj_BL_Alergia.FUN_GET_DescripcionAlergia(ddlAlergia.SelectedValue, 1, 1, 1, 1)

        dt = ViewState("ListaAlergia")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoAlergia").ToString = ddlAlergia.SelectedValue Then

                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                ddlAlergia.SelectedValue = auxdr.Item("CodigoAlergia").ToString
                pnModalAlergia.Show()
                Exit Sub

            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelacion") = int_CodigoOriginal
                auxdr.Item("CodigoAlergia") = ddlAlergia.SelectedValue
                auxdr.Item("Alergia") = ddlAlergia.SelectedItem.ToString
                auxdr.Item("CodigoTipoAlergia") = dt_descriTipoAlergia.Tables(0).Rows(0).Item("TA_CodigoTipoAlergia")
                auxdr.Item("TipoAlergia") = dt_descriTipoAlergia.Tables(0).Rows(0).Item("TA_Descripcion")

            End If

        Next

        ViewState("ListaAlergia") = dt

        gvDetalleAlergia.DataSource = dt
        gvDetalleAlergia.DataBind()

        ddlAlergia.SelectedValue = 0
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
            If auxdr.Item("CodigoRelacion").ToString = int_CodigoAlergia Then
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
    ''' Edita 1 Registro Alergía del detalle de Alergias
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarAlergia(ByVal int_Codigo As Integer, ByVal int_CodigoAlergia As Integer)

        ddlAlergia.SelectedValue = int_CodigoAlergia
        hidencodigoAlergia.Value = int_Codigo
        pnModalAlergia.Show()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleAlergia_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDetalleAlergia.RowCommand
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then

                Dim CodigoRelFichaMedAlergias As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoAlergia") = False
                    activarEditarAlergia(CodigoRelFichaMedAlergias, CType(row.FindControl("lblCodigoAlergia"), Label).Text)
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
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
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
            If ViewState("NuevoCaracteristicaPiel") = False Then
                int_CodigoAccion = 201
                editarCaracteristicasPiel()
            ElseIf ViewState("NuevoCaracteristicaPiel") = True Then
                int_CodigoAccion = 200
                agregarCaracteristicasPiel()
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_CaracteristicaPiel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalCaracteristicaPiel()
    End Sub
#End Region
#Region "Métodos"
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
        ddlCaracteristicaPiel.SelectedValue = 0

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

        Dim caracteristicaPiel As Integer

        caracteristicaPiel = ddlCaracteristicaPiel.SelectedValue

        If ddlCaracteristicaPiel.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            'ddlCaracteristicaPiel.SelectedValue = 0
            'tbFechaRegistroCaracteristicasPiel.Text = Now.Date
            pnModalCaracteristicasPiel.Show()
            Exit Sub

        End If

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaCaracteristicasPiel") Is Nothing Then

            dt = New DataTable("ListaCaracteristicasPiel")

            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoCaracteristicapiel", "Integer")
            dt = Datos.agregarColumna(dt, "CaracteristicaPiel", "String")

        Else

            dt = ViewState("ListaCaracteristicasPiel")

        End If

        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoCaracteristicapiel").ToString = ddlCaracteristicaPiel.SelectedValue Then
                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    ddlCaracteristicaPiel.SelectedValue = 0
                    pnModalCaracteristicasPiel.Show()
                    Exit Sub
                End If
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelacion") = id_codigo_fila + 1
            dr.Item("CodigoCaracteristicapiel") = ddlCaracteristicaPiel.SelectedValue
            dr.Item("Caracteristicapiel") = ddlCaracteristicaPiel.SelectedItem.ToString

            dt.Rows.Add(dr)

        End If

        ViewState("ListaCaracteristicasPiel") = dt

        gvDetalleCaracteristicaPiel.DataSource = dt
        gvDetalleCaracteristicaPiel.DataBind()

        ddlCaracteristicaPiel.SelectedValue = 0

        upCaracteristicaPiel.Update()

    End Sub

    ''' <summary>
    ''' Edita 1 Registro Caracteristica de Piel  del detalle de Caracteristicas de Piel 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarCaracteristicasPiel()

        Dim caracteristicaPiel As Integer

        caracteristicaPiel = ddlCaracteristicaPiel.SelectedValue

        If ddlCaracteristicaPiel.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            'ddlCaracteristicaPiel.SelectedValue = 0
            'tbFechaRegistroCaracteristicasPiel.Text = Now.Date
            pnModalCaracteristicasPiel.Show()
            Exit Sub

        End If

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

            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelacion") = int_CodigoOriginal
                auxdr.Item("CodigoCaracteristicapiel") = ddlCaracteristicaPiel.SelectedValue
                auxdr.Item("CaracteristicaPiel") = ddlCaracteristicaPiel.SelectedItem.ToString

            End If

        Next

        ViewState("ListaCaracteristicasPiel") = dt

        gvDetalleCaracteristicaPiel.DataSource = dt
        gvDetalleCaracteristicaPiel.DataBind()

        ddlCaracteristicaPiel.SelectedValue = 0
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
            If auxdr.Item("CodigoRelacion").ToString = int_CodigoCaracteristicasPiel Then
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
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarCaracteristicaPiel(ByVal int_codigo As Integer, ByVal int_CodigoCaracteristicasPiel As Integer)

        ddlCaracteristicaPiel.SelectedValue = int_CodigoCaracteristicasPiel
        hdCodigoCaracteristicasPiel.Value = int_codigo
        pnModalCaracteristicasPiel.Show()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleCaracteristicaPiel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub

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
                    activarEditarCaracteristicaPiel(CodigoRelFichaMedCaractPiel, CType(row.FindControl("lblCodigoCaracteristicapiel"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarCaracteristicaPiel(CodigoRelFichaMedCaractPiel)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
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

            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
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
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelacion") = id_codigo_fila + 1
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
            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then
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

                auxdr.Item("CodigoRelacion") = int_CodigoOriginal
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
            If auxdr.Item("CodigoRelacion").ToString = int_CodigoMedicamento Then
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
    ''' <param name="int_Presentacion">Codigo de Frecuencia de Uso</param>
    ''' <param name="str_DosisMedi">Fecha de registro</param>
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
                Dim CodigoRelacion As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoMedicamentos") = False
                    activarEditarMedicamento(CodigoRelacion, CType(row.FindControl("lblCodigoMedicamento"), Label).Text, CType(row.FindControl("lblCodigoPresentacion"), Label).Text, CType(row.FindControl("lblCantidadPresentacion"), Label).Text, CType(row.FindControl("lblDosisMedicamento"), Label).Text, CType(row.FindControl("lblObservaciones"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarMedicamento(CodigoRelacion)
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
            If ViewState("NuevoHospitalizacion") = False Then
                int_CodigoAccion = 201
                editarHospitalizacion()
            ElseIf ViewState("NuevoHospitalizacion") = True Then
                int_CodigoAccion = 200
                agregarHospitalizacion()
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

        If CDate(tbFechaHospitalizacion.Text) > CDate(Today.ToShortDateString) Then
            MostrarSexyAlertBox("incorrecta. La fecha de Hospitalización no puede ser mayor a la fecha actual.", "Alert")
            ddlHospitalizacion.SelectedValue = hospitalizacion
            tbFechaHospitalizacion.Text = fecha
            pnModalHospitalizacion.Show()
            Exit Sub
        End If

        If ddlHospitalizacion.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            'ddlHospitalizacion.SelectedValue = 0
            'tbFechaHospitalizacion.Text = Now.Date
            pnModalHospitalizacion.Show()
            Exit Sub

        End If

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaHospitalizacion") Is Nothing Then

            dt = New DataTable("ListaHospitalizacion")

            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
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
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("CodigoRelacion") = id_codigo_fila + 1
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

        If CDate(tbFechaHospitalizacion.Text) > CDate(Today.ToShortDateString) Then
            MostrarSexyAlertBox("incorrecta. La fecha de Hospitalización no puede ser mayor a la fecha actual.", "Alert")
            ddlHospitalizacion.SelectedValue = hospitalizacion
            tbFechaHospitalizacion.Text = fecha
            pnModalHospitalizacion.Show()
            Exit Sub
        End If

        If ddlHospitalizacion.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            'ddlHospitalizacion.SelectedValue = 0
            'tbFechaHospitalizacion.Text = Now.Date
            pnModalHospitalizacion.Show()
            Exit Sub

        End If

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

            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelacion") = int_CodigoOriginal
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
            If auxdr.Item("CodigoRelacion").ToString = int_CodigoHospitalizacion Then
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
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
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
            If ViewState("NuevoOperacion") = False Then
                int_CodigoAccion = 201
                editarOperacion()
            ElseIf ViewState("NuevoOperacion") = True Then
                int_CodigoAccion = 200
                agregarOperacion()
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

        If CDate(tbFechaOperacion.Text) > CDate(Today.ToShortDateString) Then
            MostrarSexyAlertBox("incorrecta. La fecha de operación no puede ser mayor a la fecha actual.", "Alert")
            ddlOperacion.SelectedValue = operacion
            tbFechaOperacion.Text = fecha
            pnModalOperacion.Show()
            Exit Sub
        End If

        If ddlOperacion.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            'ddlOperacion.SelectedValue = 0
            'tbFechaOperacion.Text = Now.Date
            pnModalOperacion.Show()

        End If

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaOperacion") Is Nothing Then

            dt = New DataTable("ListaOperacion")

            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
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
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelacion") = id_codigo_fila + 1
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

        If CDate(tbFechaOperacion.Text) > CDate(Today.ToShortDateString) Then
            MostrarSexyAlertBox("incorrecta. La fecha de operación no puede ser mayor a la fecha actual.", "Alert")
            ddlOperacion.SelectedValue = operacion
            tbFechaOperacion.Text = fecha
            pnModalOperacion.Show()
            Exit Sub
        End If

        If ddlOperacion.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            'ddlOperacion.SelectedValue = 0
            'tbFechaOperacion.Text = Now.Date
            pnModalOperacion.Show()
        End If

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

            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelacion") = int_CodigoOriginal
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

            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOperacion Then
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
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region


#End Region

#Region "Mantenimiento de Detalle TipoControl"

#Region "Eventos"
    Protected Sub btn_Add_OtrosControles_click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        ViewState("NuevoTipoControl") = True
        pnModalOtrosControles.Show()

    End Sub

    Protected Sub popup_btnAgregar_OtrosControles_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If ViewState("NuevoTipoControl") = False Then
                int_CodigoAccion = 201
                editarTipoControl()
            ElseIf ViewState("NuevoTipoControl") = True Then
                int_CodigoAccion = 200
                agregarTipoControl()
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
    ''' Creador:               Juan Vento
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

        If CDate(tbFechaTipoControl.Text) > CDate(Today.ToShortDateString) Then
            MostrarSexyAlertBox("incorrecta. La fecha de registro no puede ser mayor a la fecha actual.", "Alert")
            ddlTipoControl.SelectedValue = tipoControl
            tbFechaTipoControl.Text = fecha
            tbResultadoTipoControl.Text = resultado
            pnModalOtrosControles.Show()
            Exit Sub
        End If

        If ddlTipoControl.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            pnModalOtrosControles.Show()

            Exit Sub

        End If

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaTipoControl") Is Nothing Then

            dt = New DataTable("ListaTipoControl")

            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
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
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("CodigoRelacion") = id_codigo_fila + 1
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

        If CDate(tbFechaTipoControl.Text) > CDate(Today.ToShortDateString) Then
            MostrarSexyAlertBox("incorrecta. La fecha de registro no puede ser mayor a la fecha actual.", "Alert")
            ddlTipoControl.SelectedValue = tipoControl
            tbFechaTipoControl.Text = fecha
            tbResultadoTipoControl.Text = resultado
            pnModalOtrosControles.Show()
            Exit Sub
        End If

        If ddlTipoControl.SelectedValue = 0 Then

            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            'ddlTipoControl.SelectedValue = 0
            'tbFechaTipoControl.Text = Now.Date
            'tbResultadoTipoControl.Text = ""
            pnModalOtrosControles.Show()
            Exit Sub

        End If

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

            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoRelacion") = int_CodigoOriginal
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
            If auxdr.Item("CodigoRelacion").ToString = int_TipoControl Then
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
    Private Sub activarEditarTipoControl(ByVal int_Codigo As Integer, ByVal int_TipoControl As Integer, ByVal dt_fecha As Date, ByVal resultado As String)

        ddlTipoControl.SelectedValue = int_TipoControl
        hidenCodigoTipoControl.Value = int_Codigo
        tbFechaTipoControl.Text = dt_fecha
        tbResultadoTipoControl.Text = resultado
        pnModalOtrosControles.Show()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub gvDetalleTipoControl_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleTipoControl_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
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

#End Region

#Region "Manejo de Alertas - Emails"

    ''' <summary>
    ''' Recibe mensajes y los deriva a otro metodo que los visualizara cno animación de JQuery
    ''' </summary>
    ''' <param name="str_alertas">Mensaje que se quiere visualizar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
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
    ''' Fecha de Creación:     31/01/2011
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
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_CodigoUsuario As String = Obtener_CodigoFamiliarLogueado()
        Dim int_TipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(5, 82, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
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
