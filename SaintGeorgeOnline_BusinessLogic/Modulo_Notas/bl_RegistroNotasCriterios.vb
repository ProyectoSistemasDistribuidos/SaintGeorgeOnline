Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroNotasCriterios

#Region "Atributos"

        Private obj_da_RegistroNotasCriterios As da_RegistroNotasCriterios

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_RegistroNotasCriterios = New da_RegistroNotasCriterios

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_CU_RegistroNotasCriterios(ByVal dt As DataTable, ByVal lstCriterios As List(Of be_RegistroCriteriosEST), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroNotasCriterios.FUN_INS_CU_RegistroNotasCriterios(dt, lstCriterios, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_CU_RegistroNotasCriteriosyEvaluaciones( ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer,  ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RegistroNotasCriterios.FUN_LIS_CU_RegistroNotasCriteriosyEvaluaciones( _
                int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_LIS_CU_RegistroNotasCriteriosyEvaluacionesSecundaria(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_curso As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Try

                Return New da_RegistroNotasCriterios().FUN_LIS_CU_RegistroNotasCriteriosyEvaluacionesSecundaria(int_CodigoAsignacionGrupo, int_CodigoBimestre, int_curso, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoTipoUsuario, int_CodigoModulo)
            Catch ex As Exception

            End Try

        End Function



#End Region

    End Class

End Namespace