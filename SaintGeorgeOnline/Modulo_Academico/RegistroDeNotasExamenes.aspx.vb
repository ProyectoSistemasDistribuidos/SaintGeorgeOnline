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

Partial Class Modulo_Academico_RegistroDeNotasExamenes
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Registro de Notas de Examenes")
            If Not Page.IsPostBack Then
                cargarComboAnioAcademico()
                cargarComboGrados()
                ddlPeriodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                cargarComboAsignacionAulas()
                cargarGrillaAlumnos()
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


    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
             grabar()
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
    End Sub

    Private Sub cargarGrillaAlumnos()

        Dim obl_bl_ProgramacionExamen As New bl_ProgramacionExamen
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 

        Dim int_CodigoAnioAcademico As Integer = ddlPeriodo.SelectedValue
        Dim int_CodigoProfesor As Integer = int_CodigoUsuario

        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue
        Dim int_CodigoAula As Integer = ddlAula.SelectedValue

        Dim ds_Lista As DataSet = obl_bl_ProgramacionExamen.FUN_LIS_ProgramacionExamenesPorProfesor( _
            int_CodigoAnioAcademico, int_CodigoProfesor, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblidx As System.Web.UI.WebControls.Label = e.Row.FindControl("lblidx")
            lblidx.Text = e.Row.RowIndex + 1

            If e.Row.DataItem("Nota").ToString.Length > 0 Then
                CType(e.Row.FindControl("tbNota"), TextBox).Text = e.Row.DataItem("Nota")
            Else
                CType(e.Row.FindControl("tbNota"), TextBox).Text = ""
            End If
            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If
    End Sub

#End Region

#Region "Modal"

    Private Sub grabar()

        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 

        Dim obl_ProgramacionExamen As New bl_ProgramacionExamen
        Dim obe_RegistroNotasCargo As be_RegistroNotasCargo

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""

        Dim int_total As Integer = 0
        Dim bool_sel As Boolean = False

        For Each gvr As GridViewRow In GridView1.Rows

            If CType(gvr.FindControl("chkG"), CheckBox).Checked = True Then
                bool_sel = True

                obe_RegistroNotasCargo = New be_RegistroNotasCargo
                obe_RegistroNotasCargo.CodigoRegistroCargo = CType(gvr.FindControl("lblcodRegistroCargo"), Label).Text

                obe_RegistroNotasCargo.TipoNota = CType(gvr.FindControl("lblTipoReg"), Label).Text
                obe_RegistroNotasCargo.Nota = CType(gvr.FindControl("tbNota"), TextBox).Text.Trim

                usp_valor = obl_ProgramacionExamen.FUN_UPD_ProgramacionExamenesNota( _
                    obe_RegistroNotasCargo, usp_mensaje, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                If usp_valor > 0 Then
                    int_total += 1
                End If

            End If

        Next

        If bool_sel Then
            If int_total > 0 Then
                MostrarSexyAlertBox("Operación exitosa. <br />Se actualizaron " & int_total.ToString & " registro(s).", "Info")
                cargarGrillaAlumnos()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Else
            MostrarSexyAlertBox("No ha seleccionado ningún registro.", "Alert")
        End If

    End Sub

    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region

End Class
