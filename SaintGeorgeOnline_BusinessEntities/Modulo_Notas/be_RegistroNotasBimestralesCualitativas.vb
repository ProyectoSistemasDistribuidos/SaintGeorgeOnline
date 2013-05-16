Public Class be_RegistroNotasBimestralesCualitativas

    Public int_CodigoBimestre As Integer?
    Public int_CodigoRegistroAnualL As Integer?
    Public p_NotaFinalBimestre As String
    Public p_ObservacionCurso As String


    Sub New()
        MyBase.New()
        Me.int_CodigoBimestre = Nothing
        Me.int_CodigoRegistroAnualL = Nothing
        Me.p_ObservacionCurso = String.Empty

        Me.p_NotaFinalBimestre = String.Empty
    End Sub

End Class
