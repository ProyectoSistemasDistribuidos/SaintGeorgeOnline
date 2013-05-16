Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloAcademico
Namespace ModuloAcademico
    Public Class bl_TipoExamenCargo

#Region "Atributos"
        Private obj_da_TipoExamenCargo As da_TipoExamenCargo
#End Region
#Region "Constructor"
        Public Sub New()
            obj_da_TipoExamenCargo = New da_TipoExamenCargo
        End Sub
#End Region
#Region "Métodos Transaccionales"

#End Region
#Region "Método No Transaccionales"

        Public Function FUN_LIS_TipoExamenCargo(ByVal str_Descripcion As String, ByVal int_Estado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_TipoExamenCargo.FUN_LIS_TipoExamenCargo(str_Descripcion, int_Estado, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region
    End Class
End Namespace