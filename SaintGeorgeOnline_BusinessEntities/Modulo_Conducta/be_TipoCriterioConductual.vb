Namespace ModuloConductaAlumnos
    Public Class be_TipoCriterioConductual
#Region "Atributos"
        Dim int_CodigoTipoCriterio As Integer
        Dim str_Descripcion As String
        Dim int_Estado As Integer
        Dim str_Signo As String
#End Region
#Region "Propiedades"
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
        Public Property Estado() As Integer
            Get
                Return int_Estado
            End Get
            Set(ByVal value As Integer)
                int_Estado = value
            End Set
        End Property
        Public Property Signo() As String
            Get
                Return str_Signo
            End Get
            Set(ByVal value As String)
                str_Signo = value
            End Set
        End Property
#End Region
#Region "Constructor"
        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoTipoCriterio As Integer, ByVal Descripcion As String, ByVal Estado As Integer, ByVal Signo As String)
            int_CodigoTipoCriterio = CodigoTipoCriterio
            str_Descripcion = Descripcion
            int_Estado = Estado
            str_Signo = Signo
        End Sub
#End Region



    End Class
End Namespace

