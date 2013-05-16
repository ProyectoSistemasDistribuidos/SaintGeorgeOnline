Namespace ModuloMatricula
    Public Class be_EstadosCiviles
#Region "Atributos"
        Private int_CodigoEstadoCivil As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
#End Region
#Region "Propiedades"
        Public Property CodigoEstadoCivil() As Integer
            Get
                Return int_CodigoEstadoCivil
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstadoCivil = value
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
        Sub New(ByVal CodigoEstadoCivil As Integer, ByVal Descripcion As String)
            int_CodigoEstadoCivil = CodigoEstadoCivil
            str_Descripcion = Descripcion
        End Sub
#End Region
    End Class
End Namespace

