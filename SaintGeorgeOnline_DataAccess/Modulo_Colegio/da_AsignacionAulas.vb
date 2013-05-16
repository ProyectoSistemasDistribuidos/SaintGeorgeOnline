Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio

Namespace ModuloColegio

    Public Class da_AsignacionAulas
        Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"

        Private dbBase As SqlDatabase 'ExecuteDataSet
        Private dbCommand As DbCommand 'ExecuteScalar

        Private cnn As DbConnection
        Private tran As DbTransaction
#End Region

#Region "Constructor"

        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
            cnn = Me.dbBase.CreateConnection()
        End Sub

#End Region

#Region "Propiedades"

        Public ReadOnly Property BaseDatos() As SqlDatabase
            Get
                Return Me.dbBase
            End Get
        End Property

        Public ReadOnly Property Transaccion() As DbTransaction
            Get
                Return Me.tran
            End Get
        End Property

        Public ReadOnly Property Conexion() As DbConnection
            Get
                Return Me.cnn
            End Get
        End Property

#End Region

#Region "Metodos"

        Public Sub BeginTransaction()

            If Not (cnn.State = ConnectionState.Open) Then
                cnn.Open()
            End If

            tran = cnn.BeginTransaction(IsolationLevel.Serializable)

        End Sub

        Public Sub Rollback()

            tran.Rollback()

        End Sub

        Public Sub Commit()

            tran.Commit()

        End Sub

#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_INS_AsignacionAulas(ByVal objAsignacionAulas As be_AsignacionAulas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_INS_AsignacionAulas")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAula", DbType.Int32, objAsignacionAulas.CodigoAula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int16, objAsignacionAulas.CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoSede", DbType.Int16, objAsignacionAulas.CodigoSede)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaTutor", DbType.Int32, objAsignacionAulas.CodigoPersonaTutor)
            dbBase.AddInParameter(dbCommand, "@p_CapacidadAlumno", DbType.Int16, objAsignacionAulas.CapacidadAlumnos)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAmbiente", DbType.Int32, objAsignacionAulas.CodigoAmbiente)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaResponsableActa", DbType.Int32, objAsignacionAulas.CodigoPersonaResponsableActa)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaResponsableSalon", DbType.Int32, objAsignacionAulas.CodigoPersonaResponsableSalon)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAulaMinisterio", DbType.Int16, objAsignacionAulas.CodigoAulaMinisterio)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_UPD_AsignacionAulas(ByVal objAsignacionAulas As be_AsignacionAulas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_UPD_AsignacionAulas")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, objAsignacionAulas.Codigo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAula", DbType.Int32, objAsignacionAulas.CodigoAula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int16, objAsignacionAulas.CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoSede", DbType.Int16, objAsignacionAulas.CodigoSede)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaTutor", DbType.Int32, objAsignacionAulas.CodigoPersonaTutor)
            dbBase.AddInParameter(dbCommand, "@p_CapacidadAlumno", DbType.Int16, objAsignacionAulas.CapacidadAlumnos)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAmbiente", DbType.Int32, objAsignacionAulas.CodigoAmbiente)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaResponsableActa", DbType.Int32, objAsignacionAulas.CodigoPersonaResponsableActa)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaResponsableSalon", DbType.Int32, objAsignacionAulas.CodigoPersonaResponsableSalon)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAulaMinisterio", DbType.Int16, objAsignacionAulas.CodigoAulaMinisterio)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_DEL_AsignacionAulas(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_DEL_AsignacionAulas")
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


        Public Function FUN_UPD_CierreAulas(ByVal dt_Lista As DataTable, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0

            Try

                'Inicio la transaccion
                BeginTransaction()

                For Each dr As DataRow In dt_Lista.Rows

                    dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_UPD_CierreAula")

                    'Parámetros de entrada
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int32, dr.Item("CodigoAsignacionaula"))
                    dbBase.AddInParameter(dbCommand, "@p_Bim1", DbType.Int16, dr.Item("EstBim1"))
                    dbBase.AddInParameter(dbCommand, "@p_Bim2", DbType.Int16, dr.Item("EstBim2"))
                    dbBase.AddInParameter(dbCommand, "@p_Bim3", DbType.Int16, dr.Item("EstBim3"))
                    dbBase.AddInParameter(dbCommand, "@p_Bim4", DbType.Int16, dr.Item("EstBim4"))
                    dbBase.AddInParameter(dbCommand, "@p_Con1", DbType.Int16, dr.Item("EstCon1"))
                    dbBase.AddInParameter(dbCommand, "@p_Con2", DbType.Int16, dr.Item("EstCon2"))
                    dbBase.AddInParameter(dbCommand, "@p_Con3", DbType.Int16, dr.Item("EstCon3"))
                    dbBase.AddInParameter(dbCommand, "@p_Con4", DbType.Int16, dr.Item("EstCon4"))

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

                    'Parámetros de salida
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                    dbBase.ExecuteScalar(dbCommand)
                    str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    int_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                    If Not int_Valor > 0 Then
                        Rollback()
                        Return int_Valor
                    End If

                Next

                Commit()
                Return int_Valor
            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function


#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionAulas(ByVal int_AnioAcademico As Integer, ByVal int_Sede As Integer, ByVal int_Aula As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionAulas")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoSede", DbType.Int16, int_Sede)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_Aula)

            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_AsignacionAulasParaGrupos(ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionAulasParaGrupos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_AsignacionAulasParaGruposXNivel( _
            ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoNivel As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionAulasParaGruposXNivel")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoNivel", DbType.Int16, int_CodigoNivel)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function
        Public Function FUN_LIS_AsignacionAulasParaGruposXNivelMinisterio( _
            ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoNivel As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionAulasParaGruposXNivelMinisterio")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoNivel", DbType.Int16, int_CodigoNivel)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_GET_AsignacionAulas(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_GET_AsignacionAulas")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'update
        Public Function FUN_LIS_AsignacionAulasPorAnioAcademico(ByVal int_AnioAcademico As Integer, ByVal int_Sede As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionAulasPorAnioAcademico")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoSede", DbType.Int16, int_Sede)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota( _
            ByVal int_CodigoTrabajador As Integer, ByVal int_TipoNota As Integer, ByVal int_AnioAcademico As Integer, ByVal int_Sede As Integer, ByVal int_Estado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionAulasPorAnioAcademicoTipoNota")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTrabajador", DbType.Int32, int_CodigoTrabajador)
            dbBase.AddInParameter(cmd, "@p_TipoNota", DbType.Int16, int_TipoNota)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoSede", DbType.Int16, int_Sede)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'update 28/02/2012
        Public Function FUN_LIS_AsignacionAulasPorProfesoryAnioAcademico(ByVal str_Usuario As String, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionAulasPorProfesoryAnioAcademico")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.String, str_Usuario)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'update 12/07/2012
        Public Function FUN_LIS_GradosPorProfesoryAnioAcademico(ByVal str_Usuario As String, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_GradosPorProfesoryAnioAcademico  ")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.String, str_Usuario)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'update 19/03/2012
        Public Function FUN_LIS_AsignacionAulasParaCursosPorProfesoryAnioAcademico(ByVal str_Usuario As String, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionAulasParaCursosPorProfesoryAnioAcademico")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.String, str_Usuario)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'update 03/04/2012
        Public Function FUN_LIS_AsignacionAulasGradoPorAnioAcademico(ByVal int_AnioAcademico As Integer, ByVal int_Sede As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionAulasGradoPorAnioAcademico")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoSede", DbType.Int16, int_Sede)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_LIS_AsignacionAulasPorTutor( _
            ByVal int_CodigoTrabajador As Integer, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_AsignacionAulasPorTutor")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTrabajador", DbType.Int32, int_CodigoTrabajador)
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

