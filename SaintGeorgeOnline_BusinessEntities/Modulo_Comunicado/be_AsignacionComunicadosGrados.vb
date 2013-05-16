Namespace ModuloComunicado

    Public Class be_AsignacionComunicadosGrados

#Region "Atributos"
        Private int_CodigoAsignacionComunicadoGrados As Integer
        Private int_CodigoComunicado As Integer
        Private int_CodigoGrado As Integer
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionComunicadoGrados() As Integer
            Get
                Return int_CodigoAsignacionComunicadoGrados
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionComunicadoGrados = value
            End Set
        End Property

        Public Property CodigoGrado() As Integer
            Get
                Return int_CodigoGrado
            End Get
            Set(ByVal value As Integer)
                int_CodigoGrado = value
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

        Sub New(ByVal CodigoAsignacionComunicadoGrados As Integer, _
                ByVal CodigoComunicado As Integer, _
                ByVal CodigoGrado As Integer, _
                ByVal Estado As Integer)
            int_CodigoAsignacionComunicadoGrados = CodigoAsignacionComunicadoGrados
            int_CodigoComunicado = CodigoComunicado
            int_CodigoGrado = CodigoGrado
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

