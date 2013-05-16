Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_Criterios

#Region "Atributos"

        Private obj_da_Criterios As da_Criterios

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_Criterios = New da_Criterios

        End Sub

#End Region

#Region "Metodos Transacciones"
        Public Function FUN_INS_Criterios(ByVal str_Descripcion As String, ByVal int_codigoAsignacionGrupo As Integer, _
                                          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Try
                Return obj_da_Criterios.FUN_INS_Criterios(str_Descripcion, int_codigoAsignacionGrupo, _
                                                          int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
            Catch ex As Exception
            Finally

            End Try

        End Function
#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Criterios(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_codigoAsignacionGrupo As Integer, _
                                          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Criterios.FUN_LIS_Criterios(str_Descripcion, int_Estado, int_codigoAsignacionGrupo, _
                                                      int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace