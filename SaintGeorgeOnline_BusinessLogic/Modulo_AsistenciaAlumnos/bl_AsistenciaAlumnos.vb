Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloAsistenciaAlumnos
Imports SaintGeorgeOnline_DataAccess.ModuloAsistenciaAlumnos

Namespace ModuloAsistenciaAlumnos

    Public Class bl_AsistenciaAlumnos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsistenciaAlumnos As da_AsistenciaAlumnos

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
            obj_da_AsistenciaAlumnos = New da_AsistenciaAlumnos
        End Sub

#End Region

#Region "Metodos Transacciones"
        Public Function FUN_INS_AsistenciaAlumnos(ByVal objAsistenciaAlumnos As be_AsistenciaAlumnos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsistenciaAlumnos.FUN_INS_AsistenciaAlumnos(objAsistenciaAlumnos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_INS_AsistenciaJustificacion(ByVal objAsistenciaAlumnos As be_AsistenciaAlumnos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsistenciaAlumnos.FUN_INS_AsistenciaJustificacion(objAsistenciaAlumnos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsistenciaAlumnos(ByVal int_AnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal dt_FechaAsistencia As Date, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AsistenciaAlumnos.FUN_LIS_AlumnosXSalon(int_AnioAcademico, int_CodigoAula, dt_FechaAsistencia, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_BimestreXAño(ByVal int_AnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AsistenciaAlumnos.FUN_LIS_BimestreXAño(int_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace