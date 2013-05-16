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

Partial Class Interfaz_Familia_Modulo_SolicitudesActualizacionInformacion_SolicitudActualizacionFichaAlumno
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Información de Hijo")

            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                cargar_MenuInformacion()
                cargarCombos()
                ObtenerFicha("20100051")
                verificarProfesaReligion()
                verificarReligionCatolica()
                ddlReligion_SelectedIndexChanged()
                rbBautizo_SelectedIndexChanged()
                rbPriComunion_SelectedIndexChanged()
                rbConfirmado_SelectedIndexChanged()
                'verificarFactura()
                'verificarTipoSeguro()
                'verificarVigenciaSeguro()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btn_GrupoInformacion_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim cont_views As Integer = 0
        Dim str_codigoProgramacionID As String = ""
        Dim int_IndiceVista As Integer = -1

        str_codigoProgramacionID = sender.ValidationGroup

        While cont_views <= mv_GrupoInformacion.Views.Count - 1

            If mv_GrupoInformacion.Views(cont_views).ID.ToString = str_codigoProgramacionID Then
                mv_GrupoInformacion.ActiveViewIndex = cont_views

                If lbl_Bloque_DatosNacimiento.ID = "lbl_" & sender.ValidationGroup Then
                    lbl_Bloque_DatosNacimiento.Text = sender.text
                End If

                If lbl_Bloque_DatosPersonales.ID = "lbl_" & sender.ValidationGroup Then
                    lbl_Bloque_DatosPersonales.Text = sender.text
                End If

                If lbl_Bloque_DatosDomicilio.ID = "lbl_" & sender.ValidationGroup Then
                    lbl_Bloque_DatosDomicilio.Text = sender.text
                End If

                If lbl_Bloque_DatosReligiosos.ID = "lbl_" & sender.ValidationGroup Then
                    lbl_Bloque_DatosReligiosos.Text = sender.text
                End If

                If lbl_Bloque_DatosEmergencia.ID = "lbl_" & sender.ValidationGroup Then
                    lbl_Bloque_DatosEmergencia.Text = sender.text
                    Exit Sub
                End If

                If lbl_Bloque_DatosOtros.ID = "lbl_" & sender.ValidationGroup Then
                    lbl_Bloque_DatosOtros.Text = sender.text
                End If

                If lbl_Bloque_DatosEspeciales.ID = "lbl_" & sender.ValidationGroup Then
                    lbl_Bloque_DatosEspeciales.Text = sender.text
                End If

                If lbl_Bloque_DatosSituacionActual.ID = "lbl_" & sender.ValidationGroup Then
                    lbl_Bloque_DatosSituacionActual.Text = sender.text
                End If

            End If

            cont_views = cont_views + 1
        End While

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

    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(5, 79)
    End Sub

    ''' <summary>
    ''' Carga el menu informativo(Bloques de información)
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargar_MenuInformacion()
        Dim dt As DataTable

        dt = New DataTable("GruposInformacion")
        dt = Datos.agregarColumna(dt, "CodigoGrupo", "Integer")
        dt = Datos.agregarColumna(dt, "NombreGrupo", "String")
        dt = Datos.agregarColumna(dt, "CodigoProgramacion", "String")

        Dim dr As DataRow
        dr = dt.NewRow
        dr.Item("CodigoGrupo") = 1
        dr.Item("NombreGrupo") = "Personales"
        dr.Item("CodigoProgramacion") = "Bloque_DatosPersonales"
        dt.Rows.Add(dr)

        Dim dr_2 As DataRow
        dr_2 = dt.NewRow
        dr_2.Item("CodigoGrupo") = 2
        dr_2.Item("NombreGrupo") = "Nacimiento"
        dr_2.Item("CodigoProgramacion") = "Bloque_DatosNacimiento"
        dt.Rows.Add(dr_2)

        Dim dr_3 As DataRow
        dr_3 = dt.NewRow
        dr_3.Item("CodigoGrupo") = 3
        dr_3.Item("NombreGrupo") = "Domicilio"
        dr_3.Item("CodigoProgramacion") = "Bloque_DatosDomicilio"
        dt.Rows.Add(dr_3)

        Dim dr_5 As DataRow
        dr_5 = dt.NewRow
        dr_5.Item("CodigoGrupo") = 4
        dr_5.Item("NombreGrupo") = "Religioso"
        dr_5.Item("CodigoProgramacion") = "Bloque_DatosReligiosos"
        dt.Rows.Add(dr_5)

        Dim dr_7 As DataRow
        dr_7 = dt.NewRow
        dr_7.Item("CodigoGrupo") = 5
        dr_7.Item("NombreGrupo") = "Emergencia"
        dr_7.Item("CodigoProgramacion") = "Bloque_DatosEmergencia"
        dt.Rows.Add(dr_7)

        Dim dr_8 As DataRow
        dr_8 = dt.NewRow
        dr_8.Item("CodigoGrupo") = 6
        dr_8.Item("NombreGrupo") = "Otros"
        dr_8.Item("CodigoProgramacion") = "Bloque_DatosOtros"
        dt.Rows.Add(dr_8)

        Dim dr_9 As DataRow
        dr_9 = dt.NewRow
        dr_9.Item("CodigoGrupo") = 7
        dr_9.Item("NombreGrupo") = "Especiales"
        dr_9.Item("CodigoProgramacion") = "Bloque_DatosEspeciales"
        dt.Rows.Add(dr_9)

        Dim dr_10 As DataRow
        dr_10 = dt.NewRow
        dr_10.Item("CodigoGrupo") = 8
        dr_10.Item("NombreGrupo") = "Situacion Actual"
        dr_10.Item("CodigoProgramacion") = "Bloque_DatosSituacionActual"
        dt.Rows.Add(dr_10)

        dgv_GrupoInformacion.DataSource = dt
        dgv_GrupoInformacion.DataBind()

    End Sub

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

        cargarComboNacionalidades()
        cargarComboIdiomas()
        cargarComboReligiones()
        cargarComboUbigeo()
        cargarComboTipoDiscapaciadades()

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
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Nacionalidades.FUN_LIS_Nacionalidades("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 5, 79)
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
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Idiomas.FUN_LIS_Idiomas("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 5, 79)
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
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Religiones.FUN_LIS_Religiones("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 5, 79)
        Controles.llenarCombo(ddlReligion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo de Tipo de Discapacidades
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoDiscapaciadades()

        Dim obj_BL_TipoDiscapaciadades As New bl_TiposDiscapacidades
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_TipoDiscapaciadades.FUN_LIS_TiposDiscapacidades("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 5, 79)
        Controles.llenarCombo(ddlTipoDiscapacidad, ds_Lista, "Codigo", "Descripcion", False, True)

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
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Departamentos(int_CodigoUsuario, int_CodigoTipoUsuario, 5, 79)
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
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlDomicilioDepartamento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 5, 79)
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
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlDomicilioDepartamento.SelectedValue, ddlDomicilioProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 5, 79)
        Controles.llenarCombo(ddlDomicilioDistrito, ds_Lista, "Codigo", "Descripcion", False, True)

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

        If ddlReligion.SelectedValue = 2 Then

            rbBautizo.Enabled = True
            tbLugarBautizo.Enabled = True
            tbAnioBautizo.Enabled = True

            rbPriComunion.Enabled = True
            tbLugarPriComunion.Enabled = True
            tbAnioPriComunion.Enabled = True

            rbConfirmado.Enabled = True
            tbLugarConfirmado.Enabled = True
            tbAnioConfirmado.Enabled = True

        ElseIf ddlReligion.SelectedValue <> 2 And ddlReligion.SelectedValue <> 1 Then

            rbBautizo.Enabled = True
            tbLugarBautizo.Enabled = True
            tbAnioBautizo.Enabled = True

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
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarFicha(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

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

        If tbCorreoElectronico.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Correo Electrónico")
            result = False
        End If


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
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_FichaAlumno.FUN_GET_AlumnoVisualizacionActualizacionFamiliar(str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 5, 79)

        ViewState("DatosOriginales") = ds_Lista

        hd_CodigoPersonaSolicitante.Value = 18

        'Situacion Actual
        'imgFotoAlumno.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web").ToString() & ds_Lista.Tables(0).Rows(0).Item("RutaFoto").ToString
        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString)
        hd_CodigoPersona.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPersona").ToString)

        lblNombreCompleto.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString
        lblHouse.Text = ds_Lista.Tables(0).Rows(0).Item("DescHouse").ToString
        'lblSituacionAnio.Text = ds_Lista.Tables(0).Rows(0).Item("DescEstadoAnioActualAlumno").ToString
        lblEstadoActual.Text = ds_Lista.Tables(0).Rows(0).Item("DescEstadoActualAlumno").ToString
        lblAnioActual.Text = ds_Lista.Tables(0).Rows(0).Item("DescAnioActualAlumno").ToString
        lblNSnGS.Text = ds_Lista.Tables(0).Rows(0).Item("ENSnGS").ToString

        'Datos Personales
        lblCodigo.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString
        lblCodigoEducando.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoEducando").ToString
        lblApellidoPaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString
        lblApellidoMaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString
        lblNombre.Text = ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString
        lblSexo.Text = ds_Lista.Tables(0).Rows(0).Item("DescSexo").ToString
        lblTipoDocumento.Text = ds_Lista.Tables(0).Rows(0).Item("DescTipoDocIdentidad").ToString
        lblNumDocumento.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString

        'Datos de Nacimiento
        lblNacRegistrado.Text = ds_Lista.Tables(0).Rows(0).Item("DescNacimientoRegistrado").ToString
        lblFechaNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString
        lblPais.Text = ds_Lista.Tables(0).Rows(0).Item("DescPaisNacimiento").ToString
        lblDepartamento.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoNacimientoDepartamento").ToString
        lblProvincia.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoNacimientoProvincia").ToString
        lblDistrito.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoNacimientoDistrito").ToString

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

        'Detalle discapacidades
        Dim objDT_Discapacidad As DataTable

        objDT_Discapacidad = New DataTable("ListaDiscapacidad")
        objDT_Discapacidad = ds_Lista.Tables(3).Clone

        If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then

            For Each dr As DataRow In ds_Lista.Tables(3).Rows
                objDT_Discapacidad.ImportRow(dr)
            Next

            ViewState("ListaDiscapacidad") = objDT_Discapacidad
            gvDetalleDiscapacidad.DataSource = objDT_Discapacidad
            gvDetalleDiscapacidad.DataBind()
            GridviewFillColor(ds_Lista.Tables(3), gvDetalleDiscapacidad)

        End If

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
                be_Alumno.CodigoReligion = 1
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
            ddlReligion.SelectedValue = 1
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

                If ddlReligion.SelectedItem.ToString = "Católica" Then ' Si su religion es Catolica
                    If rbPriComunion.SelectedValue = 0 Then
                        tbLugarPriComunion.Text = ""
                        tbAnioPriComunion.Text = 0 : End If
                    If rbConfirmado.SelectedValue = 0 Then
                        tbLugarConfirmado.Text = ""
                        tbAnioConfirmado.Text = 0 : End If
                Else ' Si su religion no es Catolica
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

                obj_BE_Alumno.BautizoAnio = tbAnioBautizo.Text.Trim
                arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(0).Rows(0).Item("Bloque_BautizoAnio").ToString))

                If ddlReligion.SelectedValue = 2 Then ' Si su religion es catolica

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

        'Detalle Discapacidades
        Dim objDT_Discapacidad As DataTable
        objDT_Discapacidad = New DataTable("ListaDiscapacidad")
        objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "CodigoTipoDiscapacidad", "String")
        objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "DescripcionDiscapacidad", "String")

        Dim dr_Discapacidad As DataRow
        For Each drv As GridViewRow In gvDetalleDiscapacidad.Rows
            dr_Discapacidad = objDT_Discapacidad.NewRow
            dr_Discapacidad.Item("CodigoTipoDiscapacidad") = CType(drv.FindControl("lblCodigoDiscapacidad"), Label).Text
            dr_Discapacidad.Item("DescripcionDiscapacidad") = CType(drv.FindControl("lblDescripcionDiscapacidad"), Label).Text
            objDT_Discapacidad.Rows.Add(dr_Discapacidad)
        Next

        'If objDT_Discapacidad.Rows.Count = 0 Then 'Si no hay detalle
        '    dr_Discapacidad = objDT_Discapacidad.NewRow
        '    dr_Discapacidad.Item("CodigoTipoDiscapacidad") = -1
        '    dr_Discapacidad.Item("DescripcionDiscapacidad") = ""
        '    objDT_Discapacidad.Rows.Add(dr_Discapacidad)
        'End If

        miDetalle = 0
        miDetalle = Comparar2DataTable(ds_Lista.Tables(3), objDT_Discapacidad, 3)

        If miDetalle > 0 Then ' Si almenos 1 registro es diferente
            BoolGrabar = True
            arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(3).Rows(0).Item("CodigoBloqueInformacion").ToString))
        Else
            If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") > 0 Then
                If ds_Lista.Tables(3).Rows(0).Item("Origen") = "T" Then
                    BoolGrabar = True
                    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(3).Rows(0).Item("CodigoBloqueInformacion").ToString))
                Else
                    objDT_Discapacidad.Rows.Clear()
                End If
            Else
                objDT_Discapacidad.Rows.Clear()
            End If
        End If

        ''Detalle Seguro
        'Dim objDT_Clinica As DataTable
        'objDT_Clinica = New DataTable("ListaClinicas")
        'objDT_Clinica = Datos.agregarColumna(objDT_Clinica, "CodigoClinica", "String")
        'objDT_Clinica = Datos.agregarColumna(objDT_Clinica, "Clinica", "String")

        'Dim dr_Clinica As DataRow
        'For Each drv As GridViewRow In GVListaClinica.Rows
        '    dr_Clinica = objDT_Clinica.NewRow
        '    dr_Clinica.Item("CodigoClinica") = CType(drv.FindControl("Label1"), Label).Text
        '    dr_Clinica.Item("Clinica") = CType(drv.FindControl("Label2"), Label).Text
        '    objDT_Clinica.Rows.Add(dr_Clinica)
        'Next

        'If ddlTipoSeguro.SelectedValue = 3 Then
        '    objDT_Clinica.Rows.Clear()

        '    If ds_Lista.Tables(0).Rows(0).Item("CodigoTipoSeguro") <> 3 Then
        '        If objDT_Clinica.Rows.Count = 0 Then 'Si no hay detalle
        '            dr_Clinica = objDT_Clinica.NewRow
        '            dr_Clinica.Item("CodigoClinica") = 0
        '            dr_Clinica.Item("Clinica") = ""
        '            objDT_Clinica.Rows.Add(dr_Clinica)
        '        End If
        '    Else
        '        If objDT_Clinica.Rows.Count = 0 Then 'Si no hay detalle
        '            dr_Clinica = objDT_Clinica.NewRow
        '            dr_Clinica.Item("CodigoClinica") = -1
        '            dr_Clinica.Item("Clinica") = ""
        '            objDT_Clinica.Rows.Add(dr_Clinica)
        '        End If
        '    End If



        'End If

        'miDetalle = 0
        'miDetalle = Comparar2DataTable(ds_Lista.Tables(5), objDT_Clinica, 4)

        'If miDetalle > 0 Then ' Si almenos 1 registro es diferente
        '    BoolGrabar = True
        '    arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(5).Rows(0).Item("CodigoBloqueInformacion").ToString))
        'Else
        '    If ds_Lista.Tables(5).Rows(0).Item("Origen") = "T" Then
        '        BoolGrabar = True
        '        arrBloques = verificarArreglo(arrBloques, CInt(ds_Lista.Tables(5).Rows(0).Item("CodigoBloqueInformacion").ToString))
        '    Else
        '        objDT_Clinica.Rows.Clear()
        '    End If
        'End If

        If BoolGrabar Then
            'Agrego las DataTable a mi DataSet
            objDS_Detalle.Tables.Add(objDT_Nacionalidad)
            objDS_Detalle.Tables.Add(objDT_Idioma)
            objDS_Detalle.Tables.Add(objDT_Discapacidad)
            'objDS_Detalle.Tables.Add(objDT_Clinica)

            str_Perfiles = obtenerPerfiles(ds_Lista.Tables(4), arrBloques)

            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

            usp_valor = obj_BL_Alumno.FUN_INS_AlumnoTemp(ojb_BE_SolicitudActualizacionFichaAlumnos, _
                                                         obj_BE_Alumno, _
                                                         obj_BE_FichaSeguroAlumno, _
                                                         str_Perfiles, _
                                                         objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 5, 79)
        Else
            'MostrarSexyAlertBox(Alertas.ObtenerAlerta(4, ""), "Alert")
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnFichaCancelar_Click()
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

#End Region

#Region "Mantenimiento Detalle Discapacidad"

#Region "Eventos"
    Protected Sub btn_Add_Discapacidad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoDiscapacidad") = True
        pnDiscapacidad.Show()
    End Sub

    Protected Sub popup_btnAgregar_Discapacidad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If ViewState("NuevoDiscapacidad") = False Then
                editarDiscapacidad()
            ElseIf ViewState("NuevoDiscapacidad") = True Then
                agregarDiscapacidad()
            End If
        Catch ex As Exception
            EnvioEmailError(200, ex.ToString)
        End Try
    End Sub

    Protected Sub popup_btnCancelar_Discapacidad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalDiscapacidad()
    End Sub
#End Region
#Region "Métodos"
    ''' <summary>
    ''' Edita 1 registro del detalle de Discapacidad
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
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
        Dim boolIncremento As Boolean = False
        Dim dt As DataTable
        dt = ViewState("ListaDiscapacidad")

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then

            ElseIf auxdr.Item("CodigoTipoDiscapacidad").ToString = ddlTipoDiscapacidad.SelectedValue Then
                MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                pnDiscapacidad.Show()
                Exit Sub
            End If
        Next

        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("CodigoRelacion").ToString = int_CodigoOriginal Then
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
    ''' Agrega 1 registro al detalle de Discapacidad
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
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
            dt = Datos.agregarColumna(dt, "CodigoRelacion", "Integer")
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
                    pnDiscapacidad.Show()
                    Exit Sub
                End If
                id_codigo_fila = auxdr.Item("CodigoRelacion").ToString()
            Next
        End If

        If boolIncremento = False Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("CodigoRelacion") = id_codigo_fila + 1
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
    ''' Agrega 1 registro al detalle de Discapacidad
    ''' </summary>
    ''' <param name="int_CodigoRelTipoDiscapAlum">Codigo del registro de discapacidad que se desea eliminar.</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarRelTipoDiscapacidades(ByVal int_CodigoRelTipoDiscapAlum As Integer)
        Dim dt As DataTable
        dt = ViewState("ListaDiscapacidad")
        For Each auxdr As DataRow In dt.Rows
            If Val(auxdr.Item("CodigoRelacion").ToString) = int_CodigoRelTipoDiscapAlum Then
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
    ''' Setea los controles del formulario con los datos del registro de discapacidad seleccionado
    ''' </summary>
    ''' <param name="int_Codigo">Codigo de discapacidad</param>
    ''' <param name="int_CodigoTipoDiscapacidad">codigo del tipo de discapacidad</param>
    ''' <param name="int_DescripcionDiscapacidad">descripcion de la discapacidad</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
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
    ''' Setea por defecto los controles del popup de formulario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
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
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
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
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(5, 79, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region

End Class
