Namespace ModuloEnfermeria

    Public Class be_MedicamentosAlumnos
#Region "Atributos"

        Private int_CodigoMedicamento As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
        Private int_Validado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoMedicamento() As Integer
            Get
                Return int_CodigoMedicamento
            End Get
            Set(ByVal value As Integer)
                int_CodigoMedicamento = value
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
        Public Property Validado() As Integer
            Get
                Return int_Validado
            End Get
            Set(ByVal value As Integer)
                int_Validado = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoMedicamento As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer, _
                 ByVal Validado As Integer)
            int_CodigoMedicamento = CodigoMedicamento
            str_Descripcion = Descripcion
            int_Estado = Estado
            int_Validado = Validado
        End Sub

#End Region
    End Class

End Namespace