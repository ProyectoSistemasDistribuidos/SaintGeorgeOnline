Namespace ModuloSeguimiento

    Public Class be_ProgramacionWeekly

#Region "Atributos"

        Private int_CodigoProgramacionWeekly As Integer
        Private int_CodigoAnioAcademico As Integer
        Private int_CodigoGrado As Integer
        Private int_CodigoBimestre As Integer
        Private int_CodigoAula As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoProgramacionWeekly() As Integer
            Get
                Return int_CodigoProgramacionWeekly
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionWeekly = value
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

        Public Property CodigoAula() As Integer
            Get
                Return int_CodigoAula
            End Get
            Set(ByVal value As Integer)
                int_CodigoAula = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoProgramacionWeekly As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoGrado As Integer, _
                 ByVal CodigoBimestre As Integer, _
                ByVal CodigoAula As Integer)

            int_CodigoProgramacionWeekly = CodigoProgramacionWeekly
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_CodigoGrado = CodigoGrado
            int_CodigoBimestre = CodigoBimestre
            int_CodigoAula = CodigoAula
        End Sub

#End Region

    End Class

End Namespace