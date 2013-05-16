Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio

Namespace ModuloColegio

    Public Class da_AsignacionIniciosBimestres
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

        Public Function FUN_INS_AsignacionIniciosBimestres(ByVal objAsignacionIniciosBimestres As be_AsignacionIniciosBimestres, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Try

                dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_INS_AsignacionIniciosBimestres")
                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int16, objAsignacionIniciosBimestres.CodigoAnioAcademico)
                dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int16, objAsignacionIniciosBimestres.CodigoGrado)
                dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, objAsignacionIniciosBimestres.CodigoBimestre)
                dbBase.AddInParameter(dbCommand, "@p_FechaInicio", DbType.DateTime, objAsignacionIniciosBimestres.FecInicio)
                dbBase.AddInParameter(dbCommand, "@p_FechaFin", DbType.DateTime, objAsignacionIniciosBimestres.FecFin)

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

            Catch ex As Exception
                str_Mensaje = "Ocurrió un error, intente su operación otra vez."
                Return -1
            End Try

        End Function

        Public Function FUN_DEL_AsignacionIniciosBimestres(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_DEL_AsignacionIniciosBimestres")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, int_Codigo)

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

        Public Function FUN_LIS_AsignacionIniciosBimestres(ByVal int_CodigoAnioAcademico As Integer, _
                                                ByVal int_CodigoNivel As Integer, _
                                                ByVal int_CodigoSubNiveles As Integer, _
                                                ByVal int_CodigoGrado As Integer, _
                                                ByVal int_CodigoBimestre As Integer, _
                                                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionIniciosBimestres")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoNivel", DbType.Int16, int_CodigoNivel)
            dbBase.AddInParameter(cmd, "@p_CodigoSubNivel", DbType.Int16, int_CodigoSubNiveles)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

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