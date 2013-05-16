Namespace ModuloEntrevistas

    Public Class be_ProgramacionEntrevistaDetalleAsistentes

#Region "Atributos"

        Private int_CodigoAsistenteFamiliaEntrevista As Integer
        Private int_CodigoProgramacionEntrevistaCabecera As Integer
        Private int_CodigoAsistenteFamilia As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAsistenteFamiliaEntrevista() As Integer
            Get
                Return int_CodigoAsistenteFamiliaEntrevista
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsistenteFamiliaEntrevista = value
            End Set
        End Property
        Public Property CodigoProgramacionEntrevistaCabecera() As Integer
            Get
                Return int_CodigoProgramacionEntrevistaCabecera
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionEntrevistaCabecera = value
            End Set
        End Property
        Public Property CodigoAsistenteFamilia() As Integer
            Get
                Return int_CodigoAsistenteFamilia
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsistenteFamilia = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoAsistenteFamiliaEntrevista As Integer, _
                ByVal CodigoProgramacionEntrevistaCabecera As Integer, _
                ByVal CodigoAsistenteFamilia As Integer)

            int_CodigoAsistenteFamiliaEntrevista = CodigoAsistenteFamiliaEntrevista
            int_CodigoProgramacionEntrevistaCabecera = CodigoProgramacionEntrevistaCabecera
            int_CodigoAsistenteFamilia = CodigoAsistenteFamilia

        End Sub

#End Region

    End Class

End Namespace