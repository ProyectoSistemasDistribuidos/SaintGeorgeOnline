Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloBancoLibros
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

Partial Class Modulo_BancoLibros_PrestamoLibros
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

    'Actualizado 04/07/2011 13:27 pm

#Region "Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Master.MostrarTitulo("Prestamo de Libros")

            If Not Page.IsPostBack Then
                cargarComboAniosAcademicos()
                cargarComboIdioma()
                cargarComboBuscarGrados()
                limpiarCombos(ddlBuscarAula, True, False)
                ddlBuscarAnio.SelectedValue = 1
                
            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarAnio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlBuscarGrado.SelectedValue <> 0 Then
                hiddenMiGridviewIndex.Value = ""
                listar()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlBuscarGrado.SelectedValue <> 0 Then
                hiddenMiGridviewIndex.Value = ""
                cargarComboBuscarAulas()
                listar()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarAula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            hiddenMiGridviewIndex.Value = ""
            listar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBuscarIdioma_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlBuscarGrado.SelectedValue <> 0 Then
                hiddenMiGridviewIndex.Value = ""
                listar()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub limpiarCombos(ByVal combo As DropDownList, ByVal bool_Todos As Boolean, ByVal bool_Seleccione As Boolean)

        Controles.limpiarCombo(combo, bool_Todos, bool_Seleccione)

    End Sub

    Private Sub limpiarCampos()

        ViewState.Remove("Eliminados")
        ViewState.Remove("ListaLibrosMarcados")
        listar()

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Grados disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     31/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboIdioma()

        Dim int_Estado As Integer = 1
        Dim str_Descripcion As String = ""
        Dim obj_BL_Libros As New bl_Libros
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Libros.FUN_LIS_Idiomas(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarIdioma, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     27/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAniosAcademicos()

        Dim int_Estado As Integer = 1
        Dim str_Descripcion As String = ""
        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAnio, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    ''' <summary>
    ''' Llena el combo "ddlBuscarGrado" con la lista de grados activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     03/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboBuscarGrados()

        Dim int_Estado As Integer = 1
        Dim str_Descripcion As String = ""
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarGrado, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    Private Sub cargarComboBuscarAulas()

        Dim int_CodigoGrado As Integer = ddlBuscarGrado.SelectedValue
        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAula, ds_Lista, "Codigo", "Descripcion", True, False)

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
        Dim dtLibro As DataTable
        dtLibro = ds_Lista.Tables(1)

        ViewState("ListaAlumnos") = ds_Lista.Tables(0)
        ViewState("ListaLibrosCadena") = dtLibro
        ViewState("ListaLibrosMarcados") = ds_Lista.Tables(2)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        dl_NombreLibros.DataSource = dtLibro
        dl_NombreLibros.DataBind()

        If hiddenMiGridviewIndex.Value.ToString.Length > 0 Then
            Dim int_GVIndex As Integer = hiddenMiGridviewIndex.Value
            hiddenMiEstadoGrabar.Value = 1
        Else
            hiddenMiEstadoGrabar.Value = 0
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
        Dim int_Grado As Integer = CInt(ddlBuscarGrado.SelectedValue)
        Dim int_Aula As Integer = ddlBuscarAula.SelectedValue
        Dim int_Idioma As Integer = CInt(ddlBuscarIdioma.SelectedValue)
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_PrestamoDetalle As New bl_PrestamoDetalle
            ds_Lista = obj_BL_PrestamoDetalle.FUN_LIS_PrestamoPorLibroGradoPeriodo(int_Periodo, int_Grado, int_Aula, int_Idioma, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_PrestamoDetalle As New bl_PrestamoDetalle
                ds_Lista = obj_BL_PrestamoDetalle.FUN_LIS_PrestamoPorLibroGradoPeriodo(int_Periodo, int_Grado, int_Aula, int_Idioma, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Construye la estructura de la tabla temporal de permisos donde se almacenara la información registrada para su manipulación.
    ''' </summary>
    ''' <param name="dt_Libros">Tabla con las acciones permitidas por el perfil seleccionado</param>
    ''' <param name="chkLibros">control de tipo listbox para setear las acciones de acceso</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     10/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function setearDetalleTabla(ByVal dt_Libros As DataTable, ByVal dt_LibrosAlumno As DataView, ByVal str_CodigoAlumno As String, ByVal chkLibros As WebControls.CheckBoxList) As Boolean

        chkLibros.DataSource = dt_Libros
        chkLibros.DataTextField = "NombreBlanco"
        chkLibros.DataValueField = "CodigoLibro"
        chkLibros.DataBind()

        Dim int_cantLibros As Integer = 0
        Dim contObj As Integer = 0

        For Each dr As DataRowView In dt_LibrosAlumno
            If dr.Item("CodigoAlumno") = str_CodigoAlumno Then
                While contObj <= chkLibros.Items.Count - 1
                    If chkLibros.Items(contObj).Value = dr.Item("CodigoLibro") Then
                        chkLibros.Items(contObj).Selected = True
                        int_cantLibros = int_cantLibros + 1
                    End If
                    contObj = contObj + 1
                End While
                contObj = 0
            End If
        Next

        If int_cantLibros = dt_Libros.Rows.Count And int_cantLibros > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Setea las acciones de acceso del usuario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     13/04/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(cod_Modulo, cod_Opcion)
    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     10/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
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
    ''' Fecha de Creación:     13/04/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

#End Region

#Region "Metodos GridView"

    Private Sub Grabar(ByVal chkLibrosActualizar As CheckBoxList, ByVal str_CodigoAlumno As String, ByVal str_NombreAlumno As String, ByVal int_CodigoPrestamo As Integer, ByVal gvIndex As Integer, ByVal miArray As Array)

        Dim obj_BE_Prestamos As be_Prestamos
        Dim obj_BE_PrestamoDetalle As be_PrestamoDetalle
        Dim obj_BL_Prestamos As New bl_Prestamos
        Dim obj_BL_PrestamoDetalle As New bl_PrestamoDetalle

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""
        Dim usp_valorRegistrado As Integer = 0
        Dim usp_valorEliminado As Integer = 0

        Dim ds_Prestamo As DataSet
        Dim usp_CodigoPrestamoDetalle As Integer = 0
        Dim usp_EstadoPrestamo As Integer

        Dim bool_Actualizar As Boolean = False
        Dim bool_Eliminar As Boolean = False
        Dim int_CodigoLibro As Integer = 0
        Dim contObj As Integer = 0

        Dim bool_Elimino As Boolean = False
        Dim bool_Registro As Boolean = False
        Dim bool_Existe As Boolean = False

        Dim dt_Registros As New DataTable
        dt_Registros = Datos.agregarColumna(dt_Registros, "CodigoLibro", "Integer")
        dt_Registros = Datos.agregarColumna(dt_Registros, "TituloLibro", "string")
        dt_Registros = Datos.agregarColumna(dt_Registros, "Precio", "decimal")
        dt_Registros = Datos.agregarColumna(dt_Registros, "Tipo", "Integer") ' No registrado : 2 - Registrado : 1 - Eliminado : 0

        Dim de_PrecioAux As Decimal = 0
        Dim str_TituloAux As String = ""

        Dim dr_R As DataRow

        While contObj <= chkLibrosActualizar.Items.Count - 1

            obj_BE_Prestamos = New be_Prestamos
            obj_BE_Prestamos.CodigoAlumno = str_CodigoAlumno
            obj_BE_Prestamos.CodigoAnio = ddlBuscarAnio.SelectedValue
            int_CodigoLibro = chkLibrosActualizar.Items(contObj).Value

            If bool_Existe = False Then
                usp_valor = obj_BL_Prestamos.FUN_INS_Prestamos(obj_BE_Prestamos, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                If usp_valor > 0 Then
                    bool_Existe = True
                End If
            End If

            If bool_Existe Then
                ds_Prestamo = obtenerCodigoPrestamoDetalle(int_CodigoPrestamo, int_CodigoLibro)
                usp_CodigoPrestamoDetalle = ds_Prestamo.Tables(0).Rows(0).Item("CodigoDetalle")
                usp_EstadoPrestamo = Convert.ToInt32(ds_Prestamo.Tables(0).Rows(0).Item("EstadoPrestamo"))

                If usp_CodigoPrestamoDetalle > 0 Then ' Si existe

                    If miArray(contObj) = "False" Then ' Si existe y está desmarcado
                        If usp_EstadoPrestamo = 1 Then ' Si el prestamo ya fue devuelto (Estado 1), no se puede actualizar
                            If obtenerInformacionLibros(int_CodigoLibro, str_TituloAux, de_PrecioAux) > 0 Then
                                dr_R = dt_Registros.NewRow
                                dr_R.Item("CodigoLibro") = int_CodigoLibro
                                dr_R.Item("TituloLibro") = str_TituloAux
                                dr_R.Item("Precio") = de_PrecioAux
                                dr_R.Item("Tipo") = 2
                                dt_Registros.Rows.Add(dr_R)
                            End If
                        ElseIf usp_EstadoPrestamo = 0 Then ' Si el prestamo no ha sido devuelvo, elimino logicamente el registro
                            usp_valorEliminado = obj_BL_PrestamoDetalle.FUN_DEL_PrestamoDetalle(usp_CodigoPrestamoDetalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                            If usp_valorEliminado > 0 Then ' si actualizo el detalle del prestamo
                                bool_Elimino = True
                                If obtenerInformacionLibros(int_CodigoLibro, str_TituloAux, de_PrecioAux) > 0 Then
                                    dr_R = dt_Registros.NewRow
                                    dr_R.Item("CodigoLibro") = int_CodigoLibro
                                    dr_R.Item("TituloLibro") = str_TituloAux
                                    dr_R.Item("Precio") = de_PrecioAux
                                    dr_R.Item("Tipo") = 0
                                    dt_Registros.Rows.Add(dr_R)
                                End If
                            End If
                        End If
                    Else ' Si existe y está marcado
                        If usp_EstadoPrestamo = 1 Then ' Si el prestamo ya fue devuelto (Estado 1), no se puede actualizar
                            If obtenerInformacionLibros(int_CodigoLibro, str_TituloAux, de_PrecioAux) > 0 Then
                                dr_R = dt_Registros.NewRow
                                dr_R.Item("CodigoLibro") = int_CodigoLibro
                                dr_R.Item("TituloLibro") = str_TituloAux
                                dr_R.Item("Precio") = de_PrecioAux
                                dr_R.Item("Tipo") = 2
                                dt_Registros.Rows.Add(dr_R)
                            End If
                        Else ' El prestamo no ha sido devuelto
                            If obtenerInformacionLibros(int_CodigoLibro, str_TituloAux, de_PrecioAux) > 0 Then
                                dr_R = dt_Registros.NewRow
                                dr_R.Item("CodigoLibro") = int_CodigoLibro
                                dr_R.Item("TituloLibro") = str_TituloAux
                                dr_R.Item("Precio") = de_PrecioAux
                                dr_R.Item("Tipo") = 1
                                dt_Registros.Rows.Add(dr_R)
                            End If
                        End If

                    End If
                Else ' Si no existe 
                    If miArray(contObj) = "True" Then ' si está marcado, se registra el detalle
                        obj_BE_PrestamoDetalle = New be_PrestamoDetalle
                        obj_BE_PrestamoDetalle.CodigoPrestamo = usp_valor
                        obj_BE_PrestamoDetalle.CodigoLibro = int_CodigoLibro
                        usp_valorRegistrado = obj_BL_PrestamoDetalle.FUN_INS_PrestamoDetalle(obj_BE_PrestamoDetalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                        If usp_valorRegistrado > 0 Then ' si registro el detalle del prestamo
                            bool_Registro = True
                            If obtenerInformacionLibros(int_CodigoLibro, str_TituloAux, de_PrecioAux) > 0 Then
                                dr_R = dt_Registros.NewRow
                                dr_R.Item("CodigoLibro") = int_CodigoLibro
                                dr_R.Item("TituloLibro") = str_TituloAux
                                dr_R.Item("Precio") = de_PrecioAux
                                dr_R.Item("Tipo") = 1
                                dt_Registros.Rows.Add(dr_R)
                            End If
                        End If
                    End If
                End If
            End If

            contObj = contObj + 1
        End While

        If dt_Registros.Rows.Count > 0 Then
            Dim usp_MensajeRegistro As String = ""
            usp_MensajeRegistro = mensajeRegistro(dt_Registros, str_NombreAlumno)
            MostrarSexyAlertBox(usp_MensajeRegistro, "Info")
        End If

        listar()

    End Sub

    Private Function obtenerCodigoPrestamoDetalle(ByVal int_CodigoPrestamo As Integer, ByVal int_CodigoLibro As Integer) As DataSet

        Dim obj_BL_Devoluciones As New bl_Devoluciones
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As DataSet

        ds_Lista = obj_BL_Devoluciones.FUN_GET_PrestamoDetalle(int_CodigoPrestamo, int_CodigoLibro, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Return ds_Lista

    End Function

    Private Function obtenerInformacionLibros(ByVal int_CodigoLibro As Integer, ByRef str_TituloLibro As String, ByRef de_Precio As Decimal) As Integer

        str_TituloLibro = ""
        de_Precio = 0.0

        If ViewState("ListaLibrosCadena") Is Nothing Then
            Return 0
        End If

        Dim dtLibro As DataTable
        dtLibro = ViewState("ListaLibrosCadena")

        For Each dr As DataRow In dtLibro.Rows
            If dr.Item("CodigoLibro") = int_CodigoLibro Then
                str_TituloLibro = dr.Item("Titulo")
                de_Precio = dr.Item("PrecioPrestamo")
                Exit For
            End If
        Next

        Return int_CodigoLibro
    End Function

    Private Function mensajeRegistro(ByVal dt_Registro As DataTable, ByVal str_NombreAlumno As String) As String

        Dim str_MensajeFinal As New StringBuilder
        Dim str_MsjNoRegistrados As New StringBuilder
        Dim str_MsjRegistrados As New StringBuilder
        Dim str_MsjEliminados As New StringBuilder
        Dim int_Registrados, int_Eliminados, int_NoRegistrado As Integer
        Dim de_TotalRegistrados As Decimal = 0.0

        ' No registrado : 2 - Registrado : 1 - Eliminado : 0
        For Each dr As DataRow In dt_Registro.Rows
            If dr.Item("Tipo") = 2 Then
                int_NoRegistrado += 1
                str_MsjNoRegistrados.Append("<li style=""width: 300px; color: green;"">" & _
                                                "<div style=""width: 260px; border: solid 0px red; float: left; text-align:left;"">" & formatearCadenasHTML(dr.Item("TituloLibro")) & "</div>" & _
                                                "<div style=""width: 40px; border: solid 0px red; float: left; text-align:right;"">" & formatearCadenasHTML(dr.Item("Precio").ToString) & "</div></li>")
            ElseIf dr.Item("Tipo") = 1 Then
                int_Registrados += 1
                str_MsjRegistrados.Append("<li style=""width: 300px;"">" & _
                                                "<div style=""width: 260px; border: solid 0px red; float: left; text-align:left;"">" & formatearCadenasHTML(dr.Item("TituloLibro")) & "</div>" & _
                                                "<div style=""width: 40px; border: solid 0px red; float: left; text-align:right;"">" & formatearCadenasHTML(dr.Item("Precio").ToString) & "</div></li>")

                de_TotalRegistrados = Format((de_TotalRegistrados + dr.Item("Precio")), "0.00")

            ElseIf dr.Item("Tipo") = 0 Then
                int_Eliminados += 1
                str_MsjEliminados.Append("<li style=""width: 300px; color: red;"">" & _
                                                "<div style=""width: 260px; border: solid 0px red; float: left; text-align:left;"">" & formatearCadenasHTML(dr.Item("TituloLibro")) & "</div>" & _
                                                "<div style=""width: 40px; border: solid 0px red; float: left; text-align:right;"">" & formatearCadenasHTML(dr.Item("Precio").ToString) & "</div></li>")

            End If
        Next

        str_MensajeFinal.Append("<span>Operación exitosa.</span><br />")
        str_MensajeFinal.Append("<span>Prestamos del alumno(a) : </span><br><span style=""font-weight: bold;"">" & str_NombreAlumno & "</span><br />")
        str_MensajeFinal.Append("<br /><div style=""font-weight: bold;"">" & _
                                "<div style=""width: 260px; border: solid 0px #000000; float: left; text-align:left;"">Título</div>" & _
                                "<div style=""width: 40px; border: solid 0px #000000; float: left; text-align:right;"">Precio</div></div><br />")

        If int_Registrados > 0 Then
            str_MensajeFinal.Append("<br /><ul style=""font-weight: bold;"">")
            str_MensajeFinal.Append(str_MsjRegistrados.ToString)
            str_MensajeFinal.Append("</ul>")
            str_MensajeFinal.Append("<div style=""font-weight: bold;"">" & _
                                    "<div style=""width: 260px; border-top: solid 1px #000000; float: left; text-align:left;"">Total a pagar :</div>" & _
                                    "<div style=""width: 40px; border-top: solid 1px #000000; float: left; text-align:right;"">" & de_TotalRegistrados.ToString & "</div></div><br />")
        End If

        If int_NoRegistrado > 0 Then
            str_MensajeFinal.Append("<br /><ul style=""font-weight: bold;"">")
            str_MensajeFinal.Append(str_MsjNoRegistrados.ToString)
            str_MensajeFinal.Append("</ul>")
        End If


        If int_Eliminados > 0 Then
            str_MensajeFinal.Append("<br /><ul style=""font-weight: bold;"">")
            str_MensajeFinal.Append(str_MsjEliminados.ToString)
            str_MensajeFinal.Append("</ul>")
        End If

        str_MensajeFinal.Append("<br /><br /><span style=""text-align:left;"">Notas : </span><br />")
        str_MensajeFinal.Append("<ul>")
        str_MensajeFinal.Append("<li>")
        str_MensajeFinal.Append("<div style=""width: 300px; border: solid 0px #000000; float: left; text-align:left;"">")
        str_MensajeFinal.Append("<span>Prestamos : </span>")
        str_MensajeFinal.Append("<span style=""font-weight: bold; color: black;"">Registrados</span> - ")
        str_MensajeFinal.Append("<span style=""font-weight: bold; color: green;"">Devueltos</span> - ")
        str_MensajeFinal.Append("<span style=""font-weight: bold; color: red;"">Eliminados</span>")
        str_MensajeFinal.Append("</div>")
        str_MensajeFinal.Append("</li>")
        str_MensajeFinal.Append("<li>")
        str_MensajeFinal.Append("<div style=""width: 300px; border: solid 0px #000000; float: left; text-align:left;"">")
        str_MensajeFinal.Append("<span>Los registros de prestamos <em style=""font-weight: bold; color: red;"">devueltos</em> no podrán ser eliminados.</span>")
        str_MensajeFinal.Append("</div>")
        str_MensajeFinal.Append("</li>")
        str_MensajeFinal.Append("</ul>")

        Return str_MensajeFinal.ToString

    End Function

    Private Function formatearCadenasHTML(ByVal str_Cadena As String) As String

        Dim str_CadenaDepurada As String = ""

        For Each c As Char In str_Cadena
            If caracteresPermitidos(c) Then
                str_CadenaDepurada += c
            End If
        Next

        Return str_CadenaDepurada

    End Function

    Private Function caracteresPermitidos(ByVal c As Char) As Boolean

        If c = "a" Or c = "b" Or c = "c" Or c = "d" Or c = "e" Or c = "f" Or c = "g" Or c = "h" Or c = "i" Or c = "j" Or c = "k" Or _
            c = "l" Or c = "m" Or c = "n" Or c = "o" Or c = "p" Or c = "q" Or c = "r" Or c = "s" Or c = "t" Or c = "u" Or c = "v" Or _
            c = "w" Or c = "x" Or c = "y" Or c = "z" Or c = " " Or c = "¿" Or c = "?" Or c = "¡" Or c = "(" Or c = ")" Or c = "-" Or _
            c = "A" Or c = "B" Or c = "C" Or c = "D" Or c = "E" Or c = "F" Or c = "G" Or c = "H" Or c = "I" Or c = "J" Or c = "K" Or _
            c = "L" Or c = "M" Or c = "N" Or c = "O" Or c = "P" Or c = "Q" Or c = "R" Or c = "S" Or c = "T" Or c = "U" Or c = "V" Or _
            c = "W" Or c = "X" Or c = "Y" Or c = "Z" Or c = "1" Or c = "2" Or c = "3" Or c = "4" Or c = "5" Or c = "6" Or c = "7" Or _
            c = "8" Or c = "9" Or c = "0" Or c = "." Or c = "," Then
            Return True
        Else
            Return False
        End If

    End Function

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblCodigoAlumno As System.Web.UI.WebControls.Label = e.Row.FindControl("lblCodigoAlumno")
            Dim chkLibros As System.Web.UI.WebControls.CheckBoxList = e.Row.FindControl("chk_Libros_grilla")
            Dim chkAll As System.Web.UI.WebControls.CheckBox = e.Row.FindControl("chkAll")

            Dim dt_LibrosAlumno As New DataTable
            Dim dt_Libros As New DataTable
            Dim str_CodigoAlumno As String = lblCodigoAlumno.Text

            dt_Libros = ViewState("ListaLibrosCadena")
            dt_LibrosAlumno = ViewState("ListaLibrosMarcados")

            chkAll.Enabled = False

            Dim dv As DataView = dt_LibrosAlumno.DefaultView
            dv.RowFilter = "CodigoAlumno = '" & str_CodigoAlumno & "'"

            chkAll.Checked = setearDetalleTabla(dt_Libros, dv, str_CodigoAlumno, chkLibros)
            chkLibros.Enabled = False

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Grabar" Then

                Dim gvIndex As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Grabar" Then

                    Dim chkLibrosActualizar As CheckBoxList = CType(GridView1.Rows(gvIndex).FindControl("chk_Libros_grilla"), CheckBoxList)
                    Dim str_CodigoAlumno As String = CType(row.FindControl("lblCodigoAlumno"), Label).Text
                    Dim str_NombreAlumno As String = CType(row.FindControl("lblNombreCompleto"), Label).Text
                    Dim int_CodigoPrestamo As Integer = CType(row.FindControl("lblCodigoPrestamo"), Label).Text
                    Dim str_MiArray As Array = hiddenLibrosPrestados.Value.ToString.Split(",")
                    Grabar(chkLibrosActualizar, str_CodigoAlumno, str_NombreAlumno, int_CodigoPrestamo, gvIndex, str_MiArray)

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Modal Informacion del libro"

    Protected Sub CargarModalLibro(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim dtLibro As DataTable = ViewState("ListaLibrosCadena")
        Dim int_CodigoLibro As Integer = CType(sender, LinkButton).CommandArgument
        Dim str_Titulo As String = ""
        Dim int_Numero As Integer = 0
        Dim de_PrecioPrestamo As Decimal = 0.0

        For Each dr As DataRow In dtLibro.Rows
            If dr.Item("CodigoLibro") = int_CodigoLibro Then
                str_Titulo = dr.Item("Titulo")
                de_PrecioPrestamo = dr.Item("PrecioPrestamo")
                Exit For
            End If
        Next

        miContenidoModal.InnerHtml = "Título : " & str_Titulo & "<br />" & _
                                     "Precio Prestamo : " & de_PrecioPrestamo & "<br />" & _
                                     "<img alt='" & str_Titulo & "' src='../App_Themes/Imagenes/opc_actualizar.png'/>"

        ModalPopupExtender1.Show()

    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender1.Hide()
    End Sub

#End Region

End Class