Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio

Namespace ModuloColegio

    Public Class bl_AulasMinisterio


#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AulasMinisterio As da_AulasMinisterio

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
            obj_da_AulasMinisterio = New da_AulasMinisterio
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AulasMinisterio(ByVal objAulasMinisterio As be_AulasMinisterio, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AulasMinisterio.FUN_INS_AulasMinisterio(objAulasMinisterio, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_AulasMinisterio(ByVal objAulasMinisterio As be_AulasMinisterio, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AulasMinisterio.FUN_UPD_AulasMinisterio(objAulasMinisterio, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_AulasMinisterio(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AulasMinisterio.FUN_DEL_AulasMinisterio(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AulasMinisterio(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AulasMinisterio.FUN_LIS_AulasMinisterio(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_AulasMinisterio(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AulasMinisterio.FUN_GET_AulasMinisterio(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        'Prueba
        Public Function FUN_LIS_AulasMinisterioXgradoMinisterio(ByVal Int_CodigoGradoMinisterio As Integer, ByVal Int_CodigoUsuario As Integer, ByVal Int_CodigoTipoUsuario As Integer, ByVal Int_CodigoModulo As Integer, ByVal Int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AulasMinisterio.FUN_LIS_AulasMinisterioXgradoMinisterio(Int_CodigoGradoMinisterio, Int_CodigoTipoUsuario, Int_CodigoTipoUsuario, Int_CodigoModulo, Int_CodigoOpcion)
        End Function
#End Region

    End Class

End Namespace
