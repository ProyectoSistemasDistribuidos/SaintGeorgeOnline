Namespace ModuloSeguimiento

    Public Class be_WeeklyReport

#Region "Atributos"
        Private int_CodigoWeeklyReport As Integer
        Private int_CodigoAlumnoWeekly As Integer
        Private int_CodigoCurso As Integer
        Private int_CodigoCalificativo As Integer
        Private int_CodigoCriterio As Integer
        Private str_Nota As String

#End Region

#Region "Propiedades"

        Public Property CodigoWeeklyReport() As Integer
            Get
                Return int_CodigoWeeklyReport
            End Get
            Set(ByVal value As Integer)
                int_CodigoWeeklyReport = value
            End Set
        End Property

        Public Property CodigoAlumnoWeekly() As Integer
            Get
                Return int_CodigoAlumnoWeekly
            End Get
            Set(ByVal value As Integer)
                int_CodigoAlumnoWeekly = value
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

        Public Property CodigoCalificativo() As Integer
            Get
                Return int_CodigoCalificativo
            End Get
            Set(ByVal value As Integer)
                int_CodigoCalificativo = value
            End Set
        End Property

        Public Property CodigoCriterio() As Integer
            Get
                Return int_CodigoCriterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoCriterio = value
            End Set
        End Property

        Public Property Nota() As String
            Get
                Return str_Nota
            End Get
            Set(ByVal value As String)
                str_Nota = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoWeeklyReport As Integer, _
                ByVal CodigoAlumnoWeekly As Integer, _
                ByVal CodigoCurso As Integer, _
                ByVal CodigoCalificativo As Integer, _
                ByVal CodigoCriterio As Integer, _
                ByVal Nota As String)

            int_CodigoWeeklyReport = CodigoWeeklyReport
            int_CodigoAlumnoWeekly = CodigoAlumnoWeekly
            int_CodigoCurso = CodigoCurso
            int_CodigoCalificativo = CodigoCalificativo
            int_CodigoCriterio = CodigoCriterio
            str_Nota = Nota
        End Sub

#End Region


    End Class

End Namespace



