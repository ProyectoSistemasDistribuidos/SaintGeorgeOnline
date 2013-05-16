Namespace ModuloPensiones

    Public Class be_Deudas
 
#Region "Atributos"

        Private int_CodigoDeuda As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoAnioAcademico As Integer
        Private int_CodigoConceptoCobro As Integer
        Private int_CodigoMoneda As Integer
        Private int_Mes As Integer
        Private dt_FechaEmision As Date
        Private dt_FechaVencimiento As Date
        Private int_EstadoDeuda As Integer
        Private de_MontoInicial As Decimal
        Private de_MontoDescuento As Decimal
        Private de_MontoTotal As Decimal
        Private de_MontoBono As Decimal

        Private str_Descripcion As String

#End Region

#Region "Propiedades"

        Public Property CodigoDeuda() As Integer
            Get
                Return int_CodigoDeuda
            End Get
            Set(ByVal value As Integer)
                int_CodigoDeuda = value
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

        Public Property CodigoAnioAcademico() As Integer
            Get
                Return int_CodigoAnioAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoAnioAcademico = value
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

        Public Property CodigoMoneda() As Integer
            Get
                Return int_CodigoMoneda
            End Get
            Set(ByVal value As Integer)
                int_CodigoMoneda = value
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

        Public Property EstadoDeuda() As Integer
            Get
                Return int_EstadoDeuda
            End Get
            Set(ByVal value As Integer)
                int_EstadoDeuda = value
            End Set
        End Property

        Public Property MontoInicial() As Decimal
            Get
                Return de_MontoInicial
            End Get
            Set(ByVal value As Decimal)
                de_MontoInicial = value
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

        Public Property MontoTotal() As Decimal
            Get
                Return de_MontoTotal
            End Get
            Set(ByVal value As Decimal)
                de_MontoTotal = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return str_Descripcion
            End Get
            Set(ByVal value As String)
                str_Descripcion = value
            End Set
        End Property

        Public Property MontoBono() As Decimal
            Get
                Return de_MontoBono
            End Get
            Set(ByVal value As Decimal)
                de_MontoBono = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoDeuda As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoConceptoCobro As Integer, _
                ByVal CodigoMoneda As Integer, _
                ByVal Mes As Integer, _
                ByVal FechaEmision As Date, _
                ByVal FechaVencimiento As Date, _
                ByVal EstadoDeuda As Integer, _
                ByVal MontoInicial As Decimal, _
                ByVal MontoDescuento As Decimal, _
                ByVal MontoTotal As Decimal, _
                ByVal Descripcion As String, _
                ByVal MontoBono As Decimal)

            int_CodigoDeuda = CodigoDeuda
            str_CodigoAlumno = CodigoAlumno
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_CodigoConceptoCobro = CodigoConceptoCobro
            int_CodigoMoneda = CodigoMoneda
            int_Mes = Mes
            dt_FechaEmision = FechaEmision
            dt_FechaVencimiento = FechaVencimiento
            int_EstadoDeuda = EstadoDeuda
            de_MontoInicial = MontoInicial
            de_MontoDescuento = MontoDescuento
            de_MontoTotal = MontoTotal
            str_Descripcion = Descripcion
            de_MontoBono = MontoBono

        End Sub

#End Region

    End Class

End Namespace
