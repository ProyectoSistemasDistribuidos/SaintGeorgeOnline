Namespace ModuloCursos

    Public Class be_AsignacionFormaCalificacion

#Region "Atributos"

        Private int_CodigoAsignacionFormaCalificacion As Integer
        Private int_CodigoFormaCalificacion As Integer
        Private int_CodigoGrado As Integer
        Private int_CodigoAnioAcademico As Integer
        Private str_TipoRegistro As String

#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionFormaCalificacion() As Integer
            Get
                Return int_CodigoAsignacionFormaCalificacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionFormaCalificacion = value
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

        Public Property TipoRegistro() As String
            Get
                Return str_TipoRegistro
            End Get
            Set(ByVal value As String)
                str_TipoRegistro = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoAsignacionFormaCalificacion As Integer, _
                ByVal CodigoFormaCalificacion As Integer, _
                ByVal CodigoGrado As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal TipoRegistro As String)

            int_CodigoAsignacionFormaCalificacion = CodigoAsignacionFormaCalificacion
            int_CodigoFormaCalificacion = CodigoFormaCalificacion
            int_CodigoGrado = CodigoGrado
            int_CodigoAnioAcademico = CodigoAnioAcademico
            str_TipoRegistro = TipoRegistro

        End Sub

#End Region

    End Class

End Namespace



