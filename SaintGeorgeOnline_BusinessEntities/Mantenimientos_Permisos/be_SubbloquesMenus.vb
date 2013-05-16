Namespace ModuloPermisos

    Public Class be_SubbloquesMenus

        'update 28/02/2011

#Region "Atributos"

        Private int_CodigoSubBloque As Integer
        Private int_CodigoBloque As Integer
        Private str_Descripcion As String
        Private str_Link As String
        Private str_RutaDocumentacion As String
        Private int_EstadoProceso As Integer
        Private int_Estado As Integer
        Private int_TipoSubBloque As Integer
        Private int_CodigoSubBloquePadre As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoSubBloque() As Integer
            Get
                Return int_CodigoSubBloque
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubBloque = value
            End Set
        End Property

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

        Public Property Link() As String
            Get
                Return str_Link
            End Get
            Set(ByVal value As String)
                str_Link = value
            End Set
        End Property

        Public Property RutaDocumentacion() As String
            Get
                Return str_RutaDocumentacion
            End Get
            Set(ByVal value As String)
                str_RutaDocumentacion = value
            End Set
        End Property

        Public Property EstadoProceso() As Integer
            Get
                Return int_EstadoProceso
            End Get
            Set(ByVal value As Integer)
                int_EstadoProceso = value
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

        Public Property TipoSubBloque() As Integer
            Get
                Return int_TipoSubBloque
            End Get
            Set(ByVal value As Integer)
                int_TipoSubBloque = value
            End Set
        End Property

        Public Property CodigoSubBloquePadre() As Integer
            Get
                Return int_CodigoSubBloquePadre
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubBloquePadre = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoSubBloque As Integer, _
                ByVal CodigoBloque As Integer, _
                ByVal Descripcion As String, _
                ByVal Link As String, _
                ByVal RutaDocumentacion As String, _
                ByVal EstadoProceso As Integer, _
                ByVal Estado As Integer, _
                ByVal TipoSubBloque As Integer, _
                ByVal CodigoSubBloquePadre As Integer)
            int_CodigoSubBloque = CodigoSubBloque
            int_CodigoBloque = CodigoBloque
            str_Descripcion = Descripcion
            str_Link = Link
            str_RutaDocumentacion = RutaDocumentacion
            int_EstadoProceso = EstadoProceso
            int_Estado = Estado
            int_TipoSubBloque = TipoSubBloque
            int_CodigoSubBloquePadre = CodigoSubBloquePadre
        End Sub

#End Region

    End Class

End Namespace
