Namespace ModuloPermisos

    Public Class be_CamposInformacion

#Region "Atributos"

        Private int_CodigoCampoInformacion As Integer
        Private str_Descripcion As String
        Private str_CampoBD As String
        Private str_CampoAlias As String
        Private str_TablaRef As String
        Private str_TipoDato As Integer
        Private str_Identificador As Integer
        Private int_Estado As Integer


#End Region

#Region "Propiedades"

        Public Property CodigoCampoInformacion() As Integer
            Get
                Return int_CodigoCampoInformacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoCampoInformacion = value
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

        Public Property CampoBD() As String
            Get
                Return str_CampoBD
            End Get
            Set(ByVal value As String)
                str_CampoBD = value
            End Set
        End Property

        Public Property CampoAlias() As String
            Get
                Return str_CampoAlias
            End Get
            Set(ByVal value As String)
                str_CampoAlias = value
            End Set
        End Property

        Public Property TablaRef() As String
            Get
                Return str_TablaRef
            End Get
            Set(ByVal value As String)
                str_TablaRef = value
            End Set
        End Property

        Public Property TipoDato() As Integer
            Get
                Return str_TipoDato
            End Get
            Set(ByVal value As Integer)
                str_TipoDato = value
            End Set
        End Property

        Public Property Identificador() As Integer
            Get
                Return str_Identificador
            End Get
            Set(ByVal value As Integer)
                str_Identificador = value
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
        Sub New(ByVal CodigoCampoInformacion As Integer, _
                ByVal Descripcion As String, _
                ByVal CampoBD As String, _
                ByVal CampoAlias As String, _
                ByVal TablaRef As Integer, _
                ByVal Identificador As Integer, _
                ByVal Estado As Integer)
            int_CodigoCampoInformacion = CodigoCampoInformacion
            str_Descripcion = Descripcion
            str_CampoBD = CampoBD
            str_CampoAlias = CampoAlias
            str_TablaRef = TablaRef
            str_TipoDato = TipoDato
            str_Identificador = Identificador
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace