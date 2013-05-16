Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroEvaluacionesCuantitativos

#Region "Atributos"

        Private obj_da_RegistroEvaluacionesCuantitativos As da_RegistroEvaluacionesCuantitativos

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_RegistroEvaluacionesCuantitativos = New da_RegistroEvaluacionesCuantitativos

        End Sub

#End Region

#Region "Metodos Transacciones"

        'Public Function FUN_INS_RegistroEvaluacionesCuantitativos(ByVal dt_Lista As DataTable, _
        '    ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

        '    Return obj_da_RegistroEvaluacionesCuantitativos.FUN_INS_RegistroEvaluacionesCuantitativos(dt_Lista, _
        '        int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        'End Function

        Public Function FUN_INS_RegistroEvaluacionesCuantitativosIndividual(ByVal obj_be_RegistroEvaluacion As be_RegistroEvaluacionesCuantitativos, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroEvaluacionesCuantitativos.FUN_INS_RegistroEvaluacionesCuantitativosIndividual(obj_be_RegistroEvaluacion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_RegistroEvaluacionesCuantitativos(ByVal arr_Descriptores As List(Of be_RegistroEvaluacionesCuantitativos), _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroEvaluacionesCuantitativos.FUN_INS_RegistroEvaluacionesCuantitativos(arr_Descriptores, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_RegistroEvaluacionesCuantitativos(ByVal int_CodigoRegistroEvaluacion As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroEvaluacionesCuantitativos.FUN_DEL_RegistroEvaluacionesCuantitativos(int_CodigoRegistroEvaluacion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_RegistroEvaluacionesCuantitativos(ByVal int_CodigoRegistroCuantitativo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RegistroEvaluacionesCuantitativos.FUN_LIS_RegistroEvaluacionesCuantitativos( _
                int_CodigoRegistroCuantitativo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace
