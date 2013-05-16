Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloCursos
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Módulo de Registro de las Atenciones en Enfermeria
''' </summary>
''' <remarks>
''' Código del Modulo:    1
''' Código de la Opción:  2
''' </remarks>

Partial Class Modulo_Enfermeria_FichaAtencion
    Inherits System.Web.UI.Page
    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1
#Region "Eventos Busqueda Ficha"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            TimeSelector1.Attributes.Add("onKeyDown", "doCheck();")  'Solo números
            TimeSelector2.Attributes.Add("onKeyDown", "doCheck();")  'Solo números

            tbSintomas.Attributes.Add("onkeypress", " ValidarLength(this, 800);")
            tbSintomas.Attributes.Add("onkeyup", " ValidarLength(this, 800);")
            tbObservaciones.Attributes.Add("onkeypress", " ValidarLength(this, 800);")
            tbObservaciones.Attributes.Add("onkeyup", " ValidarLength(this, 800);")

            FilteredTextBoxExtender2.ValidChars = FilteredTextBoxExtender3.ValidChars & vbCrLf
            FilteredTextBoxExtender3.ValidChars = FilteredTextBoxExtender3.ValidChars & vbCrLf

            Me.Master.MostrarTitulo("Ficha de Atención")

            If Not Page.IsPostBack Then
                ListarTipoAtencion()
                SetearAccionesAcceso()
                ViewState("SortExpression") = "NombreCompleto"
                ViewState("Direccion") = "ASC"
                ViewState("FichaTemporal") = False

                'Busqueda Ficha
                cargarCombos()
                tbBuscarFechaAtencionInicial.Text = Today.ToShortDateString
                tbBuscarFechaAtencionFinal.Text = Today.ToShortDateString

                'Detalle Ficha
                btnFichaCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                listarFichas()

                'If Not Session("PersonaPopup") Is Nothing Then ' F5
                'End If
                rbTipoProcAtencion_SelectedIndexChanged()
            Else
                If Not Session("PersonaPopup") Is Nothing AndAlso Page.Session("ResetearPadre") = True Then

                    Dim objMaestroPersona As be_MaestroPersonas = Session("PersonaPopup")

                    If Session("FichaAtencionTipoBusqueda") = "paciente" Then ' Busqueda : Paciente

                        If objMaestroPersona.CodigoTipoPersona = 1 Then
                            rbTipoProcAtencion_SelectedIndexChanged()
                            cargarComboTaller(objMaestroPersona.CodigoAlumno)
                            cargarComboCurso(objMaestroPersona.CodigoGrado)
                        End If

                        LimpiarCamposFicha()

                        hidenCodigoPersona.Value = objMaestroPersona.CodigoPersona
                        hidenCodigoTipoPaciente.Value = objMaestroPersona.CodigoTipoPersona


                        cargarInformacionAtencionesAlumno(objMaestroPersona.CodigoPersona)



                        If hidenCodigoTipoPaciente.Value = 1 Then
                            spanNSnGA.Visible = True
                            lbNSnGS.Text = objMaestroPersona.NSnGS
                            EstadoCamposAlumno(True)

                        Else
                            spanNSnGA.Visible = False
                            lbNSnGS.Text = ""
                            EstadoCamposAlumno(False)
                        End If

                        lbNombrePaciente.Text = objMaestroPersona.NombreCompleto
                        lbEdadPaciente.Text = IIf(objMaestroPersona.Edad <= 0, "", "( Edad :&nbsp;" & objMaestroPersona.Edad & "&nbsp;años )")
                        lbTipoPaciente.Text = objMaestroPersona.DescTipoPersona

                        If hidenCodigoTipoPaciente.Value = 2 Then
                            imgFotoPaciente.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Admin").ToString() & objMaestroPersona.RutaFoto
                        ElseIf hidenCodigoTipoPaciente.Value = 1 Then
                            'rbTipoProcAtencion_SelectedIndexChanged()
                            'cargarComboTaller(objMaestroPersona.CodigoAlumno)
                            'cargarComboCurso(objMaestroPersona.CodigoGrado)
                            imgFotoPaciente.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & objMaestroPersona.RutaFoto
                        Else
                            imgFotoPaciente.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web").ToString() & objMaestroPersona.RutaFoto
                        End If

                        ActivarCampos()

                        'Valores por defecto
                        'Datos de inicio
                        ddlSede.SelectedValue = 1
                        cargarComboMedicamentoKardex()

                        tbFechaAtencion.Text = Today.ToShortDateString
                        TimeSelector1.Date = Now.ToShortTimeString

                        'Datos finales
                        TimeSelector2.Date = TimeSelector1.Date.AddMinutes(5) 'Now.ToShortTimeString

                    ElseIf Session("FichaAtencionTipobusqueda") = "envia" Then
                        hidenCodigoPersonalEnvia.Value = objMaestroPersona.CodigoTrabajador
                        hidenCodigoPersonaEnvia.Value = objMaestroPersona.CodigoPersona
                        hidenCodigoTipoPersonaEnvia.Value = objMaestroPersona.CodigoTipoPersona
                        tbResponsable.Text = objMaestroPersona.NombreCompleto
                    ElseIf Session("FichaAtencionTipobusqueda") = "recoje" Then
                        hidenCodigoPersonaRecoge.Value = objMaestroPersona.CodigoPersona
                        hidenCodigoTipoPersonaRecoge.Value = objMaestroPersona.CodigoTipoPersona
                        tbAcompanante.Text = objMaestroPersona.NombreCompleto
                        lblTipoAcompanante.Text = objMaestroPersona.DescTipoPersona
                    End If

                    Page.Session("ResetearPadre") = False
                End If
            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#Region "cargar cantidad de atencion del alumno "
    Private Sub cargarInformacionAtencionesAlumno(ByVal codPersona As Integer)
        Try
            Dim fechaInicial As String = ""
            Dim fechaFin As String = ""


            fechaInicial = ObtenerPrimerDiaSemana(Date.Now)
            fechaFin = DateTime.Now.Day.ToString().PadLeft(2, "0") & "/" & Date.Now.Month.ToString().PadLeft(2, "0") & "/" & Date.Now.Year

            Dim dst As DataSet
            Dim dc As New Dictionary(Of String, Object)


            dc.Add("fechaInicio", fechaInicial)
            dc.Add("fechaFin ", fechaFin)
            dc.Add("codPersona", codPersona)



            Dim nParam As String = "USP_lisAtencionesAlumno"
            dst = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam)
            Dim dtAtenciones As New DataTable
            dtAtenciones = dst.Tables(0)
            'cantidad
            If dtAtenciones.Rows.Count > 0 Then
                lblCantidadAtencionAlumno.Text = dtAtenciones.Rows(0)("cantidad") & " (Desde:" & fechaInicial & " hasta:" & fechaFin & ")"
            Else
                lblCantidadAtencionAlumno.Text = "0" & " (Desde:" & fechaInicial & " hasta:" & fechaFin & ")"
            End If



        Catch ex As Exception

        End Try
    End Sub



#End Region
#Region " funciones para calcular fechas"
    Public Shared Function ObtenerPrimerDiaSemana(ByVal diaSemana As DateTime) As String

        Dim primerDiaSemana As DateTime = diaSemana.Date
        While primerDiaSemana.DayOfWeek <> DayOfWeek.Monday
            primerDiaSemana = primerDiaSemana.AddDays(-1)
        End While

        Return primerDiaSemana.Day.ToString().PadLeft(2, "0") & "/" & primerDiaSemana.Month.ToString().PadLeft(2, "0") & "/" & primerDiaSemana.Year

    End Function

#End Region

#Region "cargar tipo atencion"
    Private Sub ListarTipoAtencion()
        Try
            Dim dtTipoAtencion As New DataTable
            Dim dst As DataSet
            Dim dc As New Dictionary(Of String, Object)
            Dim nParam As String = "USP_lisTipoAtencion"
            dst = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam)
            dtTipoAtencion = dst.Tables(0)
            cmbTipoAtencion.DataSource = dtTipoAtencion
            cmbTipoAtencion.DataTextField = "TA_Descripcion"
            cmbTipoAtencion.DataValueField = "TA_CodigoTipoAtenciones"
            cmbTipoAtencion.DataBind()
            cmbTipoAtencion.SelectedValue = 1

        Catch ex As Exception

        End Try
    End Sub
#End Region
    Protected Sub btnBuscar_Click()
        Try
            Dim usp_mensaje As String = ""
            If validarBusquedaFicha(usp_mensaje) Then
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

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        LimpiarCamposFicha()
        VerRegistro("Inserción", 1)
        DesactivarCampos()
    End Sub

    Protected Sub ddlBuscarTipoPaciente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            tipoBusqueda()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarSubNivel)
            limpiarCombos(ddlBuscarGrado)
            limpiarCombos(ddlBuscarAula)
            cargarComboAlumnoSubNivel()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarSubNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarGrado)
            limpiarCombos(ddlBuscarAula)
            cargarComboAlumnoGrado()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarAula)
            cargarComboAlumnoAulas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarFamiliarNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarFamiliarSubNivel)
            limpiarCombos(ddlBuscarFamiliarGrado)
            limpiarCombos(ddlBuscarFamiliarAula)
            cargarComboFamiliarAlumnoSubNivel()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarFamiliarSubNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarFamiliarGrado)
            limpiarCombos(ddlBuscarFamiliarAula)
            cargarComboFamiliarAlumnoGrado()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarFamiliarGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlBuscarFamiliarAula)
            cargarComboFamiliarAlumnoAulas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVerFichaTemporal_Click()
        Try
            ListaFichaTemporal()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCerraFichaTemporal_Click()
        ViewState("FichaTemporal") = False
        pnModalFichasTemporales.Hide()
    End Sub

    Protected Sub rbTipoProcAtencion_SelectedIndexChanged()

        If rbTipoProcAtencion.SelectedValue = 1 Then
            ddlProcTaller.Visible = False
            ddlProcCurso.Visible = True
            tbProcOtro.Visible = False

            pnlTipoProcAtencionCurso.Visible = True
            pnlTipoProcAtencionTaller.Visible = False
            pnlTipoProcAtencionOtro.Visible = False
            spanProcAtencion.Visible = True
        ElseIf rbTipoProcAtencion.SelectedValue = 2 Then
            ddlProcTaller.Visible = True
            ddlProcCurso.Visible = False
            tbProcOtro.Visible = False

            pnlTipoProcAtencionCurso.Visible = False
            pnlTipoProcAtencionTaller.Visible = True
            pnlTipoProcAtencionOtro.Visible = False
            spanProcAtencion.Visible = True
        ElseIf rbTipoProcAtencion.SelectedValue = 3 Then
            ddlProcTaller.Visible = False
            ddlProcCurso.Visible = False
            tbProcOtro.Visible = False

            pnlTipoProcAtencionCurso.Visible = False
            pnlTipoProcAtencionTaller.Visible = False
            pnlTipoProcAtencionOtro.Visible = False
            spanProcAtencion.Visible = False
        ElseIf rbTipoProcAtencion.SelectedValue = 4 Then
            ddlProcTaller.Visible = False
            ddlProcCurso.Visible = False
            tbProcOtro.Visible = True

            pnlTipoProcAtencionCurso.Visible = False
            pnlTipoProcAtencionTaller.Visible = False
            pnlTipoProcAtencionOtro.Visible = True
            spanProcAtencion.Visible = True
        End If

    End Sub

#End Region

#Region "Eventos Mant Ficha"

    Protected Sub ddlSede_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarComboMedicamentoKardex()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnFichaCancelar_Click()
        CancelarFicha()
        ViewState("FichaTemporal") = False
        ViewState("VerFicha") = False
    End Sub

    Protected Sub btnFichaGrabar_click()
        Try
            Dim usp_mensaje As String = ""
            If validarFicha(usp_mensaje) Then
                GrabarFicha()
            Else
                MostrarAlertas(usp_mensaje)
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVerDatosRelevantes_Click()
        Try
            VerDatosRelevantes()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVerDatosSeguro_Click()
        Try
            VerDatosSeguro()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVerContactos_Click()
        Try
            VerDatosContactos()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVerFichaMedica_Click()
        Try
            VerFichaMedica()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Mantenimiento Detalle Diagnostico"

#Region "Eventos"

    Protected Sub btnAgregarDetalleDiagnostico_Click()
        pnModalDiagnostico.Show()
    End Sub

    Protected Sub btnModalAceptarDiagnostico_Click()
        Try
            Dim resulado As Boolean
            agregarDiagnostico(resulado)
            If resulado = False Then
                pnModalDiagnostico.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(200, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarDiagnostico_Click()
        cerrarModalDiagnostico()
    End Sub

    Protected Sub btnAgregarRegistroDiagnostico_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim str_miDDL As String = ddlDiagnostico.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = pnModalDiagnostico.UniqueID.ToString

        ucIngresarDiagnostico.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarDiagnostico.mostrarModal()

    End Sub

#End Region
#Region "Métodos"
    ''' <summary>
    ''' Agrega 1 diagnóstico al detalle de diagnóstico
    ''' </summary>
    ''' <param name="resultado">Valor de la variable resultado, indica si se agrego o no el nuevo registro diagnóstico al detalle</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarDiagnostico(ByRef resultado As Boolean)

        If ddlDiagnostico.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            ddlDiagnostico.SelectedValue = 0
            Exit Sub
        End If

        Dim dt As DataTable
        If ViewState("ListaDiagnosticos") Is Nothing Then
            dt = New DataTable("ListaDiagnosticos")
            dt = Datos.agregarColumna(dt, "Codigo", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
        Else
            dt = ViewState("ListaDiagnosticos")
        End If

        Dim boolContinuar As Boolean = True

        If dt.Rows.Count > 0 Then
            For Each auxdr As DataRow In dt.Rows
                If auxdr.Item("Codigo").ToString = ddlDiagnostico.SelectedValue Then
                    MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                    ddlDiagnostico.SelectedValue = 0
                    'Exit Sub
                    boolContinuar = False
                End If
            Next
        End If

        resultado = boolContinuar

        If boolContinuar Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("Codigo") = ddlDiagnostico.SelectedValue
            dr.Item("Descripcion") = ddlDiagnostico.SelectedItem.ToString
            dt.Rows.Add(dr)
            ViewState("ListaDiagnosticos") = dt
            GVListaDiagnosticos.DataSource = dt
            GVListaDiagnosticos.DataBind()
            ddlDiagnostico.SelectedValue = 0
            upDiagnostico.Update()
        End If

    End Sub

    ''' <summary>
    ''' Cierra el popup Diagnóstico
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalDiagnostico()

        pnModalDiagnostico.Hide()
        ddlDiagnostico.SelectedValue = 0

    End Sub

    ''' <summary>
    ''' Elimina 1 clinica del detalle de diagnósticos
    ''' </summary>
    ''' <param name="int_CodigoDiagnostico">Codigo del diagnóstico que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarDiagnostico(ByVal int_CodigoDiagnostico As Integer)
        Dim dt As DataTable
        dt = ViewState("ListaDiagnosticos")
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("Codigo").ToString = int_CodigoDiagnostico Then
                auxdr.Delete()
                Exit For
            End If
        Next

        dt.AcceptChanges()
        ViewState("ListaDiagnosticos") = dt
        GVListaDiagnosticos.DataSource = dt
        GVListaDiagnosticos.DataBind()
        upDiagnostico.Update()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub GVListaDiagnosticos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Eliminar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                int_CodigoAccion = 202
                eliminarDiagnostico(codigo)
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaDiagnosticos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                If ViewState("VerFicha") = True Then
                    btnEliminar.Visible = False
                ElseIf ViewState("VerFicha") = False Then
                    btnEliminar.Visible = True
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento Detalle Procedimiento"

#Region "Eventos"

    Protected Sub btnAgregarDetalleProcedimiento_Click()
        pnModalProcedimiento.Show()
    End Sub

    Protected Sub btnModalAceptarProcedimiento_Click()
        Try
            Dim resulado As Boolean
            agregarProcedimiento(resulado)
            If resulado = False Then
                pnModalProcedimiento.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(200, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarProcedimiento_Click()
        cerrarModalProcedimiento()
    End Sub

    Protected Sub btnAgregarRegistroProcedimiento_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim str_miDDL As String = ddlProcedimiento.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = pnModalProcedimiento.UniqueID.ToString

        ucIngresarProcedimiento.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarProcedimiento.mostrarModal()

    End Sub

#End Region
#Region "Métodos"

    ''' <summary>
    ''' Agrega 1 prodecimiento médico al detalle de procedimientos médicos
    ''' </summary>
    ''' <param name="resultado">Valor de la variable resultado, indica si se agrego o no el nuevo registro procedimiento médico al detalle</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarProcedimiento(ByRef resultado As Boolean)
        If ddlProcedimiento.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            ddlProcedimiento.SelectedValue = 0
            Exit Sub
        End If
        Dim dt As DataTable
        If ViewState("ListaProcedimientos") Is Nothing Then
            dt = New DataTable("ListaProcedimientos")
            dt = Datos.agregarColumna(dt, "Codigo", "String")
            dt = Datos.agregarColumna(dt, "Descripcion", "String")
        Else
            dt = ViewState("ListaProcedimientos")
        End If

        Dim boolContinuar As Boolean = True
        If dt.Rows.Count > 0 Then
            For Each auxdr As DataRow In dt.Rows
                If auxdr.Item("Codigo").ToString = ddlProcedimiento.SelectedValue Then
                    MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                    ddlProcedimiento.SelectedValue = 0
                    'Exit Sub
                    boolContinuar = False
                End If
            Next
        End If

        resultado = boolContinuar
        If boolContinuar Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("Codigo") = ddlProcedimiento.SelectedValue
            dr.Item("Descripcion") = ddlProcedimiento.SelectedItem.ToString
            dt.Rows.Add(dr)
            ViewState("ListaProcedimientos") = dt
            GVListaProcedimientos.DataSource = dt
            GVListaProcedimientos.DataBind()
            ddlProcedimiento.SelectedValue = 0
            upProcedimiento.Update()
        End If

    End Sub

    ''' <summary>
    ''' Cierra el popup procedimiento médico
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalProcedimiento()

        pnModalProcedimiento.Hide()
        ddlProcedimiento.SelectedValue = 0

    End Sub

    ''' <summary>
    ''' Elimina 1 procedimiento médico del detalle de procedimientos médicos
    ''' </summary>
    ''' <param name="int_CodigoProcedimiento">Codigo del procedimiento médico que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarProcedimiento(ByVal int_CodigoProcedimiento As Integer)
        Dim dt As DataTable
        dt = ViewState("ListaProcedimientos")
        For Each auxdr As DataRow In dt.Rows
            If auxdr.Item("Codigo").ToString = int_CodigoProcedimiento Then
                auxdr.Delete()
                Exit For
            End If
        Next
        dt.AcceptChanges()
        ViewState("ListaProcedimientos") = dt
        GVListaProcedimientos.DataSource = dt
        GVListaProcedimientos.DataBind()
        upProcedimiento.Update()
    End Sub

#End Region
#Region "Gridview"
    Protected Sub GVListaProcedimientos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Eliminar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                int_CodigoAccion = 202
                eliminarProcedimiento(codigo)
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaProcedimientos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                If ViewState("VerFicha") = True Then
                    btnEliminar.Visible = False
                ElseIf ViewState("VerFicha") = False Then
                    btnEliminar.Visible = True
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento Detalle Medicamento"

#Region "Eventos"

    Protected Sub btnAgregarDetalleMedicamento_Click()
        ViewState("NuevoMedicamento") = True
        pnModalMedicamentos.Show()
    End Sub

    Protected Sub btnModalAceptarMedicamento_Click()
        Try
            Dim resulado As Boolean
            If ViewState("NuevoMedicamento") = True Then
                agregarMedicamento(resulado)
            ElseIf ViewState("NuevoMedicamento") = False Then
                editarMedicamento(resulado)
            End If
            If resulado = False Then
                pnModalMedicamentos.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(200, ex.ToString)
        End Try
    End Sub

    Protected Sub btnModalCancelarMedicamento_Click()
        cerrarModalMedicamento()
    End Sub

    Protected Sub btnEditarDetalleMedicamento_Click()
        Try
            Dim resulado As Boolean
            editarMedicamento(resulado)
            If resulado = False Then
                pnModalMedicamentos.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(201, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Verifica si el medicamento seleccionado de la lista requiere o no Control de Cantidad
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    ''' 
    Protected Sub ddlMedicamento_SelectedIndexChanged()

        If ViewState("DatosMedicamentos") Is Nothing Then
            cargarViewStateKardex()
        Else
            If CType(ViewState("DatosMedicamentos"), DataTable).Rows.Count <> ddlMedicamento.Items.Count - 1 Then
                cargarViewStateKardex()
            End If
        End If
        Dim dt As DataTable = ViewState("DatosMedicamentos")

        Dim codigoMedicamento As Integer = ddlMedicamento.SelectedValue
        For Each dr As DataRow In dt.Rows
            If dr.Item("CodigoMedicamento") = codigoMedicamento Then
                If dr.Item("RequiereControlCantidad") = "No" Then
                    tbCantidadMedicamento.Visible = False
                    lblCantidadModalMedicamento.Visible = False
                    tbCantidadMedicamento.Text = ""
                ElseIf dr.Item("RequiereControlCantidad") = "Si" Then
                    tbCantidadMedicamento.Visible = True
                    lblCantidadModalMedicamento.Visible = True
                End If
                Exit For
            End If
        Next
        pnModalMedicamentos.Show()
    End Sub

    Protected Sub btnAgregarRegistroMedicamento_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim str_miDDL As String = ddlMedicamento.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = pnModalMedicamentos.UniqueID.ToString

        Dim bool_ControlesAuxiliares As Boolean = True
        Dim str_miControlLabel As String = lblCantidadModalMedicamento.UniqueID.ToString
        Dim str_miControlTextbox As String = tbCantidadMedicamento.UniqueID.ToString

        ucIngresarMedicamento.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarMedicamento.mostrarModal()

    End Sub

#End Region
#Region "Métodos"
    ''' <summary>
    ''' Agrega 1 medicamneto al detalle de medicamentos
    ''' </summary>
    ''' <param name="resultado">Valor de la variable resultado, indica si se agrego o no el nuevo registro medicamento al detalle</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarMedicamento(ByRef resultado As Boolean)

        Dim boolContinuar As Boolean = True
        Dim boolAgregar As Boolean = True

        If ddlMedicamento.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            ddlMedicamento.SelectedValue = 0
            tbCantidadMedicamento.Text = 0
            boolContinuar = False
        End If

        resultado = boolContinuar
        If boolContinuar Then
            Dim dt As DataTable
            Dim boolActualizo As Boolean = True
            If ViewState("ListaMedicamentos") Is Nothing Then
                dt = New DataTable("ListaMedicamentos")
                dt = Datos.agregarColumna(dt, "Codigo", "String")
                dt = Datos.agregarColumna(dt, "Descripcion", "String")
                dt = Datos.agregarColumna(dt, "Cantidad", "decimal")
            Else
                dt = ViewState("ListaMedicamentos")
            End If

            If ViewState("DatosMedicamentos") Is Nothing Then
                cargarViewStateKardex()
            Else
                If CType(ViewState("DatosMedicamentos"), DataTable).Rows.Count <> ddlMedicamento.Items.Count - 1 Then
                    cargarViewStateKardex()
                End If
            End If
            Dim dv_Medicamento As DataTable = ViewState("DatosMedicamentos")

            Dim codigoMedicamento As Integer = ddlMedicamento.SelectedValue

            If dt.Rows.Count > 0 Then
                For Each auxdr As DataRow In dt.Rows
                    If auxdr.Item("Codigo").ToString = ddlMedicamento.SelectedValue Then
                        For Each drv_Medicamento As DataRow In dv_Medicamento.Rows
                            If drv_Medicamento.Item("CodigoMedicamento") = ddlMedicamento.SelectedValue Then
                                If (auxdr.Item("Cantidad") + Val(tbCantidadMedicamento.Text)) <= drv_Medicamento.Item("CantidadActual") Then
                                    auxdr.Item("Cantidad") += Val(tbCantidadMedicamento.Text)
                                Else
                                    MostrarSexyAlertBox("La cantidad máxima ha ingresar para el medicamento seleccionado es : " + drv_Medicamento.Item("CantidadActual").ToString, "Alert")
                                    boolActualizo = False
                                End If
                                Exit For
                            End If
                        Next
                        boolAgregar = False
                        Exit For
                    End If
                Next
            End If


            If boolAgregar Then
                For Each drv_Medicamento As DataRow In dv_Medicamento.Rows
                    If drv_Medicamento.Item("CodigoMedicamento") = codigoMedicamento Then
                        If Val(tbCantidadMedicamento.Text) <= drv_Medicamento.Item("CantidadActual") Then
                            Dim dr As DataRow
                            dr = dt.NewRow
                            dr.Item("Codigo") = ddlMedicamento.SelectedValue
                            dr.Item("Descripcion") = ddlMedicamento.SelectedItem.ToString
                            dr.Item("Cantidad") = Val(tbCantidadMedicamento.Text)
                            dt.Rows.Add(dr)
                        Else
                            MostrarSexyAlertBox("La cantidad máxima ha ingresar para el medicamento seleccionado es : " + drv_Medicamento.Item("CantidadActual").ToString, "Alert")
                            boolActualizo = False
                        End If
                        Exit For
                    End If
                Next
            End If

            resultado = boolContinuar
            ddlMedicamento.SelectedValue = 0
            tbCantidadMedicamento.Text = 0

            If boolActualizo Then
                ViewState("ListaMedicamentos") = dt
                GVListaMedicamentos.DataSource = dt
                GVListaMedicamentos.DataBind()
                upMedicamento.Update()
            End If

        End If

    End Sub

    ''' <summary>
    ''' Cierra el popup medicamento
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalMedicamento()

        pnModalMedicamentos.Hide()
        ddlMedicamento.SelectedValue = 0
        tbCantidadMedicamento.Text = 0

    End Sub

    ''' <summary>
    '''Activa opciones del detalle de medicamentos
    ''' </summary>
    ''' <param name="int_CodigoMedicamento">Codigo del diagnóstico que se desea eliminar</param>
    ''' <param name="int_CantidadMedicamento">Cantidad del medicamento</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarMedicamento(ByVal int_CodigoMedicamento As Integer, ByVal int_CantidadMedicamento As Integer)

        ddlMedicamento.SelectedValue = int_CodigoMedicamento
        hidencodigoMedicamento.Value = int_CodigoMedicamento
        tbCantidadMedicamento.Text = int_CantidadMedicamento
        ddlMedicamento_SelectedIndexChanged()
        pnModalMedicamentos.Show()

    End Sub

    ''' <summary>
    ''' Actualiza 1 medicamneto al detalle de medicamentos
    ''' </summary>
    ''' <param name="resultado">Valor de la variable resultado, indica si se actualizo o no el nuevo registro medicamento al detalle</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarMedicamento(ByRef resultado As Boolean)

        Dim boolContinuar As Boolean = True
        If ddlMedicamento.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar un registro valido.", "Alert")
            ddlMedicamento.SelectedValue = 0
            tbCantidadMedicamento.Text = 0
            boolContinuar = False
        End If

        resultado = boolContinuar
        If boolContinuar Then

            Dim int_CodigoOriginal As Integer = hidencodigoMedicamento.Value
            Dim dt As DataTable
            Dim boolIncremento As Boolean = False
            Dim boolActualizo As Boolean = True

            dt = ViewState("ListaMedicamentos")

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("Codigo").ToString = ddlMedicamento.SelectedValue And auxdr.Item("Codigo").ToString <> int_CodigoOriginal Then

                    MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
                    boolContinuar = False
                    boolActualizo = False
                    Exit For

                End If

            Next

            resultado = boolContinuar

            If boolActualizo Then

                Dim dv_Medicamento As DataTable = ViewState("DatosMedicamentos")
                Dim codigoMedicamento As Integer = ddlMedicamento.SelectedValue

                For Each auxdr As DataRow In dt.Rows

                    If auxdr.Item("Codigo").ToString = int_CodigoOriginal Then

                        For Each drv_Medicamento As DataRow In dv_Medicamento.Rows

                            If drv_Medicamento.Item("CodigoMedicamento") = ddlMedicamento.SelectedValue Then

                                If Val(tbCantidadMedicamento.Text) <= drv_Medicamento.Item("CantidadActual") Then

                                    auxdr.Item("codigo") = ddlMedicamento.SelectedValue
                                    auxdr.Item("Descripcion") = ddlMedicamento.SelectedItem.ToString
                                    auxdr.Item("Cantidad") = Val(tbCantidadMedicamento.Text)

                                Else

                                    MostrarSexyAlertBox("La cantidad máxima ha ingresar para el medicamento seleccionado es : " + drv_Medicamento.Item("CantidadActual").ToString, "Alert")
                                    boolActualizo = False
                                    boolContinuar = False

                                End If
                                Exit For

                            End If

                        Next
                        'boolActualizo = False
                        Exit For

                    End If

                Next

                resultado = boolContinuar
                tbCantidadMedicamento.Text = 0

                If boolActualizo Then

                    ViewState("ListaMedicamentos") = dt

                    GVListaMedicamentos.DataSource = dt
                    GVListaMedicamentos.DataBind()

                    upMedicamento.Update()

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Elimina 1 clinica del detalle de diagnósticos
    ''' </summary>
    ''' <param name="int_CodigoMedicamento">Codigo del diagnóstico que se desea eliminar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarMedicamento(ByVal int_CodigoMedicamento As Integer)

        Dim dt As DataTable
        dt = ViewState("ListaMedicamentos")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("Codigo").ToString = int_CodigoMedicamento Then

                auxdr.Delete()
                Exit For

            End If

        Next

        dt.AcceptChanges()

        ViewState("ListaMedicamentos") = dt

        GVListaMedicamentos.DataSource = dt
        GVListaMedicamentos.DataBind()
        upMedicamento.Update()

    End Sub
#End Region
#Region "Gridview"
    Protected Sub GVListaMedicamentos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 201
                    ViewState("NuevoMedicamento") = False
                    activarEditarMedicamento(codigo, CType(row.FindControl("Label3"), Label).Text)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarMedicamento(codigo)
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaMedicamentos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                If ViewState("VerFicha") = True Then
                    btnEliminar.Visible = False
                    btnActualizar.Visible = False
                ElseIf ViewState("VerFicha") = False Then
                    btnEliminar.Visible = True
                    btnActualizar.Visible = True
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Mantenimiento Fichas Temporales"

#Region "Eventos"
    Protected Sub btnAgregarFichaTemporal_Click()
        Try
            Dim usp_mensaje As String = ""
            If validarFicha(usp_mensaje) Then
                ViewState("FichaTemporal") = True
                GrabarFicha()
            Else
                MostrarAlertas(usp_mensaje)
            End If
        Catch ex As Exception
            EnvioEmailError(200, ex.ToString)
        End Try
    End Sub
#End Region
#Region "Métodos"
    ''' <summary>
    ''' Agrega una ficha de atención a la lista de fichas Temporales(esta lista de fichas temporales desapareceran con la caducación de la sesión del usuario)
    ''' </summary> 
    ''' <param name="codigoFichaAtencion">Codigo de la ficha de Atención que se va agregar a las fichas Temporales</param>    
    ''' <param name="NombrePaciente">Nombre del paciente</param>
    ''' <param name="TipoPaciente">Tipo de paciente</param>
    ''' <param name="fechaAtencion">Fecha de atención</param>
    ''' <param name="horaAtencion">Hora de atención</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub AgregarFichaTemporal(ByVal codigoFichaAtencion As Integer, ByVal NombrePaciente As String, ByVal TipoPaciente As String, ByVal fechaAtencion As Date, ByVal horaAtencion As Date)
        Dim dt As DataTable
        If Session("ListaFichasTemporales") Is Nothing Then
            dt = New DataTable("ListaFichasTemporales")
            dt = Datos.agregarColumna(dt, "CodigoFichaAtencion", "String")
            dt = Datos.agregarColumna(dt, "NombreCompleto", "String")
            dt = Datos.agregarColumna(dt, "DescTipoPaciente", "String")
            dt = Datos.agregarColumna(dt, "FechaHoraAtencion", "String")
        Else
            dt = Session("ListaFichasTemporales")
        End If

        If dt.Rows.Count > 0 Then
            For Each auxdr As DataRow In dt.Rows
                If auxdr.Item("CodigoFichaAtencion").ToString = codigoFichaAtencion Then
                    ViewState("FichaTemporal") = False
                    Exit Sub
                End If
            Next
        End If

        Dim dr As DataRow
        dr = dt.NewRow
        dr.Item("CodigoFichaAtencion") = codigoFichaAtencion
        dr.Item("NombreCompleto") = NombrePaciente
        dr.Item("DescTipoPaciente") = TipoPaciente
        dr.Item("FechaHoraAtencion") = fechaAtencion.ToShortDateString + " " + horaAtencion.ToShortTimeString
        dt.Rows.Add(dr)

        Session("ListaFichasTemporales") = dt
        ViewState("FichaTemporal") = False
    End Sub

    ''' <summary>
    ''' Lista las fichas de atención - temporales 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ListaFichaTemporal()

        Dim dt As DataTable
        If Session("ListaFichasTemporales") Is Nothing Then
            dt = New DataTable("ListaFichasTemporales")
            dt = Datos.agregarColumna(dt, "CodigoFichaAtencion", "String")
            dt = Datos.agregarColumna(dt, "NombreCompleto", "String")
            dt = Datos.agregarColumna(dt, "DescTipoPaciente", "String")
            dt = Datos.agregarColumna(dt, "FechaHoraAtencion", "String")
        Else
            dt = Session("ListaFichasTemporales")
        End If
        GVListaFichasTemporales.DataSource = dt
        GVListaFichasTemporales.DataBind()
        pnModalFichasTemporales.Show()

    End Sub

    ''' <summary>
    ''' Busca y elimina 1 fichas de atención de la lista de fichas temporales
    ''' </summary>
    ''' <param name="int_CodigoFichaTemporal">Codigo de la ficha de atención - temporal</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub eliminarFichasTemporal(ByVal int_CodigoFichaTemporal As Integer)

        If Session("ListaFichasTemporales") IsNot Nothing Then
            Dim dt As DataTable
            dt = Session("ListaFichasTemporales")
            If dt.Rows.Count > 0 Then
                For Each auxdr As DataRow In dt.Rows
                    If auxdr.Item("CodigoFichaAtencion").ToString = int_CodigoFichaTemporal Then
                        auxdr.Delete()
                        Exit For
                    End If
                Next
                dt.AcceptChanges()
                Session("ListaFichasTemporales") = dt
                GVListaFichasTemporales.DataSource = dt
                GVListaFichasTemporales.DataBind()
            End If
        End If

    End Sub
#End Region
#Region "Gridview"
    Protected Sub GVListaFichasTemporales_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Or e.CommandName = "Eliminar" Then

                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Seleccionar" Then
                    int_CodigoAccion = 204
                    pnModalFichasTemporales.Hide()
                    'LimpiarCamposFicha()
                    ViewState("VerFicha") = False
                    obtenerFicha(codigo)
                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 202
                    eliminarFichasTemporal(codigo)
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaFichasTemporales_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
            End If
        Catch ex As Exception
            EnvioEmailError(204, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#Region "Metodos Busqueda Ficha"

    ''' <summary>
    ''' Muestra un determinado panel de busqueda, dependiendo del tipo de paciente por el cual se quiera realizar la consulta
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub tipoBusqueda()
        If ddlBuscarTipoPaciente.SelectedValue = 1 Then
            ' pnlBuscarAlumno.Visible = True
            FSParametrosAlumno.Visible = True
            cargarCombosAlumno()
        Else
            'pnlBuscarAlumno.Visible = False
            FSParametrosAlumno.Visible = False
            limpiarCombosAlumno()
        End If
        If ddlBuscarTipoPaciente.SelectedValue = 3 Then
            'pnlBuscarFamiliar.Visible = True
            FSParametrosFamiliar.Visible = True
            cargarCombosFamiliarAlumno()
        Else
            'pnlBuscarFamiliar.Visible = False
            FSParametrosFamiliar.Visible = False
            limpiarCombosFamiliarAlumno()
        End If
    End Sub

    ''' <summary>
    ''' Limpia los parametros de busqueda de fichas de atención
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        ddlBuscarTipoPaciente.SelectedValue = 0
        FSParametrosAlumno.Visible = False
        'pnlBuscarAlumno.Visible = False

        tbBuscarApellidoPaterno.Text = ""
        tbBuscarApellidoMaterno.Text = ""
        tbBuscarNombre.Text = ""
        ddlBuscarNivel.SelectedValue = 0
        ddlBuscarSubNivel.SelectedValue = 0
        ddlBuscarGrado.SelectedValue = 0
        ddlBuscarAula.SelectedValue = 0
        tbBuscarFechaAtencionInicial.Text = Today.ToShortDateString
        tbBuscarFechaAtencionFinal.Text = Today.ToShortDateString
        ddlBuscarSede.SelectedValue = 0
        rbEstadosRegistros.SelectedValue = -1

    End Sub

    ''' <summary>
    ''' Valida los parametros de busqueda de la ficha de atención
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarBusquedaFicha(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If IsDate(tbBuscarFechaAtencionInicial.Text.Trim) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Atención Inicial")
            result = False
        End If

        If IsDate(tbBuscarFechaAtencionFinal.Text.Trim) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Atención Final")
            result = False
        End If

        If IsDate(tbBuscarFechaAtencionInicial.Text.Trim) And IsDate(tbBuscarFechaAtencionFinal.Text.Trim) Then
            If (CType(tbBuscarFechaAtencionInicial.Text, Date) > CType(tbBuscarFechaAtencionFinal.Text, Date)) Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 7, "Fecha de Atención")
                result = False
            End If
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Carga una serie de listas desplegables 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombos()
        cargarComboTipoPersona()
        cargarComboSedesColegio()
        cargarComboDiagnostico()
        cargarComboDestino()
        cargarComboProcedimiento()
        cargarComboCategoria()
      
    End Sub

    ''' <summary>
    ''' Limpia los items de una lista desplegable
    ''' </summary>
    ''' <param name="combo">Nombre que identifica a la lista desplegable</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)

        Controles.limpiarCombo(combo, True, False)

    End Sub

    ''' <summary>
    ''' Carga una serie de listas desplegables referente a nivel, subnivel, grado y aula de los alumnos 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombosAlumno()

        cargarComboAlumnoNivel()
        limpiarCombos(ddlBuscarSubNivel)
        limpiarCombos(ddlBuscarGrado)
        limpiarCombos(ddlBuscarAula)

    End Sub

    ''' <summary>
    ''' Limpia las lista desplegable de nivel, subnivel, grado y aula de los alumnos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombosAlumno()

        limpiarCombos(ddlBuscarNivel)
        limpiarCombos(ddlBuscarSubNivel)
        limpiarCombos(ddlBuscarGrado)
        limpiarCombos(ddlBuscarAula)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Tipo de personas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoPersona()

        Dim obj_BL_TiposPersonas As New bl_TiposPersonas
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_TiposPersonas.FUN_LIS_TiposPersonas("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlBuscarTipoPaciente, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Niveles de Alumno disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAlumnoNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlBuscarNivel, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de SubNiveles de Alumno disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAlumnoSubNivel()

        Dim obj_BL_SubNiveles As New bl_SubNiveles
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(CInt(ddlBuscarNivel.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlBuscarSubNivel, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Grados de Alumno disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAlumnoGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(CInt(ddlBuscarSubNivel.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlBuscarGrado, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Aulas de Alumno disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAlumnoAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(CInt(ddlBuscarGrado.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlBuscarAula, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga una serie de listas desplegables referente a nivel, subnivel, grado y aula de los alumnos, para las busquedas de los familiares del alumno
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombosFamiliarAlumno()

        cargarComboFamiliarAlumnoNivel()
        limpiarCombos(ddlBuscarFamiliarSubNivel)
        limpiarCombos(ddlBuscarFamiliarGrado)
        limpiarCombos(ddlBuscarFamiliarAula)

    End Sub

    ''' <summary>
    ''' Limpia las lista desplegable de nivel, subnivel, grado y aula de los alumnos, usados en las busquedas de los familiares del alumno
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombosFamiliarAlumno()

        limpiarCombos(ddlBuscarFamiliarNivel)
        limpiarCombos(ddlBuscarFamiliarSubNivel)
        limpiarCombos(ddlBuscarFamiliarGrado)
        limpiarCombos(ddlBuscarFamiliarAula)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Niveles de Alumno disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlBuscarFamiliarNivel, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de SubNiveles de Alumno disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoSubNivel()

        Dim obj_BL_SubNiveles As New bl_SubNiveles
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(CInt(ddlBuscarFamiliarNivel.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlBuscarFamiliarSubNivel, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Grados de Alumno disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(CInt(ddlBuscarFamiliarSubNivel.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlBuscarFamiliarGrado, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Aulas de Alumno disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(CInt(ddlBuscarFamiliarGrado.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlBuscarFamiliarAula, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Sedes disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSedesColegio()

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlSede, ds_Lista, "Codigo", "NombreSede", False, True)
        Controles.llenarCombo(ddlBuscarSede, ds_Lista, "Codigo", "NombreSede", True, False)

    End Sub

    ''' <summary>
    ''' Se realizará una accion de activar o eliminar un registro de ficha de atención
    ''' </summary>
    ''' <param name="int_Codigo">Codigo de la ficha de atención a la cual se le realizara una accion(Eliminar o Activar)</param>
    ''' <param name="str_accion">Acción que se realizará sobre la ficha de atención</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_BL_FichaAtencion As New bl_FichaAtencion
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_FichaAtencion.FUN_DEL_FichaAtencion(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        listarFichas()

    End Sub

#End Region

#Region "Metodos Mant Ficha"

    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(1, 2)

    End Sub

    ''' <summary>
    ''' Muestra una ventana(popup) con la información relevante del alumno
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerDatosRelevantes()

        Dim obj_BL_FichaAtencion As New bl_FichaAtencion
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_FichaAtencion.FUN_LIS_DatosRelevantesFichaAtencion(hidenCodigoPersona.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)

        Dim miJs As New StringBuilder

        Dim strTitle As String = "Datos Relevantes del alumno : " & ds_Lista.Tables(0).Rows(0).Item("NombreCompleto")
        Dim strContent As New StringBuilder

        strContent = scriptDatosRelevantes(ds_Lista)

        miJs.Append("var Content = ""<html><head><title>" & strTitle & "</title></head>")
        miJs.Append("<body>" & strContent.ToString & "</body></html>"";")

        miJs.Append("var popupDR = window.open('','window','resizeable,width=450,height=250,scrollbars=yes');")
        miJs.Append("popupDR.document.write(Content);")
        miJs.Append("popupDR.document.close();")

        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "openDatosRelevantes", miJs.ToString, True)

    End Sub

    ''' <summary>
    ''' Muestra una ventana(popup) con la información relevante del alumno
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     28/06/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerDatosSeguro()

        Dim obj_BL_FichaAtencion As New bl_FichaAtencion
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_FichaAtencion.FUN_LIS_DatosSeguroFichaAtencion(hidenCodigoPersona.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)

        Dim miJs As New StringBuilder

        Dim strTitle As String = "Datos de Seguro del alumno : " & ds_Lista.Tables(0).Rows(0).Item("NombreCompleto")
        Dim strContent As New StringBuilder

        strContent = scriptDatosSeguro(ds_Lista)

        miJs.Append("var Content = ""<html><head><title>" & strTitle & "</title></head>")
        miJs.Append("<body>" & strContent.ToString & "</body></html>"";")

        miJs.Append("var popupDR = window.open('','window','resizeable,width=600,height=300,scrollbars=yes');")
        miJs.Append("popupDR.document.write(Content);")
        miJs.Append("popupDR.document.close();")

        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "openDatosSeguro", miJs.ToString, True)

    End Sub

    Private Sub VerDatosContactos()

        'Dim obj_BL_FichaAtencion As New bl_FichaAtencion
        'Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        'Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        'Dim ds_Lista As DataSet = obj_BL_FichaAtencion.FUN_LIS_ContactosFichaAtencion(hidenCodigoPersona.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)

        'Dim miJs As New StringBuilder

        'Dim strTitle As String = "Contactos del alumno : " & ds_Lista.Tables(0).Rows(0).Item("NombreCompleto")
        'Dim strContent As New StringBuilder

        'strContent = scriptDatosContactos(ds_Lista)




        'miJs.Append("var Content = ""<html><head><title>" & strTitle & "</title></head>")


        '' miJs.Append("<body>" & strContent.ToString & "</body></html>"";")

        'Dim pruebas As String = strContent.ToString

        'miJs.Append("<body>" & htmlXMl.Replace("", "") & "</body></html>"";")


        'miJs.Append("var popupC = window.open('','window','resizeable,width=450,height=250,scrollbars=yes');")
        'miJs.Append("popupC.document.write(Content);")

        'miJs.Append("popupC.document.close();")


        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>detalle(" & New System.Web.Script.Serialization.JavaScriptSerializer().Serialize(New With {.codPersona = hidenCodigoPersona.Value}) & ") </script>", False)


        'ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "openContactos", miJs.ToString, True)


    End Sub

    ''' <summary>
    ''' Muestra una ventana(popup) con información sobre la ficha médica del alumno
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerFichaMedica()

        Try

            Dim obj_BL_MaestroPersonas As New bl_MaestroPersonas
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
            Dim ds_Alumno As DataSet = obj_BL_MaestroPersonas.FUN_GET_AlumnoPorCodigoPersona(hidenCodigoPersona.Value, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)

            Dim CodigoAlumno As Integer
            CodigoAlumno = CInt(ds_Alumno.Tables(0).Rows(0).Item("CodigoAlumno").ToString)

            If CodigoAlumno > 0 Then

                Dim obj_BL_FichaMedica As New bl_FichaMedicasAlumnos
                Dim ds_Lista As DataSet = obj_BL_FichaMedica.FUN_GET_FichaMedicasAlumnos(CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)

                Dim reporte_html As String = ""
                reporte_html = Exportacion.ExportarReporteFichaMedica_Html(ds_Lista, "")
                Session("Exportaciones_RepFichaMedicaHtml") = reporte_html
                ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionFichaMedica_html();</script>", False)

            Else
                EnvioEmailError(205, "Error")
            End If

        Catch ex As Exception
            EnvioEmailError(205, ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Obtiene información relevante del alumno y los devuelve en formato html
    ''' </summary>
    ''' <param name="ds_Lista">Conjunto de Datos que contiene los datos relevantes del alumno</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function scriptDatosRelevantes(ByVal ds_Lista As DataSet) As StringBuilder

        Dim strContent As New StringBuilder

        strContent.Append("<table width='400' border='0' align='center' cellpadding='0' cellspacing='0'>")
        strContent.Append("<tr>")
        strContent.Append("<td width='400' height='250' align='center' valign='top'>")

        strContent.Append("<table border='0' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif; font-size: 16px; width: 400px;'>")

        strContent.Append("<tr>")
        strContent.Append("<td colspan='2'>")
        strContent.Append("<div align='center'>")
        strContent.Append("<span style='font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-weight: normal; text-decoration: underline;'>DATOS RELEVANTES DEL ALUMNO</span>")
        strContent.Append("</div>")
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr>")
        strContent.Append("<td colspan='2'>")
        strContent.Append("<div align='center'>")
        strContent.Append("<span style='font-family: Arial, Helvetica, sans-serif; font-size: 13px; font-weight: normal;'>")
        strContent.Append(ds_Lista.Tables(0).Rows(0).Item("NombreCompleto"))
        strContent.Append("</span>")
        strContent.Append("</div>")
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        strContent.Append("<tr>")
        strContent.Append("<td colspan='2' align='center' valign='top'>")

        strContent.Append("<table cellpadding='0' cellspacing='0' border='0' style='width: 100%; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 11px; vertical-align: top;'>")
        strContent.Append("<tr><td colspan='2' style='width:400px' align='left' valign='top'>&nbsp;</td></tr>")
        strContent.Append("<tr>")
        strContent.Append("<td style='width:150px' align='left' valign='top'>")
        strContent.Append("Tipo de Sangre:</td>")
        strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        strContent.Append(ds_Lista.Tables(0).Rows(0).Item("DescripcionTipoSangre"))
        strContent.Append("</td>")
        strContent.Append("</tr>")
        strContent.Append("<tr><td colspan='2'><br /></td></tr>")
        'Lista de Alergias
        strContent.Append("<tr>")
        strContent.Append("<td style='width:150px' align='left' valign='top'>")
        strContent.Append("Lista de Alergias :</td>")
        strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")

        'Detalle : Alergias
        strContent.Append("<table cellpadding='0' cellspacing='0' border='0' style='width: 250px;'>")
        If ds_Lista.Tables(1).Rows.Count > 0 Then
            For Each dr As DataRow In ds_Lista.Tables(1).Rows
                strContent.Append("<tr><td align='left' valign='top' style='width:100%; font-family: Arial, Helvetica, sans-serif; font-size: 11px'>" & dr.Item("DescripcionAlergia") & "</td></tr>")
            Next
        Else
            strContent.Append("<tr><td align='left' valign='top' style='width:100%'>-</td></tr>")
        End If
        strContent.Append("</table>")

        strContent.Append("<tr><td colspan='2'><br /></td></tr>")
        'Lista de Medicamentos
        strContent.Append("<tr>")
        strContent.Append("<td style='width:150px' align='left' valign='top'>")
        strContent.Append("Lista de Medicamentos :</td>")
        strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")

        'Detalle : Medicamentos
        strContent.Append("<table cellpadding='0' cellspacing='0' border='0' style='width: 250px;'>")
        If ds_Lista.Tables(2).Rows.Count > 0 Then
            For Each dr As DataRow In ds_Lista.Tables(2).Rows
                strContent.Append("<tr><td align='left' valign='top' style='width:100%; font-family: Arial, Helvetica, sans-serif; font-size: 11px'>" & dr.Item("Medicamento") & " / " & dr.Item("PresentCant") & " - " & dr.Item("Observaciones") & "</td></tr>")
            Next
        Else
            strContent.Append("<tr><td align='left' valign='top' style='width:100%'>-</td></tr>")
        End If
        strContent.Append("</table>")

        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("</table>")

        strContent.Append("</td>")
        strContent.Append("</tr>")
        strContent.Append("</table>")

        strContent.Append("</td>")
        strContent.Append("</tr>")
        strContent.Append("</table>")

        Return strContent

    End Function

    ''' <summary>
    ''' Obtiene información relevante del alumno y los devuelve en formato html
    ''' </summary>
    ''' <param name="ds_Lista">Conjunto de Datos que contiene los datos relevantes del alumno</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     28/06/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function scriptDatosSeguro(ByVal ds_Lista As DataSet) As StringBuilder

        Dim strContent As New StringBuilder

        strContent.Append("<table width='580px' border='0' align='center' cellpadding='0' cellspacing='0'>")
        strContent.Append("<tr>")
        strContent.Append("<td width='580px' height='250px' align='center' valign='top'>")

        strContent.Append("<table border='0' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif; font-size: 16px; width: 580px;'>")

        strContent.Append("<tr>")
        strContent.Append("<td colspan='2'>")
        strContent.Append("<div align='center'>")
        strContent.Append("<span style='font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-weight: normal; text-decoration: underline;'>DATOS DE SEGURO DEL ALUMNO</span>")
        strContent.Append("</div>")
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr>")
        strContent.Append("<td colspan='2'>")
        strContent.Append("<div align='center'>")
        strContent.Append("<span style='font-family: Arial, Helvetica, sans-serif; font-size: 13px; font-weight: normal;'>")
        strContent.Append(ds_Lista.Tables(0).Rows(0).Item("NombreCompleto"))
        strContent.Append("</span>")
        strContent.Append("</div>")
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        strContent.Append("<tr>")
        strContent.Append("<td colspan='2' align='center' valign='top'>")

        'Lista de Seguro
        strContent.Append("<tr>")
        strContent.Append("<td style='width:170px' align='left' valign='top'>")
        strContent.Append("Lista de Seguros :</td>")
        strContent.Append("<td style='width:270px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>&nbsp;</td></tr>")
        'strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        strContent.Append("<tr>")
        strContent.Append("<td colspan='2' style='width:580px' align='left' valign='top'>")
        'Detalle : Seguro
        strContent.Append("<table cellpadding='0' cellspacing='0' border='0' style='width: 580px;'>")
        strContent.Append("<tr><td align='left' valign='top' style='border:solid 1px #000000; width:50px;  font-size:10px; ' align='left' valign='bottom'><b>Año</b></td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-size:10px; ' align='left' valign='bottom'><b>Tipo</b></td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-size:10px; ' align='left' valign='bottom'><b>Compañia</b></td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-size:10px; ' align='left' valign='bottom'><b>Numero poliza</b></td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-size:10px; ' align='left' valign='bottom'><b>Vigencia</b></td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:80px; font-size:10px; ' align='left' valign='bottom'><b>Clinicas</b></td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-size:10px; ' align='left' valign='bottom'><b>Ambulancia</b></td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-size:10px; ' align='left' valign='bottom'><b>Copia de carnet</b></td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:50px; font-size:10px; ' align='left' valign='bottom'><b>Telf.Ambulancia</b></td></tr>")

        If ds_Lista.Tables(1).Rows.Count > 0 Then
            For Each dr As DataRow In ds_Lista.Tables(1).Rows
                strContent.Append("<tr><td align='left' valign='top' style='border:solid 1px #000000;  width:50px; font-family: Arial, Helvetica, sans-serif; font-size: 10px'>" & dr.Item("AnioMatricula") & "</td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-family: Arial, Helvetica, sans-serif; font-size:10px; ' align='left' valign='bottom'>" & dr.Item("Tipo") & "</td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-family: Arial, Helvetica, sans-serif; font-size:10px; ' align='left' valign='bottom'>" & dr.Item("Compania") & "</td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-family: Arial, Helvetica, sans-serif; font-size:10px; ' align='left' valign='bottom'>" & dr.Item("NumeroPoliza") & "</td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-family: Arial, Helvetica, sans-serif; font-size:10px; ' align='left' valign='bottom'>" & dr.Item("VigenciaTime") & "</td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:80px; font-family: Arial, Helvetica, sans-serif; font-size:10px; ' align='left' valign='bottom'>" & dr.Item("Clinica") & "</td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-family: Arial, Helvetica, sans-serif; font-size:10px; ' align='left' valign='bottom'>" & dr.Item("AmbulanciaCompania") & "</td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:70px; font-family: Arial, Helvetica, sans-serif; font-size:10px; ' align='left' valign='bottom'>" & dr.Item("Carnet") & "</td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:50px; font-family: Arial, Helvetica, sans-serif; font-size:10px; ' align='left' valign='bottom'>" & dr.Item("TelefonoAmbulancia") & "</td></tr>")
            Next
        Else
            strContent.Append("<tr><td colspan='9'  align='left' valign='top' style='width:100%'>-</td></tr>")
        End If
        strContent.Append("</table></td></tr>")

        strContent.Append("<tr><td colspan='2'><br /></td></tr>")

        'Lista de Renta Estudiantil
        strContent.Append("<tr>")
        strContent.Append("<td style='width:150px' align='left' valign='top'>")
        strContent.Append("Lista de Renta Estudiantil :</td>")
        strContent.Append("<td style='width:100px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        strContent.Append("<tr>")
        strContent.Append("<td colspan='2' style='width:300px' align='left' valign='top'>")

        'Detalle : Renta Estudiantil
        strContent.Append("<table cellpadding='0' cellspacing='0' border='0' style='width: 300px;'>")
        strContent.Append("<tr><td align='left' valign='top' style='border:solid 1px #000000; width:50px;  font-size:10px; ' align='left' valign='bottom'><b>Año</b></td>" & _
                         "<td align='left' valign='top' style='border:solid 1px #000000; width:125px; font-size:10px; ' align='left' valign='bottom'><b>1er Titular</b></td>" & _
                         "<td align='left' valign='top' style='border:solid 1px #000000; width:125px; font-size:10px; ' align='left' valign='bottom'><b>2do Titular</b></td></tr>")

        If ds_Lista.Tables(2).Rows.Count > 0 Then
            For Each dr As DataRow In ds_Lista.Tables(2).Rows
                strContent.Append("<tr><td align='left' valign='top' style='width:50px; font-family: Arial, Helvetica, sans-serif; font-size: 10px'>" & dr.Item("AnioAcademico") & "</td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:125px; font-family: Arial, Helvetica, sans-serif; font-size:10px; ' align='left' valign='bottom'>" & dr.Item("FamiliarPrimerTitular") & "</td>" & _
                          "<td align='left' valign='top' style='border:solid 1px #000000; width:125px; font-family: Arial, Helvetica, sans-serif; font-size:10px; ' align='left' valign='bottom'>" & dr.Item("FamiliarSegundoTitular") & "</td></tr>")
            Next
        Else
            strContent.Append("<tr><td colspan='3' align='left' valign='top' style='width:100%'>-</td></tr>")
        End If
        strContent.Append("</table></td></tr>")

        strContent.Append("<tr><td colspan='2'><br /></td></tr>")
        'strContent.Append("</table>")

        'strContent.Append("</td>")
        'strContent.Append("</tr>")

        'strContent.Append("</table>")

        'strContent.Append("</td>")
        'strContent.Append("</tr>")
        'strContent.Append("</table>")

        'strContent.Append("</td>")
        'strContent.Append("</tr>")
        'strContent.Append("</table>")

        Return strContent

    End Function

    ''' <summary>
    ''' Obtiene información de los datos de los contactos del del alumno y los devuelve en formato html
    ''' </summary>
    ''' <param name="ds_Lista">Conjunto de Datos que contiene los datos relevantes del alumno</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function scriptDatosContactos(ByVal ds_Lista As DataSet) As StringBuilder

        Dim strContent As New StringBuilder

        strContent.Append("<table width='400' border='0' align='center' cellpadding='0' cellspacing='0'>")
        strContent.Append("<tr>")
        strContent.Append("<td width='400' height='250' align='center' valign='top'>")

        strContent.Append("<table border='0' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif; font-size: 16px; width: 400px;'>")


        strContent.Append("<tr>")
        strContent.Append("<td colspan='2'>")
        strContent.Append("<div align='center'>")
        strContent.Append("<span style='font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-weight: normal; text-decoration: underline;'>CONTACTOS DEL ALUMNO</span>")
        strContent.Append("</div>")
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr>")
        strContent.Append("<td colspan='2'>")
        strContent.Append("<div align='center'>")
        strContent.Append("<span style='font-family: Arial, Helvetica, sans-serif; font-size: 13px; font-weight: normal;'>")
        strContent.Append(ds_Lista.Tables(0).Rows(0).Item("NombreCompleto"))
        strContent.Append("</span>")
        strContent.Append("</div>")
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        strContent.Append("<tr>")
        strContent.Append("<td colspan='2' align='center' valign='top'>")

        strContent.Append("<table cellpadding='0' cellspacing='0' border='0' style='width: 100%; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 11px; vertical-align: top;'>")
        strContent.Append("<tr><td colspan='2' style='width:400px' align='left' valign='top'>&nbsp;</td></tr>")

        strContent.Append("<tr>")
        strContent.Append("<td colspan='2' style='font-family: Arial, Helvetica, sans-serif; font-weight: normal; font-size: 13px;'>")
        strContent.Append("Datos del conctacto</td>")
        strContent.Append("</tr>")
        strContent.Append("<tr>")
        strContent.Append("<td colspan='2' style='font-family: Arial, Helvetica, sans-serif; font-weight: bold'>")
        strContent.Append("<div style='BORDER-TOP: #6fa4d4 1px solid;width:400px'></div>")
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr>")
        strContent.Append("<td style='width:150px' align='left' valign='top'>")
        strContent.Append("Nombre Completo:</td>")
        strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        strContent.Append(ds_Lista.Tables(0).Rows(0).Item("NombreContacto"))
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr><td colspan='2'><br /></td></tr>")

        strContent.Append("<tr>")
        strContent.Append("<td style='width:150px' align='left' valign='top'>")
        strContent.Append("Teléfono de Casa:</td>")
        strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        strContent.Append(ds_Lista.Tables(0).Rows(0).Item("TelefonoCasaContacto"))
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr><td colspan='2'><br /></td></tr>")

        strContent.Append("<tr>")
        strContent.Append("<td style='width:150px' align='left' valign='top'>")
        strContent.Append("Celular:</td>")
        strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        strContent.Append(ds_Lista.Tables(0).Rows(0).Item("CellContacto"))
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr><td colspan='2'><br /></td></tr>")

        strContent.Append("<tr>")
        strContent.Append("<td style='width:150px' align='left' valign='top'>")
        strContent.Append("Teléfono de oficina:</td>")
        strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        strContent.Append(ds_Lista.Tables(0).Rows(0).Item("TelefonoOficinaContacto"))
        strContent.Append("</td>")
        strContent.Append("</tr>")

        strContent.Append("<tr><td colspan='2'><br /></td></tr>")

        strContent.Append("</table>")

        strContent.Append("</td>")
        strContent.Append("</tr>")

        '-----------datos del Padre--------
        If ds_Lista.Tables(1).Rows.Count > 0 Then

            strContent.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
            strContent.Append("<tr>")
            strContent.Append("<td colspan='2' align='center' valign='top'>")

            strContent.Append("<table cellpadding='0' cellspacing='0' border='0' style='width: 100%; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 11px; vertical-align: top;'>")
            strContent.Append("<tr><td colspan='2' style='width:400px' align='left' valign='top'>&nbsp;</td></tr>")

            strContent.Append("<tr>")
            strContent.Append("<td colspan='2' style='font-family: Arial, Helvetica, sans-serif; font-weight: normal; font-size: 13px;'>")
            strContent.Append("Datos del Padre</td>")
            strContent.Append("</tr>")
            strContent.Append("<tr>")
            strContent.Append("<td colspan='2' style='font-family: Arial, Helvetica, sans-serif; font-weight: bold'>")
            strContent.Append("<div style='BORDER-TOP: #6fa4d4 1px solid;width:400px'></div>")
            strContent.Append("</td>")
            strContent.Append("</tr>")

            strContent.Append("<tr>")
            strContent.Append("<td style='width:150px' align='left' valign='top'>")
            strContent.Append("Nombre Completo:</td>")
            strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
            strContent.Append(ds_Lista.Tables(1).Rows(0).Item("NombreCompletoPadre"))
            strContent.Append("</td>")
            strContent.Append("</tr>")

            strContent.Append("<tr><td colspan='2'><br /></td></tr>")

            strContent.Append("<tr>")
            strContent.Append("<td style='width:150px' align='left' valign='top'>")
            strContent.Append("Teléfono de Casa:</td>")
            strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
            strContent.Append(ds_Lista.Tables(1).Rows(0).Item("TelCasa"))
            strContent.Append("</td>")
            strContent.Append("</tr>")

            strContent.Append("<tr><td colspan='2'><br /></td></tr>")

            strContent.Append("<tr>")
            strContent.Append("<td style='width:150px' align='left' valign='top'>")
            strContent.Append("Celular:</td>")
            strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
            strContent.Append(ds_Lista.Tables(1).Rows(0).Item("TelCelular"))
            strContent.Append("</td>")
            strContent.Append("</tr>")

            strContent.Append("<tr><td colspan='2'><br /></td></tr>")

            strContent.Append("<tr>")
            strContent.Append("<td style='width:150px' align='left' valign='top'>")
            strContent.Append("Teléfono de oficina:</td>")
            strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
            strContent.Append(ds_Lista.Tables(1).Rows(0).Item("TelOficina"))
            strContent.Append("</td>")
            strContent.Append("</tr>")

            strContent.Append("<tr><td colspan='2'><br /></td></tr>")

            strContent.Append("</table>")

            strContent.Append("</td>")
            strContent.Append("</tr>")

        End If
        '------------------

        '-----------datos del Madre--------

        'strContent.Append("<tr><td colspan='2'>&nbsp;</td></tr>")
        'strContent.Append("<tr>")
        'strContent.Append("<td colspan='2' align='center' valign='top'>")

        'strContent.Append("<table cellpadding='0' cellspacing='0' border='0' style='width: 100%; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 11px; vertical-align: top;'>")
        'strContent.Append("<tr><td colspan='2' style='width:400px' align='left' valign='top'>&nbsp;</td></tr>")

        'strContent.Append("<tr>")
        'strContent.Append("<td colspan='2' style='font-family: Arial, Helvetica, sans-serif; font-weight: normal; font-size: 13px;'>")
        'strContent.Append("Datos del Padre</td>")
        'strContent.Append("</tr>")
        'strContent.Append("<tr>")
        'strContent.Append("<td colspan='2' style='font-family: Arial, Helvetica, sans-serif; font-weight: bold'>")
        'strContent.Append("<div style='BORDER-TOP: #6fa4d4 1px solid;width:400px'></div>")
        'strContent.Append("</td>")
        'strContent.Append("</tr>")

        'strContent.Append("<tr>")
        'strContent.Append("<td style='width:150px' align='left' valign='top'>")
        'strContent.Append("Nombre Completo:</td>")
        'strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        'strContent.Append(ds_Lista.Tables(2).Rows(0).Item("NombreCompletoMadre"))
        'strContent.Append("</td>")
        'strContent.Append("</tr>")

        'strContent.Append("<tr><td colspan='2'><br /></td></tr>")

        'strContent.Append("<tr>")
        'strContent.Append("<td style='width:150px' align='left' valign='top'>")
        'strContent.Append("Teléfono de Casa:</td>")
        'strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        'strContent.Append(ds_Lista.Tables(2).Rows(0).Item("TelCasa"))
        'strContent.Append("</td>")
        'strContent.Append("</tr>")

        'strContent.Append("<tr><td colspan='2'><br /></td></tr>")

        'strContent.Append("<tr>")
        'strContent.Append("<td style='width:150px' align='left' valign='top'>")
        'strContent.Append("Celular:</td>")
        'strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        'strContent.Append(ds_Lista.Tables(2).Rows(0).Item("TelCelular"))
        'strContent.Append("</td>")
        'strContent.Append("</tr>")

        'strContent.Append("<tr><td colspan='2'><br /></td></tr>")

        'strContent.Append("<tr>")
        'strContent.Append("<td style='width:150px' align='left' valign='top'>")
        'strContent.Append("Teléfono de oficina:</td>")
        'strContent.Append("<td style='width:250px ; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>")
        'strContent.Append(ds_Lista.Tables(2).Rows(0).Item("TelOficina"))
        'strContent.Append("</td>")
        'strContent.Append("</tr>")

        'strContent.Append("<tr><td colspan='2'><br /></td></tr>")

        'strContent.Append("</table>")

        'strContent.Append("</td>")
        'strContent.Append("</tr>")

        '------------------
        strContent.Append("</table>")

        strContent.Append("</td>")
        strContent.Append("</tr>")
        strContent.Append("</table>")

        Return strContent

    End Function

    ''' <summary>
    ''' Carga el combo con la lista de Diagnosticos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDiagnostico()

        Dim obj_BL_Diagnosticos As New bl_Diagnosticos
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Diagnosticos.FUN_LIS_Diagnostico("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlDiagnostico, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Indicaciones de Enfermeria disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDestino()
        Dim obj_BL_IndicacionEnfermeria As New bl_IndicacionEnfermeria
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_IndicacionEnfermeria.FUN_LIS_IndicacionEnfermeria("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlDestino, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Procedimientos Realizados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboProcedimiento()

        Dim obj_BL_ProcedimientosRealizados As New bl_ProcedimientosRealizados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_ProcedimientosRealizados.FUN_LIS_ProcedimientoRealizado("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlProcedimiento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Procedimientos Realizados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/06/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboCategoria()

        Dim obj_BL_Categoria As New bl_CategoriaAtencion
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Categoria.FUN_LIS_CategoriaAtencion("", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlCategoria, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Procedimientos Realizados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     06/07/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTaller(ByVal str_CodigoAlumno As String)

        Dim obj_BL_AsignacionTalleresBimestrales As New bl_AsignacionTalleresBimestrales
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AsignacionTalleresBimestrales.FUN_LIS_TalleresBimestralesPorAlumno(str_CodigoAlumno, Me.Master.Obtener_CodigoPeriodoEscolar, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        If str_CodigoAlumno = 0 Then
            limpiarCombos(ddlProcTaller)
        Else
            Controles.llenarCombo(ddlProcTaller, ds_Lista, "CodigoNombreGrupo", "NombreGrupo", False, True)
            With ddlProcTaller
                .Items.Insert(0, New ListItem("--Seleccione--"))
                .Items(0).Value = "0"
            End With
        End If

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Procedimientos Realizados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     06/07/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboCurso(ByVal int_CodigoGrado As Integer)

        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar

        Dim obj_BL_AsignacionCursos As New bl_AsignacionCursos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AsignacionCursos.FUN_LIS_AsignacionCursosMidTermReport(int_CodigoGrado, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlProcCurso, ds_Lista, "CodigoCurso", "DescCompuesta", False, True)
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Medicamentos en Kardex Realizados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboMedicamentoKardex()

        Dim obj_BL_Kardex As New bl_Kardex
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Kardex.FUN_LIS_Kardex(ddlSede.SelectedValue, "", int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        ViewState("DatosMedicamentos") = ds_Lista.Tables(0)
        Controles.llenarCombo(ddlMedicamento, ds_Lista, "CodigoMedicamento", "NombreCompleto", False, True)
    End Sub

    Private Sub cargarViewStateKardex()

        Dim obj_BL_Kardex As New bl_Kardex
        Dim int_CodigoSede As Integer = ddlSede.SelectedValue  ' 1
        Dim str_Descripcion As String = ""
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Kardex.FUN_LIS_Kardex(int_CodigoSede, str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        ViewState("DatosMedicamentos") = ds_Lista.Tables(0)

    End Sub

    ''' <summary>
    ''' Se listaran las fichas de atención médica que coincidan con los parametros de busqueda ingresados
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listarFichas()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GVListaFichas.DataSource = ds_Lista.Tables(0)
        GVListaFichas.DataBind()

        SortGridView(ViewState("SortExpression"), ViewState("Direccion"))

        If hfTotalRegs.Value > 0 Then
            ImagenSorting(ViewState("SortExpression"))
        End If

    End Sub

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

        Dim int_CodigoTipoPaciente As Integer = ddlBuscarTipoPaciente.SelectedValue
        Dim str_Nombre As String = tbBuscarNombre.Text.Trim
        Dim str_ApellidoPaterno As String = tbBuscarApellidoPaterno.Text.Trim
        Dim str_ApellidoMaterno As String = tbBuscarApellidoMaterno.Text.Trim
        Dim int_AlumnoNivel As Integer = IIf(ddlBuscarNivel.SelectedValue.ToString.Length = 0, 0, ddlBuscarNivel.SelectedValue)
        Dim int_AlumnoSubNivel As Integer = IIf(ddlBuscarSubNivel.SelectedValue.ToString.Length = 0, 0, ddlBuscarSubNivel.SelectedValue)
        Dim int_AlumnoGrado As Integer = IIf(ddlBuscarGrado.SelectedValue.ToString.Length = 0, 0, ddlBuscarGrado.SelectedValue)
        Dim int_AlumnoAula As Integer = IIf(ddlBuscarAula.SelectedValue.ToString.Length = 0, 0, ddlBuscarAula.SelectedValue)
        Dim str_FamiliarNombre As String = tbBuscarFamiliarNombre.Text.Trim
        Dim str_FamiliarApellidoPaterno As String = tbBuscarFamiliarApellidoPaterno.Text.Trim
        Dim str_FamiliarApellidoMaterno As String = tbBuscarFamiliarApellidoMaterno.Text.Trim
        Dim int_FamiliarAlumnoNivel As Integer = IIf(ddlBuscarFamiliarNivel.SelectedValue.ToString.Length = 0, 0, ddlBuscarFamiliarNivel.SelectedValue)
        Dim int_FamiliarAlumnoSubNivel As Integer = IIf(ddlBuscarFamiliarSubNivel.SelectedValue.ToString.Length = 0, 0, ddlBuscarFamiliarSubNivel.SelectedValue)
        Dim int_FamiliarAlumnoGrado As Integer = IIf(ddlBuscarFamiliarGrado.SelectedValue.ToString.Length = 0, 0, ddlBuscarFamiliarGrado.SelectedValue)
        Dim int_FamiliarAlumnoAula As Integer = IIf(ddlBuscarFamiliarAula.SelectedValue.ToString.Length = 0, 0, ddlBuscarFamiliarAula.SelectedValue)
        Dim dt_FechaRangoInicial As Date = tbBuscarFechaAtencionInicial.Text.Trim
        Dim dt_FechaRangoFinal As Date = tbBuscarFechaAtencionFinal.Text.Trim
        Dim int_Sede As Integer = ddlBuscarSede.SelectedValue
        Dim int_Estado As Integer = 1
        Dim int_EstadoRegistro As Integer = rbEstadosRegistros.SelectedValue
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_FichaAtencion As New bl_FichaAtencion
            ds_Lista = obj_BL_FichaAtencion.FUN_LIS_FichaAtencion(int_CodigoTipoPaciente, str_Nombre, str_ApellidoPaterno, str_ApellidoMaterno, _
            int_AlumnoNivel, int_AlumnoSubNivel, int_AlumnoGrado, int_AlumnoAula, str_FamiliarNombre, str_FamiliarApellidoPaterno, str_FamiliarApellidoMaterno, _
            int_FamiliarAlumnoNivel, int_FamiliarAlumnoSubNivel, int_FamiliarAlumnoGrado, int_FamiliarAlumnoAula, dt_FechaRangoInicial, dt_FechaRangoFinal, _
            int_Sede, int_Estado, int_EstadoRegistro, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_FichaAtencion As New bl_FichaAtencion
                ds_Lista = obj_BL_FichaAtencion.FUN_LIS_FichaAtencion(int_CodigoTipoPaciente, str_Nombre, str_ApellidoPaterno, str_ApellidoMaterno, _
                int_AlumnoNivel, int_AlumnoSubNivel, int_AlumnoGrado, int_AlumnoAula, str_FamiliarNombre, str_FamiliarApellidoPaterno, str_FamiliarApellidoMaterno, _
                int_FamiliarAlumnoNivel, int_FamiliarAlumnoSubNivel, int_FamiliarAlumnoGrado, int_FamiliarAlumnoAula, dt_FechaRangoInicial, dt_FechaRangoFinal, _
                int_Sede, int_Estado, int_EstadoRegistro, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Activara ciertos campos del formulario, esto sucede despues de haber realizado la busqueda del tipo de paciente
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ActivarCampos()

        TabContainer2.Enabled = True
        btnFichaBuscar.Enabled = True
        btnGrabar.Enabled = True
        btnFichaCancelar.Enabled = True
        btnAgregarFichaTemporal.Enabled = True

        btnAgregarFichaTemporal.ImageUrl = "~/App_Themes/Imagenes/btnGrabarFichaTemporal_1.png"
        btnGrabar.ImageUrl = "~/App_Themes/Imagenes/btnGrabarV2_1.png"

        'Datos Iniciales
        imgTimepicker1.Visible = False
        TimeSelector1.Visible = True
        btnBuscarResponsable.ImageUrl = "~/App_Themes/Imagenes/btnBuscarPersona_1.png"

        'Detalle
        btnAgregarDetalleDiagnostico.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
        btnAgregarDetalleProcedimiento.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
        btnAgregarDetalleMedicamento.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"

        'Datos Finales
        imgTimepicker2.Visible = False
        TimeSelector2.Visible = True
        btnBuscarAcompananteSalida.ImageUrl = "~/App_Themes/Imagenes/btnBuscarPersona_1.png"

    End Sub

    ''' <summary>
    ''' Desactivara ciertos campos del formulario, esto sucede cuando se quiere crear un nuevo registro o se desea editar alguno ya existente
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub DesactivarCampos()

        TabContainer2.Enabled = False
        btnFichaBuscar.Enabled = True
        btnGrabar.Enabled = False
        btnFichaCancelar.Enabled = True
        btnAgregarFichaTemporal.Enabled = False

        btnAgregarFichaTemporal.ImageUrl = "~/App_Themes/Imagenes/btnGrabarFichaTemporal_0.png"
        btnGrabar.ImageUrl = "~/App_Themes/Imagenes/btnGrabarV2_0.png"

        'Datos Iniciales
        ddlSede.SelectedValue = 0
        imgTimepicker1.Visible = True
        TimeSelector1.Visible = False
        btnBuscarResponsable.ImageUrl = "~/App_Themes/Imagenes/btnBuscarPersonaV2_0.png"

        'Detalle
        btnAgregarDetalleDiagnostico.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_0.png"
        btnAgregarDetalleProcedimiento.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_0.png"
        btnAgregarDetalleMedicamento.ImageUrl = "~/App_Themes/Imagenes/btnAgregarRegistroDetalle_0.png"

        'Datos Finales
        ddlDestino.SelectedValue = 0
        imgTimepicker2.Visible = True
        TimeSelector2.Visible = False
        btnBuscarAcompananteSalida.ImageUrl = "~/App_Themes/Imagenes/btnBuscarPersonaV2_0.png"

    End Sub

    ''' <summary>
    ''' Se limpian los detalles temporales, y se cambia a la pestaña de busqueda de fichas de atención
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CancelarFicha()

        miTab1.Enabled = True
        miTab2.Enabled = False

        ViewState.Remove("ListaDiagnosticos")
        ViewState.Remove("ListaMedicamentos")
        ViewState.Remove("ListaProcedimientos")

        lbTab2.Text = "Inserción"
        TabContainer1.ActiveTabIndex = 0

    End Sub

    ''' <summary>
    ''' Se setean y limpian los campos de la ficha de atención a sus valores por defecto
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub LimpiarCamposFicha()

        'Datos Iniciales
        lblVerSede.Visible = False
        lblVerFechaAtencion.Visible = False
        lblVerHoraIngreso.Visible = False
        lblVerResponsable.Visible = False
        lblVerTipoProcAtencion.Visible = False
        lblVerNombreprocedencia.Visible = False

        
        'Detalle
        ddlCategoria.Visible = True
        cmbTipoAtencion.Visible = True '' agregado por salcedo vila gaylussac 
        ''fecha de modificacion 06/03/2013


        lblVerCategoria.Visible = False
        lblTipoAtencion.Visible = False

        lblVerSintomas.Visible = False
        lblVerControl.Visible = False
        lblVerObservaciones.Visible = False

        rbTipoProcAtencion.SelectedValue = 1
        rbTipoProcAtencion_SelectedIndexChanged()

        'Datos Finales
        lblVerDestino.Visible = False
        lblVerHoraSalida.Visible = False
        lblVerAcompañante.Visible = False

        'Datos Generales
        hidenCodigoFichaAtencion.Value = 0
        'hidenCodigoPaciente.Value = 0
        hidenCodigoPersona.Value = 0
        hidenCodigoTipoPaciente.Value = 0
        'hidenCodigoTipoSangre.Value = 0
        hidenCodigoSedePaciente.Value = 0

        lbNombrePaciente.Text = ""
        lbEdadPaciente.Text = ""
        lbTipoPaciente.Text = ""

        lblCantidadAtencionAlumno.Text = ""

        lbNSnGS.Text = ""
        'lbTipoSangre.Text = ""
        
        'Datos iniciales
        ddlSede.SelectedValue = 0
        tbFechaAtencion.Text = ""
        imgTimepicker1.Visible = True
        TimeSelector1.Visible = False
        hidenCodigoPersonalEnvia.Value = 0
        hidenCodigoTipoPersonaEnvia.Value = 0
        'imgFotoPaciente.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web").ToString() & "noPhoto.gif"
        ddlProcCurso.SelectedValue = 0
        ddlProcTaller.SelectedValue = 0
        'limpiarCombos(ddlProcCurso)
        'limpiarCombos(ddlProcTaller)
        tbProcOtro.Text = ""
        tbResponsable.Text = ""

        'Detalle atencion
        ddlCategoria.SelectedValue = 0
        cmbTipoAtencion.SelectedValue = 1 '' agregado por salcedo vila gaylussac 
        '' fecha de modificacion 06/03/2013

        tbSintomas.Text = ""
        tbObservaciones.Text = ""
        rbControl.SelectedValue = 0

        'Datos finales
        ddlDestino.SelectedValue = 0
        imgTimepicker2.Visible = True
        TimeSelector2.Visible = False
        hidenCodigoPersonaRecoge.Value = 0
        hidenCodigoTipoPersonaRecoge.Value = 0

    End Sub

    ''' <summary>
    ''' Se validan los datos del formulario y se retornara un valor booleano para saber si se va o no a grabar
    ''' </summary>
    ''' <param name="str_Mensaje">Variable que cogera todos los mensajes de error que se pudiesen obtener de la validación de la ficha de atención</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarFicha(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlSede.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Sede")
            result = False
        End If

        If IsDate(tbFechaAtencion.Text) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha de Atención")
            result = False
        Else
            If CDate(tbFechaAtencion.Text) > CDate(Today.ToShortDateString) Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 4, "Fecha de Atención")
                result = False
            End If
        End If

        Dim d1 As Date
        Dim d2 As Date

        d1 = TimeSelector1.Date
        d2 = TimeSelector2.Date

        Dim comp As Integer = DateTime.Compare(d1, d2)

        If comp > 0 Then
            'If d1.ToShortTimeString. > d2.ToShortTimeString Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 5, "Hora de Ingreso")
            result = False
        End If

        'Si selecciona el tipo de procedencia
        If hidenCodigoTipoPaciente.Value = 1 Then
     
            If rbTipoProcAtencion.SelectedValue = 1 Then 'Curso
                If ddlProcCurso.SelectedValue = 0 Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Procedencia")
                    result = False
                End If
            ElseIf rbTipoProcAtencion.SelectedValue = 2 Then 'Taller
                If ddlProcTaller.SelectedValue = 0 Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Procedencia")
                    result = False
                End If
            ElseIf rbTipoProcAtencion.SelectedValue = 3 Then 'recreo

            ElseIf rbTipoProcAtencion.SelectedValue = 4 Then ' otro
                If Validacion.ValidarCamposIngreso(tbProcOtro) = False Then
                    str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Procedencia")
                    result = False
                End If

                If tbProcOtro.Text.Trim.Length > 0 Then
                    If Validacion.ValidarCamposIngreso(tbProcOtro) = False Then
                        str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Procedencia")
                        result = False
                    End If
                End If
            End If

        End If

        ' si posee sintomas, valido la información
        If tbSintomas.Text.Trim.Length > 0 Then
            If Validacion.ValidarCamposIngreso(tbSintomas) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Sintomas")
                result = False
            End If
        End If

        ' si posee observacion, valido la información
        If tbObservaciones.Text.Trim.Length > 0 Then
            If Validacion.ValidarCamposIngreso(tbObservaciones) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Observaciones")
                result = False
            End If
        End If

        If ddlCategoria.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Categoria")
            result = False
        End If

        If cmbTipoAtencion.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo atencion ")
            result = False
        End If


        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Se graba la ficha de atención 
    ''' </summary>   
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarFicha()

        Dim str_mensajeError As String = ""
        Dim obj_BE_FichaAtencion As New be_FichaAtencion
        Dim obj_BL_FichaAtencion As New bl_FichaAtencion
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim BoolGrabar As Integer = hidenCodigoFichaAtencion.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        'Datos Generales
        obj_BE_FichaAtencion.CodigoPersonaPaciente = hidenCodigoPersona.Value
        obj_BE_FichaAtencion.CodigoTipoPersonaPaciente = hidenCodigoTipoPaciente.Value

        'Datos Iniciales
        obj_BE_FichaAtencion.CodigoSede = ddlSede.SelectedValue
        obj_BE_FichaAtencion.FechaAtencion = tbFechaAtencion.Text & " 00:00:00.000" '& Now.ToShortTimeString
        obj_BE_FichaAtencion.HoraIngreso = "01/01/9999 " & TimeSelector1.Date.ToShortTimeString
        obj_BE_FichaAtencion.CodigoPersonaEnvia = IIf(hidenCodigoPersonaEnvia.Value.ToString.Length = 0, 0, hidenCodigoPersonaEnvia.Value)
        obj_BE_FichaAtencion.CodigoTipoProcedencia = IIf(hidenCodigoTipoPaciente.Value = 1, rbTipoProcAtencion.SelectedValue, 0)
        obj_BE_FichaAtencion.CodigoCurso = IIf(hidenCodigoTipoPaciente.Value = 1, ddlProcCurso.SelectedValue, 0)
        obj_BE_FichaAtencion.CodigoNombreGrupo = IIf(hidenCodigoTipoPaciente.Value = 1, ddlProcTaller.SelectedValue, 0)
        obj_BE_FichaAtencion.DescripcionOtros = IIf(hidenCodigoTipoPaciente.Value = 1, tbProcOtro.Text, "")

        'Detalle Atencion


        obj_BE_FichaAtencion.CodigoCategoriaAtencion = ddlCategoria.SelectedValue

        ''modificacion  tipo de atencion  : agregado por salcedo vila gaylussac 
        ''fecha de modificacion 06/03/2013
        ''agregar  el campo de tipo de atencion a la ficha  de atencion 
        obj_BE_FichaAtencion.CodigoTipoAtencion = cmbTipoAtencion.SelectedValue



        obj_BE_FichaAtencion.SintomasDescripcion = IIf(tbSintomas.Text.Trim.Length = 0, Nothing, tbSintomas.Text.Trim)
        obj_BE_FichaAtencion.DescansarEnfermeria = rbControl.SelectedValue
        obj_BE_FichaAtencion.Observaciones = IIf(tbObservaciones.Text.Trim.Length = 0, Nothing, tbObservaciones.Text.Trim)


        'Datos Finales
        obj_BE_FichaAtencion.CodigoIndicacionMedica = ddlDestino.SelectedValue
        obj_BE_FichaAtencion.HoraSalida = "01/01/9999 " & TimeSelector2.Date.ToShortTimeString
        obj_BE_FichaAtencion.CodigoPersonaRecoje = IIf(hidenCodigoPersonaRecoge.Value.ToString.Length = 0, 0, hidenCodigoPersonaRecoge.Value)
        obj_BE_FichaAtencion.CodigoTipoPersonaRecoge = IIf(hidenCodigoTipoPersonaRecoge.Value.ToString.Length = 0, 0, hidenCodigoTipoPersonaRecoge.Value)

        obj_BE_FichaAtencion.UsuarioRegistro = Me.Master.Obtener_CodigoUsuarioLogueado

        'Detalle
        Dim objDS_Detalle As New DataSet

        'Detalle Diagnostico
        Dim objDT_Diagnostico As DataTable
        objDT_Diagnostico = New DataTable("ListaDiagnosticos")
        objDT_Diagnostico = Datos.agregarColumna(objDT_Diagnostico, "Codigo", "String")
        objDT_Diagnostico = Datos.agregarColumna(objDT_Diagnostico, "Descripcion", "String")
        Dim dr_Diagnostico As DataRow
        For Each drv As GridViewRow In GVListaDiagnosticos.Rows
            dr_Diagnostico = objDT_Diagnostico.NewRow
            dr_Diagnostico.Item("Codigo") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Diagnostico.Item("Descripcion") = CType(drv.FindControl("Label2"), Label).Text
            objDT_Diagnostico.Rows.Add(dr_Diagnostico)
        Next

        'Detalle Procedimiento Realizado
        Dim objDT_ProcedimientoRealizado As New DataTable
        objDT_ProcedimientoRealizado = New DataTable("ListaProcedimientos")
        objDT_ProcedimientoRealizado = Datos.agregarColumna(objDT_ProcedimientoRealizado, "Codigo", "String")
        objDT_ProcedimientoRealizado = Datos.agregarColumna(objDT_ProcedimientoRealizado, "Descripcion", "String")
        Dim dr_Procedimiento As DataRow
        For Each drv As GridViewRow In GVListaProcedimientos.Rows
            dr_Procedimiento = objDT_ProcedimientoRealizado.NewRow
            dr_Procedimiento.Item("Codigo") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Procedimiento.Item("Descripcion") = CType(drv.FindControl("Label2"), Label).Text
            objDT_ProcedimientoRealizado.Rows.Add(dr_Procedimiento)
        Next

        'Detalle Medicamento
        Dim objDT_Medicamento As New DataTable
        objDT_Medicamento = New DataTable("ListaMedicamentos")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "Codigo", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "Descripcion", "String")
        objDT_Medicamento = Datos.agregarColumna(objDT_Medicamento, "Cantidad", "decimal")
        Dim dr_Medicamento As DataRow

        For Each drv As GridViewRow In GVListaMedicamentos.Rows
            dr_Medicamento = objDT_Medicamento.NewRow
            dr_Medicamento.Item("Codigo") = CType(drv.FindControl("btnEliminar"), ImageButton).CommandArgument.ToString()
            dr_Medicamento.Item("Descripcion") = CType(drv.FindControl("Label2"), Label).Text
            dr_Medicamento.Item("Cantidad") = CType(drv.FindControl("Label3"), Label).Text
            objDT_Medicamento.Rows.Add(dr_Medicamento)
        Next

        'Agrego las DataTable a mi DataSet
        objDS_Detalle.Tables.Add(objDT_Diagnostico)
        objDS_Detalle.Tables.Add(objDT_ProcedimientoRealizado)
        objDS_Detalle.Tables.Add(objDT_Medicamento)

        If BoolGrabar = 0 Then ' Nuevo
            usp_valor = obj_BL_FichaAtencion.FUN_INS_FichaAtencion(obj_BE_FichaAtencion, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Else ' Update
            obj_BE_FichaAtencion.CodigoFichaAtencion = CInt(BoolGrabar)
            usp_valor = obj_BL_FichaAtencion.FUN_UPD_FichaAtencion(obj_BE_FichaAtencion, objDS_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        End If

        If usp_valor > 0 Then
            Dim paramEstadoFicha As Integer = 0
            'Si la ficha se grabo, actualizo el campo "Estado : FAM_Completado"
            paramEstadoFicha = obj_BL_FichaAtencion.FUN_UPD_EstadoFichaAtencion(usp_valor, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)

            If ViewState("FichaTemporal") = True And paramEstadoFicha = 0 Then
                AgregarFichaTemporal(usp_valor, lbNombrePaciente.Text, lbTipoPaciente.Text, obj_BE_FichaAtencion.FechaAtencion.ToShortDateString, obj_BE_FichaAtencion.HoraIngreso.ToShortTimeString)
            End If

            If paramEstadoFicha = 1 Then
                eliminarFichasTemporal(usp_valor)
            End If

            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnFichaCancelar_Click()
            LimpiarCamposFicha()
            ViewState.Remove("ListaDiagnosticos")
            ViewState.Remove("ListaMedicamentos")
            ViewState.Remove("ListaProcedimientos")

            listarFichas()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Se carga toda la información de la ficha de atención que fue consultada
    ''' </summary> 
    ''' <param name="int_CodigoFichaAtencion">Codigo de la ficha de atención, de la cual se quiere obtener toda la información.</param>  
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtenerFicha(ByVal int_CodigoFichaAtencion As Integer)



        Dim str_mensajeError As String = ""
        Dim obj_BL_FichaAtencion As New bl_FichaAtencion
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_FichaAtencion.FUN_GET_FichaAtencion(int_CodigoFichaAtencion, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)

        Dim int_CodigoAlumno As Integer
        Dim int_CodigoGrado As Integer


        int_CodigoAlumno = ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString
        int_CodigoGrado = ds_Lista.Tables(0).Rows(0).Item("CodigoGrado").ToString

        cargarComboTaller(int_CodigoAlumno)
        cargarComboCurso(int_CodigoGrado)
       


        LimpiarCamposFicha()
        'Datos Generales

        hidenCodigoFichaAtencion.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoFichaAtencion").ToString
        hidenCodigoPersona.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaPaciente").ToString
        hidenCodigoTipoPaciente.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoTipoPersonaPaciente").ToString

        lbNombrePaciente.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString
        lbTipoPaciente.Text = ds_Lista.Tables(0).Rows(0).Item("DescTipoPersonaPaciente").ToString



        Dim codPersona As Integer = 0
        codPersona = ds_Lista.Tables(0).Rows(0)("CodigoPersonaPaciente").ToString()
        cargarInformacionAtencionesAlumno(codPersona)



        If hidenCodigoTipoPaciente.Value = 1 Then
            spanNSnGA.Visible = True
            lbNSnGS.Text = ds_Lista.Tables(0).Rows(0).Item("NSnGA").ToString
        Else
            spanNSnGA.Visible = False
            lbNSnGS.Text = ""
        End If



        'Datos Iniciales
        ddlSede.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoSede").ToString
        tbFechaAtencion.Text = ds_Lista.Tables(0).Rows(0).Item("FechaAtencion").ToString

        Dim dt_HoraAtencion As DateTime = Convert.ToDateTime(ds_Lista.Tables(0).Rows(0).Item("HoraAtencion").ToString)
        Dim int_Hour1 As Integer = dt_HoraAtencion.Hour
        Dim int_Minute1 As Integer = dt_HoraAtencion.Minute
        Dim int_AmPm1 As Integer
        If dt_HoraAtencion.ToString.IndexOf("a.m.") > 0 Then
            int_AmPm1 = 0
        ElseIf dt_HoraAtencion.ToString.IndexOf("p.m.") > 0 Then
            int_AmPm1 = 1
        End If

        TimeSelector1.SetTime(int_Hour1, int_Minute1, int_AmPm1)
        'TimeSelector1.Date = Convert.ToDateTime(ds_Lista.Tables(0).Rows(0).Item("HoraAtencion").ToString)

        hidenCodigoPersonaEnvia.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaEnvia").ToString
        tbResponsable.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompletoPersonaEnvia").ToString
        If hidenCodigoTipoPaciente.Value = 1 Then
            rbTipoProcAtencion.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoTipoProcedencia").ToString
            rbTipoProcAtencion_SelectedIndexChanged()

            If rbTipoProcAtencion.SelectedValue = 1 Then

                ddlProcTaller.Visible = False
                ddlProcCurso.Visible = True
                tbProcOtro.Visible = False
                ddlProcCurso.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoCurso").ToString
            ElseIf rbTipoProcAtencion.SelectedValue = 2 Then
                'pnlTipoProcAtencionCurso.Visible = False
                'pnlTipoProcAtencionTaller.Visible = True
                'pnlTipoProcAtencionOtro.Visible = False
                'spanProcAtencion.Visible = True
                ddlProcCurso.Visible = False
                tbProcOtro.Visible = False
                ddlProcTaller.Visible = True

                ddlProcTaller.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoNombreGrupo").ToString
            ElseIf rbTipoProcAtencion.SelectedValue = 3 Then
                'pnlTipoProcAtencionCurso.Visible = False
                'pnlTipoProcAtencionTaller.Visible = False
                'pnlTipoProcAtencionOtro.Visible = False
                'spanProcAtencion.Visible = False
                ddlProcTaller.Visible = False
                ddlProcCurso.Visible = False
                tbProcOtro.Visible = False
                spanProcAtencion.Visible = True
            ElseIf rbTipoProcAtencion.SelectedValue = 4 Then

                'pnlTipoProcAtencionOtro.Visible = True
                'spanProcAtencion.Visible = True
                ddlProcTaller.Visible = False
                ddlProcCurso.Visible = False
                tbProcOtro.Visible = True
                tbProcOtro.Text = ds_Lista.Tables(0).Rows(0).Item("DescripcionOtros").ToString
            End If

            rbTipoProcAtencion_SelectedIndexChanged()
        End If

        'Detalle Atencion
        ddlCategoria.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoCategoria").ToString
        tbSintomas.Text = ds_Lista.Tables(0).Rows(0).Item("Sintomas").ToString
        tbObservaciones.Text = ds_Lista.Tables(0).Rows(0).Item("Observaciones").ToString

        ''modificacion por saledo vila gaylussac 
        ''fecha de modificacion 06/03/2013
        ''agregar el combo de tipo de atencion 
        ''------------------------------------------------------------------------------------------------------------------------
        ' If CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoTipoAtencion").ToString) <> 0 Then
        cmbTipoAtencion.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoTipoAtencion").ToString
        ' End If

        ''------------------------------------------------------------------------------------------------------------------------


        If ds_Lista.Tables(0).Rows(0).Item("DescansoEnfermeria") Then
            rbControl.SelectedValue = 1
        Else
            rbControl.SelectedValue = 0
        End If

        'Datos Finales
        ddlDestino.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoIndicacionMedica").ToString

        Dim dt_HoraSalida As DateTime = Convert.ToDateTime(ds_Lista.Tables(0).Rows(0).Item("HoraSalida").ToString)
        Dim int_Hour2 As Integer = dt_HoraSalida.Hour
        Dim int_Minute2 As Integer = dt_HoraSalida.Minute
        Dim int_AmPm2 As Integer
        If dt_HoraSalida.ToString.IndexOf("a.m.") > 0 Then
            int_AmPm2 = 0
        ElseIf dt_HoraSalida.ToString.IndexOf("p.m.") > 0 Then
            int_AmPm2 = 1
        End If

        TimeSelector2.SetTime(int_Hour2, int_Minute2, int_AmPm2)
        'TimeSelector2.Date = ds_Lista.Tables(0).Rows(0).Item("HoraSalida").ToString

        hidenCodigoPersonaRecoge.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaRecoge").ToString
        hidenCodigoTipoPersonaRecoge.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoTipoPersonaRecoge").ToString

        tbAcompanante.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompletoPersonaRecoge").ToString
        lblTipoAcompanante.Text = ds_Lista.Tables(0).Rows(0).Item("DescTipoPersonaRecoge").ToString

        'Detalle Diagnostico
        Dim objDT_Diagnostico As DataTable
        objDT_Diagnostico = New DataTable("ListaDiagnosticos")
        objDT_Diagnostico = ds_Lista.Tables(1).Clone

        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
            For Each dr As DataRow In ds_Lista.Tables(1).Rows
                objDT_Diagnostico.ImportRow(dr)
            Next
            ViewState("ListaDiagnosticos") = objDT_Diagnostico
            GVListaDiagnosticos.DataSource = objDT_Diagnostico
            GVListaDiagnosticos.DataBind()
        Else
            GVListaDiagnosticos.DataBind()
        End If

        'Detalle Procedimiento
        Dim objDT_ProcedimientoRealizado As New DataTable
        objDT_ProcedimientoRealizado = New DataTable("ListaProcedimientos")
        objDT_ProcedimientoRealizado = ds_Lista.Tables(2).Clone

        If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then
            For Each dr As DataRow In ds_Lista.Tables(2).Rows
                objDT_ProcedimientoRealizado.ImportRow(dr)
            Next
            ViewState("ListaProcedimientos") = objDT_ProcedimientoRealizado
            GVListaProcedimientos.DataSource = objDT_ProcedimientoRealizado
            GVListaProcedimientos.DataBind()
        Else
            GVListaProcedimientos.DataBind()
        End If

        'Detalle Medicamentos
        Dim objDT_Medicamento As New DataTable
        objDT_Medicamento = New DataTable("ListaMedicamentos")
        objDT_Medicamento = ds_Lista.Tables(3).Clone

        If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then
            For Each dr As DataRow In ds_Lista.Tables(3).Rows
                objDT_Medicamento.ImportRow(dr)
            Next
            ViewState("ListaMedicamentos") = objDT_Medicamento
            GVListaMedicamentos.DataSource = objDT_Medicamento
            GVListaMedicamentos.DataBind()
        Else
            GVListaMedicamentos.DataBind()
        End If

        cargarComboMedicamentoKardex()

        'Mostrar Datos
        VerRegistro("Actualización", 2)

    End Sub

    ''' <summary>
    ''' Se carga toda la información, como solo lectura, de la ficha de atención que fue consultada
    ''' </summary> 
    ''' <param name="int_CodigoFichaAtencion">Codigo de la ficha de atención, de la cual se quiere obtener toda la información.</param>  
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerFicha(ByVal int_CodigoFichaAtencion As Integer)

        Dim str_mensajeError As String = ""
        Dim obj_BL_FichaAtencion As New bl_FichaAtencion
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_FichaAtencion.FUN_GET_FichaAtencion(int_CodigoFichaAtencion, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)

        Dim codPersona As Integer = 0
        codPersona = ds_Lista.Tables(0).Rows(0)("CodigoPersonaPaciente").ToString()
        cargarInformacionAtencionesAlumno(codPersona)


        'Datos Generales
        hidenCodigoFichaAtencion.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoFichaAtencion").ToString
        hidenCodigoPersona.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoPersonaPaciente").ToString
        hidenCodigoTipoPaciente.Value = ds_Lista.Tables(0).Rows(0).Item("CodigoTipoPersonaPaciente").ToString

        lbNombrePaciente.Text = ds_Lista.Tables(0).Rows(0).Item("NombreCompleto").ToString
        lbTipoPaciente.Text = ds_Lista.Tables(0).Rows(0).Item("DescTipoPersonaPaciente").ToString

        lbNSnGS.Text = ds_Lista.Tables(0).Rows(0).Item("NSnGA").ToString

        'Datos iniciales
        lblVerSede.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescSede").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescSede"))
        lblVerFechaAtencion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("FechaAtencionStr").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("FechaAtencionStr"))
        lblVerHoraIngreso.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("HoraAtencionStr").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("HoraAtencionStr"))
        lblVerResponsable.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("NombreCompletoPersonaEnvia").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("NombreCompletoPersonaEnvia"))
        lblVerTipoProcAtencion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("TipoProcedencia").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("TipoProcedencia"))
        lblVerNombreprocedencia.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("NombreProcedencia").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("NombreProcedencia"))

        'Detalle
        lblVerCategoria.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("Categoria").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("Categoria"))


        '' agregar el tipo atencion en el label 
        '' agregado por salcedo vila gaylussac 
        lblTipoAtencion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("nombreTipoAtencion").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("nombreTipoAtencion"))
        'nombreTipoAtencion

        lblVerSintomas.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("Sintomas").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("Sintomas"))
        If ds_Lista.Tables(0).Rows(0).Item("DescansoEnfermeria") Then
            lblVerControl.Text = "Si"
        Else
            lblVerControl.Text = "No"
        End If
        lblVerObservaciones.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("Observaciones").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("Observaciones"))

        'Datos finales

        lblVerDestino.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescIndicacionMedica").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescIndicacionMedica"))
        lblVerHoraSalida.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("HoraSalidaStr").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("HoraSalidaStr"))
        lblVerAcompañante.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("NombreCompletoPersonaRecoge").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("NombreCompletoPersonaRecoge"))
        lblTipoAcompanante.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("DescTipoPersonaRecoge").ToString.Length = 0, "-", ds_Lista.Tables(0).Rows(0).Item("DescTipoPersonaRecoge"))

        'Detalle Diagnostico
        Dim objDT_Diagnostico As DataTable
        objDT_Diagnostico = New DataTable("ListaDiagnosticos")
        objDT_Diagnostico = ds_Lista.Tables(1).Clone

        If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
            For Each dr As DataRow In ds_Lista.Tables(1).Rows
                objDT_Diagnostico.ImportRow(dr)
            Next
            ViewState("ListaDiagnosticos") = objDT_Diagnostico
            GVListaDiagnosticos.DataSource = objDT_Diagnostico
            GVListaDiagnosticos.DataBind()
        End If

        'Detalle Procedimiento
        Dim objDT_ProcedimientoRealizado As New DataTable
        objDT_ProcedimientoRealizado = New DataTable("ListaProcedimientos")
        objDT_ProcedimientoRealizado = ds_Lista.Tables(2).Clone

        If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then
            For Each dr As DataRow In ds_Lista.Tables(2).Rows
                objDT_ProcedimientoRealizado.ImportRow(dr)
            Next
            ViewState("ListaProcedimientos") = objDT_ProcedimientoRealizado
            GVListaProcedimientos.DataSource = objDT_ProcedimientoRealizado
            GVListaProcedimientos.DataBind()
        End If

        'Detalle Medicamentos
        Dim objDT_Medicamento As New DataTable
        objDT_Medicamento = New DataTable("ListaMedicamentos")
        objDT_Medicamento = ds_Lista.Tables(3).Clone

        If ds_Lista.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then
            For Each dr As DataRow In ds_Lista.Tables(3).Rows

                objDT_Medicamento.ImportRow(dr)
            Next
            ViewState("ListaMedicamentos") = objDT_Medicamento
            GVListaMedicamentos.DataSource = objDT_Medicamento
            GVListaMedicamentos.DataBind()
        End If
        VerRegistro("Consulta", 3)

    End Sub

    ''' <summary>
    ''' Setea el estado de los campos y opciones de la ficha de atención
    ''' </summary> 
    ''' <param name="str_Modo">Tipo de visualizacion que tendra los datos del formulario, puede ser :  Nuevo, Actualizar y Ver</param>    
    ''' <param name="int_Modo">Texto que mostrara la cabecera de la pestaña</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String, ByVal int_Modo As Integer)

        miTab1.Enabled = False
        miTab2.Enabled = True

        If int_Modo = 1 Then ' 1 : Nuevo / 2 : Actualizar / 3 : Ver

            btnAgregarDetalleDiagnostico.Style.Remove("display")
            btnAgregarDetalleProcedimiento.Style.Remove("display")
            btnAgregarDetalleMedicamento.Style.Remove("display")

            'Datos Iniciales
            ddlSede.Visible = True
            tbFechaAtencion.Visible = True
            Image1.Visible = True
            imgTimepicker1.Visible = True
            TimeSelector1.Visible = True
            tbResponsable.Visible = True

            'Detalle
            tbSintomas.Visible = True
            rbControl.Visible = True
            tbObservaciones.Visible = True

            GVListaMedicamentos.DataBind()
            GVListaProcedimientos.DataBind()
            GVListaDiagnosticos.DataBind()

            'Datos Finales
            ddlDestino.Visible = True
            imgTimepicker2.Visible = True
            TimeSelector2.Visible = True
            tbAcompanante.Visible = True

            DesactivarCampos()
            btnFichaBuscar.ImageUrl = "~/App_Themes/Imagenes/btnBuscarPersonaV2_1.png"

            spanNSnGA.Visible = False
            lbNSnGS.Text = ""
            btnBuscarAcompananteSalida.Visible = True

            EstadoCamposAlumno(False)

        ElseIf int_Modo = 2 Then

            btnAgregarDetalleDiagnostico.Style.Remove("display")
            btnAgregarDetalleProcedimiento.Style.Remove("display")
            btnAgregarDetalleMedicamento.Style.Remove("display")

            'Datos Iniciales
            ddlSede.Visible = True
            tbFechaAtencion.Visible = True
            Image1.Visible = True
            imgTimepicker1.Visible = True
            TimeSelector1.Visible = True
            tbResponsable.Visible = True
            'Detalle
            tbSintomas.Visible = True
            rbControl.Visible = True
            tbObservaciones.Visible = True
            'Datos Finales
            ddlDestino.Visible = True
            imgTimepicker2.Visible = True
            TimeSelector2.Visible = True
            tbAcompanante.Visible = True

            ActivarCampos()
            btnFichaBuscar.Enabled = False
            btnFichaBuscar.ImageUrl = "~/App_Themes/Imagenes/btnBuscarPersonaV2_0.png"

            btnGrabar.Enabled = True
            btnGrabar.ImageUrl = "~/App_Themes/Imagenes/btnGrabarV2_1.png"

            btnAgregarFichaTemporal.Enabled = True
            btnBuscarAcompananteSalida.Visible = True

            If hidenCodigoTipoPaciente.Value = 1 Then

                spanNSnGA.Visible = True
                EstadoCamposAlumno(True)

            Else

                spanNSnGA.Visible = False
                lbNSnGS.Text = ""
                EstadoCamposAlumno(False)

            End If

        ElseIf int_Modo = 3 Then

            btnAgregarDetalleDiagnostico.Style.Add("display", "none")
            btnAgregarDetalleProcedimiento.Style.Add("display", "none")
            btnAgregarDetalleMedicamento.Style.Add("display", "none")

            ActivarCampos()
            btnFichaBuscar.Enabled = False
            btnFichaBuscar.ImageUrl = "~/App_Themes/Imagenes/btnBuscarPersonaV2_0.png"

            btnGrabar.Enabled = False
            btnGrabar.ImageUrl = "~/App_Themes/Imagenes/btnGrabarV2_0.png"

            btnAgregarFichaTemporal.Enabled = False

            'Datos Iniciales
            ddlSede.Visible = False
            tbFechaAtencion.Visible = False
            Image1.Visible = False
            imgTimepicker1.Visible = False
            TimeSelector1.Visible = False
            tbResponsable.Visible = False
            rbTipoProcAtencion.Visible = False

            'Detalle
            tbSintomas.Visible = False
            rbControl.Visible = False
            tbObservaciones.Visible = False
            'Datos Finales
            ddlDestino.Visible = False
            imgTimepicker2.Visible = False
            TimeSelector2.Visible = False
            tbAcompanante.Visible = False

            'Datos Iniciales
            lblVerSede.Visible = True
            lblVerFechaAtencion.Visible = True
            lblVerHoraIngreso.Visible = True
            lblVerResponsable.Visible = True
            lblVerTipoProcAtencion.Visible = True
            lblVerNombreprocedencia.Visible = True
            'Detalle
            ddlCategoria.Visible = False
            lblVerCategoria.Visible = True

            lblTipoAtencion.Visible = True
            cmbTipoAtencion.Visible = False


            lblVerSintomas.Visible = True
            lblVerControl.Visible = True
            lblVerObservaciones.Visible = True
            'Datos Finales
            lblVerDestino.Visible = True
            lblVerHoraSalida.Visible = True
            lblVerAcompañante.Visible = True

            If hidenCodigoTipoPaciente.Value = 1 Then

                spanNSnGA.Visible = True
                lblTipoAcompanante.Visible = True
                EstadoCamposAlumno(True)

            Else
                spanTipoProcAtencion.Visible = False
                rbTipoProcAtencion.Visible = False

                spanNSnGA.Visible = False
                lbNSnGS.Visible = False
                lblVerResponsable.Visible = False
                lblVerAcompañante.Visible = False
                lblTipoAcompanante.Visible = False
                EstadoCamposAlumno(False)

            End If
            rbTipoProcAtencion.Visible = False
            tbResponsable.Visible = False
            tbProcOtro.Visible = False
            ddlProcCurso.Visible = False
            ddlProcTaller.Visible = False
            btnBuscarResponsable.Visible = False
            btnBuscarAcompananteSalida.Visible = False

        End If

        lbTab2.Text = str_Modo
        TabContainer1.ActiveTabIndex = 1
        TabContainer2.ActiveTabIndex = 0

    End Sub

    ''' <summary>
    ''' Setea la visualizacion de opciones especiales(solo para alumnos)
    ''' </summary> 
    ''' <param name="bool">Estado de las opciones especiales(True o False)</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EstadoCamposAlumno(ByVal bool As Boolean)

        btnVerDatosRelevantes.Visible = bool
        btnVerSeguro.Visible = bool
        btnVerContactos.Visible = bool
        btnVerFichaMedica.Visible = bool

        spanResponsable.Visible = bool
        tbResponsable.Visible = bool
        btnBuscarResponsable.Visible = bool
        pnlAcompañante.Visible = bool

        spanTipoProcAtencion.Visible = bool
        spanProcAtencion.Visible = bool
        rbTipoProcAtencion.Visible = bool
        pnlTipoProcAtencionCurso.Visible = bool
        pnlTipoProcAtencionTaller.Visible = bool
        pnlTipoProcAtencionOtro.Visible = bool

    End Sub

#End Region

#Region "Eventos del Gridview Busqueda"

    Protected Sub GVListaFichas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Or e.CommandName = "Activar" Or e.CommandName = "Imprimir" Or e.CommandName = "Ver" Then

                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then

                    int_CodigoAccion = 6
                    ViewState("VerFicha") = False

                    obtenerFicha(codigo)

                ElseIf e.CommandName = "Eliminar" And row.Cells(8).Text <> "Inactivo" Then

                    int_CodigoAccion = 3
                    cambiarEstado(codigo, "Eliminar")

                ElseIf e.CommandName = "Activar" And row.Cells(8).Text <> "Activo" Then

                    int_CodigoAccion = 2
                    cambiarEstado(codigo, "Activar")

                ElseIf e.CommandName = "Ver" Then

                    int_CodigoAccion = 5
                    ViewState("VerFicha") = True
                    VerFicha(codigo)

                ElseIf e.CommandName = "Imprimir" Then

                    int_CodigoAccion = 4
                    Dim obj_BL_FichaAtencion As New bl_FichaAtencion
                    Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
                    Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
                    Dim ds_Lista As DataSet = obj_BL_FichaAtencion.FUN_GET_FichaAtencion(codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)




                    Dim reporte_html As String = ""
                    reporte_html = Exportacion.ExportarReporteFichaAtencion_Html(ds_Lista, "")
                    Session("Exportaciones_RepFichaAtencionHtml") = reporte_html
                    ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresionFichaAtencion_html();</script>", False)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub GVListaFichas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Try
            'Dim btnVer As ImageButton = e.Row.FindControl("btnVer")
            Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            Dim btnActivar As ImageButton = e.Row.FindControl("btnActivar")
            Dim btnImprimir As ImageButton = e.Row.FindControl("btnImprimir")
            Dim btnVer As ImageButton = e.Row.FindControl("btnVisualizar")
            Dim imgFichaPendiente As Image = e.Row.FindControl("imgFichaPendiente")

            If e.Row.RowType = DataControlRowType.Pager Then
                Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
                _TotalPags.Text = GVListaFichas.PageCount.ToString

                Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
                _Registros.Text = InformacionPager(GVListaFichas, e.Row, Me)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.DataItem("CodigoEstadoFicha") = 0 Then 'PENDIENTE
                    imgFichaPendiente.Visible = True
                    imgFichaPendiente.ToolTip = "Registro Pendiente"
                Else
                    imgFichaPendiente.Visible = False
                End If

                If e.Row.DataItem("Estado") = "Activo" Then

                    btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")

                    If e.Row.DataItem("EsModificable") = "Si" Then
                       
                    Else
                        btnActualizar.Visible = False
                        btnEliminar.Visible = False
                    End If

                    btnActivar.Visible = False

                Else
                    btnActivar.Attributes.Add("OnClick", "return confirm_activar();")
                    btnActualizar.Visible = False
                    btnEliminar.Visible = False

                    e.Row.ForeColor = Drawing.Color.DarkRed
                End If

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub GVListaFichas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GVListaFichas.PageIndex = e.NewPageIndex
            End If

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaFichas_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
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
            EnvioEmailError(112, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaFichas_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Pager Then
                CrearBotonesPager(GVListaFichas, e.Row, Me)
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GVListaFichas.PageCount Then
                Me.GVListaFichas.PageIndex = _NumPag - 1
            Else
                Me.GVListaFichas.PageIndex = 0
            End If

            Me.GVListaFichas.SelectedIndex = -1

            'listarFichas()
            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos del Gridview Busqueda"

    'Protected Sub IraPag(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim _IraPag As TextBox = DirectCast(sender, TextBox)
    '    Dim _NumPag As Integer

    '    If Integer.TryParse(_IraPag.Text.Trim, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GVListaFichas.PageCount Then
    '        Me.GVListaFichas.PageIndex = _NumPag - 1
    '    Else
    '        Me.GVListaFichas.PageIndex = 0
    '    End If

    '    Me.GVListaFichas.SelectedIndex = -1
    '    listarFichas()

    'End Sub

    ''' <summary>
    ''' Lista las fichas de atención ordenadas por un campo especifico
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)

        Dim usp_mensaje As String = ""

        If validarBusquedaFicha(usp_mensaje) Then

            Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(2)

            hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

            Dim dv As New Data.DataView(ds_Lista.Tables(0))
            dv.Sort = sortExpression + " " + direction

            GVListaFichas.DataSource = dv
            GVListaFichas.DataBind()

        Else

            MostrarAlertas(usp_mensaje)

        End If

    End Sub

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     21/01/2011
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
    ''' Fecha de Creación:     21/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(GVListaFichas.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(GVListaFichas.HeaderRow.FindControl("btnSorting_NombreCompleto"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GVListaFichas.HeaderRow.FindControl("btnSorting_DescTipoPaciente"), ImageButton)
        Dim _btnSorting_d3 As ImageButton = CType(GVListaFichas.HeaderRow.FindControl("btnSorting_DescSede"), ImageButton)
        Dim _btnSorting_d4 As ImageButton = CType(GVListaFichas.HeaderRow.FindControl("btnSorting_FechaHoraAtencionDt"), ImageButton)

        If _btnSorting.ID = _btnSorting_d1.ID Then

            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d2.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d3.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"

        Else

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"

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
    ''' Fecha de Creación:     21/01/2011
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
    ''' Fecha de Creación:     21/01/2011
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
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     19/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(1, 2, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Recibe mensajes y los deriva a otro metodo que los visualizara cno animación de JQuery
    ''' </summary>
    ''' <param name="str_alertas">Mensaje que se quiere visualizar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
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
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

#End Region


End Class