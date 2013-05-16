Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_NotaCreditoDebito

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_NotaCreditoDebito As da_NotaCreditoDebito

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
            obj_da_NotaCreditoDebito = New da_NotaCreditoDebito
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_NotaCredito(ByVal objNotaCredito As be_NotaCreditoDebito, ByVal objDetalle As DataSet, ByRef str_ValorNumeroNotaCredito As String, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_NotaCreditoDebito.FUN_INS_NotaCredito(objNotaCredito, objDetalle, str_ValorNumeroNotaCredito, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_NotaDebito(ByVal objNotaDebito As be_NotaCreditoDebito, ByVal objDetalle As DataSet, ByRef str_ValorNumeroNotaCredito As String, ByRef str_Mensaje As String, _
    ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_NotaCreditoDebito.FUN_INS_NotaDebito(objNotaDebito, objDetalle, str_ValorNumeroNotaCredito, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_DEL_NotaDebitoLetra(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_NotaCreditoDebito.FUN_DEL_NotaDebitoLetra(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_NotasLetra(ByVal str_NumeroIni As String, ByVal str_NumeroFin As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_NotaCreditoDebito.FUN_LIS_NotasLetra(str_NumeroIni, str_NumeroFin, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace
