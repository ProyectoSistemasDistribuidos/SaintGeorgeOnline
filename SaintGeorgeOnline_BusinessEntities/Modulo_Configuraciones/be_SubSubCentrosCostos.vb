Namespace ModuloConfiguraciones

    Public Class be_SubSubCentrosCostos

#Region "Atributos"
        Private int_CodigoSubSubCentroCosto As Integer
        Private int_CodigoSubCentroCosto As Integer
        Private int_CodigoCentroCosto As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoSubSubCentroCosto() As Integer
            Get
                Return int_CodigoSubSubCentroCosto
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubSubCentroCosto = value
            End Set
        End Property


        Public Property CodigoSubCentroCosto() As Integer
            Get
                Return int_CodigoSubCentroCosto
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubCentroCosto = value
            End Set
        End Property

        Public Property CodigoCentroCosto() As Integer
            Get
                Return int_CodigoCentroCosto
            End Get
            Set(ByVal value As Integer)
                int_CodigoCentroCosto = value
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

        Sub New(ByVal CodigoSubSubCentroCosto As Integer, _
                ByVal CodigoSubCentroCosto As Integer, _
                ByVal CodigoCentroCosto As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)
            int_CodigoSubSubCentroCosto = CodigoSubSubCentroCosto
            int_CodigoSubCentroCosto = CodigoSubCentroCosto
            int_CodigoCentroCosto = CodigoCentroCosto
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
