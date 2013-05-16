Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class bl_AsignacionClasesPresupuestoGsv


#Region "Metodos Transaccionales"
    Public Function insertarEstructuraCentro(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal lstbe_PS_AsignacionSSSCentroCostoSubCategoria As List(Of be_PS_AsignacionSSSCentroCostoSubCategoria))

        Try
            Dim oda_AsignacionClasesPresupuesto As New da_AsignacionClasesPresupuesto_gsv
            Return New da_AsignacionClasesPresupuesto_gsv().insertarEstructuraCentro(ASSSCC_CodigoAsignacionSSSCentroCosto, lstbe_PS_AsignacionSSSCentroCostoSubCategoria)
        Catch ex As Exception

        End Try
    End Function


#End Region
End Class
