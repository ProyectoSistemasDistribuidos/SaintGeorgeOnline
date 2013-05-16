


Public Class BE_PS_RegistroValidacionesPresupuestos
    Public RVP_CodigoRegistroValidacionesPresupuesto As Integer?
    Public ARVP_CodigoResponsableValidarPresupuesto As Integer?
    Public DSPS_CodigoDetSolicitudPresupuestoSubCategoria As Integer?
    Public RVP_Observacion As String
    Public RVP_MontoAprobado As Decimal?
    Public RVP_Estado As Boolean?
    Public estadoAprobado As Integer?

    Public nombreClase As String
    Public nombreCategoria As String
    Public nombreSubcategoria As String

    Public Sub New()
        Me.RVP_CodigoRegistroValidacionesPresupuesto = Nothing
        Me.ARVP_CodigoResponsableValidarPresupuesto = Nothing
        Me.DSPS_CodigoDetSolicitudPresupuestoSubCategoria = Nothing
        Me.RVP_Observacion = String.Empty
        Me.RVP_MontoAprobado = Nothing
        Me.RVP_Estado = Nothing
        Me.estadoAprobado = Nothing
    End Sub
End Class
