Namespace ModuloEntrevistas

    Public Class be_ProgramacionEntrevistaCabecera

#Region "Atributos"

        Private int_CodigoProgramacionEntrevistaCabecera As Integer
        Private int_CodigoTipoSolicitanteEntrevista As Integer
        Private int_CodigoMedioSolicitudEntrevista As Integer
        Private int_CodigoEstadoProgramacionEntrevista As Integer

        Private dt_FechaEntrevista As Date
        Private dt_HoraInicio As Date
        Private dt_HoraFin As Date
        Private str_Motivo As String

        Private int_CodigoFamilia As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoTrabajadorEntrevistador As Integer

        Private bool_Asistencia As Boolean
        Private dt_HoraAsistencia As Date

        Private int_CodigoPeriodo As Integer
        Private int_CodigoAmbiente As Integer

        Public RPE_CodigoProgramacionEntrevista As Integer

        Private str_Comentario As String
        Private str_AspectosTratados As String

#End Region

#Region "Propiedades"

        Public Property CodigoProgramacionEntrevistaCabecera() As Integer
            Get
                Return int_CodigoProgramacionEntrevistaCabecera
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionEntrevistaCabecera = value
            End Set
        End Property
        Public Property CodigoTipoSolicitanteEntrevista() As Integer
            Get
                Return int_CodigoTipoSolicitanteEntrevista
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoSolicitanteEntrevista = value
            End Set
        End Property
        Public Property CodigoMedioSolicitudEntrevista() As Integer
            Get
                Return int_CodigoMedioSolicitudEntrevista
            End Get
            Set(ByVal value As Integer)
                int_CodigoMedioSolicitudEntrevista = value
            End Set
        End Property
        Public Property CodigoEstadoProgramacionEntrevista() As Integer
            Get
                Return int_CodigoEstadoProgramacionEntrevista
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstadoProgramacionEntrevista = value
            End Set
        End Property

        Public Property FechaEntrevista() As Date
            Get
                Return dt_FechaEntrevista
            End Get
            Set(ByVal value As Date)
                dt_FechaEntrevista = value
            End Set
        End Property
        Public Property HoraInicio() As Date
            Get
                Return dt_HoraInicio
            End Get
            Set(ByVal value As Date)
                dt_HoraInicio = value
            End Set
        End Property
        Public Property HoraFin() As Date
            Get
                Return dt_HoraFin
            End Get
            Set(ByVal value As Date)
                dt_HoraFin = value
            End Set
        End Property
        Public Property Motivo() As String
            Get
                Return str_Motivo
            End Get
            Set(ByVal value As String)
                str_Motivo = value
            End Set
        End Property

        Public Property CodigoFamilia() As Integer
            Get
                Return int_CodigoFamilia
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamilia = value
            End Set
        End Property
        Public Property CodigoAlumno() As String
            Get
                Return str_CodigoAlumno
            End Get
            Set(ByVal value As String)
                str_CodigoAlumno = value
            End Set
        End Property
        Public Property CodigoTrabajadorEntrevistador() As Integer
            Get
                Return int_CodigoTrabajadorEntrevistador
            End Get
            Set(ByVal value As Integer)
                int_CodigoTrabajadorEntrevistador = value
            End Set
        End Property

        Public Property Asistencia() As Boolean
            Get
                Return bool_Asistencia
            End Get
            Set(ByVal value As Boolean)
                bool_Asistencia = value
            End Set
        End Property
        Public Property HoraAsistencia() As Date
            Get
                Return dt_HoraAsistencia
            End Get
            Set(ByVal value As Date)
                dt_HoraAsistencia = value
            End Set
        End Property

        Public Property CodigoPeriodo() As Integer
            Get
                Return int_CodigoPeriodo
            End Get
            Set(ByVal value As Integer)
                int_CodigoPeriodo = value
            End Set
        End Property
        Public Property CodigoAmbiente() As Integer
            Get
                Return int_CodigoAmbiente
            End Get
            Set(ByVal value As Integer)
                int_CodigoAmbiente = value
            End Set
        End Property
        Public Property Comentario() As String
            Get
                Return str_Comentario
            End Get
            Set(ByVal value As String)
                str_Comentario = value
            End Set
        End Property
        Public Property AspectosTratados() As String
            Get
                Return str_AspectosTratados
            End Get
            Set(ByVal value As String)
                str_AspectosTratados = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoProgramacionEntrevistaCabecera As Integer, _
                ByVal CodigoTipoSolicitanteEntrevista As Integer, _
                ByVal CodigoMedioSolicitudEntrevista As Integer, _
                ByVal CodigoEstadoProgramacionEntrevista As Integer, _
                ByVal FechaEntrevista As Date, _
                ByVal HoraInicio As Date, _
                ByVal HoraFin As Date, _
                ByVal Motivo As String, _
                ByVal CodigoFamilia As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoTrabajadorEntrevistador As Integer, _
                ByVal Asistencia As Boolean, _
                ByVal HoraAsistencia As Date, _
                ByVal CodigoPeriodo As Integer, _
                ByVal CodigoAmbiente As Integer, _
                ByVal Comentario As String, _
                ByVal AspectosTratados As String)

            int_CodigoProgramacionEntrevistaCabecera = CodigoProgramacionEntrevistaCabecera
            int_CodigoTipoSolicitanteEntrevista = CodigoTipoSolicitanteEntrevista
            int_CodigoMedioSolicitudEntrevista = CodigoMedioSolicitudEntrevista
            int_CodigoEstadoProgramacionEntrevista = CodigoEstadoProgramacionEntrevista

            dt_FechaEntrevista = FechaEntrevista
            dt_HoraInicio = HoraInicio
            dt_HoraFin = HoraFin
            str_Motivo = Motivo

            int_CodigoFamilia = CodigoFamilia
            str_CodigoAlumno = CodigoAlumno
            int_CodigoTrabajadorEntrevistador = CodigoTrabajadorEntrevistador
            bool_Asistencia = Asistencia
            dt_HoraAsistencia = HoraAsistencia

            int_CodigoPeriodo = CodigoPeriodo
            int_CodigoAmbiente = CodigoAmbiente

            str_Comentario = Comentario
            str_AspectosTratados = AspectosTratados

        End Sub

#End Region

    End Class

End Namespace


