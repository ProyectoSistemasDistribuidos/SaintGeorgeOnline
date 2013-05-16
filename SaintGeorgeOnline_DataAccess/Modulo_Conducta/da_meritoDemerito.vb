Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Public Class da_meritoDemerito
    Inherits InstanciaConexion.ManejadorConexion
#Region "Atributos"
    Private dbBase As SqlDatabase
    Private dbCommand As DbCommand
#End Region
#Region "Constructor"
    Public Sub New()
        dbBase = New SqlDatabase(SqlConexionDB)
    End Sub
#End Region


#Region "No transaccional"
    Public Function F_ListarReporteMeritosDemeritos(ByVal codAula As Integer, ByVal codAlumno As Integer, ByVal codAnioAcademico As Integer, ByVal codBimestre As Integer, ByVal tipoCiterio As Integer)
        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_Rep_MeritoDemerito")
                'Parametros de Entrada
                dbBase.AddInParameter(cmd, "@codAula", DbType.Int16, codAula)
                dbBase.AddInParameter(cmd, "@codAlumno", DbType.Int32, codAlumno)
                dbBase.AddInParameter(cmd, "@codAnioAcademico", DbType.Int16, codAnioAcademico)
                dbBase.AddInParameter(cmd, "@codBimestre", DbType.Int16, codBimestre)
                dbBase.AddInParameter(cmd, "@tipoCiterio", DbType.Int16, tipoCiterio)


                '@codAula int =0,
                '@codAlumno int =20070082,
                '@codAnioAcademico int =2,
                '@codBimestre int =3,
                '@tipoCiterio int =0
                Return dbBase.ExecuteDataSet(cmd)
            End Using

        Catch ex As Exception
        Finally

        End Try


    End Function

    Public Function F_litarAlumnosMeritoDemerito(ByVal p_CodigoAnioAcademico As Integer, _
                                                ByVal p_CodigoAsignacionAula As Integer, ByVal codAlumno As Integer, ByVal p_CodigoUsuario As Integer, ByVal p_CodigoTipoUsuario As Integer, _
                                               ByVal p_CodigoModulo As Integer, ByVal p_CodigoOpcion As Integer)
        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_ListarAlumnoCodigo")
                'Parametros de Entrada
                dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, p_CodigoAnioAcademico)
                dbBase.AddInParameter(cmd, "@p_CodigoAsignacionAula", DbType.Int32, p_CodigoAsignacionAula)
                dbBase.AddInParameter(cmd, "@codAlumno", DbType.Int32, codAlumno)
                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, p_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, p_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, p_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, p_CodigoOpcion)


                '@p_CodigoAnioAcademico int,         
                '@p_CodigoAsignacionAula int,      
                '@codAlumno int =20040098,                    
                '@p_CodigoUsuario int,                                            
                '@p_CodigoTipoUsuario int,                                            
                '@p_CodigoModulo int,                                            
                '@p_CodigoOpcion int   
                Return dbBase.ExecuteDataSet(cmd)
            End Using

        Catch ex As Exception
        Finally

        End Try


    End Function
    ''USP_ListarAlumnoCodigo

#End Region

End Class
