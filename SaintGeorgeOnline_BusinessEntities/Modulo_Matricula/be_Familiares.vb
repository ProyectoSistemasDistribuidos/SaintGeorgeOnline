Namespace ModuloMatricula

    Public Class be_Familiares
        Inherits be_Personas

#Region "Atributos"

        Private int_CodigoFamiliar As Integer
        Private int_CodigoServicioRadioDomicilio As Integer
        Private int_CodigoPaisDomicilio As Integer
        Private int_CodigoEscolaridadMinisterio As Integer
        Private str_CodigoUbigeoCentroTrabajo As String
        Private int_CodigoServicioRadioOficina As Integer
        Private int_CodigoNivelInstruccion As Integer
        Private int_CodigoSituacionLaboral As Integer

        Private int_Vive As Integer
        Private dt_FechaDefuncion As Date
        Private int_ExAlumno As Integer
        Private int_ExAlumnoAnioEgreso As Integer
        Private str_Usuario As String
        Private str_Contrasenia As String
        Private int_EstadoAcceso As Integer
        Private str_OcupacionCargo As String
        Private str_CentroTrabajo As String
        Private int_CodigoPaisCentroTrabajo As Integer
        Private str_DireccionCentroTrabajo As String
        Private str_TelefonoOficina As String
        Private str_CelularOficina As String
        Private str_NumeroServicioRadioOficina As String
        Private str_EmailOficina As String
        Private int_AccesoInternetOficina As Integer
        Private str_NombreIglesia As String
        Private int_AccesoInternet As Integer
        Private str_NumeroServicioRadioPersonal As String
        Private str_ColegioEgreso As String
        Private str_ContinuaEstudios As String

#End Region

#Region "Propiedades"

        Public Property CodigoFamiliar() As Integer
            Get
                Return int_CodigoFamiliar
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamiliar = value
            End Set
        End Property

        Public Property CodigoServicioRadioDomicilio() As Integer
            Get
                Return int_CodigoServicioRadioDomicilio
            End Get
            Set(ByVal value As Integer)
                int_CodigoServicioRadioDomicilio = value
            End Set
        End Property

        Public Property CodigoPaisDomicilio() As Integer
            Get
                Return int_CodigoPaisDomicilio
            End Get
            Set(ByVal value As Integer)
                int_CodigoPaisDomicilio = value
            End Set
        End Property

        Public Property CodigoEscolaridadMinisterio() As Integer
            Get
                Return int_CodigoEscolaridadMinisterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoEscolaridadMinisterio = value
            End Set
        End Property

        Public Property CodigoUbigeoCentroTrabajo() As String
            Get
                Return str_CodigoUbigeoCentroTrabajo
            End Get
            Set(ByVal value As String)
                str_CodigoUbigeoCentroTrabajo = value
            End Set
        End Property

        Public Property CodigoServicioRadioOficina() As Integer
            Get
                Return int_CodigoServicioRadioOficina
            End Get
            Set(ByVal value As Integer)
                int_CodigoServicioRadioOficina = value
            End Set
        End Property

        Public Property CodigoNivelInstruccion() As Integer
            Get
                Return int_CodigoNivelInstruccion
            End Get
            Set(ByVal value As Integer)
                int_CodigoNivelInstruccion = value
            End Set
        End Property

        Public Property codigosituacionlaboral() As Integer
            Get
                Return int_CodigoSituacionLaboral
            End Get
            Set(ByVal value As Integer)
                int_CodigoSituacionLaboral = value
            End Set
        End Property

        Public Property Vive() As Integer
            Get
                Return int_Vive
            End Get
            Set(ByVal value As Integer)
                int_Vive = value
            End Set
        End Property

        Public Property FechaDefuncion() As Date
            Get
                Return dt_FechaDefuncion
            End Get
            Set(ByVal value As Date)
                dt_FechaDefuncion = value
            End Set
        End Property

        Public Property ExAlumno() As Integer
            Get
                Return int_ExAlumno
            End Get
            Set(ByVal value As Integer)
                int_ExAlumno = value
            End Set
        End Property

        Public Property ExAlumnoAnioEgreso() As Integer
            Get
                Return int_ExAlumnoAnioEgreso
            End Get
            Set(ByVal value As Integer)
                int_ExAlumnoAnioEgreso = value
            End Set
        End Property

        Public Property Usuario() As String
            Get
                Return str_Usuario

            End Get
            Set(ByVal value As String)
                str_usuario = value
            End Set
        End Property

        Public Property Contrasenia() As String
            Get
                Return str_Contrasenia
            End Get
            Set(ByVal value As String)
                str_Contrasenia = value
            End Set
        End Property

        Public Property EstadoAcceso() As Integer
            Get
                Return int_EstadoAcceso
            End Get
            Set(ByVal value As Integer)
                int_EstadoAcceso = value
            End Set
        End Property

        Public Property OcupacionCargo() As String
            Get
                Return str_OcupacionCargo
            End Get
            Set(ByVal value As String)
                str_OcupacionCargo = value
            End Set
        End Property

        Public Property CentroTrabajo() As String
            Get
                Return str_CentroTrabajo
            End Get
            Set(ByVal value As String)
                str_CentroTrabajo = value
            End Set
        End Property

        Public Property CodigoPaisCentroTrabajo() As Integer
            Get
                Return int_CodigoPaisCentroTrabajo
            End Get
            Set(ByVal value As Integer)
                int_CodigoPaisCentroTrabajo = value
            End Set
        End Property

        Public Property DireccionCentroTrabajo() As String
            Get
                Return Str_DireccionCentroTrabajo
            End Get
            Set(ByVal value As String)
                str_DireccionCentroTrabajo = value
            End Set
        End Property

        Public Property TelefonoOficina() As String
            Get
                Return str_TelefonoOficina
            End Get
            Set(ByVal value As String)
                str_TelefonoOficina = value
            End Set
        End Property

        Public Property CelularOficina() As String
            Get
                Return str_CelularOficina
            End Get
            Set(ByVal value As String)
                str_CelularOficina = value
            End Set
        End Property

        Public Property NumeroServicioRadioOficina() As String
            Get
                Return str_NumeroServicioRadioOficina
            End Get
            Set(ByVal value As String)
                str_NumeroServicioRadioOficina = value
            End Set
        End Property

        Public Property EmailOficina() As String
            Get
                Return str_EmailOficina
            End Get
            Set(ByVal value As String)
                str_EmailOficina = value
            End Set
        End Property

        Public Property AccesoInternetOficina() As Integer
            Get
                Return int_AccesoInternetOficina
            End Get
            Set(ByVal value As Integer)
                int_AccesoInternetOficina = value
            End Set
        End Property

        Public Property NombreIglesia() As String
            Get
                Return str_NombreIglesia
            End Get
            Set(ByVal value As String)
                str_NombreIglesia = value
            End Set
        End Property

        Public Property AccesoInternet() As Integer
            Get
                Return int_AccesoInternet
            End Get
            Set(ByVal value As Integer)
                int_AccesoInternet = value
            End Set
        End Property

        Public Property NumeroServicioRadioPersonal() As String
            Get
                Return str_NumeroServicioRadioPersonal
            End Get
            Set(ByVal value As String)
                str_NumeroServicioRadioPersonal = value
            End Set
        End Property

        Public Property ColegioEgreso() As String
            Get
                Return str_ColegioEgreso
            End Get
            Set(ByVal value As String)
                str_ColegioEgreso = value
            End Set
        End Property

        Public Property ContinuaEstudios() As String
            Get
                Return str_ContinuaEstudios
            End Get
            Set(ByVal value As String)
                str_ContinuaEstudios = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.new()
        End Sub

        Sub New(ByVal CodigoFamiliar As Integer, _
                ByVal CodigoServicioRadioDomicilio As Integer, _
                ByVal CodigoPaisDomicilio As Integer, _
                ByVal CodigoEscolaridadMinisterio As Integer, _
                ByVal CodigoUbigeoCentroTrabajo As String, _
                ByVal CodigoServicioRadioOficina As Integer, _
                ByVal CodigoNivelInstruccion As Integer, _
                ByVal CodigoSituacionLaboral As Integer, _
                ByVal Vive As Integer, _
                ByVal FechaDefuncion As Date, _
                ByVal ExAlumno As Integer, _
                ByVal ExAlumnoAnioEgreso As Integer, _
                ByVal Usuario As String, _
                ByVal Contrasenia As String, _
                ByVal EstadoAcceso As Integer, _
                ByVal OcupacionCargo As String, _
                ByVal CentroTrabajo As String, _
                ByVal CodigoPaisCentroTrabajo As Integer, _
                ByVal DireccionCentroTrabajo As String, _
                ByVal TelefonoOficina As String, _
                ByVal CelularOficina As String, _
                ByVal NumeroServicioRadioOficina As String, _
                ByVal EmailOficina As String, _
                ByVal AccesoInternetOficina As Integer, _
                ByVal NombreIglesia As String, _
                ByVal AccesoInternet As Integer, _
                ByVal NumeroServicioRadioPersonal As String, _
                ByVal ColegioEgreso As String, _
                ByVal ContinuaEstudios As String)

            int_CodigoFamiliar = CodigoFamiliar
            int_CodigoServicioRadioDomicilio = CodigoServicioRadioDomicilio
            int_CodigoPaisDomicilio = CodigoPaisDomicilio
            int_CodigoEscolaridadMinisterio = CodigoEscolaridadMinisterio
            str_CodigoUbigeoCentroTrabajo = CodigoUbigeoCentroTrabajo
            int_CodigoServicioRadioOficina = CodigoServicioRadioOficina
            int_CodigoNivelInstruccion = CodigoNivelInstruccion
            int_CodigoSituacionLaboral = CodigoSituacionLaboral
            int_Vive = Vive
            dt_FechaDefuncion = FechaDefuncion
            int_ExAlumno = ExAlumno
            int_ExAlumnoAnioEgreso = ExAlumnoAnioEgreso
            str_Usuario = Usuario
            str_Contrasenia = Contrasenia
            int_EstadoAcceso = EstadoAcceso
            str_OcupacionCargo = OcupacionCargo
            str_CentroTrabajo = CentroTrabajo
            int_CodigoPaisCentroTrabajo = CodigoPaisCentroTrabajo
            str_DireccionCentroTrabajo = DireccionCentroTrabajo
            str_TelefonoOficina = TelefonoOficina
            str_CelularOficina = CelularOficina
            str_NumeroServicioRadioOficina = NumeroServicioRadioOficina
            str_EmailOficina = EmailOficina
            int_AccesoInternetOficina = AccesoInternetOficina
            str_NombreIglesia = NombreIglesia
            int_AccesoInternet = AccesoInternet
            str_NumeroServicioRadioPersonal = NumeroServicioRadioPersonal
            str_ColegioEgreso = ColegioEgreso
            str_ContinuaEstudios = ContinuaEstudios

        End Sub

#End Region

    End Class

End Namespace
