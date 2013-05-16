Namespace ModuloPensiones

    Public Class be_CalendarioPagos

#Region "Atributos"

        Private int_CodigoCalendarioPagos As Integer
        Private int_CodigoAnioAcademico As Integer
        Private int_CodigoGrado As Integer
        Private int_CodigoMoneda As Integer
        Private int_CodigoConceptoCobro As Integer
        Private str_CodigoBanco As String
        Private int_Mes As Integer
        Private dt_FechaEmision As Date
        Private dt_FechaVencimiento As Date
        Private do_Monto As Double
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoCalendarioPagos() As Integer
            Get
                Return int_CodigoCalendarioPagos
            End Get
            Set(ByVal value As Integer)
                int_CodigoCalendarioPagos = value
            End Set
        End Property

        Public Property CodigoAnioAcademico() As Integer
            Get
                Return int_CodigoAnioAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoAnioAcademico = value
            End Set
        End Property

        Public Property CodigoGrado() As Integer
            Get
                Return int_CodigoGrado
            End Get
            Set(ByVal value As Integer)
                int_CodigoGrado = value
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

        Public Property CodigoBanco() As String
            Get
                Return str_CodigoBanco
            End Get
            Set(ByVal value As String)
                str_CodigoBanco = value
            End Set
        End Property

        Public Property Mes() As Integer
            Get
                Return int_Mes
            End Get
            Set(ByVal value As Integer)
                int_Mes = value
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

        Public Property Monto() As Double
            Get
                Return do_Monto
            End Get
            Set(ByVal value As Double)
                do_Monto = value
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
        Sub New(ByVal CodigoCalendarioPagos As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoGrado As Integer, _
                ByVal CodigoMoneda As Integer, _
                ByVal CodigoConceptoCobro As Integer, _
                ByVal CodigoBanco As String, _
                ByVal Mes As Integer, _
                ByVal FechaEmision As Date, _
                ByVal FechaVencimiento As Date, _
                ByVal Monto As Double, _
                ByVal Estado As Integer)

            int_CodigoCalendarioPagos = CodigoCalendarioPagos
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_CodigoGrado = CodigoGrado
            int_CodigoMoneda = CodigoMoneda
            int_CodigoConceptoCobro = CodigoConceptoCobro
            str_CodigoBanco = CodigoBanco
            int_Mes = Mes
            dt_FechaEmision = FechaEmision
            dt_FechaVencimiento = FechaVencimiento
            do_Monto = Monto
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace