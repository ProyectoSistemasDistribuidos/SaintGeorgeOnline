Namespace ModuloPermisos
    Public Class be_AsignacionPermisosPerfiles

#Region "Atributos"

        Private int_CodigoSubBloque As Integer
        Private int_CodigoBloqueInformacion As Integer
        Private int_CodigoPerfil As Integer
        Private int_Habilitado As Integer
        Private int_CodigoAsignacion As Integer
        Private int_CodigoAccion As Integer
        Private int_CodigoNombreAccion As Integer
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

        Public Property CodigoAccion() As Integer
            Get
                Return int_CodigoAccion
            End Get
            Set(ByVal value As Integer)
                int_CodigoAccion = value
            End Set
        End Property

        Public Property CodigoAsignacion() As Integer
            Get
                Return int_CodigoAsignacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacion = value
            End Set
        End Property

        Public Property CodigoSubBloque() As Integer
            Get
                Return int_CodigoSubBloque
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubBloque = value
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

        Public Property CodigoPerfil() As Integer
            Get
                Return int_CodigoPerfil
            End Get
            Set(ByVal value As Integer)
                int_CodigoPerfil = value
            End Set
        End Property

        Public Property Habilitado() As Integer
            Get
                Return int_Habilitado
            End Get
            Set(ByVal value As Integer)
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
