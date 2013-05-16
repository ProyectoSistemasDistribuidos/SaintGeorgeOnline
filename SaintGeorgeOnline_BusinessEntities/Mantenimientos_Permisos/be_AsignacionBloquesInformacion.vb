Namespace ModuloPermisos

    Public Class be_AsignacionBloquesInformacion

#Region "Atributos"

        Private int_CodigoAsignacion As Integer
        Private int_CodigoSubBloque As Integer
        Private int_CodigoBloqueInformacion As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAsignacion() As Integer
            Get
                Return int_CodigoAsignacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacion = value
            End Set
        End Property

        Public Property CodigoSubBloque() As Integer
            Get
                Return int_CodigoSubBloque
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubBloque = value
            End Set
        End Property

        Public Property CodigoBloqueInformacion() As Integer
            Get
                Return int_CodigoBloqueInformacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoBloqueInformacion = value
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
        Sub New(ByVal CodigoAsignacion As Integer, _
                ByVal CodigoSubBloque As Integer, _
                ByVal CodigoBloqueInformacion As Integer, _
                ByVal Estado As Integer)

            int_CodigoAsignacion = CodigoAsignacion
            int_CodigoSubBloque = CodigoSubBloque
            int_CodigoBloqueInformacion = CodigoBloqueInformacion
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace
