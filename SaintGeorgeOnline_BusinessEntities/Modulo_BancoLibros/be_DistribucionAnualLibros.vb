Namespace ModuloBancoLibros

    Public Class be_DistribucionAnualLibros

#Region "Atributos"
        Private int_CodigoAsignacionLibro As Integer
        Private int_CodigoLibro As Integer
        Private int_CodigoGrado As Integer
        Private int_CodigoAnioAcademico As Integer
        Private int_CodigoAula As String
        Private dt_FechaInicio As Date
        Private dt_FechaFin As Date
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionLibro() As Integer
            Get
                Return int_CodigoAsignacionLibro
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionLibro = value
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

        Public Property CodigoGrado() As Integer
            Get
                Return int_CodigoGrado
            End Get
            Set(ByVal value As Integer)
                int_CodigoGrado = value
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

        Public Property CodigoAula() As Integer
            Get
                Return int_CodigoAula
            End Get
            Set(ByVal value As Integer)
                int_CodigoAula = value
            End Set
        End Property

        Public Property FechaInicio() As Date
            Get
                Return dt_FechaInicio
            End Get
            Set(ByVal value As Date)
                dt_FechaInicio = value
            End Set
        End Property

        Public Property FechaFin() As Date
            Get
                Return dt_FechaFin
            End Get
            Set(ByVal value As Date)
                dt_FechaFin = value
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
        Sub New(ByVal CodigoAsignacionLibro As Integer, _
                ByVal CodigoLibro As Integer, _
                ByVal CodigoGrado As Integer, _
                ByVal CodigoEjemplar As String, _
                ByVal Disponible As Integer, _
                ByVal FechaInicio As Date, _
                ByVal FechaFin As Date, _
                ByVal Estado As Integer)

            int_CodigoAsignacionLibro = CodigoAsignacionLibro
            int_CodigoLibro = CodigoLibro
            int_CodigoGrado = CodigoGrado
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_CodigoAula = CodigoAula
            dt_FechaInicio = FechaInicio
            dt_FechaFin = FechaInicio
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
