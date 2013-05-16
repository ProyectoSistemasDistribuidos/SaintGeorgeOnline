Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloFotocopia
Imports SaintGeorgeOnline_DataAccess.ModuloFotocopias

Namespace ModuloFotocopias

    Public Class bl_DetalleSolicitudFotocopias

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_DetalleSolicitudFotocopias As da_DetalleSolicitudFotocopias

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
            obj_da_DetalleSolicitudFotocopias = New da_DetalleSolicitudFotocopias
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_DetalleSolicitudFotocopias(ByVal objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_DetalleSolicitudFotocopias.FUN_INS_DetalleSolicitudFotocopias(objDetalleSolicitudFotocopias, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_DetalleSolicitudFotocopias(ByVal objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_DetalleSolicitudFotocopias.FUN_UPD_DetalleSolicitudFotocopias(objDetalleSolicitudFotocopias, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_DetalleSolicitudFotocopias(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_DetalleSolicitudFotocopias.FUN_DEL_DetalleSolicitudFotocopias(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"


        Public Function FUN_LIS_DetalleSolicitudFotocopias(ByVal dt_Fecha As Date, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_DetalleSolicitudFotocopias.FUN_LIS_DetalleSolicitudFotocopias(dt_Fecha, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        'Public Function FUN_GET_Houses(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
        '    Return obj_da_House.FUN_GET_Houses(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        'End Function

#End Region
    End Class

End Namespace