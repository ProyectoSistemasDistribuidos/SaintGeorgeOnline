Namespace ModuloPensiones

    Public Class be_LetrasDetalle

#Region "Atributos"

        Private int_CodigoDetalleLetra As Integer
        Private int_CodigoLetra As Integer
        Private int_CodigoMoneda As Integer
        Private dt_FechaEmision As Date
        Private dt_FechaVencimiento As Date
        Private dt_FechaPago As Date
        Private de_MontoPagar As Decimal
        Private int_Orden As Integer
        Private str_NumeroLetra As String
        Private str_DescripcionLetra As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoDetalleLetra() As Integer
            Get
                Return int_CodigoDetalleLetra
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetalleLetra = value
            End Set
        End Property

        Public Property CodigoLetra() As Integer
            Get
                Return int_CodigoLetra
            End Get
            Set(ByVal value As Integer)
                int_CodigoLetra = value
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

        Public Property FechaEmision() As Date
            Get
                Return dt_FechaEmision
            End Get
            Set(ByVal value As Date)
                dt_FechaEmision = value
            End Set
        End Property

        Public Property FechaVencimiento() As Date
            Get
                Return dt_FechaVencimiento
            End Get
            Set(ByVal value As Date)
                dt_FechaVencimiento = value
            End Set
        End Property

        Public Property FechaPago() As Date
            Get
                Return dt_FechaPago
            End Get
            Set(ByVal value As Date)
                dt_FechaPago = value
            End Set
        End Property

        Public Property MontoPagar() As Decimal
            Get
                Return de_MontoPagar
            End Get
            Set(ByVal value As Decimal)
                de_MontoPagar = value
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

        Public Property NumeroLetra() As String
            Get
                Return str_NumeroLetra
            End Get
            Set(ByVal value As String)
                str_NumeroLetra = value
            End Set
        End Property

        Public Property DescripcionLetra() As String
            Get
                Return str_DescripcionLetra
            End Get
            Set(ByVal value As String)
                str_DescripcionLetra = value
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

        Sub New(ByVal CodigoDetalleLetra As Integer, _
                ByVal CodigoLetra As Integer, _
                ByVal CodigoMoneda As Integer, _
                ByVal FechaEmision As Date, _
                ByVal FechaVencimiento As Date, _
                ByVal FechaPago As Date, _
                ByVal MontoPagar As Decimal, _
                ByVal Orden As Integer, _
                ByVal NumeroLetra As String, _
                ByVal DescripcionLetra As String, _
                ByVal Estado As Integer)

            int_CodigoDetalleLetra = CodigoDetalleLetra
            int_CodigoLetra = CodigoLetra
            int_CodigoMoneda = CodigoMoneda
            dt_FechaEmision = FechaEmision
            dt_FechaVencimiento = FechaVencimiento
            dt_FechaPago = FechaPago
            de_MontoPagar = MontoPagar
            int_Orden = Orden
            str_NumeroLetra = NumeroLetra
            str_DescripcionLetra = DescripcionLetra
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace