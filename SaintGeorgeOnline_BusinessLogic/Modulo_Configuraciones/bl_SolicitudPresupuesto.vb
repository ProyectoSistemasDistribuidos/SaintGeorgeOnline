Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class bl_SolicitudPresupuesto
#Region "transaccional"


    Public Function FUN_INS_SolicitudPresupuesto(ByVal oBE_SolicitudPresupuesto As BE_SolicitudPresupuesto, ByVal Listobe_DetalleSolicitudSubCategoria As List(Of be_DetalleSolicitudSubCategoria), ByVal oBE_PS_DetalleSolicitudArticulos As List(Of BE_PS_DetalleSolicitudArticulos), ByVal TJ_CodigoTrabajador As Integer) As List(Of Integer)
        Try
            Return New da_SolicitudPresupuesto().FUN_INS_SolicitudPresupuesto(oBE_SolicitudPresupuesto, Listobe_DetalleSolicitudSubCategoria, oBE_PS_DetalleSolicitudArticulos, TJ_CodigoTrabajador)
        Catch ex As Exception
        Finally

        End Try
    End Function

#End Region

#Region "no transaccional"

    Public Function listarSolicitudPresupuesto(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal ACP_CodigoEstructuraClase As Integer, ByVal DSPS_CodigoDetSolicitudPresupuestoSubCategoria As Integer) As DataTable


        Try
            Return New da_SolicitudPresupuesto().listarSolicitudPresupuesto(ASSSCC_CodigoAsignacionSSSCentroCosto, ACP_CodigoEstructuraClase, DSPS_CodigoDetSolicitudPresupuestoSubCategoria)
        Catch ex As Exception
        Finally

        End Try

    End Function
    Function listarSolicitudPresupuestoValidarArticulo(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal ACP_CodigoEstructuraClase As Integer, ByVal DSPS_CodigoDetSolicitudPresupuestoSubCategoria As Integer, ByVal RVP_CodigoRegistroValidacionesPresupuesto As Integer) As DataTable

        Try
            Return New da_SolicitudPresupuesto().listarSolicitudPresupuestoValidarArticulo(ASSSCC_CodigoAsignacionSSSCentroCosto, ACP_CodigoEstructuraClase, DSPS_CodigoDetSolicitudPresupuestoSubCategoria, RVP_CodigoRegistroValidacionesPresupuesto)
        Catch ex As Exception

        End Try
    End Function

    Function fEliminarFilas(ByVal DSPA_CodigoDetalleSolicitudPresupuestoArticulo As Integer, ByVal codTrabajador As Integer) As Object
        Try
            Return New da_SolicitudPresupuesto().fEliminarFilas(DSPA_CodigoDetalleSolicitudPresupuestoArticulo, codTrabajador)
        Catch ex As Exception

        End Try
    End Function

#End Region
End Class
