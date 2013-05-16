Namespace ModuloMatricula
    Public Class be_TiposSeguro
#Region "Atributos"
        Private int_CodigoTipoSeguro As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
#End Region
#Region "Propiedades"
        Public Property CodigoTipoSeguro() As Integer
            Get
                Return int_CodigoTipoSeguro
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoSeguro = value
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
        Sub New(ByVal CodigoTipoSeguro As Integer, ByVal Descripcion As String, ByVal Estado As Integer)
            int_CodigoTipoSeguro = CodigoTipoSeguro
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub
#End Region
    End Class
End Namespace

