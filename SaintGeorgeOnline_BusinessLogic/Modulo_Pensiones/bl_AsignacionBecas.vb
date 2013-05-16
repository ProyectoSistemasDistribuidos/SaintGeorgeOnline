Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_AsignacionBecas

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionBecas As da_AsignacionBecas

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
            obj_da_AsignacionBecas = New da_AsignacionBecas
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionBecas(ByVal objAsignacionBecas As be_AsignacionBecas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionBecas.FUN_INS_AsignacionBecas(objAsignacionBecas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_AsignacionBecas(ByVal objAsignacionBecas As be_AsignacionBecas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionBecas.FUN_UPD_AsignacionBecas(objAsignacionBecas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_AsignacionBecas(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionBecas.FUN_DEL_AsignacionBecas(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AlumnoPrueba(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionBecas.FUN_LIS_AlumnoPrueba(int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionBecas(ByVal int_CodigoAlumno As Integer, _
                                                ByVal int_CodigoMotivoBeca As Integer, _
                                                ByVal int_CodigoTipoBeca As Integer, _
                                                ByVal int_CodigoAnioAcademico As Integer, _
                                                ByVal int_Grado As Integer, _
                                                ByVal int_Estado As Integer, _
                                                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionBecas.FUN_LIS_AsignacionBecas( _
                int_CodigoAlumno, int_CodigoMotivoBeca, int_CodigoTipoBeca, int_CodigoAnioAcademico, int_Grado, int_Estado, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_AsignacionBecas(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AsignacionBecas.FUN_GET_AsignacionBecas(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


#End Region

    End Class

End Namespace