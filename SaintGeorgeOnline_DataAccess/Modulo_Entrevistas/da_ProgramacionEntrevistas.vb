Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloEntrevistas
Imports System.Reflection
Namespace ModuloEntrevistas

    Public Class da_ProgramacionEntrevistas
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

        Public Function FUN_INS_ProgramacionEntrevistas( _
           ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
           ByVal lsDetalle As List(Of be_ProgramacionEntrevistaDetalle), ByRef str_mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, _
           ByVal lstEliminar As List(Of be_ProgramacionEntrevistaDetalle), ByVal bool_EnviarEmail As Boolean) As Integer

            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim dbCommand As DbCommand
                Dim usp_mensaje1 = "", usp_mensaje2 As String = ""
                Dim usp_Valor1 = 0, usp_Valor2 As Integer = 0


                dbCommand = dbBase.GetStoredProcCommand("ET_USP_INS_RegistroProgramacionEntrevista")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoSolicitanteEntrevista", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoTipoSolicitanteEntrevista)
                dbBase.AddInParameter(dbCommand, "@p_CodigoMedioSolicitudEntrevista", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoMedioSolicitudEntrevista)
                dbBase.AddInParameter(dbCommand, "@p_CodigoEstadoProgramacionEntrevista", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoEstadoProgramacionEntrevista)
                dbBase.AddInParameter(dbCommand, "@p_FechaEntrevista", DbType.Date, obe_ProgramacionEntrevistaCabecera.FechaEntrevista)
                dbBase.AddInParameter(dbCommand, "@p_HoraInicio", DbType.Date, obe_ProgramacionEntrevistaCabecera.HoraInicio)
                dbBase.AddInParameter(dbCommand, "@p_HoraFin", DbType.Date, obe_ProgramacionEntrevistaCabecera.HoraFin)
                dbBase.AddInParameter(dbCommand, "@p_Motivo", DbType.String, obe_ProgramacionEntrevistaCabecera.Motivo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoFamilia", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoFamilia)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, obe_ProgramacionEntrevistaCabecera.CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorEntrevistador", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoTrabajadorEntrevistador) ' Codigo Persona
                dbBase.AddInParameter(dbCommand, "@p_CodigoPeriodo", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoPeriodo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAmbiente", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoAmbiente)

                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)


                dbBase.AddInParameter(dbCommand, "@RPE_CodigoProgramacionEntrevista", DbType.Int32, obe_ProgramacionEntrevistaCabecera.RPE_CodigoProgramacionEntrevista)
                dbBase.AddInParameter(dbCommand, "@p_EnviarEmail", DbType.Boolean, bool_EnviarEmail)

                dbBase.ExecuteScalar(dbCommand, tran)

                usp_mensaje1 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                usp_Valor1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not usp_Valor1 > 0 Then
                    Rollback()
                    str_mensaje = usp_mensaje1
                    Return 0
                End If



                'USP_DelDetalle()
                '@p_CodigoProgramacionEntrevista int ,
                '@codPersona int ,
                '@p_Mensaje VARCHAR(255) OUTPUT,              
                '@p_Valor INT OUTPUT  
                'as

                For Each item As be_ProgramacionEntrevistaDetalle In lstEliminar
                    If item.CodigoTrabajadorParticipante = 0 Then
                        Continue For
                    End If
                    Dim usp_mensaje3 As String
                    Dim usp_Valor3 As Integer
                    dbCommand = dbBase.GetStoredProcCommand("USP_DelDetalle")
                    dbCommand.Parameters.Clear()

                    dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@codPersona", DbType.Int32, item.CodigoTrabajadorParticipante)

                    ''
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                    ''

                    dbBase.ExecuteScalar(dbCommand, tran)
                    usp_mensaje3 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor3 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                Next


                For Each item As be_ProgramacionEntrevistaDetalle In lsDetalle
                    dbCommand = dbBase.GetStoredProcCommand("ET_USP_INS_DetalleProgramacionEntrevistaParticipantes")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorParticipante", DbType.Int32, item.CodigoTrabajadorParticipante) ' Codigo Persona

                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                    dbBase.ExecuteScalar(dbCommand, tran)

                    usp_mensaje2 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))



                    If Not usp_Valor2 > 0 Then
                        Rollback()
                        str_mensaje = usp_mensaje2
                        Return 0
                    End If

                Next

                Commit()
                str_mensaje = usp_mensaje1
                Return usp_Valor1

            Catch ex As Exception
                Rollback()
                str_mensaje = ex.Message
                Return 0
            Finally
                Conexion.Close()
            End Try
        End Function

        Public Function F_AactualizarListaEntevista(ByVal lstEntrevista As List(Of Object)) As Integer
            Try
                Dim codEntrevist As Integer = 0
                Dim horaInicio As String = String.Empty
                Dim asistio As Boolean? = Nothing

                Dim usp_mensaje2 As String = ""

                Dim usp_Valor2 As Integer = 0
                Dim prop As PropertyInfo
                BeginTransaction()
                Dim dbCommand As DbCommand



                dbCommand = dbBase.GetStoredProcCommand("USP_udpActualizarET_RegistroProgramacionEntrevista")
                For Each obj In lstEntrevista
                    codEntrevist = obj.GetType().GetProperty("codEntrevist").GetValue(obj, Nothing)
                    horaInicio = obj.GetType().GetProperty("horaInicio").GetValue(obj, Nothing)
                    asistio = obj.GetType().GetProperty("asistio").GetValue(obj, Nothing)
                    dbCommand.Parameters.Clear()

                    dbBase.AddInParameter(dbCommand, "@RPE_CodigoProgramacionEntrevista", DbType.Int32, codEntrevist)
                    dbBase.AddInParameter(dbCommand, "@RPE_Asistencia", DbType.Boolean, asistio) ' Codigo Persona
                    dbBase.AddInParameter(dbCommand, "@RPE_HoraAsistencia", DbType.String, IIf(horaInicio Is Nothing, DBNull.Value, horaInicio)) ' Codigo Persona

                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)


                    dbBase.ExecuteScalar(dbCommand, tran)

                    usp_mensaje2 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))



                    If usp_Valor2 = 0 Then
                        Rollback()
                        Return 0
                    End If

                Next
                Commit()
                Return usp_Valor2

                '@RPE_CodigoProgramacionEntrevista  int,
                '@RPE_Asistencia bit ,
                '@RPE_HoraAsistencia smalldatetime,
                '@codigo int out ,
                '@mensaje varchar(max) out 

                '.codEntrevist = codEntrevista, .horaInicio = horaInicio, .asistio = asistio
            Catch ex As Exception
                Rollback()
                Return 0
            End Try
        End Function

        Public Function F_ElminarEntrevista(ByVal codProg As Integer) As Integer
            Dim cod As Integer = 0
            Try

                dbCommand = dbBase.GetStoredProcCommand("USP_DelET_RegistroProgramacionEntrevista")
                dbBase.AddInParameter(dbCommand, "@RPE_CodigoProgramacionEntrevista", DbType.Int32, codProg)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                dbBase.ExecuteScalar(dbCommand)
                cod = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                Return cod
            Catch ex As Exception

            End Try
        End Function

        Public Function FUN_UPD_ProgramacionEntrevistasWeb( _
           ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, ByRef str_mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim dbCommand As DbCommand
            Dim usp_mensaje As String = ""
            Dim usp_Valor As Integer = 0

            dbCommand = dbBase.GetStoredProcCommand("ET_USP_UPD_RegistroProgramacionEntrevistaWeb")
            dbCommand.Parameters.Clear()
            dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoProgramacionEntrevistaCabecera)

            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            dbBase.ExecuteScalar(dbCommand)

            usp_mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function


        Public Function FUN_UPD_ProgramacionEntrevistasFicha( _
            ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByVal lstDetalleParticipantesEliminar As List(Of be_ProgramacionEntrevistaDetalle), _
            ByVal lstDetalleParticipantes As List(Of be_ProgramacionEntrevistaDetalle), _
            ByVal lstDetalleAcuerdos As List(Of be_ProgramacionEntrevistaDetalleAcuerdos), _
            ByVal lstDetalleMotivosEliminar As List(Of be_ProgramacionEntrevistaDetalleMotivo), _
            ByVal lstDetalleMotivos As List(Of be_ProgramacionEntrevistaDetalleMotivo), _
            ByVal lstDetalleAsistentesEliminar As List(Of be_ProgramacionEntrevistaDetalleAsistentes), _
            ByVal lstDetalleAsistentes As List(Of be_ProgramacionEntrevistaDetalleAsistentes), _
            ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim dbCommand As DbCommand
                Dim usp_mensaje1 = "", usp_mensaje2 = "", usp_mensaje3 = "", usp_mensaje4 = "", usp_mensaje5 = "", usp_mensaje6 = "", usp_mensaje7 = "", usp_mensaje8 = "", usp_mensaje9 As String = ""
                Dim usp_Valor1 = 0, usp_Valor2 = 0, usp_Valor3 = 0, usp_Valor4 = 0, usp_Valor5 = 0, usp_Valor6 = 0, usp_Valor7 = 0, usp_Valor8 = 0, usp_Valor9 As Integer = 0

                ' Cabecera
                dbCommand = dbBase.GetStoredProcCommand("ET_USP_UPD_RegistroProgramacionEntrevistaFicha")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoProgramacionEntrevistaCabecera)
                dbBase.AddInParameter(dbCommand, "@p_Comentario", DbType.String, obe_ProgramacionEntrevistaCabecera.Comentario)
                dbBase.AddInParameter(dbCommand, "@p_AspectosTratados", DbType.String, obe_ProgramacionEntrevistaCabecera.AspectosTratados)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(dbCommand, tran)
                usp_mensaje1 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                usp_Valor1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not usp_Valor1 > 0 Then
                    Rollback()
                    str_mensaje = usp_mensaje1
                    Return 0
                End If


                ' Detalle Participantes
                For Each item As be_ProgramacionEntrevistaDetalle In lstDetalleParticipantesEliminar
                    dbCommand = dbBase.GetStoredProcCommand("ET_USP_DEL_DetalleParticipanteEntrevista")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorParticipante", DbType.Int32, item.CodigoTrabajadorParticipante)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.ExecuteScalar(dbCommand, tran)
                    usp_mensaje2 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                    If Not usp_Valor2 > 0 Then
                        Rollback()
                        str_mensaje = usp_mensaje2
                        Return 0
                    End If
                Next

                For Each item As be_ProgramacionEntrevistaDetalle In lstDetalleParticipantes
                    dbCommand = dbBase.GetStoredProcCommand("ET_USP_INS_DetalleParticipantesEntrevista")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorParticipante", DbType.Int32, item.CodigoTrabajadorParticipante)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.ExecuteScalar(dbCommand, tran)
                    usp_mensaje3 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor3 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                    If Not usp_Valor3 > 0 Then
                        Rollback()
                        str_mensaje = usp_mensaje3
                        Return 0
                    End If
                Next


                ' Detalle Acuerdos
                dbCommand = dbBase.GetStoredProcCommand("ET_USP_DEL_DetalleAcuerdosEntrevista")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, usp_Valor1)
                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                dbBase.ExecuteScalar(dbCommand, tran)
                usp_mensaje5 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                usp_Valor5 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                If Not usp_Valor5 > 0 Then
                    Rollback()
                    str_mensaje = usp_mensaje5
                    Return 0
                End If

                For Each item As be_ProgramacionEntrevistaDetalleAcuerdos In lstDetalleAcuerdos
                    dbCommand = dbBase.GetStoredProcCommand("ET_USP_INS_DetalleAcuerdosEntrevista")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@p_Acuerdo", DbType.String, item.Acuerdo)
                    dbBase.AddInParameter(dbCommand, "@p_FechaAcuerdo", DbType.Date, item.FechaAcuerdo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                    dbBase.ExecuteScalar(dbCommand, tran)
                    usp_mensaje4 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor4 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                    If Not usp_Valor4 > 0 Then
                        Rollback()
                        str_mensaje = usp_mensaje4
                        Return 0
                    End If
                Next


                ' Detalle Motivos
                For Each item As be_ProgramacionEntrevistaDetalleMotivo In lstDetalleMotivosEliminar
                    dbCommand = dbBase.GetStoredProcCommand("ET_USP_DEL_DetalleMotivoEntrevista")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleMotivosEntrevistas", DbType.Int32, item.CodigoDetalleMotivosEntrevistas)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                    dbBase.ExecuteScalar(dbCommand, tran)
                    usp_mensaje6 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor6 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                    If Not usp_Valor6 > 0 Then
                        Rollback()
                        str_mensaje = usp_mensaje6
                        Return 0
                    End If
                Next

                For Each item As be_ProgramacionEntrevistaDetalleMotivo In lstDetalleMotivos
                    dbCommand = dbBase.GetStoredProcCommand("ET_USP_INS_DetalleMotivoEntrevista")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionSubMotivos", DbType.Int32, item.CodigoAsignacionSubMotivos)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                    dbBase.ExecuteScalar(dbCommand, tran)
                    usp_mensaje7 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor7 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                    If Not usp_Valor7 > 0 Then
                        Rollback()
                        str_mensaje = usp_mensaje7
                        Return 0
                    End If
                Next

                'Detalle Asistentes
                For Each item As be_ProgramacionEntrevistaDetalleAsistentes In lstDetalleAsistentesEliminar
                    dbCommand = dbBase.GetStoredProcCommand("ET_USP_DEL_DetalleAsistentesEntrevista")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAsistenteFamiliaEntrevista", DbType.Int32, item.CodigoAsistenteFamiliaEntrevista)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                    dbBase.ExecuteScalar(dbCommand, tran)
                    usp_mensaje8 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor8 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                    If Not usp_Valor8 > 0 Then
                        Rollback()
                        str_mensaje = usp_mensaje8
                        Return 0
                    End If
                Next

                For Each item As be_ProgramacionEntrevistaDetalleAsistentes In lstDetalleAsistentes
                    dbCommand = dbBase.GetStoredProcCommand("ET_USP_INS_DetalleAsistentesEntrevista")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAsistenteFamilia", DbType.Int32, item.CodigoAsistenteFamilia)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                    dbBase.ExecuteScalar(dbCommand, tran)
                    usp_mensaje9 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor9 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                    If Not usp_Valor9 > 0 Then
                        Rollback()
                        str_mensaje = usp_mensaje9
                        Return 0
                    End If
                Next


                Commit()
                str_mensaje = usp_mensaje1
                Return usp_Valor1

            Catch ex As Exception
                Rollback()
                str_mensaje = ex.Message
                Return 0
            Finally
                Conexion.Close()
            End Try
        End Function

        Public Function FUN_DEL_ProgramacionEntrevistasFicha( _
            ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Try
                BeginTransaction()
                Dim dbCommand As DbCommand
                Dim usp_mensaje1 As String = ""
                Dim usp_Valor1 As Integer = 0

                dbCommand = dbBase.GetStoredProcCommand("ET_USP_DEL_RegistroProgramacionEntrevistaFicha")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionEntrevista", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoProgramacionEntrevistaCabecera)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(dbCommand, tran)
                usp_mensaje1 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                usp_Valor1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not usp_Valor1 > 0 Then
                    Rollback()
                    Return 0
                End If

                Commit()
                str_mensaje = usp_mensaje1
                Return usp_Valor1

            Catch ex As Exception
                Rollback()
                str_mensaje = ex.Message
                Return 0
            Finally
                Conexion.Close()
            End Try


        End Function
#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_ProgramacionEntrevistas( _
            ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoMes As Integer, ByVal int_CodigoGrado As Integer, ByVal int_EstadoEntrevista As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal RPE_CodigoProgramacionEntrevista As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("ET_USP_LIS_ProgramacionEntrevista")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodo", DbType.Int32, int_CodigoPeriodo)
            dbBase.AddInParameter(cmd, "@p_CodigoMes", DbType.Int32, int_CodigoMes)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_EstadoEntrevista", DbType.Int32, int_EstadoEntrevista)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            dbBase.AddInParameter(cmd, "@RPE_CodigoProgramacionEntrevista", DbType.Int32, RPE_CodigoProgramacionEntrevista)


            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_ProgramacionEntrevistasWeb(ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("ET_USP_GET_ProgramacionEntrevistaWeb")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoProgramacionEntrevista", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoProgramacionEntrevistaCabecera)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_ProgramacionEntrevistasSinFichaPorTrabajador( _
            ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("ET_USP_LIS_ProgramacionEntrevistaSinFichaPorTrabajador")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTrabajadorEntrevistador", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoTrabajadorEntrevistador)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_GET_ProgramacionEntrevistas(ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("ET_USP_GET_ProgramacionEntrevista")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoProgramacionEntrevista", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoProgramacionEntrevistaCabecera)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_ProgramacionEntrevistasConFichaPorTrabajador( _
            ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoMes As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoTrabajadorEntrevistador As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("ET_USP_LIS_ProgramacionEntrevistaConFichaPorTrabajador")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodo", DbType.Int32, int_CodigoPeriodo)
            dbBase.AddInParameter(cmd, "@p_CodigoMes", DbType.Int32, int_CodigoMes)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoTrabajadorEntrevistador", DbType.Int32, int_CodigoTrabajadorEntrevistador)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_ProgramacionEntrevistasConFicha(ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("ET_USP_GET_ProgramacionEntrevistaConFicha")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoProgramacionEntrevista", DbType.Int32, obe_ProgramacionEntrevistaCabecera.CodigoProgramacionEntrevistaCabecera)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

    End Class

End Namespace
