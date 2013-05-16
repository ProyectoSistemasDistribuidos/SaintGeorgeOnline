Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloMensajeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMensajeria

Namespace ModuloMensajeria

    Public Class bl_AdjuntosEnviados
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AdjuntosEnviados As da_AdjuntosEnviados

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
            obj_da_AdjuntosEnviados = New da_AdjuntosEnviados
        End Sub

#End Region

#Region "Metodos Transacciones"
        Public Function FUN_INS_AdjuntosEnviados(ByVal objAdjuntosEnviados As be_AdjuntosEnviados, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AdjuntosEnviados.FUN_INS_AdjuntosEnviados(objAdjuntosEnviados, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region

    End Class

End Namespace