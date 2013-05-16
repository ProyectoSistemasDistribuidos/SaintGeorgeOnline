Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class bl_AsignacionClasesPeriodo

#Region "Atributos"

        Private obj_da_AsignacionClasesPeriodo As da_AsignacionClasesPeriodo

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_AsignacionClasesPeriodo = New da_AsignacionClasesPeriodo
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionClasesPeriodo(ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionClasesPeriodo.FUN_LIS_AsignacionClasesPeriodo(int_CodigoPeriodo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

