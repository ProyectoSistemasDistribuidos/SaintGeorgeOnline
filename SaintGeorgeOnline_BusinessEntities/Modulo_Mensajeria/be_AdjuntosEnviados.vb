Namespace ModuloMensajeria

    Public Class be_AdjuntosEnviados

#Region "Atributos"

        Private int_CodigoMensajeAdjunto As Integer
        Private int_CodigoMensajeEnviado As Integer
        Private str_RutaAdjunto As String
        Private str_NombreArchivo As String
        Private int_TamanioArchivo As Integer
        Private str_Extension As String

#End Region

#Region "Propiedades"

        Public Property CodigoMensajeAdjunto() As Integer
            Get
                Return int_CodigoMensajeAdjunto
            End Get
            Set(ByVal value As Integer)
                int_CodigoMensajeAdjunto = value
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

        Public Property RutaAdjunto() As String
            Get
                Return str_RutaAdjunto
            End Get
            Set(ByVal value As String)
                str_RutaAdjunto = value
            End Set
        End Property


        Public Property NombreArchivo() As String
            Get
                Return str_NombreArchivo
            End Get
            Set(ByVal value As String)
                str_NombreArchivo = value
            End Set
        End Property

        Public Property TamanioArchivo() As Integer
            Get
                Return int_TamanioArchivo
            End Get
            Set(ByVal value As Integer)
                int_TamanioArchivo = value
            End Set
        End Property

        Public Property Extension() As String
            Get
                Return str_Extension
            End Get
            Set(ByVal value As String)
                str_Extension = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoMensajeAdjunto As Integer, _
                ByVal CodigoMensajeEnviado As Integer, _
                ByVal RutaAdjunto As String, _
                ByVal NombreArchivo As String, _
                ByVal TamanioArchivo As Integer, _
                ByVal Extension As String)

            int_CodigoMensajeAdjunto = CodigoMensajeAdjunto
            int_CodigoMensajeEnviado = CodigoMensajeEnviado
            str_RutaAdjunto = RutaAdjunto
            str_NombreArchivo = NombreArchivo
            int_TamanioArchivo = TamanioArchivo
            str_Extension = Extension

        End Sub

#End Region

    End Class

End Namespace