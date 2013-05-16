Namespace ModuloSeguimiento

    Public Class be_ProgramacionAlumnosWeekly

#Region "Atributos"

        Private int_CodigoAlumnoWeekly As Integer
        Private int_CodigoProgramacionWeekly As Integer
        Private int_CodigoSemanaAcademica As Integer
        Private int_CodigoAlumno As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAlumnoWeekly() As Integer
            Get
                Return int_CodigoAlumnoWeekly
            End Get
            Set(ByVal value As Integer)
                int_CodigoAlumnoWeekly = value
            End Set
        End Property

        Public Property CodigoProgramacionWeekly() As Integer
            Get
                Return int_CodigoProgramacionWeekly
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionWeekly = value
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

        Public Property CodigoAlumno() As Integer
            Get
                Return int_CodigoAlumno
            End Get
            Set(ByVal value As Integer)
                int_CodigoAlumno = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoAlumnoWeekly As Integer, _
                ByVal CodigoProgramacionWeekly As Integer, _
                ByVal CodigoSemanaAcademica As Integer, _
                ByVal CodigoAlumno As Integer)

            int_CodigoAlumnoWeekly = CodigoAlumnoWeekly
            int_CodigoProgramacionWeekly = CodigoProgramacionWeekly
            int_CodigoSemanaAcademica = CodigoSemanaAcademica
            int_CodigoAlumno = CodigoAlumno
        End Sub

#End Region

    End Class

End Namespace