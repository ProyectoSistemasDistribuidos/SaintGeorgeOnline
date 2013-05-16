Namespace ModuloBancoLibros
    Public Class be_CursosLibros
#Region "Propiedades"
        Private int_CodigoCurso As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
#End Region
#Region "propiedades"
        Public Property CodigoCurso() As Integer
            Get
                Return int_CodigoCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoCurso = value
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
        Sub New(ByVal CodigoCurso As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)
            CodigoCurso = int_CodigoCurso
            Descripcion = str_Descripcion
            Estado = int_Estado
        End Sub
#End Region
    End Class
End Namespace

