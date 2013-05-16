Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class BL_ValidarPresupuesto
#Region "no transaccional"
    Public Function fListarEstructuraPresupuestoValidar(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal codTrabajador As Integer) As DataSet
        Try
            Return New da_ValidarPresupuesto().fListarEstructuraPresupuestoValidar(ASSSCC_CodigoAsignacionSSSCentroCosto, codTrabajador)

        Catch ex As Exception

        End Try
    End Function

    Public Function fListarEstructuraPresupuestoValidarPorUsuario(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal codTrabajador As Integer) As DataSet
        Try
            Return New da_ValidarPresupuesto().fListarEstructuraPresupuestoValidarPorUsuario(ASSSCC_CodigoAsignacionSSSCentroCosto, codTrabajador)

        Catch ex As Exception

        End Try
    End Function


    Public Function Fun_Lis_Validadores(ByVal CodigoAsignacionSSSCentroCosto As Integer, _
                                        ByVal CodigoResponsableValidarPresupuesto As Integer) As DataSet
        Try
            Return New da_ValidarPresupuesto().Fun_Lis_Validadores(CodigoAsignacionSSSCentroCosto, CodigoResponsableValidarPresupuesto)
        Catch ex As Exception
        End Try
    End Function

#End Region

#Region "Transaccional"
    Public Function fInsertarValidarPresupuesto(ByVal lstBE_PS_RegistroValidacionesPresupuestos As List(Of BE_PS_RegistroValidacionesPresupuestos), ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer)
        Try
            Return New da_ValidarPresupuesto().fInsertarValidarPresupuesto(lstBE_PS_RegistroValidacionesPresupuestos, ASSSCC_CodigoAsignacionSSSCentroCosto)

        Catch ex As Exception

        End Try
    End Function


#End Region
End Class
