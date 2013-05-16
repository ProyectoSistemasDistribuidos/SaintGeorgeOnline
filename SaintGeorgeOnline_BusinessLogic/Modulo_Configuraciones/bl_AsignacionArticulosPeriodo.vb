Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class bl_AsignacionArticulosPeriodo

#Region "Atributos"

        Private obj_da_AsignacionArticulosPeriodo As da_AsignacionArticulosPeriodo

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_AsignacionArticulosPeriodo = New da_AsignacionArticulosPeriodo
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionArticulosPeriodo(ByVal objAsignacionArticulosPeriodo As be_AsignacionArticulosPeriodo, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionArticulosPeriodo.FUN_INS_AsignacionArticulosPeriodo(objAsignacionArticulosPeriodo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_AsignacionArticulosPeriodo(ByVal objAsignacionArticulosPeriodo As be_AsignacionArticulosPeriodo, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionArticulosPeriodo.FUN_UPD_AsignacionArticulosPeriodo(objAsignacionArticulosPeriodo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_AsignacionArticulosPeriodo(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionArticulosPeriodo.FUN_DEL_AsignacionArticulosPeriodo(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionArticulosPeriodo(ByVal int_CodigoAsignacionSubCategoria As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionArticulosPeriodo.FUN_LIS_AsignacionArticulosPeriodoPorSubCategoria(int_CodigoAsignacionSubCategoria, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace