Namespace ModuloPensiones

    Public Class be_PagosDetalle

#Region "Atributos"

        Private int_CodigoDetallePago As Integer
        Private int_CodigoPago As Integer
        Private int_CodigoConceptoCobro As Integer
        Private int_CodigoDeuda As Integer
        Private de_MontoCobro As Decimal
        Private de_MontoDescuento As Decimal
        Private de_MontoMora As Decimal
        Private de_MontoPago As Decimal
        Private int_Cantidad As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoDetallePago() As Integer
            Get
                Return int_CodigoDetallePago
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetallePago = value
            End Set
        End Property

        Public Property CodigoPago() As Integer
            Get
                Return int_CodigoPago
            End Get
            Set(ByVal value As Integer)
                int_CodigoPago = value
            End Set
        End Property

        Public Property CodigoConceptoCobro() As Integer
            Get
                Return int_CodigoConceptoCobro
            End Get
            Set(ByVal value As Integer)
                int_CodigoConceptoCobro = value
            End Set
        End Property

        Public Property CodigoDeuda() As Integer
            Get
                Return int_CodigoDeuda
            End Get
            Set(ByVal value As Integer)
                int_CodigoDeuda = value
            End Set
        End Property

        Public Property MontoCobro() As Decimal
            Get
                Return de_MontoCobro
            End Get
            Set(ByVal value As Decimal)
                de_MontoCobro = value
            End Set
        End Property

        Public Property MontoDescuento() As Decimal
            Get
                Return de_MontoDescuento
            End Get
            Set(ByVal value As Decimal)
                de_MontoDescuento = value
            End Set
        End Property

        Public Property MontoMora() As Decimal
            Get
                Return de_MontoMora
            End Get
            Set(ByVal value As Decimal)
                de_MontoMora = value
            End Set
        End Property

        Public Property MontoPago() As Decimal
            Get
                Return de_MontoPago
            End Get
            Set(ByVal value As Decimal)
                de_MontoPago = value
            End Set
        End Property

        Public Property Cantidad() As Integer
            Get
                Return int_Cantidad
            End Get
            Set(ByVal value As Integer)
                int_Cantidad = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoDetallePago As Integer, _
                ByVal CodigoPago As Integer, _
                ByVal CodigoConceptoCobro As Integer, _
                ByVal CodigoDeuda As Integer, _
                ByVal MontoCobro As Decimal, _
                ByVal MontoDescuento As Decimal, _
                ByVal MontoMora As Decimal, _
                ByVal MontoPago As Decimal, _
                ByVal Cantidad As Integer)

            int_CodigoDetallePago = CodigoDetallePago
            int_CodigoPago = CodigoPago
            int_CodigoConceptoCobro = CodigoConceptoCobro
            int_CodigoDeuda = CodigoDeuda
            de_MontoCobro = MontoCobro
            de_MontoDescuento = MontoDescuento
            de_MontoMora = MontoMora
            de_MontoPago = MontoPago
            int_Cantidad = Cantidad

        End Sub

#End Region

    End Class

End Namespace