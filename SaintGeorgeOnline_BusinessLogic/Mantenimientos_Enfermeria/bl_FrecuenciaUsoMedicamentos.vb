Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class bl_FrecuenciaUsoMedicamentos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_FrecuenciaUsoMedicamento As da_FrecuenciaUsoMedicamentos

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
            obj_da_FrecuenciaUsoMedicamento = New da_FrecuenciaUsoMedicamentos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_FrecuenciaUsoMedicamento(ByVal objFrecuenciaUsoMedicamento As be_FrecuenciaUsoMedicamentos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_FrecuenciaUsoMedicamento.FUN_INS_FrecuenciaUsoMedicamento(objFrecuenciaUsoMedicamento, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_FrecuenciaUsoMedicamento(ByVal objFrecuenciaUsoMedicamento As be_FrecuenciaUsoMedicamentos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_FrecuenciaUsoMedicamento.FUN_UPD_FrecuenciaUsoMedicamento(objFrecuenciaUsoMedicamento, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_FrecuenciaUsoMedicamento(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_FrecuenciaUsoMedicamento.FUN_DEL_FrecuenciaUsoMedicamento(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_FrecuenciaUsoMedicamento(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_FrecuenciaUsoMedicamento.FUN_LIS_FrecuenciaUsoMedicamento(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_FrecuenciaUsoMedicamento(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_FrecuenciaUsoMedicamento.FUN_GET_FrecuenciaUsoMedicamento(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
