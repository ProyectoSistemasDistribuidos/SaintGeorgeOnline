Namespace ModuloSeguimiento

    Public Class be_AsignacionCalificativosCriterios

#Region "Atributos"

        Private int_CodigoAsignacion As Integer
        Private int_CodigoTipoDocumento As Integer
        Private int_CodigoCalificativo As Integer
        Private int_CodigoCriterio As Integer
        Private int_CodigoAnioAcademico As Integer
        Private int_CodigoGrado As Integer
        Private str_Nota As String
        Private int_Orden As Integer
        Private int_Estado As Integer
        Private str_LeyendaIngles As String
        Private str_LeyendaEspaniol As String
        Private int_CodigoGrupoCriterio As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAsignacion() As Integer
            Get
                Return int_CodigoAsignacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacion = value
            End Set
        End Property

        Public Property CodigoTipoDocumento() As Integer
            Get
                Return int_CodigoTipoDocumento
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoDocumento = value
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

        Public Property CodigoCriterio() As Integer
            Get
                Return int_CodigoCriterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoCriterio = value
            End Set
        End Property

        Public Property CodigoAnioAcademico() As Integer
            Get
                Return int_CodigoAnioAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoAnioAcademico = value
            End Set
        End Property

        Public Property CodigoGrado() As Integer
            Get
                Return int_CodigoGrado
            End Get
            Set(ByVal value As Integer)
                int_CodigoGrado = value
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

        Public Property Orden() As Integer
            Get
                Return int_Orden
            End Get
            Set(ByVal value As Integer)
                int_Orden = value
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

        Public Property LeyendaIngles() As String
            Get
                Return str_LeyendaIngles
            End Get
            Set(ByVal value As String)
                str_LeyendaIngles = value
            End Set
        End Property

        Public Property LeyendaEspaniol() As String
            Get
                Return str_LeyendaEspaniol
            End Get
            Set(ByVal value As String)
                str_LeyendaEspaniol = value
            End Set
        End Property

        Public Property CodigoGrupoCriterio() As Integer
            Get
                Return int_CodigoGrupoCriterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoGrupoCriterio = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoAsignacion As Integer, _
                ByVal CodigoTipoDocumento As Integer, _
                ByVal CodigoCalificativo As Integer, _
                ByVal CodigoCriterio As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoGrado As Integer, _
                ByVal Nota As String, _
                ByVal Orden As Integer, _
                ByVal Estado As Integer, _
                ByVal LeyendaIngles As String, _
                ByVal LeyendaEspaniol As String, _
                ByVal CodigoGrupoCriterio As Integer)

            int_CodigoAsignacion = CodigoAsignacion
            int_CodigoTipoDocumento = CodigoTipoDocumento
            int_CodigoCalificativo = CodigoCalificativo
            int_CodigoCriterio = CodigoCriterio
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_CodigoGrado = CodigoGrado
            str_Nota = Nota
            int_Orden = Orden
            int_Estado = Estado
            str_LeyendaIngles = LeyendaIngles
            str_LeyendaEspaniol = LeyendaEspaniol
            int_CodigoGrupoCriterio = CodigoGrupoCriterio

        End Sub

#End Region

    End Class

End Namespace