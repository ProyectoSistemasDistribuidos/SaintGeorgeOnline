Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroCriteriosCuantitativos

#Region "Atributos"

        Private obj_da_RegistroCriteriosCuantitativos As da_RegistroCriteriosCuantitativos

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_RegistroCriteriosCuantitativos = New da_RegistroCriteriosCuantitativos

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_RegistroCriteriosCuantitativos(ByVal arr_Criterios As List(Of be_RegistroCriteriosCuantitativos), _
            ByVal int_CodigoAsignacionGrupo As Integer, _
            ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroCriteriosCuantitativos.FUN_INS_RegistroCriteriosCuantitativos( _
                arr_Criterios, int_CodigoAsignacionGrupo, int_CodigoBimestre, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_RegistroCriteriosCuantitativosIndidual( _
            ByVal int_CodigoCriterio As Integer, _
            ByVal int_CodigoAsignacionGrupo As Integer, _
            ByVal int_CodigoBimestre As Integer, _
            ByVal int_Peso As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroCriteriosCuantitativos.FUN_INS_RegistroCriteriosCuantitativosIndidual( _
                int_CodigoCriterio, int_CodigoAsignacionGrupo, int_CodigoBimestre, int_Peso, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_RegistroCriteriosCuantitativos( _
            ByVal int_CodigoRegistroCuantitativo As Integer, _
            ByVal int_CodigoCriterio As Integer, _
            ByVal int_CodigoAsignacionGrupo As Integer, _
            ByVal int_CodigoBimestre As Integer, _
            ByVal int_Peso As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroCriteriosCuantitativos.FUN_UPD_RegistroCriteriosCuantitativos( _
                int_CodigoRegistroCuantitativo, int_CodigoCriterio, int_CodigoAsignacionGrupo, int_CodigoBimestre, int_Peso, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_RegistroCriteriosCuantitativos(ByVal int_CodigoRegistroCuantitativo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroCriteriosCuantitativos.FUN_DEL_RegistroCriteriosCuantitativos(int_CodigoRegistroCuantitativo, _
              int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_RegistroCriteriosCuantitativos(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RegistroCriteriosCuantitativos.FUN_LIS_RegistroCriteriosCuantitativos( _
                int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_InformacionRegistroCriteriosCuantitativos(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RegistroCriteriosCuantitativos.FUN_GET_InformacionRegistroCriteriosCuantitativos( _
                int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_EstructuraRegistroCriteriosyEvaluaciones(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RegistroCriteriosCuantitativos.FUN_LIS_EstructuraRegistroCriteriosyEvaluaciones( _
                int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

