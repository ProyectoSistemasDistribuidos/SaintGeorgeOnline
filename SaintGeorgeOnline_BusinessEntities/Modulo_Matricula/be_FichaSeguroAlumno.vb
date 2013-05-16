Namespace ModuloMatricula

    Public Class be_FichaSeguroAlumno


#Region "Atributos"

        Private int_CodigoFichaSeguro As Integer
        Private int_CodigoAnioAcademico As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoTipoSeguro As Integer
        Private int_CodigoCompaniaSeguro As Integer
        Private str_NumeroPoliza As String
        Private int_VigenciaIndefinida As Integer
        Private dt_FechaInicio As Date
        Private dt_FechaFin As Date
        Private str_CompaniaAmbulancia As String
        Private str_TelfCompaniaAmbulancia As String
        Private int_CopiaCarnetSeguro As Integer

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

        Public Property CodigoTipoSeguro() As Integer
            Get
                Return int_CodigoTipoSeguro
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoSeguro = value
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

        Public Property NumeroPoliza() As String
            Get
                Return str_NumeroPoliza
            End Get
            Set(ByVal value As String)
                str_NumeroPoliza = value
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

        Public Property CompaniaAmbulancia() As String
            Get
                Return str_CompaniaAmbulancia
            End Get
            Set(ByVal value As String)
                str_CompaniaAmbulancia = value
            End Set
        End Property

        Public Property TelfCompaniaAmbulancia() As String
            Get
                Return str_TelfCompaniaAmbulancia
            End Get
            Set(ByVal value As String)
                str_TelfCompaniaAmbulancia = value
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

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoFichaSeguro As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoTipoSeguro As Integer, _
                ByVal CodigoCompaniaSeguro As Integer, _
                ByVal NumeroPoliza As String, _
                ByVal VigenciaIndefinida As Integer, _
                ByVal FechaInicio As Date, _
                ByVal FechaFin As Date, _
                ByVal CompaniaAmbulancia As String, _
                ByVal TelfCompaniaAmbulancia As String, _
                ByVal CopiaCarnetSeguro As Integer)

            int_CodigoFichaSeguro = CodigoFichaSeguro
            int_CodigoAnioAcademico = CodigoAnioAcademico
            str_CodigoAlumno = CodigoAlumno
            int_CodigoTipoSeguro = CodigoAlumno
            int_CodigoCompaniaSeguro = CodigoCompaniaSeguro
            str_NumeroPoliza = NumeroPoliza
            int_VigenciaIndefinida = VigenciaIndefinida
            dt_FechaInicio = FechaInicio
            dt_FechaFin = FechaFin
            str_CompaniaAmbulancia = CompaniaAmbulancia
            str_TelfCompaniaAmbulancia = TelfCompaniaAmbulancia
            int_CopiaCarnetSeguro = CopiaCarnetSeguro

        End Sub

#End Region

    End Class

End Namespace