Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones
Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess

Public Class BL_ActualizarDetaalleSolicitudArticuloPrecioCantidad
    Public Function fActualizarPrecioCantidadSolicitudArticulo(ByVal lstSolicitudArticulo As List(Of BE_ActualizarDetaalleSolicitudArticuloPrecioCantidad)) As List(Of Integer)

        Try
            Return New DA_ActualizarDetaalleSolicitudArticuloPrecioCantidad().fActualizarPrecioCantidadSolicitudArticulo(lstSolicitudArticulo)
        Catch ex As Exception

        End Try
    End Function

End Class
