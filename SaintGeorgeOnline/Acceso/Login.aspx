<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Acceso_Login" %>

<%@ OutputCache Location ="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    
    <script src="/SaintGeorgeOnline/Alertas_jquery/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/SaintGeorgeOnline/Alertas_jquery/jquery.easing.1.3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/Alertas_jquery/sexyalertbox.v1.2.jquery.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/Alertas_jquery/sexyalertbox.css"/>
      
    
    <script language="Javascript1.2" type="text/javascript">
        function btnAyudaSugerencias() {

            window.open('/SaintGeorgeOnline/OlvidoContrasenia/OlvidoPassword.aspx', '_blank', 'height=250px,width=500px;');
        }

        window.moveTo(0, 0);
        window.resizeTo(screen.width, screen.height);        
        
    </script> 
    
    <style type="text/css">
        .style1
        {
            width: 385px;
        }
        .links 		{
	COLOR: #00befd;
	font-size:11px;
	FONT-FAMILY: Arial, Tahoma, Verdana;
	TEXT-DECORATION: underline;
}

    em{ color: Red; font-weight: bold; }
    
    </style>
    
</head>
<body bgcolor="#32689c" style="margin-top:0px;margin-left:0px;margin-right:0px;margin-bottom:0px;" onLoad="goforit()">
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <table style=" width: 100%; height: 100%; text-align: center;margin-left:auto;margin-right:auto" cellpadding="0" 
            cellspacing="0" border="0">
        <tr>
             <td style="margin-left:auto;margin-right:auto">
                 <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td style="background-image: url('../App_Themes/Imagenes/loginlinea_fondo.jpg'); background-repeat:repeat-x;  ">
                            &nbsp;
                            </td>
                            <td style="margin-left:auto;margin-right:auto;background-image: url('../App_Themes/Imagenes/login4.jpg');background-repeat:no-repeat;height:704px;width:974px ;">
                                <table style="height:704px;width:974px ;" border="0" >
                                    <tr style ="height:348px">
                                        <td style="width:250px; " >
                                        &nbsp;
                                        </td>
                                        <td style="width:724px;vertical-align:top; " >
                                            <table style="width:100%; " border="0" >
                                                <tr>
                                                    <td style="width:385px;">
                                                    &nbsp;                                                    
                                                    </td>
                                                    <td style="width:165px; ">
                                                    <div align="left">
                                                 <script>
                                                     var dayarray = new Array("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday")
                                                     var montharray = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December")

                                                     function getthedate() {
                                                         var mydate = new Date()
                                                         var year = mydate.getYear()
                                                         if (year < 1000)
                                                             year += 1900
                                                         var day = mydate.getDay()
                                                         var month = mydate.getMonth()
                                                         var daym = mydate.getDate()
                                                         if (daym < 10)
                                                             daym = "0" + daym
                                                         var hours = mydate.getHours()
                                                         var minutes = mydate.getMinutes()
                                                         var seconds = mydate.getSeconds()
                                                         var dn = "AM"
                                                         if (hours >= 12)
                                                             dn = "PM"
                                                         if (hours > 12) {
                                                             hours = hours - 12
                                                         }
                                                         if (hours == 0)
                                                             hours = 12
                                                         if (minutes <= 9)
                                                             minutes = "0" + minutes
                                                         if (seconds <= 9)
                                                             seconds = "0" + seconds
                                                         //change font size here
                                                         var cdate = "<small><font color='FFFFFF' font size='1' face='Arial'><b>" + dayarray[day] + ", " + montharray[month] + " " + daym + ", " + year 
                                        + "</b></font></small>"
                                                         if (document.all)
                                                             document.all.clock.innerHTML = cdate
                                                         else if (document.getElementById)
                                                             document.getElementById("clock").innerHTML = cdate
                                                         else
                                                             document.write(cdate)
                                                     }
                                                     if (!document.all && !document.getElementById)
                                                         getthedate()
                                                     function goforit() {
                                                         if (document.all || document.getElementById)
                                                             setInterval("getthedate()", 1000)
                                                     }
                                                  </script>
                                                 &nbsp;<span id="clock"></span>
                                            </div>                                                    
                                                    </td>
                                                    <td>
                                                    <div align="left">
                                                    <a href="http://www.sanjorge.edu.pe" target="_blank" class="links">Ir a portal WEB</a>
                                                    </div>
                                                    </td>
                                                </tr>
                                            </table>                                            
                                        </td>
                                    </tr>
                                    <tr style="height:38px">
                                        <td style="width:250px; " >
                                        &nbsp;
                                        </td>
                                        <td style="width:724px;text-align:left;vertical-align:top;    " >
                                            <asp:TextBox ID="txtUsuario" runat="server" Width="141px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:42px">
                                        <td style="width:250px; " >
                                        &nbsp;
                                        </td>
                                        <td style="width:724px;text-align:left;vertical-align:top;    " >
                                            <asp:TextBox ID="txtContrasenia" runat="server" Width="141px" 
                                                TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:92px">
                                        <td style="width:250px; " >
                                        &nbsp;
                                        </td>
                                        <td style="width:724px;text-align:left;vertical-align:top;    " >
                                            <asp:Button ID="btn_Ingresar" runat="server" Text="INGRESAR" Width="108px" 
                                                BackColor="#18C5FF" BorderColor="#18C5FF" Font-Bold="True" ForeColor="White" 
                                                Height="30px" style="cursor:pointer;" />
                                        </td>
                                    </tr>
                                    <tr style="height:190px">
                                        <td style="vertical-align:top;text-align:left;" colspan="2">
                                            <table>
                                                <tr>
                                                    <td style="width:83px; ">&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbllvidoContrasenia" runat="server"  
                                                                   OnClick="btnAyudaSugerencias();" 
                                                                   BorderStyle="None" 
                                                                   Font-Bold="True"
                                                                   Font-Size="13px" 
                                                                   ForeColor="White"
                                                                   Text="¿Ha olvidado la contraseña?" 
                                                                   style="cursor:pointer;"></asp:Label>                                                        
                                                    </td>
                                                </tr>
                                            </table> 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background-image: url('../App_Themes/Imagenes/loginlinea_fondo.jpg'); background-repeat:repeat-x;  ">
                            &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                 </table>
             </td>
        </tr>        
    </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
    </form>
</body>
</html>
