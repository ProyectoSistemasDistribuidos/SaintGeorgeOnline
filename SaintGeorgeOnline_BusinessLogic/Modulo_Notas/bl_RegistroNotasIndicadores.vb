Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class bl_RegistroNotasIndicadores

    Public Function FUN_ACT_RegistroNotaIndicador(ByVal obe_CU_RegistroNotasIndicadores As be_CU_RegistroNotasIndicadores, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
        Try

            Return New da_RegistroNotasIndicadores().FUN_ACT_RegistroNotaIndicador(obe_CU_RegistroNotasIndicadores, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        Catch ex As Exception
        Finally
        End Try
    End Function



End Class
