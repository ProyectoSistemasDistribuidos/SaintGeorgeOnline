Public Class BE_CU_HomologacionCursosMinisterio
    Public HCM_CodigoHomologacionCursos As Integer?
    Public CM_CodigoCursoMinisterio As Integer?
    Public ACA_CodigoAsignacionCurso As Integer?
    Public HCM_CantidadDescriptores As Integer?

    Public HCM_Estado As Boolean?
    Public HCM_OrdenActa As Integer?
    Public ACAN_CodigoAsignacionCursoActaNiveles As Integer

    Public Sub New()
        Me.HCM_CodigoHomologacionCursos = Nothing
        Me.CM_CodigoCursoMinisterio = Nothing
        Me.ACA_CodigoAsignacionCurso = Nothing
        Me.HCM_CantidadDescriptores = Nothing

        Me.HCM_Estado = Nothing
        Me.HCM_OrdenActa = Nothing
        Me.ACAN_CodigoAsignacionCursoActaNiveles = Nothing
    End Sub
End Class
