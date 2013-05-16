Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_DataAccess.ModuloBancoLibros

Namespace ModuloBancoLibros

    Public Class bl_PrestamoDetalle

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_PrestamoDetalle As da_PrestamoDetalle

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
            obj_da_PrestamoDetalle = New da_PrestamoDetalle
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_PrestamoDetalle(ByVal objPrestamoDetalle As be_PrestamoDetalle, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_PrestamoDetalle.FUN_INS_PrestamoDetalle(objPrestamoDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_DetallePrestamoNuev(ByVal objPrestamoDetalle As be_PrestamoDetalle, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_PrestamoDetalle.FUN_INS_DetallePrestamoNuev(objPrestamoDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_PrestamoDetalle(ByVal int_Codigo As Integer, ByVal int_Accion As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return (obj_da_PrestamoDetalle.FUN_UPD_PrestamoDetalle(int_Codigo, int_Accion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion))

        End Function

        Public Function FUN_DEL_PrestamoDetalle(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_PrestamoDetalle.FUN_DEL_PrestamoDetalle(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_PrestamoPorLibroGradoPeriodo( _
            ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal int_Idioma As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_PrestamoDetalle.FUN_LIS_PrestamoPorLibroGradoPeriodo(int_AnioAcademico, int_CodigoGrado, int_CodigoAula, int_Idioma, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_ValidacionDatosLibroXCodigoBarra(ByVal str_CodigoBarra As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_PrestamoDetalle.FUN_GET_ValidacionDatosLibroXCodigoBarra(str_CodigoBarra, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace

