Namespace ModuloMatricula

    Public Class be_RelacionFamiliarProfesion

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoFamiliar As Integer
        Private int_CodigoProfesion As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRelacion() As Integer
            Get
                Return int_CodigoRelacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelacion = value
            End Set
        End Property

        Public Property CodigoFamiliar() As Integer
            Get
                Return int_CodigoFamiliar
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamiliar = value
            End Set
        End Property

        Public Property CodigoProfesion() As Integer
            Get
                Return int_CodigoProfesion
            End Get
            Set(ByVal value As Integer)
                int_CodigoProfesion = value
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

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoFamiliar As Integer, _
                ByVal CodigoProfesion As Integer, _
                ByVal Estado As Integer)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoFamiliar = CodigoFamiliar
            int_CodigoProfesion = CodigoProfesion
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace
