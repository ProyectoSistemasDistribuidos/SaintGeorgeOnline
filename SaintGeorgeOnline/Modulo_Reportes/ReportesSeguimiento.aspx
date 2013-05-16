<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ReportesSeguimiento.aspx.vb" Inherits="Modulo_Reportes_ReportesSeguimiento" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
    .contenido
    {
        font-family: Arial;
        font-size: 9px;
        font-weight: normal;  
    }
    
    #panelRegistro span{
        font-size: 11px;
        font-family: Arial;
    }
    #panelRegistro em{
        font-size: 10px;
        font-family: Arial;
        color: #a51515;
        margin-right: 7px;
        padding: 0;
    }                         
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<br />
<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">       
    <Triggers>
        <asp:PostBackTrigger ControlID="TabContainer1$miTab1$btnConsultar" />              
    </Triggers>
    <ContentTemplate>   
    <div id="miContainerMantenimiento">
     
    <atk:TabContainer ID="TabContainer1" runat="server" Width="881px" ActiveTabIndex="0" AutoPostBack="false" ScrollBars="None">        
    <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
        <HeaderTemplate>
            <asp:Label ID="lbTab1" runat="server" Text="Mid Term Report" />
        </HeaderTemplate>
        <ContentTemplate> 
            <div style="border: solid 0px blue; width: 860px;">            
                <div id="miBusquedaActualizacion_Ficha">
                    <!-- 650px -->
                    <fieldset>
                        <legend>Criterios de Búsqueda</legend>
                        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 820px;">
                            <tr>
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Mes :&nbsp;</span>
                                </td>                                                  
                                <td style="width: 450px; height: 25px;" align="left" valign="middle" colspan="3">
                                    <asp:DropDownList ID="ddlAsignacion" runat="server" Width="443px" style="font-size: 8pt; font-family: Courier New;">
                                    </asp:DropDownList>
                                </td>                                                
                                <td style="width: 320px; height: 25px;" align="right" valign="middle" colspan="5">
                                    <asp:ImageButton ID="btnConsultar" runat="server" Width="91px" Height="19px" 
                                        ImageUrl="~/App_Themes/Imagenes/btnConsultar_1.png"
                                        onmouseover="this.src = '../App_Themes/Imagenes/btnConsultar_2.png'" 
                                        onmouseout="this.src = '../App_Themes/Imagenes/btnConsultar_1.png'" ToolTip="Consultar"
                                        onclick="btnConsultar_Click" />   
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Grado :&nbsp;</span>
                                </td>                                                  
                                <td style="width: 450px; height: 25px;" align="left" valign="middle" colspan="3">
                                    <asp:DropDownList ID="ddlGrado" runat="server" Width="250px" style="font-size: 8pt; font-family: Courier New;"
                                        OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>                                                
                                <td style="width: 320px; height: 25px;" align="right" valign="middle" colspan="5">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Aula :&nbsp;</span>
                                </td>                                                  
                                <td style="width: 450px; height: 25px;" align="left" valign="middle" colspan="3">
                                    <asp:DropDownList ID="ddlAula" runat="server" Width="250px" style="font-size: 8pt; font-family: Courier New;"
                                        OnSelectedIndexChanged="ddlAula_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>                                                
                                <td style="width: 320px; height: 25px;" align="right" valign="middle" colspan="5">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Alumno :&nbsp;</span>
                                </td>                                                  
                                <td style="width: 450px; height: 25px;" align="left" valign="middle" colspan="3">
                                    <asp:DropDownList ID="ddlAlumno" runat="server" Width="350px" style="font-size: 8pt; font-family: Courier New;">
                                    </asp:DropDownList>
                                </td>                                                
                                <td style="width: 320px; height: 25px;" align="right" valign="middle" colspan="5">
                                </td>
                            </tr>
                            
                        </table>
                    </fieldset>
                </div>                                                                          
                <div class="miEspacio"></div>   
            </div>            
        </ContentTemplate>
    </atk:TabPanel> 
    </atk:TabContainer>
    
     </div>  
     
    </ContentTemplate>
    </asp:UpdatePanel>
  
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
        <atk:ModalPopupExtender id="ModalPopupExtender1" runat="server" 
            TargetControlID="lblAccionExportar" 
            PopupControlID="Panel1" 
            BackgroundCssClass="MiModalBackground" 
            DropShadow="false" />
         
        <asp:Panel ID="Panel1" runat="server" style="background-color:#FFFFFF;display:none;width:250px">
            <div style="margin: auto;">
            <table cellpadding="0" cellspacing="0" border="0" style="width:250px; border: solid 1px #000000">
                <tr><td colspan="3"></td>
                    <td style="width: 20px;" align="center" valign="middle">
                        <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" OnClick="btnVolver_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px;" align="left" valign="middle"></td>
                    <td style="width: 80px;" align="left" valign="middle">
                        <img alt="Procesando..." src="../App_Themes/Imagenes/ajax-loader.gif" />  
                    </td>
                    <td style="width: 130px;" align="left" valign="middle">
                        <span style="color: #6684b7; font-family: Arial; font-size: 9pt; font-weight: bold;">Procesando...<br />Espere un momento.</span>
                    </td>
                    <td style="width: 20px;" align="left" valign="middle">
                        
                    </td>
                </tr>
                <tr><td colspan="4"><br /></td></tr>
            </table>                              
            </div>
        </asp:Panel>        
    
        <asp:Label ID="lblAccionExportar" runat="server" Text="MiModalHandler" style="display:none;"/> 
</ContentTemplate>    
</asp:UpdatePanel>       
  
<script type="text/javascript">

    $(document).ready(function() {

        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);

    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_initializeRequest(initializeRequest);
    prm.add_endRequest(endRequest);

    var _postBackElement;

    function initializeRequest(sender, e) {

        if (prm.get_isInAsyncPostBack()) {
            e.set_cancel(true);
        }
        _postBackElement = e.get_postBackElement();

        if (_postBackElement.id.indexOf('ddlGrado') > -1 || _postBackElement.id.indexOf('ddlAula') > -1) {

            var ddlGrado = document.getElementById("<%=ddlGrado.ClientID%>");
            var codGrado = ddlGrado.options[ddlGrado.selectedIndex].value;
            
            if (codGrado > 0) {
                $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1').show();
            }
        }
    }

    function endRequest(sender, e) {
        if (_postBackElement.id.indexOf('ddlGrado') > -1 || _postBackElement.id.indexOf('ddlAula') > -1) {
            $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1').hide();
        }
    }

   function ShowMyModalPopup() {

       var ddlMes = document.getElementById("<%=ddlAsignacion.ClientID%>");
       var codMes = ddlMes.options[ddlMes.selectedIndex].value;

       var ddlGrado = document.getElementById("<%=ddlGrado.ClientID%>");
       var codGrado = ddlGrado.options[ddlGrado.selectedIndex].value;

       var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');

       if (codMes > 0 && codGrado > 0) {
           modal.show();
       }    

    }

</script>
    
</div>

</asp:Content>

