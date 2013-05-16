Namespace ModuloMatricula

    Public Class be_MaestroPersonas

#Region "Atributos"

        Private int_CodigoPersona As Integer
        Private int_CodigoTipoPersona As Integer
        Private str_DescTipoPersona As String
        Private int_CodigoAlumno As Integer
        'Private int_CodigoPersonal As Integer
        Private int_CodigoTrabajador As Integer
        Private int_CodigoFamiliar As Integer
        Private int_CodigoOtros As Integer
        Private str_RutaFoto As String
        Private int_Edad As Integer
        Private int_CodigoTipoSangre As Integer
        Private str_DescTipoSangre As String
        Private str_NSnGS As String
        Private int_CodigoGrado As Integer

        Private str_Nombre As String
        Private str_ApellidoPaterno As String
        Private str_ApellidoMaterno As String
        Private str_NombreCompleto As String
        Private int_EstadoPersona As Integer

        Private int_Sede As Integer
        Private int_AlumnoNivel As Integer
        Private int_AlumnoSubnivel As Integer
        Private int_AlumnoGrado As Integer
        Private int_AlumnoAula As Integer

        Private str_AlumnoFamiliarApellidoPaterno As String
        Private str_AlumnoFamiliarApellidoMaterno As String
        Private str_AlumnoFamiliarNombres As String
        Private int_AlumnoFamiliarSede As Integer
        Private int_AlumnoFamiliarNivel As Integer
        Private int_AlumnoFamiliarSubnivel As Integer
        Private int_AlumnoFamiliarGrado As Integer
        Private int_AlumnoFamiliarAula As Integer

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

        Public Property CodigoTipoPersona() As Integer
            Get
                Return int_CodigoTipoPersona
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoPersona = value
            End Set
        End Property

        Public Property DescTipoPersona() As String
            Get
                Return str_DescTipoPersona
            End Get
            Set(ByVal value As String)
                str_DescTipoPersona = value
            End Set
        End Property

        Public Property CodigoAlumno() As Integer
            Get
                Return int_CodigoAlumno
            End Get
            Set(ByVal value As Integer)
                int_CodigoAlumno = value
            End Set
        End Property

        'Public Property CodigoPersonal() As Integer
        '    Get
        '        Return int_CodigoPersonal
        '    End Get
        '    Set(ByVal value As Integer)
        '        int_CodigoPersonal = value
        '    End Set
        'End Property

        Public Property CodigoTrabajador() As Integer
            Get
                Return int_CodigoTrabajador
            End Get
            Set(ByVal value As Integer)
                int_CodigoTrabajador = value
            End Set
        End Property

        Public Property CodigoFamiliar() As Integer
            Get
                Return int_CodigoFamiliar
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamiliar = value
            End Set
        End Property

        Public Property CodigoOtros() As Integer
            Get
                Return int_CodigoOtros
            End Get
            Set(ByVal value As Integer)
                int_CodigoOtros = value
            End Set
        End Property

        Public Property RutaFoto() As String
            Get
                Return str_RutaFoto
            End Get
            Set(ByVal value As String)
                str_RutaFoto = value
            End Set
        End Property

        Public Property Edad() As Integer
            Get
                Return int_Edad
            End Get
            Set(ByVal value As Integer)
                int_Edad = value
            End Set
        End Property

        Public Property CodigoTipoSangre() As Integer
            Get
                Return int_CodigoTipoSangre
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoSangre = value
            End Set
        End Property

        Public Property DescTipoSangre() As String
            Get
                Return str_DescTipoSangre
            End Get
            Set(ByVal value As String)
                str_DescTipoSangre = value
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

        Public Property NSnGS() As String
            Get
                Return str_NSnGS
            End Get
            Set(ByVal value As String)
                str_NSnGS = value
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

        Public Property EstadoPersona() As Integer
            Get
                Return int_EstadoPersona
            End Get
            Set(ByVal value As Integer)
                int_EstadoPersona = value
            End Set
        End Property


        Public Property Sede() As Integer
            Get
                Return int_Sede
            End Get
            Set(ByVal value As Integer)
                int_Sede = value
            End Set
        End Property

        Public Property AlumnoNivel() As Integer
            Get
                Return int_AlumnoNivel
            End Get
            Set(ByVal value As Integer)
                int_AlumnoNivel = Value
            End Set
        End Property

        Public Property AlumnoSubnivel() As Integer
            Get
                Return int_AlumnoSubnivel
            End Get
            Set(ByVal value As Integer)
                int_AlumnoSubnivel = value
            End Set
        End Property

        Public Property AlumnoGrado() As Integer
            Get
                Return int_AlumnoGrado
            End Get
            Set(ByVal value As Integer)
                int_AlumnoGrado = value
            End Set
        End Property

        Public Property AlumnoAula() As Integer
            Get
                Return int_AlumnoAula
            End Get
            Set(ByVal value As Integer)
                int_AlumnoAula = value
            End Set
        End Property


        Public Property AlumnoFamiliarApellidoPaterno() As String
            Get
                Return str_AlumnoFamiliarApellidoPaterno
            End Get
            Set(ByVal value As String)
                str_AlumnoFamiliarApellidoPaterno = value
            End Set
        End Property

        Public Property AlumnoFamiliarApellidoMaterno() As String
            Get
                Return str_AlumnoFamiliarApellidoMaterno
            End Get
            Set(ByVal value As String)
                str_AlumnoFamiliarApellidoMaterno = value
            End Set
        End Property

        Public Property AlumnoFamiliarNombres() As String
            Get
                Return str_AlumnoFamiliarNombres
            End Get
            Set(ByVal value As String)
                str_AlumnoFamiliarNombres = value
            End Set
        End Property

        Public Property AlumnoFamiliarSede() As Integer
            Get
                Return int_AlumnoFamiliarSede
            End Get
            Set(ByVal value As Integer)
                int_AlumnoFamiliarSede = value
            End Set
        End Property

        Public Property AlumnoFamiliarNivel() As Integer
            Get
                Return int_AlumnoFamiliarNivel
            End Get
            Set(ByVal value As Integer)
                int_AlumnoFamiliarNivel = value
            End Set
        End Property

        Public Property AlumnoFamiliarSubnivel() As Integer
            Get
                Return int_AlumnoFamiliarSubnivel
            End Get
            Set(ByVal value As Integer)
                int_AlumnoFamiliarSubnivel = value
            End Set
        End Property

        Public Property AlumnoFamiliarGrado() As Integer
            Get
                Return int_AlumnoFamiliarGrado
            End Get
            Set(ByVal value As Integer)
                int_AlumnoFamiliarGrado = value
            End Set
        End Property

        Public Property AlumnoFamiliarAula() As Integer
            Get
                Return int_AlumnoFamiliarAula
            End Get
            Set(ByVal value As Integer)
                int_AlumnoFamiliarAula = value
            End Set
        End Property
     
#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoPersona As Integer, _
                ByVal CodigoTipoPersona As Integer, _
                ByVal DescTipoPersona As String, _
                ByVal CodigoAlumno As Integer, _
                ByVal CodigoTrabajador As Integer, _
                ByVal CodigoFamiliar As Integer, _
                ByVal CodigoOtros As Integer, _
                ByVal RutaFoto As String, _
                ByVal Edad As Integer, _
                ByVal DescTipoSangre As String, _
                ByVal NSnGS As String, _
                ByVal CodigoGrado As Integer, _
                ByVal Nombre As String, _
                ByVal ApellidoPaterno As String, _
                ByVal ApellidoMaterno As String, _
                ByVal NombreCompleto As String, _
                ByVal EstadoPersona As Integer, _
                ByVal Sede As Integer, _
                ByVal AlumnoNivel As Integer, _
                ByVal AlumnoSubnivel As Integer, _
                ByVal AlumnoGrado As Integer, _
                ByVal AlumnoAula As Integer, _
                ByVal AlumnoFamiliarApellidoPaterno As String, _
                ByVal AlumnoFamiliarApellidoMaterno As String, _
                ByVal AlumnoFamiliarNombres As String, _
                ByVal AlumnoFamiliarSede As Integer, _
                ByVal AlumnoFamiliarNivel As Integer, _
                ByVal AlumnoFamiliarSubnivel As Integer, _
                ByVal AlumnoFamiliarGrado As Integer, _
                ByVal AlumnoFamiliarAula As Integer)

            int_CodigoPersona = CodigoPersona
            int_CodigoTipoPersona = CodigoTipoPersona
            str_DescTipoPersona = DescTipoPersona
            int_CodigoAlumno = CodigoAlumno
            'int_CodigoPersonal = CodigoPersonal
            int_CodigoTrabajador = CodigoTrabajador
            int_CodigoFamiliar = CodigoFamiliar
            int_CodigoOtros = CodigoOtros
            str_RutaFoto = RutaFoto
            int_Edad = Edad
            int_CodigoTipoSangre = CodigoTipoSangre
            str_DescTipoSangre = DescTipoSangre
            int_CodigoGrado = CodigoGrado
            str_NSnGS = NSnGS
            str_Nombre = Nombre
            str_ApellidoPaterno = ApellidoPaterno
            str_ApellidoMaterno = ApellidoMaterno
            int_EstadoPersona = EstadoPersona
            int_Sede = Sede
            int_AlumnoNivel = AlumnoSubnivel
            int_AlumnoSubnivel = AlumnoSubnivel
            int_AlumnoGrado = AlumnoGrado
            int_AlumnoAula = AlumnoAula
            str_AlumnoFamiliarApellidoPaterno = AlumnoFamiliarApellidoPaterno
            str_AlumnoFamiliarApellidoMaterno = AlumnoFamiliarApellidoMaterno
            str_AlumnoFamiliarNombres = AlumnoFamiliarNombres
            int_AlumnoFamiliarSede = AlumnoFamiliarSede
            int_AlumnoFamiliarNivel = AlumnoFamiliarNivel
            int_AlumnoFamiliarSubnivel = AlumnoFamiliarSubnivel
            int_AlumnoFamiliarGrado = AlumnoFamiliarGrado
            int_AlumnoFamiliarAula = AlumnoFamiliarAula


        End Sub

#End Region

    End Class

End Namespace