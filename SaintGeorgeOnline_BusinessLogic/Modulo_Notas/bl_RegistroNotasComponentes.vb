Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Public Class bl_RegistroNotasComponentes


    Public Function FUN_INS_CU_RegistroNotasComponentes(ByVal o_tbla As DataTable, ByVal Lst_Componentes As List(Of componente), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
        Try
            Dim o As New da_RegistroNotasComponentes
            Return New da_RegistroNotasComponentes().FUN_INS_CU_RegistroNotasComponentes(o_tbla, Lst_Componentes, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        Catch ex As Exception
        Finally
        End Try


    End Function



    Public Function FUN_agregar_SubIndicador(ByVal lp As List(Of persona), ByVal subIndicador As Integer)
        Try

            Return New da_RegistroNotasComponentes().FUN_agregar_SubIndicador(lp, subIndicador)

        Catch ex As Exception

        End Try
    End Function

    Public Function FUN_lIS_CU_ListarNotasComponenteIndicadorSubindicador(ByVal int_codigoBimestre As Integer, ByVal int_codigoGrupo As Integer)
        Try
            Return New da_RegistroNotasComponentes().FUN_lIS_CU_ListarNotasComponenteIndicadorSubindicador(int_codigoBimestre, int_codigoGrupo)
        Catch ex As Exception

        End Try
    End Function
    Public Function FUN_UPD_CU_ActualizarNotasComponente(ByVal int_IdRegistroNotaComponente As Integer, ByVal str_notaComponenete As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
        Try
            Return New da_RegistroNotasComponentes().FUN_UPD_CU_ActualizarNotasComponente(int_IdRegistroNotaComponente, str_notaComponenete, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        Catch ex As Exception
        Finally

        End Try
    End Function



    Public Function fActualizarTodo(ByVal oLista As List(Of persona), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal lstModificadosComponente As List(Of Integer), ByVal lstNotasModificadoIndicador As List(Of Integer), ByVal ltsNotasModificadas As List(Of Integer)) As List(Of persona)

        Try

            Return New da_RegistroNotasComponentes().fActualizarTodo(oLista, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, lstModificadosComponente, lstNotasModificadoIndicador, ltsNotasModificadas)
        Catch ex As Exception

        End Try

    End Function
    Public Function listarPromediosBimestrales(ByVal int_cod_grupo As Integer, ByVal int_cod_bimestre As Integer)
        Try
            Return New da_RegistroNotasComponentes().listarPromediosBimestrales(int_cod_grupo, int_cod_bimestre)
        Catch ex As Exception

        End Try
    End Function


    ''' <summary>
    ''' funcion que  lista toda la estrucutura de resitro componente indicador  subindicador para generar su registro nota componente,nota indicador ,subindicador  
    '''
    ''' </summary>
    ''' <param name="codAsignacionAula">codigo del salo ha generar su libreta de notas </param>
    ''' <param name="codBimestre">bimestre para generar su libreta de notas</param>
    ''' <returns>retorna 3 tablas 1.)tabla de componente 2.)tabla idnicador 3.) tabla subindicador  </returns>
    ''' <remarks></remarks>
    Public Function listarEstructuraComponenteindicadorSubindicadorPorAula(ByVal codAsignacionAula As Integer, ByVal codBimestre As Integer)
        Try
            Return New da_RegistroNotasComponentes().listarEstructuraComponenteindicadorSubindicadorPorAula(codAsignacionAula, codBimestre)
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="codBimestre">codigo de bimestre </param>
    ''' <param name="lstComponenteNuevo">lista de estructuras  de componenente indicador  subindicador</param>
    ''' <param name="int_CodigoUsuario"></param>
    ''' <param name="int_CodigoTipoUsuario"></param>
    ''' <param name="int_CodigoModulo"></param>
    ''' <param name="int_CodigoOpcion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FUN_INS_CU_InsertarNotaComponenteEstrucuturaSalon(ByVal codBimestre As Integer, ByVal lstComponenteNuevo As List(Of ComponenteNuevo), ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

        Try


            Return New da_RegistroNotasComponentes().FUN_INS_CU_InsertarNotaComponenteEstrucuturaSalon(codBimestre, lstComponenteNuevo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        Catch ex As Exception
        Finally

        End Try
    End Function




End Class
