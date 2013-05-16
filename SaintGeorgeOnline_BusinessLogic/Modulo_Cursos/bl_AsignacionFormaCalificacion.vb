Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloCursos
Imports SaintGeorgeOnline_DataAccess.ModuloCursos

Namespace ModuloCursos

    Public Class bl_AsignacionFormaCalificacion

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionFormaCalificacion As da_AsignacionFormaCalificacion

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
            obj_da_AsignacionFormaCalificacion = New da_AsignacionFormaCalificacion
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionFormaCalificacion(ByVal objAsignacionFormaCalificacion As be_AsignacionFormaCalificacion, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionFormaCalificacion.FUN_INS_AsignacionFormaCalificacion(objAsignacionFormaCalificacion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_AsignacionFormaCalificacion(ByVal objAsignacionFormaCalificacion As be_AsignacionFormaCalificacion, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionFormaCalificacion.FUN_UPD_AsignacionFormaCalificacion(objAsignacionFormaCalificacion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionFormaCalificacion(ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionFormaCalificacion.FUN_LIS_AsignacionFormaCalificacion(int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace