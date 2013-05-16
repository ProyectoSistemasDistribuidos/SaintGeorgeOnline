Imports System.Data
Imports System.IO
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula

Partial Class Interfaz_Familia_Modulo_Matricula_Matricula
    Inherits System.Web.UI.Page
    Dim cod_Modulo As Integer = 1
    Dim cod_Opcion As Integer = 1

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Master.MostrarTitulo("Matrícula")
        Try
            If Not Page.IsPostBack Then
                AlumnosPorCodigoFamiliaMatricula()
            End If
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnDescargaManual_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim NombreArchivo As String = ""
        NombreArchivo = "ProcesoMatricula"

        NombreArchivo = NombreArchivo & ".pdf"
        'rutamadre = Server.MapPath(".")
        rutamadre = ConfigurationManager.AppSettings("RutaDocumentoMatricula_Local").ToString() & NombreArchivo

        downloadBytes = File.ReadAllBytes(rutamadre)

        Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()

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
    Private Sub AlumnosPorCodigoFamiliaMatricula()
        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado '162
        Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva '20040088
        Dim ds_Lista As DataSet = obj_BL_Alumnos.FUN_LIS_AlumnosPorCodigoFamiliaMatricula(int_CodigoFamilia, int_CodigoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 8)

        GridView1.DataSource = ds_Lista.Tables(1)
        GridView1.DataBind()

        ViewState("ListaDatosAlumno") = ds_Lista.Tables(1)

    End Sub
#End Region

#Region "Eventos del Gridview"
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "ProcesoMatricula" Or e.CommandName = "Constancia" Then
                Dim CodigoAlumno As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then

                    Dim str_CodigoAlumno As String = CType(row.FindControl("lblCodigoAlumno"), Label).Text
                    Dim int_CodigoAnioMatricula As Integer = CType(row.FindControl("lblCodigoAnioMatricula"), Label).Text
                    Dim int_CodigoNivelMatricula As Integer = CType(row.FindControl("lblCodigoNivelMatricula"), Label).Text
                    Dim int_CodigoGradoMatricula As Integer = CType(row.FindControl("lblCodigoGradoMatricula"), Label).Text
                    Dim str_NivelMatricula As String = CType(row.FindControl("lblNivelMatricula"), Label).Text
                    Dim str_GradoMatricula As String = CType(row.FindControl("lblGradoMatricula"), Label).Text
                    Dim str_NombreCompleto As String = CType(row.FindControl("lblNombreCompleto"), Label).Text
                    Dim str_Foto As String = CType(row.FindControl("lblRutaFoto"), Label).Text
                    Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva

                    Session("ActualizacionDatosMatricula") = str_CodigoAlumno & "," & _
                                                             int_CodigoAnioMatricula & "," & _
                                                             Me.Master.Obtener_CodigoFamiliarLogueado & "," & _
                                                             int_CodigoNivelMatricula & "," & _
                                                             int_CodigoGradoMatricula & "," & _
                                                             str_NivelMatricula & "," & _
                                                             str_GradoMatricula & "," & _
                                                             str_NombreCompleto & "," & _
                                                             str_Foto & "," & _
                                                             int_CodigoFamilia
                    Response.Redirect("ActualizacionDatosMatricula.aspx")

                ElseIf e.CommandName = "ProcesoMatricula" Then

                    Dim str_CodigoAlumno As String = CType(row.FindControl("lblCodigoAlumno"), Label).Text
                    Dim int_CodigoAnioMatricula As Integer = CType(row.FindControl("lblCodigoAnioMatricula"), Label).Text
                    Dim int_CodigoNivelMatricula As Integer = CType(row.FindControl("lblCodigoNivelMatricula"), Label).Text
                    Dim int_CodigoGradoMatricula As Integer = CType(row.FindControl("lblCodigoGradoMatricula"), Label).Text
                    Dim str_NivelMatricula As String = CType(row.FindControl("lblNivelMatricula"), Label).Text
                    Dim str_GradoMatricula As String = CType(row.FindControl("lblGradoMatricula"), Label).Text
                    Dim str_NombreCompleto As String = CType(row.FindControl("lblNombreCompleto"), Label).Text
                    Dim str_Foto As String = CType(row.FindControl("lblRutaFoto"), Label).Text

                    Session("DatosMAtriculaAlumno") = str_CodigoAlumno & "," & int_CodigoAnioMatricula & "," & Me.Master.Obtener_CodigoFamiliarLogueado & "," & int_CodigoNivelMatricula & "," & int_CodigoGradoMatricula & "," & str_NivelMatricula & "," & str_GradoMatricula & "," & str_NombreCompleto & "," & str_Foto
                    Response.Redirect("ProcesoMatricula.aspx")

                ElseIf e.CommandName = "Constancia" Then
                    'If Not (ds_Lista.Tables(0).Rows.Count >0) Then
                    Dim obj_BL_Matricula As New bl_Matricula
                    Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
                    Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado '162
                    Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva '20040088

                    Dim str_CodigoAlumno As String = CType(row.FindControl("lblCodigoAlumno"), Label).Text
                    Dim int_CodigoAnioMatricula As Integer = CType(row.FindControl("lblCodigoAnioMatricula"), Label).Text

                    Dim ds_Lista As DataSet = obj_BL_Matricula.FUN_LIS_RegistroMatriculaXalumno(int_CodigoAnioMatricula, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                    If ds_Lista.Tables(0).Rows(0).Item("CodigoMatricula") > 0 Then

                        exportarConsolidadoMatricula(ds_Lista)

                    Else
                        Me.Master.MostrarMensajeAlert("No existe Constancia de matrícula para el alumno consultado.Debe de realizar el Proceso de Matrícula")
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            'EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
            Dim btnProcesoMatricula As ImageButton = e.Row.FindControl("btnProcesoMatricula")
            Dim btnConstancia As ImageButton = e.Row.FindControl("btnConstancia")
            Dim lbl_CodigoMatricula As Label = CType(e.Row.FindControl("lblCodigoMatricula"), Label)
            Dim lbl_Confirmacion As Label = CType(e.Row.FindControl("lblConfirmacion"), Label)
            Dim lbl_EstadoMatricula As Label = CType(e.Row.FindControl("lblEstadoMatricula"), Label)
            Dim CodigoPasoLogMatriculaEtapa1 As Integer = CType(e.Row.FindControl("CodigoPasoLogMatriculaEtapa1"), Label).Text
            Dim CodigoPasoLogMatriculaEtapa2 As Integer = CType(e.Row.FindControl("CodigoPasoLogMatriculaEtapa2"), Label).Text

            If lbl_Confirmacion.Text = 0 Then

                ' Verifico los codigos de Paso de matrícula registrados en log
                If CodigoPasoLogMatriculaEtapa1 = 0 Or CodigoPasoLogMatriculaEtapa1 = 6 Or CodigoPasoLogMatriculaEtapa1 = 7 Then
                    btnActualizar.Visible = True
                    btnProcesoMatricula.Visible = False
                ElseIf CodigoPasoLogMatriculaEtapa1 = 8 Then
                    btnActualizar.Visible = False
                    btnProcesoMatricula.Visible = True
                Else
                    btnActualizar.Visible = False
                    btnProcesoMatricula.Visible = False
                End If

                'btnProcesoMatricula.Visible = True
                btnConstancia.Visible = False
                lbl_EstadoMatricula.ForeColor = Drawing.Color.DarkRed

            Else
                btnActualizar.Visible = False
                btnProcesoMatricula.Visible = False
                btnConstancia.Visible = True
                lbl_EstadoMatricula.ForeColor = Drawing.Color.DarkBlue
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

    Private Sub exportarConsolidadoMatricula(ByVal dt As DataSet) 'ExportarReporteConsolidadoMatricula_Pdf
        'Archivo PDF : Weekly Report
        Dim m As System.IO.MemoryStream = New System.IO.MemoryStream

        m = Exportacion.ExportarReporteConsolidadoMatricula_Pdf(dt, "Constancia de la Matrícula Online")

        'Exportar
        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=Reporte.pdf")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length)
        Response.OutputStream.Flush()
        Response.OutputStream.Close()
        Response.End()
    End Sub

#End Region

End Class
