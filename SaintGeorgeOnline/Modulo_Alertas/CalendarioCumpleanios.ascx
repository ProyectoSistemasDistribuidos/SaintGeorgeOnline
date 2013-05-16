<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CalendarioCumpleanios.ascx.vb" Inherits="Modulo_Alertas_CalendarioCumpleanios" %>

<script type="text/javascript">

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            $(".newsticker-jcarousellite").jCarouselLite({
                vertical: true,
                hoverPause: true,
                auto: 500,
                speed: 1000
            });
        }
    }

    $(document).ready(function() {
        $(".newsticker-jcarousellite").jCarouselLite({
            vertical: true,
            hoverPause: true,
            auto: 500,
            speed: 1000
        });
    });

</script> 

<atk:DragPanelExtender ID="DragPanelExtender_AlertaCalendarioCumpleanios" runat="server" 
                        Enabled="True" 
                        BehaviorID="Drag_AlertaCalendarioCumpleanios"                         
                        DragHandleID="pnlContainer_AlertaCalendarioCumpleanios" 
                        TargetControlID="pnlContainer_AlertaCalendarioCumpleanios">
</atk:DragPanelExtender>

<asp:Panel ID="pnlContainer_AlertaCalendarioCumpleanios" runat="server">
<div id="divCumpleanios" runat="server" style="position: relative; width: 310px; height: 208px;">

<div id="newsticker-demo" style="background-image: url('/SaintGeorgeOnline/App_Themes/imagenes/fondoAlerta1.png');">   

<table cellpadding="0" cellspacing="0" border="0" style="width: 310px; height: 208px;">
    <tr>
        <td style="width:310px; height:40px;" align="center" valign="middle">
        
    <div style="text-align:right; font-size:14px; font-weight:bold; padding: 0px; width:310px; height:40px; border:solid 0px green">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 310px;">
            <tr>
                <td style="width:160px; height: 30px;" align="left" valign="top"> 
                    &nbsp;&nbsp;&nbsp;&nbsp;<img src="/SaintGeorgeOnline/App_Themes/Imagenes/birthdayCake.png" width="50" height="40" alt="Cumpleañeros" border="0" style="border:0px"/>
                </td>
                <td style="width:150px; height: 30px;" align="right" valign="middle"> 
                    <asp:Label ID="lbltitulo" runat="server" style="padding-right:30px; font-family:Arial; font-size:14px; font-weight:bold;" />
                </td>
            </tr>
        </table>        
    </div>     
       
        </td>
    </tr>
    <tr>
        <td style="width:310px; height:9px;" align="center" valign="middle">
            &nbsp;
        </td>
    </tr>      
    <tr>
        <td style="width:310px; height:120px;" align="center" valign="top">
        
    <div class="newsticker-jcarousellite" style="border:solid 0px red">
		<ul>    
            <asp:Repeater ID="miRepeater" runat="server" OnItemCommand="miRepeater_ItemCommand">
            <ItemTemplate>
            <li>            
                <table cellpadding="0" cellspacing="0" border="0" style="width:300px;">
                    <tr>
                        <td style="width:30px; height: 35px;" align="center" valign="middle">                             
                        </td>
                        <td style="width:40px; height: 35px;" align="center" valign="middle"> 
                            <asp:Image ID="Image1" runat="server" ToolTip='<%# Bind("NombreCompleto") %>' ImageUrl="/SaintGeorgeOnline/Fotos/noPhoto.gif" width="30" height="30"/>                            
                        </td>
                        <td style="width:20px; height: 35px;" align="center" valign="middle"> 
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_ver.png" 
                                CommandName="VerMas" CommandArgument='<%# Bind("CodigoTrabajador") %>' ToolTip="Ver Mas" />
                        </td>
                        <td style="width:200px; height: 35px;" align="left" valign="middle"> 
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("NombreCompleto") %>' style="font-family:arial; font-size: 10px; font-weight:bold;" />
                        </td>
                        <td style="width:10px; height: 35px;" align="center" valign="middle">                             
                        </td>
                    </tr>
                </table>                            
			</li>                
            </ItemTemplate>
            </asp:Repeater>
        </ul> &nbsp;
    </div>            
        
        </td>
    </tr> 
    <tr>
        <td style="width:310px; height:29px;" align="center" valign="middle">
            &nbsp;
        </td>
    </tr>       
         
</table>
    

</div>  

</div>
</asp:Panel> 

<div id="ModalPopUP_AlertaCalendarioCumpleanios">           
    <atk:ModalPopupExtender ID="pnModalListaFamiliares" runat="server"
        TargetControlID="VerListaFamiliares"
        PopupControlID="miPanel"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="false" 
        OkControlID="OKListaFamiliares" 
        CancelControlID="CancelListaFamiliares"
        Drag="true" 
        PopupDragHandleControlID="miDragControl" />           

    <asp:panel id="miPanel" runat="server" BackColor="#4c4c4c" style="background:url('/SaintGeorgeOnline/App_Themes/Imagenes/fondoAlerta1_modal.png') no-repeat 0 0 transparent; width:376px; height:229px; display: none">  
        <table cellpadding="0" cellspacing="0" border="0" width="376px">    
        <tr>
            <td style="width: 376px; height: 50px" valign="middle" align="center" colspan="2">   
                <table cellpadding="0" cellspacing="0" border="0" style="width:120px; height: 50px">
                    <tr>
                        <td style="width:90px; height: 50px;" align="right" valign="middle">
                            <span id="miDragControl" style="color: #000000; font-weight:bold; font-size:14px; font-family:Arial; cursor: pointer;">Alerta</span>&nbsp;&nbsp;
                        </td>
                        <td style="width:30px; height: 50px;" align="right" valign="bottom">
                            <asp:ImageButton ID="btnCerraListaFamiliares" runat="server" Width="25" Height="29"
                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/x.png" CssClass="miPaddingBottom"
                                onclick="btnCerraListaFamiliares_Click" ToolTip="Cerrar Panel"/>
                        </td>
                    </tr>
                </table> 
            </td>            
        </tr>
        <tr>
            <td style="width: 25px; height: 159px" valign="top" align="center">            
            </td>
            <td style="width: 326px; height: 159px; border: solid 0px green" valign="top" align="center">    
                <table cellpadding="0" cellspacing="0" border="0" style="width:326px; height: 159px">
                    <tr>
                        <td style="width: 110px; height: 127px; border: solid 0px red ; margin:auto">
                            <asp:Image ID="imgFoto" runat="server" Width="90" Height="107"/>
                        </td>
                        <td style="width: 226px; height: 127px; margin:auto">
                            <table cellpadding="0" cellspacing="0" border="0" style="width:226px; height: 127px">
                                <tr>
                                    <td style="width: 226px; height:33px" align="center" valign="middle" colspan="2">
                                        <asp:Label ID="lblModalNombre" runat="server" style="border-bottom: solid 2px #4c4c4c; font-family: Arial; font-size: 11px; font-weight: bold; color: Black;"/>
                                    </td>    
                                </tr>
                                <tr>
                                    <td style="width: 226px; height:24px" align="center" valign="middle" colspan="2">   
                                        <table cellpadding="0" cellspacing="0" border="0" style="width:226px; height: 24px">
                                            <tr>
                                                <td style="width: 50px; height:24px" align="right" valign="middle">   
                                                    <img alt="Cumpleaños" src="/SaintGeorgeOnline/App_Themes/Imagenes/cake_logo.png" />&nbsp;
                                                </td>
                                                <td style="width: 176px; height:24px" align="left" valign="middle">   
                                                    <span style="font-family: Arial; font-size: 9px; color: Black;">Fecha de Cumpleaños&nbsp;<asp:Label ID="lblModalBirthday" runat="server"/></span>
                                                </td>
                                            </tr>
                                        </table>                                         
                                    </td>
                                </tr>
                                <tr>   
                                    <td style="width: 226px; height:16px" align="left" valign="middle" colspan="2">  
                                        <span style=" font-family: Arial; font-size: 10px; color: Black; padding-left:10px;">Correo :&nbsp;<asp:Label ID="lblModalCorreo" runat="server"/></span>
                                    </td>
                                </tr>  
                                <tr>   
                                    <td style="width: 226px; height:54px" align="left" valign="top" colspan="2">  
                                        <span style=" font-family: Arial; font-size: 10px; color: Black; padding-left:10px;">Skype :&nbsp;<asp:Label ID="lblModalSkype" runat="server"/></span>
                                    </td>                                   
                                </tr>
                            </table>
                        </td>
                    </tr>
                
                    <tr>
                        <td colspan="2" style="width:100%; height: 32px;" align="center" valign="middle">
                            
                        </td>
                    </tr>
                    
                </table>        
            </td>
            <td style="width: 25px; height: 159px" valign="top" align="center">            
            </td>
        </tr>
        <tr>
            <td style="width: 376px; height: 20px" valign="middle" align="center" colspan="2">                   
            </td>            
        </tr>              
    </table>     
    <div id="controlListaFamiliares" style="display:none">
            <input type="button" id="VerListaFamiliares" runat="server" />
            <input type="button" id="OKListaFamiliares" />
            <input type="button" id="CancelListaFamiliares" />
    </div>       
    </asp:panel>       
</div>  


 

