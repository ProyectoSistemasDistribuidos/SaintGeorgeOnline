Namespace ModuloPensiones

    Public Class be_NotaCreditoDebitoDetalle

#Region "Atributos"

        Private int_CodigoDetalleNotaCredito As Integer
        Private int_CodigoNotaCredito As Integer
        Private int_CodigoDetallePago As Integer
        Private de_Monto As Decimal
        Private int_CodigoDetalleLetra As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoDetalleNotaCredito() As Integer
            Get
                Return int_CodigoDetalleNotaCredito
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetalleNotaCredito = value
            End Set
        End Property

        Public Property CodigoNotaCredito() As Integer
            Get
                Return int_CodigoNotaCredito
            End Get
            Set(ByVal value As Integer)
                int_CodigoNotaCredito = value
            End Set
        End Property

        Public Property CodigoDetallePago() As Integer
            Get
                Return int_CodigoDetallePago
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetallePago = value
            End Set
        End Property

        Public Property Monto() As Decimal
            Get
                Return de_Monto
            End Get
            Set(ByVal value As Decimal)
                de_Monto = value
            End Set
        End Property

        Public Property CodigoDetalleLetra() As Integer
            Get
                Return int_CodigoDetalleLetra
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetalleLetra = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoDetalleNotaCredito As Integer, _
                ByVal CodigoNotaCredito As Integer, _
                ByVal CodigoDetallePago As Integer, _
                ByVal Monto As Decimal, _
                ByVal CodigoDetalleLetra As Integer)

            int_CodigoDetalleNotaCredito = CodigoDetalleNotaCredito
            int_CodigoNotaCredito = CodigoNotaCredito
            int_CodigoDetallePago = CodigoDetallePago
            de_Monto = Monto
            int_CodigoDetalleLetra = CodigoDetalleLetra

        End Sub

#End Region

    End Class

End Namespace