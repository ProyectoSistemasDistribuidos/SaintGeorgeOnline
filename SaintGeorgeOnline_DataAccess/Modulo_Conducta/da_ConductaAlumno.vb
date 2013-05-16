Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConductaAlumnos
'ok
Namespace ModuloConductaAlumnos

    Public Class da_ConductaAlumno

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
        Public Function FUN_INS_RegistroMeritosDemeritos(ByVal objRegistroMeritosDemeritos As be_RegistroMeritosDemeritos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CT_USP_INS_RegistroMeritosDemeritos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoCriterioConducta", DbType.Int32, objRegistroMeritosDemeritos.CodigoCriterioConducta)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajador", DbType.Int32, objRegistroMeritosDemeritos.CodigoTrabajador)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionGrupo", DbType.Int32, objRegistroMeritosDemeritos.CodigoAsignacionGrupo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroConductaBimestral", DbType.Int32, objRegistroMeritosDemeritos.CodigoRegistroConductaBimestral)
            dbBase.AddInParameter(dbCommand, "@p_FechaRegistroMD", DbType.DateTime, objRegistroMeritosDemeritos.FechaRegistroMeritosDemeritos)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_INS_Registro3BlackMarkDemeritos(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoBimestre As Integer, ByVal str_CodigoAlumno As String, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CT_USP_INS_Registro3BlackMarkDemeritos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Parámetros de salida
            'dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            'dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            'str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            'Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
            'Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, 1)))
        End Function

        Public Function FUN_INS_RegistroBlackMark(ByVal objRegistroBlackMark As be_RegistroBlackMark, ByVal int_Bimestre As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoAnioAcademico As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CT_USP_INS_RegistroBlackMark")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajador", DbType.Int32, objRegistroBlackMark.CodigoTrabajador)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int32, int_Bimestre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAula", DbType.Int32, int_CodigoAula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroConductaBimestral", DbType.Int32, objRegistroBlackMark.CodigoRegistroConductaBimestral)
            dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, objRegistroBlackMark.Descripcion)
            dbBase.AddInParameter(dbCommand, "@p_FechaRegistroBM", DbType.DateTime, objRegistroBlackMark.FechaRegistroBlackMark)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionGrupo", DbType.Int32, objRegistroBlackMark.CodigoAsignacionGrupo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_UPD_ConductaPrimaria(ByVal int_CodigoAnioAcademico As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal str_NotaBimestralLetras As String, ByVal int_CodigoAula As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CT_USP_UPD_ConductaPrimaria")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAula", DbType.Int32, int_CodigoAula)
            dbBase.AddInParameter(dbCommand, "@p_NotaBimestralLetras", DbType.String, str_NotaBimestralLetras)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_UPD_RegistroAprobacionDeMeritos(ByVal int_CodigoRegistroMeritosDemeritos As Integer, ByVal int_CodigoEstadoAprobacion As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CT_USP_UPD_RegistroAprobacionDeMeritos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroMeritosDemeritos", DbType.Int32, int_CodigoRegistroMeritosDemeritos)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEstadoAprobacion", DbType.Int32, int_CodigoEstadoAprobacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_UPD_AprobacionDeMeritosTotales(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoTipo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CT_USP_UPD_AprobacionDeMeritosTotales")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAula", DbType.Int32, int_CodigoAula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoCurso", DbType.Int32, int_CodigoCurso)
            dbBase.AddInParameter(dbCommand, "@p_Tipo", DbType.Int32, int_CodigoTipo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

#End Region

#Region "Metodos No Transaccionales"
        Public Function FUN_LIS_AsignacionGruposCursos(ByVal int_CodigoAnioAcademico As Integer, _
                                       ByVal int_CodigoAula As Integer, _
                                       ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_LIS_GruposCursos")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function
        'int_CodigoAnioAcademico, int_CodigoAula, int_CodigoBimestre, int_CodigoCurso, int_CodigoTipo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion
        Public Function FUN_LIS_RegistroConductaBimestral_NotaBimestralCualitativa(ByVal int_CodigoAnioAcademico As Integer, _
                                     ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoTipo As Integer, _
                                     ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_UPD_RegistroConductaBimestral_NotaBimestralCualitativa")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
            dbBase.AddInParameter(cmd, "@p_CodigoCurso", DbType.Int16, int_CodigoCurso)
            'dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, int_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int16, int_CodigoTipo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_RegistroConductaBimestral_NotaBimestralCualitativa(ByVal int_CodigoAnioAcademico As Integer, _
                                  ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoTipo As Integer, _
                                  ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_REP_RegistroConductaBimestral_NotaBimestralCualitativa")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
            dbBase.AddInParameter(cmd, "@p_CodigoCurso", DbType.Int16, int_CodigoCurso)
            'dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, int_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int16, int_CodigoTipo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_MeritDemeritoXAlumnoCurso(ByVal int_CodigoAnioAcademico As Integer, _
                               ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal str_CodigoAlumno As String, _
                               ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_REP_MeritDemertPorAlumnoCurso")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
            'dbBase.AddInParameter(cmd, "@p_CodigoCurso", DbType.Int16, int_CodigoCurso)
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            'dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int16, int_CodigoTipo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_AlumnosXSalonConducta(ByVal int_AnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, _
                                       ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_LIS_AlumnosXSalonConducta")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_AprobacionDEMeritos(ByVal int_AnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoCurso As Integer, ByVal str_CodigoAlumno As String, _
                                  ByVal int_CodigoTipo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_LIS_AprobacionDEMeritos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)
            dbBase.AddInParameter(cmd, "@p_CodigoCurso", DbType.Int32, int_CodigoCurso)
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.String, int_CodigoTipo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_ConductaPrimaria(ByVal int_AnioAcademico As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoAula As Integer, _
                                      ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_LIS_ConductaPrimaria")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_DetalleConducta(ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal int_Anio As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_GET_DetalleConducta")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)
            dbBase.AddInParameter(cmd, "@p_Anio", DbType.Int32, int_Anio)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_Detalle3BlackMark(ByVal int_CodigoRegistroConductual As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_GET_Detalle3BlackMark")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.String, int_CodigoRegistroConductual)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function


#End Region

    End Class

End Namespace

