
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Transactions
Imports System.Collections
Imports System.Linq

Public Class DA_ALL_DataBase


    Private Servidor13BdSanJorge As String = "data source=192.168.1.13;initial catalog=BD_SanGeorgeOnline; uid=SanJorgeOnlineSG; pwd=5g0nl1y% ;Language=spanish"
    Private Servidor18BdDocumentacion As String = "data source=192.168.1.18;initial catalog=BDSanJorge_Documentacion; uid=sa; pwd=sql123 ;Language=spanish"
    Private Servidor18BdAsistencia As String = "data source=192.168.1.18;initial catalog=BDSanJorge_Asistencia; uid=sa; pwd=sql123 ;Language=spanish"

    Private Servidor13BdSanJorgeTesoreria As String = "data source=192.168.1.13;initial catalog=BD_SanGeorgeOnline_Tesoreria; uid=SanJorgeOnlineSG; pwd=5g0nl1y% ;Language=spanish"
    '
    Sub New()


       

    End Sub

    Function listarEntrevista() As DataSet
        Try
            '    Using Otr As New TransactionScope()
            Try

                Using con As New SqlConnection(Servidor18BdDocumentacion)
                    con.Open()
                    Using ocom As New SqlCommand
                        ocom.Connection = con
                        ocom.CommandType = CommandType.StoredProcedure
                        ocom.CommandText = "USP_LIS_MigracionEntrevistas"
                        Using odata As New SqlDataAdapter

                            odata.SelectCommand = ocom
                            Using dts As New DataSet
                                odata.Fill(dts)
                                Return dts


                             



                            End Using
                        End Using
                    End Using
                End Using





                'Otr.Complete()
            Catch ex As Exception

            End Try
            '  End Using
        Catch ex As Exception

        End Try
    End Function

    Public Function FInsertarEntrevista() As Integer
        Dim dts As New DataSet

        Dim tdMotivo As New DataTable
        Dim dtEntrevista As New DataTable
        Dim dtParticipante As New DataTable
        Dim otran As SqlTransaction
        Dim filaACtual As DataRow = Nothing
        Try
            dts = listarEntrevista()

            tdMotivo = dts.Tables(0)
            dtEntrevista = dts.Tables(1)
            dtParticipante = dts.Tables(2)

            '@RPE_CodigoProgramacionEntrevista  int ,
            '@TSE_CodigoTipoSolicitanteEntrevista int ,
            '@MSE_CodigoMedioSolicitudEntrevista int ,
            '@EPE_CodigoEstadoProgramacionEntrevista int ,
            '@RPE_FechaEntrevista datetime,
            '@RPE_FechaCreacion datetime,
            '@RPE_HoraInicio datetime ,
            '@RPE_HoraFin datetime ,
            '@RPE_Asistencia bit ,
            '@RPE_HoraAsistencia smalldatetime,
            '@RPE_Estado bit,
            '@RPE_Motivo varchar(800),
            '@TJ_CodigoTrabajadorRegistrador int,
            '@FF_CodigoFamilia varchar(10),
            '@AL_CodigoAlumno varchar(8),
            '@TJ_CodigoTrabajadorEntrevistador int,
            '@AC_CodigoAnioAcademico int ,
            '@AMB_CodigoAmbiente int ,
            '@RPE_Comentario varchar(8000),
            '@RPE_AspectosTratados varchar(8000),
            '@RPE_FechaRegistroEntrevista datetime,
            '------------------------------
            '@apPaterno varchar(max),
            '@apMaterno varchar(max),
            '@nombre varchar(max),
            '@cod_trabajador int ,
            '-------------------------------
            '@codigo int out ,
            '@mensaje varchar(max) out 

            Dim CountFilas As Integer = 0
            Using con As New SqlConnection(Servidor13BdSanJorgeTesoreria)
                con.Open()




                Using ocom As New SqlCommand

                    ocom.Connection = con
                    ocom.CommandType = CommandType.StoredProcedure

                    otran = con.BeginTransaction()

                    Dim dtFecha As DateTime
                    ocom.Transaction = otran
                    For Each filasEntrevistas As DataRow In dtEntrevista.Rows
                        CountFilas += 1
                        ocom.CommandText = "USP_InsMigracionEntevista"
                        ocom.Parameters.Clear()

                        filaACtual = filasEntrevistas
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("IdEntrevista"), _
                                                                   .ParameterName = "RPE_CodigoProgramacionEntrevista", _
                                                                   .DbType = DbType.Int32})


                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("IdTipoSolicitante"), _
                                                               .ParameterName = "TSE_CodigoTipoSolicitanteEntrevista", _
                                                               .DbType = DbType.Int32})

                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("IdModoCitacion"), _
                                                               .ParameterName = "MSE_CodigoMedioSolicitudEntrevista", _
                                                               .DbType = DbType.Int32})
                        'estadoEntevista
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("estadoEntevista"), _
                                                               .ParameterName = "EPE_CodigoEstadoProgramacionEntrevista", _
                                                               .DbType = DbType.Int32})


                        ocom.Parameters.Add(New SqlParameter With {.Value = IIf(filasEntrevistas("FechaEntrevista").ToString() = "", DBNull.Value, filasEntrevistas("FechaEntrevista")), _
                                                               .ParameterName = "RPE_FechaEntrevista", _
                                                               .DbType = DbType.Date})
                        ocom.Parameters.Add(New SqlParameter With {.Value = IIf(filasEntrevistas("FechaCreacion").ToString() = "", DBNull.Value, filasEntrevistas("FechaCreacion")), _
                                                               .ParameterName = "RPE_FechaCreacion", _
                                                               .DbType = DbType.DateTime})
                        DateTime.TryParse(filasEntrevistas("horaInicio"), dtFecha)
                        ocom.Parameters.Add(New SqlParameter With {.Value = IIf(filasEntrevistas("horaInicio").ToString() = "", DBNull.Value, dtFecha), _
                                                               .ParameterName = "RPE_HoraInicio", _
                                                               .DbType = DbType.DateTime}) ' DateTime.TryParse(filasEntrevistas("horaInicio"), dtFecha)
                        DateTime.TryParse(filasEntrevistas("horaFIn"), dtFecha)
                        ocom.Parameters.Add(New SqlParameter With {.Value = IIf(filasEntrevistas("horaFIn").ToString() = "", DBNull.Value, dtFecha), _
                                                             .ParameterName = "RPE_HoraFin", _
                                                             .DbType = DbType.DateTime})

                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("Asistio"), _
                                                             .ParameterName = "RPE_Asistencia", _
                                                             .DbType = DbType.Boolean})
                        ocom.Parameters.Add(New SqlParameter With {.Value = IIf(filasEntrevistas("HoraAsitencia").ToString() = "", DBNull.Value, filasEntrevistas("HoraAsitencia")), _
                                                             .ParameterName = "RPE_HoraAsistencia", _
                                                             .DbType = DbType.DateTime})
                        ocom.Parameters.Add(New SqlParameter With {.Value = IIf(filasEntrevistas("estado") = 1, True, False), _
                                                             .ParameterName = "RPE_Estado", _
                                                             .DbType = DbType.Boolean})
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("motivoEntrevista"), _
                                                             .ParameterName = "RPE_Motivo", _
                                                             .DbType = DbType.String})
                        ocom.Parameters.Add(New SqlParameter With {.Value = 3, _
                                                             .ParameterName = "TJ_CodigoTrabajadorRegistrador", _
                                                             .DbType = DbType.Int32})
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("codigoFamilia"), _
                                                             .ParameterName = "FF_CodigoFamilia", _
                                                             .DbType = DbType.String})
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("codAlumno"), _
                                                             .ParameterName = "AL_CodigoAlumno", _
                                                             .DbType = DbType.String})

                        ''

                        ocom.Parameters.Add(New SqlParameter With {.Value = DBNull.Value, _
                                                        .ParameterName = "TJ_CodigoTrabajadorEntrevistador", _
                                                        .DbType = DbType.Int32})
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("cdoAnio"), _
                                                       .ParameterName = "AC_CodigoAnioAcademico", _
                                                       .DbType = DbType.Int32})
                        ocom.Parameters.Add(New SqlParameter With {.Value = DBNull.Value, _
                                                       .ParameterName = "AMB_CodigoAmbiente", _
                                                       .DbType = DbType.Int32})
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("comentario"), _
                                                       .ParameterName = "RPE_Comentario", _
                                                       .DbType = DbType.String})
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("aspectosTratados"), _
                                                       .ParameterName = "RPE_AspectosTratados", _
                                                       .DbType = DbType.String})
                        ocom.Parameters.Add(New SqlParameter With {.Value = DBNull.Value, _
                                                       .ParameterName = "RPE_FechaRegistroEntrevista", _
                                                       .DbType = DbType.DateTime})
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("ApePat"), _
                                                       .ParameterName = "apPaterno", _
                                                       .DbType = DbType.String})
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("ApeMat"), _
                                                       .ParameterName = "apMaterno", _
                                                       .DbType = DbType.String})
                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("Nombres"), _
                                                      .ParameterName = "nombre", _
                                                      .DbType = DbType.String})

                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("cod_trabajador"), _
                                                    .ParameterName = "cod_trabajador", _
                                                    .DbType = DbType.Int32})

                        ocom.Parameters.Add(New SqlParameter With {.Size = 10, _
                                                    .ParameterName = "codigo", _
                                                    .DbType = DbType.Int32, .Direction = ParameterDirection.Output})

                        ocom.Parameters.Add(New SqlParameter With {.Size = 200, _
                                                    .ParameterName = "mensaje", _
                                                    .DbType = DbType.String, .Direction = ParameterDirection.Output})


                        ocom.ExecuteScalar()
                        Dim cod As Integer = 0
                        cod = ocom.Parameters("codigo").Value
                        Dim mensajeOutt As String = ocom.Parameters("mensaje").Value

                        If cod = 0 Then
                            Continue For
                        End If
                        ''-----------------------------------------
                        Dim mensaje As String = ocom.Parameters("mensaje").Value
                        ocom.Parameters.Clear()
                        ocom.CommandText = "USP_insET_DetalleEntrevistaAcuerdos"

                        ocom.Parameters.Add(New SqlParameter With {.Value = 0, _
                                                  .ParameterName = "RAE_CodigoRegistroAcuerdosEntrevista", _
                                                  .DbType = DbType.Int32})

                        ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("IdEntrevista"), _
                                               .ParameterName = "RPE_CodigoProgramacionEntrevista", _
                                               .DbType = DbType.Int32})

                        ocom.Parameters.Add(New SqlParameter With {.Value = IIf(filasEntrevistas("Acuerdos").ToString() = "", DBNull.Value, filasEntrevistas("Acuerdos").ToString()), _
                                             .ParameterName = "RAE_Acuerdo", _
                                             .DbType = DbType.String})

                        ocom.Parameters.Add(New SqlParameter With {.Value = IIf(filasEntrevistas("FechaAcuerdo").ToString() = "", DBNull.Value, filasEntrevistas("FechaAcuerdo")), _
                                            .ParameterName = "RAE_FechaAcuerdo", _
                                            .DbType = DbType.DateTime})

                        Dim ids As Integer = 0
                        ids = ocom.ExecuteScalar()




                        Dim listaMotivo = tdMotivo.Select("IdEntrevista =" & filasEntrevistas("IdEntrevista"))


                        ocom.CommandText = "USP_InsET_AsignacionSubMotivosEntrevistas"
                        For Each filaMotivo As DataRow In listaMotivo
                            ocom.Parameters.Clear()
                            ocom.CommandText = "USP_InsET_AsignacionSubMotivosEntrevistas"

                            ocom.Parameters.Add(New SqlParameter With {.Value = 0, _
                                           .ParameterName = "ASME_CodigoAsignacionSubMotivos", _
                                           .DbType = DbType.Int32})

                            ocom.Parameters.Add(New SqlParameter With {.Value = filaMotivo("codMotivo"), _
                                           .ParameterName = "ME_CodigoMotivoEntrevista", _
                                           .DbType = DbType.Int32})

                            ocom.Parameters.Add(New SqlParameter With {.Value = filaMotivo("codSubmotivo"), _
                                           .ParameterName = "SME_CodigoSubMotivoEntrevista", _
                                           .DbType = DbType.Int32})

                            Dim codMotivo As Integer = 0
                            codMotivo = ocom.ExecuteScalar()


                            ''
                            ocom.CommandText = "USP_InsET_DetalleEntrevistaMotivo"
                            ocom.Parameters.Clear()
                            ''
                            ocom.Parameters.Add(New SqlParameter With {.Value = 0, _
                                          .ParameterName = "DME_CodigoDetalleMotivosEntrevistas", _
                                          .DbType = DbType.Int32})

                            ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("IdEntrevista"), _
                                           .ParameterName = "RPE_CodigoProgramacionEntrevista", _
                                           .DbType = DbType.Int32})

                            ocom.Parameters.Add(New SqlParameter With {.Value = codMotivo, _
                                           .ParameterName = "ASME_CodigoAsignacionSubMotivos", _
                                           .DbType = DbType.Int32})
                            ''

                            Dim codIds As Integer = 0
                            codIds = ocom.ExecuteScalar()




                        Next




                        Dim participantes = dtParticipante.Select("IdEntrevista=" & filasEntrevistas("IdEntrevista"))
                        ocom.CommandText = "USP_InsET_DetalleProgramacionEntrevistaParticipantes"

                        For Each filaPartipante In participantes
                            ocom.Parameters.Clear()

                            ocom.Parameters.Add(New SqlParameter With {.Value = 0, _
                                      .ParameterName = "DPEP_CodigoDetalleProgramacionEntrevistaParticipantes", _
                                      .DbType = DbType.Int32})

                            ocom.Parameters.Add(New SqlParameter With {.Value = filasEntrevistas("IdEntrevista"), _
                                        .ParameterName = "RPE_CodigoProgramacionEntrevista", _
                                        .DbType = DbType.Int32})

                            ocom.Parameters.Add(New SqlParameter With {.Value = 0, _
                                           .ParameterName = "TJ_CodigoTrabajadorParticipante", _
                                           .DbType = DbType.Int32})

                            ocom.Parameters.Add(New SqlParameter With {.Value = filaPartipante("Nombres"), _
                                        .ParameterName = "nombre", _
                                        .DbType = DbType.String})

                            ocom.Parameters.Add(New SqlParameter With {.Value = filaPartipante("ApePat"), _
                                           .ParameterName = "apPaterno", _
                                           .DbType = DbType.String})

                            ocom.Parameters.Add(New SqlParameter With {.Value = filaPartipante("ApeMat"), _
                                        .ParameterName = "apMaterno", _
                                        .DbType = DbType.String})


                            ocom.Parameters.Add(New SqlParameter With {.Value = filaPartipante("cod_trabajador"), _
                                           .ParameterName = "cod_trabajador", _
                                           .DbType = DbType.Int32})



                            ocom.Parameters.Add(New SqlParameter With {.Size = 200, _
                                                       .ParameterName = "mensaje", _
                                                       .DbType = DbType.String, .Direction = ParameterDirection.Output})

                            ocom.Parameters.Add(New SqlParameter With {.Size = 10, _
                                          .ParameterName = "codigo", _
                                          .DbType = DbType.Int32, .Direction = ParameterDirection.Output})
                            Dim mensajeOut As String = ""
                            Dim codOut As Integer = 0
                           

                           
                            '@mensaje varchar(max) out ,
                            '@codigo int out 


                            Dim ids3 As Integer = 0
                            ocom.ExecuteScalar()

                            mensajeOut = ocom.Parameters("mensaje").Value

                            codOut = ocom.Parameters("codigo").Value

                            If codOut = 0 Then
                                Continue For
                            End If

                            'cod_trabajador()
                            '129:
                            '                            ApePat	ApeMat	Nombres
                            'OBREGÓN	BRAVO	MARÍA JULIANA
                        Next


                        '@DPEP_CodigoDetalleProgramacionEntrevistaParticipantes int, 
                        '@RPE_CodigoProgramacionEntrevista int ,
                        '@TJ_CodigoTrabajadorParticipante int ,
                        '@nombre varchar(max),
                        '@apPaterno varchar(max),
                        '@apMaterno varchar(max),
                        '@cod_trabajador int 






                        ''
                        'If CountFilas = 10 Then
                        '    Exit For
                        'End If

                    Next

                    otran.Commit()




                End Using

            End Using


            'otran.Rollback()

            Return 1

        Catch ex As Exception
            Dim filaTemp As DataRow = filaACtual
            Throw ex
            otran.Commit()
            Return 0
            ' otran.Rollback()
            'Return 0
        End Try
    End Function


End Class
 