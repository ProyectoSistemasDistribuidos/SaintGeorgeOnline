Namespace ModuloBancoLibros

    Public Class be_CopiaLibros

#Region "Atributos"

        Private int_CodigoCopiaLibro As Integer
        Private int_CodigoLibro As Integer
        Private int_CodigoBarra As String
        Private int_Disponible As Integer
        Private str_CodigoEjemplar As String
        Private int_Estado As Integer
        Private str_NumeroPago As String
        Private dt_FechaCompra As Date

#End Region

#Region "Propiedades"

        Public Property CodigoCopiaLibro() As Integer
            Get
                Return int_CodigoCopiaLibro
            End Get
            Set(ByVal value As Integer)
                int_CodigoCopiaLibro = value
            End Set
        End Property

        Public Property CodigoEjemplar() As String
            Get
                Return str_CodigoEjemplar
            End Get
            Set(ByVal value As String)
                str_CodigoEjemplar = value
            End Set
        End Property


        Public Property CodigoLibro() As Integer
            Get
                Return int_CodigoLibro
            End Get
            Set(ByVal value As Integer)
                int_CodigoLibro = value
            End Set
        End Property

        Public Property CodigoBarra() As String
            Get
                Return int_CodigoBarra
            End Get
            Set(ByVal value As String)
                int_CodigoBarra = value
            End Set
        End Property

        Public Property Disponible() As Integer
            Get
                Return int_Disponible
            End Get
            Set(ByVal value As Integer)
                int_Disponible = value
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

        Public Property NumeroPago() As String
            Get
                Return str_NumeroPago
            End Get
            Set(ByVal value As String)
                str_NumeroPago = value
            End Set
        End Property

        Public Property FechaCompraDt() As Date
            Get
                Return dt_FechaCompra
            End Get
            Set(ByVal value As Date)
                dt_FechaCompra = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoCopiaLibro As Integer, _
                ByVal CodigoLibro As Integer, _
                ByVal CodigoBarra As String, _
                ByVal CodigoEjemplar As String, _
                ByVal Disponible As Integer, _
                ByVal Estado As Integer, _
                ByVal NumeroPago As String, _
                ByVal FechaCompraDt As Date)

            int_CodigoCopiaLibro = CodigoCopiaLibro
            int_CodigoLibro = CodigoLibro
            int_CodigoBarra = CodigoBarra
            int_Disponible = Disponible
            str_CodigoEjemplar = CodigoEjemplar
            int_Estado = Estado
            str_NumeroPago = NumeroPago
            dt_FechaCompra = FechaCompraDt

        End Sub

#End Region

    End Class

End Namespace