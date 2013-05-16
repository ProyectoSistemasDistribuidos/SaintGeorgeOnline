Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess

Public Class bl_RegistroNotasBimestralesCualitativas

    Public Function FUN_INS_RegistroNotasBimestralesCualitativas(ByVal obe_RegistroNotasBimestralesCualitativas As be_RegistroNotasBimestralesCualitativas, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
        Try
            Return New da_RegistroNotasBimestralesCualitativas().FUN_INS_RegistroNotasBimestralesCualitativas(obe_RegistroNotasBimestralesCualitativas, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        Catch ex As Exception


        Finally
        End Try
    End Function

    Public Function FUN_LIS_RegistroNotasBimestralesCualitativas(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
        Try

            Return New da_RegistroNotasBimestralesCualitativas().FUN_LIS_RegistroNotasBimestralesCualitativas(int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        Catch ex As Exception

        End Try
    End Function

    Public Function FUN_UPD_RegistroNotasBimestralesCualitativas(ByVal int_idBimestre As Integer, ByVal str_Observacion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
        Dim lstResultado As New List(Of String)
        Try

            Return New da_RegistroNotasBimestralesCualitativas().FUN_UPD_RegistroNotasBimestralesCualitativas(int_idBimestre, str_Observacion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        Catch

        Catch ex As Exception

        End Try
    End Function
End Class
