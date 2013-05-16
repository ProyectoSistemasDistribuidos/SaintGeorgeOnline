Namespace ModuloMatricula
    Public Class be_Matricula

#Region "Atributos"

        Private str_CodigoAlumno As String
        Private int_PeriodoAcademico As Integer
        Private int_CodigoPasoMatricula As Integer
        Private int_CodigoFamiliar As Integer
        Private int_CodigoGrado As Integer
        Private int_AceptacionEtapa As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAlumno() As String
            Get
                Return str_CodigoAlumno
            End Get
            Set(ByVal value As String)
                str_CodigoAlumno = value
            End Set
        End Property

        Public Property AceptacionEtapa() As Integer
            Get
                Return int_AceptacionEtapa
            End Get
            Set(ByVal value As Integer)
                int_AceptacionEtapa = value
            End Set
        End Property

        Public Property PeriodoAcademico() As Integer
            Get
                Return int_PeriodoAcademico
            End Get
            Set(ByVal value As Integer)
                int_PeriodoAcademico = value
            End Set
        End Property

        Public Property CodigoPasoMatricula() As Integer
            Get
                Return int_CodigoPasoMatricula
            End Get
            Set(ByVal value As Integer)
                int_CodigoPasoMatricula = value
            End Set
        End Property

        Public Property CodigoFamiliar() As Integer
            Get
                Return int_CodigoFamiliar
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamiliar = value
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

#End Region

#Region "Constructor"

        Sub New()
            MyBase.new()
        End Sub

#End Region

    End Class
End Namespace

