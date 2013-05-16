Namespace ModuloConductaAlumnos

    Public Class be_RegistroBlackMark

#Region "Atributos"
        Private int_CodigoRegistroBackMark As Integer
        Private int_CodigoTrabajador As Integer
        Private int_CodigoRegistroConductual As Integer
        Private int_CodigoRegistroConductaBimestral As Integer
        Private str_Descripcion As String
        Private dt_FechaRegistroBlackMark As Date
        Private int_CodigoAsignacionGrupo As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoRegistroBackMark() As Integer
            Get
                Return int_CodigoRegistroBackMark
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroBackMark = value
            End Set
        End Property

        Public Property CodigoRegistroConductual() As Integer
            Get
                Return int_CodigoRegistroConductual
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroConductual = value
            End Set
        End Property

        Public Property CodigoTrabajador() As Integer
            Get
                Return int_CodigoTrabajador
            End Get
            Set(ByVal value As Integer)
                int_CodigoTrabajador = value
            End Set
        End Property

        Public Property CodigoRegistroConductaBimestral() As Integer
            Get
                Return int_CodigoRegistroConductaBimestral
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroConductaBimestral = value
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

        Public Property FechaRegistroBlackMark() As Date
            Get
                Return dt_FechaRegistroBlackMark
            End Get
            Set(ByVal value As Date)
                dt_FechaRegistroBlackMark = value
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
#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroBackMark As Integer, _
                ByVal CodigoCriterioConducta As Integer, _
                ByVal CodigoTrabajador As Integer, _
                ByVal CodigoAsignacionGrupo As Integer, _
                ByVal CodigoRegistroConductaBimestral As Integer, _
                ByVal Descripcion As String, _
                ByVal FechaRegistroBlackMark As Date)
            int_CodigoRegistroBackMark = CodigoRegistroBackMark
            int_CodigoTrabajador = CodigoTrabajador
            int_CodigoRegistroConductual = CodigoRegistroConductual
            int_CodigoRegistroConductaBimestral = CodigoRegistroConductaBimestral
            str_Descripcion = Descripcion
            dt_FechaRegistroBlackMark = FechaRegistroBlackMark
            int_CodigoAsignacionGrupo = CodigoAsignacionGrupo
        End Sub

#End Region

    End Class

End Namespace

