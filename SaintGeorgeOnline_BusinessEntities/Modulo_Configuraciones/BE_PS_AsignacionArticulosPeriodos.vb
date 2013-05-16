Public Class BE_PS_AsignacionArticulosPeriodos
    Public AAP_CodigoEstructuraArticulo As Integer?
    Public ASP_CodigoEstructuraSubCategoria As Integer?
    Public AT_CodigoItem As Integer?
    Public AAP_PrecioArticulo As Decimal?
    Public MD_CodigoMoneda As Integer?
    Public AAP_UnidadMedida As String
    Public SI_CodigoSituacionItem As Integer?

    Public Sub New()
        Me.AAP_CodigoEstructuraArticulo = Nothing
        Me.ASP_CodigoEstructuraSubCategoria = Nothing
        Me.AT_CodigoItem = Nothing
        Me.AAP_PrecioArticulo = Nothing
        Me.MD_CodigoMoneda = Nothing
        Me.AAP_UnidadMedida = String.Empty
        Me.SI_CodigoSituacionItem = Nothing
    End Sub


End Class
