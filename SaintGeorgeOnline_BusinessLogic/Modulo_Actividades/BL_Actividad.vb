Imports SaintGeorgeOnline_DataAccess

Public Class BL_Actividad

#Region "Atributos"

    Private obj_DA_actividad As DA_actividad

#End Region

#Region "Constructor"

    Public Sub New()
        obj_DA_actividad = New DA_actividad
    End Sub

#End Region

#Region "transaccional"

    Public Function F_insertarActividad(ByVal dcACtividad As Dictionary(Of String, Object)) As Dictionary(Of Object, Object)
        Try
            Return New DA_actividad().F_insertarActividad(dcACtividad)

        Catch ex As Exception

        End Try

    End Function


    Public Function F_eliminarACtividad(ByVal codActividad As Integer) As Dictionary(Of Object, Object)
        Try
            Return New DA_actividad().F_eliminarACtividad(codActividad)
        Catch ex As Exception

        End Try

    End Function
#End Region

#Region "Metodos No Transaccionales"

    Public Function FUN_GET_ActividadImp( _
        ByVal int_CodigoActividad As Integer, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        Return obj_DA_actividad.FUN_GET_ActividadImp(int_CodigoActividad, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

    End Function

#End Region

End Class


