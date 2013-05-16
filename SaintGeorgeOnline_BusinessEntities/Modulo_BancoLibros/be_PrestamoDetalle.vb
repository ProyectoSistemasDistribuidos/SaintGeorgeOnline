Namespace ModuloBancoLibros

    Public Class be_PrestamoDetalle

#Region "Atributos"

        Private int_CodigoDetallePrestamo As Integer
        Private int_CodigoPrestamo As Integer
        Private int_CodigoLibro As Integer
        Private int_CodigoCopiaLibro As Integer
        Private dt_FechaPrestamo As Date
        Private dt_FechaDevolucion As Date
        Private int_Estado As Integer
        Private str_CodigoBarra As String

#End Region

#Region "Propiedades"

        Public Property CodigoBarra() As String
            Get
                Return str_CodigoBarra
            End Get
            Set(ByVal value As String)
                str_CodigoBarra = value
            End Set
        End Property

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

        Public Property CodigoCopiaLibro() As Integer
            Get
                Return int_CodigoCopiaLibro
            End Get
            Set(ByVal value As Integer)
                int_CodigoCopiaLibro = value
            End Set
        End Property

        Public Property FechaPrestamo() As Date
            Get
                Return dt_FechaPrestamo
            End Get
            Set(ByVal value As Date)
                dt_FechaPrestamo = value
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
                ByVal CodigoCopiaLibro As Integer, _
                ByVal FechaPrestamo As Date, _
                ByVal FechaDevolucion As Date, _
                ByVal Estado As Integer, _
                ByVal CodigoBarra As String)

            int_CodigoDetallePrestamo = CodigoDetallePrestamo
            int_CodigoPrestamo = CodigoPrestamo
            int_CodigoLibro = CodigoLibro
            int_CodigoCopiaLibro = CodigoCopiaLibro
            dt_FechaPrestamo = FechaPrestamo
            dt_FechaDevolucion = FechaDevolucion
            int_Estado = Estado
            str_CodigoBarra = CodigoBarra
        End Sub

#End Region

    End Class

End Namespace
