Namespace ModuloSeguimiento

    Public Class be_CriteriosEvaluacion

        'Updated 2

#Region "Atributos"

        Private int_CodigoCriterio As Integer
        Private str_Descripcion As String
        Private str_DescripcionEspaniol As String
        Private int_Tipo As Integer
        Private int_Orden As Integer
        Private int_Estado As Integer
        Private str_Abreviatura As String

#End Region

#Region "Propiedades"

        Public Property CodigoCriterio() As Integer
            Get
                Return int_CodigoCriterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoCriterio = value
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

        Public Property DescripcionEspaniol() As String
            Get
                Return str_DescripcionEspaniol
            End Get
            Set(ByVal value As String)
                str_DescripcionEspaniol = value
            End Set
        End Property

        Public Property Tipo() As Integer
            Get
                Return int_Tipo
            End Get
            Set(ByVal value As Integer)
                int_Tipo = value
            End Set
        End Property

        Public Property Orden() As Integer
            Get
                Return int_Orden
            End Get
            Set(ByVal value As Integer)
                int_Orden = value
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

        Public Property Abreviatura() As String
            Get
                Return str_Abreviatura
            End Get
            Set(ByVal value As String)
                str_Abreviatura = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoCriterio As Integer, _
                ByVal Descripcion As String, _
                ByVal str_DescripcionEspaniol As String, _
                ByVal Tipo As Integer, _
                ByVal LeyendaEspanol As Integer, _
                ByVal Estado As Integer, _
                ByVal Abreviatura As String)

            int_CodigoCriterio = CodigoCriterio
            str_Descripcion = Descripcion
            str_DescripcionEspaniol = DescripcionEspaniol
            int_Tipo = Tipo
            int_Orden = Orden
            int_Estado = Estado
            str_Abreviatura = Abreviatura

        End Sub

#End Region

    End Class

End Namespace