Namespace ModuloNotas

    Public Class be_RegistroPerfilEstudiante

#Region "Atributos"

        Private int_CodigoRegistroPerfil As Integer
        Private int_CodigoCriterio As Integer
        Private int_CodigoCalificativo As Integer
        Private int_CodigoProgramacionPerfil As Integer
        Private str_CodigoAlumno As String
        Private str_Nota As String

        Private int_MiCheck As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRegistroPerfil() As Integer
            Get
                Return int_CodigoRegistroPerfil
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroPerfil = value
            End Set
        End Property

        Public Property CodigoCriterio() As Integer
            Get
                Return int_CodigoCriterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoCriterio = value
            End Set
        End Property

        Public Property CodigoCalificativo() As Integer
            Get
                Return int_CodigoCalificativo
            End Get
            Set(ByVal value As Integer)
                int_CodigoCalificativo = value
            End Set
        End Property

        Public Property CodigoProgramacionPerfil() As Integer
            Get
                Return int_CodigoProgramacionPerfil
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionPerfil = value
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

        Public Property Nota() As String
            Get
                Return str_Nota
            End Get
            Set(ByVal value As String)
                str_Nota = value
            End Set
        End Property

        Public Property MiCheck() As Integer
            Get
                Return int_MiCheck
            End Get
            Set(ByVal value As Integer)
                int_MiCheck = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroPerfil As Integer, _
                ByVal CodigoCriterio As Integer, _
                ByVal CodigoCalificativo As Integer, _
                ByVal CodigoProgramacionPerfil As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal Nota As String, _
                ByVal MiCheck As Integer)

            int_CodigoRegistroPerfil = CodigoRegistroPerfil
            int_CodigoCriterio = CodigoCriterio
            int_CodigoCalificativo = CodigoCalificativo
            int_CodigoProgramacionPerfil = CodigoProgramacionPerfil
            str_CodigoAlumno = CodigoAlumno
            str_Nota = Nota
            int_MiCheck = MiCheck

        End Sub

#End Region

    End Class

End Namespace