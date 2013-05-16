Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class bl_progresoSeguimiento

    Private obj_da_progresoSeguimiento As da_progresoSeguimiento

    Public Sub New()
        obj_da_progresoSeguimiento = New da_progresoSeguimiento
    End Sub

#Region "no transaccional "

    Function ListarRendimientoProgresoEstudiante() As DataTable
        Try
            Return New da_progresoSeguimiento().ListarRendimientoProgresoEstudiante()
        Catch ex As Exception

        End Try

    End Function

    Function ListarSG_ActitudProgresoEstudiante() As DataTable
        Try
            Return New da_progresoSeguimiento().ListarSG_ActitudProgresoEstudiante()

        Catch ex As Exception

        End Try

    End Function

    Function ListarListarJaladosCursos(ByVal codGrupo As Integer, ByVal codBimestre As Integer, ByVal nota As Integer) As DataTable
        Try

            Return New da_progresoSeguimiento().ListarListarJaladosCursos(codGrupo, codBimestre, nota)
        Catch ex As Exception

        End Try
    End Function

    Public Function FUN_LIS_AlumnosJaladosPorCurso( _
        ByVal int_codAsigGrupo As Integer, ByVal int_codBimestre As Integer, ByVal int_nota As Integer, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        Return obj_da_progresoSeguimiento.FUN_LIS_AlumnosJaladosPorCurso(int_codAsigGrupo, int_codBimestre, int_nota, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

    End Function




    Function ListarListarEstrutura(ByVal codBimestre As Integer, ByVal codAlumno As Integer, ByVal codCurso As Integer) As DataSet
        Try
            Return New da_progresoSeguimiento().ListarListarEstrutura(codBimestre, codAlumno, codCurso)
        Catch ex As Exception

        End Try
    End Function

    Public Function FUN_REP_REP_StudentSubjectProgress( _
        ByVal int_CodigoBimestre As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoAsignacionCurso As Integer, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        Return obj_da_progresoSeguimiento.FUN_REP_REP_StudentSubjectProgress( _
            int_CodigoBimestre, str_CodigoAlumno, int_CodigoAsignacionCurso, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

    End Function



#End Region
#Region "transaccional"

    Function INS_SG_CabeceraProgresoEstudiante(ByVal obe_SG_CabeceraProgresoEstudiante As be_SG_CabeceraProgresoEstudiante) As Integer
        Try

            Return New da_progresoSeguimiento().INS_SG_CabeceraProgresoEstudiante(obe_SG_CabeceraProgresoEstudiante)
        Catch ex As Exception

        End Try

    End Function

    Function InsSG_DetalleProgresoEstudiante(ByVal lstbe_SG_DetalleProgresoEstudiante As List(Of be_SG_DetalleProgresoEstudiante)) As Integer
        Try
            Return New da_progresoSeguimiento().InsSG_DetalleProgresoEstudiante(lstbe_SG_DetalleProgresoEstudiante)
        Catch ex As Exception

        End Try
    End Function
    Function listarProgresoEstudiante(ByVal AL_CodigoAlumno As Integer, ByVal codBimestre As Integer, ByVal codCurso As Integer) As DataSet
        Try
            Return New da_progresoSeguimiento().listarProgresoEstudiante(AL_CodigoAlumno, codBimestre, codCurso)
        Catch ex As Exception

        End Try

    End Function

    Function Act_SG_DetalleProgresoEstudiante(ByVal lstbe_SG_DetalleProgresoEstudiante As List(Of be_SG_DetalleProgresoEstudiante), ByVal obe_SG_CabeceraProgresoEstudiante As be_SG_CabeceraProgresoEstudiante) As Integer
        Try
            Return New da_progresoSeguimiento().Act_SG_DetalleProgresoEstudiante(lstbe_SG_DetalleProgresoEstudiante, obe_SG_CabeceraProgresoEstudiante)
        Catch ex As Exception

        End Try

    End Function

#End Region
End Class
