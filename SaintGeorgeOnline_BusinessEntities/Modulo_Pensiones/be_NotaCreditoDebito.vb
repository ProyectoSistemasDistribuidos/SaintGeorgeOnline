Namespace ModuloPensiones

    Public Class be_NotaCreditoDebito

#Region "Atributos"

        Private int_CodigoNotaCredito As Integer
        Private int_CodigoPago As Integer
        Private int_CodigoMoneda As Integer
        Private int_CodigoConceptoCobro As Integer
        Private int_CodigoTalonario As Integer
        Private str_NumeroNotaCredito As String
        Private dt_FechaEmision As Date
        Private int_TipoNota As Integer
        Private str_Observacion As String
        Private int_Estado As Integer
        Private int_CodigoLetra As Integer

        Private int_CodigoDocumento As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoNotaCredito() As Integer
            Get
                Return int_CodigoNotaCredito
            End Get
            Set(ByVal value As Integer)
                int_CodigoNotaCredito = value
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

        Public Property CodigoMoneda() As Integer
            Get
                Return int_CodigoMoneda
            End Get
            Set(ByVal value As Integer)
                int_CodigoMoneda = value
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

        Public Property CodigoTalonario() As Integer
            Get
                Return int_CodigoTalonario
            End Get
            Set(ByVal value As Integer)
                int_CodigoTalonario = value
            End Set
        End Property

        Public Property NumeroNotaCredito() As String
            Get
                Return str_NumeroNotaCredito
            End Get
            Set(ByVal value As String)
                str_NumeroNotaCredito = value
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

        Public Property TipoNota() As Integer
            Get
                Return int_TipoNota
            End Get
            Set(ByVal value As Integer)
                int_TipoNota = value
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

        Public Property Estado() As Integer
            Get
                Return int_Estado
            End Get
            Set(ByVal value As Integer)
                int_Estado = value
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

        Public Property CodigoDocumento() As Integer
            Get
                Return int_CodigoDocumento
            End Get
            Set(ByVal value As Integer)
                int_CodigoDocumento = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoNotaCredito As Integer, _
                ByVal CodigoPago As Integer, _
                ByVal CodigoMoneda As Integer, _
                ByVal CodigoConceptoCobro As Integer, _
                ByVal CodigoTalonario As Integer, _
                ByVal NumeroNotaCredito As String, _
                ByVal FechaEmision As Date, _
                ByVal TipoNota As Integer, _
                ByVal Observacion As String, _
                ByVal Estado As Integer, _
                ByVal CodigoLetra As Integer, _
                ByVal CodigoDocumento As Integer)

            int_CodigoNotaCredito = CodigoNotaCredito
            int_CodigoPago = CodigoPago
            int_CodigoMoneda = CodigoMoneda
            int_CodigoConceptoCobro = CodigoConceptoCobro
            int_CodigoTalonario = CodigoTalonario
            str_NumeroNotaCredito = NumeroNotaCredito
            dt_FechaEmision = FechaEmision
            int_TipoNota = TipoNota
            str_Observacion = Observacion
            int_Estado = Estado
            int_CodigoLetra = CodigoLetra
            int_CodigoDocumento = CodigoDocumento

        End Sub

#End Region

    End Class

End Namespace