Namespace ModuloNotas

    Public Class be_RegistroCriteriosCuantitativos

#Region "Atributos"

        Private int_CodigoRegistroCriterioCuantitativo As Integer
        Private int_CodigoCriterio As Integer
        Private int_CodigoAsignacionGrupo As Integer
        Private int_CodigoBimestre As Integer
        Private int_Peso As Integer

        Private int_CodigoRegistro As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRegistroCriterioCuantitativo() As Integer
            Get
                Return int_CodigoRegistroCriterioCuantitativo
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroCriterioCuantitativo = value
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

        Public Property CodigoAsignacionGrupo() As Integer
            Get
                Return int_CodigoAsignacionGrupo
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionGrupo = value
            End Set
        End Property

        Public Property CodigoBimestre() As Integer
            Get
                Return int_CodigoBimestre
            End Get
            Set(ByVal value As Integer)
                int_CodigoBimestre = value
            End Set
        End Property

        Public Property Peso() As Integer
            Get
                Return int_Peso
            End Get
            Set(ByVal value As Integer)
                int_Peso = value
            End Set
        End Property

        Public Property CodigoRegistro() As Integer
            Get
                Return int_CodigoRegistro
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistro = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroCriterioCuantitativo As Integer, _
                ByVal CodigoCriterio As Integer, _
                ByVal CodigoAsignacionGrupo As Integer, _
                ByVal CodigoBimestre As Integer, _
                ByVal Peso As Integer, _
                ByVal CodigoRegistro As Integer)

            int_CodigoRegistroCriterioCuantitativo = CodigoRegistroCriterioCuantitativo
            int_CodigoCriterio = CodigoCriterio
            int_CodigoAsignacionGrupo = CodigoAsignacionGrupo
            int_CodigoBimestre = CodigoBimestre
            int_Peso = Peso
            int_CodigoRegistro = CodigoRegistro

        End Sub

#End Region

    End Class

End Namespace