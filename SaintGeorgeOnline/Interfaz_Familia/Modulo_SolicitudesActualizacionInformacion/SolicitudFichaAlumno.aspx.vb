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
''' Módulo de Registro de Solicitudes de Actualización de Información del Alumno por solicitud de un familiar
''' </summary>
''' <remarks>
''' Código del Modulo:    5
''' Código de la Opción:  79
''' </remarks>
''' actualizado 20/01/2012
Partial Class Interfaz_Familia_Modulo_SolicitudesActualizacionInformacion_SolicitudFichaAlumno
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 0
    Private cod_Opcion As Integer = 12

#Region "Eventos"

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

                If Not Request.QueryString("codigoAlumno") Is Nothing Then
                    Dim str_CodigoAlumno As String = Request.QueryString("codigoAlumno")
                    ObtenerFicha(str_CodigoAlumno)

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
                'ObtenerFicha("20100051")

                verificarProfesaReligion()
                verificarReligionCatolica()
                ddlReligion_SelectedIndexChanged()
                rbBautizo_SelectedIndexChanged()
                rbPriComunion_SelectedIndexChanged()
                rbConfirmado_SelectedIndexChanged()

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
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

    '            If lbl_Bloque_DatosNacimiento.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_DatosNacimiento.Text = sender.text
    '            End If

    '            If lbl_Bloque_DatosPersonales.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_DatosPersonales.Text = sender.text
    '            End If

    '            If lbl_Bloque_DatosDomicilio.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_DatosDomicilio.Text = sender.text
    '            End If

    '            If lbl_Bloque_DatosReligiosos.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_DatosReligiosos.Text = sender.text
    '            End If

    '            If lbl_Bloque_DatosEmergencia.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_DatosEmergencia.Text = sender.text
    '                Exit Sub
    '            End If

    '            If lbl_Bloque_DatosOtros.ID = "lbl_" & sender.ValidationGroup Then
    '                lbl_Bloque_DatosOtros.Text = sender.text
    '            End If

    '        End If

    '        cont_views = cont_views + 1
    '    End While

    'End Sub

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
        'CargarPaginaPadre()

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

    Protected Sub rbReligion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        verificarProfesaReligion()
    End Sub

    Protected Sub ddlReligion_SelectedIndexChanged()
        verificarReligionCatolica()
    End Sub

    Protected Sub rbBautizo_SelectedIndexChanged()
        If rbBautizo.SelectedValue = 1 Then
            verificarBautizo(True)
        Else
            verificarBautizo(False)
        End If
    End Sub

    Protected Sub rbPriComunion_SelectedIndexChanged()
        If rbPriComunion.SelectedValue = 1 Then
            verificarPrimeraComunion(True)
        Else
            verificarPrimeraComunion(False)
        End If
    End Sub

    Protected Sub rbConfirmado_SelectedIndexChanged()
        If rbConfirmado.SelectedValue = 1 Then
            verificarConfirmacion(True)
        Else
            verificarConfirmacion(False)
        End If
    End Sub

   

#End Region

#Region "Metodos"



#Region "lista vive con el alumno"


    Protected Sub grwInformacionAdicional_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwInformacionAdicional.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Convert.ToBoolean(CType(e.Row.FindControl("lblViveConElla"), Label).Text()) Then
                CType(e.Row.FindControl("rblViveConElla"), RadioButtonList).Items(0).Selected = True
            Else
                CType(e.Row.FindControl("rblViveConElla"), RadioButtonList).Items(1).Selected = True
            End If
            If Convert.ToBoolean(CType(e.Row.FindControl("lblEsPagante"), Label).Text()) Then
                CType(e.Row.FindControl("ddlEncargadoPagar"), DropDownList).Items(0).Selected = True
            Else
                CType(e.Row.FindControl("ddlEncargadoPagar"), DropDownList).Items(1).Selected = True
            End If
        End If
        ''nombre	AL_CodigoAlumno	PT_Descripcion	RAF_ResponsablePago	RAF_ViveConAlumno	IF_CodigoIntegranteFamilia	RAF_CodigoRelAlumnosFamiliares
    End Sub

    Protected Sub ddlEncargadoPagar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim row As GridViewRow
        Dim indexRow As Integer = 0
        row = CType(sender, DropDownList).NamingContainer
        indexRow = row.RowIndex
        If CType(sender, DropDownList).Items(0).Selected = True Then

            For Each filas As GridViewRow In grwInformacionAdicional.Rows
                If filas.RowIndex <> indexRow Then
                    CType(filas.FindControl("ddlEncargadoPagar"), DropDownList).Items(1).Selected = True
                    CType(filas.FindControl("ddlEncargadoPagar"), DropDownList).Items(0).Selected = False
                End If
            Next

        End If


    End Sub

    Function listarFilas() As DataTable

        'lacion("codigoIntegranteFamilia").ToString()))
        'dbBase.AddInParameter(dbCommand, "@p_ResponsablePago", DbType.Boolean, Convert.ToBoolean(filasEntidadRelacion("esPagante")))
        'dbBase.AddInParameter(dbCommand, "@p_ViveConAlumno", DbType.Boolean, Convert.ToBoolean(filasEntidadRelacion("viveConElla")))
        Dim dtRes As New DataTable("relacionAlumno")
        Dim dcColummnas As DataColumn
        Dim filas As DataRow

        ''lblViveConElla
        ''ddlEncargadoPagar
        ''lblCodigoFamilia
        dcColummnas = New DataColumn("codigoIntegranteFamilia", GetType(Integer))
        dtRes.Columns.Add(dcColummnas)


        dcColummnas = New DataColumn("esPagante", GetType(Boolean))
        dtRes.Columns.Add(dcColummnas)


        dcColummnas = New DataColumn("viveConElla", GetType(Boolean))
        dtRes.Columns.Add(dcColummnas)

        For Each filasgrilla As GridViewRow In grwInformacionAdicional.Rows
            filas = dtRes.NewRow
            If CType(filasgrilla.FindControl("ddlEncargadoPagar"), DropDownList).Items(0).Selected Then
                filas("esPagante") = True
            Else
                filas("esPagante") = False

            End If
            If CType(filasgrilla.FindControl("rblViveConElla"), RadioButtonList).Items(0).Selected Then
                filas("viveConElla") = True
            Else
                filas("viveConElla") = False

            End If
            filas("codigoIntegranteFamilia") = Convert.ToInt32(CType(filasgrilla.FindControl("lblCodigoFamilia"), Label).Text)
            dtRes.Rows.Add(filas)
        Next

        Return dtRes


        Try

        Catch ex As Exception

        End Try
    End Function


#End Region

    Private Sub SetearAccionesAcceso()
        RegistrarAccesoPagina(5, 79)
    End Sub

    '''' <summary>
    '''' Carga el menu informativo(Bloques de información)
    '''' </summary>
    '''' <remarks>
    '''' Creador:               Juan Vento
    '''' Fecha de Creación:     18/01/2011
    '''' Modificado por:        _____________
    '''' Fecha de modificación: _____________ 
    '''' </remarks>
    'Private Sub cargar_MenuInformacion()
    '    Dim dt As DataTable

    '    dt = New DataTable("GruposInformacion")
    '    dt = Datos.agregarColumna(dt, "CodigoGrupo", "Integer")
    '    dt = Datos.agregarColumna(dt, "NombreGrupo", "String")
    '    dt = Datos.agregarColumna(dt, "CodigoProgramacion", "String")

    '    Dim dr As DataRow
    '    dr = dt.NewRow
    '    dr.Item("CodigoGrupo") = 1
    '    dr.Item("NombreGrupo") = "Personales"
    '    dr.Item("CodigoProgramacion") = "Bloque_DatosPersonales"
    '    dt.Rows.Add(dr)

    '    Dim dr_2 As DataRow
    '    dr_2 = dt.NewRow
    '    dr_2.Item("CodigoGrupo") = 2
    '    dr_2.Item("NombreGrupo") = "Nacimiento"
    '    dr_2.Item("CodigoProgramacion") = "Bloque_DatosNacimiento"
    '    dt.Rows.Add(dr_2)

    '    Dim dr_3 As DataRow
    '    dr_3 = dt.NewRow
    '    dr_3.Item("CodigoGrupo") = 3
    '    dr_3.Item("NombreGrupo") = "Domicilio"
    '    dr_3.Item("CodigoProgramacion") = "Bloque_DatosDomicilio"
    '    dt.Rows.Add(dr_3)

    '    Dim dr_5 As DataRow
    '    dr_5 = dt.NewRow
    '    dr_5.Item("CodigoGrupo") = 4
    '    dr_5.Item("NombreGrupo") = "Religioso"
    '    dr_5.Item("CodigoProgramacion") = "Bloque_DatosReligiosos"
    '    dt.Rows.Add(dr_5)

    '    Dim dr_7 As DataRow
    '    dr_7 = dt.NewRow
    '    dr_7.Item("CodigoGrupo") = 5
    '    dr_7.Item("NombreGrupo") = "Emergencia"
    '    dr_7.Item("CodigoProgramacion") = "Bloque_DatosEmergencia"
    '    dt.Rows.Add(dr_7)

    '    Dim dr_8 As DataRow
    '    dr_8 = dt.NewRow
    '    dr_8.Item("CodigoGrupo") = 6
    '    dr_8.Item("NombreGrupo") = "Otros"
    '    dr_8.Item("CodigoProgramacion") = "Bloque_DatosOtros"
    '    dt.Rows.Add(dr_8)

    '    dgv_GrupoInformacion.DataSource = dt
    '    dgv_GrupoInformacion.DataBind()

    'End Sub

    Private Sub Enviar_ActualizarDatos()
        'Dim Context As HttpContext
        'Context = HttpContext.Current
        'Context.Items.Add("CodigoFamiliar", "2")
        'Server.Transfer("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionFichaFamiliar.aspx")
        Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionFichaAlumno.aspx")
    End Sub

    ''' <summary>
    ''' Elimina las listas en memoria(ViewState) y cierra el formulario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Cancelar()

        ViewState.Remove("ListaIdiomas")
        ViewState.Remove("ListaProfesiones")
        ViewState.Remove("ListaClinicas")
        ViewState.Remove("ListaFacturacion")
        'Response.redirect(..)
        ViewState("ListaIdiomas") = Nothing
        ViewState("ListaProfesiones") = Nothing
        ViewState("ListaClinicas") = Nothing
        ViewState("ListaFacturacion") = Nothing


    End Sub

    ''' <summary>
    ''' Limpia los items de una lista desplegable
    ''' </summary>
    ''' <param name="combo">Nombre que identifica a la lista desplegable</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)

        Controles.limpiarCombo(combo, False, True)

    End Sub

    ''' <summary>
    ''' Carga una serie de listas desplegables 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombos()

        cargarComboTipoDocumento()
        cargarComboNacionalidades()
        cargarComboIdiomas()
        cargarComboReligiones()
        cargarComboUbigeo()
        cargarPaisesNacimiento()

        ''cargar los combos de ubigeo de nacimiento
        cargarComboNacimientoDepartamentos()
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipo de Documentos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     09/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoDocumento()

        Dim obj_BL_TipoDocIdentidad As New bl_TipoDocIdentidad
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_TipoDocIdentidad.FUN_LIS_TipoDocIdentidad("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlTipoDocumento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga los combos de Nacionalidades
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNacionalidades()

        Dim obj_BL_Nacionalidades As New bl_Nacionalidades
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Nacionalidades.FUN_LIS_Nacionalidades("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlNacionalidad1, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlNacionalidad2, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga los combos de idiomas
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboIdiomas()

        Dim obj_BL_Idiomas As New bl_Idiomas
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Idiomas.FUN_LIS_Idiomas("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlLenguaMaterna1, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlLenguaMaterna2, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo de Religiones
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboReligiones()

        Dim obj_BL_Religiones As New bl_Religiones
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Religiones.FUN_LIS_Religiones("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlReligion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub


    ''' <summary>
    ''' Carga el combo de Ubigeo(Departamento) y limpia los combos de Ubigeo dependientes (Provincia, Distrito)
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboUbigeo()

        cargarComboDomicilioDepartamentos()
        limpiarCombos(ddlDomicilioProvincia)
        limpiarCombos(ddlDomicilioDistrito)

    End Sub

    ''' <summary>
    ''' Carga el combo de Ubigeo Departamento
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDomicilioDepartamentos()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Departamentos(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlDomicilioDepartamento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo de Ubigeo Provincia
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDomicilioProvincia()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlDomicilioDepartamento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlDomicilioProvincia, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo de Ubigeo Distrito
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDomicilioDistrito()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlDomicilioDepartamento.SelectedValue, ddlDomicilioProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlDomicilioDistrito, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo de Ubigeo Departamento
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNacimientoDepartamentos()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Departamentos(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlDepartamentoNacimiento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Activa o desactiva una serie de campos del formulario dependiendo del valor del RadioButton Profesa Religion
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarProfesaReligion()

        If rbReligion.SelectedValue = 0 Then 'No
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

        Else 'Si
            ddlReligion.Enabled = True
        End If

    End Sub

    ''' <summary>
    ''' Activa o desactiva una serie de campos del formulario dependiendo del valor de la lista desplegable Religion
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarReligionCatolica()

        rbBautizo.Enabled = False
        tbLugarBautizo.Enabled = False
        tbAnioBautizo.Enabled = False

        rbPriComunion.Enabled = False
        tbLugarPriComunion.Enabled = False
        tbAnioPriComunion.Enabled = False

        rbConfirmado.Enabled = False
        tbLugarConfirmado.Enabled = False
        tbAnioConfirmado.Enabled = False

        If (ddlReligion.SelectedValue = 1 Or ddlReligion.SelectedValue = 2) Then

            rbBautizo.Enabled = True
            tbLugarBautizo.Enabled = True
            tbAnioBautizo.Enabled = True

            rbPriComunion.Enabled = True
            tbLugarPriComunion.Enabled = True
            tbAnioPriComunion.Enabled = True

            rbConfirmado.Enabled = True
            tbLugarConfirmado.Enabled = True
            tbAnioConfirmado.Enabled = True

            'ElseIf (ddlReligion.SelectedValue <> 2 And ddlReligion.SelectedValue <> 1) Then

            '    rbBautizo.Enabled = True
            '    tbLugarBautizo.Enabled = True
            '    tbAnioBautizo.Enabled = True

        End If

    End Sub

    ''' <summary>
    ''' Activa o desactiva una serie de campos del formulario dependiendo del valor de la variable estado
    ''' </summary>
    ''' <param name="estado">Valor de la variable(boolean)</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarBautizo(ByVal estado As Boolean)

        tbLugarBautizo.Enabled = estado
        tbAnioBautizo.Enabled = estado

    End Sub

    ''' <summary>
    ''' Activa o desactiva una serie de campos del formulario dependiendo del valor de la variable estado
    ''' </summary>
    ''' <param name="estado">Valor de la variable(boolean)</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarPrimeraComunion(ByVal estado As Boolean)

        tbLugarPriComunion.Enabled = estado
        tbAnioPriComunion.Enabled = estado

    End Sub

    ''' <summary>
    ''' Activa o desactiva una serie de campos del formulario dependiendo del valor de la variable estado
    ''' </summary>
    ''' <param name="estado">Valor de la variable(boolean)</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub verificarConfirmacion(ByVal estado As Boolean)

        tbLugarConfirmado.Enabled = estado
        tbAnioConfirmado.Enabled = estado

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
    Protected Sub ddlPaisNacimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPaisNacimiento.SelectedIndexChanged
        Try
            If ddlPaisNacimiento.SelectedValue <> 1 Then
                ddlDepartamentoNacimiento.SelectedValue = 0
                ddlDepartamentoNacimiento.Enabled = False
                ddlProvinciaNacimiento.SelectedValue = 0
                ddlProvinciaNacimiento.Enabled = False
                ddlDistritoNacimiento.SelectedValue = 0
                ddlDistritoNacimiento.Enabled = False
            Else
                ddlDepartamentoNacimiento.Enabled = True
                ddlProvinciaNacimiento.Enabled = True
                ddlDistritoNacimiento.Enabled = True
            End If
        Catch ex As Exception
        Finally

        End Try
    End Sub


    Protected Sub ddlDepartamentoNacimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamentoNacimiento.SelectedIndexChanged
        Try
            cargarComboNacimientoProvincia()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlProvinciaNacimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvinciaNacimiento.SelectedIndexChanged
        Try
            cargarComboNacimientoDistrito()
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Ficha Alumno"

    ''' <summary>
    ''' Verifica si el codigo enviado ya existe en el arreglo
    ''' </summary>
    ''' <param name="arrList">Arreglo de códigos</param>
    ''' <param name="item">Código a buscar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
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
    ''' Fecha de Creación:     18/01/2011
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
    ''' Valida la ficha de alumno antes de grabar
    ''' </summary>
    ''' <param name="str_Mensaje">Cadena de texto que tendra todos los mensajes de error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        Juan Vento
    ''' Fecha de modificación: 09/01/2012 
    ''' </remarks>
    Private Function validarFicha(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlTipoDocumento.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Documento de Identidad")
            result = False
        End If

        If tbNumDocumento.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Número de Documento")
            result = False
        End If

        If ddlNacionalidad1.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "1era Nacionalidad")
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

        'If tbCorreoElectronico.Text.Trim.Length = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Correo Electrónico")
        '    result = False
        'End If


        'If ddlTipoSeguro.SelectedValue = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo Seguro")
        '    result = False
        'ElseIf ddlTipoSeguro.SelectedValue = 1 Then
        '    Dim dt As DataTable
        '    If ViewState("ListaClinicas") Is Nothing Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Clinicas")
        '        result = False
        '    Else
        '        dt = ViewState("ListaClinicas")
        '        If dt.Rows.Count = 0 Then
        '            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Clinicas")
        '            result = False
        '        End If
        '    End If
        'ElseIf ddlTipoSeguro.SelectedValue = 2 Then

        '    If ddlCompaniaSeguro.SelectedValue = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Compañia Seguro")
        '        result = False
        '    End If
        '    If tbNumeroPoliza.Text.Trim.Length = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Número Poliza")
        '        result = False
        '    End If

        '    If rbVigenciaSeguro.SelectedValue = -1 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Vigencia de Seguro")
        '        result = False
        '    End If

        '    If rbVigenciaSeguro.SelectedValue = 0 Then

        '        If tbFechaVigenciaInicio.Text.Trim = "" Then
        '            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Fecha Vigencia de Inicio")
        '            result = False
        '        ElseIf IsDate(tbFechaVigenciaInicio.Text.Trim) = False Then
        '            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha Vigencia de Inicio")
        '            result = False
        '        End If

        '        If tbFechaVigenciaFin.Text.Trim = "" Then
        '            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Fecha Vigencia de Fin")
        '            result = False
        '        ElseIf IsDate(tbFechaVigenciaFin.Text.Trim) = False Then
        '            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha Vigencia de Fin")
        '            result = False
        '        End If

        '        If IsDate(tbFechaVigenciaInicio.Text.Trim) And IsDate(tbFechaVigenciaFin.Text.Trim) Then
        '            If (CType(tbFechaVigenciaInicio.Text, Date) > CType(tbFechaVigenciaFin.Text, Date)) Then
        '                str_alertas = Alertas.ObtenerAlerta(str_alertas, 7, "Fecha de Vigencia")
        '                result = False
        '            End If
        '        End If

        '    End If

        'Dim dt As DataTable
        'If ViewState("ListaClinicas") Is Nothing Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Clinicas")
        '    result = False
        'Else
        '    dt = ViewState("ListaClinicas")
        '    If dt.Rows.Count = 0 Then
        '        str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Clinicas")
        '        result = False
        '    End If
        'End If

        'If tbCompaniaAmbulancia.Text.Trim.Length = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Compañia Ambulancia")
        '    result = False
        'End If
        'If tbTelefonoAmbulancia.Text.Trim.Length = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Teléfono Ambulancia")
        '    result = False
        'End If
        'If rbCopiaSeguro.SelectedValue = -1 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Copia de Seguro")
        '    result = False
        'End If

        'End If

        If ddlPaisNacimiento.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "País de Nacimiento")
            result = False

        ElseIf ddlPaisNacimiento.SelectedValue = 1 Then

            If ddlDepartamentoNacimiento.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Departamento Nacimiento")
                result = False
            End If
            If ddlProvinciaNacimiento.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Provincia Nacimiento")
                result = False
            End If
            If ddlDistritoNacimiento.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Distrito Nacimiento")
                result = False
            End If

        End If

        If rbReligion.SelectedValue = 1 Then
            If ddlReligion.SelectedValue = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Religión")
                result = False
            End If
        End If

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


        If tbDomicilioUrbanizacion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Urbanización Domicilio")
            result = False
        End If

        If tbDomicilioDireccion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Dirección Domicilio")
            result = False
        End If

        If tbNombreCompletoEmergencia.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Nombre de familiar en caso de emergencia")
            result = False
        End If

        If tbTelfCasaEmergencia.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Teléfono de familiar en caso de emergencia")
            result = False
        End If

        Dim int_cantidad As Integer = 0

        For Each drv As GridViewRow In grwInformacionAdicional.Rows
            If CType(drv.FindControl("ddlEncargadoPagar"), DropDownList).SelectedValue = 1 Then
                int_cantidad = int_cantidad + 1
            End If
        Next

        If int_cantidad = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Encargado de Pago")
            result = False
        End If
        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Recupera los valores de los datos del alumno
    ''' </summary>
    ''' <param name="str_Codigo">Codigo del alumno, al que se le quiere realizar la actualizacion</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerFicha(ByVal str_Codigo As String)

        Dim BGColor As String = "#dcff7d"

        Dim obj_BL_FichaAlumno As New bl_Alumnos
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_FichaAlumno.FUN_GET_AlumnoVisualizacionActualizacionFamiliar(str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("DatosOriginales") = ds_Lista

        hd_CodigoPersonaSolicitante.Value = Obtener_CodigoFamiliarLogueado()

        lblNombreCompletoAlumno.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString

        'Situacion Actual
        'imgFotoAlumno.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web").ToString() & ds_Lista.Tables(0).Rows(0).Item("RutaFoto").ToString
        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString)
        hd_CodigoPersona.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersona").ToString)

        ''lblNombreCompleto.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString
        ''lblHouse.Text = ds_Lista.Tables(0).Rows(0).Item("DescHouse").ToString
        ' ''lblSituacionAnio.Text = ds_Lista.Tables(0).Rows(0).Item("DescEstadoAnioActualAlumno").ToString
        ''lblEstadoActual.Text = ds_Lista.Tables(0).Rows(0).Item("DescEstadoActualAlumno").ToString
        ''lblAnioActual.Text = ds_Lista.Tables(0).Rows(0).Item("DescAnioActualAlumno").ToString
        ''lblNSnGS.Text = ds_Lista.Tables(0).Rows(0).Item("ENSnGS").ToString

        'Datos Personales
        lblCodigo.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString
        lblCodigoEducando.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoEducando").ToString
        lblApellidoPaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString
        lblApellidoMaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString
        lblNombre.Text = ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString
        lblSexo.Text = ds_Lista.Tables(0).Rows(0).Item("DescSexo").ToString

        'lblTipoDocumento.Text = ds_Lista.Tables(0).Rows(0).Item("DescTipoDocIdentidad").ToString
        'lblNumDocumento.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString
        ddlTipoDocumento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoTipoDocIdentidad").ToString
        tbNumDocumento.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString

        'Datos de Nacimiento
        'lblNacRegistrado.Text = ds_Lista.Tables(0).Rows(0).Item("DescNacimientoRegistrado").ToString
        'lblFechaNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString
        If Convert.ToBoolean(ds_Lista.Tables(0).Rows(0).Item("CodigoNacimientoRegistrado").ToString) Then
            rblEsRegistrado.Items(0).Selected = True
        Else
            rblEsRegistrado.Items(1).Selected = True
        End If

        txtFechaNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString
        'lblPais.Text = ds_Lista.Tables(0).Rows(0).Item("DescPaisNacimiento").ToString
        'lblDepartamento.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoNacimientoDepartamento").ToString
        'lblProvincia.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoNacimientoProvincia").ToString
        'lblDistrito.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoNacimientoDistrito").ToString

        ddlPaisNacimiento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoPaisNacimiento").ToString
        ddlDepartamentoNacimiento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoNacimientoDepartamento").ToString ''
        cargarComboNacimientoProvincia()
        ddlProvinciaNacimiento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoNacimientoProvincia").ToString
        cargarComboNacimientoDistrito()
        ddlDistritoNacimiento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoNacimientoDistrito").ToString

        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
            ddlNacionalidad1.SelectedValue = ds_Lista.Tables(1).Rows(0).Item("CodigoNacionalidad").ToString
            hd_CodigoRelacionNacionalidadesPersonas1.Value = CInt(ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion").ToString)
            hd_CodigoRelacionNacionalidadesPersonas2.Value = 0

            If ds_Lista.Tables(1).Rows(0).Item("Origen") = "T" Then
                ddlNacionalidad1.Style.Add("background", BGColor)
            End If

            If ds_Lista.Tables(1).Rows.Count > 1 Then
                ddlNacionalidad2.SelectedValue = ds_Lista.Tables(1).Rows(1).Item("CodigoNacionalidad").ToString
                hd_CodigoRelacionNacionalidadesPersonas2.Value = CInt(ds_Lista.Tables(1).Rows(1).Item("CodigoRelacion").ToString)

                If ds_Lista.Tables(1).Rows(1).Item("Origen") = "T" Then
                    ddlNacionalidad2.Style.Add("background", BGColor)
                End If

            End If
        ElseIf ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") = -1 Then
            ddlNacionalidad1.SelectedValue = 0
            ddlNacionalidad2.SelectedValue = 0
            hd_CodigoRelacionNacionalidadesPersonas1.Value = 0
            hd_CodigoRelacionNacionalidadesPersonas2.Value = 0
        End If

        'Otros Datos
        If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then
            ddlLenguaMaterna1.SelectedValue = ds_Lista.Tables(2).Rows(0).Item("CodigoIdioma").ToString
            hd_CodigoRelacionIdiomasPersonas1.Value = CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion").ToString)
            hd_CodigoRelacionIdiomasPersonas2.Value = 0

            If ds_Lista.Tables(2).Rows(0).Item("Origen") = "T" Then
                ddlLenguaMaterna1.Style.Add("background", BGColor)
            End If

            If ds_Lista.Tables(2).Rows.Count > 1 Then
                ddlLenguaMaterna2.SelectedValue = ds_Lista.Tables(2).Rows(1).Item("CodigoIdioma").ToString
                hd_CodigoRelacionIdiomasPersonas2.Value = CInt(ds_Lista.Tables(2).Rows(1).Item("CodigoRelacion").ToString)

                If ds_Lista.Tables(2).Rows(1).Item("Origen") = "T" Then
                    ddlLenguaMaterna2.Style.Add("background", BGColor)
                End If

            End If
        ElseIf ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") = -1 Then
            ddlLenguaMaterna1.SelectedValue = 0
            ddlLenguaMaterna2.SelectedValue = 0
            hd_CodigoRelacionIdiomasPersonas1.Value = 0
            hd_CodigoRelacionIdiomasPersonas2.Value = 0
        End If


        tbCantidadHermanos.Text = ds_Lista.Tables(0).Rows(0).Item("CantidadHermanos").ToString
        tbPosicionHermanos.Text = ds_Lista.Tables(0).Rows(0).Item("PosicionEntreHermanos").ToString
        tbCorreoElectronico.Text = ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString
        tbCelular.Text = ds_Lista.Tables(0).Rows(0).Item("Celular").ToString

        'Datos Religiosos
        rbReligion.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion"))
        verificarProfesaReligion()

        If rbReligion.SelectedValue = 0 Then

            ddlReligion.SelectedValue = 0

            rbBautizo.SelectedValue = -1
            tbLugarBautizo.Text = ""
            tbAnioBautizo.Text = ""
            rbConfirmado.SelectedValue = -1
            tbLugarConfirmado.Text = ""
            tbAnioConfirmado.Text = ""
            rbPriComunion.SelectedValue = -1
            tbLugarPriComunion.Text = ""
            tbAnioPriComunion.Text = ""

        Else

            ddlReligion.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoReligion").ToString
            ddlReligion_SelectedIndexChanged()

            rbBautizo.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("Bautizo"))
            If rbBautizo.SelectedValue = 1 Then
                tbLugarBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoLugar").ToString
                tbAnioBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoAnio").ToString
            Else
                tbLugarBautizo.Text = ""
                tbAnioBautizo.Text = ""
            End If
            rbBautizo_SelectedIndexChanged()

            If ddlReligion.SelectedValue = 2 Then 'Religion Catolica

                rbPriComunion.SelectedValue = IIf(ds_Lista.Tables(0).Rows(0).Item("PrimeraComunion").ToString.Length = 0, -1, Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("PrimeraComunion")))
                If rbPriComunion.SelectedValue = 1 Then
                    tbLugarPriComunion.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionLugar").ToString
                    tbAnioPriComunion.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionAnio").ToString
                Else
                    tbLugarPriComunion.Text = ""
                    tbAnioPriComunion.Text = ""
                End If
                rbPriComunion_SelectedIndexChanged()

                rbConfirmado.SelectedValue = IIf(ds_Lista.Tables(0).Rows(0).Item("Confirmacion").ToString.Length = 0, -1, Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("Confirmacion")))
                If rbConfirmado.SelectedValue = 1 Then
                    tbLugarConfirmado.Text = ds_Lista.Tables(0).Rows(0).Item("ConfirmacionLugar").ToString
                    tbAnioConfirmado.Text = ds_Lista.Tables(0).Rows(0).Item("ComfirmacionAnio").ToString
                Else
                    tbLugarConfirmado.Text = ""
                    tbAnioConfirmado.Text = ""
                End If
                rbConfirmado_SelectedIndexChanged()

            Else
                tbLugarPriComunion.Text = ""
                tbAnioPriComunion.Text = ""
                tbLugarConfirmado.Text = ""
                tbAnioConfirmado.Text = ""
            End If

        End If

        'Datos Domicilio
        tbDomicilioUrbanizacion.Text = ds_Lista.Tables(0).Rows(0).Item("Urbanizacion").ToString
        tbDomicilioDireccion.Text = ds_Lista.Tables(0).Rows(0).Item("Direccion").ToString
        tbDomicilioReferencia.Text = ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomicilio").ToString
        tbDomicilioTelefono.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoCasa").ToString
        rbDomicilioAccesoInternet.SelectedValue = Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("AccesoInternetDomicilio"))

        ddlDomicilioDepartamento.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioDepartamento").ToString
        cargarComboDomicilioProvincia()
        ddlDomicilioProvincia.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioProvincia").ToString
        cargarComboDomicilioDistrito()
        ddlDomicilioDistrito.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilioDistrito").ToString

        'Datos Aviso en caso de emergencia
        tbNombreCompletoEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("NombreContactoAvisoEmergencia").ToString
        tbTelfCasaEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoCasaContactoAvisoEmergencia").ToString
        tbTelfOficinaEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoOficinaContactoAvisoEmergencia").ToString
        tbTelfMovilEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("CellContactoAvisoEmergencia").ToString

        'Datos de apoderado
        grwInformacionAdicional.DataSource = ds_Lista.Tables(5)
        grwInformacionAdicional.DataBind()
    End Sub

    Private Sub cargarPaisesNacimiento()
        Try
            ''FUN_LIS_Paises
            Dim dst As New DataSet
            dst = New bl_Ubigeo().FUN_LIS_Paises(True)
            ddlPaisNacimiento.DataSource = dst.Tables(0)
            ddlPaisNacimiento.DataTextField = "PI_Descripcion"
            ddlPaisNacimiento.DataValueField = "PI_CodigoPais"
            ddlPaisNacimiento.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Carga el combo de Ubigeo Provincia
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNacimientoProvincia()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlDepartamentoNacimiento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlProvinciaNacimiento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo de Ubigeo Distrito
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNacimientoDistrito()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlDepartamentoNacimiento.SelectedValue, ddlProvinciaNacimiento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlDistritoNacimiento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Recupera los valores de los datos del alumno
    ''' </summary>
    ''' <param name="tipo">Tipo de accion que se realizara</param>
    ''' <param name="be_Alumno">Entidad donde se setearan los valores de los campos del formulario</param>
    ''' <param name="BoolGrabar">Variable que indica si se va a grabar o no el formulario</param>
    ''' <param name="arrBloques">Codigo</param>
    ''' <param name="dr">Codigo</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function SeteoReligion(ByVal tipo As Integer, ByVal be_Alumno As be_Alumnos, _
                                 ByRef BoolGrabar As Boolean, ByRef arrBloques As ArrayList, ByVal dr As DataRow) As be_Alumnos
        Select Case tipo
            Case 1 'No profesa religion
                be_Alumno.CodigoReligion = -1
                be_Alumno.Bautizo = -2
                be_Alumno.BautizoLugar = ""
                be_Alumno.BautizoAnio = -2
                be_Alumno.Confirmacion = -2
                be_Alumno.ConfirmacionLugar = ""
                be_Alumno.ConfirmacionAnio = -2
                be_Alumno.PrimeraComunion = -2
                be_Alumno.PrimeraComunionLugar = ""
                be_Alumno.PrimeraComunionAnio = -2

            Case 2 ' Profesa Religion

                If Convert.ToInt32(dr.Item("Bautizo")) = rbBautizo.SelectedValue Then
                    be_Alumno.Bautizo = -1
                Else
                    be_Alumno.Bautizo = rbBautizo.SelectedValue
                    BoolGrabar = True
                    arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_Bautizo").ToString))
                End If

                If rbBautizo.SelectedValue = 1 Then

                    If dr.Item("BautizoLugar").ToString = tbLugarBautizo.Text.Trim Then
                        be_Alumno.BautizoLugar = Nothing
                    Else
                        be_Alumno.BautizoLugar = tbLugarBautizo.Text.Trim
                        BoolGrabar = True
                        arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_BautizoLugar").ToString))
                    End If

                    If dr.Item("BautizoAnio").ToString = tbAnioBautizo.Text.Trim Then
                        be_Alumno.BautizoAnio = -1
                    Else
                        be_Alumno.BautizoAnio = tbAnioBautizo.Text.Trim
                        BoolGrabar = True
                        arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_BautizoAnio").ToString))
                    End If

                Else

                    be_Alumno.BautizoLugar = ""
                    be_Alumno.BautizoAnio = -2
                    arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_BautizoLugar").ToString))
                    arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_BautizoAnio").ToString))

                End If

                'analisis de confirmacion 
                If dr.Item("Confirmacion").ToString = "True" Or dr.Item("Confirmacion").ToString = "False" Then 'Real

                    If ddlReligion.SelectedValue = 2 Then ' la religion es catolica
                        If Convert.ToInt32(dr.Item("Confirmacion")) = rbConfirmado.SelectedValue Then
                            be_Alumno.Confirmacion = -1
                        Else
                            be_Alumno.Confirmacion = rbConfirmado.SelectedValue
                            BoolGrabar = True
                            arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_Confirmacion").ToString))
                        End If

                        If rbConfirmado.SelectedValue = 1 Then ' Se ha confirmado
                            If dr.Item("ConfirmacionLugar").ToString = tbLugarConfirmado.Text.Trim Then
                                be_Alumno.ConfirmacionLugar = Nothing
                            Else
                                be_Alumno.ConfirmacionLugar = tbLugarConfirmado.Text.Trim
                                BoolGrabar = True
                                arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_ConfirmacionLugar").ToString))
                            End If

                            If IIf(dr.Item("ComfirmacionAnio").ToString = "", -1, dr.Item("ComfirmacionAnio").ToString) = Val(tbAnioConfirmado.Text.Trim) Then
                                be_Alumno.ConfirmacionAnio = -1
                            Else
                                be_Alumno.ConfirmacionAnio = Val(tbAnioConfirmado.Text.Trim)
                                BoolGrabar = True
                                arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_ComfirmacionAnio").ToString))
                            End If
                        Else ' No se ha confirmado
                            BoolGrabar = True
                            be_Alumno.ConfirmacionLugar = ""
                            be_Alumno.ConfirmacionAnio = -2
                            arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_ConfirmacionLugar").ToString))
                            arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_ComfirmacionAnio").ToString))
                        End If

                    Else ' la religion no es catolica
                        BoolGrabar = True
                        be_Alumno.Confirmacion = -2
                        be_Alumno.ConfirmacionLugar = ""
                        be_Alumno.ConfirmacionAnio = -2
                        arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_Confirmacion").ToString))
                        arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_ConfirmacionLugar").ToString))
                        arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_ComfirmacionAnio").ToString))
                    End If

                End If

                'analisis de primera comunion 
                If dr.Item("PrimeraComunion").ToString = "True" Or dr.Item("PrimeraComunion").ToString = "False" Then 'Real

                    If ddlReligion.SelectedValue = 2 Then ' la religion es catolica
                        If Convert.ToInt32(dr.Item("PrimeraComunion")) = rbPriComunion.SelectedValue Then
                            be_Alumno.PrimeraComunion = -1
                        Else
                            be_Alumno.PrimeraComunion = rbPriComunion.SelectedValue
                            BoolGrabar = True
                            arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_PrimeraComunion").ToString))
                        End If

                        If rbPriComunion.SelectedValue = 1 Then ' Ha tenido primera comunion
                            If dr.Item("PrimeraComunionLugar").ToString = tbLugarPriComunion.Text.Trim Then
                                be_Alumno.PrimeraComunionLugar = Nothing
                            Else
                                be_Alumno.PrimeraComunionLugar = tbLugarPriComunion.Text.Trim
                                BoolGrabar = True
                                arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_PrimeraComunionLugar").ToString))
                            End If

                            If IIf(dr.Item("PrimeraComunionAnio").ToString = "", -1, dr.Item("PrimeraComunionAnio").ToString) = Val(tbAnioPriComunion.Text.Trim) Then
                                be_Alumno.PrimeraComunionAnio = -1
                            Else
                                be_Alumno.PrimeraComunionAnio = Val(tbAnioPriComunion.Text.Trim)
                                BoolGrabar = True
                                arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_PrimeraComunionAnio").ToString))
                            End If
                        Else ' No ha tenido primera comunion
                            BoolGrabar = True
                            be_Alumno.PrimeraComunionLugar = ""
                            be_Alumno.PrimeraComunionAnio = -2
                            arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_PrimeraComunionLugar").ToString))
                            arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_PrimeraComunionAnio").ToString))
                        End If

                    Else ' la religion no es catolica
                        BoolGrabar = True
                        be_Alumno.PrimeraComunion = -2
                        be_Alumno.PrimeraComunionLugar = ""
                        be_Alumno.PrimeraComunionAnio = -2
                        arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_PrimeraComunion").ToString))
                        arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_PrimeraComunionLugar").ToString))
                        arrBloques = verificarArreglo(arrBloques, CInt(dr.Item("Bloque_PrimeraComunionAnio").ToString))
                    End If

                End If

        End Select

        Return be_Alumno

    End Function

    ''' <summary>
    ''' Registra la solicitud de actualizacion de datos del alumno
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarFicha()

        Dim str_mensajeError As String = ""

        Dim obj_BE_Alumno As New be_Alumnos
        Dim obj_BE_FichaSeguroAlumno As New be_FichaSeguroAlumno
        Dim obj_BL_Alumno As New bl_Alumnos
        Dim ojb_BE_SolicitudActualizacionFichaAlumnos As New be_SolicitudActualizacionFichaAlumnos

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

        'Seteo de valores dependientes de RadioButtonList
        If rbReligion.SelectedValue = 0 Then 'No

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

        ojb_BE_SolicitudActualizacionFichaAlumnos.CodigoPeronsaSolicitante = hd_CodigoPersonaSolicitante.Value
        obj_BE_Alumno.CodigoAlumno = hd_Codigo.Value
        obj_BE_Alumno.CodigoPersona = hd_CodigoPersona.Value

        'Datos Personales

        If ds_Lista.Tables(0).Rows(0).Item("CodigoTipoDocIdentidad").ToString = ddlTipoDocumento.SelectedValue Then
            obj_BE_Alumno.CodigoTipoDocIdentidad = -1
        Else
            obj_BE_Alumno.CodigoTipoDocIdentidad = ddlTipoDocumento.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoTipoDocIdentidad").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString = tbNumDocumento.Text.Trim Then
            obj_BE_Alumno.NumeroDocIdentidad = Nothing
        Else
            obj_BE_Alumno.NumeroDocIdentidad = tbNumDocumento.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_NumeroDocIdentidad").ToString))
        End If


        If ds_Lista.Tables(0).Rows(0).Item("CantidadHermanos").ToString = tbCantidadHermanos.Text.Trim Then
            obj_BE_Alumno.CantidadHermanos = -1
        Else
            obj_BE_Alumno.CantidadHermanos = tbCantidadHermanos.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CantidadHermanos").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("PosicionEntreHermanos").ToString = tbPosicionHermanos.Text.Trim Then
            obj_BE_Alumno.PosicionEntreHermanos = -1
        Else
            obj_BE_Alumno.PosicionEntreHermanos = tbPosicionHermanos.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_PosicionEntreHermanos").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString = tbCorreoElectronico.Text.Trim Then
            obj_BE_Alumno.EmailPersonal = Nothing
        Else
            obj_BE_Alumno.EmailPersonal = tbCorreoElectronico.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_EmailPersonal").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("Celular").ToString = tbCelular.Text.Trim Then
            obj_BE_Alumno.Celular = Nothing
        Else
            obj_BE_Alumno.Celular = tbCelular.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_Celular").ToString))
        End If


        'Datos Religiosos

        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion")) = rbReligion.SelectedValue Then
            obj_BE_Alumno.ProfesaReligion = -1
        Else
            obj_BE_Alumno.ProfesaReligion = rbReligion.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ProfesaReligion").ToString))
        End If

        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion")) = rbReligion.SelectedValue Then

            If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion")) = 0 Then 'Si no profesa religion

                obj_BE_Alumno = SeteoReligion(1, obj_BE_Alumno, BoolGrabar, arrBloques, ds_Lista.Tables(0).Rows(0))

            ElseIf Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("ProfesaReligion")) = 1 Then 'Si profesa religion

                If ds_Lista.Tables(0).Rows(0).Item("CodigoReligion").ToString = ddlReligion.SelectedValue Then
                    obj_BE_Alumno.CodigoReligion = -1
                Else
                    obj_BE_Alumno.CodigoReligion = ddlReligion.SelectedValue
                    BoolGrabar = True
                    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoReligion").ToString))
                End If

                obj_BE_Alumno = SeteoReligion(2, obj_BE_Alumno, BoolGrabar, arrBloques, ds_Lista.Tables(0).Rows(0))

            End If

        Else ' Profesa Religion DB <> Profesa Religion Form

            If rbReligion.SelectedValue = 0 Then 'Si no profesa religion

                obj_BE_Alumno.ProfesaReligion = rbReligion.SelectedValue
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ProfesaReligion").ToString))
                obj_BE_Alumno = SeteoReligion(1, obj_BE_Alumno, BoolGrabar, arrBloques, ds_Lista.Tables(0).Rows(0))

            ElseIf rbReligion.SelectedValue = 1 Then 'Si profesa religion

                obj_BE_Alumno.ProfesaReligion = rbReligion.SelectedValue
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ProfesaReligion").ToString))

                BoolGrabar = True
                obj_BE_Alumno.CodigoReligion = ddlReligion.SelectedValue
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoReligion").ToString))

                obj_BE_Alumno.Bautizo = rbBautizo.SelectedValue
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_Bautizo").ToString))

                obj_BE_Alumno.BautizoLugar = tbLugarBautizo.Text.Trim
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_BautizoLugar").ToString))

                obj_BE_Alumno.BautizoAnio = Val(tbAnioBautizo.Text.Trim) 'IIf(tbAnioBautizo.Text.Trim.Length = 0, 0, tbAnioBautizo.Text.Trim)
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_BautizoAnio").ToString))

                If (ddlReligion.SelectedValue = 2 Or ddlReligion.SelectedValue = 1) Then ' Si su religion es catolica o cristiana

                    obj_BE_Alumno.Confirmacion = rbConfirmado.SelectedValue
                    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_Confirmacion").ToString))
                    If rbConfirmado.SelectedValue = 1 Then ' Si tuvo confirmacion
                        obj_BE_Alumno.ConfirmacionLugar = tbLugarConfirmado.Text.Trim
                        arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ConfirmacionLugar").ToString))
                        obj_BE_Alumno.ConfirmacionAnio = Val(tbAnioConfirmado.Text.Trim)
                        arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ComfirmacionAnio").ToString))
                    Else
                        obj_BE_Alumno.ConfirmacionLugar = ""
                        obj_BE_Alumno.ConfirmacionAnio = -2
                        arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ConfirmacionLugar").ToString))
                        arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ComfirmacionAnio").ToString))
                    End If

                    obj_BE_Alumno.PrimeraComunion = rbPriComunion.SelectedValue
                    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_PrimeraComunion").ToString))
                    If rbPriComunion.SelectedValue = 1 Then ' Si tuvo primera comunion
                        obj_BE_Alumno.PrimeraComunionLugar = tbLugarPriComunion.Text.Trim
                        arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_PrimeraComunionLugar").ToString))
                        obj_BE_Alumno.PrimeraComunionAnio = Val(tbAnioPriComunion.Text.Trim)
                        arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_PrimeraComunionAnio").ToString))
                    Else
                        obj_BE_Alumno.ConfirmacionLugar = ""
                        obj_BE_Alumno.ConfirmacionAnio = -2
                        arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_PrimeraComunionLugar").ToString))
                        arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_PrimeraComunionAnio").ToString))
                    End If

                Else ' si no es religion catolica
                    obj_BE_Alumno.Confirmacion = -2
                    obj_BE_Alumno.ConfirmacionLugar = ""
                    obj_BE_Alumno.ConfirmacionAnio = -2
                    obj_BE_Alumno.PrimeraComunion = -2
                    obj_BE_Alumno.PrimeraComunionLugar = ""
                    obj_BE_Alumno.PrimeraComunionAnio = -2
                End If

            End If
        End If



        'Datos domicilio
        If ds_Lista.Tables(0).Rows(0).Item("Urbanizacion").ToString = tbDomicilioUrbanizacion.Text.Trim Then
            obj_BE_Alumno.Urbanizacion = Nothing
        Else
            obj_BE_Alumno.Urbanizacion = tbDomicilioUrbanizacion.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_Urbanizacion").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("Direccion").ToString = tbDomicilioDireccion.Text.Trim Then
            obj_BE_Alumno.Direccion = Nothing
        Else
            obj_BE_Alumno.Direccion = tbDomicilioDireccion.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_Direccion").ToString))
        End If


        If IIf(ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilio").ToString.Length = 0, "000000", ds_Lista.Tables(0).Rows(0).Item("CodigoUbigeoDomicilio").ToString) = _
            IIf(ddlDomicilioDepartamento.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioDepartamento.SelectedValue.ToString, ddlDomicilioDepartamento.SelectedValue.ToString) & _
            IIf(ddlDomicilioProvincia.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioProvincia.SelectedValue.ToString, ddlDomicilioProvincia.SelectedValue.ToString) & _
            IIf(ddlDomicilioDistrito.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioDistrito.SelectedValue.ToString, ddlDomicilioDistrito.SelectedValue.ToString) _
        Then
            obj_BE_Alumno.CodigoUbigeo = Nothing
        Else : obj_BE_Alumno.CodigoUbigeo = IIf(ddlDomicilioDepartamento.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioDepartamento.SelectedValue.ToString, ddlDomicilioDepartamento.SelectedValue.ToString) & _
                                            IIf(ddlDomicilioProvincia.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioProvincia.SelectedValue.ToString, ddlDomicilioProvincia.SelectedValue.ToString) & _
                                            IIf(ddlDomicilioDistrito.SelectedValue.ToString.Length = 1, "0" & ddlDomicilioDistrito.SelectedValue.ToString, ddlDomicilioDistrito.SelectedValue.ToString)
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CodigoUbigeoDomicilio").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomicilio").ToString = tbDomicilioReferencia.Text.Trim Then
            obj_BE_Alumno.ReferenciaDomiciliaria = Nothing
        Else
            obj_BE_Alumno.ReferenciaDomiciliaria = tbDomicilioReferencia.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_ReferenciaDomicilio").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("TelefonoCasa").ToString = tbDomicilioTelefono.Text.Trim Then

            obj_BE_Alumno.TelefonoCasa = Nothing
        Else
            obj_BE_Alumno.TelefonoCasa = tbDomicilioTelefono.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_TelefonoCasa").ToString))
        End If

        If Convert.ToInt32(ds_Lista.Tables(0).Rows(0).Item("AccesoInternetDomicilio")) = rbDomicilioAccesoInternet.SelectedValue Then
            obj_BE_Alumno.AccesoInternet = -1
        Else
            obj_BE_Alumno.AccesoInternet = rbDomicilioAccesoInternet.SelectedValue
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_AccesoInternetDomicilio").ToString))
        End If

        'Datos en caso de emergencia
        If ds_Lista.Tables(0).Rows(0).Item("NombreContactoAvisoEmergencia").ToString = tbNombreCompletoEmergencia.Text.Trim Then
            obj_BE_Alumno.NombreContactoAvisoEmergencia = Nothing
        Else
            obj_BE_Alumno.NombreContactoAvisoEmergencia = tbNombreCompletoEmergencia.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_NombreContactoAvisoEmergencia").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("TelefonoCasaContactoAvisoEmergencia").ToString = tbTelfCasaEmergencia.Text.Trim Then
            obj_BE_Alumno.TelfCasaContactoAvisoEmergencia = Nothing
        Else
            obj_BE_Alumno.TelfCasaContactoAvisoEmergencia = tbTelfCasaEmergencia.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_TelefonoCasaContactoAvisoEmergencia").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("CellContactoAvisoEmergencia").ToString = tbTelfMovilEmergencia.Text.Trim Then
            obj_BE_Alumno.CellContactoAvisoEmergencia = Nothing
        Else
            obj_BE_Alumno.CellContactoAvisoEmergencia = tbTelfMovilEmergencia.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_CellContactoAvisoEmergencia").ToString))
        End If

        If ds_Lista.Tables(0).Rows(0).Item("TelefonoOficinaContactoAvisoEmergencia").ToString = tbTelfOficinaEmergencia.Text.Trim Then

            obj_BE_Alumno.TelfOficinaContactoAvisoEmergencia = Nothing
        Else
            obj_BE_Alumno.TelfOficinaContactoAvisoEmergencia = tbTelfOficinaEmergencia.Text.Trim
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_TelefonoOficinaContactoAvisoEmergencia").ToString))
        End If


        'Detalle
        Dim objDS_Detalle As New DataSet

        'Detalle Nacionalidades
        Dim objDT_Nacionalidad As DataTable
        objDT_Nacionalidad = New DataTable("ListaNacionalidad")
        objDT_Nacionalidad = Datos.agregarColumna(objDT_Nacionalidad, "index", "String")
        objDT_Nacionalidad = Datos.agregarColumna(objDT_Nacionalidad, "CodigoNacionalidad", "String")

        Dim dr_Nacionalidad As DataRow
        If ddlNacionalidad1.SelectedValue <> 0 Then
            dr_Nacionalidad = objDT_Nacionalidad.NewRow
            dr_Nacionalidad.Item("index") = 1
            dr_Nacionalidad.Item("CodigoNacionalidad") = ddlNacionalidad1.SelectedValue
            objDT_Nacionalidad.Rows.Add(dr_Nacionalidad) : End If

        If ddlNacionalidad2.SelectedValue <> 0 Then
            dr_Nacionalidad = objDT_Nacionalidad.NewRow
            dr_Nacionalidad.Item("index") = 2
            dr_Nacionalidad.Item("CodigoNacionalidad") = ddlNacionalidad2.SelectedValue
            objDT_Nacionalidad.Rows.Add(dr_Nacionalidad) : End If

        miDetalle = Comparar2DataTable(ds_Lista.Tables(1), objDT_Nacionalidad, 1)
        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(1).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(1).Rows(0).Item("Origen") = "T" Then
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(1).Rows(0).Item("CodigoBloqueInformacion").ToString))
            Else
                objDT_Nacionalidad.Rows.Clear()
            End If
        End If

        'Detalle Idiomas
        Dim objDT_Idioma As DataTable
        objDT_Idioma = New DataTable("ListaIdiomas")
        objDT_Idioma = Datos.agregarColumna(objDT_Idioma, "index", "String")
        objDT_Idioma = Datos.agregarColumna(objDT_Idioma, "CodigoIdioma", "String")

        Dim dr_Idioma As DataRow
        If ddlLenguaMaterna1.SelectedValue <> 0 Then
            dr_Idioma = objDT_Idioma.NewRow
            dr_Idioma.Item("index") = 1
            dr_Idioma.Item("CodigoIdioma") = ddlLenguaMaterna1.SelectedValue
            objDT_Idioma.Rows.Add(dr_Idioma) : End If

        If ddlLenguaMaterna2.SelectedValue <> 0 Then
            dr_Idioma = objDT_Idioma.NewRow
            dr_Idioma.Item("index") = 2
            dr_Idioma.Item("CodigoIdioma") = ddlLenguaMaterna2.SelectedValue
            objDT_Idioma.Rows.Add(dr_Idioma) : End If

        miDetalle = 0
        miDetalle = Comparar2DataTable(ds_Lista.Tables(2), objDT_Idioma, 2)

        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(2).Rows(0).Item("Origen") = "T" Then
                BoolGrabar = True
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(2).Rows(0).Item("CodigoBloqueInformacion").ToString))
            Else
                objDT_Idioma.Rows.Clear()
            End If
        End If

        ''

        Dim fechaNacimiento As DateTime
        If txtFechaNacimiento.Text.Trim <> "" Then
            fechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text.Trim)
            BoolGrabar = True
        Else
            fechaNacimiento = Nothing
        End If
        Dim esRegistradoNacimiento As Boolean

        If rblEsRegistrado.Items(0).Selected Then
            esRegistradoNacimiento = True
        ElseIf rblEsRegistrado.Items(1).Selected Then
            esRegistradoNacimiento = False
        End If


        ''
        Dim dtUbigeo As New DataTable("ubigeoNacimiento")
        Dim dcUbigeo As DataColumn
        Dim filaUbigeo As DataRow


        dcUbigeo = New DataColumn("codPais", GetType(Integer))
        dtUbigeo.Columns.Add(dcUbigeo)
        dcUbigeo = New DataColumn("codDepartamento", GetType(String))
        dtUbigeo.Columns.Add(dcUbigeo)
        dcUbigeo = New DataColumn("codProvincia", GetType(String))
        dtUbigeo.Columns.Add(dcUbigeo)
        dcUbigeo = New DataColumn("codDistrito", GetType(String))
        dtUbigeo.Columns.Add(dcUbigeo)

        filaUbigeo = dtUbigeo.NewRow()

        If ddlPaisNacimiento.SelectedValue = 0 Then
            filaUbigeo("codPais") = 0
        Else
            filaUbigeo("codPais") = CInt(ddlPaisNacimiento.SelectedValue.ToString())
        End If

        If ddlDepartamentoNacimiento.SelectedValue = 0 Then
            filaUbigeo("codDepartamento") = "00"
        Else
            filaUbigeo("codDepartamento") = ddlDepartamentoNacimiento.SelectedValue
            BoolGrabar = True
        End If

        If ddlProvinciaNacimiento.SelectedValue = 0 Then
            filaUbigeo("codProvincia") = "00"
        Else
            filaUbigeo("codProvincia") = ddlProvinciaNacimiento.SelectedValue
            BoolGrabar = True
        End If
        If ddlDistritoNacimiento.SelectedValue = 0 Then
            filaUbigeo("codDistrito") = "00"
        Else
            filaUbigeo("codDistrito") = ddlDistritoNacimiento.SelectedValue
            BoolGrabar = True
        End If
        dtUbigeo.Rows.Add(filaUbigeo)


        Dim dtOtros As New DataTable
        dtOtros = listarFilas()
        ''
        If BoolGrabar Then
            'Agrego las DataTable a mi DataSet
            objDS_Detalle.Tables.Add(objDT_Nacionalidad)
            objDS_Detalle.Tables.Add(objDT_Idioma)
            'objDS_Detalle.Tables.Add(objDT_Clinica)
            objDS_Detalle.Tables.Add(dtUbigeo)
            objDS_Detalle.Tables.Add(dtOtros)

            str_Perfiles = obtenerPerfiles(ds_Lista.Tables(4), arrBloques)

            Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
            Dim int_CodigoUsuario As Integer = Obtener_CodigoFamiliarLogueado()

            usp_valor = obj_BL_Alumno.FUN_INS_AlumnoTemp(ojb_BE_SolicitudActualizacionFichaAlumnos, _
                                                         obj_BE_Alumno, _
                                                         obj_BE_FichaSeguroAlumno, _
                                                         str_Perfiles, _
                                                         objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion, fechaNacimiento, esRegistradoNacimiento)
        Else
            'MostrarSexyAlertBox(Alertas.ObtenerAlerta(4, ""), "Alert")
        End If

        If usp_valor > 0 Then

            ' Si se registra la solicitud, registro en el log de pasos de matrícula
            Dim int_CodigoPasoMatricula As Integer = 7
            RegistrarPasoMatricula(int_CodigoPasoMatricula, hiddenCodigoAnioAcademico.Value, hiddenCodigoAlumno.Value, hiddenCodigoFamiliar.Value, 0)

            ' Envio de email a responsables de validación
            ' enviarEmailResponsablesValidacion(str_Perfiles, usp_valor)

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
    ''' Compara 2 conjuntos de datos (DataTable)
    ''' </summary>
    ''' <param name="dtOriginal">Conjunto de datos original (proveninente de la base de datos)</param>
    ''' <param name="dtActualizar">Conjunto de datos ha aztualizar (modificado en el formulario)</param>
    ''' <param name="caso">El tipo de comparación a realizar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function Comparar2DataTable(ByVal dtOriginal As DataTable, ByVal dtActualizar As DataTable, ByVal caso As Integer) As Integer

        'Comparo los DataTables
        Dim rpta As Integer = 0

        If Not dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows.Count = 0 Then ' ambas listas están vacias
            Return 0
        End If

        If dtOriginal.Rows.Count <> dtActualizar.Rows.Count Then

            Return 1 ' Los DataTables son diferentes

        Else 'Si tienen el mismo numero de elementos los comparo 1 por 1

            Select Case caso
                Case 1

                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoNacionalidad") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoNacionalidad") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoNacionalidad") > 0 Then

                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If dtOriginal.Rows(i).Item("CodigoNacionalidad") <> dtActualizar.Rows(i).Item("CodigoNacionalidad") Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next
                        End If
                        Return 0
                    End If

                Case 2

                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoIdioma") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoIdioma") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoIdioma") > 0 Then
                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If dtOriginal.Rows(i).Item("CodigoIdioma") <> dtActualizar.Rows(i).Item("CodigoIdioma") Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next
                        End If
                        Return 0
                    End If

                Case 3

                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoTipoDiscapacidad") = -1 Then
                        Return 0
                    Else
                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoTipoDiscapacidad") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoTipoDiscapacidad") > 0 Then
                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If (dtOriginal.Rows(i).Item("CodigoTipoDiscapacidad") <> dtActualizar.Rows(i).Item("CodigoTipoDiscapacidad")) _
                                    Or (dtOriginal.Rows(i).Item("DescripcionDiscapacidad") <> dtActualizar.Rows(i).Item("DescripcionDiscapacidad")) Then
                                    Return 1 ' Me basta con 1 solo registro diferente para grabar el nuevo Detalle(DataTable)
                                End If
                            Next
                        End If
                        Return 0
                    End If

                Case 4

                    If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoClinica") = -1 Then
                        Return 0
                    Else

                        If dtOriginal.Rows(0).Item("CodigoRelacion") = -1 And dtActualizar.Rows(0).Item("CodigoClinica") > 0 Then
                            Return 1
                        ElseIf dtOriginal.Rows(0).Item("CodigoRelacion") > 0 And dtActualizar.Rows(0).Item("CodigoClinica") > 0 Then
                            For i As Integer = 0 To dtOriginal.Rows.Count - 1
                                If dtOriginal.Rows(i).Item("CodigoClinica") <> dtActualizar.Rows(i).Item("CodigoClinica") Then
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
    ''' Fecha de Creación:     18/01/2011
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
        Dim int_TipoSolicitud As Integer = 2

        Dim obj_BL_SolicitudActualizacionDatos As New bl_SolicitudActualizacionDatos
        Dim ds_Lista As DataSet = obj_BL_SolicitudActualizacionDatos.FUN_LIS_DatosSolicitudes(int_CodigoSolicitud, int_TipoSolicitud, str_CodigoPerfiles, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        'Dim obj_EnvioEmail As New EnvioEmail
        Dim int_ExitoEnvio As Integer = 0

        Dim arr_Emails As New ArrayList
        Dim str_Asunto As String = "Solicitud de Actualización de Datos de la Ficha de Alumno"

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

        MostrarMensaje(str_Mensaje, str_TipoMensaje)

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
        Dim int_CodigoUsuario As String = Obtener_CodigoFamiliarLogueado()
        Dim int_TipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(5, 79, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
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