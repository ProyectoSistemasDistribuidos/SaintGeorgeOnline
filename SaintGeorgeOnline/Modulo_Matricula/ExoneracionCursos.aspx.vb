Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloCursos
Imports System.Data
Imports System.Data.SqlClient
Imports SaintGeorgeOnline_Utilities
Imports System.IO

Partial Class Modulo_Matricula_ExoneracionCursos
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 2
    Private cod_Opcion As Integer = 63
#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Exoneración de Cursos")

            If Not Page.IsPostBack Then
                cargarCombos()
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
    Protected Sub btn_GrabarExoneracion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
            Dim usp_mensaje As String = ""
            'If Not validar(usp_mensaje) Then
            GrabarFicha()
            'Else
            'MostrarAlertas(usp_mensaje)
            'End If

        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        listar()
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try
    End Sub
    Protected Sub ddlBuscarAnioAcademico_SelectedIndexChanged()
        Try

            cargarComboAulas()



        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlAulas_SelectedIndexChanged()
        Try
            If ddlBuscarAnioAcademico.SelectedValue > 0 Then

                'cargarGridviewAlumnos()
                Dim int_CodigoAula As Integer = ddlAulas.SelectedValue

                hiddenCodigoGrado.Value = 0
                Dim int_CodigoAnioAcademico As Integer = ddlBuscarAnioAcademico.SelectedValue

                For Each dr As DataRow In CType(ViewState("ListaAulas"), DataTable).Rows
                    If int_CodigoAula = dr.Item("CodigoAula") Then
                        hiddenCodigoGrado.Value = dr.Item("CodigoGrado")

                        tbNivel.Text = dr.Item("DescNivelMinisterio")
                        tbGrado.Text = dr.Item("DescGradoMinisterio")
                        tbSeccion.Text = dr.Item("DescAulaMinisterio")
                        Exit For
                    End If
                Next

            End If

            listar()
            cargarComboCursos()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub


#End Region

#Region "Metodos"

    Private Sub limpiarCombo(ByVal ddl As DropDownList)

        Controles.limpiarCombo(ddl, False, True)

    End Sub

    ''' <summary>
    ''' Llena el combo "ddlCurso" con la lista de grados activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboCursos()

        Dim int_CodigoAula As Integer = ddlAulas.SelectedValue

        If int_CodigoAula = 0 Then
            limpiarCombo(ddlCurso_pnlReg)
            Exit Sub
        End If

        Dim int_CodigoAnioAcademico As Integer = ddlBuscarAnioAcademico.SelectedValue
        Dim int_CodigoGrado As Integer = 0
        int_CodigoGrado = hiddenCodigoGrado.Value

        If int_CodigoGrado > 0 Then
            Dim obj_BL_AsignacionCursos As New bl_AsignacionCursos
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
            Dim ds_Lista As DataSet = obj_BL_AsignacionCursos.FUN_LIS_AsignacionCursosMidTermReport(int_CodigoGrado, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            If ds_Lista.Tables(0).Rows.Count > 0 Then
                Controles.llenarCombo(ddlCurso_pnlReg, ds_Lista, "CodigoCurso", "DescCompuesta", False, True)
            Else
                limpiarCombo(ddlCurso_pnlReg)
            End If
        Else
            limpiarCombo(ddlCurso_pnlReg)
        End If

    End Sub

    ''' <summary>
    ''' Exporta los datos del gridView en formato WORD,EXCEL,HTML,PDF,HTML.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     19/03/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Exportar()
        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        Dim int_AnioAcademico As Integer = ddlBuscarAnioAcademico.SelectedValue()
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet

            Dim obj_BL_Alumnos As New bl_Alumnos
        ds_Lista = obj_BL_Alumnos.FUN_REP_ExoneradosPorCurso(int_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)


            Dim dt As DataTable = New DataTable("ListaExportar")

            dt = Datos.agregarColumna(dt, "N°", "String")
            dt = Datos.agregarColumna(dt, "Codigo Alumno", "String")
            dt = Datos.agregarColumna(dt, "Nombre Completo", "String")
            dt = Datos.agregarColumna(dt, "Aula", "String")
            dt = Datos.agregarColumna(dt, "Localizacion Ministerio", "String")
            dt = Datos.agregarColumna(dt, "Cursos", "String")

            Dim cont As Integer = 1
            Dim auxDR As DataRow

            For Each dr As DataRow In ds_Lista.Tables(0).Rows
                auxDR = dt.NewRow
                auxDR.Item("N°") = cont
                auxDR.Item("Codigo Alumno") = dr.Item("CodigoAlumno").ToString
                auxDR.Item("Nombre Completo") = dr.Item("NombreAlumno").ToString
                auxDR.Item("Aula") = dr.Item("Aula").ToString
                auxDR.Item("Localizacion Ministerio") = dr.Item("LocalizacionMinisterio").ToString
                auxDR.Item("Cursos") = dr.Item("CursosExonerados").ToString
                dt.Rows.Add(auxDR)
                cont += 1
            Next

            NombreArchivo = Exportacion.ExportarReporte(dt, "Exoneracion alumnos")
            NombreArchivo = NombreArchivo & ".xls"
            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Matricula", "\Reportes\")

            downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()

    End Sub

    Private Sub cargarCombos()
        cargarComboAniosAcademicos()
        'cargarComboAulas()
        'limpiarCombo(ddlAulas)

    End Sub
    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)
        ddlBuscarAnioAcademico.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlBuscarAnioAcademico_SelectedIndexChanged()
    End Sub
    ''' <summary>
    ''' Llena el combo "ddlAulas" con la lista de grados activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     18/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAulas()
        If ddlBuscarAnioAcademico.SelectedValue = 0 Then
            limpiarCombo(ddlAulas)
        Else
            Dim obj_BL_Aulas As New bl_Aulas
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
            Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_AulasXAnio(ddlBuscarAnioAcademico.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("ListaAulas") = ds_Lista.Tables(0)

            Controles.llenarCombo(ddlAulas, ds_Lista, "CodigoAula", "DescAula", False, True)

        End If

    End Sub

    ''' <summary>
    ''' Setear permisos de acciones sobre el formulario según la configuración del usuario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        'Me.Master.RegistrarAccesoPagina(cod_Modulo, cod_Opcion)
    End Sub

    Private Sub GrabarFicha()
        Dim obj_BL_Alumnos As New bl_Alumnos

        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        Dim int_codigoAnioAcademico As Integer = 0
        Dim int_codigoAula As Integer = 0
        Dim int_codigoCurso As Integer = 0
        Dim str_codigoAlumno As String = ""

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim bool_Grabar As Boolean = False

        int_codigoAnioAcademico = ddlBuscarAnioAcademico.SelectedValue
        int_codigoCurso = ddlCurso_pnlReg.SelectedValue
        int_codigoAula = ddlAulas.SelectedValue
        str_codigoAlumno = hd_CodigoAlumno_pnl1.Value

        If int_codigoAula > 0 Then
            usp_valor = obj_BL_Alumnos.FUN_INS_ExoneracionesCursos(int_codigoAnioAcademico, str_codigoAlumno, int_codigoCurso, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        End If

        If usp_valor = 0 Then

        ElseIf usp_valor > 0 Then
            bool_Grabar = True
        End If



        If bool_Grabar = True Then
            ' usp_mensaje = "Operacion exitosa."
            MostrarSexyAlertBox(usp_mensaje, "Info")
            listar()
            limpiarFiltros()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
            'usp_mensaje = "No se grabo ningún registro."
        End If


        'MostrarSexyAlertBox(usp_mensaje, "Info")

    End Sub


    ''' <summary>
    ''' Valida el campo de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""
        Dim int_codigoAula As Integer = 0
        Dim int_cantidad As Integer = 0

   
        'If int_cantidad > 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Aula")
        '    result = False
        'End If

        str_Mensaje = str_alertas
        Return result

    End Function


    ''' <summary>
    ''' Lista los datos de la busqueda .
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' </remarks>
    Private Sub listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)
        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()


    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet

        Dim int_AnioAcademico As Integer = ddlBuscarAnioAcademico.SelectedValue()
        Dim int_Aula As Integer = ddlAulas.SelectedValue()
        Dim int_Estado As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Alumnos As New bl_Alumnos
            ds_Lista = obj_BL_Alumnos.FUN_LIS_ExoneradosPorCurso(int_AnioAcademico, int_Aula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_Alumnos As New bl_Alumnos
                ds_Lista = obj_BL_Alumnos.FUN_LIS_ExoneradosPorCurso(int_AnioAcademico, int_Aula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function



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

        ddlBuscarAnioAcademico.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        ddlBuscarAnioAcademico.Focus()
        ddlAulas.SelectedValue = 0
        ddlCurso_pnlReg.SelectedValue = 0
        listar()
    End Sub

#End Region


#Region "Eventos de Grilla"

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "AgregarRegistro" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                Dim NombreCompleto As Label = CType(row.FindControl("lblNombreAlumno"), Label)

                'Dim lblCodigoRegistroConductaBimestral As Label = CType(row.FindControl("lblCodigoRegistroConductaBimestral"), Label)

               
                'limpiarCamposPnl1()
                'lblBimestre_pnl1.Text = ddlBuscarBimestre.SelectedItem.ToString
                lblAnio_pnlReg.Text = ddlBuscarAnioAcademico.SelectedItem.ToString
                lblAula_pnlReg.Text = ddlAulas.SelectedItem.ToString
                lblAlumno_pnlReg.Text = NombreCompleto.Text
                hd_CodigoAlumno_pnl1.Value = codigo
                'hd_CodigoRegistroConductaBimestral_pnl1.Value = lblCodigoRegistroConductaBimestral.Text

                'If hd_CodigoRegistroConductaBimestral_pnl1.Value <= 0 Then
                '    MostrarSexyAlertBox("No existe codigo de registro conductual bimestral", "Alert")
                'Else
                pnModalAgregarRegistro.Show()
                '    ActivarPanel(1)
                'End If



            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Dim ddl As DropDownList = CType(e.Row.FindControl("ddlAula"), DropDownList)

            'Controles.llenarCombo(ddl, CType(ViewState("ListaAula"), DataSet), "Codigo", "Descripcion", False, True)
            'ddl.SelectedValue = e.Row.DataItem("CodigoAula")

            'If e.Row.DataItem("CodigoAula") <> 0 Then

            '    Dim BGColor As String = "#dcff7d"
            '    e.Row.Style.Add("background", BGColor)

            'End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If



    End Sub

#End Region


#Region "Manejo de Alertas - Emails"

    ''' <summary>
    ''' Recibe mensajes y los deriva a otro metodo que los visualizara cno animación de JQuery
    ''' </summary>
    ''' <param name="str_alertas">Mensaje que se quiere visualizar</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarAlertas(ByVal str_alertas As String)

        MostrarSexyAlertBox(str_alertas, "Alert")

    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub
#End Region

 

    Protected Sub btnCerraAgregarRegistro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerraAgregarRegistro.Click

    End Sub
End Class
