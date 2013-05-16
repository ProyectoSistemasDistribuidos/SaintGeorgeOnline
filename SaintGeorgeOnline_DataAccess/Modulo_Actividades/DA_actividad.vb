Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Public Class DA_actividad
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

#Region "transaccional "

    Public Function F_insertarActividad(ByVal dcACtividad As Dictionary(Of String, Object)) As Dictionary(Of Object, Object)
        Dim dcResultado As New Dictionary(Of Object, Object)
        Try
            BeginTransaction()
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            Dim codigo1, codigo2, codigo3, codigo4, codigo5 As Integer
            Dim mensaje1, mensaje2, mensaje3, mensaje4, mensaje5 As String
            dbCommand = dbBase.GetStoredProcCommand("USP_InsAC_Actividades")

            dbBase.AddInParameter(dbCommand, "@PA_CodigoProgramacionActividad", DbType.Int32, CInt(dcACtividad("PA_CodigoProgramacionActividad")))
            dbBase.AddInParameter(dbCommand, "@PA_FechaInicio", DbType.DateTime, Convert.ToDateTime(dcACtividad("fechaInicio")))
            dbBase.AddInParameter(dbCommand, "@PA_FechaFin", DbType.DateTime, Convert.ToDateTime(dcACtividad("fechaFin")))
            dbBase.AddInParameter(dbCommand, "@PA_HoraInicio", DbType.DateTime, Convert.ToDateTime(dcACtividad("hraInicio")))
            dbBase.AddInParameter(dbCommand, "@PA_HoraFin", DbType.DateTime, Convert.ToDateTime(dcACtividad("hraFin")))
            'dbBase.AddInParameter(dbCommand, "@PA_CodigoProgramacionActividad", DbType.Int32, dcACtividad("PA_CodigoProgramacionActividad"))
            dbBase.AddInParameter(dbCommand, "@PA_Estado", DbType.Boolean, True)
            dbBase.AddInParameter(dbCommand, "@PA_NombreActividad", DbType.String, dcACtividad("nombreActividad").ToString())

            dbBase.AddInParameter(dbCommand, "@TA_CodigoTipoActividad", DbType.Int32, CInt(dcACtividad("tipoActividad")))

            dbBase.AddInParameter(dbCommand, "@PA_ObjetivoActividad", DbType.String, dcACtividad("objetivo").ToString())
            dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajadorOrganizador", DbType.Int32, CInt(dcACtividad("organizador")))
            dbBase.AddInParameter(dbCommand, "@PA_DescripcionActividad", DbType.String, DBNull.Value)
            dbBase.AddInParameter(dbCommand, "@PA_NumProfesores", DbType.Int32, CType(dcACtividad("numeroDocente"), Integer))

            dbBase.AddInParameter(dbCommand, "@PA_NumAlumnos", DbType.Int32, CInt(dcACtividad("numeroAlumnos")))
            dbBase.AddInParameter(dbCommand, "@PA_NumPadres", DbType.Int32, CInt(dcACtividad("numeroPadres")))

            dbBase.AddInParameter(dbCommand, "@PA_NumAsistentesAula", DbType.Int32, CType(dcACtividad("numeroAsistentes"), Integer))
            dbBase.AddInParameter(dbCommand, "@PA_ReqTecnologicos", DbType.String, DBNull.Value)
            dbBase.AddInParameter(dbCommand, "@PA_ReqLogistica", DbType.Int32, DBNull.Value)
            dbBase.AddInParameter(dbCommand, "@PA_ReqInfraestructura", DbType.String, DBNull.Value)

            dbBase.AddInParameter(dbCommand, "@PA_Lugar", DbType.String, dcACtividad("lugar"))

            dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajadorRegistrador", DbType.String, dcACtividad("codUsuario"))


            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)


            dbBase.ExecuteScalar(dbCommand, tran)


            mensaje1 = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
            codigo1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))

            dcResultado("mensaje") = mensaje1
            dcResultado("codigo") = codigo1


            If codigo1 = 0 Then
                Rollback()
                dcResultado("mensaje") = mensaje1
                dcResultado("codigo") = codigo1

                Return dcResultado
            End If


            If CInt(dcACtividad("PA_CodigoProgramacionActividad")) <> 0 Then
                dbCommand = dbBase.GetStoredProcCommand("USP_DElAC_ActividadesGrados")
                dbBase.AddInParameter(dbCommand, "@PA_CodigoProgramacionActividad", DbType.Int32, codigo1)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                dbBase.ExecuteScalar(dbCommand, tran)

            End If


            For Each codGrado As String In CType(dcACtividad("grados"), List(Of String))
                dbCommand = dbBase.GetStoredProcCommand("USP_InsAC_ActividadesGrados")
                dbBase.AddInParameter(dbCommand, "@AG_CodigoActividadGrado", DbType.Int32, 0)
                dbBase.AddInParameter(dbCommand, "@PA_CodigoProgramacionActividad", DbType.Int32, codigo1)
                dbBase.AddInParameter(dbCommand, "@GD_CodigoGrado", DbType.Int32, CInt(codGrado.ToString().Trim))

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                dbBase.ExecuteScalar(dbCommand, tran)


                mensaje2 = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
                codigo2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))



                If codigo2 = 0 Then
                    Rollback()
                    dcResultado("mensaje") = mensaje2
                    dcResultado("codigo") = codigo2

                    Return dcResultado
                End If

            Next

            If CInt(dcACtividad("PA_CodigoProgramacionActividad")) <> 0 Then
                dbCommand = dbBase.GetStoredProcCommand("USP_DelAC_ActividadesTipoPersonas")
                dbBase.AddInParameter(dbCommand, "@PA_CodigoProgramacionActividad", DbType.Int32, codigo1)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                dbBase.ExecuteScalar(dbCommand, tran)
            End If

            For Each codDest As String In CType(dcACtividad("dirigido"), List(Of String))
                dbCommand = dbBase.GetStoredProcCommand("USP_InsAC_ActividadesTipoPersonas")
                dbBase.AddInParameter(dbCommand, "@ATP_CodigoActividadTipoPersona", DbType.Int32, 0)
                dbBase.AddInParameter(dbCommand, "@TPA_CodigoTipoPersonaActividad", DbType.Int32, CInt(codDest.Trim))
                dbBase.AddInParameter(dbCommand, "@PA_CodigoProgramacionActividad", DbType.Int32, codigo1)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                dbBase.ExecuteScalar(dbCommand, tran)

                mensaje3 = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
                codigo3 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                If codigo3 = 0 Then
                    Rollback()
                    dcResultado("mensaje") = mensaje3
                    dcResultado("codigo") = codigo3

                    Return dcResultado
                End If
            Next

            If CInt(dcACtividad("PA_CodigoProgramacionActividad")) <> 0 Then
                dbCommand = dbBase.GetStoredProcCommand("USP_DELAC_ActividadesProfesores")
                dbBase.AddInParameter(dbCommand, "@PA_CodigoProgramacionActividad", DbType.Int32, codigo1)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                dbBase.ExecuteScalar(dbCommand, tran)
            End If


            For Each Asist As String In CType(dcACtividad("listaAsistentes"), List(Of String))
                dbCommand = dbBase.GetStoredProcCommand("USP_InsAC_ActividadesProfesores")
                dbBase.AddInParameter(dbCommand, "@AP_CodigoActividadesProfesores", DbType.Int32, 0)
                dbBase.AddInParameter(dbCommand, "@PA_CodigoProgramacionActividad", DbType.Int32, codigo1)
                dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajador", DbType.Int32, CInt(Asist.Trim()))

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                dbBase.ExecuteScalar(dbCommand, tran)


                mensaje4 = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
                codigo4 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))


                If codigo4 = 0 Then
                    Rollback()
                    dcResultado("mensaje") = mensaje4
                    dcResultado("codigo") = codigo4
                    Return dcResultado
                End If


            Next


            For Each Doc As String In CType(dcACtividad("ListaDocentes"), List(Of String))
                dbCommand = dbBase.GetStoredProcCommand("USP_InsAC_ActividadesProfesores")
                dbBase.AddInParameter(dbCommand, "@AP_CodigoActividadesProfesores", DbType.Int32, 0)
                dbBase.AddInParameter(dbCommand, "@PA_CodigoProgramacionActividad", DbType.Int32, codigo1)
                dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajador", DbType.Int32, CInt(Doc.Trim))

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                dbBase.ExecuteScalar(dbCommand, tran)


                mensaje5 = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
                codigo5 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))

                If codigo5 = 0 Then
                    Rollback()
                    dcResultado("mensaje") = mensaje5
                    dcResultado("codigo") = codigo5
                    Return dcResultado
                End If
            Next




            Commit()

            Return dcResultado

        Catch ex As Exception
            dcResultado("mensaje") = "Error del Sistema"
            dcResultado("codigo") = 0
            Rollback()

            dbCommand.Connection.Close()
            dbCommand.Dispose()

            Return dcResultado
        Finally
            dbCommand.Connection.Close()
            dbCommand.Dispose()
        End Try


    End Function


    Public Function F_eliminarACtividad(ByVal codActividad As Integer) As Dictionary(Of Object, Object)
        Dim dc As New Dictionary(Of Object, Object)
        Try
            Dim codigo1, codigo2, codigo3, codigo4, codigo5 As Integer
            Dim mensaje1, mensaje2, mensaje3, mensaje4, mensaje5 As String
            dbCommand = dbBase.GetStoredProcCommand("USP_DELAC_Actividades")

            '            @PA_CodigoProgramacionActividad int,
            '@codigo int out ,
            '@mensaje varchar (max) out  


            dbBase.AddInParameter(dbCommand, "@PA_CodigoProgramacionActividad", DbType.Int32, codActividad)
            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
            dbBase.ExecuteScalar(dbCommand)

            mensaje4 = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
            codigo4 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))


            dc("mensaje") = mensaje4
            dc("codigo") = codigo4
            Return dc


        Catch ex As Exception
            dc("mensaje") = ex.Message.ToString()
            dc("codigo") = -1
            Return dc
        End Try
    End Function


    Public Function F_actualizarReq(ByVal dcActividad As Dictionary(Of String, Object)) As Dictionary(Of String, Object)
        Dim dcMensaje As New Dictionary(Of String, Object)
        Try
            Dim mensaje1 As String = ""
            Dim cod As Integer = 0

            dbCommand = dbBase.GetStoredProcCommand("USP_UPD_actividad")

            dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionActividad", DbType.Int32, CInt(dcActividad("codACtividad")))

            dbBase.AddInParameter(dbCommand, "@p_ReqTecnologicos", DbType.String, Convert.ToString(dcActividad("requerimientoSis")))
            dbBase.AddInParameter(dbCommand, "@p_ReqLogistica", DbType.String, Convert.ToString(dcActividad("requerimientoLog")))
            dbBase.AddInParameter(dbCommand, "@p_ReqInfraestructura", DbType.String, Convert.ToString(dcActividad("requerimientoInf")))
            dbBase.AddInParameter(dbCommand, "@p_Comentarios", DbType.String, Convert.ToString(dcActividad("comentario")))

            dbBase.AddOutParameter(dbCommand, "@p_codigo", DbType.Int32, 255)
            dbBase.AddOutParameter(dbCommand, "@p_mensaje", DbType.String, 100)


            dbBase.ExecuteScalar(dbCommand)

            mensaje1 = dbBase.GetParameterValue(dbCommand, "@p_mensaje").ToString()
            cod = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_codigo")))
            dcMensaje("mensaje") = mensaje1
            dcMensaje("codigo") = cod



            '  codACtividad:codActividadEdicion,
            'requerimientoLog: $("#txtReqLog").val(),
            '  requerimientoInf: $("#txtReqInf").val(),
            '   requerimientoSis: $("#txtReqSist").val(),
            '  comentario: $("#txtComentario").val()

            Return dcMensaje
          

        Catch ex As Exception
        Finally
            dbCommand.Connection.Close()
            dbCommand.Dispose()
        End Try
    End Function
#End Region

#Region "Metodos No Transaccionales"

    Public Function FUN_GET_ActividadImp( _
        ByVal int_CodigoActividad As Integer, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AC_USP_GET_ActividadesImp")

        'Parámetros de entrada
        dbBase.AddInParameter(cmd, "@p_CodigoProgramacionActividad", DbType.Int32, int_CodigoActividad)

        dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
        dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
        dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
        dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)




        'Ejecucion del Store Procedure
        Return dbBase.ExecuteDataSet(cmd)

    End Function

#End Region


End Class
