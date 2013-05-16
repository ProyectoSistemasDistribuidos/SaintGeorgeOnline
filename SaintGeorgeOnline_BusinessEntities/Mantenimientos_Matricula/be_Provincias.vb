Public Class be_Provincias
#Region "Atributos"
    Private int_CodigoProvincia As Integer
    Private int_CodigoDepartamento As Integer
    Private str_Descripcion As String
#End Region
#Region "Propiedades"
    Public Property CodigoProvincia() As Integer
        Get
            Return int_CodigoProvincia
        End Get
        Set(ByVal value As Integer)
            int_CodigoProvincia = value
        End Set
    End Property
    Public Property CodigoDepartamento() As Integer
        Get
            Return int_CodigoDepartamento
        End Get
        Set(ByVal value As Integer)
            int_CodigoDepartamento = value
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
    Sub New(ByVal CodigoProvincia As Integer, ByVal int_CodigoDepartamento As Integer, ByVal Descripcion As String)
        int_CodigoProvincia = CodigoProvincia
        int_CodigoDepartamento = CodigoDepartamento
        str_Descripcion = Descripcion
    End Sub
#End Region
End Class
