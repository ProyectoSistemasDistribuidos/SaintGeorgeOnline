Namespace ModuloEntrevistas

    Public Class be_ProgramacionEntrevistaDetalle

#Region "Atributos"

        Private int_CodigoProgramacionEntrevistaCabecera As Integer
        Private int_CodigoProgramacionEntrevistaDetalle As Integer
        Private int_CodigoTrabajadorParticipante As Integer
        Public estado As Boolean
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
        Public Property CodigoProgramacionEntrevistaDetalle() As Integer
            Get
                Return int_CodigoProgramacionEntrevistaDetalle
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionEntrevistaDetalle = value
            End Set
        End Property
        Public Property CodigoTrabajadorParticipante() As Integer
            Get
                Return int_CodigoTrabajadorParticipante
            End Get
            Set(ByVal value As Integer)
                int_CodigoTrabajadorParticipante = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoProgramacionEntrevistaCabecera As Integer, _
                ByVal CodigoProgramacionEntrevistaDetalle As Integer, _
                ByVal CodigoTrabajadorParticipante As Integer)

            int_CodigoProgramacionEntrevistaCabecera = CodigoProgramacionEntrevistaCabecera
            int_CodigoProgramacionEntrevistaDetalle = CodigoProgramacionEntrevistaDetalle
            int_CodigoTrabajadorParticipante = CodigoTrabajadorParticipante

        End Sub

#End Region

    End Class

End Namespace