Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Imports SaintGeorgeOnline_BusinessEntities

Public Class da_RegistroNotasIndicadores
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

    Public Function FUN_ACT_RegistroNotaIndicador(ByVal obe_CU_RegistroNotasIndicadores As be_CU_RegistroNotasIndicadores, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

        Dim lstResultado As New List(Of String)
        Try
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UDP_RegistroNotasIndicadores")


            '@P_CodigoRegistroNotaIndicador INT ,  
            '@P_NotaIndicador VARCHAR(MAX),  
            '@p_Mensaje VARCHAR(255) OUTPUT,            
            '@p_Valor INT OUTPUT  , 

            '@pCodComponente  int  OUTPUT ,
            '@pNotComponente  varchar(max) OUTPUT,

            '@p_CodigoUsuario  INT ,  
            '@p_CodigoTipoUsuario  INT,  
            '@p_CodigoModulo  INT,  
            '@p_CodigoOpcion  INT 


            dbBase.AddInParameter(cmd, "@P_CodigoRegistroNotaIndicador", DbType.Int32, obe_CU_RegistroNotasIndicadores.int_CodigoRegistroNotaIndicador)
            dbBase.AddInParameter(cmd, "@P_NotaIndicador", DbType.String, obe_CU_RegistroNotasIndicadores.str_NotaIndicador)
            '@P_CodigoRegistroNotaIndicador INT ,
            '@P_NotaIndicador VARCHAR(MAX),

            dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
            dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)

            dbBase.AddOutParameter(cmd, "@pCodComponente", DbType.Int32, 255)
            dbBase.AddOutParameter(cmd, "@pNotComponente", DbType.String, 100)
            dbBase.AddOutParameter(cmd, "@p_promedio", DbType.String, 100)




            '@p_Valor = @P_CodigoRegistroNotaIndicador,        
            '@p_Mensaje = 'Operación exitosa.'  ,
            '@pCodComponente =@codReNotaComponente,
            '@pNotComponente=DBO.cu_devolver_EquivalenciaComponente(@acComponente)


            '@p_Valor = @P_CodigoRegistroNotaIndicador,        
            '@p_Mensaje = 'Operación exitosa.'  ,
            '@pCodComponente =@codReNotaComponente,
            '@pNotComponente=DBO.cu_devolver_EquivalenciaComponente(@acComponente)


            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            dbBase.ExecuteScalar(cmd)

            str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
            p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
            lstResultado.Add(str_Mensaje)
            lstResultado.Add(p_Valor.ToString())

            lstResultado.Add(dbBase.GetParameterValue(cmd, "@pCodComponente").ToString())
            lstResultado.Add(dbBase.GetParameterValue(cmd, "@pNotComponente").ToString())
            lstResultado.Add(dbBase.GetParameterValue(cmd, "@p_promedio").ToString())





            Return lstResultado



        Catch ex As Exception

        End Try
    End Function




#End Region

#Region "Metodos No Transaccionales"



#End Region


End Class
