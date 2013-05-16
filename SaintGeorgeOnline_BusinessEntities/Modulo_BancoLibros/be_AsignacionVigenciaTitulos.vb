Namespace ModuloBancoLibros
    Public Class be_AsignacionVigenciaTitulos
#Region "Atributos"
        Private int_CodigoAsignacionVigencia As Integer
        Private int_CodigoLibro As Integer
        Private int_CodigoAnioAcademico As Integer
        Private int_Estado As Integer
#End Region
#Region "Propiedades"
        Public Property CodigoAsignacionVigencia() As Integer
            Get
                Return int_CodigoAsignacionVigencia
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionVigencia = value
            End Set
        End Property
        Public Property CodigoLibro() As Integer
            Get
                Return int_CodigoLibro
            End Get
            Set(ByVal value As Integer)
                int_CodigoLibro = value
            End Set
        End Property
        Public Property CodigoAnioAcademico() As Integer
            Get
                Return CodigoAsignacionVigencia
            End Get
            Set(ByVal value As Integer)
                CodigoAsignacionVigencia = value
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
        Sub New(ByVal CodigoAsignacionVigencia As Integer, _
                ByVal CodigoLibro As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal Estado As Integer)
            int_CodigoAsignacionVigencia = CodigoAsignacionVigencia
            int_CodigoLibro = CodigoLibro
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_Estado = Estado
        End Sub
#End Region
    End Class
End Namespace

