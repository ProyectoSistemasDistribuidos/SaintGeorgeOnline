Imports System.Configuration
Imports System
Imports System.Web
Namespace ModuloLogueo
    Public Class be_TiposPersonas
#Region "Atributos"
        Private int_CodigoTipoPersona As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
#End Region
#Region "Propiedades"
        Public Property CodigoTipoPersona() As Integer
            Get
                Return int_CodigoTipoPersona
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoPersona = value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Return str_Descripcion
            End Get
            Set(ByVal value As String)
                str_Descripcion = value
            End Set
        End Property
        Public Property Estado() As Integer
            Get
                Return int_Estado
            End Get
            Set(ByVal value As Integer)
                int_Estado = value
            End Set
        End Property
#End Region
#Region "Constructor"
        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoTipoPersona As Integer, ByVal Descripcion As String, ByVal Estado As Integer)
            CodigoTipoPersona = int_CodigoTipoPersona
            Descripcion = str_Descripcion
            Estado = int_Estado
        End Sub
#End Region

    End Class
End Namespace

