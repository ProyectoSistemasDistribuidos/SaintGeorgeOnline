Namespace ModuloNotas

    Public Class be_RegistroGradosPronostico

#Region "Atributos"

        Private int_CodigoRegistroNotaGradoPronostico As Integer
        Private int_CodigoProgramacionGradoPronostico As Integer
        Private int_CodigoAsignacionGrupoCurso As Integer
        Private str_CodigoAlumno As String
        Private int_Nota As Integer
        Private str_Observacion As String

#End Region

#Region "Propiedades"

        Public Property CodigoRegistroNotaGradoPronostico() As Integer
            Get
                Return int_CodigoRegistroNotaGradoPronostico
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroNotaGradoPronostico = value
            End Set
        End Property

        Public Property CodigoProgramacionGradoPronostico() As Integer
            Get
                Return int_CodigoProgramacionGradoPronostico
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionGradoPronostico = value
            End Set
        End Property

        Public Property CodigoAsignacionGrupoCurso() As Integer
            Get
                Return int_CodigoAsignacionGrupoCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionGrupoCurso = value
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

        Public Property Nota() As Integer
            Get
                Return int_Nota
            End Get
            Set(ByVal value As Integer)
                int_Nota = value
            End Set
        End Property

        Public Property Observacion() As String
            Get
                Return str_Observacion
            End Get
            Set(ByVal value As String)
                str_Observacion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroNotaGradoPronostico As Integer, _
                ByVal CodigoProgramacionGradoPronostico As Integer, _
                ByVal CodigoAsignacionGrupoCurso As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal Nota As Integer, _
                ByVal Observacion As Integer)

            int_CodigoRegistroNotaGradoPronostico = CodigoRegistroNotaGradoPronostico
            int_CodigoProgramacionGradoPronostico = CodigoProgramacionGradoPronostico
            int_CodigoAsignacionGrupoCurso = CodigoAsignacionGrupoCurso
            str_CodigoAlumno = CodigoAlumno
            int_Nota = Nota
            str_Observacion = Observacion

        End Sub

#End Region

    End Class

End Namespace