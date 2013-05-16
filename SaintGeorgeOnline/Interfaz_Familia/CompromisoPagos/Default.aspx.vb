Imports System.Data
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_Utilities
Partial Class Interfaz_Familia_CompromisoPagos_Default
    Inherits System.Web.UI.Page
#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarGrilla()
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlAlumno"), DropDownList)
            Dim ds_Dataset As New DataSet
            Dim obj_AsignacionBecas As New bl_AsignacionBecas
            Dim int_CodigoUsuario As Integer = 1
            Dim int_CodigoTipoUsuario As Integer = 1
            ds_Dataset = obj_AsignacionBecas.FUN_LIS_AlumnoPrueba(int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
            Controles.llenarCombo(ddl, ds_Dataset, "CodigoAlumno", "NombreCompleto", False, True)
            'ddl.SelectedValue = e.Row.DataItem("CodigoAlumno")
        End If
    End Sub
    Protected Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        ModalPopupExtender1.Show()
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        ModalPopupExtender1.Hide()
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "Actualizar" Then
                Dim Int_Codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim Row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                Dim cbo_CodAlumno As Integer = CType(Row.FindControl("ddlAlumno"), DropDownList).SelectedValue
                Editar(Int_Codigo, cbo_CodAlumno)
                ModalPopupExtender1.Show()
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "Metodos"
    Private Sub CargarGrilla()
        Dim ds_Lista As DataSet
        Dim obj_CalendarioPagos As New bl_CalendarioPagos
        Dim int_CodigoAlumno As Integer = 20100044
        Dim int_CodigoAnioAcademico As Integer = 1
        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1
        ds_Lista = obj_CalendarioPagos.FUN_LIS_CronogramaPagos(int_CodigoAlumno, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        GridView1.DataSource = ds_Lista
        GridView1.DataBind()
    End Sub
    Private Sub Editar(ByVal Int_CodAlumno As Integer, ByVal Int_CboCodAlumno As Integer)
        Dim ds_Lista As DataSet
        Dim obj_CalendarioPagos As New bl_CalendarioPagos
        Dim obj_AsignacionBecas As New bl_AsignacionBecas
        Dim int_CodigoAnioAcademico As Integer = 1
        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1
        Dim ds_Dataset As DataSet
        ds_Lista = obj_CalendarioPagos.FUN_LIS_CronogramaPagos(Int_CodAlumno, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        ds_Dataset = obj_AsignacionBecas.FUN_LIS_AlumnoPrueba(int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        txtCodAlumno.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString
        txtDescConcepCobro.Text = ds_Lista.Tables(0).Rows(0).Item("DescConceptoCobro").ToString
        txtEstado.Text = ds_Lista.Tables(0).Rows(0).Item("Estado").ToString
        txtMonto.Text = ds_Lista.Tables(0).Rows(0).Item("Monto").ToString
        txtVencimiento.Text = ds_Lista.Tables(0).Rows(0).Item("FechaVencimientoStr").ToString
        txtFechaEmision.Text = ds_Lista.Tables(0).Rows(0).Item("FechaEmisionStr").ToString
        Controles.llenarCombo(ddlMonto1, ds_Dataset, "CodigoAlumno", "NombreCompleto", True, False)
        ddlMonto1.SelectedValue = Int_CboCodAlumno
    End Sub
#End Region

    

  
    
    

End Class
