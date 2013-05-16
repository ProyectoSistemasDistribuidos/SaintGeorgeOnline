Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloAcademico
Imports SaintGeorgeOnline_BusinessEntities.ModuloAcademico
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML
Imports ClosedXML.Excel

Partial Class Modulo_Academico_ProgramacionDeExamenes
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Programación de Examenes")
            If Not Page.IsPostBack Then

                cargarComboAnioAcademico()
                cargarComboGrados()

                ddlPeriodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                'ddlPeriodoModal.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar

                cargarComboAsignacionAulas()
                cargarGrillaAlumnos()

                cargarComboProfesores()
                cargarComboTipoExamen()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlPeriodo.SelectedValue > 0 Then
                cargarComboGrados()
                cargarComboAsignacionAulas()
                cargarGrillaAlumnos()
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlPeriodo.SelectedValue > 0 Then
                cargarComboAsignacionAulas()
                cargarGrillaAlumnos()
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlAula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlPeriodo.SelectedValue > 0 Then
                cargarGrillaAlumnos()
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboGrados()
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlGrado, ds_Lista, "Codigo", "DescripcionEspaniol", True, False)
    End Sub

    Private Sub cargarComboAsignacionAulas()
        Dim int_CodigoPeriodo As Integer = ddlPeriodo.SelectedValue
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasParaGrupos( _
            int_CodigoPeriodo, int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlAula, ds_Lista, "CodigoAula", "DescAula", True, False)
    End Sub

    Private Sub cargarComboAnioAcademico()
        Dim int_CodigoAnioTop As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlPeriodo, ds_Lista, "Codigo", "Descripcion", False, True)
        'Controles.llenarCombo(ddlPeriodoModal, ds_Lista, "Codigo", "Descripcion", False, False)
    End Sub

    Private Sub cargarGrillaAlumnos()

        Dim obl_bl_ProgramacionExamen As New bl_ProgramacionExamen
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 

        Dim int_CodigoAnioAcademico As Integer = ddlPeriodo.SelectedValue
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue
        Dim int_CodigoAula As Integer = ddlAula.SelectedValue
        Dim str_CodigoAlumno As String = ""

        Dim ds_Lista As DataSet = obl_bl_ProgramacionExamen.FUN_LIS_ProgramacionExamenes( _
            int_CodigoAnioAcademico, str_CodigoAlumno, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

    End Sub

    Private Sub cargarDetalle(ByVal int_CodigoAnual As Integer, ByVal int_TipoReg As Integer, _
                               ByVal str_idx As String, _
                               ByVal str_Anio As String, ByVal str_Nivel As String, _
                               ByVal str_Grado As String, ByVal str_Aula As String, _
                               ByVal str_cAlumno As String, ByVal str_nAlumno As String, _
                               ByVal str_Curso As String, ByVal str_nota As String)

        Dim obl_ProgramacionExamen As New bl_ProgramacionExamen
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 

        Dim ds_Lista As DataSet = obl_ProgramacionExamen.FUN_LIS_DetalleProgramacionExamenes( _
            int_TipoReg, int_CodigoAnual, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        lblidx_d.Text = str_idx
        lblanio_d.Text = str_Anio
        lblnivel_d.Text = str_Nivel
        lblgrado_d.Text = str_Grado
        lblaula_d.Text = str_Aula
        lblcalumno_d.Text = str_cAlumno
        lblnalumno_d.Text = str_nAlumno
        lblcurso_d.Text = str_Curso
        lblnota_d.Text = str_nota

        GridView2.DataSource = ds_Lista.Tables(0)
        GridView2.DataBind()

    End Sub

    Private Sub cargarDetalle(ByVal int_CodigoAnual As Integer, ByVal int_TipoReg As Integer)

        Dim obl_ProgramacionExamen As New bl_ProgramacionExamen
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 

        Dim ds_Lista As DataSet = obl_ProgramacionExamen.FUN_LIS_DetalleProgramacionExamenes( _
            int_TipoReg, int_CodigoAnual, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView2.DataSource = ds_Lista.Tables(0)
        GridView2.DataBind()

    End Sub

    Private Sub eliminarDetalle(ByVal int_CodigoSubsa As Integer, ByVal int_CodigoAnual As Integer, ByVal int_TipoReg As Integer)

        Dim obl_ProgramacionExamen As New bl_ProgramacionExamen
        Dim obe_RegistroNotasCargo As New be_RegistroNotasCargo

        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 

        obe_RegistroNotasCargo.CodigoRegistroCargo = int_CodigoSubsa
        obe_RegistroNotasCargo.CodigoRegistroAnual = int_CodigoAnual
        obe_RegistroNotasCargo.TipoNota = int_TipoReg

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""

        usp_valor = obl_ProgramacionExamen.FUN_DEL_ProgramacionExamenes(obe_RegistroNotasCargo, usp_mensaje, _
                                                                        int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            cargarDetalle(int_CodigoAnual, int_TipoReg)
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    Private Sub limpiarDetalle()

        lblidx_d.Text = ""
        lblanio_d.Text = ""
        lblnivel_d.Text = ""
        lblgrado_d.Text = ""
        lblaula_d.Text = ""
        lblcalumno_d.Text = ""
        lblnalumno_d.Text = ""
        lblcurso_d.Text = ""
        lblnota_d.Text = ""

        GridView2.DataBind()

    End Sub

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblidx As System.Web.UI.WebControls.Label = e.Row.FindControl("lblidx")
            lblidx.Text = e.Row.RowIndex + 1

            Dim btnDetalle As System.Web.UI.WebControls.LinkButton = e.Row.FindControl("btnDetalle")

            If CType(e.Row.FindControl("lblcRNCC"), Label).Text > 0 Then

                e.Row.Cells(5).Style("color") = "#4b8efa"
                e.Row.Cells(5).Style("font-weight") = "bold"
                e.Row.Cells(6).Style("color") = "#4b8efa"
                e.Row.Cells(6).Style("font-weight") = "bold"
                e.Row.Cells(7).Style("color") = "#4b8efa"
                e.Row.Cells(7).Style("font-weight") = "bold"

                'e.Row.Cells(5).Style("color") = "#265589"
                'e.Row.Cells(5).Style("font-weight") = "bold"
                'e.Row.Cells(6).Style("color") = "#265589"
                'e.Row.Cells(6).Style("font-weight") = "bold"
                'e.Row.Cells(7).Style("color") = "#265589"
                'e.Row.Cells(7).Style("font-weight") = "bold"


                btnDetalle.Visible = True
            Else
                btnDetalle.Visible = False
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "nuevo" Or e.CommandName = "detalle" Then

                Dim int_CodigoAnual As Integer = CInt(e.CommandArgument.ToString) ' Codigo Nota Anual
                Dim btn As LinkButton = CType(e.CommandSource, LinkButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                Dim int_TipoReg As Integer = CType(row.FindControl("lbltipoReg"), Label).Text ' tipo de registro cuantitativo o cualitativo

                If e.CommandName = "nuevo" Then

                    Dim str_CodigoSubsa As String = 0 'CType(row.FindControl("lblcRNCCT"), Label).Text ' cantidad de registros existentes Subsa Ct

                    Dim str_Anio As String = CType(row.FindControl("lbldAnio"), Label).Text
                    Dim str_Nivel As String = CType(row.FindControl("lbldNivel"), Label).Text
                    Dim str_Grado As String = CType(row.FindControl("lbldGrado"), Label).Text
                    Dim str_Aula As String = CType(row.FindControl("lbldAula"), Label).Text
                    Dim str_cAlumno As String = CType(row.FindControl("lblcAlumno"), Label).Text
                    Dim str_nAlumno As String = CType(row.FindControl("lblnAlumno"), Label).Text
                    Dim str_Curso As String = CType(row.FindControl("lblnCurso"), Label).Text
                    Dim str_nota As String = CType(row.FindControl("lblnota"), Label).Text

                    mostrarPanel(int_CodigoAnual, int_TipoReg, str_CodigoSubsa, _
                                 str_Anio, str_Nivel, str_Grado, str_Aula, str_cAlumno, str_nAlumno, str_Curso, str_nota)

                    limpiarDetalle()

                ElseIf e.CommandName = "detalle" Then

                    Dim str_idx As String = CType(row.FindControl("lblidx"), Label).Text
                    Dim str_Anio As String = CType(row.FindControl("lbldAnio"), Label).Text
                    Dim str_Nivel As String = CType(row.FindControl("lbldNivel"), Label).Text
                    Dim str_Grado As String = CType(row.FindControl("lbldGrado"), Label).Text
                    Dim str_Aula As String = CType(row.FindControl("lbldAula"), Label).Text
                    Dim str_cAlumno As String = CType(row.FindControl("lblcAlumno"), Label).Text
                    Dim str_nAlumno As String = CType(row.FindControl("lblnAlumno"), Label).Text
                    Dim str_Curso As String = CType(row.FindControl("lblnCurso"), Label).Text
                    Dim str_nota As String = CType(row.FindControl("lblnota"), Label).Text

                    cargarDetalle(int_CodigoAnual, int_TipoReg, _
                                  str_idx, str_Anio, str_Nivel, str_Grado, str_Aula, str_cAlumno, str_nAlumno, str_Curso, str_nota)

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblidx As System.Web.UI.WebControls.Label = e.Row.FindControl("lblidx")
            lblidx.Text = e.Row.RowIndex + 1

            Dim btnEliminar As System.Web.UI.WebControls.LinkButton = e.Row.FindControl("btnEliminar")
            btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If
    End Sub

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "editar" Then

                Dim int_CodigoAnual As Integer = CInt(e.CommandArgument.ToString) ' Codigo Nota Anual
                Dim btn As LinkButton = CType(e.CommandSource, LinkButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                Dim int_TipoReg As Integer = CType(row.FindControl("lbltipoReg"), Label).Text ' tipo de registro cuantitativo o cualitativo
                Dim str_CodigoSubsa As String = CType(row.FindControl("lblcRC"), Label).Text ' codigo del registro de cargo

                Dim str_Anio As String = lblanio_d.Text
                Dim str_Nivel As String = lblnivel_d.Text
                Dim str_Grado As String = lblgrado_d.Text
                Dim str_Aula As String = lblaula_d.Text
                Dim str_cAlumno As String = lblcalumno_d.Text
                Dim str_nAlumno As String = lblnalumno_d.Text
                Dim str_Curso As String = lblcurso_d.Text
                Dim str_Nota As String = lblnota_d.Text

                mostrarPanel(int_CodigoAnual, int_TipoReg, str_CodigoSubsa, _
                             str_Anio, str_Nivel, str_Grado, str_Aula, str_cAlumno, str_nAlumno, str_Curso, str_Nota)

            ElseIf e.CommandName = "eliminar" Then

                Dim int_CodigoSubsa As Integer = CInt(e.CommandArgument.ToString) ' Codigo del registro de cargo
                Dim btn As LinkButton = CType(e.CommandSource, LinkButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                Dim int_TipoReg As Integer = CType(row.FindControl("lbltipoReg"), Label).Text ' tipo de registro cuantitativo o cualitativo
                Dim int_CodigoAnual As Integer = CType(row.FindControl("lblcRNA"), Label).Text ' Codigo Nota Anual

                eliminarDetalle(int_CodigoSubsa, int_CodigoAnual, int_TipoReg)

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub


#End Region

#Region "Modal"

    Private Sub cargarComboTipoExamen()
        Dim obl_TipoExamenCargo As New bl_TipoExamenCargo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obl_TipoExamenCargo.FUN_LIS_TipoExamenCargo("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlTipoExamenModal, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    Private Sub cargarComboProfesores()
        Dim obj_BL_MaestroPersonas As New bl_MaestroPersonas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_MaestroPersonas.FUN_LIS_PersonasProfesores(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlProfesorModal, ds_Lista, "CodigoPersona", "NombreApellidoPaterno", False, True)
    End Sub

    Private Sub mostrarPanel(ByVal int_cNotaAnual As Integer, ByVal int_TipoReg As Integer, ByVal int_cSubsa As Integer, _
        ByVal str_Anio As String, ByVal str_Nivel As String, ByVal str_Grado As String, ByVal str_Aula As String, _
        ByVal str_cAlumno As String, ByVal str_nAlumno As String, ByVal str_Curso As String, ByVal str_Nota As String)

        hiddenCodigoNotaAnual.Value = int_cNotaAnual
        hiddenCodigoSubsa.Value = int_cSubsa
        hiddenTipoNota.Value = int_TipoReg

        If int_cSubsa > 0 Then
            Dim obl_ProgramacionExamen As New bl_ProgramacionExamen
            Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 
            Dim ds_Lista As DataSet = obl_ProgramacionExamen.FUN_GET_ProgramacionExamenes( _
                hiddenTipoNota.Value, int_cSubsa, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            ddlProfesorModal.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaProfesor")
            ddlTipoExamenModal.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoTipoExamen")
            tbFechaModal.Text = ds_Lista.Tables(0).Rows(0).Item("FechaExamen")
            tbNotaModal.Text = ds_Lista.Tables(0).Rows(0).Item("Nota")
        Else
            ddlProfesorModal.SelectedValue = 0
            ddlTipoExamenModal.SelectedValue = 0
            tbFechaModal.Text = Today
            tbNotaModal.Text = ""
        End If

        lblAnioModal.Text = str_Anio
        lblNivelModal.Text = str_Nivel
        lblGradoModal.Text = str_Grado
        lblAulaModal.Text = str_Aula
        lblAlumnoModal.Text = str_nAlumno
        lblCursoModal.Text = str_Curso
        lblNotaModal.text = str_Nota
        pnModalRegistro.Show()

    End Sub

    Private Sub grabar()

        Dim obl_ProgramacionExamen As New bl_ProgramacionExamen
        Dim obe_RegistroNotasCargo As New be_RegistroNotasCargo

        obe_RegistroNotasCargo.TipoNota = hiddenTipoNota.Value
        obe_RegistroNotasCargo.CodigoRegistroCargo = hiddenCodigoSubsa.Value
        obe_RegistroNotasCargo.CodigoRegistroAnual = hiddenCodigoNotaAnual.Value

        obe_RegistroNotasCargo.CodigoPersonaProfesor = ddlProfesorModal.SelectedValue
        obe_RegistroNotasCargo.FechaExamen = tbFechaModal.Text
        obe_RegistroNotasCargo.CodigoTipoExamen = ddlTipoExamenModal.SelectedValue
        obe_RegistroNotasCargo.Nota = tbNotaModal.Text.Trim

        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""

        usp_valor = obl_ProgramacionExamen.FUN_INS_ProgramacionExamenes(obe_RegistroNotasCargo, usp_mensaje, _
                                                                        int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            cargarGrillaAlumnos()

            If hiddenCodigoSubsa.Value > 0 Then ' si se selecciono algun detalle
                Dim int_CodigoAnual As Integer = hiddenCodigoNotaAnual.Value ' Codigo Nota Anual
                Dim int_TipoReg As Integer = hiddenTipoNota.Value ' tipo de registro cuantitativo o cualitativo
                cargarDetalle(int_CodigoAnual, int_TipoReg)
            End If

        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
            pnModalRegistro.Show()
        End If

    End Sub

    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If IsDate(tbFechaModal.Text) Then
            If tbFechaModal.Text.ToString.Length = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Fecha de Examen")
                result = False
            End If
        Else
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Examen")
            result = False
        End If

        If ddlProfesorModal.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Profesor")
            result = False
        End If

        If ddlTipoExamenModal.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Examen")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Protected Sub btnModalRegistro_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validar(usp_mensaje) Then
                grabar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarRegistro_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'Exportar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

#End Region

End Class
