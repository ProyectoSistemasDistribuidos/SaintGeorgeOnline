Namespace ModuloEntrevistas

    Public Class be_ProgramacionEntrevistaDetalleMotivo

#Region "Atributos"

        Private int_CodigoProgramacionEntrevistaCabecera As Integer
        Private int_CodigoDetalleMotivosEntrevistas As Integer
        Private int_CodigoAsignacionSubMotivos As Integer
        Private int_CodigoMotivoEntrevista As Integer
        Private int_CodigoSubMotivoEntrevista As Integer
        Private str_MotivoEntrevista As String
        Private str_SubMotivoEntrevista As String

#End Region

#Region "Propiedades"

        Public Property CodigoProgramacionEntrevistaCabecera() As Integer
            Get
                Return int_CodigoProgramacionEntrevistaCabecera
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionEntrevistaCabecera = value
            End Set
        End Property
        Public Property CodigoDetalleMotivosEntrevistas() As Integer
            Get
                Return int_CodigoDetalleMotivosEntrevistas
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetalleMotivosEntrevistas = value
            End Set
        End Property
        Public Property CodigoAsignacionSubMotivos() As Integer
            Get
                Return int_CodigoAsignacionSubMotivos
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionSubMotivos = value
            End Set
        End Property
        Public Property CodigoMotivoEntrevista() As Integer
            Get
                Return int_CodigoMotivoEntrevista
            End Get
            Set(ByVal value As Integer)
                int_CodigoMotivoEntrevista = value
            End Set
        End Property
        Public Property CodigoSubMotivoEntrevista() As Integer
            Get
                Return int_CodigoSubMotivoEntrevista
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubMotivoEntrevista = value
            End Set
        End Property
        Public Property MotivoEntrevista() As String
            Get
                Return str_MotivoEntrevista
            End Get
            Set(ByVal value As String)
                str_MotivoEntrevista = value
            End Set
        End Property
        Public Property SubMotivoEntrevista() As String
            Get
                Return str_SubMotivoEntrevista
            End Get
            Set(ByVal value As String)
                str_SubMotivoEntrevista = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoProgramacionEntrevistaCabecera As Integer, _
                ByVal CodigoDetalleMotivosEntrevistas As Integer, _
                ByVal CodigoAsignacionSubMotivos As Integer, _
                ByVal CodigoMotivoEntrevista As Integer, _
                ByVal CodigoSubMotivoEntrevista As Integer, _
                ByVal MotivoEntrevista As String, _
                ByVal SubMotivoEntrevista As String)

            int_CodigoProgramacionEntrevistaCabecera = CodigoProgramacionEntrevistaCabecera
            int_CodigoDetalleMotivosEntrevistas = CodigoDetalleMotivosEntrevistas
            int_CodigoAsignacionSubMotivos = CodigoAsignacionSubMotivos
            int_CodigoMotivoEntrevista = CodigoMotivoEntrevista
            int_CodigoSubMotivoEntrevista = CodigoSubMotivoEntrevista

            str_MotivoEntrevista = MotivoEntrevista
            str_SubMotivoEntrevista = SubMotivoEntrevista

        End Sub

#End Region

    End Class

End Namespace