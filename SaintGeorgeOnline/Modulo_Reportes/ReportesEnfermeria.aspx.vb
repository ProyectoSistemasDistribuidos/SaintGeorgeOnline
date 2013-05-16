Imports Microsoft.Office.Interop.Excel
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloReportes
Imports SaintGeorgeOnline_DataAccess.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices.Marshal
Imports ClosedXML.Excel
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas

''' <summary>
''' Modulo de Reportes de Enfermeria
''' </summary>
''' <remarks>
''' Código del Modulo:    1   
''' Código de la Opción:  7
''' </remarks>

Partial Class ModuloReportes_ReportesEnfermeria
    Inherits System.Web.UI.Page

    'updated 13/10/2011




    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Master.MostrarTitulo("Reportes de Enfermería")

            btnReporteExportar.Attributes.Add("onclick", "ShowMyModalPopup()")

            If Not Page.IsPostBack Then
                ''-----------------------------------
                OcultaRrPaneles()
                cargarListaReportes()
                cargarListaPresentacion()

                tbRep1_FechaInicio.Text = Today.AddMonths(-1)
                tbRep1_FechaFin.Text = Today

                tbRep2_FechaInicio.Text = Today.AddMonths(-1)
                tbRep2_FechaFin.Text = Today

                tbRep4_FechaInicio.Text = Today.AddMonths(-1)
                tbRep4_FechaFin.Text = Today

                tbRep6_FechaInicio.Text = "01/03/" & Year(Now.Date)
                tbRep6_FechaFin.Text = Today

                pnlReporte1.Visible = True
                pnlReporte2.Visible = False
                pnlReporte3.Visible = False
                pnlReporte4.Visible = False
                PnlReporte5.Visible = False
                pnlReporte6.Visible = False

                cargarComboTipoPersona_Rep1()
                cargarComboNivel_Rep1()
                cargarComboNivel_Rep5()
                limpiarCombos(ddlRep1_SubNivel, True, False)
                limpiarCombos(ddlRep1_Grado, True, False)
                limpiarCombos(ddlRep1_Aula, True, False)
                limpiarCombos(ddlRep1_Persona, True, False)

                cargarComboSede_Rep6()
                cargarComboNivel_Rep6()
                limpiarCombos(ddlRep6_SubNivel, True, False)
                limpiarCombos(ddlRep6_Grado, True, False)
                limpiarCombos(ddlRep6_Aula, True, False)
                cargarCombosNumeroPintar()
                ddlRep6_NumeroPintar.SelectedValue = 12

                ddlRep3_TipoPersona.SelectedValue = 1
                ddlRep3_TipoPersona.Enabled = False
                cargarComboAniosAcademicos()
                cargarComboAula()
                cargarComboAlumnos()

                cargarComboNivel_Rep3()
                limpiarCombos(ddlRep3_SubNivel, True, False)
                limpiarCombos(ddlRep3_Grado, True, False)
                limpiarCombos(ddlRep3_Aula, True, False)

                limpiarCombos(ddlRep5_SubNivel, True, False)
                limpiarCombos(ddlRep5_Grado, True, False)
                limpiarCombos(ddlRep5_Aula, True, False)
                limpiarCombos(ddlRep5_Persona, True, False)

                ' poner la fecha  de inicio al dia lunes 
                ''-----------------------------------
                SetearFechaDefault()
            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub lstReportes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarListaPresentacion()
            mostrarPanelParametros()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub lstPresentacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPresentacion.SelectedIndexChanged

        mostrarPanelParametros()

    End Sub
    Protected Sub btnReporteExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Exportar()
    End Sub

    Protected Sub ddlAula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarComboAlumnos()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Reportes 1"

#Region "Eventos"

    Protected Sub ddlRep1_TipoPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlRep1_Nivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_Nivel.SelectedValue <> 0 Then
                cargarComboSubNivel_Rep1()
                limpiarCombos(ddlRep1_Grado, True, False)
                limpiarCombos(ddlRep1_Aula, True, False)
                cargarComboPersona_Rep1()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlRep1_SubNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_SubNivel.SelectedValue <> 0 Then
                cargarComboGrado_Rep1()
                limpiarCombos(ddlRep1_Aula, True, False)
                cargarComboPersona_Rep1()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlRep1_Grado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_Grado.SelectedValue <> 0 Then
                cargarComboAulas_Rep1()
                cargarComboPersona_Rep1()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlRep1_Aula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_Aula.SelectedValue <> 0 Then
                cargarComboPersona_Rep1()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboNivel_Rep1()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep1_Nivel, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboSubNivel_Rep1()

        Dim int_CodigoNivel As Integer = ddlRep1_Nivel.SelectedValue
        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep1_SubNivel, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboGrado_Rep1()

        Dim int_CodigoSubNivel As Integer = ddlRep1_SubNivel.SelectedValue
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(int_CodigoSubNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep1_Grado, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboAulas_Rep1()

        Dim int_CodigoGrado As Integer = ddlRep1_Grado.SelectedValue
        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep1_Aula, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboPersona_Rep1()

        Dim int_CodigoPeriodo As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim int_CodigoNivel As Integer = ddlRep1_Nivel.SelectedValue
        Dim int_CodigoSubNivel As Integer = ddlRep1_SubNivel.SelectedValue
        Dim int_CodigoGrado As Integer = ddlRep1_Grado.SelectedValue
        Dim int_CodigoAula As Integer = ddlRep1_Aula.SelectedValue

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Alumnos.FUN_LIS_AlumnosPorNivelSubNivelGradoAula(int_CodigoPeriodo, int_CodigoNivel, int_CodigoSubNivel, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep1_Persona, ds_Lista, "CodigoPersona", "NombreCompleto", True, False)

    End Sub

#End Region

#Region "Presentacion 66"
#Region "Eventos"
    Protected Sub ddlRep6_TipoPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddlRep6_Nivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep6_Nivel.SelectedValue <> 0 Then
                cargarComboSubNivel_Rep6()
                limpiarCombos(ddlRep6_Grado, True, False)
                limpiarCombos(ddlRep6_Aula, True, False)

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlRep6_SubNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep6_SubNivel.SelectedValue <> 0 Then
                cargarComboGrado_Rep6()
                limpiarCombos(ddlRep6_Aula, True, False)

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlRep6_Grado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep6_Grado.SelectedValue <> 0 Then
                cargarComboAulas_Rep6()

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlRep6_Aula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep6_Aula.SelectedValue <> 0 Then

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboSede_Rep6()

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlRep6_Sede, ds_Lista, "Codigo", "NombreSede", True, False)

        Controles.llenarCombo(dpSede, ds_Lista, "Codigo", "NombreSede", True, False)
        Controles.llenarCombo(cmbSede2, ds_Lista, "Codigo", "NombreSede", True, False)


    End Sub
    'Private Sub cargarComboSede_Rep6_last()

    '    Dim obj_BL_SedesColegio As New bl_SedesColegio
    '    Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
    '    Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
    '    Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
    '    Controles.llenarCombo(cmbSedeLast, ds_Lista, "Codigo", "NombreSede", True, False)
    'End Sub

    Private Sub cargarComboNivel_Rep6()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep6_Nivel, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboSubNivel_Rep6()

        Dim int_CodigoNivel As Integer = ddlRep6_Nivel.SelectedValue
        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep6_SubNivel, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboGrado_Rep6()

        Dim int_CodigoSubNivel As Integer = ddlRep6_SubNivel.SelectedValue
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(int_CodigoSubNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep6_Grado, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub
    'Private Sub cargarComboGrado_Rep6Last()

    '    Dim int_CodigoSubNivel As Integer = cmbNivelLast.SelectedValue
    '    Dim obj_BL_Grados As New bl_Grados
    '    Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
    '    Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
    '    Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(int_CodigoSubNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
    '    Controles.llenarCombo(cmbGradoLast, ds_Lista, "Codigo", "Descripcion", True, False)
    'End Sub
    Private Sub cargarComboAulas_Rep6()

        Dim int_CodigoGrado As Integer = ddlRep6_Grado.SelectedValue
        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep6_Aula, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub
    'Private Sub cargarComboAulas_Rep6Last()

    '    Dim int_CodigoGrado As Integer = cmbGradoLast.SelectedValue
    '    Dim obj_BL_Aulas As New bl_Aulas
    '    Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
    '    Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
    '    Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
    '    Controles.llenarCombo(cmbAulaLast, ds_Lista, "Codigo", "Descripcion", True, False)
    'End Sub
    Private Sub cargarCombosNumeroPintar()

        Dim ds_Lista As DataSet
        Dim int_Inicio As Integer = 1
        Dim int_Fin As Integer = 50
        ds_Lista = Controles.ListaNumerica(int_Inicio, int_Fin)
        Controles.llenarCombo(ddlRep6_NumeroPintar, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub
    'Private Sub cargarCombosNumeroPintarLast()

    '    Dim ds_Lista As DataSet
    '    Dim int_Inicio As Integer = 1
    '    Dim int_Fin As Integer = 50
    '    ds_Lista = Controles.ListaNumerica(int_Inicio, int_Fin)
    '    Controles.llenarCombo(cmbMayorLast, ds_Lista, "Codigo", "Descripcion", False, False)

    'End Sub
#End Region
#End Region

#End Region

#Region "Reportes 6"

#Region "Eventos"

    Protected Sub ddlRep3_Nivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep3_Nivel.SelectedValue <> 0 Then
                cargarComboSubNivel_Rep3()
                limpiarCombos(ddlRep3_Grado, True, False)
                limpiarCombos(ddlRep3_Aula, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlRep3_SubNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep3_SubNivel.SelectedValue <> 0 Then
                cargarComboGrado_Rep3()
                limpiarCombos(ddlRep3_Aula, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlRep3_Grado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep3_Grado.SelectedValue <> 0 Then
                cargarComboAulas_Rep3()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboAniosAcademicos()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep3_PeriodoAcademico, ds_Lista, "Codigo", "Descripcion", False, False)
        ddlRep3_PeriodoAcademico.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
    End Sub

    Private Sub cargarComboNivel_Rep3()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep3_Nivel, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboSubNivel_Rep3()

        Dim int_CodigoNivel As Integer = ddlRep3_Nivel.SelectedValue
        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep3_SubNivel, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboGrado_Rep3()

        Dim int_CodigoSubNivel As Integer = ddlRep3_SubNivel.SelectedValue
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(int_CodigoSubNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep3_Grado, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboAulas_Rep3()

        Dim int_CodigoGrado As Integer = ddlRep3_Grado.SelectedValue
        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep3_Aula, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

#End Region

#End Region

#Region "Reportes 22"

#Region "Eventos"
    Protected Sub ddlRep5_PeriodoAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep5_Aula.SelectedValue <> 0 Then
                cargarComboPersona_Rep5()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub
    Protected Sub ddlRep5_Nivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep5_Nivel.SelectedValue <> 0 Then
                cargarComboSubNivel_Rep5()
                limpiarCombos(ddlRep5_Grado, True, False)
                limpiarCombos(ddlRep5_Aula, True, False)
                limpiarCombos(ddlRep5_Persona, True, False)
            Else
                limpiarCombos(ddlRep5_SubNivel, True, False)
                limpiarCombos(ddlRep5_Grado, True, False)
                limpiarCombos(ddlRep5_Aula, True, False)
                limpiarCombos(ddlRep5_Persona, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddlRep5_SubNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep5_SubNivel.SelectedValue <> 0 Then
                cargarComboGrado_Rep5()
                limpiarCombos(ddlRep5_Aula, True, False)
                limpiarCombos(ddlRep5_Persona, True, False)
            Else
                limpiarCombos(ddlRep5_Grado, True, False)
                limpiarCombos(ddlRep5_Aula, True, False)
                limpiarCombos(ddlRep5_Persona, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddlRep5_Grado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep5_Grado.SelectedValue <> 0 Then
                cargarComboAulas_Rep5()
                limpiarCombos(ddlRep5_Persona, True, False)
            Else
                limpiarCombos(ddlRep5_Aula, True, False)
                limpiarCombos(ddlRep5_Persona, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddlRep5_Aula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep5_Aula.SelectedValue <> 0 Then
                cargarComboPersona_Rep5()
            Else
                limpiarCombos(ddlRep5_Persona, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
#End Region

#Region "Metodos"
    Private Sub cargarComboTipoPersona_Rep5()
        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_TiposPersonas As New bl_TiposPersonas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_TiposPersonas.FUN_LIS_TiposPersonas(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep5_TipoPersona, ds_Lista, "Codigo", "Descripcion", True, False)
        ddlRep5_TipoPersona.SelectedValue = 1
        ddlRep5_TipoPersona.Enabled = False
    End Sub
    Private Sub cargarComboAniosAcademicos_Rep5()
        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep5_PeriodoAcademico, ds_Lista, "Codigo", "Descripcion", False, False)
        ddlRep5_PeriodoAcademico.SelectedValue = 2
    End Sub
    Private Sub cargarComboNivel_Rep5()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep5_Nivel, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub
    Private Sub cargarComboSubNivel_Rep5()
        Dim int_CodigoNivel As Integer = ddlRep5_Nivel.SelectedValue
        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep5_SubNivel, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub
    Private Sub cargarComboGrado_Rep5()
        Dim int_CodigoSubNivel As Integer = ddlRep5_SubNivel.SelectedValue
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(int_CodigoSubNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep5_Grado, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub
    Private Sub cargarComboAulas_Rep5()
        Dim int_CodigoGrado As Integer = ddlRep5_Grado.SelectedValue
        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep5_Aula, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub
    Private Sub cargarComboPersona_Rep5()
        Dim int_CodigoPeriodo As Integer = ddlRep5_PeriodoAcademico.SelectedValue
        Dim int_CodigoNivel As Integer = ddlRep5_Nivel.SelectedValue
        Dim int_CodigoSubNivel As Integer = ddlRep5_SubNivel.SelectedValue
        Dim int_CodigoGrado As Integer = ddlRep5_Grado.SelectedValue
        Dim int_CodigoAula As Integer = ddlRep5_Aula.SelectedValue

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Alumnos.FUN_LIS_AlumnosPorNivelSubNivelGradoAula(int_CodigoPeriodo, int_CodigoNivel, int_CodigoSubNivel, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep5_Persona, ds_Lista, "CodigoPersona", "NombreCompleto", True, False)

    End Sub
#End Region

#End Region

#Region "Metodos"

    Private Sub cargarListaReportes()

        Dim int_CodigoTipoReporte As Integer = 1 ' Reportes de Enfermería
        Dim obj_BL_Reportes As New bl_Reportes
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Reportes.FUN_LIS_Reportes(int_CodigoTipoReporte, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("ListaReportes") = ds_Lista

        lstReportes.DataSource = ds_Lista.Tables(0)
        lstReportes.DataTextField = "Nombre"
        lstReportes.DataValueField = "Codigo"
        lstReportes.DataBind()

        lstReportes.SelectedIndex = 0

    End Sub

    Private Sub cargarListaPresentacion()

        Dim dt As Data.DataTable = CType(ViewState("ListaReportes"), DataSet).Tables(1)
        Dim int_CodigoReporte As Integer = lstReportes.SelectedValue

        Dim dv As DataView = dt.DefaultView

        With dv
            .RowFilter = "1=1 and CodigoReporte = " & int_CodigoReporte
        End With

        lstPresentacion.DataSource = dt
        lstPresentacion.DataTextField = "Descripcion"
        lstPresentacion.DataValueField = "CodigoDetalle"
        lstPresentacion.DataBind()


        lstPresentacion.SelectedIndex = 0

    End Sub
    Private Sub OcultaRrPaneles()
        Try
            pnlReporte2.Visible = False
            pnlReporte3.Visible = False
            pnlReporte4.Visible = False
            PnlReporte5.Visible = False
            pnlReporte6.Visible = False
            ' pnlReporteLast.Visible = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub mostrarPanelParametros()
        pnlReporte1.Visible = False
        pnlReporte2.Visible = False
        pnlReporte3.Visible = False
        pnlReporte4.Visible = False
        PnlReporte5.Visible = False
        pnlReporte6.Visible = False
        'pnlReporteLast.Visible = False

        If lstReportes.SelectedValue = 1 Then ' Reporte : Atenciones Médicas
            If lstPresentacion.SelectedValue = 96 Then
                '  pnlReporteLast.Visible = True
            End If


            If lstPresentacion.SelectedValue = 66 Or lstPresentacion.SelectedValue = 96 Then
                pnlReporte1.Visible = False
                pnlReporte2.Visible = False
                pnlReporte3.Visible = False
                pnlReporte4.Visible = False
                PnlReporte5.Visible = False
                pnlReporte6.Visible = True

                If lstPresentacion.SelectedValue = 96 Then
                    SetearFechaDefault()
                ElseIf lstPresentacion.SelectedValue = 66 Then
                    SetearFechaDefaultReporteAcumuladosHora()
                End If

            Else
                pnlReporte1.Visible = True
                pnlReporte2.Visible = False
                pnlReporte3.Visible = False
                pnlReporte4.Visible = False
                PnlReporte5.Visible = False
                pnlReporte6.Visible = False

            End If

        ElseIf lstReportes.SelectedValue = 2 Or lstReportes.SelectedValue = 5 Or lstReportes.SelectedValue = 7 Then ' Reporte : Medicamentos y Diagnósticos

            pnlReporte1.Visible = False
            pnlReporte2.Visible = True
            pnlReporte3.Visible = False
            pnlReporte4.Visible = False
            PnlReporte5.Visible = False
            pnlReporte6.Visible = False

        ElseIf lstReportes.SelectedValue = 6 Then

            pnlReporte1.Visible = False
            pnlReporte2.Visible = False
            pnlReporte3.Visible = True
            pnlReporte4.Visible = False
            PnlReporte5.Visible = False
            pnlReporte6.Visible = False

        ElseIf lstReportes.SelectedValue = 3 Then

            pnlReporte1.Visible = False
            pnlReporte2.Visible = False
            pnlReporte3.Visible = False
            pnlReporte4.Visible = True
            PnlReporte5.Visible = False
            pnlReporte6.Visible = False

        ElseIf lstReportes.SelectedValue = 22 Or lstReportes.SelectedValue = 19 Or lstReportes.SelectedValue = 21 Then
            pnlReporte1.Visible = False
            pnlReporte2.Visible = False
            pnlReporte3.Visible = False
            pnlReporte4.Visible = False
            PnlReporte5.Visible = True
            pnlReporte6.Visible = False
            cargarComboTipoPersona_Rep5()
            cargarComboAniosAcademicos_Rep5()
        Else

            pnlReporte1.Visible = False
            pnlReporte2.Visible = False
            pnlReporte3.Visible = False
            pnlReporte4.Visible = False
            PnlReporte5.Visible = False
            pnlReporte6.Visible = False
        End If

    End Sub

    Private Sub limpiarCombos(ByVal combo As DropDownList, ByVal bool_Todos As Boolean, ByVal bool_Seleccione As Boolean)

        Controles.limpiarCombo(combo, bool_Todos, bool_Seleccione)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Aulas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     13/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAula()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        ViewState("ListaAulas") = ds_Lista.Tables(0)
        Controles.llenarCombo(ddlAula, ds_Lista, "Codigo", "DescAulaCompuesta2", True, False)

    End Sub

    Private Sub cargarComboAlumnos()

        Dim int_CodigoAula As Integer
        int_CodigoAula = ddlAula.SelectedValue

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Alumnos.FUN_LIS_AlumnosFichaAtencion(int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAlumno, ds_Lista, "CodigoAlumno", "NombreCompleto", False, True)

    End Sub

    Private Sub cargarComboTipoPersona_Rep1()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_TiposPersonas As New bl_TiposPersonas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TiposPersonas.FUN_LIS_TiposPersonas(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep1_TipoPersona, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlRep2_TipoPersona, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlRep3_TipoPersona, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlRep6_TipoPersona, ds_Lista, "Codigo", "Descripcion", True, False)
        'Controles.llenarCombo(cmbTipoLast, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub


    ''' <summary>
    ''' Exporta los datos del gridView en formato HTML
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     16/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: 17/02/2011
    ''' </remarks>
    ''' 
    Private Sub Exportar()

        Dim codSede2 As Integer = 0
        codSede2 = cmbSede2.SelectedValue

        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet
        Dim obj_BL_Enfermeria As New bl_Enfermeria

        Dim bool_Parametros As Boolean = False
        Dim bool_MostrarTotales As Boolean = False


        Dim int_TipoReporte As Integer = lstReportes.SelectedValue 'Tipo reporte
        Dim int_PresentacionReporte As Integer = lstPresentacion.SelectedValue 'Tipo reporte


        Dim str_TituloReporte As String = "" 'Titulo reporte
        Dim Arreglo_Parametros As New ArrayList 'Arreglo de parametros para la visualizacion en el reporte

        Dim reporte_html As String = "" 'Contenido del reporte
        Dim Arreglo_Datos As String() 'Arreglo de datos del reporte (cabecera y detalle)

        Dim dt As Data.DataTable = New Data.DataTable("ListaExportar")

        Dim int_CodigoPeriodoAcademico As Integer
        Dim int_TipoPaciente As Integer
        Dim dt_FechaRangoInicio As Date
        Dim dt_FechaRangoFin As Date
        Dim int_CodigoNivel As Integer
        Dim int_CodigoSubnivel As Integer
        Dim int_CodigoGrado As Integer
        Dim int_CodigoAula As Integer
        Dim str_CodigoAlumno As String
        Dim int_TipoFecha As Integer
        Dim int_CodigoPersona As Integer

        Dim codSede As Integer
        codSede = dpSede.SelectedValue
        If int_TipoReporte = 1 Then ' Reporte : Atenciones Médicas

            int_TipoPaciente = ddlRep1_TipoPersona.SelectedValue
            dt_FechaRangoInicio = tbRep1_FechaInicio.Text
            dt_FechaRangoFin = tbRep1_FechaFin.Text
            int_CodigoNivel = ddlRep1_Nivel.SelectedValue
            int_CodigoSubnivel = ddlRep1_SubNivel.SelectedValue
            int_CodigoGrado = ddlRep1_Grado.SelectedValue
            int_CodigoAula = ddlRep1_Aula.SelectedValue
            int_CodigoPersona = ddlRep1_Persona.SelectedValue

            If int_PresentacionReporte = 96 Then ' reporte enfermeria acumulado por semanas 
                Dim NombreArchivo1 As String = ""


                Dim fechaInicio As DateTime = Convert.ToDateTime(tbRep6_FechaInicio.Text)
                Dim fechaFinal As DateTime = Convert.ToDateTime(tbRep6_FechaFin.Text)


                Dim esValidoFecha As Boolean = False


                If Not fechaFinal.Year = fechaInicio.Year Then

                    ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>alert('seleccione un intervalo en el mismo año ');</script>", False)

                    'Sexy.info(res.d.mensaje)
                    'Sexy.alert(res.d.mensaje)


                    Exit Sub
                End If
                Dim downloadBytes1 As Byte()


                NombreArchivo1 = reporteEnfermeria(0, tbRep6_FechaInicio.Text, tbRep6_FechaFin.Text, ddlRep6_NumeroPintar.SelectedValue, ddlRep6_Nivel.SelectedValue, ddlRep6_SubNivel.SelectedValue, ddlRep6_Grado.SelectedValue, ddlRep6_Aula.SelectedValue, ddlRep6_Sede.SelectedValue, ddlRep6_TipoPersona.SelectedValue, 0)
                ' NombreArchivo = ExportarReporteAtencionesAcumuladoXHora(ds_Lista, str_TituloReporte, ddlRep6_NumeroPintar.SelectedItem.ToString, ddlRep6_TipoPersona.SelectedItem.ToString, ddlRep6_Sede.SelectedItem.ToString, ddlRep6_Grado.SelectedItem.ToString, tbRep6_FechaInicio.Text, tbRep6_FechaFin.Text)

                downloadBytes1 = File.ReadAllBytes(NombreArchivo1)

                'Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)



                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=ReporteEnfermeria" & DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "") & ".xlsx; size=" + downloadBytes1.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes1)
                Response.End()
            End If

            If int_PresentacionReporte = 1 Then ' Presentación : General

                str_TituloReporte = "Atenciones Médicas General"
                ds_Lista = obj_BL_Enfermeria.FUN_REP_AtencionesMedicasTotales(codSede, int_TipoPaciente, dt_FechaRangoInicio, dt_FechaRangoFin, _
                            int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, int_CodigoPersona, _
                            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "Periodo", "integer")
                dt = Datos.agregarColumna(dt, "Mes", "String")
                dt = Datos.agregarColumna(dt, "FechaAtencion", "String")
                dt = Datos.agregarColumna(dt, "Tipo de Paciente", "String")
                dt = Datos.agregarColumna(dt, "Nombres de Paciente", "String")
                dt = Datos.agregarColumna(dt, "Grado", "String")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Periodo") = dr.Item("Periodo")
                    auxDR.Item("Mes") = dr.Item("Mes")
                    auxDR.Item("FechaAtencion") = dr.Item("FechaAtencion")
                    auxDR.Item("Tipo de Paciente") = dr.Item("TipoPaciente")
                    auxDR.Item("Nombres de Paciente") = dr.Item("NombrePaciente")
                    auxDR.Item("Grado") = dr.Item("Grado")
                    dt.Rows.Add(auxDR)
                Next

            ElseIf int_PresentacionReporte = 2 Then ' Presentación : Detallada

                int_TipoPaciente = ddlRep1_TipoPersona.SelectedValue
                dt_FechaRangoInicio = tbRep1_FechaInicio.Text
                dt_FechaRangoFin = tbRep1_FechaFin.Text
                int_CodigoNivel = ddlRep1_Nivel.SelectedValue
                int_CodigoSubnivel = ddlRep1_SubNivel.SelectedValue
                int_CodigoGrado = ddlRep1_Grado.SelectedValue
                int_CodigoAula = ddlRep1_Aula.SelectedValue
                int_CodigoPersona = ddlRep1_Persona.SelectedValue

                str_TituloReporte = "Atenciones Médicas Detalladas"
                ds_Lista = obj_BL_Enfermeria.FUN_REP_AtencionesMedicasExpandida(int_TipoPaciente, dt_FechaRangoInicio, dt_FechaRangoFin, _
                            int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, int_CodigoPersona, _
                            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "#", "string")
                dt = Datos.agregarColumna(dt, "Fecha", "String")
                dt = Datos.agregarColumna(dt, "H. Ingreso", "String")
                dt = Datos.agregarColumna(dt, "H. Salida", "String")
                dt = Datos.agregarColumna(dt, "Tipo Paciente", "String")
                dt = Datos.agregarColumna(dt, "Nombre Paciente", "String")
                dt = Datos.agregarColumna(dt, "Año / Nivel / Grado / Aula", "String")
                dt = Datos.agregarColumna(dt, "Diagnóstico", "String")
                dt = Datos.agregarColumna(dt, "Medicamentos", "String")
                dt = Datos.agregarColumna(dt, "Destino", "String")
                dt = Datos.agregarColumna(dt, "Categoria", "String")

                dt = Datos.agregarColumna(dt, "Tipo Atencion", "String")

                dt = Datos.agregarColumna(dt, "Procedencia", "String")
                dt = Datos.agregarColumna(dt, "Tipo de procedencia", "String")
                dt = Datos.agregarColumna(dt, "Profesor", "String")
                dt = Datos.agregarColumna(dt, "Persona que envía", "String")


                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("#") = dr.Item("NumeroAtencion")
                    auxDR.Item("Fecha") = dr.Item("FechaAtencion")
                    auxDR.Item("H. Ingreso") = dr.Item("HoraIngreso")
                    auxDR.Item("H. Salida") = dr.Item("HoraSalida")
                    auxDR.Item("Tipo Paciente") = dr.Item("TipoPaciente")




                    auxDR.Item("Nombre Paciente") = dr.Item("NombreAlumno")
                    auxDR.Item("Año / Nivel / Grado / Aula") = dr.Item("NSnGA")
                    auxDR.Item("Diagnóstico") = dr.Item("Diagnosticos")
                    auxDR.Item("Medicamentos") = dr.Item("Medicamentos")
                    auxDR.Item("Destino") = dr.Item("Destino")
                    auxDR.Item("Categoria") = dr.Item("Categoria")

                    auxDR.Item("Tipo Atencion") = dr.Item("tipoAtencion")


                    auxDR.Item("Procedencia") = dr.Item("NombreProcedencia")
                    auxDR.Item("Tipo de procedencia") = dr.Item("TipoProcedencia")
                    auxDR.Item("Profesor") = dr.Item("Profesor")
                    auxDR.Item("Persona que envía") = dr.Item("PersonaEnvia")
                    dt.Rows.Add(auxDR)
                Next
            ElseIf int_PresentacionReporte = 66 Then ' Presentación : Acumulado por hora
                Dim int_NumeroPintar As Integer
                Dim int_CodigoSede As Integer

                'int_CodigoSede=
                int_TipoPaciente = ddlRep6_TipoPersona.SelectedValue
                dt_FechaRangoInicio = tbRep6_FechaInicio.Text
                dt_FechaRangoFin = tbRep6_FechaFin.Text
                int_CodigoNivel = ddlRep6_Nivel.SelectedValue
                int_CodigoSubnivel = ddlRep6_SubNivel.SelectedValue
                int_CodigoGrado = ddlRep6_Grado.SelectedValue
                int_CodigoAula = ddlRep6_Aula.SelectedValue
                int_CodigoSede = ddlRep6_Sede.SelectedValue
                int_NumeroPintar = ddlRep6_NumeroPintar.SelectedValue

                str_TituloReporte = "Atenciones Médicas Detalladas"
                ds_Lista = obj_BL_Enfermeria.FUN_REP_AtencionesAcumuladoXHora(int_TipoPaciente, dt_FechaRangoInicio, dt_FechaRangoFin, _
            int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, int_CodigoSede, int_NumeroPintar, _
            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                dt = ds_Lista.Tables(0)
            End If

        ElseIf int_TipoReporte = 2 Then ' Reporte : Medicamentos consumidos por fechas

            int_TipoPaciente = ddlRep2_TipoPersona.SelectedValue
            dt_FechaRangoInicio = tbRep2_FechaInicio.Text
            dt_FechaRangoFin = tbRep2_FechaFin.Text

            If int_PresentacionReporte = 3 Then ' Presentación : General

                str_TituloReporte = "Medicamentos Consumidos"
                ds_Lista = obj_BL_Enfermeria.FUN_REP_MedicamentosPorFechas(int_TipoPaciente, dt_FechaRangoInicio, dt_FechaRangoFin, _
                            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "#", "string")
                dt = Datos.agregarColumna(dt, "Tipo de Paciente", "String")
                dt = Datos.agregarColumna(dt, "Medicamento", "String")
                dt = Datos.agregarColumna(dt, "Presentación x Unidad", "String")
                dt = Datos.agregarColumna(dt, "Presentación", "String")
                dt = Datos.agregarColumna(dt, "Unidad", "String")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("#") = dr.Item("CodigoMedicamento")
                    auxDR.Item("Tipo de Paciente") = dr.Item("TipoPaciente")
                    auxDR.Item("Medicamento") = dr.Item("Medicamento")
                    auxDR.Item("Presentación x Unidad") = dr.Item("PresentacionUnidad")
                    auxDR.Item("Presentación") = dr.Item("Presentacion")
                    auxDR.Item("Unidad") = dr.Item("Unidad")
                    dt.Rows.Add(auxDR)
                Next

            End If

        ElseIf int_TipoReporte = 3 Then ' Reporte : Historial de atenciones

            If int_PresentacionReporte = 24 Then ' Resumen de Becas

                If ddlAlumno.SelectedValue = 0 Then
                    Me.Master.MostrarMensajeAlert("Debe seleccionar el alumno.")
                    Exit Sub
                End If

                int_CodigoAula = ddlAula.SelectedValue
                str_CodigoAlumno = ddlAlumno.SelectedValue
                If rbTipoFecha.SelectedValue = 0 Then 'No
                    int_TipoFecha = 0
                ElseIf rbTipoFecha.SelectedValue = 1 Then 'Si
                    int_TipoFecha = 1
                End If

                dt_FechaRangoInicio = tbRep4_FechaInicio.Text
                dt_FechaRangoFin = tbRep4_FechaFin.Text

                str_TituloReporte = "MiReporte" '
                ds_Lista = obj_BL_Enfermeria.FUN_REP_AteMedHistorialClinico(int_CodigoAula, str_CodigoAlumno, int_TipoFecha, dt_FechaRangoInicio, dt_FechaRangoFin, _
                   int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                dt = ds_Lista.Tables(3)

            End If
        ElseIf int_TipoReporte = 4 Then ' Reporte : Ficha Médica

        ElseIf int_TipoReporte = 5 Then ' Reporte : Diagnosticosdcit

            int_TipoPaciente = ddlRep2_TipoPersona.SelectedValue
            dt_FechaRangoInicio = tbRep2_FechaInicio.Text
            dt_FechaRangoFin = tbRep2_FechaFin.Text

            If int_PresentacionReporte = 6 Then ' Presentación : General

                str_TituloReporte = "Diagnósticos"
                ds_Lista = obj_BL_Enfermeria.FUN_REP_DiagnosticoPorFechas(int_TipoPaciente, dt_FechaRangoInicio, dt_FechaRangoFin, _
                            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "#", "string")
                dt = Datos.agregarColumna(dt, "Tipo de Paciente", "String")
                dt = Datos.agregarColumna(dt, "Diagnóstico", "String")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("#") = dr.Item("CodigoDiagnostico")
                    auxDR.Item("Tipo de Paciente") = dr.Item("TipoPaciente")
                    auxDR.Item("Diagnóstico") = dr.Item("Diagnostico")
                    dt.Rows.Add(auxDR)
                Next

            End If

        ElseIf int_TipoReporte = 6 Then ' Reporte : Alergía

            'int_TipoPaciente = ddlRep3_TipoPersona.SelectedValue
            int_CodigoPeriodoAcademico = ddlRep3_PeriodoAcademico.SelectedValue
            int_CodigoNivel = ddlRep3_Nivel.SelectedValue
            int_CodigoSubnivel = ddlRep3_SubNivel.SelectedValue
            int_CodigoGrado = ddlRep3_Grado.SelectedValue
            int_CodigoAula = ddlRep3_Aula.SelectedValue

            If int_PresentacionReporte = 7 Then ' Presentación : General

                str_TituloReporte = "Alergías de los Alumnos"
                ds_Lista = obj_BL_Enfermeria.FUN_REP_Alergias(int_CodigoPeriodoAcademico, _
                            int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, _
                            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "Periodo", "integer")
                dt = Datos.agregarColumna(dt, "Nivel", "String")
                dt = Datos.agregarColumna(dt, "Grado", "String")
                dt = Datos.agregarColumna(dt, "Aula", "String")
                'dt = Datos.agregarColumna(dt, "Código", "String")
                dt = Datos.agregarColumna(dt, "Nombres Completo", "String")
                dt = Datos.agregarColumna(dt, "Alergias", "String")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Periodo") = dr.Item("Periodo")
                    auxDR.Item("Nivel") = dr.Item("Nivel")
                    auxDR.Item("Grado") = dr.Item("Grado")
                    auxDR.Item("Aula") = dr.Item("Aula")
                    'auxDR.Item("Código") = dr.Item("CodigoAlumno")
                    auxDR.Item("Nombres Completo") = dr.Item("NombreAlumno")
                    auxDR.Item("Alergias") = dr.Item("Alergia")
                    dt.Rows.Add(auxDR)
                Next

            End If

        ElseIf int_TipoReporte = 7 Then ' Reporte : Procedimientos realizados por fechas

            int_TipoPaciente = ddlRep2_TipoPersona.SelectedValue
            dt_FechaRangoInicio = tbRep2_FechaInicio.Text
            dt_FechaRangoFin = tbRep2_FechaFin.Text

            If int_PresentacionReporte = 8 Then ' Presentación : General

                str_TituloReporte = "Procedimientos Realizados"
                ds_Lista = obj_BL_Enfermeria.FUN_REP_ProcedimientosRealizadosPorFechas(codSede2, int_TipoPaciente, dt_FechaRangoInicio, dt_FechaRangoFin, _
                            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "#", "string")
                dt = Datos.agregarColumna(dt, "Tipo de Paciente", "String")
                dt = Datos.agregarColumna(dt, "Procedimiento", "String")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("#") = dr.Item("CodigoProcedimiento")
                    auxDR.Item("Tipo de Paciente") = dr.Item("TipoPaciente")
                    auxDR.Item("Procedimiento") = dr.Item("Procedimiento")
                    dt.Rows.Add(auxDR)
                Next

            End If

        ElseIf int_TipoReporte = 22 Or int_TipoReporte = 19 Or int_TipoReporte = 21 Then ' Reporte : Telefonos de emergencia del alumno

            'int_TipoPaciente = ddlRep3_TipoPersona.SelectedValue
            int_CodigoPeriodoAcademico = ddlRep5_PeriodoAcademico.SelectedValue
            int_CodigoNivel = ddlRep5_Nivel.SelectedValue
            int_CodigoSubnivel = ddlRep5_SubNivel.SelectedValue
            int_CodigoGrado = ddlRep5_Grado.SelectedValue
            int_CodigoAula = ddlRep5_Aula.SelectedValue
            int_CodigoPersona = ddlRep5_Persona.SelectedValue

            If int_PresentacionReporte = 36 Then ' Presentación : General

                'str_TituloReporte = "Emergencia"
                'ds_Lista = obj_BL_Enfermeria.FUN_REP_Emergencia(int_CodigoPeriodoAcademico, _
                '            int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, _
                '            int_CodigoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                'dt = ds_Lista.Tables(0)
                crearReporteTelefonos(int_CodigoPeriodoAcademico, int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, int_CodigoPersona)

            End If


            If int_PresentacionReporte = 37 Then ' Presentación : General

                str_TituloReporte = "Seguro"
                ds_Lista = obj_BL_Enfermeria.FUN_REP_Seguro(int_CodigoPeriodoAcademico, _
                            int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, _
                            int_CodigoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                dt = ds_Lista.Tables(0)
            End If

            If int_PresentacionReporte = 38 Then ' Presentación : General

                str_TituloReporte = "Seguro"
                ds_Lista = obj_BL_Enfermeria.FUN_REP_EnfermedadAlumno(int_CodigoPeriodoAcademico, _
                            int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, _
                            int_CodigoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                dt = ds_Lista.Tables(0)
            End If
        End If

        'LLenado de reporte
        Dim NombreArchivo As String = ""
        Dim RutaMadre As String = ""
        Dim downloadBytes As Byte()

        If Not Year(tbRep1_FechaInicio.Text) = Year(tbRep1_FechaFin.Text) Then
            Me.Master.MostrarMensajeAlert("El rango que ha seleccionado deber ser del mismo  periodod academico")

            Exit Sub
        End If

        If Not dt.Rows.Count > 0 Then
            Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
            'tbRep1_FechaInicio.Text, tbRep1_FechaFin.Text, oExcel)
            Exit Sub


        End If

        If int_TipoReporte = 1 Then ' Reporte : Atenciones Médicas General

            If int_PresentacionReporte = 1 Then ' Presentación : General
                NombreArchivo = ExportarReporteDinamicoAtencionesMédicasGeneral(dt, str_TituloReporte)
            ElseIf int_PresentacionReporte = 2 Then ' Presentación : Detallada
                If Not Year(tbRep1_FechaInicio.Text) = Year(tbRep1_FechaFin.Text) Then
                    Me.Master.MostrarMensajeAlert("El rango que ha seleccionado deber ser del mismo  periodod academico")

                    Exit Sub
                End If

                NombreArchivo = ExportarReporteAtencionesMédicasExpandida(dt, str_TituloReporte)


            ElseIf int_PresentacionReporte = 66 Then ' Presentación : Detallada
                NombreArchivo = ExportarReporteAtencionesAcumuladoXHora(ds_Lista, str_TituloReporte, ddlRep6_NumeroPintar.SelectedItem.ToString, ddlRep6_TipoPersona.SelectedItem.ToString, ddlRep6_Sede.SelectedItem.ToString, ddlRep6_Grado.SelectedItem.ToString, tbRep6_FechaInicio.Text, tbRep6_FechaFin.Text)


            End If


        ElseIf int_TipoReporte = 2 Then ' Reporte : Medicamentos consumidos por fechas

            If int_PresentacionReporte = 3 Then ' Presentación : General
                NombreArchivo = ExportarReporteDinamicoMedicamentosConsumidos(dt, str_TituloReporte)
            End If

        ElseIf int_TipoReporte = 3 Then ' Reporte :Historial atenciones
            If int_PresentacionReporte = 24 Then ' Presentación : General

                NombreArchivo = ExportarReporteHistorialAtenciones_Html(ds_Lista, str_TituloReporte)
                Session("Exportaciones_RepFichaAtencionHistorialClinicohtml") = NombreArchivo
                ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionFichaMedica_html();</script>", False)

            End If
        ElseIf int_TipoReporte = 4 Then ' Reporte : Ficha Médica

        ElseIf int_TipoReporte = 5 Then ' Reporte : Diagnosticos

            If int_PresentacionReporte = 6 Then ' Presentación : General
                NombreArchivo = ExportarReporteDinamicoDiagnosticos(dt, str_TituloReporte)
            End If

        ElseIf int_TipoReporte = 6 Then ' Reporte : Alergias

            If int_PresentacionReporte = 7 Then ' Presentación : General
                NombreArchivo = ExportarReporteAlergias(dt, str_TituloReporte)
            End If

        ElseIf int_TipoReporte = 7 Then ' Reporte : Procedimientos realizados 

            If int_PresentacionReporte = 8 Then ' Presentación : General
                NombreArchivo = ExportarReporteDinamicoProcedimientos(dt, str_TituloReporte)
            End If
        ElseIf int_TipoReporte = 22 Then ' Reporte : Procedimientos realizados 

            If int_PresentacionReporte = 36 Then ' Presentación : General
                NombreArchivo = ExportarReportePorEmergencia(dt, str_TituloReporte)
            End If
        ElseIf int_TipoReporte = 19 Then ' Reporte : Procedimientos realizados 

            If int_PresentacionReporte = 37 Then ' Presentación : General
                NombreArchivo = ExportarReportePorSeguro(dt, str_TituloReporte)
            End If
        ElseIf int_TipoReporte = 21 Then ' Reporte : Procedimientos realizados 

            If int_PresentacionReporte = 38 Then ' Presentación : General
                NombreArchivo = ExportarReportePorEnfermedadAlumno(dt, str_TituloReporte)
            End If
        End If

        If int_TipoReporte = 3 Then

        Else
            NombreArchivo = NombreArchivo & ".xls"

            RutaMadre = Server.MapPath(".")
            RutaMadre = RutaMadre.Replace("\Modulo_Reportes", "\Reportes\")

            downloadBytes = File.ReadAllBytes(RutaMadre & NombreArchivo)

            ' Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()
        End If



    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     09/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
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
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     09/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

#End Region

#Region "Exportacion Reportes"

    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    Private Shared Function GetNewName() As String
        Dim sName As String = Convert.ToString(DateTime.Now.Ticks)
        Return sName
    End Function

    'Reporte Codigo : 1 - 1
    Public Function ExportarReporteDinamicoAtencionesMédicasGeneral(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim objTablaDinamica As Microsoft.Office.Interop.Excel.PivotTable
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteEnfermeria1").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteAtencionesMédicasGeneral(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinámico"
        oCells = oSheet.Cells


        Dim sqlCombo
        Dim concat
        Dim nombreSede As String = ""

        Dim index As Integer = 0
        If dpSede.SelectedValue = 0 Then
            sqlCombo = From it In dpSede.Items Where CType(it, ListItem).Value <> 0 Select New With {.nombre = CType(it, ListItem).Text.ToString()}



            For Each nombre In sqlCombo

                index += 1
                nombreSede &= nombre.nombre & IIf(index = 1, " - ", " ")


            Next

        Else
            nombreSede = dpSede.SelectedItem.Text.ToString()
        End If



        'Pintado de Título
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Reporte de Atenciones Médicas General de : " & nombreSede
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 2), oCells(3, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        Dim int_cont As Integer = 0
        Dim str_DescTipo As String = ""

        Dim dv_Grado As DataView
        dv_Grado = dtReporte.DefaultView

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        'objTablaDinamica.PivotCache.SourceData = "Atenciones Médicas General!F5C2:F" & fila & "C6"
        objTablaDinamica.PivotCache.SourceData = "Atenciones Médicas General!F5C2:F" & fila & "C7"
        objTablaDinamica.PivotCache.Refresh()

        oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        While int_cont <= dtReporte.Rows.Count - 1
            str_DescTipo = dtReporte.Rows(int_cont).Item("Grado")
            oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescTipo).ShowDetail = False
            int_cont = int_cont + 1
        End While

        oSheet.SaveAs(sFile)

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function



    Private Shared Function LlenarPlantillaReporteAtencionesMédicasGeneral( _
        ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(2, 3), oCells(2, 5))
            '.Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Reporte de Atenciones Médicas General"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 3), oCells(3, 5))
            '.Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .Font.Color = RGB(0, 0, 0)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_columnas <= dtReporte.Columns.Count - 1
            While cont_filas <= dtReporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
                cont_filas += 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        fila = fila + dtReporte.Rows.Count
        str_Fila = (fila - 1).ToString

        oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)))
        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function

    'Reporte Codigo : 1 - 2
    Public Function ExportarReporteAtencionesMédicasExpandida(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel").ToString()

        oExcel.Visible = False
        oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        oSheet.Activate()

        LlenarPlantillaReporteAtencionesMédicasExpandida(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        F_reporteEnfermeria(tbRep1_FechaInicio.Text, tbRep1_FechaFin.Text, oExcel)



        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function


    Private Sub LlenarPlantillaReporteAtencionesMédicasExpandida( _
        ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String)

        Dim fila As Integer = 8
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        'Pintado de cabecera
        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName
            With oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas))
                .RowHeight = 21
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .Font.Color = RGB(0, 0, 0)
                .VerticalAlignment = 2
                .HorizontalAlignment = 3
            End With
            cont_columnas = cont_columnas + 1
        End While

        'Pintado de detalle
        cont_columnas = 0
        fila = 9


        While cont_columnas <= dtReporte.Columns.Count - 1
            While cont_filas <= dtReporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
                With oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas))
                    '.WrapText = True
                    .Interior.Color() = RGB(242, 242, 242)
                    If cont_columnas = 0 Then
                        .ColumnWidth = 11
                        .HorizontalAlignment = 3 'Center
                    ElseIf cont_columnas = 1 Or cont_columnas = 2 Or cont_columnas = 3 Then
                        .ColumnWidth = 12
                        .HorizontalAlignment = 3 'Center
                    ElseIf cont_columnas = 4 Then
                        .ColumnWidth = 14
                        .HorizontalAlignment = 2 'Center
                    ElseIf cont_columnas = 5 Then
                        .ColumnWidth = 45
                        .HorizontalAlignment = 2 'Center
                    ElseIf cont_columnas = 6 Or cont_columnas = 7 Or cont_columnas = 8 Or cont_columnas = 9 Then
                        .ColumnWidth = 30
                        If cont_filas = dtReporte.Rows.Count - 1 Then
                            .EntireColumn.AutoFit()
                        End If
                        .HorizontalAlignment = 2 'Center
                    End If
                    'If cont_filas = dtReporte.Rows.Count - 1 Then
                    '    .EntireColumn.AutoFit()
                    'End If
                End With
                cont_filas = cont_filas + 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While
        ' tbRep1_FechaInicio.Text, tbRep1_FechaFin.Text, oExcel)

        '& "  desde " & tbRep1_FechaInicio.Text & " Hasta: " & tbRep1_FechaFin.Text


        oCells(3, 5) = "Relación de " & str_NombreEntidadReporte

        With oCells(3, 5)
            .Font.Size = 8
        End With


        oCells(4, 5) = "Fecha del reporte:  " & Date.Now.Day.ToString.PadLeft(2, "0") & "/ " & Date.Now.Month.ToString.PadLeft(2, "0") & "/ " & Date.Now.Year.ToString

        With oCells(4, 5)
            .Font.Size = 8
        End With

        oCells(6, 5) = "GRADO :  " & ddlRep1_Grado.SelectedValue.Replace("0", "") & " " & ddlRep1_Grado.SelectedItem.Text.Replace("-", "") & " ( Rango de atencion de  " & tbRep1_FechaInicio.Text & " hasta " & tbRep1_FechaFin.Text & " )"

        With oCells(6, 5)
            .Font.Size = 8
        End With
        oCells.Range("E3:E3").Font.Size = 25

        'oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").Merge()
        'oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").HorizontalAlignment = 3 'HorizontalAlign.Right

        'oExcel.Range(oCells(8, 4), oCells(fila + dtReporte.Rows.Count - 1, columna + dtReporte.Columns.Count - 1)).EntireColumn.AutoFit()
        oExcel.ActiveWindow.Zoom = 75
        'cuadradoCompleto(oExcel, oExcel.Range(oCells(8, 4), oCells(21, 13)))


        cuadradoCompleto(oExcel, oExcel.Range(oCells(8, 4), oCells(fila + IIf(dtReporte.Rows.Count = 0, 1, dtReporte.Rows.Count - 1), columna + dtReporte.Columns.Count - 1)))

    End Sub
    'Reporte Codigo : 1 - 3


#Region "llenar la segunda hoja"
    Public Sub F_reporteEnfermeria(ByVal fechaInicio As String, ByVal fechaFin As String, _
                                        ByVal oExcel As Microsoft.Office.Interop.Excel.Application)
        Try
            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current



            Dim dc As New Dictionary(Of String, Object)
            dc("fechaInicio") = fechaInicio
            dc("fechaFin") = fechaFin
            dc("codSede") = dpSede.SelectedValue
            dc("codTipo") = ddlRep1_TipoPersona.SelectedValue
            dc("codNivel") = ddlRep1_Nivel.SelectedValue
            dc("codSubNivel") = ddlRep1_SubNivel.SelectedValue

            dc("codGrado") = ddlRep1_Grado.SelectedValue
            dc("codAula") = ddlRep1_Aula.SelectedValue
            dc("codPersona") = ddlRep1_Persona.SelectedValue

            Dim dtEnfermeria As New System.Data.DataTable
            'Dim nParam As String = "USP_LisConsolidadoEnfermeria"
            Dim nParam As String = "USP_LisConsolidadoEnfermeria_nuevo"
            'USP_LisConsolidadoEnfermeria_nuevo
            dtEnfermeria = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            '            TP_CodigoTipoPersona	TP_Descripcion	TP_Estado
            '1:          Alumnos(1)
            '2:          Trabajadores(1)
            '3:          Familiares(1)
            '4:          Otros(1)
            '5:          Proveedores(1)
            '6:          Clientes(1)


            Dim nombreTipoReporte As String = ""
            If ddlRep1_TipoPersona.SelectedValue = 1 Then
                nombreTipoReporte = " Trabajadores "
            ElseIf ddlRep1_TipoPersona.SelectedValue = 2 Then
                nombreTipoReporte = " Trabajadores "
            ElseIf ddlRep1_TipoPersona.SelectedValue = 3 Then
                nombreTipoReporte = " Trabajadores "
            Else
                nombreTipoReporte = " Todos "
            End If

            ''--------------------------------------------------------------------------------''
            'agrupar alumnos por grado  aula 


            Dim grupoGradoAulaAlumnos = _
            From grados In dtEnfermeria.AsEnumerable() Group grados By _
                                        codGrado = grados("codGrado"), nombreGrado = grados("nombreGrado") Into grados = Group _
                                Select New With { _
                                    .nombreGrado = nombreGrado, _
                                    .aulas = (From aulas In grados.AsEnumerable() Group aulas By codAula = aulas("codAula"), _
                                              nombreAula = aulas("nombreAula") Into GrpAulas = Group _
                                                      Select New With { _
                                                            .nombreAula = nombreAula, _
                                                            .nombreGradoPa = nombreGrado, _
                                                            .Alumnos = (From alumnos In GrpAulas.AsEnumerable() Group alumnos By codALumno = alumnos("codAlumno"), _
                                                               tipoPersona = alumnos("tipoPersona"), nombreAlumno = alumnos("nombreALumno") Into grpAlumnos = Group _
                                                                                Select New With {.tipoPersona = tipoPersona, .nombreAlumnos = nombreAlumno, .codAlmuno = codALumno, _
                                                                                         .cantidad = grpAlumnos.Count})})}


            Dim grupoCategoria = From cat In dtEnfermeria.AsEnumerable() Group cat By categoria = cat("categoria") Into categorias = Group _
                                 Select New With {.categoria = categoria, .cantidad = (categorias.Count)}


            Dim grupoTipoAtencion = From tip In dtEnfermeria.AsEnumerable() Group tip By tipo = tip("tipoAtencion") Into Detalle = Group _
                                    Select New With {.tipo = tipo, .cantidad = Detalle.Count}
            Dim grupoClase = From curso In dtEnfermeria.AsEnumerable() Group curso By nombreCurso = curso("nombreCurso") Into detalle = Group _
                            Select New With {.curso = nombreCurso, .cantidad = detalle.Count}


            Dim grupoDiagnostico = From diag In dtEnfermeria.AsEnumerable() Group diag By nombreDiag = diag("nombreDiag") Into detalle = Group _
                                   Select New With {.nombreDiagnostico = nombreDiag, .cantidad = detalle.Count}




            'Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
            Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet

            'Dim sFile As String, sTemplate As String
            'Dim nombreRep As String
            'Dim fila As String = ""
            ' ''--------------------------------------------------
            'Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("reporteCargoEntrega")
            'Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            'Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            'File.Copy(rutaPlantillas, rutaREpositorioTemporales)





            'oSheets = oBook.Worksheets
            'oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
            'oSheet.Name = "Consolidado de Cantidades"
            'oSheet.Activate()
            'oExcel.Visible = False
            'oExcel.DisplayAlerts = False





            oBooks = oExcel.Workbooks


            oBook = oBooks.Item(1)
            oSheets = oBook.Worksheets
            oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "Consolidado de Cantidades"
            oSheet.Activate()
            oExcel.Visible = False
            oExcel.DisplayAlerts = False



            ''----------------------------------------------------------------------------------------------------
            '' pintado de alunmos  por grado  y aula  desde la fila  14 
            '' incializacion  de  variables
            Dim filaInicial As Integer = 3 ''fila inicial 

            '   filaInicial += 6
            Dim totalAtencionesPorAula As Integer = 0
            ''----------------------------------------------------------------------------------------------------


            '' pintado de la cabezera 

            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filaInicial, 6), Microsoft.Office.Interop.Excel.Range))
                .Merge(True)
                .Value = "Consolidado de Cantidades"
                .Font.Bold = True
                .Font.Size = 16
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            End With
            filaInicial += 1


            Dim amPm As String = ""
            amPm = IIf(Date.Now.Hour > 12, "Pm", "Am")
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filaInicial, 6), Microsoft.Office.Interop.Excel.Range))
                .Merge(True)
                .Value = "Fecha del reporte: " _
                & Date.Now.Day.ToString.PadLeft(2, "0") _
                & "/" & Date.Now.Month.ToString.PadLeft(2, "0") & "/" _
                & Date.Now.Year.ToString & " " _
                & Date.Now.Hour.ToString.PadLeft(2, "0") & ":" _
                & Date.Now.Minute.ToString.PadLeft(2, "0") & ":" _
                & Date.Now.Second.ToString.PadLeft(2, "0") & " " & amPm
                .Font.Bold = True
                .Font.Size = 9
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            End With
            filaInicial += 2


            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicial, 1), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Microsoft.Office.Interop.Excel.Range))
                .Merge(True)
                .Value = "Rango de Fecha de atención de : " & fechaInicio & " al " & fechaFin
                .Font.Size = 9
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
            End With
            filaInicial += 1
            filaInicial += 1
            Dim filaTemp As Integer = filaInicial

            filaInicial += 4


            '
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicial, 1), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filaInicial, 4), Microsoft.Office.Interop.Excel.Range))
                .Merge(True)
                .Value = "NUMERO DE ATENCIONES POR " & nombreTipoReporte
                .Font.Size = 16
                .Interior.Color() = RGB(255, 0, 0)
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
            End With


            ''---------------------------------------------------------------''
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicial, 6), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filaInicial, 7), Microsoft.Office.Interop.Excel.Range))
                .Merge(True)
                .Value = "CATEGORIA"
                .Font.Size = 16
                .Interior.Color() = RGB(255, 0, 0)
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                .Font.Bold = True
            End With
            ''---------------------------------------------------------------'

            filaInicial += 2
            Dim filasCount As Integer = filaInicial ''pocicion de filas para los demas cuadros  

            With CType(oExcel.ActiveSheet.Cells(filaInicial, 1), Microsoft.Office.Interop.Excel.Range)
                .Value = "Codigo"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            '-------------------------------------------------------
            With CType(oExcel.ActiveSheet.Cells(filaInicial, 2), Microsoft.Office.Interop.Excel.Range)
                .Value = "Grado/Aula"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            '-------------------------------------------------------
            With CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Microsoft.Office.Interop.Excel.Range)
                .Value = "ALUMNOS"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            '-------------------------------------------------------

            With CType(oExcel.ActiveSheet.Cells(filaInicial, 4), Microsoft.Office.Interop.Excel.Range)
                .Value = "ATENCIONES"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With

            '.Interior.Color() = RGB(149, 179, 215)
            '-------------------------------------------------------
            filaInicial += 1


            ''-----------------------------------------------------------------------------'
            '' pintar el cuadro de categoria                                               '
            ''-----------------------------------------------------------------------------'
            Dim filaInicioCategoria As Integer = filasCount
            With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range)
                .Value = "CATEGORIA"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range)
                .Value = "CANTIDAD"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With


            filasCount += 1
            For Each categorias In grupoCategoria
                With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range)
                    .NumberFormat = "@"
                    .Value = categorias.categoria
                    .Font.Size = 9
                End With
                With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range)
                    .NumberFormat = "@"
                    .Value = categorias.cantidad
                    .Font.Size = 9
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With
                filasCount += 1
            Next
            '' poner borde al cuadro de categoria 
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicioCategoria, 6), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filasCount - 1, 7), Microsoft.Office.Interop.Excel.Range))
                .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            End With



            filasCount += 2


            ''-----------------------------------------------------------------------------'

            ''-----------------------------------------------------------------------------'
            '' pintar el cuadro de tipo de atencion                                        '
            ''-----------------------------------------------------------------------------'
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range))
                .Merge(True)
                .Value = "TIPO DE ATENCION"
                .Font.Size = 16
                .Interior.Color() = RGB(255, 0, 0)
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                .Font.Bold = True
            End With

            filasCount += 2

            filaInicioCategoria = filasCount
            With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range)
                .Value = "ATENCION"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range)
                .Value = "CANTIDAD"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            filasCount += 1

            For Each ogrupoTipoAtencion In grupoTipoAtencion
                With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range)
                    .NumberFormat = "@"
                    .Value = ogrupoTipoAtencion.tipo
                    .Font.Size = 9
                End With
                With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range)
                    .NumberFormat = "@"
                    .Value = ogrupoTipoAtencion.cantidad
                    .Font.Size = 9
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With
                filasCount += 1
            Next
            '' poner borde del cuadro de tipo de atencion 
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicioCategoria, 6), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filasCount - 1, 7), Microsoft.Office.Interop.Excel.Range))
                .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            End With


            filasCount += 2
            ''-----------------------------------------------------------------------------'




            ''-----------------------------------------------------------------------------'
            '' pintar el cuadro de clases                                      '
            ''-----------------------------------------------------------------------------'
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range))
                .Merge(True)
                .Value = "CLASES"
                .Font.Size = 16
                .Interior.Color() = RGB(255, 0, 0)
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                .Font.Bold = True
            End With

            filasCount += 2

            filaInicioCategoria = filasCount
            With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range)
                .Value = "CLASES"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range)
                .Value = "CANTIDAD"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            filasCount += 1

            For Each ogrupoClase In grupoClase
                With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range)
                    .NumberFormat = "@"
                    .Value = ogrupoClase.curso
                    .Font.Size = 9
                End With
                With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range)
                    .NumberFormat = "@"
                    .Value = ogrupoClase.cantidad
                    .Font.Size = 9
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With
                filasCount += 1
            Next
            '' poner borde del cuadro de clases de atencion 
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicioCategoria, 6), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filasCount - 1, 7), Microsoft.Office.Interop.Excel.Range))
                .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            End With



            filasCount += 2
            ''-----------------------------------------------------------------------------'
            ''-----------------------------------------------------------------------------'
            '' pintar el cuadro de clases                                      '
            ''-----------------------------------------------------------------------------'
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range))
                .Merge(True)
                .Value = "MOTIVO DE ATENCION"
                .Font.Size = 16
                .Interior.Color() = RGB(255, 0, 0)
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                .Font.Bold = True
            End With

            filasCount += 2
            filaInicioCategoria = filasCount
            With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range)
                .Value = "DIAGNOSTICO"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range)
                .Value = "CANTIDAD"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            filasCount += 1

            For Each ogrupoDiagnostico In grupoDiagnostico
                With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Microsoft.Office.Interop.Excel.Range)
                    .NumberFormat = "@"
                    .Value = ogrupoDiagnostico.nombreDiagnostico
                    .Font.Size = 9
                End With
                With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Microsoft.Office.Interop.Excel.Range)
                    .NumberFormat = "@"
                    .Value = ogrupoDiagnostico.cantidad
                    .Font.Size = 9
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With
                filasCount += 1
            Next
            '' poner borde del cuadro de clases de atencion 
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicioCategoria, 6), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filasCount - 1, 7), Microsoft.Office.Interop.Excel.Range))
                .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            End With



            filasCount += 2
            '
            ''-----------------------------------------------------------------------------'


            Dim filaInicio As Integer = filaInicial - 1
            For Each ogrado In grupoGradoAulaAlumnos

                For Each aulas In ogrado.aulas

                    For Each oalumno In aulas.Alumnos

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 1), Microsoft.Office.Interop.Excel.Range)
                            .NumberFormat = "@"
                        End With

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 1), Microsoft.Office.Interop.Excel.Range)
                            .Value = oalumno.codAlmuno.ToString.Trim
                            .Font.Size = 9
                        End With


                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 2), Microsoft.Office.Interop.Excel.Range)
                            .NumberFormat = "@"
                        End With

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 2), Microsoft.Office.Interop.Excel.Range)

                            If ogrado.nombreGrado.ToString.Trim = "" And aulas.nombreAula.ToString.Trim = "" Then
                                .Value = oalumno.tipoPersona
                            Else
                                .Value = ogrado.nombreGrado & IIf(ogrado.nombreGrado.ToString.Trim <> "", "/", "") & aulas.nombreAula.ToString
                            End If
                         



                        End With

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Microsoft.Office.Interop.Excel.Range)
                            .NumberFormat = "@"
                        End With

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Microsoft.Office.Interop.Excel.Range)
                            .Value = oalumno.nombreAlumnos.ToString.Trim
                            .Font.Size = 9
                        End With
                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 4), Microsoft.Office.Interop.Excel.Range)
                            .NumberFormat = "@"
                        End With

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 4), Microsoft.Office.Interop.Excel.Range)
                            .Value = oalumno.cantidad.ToString
                            .Font.Size = 9
                            .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        End With

                        totalAtencionesPorAula += CInt(oalumno.cantidad)
                        filaInicial += 1
                    Next

                Next

            Next

            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicio, 1), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filaInicial - 1, 4), Microsoft.Office.Interop.Excel.Range))
                .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            End With

            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaTemp, 1), Microsoft.Office.Interop.Excel.Range), CType(oExcel.ActiveSheet.Cells(filaTemp, 3), Microsoft.Office.Interop.Excel.Range))
                .Merge(True)
                .Value = "TOTAL DE ATENCIONES               " & totalAtencionesPorAula.ToString
                .Font.Size = 12
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
            End With
            oExcel.ActiveSheet.Cells.Columns(1).ColumnWidth = 8
            oExcel.ActiveSheet.Cells.Columns(2).ColumnWidth = 22
            oExcel.ActiveSheet.Cells.Columns(3).ColumnWidth = 33
            oExcel.ActiveSheet.Cells.Columns(4).ColumnWidth = 11
            oExcel.ActiveSheet.Cells.Columns(6).ColumnWidth = 31


            oExcel.ActiveSheet.Save()

            'oSheet.SaveAs(rutaREpositorioTemporales)
            'oBook.Close()

            'EiminaReferencias(oSheet)
            'EiminaReferencias(oBook)
            'oExcel.Quit()
            'EiminaReferencias(oExcel)
            'System.GC.Collect()

            ' ''-------------------------
            ' ''
            'Dim downloadBytes1 As Byte()
            'downloadBytes1 = File.ReadAllBytes(rutaREpositorioTemporales)

            '' Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
            'Response.Charset = ""
            'Response.ContentType = "binary/octet-stream"
            'Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteHouse.xlsx" + "; size=" + downloadBytes1.Length.ToString())
            'Response.Flush()
            'Response.BinaryWrite(downloadBytes1)
            'Response.End()

            ''


        Catch ex As Exception

        End Try
    End Sub
#End Region
    Public Function ExportarReporteAtencionesAcumuladoXHora(ByVal dtReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String, ByVal str_NumeroPintar As String, ByVal str_TipoPersona As String, ByVal str_Sede As String, ByVal str_Grado As String, ByVal str_FechaInicio As String, ByVal str_FechaFin As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteEnfermeria1_3").ToString()

        oExcel.Visible = False
        oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        oSheet.Activate()

        LlenarPlantillaReporteAtencionesAcumuladoXHora(dtReporte, oCells, oExcel, str_NombreEntidadReporte, str_NumeroPintar, str_TipoPersona, str_Sede, str_Grado, str_FechaInicio, str_FechaFin)



        ''----------------------------------

        ''----------------------------------

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function
    Private Shared Sub LlenarPlantillaReporteAtencionesAcumuladoXHora( _
        ByVal dtReporte As System.Data.DataSet, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String, ByVal str_NumeroPintar As String, ByVal str_TipoPersona As String, ByVal str_Sede As String, ByVal str_Grado As String, ByVal str_FechaInicio As String, ByVal str_Fechafin As String)

        Dim fila As Integer = 6
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filasDias As Integer = 0
        Dim cont_filasMes As Integer = 0
        Dim cont_filasHora As Integer = 0
        Dim ContFilasFechaAtencion As Integer = 0
        Dim int_codigoDia As Integer = 0
        Dim int_codigoMes As Integer = 0
        Dim cont_ColumnaHora As Integer = 0

        Dim str_FechaAtencion As String = ""

        Dim dtPlantilla As Data.DataTable
        Dim dtHoras As Data.DataTable
        Dim dtPlantillaDias As Data.DataTable
        Dim dtPlantillaMes As Data.DataTable
        Dim dtPlantillaCantHoras As Data.DataTable
        Dim dtPlantillaFechaAtencion As Data.DataTable
        Dim dtCantAtencionesMayor12 As Data.DataTable
        Dim dtTotalgeneral As Data.DataTable
        Dim dtPromedioDias As Data.DataTable
        Dim dtTotalXHoras As Data.DataTable

        dtPlantilla = dtReporte.Tables(0)
        dtHoras = dtReporte.Tables(1)
        dtPlantillaDias = dtReporte.Tables(2)
        dtPlantillaMes = dtReporte.Tables(3)
        dtPlantillaCantHoras = dtReporte.Tables(4)
        dtPlantillaFechaAtencion = dtReporte.Tables(5)
        dtCantAtencionesMayor12 = dtReporte.Tables(6)
        dtTotalgeneral = dtReporte.Tables(7)
        dtPromedioDias = dtReporte.Tables(8)
        dtTotalXHoras = dtReporte.Tables(9)

        'Pintado de Titulo
        oExcel.Range(oCells(1, 1), oCells(1, 14)).Merge()
        oExcel.Range(oCells(1, 1), oCells(1, 14)).HorizontalAlignment = 3
        oExcel.Range(oCells(1, 1), oCells(1, 14)).Value = "Consolidado de Atenciones Médicas Acumulado Por Horas"
        oExcel.Range(oCells(1, 1), oCells(1, 14)).Font.Bold = True

        'pintado Filtros
        oExcel.Range(oCells(2, 1), oCells(2, 14)).Merge()
        oExcel.Range(oCells(2, 1), oCells(2, 14)).HorizontalAlignment = 3
        oExcel.Range(oCells(2, 1), oCells(2, 14)).Value = "Rango de fechas ( " & str_FechaInicio & "  -  " & str_Fechafin & " )"
        oExcel.Range(oCells(2, 1), oCells(2, 14)).Font.Bold = True

        'pintado Filtros
        oExcel.Range(oCells(3, 1), oCells(3, 14)).Merge()
        oExcel.Range(oCells(3, 1), oCells(3, 14)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 1), oCells(3, 14)).Value = "Sede = " & str_Sede & " /  " & "Tipo de Persona = " & str_TipoPersona & " /  " & "Grado = " & str_Grado
        oExcel.Range(oCells(3, 1), oCells(3, 14)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(4, 1), oCells(4, 14)).Merge()
        oExcel.Range(oCells(4, 1), oCells(4, 14)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 1), oCells(4, 14)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(4, 1), oCells(4, 14)).Font.Bold = True

        'Pintado de Cabecera Dinamica 
        oExcel.Range(oCells(fila, 1), oCells(fila + 1, 1)).Merge()
        oExcel.Range(oCells(fila, 1), oCells(fila + 1, 1)).HorizontalAlignment = 3
        oExcel.Range(oCells(fila, 1), oCells(fila + 1, 1)).Value = "Atenciones del día"
        oExcel.Range(oCells(fila, 1), oCells(fila + 1, 1)).Interior.Color() = RGB(222, 184, 135)
        oExcel.Range(oCells(fila, 1), oCells(fila + 1, 1)).ColumnWidth = 20

        Dim int_contHoras As Integer
        While int_contHoras <= dtHoras.Rows.Count - 1
            oExcel.Range(oCells(fila + 1, columna + cont_columnas), oCells(fila + 1, columna + cont_columnas)).Value = dtHoras.Rows(int_contHoras).Item("HoraIngresoNumero")
            oExcel.Range(oCells(fila + 1, columna + cont_columnas), oCells(fila + 1, columna + cont_columnas)).Interior.Color() = RGB(250, 250, 210) ' RGB(255, 211, 155)
            oExcel.Range(oCells(fila + 1, columna + cont_columnas), oCells(fila + 1, columna + cont_columnas)).ColumnWidth = 5
            cont_columnas = cont_columnas + 1
            int_contHoras = int_contHoras + 1
        End While
        oExcel.Range(oCells(fila, columna), oCells(fila, columna + cont_columnas - 1)).Merge()
        oExcel.Range(oCells(fila, columna), oCells(fila, columna + cont_columnas - 1)).HorizontalAlignment = 3
        oExcel.Range(oCells(fila, columna), oCells(fila, columna + cont_columnas - 1)).Value = "Hora"
        oExcel.Range(oCells(fila, columna), oCells(fila, columna + cont_columnas - 1)).Interior.Color() = RGB(222, 184, 135)

        oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila + 1, columna + cont_columnas)).Merge()
        oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila + 1, columna + cont_columnas)).HorizontalAlignment = 3
        oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas)).Value = "Total General"
        oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas)).Interior.Color() = RGB(222, 184, 135)

        oExcel.Range(oCells(fila, 1), oCells(fila + 1, columna + cont_columnas)).WrapText = True

        cont_columnas = 2
        Dim int_contFilas As Integer = fila + 2

        Dim int_cantF As Integer = 0
        Dim int_cantFA As Integer = 0
        Dim int_totalFilaDetHora As Integer = 0
        Dim int_totalColumnasXFilaDetHora As Integer = 0
        Dim int_totalColumnaDetHora As Integer = 0

        While cont_filasDias <= dtPlantillaDias.Rows.Count - 1
            int_codigoDia = dtPlantillaDias.Rows(cont_filasDias).Item("CodigoDia")
            oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 1)).Value = dtPlantillaDias.Rows(cont_filasDias).Item("Dia")

            oExcel.Range(oCells(int_contFilas + int_cantF, 2), oCells(int_contFilas + int_cantF, columna + dtHoras.Rows.Count)).Merge()

            oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, columna + dtHoras.Rows.Count)).Interior.Color() = RGB(255, 211, 155)

            int_cantF = int_cantF + 1
            While cont_filasMes <= dtPlantillaMes.Rows.Count - 1
                int_codigoMes = dtPlantillaMes.Rows(cont_filasMes).Item("CodigoMes")
                oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 1)).Value = dtPlantillaMes.Rows(cont_filasMes).Item("Mes")

                oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 1)).Interior.Color() = RGB(205, 149, 12) 'RGB(0, 255, 0)

                oExcel.Range(oCells(int_contFilas + int_cantF, 2), oCells(int_contFilas + int_cantF, columna + dtHoras.Rows.Count)).Merge()

                Dim dv_FechaAtencion As DataView
                dv_FechaAtencion = dtPlantillaFechaAtencion.DefaultView
                dv_FechaAtencion.RowFilter = "1=1 and CodigoDia = '" & int_codigoDia.ToString & "' and CodigoMes ='" & int_codigoMes.ToString & "'"
                int_cantF = int_cantF + 1

                If dv_FechaAtencion.Count > 0 Then
                    For i As Integer = 0 To dv_FechaAtencion.Count - 1
                        oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 1)).Value = dv_FechaAtencion(i).Item("FechaAtencion").ToString()

                        ''''Horas
                        While cont_filasHora <= dtHoras.Rows.Count - 1

                            Dim dv_DetHora As DataView
                            dv_DetHora = dtPlantillaCantHoras.DefaultView
                            dv_DetHora.RowFilter = "1=1 and CodigoDia = '" & int_codigoDia.ToString & "' and CodigoMes ='" & int_codigoMes.ToString & "' and FechaAtencion = '" & dv_FechaAtencion(i).Item("FechaAtencion").ToString() & "' and HoraIngresoNumero= '" & dtHoras.Rows(cont_filasHora).Item("HoraIngresoNumero") & "'"


                            If dv_DetHora.Count > 0 Then
                                For j As Integer = 0 To dv_DetHora.Count - 1
                                    oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora), oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora)).Value = dv_DetHora(j).Item("CantidadAtencionesXHora").ToString()
                                    int_totalFilaDetHora = int_totalFilaDetHora + dv_DetHora(j).Item("CantidadAtencionesXHora").ToString()
                                    If dv_DetHora(j).Item("NumeroPintar") = 1 Then
                                        oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora), oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora)).Interior.Color() = RGB(255, 255, 0) 'RGB(149, 179, 215)
                                    End If
                                Next
                            Else
                                oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora), oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora)).Value = 0

                            End If
                            cont_ColumnaHora = cont_ColumnaHora + 1
                            cont_filasHora = cont_filasHora + 1
                        End While
                        ''''Total Filas
                        oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora), oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora)).Value = int_totalFilaDetHora
                        int_totalColumnasXFilaDetHora = int_totalColumnasXFilaDetHora + int_totalFilaDetHora

                        cont_filasHora = 0
                        cont_ColumnaHora = 0
                        int_totalFilaDetHora = 0
                        int_cantF = int_cantF + 1
                    Next
                End If

                cont_filasMes = cont_filasMes + 1
            End While

            cont_filasMes = 0
            cont_filasDias = cont_filasDias + 1
        End While
        cont_filasDias = 0



        'oExcel.ActiveWindow.Zoom = 75
        cuadradoCompleto(oExcel, oExcel.Range(oCells(fila, 1), oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora)))

        int_contHoras = 0
        cont_columnas = 0

        Dim int_NumeroHora As Integer

        oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_columnas - 1), oCells(int_contFilas + int_cantF, columna + cont_columnas - 1)).Value = "Total General"
        oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_columnas - 1), oCells(int_contFilas + int_cantF, columna + cont_columnas - 1)).Interior.Color() = RGB(153, 204, 50)

        While int_contHoras <= dtHoras.Rows.Count - 1
            int_NumeroHora = dtHoras.Rows(int_contHoras).Item("HoraIngresoNumero")

            Dim dv_TotalXHoras As DataView
            dv_TotalXHoras = dtTotalXHoras.DefaultView
            dv_TotalXHoras.RowFilter = "1=1 and HoraIngresoNumero = '" & int_NumeroHora.ToString & "'"

            If dv_TotalXHoras.Count > 0 Then
                For r As Integer = 0 To dv_TotalXHoras.Count - 1
                    oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_columnas), oCells(int_contFilas + int_cantF, columna + cont_columnas)).Value = dv_TotalXHoras(r).Item("CantidadTotalXHoras").ToString()
                    oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_columnas), oCells(int_contFilas + int_cantF, columna + cont_columnas)).Interior.Color() = RGB(153, 204, 50)

                Next
            End If
            int_contHoras = int_contHoras + 1
            cont_columnas = cont_columnas + 1
        End While
        oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_columnas), oCells(int_contFilas + int_cantF, columna + cont_columnas)).Interior.Color() = RGB(153, 204, 50)
        oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_columnas), oCells(int_contFilas + int_cantF, columna + cont_columnas)).Value = int_totalColumnasXFilaDetHora

        '''''''''''''''''''''''''''''''''''''''''
        ''Total General Mayor a la cantidad ingresada
        '''''''''''''''''''''''''''''''''''''''''
        cont_columnas = 0
        cont_filasMes = 0
        cont_filasHora = 0
        int_contHoras = 0
        int_codigoMes = 0
        int_contFilas = int_contFilas + 5
        'Cabecera

        oExcel.Range(oCells(int_contFilas + int_cantF - 2, 1), oCells(int_contFilas + int_cantF - 2, 1)).Value = "Mes"
        oExcel.Range(oCells(int_contFilas + int_cantF - 2, 1), oCells(int_contFilas + int_cantF - 1, 1)).Merge()

        While int_contHoras <= dtHoras.Rows.Count - 1
            oExcel.Range(oCells(int_contFilas + int_cantF - 1, columna + cont_columnas), oCells(int_contFilas + int_cantF - 1, columna + cont_columnas)).Value = dtHoras.Rows(int_contHoras).Item("HoraIngresoNumero")
            cont_columnas = cont_columnas + 1
            int_contHoras = int_contHoras + 1
        End While
        oExcel.Range(oCells(int_contFilas + int_cantF - 2, 2), oCells(int_contFilas + int_cantF - 2, columna + cont_columnas - 1)).Merge()
        oExcel.Range(oCells(int_contFilas + int_cantF - 2, 2), oCells(int_contFilas + int_cantF - 2, 2)).Value = "Cantidad de atenciones mayores a " & str_NumeroPintar


        oExcel.Range(oCells(int_contFilas + int_cantF - 2, 1), oCells(int_contFilas + int_cantF - 1, columna + cont_columnas - 1)).Interior.Color() = RGB(238, 221, 130)
        cuadradoCompleto(oExcel, oExcel.Range(oCells(int_contFilas + int_cantF - 2, 1), oCells(int_contFilas + int_cantF - 1, columna + cont_columnas - 1)))

        cont_columnas = 0
        'detalle
        While cont_filasMes <= dtPlantillaMes.Rows.Count - 1
            int_codigoMes = dtPlantillaMes.Rows(cont_filasMes).Item("CodigoMes")
            oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 1)).Value = dtPlantillaMes.Rows(cont_filasMes).Item("Mes")

            While cont_filasHora <= dtHoras.Rows.Count - 1
                Dim dv_DetHoraMayParEntr As DataView
                dv_DetHoraMayParEntr = dtCantAtencionesMayor12.DefaultView
                dv_DetHoraMayParEntr.RowFilter = "1=1 and CodigoMes ='" & int_codigoMes.ToString & "' and HoraIngresoNumero= '" & dtHoras.Rows(cont_filasHora).Item("HoraIngresoNumero") & "'"

                If dv_DetHoraMayParEntr.Count > 0 Then
                    For k As Integer = 0 To dv_DetHoraMayParEntr.Count - 1
                        oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora), oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora)).Value = dv_DetHoraMayParEntr(k).Item("CantidadAtencionesXHoraMayor12").ToString()
                    Next
                Else
                    oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora), oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora)).Value = 0

                End If
                cuadradoCompleto(oExcel, oExcel.Range(oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora - 1), oCells(int_contFilas + int_cantF, columna + cont_ColumnaHora)))

                'oExcel.Range(oCells(int_contFilas + int_cantF - 1, columna + cont_columnas), oCells(int_contFilas + int_cantF - 1, columna + cont_columnas)).Value = dtHoras.Rows(int_contHoras).Item("CantidadAtencionesXHoraMayor12")
                'int_cantF = int_cantF + 1
                cont_ColumnaHora = cont_ColumnaHora + 1
                cont_filasHora = cont_filasHora + 1
            End While
            'cont_columnas = cont_columnas + 1
            cont_filasHora = 0
            cont_ColumnaHora = 0
            int_cantF = int_cantF + 1
            cont_filasMes = cont_filasMes + 1
        End While




        cont_columnas = 0


        '''''''''''''''''''''''''''''''''''''''''
        ''Total General 
        '''''''''''''''''''''''''''''''''''''''''
        int_cantF = int_cantF + 2

        ''Cabecera
        oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 1)).Value = "Mes"
        oExcel.Range(oCells(int_contFilas + int_cantF, 2), oCells(int_contFilas + int_cantF, 4)).Merge()
        oExcel.Range(oCells(int_contFilas + int_cantF, 2), oCells(int_contFilas + int_cantF, 2)).Value = "Total General"

        oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 4)).Interior.Color() = RGB(219, 219, 112)
        cuadradoCompleto(oExcel, oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 4)))

        Dim cont_filasTotalGeneralXMes As Integer = 0

        While cont_filasTotalGeneralXMes <= dtTotalgeneral.Rows.Count - 1
            int_cantF = int_cantF + 1
            oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 1)).Value = dtTotalgeneral.Rows(cont_filasTotalGeneralXMes).Item("Mes")
            oExcel.Range(oCells(int_contFilas + int_cantF, 2), oCells(int_contFilas + int_cantF, 4)).Merge()
            oExcel.Range(oCells(int_contFilas + int_cantF, 2), oCells(int_contFilas + int_cantF, 2)).Value = dtTotalgeneral.Rows(cont_filasTotalGeneralXMes).Item("CantidadAtencionesXMes")
            cuadradoCompleto(oExcel, oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 4)))

            cont_filasTotalGeneralXMes = cont_filasTotalGeneralXMes + 1
        End While


        '''''''''''''''''''''''''''''''''''''''''
        ''Total Promedio 
        '''''''''''''''''''''''''''''''''''''''''
        int_cantF = int_cantF + 2

        ''Cabecera
        oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 1)).Value = "Mes"
        oExcel.Range(oCells(int_contFilas + int_cantF, 2), oCells(int_contFilas + int_cantF, 2)).Value = "Dias"
        oExcel.Range(oCells(int_contFilas + int_cantF, 3), oCells(int_contFilas + int_cantF, 4)).Merge()

        oExcel.Range(oCells(int_contFilas + int_cantF, 3), oCells(int_contFilas + int_cantF, 3)).Value = "Promedio (TotalGeneral / Dias)"
        oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 4)).WrapText = True

        oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 4)).Interior.Color() = RGB(205, 205, 0)
        cuadradoCompleto(oExcel, oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 4)))

        Dim cont_filasPromedioDias As Integer = 0

        While cont_filasPromedioDias <= dtPromedioDias.Rows.Count - 1
            int_cantF = int_cantF + 1
            oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 1)).Value = dtPromedioDias.Rows(cont_filasPromedioDias).Item("Mes")

            oExcel.Range(oCells(int_contFilas + int_cantF, 2), oCells(int_contFilas + int_cantF, 2)).Value = dtPromedioDias.Rows(cont_filasPromedioDias).Item("cantidadDias")
            oExcel.Range(oCells(int_contFilas + int_cantF, 3), oCells(int_contFilas + int_cantF, 4)).Merge()
            oExcel.Range(oCells(int_contFilas + int_cantF, 3), oCells(int_contFilas + int_cantF, 3)).Value = dtPromedioDias.Rows(cont_filasPromedioDias).Item("promedio")

            'oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 1)).Interior.Color() = RGB(205, 149, 12)

            cuadradoCompleto(oExcel, oExcel.Range(oCells(int_contFilas + int_cantF, 1), oCells(int_contFilas + int_cantF, 4)))

            cont_filasPromedioDias = cont_filasPromedioDias + 1
        End While




    End Sub

    'Reporte Codigo : 2 - 3
    Public Shared Function ExportarReporteDinamicoMedicamentosConsumidos(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim objTablaDinamica As Microsoft.Office.Interop.Excel.PivotTable
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteEnfermeria3").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteMedicamentosConsumidos(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinámico"
        oCells = oSheet.Cells

        'Pintado de Título
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Reporte de Medicamentos Consumidos"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 2), oCells(3, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        Dim int_cont As Integer = 0
        Dim str_DescTipo As String = ""

        Dim dv_Grado As DataView
        dv_Grado = dtReporte.DefaultView

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        ''Datos de Consulta'!$B$5:$G$17
        'Atenciones Médicas Detalladas!F5C2:F17C7
        'Medicamentos(Consumidos)
        objTablaDinamica.PivotCache.SourceData = "Medicamentos Consumidos!F5C2:F" & fila & "C7"
        objTablaDinamica.PivotCache.Refresh()

        oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        'While int_cont <= dtReporte.Rows.Count - 1
        '    str_DescTipo = dtReporte.Rows(int_cont).Item("Grado")
        '    oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescTipo).ShowDetail = False
        '    int_cont = int_cont + 1
        'End While

        oSheet.SaveAs(sFile)

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function
    Private Shared Function LlenarPlantillaReporteMedicamentosConsumidos( _
        ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(2, 3), oCells(2, 5))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Reporte de Medicamentos Consumidos"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 3), oCells(3, 5))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .Font.Color = RGB(0, 0, 0)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_columnas <= dtReporte.Columns.Count - 1
            While cont_filas <= dtReporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
                cont_filas += 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        fila = fila + dtReporte.Rows.Count
        str_Fila = (fila - 1).ToString

        oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)))
        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function

    'Reporte Codigo : 5 - 6
    Public Function ExportarReporteDinamicoDiagnosticos(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim objTablaDinamica As Microsoft.Office.Interop.Excel.PivotTable
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteEnfermeria6").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteDiagnosticos(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinámico"
        oCells = oSheet.Cells

        ''obtener el nombre de las sedes 
        ''----------------------------------------------------------------------------
        Dim sqlCombo2
        Dim concat2
        Dim nombreSede2 As String = ""

        Dim index2 As Integer = 0
        If cmbSede2.SelectedValue = 0 Then
            sqlCombo2 = From it In cmbSede2.Items Where CType(it, ListItem).Value <> 0 Select New With {.nombre = CType(it, ListItem).Text.ToString()}



            For Each nombre In sqlCombo2

                index2 += 1
                nombreSede2 &= nombre.nombre & IIf(index2 = 1, " - ", " ")


            Next

        Else
            nombreSede2 = cmbSede2.SelectedItem.Text.ToString()
        End If

        'Pintado de Título
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Reporte de Diagnósticos  de la sede : " & nombreSede2
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 2), oCells(3, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        Dim int_cont As Integer = 0
        Dim str_DescTipo As String = ""

        Dim dv_Grado As DataView
        dv_Grado = dtReporte.DefaultView

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        ''Diagnósticos!F5C2:F10C4
        objTablaDinamica.PivotCache.SourceData = "Diagnósticos!F5C2:F" & fila & "C4"
        objTablaDinamica.PivotCache.Refresh()

        oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        'While int_cont <= dtReporte.Rows.Count - 1
        '    str_DescTipo = dtReporte.Rows(int_cont).Item("Grado")
        '    oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescTipo).ShowDetail = False
        '    int_cont = int_cont + 1
        'End While

        oSheet.SaveAs(sFile)

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function
    Private Shared Function LlenarPlantillaReporteDiagnosticos( _
        ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(2, 3), oCells(2, 5))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Reporte de Procedimientos Realizados"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 3), oCells(3, 5))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .Font.Color = RGB(0, 0, 0)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_columnas <= dtReporte.Columns.Count - 1
            While cont_filas <= dtReporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
                cont_filas += 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        fila = fila + dtReporte.Rows.Count
        str_Fila = (fila - 1).ToString

        oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)))
        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function

    'Reporte Codigo : 6 - 7
    Public Shared Function ExportarReporteAlergias(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel").ToString()

        oExcel.Visible = False
        oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        oSheet.Activate()

        LlenarPlantillaReporteAlergias(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function
    Private Shared Sub LlenarPlantillaReporteAlergias( _
        ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String)

        Dim fila As Integer = 7 '5
        Dim columna As Integer = 3 '2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(2, 5), oCells(2, 3 + dtReporte.Columns.Count - 1))
            .Merge()
            .EntireRow.AutoFit()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Reporte de Alergias De Alumnos"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 5), oCells(3, 3 + dtReporte.Columns.Count - 1))
            .Merge()
            .EntireRow.AutoFit()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Size = 12
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .Font.Color = RGB(0, 0, 0)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_filas <= dtReporte.Rows.Count - 1
            While cont_columnas <= dtReporte.Columns.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
                cont_columnas += 1

                If cont_filas = dtReporte.Rows.Count - 1 Then
                    With oExcel.Range(oCells(fila + cont_filas, columna + cont_columnas), oCells(fila + cont_filas, columna + cont_columnas))
                        .EntireColumn.AutoFit()
                    End With
                End If
            End While
            cont_columnas = 0
            cont_filas += 1
        End While

        'While cont_columnas <= dtReporte.Columns.Count - 1
        '    While cont_filas <= dtReporte.Rows.Count - 1
        '        oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
        '        cont_filas += 1
        '    End While
        '    cont_filas = 0
        '    cont_columnas = cont_columnas + 1
        'End While

        fila = fila + dtReporte.Rows.Count
        'str_Fila = (fila - 1).ToString

        'oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(7, 3), oCells(fila - 1, columna + dtReporte.Columns.Count - 1)))
        oExcel.ActiveWindow.Zoom = 75

    End Sub

    'Reporte Codigo : 7 - 8
    Public Function ExportarReporteDinamicoProcedimientos(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim objTablaDinamica As Microsoft.Office.Interop.Excel.PivotTable
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteEnfermeria7").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteProcedimientos(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinámico"
        oCells = oSheet.Cells
        ''obtener el nombre de las sedes 
        ''----------------------------------------------------------------------------
        Dim sqlCombo2
        Dim concat2
        Dim nombreSede2 As String = ""

        Dim index2 As Integer = 0
        If cmbSede2.SelectedValue = 0 Then
            sqlCombo2 = From it In cmbSede2.Items Where CType(it, ListItem).Value <> 0 Select New With {.nombre = CType(it, ListItem).Text.ToString()}



            For Each nombre In sqlCombo2

                index2 += 1
                nombreSede2 &= nombre.nombre & IIf(index2 = 1, " - ", " ")


            Next

        Else
            nombreSede2 = cmbSede2.SelectedItem.Text.ToString()
        End If
        ''---------------------------------------------------------------------------------
        'Pintado de Título
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Reporte de Procedimientos sede : " & nombreSede2
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 2), oCells(3, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        Dim int_cont As Integer = 0
        Dim str_DescTipo As String = ""

        Dim dv_Grado As DataView
        dv_Grado = dtReporte.DefaultView

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        ''Datos de Consulta'!$B$5:$D$8
        objTablaDinamica.PivotCache.SourceData = "Procedimientos Realizados!F5C2:F" & fila & "C4"
        objTablaDinamica.PivotCache.Refresh()

        oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        'While int_cont <= dtReporte.Rows.Count - 1
        '    str_DescTipo = dtReporte.Rows(int_cont).Item("Grado")
        '    oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescTipo).ShowDetail = False
        '    int_cont = int_cont + 1
        'End While

        oSheet.SaveAs(sFile)

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function
    Private Shared Function LlenarPlantillaReporteProcedimientos( _
        ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(2, 3), oCells(2, 5))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Reporte de Medicamentos Consumidos"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 3), oCells(3, 5))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .Font.Color = RGB(0, 0, 0)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_columnas <= dtReporte.Columns.Count - 1
            While cont_filas <= dtReporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
                cont_filas += 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        fila = fila + dtReporte.Rows.Count
        str_Fila = (fila - 1).ToString

        oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)))
        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function

    'Reporte : 22 - 36
    Public Function ExportarReportePorEmergencia(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("Plantilla_Reporte_Medic_Deuda").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReportePorEmergencia(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function
    Private Function LlenarPlantillaReportePorEmergencia( _
         ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 6
        Dim columna As Integer = 1
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(1, 1), oCells(1, 10))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "RELACIÓN DE ALUMNOS"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(2, 1), oCells(2, 10))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 16
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        'Nivel, SubNivel, Grado y Aula
        oExcel.Range("C:E").HorizontalAlignment = 2
        If ddlRep5_Nivel.SelectedValue <> 0 Then
            oExcel.Range(oCells(4, 1), oCells(4, 10)).Merge()
            oExcel.Range(oCells(4, 1), oCells(4, 10)).HorizontalAlignment = 3
        End If

        If ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue <> 0 And ddlRep5_Grado.SelectedValue <> 0 And ddlRep5_Aula.SelectedValue <> 0 Then
            'Nivel, SubNivel, Grado, Aula           
            oCells(4, 1) = "Nivel:" & ddlRep5_Nivel.SelectedItem.ToString & "                                                      " & _
            "SubNivel:" & ddlRep5_SubNivel.SelectedItem.ToString & "                                                      " & _
           "Grado:" & ddlRep5_Grado.SelectedItem.ToString & "                                                      " & _
            "Aula:" & ddlRep5_Aula.SelectedItem.ToString
        ElseIf ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue <> 0 And ddlRep5_Grado.SelectedValue <> 0 And ddlRep5_Aula.SelectedValue = 0 Then
            oCells(4, 1) = "Nivel: " & ddlRep5_Nivel.SelectedItem.ToString & "                                                      " & _
            "SubNivel:" & ddlRep5_Nivel.SelectedItem.ToString & "                                                      " & _
            "Grado: " & ddlRep5_Grado.SelectedItem.ToString & "                                                       "
        ElseIf ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue <> 0 And ddlRep5_Grado.SelectedValue = 0 And ddlRep5_Aula.SelectedValue = 0 Then
            oCells(4, 1) = "Nivel:" & ddlRep5_Nivel.SelectedItem.ToString & "                                                      " & _
            "SubNivel:" & ddlRep5_SubNivel.SelectedItem.ToString
        ElseIf ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue = 0 And ddlRep5_Grado.SelectedValue = 0 And ddlRep5_Aula.SelectedValue = 0 Then
            oCells(4, 1) = "Nivel:" & ddlRep5_Nivel.SelectedItem.ToString
        ElseIf ddlRep5_Nivel.SelectedValue = 0 Then
            fila = 4
        End If

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_columnas <= dtReporte.Columns.Count - 1
            While cont_filas <= dtReporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
                cont_filas += 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        fila = fila + dtReporte.Rows.Count
        str_Fila = (fila - 1).ToString
        Dim Valfila As Integer
        If ddlRep5_Nivel.SelectedValue = 0 Then
            Valfila = 4
        Else
            Valfila = 6
        End If
        oExcel.Range(oCells(4, 1), oCells(str_Fila, 10)).Select()
        'Tamaño y tipo de Letra
        With oExcel.Selection.Font
            .Name = "Arial"
            .Size = "14"
        End With
        oExcel.Range("C:C").HorizontalAlignment = 3
        If ddlRep5_Nivel.SelectedValue = 0 Then
            'Cambiar la altura y Centrado el registro
            With oExcel.Rows("5:5")
                .RowHeight = 26.25
                .VerticalAlignment = 2
            End With
            oExcel.Rows("5:5").Select()
        Else
            'Cambiar la altura y Centrado el registro
            With oExcel.Rows("7:7")
                .RowHeight = 26.25
                .VerticalAlignment = 2
            End With
            oExcel.Rows("7:7").Select()
        End If
        oExcel.ActiveWindow.FreezePanes = True

        'HORIZONTAL
        oExcel.Application.PrintCommunication = False
        With oExcel.ActiveSheet.PageSetup
            .PrintTitleRows = ""
            .PrintTitleColumns = ""
        End With
        oExcel.Application.PrintCommunication = True
        oExcel.ActiveSheet.PageSetup.PrintArea = ""
        With oExcel.ActiveSheet.PageSetup
            .LeftHeader = ""
            .CenterHeader = ""
            .RightHeader = ""
            .LeftFooter = ""
            .CenterFooter = ""
            .RightFooter = ""
            .LeftMargin = oExcel.Application.InchesToPoints(0.708661417322835)
            .RightMargin = oExcel.Application.InchesToPoints(0.708661417322835)
            .TopMargin = oExcel.Application.InchesToPoints(0.748031496062992)
            .BottomMargin = oExcel.Application.InchesToPoints(0.748031496062992)
            .HeaderMargin = oExcel.Application.InchesToPoints(0.31496062992126)
            .FooterMargin = oExcel.Application.InchesToPoints(0.31496062992126)
            .PrintHeadings = False
            .PrintGridlines = False
            .PrintComments = Microsoft.Office.Interop.Excel.XlPrintLocation.xlPrintNoComments
            .PrintQuality = 600
            .CenterHorizontally = False
            .CenterVertically = False
            .Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape
            .Draft = False
            .PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4
            .FirstPageNumber = 1
            .Order = Microsoft.Office.Interop.Excel.XlOrder.xlDownThenOver
            .BlackAndWhite = False
            .Zoom = 100
            .PrintErrors = Microsoft.Office.Interop.Excel.XlPrintErrors.xlPrintErrorsDisplayed
            .OddAndEvenPagesHeaderFooter = False
            .DifferentFirstPageHeaderFooter = False
            .ScaleWithDocHeaderFooter = True
            .AlignMarginsHeaderFooter = True
            .EvenPage.LeftHeader.Text = ""
            .EvenPage.CenterHeader.Text = ""
            .EvenPage.RightHeader.Text = ""
            .EvenPage.LeftFooter.Text = ""
            .EvenPage.CenterFooter.Text = ""
            .EvenPage.RightFooter.Text = ""
            .FirstPage.LeftHeader.Text = ""
            .FirstPage.CenterHeader.Text = ""
            .FirstPage.RightHeader.Text = ""
            .FirstPage.LeftFooter.Text = ""
            .FirstPage.CenterFooter.Text = ""
            .FirstPage.RightFooter.Text = ""
        End With

        If ddlRep5_Nivel.SelectedValue = 0 Then
            'Copy el formato a todas las celdas
            oExcel.Rows("5:5").Select()
            oExcel.Selection.VerticalAlignment = 3
            'oExcel.VerticalAlignment = 3
            oExcel.Selection.Copy()
            oExcel.ActiveWindow.SmallScroll(Down:=-3)
            oExcel.Rows("6:" & str_Fila).Select()
            oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
            'Cuadra la Hoja del excel
            oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
            cuadradoCompleto(oExcel, oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)))
            oExcel.Range(oCells(5, 6), oCells(fila, 6)).HorizontalAlignment = 2
            'Margen()
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
            oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("j1")
            oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
        Else
            'Copy el formato a todas las celdas
            oExcel.Rows("7:7").Select()
            'oExcel.Selection.WrapText = True
            oExcel.Selection.Copy()
            oExcel.ActiveWindow.SmallScroll(Down:=-3)
            oExcel.Rows("8:" & str_Fila).Select()
            oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
            'Cuadra la Hoja del excel
            oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
            cuadradoCompleto(oExcel, oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)))
            oExcel.Range(oCells(7, 6), oCells(fila, 6)).HorizontalAlignment = 2
            'Margen
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
            oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("j1")
            oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView

        End If
        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("a3").Select()
        Return str_Fila
    End Function

    'Reporte : 19 - 37
    Public Function ExportarReportePorSeguro(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("Plantilla_Reporte_Medic_Deuda").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReportePorSeguro(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function
    Private Function LlenarPlantillaReportePorSeguro( _
         ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 6
        Dim columna As Integer = 1
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(1, 1), oCells(1, 6))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "DATOS MÉDICOS DEL ALUMNOS"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(2, 1), oCells(2, 6))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 16
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        'Nivel, SubNivel, Grado y Aula
        oExcel.Range("C:E").HorizontalAlignment = 2
        If ddlRep5_Nivel.SelectedValue <> 0 Then
            oExcel.Range(oCells(4, 1), oCells(4, 6)).Merge()
            oExcel.Range(oCells(4, 1), oCells(4, 6)).HorizontalAlignment = 3
        End If

        If ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue <> 0 And ddlRep5_Grado.SelectedValue <> 0 And ddlRep5_Aula.SelectedValue <> 0 Then
            'Nivel, SubNivel, Grado, Aula           
            oCells(4, 1) = "Nivel:" & ddlRep5_Nivel.SelectedItem.ToString & "               " & _
            "SubNivel:" & ddlRep5_SubNivel.SelectedItem.ToString & "               " & _
           "Grado:" & ddlRep5_Grado.SelectedItem.ToString & "               " & _
            "Aula:" & ddlRep5_Aula.SelectedItem.ToString
        ElseIf ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue <> 0 And ddlRep5_Grado.SelectedValue <> 0 And ddlRep5_Aula.SelectedValue = 0 Then
            oCells(4, 1) = "Nivel: " & ddlRep5_Nivel.SelectedItem.ToString & "               " & _
            "SubNivel:" & ddlRep5_Nivel.SelectedItem.ToString & "               " & _
            "Grado: " & ddlRep5_Grado.SelectedItem.ToString & "               "
        ElseIf ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue <> 0 And ddlRep5_Grado.SelectedValue = 0 And ddlRep5_Aula.SelectedValue = 0 Then
            oCells(4, 1) = "Nivel:" & ddlRep5_Nivel.SelectedItem.ToString & "               " & _
            "SubNivel:" & ddlRep5_SubNivel.SelectedItem.ToString
        ElseIf ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue = 0 And ddlRep5_Grado.SelectedValue = 0 And ddlRep5_Aula.SelectedValue = 0 Then
            oCells(4, 1) = "Nivel:" & ddlRep5_Nivel.SelectedItem.ToString
        ElseIf ddlRep5_Nivel.SelectedValue = 0 Then
            fila = 4
        End If

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_columnas <= dtReporte.Columns.Count - 1
            While cont_filas <= dtReporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
                cont_filas += 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        fila = fila + dtReporte.Rows.Count
        str_Fila = (fila - 1).ToString
        Dim Valfila As Integer
        If ddlRep5_Nivel.SelectedValue = 0 Then
            Valfila = 4
        Else
            Valfila = 6
        End If
        oExcel.Range(oCells(4, 1), oCells(str_Fila, 6)).Select()
        'Tamaño y tipo de Letra
        With oExcel.Selection.Font
            .Name = "Arial"
            .Size = "14"
        End With
        oExcel.Range("C:C").HorizontalAlignment = 3
        oExcel.Range("E:E").HorizontalAlignment = 3
        If ddlRep5_Nivel.SelectedValue = 0 Then
            'Cambiar la altura y Centrado el registro
            With oExcel.Rows("5:5")
                .RowHeight = 26.25
                .VerticalAlignment = 2
            End With
            oExcel.Rows("5:5").Select()
        Else
            'Cambiar la altura y Centrado el registro
            With oExcel.Rows("7:7")
                .RowHeight = 26.25
                .VerticalAlignment = 2
            End With
            oExcel.Rows("7:7").Select()
        End If
        oExcel.ActiveWindow.FreezePanes = True

        If ddlRep5_Nivel.SelectedValue = 0 Then
            'Copy el formato a todas las celdas
            oExcel.Rows("5:5").Select()
            oExcel.Selection.Copy()
            oExcel.ActiveWindow.SmallScroll(Down:=-3)
            oExcel.Rows("6:" & str_Fila).Select()
            oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
            'Cuadra la Hoja del excel
            oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
            cuadradoCompleto(oExcel, oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)))
            oExcel.Columns("F:F").ColumnWidth = 65.29
            'Margen()
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
            oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("F1")
            oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
        Else
            'Copy el formato a todas las celdas
            oExcel.Rows("7:7").Select()
            'oExcel.Selection.WrapText = True
            oExcel.Selection.Copy()
            oExcel.ActiveWindow.SmallScroll(Down:=-3)
            oExcel.Rows("8:" & str_Fila).Select()
            oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
            'Cuadra la Hoja del excel
            oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
            cuadradoCompleto(oExcel, oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)))
            oExcel.Columns("F:F").ColumnWidth = 65.29
            'Margen
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
            oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("F1")
            oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView

        End If
        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("a3").Select()
        Return str_Fila
    End Function

    'Reporte : 21 - 38
    Public Function ExportarReportePorEnfermedadAlumno(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("Plantilla_Reporte_Enfermedad").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReportePorEnfermedadAlumno(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function
    Private Function LlenarPlantillaReportePorEnfermedadAlumno( _
         ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 6
        Dim columna As Integer = 1
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(1, 1), oCells(1, 3))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "DATOS MÉDICOS DEL ALUMNOS"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(2, 1), oCells(2, 3))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 16
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        'Nivel, SubNivel, Grado y Aula
        oExcel.Range("C:E").HorizontalAlignment = 2
        If ddlRep5_Nivel.SelectedValue <> 0 Then
            oExcel.Range(oCells(4, 1), oCells(4, 3)).Merge()
            oExcel.Range(oCells(4, 1), oCells(4, 3)).HorizontalAlignment = 3
        End If

        If ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue <> 0 And ddlRep5_Grado.SelectedValue <> 0 And ddlRep5_Aula.SelectedValue <> 0 Then
            'Nivel, SubNivel, Grado, Aula           
            oCells(4, 1) = "Nivel:" & ddlRep5_Nivel.SelectedItem.ToString & "               " & _
            "SubNivel:" & ddlRep5_SubNivel.SelectedItem.ToString & "               " & _
           "Grado:" & ddlRep5_Grado.SelectedItem.ToString & "               " & _
            "Aula:" & ddlRep5_Aula.SelectedItem.ToString
        ElseIf ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue <> 0 And ddlRep5_Grado.SelectedValue <> 0 And ddlRep5_Aula.SelectedValue = 0 Then
            oCells(4, 1) = "Nivel: " & ddlRep5_Nivel.SelectedItem.ToString & "               " & _
            "SubNivel:" & ddlRep5_Nivel.SelectedItem.ToString & "               " & _
            "Grado: " & ddlRep5_Grado.SelectedItem.ToString & "               "
        ElseIf ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue <> 0 And ddlRep5_Grado.SelectedValue = 0 And ddlRep5_Aula.SelectedValue = 0 Then
            oCells(4, 1) = "Nivel:" & ddlRep5_Nivel.SelectedItem.ToString & "               " & _
            "SubNivel:" & ddlRep5_SubNivel.SelectedItem.ToString
        ElseIf ddlRep5_Nivel.SelectedValue <> 0 And ddlRep5_SubNivel.SelectedValue = 0 And ddlRep5_Grado.SelectedValue = 0 And ddlRep5_Aula.SelectedValue = 0 Then
            oCells(4, 1) = "Nivel:" & ddlRep5_Nivel.SelectedItem.ToString
        ElseIf ddlRep5_Nivel.SelectedValue = 0 Then
            fila = 4
        End If

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_columnas <= dtReporte.Columns.Count - 1
            While cont_filas <= dtReporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
                cont_filas += 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        fila = fila + dtReporte.Rows.Count
        str_Fila = (fila - 1).ToString
        Dim Valfila As Integer
        If ddlRep5_Nivel.SelectedValue = 0 Then
            Valfila = 4
        Else
            Valfila = 6
        End If
        oExcel.Range(oCells(4, 1), oCells(str_Fila, 6)).Select()
        'Tamaño y tipo de Letra
        With oExcel.Selection.Font
            .Name = "Arial"
            .Size = "14"
        End With
        'oExcel.Range("C:C").HorizontalAlignment = 3
        If ddlRep5_Nivel.SelectedValue = 0 Then
            'Cambiar la altura y Centrado el registro
            With oExcel.Rows("5:5")
                .RowHeight = 26.25
                .VerticalAlignment = 2
            End With
            oExcel.Rows("5:5").Select()
        Else
            'Cambiar la altura y Centrado el registro
            With oExcel.Rows("7:7")
                .RowHeight = 26.25
                .VerticalAlignment = 2
            End With
            oExcel.Rows("7:7").Select()
        End If
        oExcel.ActiveWindow.FreezePanes = True

        If ddlRep5_Nivel.SelectedValue = 0 Then
            'Copy el formato a todas las celdas
            oExcel.Rows("5:5").Select()
            oExcel.Selection.Copy()
            oExcel.ActiveWindow.SmallScroll(Down:=-3)
            oExcel.Rows("6:" & str_Fila).Select()
            oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
            'Cuadra la Hoja del excel
            'oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
            cuadradoCompleto(oExcel, oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)))
            'oExcel.Columns.Range("C:C").ColumnWidth = 130.57
            'Margen()
            'oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
            'oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("C1")
            'oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
            'oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
            'oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
            'oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("D1")
            'oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
        Else
            'Copy el formato a todas las celdas
            oExcel.Rows("7:7").Select()
            'oExcel.Selection.WrapText = True
            oExcel.Selection.Copy()
            oExcel.ActiveWindow.SmallScroll(Down:=-3)
            oExcel.Rows("8:" & str_Fila).Select()
            oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
            'Cuadra la Hoja del excel
            'oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
            cuadradoCompleto(oExcel, oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)))
            'oExcel.Columns("C:C").ColumnWidth = 130.57
            'Margen
            'oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
            'oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("C1")
            'oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
            'oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView

            'oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
            'oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("C1")
            'oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView

        End If
        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("a3").Select()
        Return str_Fila
    End Function

    'Reporte codigo:3

    Public Shared Function ExportarReporteHistorialAtenciones_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try

            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaFichaAtencionHistorialClinicoHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaHistorialAtencionesHtml(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)

        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try

        Return fileReaderPlantilla

    End Function
    Private Shared Function LlenarPlantillaHistorialAtencionesHtml(ByVal Plantilla As String, ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        '    
        'Plantilla = Plantilla.Replace("[Codigo_FichaAtencion]", IIf(dsReporte.Tables(0).Rows(0).Item("CodigoFichaAtencion").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("CodigoFichaAtencion")))

        'Detalle cabeceras
        Plantilla = Plantilla.Replace("[Nombre_Completo]", IIf(dsReporte.Tables(3).Rows(0).Item("NombreAlumno").ToString.Length = 0, "-", dsReporte.Tables(3).Rows(0).Item("NombreAlumno")))
        Plantilla = Plantilla.Replace("[Fecha_Nacimiento]", IIf(dsReporte.Tables(3).Rows(0).Item("FechaNacimiento").ToString.Length = 0, "-", dsReporte.Tables(3).Rows(0).Item("FechaNacimiento")))
        Plantilla = Plantilla.Replace("[Codigo]", IIf(dsReporte.Tables(3).Rows(0).Item("CodigoAlumno").ToString.Length = 0, "-", dsReporte.Tables(3).Rows(0).Item("CodigoAlumno")))
        Plantilla = Plantilla.Replace("[Sexo]", IIf(dsReporte.Tables(3).Rows(0).Item("Sexo").ToString.Length = 0, "-", dsReporte.Tables(3).Rows(0).Item("Sexo")))
        Plantilla = Plantilla.Replace("[GradoSeccion]", IIf(dsReporte.Tables(3).Rows(0).Item("GradoSeccion").ToString.Length = 0, "-", dsReporte.Tables(3).Rows(0).Item("GradoSeccion")))
        ''ListaEnfermedad

        Plantilla = Plantilla.Replace("[ListaEnfermedad]", "<table cellpadding='0' cellspacing='0' style=' width: 450px;'>[ListaEnfermedad]")
        Plantilla = Plantilla.Replace("[ListaEnfermedad]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px; ' align='left' valign='bottom'><b>Enfermedad</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:193px; font-size:10px;' align='left' valign='bottom'><b>Edad</b></td></tr>[ListaEnfermedad]")

        If dsReporte.Tables(0).Rows(0).Item("CodigoRelFichaMedEnEnfermedades") <> -1 Then

            'Plantilla = Plantilla.Replace("[ListaEnfermedad]", "<Table><tr><td align='left' valign='top' style='width:75%'>Enfermedad</td>[ListaEnfermedad]")
            For i As Integer = 0 To dsReporte.Tables(0).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaEnfermedad]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px;'>" & dsReporte.Tables(0).Rows(i).Item("Enfermedad") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:193px;font-size:10px;'>" & IIf(dsReporte.Tables(0).Rows(i).Item("Edad") = 0, "", dsReporte.Tables(0).Rows(i).Item("Edad")) & " años " & "</td></tr>[ListaEnfermedad]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaEnfermedad]", "<tr><td colspan='2' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaEnfermedad]")

        End If

        Plantilla = Plantilla.Replace("[ListaEnfermedad]", "</table>")

        ''ListaAlergia
        Plantilla = Plantilla.Replace("[ListaAlergia]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 450px;'>[ListaAlergia]")
        Plantilla = Plantilla.Replace("[ListaAlergia]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px; ' align='left' valign='bottom'><b>Alergia</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:193px; font-size:10px; ' align='left' valign='bottom'><b>Tipo Alergia</b></td></tr>[ListaAlergia]")

        If dsReporte.Tables(1).Rows(0).Item("CodigoRelFichaMedAlergias") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(1).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaAlergia]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px;'>" & dsReporte.Tables(1).Rows(i).Item("Alergia") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:193px;font-size:10px;'>" & dsReporte.Tables(1).Rows(i).Item("TipoAlergia") & "</td></tr>[ListaAlergia]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaAlergia]", "<tr><td colspan='2' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaAlergia]")

        End If

        Plantilla = Plantilla.Replace("[ListaAlergia]", "</table>")

        ''ListaMedicamento
        Plantilla = Plantilla.Replace("[ListaMedicamento]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 450px;'>[ListaMedicamento]")
        Plantilla = Plantilla.Replace("[ListaMedicamento]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:130px;  font-size:10px; ' align='left' valign='bottom'><b>Medicamento</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:120px; font-size:10px; ' align='left' valign='bottom'><b>Presentación / Cantidad</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:100px; font-size:10px; ' align='left' valign='bottom'><b>Dosis</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:100px; font-size:10px; ' align='left' valign='bottom'>Observaciones</td></tr>[ListaMedicamento]")

        If dsReporte.Tables(2).Rows(0).Item("CodigoRelFichaAtenMedicamentos") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(2).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaMedicamento]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:130px; font-size:10px;'>" & dsReporte.Tables(2).Rows(i).Item("Medicamento") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:120px;font-size:10px;'>" & dsReporte.Tables(2).Rows(i).Item("PresentCant") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:100px;font-size:10px;'>" & dsReporte.Tables(2).Rows(i).Item("DosisMedicamento") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:100px;font-size:10px;'>" & dsReporte.Tables(2).Rows(i).Item("Observaciones") & "</td></tr>[ListaMedicamento]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaMedicamento]", "<tr><td colspan='4' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaMedicamento]")

        End If
        Plantilla = Plantilla.Replace("[ListaMedicamento]", "</table>")

        If dsReporte.Tables(3).Rows.Count > 0 Then
            Dim str_plantillaFila As String
            Dim int_contfilas As Integer = 0

            str_plantillaFila = "<tr><td style='padding-left :7px; width:100px; height :20px;' align='left' valign='top'>N° Atención:</td>" & _
                                    "<td style='width:100px; height :20px;' align='left' valign='top'>[NroAtencion]</td>" & _
                                    "<td style='width:60px; height :20px;' align='left' valign='top'>Fecha:</td>" & _
                                    "<td style='width:100px;height :20px;' align='left' valign='top'>[Fecha]</td>" & _
                                    "<td style='width:60px; height :20px;' align='left' valign='top'>H.Ingreso:</td>" & _
                                    "<td style='width:80px; height :20px;' align='left' valign='top'>[HoraIngreso]</td>" & _
                                    "<td style='width:60px; height :20px;' align='left' valign='top'>H.Salida:</td>" & _
                                    "<td  style='width:80px; height :20px;' align='left' valign='top'>[HoraSalida]</td></tr>" & _
                                "<tr><td style='padding-left :7px; width:100px; height :20px;' align='left' valign='top'>Sintoma:</td>" & _
                                    "<td colspan ='7' style='width:500px; height :20px;' align='left' valign='top'>[Sintoma]</td></tr>" & _
                                "<tr><td style='padding-left :7px; width:100px; height :20px;' align='left' valign='top'>Diagnostico:</td>" & _
                                    "<td colspan ='7' style='width:500px; height :20px;' align='left' valign='top'> [Diagnostico]</td> </tr>" & _
                                "<tr><td style='padding-left :7px; width:100px; height :20px;' align='left' valign='top'> Procedimiento:</td>" & _
                                    "<td colspan ='7' style='width:500px; height :20px;' align='left' valign='top'>[Procedimiento]</td></tr>" & _
                                "<tr><td style='padding-left :7px; width:100px; height :20px;' align='left' valign='top'> Medicina:</td>" & _
                                    "<td colspan ='7' style='width:500px; height :20px;' align='left' valign='top'> [Medicina]</td></tr>" & _
                                "<tr><td style='padding-left :7px; width:100px; height :20px;' align='left' valign='top'> Destino:</td>" & _
                                    "<td colspan ='7' style='width:500px; height :20px;' align='left' valign='top'>[Destino]</td></tr>" & _
                               "<tr> <td colspan='8' style='padding-left :7px; width:600px;  height :0px; font-size:5px' > " & _
                                       "________________________________________________________________________________________________________________________________________________________________________________________________________________</td>" & _
                               " </tr> "

            While int_contfilas <= dsReporte.Tables(3).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaAtenciones]", str_plantillaFila & "[ListaAtenciones]")

                Plantilla = Plantilla.Replace("[NroAtencion]", dsReporte.Tables(3).Rows(int_contfilas).Item("NumeroAtencion"))
                Plantilla = Plantilla.Replace("[Fecha]", dsReporte.Tables(3).Rows(int_contfilas).Item("FechaAtencion"))
                Plantilla = Plantilla.Replace("[HoraIngreso]", dsReporte.Tables(3).Rows(int_contfilas).Item("HoraIngreso"))
                Plantilla = Plantilla.Replace("[HoraSalida]", dsReporte.Tables(3).Rows(int_contfilas).Item("HoraSalida"))
                Plantilla = Plantilla.Replace("[Sintoma]", dsReporte.Tables(3).Rows(int_contfilas).Item("Sintoma"))
                Plantilla = Plantilla.Replace("[Diagnostico]", dsReporte.Tables(3).Rows(int_contfilas).Item("Diagnosticos"))
                Plantilla = Plantilla.Replace("[Procedimiento]", dsReporte.Tables(3).Rows(int_contfilas).Item("ProcedimientoEnfermeria"))
                Plantilla = Plantilla.Replace("[Medicina]", dsReporte.Tables(3).Rows(int_contfilas).Item("Medicamentos"))
                Plantilla = Plantilla.Replace("[Destino]", dsReporte.Tables(3).Rows(int_contfilas).Item("Destino"))

                int_contfilas = int_contfilas + 1
            End While

            Plantilla = Plantilla.Replace("[ListaAtenciones]", "")
        End If


        Return Plantilla
    End Function

    Private Shared Sub cuadradoCompleto(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range)
        Try

            objRango.Select()
            With mexcel
                '.Range(Rango).Select()
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
            End With
        Catch ex As Exception

        End Try
    End Sub



#End Region


#Region "Reporte de enfermeria acumulados por semana"



    ''' <summary>
    ''' funcion para crear el resporte de enfermeria de atenciones medicas 
    ''' </summary>
    ''' <param name="codAnio">  </param>
    ''' <param name="fechaInicio"></param>
    ''' <param name="fechaFin"></param>
    ''' <param name="cantidadAtencion"></param>
    ''' <param name="nivel"></param>
    ''' <param name="subNivel"></param>
    ''' <param name="codGrado"></param>
    ''' <param name="codAula"></param>
    ''' <param name="codSede"></param>
    ''' <param name="tipoReporte"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function reporteEnfermeria(ByVal codAnio As Integer, ByVal fechaInicio As String, ByVal fechaFin As String, ByVal cantidadAtencion As Integer, _
                                      ByVal nivel As Integer, _
                                      ByVal subNivel As Integer, _
                                      ByVal codGrado As Integer, _
                                      ByVal codAula As Integer, _
                                      ByVal codSede As Integer, ByVal tipoReporte As Integer, _
                                      ByVal codPersona As Integer) As String
        Try
            Dim nombreAnioAcademico As String = "CONTROL DE ATENCIONES ENFERMERIA - AÑO ACADÉMICO " & Year(Convert.ToDateTime(fechaInicio)).ToString()
            Dim rangoFechas As String = " Rango de fechas ( " & fechaInicio & " -" & fechaFin & " ) "
            Dim fechaRporte As String = "Fecha de Reporte: " & Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year & " " & Date.Now.Hour.ToString & ":" & Date.Now.Minute
            Dim dst As DataSet
            Dim nParam As String = ""



            Dim dc As New Dictionary(Of String, Object)
            dc.Add("codAnioAcademico", 0)
            dc.Add("fechaInicio", fechaInicio)
            dc.Add("fechaFin", fechaFin)
            dc.Add("cantidadAtencion", cantidadAtencion)
            dc.Add("nivel", nivel)
            dc.Add("subNivel", subNivel)
            dc.Add("codGrado", codGrado)
            dc.Add("codAula", codAula)
            dc.Add("codSede", codSede)
            dc.Add("codPersona", codPersona)

            'codPersona


            Dim dicProc As New Dictionary(Of Integer, String)


            dicProc(1) = "USP_LisReporteEnfermeriaAlumno"
            dicProc(2) = "USP_LisReporteEnfermeriaTrabajador"
            dicProc(3) = "USP_LisReporteEnfermeriaFamilia"
            dicProc(0) = "USP_LisReporteEnfermeriaTodo"


            nParam = dicProc(tipoReporte)
            dst = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam)

            Dim dtRegistros As New Data.DataTable
            dtRegistros = dst.Tables(0)

            Dim dtFechas As New Data.DataTable
            dtFechas = dst.Tables(1)
            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current
            ''




            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("reporteEnfermeria")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"



            File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            ''***********************************************''
            ''****         variables           **************''
            ''***********************************************''

            Dim sqlObjectAlumnos = (From sql In dtRegistros.AsEnumerable() Group sql By codAlumno = sql("AL_CodigoAlumno"), _
                                                              nombreAlumno = sql("nombreCompleto") Into detalle = Group Select New _
                                                           With {.nombreCmpleto = nombreAlumno, .detalle = _
                                                                 (From dt In detalle.AsEnumerable() Select New With {.fecha = dt("fecha"), _
                                                                                                                     .cantidad = dt("cantidad")})})




            Dim sqlFe = From sqlFechas In dtFechas.AsEnumerable() Group sqlFechas By anio = sqlFechas("anio"), numeroMes = sqlFechas("numeroMes"), nombreMes = sqlFechas("nombreMes") Into dtFec = Group _
                        Select New With {.nombreMEs = nombreMes, .semanas = (From sem In dtFec.AsEnumerable() Group sem By codSem = sem("semana") Into dias = Group _
                                                                                                           Select New With {.codSem = codSem, .dias = (From ds In dias.AsEnumerable() _
                                                                                                                                                  Select New With _
                                                                                                                                                  { _
                                                                                                                                                        .fecha = ds("fecha"), _
                                                                                                                                                        .dia = ds("dia"), _
                                                                                                                                                        .col = ds("col"), _
                                                                                                                                                        .nombreDia = ds("nombreDia") _
                                                                                                                                                    })})}

            ''
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)
            Dim ws = workbook.Worksheet(1)


            ''  poner el titulo de la cabezera 
            With ws.Range(ws.Cell(3, 2), ws.Cell(3, 9))
                .Merge()
                .Value = nombreAnioAcademico
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With

            ''  poner el rango de fechas 
            With ws.Range(ws.Cell(4, 2), ws.Cell(4, 9))
                .Merge()
                .Value = rangoFechas
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With



            ''  poner las fechas de reporte 
            With ws.Range(ws.Cell(5, 2), ws.Cell(5, 9))
                .Merge()
                .Value = fechaRporte
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With
            With ws.Cell(8, 2)
                .Value = "Cantidad mayor o  igual a:  "
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
            End With



            With ws.Cell(8, 3)
                .Value = ddlRep6_NumeroPintar.SelectedItem.Text
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.FontColor = XLColor.Red
            End With
            ''------------------------------------------------------------''
            Dim filaEmpieza As Integer = 3
            Dim columnasPintarDias As Integer = 2
            Dim columnas As Integer = 3
            Dim columnasContador As Integer = 2
            Dim limInf As Integer = 0
            Dim limSup As Integer = 0
            Dim contador As Integer = 0
            Dim contadorFilas As Integer = 0
            Dim limInf_1 As Integer = 0
            Dim limSup_1 As Integer = 0
            Dim posMes As Integer = 0
            ''-----------------------------------------------------------------
            ''Crear cabezera 
            ''-----------------------------------------------------------------


            ''-----------------------------------------------------------------
            ''Crear Filas  de cabezera de fechas 
            ''-----------------------------------------------------------------
            filaEmpieza = 12 '' salto a la fila 12 para pintar los nombres de los dias 
            With ws.Cell(filaEmpieza, 1)
                .Value = "N°"
            End With



            With ws.Cell(filaEmpieza, 2)
                .Value = "Nombre completo"
            End With
            ''-----------------------------------------------------------------
            ''variables auxiliares 
            ''-----------------------------------------------------------------
            columnasContador = 3
            Dim indicadorEsPrimeraFila As Integer = 0

            Dim colorImpar As String = "#EFF1F3"
            Dim colorPar As String = "#E7F5FF"
            Dim indicaParImpar As Integer = 0
            Dim indicaEsPrimero As Integer = 0
            For Each mes In sqlFe
                indicadorEsPrimeraFila += 1
                ''-----------------------------------------------------------------
                ''crear los col span 
                ''-----------------------------------------------------------------
                indicaParImpar += 1
                If indicadorEsPrimeraFila = 1 Then
                    limInf = 3
                    limSup = limInf + (From dias In mes.semanas Select cantida = dias.dias.Count).Sum() - 1
                Else
                    limInf = limSup + 1
                    limSup = limInf + (From dias In mes.semanas Select cantida = dias.dias.Count).Sum() - 1
                End If


                With ws.Range(ws.Cell(filaEmpieza - 2, limInf), ws.Cell(filaEmpieza - 2, limSup))
                    .Merge()
                    .Value = mes.nombreMEs
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                With ws.Range(ws.Cell(filaEmpieza - 2, limInf), ws.Cell(sqlObjectAlumnos.Count + 12, limSup))
                    If indicaParImpar Mod 2 = 0 Then
                        .Style.Fill.BackgroundColor = XLColor.FromHtml(colorImpar)
                    Else
                        .Style.Fill.BackgroundColor = XLColor.FromHtml(colorPar)
                    End If
                End With

                posMes = 0
                For Each dias In mes.semanas ''---
                    posMes += 1
                    indicaEsPrimero += 1

                    If indicaEsPrimero = 1 Then
                        limInf_1 = 3
                        limSup_1 = limInf_1 + dias.dias.Count - 1
                    Else
                        limInf_1 = limSup_1 + 1
                        limSup_1 = limInf_1 + dias.dias.Count - 1
                    End If
                    ''----------------
                    With ws.Range(ws.Cell(filaEmpieza - 1, limInf_1), ws.Cell(filaEmpieza - 1, limSup_1))
                        .Merge()
                        .Value = " Se: " & posMes '& " (" & dias.codSem & ") "
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        .Style.Font.FontSize = 7
                    End With
                    ''----------------



                    For Each di In dias.dias ''-


                        ws.Cell(filaEmpieza, CInt(di.col)).Value = Convert.ToString(di.nombreDia) & " (" & Convert.ToString(di.dia) & ")"


                        ws.Column(CInt(di.col)).Width = 3.45
                        ''-----------------------------------------------------------------
                        ''dar estilo  a las celda de fechas 
                        ''-----------------------------------------------------------------
                        With ws.Cell(filaEmpieza, CInt(di.col))
                            .Style.Alignment.WrapText = True
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End With


                    Next ''-


                Next ''---


            Next



            ''-----------------------------------------------------------------
            '' pintar los nombre de  los alumnos  
            ''-----------------------------------------------------------------
            Dim columnasAlumnos As Integer = 2 'posicion de la columna de alumnos
            Dim columnasIndie As Integer = 1 ' pocision de la columna de id de la fila
            Dim idALumno As Integer = 0
            filaEmpieza = 12
            For Each oALumnos In sqlObjectAlumnos
                idALumno += 1
                filaEmpieza += 1

                With ws.Cell(filaEmpieza, columnasAlumnos)
                    .Value = oALumnos.nombreCmpleto.ToString
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Font.FontSize = 8
                End With

                With ws.Cell(filaEmpieza, columnasIndie)
                    .Value = idALumno.ToString()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Font.FontSize = 8
                End With

                Dim posColumnas As Integer = 0

                For Each oDetalleAlumno In oALumnos.detalle
                    posColumnas = (From col In dtFechas.AsEnumerable() Where col("fecha") = oDetalleAlumno.fecha Select col("col")).DefaultIfEmpty(0).First()
                    If posColumnas <> 0 Then

                        With ws.Cell(filaEmpieza, posColumnas)
                            .Value = oDetalleAlumno.cantidad

                            If CInt(oDetalleAlumno.cantidad) >= 3 Then
                                .Style.Font.FontColor = XLColor.Red
                            End If

                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        End With

                    End If

                Next

            Next



            ''-----------------------------------------------------------------
            ''poner la altura de la fila 
            ''-----------------------------------------------------------------
            ws.Row(12).Height = 30
            ''-----------------------------------------------------------------
            ''Poner ancho de  las columnas  para el numero de filas 
            ''-----------------------------------------------------------------
            ws.Column(1).Width = 4

            ''-----------------------------------------------------------------
            ''Poner ancho de  las columnas para la columna 2  del nommbre  del alumno
            ''-----------------------------------------------------------------
            ws.Column(2).Width = 36
            Dim empiezanFilas As Integer = 12


            If limSup = 0 Then
                limSup = filaEmpieza
            End If

            With ws.Range(ws.Cell(empiezanFilas, 1), ws.Cell(filaEmpieza, limSup))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With


            workbook.Save()
            ''

            Return rutaREpositorioTemporales


        Catch ex As Exception

        End Try
    End Function



#End Region
#Region "Reporte enfermeria"

#End Region


#Region "procedimientos  "
    Private Sub SetearFechaDefault()
        Try
            tbRep6_FechaInicio.Text = ObtenerPrimerDiaSemana(Date.Now)

            tbRep6_FechaFin.Text = Date.Now.Day.ToString & "/" & Date.Now.Month.ToString & "/" & Date.Now.Year.ToString
            ddlRep6_NumeroPintar.SelectedValue = 2
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetearFechaDefaultReporteAcumuladosHora()
        Try
            Dim instance As DateTime = Date.Now
            Dim months As Integer = -1
            Dim returnValue As DateTime
            returnValue = instance.AddMonths(months)
            tbRep6_FechaInicio.Text = returnValue.Day.ToString & "/" & returnValue.Month.ToString & "/" & returnValue.Year.ToString

            tbRep6_FechaFin.Text = Date.Now.Day.ToString & "/" & Date.Now.Month.ToString & "/" & Date.Now.Year.ToString
            ddlRep6_NumeroPintar.SelectedValue = 12
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Funciones "
    Public Shared Function ObtenerPrimerDiaSemana(ByVal diaSemana As DateTime) As String

        Dim primerDiaSemana As DateTime = diaSemana.Date
        While primerDiaSemana.DayOfWeek <> DayOfWeek.Monday
            primerDiaSemana = primerDiaSemana.AddDays(-1)
        End While

        Return primerDiaSemana.Day & "/" & primerDiaSemana.Month & "/" & primerDiaSemana.Year

    End Function
#End Region


#Region " reporte de enfermeria telefonos de hijos "



    Private Sub crearReporteTelefonos(ByVal p_PeriodoAcademico As Integer, _
                                      ByVal p_codNivel As Integer, _
                                      ByVal p_subnivel As Integer, _
                                     ByVal p_codGrado As Integer, _
                                   ByVal p_codAula As Integer, _
                                  ByVal p_codPersona As Integer)
        Try
            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

            Dim dc As New Dictionary(Of String, Object)


            Dim dstTelefonos As New DataSet
            Dim Dtniveles As New System.Data.DataTable

            Dim nParam As String = "USP_lisRepTelefonosEmergencia"

            dc("p_PeriodoAcademico") = p_PeriodoAcademico
            dc("p_codNivel") = p_codNivel
            dc("p_subnivel") = p_subnivel
            dc("p_codGrado") = p_codGrado
            dc("p_codAula") = p_codAula
            dc("p_codPersona") = p_codPersona
            dstTelefonos = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam)

            Dtniveles = dstTelefonos.Tables(1)


            Dim sqlTelf = From det In dstTelefonos.Tables(0).AsEnumerable() Group det By _
                          nombreAlumno = det("nombreCompl"), _
                          codAlumno = det("codAlumno"), _
                          tipoSangre = det("nombreTipoSangre"), _
                          preguntarPor = det("preguntarPor"), _
                          nombreGrado = det("nombreGrado"), _
                          nombreNivel = det("nombreNivel"), _
                          nombreSubNivel = det("nombreSubNivel"), _
                          nombreAula = det("nombreAula"), _
                          telefEmergencia = det("telefEmergencia") _
                          Into detalle = Group _
                          Select New With {.telefEmergencia = telefEmergencia, .nombreAlumno = nombreAlumno, _
                                           .tipoSangre = tipoSangre, _
                                           .preguntarPor = preguntarPor, _
                              .alergias = (From alerg In detalle.AsEnumerable() _
                                                 Select New alergia With {.nombreAlergia = alerg("nombreAlergia")}) _
                                                     .Aggregate(Function(prev, curr) New alergia With { _
                                                                 .nombreAlergia = prev.nombreAlergia & " " & curr.nombreAlergia}).nombreAlergia, _
                             .enfermedades = (From enfer In detalle.AsEnumerable() Select New alergia With { _
                                            .nombreAlergia = enfer("nombreEnfermedad")}).Aggregate(Function(prev, curr) _
                                                  New alergia With {.nombreAlergia = _
                                                      prev.nombreAlergia & " " & curr.nombreAlergia}).nombreAlergia, _
                             .medicamentos = (From med In detalle.AsEnumerable() _
                                                             Select New medicamento With {.nombreMed = med("nombreMed")}) _
                                                             .Aggregate(Function(prev, curr) New medicamento With {.nombreMed = prev.nombreMed & " " & curr.nombreMed}).nombreMed, _
                                                             .telfPadre = (From pad In detalle.AsEnumerable() Where CInt(pad("codParent")) = 1 _
                                                                           Select New With {.telCasa = pad("telCasa") & " " & pad("celCasa") & " " & pad("telTrab") & " " & pad("celTrab")}), _
                                                              .telfMama = (From mad In detalle.AsEnumerable() Where CInt(mad("codParent")) = 2 _
                                                                            Select New With {.telCasa = mad("telCasa") & " " & mad("celCasa") & " " & mad("telTrab") & " " & mad("celTrab")})}

            'telCasa()
            'celCasa()
            'telTrab()
            'celTrab()


            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("reporteCargoEntrega")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")


            Dim sqlNivels = (From niveles In Dtniveles.AsEnumerable() Select niveles("nombreNivel")).Distinct().Aggregate(Function(prev, curr) prev & "," & curr)
            Dim sqlSubNivel = (From subNIvel In Dtniveles.AsEnumerable() Select subNIvel("nombreSubNivel")).Distinct().Aggregate(Function(prev, curr) prev & "," & curr)
            Dim sqlSubGrado = (From grados In Dtniveles.AsEnumerable() Select grados("nombreGrado")).Distinct().Aggregate(Function(prev, curr) prev & "," & curr)
            Dim sqlAulas = (From aulas In Dtniveles.AsEnumerable() Select aulas("nombreAula")).Distinct().Aggregate(Function(prev, curr) prev & "," & curr)




            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)


            Dim filasEpiezan As Integer = 1

            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)




            Dim ws = workbook.Worksheet(1)

            With ws.Range(ws.Cell(filasEpiezan, 1), ws.Cell(filasEpiezan, 10))
                .Merge()
                .Value = "RELACIÓN DE ALUMNOS"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
                .Style.Font.FontSize = 20
            End With
            filasEpiezan += 1
            With ws.Range(ws.Cell(filasEpiezan, 1), ws.Cell(filasEpiezan, 10))
                .Merge()
                .Value = "Fecha de Reporte : " & Date.Now.Day.ToString().PadLeft(2, "0") & "/" & Date.Now.Month.ToString().PadLeft(2, "0") & "/" & Date.Now.Year
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
                .Style.Font.FontSize = 20
            End With
            filasEpiezan += 1
            filasEpiezan += 1


            With ws.Range(ws.Cell(filasEpiezan, 1), ws.Cell(filasEpiezan, 10))
                .Merge()
                .Value = "Nivel :" & sqlNivels & " ,Subnivel:" & sqlSubNivel & " ,Grado: " & sqlSubGrado & " ,Aula: " & sqlAulas
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True

            End With
            filasEpiezan += 1
            filasEpiezan += 1

            With ws.Cell(filasEpiezan, 1)
                .Value = "N°"
            End With

            With ws.Cell(filasEpiezan, 2)
                .Value = "ALUMNOS"
            End With
            With ws.Cell(filasEpiezan, 3)
                .Value = "SANGRE"
            End With
            With ws.Cell(filasEpiezan, 4)
                .Value = "TLF.PADRE"
            End With
            With ws.Cell(filasEpiezan, 5)
                .Value = "TLF.MADRE"
            End With
            With ws.Cell(filasEpiezan, 6)
                .Value = "TLF.EMERGENCIA"
            End With

            With ws.Cell(filasEpiezan, 7)
                .Value = "PREGUNTAR POR"
            End With

            With ws.Cell(filasEpiezan, 8)
                .Value = "ALERGIA"
            End With
            With ws.Cell(filasEpiezan, 9)
                .Value = "ENFERMEDAD"
            End With
            With ws.Cell(filasEpiezan, 10)
                .Value = "MEDICAMENTO"
            End With

            Dim tempFilas As Integer = filasEpiezan
            With ws.Range(ws.Cell(filasEpiezan, 1), ws.Cell(filasEpiezan, 10))
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#95B3D7")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            End With

            filasEpiezan += 1
            Dim contadoFilas As Integer = 0
            For Each oObj In sqlTelf
                contadoFilas += 1
                ws.Cell(filasEpiezan, 1).Value = contadoFilas
                ws.Cell(filasEpiezan, 1).Style.Font.FontSize = 9
                ws.Cell(filasEpiezan, 2).Value = oObj.nombreAlumno
                ws.Cell(filasEpiezan, 2).Style.Font.FontSize = 9

                ws.Cell(filasEpiezan, 3).Value = oObj.tipoSangre
                If oObj.telfPadre.Count > 0 Then
                    ws.Cell(filasEpiezan, 4).Value = oObj.telfPadre(0).telCasa
                    ws.Cell(filasEpiezan, 4).Style.Font.FontSize = 9
                End If

                If oObj.telfMama.Count > 0 Then
                    ws.Cell(filasEpiezan, 5).Value = oObj.telfMama(0).telCasa
                    ws.Cell(filasEpiezan, 5).Style.Font.FontSize = 9
                End If



                ws.Cell(filasEpiezan, 6).Value = oObj.telefEmergencia
                ws.Cell(filasEpiezan, 6).Style.Font.FontSize = 9
                ws.Cell(filasEpiezan, 7).Value = oObj.preguntarPor
                ws.Cell(filasEpiezan, 7).Style.Font.FontSize = 9


                ws.Cell(filasEpiezan, 8).Value = oObj.alergias
                ws.Cell(filasEpiezan, 8).Style.Font.FontSize = 9
                ws.Cell(filasEpiezan, 9).Value = oObj.enfermedades
                ws.Cell(filasEpiezan, 9).Style.Font.FontSize = 9
                ws.Cell(filasEpiezan, 10).Value = oObj.medicamentos
                ws.Cell(filasEpiezan, 10).Style.Font.FontSize = 9



                filasEpiezan += 1
            Next


            ws.Column(1).Width = 3.71
            ws.Column(2).Width = 36.71
            ws.Column(3).Width = 6.29
            ws.Column(4).Width = 62.86
            ws.Column(5).Width = 60
            ws.Column(6).Width = 80.43
            ws.Column(7).Width = 44.44
            ws.Column(8).Width = 60
            ws.Column(9).Width = 60
            ws.Column(10).Width = 60



            With ws.Range(ws.Cell(tempFilas, 1), ws.Cell(filasEpiezan, 10))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With

            workbook.Save()


            Dim downloadBytes1 As Byte()
            downloadBytes1 = File.ReadAllBytes(rutaREpositorioTemporales)

            ' Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteHouse.xlsx" + "; size=" + downloadBytes1.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes1)
            Response.End()

        Catch ex As Exception

        End Try
    End Sub


#Region " clases "


    Public Class alergia
        Public nombreAlergia As String
    End Class
    Public Class enfermedad
        Public nombreEnfermedad As String
    End Class
    Public Class medicamento
        Public nombreMed As String
    End Class
    Public Class parentezco
        Public nombrePar As String
    End Class

#End Region
#End Region
End Class
