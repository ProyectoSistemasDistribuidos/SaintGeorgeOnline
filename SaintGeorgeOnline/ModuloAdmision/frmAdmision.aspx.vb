Imports System.Data
Imports System.IO
Imports SaintGeorgeOnline_BusinessLogic
Imports ClosedXML.Excel
Partial Class ModuloAdmision_frmAdmision
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dsTeso As New DataSet
        Dim blTesoreris As New bl_admision2
        dsTeso = blTesoreris.FUN_reporteAlumnosBDTesoreria(0, 0)




        Dim dstSain As New DataSet
        Dim dstSaint As New bl_da_admision
        dstSain = dstSaint.FUN_reporteAlumnosBDSaint(7)



        Dim sqlLeft = dsTeso.Tables(0).AsEnumerable().Select(Function(fila, index) New With { _
                                                           .idPostulante = fila("IdPostulante"), .nombre = fila("apepaterno") & " " & fila("apematerno") & " " & fila("nombres"), _
                                                        .grado = fila("descripcion")})


        Dim sqlRight = dstSain.Tables(0).AsEnumerable().Select(Function(fila, index) New With { _
                                                           .idPostulante = fila("IdPostulante"), .Observacion = fila("Observacion")})


        Dim left = From s In sqlLeft _
 From dr In sqlRight Where s.idPostulante = dr.idPostulante _
 Select New With {.nombre = s.nombre, .grado = s.grado, .cancelo = dr.Observacion}
                 


        Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current
        ''

        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("reporteCargoEntrega")
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
        Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"

        File.Copy(rutaPlantillas, rutaREpositorioTemporales)

        Dim workbook As New XLWorkbook(rutaREpositorioTemporales)


        Dim filas As Integer = 7
        Dim index As Integer = 0
        For Each o In left
            index += 1
            ws.Cell(filas, 1).Value = "Fecha y hora"
            filas += 1

        Next


        workbook.Save()
        ''
        Dim downloadBytes As Byte()


        downloadBytes = File.ReadAllBytes(rutaREpositorioTemporales)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + "ReporteEntregaCargo.xlsx" + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()
        Response.Close()
        Response.End()

    End Sub
End Class
