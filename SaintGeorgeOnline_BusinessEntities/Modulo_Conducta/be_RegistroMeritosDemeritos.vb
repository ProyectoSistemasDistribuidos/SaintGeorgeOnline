Namespace ModuloConductaAlumnos

    Public Class be_RegistroMeritosDemeritos

#Region "Atributos"
        Private int_CodigoRegistroMeritoDemerito As Integer
        Private int_CodigoCriterioConducta As Integer
        Private int_CodigoTrabajador As Integer
        Private int_CodigoAsignacionGrupo As Integer
        Private int_CodigoRegistroConductaBimestral As Integer
        Private int_EstadoAprobacion As Integer
        Private dt_FechaRegistroMeritosDemeritos As Date
#End Region

#Region "Propiedades"

        Public Property CodigoRegistroMeritoDemerito() As Integer
            Get
                Return int_CodigoRegistroMeritoDemerito
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroMeritoDemerito = value
            End Set
        End Property

        Public Property CodigoCriterioConducta() As Integer
            Get
                Return int_CodigoCriterioConducta
            End Get
            Set(ByVal value As Integer)
                int_CodigoCriterioConducta = value
            End Set
        End Property

        Public Property CodigoTrabajador() As Integer
            Get
                Return int_CodigoTrabajador
            End Get
            Set(ByVal value As Integer)
                int_CodigoTrabajador = value
            End Set
        End Property

        Public Property CodigoAsignacionGrupo() As Integer
            Get
                Return int_CodigoAsignacionGrupo
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionGrupo = value
            End Set
        End Property

        Public Property CodigoRegistroConductaBimestral() As Integer
            Get
                Return int_CodigoRegistroConductaBimestral
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroConductaBimestral = value
            End Set
        End Property

        Public Property EstadoAprobacion() As Integer
            Get
                Return int_EstadoAprobacion
            End Get
            Set(ByVal value As Integer)
                int_EstadoAprobacion = value
            End Set
        End Property

        Public Property FechaRegistroMeritosDemeritos() As Date
            Get
                Return dt_FechaRegistroMeritosDemeritos
            End Get
            Set(ByVal value As Date)
                dt_FechaRegistroMeritosDemeritos = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroMeritoDemerito As Integer, _
                ByVal CodigoCriterioConducta As Integer, _
                ByVal CodigoTrabajador As Integer, _
                ByVal CodigoAsignacionGrupo As Integer, _
                ByVal CodigoRegistroConductaBimestral As Integer, _
                ByVal EstadoAprobacion As Integer, _
                ByVal FechaRegistroMeritosDemeritos As Date)
            int_CodigoRegistroMeritoDemerito = CodigoRegistroMeritoDemerito
            int_CodigoCriterioConducta = CodigoCriterioConducta
            int_CodigoTrabajador = CodigoTrabajador
            int_CodigoAsignacionGrupo = CodigoAsignacionGrupo
            int_CodigoRegistroConductaBimestral = CodigoRegistroConductaBimestral
            int_EstadoAprobacion = EstadoAprobacion
            dt_FechaRegistroMeritosDemeritos = FechaRegistroMeritosDemeritos
        End Sub

#End Region

    End Class

End Namespace



