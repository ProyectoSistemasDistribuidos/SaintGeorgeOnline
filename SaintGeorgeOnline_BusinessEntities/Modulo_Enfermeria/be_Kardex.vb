Namespace ModuloEnfermeria

    Public Class be_Kardex

#Region "Atributos"

        Private int_CodigoKardex As Integer
        Private int_CodigoMedicamento As Integer
        Private int_CodigoSede As Integer
        Private int_Stock As Integer
        Private int_StockMinimo As Integer
        Private int_Cantidad As Integer
        Private int_TipoAccion As Integer
        Private int_CodigoMotivoSalida As Integer
        Private str_Observacion As String
        Private int_UsuarioRegistro As Integer

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

        Public Property CodigoKardex() As Integer
            Get
                Return int_CodigoKardex
            End Get
            Set(ByVal value As Integer)
                int_CodigoKardex = value
            End Set
        End Property

        Public Property CodigoSede() As Integer
            Get
                Return int_CodigoSede
            End Get
            Set(ByVal value As Integer)
                int_CodigoSede = value
            End Set
        End Property

        Public Property Stock() As Integer
            Get
                Return int_Stock
            End Get
            Set(ByVal value As Integer)
                int_Stock = value
            End Set
        End Property

        Public Property StockMinimo() As Integer
            Get
                Return int_StockMinimo
            End Get
            Set(ByVal value As Integer)
                int_StockMinimo = value
            End Set
        End Property

        Public Property Cantidad() As Integer
            Get
                Return int_Cantidad
            End Get
            Set(ByVal value As Integer)
                int_Cantidad = value
            End Set
        End Property

        Public Property TipoAccion() As Integer
            Get
                Return int_TipoAccion
            End Get
            Set(ByVal value As Integer)
                int_TipoAccion = value
            End Set
        End Property

        Public Property CodigoMotivoSalida() As Integer
            Get
                Return int_CodigoMotivoSalida
            End Get
            Set(ByVal value As Integer)
                int_CodigoMotivoSalida = value
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

        Public Property UsuarioRegistro() As Integer
            Get
                Return int_UsuarioRegistro
            End Get
            Set(ByVal value As Integer)
                int_UsuarioRegistro = value
            End Set
        End Property

#End Region

#Region "Constructor"
        Sub New()
            MyBase.new()
        End Sub
        
#End Region

    End Class

End Namespace

