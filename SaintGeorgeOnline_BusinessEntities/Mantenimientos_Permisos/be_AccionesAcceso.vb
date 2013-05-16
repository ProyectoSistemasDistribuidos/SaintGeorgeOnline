Namespace ModuloPermisos
    Public Class be_AccionesAcceso

#Region "Atributos"

        Private int_CodigoAsignacion As Integer
        Private int_CodigoAccion As Integer
        Private int_CodigoNombreAccion As Integer
        Private int_CodigoBloqueInformacion As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAsignacion() As Integer
            Get
                Return int_CodigoAsignacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacion = value
            End Set
        End Property

        Public Property CodigoAccion() As Integer
            Get
                Return int_CodigoAccion
            End Get
            Set(ByVal value As Integer)
                int_CodigoAccion = value
            End Set
        End Property

        Public Property CodigoNombreAccion() As Integer
            Get
                Return int_CodigoNombreAccion
            End Get
            Set(ByVal value As Integer)
                int_CodigoNombreAccion = value
            End Set
        End Property

        Public Property CodigoBloqueInformacion() As Integer
            Get
                Return int_CodigoBloqueInformacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoBloqueInformacion = value
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

        Sub New(ByVal CodigoAsignacion As Integer, _
                ByVal CodigoAccion As Integer, _
                ByVal CodigoNombreAccion As Integer, _
                ByVal CodigoBloqueInformacion As Integer, _
                ByVal Estado As Integer)

            int_CodigoAsignacion = CodigoAsignacion
            int_CodigoAccion = CodigoAccion
            int_CodigoNombreAccion = CodigoNombreAccion
            int_CodigoBloqueInformacion = CodigoBloqueInformacion
            int_Estado = Estado

        End Sub

#End Region

    End Class
End Namespace

