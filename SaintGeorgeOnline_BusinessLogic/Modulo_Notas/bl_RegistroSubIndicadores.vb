Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroSubIndicadores

#Region "Atributos"

        Private obj_da_RegistroSubIndicadores As da_RegistroSubIndicadores

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_RegistroSubIndicadores = New da_RegistroSubIndicadores

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_RegistroSubIndicadores(ByVal obe_RegistroSubIndicadores As be_RegistroSubIndicadores, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Try


                Return New da_RegistroSubIndicadores().FUN_INS_RegistroSubIndicadores(obe_RegistroSubIndicadores, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
            Catch ex As Exception
            Finally

            End Try
        End Function

        Function FUN_UPD_RegistroSubIndicadores(ByVal obe_RegistroSubIndicadores As be_RegistroSubIndicadores, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal b_estado As Boolean) As List(Of String)
            Try
                Return New da_RegistroSubIndicadores().FUN_UPD_RegistroSubIndicadores(obe_RegistroSubIndicadores, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, b_estado)
            Catch ex As Exception
            Finally
            End Try
        End Function

#End Region

#Region "Metodos No Transaccionales"



#End Region

    End Class

End Namespace
