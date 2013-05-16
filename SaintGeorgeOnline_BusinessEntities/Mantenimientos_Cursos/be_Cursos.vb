Namespace ModuloCursos

    Public Class be_Cursos

#Region "Atributos"

        Private int_CodigoCurso As Integer
        Private int_CodigoNombreCurso As Integer
        Private str_NombreCurso As String
        Private int_CodigoTipoCurso As Integer
        Private str_DescripcionActa As String
        Private str_DescripcionAbrev As String
        Private str_CodigoSie As String
        Private int_Estado As Integer
        Private int_CodigoDepartamento As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoCurso() As Integer
            Get
                Return int_CodigoCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoCurso = value
            End Set
        End Property

        Public Property CodigoNombreCurso() As Integer
            Get
                Return int_CodigoNombreCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoNombreCurso = value
            End Set
        End Property

        Public Property NombreCurso() As String
            Get
                Return str_NombreCurso
            End Get
            Set(ByVal value As String)
                str_NombreCurso = value
            End Set
        End Property


        Public Property CodigoTipoCurso() As Integer
            Get
                Return int_CodigoTipoCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoCurso = value
            End Set
        End Property

        Public Property DescripcionActa() As String
            Get
                Return str_DescripcionActa
            End Get
            Set(ByVal value As String)
                str_DescripcionActa = value
            End Set
        End Property

        Public Property DescripcionAbrev() As String
            Get
                Return str_DescripcionAbrev
            End Get
            Set(ByVal value As String)
                str_DescripcionAbrev = value
            End Set
        End Property

        Public Property CodigoSie() As String
            Get
                Return str_CodigoSie
            End Get
            Set(ByVal value As String)
                str_CodigoSie = value
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

        Public Property CodigoDepartamento() As Integer
            Get
                Return int_CodigoDepartamento
            End Get
            Set(ByVal value As Integer)
                int_CodigoDepartamento = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoCurso As Integer, _
                ByVal CodigoNombreCurso As Integer, _
                ByVal NombreCurso As String, _
                ByVal CodigoTipoCurso As Integer, _
                ByVal DescripcionActa As String, _
                ByVal DescripcionAbrev As String, _
                ByVal CodigoSie As String, _
                ByVal Estado As Integer, _
                ByVal CodigoDepartamento As Integer)

            int_CodigoCurso = CodigoCurso
            int_CodigoNombreCurso = CodigoNombreCurso
            str_NombreCurso = NombreCurso
            int_CodigoTipoCurso = CodigoTipoCurso
            str_DescripcionActa = DescripcionActa
            str_DescripcionAbrev = DescripcionAbrev
            str_CodigoSie = CodigoSie
            int_Estado = Estado
            int_CodigoDepartamento = CodigoDepartamento
        End Sub

#End Region

    End Class

End Namespace

