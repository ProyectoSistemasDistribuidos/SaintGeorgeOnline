Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class bl_RelacionPresentacionesUnidadesMedidasMedicamentos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_RelacionPresentacionesUnidadesMedidasMedicamentos As da_RelacionPresentacionesUnidadesMedidasMedicamentos

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
            obj_da_RelacionPresentacionesUnidadesMedidasMedicamentos = New da_RelacionPresentacionesUnidadesMedidasMedicamentos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_RelacionPresentacionUnidadMedidaMedicamento( _
            ByVal objRelacionPresentacionesUnidadesMedidasMedicamentos As be_RelacionPresentacionesUnidadesMedidasMedicamentos, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_RelacionPresentacionesUnidadesMedidasMedicamentos.FUN_INS_RelacionPresentacionUnidadMedidaMedicamento( _
                    objRelacionPresentacionesUnidadesMedidasMedicamentos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_RelacionPresentacionUnidadMedidaMedicamento( _
            ByVal objRelacionPresentacionesUnidadesMedidasMedicamentos As be_RelacionPresentacionesUnidadesMedidasMedicamentos, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_RelacionPresentacionesUnidadesMedidasMedicamentos.FUN_UPD_RelacionPresentacionUnidadMedidaMedicamento( _
            objRelacionPresentacionesUnidadesMedidasMedicamentos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_RelacionPresentacionUnidadMedidaMedicamento( _
            ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_RelacionPresentacionesUnidadesMedidasMedicamentos.FUN_DEL_RelacionPresentacionUnidadMedidaMedicamento(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_RelacionPresentacionUnidadMedidaMedicamento( _
            ByVal int_CodigoPresentacion As Integer, _
            ByVal int_CodigoUnidadMedida As Integer, _
            ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RelacionPresentacionesUnidadesMedidasMedicamentos.FUN_LIS_RelacionPresentacionUnidadMedidaMedicamento( _
            int_CodigoPresentacion, int_CodigoUnidadMedida, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_RelacionPresentacionUnidadMedidaMedicamento( _
            ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RelacionPresentacionesUnidadesMedidasMedicamentos.FUN_GET_RelacionPresentacionUnidadMedidaMedicamento(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_PresentacionMedicaDisponible(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_RelacionPresentacionesUnidadesMedidasMedicamentos.FUN_LIS_PresentacionMedicaDisponible(int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_UnidadMedidaxPresentacionMedicaDisponible( _
            ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RelacionPresentacionesUnidadesMedidasMedicamentos.FUN_LIS_UnidadMedidaxPresentacionMedicaDisponible(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace