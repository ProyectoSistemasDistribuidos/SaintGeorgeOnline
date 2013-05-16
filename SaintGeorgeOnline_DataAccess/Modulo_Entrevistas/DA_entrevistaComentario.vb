
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloEntrevistas
Imports System.Reflection
Public Class DA_entrevistaComentario
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

    Public Function FInsertarComentarioEntrevista(ByVal lst As List(Of Dictionary(Of String, Object)))
        Try

            BeginTransaction()

            For Each o In lst
                Dim dbCommand As DbCommand
                Dim usp_mensaje1 = "", usp_mensaje2 As String = ""
                Dim usp_Valor1 = 0, usp_Valor2 As Integer = 0



               


                dbCommand = dbBase.GetStoredProcCommand("USP_Ins_ET_RegistroComentariosEntrevistas")
                dbCommand.Parameters.Clear()

                dbBase.AddInParameter(dbCommand, "@RCE_CodigoRegistroComentarioEntrevista", DbType.Int32, CInt(o("codRegCometario")))
                dbBase.AddInParameter(dbCommand, "@RPE_CodigoProgramacionEntrevista", DbType.Int32, CInt(o("codEntrevista")))

                dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajadorProfesor", DbType.Int32, CInt(o("codTrabajador")))
                dbBase.AddInParameter(dbCommand, "@AGC_CodigoAsignacionGrupo", DbType.Int32, CInt(o("codGrupo")))

                dbBase.AddInParameter(dbCommand, "@RCE_Comentario", DbType.String, CStr(o("cometario")))

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand, tran)

                usp_mensaje1 = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
                usp_Valor1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))

                If usp_Valor1 = 0 Then
                    Rollback()
                    Return 0
                End If
                 
            Next
            Commit()

            Return 1

        Catch ex As Exception
            Rollback()
            Return 0
        End Try
    End Function

End Class
