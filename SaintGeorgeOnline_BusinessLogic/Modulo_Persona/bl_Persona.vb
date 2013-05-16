
Imports SaintGeorgeOnline_DataAccess

Public Class bl_Persona
    Public Function F_insertarPersona(ByVal dcPersona As Dictionary(Of String, Object)) As Dictionary(Of String, String)

        Try
            Return New DA_persona().F_insertarPersona(dcPersona)
        Catch ex As Exception

        End Try

    End Function

    Public Function F_insetarPerfil(ByVal dcPerfil As Dictionary(Of String, Object)) As Integer


        Try
            Return New DA_persona().F_insetarPerfil(dcPerfil)
        Catch ex As Exception

        End Try
    End Function
    Public Function F_ActualizarUsuarioPass(ByVal dcUsuarioPass As Dictionary(Of String, Object)) As Integer
        Try
            Return New DA_persona().F_ActualizarUsuarioPass(dcUsuarioPass)
        Catch ex As Exception

        End Try
    End Function


End Class
