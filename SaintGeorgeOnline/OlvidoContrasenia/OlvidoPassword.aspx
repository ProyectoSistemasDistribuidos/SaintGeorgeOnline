<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OlvidoPassword.aspx.vb" Inherits="Usuarios_OlvidoPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recuperar Contraseña</title>
    
    <script src="/SaintGeorgeOnline/Alertas_jquery/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/SaintGeorgeOnline/Alertas_jquery/jquery.easing.1.3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/Alertas_jquery/sexyalertbox.v1.2.jquery.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/Alertas_jquery/sexyalertbox.css"/>
    
    <style type="text/css">
        .style2
        {
        }
        .style3
        {
        }
        </style>
</head>
<body style = "background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/FondoPopOlvidoContrasenia.jpg');background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <table style="width: 500px;  height: 251px; " >
            <tr>
                <td class="style2" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: #000080; font-weight: bold; text-align: center; vertical-align: top">
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="style2" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: #000080; font-weight: bold; text-align: center; vertical-align: top">
                    Recuperar Contraseña</td>
            </tr>
            <tr>
                <td class="style2" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: #000080; font-weight: bold; text-align: center; vertical-align: top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: #000080; font-weight: bold; text-align: center; vertical-align: top">
                    <table style="width:100%;">
                        <tr>
                            <td class="style3">
                                Documento de Identidad</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtUsuario" runat="server" Width="218px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style3" colspan="2" 
                                style="font-family: Arial, Helvetica, sans-serif; color: #800000; font-size: 10px; text-align: left">
                                Importante: Se enviará un e-mail a la cuenta registrada de su&nbsp;                                 <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                usuario en el sistema 
                                indicando sus contraseñas<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; registradas.</td>
                        </tr>
                        <tr>
                            <td class="style3" colspan="2">
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style2" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: #000080; font-weight: bold; text-align: center; vertical-align: top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: #000080; font-weight: bold; text-align: center; vertical-align: top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: #000080; font-weight: bold; text-align: center; vertical-align: top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
            </tr>
            </table>
    
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
