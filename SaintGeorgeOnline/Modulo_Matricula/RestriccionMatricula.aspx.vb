Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data

''' <summary>
''' Modulo de Descarga De Informacion Sobre Pagos Al Banco
''' </summary>
''' <remarks>
''' Código del Modulo:    1
''' Código de la Opción:  7
''' </remarks>

Partial Class Modulo_Matricula_RestriccionMatricula
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1


#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Restriccion de Matrícula")

            If Not Page.IsPostBack Then
                cargarComboPeriodoMatricula()
                ddlPeriodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            buscar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            grabar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"
    Private Sub cargarComboPeriodoMatricula()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlPeriodo, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    Private Sub buscar()

        Dim int_TipoFiltro As Integer = rbListFiltro.SelectedValue
        Dim str_Filtro As String = tbFiltro.Text.Trim
        Dim int_CodigoAnioMatricula As Integer = ddlPeriodo.SelectedValue

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obj_BL_Matricula As New bl_Matricula
        Dim ds_Lista As DataSet = obj_BL_Matricula.FUN_LIS_RetencionMatricula(int_TipoFiltro, str_Filtro, int_CodigoAnioMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("ListaAlumnos") = ds_Lista.Tables(0)
        ViewState("ListaDocumentos") = ds_Lista.Tables(1)
        ViewState("ListaDocumentosEntregados") = ds_Lista.Tables(2)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        ' Lista de Docuemntos
        dl_Documentos.DataSource = ds_Lista.Tables(1)
        dl_Documentos.DataBind()

    End Sub

    Private Sub grabar()
        Dim obj_BL_Matricula As New bl_Matricula
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""

        Dim dt_Registros As New DataTable
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoAlumno", "string")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoMotivoRestriccionMatricula", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoAnioAcademico", "integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "Check", "integer")

        Dim dr_R As DataRow

        Dim int_CodigoMotivoRestriccionMatricula As Integer = 0
        Dim int_CodigoAnioAcademico As Integer = 0
        Dim str_CodigoAlumno As String = ""
        Dim contObj As Integer = 0

        For Each gvr As GridViewRow In GridView1.Rows

            Dim chk_Documentos As CheckBoxList = gvr.FindControl("chk_Documentos")
            While contObj <= chk_Documentos.Items.Count - 1
                int_CodigoMotivoRestriccionMatricula = chk_Documentos.Items(contObj).Value
                str_CodigoAlumno = CType(gvr.FindControl("lblCodigoAlumno"), Label).Text
                int_CodigoAnioAcademico = ddlPeriodo.SelectedValue 'Me.Master.Obtener_CodigoPeriodoEscolar()

                dr_R = dt_Registros.NewRow
                dr_R.Item("CodigoAlumno") = str_CodigoAlumno
                dr_R.Item("CodigoMotivoRestriccionMatricula") = int_CodigoMotivoRestriccionMatricula
                dr_R.Item("CodigoAnioAcademico") = int_CodigoAnioAcademico

                If chk_Documentos.Items(contObj).Selected Then
                    dr_R.Item("Check") = 1
                Else
                    dr_R.Item("Check") = 0
                End If

                dt_Registros.Rows.Add(dr_R)
                contObj = contObj + 1
            End While
            contObj = 0
        Next

        usp_valor = obj_BL_Matricula.FUN_INS_RegistroRestriccionMatricula(dt_Registros, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            buscar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "alert")
        End If
    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     12/12/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    '''     Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
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
    ''' Fecha de Creación:     12/12/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim chk_Documentos As CheckBoxList = e.Row.FindControl("chk_Documentos")
            Dim str_CodigoAlumno As String = CType(e.Row.FindControl("lblCodigoAlumno"), Label).Text

            Dim dt_Documentos As New DataTable
            Dim dt_DocumentosAlumno As New DataTable

            dt_Documentos = ViewState("ListaDocumentos")
            dt_DocumentosAlumno = ViewState("ListaDocumentosEntregados")


            Dim dv As DataView = dt_DocumentosAlumno.DefaultView
            dv.RowFilter = "CodigoAlumno = '" & str_CodigoAlumno & "'"

            setearDetalleTabla(dt_Documentos, dv, str_CodigoAlumno, chk_Documentos)

            Dim lblIndex As Label = e.Row.FindControl("lblIndex")
            lblIndex.Text = e.Row.RowIndex + 1


            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

    Private Function setearDetalleTabla(ByVal dt_Documentos As DataTable, ByVal dv_DocumentosAlumno As DataView, ByVal str_CodigoAlumno As String, ByVal chkDocumentos As WebControls.CheckBoxList) As Boolean

        chkDocumentos.DataSource = dt_Documentos
        chkDocumentos.DataTextField = "DescripcionChk"
        chkDocumentos.DataValueField = "CodigoMotivoRestriccionMatricula"
        chkDocumentos.DataBind()

        Dim int_cantDocumentos As Integer = 0
        Dim contObj As Integer = 0

        For Each dr As DataRowView In dv_DocumentosAlumno
            If dr.Item("CodigoAlumno") = str_CodigoAlumno Then
                While contObj <= chkDocumentos.Items.Count - 1
                    If chkDocumentos.Items(contObj).Value = dr.Item("CodigoMotivoRestriccionMatricula") Then
                        chkDocumentos.Items(contObj).Selected = True
                    End If
                    contObj = contObj + 1
                End While
                contObj = 0
            End If
        Next

    End Function

End Class
