Public Class be_RegistroNotasAnualCualitativo
    Public int_CodigoAsignacionGrupo As Integer?
    Public int_P_CodigoAnioAcademico As Integer?
    Public str_CodigoAlumno As String
    Public NotaAnual As String
    'CU_USP_INS_RegistroNotasAnualCualitativo
    Sub New()
        MyBase.New()
        Me.int_CodigoAsignacionGrupo = Nothing
        Me.int_P_CodigoAnioAcademico = Nothing
        Me.str_CodigoAlumno = String.Empty
        Me.NotaAnual = String.Empty

    End Sub
    
End Class
