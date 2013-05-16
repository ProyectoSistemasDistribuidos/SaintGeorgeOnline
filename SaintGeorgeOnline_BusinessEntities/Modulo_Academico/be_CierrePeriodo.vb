Namespace ModuloAcademico

    Public Class be_CierrePeriodo

#Region "Atributos"
        Private int_CodigoPeriodo As Integer
        Private int_CodigoAsignacionAula As Integer
        Private int_CodigoBimestre As Integer
#End Region

#Region "Propiedades"
        Public Property CodigoPeriodo() As Integer
            Get
                Return int_CodigoPeriodo
            End Get
            Set(ByVal value As Integer)
                int_CodigoPeriodo = value
            End Set
        End Property
        Public Property CodigoAsignacionAula() As Integer
            Get
                Return int_CodigoAsignacionAula
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionAula = value
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
            MyBase.new()
        End Sub
        Sub New(ByVal CodigoPeriodo As Integer, _
                ByVal CodigoAsignacionAula As Integer, _
                ByVal CodigoBimestre As Integer)
            int_CodigoPeriodo = CodigoPeriodo
            int_CodigoAsignacionAula = CodigoAsignacionAula
            int_CodigoBimestre = CodigoBimestre
        End Sub
#End Region

    End Class

End Namespace