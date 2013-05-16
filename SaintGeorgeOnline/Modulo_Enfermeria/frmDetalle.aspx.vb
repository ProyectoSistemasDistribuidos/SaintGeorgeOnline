
Imports System.Data
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas

Partial Class Modulo_Enfermeria_frmDetalle
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cod As Integer = 0
        cod = CInt(Request.QueryString("query"))

        Dim nParam As String = "USP_LisContactos"
        Dim dc As New Dictionary(Of String, Object)

        dc.Add("p_Codigo", cod) ' hidenCodigoPersona.Value)
        dc.Add("p_CodigoUsuario", 0)
        dc.Add("p_CodigoTipoUsuario", 0)
        dc.Add("p_CodigoModulo", 0)
        dc.Add("p_CodigoOpcion", 0)

        Dim dst As New DataSet
        dst = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam)


        Dim html As String = ""
        html = crearHtml(dst.Tables(0))
        Response.Write(html)
    End Sub

#Region " funcion crear lista diccionarios  alumno "
    Function crearHtml(ByVal dtAlumno As DataTable) As String
        Dim dccc As New List(Of Dictionary(Of Object, Object))
        Try
            Dim query = From oal In dtAlumno.AsEnumerable() Group oal By codAlumno = oal("CodigoAlumno"), _
                                         nombreAlumno = oal("NombreCompletoAlumno") Into detalle = Group _
                                         Select New With { _
                                         .nombreAlumno = nombreAlumno, _
                                         .familias = (From fam In detalle.AsEnumerable() Select New With { _
                                                            .nombreFamilia = fam("NombreCompletoPadre"), _
                                                            .telefonoCasa = fam("TelefonoCasaContacto"), _
                                                            .celular = fam("CellContacto"), _
                                                            .telefonoOficina = fam("TelefonoOficinaContacto"), _
                                                            .vive = IIf(CInt(fam("vive")) = 0, "NO", "SI"), _
                                                            .tipoParentezco = fam("tipoParentezco"), _
                                                            .esApoderado = fam("esApoderado"), _
                                                            .esResponsablePago = fam("esResponsablePago")})}




            Dim xmlAlmuno As New XElement("table", New XAttribute("cellspacing", "0"), _
                                             New XAttribute("cellpadding", "0"), _
                                             New XAttribute("border", "0"), _
                                             New XAttribute("style", "width:500px; font-family:Arial,font-size:8pt"))
            xmlAlmuno.Add(New XElement("tr", New XElement("td", New XAttribute("colspan", "2"), "CONTACTOS DEL ALUMNO")))
            xmlAlmuno.Add(New XElement("tr", New XElement("td", New XAttribute("colspan", "2"), New XElement("br"))))




            For Each oal In query

                xmlAlmuno.Add(New XElement("tr", New XElement("td", New XAttribute("colspan", "2"), "ALumno :" & oal.nombreAlumno)))
                xmlAlmuno.Add(New XElement("tr", New XElement("td", New XAttribute("colspan", "2"), New XElement("br"))))
                xmlAlmuno.Add(New XElement("tr", New XElement("td", New XAttribute("colspan", "2"), "Datos del contacto")))
                xmlAlmuno.Add(New XElement("tr", New XElement("td", New XAttribute("colspsan", "2"), New XElement("hr"))))

                For Each fam In oal.familias

                    xmlAlmuno.Add(New XElement("tr", New XElement("td", "Nombre campleto", New XElement("td", fam.nombreFamilia))))
                    xmlAlmuno.Add(New XElement("tr", New XElement("td", "Teléfono de Casa:", New XElement("td", fam.telefonoCasa))))
                    xmlAlmuno.Add(New XElement("tr", New XElement("td", "Celular:", New XElement("td", fam.celular))))
                    xmlAlmuno.Add(New XElement("tr", New XElement("td", "Teléfono de oficina:", New XElement("td", fam.telefonoOficina))))
                    xmlAlmuno.Add(New XElement("tr", New XElement("td", "Parentesco:", New XElement("td", fam.tipoParentezco))))
                    xmlAlmuno.Add(New XElement("tr", New XElement("td", "Vive con el alumno  :", New XElement("td", fam.vive))))

                    'xmlAlmuno.Add(New XElement("tr", New XElement("td", "Es apoderado :", New XElement("td", fam.esApoderado))))

                    xmlAlmuno.Add(New XElement("tr", New XElement("td", "Responsable económico:", New XElement("td", fam.esResponsablePago))))
                    xmlAlmuno.Add(New XElement("tr", New XElement("td", New XAttribute("colspan", "2"), New XElement("hr"))))
                Next


            Next

            Return xmlAlmuno.ToString()

        Catch ex As Exception

        End Try
    End Function
#End Region
End Class
