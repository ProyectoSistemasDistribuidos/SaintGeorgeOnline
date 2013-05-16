Public Class BE_PS_RegistroValidacionesDetallePresupuestos
    Public RVDP_CodigoRegistroValidacionesDetalle As Integer?
    Public RVP_CodigoRegistroValidacionesPresupuesto As Integer?
    Public DSPA_CodigoDetalleSolicitudPresupuestoArticulo As Integer?
    Public RVDP_Observacion As String
    Public RVDP_Estado As Boolean?
    Public Sub New()
        Me.RVDP_CodigoRegistroValidacionesDetalle = Nothing
        Me.RVP_CodigoRegistroValidacionesPresupuesto = Nothing
        Me.DSPA_CodigoDetalleSolicitudPresupuestoArticulo = Nothing
        Me.RVDP_Observacion = String.Empty
        Me.RVDP_Estado = Nothing
    End Sub
End Class
