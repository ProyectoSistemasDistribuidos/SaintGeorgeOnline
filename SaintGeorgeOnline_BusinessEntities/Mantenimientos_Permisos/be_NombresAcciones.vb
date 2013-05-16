Namespace ModuloPermisos

    Public Class be_NombresAcciones

#Region "Atributos"

        Private int_CodigoNombreAccion As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
        Private str_CodigoProgramacion As String

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

        Public Property Descripcion() As String
            Get
                Return str_Descripcion
            End Get
            Set(ByVal value As String)
                str_Descripcion = value
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

        Public Property CodigoProgramacion() As String
            Get
                Return str_CodigoProgramacion
            End Get
            Set(ByVal value As String)
                str_CodigoProgramacion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoNombreAccion As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer, _
                ByVal CodigoProgramacion As String)
            int_CodigoNombreAccion = CodigoNombreAccion
            str_Descripcion = Descripcion
            int_Estado = Estado
            str_CodigoProgramacion = CodigoProgramacion
        End Sub

#End Region

    End Class

End Namespace

