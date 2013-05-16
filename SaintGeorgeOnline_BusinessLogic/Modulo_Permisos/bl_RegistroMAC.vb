Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos

Namespace ModuloPermisos

    Public Class bl_RegistroMAC

#Region "Atributos"
        Private obj_da_RegistoMAC As da_RegistoMAC
#End Region

#Region "Constructor"
        Public Sub New()
            obj_da_RegistoMAC = New da_RegistoMAC
        End Sub
#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_RegistroMAC( _
            ByVal obe_RegistroMAC As be_RegistroMAC, _
            ByVal lstEliminados As List(Of Integer), _
            ByVal dt_Detalle As DataTable, _
            ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_RegistoMAC.FUN_INS_RegistroMAC(obe_RegistroMAC, lstEliminados, dt_Detalle, str_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_RegistroMAC(ByVal int_CodigoPersona As Integer, ByVal int_CodigoTipoPersona As Integer, _
                                            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_RegistoMAC.FUN_LIS_RegistroMAC(int_CodigoPersona, int_CodigoTipoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region
    End Class

End Namespace