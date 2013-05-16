Namespace ModuloSeguimiento

    Public Class be_MidTermReport

#Region "Atributos"

        Private int_CodigoMidTermReport As Integer
        Private int_CodigoAula As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoProgramacion As Integer
        Private str_Observacion As String

#End Region

#Region "Propiedades"

        Public Property CodigoMidTermReport() As Integer
            Get
                Return int_CodigoMidTermReport
            End Get
            Set(ByVal value As Integer)
                int_CodigoMidTermReport = value
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

        Public Property CodigoAlumno() As String
            Get
                Return str_CodigoAlumno
            End Get
            Set(ByVal value As String)
                str_CodigoAlumno = value
            End Set
        End Property

        Public Property CodigoProgramacion() As Integer
            Get
                Return int_CodigoProgramacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacion = value
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

        Sub New(ByVal CodigoMidTermReport As Integer, _
                ByVal CodigoAula As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoProgramacion As Integer, _
                ByVal Observacion As String)

            int_CodigoMidTermReport = CodigoMidTermReport
            int_CodigoAula = CodigoAula
            str_CodigoAlumno = CodigoAlumno
            int_CodigoProgramacion = CodigoProgramacion
            str_Observacion = Observacion
        End Sub

#End Region

    End Class

End Namespace
