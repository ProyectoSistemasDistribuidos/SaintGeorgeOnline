Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloReportes
Imports SaintGeorgeOnline_DataAccess.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices.Marshal
Imports ClosedXML
Imports ClosedXML.Excel

''' <summary>
''' Modulo de Reportes de Pensiones
''' </summary>
''' <remarks>
''' Código del Modulo:    1
''' Código de la Opción:  7
''' </remarks>

Partial Class ModuloReportes_ReportesPensiones
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Variables de Posiciones en Excel"

    Private Shared int_HA_Left As Integer = 2 ' Alineación Horizontal Izquierda
    Private Shared int_HA_Center As Integer = 3 ' Alineación Horizontal Centrada
    Private Shared int_HA_Right As Integer = 4 ' Alineación Horizontal Derecha

    Private Shared int_VA_Top As Integer = 1 ' Alineación Vertical Superior
    Private Shared int_VA_Middle As Integer = 2 ' Alineación Vertical Media
    Private Shared int_VA_Bottom As Integer = 3 ' Alineación Vertical Inferior

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Master.MostrarTitulo("Reportes de Pensiones")

            btnReporteExportar.Attributes.Add("onclick", "ShowMyModalPopup()")

            If Not Page.IsPostBack Then
                cargarListaReportes()
                cargarListaPresentacion()

                pnlReporte1.Visible = True
                pnlReporte2.Visible = False
                pnlReporte3.Visible = False
                pnlReporte4.Visible = False
                pnlReporte5.Visible = False
                pnlReporte6.Visible = False
                pnlReporte7.Visible = False

                cargarComboAniosAcademicos()
                cargarComboConceptoCobro()
                cargarComboGrado_Rep1()
                cargarComboNivelMinisterio_Rep3()
                cargarComboMotivoBeca_Rep3()
                cargarComboTipoBeca_Rep3()

                limpiarCombos(ddlRep1_Aulas, True, False)
                limpiarCombos(ddlRep2_Aulas, True, False)
                limpiarCombos(ddlRep3_Grado, True, False)
                limpiarCombos(ddlRep4_Periodo, False, True)

                ddlRep1_Periodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                ddlRep2_Periodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                ddlRep3_Periodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                ddlRep4_Periodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                ddlRep6_Periodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                ddlRep7_Periodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar

                tbRep1_FechaInicio.Text = Today.AddDays(-7).ToShortDateString
                tbRep1_FechaFin.Text = Today
                tbRep2_FechaFin.Text = Today

                tbRep5_FechaInicio.Text = GetFirstDayOfMonth(Today.AddMonths(-1))
                tbRep5_FechaFin.Text = GetLastDayOfMonth(Today.AddMonths(-1))

                tbRep6_FechaInicio.Text = GetFirstDayOfMonth(Today)
                tbRep6_FechaFin.Text = Today

                tbRep7_Fecha1.Text = GetFirstDayOfMonth(Today)
                tbRep7_Fecha2.Text = GetFirstDayOfMonth(Today)

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

    Protected Sub btnReporteExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
            'MsgBox(ex.Message)
        End Try
    End Sub

#End Region

#Region "Auxiliares"

    Private Function GetFirstDayOfMonth(ByVal dtDate As DateTime) As DateTime
        Dim dtFrom As DateTime = dtDate
        dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1))
        Return dtFrom
    End Function

    Private Function GetLastDayOfMonth(ByVal dtDate As DateTime) As DateTime
        Dim dtTo As DateTime = dtDate
        dtTo = dtTo.AddMonths(1)
        dtTo = dtTo.AddDays(-(dtTo.Day))
        Return dtTo
    End Function

#End Region

#Region "Metodos"

    Private Sub cargarListaReportes()

        Dim int_CodigoTipoReporte As Integer = 4 ' Reportes de Pensiones
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

        Dim dt As DataTable = CType(ViewState("ListaReportes"), DataSet).Tables(1)
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

        If lstReportes.SelectedValue = 13 Then ' Reporte : Reportes de Pagos

            pnlReporte1.Visible = True
            pnlReporte2.Visible = False
            pnlReporte3.Visible = False
            pnlReporte4.Visible = False
            pnlReporte5.Visible = False
            pnlReporte6.Visible = False
            pnlReporte7.Visible = False

        ElseIf lstReportes.SelectedValue = 14 Then ' Reporte : Reportes de Deudas

            pnlReporte1.Visible = False
            pnlReporte2.Visible = True
            pnlReporte3.Visible = False
            pnlReporte4.Visible = False
            pnlReporte5.Visible = False
            pnlReporte6.Visible = False
            pnlReporte7.Visible = False

        ElseIf lstReportes.SelectedValue = 15 Then ' Reporte : Reportes de becas

            If lstPresentacion.SelectedIndex = 0 Then
                pnlReporte1.Visible = False
                pnlReporte2.Visible = False
                pnlReporte3.Visible = False
                pnlReporte4.Visible = True
                pnlReporte5.Visible = False
                pnlReporte6.Visible = False
                pnlReporte7.Visible = False

                cargarComboMes_Rep4()
                tr_Beca.Visible = False
                cargarComboNivel_Rep4()
                'cargarComboTipoBeca_Rep4()
                limpiarCombos(ddlRep4_SubNivel, True, False)
                limpiarCombos(ddlRep4_Grado, True, False)
                limpiarCombos(ddlRep4_Aula, True, False)
            Else
                pnlReporte1.Visible = False
                pnlReporte2.Visible = False
                pnlReporte3.Visible = True
                pnlReporte4.Visible = False
                pnlReporte5.Visible = False
                pnlReporte6.Visible = False
                pnlReporte7.Visible = False

            End If

        ElseIf lstReportes.SelectedValue = 24 Then ' Reporte : Contables

            pnlReporte1.Visible = False
            pnlReporte2.Visible = False
            pnlReporte3.Visible = False
            pnlReporte4.Visible = False
            pnlReporte5.Visible = True
            pnlReporte6.Visible = False
            pnlReporte7.Visible = False

        ElseIf lstReportes.SelectedValue = 35 Then ' Reporte : Proyecciones

            pnlReporte1.Visible = False
            pnlReporte2.Visible = False
            pnlReporte3.Visible = False
            pnlReporte4.Visible = False
            pnlReporte5.Visible = False
            pnlReporte6.Visible = True
            pnlReporte7.Visible = False

        ElseIf lstReportes.SelectedValue = 37 Then ' Reporte : Morosidad

            pnlReporte1.Visible = False
            pnlReporte2.Visible = False
            pnlReporte3.Visible = False
            pnlReporte4.Visible = False
            pnlReporte5.Visible = False
            pnlReporte6.Visible = False
            pnlReporte7.Visible = True

        Else
            pnlReporte1.Visible = False
            pnlReporte2.Visible = False
            pnlReporte3.Visible = False
            pnlReporte4.Visible = False
            pnlReporte5.Visible = False
            pnlReporte6.Visible = False
            pnlReporte7.Visible = False

        End If

    End Sub

    Private Sub limpiarCombos(ByVal combo As DropDownList, ByVal bool_Todos As Boolean, ByVal bool_Seleccione As Boolean)

        Controles.limpiarCombo(combo, bool_Todos, bool_Seleccione)

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
    Private Sub Exportar()

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet
        Dim obj_BL_Pensiones As New bl_Pensiones

        Dim bool_Parametros As Boolean = False
        Dim bool_MostrarTotales As Boolean = False


        Dim int_TipoReporte As Integer = lstReportes.SelectedValue 'Tipo reporte
        Dim int_PresentacionReporte As Integer = lstPresentacion.SelectedValue 'Tipo reporte


        Dim str_TituloReporte As String = "" 'Titulo reporte
        Dim Arreglo_Parametros As New ArrayList 'Arreglo de parametros para la visualizacion en el reporte

        Dim reporte_html As String = "" 'Contenido del reporte
        Dim Arreglo_Datos As String() 'Arreglo de datos del reporte (cabecera y detalle)

        Dim dt As DataTable = New DataTable("ListaExportar")

        Dim int_CodigoPeriodoAcademico As Integer
        Dim int_CodigoNivelMinisterio As Integer
        Dim int_CodigoTipoBeca As Integer
        Dim int_CodigoMotivoBeca As Integer
        Dim str_CodigoConceptoCobro As String = ""
        Dim sb_CodigoConceptoCobro As New StringBuilder
        Dim dt_FechaRangoInicio As Date
        Dim dt_FechaRangoFin As Date
        Dim int_CodigoGrado As Integer
        Dim int_CodigoAula As Integer
        Dim bool_Valido As Boolean = False
        Dim int_Mes, int_Nivel, int_SubNivel As Integer
        Dim dt_TipoBeca As DataTable

     If int_TipoReporte = 13 Then ' Reporte : Reportes de Pagos

            int_CodigoPeriodoAcademico = ddlRep1_Periodo.SelectedValue
            dt_FechaRangoInicio = tbRep1_FechaInicio.Text
            dt_FechaRangoFin = tbRep1_FechaFin.Text
            int_CodigoGrado = ddlRep1_Grados.SelectedValue
            int_CodigoAula = ddlRep1_Aulas.SelectedValue

            If int_PresentacionReporte <> 19 Then

                If ddlRep1_chkAll.Checked Then ' Todos checkeado
                    sb_CodigoConceptoCobro.Append("")
                Else
                    Dim int_ContItems As Integer = 0
                    While int_ContItems <= ddlRep1_rbConceptosCobro.Items.Count - 1
                        If ddlRep1_rbConceptosCobro.Items(int_ContItems).Selected Then
                            sb_CodigoConceptoCobro.Append(ddlRep1_rbConceptosCobro.Items(int_ContItems).Value.ToString & ",")
                            bool_Valido = True
                        End If
                        int_ContItems += 1
                    End While

                    If bool_Valido = False Then
                        Me.Master.MostrarMensajeAlert("Debe seleccionar por lo menos 1 concepto.")
                        Exit Sub
                    End If

                    str_CodigoConceptoCobro = sb_CodigoConceptoCobro.ToString()
                    str_CodigoConceptoCobro = str_CodigoConceptoCobro.Substring(0, str_CodigoConceptoCobro.Length - 1)
                End If

            End If

            If int_PresentacionReporte = 16 Then ' Diario por conceptos

                str_TituloReporte = "MiReporte" '"Cancelación Diaria"
                ds_Lista = obj_BL_Pensiones.FUN_REP_PagosDiariosPorConceptos( _
                    int_CodigoPeriodoAcademico, str_CodigoConceptoCobro, dt_FechaRangoInicio, dt_FechaRangoFin, int_CodigoGrado, int_CodigoAula, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "Concepto", "string")
                dt = Datos.agregarColumna(dt, "NroPago", "string")
                dt = Datos.agregarColumna(dt, "Periodo", "string")
                dt = Datos.agregarColumna(dt, "NGS", "string")
                dt = Datos.agregarColumna(dt, "Origen", "string")
                dt = Datos.agregarColumna(dt, "Codigo", "string")
                dt = Datos.agregarColumna(dt, "Nombre", "string")
                dt = Datos.agregarColumna(dt, "F.Pago", "string")
                dt = Datos.agregarColumna(dt, "Mon", "string")
                dt = Datos.agregarColumna(dt, "Monto", "string")
                dt = Datos.agregarColumna(dt, "Dscto", "string")
                dt = Datos.agregarColumna(dt, "Mora", "string")
                dt = Datos.agregarColumna(dt, "Total", "string")
                dt = Datos.agregarColumna(dt, "Trabajador", "string")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Concepto") = dr.Item("Concepto")
                    auxDR.Item("NroPago") = dr.Item("NroPago")
                    auxDR.Item("Periodo") = dr.Item("Mes")
                    auxDR.Item("NGS") = dr.Item("NGS")
                    auxDR.Item("Origen") = dr.Item("Origen")
                    auxDR.Item("Codigo") = dr.Item("CodigoAlumno")
                    auxDR.Item("Nombre") = dr.Item("NombreCompletoAlumno")
                    auxDR.Item("F.Pago") = dr.Item("F.Pago")
                    auxDR.Item("Mon") = dr.Item("Mon")
                    auxDR.Item("Monto") = dr.Item("Monto")
                    auxDR.Item("Dscto") = dr.Item("Dscto")
                    auxDR.Item("Mora") = dr.Item("Mora")
                    auxDR.Item("Total") = dr.Item("Total")
                    auxDR.Item("Trabajador") = dr.Item("NombreTrabajador")
                    dt.Rows.Add(auxDR)
                Next

            ElseIf int_PresentacionReporte = 17 Then ' Presentación : Por concepto y salón

                str_TituloReporte = "MiReporte" '"Cancelación Diaria Por Aula"
                ds_Lista = obj_BL_Pensiones.FUN_REP_PagosDiariosPorConceptosYAula( _
                    int_CodigoPeriodoAcademico, str_CodigoConceptoCobro, dt_FechaRangoInicio, dt_FechaRangoFin, int_CodigoGrado, int_CodigoAula, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "Concepto", "string")
                dt = Datos.agregarColumna(dt, "NroPago", "string")
                dt = Datos.agregarColumna(dt, "Periodo", "string")
                dt = Datos.agregarColumna(dt, "Grado", "integer")
                dt = Datos.agregarColumna(dt, "Grado - Aula", "string")
                dt = Datos.agregarColumna(dt, "Origen", "string")
                dt = Datos.agregarColumna(dt, "Codigo", "string")
                dt = Datos.agregarColumna(dt, "Nombre", "string")
                dt = Datos.agregarColumna(dt, "F.Pago", "string")
                dt = Datos.agregarColumna(dt, "Mon", "string")
                dt = Datos.agregarColumna(dt, "Monto", "string")
                dt = Datos.agregarColumna(dt, "Dscto", "string")
                dt = Datos.agregarColumna(dt, "Mora", "string")
                dt = Datos.agregarColumna(dt, "Total", "string")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Concepto") = dr.Item("Concepto")
                    auxDR.Item("NroPago") = dr.Item("NroPago")
                    auxDR.Item("Periodo") = dr.Item("Mes")
                    auxDR.Item("Grado") = dr.Item("CodigoGrado")
                    auxDR.Item("Grado - Aula") = dr.Item("NGS")
                    auxDR.Item("Origen") = dr.Item("Origen")
                    auxDR.Item("Codigo") = dr.Item("CodigoAlumno")
                    auxDR.Item("Nombre") = dr.Item("NombreCompletoAlumno")
                    auxDR.Item("F.Pago") = dr.Item("F.Pago")
                    auxDR.Item("Mon") = dr.Item("Mon")
                    auxDR.Item("Monto") = dr.Item("Monto")
                    auxDR.Item("Dscto") = dr.Item("Dscto")
                    auxDR.Item("Mora") = dr.Item("Mora")
                    auxDR.Item("Total") = dr.Item("Total")
                    dt.Rows.Add(auxDR)
                Next

            ElseIf int_PresentacionReporte = 19 Then ' Presentación : Resumen de Pagos Realizados

                str_TituloReporte = "MiReporte" '"Pagos Realizados"
                ds_Lista = obj_BL_Pensiones.FUN_REP_PagosRealizadosPorPeriodo( _
                    int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "Origen", "string")
                dt = Datos.agregarColumna(dt, "Concepto", "string")
                dt = Datos.agregarColumna(dt, "Mes", "string")
                dt = Datos.agregarColumna(dt, "Mon", "string")
                dt = Datos.agregarColumna(dt, "Total", "decimal")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Origen") = dr.Item("Origen")
                    auxDR.Item("Concepto") = dr.Item("Concepto")
                    auxDR.Item("Mes") = dr.Item("Mes")
                    auxDR.Item("Mon") = dr.Item("Mon")
                    auxDR.Item("Total") = dr.Item("Total")
                    dt.Rows.Add(auxDR)
                Next

            End If

        ElseIf int_TipoReporte = 14 Then ' Reporte : Reportes de Deudas

            ' Parámetros
            int_CodigoPeriodoAcademico = ddlRep2_Periodo.SelectedValue
            dt_FechaRangoFin = tbRep2_FechaFin.Text
            int_CodigoGrado = ddlRep2_Grados.SelectedValue
            int_CodigoAula = ddlRep2_Aulas.SelectedValue



            If int_PresentacionReporte = 74 Then ' Deudas Generadas
                sb_CodigoConceptoCobro.Append("")
            Else
                If ddlRep2_chkAll.Checked Then ' Todos checkeado
                    sb_CodigoConceptoCobro.Append("")
                Else
                    Dim int_ContItems As Integer = 0
                    While int_ContItems <= ddlRep2_rbConceptosCobro.Items.Count - 1
                        If ddlRep2_rbConceptosCobro.Items(int_ContItems).Selected Then
                            sb_CodigoConceptoCobro.Append(ddlRep2_rbConceptosCobro.Items(int_ContItems).Value.ToString & ",")
                            bool_Valido = True
                        End If
                        int_ContItems += 1
                    End While
                    If bool_Valido = False Then
                        Me.Master.MostrarMensajeAlert("Debe seleccionar por lo menos 1 concepto.")
                        Exit Sub
                    End If
                    str_CodigoConceptoCobro = sb_CodigoConceptoCobro.ToString()
                    str_CodigoConceptoCobro = str_CodigoConceptoCobro.Substring(0, str_CodigoConceptoCobro.Length - 1)
                End If
            End If

            If int_PresentacionReporte = 18 Then ' Por salon y concepto

                str_TituloReporte = "MiReporte" '
                ds_Lista = obj_BL_Pensiones.FUN_REP_DeudasPorConceptosYAula( _
                    int_CodigoPeriodoAcademico, str_CodigoConceptoCobro, dt_FechaRangoFin, int_CodigoGrado, int_CodigoAula, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "Año", "integer")
                dt = Datos.agregarColumna(dt, "Mes", "string")
                dt = Datos.agregarColumna(dt, "Grado", "integer")
                dt = Datos.agregarColumna(dt, "Grado-Aula", "string")
                dt = Datos.agregarColumna(dt, "Codigo", "string")
                dt = Datos.agregarColumna(dt, "Alumno", "string")
                dt = Datos.agregarColumna(dt, "Concepto", "string")
                dt = Datos.agregarColumna(dt, "Mon", "string")
                dt = Datos.agregarColumna(dt, "Monto", "string")
                dt = Datos.agregarColumna(dt, "Filtro", "integer")
                dt = Datos.agregarColumna(dt, "# Deudas", "integer")
                dt = Datos.agregarColumna(dt, "# Deu", "integer")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Año") = dr.Item("Anio")
                    auxDR.Item("Mes") = dr.Item("Mes")
                    auxDR.Item("Grado") = dr.Item("CodigoGrado")
                    auxDR.Item("Grado-Aula") = dr.Item("NGS")
                    auxDR.Item("Codigo") = dr.Item("CodigoAlumno")
                    auxDR.Item("Alumno") = dr.Item("NombreCompletoAlumno")
                    auxDR.Item("Concepto") = dr.Item("Concepto")
                    auxDR.Item("Mon") = dr.Item("Mon")
                    auxDR.Item("Monto") = dr.Item("Monto")
                    auxDR.Item("Filtro") = dr.Item("FiltroDeudas")
                    auxDR.Item("# Deudas") = dr.Item("CantidadDeudas")
                    auxDR.Item("# Deu") = dr.Item("TotalAlumnos")
                    dt.Rows.Add(auxDR)
                Next

            ElseIf int_PresentacionReporte = 25 Then ' Deudas Generadas

                str_TituloReporte = "MiReporte" '
                ds_Lista = obj_BL_Pensiones.FUN_REP_DeudasGeneradas( _
                    int_CodigoPeriodoAcademico, str_CodigoConceptoCobro, dt_FechaRangoFin, int_CodigoGrado, int_CodigoAula, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "Año", "integer")
                dt = Datos.agregarColumna(dt, "Mes", "string")
                dt = Datos.agregarColumna(dt, "Grado", "integer")
                dt = Datos.agregarColumna(dt, "Grado-Aula", "string")
                dt = Datos.agregarColumna(dt, "Codigo", "string")
                dt = Datos.agregarColumna(dt, "Alumno", "string")
                dt = Datos.agregarColumna(dt, "Concepto", "string")
                dt = Datos.agregarColumna(dt, "Mon", "string")
                dt = Datos.agregarColumna(dt, "Monto", "string")
                dt = Datos.agregarColumna(dt, "Filtro", "integer")
                dt = Datos.agregarColumna(dt, "# Deudas", "integer")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Año") = dr.Item("Anio")
                    auxDR.Item("Mes") = dr.Item("Mes")
                    auxDR.Item("Grado") = dr.Item("CodigoGrado")
                    auxDR.Item("Grado-Aula") = dr.Item("NGS")
                    auxDR.Item("Codigo") = dr.Item("CodigoAlumno")
                    auxDR.Item("Alumno") = dr.Item("NombreCompletoAlumno")
                    auxDR.Item("Concepto") = dr.Item("Concepto")
                    auxDR.Item("Mon") = dr.Item("Mon")
                    auxDR.Item("Monto") = dr.Item("Monto")
                    auxDR.Item("Filtro") = dr.Item("FiltroDeudas")
                    auxDR.Item("# Deudas") = dr.Item("CantidadDeudas")
                    dt.Rows.Add(auxDR)
                Next


            ElseIf int_PresentacionReporte = 74 Then ' Deudas Generadas

                ds_Lista = obj_BL_Pensiones.FUN_REP_DeudasPorConceptosYAulaTotales( _
                    int_CodigoPeriodoAcademico, str_CodigoConceptoCobro, dt_FechaRangoFin, int_CodigoGrado, int_CodigoAula, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "fila", "Integer")
                dt = Datos.agregarColumna(dt, "grado", "Integer")
                dt = Datos.agregarColumna(dt, "aula", "Integer")
                dt = Datos.agregarColumna(dt, "gradoaula", "string")

                Dim auxDR As DataRow
                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("fila") = dr.Item("fila")
                    auxDR.Item("grado") = dr.Item("grado")
                    auxDR.Item("aula") = dr.Item("aula")
                    auxDR.Item("gradoaula") = dr.Item("gradoaula")
                    dt.Rows.Add(auxDR)
                Next

            End If

        ElseIf int_TipoReporte = 15 Then ' Reporte : Reportes de Becas
            str_TituloReporte = "MiReporte" '
            If int_PresentacionReporte = 23 Then ' Resumen de Becas
                int_CodigoPeriodoAcademico = ddlRep3_Periodo.SelectedValue
                int_CodigoNivelMinisterio = ddlRep3_NivelMinisterio.SelectedValue
                int_CodigoGrado = ddlRep3_Grado.SelectedValue
                int_CodigoMotivoBeca = ddlRep3_MotivoBeca.SelectedValue
                int_CodigoTipoBeca = ddlRep3_TipoBeca.SelectedValue
                ds_Lista = obj_BL_Pensiones.FUN_REP_ResumenBecasXPeriodo(int_CodigoPeriodoAcademico, int_CodigoNivelMinisterio, int_CodigoGrado, int_CodigoMotivoBeca, int_CodigoTipoBeca, _
                   int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                dt = ds_Lista.Tables(0)
            ElseIf int_PresentacionReporte = 20 Or int_PresentacionReporte = 21 Or int_PresentacionReporte = 22 Then 'Resumen

                int_CodigoPeriodoAcademico = ddlRep4_Periodo.SelectedValue
                int_Nivel = ddlRep4_Nivel.SelectedValue
                int_SubNivel = ddlRep4_SubNivel.SelectedValue
                int_CodigoGrado = ddlRep4_Grado.SelectedValue
                int_CodigoAula = ddlRep4_Aula.SelectedValue
                If int_PresentacionReporte = 20 Then
                    int_Mes = ddlRep4_Mes.SelectedValue
                    ds_Lista = obj_BL_Pensiones.FUN_REP_BecaXmes(int_CodigoPeriodoAcademico, int_Mes, int_Nivel, int_SubNivel, _
                    int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                    dt = ds_Lista.Tables(0)
                    dt_TipoBeca = ds_Lista.Tables(1)
                End If
                If int_PresentacionReporte = 21 Then
                    If ddlRep4_TipoBeca.SelectedValue = 0 Then
                        int_CodigoTipoBeca = 7
                    Else
                        int_CodigoTipoBeca = ddlRep4_TipoBeca.SelectedValue
                    End If
                    int_Mes = ddlRep4_Mes.SelectedValue
                    ds_Lista = obj_BL_Pensiones.FUN_REP_TipoBecaNivelGradoAula(int_CodigoPeriodoAcademico, int_Mes, int_Nivel, int_SubNivel, _
                    int_CodigoGrado, int_CodigoAula, int_CodigoTipoBeca, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                    dt = ds_Lista.Tables(0)
                End If
                If int_PresentacionReporte = 22 Then
                    int_CodigoTipoBeca = ddlRep4_TipoBeca.SelectedValue
                    int_Mes = ddlRep4_Mes.SelectedValue
                    ds_Lista = obj_BL_Pensiones.FUN_REP_TipoBecaOtorgada(int_CodigoPeriodoAcademico, int_CodigoTipoBeca, int_Nivel, int_SubNivel, _
                    int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                    dt = ds_Lista.Tables(0)
                    dt_TipoBeca = ds_Lista.Tables(1)
                End If
            End If

        ElseIf int_TipoReporte = 24 Then ' Reporte : Reporte Contable

            str_TituloReporte = "MiReporte" '

            int_CodigoPeriodoAcademico = Me.Master.Obtener_CodigoPeriodoEscolar
            dt_FechaRangoInicio = tbRep5_FechaInicio.Text
            dt_FechaRangoFin = tbRep5_FechaFin.Text

            If int_PresentacionReporte = 41 Then ' General

                ds_Lista = obj_BL_Pensiones.FUN_REP_PagosRealizadosExportacionSIE_General( _
                    int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            ElseIf int_PresentacionReporte = 42 Then ' Cancelados

                ds_Lista = obj_BL_Pensiones.FUN_REP_PagosRealizadosExportacionSIE_Cancelados( _
                    int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)


            ElseIf int_PresentacionReporte = 43 Then ' Emitidos

                ds_Lista = obj_BL_Pensiones.FUN_REP_PagosRealizadosExportacionSIE_Emitidos( _
                    int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            End If

            dt = Datos.agregarColumna(dt, "VOUCHER", "integer")
            dt = Datos.agregarColumna(dt, "NRO_DOC", "string")
            dt = Datos.agregarColumna(dt, "F_EMIS", "string")
            dt = Datos.agregarColumna(dt, "F_PAGO", "string")
            dt = Datos.agregarColumna(dt, "F_CANC_POST", "string")
            dt = Datos.agregarColumna(dt, "FORMA_PAGO", "string")
            dt = Datos.agregarColumna(dt, "CODIGO", "string")
            dt = Datos.agregarColumna(dt, "NOMBRE", "string")
            dt = Datos.agregarColumna(dt, "NGS", "string")
            dt = Datos.agregarColumna(dt, "ORIGEN", "string")
            dt = Datos.agregarColumna(dt, "CONCEPTO", "string")
            dt = Datos.agregarColumna(dt, "A_DEDUDA", "integer")
            dt = Datos.agregarColumna(dt, "M_DEUDA", "string")
            dt = Datos.agregarColumna(dt, "DOLAR_MONTO", "decimal")
            dt = Datos.agregarColumna(dt, "DOLAR_DESCUENTO", "decimal")
            dt = Datos.agregarColumna(dt, "DOLAR_MORA", "decimal")
            dt = Datos.agregarColumna(dt, "DOLAR_IGV", "decimal")
            dt = Datos.agregarColumna(dt, "DOLAR_TOTAL", "decimal")
            dt = Datos.agregarColumna(dt, "SOLES_MONTO", "decimal")
            dt = Datos.agregarColumna(dt, "SOLES_DESCUENTO", "decimal")
            dt = Datos.agregarColumna(dt, "SOLES_MORA", "decimal")
            dt = Datos.agregarColumna(dt, "SOLES_IGV", "decimal")
            dt = Datos.agregarColumna(dt, "SOLES_TOTAL", "decimal")
            dt = Datos.agregarColumna(dt, "OBSERVACION", "string")
            dt = Datos.agregarColumna(dt, "F_EMIDOCN", "string")
            dt = Datos.agregarColumna(dt, "NRO_DOCN", "string")

            dt = Datos.agregarColumna(dt, "CODDOC", "integer")
            dt = Datos.agregarColumna(dt, "DESDOC", "string")
            dt = Datos.agregarColumna(dt, "CODDOCREF", "integer")
            dt = Datos.agregarColumna(dt, "DESDOCREF", "string")
            dt = Datos.agregarColumna(dt, "DOCCAN", "string")
            dt = Datos.agregarColumna(dt, "TCAMBIO", "string")

            Dim cont As Integer = 1
            Dim auxDR As DataRow

            For Each dr As DataRow In ds_Lista.Tables(0).Rows
                auxDR = dt.NewRow
                auxDR.Item("VOUCHER") = dr.Item("VOUCHER")
                auxDR.Item("NRO_DOC") = dr.Item("NRO_DOC")
                auxDR.Item("F_EMIS") = dr.Item("F_EMIS")
                auxDR.Item("F_PAGO") = dr.Item("F_PAGO")
                auxDR.Item("F_CANC_POST") = dr.Item("F_CANC_POST")
                auxDR.Item("FORMA_PAGO") = dr.Item("FORMA_PAGO")
                auxDR.Item("CODIGO") = dr.Item("CODIGO")
                auxDR.Item("NOMBRE") = dr.Item("NOMBRE")
                auxDR.Item("NGS") = dr.Item("NGS")
                auxDR.Item("ORIGEN") = dr.Item("ORIGEN")
                auxDR.Item("CONCEPTO") = dr.Item("CONCEPTO")
                auxDR.Item("A_DEDUDA") = dr.Item("A_DEDUDA")
                auxDR.Item("M_DEUDA") = dr.Item("M_DEUDA")
                auxDR.Item("DOLAR_MONTO") = dr.Item("DOLAR_MONTO")
                auxDR.Item("DOLAR_DESCUENTO") = dr.Item("DOLAR_DESCUENTO")
                auxDR.Item("DOLAR_MORA") = dr.Item("DOLAR_MORA")
                auxDR.Item("DOLAR_IGV") = dr.Item("DOLAR_IGV")
                auxDR.Item("DOLAR_TOTAL") = dr.Item("DOLAR_TOTAL")
                auxDR.Item("SOLES_MONTO") = dr.Item("SOLES_MONTO")
                auxDR.Item("SOLES_DESCUENTO") = dr.Item("SOLES_DESCUENTO")
                auxDR.Item("SOLES_MORA") = dr.Item("SOLES_MORA")
                auxDR.Item("SOLES_IGV") = dr.Item("SOLES_IGV")
                auxDR.Item("SOLES_TOTAL") = dr.Item("SOLES_TOTAL")
                auxDR.Item("OBSERVACION") = dr.Item("OBSERVACION")
                auxDR.Item("F_EMIDOCN") = dr.Item("F_EMIDOCN")
                auxDR.Item("NRO_DOCN") = dr.Item("NRO_DOCN")

                auxDR.Item("CODDOC") = dr.Item("CodigoTalonario")
                auxDR.Item("DESDOC") = dr.Item("Talonario")
                auxDR.Item("CODDOCREF") = dr.Item("CodigoTalonarioReferencia")
                auxDR.Item("DESDOCREF") = dr.Item("TalonarioReferencia")
                auxDR.Item("DOCCAN") = dr.Item("Cancela")
                auxDR.Item("TCAMBIO") = dr.Item("TipoCambio")

                dt.Rows.Add(auxDR)
            Next

        ElseIf int_TipoReporte = 35 Then ' Reporte : Reporte de Proyecciones

            str_TituloReporte = "MiReporte" '

            int_CodigoPeriodoAcademico = ddlRep6_Periodo.Selectedvalue ' Me.Master.Obtener_CodigoPeriodoEscolar
            dt_FechaRangoInicio = tbRep6_FechaInicio.Text
            dt_FechaRangoFin = tbRep6_FechaFin.Text

            If int_PresentacionReporte = 68 Then ' Ingresos Generales

                ds_Lista = obj_BL_Pensiones.FUN_REP_ProyeccionIngresos( _
                    int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                dt = Datos.agregarColumna(dt, "Grados", "string")
                Dim auxDR As DataRow
                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Grados") = dr.Item("Grados")
                    dt.Rows.Add(auxDR)
                Next

            ElseIf int_PresentacionReporte = 69 Then ' Cutoas de Ingreso

                ds_Lista = obj_BL_Pensiones.FUN_REP_ProyeccionCuotasDeIngreso( _
                    int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                dt = Datos.agregarColumna(dt, "Grado", "string")
                Dim auxDR As DataRow
                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Grado") = dr.Item("Grado")
                    dt.Rows.Add(auxDR)
                Next

            End If

        ElseIf int_TipoReporte = 37 Then ' Reporte : Reporte de Morosidad

            str_TituloReporte = "MiReporte"
            int_CodigoPeriodoAcademico = ddlRep7_Periodo.SelectedValue
            dt_FechaRangoInicio = tbRep7_Fecha1.Text
            dt_FechaRangoFin = tbRep7_Fecha2.Text

            If int_PresentacionReporte = 71 Then ' Cuadro de Morosidad Anual

                ds_Lista = obj_BL_Pensiones.FUN_REP_Morosidad(int_CodigoPeriodoAcademico, _
                                                              int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "Fila", "Integer")
                dt = Datos.agregarColumna(dt, "Col", "Integer")
                dt = Datos.agregarColumna(dt, "Quincena", "string")

                Dim auxDR As DataRow
                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Fila") = dr.Item("Fila")
                    auxDR.Item("Col") = dr.Item("Col")
                    auxDR.Item("Quincena") = dr.Item("Quincena")
                    dt.Rows.Add(auxDR)
                Next

            ElseIf int_PresentacionReporte = 72 Then ' Cuadro de Morosidad Por Corte

                ds_Lista = obj_BL_Pensiones.FUN_REP_MorosidadPorCorte(dt_FechaRangoInicio, dt_FechaRangoFin, _
                                                            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "Fila", "Integer")
                dt = Datos.agregarColumna(dt, "Anio", "string")
                dt = Datos.agregarColumna(dt, "Mes", "string")
                dt = Datos.agregarColumna(dt, "Total", "Integer")
                dt = Datos.agregarColumna(dt, "Morosos", "Integer")
                dt = Datos.agregarColumna(dt, "MorososPor", "decimal")

                Dim auxDR As DataRow
                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("Fila") = dr.Item("Fila")
                    auxDR.Item("Anio") = dr.Item("Anio")
                    auxDR.Item("Mes") = dr.Item("Mes")
                    auxDR.Item("Total") = dr.Item("total")
                    auxDR.Item("Morosos") = dr.Item("totnocan")
                    auxDR.Item("MorososPor") = dr.Item("nocanpor")
                    dt.Rows.Add(auxDR)
                Next

            ElseIf int_PresentacionReporte = 73 Then ' Cuadro de Morosidad Historico Por Corte

                ds_Lista = obj_BL_Pensiones.FUN_REP_MorosidadHistoricoPorCorte(dt_FechaRangoInicio, _
                                                            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "fila", "Integer")
                dt = Datos.agregarColumna(dt, "grado", "Integer")
                dt = Datos.agregarColumna(dt, "aula", "Integer")
                dt = Datos.agregarColumna(dt, "gradoaula", "string")

                Dim auxDR As DataRow
                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("fila") = dr.Item("fila")
                    auxDR.Item("grado") = dr.Item("grado")
                    auxDR.Item("aula") = dr.Item("aula")
                    auxDR.Item("gradoaula") = dr.Item("gradoaula")
                    dt.Rows.Add(auxDR)
                Next

            End If

        End If

        'LLenado de reporte
        Dim NombreArchivo As String = ""
        Dim RutaMadre As String = ""
        Dim downloadBytes As Byte()

        If Not dt.Rows.Count > 0 Then
            Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
            Exit Sub
        End If

        If int_TipoReporte = 13 Then ' Reporte : Reportes de Pagos

            If int_PresentacionReporte = 16 Then ' Presentación : Diario por conceptos
                NombreArchivo = ExportarReporteDinamicoPagosDiariosPorConceptos(dt, str_TituloReporte)
            ElseIf int_PresentacionReporte = 17 Then ' Presentación : Por concepto y salón
                NombreArchivo = ExportarReporteDinamicoPagosDiariosPorConceptosYAula(dt, str_TituloReporte)
            ElseIf int_PresentacionReporte = 19 Then ' Presentación : Resumen de Pagos Realizados
                NombreArchivo = ExportarReporteDinamicoPagosRealizadosPorPeriodo(dt, str_TituloReporte)
            End If

        ElseIf int_TipoReporte = 14 Then ' Reporte : Reportes de Deudas

            If int_PresentacionReporte = 18 Then ' Presentación : Diario por conceptos
                NombreArchivo = ExportarReporteDinamicoDeudasPorConceptosYAula(dt, str_TituloReporte)
            ElseIf int_PresentacionReporte = 25 Then ' Presentación : Deudas Generadas
                NombreArchivo = ExportarReporteDinamicoDeudasPorConceptosYAula(dt, str_TituloReporte)

            ElseIf int_PresentacionReporte = 74 Then ' Presentación : Por Niveles

                Dim str_PeriodoAcademico As String = ddlRep7_Periodo.SelectedItem.ToString
                Dim dt_FechaCorte1 As Date = tbRep7_Fecha1.Text

                NombreArchivo = ExportarReporteDeudasPorNivel(ds_Lista, str_TituloReporte, str_PeriodoAcademico, dt_FechaCorte1)
                NombreArchivo = NombreArchivo
                downloadBytes = File.ReadAllBytes(NombreArchivo)

                Response.Clear()
                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=ReporteDeudasPorNivel" & str_PeriodoAcademico & ".xlsx; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()



            End If

        ElseIf int_TipoReporte = 15 Then ' Reporte : Reportes de becas

            If int_PresentacionReporte = 23 Then ' Presentación : Diario por conceptos
                NombreArchivo = ExportarReporteResumenBecas(ds_Lista, str_TituloReporte)
            ElseIf int_PresentacionReporte = 20 Then
                NombreArchivo = ExportarReporteBecaXmes(dt, dt_TipoBeca, str_TituloReporte)
            ElseIf int_PresentacionReporte = 21 Then
                NombreArchivo = ExportarReporteTipoBecaNivelGradoAula(dt, str_TituloReporte)
            ElseIf int_PresentacionReporte = 22 Then
                NombreArchivo = ExportarReporteBecaOtorgado(dt, dt_TipoBeca, str_TituloReporte)
            End If

        ElseIf int_TipoReporte = 24 Then ' Reporte : Reporte Contable

            If int_PresentacionReporte = 41 Or _
                int_PresentacionReporte = 42 Or _
                int_PresentacionReporte = 43 Then ' Presentación : General, Cancelados, Emitidos
                NombreArchivo = ExportarReporteContableGeneral(dt, str_TituloReporte)
            End If

        ElseIf int_TipoReporte = 35 Then ' Reporte : Reporte de Proyecciones

            If int_PresentacionReporte = 68 Then ' Presentación : Ingresos Generales


                'NombreArchivo = ExportarReporteProyeccionesIngresos(ds_Lista, str_TituloReporte)

                Dim str_PeriodoAcademico As String = ddlRep7_Periodo.SelectedItem.ToString
                Dim str_Fecha1 As String = tbRep7_Fecha1.Text
                Dim str_Fecha2 As String = tbRep7_Fecha1.Text

                NombreArchivo = ExportarReporteProyeccionesIngresos2(ds_Lista, str_TituloReporte, str_PeriodoAcademico, str_Fecha1, str_Fecha2)
                NombreArchivo = NombreArchivo
                downloadBytes = File.ReadAllBytes(NombreArchivo)

                Response.Clear()
                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=ReporteProyecciones_" & str_PeriodoAcademico & "_" & str_Fecha1 & "_" & str_Fecha2 & ".xlsx; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()



            ElseIf int_PresentacionReporte = 69 Then ' Presentación : Cuotas de Ingreso
                NombreArchivo = ExportarReporteProyeccionesCuotasDeIngreso(ds_Lista, str_TituloReporte)
            End If

        ElseIf int_TipoReporte = 37 Then ' Reporte : Reporte de Morosidad

            If int_PresentacionReporte = 71 Then ' Cuadro de Morosidad Anual

                Dim str_PeriodoAcademico As String = ddlRep7_Periodo.SelectedItem.ToString
                NombreArchivo = ExportarReporteMorasidad(ds_Lista, str_TituloReporte, str_PeriodoAcademico)
                NombreArchivo = NombreArchivo
                downloadBytes = File.ReadAllBytes(NombreArchivo)

                Response.Clear()
                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=ReporteCuadroMorosidad" & str_PeriodoAcademico & ".xlsx; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()

            ElseIf int_PresentacionReporte = 72 Then ' Cuadro de Morosidad Por Corte

                Dim str_PeriodoAcademico As String = ddlRep7_Periodo.SelectedItem.ToString
                Dim dt_FechaCorte1 As Date = tbRep7_Fecha1.Text
                Dim dt_FechaCorte2 As Date = tbRep7_Fecha2.Text

                NombreArchivo = ExportarReporteMorasidadPorCorte(ds_Lista, str_TituloReporte, str_PeriodoAcademico, dt_FechaCorte1, dt_FechaCorte2)
                NombreArchivo = NombreArchivo
                downloadBytes = File.ReadAllBytes(NombreArchivo)

                Response.Clear()
                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=ReporteMorosidadPorCorte" & str_PeriodoAcademico & ".xlsx; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()

            ElseIf int_PresentacionReporte = 73 Then ' Cuadro de Morosidad Historico Por Corte

                Dim str_PeriodoAcademico As String = ddlRep7_Periodo.SelectedItem.ToString
                Dim dt_FechaCorte1 As Date = tbRep7_Fecha1.Text
                Dim dt_FechaCorte2 As Date = tbRep7_Fecha2.Text

                NombreArchivo = ExportarReporteMorasidadHistoricoPorCorte(ds_Lista, str_TituloReporte, str_PeriodoAcademico, dt_FechaCorte1)
                NombreArchivo = NombreArchivo
                downloadBytes = File.ReadAllBytes(NombreArchivo)

                Response.Clear()
                Response.Charset = ""
                Response.ContentType = "binary/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=ReporteMorosidadHistoricoPorCorte" & str_PeriodoAcademico & ".xlsx; size=" + downloadBytes.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(downloadBytes)
                Response.End()

            End If

        End If

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


    Private Sub cargarComboAniosAcademicos()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep1_Periodo, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlRep2_Periodo, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlRep3_Periodo, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlRep4_Periodo, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlRep6_Periodo, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlRep7_Periodo, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    Private Sub cargarComboConceptoCobro()

        Dim int_CodigoPagina As Integer = Convert.ToInt16(ConfigurationManager.AppSettings("CodigoPagina_ReportesPensiones_CancelacionDiaria").ToString)
        Dim int_Estado As Integer = 1

        Dim obj_BL_ConceptosCobros As New bl_ConceptosCobros
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_ConceptosCobros.FUN_LIS_ConceptosCobrosPorModulo(int_CodigoPagina, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ddlRep1_rbConceptosCobro.DataSource = ds_Lista.Tables(0)
        ddlRep1_rbConceptosCobro.DataValueField = "Codigo"
        ddlRep1_rbConceptosCobro.DataTextField = "Descripcion"
        ddlRep1_rbConceptosCobro.DataBind()

        ddlRep2_rbConceptosCobro.DataSource = ds_Lista.Tables(0)
        ddlRep2_rbConceptosCobro.DataValueField = "Codigo"
        ddlRep2_rbConceptosCobro.DataTextField = "Descripcion"
        ddlRep2_rbConceptosCobro.DataBind()

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

#Region "Reportes 13"

#Region "Eventos"

    Protected Sub ddlRep1_Grados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep1_Grados.selectedvalue > 0 Then
                cargarComboAulas_Rep1()
            Else
                limpiarCombos(ddlRep1_Aulas, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboGrado_Rep1()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlRep1_Grados, ds_Lista, "Codigo", "DescripcionEspaniol", True, False)
        Controles.llenarCombo(ddlRep2_Grados, ds_Lista, "Codigo", "DescripcionEspaniol", True, False)

    End Sub

    Private Sub cargarComboAulas_Rep1()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(ddlRep1_Grados.selectedvalue, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlRep1_Aulas, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

#End Region

#End Region

#Region "Reportes 14"

#Region "Eventos"

    Protected Sub ddlRep2_Grados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep2_Grados.SelectedValue > 0 Then
                cargarComboAulas_Rep2()
            Else
                limpiarCombos(ddlRep2_Aulas, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboGrado_Rep2()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlRep2_Grados, ds_Lista, "Codigo", "DescripcionEspaniol", True, False)

    End Sub

    Private Sub cargarComboAulas_Rep2()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(ddlRep1_Grados.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlRep2_Aulas, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

#End Region

#End Region

#Region "Reportes 15"

#Region "Eventos"

    Protected Sub ddlRep3_NivelMinisterio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlRep3_NivelMinisterio.SelectedValue > 0 Then
                cargarComboGrado_Rep3()
            Else
                limpiarCombos(ddlRep3_Grado, True, False)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub lstPresentacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim int_TipoReporte As Integer = lstReportes.SelectedValue 'Tipo reporte
        If lstReportes.SelectedValue = 15 Then

            limpiarCombos(ddlRep4_Aula, True, False)
            limpiarCombos(ddlRep4_Grado, True, False)
            limpiarCombos(ddlRep4_Mes, False, True)
            limpiarCombos(ddlRep4_Nivel, True, False)
            limpiarCombos(ddlRep4_SubNivel, True, False)
            limpiarCombos(ddlRep4_TipoBeca, True, False)
            If lstPresentacion.SelectedIndex = 3 Then
                pnlReporte1.Visible = False
                pnlReporte2.Visible = False
                pnlReporte3.Visible = True
                pnlReporte4.Visible = False
            ElseIf lstPresentacion.SelectedIndex = 0 Then 'mes
                pnlReporte1.Visible = False
                pnlReporte2.Visible = False
                pnlReporte3.Visible = False
                pnlReporte4.Visible = True
                tr_Beca.Visible = False
                tr_Mes.Visible = True
                cargarComboMes_Rep4()
                cargarComboNivel_Rep4()
                'cargarComboTipoBeca_Rep4()
            ElseIf lstPresentacion.SelectedIndex = 2 Then 'Otorgadas
                pnlReporte1.Visible = False
                pnlReporte2.Visible = False
                pnlReporte3.Visible = False
                pnlReporte4.Visible = True
                tr_Beca.Visible = True
                tr_Mes.Visible = False
                'cargarComboMes_Rep4()
                cargarComboNivel_Rep4()
                cargarComboTipoBeca_Rep4()
            ElseIf lstPresentacion.SelectedIndex = 1 Then 'Tipobeca, Nivel, Grado, Aula
                pnlReporte1.Visible = False
                pnlReporte2.Visible = False
                pnlReporte3.Visible = False
                pnlReporte4.Visible = True
                tr_Beca.Visible = True
                tr_Mes.Visible = True
                cargarComboMes_Rep4()
                cargarComboNivel_Rep4()
                cargarComboTipoBeca_Rep4()

            End If
        End If

        If lstPresentacion.SelectedValue = 71 Then
            ddlRep7_Periodo.Enabled = True
            tbRep7_Fecha1.Enabled = False
            tbRep7_Fecha2.Enabled = False
        ElseIf lstPresentacion.SelectedValue = 72 Then
            ddlRep7_Periodo.Enabled = False
            tbRep7_Fecha1.Enabled = True
            tbRep7_Fecha2.Enabled = True
        ElseIf lstPresentacion.SelectedValue = 73 Then
            ddlRep7_Periodo.Enabled = False
            tbRep7_Fecha1.Enabled = True
            tbRep7_Fecha2.Enabled = False
        End If

    End Sub

    Private Function validarCombos() As Boolean

        Dim result As Boolean = True

        If pnlReporte4.Visible = True Then
            If lstPresentacion.SelectedIndex = 0 Or lstPresentacion.SelectedIndex = 1 Then
                If ddlRep4_Mes.SelectedValue = 0 Then
                    Me.Master.MostrarMensajeAlert("Debe de seleccionar un mes.")
                    result = False
                End If
            End If

            Return result
        End If
    End Function
    Protected Sub ddlRep4_Nivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If ddlRep4_Nivel.SelectedValue <> 0 Then
            cargarComboSubNivel_Rep4()
        Else
            limpiarCombos(ddlRep4_Aula, True, False)
            limpiarCombos(ddlRep4_Grado, True, False)
            limpiarCombos(ddlRep4_SubNivel, True, False)
        End If
    End Sub
    Protected Sub ddlRep4_SubNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If ddlRep4_SubNivel.SelectedValue <> 0 Then
            cargarComboGrado_Rep4()
        Else
            limpiarCombos(ddlRep4_Grado, True, False)
            limpiarCombos(ddlRep4_Aula, True, False)
        End If
    End Sub
    Protected Sub ddlRep4_Grado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If ddlRep4_Grado.SelectedValue <> 0 Then
            cargarComboAulas_Rep4()
        Else
            limpiarCombos(ddlRep4_Aula, True, False)
        End If

    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboNivelMinisterio_Rep3()

        Dim obj_BL_NivelesMinisterio As New bl_NivelesMinisterio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_NivelesMinisterio.FUN_LIS_NivelesMinisterio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlRep3_NivelMinisterio, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboMotivoBeca_Rep3()

        Dim obj_BL_MotivoBeca As New bl_MotivoBeca
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_MotivoBeca.FUN_LIS_MotivoBeca("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlRep3_MotivoBeca, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboTipoBeca_Rep3()

        Dim obj_BL_TipoBeca As New bl_TipoBeca
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_TipoBeca.FUN_LIS_TipoBeca("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlRep3_TipoBeca, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboGrado_Rep3()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_GradosXCodigoNivelMinisterio(ddlRep3_NivelMinisterio.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlRep3_Grado, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub cargarComboMes_Rep4()
        Dim ds_Lista As DataSet
        ds_Lista = Controles.ListaMeses()
        Controles.llenarCombo(ddlRep4_Mes, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub
 
    Private Sub cargarComboNivel_Rep4()
        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep4_Nivel, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboSubNivel_Rep4()
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoNivel As Integer = ddlRep4_Nivel.SelectedValue
        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep4_SubNivel, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboGrado_Rep4()
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoSubNivel As Integer = ddlRep4_SubNivel.SelectedValue
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(int_CodigoSubNivel, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep4_Grado, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboAulas_Rep4()
        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoGrado As Integer = ddlRep4_Grado.SelectedValue
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlRep4_Aula, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboTipoBeca_Rep4()

        Dim obj_BL_TipoBeca As New bl_TipoBeca
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_TipoBeca.FUN_LIS_TipoBeca("", 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlRep4_TipoBeca, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

#End Region

#End Region

#Region "Exportacion Reportes"

    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    Private Shared Function GetNewName() As String
        Dim sName As String = Convert.ToString(DateTime.Now.Ticks)
        Return sName
    End Function


    'Reporte Codigo : 13 - 16
    Public Shared Function ExportarReporteDinamicoPagosDiariosPorConceptos(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

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
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones13_16").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReportePagosDiariosPorConceptos(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinámico"
        oCells = oSheet.Cells

        'Pintado de Título
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Reporte de Cancelación Diaria Por Concepto"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 2), oCells(3, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            'Now.ToString("dddd, MMMM d, yyyy h:mm") & " " & Now.ToString("tt").ToLower()

        End With

        Dim int_cont As Integer = 0
        Dim str_DescTipo As String = ""

        Dim dv_Grado As DataView
        dv_Grado = dtReporte.DefaultView

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        objTablaDinamica.PivotCache.SourceData = "MiReporte!F5C2:F" & fila & "C15"
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
    Private Shared Function LlenarPlantillaReportePagosDiariosPorConceptos( _
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
            .Value = "Reporte de Cancelación Diaria"
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


    'Reporte Codigo : 13 - 17
    Public Shared Function ExportarReporteDinamicoPagosDiariosPorConceptosYAula(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

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
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones13_17").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReportePagosDiariosPorConceptosYAula(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinámico"
        oCells = oSheet.Cells

        'Pintado de Título
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Reporte de Cancelación Diaria Por Conceptos Y Aulas"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 2), oCells(3, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            'Now.ToString("dddd, MMMM d, yyyy h:mm") & " " & Now.ToString("tt").ToLower()

        End With

        Dim int_cont As Integer = 0
        Dim str_DescTipo As String = ""

        Dim dv_Grado As DataView
        dv_Grado = dtReporte.DefaultView

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        objTablaDinamica.PivotCache.SourceData = "MiReporte!F5C2:F" & fila & "C15"
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
    Private Shared Function LlenarPlantillaReportePagosDiariosPorConceptosYAula( _
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
            .Value = "Reporte de Cancelación Diaria"
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


    'Reporte Codigo : 13 - 19
    Public Shared Function ExportarReporteDinamicoPagosRealizadosPorPeriodo(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

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
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones13_19").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReportePagosRealizadosPorPeriodo(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinámico"
        oCells = oSheet.Cells

        'Pintado de Título
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Reporte de Pagos Realizados"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 2), oCells(3, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            'Now.ToString("dddd, MMMM d, yyyy h:mm") & " " & Now.ToString("tt").ToLower()

        End With

        Dim int_cont As Integer = 0
        Dim str_DescTipo As String = ""

        Dim dv_Grado As DataView
        dv_Grado = dtReporte.DefaultView

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        objTablaDinamica.PivotCache.SourceData = "MiReporte!F5C2:F" & fila & "C6"
        objTablaDinamica.PivotCache.Refresh()

        oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        'While int_cont <= dtReporte.Rows.Count - 1
        '    str_DescTipo = dtReporte.Rows(int_cont).Item("Grado")
        '    oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescTipo).ShowDetail = False
        '    int_cont = int_cont + 1
        'End While


        ' Formato
        Dim colIni As Integer = 4
        Dim colFin As Integer = colIni + 1
        fila = 5

        With oExcel.Range(oCells(fila, colIni), oCells(fila, colFin))
            .ColumnWidth = 5
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
            .WrapText = True
        End With

        colIni = colFin
        colFin = colIni + 13

        With oExcel.Range(oCells(fila, colIni), oCells(fila, colFin))
            .ColumnWidth = 14
            .WrapText = True
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
    Private Shared Function LlenarPlantillaReportePagosRealizadosPorPeriodo( _
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
            .Value = "Reporte de Cancelación Diaria"
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


    'Reporte Codigo : 14 - 18
    Public Shared Function ExportarReporteDinamicoDeudasPorConceptosYAula(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

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
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones14_18").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteDeudasPorConceptosYAula(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinámico"
        oCells = oSheet.Cells

        'Pintado de Título
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Reporte de Alumnos Deudores Por Conceptos Y Aulas"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 2), oCells(3, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            'Now.ToString("dddd, MMMM d, yyyy h:mm") & " " & Now.ToString("tt").ToLower()

        End With

        Dim int_cont As Integer = 0
        Dim str_DescTipo As String = ""

        Dim dv_Grado As DataView
        dv_Grado = dtReporte.DefaultView

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        objTablaDinamica.PivotCache.SourceData = "MiReporte!F5C2:F" & fila & "C12" ' C13
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
    Private Shared Function LlenarPlantillaReporteDeudasPorConceptosYAula( _
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
            .Value = "Reporte de Cancelación Diaria"
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


    'Reporte Codigo : 24 - 41
    Public Shared Function ExportarReporteContableGeneral(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones24_41").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        LlenarPlantillaReporteContableGeneral(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        'Pintado de Título
        'With oExcel.Range(oCells(2, 4), oCells(2, 4))
        '    .Merge()
        '    .HorizontalAlignment = 1
        '    .Font.Bold = True
        '    .Value = "Reporte Contable General"
        'End With

        'Pintado de Fecha 
        'With oExcel.Range(oCells(3, 4), oCells(3, 4))
        '    .Merge()
        '    .HorizontalAlignment = 1
        '    .Font.Bold = True
        '    .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
        'End With

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
    Private Shared Function LlenarPlantillaReporteContableGeneral( _
        ByVal dtReporte As System.Data.DataTable, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

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

        oExcel.Columns("A:A").Delete()
        oExcel.Rows("1:4").Delete()

        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function


    ' update 06/07/2012
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
        ElseIf idColumna = 45 Then
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


    'Reporte Codigo : 15
    Public Function ExportarReporteResumenBecas(ByVal dtReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones15").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        LlenarPlantillaReporteResumenBecas(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

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
    Public Function LlenarPlantillaReporteResumenBecas( _
        ByVal dsReporte As System.Data.DataSet, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String
        'Dim int_columna As Integer = 0
        Dim dtAlumnosBecados As DataTable
        Dim dtAlumnosSinRepetir As DataTable
        Dim dtAlumnosCantidadRepetidos As DataTable
        Dim dtGrado As DataTable
        Dim dtDeudasXMes As DataTable
        Dim dtCantBecadosXGrado As DataTable
        Dim dtCantTotalGeneral As DataTable
        Dim dtCantBecadosXMotivoBeca As DataTable
        Dim dtCantTotalBecadosXMotivoBeca As DataTable
        Dim int_fila As Integer = 5
        Dim int_columna As Integer = 2

        dtAlumnosBecados = dsReporte.Tables(0)
        dtAlumnosSinRepetir = dsReporte.Tables(1)
        dtAlumnosCantidadRepetidos = dsReporte.Tables(2)
        dtDeudasXMes = dsReporte.Tables(3)
        dtGrado = dsReporte.Tables(4)
        dtCantBecadosXGrado = dsReporte.Tables(5)
        dtCantTotalGeneral = dsReporte.Tables(6)
        dtCantBecadosXMotivoBeca = dsReporte.Tables(7)
        dtCantTotalBecadosXMotivoBeca = dsReporte.Tables(8)

        'Pintado de Titulo
        oExcel.Range(oCells(2, 5), oCells(2, 10)).Merge()
        oExcel.Range(oCells(2, 5), oCells(2, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(2, 5), oCells(2, 10)).Value = "RESUMEN DE BECAS - AÑO ACADÉMICO " & ddlRep3_Periodo.SelectedItem.ToString()
        oExcel.Range(oCells(2, 5), oCells(2, 10)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Merge()
        oExcel.Range(oCells(3, 5), oCells(3, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Font.Bold = True

        'Pintado de Cabecera estática 
        oExcel.Range(oCells(int_fila, int_columna), oCells(int_fila + 1, int_columna)).Merge()
        oExcel.Range(oCells(int_fila, int_columna), oCells(int_fila + 1, int_columna)).Value = "N° Expediente"
        oExcel.Range(oCells(int_fila, int_columna + 1), oCells(int_fila + 1, int_columna + 1)).Merge()
        oExcel.Range(oCells(int_fila, int_columna + 1), oCells(int_fila + 1, int_columna + 1)).Value = "Motivo"
        oExcel.Range(oCells(int_fila, int_columna + 2), oCells(int_fila + 1, int_columna + 2)).Merge()
        oExcel.Range(oCells(int_fila, int_columna + 2), oCells(int_fila + 1, int_columna + 2)).Value = "Año de Ingreso"
        oExcel.Range(oCells(int_fila, int_columna + 3), oCells(int_fila + 1, int_columna + 3)).Merge()
        oExcel.Range(oCells(int_fila, int_columna + 3), oCells(int_fila + 1, int_columna + 3)).Value = "Alumno"
        oExcel.Range(oCells(int_fila, int_columna + 4), oCells(int_fila + 1, int_columna + 4)).Merge()
        oExcel.Range(oCells(int_fila, int_columna + 4), oCells(int_fila + 1, int_columna + 4)).Value = "Grado"
        oExcel.Range(oCells(int_fila, int_columna + 5), oCells(int_fila + 1, int_columna + 5)).Merge()
        oExcel.Range(oCells(int_fila, int_columna + 5), oCells(int_fila + 1, int_columna + 5)).Value = "Pension S/."
        oExcel.Range(oCells(int_fila, int_columna + 6), oCells(int_fila, int_columna + 8)).Merge()
        oExcel.Range(oCells(int_fila, int_columna + 6), oCells(int_fila, int_columna + 8)).Value = "Descuentos"
        oExcel.Range(oCells(int_fila, int_columna + 6), oCells(int_fila, int_columna + 8)).HorizontalAlignment = 3
        oExcel.Range(oCells(int_fila + 1, int_columna + 6), oCells(int_fila + 1, int_columna + 6)).Value = "Bono S/."
        oExcel.Range(oCells(int_fila + 1, int_columna + 7), oCells(int_fila + 1, int_columna + 7)).Value = "Beca"
        oExcel.Range(oCells(int_fila + 1, int_columna + 8), oCells(int_fila + 1, int_columna + 8)).Value = "S/.Beca"
        oExcel.Range(oCells(int_fila, int_columna + 9), oCells(int_fila + 1, int_columna + 9)).Merge()
        oExcel.Range(oCells(int_fila, int_columna + 9), oCells(int_fila + 1, int_columna + 9)).Value = "Pago a realizar"
        oExcel.Range(oCells(int_fila, int_columna + 10), oCells(int_fila + 1, int_columna + 10)).Merge()
        oExcel.Range(oCells(int_fila, int_columna + 10), oCells(int_fila + 1, int_columna + 10)).Value = "Matricula"
        oExcel.Range(oCells(int_fila, int_columna + 11), oCells(int_fila, int_columna + 21)).Merge()
        oExcel.Range(oCells(int_fila, int_columna + 11), oCells(int_fila, int_columna + 21)).Value = "Detalle Por Mes" '11
        oExcel.Range(oCells(int_fila + 1, int_columna + 11), oCells(int_fila + 1, int_columna + 11)).Value = "Mar"
        oExcel.Range(oCells(int_fila + 1, int_columna + 12), oCells(int_fila + 1, int_columna + 12)).Value = "Abr"
        oExcel.Range(oCells(int_fila + 1, int_columna + 13), oCells(int_fila + 1, int_columna + 13)).Value = "May"
        oExcel.Range(oCells(int_fila + 1, int_columna + 14), oCells(int_fila + 1, int_columna + 14)).Value = "Jun"
        oExcel.Range(oCells(int_fila + 1, int_columna + 15), oCells(int_fila + 1, int_columna + 15)).Value = "Jul"
        oExcel.Range(oCells(int_fila + 1, int_columna + 16), oCells(int_fila + 1, int_columna + 16)).Value = "Ago"
        oExcel.Range(oCells(int_fila + 1, int_columna + 17), oCells(int_fila + 1, int_columna + 17)).Value = "Sep"
        oExcel.Range(oCells(int_fila + 1, int_columna + 18), oCells(int_fila + 1, int_columna + 18)).Value = "Oct"
        oExcel.Range(oCells(int_fila + 1, int_columna + 19), oCells(int_fila + 1, int_columna + 19)).Value = "Nov"
        oExcel.Range(oCells(int_fila + 1, int_columna + 20), oCells(int_fila + 1, int_columna + 20)).Value = "Dic"
        oExcel.Range(oCells(int_fila + 1, int_columna + 21), oCells(int_fila + 1, int_columna + 21)).Value = "Total"

        oExcel.Range(oCells(int_fila, 2), oCells(int_fila + 1, int_columna + 21)).HorizontalAlignment = 3
        oExcel.Range(oCells(int_fila, 2), oCells(int_fila + 1, int_columna + 21)).WrapText = True
        oExcel.Range(oCells(int_fila, 2), oCells(int_fila + 1, int_columna + 21)).Font.Bold = True
        oExcel.Range(oCells(int_fila, 2), oCells(int_fila + 1, int_columna + 21)).Interior.Color() = RGB(204, 255, 204)

        'Inmovilizar paneles
        Dim objRangoajuste4 As Microsoft.Office.Interop.Excel.Range = oExcel.Range("A7")
        objRangoajuste4.Select()
        oExcel.ActiveWindow.FreezePanes = True

        ' Detalle de Alumnos
        int_fila = 7
        int_columna = 2

        Dim int_contDv As Integer = 0
        Dim int_contFilas As Integer = 0
        Dim int_contGrado As Integer = 0
        Dim int_cantAlumRepetidos As Integer = 0
        Dim dec_SumTotalXGrado As Decimal = 0.0
        Dim dec_SumTotalGeneralXGrado As Decimal = 0.0


        Dim str_Grado As String = ""
        While int_contGrado <= dtGrado.Rows.Count - 1
            str_Grado = dtGrado.Rows(int_contGrado).Item("Grado").ToString()

            Dim dvAlumnosSinRepetirXGrado As DataView = dtAlumnosSinRepetir.DefaultView
            dvAlumnosSinRepetirXGrado.RowFilter = "1=1 and CodigoGrado=" & dtGrado.Rows(int_contGrado).Item("CodigoGrado").ToString

            If dvAlumnosSinRepetirXGrado.Count > 0 Then
                While int_contFilas <= dvAlumnosSinRepetirXGrado.Count - 1
                    Dim str_CodigoAlumno As String = ""
                    str_CodigoAlumno = dvAlumnosSinRepetirXGrado.Item(int_contFilas).Item("CodigoAlumno")

                    Dim dvAlumnosRept As DataView = dtAlumnosCantidadRepetidos.DefaultView
                    Dim dvPlantillaDeudas As DataView = dtDeudasXMes.DefaultView

                    Dim dec_total As Decimal = 0.0
                    dvAlumnosRept.RowFilter = "1=1 and CodigoAlumno=" & str_CodigoAlumno
                    dvPlantillaDeudas.RowFilter = "1=1 and CodigoAlumno=" & str_CodigoAlumno

                    If dvAlumnosRept.Count > 0 Then
                        int_cantAlumRepetidos = dvAlumnosRept.Item(0).Item("cantidadRepetidas")
                        oExcel.Cells(int_fila, int_columna + 1) = dvAlumnosSinRepetirXGrado.Item(int_contFilas).Item("DescMotivoBeca")
                        oExcel.Cells(int_fila, int_columna + 2) = dvAlumnosSinRepetirXGrado.Item(int_contFilas).Item("AnioIngreso")
                        oExcel.Cells(int_fila, int_columna + 3) = dvAlumnosSinRepetirXGrado.Item(int_contFilas).Item("NombreCompleto")
                        oExcel.Cells(int_fila, int_columna + 4) = dvAlumnosSinRepetirXGrado.Item(int_contFilas).Item("Grado")
                        oExcel.Range(oCells(int_fila, int_columna + 0), oCells(int_fila + int_cantAlumRepetidos - 1, int_columna + 0)).Merge()
                        oExcel.Range(oCells(int_fila, int_columna + 1), oCells(int_fila + int_cantAlumRepetidos - 1, int_columna + 1)).Merge()
                        oExcel.Range(oCells(int_fila, int_columna + 2), oCells(int_fila + int_cantAlumRepetidos - 1, int_columna + 2)).Merge()
                        oExcel.Range(oCells(int_fila, int_columna + 3), oCells(int_fila + int_cantAlumRepetidos - 1, int_columna + 3)).Merge()
                        oExcel.Range(oCells(int_fila, int_columna + 4), oCells(int_fila + int_cantAlumRepetidos - 1, int_columna + 4)).Merge()

                        While int_contDv <= dvPlantillaDeudas.Count - 1 '

                            oExcel.Cells(int_fila + int_contDv, int_columna + 5) = dvPlantillaDeudas.Item(int_contDv).Item("Pension")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 6) = dvPlantillaDeudas.Item(int_contDv).Item("Bono") 'IIf(dvPlantillaDeudas.Item(int_contDv).Item("Bono").Equals(DBNull.Value), 0, dvPlantillaDeudas.Item(int_contDv).Item("Bono")) '
                            oExcel.Cells(int_fila + int_contDv, int_columna + 7) = dvPlantillaDeudas.Item(int_contDv).Item("TipoBeca") & "%"
                            oExcel.Cells(int_fila + int_contDv, int_columna + 8) = dvPlantillaDeudas.Item(int_contDv).Item("BecaSoles")

                            oExcel.Cells(int_fila + int_contDv, int_columna + 9) = dvPlantillaDeudas.Item(int_contDv).Item("PagoRealizar")
                            'oExcel.Cells(int_fila + int_contDv, int_columna + 8) = dvPlantillaDeudas.Item(int_contDv).Item("Pension")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 10) = dvPlantillaDeudas.Item(int_contDv).Item("Matricula")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 11) = dvPlantillaDeudas.Item(int_contDv).Item("Mar")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 12) = dvPlantillaDeudas.Item(int_contDv).Item("Abr")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 13) = dvPlantillaDeudas.Item(int_contDv).Item("May")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 14) = dvPlantillaDeudas.Item(int_contDv).Item("Jun")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 15) = dvPlantillaDeudas.Item(int_contDv).Item("Jul")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 16) = dvPlantillaDeudas.Item(int_contDv).Item("Ago")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 17) = dvPlantillaDeudas.Item(int_contDv).Item("Sep")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 18) = dvPlantillaDeudas.Item(int_contDv).Item("Oct")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 19) = dvPlantillaDeudas.Item(int_contDv).Item("Nov")
                            oExcel.Cells(int_fila + int_contDv, int_columna + 20) = dvPlantillaDeudas.Item(int_contDv).Item("Dic")
                            dec_total = CDec(dvPlantillaDeudas.Item(int_contDv).Item("Matricula")) + CDec(dvPlantillaDeudas.Item(int_contDv).Item("Mar")) + CDec(dvPlantillaDeudas.Item(int_contDv).Item("Abr")) + _
                                CDec(dvPlantillaDeudas.Item(int_contDv).Item("May")) + CDec(dvPlantillaDeudas.Item(int_contDv).Item("Jun")) + CDec(dvPlantillaDeudas.Item(int_contDv).Item("Jul")) + _
                                CDec(dvPlantillaDeudas.Item(int_contDv).Item("Ago")) + CDec(dvPlantillaDeudas.Item(int_contDv).Item("Sep")) + CDec(dvPlantillaDeudas.Item(int_contDv).Item("Oct")) + _
                                CDec(dvPlantillaDeudas.Item(int_contDv).Item("Nov")) + CDec(dvPlantillaDeudas.Item(int_contDv).Item("Dic"))
                            oExcel.Cells(int_fila + int_contDv, int_columna + 21) = dec_total
                            dec_SumTotalXGrado = dec_SumTotalXGrado + dec_total

                            int_contDv = int_contDv + 1
                        End While

                        int_fila = int_fila + int_cantAlumRepetidos - 1
                    Else
                        int_cantAlumRepetidos = 0
                        oExcel.Cells(int_fila, int_columna + 0) = dvAlumnosSinRepetirXGrado.Item(int_contFilas).Item("CodigoExpediente")
                        oExcel.Cells(int_fila, int_columna + 1) = dvAlumnosSinRepetirXGrado.Item(int_contFilas).Item("DescMotivoBeca") 'dtAlumnosSinRepetir.Rows(int_contFilas).Item("DescMotivoBeca").ToString
                        oExcel.Cells(int_fila, int_columna + 2) = dvAlumnosSinRepetirXGrado.Item(int_contFilas).Item("AnioIngreso") 'dtAlumnosSinRepetir.Rows(int_contFilas).Item("AnioIngreso").ToString
                        oExcel.Cells(int_fila, int_columna + 3) = dvAlumnosSinRepetirXGrado.Item(int_contFilas).Item("NombreCompleto") 'dtAlumnosSinRepetir.Rows(int_contFilas).Item("NombreCompleto").ToString
                        oExcel.Cells(int_fila, int_columna + 4) = dvAlumnosSinRepetirXGrado.Item(int_contFilas).Item("Grado") 'dtAlumnosSinRepetir.Rows(int_contFilas).Item("Grado").ToString

                        oExcel.Cells(int_fila, int_columna + 5) = dvPlantillaDeudas.Item(0).Item("Pension")
                        oExcel.Cells(int_fila, int_columna + 6) = dvPlantillaDeudas.Item(0).Item("Bono")
                        oExcel.Cells(int_fila, int_columna + 7) = dvPlantillaDeudas.Item(0).Item("TipoBeca") & "%"
                        oExcel.Cells(int_fila, int_columna + 8) = dvPlantillaDeudas.Item(0).Item("BecaSoles")
                        oExcel.Cells(int_fila, int_columna + 9) = dvPlantillaDeudas.Item(0).Item("PagoRealizar")
                        'oExcel.Cells(int_fila, int_columna + 8) = dvPlantillaDeudas.Item(0).Item("Pension")
                        oExcel.Cells(int_fila, int_columna + 10) = dvPlantillaDeudas.Item(0).Item("Matricula")
                        oExcel.Cells(int_fila, int_columna + 11) = dvPlantillaDeudas.Item(0).Item("Mar")
                        oExcel.Cells(int_fila, int_columna + 12) = dvPlantillaDeudas.Item(0).Item("Abr")
                        oExcel.Cells(int_fila, int_columna + 13) = dvPlantillaDeudas.Item(0).Item("May")
                        oExcel.Cells(int_fila, int_columna + 14) = dvPlantillaDeudas.Item(0).Item("Jun")
                        oExcel.Cells(int_fila, int_columna + 15) = dvPlantillaDeudas.Item(0).Item("Jul")
                        oExcel.Cells(int_fila, int_columna + 16) = dvPlantillaDeudas.Item(0).Item("Ago")
                        oExcel.Cells(int_fila, int_columna + 17) = dvPlantillaDeudas.Item(0).Item("Sep")
                        oExcel.Cells(int_fila, int_columna + 18) = dvPlantillaDeudas.Item(0).Item("Oct")
                        oExcel.Cells(int_fila, int_columna + 19) = dvPlantillaDeudas.Item(0).Item("Nov")
                        oExcel.Cells(int_fila, int_columna + 20) = dvPlantillaDeudas.Item(0).Item("Dic")
                        dec_total = CDec(dvPlantillaDeudas.Item(0).Item("Matricula")) + CDec(dvPlantillaDeudas.Item(0).Item("Mar")) + CDec(dvPlantillaDeudas.Item(0).Item("Abr")) + _
                        CDec(dvPlantillaDeudas.Item(0).Item("May")) + CDec(dvPlantillaDeudas.Item(0).Item("Jun")) + CDec(dvPlantillaDeudas.Item(0).Item("Jul")) + _
                        CDec(dvPlantillaDeudas.Item(0).Item("Ago")) + CDec(dvPlantillaDeudas.Item(0).Item("Sep")) + CDec(dvPlantillaDeudas.Item(0).Item("Oct")) + _
                        CDec(dvPlantillaDeudas.Item(0).Item("Nov")) + CDec(dvPlantillaDeudas.Item(0).Item("Dic"))
                        oExcel.Cells(int_fila, int_columna + 21) = dec_total
                        dec_SumTotalXGrado = dec_SumTotalXGrado + dec_total
                    End If
                    int_fila = int_fila + 1
                    oExcel.Cells(int_fila, int_columna + 4) = "Total " & str_Grado ''era 4

                    Dim dvCantBecadosXGrado As DataView = dtCantBecadosXGrado.DefaultView
                    dvCantBecadosXGrado.RowFilter = "1=1 and CodigoGrado=" & dtGrado.Rows(int_contGrado).Item("CodigoGrado").ToString

                    If dvCantBecadosXGrado.Count > 0 Then
                        'oExcel.Cells(int_fila, int_columna + 5) = dvCantBecadosXGrado.Item(0).Item("SumaTipoBeca")
                        oExcel.Cells(int_fila, int_columna + 5) = dvCantBecadosXGrado.Item(0).Item("SumaPension")
                        oExcel.Cells(int_fila, int_columna + 6) = dvCantBecadosXGrado.Item(0).Item("SumaBono")
                        oExcel.Cells(int_fila, int_columna + 8) = dvCantBecadosXGrado.Item(0).Item("SumaBecadosSoles")
                        oExcel.Cells(int_fila, int_columna + 9) = dvCantBecadosXGrado.Item(0).Item("SumaPagoRealizar")
                        'oExcel.Cells(int_fila, int_columna + 10) = dvCantBecadosXGrado.Item(0).Item("SumaPension")
                        oExcel.Cells(int_fila, int_columna + 10) = dvCantBecadosXGrado.Item(0).Item("SumaMatricula")
                        oExcel.Cells(int_fila, int_columna + 11) = dvCantBecadosXGrado.Item(0).Item("SumaMar")
                        oExcel.Cells(int_fila, int_columna + 12) = dvCantBecadosXGrado.Item(0).Item("SumaAbr")
                        oExcel.Cells(int_fila, int_columna + 13) = dvCantBecadosXGrado.Item(0).Item("SumaMay")
                        oExcel.Cells(int_fila, int_columna + 14) = dvCantBecadosXGrado.Item(0).Item("SumaJun")
                        oExcel.Cells(int_fila, int_columna + 15) = dvCantBecadosXGrado.Item(0).Item("SumaJul")
                        oExcel.Cells(int_fila, int_columna + 16) = dvCantBecadosXGrado.Item(0).Item("SumaAgo")
                        oExcel.Cells(int_fila, int_columna + 17) = dvCantBecadosXGrado.Item(0).Item("SumaSep")
                        oExcel.Cells(int_fila, int_columna + 18) = dvCantBecadosXGrado.Item(0).Item("SumaOct")
                        oExcel.Cells(int_fila, int_columna + 19) = dvCantBecadosXGrado.Item(0).Item("SumaNov")
                        oExcel.Cells(int_fila, int_columna + 20) = dvCantBecadosXGrado.Item(0).Item("SumaDic")

                    End If

                    int_contFilas = int_contFilas + 1
                    int_contDv = 0
                    int_cantAlumRepetidos = 0

                End While
                'oExcel.Cells(int_fila, int_columna + 4) = "Cantidad = " & int_contFilas.ToString
                'pintado de totales grados
                oExcel.Range(oCells(int_fila, 2), oCells(int_fila, int_columna + 21)).Font.Bold = True
                oExcel.Range(oCells(int_fila, 2), oCells(int_fila, int_columna + 21)).Interior.Color() = RGB(204, 204, 204)

                oExcel.Cells(int_fila, int_columna + 21) = dec_SumTotalXGrado
                dec_SumTotalGeneralXGrado = dec_SumTotalGeneralXGrado + dec_SumTotalXGrado
                int_fila = int_fila + 3
                int_contFilas = 0
                dec_SumTotalXGrado = 0.0
                'Else
                '    int_fila = int_fila + 3
            End If

            If dtGrado.Rows.Count - 1 = int_contGrado Then
                If dtCantTotalGeneral.Rows.Count > 0 Then
                    oExcel.Cells(int_fila - 2, int_columna + 4) = "Total General"
                    oExcel.Cells(int_fila - 2, int_columna + 5) = dtCantTotalGeneral.Rows(0).Item("SumaPension")
                    oExcel.Cells(int_fila - 2, int_columna + 6) = dtCantTotalGeneral.Rows(0).Item("SumaBono")
                    oExcel.Cells(int_fila - 2, int_columna + 8) = dtCantTotalGeneral.Rows(0).Item("SumaBecadosSoles")
                    oExcel.Cells(int_fila - 2, int_columna + 9) = dtCantTotalGeneral.Rows(0).Item("SumaPagoRealizar")

                    oExcel.Cells(int_fila - 2, int_columna + 10) = dtCantTotalGeneral.Rows(0).Item("SumaMatricula")
                    oExcel.Cells(int_fila - 2, int_columna + 11) = dtCantTotalGeneral.Rows(0).Item("SumaMar")
                    oExcel.Cells(int_fila - 2, int_columna + 12) = dtCantTotalGeneral.Rows(0).Item("SumaAbr")
                    oExcel.Cells(int_fila - 2, int_columna + 13) = dtCantTotalGeneral.Rows(0).Item("SumaMay")
                    oExcel.Cells(int_fila - 2, int_columna + 14) = dtCantTotalGeneral.Rows(0).Item("SumaJun")
                    oExcel.Cells(int_fila - 2, int_columna + 15) = dtCantTotalGeneral.Rows(0).Item("SumaJul")
                    oExcel.Cells(int_fila - 2, int_columna + 16) = dtCantTotalGeneral.Rows(0).Item("SumaAgo")
                    oExcel.Cells(int_fila - 2, int_columna + 17) = dtCantTotalGeneral.Rows(0).Item("SumaSep")
                    oExcel.Cells(int_fila - 2, int_columna + 18) = dtCantTotalGeneral.Rows(0).Item("SumaOct")
                    oExcel.Cells(int_fila - 2, int_columna + 19) = dtCantTotalGeneral.Rows(0).Item("SumaNov")
                    oExcel.Cells(int_fila - 2, int_columna + 20) = dtCantTotalGeneral.Rows(0).Item("SumaDic")
                    oExcel.Cells(int_fila - 2, int_columna + 21) = dec_SumTotalGeneralXGrado
                End If
            End If
            int_contGrado = int_contGrado + 1
        End While

        'pintado de total general de la plantilla
        oExcel.Range(oCells(int_fila - 2, 2), oCells(int_fila - 2, int_columna + 20)).Interior.Color() = RGB(204, 204, 204)
        oExcel.Range(oCells(int_fila - 2, 2), oCells(int_fila - 2, int_columna + 20)).Font.Bold = True

        'Pintado cabecera de Resumen x Gradoestática 
        'oExcel.Cells(int_fila, int_columna + 2) = "Resumen de Becas"
        oExcel.Cells(int_fila, int_columna + 3) = "Resumen de Becas"
        oExcel.Cells(int_fila, int_columna + 4) = "Cantidad de becas"
        oExcel.Cells(int_fila, int_columna + 5) = "Bono"
        oExcel.Cells(int_fila, int_columna + 6) = "S/."
        oExcel.Cells(int_fila, int_columna + 7) = "Mix Colegio"
        oExcel.Cells(int_fila, int_columna + 8) = "Resumen mensual"
        oExcel.Cells(int_fila, int_columna + 9) = "Matricula"
        oExcel.Cells(int_fila, int_columna + 10) = "Mar"
        oExcel.Cells(int_fila, int_columna + 11) = "Abr"
        oExcel.Cells(int_fila, int_columna + 12) = "May"
        oExcel.Cells(int_fila, int_columna + 13) = "Jun"
        oExcel.Cells(int_fila, int_columna + 14) = "Jul"
        oExcel.Cells(int_fila, int_columna + 15) = "Ago"
        oExcel.Cells(int_fila, int_columna + 16) = "Sep"
        oExcel.Cells(int_fila, int_columna + 17) = "Oct"
        oExcel.Cells(int_fila, int_columna + 18) = "Nov"
        oExcel.Cells(int_fila, int_columna + 19) = "Dic"
        oExcel.Cells(int_fila, int_columna + 20) = "Total"

        'Pintado y formato de cabecera de resumen X Grado
        oExcel.Range(oCells(int_fila, int_columna + 3), oCells(int_fila, int_columna + 20)).HorizontalAlignment = 3
        oExcel.Range(oCells(int_fila, int_columna + 3), oCells(int_fila, int_columna + 20)).WrapText = True
        oExcel.Range(oCells(int_fila, int_columna + 3), oCells(int_fila, int_columna + 20)).Font.Bold = True
        oExcel.Range(oCells(int_fila, int_columna + 3), oCells(int_fila, int_columna + 20)).Interior.Color() = RGB(204, 255, 204)

        'Resumen de Becas X Grado
        Dim int_contResumenBecasXGrado As Integer = 0
        int_fila = int_fila + 1
        Dim dec_Resumentotal As Decimal
        Dim dec_SumResumenTotalXGrado As Decimal
        Dim dec_mixColegio As Decimal
        Dim dec_TotalmixColegio As Decimal

        While int_contResumenBecasXGrado <= dtCantBecadosXGrado.Rows.Count - 1
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 3) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("Grado")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 4) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("cantidadBecadosGrado")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 5) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaBono") 'SumaTipoBeca
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 6) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaBecadosSoles")
            dec_mixColegio = FormatNumber(((dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaBecadosSoles") * 100) / dtCantTotalGeneral.Rows(0).Item("SumaBecadosSoles")), 2)
            dec_TotalmixColegio = dec_TotalmixColegio + dec_mixColegio
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 7) = dec_mixColegio & "%"
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 8) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("Grado")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 9) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaMatricula")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 10) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaMar")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 11) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaAbr")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 12) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaMay")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 13) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaJun")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 14) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaJul")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 15) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaAgo")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 16) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaSep")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 17) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaOct")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 18) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaNov")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 19) = dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaDic")
            dec_Resumentotal = CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaMatricula")) + CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaMar")) + CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaAbr")) + _
                             CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaMay")) + CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaJun")) + CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaJul")) + _
                             CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaAgo")) + CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaSep")) + CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaOct")) + _
                             CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaNov")) + CDec(dtCantBecadosXGrado.Rows(int_contResumenBecasXGrado).Item("SumaDic"))
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 20) = dec_Resumentotal
            dec_SumResumenTotalXGrado = dec_SumResumenTotalXGrado + dec_Resumentotal

            int_contResumenBecasXGrado = int_contResumenBecasXGrado + 1
        End While
        If dtCantTotalGeneral.Rows.Count > 0 Then

            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 3) = "Total"
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 4) = dtCantTotalGeneral.Rows(0).Item("SumacantidadBecadosGrado")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 5) = dtCantTotalGeneral.Rows(0).Item("SumaBono") 'SumaTipoBeca
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 6) = dtCantTotalGeneral.Rows(0).Item("SumaBecadosSoles")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 7) = dec_TotalmixColegio
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 8) = "Total"
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 9) = dtCantTotalGeneral.Rows(0).Item("SumaMatricula")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 10) = dtCantTotalGeneral.Rows(0).Item("SumaMar")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 11) = dtCantTotalGeneral.Rows(0).Item("SumaAbr")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 12) = dtCantTotalGeneral.Rows(0).Item("SumaMay")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 13) = dtCantTotalGeneral.Rows(0).Item("SumaJun")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 14) = dtCantTotalGeneral.Rows(0).Item("SumaJul")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 15) = dtCantTotalGeneral.Rows(0).Item("SumaAgo")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 16) = dtCantTotalGeneral.Rows(0).Item("SumaSep")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 17) = dtCantTotalGeneral.Rows(0).Item("SumaOct")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 18) = dtCantTotalGeneral.Rows(0).Item("SumaNov")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 19) = dtCantTotalGeneral.Rows(0).Item("SumaDic")
            oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 20) = dec_SumResumenTotalXGrado
        End If
        'Pintado del cuadrado de Resumen de becas X grado estática
        oExcel.Range(oExcel.Cells(int_fila - 1, 5), oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 20)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(int_fila - 1, 5), oExcel.Cells(int_fila + int_contResumenBecasXGrado, int_columna + 20)))
        oExcel.Range(oCells(int_fila + int_contResumenBecasXGrado, 5), oCells(int_fila + int_contResumenBecasXGrado, int_columna + 20)).Interior.Color() = RGB(204, 204, 204)
        oExcel.Range(oCells(int_fila + int_contResumenBecasXGrado, 5), oCells(int_fila + int_contResumenBecasXGrado, int_columna + 20)).Font.Bold = True
        'Pintado del cuadrado de Cabecera estática plantilla general
        oExcel.Range(oExcel.Cells(7, 5), oExcel.Cells(int_fila - 3, int_columna + 20)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(5, 2), oExcel.Cells(int_fila - 3, int_columna + 20)))

        'Resumen de Becas X Motivo
        Dim int_contResumenXMotivo As Integer = 0
        int_fila = int_fila + int_contResumenBecasXGrado + 2
        Dim dec_ResumenmixColegio As Decimal
        Dim dec_ResumenTotalmixColegio As Decimal

        While int_contResumenXMotivo <= dtCantBecadosXMotivoBeca.Rows.Count - 1
            oExcel.Cells(int_fila + int_contResumenXMotivo, int_columna + 3) = dtCantBecadosXMotivoBeca.Rows(int_contResumenXMotivo).Item("MotivoBeca")
            oExcel.Cells(int_fila + int_contResumenXMotivo, int_columna + 4) = dtCantBecadosXMotivoBeca.Rows(int_contResumenXMotivo).Item("cantidadBecadosXMotivo")
            oExcel.Cells(int_fila + int_contResumenXMotivo, int_columna + 5) = dtCantBecadosXMotivoBeca.Rows(int_contResumenXMotivo).Item("SumaBono") 'SumaTipoBeca
            oExcel.Cells(int_fila + int_contResumenXMotivo, int_columna + 6) = dtCantBecadosXMotivoBeca.Rows(int_contResumenXMotivo).Item("SumaBecadosSoles")

            dec_ResumenmixColegio = FormatNumber(((dtCantBecadosXMotivoBeca.Rows(int_contResumenXMotivo).Item("SumaBecadosSoles") * 100) / dtCantTotalBecadosXMotivoBeca.Rows(0).Item("SumaBecadosSoles")), 2)
            dec_ResumenTotalmixColegio = dec_ResumenTotalmixColegio + dec_ResumenmixColegio
            oExcel.Cells(int_fila + int_contResumenXMotivo, int_columna + 7) = dec_ResumenmixColegio & "%"

            int_contResumenXMotivo = int_contResumenXMotivo + 1
        End While
        If dtCantTotalBecadosXMotivoBeca.Rows.Count > 0 Then

            oExcel.Cells(int_fila + int_contResumenXMotivo, int_columna + 3) = "Total"
            oExcel.Cells(int_fila + int_contResumenXMotivo, int_columna + 4) = dtCantTotalBecadosXMotivoBeca.Rows(0).Item("SumacantidadBecadosXMotivo")
            oExcel.Cells(int_fila + int_contResumenXMotivo, int_columna + 5) = dtCantTotalBecadosXMotivoBeca.Rows(0).Item("SumaBono") 'SumaTipoBeca
            oExcel.Cells(int_fila + int_contResumenXMotivo, int_columna + 6) = dtCantTotalBecadosXMotivoBeca.Rows(0).Item("SumaBecadosSoles")
            oExcel.Cells(int_fila + int_contResumenXMotivo, int_columna + 7) = dec_ResumenTotalmixColegio & "%"
        End If
        'Pintado del cuadrado de Resumen de becas X Motivo estática
        oExcel.Range(oExcel.Cells(int_fila - 1, 5), oExcel.Cells(int_fila + int_contResumenXMotivo, 9)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(int_fila, 5), oExcel.Cells(int_fila + int_contResumenXMotivo, 9)))
        oExcel.Range(oCells(int_fila + int_contResumenXMotivo, 5), oCells(int_fila + int_contResumenXMotivo, 9)).Interior.Color() = RGB(204, 204, 204)
        oExcel.Range(oCells(int_fila + int_contResumenXMotivo, 5), oCells(int_fila + int_contResumenXMotivo, 9)).Font.Bold = True
        oExcel.ActiveWindow.Zoom = 75

        Return str_NombreEntidadReporte
    End Function


    ''termina 15
    Public Function ExportarReporteBecaXmes(ByVal dtReporte As System.Data.DataTable, ByVal dt_TipoBeca As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones15_20").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteBecaXmes(dtReporte, dt_TipoBeca, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Function LlenarPlantillaReporteBecaXmes( _
    ByVal dtReporte As System.Data.DataTable, _
    ByVal dt_TipoBeca As System.Data.DataTable, _
    ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
    ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
    ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 9
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""
        oExcel.Sheets("MiReporte").Select()
        'Pintado de Título
        With oExcel.Range(oCells(2, 4), oCells(2, 10))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "RELACIÓN DE ALUMNOS"
        End With
        With oExcel.Range(oCells(3, 4), oCells(3, 10))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "BECADOS POR MES"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(5, 4), oCells(5, 10))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 16
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        'Nivel, SubNivel, Grado, Aula
        oExcel.Range(oCells(7, 4), oCells(7, 10)).Merge()
        oExcel.Range(oCells(7, 4), oCells(7, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(7, 1), oCells(7, 10)).Font.Bold = True
        oExcel.Range("G:G").HorizontalAlignment = 3
        If ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 _
        And ddlRep4_Grado.SelectedValue <> 0 And ddlRep4_Aula.SelectedValue <> 0 Then
            'TipoBeca,Nivel, SubNivel, Grado, Aula
            oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
        "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
       "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
      "Grado: " & ddlRep4_Grado.SelectedItem.ToString & "                              " & _
       "Aula: " & ddlRep4_Aula.SelectedItem.ToString
        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue = 0 Then
            oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
            "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString
        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 And ddlRep4_Grado.SelectedValue = 0 Then
            oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
                "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString
        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 And ddlRep4_Grado.SelectedValue <> 0 And ddlRep4_Aula.SelectedValue = 0 Then
            oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
            "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
          "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
         "Grado: " & ddlRep4_Grado.SelectedItem.ToString

        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 And ddlRep4_Grado.SelectedValue <> 0 And ddlRep4_Aula.SelectedValue <> 0 Then
            oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
            "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
            "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
           "Grado: " & ddlRep4_Grado.SelectedItem.ToString & "                              " & _
            "Aula: " & ddlRep4_Aula.SelectedItem.ToString
        ElseIf ddlRep4_Nivel.SelectedValue = 0 Then
            oExcel.Range(oCells(7, 4), oCells(7, 10)).HorizontalAlignment = 2
            oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString
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

        oExcel.Range(oCells(9, 4), oCells(str_Fila, 10)).Font.Name = "Arial"
        oExcel.Range(oCells(9, 4), oCells(str_Fila, 10)).Font.Size = "14"

        'Copy el formato a todas las celdas
        oExcel.Rows("10:10").Select()
        oExcel.ActiveWindow.FreezePanes = True
        oExcel.Selection.Copy()
        oExcel.ActiveWindow.SmallScroll(Down:=-3)
        oExcel.Rows("11:" & str_Fila + 16).Select()
        oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)

        oExcel.Range(oCells(9, columna), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(9, columna), oCells(fila - 1, columna + cont_columnas - 1)))
        'Margen
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
        oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("J1")
        oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView

        '*********************
        '   Segunda(Tabla)
        '*********************

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_columnas <= dt_TipoBeca.Columns.Count - 1
            While cont_filas <= dt_TipoBeca.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas + 1) = dt_TipoBeca.Rows(cont_filas).Item(cont_columnas)
                cont_filas += 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        cuadradoCompleto(oExcel, oExcel.Range(oCells(str_Fila + 2, columna + 1), oCells(str_Fila + 16, columna + cont_columnas)))
        oExcel.Range("F" & str_Fila & ":F200").HorizontalAlignment = 3
        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("a10").Select()
        Return str_Fila
    End Function


    Public Function ExportarReporteTipoBecaNivelGradoAula(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones15_20").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteTipoBecaNivelGradoAula(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Function LlenarPlantillaReporteTipoBecaNivelGradoAula( _
    ByVal dtReporte As System.Data.DataTable, _
    ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
    ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
    ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 9
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""
        oExcel.Sheets("MiReporte").Select()
        'Pintado de Título
        With oExcel.Range(oCells(2, 4), oCells(2, 9))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "RELACIÓN DE ALUMNOS"
        End With
        With oExcel.Range(oCells(3, 4), oCells(3, 9))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "BECADOS POR TIPO DE BECA"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(5, 4), oCells(5, 9))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 16
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        'Nivel, SubNivel, Grado, Aula
        oExcel.Range(oCells(7, 4), oCells(7, 9)).Merge()
        oExcel.Range(oCells(7, 4), oCells(7, 9)).HorizontalAlignment = 3
        oExcel.Range(oCells(7, 4), oCells(7, 9)).Font.Bold = True
        If ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 _
        And ddlRep4_Grado.SelectedValue <> 0 And ddlRep4_Aula.SelectedValue <> 0 Then
            'TipoBeca,Nivel, SubNivel, Grado, Aula
            oExcel.Range(oCells(7, 4), oCells(7, 9)).Merge()
            oExcel.Range(oCells(7, 4), oCells(7, 9)).HorizontalAlignment = 3
            If ddlRep4_TipoBeca.SelectedValue <> 0 Then
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "          " & _
            "Tipo Beca:" & ddlRep4_TipoBeca.SelectedItem.ToString & "          " & _
           "Nivel:" & ddlRep4_Nivel.SelectedItem.ToString & "          " & _
           "SubNivel:" & ddlRep4_SubNivel.SelectedItem.ToString & "          " & _
          "Grado:" & ddlRep4_Grado.SelectedItem.ToString & "          " & _
           "Aula:" & ddlRep4_Aula.SelectedItem.ToString
            Else
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "          " & _
            "Beca: 50% Beneficio" & "          " & _
           "Nivel:" & ddlRep4_Nivel.SelectedItem.ToString & "          " & _
           "SubNivel:" & ddlRep4_SubNivel.SelectedItem.ToString & "          " & _
          "Grado:" & ddlRep4_Grado.SelectedItem.ToString & "          " & _
           "Aula:" & ddlRep4_Aula.SelectedItem.ToString
            End If

        ElseIf ddlRep4_Nivel.SelectedValue = 0 Then
            If ddlRep4_TipoBeca.SelectedValue <> 0 Then
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                                "Beca: " & ddlRep4_TipoBeca.SelectedItem.ToString
            Else
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                                "Beca: 50% Beneficio"
            End If

        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue = 0 Then

            If ddlRep4_TipoBeca.SelectedValue <> 0 Then
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                "Beca:" & ddlRep4_TipoBeca.SelectedItem.ToString & "                              " & _
                "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString
            Else
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                "Beca: 50% Beneficio" & "                              " & _
                "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString
            End If
        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 And ddlRep4_Grado.SelectedValue = 0 Then
            If ddlRep4_TipoBeca.SelectedValue <> 0 Then
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                "Beca:" & ddlRep4_TipoBeca.SelectedItem.ToString & "                              " & _
                "Nivel:" & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
                "SubNivel:" & ddlRep4_SubNivel.SelectedItem.ToString
            Else
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                "Beca: 50% Beneficio" & "                              " & _
                "Nivel:" & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
                "SubNivel:" & ddlRep4_SubNivel.SelectedItem.ToString
            End If

        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 And ddlRep4_Grado.SelectedValue <> 0 And ddlRep4_Aula.SelectedValue = 0 Then
            If ddlRep4_TipoBeca.SelectedValue <> 0 Then
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                "Beca:" & ddlRep4_TipoBeca.SelectedItem.ToString & "                              " & _
          "Nivel:" & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
          "SubNivel:" & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
         "Grado:" & ddlRep4_Grado.SelectedItem.ToString
            Else
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                "Beca: 50% Beneficio" & "                              " & _
          "Nivel:" & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
          "SubNivel:" & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
         "Grado:" & ddlRep4_Grado.SelectedItem.ToString
            End If

        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 And ddlRep4_Grado.SelectedValue <> 0 And ddlRep4_Aula.SelectedValue <> 0 Then
            oExcel.Range(oCells(7, 4), oCells(7, 9)).Merge()
            oExcel.Range(oCells(7, 4), oCells(7, 9)).HorizontalAlignment = 3
            If ddlRep4_TipoBeca.SelectedValue <> 0 Then
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                "Beca:" & ddlRep4_TipoBeca.SelectedItem.ToString & "                              " & _
            "Nivel:" & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
            "SubNivel:" & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
           "Grado:" & ddlRep4_Grado.SelectedItem.ToString & "                              " & _
            "Aula:" & ddlRep4_Aula.SelectedItem.ToString
            Else
                oCells(7, 4) = "Mes: " & ddlRep4_Mes.SelectedItem.ToString & "                              " & _
                "Beca: 50% Beneficio" & "                              " & _
            "Nivel:" & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
            "SubNivel:" & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
           "Grado:" & ddlRep4_Grado.SelectedItem.ToString & "                              " & _
            "Aula:" & ddlRep4_Aula.SelectedItem.ToString
            End If

        ElseIf ddlRep4_Nivel.SelectedValue = 0 Then

            fila = 8
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

        oExcel.Range(oCells(9, 4), oCells(str_Fila, 10)).Font.Name = "Arial"
        oExcel.Range(oCells(9, 4), oCells(str_Fila, 10)).Font.Size = "14"

        'Copy el formato a todas las celdas
        oExcel.Rows("10:10").Select()
        oExcel.ActiveWindow.FreezePanes = True
        oExcel.Selection.Copy()
        oExcel.ActiveWindow.SmallScroll(Down:=-3)
        oExcel.Rows("11:" & str_Fila).Select()
        oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)

        oExcel.Range(oCells(9, columna), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(9, columna), oCells(fila - 1, columna + cont_columnas - 1)))
        'Margen
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
        oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("I1")
        oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView

        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("a3").Select()
        Return str_Fila
    End Function


    Public Function ExportarReporteBecaOtorgado(ByVal dtReporte As System.Data.DataTable, ByVal dt_TipoBeca As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones15_20").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteBecaOtorgado(dtReporte, dt_TipoBeca, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Function LlenarPlantillaReporteBecaOtorgado( _
    ByVal dtReporte As System.Data.DataTable, _
    ByVal dt_TipoBeca As System.Data.DataTable, _
    ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
    ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
    ByVal str_NombreEntidadReporte As String) As String

        Dim fila As Integer = 9
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""
        oExcel.Sheets("MiReporte").Select()
        'Pintado de Título
        With oExcel.Range(oCells(3, 4), oCells(3, 10))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 20
            .Value = "RELACIÓN TOTAL DE ALUMNOS BECADOS"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(5, 4), oCells(5, 10))
            .Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Font.Name = "Arial"
            .Font.Size = 16
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        'Nivel, SubNivel, Grado, Aula
        oExcel.Range(oCells(7, 4), oCells(7, 10)).Merge()
        oExcel.Range(oCells(7, 4), oCells(7, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(7, 1), oCells(7, 10)).Font.Bold = True
        oExcel.Range("G:G").HorizontalAlignment = 3
        If ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 _
        And ddlRep4_Grado.SelectedValue <> 0 And ddlRep4_Aula.SelectedValue <> 0 Then
            'TipoBeca,Nivel, SubNivel, Grado, Aula
            If ddlRep4_TipoBeca.SelectedValue = 0 Then
                oCells(7, 4) = "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
      "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
     "Grado: " & ddlRep4_Grado.SelectedItem.ToString & "                              " & _
      "Aula: " & ddlRep4_Aula.SelectedItem.ToString
            Else
                oCells(7, 4) = "Beca: " & ddlRep4_TipoBeca.SelectedItem.ToString & "                              " & _
       "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
      "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
     "Grado: " & ddlRep4_Grado.SelectedItem.ToString & "                              " & _
      "Aula: " & ddlRep4_Aula.SelectedItem.ToString
            End If

        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue = 0 Then
            If ddlRep4_TipoBeca.SelectedValue = 0 Then
                oCells(7, 4) = "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString
            Else
                oCells(7, 4) = "Beca: " & ddlRep4_TipoBeca.SelectedItem.ToString & "                              " & _
            "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString
            End If

        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 And ddlRep4_Grado.SelectedValue = 0 Then
            If ddlRep4_TipoBeca.SelectedValue = 0 Then
                oCells(7, 4) = "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
                "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString
            Else
                oCells(7, 4) = "Beca: " & ddlRep4_TipoBeca.SelectedItem.ToString & "                              " & _
                "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
                "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString
            End If

        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 And ddlRep4_Grado.SelectedValue <> 0 And ddlRep4_Aula.SelectedValue = 0 Then
            If ddlRep4_TipoBeca.SelectedValue = 0 Then
                oCells(7, 4) = "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
                         "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
                        "Grado: " & ddlRep4_Grado.SelectedItem.ToString
            Else
                oCells(7, 4) = "Beca: " & ddlRep4_TipoBeca.SelectedItem.ToString & "                              " & _
                          "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
                        "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
                       "Grado: " & ddlRep4_Grado.SelectedItem.ToString
            End If

        ElseIf ddlRep4_Nivel.SelectedValue <> 0 And ddlRep4_SubNivel.SelectedValue <> 0 And ddlRep4_Grado.SelectedValue <> 0 And ddlRep4_Aula.SelectedValue <> 0 Then
            If ddlRep4_TipoBeca.SelectedValue = 0 Then
                oCells(7, 4) = "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
            "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
           "Grado: " & ddlRep4_Grado.SelectedItem.ToString & "                              " & _
            "Aula: " & ddlRep4_Aula.SelectedItem.ToString
            Else
                oCells(7, 4) = "Beca: " & ddlRep4_TipoBeca.SelectedItem.ToString & "                              " & _
            "Nivel: " & ddlRep4_Nivel.SelectedItem.ToString & "                              " & _
            "SubNivel: " & ddlRep4_SubNivel.SelectedItem.ToString & "                              " & _
           "Grado: " & ddlRep4_Grado.SelectedItem.ToString & "                              " & _
            "Aula: " & ddlRep4_Aula.SelectedItem.ToString
            End If

        ElseIf ddlRep4_Nivel.SelectedValue = 0 Then
            If ddlRep4_TipoBeca.SelectedValue <> 0 Then
                oExcel.Range(oCells(7, 4), oCells(7, 10)).HorizontalAlignment = 2
                oCells(7, 4) = "Beca: " & ddlRep4_TipoBeca.SelectedItem.ToString
            End If

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

        oExcel.Range(oCells(9, 4), oCells(str_Fila, 10)).Font.Name = "Arial"
        oExcel.Range(oCells(9, 4), oCells(str_Fila, 10)).Font.Size = "14"

        'Copy el formato a todas las celdas
        oExcel.Rows("10:10").Select()
        oExcel.ActiveWindow.FreezePanes = True
        oExcel.Selection.Copy()
        oExcel.ActiveWindow.SmallScroll(Down:=-3)
        oExcel.Rows("11:" & str_Fila + 16).Select()
        oExcel.Selection.PasteSpecial(Paste:=Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormats)

        oExcel.Range(oCells(9, columna), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(9, columna), oCells(fila - 1, columna + cont_columnas - 1)))
        'Margen
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
        oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("J1")
        oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView

        '*********************
        '   Segunda(Tabla)
        '*********************

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_columnas <= dt_TipoBeca.Columns.Count - 1
            While cont_filas <= dt_TipoBeca.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas + 1) = dt_TipoBeca.Rows(cont_filas).Item(cont_columnas)
                cont_filas += 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        cuadradoCompleto(oExcel, oExcel.Range(oCells(str_Fila + 2, columna + 1), oCells(str_Fila + 16, columna + cont_columnas)))
        oExcel.Range("F" & str_Fila & ":F200").HorizontalAlignment = 3
        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("a10").Select()

        Return str_Fila
    End Function


    ' Reporte Codigo : 35 - 68 (Proyeccion : Ingresos Generales)
    Public Shared Function ExportarReporteProyeccionesIngresos(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones24_41").ToString() ' en blanco

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        LlenarPlantillaReporteProyeccionesIngresos(dsReporte, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Shared Function LlenarPlantillaReporteProyeccionesIngresos( _
        ByVal dsReporte As System.Data.DataSet, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim dtReporte As New System.Data.DataTable
        dtReporte = dsReporte.Tables(0).Copy

        ' Posiciones iniciales de pintado
        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        Dim lstPos As New List(Of posicionCelda)
        Dim str_Rango As String = ""

        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("A:A").ColumnWidth = 4

        ' TITULO
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Value = "INGRESOS COLEGIO " & dsReporte.Tables(1).Rows(0).Item("anio")
            .Font.Size = 24
            .Font.Bold = True
            .Font.Color = RGB(0, 0, 0)
            .RowHeight = 30
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        ' CUADRO 1: Costo de Pensiones
        With oExcel.Range(oCells(4, 2), oCells(4, 2))
            .Value = "SUPUESTOS"
            .ColumnWidth = 45
            .Font.Bold = True
            .Font.Color = RGB(0, 0, 0)
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        fila = 5
        columna = 2
        cont_columnas = 0
        cont_filas = 0

        Dim posCel_01 As New posicionCelda
        posCel_01.posfilaini = fila
        posCel_01.poscolini = columna

        Dim bool_cabecera As Boolean = True
        While cont_filas <= dsReporte.Tables(0).Rows.Count - 1
            While cont_columnas <= dsReporte.Tables(0).Columns.Count - 1
                If bool_cabecera Then
                    With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                        .Value = dsReporte.Tables(0).Columns(cont_columnas).ColumnName
                        .Font.Bold = True
                        .Interior.Color() = RGB(255, 255, 0)
                        .Font.Color = RGB(0, 0, 0)
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                    End With
                End If
                With oExcel.Range(oCells(fila + cont_filas + 1, columna + cont_columnas), oCells(fila + cont_filas + 1, columna + cont_columnas))
                    .Value = dsReporte.Tables(0).Rows(cont_filas).Item(cont_columnas)
                    .NumberFormat = "#,##0.00"
                End With
                cont_columnas += 1
            End While
            bool_cabecera = False
            If cont_filas < dsReporte.Tables(3).Rows.Count - 1 Then
                cont_columnas = 0
            End If
            cont_filas += 1
        End While

        fila = fila + (dsReporte.Tables(0).Rows.Count - 1) + 1 ' fila: 9

        posCel_01.posfilafin = fila
        posCel_01.poscolfin = columna + (dsReporte.Tables(0).Columns.Count - 1)
        lstPos.Add(posCel_01)

        fila += 2 ' fila: 11
        With oExcel.Range(oCells(fila, 2), oCells(fila, 2))
            .Value = "PROYECCIÓN ALUMNADO " & dsReporte.Tables(1).Rows(0).Item("anio")
            .Font.Bold = True
            .Font.Color = RGB(0, 0, 0)
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        ' CUADRO 2: Tipo de Cambio
        fila += 1 ' fila: 12
        columna = 2
        cont_columnas = 0
        cont_filas = 0

        Dim posCel_03 As New posicionCelda
        With posCel_03
            .posfilaini = fila + 1
            .poscolini = columna
            .posfilafin = fila + 1
            .poscolfin = columna
        End With
        lstPos.Add(posCel_03)
        With oExcel.Range(oCells(posCel_03.posfilaini, posCel_03.poscolini), oCells(posCel_03.posfilaini, posCel_03.poscolini))
            .Value = "Tipo de cambio:"
        End With

        columna = 3

        Dim posCel_02 As New posicionCelda
        posCel_02.posfilaini = fila
        posCel_02.poscolini = columna

        bool_cabecera = True
        While cont_filas <= dsReporte.Tables(2).Rows.Count - 1
            While cont_columnas <= dsReporte.Tables(2).Columns.Count - 1
                If bool_cabecera Then
                    With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                        .Value = dsReporte.Tables(2).Columns(cont_columnas).ColumnName
                        .Font.Bold = True
                        .Interior.Color() = RGB(255, 255, 0)
                        .Font.Color = RGB(0, 0, 0)
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                    End With
                End If
                With oExcel.Range(oCells(fila + cont_filas + 1, columna + cont_columnas), oCells(fila + cont_filas + 1, columna + cont_columnas))
                    .Value = dsReporte.Tables(2).Rows(cont_filas).Item(cont_columnas)
                    .NumberFormat = "#,##0.000"
                End With
                cont_columnas += 1
            End While
            bool_cabecera = False
            If cont_filas < dsReporte.Tables(3).Rows.Count - 1 Then
                cont_columnas = 0
            End If
            cont_filas += 1
        End While

        fila = fila + (dsReporte.Tables(2).Rows.Count - 1) + 1 ' 13

        posCel_02.posfilafin = fila
        posCel_02.poscolfin = columna + (dsReporte.Tables(2).Columns.Count - 1)
        lstPos.Add(posCel_02)

        ' CUADRO 3: Proyeccion matricula
        fila += 2 ' 15
        columna = 2

        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Value = "MATRÍCULA"
            .Font.Bold = True
            .Font.Color = RGB(0, 0, 0)
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        fila += 1 ' 16

        Dim posCel_04 As New posicionCelda
        posCel_04.posfilaini = fila
        posCel_04.poscolini = columna

        bool_cabecera = True
        cont_columnas = 0
        cont_filas = 0
        While cont_filas <= dsReporte.Tables(3).Rows.Count - 1
            While cont_columnas <= dsReporte.Tables(3).Columns.Count - 1
                If bool_cabecera Then
                    With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                        .Value = dsReporte.Tables(3).Columns(cont_columnas).ColumnName
                        .Font.Bold = True
                        .Interior.Color() = RGB(255, 255, 0)
                        .Font.Color = RGB(0, 0, 0)
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                    End With
                End If
                With oExcel.Range(oCells(fila + cont_filas + 1, columna + cont_columnas), oCells(fila + cont_filas + 1, columna + cont_columnas))
                    .Value = dsReporte.Tables(3).Rows(cont_filas).Item(cont_columnas)
                End With
                cont_columnas += 1
            End While
            bool_cabecera = False
            If cont_filas < dsReporte.Tables(3).Rows.Count - 1 Then
                cont_columnas = 0
            End If
            cont_filas += 1
        End While

        fila = fila + (dsReporte.Tables(3).Rows.Count - 1) + 1 ' 30

        posCel_04.posfilafin = fila
        posCel_04.poscolfin = columna + (dsReporte.Tables(3).Columns.Count - 1)

        'Totales
        cont_columnas = 0
        cont_filas = 0
        fila += 1 ' 31
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Value = "Total"
            .Font.Bold = True
            .Interior.Color() = RGB(166, 166, 166)
        End With
        columna = 3
        str_Rango = ""
        While cont_columnas <= dsReporte.Tables(3).Columns.Count - 2
            str_Rango = DevLetraColumna(posCel_04.poscolini + 1 + cont_columnas) + (posCel_04.posfilaini + 1).ToString + ":" + _
                        DevLetraColumna(posCel_04.poscolini + 1 + cont_columnas) + (posCel_04.posfilafin).ToString
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Value = "=SUMA(" + str_Rango + ")"
                .Font.Bold = True
                .Interior.Color() = RGB(166, 166, 166)
            End With
            cont_columnas += 1
        End While

        posCel_04.posfilafin = fila
        lstPos.Add(posCel_04)

        fila += 1 ' 32

        ' CUADRO 4: Proyeccion matricula x pension
        fila = posCel_04.posfilaini ' 16
        columna = posCel_04.poscolfin + 3

        Dim posCel_05 As New posicionCelda
        posCel_05.posfilaini = fila
        posCel_05.poscolini = columna

        bool_cabecera = True
        cont_columnas = 0
        cont_filas = 0
        While cont_filas <= dsReporte.Tables(5).Rows.Count - 1
            While cont_columnas <= dsReporte.Tables(5).Columns.Count - 1
                If bool_cabecera Then
                    With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                        .Value = dsReporte.Tables(5).Columns(cont_columnas).ColumnName
                        .Font.Bold = True
                        .Interior.Color() = RGB(255, 255, 0)
                        .Font.Color = RGB(0, 0, 0)
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                    End With
                End If
                With oExcel.Range(oCells(fila + cont_filas + 1, columna + cont_columnas), oCells(fila + cont_filas + 1, columna + cont_columnas))
                    .Value = dsReporte.Tables(5).Rows(cont_filas).Item(cont_columnas)
                    .NumberFormat = "#,##0.00"
                    If cont_columnas > 0 Then
                        If dsReporte.Tables(5).Rows(cont_filas).Item(cont_columnas) < 0 Then : .Font.Color = RGB(255, 0, 0)
                        Else : .Font.Color = RGB(0, 0, 0) : End If
                    End If
                End With
                cont_columnas += 1
            End While
            bool_cabecera = False
            If cont_filas < dsReporte.Tables(5).Rows.Count - 1 Then
                cont_columnas = 0
            End If
            cont_filas += 1
        End While

        fila = fila + (dsReporte.Tables(5).Rows.Count - 1) + 1 ' 30

        posCel_05.posfilafin = fila
        posCel_05.poscolfin = columna + (dsReporte.Tables(5).Columns.Count - 1)

        'Totales
        cont_columnas = 0
        cont_filas = 0
        fila += 1
        With oExcel.Range(oCells(fila, posCel_05.poscolini), oCells(fila, posCel_05.poscolini))
            .Value = "Total"
            .Font.Bold = True
            .Interior.Color() = RGB(166, 166, 166)
        End With
        columna = posCel_05.poscolini + 1
        str_Rango = ""
        While cont_columnas <= dsReporte.Tables(5).Columns.Count - 2
            str_Rango = DevLetraColumna(posCel_05.poscolini + 1 + cont_columnas) + (posCel_05.posfilaini + 1).ToString + ":" + _
                        DevLetraColumna(posCel_05.poscolini + 1 + cont_columnas) + (posCel_05.posfilafin).ToString
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Value = "=SUMA(" + str_Rango + ")"
                .NumberFormat = "#,##0.00"
                .Font.Bold = True
                .Interior.Color() = RGB(166, 166, 166)
            End With
            cont_columnas += 1
        End While

        posCel_05.posfilafin = fila
        lstPos.Add(posCel_05)

        ' CUADRO 5: Proyeccion armadas
        fila += 2 ' 33
        columna = 2

        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Value = "ARMADAS"
            .Font.Bold = True
            .Font.Color = RGB(0, 0, 0)
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        fila += 1 ' 34

        Dim posCel_06 As New posicionCelda
        posCel_06.posfilaini = fila
        posCel_06.poscolini = columna

        bool_cabecera = True
        cont_columnas = 0
        cont_filas = 0
        While cont_filas <= dsReporte.Tables(4).Rows.Count - 1
            While cont_columnas <= dsReporte.Tables(4).Columns.Count - 1
                If bool_cabecera Then
                    With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                        .Value = dsReporte.Tables(4).Columns(cont_columnas).ColumnName
                        .Font.Bold = True
                        .Interior.Color() = RGB(255, 255, 0)
                        .Font.Color = RGB(0, 0, 0)
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                    End With
                End If
                With oExcel.Range(oCells(fila + cont_filas + 1, columna + cont_columnas), oCells(fila + cont_filas + 1, columna + cont_columnas))
                    .Value = dsReporte.Tables(4).Rows(cont_filas).Item(cont_columnas)
                End With
                cont_columnas += 1
            End While
            bool_cabecera = False
            If cont_filas < dsReporte.Tables(4).Rows.Count - 1 Then
                cont_columnas = 0
            End If
            cont_filas += 1
        End While

        fila = fila + (dsReporte.Tables(4).Rows.Count - 1) + 1 ' 48

        posCel_06.posfilafin = fila
        posCel_06.poscolfin = columna + (dsReporte.Tables(4).Columns.Count - 1)

        'Totales
        cont_columnas = 0
        cont_filas = 0
        fila += 1 ' 49
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Value = "Total"
            .Font.Bold = True
            .Interior.Color() = RGB(166, 166, 166)
        End With
        columna = 3
        str_Rango = ""
        While cont_columnas <= dsReporte.Tables(4).Columns.Count - 2
            str_Rango = DevLetraColumna(posCel_06.poscolini + 1 + cont_columnas) + (posCel_06.posfilaini + 1).ToString + ":" + _
                        DevLetraColumna(posCel_06.poscolini + 1 + cont_columnas) + (posCel_06.posfilafin).ToString
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Value = "=SUMA(" + str_Rango + ")"
                .Font.Bold = True
                .Interior.Color() = RGB(166, 166, 166)
            End With
            cont_columnas += 1
        End While

        posCel_06.posfilafin = fila
        lstPos.Add(posCel_06)

        fila += 1

        ' CUADRO 6: Proyeccion armadas x pension
        fila = posCel_06.posfilaini
        columna = posCel_06.poscolfin + 2

        Dim posCel_07 As New posicionCelda
        posCel_07.posfilaini = fila
        posCel_07.poscolini = columna

        bool_cabecera = True
        cont_columnas = 0
        cont_filas = 0
        While cont_filas <= dsReporte.Tables(6).Rows.Count - 1
            While cont_columnas <= dsReporte.Tables(6).Columns.Count - 1
                If bool_cabecera Then
                    With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                        .Value = dsReporte.Tables(6).Columns(cont_columnas).ColumnName
                        .Font.Bold = True
                        .Interior.Color() = RGB(255, 255, 0)
                        .Font.Color = RGB(0, 0, 0)
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                    End With
                End If
                With oExcel.Range(oCells(fila + cont_filas + 1, columna + cont_columnas), oCells(fila + cont_filas + 1, columna + cont_columnas))
                    .Value = dsReporte.Tables(6).Rows(cont_filas).Item(cont_columnas)
                    .NumberFormat = "#,##0.00"
                    If cont_columnas > 0 Then
                        If dsReporte.Tables(6).Rows(cont_filas).Item(cont_columnas) < 0 Then : .Font.Color = RGB(255, 0, 0)
                        Else : .Font.Color = RGB(0, 0, 0) : End If
                    End If
                End With
                cont_columnas += 1
            End While
            bool_cabecera = False
            If cont_filas < dsReporte.Tables(5).Rows.Count - 1 Then
                cont_columnas = 0
            End If
            cont_filas += 1
        End While

        fila = fila + (dsReporte.Tables(6).Rows.Count - 1) + 1 ' 47

        posCel_07.posfilafin = fila
        posCel_07.poscolfin = columna + (dsReporte.Tables(6).Columns.Count - 1)

        'Totales
        cont_columnas = 0
        cont_filas = 0
        fila += 1
        With oExcel.Range(oCells(fila, posCel_07.poscolini), oCells(fila, posCel_07.poscolini))
            .Value = "Total"
            .Font.Bold = True
            .Interior.Color() = RGB(166, 166, 166)
        End With
        columna = posCel_07.poscolini + 1
        str_Rango = ""
        While cont_columnas <= dsReporte.Tables(6).Columns.Count - 2
            str_Rango = DevLetraColumna(posCel_07.poscolini + 1 + cont_columnas) + (posCel_07.posfilaini + 1).ToString + ":" + _
                        DevLetraColumna(posCel_07.poscolini + 1 + cont_columnas) + (posCel_07.posfilafin).ToString
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Value = "=SUMA(" + str_Rango + ")"
                .NumberFormat = "#,##0.00"
                .ColumnWidth = 15
                .Font.Bold = True
                .Interior.Color() = RGB(166, 166, 166)
            End With
            cont_columnas += 1
        End While

        posCel_07.posfilafin = fila
        lstPos.Add(posCel_07)

        ' CUADRO 7: Ingresos en soles
        fila += 2 ' 51
        columna = 2


        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Value = "INGRESOS " & dsReporte.Tables(1).Rows(0).Item("anio") & " S/."
            .Font.Bold = True
            .Font.Color = RGB(0, 0, 0)
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        fila += 1 ' 52

        Dim posCel_08 As New posicionCelda
        posCel_08.posfilaini = fila
        posCel_08.poscolini = columna

        bool_cabecera = True
        cont_columnas = 0
        cont_filas = 0
        Dim bool_redline As Boolean = False
        While cont_filas <= dsReporte.Tables(7).Rows.Count - 1
            bool_redline = False
            While cont_columnas <= dsReporte.Tables(7).Columns.Count - 1
                If bool_cabecera Then
                    With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                        .Value = dsReporte.Tables(7).Columns(cont_columnas).ColumnName
                        .Font.Bold = True
                        .Interior.Color() = RGB(255, 255, 0)
                        .Font.Color = RGB(0, 0, 0)
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                    End With
                End If
                With oExcel.Range(oCells(fila + cont_filas + 1, columna + cont_columnas), oCells(fila + cont_filas + 1, columna + cont_columnas))
                    .Value = dsReporte.Tables(7).Rows(cont_filas).Item(cont_columnas)
                    .NumberFormat = "#,##0.00"

                    If cont_columnas = 0 Then
                        If dsReporte.Tables(7).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("*") >= 0 Or _
                            dsReporte.Tables(7).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("Beca") = 0 Or _
                            dsReporte.Tables(7).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("Descuento") = 0 Then
                            bool_redline = True
                        End If
                    End If

                    If bool_redline = True Then : .Font.Color = RGB(255, 0, 0) : Else : .Font.Color = RGB(0, 0, 0) : End If
                    'If cont_columnas > 0 Then
                    '    If dsReporte.Tables(7).Rows(cont_filas).Item(cont_columnas) < 0 Then : .Font.Color = RGB(255, 0, 0)
                    '    Else : .Font.Color = RGB(0, 0, 0) : End If
                    'End If
                End With
                cont_columnas += 1
            End While
            bool_cabecera = False
            If cont_filas < dsReporte.Tables(7).Rows.Count - 1 Then
                cont_columnas = 0
            End If
            cont_filas += 1
        End While

        fila = fila + (dsReporte.Tables(7).Rows.Count - 1) + 1 ' 48

        posCel_08.posfilafin = fila ' 93
        posCel_08.poscolfin = columna + (dsReporte.Tables(7).Columns.Count - 1)

        'Totales
        cont_columnas = 0
        cont_filas = 0
        fila += 1 ' 94
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Value = "Total"
            .Font.Bold = True
            .Interior.Color() = RGB(166, 166, 166)
        End With
        oExcel.Range(oCells(fila, columna), oCells(fila, columna)).Font.Bold = True
        columna = 3
        str_Rango = ""
        While cont_columnas <= dsReporte.Tables(7).Columns.Count - 2
            str_Rango = DevLetraColumna(posCel_08.poscolini + 1 + cont_columnas) + (posCel_08.posfilaini + 1).ToString + ":" + _
                        DevLetraColumna(posCel_08.poscolini + 1 + cont_columnas) + (posCel_08.posfilafin).ToString
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Value = "=SUMA(" + str_Rango + ")"
                .NumberFormat = "#,##0.00"
                .Font.Bold = True
                .Interior.Color() = RGB(166, 166, 166)
            End With
            cont_columnas += 1
        End While

        posCel_08.posfilafin = fila
        lstPos.Add(posCel_08)

        ' CUADRO 8: Ingresos en dolares
        fila += 2 ' 77
        columna = 2

        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Value = "INGRESOS " & dsReporte.Tables(1).Rows(0).Item("anio") & " US$"
            .Font.Bold = True
            .Font.Color = RGB(0, 0, 0)
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        fila += 1 ' 78

        Dim posCel_09 As New posicionCelda
        posCel_09.posfilaini = fila
        posCel_09.poscolini = columna

        bool_cabecera = True
        cont_columnas = 0
        cont_filas = 0
        While cont_filas <= dsReporte.Tables(8).Rows.Count - 1
            bool_redline = False
            While cont_columnas <= dsReporte.Tables(8).Columns.Count - 1
                If bool_cabecera Then
                    With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                        .Value = dsReporte.Tables(8).Columns(cont_columnas).ColumnName
                        .Font.Bold = True
                        .Interior.Color() = RGB(255, 255, 0)
                        .Font.Color = RGB(0, 0, 0)
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle
                    End With
                End If
                With oExcel.Range(oCells(fila + cont_filas + 1, columna + cont_columnas), oCells(fila + cont_filas + 1, columna + cont_columnas))
                    .Value = dsReporte.Tables(8).Rows(cont_filas).Item(cont_columnas)
                    .NumberFormat = "#,##0.00"

                    If cont_columnas = 0 Then
                        If dsReporte.Tables(8).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("*") >= 0 Or _
                            dsReporte.Tables(8).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("Beca") = 0 Or _
                            dsReporte.Tables(8).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("Descuento") = 0 Then
                            bool_redline = True
                        End If
                    End If

                    If bool_redline = True Then : .Font.Color = RGB(255, 0, 0) : Else : .Font.Color = RGB(0, 0, 0) : End If

                End With
                cont_columnas += 1
            End While
            bool_cabecera = False
            If cont_filas < dsReporte.Tables(8).Rows.Count - 1 Then
                cont_columnas = 0
            End If
            cont_filas += 1
        End While

        fila = fila + (dsReporte.Tables(8).Rows.Count - 1) + 1 ' 100

        posCel_09.posfilafin = fila ' 100
        posCel_09.poscolfin = columna + (dsReporte.Tables(8).Columns.Count - 1)

        'Totales
        cont_columnas = 0
        cont_filas = 0
        fila += 1 ' 101
        With oExcel.Range(oCells(fila, columna), oCells(fila, columna))
            .Value = "Total"
            .Font.Bold = True
            .Interior.Color() = RGB(166, 166, 166)
        End With
        oExcel.Range(oCells(fila, columna), oCells(fila, columna)).Font.Bold = True
        columna = 3
        str_Rango = ""
        While cont_columnas <= dsReporte.Tables(8).Columns.Count - 2
            str_Rango = DevLetraColumna(posCel_09.poscolini + 1 + cont_columnas) + (posCel_09.posfilaini + 1).ToString + ":" + _
                        DevLetraColumna(posCel_09.poscolini + 1 + cont_columnas) + (posCel_09.posfilafin).ToString
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Value = "=SUMA(" + str_Rango + ")"
                .NumberFormat = "#,##0.00"
                .ColumnWidth = 15
                .Font.Bold = True
                .Interior.Color() = RGB(166, 166, 166)
            End With
            cont_columnas += 1
        End While

        posCel_09.posfilafin = fila
        lstPos.Add(posCel_09)

        Dim fil_fin As Integer = posCel_09.posfilafin
        Dim col_fin As Integer = posCel_07.poscolfin

        oExcel.Range(oCells(1, 1), oCells(fil_fin, col_fin)).Font.Name = "Arial"

        ' pintado de bordes
        For Each p As posicionCelda In lstPos
            cuadradoCompleto(oExcel, oExcel.Range(oCells(p.posfilaini, p.poscolini), oCells(p.posfilafin, p.poscolfin)))
        Next

        'oExcel.Columns("A:A").Delete()
        'oExcel.Rows("1:4").Delete()

        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila

    End Function

    ' Reporte Codigo : 35 - 69 (Proyeccion : Cuotas de Ingreso)
    Public Shared Function ExportarReporteProyeccionesCuotasDeIngreso(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReportePensiones24_41").ToString() ' en blanco

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        LlenarPlantillaReporteProyeccionesCuotasDeIngreso(dsReporte, oCells, oExcel, str_NombreEntidadReporte)

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
    Private Shared Function LlenarPlantillaReporteProyeccionesCuotasDeIngreso( _
        ByVal dsReporte As System.Data.DataSet, _
        ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
        ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
        ByVal str_NombreEntidadReporte As String) As String

        Dim dtReporte As New System.Data.DataTable
        dtReporte = dsReporte.Tables(0).Copy

        ' Posiciones iniciales de pintado
        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        Dim lstPos As New List(Of posicionCelda)
        Dim str_Rango As String = ""

        oExcel.ActiveWindow.Zoom = 75
        oExcel.Range("A:A").ColumnWidth = 4

        Dim posCel_01 As New posicionCelda
        posCel_01.posfilaini = fila
        posCel_01.poscolini = columna

        Dim colorFondo As Integer = RGB(146, 205, 220)
        Dim colorLetra As Integer = RGB(0, 0, 0)

        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Value = "Reporte de Proyección de Cuotas de Ingreso"
            .Font.Size = 15
            .RowHeight = 30
        End With

        oExcel.Range(oCells(3, 2), oCells(3, 3)).RowHeight = 20
        oExcel.Range(oCells(3, 2), oCells(3, 2)).Value = "PERIODO: " & dsReporte.Tables(1).Rows(0).Item("Periodo")
        oExcel.Range(oCells(3, 3), oCells(3, 3)).Value = "FECHA INICIO: " & dsReporte.Tables(1).Rows(0).Item("FechaInicio") & " - " & _
                                                         "FECHA FIN: " & dsReporte.Tables(1).Rows(0).Item("FechaFin")

        With oExcel.Range(oCells(2, 2), oCells(3, 3))
            .Font.Bold = True
            .Font.Color = colorLetra
            .HorizontalAlignment = int_HA_Left
            .VerticalAlignment = int_VA_Middle
        End With

        Dim bool_cabecera As Boolean = True

        ' Listado
        While cont_filas <= dsReporte.Tables(0).Rows.Count - 1
            While cont_columnas <= dsReporte.Tables(0).Columns.Count - 1
                If bool_cabecera Then
                    With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                        .Value = dsReporte.Tables(0).Columns(cont_columnas).ColumnName
                        .Font.Bold = True
                        .Interior.Color() = colorFondo
                        .Font.Color = colorLetra
                        .HorizontalAlignment = int_HA_Center
                        .VerticalAlignment = int_VA_Middle

                        If cont_columnas = 0 Then : .ColumnWidth = 25
                        ElseIf cont_columnas = 1 Then : .ColumnWidth = 16
                        ElseIf cont_columnas = 2 Then : .ColumnWidth = 10
                        ElseIf cont_columnas = 3 Or cont_columnas = 4 Then : .ColumnWidth = 11
                        ElseIf cont_columnas = 5 Then : .ColumnWidth = 10
                        ElseIf cont_columnas = 6 Then : .ColumnWidth = 50
                        ElseIf cont_columnas = 7 Or cont_columnas = 8 Then : .ColumnWidth = 11
                        ElseIf cont_columnas = 9 Then : .ColumnWidth = 5
                        ElseIf cont_columnas = 10 Or cont_columnas = 13 Then : .ColumnWidth = 14
                        ElseIf cont_columnas = 11 Or cont_columnas = 12 Then : .ColumnWidth = 10
                        End If

                    End With
                End If
                With oExcel.Range(oCells(fila + cont_filas + 1, columna + cont_columnas), oCells(fila + cont_filas + 1, columna + cont_columnas))
                    .Value = dsReporte.Tables(0).Rows(cont_filas).Item(cont_columnas)

                    If cont_columnas = 0 Or cont_columnas = 3 Or cont_columnas = 4 Or cont_columnas = 6 Then : .HorizontalAlignment = int_HA_Left
                    ElseIf cont_columnas = 10 Or cont_columnas = 13 Or cont_columnas = 11 Or _
                            cont_columnas = 12 Then : .HorizontalAlignment = int_HA_Right
                    ElseIf cont_columnas = 1 Or cont_columnas = 5 Or cont_columnas = 2 Or _
                            cont_columnas = 7 Or cont_columnas = 8 Or cont_columnas = 9 Then : .HorizontalAlignment = int_HA_Center
                    End If

                End With
                cont_columnas += 1
            End While
            bool_cabecera = False
            cont_columnas = 0
            cont_filas += 1
        End While

        fila = fila + (dsReporte.Tables(0).Rows.Count - 1) + 1 ' ultima fila
        columna = columna + (dsReporte.Tables(0).Columns.Count - 1) ' ultima columna

        posCel_01.posfilafin = fila
        posCel_01.poscolfin = columna
        lstPos.Add(posCel_01)

        ' Formato
        str_Rango = DevLetraColumna(posCel_01.poscolfin - 3) + (posCel_01.posfilaini + 1).ToString + ":" + _
                    DevLetraColumna(posCel_01.poscolfin) + (posCel_01.posfilafin).ToString

        With oExcel.Range(oCells(posCel_01.posfilaini + 1, posCel_01.poscolfin - 3), _
                          oCells(posCel_01.posfilafin, posCel_01.poscolfin))
            .NumberFormat = "#,##0.00"
        End With

        ' Totales
        Dim posCel_02 As New posicionCelda
        posCel_02.posfilaini = fila + 2
        posCel_02.poscolini = columna - 3

        With oExcel.Range(oCells(posCel_02.posfilaini, posCel_02.poscolini - 3), oCells(posCel_02.posfilaini, posCel_02.poscolini - 3))
            .Value = "Debitos Sin Anular"
            .Font.Bold = True
            .Font.Color = colorLetra
        End With

        Dim str_condicion As String = "," & """>" & "0""" & ")"
        'Dim str_condicion As String = """>0"""
        str_Rango = DevLetraColumna(posCel_01.poscolfin - 1) + (posCel_01.posfilaini + 1).ToString + ":" + _
                    DevLetraColumna(posCel_01.poscolfin - 1) + (posCel_01.posfilafin).ToString
        With oExcel.Range(oCells(posCel_02.posfilaini, posCel_02.poscolini), oCells(posCel_02.posfilaini, posCel_02.poscolini))
            .Value = "=CONTAR.SI(" + str_Rango + str_condicion
            .Font.Bold = True
            .Font.Color = colorLetra
        End With

        posCel_02.posfilaini += 1
        With oExcel.Range(oCells(posCel_02.posfilaini, posCel_02.poscolini - 3), oCells(posCel_02.posfilaini, posCel_02.poscolini - 3))
            .Value = "Anulados"
            .Font.Bold = True
            .Font.Color = colorLetra
        End With
        str_condicion = "," & """=" & "ANULADO""" & ")" ' """=ANULADO"""
        str_Rango = DevLetraColumna(posCel_01.poscolfin - 5) + (posCel_01.posfilaini + 1).ToString + ":" + _
                    DevLetraColumna(posCel_01.poscolfin - 5) + (posCel_01.posfilafin).ToString
        With oExcel.Range(oCells(posCel_02.posfilaini, posCel_02.poscolini), oCells(posCel_02.posfilaini, posCel_02.poscolini))
            .Value = "=CONTAR.SI(" + str_Rango + str_condicion
            .Font.Bold = True
            .Font.Color = colorLetra
        End With

        posCel_02.posfilaini += 1
        With oExcel.Range(oCells(posCel_02.posfilaini, posCel_02.poscolini - 3), oCells(posCel_02.posfilaini, posCel_02.poscolini - 3))
            .Value = "Total"
            .Font.Bold = True
            .Font.Color = colorLetra
        End With

        For i As Integer = 0 To 3
            str_Rango = DevLetraColumna(posCel_02.poscolini + i) + (posCel_01.posfilaini + 1).ToString + ":" + _
                        DevLetraColumna(posCel_02.poscolini + i) + (posCel_01.posfilafin).ToString
            With oExcel.Range(oCells(posCel_02.posfilaini, posCel_02.poscolini + i), oCells(posCel_02.posfilaini, posCel_02.poscolini + i))
                .Value = "=SUMA(" + str_Rango + ")"
                .NumberFormat = "#,##0.00"
                .Font.Bold = True
                .Font.Color = colorLetra
            End With
        Next

        posCel_02.posfilafin = posCel_02.posfilaini
        posCel_02.poscolfin = columna

        Dim fil_fin As Integer = posCel_02.posfilafin
        Dim col_fin As Integer = posCel_02.poscolfin
        oExcel.Range(oCells(1, 1), oCells(fil_fin, col_fin)).Font.Name = "Arial"

        ' pintado de bordes
        For Each p As posicionCelda In lstPos
            cuadradoCompleto(oExcel, oExcel.Range(oCells(p.posfilaini, p.poscolini), oCells(p.posfilafin, p.poscolfin)))
        Next

        Dim objRangoAjustePanel As Microsoft.Office.Interop.Excel.Range = oExcel.Range(oCells(posCel_01.posfilaini + 1, posCel_01.poscolini), _
                                                                                       oCells(posCel_01.posfilaini + 1, posCel_01.poscolfin))
        objRangoAjustePanel.Select()
        oExcel.ActiveWindow.FreezePanes = True

        Dim objRangoSel As Microsoft.Office.Interop.Excel.Range = oExcel.Range(oCells(posCel_01.posfilaini, posCel_01.poscolini), _
                                                                               oCells(posCel_01.posfilaini, posCel_01.poscolfin))
        objRangoSel.Select()

        'oExcel.Columns("A:A").Delete()
        'oExcel.Rows("1:4").Delete()

        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila

    End Function


    Public Class posicionCelda
        Public posfilaini As Integer
        Public poscolini As Integer
        Public posfilafin As Integer
        Public poscolfin As Integer
    End Class
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

#Region "Reporte Morosidad"

    Public Shared Function ExportarReporteMorasidad(ByVal ds_Reporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String, ByVal str_PeriodoAcademico As String) As String
        Dim nombreRep As String
        nombreRep = GetNewName()
        Dim rutaTemporal As String = ""
        LlenarPlantillaReporteMorosidad(ds_Reporte, str_NombreEntidadReporte, rutaTemporal, str_PeriodoAcademico)
        Return rutaTemporal
    End Function

    Private Shared Function LlenarPlantillaReporteMorosidad(ByVal dsReporte As System.Data.DataSet, _
        ByVal str_NombreEntidadReporte As String, ByRef rutaTempDest As String, ByVal str_PeriodoAcademidoRep As String) As String

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
                .Value = "Reporte de Morosidad del Año " & str_PeriodoAcademidoRep
            End With

            ws.Row(3).Height = 20
            With ws.Range(ws.Cell(3, 3), ws.Cell(3, 3))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Style.Font.FontSize = 16
                .Value = "Armada (COLEGIO / BANCO)"
            End With

            With ws.Range(ws.Cell(4, 3), ws.Cell(4, 3))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
            End With

            fila = 6 : columna = 2 : cont_columnas = 0 : cont_filas = 0

            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila, columna))
                .Value = "QUINCENA"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Alignment.WrapText = True
                .Style.Font.Bold = True
                .Style.Font.FontSize = 10
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#bfbfbf")
            End With

            ws.Column(1).Width = 4
            ws.Column(columna).Width = 25 'quincena

            Dim str_mes As String = ""

            For i As Integer = 1 To 12
                ws.Column(columna + i).Width = 15
                With ws.Range(ws.Cell(fila, columna + i), ws.Cell(fila, columna + i))
                    Select Case i
                        Case 1 : str_mes = "ENERO"
                        Case 2 : str_mes = "FEBRERO"
                        Case 3 : str_mes = "MARZO"
                        Case 4 : str_mes = "ABRIL"
                        Case 5 : str_mes = "MAYO"
                        Case 6 : str_mes = "JUNIO"
                        Case 7 : str_mes = "JULIO"
                        Case 8 : str_mes = "AGOSTO"
                        Case 9 : str_mes = "SETIEMBRE"
                        Case 10 : str_mes = "OCTUBRE"
                        Case 11 : str_mes = "NOVIEMBRE"
                        Case 12 : str_mes = "DICIEMBRE"
                    End Select
                    .Value = str_mes
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#bfbfbf")
                End With
            Next

            Dim filAux As Integer = 0
            Dim colAux As Integer = 0
            Dim codMes As Integer = 0

            While cont_filas <= dsReporte.Tables(0).Rows.Count - 1
                filAux = dsReporte.Tables(0).Rows(cont_filas).Item("Fila")
                colAux = dsReporte.Tables(0).Rows(cont_filas).Item("Col")
                codMes = filAux

                With ws.Range(ws.Cell(fila + filAux, columna + colAux), ws.Cell(fila + filAux, columna + colAux))
                    .Value = dsReporte.Tables(0).Rows(cont_filas).Item("QUINCENA")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                Dim lst = _
                (From s In dsReporte.Tables(1).AsEnumerable _
                Select miFila = s.Field(Of Integer)("Fila"), _
                        miCol = s.Field(Of Integer)("Col"), _
                        miPorcentaje = s.Field(Of Decimal)("NoCanceladasPorc") _
                Where (miFila = codMes)).ToList

                For i As Integer = 0 To lst.Count - 1
                    filAux = lst(i).miFila
                    colAux = lst(i).miCol
                    With ws.Range(ws.Cell(fila + filAux, columna + colAux), ws.Cell(fila + filAux, columna + colAux))
                        If lst(i).miPorcentaje = 0 Then
                            .Value = ""
                        Else
                            .Value = CStr(Format(CDec(lst(i).miPorcentaje.ToString), "##,##0.0")) & " %"
                        End If
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.WrapText = True
                        .Style.Font.Bold = True
                        .Style.Font.FontSize = 10
                    End With
                Next
                cont_filas += 1
            End While

            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila + dsReporte.Tables(0).Rows.Count, columna + 12))
                With .Style.Border
                    .BottomBorder = XLBorderStyleValues.Thin
                    .TopBorder = XLBorderStyleValues.Thin
                    .LeftBorder = XLBorderStyleValues.Thin
                    .RightBorder = XLBorderStyleValues.Thin
                    .BottomBorderColor = XLColor.Black
                    .TopBorderColor = XLColor.Black
                    .LeftBorderColor = XLColor.Black
                    .RightBorderColor = XLColor.Black
                End With
            End With

            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales

        Catch ex As Exception
        End Try

    End Function


    Public Shared Function ExportarReporteMorasidadPorCorte(ByVal ds_Reporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String, _
                                                            ByVal str_PeriodoAcademico As String, _
                                                            ByVal dt_fecha1 As Date, _
                                                            ByVal dt_fecha2 As Date) As String
        Dim nombreRep As String
        nombreRep = GetNewName()
        Dim rutaTemporal As String = ""
        LlenarPlantillaReporteMorosidadPorCorte(ds_Reporte, str_NombreEntidadReporte, rutaTemporal, str_PeriodoAcademico, dt_fecha1, dt_fecha2)
        Return rutaTemporal
    End Function

    Private Shared Function LlenarPlantillaReporteMorosidadPorCorte(ByVal dsReporte As System.Data.DataSet, _
        ByVal str_NombreEntidadReporte As String, ByRef rutaTempDest As String, ByVal str_PeriodoAcademidoRep As String, _
        ByVal dt_fecha1 As Date,  ByVal dt_fecha2 As Date) As String

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
                .Value = "Reporte de Morosidad Por Corte" ' del Año " & str_PeriodoAcademidoRep
            End With

            With ws.Range(ws.Cell(3, 3), ws.Cell(3, 3))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
            End With

            fila = 6 : columna = 2 : cont_columnas = 0 : cont_filas = 0

            ws.Column(1).Width = 4
            ws.Column(columna).Width = 10 'anio
            ws.Column(columna + 1).Width = 20 'mes
            ws.Column(columna + 2).Width = 15 'total
            ws.Column(columna + 3).Width = 15 'total morosos
            ws.Column(columna + 4).Width = 17 'importe
            ws.Column(columna + 5).Width = 20 'porcentaje

            ws.Column(columna + 6).Width = 10 ' -----------
            ws.Column(columna + 7).Width = 10 'anio
            ws.Column(columna + 8).Width = 20 'mes
            ws.Column(columna + 9).Width = 15 'total
            ws.Column(columna + 10).Width = 15 'total morosos
            ws.Column(columna + 11).Width = 17 'importe
            ws.Column(columna + 12).Width = 20 'porcentaje

            ws.Row(fila).Height = 30

            Dim corteAux As Integer = 0

            For i = 1 To 2
                If i = 1 Then : corteAux = 0 : ElseIf i = 2 Then : corteAux = 7 : End If

                With ws.Range(ws.Cell(fila, columna + corteAux), ws.Cell(fila, columna + corteAux))
                    .Value = "AÑO"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#bfbfbf")
                End With

                With ws.Range(ws.Cell(fila, columna + 1 + corteAux), ws.Cell(fila, columna + 1 + corteAux))
                    .Value = "MES"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#bfbfbf")
                End With

                With ws.Range(ws.Cell(fila, columna + 2 + corteAux), ws.Cell(fila, columna + 2 + corteAux))
                    .Value = "TOTAL DE ALUMNOS"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#bfbfbf")
                End With

                With ws.Range(ws.Cell(fila, columna + 3 + corteAux), ws.Cell(fila, columna + 3 + corteAux))
                    .Value = "TOTAL DE MOROSOS"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#bfbfbf")
                End With

                With ws.Range(ws.Cell(fila, columna + 4 + corteAux), ws.Cell(fila, columna + 4 + corteAux))
                    .Value = "TOTAL DE IMPORTE"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#bfbfbf")
                End With

                With ws.Range(ws.Cell(fila, columna + 5 + corteAux), ws.Cell(fila, columna + 5 + corteAux))
                    .Value = "PORCENTAJE MOROSIDAD"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#bfbfbf")
                End With

            Next

            Dim filAux As Integer = 0
            Dim colAux As Integer = 0

            ' corte 1
            With ws.Range(ws.Cell(fila - 1, columna + 1), ws.Cell(fila - 1, columna + 1))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Value = "Corte al " & dt_fecha1.Date
            End With

            While cont_filas <= dsReporte.Tables(0).Rows.Count - 1
                filAux = dsReporte.Tables(0).Rows(cont_filas).Item("Fila")

                colAux = 0
                With ws.Range(ws.Cell(fila + filAux, columna + colAux), ws.Cell(fila + filAux, columna + colAux))
                    .Value = dsReporte.Tables(0).Rows(cont_filas).Item("Anio")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                colAux += 1
                With ws.Range(ws.Cell(fila + filAux, columna + colAux), ws.Cell(fila + filAux, columna + colAux))
                    .Value = dsReporte.Tables(0).Rows(cont_filas).Item("Mes")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                colAux += 1
                With ws.Range(ws.Cell(fila + filAux, columna + colAux), ws.Cell(fila + filAux, columna + colAux))
                    .Value = dsReporte.Tables(0).Rows(cont_filas).Item("total")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                colAux += 1
                With ws.Range(ws.Cell(fila + filAux, columna + colAux), ws.Cell(fila + filAux, columna + colAux))
                    .Value = dsReporte.Tables(0).Rows(cont_filas).Item("totnocan")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                colAux += 1
                With ws.Range(ws.Cell(fila + filAux, columna + colAux), ws.Cell(fila + filAux, columna + colAux))
                    .Value = dsReporte.Tables(0).Rows(cont_filas).Item("importe")
                    .Style.NumberFormat.Format = "#,##0.00"
                    '.Value = CStr(Format(CDec(dsReporte.Tables(0).Rows(cont_filas).Item("importe").ToString), "#,##0.00"))
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                colAux += 1
                With ws.Range(ws.Cell(fila + filAux, columna + colAux), ws.Cell(fila + filAux, columna + colAux))
                    .Value = CStr(Format(CDec(dsReporte.Tables(0).Rows(cont_filas).Item("nocanpor").ToString), "#,##0.0")) & " %"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                cont_filas += 1
            End While


            filAux = 0 : colAux = 0 : corteAux = 7 : cont_filas = 0

            ' corte 2
            With ws.Range(ws.Cell(fila - 1, columna + 1 + corteAux), ws.Cell(fila - 1, columna + 1 + corteAux))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Value = "Corte al " & dt_fecha2.Date
            End With
            While cont_filas <= dsReporte.Tables(1).Rows.Count - 1
                filAux = dsReporte.Tables(1).Rows(cont_filas).Item("Fila")

                colAux = 0
                With ws.Range(ws.Cell(fila + filAux, columna + colAux + corteAux), ws.Cell(fila + filAux, columna + colAux + corteAux))
                    .Value = dsReporte.Tables(1).Rows(cont_filas).Item("Anio")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                colAux += 1
                With ws.Range(ws.Cell(fila + filAux, columna + colAux + corteAux), ws.Cell(fila + filAux, columna + colAux + corteAux))
                    .Value = dsReporte.Tables(1).Rows(cont_filas).Item("Mes")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                colAux += 1
                With ws.Range(ws.Cell(fila + filAux, columna + colAux + corteAux), ws.Cell(fila + filAux, columna + colAux + corteAux))
                    .Value = dsReporte.Tables(1).Rows(cont_filas).Item("total")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                colAux += 1
                With ws.Range(ws.Cell(fila + filAux, columna + colAux + corteAux), ws.Cell(fila + filAux, columna + colAux + corteAux))
                    .Value = dsReporte.Tables(1).Rows(cont_filas).Item("totnocan")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                colAux += 1
                With ws.Range(ws.Cell(fila + filAux, columna + colAux + corteAux), ws.Cell(fila + filAux, columna + colAux + corteAux))
                    .Value = dsReporte.Tables(1).Rows(cont_filas).Item("importe")
                    .Style.NumberFormat.Format = "#,##0.00"
                    '.Value = CStr(Format(CDec(dsReporte.Tables(1).Rows(cont_filas).Item("importe").ToString), "#,##0.0"))
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                colAux += 1
                With ws.Range(ws.Cell(fila + filAux, columna + colAux + corteAux), ws.Cell(fila + filAux, columna + colAux + corteAux))
                    .Value = CStr(Format(CDec(dsReporte.Tables(1).Rows(cont_filas).Item("nocanpor").ToString), "##,##0.0")) & " %"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                    .Style.Font.Bold = True
                    .Style.Font.FontSize = 10
                End With

                cont_filas += 1
            End While

            With ws.Range(ws.Cell(fila, columna + corteAux), ws.Cell(fila + dsReporte.Tables(1).Rows.Count, columna + 5 + corteAux))
                With .Style.Border
                    .BottomBorder = XLBorderStyleValues.Thin
                    .TopBorder = XLBorderStyleValues.Thin
                    .LeftBorder = XLBorderStyleValues.Thin
                    .RightBorder = XLBorderStyleValues.Thin
                    .BottomBorderColor = XLColor.Black
                    .TopBorderColor = XLColor.Black
                    .LeftBorderColor = XLColor.Black
                    .RightBorderColor = XLColor.Black
                End With
            End With


            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila + dsReporte.Tables(0).Rows.Count, columna + 5))
                With .Style.Border
                    .BottomBorder = XLBorderStyleValues.Thin
                    .TopBorder = XLBorderStyleValues.Thin
                    .LeftBorder = XLBorderStyleValues.Thin
                    .RightBorder = XLBorderStyleValues.Thin
                    .BottomBorderColor = XLColor.Black
                    .TopBorderColor = XLColor.Black
                    .LeftBorderColor = XLColor.Black
                    .RightBorderColor = XLColor.Black
                End With
            End With

            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales

        Catch ex As Exception
        End Try

    End Function


    Public Shared Function ExportarReporteMorasidadHistoricoPorCorte(ByVal ds_Reporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String, _
                                                            ByVal str_PeriodoAcademico As String, _
                                                            ByVal dt_fecha As Date) As String
        Dim nombreRep As String
        nombreRep = GetNewName()
        Dim rutaTemporal As String = ""
        LlenarPlantillaReporteMorosidadHistoricoPorCorte(ds_Reporte, str_NombreEntidadReporte, rutaTemporal, str_PeriodoAcademico, dt_fecha)
        Return rutaTemporal
    End Function

    Private Shared Function LlenarPlantillaReporteMorosidadHistoricoPorCorte(ByVal dsReporte As System.Data.DataSet, _
        ByVal str_NombreEntidadReporte As String, ByRef rutaTempDest As String, ByVal str_PeriodoAcademidoRep As String, _
        ByVal dt_fecha As Date) As String

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
                .Value = "Reporte de Alumnos Deudores - " & str_PeriodoAcademidoRep
            End With

            With ws.Range(ws.Cell(3, 3), ws.Cell(3, 3))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Value = "Solo deudas en Soles hasta el " & dt_fecha.Date
            End With

            With ws.Range(ws.Cell(4, 3), ws.Cell(4, 3))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
            End With

            fila = 6 : columna = 2 : cont_columnas = 0 : cont_filas = 0

            ws.Column(1).Width = 3
            ws.Column(columna).Width = 45 'apellidos y nombre
            ws.Column(columna + 1).Width = 14 'concepto
            ws.Column(columna + 2).Width = 10 'ene
            ws.Column(columna + 3).Width = 10 'feb
            ws.Column(columna + 4).Width = 10 'mar
            ws.Column(columna + 5).Width = 10 'abr
            ws.Column(columna + 6).Width = 10 'may
            ws.Column(columna + 7).Width = 10 'jun
            ws.Column(columna + 8).Width = 10 'jul
            ws.Column(columna + 9).Width = 10 'ago
            ws.Column(columna + 10).Width = 10 'set
            ws.Column(columna + 11).Width = 10 'oct
            ws.Column(columna + 12).Width = 10 'nov
            ws.Column(columna + 13).Width = 10 'dic
            ws.Column(columna + 14).Width = 15 'total

            Dim str_Cabecera As String = ""

            For i = 0 To 14
                Select Case i
                    Case 0 : str_Cabecera = "Apellidos y Nombres"
                    Case 1 : str_Cabecera = "Concepto"
                    Case 2 : str_Cabecera = "ENE"
                    Case 3 : str_Cabecera = "FEB"
                    Case 4 : str_Cabecera = "MAR"
                    Case 5 : str_Cabecera = "ABR"
                    Case 6 : str_Cabecera = "MAY"
                    Case 7 : str_Cabecera = "JUN"
                    Case 8 : str_Cabecera = "JUL"
                    Case 9 : str_Cabecera = "AGO"
                    Case 10 : str_Cabecera = "SET"
                    Case 11 : str_Cabecera = "OCT"
                    Case 12 : str_Cabecera = "NOV"
                    Case 13 : str_Cabecera = "DIC"
                    Case 14 : str_Cabecera = "TOTAL"
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


            Dim dt_aulas As DataTable = dsReporte.Tables(0)
            Dim dt_alumnos As DataTable = dsReporte.Tables(1)
            Dim dt_deudas As DataTable = dsReporte.Tables(2)
            Dim dt_total As DataTable = dsReporte.Tables(3)

            Dim lstAulas As IEnumerable(Of cla_aula)
            Dim lstReport As IEnumerable(Of cla_report)

            lstReport = _
            From sql4 In dt_total.AsEnumerable() _
            Select New cla_report With { _
                .aula = sql4.Field(Of Integer)("aula"), _
                .codigoalumno = sql4.Field(Of String)("alumno"), _
                .mes = sql4.Field(Of Integer)("mes"), _
                .monto = sql4.Field(Of Decimal)("monto") _
            }

            lstAulas = _
            From sql1 In dt_aulas.AsEnumerable() _
            Select New cla_aula With { _
                   .orden = sql1.Field(Of Integer)("fila"), _
                   .codigogrado = sql1.Field(Of Integer)("grado"), _
                   .codigoaula = sql1.Field(Of Integer)("aula"), _
                   .descripcion = sql1.Field(Of String)("gradoaula"), _
                   .lstalumno = (From sql2 In dt_alumnos.AsEnumerable() _
                                  Where sql2.Field(Of Integer)("aula") = sql1.Field(Of Integer)("aula") _
                                  Select New cla_alumno With { _
                                        .codigoaula = sql2.Field(Of Integer)("aula"), _
                                        .codigoalumno = sql2.Field(Of String)("alumno"), _
                                        .nombre = sql2.Field(Of String)("nombrealumno"), _
                                        .lstdeudas = (From sql3 In dt_deudas.AsEnumerable() _
                                                      Where sql3.Field(Of String)("alumno") = sql2.Field(Of String)("alumno") _
                                                      Select New cla_deuda With { _
                                                            .codigoalumno = sql3.Field(Of String)("alumno"), _
                                                            .concepto = sql3.Field(Of String)("concepto"), _
                                                            .mes = sql3.Field(Of Integer)("mes"), _
                                                            .monto = sql3.Field(Of Decimal)("monto") _
                                                        }) _
                                  }) _
                    }

            Dim filAux As Integer = 0
            Dim colAux As Integer = 0
            fila = 7 : columna = 2 : cont_columnas = 0 : cont_filas = 0

            Dim iteaula As Integer = 0
            Dim itealu As Integer = 0
            Dim itedeu As Integer = 0
            Dim codaula As Integer = 0

            filAux = fila

            For Each aula In lstAulas
                iteaula += 1
                codaula = aula.codigoaula

                With ws.Range(ws.Cell(fila + iteaula, columna), _
                              ws.Cell(fila + iteaula, columna))

                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.Bold = True
                    .Value = aula.descripcion
                    .Style.Font.FontSize = 9

                    For Each alumno In aula.lstalumno
                        itealu += 1
                        With ws.Range(ws.Cell(fila + iteaula + itealu, columna), _
                                      ws.Cell(fila + iteaula + itealu, columna))

                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.Indent = 3
                            .Value = alumno.nombre
                            .Style.Font.FontSize = 9

                            For i As Integer = 1 To 12
                                With ws.Range(ws.Cell(fila + iteaula + itealu, columna + 1 + i), _
                                              ws.Cell(fila + iteaula + itealu, columna + 1 + i))
                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    .Value = "0.00"
                                    .Style.NumberFormat.Format = "#,##0.00"
                                    .Style.Font.FontSize = 9
                                End With
                            Next

                            For Each deuda In alumno.lstdeudas
                                With ws.Range(ws.Cell(fila + iteaula + itealu, columna + 1), _
                                              ws.Cell(fila + iteaula + itealu, columna + 1))
                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    .Value = deuda.concepto
                                End With
                                With ws.Range(ws.Cell(fila + iteaula + itealu, columna + 1 + deuda.mes), _
                                              ws.Cell(fila + iteaula + itealu, columna + 1 + deuda.mes))
                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    .Value = deuda.monto
                                    .Style.NumberFormat.Format = "#,##0.00"
                                    .Style.Font.FontSize = 9
                                End With
                            Next

                            Dim mifun As Func(Of cla_deuda, Decimal)
                            mifun = Function(a) a.monto

                            With ws.Range(ws.Cell(fila + iteaula + itealu, columna + 1 + 12 + 1), _
                                          ws.Cell(fila + iteaula + itealu, columna + 1 + 12 + 1))
                                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Value = alumno.lstdeudas.Sum(mifun)
                                .Style.NumberFormat.Format = "#,##0.00"
                                .Style.Font.FontSize = 9
                                .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
                            End With

                        End With
                    Next

                    iteaula = iteaula + itealu + 1

                    Dim mifun2 As Func(Of cla_report, Decimal)
                    mifun2 = Function(a) a.monto


                    Dim tot = From t In dt_total.AsEnumerable() _
                              Select codigoaula = t.Field(Of Integer)("aula"), _
                                     codigoalumno = t.Field(Of String)("alumno") _
                                     Distinct

                    With ws.Range(ws.Cell(fila + iteaula, columna), _
                                  ws.Cell(fila + iteaula, columna))
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = "Total Alumnos: " & CStr(tot.Where(Function(b) b.codigoaula = codaula).Count)
                        .Style.Font.FontSize = 9
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
                    End With

                    With ws.Range(ws.Cell(fila + iteaula, columna + 1), _
                                  ws.Cell(fila + iteaula, columna + 1))
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = "Total Por Nivel:"
                        .Style.Font.FontSize = 9
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
                    End With

                    For i As Integer = 1 To 12
                        With ws.Range(ws.Cell(fila + iteaula, columna + 1 + i), _
                                      ws.Cell(fila + iteaula, columna + 1 + i))
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Value = lstReport.Where(Function(a) a.aula = codaula And a.mes = i).Sum(mifun2)
                            .Style.NumberFormat.Format = "#,##0.00"
                            .Style.Font.FontSize = 9
                            .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
                        End With
                    Next

                    With ws.Range(ws.Cell(fila + iteaula, columna + 1 + 13), _
                                  ws.Cell(fila + iteaula, columna + 1 + 13))
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = lstReport.Where(Function(a) a.aula = codaula).Sum(mifun2)
                        .Style.NumberFormat.Format = "#,##0.00"
                        .Style.Font.FontSize = 9
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
                    End With

                    iteaula += 1
                    itealu = 0

                End With
            Next

            Dim totg = From t In dt_total.AsEnumerable() _
                            Select codigoalumno = t.Field(Of String)("alumno") _
                            Distinct

            With ws.Range(ws.Cell(fila + iteaula + 1, columna), _
                          ws.Cell(fila + iteaula + 1, columna))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Value = "Total Alumnos: " & CStr(totg.Count)
                .Style.Font.FontSize = 9
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
            End With

            With ws.Range(ws.Cell(fila + iteaula + 1, columna + 1), _
                          ws.Cell(fila + iteaula + 1, columna + 1))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Value = "Total General:"
                .Style.Font.FontSize = 9
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
            End With

            Dim mifun3 As Func(Of cla_report, Decimal)
            mifun3 = Function(a) a.monto

            For i As Integer = 1 To 12
                With ws.Range(ws.Cell(fila + iteaula + 1, columna + 1 + i), _
                              ws.Cell(fila + iteaula + 1, columna + 1 + i))
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Value = lstReport.Where(Function(a) a.mes = i).Sum(mifun3)
                    .Style.NumberFormat.Format = "#,##0.00"
                    .Style.Font.FontSize = 9
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
                End With
            Next

            With ws.Range(ws.Cell(fila + iteaula + 1, columna + 1 + 13), _
                          ws.Cell(fila + iteaula + 1, columna + 1 + 13))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Value = lstReport.Sum(mifun3)
                .Style.NumberFormat.Format = "#,##0.00"
                .Style.Font.FontSize = 9
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
            End With

            ws.SheetView.Freeze(6, 0)

            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales

        Catch ex As Exception
        End Try

    End Function



    Public Shared Function ExportarReporteDeudasPorNivel(ByVal ds_Reporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String, _
                                                            ByVal str_PeriodoAcademico As String, _
                                                            ByVal dt_fecha As Date) As String
        Dim nombreRep As String
        nombreRep = GetNewName()
        Dim rutaTemporal As String = ""
        LlenarPlantillaReporteDeudasPorNivel(ds_Reporte, str_NombreEntidadReporte, rutaTemporal, str_PeriodoAcademico, dt_fecha)
        Return rutaTemporal
    End Function

    Private Shared Function LlenarPlantillaReporteDeudasPorNivel(ByVal dsReporte As System.Data.DataSet, _
        ByVal str_NombreEntidadReporte As String, ByRef rutaTempDest As String, ByVal str_PeriodoAcademidoRep As String, _
        ByVal dt_fecha As Date) As String

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
                .Value = "Reporte de Alumnos Deudores por Servicio de Enseñanza " & str_PeriodoAcademidoRep
            End With

            With ws.Range(ws.Cell(3, 3), ws.Cell(3, 3))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Value = "" '"Solo deudas por conceptos académicos en Soles hasta el " & dt_fecha.Date
            End With

            With ws.Range(ws.Cell(4, 3), ws.Cell(4, 3))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
            End With

            fila = 6 : columna = 2 : cont_columnas = 0 : cont_filas = 0

            ws.Column(1).Width = 3
            ws.Column(columna).Width = 45 'apellidos y nombre
            ws.Column(columna + 1).Width = 30 'concepto
            ws.Column(columna + 2).Width = 10 'ene
            ws.Column(columna + 3).Width = 10 'feb
            ws.Column(columna + 4).Width = 10 'mar
            ws.Column(columna + 5).Width = 10 'abr
            ws.Column(columna + 6).Width = 10 'may
            ws.Column(columna + 7).Width = 10 'jun
            ws.Column(columna + 8).Width = 10 'jul
            ws.Column(columna + 9).Width = 10 'ago
            ws.Column(columna + 10).Width = 10 'set
            ws.Column(columna + 11).Width = 10 'oct
            ws.Column(columna + 12).Width = 10 'nov
            ws.Column(columna + 13).Width = 10 'dic
            ws.Column(columna + 14).Width = 15 'total

            Dim str_Cabecera As String = ""

            For i = 0 To 14
                Select Case i
                    Case 0 : str_Cabecera = "Apellidos y Nombres"
                    Case 1 : str_Cabecera = "Concepto"
                    Case 2 : str_Cabecera = "ENE"
                    Case 3 : str_Cabecera = "FEB"
                    Case 4 : str_Cabecera = "MAR"
                    Case 5 : str_Cabecera = "ABR"
                    Case 6 : str_Cabecera = "MAY"
                    Case 7 : str_Cabecera = "JUN"
                    Case 8 : str_Cabecera = "JUL"
                    Case 9 : str_Cabecera = "AGO"
                    Case 10 : str_Cabecera = "SET"
                    Case 11 : str_Cabecera = "OCT"
                    Case 12 : str_Cabecera = "NOV"
                    Case 13 : str_Cabecera = "DIC"
                    Case 14 : str_Cabecera = "TOTAL"
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

            Dim dt_aulas As DataTable = dsReporte.Tables(0)
            Dim dt_alumnos As DataTable = dsReporte.Tables(1)
            Dim dt_conceptos As DataTable = dsReporte.Tables(2)
            Dim dt_deudas As DataTable = dsReporte.Tables(3)
            Dim dt_total As DataTable = dsReporte.Tables(4)

            Dim lstAulas As IEnumerable(Of cla_aula2)
            Dim lstReport As IEnumerable(Of cla_report)

            lstReport = _
            From sql4 In dt_total.AsEnumerable() _
            Select New cla_report With { _
                .aula = sql4.Field(Of Integer)("aula"), _
                .codigoalumno = sql4.Field(Of String)("alumno"), _
                .mes = sql4.Field(Of Integer)("mes"), _
                .monto = sql4.Field(Of Decimal)("monto") _
            }

            lstAulas = _
            From sql1 In dt_aulas.AsEnumerable() _
            Select New cla_aula2 With { _
                   .orden = sql1.Field(Of Integer)("fila"), _
                   .codigogrado = sql1.Field(Of Integer)("grado"), _
                   .codigoaula = sql1.Field(Of Integer)("aula"), _
                   .descripcion = sql1.Field(Of String)("gradoaula"), _
                   .lstalumno = (From sql2 In dt_alumnos.AsEnumerable() _
                                  Where sql2.Field(Of Integer)("aula") = sql1.Field(Of Integer)("aula") _
                                  Select New cla_alumno2 With { _
                                        .codigoaula = sql2.Field(Of Integer)("aula"), _
                                        .codigoalumno = sql2.Field(Of String)("alumno"), _
                                        .nombre = sql2.Field(Of String)("nombrealumno"), _
                                        .conceptos = sql2.Field(Of Integer)("conceptos"), _
                                        .lstconceptos = (From sql3 In dt_conceptos.AsEnumerable() _
                                                         Where sql3.Field(Of String)("alumno") = sql2.Field(Of String)("alumno") _
                                                         Select New cla_concepto With { _
                                                            .codigoconcepto = sql3.Field(Of Integer)("codconcepto"), _
                                                            .descripcion = sql3.Field(Of String)("concepto"), _
                                                            .lstdeudas = (From sql4 In dt_deudas.AsEnumerable() _
                                                                           Where sql4.Field(Of String)("alumno") = sql3.Field(Of String)("alumno") _
                                                                           And sql4.Field(Of Integer)("codconcepto") = sql3.Field(Of Integer)("codconcepto") _
                                                                           Select New cla_deuda2 With { _
                                                                           .codigoalumno = sql4.Field(Of String)("alumno"), _
                                                                           .codigoconcepto = sql4.Field(Of Integer)("codconcepto"), _
                                                                           .mes = sql4.Field(Of Integer)("mes"), _
                                                                           .monto = sql4.Field(Of Decimal)("monto") _
                                                                        }) _
                                                         }) _
                                  }) _
                    }

            Dim filAux As Integer = 0
            Dim colAux As Integer = 0
            fila = 7 : columna = 2 : cont_columnas = 0 : cont_filas = 0

            Dim iteaula As Integer = 0
            Dim itealu As Integer = 0
            Dim itecon As Integer = 0
            Dim itedeu As Integer = 0
            Dim iteaux As Integer = 0
            Dim codaula As Integer = 0

            filAux = fila

            For Each aula In lstAulas
                fila += 1
                codaula = aula.codigoaula
                With ws.Range(ws.Cell(fila + iteaula, columna), _
                              ws.Cell(fila + iteaula, columna))
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.Bold = True
                    .Value = aula.descripcion
                    .Style.Font.FontSize = 9

                    For Each alumno In aula.lstalumno
                        fila += 1
                        With ws.Range(ws.Cell(fila, columna), _
                                      ws.Cell(fila, columna))
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Style.Alignment.Indent = 3
                            .Value = alumno.nombre
                            .Style.Font.FontSize = 9

                            itecon = 0
                            iteaux = 0
                            For Each concepto In alumno.lstconceptos
                                iteaux += 1
                                With ws.Range(ws.Cell(fila, columna + 1), _
                                              ws.Cell(fila, columna + 1))
                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    .Value = concepto.descripcion
                                End With
                                For i As Integer = 1 To 12
                                    With ws.Range(ws.Cell(fila, columna + 1 + i), _
                                                  ws.Cell(fila, columna + 1 + i))
                                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                        .Value = "0.00"
                                        .Style.NumberFormat.Format = "#,##0.00"
                                        .Style.Font.FontSize = 9
                                    End With
                                Next
                                'total por alumno y concepto 
                                Dim mifun As Func(Of cla_deuda2, Decimal)
                                mifun = Function(a) a.monto
                                With ws.Range(ws.Cell(fila, columna + 1 + 12 + 1), _
                                              ws.Cell(fila, columna + 1 + 12 + 1))
                                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    .Value = concepto.lstdeudas.Sum(mifun)
                                    .Style.NumberFormat.Format = "#,##0.00"
                                    .Style.Font.FontSize = 9
                                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
                                End With
                                For Each deuda In concepto.lstdeudas
                                    With ws.Range(ws.Cell(fila, columna + 1 + deuda.mes), _
                                                  ws.Cell(fila, columna + 1 + deuda.mes))
                                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                        .Value = deuda.monto
                                        .Style.NumberFormat.Format = "#,##0.00"
                                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00")
                                        .Style.Font.FontSize = 9
                                    End With
                                Next
                                If alumno.conceptos > 1 And iteaux < alumno.conceptos Then
                                    fila += 1
                                End If
                            Next
                        End With
                    Next

                    fila += 1
                    ' total de alumnos por aula
                    Dim mifun2 As Func(Of cla_report, Decimal)
                    mifun2 = Function(a) a.monto
                    Dim tot = From t In dt_total.AsEnumerable() _
                              Select codigoaula = t.Field(Of Integer)("aula"), _
                                     codigoalumno = t.Field(Of String)("alumno") _
                                     Distinct
                    With ws.Range(ws.Cell(fila, columna), _
                                  ws.Cell(fila, columna))
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = "Total Alumnos: " & CStr(tot.Where(Function(b) b.codigoaula = codaula).Count)
                        .Style.Font.FontSize = 9
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
                    End With
                    With ws.Range(ws.Cell(fila, columna + 1), _
                                  ws.Cell(fila, columna + 1))
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = "Total Por Nivel:"
                        .Style.Font.FontSize = 9
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
                    End With
                    For i As Integer = 1 To 12
                        With ws.Range(ws.Cell(fila, columna + 1 + i), _
                                      ws.Cell(fila, columna + 1 + i))
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Value = lstReport.Where(Function(a) a.aula = codaula And a.mes = i).Sum(mifun2)
                            .Style.NumberFormat.Format = "#,##0.00"
                            .Style.Font.FontSize = 9
                            .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
                        End With
                    Next
                    With ws.Range(ws.Cell(fila, columna + 1 + 13), _
                                  ws.Cell(fila, columna + 1 + 13))
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = lstReport.Where(Function(a) a.aula = codaula).Sum(mifun2)
                        .Style.NumberFormat.Format = "#,##0.00"
                        .Style.Font.FontSize = 9
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#d9d9d9")
                    End With
                    fila += 1
                End With
            Next

            Dim totg = From t In dt_total.AsEnumerable() _
                       Select codigoalumno = t.Field(Of String)("alumno") _
                       Distinct
            With ws.Range(ws.Cell(fila + 1, columna), _
                          ws.Cell(fila + 1, columna))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Value = "Total Alumnos: " & CStr(totg.Count)
                .Style.Font.FontSize = 9
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
            End With
            With ws.Range(ws.Cell(fila + 1, columna + 1), _
                          ws.Cell(fila + 1, columna + 1))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Value = "Total General:"
                .Style.Font.FontSize = 9
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
            End With
            Dim mifun3 As Func(Of cla_report, Decimal)
            mifun3 = Function(a) a.monto

            For i As Integer = 1 To 12
                With ws.Range(ws.Cell(fila + 1, columna + 1 + i), _
                              ws.Cell(fila + 1, columna + 1 + i))
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Value = lstReport.Where(Function(a) a.mes = i).Sum(mifun3)
                    .Style.NumberFormat.Format = "#,##0.00"
                    .Style.Font.FontSize = 9
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
                End With
            Next
            With ws.Range(ws.Cell(fila + 1, columna + 1 + 13), _
                          ws.Cell(fila + 1, columna + 1 + 13))
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Value = lstReport.Sum(mifun3)
                .Style.NumberFormat.Format = "#,##0.00"
                .Style.Font.FontSize = 9
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050")
            End With

            ws.SheetView.Freeze(6, 0)

            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales

        Catch ex As Exception
        End Try

    End Function


#Region "Clases auxiliares"

    Public Class cla_aula
        Public orden As Integer
        Public codigogrado As Integer
        Public codigoaula As Integer
        Public descripcion As String
        Public lstalumno As IEnumerable(Of cla_alumno)
    End Class
    Public Class cla_alumno
        Public codigoaula As Integer
        Public codigoalumno As String
        Public nombre As String
        Public lstdeudas As IEnumerable(Of cla_deuda)
    End Class
    Public Class cla_deuda
        Public codigoalumno As String
        Public concepto As String
        Public mes As Integer
        Public monto As Decimal
    End Class
    Public Class cla_report
        Public aula As Integer
        Public codigoalumno As String
        Public mes As Integer
        Public monto As Decimal
    End Class


    Public Class cla_aula2
        Public orden As Integer
        Public codigogrado As Integer
        Public codigoaula As Integer
        Public descripcion As String
        Public lstalumno As IEnumerable(Of cla_alumno2)
    End Class
    Public Class cla_alumno2
        Public codigoaula As Integer
        Public codigoalumno As String
        Public nombre As String
        Public conceptos As Integer
        Public lstconceptos As IEnumerable(Of cla_concepto)
    End Class
    Public Class cla_concepto
        Public codigoconcepto As Integer
        Public descripcion As String
        Public lstdeudas As IEnumerable(Of cla_deuda2)
    End Class
    Public Class cla_deuda2
        Public codigoalumno As String
        Public codigoconcepto As Integer
        Public mes As Integer
        Public monto As Decimal
    End Class

#End Region

#End Region

#Region "reportes proyecciones"

    Public Shared Function ExportarReporteProyeccionesIngresos2(ByVal ds_Reporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String, _
                                                                ByVal str_PeriodoAcademico As String, ByVal str_fecha1 As String, ByVal str_fecha2 As String) As String
        Dim nombreRep As String
        nombreRep = GetNewName()
        Dim rutaTemporal As String = ""
        LlenarPlantillaReporteProyeccionesIngresos2(ds_Reporte, str_NombreEntidadReporte, str_PeriodoAcademico, str_fecha1, str_fecha2, rutaTemporal)
        Return rutaTemporal
    End Function

    Private Shared Function LlenarPlantillaReporteProyeccionesIngresos2( _
        ByVal dsReporte As System.Data.DataSet, _
        ByVal str_NombreEntidadReporte As String, ByVal str_PeriodoAcademidoRep As String, ByVal str_fecha1 As String, ByVal str_fecha2 As String, ByRef rutaTempDest As String) As String

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

            Dim lstPos As New List(Of posicionCelda)
            Dim str_Rango As String = ""

            ' TITULO
            ws.Row(2).Height = 30
            With ws.Range(ws.Cell(2, 2), ws.Cell(2, 2))
                .Value = "INGRESOS COLEGIO " & dsReporte.Tables(1).Rows(0).Item("anio")
                .Style.Font.FontSize = 24
                .Style.Font.Bold = True
                .Style.Font.FontColor = XLColor.FromHtml("#000000")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
            End With

            ' CUADRO 1: Costo de Pensiones
            ws.Column(2).Width = 45
            With ws.Range(ws.Cell(4, 2), ws.Cell(4, 2))
                .Value = "SUPUESTOS"
                .Style.Font.Bold = True
                .Style.Font.FontColor = XLColor.FromHtml("#000000")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
            End With

            fila = 5
            columna = 2
            cont_columnas = 0
            cont_filas = 0

            Dim posCel_01 As New posicionCelda
            posCel_01.posfilaini = fila
            posCel_01.poscolini = columna

            Dim bool_cabecera As Boolean = True
            While cont_filas <= dsReporte.Tables(0).Rows.Count - 1
                While cont_columnas <= dsReporte.Tables(0).Columns.Count - 1
                    If bool_cabecera Then
                        With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                            .Value = dsReporte.Tables(0).Columns(cont_columnas).ColumnName
                            .Style.Font.Bold = True
                            .Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00")
                            .Style.Font.FontColor = XLColor.FromHtml("#000000")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        End With
                    End If
                    With ws.Range(ws.Cell(fila + cont_filas + 1, columna + cont_columnas), ws.Cell(fila + cont_filas + 1, columna + cont_columnas))
                        .Value = dsReporte.Tables(0).Rows(cont_filas).Item(cont_columnas)
                        .Style.NumberFormat.Format = "#,##0.00"
                    End With
                    cont_columnas += 1
                End While
                bool_cabecera = False
                If cont_filas < dsReporte.Tables(3).Rows.Count - 1 Then
                    cont_columnas = 0
                End If
                cont_filas += 1
            End While

            fila = fila + (dsReporte.Tables(0).Rows.Count - 1) + 1 ' fila: 9

            posCel_01.posfilafin = fila
            posCel_01.poscolfin = columna + (dsReporte.Tables(0).Columns.Count - 1)
            lstPos.Add(posCel_01)

            fila += 2 ' fila: 11
            With ws.Range(ws.Cell(fila, 2), ws.Cell(fila, 2))
                .Value = "PROYECCIÓN ALUMNADO " & dsReporte.Tables(1).Rows(0).Item("anio")
                .Style.Font.Bold = True
                .Style.Font.FontColor = XLColor.FromHtml("#000000")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
            End With

            ' CUADRO 2: Tipo de Cambio
            fila += 1 ' fila: 12
            columna = 2
            cont_columnas = 0
            cont_filas = 0

            Dim posCel_03 As New posicionCelda
            With posCel_03
                .posfilaini = fila + 1
                .poscolini = columna
                .posfilafin = fila + 1
                .poscolfin = columna
            End With
            lstPos.Add(posCel_03)
            With ws.Range(ws.Cell(posCel_03.posfilaini, posCel_03.poscolini), ws.Cell(posCel_03.posfilaini, posCel_03.poscolini))
                .Value = "Tipo de cambio:"
            End With

            columna = 3

            Dim posCel_02 As New posicionCelda
            posCel_02.posfilaini = fila
            posCel_02.poscolini = columna

            bool_cabecera = True
            While cont_filas <= dsReporte.Tables(2).Rows.Count - 1
                While cont_columnas <= dsReporte.Tables(2).Columns.Count - 1
                    If bool_cabecera Then
                        With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                            .Value = dsReporte.Tables(2).Columns(cont_columnas).ColumnName
                            .Style.Font.Bold = True
                            .Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00")
                            .Style.Font.FontColor = XLColor.FromHtml("#000000")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        End With
                    End If
                    With ws.Range(ws.Cell(fila + cont_filas + 1, columna + cont_columnas), ws.Cell(fila + cont_filas + 1, columna + cont_columnas))
                        .Value = dsReporte.Tables(2).Rows(cont_filas).Item(cont_columnas)
                        .Style.NumberFormat.Format = "#,##0.00"
                    End With
                    cont_columnas += 1
                End While
                bool_cabecera = False
                If cont_filas < dsReporte.Tables(3).Rows.Count - 1 Then
                    cont_columnas = 0
                End If
                cont_filas += 1
            End While

            fila = fila + (dsReporte.Tables(2).Rows.Count - 1) + 1 ' 13

            posCel_02.posfilafin = fila
            posCel_02.poscolfin = columna + (dsReporte.Tables(2).Columns.Count - 1)
            lstPos.Add(posCel_02)

            ' CUADRO 3: Proyeccion matricula
            fila += 2 ' 15
            columna = 2

            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila, columna))
                .Value = "MATRÍCULA"
                .Style.Font.Bold = True
                .Style.Font.FontColor = XLColor.FromHtml("#000000")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
            End With

            fila += 1 ' 16

            Dim posCel_04 As New posicionCelda
            posCel_04.posfilaini = fila
            posCel_04.poscolini = columna

            bool_cabecera = True
            cont_columnas = 0
            cont_filas = 0
            While cont_filas <= dsReporte.Tables(3).Rows.Count - 1
                While cont_columnas <= dsReporte.Tables(3).Columns.Count - 1
                    If bool_cabecera Then
                        With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                            .Value = dsReporte.Tables(3).Columns(cont_columnas).ColumnName
                            .Style.Font.Bold = True
                            .Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00")
                            .Style.Font.FontColor = XLColor.FromHtml("#000000")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        End With
                    End If
                    With ws.Range(ws.Cell(fila + cont_filas + 1, columna + cont_columnas), ws.Cell(fila + cont_filas + 1, columna + cont_columnas))
                        .Value = dsReporte.Tables(3).Rows(cont_filas).Item(cont_columnas)
                    End With
                    cont_columnas += 1
                End While
                bool_cabecera = False
                If cont_filas < dsReporte.Tables(3).Rows.Count - 1 Then
                    cont_columnas = 0
                End If
                cont_filas += 1
            End While

            fila = fila + (dsReporte.Tables(3).Rows.Count - 1) + 1 ' 30

            posCel_04.posfilafin = fila
            posCel_04.poscolfin = columna + (dsReporte.Tables(3).Columns.Count - 1)

            'Totales
            cont_columnas = 0
            cont_filas = 0
            fila += 1 ' 31
            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila, columna))
                .Value = "Total"
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
            End With
            columna = 3
            str_Rango = ""
            While cont_columnas <= dsReporte.Tables(3).Columns.Count - 2
                str_Rango = DevLetraColumna(posCel_04.poscolini + 1 + cont_columnas) + (posCel_04.posfilaini + 1).ToString + ":" + _
                            DevLetraColumna(posCel_04.poscolini + 1 + cont_columnas) + (posCel_04.posfilafin).ToString
                With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                    .FormulaA1 = "=SUM(" + str_Rango + ")"
                    .Style.Font.Bold = True
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
                End With
                cont_columnas += 1
            End While

            posCel_04.posfilafin = fila
            lstPos.Add(posCel_04)

            fila += 1 ' 32

            ' CUADRO 4: Proyeccion matricula x pension
            fila = posCel_04.posfilaini ' 16
            columna = posCel_04.poscolfin + 3

            Dim posCel_05 As New posicionCelda
            posCel_05.posfilaini = fila
            posCel_05.poscolini = columna

            bool_cabecera = True
            cont_columnas = 0
            cont_filas = 0
            While cont_filas <= dsReporte.Tables(5).Rows.Count - 1
                While cont_columnas <= dsReporte.Tables(5).Columns.Count - 1
                    If bool_cabecera Then
                        With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                            .Value = dsReporte.Tables(5).Columns(cont_columnas).ColumnName
                            .Style.Font.Bold = True
                            .Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00")
                            .Style.Font.FontColor = XLColor.FromHtml("#000000")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        End With
                    End If
                    With ws.Range(ws.Cell(fila + cont_filas + 1, columna + cont_columnas), ws.Cell(fila + cont_filas + 1, columna + cont_columnas))
                        .Value = dsReporte.Tables(5).Rows(cont_filas).Item(cont_columnas)
                        .Style.NumberFormat.Format = "#,##0.00"
                        If cont_columnas > 0 Then
                            If dsReporte.Tables(5).Rows(cont_filas).Item(cont_columnas) < 0 Then
                                .Style.Font.FontColor = XLColor.FromHtml("#ff0000")
                            Else
                                .Style.Font.FontColor = XLColor.FromHtml("#000000")
                            End If
                        End If
                    End With
                    cont_columnas += 1
                End While
                bool_cabecera = False
                If cont_filas < dsReporte.Tables(5).Rows.Count - 1 Then
                    cont_columnas = 0
                End If
                cont_filas += 1
            End While

            fila = fila + (dsReporte.Tables(5).Rows.Count - 1) + 1 ' 30

            posCel_05.posfilafin = fila
            posCel_05.poscolfin = columna + (dsReporte.Tables(5).Columns.Count - 1)

            'Totales
            cont_columnas = 0
            cont_filas = 0
            fila += 1
            With ws.Range(ws.Cell(fila, posCel_05.poscolini), ws.Cell(fila, posCel_05.poscolini))
                .Value = "Total"
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
            End With
            columna = posCel_05.poscolini + 1
            str_Rango = ""
            While cont_columnas <= dsReporte.Tables(5).Columns.Count - 2
                str_Rango = DevLetraColumna(posCel_05.poscolini + 1 + cont_columnas) + (posCel_05.posfilaini + 1).ToString + ":" + _
                            DevLetraColumna(posCel_05.poscolini + 1 + cont_columnas) + (posCel_05.posfilafin).ToString
                With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                    .FormulaA1 = "=SUM(" + str_Rango + ")"
                    .Style.NumberFormat.Format = "#,##0.00"
                    .Style.Font.Bold = True
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
                End With
                cont_columnas += 1
            End While

            posCel_05.posfilafin = fila
            lstPos.Add(posCel_05)

            ' CUADRO 5: Proyeccion armadas
            fila += 2 ' 33
            columna = 2

            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila, columna))
                .Value = "ARMADAS"
                .Style.Font.Bold = True
                .Style.Font.FontColor = XLColor.FromHtml("#000000")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
            End With

            fila += 1 ' 34

            Dim posCel_06 As New posicionCelda
            posCel_06.posfilaini = fila
            posCel_06.poscolini = columna

            bool_cabecera = True
            cont_columnas = 0
            cont_filas = 0
            While cont_filas <= dsReporte.Tables(4).Rows.Count - 1
                While cont_columnas <= dsReporte.Tables(4).Columns.Count - 1
                    If bool_cabecera Then
                        With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                            .Value = dsReporte.Tables(4).Columns(cont_columnas).ColumnName
                            .Style.Font.Bold = True
                            .Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00")
                            .Style.Font.FontColor = XLColor.FromHtml("#000000")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        End With
                    End If
                    With ws.Range(ws.Cell(fila + cont_filas + 1, columna + cont_columnas), ws.Cell(fila + cont_filas + 1, columna + cont_columnas))
                        .Value = dsReporte.Tables(4).Rows(cont_filas).Item(cont_columnas)
                    End With
                    cont_columnas += 1
                End While
                bool_cabecera = False
                If cont_filas < dsReporte.Tables(4).Rows.Count - 1 Then
                    cont_columnas = 0
                End If
                cont_filas += 1
            End While

            fila = fila + (dsReporte.Tables(4).Rows.Count - 1) + 1 ' 48

            posCel_06.posfilafin = fila
            posCel_06.poscolfin = columna + (dsReporte.Tables(4).Columns.Count - 1)

            'Totales
            cont_columnas = 0
            cont_filas = 0
            fila += 1 ' 49
            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila, columna))
                .Value = "Total"
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
            End With
            columna = 3
            str_Rango = ""
            While cont_columnas <= dsReporte.Tables(4).Columns.Count - 2
                str_Rango = DevLetraColumna(posCel_06.poscolini + 1 + cont_columnas) + (posCel_06.posfilaini + 1).ToString + ":" + _
                            DevLetraColumna(posCel_06.poscolini + 1 + cont_columnas) + (posCel_06.posfilafin).ToString
                With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                    .FormulaA1 = "=SUM(" + str_Rango + ")"
                    .Style.Font.Bold = True
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
                End With
                cont_columnas += 1
            End While

            posCel_06.posfilafin = fila
            lstPos.Add(posCel_06)

            fila += 1

            ' CUADRO 6: Proyeccion armadas x pension
            fila = posCel_06.posfilaini
            columna = posCel_06.poscolfin + 2

            Dim posCel_07 As New posicionCelda
            posCel_07.posfilaini = fila
            posCel_07.poscolini = columna

            bool_cabecera = True
            cont_columnas = 0
            cont_filas = 0
            While cont_filas <= dsReporte.Tables(6).Rows.Count - 1
                While cont_columnas <= dsReporte.Tables(6).Columns.Count - 1
                    If bool_cabecera Then
                        With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                            .Value = dsReporte.Tables(6).Columns(cont_columnas).ColumnName
                            .Style.Font.Bold = True
                            .Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00")
                            .Style.Font.FontColor = XLColor.FromHtml("#000000")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        End With
                    End If
                    With ws.Range(ws.Cell(fila + cont_filas + 1, columna + cont_columnas), ws.Cell(fila + cont_filas + 1, columna + cont_columnas))
                        .Value = dsReporte.Tables(6).Rows(cont_filas).Item(cont_columnas)
                        .Style.NumberFormat.Format = "#,##0.00"
                        If cont_columnas > 0 Then
                            If dsReporte.Tables(6).Rows(cont_filas).Item(cont_columnas) < 0 Then
                                .Style.Font.FontColor = XLColor.FromHtml("#ff0000")
                            Else
                                .Style.Font.FontColor = XLColor.FromHtml("#000000")
                            End If
                        End If
                    End With
                    cont_columnas += 1
                End While
                bool_cabecera = False
                If cont_filas < dsReporte.Tables(5).Rows.Count - 1 Then
                    cont_columnas = 0
                End If
                cont_filas += 1
            End While

            fila = fila + (dsReporte.Tables(6).Rows.Count - 1) + 1 ' 47

            posCel_07.posfilafin = fila
            posCel_07.poscolfin = columna + (dsReporte.Tables(6).Columns.Count - 1)

            'Totales
            cont_columnas = 0
            cont_filas = 0
            fila += 1
            With ws.Range(ws.Cell(fila, posCel_07.poscolini), ws.Cell(fila, posCel_07.poscolini))
                .Value = "Total"
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
            End With
            columna = posCel_07.poscolini + 1
            str_Rango = ""
            While cont_columnas <= dsReporte.Tables(6).Columns.Count - 2
                str_Rango = DevLetraColumna(posCel_07.poscolini + 1 + cont_columnas) + (posCel_07.posfilaini + 1).ToString + ":" + _
                            DevLetraColumna(posCel_07.poscolini + 1 + cont_columnas) + (posCel_07.posfilafin).ToString

                ws.Column(columna + cont_columnas).Width = 15
                With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                    .FormulaA1 = "=SUM(" + str_Rango + ")"
                    .Style.NumberFormat.Format = "#,##0.00"
                    .Style.Font.Bold = True
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
                End With
                cont_columnas += 1
            End While

            posCel_07.posfilafin = fila
            lstPos.Add(posCel_07)

            ' CUADRO 7: Ingresos en soles
            fila += 2 ' 51
            columna = 2


            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila, columna))
                .Value = "INGRESOS " & dsReporte.Tables(1).Rows(0).Item("anio") & " S/."
                .Style.Font.Bold = True
                .Style.Font.FontColor = XLColor.FromHtml("#000000")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
            End With

            fila += 1 ' 52

            Dim posCel_08 As New posicionCelda
            posCel_08.posfilaini = fila
            posCel_08.poscolini = columna

            bool_cabecera = True
            cont_columnas = 0
            cont_filas = 0
            Dim bool_redline As Boolean = False
            While cont_filas <= dsReporte.Tables(7).Rows.Count - 1
                bool_redline = False
                While cont_columnas <= dsReporte.Tables(7).Columns.Count - 1
                    If bool_cabecera Then
                        With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                            .Value = dsReporte.Tables(7).Columns(cont_columnas).ColumnName
                            .Style.Font.Bold = True
                            .Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00")
                            .Style.Font.FontColor = XLColor.FromHtml("#000000")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        End With
                    End If
                    With ws.Range(ws.Cell(fila + cont_filas + 1, columna + cont_columnas), ws.Cell(fila + cont_filas + 1, columna + cont_columnas))
                        .Value = dsReporte.Tables(7).Rows(cont_filas).Item(cont_columnas)
                        .Style.NumberFormat.Format = "#,##0.00"

                        If cont_columnas = 0 Then
                            If dsReporte.Tables(7).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("*") >= 0 Or _
                                dsReporte.Tables(7).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("Beca") = 0 Or _
                                dsReporte.Tables(7).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("Descuento") = 0 Then
                                bool_redline = True
                            End If
                        End If

                        If bool_redline = True Then
                            .Style.Font.FontColor = XLColor.FromHtml("#ff0000")
                        Else
                            .Style.Font.FontColor = XLColor.FromHtml("#000000")
                        End If
                        'If cont_columnas > 0 Then
                        '    If dsReporte.Tables(7).Rows(cont_filas).Item(cont_columnas) < 0 Then : .Font.Color = RGB(255, 0, 0)
                        '    Else : .Font.Color = RGB(0, 0, 0) : End If
                        'End If
                    End With
                    cont_columnas += 1
                End While
                bool_cabecera = False
                If cont_filas < dsReporte.Tables(7).Rows.Count - 1 Then
                    cont_columnas = 0
                End If
                cont_filas += 1
            End While

            fila = fila + (dsReporte.Tables(7).Rows.Count - 1) + 1 ' 48

            posCel_08.posfilafin = fila ' 93
            posCel_08.poscolfin = columna + (dsReporte.Tables(7).Columns.Count - 1)

            'Totales
            cont_columnas = 0
            cont_filas = 0
            fila += 1 ' 94
            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila, columna))
                .Value = "Total"
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
            End With

            columna = 3
            str_Rango = ""
            While cont_columnas <= dsReporte.Tables(7).Columns.Count - 2
                str_Rango = DevLetraColumna(posCel_08.poscolini + 1 + cont_columnas) + (posCel_08.posfilaini + 1).ToString + ":" + _
                            DevLetraColumna(posCel_08.poscolini + 1 + cont_columnas) + (posCel_08.posfilafin).ToString
                With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                    .FormulaA1 = "=SUM(" + str_Rango + ")"
                    .Style.NumberFormat.Format = "#,##0.00"
                    .Style.Font.Bold = True
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
                End With
                cont_columnas += 1
            End While

            posCel_08.posfilafin = fila
            lstPos.Add(posCel_08)

            ' CUADRO 8: Ingresos en dolares
            fila += 2 ' 77
            columna = 2

            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila, columna))
                .Value = "INGRESOS " & dsReporte.Tables(1).Rows(0).Item("anio") & " US$"
                .Style.Font.Bold = True
                .Style.Font.FontColor = XLColor.FromHtml("#000000")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
            End With

            fila += 1 ' 78

            Dim posCel_09 As New posicionCelda
            posCel_09.posfilaini = fila
            posCel_09.poscolini = columna

            bool_cabecera = True
            cont_columnas = 0
            cont_filas = 0
            While cont_filas <= dsReporte.Tables(8).Rows.Count - 1
                bool_redline = False
                While cont_columnas <= dsReporte.Tables(8).Columns.Count - 1
                    If bool_cabecera Then
                        With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                            .Value = dsReporte.Tables(8).Columns(cont_columnas).ColumnName
                            .Style.Font.Bold = True
                            .Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00")
                            .Style.Font.FontColor = XLColor.FromHtml("#000000")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        End With
                    End If
                    With ws.Range(ws.Cell(fila + cont_filas + 1, columna + cont_columnas), ws.Cell(fila + cont_filas + 1, columna + cont_columnas))
                        .Value = dsReporte.Tables(8).Rows(cont_filas).Item(cont_columnas)
                        .Style.NumberFormat.Format = "#,##0.00"

                        If cont_columnas = 0 Then
                            If dsReporte.Tables(8).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("*") >= 0 Or _
                                dsReporte.Tables(8).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("Beca") = 0 Or _
                                dsReporte.Tables(8).Rows(cont_filas).Item(cont_columnas).ToString.IndexOf("Descuento") = 0 Then
                                bool_redline = True
                            End If
                        End If

                        If bool_redline = True Then
                            .Style.Font.FontColor = XLColor.FromHtml("#ff0000")
                        Else
                            .Style.Font.FontColor = XLColor.FromHtml("#000000")
                        End If

                    End With
                    cont_columnas += 1
                End While
                bool_cabecera = False
                If cont_filas < dsReporte.Tables(8).Rows.Count - 1 Then
                    cont_columnas = 0
                End If
                cont_filas += 1
            End While

            fila = fila + (dsReporte.Tables(8).Rows.Count - 1) + 1 ' 100

            posCel_09.posfilafin = fila ' 100
            posCel_09.poscolfin = columna + (dsReporte.Tables(8).Columns.Count - 1)

            'Totales
            cont_columnas = 0
            cont_filas = 0
            fila += 1 ' 101
            With ws.Range(ws.Cell(fila, columna), ws.Cell(fila, columna))
                .Value = "Total"
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
            End With

            columna = 3
            str_Rango = ""
            While cont_columnas <= dsReporte.Tables(8).Columns.Count - 2
                str_Rango = DevLetraColumna(posCel_09.poscolini + 1 + cont_columnas) + (posCel_09.posfilaini + 1).ToString + ":" + _
                            DevLetraColumna(posCel_09.poscolini + 1 + cont_columnas) + (posCel_09.posfilafin).ToString

                ws.Column(columna + cont_columnas).Width = 15
                With ws.Range(ws.Cell(fila, columna + cont_columnas), ws.Cell(fila, columna + cont_columnas))
                    .FormulaA1 = "=SUM(" + str_Rango + ")"
                    .Style.NumberFormat.Format = "#,##0.00"
                    .Style.Font.Bold = True
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#a6a6a6")
                End With
                cont_columnas += 1
            End While

            posCel_09.posfilafin = fila
            lstPos.Add(posCel_09)

            Dim fil_fin As Integer = posCel_09.posfilafin
            Dim col_fin As Integer = posCel_07.poscolfin

            ws.Range(ws.Cell(1, 1), ws.Cell(fil_fin, col_fin)).Style.Font.FontName = "Arial"

            ' pintado de bordes
            'For Each p As posicionCelda In lstPos
            '    cuadradoCompleto(oExcel, ws.Range(ws.Cell(p.posfilaini, p.poscolini), ws.Cell(p.posfilafin, p.poscolfin)))
            'Next

            workbook.CalculateMode = XLCalculateMode.Auto


            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales

        Catch ex As Exception
        End Try

    End Function

#End Region


End Class
