Namespace ModuloMensajeria

    Public Class be_MensajesRecibidos

#Region "Atributos"
        Private int_CodigoMensajeRecibido As Integer
        Private int_CodigoMensajeEnviado As Integer
        Private int_EstadoLectura As Integer
        Private dt_FechaLectura As Date
        Private int_CodigoPersonaRecibida As Integer
        Private int_CodigoTipoPersonaRecepcion As Integer

        Private str_CodigoUsuario As String

#End Region

#Region "Propiedades"

        Public Property CodigoMensajeRecibido() As Integer
            Get
                Return int_CodigoMensajeRecibido
            End Get
            Set(ByVal value As Integer)
                int_CodigoMensajeRecibido = value
            End Set
        End Property

        Public Property CodigoMensajeEnviado() As Integer
            Get
                Return int_CodigoMensajeEnviado
            End Get
            Set(ByVal value As Integer)
                int_CodigoMensajeEnviado = value
            End Set
        End Property

        Public Property EstadoLectura() As Integer
            Get
                Return int_EstadoLectura
            End Get
            Set(ByVal value As Integer)
                int_EstadoLectura = value
            End Set
        End Property

        Public Property FechaLectura() As Date
            Get
                Return dt_FechaLectura
            End Get
            Set(ByVal value As Date)
                dt_FechaLectura = value
            End Set
        End Property

        Public Property CodigoPersonaRecibida() As Integer
            Get
                Return int_CodigoPersonaRecibida
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaRecibida = value
            End Set
        End Property

        Public Property CodigoTipoPersonaRecepcion() As Integer
            Get
                Return int_CodigoTipoPersonaRecepcion
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoPersonaRecepcion = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return str_CodigoUsuario
            End Get
            Set(ByVal value As String)
                str_CodigoUsuario = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoMensajeRecibido As Integer, _
                ByVal CodigoMensajeEnviado As Integer, _
                ByVal FechaLectura As Date, _
                ByVal CodigoPersonaRecibida As Integer, _
                ByVal CodigoTipoPersonaRecepcion As Integer, _
                ByVal CodigoUsuario As String)

            int_CodigoMensajeRecibido = CodigoMensajeRecibido
            int_CodigoMensajeEnviado = CodigoMensajeEnviado
            int_EstadoLectura = EstadoLectura
            dt_FechaLectura = FechaLectura
            int_CodigoPersonaRecibida = CodigoPersonaRecibida
            int_CodigoTipoPersonaRecepcion = CodigoTipoPersonaRecepcion
            str_CodigoUsuario = CodigoUsuario

        End Sub

#End Region

    End Class

End Namespace