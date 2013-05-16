Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones

Namespace ModuloPensiones

    Public Class da_AsignacionBecas
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

        Public Function FUN_INS_AsignacionBecas(ByVal objAsignacionBecas As be_AsignacionBecas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Try

                dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_AsignacionBecas")
                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objAsignacionBecas.CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@p_CodigoMotivoBeca", DbType.Int32, objAsignacionBecas.CodigoMotivoBeca)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoBeca", DbType.Int32, objAsignacionBecas.CodigoTipoBeca)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, objAsignacionBecas.CodigoAnioAcademico)
                dbBase.AddInParameter(dbCommand, "@p_MesIni", DbType.Int32, objAsignacionBecas.MesIni)
                dbBase.AddInParameter(dbCommand, "@p_MesFin", DbType.Int32, objAsignacionBecas.MesFin)
                dbBase.AddInParameter(dbCommand, "@p_CodigoExpediente", DbType.Int32, objAsignacionBecas.NumExp)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Catch ex As Exception
                str_Mensaje = "Ocurrió un error, intente su operación otra vez."
                Return -1
            End Try

        End Function

        Public Function FUN_UPD_AsignacionBecas(ByVal objAsignacionBecas As be_AsignacionBecas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_AsignacionBecas")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, objAsignacionBecas.CodigoAsignacionBeca)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objAsignacionBecas.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMotivoBeca", DbType.Int32, objAsignacionBecas.CodigoMotivoBeca)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoBeca", DbType.Int32, objAsignacionBecas.CodigoTipoBeca)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, objAsignacionBecas.CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_MesIni", DbType.Int32, objAsignacionBecas.MesIni)
            dbBase.AddInParameter(dbCommand, "@p_MesFin", DbType.Int32, objAsignacionBecas.MesFin)
            dbBase.AddInParameter(dbCommand, "@p_CodigoExpediente", DbType.Int32, objAsignacionBecas.NumExp)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_DEL_AsignacionBecas(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_DEL_AsignacionBecas")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
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

        Public Function FUN_LIS_AlumnoPrueba(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_Alumnos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_AsignacionBecas(ByVal int_CodigoAlumno As Integer, _
                                                ByVal int_CodigoMotivoBeca As Integer, _
                                                ByVal int_CodigoTipoBeca As Integer, _
                                                ByVal int_CodigoAnioAcademico As Integer, _
                                                ByVal int_Grado As Integer, _
                                                ByVal int_Estado As Integer, _
                                                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_AsignacionBecas")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, int_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoMotivoBeca", DbType.Int32, int_CodigoMotivoBeca)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoBeca", DbType.Int32, int_CodigoTipoBeca)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_Grado", DbType.Int32, int_Grado)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int32, int_Estado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_AsignacionBecas(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_GET_AsignacionBecas")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int32, int_Codigo)

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