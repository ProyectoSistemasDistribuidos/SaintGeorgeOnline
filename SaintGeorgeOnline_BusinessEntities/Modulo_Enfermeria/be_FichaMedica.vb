Namespace ModuloEnfermeria

    Public Class be_FichaMedica

#Region "Atributos"
        Private str_CodigoAlumno As String
        Private int_CodigoTipoNacimiento As Integer
        Private int_CodigoTipoSangre As Integer
        Private str_TipoNacimientoObservaciones As String
        Private int_EdadLevantoCabeza As Integer
        Private int_MesesLevantoCabeza As Integer
        Private int_EdadSento As Integer
        Private int_MesesSento As Integer
        Private int_EdadParo As Integer
        Private int_MesesParo As Integer
        Private int_EdadCamino As Integer
        Private int_MesesCamino As Integer
        Private int_EdadControloEsfinteres As Integer
        Private int_MesesControloEsfinteres As Integer
        Private int_EdadHabloPrimerasPalabras As Integer
        Private int_MesesHabloPrimerasPalabras As Integer
        Private int_EdadHabloFluidez As Integer
        Private int_MesesHabloFluidez As Integer
        Private int_TabiqueDesviado As Integer
        Private int_SangradoNasal As Integer
        Private int_UsaLentes As Integer
        Private str_ObservacionesOftalmologicas As String
        Private int_UsaOrtodoncia As Integer
        Private str_ObservacionesDental As String

#End Region

#Region "Propiedades"

        Public Property CodigoAlumno() As String
            Get
                Return str_CodigoAlumno
            End Get
            Set(ByVal value As String)
                str_CodigoAlumno = value
            End Set
        End Property

        Public Property CodigoTipoNacimiento() As Integer
            Get
                Return int_CodigoTipoNacimiento
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoNacimiento = value
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

        Public Property TipoNacimientoObservaciones() As String
            Get
                Return str_TipoNacimientoObservaciones
            End Get
            Set(ByVal value As String)
                str_TipoNacimientoObservaciones = value
            End Set
        End Property

        Public Property EdadLevantoCabeza() As Integer
            Get
                Return int_EdadLevantoCabeza
            End Get
            Set(ByVal value As Integer)
                int_EdadLevantoCabeza = value
            End Set
        End Property

        Public Property MesesLevantoCabeza() As Integer
            Get
                Return int_MesesLevantoCabeza
            End Get
            Set(ByVal value As Integer)
                int_MesesLevantoCabeza = value
            End Set
        End Property

        Public Property EdadSento() As Integer
            Get
                Return int_EdadSento
            End Get
            Set(ByVal value As Integer)
                int_EdadSento = value
            End Set
        End Property

        Public Property MesesSento() As Integer
            Get
                Return int_MesesSento
            End Get
            Set(ByVal value As Integer)
                int_MesesSento = value
            End Set
        End Property

        Public Property EdadParo() As Integer
            Get
                Return int_EdadParo
            End Get
            Set(ByVal value As Integer)
                int_EdadParo = value
            End Set
        End Property

        Public Property MesesParo() As Integer
            Get
                Return int_MesesParo
            End Get
            Set(ByVal value As Integer)
                int_MesesParo = value
            End Set
        End Property

        Public Property EdadCamino() As Integer
            Get
                Return int_EdadCamino
            End Get
            Set(ByVal value As Integer)
                int_EdadCamino = value
            End Set
        End Property

        Public Property MesesCamino() As Integer
            Get
                Return int_MesesCamino
            End Get
            Set(ByVal value As Integer)
                int_MesesCamino = value
            End Set
        End Property

        Public Property EdadControloEsfinteres() As Integer
            Get
                Return int_EdadControloEsfinteres
            End Get
            Set(ByVal value As Integer)
                int_EdadControloEsfinteres = value
            End Set
        End Property

        Public Property MesesControloEsfinteres() As Integer
            Get
                Return int_MesesControloEsfinteres
            End Get
            Set(ByVal value As Integer)
                int_MesesControloEsfinteres = value
            End Set
        End Property

        Public Property EdadHabloPrimerasPalabras() As Integer
            Get
                Return int_EdadHabloPrimerasPalabras
            End Get
            Set(ByVal value As Integer)
                int_EdadHabloPrimerasPalabras = value
            End Set
        End Property

        Public Property MesesHabloPrimerasPalabras() As Integer
            Get
                Return int_MesesHabloPrimerasPalabras
            End Get
            Set(ByVal value As Integer)
                int_MesesHabloPrimerasPalabras = value
            End Set
        End Property

        Public Property EdadHabloFluidez() As Integer
            Get
                Return int_EdadHabloFluidez
            End Get
            Set(ByVal value As Integer)
                int_EdadHabloFluidez = value
            End Set
        End Property

        Public Property MesesHabloFluidez() As Integer
            Get
                Return int_MesesHabloFluidez
            End Get
            Set(ByVal value As Integer)
                int_MesesHabloFluidez = value
            End Set
        End Property

        Public Property TabiqueDesviado() As Integer
            Get
                Return int_TabiqueDesviado
            End Get
            Set(ByVal value As Integer)
                int_TabiqueDesviado = value
            End Set
        End Property

        Public Property SangradoNasal() As Integer
            Get
                Return int_SangradoNasal
            End Get
            Set(ByVal value As Integer)
                int_SangradoNasal = value
            End Set
        End Property

        Public Property UsaLentes() As Integer
            Get
                Return int_UsaLentes
            End Get
            Set(ByVal value As Integer)
                int_UsaLentes = value
            End Set
        End Property

        Public Property ObservacionesOftalmologicas() As String
            Get
                Return str_ObservacionesOftalmologicas
            End Get
            Set(ByVal value As String)
                str_ObservacionesOftalmologicas = value
            End Set
        End Property

        Public Property UsaOrtodoncia() As Integer
            Get
                Return int_UsaOrtodoncia
            End Get
            Set(ByVal value As Integer)
                int_UsaOrtodoncia = value
            End Set
        End Property

        Public Property ObservacionesDental() As String
            Get
                Return str_ObservacionesDental
            End Get
            Set(ByVal value As String)
                str_ObservacionesDental = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoAlumno As String, _
                ByVal CodigoTipoNacimiento As Integer, _
                ByVal CodigoTipoSangre As Integer, _
                ByVal TipoNacimientoObservaciones As String, _
                ByVal EdadLevantoCabeza As Integer, _
                ByVal MesesLevantoCabeza As Integer, _
                ByVal EdadSento As Integer, _
                ByVal MesesSento As Integer, _
                ByVal EdadParo As Integer, _
                ByVal MesesParo As Integer, _
                ByVal EdadCamino As Integer, _
                ByVal MesesCamino As Integer, _
                ByVal EdadControloEsfinteres As Integer, _
                ByVal MesesControloEsfinteres As Integer, _
                ByVal EdadHabloPrimerasPalabras As Integer, _
                ByVal MesesHabloPrimerasPalabras As Integer, _
                ByVal EdadHabloFluidez As Integer, _
                ByVal MesesHabloFluidez As Integer, _
                ByVal TabiqueDesviado As Integer, _
                ByVal SangradoNasal As Integer, _
                ByVal UsaLentes As Integer, _
                ByVal ObservacionesOftalmologicas As String, _
                ByVal UsaOrtodoncia As Integer, _
                ByVal ObservacionesDental As String)
            str_CodigoAlumno = CodigoAlumno
            int_CodigoTipoNacimiento = CodigoTipoNacimiento
            int_CodigoTipoSangre = CodigoTipoSangre
            str_TipoNacimientoObservaciones = TipoNacimientoObservaciones
            int_EdadLevantoCabeza = EdadLevantoCabeza
            int_MesesLevantoCabeza = MesesLevantoCabeza
            int_EdadSento = EdadSento
            int_MesesSento = MesesSento
            int_EdadParo = EdadParo
            int_MesesParo = MesesParo
            int_EdadCamino = EdadCamino
            int_MesesCamino = MesesCamino
            int_EdadControloEsfinteres = EdadControloEsfinteres
            int_MesesControloEsfinteres = MesesControloEsfinteres
            int_EdadHabloPrimerasPalabras = EdadHabloPrimerasPalabras
            int_MesesHabloPrimerasPalabras = MesesHabloPrimerasPalabras
            int_EdadHabloFluidez = EdadHabloFluidez
            int_MesesHabloFluidez = MesesHabloFluidez
            int_TabiqueDesviado = TabiqueDesviado
            int_SangradoNasal = SangradoNasal
            int_UsaLentes = UsaLentes
            str_ObservacionesOftalmologicas = ObservacionesOftalmologicas
            int_UsaOrtodoncia = UsaOrtodoncia
            str_ObservacionesDental = ObservacionesDental

        End Sub

#End Region


    End Class

End Namespace
