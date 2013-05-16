Namespace ModuloNotas
    Public Class be_AlumnoNotaCuantitativa

        Public CodigoRegistroBimestral As String
        Public CodigoAsignacionGrupo As String
        Public Href As String
        Public anioAcademico As String

        Public CodigoBimestre As String
        Public NombreCompletoAlumno As String
        Public CodigoAlumno As String
        Public lstNotaCriterio As New List(Of be_NotaCriterio)
        Public ObservacionCurso As String
        Public Foto As String
        Public Promedio As String
        Public NotaExamen As String
        Public verNotaExamen As Integer

        Public estadoAlumno As Integer
        Public estaExonerado As Integer

    End Class
End Namespace
