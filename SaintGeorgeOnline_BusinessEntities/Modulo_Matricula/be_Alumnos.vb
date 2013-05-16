Namespace ModuloMatricula

    Public Class be_Alumnos
        Inherits be_Personas

        'Actualizado 16/02/2012

#Region "Atributos"

        Private str_CodigoAlumno As String
        Private int_CodigoPersona As Integer
        Private int_CodigoHouse As Integer
        Private int_CodigoPais As Integer
        Private str_CodigoEducando As String
        Private int_CantidadHermanos As Integer
        Private int_PosicionEntreHermanos As Integer
        Private str_CorreoInstitucional As String
        Private int_Bautizo As Integer
        Private str_BautizoLugar As String
        Private int_BautizoAnio As Integer
        Private int_PrimeraComunion As Integer
        Private str_PrimeraComunionLugar As String
        Private int_PrimeraComunionAnio As Integer
        Private int_Confirmacion As Integer
        Private str_ConfirmacionLugar As String
        Private int_ConfirmacionAnio As Integer
        Private int_NacimientoRegistrado As Integer
        Private int_EstadoActualAlumno As Integer
        Private int_AnioActualAlumno As Integer
        Private str_RutaFoto As String
        Private str_Usuario As String
        Private str_Contrasenia As String
        Private int_EstadoAcceso As Integer
        Private int_AccesoInternet As Integer
        Private str_ExperienciasTraumaticasDescripcion As String
        Private str_NombreContactoAvisoEmergencia As String
        Private str_TelfCasaContactoAvisoEmergencia As String
        Private str_CellContactoAvisoEmergencia As String
        Private str_TelfOficinaContactoAvisoEmergencia As String
        Private int_SubNivelActual As Integer
        Private int_GradoActual As Integer
        Private int_AulaActual As Integer
        Private int_LastSession As Integer
        Private str_CodigoNacimientoUbigeo As String
        Private int_CodigoRelacionIdiomasPersonas1 As Integer
        Private int_CodigoRelacionIdiomasPersonas2 As Integer
        Private int_CodigoIdioma1 As Integer
        Private int_CodigoIdioma2 As Integer
        Private int_CodigoRelacionNacionalidadesPersonas1 As Integer
        Private int_CodigoRelacionNacionalidadesPersonas2 As Integer
        Private int_CodigoNacionalidades1 As Integer
        Private int_CodigoNacionalidades2 As Integer

        Private int_EmitirFactura As Integer
        Private str_FacturaRazonSocial As String
        Private str_FacturaRUC As String
        Private str_FacturaDireccionEmpresa As String
        Private int_CodigoEmpresa As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoIdioma1() As Integer
            Get
                Return int_CodigoIdioma1
            End Get
            Set(ByVal value As Integer)
                int_CodigoIdioma1 = value
            End Set
        End Property

        Public Property CodigoIdioma2() As Integer
            Get
                Return int_CodigoIdioma2
            End Get
            Set(ByVal value As Integer)
                int_CodigoIdioma2 = value
            End Set
        End Property

        Public Property CodigoRelacionNacionalidadesPersonas1() As Integer
            Get
                Return int_CodigoRelacionNacionalidadesPersonas1
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelacionNacionalidadesPersonas1 = value
            End Set
        End Property

        Public Property CodigoRelacionNacionalidadesPersonas2() As Integer
            Get
                Return int_CodigoRelacionNacionalidadesPersonas2
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelacionNacionalidadesPersonas2 = value
            End Set
        End Property

        Public Property CodigoNacionalidades1() As Integer
            Get
                Return int_CodigoNacionalidades1
            End Get
            Set(ByVal value As Integer)
                int_CodigoNacionalidades1 = value
            End Set
        End Property

        Public Property CodigoNacionalidades2() As Integer
            Get
                Return int_CodigoNacionalidades2
            End Get
            Set(ByVal value As Integer)
                int_CodigoNacionalidades2 = value
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

        Public Property _CodigoPersona() As Integer
            Get
                Return int_CodigoPersona
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersona = value
            End Set
        End Property

        Public Property CodigoHouse() As Integer
            Get
                Return int_CodigoHouse
            End Get
            Set(ByVal value As Integer)
                int_CodigoHouse = value
            End Set
        End Property

        Public Property CodigoPais() As Integer
            Get
                Return int_CodigoPais
            End Get
            Set(ByVal value As Integer)
                int_CodigoPais = value
            End Set
        End Property

        Public Property CodigoEducando() As String
            Get
                Return str_CodigoEducando
            End Get
            Set(ByVal value As String)
                str_CodigoEducando = value
            End Set
        End Property


        Public Property CantidadHermanos() As Integer
            Get
                Return int_CantidadHermanos
            End Get
            Set(ByVal value As Integer)
                int_CantidadHermanos = value
            End Set
        End Property

        Public Property PosicionEntreHermanos() As Integer
            Get
                Return int_PosicionEntreHermanos
            End Get
            Set(ByVal value As Integer)
                int_PosicionEntreHermanos = value
            End Set
        End Property

        Public Property CorreoInstitucional() As String
            Get
                Return str_CorreoInstitucional
            End Get
            Set(ByVal value As String)
                str_CorreoInstitucional = value
            End Set
        End Property

        Public Property Bautizo() As Integer
            Get
                Return int_Bautizo
            End Get
            Set(ByVal value As Integer)
                int_Bautizo = value
            End Set
        End Property

        Public Property BautizoLugar() As String
            Get
                Return str_BautizoLugar
            End Get
            Set(ByVal value As String)
                str_BautizoLugar = value
            End Set
        End Property

        Public Property BautizoAnio() As Integer
            Get
                Return int_BautizoAnio
            End Get
            Set(ByVal value As Integer)
                int_BautizoAnio = value
            End Set
        End Property

        Public Property PrimeraComunion() As Integer
            Get
                Return int_PrimeraComunion
            End Get
            Set(ByVal value As Integer)
                int_PrimeraComunion = value
            End Set
        End Property

        Public Property PrimeraComunionLugar() As String
            Get
                Return str_PrimeraComunionLugar
            End Get
            Set(ByVal value As String)
                str_PrimeraComunionLugar = value
            End Set
        End Property

        Public Property PrimeraComunionAnio() As Integer
            Get
                Return int_PrimeraComunionAnio
            End Get
            Set(ByVal value As Integer)
                int_PrimeraComunionAnio = value
            End Set
        End Property

        Public Property Confirmacion() As Integer
            Get
                Return int_Confirmacion
            End Get
            Set(ByVal value As Integer)
                int_Confirmacion = value
            End Set
        End Property

        Public Property ConfirmacionLugar() As String
            Get
                Return str_ConfirmacionLugar
            End Get
            Set(ByVal value As String)
                str_ConfirmacionLugar = value
            End Set
        End Property

        Public Property ConfirmacionAnio() As Integer
            Get
                Return int_ConfirmacionAnio
            End Get
            Set(ByVal value As Integer)
                int_ConfirmacionAnio = value
            End Set
        End Property

        Public Property CodigoNacimientoUbigeo() As String
            Get
                Return str_CodigoNacimientoUbigeo
            End Get
            Set(ByVal value As String)
                str_CodigoNacimientoUbigeo = value
            End Set
        End Property

        Public Property NacimientoRegistrado() As Integer
            Get
                Return int_NacimientoRegistrado
            End Get
            Set(ByVal value As Integer)
                int_NacimientoRegistrado = value
            End Set
        End Property

        Public Property EstadoActualAlumno() As Integer
            Get
                Return int_EstadoActualAlumno
            End Get
            Set(ByVal value As Integer)
                int_EstadoActualAlumno = value
            End Set
        End Property

        Public Property AnioActualAlumno() As Integer
            Get
                Return int_AnioActualAlumno
            End Get
            Set(ByVal value As Integer)
                int_AnioActualAlumno = value
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

        Public Property Usuario() As String
            Get
                Return str_Usuario
            End Get
            Set(ByVal value As String)
                str_Usuario = value
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

        Public Property AccesoInternet() As Integer
            Get
                Return int_AccesoInternet
            End Get
            Set(ByVal value As Integer)
                int_AccesoInternet = value
            End Set
        End Property

        Public Property ExperienciasTraumaticasDescripcion() As String
            Get
                Return str_ExperienciasTraumaticasDescripcion
            End Get
            Set(ByVal value As String)
                str_ExperienciasTraumaticasDescripcion = value
            End Set
        End Property

        Public Property NombreContactoAvisoEmergencia() As String
            Get
                Return str_NombreContactoAvisoEmergencia
            End Get
            Set(ByVal value As String)
                str_NombreContactoAvisoEmergencia = value
            End Set
        End Property

        Public Property TelfCasaContactoAvisoEmergencia() As String
            Get
                Return str_TelfCasaContactoAvisoEmergencia
            End Get
            Set(ByVal value As String)
                str_TelfCasaContactoAvisoEmergencia = value
            End Set
        End Property

        Public Property CellContactoAvisoEmergencia() As String
            Get
                Return str_CellContactoAvisoEmergencia
            End Get
            Set(ByVal value As String)
                str_CellContactoAvisoEmergencia = value
            End Set
        End Property

        Public Property TelfOficinaContactoAvisoEmergencia() As String
            Get
                Return str_TelfOficinaContactoAvisoEmergencia
            End Get
            Set(ByVal value As String)
                str_TelfOficinaContactoAvisoEmergencia = value
            End Set
        End Property

        Public Property SubNivelActual() As Integer
            Get
                Return int_SubNivelActual
            End Get
            Set(ByVal value As Integer)
                int_SubNivelActual = value
            End Set
        End Property

        Public Property GradoActual() As Integer
            Get
                Return int_GradoActual
            End Get
            Set(ByVal value As Integer)
                int_GradoActual = value
            End Set
        End Property

        Public Property AulaActual() As Integer
            Get
                Return int_AulaActual
            End Get
            Set(ByVal value As Integer)
                int_AulaActual = value
            End Set
        End Property

        Public Property LastSession() As Integer
            Get
                Return int_LastSession
            End Get
            Set(ByVal value As Integer)
                int_LastSession = value
            End Set
        End Property

        Public Property CodigoRelacionIdiomasPersonas1() As Integer
            Get
                Return int_CodigoRelacionIdiomasPersonas1
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelacionIdiomasPersonas1 = value
            End Set
        End Property

        Public Property CodigoRelacionIdiomasPersonas2() As Integer
            Get
                Return int_CodigoRelacionIdiomasPersonas2
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelacionIdiomasPersonas2 = value
            End Set
        End Property

        'Public Property EstadoAlumno() As Integer
        '    Get
        '        Return int_EstadoAlumno
        '    End Get
        '    Set(ByVal value As Integer)
        '        int_EstadoAlumno = value
        '    End Set
        'End Property

        Public Property EmitirFactura() As Integer
            Get
                Return int_EmitirFactura
            End Get
            Set(ByVal value As Integer)
                int_EmitirFactura = value
            End Set
        End Property

        Public Property FacturaRazonSocial() As String
            Get
                Return str_FacturaRazonSocial
            End Get
            Set(ByVal value As String)
                str_FacturaRazonSocial = value
            End Set
        End Property

        Public Property FacturaRUC() As String
            Get
                Return str_FacturaRUC
            End Get
            Set(ByVal value As String)
                str_FacturaRUC = value
            End Set
        End Property

        Public Property FacturaDireccionEmpresa() As String
            Get
                Return str_FacturaDireccionEmpresa
            End Get
            Set(ByVal value As String)
                str_FacturaDireccionEmpresa = value
            End Set
        End Property

        Public Property CodigoEmpresa() As Integer
            Get
                Return int_CodigoEmpresa
            End Get
            Set(ByVal value As Integer)
                int_CodigoEmpresa = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoAlumno As String, _
                ByVal _CodigoPersona As Integer, _
                ByVal CodigoHouse As Integer, _
                ByVal CodigoPais As Integer, _
                ByVal CodigoEducando As String, _
                ByVal CantidadHermanos As Integer, _
                ByVal PosicionEntreHermanos As Integer, _
                ByVal CorreoInstitucional As String, _
                ByVal Bautizo As Integer, _
                ByVal BautizoLugar As String, _
                ByVal BautizoAnio As Integer, _
                ByVal PrimeraComunion As Integer, _
                ByVal PrimeraComunionLugar As String, _
                ByVal PrimeraComunionAnio As Integer, _
                ByVal Confirmacion As Integer, _
                ByVal ConfirmacionLugar As Integer, _
                ByVal ConfirmacionAnio As Integer, _
                ByVal NacimientoRegistrado As Integer, _
                ByVal EstadoActualAlumno As Integer, _
                ByVal AnioActualAlumno As Integer, _
                ByVal RutaFoto As String, _
                ByVal Usuario As String, _
                ByVal Contrasenia As String, _
                ByVal EstadoAcceso As Integer, _
                ByVal AccesoInternet As Integer, _
                ByVal ExperienciasTraumaticasDescripcion As String, _
                ByVal NombreContactoAvisoEmergencia As String, _
                ByVal TelfCasaContactoAvisoEmergencia As String, _
                ByVal CellContactoAvisoEmergencia As String, _
                ByVal TelfOficinaContactoAvisoEmergencia As String, _
                ByVal SubNivelActual As Integer, _
                ByVal GradoActual As Integer, _
                ByVal AulaActual As Integer, _
                ByVal LastSession As Integer, _
                ByVal CodigoRelacionIdiomasPersonas1 As Integer, _
                ByVal CodigoRelacionIdiomasPersonas2 As Integer, _
                ByVal CodigoIdioma1 As Integer, _
                ByVal CodigoIdioma2 As Integer, _
                ByVal CodigoRelacionNacionalidadesPersonas1 As Integer, _
                ByVal CodigoRelacionNacionalidadesPersonas2 As Integer, _
                ByVal CodigoNacionalidades1 As Integer, _
                ByVal CodigoNacionalidades2 As Integer, _
                ByVal EmitirFactura As Integer, _
                ByVal FacturaRazonSocial As String, _
                ByVal FacturaRUC As String, _
                ByVal FacturaDireccionEmpresa As String, _
                ByVal CodigoEmpresa As Integer)
            'ByVal EstadoAlumno As Integer _

            str_CodigoAlumno = CodigoAlumno
            int_CodigoPersona = _CodigoPersona
            int_CodigoHouse = CodigoHouse
            int_CodigoPais = CodigoPais
            str_CodigoEducando = CodigoEducando
            int_CantidadHermanos = CantidadHermanos
            int_PosicionEntreHermanos = PosicionEntreHermanos
            str_CorreoInstitucional = CorreoInstitucional
            int_Bautizo = Bautizo
            str_BautizoLugar = BautizoLugar
            int_BautizoAnio = BautizoAnio
            int_PrimeraComunion = PrimeraComunion
            str_PrimeraComunionLugar = PrimeraComunionLugar
            int_PrimeraComunionAnio = PrimeraComunionAnio
            int_Confirmacion = Confirmacion
            str_ConfirmacionLugar = ConfirmacionLugar
            int_ConfirmacionAnio = ConfirmacionAnio
            int_NacimientoRegistrado = NacimientoRegistrado
            int_EstadoActualAlumno = EstadoActualAlumno
            int_AnioActualAlumno = AnioActualAlumno
            str_RutaFoto = RutaFoto
            str_Usuario = Usuario
            str_Contrasenia = Contrasenia
            int_EstadoAcceso = EstadoAcceso
            int_AccesoInternet = AccesoInternet
            str_ExperienciasTraumaticasDescripcion = ExperienciasTraumaticasDescripcion
            str_NombreContactoAvisoEmergencia = NombreContactoAvisoEmergencia
            str_TelfCasaContactoAvisoEmergencia = TelfCasaContactoAvisoEmergencia
            str_CellContactoAvisoEmergencia = CellContactoAvisoEmergencia
            str_TelfOficinaContactoAvisoEmergencia = TelfOficinaContactoAvisoEmergencia
            int_SubNivelActual = SubNivelActual
            int_GradoActual = GradoActual
            int_AulaActual = AulaActual
            int_LastSession = LastSession
            int_CodigoRelacionIdiomasPersonas1 = CodigoRelacionIdiomasPersonas1
            int_CodigoRelacionIdiomasPersonas2 = CodigoRelacionIdiomasPersonas2
            int_CodigoIdioma1 = CodigoIdioma1
            int_CodigoIdioma2 = CodigoIdioma2
            int_CodigoRelacionNacionalidadesPersonas1 = CodigoRelacionNacionalidadesPersonas1
            int_CodigoRelacionNacionalidadesPersonas2 = CodigoRelacionNacionalidadesPersonas2
            int_CodigoNacionalidades1 = int_CodigoNacionalidades1
            int_CodigoNacionalidades2 = int_CodigoNacionalidades2

            int_EmitirFactura = EmitirFactura
            str_FacturaRazonSocial = FacturaRazonSocial
            str_FacturaRUC = FacturaRUC
            str_FacturaDireccionEmpresa = FacturaDireccionEmpresa
            'int_EstadoAlumno = Estado      

            int_CodigoEmpresa = CodigoEmpresa

        End Sub

#End Region

    End Class

End Namespace
