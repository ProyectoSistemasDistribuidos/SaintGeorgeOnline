Namespace ModuloColegio

    Public Class be_AsignacionSemanasPorBimestres

#Region "Atributos"
        Private int_CodigoAsignacionSemana As Integer
        Private int_CodigoSemanaAcademica As Integer
        Private int_CodigoBimestre As Integer
        Private int_CodigoAnioAcademico As Integer
        Private dt_FechaInicio As Date
        Private dt_FechaFinal As Date
        Private int_Orden As Integer
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionSemana() As Integer
            Get
                Return int_CodigoAsignacionSemana
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionSemana = value
            End Set
        End Property

        Public Property CodigoSemanaAcademica() As Integer
            Get
                Return int_CodigoSemanaAcademica
            End Get
            Set(ByVal value As Integer)
                int_CodigoSemanaAcademica = value
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

        Public Property CodigoAnioAcademico() As Integer
            Get
                Return int_CodigoAnioAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoAnioAcademico = value
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

        Public Property FechaFinal() As Date
            Get
                Return dt_FechaFinal
            End Get
            Set(ByVal value As Date)
                dt_FechaFinal = value
            End Set
        End Property

        Public Property Orden() As Integer
            Get
                Return int_Orden
            End Get
            Set(ByVal value As Integer)
                int_Orden = value
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

        Sub New(ByVal CodigoAsignacionSemana As Integer, _
                ByVal CodigoSemanaAcademica As Integer, _
                ByVal CodigoBimestre As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal FechaInicio As Date, _
                ByVal FechaFinal As Date, _
                ByVal Orden As Integer, _
                ByVal Estado As Integer)
            int_CodigoAsignacionSemana = CodigoAsignacionSemana
            int_CodigoSemanaAcademica = CodigoSemanaAcademica
            int_CodigoBimestre = CodigoBimestre
            int_CodigoAnioAcademico = CodigoAnioAcademico
            dt_FechaInicio = FechaInicio
            dt_FechaFinal = FechaFinal
            int_Orden = Orden
            int_Estado = Estado
        End Sub

#End Region
    End Class

End Namespace
