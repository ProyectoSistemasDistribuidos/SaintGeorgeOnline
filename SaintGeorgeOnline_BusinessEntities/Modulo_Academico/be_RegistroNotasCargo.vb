Namespace ModuloAcademico

    Public Class be_RegistroNotasCargo

#Region "Atributos"

        Private int_TipoNota As Integer ' Cuantitativo: 1, Cualitativo: 2
        Private int_CodigoRegistroCargo As Integer
        Private int_CodigoRegistroAnual As Integer
        Private int_CodigoPersonaProfesor As Integer

        Private str_Nota As String

        Private str_NotaCl As String
        Private str_NotaCt As Decimal
        Private dt_FechaExamen As Date
        Private int_CodigoTipoExamen As Integer

#End Region

#Region "Propiedades"

        Public Property TipoNota() As Integer
            Get
                Return int_TipoNota
            End Get
            Set(ByVal value As Integer)
                int_TipoNota = value
            End Set
        End Property

        Public Property CodigoRegistroCargo() As Integer
            Get
                Return int_CodigoRegistroCargo
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroCargo = value
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

        Public Property CodigoPersonaProfesor() As Integer
            Get
                Return int_CodigoPersonaProfesor
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaProfesor = value
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

        Public Property NotaCl() As String
            Get
                Return str_NotaCl
            End Get
            Set(ByVal value As String)
                str_NotaCl = value
            End Set
        End Property

        Public Property NotaCt() As Decimal
            Get
                Return str_NotaCt
            End Get
            Set(ByVal value As Decimal)
                str_NotaCt = value
            End Set
        End Property

        Public Property FechaExamen() As Date
            Get
                Return dt_FechaExamen
            End Get
            Set(ByVal value As Date)
                dt_FechaExamen = value
            End Set
        End Property

        Public Property CodigoTipoExamen() As Integer
            Get
                Return int_CodigoTipoExamen
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoExamen = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal TipoNota As Integer, _
                ByVal CodigoRegistroCargo As Integer, _
                ByVal CodigoRegistroAnual As Integer, _
                ByVal CodigoPersonaProfesor As Integer, _
                ByVal Nota As String, _
                ByVal NotaCl As String, _
                ByVal NotaCt As Decimal, _
                ByVal FechaExamen As Date, _
                ByVal CodigoTipoExamen As Integer)

            int_TipoNota = TipoNota
            int_CodigoRegistroCargo = CodigoRegistroCargo
            int_CodigoRegistroAnual = CodigoRegistroAnual
            int_CodigoPersonaProfesor = CodigoPersonaProfesor
            str_Nota = Nota
            str_NotaCl = NotaCl
            str_NotaCt = NotaCt
            dt_FechaExamen = FechaExamen
            int_CodigoTipoExamen = CodigoTipoExamen

        End Sub

#End Region

    End Class

End Namespace