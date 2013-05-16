Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_SubIndicadores

#Region "Atributos"

        Private obj_da_SubIndicadores As da_SubIndicadores

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_SubIndicadores = New da_SubIndicadores

        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_SubIndicadores(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SubIndicadores.FUN_LIS_SubIndicadores(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_SubIndicadoresPorIndicador(ByVal int_CodigoIndicador As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SubIndicadores.FUN_LIS_SubIndicadoresPorIndicador(int_CodigoIndicador, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_INS_Subindicadores(ByVal str_DescripcionSubIndicador As String, ByVal int_CodigoIndicador As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Try

                Return New da_SubIndicadores().FUN_INS_Subindicadores(str_DescripcionSubIndicador, int_CodigoIndicador, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
            Catch ex As Exception

            End Try
        End Function

        Public Function FUN_UDP_Subindicadores(ByVal str_DescripcionSubIndicador As String, ByVal int_CodigoSubIndicador As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Try

                Return New da_SubIndicadores().FUN_UDP_Subindicadores(str_DescripcionSubIndicador, int_CodigoSubIndicador, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

            Catch ex As Exception
            Finally
            End Try
        End Function
#End Region

    End Class

End Namespace