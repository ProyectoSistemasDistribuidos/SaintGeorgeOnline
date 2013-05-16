Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloCursos
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

Partial Class Modulo_Notas_RegistroNotas
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Registro de Notas")
            If Not Page.IsPostBack Then

                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                cargarComboAnioAcademico()
                ddlPeriodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                cargarComboGrados()

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlGrado.SelectedValue > 0 Then
                listarAlumnos()
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlGrado.SelectedValue > 0 Then
                listarAlumnos()
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    Private Sub cargarComboAnioAcademico()
        Dim int_CodigoAnioTop As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicosHistorico(int_CodigoAnioTop, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlPeriodo, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    Private Sub cargarComboGrados()
        Dim int_CodigoNivel As Integer = 2 ' secundaria
        Dim int_Top As Integer = 5 ' ultimos 5 grados
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_GradosXNivel(int_CodigoNivel, int_Top, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlGrado, ds_Lista, "Codigo", "DescripcionEspaniol", False, True)
    End Sub

    Private Sub listarAlumnos()
        Dim int_CodigoPeriodo As Integer = ddlPeriodo.SelectedValue
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue
        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Alumnos.FUN_LIS_AlumnosPorPeriodoYGrado( _
        int_CodigoPeriodo, int_CodigoGrado, _
        int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()
    End Sub

    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

#End Region

#Region "Eventos Gridview"

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "seleccionar" Then

                Dim codigo As String = CInt(e.CommandArgument.ToString)

                Dim lnk As LinkButton = CType(e.CommandSource, LinkButton)
                Dim row As GridViewRow = CType(lnk.NamingContainer, GridViewRow)

                Dim periodo As String = CType(row.FindControl("lblPeriodo"), Label).Text
                Dim grado As String = CType(row.FindControl("lblGrado"), Label).Text
                Dim aula As String = CType(row.FindControl("lblAula"), Label).Text
                Dim nombrealumno As String = CType(row.FindControl("lblNombreCompleto"), Label).Text
                Dim codigoaula As String = CType(row.FindControl("lblCodigoAula"), Label).Text
                hiddenCodigoAula.Value = codigoaula

                cargarPlanCurricular(codigo, nombrealumno, periodo, grado, aula)

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        'Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
        'Dim btnActivar As ImageButton = e.Row.FindControl("btnActivar")
        'Dim btnGrupo As ImageButton = e.Row.FindControl("btnGrupo")

        If e.Row.RowType = DataControlRowType.DataRow Then

            CType(e.Row.FindControl("lblidx"), Label).Text = e.Row.RowIndex + 1

            'If (e.Row.DataItem("CodigoTipocurso") = 1 And e.Row.DataItem("CantGrupos") = 0) Then
            '    btnGrupo.Visible = False
            'End If

            'btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
            'If (e.Row.DataItem("CodigoTipocurso") = 1 And e.Row.DataItem("CantCursosInternos") > 0) Then
            '    btnEliminar.Visible = False
            'End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

#Region "Eventos Registro Notas"

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'If ddlGrado.SelectedValue > 0 Then
            '    listarAlumnos()
            'End If
            grabarNotas()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            miTab1.Enabled = True
            miTab2.Enabled = False
            TabContainer1.ActiveTabIndex = 0
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos Registro Notas"

    Private Sub cargarPlanCurricular(ByVal str_codigoAlumno As String, _
                                     ByVal str_nombreAlumno As String, _
                                     ByVal str_periodo As String, _
                                     ByVal str_grado As String, _
                                     ByVal str_aula As String)

        miTab1.Enabled = False
        miTab2.Enabled = True
        TabContainer1.ActiveTabIndex = 1

        lblPeriodoAlumno.Text = str_periodo
        lblGradoAlumno.Text = str_grado 'Replace(str_grado, "Grado ", "")
        lblAulaAlumno.Text = str_aula
        lblCodigoAlumno.Text = str_codigoAlumno
        lblNombreAlumno.Text = str_nombreAlumno

        Dim int_AnioAcademico As Integer = ddlPeriodo.SelectedValue
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue
        Dim int_CodigoAula As Integer = hiddenCodigoAula.Value

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        Dim obj_BL_AsignacionCursos As New bl_AsignacionCursos
        ds_Lista = obj_BL_AsignacionCursos.FUN_LIS_AsignacionCursosHistorico(str_codigoAlumno, int_CodigoGrado, int_CodigoAula, int_AnioAcademico, _
                                                                             int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        GridView2.DataSource = ds_Lista.Tables(0)
        GridView2.DataBind()

    End Sub

    Private Sub grabarNotas()

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim lstNotas As New List(Of be_RegistroNotasAnualCuantitativo)

        For Each gvr As GridViewRow In GridView2.Rows

            Dim be_rnac As New be_RegistroNotasAnualCuantitativo
            be_rnac.CodigoRegistroAnual = CType(gvr.FindControl("lblCodigoRegistroNotaAnual"), Label).Text
            be_rnac.CodigoAsignacionGrupo = CType(gvr.FindControl("lblCodigoAsignacionGrupo"), Label).Text
            be_rnac.CodigoAnioAcademico = CType(gvr.FindControl("lblCodigoAnioAcademico"), Label).Text
            be_rnac.CodigoAlumno = CType(gvr.FindControl("lblCodigoAlumno"), Label).Text
            be_rnac.NotaAnual = IIf(CType(gvr.FindControl("tbNotaFinal"), TextBox).Text.Length = 0, 0, CType(gvr.FindControl("tbNotaFinal"), TextBox).Text)

            be_rnac.auxNota1 = IIf(CType(gvr.FindControl("tbNota1"), TextBox).Text.Length = 0, 0, CType(gvr.FindControl("tbNota1"), TextBox).Text)
            be_rnac.auxNota2 = IIf(CType(gvr.FindControl("tbNota2"), TextBox).Text.Length = 0, 0, CType(gvr.FindControl("tbNota2"), TextBox).Text)
            be_rnac.auxNota3 = IIf(CType(gvr.FindControl("tbNota3"), TextBox).Text.Length = 0, 0, CType(gvr.FindControl("tbNota3"), TextBox).Text)
            be_rnac.auxNota4 = IIf(CType(gvr.FindControl("tbNota4"), TextBox).Text.Length = 0, 0, CType(gvr.FindControl("tbNota4"), TextBox).Text)

            be_rnac.auxCodigoRegistroBimestral1 = CType(gvr.FindControl("lblCodigoRegistroBimestral1"), Label).Text
            be_rnac.auxCodigoRegistroBimestral2 = CType(gvr.FindControl("lblCodigoRegistroBimestral2"), Label).Text
            be_rnac.auxCodigoRegistroBimestral3 = CType(gvr.FindControl("lblCodigoRegistroBimestral3"), Label).Text
            be_rnac.auxCodigoRegistroBimestral4 = CType(gvr.FindControl("lblCodigoRegistroBimestral4"), Label).Text

            lstNotas.Add(be_rnac)

        Next

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""
        Dim obj_BL_RegistroNotasAnualCuantitativo As New bl_RegistroNotasAnualCuantitativo

        usp_valor = obj_BL_RegistroNotasAnualCuantitativo.FUN_INS_RegistroNotasAnualCuantitativoHistorica( _
            lstNotas, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            'btnCancelar_Click()
            'limpiarCampos()
            cargarPlanCurricular(lblCodigoAlumno.Text, lblNombreAlumno.Text, lblPeriodoAlumno.Text, lblGradoAlumno.Text, lblAulaAlumno.Text)
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If


    End Sub

#End Region

#Region "Eventos Gridview Registro Notas"

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "seleccionar" Then
                Dim codigo As String = CInt(e.CommandArgument.ToString)
                'Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                'Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                'cargarPlanCurricular(codigo)

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            CType(e.Row.FindControl("lblidx"), Label).Text = e.Row.RowIndex + 1

            CType(e.Row.FindControl("tbNota1"), TextBox).Text = IIf(e.Row.DataItem("Nota1").ToString > 0, e.Row.DataItem("Nota1").ToString, "")
            CType(e.Row.FindControl("tbNota2"), TextBox).Text = IIf(e.Row.DataItem("Nota2").ToString > 0, e.Row.DataItem("Nota2").ToString, "")
            CType(e.Row.FindControl("tbNota3"), TextBox).Text = IIf(e.Row.DataItem("Nota3").ToString > 0, e.Row.DataItem("Nota3").ToString, "")
            CType(e.Row.FindControl("tbNota4"), TextBox).Text = IIf(e.Row.DataItem("Nota4").ToString > 0, e.Row.DataItem("Nota4").ToString, "")
            CType(e.Row.FindControl("tbNotaFinal"), TextBox).Text = IIf(e.Row.DataItem("NotaFinal").ToString > 0, e.Row.DataItem("NotaFinal").ToString, "")

            If e.Row.Cells(2).Text = "O" Then ' Curso Oficial
                e.Row.Cells(1).Style("text-align") = "left"
                e.Row.Cells(1).Style("color") = "blue"
                e.Row.Cells(1).Style("font-weight") = "bold"
            ElseIf e.Row.Cells(2).Text = "C" Then ' Curso Complementario
                e.Row.Cells(1).Style("text-align") = "left"
                e.Row.Cells(1).Style("color") = "green"
                e.Row.Cells(1).Style("font-weight") = "bold"
            ElseIf e.Row.Cells(2).Text = "I" Then ' curso Interno
                e.Row.Cells(1).Style("text-align") = "right"
                e.Row.Cells(1).Style("color") = "red"
                e.Row.Cells(1).Style("font-weight") = "bold"
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

End Class
