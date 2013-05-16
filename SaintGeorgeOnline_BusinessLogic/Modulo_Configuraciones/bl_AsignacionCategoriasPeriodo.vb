Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class bl_AsignacionCategoriasPeriodo

#Region "Atributos"

        Private obj_da_AsignacionCategoriasPeriodo As da_AsignacionCategoriasPeriodo

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_AsignacionCategoriasPeriodo = New da_AsignacionCategoriasPeriodo
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionCategoriasPeriodo(ByVal int_CodigoAsignacionClase As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCategoriasPeriodo.FUN_LIS_AsignacionCategoriaPeriodoPorClase(int_CodigoAsignacionClase, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

