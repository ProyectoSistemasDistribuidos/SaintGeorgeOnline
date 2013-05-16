Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroComponentes

#Region "Atributos"

        Private obj_da_RegistroComponentes As da_RegistroComponentes

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_RegistroComponentes = New da_RegistroComponentes


        End Sub

#End Region

#Region "Metodos Transacciones"

        Function FUN_INS_RegistroComponentes(ByVal obe_RegistroComponentes As be_RegistroComponentes, ByVal int_pocision As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Try
                Return New da_RegistroComponentes().FUN_INS_RegistroComponentes(obe_RegistroComponentes, int_pocision, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

            Catch ex As Exception

            End Try
        End Function

        Function FUN_UPD_RegistroComponentes(ByVal obe_RegistroComponentes As be_RegistroComponentes, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal b_estado As Boolean) As List(Of String)

            Try
                Return New da_RegistroComponentes().FUN_UPD_RegistroComponentes(obe_RegistroComponentes, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, b_estado)
            Catch ex As Exception
            Finally
            End Try
        End Function
        Public Function FUN_UPD_ActualizarPocisionRegistroComponente(ByVal intPocision As Integer, ByVal intCodRegistroComponente As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal b_estado As Boolean) As List(Of String)
            Try
                Return New da_RegistroComponentes().FUN_UPD_ActualizarPocisionRegistroComponente(intPocision, intCodRegistroComponente, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, b_estado)
            Catch ex As Exception
            Finally

            End Try
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_RegistroComponentes(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal b_estadoComponente As Boolean, ByVal b_estadoIndicador As Boolean, ByVal b_estado_subIndicador As Boolean) As DataSet
            Try

                Return New da_RegistroComponentes().FUN_LIS_RegistroComponentes(int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, b_estadoComponente, b_estadoIndicador, b_estado_subIndicador)

            Catch ex As Exception
            Finally
            End Try
        End Function


#End Region

    End Class

End Namespace
