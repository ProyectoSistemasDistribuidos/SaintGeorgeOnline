Namespace ModuloColegio
    'ok
    Public Class be_SedesColegio

#Region "Atributos"

        Private int_CodigoSede As Integer
        Private str_NombreSede As String
        Private int_CodigoColegio As Integer
        Private str_CodigoUbigeo As String
        Private int_CodigoPersonaResponsableMatricula As Integer
        Private int_CodigoPersonaDirectorGeneral As Integer
        Private int_CodigoPersonaDirectorNacional As Integer
        Private int_CodigoPersonaSubDirector As Integer
        Private str_Direccion As String
        Private str_NombreUgel As String
        Private str_CodigoUgel As String
        Private int_NumeroUgel As Integer
        Private str_Urbanizacion As String
        Private str_NumeroResolucion As String
        Private str_Gestion As String
        Private str_GestionAbrv As String
        Private str_Forma As String
        Private str_FormaAbrv As String
        Private str_Modalidad As String
        Private str_ModalidadAbrv As String
        Private str_Programa As String
        Private str_ProgramaAbrv As String
        Private str_Turno As String
        Private str_TurnoAbrv As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoSede() As Integer
            Get
                Return int_CodigoSede
            End Get
            Set(ByVal value As Integer)
                int_CodigoSede = value
            End Set
        End Property

        Public Property NombreSede() As String
            Get
                Return str_NombreSede
            End Get
            Set(ByVal value As String)
                str_NombreSede = value
            End Set
        End Property

        Public Property CodigoColegio() As Integer
            Get
                Return int_CodigoColegio
            End Get
            Set(ByVal value As Integer)
                int_CodigoColegio = value
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

        Public Property CodigoPersonaResponsableMatricula() As Integer
            Get
                Return int_CodigoPersonaResponsableMatricula
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaResponsableMatricula = value
            End Set
        End Property

        Public Property CodigoPersonaDirectorGeneral() As Integer
            Get
                Return int_CodigoPersonaDirectorGeneral
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaDirectorGeneral = value
            End Set
        End Property

        Public Property CodigoPersonaDirectorNacional() As Integer
            Get
                Return int_CodigoPersonaDirectorNacional
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaDirectorNacional = value
            End Set
        End Property

        Public Property CodigoPersonaSubDirector() As Integer
            Get
                Return int_CodigoPersonaSubDirector
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaSubDirector = value
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

        Public Property NombreUgel() As String
            Get
                Return str_NombreUgel
            End Get
            Set(ByVal value As String)
                str_NombreUgel = value
            End Set
        End Property

        Public Property CodigoUgel() As String
            Get
                Return str_CodigoUgel
            End Get
            Set(ByVal value As String)
                str_CodigoUgel = value
            End Set
        End Property

        Public Property NumeroUgel() As Integer
            Get
                Return int_NumeroUgel
            End Get
            Set(ByVal value As Integer)
                int_NumeroUgel = value
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

        Public Property NumeroResolucion() As String
            Get
                Return str_NumeroResolucion
            End Get
            Set(ByVal value As String)
                str_NumeroResolucion = value
            End Set
        End Property

        Public Property Gestion() As String
            Get
                Return str_Gestion
            End Get
            Set(ByVal value As String)
                str_Gestion = value
            End Set
        End Property

        Public Property GestionAbrv() As String
            Get
                Return str_GestionAbrv
            End Get
            Set(ByVal value As String)
                str_GestionAbrv = value
            End Set
        End Property


        Public Property Forma() As String
            Get
                Return str_Forma
            End Get
            Set(ByVal value As String)
                str_Forma = value
            End Set
        End Property

        Public Property FormaAbrv() As String
            Get
                Return str_FormaAbrv
            End Get
            Set(ByVal value As String)
                str_FormaAbrv = value
            End Set
        End Property

        Public Property Modalidad() As String
            Get
                Return str_Modalidad
            End Get
            Set(ByVal value As String)
                str_Modalidad = value
            End Set
        End Property

        Public Property ModalidadAbrv() As String
            Get
                Return str_ModalidadAbrv
            End Get
            Set(ByVal value As String)
                str_ModalidadAbrv = value
            End Set
        End Property

        Public Property Programa() As String
            Get
                Return str_Programa
            End Get
            Set(ByVal value As String)
                str_Programa = value
            End Set
        End Property

        Public Property ProgramaAbrv() As String
            Get
                Return str_ProgramaAbrv
            End Get
            Set(ByVal value As String)
                str_ProgramaAbrv = value
            End Set
        End Property

        Public Property Turno() As String
            Get
                Return str_Turno
            End Get
            Set(ByVal value As String)
                str_Turno = value
            End Set
        End Property

        Public Property TurnoAbrv() As String
            Get
                Return str_TurnoAbrv
            End Get
            Set(ByVal value As String)
                str_TurnoAbrv = value
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

        Sub New(ByVal CodigoSede As Integer, _
             ByVal NombreSede As String, _
             ByVal CodigoColegio As Integer, _
             ByVal CodigoUbigeo As String, _
             ByVal CodigoPersonaResponsableMatricula As Integer, _
             ByVal CodigoPersonaDirectorGeneral As Integer, _
             ByVal CodigoPersonaDirectorNacional As Integer, _
             ByVal CodigoPersonaSubDirector As Integer, _
             ByVal Direccion As String, _
             ByVal NombreUgel As String, _
             ByVal CodigoUgel As String, _
             ByVal NumeroUgel As Integer, _
             ByVal Urbanizacion As String, _
             ByVal NumeroResolucion As String, _
             ByVal Gestion As String, _
             ByVal GestionAbrv As String, _
             ByVal Forma As String, _
             ByVal FormaAbrv As String, _
             ByVal Modalidad As String, _
             ByVal ModalidadAbrv As String, _
             ByVal Programa As String, _
             ByVal ProgramaAbrv As String, _
             ByVal Turno As String, _
             ByVal TurnoAbrv As String, _
             ByVal Estado As Integer)

            int_CodigoSede = CodigoSede
            str_NombreSede = NombreSede
            int_CodigoColegio = CodigoColegio
            str_CodigoUbigeo = CodigoUbigeo
            int_CodigoPersonaResponsableMatricula = CodigoPersonaResponsableMatricula
            int_CodigoPersonaDirectorGeneral = CodigoPersonaDirectorGeneral
            int_CodigoPersonaDirectorNacional = CodigoPersonaDirectorNacional
            int_CodigoPersonaSubDirector = CodigoPersonaSubDirector
            str_Direccion = Direccion
            str_NombreUgel = NombreUgel
            str_CodigoUgel = CodigoUgel
            int_NumeroUgel = NumeroUgel
            str_Urbanizacion = Urbanizacion
            str_NumeroResolucion = NumeroResolucion
            str_GestionAbrv = GestionAbrv
            str_Forma = Forma
            str_Modalidad = Modalidad
            str_Programa = Programa
            str_Turno = Turno
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace