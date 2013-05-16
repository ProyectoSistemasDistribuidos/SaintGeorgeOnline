Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess

Public Class BL_PS_SolicitudCompraPresupuesto
    Public Function F_insertarSolicitudCompraPresupuesto(ByVal oBE_PS_SolicitudCompraPresupuesto As BE_PS_SolicitudCompraPresupuesto, ByVal lstBE_PS_DetalleSolicitudCompraPresupuesto As List(Of BE_PS_DetalleSolicitudCompraPresupuesto)) As Integer

        Try
            Return New DA_PS_SolicitudCompraPresupuesto().F_insertarSolicitudCompraPresupuesto(oBE_PS_SolicitudCompraPresupuesto, lstBE_PS_DetalleSolicitudCompraPresupuesto)
        Catch ex As Exception

        End Try
    End Function

End Class