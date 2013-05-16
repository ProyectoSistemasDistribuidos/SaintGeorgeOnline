Namespace ModuloPermisos

    Public Class be_RegistroMAC

#Region "Atributos"

        Private int_CodigoCabecera As Integer
        Private int_CodigoPersona As Integer
        Private int_CodigoTipoPersona As Integer

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
        Public Property CodigoPersona() As Integer
            Get
                Return int_CodigoPersona
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersona = value
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

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoCabecera As Integer, _
                ByVal CodigoPersona As Integer, _
                ByVal CodigoTipoPersona As Integer)

            int_CodigoCabecera = CodigoCabecera
            int_CodigoPersona = CodigoPersona
            int_CodigoTipoPersona = CodigoTipoPersona

        End Sub

#End Region

    End Class

End Namespace
