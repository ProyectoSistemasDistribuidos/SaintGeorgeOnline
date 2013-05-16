Namespace ModuloColegio

    Public Class be_AsignacionAulas

#Region "Atributos"

        Private int_Codigo As Integer
        Private int_CodigoAula As Integer
        Private int_CodigoAulaMinisterio As Integer
        Private int_CodigoAnioAcademico As Integer
        Private int_CodigoSede As Integer
        Private int_CodigoPersonaTutor As Integer
        Private int_CapacidadAlumnos As Integer
        Private int_CodigoAmbiente As Integer
        Private int_CodigoPersonaResponsableActa As Integer
        Private int_CodigoPersonaResponsableSalon As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property Codigo() As Integer
            Get
                Return int_Codigo
            End Get
            Set(ByVal value As Integer)
                int_Codigo = value
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

        Public Property CodigoAulaMinisterio() As Integer
            Get
                Return int_CodigoAulaMinisterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoAulaMinisterio = value
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

        Public Property CodigoSede() As Integer
            Get
                Return int_CodigoSede
            End Get
            Set(ByVal value As Integer)
                int_CodigoSede = value
            End Set
        End Property

        Public Property CodigoPersonaTutor() As Integer
            Get
                Return int_CodigoPersonaTutor
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaTutor = value
            End Set
        End Property

        Public Property CapacidadAlumnos() As Integer
            Get
                Return int_CapacidadAlumnos
            End Get
            Set(ByVal value As Integer)
                int_CapacidadAlumnos = value
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

        Public Property CodigoPersonaResponsableActa() As Integer
            Get
                Return int_CodigoPersonaResponsableActa
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaResponsableActa = value
            End Set
        End Property

        Public Property CodigoPersonaResponsableSalon() As Integer
            Get
                Return int_CodigoPersonaResponsableSalon
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaResponsableSalon = value
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

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal Codigo As Integer, _
                ByVal CodigoAula As Integer, _
                ByVal CodigoAulaMinisterio As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoSede As Integer, _
                ByVal CodigoPersonaTutor As Integer, _
                ByVal CapacidadAlumnos As Integer, _
                ByVal CodigoAmbiente As Integer, _
                ByVal CodigoPersonaResponsableActa As Integer, _
                ByVal CodigoPersonaResponsableSalon As Integer, _
                ByVal Estado As Integer)

            int_Codigo = Codigo
            int_CodigoAula = CodigoAula
            int_CodigoAulaMinisterio = CodigoAulaMinisterio
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_CodigoSede = CodigoSede
            int_CodigoPersonaTutor = CodigoPersonaTutor
            int_CapacidadAlumnos = CapacidadAlumnos
            int_CodigoAmbiente = CodigoAmbiente
            int_CodigoPersonaResponsableActa = CodigoPersonaResponsableActa
            int_CodigoPersonaResponsableSalon = CodigoPersonaResponsableSalon
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

