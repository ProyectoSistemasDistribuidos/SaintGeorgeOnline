Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess

Public Class bl_articulo


    Public Function FUN_INS_Articulo(ByVal obe_articulo As be_articulo, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
        Dim oda_articulo As New da_articulo
        Try
            Return oda_articulo.FUN_INS_Articulo(obe_articulo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        Catch ex As Exception
        Finally
        End Try
    End Function
    Public Function FUN_DEL_EliminarArtiuculo(ByVal codArticulo As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
        Try
            Dim oda_articulo As New da_articulo
            Return oda_articulo.FUN_DEL_EliminarArtiuculo(codArticulo, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        Catch ex As Exception

        End Try


    End Function


#Region "no trasaccional "

    Public Function FUN_Lis_Articulo(ByVal obe_articulo As be_articulo, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
        Dim oda_articulo As New da_articulo
        Try

            Return oda_articulo.FUN_Lis_Articulo(obe_articulo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        Catch ex As Exception
        Finally
        End Try
    End Function

#End Region
End Class
