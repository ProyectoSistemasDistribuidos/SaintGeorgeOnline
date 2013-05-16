Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class bl_PS_RegistroValidacionesDetallePresupuestos
    Public Function FinsercionPS_RegistroValidacionesDetallePresupuestos(ByVal lstBE_PS_RegistroValidacionesDetallePresupuestos As List(Of BE_PS_RegistroValidacionesDetallePresupuestos)) As List(Of Integer)
        Try
            Dim oda_PS_RegistroValidacionesDetallePresupuestos As New da_PS_RegistroValidacionesDetallePresupuestos
            Return New da_PS_RegistroValidacionesDetallePresupuestos().FinsercionPS_RegistroValidacionesDetallePresupuestos(lstBE_PS_RegistroValidacionesDetallePresupuestos)

        Catch ex As Exception

        End Try

    End Function

End Class
