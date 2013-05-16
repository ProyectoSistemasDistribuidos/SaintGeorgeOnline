Namespace ModuloNotas

    Public Class be_RegistroNotasBimestralesCuantitativas

#Region "Atributos"

        Private int_CodigoRegistroBimestral As Integer
        Private int_CodigoBimestre As Integer
        Private int_CodigoRegistroAnual As Integer
        Private de_NotaBimestral As Decimal
        Private str_Observacion As String

#End Region

#Region "Propiedades"

        Public Property CodigoRegistroBimestral() As Integer
            Get
                Return int_CodigoRegistroBimestral
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroBimestral = value
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

        Public Property CodigoRegistroAnual() As Integer
            Get
                Return int_CodigoRegistroAnual
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroAnual = value
            End Set
        End Property

        Public Property NotaBimestral() As Decimal
            Get
                Return de_NotaBimestral
            End Get
            Set(ByVal value As Decimal)
                de_NotaBimestral = value
            End Set
        End Property

        Public Property Observacion() As String
            Get
                Return str_Observacion
            End Get
            Set(ByVal value As String)
                str_Observacion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroBimestral As Integer, _
                ByVal CodigoBimestre As Integer, _
                ByVal CodigoRegistroAnual As Integer, _
                ByVal NotaBimestral As Decimal, _
                ByVal Observacion As String)

            int_CodigoRegistroBimestral = CodigoRegistroBimestral
            int_CodigoBimestre = CodigoBimestre
            int_CodigoRegistroAnual = CodigoRegistroAnual
            de_NotaBimestral = NotaBimestral
            str_Observacion = Observacion

        End Sub

#End Region

    End Class

End Namespace