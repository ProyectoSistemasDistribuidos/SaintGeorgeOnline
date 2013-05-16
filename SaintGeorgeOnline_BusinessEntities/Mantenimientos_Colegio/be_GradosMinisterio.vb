Namespace ModuloColegio

    Public Class be_GradosMinisterio

#Region "Atributos"

        Private int_CodigoGradoMinisterio As Integer
        Private int_CodigoNivelMinisterio As Integer
        Private int_CodigoNombreGradoMinisterio As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoGradoMinisterio() As Integer
            Get
                Return int_CodigoGradoMinisterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoGradoMinisterio = value
            End Set
        End Property

        Public Property CodigoNivelMinisterio() As Integer
            Get
                Return int_CodigoNivelMinisterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoNivelMinisterio = value
            End Set
        End Property

        Public Property CodigoNombreGradoMinisterio() As Integer
            Get
                Return int_CodigoNombreGradoMinisterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoNombreGradoMinisterio = value
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
        Sub New(ByVal CodigoGradoMinisterio As Integer, _
                ByVal CodigoNivelMinisterio As Integer, _
                ByVal CodigoNombreGradoMinisterio As Integer, _
                ByVal Estado As Integer)

            int_CodigoGradoMinisterio = CodigoGradoMinisterio
            int_CodigoNivelMinisterio = CodigoNivelMinisterio
            int_CodigoNombreGradoMinisterio = CodigoNombreGradoMinisterio
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
