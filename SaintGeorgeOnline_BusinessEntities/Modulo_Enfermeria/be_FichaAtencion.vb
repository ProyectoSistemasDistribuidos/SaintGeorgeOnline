Namespace ModuloEnfermeria

    Public Class be_FichaAtencion

#Region "Atributos"

        Private int_CodigoFichaAtencion As Integer
        Private int_CodigoPersonaPaciente As Integer
        Private int_CodigoTipoPersonaPaciente As Integer
        Private int_CodigoIndicacionMedica As Integer
        Private int_CodigoSede As Integer
        Private dt_FechaAtencion As Date
        Private dt_HoraIngreso As Date
        Private dt_HoraSalida As Date
        Private str_SintomasDescripcion As String
        Private str_Observaciones As String
        Private int_DescansarEnfermeria As Integer
        Private int_CodigoTipoPersonaRecoge As Integer
        Private int_CodigoPersonaRecoje As Integer
        Private int_CodigoPersonaEnvia As Integer
        Private int_CodigoTipoProcedencia As Integer
        Private int_CodigoCurso As Integer
        Private int_CodigoNombreGrupo As Integer
        Private str_DescripcionOtros As String
        Private int_Completado As Integer
        Private int_TieneMatricula As Integer
        Private int_PermisoModificar As Integer
        Private int_Estado As Integer
        Private int_UsuarioRegistro As Integer
        Private int_CodigoCategoriaAtencion As Integer

        Private int_CodigoTipoAtencion As Integer


#End Region

#Region "Propiedades"
        Public Property CodigoTipoAtencion() As Integer
            Get
                Return int_CodigoTipoAtencion
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoAtencion = value
            End Set
        End Property



        Public Property CodigoFichaAtencion() As Integer
            Get
                Return int_CodigoFichaAtencion
            End Get
            Set(ByVal value As Integer)
                int_CodigoFichaAtencion = value
            End Set
        End Property

        Public Property CodigoPersonaPaciente() As Integer
            Get
                Return int_CodigoPersonaPaciente
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaPaciente = value
            End Set
        End Property

        Public Property CodigoTipoPersonaPaciente() As Integer
            Get
                Return int_CodigoTipoPersonaPaciente
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoPersonaPaciente = value
            End Set
        End Property

        Public Property CodigoIndicacionMedica() As Integer
            Get
                Return int_CodigoIndicacionMedica
            End Get
            Set(ByVal value As Integer)
                int_CodigoIndicacionMedica = value
            End Set
        End Property

        Public Property CodigoSede() As Integer
            Get
                Return int_CodigoSede
            End Get
            Set(ByVal value As Integer)
                int_CodigoSede = value
            End Set
        End Property

        Public Property FechaAtencion() As Date
            Get
                Return dt_FechaAtencion
            End Get
            Set(ByVal value As Date)
                dt_FechaAtencion = value
            End Set
        End Property

        Public Property HoraIngreso() As Date
            Get
                Return dt_HoraIngreso
            End Get
            Set(ByVal value As Date)
                dt_HoraIngreso = value
            End Set
        End Property

        Public Property HoraSalida() As Date
            Get
                Return dt_HoraSalida
            End Get
            Set(ByVal value As Date)
                dt_HoraSalida = value
            End Set
        End Property

        Public Property SintomasDescripcion() As String
            Get
                Return str_SintomasDescripcion
            End Get
            Set(ByVal value As String)
                str_SintomasDescripcion = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return str_Observaciones
            End Get
            Set(ByVal value As String)
                str_Observaciones = value
            End Set
        End Property

        Public Property DescansarEnfermeria() As Integer
            Get
                Return int_DescansarEnfermeria
            End Get
            Set(ByVal value As Integer)
                int_DescansarEnfermeria = value
            End Set
        End Property

        Public Property CodigoTipoPersonaRecoge() As Integer
            Get
                Return int_CodigoTipoPersonaRecoge
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoPersonaRecoge = value
            End Set
        End Property

        Public Property CodigoPersonaRecoje() As Integer
            Get
                Return int_CodigoPersonaRecoje
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaRecoje = value
            End Set
        End Property

        Public Property CodigoPersonaEnvia() As Integer
            Get
                Return int_CodigoPersonaEnvia
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaEnvia = value
            End Set
        End Property

        Public Property CodigoTipoProcedencia() As Integer
            Get
                Return int_CodigoTipoProcedencia
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoProcedencia = value
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
        Public Property CodigoNombreGrupo() As Integer
            Get
                Return int_CodigoNombreGrupo
            End Get
            Set(ByVal value As Integer)
                int_CodigoNombreGrupo = value
            End Set
        End Property
        Public Property DescripcionOtros() As String
            Get
                Return str_DescripcionOtros
            End Get
            Set(ByVal value As String)
                str_DescripcionOtros = value
            End Set
        End Property

        Public Property Completado() As Integer
            Get
                Return int_Completado
            End Get
            Set(ByVal value As Integer)
                int_Completado = value
            End Set
        End Property

        Public Property TieneMatricula() As Integer
            Get
                Return int_TieneMatricula
            End Get
            Set(ByVal value As Integer)
                int_TieneMatricula = value
            End Set
        End Property

        Public Property PermisoModificar() As Integer
            Get
                Return int_PermisoModificar
            End Get
            Set(ByVal value As Integer)
                int_PermisoModificar = value
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

        Public Property UsuarioRegistro() As Integer
            Get
                Return int_UsuarioRegistro
            End Get
            Set(ByVal value As Integer)
                int_UsuarioRegistro = value
            End Set
        End Property

        Public Property CodigoCategoriaAtencion() As Integer
            Get
                Return int_CodigoCategoriaAtencion
            End Get
            Set(ByVal value As Integer)
                int_CodigoCategoriaAtencion = value
            End Set
        End Property
#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoFichaAtencion As Integer, _
                ByVal CodigoPersonaPaciente As Integer, _
                ByVal CodigoTipoPersonaPaciente As Integer, _
                ByVal CodigoIndicacionMedica As Integer, _
                ByVal CodigoSede As Integer, _
                ByVal dt_FechaAtencion As Date, _
                ByVal dt_HoraIngreso As Date, _
                ByVal dt_HoraSalida As Date, _
                ByVal SintomasDescripcion As String, _
                ByVal Observaciones As String, _
                ByVal DescansarEnfermeria As Integer, _
                ByVal codigoTipoPersonaRecoge As Integer, _
                ByVal CodigoPersonaRecoje As Integer, _
                ByVal CodigoPersonaEnvia As Integer, _
                ByVal CodigoTipoProcedencia As Integer, _
                ByVal CodigoCurso As Integer, _
                ByVal CodigoNombreGrupo As Integer, _
                ByVal DescripcionOtros As String, _
                ByVal Completado As Integer, _
                ByVal TieneMatricula As Integer, _
                ByVal PermisoModificar As Integer, _
                ByVal Estado As Integer, _
                ByVal CodigoCategoriaAtencion As Integer)

            int_CodigoFichaAtencion = CodigoFichaAtencion
            int_CodigoPersonaPaciente = CodigoPersonaPaciente
            int_CodigoTipoPersonaPaciente = CodigoTipoPersonaPaciente
            int_CodigoIndicacionMedica = CodigoIndicacionMedica
            int_CodigoSede = CodigoSede
            dt_FechaAtencion = FechaAtencion
            dt_HoraIngreso = HoraIngreso
            dt_HoraSalida = HoraSalida
            str_SintomasDescripcion = SintomasDescripcion
            str_Observaciones = Observaciones
            int_DescansarEnfermeria = DescansarEnfermeria
            int_CodigoTipoPersonaRecoge = codigoTipoPersonaRecoge
            int_CodigoPersonaRecoje = CodigoPersonaRecoje
            int_CodigoPersonaEnvia = CodigoPersonaEnvia
            int_CodigoTipoProcedencia = CodigoTipoProcedencia
            int_CodigoCurso = CodigoCurso
            int_CodigoNombreGrupo = CodigoNombreGrupo
            str_DescripcionOtros = DescripcionOtros
            int_Completado = Completado
            int_TieneMatricula = TieneMatricula
            int_PermisoModificar = PermisoModificar
            int_Estado = Estado
            int_CodigoCategoriaAtencion = CodigoCategoriaAtencion
        End Sub

#End Region

    End Class

End Namespace

