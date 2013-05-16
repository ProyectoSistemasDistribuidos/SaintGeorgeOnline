Namespace ModuloMatricula

    Public Class be_Personas

#Region "Atributos"

        Private int_CodigoPersona As Integer
        Private int_CodigoSexo As Integer
        Private int_CodigoEstadoCivil As Integer
        Private int_CodigoTipoDocIdentidad As String
        Private int_CodigoReligion As Integer
        Private str_CodigoUbigeo As String
        Private str_Nombre As String
        Private str_ApellidoPaterno As String
        Private str_ApellidoMaterno As String
        Private str_NombreCompleto As String
        Private str_FechaNacimiento As String
        Private str_NumeroDocIdentidad As String
        Private str_EmailPersonal As String
        Private str_Direccion As String
        Private str_Urbanizacion As String
        Private str_ReferenciaDomiciliaria As String
        Private str_TelefonoCasa As String
        Private str_Celular As String
        Private int_ProfesaReligion As Integer
        Private int_Estado As Integer
        Private int_CodigoNacionalidad As Integer
        Private int_CodigoRelacionPersonaNacionalidad As Integer
        Private str_DescripcionNacionalidad As String

#End Region

#Region "Propiedades"

        Public Property CodigoPersona() As Integer
            Get
                Return int_CodigoPersona
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersona = value
            End Set
        End Property

        Public Property CodigoSexo() As Integer
            Get
                Return int_CodigoSexo
            End Get
            Set(ByVal value As Integer)
                int_CodigoSexo = value
            End Set
        End Property

        Public Property CodigoEstadoCivil() As Integer
            Get
                Return int_CodigoEstadoCivil
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstadoCivil = value
            End Set
        End Property

        Public Property CodigoTipoDocIdentidad() As Integer
            Get
                Return int_CodigoTipoDocIdentidad
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoDocIdentidad = value
            End Set
        End Property

        Public Property CodigoReligion() As Integer
            Get
                Return int_CodigoReligion
            End Get
            Set(ByVal value As Integer)
                int_CodigoReligion = value
            End Set
        End Property

        Public Property CodigoUbigeo() As String
            Get
                Return str_CodigoUbigeo
            End Get
            Set(ByVal value As String)
                str_CodigoUbigeo = value
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return str_Nombre
            End Get
            Set(ByVal value As String)
                str_Nombre = value
            End Set
        End Property

        Public Property ApellidoPaterno() As String
            Get
                Return str_ApellidoPaterno
            End Get
            Set(ByVal value As String)
                str_ApellidoPaterno = value
            End Set
        End Property

        Public Property ApellidoMaterno() As String
            Get
                Return str_ApellidoMaterno
            End Get
            Set(ByVal value As String)
                str_ApellidoMaterno = value
            End Set
        End Property

        Public Property NombreCompleto() As String
            Get
                Return str_NombreCompleto
            End Get
            Set(ByVal value As String)
                str_NombreCompleto = value
            End Set
        End Property

        Public Property FechaNacimiento() As String
            Get
                Return str_FechaNacimiento
            End Get
            Set(ByVal value As String)
                str_FechaNacimiento = value
            End Set
        End Property

        Public Property NumeroDocIdentidad() As String
            Get
                Return str_NumeroDocIdentidad
            End Get
            Set(ByVal value As String)
                str_NumeroDocIdentidad = value
            End Set
        End Property

        Public Property EmailPersonal() As String
            Get
                Return str_EmailPersonal
            End Get
            Set(ByVal value As String)
                str_EmailPersonal = value
            End Set
        End Property

        Public Property Direccion() As String
            Get
                Return str_Direccion
            End Get
            Set(ByVal value As String)
                str_Direccion = value
            End Set
        End Property

        Public Property Urbanizacion() As String
            Get
                Return str_Urbanizacion
            End Get
            Set(ByVal value As String)
                str_Urbanizacion = value
            End Set
        End Property

        Public Property ReferenciaDomiciliaria() As String
            Get
                Return str_ReferenciaDomiciliaria
            End Get
            Set(ByVal value As String)
                str_ReferenciaDomiciliaria = value
            End Set
        End Property

        Public Property TelefonoCasa() As String
            Get
                Return str_TelefonoCasa
            End Get
            Set(ByVal value As String)
                str_TelefonoCasa = value
            End Set
        End Property

        Public Property Celular() As String
            Get
                Return str_Celular
            End Get
            Set(ByVal value As String)
                str_Celular = value
            End Set
        End Property

        Public Property ProfesaReligion() As Integer
            Get
                Return int_ProfesaReligion
            End Get
            Set(ByVal value As Integer)
                int_ProfesaReligion = value
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


        Public Property CodigoNacionalidad() As Integer
            Get
                Return int_CodigoNacionalidad
            End Get
            Set(ByVal value As Integer)
                int_CodigoNacionalidad = value
            End Set
        End Property

        Public Property CodigoRelacionPersonaNacionalidad() As Integer
            Get
                Return int_CodigoRelacionPersonaNacionalidad
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelacionPersonaNacionalidad = value
            End Set
        End Property

        Public Property DescripcionNacionalidad() As String
            Get
                Return str_DescripcionNacionalidad
            End Get
            Set(ByVal value As String)
                str_DescripcionNacionalidad = value
            End Set
        End Property


#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoPersona As Integer, _
                 ByVal CodigoSexo As Integer, _
                 ByVal CodigoEstadoCivil As Integer, _
                 ByVal CodigoTipoDocIdentidad As String, _
                 ByVal CodigoReligion As Integer, _
                 ByVal CodigoUbigeo As String, _
                 ByVal Nombre As String, _
                 ByVal ApellidoPaterno As String, _
                 ByVal ApellidoMaterno As String, _
                 ByVal NombreCompleto As String, _
                 ByVal FechaNacimiento As String, _
                 ByVal NumeroDocIdentidad As String, _
                 ByVal EmailPersonal As String, _
                 ByVal Direccion As String, _
                 ByVal Urbanizacion As String, _
                 ByVal ReferenciaDomiciliaria As String, _
                 ByVal TelefonoCasa As String, _
                 ByVal Celular As String, _
                 ByVal ProfesaReligion As Integer, _
                 ByVal Estado As Integer, _
                 ByVal CodigoNacionalidad As Integer, _
                 ByVal CodigoRelacionPersonaNacionalidad As Integer, _
                 ByVal DescripcionNacionalidad As String)

            int_CodigoPersona = CodigoPersona
            int_CodigoSexo = CodigoSexo
            int_CodigoEstadoCivil = CodigoEstadoCivil
            int_CodigoTipoDocIdentidad = CodigoTipoDocIdentidad
            int_CodigoReligion = CodigoReligion
            str_CodigoUbigeo = CodigoUbigeo
            str_Nombre = Nombre
            str_ApellidoPaterno = ApellidoPaterno
            str_ApellidoMaterno = ApellidoMaterno
            str_NombreCompleto = NombreCompleto
            str_FechaNacimiento = FechaNacimiento
            str_NumeroDocIdentidad = NumeroDocIdentidad
            str_EmailPersonal = EmailPersonal
            str_Direccion = Direccion
            str_Urbanizacion = Urbanizacion
            str_ReferenciaDomiciliaria = ReferenciaDomiciliaria
            str_TelefonoCasa = TelefonoCasa
            str_Celular = Celular
            int_ProfesaReligion = ProfesaReligion
            int_Estado = Estado
            int_CodigoNacionalidad = CodigoNacionalidad
            int_CodigoRelacionPersonaNacionalidad = CodigoRelacionPersonaNacionalidad
            str_DescripcionNacionalidad = DescripcionNacionalidad

        End Sub

#End Region

    End Class

End Namespace

