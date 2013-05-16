Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloBancoLibros
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

Partial Class Modulo_BancoLibros_AsignacionVigenciaLibros
    Inherits System.Web.UI.Page
    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1
    'ok

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Asignación Vigencia de Libros")

            If Not Page.IsPostBack Then

                cargarComboAniosAcademicos()
                ddlAnioAcademico1.SelectedValue = 1
                ddlAnioAcademico2.SelectedValue = 1
                TabContainer1.ActiveTabIndex = 0
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                camposHabilitados()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim usp_mensaje As String = ""
            cargarGridviewLibros()
            camposDeshabilitados()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        limpiarFiltros()

    End Sub

    Protected Sub camposHabilitados()
        ddlAnioAcademico1.Enabled = True
        ddlAnioAcademico2.Enabled = True
        btnBuscar.Enabled = True
        btnLimpiar.Enabled = True
        btnGrabar.Enabled = False
        btnCancelar.Enabled = False
        btnGrabar.ImageUrl = "~/App_Themes/Imagenes/btnGrabar_0.png"
        btnCancelar.ImageUrl = "~/App_Themes/Imagenes/btnCancelar_0.png"
        btnBuscar.ImageUrl = "~/App_Themes/Imagenes/btnBuscar_1.png"
        btnLimpiar.ImageUrl = "~/App_Themes/Imagenes/btnLimpiar_1.png"
    End Sub

    Protected Sub camposDeshabilitados()
        ddlAnioAcademico1.Enabled = False
        ddlAnioAcademico2.Enabled = False
        btnBuscar.Enabled = False
        btnLimpiar.Enabled = False
        btnCancelar.Enabled = True
        btnGrabar.Enabled = True
        btnGrabar.ImageUrl = "~/App_Themes/Imagenes/btnGrabar_1.png"
        btnCancelar.ImageUrl = "~/App_Themes/Imagenes/btnCancelar_1.png"
        btnBuscar.ImageUrl = "~/App_Themes/Imagenes/btnBuscarV2_0.png"
        btnLimpiar.ImageUrl = "~/App_Themes/Imagenes/btnLimpiar_0.png"
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim usp_mensaje As String = ""
            'If validarGrabar(usp_mensaje) Then
            '    'grabar()
            'Else
            '    MostrarSexyAlertBox(usp_mensaje, "Alert")
            'End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Valida los campos del formulario antes de proceder a "Grabar"
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     21/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarGrabar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        Dim bool_RegistrosGV As Boolean = False

        If ddlAnioAcademico1.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Año")
            result = False
        End If

        If ddlAnioAcademico2.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Aulas")
            result = False
        End If

        If GridView1.Rows.Count > 0 Then
            bool_RegistrosGV = True
        End If

        If bool_RegistrosGV = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 34, "Criterios y Calificativos ")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function


    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        miTab1.Enabled = True
        camposHabilitados()
        limpiarFiltros()
        limpiarGrillaLibros()
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     01/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAnioAcademico1, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlAnioAcademico2, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Llena el gridview con la lista de Alumnos del aula seleccionado
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     19/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarGridviewLibros()

        Dim int_CodigoAnioAcademico1 As Integer = ddlAnioAcademico1.SelectedValue
        Dim int_CodigoAnioAcademico2 As Integer = ddlAnioAcademico2.SelectedValue

        If int_CodigoAnioAcademico1 = 0 And int_CodigoAnioAcademico2 = 0 Then
            GridView1.DataBind()
            Exit Sub
        End If


        Dim obj_BL_AsignacionVigenciaLibros As New bl_AsignacionVigenciaLibros
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AsignacionVigenciaLibros.FUN_LIS_AsignacionVigenciaLibros(int_CodigoAnioAcademico1, int_CodigoAnioAcademico2, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If ds_Lista.Tables(0).Rows.Count > 0 Then
            ViewState("ListaLibros") = ds_Lista.Tables(0)
            ViewState("ListaAniosAcademicos") = ds_Lista.Tables(1)
            GridView1.DataSource = ds_Lista.Tables(0)
            GridView1.DataBind()

            'Else
            '    limpiarCombo(ddlBimestres)
        End If

    End Sub

    Private Sub limpiarCombo(ByVal ddl As DropDownList)

        Controles.limpiarCombo(ddl, False, True)

    End Sub



    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     22/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    '''     Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     22/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    Private Sub limpiarFiltros()
        ddlAnioAcademico1.SelectedValue = 1
        ddlAnioAcademico2.SelectedValue = 1
        'limpiarGrillaAlumnos()

    End Sub

    Private Sub limpiarGrillaLibros()
        Dim dt As New DataTable("ListaLibros")
        dt = Datos.agregarColumna(dt, "IdFila", "Integer")
        dt = Datos.agregarColumna(dt, "Titulo", "String")
        dt = Datos.agregarColumna(dt, "Editorial", "String")
        dt = Datos.agregarColumna(dt, "Autor", "String")
        dt = Datos.agregarColumna(dt, "Idioma", "String")
        Dim dr As DataRow
        dr = dt.NewRow
        dr.Item("IdFila") = 0
        dr.Item("Titulo") = ""
        dr.Item("Editorial") = ""
        dr.Item("Autor") = ""
        dr.Item("Idioma") = ""
        dt.Rows.Add(dr)

        dt.Clear()

        Dim dtS As New DataTable("ListaAnioAcademico")
        dtS = Datos.agregarColumna(dt, "DescripcionCriterio", "String")
        dtS = Datos.agregarColumna(dt, "CodigoCriterio", "Integer")
        Dim drS As DataRow
        drS = dt.NewRow
        drS.Item("DescripcionCriterio") = ""
        drS.Item("CodigoCriterio") = 0
        dtS.Rows.Add(drS)

        dtS.Clear()

        GridView1.DataSource = dt
        GridView1.DataBind()

    End Sub

#End Region

#Region "Eventos Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Header Then

            Dim ListaCriterios As DataList = CType(e.Row.FindControl("ListaAniosAcademicos"), DataList)
            If ViewState("ListaAniosAcademicos") IsNot Nothing Then
                Dim dt As DataTable
                dt = ViewState("ListaAniosAcademicos")
                ListaCriterios.DataSource = dt
                ListaCriterios.DataBind()
            End If

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            Dim str_CodigoAlumno As String = CType(e.Row.FindControl("lblCodigoLibro"), Label).Text
            Dim chkLibros As System.Web.UI.WebControls.CheckBoxList = e.Row.FindControl("chk_Libros_grilla")
            Dim lblFila As Label = CType(e.Row.FindControl("lblIdFila"), Label)

            lblFila.Text = e.Row.RowIndex + 1

            If ViewState("ListaAniosAcademicos") IsNot Nothing Then
                chkLibros.DataSource = ViewState("ListaAniosAcademicos")
                chkLibros.DataTextField = "NombreBlanco"
                chkLibros.DataValueField = "CodigoAnioAcademico"
                chkLibros.DataBind()
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

        'If e.Row.RowType = DataControlRowType.DataRow Then

        '    Dim lblCodigoAlumno As System.Web.UI.WebControls.Label = e.Row.FindControl("lblCodigoAlumno")
        '    'Dim chkCriterios As System.Web.UI.WebControls.CheckBoxList = e.Row.FindControl("chk_Criterios")

        '    Dim dt_Criterios As DataTable = ViewState("ListaCriterios")
        '    Dim dt_Alumnos As DataTable = ViewState("ListaAlumnos")

        'End If

    End Sub
#End Region
End Class
