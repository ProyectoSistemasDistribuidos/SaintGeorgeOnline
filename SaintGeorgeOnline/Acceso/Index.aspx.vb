Imports System.Net

Partial Class Acceso_Index
    Inherits System.Web.UI.Page

    'Public Function GetIpAddress() As String
    '    Dim stringIpAddress As String
    '    stringIpAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
    '    If stringIpAddress = Nothing Then
    '        stringIpAddress = Request.ServerVariables("REMOTE_ADDR")
    '    End If
    '    Return stringIpAddress
    'End Function

    'Public Function GetLanIPAddress() As String
    '    Dim stringHostName As String = Dns.GetHostName()
    '    Dim ipHostEntries As IPHostEntry = Dns.GetHostEntry(stringHostName)
    '    Dim arrIpAddress As IPAddress() = ipHostEntries.AddressList
    '    Return arrIpAddress(arrIpAddress.Length - 1).ToString()
    'End Function

    'Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load

    '    Try

    '        Dim myPublicIP As String = ConfigurationManager.AppSettings("publicIP").ToString()

    '        Dim utf8 As New System.Text.UTF8Encoding()
    '        Dim webclient As New System.Net.WebClient
    '        Dim externalip As String = utf8.GetString(webclient.DownloadData("http://api.externalip.net/ip/"))


    '        Response.Write("<br />")
    '        Response.Write("ip servidor: " & myPublicIP)
    '        Response.Write("<br />")
    '        Response.Write("ip publica: " & externalip)
    '        Response.Write("<br />")


    '        If myPublicIP = externalip Then
    '            Response.Write("<br />")
    '            Response.Write("local (webconfig)")
    '            Response.Write("<br />")
    '            Response.Write("redireccionar a: http://192.168.1.16:8075/SaintGeorgeOnline/Acceso/Login.aspx")

    '            'Response.Redirect("http://192.168.1.16:8075/SaintGeorgeOnline/Acceso/Login.aspx")
    '        Else
    '            Response.Write("<br />")
    '            Response.Write("externo")
    '            Response.Write("<br />")
    '            Response.Write("redireccionar a: http://web.stgeorgescollege.edu.pe/SaintGeorgeOnline/Acceso/Login.aspx")

    '            'Response.Redirect("http://web.stgeorgescollege.edu.pe/SaintGeorgeOnline/Acceso/Login.aspx")
    '        End If



    '        'Dim strVisitorIpAddress As String = GetIpAddress()
    '        'Dim strLanIpAddress As String = GetLanIPAddress()

    '        'Response.Write("<br />")
    '        'Response.Write("ip cliente: " & strVisitorIpAddress)
    '        'Response.Write("<br />")
    '        'Response.Write("ip lan: " & strLanIpAddress)
    '        'Response.Write("<br />")

    '        'If strVisitorIpAddress = ("127.0.0.1") Or strVisitorIpAddress = ("::1") Or Not strVisitorIpAddress.IndexOf("192.168.1.") < 0 Then
    '        '    Response.Write("local")
    '        '    'Response.Redirect("http://192.168.1.16:8075/SaintGeorgeOnline/Acceso/Login.aspx")
    '        'Else
    '        '    Response.Write("externo")
    '        '    'Response.Redirect("http://web.stgeorgescollege.edu.pe/SaintGeorgeOnline/Acceso/Login.aspx")
    '        'End If

    '    Catch ex As Exception

    '        Response.Write(ex.Message)

    '        'Response.Write("externo")
    '        'Response.Redirect("http://web.stgeorgescollege.edu.pe/SaintGeorgeOnline/Acceso/Login.aspx")
    '    End Try

    'End Sub

End Class
