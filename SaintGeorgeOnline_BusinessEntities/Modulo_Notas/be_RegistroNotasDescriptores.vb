Namespace ModuloNotas

    Public Class be_RegistroNotasDescriptores

#Region "Atributos"

        Private int_CodigoRegistroNota As Integer
        Private int_CodigoCriterio As Integer
        Private int_CodigoCalificativo As Integer
        Private int_CodigoProgramacionDescriptores As Integer
        Private str_CodigoAlumno As String
        Private str_Nota As String

        Private int_MiCheck As Integer
        Private int_Orden As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRegistroNota() As Integer
            Get
                Return int_CodigoRegistroNota
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroNota = value
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

        Public Property CodigoProgramacionDescriptores() As Integer
            Get
                Return int_CodigoProgramacionDescriptores
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionDescriptores = value
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

        Public Property Orden() As Integer
            Get
                Return int_Orden
            End Get
            Set(ByVal value As Integer)
                int_Orden = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroNota As Integer, _
                ByVal CodigoCriterio As Integer, _
                ByVal CodigoCalificativo As Integer, _
                ByVal CodigoProgramacionDescriptores As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal Nota As String, _
                ByVal MiCheck As Integer, _
                ByVal Orden As Integer)

            int_CodigoRegistroNota = CodigoRegistroNota
            int_CodigoCriterio = CodigoCriterio
            int_CodigoCalificativo = CodigoCalificativo
            int_CodigoProgramacionDescriptores = CodigoProgramacionDescriptores
            str_CodigoAlumno = CodigoAlumno
            str_Nota = Nota
            int_MiCheck = MiCheck
            int_Orden = Orden

        End Sub

#End Region

    End Class

End Namespace