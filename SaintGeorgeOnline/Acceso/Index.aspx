<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Index.aspx.vb" Inherits="Acceso_Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!-- 
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
--> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <%--
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js"></script> --%>   
    <%--
    <script src="../Alertas_jquery/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    
    <script src="jquery-1.4.1.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        var getIP = "http://api.hostip.info/get_json.php";
        var localIP = "190.12.85.114";
        var linkINT = "http://192.168.1.16:8075/SaintGeorgeOnline/Acceso/Login.aspx";
        var linkEXT = "http://web.stgeorgescollege.edu.pe/SaintGeorgeOnline/Acceso/Login.aspx";

        $(document).ready(function() {
            $.ajax({
                url: getIP,
                type: "GET",
                dataType: "json",
                success: function(json) {
                    if (json.ip == localIP) {
                        $(location).attr('href', linkINT);
                    } else {
                        $(location).attr('href', linkEXT);
                    }
                    //document.write(json.ip);
                },
                error: function(request, type, status) {
                    //alert('[request: ' + request.statusText + '] [type: ' + type + '] [status: ' + status + ']');
                    $(location).attr('href', linkEXT);
                }
            });
        });
        
    </script>

    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
