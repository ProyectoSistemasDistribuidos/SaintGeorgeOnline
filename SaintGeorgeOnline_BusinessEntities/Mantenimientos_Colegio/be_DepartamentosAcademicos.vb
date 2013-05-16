Namespace ModuloColegio

    Public Class be_DepartamentosAcademicos

        'update 28/02/2011

#Region "Atributos"

        Private int_CodigoDepartamentoAcademico As Integer
        Private int_CodigoPersonaJefeDepartamento As Integer
        Private str_Descripcion As String
        Private str_Abrev As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoDepartamentoAcademico() As Integer
            Get
                Return int_CodigoDepartamentoAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoDepartamentoAcademico = value
            End Set
        End Property

        Public Property CodigoPersonaJefeDepartamento() As Integer
            Get
                Return int_CodigoPersonaJefeDepartamento
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaJefeDepartamento = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return str_Descripcion
            End Get
            Set(ByVal value As String)
                str_Descripcion = value
            End Set
        End Property

        Public Property Abrev() As String
            Get
                Return str_Abrev
            End Get
            Set(ByVal value As String)
                str_Abrev = value
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
        Sub New(ByVal CodigoDepartamentoAcademico As Integer, _
                ByVal CodigoPersonaJefeDepartamento As Integer, _
                ByVal Descripcion As String, _
                ByVal Abrev As String, _
                ByVal Estado As Integer)

            int_CodigoDepartamentoAcademico = CodigoDepartamentoAcademico
            int_CodigoPersonaJefeDepartamento = CodigoPersonaJefeDepartamento
            str_Descripcion = Descripcion
            str_Abrev = Abrev
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
