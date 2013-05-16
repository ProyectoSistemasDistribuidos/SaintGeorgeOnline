Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones

Partial Class Interfaz_Familia_Modulo_CompanierosAula_CompanierosAula
    Inherits System.Web.UI.Page

    Dim cod_Modulo As Integer = 1
    Dim cod_Opcion As Integer = 1

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Master.MostrarTitulo("Compañeros de Aula")
        Try
            If Not Page.IsPostBack Then
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

    Protected Sub dl_CompanierosAula_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)

        Dim img As Image = e.Item.FindControl("img_Foto_dlCompanierosAula")

        img.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & e.Item.DataItem("RutaFoto")


    End Sub

#End Region

#Region "Método"
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
        Dim int_CodigoFamiliar As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

        Dim ds_Lista As DataSet

        If int_CodigoTipoUsuario = 3 Then ' familiar

            'int_CodigoFamiliar : codigo Familiar
            ds_Lista = obj_BL_Alumnos.FUN_LIS_AlumnosPorCodigoFamilia(int_CodigoFamilia, int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 8)

        Else ' 1 : alumno

            'int_CodigoFamiliar : codigo Alumno
            ds_Lista = obj_BL_Alumnos.FUN_GET_AlumnosPorCodigoAlumnoYPeriodo(int_CodigoFamilia, int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 8)

        End If

        dl_DatosAlumno.DataSource = ds_Lista.Tables(1)
        dl_DatosAlumno.DataBind()

        ViewState("ListaDatosAlumno") = ds_Lista.Tables(1)

        If ds_Lista.Tables(1).Rows.Count > 0 Then

            img_Foto.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & ds_Lista.Tables(1).Rows(0).Item("RutaFoto").ToString
            lblCodigoAlumno.Text = ds_Lista.Tables(1).Rows(0).Item("CodigoAlumno")
            lblCodigoGrado.Text = ds_Lista.Tables(1).Rows(0).Item("CodigoGrado")
            lblCodigoAula.Text = ds_Lista.Tables(1).Rows(0).Item("CodigoAula")
            lblNombre.Text = ds_Lista.Tables(1).Rows(0).Item("NombreCompleto")
            lblGrado.Text = ds_Lista.Tables(1).Rows(0).Item("GradoAcad")
            lblSeccion.Text = ds_Lista.Tables(1).Rows(0).Item("AulaAcad")
            listar()

            Dim btn_Contenedor As ImageButton = dl_DatosAlumno.Items(0).FindControl("btnVer_dl")
            Dim table_Contenedor As HtmlTable

            table_Contenedor = CType(btn_Contenedor.Parent.Parent.Parent, HtmlTable)
            table_Contenedor.Style.Value = "background-color:#215386;"

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
        lblCodigoGrado.Text = CInt(dv.Item(0).Item("CodigoGrado"))
        lblCodigoAula.Text = CInt(dv.Item(0).Item("CodigoAula"))
        lblNombre.Text = dv.Item(0).Item("NombreCompleto")
        lblGrado.Text = dv.Item(0).Item("GradoAcad")
        lblSeccion.Text = dv.Item(0).Item("AulaAcad")
        listar()

    End Sub

    ''' <summary>
    ''' Lista los datos      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     21/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda()
        dl_CompanierosAula.DataSource = ds_Lista.Tables(0)
        dl_CompanierosAula.DataBind()

    End Sub


    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     21/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda() As DataSet

        Dim int_CodigoAlumno As Integer = lblCodigoAlumno.Text
        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim int_CodigoGrado As Integer = CInt(lblCodigoGrado.Text)
        Dim int_CodigoAula As Integer = CInt(lblCodigoAula.Text)
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        Dim obj_BL_Alumnos As New bl_Alumnos
        ds_Lista = obj_BL_Alumnos.FUN_LIS_CompanierosAlumnos( _
        int_CodigoAlumno, int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Return ds_Lista

    End Function

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     05/09/2011
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
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     05/09/2011
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

#End Region

End Class
