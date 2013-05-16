Namespace ModuloActividades

    Public Class be_AprobacionActividades

#Region "Atributos"
        Private int_CodigoActividad As Integer
        Private int_CodigoAprobacion As Integer
        Private int_CodigoTrabajador As Integer
        Private int_CodigoEstado As Integer
        Private str_Observacion As String
#End Region

#Region "Propiedades"

        Public Property CodigoActividad() As Integer
            Get
                Return int_CodigoActividad
            End Get
            Set(ByVal value As Integer)
                int_CodigoActividad = value
            End Set
        End Property
        Public Property CodigoAprobacion() As Integer
            Get
                Return int_CodigoAprobacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoAprobacion = value
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
        Public Property CodigoEstado() As Integer
            Get
                Return int_CodigoEstado
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstado = value
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
        Sub New(ByVal CodigoActividad As Integer, _
                ByVal CodigoAprobacion As Integer, _
                ByVal CodigoTrabajador As Integer, _
                ByVal CodigoEstado As Integer, _
                ByVal Observacion As String)

            int_CodigoActividad = CodigoActividad
            int_CodigoAprobacion = CodigoAprobacion
            int_CodigoTrabajador = CodigoTrabajador
            int_CodigoEstado = CodigoEstado
            str_Observacion = Observacion

        End Sub

#End Region

    End Class

End Namespace