Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_Componentes

#Region "Atributos"

        Private obj_da_Componentes As da_Componentes

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_Componentes = New da_Componentes

        End Sub

#End Region

#Region "Metodos Transacciones"
        Public Function FUN_INS_Componentes(ByVal str_Descripcion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal int_codigoGrupo As Integer) As List(Of String)
            Try
                Return New da_Componentes().FUN_INS_Componentes(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, int_codigoGrupo)
            Catch ex As Exception
            Finally

            End Try

        End Function
        Public Function FUN_UDP_Componentes(ByVal str_Descripcion As String, ByVal int_CodComponente As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Try
                Return New da_Componentes().FUN_UDP_Componentes(str_Descripcion, int_CodComponente, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
            Catch ex As Exception
            Finally
            End Try
        End Function
#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Componentes(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal int_asignacionGrupo As Integer) As DataSet

            Return obj_da_Componentes.FUN_LIS_Componentes(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, int_asignacionGrupo)

        End Function

#End Region

    End Class

End Namespace