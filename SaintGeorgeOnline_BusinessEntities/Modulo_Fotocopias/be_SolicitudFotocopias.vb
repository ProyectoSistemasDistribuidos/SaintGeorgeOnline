Namespace ModuloFotocopia

    Public Class be_SolicitudFotocopias

#Region "Atributos"

        Private int_CodigoSolicitudFotocopia As Integer
        Private int_CodigoTrabajadorRegistro As Integer
        Private dt_FechaRegistro As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoSolicitudFotocopia() As Integer
            Get
                Return int_CodigoSolicitudFotocopia
            End Get
            Set(ByVal value As Integer)
                int_CodigoSolicitudFotocopia = value
            End Set
        End Property

        Public Property CodigoTrabajadorRegistro() As Integer
            Get
                Return int_CodigoTrabajadorRegistro
            End Get
            Set(ByVal value As Integer)
                int_CodigoTrabajadorRegistro = value
            End Set
        End Property

        Public Property FechaRegistro() As String
            Get
                Return dt_FechaRegistro
            End Get
            Set(ByVal value As String)
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

        Sub New(ByVal CodigoSolicitudFotocopia As Integer, _
                ByVal CodigoTrabajadorRegistro As Integer, _
                ByVal FechaRegistro As String, _
                ByVal Estado As Integer)

            int_CodigoSolicitudFotocopia = CodigoSolicitudFotocopia
            int_CodigoTrabajadorRegistro = CodigoTrabajadorRegistro
            dt_FechaRegistro = FechaRegistro
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace