﻿Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloSeguimiento

Imports System.Data.SqlClient
Imports System.IO

Partial Class Interfaz_Familia_Modulo_Seguimiento_WeeklyReport
    Inherits System.Web.UI.Page

    Dim cod_Modulo As Integer = 1
    Dim cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Master.MostrarTitulo("Weekly Report")
        Try
            If Not Page.IsPostBack Then
                ViewState("SortExpression") = "Bimestre"
                ViewState("Direccion") = "ASC"
                AlumnosPorCodigoFamilia()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub dl_DatosAlumno_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs)
        Dim int_CodigoAccion As Integer
        Try

            If e.CommandName = "Ver" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As DataListItem = CType(btn.NamingContainer, DataListItem)
                Dim table As HtmlTable = CType(btn.Parent.Parent.Parent, HtmlTable)
                Dim int_contItems As Integer = 0
                Dim btn_Contenedor As ImageButton
                Dim table_Contenedor As HtmlTable

                While int_contItems <= dl_DatosAlumno.Items.Count - 1
                    btn_Contenedor = dl_DatosAlumno.Items(int_contItems).FindControl("btnVer_dl")
                    table_Contenedor = CType(btn_Contenedor.Parent.Parent.Parent, HtmlTable)
                    table_Contenedor.Style.Value = "background-color:#17c4fc;"
                    int_contItems = int_contItems + 1
                End While

                If e.CommandName = "Ver" Then
                    int_CodigoAccion = 6
                    obtenerDatos(codigo)
                    table.Style.Value = "background-color:#215386;"
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub dl_DatosAlumno_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)

        Dim img As Image = e.Item.FindControl("img_Foto_dl")
        img.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & e.Item.DataItem("RutaFoto")

    End Sub

#End Region

#Region "Método"

    Private Sub exportar(ByVal int_CodigoBimestre As Integer, ByVal int_CodigoSemana As Integer)

        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar '1

        Dim int_CodigoGrado As Integer = lblCodigoGrado.Text '1
        Dim int_CodigoAula As Integer = lblCodigoSeccion.Text ' 1
        Dim str_CodigoAlumno As String = lblCodigoAlumno.Text '"20110203"

        Dim obj_BL_WeeklyReport As New bl_ProgramacionWeekly
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado ' 

        Dim ds_Lista As DataSet = obj_BL_WeeklyReport.FUN_LIS_WeeklyReportFamilia(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoAula, str_CodigoAlumno, int_CodigoBimestre, int_CodigoSemana, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If ds_Lista.Tables.Count = 8 Then

            If Not (ds_Lista.Tables(0).Rows.Count > 0 And ds_Lista.Tables(5).Rows.Count > 0) Then
                Me.Master.MostrarMensajeAlert("No existen registros de notas para el alumno consultado.")
                Exit Sub
            Else
                'Archivo PDF : Weekly Report
                Dim m As System.IO.MemoryStream = New System.IO.MemoryStream

                m = Exportacion.ExportarReporteWeekly_Pdf(ds_Lista, "WeeklyReport")

                'Exportar
                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-disposition", "attachment;filename=Reporte.pdf")
                Response.Cache.SetCacheability(HttpCacheability.NoCache)

                Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length)
                Response.OutputStream.Flush()
                Response.OutputStream.Close()
                Response.End()

                ''Archivo Word : Weekly Report
                'Dim rutamadre As String = ""
                'rutamadre = Server.MapPath(".")
                'rutamadre = rutamadre.Replace("\Interfaz_Familia\Modulo_Seguimiento", "\Reportes\")

                'Dim downloadBytes As Byte()
                'Dim NombreArchivo As String = ""

                'NombreArchivo = GenerarReporte(ds_Lista)
                'downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

                'Dim str_FileName As String
                'str_FileName = "WeeklyReport.doc"

                'Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
                'Response.Clear()
                'Response.Charset = ""
                'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                'Response.ContentType = "application/vnd.word"
                'Response.AddHeader("content-disposition", "attachment;filename=" & str_FileName + "; size=" + downloadBytes.Length.ToString())
                'Response.Flush()
                'Response.BinaryWrite(downloadBytes)
                'Response.End()

            End If

        End If


    End Sub

    Public Shared Function obtenerNota(ByVal dv_Notas As DataView, ByVal int_CodigoCurso As Integer, ByVal int_CodigoCriterio As Integer) As String

        Dim str_Nota As String = ""

        For Each drv As DataRowView In dv_Notas
            If drv.Item("CodigoCurso") = int_CodigoCurso And drv.Item("CodigoCriterio") = int_CodigoCriterio Then
                str_Nota = drv.Item("Nota")
                Exit For
            End If
        Next

        Return str_Nota

    End Function

    ''' <summary>
    ''' Lista los de alumnos por el codigo de familia      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     16/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub AlumnosPorCodigoFamilia()

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva
        Dim ds_Lista As New DataSet

        If int_CodigoTipoUsuario = 1 Then ' Alumnos

            ds_Lista = obj_BL_Alumnos.FUN_GET_AlumnosPorCodigoAlumno(int_CodigoFamilia, int_CodigoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 65)
            'ddl_Alumno.Enabled = False

        ElseIf int_CodigoTipoUsuario = 3 Then ' Familiares

            ds_Lista = obj_BL_Alumnos.FUN_LIS_AlumnosPorCodigoFamilia(int_CodigoFamilia, int_CodigoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 8)
            'ddl_Alumno.Enabled = True

        End If

        dl_DatosAlumno.DataSource = ds_Lista.Tables(1)
        dl_DatosAlumno.DataBind()
        ViewState("ListaDatosAlumno") = ds_Lista.Tables(1)

        If ds_Lista.Tables(1).Rows.Count > 0 Then

            img_Foto.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & ds_Lista.Tables(1).Rows(0).Item("RutaFoto").ToString
            lblCodigoAlumno.Text = ds_Lista.Tables(1).Rows(0).Item("CodigoAlumno")
            lblNombre.Text = ds_Lista.Tables(1).Rows(0).Item("NombreCompleto")
            lblGrado.Text = ds_Lista.Tables(1).Rows(0).Item("GradoAcad")
            lblSeccion.Text = ds_Lista.Tables(1).Rows(0).Item("AulaAcad")
            lblCodigoGrado.Text = ds_Lista.Tables(1).Rows(0).Item("CodigoGrado")
            lblCodigoSeccion.Text = ds_Lista.Tables(1).Rows(0).Item("CodigoAula")
            listar()

            Dim btn_Contenedor As ImageButton = dl_DatosAlumno.Items(0).FindControl("btnVer_dl")
            Dim table_Contenedor As HtmlTable

            table_Contenedor = CType(btn_Contenedor.Parent.Parent.Parent, HtmlTable)
            table_Contenedor.Style.Value = "background-color:#215386;"

            If int_CodigoTipoUsuario = 1 Then

                obtenerDatos(int_CodigoUsuario)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Lista los de alumnos por el codigo de familia      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     16/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtenerDatos(ByVal codigo As Integer)
        Dim dt As DataTable
        dt = ViewState("ListaDatosAlumno")

        Dim dv As DataView
        dv = dt.DefaultView
        dv.RowFilter = "1=1 and CodigoAlumno =" & codigo.ToString

        img_Foto.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & dv.Item(0).Item("RutaFoto").ToString
        lblCodigoAlumno.Text = dv.Item(0).Item("CodigoAlumno")
        lblNombre.Text = dv.Item(0).Item("NombreCompleto")
        lblGrado.Text = dv.Item(0).Item("GradoAcad")
        lblSeccion.Text = dv.Item(0).Item("AulaAcad")
        lblCodigoGrado.Text = dv.Item(0).Item("CodigoGrado")
        lblCodigoSeccion.Text = dv.Item(0).Item("CodigoAula")
        listar()

    End Sub

    ''' <summary>
    ''' Lista los datos      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     06/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listar()
        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)
        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
        ImagenSorting(ViewState("SortExpression"))
    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet
        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar '1 
        Dim str_CodigoAlumno As String = lblCodigoAlumno.Text '"20110203"

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_WeeklyFamilia As New bl_ProgramacionWeekly
            ds_Lista = obj_BL_WeeklyFamilia.FUN_LIS_WeeklyFamilia( _
            int_CodigoAnioAcademico, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_WeeklyFamilia As New bl_ProgramacionWeekly
                ds_Lista = obj_BL_WeeklyFamilia.FUN_LIS_WeeklyFamilia( _
                int_CodigoAnioAcademico, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista

    End Function

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(0, 8, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_mensaje As String, ByVal str_tipoMensaje As String)
        Dim SexyAlertScript As String = ""
        Select Case str_tipoMensaje
            Case "Alert"
                SexyAlertScript = "Sexy.alert('" & str_mensaje & "');"
            Case "Info"
                SexyAlertScript = "Sexy.info('" & str_mensaje & "');"
            Case "Error"
                SexyAlertScript = "Sexy.error('" & str_mensaje & "');"
        End Select
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", SexyAlertScript, True)
    End Sub

    Protected Sub MostrarAlertBox(ByVal str_Mensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, "")

    End Sub
#End Region

#Region "Metodos del Gridview"

    ''' <summary>
    ''' Agrega el índice de páginas al combo de páginación. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
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
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
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

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
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
    ''' Lista los datos de procedimientos realizados ordenados por Descripción.
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(2)

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        GridView1.DataSource = dv
        GridView1.DataBind()

    End Sub

    '''' <summary>
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        If GridView1.Rows.Count > 0 Then
            Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
            Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Bimestre"), ImageButton)
            Dim _btnSorting_d2 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Semana"), ImageButton)

            If _btnSorting.ID = _btnSorting_d1.ID Then
                _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
                _btnSorting_d2.ToolTip = "Descendente"
            Else
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

        End If


    End Sub

#End Region
    
#Region "Eventos del Gridview"

    Protected Sub ddlPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GridView1.PageCount Then
                Me.GridView1.PageIndex = _NumPag - 1
            Else
                Me.GridView1.PageIndex = 0
            End If

            Me.GridView1.SelectedIndex = -1

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Visualizar" Then

                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                Dim int_CodigoBimestre As Integer = row.Cells(0).Text 'CInt(row.Cells("lblCodigoBimestre").Text)
                Dim int_CodigoSemana As Integer = row.Cells(1).Text 'CInt(row.Cells("lblCodigoSemana").Text)

                'int_CodigoBimestre = row.Cells(1).Text
                'int_CodigoSemana = row.Cells(0).Text

                If e.CommandName = "Visualizar" Then
                    exportar(int_CodigoBimestre, int_CodigoSemana)
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim btnVer As ImageButton = e.Row.FindControl("btnVer")

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = GridView1.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            btnVer.Attributes.Add("OnClick", " ShowMyModalPopup()")

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GridView1.PageIndex = e.NewPageIndex
            End If

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
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

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager(GridView1, e.Row, Me)
        End If

    End Sub
#End Region

End Class
