Namespace ModuloPensiones

    Public Class be_Pagos

#Region "Atributos"

        Private int_CodigoPago As Integer
        Private int_CodigoFormaPago As Integer
        Private int_CodigoOrigenPago As Integer
        Private int_CodigoBanco As Integer
        Private int_CodigoMoneda As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoTalonario As Integer
        Private int_CodigoCuentaBancaria As Integer
        Private dt_FechaPago As Date
        Private dt_FechaEmision As Date
        Private de_MontoTotalCobro As Decimal
        Private de_MontoTotalDescuento As Decimal
        Private de_MontoTotalMora As Decimal
        Private de_MontoTotalPago As Decimal
        Private int_PagoAnulado As Integer
        Private str_Observacion As String
        Private str_NumeroPago As String
        Private str_NumeroOperacionBancaria As String
        Private str_NumeroCheque As String
        Private int_CodigoEmpresa As Integer
        Private int_CodigoTipoPersona As Integer
        Private str_DescripcionOtraPersona As String

#End Region

#Region "Propiedades"

        Public Property CodigoPago() As Integer
            Get
                Return int_CodigoPago
            End Get
            Set(ByVal value As Integer)
                int_CodigoPago = value
            End Set
        End Property

        Public Property CodigoFormaPago() As Integer
            Get
                Return int_CodigoFormaPago
            End Get
            Set(ByVal value As Integer)
                int_CodigoFormaPago = value
            End Set
        End Property

        Public Property CodigoOrigenPago() As Integer
            Get
                Return int_CodigoOrigenPago
            End Get
            Set(ByVal value As Integer)
                int_CodigoOrigenPago = value
            End Set
        End Property

        Public Property CodigoBanco() As Integer
            Get
                Return int_CodigoBanco
            End Get
            Set(ByVal value As Integer)
                int_CodigoBanco = value
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

        Public Property CodigoAlumno() As String
            Get
                Return str_CodigoAlumno
            End Get
            Set(ByVal value As String)
                str_CodigoAlumno = value
            End Set
        End Property

        Public Property CodigoTalonario() As Integer
            Get
                Return int_CodigoTalonario
            End Get
            Set(ByVal value As Integer)
                int_CodigoTalonario = value
            End Set
        End Property

        Public Property CodigoCuentaBancaria() As Integer
            Get
                Return int_CodigoCuentaBancaria
            End Get
            Set(ByVal value As Integer)
                int_CodigoCuentaBancaria = value
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

        Public Property FechaEmision() As Date
            Get
                Return dt_FechaEmision
            End Get
            Set(ByVal value As Date)
                dt_FechaEmision = value
            End Set
        End Property

        Public Property MontoTotalCobro() As Decimal
            Get
                Return de_MontoTotalCobro
            End Get
            Set(ByVal value As Decimal)
                de_MontoTotalCobro = value
            End Set
        End Property

        Public Property MontoTotalDescuento() As Decimal
            Get
                Return de_MontoTotalDescuento
            End Get
            Set(ByVal value As Decimal)
                de_MontoTotalDescuento = value
            End Set
        End Property

        Public Property MontoTotalMora() As Decimal
            Get
                Return de_MontoTotalMora
            End Get
            Set(ByVal value As Decimal)
                de_MontoTotalMora = value
            End Set
        End Property

        Public Property MontoTotalPago() As Decimal
            Get
                Return de_MontoTotalPago
            End Get
            Set(ByVal value As Decimal)
                de_MontoTotalPago = value
            End Set
        End Property

        Public Property PagoAnulado() As Integer
            Get
                Return int_PagoAnulado
            End Get
            Set(ByVal value As Integer)
                int_PagoAnulado = value
            End Set
        End Property

        Public Property Observacion() As String
            Get
                Return str_Observacion
            End Get
            Set(ByVal value As String)
                str_Observacion = value
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

        Public Property NumeroOperacionBancaria() As String
            Get
                Return str_NumeroOperacionBancaria
            End Get
            Set(ByVal value As String)
                str_NumeroOperacionBancaria = value
            End Set
        End Property

        Public Property NumeroCheque() As String
            Get
                Return str_NumeroCheque
            End Get
            Set(ByVal value As String)
                str_NumeroCheque = value
            End Set
        End Property

        Public Property CodigoEmpresa() As Integer
            Get
                Return int_CodigoEmpresa
            End Get
            Set(ByVal value As Integer)
                int_CodigoEmpresa = value
            End Set
        End Property

        Public Property CodigoTipoPersona() As Integer
            Get
                Return int_CodigoTipoPersona
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoPersona = value
            End Set
        End Property

        Public Property DescripcionOtraPersona() As String
            Get
                Return str_DescripcionOtraPersona
            End Get
            Set(ByVal value As String)
                str_DescripcionOtraPersona = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoPago As Integer, _
                ByVal CodigoFormaPago As Integer, _
                ByVal CodigoOrigenPago As Integer, _
                ByVal CodigoBanco As Integer, _
                ByVal CodigoMoneda As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoTalonario As Integer, _
                ByVal CodigoCuentaBancaria As Integer, _
                ByVal FechaPago As Date, _
                ByVal FechaEmision As Date, _
                ByVal MontoTotalCobro As Decimal, _
                ByVal MontoTotalDescuento As Decimal, _
                ByVal MontoTotalMora As Decimal, _
                ByVal MontoTotalPago As Decimal, _
                ByVal PagoAnulado As Integer, _
                ByVal Observacion As String, _
                ByVal NumeroPago As String, _
                ByVal NumeroOperacionBancaria As String, _
                ByVal NumeroCheque As String, _
                ByVal CodigoEmpresa As Integer, _
                ByVal CodigoTipoPersona As Integer, _
                ByVal DescripcionOtraPersona As String)

            int_CodigoPago = CodigoPago
            int_CodigoFormaPago = CodigoFormaPago
            int_CodigoOrigenPago = CodigoOrigenPago
            int_CodigoBanco = CodigoBanco
            int_CodigoMoneda = CodigoMoneda
            str_CodigoAlumno = CodigoAlumno
            int_CodigoTalonario = CodigoTalonario
            int_CodigoCuentaBancaria = CodigoCuentaBancaria
            dt_FechaPago = FechaPago
            dt_FechaEmision = FechaEmision
            de_MontoTotalCobro = MontoTotalCobro
            de_MontoTotalDescuento = MontoTotalDescuento
            de_MontoTotalMora = MontoTotalMora
            de_MontoTotalPago = MontoTotalPago
            int_PagoAnulado = PagoAnulado
            str_Observacion = Observacion
            str_NumeroPago = NumeroPago
            str_NumeroOperacionBancaria = NumeroOperacionBancaria
            str_NumeroCheque = NumeroCheque
            int_CodigoEmpresa = CodigoEmpresa
            int_CodigoTipoPersona = CodigoTipoPersona
            str_DescripcionOtraPersona = DescripcionOtraPersona

        End Sub

#End Region

    End Class

End Namespace
