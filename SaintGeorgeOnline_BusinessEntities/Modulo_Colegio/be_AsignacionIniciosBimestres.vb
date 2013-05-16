Namespace ModuloColegio

    Public Class be_AsignacionIniciosBimestres

#Region "Atributos"
        Private int_CodigoInicioBimestre As Integer
        Private int_CodigoAnioAcademico As Integer
        Private int_CodigoNivel As Integer
        Private int_CodigoGrado As Integer
        Private int_CodigoBimestre As Integer
        Private dt_FecInicio As Date
        Private dt_FecFin As Date
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoInicioBimestre() As Integer
            Get
                Return int_CodigoInicioBimestre
            End Get
            Set(ByVal value As Integer)
                int_CodigoInicioBimestre = value
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

        Public Property CodigoNivel() As Integer
            Get
                Return int_CodigoNivel
            End Get
            Set(ByVal value As Integer)
                int_CodigoNivel = value
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

        Public Property CodigoBimestre() As Integer
            Get
                Return int_CodigoBimestre
            End Get
            Set(ByVal value As Integer)
                int_CodigoBimestre = value
            End Set
        End Property

        Public Property FecInicio() As Date
            Get
                Return dt_FecInicio
            End Get
            Set(ByVal value As Date)
                dt_FecInicio = value
            End Set
        End Property

        Public Property FecFin() As Date
            Get
                Return dt_FecFin
            End Get
            Set(ByVal value As Date)
                dt_FecFin = value
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

        Sub New(ByVal CodigoInicioBimestre As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoNivel As Integer, _
                ByVal CodigoGrado As Integer, _
                ByVal CodigoBimestre As Integer, _
                ByVal FecInicio As Date, _
                ByVal FecFin As Date, _
                ByVal Estado As Integer)
            int_CodigoInicioBimestre = CodigoInicioBimestre
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_CodigoNivel = CodigoNivel
            int_CodigoGrado = CodigoGrado
            int_CodigoBimestre = CodigoBimestre
            dt_FecInicio = FecInicio
            dt_FecFin = FecFin
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

