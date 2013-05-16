Namespace ModuloMensajeria

    Public Class be_MensajesEnviados

#Region "Atributos"

        Private int_CodigoMensajeEnviado As Integer
        Private str_Asunto As String
        Private str_CuerpoCorreo As String
        Private dt_FechaEnvio As Date
        Private int_EstadoEliminado As Integer
        Private int_CodigoPersonaEnvio As Integer
        Private int_CodigoTipoPersonaEnvio As Integer
        Private int_CodigoPersonaRecibida As Integer
        Private int_CodigoTipoPersonaRecepcion As Integer

        Private int_CodigoTipoMensaje As Integer
        Private str_CodigoUsuario As String
        Private int_ConfirmacionLectura As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoMensajeEnviado() As Integer
            Get
                Return int_CodigoMensajeEnviado
            End Get
            Set(ByVal value As Integer)
                int_CodigoMensajeEnviado = value
            End Set
        End Property

        Public Property Asunto() As String
            Get
                Return str_Asunto
            End Get
            Set(ByVal value As String)
                str_Asunto = value
            End Set
        End Property

        Public Property CuerpoCorreo() As String
            Get
                Return str_CuerpoCorreo
            End Get
            Set(ByVal value As String)
                str_CuerpoCorreo = value
            End Set
        End Property

        Public Property FechaEnvio() As Date
            Get
                Return dt_FechaEnvio
            End Get
            Set(ByVal value As Date)
                dt_FechaEnvio = value
            End Set
        End Property

        Public Property EstadoEliminado() As Integer
            Get
                Return int_EstadoEliminado
            End Get
            Set(ByVal value As Integer)
                int_EstadoEliminado = value
            End Set
        End Property

        Public Property CodigoPersonaEnvio() As Integer
            Get
                Return int_CodigoPersonaEnvio
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaEnvio = value
            End Set
        End Property

        Public Property CodigoTipoPersonaEnvio() As Integer
            Get
                Return int_CodigoTipoPersonaEnvio
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoPersonaEnvio = value
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

        Public Property CodigoTipoMensaje() As Integer
            Get
                Return int_CodigoTipoMensaje
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoMensaje = value
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

        Public Property ConfirmacionLectura() As Integer
            Get
                Return int_ConfirmacionLectura
            End Get
            Set(ByVal value As Integer)
                int_ConfirmacionLectura = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoMensajeEnviado As Integer, _
                ByVal Asunto As String, _
                ByVal CuerpoCorreo As String, _
                ByVal FechaEnvio As Date, _
                ByVal EstadoEliminado As Integer, _
                ByVal CodigoPersonaEnvio As Integer, _
                ByVal CodigoTipoPersonaEnvio As Integer, _
                ByVal CodigoTipoMensaje As Integer, _
                ByVal CodigoUsuario As String, _
                ByVal ConfirmacionLectura As Integer)

            int_CodigoMensajeEnviado = CodigoMensajeEnviado
            str_Asunto = Asunto
            str_CuerpoCorreo = CuerpoCorreo
            dt_FechaEnvio = FechaEnvio
            int_EstadoEliminado = EstadoEliminado
            int_CodigoPersonaEnvio = CodigoPersonaEnvio
            int_CodigoTipoPersonaEnvio = CodigoTipoPersonaEnvio
            int_CodigoTipoMensaje = CodigoTipoMensaje
            str_CodigoUsuario = CodigoUsuario
            int_ConfirmacionLectura = ConfirmacionLectura

        End Sub

#End Region

    End Class

End Namespace