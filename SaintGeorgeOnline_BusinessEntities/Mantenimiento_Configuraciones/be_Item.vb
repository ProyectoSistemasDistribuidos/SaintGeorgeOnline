Namespace ModuloConfiguraciones

    Public Class be_Item

#Region "Atributos"

        Private int_CodigoItem As Integer
        Private int_CodigoTipoItem As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoItem() As Integer
            Get
                Return int_CodigoItem
            End Get
            Set(ByVal value As Integer)
                int_CodigoItem = value
            End Set
        End Property

        Public Property CodigoTipoItem() As Integer
            Get
                Return int_CodigoTipoItem
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoItem = value
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

        Sub New(ByVal CodigoItem As Integer, _
                ByVal CodigoTipoItem As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)

            int_CodigoItem = CodigoItem
            int_CodigoTipoItem = CodigoTipoItem
            str_Descripcion = Descripcion
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace

