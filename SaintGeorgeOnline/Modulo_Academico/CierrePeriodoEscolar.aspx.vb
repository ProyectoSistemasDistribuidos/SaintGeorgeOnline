Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloCursos
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_BusinessLogic.ModuloAcademico
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities.ModuloAcademico
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports SaintGeorgeOnline_BusinessLogic

Partial Class Modulo_Academico_CierrePeriodoEscolar
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Cierre de Periodo - Nota Final")
            If Not Page.IsPostBack Then
                btnCalcularNota.Attributes.Add("OnClick", "return confirm_procesar();")
                cargarComboAnioAcademico()
                ddlPeriodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                cargarComboNiveles()
                cargarComboGrados()
                listarAsignacionAulas()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            listarAsignacionAulas()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarComboGrados()
            listarAsignacionAulas()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            listarAsignacionAulas()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCalcularNota_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim codGrado As Integer = 0
        Dim codPeriodo As Integer = 0
        codGrado = ddlGrado.SelectedValue
        codPeriodo = ddlPeriodo.SelectedValue

        Dim codOperacion As Integer = 0
        Dim lstIdsConducta As New List(Of Integer)
        Try
            If ddlNivel.SelectedValue = 1 Then 'inicial

            ElseIf ddlNivel.SelectedValue = 2 Then 'primaria

                If ddlGrado.SelectedValue <> 9 Then
                    CalcularNotaOficialesInicialPrimaria(codPeriodo)
                End If

                If ddlPeriodo.SelectedValue <> 0 Then


                    If ddlGrado.SelectedValue <> 0 Then

                        If ddlGrado.SelectedValue <> 9 Then
                            lstIdsConducta = F_crearNotasConducta(codGrado, codPeriodo) 'funcion  para exportar  notas de conducta
                            codOperacion = F_actualizarPromedioFinal(codPeriodo, codGrado)

                            If codOperacion = 0 Then
                                MostrarSexyAlertBox("Error al actualizar las notas finales ", "Alert")
                            Else
                                MostrarSexyAlertBox("Se actualizaron correctamente  <br />", "Info")
                            End If

                            If lstIdsConducta.Contains(0) Then
                                MostrarSexyAlertBox("Error al actualizar las notas finales ", "Alert")
                            Else
                                MostrarSexyAlertBox("Se actualizaron correctamente  <br />", "Info")
                            End If

                        End If

                    End If

                End If


            End If
            If ddlGrado.SelectedValue = 9 Then 'grado 6 se calcula como secundaria
                CalcularNota()
            End If

            If ddlNivel.SelectedValue = 3 Then 'secundaria
                CalcularNota()
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCalcularNotaOf_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim codOperacion As Integer = 0
        Dim lstIdsConducta As New List(Of Integer)
        Try

            If ddlNivel.SelectedValue = 1 Then 'inicial

            ElseIf ddlNivel.SelectedValue = 2 Then 'primaria
                If ddlGrado.SelectedValue <> 9 Then

                End If






                'ddlPeriodo
                'ddlGrado

                If ddlGrado.SelectedValue = 9 Then 'grado 6 se calcula como secundaria
                    CalcularNotaOficialesConInternos()
                End If

            ElseIf ddlNivel.SelectedValue = 3 Then 'secundaria
                CalcularNotaOficialesConInternos()
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub



#End Region

#Region "Metodos"

    Private Sub cargarComboAnioAcademico()
        Dim int_CodigoAnioTop As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlPeriodo, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    Private Sub cargarComboNiveles()
        Dim obj_BL_NivelesMinisterio As New bl_NivelesMinisterio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_NivelesMinisterio.FUN_LIS_NivelesMinisterio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlNivel, ds_Lista, "Codigo", "Descripcion", False, False)
    End Sub

    Private Sub cargarComboGrados()
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_GradosXCodigoNivelMinisterio(ddlNivel.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlGrado, ds_Lista, "Codigo", "DescripcionEspaniol", True, False)
    End Sub

    Private Sub listarAsignacionAulas()
        Dim int_CodigoPeriodo As Integer = ddlPeriodo.SelectedValue
        Dim int_CodigoNivel As Integer = ddlNivel.SelectedValue()
        Dim int_CodigoGrado As Integer = ddlGrado.SelectedValue

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasParaGruposXNivelMinisterio( _
            int_CodigoPeriodo, int_CodigoGrado, int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()
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

    Private Sub CalcularNota()

        Dim obl_CierrePeriodo As New bl_CierrePeriodo
        Dim obe_CierrePeriodo As be_CierrePeriodo

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""
        Dim lstResultado As New List(Of String)

        Dim str_Grado As String = ""
        Dim str_Aula As String = ""

        Dim str_Mensaje As New StringBuilder
        Dim bool_chk As Boolean = False

        For Each gvr As GridViewRow In GridView1.Rows
            If CType(gvr.FindControl("chkG"), CheckBox).Checked = True Then
                bool_chk = True
                obe_CierrePeriodo = New be_CierrePeriodo
                obe_CierrePeriodo.CodigoPeriodo = ddlPeriodo.SelectedValue
                obe_CierrePeriodo.CodigoAsignacionAula = CType(gvr.FindControl("lblCodigoAsignacionAula"), Label).Text
                str_Grado = CType(gvr.FindControl("lblGrado"), Label).Text
                str_Aula = CType(gvr.FindControl("lblAula"), Label).Text
                usp_valor = obl_CierrePeriodo.FUN_UPD_NotaFinal(obe_CierrePeriodo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                str_Mensaje.Append(str_Grado & " - " & str_Aula & ": " & usp_valor & " registro(s)." & "<br />")
            End If
        Next

        If bool_chk Then
            MostrarSexyAlertBox("Se actualizaron los siguientes registros:<br />" & str_Mensaje.ToString, "Info")
        Else
            MostrarSexyAlertBox("Debe seleecionar por lo menos 1 aula.", "Alert")
        End If

    End Sub


    Private Sub CalcularNotaOficialesConInternos()

        Dim obl_CierrePeriodo As New bl_CierrePeriodo
        Dim obe_CierrePeriodo As be_CierrePeriodo

        Dim int_CodigoBimestre As Integer = ddlBimestre.SelectedValue
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""
        Dim lstResultado As New List(Of String)

        Dim str_Grado As String = ""
        Dim str_Aula As String = ""

        Dim str_Mensaje As New StringBuilder
        Dim bool_chk As Boolean = False

        For Each gvr As GridViewRow In GridView1.Rows
            If CType(gvr.FindControl("chkG"), CheckBox).Checked = True Then
                bool_chk = True
                obe_CierrePeriodo = New be_CierrePeriodo
                obe_CierrePeriodo.CodigoPeriodo = ddlPeriodo.SelectedValue
                obe_CierrePeriodo.CodigoAsignacionAula = CType(gvr.FindControl("lblCodigoAsignacionAula"), Label).Text
                str_Grado = CType(gvr.FindControl("lblGrado"), Label).Text
                str_Aula = CType(gvr.FindControl("lblAula"), Label).Text

                If int_CodigoBimestre = 5 Then ' anual
                    usp_valor = obl_CierrePeriodo.FUN_UPD_NotaFinalcOficialcInterno( _
                        obe_CierrePeriodo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                Else ' x bimestre
                    obe_CierrePeriodo.CodigoBimestre = ddlBimestre.SelectedValue
                    usp_valor = obl_CierrePeriodo.FUN_UPD_NotaFinalcOficialcInternoXBimestre( _
                        obe_CierrePeriodo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                End If

                str_Mensaje.Append(str_Grado & " - " & str_Aula & ": " & usp_valor & " registro(s)." & "<br />")
            End If
        Next

        If bool_chk Then
            MostrarSexyAlertBox("Se actualizaron los siguientes registros:<br />" & str_Mensaje.ToString, "Info")
        Else
            MostrarSexyAlertBox("Debe seleecionar por lo menos 1 aula.", "Alert")
        End If

    End Sub



#End Region


#Region "Eventos Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("lblidx"), Label).Text = e.Row.RowIndex + 1
            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If
    End Sub

#End Region


#Region "Nota inicial - primaria"

    Private Sub CalcularNotaOficialesInicialPrimaria(ByVal codAnio As Integer)

        Dim lsTIds As New List(Of Integer)
        Dim bool_chk As Boolean = False

        For Each gvr As GridViewRow In GridView1.Rows
            If CType(gvr.FindControl("chkG"), CheckBox).Checked = True Then
                bool_chk = True
                lsTIds = F_replicaNotasPrimaria(CType(gvr.FindControl("lblCodigoAsignacionAula"), Label).Text, codAnio)
            End If
        Next
        If lsTIds.Contains(0) Then
            MostrarSexyAlertBox("Error itente de nuevo", "Alert")
        Else
            MostrarSexyAlertBox("Se realizo correctamente ", "Info")
        End If

    End Sub
    Private Function F_actualizarPromedioFinal(ByVal codPerido As Integer, ByVal codGrado As Integer) As Integer
        Try
            Dim dcPAramII As New Dictionary(Of String, Object)
            Dim nombreSP As String = "USP_listarMatrizCursoOficial_calcularPromedioFinal"

            dcPAramII.Add("GD_CodigoGrado", codGrado)
            dcPAramII.Add("AC_CodigoAnioAcademico", codPerido)
            dcPAramII.Add("TC_CodigoTipoCurso", 1)

            Dim oBL_CrearEstructuraNotasOficialesII As New BL_CrearEstructuraNotasOficiales
            Dim dst As DataTable
            dst = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dcPAramII, nombreSP).Tables(0)
            Dim estadoOperacion As Integer = 0
            estadoOperacion = oBL_CrearEstructuraNotasOficialesII.f_actualizarNotaFinalPrimaria(dst)

            Return estadoOperacion
        Catch ex As Exception

        End Try
    End Function

    Private Function F_replicaNotasPrimaria(ByVal coAsignacionAula As Integer, ByVal codAnio As Integer) As List(Of Integer)
        Dim Obl_rep_libretaNotas As New bl_rep_libretaNotas
        Dim dcPAram As New Dictionary(Of String, Object)
        Dim lstIds As New List(Of Integer)
        Dim lstsBimestres As Integer() = {1, 2, 3, 4}
         


        For Each codBim As Integer In lstsBimestres
            dcPAram.Clear()
            dcPAram.Add("codAsignacionAula", coAsignacionAula)
            dcPAram.Add("codGrado", 0)
            dcPAram.Add("codAsignacionGrupo", 0)
            dcPAram.Add("codBimestre", codBim)
            dcPAram.Add("codAnio", codAnio)
            dcPAram.Add("codCurso", 0)
            dcPAram.Add("codTipoCurso", 3)
            dcPAram.Add("codAsignacionPadre", 0)
            dcPAram.Add("codAlumno", 0)
            '20100044
            Dim dst As New DataTable
            Dim nParam As String = "USP_LisEstructuraBimestreTipoCurso"
            dst = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dcPAram, nParam).Tables(0)
            Dim oBL_CrearEstructuraNotasOficiales As New BL_CrearEstructuraNotasOficiales
            Dim codigoId As Integer = 0
            If dst.Rows.Count = 0 Then
                Exit Function
            End If
            codigoId = oBL_CrearEstructuraNotasOficiales.insertarAsignacionGrupo(dst, codBim, 42, codAnio)
            lstIds.Add(codigoId)

        Next

        Return lstIds
    End Function

#End Region

#Region "calculo de notas finales  de primaria "

    Function F_crearNotasConducta(ByVal codGrado As Integer, ByVal codAnio As Integer) As List(Of Integer)
        Dim lstIsd As New List(Of Integer)
        Dim mtrBimestres As Integer() = {1, 2, 3, 4}
        Try


            Dim dcPAramII As New Dictionary(Of String, Object)
            Dim nombreSP As String = "USP_listarMatriz"

            For Each codBim As Integer In mtrBimestres

                dcPAramII.Clear()
                dcPAramII.Add("BM_CodigoBimestre", codBim)
                dcPAramII.Add("GD_CodigoGrado", codGrado)
                dcPAramII.Add("AC_CodigoAnioAcademico", codAnio)
                Dim dst As DataTable
                dst = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dcPAramII, nombreSP).Tables(0)

                If dst.Rows.Count = 0 Then
                    Exit Function
                End If
                Dim oBL_CrearEstructuraNotasOficiales As New BL_CrearEstructuraNotasOficiales
                Dim codigo As Integer = 0
                codigo = oBL_CrearEstructuraNotasOficiales.insertarAsignacionGrupoConductaPrimaria(dst, codAnio)
                lstIsd.Add(codigo)
            Next
            Return lstIsd
        Catch ex As Exception

        End Try
    End Function
#End Region
End Class
