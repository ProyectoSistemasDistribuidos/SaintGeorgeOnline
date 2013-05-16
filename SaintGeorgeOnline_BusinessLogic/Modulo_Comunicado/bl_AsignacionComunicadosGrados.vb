Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloComunicado
Imports SaintGeorgeOnline_DataAccess.ModuloComunicado

Namespace ModuloComunicado

    Public Class bl_AsignacionComunicadosGrados

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionComunicadosGrados As da_AsignacionComunicadosGrados

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
            obj_da_AsignacionComunicadosGrados = New da_AsignacionComunicadosGrados
        End Sub

#End Region

#Region "Metodos Transacciones"


#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_INS_AsignacionComunicadosGrados(ByVal objAsignacionComunicadosGrados As be_AsignacionComunicadosGrados, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionComunicadosGrados.FUN_INS_AsignacionComunicadosGrados(objAsignacionComunicadosGrados, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_AsignacionComunicadosGrados(ByVal objAsignacionComunicadosGrados As be_AsignacionComunicadosGrados, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionComunicadosGrados.FUN_UPD_AsignacionComunicadosGrados(objAsignacionComunicadosGrados, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_AsignacionComunicadosGrados(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionComunicadosGrados.FUN_DEL_AsignacionComunicadosGrados(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
