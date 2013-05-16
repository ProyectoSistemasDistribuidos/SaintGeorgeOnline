Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_DataAccess.ModuloBancoLibros

Namespace ModuloBancoLibros
    Public Class bl_Devoluciones

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Devoluciones As da_Devoluciones

#End Region

#Region "Propiedades"

        Public ReadOnly Property Mensaje() As String
            Get
                Return str_Mensaje
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_Devoluciones = New da_Devoluciones
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_UPD_PrestamoDetalle(ByVal int_Codigo As Integer, ByVal int_Accion As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Devoluciones.FUN_UPD_PrestamoDetalle(int_Codigo, int_Accion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_UPD_DevolverLibro(ByVal int_CodigoDetalle As Integer, _
                                              ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_Devoluciones.FUN_UPD_DevolverLibro(int_CodigoDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_PrestamoDetalle(ByVal int_CodigoPrestamo As Integer, ByVal int_CodigoLibro As Integer, _
                                                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Devoluciones.FUN_GET_PrestamoDetalle(int_CodigoPrestamo, int_CodigoLibro, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PrestamoPorLibroGradoPeriodo( _
            ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal int_Idioma As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Devoluciones.FUN_LIS_PrestamoPorLibroGradoPeriodo(int_AnioAcademico, int_CodigoGrado, int_CodigoAula, int_Idioma, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class




End Namespace
