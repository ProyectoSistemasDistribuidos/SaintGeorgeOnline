Namespace ModuloConfiguraciones

    Public Class be_SubSubSubCentrosCostos

#Region "Atributos"
        Private int_CodigoSubSubSubCentroCosto As Integer
        Private int_CodigoSubSubCentroCosto As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoSubSubSubCentroCosto() As Integer
            Get
                Return int_CodigoSubSubSubCentroCosto
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubSubSubCentroCosto = value
            End Set
        End Property

        Public Property CodigoSubSubCentroCosto() As Integer
            Get
                Return int_CodigoSubSubCentroCosto
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubSubCentroCosto = value
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

        Sub New(ByVal CodigoSubSubSubCentroCosto As Integer, _
                ByVal CodigoSubSubCentroCosto As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)
            int_CodigoSubSubSubCentroCosto = CodigoSubSubSubCentroCosto
            int_CodigoSubSubCentroCosto = CodigoSubSubCentroCosto
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

