Namespace ModuloNotas
    Public Class be_RegistroCriteriosEST

        Public idCorr As Integer
        Public idRegistro As Integer
        Public idCriterio As Integer
        Public estado As Boolean
        Public nombre As String
        Public listaEvaluaciones As New List(Of be_RegistroEvaluacionesEST)

    End Class
End Namespace
