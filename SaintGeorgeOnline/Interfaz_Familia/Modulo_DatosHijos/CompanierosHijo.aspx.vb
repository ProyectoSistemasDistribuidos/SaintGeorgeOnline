Imports System.Data

Partial Class Interfaz_Familia_Modulo_DatosHijos_CompanierosHijo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            cargar()
        End If

    End Sub

    Private Sub cargar()

        Dim dt As New datatable
        dt.Columns.Add("nombre", System.Type.GetType("System.String"))
        dt.Columns.Add("cumpleanios", System.Type.GetType("System.String"))
        dt.Columns.Add("telefono", System.Type.GetType("System.String"))

        Dim dr As DataRow
        For i As Integer = 1 To 20
            dr = dt.NewRow
            dr.Item("nombre") = "nombre" & i & "<br />" & "nombre" & i
            dr.Item("cumpleanios") = IIf(i < 10, "0" & i & "/01/2000", i & "/01/2000")
            dr.Item("telefono") = IIf(i < 10, "123456" & i, "12345" & i)
            dt.Rows.Add(dr)
        Next

        DataList1.DataSource = dt
        DataList1.DataBind()

    End Sub

    Protected Sub DataList1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim imagen As HtmlImage = CType(e.Item.FindControl("foto"), HtmlImage)

            imagen.Attributes.Add("class", "pimg")
            imagen.Attributes.Add("img_src", "http://messenger.es/wp-content/uploads/2008/02/messenger-icon.jpg")
            imagen.Attributes.Add("src", "http://www.iconarchive.com/icons/artua/dragon-soft/128/User-icon.png")
            imagen.Attributes.Add("title", e.Item.DataItem("nombre") & "<br />" & e.Item.DataItem("cumpleanios"))


            e.Item.Attributes.Add("onMouseOver", "this.bgColor='#FFFFFF'")
            e.Item.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

End Class
