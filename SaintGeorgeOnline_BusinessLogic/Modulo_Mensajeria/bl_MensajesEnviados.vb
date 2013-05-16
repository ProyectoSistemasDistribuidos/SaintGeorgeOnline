Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloMensajeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMensajeria

Namespace ModuloMensajeria

    Public Class bl_MensajesEnviados

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_MensajesEnviados As da_MensajesEnviados

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
            obj_da_MensajesEnviados = New da_MensajesEnviados
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_MensajesEnviados(ByVal objMensajesEnviados As be_MensajesEnviados, _
                                                        ByVal str_ListaCodigosyTipoRecibe As String, _
                                                        ByRef str_Mensaje As String, _
                   ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_MensajesEnviados.FUN_INS_MensajesEnviados(objMensajesEnviados, str_ListaCodigosyTipoRecibe, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_MensajesEnviadosConAdjuntos(ByVal objMensajesEnviados As be_MensajesEnviados, _
                                                            ByVal str_ListaCodigosyTipoRecibe As String, _
                                                            ByVal dt_ListaAdjuntos As DataTable, _
                                                            ByRef str_Mensaje As String, _
                   ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_MensajesEnviados.FUN_INS_MensajesEnviadosConAdjuntos(objMensajesEnviados, str_ListaCodigosyTipoRecibe, dt_ListaAdjuntos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transacciones"

        Public Function FUN_GET_MensajeEnviado(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MensajesEnviados.FUN_GET_MensajeEnviado(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace