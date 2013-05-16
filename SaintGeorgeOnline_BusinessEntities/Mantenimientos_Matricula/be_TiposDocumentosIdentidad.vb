Namespace ModuloMatricula
    Public Class be_TiposDocumentosIdentidad
#Region "Atributos"
        Private int_CodigoTipoDocIdentidad As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
#End Region
#Region "Propiedades"
        Public Property CodigoTipoDocIdentidad() As Integer
            Get
                Return int_CodigoTipoDocIdentidad
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoDocIdentidad = value
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
        Sub New(ByVal CodigoTipoDocIdentidad As Integer, ByVal Descripcion As String, ByVal Estado As Integer)
            int_CodigoTipoDocIdentidad = CodigoTipoDocIdentidad
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub
#End Region
    End Class
End Namespace

