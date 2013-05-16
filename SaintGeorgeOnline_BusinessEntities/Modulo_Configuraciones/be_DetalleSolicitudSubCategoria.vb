Public Class be_DetalleSolicitudSubCategoria

    Public DSPS_CodigoDetSolicitudPresupuestoSubCategoria As Integer
    Public SP_CodigoSolicitudPresupuesto As Integer?
    Public ASS_CodigoSSCentroCostoSubCategoria As Integer?
    Public DSPS_Estado As Boolean?
    Public ESA_CodigoEstadoValidaciones As Integer?
    Public DSPS_MontoValidacionFinal As Decimal?
    Public DSPS_FechaRegistroValidacion As DateTime?
    Public DSPS_ObservacionValidacionFinal As String
    Public DSPS_UsuarioReasignador As Integer?
    Public DSPS_TipoReasignacion As Integer?

    Public codEstructuraCategoria As Integer?
    Public codEstrcuturaSubCat As Integer?
    Public codEstructuraClase As Integer?
    Public codAsignacionCentroCosto As Integer?


    Public codArticulo As Integer?

    Public Sub New()
        
        Me.SP_CodigoSolicitudPresupuesto = Nothing
        Me.ASS_CodigoSSCentroCostoSubCategoria = Nothing
        Me.DSPS_Estado = Nothing
        Me.ESA_CodigoEstadoValidaciones = Nothing
        Me.DSPS_MontoValidacionFinal = Nothing
        Me.DSPS_FechaRegistroValidacion = Nothing
        Me.DSPS_ObservacionValidacionFinal = String.Empty
        Me.DSPS_UsuarioReasignador = Nothing
        Me.DSPS_TipoReasignacion = Nothing


        Me.codArticulo = Nothing
        Me.codEstructuraCategoria = Nothing
        Me.codEstrcuturaSubCat = Nothing
        Me.codEstructuraClase = Nothing
        Me.codAsignacionCentroCosto = Nothing
    End Sub

    '[DSPS_CodigoDetSolicitudPresupuestoSubCategoria] [int] NOT NULL,
    '[SP_CodigoSolicitudPresupuesto] [int] NULL,
    '[ASS_CodigoSSCentroCostoSubCategoria] [int] NULL,
    '[DSPS_Estado] [bit] NULL,
    '[ESA_CodigoEstadoValidaciones] [int] NULL,
    '[DSPS_MontoValidacionFinal] [decimal](10, 2) NULL,
    '[DSPS_FechaRegistroValidacion] [datetime] NULL,
    '[DSPS_ObservacionValidacionFinal] [varchar](8000) NULL,
    '[DSPS_UsuarioReasignador] [int] NULL,
    '[DSPS_TipoReasignacion] [int] NULL,
End Class
