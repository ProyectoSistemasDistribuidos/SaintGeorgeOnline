Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos

Namespace ModuloPermisos

    Public Class bl_NombresAcciones

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_NombresAcciones As da_NombresAcciones

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
            obj_da_NombresAcciones = New da_NombresAcciones
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_NombresAcciones(ByVal objNombresAcciones As be_NombresAcciones, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_NombresAcciones.FUN_INS_NombresAcciones(objNombresAcciones, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_NombresAcciones(ByVal objNombresAcciones As be_NombresAcciones, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_NombresAcciones.FUN_UPD_NombresAcciones(objNombresAcciones, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_NombresAcciones(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_NombresAcciones.FUN_DEL_NombresAcciones(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_NombresAcciones(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_NombresAcciones.FUN_LIS_NombresAcciones(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_NombresAcciones(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_NombresAcciones.FUN_GET_NombresAcciones(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace

