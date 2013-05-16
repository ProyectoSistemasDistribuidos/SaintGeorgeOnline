<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Interfaz_LogueoFamilia.aspx.vb" Inherits="Interfaz_Familia_Interfaz_LogueoFamilia" %>

<%@ OutputCache Location ="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Interfaz de Logueo - Selección de Familia</title>
    
    <script src="/SaintGeorgeOnline/Alertas_jquery/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/SaintGeorgeOnline/Alertas_jquery/jquery.easing.1.3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/Alertas_jquery/sexyalertbox.v1.2.jquery.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/Alertas_jquery/sexyalertbox.css"/>
    
    
</head>
<body bgcolor="#32689c" style="margin-top:0px;margin-left:0px;margin-right:0px;margin-bottom:0px;" >
    <form id="form1" runat="server">
    <div>
        
        
        
            <table style=" width: 100%; height: 100%; text-align: center;margin-left:auto;margin-right:auto" cellpadding="0" 
            cellspacing="0" border="0">
                <tr>
                    <td style="margin-left:auto;margin-right:auto">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                            <tr>
                                <td style="background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/loginlinea_fondo.jpg'); background-repeat:repeat-x;  ">
                                &nbsp;
                                </td>
                                <td style="margin-left:auto;margin-right:auto;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/SeleccionUsuario.jpg');background-repeat:no-repeat;height:579px;width:974px ;">
                                    <table style="height:704px;width:974px ;" border="0" >
                                        <tr style ="height:120px">
                                            <td>
                                            &nbsp;
                                            </td>
                                        </tr>
                                        <tr style ="height:348px;vertical-align:top; ">
                                            <td style="margin-left:auto;margin-right:auto;text-align:center; ">
                                                <table border="0" cellpadding="0" cellspacing="0" style="margin-left:auto;margin-right:auto;width:450px; " >
                                                    <tr>
                                                        <td style="width:46px;height:56px;">
                                                            &nbsp;</td>
                                                        <td style="width:404px;height:56px;text-align:left; ">
                                                            <span style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 12px;color:#18c6fe;">
                                                            Usuario Logueado:&nbsp; <asp:Label 
                                                                ID="lbl_UsuarioLogueado" runat="server" Font-Size="10pt"></asp:Label>
                                                            </span><br>
                                                            <asp:Label ID="Label1" runat="server" Font-Size="10pt" ForeColor="#4b6a8f" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color:#083264;">
                                                        <td colspan="2">
                                                            <span style="color:#18c6fe;text-align:justify;">
                                                            El sistema ha detectado que su usuario está unido a más de una (1) Familia, 
por favor elija la familia con la cual desee ingresar al sistema. 
                                                            </span> 
                                                        </td>    
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align:left;" >
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="pnl_SuplantarUsuario" runat="server" Visible="true">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="height: 80px; width:450px;background-color:#dad7d7;">
                                                                <tr>
                                                                    <td style="text-align: left; vertical-align: bottom; padding-right: 15px;height:27px">
                                                                        
                                                                        &nbsp;&nbsp;</td>
                                                                    <td style="text-align: left; vertical-align: bottom;"> 
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; vertical-align: bottom; padding-right: 15px;height:27px">
                                                                        
                                                                        <asp:RadioButtonList ID="rbl_Familias" runat="server">
                                                                        </asp:RadioButtonList>
                                                                        
                                                                    </td>
                                                                    <td style="text-align: left; vertical-align: bottom;"> 
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height:27px;text-align: left; vertical-align: bottom;" >
                                                                    
                                                                        &nbsp;&nbsp;</td>
                                                                    <td style="text-align:left; ">
                                                                        &nbsp;&nbsp;&nbsp;    
                                                                        </td>
                                                                </tr>                                                
                                                            </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                        &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="btnContinuar" runat="server" OnClick="btnContinuar_Click" 
                                                                Text="Continuar" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnSalir"   runat="server" Text="salir" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/loginlinea_fondo.jpg'); background-repeat:repeat-x;  ">
                                &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        
           
       
        
    </div>
    </form>
</body>
</html>
