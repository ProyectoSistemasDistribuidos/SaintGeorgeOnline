Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroNotasEvaluaciones

#Region "Atributos"

        Private obj_da_RegistroNotasEvaluaciones As da_RegistroNotasEvaluaciones

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_RegistroNotasEvaluaciones = New da_RegistroNotasEvaluaciones

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_UPD_CU_RegistroNotasEvaluaciones(ByVal obe_RegistroNotasEvaluaciones As be_RegistroNotasEvaluaciones, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroNotasEvaluaciones.FUN_UPD_CU_RegistroNotasEvaluaciones(obe_RegistroNotasEvaluaciones, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_CU_RecalcularNotasEvaluaciones(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroNotasEvaluaciones.FUN_UPD_CU_RecalcularNotasEvaluaciones(int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoTrabajador, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_CU_RegistroListaNotasEvaluaciones(ByVal arr_NotasEvaluaciones As List(Of be_RegistroNotasEvaluaciones), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroNotasEvaluaciones.FUN_UPD_CU_RegistroListaNotasEvaluaciones(arr_NotasEvaluaciones, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Nota Examen
        Public Function FUN_UPD_CU_RegistroNotasExamen(ByVal obe_RegistroNotasEvaluaciones As be_RegistroNotasEvaluaciones, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroNotasEvaluaciones.FUN_UPD_CU_RegistroNotasExamen(obe_RegistroNotasEvaluaciones, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_CU_RegistroListaNotasExamenes(ByVal arr_NotasExamenes As List(Of be_RegistroNotasEvaluaciones), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroNotasEvaluaciones.FUN_UPD_CU_RegistroListaNotasExamenes(arr_NotasExamenes, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region

#Region "Metodos No Transaccionales"

#End Region

    End Class

End Namespace