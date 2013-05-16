Namespace ModuloMatricula

    Public Class be_Procedencia

#Region "Atributos"

        Private int_CodigoProcedencia As Integer
        Private int_CodigoAnioAcademico As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoColegioProcedencia As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoProcedencia() As Integer
            Get
                Return int_CodigoProcedencia
            End Get
            Set(ByVal value As Integer)
                int_CodigoProcedencia = value
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

        Public Property CodigoColegioProcedencia() As Integer
            Get
                Return int_CodigoColegioProcedencia
            End Get
            Set(ByVal value As Integer)
                int_CodigoColegioProcedencia = value
            End Set
        End Property


#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoProcedencia As Integer, _
                ByVal CodigoMotivoRetiro As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoColegio As Integer)
            int_CodigoProcedencia = CodigoProcedencia
            int_CodigoAnioAcademico = CodigoAnioAcademico
            str_CodigoAlumno = CodigoAlumno
            int_CodigoColegioProcedencia = CodigoColegioProcedencia

        End Sub

#End Region

    End Class

End Namespace
