Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Namespace ModuloNotas

    Public Class da_RegistroComponentes
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

        Public Function FUN_INS_RegistroComponentes(ByVal obe_RegistroComponentes As be_RegistroComponentes, ByVal int_pocision As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroComponentes")
                dbBase.AddInParameter(cmd, "@P_CodigoRegistroComponentes", DbType.Int32, obe_RegistroComponentes.CodigoRegistroComponentes)
                dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int32, obe_RegistroComponentes.CodigoAsignacionGrupo)
                dbBase.AddInParameter(cmd, "@p_CodigoComponente", DbType.Int32, obe_RegistroComponentes.CodigoComponente)
                dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, obe_RegistroComponentes.CodigoBimestre)

                dbBase.AddInParameter(cmd, "@p_pocision", DbType.Int32, int_pocision)


                dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd)
                '@P_CodigoRegistroComponentes int ,    
                '@p_CodigoAsignacionGrupo int,    
                '@p_CodigoComponente int,    
                '@p_CodigoBimestre int, 

                '@p_pocision int,   

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



        Public Function FUN_UPD_RegistroComponentes(ByVal obe_RegistroComponentes As be_RegistroComponentes, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal b_estado As Boolean) As List(Of String)
            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UPD_RegistroComponentes")
                dbBase.AddInParameter(cmd, "@P_CodigoRegistroComponentes", DbType.Int32, obe_RegistroComponentes.CodigoRegistroComponentes)
                dbBase.AddInParameter(cmd, "@P_CodigoComponente", DbType.Int32, obe_RegistroComponentes.CodigoComponente)
                dbBase.AddInParameter(cmd, "@P_Estado", DbType.Boolean, b_estado)



                dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd)



                '@P_CodigoRegistroComponentes int ,
                '@P_CodigoComponente INT ,

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

        ' funcion para actualizar la pocision del registro componente
        
        Public Function FUN_UPD_ActualizarPocisionRegistroComponente(ByVal intPocision As Integer, ByVal intCodRegistroComponente As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal b_estado As Boolean) As List(Of String)
            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_ACT_PocisionRegistroComponente")
                dbBase.AddInParameter(cmd, "@RC_CodigoRegistroComponentes", DbType.Int32, intCodRegistroComponente)
                dbBase.AddInParameter(cmd, "@p_orden", DbType.Int32, intPocision)

                dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd)



                '@P_CodigoRegistroComponentes int ,
                '@P_CodigoComponente INT ,

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

        'CU_USP_ACT_PocisionRegistroComponente


#End Region

#Region "Metodos No Transaccionales"


        Public Function FUN_LIS_RegistroComponentes(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal b_estadoComponente As Boolean, ByVal b_estadoIndicador As Boolean, ByVal b_estado_subIndicador As Boolean) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_lIS_RegistroS")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int32, int_CodigoAsignacionGrupo)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            dbBase.AddInParameter(cmd, "@p_estadoComponente", DbType.Boolean, b_estadoComponente)
            dbBase.AddInParameter(cmd, "@p_estadoIndicador", DbType.Boolean, b_estadoIndicador)
            dbBase.AddInParameter(cmd, "@p_estado_subIndicador", DbType.Boolean, b_estado_subIndicador)


            '@p_estadoComponente bit,
            '@p_estadoIndicador bit,
            '@p_estado_subIndicador bit

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)



        End Function

#End Region

    End Class

End Namespace