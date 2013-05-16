Public Class BE_Subcategoria
    Public SCA_CodigoSubCategoriaArticulo As Integer
    Public CA_CodigoCategoriaArticulo As Integer
    Public SCA_Descripcion As String
    Public SCA_Estado As Boolean
    Public Sub New()
        Me.SCA_CodigoSubCategoriaArticulo = Nothing
        Me.CA_CodigoCategoriaArticulo = Nothing
        Me.SCA_Descripcion = String.Empty
        Me.SCA_Estado = Nothing
    End Sub
End Class
