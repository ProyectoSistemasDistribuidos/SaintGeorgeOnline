Public Class be_articulo
    Public AT_CodigoArticulo As Integer?
    Public CL_CodigoColor As Integer?
    Public MC_CodigoMarca As Integer?
    'Public MD_CodigoModelo As Integer?
    Public UM_CodigoUnidadMedida As Integer?
    Public TA_CodigoTipoArticulo As Integer?
    Public PS_CodigoPresentacion As Integer?
    Public AT_Especificaciones As String
    Public AT_Foto As String
    Public AT_StockActual As Integer?
    Public AT_ActivoFijo As Boolean?
    Public AT_FechaCreacion As DateTime?

    Public AT_PrecioUnitario As Decimal?
    Public AT_NumeroSerie As String
    Public AT_CodigoBarras As String
    Public AT_Modelo As String
    Public BJ_CodigoMotivoBaja As Integer?
    Public ET_CodigoEstadoArticulo As Integer?
    Public AT_Observacion As String
    Public NA_CodigoNombreArticuloInventario As Integer
    Public CA_CodigoCategoriaArticulo As Integer?
    Public AT_Cantidad As Integer?

    Public AT_NumeroParte As String
    Public AT_Estado As Boolean?

    ''
    Public AT_ConGarantia As Boolean?
    Public AT_FechaIngreso As DateTime?
    Public AT_CantidadMesesGarantia As Integer?
    Public SCA_CodigoSubCategoriaArticulo As Integer?

    Public TJ_CodigoTrabajadorRegistrador As Integer?


    ''modificacion  de fuente 23/11/2012 hora :09:49am
    Public AT_IdOrden As Integer?
    Public AT_NumOrden As String
    Public AT_nFacID As Integer?
    Public AT_Documento As String
    Public AT_NumGuiaRemision As String
    Public PV_CodigoProveedor As Integer?
    Public PV_Descripcion As String
    ''
    Public Sub New()
        ''
        Me.AT_CodigoArticulo = Nothing
        Me.CL_CodigoColor = Nothing
        Me.MC_CodigoMarca = Nothing
        ' Me.MD_CodigoModelo = Nothing
        Me.UM_CodigoUnidadMedida = Nothing
        Me.TA_CodigoTipoArticulo = Nothing
        Me.PS_CodigoPresentacion = Nothing
        Me.AT_Especificaciones = String.Empty
        Me.AT_Foto = String.Empty
        Me.AT_StockActual = Nothing
        Me.AT_ActivoFijo = Nothing
        Me.AT_FechaCreacion = Nothing

        Me.AT_PrecioUnitario = Nothing
        Me.AT_NumeroSerie = String.Empty
        Me.AT_CodigoBarras = String.Empty
        Me.AT_Modelo = String.Empty
        Me.BJ_CodigoMotivoBaja = Nothing
        Me.ET_CodigoEstadoArticulo = Nothing
        Me.AT_Observacion = String.Empty
        Me.AT_Cantidad = Nothing

        Me.NA_CodigoNombreArticuloInventario = Nothing

        Me.AT_NumeroParte = String.Empty
        Me.AT_Estado = Nothing
        Me.CA_CodigoCategoriaArticulo = Nothing
        Me.TJ_CodigoTrabajadorRegistrador = Nothing


        Me.AT_IdOrden = Nothing
        Me.AT_NumOrden = Nothing
        Me.AT_nFacID = Nothing
        Me.AT_Documento = String.Empty
        Me.AT_NumGuiaRemision = String.Empty
        Me.PV_CodigoProveedor = Nothing


        Me.PV_Descripcion = String.Empty
        ''
    End Sub
End Class
