<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Interfaz_Logueo.aspx.vb" Inherits="Interfaz_Logueo" %>

<%@ OutputCache Location ="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Interfaz de Logueo - Super Usuario</title>
    
    <script src="/SaintGeorgeOnline/Alertas_jquery/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/SaintGeorgeOnline/Alertas_jquery/jquery.easing.1.3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/Alertas_jquery/sexyalertbox.v1.2.jquery.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/Alertas_jquery/sexyalertbox.css"/>
    
    
   
</head>
<body bgcolor="#32689c" style="margin-top:0px;margin-left:0px;margin-right:0px;margin-bottom:0px;" >
    <form id="form1" runat="server">
    <div>
        
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"  >
        </asp:ScriptManager>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style=" width: 100%; height: 100%; text-align: center;margin-left:auto;margin-right:auto" cellpadding="0" 
            cellspacing="0" border="0">
                <tr>
                    <td style="margin-left:auto;margin-right:auto">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                            <tr>
                                <td style="background-image: url('App_Themes/Imagenes/loginlinea_fondo.jpg'); background-repeat:repeat-x;  ">
                                &nbsp;
                                </td>
                                <td style="margin-left:auto;margin-right:auto;background-image: url('App_Themes/Imagenes/SeleccionUsuario.jpg');background-repeat:no-repeat;height:579px;width:974px ;">
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
                                                            <asp:Image ID="Img_FotoUsuario" runat="server" Width="46px" Height="56px" Style="border: #7f9db9 1px solid"
                                                                ImageUrl="" />
                                                        </td>
                                                        <td style="width:404px;height:56px;text-align:left; ">
                                                            <span style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 12px;color:#18c6fe;">Usuario Logueado:</span><br>
                                                            <asp:Label ID="lbl_UsuarioLogueado" runat="server" Font-Size="10pt" ForeColor="#4b6a8f" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color:#083264;">
                                                        <td colspan="2">
                                                            <span style="color:#18c6fe;text-align:justify;">
                                                            El sistema ha detectado que su usuario tiene privilegios de administrador por lo cual debe seleccionar cual es el usuario deseado para poder ingresar al sistema.
                                                            </span> 
                                                        </td>    
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align:left;" >
                                                        <asp:CheckBox ID="chk_UsuarioReferencia" runat="server" AutoPostBack="True" 
                                                Font-Size="10pt" 
                                                Text="Continuar proceso de logueo usando un usuario de referencia." />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="pnl_SuplantarUsuario" runat="server" Visible="False">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="height: 80px; width:450px;background-color:#dad7d7;">
                                                                <tr>
                                                                    <td style="text-align: left; vertical-align: bottom; padding-right: 15px;height:27px" 
                                                                        colspan="2">
                                                                        
                                                                        <span style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 12px;">Tipo de Persona:</span>
                                                                        
                                                                    </td>
                                                                    <td style="text-align: left; vertical-align: bottom;"> 
                                                                        <asp:DropDownList ID="ddl_TipoPersona" runat="server" Height="18px" 
                                                                            Width="156px" AutoPostBack="True" OnSelectedIndexChanged="ddl_TipoPersona_SelectedIndexChanged" >
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; vertical-align: bottom; padding-right: 15px;height:27px" 
                                                                        colspan="2">
                                                                        
                                                                        <span style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 12px;">Persona:</span>
                                                                        
                                                                    </td>
                                                                    <td style="text-align: left; vertical-align: bottom;"> 
                                                                        <asp:DropDownList ID="ddl_Persona" runat="server" Height="18px" Width="270px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="height:27px;text-align: left; vertical-align: bottom;" >
                                                                    
                                                                        <span style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 12px;">Contraseña:</span>
                                                                                                                                                
                                                                    </td>
                                                                    <td style="text-align:left; ">
                                                                        <asp:TextBox 
                                                                            ID="txt_Contrasenia" runat="server" Height="18px" TextMode="Password" 
                                                                            Width="100px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                                                        &nbsp;&nbsp;&nbsp;    
                                                                        <asp:Button ID="btn_GenerarContraseña" runat="server" BackColor="#99CCFF" 
                                                                            BorderColor="#000066" BorderWidth="1px" ForeColor="#000066" 
                                                                            Text="Generar" Width="93px" style="cursor:pointer;" 
                                                                            BorderStyle="Solid" OnClick="btn_GenerarContraseña_Click" />
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
                                                        <asp:Button ID="btnContinuar"   runat="server" Text="Continuar" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnSalir"   runat="server" Text="salir" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="background-image: url('App_Themes/Imagenes/loginlinea_fondo.jpg'); background-repeat:repeat-x;  ">
                                &nbsp;
                                </td>
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
