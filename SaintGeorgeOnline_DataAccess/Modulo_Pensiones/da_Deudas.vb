Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones

Namespace ModuloPensiones

    Public Class da_Deudas
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

        Public Function FUN_INS_Deudas(ByVal objDeudas As be_Deudas, ByVal int_CodigoPostulante As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_Deudas")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objDeudas.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPeriodo", DbType.Int32, objDeudas.CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoConcepto", DbType.Int32, objDeudas.CodigoConceptoCobro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMes", DbType.Int32, objDeudas.Mes)
            dbBase.AddInParameter(dbCommand, "@p_FechaVencimiento", DbType.Date, objDeudas.FechaVencimiento)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objDeudas.CodigoMoneda)
            dbBase.AddInParameter(dbCommand, "@p_Monto", DbType.Decimal, objDeudas.MontoTotal)
            dbBase.AddInParameter(dbCommand, "@p_MontoBono", DbType.Decimal, objDeudas.MontoBono)

            dbBase.AddInParameter(dbCommand, "@p_CodigoPostulante", DbType.Int32, IIf(int_CodigoPostulante = 0, DBNull.Value, int_CodigoPostulante))

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

        Public Function FUN_UPD_Deudas(ByVal objDeudas As be_Deudas, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_Deudas")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, objDeudas.CodigoDeuda)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objDeudas.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_FechaVencimiento", DbType.Date, objDeudas.FechaVencimiento)

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

        Public Function FUN_DEL_Deudas(ByVal objDeudas As be_Deudas, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_DEL_Deudas")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, objDeudas.CodigoDeuda)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objDeudas.CodigoAlumno)

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

        Public Function FUN_DEL_ListaDeudas(ByVal str_CodigoDeudas As String, ByVal str_CodigoAlumno As String, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_DEL_ListaDeudas")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.String, str_CodigoDeudas)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)

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


        Public Function FUN_INS_DeudasDeCalendarioPorAlumno(ByVal objDetalle As DataSet, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_TotalRegistros As Integer = 0

            Try

                'Inicio la transaccion
                BeginTransaction()

                For Each dr As DataRow In objDetalle.Tables(0).Rows

                    dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_DeudasAlumnoPorCalendario")

                    'Parámetros de entrada                
                    dbBase.AddInParameter(dbCommand, "@p_CodigoPeriodo", DbType.Int32, dr.Item("CodigoAnioAcademico"))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int32, dr.Item("CodigoGrado"))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, dr.Item("CodigoAlumno"))
                    dbBase.AddInParameter(dbCommand, "@p_ListaMeses", DbType.String, dr.Item("ListaMesesDeudas"))

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                    'Parámetros de salida
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                    'Ejecucion del Store Procedure
                    dbBase.ExecuteScalar(dbCommand, tran)
                    str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))


                    If Not int_Valor >= 0 Then ' Si ocurre algun error : int_Valor (-1)
                        Rollback()
                        Return int_Valor
                    Else
                        int_TotalRegistros += 1
                    End If

                Next

                Commit()
                Return int_TotalRegistros 'int_Valor

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function


        Public Function FUN_INS_DeudasPorServicio(ByVal objDetalle As DataSet, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_TotalRegistros As Integer = 0
            Try
                'Inicio la transaccion
                BeginTransaction()
                For Each dr As DataRow In objDetalle.Tables(0).Rows
                    dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_DeudasPorServicio")

                    'Parámetros de entrada                
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, dr.Item("codigoalumno"))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoPeriodo", DbType.Int32, dr.Item("codigoanio"))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoConcepto", DbType.Int32, dr.Item("codigoconcepto"))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoMes", DbType.Int32, dr.Item("codigomes"))
                    dbBase.AddInParameter(dbCommand, "@p_FechaVencimiento", DbType.Date, dr.Item("fecven"))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, dr.Item("codigomoneda"))
                    dbBase.AddInParameter(dbCommand, "@p_Monto", DbType.Decimal, dr.Item("monto"))
                    dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, dr.Item("cuota"))

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                    'Parámetros de salida
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                    'Ejecucion del Store Procedure
                    dbBase.ExecuteScalar(dbCommand, tran)
                    str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                    If Not int_Valor >= 0 Then ' Si ocurre algun error : int_Valor (-1)
                        Rollback()
                        Return int_Valor
                    Else
                        int_TotalRegistros += 1
                    End If
                Next
                Commit()
                Return int_TotalRegistros 'int_Valor

            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0
            Finally
                Conexion.Close()
            End Try

        End Function


#Region "Bonos"

        Public Function fun_ins_exportacionbonos(ByVal objdetalle As DataTable, ByRef str_mensaje As String, _
            ByVal int_codigousuario As Integer, ByVal int_codigotipousuario As Integer, ByVal int_codigomodulo As Integer, ByVal int_codigoopcion As Integer) As List(Of String)

            Dim int_valor As Integer = 0
            Dim int_totalregistros As Integer = 0

            Dim lstRegistro As New List(Of String)

            Try
                'inicio la transaccion
                BeginTransaction()
                For Each dr As DataRow In objdetalle.Rows
                    dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_Bono")

                    'parámetros de entrada   
                    dbBase.AddInParameter(dbCommand, "@p_periodo", DbType.Int32, dr.Item("nperiodo"))
                    dbBase.AddInParameter(dbCommand, "@p_grado", DbType.String, dr.Item("ngrado"))
                    dbBase.AddInParameter(dbCommand, "@p_codigoalumno", DbType.String, dr.Item("calumno"))
                    dbBase.AddInParameter(dbCommand, "@p_nombrealumno", DbType.String, dr.Item("nalumno"))
                    dbBase.AddInParameter(dbCommand, "@p_distrito", DbType.String, dr.Item("ndistrito"))
                    dbBase.AddInParameter(dbCommand, "@p_bono", DbType.Decimal, dr.Item("bono"))

                    'dbbase.addinparameter(dbcommand, "@p_codigousuario", dbtype.int32, int_codigousuario)
                    'dbbase.addinparameter(dbcommand, "@p_codigotipousuario", dbtype.int32, int_codigotipousuario)
                    'dbbase.addinparameter(dbcommand, "@p_codigomodulo", dbtype.string, int_codigomodulo)
                    'dbbase.addinparameter(dbcommand, "@p_codigoopcion", dbtype.int32, int_codigoopcion)

                    'parámetros de salida
                    dbBase.AddOutParameter(dbCommand, "@p_valor", DbType.Int32, 10)
                    dbBase.AddOutParameter(dbCommand, "@p_mensaje", DbType.String, 255)

                    'ejecucion del store procedure
                    dbBase.ExecuteScalar(dbCommand, tran)
                    str_mensaje = dbBase.GetParameterValue(dbCommand, "@p_mensaje").ToString()
                    int_valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_valor")))

                    lstRegistro.Add("Registro: " & dr.Item("calumno") & " " & dr.Item("nalumno") & " : " & str_mensaje)
                    int_totalregistros += 1

                Next

                str_mensaje = "Registros actualizados: " & int_totalregistros
                Commit()

                Return lstRegistro 'int_valor

            Catch ex As Exception
                str_mensaje = "ocurrio un error durante el registro."
                lstRegistro.Add(str_mensaje)
                Rollback()
                Return lstRegistro
            Finally
                Conexion.Close()
            End Try

        End Function

#End Region



#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_DeudasAlumnos(ByVal int_CodigoBanco As Integer, ByVal int_CodigoMoneda As Integer, ByVal int_CodigoTipo As Integer, _
                                              ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_DeudasAlumnos")

            cmd.CommandTimeout = 1200 ' 20 mins

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoBanco", DbType.Int32, int_CodigoBanco)
            dbBase.AddInParameter(cmd, "@p_CodigoMoneda", DbType.Int32, int_CodigoMoneda)
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int32, int_CodigoTipo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DeudasAlumnosPorServicio( _
            ByVal int_CodigoServicio As Integer, _
            ByVal int_CodigoBanco As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_DeudasAlumnosPorServicio")

            cmd.CommandTimeout = 1200 ' 20 mins

            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoServicio", DbType.Int32, int_CodigoServicio)
            dbBase.AddInParameter(cmd, "@p_CodigoBanco", DbType.Int32, int_CodigoBanco)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'update 07/08/2012
        Public Function FUN_LIS_DeudasAlumnosPorServicioGenerales( _
            ByVal int_CodigoServicio As Integer, _
            ByVal int_CodigoBanco As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_DeudasAlumnosPorServiciosGenerales")

            cmd.CommandTimeout = 1200 ' 20 mins

            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoServicio", DbType.Int32, int_CodigoServicio)
            dbBase.AddInParameter(cmd, "@p_CodigoBanco", DbType.Int32, int_CodigoBanco)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'update 13/08/2012
        Public Function FUN_LIS_DeudasAlumnosPorServicioGeneralesCuotas( _
            ByVal int_CodigoServicio As Integer, _
            ByVal int_CodigoBanco As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_DeudasAlumnosPorServiciosGeneralesCuotas")

            cmd.CommandTimeout = 1200 ' 20 mins

            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoServicio", DbType.Int32, int_CodigoServicio)
            dbBase.AddInParameter(cmd, "@p_CodigoBanco", DbType.Int32, int_CodigoBanco)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function



        Public Function FUN_LIS_InformacionDeudasAlumnos(ByVal str_CodigoAlumno As String, ByVal str_FechaVencimiento As String, ByVal int_CodigoMoneda As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_InformacionDeudasAlumnos")

            cmd.CommandTimeout = 1200 ' 20 mins

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_FechaVencimiento", DbType.String, str_FechaVencimiento)
            dbBase.AddInParameter(cmd, "@p_CodigoMoneda", DbType.Int32, int_CodigoMoneda)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ' update
        Public Function FUN_LIS_InformacionDeudasAnualesAlumnos(ByVal str_CodigoAlumno As String, ByVal str_FechaVencimiento As String, ByVal int_CodigoMoneda As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_InformacionDeudasAnualesAlumnos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_FechaVencimiento", DbType.String, str_FechaVencimiento)
            dbBase.AddInParameter(cmd, "@p_CodigoMoneda", DbType.Int32, int_CodigoMoneda)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DeudasPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_DeudasPorAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodoAcademico", DbType.Int32, int_CodigoPeriodoAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DeudasVencidas(ByVal int_CodigoTalonario As Integer, _
                                               ByVal dt_FechaVencimiento As Date, _
                                               ByVal int_CodigoConcepto As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_DeudasVencidas")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)
            dbBase.AddInParameter(cmd, "@p_FechaVencimiento", DbType.DateTime, dt_FechaVencimiento)
            dbBase.AddInParameter(cmd, "@p_Concepto", DbType.Int32, int_CodigoConcepto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DeudasFechasVencimiento(ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_DeudasFechaVencimiento")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_LIS_GeneracionDeudas(ByVal str_ApellidoPaterno As String, ByVal int_CodigoAnioAcademico As Integer, ByVal int_TipoBusqueda As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_GeneracionDeudasAlumnos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, str_ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_TipoBusqueda", DbType.Int32, int_TipoBusqueda)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DeudasGeneradasPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_DeudasGeneradasPorAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodoAcademico", DbType.Int32, int_CodigoPeriodoAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ' updated 31/10/2012
        Public Function FUN_GET_Deuda(ByVal int_Codigo As Integer, ByVal int_TipoPagoAdmision As Integer, ByVal int_CodigoPagoAuxAdmision As Integer, _
                                      ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_GET_Deuda")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int32, int_Codigo)
            dbBase.AddInParameter(cmd, "@p_TipoPago", DbType.Int32, int_TipoPagoAdmision)
            dbBase.AddInParameter(cmd, "@p_CodigoPagoAux", DbType.Int32, int_CodigoPagoAuxAdmision)

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

