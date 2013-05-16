Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class bl_Trabajador

#Region "Atributos"

        Private obj_da_Trabajador As da_Trabajador

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_Trabajador = New da_Trabajador
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Trabajador(ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombre As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Trabajador.FUN_LIS_Trabajador(str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

