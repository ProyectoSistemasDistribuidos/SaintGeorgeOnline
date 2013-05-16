Namespace ModuloEnfermeria

    Public Class be_NombresMedicamentos

#Region "Atributos"

        Private int_CodigoNombreMedicamento As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoNombreMedicamento() As Integer
            Get
                Return int_CodigoNombreMedicamento
            End Get
            Set(ByVal value As Integer)
                int_CodigoNombreMedicamento = value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Return str_Descripcion
            End Get
            Set(ByVal value As String)
                str_Descripcion = value
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
        Sub New(ByVal CodigoNombreMedicamento As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)
            int_CodigoNombreMedicamento = CodigoNombreMedicamento
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
