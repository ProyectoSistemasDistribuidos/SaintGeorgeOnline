Imports SaintGeorgeOnline_DataAccess.Utilitario

Namespace Utilitario

    Public Class bl_Generico

        Private oda_Generico As da_Generico

        Public Sub New()
            oda_Generico = New da_Generico
        End Sub

        Public Function Fun_Lis_Generico(ByVal dcParametros As Dictionary(Of String, Object), ByVal nombreProcedure As String) As DataSet
            Try
                Return oda_Generico.Fun_Lis_Generico(dcParametros, nombreProcedure)
            Catch ex As Exception
            Finally
            End Try
        End Function

    End Class

End Namespace
