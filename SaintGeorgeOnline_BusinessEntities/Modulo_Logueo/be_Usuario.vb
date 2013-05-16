Imports System.Configuration
Imports System
Imports System.Web

Namespace ModuloLogueo

    Public Class be_Usuario

#Region "Atributos"

        Private int_CodigoUsuario As Integer
        Private int_TipoUsuario As Integer
        Private str_IpUsuario As String
        Private str_NombreUsuario As String
        Private str_HostUsuario As String
        Private int_CodigoComunicado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoUsuario() As Integer
            Get
                Return int_CodigoUsuario
            End Get
            Set(ByVal value As Integer)
                int_CodigoUsuario = value
            End Set
        End Property

        Public Property TipoUsuario() As Integer
            Get
                Return int_TipoUsuario
            End Get
            Set(ByVal value As Integer)
                int_TipoUsuario = value
            End Set
        End Property

        Public Property IpUsuario() As String
            Get
                Return str_IpUsuario
            End Get
            Set(ByVal value As String)
                str_IpUsuario = value
            End Set
        End Property

        Public Property NombreUsuario() As String
            Get
                Return str_NombreUsuario
            End Get
            Set(ByVal value As String)
                str_NombreUsuario = value
            End Set
        End Property

        Public Property HostUsuario() As String
            Get
                Return str_HostUsuario
            End Get
            Set(ByVal value As String)
                str_HostUsuario = value
            End Set
        End Property

        Public Property CodigoComunicado() As Integer
            Get
                Return int_CodigoComunicado
            End Get
            Set(ByVal value As Integer)
                int_CodigoComunicado = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

#End Region

    End Class

End Namespace

