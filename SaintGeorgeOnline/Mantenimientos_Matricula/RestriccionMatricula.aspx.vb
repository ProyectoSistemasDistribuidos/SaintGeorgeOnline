Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Partial Class Mantenimientos_Matricula_RestriccionMatricula
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Motivo de Restriccion Matricula")
            If Not IsPostBack Then
                ' Dynamically create field columns to display the desired
                ' fields from the data source. Create a TemplateField object 
                ' to display an author's first and last name.

                cargarComboGrado()
                Listar(tbBuscarApellidoPaterno.Text.Trim(), tbBuscarApellidoMaterno.Text.Trim(), tbBuscarNombre.Text.Trim(), ddlGrados.SelectedValue)

                'ColumnaMotivoRestriccionMatricula()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(2, 28, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub
    Private Sub cargarComboGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlGrados, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    Private Sub Listar(ByVal ApellidoPaterno As String, ByVal ApellidoMaterno As String, ByVal Nombre As String, ByVal Grados As Integer)

        Dim obj_BL_Motivo As New bl_MotivoRestriccionMatricula
        Dim ds_Lista As DataSet = obj_BL_Motivo.FUN_LIS_AlumnosPorMotivoRestriccion(ApellidoPaterno, ApellidoMaterno, Nombre, Grados)
        Dim dt As New DataTable
        Dim dt_relacion As New DataTable
        dt = ds_Lista.Tables(0)
        'Dim bfield As New TemplateField

        'For Each col As DataColumn In dt.Columns
        '    bfield.HeaderTemplate = New GridViewTemplate(ListItemType.Header, col.ColumnName)
        '    GV_DinamicoAlumno.Columns.Add(bfield)
        'Next

        GV_DinamicoAlumno.DataSource = dt
        ViewState("MotivoRestriccionMatricula") = dt
        GV_DinamicoAlumno.DataBind()

    End Sub

    'Private Sub ColumnaMotivoRestriccionMatricula()

    '    Dim obj_BL_Motivo As New bl_MotivoRestriccionMatricula
    '    Dim ds_Lista As DataSet = obj_BL_Motivo.FUN_LIS_MotivoRestriccionMatriculaPorMotivoRestriccion()

    '    Dim dt As New DataTable
    '    dt = ds_Lista.Tables(0)
    '    Dim a As Integer = 0
    '    'For Each col As DataColumn In dt.Columns

    '    While a < dt.Rows.Count

    '        'For Each col As DataRow In dt.Rows(a).Item("Descripcion")
    '        Dim bfield As New TemplateField
    '        Dim col As String
    '        Dim bfield1 As New TemplateField
    '        col = dt.Rows(a).Item("Descripcion")

    '        bfield.HeaderTemplate = New GridViewTemplate(ListItemType.Header, col.ToString)
    '        bfield1.FooterTemplate = New GridViewTemplate(ListItemType.Footer, col.ToString)
    '        GV_DinamicoMotivo.Columns.Add(bfield)
    '        GV_DinamicoMotivo.Columns.Add(bfield1)
    '        'Next
    '        a = a + 1
    '    End While
    '    GV_DinamicoMotivo.DataSource = dt

    '    GV_DinamicoMotivo.DataBind()

    '    ''Dim customField As New TemplateField
    '    'Dim a As New TemplateField
    '    'Dim b As New TemplateField
    '    'Dim c As New TemplateField
    '    'Dim d As New TemplateField
    '    '' Create the dynamic templates and assign them to
    '    '' the appropriate template property.
    '    ''customField.ItemTemplate = New GridViewTemplate(DataControlRowType.DataRow, "Author Name")
    '    ''customField.HeaderTemplate = New GridViewTemplate(DataControlRowType.Header, "Author Name")
    '    'a.HeaderTemplate = New GridViewTemplate(DataControlRowType.Header, "Motivo1")
    '    'b.HeaderTemplate = New GridViewTemplate(DataControlRowType.Header, "Motivo2")
    '    'c.HeaderTemplate = New GridViewTemplate(DataControlRowType.Header, "Motivo3")
    '    'd.HeaderTemplate = New GridViewTemplate(DataControlRowType.Header, "Motivo4")
    '    '' Add the field column to the Columns collection of the
    '    '' GridView control.
    '    ''GV_Dinamico.Columns.Add(customField)
    '    'GV_Dinamico.Columns.Add(a)
    '    'GV_Dinamico.Columns.Add(b)
    '    'GV_Dinamico.Columns.Add(c)
    '    'GV_Dinamico.Columns.Add(d)
    'End Sub

#Region "Click"
    Protected Sub btnBuscar_Click()
        Try
            Listar(tbBuscarApellidoPaterno.Text.Trim(), tbBuscarApellidoMaterno.Text.Trim(), tbBuscarNombre.Text.Trim(), ddlGrados.SelectedValue)
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        tbBuscarApellidoPaterno.Text = ""
        tbBuscarApellidoMaterno.Text = ""
        tbBuscarNombre.Text = ""
        ddlGrados.SelectedValue = 0
    End Sub
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim objDT_Cabecera As New DataTable("DatosCabecera")
            objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "CodigoAlumno", "Int")
            objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "NombreCompleto", "string")
            objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "Opcion", "int")
            objDT_Cabecera = Datos.agregarColumna(objDT_Cabecera, "Observacion", "string")

            For Each gvr As GridViewRow In GV_DinamicoAlumno.Rows
                If CType(gvr.FindControl("lblOpcion"), CheckBox).Checked Then
                    Dim dr As DataRow
                    dr = objDT_Cabecera.NewRow
                    dr.Item(0) = CType(gvr.FindControl("lblCodigo"), Label).Text
                    dr.Item(1) = CType(gvr.FindControl("lblNombreCompleto"), Label).Text
                    dr.Item(2) = 1
                    dr.Item(3) = CType(gvr.FindControl("lblObservacion1"), TextBox).Text
                    objDT_Cabecera.Rows.Add(dr)
                Else
                    Dim dr As DataRow
                    dr = objDT_Cabecera.NewRow
                    dr.Item(0) = CType(gvr.FindControl("lblCodigo"), Label).Text
                    dr.Item(1) = CType(gvr.FindControl("lblNombreCompleto"), Label).Text
                    dr.Item(2) = 0
                    dr.Item(3) = CType(gvr.FindControl("lblObservacion1"), TextBox).Text
                    objDT_Cabecera.Rows.Add(dr)
                End If
            Next

            Dim dt_MotivoRestriccionBD As DataTable
            Dim obj_BL_MotivoRestriccionMatricula As New bl_MotivoRestriccionMatricula
            Dim usp_mensaje As String = ""
            Dim usp_valor As Integer = 0

            dt_MotivoRestriccionBD = ViewState("MotivoRestriccionMatricula")

            Dim cont As Integer = 0
            Dim cont1 As Integer = 0
            Dim codigo As Integer = 0
            Dim opcion As Integer = 0
            Dim nombreCompleto As String
            Dim observacion As String

            While cont < dt_MotivoRestriccionBD.Rows.Count

                While cont1 < objDT_Cabecera.Rows.Count
                    If objDT_Cabecera.Rows(cont1).Item("CodigoAlumno") = dt_MotivoRestriccionBD.Rows(cont).Item("CodigoAlumno") Then
                        codigo = objDT_Cabecera.Rows(cont1).Item("CodigoAlumno")
                        nombreCompleto = objDT_Cabecera.Rows(cont1).Item("nombreCompleto")
                        opcion = objDT_Cabecera.Rows(cont1).Item("Opcion")
                        observacion = objDT_Cabecera.Rows(cont1).Item("Observacion")

                        usp_valor = obj_BL_MotivoRestriccionMatricula.FUN_UPD_RelacionMotivoRestriccionMatricula(codigo, nombreCompleto, opcion, observacion)
                        Exit While
                    End If
                    cont1 = cont1 + 1
                End While

                cont = cont + 1
            End While

            If usp_valor > 0 Then
                'MostrarSexyAlertBox(usp_mensaje, "Info")
                MsgBox("ok")
                btnBuscar_Click()
            Else
                MsgBox("Error")
                'MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub
#End Region

    Protected Sub GV_DinamicoAlumno_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV_DinamicoAlumno.RowCommand
        Dim Codigo As Integer = CInt(e.CommandArgument.ToString)
        Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim opcion1 As Integer = CType(row.FindControl("lblOpcion1"), Label).Text
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        tbBuscarApellidoPaterno.Text = ""
        tbBuscarApellidoMaterno.Text = ""
        tbBuscarNombre.Text = ""
        ddlGrados.SelectedValue = 0
        Listar(tbBuscarApellidoPaterno.Text.Trim(), tbBuscarApellidoMaterno.Text.Trim(), tbBuscarNombre.Text.Trim(), ddlGrados.SelectedValue)

    End Sub
End Class
