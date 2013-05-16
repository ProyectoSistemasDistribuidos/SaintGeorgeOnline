Public Class be_Distritos
#Region "Atributos"
    Private int_CodigoDistrito As Integer
    Private int_CodigoProvincia As Integer
    Private str_Descripcion As String
#End Region
#Region "Propiedades"
    Public Property CodigoDistrito() As Integer
        Get
            Return int_CodigoDistrito
        End Get
        Set(ByVal value As Integer)
            int_CodigoDistrito = value
        End Set
    End Property
    Public Property CodigoProvincia() As Integer
        Get
            Return int_CodigoProvincia
        End Get
        Set(ByVal value As Integer)
            int_CodigoProvincia = value
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
#End Region
#Region "Constructor"
    Sub New()
        MyBase.New()
    End Sub
    Sub New(ByVal CodigoDistrito As Integer, ByVal CodigoProvincia As Integer, ByVal Descripcion As String)
        int_CodigoDistrito = CodigoDistrito
        int_CodigoProvincia = CodigoProvincia
        str_Descripcion = Descripcion
    End Sub
#End Region
End Class
