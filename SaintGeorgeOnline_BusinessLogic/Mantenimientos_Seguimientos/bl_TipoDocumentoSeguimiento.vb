Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento

Namespace ModuloSeguimiento

    Public Class bl_TipoDocumentoSeguimiento

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_TipoDocumentoSeguimiento As da_TipoDocumentoSeguimiento

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
            obj_da_TipoDocumentoSeguimiento = New da_TipoDocumentoSeguimiento
        End Sub

#End Region

#Region "Metodos Transacciones"
        Public Function FUN_INS_TipoDocumentoSeguimiento(ByVal objTipoDocumentoSeguimiento As be_TipoDocumentoSeguimiento, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_TipoDocumentoSeguimiento.FUN_INS_TipoDocumentoSeguimiento(objTipoDocumentoSeguimiento, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_UPD_TipoDocumentoSeguimiento(ByVal objTipoDocumentoSeguimiento As be_TipoDocumentoSeguimiento, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_TipoDocumentoSeguimiento.FUN_UPD_TipoDocumentoSeguimiento(objTipoDocumentoSeguimiento, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_DEL_TipoDocumentoSeguimiento(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_TipoDocumentoSeguimiento.FUN_DEL_TipoDocumentoSeguimiento(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        
#End Region

#Region "Metodos No Transaccionales"
        Public Function FUN_LIS_TipoDocumentoSeguimiento(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_TipoDocumentoSeguimiento.FUN_LIS_TipoDocumentoSeguimiento(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_GET_TipoDocumentoSeguimiento(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_TipoDocumentoSeguimiento.FUN_GET_TipoDocumentoSeguimiento(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region

    End Class

End Namespace
