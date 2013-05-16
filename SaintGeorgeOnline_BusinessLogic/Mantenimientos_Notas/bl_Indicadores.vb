Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_Indicadores

#Region "Atributos"

        Private obj_da_Indicadores As da_Indicadores

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_Indicadores = New da_Indicadores

        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Indicadores(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Indicadores.FUN_LIS_Indicadores(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_IndicadoresPorComponente(ByVal int_CodigoComponente As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Indicadores.FUN_LIS_IndicadoresPorComponente(int_CodigoComponente, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_Indicadores(ByVal str_DescripcionIndicador As String, ByVal int_CodigoComponente As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Try

                Return New da_Indicadores().FUN_INS_Indicadores(str_DescripcionIndicador, int_CodigoComponente, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

            Catch ex As Exception
            Finally
            End Try
        End Function
        Public Function FUN_UDP_Indicadores(ByVal str_DescripcionIndicador As String, ByVal int_CodigoIndicador As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Try
                Return New da_Indicadores().FUN_UDP_Indicadores(str_DescripcionIndicador, int_CodigoIndicador, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

            Catch
            End Try
        End Function


#End Region

    End Class

End Namespace