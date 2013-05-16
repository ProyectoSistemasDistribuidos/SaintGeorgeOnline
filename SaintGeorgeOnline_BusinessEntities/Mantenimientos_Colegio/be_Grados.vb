Namespace ModuloColegio

    Public Class be_Grados

#Region "Atributos"

        Private int_CodigoGrado As Integer
        Private int_CodigoSubNivel As Integer
        Private int_CodigoGradoMinisterio As Integer
        Private str_Descripcion As String
        Private str_DescripcionEspaniol As String
        Private str_Abrev As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoGrado() As Integer
            Get
                Return int_CodigoGrado
            End Get
            Set(ByVal value As Integer)
                int_CodigoGrado = value
            End Set
        End Property

        Public Property CodigoSubNivel() As Integer
            Get
                Return int_CodigoSubNivel
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubNivel = value
            End Set
        End Property

        Public Property CodigoGradoMinisterio() As Integer
            Get
                Return int_CodigoGradoMinisterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoGradoMinisterio = value
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

        Public Property Abrev() As String
            Get
                Return str_Abrev
            End Get
            Set(ByVal value As String)
                str_Abrev = value
            End Set
        End Property

        Public Property DescripcionEspaniol() As String
            Get
                Return str_DescripcionEspaniol
            End Get
            Set(ByVal value As String)
                str_DescripcionEspaniol = value
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
        Sub New(ByVal CodigoGrado As Integer, _
                ByVal CodigoSubNivel As Integer, _
                ByVal CodigoGradoMinisterio As Integer, _
                ByVal Descripcion As String, _
                ByVal DescripcionEspaniol As String, _
                ByVal Abrev As String, _
                ByVal Estado As Integer)

            int_CodigoGrado = CodigoGrado
            int_CodigoSubNivel = CodigoSubNivel
            int_CodigoGradoMinisterio = CodigoGradoMinisterio
            str_Descripcion = str_Descripcion
            str_DescripcionEspaniol = DescripcionEspaniol
            str_Abrev = Abrev
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

