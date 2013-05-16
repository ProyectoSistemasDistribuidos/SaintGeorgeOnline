Namespace ModuloSeguimiento

    Public Class be_MidTermReportDetalle

#Region "Atributos"

        Private int_CodigoMidTermReportDetalle As Integer
        Private int_CodigoMidTermReport As Integer
        Private int_CodigoCriterio As Integer
        Private int_CodigoCalificativo As Integer
        Private int_CodigoCurso As Integer
        Private str_Nota As String

#End Region

#Region "Propiedades"

        Public Property CodigoMidTermReportDetalle() As Integer
            Get
                Return int_CodigoMidTermReportDetalle
            End Get
            Set(ByVal value As Integer)
                int_CodigoMidTermReportDetalle = value
            End Set
        End Property

        Public Property CodigoMidTermReport() As Integer
            Get
                Return int_CodigoMidTermReport
            End Get
            Set(ByVal value As Integer)
                int_CodigoMidTermReport = value
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

        Public Property CodigoCurso() As Integer
            Get
                Return int_CodigoCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoCurso = value
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

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoMidTermReportDetalle As Integer, _
                ByVal CodigoMidTermReport As Integer, _
                ByVal CodigoCriterio As Integer, _
                ByVal CodigoCalificativo As Integer, _
                ByVal CodigoCurso As Integer, _
                ByVal Nota As String)

            int_CodigoMidTermReportDetalle = CodigoMidTermReportDetalle
            int_CodigoMidTermReport = CodigoMidTermReport
            int_CodigoCriterio = CodigoCriterio
            int_CodigoCalificativo = CodigoCalificativo
            int_CodigoCurso = CodigoCurso
            str_Nota = Nota
        End Sub

#End Region

    End Class

End Namespace
