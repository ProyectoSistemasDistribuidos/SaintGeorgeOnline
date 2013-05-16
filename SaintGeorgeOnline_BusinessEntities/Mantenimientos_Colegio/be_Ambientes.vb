Namespace ModuloColegio

    Public Class be_Ambientes

#Region "Atributos"
        Private int_CodigoAmbiente As Integer
        Private int_CodigoSede As Integer
        Private str_NombreAmbiente As String
        Private int_CodigoTipoAmbiente As Integer
        Private int_CodigoTipoAmbienteProyecto As Integer
        Private int_CodigoPabellon As Integer
        Private int_CodigoPiso As Integer
        Private str_Referencia As String
        Private int_Capacidad As Integer
        Private str_CodigoAlfanumerico As String
        Private int_Reservable As Integer
        Private int_Multimedia As Integer
        Private int_Estado As Integer
        Private str_AreaAmbiente As String
#End Region

#Region "Propiedades"

        Public Property CodigoAmbiente() As Integer
            Get
                Return int_CodigoAmbiente
            End Get
            Set(ByVal value As Integer)
                int_CodigoAmbiente = value
            End Set
        End Property

        Public Property CodigoAlfanumerico() As String
            Get
                Return str_CodigoAlfanumerico
            End Get
            Set(ByVal value As String)
                str_CodigoAlfanumerico = value
            End Set
        End Property

        Public Property CodigoSede() As Integer
            Get
                Return int_CodigoSede
            End Get
            Set(ByVal value As Integer)
                int_CodigoSede = value
            End Set
        End Property

        Public Property NombreAmbiente() As String
            Get
                Return str_NombreAmbiente
            End Get
            Set(ByVal value As String)
                str_NombreAmbiente = value
            End Set
        End Property

        Public Property CodigoTipoAmbiente() As Integer
            Get
                Return int_CodigoTipoAmbiente
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoAmbiente = value
            End Set
        End Property

        Public Property CodigoPabellon() As Integer
            Get
                Return int_CodigoPabellon
            End Get
            Set(ByVal value As Integer)
                int_CodigoPabellon = value
            End Set
        End Property

        Public Property CodigoPiso() As Integer
            Get
                Return int_CodigoPiso
            End Get
            Set(ByVal value As Integer)
                int_CodigoPiso = value
            End Set
        End Property

        Public Property Referencia() As String
            Get
                Return str_Referencia
            End Get
            Set(ByVal value As String)
                str_Referencia = value
            End Set
        End Property

        Public Property Capacidad() As Integer
            Get
                Return int_Capacidad
            End Get
            Set(ByVal value As Integer)
                int_Capacidad = value
            End Set
        End Property

        Public Property CodigoTipoAmbienteProyecto() As Integer
            Get
                Return int_CodigoTipoAmbienteProyecto
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoAmbienteProyecto = value
            End Set
        End Property

        Public Property Reservable() As Integer
            Get
                Return int_Reservable
            End Get
            Set(ByVal value As Integer)
                int_Reservable = value
            End Set
        End Property

        Public Property Multimedia() As Integer
            Get
                Return int_Multimedia
            End Get
            Set(ByVal value As Integer)
                int_Multimedia = value
            End Set
        End Property

        Public Property AreaAmbiente() As String
            Get
                Return str_AreaAmbiente
            End Get
            Set(ByVal value As String)
                str_AreaAmbiente = value
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

        Sub New(ByVal CodigoAmbiente As Integer, _
                ByVal CodigoSede As Integer, _
                ByVal NombreAmbiente As String, _
                ByVal CodigoTipoAmbiente As Integer, _
                ByVal CodigoPabellon As Integer, _
                ByVal CodigoPiso As Integer, _
                ByVal Referencia As String, _
                ByVal Capacidad As Integer, _
                ByVal Reservable As Integer, _
                ByVal Multimedia As Integer, _
                ByVal Estado As Integer, _
                ByVal CodigoTipoAmbienteProyecto As Integer, _
                ByVal CodigoAlfanumerico As String, _
                ByVal AreaAmbiente As String)
            int_CodigoAmbiente = CodigoAmbiente
            int_CodigoSede = CodigoSede
            str_NombreAmbiente = NombreAmbiente
            int_CodigoTipoAmbiente = CodigoTipoAmbiente
            int_CodigoPabellon = CodigoPabellon
            int_CodigoPiso = CodigoPiso
            str_Referencia = Referencia
            int_Capacidad = Capacidad
            int_Reservable = Reservable
            int_Multimedia = Multimedia
            int_Estado = Estado
            int_CodigoTipoAmbienteProyecto = CodigoTipoAmbienteProyecto
            str_CodigoAlfanumerico = CodigoAlfanumerico
            str_AreaAmbiente = AreaAmbiente
        End Sub

#End Region

    End Class

End Namespace