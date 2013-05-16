Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_CuentasBancarias

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_CuentasBancarias As da_CuentasBancarias

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
            obj_da_CuentasBancarias = New da_CuentasBancarias
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_CuentasBancarias(ByVal objCuentasBancarias As be_CuentasBancarias, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CuentasBancarias.FUN_INS_CuentasBancarias(objCuentasBancarias, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_CuentasBancarias(ByVal objCuentasBancarias As be_CuentasBancarias, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CuentasBancarias.FUN_UPD_CuentasBancarias(objCuentasBancarias, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_CuentasBancarias(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CuentasBancarias.FUN_DEL_CuentasBancarias(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_CuentasBancarias(ByVal int_CodigoBanco As Integer, ByVal int_CodigoMoneda As Integer, ByVal str_NumeroCuenta As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_CuentasBancarias.FUN_LIS_CuentasBancarias(int_CodigoBanco, int_CodigoMoneda, str_NumeroCuenta, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_CuentasBancarias(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_CuentasBancarias.FUN_GET_CuentasBancarias(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


#End Region

    End Class

End Namespace
