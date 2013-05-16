Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
 

Public Class DA_persona
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




#Region "Persona"

    Public Function F_insertarPersona(ByVal dcPersona As Dictionary(Of String, Object)) As Dictionary(Of String, String)
        Dim dc As Dictionary(Of String, String)
        dc = New Dictionary(Of String, String)
        Try


            Dim codigo1 As Integer = 0
            Dim mensaje1 As String = ""
            ''
            Dim codigo2 As Integer = 0
            Dim mensaje2 As String = ""
            ''
            Dim codigo3 As Integer = 0
            Dim mensaje3 As String = ""
            BeginTransaction()
            dbCommand = dbBase.GetStoredProcCommand("USP_InsPersonas")
            dbCommand.Parameters.Clear()
            dbBase.AddInParameter(dbCommand, "@PE_CodigoPersona", DbType.Int32, dcPersona("codPersona"))
            dbBase.AddInParameter(dbCommand, "@PE_ApellidoPaterno", DbType.String, dcPersona("apellidoPAterno").ToString().Trim())
            dbBase.AddInParameter(dbCommand, "@PE_ApellidoMaterno", DbType.String, dcPersona("apellidoMAterno").ToString().Trim())
            dbBase.AddInParameter(dbCommand, "@PE_Nombre", DbType.String, dcPersona("nombre").ToString().Trim())

            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
            'dbBase.AddOutParameter(dbCommand, "@operacion", DbType.Int32, 100)

            dbBase.ExecuteScalar(dbCommand, tran)
            codigo1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
            mensaje1 = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))

            If codigo1 = 0 Then

                dc("codigo") = codigo1
                dc("mensaje") = mensaje1

                Rollback()

                Return dc
            End If

            If codigo1 = -2 Then

                dc("codigo") = codigo1
                dc("mensaje") = mensaje1

                Rollback()

                Return dc
            End If
            ''------------------------------------------------------------------------------
            dbCommand = dbBase.GetStoredProcCommand("USP_InsTrabajador")
            dbCommand.Parameters.Clear()

            dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajador", DbType.Int32, dcPersona("codTrab"))
            dbBase.AddInParameter(dbCommand, "@PE_CodigoPersona", DbType.Int32, codigo1)

            dbBase.AddInParameter(dbCommand, "@TJ_CorreoCorporativo", DbType.String, dcPersona("correoCorporativo"))
            dbBase.AddInParameter(dbCommand, "@TJ_UsuarioSkype", DbType.String, dcPersona("correoSkype"))

            dbBase.AddInParameter(dbCommand, "@TJ_Ensenia", DbType.Boolean, dcPersona("codEnsenia"))
            dbBase.AddInParameter(dbCommand, "@TJ_EstadoAcceso", DbType.Boolean, dcPersona("codAcceso"))

            dbBase.AddInParameter(dbCommand, "@TJ_Usuario", DbType.String, dcPersona("usuario"))
            dbBase.AddInParameter(dbCommand, "@TJ_Contrasenia", DbType.String, dcPersona("pass"))

            dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajadoreAsistencia", DbType.Int32, dcPersona("TJ_CodigoTrabajadoreAsistencia"))



            dbBase.AddInParameter(dbCommand, "@esAsistente", DbType.Boolean, dcPersona("esAsistente"))
            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
            'dbBase.AddOutParameter(dbCommand, "@operacion", DbType.Int32, 100)


            dbBase.ExecuteScalar(dbCommand, tran)
            codigo2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
            mensaje2 = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))

            'If codigo2 = -5 Then
            '    dc("codigo") = codigo2
            '    dc("mensaje") = mensaje2
            '    Commit()
            '    Return dc
            'End If

            ''------------------------------------------------------------------------------
            dbCommand = dbBase.GetStoredProcCommand("USP_insCF_RelacionTrabajadoresPerfiles")
            dbCommand.Parameters.Clear()


            dbBase.AddInParameter(dbCommand, "@RTPS_Codigo", DbType.Int32, dcPersona("codRelacionPerfil"))
            dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajador", DbType.Int32, codigo2)
            dbBase.AddInParameter(dbCommand, "@PS_CodigoPerfil", DbType.Int32, dcPersona("codPerfil"))

            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
            ' dbBase.AddOutParameter(dbCommand, "@operacion", DbType.Int32, 100)

            dbBase.ExecuteScalar(dbCommand, tran)
            codigo3 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
            mensaje3 = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))





            If codigo1 = 0 Or codigo2 = 0 Or codigo3 = 0 Then
                Rollback()
                dc("codigo") = 0
                dc("mensaje") = "Error al registrar"
                Return dc
            End If
            Commit()
            dc("codigo") = codigo1
            dc("mensaje") = mensaje1

            Return dc

        Catch ex As Exception
            Rollback()
            dc("codigo") = -1
            dc("mensaje") = "error de sistema"
            Return dc

        End Try


    End Function

    Public Function F_insetarPerfil(ByVal dcPerfil As Dictionary(Of String, Object)) As Integer
        Dim codigo1 As Integer = 0
        Dim mensaje1 As String = ""
        Try


           

            BeginTransaction()
            dbCommand = dbBase.GetStoredProcCommand("USP_InsCF_Perfiles")
            dbCommand.Parameters.Clear()


            dbBase.AddInParameter(dbCommand, "@PS_CodigoPerfil", DbType.Int32, dcPerfil("codRegPerfil"))
            dbBase.AddInParameter(dbCommand, "@PS_Descripcion", DbType.String, dcPerfil("nombrePerfil").ToString().Trim())
            dbBase.AddInParameter(dbCommand, "@PS_Estado", DbType.Boolean, True)
            dbBase.AddInParameter(dbCommand, "@PS_TipoPerfil", DbType.Int32, dcPerfil("codTipoPerfil"))

            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)

            dbBase.ExecuteScalar(dbCommand, tran)

            codigo1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
            mensaje1 = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))

            If codigo1 = 0 Then
                Rollback()
                Return 0
            Else

                Commit()
                Return codigo1
            End If
          
        Catch ex As Exception
            Rollback()
            Return 0
        End Try
    End Function
    Public Function F_ActualizarUsuarioPass(ByVal dcUsuarioPass As Dictionary(Of String, Object)) As Integer
        Dim codigo1 As Integer = 0
        Dim mensaje1 As String = ""
        Try
            BeginTransaction()
            dbCommand = dbBase.GetStoredProcCommand("USP_UDP_Trabajador")
            dbCommand.Parameters.Clear()
            dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajador", DbType.Int32, dcUsuarioPass("codTrab"))
            dbBase.AddInParameter(dbCommand, "@TJ_Usuario", DbType.String, dcUsuarioPass("usuario"))
            dbBase.AddInParameter(dbCommand, "@TJ_Contrasenia", DbType.String, dcUsuarioPass("pass"))
            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
            dbBase.ExecuteScalar(dbCommand, tran)
            codigo1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
            mensaje1 = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
            If codigo1 = 0 Then
                Rollback()
                Return 0
            Else
                Commit()
                Return 1
            End If
        Catch ex As Exception
            Rollback()
            Return 0
        End Try
    End Function


#End Region



End Class
