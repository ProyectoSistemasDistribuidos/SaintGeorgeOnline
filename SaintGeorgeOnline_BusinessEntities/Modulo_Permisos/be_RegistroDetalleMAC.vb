Namespace ModuloPermisos

    Public Class be_RegistroDetalleMAC

#Region "Atributos"

        Private int_CodigoCabecera As Integer
        Private int_CodigoDetalle As Integer
        Private int_CodigoTipoDispositivo As Integer
        Private str_DireccionMAC As String

#End Region

#Region "Propiedades"

        Public Property CodigoCabecera() As Integer
            Get
                Return int_CodigoCabecera
            End Get
            Set(ByVal value As Integer)
                int_CodigoCabecera = value
            End Set
        End Property
        Public Property CodigoDetalle() As Integer
            Get
                Return int_CodigoDetalle
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetalle = value
            End Set
        End Property
        Public Property CodigoTipoDispositivo() As Integer
            Get
                Return int_CodigoTipoDispositivo
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoDispositivo = value
            End Set
        End Property
        Public Property DireccionMAC() As String
            Get
                Return str_DireccionMAC
            End Get
            Set(ByVal value As String)
                str_DireccionMAC = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoCabecera As Integer, _
                ByVal CodigoDetalle As Integer, _
                ByVal CodigoTipoDispositivo As Integer, _
                ByVal DireccionMAC As String)

            int_CodigoCabecera = CodigoCabecera
            int_CodigoDetalle = CodigoDetalle
            int_CodigoTipoDispositivo = CodigoTipoDispositivo
            str_DireccionMAC = DireccionMAC

        End Sub

#End Region

    End Class

End Namespace