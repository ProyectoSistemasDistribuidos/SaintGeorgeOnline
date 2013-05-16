Namespace ModuloPermisos

    Public Class be_AsignacionPermisosReportesPerfiles

#Region "Atributos"

        Private int_CodigoAsignacionPermisoReporte As Integer
        Private int_CodigoPresentacionReporte As Integer
        Private int_CodigoPerfil As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionPermisoReporte() As Integer
            Get
                Return int_CodigoAsignacionPermisoReporte
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionPermisoReporte = value
            End Set
        End Property

        Public Property CodigoPresentacionReporte() As Integer
            Get
                Return int_CodigoPresentacionReporte
            End Get
            Set(ByVal value As Integer)
                int_CodigoPresentacionReporte = value
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

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoAsignacionPermisoReporte As Integer, _
                ByVal CodigoPresentacionReporte As String, _
                ByVal CodigoPerfil As Integer)

            int_CodigoAsignacionPermisoReporte = CodigoAsignacionPermisoReporte
            int_CodigoPresentacionReporte = CodigoPresentacionReporte
            int_CodigoPerfil = CodigoPerfil

        End Sub

#End Region


    End Class

End Namespace