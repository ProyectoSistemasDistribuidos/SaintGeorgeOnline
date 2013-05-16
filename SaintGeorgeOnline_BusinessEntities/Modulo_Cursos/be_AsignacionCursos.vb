Namespace ModuloCursos

    Public Class be_AsignacionCursos

#Region "Atributos"

        Private int_CodigoAsignacionCurso As Integer
        Private int_CodigoCurso As Integer
        Private int_CodigoFormaCalificacion As Integer
        Private int_CodigoGrado As Integer
        Private int_CodigoAnioAcademico As Integer
        Private int_CodigoAsignacionCursoPadre As Integer
        Private int_OrdenActa As Integer
        Private int_OrdenReporte As Integer
        Private int_NotaAprobatoria As Integer
        Private int_MaxComponentes As Integer
        Private int_MaxIndicadores As Integer
        Private int_MaxSubIndicadores As Integer
        Private int_MaxCriterios As Integer
        Private int_MaxEvaluaciones As Integer
        Private int_AutogenerarGrupo As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionCurso() As Integer
            Get
                Return int_CodigoAsignacionCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionCurso = value
            End Set
        End Property

        Public Property CodigoCurso() As Integer
            Get
                Return int_CodigoCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoCurso = value
            End Set
        End Property

        Public Property CodigoFormaCalificacion() As Integer
            Get
                Return int_CodigoFormaCalificacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoFormaCalificacion = value
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

        Public Property CodigoAsignacionCursoPadre() As Integer
            Get
                Return int_CodigoAsignacionCursoPadre
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionCursoPadre = value
            End Set
        End Property

        Public Property OrdenActa() As Integer
            Get
                Return int_OrdenActa
            End Get
            Set(ByVal value As Integer)
                int_OrdenActa = value
            End Set
        End Property

        Public Property OrdenReporte() As Integer
            Get
                Return int_OrdenReporte
            End Get
            Set(ByVal value As Integer)
                int_OrdenReporte = value
            End Set
        End Property

        Public Property NotaAprobatoria() As Integer
            Get
                Return int_NotaAprobatoria
            End Get
            Set(ByVal value As Integer)
                int_NotaAprobatoria = value
            End Set
        End Property

        Public Property MaxComponentes() As Integer
            Get
                Return int_MaxComponentes
            End Get
            Set(ByVal value As Integer)
                int_MaxComponentes = value
            End Set
        End Property

        Public Property MaxIndicadores() As Integer
            Get
                Return int_MaxIndicadores
            End Get
            Set(ByVal value As Integer)
                int_MaxIndicadores = value
            End Set
        End Property

        Public Property MaxSubIndicadores() As Integer
            Get
                Return int_MaxSubIndicadores
            End Get
            Set(ByVal value As Integer)
                int_MaxSubIndicadores = value
            End Set
        End Property

        Public Property MaxCriterios() As Integer
            Get
                Return int_MaxCriterios
            End Get
            Set(ByVal value As Integer)
                int_MaxCriterios = value
            End Set
        End Property

        Public Property MaxEvaluaciones() As Integer
            Get
                Return int_MaxEvaluaciones
            End Get
            Set(ByVal value As Integer)
                int_MaxEvaluaciones = value
            End Set
        End Property

        Public Property AutogenerarGrupo() As Integer
            Get
                Return int_AutogenerarGrupo
            End Get
            Set(ByVal value As Integer)
                int_AutogenerarGrupo = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoAsignacionCurso As Integer, _
                 ByVal CodigoCurso As Integer, _
                 ByVal CodigoFormaCalificacion As Integer, _
                 ByVal CodigoGrado As Integer, _
                 ByVal CodigoAnioAcademico As Integer, _
                 ByVal CodigoAsignacionCursoPadre As Integer, _
                 ByVal OrdenActa As Integer, _
                 ByVal OrdenReporte As Integer, _
                 ByVal NotaAprobatoria As Integer, _
                 ByVal MaxComponentes As Integer, _
                 ByVal MaxIndicadores As Integer, _
                 ByVal MaxSubIndicadores As Integer, _
                 ByVal MaxCriterios As Integer, _
                 ByVal MaxEvaluaciones As Integer, _
                 ByVal AutogenerarGrupo As Integer)

            int_CodigoAsignacionCurso = CodigoAsignacionCurso
            int_CodigoCurso = CodigoCurso
            int_CodigoFormaCalificacion = CodigoFormaCalificacion
            int_CodigoGrado = CodigoGrado
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_CodigoAsignacionCursoPadre = CodigoAsignacionCursoPadre
            int_OrdenActa = OrdenActa
            int_OrdenReporte = OrdenReporte
            int_NotaAprobatoria = NotaAprobatoria
            int_MaxComponentes = MaxComponentes
            int_MaxIndicadores = MaxIndicadores
            int_MaxSubIndicadores = MaxSubIndicadores
            int_MaxCriterios = MaxCriterios
            int_MaxEvaluaciones = MaxEvaluaciones
            int_AutogenerarGrupo = AutogenerarGrupo

        End Sub

#End Region

    End Class

End Namespace


