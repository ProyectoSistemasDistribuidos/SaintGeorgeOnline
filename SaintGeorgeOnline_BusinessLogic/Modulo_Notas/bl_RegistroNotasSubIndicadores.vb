Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_DataAccess.ModuloNotas
Public Class bl_RegistroNotasSubIndicadores

    Public Function FUN_ACT_RegistroNotaSubIndicador(ByVal obe_registroNotaSubIndicadores As be_registroNotaSubIndicadores, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
        Try

            Return New da_RegistroNotasSubIndicadores().FUN_ACT_RegistroNotaSubIndicador(obe_registroNotaSubIndicadores, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        Catch ex As Exception
        Finally

        End Try
    End Function


End Class
