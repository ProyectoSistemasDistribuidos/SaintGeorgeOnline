Namespace ModuloMatricula

    Public Class be_ColegiosExternos

#Region "Atributos"
        Private int_CodigoColegiosExternos As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoColegiosExternos() As Integer
            Get
                Return int_CodigoColegiosExternos
            End Get
            Set(ByVal value As Integer)
                int_CodigoColegiosExternos = value
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

        Sub New(ByVal CodigoColegiosExternos As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)
            int_CodigoColegiosExternos = CodigoColegiosExternos
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

