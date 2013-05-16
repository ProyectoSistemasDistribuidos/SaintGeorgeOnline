Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Namespace ModuloNotas

    Public Class da_RegistroIndicadores
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

        Public Function FUN_INS_RegistroIndicadores(ByVal obe_RegistroIndicadores As be_RegistroIndicadores, ByVal int_orden As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroIndicadores")



                dbBase.AddInParameter(cmd, "@p_CodigoIndicador", DbType.Int32, obe_RegistroIndicadores.CodigoIndicador)
                dbBase.AddInParameter(cmd, "@p_CodigoRegistroComponentes", DbType.Int32, obe_RegistroIndicadores.CodigoRegistroComponentes)

                dbBase.AddInParameter(cmd, "@orden", DbType.Int32, int_orden)


                dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd)


                '@p_CodigoIndicador INT ,   
                '@p_CodigoRegistroComponentes INT ,  
                '@orden int,  
                '@p_Mensaje VARCHAR(255) OUTPUT,              
                '@p_Valor INT OUTPUT  ,    
                '@p_CodigoUsuario  INT ,    
                '@p_CodigoTipoUsuario  INT,    
                '@p_CodigoModulo  INT,    
                '@p_CodigoOpcion  INT 


                str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
                Return lstResultado



            Catch ex As Exception

            End Try
        End Function

        ''
        Public Function FUN_INS_RegistroPocicionIndicadore(ByVal int_posIndicador As Integer, ByVal codRegIndicador As Integer) As List(Of String)
            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_ACT_PocisionRegistroIndicador")



                dbBase.AddInParameter(cmd, "@p_CodigoRegistroIndicadores", DbType.Int32, codRegIndicador)
                dbBase.AddInParameter(cmd, "@p_pocisionRegistroIndicador", DbType.Int32, int_posIndicador)


 

                'dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                'dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                'dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                'dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                'dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                'dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd)




                'str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
                'p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
                'lstResultado.Add(str_Mensaje)
                'lstResultado.Add(p_Valor.ToString())

                Return lstResultado



            Catch ex As Exception

            End Try
        End Function

        ''
        Public Function FUN_UPD_RegistroIndicadores(ByVal obe_RegistroIndicadores As be_RegistroIndicadores, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal b_estado As Boolean) As List(Of String)
            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UPD_RegistroIndicadores")

                dbBase.AddInParameter(cmd, "@p_CodigoRegistroIndicadores", DbType.Int32, obe_RegistroIndicadores.CodigoRegistroIndicadores)
                dbBase.AddInParameter(cmd, "@p_CodigoIndicador", DbType.Int32, obe_RegistroIndicadores.CodigoIndicador)
                dbBase.AddInParameter(cmd, "@P_estado", DbType.Boolean, b_estado)
                '                @p_CodigoRegistroIndicadores int,
                '@p_CodigoIndicador INT , 
                '@P_estado bit ,


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

End Namespace
