Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class bl_AsignacionResponsablesPresupuestos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionResponsablesPresupuestos As da_AsignacionResponsablesPresupuestos

#End Region

#Region "Propiedades"

        Public ReadOnly Property Mensaje() As String
            Get
                Return str_Mensaje
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_AsignacionResponsablesPresupuestos = New da_AsignacionResponsablesPresupuestos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_UPD_AsignacionResponsablesPresupuestos(ByVal objAsignacionResponsablesPresupuestos As be_AsignacionResponsablesPresupuestos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionResponsablesPresupuestos.FUN_UPD_AsignacionResponsablesPresupuestos(objAsignacionResponsablesPresupuestos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionResponsablesPresupuestos(ByVal int_Sede As Integer, ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoCentrosCostos As Integer, ByVal int_CodigoSubCentrosCostos As Integer, ByVal int_CodigoSubSubCentroCostos As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AsignacionResponsablesPresupuestos.FUN_LIS_AsignacionResponsablesPresupuestos(int_Sede, int_CodigoPeriodo, int_CodigoCentrosCostos, int_CodigoSubCentrosCostos, int_CodigoSubSubCentroCostos, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_Trabajadores(ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombre As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AsignacionResponsablesPresupuestos.FUN_LIS_Trabajadores(str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace

