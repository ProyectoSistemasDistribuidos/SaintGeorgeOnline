Namespace ModuloNotas

    Public Class be_RegistroNotasAnualCuantitativo

#Region "Atributos"

        Private int_CodigoRegistroAnual As Integer
        Private int_CodigoAsignacionGrupo As Integer
        Private int_CodigoAnioAcademico As Integer
        Private str_CodigoAlumno As String
        Private de_NotaAnual As Decimal

        Private de_auxNota1 As Integer
        Private de_auxNota2 As Integer
        Private de_auxNota3 As Integer
        Private de_auxNota4 As Integer
        Private int_auxCodigoRegistroBimestral1 As Integer
        Private int_auxCodigoRegistroBimestral2 As Integer
        Private int_auxCodigoRegistroBimestral3 As Integer
        Private int_auxCodigoRegistroBimestral4 As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRegistroAnual() As Integer
            Get
                Return int_CodigoRegistroAnual
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroAnual = value
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

        Public Property CodigoAnioAcademico() As Integer
            Get
                Return int_CodigoAnioAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoAnioAcademico = value
            End Set
        End Property

        Public Property CodigoAlumno() As String
            Get
                Return str_CodigoAlumno
            End Get
            Set(ByVal value As String)
                str_CodigoAlumno = value
            End Set
        End Property

        Public Property NotaAnual() As Decimal
            Get
                Return de_NotaAnual
            End Get
            Set(ByVal value As Decimal)
                de_NotaAnual = value
            End Set
        End Property

        Public Property auxNota1() As Decimal
            Get
                Return de_auxNota1
            End Get
            Set(ByVal value As Decimal)
                de_auxNota1 = value
            End Set
        End Property
        Public Property auxNota2() As Decimal
            Get
                Return de_auxNota2
            End Get
            Set(ByVal value As Decimal)
                de_auxNota2 = value
            End Set
        End Property
        Public Property auxNota3() As Decimal
            Get
                Return de_auxNota3
            End Get
            Set(ByVal value As Decimal)
                de_auxNota3 = value
            End Set
        End Property
        Public Property auxNota4() As Decimal
            Get
                Return de_auxNota4
            End Get
            Set(ByVal value As Decimal)
                de_auxNota4 = value
            End Set
        End Property

        Public Property auxCodigoRegistroBimestral1() As Integer
            Get
                Return int_auxCodigoRegistroBimestral1
            End Get
            Set(ByVal value As Integer)
                int_auxCodigoRegistroBimestral1 = value
            End Set
        End Property
        Public Property auxCodigoRegistroBimestral2() As Integer
            Get
                Return int_auxCodigoRegistroBimestral2
            End Get
            Set(ByVal value As Integer)
                int_auxCodigoRegistroBimestral2 = value
            End Set
        End Property
        Public Property auxCodigoRegistroBimestral3() As Integer
            Get
                Return int_auxCodigoRegistroBimestral3
            End Get
            Set(ByVal value As Integer)
                int_auxCodigoRegistroBimestral3 = value
            End Set
        End Property
        Public Property auxCodigoRegistroBimestral4() As Integer
            Get
                Return int_auxCodigoRegistroBimestral4
            End Get
            Set(ByVal value As Integer)
                int_auxCodigoRegistroBimestral4 = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroAnual As Integer, _
                ByVal CodigoAsignacionGrupo As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal NotaAnual As Decimal, _
                ByVal auxNota1 As Decimal, _
                ByVal auxNota2 As Decimal, _
                ByVal auxNota3 As Decimal, _
                ByVal auxNota4 As Decimal, _
                ByVal auxCodigoRegistroBimestral1 As Integer, _
                ByVal auxCodigoRegistroBimestral2 As Integer, _
                ByVal auxCodigoRegistroBimestral3 As Integer, _
                ByVal auxCodigoRegistroBimestral4 As Integer)

            int_CodigoRegistroAnual = CodigoRegistroAnual
            int_CodigoAsignacionGrupo = CodigoAsignacionGrupo
            int_CodigoAnioAcademico = CodigoAnioAcademico
            str_CodigoAlumno = CodigoAlumno
            de_NotaAnual = NotaAnual
            de_auxNota1 = auxNota1
            de_auxNota2 = auxNota2
            de_auxNota3 = auxNota3
            de_auxNota4 = auxNota4
            int_auxCodigoRegistroBimestral1 = auxCodigoRegistroBimestral1
            int_auxCodigoRegistroBimestral2 = auxCodigoRegistroBimestral2
            int_auxCodigoRegistroBimestral3 = auxCodigoRegistroBimestral3
            int_auxCodigoRegistroBimestral4 = auxCodigoRegistroBimestral4

        End Sub

#End Region

    End Class

End Namespace
