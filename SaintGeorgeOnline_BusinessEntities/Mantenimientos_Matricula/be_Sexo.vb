Public Class be_Sexo
#Region "Atributos"
    Private int_CodigoSexo As Integer
    Private str_Descripcion As String
#End Region
#Region "Propiedades"
    Public Property CodigoSexo() As Integer
        Get
            Return int_CodigoSexo
        End Get
        Set(ByVal value As Integer)
            int_CodigoSexo = value
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
    Sub New(ByVal CodigoSexo As Integer, ByVal Descripcion As String)
        int_CodigoSexo = CodigoSexo
        str_Descripcion = Descripcion
    End Sub
#End Region
End Class
