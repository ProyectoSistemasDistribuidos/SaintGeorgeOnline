<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AgregarFrecuenciaUso.aspx.vb" Inherits="Popups_AgregarFrecuenciaUso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AgregarFrecuenciaUso</title>
    
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
        height: 115px;
        width: 494px;
    }
    #form1
    {
        width: 494px;
        height: 154px;
        margin-bottom: 0px;
    }
	
    .style1
    {
        height: 11px;
    }
    .style9
    {
        height: 25px;
        width: 312px;
    }
    .style10
    {
        height: 25px;
        width: 447px;
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
    
    <asp:UpdatePanel ID="updPanel_AgregarTipoAlergia" runat="server">
        <ContentTemplate>
       
       <div id="miModalWindow">        
    
        <table border="0" cellpadding="0" cellspacing="0" 
                style="width: 494px; height: 114px;">            
            <tr>                
                <td style="height: 25px;" colspan="2" valign="middle" align="left" 
                    class="TitlebarLeft">
                    <span>Agregar Frecuencia de Uso</span>
                </td>
                <td style="width: 100px; height: 25px;" valign="middle" align="right" class="TitlebarLeft">
                    <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        Width="16" Height="15" CssClass="TitlebarLeft_Button" />&nbsp;
                </td>
            </tr>  

            <tr><td colspan="3" class="style1"></td></tr>
                                
            <tr>
                <td valign="middle" align="left" style="width: 150px; height: 25px; >                   
                           
                </td>
                <td valign="middle" align="center"  style="width: 337px; height: 25px;" >
                    Frecuencia de Uso</td>
                <td style="width: 100px; height: 25px; padding-top: 6px;" valign="top" 
                    align="left">
                    <asp:TextBox ID="tbCaracteristicaPiel" runat="server" CssClass="miTextBox" 
                        Height="29px" Rows="2" TextMode="MultiLine" Width="337px" />
                </td>
            </tr>
            
            <tr>
                <td valign="middle" align="left" class="style9">
                   </td>
                <td valign="middle" align="left" class="style8" >
                    <asp:ImageButton ID="btnAgregar" runat="server" Height="19" 
                        ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" OnClick="btnAgregar_Click" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'" 
                        style="margin-left: 0px" Width="74" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                <td style="width: 537px;" valign="top" align="left" colspan="3">                
                    &nbsp;</td>
            </tr>
            
        </table>           
            
       
        </div>  
                          
        </ContentTemplate>
    </asp:UpdatePanel>   
    
    </form>
</body>
</html>
