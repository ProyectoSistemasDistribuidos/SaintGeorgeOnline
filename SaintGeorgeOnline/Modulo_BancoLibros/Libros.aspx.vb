Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_DataAccess.ModuloBancoLibros
Imports SaintGeorgeOnline_BusinessLogic.ModuloBancoLibros
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing

Imports System.Security.Cryptography
Imports System.Web.Services
Imports System.Configuration.ConfigurationManager
Partial Class Modulo_BancoLibros_Libros
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Mantenimiento de Libros")
            If Not Page.IsPostBack Then

                ViewState("SortExpression") = "Titulo"
                ViewState("Direccion") = "ASC"

                ViewState("SortExpression1") = "NumeroEjemplar"
                ViewState("Direccion1") = "ASC"
                chkAll.Checked = True
                chkAll_CheckedChanged()
                'btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                tbPrecioLibro.Attributes.Add("onkeypress", " ValidarLength(this, 5);")
                tbPrecioLibro.Attributes.Add("onkeyup", " ValidarLength(this, 5);")
                tbPrecioPrestamo.Attributes.Add("onkeypress", " ValidarLength(this, 5);")
                tbPrecioPrestamo.Attributes.Add("onkeyup", " ValidarLength(this, 5);")
                tbPrecioReposicion.Attributes.Add("onkeypress", " ValidarLength(this, 5);")
                tbPrecioReposicion.Attributes.Add("onkeyup", " ValidarLength(this, 5);")
                tbLargo.Attributes.Add("onkeypress", " ValidarLength(this, 6);")
                tbLargo.Attributes.Add("onkeyup", " ValidarLength(this, 6);")
                tbAncho.Attributes.Add("onkeypress", " ValidarLength(this, 6);")
                tbAncho.Attributes.Add("onkeyup", " ValidarLength(this, 6);")
                tbGrosor.Attributes.Add("onkeypress", " ValidarLength(this, 6);")
                tbGrosor.Attributes.Add("onkeyup", " ValidarLength(this, 6);")
                cargarCombos()
                ddlBuscarAnio.SelectedValue = Now.Year
                listar()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCerrarModal_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        hd_Codigo.Value = 0
        pnModalAgregarRegistro.Hide()

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        VerRegistro("Reporte")
        ObtenerUltimoCodigoLibro()
        limpiarCampos()


        ddlCursosLibros.Visible = False
        lbCurso.Visible = False
    End Sub

    Protected Sub btnCancelar_Click()

        miTab1.Enabled = True
        miTab2.Enabled = False
        lbTab2.Text = "Reporte"
        TabContainer1.ActiveTabIndex = 0
        hd_Codigo.Value = 0
        lblObtieneCodigoLibro.Text = 0
        ddlBuscarAnio.Focus()
        limpiarCampos()

    End Sub

    Protected Sub btnVerImagenPoppup_Click()
        lblCabNombreTitulo.Text = tbTitulo.Text.Trim
        img_FotoGrande.ImageUrl = hd_rutaFotoImagen.Value
        pnlModalVerImagen.Show()
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validar(usp_mensaje) Then
                Grabar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        ModalPopupExtender1.Dispose()

    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        limpiarFiltros()

    End Sub

    Protected Sub btn_SubirImagen_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        VerificarImagen()

    End Sub

    Protected Sub rbIdioma_SelectedIndexChanged()
        If rbIdioma.SelectedValue = 4 Then
            tbIdiomaOtro.Enabled = True
            tbIdiomaOtro.BackColor = Color.LightYellow
        Else
            tbIdiomaOtro.Enabled = False
            tbIdiomaOtro.BackColor = Color.Silver
        End If
    End Sub
    Protected Sub rbTipoLibro_SelectedIndexChanged()
        If rbTipoLibro.SelectedValue = 1 Or rbTipoLibro.SelectedValue = 4 Then
            ddlCursosLibros.Visible = True
            lbCurso.Visible = True
            ddlCursosLibros.SelectedValue = 0
        Else
            ddlCursosLibros.Visible = False
            lbCurso.Visible = False
        End If
    End Sub

    Protected Sub chkAll_CheckedChanged()

        If chkAll.Checked = True Then
            chkBuscarGrados.Enabled = False
            limpiarBuscarchkGrados()
        Else
            chkBuscarGrados.Enabled = True
        End If


    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Sube en memoria la imagen a adjuntar
    ''' </summary>
    ''' <returns>Indicador sobre el exito de la operación</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function VerificarImagen() As Boolean
        Dim sFileName As String = ""
        Dim FullFileName As String = ""
        Dim ruta As String = ConfigurationManager.AppSettings.Item("RutaImagenesBancoLibro_Local").ToString()
        Dim valida As Boolean = True
        Dim rutaTemp As String = ConfigurationManager.AppSettings.Item("RutaImagenesBancoLibro_Temp_Local").ToString()
        Dim pesoMaxLibro As String = ConfigurationManager.AppSettings.Item("CantidadPesoMaximoLibro").ToString()
        Dim rutaTempObtener As String = ConfigurationManager.AppSettings.Item("RutaImagenesBancoLibro_Temp_Web").ToString()

        Dim file As HttpPostedFile = tbRutaImagenPortada.PostedFile

        If tbRutaImagenPortada.PostedFile.ContentLength > 0 Then
            If (tbRutaImagenPortada.PostedFile.ContentLength < pesoMaxLibro) Then
                sFileName = System.IO.Path.GetFileName(tbRutaImagenPortada.PostedFile.FileName)
                TbRutaIcono.Text = sFileName

                Dim bm As New Bitmap(tbRutaImagenPortada.PostedFile.InputStream)

                ViewState("ImagenMenu") = bm

                bm.Save(rutaTemp + TbRutaIcono.Text)
                ImgFoto.ImageUrl = rutaTempObtener & TbRutaIcono.Text.Trim()
                hd_rutaFotoImagen.Value = ImgFoto.ImageUrl

            Else
                valida = False
                'MostrarSexyAlertBox("Imagen Adjunta no puede pasar de los 200 Kb", "Alert")
                ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", "alert('Imagen Adjunta no puede pasar de los 200 Kb!!!')", True)

            End If
        Else
            valida = False

            'MostrarSexyAlertBox("Debe seleccionar una imagen!!!", "Alert")
            ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", "alert('Debe seleccionar una imagen!!!')", True)

        End If

        Return valida

    End Function

    ''' <summary>
    ''' Guarda la imagen adjuntada por el usuario para el registro indicado.
    ''' </summary>
    ''' <returns>Indicador de exito de la operación</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function GuardarImagen() As Boolean
        Dim sFileName As String = ""
        Dim FullFileName As String = ""
        Dim ruta As String = ConfigurationManager.AppSettings.Item("RutaImagenesBancoLibro_Local").ToString()
        Dim valida As Boolean = True

        If Not ViewState("ImagenMenu") Is Nothing Then
            Dim bm As Bitmap
            bm = ViewState("ImagenMenu")
            bm.Save(ruta + TbRutaIcono.Text)

        End If

        Return valida
    End Function

    Private Function Extraer(ByVal Path As String, ByVal Caracter As String) As String
        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))

        ' -- Retorna el valor   
        Extraer = ret
    End Function


    Private Function guardarImagenDirectorio(ByVal IdRegistro As Integer) As String
        Try
            Dim sFileDir As String = ConfigurationManager.AppSettings.Item("RutaImagenesBancoLibro_Local").ToString()
            Dim FullFileName As String = ""
            Dim sFileName As String = ""
            Dim obj_BE_Libros As New be_Libros
            Dim obj_BL_Libros As New bl_Libros
            Dim usp_mensaje As String = ""
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

            If Not ViewState("ImagenMenu") Is Nothing Then
                Dim bm As Bitmap
                bm = ViewState("ImagenMenu")

                Dim cont As Integer = 0
                If (Directory.Exists(sFileDir & IdRegistro & " \") = False) Then
                    Directory.CreateDirectory(sFileDir & IdRegistro & "\")
                Else
                    Dim str_NombresFile As String() = Directory.GetFiles(sFileDir & IdRegistro & "\")
                    While cont <= str_NombresFile.Length - 1
                        File.Delete(str_NombresFile(cont))
                        cont = cont + 1
                    End While
                    Directory.CreateDirectory(sFileDir & IdRegistro & "\")
                End If

                FullFileName = sFileDir & IdRegistro & "\"
                bm.Save(FullFileName + IdRegistro.ToString & "." & Extraer(TbRutaIcono.Text, "."))

            End If

            obj_BE_Libros.CodigoLibro = IdRegistro
            obj_BE_Libros.RutaTapa = IdRegistro.ToString & "." & Extraer(TbRutaIcono.Text, ".")
            obj_BL_Libros.FUN_UPD_ImagenLibro(obj_BE_Libros, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)

            Return IdRegistro.ToString & "." & Extraer(TbRutaIcono.Text, ".")

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType(), "erroradj", "<script language='JavaScript' type='text/javascript'>alert('" & ex.Message & "');</script>")
        End Try
    End Function

    ''' <summary>
    ''' Llamada de métodos de los combobox y seteo de las fechas de los formularios.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     07/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombos()
        cargarRadioButonTipoLibro()
        cargarComboAniosAcademicos()
        cargarComboMoneda()
        cargarComboCursoLibro()
        cargarRadioButtonGrado()
        rbIdioma_SelectedIndexChanged()
        'rbTipoLibro_SelectedIndexChanged()
        ddlMonedaPrecioLibro.SelectedValue = 1
        ddlMonedaPrecioPrestamo.SelectedValue = 1
        ddlMonedaPrecioReposicion.SelectedValue = 1
        ddlCursosLibros.SelectedValue = 0
        cargarComboSedesColegio()
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAnio, ds_Lista, "Codigo", "Descripcion", True, True)

    End Sub

    Private Sub cargarComboSedesColegio()

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlSede, ds_Lista, "Codigo", "NombreSede", False, True)

    End Sub
    ''' <summary>
    ''' Carga la información de las monedas
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboMoneda()
        Dim obj_BL_Moneda As New bl_Moneda
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Moneda.FUN_LIS_Moneda("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlMonedaPrecioLibro, ds_Lista, "Codigo", "Simbolo", False, True)
        Controles.llenarCombo(ddlMonedaPrecioPrestamo, ds_Lista, "Codigo", "Simbolo", False, True)
        Controles.llenarCombo(ddlMonedaPrecioReposicion, ds_Lista, "Codigo", "Simbolo", False, True)

    End Sub
    ''' <summary>
    ''' Carga la información de las Ficha Autos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     12/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarRadioButonTipoLibro()
        Dim obj_BL_TipoLibro As New bl_TipoLibro
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TipoLibro.FUN_LIS_TipoLibro("", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        rbTipoLibro.DataSource = ds_Lista.Tables(0)
        rbTipoLibro.DataValueField = "Codigo"
        rbTipoLibro.DataTextField = "Descripcion"
        rbTipoLibro.DataBind()
    End Sub

    ''' <summary>
    ''' Carga la información de Cursos Libros
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     09/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboCursoLibro()
        Dim obj_BL_CursoLibro As New bl_CursosLibros
        Dim int_CodigoTipoUsuario As Integer = Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_CursoLibro.FUN_LIST_CursosLibros("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlCursosLibros, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    ''' <summary>
    ''' Carga la información de Grados
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarRadioButtonGrado()
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        ViewState("ListadoGrado") = ds_Lista.Tables(0)
        'Controles.llenarCombo(ddlCursosLibros, ds_Lista, "Codigo", "Abrev", False, True)
        chkGrados.DataSource = ds_Lista.Tables(0)
        chkGrados.DataValueField = "Codigo"
        chkGrados.DataTextField = "Abrev"
        chkGrados.DataBind()

        chkBuscarGrados.DataSource = ds_Lista.Tables(0)
        chkBuscarGrados.DataValueField = "Codigo"
        chkBuscarGrados.DataTextField = "Abrev"
        chkBuscarGrados.DataBind()
    End Sub

    '''' <summary>
    '''' Habilita el TabPanel del formulario
    '''' </summary>
    '''' <param name="str_Modo">Nombre del label del tabPanel</param>
    '''' <remarks>
    '''' Creador:               Fanny Salinas 
    '''' Fecha de Creación:     13/01/2011
    '''' Modificado por:        _____________
    '''' Fecha de modificación: _____________ 
    '''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String)

        miTab1.Enabled = False
        miTab2.Enabled = True
        lbTab2.Text = str_Modo
        TabContainer1.ActiveTabIndex = 1
        tbTitulo.Focus()

    End Sub

    Private Sub ObtenerUltimoCodigoLibro()
        Dim ds_Lista As DataSet
        Dim obj_BL_Libros As New bl_Libros
        ds_Lista = obj_BL_Libros.FUN_LIS_UltimoCodigoLibro()

        lblCodigoLibroR.Text = ds_Lista.Tables(0).Rows(0).Item("UltimoCodigoLibro")
        'lblObtieneCodigoLibro.Text = lblCodigoLibroR.Text
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

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        If ds_Lista.Tables(0).Rows.Count = 0 Then
            btnExportar.Enabled = False
            rbTipoReporte.Enabled = False
        Else
            btnExportar.Enabled = True
            rbTipoReporte.Enabled = True
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
        Dim int_Periodo As Integer = CInt(ddlBuscarAnio.SelectedValue)
        Dim str_Titulo As String = tbBuscarTitulo.Text.Trim()
        Dim int_Idioma As Integer = CInt(rdBuscarIdioma.SelectedValue)
        Dim str_ISBN As String = tbBuscarISBN.Text.Trim()
        Dim str_Grado As String = "" ' CInt(chkBuscarGrados.SelectedValue)
        Dim int_TipoReporte As Integer = CInt(rbTipoReporte.SelectedValue)
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim int_cont As Integer


        If chkAll.Checked = True Then
            str_Grado = 0
        ElseIf chkAll.Checked = False Then
            While int_cont <= chkGrados.Items.Count - 1
                If chkBuscarGrados.Items(int_cont).Selected = True Then
                    Dim int_codigoGrado As Integer
                    int_codigoGrado = chkBuscarGrados.Items(int_cont).Value
                    str_Grado = str_Grado & "," & int_codigoGrado.ToString
                End If
                int_cont = int_cont + 1
            End While
        End If

        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Libros As New bl_Libros
            ds_Lista = obj_BL_Libros.FUN_LIS_Libros(int_Periodo, str_Titulo, int_Idioma, str_ISBN, str_Grado, int_TipoReporte, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_Libros As New bl_Libros
                ds_Lista = obj_BL_Libros.FUN_LIS_Libros(int_Periodo, str_Titulo, int_Idioma, str_ISBN, str_Grado, int_TipoReporte, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Obtiene la información sobre un registro y lo muestra en el formulario.
    ''' </summary>
    ''' <param name="int_Codigo">Código de Clínicas</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As Integer)

        Dim obj_BL_Libros As New bl_Libros
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_cantTotal As Integer = 0
        Dim int_cantDisponible As Integer = 0
        Dim int_cantUtilizado As Integer = 0
        Dim ds_Lista As DataSet = obj_BL_Libros.FUN_GET_Libros(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)
        Dim dt_Lista As DataTable
        dt_Lista = ds_Lista.Tables(1)
        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoLibro").ToString)
        lblObtieneCodigoLibro.Text = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoLibro").ToString)
        ImgFoto.ImageUrl = ConfigurationManager.AppSettings("RutaImagenesBancoLibro_Web").ToString() & lblObtieneCodigoLibro.Text & "/" & ds_Lista.Tables(0).Rows(0).Item("RutaTapa").ToString
        hd_rutaFotoImagen.Value = ImgFoto.ImageUrl
        lblCodigoLibroR.Text = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoLibro").ToString)
        tbTitulo.Text = ds_Lista.Tables(0).Rows(0).Item("Titulo").ToString
        rbIdioma.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoIdioma").ToString
        If ds_Lista.Tables(0).Rows(0).Item("CodigoTipoLibro").ToString = 0 Then
            rbTipoLibro.ClearSelection()
            ddlCursosLibros.Visible = False
        ElseIf ds_Lista.Tables(0).Rows(0).Item("CodigoTipoLibro").ToString = 1 Then
            rbTipoLibro.SelectedValue = 1
            ddlCursosLibros.Visible = True
        ElseIf ds_Lista.Tables(0).Rows(0).Item("CodigoTipoLibro").ToString = 2 Then
            rbTipoLibro.SelectedValue = 2
            ddlCursosLibros.Visible = False
        ElseIf ds_Lista.Tables(0).Rows(0).Item("CodigoTipoLibro").ToString = 3 Then
            rbTipoLibro.SelectedValue = 3
            ddlCursosLibros.Visible = False
        ElseIf ds_Lista.Tables(0).Rows(0).Item("CodigoTipoLibro").ToString = 4 Then
            rbTipoLibro.SelectedValue = 4
            ddlCursosLibros.Visible = True
        ElseIf ds_Lista.Tables(0).Rows(0).Item("CodigoTipoLibro").ToString = 5 Then
            rbTipoLibro.SelectedValue = 5
            ddlCursosLibros.Visible = False
        End If
        tbAutor.Text = ds_Lista.Tables(0).Rows(0).Item("Autor").ToString
        tbEditorial.Text = ds_Lista.Tables(0).Rows(0).Item("Editorial").ToString
        tbColeccion.Text = ds_Lista.Tables(0).Rows(0).Item("Coleccion").ToString
        tbNivel.Text = ds_Lista.Tables(0).Rows(0).Item("Nivel").ToString
        tbISBN.Text = ds_Lista.Tables(0).Rows(0).Item("ISBN").ToString
        tbEdicion.Text = ds_Lista.Tables(0).Rows(0).Item("Edicion").ToString
        tbPrecioLibro.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("PrecioLibro").ToString = -1, "", ds_Lista.Tables(0).Rows(0).Item("PrecioLibro").ToString)
        ddlMonedaPrecioLibro.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoMonedaPrecioLibro").ToString
        tbPrecioPrestamo.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("PrecioPrestamo").ToString = -1, "", ds_Lista.Tables(0).Rows(0).Item("PrecioPrestamo").ToString)
        ddlMonedaPrecioPrestamo.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoMonedaPrecioPrestamo").ToString
        tbPrecioReposicion.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("PrecioReposicion").ToString = -1, "", ds_Lista.Tables(0).Rows(0).Item("PrecioReposicion").ToString)
        ddlMonedaPrecioReposicion.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoMonedaPrecioReposicion").ToString
        TbRutaIcono.Text = ds_Lista.Tables(0).Rows(0).Item("RutaTapa").ToString
        tbLargo.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("Largo").ToString = 0, "", ds_Lista.Tables(0).Rows(0).Item("Largo").ToString)
        tbAncho.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("Ancho").ToString = 0, "", ds_Lista.Tables(0).Rows(0).Item("Ancho").ToString)
        tbGrosor.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("Grosor").ToString = 0, "", ds_Lista.Tables(0).Rows(0).Item("Grosor").ToString)
        hfTotalRegs1.Value = CInt(ds_Lista.Tables(1).Rows.Count.ToString)
        GV_DatosEjemplares.DataSource = ds_Lista.Tables(1)
        GV_DatosEjemplares.DataBind()
        If ds_Lista.Tables(0).Rows(0).Item("CodigoCurso").ToString <> 0 Then
            ddlCursosLibros.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoCurso").ToString
        End If
        ViewState("listaDatosEjemplares") = ds_Lista.Tables(1)
        ViewState("listaDatosCantNumEjemplares") = ds_Lista.Tables(3).Rows(0).Item("Numero")
        ViewState("ListaGrados") = ds_Lista.Tables(4)

        While int_cantTotal <= dt_Lista.Rows.Count - 1
            If dt_Lista.Rows(int_cantTotal).Item("CodigoDisponible") = False Then
                int_cantDisponible = int_cantDisponible + 1
            ElseIf dt_Lista.Rows(int_cantTotal).Item("CodigoDisponible") = True Then
                int_cantUtilizado = int_cantUtilizado + 1
            End If
            int_cantTotal = int_cantTotal + 1
        End While

        'seteo los grados
        If ViewState("ListaGrados") IsNot Nothing Then
            setearDetalles(ds_Lista.Tables(4), chkGrados)
        End If

        ddlSede.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoSede").ToString

        tbTotal.Text = int_cantTotal
        tbDisponible.Text = int_cantDisponible
        tbUtilizado.Text = int_cantUtilizado

        VerRegistro("Reporte")

    End Sub

    Private Sub setearDetalles(ByVal dt As DataTable, ByVal listBox As Object)
        Dim contObj As Integer = 0
        Dim contTabla As Integer = 0

        While contObj <= listBox.Items.Count - 1
            While contTabla <= dt.Rows.Count - 1
                If listBox.Items(contObj).Value = dt.Rows(contTabla).Item(1) Then
                    listBox.Items(contObj).Selected = True
                End If
                contTabla = contTabla + 1
            End While
            contObj = contObj + 1
            contTabla = 0
        End While

    End Sub


    Private Sub VerListadoTotal(ByVal int_Codigo As Integer)

        Dim obj_BL_CopiaLibros As New bl_CopiaLibros
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_CopiaLibros.FUN_GET_CopiaLibros(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)

        pnlModalListadoCopiaLibros.Show()

    End Sub

    ''' <summary>
    ''' Llama al metodo de Eliminar o Activar según la acción seleccionada.
    ''' </summary>
    ''' <param name="int_Codigo">codigo de Clínicas</param>
    ''' <param name="str_accion">tipo de acción a realizar (Activar o Eliminar)</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_BL_Libros As New bl_Libros
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_Libros.FUN_DEL_Libros(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        listar()

    End Sub

    ''' <summary>
    ''' Graba o Actualiza el registro indicado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_Libros As New be_Libros
        Dim obj_BE_CopiaLibros As New be_CopiaLibros
        Dim obj_BL_Libros As New bl_Libros
        Dim obj_BL_CopiaLibros As New bl_CopiaLibros
        Dim BoolGrabar As Integer = lblObtieneCodigoLibro.Text
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim usp_valorDetalle As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        '1. REGISTRO DE LIBRO - CABECERA
        obj_BE_Libros.Titulo = tbTitulo.Text.Trim
        obj_BE_Libros.Autor = tbAutor.Text.Trim
        obj_BE_Libros.Editorial = tbEditorial.Text.Trim
        obj_BE_Libros.Coleccion = tbColeccion.Text.Trim
        obj_BE_Libros.Nivel = tbNivel.Text.Trim
        obj_BE_Libros.ISBN = tbISBN.Text.Trim
        obj_BE_Libros.PrecioLibro = IIf(tbPrecioLibro.Text.Trim = "", -1, tbPrecioLibro.Text.Trim)
        obj_BE_Libros.PrecioPrestamo = IIf(tbPrecioPrestamo.Text.Trim = "", -1, tbPrecioPrestamo.Text.Trim)
        obj_BE_Libros.PrecioReposicion = IIf(tbPrecioReposicion.Text.Trim = "", -1, tbPrecioReposicion.Text.Trim)
        obj_BE_Libros.RutaTapa = ""
        obj_BE_Libros.NumeroCopia = IIf(tbTotal.Text.Trim = "", 0, tbTotal.Text.Trim)
        obj_BE_Libros.CodigoMonedaPrecioLibro = ddlMonedaPrecioLibro.SelectedValue()
        obj_BE_Libros.CodigoMonedaPrecioPrestamo = ddlMonedaPrecioPrestamo.SelectedValue()
        obj_BE_Libros.CodigoMonedaPrecioReposicion = ddlMonedaPrecioReposicion.SelectedValue()
        obj_BE_Libros.CodigoIdioma = rbIdioma.SelectedValue()
        obj_BE_Libros.Largo = IIf(tbLargo.Text.Trim = "", 0, tbLargo.Text.Trim)
        obj_BE_Libros.Ancho = IIf(tbAncho.Text.Trim = "", 0, tbAncho.Text.Trim)
        obj_BE_Libros.Grosor = IIf(tbGrosor.Text.Trim = "", 0, tbGrosor.Text.Trim)
        obj_BE_Libros.Edicion = tbEdicion.Text.Trim
        obj_BE_Libros.Sede = ddlSede.SelectedValue

        If rbTipoLibro.SelectedValue = 1 Then
            obj_BE_Libros.CodigoTipoLibro = rbTipoLibro.SelectedValue()
            obj_BE_Libros.CodigoCurso = ddlCursosLibros.SelectedValue()
        Else
            obj_BE_Libros.CodigoTipoLibro = rbTipoLibro.SelectedValue()
            obj_BE_Libros.CodigoCurso = 0
        End If
        Dim int_cont As Integer = 0
        Dim dt_Grado As DataTable
        dt_Grado = ViewState("ListadoGrado")


        If BoolGrabar = 0 Then
            usp_valor = obj_BL_Libros.FUN_INS_Libros(obj_BE_Libros, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)
            'GuardarImagen()
            guardarImagenDirectorio(usp_valor)
           
        Else
            obj_BE_Libros.CodigoLibro = CInt(BoolGrabar)
            usp_valor = obj_BL_Libros.FUN_UPD_Libros(obj_BE_Libros, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)
            'GuardarImagen()
            guardarImagenDirectorio(usp_valor)
        End If

        If usp_valor > 0 Then

            'elimino el detalle del grado
            obj_BL_Libros.FUN_DEL_AsignacionGradosLibros(usp_valor, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)

            'registro el detalle del Grado
            While int_cont <= chkGrados.Items.Count - 1
                If chkGrados.Items(int_cont).Selected = True Then
                    Dim int_codigoGrado As Integer
                    int_codigoGrado = chkGrados.Items(int_cont).Value
                    obj_BL_Libros.FUN_INS_AsignacionGradosLibros(usp_valor, int_codigoGrado, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)
                End If
                int_cont = int_cont + 1
            End While

            '2. REGISTRO DE LIBRO DETALLE

            Dim dt_listaDetalleLibro As New DataTable 'se lista todo de la grilla actual
            dt_listaDetalleLibro = ViewState("listaDatosEjemplares")

            Dim dt_listaEliminar As New DataTable 'se lista todo de la grilla actual
            dt_listaEliminar = ViewState("ListaDatosEliminados")

            Dim int_codigo As Integer = 0
            Dim int_codigoEli As Integer = 0

            If ViewState("listaDatosEjemplares") IsNot Nothing Then

                'If dt_listaDetalleCompromisoPagoActualizarBD.Rows.Count > 0 Then

                For Each drv As DataRow In dt_listaDetalleLibro.Rows
                    'codigo = drv.Item("codigoDetalleCompromisoPago")
                    obj_BE_CopiaLibros.CodigoLibro = usp_valor
                    obj_BE_CopiaLibros.CodigoBarra = drv.Item("CodigoBarra")
                    obj_BE_CopiaLibros.CodigoEjemplar = drv.Item("CodigoEjemplar")
                    obj_BE_CopiaLibros.NumeroPago = drv.Item("NumeroPago")
                    obj_BE_CopiaLibros.FechaCompraDt = drv.Item("FechaCompra")

                    If drv.Item("TipoDato") = "T" Then
                        usp_valorDetalle = obj_BL_CopiaLibros.FUN_INS_CopiaLibros(obj_BE_CopiaLibros, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)
                    ElseIf drv.Item("TipoDato") = "A" Then
                        obj_BE_CopiaLibros.CodigoCopiaLibro = drv.Item("CodigoCopiaLibro")
                        usp_valorDetalle = obj_BL_CopiaLibros.FUN_UPD_CopiaLibros(obj_BE_CopiaLibros, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)
                    End If
                Next


                If ViewState("ListaDatosEliminados") IsNot Nothing Then
                    For Each drv1 As DataRow In dt_listaEliminar.Rows
                        int_codigoEli = drv1.Item("CodigoCopiaLibro")
                        obj_BL_CopiaLibros.FUN_DEL_CopiaLibros(int_codigoEli, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 42)
                    Next
                End If

            End If
        End If


        If usp_valor > 0 And usp_valorDetalle > -1 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnCancelar_Click()
            If BoolGrabar = 0 Then
                limpiarCampos()
            Else
                limpiarCampos()

            End If
            limpiarCampos()
            limpiarFiltros()
            listar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

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

        If tbTitulo.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Título")
            result = False
        End If

        If tbAutor.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Autor")
            result = False
        End If

        If tbEditorial.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Editorial")
            result = False
        End If

        If tbPrecioLibro.Text.Trim.Length > 0 Then
            If ddlMonedaPrecioLibro.SelectedValue() = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Moneda de Precio de Libro")
                result = False
            End If
        End If

        If tbPrecioPrestamo.Text.Trim.Length > 0 Then
            If ddlMonedaPrecioPrestamo.SelectedValue() = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Moneda de Precio de Prestamo")
                result = False
            End If
        End If

        If tbPrecioReposicion.Text.Trim.Length > 0 Then
            If ddlMonedaPrecioReposicion.SelectedValue() = 0 Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Moneda de Precio de Reposición")
                result = False
            End If
        End If

        If Validacion.ValidarCamposIngreso(tbTitulo) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Titulo")
            result = False
        End If

        If Validacion.ValidarCamposIngreso(tbAutor) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Autor")
            result = False
        End If

        If Validacion.ValidarCamposIngreso(tbEditorial) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Editorial")
            result = False
        End If

        If rbTipoLibro.SelectedValue() = "" Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 12, "Tipo de Libro")
            result = False
        End If

        If ddlCursosLibros.Visible = True And ddlCursosLibros.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Curso Libros")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Function validarDetalle(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If tbTitulo.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Título")
            result = False
        End If

        If tbTitulo.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Autor")
            result = False
        End If

        If tbTitulo.Text.Trim.Length < 2 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 27, "Titulo")
            result = False
        End If

        If tbAutor.Text.Trim.Length < 2 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 27, "Autor")
            result = False
        End If

        If rbTipoReporte.SelectedIndex - 1 Then

            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Libro")
            result = False
        End If

        If ddlSede.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Sede")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

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
    ''' Limpia los filtros de búsqueda del formulario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks> 
    Private Sub limpiarFiltros()

        ddlBuscarAnio.SelectedValue = 0
        tbBuscarTitulo.Text = ""
        tbBuscarISBN.Text = ""
        rdBuscarIdioma.SelectedValue = 0
        chkAll.Checked = True
        limpiarBuscarchkGrados()
        chkAll_CheckedChanged()
        'chkBuscarGrados.SelectedValue = True
        'rbTipoLibro.SelectedValue = -1
        ddlBuscarAnio.Focus()
    End Sub

    Private Sub limpiarBuscarchkGrados()
        Dim int_cont As Integer
        While int_cont <= chkBuscarGrados.Items.Count - 1
            chkBuscarGrados.Items(int_cont).Selected = False
            int_cont = int_cont + 1
        End While
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
    ''' Limpia los campos del formulario de registro.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        Edgar Chang
    ''' Fecha de modificación: 09/08/2011
    ''' </remarks>
    Private Sub limpiarCampos()
        lblObtieneCodigoLibro.Text = 0
        hd_Codigo.Value = 0
        tbTitulo.Text = ""
        tbAutor.Text = ""
        tbEditorial.Text = ""
        tbColeccion.Text = ""
        tbEdicion.Text = ""
        tbNivel.Text = ""
        tbISBN.Text = ""
        tbIdiomaOtro.Text = ""
        rbIdioma.SelectedValue = 2
        tbPrecioLibro.Text = ""
        tbPrecioPrestamo.Text = ""
        tbPrecioReposicion.Text = ""
        TbRutaIcono.Text = ""
        tbTotal.Text = ""
        tbDisponible.Text = ""
        tbUtilizado.Text = ""
        tbCantidadLibros.Text = 0
        ddlMonedaPrecioLibro.SelectedValue = 0
        ddlMonedaPrecioPrestamo.SelectedValue = 0
        ddlMonedaPrecioReposicion.SelectedValue = 0
        ImgFoto.ImageUrl = ConfigurationManager.AppSettings("RutaImagenesBancoLibro_Web").ToString() & "noPhoto.gif"
        hd_rutaFotoImagen.Value = ImgFoto.ImageUrl
        tbAncho.Text = ""
        tbLargo.Text = ""
        tbGrosor.Text = ""
        ddlSede.SelectedValue = 0
        Dim dt As DataTable

        dt = New DataTable("listaDatosEjemplares")
        dt = Datos.agregarColumna(dt, "TipoDato", "String")
        dt = Datos.agregarColumna(dt, "CodigoCopiaLibro", "String")
        dt = Datos.agregarColumna(dt, "CodigoLibro", "String")
        dt = Datos.agregarColumna(dt, "CodigoBarra", "String")
        dt = Datos.agregarColumna(dt, "CodigoEjemplar", "String")
        dt = Datos.agregarColumna(dt, "Disponible", "String")
        dt = Datos.agregarColumna(dt, "estado", "String")

        GV_DatosEjemplares.DataSource = dt
        GV_DatosEjemplares.DataBind()

        Dim int_cont As Integer
        While int_cont <= chkGrados.Items.Count - 1
            chkGrados.Items(int_cont).Selected = False
            int_cont = int_cont + 1
        End While

        ViewState.Remove("ListaGrados")
        ViewState.Remove("listaDatosEjemplares")
        ViewState.Remove("ListaDatosEliminados")
        ViewState.Remove("listaDatosCantNumEjemplares")
        ViewState.Remove("ImagenMenu")

        rbTipoLibro.ClearSelection()
        rbTipoLibro.SelectedIndex = -1
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
    ''' Fecha de Creación:     13/01/2011
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
    ''' Fecha de Creación:     13/01/2011
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
    ''' Fecha de Creación:     13/01/2011
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
    ''' Fecha de Creación:     13/01/2011
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

    ''' <summary>
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting()

        Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting"), ImageButton)

        If ViewState("Direccion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP.png"
            _btnSorting.ToolTip = "Ascendente"
        End If

    End Sub

#End Region

#Region "Eventos del Gridview"

    ''' <summary>
    ''' Selecciona el índice de una página y se visualiza en el listado
    ''' </summary>
    ''' <param name="sender">Es un objeto que hace referencia al combo que contiene la paginación de la lista</param>
    ''' <param name="e">Es un evento del objeto</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
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
            'ImagenSorting()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Or e.CommandName = "Total" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    obtener(codigo)
                ElseIf e.CommandName = "Eliminar" Then
                    cambiarEstado(codigo, "Eliminar")
                ElseIf e.CommandName = "Total" Then
                    VerListadoTotal(codigo)

                End If

            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
        Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
        Dim btnVerFoto As HtmlAnchor = e.Row.FindControl("btnVerPortada")
        Dim lblCodigoLibro As Label = e.Row.FindControl("lblCodigoLibro")
        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = GridView1.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            ''SETEO DE PERMISOS DE ACCIONES---------------
            'Master.BloqueoControles(btnEliminar, 1)
            'Master.BloqueoControles(btnActualizar, 1)
            'Master.BloqueoControles(btnActivar, 1)
            ''---------------------------------------------

            If e.Row.DataItem("Estado") = "Activo" Then
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")

                'Master.SeteoPermisosAcciones(btnEliminar, 42)
                'Master.SeteoPermisosAcciones(btnActualizar, 42)
                'Else
                '    btnActivar.Attributes.Add("OnClick", "return confirm_activar();")
                '    btnActualizar.Visible = False
                '    btnEliminar.Visible = False
                '    e.Row.ForeColor = Drawing.Color.DarkRed

                '    Master.SeteoPermisosAcciones(btnActivar, 42)
            End If
            btnVerFoto.Attributes.Add("rel", "sexylightbox")

            btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaImagenesBancoLibro_Web").ToString() & lblCodigoLibro.Text & "/" & e.Row.DataItem("RutaPortada")

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
            'ImagenSorting()
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

            'ImagenSorting()

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

#Region "Metodos del detalle GV_DatosEjemplar"

    ''' <summary>
    ''' Valida los campos del formulario antes de proceder a "Grabar" un ejemplar
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     30/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarGrabarEjemplar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If IIf(tbCantidadLibros.Text.Trim.Length = 0, 0, tbCantidadLibros.Text.Trim) <= 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 26, "Cantidad de Libros")
            result = False
        End If

        If tbFechaCompra.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Fecha Compra")
            result = False
        Else
            If IsDate(tbFechaCompra.Text.Trim) = False Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha Compra")
                result = False
            End If
        End If

        If tbNumeroPago.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Número de Pago")
            result = False
        End If

        
        'If tbCodigoBarra.Text.Trim.Length = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Código de Barra")
        '    result = False
        'End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Protected Sub btnGrabarDetalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim usp_mensaje As String = ""
        If validarGrabarEjemplar(usp_mensaje) Then
            Dim cont As Integer = 0

            If rbTipoLibro.SelectedIndex = -1 Then
                MostrarSexyAlertBox("Debe seleccionar el tipo de libro", "Alert")
                pnModalAgregarRegistro.Hide()
                Exit Sub
            End If

            While cont <= CInt(IIf(tbCantidadLibros.Text.Trim = "", 0, tbCantidadLibros.Text.Trim)) - 1
                GrabarIngresoDetalle()
                cont = cont + 1
            End While
            pnModalAgregarRegistro.Hide()
            tbCantidadLibros.Text = 0
            tbNumeroPago.Text = ""
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
            pnModalAgregarRegistro.Show()
        End If

    End Sub

    Protected Sub btnCancelarDetalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCancelarDetalle.Click
        pnModalAgregarRegistro.Hide()
        'tbCodigoEjemplar.Text = 0
        'tbCodigoBarra.Text = 0
    End Sub

    Protected Sub btnCerraListadoCopiaLibros_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        pnlModalListadoCopiaLibros.Hide()
    End Sub

    Protected Sub btn_Add_CopiaLibro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("GV_DatosEjemplares") = True
        Dim usp_mensaje As String = ""

        If validarDetalle(usp_mensaje) Then
            tbCantidadLibros.Text = 0
            tbFechaCompra.Text = Today
            tbNumeroPago.Text = ""
            pnModalAgregarRegistro.Show()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    Private Sub obtenerDatosEjemplares(ByVal int_codigoCopiaLibro As Integer, ByVal str_disponible As String, ByVal int_codigoEjemplar As String, ByVal int_CodigoBarra As Integer)

        hd_CodigoDetalle.Value = int_codigoCopiaLibro
        'hd_Disponible.Value = str_disponible
        'tbCodigoEjemplar.Text = int_codigoEjemplar
        'tbCodigoBarra.Text = 0
        pnModalAgregarRegistro.Show()

    End Sub

    Private Function ValidarCamposIngresoPalabras(ByVal str_CampoIngreso As String) As String

        Dim texto As String = ""
        Dim cont As Integer = 0
        Dim alert As Boolean = True
        Dim cont_palabra As Integer = 0
        Dim palabra As String = ""

        While cont <= str_CampoIngreso.Length - 1

            palabra = str_CampoIngreso.Substring(cont, 1)


            If palabra = " " Then
                cont_palabra = 0
            Else
                cont_palabra = cont_palabra + 1
                texto &= palabra
            End If

            If cont_palabra = 2 Then
                Exit While
            End If

            cont = cont + 1
        End While

        Return texto

    End Function

    Private Sub GrabarIngresoDetalle()

        Dim obj_BE_CopiaLibros As New be_CopiaLibros
        Dim obj_BL_CopiaLibros As New bl_CopiaLibros

        Dim BoolGrabar As Integer = lblObtieneCodigoLibro.Text
        Dim usp_mensaje As String = ""
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim cont_Registrados As Integer = 0
        Dim dt_listaDetalle As New DataTable
        Dim int_cantidadD As Integer = 0
        Dim int_cantidadU As Integer = 0
        Dim id_codigo_fila As Integer = 0
        Dim id_num_ejemplar As Integer = 0

        'Asigno el valor del alumno seleccionado
        Dim str_CodigoCopiaLibro As String = hd_CodigoDetalle.Value
        Dim dt As DataTable
        Dim int_cont As Integer = 0
        Dim boolIncremento As Boolean


        If ViewState("listaDatosEjemplares") Is Nothing Then
            dt = New DataTable("listaDatosEjemplares")
            dt = Datos.agregarColumna(dt, "TipoDato", "String")
            dt = Datos.agregarColumna(dt, "CodigoCopiaLibro", "String")
            dt = Datos.agregarColumna(dt, "CodigoLibro", "String")
            dt = Datos.agregarColumna(dt, "CodigoBarra", "String")
            dt = Datos.agregarColumna(dt, "CodigoEjemplar", "String")
            dt = Datos.agregarColumna(dt, "NumeroEjemplar", "Integer")
            dt = Datos.agregarColumna(dt, "Disponible", "String")
            dt = Datos.agregarColumna(dt, "estado", "String")
            dt = Datos.agregarColumna(dt, "FechaCompra", "Datetime")
            dt = Datos.agregarColumna(dt, "FechaCompraStr", "String")
            dt = Datos.agregarColumna(dt, "NumeroPago", "String")
        Else
            dt = ViewState("listaDatosEjemplares")
        End If



        'Codigo ejemplar
        If dt.Rows.Count > 0 Then
            For Each auxdr As DataRow In dt.Rows
                id_codigo_fila = auxdr.Item("CodigoCopiaLibro").ToString()
            Next
        End If

        Dim str_codigoBarra As String = ""
        Dim codigoGenerado As String = ""
        Dim titulo As String = tbTitulo.Text.Trim
        Dim autor As String = tbAutor.Text.Trim
        If ViewState("listaDatosCantNumEjemplares") Is Nothing Then
            id_num_ejemplar = 0
        Else
            id_num_ejemplar = ViewState("listaDatosCantNumEjemplares")
        End If

        Dim codigoAuto As String = CStr(id_num_ejemplar + 1)
        Dim str_InicialtipoLibro As String = ""
        Dim str_InicialtipoLibroCB As String = ""
        Dim str_sede As String = ""

       
        If rbTipoLibro.SelectedValue = 1 Then
            str_InicialtipoLibro = "T"
            str_InicialtipoLibroCB = "TX"
        ElseIf rbTipoLibro.SelectedValue = 2 Then
            str_InicialtipoLibro = "P"
            str_InicialtipoLibroCB = "PL"
        ElseIf rbTipoLibro.SelectedValue = 3 Then
            str_InicialtipoLibro = "R"
            str_InicialtipoLibroCB = "RE"
        ElseIf rbTipoLibro.SelectedValue = 4 Then
            str_InicialtipoLibro = "W"
            str_InicialtipoLibroCB = "WB"
        ElseIf rbTipoLibro.SelectedValue = 5 Then
            str_InicialtipoLibro = "L"
            str_InicialtipoLibroCB = "LE"
        End If

        If ddlSede.SelectedValue = 1 Then
            str_sede = "1"
        ElseIf ddlSede.SelectedValue = 2 Then
            str_sede = "2"
        End If

        'codigoGenerado = ValidarCamposIngresoPalabras(titulo).Substring(0, 2) & ValidarCamposIngresoPalabras(autor).Substring(0, 2) & "_" & codigoAuto
        codigoGenerado = str_InicialtipoLibro + "-" + ObtenerCadenaCodigoLibro(lblCodigoLibroR.Text.Trim, 1) + "/" + ObtenerCadenaCodigoLibro(codigoAuto, 2)
        str_codigoBarra = str_sede + str_InicialtipoLibroCB + ObtenerCadenaCodigoLibro(lblCodigoLibroR.Text.Trim, 1) + "-" + ObtenerCadenaCodigoLibro(codigoAuto, 2)

        If boolIncremento = False Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("TipoDato") = "T"
            dr.Item("CodigoCopiaLibro") = id_codigo_fila + 1
            dr.Item("CodigoLibro") = lblCodigoLibroR.Text
            dr.Item("CodigoBarra") = str_codigoBarra 'tbCodigoBarra.Text.Trim
            dr.Item("CodigoEjemplar") = codigoGenerado
            dr.Item("NumeroEjemplar") = codigoAuto
            dr.Item("Disponible") = "Disponible"
            dr.Item("estado") = "Activo"
            dr.Item("FechaCompra") = tbFechaCompra.Text.Trim
            dr.Item("FechaCompraStr") = tbFechaCompra.Text.Trim
            dr.Item("NumeroPago") = tbNumeroPago.Text.Trim
            dt.Rows.Add(dr)
        End If

        While int_cont <= dt.Rows.Count - 1
            If dt.Rows(int_cont).Item("Disponible").Equals("Disponible") Then
                int_cantidadD = int_cantidadD + 1
            Else
                int_cantidadU = int_cantidadU + 1
            End If
            int_cont = int_cont + 1
        End While

        tbTotal.Text = int_cont
        hfTotalRegs1.Value = tbTotal.Text
        tbDisponible.Text = int_cantidadD
        tbUtilizado.Text = int_cantidadU

        ViewState("listaDatosEjemplares") = dt
        ViewState("listaDatosCantNumEjemplares") = codigoAuto
        GV_DatosEjemplares.DataSource = dt
        GV_DatosEjemplares.DataBind()

    End Sub

    Private Function ObtenerCadenaCodigoLibro(ByVal int_codigo As Integer, ByVal int_tipo As Integer) As String
        Dim str_texto As String = ""

        If int_tipo = 1 Then

            If int_codigo.ToString.Length = 1 Then
                str_texto = "000" + int_codigo.ToString
            ElseIf int_codigo.ToString.Length = 2 Then
                str_texto = "00" + int_codigo.ToString
            ElseIf int_codigo.ToString.Length = 3 Then
                str_texto = "0" + int_codigo.ToString
            Else
                str_texto = int_codigo.ToString
            End If
        ElseIf int_tipo = 2 Then
            If int_codigo.ToString.Length = 1 Then
                str_texto = "00" + int_codigo.ToString
            ElseIf int_codigo.ToString.Length = 2 Then
                str_texto = "0" + int_codigo.ToString
            Else
                str_texto = int_codigo.ToString
            End If
        End If
        Return str_texto
    End Function

    'Private Sub editarDetalle()

    '    If tbCodigoEjemplar.Text = "" Then
    '        pnModalAgregarRegistro.Show()
    '        MostrarSexyAlertBox("Debe ingresar el codigo de ejemplar.", "Alert")
    '        Exit Sub
    '    End If

    '    Dim int_CodigoOriginal As Integer = hd_CodigoDetalle.Value
    '    Dim dt As DataTable
    '    Dim boolIncremento As Boolean = False
    '    dt = ViewState("listaDatosEjemplares")


    '    For Each auxdr As DataRow In dt.Rows
    '        If auxdr.Item("CodigoCopiaLibro").ToString = int_CodigoOriginal Then
    '            auxdr.Item("TipoDato") = "A"
    '            auxdr.Item("CodigoCopiaLibro") = hd_CodigoDetalle.Value
    '            auxdr.Item("CodigoLibro") = hd_Codigo.Value
    '            auxdr.Item("CodigoBarra") = tbCodigoBarra.Text
    '            auxdr.Item("CodigoEjemplar") = tbCodigoEjemplar.Text
    '            'auxdr.Item("NumeroEjemplar") = 1
    '            auxdr.Item("Disponible") = "" 'hd_Disponible.Value
    '            auxdr.Item("estado") = "Activo"
    '        End If
    '    Next
    '    Dim int_cantidadD As Integer = 0
    '    Dim int_cantidadU As Integer = 0
    '    Dim int_cont As Integer = 0

    '    While int_cont <= dt.Rows.Count - 1
    '        If dt.Rows(int_cont).Item("Disponible").Equals("Disponible") Then
    '            int_cantidadD = int_cantidadD + 1
    '        Else
    '            int_cantidadU = int_cantidadU + 1
    '        End If
    '        int_cont = int_cont + 1
    '    End While

    '    tbTotal.Text = int_cont
    '    tbDisponible.Text = int_cantidadD
    '    tbUtilizado.Text = int_cantidadU

    '    ViewState.Remove("listaDatosEjemplares")
    '    ViewState("listaDatosEjemplares") = dt
    '    GV_DatosEjemplares.DataSource = dt
    '    GV_DatosEjemplares.DataBind()
    '    tbCodigoBarra.Text = 0
    '    tbCodigoEjemplar.Text = 0
    '    'upDatosEjemplares.Update()
    'End Sub

    Private Sub eliminarDetalle(ByVal int_Codigo As Integer)

        Dim id_codigo_fila As Integer = 0
        Dim ds_Lista As New DataSet
        Dim dt As DataTable
        Dim dt_listaDatos As DataTable
        dt_listaDatos = ViewState("listaDatosEjemplares")

        dt = New DataTable("listaDatosEjemplares")
        dt = Datos.agregarColumna(dt, "TipoDato", "String")
        dt = Datos.agregarColumna(dt, "CodigoCopiaLibro", "String")
        dt = Datos.agregarColumna(dt, "CodigoLibro", "String")
        dt = Datos.agregarColumna(dt, "CodigoBarra", "String")
        dt = Datos.agregarColumna(dt, "CodigoEjemplar", "String")
        dt = Datos.agregarColumna(dt, "NumeroEjemplar", "Integer")
        dt = Datos.agregarColumna(dt, "Disponible", "String")
        dt = Datos.agregarColumna(dt, "estado", "String")
        dt = Datos.agregarColumna(dt, "FechaCompra", "Datetime")
        dt = Datos.agregarColumna(dt, "FechaCompraStr", "String")
        dt = Datos.agregarColumna(dt, "NumeroPago", "String")

        Dim dt_RegistrosEliminados As DataTable

        If ViewState("ListaDatosEliminados") Is Nothing Then
            dt_RegistrosEliminados = New DataTable("ListaDatosEliminados")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "CodigoCopiaLibro", "String")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "CodigoLibro", "String")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "CodigoBarra", "String")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "CodigoEjemplar", "String")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "NumeroEjemplar", "Integer")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "Disponible", "String")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "estado", "String")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "FechaCompra", "Datetime")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "FechaCompraStr", "String")
            dt_RegistrosEliminados = Datos.agregarColumna(dt_RegistrosEliminados, "NumeroPago", "String")
        Else
            dt_RegistrosEliminados = ViewState("ListaDatosEliminados")
        End If

        For Each gvr As DataRow In dt_listaDatos.Rows
            Dim dr As DataRow
            If int_Codigo = gvr.Item("CodigoCopiaLibro") Then
                'Solo agrego a mi DataTable de eliminados los registros que existen en la Base de Datos
                If gvr.Item("TipoDato").ToString() = "R" Then
                    Dim drLE As DataRow
                    drLE = dt_RegistrosEliminados.NewRow
                    drLE.Item("CodigoCopiaLibro") = gvr.Item("CodigoCopiaLibro")
                    drLE.Item("CodigoLibro") = gvr.Item("CodigoLibro")
                    drLE.Item("CodigoBarra") = gvr.Item("CodigoBarra")
                    drLE.Item("CodigoEjemplar") = gvr.Item("CodigoEjemplar")
                    drLE.Item("NumeroEjemplar") = gvr.Item("NumeroEjemplar")
                    drLE.Item("Disponible") = gvr.Item("Disponible").ToString()
                    drLE.Item("estado") = gvr.Item("Estado").ToString()
                    drLE.Item("FechaCompra") = CDate(gvr.Item("FechaCompra"))
                    drLE.Item("FechaCompraStr") = gvr.Item("FechaCompraStr").ToString
                    drLE.Item("NumeroPago") = gvr.Item("NumeroPago").ToString()
                    dt_RegistrosEliminados.Rows.Add(drLE)
                End If
            Else
                dr = dt.NewRow
                dr.Item("TipoDato") = gvr.Item("TipoDato").ToString()
                dr.Item("CodigoCopiaLibro") = gvr.Item("CodigoCopiaLibro")
                dr.Item("CodigoLibro") = gvr.Item("CodigoLibro")
                dr.Item("CodigoBarra") = gvr.Item("CodigoBarra")
                dr.Item("CodigoEjemplar") = gvr.Item("CodigoEjemplar")
                dr.Item("NumeroEjemplar") = gvr.Item("NumeroEjemplar")
                dr.Item("Disponible") = gvr.Item("Disponible").ToString()
                dr.Item("estado") = gvr.Item("Estado").ToString()
                dr.Item("FechaCompra") = CDate(gvr.Item("FechaCompra"))
                dr.Item("FechaCompraStr") = gvr.Item("FechaCompraStr").ToString
                dr.Item("NumeroPago") = gvr.Item("NumeroPago").ToString()
                dt.Rows.Add(dr)
            End If
        Next

        'For Each gvr As GridViewRow In GV_DatosEjemplares.Rows
        '    Dim dr As DataRow

        '    If int_Codigo = CType(gvr.FindControl("lblCodigoCopiaLibro"), Label).Text Then
        '        'Solo agrego a mi DataTable de eliminados los registros que existen en la Base de Datos
        '        If CType(gvr.FindControl("lblTipoDato"), Label).Text = "R" Then
        '            Dim drLE As DataRow

        '            drLE = dt_RegistrosEliminados.NewRow
        '            drLE.Item("CodigoCopiaLibro") = CType(gvr.FindControl("lblCodigoCopiaLibro"), Label).Text
        '            drLE.Item("CodigoLibro") = CType(gvr.FindControl("lblCodigoLibro"), Label).Text
        '            drLE.Item("CodigoBarra") = 0
        '            drLE.Item("CodigoEjemplar") = CType(gvr.FindControl("lblCodigoEjemplar"), Label).Text
        '            drLE.Item("NumeroEjemplar") = CType(gvr.FindControl("lblNumeroEjemplar"), Label).Text
        '            drLE.Item("Disponible") = CType(gvr.FindControl("lblDisponible"), Label).Text
        '            drLE.Item("estado") = CType(gvr.FindControl("lblEstado"), Label).Text
        '            dt_RegistrosEliminados.Rows.Add(drLE)

        '        End If

        '        'dr.Delete()
        '    Else
        '        dr = dt.NewRow

        '        dr.Item("TipoDato") = CType(gvr.FindControl("lblTipoDato"), Label).Text
        '        dr.Item("CodigoCopiaLibro") = CType(gvr.FindControl("lblCodigoCopiaLibro"), Label).Text
        '        dr.Item("CodigoLibro") = CType(gvr.FindControl("lblCodigoLibro"), Label).Text
        '        dr.Item("CodigoBarra") = 0
        '        dr.Item("CodigoEjemplar") = CType(gvr.FindControl("lblCodigoEjemplar"), Label).Text
        '        dr.Item("NumeroEjemplar") = CType(gvr.FindControl("lblNumeroEjemplar"), Label).Text
        '        dr.Item("Disponible") = CType(gvr.FindControl("lblDisponible"), Label).Text
        '        dr.Item("estado") = CType(gvr.FindControl("lblEstado"), Label).Text
        '        dt.Rows.Add(dr)
        '    End If
        'Next

        'dt.AcceptChanges()

        Dim int_cantidadD As Integer = 0
        Dim int_cantidadU As Integer = 0
        Dim int_cont As Integer = 0

        While int_cont <= dt.Rows.Count - 1
            If dt.Rows(int_cont).Item("Disponible").Equals("Disponible") Then
                int_cantidadD = int_cantidadD + 1
            Else
                int_cantidadU = int_cantidadU + 1
            End If
            int_cont = int_cont + 1
        End While

        tbTotal.Text = int_cont
        hfTotalRegs1.Value = tbTotal.Text
        tbDisponible.Text = int_cantidadD
        tbUtilizado.Text = int_cantidadU

        ViewState.Remove("listaDatosEjemplares")
        ViewState("listaDatosEjemplares") = dt

        Dim int_valAux As Integer = 0

        If ViewState("listaDatosEjemplares") Is Nothing Then
            int_valAux = 0
        Else
            If dt.Rows.Count > 0 Then
                int_valAux = (From p In dt.AsEnumerable _
                                        Select p.Field(Of Integer)("NumeroEjemplar")).Max()
            Else
                int_valAux = 0
            End If

        End If

        ViewState("listaDatosCantNumEjemplares") = int_valAux

        'Dim id_num_ejemplar As Integer
        'If ViewState("listaDatosCantNumEjemplares") Is Nothing Then
        '    id_num_ejemplar = 0
        'Else
        '    id_num_ejemplar = ViewState("listaDatosCantNumEjemplares")
        'End If

        ViewState.Remove("ListaDatosEliminados")
        ViewState("ListaDatosEliminados") = dt_RegistrosEliminados

        GV_DatosEjemplares.DataSource = dt
        GV_DatosEjemplares.DataBind()

    End Sub

#End Region

#Region "Metodos GV_DatosEjemplares"

    ''' <summary>
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     11/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting1()

        Dim _btnSorting As ImageButton = CType(GV_DatosEjemplares.HeaderRow.FindControl("btnSorting"), ImageButton)

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
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CrearBotonesPager1(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page)

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim ddlPageSelector As DropDownList = DirectCast(gvPagerRow.FindControl("ddlPageSelector1"), DropDownList)
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
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function InformacionPager1(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page) As String

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim pageSize As Integer = gridView.PageSize
        Dim rowCount As Integer = gridView.Rows.Count

        Dim currentPageFirstRow As Integer = ((pageIndex * pageSize) + 1)
        Dim currentPageLastRow As Integer = 0
        Dim lastPageRemainder As Integer = pageCount Mod pageSize

        currentPageLastRow = currentPageFirstRow + rowCount - 1

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegs1.Value)

    End Function

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Property GridViewSortDirection1() As SortDirection

        Get
            If ViewState("sortDirection1") Is Nothing Then
                ViewState("sortDirection1") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("sortDirection1"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection1") = value
        End Set

    End Property

    ''' <summary>
    ''' Lista los datos de procedimientos realizados ordenados por Descripción.
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView1(ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataTable = ViewState("listaDatosEjemplares")

        hfTotalRegs1.Value = CInt(ds_Lista.Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista)
        dv.Sort = sortExpression + " " + direction

        GV_DatosEjemplares.DataSource = dv
        GV_DatosEjemplares.DataBind()

    End Sub

#End Region

#Region "Eventos del GV_DatosEjemplar"

    Protected Sub GV_DatosEjemplares_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then

                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    ViewState("GV_DatosEjemplares") = False
                    int_CodigoAccion = 6
                    obtenerDatosEjemplares(codigo, CType(row.FindControl("lblDisponible"), Label).Text, CType(row.FindControl("lblCodigoEjemplar"), Label).Text, CType(row.FindControl("lblCodigoBarra"), Label).Text)

                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 3
                    eliminarDetalle(codigo)
                End If

            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GV_DatosEjemplares_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        'If ViewState("DatosEjemplares") <> "" Then
        Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
        Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas1")
            _TotalPags.Text = GV_DatosEjemplares.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales1")
            _Registros.Text = InformacionPager1(GV_DatosEjemplares, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            ''SETEO DE PERMISOS DE ACCIONES---------------
            'Master.BloqueoControles(btnEliminar, 1)
            'Master.BloqueoControles(btnActualizar, 1)
            'Master.BloqueoControles(btnActivar, 1)
            ''---------------------------------------------

            If e.Row.DataItem("Estado") = "Activo" Then
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

    Protected Sub GV_DatosEjemplares_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GV_DatosEjemplares.PageIndex = e.NewPageIndex
            End If

            SortGridView1(ViewState("SortExpression1"), ViewState("Direccion1"))
            'ImagenSorting1()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPageSelector1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GV_DatosEjemplares.PageCount Then
                Me.GV_DatosEjemplares.PageIndex = _NumPag - 1
            Else
                Me.GV_DatosEjemplares.PageIndex = 0
            End If

            Me.GV_DatosEjemplares.SelectedIndex = -1

            SortGridView1(ViewState("SortExpression1"), ViewState("Direccion1"))
            'ImagenSorting1()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try

    End Sub

    Protected Sub GV_DatosEjemplares_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager1(GV_DatosEjemplares, e.Row, Me)
        End If

    End Sub

    Protected Sub GV_DatosEjemplares_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression

            ViewState("SortExpression1") = sortExpression

            If GridViewSortDirection1 = SortDirection.Ascending Then
                GridViewSortDirection1 = SortDirection.Descending
                SortGridView1(sortExpression, "DESC")
                ViewState("Direccion1") = "DESC"
            Else
                GridViewSortDirection1 = SortDirection.Ascending
                SortGridView1(sortExpression, "ASC")
                ViewState("Direccion1") = "ASC"
            End If

            'ImagenSorting()

        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try

    End Sub

#End Region
End Class