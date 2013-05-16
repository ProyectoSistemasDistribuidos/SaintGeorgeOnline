Namespace ModuloAsistenciaAlumnos

    Public Class be_AsistenciaAlumnos

#Region "Atributos"
        'ok
        Private int_CodigoRegistroAsistencia As Integer
        Private dt_FechaAsistencia As Date
        Private dt_FechaJustificacion As Date
        Private str_CodigoAlumno As String
        Private int_CodigoEventoAsistencia As Integer
        Private int_CodigoAula As Integer
        Private int_Estado As Integer
        Private str_ObservacionTutor As String
        Private int_CodigoMedioUso As Integer
        Private int_CodigoMotivo As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoRegistroAsistencia() As Integer
            Get
                Return int_CodigoRegistroAsistencia
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroAsistencia = value
            End Set
        End Property

        Public Property FechaAsistencia() As Date
            Get
                Return dt_FechaAsistencia
            End Get
            Set(ByVal value As Date)
                dt_FechaAsistencia = value
            End Set
        End Property

        Public Property FechaJustificacion() As Date
            Get
                Return dt_FechaJustificacion
            End Get
            Set(ByVal value As Date)
                dt_FechaJustificacion = value
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

        Public Property CodigoEventoAsistencia() As Integer
            Get
                Return int_CodigoEventoAsistencia
            End Get
            Set(ByVal value As Integer)
                int_CodigoEventoAsistencia = value
            End Set
        End Property

        Public Property CodigoAula() As Integer
            Get
                Return int_CodigoAula
            End Get
            Set(ByVal value As Integer)
                int_CodigoAula = value
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

        Public Property ObservacionTutor() As String
            Get
                Return str_ObservacionTutor
            End Get
            Set(ByVal value As String)
                str_ObservacionTutor = value
            End Set
        End Property

        Public Property CodigoMedioUso() As Integer
            Get
                Return int_CodigoMedioUso
            End Get
            Set(ByVal value As Integer)
                int_CodigoMedioUso = value
            End Set
        End Property

        Public Property CodigoMotivo() As Integer
            Get
                Return int_CodigoMotivo
            End Get
            Set(ByVal value As Integer)
                int_CodigoMotivo = value
            End Set
        End Property
#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoRegistroAsistencia As Integer, _
                ByVal FechaAsistencia As Date, _
                ByVal FechaJustificacion As Date, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoEventoAsistencia As Integer, _
                ByVal CodigoAula As Integer, _
                ByVal Estado As Integer, _
                ByVal ObservacionTutor As String, _
                ByVal CodigoMedioUso As Integer, _
                ByVal CodigoMotivo As Integer)

            int_CodigoRegistroAsistencia = CodigoRegistroAsistencia
            dt_FechaAsistencia = FechaAsistencia
            dt_FechaJustificacion = FechaJustificacion
            str_CodigoAlumno = CodigoAlumno
            int_CodigoEventoAsistencia = CodigoEventoAsistencia
            int_CodigoAula = CodigoAula
            int_Estado = Estado
            str_ObservacionTutor = ObservacionTutor
            int_CodigoMedioUso = CodigoMedioUso
            int_CodigoMotivo = CodigoMotivo

        End Sub

#End Region

    End Class
End Namespace


