Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities

Namespace ModuloNotas

    Public Class da_RegistroNotasCriterios
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

        Public Function FUN_INS_CU_RegistroNotasCriterios(ByVal dt As DataTable, ByVal lstCriterios As List(Of be_RegistroCriteriosEST), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim dbCommand As DbCommand
                Dim str_Mensaje As String = ""
                Dim p_Valor1 = 0, p_Valor2 As Integer = 0

                For Each dr As DataRow In dt.Rows
                    For Each obe_Criterio As be_RegistroCriteriosEST In lstCriterios

                        dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotasCriterios")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroBimestralCuantitativo", DbType.Int32, Convert.ToInt32(dr("CodigoRegistroBimestralCuantitativo").ToString()))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroCriterio", DbType.Int32, obe_Criterio.idRegistro)
                        dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                        dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                        p_Valor1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                        If Not p_Valor1 >= 0 Then
                            Rollback()
                            Return lstResultado
                        End If

                        For Each obe_Evaluacion As be_RegistroEvaluacionesEST In obe_Criterio.listaEvaluaciones
                            dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotasEvaluaciones")
                            dbCommand.Parameters.Clear()
                            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroNotaCriterio", DbType.Int32, p_Valor1)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroEvaluacion", DbType.Int32, obe_Evaluacion.idRegistro)
                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                            dbBase.ExecuteScalar(dbCommand, tran)
                            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                            p_Valor2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                            dbCommand.Parameters.Clear()

                            If Not p_Valor2 >= 0 Then
                                Rollback()
                                Return lstResultado
                            End If

                        Next
                    Next
                Next

                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor2.ToString())
                Commit()
                Return lstResultado

            Catch ex As Exception
                lstResultado.Add("Ocurrio un error durante el registro.")
                lstResultado.Add(0)
                Rollback()
                Return lstResultado
            Finally
                Conexion.Close()
            End Try
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_CU_RegistroNotasCriteriosyEvaluaciones( ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_RegistroNotasCriteriosyEvaluaciones")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int32, int_CodigoAsignacionGrupo)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_LIS_CU_RegistroNotasCriteriosyEvaluacionesSecundaria(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_curso As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Try

                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_RegistroNotasCriteriosyEvaluacionesLibretaSecundaria")
                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int32, int_CodigoAsignacionGrupo)
                dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)
                dbBase.AddInParameter(cmd, "@p_curso", DbType.Int16, int_curso)

                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)


                '@p_CodigoAsignacionGrupo int,                             
                '@p_CodigoBimestre int,
                '@p_curso int,                
                '@p_CodigoUsuario INT,                        
                '@p_CodigoTipoUsuario INT,                        
                '@p_CodigoModulo INT,                        
                '@p_CodigoOpcion INT  


                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)
            Catch ex As Exception

            Finally


            End Try
           

        End Function

        'CU_USP_LIS_RegistroNotasCriteriosyEvaluacionesLibretaSecundaria


        '@p_CodigoAsignacionGrupo int,                             
        '@p_CodigoBimestre int,
        '@p_curso int,                
        '@p_CodigoUsuario INT,                        
        '@p_CodigoTipoUsuario INT,                        
        '@p_CodigoModulo INT,                        
        '@p_CodigoOpcion INT  

#End Region

    End Class

End Namespace
