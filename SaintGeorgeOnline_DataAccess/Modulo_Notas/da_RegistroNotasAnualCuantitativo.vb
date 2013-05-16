Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Namespace ModuloNotas

    Public Class da_RegistroNotasAnualCuantitativo
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

        Public Function FUN_INS_RegistroNotasAnualCuantitativo( _
            ByVal obj_RegistroNotasAnualCualitativo As be_RegistroNotasAnualCuantitativo, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim str_Mensaje As String = ""
                Dim int_Valor As Integer = 0
                Dim str_MensajeDetalle As String = ""
                Dim int_ValorDetalle As Integer = 0

                dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotasAnualCuantitativo")

                dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionGrupo", DbType.Int32, obj_RegistroNotasAnualCualitativo.CodigoAsignacionGrupo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, obj_RegistroNotasAnualCualitativo.CodigoAnioAcademico)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, obj_RegistroNotasAnualCualitativo.CodigoAlumno)

                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                lstResultado.Add(str_Mensaje)
                lstResultado.Add(int_Valor.ToString())

                If int_Valor > 0 Then
                    Dim lsta As New List(Of String)
                    For i = 1 To 4
                        lsta.Clear()
                        dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotasBimestralesCuantitativas")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int32, i)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroAnual", DbType.Int32, int_Valor)

                        dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                        dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        str_MensajeDetalle = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                        int_ValorDetalle = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                        lsta.Add(str_MensajeDetalle)
                        lsta.Add(int_ValorDetalle.ToString())
                        dbCommand.Parameters.Clear()

                        If Not int_ValorDetalle > 0 Then
                            Rollback()
                            Return lsta
                            'Exit For
                        End If

                    Next

                    Commit()
                Else
                    Rollback()
                End If

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


        Public Function FUN_INS_RegistroNotasAnualCuantitativoHistorica( _
           ByVal lstNotas As List(Of be_RegistroNotasAnualCuantitativo), ByRef str_mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()

                Dim str_MensajeDetalle As String = ""
                Dim int_ValorDetalle As Integer = 0

                For Each iNota As be_RegistroNotasAnualCuantitativo In lstNotas

                    If Not iNota.CodigoAsignacionGrupo > 0 Then
                        Continue For
                    End If


                    dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotasAnualCuantitativoHistorico")
                    dbCommand.Parameters.Clear()

                    dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroNotaAnualCuantitativo", DbType.Int32, iNota.CodigoRegistroAnual)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionGrupo", DbType.Int32, iNota.CodigoAsignacionGrupo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, iNota.CodigoAnioAcademico)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, iNota.CodigoAlumno)
                    dbBase.AddInParameter(dbCommand, "@p_NotaAnual", DbType.Int32, IIf(iNota.NotaAnual > 0, iNota.NotaAnual, DBNull.Value))

                    dbBase.AddInParameter(dbCommand, "@p_Nota1", DbType.Decimal, IIf(iNota.auxNota1 > 0, iNota.auxNota1, DBNull.Value))
                    dbBase.AddInParameter(dbCommand, "@p_Nota2", DbType.Decimal, IIf(iNota.auxNota2 > 0, iNota.auxNota2, DBNull.Value))
                    dbBase.AddInParameter(dbCommand, "@p_Nota3", DbType.Decimal, IIf(iNota.auxNota3 > 0, iNota.auxNota3, DBNull.Value))
                    dbBase.AddInParameter(dbCommand, "@p_Nota4", DbType.Decimal, IIf(iNota.auxNota4 > 0, iNota.auxNota4, DBNull.Value))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroNotaBimestralCuantitativo1", DbType.Int32, iNota.auxCodigoRegistroBimestral1)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroNotaBimestralCuantitativo2", DbType.Int32, iNota.auxCodigoRegistroBimestral2)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroNotaBimestralCuantitativo3", DbType.Int32, iNota.auxCodigoRegistroBimestral3)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroNotaBimestralCuantitativo4", DbType.Int32, iNota.auxCodigoRegistroBimestral4)

                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                    dbBase.ExecuteScalar(dbCommand, tran)

                    str_MensajeDetalle = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    int_ValorDetalle = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                    If Not int_ValorDetalle > 0 Then
                        Rollback()
                        Return int_ValorDetalle
                    End If

                Next

                Commit()
                str_mensaje = str_MensajeDetalle
                Return int_ValorDetalle

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


#End Region

    End Class

End Namespace
