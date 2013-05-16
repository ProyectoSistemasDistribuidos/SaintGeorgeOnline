Namespace ModuloCursos

    Public Class be_CalificativoDescriptores

#Region "Atributos"

        Private int_CodigoCalificativoDescriptores As Integer
        Private str_Descripcion As String
        Private int_Peso As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoCalificativoDescriptores() As Integer
            Get
                Return int_CodigoCalificativoDescriptores
            End Get
            Set(ByVal value As Integer)
                int_CodigoCalificativoDescriptores = value
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

        Public Property Peso() As Integer
            Get
                Return int_Peso
            End Get
            Set(ByVal value As Integer)
                int_Peso = value
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
        Sub New(ByVal CodigoCalificativoDescriptores As Integer, _
                ByVal Descripcion As String, _
                ByVal Peso As Integer, _
                ByVal Estado As Integer)

            int_CodigoCalificativoDescriptores = CodigoCalificativoDescriptores
            str_Descripcion = Descripcion
            int_Peso = Peso
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace

