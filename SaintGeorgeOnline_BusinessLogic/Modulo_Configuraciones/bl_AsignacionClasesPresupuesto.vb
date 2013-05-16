Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class bl_AsignacionClasesPresupuesto

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionClasesPresupuesto As da_AsignacionClasesPresupuesto

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
            obj_da_AsignacionClasesPresupuesto = New da_AsignacionClasesPresupuesto
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionClasesPresupuesto(ByVal objAsignacionClasesPresupuesto As be_AsignacionClasesPresupuesto, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionClasesPresupuesto.FUN_INS_AsignacionClasesPresupuesto(objAsignacionClasesPresupuesto, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        'Public Function FUN_UPD_AsignacionClasesPresupuesto(ByVal objAsignacionClasesPresupuesto As be_AsignacionClasesPresupuesto, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
        '    Return obj_da_AsignacionClasesPresupuesto.FUN_UPD_AsignacionClasesPresupuesto(objAsignacionClasesPresupuesto, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        'End Function
        Public Function FUN_DEL_AsignacionClasesPresupuesto(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionClasesPresupuesto.FUN_DEL_AsignacionClasesPresupuesto(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionClasesPresupuesto(ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoSubSubSubCentroCostos As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionClasesPresupuesto.FUN_LIS_AsignacionClasesPresupuesto(int_CodigoPeriodo, int_CodigoSubSubSubCentroCostos, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

