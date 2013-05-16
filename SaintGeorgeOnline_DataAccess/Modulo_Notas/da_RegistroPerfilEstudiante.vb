Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Namespace ModuloNotas

    Public Class da_RegistroPerfilEstudiante
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

        Public Function FUN_INS_RegistroPerfilEstudiante( _
            ByVal arr_RegistroPerfilEstudiante As List(Of be_RegistroPerfilEstudiante), _
            ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0

                'Inicio la transaccion
                BeginTransaction()

                For Each obj_be_RegistroPerfilEstudiante As be_RegistroPerfilEstudiante In arr_RegistroPerfilEstudiante

                    If (obj_be_RegistroPerfilEstudiante.MiCheck > 0 Or _
                        (obj_be_RegistroPerfilEstudiante.MiCheck = 0 And obj_be_RegistroPerfilEstudiante.CodigoRegistroPerfil > 0)) Then

                        If obj_be_RegistroPerfilEstudiante.CodigoRegistroPerfil > 0 Then ' update
                            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_RegistroPerfilEstudiante")
                            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroPerfil", DbType.Int16, obj_be_RegistroPerfilEstudiante.CodigoRegistroPerfil)
                            dbBase.AddInParameter(dbCommand, "@p_MiCheck", DbType.Int16, obj_be_RegistroPerfilEstudiante.MiCheck)
                        Else ' insert
                            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_RegistroPerfilEstudiante")
                        End If

                        dbBase.AddInParameter(dbCommand, "@p_CodigoCriterio", DbType.Int16, obj_be_RegistroPerfilEstudiante.CodigoCriterio)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoCalificativo", DbType.Int16, obj_be_RegistroPerfilEstudiante.CodigoCalificativo)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, obj_be_RegistroPerfilEstudiante.CodigoAlumno)
                        dbBase.AddInParameter(dbCommand, "@p_Nota", DbType.String, obj_be_RegistroPerfilEstudiante.Nota)

                        dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)

                        dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                        dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                        dbBase.ExecuteScalar(dbCommand, tran)

                        str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                        p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                        If Not p_Valor > 0 Then
                            Rollback()
                        End If

                    End If

                Next

                Commit()

                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
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


#End Region

    End Class

End Namespace