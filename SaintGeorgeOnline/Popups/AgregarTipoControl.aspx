<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AgregarTipoControl.aspx.vb" Inherits="Popups_AgregarTipoControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AgregarTipoControl</title>
    
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery-1.4.1.min.js"></script>
   
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/ToolTipPreview.js"></script>
    
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/misEstilos.css" />
    
<style type="text/css">
    #screenshot{
	    position:absolute;
	    border:1px solid #ccc;
	    background:#333;
	    padding:5px;
	    display:none;
	    color:#fff;
	}
    #preview{
	    position:absolute;
	    border:1px solid #ccc;
	    background:#333;
	    padding:5px;
	    display:none;
	    color:#fff;
	}	
	
    #miModalWindow
    {
        height: 112px;
        width: 511px;
    }
    #form1
    {
        width: 508px;
    }
	
    .style2
    {
        height: 25px;
        width: 175px;
    }
    .miTextBox
    {
        margin-left: 4px;
    }
	
    .style3
    {
        height: 25px;
        width: 240px;
    }
	
</style>    

<script type="text/javascript">   

    function pageLoad(sender, args){   
        if (args.get_isPartialLoad()){
            imagePreview();
        }
    }
    
</script>  

    
</head>
<body>
    <form id="form1" runat="server">
 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="updPanel_AgregarVacuna" runat="server">
        <ContentTemplate>
        
        <div id="miModalWindow">        
    
        <table border="0" cellpadding="0" cellspacing="0" 
                style="width: 510px; height: 107px;">            
            <tr>                
                <td style="height: 25px;" colspan="2" valign="middle" align="left" 
                    class="TitlebarLeft">
                    <span>Agregar Tipo de Control</span></td>
                <td style="width: 100px; height: 25px;" valign="middle" align="right" class="TitlebarLeft">
                    <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        Width="16" Height="15" CssClass="TitlebarLeft_Button" />&nbsp;
                </td>
            </tr>  

            <tr><td colspan="3" height="11px">&nbsp;</td></tr>
                                
            <tr>
                <td valign="middle" align="left" class="style3">
                    Tipo de Control
                </td>
                <td valign="middle" align="Center" style="width: 360px;">
                    <asp:TextBox ID="tbVacuna" runat="server" CssClass="miTextBox" 
                        Height="22px" Rows="2" TextMode="MultiLine" Width="363px" />
                    
                   
                </td>
                <td style="width: 50px; height: 25px; padding-top: 6px;" valign="top" 
                    align="left">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td valign="middle" align="left" class="style3">
                    &nbsp;</td>
                <td valign="middle" align="left" class="style2">
                    <asp:ImageButton ID="btnAgregar" runat="server" Height="19" 
                        ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" OnClick="btnAgregar_Click" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'" 
                        style="margin-left: 6px" Width="74" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancelar" runat="server" Height="19" 
                        ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" OnClick="btnCancelar_Click" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                        style="margin-left: 0px" Width="74" />
                </td>
                <td align="left" style="width: 100px; height: 25px; padding-top: 6px;" 
                    valign="top">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td style="width: 740px;" valign="top" align="left" colspan="3">                
                    &nbsp;</td>
            </tr>
            
        </table>           
        <br />        
        <table width="740px" border="0" cellpadding="0" cellspacing="0">                    
            
        </table>
             
        </div>  
                          
        </ContentTemplate>
    </asp:UpdatePanel>                  
                     
    </form>
</body>
</html>
