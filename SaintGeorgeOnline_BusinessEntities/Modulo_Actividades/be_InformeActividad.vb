Namespace ModuloActividades

    Public Class be_InformeActividad

#Region "Atributos"
        Private int_CodigoInforme As Integer
        Private int_CodigoActividad As Integer
        Private str_Objetivo As String
        Private str_Logros As String
        Private str_Dificultades As String
        Private str_MomentosImportantes As String
        Private str_Conclusiones As String
        Private str_Recomendaciones As String
        Private str_InformacionImagen As String
#End Region

#Region "Propiedades"

        Public Property CodigoInforme() As Integer
            Get
                Return int_CodigoInforme
            End Get
            Set(ByVal value As Integer)
                int_CodigoInforme = value
            End Set
        End Property
        Public Property CodigoActividad() As Integer
            Get
                Return int_CodigoActividad
            End Get
            Set(ByVal value As Integer)
                int_CodigoActividad = value
            End Set
        End Property
        Public Property Objetivo() As String
            Get
                Return str_Objetivo
            End Get
            Set(ByVal value As String)
                str_Objetivo = value
            End Set
        End Property
        Public Property Logros() As String
            Get
                Return str_Logros
            End Get
            Set(ByVal value As String)
                str_Logros = value
            End Set
        End Property
        Public Property Dificultades() As String
            Get
                Return str_Dificultades
            End Get
            Set(ByVal value As String)
                str_Dificultades = value
            End Set
        End Property
        Public Property MomentosImportantes() As String
            Get
                Return str_MomentosImportantes
            End Get
            Set(ByVal value As String)
                str_MomentosImportantes = value
            End Set
        End Property
        Public Property Conclusiones() As String
            Get
                Return str_Conclusiones
            End Get
            Set(ByVal value As String)
                str_Conclusiones = value
            End Set
        End Property
        Public Property Recomendaciones() As String
            Get
                Return str_Recomendaciones
            End Get
            Set(ByVal value As String)
                str_Recomendaciones = value
            End Set
        End Property
        Public Property InformacionImagen() As String
            Get
                Return str_InformacionImagen
            End Get
            Set(ByVal value As String)
                str_InformacionImagen = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoInforme As Integer, _
                ByVal CodigoActividad As Integer, _
                ByVal Objetivo As String, _
                ByVal Logros As String, _
                ByVal Dificultades As String, _
                ByVal MomentosImportantes As String, _
                ByVal Conclusiones As String, _
                ByVal Recomendaciones As String, _
                ByVal InformacionImagen As String)

            int_CodigoInforme = int_CodigoInforme
            int_CodigoActividad = CodigoActividad
            str_Objetivo = Objetivo
            str_Logros = Logros
            str_Dificultades = Dificultades
            str_MomentosImportantes = MomentosImportantes
            str_Conclusiones = Conclusiones
            str_Recomendaciones = Recomendaciones
            str_InformacionImagen = InformacionImagen

        End Sub

#End Region

    End Class

End Namespace