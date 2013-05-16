Namespace ModuloMatricula

    Public Class be_FichaSeguroRentaEstudiantil

#Region "Atributos"

        Private int_CodigoRentaEstudiantil As Integer
        Private int_CodigoAnioAcademico As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoFamiliarPrimerTitular As Integer
        Private int_CodigoFamiliarSegundoTitular As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRentaEstudiantil() As Integer
            Get
                Return int_CodigoRentaEstudiantil
            End Get
            Set(ByVal value As Integer)
                int_CodigoRentaEstudiantil = value
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

        Public Property CodigoAlumno() As String
            Get
                Return str_CodigoAlumno
            End Get
            Set(ByVal value As String)
                str_CodigoAlumno = value
            End Set
        End Property

        Public Property CodigoFamiliarPrimerTitular() As Integer
            Get
                Return int_CodigoFamiliarPrimerTitular
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamiliarPrimerTitular = value
            End Set
        End Property

        Public Property CodigoFamiliarSegundoTitular() As Integer
            Get
                Return int_CodigoFamiliarSegundoTitular
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamiliarSegundoTitular = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRentaEstudiantil As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoFamiliarPrimerTitular As Integer, _
                ByVal CodigoFamiliarSegundoTitular As Integer)

            int_CodigoRentaEstudiantil = CodigoRentaEstudiantil
            int_CodigoAnioAcademico = CodigoAnioAcademico
            str_CodigoAlumno = CodigoAlumno
            int_CodigoFamiliarPrimerTitular = CodigoFamiliarPrimerTitular
            int_CodigoFamiliarSegundoTitular = CodigoFamiliarSegundoTitular

        End Sub

#End Region

    End Class

End Namespace

