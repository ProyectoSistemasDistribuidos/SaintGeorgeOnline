Namespace ModuloMatricula

    Public Class be_Paises

#Region "Atributos"
        Private int_CodigoPais As Integer
        Private int_CodigoPaisMinisterio As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
#End Region
#Region "Propiedades"
        Public Property CodigoPais() As Integer
            Get
                Return int_CodigoPais
            End Get
            Set(ByVal value As Integer)
                int_CodigoPais = value
            End Set
        End Property
        Public Property CodigoPaisMinisterio() As Integer
            Get
                Return int_CodigoPaisMinisterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoPaisMinisterio = value
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
        Sub New(ByVal CodigoPais As Integer, ByVal CodigoPaisMinisterio As Integer, ByVal Descripcion As String, ByVal Estado As Integer)
            int_CodigoPais = CodigoPais
            int_CodigoPaisMinisterio = CodigoPaisMinisterio
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub
#End Region

    End Class

End Namespace
