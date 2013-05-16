Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloReportes

Namespace ModuloReportes

    Public Class bl_Reportes

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Reportes As da_Reportes

#End Region

#Region "Propiedades"

        Public ReadOnly Property Mensaje() As String
            Get
                Return str_Mensaje
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_Reportes = New da_Reportes
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Reportes(ByVal int_CodigoTipoReporte As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Reportes.FUN_LIS_Reportes(int_CodigoTipoReporte, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FunListarJalados() As DataSet
            Try


                Return New da_Reportes().FunListarJalados()
            Catch ex As Exception

            End Try

        End Function
#End Region

    End Class

End Namespace