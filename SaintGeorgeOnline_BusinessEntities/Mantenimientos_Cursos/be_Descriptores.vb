Namespace ModuloCursos

    Public Class be_Descriptores

#Region "Atributos"

        Private int_CodigoDescriptores As Integer
        Private int_CodigoTipoDescriptores As Integer
        Private str_Descripcion As String
        Private int_Orden As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoDescriptores() As Integer
            Get
                Return int_CodigoDescriptores
            End Get
            Set(ByVal value As Integer)
                int_CodigoDescriptores = value
            End Set
        End Property

        Public Property CodigoTipoDescriptores() As Integer
            Get
                Return int_CodigoTipoDescriptores
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoDescriptores = value
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

        Public Property Orden() As Integer
            Get
                Return int_Orden
            End Get
            Set(ByVal value As Integer)
                int_Orden = value
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
        Sub New(ByVal CodigoDescriptores As Integer, _
                ByVal CodigoTipoDescriptores As Integer, _
                ByVal Descripcion As String, _
                ByVal Orden As Integer, _
                ByVal Estado As Integer)

            int_CodigoDescriptores = CodigoDescriptores
            int_CodigoTipoDescriptores = CodigoTipoDescriptores
            str_Descripcion = Descripcion
            int_Orden = Orden
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace
