Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports System.Data
Partial Class Interfaz_Familia_Modulo_Enfermeria_DatosFichaMedica
    Inherits System.Web.UI.Page

    Dim cod_Modulo As Integer = 1
    Dim cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Master.MostrarTitulo("Información de Ficha Médica del Alumno")

        Try
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                ObtenerDatosFamilia()
                Obtener_FichaMedica()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_SolicitarActualizarDatos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_SolicitarActualizarDatos.Click
        'Enviar_ActualizarDatos()
    End Sub

    Protected Sub ddl_Hijo_SelectedIndexChanged()
        Obtener_FichaMedica()
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene la relación de Familiares según el usuario en el sistema.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     23/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerDatosFamilia()

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim ds_DatosAlumnos As New DataSet
        'Dim dt_Familia As New DataTable
        'Dim dt_Familiares As New DataTable
        Dim int_CodigoFamiliar As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        'Dim ds_Familiares As New DataSet
        'Dim dt_copia As New DataTable

        If int_CodigoTipoUsuario = 1 Then ' Alumnos

            ds_DatosAlumnos = obj_BL_Alumnos.FUN_GET_AlumnosPorCodigoAlumno(int_CodigoFamilia, int_CodigoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 65)
            ddl_Hijo.Enabled = False

        ElseIf int_CodigoTipoUsuario = 3 Then ' Familiares

            ds_DatosAlumnos = obj_BL_Alumnos.FUN_LIS_AlumnosPorCodigoFamilia(int_CodigoFamilia, int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 8)
            ddl_Hijo.Enabled = True

        End If


        ddl_Hijo.DataSource = ds_DatosAlumnos.Tables(1)
        ddl_Hijo.DataValueField = "CodigoAlumno"
        ddl_Hijo.DataTextField = "NombreCompleto"
        ddl_Hijo.DataBind()

        'ddl_Hijo.Items.Insert(0, New ListItem("--Seleccione--"))
        'ddl_Hijo.Items(0).Value = "0"

    End Sub


    ''' <summary>
    ''' Setear permisos de acciones sobre el formulario según la configuración del usuario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     23/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(4, 64)

    End Sub

    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        'Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

    Public Sub Obtener_FichaMedica()

        Try

            Dim int_CodigoAlumno As Integer = ddl_Hijo.SelectedValue
            Dim obj_BL_FichaMedica As New bl_FichaMedicasAlumnos

            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

            Dim ds_Lista As DataSet = obj_BL_FichaMedica.FUN_GET_FichaMedicasAlumnos(int_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            lbl_NombreCompleto.Text = ddl_Hijo.SelectedItem.Text

            '1...DESARROLLO INFANTIL
            lbl_TipoNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("Nacimiento").ToString
            lbl_ObservacionesNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("Observacion").ToString
            lbl_EdadLevCabeza.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadLevantocabeza").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadLevantocabeza").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadLevantocabeza") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadLevantocabeza") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadLevantocabeza").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " meses ")))
            lbl_EdadSento.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadSento").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadSento").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadSento") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadSento") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesSento").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadSento").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesSento").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesSento") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesSento") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesSento").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesSento") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesSento") & " meses ")))
            lbl_EdadParo.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadParo").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadParo").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadParo") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadParo") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesParo").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadParo").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesParo").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesParo") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesParo") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesParo").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesParo") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesParo") & " meses ")))
            lbl_EdadCamino.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadCamino").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadCamino").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadCamino") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadCamino") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesCamino").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadCamino").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesCamino").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesCamino") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesCamino") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesCamino").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesCamino") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesCamino") & " meses ")))
            lbl_EdadControloEsfinteres.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " meses ")))
            lbl_EdadHabloPrimerasPalabras.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " meses ")))
            lbl_EdadHabloFluidez.Text = IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez") & " año ", ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez") & " años ")) & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString = 0, " ", IIf(ds_Lista.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString = 0, IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez") & " meses "), " y " & IIf(ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString = 1, ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez") & " mes ", ds_Lista.Tables(0).Rows(0).Item("MesesHabloFluidez") & " meses ")))
            lbl_TipoSangre.Text = ds_Lista.Tables(0).Rows(0).Item("TipoSangre").ToString

            '2...ESTADO DE SALUD

            'Detalle Enfermedad
            If ds_Lista.Tables(1).Rows(0).Item("CodigoRelFichaMedEnEnfermedades") <> -1 Then
                gvDetalleEnfermedad.DataSource = ds_Lista.Tables(1)
                gvDetalleEnfermedad.DataBind()
            Else
                gvDetalleEnfermedad.DataBind()
            End If


            'Detalle Vacuna
            If ds_Lista.Tables(2).Rows(0).Item("CodigoRelVacunasFichaMed") <> -1 Then
                gvDetalleVacuna.DataSource = ds_Lista.Tables(2)
                gvDetalleVacuna.DataBind()
            Else
                gvDetalleVacuna.DataBind()
            End If


            ''Detalle Alergias
            If ds_Lista.Tables(3).Rows(0).Item("CodigoRelFichaMedAlergias") <> -1 Then
                gvDetalleAlergia.DataSource = ds_Lista.Tables(3)
                gvDetalleAlergia.DataBind()
                ViewState("ListaAlergia") = ds_Lista.Tables(3)
            Else
                gvDetalleAlergia.DataBind()
            End If


            'Detalle Caracteristicas de la piel
            If ds_Lista.Tables(4).Rows(0).Item("CodigoRelFichaMedCaractPiel") <> -1 Then
                gvDetalleCaracteristicaPiel.DataSource = ds_Lista.Tables(4)
                gvDetalleCaracteristicaPiel.DataBind()
                ViewState("ListaCaracteristicasPiel") = ds_Lista.Tables(4)
            Else
                gvDetalleCaracteristicaPiel.DataBind()
            End If


            'Detalle Medicamento
            If ds_Lista.Tables(5).Rows(0).Item("CodigoRelFichaAtenMedicamentos") <> -1 Then
                gvDetalleMedicamento.DataSource = ds_Lista.Tables(5)
                gvDetalleMedicamento.DataBind()
                ViewState("ListaMedicamentos") = ds_Lista.Tables(5)
            Else
                gvDetalleMedicamento.DataBind()
            End If

            'Hospitalizacion
            If ds_Lista.Tables(6).Rows(0).Item("CodigoRelFichaMedMotivoHosp") <> -1 Then
                gvDetalleHospitalizacion.DataSource = ds_Lista.Tables(6)
                gvDetalleHospitalizacion.DataBind()
                ViewState("ListaHospitalizacion") = ds_Lista.Tables(6)
            Else
                gvDetalleHospitalizacion.DataBind()
            End If


            'Operacion
            If ds_Lista.Tables(7).Rows(0).Item("CodigoRelFichaMedOperaciones") <> -1 Then
                gvDetalleOperacion.DataSource = ds_Lista.Tables(7)
                gvDetalleOperacion.DataBind()
                ViewState("ListaOperacion") = ds_Lista.Tables(7)
            Else
                gvDetalleOperacion.DataBind()
            End If

            '3...OTROS DATOS MEDICOS

            If ds_Lista.Tables(0).Rows(0).Item("TabiqueDesviado").ToString = "1" Then
                lbl_TabiqueDesviado.Text = "Si"
            ElseIf ds_Lista.Tables(0).Rows(0).Item("TabiqueDesviado").ToString = "0" Then
                lbl_TabiqueDesviado.Text = "No"
            End If

            If ds_Lista.Tables(0).Rows(0).Item("SangradoNasal").ToString = "1" Then
                lbl_SangradoNasal.Text = "Si"
            ElseIf ds_Lista.Tables(0).Rows(0).Item("SangradoNasal").ToString = "0" Then
                lbl_SangradoNasal.Text = "No"
            End If

            lbl_ObservacionesOftamologicas.Text = ds_Lista.Tables(0).Rows(0).Item("ObservacionesOftalmologicas").ToString

            If ds_Lista.Tables(0).Rows(0).Item("UsaLentes").ToString = "1" Then
                lbl_UsaLentes.Text = "Si"
            ElseIf ds_Lista.Tables(0).Rows(0).Item("UsaLentes").ToString = "0" Then
                lbl_UsaLentes.Text = "No"
            End If
            lbl_ObservacionesDental.Text = ds_Lista.Tables(0).Rows(0).Item("ObservacionesDental").ToString

            If ds_Lista.Tables(0).Rows(0).Item("UsaOrtodoncia").ToString = "1" Then
                lbl_UsaOrtodoncia.Text = "Si"
            ElseIf ds_Lista.Tables(0).Rows(0).Item("UsaOrtodoncia").ToString = "0" Then
                lbl_UsaOrtodoncia.Text = "No"
            End If

            '4...CONTROLES DE SALUD

            'ControlPesoTalla
            If ds_Lista.Tables(8).Rows(0).Item("CodigoControlPesoTalla") <> -1 Then
                gvDetalleControlPesoTalla.DataSource = ds_Lista.Tables(8)
                gvDetalleControlPesoTalla.DataBind()
                ViewState("ListaControlPesoTalla") = ds_Lista.Tables(8)
            Else
                gvDetalleControlPesoTalla.DataBind()
            End If

            'Otros Controles
            If ds_Lista.Tables(9).Rows(0).Item("CodigoRelFichaMedTiposControles") <> -1 Then
                gvDetalleTipoControl.DataSource = ds_Lista.Tables(9)
                gvDetalleTipoControl.DataBind()
                ViewState("ListaTipoControl") = ds_Lista.Tables(9)
            Else
                gvDetalleTipoControl.DataBind()
            End If


        Catch ex As Exception

            'MostrarSexyAlertBox(Alertas.ObtenerMensajaError(4), "Error")

        End Try

    End Sub

    Private Sub Enviar_ActualizarDatos()
        'Dim Context As HttpContext
        'Context = HttpContext.Current
        'Context.Items.Add("CodigoFamiliar", "2")
        'Server.Transfer("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionFichaFamiliar.aspx")
        'Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionFichaFamiliar.aspx")
    End Sub

    'Private Sub BlanquearBotones()
    '    Dim int_Contador As Integer = 0
    '    Dim buton_Opcion As Button

    '    While int_Contador <= dgv_GrupoInformacion.Rows.Count - 1

    '        buton_Opcion = dgv_GrupoInformacion.Rows(int_Contador).FindControl("btn_GrupoInformacion")
    '        buton_Opcion.Style.Value = "cursor:pointer;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/menu/grupoInformacion_itemMenu.jpg') ; background-repeat:no-repeat; "
    '        int_Contador = int_Contador + 1
    '    End While
    'End Sub

#End Region

End Class
