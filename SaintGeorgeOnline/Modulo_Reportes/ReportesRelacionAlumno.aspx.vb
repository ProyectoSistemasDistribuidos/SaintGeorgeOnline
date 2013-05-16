Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices.Marshal
Imports Microsoft.Office.Interop.Excel
Imports SaintGeorgeOnline_BusinessLogic.ModuloCursos
Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Imports CrystalDecisions
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports ClosedXML
Imports ClosedXML.Excel

'PruebA
''' <summary>  
''' Modulo de Reportes Relación Alumno
''' </summary>
''' <remarks>
''' Código del Modulo:    1
''' Código de la Opción:  1
''' </remarks>
Partial Class ModuloReportes_ReportesRelacionAlumno
    Inherits System.Web.UI.Page
    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1
    'Edgar

    Private Shared int_HA_Left As Integer = 2 ' Alineación Horizontal Izquierda
    Private Shared int_HA_Center As Integer = 3 ' Alineación Horizontal Centrada
    Private Shared int_HA_Right As Integer = 4 ' Alineación Horizontal Derecha

    Private Shared int_VA_Top As Integer = 1 ' Alineación Vertical Superior
    Private Shared int_VA_Middle As Integer = 2 ' Alineación Vertical Media
    Private Shared int_VA_Bottom As Integer = 3 ' Alineación Vertical Inferior

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Master.MostrarTitulo("Reportes de Relación de Alumno")

            btnReporteExportar.Attributes.Add("onclick", "ShowMyModalPopup()")

            If Not Page.IsPostBack Then
                cargarComboPrimaria()
                cargarListaReportes()
                cargarListaPresentacion()

                pnlReporte1.Visible = True
                pnlReporte2.Visible = False
                pnlReporte3.Visible = False
                pnlReporte4.Visible = False
                pnlPrimaria.Visible = False
                pnlExportarRegistroNotas.Visible = False
                pnlReporteAsistencia.Visible = False
                pnlReporteRetiro.Visible = False
                pnlReporteFotos.Visible = False
                pnlDescriptores.Visible = False
                pnlReporteMatricula.Visible = False
                pnlReporteLibretas.Visible = False
                pnlReporteComparacion.Visible = False
                pnlReporteGradoPrimariaBimestre.Visible = False

                cargarComboAniosAcademicos()
                cargarComboNivel_Rep1()

                cargarComboAsignacionAula()
                cargarComboBimestres()


                chkAll1.Checked = False


                ddlAnioAcademico_RepMatricula.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                ddlAnioAcademicoLibretas.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar

                limpiarCombos(ddlBuscarAulaFoto, False, True)
                limpiarCombos(ddlAlumno_Asist, True, False)
                limpiarCombos(ddlRep1_SubNivel, False, True)
                limpiarCombos(ddlRep1_Grado, True, False)
                limpiarCombos(ddlRep1_Aula, True, False)


            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Private Sub cargarComboAsignacionCursos()

        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 
        Dim int_CodigoTrabajador As Integer = int_CodigoUsuario
        Dim int_CodigoAsignacionAula As Integer = dpsSalonDescriptores.SelectedValue
        Dim obj_BL_AsignacionCursos As New bl_AsignacionCursos

        ''FUN_LIS_AsignacionCursosPorAsgignacionAula
        Dim ds_Lista As DataSet = obj_BL_AsignacionCursos.FUN_LIS_AsignacionCursosPorAsgignacionAula(int_CodigoAsignacionAula, 0, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(dpdCursosDescriptores, ds_Lista, "CodigoCurso", "DescCursoCompuesta", False, False)

    End Sub

    Protected Sub ddlBuscarGradoFoto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If ddlBuscarGradoFoto.SelectedValue > 0 Then
            cargarComboBuscarAulaFoto()
        End If

    End Sub

    Protected Sub btnReporteExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim usp_mensaje As String = ""
            If validarCombos() Then
                Exportar()
            Else
                MostrarAlertas(usp_mensaje)
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub lstReportes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstReportes.SelectedIndexChanged
        Try
            cargarListaPresentacion()
            mostrarPanelParametros()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Sub mostrarSoloEstePanel(ByVal str_nombrePanel As String)
        Try
            For Each ctr As Control In Form.Controls
                If TypeOf (ctr) Is Panel Then
                    If ctr.ID <> str_nombrePanel Then
                        ctr.Visible = False
                    End If
                End If
            Next
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Protected Sub lstPresentacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPresentacion.SelectedIndexChanged

        mostrarPanelParametros()

    End Sub

    Protected Sub ddlAula_Asist_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlAula_Asist.SelectedValue <> 0 Then
                cargarComboAlumno()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' rutina para cargar el combo de primaria 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub cargarComboPrimaria()
        Try


            '''
            'cmbPimariaComparacionBimestre
            Dim dst As New DataSet
            dst = New bl_rep_libretaNotas().listarGradosPrimaria()

            Dim dtComparacion As New System.Data.DataTable
            dtComparacion = dst.Tables(0)

            cmbPimariaComparacionBimestre.DataSource = dtComparacion
            cmbPimariaComparacionBimestre.DataTextField = "GD_Descripcion"
            cmbPimariaComparacionBimestre.DataValueField = "GD_CodigoGrado"
            cmbPimariaComparacionBimestre.DataBind()

            cmbReportePrimariaGradoBimestre.DataSource = dtComparacion
            cmbReportePrimariaGradoBimestre.DataTextField = "GD_Descripcion"
            cmbReportePrimariaGradoBimestre.DataValueField = "GD_CodigoGrado"
            cmbReportePrimariaGradoBimestre.DataBind()




            '            GD_CodigoGrado()
            '4:
            '            GD_Descripcion()
            '            First()
        Catch ex As Exception
        Finally

        End Try
    End Sub
    ''' <summary>
    ''' Carga el combo con la lista de Aulas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboBuscarAulaFoto()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(ddlBuscarGradoFoto.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAulaFoto, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub
    ''' <summary>
    ''' Valida los combos: Nivel, SubNivel,Grado y Aulas. 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     16/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarCombos() As Boolean

        Dim result As Boolean = True

        If pnlReporte1.Visible = True Then
            If ddlRep1_Nivel.SelectedValue = 0 Or ddlAnioAcademico1.SelectedValue = 0 Then
                Me.Master.MostrarMensajeAlert("Debe de seleccionar un Año Académico y un Nivel.")
                result = False
            ElseIf ddlRep1_Nivel.SelectedValue <> 0 And ddlRep1_SubNivel.SelectedValue = 0 Then
                Me.Master.MostrarMensajeAlert("Debe de seleccionar un SubNivel.")
                result = False
                'ElseIf ddlRep1_SubNivel.SelectedValue <> 0 And ddlRep1_Grado.SelectedValue = 0 Then
                '    Me.Master.MostrarMensajeAlert("Debe de seleccionar un Grado.")
                '    result = False
                'ElseIf ddlRep1_Grado.SelectedValue <> 0 And ddlRep1_Aula.SelectedValue = 0 Then
                '    Me.Master.MostrarMensajeAlert("Debe de seleccionar un Aula.")
                '    result = False
            End If
        ElseIf pnlReporte2.Visible = True Then
            If ddlAnioAcademico2.SelectedValue = 0 Then
                Me.Master.MostrarMensajeAlert("Debe de seleccionar un Año Académico.")
                result = False
                'Else
                '    If ddlRep1_Nivel1.SelectedValue <> 0 And ddlMes.SelectedValue = 0 Then
                '        Me.Master.MostrarMensajeAlert("Debe de seleccionar el Mes.")
                '        result = False
                '    ElseIf ddlRep1_SubNivel1.SelectedValue <> 0 And ddlMes.SelectedValue = 0 Then
                '        Me.Master.MostrarMensajeAlert("Debe de seleccionar el Mes.")
                '        result = False
                '    ElseIf ddlRep1_SubNivel1.SelectedValue <> 0 And ddlMes.SelectedValue = 0 Then
                '        Me.Master.MostrarMensajeAlert("Debe de seleccionar el Mes.")
                '        result = False
                '    ElseIf ddlRep1_Grado1.SelectedValue <> 0 And ddlMes.SelectedValue = 0 Then
                '        Me.Master.MostrarMensajeAlert("Debe de seleccionar el Mes.")
                '        result = False
                '    ElseIf ddlRep1_Aula1.SelectedValue <> 0 And ddlMes.SelectedValue = 0 Then
                '        Me.Master.MostrarMensajeAlert("Debe de seleccionar el Mes.")
                '        result = False
                'ElseIf ddlRep1_Nivel1.SelectedValue = 0 And ddlMes.SelectedValue <> 0 Then
                '    Me.Master.MostrarMensajeAlert("Debe de seleccionar un Nivel.")
                '    result = False
                'ElseIf ddlRep1_Nivel1.SelectedValue <> 0 And ddlRep1_SubNivel1.SelectedValue = 0 And ddlMes.SelectedValue <> 0 Then
                '    Me.Master.MostrarMensajeAlert("Debe de seleccionar un SubNivel.")
                '    result = False
                'ElseIf ddlRep1_Nivel1.SelectedValue <> 0 And ddlRep1_SubNivel1.SelectedValue <> 0 And ddlRep1_Grado1.SelectedValue = 0 And ddlMes.SelectedValue <> 0 Then
                '    Me.Master.MostrarMensajeAlert("Debe de seleccionar un Grado.")
                '    result = False
                'ElseIf ddlRep1_Nivel1.SelectedValue <> 0 And ddlRep1_SubNivel1.SelectedValue <> 0 And ddlRep1_Grado1.SelectedValue <> 0 And ddlRep1_Aula1.SelectedValue = 0 And ddlMes.SelectedValue <> 0 Then
                '    Me.Master.MostrarMensajeAlert("Debe de seleccionar un Grado.")
                '    result = False
                'End If
            End If
        ElseIf pnlReporteAsistencia.Visible = True Then
            If ddlAnioAcademico_Asist.SelectedValue = 0 Then
                Me.Master.MostrarMensajeAlert("Debe de seleccionar un Año Académico.")
                result = False
            End If
            If ddlBimestre_Asist.SelectedValue = 0 Then
                Me.Master.MostrarMensajeAlert("Debe de seleccionar un Bimestre.")
                result = False
            End If
            If ddlAula_Asist.SelectedValue = 0 Then
                Me.Master.MostrarMensajeAlert("Debe de seleccionar un Aula.")
                result = False
            End If
        ElseIf pnlReporteRetiro.Visible = True Then
            If ddlAnio_ret.SelectedValue = 0 Then
                Me.Master.MostrarMensajeAlert("Debe de seleccionar un Año Académico.")
                result = False
            End If
        ElseIf pnlReporteFotos.Visible = True Then
            If ddlBuscarAnioAcademicoFoto.SelectedValue = 0 Then
                Me.Master.MostrarMensajeAlert("Debe de seleccionar un Año Académico.")
                result = False
            End If
            If ddlBuscarGradoFoto.SelectedValue = 0 Then
                Me.Master.MostrarMensajeAlert("Debe de seleccionar un Grado.")
                result = False
            End If
        End If
        Return result
    End Function

#End Region

#Region "Reportes 1"

#Region "Eventos"
    ''' <summary>
    ''' Cada combo llama a otro combo en cascada. 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     16/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub ddlRep1_Nivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_Nivel.SelectedValue <> 0 Then
                cargarComboSubNivel_Rep1()
            Else
                limpiarCombos(ddlRep1_SubNivel, False, True)
                limpiarCombos(ddlRep1_Grado, True, False)
                limpiarCombos(ddlRep1_Aula, True, False)
                'limpiarCombos(ddlRep1_Grado, False, True)
                'limpiarCombos(ddlRep1_Aula, False, True)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddlRep1_Nivel1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_Nivel1.SelectedValue <> 0 Then
                cargarComboSubNivel_Rep1()
            Else
                limpiarCombos(ddlRep1_SubNivel1, True, False)
                limpiarCombos(ddlRep1_Grado1, True, False)
                limpiarCombos(ddlRep1_Aula1, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddlRep1_SubNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_SubNivel.SelectedValue <> 0 Then
                cargarComboGrado_Rep1()
            Else
                limpiarCombos(ddlRep1_Grado, True, False)
                limpiarCombos(ddlRep1_Aula, True, False)
                'limpiarCombos(ddlRep1_Grado, False, True)
                'limpiarCombos(ddlRep1_Aula, False, True)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddlRep1_SubNivel1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_SubNivel1.SelectedValue <> 0 Then
                cargarComboGrado_Rep1()
            Else
                limpiarCombos(ddlRep1_Grado1, True, False)
                limpiarCombos(ddlRep1_Aula1, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddlRep1_Grado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_Grado.SelectedValue <> 0 Then
                cargarComboAulas_Rep1()
            Else
                limpiarCombos(ddlRep1_Aula, True, False) ' False, True)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddlRep1_Grado1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_Grado1.SelectedValue <> 0 Then
                cargarComboAulas_Rep1()
            Else
                limpiarCombos(ddlRep1_Aula1, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Carga la información de los Bimestres,con la lista de grados activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     19/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAlumno()

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Alumnos.FUN_LIS_AlumnosMidTermReport(ddlAula_Asist.SelectedValue, Me.Master.Obtener_CodigoPeriodoEscolar, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlAlumno_Asist, ds_Lista, "CodigoAlumno", "NombreCompleto", True, False)

    End Sub

    ''' <summary>
    ''' Carga los combos: Nivel, SubNivel,Grado, Aulas, AniosAcademicos(1 y 2) y meses en estado activo.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboNivel_Rep1() ''reportePrimero

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep1_Nivel, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlRep1_Nivel1, ds_Lista, "Codigo", "Descripcion", True, False)



    End Sub

    Private Sub cargarComboSubNivel_Rep1()
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        If pnlReporte1.Visible = True Then
            Dim int_CodigoNivel As Integer = ddlRep1_Nivel.SelectedValue
            Dim obj_BL_SubNiveles As New bl_Subniveles
            Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            Controles.llenarCombo(ddlRep1_SubNivel, ds_Lista, "Codigo", "Descripcion", False, True)
        ElseIf pnlReporte2.Visible = True Then
            Dim int_CodigoNivel1 As Integer = ddlRep1_Nivel1.SelectedValue
            Dim obj_BL_SubNiveles As New bl_Subniveles
            Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(int_CodigoNivel1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            Controles.llenarCombo(ddlRep1_SubNivel1, ds_Lista, "Codigo", "Descripcion", True, False)
        End If
    End Sub
    Private Sub cargarComboGrado_Rep1()
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        If pnlReporte1.Visible = True Then
            Dim int_CodigoSubNivel As Integer = ddlRep1_SubNivel.SelectedValue
            Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(int_CodigoSubNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            Controles.llenarCombo(ddlRep1_Grado, ds_Lista, "Codigo", "Descripcion", True, False) ' False, True)
        ElseIf pnlReporte2.Visible = True Then
            Dim int_CodigoSubNivel As Integer = ddlRep1_SubNivel1.SelectedValue
            Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(int_CodigoSubNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            Controles.llenarCombo(ddlRep1_Grado1, ds_Lista, "Codigo", "Descripcion", True, False)
        ElseIf pnlReporteFotos.Visible = True Then
            Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            Controles.llenarCombo(ddlBuscarGradoFoto, ds_Lista, "Codigo", "Descripcion", False, True)
            limpiarCombos(ddlBuscarAulaFoto, True, False)
        End If
    End Sub
    
    Private Sub cargarComboAulas_Rep1()
        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        If pnlReporte1.Visible = True Then
            Dim int_CodigoGrado As Integer = ddlRep1_Grado.SelectedValue
            Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            Controles.llenarCombo(ddlRep1_Aula, ds_Lista, "Codigo", "Descripcion", True, False) ' False, True)
        ElseIf pnlReporte2.Visible = True Then
            Dim int_CodigoGrado As Integer = ddlRep1_Grado1.SelectedValue
            Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            Controles.llenarCombo(ddlRep1_Aula1, ds_Lista, "Codigo", "Descripcion", True, False)
        ElseIf pnlReporteAsistencia.Visible = True Then
            Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            Controles.llenarCombo(ddlAula_Asist, ds_Lista, "Codigo", "DescAulaCompuesta", False, True)
      
        End If
    End Sub
    Private Sub cargarComboAniosAcademicos()
        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 47)

        Try
            If pnlReporte1.Visible = True Then
                Controles.llenarCombo(ddlAnioAcademico1, ds_Lista, "Codigo", "Descripcion", False, True)
                ddlAnioAcademico1.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
            ElseIf pnlReporte2.Visible = True Then
                Controles.llenarCombo(ddlAnioAcademico2, ds_Lista, "Codigo", "Descripcion", False, True)
                ddlAnioAcademico2.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                cargarComboMeses()
            ElseIf pnlReporteAsistencia.Visible = True Then
                Controles.llenarCombo(ddlAnioAcademico_Asist, ds_Lista, "Codigo", "Descripcion", False, True)
            ElseIf pnlReporteRetiro.Visible = True Then
                Controles.llenarCombo(ddlAnio_ret, ds_Lista, "Codigo", "Descripcion", False, True)
            ElseIf pnlReporteFotos.Visible = True Then
                Controles.llenarCombo(ddlBuscarAnioAcademicoFoto, ds_Lista, "Codigo", "Descripcion", False, True)

            End If

            Controles.llenarCombo(ddlAnioAcademico_RepMatricula, ds_Lista, "Codigo", "Descripcion", True, False)
            Controles.llenarCombo(ddlAnioAcademicoLibretas, ds_Lista, "Codigo", "Descripcion", False, False)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cargarComboMeses()
        Dim ds_Lista As DataSet
        ds_Lista = Controles.ListaMeses()
        Controles.llenarCombo(ddlMes, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub
    ''' <summary>
    ''' Recibe mensajes y los deriva a otro metodo que los visualizara cno animación de JQuery
    ''' </summary>
    ''' <param name="str_alertas">Mensaje que se quiere visualizar</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarAlertas(ByVal str_alertas As String)
        MostrarSexyAlertBox(str_alertas, "Alert")
    End Sub
    ''' <summary>
    ''' Carga la lista de reportes, presentación y muestra el panel de parámetros.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               EdgarChang
    ''' Fecha de Creación:     12/01/2012 
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarListaReportes()

        Dim int_CodigoTipoReporte As Integer = 5 ' Reportes de  Relación de Alumnos
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

        Dim dt As System.Data.DataTable = CType(ViewState("ListaReportes"), DataSet).Tables(1)
        Dim int_CodigoReporte As Integer = lstReportes.SelectedValue

        Dim dv As DataView = dt.DefaultView

        With dv
            .RowFilter = "1=1 and CodigoReporte = " & int_CodigoReporte
        End With

        lstPresentacion.DataSource = dv
        lstPresentacion.DataTextField = "Descripcion"
        lstPresentacion.DataValueField = "CodigoDetalle"
        lstPresentacion.DataBind()

        lstPresentacion.SelectedIndex = 0

    End Sub

    Private Sub mostrarPanelParametros()

        pnlReporte1.Visible = False
        pnlReporte2.Visible = False
        pnlReporte3.Visible = False
        pnlReporte4.Visible = False
        pnlPrimaria.Visible = False
        pnlDescriptores.Visible = False
        pnlExportarRegistroNotas.Visible = False
        pnlReporteAsistencia.Visible = False
        pnlReporteRetiro.Visible = False
        pnlReporteFotos.Visible = False
        pnlReporteMatricula.Visible = False
        pnlReporteLibretas.Visible = False
        pnlReporteComparacion.Visible = False
        pnlReporteGradoPrimariaBimestre.Visible = False



        If lstPresentacion.SelectedValue = 26 Or lstPresentacion.SelectedValue = 27 Or lstPresentacion.SelectedValue = 28 _
        Or lstPresentacion.SelectedValue = 29 Or lstPresentacion.SelectedValue = 30 Or lstPresentacion.SelectedValue = 31 Then

            '' Reporte : Relacion Alumnos (Firma de losPadres, Relación de Teléfonos, Por Sexo, Por Control y Por Salón)
            pnlReporte1.Visible = True
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            'pnlPrimaria.Visible = False
            'pnlDescriptores.Visible = False
            'pnlExportarRegistroNotas.Visible = False
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False

        ElseIf lstPresentacion.SelectedValue = 32 Then ' Reporte : Relacion Alumno (Cumpleaños del Mes)

            'pnlReporte1.Visible = False
            pnlReporte2.Visible = True
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            'pnlPrimaria.Visible = False
            'pnlDescriptores.Visible = False
            'limpiarCombos(ddlRep1_SubNivel1, True, False)
            'limpiarCombos(ddlRep1_Grado1, True, False)
            'limpiarCombos(ddlRep1_Aula1, True, False)
            cargarComboAniosAcademicos()
            'pnlExportarRegistroNotas.Visible = False
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False

        ElseIf lstPresentacion.SelectedValue = 44 Or _
                lstPresentacion.SelectedValue = 45 Or _
                lstPresentacion.SelectedValue = 49 Then ' Reporte : Consolidado senior, primaria, inicial

            'pnlExportarRegistroNotas.Visible = False
            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            pnlReporte3.Visible = True
            'pnlReporte4.Visible = False
            'pnlPrimaria.Visible = False
            'pnlDescriptores.Visible = False
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False
            If lstPresentacion.SelectedValue = 49 Then ' Inicial

                cargarComboAsignacionAulaConsolidado(3)

            ElseIf lstPresentacion.SelectedValue = 45 Then ' primaria

                cargarComboAsignacionAulaConsolidado(4)

            ElseIf lstPresentacion.SelectedValue = 44 Then 'secundaria

                cargarComboAsignacionAulaConsolidado(2)

            End If

        ElseIf lstPresentacion.SelectedValue = 46 Or _
                lstPresentacion.SelectedValue = 47 Or _
                lstPresentacion.SelectedValue = 48 Then ' Reporte : Libreta incial, primaria, secundaria

            'pnlExportarRegistroNotas.Visible = False
            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            pnlPrimaria.Visible = True
            'pnlDescriptores.Visible = False
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False

            If lstPresentacion.SelectedValue = 46 Then ' Libreta Inicial
                cargarComboAsignacionAulaLibretas(3)
            ElseIf lstPresentacion.SelectedValue = 47 Then ' Libreta primaria
                cargarComboAsignacionAulaLibretas(4)
            ElseIf lstPresentacion.SelectedValue = 48 Then 'Libreta secundaria
                cargarComboAsignacionAulaLibretas(2)
            End If

        End If


        If lstPresentacion.SelectedValue = 50 Then

            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            'pnlPrimaria.Visible = False
            pnlDescriptores.Visible = True
            cargarComboAsignacionCursos()



            'pnlExportarRegistroNotas.Visible = False
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False

        End If
        'If lstPresentacion.SelectedValue = 28 Then

        '    pnlReporte1.Visible = False
        '    pnlReporte2.Visible = False
        '    pnlReporte3.Visible = False
        '    pnlReporte4.Visible = False
        '    pnlPrimaria.Visible = False
        '    pnlDescriptores.Visible = False
        '    pnlExportarRegistroNotas.Visible = False
        '    pnlReporteAsistencia.Visible = False

        'End If
        If lstPresentacion.SelectedValue = 51 Then ''cargar salones primaria

            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            'pnlPrimaria.Visible = False
            'pnlDescriptores.Visible = False
            pnlExportarRegistroNotas.Visible = True
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False

            cargarSalonPrimariaInicial(4)
            cargarComboAsignacionCursosReporteRevision()

            'else if @p_TipoNota = 3 begin -- Inicial          
            'select @i = 1, @top = 3        
            'End
            'else if @p_TipoNota = 4 begin -- Primaria           
            'select @i = 4, @top = 8        

        End If

        If lstPresentacion.SelectedValue = 52 Then ''cargar salones inicial
            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            'pnlPrimaria.Visible = False
            'pnlDescriptores.Visible = False
            pnlExportarRegistroNotas.Visible = True
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False

            cargarSalonPrimariaInicial(3)
            cargarComboAsignacionCursosReporteRevision()

        End If

        If lstPresentacion.SelectedValue = 53 Or lstPresentacion.SelectedValue = 54 Or lstPresentacion.SelectedValue = 55 Then ''cargar salones inicial
            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            'pnlPrimaria.Visible = False
            'pnlDescriptores.Visible = False
            'pnlExportarRegistroNotas.Visible = False
            pnlReporteAsistencia.Visible = True
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False
            cargarComboAniosAcademicos()
            cargarComboAulas_Rep1()
            cargarComboBimestres()
            ddlAnioAcademico_Asist.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        End If


        If lstPresentacion.SelectedValue = 56 Then ''cargar alumnos retirados
            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            'pnlPrimaria.Visible = False
            'pnlDescriptores.Visible = False
            'pnlExportarRegistroNotas.Visible = False
            'pnlReporteAsistencia.Visible = False
            pnlReporteRetiro.Visible = True
            'pnlReporteMatricula.Visible = False
            cargarComboAniosAcademicos()
            ddlAnio_ret.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        End If
      

        If lstReportes.SelectedValue = 30 Then


            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            pnlPrimaria.Visible = True
            'pnlDescriptores.Visible = False
            'pnlExportarRegistroNotas.Visible = False
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False


            If lstPresentacion.SelectedValue = 57 Then

                cargarComboAsignacionAulaLibretas(1)

            ElseIf lstPresentacion.SelectedValue = 58 Then
                cargarComboAsignacionAulaLibretas(2)

            End If



        End If

        ''reporte class summary report
        If lstReportes.SelectedValue = 31 Then


            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            pnlPrimaria.Visible = True
            'pnlDescriptores.Visible = False
            'pnlExportarRegistroNotas.Visible = False
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False


            If lstPresentacion.SelectedValue = 59 Then

                cargarComboAsignacionAulaLibretas(1)

            ElseIf lstPresentacion.SelectedValue = 60 Then
                cargarComboAsignacionAulaLibretas(2)
            End If

        End If
        If lstReportes.SelectedValue = 33 Then
            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            pnlPrimaria.Visible = True
            'pnlDescriptores.Visible = False
            'pnlExportarRegistroNotas.Visible = False
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            cargarComboAsignacionAulaLibretas(2)
            'pnlReporteMatricula.Visible = False
        End If
        If lstReportes.SelectedValue = 36 Then
            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            'pnlPrimaria.Visible = False
            'pnlDescriptores.Visible = False
            'pnlExportarRegistroNotas.Visible = False
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            'pnlReporteMatricula.Visible = False
        End If

        If lstReportes.SelectedValue = 38 Then
            'pnlReporte1.Visible = False
            'pnlReporte2.Visible = False
            'pnlReporte3.Visible = False
            'pnlReporte4.Visible = False
            'pnlPrimaria.Visible = False
            'pnlDescriptores.Visible = False
            'pnlExportarRegistroNotas.Visible = False
            'pnlReporteAsistencia.Visible = False
            'pnlReporteRetiro.Visible = False
            pnlReporteMatricula.Visible = True
        End If

        If lstPresentacion.SelectedValue = 79 Then
            ' cargarComboAsignacionAulaConsolidado(3)
        End If


        If lstReportes.SelectedValue = 40 Then
            ''  pnlReporte3.Visible = True

            pnlReporteComparacion.Visible = True
        End If

        If lstReportes.SelectedValue = 41 Then
            ''  pnlReporte3.Visible = True
            pnlReporteGradoPrimariaBimestre.Visible = True
        End If


        If lstPresentacion.SelectedValue = 81 Then


            pnlReporteGradoPrimariaBimestre.Visible = True
        End If

        If lstPresentacion.SelectedValue = 83 Then ''cargar alumnos Fotos

            pnlReporteFotos.Visible = True

            limpiarCombos(ddlBuscarGradoFoto, False, True)
            cargarComboAniosAcademicos()
            cargarComboGrado_Rep1()
            ddlBuscarAnioAcademicoFoto.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar


        End If

        If lstReportes.SelectedValue = 39 Then

            pnlReporteLibretas.Visible = True

            Dim int_tipo As Integer = 0

            If lstPresentacion.SelectedValue = 76 Then
                int_tipo = 3 ' inicial
            ElseIf lstPresentacion.SelectedValue = 77 Then
                int_tipo = 4 ' primaria
            ElseIf lstPresentacion.SelectedValue = 78 Then
                int_tipo = 2 ' secundaria
            End If

            If int_tipo > 0 Then
                cargarComboAsignacionAula(int_tipo)
            End If

        End If

    End Sub

    ''' <summary>
    ''' Limpia los combos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               EdgarChang
    ''' Fecha de Creación:     12/01/2012 
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList, ByVal bool_Todos As Boolean, ByVal bool_Seleccione As Boolean)
        Controles.limpiarCombo(combo, bool_Todos, bool_Seleccione)
    End Sub

    ''' <summary>
    ''' Exporta los datos en formato xls (Excel)
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     12/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________
    ''' </remarks>
    ''' 
    Private Sub Exportar()





        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoUsuarioLogueado
        Dim dt_Lista As New System.Data.DataTable
        Dim ds_Lista As New DataSet
        Dim obj_bl_ModuloReporte As New bl_RelacionAlumnos
        Dim obj_BL_Alumnos As New bl_Alumnos

        ''lstPresentacion
        Dim int_TipoReporte As Integer = lstReportes.SelectedValue 'Tipo reporte
        Dim int_PresentacionReporte As Integer = lstPresentacion.SelectedValue 'Tipo reporte

        Dim str_TituloReporte As String = "" 'Titulo reporte
        Dim int_CodigoGrado As Integer
        Dim int_CodigoAula As Integer

        'LLenado de reporte
        Dim NombreArchivo As String = ""
        Dim RutaMadre As String = ""
        Dim downloadBytes As Byte()
        Dim int_CodAnio1 As Integer
        Dim int_CodAnio2 As Integer
        Dim int_CodMes As Integer
        Dim int_codNivel As Integer
        Dim int_codGdo As Integer
        Dim int_codAula As Integer



        '' '' ''
        '' ''NombreArchivo = crearLibretaInicial1(18, 3, "")


        ' '' ''NombreArchivo = crearConsolidadoEvaluacion(Convert.ToInt32(ddlAsignacionAula3.SelectedValue), Convert.ToInt32(ddlBimestre3.SelectedValue))

        '' ''downloadBytes = File.ReadAllBytes(NombreArchivo)
        '' ''Response.Charset = ""
        '' ''Response.ContentType = "binary/octet-stream"
        '' ''Response.AddHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoEvaluacionInicial.xlsx" + "; size=" + downloadBytes.Length.ToString())
        '' ''Response.Flush()
        '' ''Response.BinaryWrite(downloadBytes)
        '' ''Response.End()
        '' '' ''



        If int_PresentacionReporte = 76 Then
            Dim idCod As String = ""
            For Each filaGridView As GridViewRow In GridView1.Rows

                If filaGridView.RowType = DataControlRowType.DataRow Then

                    If CType(filaGridView.FindControl("chk"), System.Web.UI.WebControls.CheckBox).Checked Then
                        idCod &= CType(filaGridView.FindControl("lblCodigoAlumno"), System.Web.UI.WebControls.Label).Text & ","
                    End If

                End If
            Next

            NombreArchivo = crearLibretaInicial1(ddlSalonLibretas.SelectedValue, ddlBimestreLibretas.SelectedValue, idCod)

            downloadBytes = File.ReadAllBytes(NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoEvaluacionInicial.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()
            Exit Sub
        End If


        If int_PresentacionReporte = 81 Then


            'cmbReportePrimariaGradoBimestre

            'cmbBimestreReportePrimariaGradoBimestre

            NombreArchivo = ExportarAlumnosAoC(cmbReportePrimariaGradoBimestre.SelectedValue, cmbBimestreReportePrimariaGradoBimestre.SelectedValue)

            downloadBytes = File.ReadAllBytes(NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoEvaluacionInicial.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()
            Exit Sub

        End If


        ''
        If int_PresentacionReporte = 77 Then
            Dim idCod As String = ""
            For Each filaGridView As GridViewRow In GridView1.Rows

                If filaGridView.RowType = DataControlRowType.DataRow Then

                    If CType(filaGridView.FindControl("chk"), System.Web.UI.WebControls.CheckBox).Checked Then
                        idCod &= CType(filaGridView.FindControl("lblCodigoAlumno"), System.Web.UI.WebControls.Label).Text & ","
                    End If

                End If
            Next

            NombreArchivo = crearLibretaPrimariaUnaSolaHoja(ddlSalonLibretas.SelectedValue, ddlBimestreLibretas.SelectedValue, idCod)

            downloadBytes = File.ReadAllBytes(NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoEvaluacionInicial.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()
            Exit Sub
        End If

        If int_TipoReporte = 16 Then ' Reporte : 

            int_CodigoGrado = ddlRep1_Grado.SelectedValue
            int_CodigoAula = ddlRep1_Aula.SelectedValue
            int_CodAnio1 = ddlAnioAcademico1.SelectedValue
            int_CodAnio2 = ddlAnioAcademico2.SelectedValue


            Dim int_Nivel As Integer = 0
            Dim int_subNIvel As Integer = 0

            int_Nivel = ddlRep1_Nivel.SelectedValue
            int_subNIvel = ddlRep1_SubNivel.SelectedValue

            int_CodMes = 1
            If int_PresentacionReporte = 26 Then ' Presentación : Por Salon
                str_TituloReporte = "RelacionAlumnosXsalon"
                ds_Lista = obj_bl_ModuloReporte.FUN_REP_REP_RelacionAlumnoXsalon(int_CodAnio1, int_Nivel, int_subNIvel, int_CodigoGrado, int_CodigoAula, _
                                                                      int_CodigoUsuario, int_CodigoTipoUsuario, _
                                                                      cod_Modulo, cod_Opcion)
                dt_Lista = ds_Lista.Tables(0)
                If Not dt_Lista.Rows.Count > 0 Then
                    Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
                    Exit Sub
                End If
                dt_Lista = ds_Lista.Tables(0)
                NombreArchivo = ExportarReportePorSalon(dt_Lista, str_TituloReporte)
            ElseIf int_PresentacionReporte = 27 Then ' Presentación : Por Control
                str_TituloReporte = "RelacionAlumnosXControl"
                ds_Lista = obj_bl_ModuloReporte.FUN_REP_RelacionAlumnoXControl(int_CodAnio1, int_CodigoGrado, int_CodigoAula, _
                                                                      int_CodigoUsuario, int_CodigoTipoUsuario, _
                                                                      cod_Modulo, cod_Opcion)
                dt_Lista = ds_Lista.Tables(0)
                If Not dt_Lista.Rows.Count > 0 Then
                    Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
                    Exit Sub
                End If
                dt_Lista = ds_Lista.Tables(0)
                NombreArchivo = ExportarReportePorControl(dt_Lista, str_TituloReporte)
            ElseIf int_PresentacionReporte = 28 Then ' Presentación : Por Sexo
                str_TituloReporte = "RelacionAlumnosXsexo"
                ds_Lista = obj_bl_ModuloReporte.FUN_REP_REP_RelacionAlumnoXsexo(int_CodAnio1, int_CodigoGrado, int_CodigoAula, _
                                                                      int_CodigoUsuario, int_CodigoTipoUsuario, _
                                                                      cod_Modulo, cod_Opcion)
                dt_Lista = ds_Lista.Tables(0)
                If Not dt_Lista.Rows.Count > 0 Then
                    Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
                    Exit Sub
                End If
                dt_Lista = ds_Lista.Tables(0)
                NombreArchivo = ExportarReportePorSexo(dt_Lista, str_TituloReporte)
            ElseIf int_PresentacionReporte = 29 Then ' Presentación : Por Procedencia 
                dt_Lista = ds_Lista.Tables(0)
                If Not dt_Lista.Rows.Count > 0 Then
                    Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
                    Exit Sub
                End If
            ElseIf int_PresentacionReporte = 30 Then ' Presentación : Relación de Teléfono
                str_TituloReporte = "RelacionAlumnosXtelefono"
                ds_Lista = obj_bl_ModuloReporte.FUN_REP_REP_RelacionAlumnoXtelefono(int_CodAnio1, int_CodigoGrado, int_CodigoAula, _
                                                                       int_CodigoUsuario, int_CodigoTipoUsuario, _
                                                                       cod_Modulo, cod_Opcion)
                dt_Lista = ds_Lista.Tables(0)
                If Not dt_Lista.Rows.Count > 0 Then
                    Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
                    Exit Sub
                End If
                dt_Lista = ds_Lista.Tables(0)
                NombreArchivo = ExportarReporteRelacionTelefono(dt_Lista, str_TituloReporte)
            ElseIf int_PresentacionReporte = 31 Then ' Presentación : Firmas de los Padres
                str_TituloReporte = "RelacionAlumnoXfirmaPadre"
                ds_Lista = obj_bl_ModuloReporte.FUN_REP_RelacionAlumnoXfirmasPadres(int_CodAnio1, int_CodigoGrado, int_CodigoAula, _
                                                                 int_CodigoUsuario, int_CodigoTipoUsuario, _
                                                                 cod_Modulo, cod_Opcion)
                dt_Lista = ds_Lista.Tables(0)
                If Not dt_Lista.Rows.Count > 0 Then
                    Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
                    Exit Sub
                End If
                dt_Lista = ds_Lista.Tables(0)
                NombreArchivo = ExportarReporteFirmaPadre(dt_Lista, str_TituloReporte)
            ElseIf int_PresentacionReporte = 32 Then ' Presentación : Cumpleaños Por Mes
                str_TituloReporte = "RelacionAlumnoXcumpleaños"
                int_CodMes = ddlMes.SelectedValue
                int_codNivel = ddlRep1_Nivel1.SelectedValue
                int_codGdo = ddlRep1_Grado1.SelectedValue
                int_codAula = ddlRep1_Aula1.SelectedValue
                ds_Lista = obj_bl_ModuloReporte.FUN_REP_RelacionAlumnoXcumpleaniosMes(int_CodAnio2, int_CodMes, _
                                                                                     int_codNivel, int_codGdo, int_codAula, _
                                                                 int_CodigoUsuario, int_CodigoTipoUsuario, _
                                                                 cod_Modulo, cod_Opcion)
                dt_Lista = ds_Lista.Tables(0)
                If Not dt_Lista.Rows.Count > 0 Then
                    Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
                    Exit Sub
                End If
                dt_Lista = ds_Lista.Tables(0)
                NombreArchivo = ExportarReporteCumpleaniosXMes(dt_Lista, str_TituloReporte)
            ElseIf int_PresentacionReporte = 56 Then ' Reporte de Alumnos Retirados
                str_TituloReporte = "RelacionAlumnosRetirados"
                'int_CodMes = ddlMes.SelectedValue
                'int_codNivel = ddlRep1_Nivel1.SelectedValue
                'int_codGdo = ddlRep1_Grado1.SelectedValue
                'int_codAula = ddlRep1_Aula1.SelectedValue
                ds_Lista = obj_BL_Alumnos.FUN_REP_AlumnosRetirados(ddlAnio_ret.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 0)

                Dim dt As System.Data.DataTable = New System.Data.DataTable("ListaExportar")

                dt = Datos.agregarColumna(dt, "N°", "String")
                dt = Datos.agregarColumna(dt, "Nombre Completo", "String")
                dt = Datos.agregarColumna(dt, "DNI", "String")
                dt = Datos.agregarColumna(dt, "Codigo Educando", "String")
                dt = Datos.agregarColumna(dt, "Grado", "String")
                dt = Datos.agregarColumna(dt, "Aula", "String")
                dt = Datos.agregarColumna(dt, "Fecha Retiro", "String")
                dt = Datos.agregarColumna(dt, "Motivos", "String")
                dt = Datos.agregarColumna(dt, "Colegio", "String")
                dt = Datos.agregarColumna(dt, "Observaciones", "String")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("N°") = cont
                    auxDR.Item("Nombre Completo") = dr.Item("NombreCompleto").ToString
                    auxDR.Item("DNI") = dr.Item("DNI").ToString
                    auxDR.Item("Codigo Educando") = dr.Item("CodigoEducando").ToString
                    auxDR.Item("Grado") = dr.Item("Grado").ToString
                    auxDR.Item("Aula") = dr.Item("Aula").ToString
                    auxDR.Item("Fecha Retiro") = dr.Item("FechaRetiro").ToString
                    auxDR.Item("Motivos") = dr.Item("Motivos").ToString
                    auxDR.Item("Colegio") = dr.Item("Colegio").ToString
                    auxDR.Item("Observaciones") = dr.Item("Observaciones").ToString
                    dt.Rows.Add(auxDR)
                    cont += 1
                Next


                dt_Lista = ds_Lista.Tables(0)
                If Not dt_Lista.Rows.Count > 0 Then
                    Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
                    Exit Sub
                End If
                dt_Lista = ds_Lista.Tables(0)
                NombreArchivo = Exportacion.ExportarReporteAlumnosRetirados(dt, "Alumnos Retirados") 'ExportarReporteCumpleaniosXMes(dt_Lista, str_TituloReporte)

            ElseIf int_PresentacionReporte = 83 Then ' Reporte de Fotos Alumnos 
                str_TituloReporte = "RelacionAlumnosFotos"

                ds_Lista = obj_BL_Alumnos.FUN_LIS_FotosAlumnos(ddlBuscarAnioAcademicoFoto.SelectedValue, ddlBuscarGradoFoto.SelectedValue, ddlBuscarAulaFoto.SelectedValue, "", "", "", "", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                'NombreArchivo = Exportacion.ExportarReporteFotosAlumnos_Html(ds_Lista, "Alumnos Retirados")
                Dim reporte_html As String = ""
                reporte_html = Exportacion.ExportarReporteFotosAlumnos_Html(ds_Lista, "Alumnos Retirados")
                Session("Exportaciones_FotosAlumnosHtml") = reporte_html
                ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionFotoAlumno_html();</script>", False)

            End If
        ElseIf int_TipoReporte = 25 Then ' Reporte : Consolidado de Notas

            If int_PresentacionReporte = 44 Then ' secundaria

                Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
                Dim int_CodigoAsignacionAula As Integer = ddlAsignacionAula3.SelectedValue
                Dim int_CodigoBimestre As Integer = ddlBimestre3.SelectedValue

                Dim int_TipoRep As Integer = 1 ' update 05/12/2012

                ds_Lista = obl_rep_libretaNotas.FUN_LIS_REP_ConsolidadoNotasSecundaria( _
                          int_CodigoAsignacionAula, int_CodigoBimestre, int_TipoRep, _
                          int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                If Not (ds_Lista.Tables(0).Rows.Count > 0 Or ds_Lista.Tables(1).Rows.Count > 0 Or ds_Lista.Tables(2).Rows.Count > 0) Then
                    Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
                    Exit Sub
                End If

                str_TituloReporte = "Consolidado" ' Nombre de la hoja 
                NombreArchivo = ExportarReporteconsolidadoSecundaria(ds_Lista, str_TituloReporte)

            ElseIf int_PresentacionReporte = 45 Then ' primaria

                NombreArchivo = crearReportePrimaria(Convert.ToInt32(ddlAsignacionAula3.SelectedValue), Convert.ToInt32(ddlBimestre3.SelectedValue))
                downloadBytes = File.ReadAllBytes(NombreArchivo)
                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoEvaluacionPrimaria.xlsx" + "; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()

            ElseIf int_PresentacionReporte = 49 Then




                NombreArchivo = crearConsolidadoEvaluacion(Convert.ToInt32(ddlAsignacionAula3.SelectedValue), Convert.ToInt32(ddlBimestre3.SelectedValue))
                downloadBytes = File.ReadAllBytes(NombreArchivo)
                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "ConsolidadoEvaluacionInicial.xlsx" + "; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()

            End If

        ElseIf int_TipoReporte = 26 Then ' Reporte : Libretas

            If int_PresentacionReporte = 48 Then ' Secundaria

                Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
                Dim int_CodigoAsignacionAula As Integer = ddlSalonRepPrimaria.SelectedValue
                Dim int_CodigoBimestre As Integer = cmbBimestrePrimaria.SelectedValue
                Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar

                Dim ds_ListaAlumnos As DataSet
                ds_ListaAlumnos = obl_rep_libretaNotas.FUN_LIS_REP_AlumnosLibreta( _
                    int_CodigoAsignacionAula, int_CodigoBimestre, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                NombreArchivo = ExportarReporteLibretaSecundaria(ds_ListaAlumnos, int_CodigoAsignacionAula, int_CodigoBimestre, int_CodigoAnioAcademico, _
                                                                 int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            End If
            If int_PresentacionReporte = 47 Then 'Primaria 

                NombreArchivo = crearLibretaPrimaria(CInt(ddlSalonRepPrimaria.SelectedValue), CInt(cmbBimestrePrimaria.SelectedValue))
                downloadBytes = File.ReadAllBytes(NombreArchivo)
                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporte.xlsx" + "; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()

            End If
            If int_PresentacionReporte = 46 Then ' Inicial 



                NombreArchivo = crearLibretaInicial(CInt(ddlSalonRepPrimaria.SelectedValue), CInt(cmbBimestrePrimaria.SelectedValue))
                downloadBytes = File.ReadAllBytes(NombreArchivo)
                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporte.xlsx" + "; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()

            End If

        ElseIf int_TipoReporte = 29 Then ' Reporte : Asistencia

            If int_PresentacionReporte = 53 Then ' ATTENDANCE

                Dim obj_BL_Asistencia As New bl_Asistencia

                Dim ds As DataSet
                ds = obj_BL_Asistencia.FUN_REP_AsistenciaXBimestreMeses(Me.Master.Obtener_CodigoPeriodoEscolar, _
                                                                     ddlAula_Asist.SelectedValue, ddlBimestre_Asist.SelectedValue, _
                                                                      ddlAlumno_Asist.SelectedValue, int_CodigoUsuario, _
                                                                      int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                NombreArchivo = Exportacion.ExportarReporteAsistenciaXBimestreMeses(ds, "Attendance")
            ElseIf int_PresentacionReporte = 54 Then ' Control de Asistencia

                Dim obj_BL_Asistencia As New bl_Asistencia
                Dim ds As DataSet
                ds = obj_BL_Asistencia.FUN_REP_ControlXBimestre(Me.Master.Obtener_CodigoPeriodoEscolar, _
                                                                     ddlAula_Asist.SelectedValue, ddlBimestre_Asist.SelectedValue, _
                                                                      ddlAlumno_Asist.SelectedValue, int_CodigoUsuario, _
                                                                      int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                NombreArchivo = Exportacion.ExportarReporteControlAsistenciaXBimestre(ds, "Control_Asistencia")
            ElseIf int_PresentacionReporte = 55 Then ' Consolidado de Incidencias

                Dim obj_BL_Asistencia As New bl_Asistencia

                Dim ds As DataSet
                ds = obj_BL_Asistencia.FUN_REP_IncidenciasAsistencia(Me.Master.Obtener_CodigoPeriodoEscolar, _
                                                                     ddlAula_Asist.SelectedValue, ddlBimestre_Asist.SelectedValue, _
                                                                      ddlAlumno_Asist.SelectedValue, int_CodigoUsuario, _
                                                                      int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                NombreArchivo = Exportacion.ExportarReporteIncidenciasAsistencia(ds, "Incidencias de Asistencia")

            End If


        End If

        If int_PresentacionReporte = 50 Then
            Dim rutaExcelGenerado As String = ""


            If dpdCursosDescriptores.SelectedValue = 0 Then
                MsgBox("Seleccione un curso")
                Exit Sub
            End If
            rutaExcelGenerado = crearReporteRegistroDescriptoresI(dpdCursosDescriptores.SelectedValue, dpsSalonDescriptores.SelectedValue, dpdBimestreDescriptores.SelectedValue)

            downloadBytes = File.ReadAllBytes(rutaExcelGenerado)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteDescriptoresCursos.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()


        End If

        'lstPresentacion.SelectedValue = 51
        'lstPresentacion.SelectedValue = 52


        If lstPresentacion.SelectedValue = 51 Or lstPresentacion.SelectedValue = 52 Then
            Dim rutaExcelGenerado As String = ""


            If cmbCurso.SelectedValue = "" Then
                MsgBox("Seleccione un curso")
                Exit Sub
            End If
            rutaExcelGenerado = crearReporteLibretaRevisionInicial()

            downloadBytes = File.ReadAllBytes(rutaExcelGenerado)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteNotasComponenteIndicador.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()


        End If

        Dim rutaArchivo As String = ""
        If lstPresentacion.SelectedValue = 57 Then


            rutaArchivo = reporteComentarioPrimariaSecundaria(lstPresentacion.SelectedValue, ddlSalonRepPrimaria.SelectedValue, cmbBimestrePrimaria.SelectedValue)

            ''
            downloadBytes = File.ReadAllBytes(rutaArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteNotasComponenteIndicador.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()
        End If

        If lstPresentacion.SelectedValue = 58 Then

            rutaArchivo = reporteComentarioPrimariaSecundaria(lstPresentacion.SelectedValue, ddlSalonRepPrimaria.SelectedValue, cmbBimestrePrimaria.SelectedValue)
            ''
            downloadBytes = File.ReadAllBytes(rutaArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteNotasComponenteIndicador.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()

        End If


        If lstPresentacion.SelectedValue = 59 Or lstPresentacion.SelectedValue = 60 Then

            rutaArchivo = reporteClassSumamaryReport(ddlSalonRepPrimaria.SelectedValue, cmbBimestrePrimaria.SelectedValue)
            ''
            If rutaArchivo = "" Then
                Exit Sub
            End If

            downloadBytes = File.ReadAllBytes(rutaArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteClassSummaryReport.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()

        End If
        If lstPresentacion.SelectedValue = 61 Then

            rutaArchivo = crearReporteTutor(ddlSalonRepPrimaria.SelectedValue, cmbBimestrePrimaria.SelectedValue)
            ''
            If rutaArchivo = "" Then
                Exit Sub
            End If

            downloadBytes = File.ReadAllBytes(rutaArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteTutorReport.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()

        End If



        If lstReportes.SelectedValue = 33 Then

            If lstPresentacion.SelectedValue = 62 Then
                rutaArchivo = crearReporeConsolidadoSecundariaTipoCurso(cmbBimestrePrimaria.SelectedValue, 1, ddlSalonRepPrimaria.SelectedValue)
            End If
            If lstPresentacion.SelectedValue = 63 Then
                rutaArchivo = crearReporeConsolidadoSecundariaTipoCurso(cmbBimestrePrimaria.SelectedValue, 2, ddlSalonRepPrimaria.SelectedValue)
            End If
            If lstPresentacion.SelectedValue = 64 Then
                rutaArchivo = crearReporeConsolidadoSecundariaTipoCurso(cmbBimestrePrimaria.SelectedValue, 3, ddlSalonRepPrimaria.SelectedValue)
            End If
            If lstPresentacion.SelectedValue = 65 Then
                rutaArchivo = crearReporeConsolidadoSecundariaTipoCurso(cmbBimestrePrimaria.SelectedValue, 0, ddlSalonRepPrimaria.SelectedValue)
            End If

            'ddlSalonRepPrimaria
            'cmbBimestrePrimaria
            ''
            If rutaArchivo = "" Then
                Exit Sub
            End If

            downloadBytes = File.ReadAllBytes(rutaArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteTutorReport.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()

        End If

        If lstPresentacion.SelectedValue = 67 Then
            reporteGradoPronostico(14)
        End If

        'crearReporeConsolidadoSecundariaTipoCurso(ByVal codBimestre As Integer, ByVal tipoCursoCod As Integer, ByVal coAula As Integer) As String
        '62	33	Oficial
        '63	33	complementario
        '64	33	Interno
        '65	33	Todos

        '        TC_CodigoTipoCurso(TC_Descripcion)
        '1:      Oficial()
        '2:      Complementario()
        '3:      Interno()

        If lstReportes.SelectedValue = 38 Then ' Reporte de matricula

            Dim int_tipoMatricula As Integer = ddlTipoMatricula_RepMatricula.SelectedValue
            Dim int_CodigoPeriodoAcademico As Integer = ddlAnioAcademico_RepMatricula.SelectedValue

            ds_Lista = obj_bl_ModuloReporte.FUN_REP_RelacionAlumnosPorTipoMatricula( _
                int_CodigoPeriodoAcademico, int_tipoMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)


            Dim str_PeriodoAcademico As String = ddlAnioAcademico_RepMatricula.SelectedItem.ToString
            Dim str_TipoMatricula As String = ddlTipoMatricula_RepMatricula.SelectedItem.ToString

            NombreArchivo = ExportarReporteMatriculaPorTipo(ds_Lista, str_TituloReporte, str_PeriodoAcademico, str_TipoMatricula)
            NombreArchivo = NombreArchivo
            downloadBytes = File.ReadAllBytes(NombreArchivo)

            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=ReporteMatriculaPorTipo-" & Replace(str_TipoMatricula, " ", "") & "-" & str_PeriodoAcademico & ".xlsx; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()


        End If


        If lstReportes.SelectedValue = 39 Then ' Libretas

            If lstPresentacion.SelectedValue = 78 Then ' Secundaria

                Dim CodAnio As Integer = CInt(ddlAnioAcademicoLibretas.SelectedValue)
                Dim CodBimestre As Integer = CInt(ddlBimestreLibretas.SelectedValue)
                Dim CodSalon As Integer = CInt(ddlSalonLibretas.SelectedValue)
                Dim int_idioma As Integer = 2 ' ingles 

                'LLenado de reporte
                'Dim NombreArchivo As String = ""
                'Dim RutaMadre As String = ""
                'Dim downloadBytes As Byte()
                NombreArchivo = generarLibretaSecundaria(CodAnio, CodBimestre, CodSalon, NombreArchivo, int_idioma)

                Dim str_Nombre As String = ddlAnioAcademicoLibretas.SelectedItem.ToString
                downloadBytes = File.ReadAllBytes(NombreArchivo)

                Response.Clear()
                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=Libreta" & str_Nombre & ".xlsx; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()

            End If


        End If

        If lstReportes.SelectedValue = 40 Then

            Dim ruta As String = ""
            ruta = fExportarComparacionBimestre(cmbPimariaComparacionBimestre.SelectedValue, cmbBimestreA.SelectedValue, cmbBimestreB.SelectedValue)


            downloadBytes = File.ReadAllBytes(ruta)

            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=LibretacomparacionBimestre.xlsx; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()


        End If

        If lstReportes.SelectedValue = 41 Then

            Dim ruta As String = ""
            ruta = fExportarConsolidadoBimestreGrado(cmbReportePrimariaGradoBimestre.SelectedValue, cmbBimestreReportePrimariaGradoBimestre.SelectedValue)


            downloadBytes = File.ReadAllBytes(ruta)

            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=LibretacomparacionBimestre.xlsx; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()


        End If




        If lstReportes.SelectedValue = 36 Then

            Dim _MatriExcel As Byte()
            _MatriExcel = crearReporteJalados(NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + _MatriExcel.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(_MatriExcel)
            Response.End()
            Exit Sub
        End If
        If lstPresentacion.SelectedValue = 83 Then

        Else
            NombreArchivo = NombreArchivo & ".xls"
            RutaMadre = Server.MapPath(".")
            RutaMadre = RutaMadre.Replace("\Modulo_Reportes", "\Reportes\")
            downloadBytes = File.ReadAllBytes(RutaMadre & "\" & NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()

        End If
       
    End Sub


    ''' <summary>
    ''' reporte para realizar el reporte  de grados pronostico
    ''' </summary>
    ''' <param name="intGradoPronostico">codigo del grado pronostico </param>
    ''' <remarks></remarks>
    Sub reporteGradoPronostico(ByVal intGradoPronostico As Integer)

        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("PlantillaReporteGradosPronostico")
        Dim CrReport As New ReportDocument
        Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
        Dim dst As New System.Data.DataSet
        dst = obl_rep_libretaNotas.FUN_LIS_REP_ReportePronosticoGrado(intGradoPronostico)
        'dst = obl_rep_libretaNotas.FUN_LIS_REP_ReportePronosticoGrado(intGradoPronostico)
        Dim rutaReport As String = ""
        rutaReport = Server.MapPath(".")



        '' rutaReport = "D:\VSS_Projects S13\SaintGeorgeOnline\SaintGeorgeOnline\Reportes\ReportesCrystal\rptGradoPronostico.rpt"

        CrReport.Load(rutaPlantillas)
        CrReport.SetDataSource(dst.Tables(0))
        CrReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "reporteGradoPronostico")
        Try

        Catch ex As Exception
        Finally

        End Try



    End Sub



#Region "Notas B o C"
    Public Function ExportarAlumnosAoC(ByVal GD_CodigoGrado As Integer, ByVal BM_CodigoBimestre As Integer) As String

        Try


            Dim Obl_rep_libretaNotas As New bl_rep_libretaNotas
            Dim dcPAram As New Dictionary(Of String, Object)
            dcPAram.Add("GD_CodigoGrado", GD_CodigoGrado)


            dcPAram.Add("BM_CodigoBimestre", BM_CodigoBimestre)
            Dim dst As New DataSet

            Dim nParam As String = "REP_USP_NotasBoC"
            dst = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dcPAram, nParam)


            Dim lst As New List(Of Modulo_Notas_frmAlumosAoC_Curso)
            lst = FCrearColeccionModulo_Notas_frmAlumosAoC_Curso(dst.Tables(0))



            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current
            ''

            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaNotaBoC")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)


            Dim dstUbicacion As New System.Data.DataTable

            dstUbicacion = dst.Tables(1)


            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)



            Dim ws = workbook.Worksheet(1)



            ws.Range(ws.Cell(2, 2), ws.Cell(2, 2)).Value = "Bimestre " & Str(BM_CodigoBimestre)
            ws.Range(ws.Cell(2, 2), ws.Cell(2, 2)).Style.Font.Bold = True

            ws.Range(ws.Cell(4, 2), ws.Cell(4, 2 + dstUbicacion.Rows.Count)).Merge()
            ws.Range(ws.Cell(4, 2), ws.Cell(4, 2 + dstUbicacion.Rows.Count)).Value = " Alumnos con B o C "
            ws.Range(ws.Cell(4, 2), ws.Cell(4, 2 + dstUbicacion.Rows.Count)).Style.Font.Bold = True

            ws.Range(ws.Cell(4, 2), ws.Cell(4, 2 + dstUbicacion.Rows.Count)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            With ws.Range(ws.Cell(4, 2), ws.Cell(4, 2 + dstUbicacion.Rows.Count))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With


            Dim indiceCurso As Integer = 0
            Dim filaInicio As Integer = 6
            Dim cantidadAulas As Integer = 0
            Dim contInicial As Integer = 0

            Dim totalAlumnos As Integer = 0

            Dim columnas As Integer = 2

            Dim maxAlumnos As Integer = 0


            Dim empiezanFilas As Integer = 5

            Dim lastIndice As Integer = 0
            Dim lstMaximos As List(Of Integer)

            ws.Cell(5, 2).Value = "Grade " & (GD_CodigoGrado - 3).ToString()

            With ws.Cell(5, 2)
                .Style.Font.Bold = True
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

            End With



            'ub	AU_Descripcion	AAP_CodigoAsignacionAula
            '3:          Dolphins(33)

            Dim contadorFilas As Integer = 0
            For Each filas As DataRow In dstUbicacion.Rows
                ws.Cell(5, CInt(filas("ub").ToString())).Value = filas("AU_Descripcion").ToString()


                ws.Column(CInt(filas("ub").ToString())).Width = 37

                ws.Column(CInt(filas("ub").ToString())).Style.Font.Bold = True

                'With ws.Cell(5, CInt(filas("ub").ToString()))
                '    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                '    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                '    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                '    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                'End With



            Next





            For Each oModulo_Notas_frmAlumosAoC_Curso As Modulo_Notas_frmAlumosAoC_Curso In lst

                indiceCurso = 1
                contInicial += 1
                cantidadAulas = oModulo_Notas_frmAlumosAoC_Curso.maximo
                cantidadAulas -= 1
                'totalAlumnos =  From au In oModulo_Notas_frmAlumosAoC_Curso.lstModulo_Notas_frmAlumosAoC_Aula _
                '                Where au.LstModulo_Notas_frmAlumosAoC_Alumno.Count=

                columnas = 2

                ws.Range(ws.Cell(filaInicio, 2), ws.Cell((filaInicio + cantidadAulas), 2)).Merge()



                ws.Range(ws.Cell(filaInicio, 2), ws.Cell((filaInicio + cantidadAulas), 2)).Value = oModulo_Notas_frmAlumosAoC_Curso.NC_Descripcion

                ws.Range(ws.Cell(filaInicio, 2), ws.Cell((filaInicio + cantidadAulas), 2)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ws.Range(ws.Cell(filaInicio, 2), ws.Cell((filaInicio + cantidadAulas), 2)).Style.Font.Bold = True

                lstMaximos = New List(Of Integer)

                For Each oModulo_Notas_frmAlumosAoC_Aula As Modulo_Notas_frmAlumosAoC_Aula In oModulo_Notas_frmAlumosAoC_Curso.lstModulo_Notas_frmAlumosAoC_Aula
                    columnas += 1
                    totalAlumnos = 0

                    ' ws.Cell(5, columnas).Value = oModulo_Notas_frmAlumosAoC_Aula.AU_Descripcion

                    For Each oModulo_Notas_frmAlumosAoC_Alumno As Modulo_Notas_frmAlumosAoC_Alumno In oModulo_Notas_frmAlumosAoC_Aula.LstModulo_Notas_frmAlumosAoC_Alumno

                        ws.Cell(oModulo_Notas_frmAlumosAoC_Curso.empieza + totalAlumnos, oModulo_Notas_frmAlumosAoC_Aula.ubicacion).Value = oModulo_Notas_frmAlumosAoC_Alumno.nombre
                        totalAlumnos += 1

                    Next


                    lstMaximos.Add(totalAlumnos)
                Next
                filaInicio += oModulo_Notas_frmAlumosAoC_Curso.maximo


                contadorFilas += oModulo_Notas_frmAlumosAoC_Curso.maximo


            Next


            '.Style.Border.OutsideBorder = XLBorderStyleValues.Thin


            With ws.Range(ws.Cell(5, 2), ws.Cell(5 + contadorFilas, 2 + dstUbicacion.Rows.Count))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With



            workbook.Save()
            Return rutaREpositorioTemporales

        Catch ex As Exception

        End Try

    End Function

#Region "Clases Colecciones"

    Public Class Modulo_Notas_frmAlumosAoC_Curso

        Public ACA_CodigoAsignacionCurso As Integer
        Public NC_Descripcion As String
        Public lstModulo_Notas_frmAlumosAoC_Aula As New List(Of Modulo_Notas_frmAlumosAoC_Aula)
        Public maximo As Integer
        Public empieza As Integer


    End Class
    Public Class Modulo_Notas_frmAlumosAoC_Aula

        Public AAP_CodigoAsignacionAula As Integer
        Public AU_Descripcion As String
        Public ACA_CodigoAsignacionCurso As Integer
        Public LstModulo_Notas_frmAlumosAoC_Alumno As New List(Of Modulo_Notas_frmAlumosAoC_Alumno)
        Public ubicacion As Integer

    End Class
    Public Class Modulo_Notas_frmAlumosAoC_Alumno

        Public AL_CodigoAlumno As Integer
        Public nombre As String
        Public AAP_CodigoAsignacionAula As Integer


    End Class

#End Region

#Region "Metodos Crear Colecciones"



    Public Function FCrearColeccionModulo_Notas_frmAlumosAoC_Curso(ByVal dtSQlConsulta As System.Data.DataTable) As List(Of Modulo_Notas_frmAlumosAoC_Curso)
        Dim lstModulo_Notas_frmAlumosAoC_Curso As New List(Of Modulo_Notas_frmAlumosAoC_Curso)
        Dim oModulo_Notas_frmAlumosAoC_Curso As Modulo_Notas_frmAlumosAoC_Curso
        Dim tempACA_CodigoAsignacionCurso As Integer = 0

        Dim oModulo_Notas_frmAlumosAoC_Aula As Modulo_Notas_frmAlumosAoC_Aula
        Dim tempAAP_CodigoAsignacionAula As Integer = 0


        Dim oModulo_Notas_frmAlumosAoC_Alumno As Modulo_Notas_frmAlumosAoC_Alumno

        Dim ArrAula As DataRow()

        Dim arrAlumnos As DataRow()

        Dim lstMaximo As List(Of Integer)
        Dim lst As New List(Of Modulo_Notas_frmAlumosAoC_Curso)

        Dim contadorAlumnos As Integer = 0
        Try


            For Each filasSQl As DataRow In dtSQlConsulta.Rows

                If CInt(filasSQl("ACA_CodigoAsignacionCurso").ToString()) <> tempACA_CodigoAsignacionCurso Then
                    tempAAP_CodigoAsignacionAula = 0
                    oModulo_Notas_frmAlumosAoC_Curso = New Modulo_Notas_frmAlumosAoC_Curso

                    oModulo_Notas_frmAlumosAoC_Curso.ACA_CodigoAsignacionCurso = CInt(filasSQl("ACA_CodigoAsignacionCurso").ToString())
                    oModulo_Notas_frmAlumosAoC_Curso.NC_Descripcion = filasSQl("NC_Descripcion").ToString()



                    ArrAula = dtSQlConsulta.Select("ACA_CodigoAsignacionCurso =" & oModulo_Notas_frmAlumosAoC_Curso.ACA_CodigoAsignacionCurso)

                    lstMaximo = New List(Of Integer)
                    For Each filasAulas As DataRow In ArrAula


                        If CInt(filasAulas("AAP_CodigoAsignacionAula").ToString()) <> tempAAP_CodigoAsignacionAula Then

                            oModulo_Notas_frmAlumosAoC_Aula = New Modulo_Notas_frmAlumosAoC_Aula
                            oModulo_Notas_frmAlumosAoC_Aula.AAP_CodigoAsignacionAula = CInt(filasAulas("AAP_CodigoAsignacionAula").ToString())
                            oModulo_Notas_frmAlumosAoC_Aula.AU_Descripcion = filasAulas("AU_Descripcion").ToString()

                            oModulo_Notas_frmAlumosAoC_Aula.ubicacion = CInt(filasAulas("ub").ToString())
                            arrAlumnos = dtSQlConsulta.Select("AAP_CodigoAsignacionAula=" & oModulo_Notas_frmAlumosAoC_Aula.AAP_CodigoAsignacionAula & " and ACA_CodigoAsignacionCurso = " & oModulo_Notas_frmAlumosAoC_Curso.ACA_CodigoAsignacionCurso)
                            ' CInt(filasSQl("ub").ToString())


                            contadorAlumnos = 0
                            For Each filasAlumnos As DataRow In arrAlumnos

                                oModulo_Notas_frmAlumosAoC_Alumno = New Modulo_Notas_frmAlumosAoC_Alumno
                                oModulo_Notas_frmAlumosAoC_Alumno.nombre = filasAlumnos("nombre").ToString()
                                oModulo_Notas_frmAlumosAoC_Aula.LstModulo_Notas_frmAlumosAoC_Alumno.Add(oModulo_Notas_frmAlumosAoC_Alumno)
                                contadorAlumnos += 1

                            Next


                            oModulo_Notas_frmAlumosAoC_Curso.lstModulo_Notas_frmAlumosAoC_Aula.Add(oModulo_Notas_frmAlumosAoC_Aula)
                            lstMaximo.Add(contadorAlumnos)


                            tempAAP_CodigoAsignacionAula = oModulo_Notas_frmAlumosAoC_Aula.AAP_CodigoAsignacionAula
                        End If


                    Next

                    'oModulo_Notas_frmAlumosAoC_Curso.lstModulo_Notas_frmAlumosAoC_Aula.Add(oModulo_Notas_frmAlumosAoC_Aula)
                    oModulo_Notas_frmAlumosAoC_Curso.maximo = lstMaximo.Max()



                    lst.Add(oModulo_Notas_frmAlumosAoC_Curso)
                    tempACA_CodigoAsignacionCurso = oModulo_Notas_frmAlumosAoC_Curso.ACA_CodigoAsignacionCurso

                End If

            Next

            For indice As Integer = 0 To lst.Count - 1
                If indice = 0 Then
                    lst(indice).empieza = 6
                Else
                    lst(indice).empieza = lst(indice - 1).empieza + lst(indice - 1).maximo
                End If
            Next



            Return lst
        Catch ex As Exception

        End Try
    End Function
#End Region

#End Region


#Region "Exportar consolidado cursos secundaria tipo curso"

    Function crearReporeConsolidadoSecundariaTipoCurso(ByVal codBimestre As Integer, ByVal tipoCursoCod As Integer, ByVal coAula As Integer) As String

        Dim excel As New ApplicationClass
        Dim wbkWorkbook As Workbook
        Dim wshWorksheet As Worksheet
        Dim rng As Range
        Dim objColorRojo As Object = RGB(255, 0, 0) 'Rojo
        Dim objColorAzul As Object = RGB(34, 37, 194) 'Azul
        Dim obl_rep_libretaNotas As New bl_rep_libretaNotas

        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("plantillaReporteConsolidadoSecundariaTipoCurso")
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
        Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
        File.Copy(rutaPlantillas, rutaREpositorioTemporales)


        '62	33	Oficial
        '63	33	complementario
        '64	33	Interno
        '65	33	Todos
        '        TC_CodigoTipoCurso(TC_Descripcion)
        '1:      Oficial()
        '2:      Complementario()
        '3:      Interno()
        Try
            Dim dstConsolidado As New Data.DataSet
            dstConsolidado = obl_rep_libretaNotas.FUN_LIS_REP_ReporteConsolidadoEvaluacionSecundaria(codBimestre, coAula, tipoCursoCod)
            ''
            Dim abrBimestre As String = ""
            If codBimestre = 1 Then
                abrBimestre = "I"
            End If
            If codBimestre = 2 Then
                abrBimestre = "II"
            End If
            If codBimestre = 3 Then
                abrBimestre = "III"
            End If
            If codBimestre = 4 Then
                abrBimestre = "IV"
            End If
            Dim anio As String = ""
            ''



            Dim tipoCurso As String = ""
            If tipoCursoCod = 0 Then
                tipoCurso = "Asignaturas Totales"
            ElseIf tipoCursoCod = 1 Then
                tipoCurso = "Asignaturas Oficiales"

            ElseIf tipoCursoCod = 2 Then

                tipoCurso = "Asignaturas Complementario"
            ElseIf tipoCursoCod = 3 Then
                tipoCurso = "Asignaturas Interno"
            End If

            '' Dim lstpersonaConsolidado As New List(Of personaConsolidado)

            Dim ocontextoPersonaConsolidado As New contextoPersonaConsolidado



            ocontextoPersonaConsolidado = crearListaPersonaConsolidado(dstConsolidado.Tables(1), dstConsolidado.Tables(2), dstConsolidado.Tables(3), 2)

            Dim dtNumeroCursos As New System.Data.DataTable
            dtNumeroCursos = dstConsolidado.Tables(4)

            Dim int_numerosCursos As Integer = dtNumeroCursos.Rows.Count



            Dim lstcursoConsolidado As New List(Of cursoConsolidado)
            lstcursoConsolidado = crearListaCursos(dstConsolidado.Tables(0))


            Dim rutaPlantilla As String = ""
            rutaPlantilla = "C:\repConsolidadoSecundaria.xlsx"
            wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)
            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()
            wshWorksheet.Visible = True


            Dim acAprobados As Integer = 0
            Dim acDesaprobados As Integer = 0
            Dim acConducta As Integer = 0
            Dim filasEmpiezan As Integer = 7

            excel.Application.Range(CType(excel.ActiveSheet.Cells(6, 2), Range), CType(excel.ActiveSheet.Cells(6, lstcursoConsolidado.Count + 14), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(6, 2), Range), CType(excel.ActiveSheet.Cells(6, lstcursoConsolidado.Count + 14), Range)).RowHeight = 45
            excel.Application.Range(CType(excel.ActiveSheet.Cells(6, 2), Range), CType(excel.ActiveSheet.Cells(6, lstcursoConsolidado.Count + 14), Range)).Value = "Consolidado de evaluacion (" & tipoCurso & ")" & abrBimestre & " Bimestre - " & Year(Date.Now)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(6, 2), Range), CType(excel.ActiveSheet.Cells(6, lstcursoConsolidado.Count + 14), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(6, 2), Range), CType(excel.ActiveSheet.Cells(6, lstcursoConsolidado.Count + 14), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(6, 2), Range), CType(excel.ActiveSheet.Cells(6, lstcursoConsolidado.Count + 14), Range)).Font.Size = 25


            CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).Value = "Nro"
            CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, 2), Range).Value = "Apellidos y nombres "
            CType(excel.ActiveSheet.Cells(filasEmpiezan, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, 2), Range).Font.Bold = True

            For Each ocursoConsolidado As cursoConsolidado In lstcursoConsolidado
                CType(excel.ActiveSheet.Cells(filasEmpiezan, ocursoConsolidado.posCurso), Range).Value = "  " & ocursoConsolidado.nombreCurso
                CType(excel.ActiveSheet.Cells(filasEmpiezan, ocursoConsolidado.posCurso), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filasEmpiezan, ocursoConsolidado.posCurso), Range).WrapText = True
                CType(excel.ActiveSheet.Cells(filasEmpiezan, ocursoConsolidado.posCurso), Range).Orientation = 90

                excel.ActiveSheet.Cells.Columns(ocursoConsolidado.posCurso).ColumnWidth = 4

                CType(excel.ActiveSheet.Cells(filasEmpiezan, ocursoConsolidado.posCurso), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
                CType(excel.ActiveSheet.Cells(filasEmpiezan, ocursoConsolidado.posCurso), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter



            Next
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).Value = "  PROMDEIO"
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).WrapText = True
            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 3).ColumnWidth = 5
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 4), Range).Value = "  Puntaje total "
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 4), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 4), Range).WrapText = True
            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 4).ColumnWidth = 4
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 4), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).Value = "  Aprobados"
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).WrapText = True
            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 5).ColumnWidth = 4
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).Value = "  Desaprobados"
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).WrapText = True
            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 6).ColumnWidth = 4
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).Value = "  Conducta"

            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).WrapText = True

            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 7).ColumnWidth = 4
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

            ''
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 8), Range).Value = "  "
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 8), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 8), Range).WrapText = True
            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 8).ColumnWidth = 4
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 8), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 8), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 8), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            ''

            ''
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 9), Range).Value = "Inasist. Injustificadas"
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 9), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 9), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 9), Range).WrapText = True
            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 9).ColumnWidth = 4
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 9), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 9), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            ''

            ''
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 10), Range).Value = "Inasist. justificadas"
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 10), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 10), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 10), Range).WrapText = True
            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 10).ColumnWidth = 4
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 10), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 10), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            ''
            ''
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 11), Range).Value = "Tardanzas"
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 11), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 11), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 11), Range).WrapText = True
            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 11).ColumnWidth = 4
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 11), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 11), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            ''
            ''
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 12), Range).Value = "Tercio"
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 12), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 12), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 12), Range).WrapText = True
            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 12).ColumnWidth = 4
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 12), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 12), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            ''
            ''
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 13), Range).Value = "Orden de merito"
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 13), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 13), Range).Orientation = 90
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 13), Range).WrapText = True
            excel.ActiveSheet.Cells.Columns(lstcursoConsolidado.Count + 13).ColumnWidth = 4
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 13), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 13), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            ''


            '' 


            filasEmpiezan += 1
            Dim countFilas As Integer = 0

            For Each opersonaConsolidado As personaConsolidado In ocontextoPersonaConsolidado.LstPersonaConsolidado
                countFilas += 1
                CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).Value = countFilas
                CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(filasEmpiezan, 2), Range).Value = opersonaConsolidado.nombrePersona
                CType(excel.ActiveSheet.Cells(filasEmpiezan, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                For Each onotaBimestre As notaBimestre In opersonaConsolidado.lstNotasBimestre
                    If onotaBimestre.notaFinal < 10.5 Then
                        If onotaBimestre.notaFinal = 0 Then
                            CType(excel.ActiveSheet.Cells(filasEmpiezan, onotaBimestre.posCurso), Range).Value = ""
                        Else
                            CType(excel.ActiveSheet.Cells(filasEmpiezan, onotaBimestre.posCurso), Range).Value = onotaBimestre.notaFinal
                            CType(excel.ActiveSheet.Cells(filasEmpiezan, onotaBimestre.posCurso), Range).Font.Color = objColorRojo
                        End If
                        ''  CType(excel.ActiveSheet.Cells(filasEmpiezan, onotaBimestre.posCurso), Range).Font.ColorIndex=objColorRojo
                    Else
                        CType(excel.ActiveSheet.Cells(filasEmpiezan, onotaBimestre.posCurso), Range).Value = onotaBimestre.notaFinal
                        CType(excel.ActiveSheet.Cells(filasEmpiezan, onotaBimestre.posCurso), Range).Font.Color = objColorAzul
                    End If
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, onotaBimestre.posCurso), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    opersonaConsolidado.promedioCursos() ''

                    If opersonaConsolidado.promedioFinal = 0 Then
                        CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).Value = ""
                        CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    Else
                        CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).Value = Format(opersonaConsolidado.promedioFinal, "#.00")
                        CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    End If

                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    If opersonaConsolidado.promedioFinal < 10.5 Then

                        CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).Font.Color = objColorRojo

                    Else
                        CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 3), Range).Font.Color = objColorAzul

                    End If


                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 4), Range).Value = opersonaConsolidado.puntajeTotal
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).Value = opersonaConsolidado.cantidadAprobados
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).Value = opersonaConsolidado.cantidadDesAprobados
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).Value = opersonaConsolidado.notaConducta
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 8), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 9), Range).Value = opersonaConsolidado.cantidadFaltasInjustificadas
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 9), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 9), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 10), Range).Value = opersonaConsolidado.cantidadFaltasJustificadas
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 10), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 10), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 11), Range).Value = opersonaConsolidado.cantidadTardanzasInjusticadas + opersonaConsolidado.cantidadTardanzasJustificadas
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 11), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 11), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                Next

                Dim cantidadColumnasPintar2 As Integer = 3
                For columnas As Integer = 0 To lstcursoConsolidado.Count - 1


                    CType(excel.ActiveSheet.Cells(filasEmpiezan, cantidadColumnasPintar2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    cantidadColumnasPintar2 += 1
                Next

                acAprobados += opersonaConsolidado.cantidadAprobados
                acDesaprobados += opersonaConsolidado.cantidadDesAprobados
                acConducta += opersonaConsolidado.notaConducta





                filasEmpiezan += 1
            Next
            ocontextoPersonaConsolidado.fAsignarOrden()

            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).Value = CStr(acAprobados)
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            ''
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, lstcursoConsolidado.Count + 5), Range).Value = CStr((acAprobados / ocontextoPersonaConsolidado.LstPersonaConsolidado.Count) * 10) & "%"
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, lstcursoConsolidado.Count + 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            ''

            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).Value = CStr(acDesaprobados)
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 6), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            ''
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, lstcursoConsolidado.Count + 6), Range).Value = CStr(100 - (acAprobados / ocontextoPersonaConsolidado.LstPersonaConsolidado.Count) * 10) & "%"
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, lstcursoConsolidado.Count + 6), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


            ''



            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).Value = CStr(acConducta)
            CType(excel.ActiveSheet.Cells(filasEmpiezan, lstcursoConsolidado.Count + 7), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            Dim filasPonerOrdenMerito As Integer = 8

            For Each opersonaConsolidado As personaConsolidado In ocontextoPersonaConsolidado.LstPersonaConsolidado
                CType(excel.ActiveSheet.Cells(filasPonerOrdenMerito, lstcursoConsolidado.Count + 13), Range).Value = opersonaConsolidado.ordenMerito
                CType(excel.ActiveSheet.Cells(filasPonerOrdenMerito, lstcursoConsolidado.Count + 13), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(filasPonerOrdenMerito, lstcursoConsolidado.Count + 13), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(filasPonerOrdenMerito, lstcursoConsolidado.Count + 12), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                filasPonerOrdenMerito += 1
            Next



            ''
            'Dim contadorColumnas As Integer = 1
            'Dim matrizDinamica As Integer(,)

            ''Dim dc As Dictionary(Of Integer, List(Of Integer))

            Dim indicePersonas As Integer = 0
            Dim acumulador As Integer = 0

            Dim cantidadColumnas As Integer = 0

            cantidadColumnas = ocontextoPersonaConsolidado.LstPersonaConsolidado(0).lstNotasBimestre.Count - 1

            Dim posColumnas As Integer = 0

            Dim notaFinalAc As Integer = 0
            Dim notaMaxima As Integer = 0
            Dim notaMinima As Integer = 0


            Dim tempMaxima As Integer = 0
            Dim tempMinima As Integer = 0
            Dim numeroAprobados As Integer = 0
            Dim numeroDesAprobados As Integer = 0

            CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, 2), Range).Value = "Promedio del curso"
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 2, 2), Range).Value = "  No de alumnos aprobados "
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 3, 2), Range).Value = "  No de alumnos desaprobados "
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 4, 2), Range).Value = "     En cursos globales"
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 5, 2), Range).Value = "     En cursos oficiales"
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 6, 2), Range).Value = "     En cursos internos"

            CType(excel.ActiveSheet.Cells(filasEmpiezan + 7, 2), Range).Value = "%   de alumnos aprobados "
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 8, 2), Range).Value = "%   de alumnos desaprobados "

            CType(excel.ActiveSheet.Cells(filasEmpiezan + 9, 2), Range).Value = "Nota maxima"
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 10, 2), Range).Value = "Nota minima"




            Dim desTipoOficial As Integer = 0 ''1
            Dim desTipoComplementario As Integer = 0 ''2
            Dim desTipoInterno As Integer = 0 ''3

            Dim contadorpersonasDifCero As Integer = 0

            Dim porcentajeAprobados As Integer = 0
            Dim porcentajeDesAprobados As Integer = 0


            Dim acumuladorNotaMaxima As Decimal = 0.0
            Dim contadorNotasMaximas As Integer = 0
            Dim acumuladorNotaMinima As Decimal = 0.0
            Dim contadorNotasMinima As Integer = 0

            Dim acumuladorPromedioCurso As Decimal = 0.0

            ocontextoPersonaConsolidado.LpadCurosAlumnos(int_numerosCursos)

            For indiceColumnas As Integer = 0 To cantidadColumnas
                indicePersonas = 0
                acumulador = 0
                numeroAprobados = 0
                numeroDesAprobados = 0
                desTipoOficial = 0 ''1
                desTipoComplementario = 0 ''2
                desTipoInterno = 0 ''3
                contadorpersonasDifCero = 0
                notaMaxima = 0
                notaMinima = 20
                tempMaxima = 0
                tempMinima = 20
                For indiceSub As Integer = 0 To ocontextoPersonaConsolidado.LstPersonaConsolidado.Count - 1
                    ''If lstpersonaConsolidado(indicePersonas).lstNotasBimestre(indiceColumnas).tipoCurso = 3 Then
                    notaFinalAc = ocontextoPersonaConsolidado.LstPersonaConsolidado(indicePersonas).lstNotasBimestre(indiceColumnas).notaFinal

                    If notaFinalAc > 0 Then
                        contadorpersonasDifCero += 1
                        notaMaxima = notaFinalAc
                        notaMinima = notaFinalAc
                        If notaMaxima > tempMaxima Then
                            tempMaxima = notaMaxima
                            '' acumuladorNotaMaxima += tempMaxima
                            ''   contadorNotasMaximas += 1
                        End If

                        If notaMinima < tempMinima Then
                            tempMinima = notaMinima
                            ''  acumuladorNotaMinima += tempMinima
                            '' contadorNotasMinima += 1

                        End If
                    End If
                    If notaFinalAc < 10.5 And notaFinalAc > 0 Then
                        numeroDesAprobados += 1
                        If ocontextoPersonaConsolidado.LstPersonaConsolidado(indicePersonas).lstNotasBimestre(indiceColumnas).tipoCurso = 1 Then
                            desTipoOficial += 1
                        ElseIf ocontextoPersonaConsolidado.LstPersonaConsolidado(indicePersonas).lstNotasBimestre(indiceColumnas).tipoCurso = 2 Then
                            desTipoComplementario += 1
                        ElseIf ocontextoPersonaConsolidado.LstPersonaConsolidado(indicePersonas).lstNotasBimestre(indiceColumnas).tipoCurso = 3 Then
                            desTipoInterno += 1
                        End If
                    ElseIf notaFinalAc > 10.5 And notaFinalAc > 0 Then
                        numeroAprobados += 1
                    End If
                    acumulador += notaFinalAc
                    posColumnas = ocontextoPersonaConsolidado.LstPersonaConsolidado(indicePersonas).lstNotasBimestre(indiceColumnas).posCurso
                    ''End If
                    indicePersonas += 1
                Next
                If posColumnas <> 0 Then
                    '' Format(opersonaConsolidado.promedioFinal, "#.00")

                    If Format((acumulador / contadorpersonasDifCero), "#.00") = "NeuN" Then
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, posColumnas), Range).Value = ""
                    Else

                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, posColumnas), Range).Value = Format((acumulador / contadorpersonasDifCero), "#.00")

                        acumuladorPromedioCurso += acumulador / contadorpersonasDifCero

                    End If
                    If numeroAprobados > 0 Then
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 2, posColumnas), Range).Value = CStr(numeroAprobados)
                    Else
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 2, posColumnas), Range).Value = ""
                    End If
                    If numeroDesAprobados > 0 Then
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 3, posColumnas), Range).Value = CStr(numeroDesAprobados)
                    End If
                    If desTipoOficial = 0 Then
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 4, posColumnas), Range).Value = ""
                    Else
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 4, posColumnas), Range).Value = CStr(desTipoOficial)
                    End If

                    If desTipoComplementario = 0 Then
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 5, posColumnas), Range).Value = ""
                    Else
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 5, posColumnas), Range).Value = CStr(desTipoComplementario)
                    End If

                    If desTipoInterno = 0 Then
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 6, posColumnas), Range).Value = ""
                    Else
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 6, posColumnas), Range).Value = CStr(desTipoInterno)
                    End If

                    ''


                    Dim porTempDes As String = Math.Round((desTipoOficial + desTipoComplementario + desTipoInterno) / contadorpersonasDifCero * 100, 2)
                    Dim porTempApr As String = Math.Round(100 - (((desTipoOficial + desTipoComplementario + desTipoInterno) / contadorpersonasDifCero) * 100), 2)



                    If CStr(porTempApr) = "NeuN" Then
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 7, posColumnas), Range).Value = ""
                    Else
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 7, posColumnas), Range).Value = CStr(porTempApr)
                    End If
                    If CStr(porTempDes) = "NeuN" Then
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 8, posColumnas), Range).Value = ""
                    Else
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 8, posColumnas), Range).Value = CStr(porTempDes)
                    End If




                    If tempMaxima = 0 And tempMinima = 20 Then
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 9, posColumnas), Range).Value = ""
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 10, posColumnas), Range).Value = ""
                    Else
                        acumuladorNotaMaxima += tempMaxima
                        acumuladorNotaMinima += tempMinima
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 9, posColumnas), Range).Value = CStr(tempMaxima)
                        contadorNotasMaximas += 1
                        CType(excel.ActiveSheet.Cells(filasEmpiezan + 10, posColumnas), Range).Value = CStr(tempMinima)

                    End If


                    ''

                    ''

                    ''
                End If
            Next

            ''
            If contadorNotasMaximas = 0 Then
                contadorNotasMaximas = 1
                CType(excel.ActiveSheet.Cells(filasEmpiezan + 9, lstcursoConsolidado.Count + 3), Range).Value = acumuladorNotaMaxima / contadorNotasMaximas
            Else
                CType(excel.ActiveSheet.Cells(filasEmpiezan + 9, lstcursoConsolidado.Count + 3), Range).Value = acumuladorNotaMaxima / contadorNotasMaximas
            End If


            If contadorNotasMaximas = 0 Then
                contadorNotasMaximas = 1
                CType(excel.ActiveSheet.Cells(filasEmpiezan + 10, lstcursoConsolidado.Count + 3), Range).Value = acumuladorNotaMinima / contadorNotasMaximas
            Else
                CType(excel.ActiveSheet.Cells(filasEmpiezan + 10, lstcursoConsolidado.Count + 3), Range).Value = acumuladorNotaMinima / contadorNotasMaximas
            End If



            CType(excel.ActiveSheet.Cells(filasEmpiezan + 9, lstcursoConsolidado.Count + 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(filasEmpiezan + 10, lstcursoConsolidado.Count + 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


            If contadorNotasMaximas = 0 Then
                contadorNotasMaximas = 1
                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, lstcursoConsolidado.Count + 3), Range).Value = acumuladorPromedioCurso / contadorNotasMaximas
            Else
                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, lstcursoConsolidado.Count + 3), Range).Value = acumuladorPromedioCurso / contadorNotasMaximas
            End If



            CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, lstcursoConsolidado.Count + 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            ''
            Dim cantidadColumnasPintar As Integer = 2
            For filasLast As Integer = 0 To 10
                cantidadColumnasPintar = 2
                For columnas As Integer = 0 To lstcursoConsolidado.Count + 1
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, cantidadColumnasPintar), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    cantidadColumnasPintar += 1
                Next
                filasEmpiezan += 1
            Next

            Dim cantidadInvictos As Integer = 0
            Dim cantidadJalados1curso As Integer = 0
            Dim cantidadJalados2curso As Integer = 0
            Dim cantidadJalados3curso As Integer = 0
            Dim cantidadJalados4mascurso As Integer = 0

            For Each opersonaConsolidado As personaConsolidado In ocontextoPersonaConsolidado.LstPersonaConsolidado

                If opersonaConsolidado.cantidadCursosJalados = 0 Then
                    cantidadInvictos += 1
                End If
                If opersonaConsolidado.cantidadCursosJalados = 1 Then
                    cantidadJalados1curso += 1
                End If
                If opersonaConsolidado.cantidadCursosJalados = 2 Then
                    cantidadJalados2curso += 1
                End If
                If opersonaConsolidado.cantidadCursosJalados = 3 Then
                    cantidadJalados3curso += 1
                End If

                If opersonaConsolidado.cantidadCursosJalados >= 4 Then
                    cantidadJalados4mascurso += 1
                End If

            Next

            Dim strCandePorcentajes As String = ""

            strCandePorcentajes &= " Invictos :" & cantidadInvictos & "(" & CStr(Math.Round((cantidadInvictos / ocontextoPersonaConsolidado.LstPersonaConsolidado.Count) * 100, 2)) & "%)       "
            strCandePorcentajes &= " 1 Curso :" & cantidadJalados1curso & "(" & CStr(Math.Round((cantidadJalados1curso / ocontextoPersonaConsolidado.LstPersonaConsolidado.Count) * 100, 2)) & "%)       "
            strCandePorcentajes &= " 2 Cursos:" & cantidadJalados2curso & "(" & CStr(Math.Round((cantidadJalados2curso / ocontextoPersonaConsolidado.LstPersonaConsolidado.Count) * 100, 2)) & "%)       "
            strCandePorcentajes &= " 3 Cursos:" & cantidadJalados3curso & "(" & CStr(Math.Round((cantidadJalados3curso / ocontextoPersonaConsolidado.LstPersonaConsolidado.Count) * 100, 2)) & "%)       "
            strCandePorcentajes &= " 4 o + Cursos:" & cantidadJalados4mascurso & "(" & CStr(Math.Round((cantidadJalados4mascurso / ocontextoPersonaConsolidado.LstPersonaConsolidado.Count) * 100, 2)) & "%)       "


            excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, 2), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, lstcursoConsolidado.Count + 3), Range)).Merge(True)

            excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, 2), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, lstcursoConsolidado.Count + 3), Range)).Value = strCandePorcentajes

            excel.ActiveSheet.Cells.Columns(1).ColumnWidth = 4
            excel.ActiveSheet.Columns(2).EntireColumn.AutoFit()
            wbkWorkbook.Save()
            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

            Return rutaREpositorioTemporales



        Catch ex As Exception

        End Try
    End Function
    
      
    ''repConsolidadoSecundaria.xlsx

#End Region


#Region "exportar comparacion de libretas"
    Function fExportarComparacionBimestre(ByVal codGrado As Integer, ByVal codBimestreA As Integer, ByVal codBimestreB As Integer) As String


        Try


            Dim abrPrimerBimestre As String = ""
            Dim abrSegundoBimestre As String = ""

            If codBimestreA = 1 Then
                abrPrimerBimestre = "1Er."
            End If
            If codBimestreA = 2 Then
                abrPrimerBimestre = "2Do."
            End If
            If codBimestreA = 3 Then
                abrPrimerBimestre = "3Er."
            End If
            If codBimestreA = 4 Then
                abrPrimerBimestre = "4To."
            End If

            If codBimestreB = 1 Then
                abrSegundoBimestre = "1Er."
            End If
            If codBimestreB = 2 Then
                abrSegundoBimestre = "2Do."
            End If
            If codBimestreB = 3 Then
                abrSegundoBimestre = "3Er."
            End If
            If codBimestreB = 4 Then
                abrSegundoBimestre = "4To."
            End If


            Dim dst As New DataSet
            Dim Obl_rep_libretaNotas As New bl_rep_libretaNotas
            Dim dcPAram As New Dictionary(Of String, Object)
            dcPAram.Add("GD_CodigoGrado", codGrado)
            dcPAram.Add("BM_CodigoBimestreA", codBimestreA)
            dcPAram.Add("BM_CodigoBimestreB", codBimestreB)
            dcPAram.Add("tipoReporte", 1)

            Dim nParam As String = "USP_Rep"
            dst = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dcPAram, nParam)
            Dim lstCursos As New List(Of Curso)
            lstCursos = crearListaCursos(dst.Tables(0), 1, 1)
            Dim lstCursos1 As New List(Of Curso)
            lstCursos1 = crearListaCursos(dst.Tables(1), 1, 2)


            ''
            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaReporteConparacion")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)

            ''
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)



            Dim ws = workbook.Worksheet(1)
            Dim filaInicio As Integer = 4
            Dim cantidadAulas As Integer = 0
            Dim contInicial As Integer = 0
            Dim filasSecuencial As Integer = 4
            Dim contElementos As Integer = -1
            Dim indiceCurso As Integer = -1
            Dim cantidadFilas As Integer = 0

            ws.Range(ws.Cell(2, 2), ws.Cell((3), 3)).Merge()
            ws.Range(ws.Cell(2, 2), ws.Cell((3), 3)).Value = "Grade " & (codGrado - 3).ToString


            ws.Cell(3, 4).Value = abrPrimerBimestre
            ws.Cell(3, 7).Value = abrPrimerBimestre

            ws.Cell(3, 10).Value = abrPrimerBimestre
            ws.Cell(3, 13).Value = abrPrimerBimestre




            ws.Cell(3, 5).Value = abrSegundoBimestre
            ws.Cell(3, 8).Value = abrSegundoBimestre

            ws.Cell(3, 11).Value = abrSegundoBimestre
            ws.Cell(3, 14).Value = abrSegundoBimestre




            For Each oCursos As Curso In lstCursos
                indiceCurso = 1
                contInicial += 1
                cantidadAulas = oCursos.lstAula.Count
                cantidadAulas -= 1
                ws.Range(ws.Cell(filaInicio, 2), ws.Cell((filaInicio + cantidadAulas), 2)).Merge()
                ws.Range(ws.Cell(filaInicio, 2), ws.Cell((filaInicio + cantidadAulas), 2)).Value = oCursos.NC_Descripcion



                contElementos = -1
                For inicio As Integer = filaInicio To filaInicio + cantidadAulas
                    contElementos += 1
                    ws.Cell(inicio, 3).Value = oCursos.lstAula(contElementos).AU_Descripcion
                    cantidadFilas += 1
                    Dim indiceAulas As Integer = -1
                    For Each oNotas As notas In oCursos.lstAula(contElementos).lstnotas
                        indiceAulas += 1
                        If oNotas.posicion <> 0 Then
                            ws.Cell(inicio, oNotas.posicion).Value = ((oNotas.cantidadNotaCursoAula / oCursos.lstAula(contElementos).totalAula) * 100).ToString("N2") & "%"
                        End If
                    Next
                Next
                filaInicio += oCursos.lstAula.Count
            Next
            filaInicio = 4
            cantidadAulas = 0
            contInicial = 0
            filasSecuencial = 4
            contElementos = -1
            indiceCurso = -1

            ''
            For Each oCursos As Curso In lstCursos1
                indiceCurso = 1
                contInicial += 1
                cantidadAulas = oCursos.lstAula.Count
                cantidadAulas -= 1
                contElementos = -1
                For inicio As Integer = filaInicio To filaInicio + cantidadAulas
                    contElementos += 1
                    Dim indiceAulas As Integer = -1
                    For Each oNotas As notas In oCursos.lstAula(contElementos).lstnotas
                        indiceAulas += 1
                        If oNotas.posicion <> 0 Then
                            ws.Cell(inicio, oNotas.posicion).Value = ((oNotas.cantidadNotaCursoAula / oCursos.lstAula(contElementos).totalAula) * 100).ToString("N2") & "%"
                        End If
                    Next
                Next



                filaInicio += oCursos.lstAula.Count
            Next

            ''

            Dim filasInician As Integer = 3

            Dim AD1Bim As Double = 0.0
            Dim AD2Bim As Double = 0.0

            Dim A1Bim As Double = 0.0
            Dim A2Bim As Double = 0.0

            Dim B1Bim As Double = 0.0
            Dim B2Bim As Double = 0.0

            Dim C1Bim As Double = 0.0
            Dim C2Bim As Double = 0.0


            For filasEmpiezanSumar As Integer = 1 To cantidadFilas
                filasInician += 1


                If ws.Cell(filasInician, 4).Value().ToString() = "" Then
                    ws.Cell(filasInician, 4).Value() = 0
                End If
                If ws.Cell(filasInician, 5).Value().ToString() = "" Then
                    ws.Cell(filasInician, 5).Value = 0
                End If
                If ws.Cell(filasInician, 7).Value().ToString() = "" Then
                    ws.Cell(filasInician, 7).Value = 0
                End If
                If ws.Cell(filasInician, 8).Value().ToString() = "" Then
                    ws.Cell(filasInician, 8).Value = 0
                End If
                If ws.Cell(filasInician, 10).Value().ToString() = "" Then
                    ws.Cell(filasInician, 10).Value = 0
                End If
                If ws.Cell(filasInician, 11).Value().ToString() = "" Then
                    ws.Cell(filasInician, 11).Value = 0
                End If
                If ws.Cell(filasInician, 13).Value().ToString() = "" Then
                    ws.Cell(filasInician, 13).Value = 0
                End If
                If ws.Cell(filasInician, 14).Value().ToString() = "" Then
                    ws.Cell(filasInician, 14).Value = 0
                End If





                AD1Bim = IIf(ws.Cell(filasInician, 4).Value().ToString().Replace("%", "") = "", 0, ws.Cell(filasInician, 4).Value().ToString().Replace("%", ""))


                AD2Bim = IIf(ws.Cell(filasInician, 5).Value().ToString().Replace("%", "") = "", 0, ws.Cell(filasInician, 5).Value().ToString().Replace("%", ""))

                ws.Cell(filasInician, 6).Value = (AD2Bim - AD1Bim).ToString("N2") & "%"

                A1Bim = IIf(ws.Cell(filasInician, 7).Value().ToString().Replace("%", "") = "", 0, ws.Cell(filasInician, 7).Value().ToString().Replace("%", ""))
                A2Bim = IIf(ws.Cell(filasInician, 8).Value().ToString().Replace("%", "") = "", 0, ws.Cell(filasInician, 8).Value().ToString().Replace("%", ""))

                ws.Cell(filasInician, 9).Value = (A2Bim - A1Bim).ToString("N2") & "%"

                B1Bim = IIf(ws.Cell(filasInician, 10).Value().ToString().Replace("%", "") = "", 0, ws.Cell(filasInician, 10).Value().ToString().Replace("%", ""))
                B2Bim = IIf(ws.Cell(filasInician, 11).Value().ToString().Replace("%", "") = "", 0, ws.Cell(filasInician, 11).Value().ToString().Replace("%", ""))

                ws.Cell(filasInician, 12).Value = (B2Bim - B1Bim).ToString("N2") & "%"
                ''.ToString("N2")
                C1Bim = IIf(ws.Cell(filasInician, 13).Value().ToString().Replace("%", "") = "", 0, ws.Cell(filasInician, 13).Value().ToString().Replace("%", ""))
                C2Bim = IIf(ws.Cell(filasInician, 14).Value().ToString().Replace("%", "") = "", 0, ws.Cell(filasInician, 14).Value().ToString().Replace("%", ""))
                ws.Cell(filasInician, 15).Value = (C2Bim - C1Bim).ToString("N2") & "%"

            Next


            With ws.Range(ws.Cell(2, 2), ws.Cell(cantidadFilas + 3, 15))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin

            End With


            workbook.Save()


            Return rutaREpositorioTemporales
        Catch ex As Exception
        Finally

        End Try

    End Function
#End Region


#Region "exportar consolidado grado bimestre  "
    ''' <summary>
    ''' funcion para exportar porcentaje de cursos por grado y bimestre 
    ''' </summary>
    ''' <param name="codGrado"></param>
    ''' <param name="codBimestreA"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function fExportarConsolidadoBimestreGrado(ByVal codGrado As Integer, ByVal codBimestreA As Integer) As String
        Try

            Dim dst As New DataSet
            Dim Obl_rep_libretaNotas As New bl_rep_libretaNotas
            Dim dcPAram As New Dictionary(Of String, Object)
            dcPAram.Add("GD_CodigoGrado", codGrado)
            dcPAram.Add("BM_CodigoBimestreA", codBimestreA)
            dcPAram.Add("BM_CodigoBimestreB", 2)
            dcPAram.Add("tipoReporte", 2)
            Dim nParam As String = "USP_Rep"
            dst = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dcPAram, nParam)
            Dim lstCursos As New List(Of Curso)
            lstCursos = crearListaCursos(dst.Tables(0), 1, 1)


            ''
            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaReporteConparacionBimestreGrado")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)

            ''
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)



            Dim ws = workbook.Worksheet(1)
            Dim filaInicio As Integer = 7
            Dim cantidadAulas As Integer = 0
            Dim contInicial As Integer = 0
            Dim filasSecuencial As Integer = 4
            Dim contElementos As Integer = -1
            Dim indiceCurso As Integer = -1
            Dim cantidadFilas As Integer = 0

            ws.Range(ws.Cell(4, 2), ws.Cell(4, 2)).Value = "Bimestre " & Str(codBimestreA)

            ws.Range(ws.Cell(6, 2), ws.Cell(6, 3)).Merge()
            With ws.Range(ws.Cell(6, 2), ws.Cell(6, 3))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With
            ws.Range(ws.Cell(6, 2), ws.Cell(6, 3)).Value = "Grado " & Str(codGrado - 3)

            Dim CatntidadFilasTotal As Integer = 0
            For Each oCursos As Curso In lstCursos
                indiceCurso = 1
                contInicial += 1
                cantidadAulas = oCursos.lstAula.Count
                cantidadAulas -= 1
                ws.Range(ws.Cell(filaInicio, 2), ws.Cell((filaInicio + cantidadAulas), 2)).Merge()
                ws.Range(ws.Cell(filaInicio, 2), ws.Cell((filaInicio + cantidadAulas), 2)).Value = oCursos.NC_Descripcion

                contElementos = -1
                For inicio As Integer = filaInicio To filaInicio + cantidadAulas
                    contElementos += 1
                    ws.Cell(inicio, 3).Value = oCursos.lstAula(contElementos).AU_Descripcion
                    cantidadFilas += 1
                    Dim indiceAulas As Integer = -1

                    Dim sumaNotas As Integer = 0


                    For Each oNotas As notas In oCursos.lstAula(contElementos).lstnotas
                        indiceAulas += 1

                        sumaNotas += oNotas.cantidadNotaCursoAula
                        If oNotas.RNBL_NotaFinalBimestre.ToString().ToUpper() = "AD" Then
                            ws.Cell(inicio, 4).Value = oNotas.cantidadNotaCursoAula
                            ws.Cell(inicio, 5).Value = ((oNotas.cantidadNotaCursoAula / oCursos.lstAula(contElementos).totalAula) * 100).ToString("N2") & "%"
                        ElseIf oNotas.RNBL_NotaFinalBimestre.ToString().ToUpper() = "A" Then
                            ws.Cell(inicio, 6).Value = oNotas.cantidadNotaCursoAula
                            ws.Cell(inicio, 7).Value = ((oNotas.cantidadNotaCursoAula / oCursos.lstAula(contElementos).totalAula) * 100).ToString("N2") & "%"
                        ElseIf oNotas.RNBL_NotaFinalBimestre.ToString().ToUpper() = "B" Then
                            ws.Cell(inicio, 8).Value = oNotas.cantidadNotaCursoAula
                            ws.Cell(inicio, 9).Value = ((oNotas.cantidadNotaCursoAula / oCursos.lstAula(contElementos).totalAula) * 100).ToString("N2") & "%"
                        ElseIf oNotas.RNBL_NotaFinalBimestre.ToString().ToUpper() = "C" Then
                            ws.Cell(inicio, 10).Value = oNotas.cantidadNotaCursoAula
                            ws.Cell(inicio, 11).Value = ((oNotas.cantidadNotaCursoAula / oCursos.lstAula(contElementos).totalAula) * 100).ToString("N2") & "%"
                        End If


                    Next


                    ws.Cell(inicio, 12).Value = sumaNotas.ToString()
                Next
                filaInicio += oCursos.lstAula.Count
            Next
           
            ''
             


            ''




            ''

            With ws.Range(ws.Cell(7, 2), ws.Cell(cantidadFilas + 7, 12))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin

            End With

            ''
            workbook.Save()


            Return rutaREpositorioTemporales



        Catch ex As Exception

        End Try
    End Function

#End Region
#Region "utiles "
#Region "clases serializar"
    Partial Public Class Curso

        Public ACA_CodigoAsignacionCurso As Integer
        Public NC_Descripcion As String
        Public lstAula As New List(Of Aula)
    End Class

    Partial Public Class Aula

        Public AU_Descripcion As String
        Public AAP_CodigoAsignacionAula As Integer
        Public totalAula As Integer
        Public lstnotas As New List(Of notas)

    End Class

    Public Class notas


        Public RNBL_NotaFinalBimestre As String
        Public cantidadNotaCursoAula As Integer
        Public AGC_CodigoAsignacionGrupo As Integer
        Public porcentaje As Double
        Public posicion As Integer
        Public BM_CodigoBimestre As Integer

    End Class
#End Region

#Region "metodos crear colecciones "


    Public Function crearListaCursos(ByVal dtCursos As System.Data.DataTable, ByVal opcional As Integer, ByVal bimestre As Integer) As List(Of Curso)
        Dim dtCursosFiltro As DataRow()

        Dim oCurso As Curso
        Dim lstCurso As New List(Of Curso)
        Dim TempACA_CodigoAsignacionCurso As Integer
        Try
            For Each filasCurso As DataRow In dtCursos.Rows
                oCurso = New Curso
                oCurso.NC_Descripcion = filasCurso("NC_Descripcion").ToString()
                oCurso.ACA_CodigoAsignacionCurso = CInt(filasCurso("ACA_CodigoAsignacionCurso").ToString())

                If oCurso.ACA_CodigoAsignacionCurso <> TempACA_CodigoAsignacionCurso Then

                    dtCursosFiltro = dtCursos.Select(" ACA_CodigoAsignacionCurso=" & oCurso.ACA_CodigoAsignacionCurso, "")
                    oCurso.lstAula = crearListaAulas(dtCursosFiltro, bimestre)
                    lstCurso.Add(oCurso)

                End If

                TempACA_CodigoAsignacionCurso = oCurso.ACA_CodigoAsignacionCurso
            Next
            Return lstCurso
        Catch ex As Exception

        End Try
    End Function

    'Function crearListaCursos(ByVal drNotas As DataRow()) As List(Of notas)
    '    Dim onotas As notas
    '    Dim lstNotas As New List(Of notas)

    '    Try


    '    Catch ex As Exception
    '    Finally

    '    End Try


    'End Function
    Public Function crearListaAulas(ByVal aulas As DataRow(), ByVal codBim As Integer) As List(Of Aula)
        Dim oAula As Aula
        Dim lstAulas As New List(Of Aula)
        Dim drNotas As DataRow()
        Dim lstNotas As New List(Of notas)
        Dim onotas As notas

        '  Dim funcionWhere As Func(Of DataRow, Boolean) = Function(fila) fila("AGC_CodigoAsignacionGrupo")
        Try
            Dim AAP_CodigoAsignacionAula As Integer
            For Each filasCursos As DataRow In aulas
                oAula = New Aula
                oAula.AAP_CodigoAsignacionAula = CInt(filasCursos("AAP_CodigoAsignacionAula").ToString())
                oAula.AU_Descripcion = filasCursos("AU_Descripcion").ToString()
                oAula.totalAula = CInt(filasCursos("totalAula").ToString())
                If oAula.AAP_CodigoAsignacionAula <> AAP_CodigoAsignacionAula Then



                    For Each filas As DataRow In aulas
                        If CInt(filas("AAP_CodigoAsignacionAula").ToString()) = oAula.AAP_CodigoAsignacionAula Then
                            onotas = New notas

                            onotas.cantidadNotaCursoAula = CInt(filas("cantidadNotaCursoAula").ToString())
                            onotas.RNBL_NotaFinalBimestre = filas("RNBL_NotaFinalBimestre").ToString()
                            onotas.BM_CodigoBimestre = CInt(filas("BM_CodigoBimestre").ToString())

                            If onotas.RNBL_NotaFinalBimestre = "AD" Then


                                If codBim = 1 Then
                                    onotas.posicion = 4
                                ElseIf codBim = 2 Then
                                    onotas.posicion = 5
                                End If


                            ElseIf onotas.RNBL_NotaFinalBimestre = "A" Then

                                If codBim = 1 Then
                                    onotas.posicion = 7
                                ElseIf codBim = 2 Then
                                    onotas.posicion = 8
                                End If

                            ElseIf onotas.RNBL_NotaFinalBimestre = "B" Then

                                If codBim = 1 Then
                                    onotas.posicion = 10
                                ElseIf codBim = 2 Then
                                    onotas.posicion = 11
                                End If

                            ElseIf onotas.RNBL_NotaFinalBimestre = "C" Then

                                If codBim = 1 Then
                                    onotas.posicion = 13
                                ElseIf codBim = 2 Then
                                    onotas.posicion = 14
                                End If

                            Else

                                onotas.posicion = 0

                            End If

                            oAula.lstnotas.Add(onotas)
                        End If
                    Next
                    onotas = New notas



                    lstAulas.Add(oAula)
                End If

                AAP_CodigoAsignacionAula = oAula.AAP_CodigoAsignacionAula
            Next

            Return lstAulas
        Catch ex As Exception

        End Try



    End Function


    Private Function crearLitasNotas(ByVal drNotas As DataRow()) As List(Of notas)
        Dim onotas As notas
        Dim lstnotas As New List(Of notas)

        Try

            For Each filasNotas As DataRow In drNotas
                onotas = New notas
                onotas.cantidadNotaCursoAula = CInt(filasNotas("cantidadNotaCursoAula").ToString())
                onotas.RNBL_NotaFinalBimestre = CStr(filasNotas("RNBL_NotaFinalBimestre").ToString())
                lstnotas.Add(onotas)
            Next
            Return lstnotas
        Catch ex As Exception
        Finally

        End Try
    End Function
#End Region

#End Region
    ''


    ''
#Region "Utilidades Consoliado Secundaria"
    Function crearListaCursos(ByVal dtCursos As System.Data.DataTable) As List(Of cursoConsolidado)
        Dim lstCursos As New List(Of cursoConsolidado)
        Dim ocursoConsolidado As cursoConsolidado

        Try

            For Each filasCurso As System.Data.DataRow In dtCursos.Rows
                ocursoConsolidado = New cursoConsolidado
                ocursoConsolidado.nombreCurso = filasCurso("nombreCurso").ToString()
                ocursoConsolidado.posCurso = CInt(filasCurso("pk").ToString())
                ''pk	codCurso	nombreCurso
                lstCursos.Add(ocursoConsolidado)
            Next
            Return lstCursos
        Catch ex As Exception
        Finally
        End Try
    End Function



    'Public Class 
    Function crearListaPersonaConsolidado(ByVal dtPersonas As System.Data.DataTable, ByVal dtNotasCurso As System.Data.DataTable, ByVal dtNotas As System.Data.DataTable, ByVal tipoCurso As Integer) As contextoPersonaConsolidado

        Dim opersonaConsolidado As personaConsolidado
        Dim lstpersonaConsolidado As New List(Of personaConsolidado)
        Dim sqlFilasCursosNotas As System.Data.DataRow()
        Dim sqlFaltas As System.Data.DataRow()
        Dim ocontextoPersonaConsolidado As New contextoPersonaConsolidado
        Dim onotaBimestre As notaBimestre
        Try
            For Each filasPersona As System.Data.DataRow In dtPersonas.Rows
                opersonaConsolidado = New personaConsolidado
                opersonaConsolidado.codPersona = filasPersona("AL_CodigoAlumno").ToString()
                opersonaConsolidado.nombrePersona = filasPersona("nombreAlumno").ToString()
                opersonaConsolidado.notaConducta = CDec(filasPersona("notaConducta").ToString())

                sqlFilasCursosNotas = dtNotasCurso.Select("AL_CodigoAlumno=" & opersonaConsolidado.codPersona)


                For Each filasResSqlCursosNotas As System.Data.DataRow In sqlFilasCursosNotas
                    onotaBimestre = New notaBimestre
                    onotaBimestre.notaFinal = CInt(filasResSqlCursosNotas("RNBT_NotaFinalBimestre").ToString())
                    onotaBimestre.posCurso = CInt(filasResSqlCursosNotas("posX").ToString())
                    onotaBimestre.tipoCurso = CInt(filasResSqlCursosNotas("TC_CodigoTipoCurso").ToString())

                    ''TC_CodigoTipoCurso
                    '1:
                    '' If onotaBimestre.notaFinal > 0 Then
                    opersonaConsolidado.lstNotasBimestre.Add(onotaBimestre)
                    '' End If

                Next
                sqlFaltas = dtNotas.Select("codAlumno=" & opersonaConsolidado.codPersona)
                For Each filasFaltas As System.Data.DataRow In sqlFaltas
                    opersonaConsolidado.cantidadFaltasInjustificadas = CInt(filasFaltas("cantidadFaltasInjustificadas").ToString())
                    opersonaConsolidado.cantidadFaltasJustificadas = CInt(filasFaltas("cantidadFaltasJustificadas").ToString())
                    opersonaConsolidado.cantidadTardanzasJustificadas = CInt(filasFaltas("cantidadTardanzasJustificadas").ToString())
                    opersonaConsolidado.cantidadTardanzasInjusticadas = CInt(filasFaltas("cantidadTardanzasInjusticadas").ToString())
                Next

                ocontextoPersonaConsolidado.LstPersonaConsolidado.Add(opersonaConsolidado)
                ''  lstpersonaConsolidado.Add(opersonaConsolidado)
            Next


            Return ocontextoPersonaConsolidado
        Catch ex As Exception
        Finally

        End Try
    End Function

    Public Class cursoConsolidado
        Public nombreCurso As String
        Public posCurso As Integer

    End Class


    Public Class contextoPersonaConsolidado
        Public LstPersonaConsolidado As New List(Of personaConsolidado)
        Public lstOrdenadoPersonas As IEnumerable(Of personaConsolidado)
        Public pos As Integer = 0

        Public Sub fAsignarOrden()
            lstOrdenadoPersonas = From j In LstPersonaConsolidado Order By j.promedioFinal Descending Select j
            For Each opersonaConsolidado As personaConsolidado In LstPersonaConsolidado
                pos = 0
                For Each oopersonaConsolidado As personaConsolidado In lstOrdenadoPersonas
                    pos += 1
                    If opersonaConsolidado.codPersona = oopersonaConsolidado.codPersona Then
                        opersonaConsolidado.ordenMerito = pos
                        Exit For
                    End If
                Next
            Next
        End Sub
        Public Sub LpadCurosAlumnos(ByVal pCantidadCursos As Integer)
            Dim onotaBimestre As notaBimestre
            Try
                Dim diferencia As Integer = 0
                For Each opersonaConsolidado As personaConsolidado In LstPersonaConsolidado
                    diferencia = pCantidadCursos - opersonaConsolidado.lstNotasBimestre.Count
                    If diferencia <> 0 Then
                        For indice As Integer = 0 To diferencia
                            onotaBimestre = New notaBimestre
                            opersonaConsolidado.lstNotasBimestre.Add(onotaBimestre)
                        Next
                    End If
                Next
            Catch ex As Exception
            Finally
            End Try
        End Sub
    End Class
    Public Class personaConsolidado
        Public nombrePersona As String
        Public codPersona As String
        Public lstNotasBimestre As New List(Of notaBimestre)
        Public promedioFinal As Decimal
        Public puntajeTotal As Integer
        Public cantidadAprobados As Integer
        Public cantidadDesAprobados As Integer
        Public notaConducta As Decimal
        Public ordenMerito As Integer
        ''
        Public cantidadTardanzasInjusticadas As Integer
        Public cantidadTardanzasJustificadas As Integer
        Public cantidadFaltasJustificadas As Integer
        Public cantidadFaltasInjustificadas As Integer
        Public tercioNombre As String
        Public cantidadCursosJalados As Integer
        ''
        Public Sub New()
            Me.cantidadCursosJalados = 0
        End Sub

        Public Sub promedioCursos()
            Dim countNotas As Decimal = 0.0
            Dim countAprobados As Integer = 0
            Dim countDesAprobados As Integer = 0
            Dim contadoNotas As Integer = 0


            Dim cantidadJalados As Integer = 0
            For Each onotaBimestre As notaBimestre In Me.lstNotasBimestre
                If onotaBimestre.notaFinal > 0 Then
                    countNotas += onotaBimestre.notaFinal
                    contadoNotas += 1
                End If

                If onotaBimestre.notaFinal > 10 Then
                    countAprobados += 1
                ElseIf onotaBimestre.notaFinal > 0 And onotaBimestre.notaFinal < 10.5 Then
                    countDesAprobados += 1
                End If
            Next


            If contadoNotas = 0 Then
                contadoNotas = 1
                promedioFinal = countNotas / contadoNotas
            Else
                promedioFinal = countNotas / contadoNotas
            End If



            cantidadJalados = 0
            For Each onotaBimestre As notaBimestre In Me.lstNotasBimestre
                If onotaBimestre.notaFinal < 10.5 And onotaBimestre.notaFinal > 0 Then
                    cantidadJalados += 1
                End If
            Next
            Me.cantidadCursosJalados = cantidadJalados
            Me.promedioFinal = promedioFinal
            Me.puntajeTotal = countNotas
            Me.cantidadAprobados = countAprobados
            Me.cantidadDesAprobados = countDesAprobados
        End Sub





    End Class
    Public Class notaBimestre
        Public notaFinal As Integer
        Public posCurso As Integer
        Public tipoCurso As Integer
    End Class

#End Region

    ''' <summary>
    ''' reporte crear tutor report 
    ''' </summary>
    ''' <param name="codAsignacionAula"></param>
    ''' <param name="codbimestre"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function crearReporteTutor(ByVal codAsignacionAula As Integer, ByVal codbimestre As Integer) As String
        Try
            ''
            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range


            Dim obl_tutorReport As New bl_tutorReport
            Dim dtsReportteExcel As DataSet
            dtsReportteExcel = obl_tutorReport.FUN_REP_SeguimientoTutorReportDatosExcel(codAsignacionAula, codbimestre)

            If dtsReportteExcel.Tables(0).Rows.Count = 0 Then
                Return ""

            End If
            Dim dtDetallerReportCabezera As New System.Data.DataTable

            Dim dtCalificativos As New System.Data.DataTable

            Dim dt_faltas As New System.Data.DataTable
            dt_faltas = dtsReportteExcel.Tables(6)
            Dim dt_tardanzas As New System.Data.DataTable
            dt_tardanzas = dtsReportteExcel.Tables(7)
            Dim dtMatriz As New System.Data.DataTable
            dtMatriz = dtsReportteExcel.Tables(3)

            Dim dtPorcentaje As New System.Data.DataTable
            dtPorcentaje = dtsReportteExcel.Tables(10)

            dtDetallerReportCabezera = dtsReportteExcel.Tables(0)
            dtCalificativos = dtsReportteExcel.Tables(2)

            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("plantillaTutorReport")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)

            wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)


            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()
            wshWorksheet.Visible = True

            Dim contadorFilas As Integer = 7
            Dim contadorColumnas As Integer = 1

            ''
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Value = "TUTOR REPORT"
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Font.Size = 25

            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).RowHeight = 45
            ''     excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            ''

            contadorFilas += 1

            Dim encabezadoPrimero As String = ""
            Dim dt_tutor As New System.Data.DataTable
            dt_tutor = dtsReportteExcel.Tables(11)
            Dim dtNombreAula As New System.Data.DataTable
            dtNombreAula = dtsReportteExcel.Tables(12)
            ''nombreAula
            Dim strNombreTutor As String = ""
            strNombreTutor = dt_tutor.Rows(0)("nombreTutor").ToString()
            Dim strNombreAula As String = ""
            strNombreAula = dtNombreAula.Rows(0)("nombreAula").ToString()
            encabezadoPrimero &= "TUTOR:  " & strNombreTutor & " CLASE :     " & strNombreAula & "  BIMESTRE:   " & codbimestre & ", " & CStr(Year(Date.Now))

            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Value = encabezadoPrimero
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            contadorFilas += 1


            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Value = "Perfil del grupo"
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


            contadorFilas += 1

            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range)).Value = "Rendimiento general"
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(contadorFilas, 3), Range).Value = dtDetallerReportCabezera.Rows(0)("GradeRendimiento").ToString()

            CType(excel.ActiveSheet.Cells(contadorFilas, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            CType(excel.ActiveSheet.Cells(contadorFilas, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 4), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 5), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 4), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 5), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 4), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 5), Range)).Value = "Actitud del grupo  "
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 4), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 5), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(contadorFilas, 6), Range).Value = dtDetallerReportCabezera.Rows(0)("GradeActitud").ToString()
            CType(excel.ActiveSheet.Cells(contadorFilas, 6), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            CType(excel.ActiveSheet.Cells(contadorFilas, 6), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 7), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 8), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 7), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 8), Range)).Value = "Esfuerzo general  "
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 7), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 8), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 7), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 8), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(contadorFilas, 9), Range).Value = dtDetallerReportCabezera.Rows(0)("GradeEsfuerzo").ToString()
            CType(excel.ActiveSheet.Cells(contadorFilas, 9), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            CType(excel.ActiveSheet.Cells(contadorFilas, 9), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            'GradeActitud	GradeEsfuerzo	GradeRendimiento	CTR_CodigoCabeceraTutorReport	EG_Abrev

            ''
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 10), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 11), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 10), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 11), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            ''
            'F	F	F	1	F
            contadorFilas += 1
            Dim contadorFilasTemp As Integer = 0
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Merge(True)

            Dim strCalificativo As String = ""
            Dim conSaltos As Integer = 0
            For Each filasCalififcativo As System.Data.DataRow In dtCalificativos.Rows
                conSaltos += 1

                If conSaltos < dtCalificativos.Rows.Count Then
                    strCalificativo &= filasCalififcativo("CF_Descripcion").ToString() & " - "
                Else
                    strCalificativo &= filasCalififcativo("CF_Descripcion").ToString() & "   "
                End If

            Next
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Value = strCalificativo
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            contadorFilas += 1
            CType(excel.ActiveSheet.Cells(contadorFilas, contadorColumnas), Range).Value = "Criterios"
            CType(excel.ActiveSheet.Cells(contadorFilas, contadorColumnas), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            CType(excel.ActiveSheet.Cells(contadorFilas, contadorColumnas), Range).Font.Bold = True

            Dim contAgruparFilas As Integer = 2


            For Each filasCalificativos As System.Data.DataRow In dtCalificativos.Rows
                contadorColumnas += 1

                Dim limInf As Integer = 0
                Dim limSup As Integer = 0
                ''
                limInf = (2 * (contAgruparFilas) - 2)
                limSup = (2 * (contAgruparFilas) - 1)

                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilas, limSup), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilas, limSup), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilas, limSup), Range)).Value = filasCalificativos("CF_Descripcion").ToString()
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilas, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                'CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (contAgruparFilas) - 2)), Range).Value = filasCalificativos("CF_Descripcion").ToString()
                'CType(excel.ActiveSheet.Cells(contadorFilas, contadorColumnas), Range).Font.Bold = True


                contAgruparFilas += 1
            Next

            contadorColumnas = 1
            contadorFilas += 1
            For Each filasMatriz As System.Data.DataRow In dtMatriz.Rows

                Dim limInf As Integer = 0
                Dim limSup As Integer = 0
                ''
                limInf = (2 * CInt(filasMatriz("ubicacion").ToString()) - 2)
                limSup = (2 * CInt(filasMatriz("ubicacion").ToString()) - 1)


                ''  excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, dtCalificativos.Rows.Count + 4), Range)).Merge(True)
                CType(excel.ActiveSheet.Cells(contadorFilas, contadorColumnas), Range).Value = filasMatriz("CE_DescripcionCriterio").ToString()


                CType(excel.ActiveSheet.Cells(contadorFilas, contadorColumnas), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'CType(excel.ActiveSheet.Cells(contadorFilas, CInt(filasMatriz("ubicacion").ToString())), Range).Value = "X"
                'CType(excel.ActiveSheet.Cells(contadorFilas, CInt(filasMatriz("ubicacion").ToString())), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilas, limSup), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilas, limSup), Range)).Value = "X"
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilas, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                contAgruparFilas += 1


                Dim subContadorFilas As Integer = 2
                For Each filasCalificativos As System.Data.DataRow In dtCalificativos.Rows
                    Dim limInf1 As Integer = 0
                    Dim limSup1 As Integer = 0
                    limInf1 = (2 * subContadorFilas) - 2
                    limSup1 = (2 * subContadorFilas) - 1

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, limInf1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, limSup1), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, limInf1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, limSup1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    subContadorFilas += 1
                Next
                contadorFilas += 1

            Next


            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 2, 1), Range)).MergeCells = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 2, 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 2, 1), Range)).Value = "Asistencia y puntualidad"
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 2, 1), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop

            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Value = "Lista de alumnos con problemas "
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            contadorFilas += 1
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + 1), Range)).Value = "Faltas "
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

            ''
            Dim str_faltas As String = ""
            For Each filasFaltas As System.Data.DataRow In dt_faltas.Rows
                str_faltas &= filasFaltas("nombreAlumno").ToString() & System.Environment.NewLine
            Next


            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 1), Range)).Value = str_faltas
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 1), Range)).WrapText = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 1), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop






            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).Value = "Tardanzas "
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            ''excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).Font.Bold = True



            Dim strTradanzas As String = ""

            For Each filasTradanzas As System.Data.DataRow In dt_tardanzas.Rows
                strTradanzas &= filasTradanzas("nombreAlumno").ToString() & System.Environment.NewLine
            Next
            ''


            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).Value = strTradanzas
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).WrapText = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
            ''excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + (dtCalificativos.Rows.Count) + 1), Range)).Font.Bold = True

            If dt_faltas.Rows.Count >= dt_tardanzas.Rows.Count Then
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 1), Range)).RowHeight = dt_faltas.Rows.Count * 19
            ElseIf dt_tardanzas.Rows.Count >= dt_faltas.Rows.Count Then

                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (dtCalificativos.Rows.Count) + 1), Range)).RowHeight = dt_tardanzas.Rows.Count * 19
            End If


            contadorFilas += 2

            ''
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range)).MergeCells = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range)).Value = "tutoria"
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Value = " "
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            ''
            contadorFilas += 1

            ''
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range)).MergeCells = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range)).Value = "Conduta"
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Value = " "
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            ''



            contadorFilas += 1
            contadorColumnas = 1
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, 1), Range)).MergeCells = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, 1), Range)).Value = "Cursos"
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (2) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (2) - 1)), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (2) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (2) - 1)), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (2) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (2) - 1)), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (2) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (2) - 1)), Range)).Value = "AD"

            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (3) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (3) - 1)), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (3) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (3) - 1)), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (3) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (3) - 1)), Range)).Value = "A"
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (3) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (3) - 1)), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (4) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (4) - 1)), Range)).Merge(True)

            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (4) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (4) - 1)), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (4) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (4) - 1)), Range)).Value = "B"
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (4) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (4) - 1)), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (5) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (5) - 1)), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (5) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (5) - 1)), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (5) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (5) - 1)), Range)).Value = "C"
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (5) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas + 1, (2 * (5) - 1)), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, dtCalificativos.Rows.Count + 4), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, dtCalificativos.Rows.Count + 4), Range)).Value = "Porcentaje de alumnos con rendimiento Académico"

            Dim temporalFilas As Integer = contadorFilas
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, dtCalificativos.Rows.Count + 4), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, dtCalificativos.Rows.Count + 4), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range), CType(excel.ActiveSheet.Cells(contadorFilas, dtCalificativos.Rows.Count + 4), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            contadorFilas += 1

            Dim contadorFilasAgrupas As Integer = 2

            Dim LimInf2 As Integer = 0
            Dim LImsSup2 As Integer = 0


            Dim LiMSup22 As Integer = 0
            Dim lisMIns As Integer = 0


            For Each filasPorcentaje As System.Data.DataRow In dtPorcentaje.Rows

                contadorFilas += 1

                'LimInf2 = (2 * (contadorFilasAgrupas) - 2)
                'LImsSup2 = (2 * (contadorFilasAgrupas) - 1)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (2) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (2) - 1)), Range)).Merge(True)

                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (2) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (2) - 1)), Range)).Value = filasPorcentaje("porcentajeTextoAD").ToString()
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (2) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (2) - 1)), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (3) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (3) - 1)), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (3) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (3) - 1)), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (3) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (3) - 1)), Range)).Value = filasPorcentaje("porcentajeTextoA").ToString()
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (4) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (4) - 1)), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (4) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (4) - 1)), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (4) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (4) - 1)), Range)).Value = filasPorcentaje("porcentajeTextoB").ToString()
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (5) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (5) - 1)), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (5) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (5) - 1)), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (5) - 2)), Range), CType(excel.ActiveSheet.Cells(contadorFilas, (2 * (5) - 1)), Range)).Value = filasPorcentaje("porcentajeTextoC").ToString()

                ''excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, limInf1), Range), CType(excel.ActiveSheet.Cells(contadorFilas, limSup1), Range)).Merge(True)
                CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range).Value = filasPorcentaje("nombreCurso").ToString()

                CType(excel.ActiveSheet.Cells(contadorFilas, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'CType(excel.ActiveSheet.Cells(contadorFilas, 2), Range).Value = filasPorcentaje("porcentajeTextoAD").ToString()
                'CType(excel.ActiveSheet.Cells(contadorFilas, 3), Range).Value = filasPorcentaje("porcentajeTextoA").ToString()
                'CType(excel.ActiveSheet.Cells(contadorFilas, 4), Range).Value = filasPorcentaje("porcentajeTextoB").ToString()
                'CType(excel.ActiveSheet.Cells(contadorFilas, 5), Range).Value = filasPorcentaje("porcentajeTextoC").ToString()

                '' contadorFilasAgrupas += 1
            Next


            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2)), Range), CType(excel.ActiveSheet.Cells(temporalFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).MergeCells = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilas, (dtCalificativos.Rows.Count * 2)), Range), CType(excel.ActiveSheet.Cells(temporalFilas, (dtCalificativos.Rows.Count * 2) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            ''.MergeCells = True

            wbkWorkbook.Save()
            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

            Return rutaREpositorioTemporales

        Catch ex As Exception
        Finally

        End Try
    End Function
    ''


    ''' <summary>
    ''' reporte class summart report 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function reporteClassSumamaryReport(ByVal codAsignacionAula As Integer, ByVal codBimestre As Integer) As String
        Try
            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range
            Dim lstGrupo As IEnumerable(Of grupo)
            Try
                Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("pantillaClassSummaryReport")
                Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
                Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
                File.Copy(rutaPlantillas, rutaREpositorioTemporales)

                wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)

                wshWorksheet = wbkWorkbook.Worksheets(1)
                wshWorksheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
                wshWorksheet.Activate()
                wshWorksheet.Visible = True
                Dim obl_Seguimiento_Rep As New bl_Seguimiento_Rep
                Dim dstSeguimientoReporte As System.Data.DataSet



                dstSeguimientoReporte = obl_Seguimiento_Rep.fRepClassSumaryReport(codAsignacionAula, 78, 5, codBimestre)


                If dstSeguimientoReporte.Tables(7).Rows.Count < 1 Then

                    Return ""
                    Exit Function

                End If


                lstGrupo = crearGrupos(dstSeguimientoReporte.Tables(5), dstSeguimientoReporte.Tables(4))
                Dim lstCurso As New List(Of cursos)
                ''n fcrearListaCursos(ByVal dt_curso As System.Data.DataTable, ByVal dt_reporte As System.Data.DataTable) As List(Of cursos)
                lstCurso = fcrearListaCursos(dstSeguimientoReporte.Tables(2), dstSeguimientoReporte.Tables(4), dstSeguimientoReporte.Tables(10))
                Dim cantidadCursos As Integer = 0

                cantidadCursos = dstSeguimientoReporte.Tables(2).Rows.Count


                Dim dtNombreSalon As New System.Data.DataTable
                dtNombreSalon = dstSeguimientoReporte.Tables(9)

                Dim nombreSalon As String = dtNombreSalon.Rows(0)("DescAulaCompuesta").ToString()

                Dim dtCAlificativo As New System.Data.DataTable

                dtCAlificativo = dstSeguimientoReporte.Tables(0)

                Dim cantidadCalificativo As Integer = 0

                cantidadCalificativo = dtCAlificativo.Rows.Count '' cantidad  de criterios calificativos

                Dim filasEmpiezan As Integer = 5 '+ 2
                Dim filasEmpiezanTemp As Integer = 5 '+ 2
                ''pintar las columnas de cursos 
                Dim columnasContador As Integer = 2
                Dim limInf As Integer = 0
                Dim limSup As Integer = 0

                Dim contador As Integer = 0
                Dim contadorFilasGl As Integer = 4 ' + 2
                ''cantidadCalificativo
                lstCurso = fAsignarDetalleClassSumary(lstCurso, dstSeguimientoReporte.Tables(7))
                Dim lstPosCalificativos As New List(Of Integer)
                Dim lstPosgrupo As New List(Of Integer)
                Dim lstEspaciosBlanco As New List(Of Integer)

                Dim lstPosicionesNoPintarSuperior As New List(Of Integer)
                Dim lstPosicionesNoPintarInferior As New List(Of Integer)


                Dim CodBimestre1 As Integer = 1
                Dim abrBimestre As String = ""
                If codBimestre = 1 Then
                    abrBimestre = "I"
                End If
                If codBimestre = 2 Then
                    abrBimestre = "II"
                End If
                If codBimestre = 3 Then
                    abrBimestre = "III"
                End If
                If codBimestre = 4 Then
                    abrBimestre = "IV"
                End If


                '' variables contador columnas  conTcolumnas 
                Dim conTcolumnas1 As Integer = 0

                Dim lstPosMaximo As New List(Of Integer)
                Dim pos1 As Integer = 0
                Dim pos2 As Integer = 0
                Dim pos3 As Integer = 0

                CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1), Range).Value = " Class :" & nombreSalon ''System.Environment.NewLine & nombreSalon.Replace(" ", System.Environment.NewLine)

                conTcolumnas1 += 1

                '  excel.ActiveSheet.Cells.Columns(1).ColumnWidth = 21

                excel.ActiveSheet.Cells.Columns(1).ColumnWidth = 21


                CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1), Range).RowHeight = 45

                CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1), Range).WrapText = True

                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1 + (2 * cantidadCalificativo)), Range)).Merge(True)

                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1 + (2 * cantidadCalificativo)), Range)).Value = "Term/Semester: " & abrBimestre
                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1 + (2 * cantidadCalificativo)), Range)).Font.Size = 9

                conTcolumnas1 += 2 * cantidadCalificativo

                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1 + (2 * cantidadCalificativo)), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2 + (2 * cantidadCalificativo)), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1 + (2 * cantidadCalificativo) + (7 * cantidadCalificativo) + 3), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2 + (2 * cantidadCalificativo)), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1 + (2 * cantidadCalificativo) + (7 * cantidadCalificativo) + 3), Range)).Value = "CLASS SUMARY ACADEMIC REPORT"
                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2 + (2 * cantidadCalificativo)), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1 + (2 * cantidadCalificativo) + (7 * cantidadCalificativo) + 3), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2 + (2 * cantidadCalificativo)), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1 + (2 * cantidadCalificativo) + (7 * cantidadCalificativo) + 3), Range)).Font.Bold = True
                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2 + (2 * cantidadCalificativo)), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 1 + (2 * cantidadCalificativo) + (7 * cantidadCalificativo) + 3), Range)).Font.Size = 21

                conTcolumnas1 += cantidadCalificativo * 7
                ''
                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2 + (2 * cantidadCalificativo) + (7 * cantidadCalificativo) + 3), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, (cantidadCursos * cantidadCalificativo) + 1), Range)).Merge(True)            ''
                '1 + (2 * cantidadCalificativo) + (7 * cantidadCalificativo) + (5 * cantidadCalificativo) + 6
                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2 + (2 * cantidadCalificativo) + (7 * cantidadCalificativo) + 3), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, (cantidadCursos * cantidadCalificativo) + 1), Range)).Value = String.Format("Date :{0} , {1} Year : {2}", MonthName(Date.Now().Month), Day(Date.Now()), Year(Date.Now()))

                excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, 2 + (2 * cantidadCalificativo) + (7 * cantidadCalificativo) + 3), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan - 1, (cantidadCursos * cantidadCalificativo) + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                For Each ogr As grupo In lstGrupo


                    lstPosicionesNoPintarSuperior.Add(filasEmpiezanTemp + 2)
                    lstPosicionesNoPintarInferior.Add(filasEmpiezanTemp - 1)
                    filasEmpiezanTemp += 2
                    For Each ocriterios As criterio In ogr.lstCiterio
                        filasEmpiezanTemp += 1
                    Next



                Next



                For Each ogrupo As grupo In lstGrupo
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).Value = ogrupo.nombreGrupo
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).Font.Size = 13
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).Font.Bold = True
                    lstPosgrupo.Add(filasEmpiezan)






                    contador = 0
                    columnasContador = 2
                    limInf = 0
                    limSup = 0
                    Dim contadorCalificativos As Integer = 1
                    For Each ocursos As cursos In lstCurso
                        contador += 1
                        If contador = 1 Then
                            limInf = columnasContador
                            limSup = (limInf + cantidadCalificativo) - 1
                        End If
                        If contador > 1 Then
                            limInf += cantidadCalificativo
                            limSup = (limInf + cantidadCalificativo) - 1
                        End If
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).Merge(True)
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).Value = ocursos.nombreCurso
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).RowHeight = 38
                        ''
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).Font.Size = 8


                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).WrapText = True
                        Dim contadorFilas As Integer = 0
                        ''
                        Dim contadorColumnasCalificativos As Integer = 0
                        For limfTemp As Integer = limInf To limSup

                            contadorColumnasCalificativos += 1

                            CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Value = dtCAlificativo.Rows(contadorFilas)("descr").ToString()
                            CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                            CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            ''
                            If contadorColumnasCalificativos = 1 Then
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(171, 171, 171)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).Color = RGB(0, 0, 0)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft).Color = RGB(0, 0, 0)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).Color = RGB(171, 171, 171)
                            ElseIf contadorColumnasCalificativos = cantidadCalificativo Then
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(171, 171, 171)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).Color = RGB(0, 0, 0)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft).Color = RGB(171, 171, 171)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).Color = RGB(0, 0, 0)
                                contadorColumnasCalificativos = 0
                            ElseIf contadorColumnasCalificativos > 1 And contadorColumnasCalificativos < cantidadCalificativo Then
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(171, 171, 171)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).Color = RGB(0, 0, 0)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft).Color = RGB(171, 171, 171)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, limfTemp), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).Color = RGB(171, 171, 171)
                            End If

                            ''
                            contadorFilas += 1
                            excel.ActiveSheet.Cells.Columns(limfTemp).ColumnWidth = 2

                        Next
                        For Each oposicion In ocursos.lstPosicion
                        Next
                    Next
                    ''
                    ''
                    CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, 1), Range).Value = ogrupo.tipoGrupo.ToUpper()

                    CType(excel.ActiveSheet.Cells(filasEmpiezan + 1, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    filasEmpiezan += 2
                    lstPosCalificativos.Add(filasEmpiezan - 1)
                    lstPosgrupo.Add(filasEmpiezan - 2)

                    Dim contColumnas As Integer = 1


                    For Each ocriterio As criterio In ogrupo.lstCiterio
                        CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).Value = ocriterio.nombreCriterio
                        contadorFilasGl += 1
                        CType(excel.ActiveSheet.Cells(filasEmpiezan, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        contColumnas = 1
                        Dim contadorCantidadCalificativos As Integer = 0
                        For ii As Integer = 1 To lstCurso.Count * cantidadCalificativo
                            contColumnas += 1
                            'CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            ''CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).ColorIndex = 45
                            ''CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders.Color = 8
                            contadorCantidadCalificativos += 1
                            If contadorCantidadCalificativos = 1 Then
                                'If lstPosgrupo.Contains(filasEmpiezan) Or lstPosCalificativos.Contains(filasEmpiezan) Then
                                '    CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).Color = RGB(0, 0, 0)
                                'Else
                                ' 
                                'End If
                                CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft).Color = RGB(0, 0, 0)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).Color = RGB(171, 171, 171)


                                If lstPosicionesNoPintarSuperior.Contains(filasEmpiezan) Then
                                    If lstPosicionesNoPintarSuperior(lstPosicionesNoPintarSuperior.Count - 1) = filasEmpiezan Then
                                        CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(0, 0, 0)

                                    Else
                                        CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).Color = RGB(0, 0, 0)
                                    End If
                                ElseIf lstPosicionesNoPintarInferior.Contains(filasEmpiezan) Then
                                    CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(0, 0, 0)

                                Else
                                    CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).Color = RGB(171, 171, 171)
                                    CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(171, 171, 171)
                                End If

                                '' cantidadCalificativo = 0
                            ElseIf contadorCantidadCalificativos = cantidadCalificativo Then
                                CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft).Color = RGB(171, 171, 171)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).Color = RGB(0, 0, 0)


                                If lstPosicionesNoPintarSuperior.Contains(filasEmpiezan) Then
                                    If lstPosicionesNoPintarSuperior(lstPosicionesNoPintarSuperior.Count - 1) = filasEmpiezan Then
                                        CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(0, 0, 0)

                                    Else
                                        CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).Color = RGB(0, 0, 0)
                                    End If


                                ElseIf lstPosicionesNoPintarInferior.Contains(filasEmpiezan) Then
                                    CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(0, 0, 0)
                                Else
                                    CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).Color = RGB(171, 171, 171)
                                    CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(171, 171, 171)
                                End If

                                contadorCantidadCalificativos = 0
                            ElseIf contadorCantidadCalificativos > 1 And contadorCantidadCalificativos < cantidadCalificativo Then
                                CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft).Color = RGB(171, 171, 171)
                                CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).Color = RGB(171, 171, 171)

                                If lstPosicionesNoPintarSuperior.Contains(filasEmpiezan) Then

                                    If lstPosicionesNoPintarSuperior(lstPosicionesNoPintarSuperior.Count - 1) = filasEmpiezan Then
                                        CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(0, 0, 0)
                                    Else
                                        CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).Color = RGB(0, 0, 0)
                                    End If
                                ElseIf lstPosicionesNoPintarInferior.Contains(filasEmpiezan) Then
                                    CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(0, 0, 0)
                                Else
                                    CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).Color = RGB(171, 171, 171)
                                    CType(excel.ActiveSheet.Cells(filasEmpiezan, contColumnas), Range).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Color = RGB(171, 171, 171)

                                End If

                            End If
                        Next
                        filasEmpiezan += 1
                    Next
                Next
                For Each ogrupo As grupo In lstGrupo
                    contador = 0
                    columnasContador = 2
                    limInf = 0
                    limSup = 0
                    For Each ocursos As cursos In lstCurso
                        contador += 1

                        If contador = 1 Then
                            limInf = columnasContador
                            limSup = (limInf + cantidadCalificativo) - 1
                        End If

                        If contador > 1 Then
                            limInf += cantidadCalificativo
                            limSup = (limInf + cantidadCalificativo) - 1
                        End If
                        Dim filasPosicion As Integer = 5
                        For Each oposicion In ocursos.lstPosicion
                            filasPosicion += 1
                            If lstPosgrupo.Contains(filasPosicion) Then
                                filasPosicion += 1
                            End If
                            If lstPosCalificativos.Contains(filasPosicion) Then
                                filasPosicion += 1
                            End If

                            If oposicion.ltPosicion <> 0 Then
                                CType(excel.ActiveSheet.Cells(filasPosicion, oposicion.ltPosicion), Range).Value = " X "
                                CType(excel.ActiveSheet.Cells(filasPosicion, oposicion.ltPosicion), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                                ''Color = 0;
                                ''CType(excel.ActiveSheet.Cells(filasPosicion, oposicion.ltPosicion), Range).ColorIndex()
                                ''   CType(excel.ActiveSheet.Cells(filasPosicion, oposicion.ltPosicion), Range).ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic

                                'With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                                '    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                                '    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                                '    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                            End If
                        Next
                    Next
                Next
                ''
                contadorFilasGl += 8 '+ 2
                Dim contadorColumnasDetalle As Integer = 2
                ''
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, 1), Range).Value = " Perfort Group "
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, 1), Range).Value = " Effort grade "
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, 1), Range).Value = " % Course Covered"
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, 1), Range).Value = " ACHIEVEMENTS"
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, 1), Range).Font.Size = 13
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, 1), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, 1), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, 1), Range).RowHeight = 188



                CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, 1), Range).Value = " DIFICULTADES"
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, 1), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, 1), Range).Font.Size = 13
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, 1), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, 1), Range).RowHeight = 188


                CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, 1), Range).Value = " RECOMENDACION"
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, 1), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, 1), Range).RowHeight = 188
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, 1), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, 1), Range).Font.Size = 13

                CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, 1), Range).Value = " Weak Students"
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, 1), Range).Font.Size = 13
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, 1), Range).RowHeight = 188
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, 1), Range).Font.Bold = True

                CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, 1), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                ''
                For Each ocurso As cursos In lstCurso
                Next
                ''
                contador = 0
                columnasContador = 2
                limInf = 0
                limSup = 0
                ''
                For Each ocursos As cursos In lstCurso
                    contador += 1
                    If contador = 1 Then
                        limInf = columnasContador
                        limSup = (limInf + cantidadCalificativo) - 1
                    End If
                    If contador > 1 Then
                        limInf += cantidadCalificativo
                        limSup = (limInf + cantidadCalificativo) - 1
                    End If
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limSup), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limSup), Range)).Value = ocursos.osumaryCabezera.perormaceGroupDescripcion
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limSup), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 1, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, limSup), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, limSup), Range)).Value = ocursos.osumaryCabezera.effortDescripcion
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, limSup), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 2, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, limSup), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, limSup), Range)).Value = ocursos.osumaryCabezera.coureCovered
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, limSup), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 3, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limSup), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limSup), Range)).Value = ocursos.osumaryCabezera.arch
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limSup), Range)).Font.Size = 8
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limSup), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limSup), Range)).WrapText = True

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limSup), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 4, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limSup), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limSup), Range)).Value = ocursos.osumaryCabezera.dificultad
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limSup), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limSup), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limSup), Range)).WrapText = True
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 5, limSup), Range)).Font.Size = 8

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limSup), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limSup), Range)).Value = ocursos.osumaryCabezera.rec
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limSup), Range)).Font.Size = 8
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limSup), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limSup), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 6, limSup), Range)).WrapText = True


                    contadorColumnasDetalle += 1
                Next

                contador = 0
                columnasContador = 2
                limInf = 0
                limSup = 0
                ''

                Dim sizesJaladoas As New List(Of Integer)
                Dim acSize As Integer = 0
                Dim contadorSize As Integer = 0
                For Each ocursos As cursos In lstCurso
                    contador += 1
                    If contador = 1 Then
                        limInf = columnasContador
                        limSup = (limInf + cantidadCalificativo) - 1
                    End If
                    If contador > 1 Then
                        limInf += cantidadCalificativo
                        limSup = (limInf + cantidadCalificativo) - 1
                    End If

                    Dim datosAlumnos As String = ""

                    acSize = 0
                    contadorSize = 0

                    For Each Ojalados As alumnosJalados In ocursos.lstJalados
                        datosAlumnos &= Ojalados.nombre & System.Environment.NewLine
                        acSize += 1
                    Next


                    sizesJaladoas.Add(acSize)


                    Dim maximos As Integer = 0
                    maximos = sizesJaladoas.Max()



                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limSup), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limSup), Range)).Value = datosAlumnos
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limSup), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft



                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limSup), Range)).WrapText = True
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limSup), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limSup), Range)).Font.Size = 8

                    ' excel.Application.Range(CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limInf), Range), CType(excel.ActiveSheet.Cells(contadorFilasGl + 7, limSup), Range)).RowHeight = (IIf(maximos = 0, 1, maximos)) * 50
                    ''.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop

                    ''    contadorSize = acSize

                Next



                'excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).Merge(True)
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).Value = ocursos.nombreCurso
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(filasEmpiezan, limInf), Range), CType(excel.ActiveSheet.Cells(filasEmpiezan, limSup), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.ActiveSheet.Cells.Columns(1).ColumnWidth = 21

                excel.Rows("3:3").Insert()
                excel.Rows("3:3").Insert()


                ' excel.Rows("2:2").Insert()
                excel.ActiveWindow.Zoom = 75



                '   CType(excel.ActiveSheet.Cells(33, 1), Range).Value = "pruebita "

                '.LeftHeader = ""
                '.CenterHeader = ""
                '.RightHeader = ""
                '.LeftFooter = ""
                '.CenterFooter = ""
                '.RightFooter = ""
                '.LeftMargin = excel.Application.InchesToPoints(0.7)
                '.RightMargin = excel.Application.InchesToPoints(0.7)
                '.TopMargin = excel.Application.InchesToPoints(0.75)
                '.BottomMargin =excel. Application.InchesToPoints(0.75)
                '.HeaderMargin = excel.Application.InchesToPoints(0.3)
                '.FooterMargin = excel.Application.InchesToPoints(0.3)
                '.PrintHeadings = False
                '.PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                '.PrintQuality = 600
                '.CenterHorizontally = False
                '.CenterVertically = False
                '.Orientation = xlPortrait
                '.Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = xlDownThenOver
                '.BlackAndWhite = False
                '.Zoom = False
                '.FitToPagesWide = 1
                '.FitToPagesTall = 0
                '.PrintErrors = xlPrintErrorsDisplayed
                '.OddAndEvenPagesHeaderFooter = False
                '.DifferentFirstPageHeaderFooter = False
                '.ScaleWithDocHeaderFooter = True
                '.AlignMarginsHeaderFooter = True
                '.EvenPage.LeftHeader.Text = ""
                '.EvenPage.CenterHeader.Text = ""
                '.EvenPage.RightHeader.Text = ""
                '.EvenPage.LeftFooter.Text = ""
                '.EvenPage.CenterFooter.Text = ""
                '.EvenPage.RightFooter.Text = ""
                '.FirstPage.LeftHeader.Text = ""
                '.FirstPage.CenterHeader.Text = ""
                '.FirstPage.RightHeader.Text = ""
                '.FirstPage.LeftFooter.Text = ""
                '.FirstPage.CenterFooter.Text = ""
                '.FirstPage.RightFooter.Text = ""
                With excel.ActiveSheet.PageSetup
                    ''''''''''''''''''''''''''''


                    .LeftHeader = ""
                    .CenterHeader = ""
                    .RightHeader = ""
                    .LeftFooter = ""
                    .CenterFooter = ""
                    .RightFooter = ""
                    .LeftMargin = excel.Application.InchesToPoints(0.7)
                    .RightMargin = excel.Application.InchesToPoints(0.7)
                    .TopMargin = excel.Application.InchesToPoints(0.75)
                    .BottomMargin = excel.Application.InchesToPoints(0.75)
                    .HeaderMargin = excel.Application.InchesToPoints(0.3)
                    .FooterMargin = excel.Application.InchesToPoints(0.3)
                    .PrintHeadings = False
                    .PrintGridlines = False

                    .PrintQuality = 600
                    .CenterHorizontally = False
                    .CenterVertically = False
                    .Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait
                    .Draft = False
                    .PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperLetter

                    .BlackAndWhite = False
                    .Zoom = False

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

                    '''''''''''''''''''''''''''''
                    '.LeftHeader = ""
                    '.CenterHeader = ""
                    '.RightHeader = ""
                    '.LeftFooter = ""
                    '.CenterFooter = ""
                    '.RightFooter = ""
                    '.LeftMargin = excel.Application.InchesToPoints(0.7)
                    '.RightMargin = excel.Application.InchesToPoints(0.7)
                    '.TopMargin = excel.Application.InchesToPoints(0.75)
                    '.BottomMargin = excel.Application.InchesToPoints(0.75)
                    '.HeaderMargin = excel.Application.InchesToPoints(0.3)
                    '.FooterMargin = excel.Application.InchesToPoints(0.3)
                    '.PrintHeadings = False
                    '.PrintGridlines = False
                    ''.PrintComments = xlPrintNoComments
                    '.PrintQuality = 600
                    '.CenterHorizontally = False
                    '.CenterVertically = False
                    '.Orientation = 2
                    '.Draft = False
                    ''.PaperSize = xlPaperLetter
                    ''.FirstPageNumber = xlAutomatic
                    ''.Order = OrderedDictionary xlDownThenOver
                    '.BlackAndWhite = False
                    '.Zoom = False
                    '.FitToPagesWide = False
                    '.FitToPagesTall = 1
                    ''.PrintErrors = xlPrintErrorsDisplayed
                    '.OddAndEvenPagesHeaderFooter = False
                    '.DifferentFirstPageHeaderFooter = False
                    '.ScaleWithDocHeaderFooter = True
                    '.AlignMarginsHeaderFooter = True
                    '.EvenPage.LeftHeader.Text = ""
                    '.EvenPage.CenterHeader.Text = ""
                    '.EvenPage.RightHeader.Text = ""
                    '.EvenPage.LeftFooter.Text = ""
                    '.EvenPage.CenterFooter.Text = ""
                    '.EvenPage.RightFooter.Text = ""
                    '.FirstPage.LeftHeader.Text = ""
                    '.FirstPage.CenterHeader.Text = ""
                    '.FirstPage.RightHeader.Text = ""
                    '.FirstPage.LeftFooter.Text = ""
                    '.FirstPage.CenterFooter.Text = ""
                    '.FirstPage.RightFooter.Text = ""
                    '.PrintTitleColumns = "$A:$B"
                End With
                wbkWorkbook.Save()
                EiminaReferencias(wshWorksheet)
                EiminaReferencias(wbkWorkbook)
                excel.Quit()
                EiminaReferencias(excel)
                System.GC.Collect()
                Return rutaREpositorioTemporales
            Catch ex As Exception

            End Try


            ''EXEC USP_REP_SEG_SUMARY null, 100,5,1  




        Catch ex As Exception

        End Try

    End Function
    ''
#Region "Funciones para crear listas class summary report"

    ''
   
    Function crearGrupos(ByVal dt_grupo As System.Data.DataTable, ByVal dt_Reporte As System.Data.DataTable) As IEnumerable(Of grupo)
        Dim lstGrupo As New List(Of grupo)

        Dim lstGrupoEnumeracion As IEnumerable(Of grupo)

        Dim ogrupo As grupo
        Dim primerCodigoAsignacionGrupo As Integer
        Try
            Dim ocriterio As criterio
            primerCodigoAsignacionGrupo = Integer.Parse(dt_Reporte.Rows(0)("AGC_CodigoAsignacionGrupo").ToString())

            ''pos	CF_CodigoCalificativo	CE_CodigoCriterio	grupoDescripcion	grupoCodigo	AGC_CodigoAsignacionGrupo	nombreCurso	BM_CodigoBimestre	AC_CodigoAnioAcademico	CE_DescripcionCriterio	PG_Descripcion	EG_Descripcion	CSRC_CodigoCabeceraClassSumaryReport	CSRC_CourseCovered
            ''2	12	33	Ninguno	3	78	Mathematics	1	2	Quality of Learning	2 - Débil	B-muy bueno	1	100
            Dim dtCriterio As System.Data.DataRow()
            For Each filas As System.Data.DataRow In dt_grupo.Rows
                ogrupo = New grupo
                ogrupo.codGrupo = CInt(filas("GCE_CodigoGrupoCriterio").ToString())
                ogrupo.nombreGrupo = filas("GCE_Descripcion").ToString()
                ogrupo.tipoGrupo = filas("tipoGrupo").ToString()

                dtCriterio = dt_Reporte.Select("AGC_CodigoAsignacionGrupo =" & primerCodigoAsignacionGrupo & " and grupoCodigo = " & ogrupo.codGrupo)

                For Each filasCriterio As System.Data.DataRow In dtCriterio
                    ocriterio = New criterio
                    ocriterio.codCriterio = CInt(filasCriterio("CE_CodigoCriterio").ToString())
                    ocriterio.nombreCriterio = filasCriterio("CE_DescripcionCriterio").ToString()
                    ogrupo.lstCiterio.Add(ocriterio)
                Next
                lstGrupo.Add(ogrupo)
            Next



            lstGrupoEnumeracion = From h In lstGrupo Order By h.codGrupo Descending Select h

            Return lstGrupoEnumeracion
        Catch ex As Exception

        End Try
    End Function

    Function fcrearListaCursos(ByVal dt_curso As System.Data.DataTable, ByVal dt_reporte As System.Data.DataTable, ByVal dt_Jalados As System.Data.DataTable) As List(Of cursos)
        Dim ocurso As cursos
        Dim opos As posicion
        Dim lstCurso As New List(Of cursos)
        Dim dtConsultaCriterio As System.Data.DataRow()
        Dim pocisionesCurso As Integer = 0
        Try
            For Each filasCriterio As System.Data.DataRow In dt_curso.Rows
                pocisionesCurso += 1
                ocurso = New cursos
                ocurso.codCurso = CInt(filasCriterio("AGC_CodigoAsignacionGrupo").ToString())
                ocurso.nombreCurso = filasCriterio("NC_Descripcion").ToString()
                dtConsultaCriterio = dt_reporte.Select("AGC_CodigoAsignacionGrupo=" & ocurso.codCurso)

                For Each filasCriterio1 As System.Data.DataRow In dtConsultaCriterio
                    opos = New posicion
                    opos.ltPosicion = CInt(filasCriterio1("pos").ToString())
                    ocurso.lstPosicion.Add(opos)
                Next
                ocurso.cantidad = ocurso.lstPosicion.Count
                ocurso.posCurso = pocisionesCurso
                lstCurso.Add(ocurso)
            Next
            'For Each filasJalados As System.Data.DataRow In dt_Jalados.Rows
            'Next
            Dim dtJalados As System.Data.DataRow()
            Dim oalumnosJalados As alumnosJalados


            For Each ocuros As cursos In lstCurso
                dtJalados = dt_Jalados.Select("AGC_CodigoAsignacionGrupo=" & ocuros.codCurso)
                For Each filasJalados As System.Data.DataRow In dtJalados

                    oalumnosJalados = New alumnosJalados
                    oalumnosJalados.nombre = filasJalados("nombre").ToString()
                    oalumnosJalados.promedio = filasJalados("promedioFinal").ToString()
                    ocuros.lstJalados.Add(oalumnosJalados)
                Next
                'nombre	promedioFinal	AGC_CodigoAsignacionGrupo
                ''NAVARRO CABRERA Fiorella	10.00	78
            Next

            '' Environment.NewLine 

            'Dim lstTemp As IEnumerable(Of cursos)
            'lstTemp = From h In lstCurso Order By h.cantidad Descending Select h

            Return lstCurso '' lstTemp

        Catch ex As Exception
        Finally

        End Try
        '  IEnumerable<Empleado> empleadosBuscados = from busqueda in personal 
        '                                          orderby busqueda.Nombre descending 
        'where(busqueda.Edad > 40)
        '                                          select busqueda;



    End Function

    Function fAsignarDetalleClassSumary(ByVal lstCurso As IEnumerable(Of cursos), ByVal dt_summary As System.Data.DataTable) As IEnumerable(Of cursos)
        Dim lstCursoTemp As IEnumerable(Of cursos)
        Dim osumaryCabezera As sumaryCabezera
        Try

            'SG_PerformanceGroup.PG_Abrev,       
            'SG_EffortGrade.EG_Abrev, 


            For indice As Integer = 0 To lstCurso.Count - 1
                For Each filasSummary As System.Data.DataRow In dt_summary.Rows
                    If lstCurso(indice).codCurso = CInt(filasSummary("AGC_CodigoAsignacionGrupo").ToString()) Then
                        osumaryCabezera = New sumaryCabezera
                        osumaryCabezera.perormaceGroupDescripcion = filasSummary("PG_Abrev").ToString()
                        osumaryCabezera.effortDescripcion = filasSummary("EG_Abrev").ToString()
                        osumaryCabezera.coureCovered = CInt(filasSummary("CSRC_CourseCovered").ToString())

                        osumaryCabezera.dificultad = filasSummary("CSRC_Dificultades").ToString()
                        osumaryCabezera.rec = filasSummary("CSRC_Recomendaciones").ToString()
                        osumaryCabezera.arch = filasSummary("CSRC_Archievements").ToString()




                        ' SG_ClassSumaryReportCabecera.CSRC_Dificultades, SG_ClassSumaryReportCabecera.CSRC_Recomendaciones, 
                        'SG_ClassSumaryReportCabecera.CSRC_Archievements()

                        ''  PG_Descripcion	EG_Descripcion	CSRC_CodigoCabeceraClassSumaryReport	CSRC_CourseCovered	AGC_CodigoAsignacionGrupo	AC_CodigoAnioAcademico	BM_CodigoBimestre
                        lstCurso(indice).osumaryCabezera = osumaryCabezera
                    End If
                Next
            Next
            Return lstCurso
        Catch ex As Exception
        Finally
        End Try
    End Function


    ''
#End Region
#Region "entidades reporte calss summary report "
    Public Class cursos
        Public codCurso As Integer
        Public nombreCurso As String
        Public posCurso As Integer
        Public lstPosicion As New List(Of posicion)
        Public cantidad As Integer
        Public osumaryCabezera As New sumaryCabezera
        Public lstJalados As New List(Of alumnosJalados)
    End Class

    Public Class alumnosJalados
        Public nombre As String
        Public promedio As String
    End Class

    Public Class posicion
        Public ltPosicion As Integer
    End Class
    Public Class sumaryCabezera
        Public perormaceGroupDescripcion As String
        Public effortDescripcion As String
        Public coureCovered As Integer
        Public dificultad As String
        Public rec As String
        Public arch As String

    End Class
    Public Class grupo
        Public nombreGrupo As String
        Public codGrupo As Integer
        Public tipoGrupo As String
        Public lstCiterio As New List(Of criterio)
    End Class
    Public Class criterio
        Public nombreCriterio As String
        Public codCriterio As Integer
    End Class

    Public Class calificativoSummaryReport
        Public nombreCalificativo As String
    End Class
#End Region

    ''' <summary>
    ''' reporte de comentario de primaria y secundaria 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function reporteComentarioPrimariaSecundaria(ByVal int_tipoReporte As Integer, ByVal codAsignacinAula As Integer, ByVal codBimestre As Integer) As String
        Try
            Dim abrBimestre As String = ""
            If codBimestre = 1 Then
                abrBimestre = "I"
            End If
            If codBimestre = 2 Then
                abrBimestre = "II"
            End If
            If codBimestre = 3 Then
                abrBimestre = "III"
            End If
            If codBimestre = 4 Then
                abrBimestre = "IV"
            End If


            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("plantillaComentario")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)


            Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
            Dim dst As System.Data.DataSet
            ''REP_USP_Comentarios 32,1
            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range
            If int_tipoReporte = 57 Then
                dst = obl_rep_libretaNotas.FUN_LIS_REP_ReporteLibretaComentarioPrimaria(codAsignacinAula, codBimestre)
            ElseIf int_tipoReporte = 58 Then
                dst = obl_rep_libretaNotas.FUN_LIS_REP_ReporteLibretaComentarioSecundaria(codAsignacinAula, codBimestre)
            End If
            ''   dst = obl_rep_libretaNotas.FUN_LIS_REP_ReporteLibretaComentarioPrimaria(32, 1)

            Dim lstpersonaComentario As New List(Of personaComentario)
            Dim lstcursoComentario As New List(Of cursoComentario)
            Dim dtAlumnos As New System.Data.DataTable
            dtAlumnos = dst.Tables(0)

            Dim dtNombreTutor As New System.Data.DataTable
            dtNombreTutor = dst.Tables(5)

            Dim dtNombreAula As New System.Data.DataTable
            dtNombreAula = dst.Tables(3)

            Dim dtNombreGrado As New System.Data.DataTable
            dtNombreGrado = dst.Tables(4)


            Dim dtCursos As New System.Data.DataTable
            dtCursos = dst.Tables(1)
            lstcursoComentario = crearListaCurso(dtCursos)

            Dim dtUbicaciones As New System.Data.DataTable
            dtUbicaciones = dst.Tables(2)

            ''plantillaComentario

            lstpersonaComentario = crearListaPersona(dtAlumnos, dtUbicaciones)
            ''repAlumnoComentario.xlsx
            Dim rutaPlantilla As String = ""

            wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)
            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()
            wshWorksheet.Visible = True
            Dim filasAlumnos As Integer = 8
            Dim columnasCursos As Integer = 2





            '' CType(excel.ActiveSheet.Cells(5, 2), Range).Value = "REPORTE DE COMENTARIOS " & dtNombreGrado.Rows(0)("GD_Descripcion").ToString() & " " & dtNombreAula.Rows(0)("nombreAula").ToString() & " " & abrBimestre



            excel.Application.Range(CType(excel.Cells(6, 1), Range), CType(excel.Cells(6, dtCursos.Rows.Count + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.Cells(6, 1), Range), CType(excel.Cells(6, dtCursos.Rows.Count + 1), Range)).Value = "REPORTE DE COMENTARIOS "
            excel.Application.Range(CType(excel.Cells(6, 1), Range), CType(excel.Cells(6, dtCursos.Rows.Count + 1), Range)).RowHeight = 45
            excel.Application.Range(CType(excel.Cells(6, 1), Range), CType(excel.Cells(6, dtCursos.Rows.Count + 1), Range)).Font.Size = 25
            excel.Application.Range(CType(excel.Cells(6, 1), Range), CType(excel.Cells(6, dtCursos.Rows.Count + 1), Range)).Font.Bold = True

            excel.Application.Range(CType(excel.Cells(6, 1), Range), CType(excel.Cells(6, dtCursos.Rows.Count + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            excel.Application.Range(CType(excel.Cells(6, 1), Range), CType(excel.Cells(6, dtCursos.Rows.Count + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter



            excel.Application.Range(CType(excel.Cells(7, 1), Range), CType(excel.Cells(7, 2), Range)).Merge(True)
            excel.Application.Range(CType(excel.Cells(7, 1), Range), CType(excel.Cells(7, 2), Range)).Value = "TUTOR :" & dtNombreTutor.Rows(0)("nombreTutor").ToString()
            excel.Application.Range(CType(excel.Cells(7, 1), Range), CType(excel.Cells(7, 2), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            excel.Application.Range(CType(excel.Cells(7, 3), Range), CType(excel.Cells(7, 4), Range)).Merge(True)
            excel.Application.Range(CType(excel.Cells(7, 3), Range), CType(excel.Cells(7, 4), Range)).Value = dtNombreGrado.Rows(0)("GD_Descripcion").ToString() & " " & dtNombreAula.Rows(0)("nombreAula").ToString() & " TERM/SEMESTER :" & abrBimestre & ", " & Day(Date.Now).ToString & " , " & Month(Date.Now) & " , " & Year(Date.Now).ToString()
            excel.Application.Range(CType(excel.Cells(7, 3), Range), CType(excel.Cells(7, 4), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


            excel.Application.Range(CType(excel.Cells(7, 5), Range), CType(excel.Cells(7, dtCursos.Rows.Count + 1), Range)).Merge(True)
            excel.Application.Range(CType(excel.Cells(7, 5), Range), CType(excel.Cells(7, dtCursos.Rows.Count + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            For Each ocursoComentario As cursoComentario In lstcursoComentario
                CType(excel.ActiveSheet.Cells(filasAlumnos, columnasCursos), Range).Value = ocursoComentario.nombreCurso.ToUpper()

                CType(excel.ActiveSheet.Cells(filasAlumnos, columnasCursos), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(filasAlumnos, columnasCursos), Range).WrapText = True
                CType(excel.ActiveSheet.Cells(filasAlumnos, columnasCursos), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filasAlumnos, columnasCursos), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                CType(excel.ActiveSheet.Cells(filasAlumnos, columnasCursos), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                columnasCursos += 1
            Next
            ''

            filasAlumnos += 1
            CType(excel.ActiveSheet.Cells(8, 1), Range).Value = "Nombre y Apellido "
            CType(excel.ActiveSheet.Cells(8, 1), Range).Font.Bold = True
            CType(excel.ActiveSheet.Cells(8, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            For Each opersonaComentario As personaComentario In lstpersonaComentario
                Dim cl As Integer = 2
                CType(excel.ActiveSheet.Cells(filasAlumnos, 1), Range).Value = opersonaComentario.nombrePersona
                CType(excel.ActiveSheet.Cells(filasAlumnos, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filasAlumnos, 1), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop

                For Each ocomentarioCurso As comentarioCurso In opersonaComentario.lstComentario
                    CType(excel.ActiveSheet.Cells(filasAlumnos, ocomentarioCurso.pocision), Range).Value = ocomentarioCurso.comentario
                    CType(excel.ActiveSheet.Cells(filasAlumnos, ocomentarioCurso.pocision), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    CType(excel.ActiveSheet.Cells(filasAlumnos, ocomentarioCurso.pocision), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                    CType(excel.ActiveSheet.Cells(filasAlumnos, ocomentarioCurso.pocision), Range).WrapText = True
                    CType(excel.ActiveSheet.Cells(filasAlumnos, ocomentarioCurso.pocision), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    If ocomentarioCurso.comentario.Trim.Length > 40 Then
                        excel.ActiveSheet.Cells.Columns(ocomentarioCurso.pocision).ColumnWidth = 35
                    Else
                        excel.ActiveSheet.Cells.Columns(ocomentarioCurso.pocision).ColumnWidth = 35
                        '' excel.Columns(ocomentarioCurso.pocision).EntireColumn.AutoFit()

                        ''CType(excel.ActiveSheet.Cells(filasAlumnos, ocomentarioCurso.pocision), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    End If
                    ''
                Next
                For Each ocomentarioCurso As comentarioCurso In opersonaComentario.lstComentario
                    CType(excel.ActiveSheet.Cells(filasAlumnos, cl), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Columns(cl).EntireColumn.AutoFit()
                    excel.ActiveSheet.Cells.Columns(cl).ColumnWidth = 35
                    cl += 1
                Next




                filasAlumnos += 1
            Next

            excel.Columns(1).EntireColumn.AutoFit()
            wbkWorkbook.Save()


            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

            Return rutaREpositorioTemporales


        Catch ex As Exception
        Finally

        End Try

    End Function

    Function crearReporteLibretaRevisionInicial() As String

        ''plantillaDscriptores.xlsx
        Try
            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range
            Dim rutaREpositorioTemporales As String = ""
            ''plantillaLibretaRevision

            ''asignascion aula 26
            ''
            Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
            Dim dt As New System.Data.DataTable


            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("plantillaLibretaRevision")

            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            rutaREpositorioTemporales = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)


            Dim dst As New DataSet

            dst = obl_rep_libretaNotas.FUN_LIS_REP_ReporteIncialRevision(cmbSalonPrimariaInicial.SelectedValue, DropDownList3.SelectedValue, cmbCurso.SelectedValue, 1, 2, 1, 1)

            Dim abrBimestre As String = ""
            If DropDownList3.SelectedValue = 1 Then
                abrBimestre = "I"
            End If
            If DropDownList3.SelectedValue = 2 Then
                abrBimestre = "II"
            End If
            If DropDownList3.SelectedValue = 3 Then
                abrBimestre = "III"
            End If
            If DropDownList3.SelectedValue = 4 Then
                abrBimestre = "IV"
            End If




            dt = dst.Tables(0)
            Dim dtNombreTutor As New System.Data.DataTable
            dtNombreTutor = dst.Tables(1)


            Dim lst As New List(Of persona)
            lst = listaNotasRegistros(dt)

            Dim nombreTutor As String = ""

            nombreTutor = dtNombreTutor.Rows(0)("nombre").ToString()



            ''crear cabezera
            Dim filaCabezeara As Integer = 4
            Dim contPersona As Integer = 0
            Dim contColumnas As Integer = 4

            CType(excel.ActiveSheet.Cells(3, 1), Range).Value = "TUTOR:"
            CType(excel.ActiveSheet.Cells(3, 2), Range).Value = nombreTutor

            For Each oPersona As persona In lst

                If contPersona = 1 Then
                    Exit For
                End If


                CType(excel.ActiveSheet.Cells(4, 3), Range).Value = "PB"
                CType(excel.ActiveSheet.Cells(4, 3), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(4, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(4, 3), Range).Interior.Color() = RGB(230, 230, 230)


                CType(excel.ActiveSheet.Cells(4, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(4, 2), Range).Value = "APELLIDOS Y NOMBRES "
                CType(excel.ActiveSheet.Cells(4, 2), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(4, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(4, 1), Range).Value = "Cod."
                CType(excel.ActiveSheet.Cells(4, 1), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                CType(excel.ActiveSheet.Cells(4, 1), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(4, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                For Each notaComponente In oPersona.lstNotaComponente

                    For Each oNotaInd As notaIndicador In notaComponente.lstNotaIndicador

                        For Each onotaSub As notaSubIndicador In oNotaInd.lstNotaSubinidcador

                            CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Value = "PS"
                            CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Font.Bold = True

                            CType(excel.ActiveSheet.Cells(4, contColumnas), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                            CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            excel.ActiveSheet.Cells.Columns(contColumnas).ColumnWidth = 5


                            contColumnas += 1
                            'objXL.selection.Interior.ColorIndex = 41
                            'objXL.selection.Interior.Pattern = 1
                            'objXL.selection.Font.ColorIndex = 2



                        Next

                        CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Value = "PI"
                        CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Font.Bold = True
                        CType(excel.ActiveSheet.Cells(4, contColumnas), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        excel.ActiveSheet.Cells.Columns(contColumnas).ColumnWidth = 5
                        CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Interior.Color() = RGB(255, 255, 225)

                        contColumnas += 1
                    Next

                    CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Value = "PC"
                    excel.ActiveSheet.Cells.Columns(contColumnas).ColumnWidth = 5
                    CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Font.Bold = True
                    CType(excel.ActiveSheet.Cells(4, contColumnas), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(4, contColumnas), Range).Interior.Color() = RGB(192, 220, 192)


                    contColumnas += 1
                Next


                contPersona += 1

            Next



            Dim fila As Integer = 4
            Dim colPersona As Integer = 4

            Dim indicePersona As Integer = 0
            For Each oPersona As persona In lst
                indicePersona += 1
                fila += 1

                CType(excel.ActiveSheet.Cells(fila, 1), Range).Value = indicePersona
                CType(excel.ActiveSheet.Cells(fila, 1), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                CType(excel.ActiveSheet.Cells(fila, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fila, 2), Range).Value = oPersona.nombrepersona
                CType(excel.ActiveSheet.Cells(fila, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fila, 3), Range).Value = oPersona.promedio


                CType(excel.ActiveSheet.Cells(fila, 3), Range).Interior.Color() = RGB(230, 230, 230)
                CType(excel.ActiveSheet.Cells(fila, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                CType(excel.ActiveSheet.Cells(fila, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                colPersona = 4
                For Each notaComponente In oPersona.lstNotaComponente


                    For Each oNotaInd As notaIndicador In notaComponente.lstNotaIndicador


                        For Each onotaSub As notaSubIndicador In oNotaInd.lstNotaSubinidcador
                            CType(excel.ActiveSheet.Cells(fila, colPersona), Range).Value = onotaSub.notaSubIndicador.ToUpper()
                            CType(excel.ActiveSheet.Cells(fila, colPersona), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            CType(excel.ActiveSheet.Cells(fila, colPersona), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


                            colPersona += 1
                        Next
                        CType(excel.ActiveSheet.Cells(fila, colPersona), Range).Value = oNotaInd.notaIndicador.ToUpper()
                        CType(excel.ActiveSheet.Cells(fila, colPersona), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        CType(excel.ActiveSheet.Cells(fila, colPersona), Range).Interior.Color() = RGB(255, 255, 225)
                        CType(excel.ActiveSheet.Cells(fila, colPersona), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        colPersona += 1

                    Next
                    CType(excel.ActiveSheet.Cells(fila, colPersona), Range).Value = notaComponente.notaComponente.ToUpper()
                    CType(excel.ActiveSheet.Cells(fila, colPersona), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    CType(excel.ActiveSheet.Cells(fila, colPersona), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(fila, colPersona), Range).Interior.Color() = RGB(192, 220, 192)
                    colPersona += 1
                Next



            Next


            excel.ActiveSheet.Cells.Columns(2).ColumnWidth = 45
            excel.ActiveSheet.Cells.Columns(1).ColumnWidth = 5
            excel.ActiveSheet.Cells.Columns(3).ColumnWidth = 5


            With excel.ActiveSheet.PageSetup
                .LeftHeader = ""
                .CenterHeader = ""
                .RightHeader = ""
                .LeftFooter = ""
                .CenterFooter = ""
                .RightFooter = ""
                .LeftMargin = excel.Application.InchesToPoints(0.7)
                .RightMargin = excel.Application.InchesToPoints(0.7)
                .TopMargin = excel.Application.InchesToPoints(0.75)
                .BottomMargin = excel.Application.InchesToPoints(0.75)
                .HeaderMargin = excel.Application.InchesToPoints(0.3)
                .FooterMargin = excel.Application.InchesToPoints(0.3)
                .PrintHeadings = False
                .PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                .PrintQuality = 600
                .CenterHorizontally = False
                .CenterVertically = False
                .Orientation = 2
                .Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = OrderedDictionary xlDownThenOver
                .BlackAndWhite = False
                .Zoom = False
                .FitToPagesWide = False
                .FitToPagesTall = 1
                '.PrintErrors = xlPrintErrorsDisplayed
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
                .PrintTitleColumns = "$A:$B"
            End With


            wbkWorkbook.Save()





            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

            'Dim lPersona As New List(Of persona)
            'lPersona = listaNotasRegistros(dst.Tables(0))


            Return rutaREpositorioTemporales

        Catch ex As Exception

        Finally

        End Try
    End Function

#Region "CREAR LIBRETA SECUNDARIA"
    Function crearLibretaSecundaria() As String
        Try
            Dim obj_BL_RegistroNotasCriterios As New bl_RegistroNotasCriterios
            Dim dtsRegistroNotasAlumnos As New System.Data.DataSet

            dtsRegistroNotasAlumnos = obj_BL_RegistroNotasCriterios.FUN_LIS_CU_RegistroNotasCriteriosyEvaluacionesSecundaria(1, 1, 22, 1, 1, 1, 1)
            Dim lstNotasCuantitativas As New List(Of be_AlumnoNotaCuantitativa)
            '        lstNotasCuantitativas = listaNotasRegistrosSecundaria(dtsRegistroNotasAlumnos.Tables(0))


            '        Dim excel As New ApplicationClass
            '        Dim wbkWorkbook As Workbook
            '        Dim wshWorksheet As Worksheet
            '        Dim rng As Range

            '        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("plantillaLibretaRevisionSecundaria")
            '        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

            '        ''  <add key="RutaPlantillaLibretaInicial" value="\Plantillas\ExportacionLibreta\libretaInicial.xlsx"/>
            '        Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"

            '        File.Copy(rutaPlantillas, rutaREpositorioTemporales)


            '        wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)


            '        wbkWorkbook.Save()


            '        wshWorksheet = wbkWorkbook.Worksheets(1)
            '        wshWorksheet.Visible = XlSheetVisibility.xlSheetVisible
            '        wshWorksheet.Activate()



            '        EiminaReferencias(wshWorksheet)
            '        EiminaReferencias(wbkWorkbook)
            '        excel.Quit()
            '        EiminaReferencias(excel)
            '        System.GC.Collect()
            '        ''.CS_CodigoCurso = 22) AND (CU_AsignacionGruposCursos.AGC_CodigoAsignacionGrupo = 113)


        Catch ex As Exception
        Finally

        End Try

    End Function
#End Region


#Region " UTILIDADES LIBRETA SECUNDARIA "


    ''funciones para crear  metodos  libreta secundaria 
    Shared Function listaNotasRegistrosSecundaria(ByVal tbl_NotasRegistro As System.Data.DataTable) As List(Of be_AlumnoNotaCuantitativa)
        Dim lstPersonas As New List(Of be_AlumnoNotaCuantitativa)
        Dim obe_AlumnoNota As be_AlumnoNotaCuantitativa
        Dim obe_NotaCriterio As be_NotaCriterio
        Dim obe_NotaEvaluacion As be_NotaEvaluacion
        Dim str_CodigoAlumnoTemp As String = ""

        Dim lstPersonasAux As New List(Of be_AlumnoNotaCuantitativa)
        Dim lstNotasCriterio As New List(Of be_NotaCriterio)
        Dim lstNotasEvaluacion As New List(Of be_NotaEvaluacion)

        lstPersonasAux = crearListaAlumnosSecundaria(tbl_NotasRegistro)
        lstNotasCriterio = crearListaCriteriosSecundaria(tbl_NotasRegistro)
        lstNotasEvaluacion = crearListaEvaluacionesSecundaria(tbl_NotasRegistro)

        For Each op As be_AlumnoNotaCuantitativa In lstPersonasAux
            obe_AlumnoNota = New be_AlumnoNotaCuantitativa
            obe_AlumnoNota.CodigoAlumno = op.CodigoAlumno
            obe_AlumnoNota.CodigoRegistroBimestral = op.CodigoRegistroBimestral
            obe_AlumnoNota.CodigoAsignacionGrupo = op.CodigoAsignacionGrupo
            obe_AlumnoNota.anioAcademico = op.anioAcademico
            obe_AlumnoNota.CodigoBimestre = op.CodigoBimestre
            obe_AlumnoNota.NombreCompletoAlumno = op.NombreCompletoAlumno
            obe_AlumnoNota.ObservacionCurso = op.ObservacionCurso
            obe_AlumnoNota.Href = op.Href
            obe_AlumnoNota.Promedio = op.Promedio
            For Each onotaCri As be_NotaCriterio In lstNotasCriterio
                obe_NotaCriterio = New be_NotaCriterio
                obe_NotaCriterio.CodigoAlumno = onotaCri.CodigoAlumno
                obe_NotaCriterio.nombreCriterio = onotaCri.nombreCriterio
                obe_NotaCriterio.CodigoRegistroNotaCriterio = onotaCri.CodigoRegistroNotaCriterio
                obe_NotaCriterio.notaCriterio = onotaCri.notaCriterio
                If obe_AlumnoNota.CodigoAlumno = obe_NotaCriterio.CodigoAlumno Then
                    For Each oNotaEva As be_NotaEvaluacion In lstNotasEvaluacion
                        obe_NotaEvaluacion = New be_NotaEvaluacion
                        obe_NotaEvaluacion.nombreEvaluacion = oNotaEva.nombreEvaluacion
                        obe_NotaEvaluacion.CodigoRegistroNotaCriterio = oNotaEva.CodigoRegistroNotaCriterio
                        obe_NotaEvaluacion.CodigoRegistroNotaEvaluacion = oNotaEva.CodigoRegistroNotaEvaluacion
                        obe_NotaEvaluacion.notaEvaluacion = oNotaEva.notaEvaluacion
                        If obe_NotaEvaluacion.CodigoRegistroNotaCriterio = obe_NotaCriterio.CodigoRegistroNotaCriterio Then
                            obe_NotaCriterio.lstNotaEvaluacion.Add(obe_NotaEvaluacion)
                        End If
                    Next
                    obe_AlumnoNota.lstNotaCriterio.Add(obe_NotaCriterio)
                End If
            Next
            lstPersonas.Add(obe_AlumnoNota)
        Next
        Return lstPersonas

    End Function
    Shared Function crearListaAlumnosSecundaria(ByVal dt_ListaNotas As System.Data.DataTable) As List(Of be_AlumnoNotaCuantitativa)
        Try
            Dim lstPersonas As New List(Of be_AlumnoNotaCuantitativa)
            Dim obe_AlumnoNotaCuantitavia As be_AlumnoNotaCuantitativa
            Dim str_CodigoAlumnoAux As String = ""
            For Each dr As DataRow In dt_ListaNotas.Rows
                obe_AlumnoNotaCuantitavia = New be_AlumnoNotaCuantitativa
                obe_AlumnoNotaCuantitavia.CodigoAlumno = dr("CodigoAlumno").ToString()
                obe_AlumnoNotaCuantitavia.NombreCompletoAlumno = dr("NombreCompletoAlumno").ToString()
                obe_AlumnoNotaCuantitavia.CodigoRegistroBimestral = dr("CodigoRegistroBimestral").ToString()
                obe_AlumnoNotaCuantitavia.CodigoAsignacionGrupo = dr("CodigoAsignacionGrupo").ToString()
                obe_AlumnoNotaCuantitavia.CodigoBimestre = dr("CodigoBimestre").ToString()
                obe_AlumnoNotaCuantitavia.anioAcademico = dr("AnioAcademico").ToString()
                obe_AlumnoNotaCuantitavia.ObservacionCurso = dr("OBservacionCurso").ToString()
                obe_AlumnoNotaCuantitavia.Href = dr("RutaFoto").ToString()
                obe_AlumnoNotaCuantitavia.Promedio = dr("NotaFinalBimestre").ToString()

                If dr("CodigoAlumno") <> str_CodigoAlumnoAux Then
                    lstPersonas.Add(obe_AlumnoNotaCuantitavia)
                End If
                str_CodigoAlumnoAux = obe_AlumnoNotaCuantitavia.CodigoAlumno
            Next
            Return lstPersonas
        Catch ex As Exception

        End Try
    End Function
    Shared Function crearListaCriteriosSecundaria(ByVal dt_ListaNotas As System.Data.DataTable) As List(Of be_NotaCriterio)
        Dim lstNotasCriterio As New List(Of be_NotaCriterio)
        Dim obe_NotaCriterio As be_NotaCriterio
        Dim str_CodigoCriterioAux As String
        For Each dr As DataRow In dt_ListaNotas.Rows
            obe_NotaCriterio = New be_NotaCriterio
            obe_NotaCriterio.CodigoRegistroNotaCriterio = Convert.ToInt32(dr("CodigoRegistroNotasCriterios").ToString())
            obe_NotaCriterio.nombreCriterio = dr("Criterio").ToString()
            obe_NotaCriterio.CodigoAlumno = dr("CodigoAlumno").ToString()
            obe_NotaCriterio.notaCriterio = dr("NotaCriterio").ToString()
            If obe_NotaCriterio.CodigoRegistroNotaCriterio = 0 Then
                Continue For
            End If
            If obe_NotaCriterio.CodigoRegistroNotaCriterio <> str_CodigoCriterioAux Then
                lstNotasCriterio.Add(obe_NotaCriterio)
            End If
            str_CodigoCriterioAux = obe_NotaCriterio.CodigoRegistroNotaCriterio
        Next
        Return lstNotasCriterio
        Try
        Catch ex As Exception
        End Try
    End Function
    Shared Function crearListaEvaluacionesSecundaria(ByVal dt_ListaNotas As System.Data.DataTable) As List(Of be_NotaEvaluacion)
        Try
            Dim lstNotasEvaluacion As New List(Of be_NotaEvaluacion)
            Dim obe_NotaEvaluacion As be_NotaEvaluacion
            Dim str_CodigoEvaluacionAux As String
            For Each dr As DataRow In dt_ListaNotas.Rows
                obe_NotaEvaluacion = New be_NotaEvaluacion
                obe_NotaEvaluacion.CodigoRegistroNotaCriterio = Convert.ToInt32(dr("CodigoRegistroNotasCriterios").ToString())
                obe_NotaEvaluacion.CodigoRegistroNotaEvaluacion = Convert.ToInt32(dr("CodigoRegistroNotaEvaluacion").ToString())
                obe_NotaEvaluacion.nombreEvaluacion = dr("Evaluaciones").ToString()
                obe_NotaEvaluacion.notaEvaluacion = dr("NotaEvaluacion").ToString()
                If obe_NotaEvaluacion.CodigoRegistroNotaEvaluacion = 0 Then
                    Continue For
                End If
                If obe_NotaEvaluacion.CodigoRegistroNotaEvaluacion <> str_CodigoEvaluacionAux Then
                    lstNotasEvaluacion.Add(obe_NotaEvaluacion)
                End If
                str_CodigoEvaluacionAux = obe_NotaEvaluacion.CodigoRegistroNotaEvaluacion
            Next
            Return lstNotasEvaluacion
        Catch ex As Exception

        End Try
    End Function


    ''
#End Region
    ''
    Function listaNotasRegistros(ByVal tbl_NotasRegistro As System.Data.DataTable) As List(Of persona)
        Dim lstPersona As New List(Of persona)
        Dim opersona As persona
        Dim onotaComponente As notaComponente
        Dim onotaIndicador As notaIndicador
        Dim onotaSubIndicador As notaSubIndicador
        Dim codAlumnoTemp As String = ""

        Dim codCompTemp As Integer
        Dim codIndTemp As Integer
        Dim codSubInd As Integer

        Dim lstPersona1 As New List(Of persona)
        Dim lstNotaComponente As New List(Of notaComponente)
        Dim lstNotaIndicador As New List(Of notaIndicador)
        Dim lstNotaSubIndicador As New List(Of notaSubIndicador)


        Dim onotaIndicadorNull As notaIndicador

        Dim nullnotaSubIndicador As notaSubIndicador

        lstPersona1 = crearListaPersona(tbl_NotasRegistro)
        lstNotaComponente = crearListaComponente(tbl_NotasRegistro)
        lstNotaIndicador = crearListaIndicador(tbl_NotasRegistro)
        lstNotaSubIndicador = crearListaSubIndicador(tbl_NotasRegistro)

        For Each op As persona In lstPersona1

            opersona = New persona
            opersona.codAlumnos = op.codAlumnos
            opersona.nombrepersona = op.nombrepersona
            opersona.bimestre = op.bimestre
            opersona.observacionCuro = op.observacionCuro
            opersona.foto = op.foto
            opersona.promedio = op.promedio
            For Each onotaCom As notaComponente In lstNotaComponente
                onotaComponente = New notaComponente
                onotaComponente.codAlumnos = onotaCom.codAlumnos
                onotaComponente.nomComponente = onotaCom.nomComponente
                onotaComponente.codRegNotaComponenente = onotaCom.codRegNotaComponenente
                onotaComponente.notaComponente = onotaCom.notaComponente
                onotaComponente.codRegComponente = onotaCom.codRegComponente

                If opersona.codAlumnos = onotaComponente.codAlumnos Then
                    For Each oNotaInd As notaIndicador In lstNotaIndicador
                        onotaIndicador = New notaIndicador
                        onotaIndicador.nomIndicador = oNotaInd.nomIndicador
                        onotaIndicador.codRegNotaComponenente = oNotaInd.codRegNotaComponenente
                        onotaIndicador.codRegNotaIndicador = oNotaInd.codRegNotaIndicador
                        onotaIndicador.notaIndicador = oNotaInd.notaIndicador

                        onotaIndicador.codRegIndicador = oNotaInd.codRegIndicador
                        If onotaIndicador.codRegNotaComponenente = onotaComponente.codRegNotaComponenente Then
                            For Each onotaSub As notaSubIndicador In lstNotaSubIndicador
                                onotaSubIndicador = New notaSubIndicador
                                onotaSubIndicador.codRegNotaIndicador = onotaSub.codRegNotaIndicador
                                onotaSubIndicador.nomRegSubindicador = onotaSub.nomRegSubindicador
                                onotaSubIndicador.codRegSubindicador = onotaSub.codRegSubindicador
                                onotaSubIndicador.notaSubIndicador = onotaSub.notaSubIndicador

                                onotaSubIndicador.codRegSubindicador = onotaSub.codRegSubindicador
                                If onotaSubIndicador.codRegNotaIndicador = onotaIndicador.codRegNotaIndicador Then
                                    onotaIndicador.lstNotaSubinidcador.Add(onotaSubIndicador)
                                End If


                            Next
                            '    If onotaIndicador.lstNotaSubinidcador.Count = 0 Then

                            '        nullnotaSubIndicador = New notaSubIndicador
                            '        nullnotaSubIndicador.codRegNotaIndicador = 0
                            '        nullnotaSubIndicador.codRegSubindicador = 0
                            '        nullnotaSubIndicador.nomRegSubindicador = ""
                            '        nullnotaSubIndicador.notaSubIndicador = ""
                            '        onotaIndicador.lstNotaSubinidcador.Add(nullnotaSubIndicador)
                            '        'Dim onotaIndicadorNull As notaIndicador

                            '        'Dim nullnotaSubIndicador As notaSubIndicador
                            '    End If

                            onotaComponente.lstNotaIndicador.Add(onotaIndicador)
                        End If


                    Next
                    'If onotaComponente.lstNotaIndicador.Count = 0 Then

                    '    nullnotaSubIndicador = New notaSubIndicador
                    '    nullnotaSubIndicador.codRegNotaIndicador = 0
                    '    nullnotaSubIndicador.codRegSubindicador = 0
                    '    nullnotaSubIndicador.nomRegSubindicador = ""
                    '    nullnotaSubIndicador.notaSubIndicador = ""

                    '    onotaIndicadorNull = New notaIndicador
                    '    onotaIndicadorNull.nomIndicador = ""
                    '    onotaIndicadorNull.notaIndicador = ""
                    '    onotaIndicadorNull.codRegNotaComponenente = 0
                    '    onotaIndicadorNull.codRegNotaIndicador = 0

                    '    onotaIndicadorNull.lstNotaSubinidcador.Add(nullnotaSubIndicador)
                    '    onotaComponente.lstNotaIndicador.Add(onotaIndicadorNull)
                    '    'Dim onotaIndicadorNull As notaIndicador

                    '    'Dim nullnotaSubIndicador As notaSubIndicador

                    'End If



                    opersona.lstNotaComponente.Add(onotaComponente)


                End If

            Next

            lstPersona.Add(opersona)

        Next




        Return lstPersona

    End Function
    ''
    Function crearListaComponente(ByVal tb_listaNotasIndicador As System.Data.DataTable) As List(Of notaComponente)
        Dim lstnotaComponente As New List(Of notaComponente)
        Dim onotaComponente As notaComponente
        Dim codTemComp As String
        For Each fila In tb_listaNotasIndicador.Rows
            onotaComponente = New notaComponente

            onotaComponente.nomComponente = fila("CP_Descripcion").ToString()
            onotaComponente.codRegNotaComponenente = Convert.ToInt32(fila("RNC_CodigoRegistroNotaComponente").ToString())
            onotaComponente.codAlumnos = fila("AL_CodigoAlumno").ToString()
            onotaComponente.notaComponente = fila("RNC_NotaComponente").ToString()
            onotaComponente.codRegComponente = Convert.ToInt32(fila("RC_CodigoRegistroComponentes").ToString())
            If onotaComponente.codRegNotaComponenente = 0 Then
                Continue For
            End If


            If onotaComponente.codRegNotaComponenente <> codTemComp Then
                lstnotaComponente.Add(onotaComponente)
            End If


            'codRegNotaComponenente()
            'codRegNotaIndicador()
            'codRegSubindicador()


            '            CP_Descripcion

            '            RNC_CodigoRegistroNotaComponente

            '            AL_CodigoAlumno
            codTemComp = onotaComponente.codRegNotaComponenente

        Next
        Return lstnotaComponente
        Try

        Catch ex As Exception

        End Try
    End Function
    Function crearListaIndicador(ByVal tb_listaNotasIndicador As System.Data.DataTable) As List(Of notaIndicador)
        Try
            Dim lstNotaIndicador As New List(Of notaIndicador)
            Dim onotaIndicador As notaIndicador
            Dim codTempIndicador As String
            '            ID_Descripcion

            '            RNI_CodigoRegistroNotaIndicador
            '11:
            'codRegNotaComponenente()
            'codRegNotaIndicador()
            'codRegSubindicador()


            For Each fila As DataRow In tb_listaNotasIndicador.Rows
                onotaIndicador = New notaIndicador
                onotaIndicador.codRegNotaComponenente = Convert.ToInt32(fila("RNC_CodigoRegistroNotaComponente").ToString())
                onotaIndicador.nomIndicador = fila("ID_Descripcion").ToString()
                onotaIndicador.codRegNotaIndicador = Convert.ToInt32(fila("RNI_CodigoRegistroNotaIndicador").ToString())
                onotaIndicador.notaIndicador = fila("RNI_NotaIndicador").ToString()

                onotaIndicador.codRegIndicador = Convert.ToInt32(fila("RI_CodigoRegistroIndicadores").ToString())

                If onotaIndicador.codRegNotaIndicador = 0 Then
                    Continue For
                End If

                If onotaIndicador.codRegNotaIndicador <> codTempIndicador Then
                    lstNotaIndicador.Add(onotaIndicador)
                End If
                codTempIndicador = onotaIndicador.codRegNotaIndicador
            Next
            Return lstNotaIndicador
        Catch ex As Exception

        End Try
    End Function
    Function crearListaSubIndicador(ByVal tb_listaNotasIndicador As System.Data.DataTable) As List(Of notaSubIndicador)
        Dim lstNotaSubIndicador As New List(Of notaSubIndicador)
        Dim onotaSubIndicador As notaSubIndicador
        Dim tempSubIndicador As String
        Try
            For Each fila In tb_listaNotasIndicador.Rows
                onotaSubIndicador = New notaSubIndicador

                onotaSubIndicador.codRegNotaIndicador = fila("RNI_CodigoRegistroNotaIndicador").ToString()
                onotaSubIndicador.codRegSubindicador = fila("RNSI_CodigoRegistroNotaSubIndicador").ToString()
                onotaSubIndicador.nomRegSubindicador = fila("SI_Descripcion").ToString()
                onotaSubIndicador.notaSubIndicador = fila("RNSI_NotaSubIndicador").ToString()

                'ULEVA01()
                'LEIVA588()
                'EDU  ANT YAMADA 
                '4TO LION

                'codRegNotaComponenente()
                'codRegNotaIndicador()
                'codRegSubindicador()

                If onotaSubIndicador.codRegSubindicador = 0 Then
                    Continue For
                End If

                If onotaSubIndicador.nomRegSubindicador <> tempSubIndicador Then
                    lstNotaSubIndicador.Add(onotaSubIndicador)
                End If

                tempSubIndicador = onotaSubIndicador.codRegSubindicador
            Next
            Return lstNotaSubIndicador
        Catch ex As Exception

        End Try
    End Function

    ''
    Public Function crearListaPersona(ByVal tb_listaNotasIndicador As System.Data.DataTable) As List(Of persona)
        Try
            Dim lstPersonas As New List(Of persona)

            '            nombreCompleto

            '            AL_CodigoAlumno
            Dim opersona As persona
            Dim codTempCodPersona As String

            For Each fila As DataRow In tb_listaNotasIndicador.Rows
                opersona = New persona
                opersona.promedio = fila("RNBL_NotaFinalBimestre").ToString()
                opersona.codAlumnos = fila("AL_CodigoAlumno").ToString()
                opersona.nombrepersona = fila("nombreCompleto").ToString()
                opersona.bimestre = fila("RNBL_CodigoRegistroBimestralL").ToString()
                opersona.observacionCuro = fila("RNBL_ObservacionCurso").ToString()
                opersona.foto = fila("AL_RutaFoto").ToString()


                If fila("AL_CodigoAlumno") <> codTempCodPersona Then



                    lstPersonas.Add(opersona)
                End If
                codTempCodPersona = opersona.codAlumnos

            Next

            Return lstPersonas
        Catch ex As Exception

        End Try
    End Function

    Function crearReportePrimaria(ByVal int_codAsignascionAula As Integer, ByVal int_bimestre As Integer) As String
        Try
            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaConsolidadoPrimaria")

            ''  <add key="RutaPlantillaConsolidadoPrimaria" value="\Plantillas\ExportacionLibreta\plnConsolidadoPrimaria.xlsx"/>


            'sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"

            'sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("Plantilla_ReporteRelacionAlumno").ToString()

            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range

            'Dim excel As New Microsoft.Office.Interop.Excel.Application
            'Dim wbkWorkbook As Microsoft.Office.Interop.Excel.Workbooks
            'Dim wshWorksheet As Microsoft.Office.Interop.Excel.Sheets
            'Dim rng As Microsoft.Office.Interop.Excel.Range

            'wbkWorkbook = excel.Workbooks.Open("D:\VSS_Projects S13\SaintGeorgeOnline\SaintGeorgeOnline\Plantillas\ExportacionLibreta\plnConsolidadoPrimaria.xlsx") ''\" + ntemp & ".xls"

            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")


            Dim abrBimestre As String = ""
            If int_bimestre = 1 Then
                abrBimestre = "I"
            End If
            If int_bimestre = 2 Then
                abrBimestre = "II"
            End If
            If int_bimestre = 3 Then
                abrBimestre = "III"
            End If
            If int_bimestre = 4 Then
                abrBimestre = "IV"
            End If



            'Return New da_rep_libretaNotas().FUN_LIS_REP_ListarLibretaNotasPrimariaInicial(int_codigoAlumno, int_bimestre, int_anioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
            Dim dst As New DataSet
            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ConsolidadoNotasPrimaria(int_codAsignascionAula, int_bimestre, 1, 1, 1, 1)
            Dim bimestre As Integer = 1
            Dim lp As New List(Of alumnos)
            lp = crearListaPersonas(dst.Tables(1))
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)


            wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)





            Dim rutasPlatillas As String = ConfigurationManager.AppSettings("")





            Dim dtConductaAlumno As New System.Data.DataTable
            Dim promedioFinalConducta As New System.Data.DataTable


            dtConductaAlumno = dst.Tables(5)
            promedioFinalConducta = dst.Tables(6)



            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()
            Dim fi As Integer = 5
            Dim cont As Integer = 0
            CType(excel.Cells(4, 1), Microsoft.Office.Interop.Excel.Range).Value = "Nro."
            CType(excel.Cells(4, 1), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(4, 2), Microsoft.Office.Interop.Excel.Range).Value = "Apellidos y nombres "
            CType(excel.Cells(4, 2), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(4, 3), Microsoft.Office.Interop.Excel.Range).Value = "Codigo"
            CType(excel.Cells(4, 3), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            ''.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            For Each ff As System.Data.DataRow In dst.Tables(0).Rows
                CType(excel.Cells(4, CInt(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value = ff("nomCurso")
                CType(excel.Cells(4, CInt(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(4, CInt(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Orientation = 90
                'codCurso	nomCurso	posCurso
                '1:          Maths(4)
            Next
            Dim sumasFilas As Integer = dst.Tables(0).Rows.Count + 3

            '            tardanzas	FaltaJustificada	FaltaSinJustificar
            '0	0	0

            'RCB_NotaBimestralCualitativa(AL_CodigoAlumno)
            'A(20120208)

            'dtConductaAlumno = dst.Tables(5)
            'promedioFinalConducta = dst.Tables(6)



            Dim tempColumnasNotas As Integer = 0

            tempColumnasNotas = sumasFilas
            '+4
            CType(excel.Cells(4, sumasFilas + 1), Microsoft.Office.Interop.Excel.Range).Value = "CONDUCTA"
            CType(excel.Cells(4, sumasFilas + 1), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(4, sumasFilas + 1), Microsoft.Office.Interop.Excel.Range).Orientation = 90
            CType(excel.Cells(4, sumasFilas + 2), Microsoft.Office.Interop.Excel.Range).Value = "TARDANZAS"
            CType(excel.Cells(4, sumasFilas + 2), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(4, sumasFilas + 2), Microsoft.Office.Interop.Excel.Range).Orientation = 90
            CType(excel.Cells(4, sumasFilas + 3), Microsoft.Office.Interop.Excel.Range).Value = "Inast. justificadas"
            CType(excel.Cells(4, sumasFilas + 3), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(4, sumasFilas + 3), Microsoft.Office.Interop.Excel.Range).Orientation = 90
            CType(excel.Cells(4, sumasFilas + 4), Microsoft.Office.Interop.Excel.Range).Value = "Inast. InJustificadas"
            CType(excel.Cells(4, sumasFilas + 4), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(4, sumasFilas + 4), Microsoft.Office.Interop.Excel.Range).Orientation = 90


            '            tardanzas	FaltaJustificada	FaltaSinJustificar
            '0	0	0

            'RCB_NotaBimestralCualitativa(AL_CodigoAlumno)
            'A(20120208)

            'dtConductaAlumno = dst.Tables(5)
            'promedioFinalConducta = dst.Tables(6)


            CType(excel.Cells(4, sumasFilas + 5), Microsoft.Office.Interop.Excel.Range).Value = "AD"
            excel.ActiveSheet.Cells.Columns(sumasFilas + 5).ColumnWidth = 4

            CType(excel.Cells(4, sumasFilas + 5), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(4, sumasFilas + 6), Microsoft.Office.Interop.Excel.Range).Value = "A"
            excel.ActiveSheet.Cells.Columns(sumasFilas + 6).ColumnWidth = 4

            CType(excel.Cells(4, sumasFilas + 6), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(4, sumasFilas + 7), Microsoft.Office.Interop.Excel.Range).Value = "B"
            excel.ActiveSheet.Cells.Columns(sumasFilas + 7).ColumnWidth = 4

            CType(excel.Cells(4, sumasFilas + 7), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.Cells(4, sumasFilas + 8), Microsoft.Office.Interop.Excel.Range).Value = "C"
            excel.ActiveSheet.Cells.Columns(sumasFilas + 8).ColumnWidth = 4

            CType(excel.Cells(4, sumasFilas + 8), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous




            Dim posCol As Integer = 3
            Dim sumasA As Integer = 0
            Dim sumasB As Integer = 0
            Dim sumasC As Integer = 0
            Dim sumasAD As Integer = 0
            For Each oal As alumnos In lp
                cont += 1
                CType(excel.Cells(fi, 1), Microsoft.Office.Interop.Excel.Range).Value = cont
                CType(excel.Cells(fi, 1), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi, 2), Microsoft.Office.Interop.Excel.Range).Value = oal.nombre
                CType(excel.Cells(fi, 2), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi, 3), Microsoft.Office.Interop.Excel.Range).Value = oal.codigo
                CType(excel.Cells(fi, 3), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                posCol = 3

                sumasA = 0
                sumasB = 0
                sumasC = 0
                sumasAD = 0




                For Each onotasConsolidado In oal.lstNotas

                    CType(excel.Cells(fi, onotasConsolidado.pos), Microsoft.Office.Interop.Excel.Range).Value = onotasConsolidado.notaPromedio.ToString().ToUpper()
                    If onotasConsolidado.notaPromedio.ToString().ToUpper() = "A" Then
                        sumasA += 1
                    End If
                    If onotasConsolidado.notaPromedio.ToString().ToUpper() = "B" Then
                        sumasB += 1
                    End If
                    If onotasConsolidado.notaPromedio.ToString().ToUpper() = "C" Then
                        sumasC += 1
                    End If
                    If onotasConsolidado.notaPromedio.ToString().ToUpper() = "AD" Then
                        sumasAD += 1
                    End If
                Next

                CType(excel.Cells(fi, sumasFilas + 5), Microsoft.Office.Interop.Excel.Range).Value = sumasAD
                CType(excel.Cells(fi, sumasFilas + 5), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi, sumasFilas + 6), Microsoft.Office.Interop.Excel.Range).Value = sumasA
                CType(excel.Cells(fi, sumasFilas + 6), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi, sumasFilas + 7), Microsoft.Office.Interop.Excel.Range).Value = sumasB
                CType(excel.Cells(fi, sumasFilas + 7), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi, sumasFilas + 8), Microsoft.Office.Interop.Excel.Range).Value = sumasC
                CType(excel.Cells(fi, sumasFilas + 8), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                For i = 4 To dst.Tables(0).Rows.Count - 1 + 4
                    CType(excel.Cells(fi, i), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                Next
                fi += 1
            Next

            Dim cantidadFilas As Integer = fi
            Dim catidadColumnas As Integer = dst.Tables(0).Rows.Count - 1
            Dim cantidadA As Integer = 0
            Dim cantidadB As Integer = 0
            Dim cantidadC As Integer = 0
            Dim cantidadAD As Integer = 0
            Dim letras As String = ""

            For Each ff As System.Data.DataRow In dst.Tables(0).Rows
                For contFilas As Integer = 3 To cantidadFilas
                    letras = CType(excel.Cells(contFilas, Convert.ToInt32(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value
                    If letras Is Nothing Then
                        letras = ""
                    End If
                    If letras = "A" Then
                        cantidadA += 1
                    End If
                    If letras = "B" Then
                        cantidadB += 1
                    End If
                    If letras = "C" Then
                        cantidadC += 1
                    End If
                    If letras = "AD" Then
                        cantidadAD += 1
                    End If

                Next
                CType(excel.Cells(fi, Convert.ToInt32(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value = cantidadAD
                CType(excel.Cells(fi + 1, Convert.ToInt32(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value = cantidadA
                CType(excel.Cells(fi + 2, Convert.ToInt32(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value = cantidadB
                CType(excel.Cells(fi + 3, Convert.ToInt32(ff("posCurso").ToString())), Microsoft.Office.Interop.Excel.Range).Value = cantidadC
                ''
                CType(excel.Cells(fi, 3), Microsoft.Office.Interop.Excel.Range).Value = "TOTAL AD"
                CType(excel.Cells(fi + 1, 3), Microsoft.Office.Interop.Excel.Range).Value = "TOTAL A"
                CType(excel.Cells(fi + 2, 3), Microsoft.Office.Interop.Excel.Range).Value = "TOTAL B"
                CType(excel.Cells(fi + 3, 3), Microsoft.Office.Interop.Excel.Range).Value = "TOTAL C"
                ''
                cantidadA = 0
                cantidadB = 0
                cantidadC = 0
                cantidadAD = 0
            Next
            For i = 3 To dst.Tables(0).Rows.Count - 1 + 4
                CType(excel.Cells(fi, i), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi + 1, i), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi + 2, i), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.Cells(fi + 3, i), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            Next
            CType(excel.Cells(3, 2), Microsoft.Office.Interop.Excel.Range).Value = dst.Tables(2).Rows(0)("tutor").ToString()
            CType(excel.Cells(3, 4), Microsoft.Office.Interop.Excel.Range).Value = dst.Tables(3).Rows(0)("AU_Descripcion").ToString()
            CType(excel.Cells(2, 11), Microsoft.Office.Interop.Excel.Range).Value = abrBimestre
            CType(excel.Cells(2, 11), Microsoft.Office.Interop.Excel.Range).Orientation = 0



            ''
            ''tempColumnasNotas

            '            tardanzas	FaltaJustificada	FaltaSinJustificar
            '0	0	0

            'RCB_NotaBimestralCualitativa(AL_CodigoAlumno)
            'A(20120208)

            'dtConductaAlumno = dst.Tables(5)
            'promedioFinalConducta = dst.Tables(6)

            '' For Each persona As personaLibreta In lst

            '            CodigoAlumno()
            '20120208:


            Dim filasContar As Integer = 4
            For Each oal As alumnos In lp
                filasContar += 1
                For Each flas As System.Data.DataRow In promedioFinalConducta.Rows
                    If flas("AL_CodigoAlumno").ToString() = oal.codigo Then
                        CType(excel.Cells(filasContar, tempColumnasNotas + 1), Microsoft.Office.Interop.Excel.Range).Value = flas("RCB_NotaBimestralCualitativa").ToString()
                        CType(excel.Cells(filasContar, tempColumnasNotas + 1), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    End If
                Next
                For Each flas As System.Data.DataRow In dtConductaAlumno.Rows
                    If flas("CodigoAlumno").ToString() = oal.codigo Then
                        CType(excel.Cells(filasContar, tempColumnasNotas + 2), Microsoft.Office.Interop.Excel.Range).Value = flas("tardanzas").ToString()
                        CType(excel.Cells(filasContar, tempColumnasNotas + 2), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.Cells(filasContar, tempColumnasNotas + 3), Microsoft.Office.Interop.Excel.Range).Value = flas("FaltaJustificada").ToString()
                        CType(excel.Cells(filasContar, tempColumnasNotas + 3), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.Cells(filasContar, tempColumnasNotas + 4), Microsoft.Office.Interop.Excel.Range).Value = flas("FaltaSinJustificar").ToString()
                        CType(excel.Cells(filasContar, tempColumnasNotas + 4), Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    End If
                Next

            Next

            With excel.ActiveSheet.PageSetup
                .LeftHeader = ""
                .CenterHeader = ""
                .RightHeader = ""
                .LeftFooter = ""
                .CenterFooter = ""
                .RightFooter = ""
                .LeftMargin = excel.Application.InchesToPoints(0.7)
                .RightMargin = excel.Application.InchesToPoints(0.7)
                .TopMargin = excel.Application.InchesToPoints(0.75)
                .BottomMargin = excel.Application.InchesToPoints(0.75)
                .HeaderMargin = excel.Application.InchesToPoints(0.3)
                .FooterMargin = excel.Application.InchesToPoints(0.3)
                .PrintHeadings = False
                .PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                .PrintQuality = 600
                .CenterHorizontally = False
                .CenterVertically = False
                .Orientation = 2
                .Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = OrderedDictionary xlDownThenOver
                .BlackAndWhite = False
                .Zoom = False
                .FitToPagesWide = False
                .FitToPagesTall = 1
                '.PrintErrors = xlPrintErrorsDisplayed
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
                .PrintTitleColumns = "$A:$B"
            End With


            wbkWorkbook.Save()
            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()
            Return rutaREpositorioTemporales
        Catch ex As Exception
        Finally
        End Try
    End Function

    ''28


    ''funcion crear reporte  descriptores por cursos solo para secundaria 
    Function crearReporteRegistroDescriptores(ByVal curso As Integer, ByVal CodSalon As Integer, ByVal bimestre As Integer) As String
        Dim rutaREpositorioTemporales As String = ""
        Dim excel As New ApplicationClass
        Dim wbkWorkbook As Workbook
        Dim wshWorksheet As Worksheet
        Dim rng As Range

        Try
            Dim obl_Seguimiento_Rep As New bl_Seguimiento_Rep
            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("plantillaDescriptores")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            rutaREpositorioTemporales = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)

            Dim ds As System.Data.DataSet
            ds = obl_Seguimiento_Rep.FUN_REP_Seguimiento(CodSalon, curso, bimestre)

            Dim dtCabezera As New System.Data.DataTable
            Dim dtNotasAlumnos As New System.Data.DataTable
            Dim dtAlumnos As New System.Data.DataTable
            Dim dtCalificativos As New System.Data.DataTable
            Dim dtPromedio As New System.Data.DataTable

            Dim dtAula As New System.Data.DataTable
            Dim dtCurso As New System.Data.DataTable

            dtCabezera = ds.Tables(0)

            dtAlumnos = ds.Tables(1)

            dtNotasAlumnos = ds.Tables(2)

            dtCalificativos = ds.Tables(3)

            dtPromedio = ds.Tables(4)

            dtAula = ds.Tables(5)

            dtCurso = ds.Tables(6)


            Dim columnasCriterios As Integer = 2
            Dim filas As Integer = 3
            Dim colSpan As Integer = dtCalificativos.Rows.Count

            Dim nombreCriterioTemp As String = ""
            Dim lstCriterios As New List(Of criterios)

            lstCriterios = crearListaCriterios(dtCabezera)

            Dim CodBimestre As Integer = 1


            Dim abrBimestre As String = ""
            If CodBimestre = 1 Then
                abrBimestre = "I"
            End If
            If CodBimestre = 2 Then
                abrBimestre = "II"
            End If
            If CodBimestre = 3 Then
                abrBimestre = "III"
            End If
            If CodBimestre = 4 Then
                abrBimestre = "IV"
            End If

            Dim colNumeral As Integer = 2
            Dim esPrimero As Integer = 0
            Dim colCont As Integer = 3
            Dim finalInicial As Integer = 0
            Dim cantidadColumnas As Integer = 0

            cantidadColumnas = lstCriterios.Count * dtCalificativos.Rows.Count



            ''GD_Descripcion	AAP_CodigoAsignacionAula	AU_Descripcion
            ''Nursery	1	Bunnies

            'NC_Descripcion 
            'Maths 


            'CType(excel.ActiveSheet.Cells(2, cantidadColumnas - 4), Range).Value = "Fecha :"
            'CType(excel.ActiveSheet.Cells(3, cantidadColumnas - 4), Range).Value = "Hora :"

            'CType(excel.ActiveSheet.Cells(2, cantidadColumnas - 3), Range).Value = Now().Year().ToString()
            'CType(excel.ActiveSheet.Cells(3, cantidadColumnas - 3), Range).Value = Hour(Now()).ToString() & ":" & Hour(Now()).ToString() & ":" & Second(Now()).ToString()


            excel.Application.Range(CType(excel.ActiveSheet.Cells(2, cantidadColumnas - 4), Range), CType(excel.ActiveSheet.Cells(2, cantidadColumnas - 2), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(2, cantidadColumnas - 4), Range), CType(excel.ActiveSheet.Cells(2, cantidadColumnas - 2), Range)).Value = "Fecha : " & Now().Year().ToString()

            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, cantidadColumnas - 4), Range), CType(excel.ActiveSheet.Cells(3, cantidadColumnas - 2), Range)).Merge(True)
            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, cantidadColumnas - 4), Range), CType(excel.ActiveSheet.Cells(3, cantidadColumnas - 2), Range)).Value = "Hora : " & Hour(Now()).ToString() & ":" & Hour(Now()).ToString() & ":" & Second(Now()).ToString()

            excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 3), Range), CType(excel.ActiveSheet.Cells(4, cantidadColumnas - 4), Range)).Merge(True)


            excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 3), Range), CType(excel.ActiveSheet.Cells(4, cantidadColumnas - 4), Range)).Value = "Registro   Descriptores  de  " & dtCurso.Rows(0)("NC_Descripcion").ToString() & " - " & dtAula.Rows(0)("GD_Descripcion").ToString() & " " & abrBimestre & "Bimestre  - " & Now().Year().ToString()

            excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 3), Range), CType(excel.ActiveSheet.Cells(4, cantidadColumnas - 4), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 3), Range), CType(excel.ActiveSheet.Cells(4, cantidadColumnas - 4), Range)).Font.Size = 16





            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 2), Range), CType(excel.ActiveSheet.Cells(6, 2), Range)).MergeCells = True

            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 2), Range), CType(excel.ActiveSheet.Cells(6, 2), Range)).Value = "Apellidos y nombres "
            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 2), Range), CType(excel.ActiveSheet.Cells(6, 2), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 2), Range), CType(excel.ActiveSheet.Cells(6, 2), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 1), Range), CType(excel.ActiveSheet.Cells(6, 1), Range)).MergeCells = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 1), Range), CType(excel.ActiveSheet.Cells(6, 1), Range)).Value = "Nro"


            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 1), Range), CType(excel.ActiveSheet.Cells(6, 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            For Each oCriterio As criterios In lstCriterios

                esPrimero += 1
                If esPrimero = 1 Then
                    colCont += colSpan - 1
                    finalInicial = colCont - colSpan + 1
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).Value = oCriterio.nombreCriterio

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).RowHeight = 230

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).Orientation = 90
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter




                    For Each filaCual As System.Data.DataRow In dtCalificativos.Rows
                        colNumeral += 1
                        CType(excel.ActiveSheet.Cells(6, colNumeral), Range).Value = filaCual("ACC_Nota").ToString()

                        CType(excel.ActiveSheet.Cells(6, colNumeral), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        CType(excel.ActiveSheet.Cells(6, colNumeral), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    Next

                Else

                    colCont += colSpan
                    finalInicial = colCont - colSpan + 1
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).Value = oCriterio.nombreCriterio

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).Orientation = 90
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, finalInicial), Range), CType(excel.ActiveSheet.Cells(5, colCont), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    For Each filaCual As System.Data.DataRow In dtCalificativos.Rows
                        colNumeral += 1
                        CType(excel.ActiveSheet.Cells(6, colNumeral), Range).Value = filaCual("ACC_Nota").ToString()

                        CType(excel.ActiveSheet.Cells(6, colNumeral), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        CType(excel.ActiveSheet.Cells(6, colNumeral), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    Next


                End If


            Next

            Dim ultimaColumna As Integer = cantidadColumnas + 2

            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, ultimaColumna + 1), Range), CType(excel.ActiveSheet.Cells(6, ultimaColumna + 1), Range)).MergeCells = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, ultimaColumna + 1), Range), CType(excel.ActiveSheet.Cells(6, ultimaColumna + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, ultimaColumna + 1), Range), CType(excel.ActiveSheet.Cells(6, ultimaColumna + 1), Range)).Orientation = 90
            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, ultimaColumna + 1), Range), CType(excel.ActiveSheet.Cells(6, ultimaColumna + 1), Range)).Value = "          PROMEDIO"

            excel.Application.Range(CType(excel.ActiveSheet.Cells(5, ultimaColumna + 1), Range), CType(excel.ActiveSheet.Cells(6, ultimaColumna + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

            ''    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, ultimaColumna), Range), CType(excel.ActiveSheet.Cells(6, ultimaColumna), Range)).RowHeight = 230



            dtAlumnos = ds.Tables(1)

            Dim dtFilAlumnos As DataRow() = ds.Tables(1).Select("1=1", "nombre asc")
            Dim filaPintarAlumnos As Integer = 6
            Dim filaIndicador As Integer = 0



            For Each filAlumnos As System.Data.DataRow In dtFilAlumnos
                filaIndicador += 1
                filaPintarAlumnos += 1
                CType(excel.ActiveSheet.Cells(filaPintarAlumnos, 1), Range).Value = filaIndicador
                CType(excel.ActiveSheet.Cells(filaPintarAlumnos, 2), Range).Value = filAlumnos("nombre").ToString()
                '' CType(excel.ActiveSheet.Cells(filaPintarAlumnos, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                For Each filNotaAlumnos As System.Data.DataRow In dtNotasAlumnos.Rows
                    If filAlumnos("AL_CodigoAlumno") = filNotaAlumnos("AL_CodigoAlumno").ToString Then
                        If filNotaAlumnos("ub").ToString() <> "" Then
                            CType(excel.ActiveSheet.Cells(filaPintarAlumnos, CInt(filNotaAlumnos("ub").ToString())), Range).Value = "X"
                            CType(excel.ActiveSheet.Cells(filaPintarAlumnos, CInt(filNotaAlumnos("ub").ToString())), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                        End If
                    End If
                Next

                For Each filNotasAlumno As System.Data.DataRow In dtPromedio.Rows

                    '                AL_CodigoAlumno(RNBT_NotaFinalBimestre)
                    '20090188	12.00
                    If filNotasAlumno("AL_CodigoAlumno").ToString() = filAlumnos("AL_CodigoAlumno") Then


                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filaPintarAlumnos, ultimaColumna + 1), Range), CType(excel.ActiveSheet.Cells(filaPintarAlumnos, ultimaColumna + 1), Range)).Value = filNotasAlumno("RNBT_NotaFinalBimestre").ToString()

                        '' excel.Application.Range(CType(excel.ActiveSheet.Cells(filaPintarAlumnos, ultimaColumna + 1), Range), CType(excel.ActiveSheet.Cells(filaPintarAlumnos, ultimaColumna + 1), Range)).Value = filAlumnos("RNBT_NotaFinalBimestre")

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filaPintarAlumnos, ultimaColumna + 1), Range), CType(excel.ActiveSheet.Cells(filaPintarAlumnos, ultimaColumna + 1), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filaPintarAlumnos, ultimaColumna + 1), Range), CType(excel.ActiveSheet.Cells(filaPintarAlumnos, ultimaColumna + 1), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


                    End If


                Next

                Dim iniciaColumnas As Integer = 0

                For contCol As Integer = 0 To cantidadColumnas + 1


                    iniciaColumnas += 1
                    CType(excel.ActiveSheet.Cells(filaPintarAlumnos, iniciaColumnas), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    If iniciaColumnas >= 3 Then
                        excel.ActiveSheet.Cells.Columns(iniciaColumnas).ColumnWidth = 4
                    End If

                Next



            Next




            excel.ActiveSheet.Cells.Columns(ultimaColumna + 1).ColumnWidth = 4
            excel.ActiveSheet.Cells.Columns(2).ColumnWidth = 40
            excel.ActiveSheet.Cells.Columns(1).ColumnWidth = 4
            ''excel.ActiveSheet.Cells.Columns(cantidadColumnas - 4).ColumnWidth = 7
            '' excel.ActiveSheet.Cells.Columns(cantidadColumnas - 3).ColumnWidth = 12

            excel.ActiveWindow.Zoom = 75





            With excel.ActiveSheet.PageSetup
                .LeftHeader = ""
                .CenterHeader = ""
                .RightHeader = ""
                .LeftFooter = ""
                .CenterFooter = ""
                .RightFooter = ""
                .LeftMargin = excel.Application.InchesToPoints(0.7)
                .RightMargin = excel.Application.InchesToPoints(0.7)
                .TopMargin = excel.Application.InchesToPoints(0.75)
                .BottomMargin = excel.Application.InchesToPoints(0.75)
                .HeaderMargin = excel.Application.InchesToPoints(0.3)
                .FooterMargin = excel.Application.InchesToPoints(0.3)
                .PrintHeadings = False
                .PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                .PrintQuality = 600
                .CenterHorizontally = False
                .CenterVertically = False
                .Orientation = 2
                .Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = OrderedDictionary xlDownThenOver
                .BlackAndWhite = False
                .Zoom = False
                .FitToPagesWide = False
                .FitToPagesTall = 1
                '.PrintErrors = xlPrintErrorsDisplayed
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
                .PrintTitleColumns = "$A:$B"
            End With






            wbkWorkbook.Save()

            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

        Catch ex As Exception
        Finally

        End Try

        Return rutaREpositorioTemporales
    End Function

 
    '
    Function crearReporteRegistroDescriptoresI(ByVal curso As Integer, ByVal CodSalon As Integer, ByVal bimestre As Integer) As String
        Dim rutaREpositorioTemporales As String = ""
        REM Dim excel As New ApplicationClass
        REM Dim wbkWorkbook As Workbook
        REM Dim wshWorksheet As Worksheet
        REM Dim rng As Range

        Try
            Dim obl_Seguimiento_Rep As New bl_Seguimiento_Rep
            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("plantillaDescriptores")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            rutaREpositorioTemporales = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)


            'wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)

            Dim workbook As New ClosedXML.Excel.XLWorkbook(rutaREpositorioTemporales)

            Dim ws = workbook.Worksheet(1)


            Dim ds As System.Data.DataSet
            ds = obl_Seguimiento_Rep.FUN_REP_Seguimiento(CodSalon, curso, bimestre)

            Dim dtCabezera As New System.Data.DataTable
            Dim dtNotasAlumnos As New System.Data.DataTable
            Dim dtAlumnos As New System.Data.DataTable
            Dim dtCalificativos As New System.Data.DataTable
            Dim dtPromedio As New System.Data.DataTable

            Dim dtAula As New System.Data.DataTable
            Dim dtCurso As New System.Data.DataTable

            dtCabezera = ds.Tables(0)

            dtAlumnos = ds.Tables(1)

            dtNotasAlumnos = ds.Tables(2)

            dtCalificativos = ds.Tables(3)

            dtPromedio = ds.Tables(4)

            dtAula = ds.Tables(5)

            dtCurso = ds.Tables(6)


            Dim columnasCriterios As Integer = 2
            Dim filas As Integer = 3
            Dim colSpan As Integer = dtCalificativos.Rows.Count

            Dim nombreCriterioTemp As String = ""
            Dim lstCriterios As New List(Of criterios)

            lstCriterios = crearListaCriterios(dtCabezera)

            Dim CodBimestre As Integer = 1


            Dim abrBimestre As String = ""
            If CodBimestre = 1 Then
                abrBimestre = "I"
            End If
            If CodBimestre = 2 Then
                abrBimestre = "II"
            End If
            If CodBimestre = 3 Then
                abrBimestre = "III"
            End If
            If CodBimestre = 4 Then
                abrBimestre = "IV"
            End If

            Dim colNumeral As Integer = 2
            Dim esPrimero As Integer = 0
            Dim colCont As Integer = 3
            Dim finalInicial As Integer = 0
            Dim cantidadColumnas As Integer = 0

            cantidadColumnas = lstCriterios.Count * dtCalificativos.Rows.Count



            ''GD_Descripcion	AAP_CodigoAsignacionAula	AU_Descripcion
            ''Nursery	1	Bunnies

            'NC_Descripcion 
            'Maths 


            ' ws.Cell(2, cantidadColumnas - 4) .Value = "Fecha :"
            ' ws.Cell(3, cantidadColumnas - 4) .Value = "Hora :"

            ' ws.Cell(2, cantidadColumnas - 3) .Value = Now().Year().ToString()
            ' ws.Cell(3, cantidadColumnas - 3) .Value = Hour(Now()).ToString() & ":" & Hour(Now()).ToString() & ":" & Second(Now()).ToString()


            ws.Range(ws.Cell(2, cantidadColumnas - 4), ws.Cell(2, cantidadColumnas - 2)).Merge()
            ws.Range(ws.Cell(2, cantidadColumnas - 4), ws.Cell(2, cantidadColumnas - 2)).Value = "Fecha : " & Now().Year().ToString()

            ws.Range(ws.Cell(3, cantidadColumnas - 4), ws.Cell(3, cantidadColumnas - 2)).Merge()
            ws.Range(ws.Cell(3, cantidadColumnas - 4), ws.Cell(3, cantidadColumnas - 2)).Value = "Hora : " & Hour(Now()).ToString() & ":" & Hour(Now()).ToString() & ":" & Second(Now()).ToString()

            ws.Range(ws.Cell(4, 3), ws.Cell(4, cantidadColumnas - 4)).Merge()


            ws.Range(ws.Cell(4, 3), ws.Cell(4, cantidadColumnas - 4)).Value = "Registro   Descriptores  de  " & dtCurso.Rows(0)("NC_Descripcion").ToString() & " - " & dtAula.Rows(0)("GD_Descripcion").ToString() & " " & abrBimestre & "Bimestre  - " & Now().Year().ToString()

            ws.Range(ws.Cell(4, 3), ws.Cell(4, cantidadColumnas - 4)).Style.Font.Bold = True
            ws.Range(ws.Cell(4, 3), ws.Cell(4, cantidadColumnas - 4)).Style.Font.FontSize = 16


            REM .Style.Font.FontName = "Arial"


            ws.Range(ws.Cell(5, 2), ws.Cell(6, 2)).Merge()

            ws.Range(ws.Cell(5, 2), ws.Cell(6, 2)).Value = "Apellidos y nombres "
            'ws.Range(ws.Cell(5, 2) ,  ws.Cell(6, 2) ).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


            With ws.Range(ws.Cell(5, 2), ws.Cell(6, 2))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With




            ws.Range(ws.Cell(5, 2), ws.Cell(6, 2)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


            ws.Range(ws.Cell(5, 1), ws.Cell(6, 1)).Merge()
            ws.Range(ws.Cell(5, 1), ws.Cell(6, 1)).Value = "Nro"


            'ws.Range(ws.Cell(5, 1) ,  ws.Cell(6, 1) ).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            With ws.Range(ws.Cell(5, 1), ws.Cell(6, 1))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With


            For Each oCriterio As criterios In lstCriterios

                esPrimero += 1
                If esPrimero = 1 Then
                    colCont += colSpan - 1
                    finalInicial = colCont - colSpan + 1

                    ws.Row(5).Height = 115
                    'ws.Rows(5).Height=115

                    'ws.Row(4);


                    ws.Range(ws.Cell(5, finalInicial), ws.Cell(5, colCont)).Merge()


                    ws.Range(ws.Cell(5, finalInicial), ws.Cell(5, colCont)).Value = oCriterio.nombreCriterio



                    ' ws.Range(ws.Cell(5, finalInicial) ,  ws.Cell(5, colCont) ).RowHeight = 230






                    ws.Range(ws.Cell(5, finalInicial), ws.Cell(5, colCont)).Style.Alignment.TextRotation = 90
                    'ws.Range(ws.Cell(5, finalInicial) ,  ws.Cell(5, colCont) ).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                    With ws.Range(ws.Cell(5, finalInicial), ws.Cell(5, colCont))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ws.Range(ws.Cell(5, finalInicial), ws.Cell(5, colCont)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center




                    For Each filaCual As System.Data.DataRow In dtCalificativos.Rows
                        colNumeral += 1
                        ws.Cell(6, colNumeral).Value = filaCual("ACC_Nota").ToString()

                        'ws.Cell(6, colNumeral) .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(6, colNumeral)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(6, colNumeral).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    Next

                Else

                    colCont += colSpan
                    finalInicial = colCont - colSpan + 1
                    ws.Range(ws.Cell(5, finalInicial), ws.Cell(5, colCont)).Merge()
                    ws.Range(ws.Cell(5, finalInicial), ws.Cell(5, colCont)).Value = oCriterio.nombreCriterio
                    ').Style.Alignment.TextRotation = 45;

                    ws.Range(ws.Cell(5, finalInicial), ws.Cell(5, colCont)).Style.Alignment.TextRotation = 90
                    '  ws.Range(ws.Cell(5, finalInicial) ,  ws.Cell(5, colCont) ).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(5, finalInicial), ws.Cell(5, colCont))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    ws.Range(ws.Cell(5, finalInicial), ws.Cell(5, colCont)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    For Each filaCual As System.Data.DataRow In dtCalificativos.Rows
                        colNumeral += 1
                        ws.Cell(6, colNumeral).Value = filaCual("ACC_Nota").ToString()

                        'ws.Cell(6, colNumeral) .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(6, colNumeral)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(6, colNumeral).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    Next


                End If


            Next

            Dim ultimaColumna As Integer = cantidadColumnas + 2

            ws.Range(ws.Cell(5, ultimaColumna + 1), ws.Cell(6, ultimaColumna + 1)).Merge()
            ' ws.Range(ws.Cell(5, ultimaColumna + 1) ,  ws.Cell(6, ultimaColumna + 1) ).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


            With ws.Range(ws.Cell(5, ultimaColumna + 1), ws.Cell(6, ultimaColumna + 1))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With


            ws.Range(ws.Cell(5, ultimaColumna + 1), ws.Cell(6, ultimaColumna + 1)).Style.Alignment.TextRotation = 90
            ws.Range(ws.Cell(5, ultimaColumna + 1), ws.Cell(6, ultimaColumna + 1)).Value = "          PROMEDIO"

            ws.Range(ws.Cell(5, ultimaColumna + 1), ws.Cell(6, ultimaColumna + 1)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

            ''     ws.Range(ws.Cell(5, ultimaColumna) ,  ws.Cell(6, ultimaColumna) ).RowHeight = 230



            dtAlumnos = ds.Tables(1)

            Dim dtFilAlumnos As DataRow() = ds.Tables(1).Select("1=1", "nombre asc")
            Dim filaPintarAlumnos As Integer = 6
            Dim filaIndicador As Integer = 0



            For Each filAlumnos As System.Data.DataRow In dtFilAlumnos
                filaIndicador += 1
                filaPintarAlumnos += 1
                ws.Cell(filaPintarAlumnos, 1).Value = filaIndicador
                ws.Cell(filaPintarAlumnos, 2).Value = filAlumnos("nombre").ToString()
                ''  ws.Cell(filaPintarAlumnos, 2) .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                For Each filNotaAlumnos As System.Data.DataRow In dtNotasAlumnos.Rows
                    If filAlumnos("AL_CodigoAlumno") = filNotaAlumnos("AL_CodigoAlumno").ToString Then
                        If filNotaAlumnos("ub").ToString() <> "" Then
                            ws.Cell(filaPintarAlumnos, CInt(filNotaAlumnos("ub").ToString())).Value = "X"
                            ws.Cell(filaPintarAlumnos, CInt(filNotaAlumnos("ub").ToString())).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        End If
                    End If
                Next

                For Each filNotasAlumno As System.Data.DataRow In dtPromedio.Rows

                    '                AL_CodigoAlumno(RNBT_NotaFinalBimestre)
                    '20090188	12.00
                    If filNotasAlumno("AL_CodigoAlumno").ToString() = filAlumnos("AL_CodigoAlumno") Then


                        ws.Range(ws.Cell(filaPintarAlumnos, ultimaColumna + 1), ws.Cell(filaPintarAlumnos, ultimaColumna + 1)).Value = filNotasAlumno("RNBT_NotaFinalBimestre").ToString()

                        ''  ws.Range(ws.Cell(filaPintarAlumnos, ultimaColumna + 1) ,  ws.Cell(filaPintarAlumnos, ultimaColumna + 1) ).Value = filAlumnos("RNBT_NotaFinalBimestre")

                        'ws.Range(ws.Cell(filaPintarAlumnos, ultimaColumna + 1) ,  ws.Cell(filaPintarAlumnos, ultimaColumna + 1) ).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Range(ws.Cell(filaPintarAlumnos, ultimaColumna + 1), ws.Cell(filaPintarAlumnos, ultimaColumna + 1))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Range(ws.Cell(filaPintarAlumnos, ultimaColumna + 1), ws.Cell(filaPintarAlumnos, ultimaColumna + 1)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                    End If


                Next

                Dim iniciaColumnas As Integer = 0

                For contCol As Integer = 0 To cantidadColumnas + 1


                    iniciaColumnas += 1
                    'ws.Cell(filaPintarAlumnos, iniciaColumnas) .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(filaPintarAlumnos, iniciaColumnas)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    If iniciaColumnas >= 3 Then

                        ws.Column(iniciaColumnas).Width = 4
                        ' Excel.ActiveSheet.Cells.Columns(iniciaColumnas).ColumnWidth = 4

                    End If

                Next



            Next




            ws.Column(ultimaColumna + 1).Width = 4

            ws.Column(2).Width = 40
            ws.Column(1).Width = 4
            ' 'excel.ActiveSheet.Cells.Columns(2).ColumnWidth = 40
            ''excel.ActiveSheet.Cells.Columns(1).ColumnWidth = 4








            ''excel.ActiveSheet.Cells.Columns(cantidadColumnas - 4).ColumnWidth = 7
            '' excel.ActiveSheet.Cells.Columns(cantidadColumnas - 3).ColumnWidth = 12

            'excel.ActiveWindow.Zoom = 75





            'With excel.ActiveSheet.PageSetup
            '    .LeftHeader = ""
            '    .CenterHeader = ""
            '    .RightHeader = ""
            '    .LeftFooter = ""
            '    .CenterFooter = ""
            '    .RightFooter = ""
            '    .LeftMargin = excel.Application.InchesToPoints(0.7)
            '    .RightMargin = excel.Application.InchesToPoints(0.7)
            '    .TopMargin = excel.Application.InchesToPoints(0.75)
            '    .BottomMargin = excel.Application.InchesToPoints(0.75)
            '    .HeaderMargin = excel.Application.InchesToPoints(0.3)
            '    .FooterMargin = excel.Application.InchesToPoints(0.3)
            '    .PrintHeadings = False
            '    .PrintGridlines = False
            '    '.PrintComments = xlPrintNoComments
            '    .PrintQuality = 600
            '    .CenterHorizontally = False
            '    .CenterVertically = False
            '    .Style.Alignment.TextRotation = 2
            '    .Draft = False
            '    '.PaperSize = xlPaperLetter
            '    '.FirstPageNumber = xlAutomatic
            '    '.Order = OrderedDictionary xlDownThenOver
            '    .BlackAndWhite = False
            '    .Zoom = False
            '    .FitToPagesWide = False
            '    .FitToPagesTall = 1
            '    '.PrintErrors = xlPrintErrorsDisplayed
            '    .OddAndEvenPagesHeaderFooter = False
            '    .DifferentFirstPageHeaderFooter = False
            '    .ScaleWithDocHeaderFooter = True
            '    .AlignMarginsHeaderFooter = True
            '    .EvenPage.LeftHeader.Text = ""
            '    .EvenPage.CenterHeader.Text = ""
            '    .EvenPage.RightHeader.Text = ""
            '    .EvenPage.LeftFooter.Text = ""
            '    .EvenPage.CenterFooter.Text = ""
            '    .EvenPage.RightFooter.Text = ""
            '    .FirstPage.LeftHeader.Text = ""
            '    .FirstPage.CenterHeader.Text = ""
            '    .FirstPage.RightHeader.Text = ""
            '    .FirstPage.LeftFooter.Text = ""
            '    .FirstPage.CenterFooter.Text = ""
            '    .FirstPage.RightFooter.Text = ""
            '    .PrintTitleColumns = "$A:$B"
            'End With





            workbook.Save()


            'wbkWorkbook.Save()

            'EiminaReferencias(wshWorksheet)
            'EiminaReferencias(wbkWorkbook)
            'excel.Quit()
            'EiminaReferencias(excel)
            'System.GC.Collect()

        Catch ex As Exception
        Finally

        End Try

        Return rutaREpositorioTemporales
    End Function
    '


    Function crearListaCriterios(ByVal dt_criterios As System.Data.DataTable) As List(Of criterios)
        Dim lstNorCriterios = Nothing
        Dim lstCriterios As New List(Of criterios)
        Try

            Dim tempCodCriterio As String = ""

            Dim ocriterios As criterios
            For Each fila As System.Data.DataRow In dt_criterios.Rows
                ocriterios = New criterios
                ocriterios.codigoCriterio = fila("CE_CodigoCriterio").ToString()
                ocriterios.nombreCriterio = fila("descripcion").ToString()

                If ocriterios.codigoCriterio <> tempCodCriterio Then
                    lstCriterios.Add(ocriterios)
                End If
                tempCodCriterio = ocriterios.codigoCriterio


            Next





        Catch ex As Exception
        Finally

        End Try
        Return lstCriterios
    End Function


    Public Class criterios
        Public nombreCriterio As String
        Public codigoCriterio As String
        Public lstCalificativo As New List(Of calificativo)
    End Class

    Public Class calificativo
        Public codCalificativo As String
        Public nombreCalificativo As String
    End Class

    ''fin funcion  crear reporte descriptores 
    Function crearLibretaPrimaria(ByVal codigoAula As Integer, ByVal int_bimestre As Integer) As String


        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaLibretaPrimaria")
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

        Dim tb_Asistencias As New System.Data.DataTable '' extraer las inasistencias del alumno
        Dim tb_demeritos As New System.Data.DataTable '' extrar los meritos y demeritos del alumno 
        Dim tb_conducta As New System.Data.DataTable

        Dim oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Try
            ''
            Dim dt As New System.Data.DataTable
            Dim dst As New DataSet
            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaPrimaria(codigoAula, int_bimestre, 1, 1, 1, 1)
            dt = dst.Tables(0)
            ''
            tb_Asistencias = dst.Tables(3)
            tb_demeritos = dst.Tables(4)
            tb_conducta = dst.Tables(5)

            ''
            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)
            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range


            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"

            File.Copy(rutaPlantillas, rutaREpositorioTemporales)

            wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)

            'Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()
            Dim fil As Integer = 8
            Dim nombreCursoTemp As String = ""
            Dim contadorIndicador As Integer = 0
            Dim filaCount As Integer = 0
            Dim iniciaIndicador As Integer = 0
            Dim indiceHojas As Integer = 0
            'For iii = 0 To lst.Count - 1
            '    wbkWorkbook.Sheets.Add()
            'Next



            For Each opersonaLibreta As personaLibreta In lst
                fil = 8
                indiceHojas += 1
                oSheet = wbkWorkbook.Worksheets(indiceHojas)
                '' oSheets = wbkWorkbook.Worksheets
                oSheet.Activate()
                oSheet.Select()
                '' oSheet.Name = ""
                oSheet.Name = opersonaLibreta.codAlumno


                'Dim str_Logo As String = "D:\Escudo_SG.jpg"
                'Dim p As Object
                'With excel.Range(excel.ActiveSheet.Cells(1, 1), excel.ActiveSheet.Cells(5, 2))
                '    .Merge()

                '    p = excel.ActiveSheet.Shapes.AddPicture(str_Logo, False, True, 30, 10, 75, 75)
                'End With
                'p = Nothing

                ''desomentado
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 4), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Merge(True)
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 4), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Value = "REPORT CARD"
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 4), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 4), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                'CType(excel.ActiveSheet.Cells(3, 4), Range).Value = "NAME"
                'CType(excel.ActiveSheet.Cells(3, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'CType(excel.ActiveSheet.Cells(3, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 5), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Merge(True)
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 5), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 5), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Value = opersonaLibreta.nombreAlumno
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 5), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                'CType(excel.ActiveSheet.Cells(4, 4), Range).Value = "CLASS"
                'CType(excel.ActiveSheet.Cells(4, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'CType(excel.ActiveSheet.Cells(4, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                'excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 5), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Merge(True)
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 5), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 5), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 5), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                'CType(excel.ActiveSheet.Cells(5, 4), Range).Value = "TUTOR"
                'CType(excel.ActiveSheet.Cells(5, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'CType(excel.ActiveSheet.Cells(5, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 5), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Merge(True)
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 5), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Value = dst.Tables(2).Rows(0)("nombre").ToString()
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 5), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 5), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                ''

                excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Value = "REPORT CARD"
                'excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                CType(excel.ActiveSheet.Cells(3, 3), Range).Value = "NAME"
                CType(excel.ActiveSheet.Cells(3, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(3, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Merge(True)

                excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Value = opersonaLibreta.nombreAlumno

                excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                CType(excel.ActiveSheet.Cells(4, 3), Range).Value = "CLASS"
                CType(excel.ActiveSheet.Cells(4, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                CType(excel.ActiveSheet.Cells(4, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                CType(excel.ActiveSheet.Cells(5, 3), Range).Value = "TUTOR"
                CType(excel.ActiveSheet.Cells(5, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(5, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Value = dst.Tables(2).Rows(0)("nombre").ToString()

                excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous




                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente
                    fil += 1
                    If nombreCursoTemp <> olibretaComponente.nombreCurso Then
                        filaCount = fil + 1
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(filaCount, 6), Range)).MergeCells = True

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(filaCount, 6), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(filaCount, 6), Range)).Value = olibretaComponente.nombreCurso.ToUpper() '' & " nombre alumno " & opersonaLibreta.nombreAlumno
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(filaCount, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(filaCount, 6), Range)).Font.Bold = True


                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).Merge(True)
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).Value = "PERFORMANCE"

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil, 11), Range).Value = "AVERAGE"
                        CType(excel.ActiveSheet.Cells(fil, 11), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                        CType(excel.ActiveSheet.Cells(fil, 11), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        fil = filaCount

                        CType(excel.ActiveSheet.Cells(fil, 7), Range).Value = "C"
                        excel.ActiveSheet.Cells.Columns(7).ColumnWidth = 4
                        CType(excel.ActiveSheet.Cells(fil, 7), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil, 8), Range).Value = "B"
                        excel.ActiveSheet.Cells.Columns(8).ColumnWidth = 4
                        CType(excel.ActiveSheet.Cells(fil, 8), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil, 9), Range).Value = "A"
                        excel.ActiveSheet.Cells.Columns(9).ColumnWidth = 4
                        CType(excel.ActiveSheet.Cells(fil, 9), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil, 10), Range).Value = "AD"
                        excel.ActiveSheet.Cells.Columns(10).ColumnWidth = 4
                        CType(excel.ActiveSheet.Cells(fil, 10), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        CType(excel.ActiveSheet.Cells(fil, 11), Range).Value = olibretaComponente.promedioComponente.ToUpper()
                        CType(excel.ActiveSheet.Cells(fil, 11), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        fil += 1
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).Merge(True)
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 7), Range), CType(excel.ActiveSheet.Cells(fil, 10), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    End If
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Value = olibretaComponente.nombreComponente.ToUpper()

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).WrapText = True

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Font.Bold = True

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    CType(excel.ActiveSheet.Cells(fil, 11), Range).Value = olibretaComponente.notaComponente.ToUpper()
                    CType(excel.ActiveSheet.Cells(fil, 11), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    iniciaIndicador = fil + 1
                    contadorIndicador = 0

                    For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador

                        fil += 1
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Merge(True)

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Value = olibretaIndicador.nombreIndicador

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).WrapText = True

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).IndentLevel = 2
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        If olibretaIndicador.notaIndicador.ToUpper() = "C" Then
                            CType(excel.ActiveSheet.Cells(fil, 7), Range).Value = " * "
                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "B" Then
                            CType(excel.ActiveSheet.Cells(fil, 8), Range).Value = " * "
                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "A" Then
                            CType(excel.ActiveSheet.Cells(fil, 9), Range).Value = " * "
                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "AD" Then
                            CType(excel.ActiveSheet.Cells(fil, 10), Range).Value = " * "
                        End If
                        contadorIndicador += 1


                        For ii As Integer = 7 To 10
                            CType(excel.ActiveSheet.Cells(fil, ii), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        Next
                    Next

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(iniciaIndicador, 11), Range), CType(excel.ActiveSheet.Cells(iniciaIndicador + contadorIndicador - 1, 11), Range)).MergeCells = True
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(iniciaIndicador, 11), Range), CType(excel.ActiveSheet.Cells(iniciaIndicador + contadorIndicador - 1, 11), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    nombreCursoTemp = olibretaComponente.nombreCurso
                Next

                ''creando inasistencias del alumno 


                '' CType(excel.ActiveSheet.Cells(iniciaIndicador, 8), Range).
                ''

                fil += 3
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Value = "ABSENCES"
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 6), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(fil + 1, 1), Range).Value = ""
                CType(excel.ActiveSheet.Cells(fil + 1, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 2), Range).Value = "Term I"
                CType(excel.ActiveSheet.Cells(fil + 1, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 3), Range).Value = "Term II"
                CType(excel.ActiveSheet.Cells(fil + 1, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Value = "Term III"
                CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).Value = "Term IV"
                CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 6), Range).Value = "Term Total / Average"
                CType(excel.ActiveSheet.Cells(fil + 1, 6), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                ''
                ''
                CType(excel.ActiveSheet.Cells(fil + 2, 1), Range).Value = "Justtified"
                CType(excel.ActiveSheet.Cells(fil + 2, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 3, 1), Range).Value = "Unjusttified"
                CType(excel.ActiveSheet.Cells(fil + 3, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 4, 1), Range).Value = "Lateness"
                CType(excel.ActiveSheet.Cells(fil + 4, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                For Each filaTb As System.Data.DataRow In tb_Asistencias.Rows
                    If Convert.ToInt32(filaTb("CodigoAlumno").ToString()) = opersonaLibreta.codAlumno Then
                        CType(excel.ActiveSheet.Cells(fil + 2, 2), Range).Value = filaTb("1FaltaJustificada").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 2, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 2), Range).Value = filaTb("1FaltaSinJustificar").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 3, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 4, 2), Range).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 4, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 2, 3), Range).Value = filaTb("2FaltaJustificada").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 2, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 3), Range).Value = filaTb("2FaltaSinJustificar").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 3, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 4, 3), Range).Value = Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 4, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 2, 4), Range).Value = filaTb("3FaltaJustificada").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 2, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 4), Range).Value = filaTb("3FaltaSinJustificar").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 3, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 4, 4), Range).Value = Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 4, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 2, 5), Range).Value = filaTb("4FaltaJustificada").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 2, 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 5), Range).Value = filaTb("4FaltaSinJustificar").ToString()
                        CType(excel.ActiveSheet.Cells(fil + 3, 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 4, 5), Range).Value = Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 4, 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 2, 6), Range).Value = Convert.ToInt32(filaTb("1FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("2FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("3FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("4FaltaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 2, 6), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 3, 6), Range).Value = Convert.ToInt32(filaTb("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4FaltaSinJustificar").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 3, 6), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(fil + 4, 6), Range).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        CType(excel.ActiveSheet.Cells(fil + 4, 6), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        Exit For
                    End If

                Next


                'RCB_NotaBimestralCualitativa	BM_CodigoBimestre	AL_CodigoAlumno
                'AD	1	20090083
                'AD	1	20090174




                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 8), Range), CType(excel.ActiveSheet.Cells(fil, 11), Range)).Merge(True)
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 8), Range), CType(excel.ActiveSheet.Cells(fil, 11), Range)).Value = "CONDUCTA"
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 8), Range), CType(excel.ActiveSheet.Cells(fil, 11), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


                CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).Value = "Term I"
                CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 9), Range).Value = "Term II"
                CType(excel.ActiveSheet.Cells(fil + 1, 9), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 10), Range).Value = "Term III"
                CType(excel.ActiveSheet.Cells(fil + 1, 10), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(fil + 1, 11), Range).Value = "Term IV"
                CType(excel.ActiveSheet.Cells(fil + 1, 11), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                CType(excel.ActiveSheet.Cells(fil + 2, 8), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(fil + 2, 9), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(fil + 2, 10), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(fil + 2, 11), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                For Each fill As System.Data.DataRow In tb_conducta.Rows

                    If Convert.ToInt32(fill("AL_CodigoAlumno").ToString()) = opersonaLibreta.codAlumno Then

                        If fill("BM_CodigoBimestre").ToString() = "1" Then
                            CType(excel.ActiveSheet.Cells(fil + 2, 8), Range).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "2" Then
                            CType(excel.ActiveSheet.Cells(fil + 2, 9), Range).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "3" Then
                            CType(excel.ActiveSheet.Cells(fil + 2, 10), Range).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "4" Then
                            CType(excel.ActiveSheet.Cells(fil + 2, 11), Range).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                        End If

                    End If

                Next

                'RCB_NotaBimestralCualitativa	BM_CodigoBimestre	AL_CodigoAlumno
                'A	1	20100051

                'CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).Value = ""
                'CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).Value = ""
                'CType(excel.ActiveSheet.Cells(fil + 1, 8), Range).Value = ""
                'CType(excel.ActiveSheet.Cells(fil, 8), Range).Value = ""



                fil += 10
                Dim nombreCurso As String = ""
                fil += 1
                CType(excel.ActiveSheet.Cells(fil, 1), Range).Value = "COMMNETS"
                CType(excel.ActiveSheet.Cells(fil, 1), Range).Font.Bold = True
                fil += 1
                CType(excel.ActiveSheet.Cells(fil, 1), Range).Value = "TUTOR"

                For Each olibretaComponenteTemp As libretaComponente In opersonaLibreta.lstLibretaComponente
                    If olibretaComponenteTemp.nombreCurso <> nombreCurso Then

                        If olibretaComponenteTemp.observacionCurso = "" Then
                            Continue For
                        End If

                        fil += 1
                        CType(excel.ActiveSheet.Cells(fil, 1), Range).Value = olibretaComponenteTemp.nombreCurso
                        fil += 1

                        ''CType(excel.ActiveSheet.Cells(fil, 1), Range).Value = olibretaComponenteTemp.observacionCurso
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 8), Range)).MergeCells = True
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 8), Range)).Value = olibretaComponenteTemp.observacionCurso
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 8), Range)).WrapText = True
                        fil += 3
                    End If
                    nombreCurso = olibretaComponenteTemp.nombreCurso
                Next
                excel.Application.Range(CType(excel.ActiveSheet.Cells(iniciaIndicador, 8), Range), CType(excel.ActiveSheet.Cells(iniciaIndicador + contadorIndicador - 1, 8), Range)).MergeCells = True
                excel.Application.Range(CType(excel.ActiveSheet.Cells(iniciaIndicador, 8), Range), CType(excel.ActiveSheet.Cells(iniciaIndicador + contadorIndicador - 1, 8), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                ''
                fil += 1
                excel.Application.Range(CType(excel.ActiveSheet.Cells(iniciaIndicador, 8), Range), CType(excel.ActiveSheet.Cells(iniciaIndicador + contadorIndicador - 1, 8), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'CType(excel.ActiveSheet.Cells(fil + 4, 2), Range).Borders(XlBordersIndex.xlInsideVertical).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'CType(excel.ActiveSheet.Cells(fil + 4, 2), Range).Value = "XlBordersIndex.xlInsideVertical"


                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 2), Range), CType(excel.ActiveSheet.Cells(fil + 5, 3), Range)).Merge(True)

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 2), Range), CType(excel.ActiveSheet.Cells(fil + 5, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 2), Range), CType(excel.ActiveSheet.Cells(fil + 5, 3), Range)).Value = "TUTOR"

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 2), Range), CType(excel.ActiveSheet.Cells(fil + 5, 3), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 5), Range), CType(excel.ActiveSheet.Cells(fil + 5, 7), Range)).Merge(True)

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 5), Range), CType(excel.ActiveSheet.Cells(fil + 5, 7), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 5), Range), CType(excel.ActiveSheet.Cells(fil + 5, 7), Range)).Value = "PARENTS"

                excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 5), Range), CType(excel.ActiveSheet.Cells(fil + 5, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                excel.ActiveSheet.Cells.Columns(3).ColumnWidth = 30
                excel.ActiveWindow.Zoom = 75

                Exit For
            Next


            wbkWorkbook.Save()
            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

            ''
            Return rutaREpositorioTemporales

        Catch ex As Exception

        End Try
    End Function

    Function crearLibretaInicial(ByVal codSalon As Integer, ByVal int_bimestre As Integer) As String
        Dim dt_ausencias As New System.Data.DataTable
        Try
            ''codSalon = 8
            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaLibretaInicial")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            ''  <add key="RutaPlantillaLibretaInicial" value="\Plantillas\ExportacionLibreta\libretaInicial.xlsx"/>
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"

            File.Copy(rutaPlantillas, rutaREpositorioTemporales)

            Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim dt As New System.Data.DataTable
            Dim dst As New DataSet

            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaInicial(codSalon, int_bimestre, 1, 1, 1, 1)
            dt = dst.Tables(0)
            dt_ausencias = dst.Tables(3)

            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)

            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range

            wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)

            'Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()
            Dim fil As Integer = 8

            Dim filDerecha As Integer = 8

            Dim nombreCursoTemp As String = ""
            Dim contadorIndicador As Integer = 0
            Dim filaCount As Integer = 0
            Dim iniciaIndicador As Integer = 0
            Dim indiceHojas As Integer = 0
            'For iii = 0 To lst.Count - 1
            '    wbkWorkbook.Sheets.Add()
            'Next

            For Each opersonaLibreta As personaLibreta In lst
                fil = 8
                filDerecha = 8
                indiceHojas += 1
                oSheet = wbkWorkbook.Worksheets(indiceHojas)
                '' oSheet.Name = opersonaLibreta.codAlumno
                oSheet.Name = opersonaLibreta.codAlumno
                oSheet.Activate()
                oSheet.Select()


                'Dim str_Logo As String = "D:\Escudo_SG.jpg"
                'Dim p As Object
                'With excel.Range(excel.ActiveSheet.Cells(1, 1), excel.ActiveSheet.Cells(5, 2))
                '    .Merge()

                '    p = excel.ActiveSheet.Shapes.AddPicture(str_Logo, False, True, 30, 10, 75, 75)
                'End With
                'p = Nothing

                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente



                    excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Value = "REPORT CARD"
                    'excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(2, 3), Range), CType(excel.ActiveSheet.Cells(2, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    CType(excel.ActiveSheet.Cells(3, 3), Range).Value = "NAME"
                    CType(excel.ActiveSheet.Cells(3, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(3, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Merge(True)

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).Value = opersonaLibreta.nombreAlumno

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 4), Range), CType(excel.ActiveSheet.Cells(3, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    CType(excel.ActiveSheet.Cells(4, 3), Range).Value = "CLASS"
                    CType(excel.ActiveSheet.Cells(4, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    CType(excel.ActiveSheet.Cells(4, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(4, 4), Range), CType(excel.ActiveSheet.Cells(4, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    CType(excel.ActiveSheet.Cells(5, 3), Range).Value = "TUTOR"
                    CType(excel.ActiveSheet.Cells(5, 3), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(5, 3), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Value = dst.Tables(2).Rows(0)("nombre").ToString()
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(5, 4), Range), CType(excel.ActiveSheet.Cells(5, 7), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                    If olibretaComponente.columna Then

                        If olibretaComponente.nombreCurso <> nombreCursoTemp Then
                            fil += 1
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Merge(True)
                            ''excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Value = olibretaComponente.nombreCurso.ToUpper()
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Value = olibretaComponente.nombreCurso.ToUpper()
                            CType(excel.ActiveSheet.Cells(fil, 1), Range).IndentLevel = 3
                            CType(excel.ActiveSheet.Cells(fil, 1), Range).Font.Bold = True
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            '' excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 2), Range), CType(excel.ActiveSheet.Cells(fil + 5, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        End If
                        For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador
                            fil += 1
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Merge(True)
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Value = olibretaIndicador.nombreIndicador
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Font.Size = 7.5
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Font.Name = "Arial"
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                            'CType(excel.Cells(f, c), Range).Font.Name = "Arial"
                            'CType(excel.Cells(f, c), Range).Font.Size = 9
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).WrapText = True
                            With excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range))
                                .Borders(XlBordersIndex.xlEdgeLeft).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            End With
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).Value = olibretaIndicador.notaIndicador.ToUpper()
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).Font.Bold = True
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                            CType(excel.ActiveSheet.Cells(fil, 4), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                        Next
                        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        nombreCursoTemp = olibretaComponente.nombreCurso

                    Else
                        If olibretaComponente.nombreCurso <> nombreCursoTemp Then
                            filDerecha += 1
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Merge(True)
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Value = olibretaComponente.nombreCurso.ToUpper()
                            CType(excel.ActiveSheet.Cells(filDerecha, 5), Range).IndentLevel = 3
                            CType(excel.ActiveSheet.Cells(filDerecha, 5), Range).Font.Bold = True
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            '' excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 2), Range), CType(excel.ActiveSheet.Cells(fil + 5, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        End If

                        For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador
                            filDerecha += 1
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Merge(True)
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Value = olibretaIndicador.nombreIndicador
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Font.Size = 7.5
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Font.Name = "Arial"
                            'excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Font.Size = 8
                            'excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil, 3), Range)).Font.Name = "Arial"
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).WrapText = True
                            With excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range))
                                .Borders(XlBordersIndex.xlEdgeLeft).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            End With
                            CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).Value = olibretaIndicador.notaIndicador.ToUpper()
                            CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).Font.Bold = True
                            CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                            ''CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlTop
                            '' VerticalAlignment = xlVAlignCenter

                            ''   CType(excel.ActiveSheet.Cells(filDerecha, 8), Range).a = Microsoft.Office.Interop.Excel.Constants.xlTop
                        Next

                        ''excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 10, 5), Range), CType(excel.ActiveSheet.Cells(fil + 10, 7), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha, 7), Range)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        nombreCursoTemp = olibretaComponente.nombreCurso
                    End If
                Next

                If filDerecha > fil Then

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 4, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 4, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 4, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 4, 3), Range)).Value = "CONDUCTA"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 4, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 4, 3), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    CType(excel.ActiveSheet.Cells(filDerecha + 4, 4), Range).Value = opersonaLibreta.conductaBimestral
                    CType(excel.ActiveSheet.Cells(filDerecha + 4, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    ''
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 5, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 5, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 5, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 5, 3), Range)).Value = "Comentario de la tutora"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 5, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 5, 3), Range)).Font.Bold = True
                    For Each olibretaComponenteT As libretaComponente In opersonaLibreta.lstLibretaComponente
                        If olibretaComponenteT.observacionCurso <> "" Then
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 7, 6), Range)).MergeCells = True
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 7, 6), Range)).Value = olibretaComponenteT.observacionCurso
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 7, 6), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 7, 6), Range)).WrapText = True
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 8), Range)).MergeCells = True
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 8), Range)).WrapText = True
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 8), Range)).MergeCells = True
                            'excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 6, 1), Range), CType(excel.ActiveSheet.Cells(fil + 7, 6), Range)).Value = olibretaComponenteT.observacionCurso
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 8), Range)).Value = olibretaComponenteT.observacionCurso
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 8), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            filDerecha += 2
                        End If
                    Next
                    ''


                    filDerecha += 10
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 1, 3), Range)).MergeCells = True

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 1, 3), Range)).Value = "ABSENCES"

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 1, 3), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(filDerecha, 4), Range).Value = "Justified"
                    CType(excel.ActiveSheet.Cells(filDerecha, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(filDerecha, 5), Range).Value = "Not justified"
                    CType(excel.ActiveSheet.Cells(filDerecha, 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    CType(excel.ActiveSheet.Cells(filDerecha + 1, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(filDerecha + 1, 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 3), Range)).Merge(True)

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 3), Range)).Value = "Lateness"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 3), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 4), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range)).Merge(True)

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 4), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous




                    For Each filt As System.Data.DataRow In dt_ausencias.Rows

                        If opersonaLibreta.codAlumno = filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then

                            CType(excel.ActiveSheet.Cells(filDerecha + 1, 4), Range).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                            CType(excel.ActiveSheet.Cells(filDerecha + 1, 5), Range).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 4), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range)).Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())

                            Exit For

                        End If
                        'CodigoAlumno	NombreAlumno	1TardanzaJustificada	1TardanzaSinJustificar	1FaltaJustificada	1FaltaSinJustificar	2TardanzaJustificada	2TardanzaSinJustificar	2FaltaJustificada	2FaltaSinJustificar	3TardanzaJustificada	3TardanzaSinJustificar	3FaltaJustificada	3FaltaSinJustificar	4TardanzaJustificada	4TardanzaSinJustificar	4FaltaJustificada	4FaltaSinJustificar
                        '20090135	INGA BARRERA, Enzo Jesús	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0


                    Next




                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 6, 3), Range)).Merge(True)

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 6, 3), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous




                    ''
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 3), Range)).Value = "TUTORA"

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 3), Range)).Font.Bold = True
                    ''.Font.Bold = True

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 3), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 7), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 7), Range)).Value = "PARENTS"


                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 7), Range)).Font.Bold = True
                    ''.Font.Bold = True
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 10, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    ''
                    ''
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 11, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 11, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 11, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 11, 3), Range)).Value = "AD:Archieved with Distinction / Actuación destacada"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 12, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 12, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 12, 2), Range), CType(excel.ActiveSheet.Cells(filDerecha + 12, 3), Range)).Value = "A:Archieved  / Aprobado"
                    ''
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 11, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 11, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 11, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 11, 7), Range)).Value = "B:Needs inprovement/ Bases en  Proceso / Desaprobado"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 12, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 12, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 12, 5), Range), CType(excel.ActiveSheet.Cells(filDerecha + 12, 7), Range)).Value = "Initial Stage  / Calificado Insuficiente / Desaprobado"
                    ''

                Else
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 4, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 4, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 3), Range)).Value = "CONDUCTA"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 4, 1), Range), CType(excel.ActiveSheet.Cells(fil + 4, 3), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                    CType(excel.ActiveSheet.Cells(fil + 4, 4), Range).Value = opersonaLibreta.conductaBimestral

                    CType(excel.ActiveSheet.Cells(fil + 4, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 1), Range), CType(excel.ActiveSheet.Cells(fil + 5, 6), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 1), Range), CType(excel.ActiveSheet.Cells(fil + 5, 6), Range)).Value = "Comentario de la tutora"

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 1), Range), CType(excel.ActiveSheet.Cells(fil + 5, 6), Range)).Font.Bold = True

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 1), Range), CType(excel.ActiveSheet.Cells(fil + 5, 6), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 5, 1), Range), CType(excel.ActiveSheet.Cells(fil + 5, 6), Range)).IndentLevel = 2


                    For Each olibretaComponenteT As libretaComponente In opersonaLibreta.lstLibretaComponente

                        If olibretaComponenteT.observacionCurso <> "" Then

                            '' excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 6, 1), Range), CType(excel.ActiveSheet.Cells(fil + 6, 3), Range)).Merge(True)
                            ''excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 6, 1), Range), CType(excel.ActiveSheet.Cells(fil + 6, 3), Range)).Value = olibretaComponenteT.observacionCurso
                            '' excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 6, 1), Range), CType(excel.ActiveSheet.Cells(fil + 6, 3), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 6, 1), Range), CType(excel.ActiveSheet.Cells(fil + 10, 8), Range)).MergeCells = True
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 6, 1), Range), CType(excel.ActiveSheet.Cells(fil + 10, 8), Range)).WrapText = True
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 6, 1), Range), CType(excel.ActiveSheet.Cells(fil + 10, 8), Range)).MergeCells = True
                            'excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 6, 1), Range), CType(excel.ActiveSheet.Cells(fil + 7, 6), Range)).Value = olibretaComponenteT.observacionCurso
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 6, 1), Range), CType(excel.ActiveSheet.Cells(fil + 10, 8), Range)).Value = olibretaComponenteT.observacionCurso
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 6, 1), Range), CType(excel.ActiveSheet.Cells(fil + 10, 8), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                            fil += 4


                            ''
                        End If

                    Next
                    fil += 10
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 1, 3), Range)).MergeCells = True

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 1, 3), Range)).Value = "ABSENCES"

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil, 1), Range), CType(excel.ActiveSheet.Cells(fil + 1, 3), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(fil, 4), Range).Value = "Justified"
                    CType(excel.ActiveSheet.Cells(fil, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(fil, 5), Range).Value = "Not justified"
                    CType(excel.ActiveSheet.Cells(fil, 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 1), Range), CType(excel.ActiveSheet.Cells(fil + 2, 3), Range)).Merge(True)

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 1), Range), CType(excel.ActiveSheet.Cells(fil + 2, 3), Range)).Value = "Lateness"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 1), Range), CType(excel.ActiveSheet.Cells(fil + 2, 3), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 4), Range), CType(excel.ActiveSheet.Cells(fil + 2, 5), Range)).Merge(True)

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 4), Range), CType(excel.ActiveSheet.Cells(fil + 2, 5), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    'For Each filt As System.Data.DataRow In dt_ausencias.Rows


                    '    If filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then

                    '        CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Value = "p1" '' Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                    '        CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).Value = "p1" '' Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                    '        excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 4), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range)).Value = "p1" '' Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                    '        CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Value = "prueba"
                    '        Exit For

                    '    End If




                    'Next



                    'excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 6, 1), Range), CType(excel.ActiveSheet.Cells(filDerecha + 10, 8), Range)).MergeCells = True


                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 10, 2), Range), CType(excel.ActiveSheet.Cells(fil + 10, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 10, 2), Range), CType(excel.ActiveSheet.Cells(fil + 10, 3), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 10, 2), Range), CType(excel.ActiveSheet.Cells(fil + 10, 3), Range)).Value = "TUTORA"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 10, 2), Range), CType(excel.ActiveSheet.Cells(fil + 10, 3), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 10, 5), Range), CType(excel.ActiveSheet.Cells(fil + 10, 7), Range)).Merge(True)


                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 10, 5), Range), CType(excel.ActiveSheet.Cells(fil + 10, 7), Range)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 10, 5), Range), CType(excel.ActiveSheet.Cells(fil + 10, 7), Range)).Value = "PARENTS"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 10, 5), Range), CType(excel.ActiveSheet.Cells(fil + 10, 7), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


                    ''
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 11, 2), Range), CType(excel.ActiveSheet.Cells(fil + 11, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 11, 2), Range), CType(excel.ActiveSheet.Cells(fil + 11, 3), Range)).Value = "AD:Archieved with Distinction / Actuación destacada"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 12, 2), Range), CType(excel.ActiveSheet.Cells(fil + 12, 3), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 12, 2), Range), CType(excel.ActiveSheet.Cells(fil + 12, 3), Range)).Value = "A:Archieved  / Aprobado"
                    ''
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 11, 5), Range), CType(excel.ActiveSheet.Cells(fil + 11, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 11, 5), Range), CType(excel.ActiveSheet.Cells(fil + 11, 7), Range)).Value = "B:Needs inprovement/ Bases en  Proceso / Desaprobado"
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 12, 5), Range), CType(excel.ActiveSheet.Cells(fil + 12, 7), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 12, 5), Range), CType(excel.ActiveSheet.Cells(fil + 12, 7), Range)).Value = "Initial Stage  / Calificado Insuficiente / Desaprobado"
                    ''

                End If


                If filDerecha > fil Then
                    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                        If opersonaLibreta.codAlumno = filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then
                            CType(excel.ActiveSheet.Cells(filDerecha + 1, 4), Range).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                            CType(excel.ActiveSheet.Cells(filDerecha + 1, 5), Range).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(filDerecha + 2, 4), Range), CType(excel.ActiveSheet.Cells(filDerecha + 2, 5), Range)).Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                            Exit For
                        End If
                    Next
                Else
                    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                        If filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then
                            CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                            CType(excel.ActiveSheet.Cells(fil + 1, 5), Range).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                            excel.Application.Range(CType(excel.ActiveSheet.Cells(fil + 2, 4), Range), CType(excel.ActiveSheet.Cells(fil + 2, 5), Range)).Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                            '' CType(excel.ActiveSheet.Cells(fil + 1, 4), Range).Value = "prueba"
                            Exit For
                        End If
                    Next
                End If

                excel.ActiveSheet.Cells.Columns(4).ColumnWidth = 4
                excel.ActiveSheet.Cells.Columns(8).ColumnWidth = 4
                excel.ActiveSheet.Cells.Columns(3).ColumnWidth = 24
                excel.ActiveSheet.Cells.Columns(7).ColumnWidth = 24
                excel.ActiveWindow.Zoom = 75
                ''  Exit For
            Next
            wbkWorkbook.Save()

            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

            Return rutaREpositorioTemporales
        Catch ex As Exception

        Finally


        End Try
    End Function



    Function crearConsolidadoEvaluacion(ByVal codSalon As Integer, ByVal codBimestre As Integer) As String
        Try
            Dim abrBimestre As String = ""
            If codBimestre = 1 Then
                abrBimestre = "I"
            End If
            If codBimestre = 2 Then
                abrBimestre = "II"
            End If
            If codBimestre = 3 Then
                abrBimestre = "III"
            End If
            If codBimestre = 4 Then
                abrBimestre = "IV"
            End If
            Dim anio As String = ""


            anio = Year(Now).ToString()

            Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim dt As New System.Data.DataTable
            Dim descripcionAula As New System.Data.DataTable

            Dim dst As New DataSet

            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaInicial(codSalon, codBimestre, 1, 1, 1, 1)
            dt = dst.Tables(0)


            descripcionAula = dst.Tables(6)


            Dim descripcionGrado As String = ""
            Dim descAula As String = ""


            'GD_Descripcion	GD_CodigoGrado	GD_Abrev	AU_Descripcion	AAP_CodigoAsignacionAula	GD_DescripcionEspaniol
            'Nursery	1	N	Bunnies	1	Nursery
            descripcionGrado = descripcionAula.Rows(0)("GD_Descripcion").ToString()
            descAula = descripcionAula.Rows(0)("AU_Descripcion").ToString()


            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)

            Dim excel As New ApplicationClass
            Dim wbkWorkbook As Workbook
            Dim wshWorksheet As Worksheet
            Dim rng As Range

            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaConsolidadoInicial")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

            ''  <add key="RutaPlantillaLibretaInicial" value="\Plantillas\ExportacionLibreta\libretaInicial.xlsx"/>
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"

            File.Copy(rutaPlantillas, rutaREpositorioTemporales)


            wbkWorkbook = excel.Workbooks.Open(rutaREpositorioTemporales)


            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()

            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).MergeCells = True

            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).Font.Size = 16
            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).Font.Bold = True
            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            excel.Application.Range(CType(excel.ActiveSheet.Cells(3, 2), Range), CType(excel.ActiveSheet.Cells(4, 9), Range)).Value = " Consolidado  de Evaluación -" & descripcionGrado & " " & descAula & " (Asignaturas Totales )" & abrBimestre & " Bimestre - " & anio


            CType(excel.ActiveSheet.Cells(1, 12), Range).Value = Day(Now).ToString() & "/" & Month(Now).ToString() & "/" & Year(Now).ToString()
            CType(excel.ActiveSheet.Cells(1, 12), Range).WrapText = True
            CType(excel.ActiveSheet.Cells(2, 12), Range).Value = Hour(Now).ToString() & ":" & Minute(Now).ToString() & ":" & Second(Now).ToString()



            Dim columnas As Integer = 3
            Dim filas As Integer = 8 ''6 ''4 
            Dim nombreCursoTemp As String = ""
            Dim nombreCurso As String = ""
            Dim lstContador As New List(Of Integer)
            Dim lstNombre As New List(Of String)
            For Each opersonaLibreta In lst


                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente
                    nombreCurso = olibretaComponente.nombreCurso
                    If nombreCursoTemp <> olibretaComponente.nombreCurso Then
                        Dim count = From i In opersonaLibreta.lstLibretaComponente _
                                    Where i.nombreCurso = nombreCurso _
                                    Select u = i.lstIndicador.Count
                        Dim s As Integer = count.Sum()
                        lstContador.Add(s)
                        lstNombre.Add(olibretaComponente.nombreCurso)
                        nombreCursoTemp = olibretaComponente.nombreCurso
                    End If
                Next

                Exit For

            Next


            Dim colCont As Integer = 3
            Dim esPrimero As Integer = 0
            Dim finalInicial As Integer = 0


            For indice = 0 To lstContador.Count - 1
                esPrimero += 1
                If esPrimero = 1 Then

                    colCont += lstContador(indice) - 1
                    finalInicial = colCont - lstContador(indice) + 1
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Value = lstNombre(indice).ToUpper()
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                Else

                    colCont += lstContador(indice)
                    finalInicial = colCont - lstContador(indice) + 1
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Merge(True)
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Value = lstNombre(indice).ToUpper()
                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    excel.Application.Range(CType(excel.ActiveSheet.Cells(7, finalInicial), Range), CType(excel.ActiveSheet.Cells(7, colCont), Range)).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                End If

            Next
            Dim sumaTotalColumnas As Integer = lstContador.Sum()
            Dim cantidadAlumnos As Integer = lst.Count


            Dim indiceFilas As Integer = 0
            Dim contColumnas As Integer = 2


            Dim filasNombreIndicador As Integer = 8 ''6 ''4
            Dim contCol As Integer = 2

            CType(excel.ActiveSheet.Cells(8, 1), Range).Value = "Nro."
            CType(excel.ActiveSheet.Cells(8, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(8, 2), Range).Value = "Apellidos y nombres "
            CType(excel.ActiveSheet.Cells(8, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            CType(excel.ActiveSheet.Cells(8, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter


            CType(excel.ActiveSheet.Cells(8, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

            For Each opersonaLibreta In lst


                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente

                    For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador
                        contCol += 1
                        If olibretaIndicador.nombreIndicador.Length > 50 Then
                            CType(excel.ActiveSheet.Cells(filasNombreIndicador, contCol), Range).Value = olibretaIndicador.nombreIndicador.Substring(0, 50)

                        Else
                            CType(excel.ActiveSheet.Cells(filasNombreIndicador, contCol), Range).Value = olibretaIndicador.nombreIndicador
                        End If

                        CType(excel.ActiveSheet.Cells(filasNombreIndicador, contCol), Range).Orientation = 90
                        CType(excel.ActiveSheet.Cells(filasNombreIndicador, contCol), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                    Next



                Next

                Exit For

            Next





            For Each opersonaLibreta In lst
                contColumnas = 2
                filas += 1
                indiceFilas += 1
                CType(excel.ActiveSheet.Cells(filas, 2), Range).Value = opersonaLibreta.nombreAlumno

                CType(excel.ActiveSheet.Cells(filas, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas, 1), Range).Value = indiceFilas.ToString()

                CType(excel.ActiveSheet.Cells(filas, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente

                    For Each olibretaIndicador In olibretaComponente.lstIndicador
                        contColumnas += 1
                        CType(excel.ActiveSheet.Cells(filas, contColumnas), Range).Value = olibretaIndicador.notaIndicador.ToUpper()
                        CType(excel.ActiveSheet.Cells(filas, contColumnas), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        CType(excel.ActiveSheet.Cells(filas, contColumnas), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    Next


                Next



            Next
            Dim empiezanContarColumnas As Integer = 2
            Dim empiezanFilas As Integer = 9 ''7 ''5
            Dim cantidadA As Integer = 0
            Dim cantidadB As Integer = 0
            Dim cantidadC As Integer = 0
            Dim contColumnasSumar As Integer = 2
            For colEmp As Integer = 1 To sumaTotalColumnas
                contColumnasSumar += 1
                empiezanFilas = 8 ''6 ''4
                cantidadA = 0
                cantidadB = 0
                cantidadC = 0
                ''
                For indice = 1 To cantidadAlumnos + 1
                    empiezanFilas += 1
                    If CType(excel.ActiveSheet.Cells(empiezanFilas, contColumnasSumar), Range).Value = "A" Then
                        cantidadA += 1
                    End If
                    If CType(excel.ActiveSheet.Cells(empiezanFilas, contColumnasSumar), Range).Value = "B" Then
                        cantidadB += 1
                    End If
                    If CType(excel.ActiveSheet.Cells(empiezanFilas, contColumnasSumar), Range).Value = "C" Then
                        cantidadC += 1
                    End If
                Next
                CType(excel.ActiveSheet.Cells(filas + 1, 2), Range).Value = "TOTAL A:  "
                CType(excel.ActiveSheet.Cells(filas + 1, 2), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(filas + 1, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                CType(excel.ActiveSheet.Cells(filas + 1, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas + 1, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(filas + 2, 2), Range).Value = "TOTAL B:  "
                CType(excel.ActiveSheet.Cells(filas + 2, 2), Range).Font.Bold = True

                CType(excel.ActiveSheet.Cells(filas + 2, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                CType(excel.ActiveSheet.Cells(filas + 2, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas + 2, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                CType(excel.ActiveSheet.Cells(filas + 3, 2), Range).Value = "TOTAL C:  "
                CType(excel.ActiveSheet.Cells(filas + 3, 2), Range).Font.Bold = True
                CType(excel.ActiveSheet.Cells(filas + 3, 2), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                CType(excel.ActiveSheet.Cells(filas + 3, 2), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas + 3, 1), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                CType(excel.ActiveSheet.Cells(filas + 1, contColumnasSumar), Range).Value = cantidadA.ToString()
                CType(excel.ActiveSheet.Cells(filas + 1, contColumnasSumar), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(filas + 1, contColumnasSumar), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas + 2, contColumnasSumar), Range).Value = cantidadB.ToString()
                CType(excel.ActiveSheet.Cells(filas + 2, contColumnasSumar), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                CType(excel.ActiveSheet.Cells(filas + 2, contColumnasSumar), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                CType(excel.ActiveSheet.Cells(filas + 3, contColumnasSumar), Range).Value = cantidadC.ToString()
                CType(excel.ActiveSheet.Cells(filas + 3, contColumnasSumar), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                CType(excel.ActiveSheet.Cells(filas + 3, contColumnasSumar), Range).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                ''
                ''filas
            Next
            ''
            '.FitToPagesWide = 1
            '.FitToPagesTall = 0
            With excel.ActiveSheet.PageSetup
                .LeftHeader = ""
                .CenterHeader = ""
                .RightHeader = ""
                .LeftFooter = ""
                .CenterFooter = ""
                .RightFooter = ""
                .LeftMargin = excel.Application.InchesToPoints(0.7)
                .RightMargin = excel.Application.InchesToPoints(0.7)
                .TopMargin = excel.Application.InchesToPoints(0.75)
                .BottomMargin = excel.Application.InchesToPoints(0.75)
                .HeaderMargin = excel.Application.InchesToPoints(0.3)
                .FooterMargin = excel.Application.InchesToPoints(0.3)
                .PrintHeadings = False
                .PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                .PrintQuality = 600
                .CenterHorizontally = False
                .CenterVertically = False
                .Orientation = 2
                .Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = OrderedDictionary xlDownThenOver
                .BlackAndWhite = False
                .Zoom = False
                .FitToPagesWide = False
                .FitToPagesTall = 1
                '.PrintErrors = xlPrintErrorsDisplayed
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
                .PrintTitleColumns = "$A:$B"
            End With
            ''


            wbkWorkbook.Save()

            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

            Return rutaREpositorioTemporales
        Catch ex As Exception


        Finally

        End Try

    End Function

    Public Class personaLibreta
        Public nombreAlumno As String
        Public codAlumno As String
        Public conductaBimestral As String
        Public lstLibretaComponente As New List(Of libretaComponente)
        '  Public lstCursoLibreta As New List(Of CursoLibreta)
    End Class

    Public Class libretaComponente
        Public codAlumno As String
        Public codRegComponente As String
        Public nombreCurso As String

        Public columna As Boolean

        Public nombreComponente As String
        Public notaComponente As String
        Public promedioComponente As String
        Public observacionCurso As String
        Public lstIndicador As New List(Of libretaIndicador)
    End Class

    Public Class libretaIndicador
        Public codComponente As String
        Public nombreIndicador As String
        Public notaIndicador As String
        Public codIndicador As String
    End Class

    Public Function crearListaLibreta(ByVal dt_tb As System.Data.DataTable) As List(Of personaLibreta)

        Dim lstLibretaAlumno As New List(Of personaLibreta)
        Dim lstLibretasComponente As New List(Of libretaComponente)
        Dim lstLibretaIndicador As New List(Of libretaIndicador)

        Dim lstLibretaAlumnoRes As New List(Of personaLibreta)
        lstLibretaAlumno = crearListaAlumnos(dt_tb)
        lstLibretasComponente = creaListaComponente(dt_tb)
        lstLibretaIndicador = crearListaLibretaIndicador(dt_tb)


        Dim opersonaLibretaT As personaLibreta
        Dim oLibretaComponenteT As libretaComponente
        Dim oLibretaIndicadorT As libretaIndicador


        For Each opersonaLibreta As personaLibreta In lstLibretaAlumno

            opersonaLibretaT = New personaLibreta
            opersonaLibretaT.codAlumno = opersonaLibreta.codAlumno
            opersonaLibretaT.nombreAlumno = opersonaLibreta.nombreAlumno
            opersonaLibretaT.conductaBimestral = opersonaLibreta.conductaBimestral
            For Each oLibretaComponente As libretaComponente In lstLibretasComponente

                oLibretaComponenteT = New libretaComponente
                oLibretaComponenteT.codRegComponente = oLibretaComponente.codRegComponente
                oLibretaComponenteT.nombreComponente = oLibretaComponente.nombreComponente
                oLibretaComponenteT.notaComponente = oLibretaComponente.notaComponente
                oLibretaComponenteT.codAlumno = oLibretaComponente.codAlumno
                oLibretaComponenteT.nombreCurso = oLibretaComponente.nombreCurso
                oLibretaComponenteT.promedioComponente = oLibretaComponente.promedioComponente

                oLibretaComponenteT.columna = oLibretaComponente.columna

                oLibretaComponenteT.observacionCurso = oLibretaComponente.observacionCurso

                For Each oLibretaIndicador As libretaIndicador In lstLibretaIndicador

                    oLibretaIndicadorT = New libretaIndicador
                    oLibretaIndicadorT.nombreIndicador = oLibretaIndicador.nombreIndicador
                    oLibretaIndicadorT.notaIndicador = oLibretaIndicador.notaIndicador
                    oLibretaIndicadorT.codComponente = oLibretaIndicador.codComponente
                    If oLibretaComponenteT.codRegComponente = oLibretaIndicadorT.codComponente Then
                        oLibretaComponenteT.lstIndicador.Add(oLibretaIndicadorT)
                    End If
                Next
                If opersonaLibretaT.codAlumno = oLibretaComponenteT.codAlumno Then
                    opersonaLibretaT.lstLibretaComponente.Add(oLibretaComponenteT)
                End If

            Next
            lstLibretaAlumnoRes.Add(opersonaLibretaT)
        Next



        'Dim lstCursoOrdenar As New List(Of curso)
        'lstCursoOrdenar = From lstOrdenado In lstCurso Order By lstOrdenado.orderCurso Ascending Select lstOrdenado


        Return lstLibretaAlumnoRes


        'Dim lPersona1 = Nothing

        'lPersona1 = From lp In lPersona Order By lp.nombrepersona Ascending Select lp
    End Function
    Function creaListaComponente(ByVal dt As System.Data.DataTable) As List(Of libretaComponente)

        Dim olibretaComponente As libretaComponente
        Dim codComponenteTemp As String = ""
        Dim lstRegistroNotaComponente As New List(Of libretaComponente)
        For Each fila As System.Data.DataRow In dt.Rows

            olibretaComponente = New libretaComponente
            olibretaComponente.codAlumno = fila("AL_CodigoAlumno").ToString()
            olibretaComponente.nombreCurso = fila("NC_Descripcion").ToString()
            olibretaComponente.codRegComponente = fila("RNC_CodigoRegistroNotaComponente").ToString()
            olibretaComponente.nombreComponente = fila("CP_Descripcion").ToString()
            olibretaComponente.notaComponente = fila("RNC_NotaComponente").ToString()
            olibretaComponente.promedioComponente = fila("RNBL_NotaFinalBimestre").ToString()

            olibretaComponente.columna = Convert.ToBoolean(fila("grupoLibreta").ToString())

            olibretaComponente.observacionCurso = fila("RNBL_ObservacionCurso").ToString()





            If olibretaComponente.codRegComponente <> codComponenteTemp Then

                lstRegistroNotaComponente.Add(olibretaComponente)
            End If

            codComponenteTemp = olibretaComponente.codRegComponente
        Next

        Return lstRegistroNotaComponente
        ''RNC_CodigoRegistroNotaComponente RNI_CodigoRegistroNotaIndicador AL_CodigoAlumno AGC_CodigoAsignacionGrupo pComponente CP_Descripcion	ID_Descripcion	BM_CodigoBimestre	RNBL_CodigoRegistroBimestralL	RNC_NotaComponente	RNI_NotaIndicador	RNBL_ObservacionCurso	RNBL_NotaFinalBimestre	RC_CodigoRegistroComponentes	RI_CodigoRegistroIndicadores	NC_Descripcion	CS_CodigoCurso
        ''26561	87805	20100052	687	26561	DOMINIO CORPORAL Y EXPRESIÓN CREATIVA.	Controla movimientos de su  cuerpo durante actividades de  habilidad y  destreza.	1	42509	A	A		A	578	1068	Educación Física	44

    End Function
    Function crearListaAlumnos(ByVal dt As System.Data.DataTable) As List(Of personaLibreta)
        Dim opersonaLibreta As personaLibreta
        Dim codTempAlumno As String = ""
        Dim lstPersonaLibreta As New List(Of personaLibreta)
        For Each fila As System.Data.DataRow In dt.Rows
            opersonaLibreta = New personaLibreta
            opersonaLibreta.codAlumno = fila("AL_CodigoAlumno").ToString()
            opersonaLibreta.nombreAlumno = fila("nombre").ToString()
            opersonaLibreta.conductaBimestral = fila("conductaBimestral").ToString()


            If opersonaLibreta.codAlumno <> codTempAlumno Then
                lstPersonaLibreta.Add(opersonaLibreta)
            End If
            codTempAlumno = opersonaLibreta.codAlumno
        Next


        Return lstPersonaLibreta

    End Function
    Public Function crearListaLibretaIndicador(ByVal dt As System.Data.DataTable) As List(Of libretaIndicador)
        Dim lstLibretaIndicador As New List(Of libretaIndicador)
        Dim olibretaIndicador As libretaIndicador
        Dim idTempIndicador As String = ""


        For Each fila As System.Data.DataRow In dt.Rows
            olibretaIndicador = New libretaIndicador

            olibretaIndicador.codComponente = fila("RNC_CodigoRegistroNotaComponente").ToString()
            olibretaIndicador.notaIndicador = fila("RNI_NotaIndicador").ToString()
            olibretaIndicador.nombreIndicador = fila("ID_Descripcion").ToString()
            olibretaIndicador.codIndicador = fila("RNI_CodigoRegistroNotaIndicador").ToString()

            If olibretaIndicador.codIndicador <> idTempIndicador Then
                lstLibretaIndicador.Add(olibretaIndicador)
            End If

            idTempIndicador = olibretaIndicador.codIndicador

        Next
        Return lstLibretaIndicador
    End Function




    'Public Class asistenciasAlumno

    'End Class

    'Public Class meritosAlumnos

    'End Class


    Shared Sub EiminaReferencias(ByRef Referencias As Object)
        Try
            'Bucle de eliminacion
            Do Until _
                      System.Runtime.InteropServices.Marshal.ReleaseComObject(Referencias) <= 0
            Loop
        Catch
        Finally
            Referencias = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     12/01/2012
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
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     12/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub
#End Region


#Region "Reporte Consolidado Notas"

    Private Sub cargarComboAsignacionAula()

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim int_CodigoTrabajador As Integer = 0 ' todas las aulas de secundaria, sin filtrar por profesor
        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim int_CodigoSede As Integer = 1
        Dim int_Estado As Integer = 1
        Dim int_TipoNota As Integer = 2 ' Cuantitativo

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas


        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
          int_CodigoTrabajador, int_TipoNota, int_CodigoAnioAcademico, int_CodigoSede, _
          int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlAsignacionAula3, ds_Lista, "Codigo", "DescAulaCompuestaCorta", False, False)


        '' cargar combo salones descriptores

        Dim ds_salonDescriptores As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
          int_CodigoTrabajador, int_TipoNota, int_CodigoAnioAcademico, int_CodigoSede, _
          int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(dpsSalonDescriptores, ds_Lista, "Codigo", "DescAulaCompuestaCorta", False, False)
        ''


        ''cargar combo salon primaria reporte exportacion notas 
        int_TipoNota = 2

        ''

        ''cargar combo  salon de descriptores para reportes de descriptores solo secundaria
        ''tipo nota 2 solo para secundaria
        Dim int_CodigoTipoUsuarioDesc As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuarioDesc As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim int_CodigoTrabajadorDesc As Integer = 0 ' todas las aulas de secundaria, sin filtrar por profesor
        Dim int_CodigoAnioAcademicoDesc As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim int_CodigoSedeDesc As Integer = 1
        Dim int_EstadoDesc As Integer = 1

        Dim obj_BL_AsignacionAulasDesc As New bl_AsignacionAulas
        Dim ds_ListaDesc As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
          int_CodigoTrabajador, 2, int_CodigoAnioAcademico, int_CodigoSede, _
          int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(dpsSalonDescriptores, ds_Lista, "Codigo", "DescAulaCompuestaCorta", False, False)
        ''

        ''
        Dim int_TipoNota1 As Integer = 1 ' Cualitativo

        Dim obj_BL_AsignacionAulas1 As New bl_AsignacionAulas
        Dim ds_Lista1 As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
          int_CodigoTrabajador, int_TipoNota1, int_CodigoAnioAcademico, int_CodigoSede, _
          int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlAsignacionAula4, ds_Lista1, "Codigo", "DescAulaCompuestaCorta", False, False)




    End Sub

    Private Sub cargarSalonPrimariaInicial(ByVal int_tipoSalon As Integer)
        Try
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

            Dim int_CodigoTrabajador As Integer = 0 ' todas las aulas de secundaria, sin filtrar por profesor
            Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
            Dim int_CodigoSede As Integer = 1
            Dim int_Estado As Integer = 1
            Dim int_TipoNota As Integer = int_tipoSalon ' Cuantitativo
            Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas


            'select @i = 1, @top = 3        
            'End
            'else if @p_TipoNota = 4 begin -- Primaria  



            Dim dst_salonPrimariaInicial As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
    int_CodigoTrabajador, int_TipoNota, int_CodigoAnioAcademico, int_CodigoSede, _
    int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            Controles.llenarCombo(cmbSalonPrimariaInicial, dst_salonPrimariaInicial, "Codigo", "DescAulaCompuestaCorta", False, False)



        Catch ex As Exception

        End Try
    End Sub

    Private Sub cargarComboAsignacionAulaLibretas(ByVal int_TipoNota As Integer)

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim int_CodigoTrabajador As Integer = 0 ' todas las aulas de secundaria, sin filtrar por profesor
        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim int_CodigoSede As Integer = 1
        Dim int_Estado As Integer = 1

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
          int_CodigoTrabajador, int_TipoNota, int_CodigoAnioAcademico, int_CodigoSede, _
          int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlSalonRepPrimaria, ds_Lista, "Codigo", "DescAulaCompuestaCorta", False, False)

    End Sub

    Private Sub cargarComboAsignacionAulaConsolidado(ByVal int_TipoNota As Integer)

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim int_CodigoTrabajador As Integer = 0 ' todas las aulas de secundaria, sin filtrar por profesor
        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim int_CodigoSede As Integer = 1
        Dim int_Estado As Integer = 1

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
          int_CodigoTrabajador, int_TipoNota, int_CodigoAnioAcademico, int_CodigoSede, _
          int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlAsignacionAula3, ds_Lista, "Codigo", "DescAulaCompuestaCorta", False, False)

    End Sub


    Private Sub cargarComboBimestres()

        Dim str_Descripcion As String = ""
        Dim obj_BL_Bimestres As New bl_Bimestres
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Bimestres.FUN_LIS_Bimestres(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBimestre3, ds_Lista, "Codigo", "Descripcion", False, False)

        Controles.llenarCombo(ddlBimestre4, ds_Lista, "Codigo", "Descripcion", False, False)

        ''cargar combo de bimestre de para reporte de Libretas 
        Controles.llenarCombo(cmbBimestrePrimaria, ds_Lista, "Codigo", "Descripcion", False, False)


        Controles.llenarCombo(dpdBimestreDescriptores, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(DropDownList3, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlBimestre_Asist, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlBimestreLibretas, ds_Lista, "Codigo", "Descripcion", False, False)
        ''

        Controles.llenarCombo(cmbBimestreA, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(cmbBimestreB, ds_Lista, "Codigo", "Descripcion", False, False)


        Controles.llenarCombo(cmbBimestreReportePrimariaGradoBimestre, ds_Lista, "Codigo", "Descripcion", False, False)



    End Sub

    Private Sub ExportarConsolidado()

        Dim int_CodigoTipoUsuario As Integer = 1
        Dim int_CodigoUsuario As Integer = 1
        Dim ds_Lista As New DataSet
        Dim obl_rep_libretaNotas As New bl_rep_libretaNotas

        Dim int_CodigoAsignacionAula As Integer = ddlAsignacionAula3.SelectedValue
        Dim int_CodigoBimestre As Integer = ddlBimestre3.SelectedValue

        Dim int_TipoRep As Integer = 1 ' update 05/12/2012

        ds_Lista = obl_rep_libretaNotas.FUN_LIS_REP_ConsolidadoNotasSecundaria( _
                  int_CodigoAsignacionAula, int_CodigoBimestre, int_TipoRep, _
                  int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        'LLenado de reporte
        Dim NombreArchivo As String = ""
        Dim RutaMadre As String = ""
        Dim downloadBytes As Byte()

        Dim str_TituloReporte As String = "Consolidado" ' Nombre de la hoja 


        NombreArchivo = ExportarReporteconsolidadoSecundaria(ds_Lista, str_TituloReporte)

        NombreArchivo = NombreArchivo & ".xls"

        RutaMadre = Server.MapPath(".")
        RutaMadre = RutaMadre.Replace("\Modulo_Reportes", "\Reportes\")

        downloadBytes = File.ReadAllBytes(RutaMadre & NombreArchivo)

        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()

    End Sub


    ' Consolidado de Notas Secundaria

    Public Shared Function ExportarReporteconsolidadoSecundaria(ByVal ds_Lista As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        ' Archivo excel a grabar
        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        ' Plantilla a cargar
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaConsolidadoSecundaria").ToString()

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

        LlenarPlantillaReporteConsolidadoSecundaria(ds_Lista, oCells, oExcel, str_NombreEntidadReporte)

        With oExcel.ActiveSheet.PageSetup
            .LeftHeader = ""
            .CenterHeader = ""
            .RightHeader = ""
            .LeftFooter = ""
            .CenterFooter = ""
            .RightFooter = ""
            .LeftMargin = oExcel.Application.InchesToPoints(0)
            .RightMargin = oExcel.Application.InchesToPoints(0)
            .TopMargin = oExcel.Application.InchesToPoints(0)
            .BottomMargin = oExcel.Application.InchesToPoints(0)
            .HeaderMargin = oExcel.Application.InchesToPoints(0)
            .FooterMargin = oExcel.Application.InchesToPoints(0)
            .PrintHeadings = False
            .PrintGridlines = False
            '.PrintComments = xlPrintNoComments
            .PrintQuality = 600
            .CenterHorizontally = False
            .CenterVertically = False
            .Orientation = 2
            .Draft = False
            '.PaperSize = xlPaperLetter
            '.FirstPageNumber = xlAutomatic
            '.Order = OrderedDictionary xlDownThenOver
            .BlackAndWhite = False
            .Zoom = False
            .FitToPagesWide = 1
            .FitToPagesTall = False
            '.PrintErrors = xlPrintErrorsDisplayed
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

    Private Shared Function LlenarPlantillaReporteConsolidadoSecundaria( _
        ByVal ds_Lista As System.Data.DataSet, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim dtReporte As System.Data.DataTable = ds_Lista.Tables(0)

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        ' Pintado de Titulo
        Dim str_GradoAula As String = ds_Lista.Tables(3).Rows(0).Item("DescEspaniol").ToString
        Dim str_BimestreAnio As String = ds_Lista.Tables(3).Rows(0).Item("DescBimestreAnio").ToString
        Dim str_TipoReporte As String = "(Asignaturas Oficiales)"

        'oCells(3, 3) = "Consolidado de Evaluación - " + str_Titulo
        With oExcel.Range(oCells(3, 2), oCells(3, 27))
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 16
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .Value = "Consolidado de Evaluación - " + str_GradoAula + " " + str_TipoReporte + " " + str_BimestreAnio
        End With
        oExcel.Rows("3:3").RowHeight = 40

        'Pintado de Fecha 
        With oCells(1, 24)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
            .HorizontalAlignment = int_HA_Left
            .Font.Bold = True
            .Value = "Fecha: " & Now.Date
        End With

        'Pintado de Hora 
        With oCells(2, 24)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
            .HorizontalAlignment = int_HA_Left
            .Font.Bold = True
            .Value = "Hora: " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
        End With


        ' Pintado de Cabeceras
        oCells(fila, columna) = "Nro"
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Font.Name = "Arial"
            .Font.Size = 11
            .HorizontalAlignment = 3
        End With
        columna += 1

        oCells(fila, columna) = "Apellidos y Nombres"
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Font.Name = "Arial"
            .Font.Size = 11
            .HorizontalAlignment = 3
        End With

        cont_columnas = 0
        cont_filas = 0
        columna += 1

        For i As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1 ' Pintado de Cursos
            oCells(fila, columna + i) = ds_Lista.Tables(0).Rows(i).Item("NombreCurso")
            With oExcel.Range(oCells(fila, columna + i), oCells(fila, columna + i))
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 11
                .HorizontalAlignment = 3
                .ColumnWidth = 4
            End With
            oCells(fila + 1, columna + i) = ds_Lista.Tables(0).Rows(i).Item("CodigoAsignacionGrupo") ' Fila 6
        Next

        For i As Integer = 0 To 35
            With oExcel.Range(oCells(fila, columna + i), oCells(fila, columna + i))
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 11
                .HorizontalAlignment = 3
                If i >= 20 Then
                    If i = 20 Or i = 21 Then
                        .ColumnWidth = 8
                    Else
                        .ColumnWidth = 6
                    End If
                Else
                    .ColumnWidth = 6
                End If
            End With

            If i >= 20 Then
                If i = 20 Then
                    oCells(fila, columna + i) = "PROMEDIO"
                ElseIf i = 21 Then
                    oCells(fila, columna + i) = "PUNTAJE TOTAL"
                ElseIf i = 22 Then
                    oCells(fila, columna + i) = "APROBADOS"
                ElseIf i = 23 Then
                    oCells(fila, columna + i) = "DESAPROBADOS"
                ElseIf i = 24 Then
                    oCells(fila, columna + i) = "CONDUCTA"
                ElseIf i = 25 Then
                    oCells(fila, columna + i) = ""
                ElseIf i = 26 Then
                    oCells(fila, columna + i) = "Inasist. Injustificadas"
                ElseIf i = 27 Then
                    oCells(fila, columna + i) = "Inasist. Justificadas"
                ElseIf i = 28 Then
                    oCells(fila, columna + i) = "Tardanzas Injustificadas"
                ElseIf i = 29 Then
                    oCells(fila, columna + i) = "Tardanzas Justificadas"
                ElseIf i = 30 Then
                    oCells(fila, columna + i) = "Tercio"
                ElseIf i = 31 Then
                    oCells(fila, columna + i) = "Orden de Mérito"
                End If
            End If

        Next
        oExcel.Rows("5:5").RowHeight = 129

        cont_columnas = 0
        cont_filas = 0
        fila += 1
        fila += 1
        columna = 2

        Dim int_CodigoAsignacionGrupo As Integer = 0
        Dim str_CodigoAlumno As String = ""
        Dim int_Idx As Integer = 0
        Dim str_Nota As String = ""

        For i As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1 ' Pintado de Alumnos
            int_Idx = i + 1
            str_CodigoAlumno = ds_Lista.Tables(1).Rows(i).Item("CodigoAlumno")
            oCells(fila + i, columna) = int_Idx
            oCells(fila + i, columna + 1) = ds_Lista.Tables(1).Rows(i).Item("NombreCompleto")
            With oExcel.Range(oCells(fila + i, columna), oCells(fila + i, columna))
                .Font.Name = "Arial"
                .Font.Size = 11
                .RowHeight = 15
            End With
            int_CodigoAsignacionGrupo = 0
            For j As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1
                int_CodigoAsignacionGrupo = oCells(6, j + 4).Text()
                For k As Integer = 0 To ds_Lista.Tables(2).Rows.Count - 1
                    If (ds_Lista.Tables(2).Rows(k).Item("CodigoAlumno").ToString = str_CodigoAlumno) And _
                        (ds_Lista.Tables(2).Rows(k).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo) Then
                        oCells(fila + i, j + 4) = ds_Lista.Tables(2).Rows(k).Item("NotaFinal")
                        With oCells(fila + i, j + 4)
                            .NumberFormat = "00"
                            .HorizontalAlignment = 3
                            .font.bold = True
                            If ds_Lista.Tables(2).Rows(k).Item("NotaFinal") IsNot DBNull.Value Then
                                If Convert.ToInt16(ds_Lista.Tables(2).Rows(k).Item("NotaFinal")) > 10 Then
                                    .Font.Color = RGB(22, 54, 92)
                                Else
                                    .Font.Color = RGB(255, 0, 0)
                                End If
                            End If
                        End With
                        Exit For
                    End If
                Next
            Next

            For x As Integer = 0 To ds_Lista.Tables(4).Rows.Count - 1 ' Pintado de Nota de Conducta Alumnos
                If ds_Lista.Tables(4).Rows(x).Item("CodigoAlumno") = str_CodigoAlumno Then

                    With oCells(fila + i, columna + 22) ' Promedio
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Promedio")
                        .NumberFormat = "00.00"
                    End With
                    With oCells(fila + i, columna + 23) ' Puntaje total
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("PuntajeTotal")
                    End With
                    With oCells(fila + i, columna + 24) ' Cursos aprobados
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Aprobados")
                    End With
                    With oCells(fila + i, columna + 25) ' Cursos desaprobados
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Desaprobados")
                    End With
                    With oCells(fila + i, columna + 26) ' Nota Conducta
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Conducta")
                    End With
                    With oCells(fila + i, columna + 27) ' 
                        .HorizontalAlignment = 3
                        .Value = ""
                    End With
                    With oCells(fila + i, columna + 28) ' inasistencias injustificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("FaltasSinJustificar")
                    End With
                    With oCells(fila + i, columna + 29) ' inasistencias justificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("FaltasJustificadas")
                    End With
                    With oCells(fila + i, columna + 30) ' tardanzas injustificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("TardanzasSinJustificar")
                    End With
                    With oCells(fila + i, columna + 31) ' tardanzas justificadas
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("TardanzasJustificadas")
                    End With
                    With oCells(fila + i, columna + 32) ' tercio
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("Tercio")
                    End With
                    With oCells(fila + i, columna + 33) ' orden de merito
                        .HorizontalAlignment = 3
                        .Value = ds_Lista.Tables(4).Rows(x).Item("OrdenMerito")
                    End With

                    Exit For
                End If
            Next

            'With oCells(fila + i, columna + 26) ' Nota Conducta
            '    For x As Integer = 0 To ds_Lista.Tables(4).Rows.Count - 1 ' Pintado de Nota de Conducta Alumnos
            '        If ds_Lista.Tables(4).Rows(x).Item("CodigoAlumno") = str_CodigoAlumno Then
            '            .Value = ds_Lista.Tables(4).Rows(x).Item("Conducta")
            '            Exit For
            '        End If
            '    Next
            '    .HorizontalAlignment = 3
            'End With

            ' ''Dim str_Rango As String = DevLetraColumna(columna + 2) + (fila + i).ToString + ":" + DevLetraColumna(columna + 21) + (fila + i).ToString

            ' ''With oCells(fila + i, columna + 22) ' Promedio de notas del alumno
            ' ''    .Value = "=PROMEDIO(" + str_Rango + ")"
            ' ''    .NumberFormat = "00.00"
            ' ''    .HorizontalAlignment = 3
            ' ''End With

            ' ''With oCells(fila + i, columna + 23) ' Promedio de notas del alumno
            ' ''    .Value = "=SUMA(" + str_Rango + ")"
            ' ''    .NumberFormat = "00.00"
            ' ''    .HorizontalAlignment = 3
            ' ''End With

            ' ''Dim cond1 As String = "," & """>" & "10""" & ")"
            ' ''Dim cond2 As String = "," & """<" & "11""" & ")" 

            ' ''With oCells(fila + i, columna + 24) ' Total Cursos Aprobados
            ' ''    .Value = "=CONTAR.SI(" + str_Rango + cond1
            ' ''    .HorizontalAlignment = 3
            ' ''End With

            ' ''With oCells(fila + i, columna + 25) ' Total Cursos Desaprobados
            ' ''    .Value = "=CONTAR.SI(" + str_Rango + cond2
            ' ''    .HorizontalAlignment = 3
            ' ''End With

        Next

        oExcel.Range(oCells(5, columna), oCells(fila - 1, columna)).EntireColumn.AutoFit() ' Codigo alumno
        oExcel.Range(oCells(5, columna + 1), oCells(fila - 1, columna + 1)).EntireColumn.AutoFit() ' Nombre alumno

        oExcel.Rows("6:6").Delete() ' Elimino la fila de codigos de asignacion de grupos

        Dim int_NumAlumnos As Integer = ds_Lista.Tables(1).Rows.Count
        Dim int_NumCursos As Integer = ds_Lista.Tables(0).Rows.Count
        Dim int_MaxNumCursos As Integer = 20
        Dim int_UltimaFila As Integer = fila - 2 + int_NumAlumnos
        Dim int_UltimaColumna As Integer = columna + 1 + int_NumCursos + (int_MaxNumCursos - int_NumCursos) + 12 ' 12 columnas con campos calculados

        cuadradoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila, int_UltimaColumna)))

        ' Campos calculados
        With oCells(int_UltimaFila + 2, columna + 1)
            .value = "Promedio del Curso"
        End With
        Dim str_Rango As String = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 2, 4 + i)
                .Value = "=PROMEDIO(" + str_Rango + ")"
                .NumberFormat = "00.0"
                .HorizontalAlignment = 3
            End With
        Next

        Dim cond1 As String = "," & """>" & "10""" & ")"
        Dim cond2 As String = "," & """<" & "11""" & ")"

        With oCells(int_UltimaFila + 3, columna + 1)
            .value = "Nro de Alumnos Aprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 3, 4 + i)
                .Value = "=CONTAR.SI(" + str_Rango + cond1
                .NumberFormat = "0"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 4, columna + 1)
            .value = "Nro de Alumnos Desaprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 4, 4 + i)
                .Value = "=CONTAR.SI(" + str_Rango + cond2
                .NumberFormat = "0"
                .HorizontalAlignment = 3
            End With
        Next

        Dim cond3 As String = ")*100/" & int_NumAlumnos.ToString

        With oCells(int_UltimaFila + 5, columna + 1)
            .value = "% de Alumnos Aprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 5, 4 + i)
                .Value = "=(CONTAR.SI(" + str_Rango + cond1 & cond3
                .NumberFormat = "0.0"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 6, columna + 1)
            .value = "% de Alumnos Desaprobados"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 6, 4 + i)
                .Value = "=100-(CONTAR.SI(" + str_Rango + cond1 & cond3
                .NumberFormat = "0.0"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 7, columna + 1)
            .value = "Nota Máxima"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 7, 4 + i)
                .Value = "=MAX(" + str_Rango + ")"
                .NumberFormat = "00"
                .HorizontalAlignment = 3
            End With
        Next

        With oCells(int_UltimaFila + 8, columna + 1)
            .value = "Nota Mínima"
        End With
        str_Rango = ""
        For i As Integer = 0 To int_NumCursos - 1
            str_Rango = DevLetraColumna(4 + i) + (fila - 1).ToString + ":" + DevLetraColumna(4 + i) + (int_UltimaFila).ToString
            With oCells(int_UltimaFila + 8, 4 + i)
                .Value = "=MIN(" + str_Rango + ")"
                .NumberFormat = "00"
                .HorizontalAlignment = 3
            End With
        Next

        cuadradoCompleto(oExcel, oExcel.Range(oCells(int_UltimaFila + 2, columna + 1), oCells(int_UltimaFila + 8, 4 + int_NumCursos - 1)))

        oExcel.Columns("A:A").Delete()

        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function

    ''Public Shared Function ExportarReporteconsolidadoSecundaria(ByVal ds_Lista As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

    ''    Dim oExcel As New Microsoft.Office.Interop.Excel.Application
    ''    Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
    ''    Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
    ''    Dim oCells As Microsoft.Office.Interop.Excel.Range
    ''    Dim sFile As String, sTemplate As String
    ''    Dim nombreRep As String

    ''    nombreRep = GetNewName()

    ''    ' Archivo excel a grabar
    ''    sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
    ''    ' Plantilla a cargar
    ''    sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaConsolidadoSecundaria").ToString()

    ''    oExcel.Visible = False
    ''    oExcel.DisplayAlerts = False

    ''    ''Start a new workbook 
    ''    oBooks = oExcel.Workbooks
    ''    oBooks.Open(sTemplate) 'Load colorful template with graph
    ''    oBook = oBooks.Item(1)
    ''    oSheets = oBook.Worksheets
    ''    oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
    ''    oSheet.Name = str_NombreEntidadReporte
    ''    oCells = oSheet.Cells

    ''    LlenarPlantillaReporteConsolidadoSecundaria(ds_Lista, oCells, oExcel, str_NombreEntidadReporte)


    ''    With oExcel.ActiveSheet.PageSetup
    ''        .LeftHeader = ""
    ''        .CenterHeader = ""
    ''        .RightHeader = ""
    ''        .LeftFooter = ""
    ''        .CenterFooter = ""
    ''        .RightFooter = ""
    ''        .LeftMargin = oExcel.Application.InchesToPoints(0.25)
    ''        .RightMargin = oExcel.Application.InchesToPoints(0.25)
    ''        .TopMargin = oExcel.Application.InchesToPoints(0.75)
    ''        .BottomMargin = oExcel.Application.InchesToPoints(0.75)
    ''        .HeaderMargin = oExcel.Application.InchesToPoints(0.3)
    ''        .FooterMargin = oExcel.Application.InchesToPoints(0.3)
    ''        .PrintHeadings = False
    ''        .PrintGridlines = False
    ''        '.PrintComments = xlPrintNoComments
    ''        .PrintQuality = 600
    ''        .CenterHorizontally = False
    ''        .CenterVertically = False
    ''        .Orientation = 2
    ''        .Draft = False
    ''        '.PaperSize = xlPaperLetter
    ''        '.FirstPageNumber = xlAutomatic
    ''        '.Order = OrderedDictionary xlDownThenOver
    ''        .BlackAndWhite = False
    ''        .Zoom = False
    ''        .FitToPagesWide = 1
    ''        .FitToPagesTall = False
    ''        '.PrintErrors = xlPrintErrorsDisplayed
    ''        .OddAndEvenPagesHeaderFooter = False
    ''        .DifferentFirstPageHeaderFooter = False
    ''        .ScaleWithDocHeaderFooter = True
    ''        .AlignMarginsHeaderFooter = True
    ''        .EvenPage.LeftHeader.Text = ""
    ''        .EvenPage.CenterHeader.Text = ""
    ''        .EvenPage.RightHeader.Text = ""
    ''        .EvenPage.LeftFooter.Text = ""
    ''        .EvenPage.CenterFooter.Text = ""
    ''        .EvenPage.RightFooter.Text = ""
    ''        .FirstPage.LeftHeader.Text = ""
    ''        .FirstPage.CenterHeader.Text = ""
    ''        .FirstPage.RightHeader.Text = ""
    ''        .FirstPage.LeftFooter.Text = ""
    ''        .FirstPage.CenterFooter.Text = ""
    ''        .FirstPage.RightFooter.Text = ""
    ''    End With


    ''    oSheet.SaveAs(sFile)
    ''    oBook.Close()

    ''    'Quit Excel and thoroughly deallocate everything
    ''    oExcel.Quit()
    ''    ReleaseComObject(oCells)
    ''    ReleaseComObject(oSheet)
    ''    ReleaseComObject(oSheets)
    ''    ReleaseComObject(oBook)
    ''    ReleaseComObject(oBooks)
    ''    ReleaseComObject(oExcel)
    ''    oExcel = Nothing
    ''    oBooks = Nothing
    ''    oBook = Nothing
    ''    oSheets = Nothing
    ''    oSheet = Nothing
    ''    oCells = Nothing
    ''    System.GC.Collect()

    ''    Return nombreRep

    ''End Function

    ''Private Shared Function LlenarPlantillaReporteConsolidadoSecundaria( _
    ''    ByVal ds_Lista As System.Data.DataSet, _
    ''    ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
    ''    ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
    ''    ByVal str_NombreEntidadReporte As String) As String

    ''    Dim dtReporte As System.Data.DataTable = ds_Lista.Tables(0)

    ''    Dim fila As Integer = 5
    ''    Dim columna As Integer = 2
    ''    Dim cont_columnas As Integer = 0
    ''    Dim cont_filas As Integer = 0
    ''    Dim str_Fila As String = ""

    ''    ' Pintado de Titulo
    ''    Dim str_GradoAula As String = ds_Lista.Tables(3).Rows(0).Item("DescEspaniol").ToString
    ''    Dim str_BimestreAnio As String = ds_Lista.Tables(3).Rows(0).Item("DescBimestreAnio").ToString
    ''    Dim str_TipoReporte As String = "(Asignaturas Globales)"

    ''    'oCells(3, 3) = "Consolidado de Evaluación - " + str_Titulo
    ''    With oExcel.Range(oCells(3, 2), oCells(3, 27))
    ''        .Merge()
    ''        .Font.Name = "Arial"
    ''        .Font.Size = 16
    ''        .Font.Bold = True
    ''        .HorizontalAlignment = int_HA_Center
    ''        .VerticalAlignment = int_VA_Middle
    ''        .Value = "Consolidado de Evaluación - " + str_GradoAula + " " + str_TipoReporte + " " + str_BimestreAnio
    ''    End With
    ''    oExcel.Rows("3:3").RowHeight = 40

    ''    'Pintado de Fecha 
    ''    With oCells(1, 24)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
    ''        .HorizontalAlignment = int_HA_Left
    ''        .Font.Bold = True
    ''        .Value = "Fecha: " & Now.Date
    ''    End With

    ''    'Pintado de Hora 
    ''    With oCells(2, 24)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
    ''        .HorizontalAlignment = int_HA_Left
    ''        .Font.Bold = True
    ''        .Value = "Hora: " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
    ''    End With


    ''    ' Pintado de Cabeceras
    ''    oCells(fila, columna) = "Nro"
    ''    With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
    ''        .Font.Name = "Arial"
    ''        .Font.Size = 11
    ''        .HorizontalAlignment = 3
    ''    End With
    ''    columna += 1

    ''    oCells(fila, columna) = "Apellidos y Nombres"
    ''    With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
    ''        .Font.Name = "Arial"
    ''        .Font.Size = 11
    ''        .HorizontalAlignment = 3
    ''    End With

    ''    cont_columnas = 0
    ''    cont_filas = 0
    ''    columna += 1

    ''    For i As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1 ' Pintado de Cursos
    ''        oCells(fila, columna + i) = ds_Lista.Tables(0).Rows(i).Item("NombreCurso")
    ''        With oExcel.Range(oCells(fila, columna + i), oCells(fila, columna + i))
    ''            .WrapText = True
    ''            .Orientation = 90
    ''            .AddIndent = False
    ''            .IndentLevel = 0
    ''            .ShrinkToFit = False
    ''            .Font.Name = "Arial"
    ''            .Font.Size = 11
    ''            .HorizontalAlignment = 3
    ''            .ColumnWidth = 4
    ''        End With
    ''        oCells(fila + 1, columna + i) = ds_Lista.Tables(0).Rows(i).Item("CodigoAsignacionGrupo") ' Fila 6
    ''    Next

    ''    For i As Integer = 0 To 30
    ''        With oExcel.Range(oCells(fila, columna + i), oCells(fila, columna + i))
    ''            .WrapText = True
    ''            .Orientation = 90
    ''            .AddIndent = False
    ''            .IndentLevel = 0
    ''            .ShrinkToFit = False
    ''            .Font.Name = "Arial"
    ''            .Font.Size = 11
    ''            .HorizontalAlignment = 3
    ''            If i >= 20 Then
    ''                If i = 20 Or i = 21 Then
    ''                    .ColumnWidth = 8
    ''                Else
    ''                    .ColumnWidth = 4
    ''                End If
    ''            Else
    ''                .ColumnWidth = 4
    ''            End If
    ''        End With

    ''        If i >= 20 Then
    ''            If i = 20 Then
    ''                oCells(fila, columna + i) = "PROMEDIO"
    ''            ElseIf i = 21 Then
    ''                oCells(fila, columna + i) = "PUNTAJE TOTAL"
    ''            ElseIf i = 22 Then
    ''                oCells(fila, columna + i) = "APROBADOS"
    ''            ElseIf i = 23 Then
    ''                oCells(fila, columna + i) = "DESAPROBADOS"
    ''            ElseIf i = 24 Then
    ''                oCells(fila, columna + i) = "CONDUCTA"
    ''            End If
    ''        End If

    ''    Next
    ''    oExcel.Rows("5:5").RowHeight = 129

    ''    cont_columnas = 0
    ''    cont_filas = 0
    ''    fila += 1
    ''    fila += 1
    ''    columna = 2

    ''    Dim int_CodigoAsignacionGrupo As Integer = 0
    ''    Dim str_CodigoAlumno As String = ""
    ''    Dim int_Idx As Integer = 0
    ''    Dim str_Nota As String = ""

    ''    For i As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1 ' Pintado de Alumnos
    ''        int_Idx = i + 1
    ''        str_CodigoAlumno = ds_Lista.Tables(1).Rows(i).Item("CodigoAlumno")
    ''        oCells(fila + i, columna) = int_Idx
    ''        oCells(fila + i, columna + 1) = ds_Lista.Tables(1).Rows(i).Item("NombreCompleto")
    ''        With oExcel.Range(oCells(fila + i, columna), oCells(fila + i, columna))
    ''            .Font.Name = "Arial"
    ''            .Font.Size = 11
    ''            .RowHeight = 15
    ''        End With
    ''        int_CodigoAsignacionGrupo = 0
    ''        For j As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1
    ''            int_CodigoAsignacionGrupo = oCells(6, j + 4).Text()
    ''            For k As Integer = 0 To ds_Lista.Tables(2).Rows.Count - 1
    ''                If ds_Lista.Tables(2).Rows(k).Item("CodigoAlumno") = str_CodigoAlumno And ds_Lista.Tables(2).Rows(k).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo Then
    ''                    oCells(fila + i, j + 4) = ds_Lista.Tables(2).Rows(k).Item("NotaFinal")
    ''                    With oCells(fila + i, j + 4)
    ''                        .NumberFormat = "00"
    ''                        .HorizontalAlignment = 3
    ''                        .font.bold = True
    ''                        If ds_Lista.Tables(2).Rows(k).Item("NotaFinal") IsNot DBNull.Value Then
    ''                            If Convert.ToInt16(ds_Lista.Tables(2).Rows(k).Item("NotaFinal")) > 10 Then
    ''                                .Font.Color = RGB(22, 54, 92)
    ''                            Else
    ''                                .Font.Color = RGB(255, 0, 0)
    ''                            End If
    ''                        End If
    ''                    End With
    ''                    Exit For
    ''                End If
    ''            Next
    ''        Next

    ''        With oCells(fila + i, columna + 26) ' Nota Conducta
    ''            For x As Integer = 0 To ds_Lista.Tables(4).Rows.Count - 1 ' Pintado de Nota de Conducta Alumnos
    ''                If ds_Lista.Tables(4).Rows(x).Item("CodigoAlumno") = str_CodigoAlumno Then
    ''                    .Value = ds_Lista.Tables(4).Rows(x).Item("Nota")
    ''                    Exit For
    ''                End If
    ''            Next
    ''            .HorizontalAlignment = 3
    ''        End With

    ''        Dim str_Rango As String = DevLetraColumna(columna + 2) + (fila + i).ToString + ":" + DevLetraColumna(columna + 21) + (fila + i).ToString

    ''        With oCells(fila + i, columna + 22) ' Promedio de notas del alumno
    ''            .Value = "=PROMEDIO(" + str_Rango + ")"
    ''            .NumberFormat = "00.00"
    ''            .HorizontalAlignment = 3
    ''        End With

    ''        With oCells(fila + i, columna + 23) ' Promedio de notas del alumno
    ''            .Value = "=SUMA(" + str_Rango + ")"
    ''            .NumberFormat = "00.00"
    ''            .HorizontalAlignment = 3
    ''        End With

    ''        Dim cond1 As String = "," & """>" & "10""" & ")"
    ''        Dim cond2 As String = "," & """<" & "11""" & ")"

    ''        With oCells(fila + i, columna + 24) ' Total Cursos Aprobados
    ''            .Value = "=CONTAR.SI(" + str_Rango + cond1
    ''            .HorizontalAlignment = 3
    ''        End With

    ''        With oCells(fila + i, columna + 25) ' Total Cursos Desaprobados
    ''            .Value = "=CONTAR.SI(" + str_Rango + cond2
    ''            .HorizontalAlignment = 3
    ''        End With

    ''    Next

    ''    oExcel.Range(oCells(5, columna), oCells(fila - 1, columna)).EntireColumn.AutoFit() ' Codigo alumno
    ''    oExcel.Range(oCells(5, columna + 1), oCells(fila - 1, columna + 1)).EntireColumn.AutoFit() ' Nombre alumno

    ''    oExcel.Rows("6:6").Delete() ' Elimino la fila de codigos de asignacion de grupos

    ''    Dim int_NumAlumnos As Integer = ds_Lista.Tables(1).Rows.Count
    ''    Dim int_NumCursos As Integer = ds_Lista.Tables(0).Rows.Count
    ''    Dim int_MaxNumCursos As Integer = 20
    ''    Dim int_UltimaFila As Integer = fila - 2 + int_NumAlumnos
    ''    Dim int_UltimaColumna As Integer = columna + 1 + int_NumCursos + (int_MaxNumCursos - int_NumCursos) + 5 ' 5 columnas con campos calculados

    ''    cuadradoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila, int_UltimaColumna)))

    ''    oExcel.Columns("A:A").Delete()


    ''    oExcel.ActiveWindow.Zoom = 75

    ''    Return str_Fila
    ''End Function


#End Region

#Region "Reportes Libreta Notas"

    Public Shared Function ExportarReporteLibretaSecundaria( _
    ByVal ds_ListaAlumnos As System.Data.DataSet, _
    ByVal int_CodigoAsignacionAula As Integer, _
    ByVal int_CodigoBimestre As Integer, _
    ByVal int_CodigoAnioAcademico As Integer, _
    ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal cod_Modulo As Integer, ByVal cod_Opcion As Integer) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        'Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks
        Dim oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        ' Archivo excel a grabar
        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        ' Plantilla a cargar
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaLibretaSecundaria").ToString()

        oExcel.Visible = False
        oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBook = oExcel.Workbooks.Add()

        Dim str_CodigoAlumno As String = ""
        Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
        Dim ds_Lista As DataSet
        Dim int_NumAlumno As Integer = ds_ListaAlumnos.Tables(0).Rows.Count

        For i As Integer = 0 To (int_NumAlumno - 3) - 1
            oBook.Sheets.Add()
        Next

        Dim str_NombreEntidadReporte As String = ""

        For i As Integer = 0 To ds_ListaAlumnos.Tables(0).Rows.Count - 1
            str_CodigoAlumno = ds_ListaAlumnos.Tables(0).Rows(i).Item("CodigoAlumno")

            ds_Lista = obl_rep_libretaNotas.FUN_LIS_REP_LibretaNotasSecundaria( _
                  str_CodigoAlumno, int_CodigoBimestre, int_CodigoAnioAcademico, _
                  int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            oBook.Worksheets(i + 1).Name = "Codigo " & str_CodigoAlumno
            oBook.Worksheets(i + 1).Select()
            oSheets = oBook.Worksheets
            oSheet = CType(oSheets.Item(i + 1), Microsoft.Office.Interop.Excel.Worksheet)
            oCells = oSheet.Cells
            LlenarPlantillaReporteLibretaSecundaria(ds_Lista, oCells, oExcel)

            With oExcel.ActiveSheet.PageSetup
                .LeftHeader = ""
                .CenterHeader = ""
                .RightHeader = ""
                .LeftFooter = ""
                .CenterFooter = ""
                .RightFooter = ""
                .LeftMargin = oExcel.Application.InchesToPoints(0.25)
                .RightMargin = oExcel.Application.InchesToPoints(0.25)
                .TopMargin = oExcel.Application.InchesToPoints(0.75)
                .BottomMargin = oExcel.Application.InchesToPoints(0.75)
                .HeaderMargin = oExcel.Application.InchesToPoints(0.3)
                .FooterMargin = oExcel.Application.InchesToPoints(0.3)
                .PrintHeadings = False
                .PrintGridlines = False
                '.PrintComments = xlPrintNoComments
                .PrintQuality = 600
                .CenterHorizontally = False
                .CenterVertically = False
                .Orientation = 2
                .Draft = False
                '.PaperSize = xlPaperLetter
                '.FirstPageNumber = xlAutomatic
                '.Order = OrderedDictionary xlDownThenOver
                .BlackAndWhite = False
                .Zoom = False
                .FitToPagesWide = 1
                .FitToPagesTall = False
                '.PrintErrors = xlPrintErrorsDisplayed
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

        Next



        '' ''str_CodigoAlumno = "20060007"

        '' ''ds_Lista = obl_rep_libretaNotas.FUN_LIS_REP_LibretaNotasSecundaria( _
        '' ''      str_CodigoAlumno, int_CodigoBimestre, int_CodigoAnioAcademico, _
        '' ''      int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        '' ''oBook.Worksheets(1).Name = "Codigo " & str_CodigoAlumno
        '' ''oBook.Worksheets(1).Select()
        '' ''oSheets = oBook.Worksheets
        '' ''oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        '' ''oCells = oSheet.Cells
        '' ''LlenarPlantillaReporteLibretaSecundaria(ds_Lista, oCells, oExcel)




        oBook.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        'ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        'oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep

    End Function

    Private Shared Function LlenarPlantillaReporteLibretaSecundaria( _
        ByVal ds_Lista As System.Data.DataSet, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application) As String

        Dim dtReporte As System.Data.DataTable = ds_Lista.Tables(0)

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        cont_columnas = 0
        cont_filas = 0

        columna += 1

        ' PINTADO DE LOGO
        'Dim str_Logo As String = System.Configuration.ConfigurationManager.AppSettings("RutaLogoLibreta").ToString '"D:\miLogo\Escudo_SG.jpg"
        'Dim p As Object
        'With oExcel.Range(oCells(1, 2), oCells(4, 2))
        '    .Merge()
        '    p = oExcel.ActiveSheet.Shapes.AddPicture(str_Logo, False, True, 30, 10, 60, 80)
        'End With
        'p = Nothing

        ' Pintado de Titulo
        Dim str_GradoAula As String = ds_Lista.Tables(4).Rows(0).Item("DescIngles").ToString
        Dim str_NombreTutor As String = ds_Lista.Tables(4).Rows(0).Item("NombreTutor").ToString
        Dim str_NombreAlumno As String = ds_Lista.Tables(4).Rows(0).Item("NombreAlumno").ToString

        With oExcel.Range(oCells(1, 6), oCells(1, 24)) ' NAME
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 20
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .Value = "REPORT CARD"
        End With
        oExcel.Rows("1:1").RowHeight = 40 ' Listado de Cursos

        With oExcel.Range(oCells(2, 6), oCells(2, 8)) ' NAME
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = "NAME"
        End With

        With oExcel.Range(oCells(2, 9), oCells(2, 24)) ' NAME
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = str_NombreAlumno
        End With
        oExcel.Rows("2:2").RowHeight = 20

        With oExcel.Range(oCells(3, 6), oCells(3, 8)) ' NAME
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = "CLASS"
        End With

        With oExcel.Range(oCells(3, 9), oCells(3, 24)) ' CLASS
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = str_GradoAula
        End With
        oExcel.Rows("3:3").RowHeight = 20

        With oExcel.Range(oCells(4, 6), oCells(4, 8)) ' NAME
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = "TUTOR"
        End With

        With oExcel.Range(oCells(4, 9), oCells(4, 24)) ' TUTOR
            .Merge()
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = str_NombreTutor
        End With
        oExcel.Rows("4:4").RowHeight = 20

        cuadradoCompleto(oExcel, oExcel.Range(oCells(2, 6), oCells(4, 24)))

        'oExcel.Rows("3:3").RowHeight = 10

        'Pintado de Fecha 
        With oCells(2, 30)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
            .HorizontalAlignment = int_HA_Left
            .Font.Bold = True
            .Value = "Date: " & StrConv(Format(Now, "MMMM d,yyyy").ToString, VbStrConv.ProperCase)
        End With

        ' '' ''Pintado de Hora 
        '' ''With oCells(2, 24)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
        '' ''    .HorizontalAlignment = int_HA_Left
        '' ''    .Font.Bold = True
        '' ''    .Value = "Hora: " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
        '' ''End With

        Dim colIni As Integer = 0
        Dim colFin As Integer = 0
        Dim lstPosCursos As New List(Of Integer)

        For i As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos

            colIni = columna + (i * 3)
            colFin = colIni + 2

            lstPosCursos.Add(colIni) ' agrego la columna de la posicion inicial

            With oExcel.Range(oCells(fila, colIni), oCells(fila, colFin))
                .Merge()
                .Value = ds_Lista.Tables(3).Rows(i).Item("NombreCurso")
                .Font.Name = "Arial"
                .Font.Size = 8
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .ColumnWidth = 3
                .WrapText = True
            End With

            With oExcel.Range(oCells(fila + 1, colIni), oCells(fila + 1, colFin)) ' Fila 6 : Lista de Codigos Asignacion de Grupo
                .Merge()
                .Value = ds_Lista.Tables(3).Rows(i).Item("CodigoAsignacionGrupo")
                .Font.Name = "Arial"
                .Font.Size = 8
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .ColumnWidth = 3
                .WrapText = True
            End With
        Next
        oExcel.Rows("5:5").RowHeight = 30 ' Listado de Cursos

        columna -= 1

        Dim dt As System.Data.DataTable = ds_Lista.Tables(0)
        Dim sql = From s In ds_Lista.Tables(0).AsEnumerable() _
                  Select CodigoAsignacionGrupo = s.Field(Of Integer)("CodigoGrupoCriterio") _
                  Distinct

        Dim int_NumGrupo As Integer = sql.Count
        Dim int_NumCriterios As Integer = ds_Lista.Tables(0).Rows.Count
        Dim int_NumCalificativos As Integer = ds_Lista.Tables(1).Rows.Count
        Dim int_NumCursos As Integer = ds_Lista.Tables(3).Rows.Count

        Dim int_UltimaFila As Integer = fila + int_NumCriterios + int_NumGrupo + 2 ' 2 grupos de criterios extras
        Dim int_UltimaColumna As Integer = columna + (int_NumCursos * int_NumCalificativos) ' 4 columnas con campos calculados

        fila += 1
        fila += 1

        Dim lstPos As New List(Of Integer)
        Dim str_Grupo As String = ""
        Dim bool_PintadoGrupo As Boolean = False

        Dim int_CodigoAsignacionGrupo As Integer = 0
        Dim int_CodigoCalificativo As Integer = 0
        Dim int_Idx As Integer = 0
        Dim str_Nota As String = ""
        Dim bool_NotaCriterio As Boolean = False

        For i As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1 ' Pintado de Criterios
            colIni = 0
            If str_Grupo = "" Or str_Grupo <> ds_Lista.Tables(0).Rows(i).Item("GrupoCriterio") Then
                str_Grupo = ds_Lista.Tables(0).Rows(i).Item("GrupoCriterio")
                With oCells(fila + i, columna)
                    .Font.Bold = True
                    .Value = str_Grupo
                End With
                If bool_PintadoGrupo = False Then
                    For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos
                        colIni = columna + 1 + (j * 3)
                        For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1 ' Pintado de Calificativos
                            With oCells(fila + i, colIni + k)
                                .Font.Bold = True
                                .Value = ds_Lista.Tables(1).Rows(k).Item("Abreviatura")
                                .HorizontalAlignment = int_HA_Center
                                .VerticalAlignment = int_VA_Middle
                            End With
                            oCells(fila + i + 1, colIni + k).Value = ds_Lista.Tables(1).Rows(k).Item("CodigoCalificativo")
                        Next
                    Next
                    bool_PintadoGrupo = True
                    fila += 1
                End If
                lstPos.Add(fila + i)
                fila += 1
            End If

            oCells(fila + i, columna) = ds_Lista.Tables(0).Rows(i).Item("Criterio")
            int_CodigoAsignacionGrupo = 0
            colIni = 0

            For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1
                colIni = columna + 1 + (j * 3)
                int_CodigoAsignacionGrupo = oCells(6, colIni).Text()
                int_CodigoCalificativo = 0
                If bool_NotaCriterio Then : bool_NotaCriterio = False : End If
                For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1
                    int_CodigoCalificativo = oCells(8, columna + 1 + k).Text()
                    For l As Integer = 0 To ds_Lista.Tables(2).Rows.Count - 1
                        If ds_Lista.Tables(2).Rows(l).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo And ds_Lista.Tables(2).Rows(l).Item("CodigoCalificativo") = int_CodigoCalificativo Then
                            With oCells(fila + i, colIni + k)
                                .value = "X"
                                .HorizontalAlignment = int_HA_Center
                                .VerticalAlignment = int_VA_Middle
                            End With
                            bool_NotaCriterio = True
                            Exit For
                        End If
                    Next
                    If bool_NotaCriterio Then : Exit For : End If
                Next
            Next
        Next

        oExcel.Range(oCells(5, columna), oCells(fila - 1, columna)).EntireColumn.AutoFit() ' Listado de Criterios


        Dim objColor0 As Object = RGB(0, 0, 0) 'Negro
        Dim objColor1 As Object = RGB(191, 191, 191) 'Plomo

        pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor0)
        pintadoInterior(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor1)
        pintadoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(5, int_UltimaColumna)), objColor0)
        pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila, 2)), objColor0)

        pintadoCelda(oExcel, oExcel.Range(oCells(lstPos(1), 2), oCells(lstPos(1), int_UltimaColumna)), objColor0, 2) ' separador de grupos de criterio
        pintadoCelda(oExcel, oExcel.Range(oCells(int_UltimaFila, 2), oCells(int_UltimaFila, int_UltimaColumna)), objColor0, 4) ' separador Notas

        For i As Integer = 0 To lstPosCursos.Count - 1
            pintadoCelda(oExcel, oExcel.Range(oCells(5, lstPosCursos(i)), oCells(int_UltimaFila + 2, lstPosCursos(i))), objColor0, 1) ' separador de cursos
        Next

        With oCells(int_UltimaFila + 1, 2)
            .Value = "ACADEMIC PERFORMANCE"
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        With oCells(int_UltimaFila + 2, 2)
            .Value = "OVERALL ATTAINMENT"
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        oExcel.Rows((int_UltimaFila + 2).ToString & ":" & (int_UltimaFila + 2).ToString).RowHeight = 24 ' Listado de Cursos

        colIni = 0
        colFin = 0

        For i As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1 ' Pintado de Nota de Cursos 
            colIni = 0
            colIni = columna + 1 + (i * 3)
            colFin = colIni + 2
            int_CodigoAsignacionGrupo = oCells(6, colIni).Text()
            For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                If ds_Lista.Tables(5).Rows(j).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo Then
                    With oExcel.Range(oCells(int_UltimaFila + 1, colIni), oCells(int_UltimaFila + 1, colFin))
                        .Merge()
                        .Value = ds_Lista.Tables(5).Rows(j).Item("NotaBimestre")
                        .Font.Name = "Arial"
                        .Font.Size = 8
                        .Font.Bold = True
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                    End With
                    With oExcel.Range(oCells(int_UltimaFila + 1 + 1, colIni), oCells(int_UltimaFila + 1 + 1, colFin))
                        .Merge()
                        .Value = ds_Lista.Tables(5).Rows(j).Item("Observacion")
                        .Font.Name = "Arial"
                        .Font.Size = 8
                        .Font.Bold = True
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                    End With
                    Exit For
                End If
            Next
        Next

        ' PINTADO DE COMENTARIOS 
        Dim int_FilaComentario As Integer = int_UltimaFila + 3
        Dim int_MaxNumFilasComentario As Integer = 15 * 2
        Dim int_UltimaFilaComentario As Integer = int_FilaComentario + int_MaxNumFilasComentario 'ds_Lista.Tables(6).Rows.Count
        int_FilaComentario += 1
        With oCells(int_FilaComentario, 2)
            .Value = "COMMENTS"
            .Font.Bold = True
        End With
        int_FilaComentario += 1

        Dim pos_ComentAux As Integer = 0
        Dim int_FilasPintadas As Integer = 0

        For i As Integer = 0 To ds_Lista.Tables(6).Rows.Count - 1 ' Pintado de Oservaciones de Cursos 
            If ds_Lista.Tables(6).Rows(i).Item("Observacion") IsNot DBNull.Value Then

                If ds_Lista.Tables(6).Rows(i).Item("Observacion").ToString.Length > 0 Then
                    int_FilasPintadas += 1
                End If

                'With oExcel.Range(oCells(int_FilaComentario + i * 2, 2), oCells(int_FilaComentario + i * 2, int_UltimaColumna))
                With oExcel.Range(oCells(int_FilaComentario - 2 + int_FilasPintadas * 2, 2), _
                                  oCells(int_FilaComentario - 2 + int_FilasPintadas * 2, int_UltimaColumna))
                    .Merge()
                    .Value = ds_Lista.Tables(6).Rows(i).Item("Curso") & ":"
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                    .HorizontalAlignment = int_HA_Left
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                End With

                'With oExcel.Range(oCells(int_FilaComentario + i * 2 + 1, 2), oCells(int_FilaComentario + i * 2 + 1, int_UltimaColumna))
                With oExcel.Range(oCells(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, 2), _
                                  oCells(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, int_UltimaColumna))
                    .Merge()
                    .Value = ds_Lista.Tables(6).Rows(i).Item("Observacion")
                    .Font.Name = "Arial"
                    .Font.Size = 9
                    .HorizontalAlignment = int_HA_Left
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                End With

            End If
        Next
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaComentario, 2), oCells(int_UltimaFilaComentario, int_UltimaColumna)), objColor0)

        ' PINTADO DE NOTAS 
        Dim int_FilaNotas As Integer = int_UltimaFilaComentario + 2
        Dim int_UltimaFilaNotas As Integer = int_FilaNotas + ds_Lista.Tables(5).Rows.Count
        Dim int_UltimaColumnaNotas As Integer = 2 + ds_Lista.Tables(5).Columns.Count - 6

        With oExcel.Range(oCells(int_FilaNotas, 2), oCells(int_FilaNotas, 11))
            .Merge()
            .Value = "TERM AND ANNUAL MARK"
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
        End With
        int_FilaNotas += 1

        For i As Integer = 0 To ds_Lista.Tables(5).Columns.Count - 1
            If i > 3 And i < 13 Then
                If i = 12 Then
                    With oExcel.Range(oCells(int_FilaNotas, 2 + i - 4), oCells(int_FilaNotas, 2 + i - 4 + 1))
                        .Merge()
                        .Value = ds_Lista.Tables(5).Columns(i).ColumnName
                        .Font.Bold = True
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .Font.Name = "Arial"
                        .Font.Size = 9
                    End With
                    For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                        With oExcel.Range(oCells(int_FilaNotas + 1 + j, 2 + i - 4), oCells(int_FilaNotas + 1 + j, 2 + i - 4 + 1))
                            .Merge()
                            .Value = ds_Lista.Tables(5).Rows(j).Item(i)
                            .Font.Bold = True
                            .HorizontalAlignment = int_HA_Center
                            .VerticalAlignment = int_VA_Middle
                            .Font.Name = "Arial"
                            .Font.Size = 8
                        End With
                    Next
                Else
                    With oExcel.Range(oCells(int_FilaNotas, 2 + i - 4), oCells(int_FilaNotas, 2 + i - 4))
                        .Value = ds_Lista.Tables(5).Columns(i).ColumnName
                        .Font.Bold = True
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .Font.Name = "Arial"
                        .Font.Size = 9
                    End With
                    For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                        With oExcel.Range(oCells(int_FilaNotas + 1 + j, 2 + i - 4), oCells(int_FilaNotas + 1 + j, 2 + i - 4))
                            .Value = ds_Lista.Tables(5).Rows(j).Item(i)
                            .Font.Bold = True
                            If i = 4 Then
                                .HorizontalAlignment = int_HA_Left
                                .VerticalAlignment = int_VA_Middle
                                .Font.Size = 10
                            Else
                                .HorizontalAlignment = int_HA_Center
                                .VerticalAlignment = int_VA_Middle
                                .Font.Size = 8
                            End If
                            .Font.Name = "Arial"
                        End With
                    Next
                End If
            End If
        Next
        pintadoCompleto(oExcel, oExcel.Range(oCells(int_FilaNotas - 1, 2), oCells(int_UltimaFilaNotas + 1, int_UltimaColumnaNotas)), objColor0)

        With oExcel.Range(oCells(int_UltimaFilaNotas + 2, 2), oCells(int_UltimaFilaNotas + 2, int_UltimaColumnaNotas)) 'oCells(int_UltimaFilaNotas + 2, 2)
            .Merge()
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Value = "Note: The note indicates 'P' value test is pending."
            .Font.Bold = True
        End With

        ' PINTADO DE STUDENT PROFILE 
        Dim int_FilaProfile As Integer = int_UltimaFilaComentario + 2
        Dim int_ColumnaProfile As Integer = int_UltimaColumnaNotas + 2
        Dim int_UltimaFilaProfile As Integer = int_FilaNotas - 2 + 8 + ds_Lista.Tables(7).Rows.Count * 2
        Dim int_UltimaColumnaProfile As Integer = int_ColumnaProfile + ds_Lista.Tables(8).Rows.Count * 2

        Dim lstPosProfile As New List(Of posicionCelda)
        Dim posCelda As posicionCelda

        Dim int_PosProfileFila As Integer = 0
        Dim int_PosProfileColumna As Integer = 0

        With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8))
            .Merge()
            .Value = "STUDENT PROFILE"
            .Font.Bold = True
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .Font.Name = "Arial"
            .Font.Size = 10
        End With

        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8)), objColor0)

        For i As Integer = 0 To ds_Lista.Tables(8).Rows.Count - 1

            With oCells(int_FilaProfile - 1, int_ColumnaProfile + 8 + 1 + i * 2)
                .Value = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
                posCelda = New posicionCelda
                posCelda.posFila = int_FilaProfile - 1
                posCelda.posColumna = int_ColumnaProfile + 8 + 1 + i * 2
                posCelda.Codigo = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
                lstPosProfile.Add(posCelda)
            End With

            With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile + 8 + 1 + i * 2), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8 + 1 + i * 2))
                .Merge()
                .Value = ds_Lista.Tables(8).Rows(i).Item("Calificativo")
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Bottom
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 9
            End With
            With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile + 8 + 2 + i * 2), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8 + 2 + i * 2))
                .Merge()
                .Value = ds_Lista.Tables(8).Rows(i).Item("CalificativoES")
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Bottom
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 9
            End With
        Next

        Dim int_codCriterio As Integer = 0
        Dim int_codCalificativo As Integer = 0
        Dim int_codCalificativoAux As Integer = 0
        Dim int_codCalificativoPos As Integer = 0

        For i As Integer = 0 To ds_Lista.Tables(7).Rows.Count - 1

            With oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile - 1)
                .Value = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
                int_codCriterio = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
                posCelda = New posicionCelda
                posCelda.posFila = int_FilaProfile + 8 + i * 2
                posCelda.posColumna = int_ColumnaProfile - 1
                posCelda.Codigo = 0
                lstPosProfile.Add(posCelda)
            End With

            With oExcel.Range(oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile), oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile + 8))
                .Merge()
                .Value = ds_Lista.Tables(7).Rows(i).Item("Criterio")
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 9
            End With
            With oExcel.Range(oCells(int_FilaProfile + 9 + i * 2, int_ColumnaProfile), oCells(int_FilaProfile + 9 + i * 2, int_ColumnaProfile + 8))
                .Merge()
                .Value = ds_Lista.Tables(7).Rows(i).Item("CriterioES")
                .HorizontalAlignment = int_HA_Left
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 9
            End With

            For k As Integer = 0 To ds_Lista.Tables(9).Rows.Count - 1
                If int_codCriterio = ds_Lista.Tables(9).Rows(k).Item("CodigoCriterio") Then
                    int_codCalificativo = ds_Lista.Tables(9).Rows(k).Item("CodigoCalificativo")
                    For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
                        If posCel.Codigo > 0 Then
                            If posCel.Codigo = int_codCalificativo Then
                                With oExcel.Range(oCells(int_FilaProfile + 8 + i * 2, posCel.posColumna), oCells(int_FilaProfile + 9 + i * 2, posCel.posColumna + 1))
                                    .Merge()
                                    .Value = "X"
                                    .Font.Bold = True
                                    .HorizontalAlignment = int_HA_Center
                                    .VerticalAlignment = int_VA_Middle
                                    .Font.Name = "Arial"
                                    .Font.Size = 9
                                End With
                                Exit For
                            End If
                        End If
                    Next
                End If
            Next
        Next

        pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor0)

        For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
            With oCells(posCel.posFila, posCel.posColumna)
                .value = ""
            End With
            If posCel.Codigo = 0 Then
                pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila, posCel.posColumna + 1), oCells(posCel.posFila + 1, 8 + int_UltimaColumnaProfile)), objColor0)
            End If
            If posCel.Codigo > 0 Then
                pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila + 1, posCel.posColumna), oCells(int_UltimaFilaProfile, posCel.posColumna + 1)), objColor0)
            End If
        Next

        ' PINTADO DE ASISTENCIA
        Dim int_FilaAsistencia As Integer = int_UltimaFilaComentario + 2
        Dim int_ColumnaAsistencia As Integer = int_UltimaColumnaProfile + 1 + 9

        Dim int_UltimaFilaAsistencia As Integer = int_FilaAsistencia - 1 + 8 + 14
        'Dim int_UltimaColumnaAsistencia As Integer = int_ColumnaAsistencia + 8 + 10

        Dim lstPosAsistencia As New List(Of posicionCelda)
        Dim posCelda2 As posicionCelda

        Dim int_PosAsistenciaFila As Integer = 0
        Dim int_PosAsistenciaColumna As Integer = 0

        With oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "ATTENDANCE                                  Asistencia"
            .Font.Bold = True
            .WrapText = True
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .Font.Name = "Arial"
            .Font.Size = 10
        End With

        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8)), objColor0)
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 10)), objColor0)

        Dim str_Bimestre As String = ""

        For i As Integer = 0 To 4
            Select Case i
                Case 0 : str_Bimestre = "TERM I"
                Case 1 : str_Bimestre = "TERM II"
                Case 2 : str_Bimestre = "TERM III"
                Case 3 : str_Bimestre = "TERM IV"
                Case 4 : str_Bimestre = "AVERAGE"
            End Select
            With oCells(int_FilaAsistencia - 1, int_ColumnaAsistencia + 8 + 1 + i * 2)
                .Value = "" 'i + 1
                posCelda2 = New posicionCelda
                posCelda2.posFila = int_FilaAsistencia - 1
                posCelda2.posColumna = int_ColumnaAsistencia + 8 + 1 + i * 2
                posCelda2.Codigo = i + 1
                lstPosAsistencia.Add(posCelda2)
            End With
            With oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia + 8 + 1 + i * 2), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 2 + i * 2))
                .Merge()
                .Value = str_Bimestre
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Bottom
                .WrapText = True
                .Orientation = 90
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With
        Next

        Dim int_codBimestre As Integer = 0
        Dim int_codBimestreAux As Integer = 0

        Dim lstPosAsis As New List(Of posicionCelda)
        Dim posCelda3 As posicionCelda

        ' TARDANZAS Y CONDUCTA

        ' TARDANZAS
        With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 4))
            .Merge()
            .Value = "LATES          Tardanzas"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 4)), objColor0)

        With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 9, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "Justified          Justificado"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 9
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 9, int_ColumnaAsistencia + 8 + 10)), objColor0)

        With oExcel.Range(oCells(int_FilaAsistencia + 10, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "Unjustified          Injustificado"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 9
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 10, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 8 + 10)), objColor0)

        ' FALTAS
        With oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 4))
            .Merge()
            .Value = "ABSENCES          Ausencias"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 4)), objColor0)

        With oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 13, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "Justified          Justificado"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 9
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 13, int_ColumnaAsistencia + 8 + 10)), objColor0)

        With oExcel.Range(oCells(int_FilaAsistencia + 14, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "Unjustified          Injustificado"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 9
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 14, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 8 + 10)), objColor0)

        ' DEMERITOS
        With oExcel.Range(oCells(int_FilaAsistencia + 16, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 17, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "DEMERITS                                            Deméritos"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 16, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 17, int_ColumnaAsistencia + 18)), objColor0)

        ' MERITOS
        With oExcel.Range(oCells(int_FilaAsistencia + 18, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 19, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "MERITS                                                      Méritos"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 18, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 19, int_ColumnaAsistencia + 18)), objColor0)

        ' CONDUCT MARK
        With oExcel.Range(oCells(int_FilaAsistencia + 20, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 21, int_ColumnaAsistencia + 8))
            .Merge()
            .Value = "CONDUCT MARK                                            Nota de conducta"
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
        End With
        pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 20, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 21, int_ColumnaAsistencia + 18)), objColor0)

        Dim pos_Bimestre As Integer = 0

        If ds_Lista.Tables(10).Rows.Count > 0 Then
            For Each dr As DataRow In ds_Lista.Tables(10).Rows
                If dr.Item("CodigoBimestre") = 1 Then
                    pos_Bimestre = 0
                ElseIf dr.Item("CodigoBimestre") = 2 Then
                    pos_Bimestre = 2
                ElseIf dr.Item("CodigoBimestre") = 3 Then
                    pos_Bimestre = 4
                ElseIf dr.Item("CodigoBimestre") = 4 Then
                    pos_Bimestre = 6
                End If
                With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 9, int_ColumnaAsistencia + 10 + pos_Bimestre))
                    .Merge()
                    .Value = dr.Item("TotalTardanzasJustificadas")
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                End With
                With oExcel.Range(oCells(int_FilaAsistencia + 10, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 10 + pos_Bimestre))
                    .Merge()
                    .Value = dr.Item("TotalTardanzasSinJustificar")
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                End With
                With oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia + +9 + pos_Bimestre), oCells(int_FilaAsistencia + 13, int_ColumnaAsistencia + 10 + pos_Bimestre))
                    .Merge()
                    .Value = dr.Item("TotalFaltasJustificadas")
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                End With
                With oExcel.Range(oCells(int_FilaAsistencia + 14, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 10 + pos_Bimestre))
                    .Merge()
                    .Value = dr.Item("TotalFaltasSinJustificar")
                    .HorizontalAlignment = int_HA_Center
                    .VerticalAlignment = int_VA_Middle
                    .WrapText = True
                    .Font.Name = "Arial"
                    .Font.Size = 10
                    .Font.Bold = True
                End With
            Next


            pos_Bimestre = 8
            Dim str_Rango As String = ""
            'SUMA DE TARDANZAS JUSTIFICADAS
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 8).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 9).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 9, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'SUMA DE TARDANZAS SIN JUSTIFICAR
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 10).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 11).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 10, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 11, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'SUMA DE FALTAS JUSTIFICADAS
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 12).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 13).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 12, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 13, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'SUMA DE FALTAS SIN JUSTIFICAR
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 14).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 15).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 14, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 15, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'SUMA DE DEMERITOS
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 16).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 17).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 16, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 17, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'SUMA DE MERITOS
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 18).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 19).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 18, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 19, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=SUMA(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            'PROMEDIO DE NOTA
            str_Rango = DevLetraColumna(int_ColumnaAsistencia + 9) + (int_FilaAsistencia + 20).ToString + ":" + _
                        DevLetraColumna(int_ColumnaAsistencia + 10 + 6) + (int_FilaAsistencia + 21).ToString

            With oExcel.Range(oCells(int_FilaAsistencia + 20, int_ColumnaAsistencia + 9 + pos_Bimestre), _
                              oCells(int_FilaAsistencia + 21, int_ColumnaAsistencia + 10 + pos_Bimestre))
                .Merge()
                .Value = "=PROMEDIO(" + str_Rango + ")"
                .HorizontalAlignment = int_HA_Center
                .VerticalAlignment = int_VA_Middle
                .WrapText = True
                .Font.Name = "Arial"
                .Font.Size = 10
                .Font.Bold = True
            End With

            oExcel.Range(DevLetraColumna(int_ColumnaAsistencia + 9 + pos_Bimestre) & ":" & _
                         DevLetraColumna(int_ColumnaAsistencia + 9 + pos_Bimestre)).ColumnWidth = 3
            oExcel.Range(DevLetraColumna(int_ColumnaAsistencia + 10 + pos_Bimestre) & ":" & _
                         DevLetraColumna(int_ColumnaAsistencia + 10 + pos_Bimestre)).ColumnWidth = 3

            pos_Bimestre = 0

            If ds_Lista.Tables(11).Rows.Count > 0 Then
                For Each dr As DataRow In ds_Lista.Tables(11).Rows
                    If dr.Item("CodigoBimestre") = 1 Then
                        pos_Bimestre = 0
                    ElseIf dr.Item("CodigoBimestre") = 2 Then
                        pos_Bimestre = 2
                    ElseIf dr.Item("CodigoBimestre") = 3 Then
                        pos_Bimestre = 4
                    ElseIf dr.Item("CodigoBimestre") = 4 Then
                        pos_Bimestre = 6
                    End If
                    With oExcel.Range(oCells(int_FilaAsistencia + 16, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 17, int_ColumnaAsistencia + 10 + pos_Bimestre))
                        .Merge()
                        .Value = dr.Item("TotalDemeritos")
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With
                    With oExcel.Range(oCells(int_FilaAsistencia + 18, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 19, int_ColumnaAsistencia + 10 + pos_Bimestre))
                        .Merge()
                        .Value = dr.Item("TotalMeritos")
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With
                    With oExcel.Range(oCells(int_FilaAsistencia + 20, int_ColumnaAsistencia + 9 + pos_Bimestre), oCells(int_FilaAsistencia + 21, int_ColumnaAsistencia + 10 + pos_Bimestre))
                        .Merge()
                        .Value = dr.Item("NotaConducta")
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                        .WrapText = True
                        .Font.Name = "Arial"
                        .Font.Size = 10
                        .Font.Bold = True
                    End With
                Next

            End If
        End If

        For Each posCel As posicionCelda In lstPosAsistencia ' Limpio todos los codigos pintados previamente
            If posCel.Codigo > 0 Then
                pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila + 1, posCel.posColumna), oCells(int_UltimaFilaAsistencia, posCel.posColumna + 1)), objColor0)
            End If
        Next

        ' FIRMAS
        With oExcel.Range(oCells(int_FilaAsistencia + 25, 2), oCells(int_FilaAsistencia + 25, 5))
            .Merge()
            .Value = "SIGNATURE OF TUTOR"
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
        End With
        pintadoCelda(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 25, 2), oCells(int_FilaAsistencia + 25, 5)), objColor0, 2)

        With oExcel.Range(oCells(int_FilaAsistencia + 25, 8), oCells(int_FilaAsistencia + 25, 23))
            .Merge()
            .Value = "SIGNATURE OF PARENT"
            .HorizontalAlignment = int_HA_Center
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 10
            .Font.Bold = True
        End With
        pintadoCelda(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 25, 8), oCells(int_FilaAsistencia + 25, 23)), objColor0, 2)


        oExcel.Rows("6:6").Delete() ' Elimino la fila de codigos de asignacion de grupos
        oExcel.Rows("7:7").Delete() ' Elimino la fila de codigos de los calificativos
        oExcel.Rows("5:5").Insert()
        oExcel.Rows("5:5").RowHeight = 5 ' Listado de Cursos
        borrarPintado(oExcel, oExcel.Range(oCells(5, 2), oCells(5, int_UltimaColumna)))
        cuadradoCompleto(oExcel, oExcel.Range(oCells(2, 6), oCells(4, 24)))
        'oExcel.Columns("A:A").Delete()
        oExcel.Range("A:A").ColumnWidth = 3


        oExcel.ActiveWindow.Zoom = 75
        Return str_Fila

    End Function
    'Private Shared Function LlenarPlantillaReporteLibretaSecundaria( _
    '    ByVal ds_Lista As System.Data.DataSet, _
    '    ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
    '    ByVal oExcel As Microsoft.Office.Interop.Excel.Application) As String

    '    Dim dtReporte As System.Data.DataTable = ds_Lista.Tables(0)

    '    Dim fila As Integer = 5
    '    Dim columna As Integer = 2
    '    Dim cont_columnas As Integer = 0
    '    Dim cont_filas As Integer = 0
    '    Dim str_Fila As String = ""

    '    cont_columnas = 0
    '    cont_filas = 0

    '    columna += 1

    '    ' Pintado de Titulo
    '    Dim str_GradoAula As String = ds_Lista.Tables(4).Rows(0).Item("DescIngles").ToString
    '    Dim str_NombreTutor As String = ds_Lista.Tables(4).Rows(0).Item("NombreTutor").ToString
    '    Dim str_NombreAlumno As String = ds_Lista.Tables(4).Rows(0).Item("NombreAlumno").ToString

    '    With oExcel.Range(oCells(1, 6), oCells(1, 24)) ' NAME
    '        .Merge()
    '        .Font.Name = "Arial"
    '        .Font.Size = 20
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Center
    '        .VerticalAlignment = int_VA_Middle
    '        .Value = "REPORT CARD"
    '    End With
    '    oExcel.Rows("1:1").RowHeight = 40 ' Listado de Cursos

    '    With oExcel.Range(oCells(2, 6), oCells(2, 8)) ' NAME
    '        .Merge()
    '        .Font.Name = "Arial"
    '        .Font.Size = 10
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Left
    '        .VerticalAlignment = int_VA_Middle
    '        .Value = "NAME"
    '    End With

    '    With oExcel.Range(oCells(2, 9), oCells(2, 24)) ' NAME
    '        .Merge()
    '        .Font.Name = "Arial"
    '        .Font.Size = 10
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Left
    '        .VerticalAlignment = int_VA_Middle
    '        .Value = str_NombreAlumno
    '    End With
    '    oExcel.Rows("2:2").RowHeight = 20

    '    With oExcel.Range(oCells(3, 6), oCells(3, 8)) ' NAME
    '        .Merge()
    '        .Font.Name = "Arial"
    '        .Font.Size = 10
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Left
    '        .VerticalAlignment = int_VA_Middle
    '        .Value = "CLASS"
    '    End With

    '    With oExcel.Range(oCells(3, 9), oCells(3, 24)) ' CLASS
    '        .Merge()
    '        .Font.Name = "Arial"
    '        .Font.Size = 10
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Left
    '        .VerticalAlignment = int_VA_Middle
    '        .Value = str_GradoAula
    '    End With
    '    oExcel.Rows("3:3").RowHeight = 20

    '    With oExcel.Range(oCells(4, 6), oCells(4, 8)) ' NAME
    '        .Merge()
    '        .Font.Name = "Arial"
    '        .Font.Size = 10
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Left
    '        .VerticalAlignment = int_VA_Middle
    '        .Value = "TUTOR"
    '    End With

    '    With oExcel.Range(oCells(4, 9), oCells(4, 24)) ' TUTOR
    '        .Merge()
    '        .Font.Name = "Arial"
    '        .Font.Size = 10
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Left
    '        .VerticalAlignment = int_VA_Middle
    '        .Value = str_NombreTutor
    '    End With
    '    oExcel.Rows("4:4").RowHeight = 20

    '    cuadradoCompleto(oExcel, oExcel.Range(oCells(2, 6), oCells(4, 24)))

    '    'oExcel.Rows("3:3").RowHeight = 10

    '    'Pintado de Fecha 
    '    With oCells(2, 30)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
    '        .HorizontalAlignment = int_HA_Left
    '        .Font.Bold = True
    '        .Value = "Date: " & Today.ToString("MMMM dd, yyyy")
    '    End With

    '    ' '' ''Pintado de Hora 
    '    '' ''With oCells(2, 24)  'oExcel.Range(oCells(3, 20), oCells(3, 4))
    '    '' ''    .HorizontalAlignment = int_HA_Left
    '    '' ''    .Font.Bold = True
    '    '' ''    .Value = "Hora: " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
    '    '' ''End With

    '    Dim colIni As Integer = 0
    '    Dim colFin As Integer = 0
    '    Dim lstPosCursos As New List(Of Integer)

    '    For i As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos

    '        colIni = columna + (i * 3)
    '        colFin = colIni + 2

    '        lstPosCursos.Add(colIni) ' agrego la columna de la posicion inicial

    '        With oExcel.Range(oCells(fila, colIni), oCells(fila, colFin))
    '            .Merge()
    '            .Value = ds_Lista.Tables(3).Rows(i).Item("NombreCurso")
    '            .Font.Name = "Arial"
    '            .Font.Size = 8
    '            .HorizontalAlignment = int_HA_Center
    '            .VerticalAlignment = int_VA_Middle
    '            .ColumnWidth = 3
    '            .WrapText = True
    '        End With

    '        With oExcel.Range(oCells(fila + 1, colIni), oCells(fila + 1, colFin)) ' Fila 6 : Lista de Codigos Asignacion de Grupo
    '            .Merge()
    '            .Value = ds_Lista.Tables(3).Rows(i).Item("CodigoAsignacionGrupo")
    '            .Font.Name = "Arial"
    '            .Font.Size = 8
    '            .HorizontalAlignment = int_HA_Center
    '            .VerticalAlignment = int_VA_Middle
    '            .ColumnWidth = 3
    '            .WrapText = True
    '        End With
    '    Next
    '    oExcel.Rows("5:5").RowHeight = 30 ' Listado de Cursos

    '    columna -= 1

    '    Dim dt As System.Data.DataTable = ds_Lista.Tables(0)
    '    Dim sql = From s In ds_Lista.Tables(0).AsEnumerable() _
    '              Select CodigoAsignacionGrupo = s.Field(Of Integer)("CodigoGrupoCriterio") _
    '              Distinct

    '    Dim int_NumGrupo As Integer = sql.Count
    '    Dim int_NumCriterios As Integer = ds_Lista.Tables(0).Rows.Count
    '    Dim int_NumCalificativos As Integer = ds_Lista.Tables(1).Rows.Count
    '    Dim int_NumCursos As Integer = ds_Lista.Tables(3).Rows.Count

    '    Dim int_UltimaFila As Integer = fila + int_NumCriterios + int_NumGrupo + 2 ' 2 grupos de criterios extras
    '    Dim int_UltimaColumna As Integer = columna + (int_NumCursos * int_NumCalificativos) ' 4 columnas con campos calculados

    '    fila += 1
    '    fila += 1

    '    Dim lstPos As New List(Of Integer)
    '    Dim str_Grupo As String = ""
    '    Dim bool_PintadoGrupo As Boolean = False

    '    Dim int_CodigoAsignacionGrupo As Integer = 0
    '    Dim int_CodigoCalificativo As Integer = 0
    '    Dim int_Idx As Integer = 0
    '    Dim str_Nota As String = ""
    '    Dim bool_NotaCriterio As Boolean = False

    '    For i As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1 ' Pintado de Criterios
    '        colIni = 0
    '        If str_Grupo = "" Or str_Grupo <> ds_Lista.Tables(0).Rows(i).Item("GrupoCriterio") Then
    '            str_Grupo = ds_Lista.Tables(0).Rows(i).Item("GrupoCriterio")
    '            With oCells(fila + i, columna)
    '                .Font.Bold = True
    '                .Value = str_Grupo
    '            End With
    '            If bool_PintadoGrupo = False Then
    '                For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos
    '                    colIni = columna + 1 + (j * 3)
    '                    For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1 ' Pintado de Calificativos
    '                        With oCells(fila + i, colIni + k)
    '                            .Font.Bold = True
    '                            .Value = ds_Lista.Tables(1).Rows(k).Item("Abreviatura")
    '                            .HorizontalAlignment = int_HA_Center
    '                            .VerticalAlignment = int_VA_Middle
    '                        End With
    '                        oCells(fila + i + 1, colIni + k).Value = ds_Lista.Tables(1).Rows(k).Item("CodigoCalificativo")
    '                    Next
    '                Next
    '                bool_PintadoGrupo = True
    '                fila += 1
    '            End If
    '            lstPos.Add(fila + i)
    '            fila += 1
    '        End If

    '        oCells(fila + i, columna) = ds_Lista.Tables(0).Rows(i).Item("Criterio")
    '        int_CodigoAsignacionGrupo = 0
    '        colIni = 0

    '        For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1
    '            colIni = columna + 1 + (j * 3)
    '            int_CodigoAsignacionGrupo = oCells(6, colIni).Text()
    '            int_CodigoCalificativo = 0
    '            If bool_NotaCriterio Then : bool_NotaCriterio = False : End If
    '            For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1
    '                int_CodigoCalificativo = oCells(8, columna + 1 + k).Text()
    '                For l As Integer = 0 To ds_Lista.Tables(2).Rows.Count - 1
    '                    If ds_Lista.Tables(2).Rows(l).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo And ds_Lista.Tables(2).Rows(l).Item("CodigoCalificativo") = int_CodigoCalificativo Then
    '                        With oCells(fila + i, colIni + k)
    '                            .value = "X"
    '                            .HorizontalAlignment = int_HA_Center
    '                            .VerticalAlignment = int_VA_Middle
    '                        End With
    '                        bool_NotaCriterio = True
    '                        Exit For
    '                    End If
    '                Next
    '                If bool_NotaCriterio Then : Exit For : End If
    '            Next
    '        Next
    '    Next

    '    oExcel.Range(oCells(5, columna), oCells(fila - 1, columna)).EntireColumn.AutoFit() ' Listado de Criterios


    '    Dim objColor0 As Object = RGB(0, 0, 0) 'Negro
    '    Dim objColor1 As Object = RGB(191, 191, 191) 'Plomo

    '    pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor0)
    '    pintadoInterior(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor1)
    '    pintadoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(5, int_UltimaColumna)), objColor0)
    '    pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila, 2)), objColor0)

    '    pintadoCelda(oExcel, oExcel.Range(oCells(lstPos(1), 2), oCells(lstPos(1), int_UltimaColumna)), objColor0, 2) ' separador de grupos de criterio
    '    pintadoCelda(oExcel, oExcel.Range(oCells(int_UltimaFila, 2), oCells(int_UltimaFila, int_UltimaColumna)), objColor0, 4) ' separador Notas

    '    For i As Integer = 0 To lstPosCursos.Count - 1
    '        pintadoCelda(oExcel, oExcel.Range(oCells(5, lstPosCursos(i)), oCells(int_UltimaFila + 2, lstPosCursos(i))), objColor0, 1) ' separador de cursos
    '    Next

    '    With oCells(int_UltimaFila + 1, 2)
    '        .Value = "ACADEMIC PERFORMANCE"
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Left
    '        .VerticalAlignment = int_VA_Middle
    '    End With

    '    With oCells(int_UltimaFila + 2, 2)
    '        .Value = "OVERALL ATTAINMENT"
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Left
    '        .VerticalAlignment = int_VA_Middle
    '    End With

    '    oExcel.Rows((int_UltimaFila + 2).ToString & ":" & (int_UltimaFila + 2).ToString).RowHeight = 24 ' Listado de Cursos

    '    colIni = 0
    '    colFin = 0

    '    For i As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1 ' Pintado de Nota de Cursos 
    '        colIni = 0
    '        colIni = columna + 1 + (i * 3)
    '        colFin = colIni + 2
    '        int_CodigoAsignacionGrupo = oCells(6, colIni).Text()
    '        For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
    '            If ds_Lista.Tables(5).Rows(j).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo Then
    '                With oExcel.Range(oCells(int_UltimaFila + 1, colIni), oCells(int_UltimaFila + 1, colFin))
    '                    .Merge()
    '                    .Value = ds_Lista.Tables(5).Rows(j).Item("NotaBimestre")
    '                    .Font.Name = "Arial"
    '                    .Font.Size = 8
    '                    .Font.Bold = True
    '                    .HorizontalAlignment = int_HA_Center
    '                    .VerticalAlignment = int_VA_Middle
    '                    .WrapText = True
    '                End With
    '                With oExcel.Range(oCells(int_UltimaFila + 1 + 1, colIni), oCells(int_UltimaFila + 1 + 1, colFin))
    '                    .Merge()
    '                    .Value = ds_Lista.Tables(5).Rows(j).Item("Observacion")
    '                    .Font.Name = "Arial"
    '                    .Font.Size = 8
    '                    .Font.Bold = True
    '                    .HorizontalAlignment = int_HA_Center
    '                    .VerticalAlignment = int_VA_Middle
    '                    .WrapText = True
    '                End With
    '                Exit For
    '            End If
    '        Next
    '    Next

    '    ' PINTADO DE COMENTARIOS 
    '    Dim int_FilaComentario As Integer = int_UltimaFila + 3
    '    Dim int_MaxNumFilasComentario As Integer = 20 * 2
    '    Dim int_UltimaFilaComentario As Integer = int_FilaComentario + int_MaxNumFilasComentario 'ds_Lista.Tables(6).Rows.Count
    '    Dim int_FilasPintadas As Integer = 0
    '    int_FilaComentario += 1
    '    With oCells(int_FilaComentario, 2)
    '        .Value = "COMMENTS"
    '        .Font.Bold = True
    '    End With
    '    int_FilaComentario += 1
    '    For i As Integer = 0 To ds_Lista.Tables(6).Rows.Count - 1 ' Pintado de Oservaciones de Cursos 
    '        If ds_Lista.Tables(6).Rows(i).Item("Observacion") IsNot DBNull.Value Then
    '            With oExcel.Range(oCells(int_FilaComentario + i * 2, 2), oCells(int_FilaComentario + i * 2, int_UltimaColumna))
    '                .Merge()
    '                .Value = ds_Lista.Tables(6).Rows(i).Item("Curso") & ":"
    '                .Font.Name = "Arial"
    '                .Font.Size = 10
    '                .Font.Bold = True
    '                .HorizontalAlignment = int_HA_Left
    '                .VerticalAlignment = int_VA_Middle
    '                .WrapText = True
    '            End With

    '            With oExcel.Range(oCells(int_FilaComentario + i * 2 + 1, 2), oCells(int_FilaComentario + i * 2 + 1, int_UltimaColumna))
    '                .Merge()
    '                .Value = ds_Lista.Tables(6).Rows(i).Item("Observacion")
    '                .Font.Name = "Arial"
    '                .Font.Size = 9
    '                .HorizontalAlignment = int_HA_Left
    '                .VerticalAlignment = int_VA_Middle
    '                .WrapText = True
    '            End With

    '            int_FilasPintadas += 1
    '        End If
    '    Next
    '    pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaComentario, 2), oCells(int_UltimaFilaComentario, int_UltimaColumna)), objColor0)

    '    ' PINTADO DE NOTAS 
    '    Dim int_FilaNotas As Integer = int_UltimaFilaComentario + 2
    '    Dim int_UltimaFilaNotas As Integer = int_FilaNotas + ds_Lista.Tables(5).Rows.Count
    '    Dim int_UltimaColumnaNotas As Integer = 2 + ds_Lista.Tables(5).Columns.Count - 6

    '    With oExcel.Range(oCells(int_FilaNotas, 2), oCells(int_FilaNotas, 11))
    '        .Merge()
    '        .Value = "TERM AND ANNUAL MARK"
    '        .Font.Name = "Arial"
    '        .Font.Size = 10
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Center
    '        .VerticalAlignment = int_VA_Middle
    '        .WrapText = True
    '    End With
    '    int_FilaNotas += 1

    '    For i As Integer = 0 To ds_Lista.Tables(5).Columns.Count - 1
    '        If i > 3 And i < 13 Then
    '            If i = 12 Then
    '                With oExcel.Range(oCells(int_FilaNotas, 2 + i - 4), oCells(int_FilaNotas, 2 + i - 4 + 1))
    '                    .Merge()
    '                    .Value = ds_Lista.Tables(5).Columns(i).ColumnName
    '                    .Font.Bold = True
    '                    .HorizontalAlignment = int_HA_Center
    '                    .VerticalAlignment = int_VA_Middle
    '                    .Font.Name = "Arial"
    '                    .Font.Size = 9
    '                End With
    '                For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
    '                    With oExcel.Range(oCells(int_FilaNotas + 1 + j, 2 + i - 4), oCells(int_FilaNotas + 1 + j, 2 + i - 4 + 1))
    '                        .Merge()
    '                        .Value = ds_Lista.Tables(5).Rows(j).Item(i)
    '                        .Font.Bold = True
    '                        .HorizontalAlignment = int_HA_Center
    '                        .VerticalAlignment = int_VA_Middle
    '                        .Font.Name = "Arial"
    '                        .Font.Size = 8
    '                    End With
    '                Next
    '            Else
    '                With oExcel.Range(oCells(int_FilaNotas, 2 + i - 4), oCells(int_FilaNotas, 2 + i - 4))
    '                    .Value = ds_Lista.Tables(5).Columns(i).ColumnName
    '                    .Font.Bold = True
    '                    .HorizontalAlignment = int_HA_Center
    '                    .VerticalAlignment = int_VA_Middle
    '                    .Font.Name = "Arial"
    '                    .Font.Size = 9
    '                End With
    '                For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
    '                    With oExcel.Range(oCells(int_FilaNotas + 1 + j, 2 + i - 4), oCells(int_FilaNotas + 1 + j, 2 + i - 4))
    '                        .Value = ds_Lista.Tables(5).Rows(j).Item(i)
    '                        .Font.Bold = True
    '                        If i = 4 Then
    '                            .HorizontalAlignment = int_HA_Left
    '                            .VerticalAlignment = int_VA_Middle
    '                            .Font.Size = 10
    '                        Else
    '                            .HorizontalAlignment = int_HA_Center
    '                            .VerticalAlignment = int_VA_Middle
    '                            .Font.Size = 8
    '                        End If
    '                        .Font.Name = "Arial"
    '                    End With
    '                Next
    '            End If
    '        End If
    '    Next
    '    pintadoCompleto(oExcel, oExcel.Range(oCells(int_FilaNotas - 1, 2), oCells(int_UltimaFilaNotas, int_UltimaColumnaNotas)), objColor0)

    '    With oCells(int_UltimaFilaNotas + 1, 2)
    '        .Value = "Note: The note indicates 'P' value test is pending."
    '        .Font.Bold = True
    '    End With

    '    ' PINTADO DE STUDENT PROFILE 
    '    Dim int_FilaProfile As Integer = int_UltimaFilaComentario + 2
    '    Dim int_ColumnaProfile As Integer = int_UltimaColumnaNotas + 2
    '    Dim int_UltimaFilaProfile As Integer = int_FilaNotas - 2 + 8 + ds_Lista.Tables(7).Rows.Count * 2
    '    Dim int_UltimaColumnaProfile As Integer = int_ColumnaProfile + ds_Lista.Tables(8).Rows.Count * 2

    '    Dim lstPosProfile As New List(Of posicionCelda)
    '    Dim posCelda As posicionCelda

    '    Dim int_PosProfileFila As Integer = 0
    '    Dim int_PosProfileColumna As Integer = 0

    '    With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8))
    '        .Merge()
    '        .Value = "STUDENT PROFILE"
    '        .Font.Bold = True
    '        .HorizontalAlignment = int_HA_Center
    '        .VerticalAlignment = int_VA_Middle
    '        .Font.Name = "Arial"
    '        .Font.Size = 10
    '    End With

    '    pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8)), objColor0)

    '    For i As Integer = 0 To ds_Lista.Tables(8).Rows.Count - 1

    '        With oCells(int_FilaProfile - 1, int_ColumnaProfile + 8 + 1 + i * 2)
    '            .Value = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
    '            posCelda = New posicionCelda
    '            posCelda.posFila = int_FilaProfile - 1
    '            posCelda.posColumna = int_ColumnaProfile + 8 + 1 + i * 2
    '            posCelda.Codigo = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
    '            lstPosProfile.Add(posCelda)
    '        End With

    '        With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile + 8 + 1 + i * 2), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8 + 1 + i * 2))
    '            .Merge()
    '            .Value = ds_Lista.Tables(8).Rows(i).Item("Calificativo")
    '            .HorizontalAlignment = int_HA_Center
    '            .VerticalAlignment = int_VA_Bottom
    '            .WrapText = True
    '            .Orientation = 90
    '            .AddIndent = False
    '            .IndentLevel = 0
    '            .ShrinkToFit = False
    '            .Font.Name = "Arial"
    '            .Font.Size = 9
    '        End With
    '        With oExcel.Range(oCells(int_FilaProfile, int_ColumnaProfile + 8 + 2 + i * 2), oCells(int_FilaProfile + 7, int_ColumnaProfile + 8 + 2 + i * 2))
    '            .Merge()
    '            .Value = ds_Lista.Tables(8).Rows(i).Item("CalificativoES")
    '            .HorizontalAlignment = int_HA_Center
    '            .VerticalAlignment = int_VA_Bottom
    '            .WrapText = True
    '            .Orientation = 90
    '            .AddIndent = False
    '            .IndentLevel = 0
    '            .ShrinkToFit = False
    '            .Font.Name = "Arial"
    '            .Font.Size = 9
    '        End With
    '    Next

    '    Dim int_codCriterio As Integer = 0
    '    Dim int_codCalificativo As Integer = 0
    '    Dim int_codCalificativoAux As Integer = 0
    '    Dim int_codCalificativoPos As Integer = 0

    '    For i As Integer = 0 To ds_Lista.Tables(7).Rows.Count - 1

    '        With oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile - 1)
    '            .Value = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
    '            int_codCriterio = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
    '            posCelda = New posicionCelda
    '            posCelda.posFila = int_FilaProfile + 8 + i * 2
    '            posCelda.posColumna = int_ColumnaProfile - 1
    '            posCelda.Codigo = 0
    '            lstPosProfile.Add(posCelda)
    '        End With

    '        With oExcel.Range(oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile), oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile + 8))
    '            .Merge()
    '            .Value = ds_Lista.Tables(7).Rows(i).Item("Criterio")
    '            .HorizontalAlignment = int_HA_Left
    '            .VerticalAlignment = int_VA_Middle
    '            .WrapText = True
    '            .Font.Name = "Arial"
    '            .Font.Size = 9
    '        End With
    '        With oExcel.Range(oCells(int_FilaProfile + 9 + i * 2, int_ColumnaProfile), oCells(int_FilaProfile + 9 + i * 2, int_ColumnaProfile + 8))
    '            .Merge()
    '            .Value = ds_Lista.Tables(7).Rows(i).Item("CriterioES")
    '            .HorizontalAlignment = int_HA_Left
    '            .VerticalAlignment = int_VA_Middle
    '            .WrapText = True
    '            .Font.Name = "Arial"
    '            .Font.Size = 9
    '        End With

    '        For k As Integer = 0 To ds_Lista.Tables(9).Rows.Count - 1
    '            If int_codCriterio = ds_Lista.Tables(9).Rows(k).Item("CodigoCriterio") Then
    '                int_codCalificativo = ds_Lista.Tables(9).Rows(k).Item("CodigoCalificativo")
    '                For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
    '                    If posCel.Codigo > 0 Then
    '                        If posCel.Codigo = int_codCalificativo Then
    '                            With oExcel.Range(oCells(int_FilaProfile + 8 + i * 2, posCel.posColumna), oCells(int_FilaProfile + 9 + i * 2, posCel.posColumna + 1))
    '                                .Merge()
    '                                .Value = "X"
    '                                .Font.Bold = True
    '                                .HorizontalAlignment = int_HA_Center
    '                                .VerticalAlignment = int_VA_Middle
    '                                .Font.Name = "Arial"
    '                                .Font.Size = 9
    '                            End With
    '                            Exit For
    '                        End If
    '                    End If
    '                Next
    '            End If
    '        Next
    '    Next

    '    pintadoBordes(oExcel, oExcel.Range(oCells(5, 2), oCells(int_UltimaFila + 2, int_UltimaColumna)), objColor0)

    '    For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
    '        With oCells(posCel.posFila, posCel.posColumna)
    '            .value = ""
    '        End With
    '        If posCel.Codigo = 0 Then
    '            pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila, posCel.posColumna + 1), oCells(posCel.posFila + 1, 8 + int_UltimaColumnaProfile)), objColor0)
    '        End If
    '        If posCel.Codigo > 0 Then
    '            pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila + 1, posCel.posColumna), oCells(int_UltimaFilaProfile, posCel.posColumna + 1)), objColor0)
    '        End If
    '    Next

    '    ' PINTADO DE ASISTENCIA
    '    Dim int_FilaAsistencia As Integer = int_UltimaFilaComentario + 2
    '    Dim int_ColumnaAsistencia As Integer = int_UltimaColumnaProfile + 1 + 9

    '    Dim int_UltimaFilaAsistencia As Integer = int_FilaAsistencia - 1 + 8 + 12
    '    'Dim int_UltimaColumnaAsistencia As Integer = int_ColumnaAsistencia + 8 + 10

    '    Dim lstPosAsistencia As New List(Of posicionCelda)
    '    Dim posCelda2 As posicionCelda

    '    Dim int_PosAsistenciaFila As Integer = 0
    '    Dim int_PosAsistenciaColumna As Integer = 0

    '    With oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8))
    '        .Merge()
    '        .Value = "ATTENDANCE                                  Asistencia"
    '        .Font.Bold = True
    '        .WrapText = True
    '        .HorizontalAlignment = int_HA_Left
    '        .VerticalAlignment = int_VA_Middle
    '        .Font.Name = "Arial"
    '        .Font.Size = 10
    '    End With

    '    pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8)), objColor0)
    '    pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 10)), objColor0)

    '    Dim str_Bimestre As String = ""

    '    For i As Integer = 0 To 4
    '        Select Case i
    '            Case 0 : str_Bimestre = "TERM I"
    '            Case 1 : str_Bimestre = "TERM II"
    '            Case 2 : str_Bimestre = "TERM III"
    '            Case 3 : str_Bimestre = "TERM IV"
    '            Case 4 : str_Bimestre = "AVERAGE"
    '        End Select
    '        With oCells(int_FilaAsistencia - 1, int_ColumnaAsistencia + 8 + 1 + i * 2)
    '            .Value = i + 1
    '            posCelda2 = New posicionCelda
    '            posCelda2.posFila = int_FilaAsistencia - 1
    '            posCelda2.posColumna = int_ColumnaAsistencia + 8 + 1 + i * 2
    '            posCelda2.Codigo = i + 1
    '            lstPosAsistencia.Add(posCelda2)
    '        End With
    '        With oExcel.Range(oCells(int_FilaAsistencia, int_ColumnaAsistencia + 8 + 1 + i * 2), oCells(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 2 + i * 2))
    '            .Merge()
    '            .Value = str_Bimestre
    '            .HorizontalAlignment = int_HA_Center
    '            .VerticalAlignment = int_VA_Bottom
    '            .WrapText = True
    '            .Orientation = 90
    '            .AddIndent = False
    '            .IndentLevel = 0
    '            .ShrinkToFit = False
    '            .Font.Name = "Arial"
    '            .Font.Size = 10
    '            .Font.Bold = True
    '        End With
    '    Next

    '    Dim int_codBimestre As Integer = 0
    '    Dim int_codBimestreAux As Integer = 0

    '    Dim lstPosAsis As New List(Of posicionCelda)
    '    Dim posCelda3 As posicionCelda

    '    ' Tardanzas
    '    For i As Integer = 1 To 7
    '        If i < 5 Then
    '            If i = 1 Then
    '                With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia), oCells(int_FilaAsistencia + 8 - 1 + i * 4, int_ColumnaAsistencia + 4))
    '                    .Merge()
    '                    .Value = "LATES          Tardanzas"
    '                    .HorizontalAlignment = int_HA_Left
    '                    .VerticalAlignment = int_VA_Middle
    '                    .WrapText = True
    '                    .Font.Name = "Arial"
    '                    .Font.Size = 10
    '                End With

    '                With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 8 - 1 + i * 2, int_ColumnaAsistencia + 8))
    '                    .Merge()
    '                    .Value = "Justified          Justificado"
    '                    .HorizontalAlignment = int_HA_Left
    '                    .VerticalAlignment = int_VA_Middle
    '                    .WrapText = True
    '                    .Font.Name = "Arial"
    '                    .Font.Size = 9
    '                End With

    '                With oExcel.Range(oCells(int_FilaAsistencia + 8 + 2, int_ColumnaAsistencia + 5), oCells(int_FilaAsistencia + 8 - 1 + 2 + i * 2, int_ColumnaAsistencia + 8))
    '                    .Merge()
    '                    .Value = "Unjustified          Injustificado"
    '                    .HorizontalAlignment = int_HA_Left
    '                    .VerticalAlignment = int_VA_Middle
    '                    .WrapText = True
    '                    .Font.Name = "Arial"
    '                    .Font.Size = 9
    '                End With
    '                'ElseIf i = 3 Then
    '                '    With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaProfile), oCells(int_FilaAsistencia + 8 + i * 4, int_ColumnaProfile + 4))
    '                '        .Merge()
    '                '        .Value = "ABSENCES Ausencias"
    '                '        .HorizontalAlignment = int_HA_Left
    '                '        .VerticalAlignment = int_VA_Middle
    '                '        .WrapText = True
    '                '        .Font.Name = "Arial"
    '                '        .Font.Size = 10
    '                '    End With
    '            End If
    '            'pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaProfile), oCells(int_FilaAsistencia + 8 + i * 4, int_ColumnaProfile + 4)), objColor0)
    '        Else
    '            'If i = 5 Then
    '            '    With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaProfile), oCells(int_FilaAsistencia + 8 + i * 4, int_ColumnaProfile + 4))
    '            '        .Merge()
    '            '        .Value = "DEMERITS (Deméritos)"
    '            '        .HorizontalAlignment = int_HA_Left
    '            '        .VerticalAlignment = int_VA_Middle
    '            '        .WrapText = True
    '            '        .Font.Name = "Arial"
    '            '        .Font.Size = 10
    '            '    End With
    '            'ElseIf i = 6 Then
    '            '    With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaProfile), oCells(int_FilaAsistencia + 8 + i * 4, int_ColumnaProfile + 4))
    '            '        .Merge()
    '            '        .Value = "MERITS (Méritos)"
    '            '        .HorizontalAlignment = int_HA_Left
    '            '        .VerticalAlignment = int_VA_Middle
    '            '        .WrapText = True
    '            '        .Font.Name = "Arial"
    '            '        .Font.Size = 10
    '            '    End With
    '            'ElseIf i = 7 Then
    '            '    With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaProfile), oCells(int_FilaAsistencia + 8 + i * 4, int_ColumnaProfile + 4))
    '            '        .Merge()
    '            '        .Value = "CONDUCT MARK (Nota de Conducta)"
    '            '        .HorizontalAlignment = int_HA_Left
    '            '        .VerticalAlignment = int_VA_Middle
    '            '        .WrapText = True
    '            '        .Font.Name = "Arial"
    '            '        .Font.Size = 10
    '            '    End With
    '            'End If
    '            'pintadoBordes(oExcel, oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaProfile), oCells(int_FilaAsistencia + 8 + i * 4, int_ColumnaProfile + 4)), objColor0)

    '        End If
    '    Next


    '    'For i As Integer = 0 To ds_Lista.Tables(11).Rows.Count
    '    '    int_codBimestre = ds_Lista.Tables(11).Rows(i).Item("CodigoBimestre")

    '    '    With oExcel.Range(oCells(int_FilaAsistencia + 8, int_ColumnaProfile), oCells(int_FilaProfile + 8 + i * 2, int_ColumnaProfile + 8))
    '    '        .Merge()
    '    '        .Value = ds_Lista.Tables(7).Rows(i).Item("Criterio")
    '    '        .HorizontalAlignment = int_HA_Left
    '    '        .VerticalAlignment = int_VA_Middle
    '    '        .WrapText = True
    '    '        .Font.Name = "Arial"
    '    '        .Font.Size = 9
    '    '    End With


    '    'Next






    '    For Each posCel As posicionCelda In lstPosAsistencia ' Limpio todos los codigos pintados previamente
    '        'With oCells(posCel.posFila, posCel.posColumna)
    '        '    .value = ""
    '        'End With
    '        'If posCel.Codigo = 0 Then
    '        '    pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila, posCel.posColumna + 1), oCells(posCel.posFila + 1, 8 + int_UltimaColumnaProfile)), objColor0)
    '        'End If
    '        If posCel.Codigo > 0 Then
    '            pintadoBordes(oExcel, oExcel.Range(oCells(posCel.posFila + 1, posCel.posColumna), oCells(int_UltimaFilaAsistencia, posCel.posColumna + 1)), objColor0)
    '        End If
    '    Next

    '    ' PINTADO DE CONDUCTA




    '    oExcel.Rows("6:6").Delete() ' Elimino la fila de codigos de asignacion de grupos
    '    oExcel.Rows("7:7").Delete() ' Elimino la fila de codigos de los calificativos
    '    oExcel.Rows("5:5").Insert()
    '    oExcel.Rows("5:5").RowHeight = 5 ' Listado de Cursos
    '    borrarPintado(oExcel, oExcel.Range(oCells(5, 2), oCells(5, int_UltimaColumna)))
    '    oExcel.Columns("A:A").Delete()
    '    cuadradoCompleto(oExcel, oExcel.Range(oCells(2, 6), oCells(4, 24)))

    '    oExcel.ActiveWindow.Zoom = 75
    '    Return str_Fila

    'End Function


    Private Shared Sub pintadoBordes(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                     ByVal objRango As Microsoft.Office.Interop.Excel.Range, _
                     ByVal objColor As Object)
        Try

            objRango.Select()
            With mexcel

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub pintadoInterior(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range, _
                         ByVal objColor As Object)
        Try

            objRango.Select()
            With mexcel

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .Color = objColor
                End With

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub pintadoCelda(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range, _
                         ByVal objColor As Object, _
                         ByVal int_Posicion As Integer)
        Try

            objRango.Select()
            With mexcel

                If int_Posicion = 1 Then ' LEFT
                    With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                        .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                        .ColorIndex = objColor
                    End With
                End If
                If int_Posicion = 2 Then ' TOP
                    With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                        .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                        .ColorIndex = objColor
                    End With
                End If
                If int_Posicion = 3 Then ' RIGHT
                    With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                        .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                        .ColorIndex = objColor
                    End With
                End If
                If int_Posicion = 4 Then ' BOTTOM
                    With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                        .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                        .ColorIndex = objColor
                    End With
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub borrarPintado(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range)
        Try

            objRango.Select()
            With mexcel

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                End With

                'With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                '    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                'End With

                'With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                '    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                'End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone
                End With

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub pintadoCompleto(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range, _
                         ByVal objColor As Object)
        Try

            objRango.Select()
            With mexcel

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = objColor
                End With

            End With
        Catch ex As Exception

        End Try
    End Sub

    Public Class posicionCelda
        Public posFila As Integer
        Public posColumna As Integer
        Public Codigo As Integer
    End Class

#End Region


#Region "Exportacion Reportes"

    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current
    Private Shared Function GetNewName() As String
        Dim sName As String = Convert.ToString(DateTime.Now.Ticks)
        Return sName
    End Function

    'Reporte Relación de Alumno - Por Salon: 1 - 1
    Public Function ExportarReportePorSalon(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range

        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"

        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("Plantilla_ReporteRelacionAlumno").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReportePorSalon(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Function LlenarPlantillaReportePorSalon( _
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
        With oExcel.Range(oCells(1, 1), oCells(1, 5))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "LISTADO DE ALUMNOS"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(2, 1), oCells(2, 5))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 16
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        'Nivel, SubNivel, Grado, Aula
        oExcel.Range(oCells(4, 1), oCells(4, 5)).Merge()
        oExcel.Range(oCells(4, 1), oCells(4, 5)).HorizontalAlignment = 3
        oCells(4, 1) = "Nivel:" & ddlRep1_Nivel.SelectedItem.ToString & "                        " & _
        "SubNivel:" & ddlRep1_SubNivel.SelectedItem.ToString & "                        " & _
        "Grado:" & ddlRep1_Grado.SelectedItem.ToString & "                        " & _
        "Aula:" & ddlRep1_Aula.SelectedItem.ToString

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
        oExcel.Range(oCells(6, 1), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(6, 1), oCells(fila - 1, columna + cont_columnas - 1)))
        'Inmovilizar Cabecera
        oExcel.Rows("7:7").Select()
        oExcel.ActiveWindow.FreezePanes = True
        'Ancho de la celda
        oExcel.Columns("B:B").ColumnWidth = 16.57
        oExcel.Columns("C:C").ColumnWidth = 69.43
        oExcel.Columns("D:D").ColumnWidth = 28.43
        oExcel.Columns("E:E").ColumnWidth = 23.14
        'Tamaño y tipo de Letra
        oExcel.Range(oCells(4, 1), oCells(str_Fila, 5)).Select()
        With oExcel.Selection.Font
            .Name = "Arial"
            .Size = 14
        End With
        'Cambiar la altura y Centrado el registro
        With oExcel.Rows("7:7")
            .RowHeight = 26.5
            .VerticalAlignment = 2
        End With
        oExcel.Range(oCells(7, 1), oCells(7, 2)).HorizontalAlignment = 3
        oExcel.Range(oCells(7, 4), oCells(7, 4)).HorizontalAlignment = 2
        oExcel.Range(oCells(7, 5), oCells(7, 5)).HorizontalAlignment = 3
        'oExcel.Range("E7").Activate()
        oExcel.Rows("7:7").Select()
        oExcel.Selection.Copy()
        oExcel.ActiveWindow.SmallScroll(Down:=-3)
        oExcel.Rows("8:" & str_Fila).Select()
        oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
        'Margen
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
        oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("e1")
        oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
        'Zoom
        oExcel.ActiveWindow.Zoom = 75
        'Posición
        oExcel.Range("a3").Select()
        Return str_Fila
    End Function

    'Reporte Relación de Alumno - Por Control : 1 - 2
    Public Function ExportarReportePorControl(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("Plantilla_ReporteRelacionAlumno").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteXControl(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Function LlenarPlantillaReporteXControl( _
        ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 10
        Dim columna As Integer = 1
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(1, 1), oCells(1, 16))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "RELACIÓN DE ALUMNOS"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(2, 1), oCells(2, 16))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 16
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        'Nivel, SubNivel, Grado, Aula

        oExcel.Range(oCells(4, 1), oCells(4, 16)).Merge()
        oExcel.Range(oCells(4, 1), oCells(4, 16)).HorizontalAlignment = 3
        oCells(4, 1) = "Nivel:" & ddlRep1_Nivel.SelectedItem.ToString & "                        " & _
        "SubNivel:" & ddlRep1_SubNivel.SelectedItem.ToString & "                        " & _
        "Grado:" & ddlRep1_Grado.SelectedItem.ToString & "                        " & _
        "Aula:" & ddlRep1_Aula.SelectedItem.ToString

        'Pinta Título Curso
        With oExcel.Range(oCells(7, 1), oCells(7, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Curso: ________________ "
        End With

        'Pinta Título Profesor
        With oExcel.Range(oCells(8, 1), oCells(8, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Profesor: ________________ "
        End With

        While cont_columnas <= dtReporte.Columns.Count - 15
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

        oExcel.Range(oCells(10, 1), oCells(fila - 1, columna + cont_columnas - 15)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(10, 1), oCells(fila - 1, columna + cont_columnas - 1)))

        oExcel.Range("C6:C10").Select()
        oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
        oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
        oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
        oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
        oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
        With oExcel.Selection
            .HorizontalAlignment = 3
            .VerticalAlignment = 3
            .WrapText = True
        End With

        oExcel.Selection.Merge()
        With oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
            .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            .ColorIndex = 0
            .TintAndShade = 0
        End With
        With oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
            .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            .ColorIndex = 0
            .TintAndShade = 0
        End With
        With oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
            .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            .ColorIndex = 0
            .TintAndShade = 0
        End With
        With oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
            .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            .ColorIndex = 0
            .TintAndShade = 0
        End With
        oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
        oExcel.Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
        oExcel.Selection.AutoFill(Destination:=oExcel.Range("C6:P10"), Type:=Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillDefault)
        oExcel.Range("C6:P10").Select()
        oExcel.Columns("C:P").ColumnWidth = 5
        oExcel.Columns("B:B").ColumnWidth = 54
        oExcel.Rows("11:11").Select()
        oExcel.ActiveWindow.FreezePanes = True
        'Tamaño y tipo de Letra
        oExcel.Range(oCells(4, 1), oCells(str_Fila, 16)).Select()
        With oExcel.Selection.Font
            .Name = "Arial"
            .Size = 14
        End With
        'Cambiar la altura y Centrado el registro
        With oExcel.Rows("11:11")
            .RowHeight = 26.5
            .VerticalAlignment = 2
        End With
        oExcel.Range("A11").Select()
        oExcel.Selection.HorizontalAlignment = 3
        oExcel.Rows("11:11").Select()
        oExcel.Selection.Copy()
        oExcel.ActiveWindow.SmallScroll(Down:=-3)
        oExcel.Rows("12:" & str_Fila).Select()
        oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
        'Margen
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
        oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("P1")
        oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
        oExcel.Range("a3").Select()
        oExcel.ActiveWindow.Zoom = 75
        Return str_Fila
    End Function

    'Reporte Relación de Alumno - Por Sexo : 1 - 3
    Public Function ExportarReportePorSexo(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("Plantilla_ReporteRelacionAlumno").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReportePosSexo(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Function LlenarPlantillaReportePosSexo( _
        ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 6
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(1, 1), oCells(1, 6))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Name = "Arial"
            .Font.Size = 20
            .Font.Bold = True
            .Value = "RELACIÓN DE ALUMNOS POR SEXO"
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

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        'Nivel, SubNivel, Grado, Aula
        oExcel.Range(oCells(4, 1), oCells(4, 6)).Merge()
        oExcel.Range(oCells(4, 1), oCells(4, 6)).HorizontalAlignment = 3
        oCells(4, 1) = "Nivel:" & ddlRep1_Nivel.SelectedItem.ToString & "                  " & _
        "SubNivel:" & ddlRep1_SubNivel.SelectedItem.ToString & "                  " & _
       "Grado:" & ddlRep1_Grado.SelectedItem.ToString & "                  " & _
        "Aula:" & ddlRep1_Aula.SelectedItem.ToString

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

        oExcel.Range(oCells(6, 2), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(6, 2), oCells(fila - 1, columna + cont_columnas - 1)))
        oExcel.Rows("7:7").Select()
        oExcel.ActiveWindow.FreezePanes = True
        'Ancho de la celda
        oExcel.Columns("B:B").ColumnWidth = 12.29
        oExcel.Columns("C:C").ColumnWidth = 4.86
        oExcel.Columns("D:D").ColumnWidth = 64.43
        oExcel.Columns("E:E").ColumnWidth = 21.43
        'Tamaño y tipo de Letra
        oExcel.Range(oCells(4, 2), oCells(str_Fila, 5)).Select()
        With oExcel.Selection.Font
            .Name = "Arial"
            .Size = 14
        End With
        'Cambiar la altura y Centrado el registro
        With oExcel.Rows("7:7")
            .RowHeight = 26.5
            .VerticalAlignment = 2
        End With
        oExcel.Range(oCells(7, 2), oCells(7, 3)).HorizontalAlignment = 3
        oExcel.Range(oCells(7, 5), oCells(7, 5)).HorizontalAlignment = 3
        oExcel.Rows("7:7").Select()
        oExcel.Selection.Copy()
        oExcel.ActiveWindow.SmallScroll(Down:=-3)
        oExcel.Rows("8:" & str_Fila).Select()
        oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
        'Margen
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
        oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("F1")
        oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView

        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("a3").Select()
        Return str_Fila
    End Function

    'Reporte Relación de Alumno - Por Teléfono : 1 - 5    
    Public Function ExportarReporteRelacionTelefono(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("Plantilla_ReporteRelacionAlumno").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteRelacionTelefono(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Function LlenarPlantillaReporteRelacionTelefono( _
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
        With oExcel.Range(oCells(1, 1), oCells(1, 12))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Size = 22
            .Font.Name = "Arial"
            .Value = "RELACIÓN DE ALUMNOS POR TELÉFONOS"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(2, 1), oCells(2, 12))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Size = 18
            .Font.Name = "Arial"
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        'Nivel, SubNivel, Grado, Aula
        oExcel.Range(oCells(4, 1), oCells(4, 12)).Merge()
        oExcel.Range(oCells(4, 1), oCells(4, 12)).HorizontalAlignment = 3
        oCells(4, 1) = "Nivel:" & ddlRep1_Nivel.SelectedItem.ToString & "                                                                    " & _
        "SubNivel:" & ddlRep1_SubNivel.SelectedItem.ToString & "                                                                    " & _
        "Grado:" & ddlRep1_Grado.SelectedItem.ToString & "                                                                    " & _
        "Aula:" & ddlRep1_Aula.SelectedItem.ToString

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
        'Formato Texto por los ceros
        oExcel.Range("H:H").NumberFormat = "@"
        oExcel.Range("L:L").NumberFormat = "@"
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
        'oExcel.Range(oCells(6, 1), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        'cuadradoCompleto(oExcel, oExcel.Range(oCells(6, 1), oCells(fila - 1, columna + cont_columnas - 1)))

        'Tamaño y tipo de Letra
        oExcel.Range(oCells(4, 1), oCells(str_Fila, 12)).Select()
        With oExcel.Selection.Font
            .Name = "Arial"
            .Size = 14
        End With
        'Cambiar la altura y Centrado el registro
        With oExcel.Rows("7:7")
            .RowHeight = 26.25
            .VerticalAlignment = 2
        End With
        'El ancho de la celda
        'oExcel.Columns("A:A").ColumnWidth = 3.29
        'oExcel.Columns("B:B").ColumnWidth = 55.43
        'oExcel.Columns("C:C").ColumnWidth = 27.43
        'oExcel.Columns("D:D").ColumnWidth = 55.43
        'oExcel.Columns("E:E").ColumnWidth = 27.43
        'oExcel.Columns("F:F").ColumnWidth = 15.14
        'oExcel.Columns("G:G").ColumnWidth = 55.43
        'oExcel.Columns("H:H").ColumnWidth = 27.43
        'oExcel.Columns("I:I").ColumnWidth = 15.14
        'Medio Vertical
        oExcel.Range(oCells(7, 2), oCells(str_Fila, 12)).Select()
        oExcel.Range(oCells(7, 1), oCells(str_Fila, 12)).VerticalAlignment = 2
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
        oExcel.Range("D:D").HorizontalAlignment = 1
        oExcel.Range("G:G").HorizontalAlignment = 1
        oExcel.Range("K:K").HorizontalAlignment = 1
        oExcel.Range(oCells(6, 4), oCells(6, 4)).HorizontalAlignment = 3
        oExcel.Range(oCells(6, 7), oCells(6, 7)).HorizontalAlignment = 3
        oExcel.Range(oCells(6, 11), oCells(6, 11)).HorizontalAlignment = 3
        oExcel.Rows("7:7").Select()
        oExcel.Selection.Copy()
        oExcel.ActiveWindow.SmallScroll(Down:=-3)
        oExcel.Rows("8:" & str_Fila).Select()
        oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)


        oExcel.Rows("7:7").Select()
        oExcel.ActiveWindow.FreezePanes = True
        oExcel.Range(oCells(6, 1), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(6, 1), oCells(fila - 1, columna + cont_columnas - 1)))
        'Margen Horizontal
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
        oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("l1")
        oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("a3").Select()
        Return str_Fila
    End Function

    'Reporte Relación de Alumno - Firma Padre : 1 - 6
    Public Function ExportarReporteFirmaPadre(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("Plantilla_ReporteRelacionAlumno").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteFirmaPadre(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Function LlenarPlantillaReporteFirmaPadre( _
        ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 6
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(1, 1), oCells(1, 7))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "RELACIÓN DE ALUMNOS - FIRMA DE LOS PADRES"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(2, 1), oCells(2, 7))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 16
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        'Nivel, SubNivel, Grado, Aula
        oExcel.Range(oCells(4, 1), oCells(4, 7)).Merge()
        oExcel.Range(oCells(4, 1), oCells(4, 7)).HorizontalAlignment = 3
        oCells(4, 1) = "Nivel:" & ddlRep1_Nivel.SelectedItem.ToString & "                              " & _
        "SubNivel:" & ddlRep1_SubNivel.SelectedItem.ToString & "                              " & _
       "Grado:" & ddlRep1_Grado.SelectedItem.ToString & "                              " & _
        "Aula:" & ddlRep1_Aula.SelectedItem.ToString

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

        oExcel.Range(oCells(6, 2), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(6, 2), oCells(fila - 1, columna + cont_columnas - 1)))
        'El ancho de la celda
        oExcel.Columns("B:B").ColumnWidth = 3.71
        oExcel.Columns("C:C").ColumnWidth = 16.86
        oExcel.Columns("D:D").ColumnWidth = 55.43
        oExcel.Columns("E:E").ColumnWidth = 29.14
        oExcel.Columns("F:F").ColumnWidth = 29.14

        'Tamaño y tipo de Letra
        oExcel.Range(oCells(4, 1), oCells(str_Fila, 9)).Select()
        With oExcel.Selection.Font
            .Name = "Arial"
            .Size = 14
        End With
        'Cambiar la altura y Centrado el registro
        With oExcel.Rows("7:7")
            .RowHeight = 40.25
            .VerticalAlignment = 2
        End With
        'Medio Vertical
        oExcel.Range(oCells(7, 2), oCells(str_Fila, 12)).Select()
        oExcel.Range(oCells(7, 1), oCells(str_Fila, 12)).VerticalAlignment = 2
        oExcel.Range("B:C").HorizontalAlignment = 3
        'Copy el formato a todas las celdas
        oExcel.Rows("7:7").Select()
        oExcel.Selection.Copy()
        oExcel.ActiveWindow.SmallScroll(Down:=-3)
        oExcel.Rows("8:" & str_Fila).Select()
        oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)


        oExcel.Rows("7:7").Select()
        oExcel.ActiveWindow.FreezePanes = True
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
        oExcel.ActiveWindow.SmallScroll(Down:=-6)
        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("A3").Select()
        Return str_Fila
    End Function

    'Reporte Relación de Alumno - Cumpleaños por mes : 1 - 7
    Public Function ExportarReporteCumpleaniosXMes(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("Plantilla_ReporteRelacionAlumno").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteCumpleaniosXMes(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Function LlenarPlantillaReporteCumpleaniosXMes( _
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
            .Value = "RELACIÓN DE ALUMNOS CUMPLEAÑOS"
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

        'Nivel, SubNivel, Grado, Aula
        If ddlRep1_Nivel1.SelectedValue <> 0 And ddlRep1_Grado1.SelectedValue <> 0 And ddlRep1_Aula1.SelectedValue <> 0 Then
            'Nivel, SubNivel, Grado, Aula
            oExcel.Range(oCells(4, 1), oCells(4, 6)).Merge()
            oExcel.Range(oCells(4, 1), oCells(4, 6)).HorizontalAlignment = 3
            oCells(4, 1) = "Nivel:" & ddlRep1_Nivel1.SelectedItem.ToString & "                              " & _
            "SubNivel:" & ddlRep1_SubNivel1.SelectedItem.ToString & "                              " & _
           "Grado:" & ddlRep1_Grado1.SelectedItem.ToString & "                              " & _
            "Aula:" & ddlRep1_Aula1.SelectedItem.ToString
        ElseIf ddlRep1_Nivel1.SelectedValue <> 0 And ddlRep1_Grado1.SelectedValue = 0 Then
            oCells(4, 2) = "Nivel: " & ddlRep1_Nivel1.SelectedItem.ToString
            oExcel.Range(oCells(4, 1), oCells(4, 4)).Merge()
        ElseIf ddlRep1_Nivel1.SelectedValue <> 0 And ddlRep1_Grado1.SelectedValue <> 0 And ddlRep1_Aula1.SelectedValue = 0 Then
            oExcel.Range(oCells(4, 1), oCells(4, 6)).Merge()
            oExcel.Range(oCells(4, 1), oCells(4, 6)).HorizontalAlignment = 3
            oCells(4, 1) = "Nivel:" & ddlRep1_Nivel1.SelectedItem.ToString & "                              " & _
            "SubNivel:" & ddlRep1_SubNivel1.SelectedItem.ToString & "                              " & _
           "Grado:" & ddlRep1_Grado1.SelectedItem.ToString
        ElseIf ddlRep1_Nivel1.SelectedValue = 0 Then
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
        If ddlRep1_Nivel1.SelectedValue = 0 Then
            Valfila = 4
        Else
            Valfila = 6
        End If
        oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(Valfila, 1), oCells(fila - 1, columna + cont_columnas - 1)))

        oExcel.Range(oCells(4, 1), oCells(str_Fila, 6)).Select()
        'Tamaño y tipo de Letra
        With oExcel.Selection.Font
            .Name = "Arial"
            .Size = "14"
        End With

        If ddlRep1_Nivel1.SelectedValue = 0 Then
            'Cambiar la altura y Centrado el registro
            With oExcel.Rows("5:5")
                .RowHeight = 26.25
                .VerticalAlignment = 2
            End With
            oExcel.Range(oCells(5, 1), oCells(str_Fila, 2)).HorizontalAlignment = 3
            oExcel.Range(oCells(5, 4), oCells(str_Fila, 5)).HorizontalAlignment = 3
            oExcel.Rows("5:5").Select()
        Else
            'Cambiar la altura y Centrado el registro
            With oExcel.Rows("7:7")
                .RowHeight = 26.25
                .VerticalAlignment = 2
            End With
            oExcel.Range(oCells(7, 1), oCells(str_Fila, 2)).HorizontalAlignment = 3
            oExcel.Rows("7:7").Select()
        End If
        oExcel.ActiveWindow.FreezePanes = True
        'El ancho de la celda
        oExcel.Columns("A:A").ColumnWidth = 13.43
        oExcel.Columns("B:B").ColumnWidth = 16.86
        oExcel.Columns("C:C").ColumnWidth = 86
        oExcel.Columns("D:D").ColumnWidth = 18.71
        oExcel.Columns("E:E").ColumnWidth = 8.29
        oExcel.Columns("F:F").ColumnWidth = 30

        If ddlRep1_Nivel1.SelectedValue = 0 Then
            'Copy el formato a todas las celdas
            oExcel.Rows("5:5").Select()
            oExcel.Selection.Copy()
            oExcel.ActiveWindow.SmallScroll(Down:=-3)
            oExcel.Rows("6:" & str_Fila).Select()
            oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
            'Margen
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
            oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
            oExcel.Rows("7:7").Select()
            oExcel.ActiveWindow.FreezePanes = True
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
            oExcel.ActiveWindow.SmallScroll(Down:=-6)
        Else
            'Copy el formato a todas las celdas
            oExcel.Rows("7:7").Select()
            oExcel.Selection.Copy()
            oExcel.ActiveWindow.SmallScroll(Down:=-3)
            oExcel.Rows("8:" & str_Fila).Select()
            oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)
            'Margen
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
            oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
            oExcel.Rows("7:7").Select()
            oExcel.ActiveWindow.FreezePanes = True
            oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
            oExcel.ActiveWindow.SmallScroll(Down:=-6)
            oExcel.Range("D:D").HorizontalAlignment = 2
            oExcel.Range("E:E").HorizontalAlignment = 3
            'If ddlMes.SelectedValue = 0 And ddlRep1_Aula1.SelectedValue <> 0 Then
            '    oExcel.Columns("A:A").ColumnWidth = 3.71
            '    oExcel.Columns("B:B").ColumnWidth = 16.86
            '    oExcel.Columns("C:C").ColumnWidth = 115.86
            '    oExcel.Columns("D:D").ColumnWidth = 18.71
            '    oExcel.Columns("E:E").ColumnWidth = 8.29
            '    oExcel.Columns("F:F").ColumnWidth = 7.29
            '    oExcel.Range("D:D").HorizontalAlignment = 2
            '    oExcel.Range("E:F").HorizontalAlignment = 3
            'End If
        End If
        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("a3").Select()
        Return str_Fila
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

    ' Valores 1-52
    Private Shared Function DevLetraColumna(ByVal idColumna As Integer) As String
        Dim letra As String = ""

        If idColumna = 1 Then
            letra = "A"
        ElseIf idColumna = 2 Then
            letra = "B"
        ElseIf idColumna = 3 Then
            letra = "C"
        ElseIf idColumna = 4 Then
            letra = "D"
        ElseIf idColumna = 5 Then
            letra = "E"
        ElseIf idColumna = 6 Then
            letra = "F"
        ElseIf idColumna = 7 Then
            letra = "G"
        ElseIf idColumna = 8 Then
            letra = "H"
        ElseIf idColumna = 9 Then
            letra = "I"
        ElseIf idColumna = 10 Then
            letra = "J"
        ElseIf idColumna = 11 Then
            letra = "K"
        ElseIf idColumna = 12 Then
            letra = "L"
        ElseIf idColumna = 13 Then
            letra = "M"
        ElseIf idColumna = 14 Then
            letra = "N"
        ElseIf idColumna = 15 Then
            letra = "O"
        ElseIf idColumna = 16 Then
            letra = "P"
        ElseIf idColumna = 17 Then
            letra = "Q"
        ElseIf idColumna = 18 Then
            letra = "R"
        ElseIf idColumna = 19 Then
            letra = "S"
        ElseIf idColumna = 20 Then
            letra = "T"
        ElseIf idColumna = 21 Then
            letra = "U"
        ElseIf idColumna = 22 Then
            letra = "V"
        ElseIf idColumna = 23 Then
            letra = "W"
        ElseIf idColumna = 24 Then
            letra = "X"
        ElseIf idColumna = 25 Then
            letra = "Y"
        ElseIf idColumna = 26 Then
            letra = "Z"

        ElseIf idColumna = 27 Then
            letra = "AA"
        ElseIf idColumna = 28 Then
            letra = "AB"
        ElseIf idColumna = 29 Then
            letra = "AC"
        ElseIf idColumna = 30 Then
            letra = "AD"
        ElseIf idColumna = 31 Then
            letra = "AE"
        ElseIf idColumna = 32 Then
            letra = "AF"
        ElseIf idColumna = 33 Then
            letra = "AG"
        ElseIf idColumna = 34 Then
            letra = "AH"
        ElseIf idColumna = 35 Then
            letra = "AI"
        ElseIf idColumna = 36 Then
            letra = "AJ"
        ElseIf idColumna = 37 Then
            letra = "AK"
        ElseIf idColumna = 38 Then
            letra = "AL"
        ElseIf idColumna = 39 Then
            letra = "AM"
        ElseIf idColumna = 40 Then
            letra = "AN"
        ElseIf idColumna = 41 Then
            letra = "AO"
        ElseIf idColumna = 42 Then
            letra = "AP"
        ElseIf idColumna = 43 Then
            letra = "AQ"
        ElseIf idColumna = 44 Then
            letra = "AR"
        ElseIf idColumna = 44 Then
            letra = "AS"
        ElseIf idColumna = 46 Then
            letra = "AT"
        ElseIf idColumna = 47 Then
            letra = "AU"
        ElseIf idColumna = 48 Then
            letra = "AV"
        ElseIf idColumna = 49 Then
            letra = "AW"
        ElseIf idColumna = 50 Then
            letra = "AX"
        ElseIf idColumna = 51 Then
            letra = "AY"
        ElseIf idColumna = 52 Then
            letra = "AZ"
        End If

        Return letra
    End Function

    ' Valores 1-26
    Private Shared Function DevIDColumna(ByVal strLetra As String) As Integer
        Dim idx As Integer = 0

        If strLetra = "A" Then
            idx = 1
        ElseIf strLetra = "B" Then
            idx = 2
        ElseIf strLetra = "C" Then
            idx = 3
        ElseIf strLetra = "D" Then
            idx = 4
        ElseIf strLetra = "E" Then
            idx = 5
        ElseIf strLetra = "F" Then
            idx = 6
        ElseIf strLetra = "G" Then
            idx = 7
        ElseIf strLetra = "H" Then
            idx = 8
        ElseIf strLetra = "I" Then
            idx = 9
        ElseIf strLetra = "J" Then
            idx = 10
        ElseIf strLetra = "K" Then
            idx = 11
        ElseIf strLetra = "L" Then
            idx = 12
        ElseIf strLetra = "M" Then
            idx = 13
        ElseIf strLetra = "N" Then
            idx = 14
        ElseIf strLetra = "O" Then
            idx = 15
        ElseIf strLetra = "P" Then
            idx = 16
        ElseIf strLetra = "Q" Then
            idx = 17
        ElseIf strLetra = "R" Then
            idx = 18
        ElseIf strLetra = "S" Then
            idx = 19
        ElseIf strLetra = "T" Then
            idx = 20
        ElseIf strLetra = "U" Then
            idx = 21
        ElseIf strLetra = "V" Then
            idx = 22
        ElseIf strLetra = "W" Then
            idx = 23
        ElseIf strLetra = "X" Then
            idx = 24
        ElseIf strLetra = "Y" Then
            idx = 25
        ElseIf strLetra = "Z" Then
            idx = 26
        End If

        Return idx
    End Function

#End Region

#End Region

    Public Class alumnos
        Public nombre As String
        Public codigo As String
        Public lstNotas As New List(Of notasConsolidado)

    End Class
    Public Class notasConsolidado
        Public notaPromedio As String
        Public pos As Integer
        Public codAlumno As Integer

    End Class
    Private Function crearListaPersonas(ByVal dt As System.Data.DataTable) As List(Of alumnos)
        Dim listasAlumno As New List(Of alumnos)
        Dim oalumnos As alumnos
        'nombres	RNBL_NotaFinalBimestre	curso	codCurso	pos	AL_CodigoAlumno
        Dim codaL As Integer
        Dim onotasConsolidado As notasConsolidado

        For Each fil As System.Data.DataRow In dt.Rows
            oalumnos = New alumnos
            oalumnos.codigo = Convert.ToInt32(fil("AL_CodigoAlumno").ToString())
            oalumnos.nombre = fil("nombres").ToString()

            If oalumnos.codigo <> codaL Then
                For Each fi As System.Data.DataRow In dt.Rows
                    onotasConsolidado = New notasConsolidado
                    onotasConsolidado.codAlumno = Convert.ToInt32(fi("AL_CodigoAlumno").ToString())
                    onotasConsolidado.notaPromedio = fi("RNBL_NotaFinalBimestre").ToString()
                    onotasConsolidado.pos = Convert.ToInt32(fi("pos").ToString())
                    If onotasConsolidado.codAlumno = oalumnos.codigo Then
                        oalumnos.lstNotas.Add(onotasConsolidado)
                    End If
                Next

                listasAlumno.Add(oalumnos)
            End If
            codaL = oalumnos.codigo

        Next
        Return listasAlumno

    End Function


    Protected Sub dpsSalonDescriptores_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpsSalonDescriptores.SelectedIndexChanged
        cargarComboAsignacionCursos()
    End Sub

    Protected Sub cmbSalonPrimariaInicial_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSalonPrimariaInicial.SelectedIndexChanged
        cargarComboAsignacionCursosReporteRevision()
    End Sub

    Private Sub cargarComboAsignacionCursosReporteRevision()

        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 
        Dim int_CodigoTrabajador As Integer = int_CodigoUsuario
        Dim int_CodigoAsignacionAula As Integer = cmbSalonPrimariaInicial.SelectedValue
        Dim obj_BL_AsignacionCursos As New bl_AsignacionCursos

        ''FUN_LIS_AsignacionCursosPorAsgignacionAula
        Dim ds_Lista As DataSet = obj_BL_AsignacionCursos.FUN_LIS_AsignacionCursosPorAsgignacionAula(int_CodigoAsignacionAula, 0, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(cmbCurso, ds_Lista, "CodigoCurso", "DescCursoCompuesta", False, False)

    End Sub
    Function crearListaPersona(ByVal dtPersona As System.Data.DataTable, ByVal dtPosiciones As System.Data.DataTable) As List(Of personaComentario)
        Dim lstPersonaCoemntario As New List(Of personaComentario)
        Dim oPersonaComentario As personaComentario
        Dim ocomentarioCurso As comentarioCurso
        Dim sqlBusqueda As System.Data.DataRow()
        Try

            For Each filasPersonaComentario As System.Data.DataRow In dtPersona.Rows
                oPersonaComentario = New personaComentario
                oPersonaComentario.nombrePersona = filasPersonaComentario("nombreAlumno").ToString()
                sqlBusqueda = dtPosiciones.Select("AL_CodigoAlumno =" & filasPersonaComentario("AL_CodigoAlumno").ToString())
                For Each filasBusqueda In sqlBusqueda
                    ocomentarioCurso = New comentarioCurso
                    ocomentarioCurso.comentario = filasBusqueda("RNBL_ObservacionCurso").ToString()
                    ocomentarioCurso.pocision = CInt(filasBusqueda("ub").ToString())
                    oPersonaComentario.lstComentario.Add(ocomentarioCurso)
                Next


                lstPersonaCoemntario.Add(oPersonaComentario)
                'AL_CodigoAlumno(nombreAlumno)
                '20080002	CESPEDES CASTRO Andrea
            Next

            Return lstPersonaCoemntario
        Catch ex As Exception
        Finally

        End Try
    End Function
    Function crearListaCurso(ByVal dtCurso As System.Data.DataTable) As List(Of cursoComentario)
        Dim ocursoComentario As cursoComentario
        Dim lstcursoComentario As New List(Of cursoComentario)
        Try
            For Each filasCursoCementario As System.Data.DataRow In dtCurso.Rows
                ocursoComentario = New cursoComentario
                ocursoComentario.nombreCurso = filasCursoCementario("nombreCurso").ToString()
                lstcursoComentario.Add(ocursoComentario)
            Next
            Return lstcursoComentario
        Catch ex As Exception
        Finally

        End Try
    End Function
    Public Class personaComentario
        Public nombrePersona As String
        Public lstComentario As New List(Of comentarioCurso)
    End Class
    Public Class comentarioCurso
        Public comentario As String
        Public pocision As Integer

    End Class
    Public Class cursoComentario
        Public nombreCurso As String
    End Class
    Public Class colores
        Public R As Integer
        Public G As Integer
        Public B As Integer
    End Class
    'funcion para crear reporte de jalados '
    Function crearReporteJalados(ByRef nombreArchivo As String) As Byte()

        Dim sumas1 As Integer = 0
        Dim sumas2 As Integer = 0
        Dim sumas3 As Integer = 0
        Dim sumas4 As Integer = 0



        Dim lstaColores As New List(Of colores)
        Dim ocolores As New colores
        ocolores.R = 146 : ocolores.G = 205 : ocolores.B = 220
        lstaColores.Add(ocolores)
        Dim ocolores1 As New colores
        ocolores1.R = 204 : ocolores1.G = 192 : ocolores1.B = 218
        lstaColores.Add(ocolores1)
        Dim ocolores2 As New colores
        ocolores2.R = 197 : ocolores2.G = 123 : ocolores2.B = 241
        lstaColores.Add(ocolores2)
        Dim ocolores3 As New colores
        ocolores3.R = 123 : ocolores3.G = 217 : ocolores3.B = 123
        lstaColores.Add(ocolores3)
        Dim ocolores4 As New colores
        ocolores4.R = 228 : ocolores4.G = 223 : ocolores4.B = 236
        lstaColores.Add(ocolores4)
        Dim ocolores5 As New colores
        ocolores5.R = 197 : ocolores5.G = 217 : ocolores5.B = 241

        lstaColores.Add(ocolores5)
        lstaColores.Add(ocolores4)
        lstaColores.Add(ocolores3)
        lstaColores.Add(ocolores2)
        lstaColores.Add(ocolores1)
        lstaColores.Add(ocolores5)
        lstaColores.Add(ocolores4)
        lstaColores.Add(ocolores3)
        lstaColores.Add(ocolores1)
        lstaColores.Add(ocolores5)
        lstaColores.Add(ocolores4)
        lstaColores.Add(ocolores3)



        'Dim ocolores6 As New colores
        'ocolores6.R = 228 : ocolores6.G = 223 : ocolores6.B = 236

        'Dim ocolores7 As New colores
        'ocolores7.R = 123 : ocolores7.G = 123 : ocolores7.B = 123

        'Dim ocolores8 As New colores
        'ocolores8.R = 123 : ocolores8.G = 123 : ocolores8.B = 123

        'Dim ocolores9 As New colores
        'ocolores9.R = 123 : ocolores9.G = 123 : ocolores9.B = 123

        'Dim ocolores10 As New colores
        'ocolores10.R = 123 : ocolores10.G = 123 : ocolores10.B = 123

        'Dim ocolores11 As New colores
        'ocolores11.R = 123 : ocolores11.G = 123 : ocolores11.B = 123


        Try
            Dim downloadBytes As Byte()

            Dim dstJalados As New DataSet
            Dim obl_Reportes As New bl_Reportes
            Dim dtCursos As New System.Data.DataTable
            Dim dt As New System.Data.DataTable
            dstJalados = obl_Reportes.FunListarJalados()

          


            Dim dtSalones As New System.Data.DataTable
            dtSalones = dstJalados.Tables(0)

            Dim dtAlumno As New System.Data.DataTable
            dtAlumno = dstJalados.Tables(4)

            Dim dtNotas As New System.Data.DataTable
            dtNotas = dstJalados.Tables(3)

            Dim lstcontexto As New contexto

            lstcontexto = crearDetalle(dtSalones, dtAlumno, dtNotas)

            Dim workbook As New XLWorkbook()

            Dim ws = workbook.Worksheets.Add("Prueba")


            With ws.Range(ws.Cell(2, 1), ws.Cell(dtAlumno.Rows.Count + 3, (dstJalados.Tables(1).Rows.Count * 4) + 7))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin

            End With

            Dim contadorCOlumnas As Integer = 2
            ws.Cell(2, 1).Value = "Grado"
            ws.Cell(2, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#FF6500")
            ws.Cell(2, 2).Value = "Student"
            ws.Cell(2, 2).Style.Fill.BackgroundColor = XLColor.FromHtml("#FF6500")

            ws.Cell(2, (dstJalados.Tables(1).Rows.Count * 4) + 3).Value = "I"
            ws.Cell(2, (dstJalados.Tables(1).Rows.Count * 4) + 3).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
            ws.Cell(2, (dstJalados.Tables(1).Rows.Count * 4) + 4).Value = "II"
            ws.Cell(2, (dstJalados.Tables(1).Rows.Count * 4) + 4).Style.Fill.BackgroundColor = XLColor.FromHtml("#00FF00")
            ws.Cell(2, (dstJalados.Tables(1).Rows.Count * 4) + 5).Value = "III"
            ws.Cell(2, (dstJalados.Tables(1).Rows.Count * 4) + 5).Style.Fill.BackgroundColor = XLColor.FromHtml("#FF0000")

            ws.Cell(2, (dstJalados.Tables(1).Rows.Count * 4) + 6).Value = "IV"
            ws.Cell(2, (dstJalados.Tables(1).Rows.Count * 4) + 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#59CBF5")

            ws.Cell(2, (dstJalados.Tables(1).Rows.Count * 4) + 7).Value = ""
            ws.Cell(2, (dstJalados.Tables(1).Rows.Count * 4) + 7).Style.Fill.BackgroundColor = XLColor.FromHtml("#59CBF5")

            With ws.Cell(2, 1)
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.FontSize = 10
            End With

            With ws.Cell(2, 2)
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.FontSize = 10
            End With
            Dim contador As Integer = 0
            Dim columnasContador As Integer = 3
            Dim limInf As Integer = 0
            Dim limSup As Integer = 0
            ws.Rows(2).Height = 85
            For Each filas As DataRow In dstJalados.Tables(1).Rows
                contadorCOlumnas += 1



                contador += 1
                If contador = 1 Then
                    limInf = columnasContador
                    limSup = (limInf + 4) - 1
                End If
                If contador > 1 Then
                    limInf += 4
                    limSup = (limInf + 4) - 1
                End If


                With ws.Range(ws.Cell(2, limInf), ws.Cell(2, limSup))
                    .Merge()
                    .Value = filas("NC_Descripcion").ToString().Substring(0, IIf(filas("NC_Descripcion").ToString().Length > 5, 5, 3))
                    .Style.Alignment.TextRotation = 90
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#FF6500")

                End With

                With ws.Range(ws.Cell(3, limInf), ws.Cell(dtAlumno.Rows.Count + 2, limSup))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin

                    .Style.Border.RightBorderColor = XLColor.FromHtml("#DFE2E6")
                    .Style.Border.TopBorderColor = XLColor.FromHtml("#DFE2E6")
                    .Style.Border.BottomBorderColor = XLColor.FromHtml("#DFE2E6")
                    .Style.Border.LeftBorderColor = XLColor.FromHtml("#DFE2E6")

                End With

                With ws.Range(ws.Cell(3, limInf), ws.Cell(dtAlumno.Rows.Count + 2, limInf))
                    .Style.Border.LeftBorderColor = XLColor.FromHtml("#000000")
                End With
                With ws.Range(ws.Cell(3, limSup), ws.Cell(dtAlumno.Rows.Count + 2, limSup))
                    .Style.Border.RightBorderColor = XLColor.FromHtml("#000000")
                End With

            Next

            contador = 0
            columnasContador = 3
            limInf = 0
            limSup = 0

            Dim filasAlumnos As Integer = 2

            Dim filasLimI As Integer = 0
            Dim filasLimS As Integer = 0
            Dim temp As Integer = 3
            Dim catidadJalados1 As Integer = 0
            Dim catidadJalados2 As Integer = 0
            Dim catidadJalados3 As Integer = 0
            Dim catidadJalados4 As Integer = 0

            For indice As Integer = 0 To lstcontexto.listaSalones.Count - 1
                ''
                temp += lstcontexto.listaSalones(indice).listaAlumnos.Count
                contador += 1
                If contador = 1 Then


                    filasLimI = 3
                    filasLimS = temp - 1

                End If
                If contador > 1 Then

                    filasLimS = temp - 1
                    filasLimI = temp - lstcontexto.listaSalones(indice).listaAlumnos.Count
                End If

                With ws.Range(ws.Cell(filasLimI, 1), ws.Cell(filasLimS, 1))
                    .Merge()
                    .Value = lstcontexto.listaSalones(indice).nobreSalon
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(filasLimI, 2), ws.Cell(filasLimS, (dstJalados.Tables(1).Rows.Count * 4) + 2))
                    '.Merge()
                    '.Style.Fill.BackgroundColor = XLColor.FromHtml("#C0EAFF")

                    .Style.Fill.BackgroundColor = IIf(indice Mod 2 = 0, XLColor.FromArgb(lstaColores(0).R, lstaColores(0).G, lstaColores(0).B), XLColor.FromArgb(lstaColores(1).R, lstaColores(1).G, lstaColores(1).B))

                    ' Dim rnd As Integer = New Random().Next(10, 50)
                    '.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    '.Style.Font.FontSize = 10
                End With

                For Each oalumnoJalados As alumnoJalados In lstcontexto.listaSalones(indice).listaAlumnos
                    catidadJalados1 = 0
                    catidadJalados2 = 0
                    catidadJalados3 = 0
                    catidadJalados4 = 0
                    filasAlumnos += 1
                    For Each onotasAlumno As notasAlumno In (From h In oalumnoJalados.ltNotas Distinct Select h).ToList()
                        ws.Cell(filasAlumnos, onotasAlumno.pos).Value = onotasAlumno.nota

                        ws.Cell(filasAlumnos, onotasAlumno.pos).Style.Font.FontSize = 8

                        'ws.Cell(ro, co).Style.Fill.PatternType = XLFillPatternValues.DarkTrellis;
                        'ws.Cell(ro, co).Style.Fill.PatternColor = XLColor.Orange;
                        'ws.Cell(ro, co).Style.Fill.PatternBackgroundColor = XLColor.Blue;
                        If onotasAlumno.codBimestre = 1 Then
                            ws.Cell(filasAlumnos, onotasAlumno.pos).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
                        End If
                        If onotasAlumno.codBimestre = 2 Then
                            ws.Cell(filasAlumnos, onotasAlumno.pos).Style.Fill.BackgroundColor = XLColor.FromHtml("#00FF00")
                        End If

                        If onotasAlumno.codBimestre = 3 Then '
                            ws.Cell(filasAlumnos, onotasAlumno.pos).Style.Fill.BackgroundColor = XLColor.FromHtml("#FF0000")
                        End If

                        If onotasAlumno.codBimestre = 4 Then
                            ws.Cell(filasAlumnos, onotasAlumno.pos).Style.Fill.BackgroundColor = XLColor.FromHtml("#59CBF5")
                        End If



                        ws.Cell(filasAlumnos, onotasAlumno.pos).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        If onotasAlumno.codBimestre = 1 Then
                            catidadJalados1 += 1
                        End If
                        If onotasAlumno.codBimestre = 2 Then
                            catidadJalados2 += 1
                        End If
                        If onotasAlumno.codBimestre = 3 Then
                            catidadJalados3 += 1
                        End If
                        If onotasAlumno.codBimestre = 4 Then
                            catidadJalados4 += 1
                        End If
                    Next

                    With ws.Cell(filasAlumnos, 2)
                        .Value = oalumnoJalados.nombre
                        .Style.Font.FontSize = 8
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 3).Value = IIf(catidadJalados1 = 0, "", catidadJalados1 / 2)
                        ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 4).Value = IIf(catidadJalados2 = 0, "", catidadJalados2 / 2)
                        ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 5).Value = IIf(catidadJalados3 = 0, "", catidadJalados3 / 2)
                        ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 6).Value = IIf(catidadJalados4 = 0, "", catidadJalados4 / 2)



                        ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 3).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
                        ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 4).Style.Fill.BackgroundColor = XLColor.FromHtml("#00FF00")
                        ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 5).Style.Fill.BackgroundColor = XLColor.FromHtml("#FF0000")
                        ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#59CBF5")

                        sumas1 += catidadJalados1 / 2
                        sumas2 += catidadJalados2 / 2
                        sumas3 += catidadJalados3 / 2
                        sumas4 += catidadJalados4 / 2


                        If (catidadJalados1 / 2 + catidadJalados2 / 2 + catidadJalados3 / 2 + catidadJalados4 / 2) > 3 Then
                            ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 7).Style.Fill.BackgroundColor = XLColor.FromHtml("#59CBF5")
                            ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 7).Value = "HOL"
                        End If

                        If (catidadJalados1 / 2 + catidadJalados2 / 2 + catidadJalados3 / 2 + catidadJalados4 / 2) <= 3 And (catidadJalados1 / 2 + catidadJalados2 / 2 + catidadJalados3 / 2 + catidadJalados4 / 2) >= 1 Then
                            ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 7).Style.Fill.BackgroundColor = XLColor.FromHtml("#59CBF5")
                            ws.Cell(filasAlumnos, (dstJalados.Tables(1).Rows.Count * 4) + 7).Value = "PEV"
                        End If




                    End With
                Next
            Next

            ws.Column(2).Width = 30
            For indice = 3 To (dstJalados.Tables(1).Rows.Count * 4) + 6
                ws.Column(indice).Width = 2
            Next

            

            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"

            With ws.Range(ws.Cell(dtAlumno.Rows.Count + 2, (dstJalados.Tables(1).Rows.Count * 4) + 6), ws.Cell(dtAlumno.Rows.Count + 2, (dstJalados.Tables(1).Rows.Count * 4) + 6))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorderColor = XLColor.FromHtml("#000000")

            End With
            ws.Cell(filasAlumnos + 1, 1).Value = "Totales"
            ws.Cell(filasAlumnos + 1, (dstJalados.Tables(1).Rows.Count * 4) + 3).Value = sumas1
            ' ws.Cell(filasAlumnos + 1, (dstJalados.Tables(1).Rows.Count * 4) + 3).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
            ws.Cell(filasAlumnos + 1, (dstJalados.Tables(1).Rows.Count * 4) + 4).Value = sumas2
            'ws.Cell(filasAlumnos + 1, (dstJalados.Tables(1).Rows.Count * 4) + 4).Style.Fill.BackgroundColor = XLColor.FromHtml("#00FF00")
            ws.Cell(filasAlumnos + 1, (dstJalados.Tables(1).Rows.Count * 4) + 5).Value = sumas3
            'ws.Cell(filasAlumnos + 1, (dstJalados.Tables(1).Rows.Count * 4) + 5).Style.Fill.BackgroundColor = XLColor.FromHtml("#FF0000")

            ws.Cell(filasAlumnos + 1, (dstJalados.Tables(1).Rows.Count * 4) + 6).Value = sumas4
            'ws.Cell(filasAlumnos + 1, (dstJalados.Tables(1).Rows.Count * 4) + 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#59CBF5")


            '


            With ws.Range(ws.Cell(filasAlumnos + 1, 1), ws.Cell(filasAlumnos + 1, (dstJalados.Tables(1).Rows.Count * 4) + 7))
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
            End With

            ' With ws.Range(ws.Cell(2, 1), ws.Cell(dtAlumno.Rows.Count + 3, (dstJalados.Tables(1).Rows.Count * 4) + 7))
           
            '  Dim ws = workbook.Worksheets.Add("Prueba")

            Dim wsFreeze = workbook.Worksheets.Add("Freeze View")

            ' // Freeze rows and columns in one shot
            ' wsFreeze.SheetView.Freeze(3, 3);

            ' // You can also be more specific on what you want to freeze
            ' // For example:
            wsFreeze.SheetView.FreezeRows(2)


            'wsFreeze.SheetView.FreezeRows(3);



            workbook.SaveAs(rutaREpositorioTemporales)
            downloadBytes = File.ReadAllBytes(rutaREpositorioTemporales)
            nombreArchivo = rutaREpositorioTemporales
            Return downloadBytes
        Catch ex As Exception

        End Try

    End Function
    ''
    'Utilidades de reporte de jalados'
#Region "Clases de reporte jalados "
    Public Class contexto
        Public listaSalones As New List(Of salon)
    End Class

    Public Class salon
        Public codSalon As Integer
        Public nobreSalon As String
        Public listaAlumnos As New List(Of alumnoJalados)
    End Class
    Public Class alumnoJalados
        Public nombre As String
        Public codAlumno As Integer
        Public codSalon As Integer
        Public ltNotas As New List(Of notasAlumno)

        Public jaladosI As Integer
        Public jaladosII As Integer
        Public jaladosIII As Integer
        Public jaladosIV As Integer

    End Class
    Public Class notasAlumno
        Public nota As String
        Public pos As Integer
        Public codAlumno As Integer
        Public codBimestre As Integer

    End Class
#End Region
#Region "funciones para crear coleccionaes de reporte jalados "
    Function crearListaSalones(ByVal dtAulas As System.Data.DataTable) As List(Of salon)
        Dim lstsalon As New List(Of salon)
        Dim osalon As salon
        Try
            For Each filas As DataRow In dtAulas.Rows
                osalon = New salon
                osalon.codSalon = CInt(filas("AU_CodigoAula").ToString())
                osalon.nobreSalon = filas("AU_Descripcion").ToString()
                lstsalon.Add(osalon)
            Next
            Return lstsalon
            ''pos	CS_CodigoCurso	BM_CodigoBimestre	RNBT_NotaFinalBimestre	GD_CodigoGrado	nombre	AU_CodigoAula	AU_Descripcion	AL_CodigoAlumno
        Catch ex As Exception
        Finally

        End Try
    End Function
    Public Function crearListaNotaAlumno(ByVal dtNotas As System.Data.DataTable) As List(Of notasAlumno)
        Dim ltNotas As New List(Of notasAlumno)
        Dim onotasAlumno As notasAlumno
        Try
            For Each filas As DataRow In dtNotas.Rows
                onotasAlumno = New notasAlumno
                onotasAlumno.codAlumno = CInt(filas("AL_CodigoAlumno").ToString())
                onotasAlumno.pos = CInt(filas("pos").ToString())
                onotasAlumno.nota = filas("RNBT_NotaFinalBimestre").ToString()
                onotasAlumno.codBimestre = CInt(filas("BM_CodigoBimestre").ToString())
                ltNotas.Add(onotasAlumno)
            Next
            Return ltNotas
        Catch ex As Exception

        Finally

        End Try
    End Function

    Function crearListaAlumnosJalados(ByVal dtJa As System.Data.DataTable) As List(Of alumnoJalados)
        Dim oalumnoJalados As alumnoJalados
        Dim lstalumnoJalados As New List(Of alumnoJalados)
        Try
            For Each filas As DataRow In dtJa.Rows
                oalumnoJalados = New alumnoJalados
                oalumnoJalados.codAlumno = CInt(filas("AL_CodigoAlumno").ToString())
                oalumnoJalados.codSalon = CInt(filas("AU_CodigoAula").ToString())
                oalumnoJalados.nombre = filas("nombre").ToString()
                lstalumnoJalados.Add(oalumnoJalados)
            Next
            Return lstalumnoJalados
        Catch ex As Exception
        Finally

        End Try
    End Function
    Function crearDetalle(ByVal dtSalones As System.Data.DataTable, ByVal dtAlumno As System.Data.DataTable, ByVal dtNotas As System.Data.DataTable) As contexto
        Dim ocontexto As New contexto
        Dim lstsalon As New List(Of salon)
        lstsalon = crearListaSalones(dtSalones)

        Dim ltNotas As New List(Of notasAlumno)
        ltNotas = crearListaNotaAlumno(dtNotas)

        Dim lstalumnoJalados As New List(Of alumnoJalados)
        lstalumnoJalados = crearListaAlumnosJalados(dtAlumno)

        Dim notasAlumno As IEnumerable(Of notasAlumno)

        For Each oalumnoJalados As alumnoJalados In lstalumnoJalados
            notasAlumno = From h In ltNotas Where h.codAlumno = oalumnoJalados.codAlumno Select h


            For Each oonotasAlumno As notasAlumno In notasAlumno
                oalumnoJalados.ltNotas.Add(oonotasAlumno)
            Next


        Next

        Dim salonAlumno As IEnumerable(Of alumnoJalados)


        For Each osalon As salon In lstsalon

            salonAlumno = From h In lstalumnoJalados Where h.codSalon = osalon.codSalon Select h

            For Each oalumnoJalados As alumnoJalados In salonAlumno
                osalon.listaAlumnos.Add(oalumnoJalados)
            Next
            ocontexto.listaSalones.Add(osalon)
        Next

        Return ocontexto
        Try

        Catch ex As Exception
        Finally

        End Try
    End Function

#End Region

#Region "Reportes Matricula"
    Public Shared Function ExportarReporteMatriculaPorTipo(ByVal ds_Reporte As System.Data.DataSet, _
                                                           ByVal str_NombreEntidadReporte As String, _
                                                           ByVal str_PeriodoAcademico As String, _
                                                           ByVal str_TipoMatricula As String) As String
        Dim nombreRep As String
        nombreRep = GetNewName()
        Dim rutaTemporal As String = ""
        LlenarPlantillaReporteMatriculaporTipo(ds_Reporte, str_NombreEntidadReporte, rutaTemporal, str_PeriodoAcademico, str_TipoMatricula)
        Return rutaTemporal
    End Function

    Private Shared Function LlenarPlantillaReporteMatriculaporTipo(ByVal dsReporte As System.Data.DataSet, _
        ByVal str_NombreEntidadReporte As String, ByRef rutaTempDest As String, _
        ByVal str_PeriodoAcademidoRep As String, ByVal str_TipoMatricula As String) As String

        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_PagosServicios").ToString()
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
        Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
        File.Copy(rutaPlantillas, rutaREpositorioTemporales, True)

        Try

            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)
            Dim ws = workbook.Worksheet(1)

            Dim fila As Integer = 5
            Dim columna As Integer = 2
            Dim cont_columnas As Integer = 0
            Dim cont_filas As Integer = 0
            Dim str_Fila As String = ""

            ws.Row(2).Height = 30
            With ws.Range(ws.Cell(2, 3), ws.Cell(2, 3))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Style.Font.FontSize = 20
                .Value = "Reporte de Matricula de Alumnos - " & str_TipoMatricula & " - " & str_PeriodoAcademidoRep
            End With

            With ws.Range(ws.Cell(4, 3), ws.Cell(4, 3))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
            End With

            fila = 6 : columna = 2 : cont_columnas = 0 : cont_filas = 0

            ws.Column(1).Width = 3
            ws.Column(columna).Width = 6 'anio
            ws.Column(columna + 1).Width = 25 'grado aula   
            ws.Column(columna + 2).Width = 8 'codigo
            ws.Column(columna + 3).Width = 45 'apellidos y nombre            

            Dim str_Cabecera As String = ""

            For i = 0 To 3
                Select Case i
                    Case 0 : str_Cabecera = "Añio"
                    Case 1 : str_Cabecera = "Grado-Aula"
                    Case 2 : str_Cabecera = "Código"
                    Case 3 : str_Cabecera = "Apellidos y Nombres"
                End Select

                With ws.Range(ws.Cell(fila, columna + i), ws.Cell(fila, columna + i))
                    .Value = str_Cabecera
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#95b3d7")
                End With
            Next

            Dim dt_anios As System.Data.DataTable = dsReporte.Tables(0)
            Dim dt_aulas As System.Data.DataTable = dsReporte.Tables(1)
            Dim dt_alumnos As System.Data.DataTable = dsReporte.Tables(2)

            Dim lstAnios As IEnumerable(Of cla_anio)
            Dim lstGradoAulas As IEnumerable(Of cla_gradoaula)
            Dim lstAlumnos As IEnumerable(Of cla_alumno)

            lstAnios = _
            From sql1 In dt_anios.AsEnumerable() _
            Select New cla_anio With { _
                   .orden = sql1.Field(Of Integer)("fila"), _
                   .descripcion = sql1.Field(Of Integer)("anio"), _
                   .lstgradoaulas = (From sql2 In dt_aulas.AsEnumerable() _
                                     Where sql2.Field(Of Integer)("anio") = sql1.Field(Of Integer)("anio") _
                                     Select New cla_gradoaula With { _
                                        .codigogrado = sql2.Field(Of Integer)("grado"), _
                                        .codigoaula = sql2.Field(Of Integer)("aula"), _
                                        .gradoaula = sql2.Field(Of String)("gradoaula"), _
                                        .lstalumnos = (From sql3 In dt_alumnos.AsEnumerable() _
                                                       Where sql3.Field(Of Integer)("anio") = sql2.Field(Of Integer)("anio") And _
                                                             sql3.Field(Of Integer)("aula") = sql2.Field(Of Integer)("aula") _
                                                       Select New cla_alumno With { _
                                                            .codigoalumno = sql3.Field(Of String)("alumno"), _
                                                            .nombre = sql3.Field(Of String)("nombrealumno") _
                                                         }) _
                                  }) _
                    }

            Dim filAux As Integer = 0
            Dim colAux As Integer = 0
            fila = 7 : columna = 2 : cont_columnas = 0 : cont_filas = 0

            Dim desanio As Integer = 0
            Dim codaula As Integer = 0
            Dim codgrado As Integer = 0

            filAux = fila

            ' total de alumnos por aula
            Dim mifun2 As Func(Of cla_report, Decimal)
            mifun2 = Function(a) a.codigoalumno
            Dim tot = From t In dt_alumnos.AsEnumerable() _
                      Select anio = t.Field(Of Integer)("anio"), _
                             codigogrado = t.Field(Of Integer)("grado"), _
                             codigoaula = t.Field(Of Integer)("aula"), _
                             codigoalumno = t.Field(Of String)("alumno") _
                             Distinct

            For Each anio In lstAnios
                fila += 1
                desanio = anio.descripcion

                With ws.Range(ws.Cell(fila, columna), _
                              ws.Cell(fila, columna))
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.Bold = True
                    .Value = anio.descripcion
                    .Style.Font.FontSize = 9

                    For Each gradoaula In anio.lstgradoaulas
                        codgrado = gradoaula.codigogrado
                        codaula = gradoaula.codigoaula
                        With ws.Range(ws.Cell(fila, columna + 1), _
                                      ws.Cell(fila, columna + 1))
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Value = gradoaula.gradoaula
                            .Style.Font.FontSize = 9

                            For Each alumno In gradoaula.lstalumnos
                                With ws.Range(ws.Cell(fila, columna + 2), _
                                              ws.Cell(fila, columna + 2))
                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    .Value = alumno.codigoalumno
                                    .Style.Font.FontSize = 9
                                End With
                                With ws.Range(ws.Cell(fila, columna + 3), _
                                              ws.Cell(fila, columna + 3))
                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    .Value = alumno.nombre
                                    .Style.Font.FontSize = 9
                                End With
                                fila += 1
                            Next

                            With ws.Range(ws.Cell(fila, columna + 1), _
                                          ws.Cell(fila, columna + 2))
                                .Merge()
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Value = "Total Alumnos " & gradoaula.gradoaula & ":"
                                .Style.Font.FontSize = 9
                                .Style.Font.Bold = True
                                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
                            End With
                            With ws.Range(ws.Cell(fila, columna + 3), _
                                        ws.Cell(fila, columna + 3))
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Value = CStr(tot.Where(Function(b) b.codigoaula = codaula And b.codigogrado = codgrado And b.anio = desanio).Count)
                                .Style.Font.FontSize = 9
                                .Style.Font.Bold = True
                                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
                            End With
                            fila += 1
                            fila += 1
                        End With

                    Next

                    With ws.Range(ws.Cell(fila, columna), _
                                  ws.Cell(fila, columna + 2))
                        .Merge()
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = "Total Alumnos " & desanio & ":"
                        .Style.Font.FontSize = 9
                        .Style.Font.Bold = True
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
                    End With

                    With ws.Range(ws.Cell(fila, columna + 3), _
                                 ws.Cell(fila, columna + 3))
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = CStr(tot.Where(Function(b) b.anio = desanio).Count)
                        .Style.Font.FontSize = 9
                        .Style.Font.Bold = True
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
                    End With

                    fila += 1

                End With

            Next

            ws.SheetView.Freeze(6, 0)

            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales

        Catch ex As Exception
        End Try

    End Function
#End Region

#Region "Libretas"

#Region "Eventos"

    Protected Sub ddlSalonLibretas_SelectedIndexChanged()
        Try
            If ddlSalonLibretas.SelectedValue > 0 Then
                cargarGrillaAlumnos()
                chkAll1.Checked = False
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    'Protected Sub lstPresentacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPresentacion.SelectedIndexChanged


    '    cargarComboAsignacionAula(lstPresentacion.SelectedValue)

    '    'Public tipoReportePrimaria As Integer = 4
    '    'Public tipoReporteSecundaria As Integer = 2
    '    'Public tipoReporteIncial As Integer = 3


    'End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboAsignacionAula(ByVal int_TipoNota As Integer)

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim int_CodigoTrabajador As Integer = 0 ' todas las aulas de secundaria, sin filtrar por profesor
        Dim int_CodigoAnioAcademico As Integer = ddlAnioAcademicoLibretas.SelectedValue
        Dim int_CodigoSede As Integer = 1
        Dim int_Estado As Integer = 1

        Dim obj_BL_AsignacionAulas As New bl_AsignacionAulas
        Dim ds_Lista As DataSet = obj_BL_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
          int_CodigoTrabajador, int_TipoNota, int_CodigoAnioAcademico, int_CodigoSede, _
          int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

        Controles.llenarCombo(ddlSalonLibretas, ds_Lista, "Codigo", "DescAulaCompuestaCortaEsp", False, True)

    End Sub

    Private Sub cargarGrillaAlumnos()

        Dim obl_bl_alumnos As New bl_Alumnos
        Dim int_CodigoUsuario As String = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado ' Alumno 1 / Trabajadores 2 / Familiares 3 

        Dim int_CodigoAnioAcademico As Integer = ddlAnioAcademicoLibretas.SelectedValue
        Dim int_CodigoAsignacionAula As Integer = ddlSalonLibretas.SelectedValue()

        Dim ds_Lista As DataSet = obl_bl_alumnos.FUN_LIS_AlumnosPorAulaGradoyAnioAcademico( _
            int_CodigoAnioAcademico, int_CodigoAsignacionAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblidx As System.Web.UI.WebControls.Label = e.Row.FindControl("lblidx")
            lblidx.Text = e.Row.RowIndex + 1
            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If

    End Sub

#End Region

#Region "Exportacion"

    Private Function generarLibretaSecundaria(ByVal CodAnio As Integer, _
                                              ByVal CodBimestre As Integer, _
                                              ByVal CodSalon As Integer, _
                                              ByVal NombreArchivo As String, _
                                              ByVal int_idioma As Integer) As String

        Dim rutaTempDest As String = ""
        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaLibretaSecundariaWeb").ToString()
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
        Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
        File.Copy(rutaPlantillas, rutaREpositorioTemporales, True)

        Dim int_Total As Integer = 0

        For Each gvr As GridViewRow In GridView1.Rows
            If CType(gvr.FindControl("chk"), System.Web.UI.WebControls.CheckBox).Checked Then
                int_Total += 1
            End If
        Next

        Try

            Dim obl_rep_libretaNotas As New bl_rep_libretaNotas
            Dim ds_Lista As DataSet
            Dim str_CodigoAlumno As String = ""

            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)

            workbook.CalculateMode = XLCalculateMode.Auto

            Dim int_PosSheet As Integer = 0

            Dim fila As Integer = 5
            Dim columna As Integer = 2
            Dim cont_columnas As Integer = 0
            Dim cont_filas As Integer = 0
            Dim str_Fila As String = ""

            For ix As Integer = 0 To GridView1.Rows.Count - 1

                If CType(GridView1.Rows(ix).FindControl("Chk"), System.Web.UI.WebControls.CheckBox).Checked = False Then
                    Continue For
                End If

                int_PosSheet = int_PosSheet + 1
                str_CodigoAlumno = CType(GridView1.Rows(ix).FindControl("lblCodigoAlumno"), System.Web.UI.WebControls.Label).Text
                ds_Lista = obl_rep_libretaNotas.FUN_LIS_REP_LibretaNotasSecundariaImp( _
                            str_CodigoAlumno, CodBimestre, CodAnio, int_idioma, 0, 0, 0, 0)

                Dim ws = workbook.Worksheet(int_PosSheet)

                fila = 5
                columna = 2
                cont_columnas = 0
                cont_filas = 0
                str_Fila = ""

                cont_columnas = 0
                cont_filas = 0
                columna += 1

                ' Pintado de Titulo
                Dim str_GradoAula As String = ds_Lista.Tables(4).Rows(0).Item("DescIngles").ToString
                Dim str_NombreTutor As String = ds_Lista.Tables(4).Rows(0).Item("NombreTutor").ToString
                Dim str_NombreAlumno As String = ds_Lista.Tables(4).Rows(0).Item("NombreAlumno").ToString

                ws.Row(1).Height = 40 ' Listado de Cursos
                With ws.Range(ws.Cell(1, 6), ws.Cell(1, 24))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 20
                    .Style.Font.Bold = True
                    .Value = "REPORT CARD"
                End With

                With ws.Range(ws.Cell(2, 6), ws.Cell(2, 8))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = "NAME"
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.RightBorderColor = XLColor.Black
                End With

                ws.Row(2).Height = 20 ' Listado de Cursos
                With ws.Range(ws.Cell(2, 9), ws.Cell(2, 24))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = str_NombreAlumno
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                ws.Row(3).Height = 20
                With ws.Range(ws.Cell(3, 6), ws.Cell(3, 8))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = "CLASS"
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.RightBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(3, 9), ws.Cell(3, 24))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = str_GradoAula
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                ws.Row(4).Height = 20
                With ws.Range(ws.Cell(4, 6), ws.Cell(4, 8))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = "TUTOR"
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.RightBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(4, 9), ws.Cell(4, 24))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Value = str_NombreTutor
                End With

                With ws.Range(ws.Cell(2, 6), ws.Cell(4, 24))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                'Pintado de Fecha 
                With ws.Cell(2, 30)
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Font.Bold = True
                    .Value = "Date: " & Now.ToString("MMMM d, yyyy")
                End With

                Dim colIni As Integer = 0
                Dim colFin As Integer = 0
                Dim lstPosCursos As New List(Of Integer)

                For i As Integer = 0 To 60 ' Pintado de Cursos
                    colIni = columna + i ' (i * 3)
                    colFin = colIni + 2
                    ws.Column(colIni).Width = 3
                Next

                colIni = 0
                colFin = 0

                ws.Row(5).Height = 30 ' Listado de Cursos
                For i As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos
                    colIni = columna + (i * 3)
                    colFin = colIni + 2
                    lstPosCursos.Add(colIni) ' agrego la columna de la posicion inicial
                    With ws.Range(ws.Cell(fila, colIni), _
                                  ws.Cell(fila, colFin))
                        .Merge()
                        .Value = ds_Lista.Tables(3).Rows(i).Item("NombreCurso")
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 8
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.WrapText = True
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorderColor = XLColor.Black
                    End With
                    With ws.Range(ws.Cell(fila + 1, colIni), _
                                  ws.Cell(fila + 1, colFin)) ' Fila 6 : Lista de Codigos Asignacion de Grupo
                        .Merge()
                        .Value = ds_Lista.Tables(3).Rows(i).Item("CodigoAsignacionGrupo")
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 8
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.WrapText = True
                    End With
                Next

                columna -= 1

                Dim dt As System.Data.DataTable = ds_Lista.Tables(0)
                Dim sql = From s In ds_Lista.Tables(0).AsEnumerable() _
                          Select CodigoAsignacionGrupo = s.Field(Of Integer)("CodigoGrupoCriterio") _
                          Distinct

                Dim int_NumGrupo As Integer = sql.Count
                Dim int_NumCriterios As Integer = ds_Lista.Tables(0).Rows.Count
                Dim int_NumCalificativos As Integer = ds_Lista.Tables(1).Rows.Count
                Dim int_NumCursos As Integer = ds_Lista.Tables(3).Rows.Count

                Dim int_UltimaFila As Integer = fila + int_NumCriterios + int_NumGrupo + 2 ' 2 grupos de criterios extras
                Dim int_UltimaColumna As Integer = columna + (int_NumCursos * int_NumCalificativos) ' 4 columnas con campos calculados

                fila += 1
                fila += 1

                Dim lstPos As New List(Of Integer)
                Dim str_Grupo As String = ""
                Dim bool_PintadoGrupo As Boolean = False

                Dim int_CodigoAsignacionGrupo As Integer = 0
                Dim int_CodigoCalificativo As Integer = 0
                Dim int_Idx As Integer = 0
                Dim str_Nota As String = ""
                Dim bool_NotaCriterio As Boolean = False

                Dim int_CodigoCriterio As Integer = 0

                For i As Integer = 0 To ds_Lista.Tables(0).Rows.Count - 1 ' Pintado de Criterios
                    colIni = 0
                    If str_Grupo = "" Or str_Grupo <> ds_Lista.Tables(0).Rows(i).Item("GrupoCriterio") Then

                        str_Grupo = ds_Lista.Tables(0).Rows(i).Item("GrupoCriterio")
                        With ws.Cell(fila + i, columna)
                            .Style.Font.Bold = True
                            .Value = str_Grupo
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorderColor = XLColor.Black
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.RightBorderColor = XLColor.Black
                        End With

                        For j2 As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1
                            colIni = columna + 1 + (j2 * 3)
                            For kkk = 0 To 2
                                With ws.Cell(fila + i, colIni + kkk)
                                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                    .Style.Border.TopBorderColor = XLColor.Black
                                    If kkk = 0 Then
                                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                        .Style.Border.LeftBorderColor = XLColor.Black
                                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                        .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                                    ElseIf kkk = 1 Then
                                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                        .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo      
                                    End If
                                End With
                            Next
                        Next

                        If bool_PintadoGrupo = False Then
                            For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1 ' Pintado de Cursos
                                colIni = columna + 1 + (j * 3)
                                For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1 ' Pintado de Calificativos
                                    With ws.Cell(fila + i, colIni + k)
                                        .Style.Font.Bold = True
                                        .Value = ds_Lista.Tables(1).Rows(k).Item("Abreviatura")
                                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                        .Style.Border.BottomBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                                        If k = 0 Then
                                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                            .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                            .Style.Border.LeftBorderColor = XLColor.Black '.FromArgb(191, 191, 191) 'Plomo
                                        ElseIf k = 1 Then
                                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                            .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo                                         
                                        End If
                                    End With
                                    ws.Cell(fila + i + 1, colIni + k).Value = ds_Lista.Tables(1).Rows(k).Item("CodigoCalificativo")
                                Next
                            Next
                            bool_PintadoGrupo = True
                            fila += 1
                        End If
                        lstPos.Add(fila + i)
                        fila += 1
                    End If

                    ws.Cell(fila + i, columna).Value = ds_Lista.Tables(0).Rows(i).Item("Criterio")
                    int_CodigoCriterio = ds_Lista.Tables(0).Rows(i).Item("CodigoCriterio")
                    int_CodigoAsignacionGrupo = 0
                    colIni = 0

                    For j As Integer = 0 To ds_Lista.Tables(3).Rows.Count - 1
                        colIni = columna + 1 + (j * 3)
                        int_CodigoAsignacionGrupo = ws.Cell(6, colIni).Value
                        int_CodigoCalificativo = 0
                        For kk = 0 To 2
                            With ws.Cell(fila + i, colIni + kk)
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                                If kk = 0 Then
                                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                    .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                    .Style.Border.LeftBorderColor = XLColor.Black
                                ElseIf kk = 1 Then
                                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                    .Style.Border.RightBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo                                         
                                End If
                            End With
                        Next

                        If bool_NotaCriterio Then : bool_NotaCriterio = False : End If
                        For k As Integer = 0 To ds_Lista.Tables(1).Rows.Count - 1
                            int_CodigoCalificativo = ws.Cell(8, columna + 1 + k).Value
                            For l As Integer = 0 To ds_Lista.Tables(2).Rows.Count - 1
                                If ds_Lista.Tables(2).Rows(l).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo And _
                                    ds_Lista.Tables(2).Rows(l).Item("CodigoCalificativo") = int_CodigoCalificativo And _
                                    ds_Lista.Tables(2).Rows(l).Item("CodigoCriterio") = int_CodigoCriterio Then
                                    With ws.Cell(fila + i, colIni + k)
                                        .Value = "X"
                                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    End With
                                    bool_NotaCriterio = True
                                    Exit For
                                End If
                            Next
                            If bool_NotaCriterio Then : Exit For : End If
                        Next
                    Next
                Next

                ws.Column(2).Width = 43 ' Listado de Criterios

                With ws.Cell(int_UltimaFila + 1, 2)
                    .Value = "ACADEMIC PERFORMANCE"
                    .Style.Font.Bold = True
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                End With

                With ws.Cell(int_UltimaFila + 2, 2)
                    .Value = "OVERALL ATTAINMENT"
                    .Style.Font.Bold = True
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With

                ws.Row(int_UltimaFila + 2).Height = 26 ' fila:24

                colIni = 0
                colFin = 0

                For i As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1 ' Pintado de Nota de Cursos 
                    colIni = 0
                    colIni = columna + 1 + (i * 3)
                    colFin = colIni + 2

                    'int_CodigoAsignacionGrupo = ws.Cell(6, colIni).Value()
                    int_CodigoAsignacionGrupo = IIf(ws.Cell(6, colIni).Value().ToString.Length = 0, 0, ws.Cell(6, colIni).Value())

                    For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                        If ds_Lista.Tables(5).Rows(j).Item("CodigoAsignacionGrupo") = int_CodigoAsignacionGrupo Then
                            With ws.Range(ws.Cell(int_UltimaFila + 1, colIni), ws.Cell(int_UltimaFila + 1, colFin))
                                .Merge()
                                .Value = ds_Lista.Tables(5).Rows(j).Item("NotaBimestre")
                                .Style.Font.FontName = "Arial"
                                .Style.Font.FontSize = 8
                                .Style.Font.Bold = True
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Style.Alignment.WrapText = True
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorderColor = XLColor.Black
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorderColor = XLColor.FromArgb(191, 191, 191) 'Plomo
                            End With

                            With ws.Range(ws.Cell(int_UltimaFila + 1 + 1, colIni), ws.Cell(int_UltimaFila + 1 + 1, colFin))
                                .Merge()
                                .Value = ds_Lista.Tables(5).Rows(j).Item("Observacion")
                                .Style.Font.FontName = "Arial"
                                .Style.Font.FontSize = 8
                                .Style.Font.Bold = True
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Style.Alignment.WrapText = True
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorderColor = XLColor.Black
                            End With
                            Exit For
                        End If
                    Next
                Next

                With ws.Range(ws.Cell(5, 2), ws.Cell(5, colFin))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(6, 2), ws.Cell(int_UltimaFila, colFin))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_UltimaFila + 1, 2), ws.Cell(int_UltimaFila + 2, colFin))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                ' PINTADO DE NOTAS 
                Dim int_FilaPintadoNotas As Integer = int_UltimaFila + 4 '51
                Dim int_FilaNotas As Integer = int_FilaPintadoNotas ' int_UltimaFilaComentario + 2
                Dim int_UltimaFilaNotas As Integer = int_FilaNotas + ds_Lista.Tables(5).Rows.Count
                Dim int_UltimaColumnaNotas As Integer = 2 + ds_Lista.Tables(5).Columns.Count - 6

                With ws.Range(ws.Cell(int_FilaNotas, 2), _
                              ws.Cell(int_FilaNotas, 9))
                    .Merge()
                    .Value = "TERM AND ANNUAL MARK"
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With
                int_FilaNotas += 1

                For i As Integer = 0 To ds_Lista.Tables(5).Columns.Count - 1
                    If i > 3 And i < 13 - 2 Then
                        If i = 12 - 2 Then
                            With ws.Range(ws.Cell(int_FilaNotas, 2 + i - 4), _
                                          ws.Cell(int_FilaNotas, 2 + i - 4 + 1))
                                .Merge()
                                .Value = ds_Lista.Tables(5).Columns(i).ColumnName
                                .Style.Font.FontName = "Arial"
                                .Style.Font.FontSize = 9
                                .Style.Font.Bold = True
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorderColor = XLColor.Black
                            End With
                            For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                                With ws.Range(ws.Cell(int_FilaNotas + 1 + j, 2 + i - 4), _
                                              ws.Cell(int_FilaNotas + 1 + j, 2 + i - 4 + 1))
                                    .Merge()
                                    .Value = ds_Lista.Tables(5).Rows(j).Item(i)
                                    .Style.Font.FontName = "Arial"
                                    .Style.Font.FontSize = 8
                                    .Style.Font.Bold = True
                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                    .Style.Border.BottomBorderColor = XLColor.Black
                                End With
                            Next
                        Else
                            With ws.Range(ws.Cell(int_FilaNotas, 2 + i - 4), _
                                          ws.Cell(int_FilaNotas, 2 + i - 4))
                                .Value = ds_Lista.Tables(5).Columns(i).ColumnName
                                .Style.Font.FontName = "Arial"
                                .Style.Font.FontSize = 9
                                .Style.Font.Bold = True
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.RightBorderColor = XLColor.Black
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorderColor = XLColor.Black
                            End With
                            For j As Integer = 0 To ds_Lista.Tables(5).Rows.Count - 1
                                With ws.Range(ws.Cell(int_FilaNotas + 1 + j, 2 + i - 4), _
                                              ws.Cell(int_FilaNotas + 1 + j, 2 + i - 4))
                                    .Value = ds_Lista.Tables(5).Rows(j).Item(i)
                                    .Style.Font.Bold = True
                                    If i = 4 Then
                                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                        .Style.Font.FontSize = 10
                                    Else
                                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                        .Style.Font.FontSize = 8
                                    End If
                                    .Style.Font.FontName = "Arial"
                                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                    .Style.Border.RightBorderColor = XLColor.Black
                                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                    .Style.Border.BottomBorderColor = XLColor.Black
                                End With
                            Next
                        End If
                    End If
                Next

                With ws.Range(ws.Cell(int_FilaNotas - 1, 2), _
                              ws.Cell(int_UltimaFilaNotas + 1, int_UltimaColumnaNotas))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_UltimaFilaNotas + 2, 2), _
                              ws.Cell(int_UltimaFilaNotas + 2, int_UltimaColumnaNotas))
                    .Merge()
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Value = "Note: The note indicates 'P' value test is pending."
                    .Style.Font.Bold = True
                End With

                ' PINTADO DE STUDENT PROFILE 
                Dim int_FilaProfile As Integer = int_FilaPintadoNotas '  int_UltimaFilaComentario + 2
                Dim int_ColumnaProfile As Integer = int_UltimaColumnaNotas + 2
                Dim int_UltimaFilaProfile As Integer = int_FilaNotas - 2 + 8 + ds_Lista.Tables(7).Rows.Count * 2
                Dim int_UltimaColumnaProfile As Integer = int_ColumnaProfile + ds_Lista.Tables(8).Rows.Count * 2

                Dim lstPosProfile As New List(Of posicionCelda)
                Dim posCelda As posicionCelda

                Dim int_PosProfileFila As Integer = 0
                Dim int_PosProfileColumna As Integer = 0

                With ws.Range(ws.Cell(int_FilaProfile, int_ColumnaProfile), _
                              ws.Cell(int_FilaProfile + 7 + (ds_Lista.Tables(7).Rows.Count) * 2, int_ColumnaProfile + 8 + (ds_Lista.Tables(8).Rows.Count) * 2))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaProfile, int_ColumnaProfile), _
                              ws.Cell(int_FilaProfile + 7, int_ColumnaProfile + 8))
                    .Merge()
                    .Value = "STUDENT PROFILE"
                    .Style.Font.Bold = True
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                For i As Integer = 0 To ds_Lista.Tables(8).Rows.Count - 1

                    With ws.Cell(int_FilaProfile - 1, _
                                 int_ColumnaProfile + 8 + 1 + i * 2)
                        .Value = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
                        posCelda = New posicionCelda
                        posCelda.posFila = int_FilaProfile - 1
                        posCelda.posColumna = int_ColumnaProfile + 8 + 1 + i * 2
                        posCelda.Codigo = ds_Lista.Tables(8).Rows(i).Item("CodigoCalificativo")
                        lstPosProfile.Add(posCelda)
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile, int_ColumnaProfile + 8 + 1 + i * 2), _
                                  ws.Cell(int_FilaProfile + 7, int_ColumnaProfile + 8 + 1 + i * 2))
                        .Merge()
                        .Value = ds_Lista.Tables(8).Rows(i).Item("Calificativo")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom
                        .Style.Alignment.WrapText = True
                        .Style.Alignment.TextRotation = 90
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 9
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorderColor = XLColor.Black
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile, int_ColumnaProfile + 8 + 2 + i * 2), _
                                  ws.Cell(int_FilaProfile + 7, int_ColumnaProfile + 8 + 2 + i * 2))
                        .Merge()
                        .Value = ds_Lista.Tables(8).Rows(i).Item("CalificativoES")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom
                        .Style.Alignment.WrapText = True
                        .Style.Alignment.TextRotation = 90
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 9
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorderColor = XLColor.Black
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile, int_ColumnaProfile + 8 + 1 + i * 2), _
                                  ws.Cell(int_FilaProfile + 7 + (ds_Lista.Tables(7).Rows.Count) * 2, int_ColumnaProfile + 8 + 1 + i * 2))
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorderColor = XLColor.Black
                    End With
                Next

                Dim int_codCriterio As Integer = 0
                Dim int_codCalificativo As Integer = 0
                Dim int_codCalificativoAux As Integer = 0
                Dim int_codCalificativoPos As Integer = 0

                For i As Integer = 0 To ds_Lista.Tables(7).Rows.Count - 1

                    With ws.Cell(int_FilaProfile + 8 + i * 2, _
                                 int_ColumnaProfile - 1)
                        .Value = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
                        int_codCriterio = ds_Lista.Tables(7).Rows(i).Item("CodigoCriterio")
                        posCelda = New posicionCelda
                        posCelda.posFila = int_FilaProfile + 8 + i * 2
                        posCelda.posColumna = int_ColumnaProfile - 1
                        posCelda.Codigo = 0
                        lstPosProfile.Add(posCelda)
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile + 8 + i * 2, int_ColumnaProfile), _
                                  ws.Cell(int_FilaProfile + 8 + i * 2, int_ColumnaProfile + 8))
                        .Merge()
                        .Value = ds_Lista.Tables(7).Rows(i).Item("Criterio")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.WrapText = True
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 9
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.RightBorderColor = XLColor.Black
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile + 9 + i * 2, int_ColumnaProfile), _
                                  ws.Cell(int_FilaProfile + 9 + i * 2, int_ColumnaProfile + 8))
                        .Merge()
                        .Value = ds_Lista.Tables(7).Rows(i).Item("CriterioES")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.WrapText = True
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 9
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.RightBorderColor = XLColor.Black
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorderColor = XLColor.Black
                    End With

                    With ws.Range(ws.Cell(int_FilaProfile + 9 + i * 2, int_ColumnaProfile + 8), _
                                  ws.Cell(int_FilaProfile + 9 + i * 2, int_ColumnaProfile + 8 + (ds_Lista.Tables(8).Rows.Count) * 2))
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorderColor = XLColor.Black
                    End With

                    For k As Integer = 0 To ds_Lista.Tables(9).Rows.Count - 1
                        If int_codCriterio = ds_Lista.Tables(9).Rows(k).Item("CodigoCriterio") Then
                            int_codCalificativo = ds_Lista.Tables(9).Rows(k).Item("CodigoCalificativo")
                            For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
                                If posCel.Codigo > 0 Then
                                    If posCel.Codigo = int_codCalificativo Then
                                        With ws.Range(ws.Cell(int_FilaProfile + 8 + i * 2, posCel.posColumna), _
                                                      ws.Cell(int_FilaProfile + 9 + i * 2, posCel.posColumna + 1))
                                            .Merge()
                                            .Value = "X"
                                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                            .Style.Font.FontName = "Arial"
                                            .Style.Font.FontSize = 9
                                            .Style.Font.Bold = True
                                        End With
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                    Next
                Next

                For Each posCel As posicionCelda In lstPosProfile ' Limpio todos los codigos pintados previamente
                    With ws.Cell(posCel.posFila, posCel.posColumna)
                        .Value = ""
                    End With
                Next

                ' PINTADO DE ASISTENCIA
                Dim int_FilaAsistencia As Integer = int_FilaPintadoNotas ' int_UltimaFilaComentario + 2
                Dim int_ColumnaAsistencia As Integer = int_UltimaColumnaProfile + 1 + 9

                Dim int_UltimaFilaAsistencia As Integer = int_FilaAsistencia - 1 + 8 + 14
                'Dim int_UltimaColumnaAsistencia As Integer = int_ColumnaAsistencia + 8 + 10

                Dim lstPosAsistencia As New List(Of posicionCelda)
                Dim posCelda2 As posicionCelda

                Dim int_PosAsistenciaFila As Integer = 0
                Dim int_PosAsistenciaColumna As Integer = 0

                With ws.Range(ws.Cell(int_FilaAsistencia, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 7 + (ds_Lista.Tables(10).Rows.Count) * 2, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "ATTENDANCE                                  Asistencia"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Style.Alignment.WrapText = True
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 7, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                Dim str_Bimestre As String = ""
                For i As Integer = 0 To 4
                    Select Case i
                        Case 0 : str_Bimestre = "TERM I"
                        Case 1 : str_Bimestre = "TERM II"
                        Case 2 : str_Bimestre = "TERM III"
                        Case 3 : str_Bimestre = "TERM IV"
                        Case 4 : str_Bimestre = "AVERAGE"
                    End Select

                    With ws.Cell(int_FilaAsistencia - 1, int_ColumnaAsistencia + 8 + 1 + i * 2)
                        .Value = "" 'i + 1
                        posCelda2 = New posicionCelda
                        posCelda2.posFila = int_FilaAsistencia - 1
                        posCelda2.posColumna = int_ColumnaAsistencia + 8 + 1 + i * 2
                        posCelda2.Codigo = i + 1
                        lstPosAsistencia.Add(posCelda2)
                    End With

                    With ws.Range(ws.Cell(int_FilaAsistencia, int_ColumnaAsistencia + 8 + 1 + i * 2), _
                                  ws.Cell(int_FilaAsistencia + 7, int_ColumnaAsistencia + 8 + 2 + i * 2))
                        .Merge()
                        .Value = str_Bimestre
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom
                        .Style.Alignment.WrapText = True
                        .Style.Alignment.TextRotation = 90
                        .Style.Font.FontName = "Arial"
                        .Style.Font.FontSize = 10
                        .Style.Font.Bold = True
                    End With

                    With ws.Range(ws.Cell(int_FilaAsistencia, int_ColumnaAsistencia + 8 + 1 + i * 2), _
                                  ws.Cell(int_FilaAsistencia + 7 + (ds_Lista.Tables(10).Rows.Count) * 2, int_ColumnaAsistencia + 8 + 2 + i * 2))
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorderColor = XLColor.Black
                    End With
                Next

                Dim int_codBimestre As Integer = 0
                Dim int_codBimestreAux As Integer = 0

                Dim lstPosAsis As New List(Of posicionCelda)
                Dim posCelda3 As posicionCelda

                ' TARDANZAS Y CONDUCTA
                ' TARDANZAS
                With ws.Range(ws.Cell(int_FilaAsistencia + 8, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 11, int_ColumnaAsistencia + 4))
                    .Merge()
                    .Value = "LATES          Tardanzas"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 11, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 11, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 9, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "Justified          Justificado"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 9
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 9, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 9, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 8, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 8 + 8, int_ColumnaAsistencia + 5))
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 10, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 11, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "Unjustified          Injustificado"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 9
                End With

                ' FALTAS
                With ws.Range(ws.Cell(int_FilaAsistencia + 12, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 15, int_ColumnaAsistencia + 4))
                    .Merge()
                    .Value = "ABSENCES          Ausencias"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 15, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 15, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 12, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 13, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "Justified          Justificado"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 9
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 13, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 13, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 14, int_ColumnaAsistencia + 5), _
                              ws.Cell(int_FilaAsistencia + 15, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "Unjustified          Injustificado"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 9
                End With

                ' DEMERITOS
                With ws.Range(ws.Cell(int_FilaAsistencia + 16, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 17, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "DEMERITS                                            Deméritos"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 17, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 17, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                ' MERITOS
                With ws.Range(ws.Cell(int_FilaAsistencia + 18, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 19, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "MERITS                                                      Méritos"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 19, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 19, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                ' CONDUCT MARK
                With ws.Range(ws.Cell(int_FilaAsistencia + 20, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 21, int_ColumnaAsistencia + 8))
                    .Merge()
                    .Value = "CONDUCT MARK                                            Nota de conducta"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 21, int_ColumnaAsistencia), _
                              ws.Cell(int_FilaAsistencia + 21, int_ColumnaAsistencia + 8 + 10))
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorderColor = XLColor.Black
                End With

                Dim pos_Bimestre As Integer = 0
                Dim int_filapos As Integer = 0
                Dim int_colpos As Integer = 0
                Dim int_filaini As Integer = 0
                Dim str_Rango As String = ""

                If ds_Lista.Tables(10).Rows.Count > 0 Then
                    For i As Integer = 0 To ds_Lista.Tables(10).Rows.Count - 1

                        int_filapos = 8 + i * 2 : int_colpos = 9 : pos_Bimestre = 0
                        With ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                            .Merge()
                            .Value = ds_Lista.Tables(10).Rows(i).Item("Bim1")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 10
                            .Style.Font.Bold = True
                        End With

                        pos_Bimestre = 2
                        With ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                            .Merge()
                            .Value = ds_Lista.Tables(10).Rows(i).Item("Bim2")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 10
                            .Style.Font.Bold = True
                        End With

                        pos_Bimestre = 4
                        With ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                            .Merge()
                            .Value = ds_Lista.Tables(10).Rows(i).Item("Bim3")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 10
                            .Style.Font.Bold = True
                        End With

                        pos_Bimestre = 6
                        With ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                            .Merge()
                            .Value = ds_Lista.Tables(10).Rows(i).Item("Bim4")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 10
                            .Style.Font.Bold = True
                        End With

                        str_Rango = DevLetraColumna(int_ColumnaAsistencia + int_colpos) + (int_FilaAsistencia + int_filapos).ToString + ":" + _
                                    DevLetraColumna(int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre) + (int_FilaAsistencia + int_filapos + 1).ToString

                        pos_Bimestre = 8

                        'Dim cellWithFormulaA1 = ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                        '                                 ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))

                        'cellWithFormulaA1.FormulaA1 = ds_Lista.Tables(10).Rows(i).Item("operacion") & "(" + str_Rango + ")"


                        With ws.Range(ws.Cell(int_FilaAsistencia + int_filapos, int_ColumnaAsistencia + int_colpos + pos_Bimestre), _
                                      ws.Cell(int_FilaAsistencia + int_filapos + 1, int_ColumnaAsistencia + int_colpos + 1 + pos_Bimestre))
                            .Merge()
                            .Value = ds_Lista.Tables(10).Rows(i).Item("operacion")
                            .Style.NumberFormat.Format = "0"
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 10
                            .Style.Font.Bold = True
                        End With

                    Next
                End If

                ' PINTADO DE COMENTARIOS ' 17 cursos 
                Dim int_FilaComentario As Integer = 59 '62 '54 ' 51 ' int_UltimaFila + 3
                Dim int_MaxNumFilasComentario As Integer = 13 * 2 '13 * 2
                Dim int_UltimaFilaComentario As Integer = int_FilaComentario + int_MaxNumFilasComentario + 1  'ds_Lista.Tables(6).Rows.Count
                int_FilaComentario += 1
                With ws.Cell(int_FilaComentario, 2)
                    .Value = "COMMENTS"
                    .Style.Font.Bold = True
                End With
                int_FilaComentario += 1

                Dim pos_ComentAux As Integer = 0
                Dim int_FilasPintadas As Integer = 0

                'maximo 17 cursos
                int_UltimaColumna = 53

                For i As Integer = 0 To ds_Lista.Tables(6).Rows.Count - 1 ' Pintado de Oservaciones de Cursos 

                    int_FilasPintadas += 1
                    ws.Row(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1).Height = 40

                    If ds_Lista.Tables(6).Rows(i).Item("Observacion") IsNot DBNull.Value Then

                        With ws.Range(ws.Cell(int_FilaComentario - 2 + int_FilasPintadas * 2, 2), _
                                     ws.Cell(int_FilaComentario - 2 + int_FilasPintadas * 2, 50)) 'int_UltimaColumna + 12))
                            .Merge()
                            .Value = ds_Lista.Tables(6).Rows(i).Item("Curso") & ":"
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 11 '10
                            .Style.Font.Bold = True
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                        End With

                        ws.Row(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1).Height = 40
                        With ws.Range(ws.Cell(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, 2), _
                                     ws.Cell(int_FilaComentario - 2 + int_FilasPintadas * 2 + 1, 50)) 'int_UltimaColumna + 12))
                            .Merge()
                            .Value = ds_Lista.Tables(6).Rows(i).Item("Observacion")
                            .Style.Font.FontName = "Arial"
                            .Style.Font.FontSize = 11 ' 9
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.WrapText = True
                        End With

                    End If
                Next

                int_FilaAsistencia = int_UltimaFilaComentario + 10 - 25
                With ws.Range(ws.Cell(int_FilaComentario, 2), _
                              ws.Cell(int_FilaComentario + int_FilasPintadas * 2, 50)) 'int_UltimaColumna + 12))
                    .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                    .Style.Border.OutsideBorderColor = XLColor.Black
                End With

                ' FIRMAS
                With ws.Range(ws.Cell(int_FilaAsistencia + 23, 2), ws.Cell(int_FilaAsistencia + 23, 5))
                    .Merge()
                    .Value = "SIGNATURE OF TUTOR"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorderColor = XLColor.Black
                End With

                With ws.Range(ws.Cell(int_FilaAsistencia + 23, 8), ws.Cell(int_FilaAsistencia + 23, 23))
                    .Merge()
                    .Value = "SIGNATURE OF PARENT"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.FontName = "Arial"
                    .Style.Font.FontSize = 10
                    .Style.Font.Bold = True
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorderColor = XLColor.Black
                End With

                ws.Rows(6).Height = 0 ' Elimino la fila de codigos de asignacion de grupos
                ws.Rows(8).Height = 0 ' Elimino la fila de codigos de los calificativos

                ws.Row(4).InsertRowsBelow(1)
                ws.Rows(5).Height = 5 ' Listado de Cursos

                'borrarPintado(ws, ws.Range(ws.Cell(5, 2), ws.Cell(5, int_UltimaColumna)))
                'cuadradoCompleto(ws, ws.Range(ws.Cell(2, 6), ws.Cell(4, 24)))
                'ws.Columns("A:A").Delete()

                ws.Column(1).Width = 3
                ws.Column(2).Width = 45
                ws.PageSetup.PagesWide = 1
                ws.PageSetup.PrintAreas.Add("A1:BA94")

                ws.PageSetup.AdjustTo(60)
                ws.PageSetup.PageOrientation = ClosedXML.Excel.XLPageOrientation.Landscape
                ws.PageSetup.Margins.Top = 0.5 '1.9
                ws.PageSetup.Margins.Bottom = 0.5 '1.9
                ws.PageSetup.Margins.Left = 0 '0.6
                ws.PageSetup.Margins.Right = 0 '0.6
                ws.PageSetup.Margins.Header = 0 '0.8
                ws.PageSetup.Margins.Footer = 0 '0.8

                'ws.PageSetup.PrintAreas.Add("B1:AX95")
                'ws.PageSetup.PrintAreas.Add("A1:BA94")

            Next

            'For idel As Integer = int_PosSheet + 1 To 35
            '    workbook.Worksheet(idel).Delete()
            'Next

            'Dim ite As Integer = 35 - (int_PosSheet + 1)
            'For id As Integer = 0 To ite - 1
            '    workbook.Worksheet(int_PosSheet + 1).Delete()
            'Next

            ''For idel As Integer = 35 To int_PosSheet + 1 Step -1
            ''    workbook.Worksheet(idel).Delete()
            ''    workbook.Save()
            ''Next

            '' ''workbook.Worksheet(3).Delete()
            ' ''workbook.Worksheets.Delete(2)
            '' ''workbook.Save()
            ' ''workbook.Worksheets.Delete(3)
            '' '' workbook.Save()

            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales

            Return rutaTempDest

        Catch ex As Exception
            Return rutaTempDest
        End Try

    End Function

#End Region

#End Region

#Region "Clases auxiliares"

    Public Class cla_anio
        Public orden As Integer
        Public descripcion As Integer
        Public lstgradoaulas As IEnumerable(Of cla_gradoaula)
    End Class
    Public Class cla_gradoaula
        Public codigogrado As Integer
        Public codigoaula As Integer
        Public gradoaula As String
        Public lstalumnos As IEnumerable(Of cla_alumno)
    End Class
    Public Class cla_alumno
        Public codigoalumno As String
        Public nombre As String
    End Class
    Public Class cla_report
        Public anio As Integer
        Public grado As Integer
        Public aula As Integer
        Public codigoalumno As String
    End Class

#End Region

#Region "Crear libreta inicial "


    ''
    ''
    ''ahora 
    Public Function crearLibretaInicial1(ByVal codSalon As Integer, ByVal CodBimestre As Integer, ByVal nombreArchivo As String) As String
        Dim dt_ausencias As New System.Data.DataTable

        Dim cantidadAlumnos As Integer = 0

        Try


            ''codSalon = 8
            '<add key="LibretaPrimaria"  value="\Plantillas\ExportacionLibreta\libretaPrimaria.xlsx"/>
            ' <add key="LibretaInicial" value="\Plantillas\ExportacionLibreta\libretaInicial.xlsx"/>


            'Dim rutaApp As String = ""

            'rutaApp = Environment.CurrentDirectory()

            'Dim rutaPlantillas As String = System.Configuration.ConfigurationManager.AppSettings.Item("LibretaPrimaria").ToString.Trim

            'Dim rutaREpositorioTemporales As String = System.Configuration.ConfigurationManager.AppSettings.Item("Temporales").ToString.Trim & rutaTemp & ".xlsx"

            'File.Copy(rutaApp & rutaPlantillas, rutaApp & rutaREpositorioTemporales)

            'wbkWorkbook = excel.Workbooks.Open(rutaApp & rutaREpositorioTemporales)

            Dim abrBimestre As String = ""
            If CodBimestre = 1 Then
                abrBimestre = "I"
            End If
            If CodBimestre = 2 Then
                abrBimestre = "II"
            End If
            If CodBimestre = 3 Then
                abrBimestre = "III"
            End If
            If CodBimestre = 4 Then
                abrBimestre = "IV"
            End If



            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaLibretaInicial")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)


            '    File.Copy(rutaApp & rutaPlantillas, rutaApp & rutaREpositorioTemporales)

            'Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet






            Dim dt As New System.Data.DataTable

            Dim dst As New DataSet

            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaInicial_1(codSalon, CodBimestre, nombreArchivo, 1, 1, 1, 1)
            dt = dst.Tables(0)
            dt_ausencias = dst.Tables(3)

            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)


            '  lst.OrderBy(Function(iperosona) iperosona.nombreAlumno)

            ' Dim Int_nueva As Integer = (From h In dt_ListaAlumnos.AsEnumerable() Where h.Field(Of Boolean)("Chk") = True Select h).ToList().Count


            '  cantidadAlumnos = Int_nueva
            '




            ' Dim excel As New ApplicationClass
            ' Dim wbkWorkbook As Workbook
            ' Dim wshWorksheet As Worksheet
            'Dim rng As Range



            ' wbkWorkbook = excel.Workbooks.Open(rutaApp & rutaREpositorioTemporales)



            Dim workbook As New ClosedXML.Excel.XLWorkbook(rutaREpositorioTemporales)

            workbook.CalculateMode = XLCalculateMode.Auto






            'Dim ws As Worksheet


            'Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")


            ' wshWorksheet = wbkWorkbook.Worksheets(1)
            ' wshWorksheet.Visible = XlSheetVisibility.xlSheetVisible
            ' wshWorksheet.Activate()



            Dim fil As Integer = 8

            Dim acIndicadorDerecha As Integer = 0
            Dim acIndicadorIzquierda As Integer = 0

            Dim filDerecha As Integer = 8

            Dim nombreCursoTemp As String = ""
            Dim contadorIndicador As Integer = 0
            Dim filaCount As Integer = 0
            Dim iniciaIndicador As Integer = 0
            Dim indiceHojas As Integer = 0
            'For iii = 0 To lst.Count - 1
            '    wbkWorkbook.Sheets.Add()
            'Next

            Dim espacioUsar As Integer = 0
            Dim espacioUsar1 As Integer = 0
            Dim esMayorIndicadores As Boolean = False


            Dim boolEstado As Boolean = True
            For Each opersonaLibreta As personaLibreta In lst
                'For i As Integer = 0 To dt_ListaAlumnos.Rows.Count - 1
                '    If dt_ListaAlumnos.Rows(i).Item(0) = opersonaLibreta.codAlumno Then
                '        'If Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item(2)) = False Then
                '        boolEstado = Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item(2))
                '        Exit For
                '        'End If
                '    End If
                'Next
                'If boolEstado = False Then
                '    If Not estadoDetenerProceso Then
                '        Exit For
                '    End If
                '    Continue For
                'End If




                fil = 8
                filDerecha = 8
                indiceHojas += 1
                ' Dim ws As New Worksheet



                Dim ws = workbook.Worksheet(indiceHojas)




                '' oSheet.Name = opersonaLibreta.codAlumno
                '    ws.Name(opersonaLibreta.codAlumno)




                '  oSheet.Activate()
                '  oSheet.Select()

                Dim filasTemp As Integer = 0
                Dim sumasIndicador As Integer = 0
                ''

                '    excel.ActiveWindow.Zoom = 75


                'With excel.ActiveSheet.PageSetup
                '    .LeftHeader = ""
                '    .CenterHeader = ""
                '    .RightHeader = ""
                '    .LeftFooter = ""
                '    .CenterFooter = ""
                '    .RightFooter = ""
                '    .LeftMargin = excel.Application.InchesToPoints(0.7)
                '    .RightMargin = excel.Application.InchesToPoints(0.7)
                '    .TopMargin = excel.Application.InchesToPoints(0.75)
                '    .BottomMargin = excel.Application.InchesToPoints(0.75)
                '    .HeaderMargin = excel.Application.InchesToPoints(0.3)
                '    .FooterMargin = excel.Application.InchesToPoints(0.3)
                '    .PrintHeadings = False
                '    .PrintGridlines = False
                '    '.PrintComments = xlPrintNoComments
                '    .PrintQuality = 600
                '    .CenterHorizontally = False
                '    .CenterVertically = False
                '    .Orientation = 1
                '    .Draft = False
                '    '.PaperSize = xlPaperLetter
                '    '.FirstPageNumber = xlAutomatic
                '    '.Order = OrderedDictionary xlDownThenOver
                '    .BlackAndWhite = False
                '    .Zoom = False
                '    .FitToPagesWide = 1
                '    .FitToPagesTall = False
                '    '.PrintErrors = xlPrintErrorsDisplayed
                '    .OddAndEvenPagesHeaderFooter = False
                '    .DifferentFirstPageHeaderFooter = False
                '    .ScaleWithDocHeaderFooter = True
                '    .AlignMarginsHeaderFooter = True
                '    .EvenPage.LeftHeader.Text = ""
                '    .EvenPage.CenterHeader.Text = ""
                '    .EvenPage.RightHeader.Text = ""
                '    .EvenPage.LeftFooter.Text = ""
                '    .EvenPage.CenterFooter.Text = ""
                '    .EvenPage.RightFooter.Text = ""
                '    .FirstPage.LeftHeader.Text = ""
                '    .FirstPage.CenterHeader.Text = ""
                '    .FirstPage.RightHeader.Text = ""
                '    .FirstPage.LeftFooter.Text = ""
                '    .FirstPage.CenterFooter.Text = ""
                '    .FirstPage.RightFooter.Text = ""
                'End With



                ''
                'Dim str_Logo As String = "D:\Escudo_SG.jpg"
                'Dim p As Object
                'With excel.Range(excel.ActiveSheet.Cells(1, 1), excel.ActiveSheet.Cells(5, 2))
                '    .Merge()

                '    p = excel.ActiveSheet.Shapes.AddPicture(str_Logo, False, True, 30, 10, 75, 75)
                'End With
                'p = Nothing

                Dim ci As Globalization.CultureInfo = New Globalization.CultureInfo("en-US")



                ws.Range(ws.Cell(8, 1), ws.Cell(8, 3)).Merge()

                ws.Range(ws.Cell(8, 1), ws.Cell(8, 3)).Value = "SUBJECT AREAS -TERM " & abrBimestre
                ws.Range(ws.Cell(8, 1), ws.Cell(8, 3)).Style.Font.FontSize = 16
                ws.Range(ws.Cell(8, 1), ws.Cell(8, 3)).Style.Font.Bold = True

                ws.Range(ws.Cell(8, 6), ws.Cell(8, 7)).Merge()

                ws.Range(ws.Cell(8, 6), ws.Cell(8, 7)).Value = "Date : " & Date.Now.ToString("MMMM", ci) & " , " & Now().Year().ToString()
                ws.Range(ws.Cell(8, 6), ws.Cell(8, 7)).Style.Font.Bold = True


                For Each olibretaComponente As libretaComponente In opersonaLibreta.lstLibretaComponente

                    ws.Range(ws.Cell(2, 1), ws.Cell(2, 8)).Merge()
                    ws.Range(ws.Cell(2, 1), ws.Cell(2, 8)).Value = "REPORT CARD"
                    ws.Range(ws.Cell(2, 1), ws.Cell(2, 8)).Style.Font.FontSize = 20
                    ws.Range(ws.Cell(2, 1), ws.Cell(2, 8)).Style.Font.Bold = True
                    ws.Range(ws.Cell(2, 1), ws.Cell(2, 8)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                    'excel.Application.Range(ws.Cell(2, 3), ws.Cell(2, 7)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    ws.Range(ws.Cell(2, 3), ws.Cell(2, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    ws.Cell(3, 3).Value = "NAME"



                    ''ws.Cell(3, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(3, 3)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    ws.Cell(3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                    ''.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                    ws.Range(ws.Cell(3, 4), ws.Cell(3, 7)).Merge()

                    ' ws.Range(ws.Cell(3, 4), ws.Cell(3, 7)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(3, 4), ws.Cell(3, 7))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    ws.Range(ws.Cell(3, 4), ws.Cell(3, 7)).Value = opersonaLibreta.nombreAlumno
                    ws.Range(ws.Cell(3, 4), ws.Cell(3, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    ws.Cell(4, 3).Value = "CLASS"
                    ws.Cell(4, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                    '' ws.Cell(4, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                    With ws.Cell(4, 3)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    ws.Range(ws.Cell(4, 4), ws.Cell(4, 7)).Merge()
                    ' ws.Range(ws.Cell(4, 4), ws.Cell(4, 7)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous




                    With ws.Range(ws.Cell(4, 4), ws.Cell(4, 7))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    ws.Range(ws.Cell(4, 4), ws.Cell(4, 7)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                    ws.Range(ws.Cell(4, 4), ws.Cell(4, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    ws.Cell(5, 3).Value = "TUTOR"
                    ''  ws.Cell(5, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(5, 3)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With




                    ws.Cell(5, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    ws.Range(ws.Cell(5, 4), ws.Cell(5, 7)).Merge()
                    ws.Range(ws.Cell(5, 4), ws.Cell(5, 7)).Value = dst.Tables(2).Rows(0)("nombre").ToString()
                    ws.Range(ws.Cell(5, 4), ws.Cell(5, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                    ' ws.Range(ws.Cell(5, 4), ws.Cell(5, 7)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(5, 4), ws.Cell(5, 7))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    If olibretaComponente.columna Then
                        If olibretaComponente.nombreCurso <> nombreCursoTemp Then
                            fil += 1



                            ''
                            Dim cantidadInidcador As Integer = 0
                            Dim listaLibreta As IEnumerable(Of libretaComponente)
                            listaLibreta = (From h In opersonaLibreta.lstLibretaComponente Where h.nombreCurso = olibretaComponente.nombreCurso)
                            sumasIndicador = 0

                            For Each olibretaComponenteTemp As libretaComponente In listaLibreta
                                sumasIndicador += olibretaComponenteTemp.lstIndicador.Count()
                            Next
                            '






                            Dim espacioSobra As Integer = 0




                            If fil < 42 Then
                                espacioSobra = 41 - fil

                                If espacioSobra < sumasIndicador And sumasIndicador < 32 Then
                                    fil += (espacioSobra) + 2 ' + 12

                                End If

                            ElseIf fil < 81 And fil > 41 Then
                                espacioSobra = 80 - fil

                                If espacioSobra < sumasIndicador And sumasIndicador < 32 Then
                                    fil += (espacioSobra) + 2 '+ 12

                                End If
                            ElseIf fil > 80 And fil < 121 Then
                                espacioSobra = 120 - fil

                                If espacioSobra < sumasIndicador And sumasIndicador < 32 Then
                                    fil += (espacioSobra) + 2 '+ 12

                                End If
                            End If



                            If sumasIndicador > 34 Then

                                espacioUsar = (fil + 33)

                                espacioUsar1 = (fil + 33) + 1

                                ws.Range(ws.Cell(espacioUsar, 1), ws.Cell(espacioUsar, 4)).Style.Border.BottomBorder = XLBorderStyleValues.Thin

                                esMayorIndicadores = True


                            Else
                                ws.Range(ws.Cell((fil + sumasIndicador), 1), ws.Cell(fil + sumasIndicador, 3)).Style.Border.BottomBorder = XLBorderStyleValues.Thin


                            End If




                            ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).RowHeight = 25

                            'filasInicioComentario = fil
                            'contadorFilasCurso = 0
                            ''



                            ''acIndicadorIzquierda = olibretaComponente.lstIndicador.Count
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Merge()
                            ''excel.Application.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = olibretaComponente.nombreCurso.ToUpper()
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = olibretaComponente.nombreCurso.ToUpper()
                            '' ws.Cell(fil, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                            With ws.Cell(fil, 4)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With


                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top

                            ''devolver
                            'ws.Cell(fil, 1).IndentLevel = 3



                            ws.Cell(fil, 1).Style.Font.Bold = True
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Border.TopBorder = XLBorderStyleValues.Thin
                            ''


                            ''
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.FontSize = 16
                            'devolver
                            ''ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).RowHeight = 25

                            ws.Rows(fil).Height = 25

                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Border.LeftBorder = XLBorderStyleValues.Thin






                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Border.RightBorder = XLBorderStyleValues.Thin


                        End If

                        For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador





                            filasTemp = fil
                            fil += 1

                            If esMayorIndicadores Then

                                If fil = espacioUsar1 Then
                                    ''
                                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Merge()
                                    ''excel.Application.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = olibretaComponente.nombreCurso.ToUpper()
                                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = olibretaComponente.nombreCurso.ToUpper()
                                    ''

                                    ''
                                    ' ws.Cell(fil, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                                    With ws.Cell(fil, 4)
                                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                                    End With




                                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top

                                    ''dev
                                    ' ws.Cell(fil, 1).IndentLevel = 3


                                    ws.Cell(fil, 1).Style.Font.Bold = True


                                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Border.TopBorder = XLBorderStyleValues.Thin




                                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Border.LeftBorder = XLBorderStyleValues.Thin


                                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.FontSize = 16

                                    'dev
                                    'ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).RowHeight = 25



                                    ws.Rows(fil).Height = 25
                                    ''
                                    ''
                                    fil += 1
                                    esMayorIndicadores = False
                                End If

                            End If



                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Merge()
                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Value = olibretaIndicador.nombreIndicador

                            'dev
                            ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).RowHeight = 25
                            ws.Rows(fil).Height = 25

                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.FontSize = 8


                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.FontName = "Arial"



                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top

                            ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Alignment.WrapText = True

                            With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3))
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin

                            End With
                            ws.Cell(fil, 4).Value = olibretaIndicador.notaIndicador.ToUpper()
                            ws.Cell(fil, 4).Style.Font.Bold = True

                            '' ws.Cell(fil, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                            With ws.Cell(fil, 4)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With

                            ws.Cell(fil, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            ws.Cell(fil, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top




                        Next

                        'ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 4)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        nombreCursoTemp = olibretaComponente.nombreCurso


                    Else
                        If olibretaComponente.nombreCurso <> nombreCursoTemp Then
                            filDerecha += 1

                            ''
                            Dim cantidadInidcador As Integer = 0
                            Dim listaLibreta As IEnumerable(Of libretaComponente)
                            listaLibreta = (From h In opersonaLibreta.lstLibretaComponente Where h.nombreCurso = olibretaComponente.nombreCurso)
                            sumasIndicador = 0
                            For Each olibretaComponenteTemp As libretaComponente In listaLibreta
                                sumasIndicador += olibretaComponenteTemp.lstIndicador.Count()
                            Next


                            Dim espacioSobra As Integer = 0
                            If filDerecha < 42 Then
                                espacioSobra = 41 - filDerecha

                                If espacioSobra < sumasIndicador And sumasIndicador < 32 Then
                                    filDerecha += (espacioSobra) + 2 ' + 12

                                End If

                            ElseIf filDerecha < 81 And filDerecha > 41 Then
                                espacioSobra = 80 - filDerecha

                                If espacioSobra < sumasIndicador And sumasIndicador < 32 Then
                                    filDerecha += (espacioSobra) + 2 '+ 12

                                End If
                            ElseIf filDerecha > 80 And filDerecha < 121 Then
                                espacioSobra = 120 - filDerecha

                                If espacioSobra < sumasIndicador And sumasIndicador < 32 Then
                                    filDerecha += (espacioSobra) + 2 '+ 12

                                End If
                            End If
                            ''


                            'If sumasIndicador > 32 Then


                            '    espacioUsar = (fil + 32)

                            '    espacioUsar1 = (fil + 32) + 1

                            '   ws.Range(ws.Cell(espacioUsar, 1), ws.Cell(espacioUsar, 4)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                            '    esMayorIndicadores = True
                            'Else
                            '   ws.Range(ws.Cell((fil + sumasIndicador), 1), ws.Cell(fil + sumasIndicador, 3)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                            'End If






                            ws.Range(ws.Cell((filDerecha + sumasIndicador), 5), ws.Cell(filDerecha + sumasIndicador, 8)).Style.Border.BottomBorder = XLBorderStyleValues.Thin



                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Merge()
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Value = olibretaComponente.nombreCurso.ToUpper()

                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Font.FontSize = 16

                            'dev
                            '  ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).RowHeight = 25
                            ws.Rows(filDerecha).Height = 25

                            ''.Height = 30;




                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top

                            'dev
                            'ws.Cell(filDerecha, 5).IndentLevel = 3
                            ws.Cell(filDerecha, 5).Style.Font.Bold = True
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Border.TopBorder = XLBorderStyleValues.Thin

                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Border.LeftBorder = XLBorderStyleValues.Thin

                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Border.RightBorder = XLBorderStyleValues.Thin






                            ' ws.Cell(filDerecha, 8).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                            With ws.Cell(filDerecha, 8)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With



                            ''ws.Range(ws.Cell(fil + 5, 2), ws.Cell(fil + 5, 3)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        End If

                        For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador
                            filasTemp = filDerecha
                            filDerecha += 1
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Merge()
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Value = olibretaIndicador.nombreIndicador

                            'dev
                            'ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).RowHeight = 25

                            ws.Rows(filDerecha).Height = 25


                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Font.FontSize = 8
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Font.FontName = "Arial"



                            'excel.Application.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Style.Font.FontSize = 8
                            'excel.Application.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Font.Name = "Arial"
                            ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Style.Alignment.WrapText = True
                            With ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7))
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With









                            ws.Cell(filDerecha, 8).Value = olibretaIndicador.notaIndicador.ToUpper()
                            ws.Cell(filDerecha, 8).Style.Font.Bold = True



                            ''ws.Cell(filDerecha, 8).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            With ws.Cell(filDerecha, 8)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With


                            ws.Cell(filDerecha, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            ''cambiar
                            ''ws.Cell(filDerecha, 8).RowHeight = 25

                            ws.Rows(filDerecha).Height = 25


                            ''ws.Cell(filDerecha, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                            '' VerticalAlignment = xlVAlignCenter

                            ''   ws.Cell(filDerecha, 8).a = Microsoft.Office.Interop.Excel.Constants.xlTop
                        Next


                        'ws.Range(ws.Cell(fil + 10, 5), ws.Cell(fil + 10, 7)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        'ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 8)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                        nombreCursoTemp = olibretaComponente.nombreCurso

                        'excel.Application.Range(ws.Cell((filasTemp + sumasIndicador - 2), 5), ws.Cell(filasTemp + sumasIndicador, 8)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    End If


                Next





                ''cambiado
                ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'excel.Application.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                ''


                If filDerecha > fil Then
                    filDerecha -= 2

                    ws.Range(ws.Cell(filDerecha + 4, 1), ws.Cell(filDerecha + 4, 3)).Merge()
                    ws.Range(ws.Cell(filDerecha + 4, 1), ws.Cell(filDerecha + 4, 3)).Value = "CONDUCTA"
                    'ws.Range(ws.Cell(filDerecha + 4, 1), ws.Cell(filDerecha + 4, 3)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                    With ws.Range(ws.Cell(filDerecha + 4, 1), ws.Cell(filDerecha + 4, 3))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    ws.Cell(filDerecha + 4, 4).Value = opersonaLibreta.conductaBimestral
                    ws.Cell(filDerecha + 4, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                    ''ws.Cell(filDerecha + 4, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    ''

                    With ws.Cell(filDerecha + 4, 4)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With




                    If filDerecha + 5 = 80 Then
                        filDerecha += 3
                    End If
                    ws.Range(ws.Cell(filDerecha + 5, 1), ws.Cell(filDerecha + 5, 3)).Merge()

                    ws.Range(ws.Cell(filDerecha + 5, 1), ws.Cell(filDerecha + 5, 3)).Value = "Comentario de la tutora"


                    ws.Range(ws.Cell(filDerecha + 5, 1), ws.Cell(filDerecha + 5, 3)).Style.Font.Bold = True
                    For Each olibretaComponenteT As libretaComponente In opersonaLibreta.lstLibretaComponente
                        If olibretaComponenteT.observacionCurso <> "" And olibretaComponenteT.observacionCurso.Trim().Length >= 5 Then
                            ws.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 7, 6)).Merge()

                            ws.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 7, 6)).Value = olibretaComponenteT.observacionCurso

                            '' ws.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 7, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                            With ws.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 7, 6))
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With



                            'excel.Application.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 7, 6)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                            'excel.Application.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 7, 6)).Style.Alignment.WrapText = True
                            'excel.Application.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 10, 8)).Merge()
                            'excel.Application.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 10, 8)).Style.Alignment.WrapText = True
                            'excel.Application.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 10, 8)).Merge()
                            ''excel.Application.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 7, 6)).Value = olibretaComponenteT.observacionCurso

                            'excel.Application.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 10, 8)).Value = olibretaComponenteT.observacionCurso

                            'excel.Application.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 10, 8)).Style.Font.FontSize = 14

                            'excel.Application.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 10, 8)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                            'excel.Application.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 10, 8)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top



                            filDerecha += 2
                            Exit For
                        End If
                    Next
                    ''

                    ''ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    ''ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                    filDerecha += 8
                    ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha + 1, 4)).Merge()
                    ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha + 1, 4)).Value = "ABSENCES"
                    ''
                    ' ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha + 1, 4)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    With ws.Range(ws.Cell(filDerecha, 1), ws.Cell(filDerecha + 1, 4))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ws.Cell(filDerecha, 5).Value = "Justified"

                    'ws.Cell(filDerecha, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(filDerecha, 5)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ws.Cell(filDerecha, 6).Value = "Not justified"
                    ''ws.Cell(filDerecha, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(filDerecha, 6)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    ' ws.Cell(filDerecha + 1, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(filDerecha + 1, 5)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ''ws.Cell(filDerecha + 1, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    With ws.Cell(filDerecha + 1, 6)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With




                    ws.Range(ws.Cell(filDerecha + 2, 1), ws.Cell(filDerecha + 2, 4)).Merge()

                    ws.Range(ws.Cell(filDerecha + 2, 1), ws.Cell(filDerecha + 2, 4)).Value = "Lateness"
                    ' ws.Range(ws.Cell(filDerecha + 2, 1), ws.Cell(filDerecha + 2, 4)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(filDerecha + 2, 1), ws.Cell(filDerecha + 2, 4))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Merge()

                    ' ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With




                    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                        If opersonaLibreta.codAlumno = filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then
                            ws.Cell(filDerecha + 1, 5).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                            ws.Cell(filDerecha + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            ws.Cell(filDerecha + 1, 6).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                            ws.Cell(filDerecha + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                            ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                            ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            Exit For
                        End If
                        'CodigoAlumno	NombreAlumno	1TardanzaJustificada	1TardanzaSinJustificar	1FaltaJustificada	1FaltaSinJustificar	2TardanzaJustificada	2TardanzaSinJustificar	2FaltaJustificada	2FaltaSinJustificar	3TardanzaJustificada	3TardanzaSinJustificar	3FaltaJustificada	3FaltaSinJustificar	4TardanzaJustificada	4TardanzaSinJustificar	4FaltaJustificada	4FaltaSinJustificar
                        '20090135	INGA BARRERA, Enzo Jesús	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0


                    Next




                    'ws.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 6, 3)).Merge()

                    'ws.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 6, 3)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous




                    ''
                    ws.Range(ws.Cell(filDerecha + 9, 2), ws.Cell(filDerecha + 9, 3)).Merge()
                    ws.Range(ws.Cell(filDerecha + 9, 2), ws.Cell(filDerecha + 9, 3)).Style.Border.TopBorder = XLBorderStyleValues.Thin



                    ws.Range(ws.Cell(filDerecha + 9, 2), ws.Cell(filDerecha + 9, 3)).Value = "TUTORA"




                    ws.Range(ws.Cell(filDerecha + 9, 2), ws.Cell(filDerecha + 9, 3)).Style.Font.Bold = True
                    ''.Style.Font.Bold = True

                    ws.Range(ws.Cell(filDerecha + 9, 2), ws.Cell(filDerecha + 9, 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ws.Range(ws.Cell(filDerecha + 9, 5), ws.Cell(filDerecha + 9, 7)).Merge()
                    ws.Range(ws.Cell(filDerecha + 9, 5), ws.Cell(filDerecha + 9, 7)).Style.Border.TopBorder = XLBorderStyleValues.Thin



                    ws.Range(ws.Cell(filDerecha + 9, 5), ws.Cell(filDerecha + 9, 7)).Value = "PARENTS"


                    ws.Range(ws.Cell(filDerecha + 9, 5), ws.Cell(filDerecha + 9, 7)).Style.Font.Bold = True
                    ''.Style.Font.Bold = True
                    ws.Range(ws.Cell(filDerecha + 9, 5), ws.Cell(filDerecha + 9, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ''
                    ''
                    ws.Range(ws.Cell(filDerecha + 10, 2), ws.Cell(filDerecha + 10, 3)).Merge()
                    ws.Range(ws.Cell(filDerecha + 10, 2), ws.Cell(filDerecha + 10, 3)).Value = "AD:Archieved with Distinction / Actuación destacada"
                    ws.Range(ws.Cell(filDerecha + 11, 2), ws.Cell(filDerecha + 11, 3)).Merge()
                    ws.Range(ws.Cell(filDerecha + 11, 2), ws.Cell(filDerecha + 11, 3)).Value = "A:Archieved  / Aprobado"
                    ''
                    ws.Range(ws.Cell(filDerecha + 10, 5), ws.Cell(filDerecha + 10, 7)).Merge()
                    ws.Range(ws.Cell(filDerecha + 10, 5), ws.Cell(filDerecha + 10, 7)).Value = "B:Needs inprovement/ Bases en  Proceso / Desaprobado"
                    ws.Range(ws.Cell(filDerecha + 11, 5), ws.Cell(filDerecha + 11, 7)).Merge()
                    ws.Range(ws.Cell(filDerecha + 11, 5), ws.Cell(filDerecha + 11, 7)).Value = "Initial Stage  / Calificado Insuficiente / Desaprobado"
                    ''


                Else
                    fil -= 2
                    ws.Range(ws.Cell(fil + 4, 1), ws.Cell(fil + 4, 3)).Merge()
                    ws.Range(ws.Cell(fil + 4, 1), ws.Cell(fil + 4, 3)).Value = "CONDUCTA"
                    ''ws.Range(ws.Cell(fil + 4, 1), ws.Cell(fil + 4, 3)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                    With ws.Range(ws.Cell(fil + 4, 1), ws.Cell(fil + 4, 3))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With







                    ws.Cell(fil + 4, 4).Value = opersonaLibreta.conductaBimestral

                    ws.Cell(fil + 4, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    '' ws.Cell(fil + 4, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(fil + 4, 4)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    If fil + 5 = 80 Then
                        fil += 3
                    End If
                    ws.Range(ws.Cell(fil + 5, 1), ws.Cell(fil + 5, 6)).Merge()

                    ws.Range(ws.Cell(fil + 5, 1), ws.Cell(fil + 5, 6)).Value = "Comentario de la tutora"

                    ws.Range(ws.Cell(fil + 5, 1), ws.Cell(fil + 5, 6)).Style.Font.Bold = True

                    ws.Range(ws.Cell(fil + 5, 1), ws.Cell(fil + 5, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    'des
                    ' ws.Range(ws.Cell(fil + 5, 1), ws.Cell(fil + 5, 6)).Style.Alignment.Indent = 2

                    ''Style.Alignment.Indent = 2;

                    For Each olibretaComponenteT As libretaComponente In opersonaLibreta.lstLibretaComponente

                        If olibretaComponenteT.observacionCurso <> "" And olibretaComponenteT.observacionCurso.Trim().Length >= 5 Then

                            ''ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 6, 3)).Merge()
                            ''excel.Application.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 6, 3)).Value = olibretaComponenteT.observacionCurso
                            ''ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 6, 3)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Merge()


                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Style.Alignment.WrapText = True

                            ''.Style.Alignment.Style.Alignment.WrapText = True
                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Merge()
                            'excel.Application.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 7, 6)).Value = olibretaComponenteT.observacionCurso
                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Value = olibretaComponenteT.observacionCurso


                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Style.Font.FontSize = 14




                            '  ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                            With ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8))
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With



                            ws.Range(ws.Cell(fil + 6, 1), ws.Cell(fil + 10, 8)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top

                            ''   .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top



                            fil += 4
                            Exit For

                            ''
                        End If

                    Next


                    '' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 3)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    ''  ws.Range(ws.Cell(filDerecha, 5), ws.Cell(filDerecha, 7)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                    fil += 8
                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 1, 4)).Merge()

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 1, 4)).Value = "ABSENCES"

                    'ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 1, 4)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 1, 4))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    ws.Cell(fil, 5).Value = "Justified"
                    ' ws.Cell(fil, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(fil, 5)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    ws.Cell(fil, 6).Value = "Not justified"
                    'ws.Cell(fil, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(fil, 6)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    '' ws.Cell(fil + 1, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(fil + 1, 5)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    '' ws.Cell(fil + 1, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    With ws.Cell(fil + 1, 6)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With




                    ws.Range(ws.Cell(fil + 2, 1), ws.Cell(fil + 2, 4)).Merge()

                    ws.Range(ws.Cell(fil + 2, 1), ws.Cell(fil + 2, 4)).Value = "Lateness"
                    ''   ws.Range(ws.Cell(fil + 2, 1), ws.Cell(fil + 2, 4)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(fil + 2, 1), ws.Cell(fil + 2, 4))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Merge()

                    ''ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    'For Each filt As System.Data.DataRow In dt_ausencias.Rows


                    '    If filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then

                    '        ws.Cell(fil + 1, 4).Value = "p1" '' Convert.ToInt32(filt("1FaltaJustificada").ToString()) + Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                    '        ws.Cell(fil + 1, 5).Value = "p1" '' Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                    '       ws.Range(ws.Cell(fil + 2, 4), ws.Cell(filDerecha + 2, 5)).Value = "p1" '' Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                    '        ws.Cell(fil + 1, 4).Value = "prueba"
                    '        Exit For

                    '    End If




                    'Next



                    'excel.Application.Range(ws.Cell(filDerecha + 6, 1), ws.Cell(filDerecha + 10, 8)).Merge()


                    ws.Range(ws.Cell(fil + 7, 2), ws.Cell(fil + 7, 3)).Merge()
                    ws.Range(ws.Cell(fil + 7, 2), ws.Cell(fil + 7, 3)).Style.Border.TopBorder = XLBorderStyleValues.Thin




                    ws.Range(ws.Cell(fil + 7, 2), ws.Cell(fil + 7, 3)).Value = "TUTORA"
                    ws.Range(ws.Cell(fil + 7, 2), ws.Cell(fil + 7, 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ws.Range(ws.Cell(fil + 7, 5), ws.Cell(fil + 7, 7)).Merge()


                    ws.Range(ws.Cell(fil + 7, 5), ws.Cell(fil + 7, 7)).Style.Border.TopBorder = XLBorderStyleValues.Thin




                    ws.Range(ws.Cell(fil + 7, 5), ws.Cell(fil + 7, 7)).Value = "PARENTS"
                    ws.Range(ws.Cell(fil + 7, 5), ws.Cell(fil + 7, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                    ''
                    ws.Range(ws.Cell(fil + 8, 2), ws.Cell(fil + 8, 3)).Merge()
                    ws.Range(ws.Cell(fil + 8, 2), ws.Cell(fil + 8, 3)).Value = "AD:Archieved with Distinction / Actuación destacada"
                    ws.Range(ws.Cell(fil + 9, 2), ws.Cell(fil + 9, 3)).Merge()
                    ws.Range(ws.Cell(fil + 9, 2), ws.Cell(fil + 9, 3)).Value = "A:Archieved  / Aprobado"
                    ''
                    ws.Range(ws.Cell(fil + 8, 5), ws.Cell(fil + 8, 7)).Merge()
                    ws.Range(ws.Cell(fil + 8, 5), ws.Cell(fil + 8, 7)).Value = "B:Needs inprovement/ Bases en  Proceso / Desaprobado"
                    ws.Range(ws.Cell(fil + 9, 5), ws.Cell(fil + 9, 7)).Merge()
                    ws.Range(ws.Cell(fil + 9, 5), ws.Cell(fil + 9, 7)).Value = "Initial Stage  / Calificado Insuficiente / Desaprobado"
                    ''

                End If


                If filDerecha > fil Then
                    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                        If opersonaLibreta.codAlumno = filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then
                            If CodBimestre = 1 Then
                                ws.Cell(filDerecha + 1, 5).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(filDerecha + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(filDerecha + 1, 6).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) '+ Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(filDerecha + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            If CodBimestre = 2 Then
                                ws.Cell(filDerecha + 1, 5).Value = Convert.ToInt32(filt("2FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(filDerecha + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(filDerecha + 1, 6).Value = Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) '+ Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(filDerecha + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Value = Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            If CodBimestre = 3 Then
                                ws.Cell(filDerecha + 1, 5).Value = Convert.ToInt32(filt("3FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(filDerecha + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(filDerecha + 1, 6).Value = Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) ' + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(filDerecha + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Value = Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If

                            If CodBimestre = 4 Then
                                ws.Cell(filDerecha + 1, 5).Value = Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(filDerecha + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(filDerecha + 1, 6).Value = Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(filDerecha + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Value = Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(filDerecha + 2, 5), ws.Cell(filDerecha + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If


                            Exit For
                        End If
                    Next
                Else
                    For Each filt As System.Data.DataRow In dt_ausencias.Rows
                        If filt("CodigoAlumno").ToString() = opersonaLibreta.codAlumno Then

                            If CodBimestre = 1 Then
                                ws.Cell(fil + 1, 5).Value = Convert.ToInt32(filt("1FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("2FaltaJustificada").ToString()) + Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(fil + 1, 6).Value = Convert.ToInt32(filt("1FaltaSinJustificar").ToString()) '+ Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Value = Convert.ToInt32(filt("1TardanzaJustificada").ToString()) + Convert.ToInt32(filt("1TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            If CodBimestre = 2 Then
                                ws.Cell(fil + 1, 5).Value = Convert.ToInt32(filt("2FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("3FaltaJustificada").ToString()) + Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(fil + 1, 6).Value = Convert.ToInt32(filt("2FaltaSinJustificar").ToString()) '+ Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Value = Convert.ToInt32(filt("2TardanzaJustificada").ToString()) + Convert.ToInt32(filt("2TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If
                            If CodBimestre = 3 Then
                                ws.Cell(fil + 1, 5).Value = Convert.ToInt32(filt("3FaltaJustificada").ToString()) '+ Convert.ToInt32(filt("4FaltaJustificada").ToString())
                                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(fil + 1, 6).Value = Convert.ToInt32(filt("3FaltaSinJustificar").ToString()) ' + Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Value = Convert.ToInt32(filt("3TardanzaJustificada").ToString()) + Convert.ToInt32(filt("3TardanzaSinJustificar").ToString()) '+ Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If

                            If CodBimestre = 4 Then
                                ws.Cell(fil + 1, 5).Value = Convert.ToInt32(filt("4FaltaJustificada").ToString())

                                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                                ws.Cell(fil + 1, 6).Value = Convert.ToInt32(filt("4FaltaSinJustificar").ToString())
                                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Value = Convert.ToInt32(filt("4TardanzaJustificada").ToString()) + Convert.ToInt32(filt("4TardanzaSinJustificar").ToString())
                                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            End If

                            Exit For
                        End If
                    Next
                End If


                With ws


                    ws.PageSetup.AdjustTo(60)
                    'ws.PageSetup.PageOrientation = ClosedXML.Excel.XLPageOrientation.Landscape
                    ws.PageSetup.Margins.Top = 0.75 '1.9
                    ws.PageSetup.Margins.Bottom = 0.75 '1.9
                    ws.PageSetup.Margins.Left = 0.7 '0.6
                    ws.PageSetup.Margins.Right = 0.7 '0.6
                    ws.PageSetup.Margins.Header = 0.3 '0.8
                    ws.PageSetup.Margins.Footer = 0.3 '0.8
                    ws.PageSetup.PagesWide = 1

                    ' .LeftMargin = Excel.Application.InchesToPoints(0.7)
                    '    .RightMargin = excel.Application.InchesToPoints(0.7)
                    '    .TopMargin = excel.Application.InchesToPoints(0.75)
                    '    .BottomMargin = excel.Application.InchesToPoints(0.75)
                    '    .HeaderMargin = excel.Application.InchesToPoints(0.3)
                    '    .FooterMargin = excel.Application.InchesToPoints(0.3)


                End With
                ws.Column(4).Width = 4
                ws.Column(8).Width = 4
                ws.Column(3).Width = 24
                ws.Column(7).Width = 30
                ws.Column(5).Width = 8
                ws.Column(6).Width = 13
                ws.Column(3).Width = 41

                'Excel.ActiveSheet.Cells.Columns(4).ColumnWidth = 4
                'Excel.ActiveSheet.Cells.Columns(8).ColumnWidth = 4
                'Excel.ActiveSheet.Cells.Columns(3).ColumnWidth = 24
                'Excel.ActiveSheet.Cells.Columns(7).ColumnWidth = 30
                'Excel.ActiveSheet.Cells.Columns(5).ColumnWidth = 8
                'Excel.ActiveSheet.Cells.Columns(6).ColumnWidth = 13
                'Excel.ActiveSheet.Cells.Columns(3).ColumnWidth = 41
                'Excel.ActiveWindow.Zoom = 75


                'alumnosProcesado += 1
                'If Not estadoDetenerProceso Then

                '    Exit For
                'End If

            Next
            Dim rutaExplorer As String = ""

           
         



            ''


            '.LeftMargin = Application.InchesToPoints(0.7)
            '.RightMargin = Application.InchesToPoints(0.7)
            '.TopMargin = Application.InchesToPoints(0.75)
            '.BottomMargin = Application.InchesToPoints(0.75)
            '.HeaderMargin = Application.InchesToPoints(0.3)
            '.FooterMargin = Application.InchesToPoints(0.3)

            'With excel.ActiveSheet.PageSetup
            '    .LeftHeader = ""
            '    .CenterHeader = ""
            '    .RightHeader = ""
            '    .LeftFooter = ""
            '    .CenterFooter = ""
            '    .RightFooter = ""
            '    .LeftMargin = excel.Application.InchesToPoints(0.7)
            '    .RightMargin = excel.Application.InchesToPoints(0.7)
            '    .TopMargin = excel.Application.InchesToPoints(0.75)
            '    .BottomMargin = excel.Application.InchesToPoints(0.75)
            '    .HeaderMargin = excel.Application.InchesToPoints(0.3)
            '    .FooterMargin = excel.Application.InchesToPoints(0.3)
            '    .PrintHeadings = False
            '    .PrintGridlines = False
            '    '.PrintComments = xlPrintNoComments
            '    .PrintQuality = 600
            '    .CenterHorizontally = False
            '    .CenterVertically = False
            '    .Orientation = 1
            '    .Draft = False
            '    '.PaperSize = xlPaperLetter
            '    '.FirstPageNumber = xlAutomatic
            '    '.Order = OrderedDictionary xlDownThenOver
            '    .BlackAndWhite = False
            '    .Zoom = False
            '    .FitToPagesWide = 1
            '    .FitToPagesTall = False
            '    '.PrintErrors = xlPrintErrorsDisplayed
            '    .OddAndEvenPagesHeaderFooter = False
            '    .DifferentFirstPageHeaderFooter = False
            '    .ScaleWithDocHeaderFooter = True
            '    .AlignMarginsHeaderFooter = True
            '    .EvenPage.LeftHeader.Text = ""
            '    .EvenPage.CenterHeader.Text = ""
            '    .EvenPage.RightHeader.Text = ""
            '    .EvenPage.LeftFooter.Text = ""
            '    .EvenPage.CenterFooter.Text = ""
            '    .EvenPage.RightFooter.Text = ""
            '    .FirstPage.LeftHeader.Text = ""
            '    .FirstPage.CenterHeader.Text = ""
            '    .FirstPage.RightHeader.Text = ""
            '    .FirstPage.LeftFooter.Text = ""
            '    .FirstPage.CenterFooter.Text = ""
            '    .FirstPage.RightFooter.Text = ""
            'End With




            '.CenterHeader = ""
            '.RightHeader = ""
            '.LeftFooter = ""
            '.CenterFooter = ""
            '.RightFooter = ""
            '.LeftMargin = Application.InchesToPoints(0.7)
            '.RightMargin = Application.InchesToPoints(0.7)
            '.TopMargin = Application.InchesToPoints(0.75)
            '.BottomMargin = Application.InchesToPoints(0.75)
            '.HeaderMargin = Application.InchesToPoints(0.3)
            '.FooterMargin = Application.InchesToPoints(0.3)
            '.PrintHeadings = False
            '.PrintGridlines = False
            '.PrintComments = xlPrintNoComments
            '.PrintQuality = 600
            '.CenterHorizontally = False
            '.CenterVertically = False
            '.Orientation = xlPortrait
            '.Draft = False
            '.PaperSize = xlPaperLetter
            '.FirstPageNumber = xlAutomatic
            '.Order = xlDownThenOver
            '.BlackAndWhite = False
            '.Zoom = False
            '.FitToPagesWide = 1
            '.FitToPagesTall = 0
            '.PrintErrors = xlPrintErrorsDisplayed
            '.OddAndEvenPagesHeaderFooter = False
            '.DifferentFirstPageHeaderFooter = False
            '.ScaleWithDocHeaderFooter = True
            '.AlignMarginsHeaderFooter = True
            '.EvenPage.LeftHeader.Text = ""
            '.EvenPage.CenterHeader.Text = ""
            '.EvenPage.RightHeader.Text = ""
            '.EvenPage.LeftFooter.Text = ""
            '.EvenPage.CenterFooter.Text = ""
            '.EvenPage.RightFooter.Text = ""
            '.FirstPage.LeftHeader.Text = ""
            '.FirstPage.CenterHeader.Text = ""
            '.FirstPage.RightHeader.Text = ""
            '.FirstPage.LeftFooter.Text = ""
            '.FirstPage.CenterFooter.Text = ""
            '.FirstPage.RightFooter.Text = ""


            ''
            workbook.Save()

            ''
            Return rutaREpositorioTemporales

            'estadoOperacion = True
            'rutaExplorer = nombreArchivo '' wbkWorkbook.Path()

            'EiminaReferencias(wshWorksheet)
            'EiminaReferencias(wbkWorkbook)
            'excel.Quit()
            'EiminaReferencias(excel)
            'System.GC.Collect()

            '' Return rutaExplorer

        Catch ex As Exception

        Finally


        End Try
    End Function

    ''
    ''



#End Region

#Region "Libreta primaria"
    ''
    Function crearLibretaPrimaria1(ByVal codigoAula As Integer, ByVal int_bimestre As Integer, ByVal codAlumno As String) As String
        '     <add key="LibretaPrimaria"  value="\Plantillas\ExportacionLibreta\libretaPrimaria.xlsx"/>
        '<add key="LibretaInicial" value="\Plantillas\ExportacionLibreta\libretaInicial.xlsx"/>

        ''   <add key="Temporales" value="\Temporales\"/>



        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaLibretaPrimaria")
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
        Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
        File.Copy(rutaPlantillas, rutaREpositorioTemporales)



        Dim tb_Asistencias As New System.Data.DataTable '' extraer las inasistencias del alumno
        Dim tb_demeritos As New System.Data.DataTable '' extrar los meritos y demeritos del alumno 
        Dim tb_conducta As New System.Data.DataTable


        Try
            ''
            Dim dt As New System.Data.DataTable
            Dim dst As New DataSet
            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaPrimaria_1(codigoAula, int_bimestre, codAlumno, 1, 1, 1, 1)
            dt = dst.Tables(0)
            ''
            tb_Asistencias = dst.Tables(3)
            tb_demeritos = dst.Tables(4)
            tb_conducta = dst.Tables(5)

            ''
            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)



            '  lst.OrderBy(Function(iperosona) iperosona.nombreAlumno)
            'fun=(int m)={return 4}
            ' fun()
            'fun
            'dt_ListaAlumnos.Rows(i).Item(0) = lst(iPerosona).codAlumno Then
            ''If Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item(2)) = False Then


            'Dim Int_nueva As Integer = (From h In dt_ListaAlumnos.AsEnumerable() Where h.Field(Of Boolean)("Chk") = True Select h).ToList().Count

            'cantidadAlumnos = Int_nueva 'lst.Count










            Dim workbook As New ClosedXML.Excel.XLWorkbook(rutaREpositorioTemporales)

            workbook.CalculateMode = XLCalculateMode.Auto








            Dim fil As Integer = 8
            Dim nombreCursoTemp As String = ""
            Dim contadorIndicador As Integer = 0
            Dim filaCount As Integer = 0
            Dim iniciaIndicador As Integer = 0
            Dim indiceHojas As Integer = 0
            'For iii = 0 To lst.Count - 1
            '    wbkWorkbook.Sheets.Add()
            'Next


            ''
            Dim abrBimestre As String = ""
            If int_bimestre = 1 Then
                abrBimestre = "I"
            End If
            If int_bimestre = 2 Then
                abrBimestre = "II"
            End If
            If int_bimestre = 3 Then
                abrBimestre = "III"
            End If
            If int_bimestre = 4 Then
                abrBimestre = "IV"
            End If


            Dim ci As Globalization.CultureInfo = New Globalization.CultureInfo("en-US")


            Date.Now.ToString("MMMM", ci)
            ''


            Dim agregoFilas As Integer = 0
            Dim agregoFilas1 As Integer = 0

            Dim boolEstado As Boolean = True
            For iPerosona As Integer = 0 To lst.Count - 1

                'For i As Integer = 0 To dt_ListaAlumnos.Rows.Count - 1
                '    If dt_ListaAlumnos.Rows(i).Item(0) = lst(iPerosona).codAlumno Then
                '        'If Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item(2)) = False Then
                '        boolEstado = Convert.ToBoolean(dt_ListaAlumnos.Rows(i).Item(2))
                '        Exit For
                '        'End If
                '    End If
                'Next

                'If boolEstado = False Then
                '    If Not estadoDetenerProceso Then
                '        Exit For
                '    End If
                '    Continue For
                'End If



                agregoFilas = 0
                agregoFilas1 = 0
                fil = 8
                indiceHojas += 1

                'oSheet = wbkWorkbook.Worksheets(indiceHojas)
                ' '' oSheets = wbkWorkbook.Worksheets
                'oSheet.Activate()
                'oSheet.Select()

                ' '' oSheet.Name = ""
                'oSheet.Name = lst(iPerosona).codAlumno

                Dim ws = workbook.Worksheet(indiceHojas)



                ''

                'excel.ActiveWindow.Zoom = 75


                'With excel.ActiveSheet.PageSetup
                '    .LeftHeader = ""
                '    .CenterHeader = ""
                '    .RightHeader = ""
                '    .LeftFooter = ""
                '    .CenterFooter = ""
                '    .RightFooter = ""
                '    .LeftMargin = excel.Application.InchesToPoints(0.7)
                '    .RightMargin = excel.Application.InchesToPoints(0.7)
                '    .TopMargin = excel.Application.InchesToPoints(0.75)
                '    .BottomMargin = excel.Application.InchesToPoints(0.75)
                '    .HeaderMargin = excel.Application.InchesToPoints(0.3)
                '    .FooterMargin = excel.Application.InchesToPoints(0.3)
                '    .PrintHeadings = False
                '    .PrintGridlines = False
                '    '.PrintComments = xlPrintNoComments
                '    .PrintQuality = 600
                '    .CenterHorizontally = False
                '    .CenterVertically = False
                '    .Orientation = 1
                '    .Draft = False
                '    '.PaperSize = xlPaperLetter
                '    '.FirstPageNumber = xlAutomatic
                '    '.Order = OrderedDictionary xlDownThenOver
                '    .BlackAndWhite = False
                '    .Zoom = False
                '    .FitToPagesWide = 1
                '    .FitToPagesTall = False
                '    '.PrintErrors = xlPrintErrorsDisplayed
                '    .OddAndEvenPagesHeaderFooter = False
                '    .DifferentFirstPageHeaderFooter = False
                '    .ScaleWithDocHeaderFooter = True
                '    .AlignMarginsHeaderFooter = True
                '    .EvenPage.LeftHeader.Text = ""
                '    .EvenPage.CenterHeader.Text = ""
                '    .EvenPage.RightHeader.Text = ""
                '    .EvenPage.LeftFooter.Text = ""
                '    .EvenPage.CenterFooter.Text = ""
                '    .EvenPage.RightFooter.Text = ""
                '    .FirstPage.LeftHeader.Text = ""
                '    .FirstPage.CenterHeader.Text = ""
                '    .FirstPage.RightHeader.Text = ""
                '    .FirstPage.LeftFooter.Text = ""
                '    .FirstPage.CenterFooter.Text = ""
                '    .FirstPage.RightFooter.Text = ""
                'End With
                ''


                ws.Range(ws.Cell(8, 1), ws.Cell(8, 2)).Merge()
                ws.Range(ws.Cell(8, 1), ws.Cell(8, 2)).Value = "SUBJECT AREAS -" & Now().Year().ToString()
                ws.Range(ws.Cell(8, 1), ws.Cell(8, 2)).Style.Font.Bold = True
                ws.Range(ws.Cell(8, 1), ws.Cell(8, 2)).Style.Font.FontSize = 16

                ws.Cell(8, 11).Value = "TERM " & abrBimestre

                ws.Cell(8, 11).Style.Font.Bold = True

                ws.Range(ws.Cell(4, 9), ws.Cell(4, 10)).Merge()
                ws.Range(ws.Cell(4, 9), ws.Cell(4, 10)).Value = "Date : " & Date.Now.ToString("MMMM", ci) & " " & Date.Now.Day.ToString() & " ," & Date.Now.Year().ToString
                ws.Range(ws.Cell(4, 9), ws.Cell(4, 10)).Style.Font.Bold = True

                ws.Range(ws.Cell(2, 3), ws.Cell(2, 7)).Merge()
                ws.Range(ws.Cell(2, 3), ws.Cell(2, 7)).Value = "REPORT CARD"

                ws.Range(ws.Cell(2, 3), ws.Cell(2, 7)).Style.Font.Bold = True

                ' ws.Range(ws.Cell(2, 3), ws.Cell(2, 7)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                ws.Range(ws.Cell(2, 3), ws.Cell(2, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ws.Cell(3, 3).Value = "NAME"

                'ws.Cell(3, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(3, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                '.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left 

                ws.Range(ws.Cell(3, 4), ws.Cell(3, 7)).Merge()

                'ws.Range(ws.Cell(3, 4), ws.Cell(3, 7)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Range(ws.Cell(3, 4), ws.Cell(3, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Range(ws.Cell(3, 4), ws.Cell(3, 7)).Value = lst(iPerosona).nombreAlumno

                ws.Range(ws.Cell(3, 4), ws.Cell(3, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                ws.Cell(4, 3).Value = "CLASS"
                ws.Cell(4, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                '  ws.Cell(4, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(4, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Range(ws.Cell(4, 4), ws.Cell(4, 7)).Merge()
                'ws.Range(ws.Cell(4, 4), ws.Cell(4, 7)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Range(ws.Cell(4, 4), ws.Cell(4, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Range(ws.Cell(4, 4), ws.Cell(4, 7)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                ws.Range(ws.Cell(4, 4), ws.Cell(4, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                ws.Cell(5, 3).Value = "TUTOR"
                ' ws.Cell(5, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(5, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(5, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                ws.Range(ws.Cell(5, 4), ws.Cell(5, 7)).Merge()
                ws.Range(ws.Cell(5, 4), ws.Cell(5, 7)).Value = dst.Tables(2).Rows(0)("nombre").ToString()

                ws.Range(ws.Cell(5, 4), ws.Cell(5, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                'ws.Range(ws.Cell(5, 4), ws.Cell(5, 7)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Range(ws.Cell(5, 4), ws.Cell(5, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                For Each olibretaComponente As libretaComponente In lst(iPerosona).lstLibretaComponente
                    fil += 1
                    'If fil = 67 And agregoFilas = 0 Then
                    '    fil += 10
                    '    agregoFilas = 1
                    'End If

                    'If fil = 135 And agregoFilas1 = 0 Then
                    '    fil += 18
                    '    agregoFilas1 = 1
                    'End If


                    If nombreCursoTemp <> olibretaComponente.nombreCurso Then
                        Dim nombreTemp As String = ""
                        nombreTemp = olibretaComponente.nombreCurso

                        Dim count = From o In lst(iPerosona).lstLibretaComponente Where o.nombreCurso = nombreTemp _
                            Select o.lstIndicador
                        Dim cntListas As Integer = 0
                        'cntListas = count.Count()

                        Dim contador As Integer = 0

                        For Each el In count
                            contador += el.Count() + 1
                        Next

                        Dim cnt As Integer = contador + 2


                        If Not fil + cnt <= 75 And fil < 150 And agregoFilas = 0 Then
                            Dim diferencias As Integer = 0
                            diferencias = 77 - fil
                            fil += diferencias
                            agregoFilas = 1

                        End If
                        If Not fil + cnt <= 150 And fil > 75 And agregoFilas1 = 0 Then
                            Dim diferencias As Integer = 0
                            diferencias = 150 - fil
                            fil += diferencias + 3
                            agregoFilas1 = 1

                        End If

                        filaCount = fil + 1
                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Merge()
                        '  ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With




                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Value = olibretaComponente.nombreCurso.ToUpper() '' & " nombre alumno " & opersonaLibreta.nombreAlumno
                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Style.Font.Bold = True
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Merge()
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Value = "PERFORMANCE"
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 11).Value = "AVERAGE"
                        ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        ' ws.Cell(fil, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 11)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        fil = filaCount
                        ws.Cell(fil, 7).Value = "C"
                        ws.Cell(fil, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        'ws.Cell(fil, 7).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                        With ws.Cell(fil, 7)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 8).Value = "B"
                        ws.Cell(fil, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil, 8).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 8)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil, 9).Value = "A"
                        ws.Cell(fil, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil, 9).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 9)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With




                        ws.Cell(fil, 10).Value = "AD"
                        ws.Cell(fil, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil, 10).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 10)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 11).Value = olibretaComponente.promedioComponente.ToUpper()
                        ' ws.Cell(fil, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil, 11)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        fil += 1
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Merge()
                        'ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                    End If

                    'If olibretaComponente.nombreComponente = "READING" Then
                    '    fil += 5
                    'End If


                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Merge()
                    ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Value = olibretaComponente.nombreComponente.ToUpper()

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.WrapText = True

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Font.Bold = True

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    ws.Cell(fil, 11).Value = olibretaComponente.notaComponente.ToUpper()

                    ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    ' ws.Cell(fil, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Cell(fil, 11)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    iniciaIndicador = fil + 1
                    contadorIndicador = 0


                    For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador

                        fil += 1
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Merge()

                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Value = olibretaIndicador.nombreIndicador

                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top

                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.WrapText = True

                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Indent = 2


                        ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        '' .Style.Alignment.Indent = 2


                        With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With





                        If olibretaIndicador.notaIndicador.ToUpper() = "C" Then
                            ws.Cell(fil, 7).Value = " * "
                            ws.Cell(fil, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "B" Then
                            ws.Cell(fil, 8).Value = " * "
                            ws.Cell(fil, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "A" Then
                            ws.Cell(fil, 9).Value = " * "
                            ws.Cell(fil, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "AD" Then
                            ws.Cell(fil, 10).Value = " * "
                            ws.Cell(fil, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        contadorIndicador += 1


                        For ii As Integer = 7 To 10
                            ' ws.Cell(fil, ii).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                            With ws.Cell(fil, ii)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With



                        Next
                    Next

                    ws.Range(ws.Cell(iniciaIndicador, 11), ws.Cell(iniciaIndicador + contadorIndicador - 1, 11)).Merge()


                    ' ws.Range(ws.Cell(iniciaIndicador, 11), ws.Cell(iniciaIndicador + contadorIndicador - 1, 11)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(iniciaIndicador, 11), ws.Cell(iniciaIndicador + contadorIndicador - 1, 11))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    nombreCursoTemp = olibretaComponente.nombreCurso
                Next



                ''creando inasistencias del alumno 


                '' ws.Cell(iniciaIndicador, 8).
                ''

                fil += 3
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Merge()
                'ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With




                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Value = "ABSENCES"
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With




                ws.Cell(fil + 1, 1).Value = ""
                'ws.Cell(fil + 1, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 2).Value = "Term I"
                ws.Cell(fil + 1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 2)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 3).Value = "Term II"
                ws.Cell(fil + 1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 4).Value = "Term III"
                ws.Cell(fil + 1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 4)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 5).Value = "Term IV"
                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 5)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 6).Value = "Term Total / Average"
                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 1, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 6)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ws.Cell(fil + 2, 1).Value = "Justified"
                ws.Cell(fil + 2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 2, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 2, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 3, 1).Value = "Unjustified"
                ws.Cell(fil + 3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 3, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 3, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 4, 1).Value = "Lateness"
                ws.Cell(fil + 4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 4, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 4, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                For Each filaTb As System.Data.DataRow In tb_Asistencias.Rows
                    If Convert.ToInt32(filaTb("CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                        ws.Cell(fil + 2, 2).Value = filaTb("1FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 2).Value = filaTb("1FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Cell(fil + 4, 2).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString())
                        'ws.Cell(fil + 4, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil + 4, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 4, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Cell(fil + 2, 3).Value = filaTb("2FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 3).Value = filaTb("2FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 4, 3).Value = Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil + 4, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 2, 4).Value = filaTb("3FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ''  ws.Cell(fil + 2, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 3, 4).Value = filaTb("3FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        '					  ws.Cell(fil + 3, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 4, 4).Value = Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 4, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 2, 5).Value = filaTb("4FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 5).Value = filaTb("4FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 4, 5).Value = Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 4, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil + 4, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 2, 6).Value = Convert.ToInt32(filaTb("1FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("2FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("3FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("4FaltaJustificada").ToString())
                        ws.Cell(fil + 2, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 3, 6).Value = Convert.ToInt32(filaTb("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4FaltaSinJustificar").ToString())
                        ws.Cell(fil + 3, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 3, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 4, 6).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 4, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With




                        Exit For
                    End If

                Next


                'RCB_NotaBimestralCualitativa	BM_CodigoBimestre	AL_CodigoAlumno
                'AD	1	20090083
                'AD	1	20090174




                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Merge()
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Value = "CONDUCTA"
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                ws.Cell(fil + 1, 8).Value = "Term I"
                'ws.Cell(fil + 1, 8).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 8)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ws.Cell(fil + 1, 9).Value = "Term II"
                ws.Cell(fil + 1, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 9).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 9)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil + 1, 10).Value = "Term III"
                ws.Cell(fil + 1, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 10).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 10)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 11).Value = "Term IV"
                ws.Cell(fil + 1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 11)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil + 1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                ' ws.Cell(fil + 2, 8).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 2, 8)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                'ws.Cell(fil + 2, 9).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 2, 9)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ' ws.Cell(fil + 2, 10).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                With ws.Cell(fil + 2, 10)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                'ws.Cell(fil + 2, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 2, 11)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                For Each fill As System.Data.DataRow In tb_conducta.Rows
                    If Convert.ToInt32(fill("AL_CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                        If fill("BM_CodigoBimestre").ToString() = "1" Then
                            ws.Cell(fil + 2, 8).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "2" Then
                            ws.Cell(fil + 2, 9).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "3" Then
                            ws.Cell(fil + 2, 10).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "4" Then
                            ws.Cell(fil + 2, 11).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                    End If
                Next

                'RCB_NotaBimestralCualitativa	BM_CodigoBimestre	AL_CodigoAlumno
                'A	1	20100051

                'ws.Cell(fil + 1, 8).Value = ""
                'ws.Cell(fil + 1, 8).Value = ""
                'ws.Cell(fil + 1, 8).Value = ""
                'ws.Cell(fil, 8).Value = ""
                fil += 4
                Dim nombreCurso As String = ""
                fil += 1
                ws.Cell(fil, 1).Value = "COMMENTS"
                ws.Cell(fil, 1).Style.Font.Bold = True
                fil += 1
                ws.Cell(fil, 1).Value = "TUTOR"



                Dim estadoFilasMayor225 As Boolean = False
                For iComp As Integer = 0 To lst(iPerosona).lstLibretaComponente.Count - 1
                    If lst(iPerosona).lstLibretaComponente(iComp).nombreCurso <> nombreCurso Then
                        If lst(iPerosona).lstLibretaComponente(iComp).observacionCurso = "" Then
                            Continue For
                        End If
                        fil += 1

                        If 225 - (fil + 6) < 0 And Not estadoFilasMayor225 Then
                            fil = fil + (225 - fil) + 2
                            estadoFilasMayor225 = True
                        End If

                        ws.Cell(fil, 1).Value = lst(iPerosona).lstLibretaComponente(iComp).nombreCurso
                        ws.Cell(fil, 1).Style.Font.Bold = True
                        fil += 1
                        ''ws.Cell(fil, 1).Value = olibretaComponenteTemp.observacionCurso
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Merge()
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Value = lst(iPerosona).lstLibretaComponente(iComp).observacionCurso
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Style.Font.FontSize = 14
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Style.Alignment.WrapText = True
                        fil += 5
                    End If
                    nombreCurso = lst(iPerosona).lstLibretaComponente(iComp).nombreCurso
                Next

                '' ws.Range(ws.Cell(iniciaIndicador, 8), ws.Cell(iniciaIndicador + contadorIndicador - 1, 8)).Merge()
                '' ws.Range(ws.Cell(iniciaIndicador, 8), ws.Cell(iniciaIndicador + contadorIndicador - 1, 8)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                ''
                fil += 1
                ''  ws.Range(ws.Cell(iniciaIndicador, 8), ws.Cell(iniciaIndicador + contadorIndicador - 1, 8)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                'ws.Cell(fil + 4, 2).Borders(XlBordersIndex.xlInsideVertical).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                'ws.Cell(fil + 4, 2).Value = "XlBordersIndex.xlInsideVertical"


                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Merge()
                ' ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3))
                    '.Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    '.Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    '.Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Value = "TUTOR"

                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Merge()

                'ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Borders(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7))
                    '.Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    '.Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    '.Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With




                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Value = "PARENTS"

                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center




                ws.Column(1).Width = 12

                ws.Column(2).Width = 17
                ws.Column(3).Width = 17
                ws.Column(4).Width = 17
                ws.Column(5).Width = 17

                ws.Column(11).Width = 8
                ws.Column(6).Width = 19


                ws.Column(7).Width = 7
                ws.Column(8).Width = 7
                ws.Column(9).Width = 7
                ws.Column(10).Width = 8

                ws.PageSetup.AdjustTo(60)
                'ws.PageSetup.PageOrientation = ClosedXML.Excel.XLPageOrientation.Landscape
                ws.PageSetup.Margins.Top = 0.75 '1.9
                ws.PageSetup.Margins.Bottom = 0.75 '1.9
                ws.PageSetup.Margins.Left = 0.7 '0.6
                ws.PageSetup.Margins.Right = 0.7 '0.6
                ws.PageSetup.Margins.Header = 0.3 '0.8
                ws.PageSetup.Margins.Footer = 0.3 '0.8

                ws.PageSetup.PagesWide = 1


                '    .LeftMargin = excel.Application.InchesToPoints(0.7)
                '    .RightMargin = excel.Application.InchesToPoints(0.7)
                '    .TopMargin = excel.Application.InchesToPoints(0.75)
                '    .BottomMargin = excel.Application.InchesToPoints(0.75)
                '    .HeaderMargin = excel.Application.InchesToPoints(0.3)
                '    .FooterMargin = excel.Application.InchesToPoints(0.3)


                ''

                'excel.ActiveWindow.Zoom = 75


                'With excel.ActiveSheet.PageSetup
                '    .LeftHeader = ""
                '    .CenterHeader = ""
                '    .RightHeader = ""
                '    .LeftFooter = ""
                '    .CenterFooter = ""
                '    .RightFooter = ""
                '    .LeftMargin = excel.Application.InchesToPoints(0.7)
                '    .RightMargin = excel.Application.InchesToPoints(0.7)
                '    .TopMargin = excel.Application.InchesToPoints(0.75)
                '    .BottomMargin = excel.Application.InchesToPoints(0.75)
                '    .HeaderMargin = excel.Application.InchesToPoints(0.3)
                '    .FooterMargin = excel.Application.InchesToPoints(0.3)
                '    .PrintHeadings = False
                '    .PrintGridlines = False
                '    '.PrintComments = xlPrintNoComments
                '    .PrintQuality = 600
                '    .CenterHorizontally = False
                '    .CenterVertically = False
                '    .Orientation = 1
                '    .Draft = False
                '    '.PaperSize = xlPaperLetter
                '    '.FirstPageNumber = xlAutomatic
                '    '.Order = OrderedDictionary xlDownThenOver
                '    .BlackAndWhite = False
                '    .Zoom = False
                '    .FitToPagesWide = 1
                '    .FitToPagesTall = False
                '    '.PrintErrors = xlPrintErrorsDisplayed
                '    .OddAndEvenPagesHeaderFooter = False
                '    .DifferentFirstPageHeaderFooter = False
                '    .ScaleWithDocHeaderFooter = True
                '    .AlignMarginsHeaderFooter = True
                '    .EvenPage.LeftHeader.Text = ""
                '    .EvenPage.CenterHeader.Text = ""
                '    .EvenPage.RightHeader.Text = ""
                '    .EvenPage.LeftFooter.Text = ""
                '    .EvenPage.CenterFooter.Text = ""
                '    .EvenPage.RightFooter.Text = ""
                '    .FirstPage.LeftHeader.Text = ""
                '    .FirstPage.CenterHeader.Text = ""
                '    .FirstPage.RightHeader.Text = ""
                '    .FirstPage.LeftFooter.Text = ""
                '    .FirstPage.CenterFooter.Text = ""
                '    .FirstPage.RightFooter.Text = ""
                'End With





                '' Exit For

                'alumnosProcesado += 1

                'If Not estadoDetenerProceso Then

                '    Exit For

                'End If



            Next





            ''
            workbook.Save()

            Return rutaREpositorioTemporales


        Catch ex As Exception

        End Try
    End Function
    ''
#End Region

#Region "Crear libreta primaria En una sola hoja "
    ''
    Function crearLibretaPrimariaUnaSolaHoja(ByVal codigoAula As Integer, ByVal int_bimestre As Integer, ByVal codAlumno As String) As String
        '     <add key="LibretaPrimaria"  value="\Plantillas\ExportacionLibreta\libretaPrimaria.xlsx"/>
        '<add key="LibretaInicial" value="\Plantillas\ExportacionLibreta\libretaInicial.xlsx"/>

        ''   <add key="Temporales" value="\Temporales\"/>



        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaLibretaPrimaria")
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
        Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
        File.Copy(rutaPlantillas, rutaREpositorioTemporales)



        Dim tb_Asistencias As New System.Data.DataTable '' extraer las inasistencias del alumno
        Dim tb_demeritos As New System.Data.DataTable '' extrar los meritos y demeritos del alumno 
        Dim tb_conducta As New System.Data.DataTable


        Try
            ''
            Dim dt As New System.Data.DataTable
            Dim dst As New DataSet
            dst = New bl_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaPrimaria_1(codigoAula, int_bimestre, codAlumno, 1, 1, 1, 1)
            dt = dst.Tables(0)
            ''
            tb_Asistencias = dst.Tables(3)
            tb_demeritos = dst.Tables(4)
            tb_conducta = dst.Tables(5)

            ''
            Dim lst As New List(Of personaLibreta)
            lst = crearListaLibreta(dt)


            Dim workbook As New ClosedXML.Excel.XLWorkbook(rutaREpositorioTemporales)

            workbook.CalculateMode = XLCalculateMode.Auto

            '' seleccionar la primera hoja 
            Dim ws = workbook.Worksheet(1)



            Dim nombreCursoTemp As String = ""
            Dim contadorIndicador As Integer = 0
            Dim filaCount As Integer = 0
            Dim iniciaIndicador As Integer = 0
            Dim indiceHojas As Integer = 0
           
            ''
            Dim abrBimestre As String = ""
            If int_bimestre = 1 Then
                abrBimestre = "I"
            End If
            If int_bimestre = 2 Then
                abrBimestre = "II"
            End If
            If int_bimestre = 3 Then
                abrBimestre = "III"
            End If
            If int_bimestre = 4 Then
                abrBimestre = "IV"
            End If


            Dim ci As Globalization.CultureInfo = New Globalization.CultureInfo("en-US")

            Date.Now.ToString("MMMM", ci)
            ''
            Dim agregoFilas As Integer = 0
            Dim agregoFilas1 As Integer = 0

            Dim boolEstado As Boolean = True
            ''iterando la lista de alumnmos 

            ''variables  globales
            Dim fil As Integer = 2
            ''
            ''============================================
            ''
            ''--------------------------------------------
            '' 
            ''
            ''
            ''--------------------------------------------

            For iPerosona As Integer = 0 To lst.Count - 1
                agregoFilas = 0
                agregoFilas1 = 0

                indiceHojas += 1


                ''
                ''1.pintado del reports card 
                ''------------------------------------------------------
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Merge()
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Value = "REPORT CARD"
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Style.Font.Bold = True
                ws.Range(ws.Cell(fil, 3), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ''------------------------------------------------------

                ''2.pintado del nombre 
                ''--------------------------------------------------------
                ''2.1 pintado de la etiqueta  del nombre  
                fil += 1 ''  
                ws.Cell(fil, 3).Value = "NAME"
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                ''2.2 pintado del nombre  del alumno
                ''---------------------------------------------------------
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = lst(iPerosona).nombreAlumno
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                ''pintado de class
                ''---------------------------------------------------------
                fil += 1
                ws.Cell(fil, 3).Value = "CLASS"
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = dst.Tables(1).Rows(0)("informacion").ToString()
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                ''pintado del nombre del tutor 
                ''---------------------------------------------------------

                ''

                ''pintado del "DATE"
                ''--------------------------------------
                ws.Range(ws.Cell(fil, 9), ws.Cell(fil, 10)).Merge()
                ws.Range(ws.Cell(fil, 9), ws.Cell(fil, 10)).Value = "Date : " & Date.Now.ToString("MMMM", ci) & " " & Date.Now.Day.ToString() & " ," & Date.Now.Year().ToString
                ws.Range(ws.Cell(fil, 9), ws.Cell(fil, 10)).Style.Font.Bold = True
                ''---------------------------------------
                ''
                fil += 1
                ws.Cell(fil, 3).Value = "TUTOR"
                With ws.Cell(fil, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ws.Cell(fil, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Merge()
                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Value = dst.Tables(2).Rows(0)("nombre").ToString()

                ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                With ws.Range(ws.Cell(fil, 4), ws.Cell(fil, 7))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With
                ''---------------------------------------------------------
                fil += 1
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Merge()
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Value = "SUBJECT AREAS -" & Now().Year().ToString()
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Style.Font.Bold = True
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 2)).Style.Font.FontSize = 16

                ''pintado de "TERM"
                ws.Cell(fil, 11).Value = "TERM " & abrBimestre
                ws.Cell(fil, 11).Style.Font.Bold = True
                ''
              
                ''
                ''pintado de cursos
                ''---------------------------------------------------------------------

                For Each olibretaComponente As libretaComponente In lst(iPerosona).lstLibretaComponente
                    fil += 1

                    If nombreCursoTemp <> olibretaComponente.nombreCurso Then
                        Dim nombreTemp As String = ""
                        nombreTemp = olibretaComponente.nombreCurso

                        Dim count = From o In lst(iPerosona).lstLibretaComponente Where o.nombreCurso = nombreTemp _
                            Select o.lstIndicador
                        Dim cntListas As Integer = 0
                        'cntListas = count.Count()

                        Dim contador As Integer = 0

                        For Each el In count
                            contador += el.Count() + 1
                        Next

                        Dim cnt As Integer = contador + 2


                        If Not fil + cnt <= 75 And fil < 150 And agregoFilas = 0 Then
                            Dim diferencias As Integer = 0
                            diferencias = 77 - fil
                            fil += diferencias
                            agregoFilas = 1

                        End If
                        If Not fil + cnt <= 150 And fil > 75 And agregoFilas1 = 0 Then
                            Dim diferencias As Integer = 0
                            diferencias = 150 - fil
                            fil += diferencias + 3
                            agregoFilas1 = 1

                        End If

                        filaCount = fil + 1
                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Merge()
                        '  ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With




                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Value = olibretaComponente.nombreCurso.ToUpper() '' & " nombre alumno " & opersonaLibreta.nombreAlumno


                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Range(ws.Cell(fil, 1), ws.Cell(filaCount, 6)).Style.Font.Bold = True
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Merge()
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Value = "PERFORMANCE"
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 11).Value = "AVERAGE"
                        ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        ' ws.Cell(fil, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 11)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        fil = filaCount
                        ws.Cell(fil, 7).Value = "C"
                        ws.Cell(fil, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        'ws.Cell(fil, 7).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous



                        With ws.Cell(fil, 7)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 8).Value = "B"
                        ws.Cell(fil, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil, 8).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 8)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil, 9).Value = "A"
                        ws.Cell(fil, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil, 9).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 9)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With




                        ws.Cell(fil, 10).Value = "AD"
                        ws.Cell(fil, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil, 10).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil, 10)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With



                        ws.Cell(fil, 11).Value = olibretaComponente.promedioComponente.ToUpper()
                        ' ws.Cell(fil, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil, 11)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        fil += 1
                        ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Merge()
                        'ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Range(ws.Cell(fil, 7), ws.Cell(fil, 10))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                    End If

                    'If olibretaComponente.nombreComponente = "READING" Then
                    '    fil += 5
                    'End If


                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Merge()
                    ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Value = olibretaComponente.nombreComponente.ToUpper()

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.WrapText = True

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Font.Bold = True

                    ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    ws.Cell(fil, 11).Value = olibretaComponente.notaComponente.ToUpper()

                    ws.Cell(fil, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center



                    With ws.Cell(fil, 11)
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With



                    iniciaIndicador = fil + 1
                    contadorIndicador = 0


                    For Each olibretaIndicador As libretaIndicador In olibretaComponente.lstIndicador

                        fil += 1
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Merge()

                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Value = olibretaIndicador.nombreIndicador

                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top

                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.WrapText = True

                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Indent = 2


                        ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        '' .Style.Alignment.Indent = 2


                        With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With





                        If olibretaIndicador.notaIndicador.ToUpper() = "C" Then
                            ws.Cell(fil, 7).Value = " * "
                            ws.Cell(fil, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "B" Then
                            ws.Cell(fil, 8).Value = " * "
                            ws.Cell(fil, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "A" Then
                            ws.Cell(fil, 9).Value = " * "
                            ws.Cell(fil, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        End If
                        If olibretaIndicador.notaIndicador.ToUpper() = "AD" Then
                            ws.Cell(fil, 10).Value = " * "
                            ws.Cell(fil, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        contadorIndicador += 1


                        For ii As Integer = 7 To 10
                            ' ws.Cell(fil, ii).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                            With ws.Cell(fil, ii)
                                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                            End With



                        Next
                    Next

                    ws.Range(ws.Cell(iniciaIndicador, 11), ws.Cell(iniciaIndicador + contadorIndicador - 1, 11)).Merge()


                    ' ws.Range(ws.Cell(iniciaIndicador, 11), ws.Cell(iniciaIndicador + contadorIndicador - 1, 11)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                    With ws.Range(ws.Cell(iniciaIndicador, 11), ws.Cell(iniciaIndicador + contadorIndicador - 1, 11))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    End With


                    nombreCursoTemp = olibretaComponente.nombreCurso
                Next



                ''creando inasistencias del alumno 


                '' ws.Cell(iniciaIndicador, 8).
                ''

                fil += 3
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Merge()
                'ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With




                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Value = "ABSENCES"
                ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ' ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6)).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Range(ws.Cell(fil, 1), ws.Cell(fil, 6))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With




                ws.Cell(fil + 1, 1).Value = ""
                'ws.Cell(fil + 1, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 2).Value = "Term I"
                ws.Cell(fil + 1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 2)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 3).Value = "Term II"
                ws.Cell(fil + 1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 3)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 4).Value = "Term III"
                ws.Cell(fil + 1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 4)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 5).Value = "Term IV"
                ws.Cell(fil + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 1, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 5)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 6).Value = "Term Total / Average"
                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 1, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                With ws.Cell(fil + 1, 6)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ws.Cell(fil + 2, 1).Value = "Justified"
                ws.Cell(fil + 2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                'ws.Cell(fil + 2, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 2, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 3, 1).Value = "Unjustified"
                ws.Cell(fil + 3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 3, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 3, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 4, 1).Value = "Lateness"
                ws.Cell(fil + 4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                'ws.Cell(fil + 4, 1).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 4, 1)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                For Each filaTb As System.Data.DataRow In tb_Asistencias.Rows
                    If Convert.ToInt32(filaTb("CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                        ws.Cell(fil + 2, 2).Value = filaTb("1FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 2).Value = filaTb("1FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Cell(fil + 4, 2).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString())
                        'ws.Cell(fil + 4, 2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil + 4, 2)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 4, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ws.Cell(fil + 2, 3).Value = filaTb("2FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 3).Value = filaTb("2FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 4, 3).Value = Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil + 4, 3)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 2, 4).Value = filaTb("3FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ''  ws.Cell(fil + 2, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 3, 4).Value = filaTb("3FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        '					  ws.Cell(fil + 3, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 4, 4).Value = Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 4).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 4, 4)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 2, 5).Value = filaTb("4FaltaJustificada").ToString()
                        ws.Cell(fil + 2, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 3, 5).Value = filaTb("4FaltaSinJustificar").ToString()
                        ws.Cell(fil + 3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 3, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 4, 5).Value = Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        ' ws.Cell(fil + 4, 5).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous


                        With ws.Cell(fil + 4, 5)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 2, 6).Value = Convert.ToInt32(filaTb("1FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("2FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("3FaltaJustificada").ToString()) + Convert.ToInt32(filaTb("4FaltaJustificada").ToString())
                        ws.Cell(fil + 2, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 2, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 2, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With


                        ws.Cell(fil + 3, 6).Value = Convert.ToInt32(filaTb("1FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3FaltaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4FaltaSinJustificar").ToString())
                        ws.Cell(fil + 3, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        'ws.Cell(fil + 3, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 3, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With

                        ws.Cell(fil + 4, 6).Value = Convert.ToInt32(filaTb("1TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("2TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("3TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("4TardanzaSinJustificar").ToString()) + Convert.ToInt32(filaTb("1TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("2TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("3TardanzaJustificada").ToString()) + Convert.ToInt32(filaTb("4TardanzaJustificada").ToString())
                        ws.Cell(fil + 4, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        '  ws.Cell(fil + 4, 6).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                        With ws.Cell(fil + 4, 6)
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                        End With




                        Exit For
                    End If

                Next


                'RCB_NotaBimestralCualitativa	BM_CodigoBimestre	AL_CodigoAlumno
                'AD	1	20090083
                'AD	1	20090174




                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Merge()
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Value = "CONDUCTA"
                ws.Range(ws.Cell(fil, 8), ws.Cell(fil, 11)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center


                ws.Cell(fil + 1, 8).Value = "Term I"
                'ws.Cell(fil + 1, 8).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 8)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                ws.Cell(fil + 1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ws.Cell(fil + 1, 9).Value = "Term II"
                ws.Cell(fil + 1, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 9).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 9)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil + 1, 10).Value = "Term III"
                ws.Cell(fil + 1, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 10).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 10)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                ws.Cell(fil + 1, 11).Value = "Term IV"
                ws.Cell(fil + 1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ' ws.Cell(fil + 1, 11).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous

                With ws.Cell(fil + 1, 11)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                ws.Cell(fil + 1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center




                With ws.Cell(fil + 2, 8)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                With ws.Cell(fil + 2, 9)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                With ws.Cell(fil + 2, 10)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                With ws.Cell(fil + 2, 11)
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                For Each fill As System.Data.DataRow In tb_conducta.Rows
                    If Convert.ToInt32(fill("AL_CodigoAlumno").ToString()) = lst(iPerosona).codAlumno Then
                        If fill("BM_CodigoBimestre").ToString() = "1" Then
                            ws.Cell(fil + 2, 8).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "2" Then
                            ws.Cell(fil + 2, 9).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "3" Then
                            ws.Cell(fil + 2, 10).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                        If fill("BM_CodigoBimestre").ToString() = "4" Then
                            ws.Cell(fil + 2, 11).Value = fill("RCB_NotaBimestralCualitativa").ToString()
                            ws.Cell(fil + 2, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        End If
                    End If
                Next

                'RCB_NotaBimestralCualitativa	BM_CodigoBimestre	AL_CodigoAlumno
                'A	1	20100051

                'ws.Cell(fil + 1, 8).Value = ""
                'ws.Cell(fil + 1, 8).Value = ""
                'ws.Cell(fil + 1, 8).Value = ""
                'ws.Cell(fil, 8).Value = ""
                fil += 4
                Dim nombreCurso As String = ""
                fil += 1
                ws.Cell(fil, 1).Value = "COMMENTS"
                ws.Cell(fil, 1).Style.Font.Bold = True
                fil += 1
                ws.Cell(fil, 1).Value = "TUTOR"



                Dim estadoFilasMayor225 As Boolean = False
                For iComp As Integer = 0 To lst(iPerosona).lstLibretaComponente.Count - 1
                    If lst(iPerosona).lstLibretaComponente(iComp).nombreCurso <> nombreCurso Then
                        If lst(iPerosona).lstLibretaComponente(iComp).observacionCurso = "" Then
                            Continue For
                        End If
                        fil += 1

                        If 225 - (fil + 6) < 0 And Not estadoFilasMayor225 Then
                            fil = fil + (225 - fil) + 2
                            estadoFilasMayor225 = True
                        End If

                        ws.Cell(fil, 1).Value = lst(iPerosona).lstLibretaComponente(iComp).nombreCurso
                        ws.Cell(fil, 1).Style.Font.Bold = True
                        fil += 1
                        ''ws.Cell(fil, 1).Value = olibretaComponenteTemp.observacionCurso
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Merge()
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Value = lst(iPerosona).lstLibretaComponente(iComp).observacionCurso
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Style.Font.FontSize = 14
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        ws.Range(ws.Cell(fil, 1), ws.Cell(fil + 4, 11)).Style.Alignment.WrapText = True
                        fil += 5
                    End If
                    nombreCurso = lst(iPerosona).lstLibretaComponente(iComp).nombreCurso
                Next
                fil += 1

                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Merge()
                With ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3))
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                End With



                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Value = "TUTOR"
                ws.Range(ws.Cell(fil + 2, 2), ws.Cell(fil + 2, 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Merge()

                With ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7))
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                End With
                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Value = "PARENTS"

                ws.Range(ws.Cell(fil + 2, 5), ws.Cell(fil + 2, 7)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center




                ws.Column(1).Width = 12
                ws.Column(2).Width = 17
                ws.Column(3).Width = 17
                ws.Column(4).Width = 17
                ws.Column(5).Width = 17
                ws.Column(11).Width = 8
                ws.Column(6).Width = 19
                ws.Column(7).Width = 7
                ws.Column(8).Width = 7
                ws.Column(9).Width = 7
                ws.Column(10).Width = 8
                ws.PageSetup.AdjustTo(60)

                ws.PageSetup.Margins.Top = 0.75 '1.9
                ws.PageSetup.Margins.Bottom = 0.75 '1.9
                ws.PageSetup.Margins.Left = 0.7 '0.6
                ws.PageSetup.Margins.Right = 0.7 '0.6
                ws.PageSetup.Margins.Header = 0.3 '0.8
                ws.PageSetup.Margins.Footer = 0.3 '0.8

                ws.PageSetup.PagesWide = 1




            Next





            ''
            workbook.Save()

            Return rutaREpositorioTemporales


        Catch ex As Exception

        End Try
    End Function
    ''
#End Region
    
     
End Class
