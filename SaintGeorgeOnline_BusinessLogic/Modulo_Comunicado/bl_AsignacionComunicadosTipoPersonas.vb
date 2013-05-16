Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloComunicado
Imports SaintGeorgeOnline_DataAccess.ModuloComunicado

Namespace ModuloComunicado

    Public Class bl_AsignacionComunicadosTipoPersonas

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionComunicadosTipoPersonas As da_AsignacionComunicadosTipoPersonas

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
            obj_da_AsignacionComunicadosTipoPersonas = New da_AsignacionComunicadosTipoPersonas
        End Sub

#End Region

#Region "Metodos Transacciones"


#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_INS_AsignacionComunicadosTipoPersonas(ByVal objAsignacionComunicadosTipoPersonas As be_AsignacionComunicadosTipoPersonas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionComunicadosTipoPersonas.FUN_INS_AsignacionComunicadosTipoPersonas(objAsignacionComunicadosTipoPersonas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_AsignacionComunicadosTipoPersonas(ByVal objAsignacionComunicadosTipoPersonas As be_AsignacionComunicadosTipoPersonas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionComunicadosTipoPersonas.FUN_UPD_AsignacionComunicadosTipoPersonas(objAsignacionComunicadosTipoPersonas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_AsignacionComunicadosTipoPersonas(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionComunicadosTipoPersonas.FUN_DEL_AsignacionComunicadosTipoPersonas(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
