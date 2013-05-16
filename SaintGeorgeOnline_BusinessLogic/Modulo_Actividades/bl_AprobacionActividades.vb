Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloActividades
Imports SaintGeorgeOnline_DataAccess.ModuloActividades

Namespace ModuloActividades

Public Class bl_AprobacionActividades

#Region "Atributos"

        Private obj_da_AprobacionActividades As da_AprobacionActividades

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_AprobacionActividades = New da_AprobacionActividades
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AprobacionActividades( _
            ByVal obe_AprobacionActividades As be_AprobacionActividades, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AprobacionActividades.FUN_INS_AprobacionActividades(obe_AprobacionActividades, str_Mensaje, _
                                                                              int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AprobacionActividadesCoordinacion( _
            ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoMes As Integer, ByVal int_Estado As Integer, ByVal int_CodigoTrabajador As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AprobacionActividades.FUN_LIS_AprobacionActividadesCoordinacion(int_CodigoPeriodo, int_CodigoMes, int_Estado, int_CodigoTrabajador, _
                                                                                          int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace