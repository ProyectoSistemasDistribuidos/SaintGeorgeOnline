Namespace ModuloConductaAlumnos
    Public Class be_CriteriosConducta
#Region "Atributos"
        Private int_CodigoCriterioConducta As Integer
        Private int_CodigoTipoCriterio As Integer
        Private str_Descripcion As String
        Private int_Puntaje As Integer
#End Region
#Region "Propiedades"
        Public Property CodigoCriterioConducta() As Integer
            Get
                Return int_CodigoCriterioConducta
            End Get
            Set(ByVal value As Integer)
                int_CodigoCriterioConducta = value
            End Set
        End Property
        Public Property CodigoTipoCriterio() As Integer
            Get
                Return int_CodigoTipoCriterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoCriterio = value
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
        Public Property Puntaje() As Integer
            Get
                Return int_Puntaje
            End Get
            Set(ByVal value As Integer)
                int_Puntaje = value
            End Set
        End Property
#End Region
#Region "Constructor"
        Sub New()
            MyBase.new()
        End Sub
        Sub New(ByVal CodigoCriterioConducta As Integer, ByVal CodigoTipoCriterio As Integer, ByVal Descripcion As String, ByVal Puntaje As Integer)
            int_CodigoCriterioConducta = CodigoCriterioConducta
            int_CodigoTipoCriterio = CodigoTipoCriterio
            str_Descripcion = Descripcion
            int_Puntaje = Puntaje
        End Sub
#End Region
    End Class
End Namespace

