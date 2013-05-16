Imports System
Imports System.ComponentModel
Public Class BE_PS_DetalleSolicitudArticulos
    Public DSPA_CodigoDetalleSolicitudPresupuestoArticulo As Integer
    Public AAP_CodigoEstructuraArticulo As Integer?
    Public DSP_CodigoDetalleSolicitudPresupuesto As Integer?
    Public PD_CodigoPrioridad As Integer?
    Public DSPA_Cantidad As Decimal?
    Public DSPA_UnidadMedida As String
    Public DSPA_Observacion As String
    Public DSPA_CheckEne As Boolean?
    Public DSPA_CantEne As Decimal?
    Public DSPA_CheckFeb As Boolean?
    Public DSPA_CantFeb As Decimal?
    Public DSPA_CheckMar As Boolean?
    Public DSPA_CantMar As Decimal?
    Public DSPA_CheckAbr As Boolean?
    Public DSPA_CantAbr As Decimal?
    Public DSPA_CheckMay As Boolean?
    Public DSPA_CantMay As Decimal?
    Public DSPA_CheckJun As Boolean?
    Public DSPA_CantJun As Decimal?
    Public DSPA_CheckJul As Boolean?
    Public DSPA_CantJul As Decimal?
    Public DSPA_CheckAgo As Boolean?
    Public DSPA_CantAgo As Decimal?
    Public DSPA_CheckSet As Boolean?
    Public DSPA_CantSet As Decimal?
    Public DSPA_CheckOct As Boolean?
    Public DSPA_CantOct As Decimal?
    Public DSPA_CheckNov As Boolean?
    Public DSPA_CantNov As Decimal?
    Public DSPA_CheckDic As Boolean?
    Public DSPA_CantDic As Decimal?
    Public DSPA_Estado As Boolean?
    Public ESA_CodigoEstadoValidaciones As Integer?
    Public ESA_CantidadArticuloValidado As Decimal?
    Public ESA_ObservacionArticuloValidado As String
    Public ESA_FechaRegistroValidacion As DateTime?
    Public DSPA_Precio As Decimal?
    Public DSPA_PrecioValidado As Decimal?
    Public MD_CodigoMoneda As Integer?
    Public DSPA_UsuarioReasignador As Integer?
    Public DSPA_ObservacionSistemas As String
    Public DSPA_CantidadSistemas As Decimal?
    Public DSPA_CodigoEstadoValidacionSistemas As Integer?
    Public DSPA_PrecioSistemas As Decimal?
    Public DSPA_MonedaSistemas As Decimal?
    Public DSPA_TipoReasignacion As Integer?
    Public DSPA_UnidadSistemas As String
    Public DSPA_TipoValidacion As Integer?
    Public DSPA_TipoDistribucion As Boolean?
    Public listoEnviar As Boolean?


    Public codEstructuraCategoria As Integer?
    Public codEstrcuturaSubCat As Integer?
    Public codEstructuraClase As Integer?
    Public codAsignacionCentroCosto As Integer?


    Public Sub New()
        Me.AAP_CodigoEstructuraArticulo = Nothing
        Me.DSP_CodigoDetalleSolicitudPresupuesto = Nothing
        Me.PD_CodigoPrioridad = Nothing
        Me.DSPA_Cantidad = Nothing
        Me.DSPA_UnidadMedida = String.Empty
        Me.DSPA_Observacion = String.Empty
        Me.DSPA_CheckEne = Nothing
        Me.DSPA_CantEne = Nothing
        Me.DSPA_CheckFeb = Nothing
        Me.DSPA_CantFeb = Nothing
        Me.DSPA_CheckMar = Nothing
        Me.DSPA_CantMar = Nothing
        Me.DSPA_CheckAbr = Nothing
        Me.DSPA_CantAbr = Nothing
        Me.DSPA_CheckMay = Nothing
        Me.DSPA_CantMay = Nothing
        Me.DSPA_CheckJun = Nothing
        Me.DSPA_CantJun = Nothing
        Me.DSPA_CheckJul = Nothing
        Me.DSPA_CantJul = Nothing
        Me.DSPA_CheckAgo = Nothing
        Me.DSPA_CantAgo = Nothing
        Me.DSPA_CheckSet = Nothing
        Me.DSPA_CantSet = Nothing
        Me.DSPA_CheckOct = Nothing
        Me.DSPA_CantOct = Nothing
        Me.DSPA_CheckNov = Nothing
        Me.DSPA_CantNov = Nothing
        Me.DSPA_CheckDic = Nothing
        Me.DSPA_CantDic = Nothing
        Me.DSPA_Estado = Nothing
        Me.ESA_CodigoEstadoValidaciones = Nothing
        Me.ESA_CantidadArticuloValidado = Nothing
        Me.ESA_ObservacionArticuloValidado = String.Empty
        Me.ESA_FechaRegistroValidacion = Nothing
        Me.DSPA_Precio = Nothing
        Me.DSPA_PrecioValidado = Nothing
        Me.MD_CodigoMoneda = Nothing
        Me.DSPA_UsuarioReasignador = Nothing
        Me.DSPA_ObservacionSistemas = String.Empty
        Me.DSPA_CantidadSistemas = Nothing
        Me.DSPA_CodigoEstadoValidacionSistemas = Nothing
        Me.DSPA_PrecioSistemas = Nothing
        Me.DSPA_MonedaSistemas = Nothing
        Me.DSPA_TipoReasignacion = Nothing
        Me.DSPA_UnidadSistemas = String.Empty
        Me.DSPA_TipoValidacion = Nothing
        Me.DSPA_TipoDistribucion = Nothing
        Me.codEstructuraCategoria = Nothing
        Me.codEstrcuturaSubCat = Nothing
        Me.codEstructuraClase = Nothing
        Me.codAsignacionCentroCosto = Nothing
        Me.listoEnviar = False

    End Sub
End Class
