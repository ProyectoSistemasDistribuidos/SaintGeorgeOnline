Imports System.Data
Imports System.Security.Cryptography
Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports System.Web.Services
Imports System.Web.Script.Services
Imports SaintGeorgeOnline_Utilities
Imports System.Net.Mail
Imports System.Configuration.ConfigurationManager
Partial Class Modulo_Actividad_frmActividad
    Inherits System.Web.UI.Page

#Region "Variables"

    Public dtTrabajador As DataTable
    Public lstAnios As List(Of Integer)
    Public lstMeses As List(Of String)
    Public codTrab As Integer = 0
    Public objServer As String
#End Region

    ' 19/03/2013


#Region "Cargar Listas CheckBok"

#Region "Cargar Lista check Grados"


    Private Sub s_listarGrados()
        Try
            Dim dc As New Dictionary(Of String, Object)
            Dim dstGrados As System.Data.DataTable
            Dim nParam As String = "USP_lisGrados"
            dstGrados = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
            chkListGrados.DataTextField = "descripcion"
            chkListGrados.DataValueField = "GD_CodigoGrado"
            chkListGrados.DataSource = dstGrados
            chkListGrados.DataBind()




        Catch ex As Exception

        End Try
    End Sub


#End Region

#Region "Cargar Lista check Destinatarios"
    Private Sub cargarListaCheckDesetinatarios()
        Try
            Dim dc As New Dictionary(Of String, Object)
            Dim dtDestinatario As System.Data.DataTable
            Dim nParam As String = "USP_lisTipoPersonal"
            dtDestinatario = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
            chkListDestinatario.DataTextField = "TPA_Descripcion"
            chkListDestinatario.DataValueField = "TPA_CodigoTipoPersonaActividad"
            chkListDestinatario.DataSource = dtDestinatario
            chkListDestinatario.DataBind()




        Catch ex As Exception
        End Try
    End Sub


#End Region

#Region "cargar lista check asistentes"

    Private Sub S_CagarAsistentes()
        Try
            'chklAsistentes
            Dim dc As New Dictionary(Of String, Object)
            Dim dtAsistentes As System.Data.DataTable
            Dim nParam As String = "USP_lisDocentes"
            dc("TJ_AsistenteAula") = 1
            dc("TJ_Ensenia") = 1

            dtAsistentes = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)


            chklAsistentes.DataTextField = "nombrePersona"
            chklAsistentes.DataValueField = "TJ_CodigoTrabajador"
            chklAsistentes.DataSource = dtAsistentes
            chklAsistentes.DataBind()

        Catch ex As Exception

        End Try
    End Sub



#End Region

#Region "cargar lista docentes "

    Private Sub S_CagarDocentes()
        Try

            Dim dc As New Dictionary(Of String, Object)
            Dim dtDocentes As System.Data.DataTable
            Dim nParam As String = "USP_lisDocentes"
            dc("TJ_AsistenteAula") = 0
            dc("TJ_Ensenia") = 1

            dtDocentes = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)


            chkLDocentes.DataTextField = "nombrePersona"
            chkLDocentes.DataValueField = "TJ_CodigoTrabajador"
            chkLDocentes.DataSource = dtDocentes
            chkLDocentes.DataBind()

        Catch ex As Exception

        End Try
    End Sub



#End Region

#End Region

#Region "cargar listas radio button "
#Region "listar radio button "



    Private Sub S_listarRadioButton()
        Try
            Dim dc As New Dictionary(Of String, Object)
            Dim dtTipoActividad As System.Data.DataTable
            Dim nParam As String = "USP_LisTipoACtividad"
            dtTipoActividad = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
            rbtTipoActividad.DataTextField = "TA_Descripcion"
            rbtTipoActividad.DataValueField = "TA_CodigoTipoActividad"
            rbtTipoActividad.DataSource = dtTipoActividad
            rbtTipoActividad.DataBind()




        Catch ex As Exception

        End Try
    End Sub

#End Region
#End Region

#Region "Cargar  combos"
    Private Sub ListarComboTrabajadores()
        Try
            Dim dc As New Dictionary(Of String, Object)
            dtTrabajador = New DataTable
            Dim nParam As String = "Usp_ListarLT_Trabajadores"
            dtTrabajador = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
            Dim dtView As New DataView(dtTrabajador)
            dtView.Sort = "nombre asc"
            dtTrabajador = dtView.ToTable()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub S_cargarAnios()
        Try
            lstAnios = New List(Of Integer)
            Dim anioAtras As Integer = Year(Date.Now) - 3

            For anioAt As Integer = anioAtras To Year(Date.Now) + 3

                lstAnios.Add(anioAt)
            Next


        Catch ex As Exception

        End Try
    End Sub

    Private Sub S_cargarMeses()
        Try
            lstMeses = New List(Of String)

            lstMeses.Add("Enero")
            lstMeses.Add("Febrero")
            lstMeses.Add("Marzo")
            lstMeses.Add("Abril")
            lstMeses.Add("Mayo")
            lstMeses.Add("Junio")
            lstMeses.Add("Julio")

            lstMeses.Add("Agosto")
            lstMeses.Add("Setiembre")
            lstMeses.Add("Actubre")
            lstMeses.Add("Noviembre")
            lstMeses.Add("Diciembre")



        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Web method"
    ''' <summary>
    ''' funcion para insertar una actividad 
    ''' </summary>
    ''' <param name="dcACtividad">diccionario donde se guarda  la informacion de la actividad</param>
    ''' <returns>indicador de operacion indicando el estado de  la operacion </returns>
    ''' <remarks>desarrollado por salcedo  vila gaylussac </remarks>
    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function FInsertarActividad(ByVal dcACtividad As Dictionary(Of String, Object)) As Object
        Dim dc As New Dictionary(Of Object, Object)
        Try
            '/*************************************************************/
            '/******Validaciones para insertar la actividad ***************/
            '/*************************************************************/

            If CInt(dcACtividad("tipoActividad")) = 0 Then
                dc("codigo") = 0
                dc("mensaje") = "Ingrese el Tipo de actividad"
                Return dc
            End If

            'If CInt(dcACtividad("numeroDocente")) = 0 Then
            '    dc("codigo") = 0
            '    dc("mensaje") = "Ingrese el numero de docentes"
            '    Return dc
            'End If
            'If CInt(dcACtividad("numeroAsistentes")) = 0 Then
            '    dc("codigo") = 0
            '    dc("mensaje") = "Ingrese el numero de asistentes"
            '    Return dc
            'End If
            'If CInt(dcACtividad("numeroAlumnos")) = 0 Then
            '    dc("codigo") = 0
            '    dc("mensaje") = "Ingrese el numero de alumnos"
            '    Return dc
            'End If
            'If CInt(dcACtividad("numeroPadres")) = 0 Then
            '    dc("codigo") = 0
            '    dc("mensaje") = "Ingrese el numero de padres"
            '    Return dc
            'End If

            If dcACtividad("fechaFin").ToString().Trim = "" Then
                dc("codigo") = 0
                dc("mensaje") = "Ingrese la fecha fin "
                Return dc
            End If
            If dcACtividad("fechaInicio").ToString().Trim = "" Then
                dc("codigo") = 0
                dc("mensaje") = "Ingrese la fecha inicio "
                Return dc
            End If
           
            If dcACtividad("objetivo").ToString().Trim = "" Then
                dc("codigo") = 0
                dc("mensaje") = "Ingrese el objetivo"
                Return dc
            End If
            If CInt(dcACtividad("organizador").ToString().Trim) = 0 Then
                dc("codigo") = 0
                dc("mensaje") = "Ingrese el organizador"
                Return dc
            End If
            If dcACtividad("nombreActividad").ToString().Trim = "" Then
                dc("codigo") = 0
                dc("mensaje") = "Ingrese el nombre de la actividad"
                Return dc
            End If

            If CType(dcACtividad("dirigido"), Array).Length = 0 Then
                dc("codigo") = 0
                dc("mensaje") = "Ingrese a quienes va dirijodo la actividad "
                Return dc
            End If

            'If CType(dcACtividad("listaAsistentes"), Array).Length = 0 Then
            '    dc("codigo") = 0
            '    dc("mensaje") = "Ingrese los asistentes"
            '    Return dc
            'End If
            'If CType(dcACtividad("ListaDocentes"), Array).Length = 0 Then
            '    dc("codigo") = 0
            '    dc("mensaje") = "Ingrese los docentes que participaran en la actividad "
            '    Return dc
            'End If

            If CType(dcACtividad("grados"), Array).Length = 0 Then
                dc("codigo") = 0
                dc("mensaje") = "Los grados a quienes va dirijido la actividad"
                Return dc
            End If

            dcACtividad("dirigido") = ToNumeros(CType(dcACtividad("dirigido"), Array))
            dcACtividad("listaAsistentes") = ToNumeros(CType(dcACtividad("listaAsistentes"), Array))
            dcACtividad("ListaDocentes") = ToNumeros(CType(dcACtividad("ListaDocentes"), Array))
            dcACtividad("grados") = ToNumeros(CType(dcACtividad("grados"), Array))

            'primaria inicial  1-8
            'secundaria  9-14

            Dim bien = CType(dcACtividad("grados"), List(Of String)).Where(Function(s) CInt(s) >= 1 And CInt(s) <= 8).Count
            Dim bien2 = CType(dcACtividad("grados"), List(Of String)).Where(Function(s) CInt(s) >= 9 And CInt(s) <= 14).Count

            If bien > 0 And bien2 > 0 Then
                dc("codigo") = 0
                dc("mensaje") = "No puede seleccionar grados pertenecientes  a diferentes niveles  "
                Return dc
            End If

            Dim oBL_Actividad As New BL_Actividad
            dc = oBL_Actividad.F_insertarActividad(dcACtividad)

            Dim mensaje As String = formatoActividades(dc("codigo"))
            Dim lstCorreosGrados As New List(Of Dictionary(Of Object, Object))

            '  Dim lista = CType(dcACtividad("grados"), List(Of String))

            ''-------------------------
            ''listar validadores
            Dim dcValPro As New Dictionary(Of String, Object)
            dcValPro("NivelAprobacion") = 1
            dcValPro("codTrabajador") = 0
            dcValPro("codActividad") = 0

            Dim dtValidadores As New System.Data.DataTable
            Dim nParam As String = "USP_LisCorreosTrabajadorAprobar"

            dtValidadores = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dcValPro, nParam).Tables(0)
            Dim nombresValCorreos = From grVal In (CType(dcACtividad("grados"), List(Of String)).Join(dtValidadores.AsEnumerable(), Function(g) g, _
                                                                                        Function(dcVal) CStr(dcVal("codGrado")), _
                                                                                        Function(gra, val) New With {.nombre = val("nombreAprobar"), _
                                                                                                 .correo = val("correo")})) Group grVal By nombre = grVal.nombre, correo = grVal.correo Into validadores = Group _
                                                                                                 Select New With {.nombre = nombre, .correo = correo}
            ''--------------------------
            Dim listaCorreo As New ArrayList()
            For Each o In nombresValCorreos
                listaCorreo.Add(o.correo)
            Next

            Dim nombres = nombresValCorreos.Aggregate(Function(prev, curr) prev.nombre & " " & curr.nombre)

            Dim estadoEnvio As Integer = 0
            Dim asunto As String = "Creación de actividad."

            Dim cabecera As String = "Estimado(a) " & nombres.nombre & ". <br>" & _
                                     "Se informa que la siguiente actividad ha sido creada para su aprobación de Jefatura de Nivel.<br><br><br>"

            Dim mensajefinal As String = cabecera + mensaje

            'comentado para pruebas 05/03/2013
            estadoEnvio = SendEmail_EnvioPresupuesto(listaCorreo, mensajefinal, asunto, New ArrayList)

            Return dc

        Catch ex As Exception
            dc("mensaje") = ex.Message.ToString()
            dc("codigo") = -1
            Return dc
        End Try
    End Function
#End Region

#Region "Enviar correo "
    Public Shared Function SendEmail_EnvioPresupuesto(ByVal Emails As ArrayList, ByVal str_Cuerpo As String, ByVal str_Asunto As String, ByVal EmailCopia As ArrayList) As Integer

        Dim MailMsg As New MailMessage()
        Dim contEmail As Integer = 0

        Dim encript As New Cripto
        Dim str_SMTPServer As String = ""
        Dim str_EmailSaliente As String = ""
        Dim str_UserSaliente As String = ""
        Dim str_PasswordSaliente As String = ""

        '<add key="SMTP_SERVER" value="dL5A3VdjBJKx3H6kCb+SEXzQ6/xgNf2PDX/ybnoTvno="/>
        '<add key="PORT_SAL" value="R7nag6sx0pQ="/>
        '<add key="EMAIL_FROM_SAL" value="OK1IQ7fyi89SDk3kvXzov/7h+oOPUza+Lk/yHZIuttOW3Q8HfRQn7Q=="/>
        '<add key="USER_SAL" value="OK1IQ7fyi89SDk3kvXzov/7h+oOPUza+Lk/yHZIuttOW3Q8HfRQn7Q=="/>
        '<add key="PASSWORD_SAL" value="czukTH7LmnkyXgInwMtiTQ=="/>
        '<add key="EMAIL_ALERTA" value="jmatta@stgeorgescollege.edu.pe"/>




        str_SMTPServer = "dL5A3VdjBJKx3H6kCb+SEXzQ6/xgNf2PDX/ybnoTvno=" ' Configuration.ConfigurationManager.AppSettings.Item("SMTP_SERVER").ToString()
        str_UserSaliente = "OK1IQ7fyi89SDk3kvXzov/7h+oOPUza+Lk/yHZIuttOW3Q8HfRQn7Q==" ' Configuration.ConfigurationManager.AppSettings.Item("USER_SAL").ToString()
        str_PasswordSaliente = "czukTH7LmnkyXgInwMtiTQ==" 'Configuration.ConfigurationManager.AppSettings.Item("PASSWORD_SAL").ToString()
        str_EmailSaliente = "OK1IQ7fyi89SDk3kvXzov/7h+oOPUza+Lk/yHZIuttOW3Q8HfRQn7Q==" ' Configuration.ConfigurationManager.AppSettings.Item("EMAIL_FROM_SAL").ToString()

        str_SMTPServer = encript.Desencriptar(New RC2CryptoServiceProvider, str_SMTPServer)
        str_UserSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_UserSaliente)
        str_PasswordSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_PasswordSaliente)
        str_EmailSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_EmailSaliente)

        With MailMsg
            .From = New MailAddress(str_EmailSaliente, "Colegio San Jorge - Miraflores")
            .Subject = str_Asunto
            .BodyEncoding = System.Text.Encoding.UTF8
            .Body = str_Cuerpo
            .IsBodyHtml = True
        End With

        Dim SmtpMail As New SmtpClient()
        Dim mCredential As New Net.NetworkCredential(str_UserSaliente, str_PasswordSaliente)
        SmtpMail.Credentials = mCredential
        SmtpMail.Host = str_SMTPServer

        Try
            For Each email As String In Emails
                MailMsg.To.Add(New MailAddress(email, "", System.Text.Encoding.UTF8))
            Next


            For Each email As String In EmailCopia
                MailMsg.CC.Add(New MailAddress(email, "", System.Text.Encoding.UTF8))
            Next


            'Return 0
            SmtpMail.Send(MailMsg)

            Return 1

        Catch ex As Exception

            Return 0

        End Try

    End Function

#End Region


#Region "funciones  "

    Public Shared Function ToNumeros(ByVal lstNum As Array) As List(Of String)
        Dim lstNumeros As New List(Of String)

        Try
            For Each strNum As String In lstNum
                lstNumeros.Add((strNum.Trim()))
            Next

            Return lstNumeros

        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "Eventos pagina "
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'codTrab = Master.Obtener_CodigoUsuarioLogueado

        Me.Master.MostrarTitulo("Activities")
        If Not Page.IsPostBack Then

            codTrab = Master.Obtener_CodigoUsuarioLogueado
            s_listarGrados()
            ListarComboTrabajadores()
            cargarListaCheckDesetinatarios()
            S_CagarAsistentes()
            S_CagarDocentes()
            S_cargarAnios()
            S_cargarMeses()
            S_listarRadioButton()

            objServer = New System.Web.Script.Serialization.JavaScriptSerializer().Serialize(New With {.codUsuario = Me.Master.Obtener_CodigoUsuarioLogueado})
        End If
    End Sub


#End Region

#Region "Busqueda de actividades"
    ''' <summary>
    ''' funcion para crear la interfaz 
    ''' </summary>
    ''' <param name="anio">numero del anio</param>
    ''' <param name="mes">numero del mes </param>
    ''' <param name="fecha1">fecha desde  para filtrar fecha inicio de la actividad </param>
    ''' <param name="fecha2">fecha hasta   para filtrar fecha inicio de la actividad </param>
    ''' <returns>retorna la interfaz de la tabla  </returns>
    ''' <remarks></remarks>
#Region "web methods"

    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function fBuscarActividades(ByVal anio As Integer, ByVal mes As Integer, ByVal fecha1 As String, ByVal fecha2 As String, ByVal codRegistro As Integer) As Object
        Try
            Dim dc As New Dictionary(Of String, Object)
            Dim dtActividadesBusqueda As New System.Data.DataTable
            Dim nParam As String = "USP_LisActividades"
            dc("anio") = anio
            dc("mes") = mes
            dc("fecha1") = fecha1
            dc("fecha2") = fecha2
            dc("codActividad") = 0
            dc("codOrganizador") = 0
            dc("codTrabajador") = codRegistro

            dtActividadesBusqueda = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim coleccionActividad = From sqlObject In dtActividadesBusqueda.AsEnumerable() _
                                     Group sqlObject _
                                     By nombreActividad = sqlObject("nombreActividad"), _
                                        nombreOrganizador = sqlObject("nombreOrganizador"), _
                                        fechaActividad = sqlObject("fechaInicio"), _
                                        nombreTipoActividad = sqlObject("nombreTipoActividad"), _
                                        esEditable = sqlObject("esEditable"), _
                                        esDeletable = sqlObject("esDeletable"), _
                                        nombreImagen = sqlObject("nombreImagen"), _
                                        titulo = sqlObject("titulo"), _
                                        nombreImagenDireccion = sqlObject("nombreImagenDireccion"), _
                                        tituloDireccion = sqlObject("tituloDireccion"), _
                                        codActividad = sqlObject("codActividad") Into detalles = Group _
                                            Select New With { _
                                            .titulo = titulo, _
                                            .nombreImagen = nombreImagen, _
                                            .tituloDireccion = tituloDireccion, _
                                            .nombreImagenDireccion = nombreImagenDireccion, _
                                            .esEditable = esEditable, _
                                            .esDeletable = esDeletable, _
                                            .nombreTipoActividad = nombreTipoActividad, _
                                            .fechaActividad = fechaActividad, _
                                            .nombreActividad = nombreActividad, _
                                            .nombreOragnizador = nombreOrganizador, _
                                            .codActividad = codActividad, _
                                            .grados = ((From dtGrados In detalles Select New Grado With {.nombreGrado = dtGrados("nombreGrado")})).Select(Function(og) og.nombreGrado).Distinct.Select(Function(g) New Grado With {.nombreGrado = g}) _
                                            .Aggregate(New Grado With {.nombreGrado = ""}, Function(gr, gr2) New Grado With {.nombreGrado = gr.nombreGrado & " " & gr2.nombreGrado}), _
                                            .docentes = (From dtDocente In detalles Select New docentes With {.nombreDocente = dtDocente("nombreDocentes")}).Select(Function(d) d.nombreDocente).Distinct.Select(Function(d) New docentes With {.nombreDocente = d}) _
                                            .Aggregate(New docentes With {.nombreDocente = ""}, Function(d, dd) New docentes With {.nombreDocente = d.nombreDocente & ",  " & dd.nombreDocente})}

            Dim xml As New XElement("table", New XAttribute("id", "tablaBusqueda"), New XAttribute("cellpadding", "0"), New XAttribute("cellspacing", "0"), New XAttribute("border", "0"), _
                                    (From oacTividad In coleccionActividad _
                                     Select New XElement("tr", _
                                            New XAttribute("onmouseover", "TiposControlesActualOver(this)"), _
                                            New XAttribute("onmouseout", "TiposControlesActualOut(this)"), _
                                            New XElement("td", New XElement("div", New XAttribute("style", "height:25px;width:245px;font-size:8pt;"), oacTividad.nombreActividad)), _
                                            New XElement("td", New XElement("div", New XAttribute("style", "height:25px;width:175px;font-size:8pt;"), oacTividad.nombreOragnizador)), _
                                            New XElement("td", New XElement("div", New XAttribute("style", "height:25px;width:70px;font-size:8pt;text-align:center;"), oacTividad.nombreTipoActividad)), _
                                            New XElement("td", New XElement("div", New XAttribute("style", "height:25px;width:70px;font-size:8pt;text-align:center;"), oacTividad.fechaActividad)), _
                                            New XElement("td", New XElement("div", New XAttribute("style", "min-height:25px;height:auto;width:140px;font-size:8pt;"), oacTividad.grados.nombreGrado.Remove(0, 1))), _
                                            EdicionFila(oacTividad.esEditable, oacTividad.codActividad), _
                                            EdicionFilaEliminar(oacTividad.esDeletable, oacTividad.codActividad), _
                                            New XElement("td", New XElement("div", New XAttribute("style", "height:25px;width:25px;font-size:8pt;text-align:center"), _
                                                New XElement("img", _
                                                New XAttribute("style", "cursor:pointer;height:18px;width:18px;"), _
                                                New XAttribute("title", "Print"), _
                                                New XAttribute("onclick", "Imprimir( " & oacTividad.codActividad & ")"), _
                                                New XAttribute("src", "../App_Themes/Imagenes/opc_printer.png")))), _
                                            EdicionEstado(oacTividad.nombreImagen, oacTividad.titulo), _
                                            EdicionEstado(oacTividad.nombreImagenDireccion, oacTividad.nombreImagenDireccion), _
                                            InformeActividad(oacTividad.codActividad))))

            Return New With {.html = xml.ToString()}
            '
        Catch ex As Exception

        End Try


    End Function

    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function fRegistrarReporte(ByVal codActividad As Integer) As Object
        Try
            HttpContext.Current.Session("SS_CodigoActividad") = codActividad
            Return codActividad
        Catch ex As Exception
        End Try
    End Function

#End Region
#Region "clase resultado de actividades"
    ''' <summary>
    ''' sirve para crear una coleccion actividades 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class actividad
        Public nombreActividad As String
        Public nombreOrganizador As String
        Public codACtividad As Integer
        Public EnDocentes As IEnumerable(Of docentes)
        Public EnGrados As IEnumerable(Of Grado)

    End Class
    Public Class docentes
        Public nombreDocente As String
        Public codDocentes As Integer
    End Class
    Public Class Grado
        Public nombreGrado As String
        Public codGrado As Integer
    End Class


#End Region
#End Region

#Region "Edicion Actividad"

#Region "Cargar Actividad"
    ''' <summary>
    ''' funcion paara listar la actividad 
    ''' </summary>
    ''' <param name="PcodActividad">cod de registro de la actividad </param>
    ''' <returns>devuelve el regsitro para actualizar la actividad</returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_CargarActividadRegistro(ByVal PcodActividad As Integer) As Object
        Try
            Dim dc As New Dictionary(Of String, Object)
            Dim dtActividadesBusqueda As New System.Data.DataTable
            Dim nParam As String = "USP_LisActividades"

            dc("anio") = 0
            dc("mes") = 0
            dc("fecha1") = ""
            dc("fecha2") = ""
            dc("codActividad") = PcodActividad

            dtActividadesBusqueda = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)


            Dim sqlObject = From sqlActividad In dtActividadesBusqueda.AsEnumerable() Group sqlActividad By _
                                codActividad = sqlActividad("codActividad"), _
                                nombreActividad = sqlActividad("nombreActividad"), _
                                tipoActividad = sqlActividad("tipoActividad"), _
                                nombreTipoActividad = sqlActividad("nombreTipoActividad"), _
                                objetivoActividad = sqlActividad("objetivoActividad"), _
                                nombreOrganizador = sqlActividad("nombreOrganizador"), _
                                CodOrganizador = sqlActividad("codOrganizador"), _
                                fechaInicio = sqlActividad("fechaInicio").ToString(), _
                                fechaFin = sqlActividad("fechaFin").ToString(), _
                                hrFin = sqlActividad("horaFin"), _
                                hrInicio = sqlActividad("horaInicio"), _
                                numeroDocentes = sqlActividad("numeroDocentes"), _
                                numeroPadres = sqlActividad("numeroPadres"), _
                                numeroAlumnos = sqlActividad("numeroAlumnos"), _
                                lugar = sqlActividad("lugar") _
                             Into detalle = Group _
                            Select New With { _
                            .lugar = lugar, _
                            .codActividad = codActividad, _
                            .nombreActividad = nombreActividad, _
                            .tipoActividad = tipoActividad, _
                            .objetivoActividad = objetivoActividad, _
                            .nombreOrganizador = nombreOrganizador, _
                            .CodOrganizador = CodOrganizador, _
                            .fechaInicio = fechaInicio, _
                            .fechaFin = fechaFin, _
                            .hrFin = hrFin.ToString().Substring(10, 6).Trim(), _
                            .hrInicio = hrInicio.ToString().Substring(10, 6).Trim(), _
                            .numeroDocentes = numeroDocentes, _
                            .numeroPadres = numeroPadres, _
                            .numeroAlumnos = numeroAlumnos, _
                            .grados = (From gr In detalle.AsEnumerable() Select gr("codGrado")).Distinct(), _
                            .destinatarios = (From gr In detalle.AsEnumerable() Select gr("tipoPersona")).Distinct(), _
                            .docente = (From gr In detalle.AsEnumerable() Select gr("codDocente")).Distinct()}

            '#2/12/2013 10:12:00 PM#

            Return sqlObject
        Catch ex As Exception

        End Try
    End Function


#End Region
#Region "Eliminar"
    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_Eliminar(ByVal codActividad As Integer) As Object
        Dim dc As New Dictionary(Of Object, Object)

        Try

            Dim oBL_Actividad As New BL_Actividad
            dc = oBL_Actividad.F_eliminarACtividad(codActividad)

            Return dc
        Catch ex As Exception
            dc("mensaje") = ex.Message.ToString()
            dc("codigo") = -1

        End Try
    End Function

#End Region

#End Region
#Region "generar html para enviar  "
    Private Shared Function formatoActividades(ByVal int_CodigoActividad As Integer) As String
        Dim obl_actividad As New BL_Actividad
        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1

        Dim ds_lista As DataSet = obl_actividad.FUN_GET_ActividadImp(int_CodigoActividad, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)

        Dim sb_HTML As New StringBuilder
        sb_HTML.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: 0; width: 650px;'")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='center' valign='middle' colspan='2'><br /><h2>ACTIVITIES COORDINATION SHEET</h2><br /></td></tr>")

        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'>")

        ' fecha y hora
        sb_HTML.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: 0; width: 650px;'>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 450px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 40px; height: 15px;' align='left' valign='middle'><b>DATE:</b></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000;' align='center' valign='middle'><b>FROM:</b></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000; border-right: solid 1px #000000;' align='center' valign='middle'><b>TO:</b></td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 450px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 40px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000;' align='center' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("FechaInicio") & "</td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000; border-right: solid 1px #000000;' align='center' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("FechaFin") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 450px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 40px; height: 15px;' align='left' valign='middle'><b>HOUR:</b></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000;' align='center' valign='middle'><b>FROM:</b></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000; border-right: solid 1px #000000;' align='center' valign='middle'><b>TO:</b></td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 450px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 40px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000; border-bottom: solid 1px #000000;' align='center' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("HoraInicio") & "</td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border: solid 1px #000000;' align='center' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("HoraFin") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("</table>")

        sb_HTML.Append("</td></tr>")

        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Activity</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Type of Activity:</span></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("tipoAct") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Activity:</span></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Actividad") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>For:</span></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'><ul>")
        For Each dr As DataRow In ds_lista.Tables(2).Rows
            sb_HTML.Append("<li>" & dr.Item("dTipo") & "</li>")
        Next
        sb_HTML.Append("</ul></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Organiser</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Organiser:</span></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Organizador") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Subject Area:</span></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'><ul>")
        For Each dr As DataRow In ds_lista.Tables(1).Rows
            sb_HTML.Append("<li>" & dr.Item("dGrado") & "</li>")
        Next
        sb_HTML.Append("</ul></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Participants</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Number of Students:</span></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("NumAlumnos") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Number of Teachers:</span></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("NumProfesores") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Number of Parents:</span></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("NumPadres") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Number of Classroom Assistants:</span></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("NumAsistentesAula") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Requirements</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("ReqTecnologicos") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("ReqLogistica") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 480px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("ReqInfraestructura") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("</td></tr>")
        sb_HTML.Append("</table>")

        Return sb_HTML.ToString
    End Function
#End Region

#Region "funciones crear filas depediendo del estado "
    Public Shared Function EdicionFila(ByVal estado As Integer, ByVal codActividad As Integer) As XElement
        Try
            If estado = 1 Then
                Return New XElement("td", New XElement("div", New XAttribute("style", " height:25px;width:25px;font-size:8pt;text-align:center"), _
                                          New XElement("img", New XAttribute("style", "cursor:pointer;height:18px ;width:18px;"), _
                                                              New XAttribute("title", "Edit"), _
                                                              New XAttribute("onclick", "EditarFilas(" & codActividad & " )"), _
                                                              New XAttribute("src", "../App_Themes/Imagenes/opc_actualizar.png"))))
            Else
                Return New XElement("td", New XElement("div", New XAttribute("style", " height:25px;width:25px;font-size:8pt;text-align:center")))
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function EdicionFilaEliminar(ByVal estado As Integer, ByVal codActividad As Integer) As XElement
        Try
            If estado = 1 Then
                Return New XElement("td", New XElement("div", New XAttribute("style", " height:25px;width:25px;font-size:8pt;text-align:center"), _
                                          New XElement("img", New XAttribute("style", "cursor:pointer;height:18px ;width:18px;"), _
                                                              New XAttribute("title", "Delete"), _
                                                              New XAttribute("onclick", "EliminarActividad(" & codActividad & " )"), _
                                                              New XAttribute("src", "../App_Themes/Imagenes/opc_eliminarv2.png"))))
            Else
                Return New XElement("td", New XElement("div", New XAttribute("style", " height:25px;width:25px;font-size:8pt;text-align:center")))
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function EdicionEstado(ByVal nombreImagen As String, ByVal title As String) As XElement
        Try
            Return New XElement("td", New XElement("div", New XAttribute("style", "height:25px;width:25px;font-size:8pt;text-align:center"), _
                                      New XElement("img", New XAttribute("style", "height:18px;width:18px;"), _
                                                          New XAttribute("title", title), _
                                                          New XAttribute("src", "../App_Themes/Imagenes/" & nombreImagen))))
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function InformeActividad(ByVal codActividad As Integer) As XElement
        Try
            Return New XElement("td", New XElement("div", New XAttribute("style", " height:25px;width:25px;font-size:8pt;text-align:center"), _
                                      New XElement("img", New XAttribute("style", "cursor:pointer;height:18px ;width:18px;"), _
                                                          New XAttribute("title", "Report"), _
                                                          New XAttribute("onclick", "GenerarInforme(" & codActividad & " )"), _
                                                          New XAttribute("src", "../App_Themes/Imagenes/opc_registrar_observacion.png"))))
        Catch ex As Exception
        End Try
    End Function
#End Region
End Class
