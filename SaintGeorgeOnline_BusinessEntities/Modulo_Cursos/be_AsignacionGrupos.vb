Namespace ModuloCursos

    Public Class be_AsignacionGrupos

#Region "Atributos"

        Private int_CodigoAsignacionGrupo As Integer
        Private int_CodigoAsignacionCurso As Integer
        Private int_CodigoNombreGrupo As Integer
        Private int_CodigoPersonaProfesor1 As Integer
        Private int_CodigoPersonaProfesor2 As Integer
        Private int_CodigoPersonaProfesor3 As Integer
        Private int_CodigoPersonaProfesor4 As Integer
        Private int_CodigoAsignacionAula As Integer
        Private str_TipoRegistro As String
        Private int_NumeroVacantesTaller As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionGrupo() As Integer
            Get
                Return int_CodigoAsignacionGrupo
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionGrupo = value
            End Set
        End Property

        Public Property CodigoAsignacionCurso() As Integer
            Get
                Return int_CodigoAsignacionCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionCurso = value
            End Set
        End Property

        Public Property CodigoNombreGrupo() As Integer
            Get
                Return int_CodigoNombreGrupo
            End Get
            Set(ByVal value As Integer)
                int_CodigoNombreGrupo = value
            End Set
        End Property

        Public Property CodigoPersonaProfesor1() As Integer
            Get
                Return int_CodigoPersonaProfesor1
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaProfesor1 = value
            End Set
        End Property

        Public Property CodigoPersonaProfesor2() As Integer
            Get
                Return int_CodigoPersonaProfesor2
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaProfesor2 = value
            End Set
        End Property

        Public Property CodigoPersonaProfesor3() As Integer
            Get
                Return int_CodigoPersonaProfesor3
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaProfesor3 = value
            End Set
        End Property

        Public Property CodigoPersonaProfesor4() As Integer
            Get
                Return int_CodigoPersonaProfesor4
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaProfesor4 = value
            End Set
        End Property

        Public Property CodigoAsignacionAula() As Integer
            Get
                Return int_CodigoAsignacionAula
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionAula = value
            End Set
        End Property

        Public Property TipoRegistro() As String
            Get
                Return str_TipoRegistro
            End Get
            Set(ByVal value As String)
                str_TipoRegistro = value
            End Set
        End Property

        Public Property NumeroVacantesTaller() As Integer
            Get
                Return int_NumeroVacantesTaller
            End Get
            Set(ByVal value As Integer)
                int_NumeroVacantesTaller = value
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

        Sub New(ByVal CodigoAsignacionGrupo As Integer, _
                ByVal CodigoAsignacionCurso As Integer, _
                ByVal CodigoNombreGrupo As Integer, _
                ByVal CodigoPersonaProfesor1 As Integer, _
                ByVal CodigoPersonaProfesor2 As Integer, _
                ByVal CodigoPersonaProfesor3 As Integer, _
                ByVal CodigoPersonaProfesor4 As Integer, _
                ByVal CodigoAsignacionAula As Integer, _
                ByVal TipoRegistro As String, _
                ByVal NumeroVacantesTaller As Integer, _
                ByVal Estado As Integer)

            int_CodigoAsignacionGrupo = CodigoAsignacionGrupo
            int_CodigoAsignacionCurso = CodigoAsignacionCurso
            int_CodigoNombreGrupo = CodigoNombreGrupo
            int_CodigoPersonaProfesor1 = CodigoPersonaProfesor1
            int_CodigoPersonaProfesor2 = CodigoPersonaProfesor2
            int_CodigoPersonaProfesor3 = CodigoPersonaProfesor3
            int_CodigoPersonaProfesor4 = CodigoPersonaProfesor4
            int_CodigoAsignacionAula = CodigoAsignacionAula
            str_TipoRegistro = TipoRegistro
            int_NumeroVacantesTaller = NumeroVacantesTaller
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace



