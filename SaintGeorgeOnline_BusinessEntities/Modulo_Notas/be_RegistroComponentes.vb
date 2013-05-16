Namespace ModuloNotas

    Public Class be_RegistroComponentes

#Region "Atributos"

        Private int_CodigoRegistroComponentes As Integer
        Private int_CodigoComponente As Integer
        Private int_CodigoAsignacionGrupo As Integer
        Private int_CodigoBimestre As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRegistroComponentes() As Integer
            Get
                Return int_CodigoRegistroComponentes
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroComponentes = value
            End Set
        End Property

        Public Property CodigoComponente() As Integer
            Get
                Return int_CodigoComponente
            End Get
            Set(ByVal value As Integer)
                int_CodigoComponente = value
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

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroComponentes As Integer, _
                ByVal CodigoComponente As Integer, _
                ByVal CodigoAsignacionGrupo As Integer, _
                ByVal CodigoBimestre As Integer)

            int_CodigoRegistroComponentes = CodigoRegistroComponentes
            int_CodigoComponente = CodigoComponente
            int_CodigoAsignacionGrupo = CodigoAsignacionGrupo
            int_CodigoBimestre = CodigoBimestre

        End Sub

#End Region

    End Class

End Namespace