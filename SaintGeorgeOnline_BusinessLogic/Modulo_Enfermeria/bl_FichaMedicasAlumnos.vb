Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class bl_FichaMedicasAlumnos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_FichaMedicasAlumnos As da_FichaMedicasAlumnos


#End Region

#Region "Propiedades"

        Public ReadOnly Property Mensaje() As String
            Get
                Return str_Mensaje
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_FichaMedicasAlumnos = New da_FichaMedicasAlumnos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_UPD_FichaMedicaAlumno(ByVal objFichaMedicaAlumno As be_FichaMedica, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_FichaMedicasAlumnos.FUN_UPD_FichaMedicaAlumno(objFichaMedicaAlumno, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_FichaMedicaActualizacion( _
         ByVal intCodigoFichaMedica As String, ByVal intCodigoPersona As Integer, ByVal intCodigoSolicitud As Integer, ByVal intCodigoPerfil As Integer, _
         ByVal objDT_Cabecera As DataTable, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_FichaMedicasAlumnos.FUN_UPD_FichaMedicaActualizacion(intCodigoFichaMedica, intCodigoPersona, intCodigoSolicitud, intCodigoPerfil, objDT_Cabecera, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Registro de FichaMedicasAlumnos_Temp
        Public Function FUN_INS_FichaMedicasAlumnosTemp(ByVal objSolicitud As be_SolicitudActualizacionFichaMedicaAlumno, _
            ByVal objFichaMedicaAlumno As be_FichaMedica, _
            ByVal str_CadenaCodigoPerfil As String, _
            ByVal objDetalle As DataSet, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_FichaMedicasAlumnos.FUN_INS_FichaMedicasAlumnosTemp(objSolicitud, objFichaMedicaAlumno, str_CadenaCodigoPerfil, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_FichaMedicaAlumno(ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombre As String, ByVal int_estadoAlumno As Integer, ByVal int_Nivel As Integer, ByVal int_SubNivel As Integer, ByVal int_Grado As Integer, ByVal int_Aula As Integer, ByVal int_PeriodoInicio As Integer, ByVal int_PeriodoFin As Integer, ByVal int_Sede As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaMedicasAlumnos.FUN_LIS_FichaMedicaAlumno(str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_estadoAlumno, int_Nivel, int_SubNivel, int_Grado, int_Aula, int_PeriodoInicio, int_PeriodoFin, int_Sede, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_FichaMedicasAlumnos(ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaMedicasAlumnos.FUN_GET_FichaMedicaAlumno(str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        'Validacion de Datos
        Public Function FUN_GET_FichaMedicaActualizacion(ByVal int_Codigo As Integer, ByVal int_CodigoSolicitud As Integer, ByVal int_CodigoPerfil As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaMedicasAlumnos.FUN_GET_FichaMedicaActualizacion(int_Codigo, int_CodigoSolicitud, int_CodigoPerfil, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_FichaMedicaActualizacion(ByVal objMaestroPersona As be_MaestroPersonas, ByVal dt_FechaRangoInicial As Date, ByVal dt_FechaRangoFinal As Date, ByVal int_CodigoPerfil As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaMedicasAlumnos.FUN_LIS_FichaMedicaActualizacion(objMaestroPersona, dt_FechaRangoInicial, dt_FechaRangoFinal, int_CodigoPerfil, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Datos para visualizacion del Familiar
        Public Function FUN_GET_FichaMedicaAlumnoVisualizacionActualizacionFamiliar(ByVal int_CodigoAlumno As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaMedicasAlumnos.FUN_GET_FichaMedicaAlumnoVisualizacionActualizacionFamiliar(int_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace
