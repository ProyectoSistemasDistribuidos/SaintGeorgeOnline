Namespace ModuloPermisos

    Public Class be_BloquesMenus

#Region "Atributos"

        Private int_CodigoBloque As Integer
        Private str_Descripcion As String
        Private str_RutaIcono As String
        Private int_Estado As Integer
        Private int_TipoBloque As Integer
        Private str_RutaLink As String

#End Region

#Region "Propiedades"

        Public Property CodigoBloque() As Integer
            Get
                Return int_CodigoBloque
            End Get
            Set(ByVal value As Integer)
                int_CodigoBloque = value
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

        Public Property RutaIcono() As String
            Get
                Return str_RutaIcono
            End Get
            Set(ByVal value As String)
                str_RutaIcono = value
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

        Public Property TipoBloque() As Integer
            Get
                Return int_TipoBloque
            End Get
            Set(ByVal value As Integer)
                int_TipoBloque = value
            End Set
        End Property

        Public Property RutaLink() As String
            Get
                Return str_RutaLink
            End Get
            Set(ByVal value As String)
                str_RutaLink = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoBloque As Integer, _
                ByVal Descripcion As String, _
                ByVal RutaIcono As String, _
                ByVal Estado As Integer, _
                ByVal TipoBloque As Integer, _
                ByVal RutaLink As String)
            int_CodigoBloque = CodigoBloque
            str_Descripcion = Descripcion
            str_RutaIcono = RutaIcono
            int_Estado = Estado
            int_TipoBloque = TipoBloque
            str_RutaLink = RutaLink
        End Sub

#End Region

    End Class

End Namespace
