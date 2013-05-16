Namespace ModuloCursos

    Public Class be_ConfiguracionAnualTaller

#Region "Atributos"

        Private int_Codigo As Integer
        Private int_CodigoPeriodoAcademico As Integer
        Private int_CodigoDuracionGrupo As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property Codigo() As Integer
            Get
                Return int_Codigo
            End Get
            Set(ByVal value As Integer)
                int_Codigo = value
            End Set
        End Property

        Public Property CodigoPeriodoAcademico() As Integer
            Get
                Return int_CodigoPeriodoAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoPeriodoAcademico = value
            End Set
        End Property

        Public Property CodigoDuracionGrupo() As Integer
            Get
                Return int_CodigoDuracionGrupo
            End Get
            Set(ByVal value As Integer)
                int_CodigoDuracionGrupo = value
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
        Sub New(ByVal Codigo As Integer, _
                ByVal CodigoPeriodoAcademico As Integer, _
                ByVal CodigoDuracionGrupo As Integer, _
                ByVal Estado As Integer)

            int_Codigo = Codigo
            int_CodigoPeriodoAcademico = CodigoPeriodoAcademico
            int_CodigoDuracionGrupo = CodigoDuracionGrupo
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace