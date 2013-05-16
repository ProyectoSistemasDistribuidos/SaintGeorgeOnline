Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class bl_AsignacionEstructuraSubSubSubCentroCostoClases

#Region "Atributos"

        Private obj_da_AsignacionEstructuraSubSubSubCentroCostoClases As da_AsignacionEstructuraSubSubSubCentroCostoClases

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_AsignacionEstructuraSubSubSubCentroCostoClases = New da_AsignacionEstructuraSubSubSubCentroCostoClases
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionEstructuraSubSubSubCentroCostoClases(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionEstructuraSubSubSubCentroCostoClases.FUN_LIS_AsignacionEstructuraSubSubSubCentroCostoClases(int_CodigoAsignacionSSSCentroCosto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace