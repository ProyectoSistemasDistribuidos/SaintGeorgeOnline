Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common

Namespace Utilitario

    Public Class da_Generico
        Inherits InstanciaConexion.ManejadorConexion

        Private dbBase As SqlDatabase 'ExecuteDataSet
        Private dbCommand As DbCommand 'ExecuteScalar

        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
        End Sub

        Public Function Fun_Lis_Generico(ByVal dcParametros As Dictionary(Of String, Object), ByVal nombreProcedure As String) As DataSet
            Try
                Using dbCommand As DbCommand = dbBase.GetStoredProcCommand(nombreProcedure)
                    For Each ClaveValor As KeyValuePair(Of String, Object) In dcParametros
                        dbBase.AddInParameter(dbCommand, "@" & ClaveValor.Key, _TipoDato(dcParametros.Item(ClaveValor.Key)), ClaveValor.Value)
                    Next
                    Return dbBase.ExecuteDataSet(dbCommand)
                End Using
            Catch ex As Exception
            Finally
            End Try
        End Function

        Private Function _TipoDato(ByVal oTtype As Object) As DbType
            Dim ot As Type = oTtype.GetType
            If ot.Name = "Int32" Then
                Return DbType.Int32
            ElseIf ot.Name = "Decimal" Then
                Return DbType.Decimal
            ElseIf ot.Name = "Double" Then
                Return DbType.Double
            ElseIf ot.Name = "DateTime" Then
                Return DbType.DateTime
            ElseIf ot.Name = "Boolean" Then
                Return DbType.Boolean
            ElseIf ot.Name = "Date" Then
                Return DbType.Date
            ElseIf ot.Name = "Int16" Then
                Return DbType.Int16
            ElseIf ot.Name = "String" Then
                Return DbType.String
            End If
        End Function

    End Class

End Namespace