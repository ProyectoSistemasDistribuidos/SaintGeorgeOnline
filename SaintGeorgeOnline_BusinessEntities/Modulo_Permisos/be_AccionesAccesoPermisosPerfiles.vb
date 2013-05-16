Namespace ModuloPermisos
    Public Class be_AccionesAccesoPermisosPerfiles

#Region "Atributos"

        Private int_CodigoNombreAccion As Integer
        Private int_CodigoAccesoPerfil As Integer
        Private int_CodigoAccion As Integer
        Private int_CodigoPerfil As Integer
        Private int_Habilitado As Boolean

#End Region

#Region "Propiedades"

        Public Property CodigoNombreAccion() As Integer
            Get
                Return int_CodigoNombreAccion
            End Get
            Set(ByVal value As Integer)
                int_CodigoNombreAccion = value
            End Set
        End Property

        Public Property CodigoAccesoPerfil() As Integer
            Get
                Return int_CodigoAccesoPerfil
            End Get
            Set(ByVal value As Integer)
                int_CodigoAccesoPerfil = value
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

        Public Property CodigoPerfil() As Integer
            Get
                Return int_CodigoPerfil
            End Get
            Set(ByVal value As Integer)
                int_CodigoPerfil = value
            End Set
        End Property

        Public Property Habilitado() As Boolean
            Get
                Return int_Habilitado
            End Get
            Set(ByVal value As Boolean)
                int_Habilitado = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

#End Region

    End Class
End Namespace

