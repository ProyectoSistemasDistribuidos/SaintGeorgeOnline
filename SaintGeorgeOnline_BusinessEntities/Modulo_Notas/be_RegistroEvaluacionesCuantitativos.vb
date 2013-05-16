Namespace ModuloNotas

    Public Class be_RegistroEvaluacionesCuantitativos

#Region "Atributos"

        Private int_CodigoRegistroEvaluacionCuantitava As Integer
        Private int_CodigoRegistroCriterioCuantitativo As Integer
        Private int_CodigoEvaluacionCuantitativa As Integer

        Private int_idCorr As Integer
        Private int_CodigoRegistroEvaluacion As Integer
        Private int_CodigoRegistroCriterio As Integer
        Private str_Descriptor As String

#End Region

#Region "Propiedades"

        Public Property CodigoRegistroEvaluacionCuantitava() As Integer
            Get
                Return int_CodigoRegistroEvaluacionCuantitava
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroEvaluacionCuantitava = value
            End Set
        End Property

        Public Property CodigoRegistroCriterioCuantitativo() As Integer
            Get
                Return int_CodigoRegistroCriterioCuantitativo
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroCriterioCuantitativo = value
            End Set
        End Property

        Public Property CodigoEvaluacionCuantitativa() As Integer
            Get
                Return int_CodigoEvaluacionCuantitativa
            End Get
            Set(ByVal value As Integer)
                int_CodigoEvaluacionCuantitativa = value
            End Set
        End Property


        Public Property idCorr() As Integer
            Get
                Return int_idCorr
            End Get
            Set(ByVal value As Integer)
                int_idCorr = value
            End Set
        End Property

        Public Property CodigoRegistroEvaluacion() As Integer
            Get
                Return int_CodigoRegistroEvaluacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroEvaluacion = value
            End Set
        End Property

        Public Property CodigoRegistroCriterio() As Integer
            Get
                Return int_CodigoRegistroCriterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroCriterio = value
            End Set
        End Property

        Public Property Descriptor() As String
            Get
                Return str_Descriptor
            End Get
            Set(ByVal value As String)
                str_Descriptor = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroEvaluacionCuantitava As Integer, _
                ByVal CodigoRegistroCriterioCuantitativo As Integer, _
                ByVal CodigoEvaluacionCuantitativa As Integer)

            int_CodigoRegistroEvaluacionCuantitava = CodigoRegistroEvaluacionCuantitava
            int_CodigoRegistroCriterioCuantitativo = CodigoRegistroCriterioCuantitativo
            int_CodigoEvaluacionCuantitativa = CodigoEvaluacionCuantitativa

        End Sub

#End Region

    End Class

End Namespace
