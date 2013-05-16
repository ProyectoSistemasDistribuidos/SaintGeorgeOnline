Namespace ModuloMatricula

    Public Class be_FichaSeguro

#Region "Atributos"

        Private int_CodigoFichaSeguro As Integer
        Private int_CodigoCompaniaSeguro As Integer
        Private int_CodigoTipoSeguro As Integer
        Private int_CodigoAnioAcademico As Integer
        Private str_CodigoAlumno As String
        Private str_NumeroPoliza As String
        Private dt_FechaInicio As Date
        Private dt_FechaFin As Date
        Private int_VigenciaIndefinida As Integer
        Private int_CopiaCarnetSeguro As Integer
        Private str_Compania As String
        Private str_Telefono As String
        Private str_CodigoClinica As String

#End Region

#Region "Propiedades"

        Public Property CodigoFichaSeguro() As Integer
            Get
                Return int_CodigoFichaSeguro
            End Get
            Set(ByVal value As Integer)
                int_CodigoFichaSeguro = value
            End Set
        End Property

        Public Property CodigoCompaniaSeguro() As Integer
            Get
                Return int_CodigoCompaniaSeguro
            End Get
            Set(ByVal value As Integer)
                int_CodigoCompaniaSeguro = value
            End Set
        End Property

        Public Property CodigoTipoSeguro() As Integer
            Get
                Return int_CodigoTipoSeguro
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoSeguro = value
            End Set
        End Property

        Public Property CodigoAnioAcademico() As Integer
            Get
                Return int_CodigoAnioAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoAnioAcademico = value
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

        Public Property NumeroPoliza() As String
            Get
                Return str_NumeroPoliza
            End Get
            Set(ByVal value As String)
                str_NumeroPoliza = value
            End Set
        End Property

        Public Property FechaInicio() As Date
            Get
                Return dt_FechaInicio
            End Get
            Set(ByVal value As Date)
                dt_FechaInicio = value
            End Set
        End Property

        Public Property FechaFin() As Date
            Get
                Return dt_FechaFin
            End Get
            Set(ByVal value As Date)
                dt_FechaFin = value
            End Set
        End Property

        Public Property VigenciaIndefinida() As Integer
            Get
                Return int_VigenciaIndefinida
            End Get
            Set(ByVal value As Integer)
                int_VigenciaIndefinida = value
            End Set
        End Property

        Public Property CopiaCarnetSeguro() As Integer
            Get
                Return int_CopiaCarnetSeguro
            End Get
            Set(ByVal value As Integer)
                int_CopiaCarnetSeguro = value
            End Set
        End Property


        Public Property Compania() As String
            Get
                Return str_Compania
            End Get
            Set(ByVal value As String)
                str_Compania = value
            End Set
        End Property

        Public Property Telefono() As String
            Get
                Return str_Telefono
            End Get
            Set(ByVal value As String)
                str_Telefono = value
            End Set
        End Property

        Public Property CodigoClinica() As String
            Get
                Return str_CodigoClinica
            End Get
            Set(ByVal value As String)
                str_CodigoClinica = value
            End Set
        End Property
#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoFichaSeguro As Integer, _
                ByVal CodigoCompaniaSeguro As Integer, _
                ByVal CodigoTipoSeguro As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal NumeroPoliza As Integer, _
                 ByVal FechaInicio As Date, _
                ByVal FechaFin As Date, _
                 ByVal VigenciaIndefinida As Integer, _
                ByVal CopiaCarnetSeguro As Integer, _
                 ByVal Compania As String, _
                ByVal Telefono As String, _
            ByVal CodigoClinica As Integer)
            int_CodigoFichaSeguro = CodigoFichaSeguro
            int_CodigoCompaniaSeguro = CodigoCompaniaSeguro
            int_CodigoTipoSeguro = CodigoTipoSeguro
            int_CodigoAnioAcademico = CodigoAnioAcademico
            str_CodigoAlumno = CodigoAlumno
            str_NumeroPoliza = NumeroPoliza
            dt_FechaInicio = FechaInicio
            dt_FechaFin = FechaFin
            int_VigenciaIndefinida = VigenciaIndefinida
            int_CopiaCarnetSeguro = CopiaCarnetSeguro
            str_Compania = Compania
            str_Telefono = Telefono
            str_CodigoClinica = CodigoClinica

        End Sub

#End Region

    End Class

End Namespace