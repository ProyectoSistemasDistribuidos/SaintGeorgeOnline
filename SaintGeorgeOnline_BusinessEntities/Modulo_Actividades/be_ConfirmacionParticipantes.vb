Namespace ModuloActividades

    Public Class be_ConfirmacionParticipantes

#Region "Atributos"
        'ok
        Private int_CodigoConfirmacionAsistencia As Integer
        Private int_CodigoProgramacionActividad As Integer
        Private int_CantidadParticipantes As Integer
        Private str_Observacion As String
        Private str_CodigoFamilia As String
        Private dt_Fecha As Date
        Private int_CodigoTrabajador As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoConfirmacionAsistencia() As Integer
            Get
                Return int_CodigoConfirmacionAsistencia
            End Get
            Set(ByVal value As Integer)
                int_CodigoConfirmacionAsistencia = value
            End Set
        End Property

        Public Property CodigoProgramacionActividad() As Integer
            Get
                Return int_CodigoProgramacionActividad
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionActividad = value
            End Set
        End Property

        Public Property CantidadParticipantes() As Integer
            Get
                Return int_CantidadParticipantes
            End Get
            Set(ByVal value As Integer)
                int_CantidadParticipantes = value
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

        Public Property CodigoFamilia() As String
            Get
                Return str_CodigoFamilia
            End Get
            Set(ByVal value As String)
                str_CodigoFamilia = value
            End Set
        End Property

        Public Property Fecha() As Date
            Get
                Return dt_Fecha
            End Get
            Set(ByVal value As Date)
                dt_Fecha = value
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
#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoConfirmacionAsistencia As Integer, _
                ByVal CodigoProgramacionActividad As Integer, _
                ByVal CantidadParticipantes As Integer, _
                ByVal Observacion As String, _
                ByVal CodigoFamilia As String, _
                ByVal Fecha As Date, _
                ByVal CodigoTrabajador As Integer)

            int_CodigoConfirmacionAsistencia = CodigoConfirmacionAsistencia
            int_CodigoProgramacionActividad = CodigoProgramacionActividad
            int_CantidadParticipantes = CantidadParticipantes
            str_Observacion = Observacion
            str_CodigoFamilia = CodigoFamilia
            dt_Fecha = Fecha
            int_CodigoTrabajador = CodigoTrabajador

        End Sub

#End Region

    End Class
End Namespace