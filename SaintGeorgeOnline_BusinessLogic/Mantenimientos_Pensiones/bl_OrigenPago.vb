Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_OrigenPago

#Region "Atributos"

        Private obj_da_OrigenPago As da_OrigenPago

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_OrigenPago = New da_OrigenPago

        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_OrigenPago(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_OrigenPago.FUN_LIS_OrigenPago(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace