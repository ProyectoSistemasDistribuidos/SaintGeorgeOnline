Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Modulo de Validacion de Ficha de Alumnos
''' </summary>
''' <remarks>
''' Código del Modulo:    2
''' Código de la Opción:  58
''' </remarks>
Partial Class Modulo_Matricula_ValidarFichaAlumnos
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Actualización de Ficha de Alumno") ' <br /> por Solicitudes de Familiares")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()

                ViewState("SortExpression") = "NombreCompleto_AlumnoActualizar"
                ViewState("Direccion") = "ASC"
                btnFichaCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")

                tbFechaSolicitudInicial.Text = Today.Date.AddMonths(-1)
                tbFechaSolicitudFinal.Text = Today.ToShortDateString

                cargarCombosAlumno()
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

    Protected Sub ddlBuscarNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarSubNivel)
            limpiarCombos(ddlBuscarGrado)
            limpiarCombos(ddlBuscarAula)
            cargarComboAlumnoSubNivel()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarSubNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarGrado)
            limpiarCombos(ddlBuscarAula)
            cargarComboAlumnoGrado()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarAula)
            cargarComboAlumnoAulas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub


    Protected Sub btnGrabar_click()
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

    Protected Sub btnFichaCancelar_Click()
        CancelarFicha()
    End Sub

    Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            CambiarCheckbox(sender)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub chkActualizar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cambiarCheckBoxDependientes(sender, e)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub chkActualizarDetalleClinica_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cambiarCheckBoxDetalle(sender, e)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
#End Region

#Region "Metodos"

    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(2, 58)

        'CONTROLES DEL FORMULARIO
        'Master.BloqueoControles(btnBuscar, 1)
        'Master.BloqueoControles(btnGrabar, 1)

        'Master.SeteoPermisosAcciones(btnBuscar, 48)
        'Master.SeteoPermisosAcciones(btnGrabar, 48)

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

        tbBuscarApellidoPaterno.Text = ""
        tbBuscarApellidoMaterno.Text = ""
        tbBuscarNombre.Text = ""
        cargarCombosAlumno()
        rbEstados.SelectedValue = 1

    End Sub

    ''' <summary>
    ''' Carga una serie de listas desplegables vinculadas a la busqueda de alumnos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombosAlumno()

        cargarComboAlumnoNivel()
        limpiarCombos(ddlBuscarSubNivel)
        limpiarCombos(ddlBuscarGrado)
        limpiarCombos(ddlBuscarAula)

    End Sub

    ''' <summary>
    ''' Limpia las listas desplegables de vinculadas a la busqueda de alumnos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombosFamiliarAlumno()

        limpiarCombos(ddlBuscarNivel)
        limpiarCombos(ddlBuscarSubNivel)
        limpiarCombos(ddlBuscarGrado)
        limpiarCombos(ddlBuscarAula)

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
    Private Sub cargarComboAlumnoNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 58)

        Controles.llenarCombo(ddlBuscarNivel, ds_Lista, "Codigo", "Descripcion", True, False)

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
    Private Sub cargarComboAlumnoSubNivel()

        Dim obj_BL_SubNiveles As New bl_SubNiveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(CInt(ddlBuscarNivel.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 2, 58)
        Controles.llenarCombo(ddlBuscarSubNivel, ds_Lista, "Codigo", "Descripcion", True, False)

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
    Private Sub cargarComboAlumnoGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(CInt(ddlBuscarSubNivel.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 2, 58)
        Controles.llenarCombo(ddlBuscarGrado, ds_Lista, "Codigo", "Descripcion", True, False)

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
    Private Sub cargarComboAlumnoAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(CInt(ddlBuscarGrado.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 2, 58)
        Controles.llenarCombo(ddlBuscarAula, ds_Lista, "Codigo", "Descripcion", True, False)

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
    ''' Se listarán las fichas de alumno, que coincidan con los parametros de busqueda ingresados, para su posterior validación 
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

        GVListaAlumno.DataSource = ds_Lista.Tables(0)
        GVListaAlumno.DataBind()

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
        objMaestroPersona.AlumnoNivel = ddlBuscarNivel.SelectedValue
        objMaestroPersona.AlumnoSubnivel = ddlBuscarSubNivel.SelectedValue
        objMaestroPersona.AlumnoGrado = ddlBuscarGrado.SelectedValue
        objMaestroPersona.AlumnoAula = ddlBuscarAula.SelectedValue
        Dim int_EstadoSolicitud = rbEstados.SelectedValue
        Dim dt_FechaRangoInicial As Date = tbFechaSolicitudInicial.Text.Trim
        Dim dt_FechaRangoFinal As Date = tbFechaSolicitudFinal.Text.Trim

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Alumnos As New bl_Alumnos
            ds_Lista = obj_BL_Alumnos.FUN_LIS_AlumnoActualizacion(objMaestroPersona, dt_FechaRangoInicial, dt_FechaRangoFinal, int_EstadoSolicitud, hidenCodigoPerfil.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 58)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_Alumnos As New bl_Alumnos
                ds_Lista = obj_BL_Alumnos.FUN_LIS_AlumnoActualizacion(objMaestroPersona, dt_FechaRangoInicial, dt_FechaRangoFinal, int_EstadoSolicitud, hidenCodigoPerfil.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 58)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista

    End Function

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

        If IsDate(tbFechaSolicitudInicial.Text.Trim) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Solicitud Inicial")
            result = False
        End If

        If IsDate(tbFechaSolicitudFinal.Text.Trim) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Solicitud Final")
            result = False
        End If

        If IsDate(tbFechaSolicitudInicial.Text.Trim) And IsDate(tbFechaSolicitudFinal.Text.Trim) Then

            If (CType(tbFechaSolicitudInicial.Text, Date) > CType(tbFechaSolicitudFinal.Text, Date)) Then

                str_alertas = Alertas.ObtenerAlerta(str_alertas, 7, "Fecha de Solicitud")
                result = False

            End If

        End If

        str_Mensaje = str_alertas
        Return result

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

        Try

            For Each filasGrillas As GridViewRow In GridView1.Rows
                CType(filasGrillas.FindControl("chkActualizar"), CheckBox).Checked = CType(sender, CheckBox).Checked

            Next


            Dim gvRows As Integer = GVActualizarAlumno.Rows.Count

            If CType(sender, CheckBox).Checked Then
                For Each dr As GridViewRow In GVActualizarAlumno.Rows
                    CType(dr.FindControl("chkActualizar"), CheckBox).Checked = True
                Next

                If miTablaNacionalidad.Visible = True Then
                    CType(GVNacionalidad.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
                End If
                If miTablaIdiomas.Visible = True Then
                    CType(GVIdiomas.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
                End If

                'If miTablaClinicas.Visible = True Then
                '    CType(GVClinicas.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
                'End If
                'If miTablaDiscapacidades.Visible = True Then
                '    CType(GVDiscapacidades.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
                'End If

            Else
                For Each dr As GridViewRow In GVActualizarAlumno.Rows
                    CType(dr.FindControl("chkActualizar"), CheckBox).Checked = False
                Next

                If miTablaNacionalidad.Visible = True Then
                    CType(GVNacionalidad.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
                End If
                If miTablaIdiomas.Visible = True Then
                    CType(GVIdiomas.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
                End If

                'If miTablaClinicas.Visible = True Then
                '    CType(GVClinicas.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
                'End If
                'If miTablaDiscapacidades.Visible = True Then
                '    CType(GVDiscapacidades.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
                'End If

            End If

            ''Dim controlCheck As CheckBox
            For Each filas As GridViewRow In GridView1.Rows
                If filas.RowType = DataControlRowType.DataRow Then
                    CType(filas.FindControl("chkActualizar"), CheckBox).Checked = CType(sender, CheckBox).Checked
                End If


            Next

        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' Cambia el estado de checkbox clickeado y a la vez de su checkbox padre (si es que posee alguno)
    ''' En caso el checkbox es padre y fue desmarcado, los checkbox hijos referente a "este" seran desmarcados tambien
    ''' </summary>
    ''' <param name="sender">Hace referencia al checkbox que ha sido clickeado</param>
    ''' <param name="e">Argumentos usados durante el Evento</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cambiarCheckBoxDependientes(ByVal sender As Object, ByVal e As EventArgs)

        Dim chk As CheckBox = TryCast(sender, CheckBox)
        Dim estado As Boolean = chk.Checked
        Dim row As GridViewRow = TryCast(chk.NamingContainer, GridViewRow)
        Dim indice As Integer = CType(row.FindControl("lblIndiceCampo"), Label).Text

        'Datos Religiosos
        'Padre : indice 36
        'Hijos : 37-44
        If indice = 34 And estado = False Then
            For i As Integer = 34 To 44
                MarcarCheckbox(i, 0)
            Next
        ElseIf indice >= 35 And indice <= 44 Then
            If indice >= 37 And indice <= 38 Then
                MarcarCheckbox(36, 1)
            End If
            If indice >= 40 And indice <= 41 Then
                MarcarCheckbox(39, 1)
            End If
            If indice >= 43 And indice <= 44 Then
                MarcarCheckbox(42, 1)
            End If
            MarcarCheckbox(34, 1)
        End If

        If indice = 36 And estado = False Then
            MarcarCheckbox(37, 0)
            MarcarCheckbox(38, 0)
        End If
        If indice = 39 And estado = False Then
            MarcarCheckbox(40, 0)
            MarcarCheckbox(41, 0)
        End If
        If indice = 42 And estado = False Then
            MarcarCheckbox(43, 0)
            MarcarCheckbox(44, 0)
        End If

        'Datos Seguro
        'Padre : indice 63 
        'Hijos : indice 64 - 71
        ' ''If indice = 63 And estado = False Then
        ' ''    For i As Integer = 64 To 71
        ' ''        MarcarCheckbox(i, 0)
        ' ''    Next
        ' ''    CheckboxDetalle(GVClinicas, 2)
        ' ''ElseIf indice >= 64 And indice <= 71 Then
        ' ''    If indice >= 67 And indice <= 68 Then
        ' ''        MarcarCheckbox(66, 1)
        ' ''    End If
        ' ''    MarcarCheckbox(63, 1)
        ' ''End If
        ' ''If indice = 66 And estado = False Then
        ' ''    MarcarCheckbox(67, 0)
        ' ''    MarcarCheckbox(68, 0)
        ' ''End If

        'Datos Facturacion
        'Padre : indice 56 
        'Hijos : indice 57 - 59
        ' ''If indice = 56 And estado = False Then
        ' ''    For i As Integer = 57 To 59
        ' ''        MarcarCheckbox(i, 0)
        ' ''    Next
        ' ''ElseIf indice >= 57 And indice <= 59 Then
        ' ''    MarcarCheckbox(56, 1)
        ' ''End If

    End Sub

    ''' <summary>
    ''' Cambia el estado de checkbox detalle clickeado
    ''' </summary>
    ''' <param name="sender">Hace referencia al checkbox que ha sido clickeado</param>
    ''' <param name="e">Argumentos usados durante el Evento</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cambiarCheckBoxDetalle(ByVal sender As Object, ByVal e As EventArgs)

        Dim chk As CheckBox = TryCast(sender, CheckBox)
        Dim estado As Boolean = chk.Checked

        Dim gv As GridView = CType(Me.Page.FindControl("GVClinicas"), GridView)

        If estado = True Then
            MarcarCheckbox(63, 1)
        Else
            MarcarCheckbox(63, 0)
        End If

    End Sub


    ''' <summary>
    ''' Cambia el estado del checkbox clickeado
    ''' </summary>
    ''' <param name="rowIdx">indice del checkbox a ser buscado</param>
    ''' <param name="accion">Especifica si el checkbox sera marcado o desmarcado</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MarcarCheckbox(ByVal rowIdx As Integer, ByVal accion As Integer)

        For Each gvr As GridViewRow In GVActualizarAlumno.Rows
            If CType(gvr.FindControl("lblIndiceCampo"), Label).Text = rowIdx Then
                Dim idxChk As CheckBox = CType(gvr.FindControl("chkActualizar"), CheckBox)

                If accion = 1 Then 'Marcar
                    If Not idxChk.Checked Then
                        idxChk.Checked = True
                    End If
                Else 'Desmarcar
                    If idxChk.Checked Then
                        idxChk.Checked = False
                    End If
                End If

            End If
        Next

    End Sub

    'Private Sub DesmarcarCheckbox(ByVal rowIdx As Integer)

    '    For Each gvr As GridViewRow In GVActualizarAlumno.Rows
    '        If CType(gvr.FindControl("lblIndiceCampo"), Label).Text = rowIdx Then
    '            Dim idxChk As CheckBox = CType(gvr.FindControl("chkActualizar"), CheckBox)
    '            If idxChk.Checked Then
    '                idxChk.Checked = False
    '            End If
    '        End If
    '    Next

    'End Sub

    ''' <summary>
    ''' Cambia el estado de un gv detalle
    ''' </summary>
    ''' <param name="gv">indice gv detalle al cual se cambiara el estado de los checkbox</param>
    ''' <param name="evento">Especifica si el checkbox sera marcado o desmarcado</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CheckboxDetalle(ByVal gv As GridView, ByVal evento As Integer)

        If gv.Rows.Count > 0 Then
            Select Case evento
                Case 1 ' Marcar
                    CType(gv.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
                Case 2 ' Desmarcar
                    CType(gv.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End Select

        End If

    End Sub

    ''' <summary>
    ''' Consulta la información de la ficha de alumno
    ''' </summary> 
    ''' <param name="int_CodigoAlumno">codigo del alumno al cual se va a obtener la informacion de su ficha de alumno</param>
    ''' <param name="int_CodigoSolicitud">codigo de la solicitud de ficha de alumno que se esta validando</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerFicha(ByVal int_CodigoAlumno As Integer, ByVal int_CodigoSolicitud As Integer)

        Dim str_mensajeError As String = ""


        Dim obj_BL_alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_alumnos.FUN_GET_FamiliarActualizacion(int_CodigoAlumno, int_CodigoSolicitud, hidenCodigoPerfil.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 58)

        GVActualizarAlumno.DataSource = ds_Lista.Tables(0)
        GVActualizarAlumno.DataBind()

        'Datos del Solicitante
        lblVerNombreSolicitante.Text = ds_Lista.Tables(4).Rows(0).Item("NombreCompletoSolicitante").ToString
        lblVerFechaSolicitud.Text = ds_Lista.Tables(4).Rows(0).Item("FechaSolicitud").ToString
        lblVerEstadoSolicitud.Text = ds_Lista.Tables(4).Rows(0).Item("Estado").ToString

        'Situacion Actual
        hidenCodigoAlumno.Value = ds_Lista.Tables(3).Rows(0).Item("CodigoAlumno").ToString
        hidenCodigoPersona.Value = ds_Lista.Tables(3).Rows(0).Item("CodigoPersona").ToString
        hidenCodigoSolicitud.Value = int_CodigoSolicitud
        'hidenCodigoFichaSeguro.Value = ds_Lista.Tables(5).Rows(0).Item("CodigoFichaSeguro").ToString

        lblVerNombreCompleto.Text = IIf(ds_Lista.Tables(3).Rows(0).Item("NombreCompleto").ToString.Length = 0, "-", ds_Lista.Tables(3).Rows(0).Item("NombreCompleto").ToString)
        lblVerEstadoAnioAcademico.Text = IIf(ds_Lista.Tables(3).Rows(0).Item("EstadoAnioActualAlumno").ToString.Length = 0, "-", ds_Lista.Tables(3).Rows(0).Item("EstadoAnioActualAlumno").ToString)
        lblVerNSnGS.Text = IIf(ds_Lista.Tables(3).Rows(0).Item("NSnGS").ToString.Length = 0, "-", ds_Lista.Tables(3).Rows(0).Item("NSnGS").ToString)
        lblVerHouse.Text = IIf(ds_Lista.Tables(3).Rows(0).Item("House").ToString.Length = 0, "-", ds_Lista.Tables(3).Rows(0).Item("House").ToString)

        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
            GVNacionalidad.DataSource = ds_Lista.Tables(1)
            GVNacionalidad.DataBind()
        Else
            ' miTablaNacionalidad.Visible = False
        End If

        If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then
            GVIdiomas.DataSource = ds_Lista.Tables(2)
            GVIdiomas.DataBind()
        Else
            ' miTablaIdiomas.Visible = False
        End If
        GridView1.DataSource = ds_Lista.Tables(5)
        GridView1.DataBind()


        'If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then
        '    GVClinicas.DataSource = ds_Lista.Tables(3)
        '    GVClinicas.DataBind()
        'Else
        '    miTablaClinicas.Visible = False
        'End If

        'If ds_Lista.Tables(4).Rows(0).Item("CodigoRelacion") <> -1 Then
        '    GVDiscapacidades.DataSource = ds_Lista.Tables(4)
        '    GVDiscapacidades.DataBind()
        'Else
        '    miTablaDiscapacidades.Visible = False
        'End If

        VerRegistro("Verificación")

    End Sub

    ''' <summary>
    ''' Graba los datos que se han sido validados de la ficha de alumno
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarFicha()

        Dim str_mensajeError As String = ""

        Dim intCodigoAlumno As Integer = hidenCodigoAlumno.Value
        Dim intCodigoPersona As Integer = hidenCodigoPersona.Value
        Dim intCodigoSolicitud As Integer = hidenCodigoSolicitud.Value
        Dim intCodigoPerfil As Integer = hidenCodigoPerfil.Value
        'Dim intCodigoFichaSeguro As Integer = hidenCodigoFichaSeguro.Value

        Dim objDT_Cabecera As New DataTable("DatosCabecera")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "Indice", "string")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "NombreoCampo", "string")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "CodigoCampo", "string")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "ValorCampo", "string")

        For Each gvr As GridViewRow In GVActualizarAlumno.Rows
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

        'Detalle Nacionalidad
        Dim arrStrCodigossNacionalidad() As String
        If GVNacionalidad.Rows.Count > 0 Then
            Dim StrCodigosNacionalidad As String = CType(GVNacionalidad.Rows(0).FindControl("lblListaCodigoActualizado"), Label).Text
            arrStrCodigossNacionalidad = Split(StrCodigosNacionalidad, ",")
        End If

        'Detalle Idiomas
        Dim arrStrCodigosIdioma() As String
        If GVIdiomas.Rows.Count > 0 Then
            Dim StrCodigosIdioma As String = CType(GVIdiomas.Rows(0).FindControl("lblListaCodigoActualizado"), Label).Text
            arrStrCodigosIdioma = Split(StrCodigosIdioma, ",")
        End If

        'Detalle Clinicas
        ' ''Dim arrStrCodigosClinica() As String
        ' ''If GVClinicas.Rows.Count > 0 Then
        ' ''    Dim StrCodigosClinica As String = CType(GVClinicas.Rows(0).FindControl("lblListaCodigoActualizado"), Label).Text
        ' ''    arrStrCodigosClinica = Split(StrCodigosClinica, ",")
        ' ''End If

        'Detalle Discapacidad
        ' ''Dim arrStrCodigosDiscapacidad() As String
        ' ''Dim arrStrDetalleDiscapacidad() As String
        ' ''Dim objDT_Discapacidad As DataTable
        ' ''objDT_Discapacidad = New DataTable("ListaDiscapacidad")
        ' ''objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "Codigo", "String")
        ' ''objDT_Discapacidad = Datos.agregarColumna(objDT_Discapacidad, "Detalle", "String")
        ' ''Dim dr_Discapacidad As DataRow

        ' ''If GVDiscapacidades.Rows.Count > 0 Then
        ' ''    Dim StrCodigosDiscapacidad As String = CType(GVDiscapacidades.Rows(0).FindControl("lblListaCodigoActualizado"), Label).Text
        ' ''    Dim StrDetalleDiscapacidad As String = CType(GVDiscapacidades.Rows(0).FindControl("lblListaDetalleActualizado"), Label).Text
        ' ''    arrStrCodigosDiscapacidad = Split(StrCodigosDiscapacidad, ",")
        ' ''    arrStrDetalleDiscapacidad = Split(StrDetalleDiscapacidad, ",")

        ' ''    For i As Integer = 0 To arrStrCodigosDiscapacidad.Length - 1
        ' ''        dr_Discapacidad = objDT_Discapacidad.NewRow
        ' ''        dr_Discapacidad.Item("Codigo") = arrStrCodigosDiscapacidad(i)
        ' ''        dr_Discapacidad.Item("Detalle") = arrStrDetalleDiscapacidad(i)
        ' ''        objDT_Discapacidad.Rows.Add(dr_Discapacidad)
        ' ''    Next
        ' ''End If


        Dim dtFilasSeleccionadas As New DataTable
        dtFilasSeleccionadas = devolverTablaRelacionFamilia()
        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer = 0
        Dim dst As New DataSet

        dst.Tables.Add(objDT_Cabecera)
        dst.Tables.Add(dtFilasSeleccionadas)

        usp_valor = obj_BL_Alumnos.FUN_UPD_AlumnosActualizacion( _
        intCodigoAlumno, intCodigoPersona, intCodigoSolicitud, intCodigoPerfil, dst, _
        arrStrCodigossNacionalidad, arrStrCodigosIdioma, usp_mensaje, _
        int_CodigoUsuario, int_CodigoTipoUsuario, 2, 58)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")


            ' Envio email de confirmacion de actualizacion de solicitud
            enviarEmailSolicitanteActualidacion(intCodigoSolicitud)


            btnFichaCancelar_Click()
            btnBuscar_Click()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub
    Function devolverTablaRelacionFamilia() As DataTable
        Dim dtRelacionFamilia As New DataTable
        Dim dcRelacionFamilia As DataColumn
        Dim filasRelacionFamilia As DataRow


        dcRelacionFamilia = New DataColumn("codFamilia", GetType(Integer))
        dtRelacionFamilia.Columns.Add(dcRelacionFamilia)
        dcRelacionFamilia = New DataColumn("pagante", GetType(Boolean))
        dtRelacionFamilia.Columns.Add(dcRelacionFamilia)
        dcRelacionFamilia = New DataColumn("viveCon", GetType(Boolean))
        dtRelacionFamilia.Columns.Add(dcRelacionFamilia)

        Dim codigo As Integer = 0

        For Each filasRelacionFam As GridViewRow In GridView1.Rows
            codigo = CInt(CType(filasRelacionFam.FindControl("lblIndiceCampo7"), Label).Text)
            If CType(filasRelacionFam.FindControl("chkActualizar"), CheckBox).Checked Then
                filasRelacionFamilia = dtRelacionFamilia.NewRow
                filasRelacionFamilia("codFamilia") = codigo
                If CType(filasRelacionFam.FindControl("lblIndiceCampo5"), Label).Text = "Si" Then
                    filasRelacionFamilia("viveCon") = True
                ElseIf CType(filasRelacionFam.FindControl("lblIndiceCampo5"), Label).Text = "No" Then
                    filasRelacionFamilia("viveCon") = False
                End If
                If CType(filasRelacionFam.FindControl("lblIndiceCampo6"), Label).Text = "Si" Then
                    filasRelacionFamilia("pagante") = True
                ElseIf CType(filasRelacionFam.FindControl("lblIndiceCampo6"), Label).Text = "No" Then
                    filasRelacionFamilia("pagante") = False
                End If

                dtRelacionFamilia.Rows.Add(filasRelacionFamilia)
            End If
        Next 
        Return dtRelacionFamilia


      
        Try

        Catch ex As Exception
        Finally

        End Try
    End Function

    Private Sub enviarEmailSolicitanteActualidacion(ByVal int_CodigoSolicitud As Integer)

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado()

        'Tipos de Solicitud 
        'Ficha Familiar : 1
        'Ficha Alumno   : 2
        'Ficha Médica   : 3
        Dim int_TipoSolicitud As Integer = 2

        Dim obj_BL_SolicitudActualizacionDatos As New bl_SolicitudActualizacionDatos
        Dim ds_Lista As DataSet = obj_BL_SolicitudActualizacionDatos.FUN_LIS_DatosSolicitanteActualizacion(int_CodigoSolicitud, int_TipoSolicitud, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 58)

        If ds_Lista.Tables(0).Rows.Count > 0 Then

            Dim int_EnvioEmail As Integer = ds_Lista.Tables(0).Rows(0).Item("EnviarEmail")

            If int_EnvioEmail > 0 Then ' Enviar email

                Dim int_ExitoEnvio As Integer = 0

                Dim arr_Emails As New ArrayList
                Dim str_Asunto As String = "Confirmación de Actualización de Datos"

                Dim sb_Cuerpo As New StringBuilder
                sb_Cuerpo.Append("<div style='font-family: Arial; font-size: 11px;'>")
                sb_Cuerpo.Append("Estimado Sr(a). <i><b>" & ds_Lista.Tables(0).Rows(0).Item("NombreCompleto") & "</b></i> <br />")
                sb_Cuerpo.Append("Se informa que su solicitud de actualización de datos de Alumno ha sido actualizada correctamente.<br />")
                sb_Cuerpo.Append("Saludos Cordiales.<br />")
                sb_Cuerpo.Append("</div>")

                Dim str_EmailCopia As String = ""

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    arr_Emails.Add(dr.Item("Email").ToString)
                Next

                int_ExitoEnvio = EnvioEmail.SendEmail(arr_Emails, sb_Cuerpo.ToString, str_Asunto)

            Else ' mostrar mensaje que no se pudo enviar email


            End If

        End If

    End Sub


    ''' <summary>
    ''' Regresa al formulario de busqueda de solicitudes de validacion de fichas de alumno 
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CancelarFicha()

        miTab1.Enabled = True
        miTab2.Enabled = False

        lbTab2.Text = "Actualización"
        TabContainer1.ActiveTabIndex = 0

    End Sub

    ''' <summary>
    ''' Valida la ficha de alumno antes de grabar
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

        For Each gvr As GridViewRow In GVActualizarAlumno.Rows
            If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                miBool = True
            End If
        Next

        If GVNacionalidad.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVNacionalidad.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If

        If GVIdiomas.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVIdiomas.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If

        ' ''If GVClinicas.Rows.Count > 0 Then
        ' ''    For Each gvr As GridViewRow In GVClinicas.Rows
        ' ''        If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
        ' ''            miBool = True
        ' ''        End If
        ' ''    Next
        ' ''End If

        ' ''If GVDiscapacidades.Rows.Count > 0 Then
        ' ''    For Each gvr As GridViewRow In GVDiscapacidades.Rows
        ' ''        If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
        ' ''            miBool = True
        ' ''        End If
        ' ''    Next
        ' ''End If

        If GridView1.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GridView1.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If


        If miBool = False Then
            'strMensaje = Alertas.ObtenerAlerta(3, "") '"Debe seleccionar por lo menos 1 check de algúnn registro."
            strMensaje = "Debe seleccionar por lo menos un registro."
        End If

        Return miBool

    End Function

#End Region

#Region "Eventos Gridview - Actualizacion"

    Protected Sub GVActualizarAlumno_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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

                If e.Row.DataItem("Indice") = "27" Or e.Row.DataItem("Indice") = "45" Then
                    Dim strListaOriginal As String = e.Row.DataItem("ValorCampoOriginal")
                    Dim arrListaOriginal() As String = Split(strListaOriginal, ",")
                    Dim strResultOriginal As New StringBuilder
                    Dim strListaActualizar As String = e.Row.DataItem("ValorCampoActualizar")
                    Dim arrListaActualizar() As String = Split(strListaActualizar, ",")
                    Dim strResultActualizar As New StringBuilder

                    For i As Integer = 0 To arrListaOriginal.Length - 1
                        strResultOriginal.Append(arrListaOriginal(i))
                        strResultActualizar.Append(arrListaActualizar(i))
                        If (i + 1 <> arrListaOriginal.Length) Then
                            strResultOriginal.Append("<br />")
                            strResultActualizar.Append("<br />")
                        End If
                    Next
                    _lblNombreCampo.Text = "Departamento<br />&nbsp;&nbsp;Provincia<br />&nbsp;&nbsp;Distrito"
                    _ValorOriginal.Text = strResultOriginal.ToString
                    _ValorActualizado.Text = strResultActualizar.ToString
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub miGridview_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblListaCodigoOriginal As Label = e.Row.FindControl("lblListaCodigoOriginal")
                Dim _lblListaCodigoActualizado As Label = e.Row.FindControl("lblListaCodigoActualizado")
                Dim _lblListaDescOriginal As Label = e.Row.FindControl("lblListaDescOriginal")
                Dim _lblListaDescActualizado As Label = e.Row.FindControl("lblListaDescActualizado")

                Dim _listValorOriginal As BulletedList = e.Row.FindControl("listValorOriginal")
                Dim _listValorActualizado As BulletedList = e.Row.FindControl("listValorActualizado")

                Dim arrStrCodigosOriginal() As String
                Dim arrStrCodigosActualizar() As String
                Dim arrStrDescOriginal() As String
                Dim arrStrDescActualizar() As String

                arrStrCodigosOriginal = Split(_lblListaCodigoOriginal.Text, ",")
                arrStrDescOriginal = Split(_lblListaDescOriginal.Text, ",")
                For i As Integer = 0 To arrStrCodigosOriginal.Length - 1
                    _listValorOriginal.Items.Add(arrStrDescOriginal(i))
                Next

                arrStrCodigosActualizar = Split(_lblListaCodigoActualizado.Text, ",")
                arrStrDescActualizar = Split(_lblListaDescActualizado.Text, ",")
                For i As Integer = 0 To arrStrCodigosActualizar.Length - 1
                    _listValorActualizado.Items.Add(arrStrDescActualizar(i))
                Next

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVDiscapacidades_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _lblListaCodigoOriginal As Label = e.Row.FindControl("lblListaCodigoOriginal")
                Dim _lblListaCodigoActualizado As Label = e.Row.FindControl("lblListaCodigoActualizado")
                Dim _lblListaDescOriginal As Label = e.Row.FindControl("lblListaDescOriginal")
                Dim _lblListaDescActualizado As Label = e.Row.FindControl("lblListaDescActualizado")
                Dim _lblListaDetalleOriginal As Label = e.Row.FindControl("lblListaDetalleOriginal")
                Dim _lblListaDetalleActualizado As Label = e.Row.FindControl("lblListaDetalleActualizado")

                Dim _listValorOriginal As BulletedList = e.Row.FindControl("listValorOriginal")
                Dim _listValorActualizado As BulletedList = e.Row.FindControl("listValorActualizado")

                Dim arrStrCodigosOriginal() As String
                Dim arrStrCodigosActualizar() As String
                Dim arrStrDescOriginal() As String
                Dim arrStrDescActualizar() As String

                Dim arrStrDetalleOriginal() As String
                Dim arrStrDetalleActualizar() As String

                arrStrCodigosOriginal = Split(_lblListaCodigoOriginal.Text, ",")
                arrStrDescOriginal = Split(_lblListaDescOriginal.Text, ",")
                arrStrDetalleOriginal = Split(_lblListaDetalleOriginal.Text, ",")

                For i As Integer = 0 To arrStrCodigosOriginal.Length - 1
                    _listValorOriginal.Items.Add(arrStrDescOriginal(i) & " / " & arrStrDetalleOriginal(i))
                Next

                arrStrCodigosActualizar = Split(_lblListaCodigoActualizado.Text, ",")
                arrStrDescActualizar = Split(_lblListaDescActualizado.Text, ",")
                arrStrDetalleActualizar = Split(_lblListaDetalleActualizado.Text, ",")

                For i As Integer = 0 To arrStrCodigosActualizar.Length - 1
                    _listValorActualizado.Items.Add(arrStrDescActualizar(i) & " / " & arrStrDetalleActualizar(i))
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

    Protected Sub GVListaAlumno_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Seleccionar" Then
                    int_CodigoAccion = 5
                    ObtenerFicha(codigo, CInt(CType(row.FindControl("lblCodigoSolicitud"), Label).Text))
                    chkAll.Checked = False
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaAlumno_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnSeleccionar As ImageButton = e.Row.FindControl("btnSeleccionar")

            If e.Row.RowType = DataControlRowType.Pager Then
                Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
                _TotalPags.Text = GVListaAlumno.PageCount.ToString

                Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
                _Registros.Text = InformacionPager(GVListaAlumno, e.Row, Me)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.DataItem("EstadoSolicitud") = "Pendiente" Then
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

    Protected Sub GVListaAlumno_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GVListaAlumno.PageIndex = e.NewPageIndex
            End If

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaAlumno_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
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

    Protected Sub GVListaAlumno_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Pager Then
                CrearBotonesPager(GVListaAlumno, e.Row, Me)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GVListaAlumno.PageCount Then
                Me.GVListaAlumno.PageIndex = _NumPag - 1
            Else
                Me.GVListaAlumno.PageIndex = 0
            End If

            Me.GVListaAlumno.SelectedIndex = -1

            'listarFichas()
            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos del Gridview Busqueda"

    'Protected Sub IraPag(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim _IraPag As TextBox = DirectCast(sender, TextBox)
    '    Dim _NumPag As Integer

    '    If Integer.TryParse(_IraPag.Text.Trim, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GVListaAlumno.PageCount Then
    '        Me.GVListaAlumno.PageIndex = _NumPag - 1
    '    Else
    '        Me.GVListaAlumno.PageIndex = 0
    '    End If

    '    Me.GVListaAlumno.SelectedIndex = -1
    '    listarFichas()

    'End Sub

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

        GVListaAlumno.DataSource = dv
        GVListaAlumno.DataBind()

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

        Dim _btnSorting As ImageButton = CType(GVListaAlumno.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(GVListaAlumno.HeaderRow.FindControl("btnSorting_NombreCompleto_AlumnoActualizar"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GVListaAlumno.HeaderRow.FindControl("btnSorting_FechaRegistroSolicitud"), ImageButton)
        Dim _btnSorting_d3 As ImageButton = CType(GVListaAlumno.HeaderRow.FindControl("btnSorting_EstadoSolicitud"), ImageButton)

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
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(2, 58, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region

End Class