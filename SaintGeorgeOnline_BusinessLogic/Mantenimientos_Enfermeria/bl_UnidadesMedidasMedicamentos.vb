Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class bl_UnidadesMedidasMedicamentos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_UnidadesMedidasMedicamentos As da_UnidadesMedidasMedicamentos

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
            obj_da_UnidadesMedidasMedicamentos = New da_UnidadesMedidasMedicamentos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_UnidadMedidaMedicamento(ByVal objUnidadMedidaMedicamento As be_UnidadesMedidasMedicamentos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_UnidadesMedidasMedicamentos.FUN_INS_UnidadMedidaMedicamento(objUnidadMedidaMedicamento, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_UnidadMedidaMedicamento(ByVal objUnidadMedidaMedicamento As be_UnidadesMedidasMedicamentos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_UnidadesMedidasMedicamentos.FUN_UPD_UnidadMedidaMedicamento(objUnidadMedidaMedicamento, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_UnidadMedidaMedicamento(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_UnidadesMedidasMedicamentos.FUN_DEL_UnidadMedidaMedicamento(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_UnidadMedidaMedicamento(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_UnidadesMedidasMedicamentos.FUN_LIS_UnidadMedidaMedicamento(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_UnidadMedidaMedicamento(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_UnidadesMedidasMedicamentos.FUN_GET_UnidadMedidaMedicamento(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
