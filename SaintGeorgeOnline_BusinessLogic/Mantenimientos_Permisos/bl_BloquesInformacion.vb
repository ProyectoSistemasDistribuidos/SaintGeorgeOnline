Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos

Namespace ModuloPermisos

    Public Class bl_BloquesInformacion

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_BloquesInformacion As da_BloquesInformacion

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
            obj_da_BloquesInformacion = New da_BloquesInformacion
        End Sub

#End Region

#Region "Métodos Transaccionales"

        Public Function FUN_INS_BloquesInformaciones(ByVal objBloquesInformaciones As be_BloquesInformaciones, ByVal objDetalle As DataSet, _
                                                     ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_BloquesInformacion.FUN_INS_BloquesInformaciones(objBloquesInformaciones, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_BloquesInformaciones(ByVal objBloquesInformaciones As be_BloquesInformaciones, ByVal objDetalle As DataSet, _
                                                     ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_BloquesInformacion.FUN_UPD_BloquesInformaciones(objBloquesInformaciones, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_BloquesInformaciones(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_BloquesInformacion.FUN_DEL_BloquesInformaciones(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_BloquesInformacion(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_Tipo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_BloquesInformacion.FUN_LIS_BloquesInformacion(str_Descripcion, int_Estado, int_Tipo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_BloquesInformacion(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_BloquesInformacion.FUN_GET_BloquesInformacion(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
