Namespace ModuloFotocopia

    Public Class be_DetalleSolicitudFotocopias

#Region "Atributos"
        Private int_CodigoDetalleSolicitudFotocopia As Integer
        Private int_CodigoSolicitudFotocopia As Integer
        Private int_CodigoAsignacionGrupo As Integer
        Private int_CodigoGrado As Integer
        Private int_Estado As Integer
        Private int_NumeroCopias As Integer
        Private str_Tema As String
        Private dt_FechaImpresion As Date
        Private str_Observacion As String
        Private int_CodigoCurso As Integer
        Private int_CodigoEstadoProceso As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoDetalleSolicitudFotocopia() As Integer
            Get
                Return int_CodigoDetalleSolicitudFotocopia
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetalleSolicitudFotocopia = value
            End Set
        End Property

        Public Property CodigoSolicitudFotocopia() As Integer
            Get
                Return int_CodigoSolicitudFotocopia
            End Get
            Set(ByVal value As Integer)
                int_CodigoSolicitudFotocopia = value
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

        Public Property CodigoGrado() As Integer
            Get
                Return int_CodigoGrado
            End Get
            Set(ByVal value As Integer)
                int_CodigoGrado = value
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

        Public Property NumeroCopias() As Integer
            Get
                Return int_NumeroCopias
            End Get
            Set(ByVal value As Integer)
                int_NumeroCopias = value
            End Set
        End Property

        Public Property Tema() As String
            Get
                Return str_Tema
            End Get
            Set(ByVal value As String)
                str_Tema = value
            End Set
        End Property

        Public Property FechaImpresion() As Date
            Get
                Return dt_FechaImpresion
            End Get
            Set(ByVal value As Date)
                dt_FechaImpresion = value
            End Set
        End Property

        Public Property Observacion() As String
            Get
                Return str_Observacion
            End Get
            Set(ByVal value As String)
                str_Observacion = value
            End Set
        End Property

        Public Property CodigoCurso() As Integer
            Get
                Return int_CodigoCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoCurso = value
            End Set
        End Property

        Public Property CodigoEstadoProceso() As Integer
            Get
                Return int_CodigoEstadoProceso
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstadoProceso = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoDetalleSolicitudFotocopia As Integer, _
                ByVal CodigoSolicitudFotocopia As Integer, _
                ByVal CodigoAsignacionGrupo As Integer, _
                ByVal CodigoGrado As Integer, _
                ByVal Estado As Integer, _
                ByVal NumeroCopias As Integer, _
                ByVal Tema As String, _
                ByVal FechaImpresion As Date, _
                ByVal Observacion As String, _
                ByVal CodigoCurso As Integer, _
                ByVal CodigoEstadoProceso As Integer)
            int_CodigoDetalleSolicitudFotocopia = CodigoDetalleSolicitudFotocopia
            int_CodigoSolicitudFotocopia = CodigoSolicitudFotocopia
            int_CodigoAsignacionGrupo = CodigoAsignacionGrupo
            int_CodigoGrado = CodigoGrado
            int_Estado = Estado
            int_NumeroCopias = NumeroCopias
            str_Tema = Tema
            dt_FechaImpresion = FechaImpresion
            str_Observacion = Observacion
            int_CodigoCurso = CodigoCurso
            int_CodigoEstadoProceso = CodigoEstadoProceso
        End Sub

#End Region

    End Class

End Namespace