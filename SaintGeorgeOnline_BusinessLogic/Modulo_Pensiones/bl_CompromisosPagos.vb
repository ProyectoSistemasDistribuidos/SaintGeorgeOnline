Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones
    'Ok Edgar
    Public Class bl_CompromisosPagos
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_CompromisosPagos As da_CompromisosPagos

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
            obj_da_CompromisosPagos = New da_CompromisosPagos
        End Sub

#End Region

#Region "Metodos Transacciones"

        'Public Function FUN_INS_DetalleCompromisoPago(ByVal CompromisosPagosDetalle As be_CompromisosPagosDetalle, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
        '    Return obj_da_CompromisosPagos.FUN_INS_DetalleCompromisoPago(CompromisosPagosDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        'End Function

        Public Function FUN_INS_CompromisoPago(ByVal objCompromisosPagos As be_CompromisosPagos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CompromisosPagos.FUN_INS_CompromisoPago(objCompromisosPagos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_CompromisoPago(ByVal objCompromisosPagos As be_CompromisosPagos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CompromisosPagos.FUN_UPD_CompromisoPago(objCompromisosPagos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

       
        Public Function FUN_DEL_CompromisosPagos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CompromisosPagos.FUN_DEL_CompromisosPagos(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        'Public Function FUN_ACT_CompromisosPagos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
        '    Return obj_da_CompromisosPagos.FUN_ACT_CompromisosPagos(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        'End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_CompromisoPagoXFamiliar(ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_CompromisosPagos.FUN_LIS_CompromisoPagoXFamiliar( _
                int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_CompromisoPago(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoFamiliar As Integer, ByVal dt_FechaRegistroCPIni As Date, _
                                                ByVal dt_FechaRegistroCPFin As Date, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_CompromisosPagos.FUN_LIS_CompromisoPago(int_CodigoFamilia, int_CodigoFamiliar, dt_FechaRegistroCPIni, dt_FechaRegistroCPFin, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_CompromisosPagos(ByVal int_CodigoCompromisoPago As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_CompromisosPagos.FUN_GET_CompromisosPagos(int_CodigoCompromisoPago, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_CompromisoPagoXFamiliaFamiliar(ByVal int_CodigoFamilia As Integer, _
                                                                            ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, _
                                                                            ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, _
                                                                            ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_CompromisosPagos.FUN_LIS_CompromisoPagoXFamiliaFamiliar(int_CodigoFamilia, int_CodigoFamiliar, int_CodigoUsuario, _
                                                                                  int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region
    End Class

End Namespace