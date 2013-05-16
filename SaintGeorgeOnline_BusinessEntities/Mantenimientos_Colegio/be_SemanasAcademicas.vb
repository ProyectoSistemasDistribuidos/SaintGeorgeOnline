Namespace ModuloColegio
    Public Class be_SemanasAcademicas
#Region "Atributos"
        Private int_CodigoSemanaAcademica As Integer
        Private str_Descripcion As String
        Private str_Abreviatura As String
        Private str_DescripcionIngles As String
#End Region
#Region "Propiedades"
        Public Property CodigoSemanaAcademica() As Integer
            Get
                Return int_CodigoSemanaAcademica
            End Get
            Set(ByVal value As Integer)
                int_CodigoSemanaAcademica = value
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
        Public Property Abreviatura() As String
            Get
                Return str_Abreviatura
            End Get
            Set(ByVal value As String)
                str_Abreviatura = value
            End Set
        End Property
        Public Property DescripcionIngles() As String
            Get
                Return str_DescripcionIngles
            End Get
            Set(ByVal value As String)
                str_DescripcionIngles = value
            End Set
        End Property
#End Region
#Region "Constructor"
        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoSemanaAcademica As Integer, ByVal Descripcion As String, ByVal Abreviatura As String, ByVal DescripcionIngles As String)
            int_CodigoSemanaAcademica = CodigoSemanaAcademica
            str_Descripcion = Descripcion
            str_Abreviatura = Abreviatura
            str_DescripcionIngles = DescripcionIngles
        End Sub
#End Region
    End Class
End Namespace

