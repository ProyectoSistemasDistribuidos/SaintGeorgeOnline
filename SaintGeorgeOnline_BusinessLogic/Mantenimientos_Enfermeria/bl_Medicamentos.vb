Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class bl_Medicamentos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Medicamento As da_Medicamentos

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
            obj_da_Medicamento = New da_Medicamentos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_Medicamento(ByVal objMedicamento As be_Medicamentos, _
                                            ByRef str_Mensaje As String, _
                                            ByVal int_CodigoUsuario As Integer, _
                                            ByVal int_CodigoTipoUsuario As Integer, _
                                            ByVal int_CodigoModulo As Integer, _
                                            ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Medicamento.FUN_INS_Medicamento(objMedicamento, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_Medicamento(ByVal objMedicamento As be_Medicamentos, _
                                            ByRef str_Mensaje As String, _
                                            ByVal int_CodigoUsuario As Integer, _
                                            ByVal int_CodigoTipoUsuario As Integer, _
                                            ByVal int_CodigoModulo As Integer, _
                                            ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Medicamento.FUN_UPD_Medicamento(objMedicamento, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_Medicamento(ByVal int_Codigo As Integer, _
                                            ByRef str_Mensaje As String, _
                                            ByVal int_CodigoUsuario As Integer, _
                                            ByVal int_CodigoTipoUsuario As Integer, _
                                            ByVal int_CodigoModulo As Integer, _
                                            ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Medicamento.FUN_DEL_Medicamento(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Medicamento(ByVal int_CodigoNombre As Integer, _
                                            ByVal int_CodigoPresentacion As Integer, _
                                            ByVal int_CodigoUnidadMedida As Integer, _
                                            ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Medicamento.FUN_LIS_Medicamento(int_CodigoNombre, int_CodigoPresentacion, int_CodigoUnidadMedida, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_Medicamento(ByVal int_Codigo As Integer, _
                                            ByVal int_CodigoUsuario As Integer, _
                                            ByVal int_CodigoTipoUsuario As Integer, _
                                            ByVal int_CodigoModulo As Integer, _
                                            ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Medicamento.FUN_GET_Medicamento(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
