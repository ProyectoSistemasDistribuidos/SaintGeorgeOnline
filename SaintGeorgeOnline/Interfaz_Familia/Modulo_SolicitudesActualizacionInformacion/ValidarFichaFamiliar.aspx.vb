Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Modulo de Mantenimiento de Enfermedades
''' </summary>
''' <remarks>
''' Código del Modulo:    2
''' Código de la Opción:  57
''' </remarks>
Partial Class Modulo_Matricula_ValidarFichaFamiliar
    Inherits System.Web.UI.Page

#Region "Eventos Busqueda"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Actualización de Ficha de Familiar") '<br />por solicitudes de familiares")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()

                ViewState("SortExpression") = "NombreCompleto_FamiliarActualizar"
                ViewState("Direccion") = "ASC"
                btnFichaCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                tbFechaSolicitudInicial.Text = Today.ToShortDateString
                tbFechaSolicitudFinal.Text = Today.ToShortDateString
                hidenCodigoPerfil.Value = 2
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

#End Region

#Region "Metodos Busqueda"

    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(2, 57)

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
        rbEstados.SelectedValue = 1

    End Sub

    ''' <summary>
    ''' Setea el estado de los campos y opciones de la ficha de alumno
    ''' </summary> 
    ''' <param name="str_Modo">Tipo de visualizacion que tendra los datos del formulario</param>  
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
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
    ''' Se listarán las fichas de familiares, que coincidan con los parametros de busqueda ingresados, para su posterior validación 
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
        objMaestroPersona.ApellidoPaterno = tbBuscarApellidoPaterno.Text.Trim
        objMaestroPersona.ApellidoMaterno = tbBuscarApellidoMaterno.Text.Trim
        objMaestroPersona.Nombre = tbBuscarNombre.Text.Trim

        Dim int_EstadoSolicitud = rbEstados.SelectedValue
        Dim dt_FechaRangoInicial As Date = tbFechaSolicitudInicial.Text.Trim
        Dim dt_FechaRangoFinal As Date = tbFechaSolicitudFinal.Text.Trim

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Familiares As New bl_Familiares
            ds_Lista = obj_BL_Familiares.FUN_LIS_FamiliarActualizacion(objMaestroPersona, dt_FechaRangoInicial, dt_FechaRangoFinal, int_EstadoSolicitud, hidenCodigoPerfil.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 57)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_Familiares As New bl_Familiares
                ds_Lista = obj_BL_Familiares.FUN_LIS_FamiliarActualizacion(objMaestroPersona, dt_FechaRangoInicial, dt_FechaRangoFinal, int_EstadoSolicitud, hidenCodigoPerfil.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 57)
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

#End Region

#Region "Listado de Familiares"

#Region "Eventos"
    Protected Sub btnVerFamiliares_click()
        Try
            mostrarPanelListaFamiliares(hidenCodigoFamiliar.Value)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCerraListaFamiliares_Click()
        ocultarPanelListaFamiliares()
    End Sub
#End Region
#Region "Métodos"

    ''' <summary>
    ''' Muestra un popup con la lista de familiares del familiar
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub mostrarPanelListaFamiliares(ByVal codigoFamiliar As Integer)

        pnModalListaFamiliares.Show()
        Dim obj_BL_Familiares As New bl_Familiares
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Familiares.FUN_LIS_FamiliaresPorCodigoFamiliar(codigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 57)
        GVListaListaFamiliares.DataSource = ds_Lista.Tables(0)
        GVListaListaFamiliares.DataBind()

    End Sub

    ''' <summary>
    ''' Oculta el popup de lista de familiares 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ocultarPanelListaFamiliares()
        pnModalListaFamiliares.Hide()
    End Sub
#End Region
#Region "Gridview"
    Protected Sub GVListaListaFamiliares_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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

#End Region

#Region "Metodos Actualizacion"

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

        Dim gvRows As Integer = GVActualizarFamiliar.Rows.Count

        If CType(sender, CheckBox).Checked Then
            For Each dr As GridViewRow In GVActualizarFamiliar.Rows
                CType(dr.FindControl("chkActualizar"), CheckBox).Checked = True
            Next

            If miTablaNacionalidad.Visible = True Then
                CType(GVNacionalidad.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If
            If miTablaIdiomas.Visible = True Then
                CType(GVIdiomas.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If
            If miTablaProfesiones.Visible = True Then
                CType(GVProfesiones.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If
            If miTablaAutos.Visible = True Then
                CType(GVAutos.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = True
            End If

        Else
            For Each dr As GridViewRow In GVActualizarFamiliar.Rows
                CType(dr.FindControl("chkActualizar"), CheckBox).Checked = False
            Next

            If miTablaNacionalidad.Visible = True Then
                CType(GVNacionalidad.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If
            If miTablaIdiomas.Visible = True Then
                CType(GVIdiomas.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If
            If miTablaProfesiones.Visible = True Then
                CType(GVProfesiones.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If
            If miTablaAutos.Visible = True Then
                CType(GVAutos.Rows(0).FindControl("chkActualizar"), CheckBox).Checked = False
            End If

        End If

    End Sub

    ''' <summary>
    ''' Cambia el estado de checkbox clickeado y a la vez de su checkbox padre (si es que posee alguno)
    ''' En caso el checkbox es padre y fue desmarcado, los checkbox hijos referente a "este" seran desmarcados tambien
    ''' </summary>
    ''' <param name="sender">Hace referencia al checkbox que ha sido clickeado</param>
    ''' <param name="e">Argumentos usados durante el Evento</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     28/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cambiarCheckBoxDependientes(ByVal sender As Object, ByVal e As EventArgs)

        Dim chk As CheckBox = TryCast(sender, CheckBox)
        Dim estado As Boolean = chk.Checked
        Dim row As GridViewRow = TryCast(chk.NamingContainer, GridViewRow)
        Dim indice As Integer = CType(row.FindControl("lblIndiceCampo"), Label).Text

        'Datos Religiosos
        'Padre : indice 21
        'Hijos : 22-23
        If indice = 21 And estado = False Then
            For i As Integer = 22 To 23
                MarcarCheckbox(i, 0)
            Next
        ElseIf indice >= 22 And indice <= 23 Then
            MarcarCheckbox(21, 1)
        End If

        'Datos Domicilio
        'Padre : indice 28
        'Hijos : 29
        If indice = 28 Then

            If estado = False Then
                MarcarCheckbox(29, 0)
            Else
                MarcarCheckbox(29, 1)
            End If

        ElseIf indice = 29 Then

            If estado = False Then
                MarcarCheckbox(28, 0)
            Else
                MarcarCheckbox(28, 1)
            End If

        End If

        'Datos Trabajo
        'Padre : indice 35 
        'Hijos : indice 36 - 46
        If indice = 35 And estado = False Then
            For i As Integer = 36 To 46
                MarcarCheckbox(i, 0)
            Next
        ElseIf indice >= 36 And indice <= 46 Then
            If indice = 39 And estado = False Then
                MarcarCheckbox(40, 0)
            ElseIf indice = 40 Then
                MarcarCheckbox(39, 1)
            End If
            MarcarCheckbox(35, 1)
        End If

        'Datos Estudios
        'Padre : indice 47
        'Hijos : indice 48
        If indice = 47 And estado = False Then
            MarcarCheckbox(48, 0)
        ElseIf indice = 48 Then
            MarcarCheckbox(47, 1)
        End If

    End Sub

    ''' <summary>
    ''' Cambia el estado del checkbox clickeado
    ''' </summary>
    ''' <param name="rowIdx">indice del checkbox a ser buscado</param>
    ''' <param name="accion">Especifica si el checkbox sera marcado o desmarcado</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     28/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MarcarCheckbox(ByVal rowIdx As Integer, ByVal accion As Integer)

        For Each gvr As GridViewRow In GVActualizarFamiliar.Rows
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

    ''' <summary>
    ''' Consulta la información de la ficha de familiar
    ''' </summary> 
    ''' <param name="int_CodigoFamiliar">codigo del familiar del cual se va a obtener la informacion de su ficha de familiar</param>
    ''' <param name="int_CodigoSolicitud">codigo de la solicitud de ficha de familiar que se esta validando</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerFicha(ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoSolicitud As Integer)

        Dim str_mensajeError As String = ""

        Dim obj_BL_Familiares As New bl_Familiares
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Familiares.FUN_GET_FamiliarActualizacion(int_CodigoFamiliar, int_CodigoSolicitud, hidenCodigoPerfil.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 57)

        GVActualizarFamiliar.DataSource = ds_Lista.Tables(0)
        GVActualizarFamiliar.DataBind()

        'Datos del Solicitante
        lblVerNombreSolicitante.Text = ds_Lista.Tables(5).Rows(0).Item("NombreCompletoSolicitante").ToString
        lblVerFechaSolicitud.Text = ds_Lista.Tables(5).Rows(0).Item("FechaSolicitud").ToString
        lblVerEstadoSolicitud.Text = ds_Lista.Tables(5).Rows(0).Item("Estado").ToString

        'Datos Personales
        hidenCodigoFamiliar.Value = ds_Lista.Tables(4).Rows(0).Item("CodigoFamiliar").ToString
        hidenCodigoPersona.Value = ds_Lista.Tables(4).Rows(0).Item("CodigoPersona").ToString
        hidenCodigoSolicitud.Value = int_CodigoSolicitud

        lblVerApellidoPaterno.Text = IIf(ds_Lista.Tables(4).Rows(0).Item("ApellidoPaterno").ToString.Length = 0, "-", ds_Lista.Tables(4).Rows(0).Item("ApellidoPaterno").ToString)
        lblVerApellidoMaterno.Text = IIf(ds_Lista.Tables(4).Rows(0).Item("ApellidoMaterno").ToString.Length = 0, "-", ds_Lista.Tables(4).Rows(0).Item("ApellidoMaterno").ToString)
        lblVerNombre.Text = IIf(ds_Lista.Tables(4).Rows(0).Item("Nombre").ToString.Length = 0, "-", ds_Lista.Tables(4).Rows(0).Item("Nombre").ToString)
        lblVerSexo.Text = IIf(ds_Lista.Tables(4).Rows(0).Item("DescSexo").ToString.Length = 0, "-", ds_Lista.Tables(4).Rows(0).Item("DescSexo").ToString)
        lblVerTipoDocumento.Text = IIf(ds_Lista.Tables(4).Rows(0).Item("DescTipoDocIdentidad").ToString.Length = 0, "-", ds_Lista.Tables(4).Rows(0).Item("DescTipoDocIdentidad").ToString)
        lblVerNumDocumento.Text = IIf(ds_Lista.Tables(4).Rows(0).Item("NumeroDocIdentidad").ToString.Length = 0, "-", ds_Lista.Tables(4).Rows(0).Item("NumeroDocIdentidad").ToString)
        lblVerEstadoCivil.Text = IIf(ds_Lista.Tables(4).Rows(0).Item("DescEstadoCivil").ToString.Length = 0, "-", ds_Lista.Tables(4).Rows(0).Item("DescEstadoCivil").ToString)
        lblVerVive.Text = IIf(ds_Lista.Tables(4).Rows(0).Item("DescVive").ToString.Length = 0, "-", ds_Lista.Tables(4).Rows(0).Item("DescVive").ToString)
        lblVerFechaDefuncion.Text = IIf(ds_Lista.Tables(4).Rows(0).Item("FechaDefuncionStr").ToString.Length = 0, "-", ds_Lista.Tables(4).Rows(0).Item("FechaDefuncionStr").ToString)

        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
            GVNacionalidad.DataSource = ds_Lista.Tables(1)
            GVNacionalidad.DataBind()
        Else
            miTablaNacionalidad.Visible = False
        End If

        If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then
            GVIdiomas.DataSource = ds_Lista.Tables(2)
            GVIdiomas.DataBind()
        Else
            miTablaIdiomas.Visible = False
        End If

        If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then
            GVProfesiones.DataSource = ds_Lista.Tables(3)
            GVProfesiones.DataBind()
        Else
            miTablaProfesiones.Visible = False
        End If

        If ds_Lista.Tables(6).Rows(0).Item("CodigoRelacion") <> -1 Then
            GVAutos.DataSource = ds_Lista.Tables(6)
            GVAutos.DataBind()
        Else
            miTablaAutos.Visible = False
        End If

        VerRegistro("Verificación")

    End Sub

    ''' <summary>
    ''' Graba los datos que se han sido validados de la ficha de familiar
    ''' </summary> 
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarFicha()

        Dim str_mensajeError As String = ""

        Dim intCodigoFamiliar As Integer = hidenCodigoFamiliar.Value
        Dim intCodigoPersona As Integer = hidenCodigoPersona.Value
        Dim intCodigoSolicitud As Integer = hidenCodigoSolicitud.Value
        Dim intCodigoPerfil As Integer = hidenCodigoPerfil.Value

        Dim objDT_Cabecera As New DataTable("DatosCabecera")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "Indice", "string")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "NombreoCampo", "string")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "CodigoCampo", "string")
        objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "ValorCampo", "string")

        For Each gvr As GridViewRow In GVActualizarFamiliar.Rows
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

        'Detalle Profesion
        Dim arrStrCodigosProfesion() As String
        If GVProfesiones.Rows.Count > 0 Then
            Dim StrCodigosProfesion As String = CType(GVProfesiones.Rows(0).FindControl("lblListaCodigoActualizado"), Label).Text
            arrStrCodigosProfesion = Split(StrCodigosProfesion, ",")
        End If

        'Detalle Ficha Auto
        Dim arrStrCodigosAuto() As String, arrDescAuto() As String
        If GVAutos.Rows.Count > 0 Then
            Dim StrCodigosAuto As String = CType(GVAutos.Rows(0).FindControl("lblListaCodigoActualizado"), Label).Text
            Dim strDescAuto As String = CType(GVAutos.Rows(0).FindControl("lblListaDescActualizado"), Label).Text

            arrStrCodigosAuto = Split(StrCodigosAuto, ",")
            arrDescAuto = Split(strDescAuto, ",")
        End If


        Dim obj_BL_Familiar As New bl_Familiares
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer = 0

        usp_valor = obj_BL_Familiar.FUN_UPD_FamiliaresActualizacion( _
        intCodigoFamiliar, intCodigoPersona, intCodigoSolicitud, intCodigoPerfil, objDT_Cabecera, _
        arrStrCodigossNacionalidad, arrStrCodigosIdioma, arrStrCodigosProfesion, arrStrCodigosAuto, arrDescAuto, _
        usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 57)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnFichaCancelar_Click()
            btnBuscar_Click()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Regresa al formulario de busqueda de solicitudes de validacion de fichas de familiar 
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

        For Each gvr As GridViewRow In GVActualizarFamiliar.Rows
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

        If GVProfesiones.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVProfesiones.Rows
                If CType(gvr.FindControl("chkActualizar"), CheckBox).Checked Then
                    miBool = True
                End If
            Next
        End If

        If GVAutos.Rows.Count > 0 Then
            For Each gvr As GridViewRow In GVAutos.Rows
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

    Protected Sub GVActualizarFamiliar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
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
                End If

                If e.Row.DataItem("Indice") = "29" Or e.Row.DataItem("Indice") = "40" Then
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
                    _lblNombreCampo.Text = "Departamento<br />Provincia<br />Distrito"
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

#End Region

#Region "Eventos Gridview - Busqueda"

    Protected Sub GVListaFamiliar_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Or e.CommandName = "VerFamiliares" Then

                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Seleccionar" Then
                    int_CodigoAccion = 5
                    ObtenerFicha(codigo, CInt(CType(row.FindControl("lblCodigoSolicitud"), Label).Text))
                    chkAll.Checked = False

                ElseIf e.CommandName = "VerFamiliares" Then
                    int_CodigoAccion = 0
                    mostrarPanelListaFamiliares(codigo)

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
            EnvioEmailError(112, ex.ToString)
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
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos del Gridview Busqueda"

    'Protected Sub IraPag(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim _IraPag As TextBox = DirectCast(sender, TextBox)
    '    Dim _NumPag As Integer

    '    If Integer.TryParse(_IraPag.Text.Trim, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GVListaFamiliar.PageCount Then
    '        Me.GVListaFamiliar.PageIndex = _NumPag - 1
    '    Else
    '        Me.GVListaFamiliar.PageIndex = 0
    '    End If

    '    Me.GVListaFamiliar.SelectedIndex = -1
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
        Dim _btnSorting_d1 As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_NombreCompleto_FamiliarActualizar"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_FechaRegistroSolicitud"), ImageButton)
        Dim _btnSorting_d3 As ImageButton = CType(GVListaFamiliar.HeaderRow.FindControl("btnSorting_EstadoSolicitud"), ImageButton)

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
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(2, 57, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region

End Class
