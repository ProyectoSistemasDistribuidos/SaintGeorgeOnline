Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloAsistenciaAlumnos

Namespace ModuloAsistenciaAlumnos

    Public Class da_AsistenciaAlumnos

        Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"

        Private dbBase As SqlDatabase 'ExecuteDataSet
        Private dbCommand As DbCommand 'ExecuteScalar

#End Region

#Region "Constructor"

        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
        End Sub

#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_INS_AsistenciaAlumnos(ByVal objAsistenciaAlumnos As be_AsistenciaAlumnos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("AS_USP_INS_AsistenciaAlumnos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objAsistenciaAlumnos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroAsistenciaAlumnos", DbType.Int32, objAsistenciaAlumnos.CodigoRegistroAsistencia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEventoAsistencia", DbType.Int16, objAsistenciaAlumnos.CodigoEventoAsistencia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAula", DbType.Int16, objAsistenciaAlumnos.CodigoAula)
            dbBase.AddInParameter(dbCommand, "@p_FechaAsistencia", DbType.DateTime, objAsistenciaAlumnos.FechaAsistencia)
            dbBase.AddInParameter(dbCommand, "@p_ObservacionTutor", DbType.String, objAsistenciaAlumnos.ObservacionTutor)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_INS_AsistenciaJustificacion(ByVal objAsistenciaAlumnos As be_AsistenciaAlumnos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("AS_USP_INS_AsistenciaJustificacion")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objAsistenciaAlumnos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroAsistenciaAlumnos", DbType.Int32, objAsistenciaAlumnos.CodigoRegistroAsistencia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEventoAsistencia", DbType.Int16, objAsistenciaAlumnos.CodigoEventoAsistencia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMedioUso", DbType.Int16, objAsistenciaAlumnos.CodigoMedioUso)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMotivo", DbType.Int16, objAsistenciaAlumnos.CodigoMotivo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAula", DbType.Int16, objAsistenciaAlumnos.CodigoAula)
            dbBase.AddInParameter(dbCommand, "@p_FechaJustifica", DbType.DateTime, objAsistenciaAlumnos.FechaJustificacion)
            dbBase.AddInParameter(dbCommand, "@p_ObservacionTutor", DbType.String, objAsistenciaAlumnos.ObservacionTutor)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

#End Region

#Region "Metodos No Transaccionales"
        Public Function FUN_LIS_AlumnosXSalon(ByVal int_AnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal dt_FechaAsistencia As Date, _
                                       ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AS_USP_LIS_AlumnosXSalon")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_FechaAsistencia", DbType.DateTime, dt_FechaAsistencia)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_BimestreXAño(ByVal int_AnioAcademico As Integer, _
                                      ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AS_USP_LIS_BimestreXAño")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

    End Class

End Namespace