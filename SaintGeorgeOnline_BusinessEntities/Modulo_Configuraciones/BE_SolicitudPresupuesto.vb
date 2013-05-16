Public Class BE_SolicitudPresupuesto
    Public CodigoSolicitudPresupuesto As Integer?
    Public CodigoTipoPresupuesto As Integer?
    Public CodigoEstadoPresupuesto As Integer?
    Public CodigoAsignacionSSSCentroCosto As Integer?
    Public FechaRegistro As DateTime?
    Public Estado As Boolean?

    Public Sub New()

        Me.CodigoSolicitudPresupuesto = Nothing
        Me.CodigoTipoPresupuesto = Nothing
        Me.CodigoEstadoPresupuesto = Nothing
        Me.CodigoAsignacionSSSCentroCosto = Nothing
        Me.FechaRegistro = Nothing
        Me.Estado = Nothing

    End Sub
End Class
