Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_Utilities

Imports CrystalDecisions
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmVistaPrevia

#Region "Atributos"

    Private int_CodigoDocumento As Integer
    Private int_CodigoTalonario As Integer

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1
    Private int_CodigoUsuario As Integer = 1
    Private int_CodigoTipoUsuario As Integer = 1

#End Region

#Region "Propiedades"

    Public Property codigoDocumento() As Integer
        Get
            Return int_CodigoDocumento
        End Get
        Set(ByVal value As Integer)
            int_CodigoDocumento = value
        End Set
    End Property

    Public Property codigoTalonario() As Integer
        Get
            Return int_CodigoTalonario
        End Get
        Set(ByVal value As Integer)
            int_CodigoTalonario = value
        End Set
    End Property

#End Region

#Region "Eventos"

    Public Sub New()

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Public Sub New(ByVal int_CodigoDocumento As Integer, ByVal int_CodigoTalonario As Integer)

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        codigoDocumento = int_CodigoDocumento
        codigoTalonario = int_CodigoTalonario

    End Sub

    Private Sub frmVistaPrevia_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            cargarReport(int_CodigoDocumento, int_CodigoTalonario)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ' Update
    Private Sub cargarReport(ByVal int_CodigoDocumento As Integer, ByVal int_CodigoTalonario As Integer)

        Dim ds_Lista As DataSet
        Dim obj_BL_Pagos As New bl_Pagos

        Dim str_CodigoConceptoCobro As String = int_CodigoDocumento.ToString
        ds_Lista = obj_BL_Pagos.FUN_LIS_PagosModuloImpresionVarios(str_CodigoConceptoCobro, int_CodigoTalonario, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim dt As DataTable
        dt = ds_Lista.Tables(0).Copy

        Dim monto As String = ""
        Dim soles As Double = 0
        Dim centimos As Double = 0
        Dim str_centimos As String = ""

        For Each dr As DataRow In dt.Rows
            monto = dr.Item("MontoTotalPago").ToString
            str_centimos = monto.Substring(monto.LastIndexOf(".") + 1, 2)
            soles = monto.Substring(0, monto.Length - str_centimos.Length - 1) 'CInt(dr.Item("MontoTotalPago"))
            dr.Item("MontoTotalTexto") = Datos.Num2Text(soles) & " Y " & str_centimos & "/100 " & " " & dr.Item("DescMoneda")
        Next

        ' Maximo 10 registros en el detalle
        Dim int_MaxRowFactura As Integer = 10

        If ds_Lista.Tables(0).Rows.Count > 0 Then

            If (int_CodigoTalonario = 1 Or int_CodigoTalonario = 7 Or int_CodigoTalonario = 8) Then ' Boleta
                Dim rptB As New RptImpresionBoletaConDetalle
                rptB.SetDataSource(dt)
                rptB.GroupFooterSection1.Height = 240 * (int_MaxRowFactura - dt.Rows.Count) ' - (10 * 240) = 2400
                CrystalReportViewer1.ReportSource = rptB
                CrystalReportViewer1.Zoom(75)
            ElseIf (int_CodigoTalonario = 2 Or int_CodigoTalonario = 10) Then ' Factura 
                Dim rptF As New RptImpresionFacturaConDetalle
                rptF.SetDataSource(dt)
                rptF.GroupFooterSection1.Height = 240 * (int_MaxRowFactura - dt.Rows.Count) ' - (10 * 240) = 2400
                CrystalReportViewer1.ReportSource = rptF
                CrystalReportViewer1.Zoom(75)
            ElseIf (int_CodigoTalonario = 3 Or int_CodigoTalonario = 11) Then ' Nota Crédito 
                Dim int_MaxRowNotaCredito As Integer = 3
                Dim rptNC As New RptImpresionNotaCreditoConDetalle
                rptNC.SetDataSource(dt)
                rptNC.GroupFooterSection1.Height = 240 * (int_MaxRowNotaCredito - dt.Rows.Count) ' - (10 * 240) = 2400
                CrystalReportViewer1.ReportSource = rptNC
                CrystalReportViewer1.Zoom(75)
            ElseIf (int_CodigoTalonario = 6 Or int_CodigoTalonario = 9) Then ' Nota Débito 
                Dim rptND As New RptImpresionNotaDebitoConDetalle
                rptND.SetDataSource(dt)
                rptND.GroupFooterSection1.Height = 240 * (int_MaxRowFactura - dt.Rows.Count) ' - (10 * 240) = 2400
                CrystalReportViewer1.ReportSource = rptND
                CrystalReportViewer1.Zoom(75)
            End If

        Else
            Me.Dispose()
            Exit Sub
        End If

    End Sub

#End Region

End Class