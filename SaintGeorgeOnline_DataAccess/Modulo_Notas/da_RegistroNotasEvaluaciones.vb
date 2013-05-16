Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities

Namespace ModuloNotas

    Public Class da_RegistroNotasEvaluaciones
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

        Public Function FUN_UPD_CU_RegistroNotasEvaluaciones(ByVal obe_RegistroNotasEvaluaciones As be_RegistroNotasEvaluaciones, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim p_ValorNotaCriterio As Integer = 0
                Dim p_ValorNotaBimestral As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UPD_RegistroNotasEvaluaciones")

                dbBase.AddInParameter(cmd, "@p_CodigoRegistroNotaEvaluacion", DbType.Int32, obe_RegistroNotasEvaluaciones.CodigoRegistroNotasEvaluaciones)
                dbBase.AddInParameter(cmd, "@p_NotaEvaluacion", DbType.String, IIf(obe_RegistroNotasEvaluaciones.NotaEvaluacion.ToString.Length > 0, obe_RegistroNotasEvaluaciones.NotaEvaluacion.ToString, DBNull.Value))

                dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                dbBase.AddOutParameter(cmd, "@p_ValorNotaCriterio", DbType.Int32, 255)
                dbBase.AddOutParameter(cmd, "@p_ValorNotaBimestral", DbType.Int32, 255)

                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd, tran)

                str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
                p_ValorNotaCriterio = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_ValorNotaCriterio")))
                p_ValorNotaBimestral = Decimal.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_ValorNotaBimestral")))

                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
                lstResultado.Add(p_ValorNotaCriterio.ToString())
                lstResultado.Add(p_ValorNotaBimestral.ToString())

                If Not p_Valor >= 0 Then
                    Rollback()
                    Return lstResultado
                End If

                Commit()
                Return lstResultado
            
            Catch ex As Exception
                lstResultado.Add("Ocurrio un error durante el registro.")
                lstResultado.Add(0)
                lstResultado.Add(0)
                lstResultado.Add(0)
                Rollback()
                Return lstResultado
            Finally
                Conexion.Close()
            End Try
        End Function

        Public Function FUN_UPD_CU_RecalcularNotasEvaluaciones(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0

                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UPD_RecalcularRegistroNotasCuantitativos")

                dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int32, int_CodigoAsignacionGrupo)
                dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)
                dbBase.AddInParameter(cmd, "@p_CodigoTrabajador", DbType.Int32, int_CodigoTrabajador)

                dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)

                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd, tran)

                str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))

                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())

                If Not p_Valor >= 0 Then
                    Rollback()
                    Return lstResultado
                End If

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

        Public Function FUN_UPD_CU_RegistroListaNotasEvaluaciones(ByVal arr_NotasEvaluaciones As List(Of be_RegistroNotasEvaluaciones), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)

            Try

                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim p_ValorNotaCriterio As Integer = 0
                Dim p_ValorNotaBimestral As Integer = 0

                For i As Integer = 0 To arr_NotasEvaluaciones.Count - 1
                    Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UPD_RegistroNotasEvaluaciones")

                    dbBase.AddInParameter(cmd, "@p_CodigoRegistroNotaEvaluacion", DbType.Int32, arr_NotasEvaluaciones(i).CodigoRegistroNotasEvaluaciones)
                    dbBase.AddInParameter(cmd, "@p_NotaEvaluacion", DbType.String, IIf(arr_NotasEvaluaciones(i).NotaEvaluacion.ToString.Length > 0, arr_NotasEvaluaciones(i).NotaEvaluacion.ToString, DBNull.Value))

                    dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                    dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                    dbBase.AddOutParameter(cmd, "@p_ValorNotaCriterio", DbType.Int32, 255)
                    dbBase.AddOutParameter(cmd, "@p_ValorNotaBimestral", DbType.Int32, 255)

                    dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.ExecuteScalar(cmd)

                    str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
                    p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
                    p_ValorNotaCriterio = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_ValorNotaCriterio")))
                    p_ValorNotaBimestral = Decimal.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_ValorNotaBimestral")))

                    cmd.Parameters.Clear()

                    lstResultado.Add(str_Mensaje & "?" & _
                                     p_Valor.ToString() & "?" & _
                                     arr_NotasEvaluaciones(i).CodigoRegistroNotasEvaluaciones.ToString & "?" & _
                                     arr_NotasEvaluaciones(i).NotaEvaluacion.ToString & "?" & _
                                     p_ValorNotaCriterio.ToString() & "?" & _
                                     p_ValorNotaBimestral.ToString())
                Next

                Return lstResultado

            Catch ex As Exception
                lstResultado.Add("Ocurrio un error durante el registro.")
                lstResultado.Add(0)
                lstResultado.Add(0)
                lstResultado.Add(0)
                Return lstResultado
            End Try
        End Function

        Public Function FUN_UPD_CU_RegistroNotasExamen(ByVal obe_RegistroNotasEvaluaciones As be_RegistroNotasEvaluaciones, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                'Dim p_ValorNotaCriterio As Integer = 0
                'Dim p_ValorNotaBimestral As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UPD_RegistroNotasExamen")

                dbBase.AddInParameter(cmd, "@p_CodigoRegistroBimestral", DbType.Int32, obe_RegistroNotasEvaluaciones.CodigoRegistroBimestral)
                dbBase.AddInParameter(cmd, "@p_NotaExamen", DbType.String, IIf(obe_RegistroNotasEvaluaciones.NotaExamen.ToString.Length > 0, obe_RegistroNotasEvaluaciones.NotaExamen.ToString, DBNull.Value))

                dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)

                'dbBase.AddOutParameter(cmd, "@p_ValorNotaCriterio", DbType.Int32, 255)
                'dbBase.AddOutParameter(cmd, "@p_ValorNotaBimestral", DbType.Int32, 255)

                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd, tran)

                str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
                'p_ValorNotaCriterio = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_ValorNotaCriterio")))
                'p_ValorNotaBimestral = Decimal.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_ValorNotaBimestral")))

                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())

                'lstResultado.Add(p_ValorNotaCriterio.ToString())
                'lstResultado.Add(p_ValorNotaBimestral.ToString())

                If Not p_Valor >= 0 Then
                    Rollback()
                    Return lstResultado
                End If

                Commit()
                Return lstResultado

            Catch ex As Exception
                lstResultado.Add("Ocurrio un error durante el registro.")
                lstResultado.Add(0)
                'lstResultado.Add(0)
                'lstResultado.Add(0)
                Rollback()
                Return lstResultado
            Finally
                Conexion.Close()
            End Try
        End Function

        Public Function FUN_UPD_CU_RegistroListaNotasExamenes(ByVal arr_NotasExamenes As List(Of be_RegistroNotasEvaluaciones), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)

            Try

                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                'Dim p_ValorNotaCriterio As Integer = 0
                'Dim p_ValorNotaBimestral As Integer = 0

                For i As Integer = 0 To arr_NotasExamenes.Count - 1
                    Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UPD_RegistroNotasExamen")

                    dbBase.AddInParameter(cmd, "@p_CodigoRegistroBimestral", DbType.Int32, arr_NotasExamenes(i).CodigoRegistroBimestral)
                    dbBase.AddInParameter(cmd, "@p_NotaExamen", DbType.String, IIf(arr_NotasExamenes(i).NotaExamen.ToString.Length > 0, arr_NotasExamenes(i).NotaExamen.ToString, DBNull.Value))

                    dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                    dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                    'dbBase.AddOutParameter(cmd, "@p_ValorNotaCriterio", DbType.Int32, 255)
                    'dbBase.AddOutParameter(cmd, "@p_ValorNotaBimestral", DbType.Int32, 255)

                    dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.ExecuteScalar(cmd)

                    str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
                    p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
                    'p_ValorNotaCriterio = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_ValorNotaCriterio")))
                    'p_ValorNotaBimestral = Decimal.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_ValorNotaBimestral")))

                    cmd.Parameters.Clear()

                    lstResultado.Add(str_Mensaje & "?" & _
                                     p_Valor.ToString() & "?" & _
                                     arr_NotasExamenes(i).CodigoRegistroBimestral.ToString & "?" & _
                                     arr_NotasExamenes(i).NotaExamen.ToString) ' & "?" & _
                    'p_ValorNotaCriterio.ToString() & "?" & _
                    'p_ValorNotaBimestral.ToString())
                Next

                Return lstResultado

            Catch ex As Exception
                lstResultado.Add("Ocurrio un error durante el registro.")
                lstResultado.Add(0)
                'lstResultado.Add(0)
                'lstResultado.Add(0)
                Return lstResultado
            End Try
        End Function



#End Region

#Region "Metodos No Transaccionales"

#End Region

    End Class

End Namespace
