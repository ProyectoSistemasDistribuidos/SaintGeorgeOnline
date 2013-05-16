

Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities

Namespace ModuloNotas
    Public Class da_RegistroNotasComponentes

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

        Public Function FUN_INS_CU_RegistroNotasComponentes(ByVal o_tbla As DataTable, ByVal Lst_Componentes As List(Of componente), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Dim mensaje As String = ""
            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim str_Mensaje As String = ""
                Dim p_Valor1 = 0, p_Valor2 = 0, p_Valor3 As Integer = 0

                Dim dbCommand As DbCommand

                For Each fila As DataRow In o_tbla.Rows
                    ''RNBL_CodigoRegistroBimestralL

                    For Each oComponenente As componente In Lst_Componentes
                        dbCommand = dbBase.GetStoredProcCommand("CU_usp_INS_CU_RegistroNotasComponentes")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroBimestralL", DbType.Int32, Convert.ToInt32(fila("RNBL_CodigoRegistroBimestralL").ToString()))
                        dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroComponentes", DbType.Int32, oComponenente.idRegistro)
                        dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                        dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        p_Valor1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                        mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@p_Mensaje"))


                        ''
                        For Each oIndicador As indicador In oComponenente.listaIndicador
                            dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotInd")
                            dbCommand.Parameters.Clear()
                            dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroNotaComponente", DbType.Int32, p_Valor1)
                            dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroIndicadores", DbType.Int32, oIndicador.idRegistro)
                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                            dbBase.ExecuteScalar(dbCommand, tran)
                            p_Valor2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                            mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@p_Mensaje"))
                            dbCommand.Parameters.Clear()
                            For Each oSubindicador As subindicador In oIndicador.listaSubindicador
                                dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotasSubIndicadores")
                                dbCommand.Parameters.Clear()
                                dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroNotaIndicador", DbType.Int32, p_Valor2)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroSubIndicadores", DbType.Int32, oSubindicador.idRegistro)
                                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                                dbBase.ExecuteScalar(dbCommand, tran)
                                p_Valor3 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                                mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@p_Mensaje"))
                                dbCommand.Parameters.Clear()
                            Next
                        Next
                    Next
                Next
                '' str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                'p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
                'lstResultado.Add(str_Mensaje)
                'lstResultado.Add(p_Valor.ToString())

                Commit()
                Return lstResultado

            Catch ex As Exception
                Rollback()
            End Try
        End Function


        Public Function FUN_agregar_SubIndicador(ByVal lp As List(Of persona), ByVal subIndicador As Integer)
            BeginTransaction()
            Try

                For Each op As persona In lp
                    For Each notComp As notaComponente In op.lstNotaComponente
                        If notComp.codRegComponente <> 121 Then
                            Continue For
                        End If
                        For Each notaInd As notaIndicador In notComp.lstNotaIndicador
                            If notaInd.codRegIndicador <> 257 Then
                                Continue For
                            End If

                            'For Each notaSub In notaInd.lstNotaSubinidcador

                            dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotasSubIndicadores")
                            dbCommand.Parameters.Clear()
                            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroNotaIndicador", DbType.Int32, notaInd.codRegNotaIndicador)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroSubIndicadores", DbType.Int32, subIndicador)
                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, 1)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, 1)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, 1)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, 1)
                            dbBase.ExecuteScalar(dbCommand, tran)
                            Dim p_Valor3 As Integer = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                            dbCommand.Parameters.Clear()

                            'Next

                        Next

                    Next

                Next



                Commit()
            Catch ex As Exception
                Rollback()
            Finally

            End Try

        End Function

        Public Function FUN_UPD_CU_ActualizarNotasComponente(ByVal int_IdRegistroNotaComponente As Integer, ByVal str_notaComponenete As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim dbCommand As DbCommand
                dbCommand = dbBase.GetStoredProcCommand("CU_USP_UDP_RegistroNotasComponentes")
                dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroNotaComponente", DbType.Int32, int_IdRegistroNotaComponente)
                dbBase.AddInParameter(dbCommand, "@P_NotaComponente", DbType.String, str_notaComponenete)

                dbBase.AddOutParameter(dbCommand, "@p_promedio", DbType.String, 100)

                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                dbBase.ExecuteScalar(dbCommand, tran)

                lstResultado.Add(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                lstResultado.Add(CStr(dbBase.GetParameterValue(dbCommand, "@p_Mensaje")))

                lstResultado.Add(CStr(dbBase.GetParameterValue(dbCommand, "@p_promedio")))

                dbCommand.Parameters.Clear()

                Commit()
                Return lstResultado

            Catch ex As Exception
                Rollback()
            End Try

        End Function

#End Region
#Region "Metodos No Transaccionales"
        Public Function FUN_lIS_CU_ListarNotasComponenteIndicadorSubindicador(ByVal int_codigoBimestre As Integer, ByVal int_codigoGrupo As Integer)
            Try
                'create procedure CU_USP_LIS_RegistrosNotas
                '@p_codigoBimestre int,
                '@p_codigoGrupo int
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_RegistrosNotas")
                dbBase.AddInParameter(dbCommand, "@p_codigoBimestre", DbType.Int32, int_codigoBimestre)
                dbBase.AddInParameter(dbCommand, "@p_codigoGrupo", DbType.Int32, int_codigoGrupo)
                'dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                'dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                'dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                Return dbBase.ExecuteDataSet(dbCommand)
            Catch ex As Exception
            Finally
            End Try
        End Function
        ''
        ''
        ''lstModificadosComponente As List(Of Integer), ByVal lstNotasModificadoIndicador As List(Of Integer), ByVal ltsNotasModificadas 
        Public Function fActualizarTodo(ByVal oLista As List(Of persona), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal lstModificadosComponente As List(Of Integer), _
                                        ByVal lstNotasModificadoIndicador As List(Of Integer), _
                                        ByVal ltsNotasModificadas As List(Of Integer)) As List(Of persona)
            'lstNotasModificadoIndicador = From h In lstNotasModificadoIndicador Select h Distinct
            'ltsNotasModificadas = From j In ltsNotasModificadas Select j Distinct


            Try
                Dim lstComponente As New List(Of componente)
                Dim cantidadIndicador As Integer = 0
                Dim contadorIndicador As Integer = 0
                Dim codActualizo As Integer = 0
                Dim lsita As New List(Of String)
                '' BeginTransaction()
                For Each persona As persona In oLista
                    For Each oCompoenente As notaComponente In persona.lstNotaComponente
                        For Each oNotaIndicador As notaIndicador In oCompoenente.lstNotaIndicador
                            If lstNotasModificadoIndicador.Contains(oNotaIndicador.codRegNotaIndicador) Then
                                dbCommand = dbBase.GetStoredProcCommand("CU_USP_UDP_RegistroNotasIndicadores")
                                dbCommand.Parameters.Clear()
                                dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroNotaIndicador", DbType.Int32, oNotaIndicador.codRegNotaIndicador)
                                dbBase.AddInParameter(dbCommand, "@P_NotaIndicador", DbType.String, oNotaIndicador.notaIndicador)
                                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                                ''
                                dbBase.AddOutParameter(dbCommand, "@pCodComponente", DbType.Int32, 255)
                                dbBase.AddOutParameter(dbCommand, "@pNotComponente", DbType.String, 100)
                                dbBase.AddOutParameter(dbCommand, "@p_promedio", DbType.String, 100)
                                ''
                                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                                dbBase.ExecuteScalar(dbCommand) '', tran)
                                oNotaIndicador.estadoACtualizo = dbBase.GetParameterValue(dbCommand, "@p_Valor")
                                dbCommand.Parameters.Clear()
                            End If
                            For Each oNotaSubindicador As notaSubIndicador In oNotaIndicador.lstNotaSubinidcador
                                If ltsNotasModificadas.Contains(oNotaSubindicador.codRegSubindicador) Then
                                    dbCommand = dbBase.GetStoredProcCommand("CU_USP_UDP_RegistroNotasSubIndicadores")
                                    dbCommand.Parameters.Clear()
                                    dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroNotaSubIndicador", DbType.Int32, oNotaSubindicador.codRegSubindicador)
                                    dbBase.AddInParameter(dbCommand, "@P_NotaSubindicador", DbType.String, oNotaSubindicador.notaSubIndicador)
                                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                                    dbBase.ExecuteScalar(dbCommand) '', tran)
                                    oNotaSubindicador.estadoActualizo = dbBase.GetParameterValue(dbCommand, "@p_Valor")
                                    dbCommand.Parameters.Clear()
                                End If
                            Next
                        Next
                    Next
                Next


                '' Commit()
                Return oLista
            Catch ex As Exception
                '' Rollback()

            Finally

            End Try
        End Function

        Public Function listarPromediosBimestrales(ByVal int_cod_grupo As Integer, ByVal int_cod_bimestre As Integer)
            Try

                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_ListarPomediosNotasBimestre")
                dbBase.AddInParameter(dbCommand, "@p_codigoBimestre", DbType.Int32, int_cod_grupo)
                dbBase.AddInParameter(dbCommand, "@p_codigoGrupo", DbType.Int32, int_cod_bimestre)

                Return dbBase.ExecuteDataSet(dbCommand)


            Catch ex As Exception

            Finally

            End Try
        End Function
        ''' <summary>
        ''' funcion que  lista toda la estrucutura de resitro componente indicador  subindicador para generar su registro nota componente,nota indicador ,subindicador  
        '''
        ''' </summary>
        ''' <param name="codAsignacionAula">codigo del salo ha generar su libreta de notas </param>
        ''' <param name="codBimestre">bimestre para generar su libreta de notas</param>
        ''' <returns>retorna 3 tablas 1.)tabla de componente 2.)tabla idnicador 3.) tabla subindicador  </returns>
        ''' <remarks></remarks>
        Public Function listarEstructuraComponenteindicadorSubindicadorPorAula(ByVal codAsignacionAula As Integer, ByVal codBimestre As Integer)
            Try

                Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LisEstrucuturaCompoenente")
                dbBase.AddInParameter(dbCommand, "@codAsignacionAula", DbType.Int32, codAsignacionAula)
                dbBase.AddInParameter(dbCommand, "@codBimestre", DbType.Int32, codBimestre)
                Return dbBase.ExecuteDataSet(dbCommand)

            Catch ex As Exception
            Finally

            End Try
        End Function



        Public Function FUN_INS_CU_InsertarNotaComponenteEstrucuturaSalon(ByVal codBimestre As Integer, ByVal lstComponenteNuevo As List(Of ComponenteNuevo), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim mensaje As String = ""
            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim str_Mensaje As String = ""
                Dim p_Valor1 = 0, p_Valor2 = 0, p_Valor3 As Integer = 0

                Dim dbCommand As DbCommand


                ''RNBL_CodigoRegistroBimestralL

                For Each oComponenente As ComponenteNuevo In lstComponenteNuevo
                    dbCommand = dbBase.GetStoredProcCommand("CU_usp_INS_CU_RegistroNotasComponentes")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroBimestralL", DbType.Int32, codBimestre)
                    dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroComponentes", DbType.Int32, oComponenente.codRegComponenteNuevo)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.ExecuteScalar(dbCommand, tran)
                    p_Valor1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                    mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@p_Mensaje"))


                    ''
                    For Each oIndicador As IndicadorNuevo In oComponenente.lstIndicadorNuevo
                        dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotInd")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroNotaComponente", DbType.Int32, p_Valor1)
                        dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroIndicadores", DbType.Int32, oIndicador.codRegIndicadorNuevo)
                        dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                        dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        p_Valor2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                        mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@p_Mensaje"))
                        dbCommand.Parameters.Clear()
                        For Each oSubindicador As SubIndicadorNuevo In oIndicador.lstSubIndicadorNuevo
                            dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotasSubIndicadores")
                            dbCommand.Parameters.Clear()
                            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroNotaIndicador", DbType.Int32, p_Valor2)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroSubIndicadores", DbType.Int32, oSubindicador.ocdRegSubIndicadorNuevo)
                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                            dbBase.ExecuteScalar(dbCommand, tran)
                            p_Valor3 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                            mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@p_Mensaje"))
                            dbCommand.Parameters.Clear()
                        Next
                    Next
                Next

                '' str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                'p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
                'lstResultado.Add(str_Mensaje)
                'lstResultado.Add(p_Valor.ToString())

                Commit()
                Return lstResultado

            Catch ex As Exception
                Rollback()
            End Try
        End Function


#End Region




    End Class


    Public Class ComponenteNuevo

        Public codRegComponenteNuevo As Integer
        Public lstIndicadorNuevo As New List(Of IndicadorNuevo)

    End Class

    Public Class IndicadorNuevo

        Public codRegIndicadorNuevo As Integer
        Public lstSubIndicadorNuevo As New List(Of SubIndicadorNuevo)

    End Class

    Public Class SubIndicadorNuevo

        Public ocdRegSubIndicadorNuevo As Integer

    End Class

End Namespace
