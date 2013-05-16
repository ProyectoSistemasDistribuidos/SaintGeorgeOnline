Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Imports SaintGeorgeOnline_BusinessEntities

Public Class da_RegistroNotasSubIndicadores
    Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"

    Private dbBase As SqlDatabase 'ExecuteDataSet
    Private dbCommand As DbCommand 'ExecuteScalar

#End Region

#Region "Constructor"

    Public Sub New()
        dbBase = New SqlDatabase(Me.SqlConexionDB)
    End Sub

#End Region

#Region "Metodos Transaccionales"

    Public Function FUN_ACT_RegistroNotaSubIndicador(ByVal obe_registroNotaSubIndicadores As be_registroNotaSubIndicadores, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

        Dim lstResultado As New List(Of String)
        Try
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UDP_RegistroNotasSubIndicadores")

            dbBase.AddInParameter(cmd, "@P_CodigoRegistroNotaSubIndicador", DbType.Int32, obe_registroNotaSubIndicadores.int_CodigoRegistroNotaSubIndicador)
            dbBase.AddInParameter(cmd, "@P_NotaSubindicador", DbType.String, obe_registroNotaSubIndicadores.str_NotaSubindicador)

            '@P_CodigoRegistroNotaSubIndicador INT ,
            '@P_NotaSubindicador VARCHAR(MAX),

            dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
            dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            dbBase.ExecuteScalar(cmd)

            str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
            p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
            lstResultado.Add(str_Mensaje)
            lstResultado.Add(p_Valor.ToString())
            Return lstResultado



        Catch ex As Exception

        End Try
    End Function




#End Region

#Region "Metodos No Transaccionales"



#End Region



End Class
