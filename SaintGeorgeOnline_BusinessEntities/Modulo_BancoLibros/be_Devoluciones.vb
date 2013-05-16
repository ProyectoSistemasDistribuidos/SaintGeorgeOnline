Namespace ModuloBancoLibros

    Public Class be_Devoluciones

#Region "Atributos"

        Private int_CodigoDetallePrestamo As Integer
        Private int_CodigoPrestamo As Integer
        Private int_CodigoLibro As Integer
        Private dt_FechaDevolucion As Date
        Private int_EstadoPrestamo As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoDetallePrestamo() As Integer
            Get
                Return int_CodigoDetallePrestamo
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetallePrestamo = value
            End Set
        End Property

        Public Property CodigoPrestamo() As Integer
            Get
                Return int_CodigoPrestamo
            End Get
            Set(ByVal value As Integer)
                int_CodigoPrestamo = value
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

        Public Property FechaDevolucion() As Date
            Get
                Return dt_FechaDevolucion
            End Get
            Set(ByVal value As Date)
                dt_FechaDevolucion = value
            End Set
        End Property

        Public Property EstadoPrestamo() As Integer
            Get
                Return int_EstadoPrestamo
            End Get
            Set(ByVal value As Integer)
                int_EstadoPrestamo = value
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

        Sub New(ByVal CodigoDetallePrestamo As Integer, _
                ByVal CodigoPrestamo As Integer, _
                ByVal CodigoLibro As Integer, _
                ByVal FechaDevolucion As Date, _
                ByVal EstadoPrestamo As Integer, _
                ByVal Estado As Integer)

            int_CodigoDetallePrestamo = CodigoDetallePrestamo
            int_CodigoPrestamo = CodigoPrestamo
            int_CodigoLibro = CodigoLibro
            dt_FechaDevolucion = FechaDevolucion
            int_EstadoPrestamo = EstadoPrestamo
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
