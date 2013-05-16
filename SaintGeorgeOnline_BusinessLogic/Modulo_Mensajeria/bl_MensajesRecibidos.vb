Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloMensajeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMensajeria

Namespace ModuloMensajeria

    Public Class bl_MensajesRecibidos

#Region "Atributos"

        Private obj_da_MensajesRecibidos As da_MensajesRecibidos
#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_MensajesRecibidos = New da_MensajesRecibidos

        End Sub

#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_DEL_MensajesRecibidos(ByVal objMensajesRecibidos As be_MensajesRecibidos, ByVal str_ListaCodigosMensajesRecibidos As String, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_MensajesRecibidos.FUN_DEL_MensajesRecibidos(objMensajesRecibidos, str_ListaCodigosMensajesRecibidos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_MensajesRecibidos(ByVal str_CodigoUsuario As String, _
                                          ByVal int_TipoUsuario As Integer, _
                                          ByVal int_TipoMensaje As Integer, _
                                          ByVal int_EstadoMensaje As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MensajesRecibidos.FUN_LIS_MensajesRecibidos(str_CodigoUsuario, int_TipoUsuario, int_TipoMensaje, int_EstadoMensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        '24/10/2012
        Public Function FUN_LIS_MensajesRecibidosNew( _
            ByVal str_CodigoUsuario As String, ByVal int_TipoUsuario As Integer, _
            ByVal int_limInf As Integer, ByVal int_limSup As Integer, ByVal int_pagina As Integer, _
            ByVal int_TipoMensaje As Integer, ByVal int_EstadoMensaje As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MensajesRecibidos.FUN_LIS_MensajesRecibidosNew( _
            str_CodigoUsuario, int_TipoUsuario, _
            int_limInf, int_limSup, int_pagina, _
            int_TipoMensaje, int_EstadoMensaje, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        Public Function FUN_LIS_PaginadoMensajes( _
            ByVal str_CodigoUsuario As String, ByVal int_TipoUsuario As Integer, _
            ByVal int_limInf As Integer, ByVal int_limSup As Integer, ByVal int_pagina As Integer, _
            ByVal int_TipoMensaje As Integer, ByVal int_EstadoMensaje As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MensajesRecibidos.FUN_LIS_PaginadoMensajes( _
            str_CodigoUsuario, int_TipoUsuario, _
            int_limInf, int_limSup, int_pagina, _
            int_TipoMensaje, int_EstadoMensaje, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_MensajesRecibidosPorTipoYCodigo( _
            ByVal str_CodigoUsuario As String, ByVal int_TipoUsuario As Integer, _
            ByVal int_limInf As Integer, ByVal int_limSup As Integer, ByVal int_pagina As Integer, _
            ByVal int_TipoMensaje As Integer, ByVal int_EstadoMensaje As Integer, _
            ByVal int_Carpeta As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MensajesRecibidos.FUN_LIS_MensajesRecibidosPorTipoYCodigo( _
            str_CodigoUsuario, int_TipoUsuario, _
            int_limInf, int_limSup, int_pagina, _
            int_TipoMensaje, int_EstadoMensaje, _
            int_Carpeta, int_CodigoAula, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        Public Function FUN_LIS_PaginadoMensajesPorTipoYCodigo( _
           ByVal str_CodigoUsuario As String, ByVal int_TipoUsuario As Integer, _
           ByVal int_limInf As Integer, ByVal int_limSup As Integer, ByVal int_pagina As Integer, _
           ByVal int_TipoMensaje As Integer, ByVal int_EstadoMensaje As Integer, _
           ByVal int_Carpeta As Integer, ByVal int_CodigoAula As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MensajesRecibidos.FUN_LIS_PaginadoMensajesPorTipoYCodigo( _
            str_CodigoUsuario, int_TipoUsuario, _
            int_limInf, int_limSup, int_pagina, _
            int_TipoMensaje, int_EstadoMensaje, _
            int_Carpeta, int_CodigoAula, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_GET_MensajesRecibidos(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MensajesRecibidos.FUN_GET_MensajesRecibidos(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

