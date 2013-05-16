Namespace ModuloCursos

    Public Class be_NombresGrupos

#Region "Atributos"

        Private int_Codigo As Integer
        Private str_NombreGrupo As String
        Private str_Abreviatura As String
        Private int_AlumnoXDefecto As Integer
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

        Public Property NombreGrupo() As String
            Get
                Return str_NombreGrupo
            End Get
            Set(ByVal value As String)
                str_NombreGrupo = value
            End Set
        End Property

        Public Property Abreviatura() As String
            Get
                Return str_Abreviatura
            End Get
            Set(ByVal value As String)
                str_Abreviatura = value
            End Set
        End Property

        Public Property AlumnoXDefecto() As Integer
            Get
                Return int_AlumnoXDefecto
            End Get
            Set(ByVal value As Integer)
                int_AlumnoXDefecto = value
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
                ByVal NombreGrupo As String, _
                ByVal Abreviatura As String, _
                ByVal AlumnoXDefecto As Integer, _
                ByVal Estado As Integer)

            int_Codigo = Codigo
            str_NombreGrupo = NombreGrupo
            str_Abreviatura = Abreviatura
            int_AlumnoXDefecto = AlumnoXDefecto
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace