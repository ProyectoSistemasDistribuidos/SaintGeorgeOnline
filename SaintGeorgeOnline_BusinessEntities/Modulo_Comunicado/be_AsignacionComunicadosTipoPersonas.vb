Namespace ModuloComunicado

    Public Class be_AsignacionComunicadosTipoPersonas

#Region "Atributos"
        Private int_CodigoAsignacionComunicadoTipoPersona As Integer
        Private int_CodigoTipoPersona As Integer
        Private int_CodigoComunicado As Integer
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionComunicadoTipoPersona() As Integer
            Get
                Return int_CodigoAsignacionComunicadoTipoPersona
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionComunicadoTipoPersona = value
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

        Public Property CodigoTipoPersona() As Integer
            Get
                Return int_CodigoTipoPersona
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoPersona = value
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

        Sub New(ByVal CodigoAsignacionComunicadoTipoPersona As Integer, _
                ByVal CodigoTipoPersona As Integer, _
                ByVal CodigoComunicado As Integer, _
                ByVal Estado As Integer)
            int_CodigoAsignacionComunicadoTipoPersona = CodigoAsignacionComunicadoTipoPersona
            int_CodigoTipoPersona = CodigoTipoPersona
            int_CodigoComunicado = CodigoComunicado
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace



