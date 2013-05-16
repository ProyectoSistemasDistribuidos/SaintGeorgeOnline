Namespace ModuloColegio

    Public Class be_Aulas

#Region "Atributos"

        Private int_CodigoAula As Integer
        Private int_CodigoGrado As Integer
        Private int_CodigoAulaMinisterio As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAula() As Integer
            Get
                Return int_CodigoAula
            End Get
            Set(ByVal value As Integer)
                int_CodigoAula = value
            End Set
        End Property

        Public Property CodigoGrado() As Integer
            Get
                Return int_CodigoGrado
            End Get
            Set(ByVal value As Integer)
                int_CodigoGrado = value
            End Set
        End Property

        Public Property CodigoAulaMinisterio() As Integer
            Get
                Return int_CodigoAulaMinisterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoAulaMinisterio = value
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
        Sub New(ByVal CodigoAula As Integer, _
                ByVal CodigoGrado As Integer, _
                ByVal CodigoAulaMinisterio As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)

            int_CodigoAula = CodigoAula
            int_CodigoGrado = CodigoGrado
            int_CodigoAulaMinisterio = CodigoAulaMinisterio
            str_Descripcion = str_Descripcion
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

