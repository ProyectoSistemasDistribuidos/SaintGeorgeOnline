Namespace ModuloSeguimiento

    Public Class be_ProgramacionMidTermReport

#Region "Atributos"

        Private int_CodigoProgramacion As Integer
        Private int_CodigoAnioAcademico As Integer
        Private int_CodigoBimestre As Integer
        Private dt_FechaApertura As Date
        Private int_Mes As Integer
        Private int_EstadoProgramacion As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoProgramacion() As Integer
            Get
                Return int_CodigoProgramacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacion = value
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

        Public Property CodigoBimestre() As Integer
            Get
                Return int_CodigoBimestre
            End Get
            Set(ByVal value As Integer)
                int_CodigoBimestre = value
            End Set
        End Property

        Public Property FechaApertura() As Date
            Get
                Return dt_FechaApertura
            End Get
            Set(ByVal value As Date)
                dt_FechaApertura = value
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

        Public Property EstadoProgramacion() As Integer
            Get
                Return int_EstadoProgramacion
            End Get
            Set(ByVal value As Integer)
                int_EstadoProgramacion = value
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

        Sub New(ByVal CodigoProgramacion As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoBimestre As Integer, _
                ByVal FechaApertura As Date, _
                ByVal Mes As Integer, _
                ByVal EstadoProgramacion As Integer, _
                ByVal Estado As Integer)

            int_CodigoProgramacion = CodigoProgramacion
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_CodigoBimestre = CodigoBimestre
            dt_FechaApertura = FechaApertura
            int_Mes = Mes
            int_EstadoProgramacion = EstadoProgramacion
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
