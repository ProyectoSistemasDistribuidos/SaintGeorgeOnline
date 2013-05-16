Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_DataAccess.ModuloBancoLibros
Imports SaintGeorgeOnline_BusinessLogic.ModuloBancoLibros
Imports System.Data
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities

Partial Class Modulo_BancoLibros_DistribucionLibrosAnual
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Master.MostrarTitulo("Asignación Anual de Libros")
        If Not Page.IsPostBack Then

            cargarCombos()
            ViewState("SortExpression") = "Titulo"
            ViewState("Direccion") = "ASC"
            ddl_Periodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
            MostrarAsignacion()
        End If
    End Sub
    Protected Sub btnCerraAgregarLibro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        mpe_AsignarLibros.Show()
        mpe_AgregarLibro.Hide()
    End Sub

    Protected Sub btnAgregarLibro_click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        limpiarFiltros()
        listar()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            listar()
            mpe_AgregarLibro.Show()
            mpe_AsignarLibros.Show()
        Catch ex As Exception
            'EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        limpiarFiltros()
    End Sub

    Protected Sub btn_AgregarAsignacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim obj_BtnAgregarAsignacion As New System.Web.UI.WebControls.ImageButton
        Dim obj_lblTempCodigoGrado As New System.Web.UI.WebControls.Label
        Dim obj_lblTempGrado As New System.Web.UI.WebControls.Label
        Dim obj_lblTempCodigoAula As New System.Web.UI.WebControls.Label
        Dim obj_lblTempAula As New System.Web.UI.WebControls.Label

        Dim int_IdFila As Integer = 0
        Dim str_NombreGrado As String = ""
        Dim str_NombreSeccion As String = ""
        Dim int_CodigoGrado As Integer = 0
        Dim int_CodigoAula As Integer = 0

        obj_BtnAgregarAsignacion = sender
        int_IdFila = obj_BtnAgregarAsignacion.AlternateText
        int_IdFila = int_IdFila - 1

        obj_lblTempCodigoGrado = dgv_AsignacionAnual.Rows(int_IdFila).Cells(0).FindControl("lblCodigoGrado")
        obj_lblTempCodigoAula = dgv_AsignacionAnual.Rows(int_IdFila).Cells(1).FindControl("lblCodigoAula")
        obj_lblTempGrado = dgv_AsignacionAnual.Rows(int_IdFila).Cells(2).FindControl("lblGrado")
        obj_lblTempAula = dgv_AsignacionAnual.Rows(int_IdFila).Cells(3).FindControl("lblAula")

        str_NombreGrado = obj_lblTempGrado.Text
        str_NombreSeccion = obj_lblTempAula.Text
        int_CodigoGrado = obj_lblTempCodigoGrado.Text
        int_CodigoAula = obj_lblTempCodigoAula.Text

        'str_NombreAnio
        lbl_NombreGrado.Text = str_NombreGrado
        lbl_NombreSeccion.Text = str_NombreSeccion
        hd_codigoAnioAcademico.Value = ddl_Periodo.SelectedValue()
        hd_codigoGrado.Value = int_CodigoGrado
        hd_codigoAula.Value = int_CodigoAula

        listarDetalleDistribucionLibrosAnual(hd_codigoAnioAcademico.Value, int_CodigoGrado, int_CodigoAula)

        mpe_AsignarLibros.Show()
    End Sub
    Private Sub listarDetalleDistribucionLibrosAnual(ByVal int_codigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer)
        Dim obj_BL_AsignacionAnualLibro As New bl_DistribucionAnualLibros
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim dt_Plantilla As New DataTable
        Dim ds_Lista As DataSet

        ds_Lista = obj_BL_AsignacionAnualLibro.FUN_LIS_DetalleDistribucionLibrosAnual(hd_codigoAnioAcademico.Value, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        dt_Plantilla = ds_Lista.Tables(0)

        ViewState("DetalleDistribucionLibrosAnual") = dt_Plantilla
        ViewState("cantMaxIdFila") = ds_Lista.Tables(1).Rows(0).Item("cantMaxIdFila")
        ViewState("ListaBimestres") = ds_Lista.Tables(2)

        dgv_ConsolidadoLibros.DataSource = dt_Plantilla
        dgv_ConsolidadoLibros.DataBind()

        lbl_Anio.Text = ddl_Periodo.SelectedItem.ToString
        lblFecInicio1.Text = ds_Lista.Tables(2).Rows(0).Item("FechaInicio")
        lblFecFin1.Text = ds_Lista.Tables(2).Rows(0).Item("FechaFin")
        lblFecInicio2.Text = ds_Lista.Tables(2).Rows(1).Item("FechaInicio")
        lblFecFin2.Text = ds_Lista.Tables(2).Rows(1).Item("FechaFin")
        lblFecInicio3.Text = ds_Lista.Tables(2).Rows(2).Item("FechaInicio")
        lblFecFin3.Text = ds_Lista.Tables(2).Rows(2).Item("FechaFin")
        lblFecInicio4.Text = ds_Lista.Tables(2).Rows(3).Item("FechaInicio")
        lblFecFin4.Text = ds_Lista.Tables(2).Rows(3).Item("FechaFin")

        'hfTotalRegsGVTodos.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)


    End Sub

    Protected Sub btnGrabarDetalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validar(usp_mensaje) Then

                GrabarIngresoDetalle()

            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
                mpe_AsignarLibros.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

        
    End Sub

#End Region

#Region "Metodos"


    ''' <summary>
    ''' Limpia los filtros de búsqueda del formulario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks> 
    Private Sub limpiarFiltros()

        'lbl_Anio.Text = ""
        tbTitulo.Text = ""
        tbTitulo.Focus()
        mpe_AgregarLibro.Show()
        mpe_AsignarLibros.Show()

    End Sub

    ''' <summary>
    ''' Valida el nombre de acción a registrar.
    ''' </summary>
    ''' <returns>Indicador sobre la validación</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        Juan Vento
    ''' Fecha de modificación: 15/02/2011
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""
        Dim boolFechaInicio As Boolean = True
        Dim boolFechaFin As Boolean = True
        Dim boolDiferenciaFecha As Boolean = True
        Dim boolFechaInicioMenorBimestre As Boolean = True
        Dim boolLibroRepetidoXFecha As Boolean = True

        Dim obj_BL_AsignacionAnualLibro As New bl_DistribucionAnualLibros
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim cont As Integer = 0
        Dim cont_1 As Integer = 0

        Dim dv As DataView
        Dim dtBimestre As DataTable
        Dim dtRepetidos As DataTable

        Dim dt_listaConsolidadoLibros As New DataTable 'se lista todo de la grilla actual

        dt_listaConsolidadoLibros = ObtenerTablaConsolidadoLibros()

        dtRepetidos = ViewState("LibrosRepetidos")
        Dim fechaInicial As Date
        Dim fechaFinal As Date
        Dim IdFila As Integer = 1

        For Each drv As GridViewRow In dgv_ConsolidadoLibros.Rows
            Dim tbFechaInicio As TextBox = CType(drv.FindControl("tbFechaInicio"), TextBox)
            Dim tbFechaFin As TextBox = CType(drv.FindControl("tbFechaFin"), TextBox)
            Dim TipoDato As Label = CType(drv.FindControl("lblTipoDato"), Label)
            Dim CodigoAsignacionLibro As Label = CType(drv.FindControl("lblCodigoAsignacionLibro"), Label)
            Dim CodigoLibro As Label = CType(drv.FindControl("lblCodigoLibro"), Label)
            Dim CodigoGrado As Label = CType(drv.FindControl("lblCodigoGrado"), Label)
            Dim CodigoAnioAcademico As Label = CType(drv.FindControl("lblCodigoAnioAcademico"), Label)
            Dim CodigoAula As Label = CType(drv.FindControl("lblCodigoAula"), Label)
            Dim Titulo As Label = CType(drv.FindControl("lblTitulo"), Label)

            If IsDate(tbFechaInicio.Text.Trim) = False Then
                boolFechaInicio = False
            End If
            If IsDate(tbFechaFin.Text.Trim) = False Then
                boolFechaFin = False
            End If
            If IsDate(tbFechaInicio.Text.Trim) And IsDate(tbFechaFin.Text.Trim) Then
                If (CType(tbFechaInicio.Text, Date) > CType(tbFechaFin.Text, Date)) Then
                    boolDiferenciaFecha = False
                End If
                'dtBimestre = obj_BL_AsignacionAnualLibro.FUN_LIS_ValidaFechaBimestre(tbFechaInicio.Text.Trim, tbFechaFin.Text.Trim, hd_codigoGrado.Value, hd_codigoAnioAcademico.Value, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion).Tables(0)
                dtBimestre = obj_BL_AsignacionAnualLibro.FUN_LIS_ValidaFechaBimestre(tbFechaInicio.Text.Trim, tbFechaFin.Text.Trim, hd_codigoGrado.Value, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion).Tables(0)

                If dtBimestre.Rows(0).Item("CodigoBimestre").ToString.Length <= 0 Then
                    boolFechaInicioMenorBimestre = False
                End If
            End If


            Dim dr As DataRow
            dr = dt_listaConsolidadoLibros.NewRow
            dr.Item("IdFila") = IdFila
            dr.Item("TipoDato") = TipoDato.Text
            dr.Item("CodigoAsignacionLibro") = CInt(CodigoAsignacionLibro.Text)
            dr.Item("CodigoLibro") = CInt(CodigoLibro.Text)
            dr.Item("CodigoGrado") = CInt(CodigoGrado.Text)
            dr.Item("CodigoAnioAcademico") = CInt(CodigoAnioAcademico.Text)
            dr.Item("CodigoAula") = CInt(CodigoAula.Text)
            dr.Item("FechaInicio") = tbFechaInicio.Text
            dr.Item("FechaFin") = tbFechaFin.Text
            dr.Item("Titulo") = Titulo.Text
            dt_listaConsolidadoLibros.Rows.Add(dr)

            IdFila = IdFila + 1
        Next

        dv = dt_listaConsolidadoLibros.DefaultView
        If ViewState("LibrosRepetidos") IsNot Nothing Then
            For Each drv1 As DataRow In dtRepetidos.Rows
                dv.RowFilter = "1=1 and CodigoLibro=" & drv1.Item("CodigoLibro").ToString 'dtRepetidos.Rows(cont_1).Item("CodigoLibro")
                While cont_1 <= dv.Count - 1
                    If existefecha(dv.Item(cont_1).Item("IdFila").ToString, dv.Item(cont_1).Item("CodigoLibro").ToString, dv.Item(cont_1).Item("FechaInicio").ToString, dv.Item(cont_1).Item("FechaFin").ToString, dv) Then
                        boolLibroRepetidoXFecha = False
                        Exit While
                    End If
                    cont_1 = cont_1 + 1
                End While
                cont_1 = 0
            Next

        End If

        If boolLibroRepetidoXFecha = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha ")
            result = False
        End If

        If boolFechaInicio = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha Inicio")
            result = False
        End If

        If boolFechaFin = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha Fin")
            result = False
        End If

        If boolDiferenciaFecha = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 7, "Fecha Inicio")
            result = False
        End If

        If boolFechaInicioMenorBimestre = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 34, "Fecha ")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Function existefecha(ByVal idFila As String, ByVal codigoLibro As String, ByVal str_fechaInicial As String, ByVal str_fechaFinal As String, ByVal dv As DataView) As Boolean
        Dim bool_Existe As Boolean = False
        Dim int_IdFila As Integer = 0
        Dim fechaInicial As Date
        Dim fechaFinal As Date
        'Dim dt As DataTable
        Dim int_cont As Integer = 0

        dv.RowFilter = "1=1 and CodigoLibro=" & codigoLibro.ToString 'dtRepetidos.Rows(cont_1).Item("CodigoLibro")

        'dt = dv.Table
        int_IdFila = idFila
        fechaInicial = CDate(str_fechaInicial)
        fechaFinal = CDate(str_fechaFinal)

        If dv.Count > 1 Then
            While int_cont <= dv.Count - 1
                If dv.Item(int_cont).Item("IdFila") = idFila Then
                Else
                    If (CDate(dv.Item(int_cont).Item("FechaInicio")) <= fechaInicial And fechaInicial <= CDate(dv.Item(int_cont).Item("FechaFin"))) Or _
                       (CDate(dv.Item(int_cont).Item("FechaInicio")) <= fechaFinal And fechaFinal <= CDate(dv.Item(int_cont).Item("FechaFin"))) Then
                        bool_Existe = True
                        Exit While
                    End If
                End If
                int_cont = int_cont + 1
            End While
        End If
        Return bool_Existe
    End Function

    Private Sub cargarCombos()
        cargarComboAniosAcademicos()
        cargarComboGrado()
        cargarComboIdioma()
    End Sub

    ''' <summary>
    ''' Construye la estructura de la tabla temporal de permisos donde se almacenara la información registrada para su manipulación.
    ''' </summary>
    ''' <returns>estructura de tabla temporal de Consolidado de Libros</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     11/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerTablaConsolidadoLibros() As DataTable
        Dim dt As DataTable = New DataTable("TablaConsolidadoLibros")

        dt = Datos.agregarColumna(dt, "IdFila", "Integer")
        dt = Datos.agregarColumna(dt, "TipoDato", "String")
        dt = Datos.agregarColumna(dt, "CodigoAsignacionLibro", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoLibro", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoGrado", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoAnioAcademico", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoAula", "Integer")
        dt = Datos.agregarColumna(dt, "FechaInicio", "String")
        dt = Datos.agregarColumna(dt, "FechaFin", "String")
        dt = Datos.agregarColumna(dt, "Titulo", "String")

        Return dt
    End Function

    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddl_Periodo, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Grados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddl_Grado, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Grados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboIdioma()

        Dim obj_BL_Libros As New bl_Libros
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Libros.FUN_LIS_Idiomas("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        'Controles.llenarCombo(ddl_Idioma, ds_Lista, "Codigo", "Descripcion", True, False)

        ddl_Idioma.DataSource = ds_Lista
        ddl_Idioma.DataValueField = "Codigo"
        ddl_Idioma.DataTextField = "Descripcion"
        ddl_Idioma.DataBind()
    End Sub

    Private Sub MostrarAsignacion()
        Dim obj_BL_AsignacionAnualLibro As New bl_DistribucionAnualLibros
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim dt_Plantilla As New DataTable
        Dim dt_detalle As New DataTable

        Dim ds_Lista As DataSet = obj_BL_AsignacionAnualLibro.FUN_LIS_AsignacionAnualLibros(ddl_Periodo.SelectedValue, ddl_Grado.SelectedValue, ddl_Idioma.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        dt_Plantilla = ds_Lista.Tables(0)
        dt_detalle = ds_Lista.Tables(1)
        ViewState("listaDetalle") = dt_detalle

        dgv_AsignacionAnual.DataSource = dt_Plantilla
        dgv_AsignacionAnual.DataBind()
    End Sub
    Protected Sub ddl_Periodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            MostrarAsignacion()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddl_Grado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            MostrarAsignacion()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub ddl_Idioma_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            MostrarAsignacion()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Envia mensaje de error de todas las acciones del formulario.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(2, 42, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    ''' <summary>
    ''' Lista los datos de Clínicas     
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        hfTotalRegsGVTodos.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GVListaTodos.DataSource = ds_Lista.Tables(0)
        GVListaTodos.DataBind()

        If ds_Lista.Tables(0).Rows.Count = 0 Then
            'btnExportar.Enabled = False
            'rbTipoReporte.Enabled = False
        Else
            'btnExportar.Enabled = True
            'rbTipoReporte.Enabled = True
        End If

    End Sub



    Private Sub AgregarDetalleDistribucionLibrosAnual(ByVal int_CodigoLibro As Integer, ByVal titulo As String)
        mpe_AgregarLibro.Hide()
        Dim dt As DataTable
        Dim dt_librosRepetidos As DataTable

        Dim int_cantMaxIdFila As Integer = 0

        If ViewState("DetalleDistribucionLibrosAnual") Is Nothing Then
            dt = New DataTable("DetalleDistribucionLibrosAnual")
            dt = Datos.agregarColumna(dt, "IdFila", "Integer")
            dt = Datos.agregarColumna(dt, "TipoDato", "String")
            dt = Datos.agregarColumna(dt, "CodigoAsignacionLibro", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoLibro", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoGrado", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoAnioAcademico", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoAula", "Integer")
            dt = Datos.agregarColumna(dt, "FechaInicio", "string")
            dt = Datos.agregarColumna(dt, "FechaFin", "string")
            dt = Datos.agregarColumna(dt, "Titulo", "String")

        Else
            dt = ViewState("DetalleDistribucionLibrosAnual")
            int_cantMaxIdFila = ViewState("cantMaxIdFila") + 1
            ViewState("cantMaxIdFila") = int_cantMaxIdFila
        End If

        Dim dv As DataView
        If ViewState("LibrosRepetidos") Is Nothing Then
            dt_librosRepetidos = New DataTable("LibrosRepetidos")
            dt_librosRepetidos = Datos.agregarColumna(dt_librosRepetidos, "CodigoLibro", "Integer")
        Else
            dt_librosRepetidos = ViewState("LibrosRepetidos")

        End If

        dv = dt_librosRepetidos.DefaultView

        If dgv_ConsolidadoLibros.Rows.Count > 0 Then
            If existeCodigoLibro(int_CodigoLibro, dt_librosRepetidos) Then
            Else
                Dim dr_Rep As DataRow
                dr_Rep = dt_librosRepetidos.NewRow
                dr_Rep.Item("CodigoLibro") = int_CodigoLibro
                dt_librosRepetidos.Rows.Add(dr_Rep)
                ViewState.Remove("LibrosRepetidos")
                ViewState("LibrosRepetidos") = dt_librosRepetidos
            End If
        End If

        Dim dr As DataRow
        dr = dt.NewRow
        dr.Item("IdFila") = int_cantMaxIdFila
        dr.Item("TipoDato") = "T"
        dr.Item("CodigoAsignacionLibro") = 0
        dr.Item("CodigoLibro") = int_CodigoLibro
        dr.Item("CodigoGrado") = hd_codigoGrado.Value()
        dr.Item("CodigoAnioAcademico") = hd_codigoAnioAcademico.Value()
        dr.Item("CodigoAula") = hd_codigoAula.Value
        dr.Item("FechaInicio") = ""
        dr.Item("FechaFin") = ""
        dr.Item("Titulo") = titulo
        dt.Rows.Add(dr)

        ViewState("DetalleDistribucionLibrosAnual") = dt
        dgv_ConsolidadoLibros.DataSource = dt
        dgv_ConsolidadoLibros.DataBind()

        mpe_AsignarLibros.Show()
    End Sub

    Private Sub GrabarIngresoDetalle()
        Dim obj_BE_DistribucionAnualLibros As New be_DistribucionAnualLibros
        Dim obj_BL_DistribucionAnualLibros As New bl_DistribucionAnualLibros
        Dim usp_mensaje As String = ""
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim cont_Registrados As Integer = 0
        Dim usp_valorDetalleEl As Integer = 0
        Dim dt_listaDetalle As New DataTable
        Dim dt_listaEliminar As New DataTable
        Dim int_cantidadD As Integer = 0
        Dim usp_valorDetalle As Integer
        Dim dt_listaDetalleLibro As New DataTable 'se lista todo de la grilla actual
        dt_listaDetalleLibro = ViewState("DetalleDistribucionLibrosAnual")
        dt_listaEliminar = ViewState("ListaDatosEliminados")
        If ViewState("DetalleDistribucionLibrosAnual") IsNot Nothing Then

            For Each drv As GridViewRow In dgv_ConsolidadoLibros.Rows
                'codigo = drv.Item("codigoDetalleCompromisoPago")
                obj_BE_DistribucionAnualLibros.CodigoAsignacionLibro = CType(drv.FindControl("lblCodigoAsignacionLibro"), Label).Text 'drv.Item("CodigoAsignacionLibro")
                obj_BE_DistribucionAnualLibros.CodigoLibro = CType(drv.FindControl("lblCodigoLibro"), Label).Text 'drv.Item("CodigoLibro")
                obj_BE_DistribucionAnualLibros.CodigoAnioAcademico = CType(drv.FindControl("lblCodigoAnioAcademico"), Label).Text 'drv.Item("CodigoAnioAcademico")
                obj_BE_DistribucionAnualLibros.CodigoGrado = CType(drv.FindControl("lblCodigoGrado"), Label).Text ' drv.Item("CodigoGrado")
                obj_BE_DistribucionAnualLibros.CodigoAula = CType(drv.FindControl("lblCodigoAula"), Label).Text ' drv.Item("CodigoAula")
                obj_BE_DistribucionAnualLibros.FechaInicio = CType(drv.FindControl("tbFechaInicio"), TextBox).Text 'drv.Item("FechaInicio")
                obj_BE_DistribucionAnualLibros.FechaFin = CType(drv.FindControl("tbFechaFin"), TextBox).Text 'drv.Item("FechaFin")

                If CType(drv.FindControl("lblTipoDato"), Label).Text = "T" Then
                    usp_valorDetalle = obj_BL_DistribucionAnualLibros.FUN_INS_DetalleDistribucionLibrosAnual(obj_BE_DistribucionAnualLibros, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)
                End If
            Next
        End If

        If ViewState("ListaDatosEliminados") IsNot Nothing Then
            For Each drv1 As DataRow In dt_listaEliminar.Rows
                usp_valorDetalleEl = obj_BL_DistribucionAnualLibros.FUN_DEL_DistribucionAnualLibros(drv1.Item("CodigoAsignacionLibro"), usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)
            Next
        End If

        If usp_valorDetalle > 0 Or usp_valorDetalleEl > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            'btnCancelar_Click()
            'limpiarCampos()
            listar()
            listarDetalleDistribucionLibrosAnual(ddl_Periodo.SelectedValue, hd_codigoGrado.Value, hd_codigoAula.Value)
            MostrarAsignacion()
            mpe_AsignarLibros.Hide()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <param name="int_Modo">Tipo de accion 1 es de la BD 2 es del formulario</param>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     17/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet
        'Dim int_Periodo As Integer = CInt(ddlBuscarAnio.SelectedValue)
        Dim str_Titulo As String = tbTitulo.Text.Trim()
        Dim int_Idioma As Integer = CInt(rdBuscarIdioma.SelectedValue)
        'Dim str_ISBN As String = tbBuscarISBN.Text.Trim()
        Dim int_Grado As Integer = hd_codigoGrado.Value
        'Dim int_TipoReporte As Integer = CInt(rbTipoReporte.SelectedValue)
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Libros As New bl_Libros
            ds_Lista = obj_BL_Libros.FUN_LIS_Libros(ddl_Periodo.SelectedValue, str_Titulo, int_Idioma, "", int_Grado, 0, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_Libros As New bl_Libros
                ds_Lista = obj_BL_Libros.FUN_LIS_Libros(ddl_Periodo.SelectedValue, str_Titulo, int_Idioma, "", int_Grado, 0, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    Private Function existeCodigoLibro(ByVal int_CodigoLibro As Integer, ByVal dt As DataTable) As Boolean
        Dim bool_Existe As Boolean = False
        Dim cont As Integer = 0
        For Each drR As DataRow In dt.Rows
            If drR.Item("CodigoLibro") = int_CodigoLibro Then
                If cont = 0 Then
                    bool_Existe = True
                    Exit For
                End If
                cont = cont + 1
            End If

        Next
        Return bool_Existe
    End Function
#End Region

#Region "Eventos del dgv_ConsolidadoLibros"

    Protected Sub dgv_ConsolidadoLibros_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Eliminar" Then

                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 3
                    eliminarDetalle(codigo, CType(row.FindControl("lblCodigoAsignacionLibro"), Label).Text, CType(row.FindControl("lblTipoDato"), Label).Text, CType(row.FindControl("lblCodigoAnioAcademico"), Label).Text, CType(row.FindControl("lblCodigoGrado"), Label).Text, CType(row.FindControl("lblCodigoAula"), Label).Text)
                End If

            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub dgv_ConsolidadoLibros_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Dim tbFechaInicio As TextBox = e.Row.FindControl("tbFechaInicio")
            Dim tbFechaFin As TextBox = e.Row.FindControl("tbFechaFin")
            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")

            If e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.DataItem("IdFila") > 0 Then
                    tbFechaInicio.Text = e.Row.DataItem("FechaInicio")
                    tbFechaFin.Text = e.Row.DataItem("FechaFin")
                    btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                End If

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Llama al metodo de Eliminar o Activar según la acción seleccionada.
    ''' </summary>
    ''' <param name="int_Codigo">codigo de Clínicas</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub eliminarDetalle(ByVal int_Codigo As Integer, ByVal int_CodigoAsignacionLibro As Integer, ByVal tipoDato As String, ByVal int_anio As Integer, ByVal int_grado As Integer, ByVal int_aula As Integer)

        Dim obj_BL_DistribucionAnualLibros As New bl_DistribucionAnualLibros
        Dim usp_mensaje As String = ""
        'Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet
        Dim dt As DataTable
        Dim dt_listaDatos As DataTable
        dt_listaDatos = ViewState("DetalleDistribucionLibrosAnual")

        dt = New DataTable("ListaDetalleDistribucionLibrosAnual")
        dt = Datos.agregarColumna(dt, "IdFila", "Integer")
        dt = Datos.agregarColumna(dt, "TipoDato", "String")
        dt = Datos.agregarColumna(dt, "CodigoAsignacionLibro", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoLibro", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoGrado", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoAnioAcademico", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoAula", "Integer")
        dt = Datos.agregarColumna(dt, "FechaInicio", "String")
        dt = Datos.agregarColumna(dt, "FechaFin", "String")
        dt = Datos.agregarColumna(dt, "Titulo", "String")

        Dim dt_RegistrosEliminados As DataTable

        If ViewState("ListaDatosEliminados") Is Nothing Then
            dt_RegistrosEliminados = New DataTable("ListaDatosEliminados")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "IdFila", "Integer")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "CodigoAsignacionLibro", "Integer")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "CodigoLibro", "Integer")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "CodigoGrado", "Integer")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "CodigoAnioAcademico", "Integer")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "CodigoAula", "Integer")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "Titulo", "String")

        Else
            dt_RegistrosEliminados = ViewState("ListaDatosEliminados")
        End If

        For Each gvr As DataRow In dt_listaDatos.Rows

            Dim dr As DataRow

            If int_Codigo = gvr.Item("IdFila") Then
                'Solo agrego a mi DataTable de eliminados los registros que existen en la Base de Datos
                If gvr.Item("TipoDato").ToString() = "R" Then
                    Dim drLE As DataRow

                    drLE = dt_RegistrosEliminados.NewRow
                    drLE.Item("CodigoAsignacionLibro") = gvr.Item("CodigoAsignacionLibro")
                    drLE.Item("CodigoLibro") = gvr.Item("CodigoLibro")
                    drLE.Item("CodigoGrado") = gvr.Item("CodigoGrado")
                    drLE.Item("CodigoAnioAcademico") = gvr.Item("CodigoAnioAcademico")
                    drLE.Item("CodigoAula") = gvr.Item("CodigoAula")
                    drLE.Item("Titulo") = gvr.Item("Titulo").ToString()
                    dt_RegistrosEliminados.Rows.Add(drLE)

                End If

                'dr.Delete()
            Else
                dr = dt.NewRow
                dr.Item("IdFila") = gvr.Item("IdFila").ToString()
                dr.Item("TipoDato") = gvr.Item("TipoDato").ToString()
                dr.Item("CodigoAsignacionLibro") = gvr.Item("CodigoAsignacionLibro")
                dr.Item("CodigoLibro") = gvr.Item("CodigoLibro")
                dr.Item("CodigoGrado") = gvr.Item("CodigoGrado")
                dr.Item("CodigoAnioAcademico") = gvr.Item("CodigoAnioAcademico")
                dr.Item("CodigoAula") = gvr.Item("CodigoAula")
                dr.Item("FechaInicio") = gvr.Item("FechaInicio")
                dr.Item("FechaFin") = gvr.Item("FechaFin")
                dr.Item("Titulo") = gvr.Item("Titulo").ToString()
                dt.Rows.Add(dr)
            End If
        Next


        ViewState.Remove("DetalleDistribucionLibrosAnual")
        ViewState("DetalleDistribucionLibrosAnual") = dt

        ViewState.Remove("ListaDatosEliminados")
        ViewState("ListaDatosEliminados") = dt_RegistrosEliminados

        dgv_ConsolidadoLibros.DataSource = dt
        dgv_ConsolidadoLibros.DataBind()

        mpe_AsignarLibros.Show()
        'If tipoDato = "R" Then
        '    usp_valor = obj_BL_DistribucionAnualLibros.FUN_DEL_DistribucionAnualLibros(int_CodigoAsignacionLibro, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)


        'Else
        '    Dim dt As DataTable
        '    dt = ViewState("DetalleDistribucionLibrosAnual")
        '    For Each dr As DataRow In dt.Rows

        '        If int_Codigo = dr.Item("IdFila") Then
        '            dr.Delete()
        '            Exit For
        '        End If

        '    Next
        '    dt.AcceptChanges()

        '    ViewState.Remove("DetalleDistribucionLibrosAnual")
        '    ViewState("DetalleDistribucionLibrosAnual") = dt
        '    usp_valor = 1
        'End If

        'If usp_valor > 0 Then
        '    MostrarSexyAlertBox(usp_mensaje, "Info")
        'Else
        '    MostrarSexyAlertBox(usp_mensaje, "Alert")
        'End If
        'listarDetalleDistribucionLibrosAnual(int_anio, int_grado, int_aula)
        'mpe_AsignarLibros.Show()

    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub GVListaTodos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                AgregarDetalleDistribucionLibrosAnual(codigo, CType(row.FindControl("lblTitulo"), Label).Text)

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaTodos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            'Dim btnVerFoto As HtmlAnchor = e.Row.FindControl("btnLinkVerFoto")

            If e.Row.RowType = DataControlRowType.Pager Then

                Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
                _TotalPags.Text = GVListaTodos.PageCount.ToString

                Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
                _Registros.Text = InformacionPager(GVListaTodos, e.Row, Me)

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                ''SETEO DE PERMISOS DE ACCIONES---------------
                'Master.BloqueoControles(btnEliminar, 1)
                'Master.BloqueoControles(btnActualizar, 1)
                'Master.BloqueoControles(btnActivar, 1)
                ''---------------------------------------------

                'btnVerFoto.Attributes.Add("rel", "sexylightbox")

                'btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaImagenesBancoLibro_Web").ToString() & e.Row.DataItem("RutaPortada")

                e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
                e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaTodos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                CType(sender, GridView).PageIndex = e.NewPageIndex
            End If

            SortGridView(CType(sender, GridView), ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(CType(sender, GridView), ViewState("SortExpression"))
            mpe_AgregarLibro.Show()
            mpe_AsignarLibros.Show()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaTodos_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager(CType(sender, GridView), e.Row, Me)
        End If

    End Sub

    Protected Sub GVListaTodos_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression

            ViewState("SortExpression") = sortExpression

            If GridViewSortDirection = SortDirection.Ascending Then
                GridViewSortDirection = SortDirection.Descending
                SortGridView(CType(sender, GridView), sortExpression, "DESC")
                ViewState("Direccion") = "DESC"
            Else
                GridViewSortDirection = SortDirection.Ascending
                SortGridView(CType(sender, GridView), sortExpression, "ASC")
                ViewState("Direccion") = "ASC"
            End If

            ImagenSorting(CType(sender, GridView), e.SortExpression)
            mpe_AgregarLibro.Show()
            mpe_AsignarLibros.Show()
        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _gridview As GridView = CType(_DropDownList.Parent.Parent.Parent.Parent, GridView)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= _gridview.PageCount Then
                _gridview.PageIndex = _NumPag - 1
            Else
                _gridview.PageIndex = 0
            End If

            _gridview.SelectedIndex = -1
            listar()
            SortGridView(_gridview, ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(_gridview, ViewState("SortExpression"))
            mpe_AgregarLibro.Show()
            mpe_AsignarLibros.Show()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

#Region "Genericos"

    ''' <summary>
    ''' Lista las fichas de atención ordenadas por un campo especifico
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView(ByVal gridView As GridView, ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(2)

        hfTotalRegsGVTodos.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        gridView.DataSource = dv
        gridView.DataBind()
        gridView.Visible = True

    End Sub

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     25/01/2011
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
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal gridView As GridView, ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(gridView.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(gridView.HeaderRow.FindControl("btnSorting_Titulo"), ImageButton)

        If _btnSorting.ID = _btnSorting_d1.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"

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
    ''' Fecha de Creación:     25/01/2011
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
    ''' Fecha de Creación:     25/01/2011
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

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegsGVTodos.Value)

    End Function

#End Region

#End Region

    Protected Sub dgv_AsignacionAnual_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_AsignacionAnual.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lbl_codGrado As Label = e.Row.FindControl("lblCodigoGrado")
                Dim lbl_codAula As Label = e.Row.FindControl("lblCodigoAula")
                Dim blList1 As BulletedList = e.Row.FindControl("bl_Lista1")
                Dim blList2 As BulletedList = e.Row.FindControl("bl_Lista2")
                Dim blList3 As BulletedList = e.Row.FindControl("bl_Lista3")
                Dim blList4 As BulletedList = e.Row.FindControl("bl_Lista4")
                Dim int_codGrado As Integer = 0
                Dim int_codAula As Integer = 0
                Dim dv As DataView
                Dim dt As DataTable
                Dim cont As Integer = 0

                dt = ViewState("listaDetalle")

                dv = dt.DefaultView

                dv.RowFilter = "1=1 and GD_CodigoGrado=" & lbl_codGrado.Text & " and AU_CodigoAula =" & lbl_codAula.Text

               
                While cont <= dv.Count - 1
                    Dim arrStrCodigoBimestre() As String
                    arrStrCodigoBimestre = Split(dv.Item(cont).Item("CodigoBimestre"), ",")
                    Dim cont1 As Integer = 0

                    While cont1 <= arrStrCodigoBimestre.Length - 1

                        If arrStrCodigoBimestre(cont1).Length > 0 Then
                            If arrStrCodigoBimestre(cont1) = 1 Then
                                blList1.Items.Add(dv.Item(cont).Item("LB_Titulo"))
                                blList1.Items(blList1.Items.Count - 1).Attributes.Add("style", "cursor:pointer;background-color:" & dv.Item(cont).Item("Color"))
                            ElseIf arrStrCodigoBimestre(cont1) = 2 Then
                                blList2.Items.Add(dv.Item(cont).Item("LB_Titulo"))
                                blList2.Items(blList2.Items.Count - 1).Attributes.Add("style", "cursor:pointer;background-color:" & dv.Item(cont).Item("Color"))
                            ElseIf arrStrCodigoBimestre(cont1) = 3 Then
                                blList3.Items.Add(dv.Item(cont).Item("LB_Titulo"))
                                blList3.Items(blList3.Items.Count - 1).Attributes.Add("style", "cursor:pointer;background-color:" & dv.Item(cont).Item("Color"))
                            ElseIf arrStrCodigoBimestre(cont1) = 4 Then
                                blList4.Items.Add(dv.Item(cont).Item("LB_Titulo"))
                                blList4.Items(blList4.Items.Count - 1).Attributes.Add("style", "cursor:pointer;background-color:" & dv.Item(cont).Item("Color"))
                            End If
                        End If
                        
                        cont1 = cont1 + 1
                    End While

                    cont = cont + 1

                End While

            End If


        Catch ex As Exception

        End Try
    End Sub
End Class
