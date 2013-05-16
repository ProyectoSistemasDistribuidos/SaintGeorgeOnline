Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities


Public Class BL_AF_MovimientoArticulos_Cabecera
#Region "Transaccional"
    Public Function F_InsertarCabezeraMovimiento(ByVal oBE_AF_MovimientoArticulos_Cabecera As BE_AF_MovimientoArticulos_Cabecera, ByVal LstBE_AF_MovimientoArticulos_Detalle As List(Of BE_AF_MovimientoArticulos_Detalle)) As Integer
        Try
            Return New DA_AF_MovimientoArticulos_Cabecera().F_InsertarCabezeraMovimiento(oBE_AF_MovimientoArticulos_Cabecera, LstBE_AF_MovimientoArticulos_Detalle)
        Catch ex As Exception

        End Try
    End Function

#End Region
End Class
