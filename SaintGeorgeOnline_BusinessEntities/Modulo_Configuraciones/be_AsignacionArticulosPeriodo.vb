Namespace ModuloConfiguraciones

    Public Class be_AsignacionArticulosPeriodo

#Region "Atributos"
        Private int_CodigoEstructuraArticulo As Integer
        Private int_CodigoEstructuraSubCategoria As Integer
        Private int_CodigoItem As Integer
        Private de_PrecioArticulo As Decimal
        Private int_CodigoMoneda As Integer
        Private str_Unidad As String
#End Region

#Region "Propiedades"

        Public Property CodigoEstructuraArticulo() As Integer
            Get
                Return int_CodigoEstructuraArticulo
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstructuraArticulo = value
            End Set
        End Property

        Public Property CodigoEstructuraSubCategoria() As Integer
            Get
                Return int_CodigoEstructuraSubCategoria
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstructuraSubCategoria = value
            End Set
        End Property

        Public Property CodigoItem() As Integer
            Get
                Return int_CodigoItem
            End Get
            Set(ByVal value As Integer)
                int_CodigoItem = value
            End Set
        End Property

        Public Property PrecioArticulo() As Decimal
            Get
                Return de_PrecioArticulo
            End Get
            Set(ByVal value As Decimal)
                de_PrecioArticulo = value
            End Set
        End Property

        Public Property CodigoMoneda() As Integer
            Get
                Return int_CodigoMoneda
            End Get
            Set(ByVal value As Integer)
                int_CodigoMoneda = value
            End Set
        End Property

        Public Property Unidad() As String
            Get
                Return str_Unidad
            End Get
            Set(ByVal value As String)
                str_Unidad = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoEstructuraArticulo As Integer, _
                ByVal CodigoEstructuraSubCategoria As Integer, _
                ByVal CodigoItem As Integer, _
                ByVal PrecioArticulo As Decimal, _
                ByVal CodigoMoneda As Integer, _
                ByVal Unidad As String)

            int_CodigoEstructuraArticulo = CodigoEstructuraArticulo
            int_CodigoEstructuraSubCategoria = CodigoEstructuraSubCategoria
            int_CodigoItem = CodigoItem
            de_PrecioArticulo = PrecioArticulo
            int_CodigoMoneda = CodigoMoneda
            str_Unidad = Unidad

        End Sub

#End Region

    End Class

End Namespace

