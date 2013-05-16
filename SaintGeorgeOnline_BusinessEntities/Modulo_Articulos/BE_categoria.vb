Public Class BE_categoria
    Public CA_CodigoCategoriaArticulo As Integer?
    Public TA_CodigoTipoArticulo As Integer?
    Public CA_Descripcion As String
    Public CA_Estado As Boolean?
    Public Sub New()
        Me.CA_CodigoCategoriaArticulo = Nothing
        Me.TA_CodigoTipoArticulo = Nothing
        Me.CA_Descripcion = String.Empty
        Me.CA_Estado = Nothing
    End Sub
End Class
