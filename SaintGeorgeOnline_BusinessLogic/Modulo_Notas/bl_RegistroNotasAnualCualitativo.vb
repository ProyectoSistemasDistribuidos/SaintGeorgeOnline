Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class bl_RegistroNotasAnualCualitativo

    Public Function FUN_INS_RegistroNotasAnualCualitativo(ByVal obe_RegistroNotasAnualCualitativo As be_RegistroNotasAnualCualitativo, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal bimestre As Integer) As List(Of String)
        Try
            Return New da_RegistroNotasAnualCualitativo().FUN_INS_RegistroNotasAnualCualitativo(obe_RegistroNotasAnualCualitativo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, bimestre)
        Catch ex As Exception

        End Try
    End Function
End Class
