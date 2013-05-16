Namespace ModuloComunicado

    Public Class be_AsignacionComunicadoIdiomas

#Region "Atributos"
        Private int_CodigoAsignacionComunicadoIdioma As Integer
        Private int_CodigoComunicado As Integer
        Private int_CodigoIdioma As Integer
        Private str_RutaAdjunto As String
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionComunicadoIdioma() As Integer
            Get
                Return int_CodigoAsignacionComunicadoIdioma
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionComunicadoIdioma = value
            End Set
        End Property

        Public Property CodigoIdioma() As Integer
            Get
                Return int_CodigoIdioma
            End Get
            Set(ByVal value As Integer)
                int_CodigoIdioma = value
            End Set
        End Property

        Public Property CodigoComunicado() As Integer
            Get
                Return int_CodigoComunicado
            End Get
            Set(ByVal value As Integer)
                int_CodigoComunicado = value
            End Set
        End Property

        Public Property RutaAdjunto() As String
            Get
                Return str_RutaAdjunto
            End Get
            Set(ByVal value As String)
                str_RutaAdjunto = value
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

        Sub New(ByVal CodigoAsignacionComunicadoIdioma As Integer, _
                ByVal CodigoComunicado As Integer, _
                ByVal CodigoIdioma As Integer, _
                ByVal RutaAdjunto As String, _
                ByVal Estado As Integer)
            int_CodigoAsignacionComunicadoIdioma = CodigoAsignacionComunicadoIdioma
            int_CodigoComunicado = CodigoComunicado
            int_CodigoIdioma = CodigoIdioma
            str_RutaAdjunto = RutaAdjunto
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

