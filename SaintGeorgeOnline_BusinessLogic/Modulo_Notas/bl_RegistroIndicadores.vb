Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroIndicadores

#Region "Atributos"

        Private obj_da_RegistroIndicadores As da_RegistroIndicadores

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_RegistroIndicadores = New da_RegistroIndicadores

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_RegistroIndicadores(ByVal obe_RegistroIndicadores As be_RegistroIndicadores, ByVal int_orden As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Try
                Return New da_RegistroIndicadores().FUN_INS_RegistroIndicadores(obe_RegistroIndicadores, int_orden, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

            Catch ex As Exception
            Finally
            End Try
        End Function


        Public Function FUN_INS_RegistroIndicadoresPocision(ByVal int_posIndicador As Integer, ByVal codRegIndicador As Integer)
            Try
                Return New da_RegistroIndicadores().FUN_INS_RegistroPocicionIndicadore(int_posIndicador, codRegIndicador)


            Catch ex As Exception
            Finally
            End Try
        End Function

        Public Function FUN_UPD_RegistroIndicadores(ByVal obe_RegistroIndicadores As be_RegistroIndicadores, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal b_estado As Boolean) As List(Of String)
            Try
                Return New da_RegistroIndicadores().FUN_UPD_RegistroIndicadores(obe_RegistroIndicadores, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, b_estado)

            Catch ex As Exception
            Finally

            End Try
        End Function

#End Region

#Region "Metodos No Transaccionales"



#End Region

    End Class

End Namespace

