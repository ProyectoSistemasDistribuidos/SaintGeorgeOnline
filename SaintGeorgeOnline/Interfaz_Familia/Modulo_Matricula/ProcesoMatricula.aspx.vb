Imports System.Data
Imports System.IO
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports Ionic.Zip

''' <summary>
''' Modulo de Matricula
''' </summary>
''' <remarks>
''' Código del Modulo:    4
''' Código de la Opción:  74
''' </remarks>
Partial Class Interfaz_Familia_Modulo_Matricula_ProcesoMatricula
    Inherits System.Web.UI.Page

#Region "Eventos"

    Private cod_Modulo As Integer = 4
    Private cod_Opcion As Integer = 74

    Dim miSer As New wsMatricula.BancoLibrosWebService

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Proceso de Matrícula")

            If Not Page.IsPostBack Then

                If Session("DatosMAtriculaAlumno") IsNot Nothing Then

                    Dim arr_Datos As String() = Session("DatosMAtriculaAlumno").ToString.Split(",")
                    hiddenCodigoAlumno.Value = arr_Datos(0).ToString
                    hiddenCodigoAnioAcademico.Value = arr_Datos(1).ToString
                    hiddenCodigoFamiliar.Value = arr_Datos(2).ToString
                    hiddenCodigoNivel.Value = arr_Datos(3).ToString
                    hiddenCodigoGrado.Value = arr_Datos(4).ToString
                    hiddenNivel.Value = arr_Datos(5).ToString
                    hiddenGrado.Value = arr_Datos(6).ToString
                    hiddenNombreCompleto.Value = arr_Datos(7).ToString
                    hiddenFoto.Value = arr_Datos(8).ToString

                    img_FotoAlumno.ImageUrl = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & hiddenFoto.Value
                    lbl_NombreAlumno.Text = hiddenNombreCompleto.Value

                    Session.Remove("DatosMAtriculaAlumno")
                    Session("DatosMAtriculaAlumno") = Nothing

                End If

                'hiddenCodigoAlumno.Value = "20120011"
                'hiddenCodigoAnioAcademico.Value = 2
                'hiddenCodigoFamiliar.Value = 1646
                'hiddenCodigoNivel.Value = 1
                'hiddenCodigoGrado.Value = 3
                'hiddenNivel.Value = "Junior"
                'hiddenGrado.Value = "Kinder"
                'hiddenCodigoAlumno.Value = "20110203" ' Sin Deuda
                ''hiddenCodigoAlumno.Value = "20110094" ' Con Deuda
                ''hiddenCodigoAlumno.Value = "20110181" ' Con Deuda pagada
                'hiddenCodigoAnioAcademico.Value = 2 ' 2012

                SetearAccionesAcceso()
                CargarDatosMatricula()



                Cargar_Etapa1()
                'Cargar_Etapa5()


                'Ir directo al ultimo paso
                'mv_PasosMatricula.ActiveViewIndex = 2

                'btn_MenuPaso6.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso6Matricula_D.jpg"
                'btn_MenuPaso7.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso7Matricula.jpg"

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub btn_DocumentoDireccion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        exportar(5)
    End Sub

    Protected Sub btn_RetrocederEtapa2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            'Cargar_Etapa1()
            mv_PasosMatricula.ActiveViewIndex = 0
            btn_MenuPaso1.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso1Matricula.jpg"
            btn_MenuPaso2.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso2Matricula_D.jpg"

        Catch ex As Exception
            EnvioEmailError(116, ex.ToString)
        End Try
    End Sub

    Protected Sub btn_RetrocederEtapa3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            'Cargar_Etapa2()
            mv_PasosMatricula.ActiveViewIndex = 1
            btn_MenuPaso1.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso1Matricula_D.jpg"
            btn_MenuPaso2.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso2Matricula.jpg"
            btn_MenuPaso3.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso3Matricula_D.jpg"

        Catch ex As Exception
            EnvioEmailError(116, ex.ToString)
        End Try
    End Sub

    Protected Sub btn_RetrocederEtapa4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            'Cargar_Etapa3()
            mv_PasosMatricula.ActiveViewIndex = 2
            btn_MenuPaso2.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso2Matricula_D.jpg"
            btn_MenuPaso3.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso3Matricula.jpg"
            btn_MenuPaso4.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso4Matricula_D.jpg"

        Catch ex As Exception
            EnvioEmailError(116, ex.ToString)
        End Try
    End Sub

    Protected Sub btn_RetrocederEtapa5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Ck_Etapa4.Enabled = False
            'Cargar_Etapa4()
            Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva
            Dim int_CodigoAnioAcademicoMatricula As Integer = hiddenCodigoAnioAcademico.Value
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

            Dim obj_BL_Matricula As New bl_Matricula
            Dim ds_Lista As DataSet = obj_BL_Matricula.FUN_VAL_AceptacionDocumentoDireccion(int_CodigoFamilia, int_CodigoAnioAcademicoMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            ViewState("DatosFamiliares") = ds_Lista.Tables(1)

            mv_PasosMatricula.ActiveViewIndex = 3
            If Ck_Etapa4.Checked = False Then
                btn_SiguienteEtapa4.Enabled = False
            ElseIf Ck_Etapa4.Checked = True Then
                btn_SiguienteEtapa4.Enabled = True
            End If
            btn_MenuPaso3.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso3Matricula_D.jpg"
            btn_MenuPaso4.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso4Matricula.jpg"
            btn_MenuPaso5.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso5Matricula_D.jpg"
        Catch ex As Exception
            EnvioEmailError(116, ex.ToString)
        End Try
    End Sub

    'Protected Sub btn_RetrocederEtapa6_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    Try
    '        Ck_Etapa5.Enabled = False
    '        Cargar_Etapa5()
    '    Catch ex As Exception
    '        EnvioEmailError(116, ex.ToString)
    '    End Try
    'End Sub

    'Protected Sub btn_RetrocederEtapa7_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    Try
    '        Cargar_Etapa6()
    '    Catch ex As Exception
    '        EnvioEmailError(116, ex.ToString)
    '    End Try
    'End Sub

    Protected Sub btn_SiguienteEtapa1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            RegistrarPasoMatricula(1, hiddenCodigoAnioAcademico.Value, hiddenCodigoAlumno.Value, hiddenCodigoFamiliar.Value, 0)
            Cargar_Etapa2()
        Catch ex As Exception
            EnvioEmailError(116, ex.ToString)
        End Try
    End Sub

    Protected Sub btn_SiguienteEtapa2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            RegistrarPasoMatricula(2, hiddenCodigoAnioAcademico.Value, hiddenCodigoAlumno.Value, hiddenCodigoFamiliar.Value, 0)
            Cargar_Etapa3()
        Catch ex As Exception
            EnvioEmailError(117, ex.ToString)
        End Try

    End Sub

    Protected Sub btn_SiguienteEtapa3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            RegistrarPasoMatricula(3, hiddenCodigoAnioAcademico.Value, hiddenCodigoAlumno.Value, hiddenCodigoFamiliar.Value, 0)
            Dim str_Id As String
            Dim str_Rutamadre As String = "\SaintGeorgeOnline" 'HttpContext.Current.ApplicationInstance.Server.MapPath("/Policy guide English")
            Dim str_ArchLecturaEstructura As String = str_Rutamadre
          
            str_ArchLecturaEstructura = str_Rutamadre & ConfigurationManager.AppSettings.Item("RutaDocumentosMatricula").ToString()
            str_Id = sender.ClientId.ToString

            obtenerScript(str_Id, str_ArchLecturaEstructura)



            Cargar_Etapa4()
        Catch ex As Exception
            EnvioEmailError(118, ex.ToString)
        End Try

    End Sub

    Protected Sub btn_SiguienteEtapa4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            RegistrarPasoMatricula(4, hiddenCodigoAnioAcademico.Value, hiddenCodigoAlumno.Value, hiddenCodigoFamiliar.Value, 1)

            Dim dt As DataTable
            dt = ViewState("DatosFamiliares")

            If dt.Rows(0).Item("CodigoPersona") > 0 Then

                Dim rutamadre As String = ""
                Dim NombreArchivo As String = ""

                Dim reporte_html As String = "Estimado Padre de Familia <br> Se le envía adjunto el documento de Compromiso de los Padres de Familia del Colegio San Jorge de Miraflores el cual fue aceptado por " _
                & " usted durante el proceso de matricula (Paso 4).<br> <br> Atentamente <br> <br> Colegio San Jorge de Miraflores"
                'Contenido del reporte
                Dim arrayCorreo As New ArrayList
                Dim int_cont As Integer = 0

                While int_cont <= dt.Rows.Count - 1
                    arrayCorreo.Add(dt.Rows(int_cont).Item("EmailPersonal"))
                    int_cont = int_cont + 1
                End While

                NombreArchivo = "Compromiso de Padres.pdf"
                rutamadre = ConfigurationManager.AppSettings("RutaDocumentoMatricula_Local").ToString() & NombreArchivo

                Dim arrayCorreo2 As New ArrayList
                arrayCorreo2.Add("fsalinas@sanjorge.edu.pe")
                arrayCorreo2.Add("fanny2710_11@hotmail.com")
                Dim int_EnvioCorreo As Integer

                'Para el envio de todos los correos elegidos
                int_EnvioCorreo = EnvioEmail.SendEmail(arrayCorreo, reporte_html, "Documento de Compromiso de los Padres de Familia del Colegio San Jorge de Miraflores  ", rutamadre)
                'ViewState("DatosFamiliares")
            End If


            'Dim str_Id As String
            'Dim str_Rutamadre As String = "\SaintGeorgeOnline" 'HttpContext.Current.ApplicationInstance.Server.MapPath("/Policy guide English")
            'Dim str_ArchLecturaEstructura As String = str_Rutamadre

            'str_ArchLecturaEstructura = str_Rutamadre & ConfigurationManager.AppSettings.Item("RutaDocumentosMatricula").ToString()
            'str_Id = sender.ClientId.ToString

            'obtenerScript(str_Id, str_ArchLecturaEstructura)

            'validar si esta registrado el paso4 


            Cargar_Etapa5()
            'RegistrarMatricula()
        Catch ex As Exception
            EnvioEmailError(119, ex.ToString)
        End Try

    End Sub

    Protected Sub btn_SiguienteEtapa5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            RegistrarPasoMatricula(5, hiddenCodigoAnioAcademico.Value, hiddenCodigoAlumno.Value, hiddenCodigoFamiliar.Value, 0)
            'Cargar_Etapa6()
            RegistrarMatricula()
            btn_RetrocederEtapa5.Enabled = False
        Catch ex As Exception
            EnvioEmailError(120, ex.ToString)
        End Try

    End Sub

    'Protected Sub btn_SiguienteEtapa6_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    Try
    '        RegistrarPasoMatricula(6, 2011, 1, 1)
    '        Cargar_Etapa7()
    '    Catch ex As Exception
    '        EnvioEmailError(121, ex.ToString)
    '    End Try

    'End Sub

    'Protected Sub btn_SiguienteEtapa7_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    Try
    '        RegistrarPasoMatricula(7, 2011, 1, 1)
    '        RegistrarMatricula()
    '    Catch ex As Exception
    '        EnvioEmailError(122, ex.ToString)
    '    End Try

    'End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Setear permisos de acciones sobre el formulario según la configuración del usuario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(4, 74)
    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(4, 74, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
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

    ''' <summary>
    ''' Carga la informaciín referente a la matricula.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CargarDatosMatricula()
        cargarComboTipoSangre()
    End Sub

    Private Sub cargarComboTipoSangre()

        Dim obj_BL_Clinica As New bl_Clinicas
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Clinica.FUN_LIS_Clinicas("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlClinica1, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlClinica2, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlClinica3, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    Private Sub obtenerScript(ByVal str_Id As String, ByVal str_ArchLecturaEstructura As String)
        Dim str_LinkDocumento As String = ""
        Dim str_Script As String

        Select Case str_Id

            Case "ctl00_ContentPlaceHolder1_btn_SiguienteEtapa3"
                str_LinkDocumento = str_ArchLecturaEstructura & "\DocumentoDireccion\Direccion.swf"
                str_Script = "<embed width='450px' height='400px' fullscreen='yes' src='" & str_LinkDocumento & "' >"
                Doc_Paso4.InnerHtml = str_Script
                'Case "ctl00_ContentPlaceHolder1_btn_SiguienteEtapa4"
                '    str_LinkDocumento = str_ArchLecturaEstructura & "\CondicionesEconomicas\Anexo_2.swf"
                '    str_Script = "<embed width='450px' height='400px' fullscreen='yes' src='" & str_LinkDocumento & "' >"
                '    Doc_Paso5.InnerHtml = str_Script
                'Case "ctl00_ContentPlaceHolder1_btn_SiguienteEtapa4"
                '    str_LinkDocumento = str_ArchLecturaEstructura & "\Seguro\Exoneracion\Anexo_4.swf"
                '    str_Script = "<embed width='450px' height='400px' fullscreen='yes' src='" & str_LinkDocumento & "' >"
                '    Doc_Paso5.InnerHtml = str_Script
        End Select

    End Sub

    Protected Sub Ck_Etapa4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If Ck_Etapa4.Checked = False Then
            btn_SiguienteEtapa4.Enabled = False
        ElseIf Ck_Etapa4.Checked = True Then
            btn_SiguienteEtapa4.Enabled = True
        End If
    End Sub



    ''' <summary>
    ''' Carga la información sobre el Primer paso de la Matrícula.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
       Private Sub Cargar_Etapa1()
        mv_PasosMatricula.ActiveViewIndex = 0

        Dim str_CodigoAlumno As String = hiddenCodigoAlumno.Value
        Dim int_CodigoAnioAcademicoMatricula As Integer = hiddenCodigoAnioAcademico.Value

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim int_codigoanioanterior As Integer = CInt(Me.Master.Obtener_DescripcionPeriodoEscolar) - 1


        'Dim obj_BL_Matricula As New bl_Matricula
        'Dim ds_Lista As DataSet = obj_BL_Matricula.FUN_VAL_RequisitosMatricula(str_CodigoAlumno, int_CodigoAnioAcademicoMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        'View1_Div1.Visible = False

        'DNI
        Dim usp_MensajeDNI As String = miSer.validaDOI(str_CodigoAlumno).strMensaje
        Dim usp_ValorDNI As Integer = miSer.validaDOI(str_CodigoAlumno).strCodigo

        'Situacion Final
        Dim usp_MensajeSF As String = miSer.validacionSituacionFinal(str_CodigoAlumno, int_codigoanioanterior).strMensaje
        Dim usp_ValorSF As Integer = miSer.validacionSituacionFinal(str_CodigoAlumno, int_codigoanioanterior).strCodigo

        'Libros Biblioteca  RESTFULL
        'Dim usp_MensajeLB As String = miSer.validacionDocumentosLibro(str_CodigoAlumno, Me.Master.Obtener_DescripcionPeriodoEscolar).strMensaje
        'Dim usp_ValorLB As Integer = miSer.validacionDocumentosLibro(str_CodigoAlumno, Me.Master.Obtener_DescripcionPeriodoEscolar).strCodigo

        Dim data As String = "{""codigo"":""" & str_CodigoAlumno & """,""anio"":""" & Me.Master.Obtener_DescripcionPeriodoEscolar & """,}"
        Dim metodo As String = "POST"
        Dim URL As String = "getLibros"

        Dim respuesta = Me.ejecutar(URL, data, metodo)

        Dim serializer As New JavaScriptSerializer()
        Dim vObj_E As be_BibliotecaLibros = serializer.Deserialize(Of be_BibliotecaLibros)(respuesta)

        Dim usp_MensajeLB As String = vObj_E.mensaje
        Dim usp_ValorLB As Integer = vObj_E.valor
        'Libros banco de Libros SOAP
        'wsMatricula.bancoLibrosResponse()
        Dim ob As wsMatricula.bancoLibrosResponse = miSer.validacionDocumentosBancoLibro(str_CodigoAlumno, int_codigoanioanterior)

        Dim usp_MensajeBL As String = ob.strMensaje 'miSer.validacionDocumentosBancoLibro(str_CodigoAlumno, int_codigoanioanterior).strMensaje
        Dim usp_ValorBL As Integer = ob.strCodigo 'miSer.validacionDocumentosBancoLibro(str_CodigoAlumno, int_codigoanioanterior).strCodigo

        Dim usp_MensajeFinal As String = ""
        Dim usp_ValorFinal As Integer = 0

        If usp_ValorDNI = 1 Then
            View1_Div1.Visible = True
            View1_lblMensaje0.Text = usp_MensajeDNI
            img_Icono0.ImageUrl = "../../App_Themes/Imagenes/opc_activar.png"
            'btn_SiguienteEtapa1.Visible = True
        ElseIf usp_ValorDNI = 0 Then
            View1_Div1.Visible = True
            View1_lblMensaje0.Text = usp_MensajeDNI
            'btn_SiguienteEtapa1.Visible = False
            img_Icono0.ImageUrl = "../../App_Themes/Imagenes/AlertIcon.gif"
        Else ' Error
            View1_Div1.Visible = False
            'btn_SiguienteEtapa1.Visible = False
        End If


        If usp_ValorSF = 1 Then
            View1_Div1.Visible = True
            View1_lblMensaje1.Text = usp_MensajeSF
            img_Icono1.ImageUrl = "../../App_Themes/Imagenes/opc_activar.png"
            'btn_SiguienteEtapa1.Visible = True
        ElseIf usp_ValorSF = 0 Then
            View1_Div1.Visible = True
            View1_lblMensaje1.Text = usp_MensajeSF
            'btn_SiguienteEtapa1.Visible = False
            img_Icono1.ImageUrl = "../../App_Themes/Imagenes/AlertIcon.gif"
        Else ' Error
            View1_Div1.Visible = False
            'btn_SiguienteEtapa1.Visible = False
        End If

        If usp_ValorLB = 1 Then
            View1_Div1.Visible = True
            View1_lblMensaje2.Text = usp_MensajeLB
            img_Icono2.ImageUrl = "../../App_Themes/Imagenes/opc_activar.png"
            'btn_SiguienteEtapa1.Visible = True
        ElseIf usp_ValorLB = 0 Then
            View1_Div1.Visible = True
            View1_lblMensaje2.Text = usp_MensajeLB
            'btn_SiguienteEtapa1.Visible = False
            img_Icono2.ImageUrl = "../../App_Themes/Imagenes/AlertIcon.gif"
        Else ' Error
            View1_Div1.Visible = False
            'btn_SiguienteEtapa1.Visible = False
        End If


        If usp_ValorBL = 1 Then
            View1_Div1.Visible = True
            View1_lblMensaje3.Text = usp_MensajeBL
            img_Icono3.ImageUrl = "../../App_Themes/Imagenes/opc_activar.png"
            'btn_SiguienteEtapa1.Visible = True
        ElseIf usp_ValorBL = 0 Then
            View1_Div1.Visible = True
            View1_lblMensaje3.Text = usp_MensajeBL
            'btn_SiguienteEtapa1.Visible = False
            img_Icono3.ImageUrl = "../../App_Themes/Imagenes/AlertIcon.gif"

            'If ds_Lista.Tables(4).Rows.Count > 0 Then 'tiene libros prestados
            '    
            Dim dt As DataTable = ObtenerLibrosPrestadoBL(int_codigoanioanterior, str_CodigoAlumno)

            If dt.Rows.Count > 0 Then
                pnl_DetLibros.Visible = True
                GridView2.DataSource = dt
                GridView2.DataBind()
            End If


            'End If

        Else ' Error
            View1_Div1.Visible = False
            'btn_SiguienteEtapa1.Visible = False
        End If


        If usp_ValorDNI = 1 And usp_ValorSF = 1 And usp_ValorLB = 1 And usp_ValorBL = 1 Then
            btn_SiguienteEtapa1.Visible = True
        Else
            btn_SiguienteEtapa1.Visible = False
        End If


    End Sub
    'Metodo Post RESTFULL
    Public Function ejecutar(ByVal extremo As String, ByVal datos As String, ByVal metodo As String) As String

        Dim str_url = "http://localhost:1254/biblioteca.svc/"

        Try
            Dim data As Byte() = UTF8Encoding.UTF8.GetBytes(datos)
            Dim request As HttpWebRequest
            request = WebRequest.Create(str_url + extremo)
            request.Timeout = 10 * 1000
            request.Method = metodo

            request.ContentLength = data.Length
            request.ContentType = "application/json; charset=utf-8"
            Dim postStream = request.GetRequestStream()
            postStream.Write(data, 0, data.Length)

            Dim response As HttpWebResponse = request.GetResponse()
            Dim reader As StreamReader = New StreamReader(response.GetResponseStream)
            Dim json As String = reader.ReadToEnd()
            Return json

        Catch ex As Exception
            Return ""
        End Try

 

    End Function

    Private Function ObtenerLibrosPrestadoBL(ByVal anio As Integer, ByVal str_CodigoAlumno As String) As DataTable

        'Dim int_CodigoAlumno As Integer = hiddenCodigoAlumno.Value
        Dim int_CodigoAnioAcademico As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim dt As DataTable = New DataTable("BancoLibros")
        dt.Columns.Add("Titulo", Type.GetType("System.String"))
        'dt.Columns.Add("TipoLibro", Type.GetType("System.String"))
        'dt.Columns.Add("Curso", Type.GetType("System.String"))
        'dt.Columns.Add("Estado", Type.GetType("System.String"))
        'dt.Columns.Add("PrecioReposicion", Type.GetType("System.String"))

        Dim dr As DataRow
        Dim prestamo() As wsMatricula.beanBancolibroPrestamo

        prestamo = miSer.listaLibroBanco(str_CodigoAlumno, anio).bancoLibros

        If prestamo IsNot Nothing Then
            If prestamo.Length > 0 Then
                For Each num In prestamo

                    dr = dt.NewRow

                    dr.Item("Titulo") = num.libro
                    'dr.Item("TipoLibro") = num.tipoLibro
                    'dr.Item("Curso") = num.curso
                    'dr.Item("Estado") = num.estado
                    'dr.Item("PrecioReposicion") = num.precio
                    dt.Rows.Add(dr)
                Next
            End If
        End If
        Return dt
    End Function
    
    ''' <summary>
    ''' Carga la información sobre el Segundo paso de la Matrícula : Obligaciones Económicas 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        Juan Vento
    ''' Fecha de modificación: 16/12/2011 
    ''' </remarks>
    Private Sub Cargar_Etapa2()

        mv_PasosMatricula.ActiveViewIndex = 1
        btn_MenuPaso1.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso1Matricula_D.jpg"
        btn_MenuPaso2.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso2Matricula.jpg"

        Dim str_CodigoAlumno As String = hiddenCodigoAlumno.Value
        Dim int_CodigoAnioAcademico As Integer = hiddenCodigoAnioAcademico.Value

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obj_BL_Matricula As New bl_Matricula
        Dim ds_Lista As DataSet = obj_BL_Matricula.FUN_VAL_DeudasYPagosMatricula(str_CodigoAlumno, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        View2_Div1.Visible = False
        View2_Div2.Visible = False
        btn_SiguienteEtapa2.Visible = False

        Dim usp_Mensaje As String = ""
        Dim usp_Valor As Integer = 0

        usp_Mensaje = ds_Lista.Tables(0).Rows(0).Item("p_Mensaje")
        usp_Valor = ds_Lista.Tables(0).Rows(0).Item("p_Valor")

        If usp_Valor > 0 Then ' si realizo el pago, muestro los datos del pago

            View2_Div2.Visible = True
            View2_lblMensaje2.Text = usp_Mensaje

            If ds_Lista.Tables.Count > 1 Then ' Si tengo el 2do result set, pinto los datos
                View2_lblPago1.Text = ds_Lista.Tables(1).Rows(0).Item("Concepto")
                View2_lblPago2.Text = ds_Lista.Tables(1).Rows(0).Item("Lugar")
                View2_lblPago3.Text = ds_Lista.Tables(1).Rows(0).Item("Mon") & " " & ds_Lista.Tables(1).Rows(0).Item("Monto")
                View2_lblPago4.Text = ds_Lista.Tables(1).Rows(0).Item("FechaPago")
            End If

            btn_SiguienteEtapa2.Visible = True

        ElseIf usp_Valor = 0 Then

            View2_Div1.Visible = True
            View2_lblMensaje1.Text = usp_Mensaje

        Else ' Error
            View2_Div1.Visible = False
            View2_Div2.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Carga la información sobre el Tercer paso de la Matrícula : Documentos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        Juan Vento 
    ''' Fecha de modificación: 19/12/2011
    ''' </remarks>
    Private Sub Cargar_Etapa3()

        mv_PasosMatricula.ActiveViewIndex = 2
        btn_MenuPaso2.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso2Matricula_D.jpg"
        btn_MenuPaso3.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso3Matricula.jpg"

        Dim str_CodigoAlumno As String = hiddenCodigoAlumno.Value
        Dim int_CodigoAnioAcademico As Integer = hiddenCodigoAnioAcademico.Value

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obj_BL_Matricula As New bl_Matricula
        Dim ds_Lista As DataSet = obj_BL_Matricula.FUN_LIS_DocumentosPasosMatricula(int_CodigoAnioAcademico, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        Dim int_cantidadDoc As Integer = 0

        int_cantidadDoc = ds_Lista.Tables(1).Rows(0).Item("cantidad")

        If int_cantidadDoc > 0 Then
            btn_SiguienteEtapa3.Visible = True
        Else
            btn_SiguienteEtapa3.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Carga la información sobre el Cuarto paso de la Matrícula.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Cargar_Etapa4()
        mv_PasosMatricula.ActiveViewIndex = 3
        Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva
        Dim int_CodigoAnioAcademicoMatricula As Integer = hiddenCodigoAnioAcademico.Value
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obj_BL_Matricula As New bl_Matricula
        Dim ds_Lista As DataSet = obj_BL_Matricula.FUN_VAL_AceptacionDocumentoDireccion(int_CodigoFamilia, int_CodigoAnioAcademicoMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("DatosFamiliares") = ds_Lista.Tables(1)

        If ds_Lista.Tables(0).Rows(0).Item("p_Valor") = 1 Then
            Ck_Etapa4.Checked = True
        Else
            Ck_Etapa4.Checked = False
        End If

        If Ck_Etapa4.Checked = False Then
            btn_SiguienteEtapa4.Enabled = False
          
        ElseIf Ck_Etapa4.Checked = True Then
            btn_SiguienteEtapa4.Enabled = True
        End If

        btn_MenuPaso3.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso3Matricula_D.jpg"
        btn_MenuPaso4.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso4Matricula.jpg"
    End Sub

    ''' <summary>
    ''' Carga la información sobre el Quinto paso de la Matrícula.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Cargar_Etapa5()
        mv_PasosMatricula.ActiveViewIndex = 4

        'If Ck_Etapa5.Checked = False Then
        '    btn_SiguienteEtapa5.Visible = False
        'ElseIf Ck_Etapa5.Checked = True Then
        '    btn_SiguienteEtapa5.Visible = True
        'End If

        lblFecha.Text = Now.Date
        lblNivel.Text = hiddenNivel.Value
        lblGrado.Text = hiddenGrado.Value

        btn_MenuPaso4.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso4Matricula_D.jpg"
        btn_MenuPaso5.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso5Matricula.jpg"
    End Sub

    '''' <summary>
    '''' Carga la información sobre el Sexto paso de la Matrícula.
    '''' </summary>
    '''' <remarks>
    '''' Creador:               Johnatan Matta
    '''' Fecha de Creación:     06/01/2011
    '''' Modificado por:        _____________
    '''' Fecha de modificación: _____________ 
    '''' </remarks>
    'Private Sub Cargar_Etapa6()
    '    mv_PasosMatricula.ActiveViewIndex = 5
    '    btn_MenuPaso5.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso5Matricula_D.jpg"
    '    btn_MenuPaso6.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso6Matricula.jpg"

    '    CargarDatosEmergencia()
    'End Sub

    '''' <summary>
    '''' Carga la información sobre el Septimo paso de la Matrícula.
    '''' </summary>
    '''' <remarks>
    '''' Creador:               Johnatan Matta
    '''' Fecha de Creación:     06/01/2011
    '''' Modificado por:        _____________
    '''' Fecha de modificación: _____________ 
    '''' </remarks>
    'Private Sub Cargar_Etapa7()
    '    mv_PasosMatricula.ActiveViewIndex = 6
    '    btn_MenuPaso6.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso6Matricula_D.jpg"
    '    btn_MenuPaso7.ImageUrl = "/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso7Matricula.jpg"
    'End Sub

    ''' <summary>
    ''' Carga la información de emergencia del alumno a matricular.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CargarDatosEmergencia()

        Dim obj_BL_Matricula As New bl_Matricula
        Dim ds_DatosEmergencia As New DataSet
        Dim dt_DatosFamiliares As New DataTable
        Dim dt_DatosContactoEmergencia As New DataTable
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

        ds_DatosEmergencia = obj_BL_Matricula.FUN_GET_DatosEmergenciaAlumno(1, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 74)

        dt_DatosFamiliares = ds_DatosEmergencia.Tables(0)
        dt_DatosContactoEmergencia = ds_DatosEmergencia.Tables(1)

        dgv_DatosEmergenciaFamiliares.DataSource = dt_DatosFamiliares
        dgv_DatosEmergenciaFamiliares.DataBind()

        dgv_DatosEmergenciaOtros.DataSource = dt_DatosContactoEmergencia
        dgv_DatosEmergenciaOtros.DataBind()
    End Sub

    ''' <summary>
    ''' Registra el Log de pasos de matricula.
    ''' </summary>
    ''' <param name="int_CodigoPasoMatricula">Codigo del paso de la matricula</param>
    ''' <param name="int_PeriodoAcademico">Periodo academico de la matricula</param>
    ''' <param name="int_CodigoAlumno">Codigo del alumno a matricular</param>
    ''' <param name="int_CodigoFamiliar">Codigo del familiar que esta matriculando</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub RegistrarPasoMatricula(ByVal int_CodigoPasoMatricula As Integer, ByVal int_PeriodoAcademico As Integer, ByVal int_CodigoAlumno As Integer, ByVal int_CodigoFamiliar As Integer, ByVal int_AceptacionEtapa As Integer)

        Dim obj_BE_Matricula As New be_Matricula
        Dim obj_BL_Matricula As New bl_Matricula
        Dim int_Resultado As Integer = -1
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

        obj_BE_Matricula.PeriodoAcademico = int_PeriodoAcademico
        obj_BE_Matricula.CodigoPasoMatricula = int_CodigoPasoMatricula
        obj_BE_Matricula.CodigoAlumno = int_CodigoAlumno
        obj_BE_Matricula.CodigoFamiliar = int_CodigoFamiliar
        obj_BE_Matricula.AceptacionEtapa = int_AceptacionEtapa

        int_Resultado = obj_BL_Matricula.FUN_INS_PasoMatricula(obj_BE_Matricula, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 74)
    End Sub

    ''' <summary>
    ''' Registra el proceso de matricula del alumno seleccionado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub RegistrarMatricula()

        Dim obj_BE_Matricula As New be_Matricula
        Dim obj_BL_Matricula As New bl_Matricula

        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

        'Nursery
        'obj_BE_Matricula.CodigoAlumno = "20110405"
        'obj_BE_Matricula.PeriodoAcademico = 1
        'obj_BE_Matricula.CodigoGrado = 1

        'Pre-kinder
        obj_BE_Matricula.CodigoAlumno = hiddenCodigoAlumno.Value
        obj_BE_Matricula.PeriodoAcademico = hiddenCodigoAnioAcademico.Value
        obj_BE_Matricula.CodigoGrado = hiddenCodigoGrado.Value

        usp_valor = obj_BL_Matricula.FUN_INS_Matricula(obj_BE_Matricula, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then ' Si se registra correctamente la matricula 
            'MostrarSexyAlertBox(usp_mensaje, "Info")
            Me.Master.MostrarMensajeAlert(usp_mensaje)
            Response.Redirect("Matricula.aspx")
        Else
            Me.Master.MostrarMensajeAlert(usp_mensaje)
            'MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

#End Region

    Protected Sub btn1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles btn1.Click
        exportar(1)
    End Sub
    Protected Sub btn2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) ' Handles btn2.Click
        exportar(2)
    End Sub
    Protected Sub btn3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles btn3.Click
        exportarZip()
    End Sub

    Private Sub exportar(ByVal int_CodigoDocumento As Integer)
        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        'Dim stream As Stream
        'Dim writer As StreamWriter
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""
        If int_CodigoDocumento = 1 Then
            NombreArchivo = "Reglamento"
        ElseIf int_CodigoDocumento = 2 Then
            NombreArchivo = "condiciones económicas_general"
        ElseIf int_CodigoDocumento = 3 Then
            NombreArchivo = "Declaración_Seguro Particular"
        ElseIf int_CodigoDocumento = 4 Then
            NombreArchivo = "Declaración_No Seguro de Accidentes"
        ElseIf int_CodigoDocumento = 5 Then
            NombreArchivo = "Compromiso de Padres"
        End If

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
    Private Sub exportarZip()
        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        'Dim stream As Stream
        'Dim writer As StreamWriter
        Dim contenido_exportar As String = ""
        Dim str_NombreArchivo1 As String = ""
        Dim str_NombreArchivo2 As String = ""
        Dim str_ruta1 As String = ""
        Dim str_ruta2 As String = ""

        str_NombreArchivo1 = "Declaración_No Seguro de Accidentes.pdf"
        str_NombreArchivo2 = "Declaración_Seguro Particular.pdf"

        Dim downloadBytes1 As Byte()
        Dim downloadBytes2 As Byte()

        str_ruta1 = ConfigurationManager.AppSettings("RutaDocumentoMatricula_Local").ToString() & str_NombreArchivo1
        str_ruta2 = ConfigurationManager.AppSettings("RutaDocumentoMatricula_Local").ToString() & str_NombreArchivo2

        downloadBytes1 = File.ReadAllBytes(str_ruta1)
        downloadBytes2 = File.ReadAllBytes(str_ruta2)

        'Archivo ZIP
        Dim str_FileName As String

        str_FileName = "Declaracion_De_Responsabilidad_Ante_Accidentes"
        'str_ArchivoExcel = "Letras"
        'str_ArchivoWord = "CompromisoPago"

        Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
        Response.Clear()
        Response.BufferOutput = False
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/zip"
        Response.AddHeader("content-disposition", "filename=" & str_FileName + ".zip" & ";")
        Using zip As New ZipFile()
            zip.AddEntry(str_NombreArchivo1, downloadBytes1) 'Archivo Excel
            zip.AddEntry(str_NombreArchivo2, downloadBytes2) 'Archivo Word
            zip.Save(Response.OutputStream)
        End Using
        Response.Close()

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Exportar" Then
                Dim int_CodigoDocumento As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If int_CodigoDocumento = 1 Then ' Reglamento de Condiciones Administrativas y Economicas del Servicio Educativo del Colegio San Jorge 
                    exportar(1)
                ElseIf int_CodigoDocumento = 2 Then ' Condiciones del Contrato de Servicio Educativo que presta el Colegio San Jorge de Miraflores – 2012
                    exportar(2)
                ElseIf int_CodigoDocumento = 3 Then ' Declaración de Responsabilidad ante Accidentes (A - con seguro)
                    exportar(3)
                ElseIf int_CodigoDocumento = 4 Then ' Declaración de Responsabilidad ante Accidentes (B)
                    exportar(4)
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

    End Sub

End Class
