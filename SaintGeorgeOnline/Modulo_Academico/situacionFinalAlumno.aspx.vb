Imports System.Web.Services
Imports System.Web.Script.Services
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports System.Data
Imports SaintGeorgeOnline_BusinessLogic.ModuloCursos
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic

Partial Class Modulo_Academico_situacionFinalAlumno
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1
    Public dtSituacionMatricula As New DataTable
#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            cargarSituacionMarticula()
            Me.Master.MostrarTitulo("Registro de Notas")
            If Not Page.IsPostBack Then



                cargarComboAnioAcademico()
                ddlPeriodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                cargarComboGrados()

            End If
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlGrado.SelectedValue > 0 Then
                listarAlumnos()
            End If
        Catch ex As Exception
            'EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlGrado.SelectedValue > 0 Then
                listarAlumnos()
            End If
        Catch ex As Exception
            'EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboAnioAcademico()
        Dim int_CodigoAnioTop As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicosHistorico(int_CodigoAnioTop, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlPeriodo, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    Private Sub cargarComboGrados()
        Dim int_CodigoNivel As Integer = 0 ' secundaria
        Dim int_Top As Integer = 14 ' ultimos 5 grados
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


                cargarPlanCurricular(codigo, nombrealumno, periodo, grado, aula)

            End If
        Catch ex As Exception
            'EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        Dim cmbSituacionMatricula As DropDownList
        Dim CodSituacionMatricula As String
        If e.Row.RowType = DataControlRowType.DataRow Then

            CType(e.Row.FindControl("lblidx"), Label).Text = e.Row.RowIndex + 1



            CodSituacionMatricula = CType(e.Row.FindControl("lblCodSituacionMatricula"), Label).Text
            cmbSituacionMatricula = CType(e.Row.FindControl("cmbSituacionAlumno"), DropDownList)

            cmbSituacionMatricula.DataSource = dtSituacionMatricula

            cmbSituacionMatricula.DataTextField = "nombreDescripcion"
            cmbSituacionMatricula.DataValueField = "codSituacionMatricula"
            cmbSituacionMatricula.DataBind()

            cmbSituacionMatricula.SelectedValue = CodSituacionMatricula

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


        Catch ex As Exception
            'EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


        Catch ex As Exception
            'EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos Registro Notas"

    Private Sub cargarPlanCurricular(ByVal str_codigoAlumno As String, _
                                     ByVal str_nombreAlumno As String, _
                                     ByVal str_periodo As String, _
                                     ByVal str_grado As String, _
                                     ByVal str_aula As String)




        Dim int_AnioAcademico As Integer = ddlPeriodo.SelectedValue
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue



        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet




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
            'EnvioEmailError(int_CodigoAccion, ex.ToString)
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
#Region "cargar situacion marticula"
    Private Sub cargarSituacionMarticula()
        Try

            Dim dcPAram As New Dictionary(Of String, Object)



            Dim dst As New DataSet
            Dim nParam As String = "USP_lisSituacionFinal"
            dtSituacionMatricula = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dcPAram, nParam).Tables(0)



        Catch ex As Exception

        End Try


    End Sub

#End Region

    Protected Sub btnGrabar_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim oBL_StuacionMatricula As New BL_StuacionMatricula
        Dim dc As New Dictionary(Of Integer, Integer)



        For Each filas As GridViewRow In GridView1.Rows
            dc.Add(CType(filas.FindControl("lblCodMraticula"), Label).Text, CType(filas.FindControl("cmbSituacionAlumno"), DropDownList).SelectedValue)
        Next

        Dim cod As Integer = 0



        cod = oBL_StuacionMatricula.F_funcionActualizarSituacionMAtricula(dc)
        If cod = 0 Then
            MostrarSexyAlertBox("Error al actualizar  ", "Alert")
        Else
            MostrarSexyAlertBox("Se actualizaron correctamente  <br />", "Info")
        End If
    End Sub
End Class
