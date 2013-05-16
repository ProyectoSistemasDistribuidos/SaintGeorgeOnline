Namespace ModuloPensiones

    Public Class be_TipoCambio

#Region "Atributos"

        Private int_CodigoTipoCambio As Integer
        Private dt_Fecha As Date
        Private do_Compra As Double
        Private do_Venta As Double
        Private int_Estado As Integer
        Private int_CodigoMoneda As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoTipoCambio() As Integer
            Get
                Return int_CodigoTipoCambio
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoCambio = value
            End Set
        End Property

        Public Property Fecha() As Date
            Get
                Return dt_Fecha
            End Get
            Set(ByVal value As Date)
                dt_Fecha = value
            End Set
        End Property

        Public Property Venta() As Double
            Get
                Return do_Venta
            End Get
            Set(ByVal value As Double)
                do_Venta = value
            End Set
        End Property

        Public Property Compra() As Double
            Get
                Return do_Compra
            End Get
            Set(ByVal value As Double)
                do_Compra = value
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

        Public Property CodigoMoneda() As Integer
            Get
                Return int_CodigoMoneda
            End Get
            Set(ByVal value As Integer)
                int_CodigoMoneda = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoTipoCambio As Integer, _
                ByVal Fecha As Date, _
                ByVal Compra As Double, _
                ByVal Venta As Double, _
                ByVal Estado As Integer, _
                ByVal CodigoMoneda As Integer)

            int_CodigoTipoCambio = CodigoTipoCambio
            dt_Fecha = Fecha
            do_Venta = Venta
            do_Compra = Compra
            int_Estado = Estado
            int_CodigoMoneda = CodigoMoneda

        End Sub

#End Region

    End Class

End Namespace
