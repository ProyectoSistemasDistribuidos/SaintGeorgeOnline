Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class BL_PS_AsignacionArticulosPeriodos


#Region "Transaccional"
    Public Function InsertarPS_AsignacionArticulosPeriodos(ByVal lstBE_PS_AsignacionArticulosPeriodos As List(Of BE_PS_AsignacionArticulosPeriodos)) As List(Of Integer)
        Try

            Return New DA_PS_AsignacionArticulosPeriodos().InsertarPS_AsignacionArticulosPeriodos(lstBE_PS_AsignacionArticulosPeriodos)

        Catch ex As Exception

        End Try
    End Function
#End Region


#Region "no transaccional"
    Public Function listarArticulosAsignacionSubcategoria(ByVal ASP_CodigoEstructuraSubCategoria As Integer) As DataTable
        Try
            Return New DA_PS_AsignacionArticulosPeriodos().listarArticulosAsignacionSubcategoria(ASP_CodigoEstructuraSubCategoria)
        Catch ex As Exception

        End Try
    End Function

#End Region
End Class
