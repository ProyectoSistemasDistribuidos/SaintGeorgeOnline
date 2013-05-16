Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

'k

Namespace ModuloPensiones

    Public Class bl_CompromisosPagosDetalle

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_CompromisosPagosDetalle As da_CompromisosPagosDetalle

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
            obj_da_CompromisosPagosDetalle = New da_CompromisosPagosDetalle
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_DetalleCompromisoPago(ByVal CompromisosPagosDetalleDetalle As be_CompromisosPagosDetalle, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CompromisosPagosDetalle.FUN_INS_DetalleCompromisoPago(CompromisosPagosDetalleDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_DetalleCompromisoPago(ByVal objCompromisosPagosDetalle As be_CompromisosPagosDetalle, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CompromisosPagosDetalle.FUN_UPD_DetalleCompromisoPago(objCompromisosPagosDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_DetalleCompromisoPago(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CompromisosPagosDetalle.FUN_DEL_DetalleCompromisoPago(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function



#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_DetalleCompromisos(ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_CompromisosPagosDetalle.FUN_LIS_DetalleCompromisos( _
                int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_DetalleCompromisosPagoXCP(ByVal int_CodigoCP As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_CompromisosPagosDetalle.FUN_LIS_DetalleCompromisosPagoXCP( _
                int_CodigoCP, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_ConceptosDetalleCompromisoPago(ByVal int_AnioAcademico As Integer, ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_CompromisosPagosDetalle.FUN_GET_ConceptosDetalleCompromisoPago(int_AnioAcademico, str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace