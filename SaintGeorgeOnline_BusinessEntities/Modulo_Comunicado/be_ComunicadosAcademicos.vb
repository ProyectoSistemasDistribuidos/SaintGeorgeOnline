Namespace ModuloComunicado

    Public Class be_ComunicadosAcademicos

#Region "Atributos"
        Private int_CodigoComunicado As Integer
        Private int_CodigoTipoComunicado As Integer
        Private int_CodigoIdioma As Integer
        Private str_Descripcion As String
        Private str_RutaAdjunto As String
        Private dt_FechaRegistro As Date
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoComunicado() As Integer
            Get
                Return int_CodigoComunicado
            End Get
            Set(ByVal value As Integer)
                int_CodigoComunicado = value
            End Set
        End Property

        Public Property CodigoTipoComunicado() As Integer
            Get
                Return int_CodigoTipoComunicado
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoComunicado = value
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

        Public Property Descripcion() As String
            Get
                Return str_Descripcion
            End Get
            Set(ByVal value As String)
                str_Descripcion = value
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

        Public Property FechaRegistro() As Date
            Get
                Return dt_FechaRegistro
            End Get
            Set(ByVal value As Date)
                dt_FechaRegistro = value
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

        Sub New(ByVal CodigoComunicado As Integer, _
                ByVal CodigoTipoComunicado As Integer, _
                ByVal CodigoIdioma As Integer, _
                ByVal Descripcion As String, _
                ByVal RutaAdjunto As String, _
                ByVal Estado As Integer)
            int_CodigoComunicado = CodigoComunicado
            int_CodigoTipoComunicado = CodigoTipoComunicado
            int_CodigoIdioma = CodigoIdioma
            str_Descripcion = Descripcion
            str_RutaAdjunto = RutaAdjunto
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

