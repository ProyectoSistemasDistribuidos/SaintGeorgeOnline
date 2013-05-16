<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="AsignacionPermisosReportesPerfiles.aspx.vb" Inherits="Modulo_Permisos_AsignacionPermisosReportesPerfiles" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
               
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
</style>

<script type="text/javascript" >

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }
      
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional"  >
        
    <ContentTemplate>
    
 <div id="miContainerMantenimiento">
    <atk:TabContainer ID="TabContainer1" runat="server" Width="881px" ActiveTabIndex="0"
        AutoPostBack="false" ScrollBars="None" >        
        <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="true">
            <HeaderTemplate>
                 <asp:Label ID="lbTab2" runat="server" Text="Inserción" />
            </HeaderTemplate>
            <ContentTemplate>
                <div style="border: solid 0px blue; width: 850px;">
                    <div id="miDetalleMant">
                        
    <table cellpadding="0" cellspacing="0" border="0" width="850px" >                                
        <tr>
            <td style="width: 50px; height: 25px;" align="left" valign="middle"> 
                <span>Perfil :</span>
            </td>           
            <td style="width: 250px; height: 25px;" align="left" valign="middle"> 
                <asp:DropDownList ID="ddlPerfiles" runat="server" Width="240px" style="font-size: 8pt; font-family: Arial;"
                    OnSelectedIndexChanged="ddlPerfiles_SelectedIndexChanged" AutoPostBack="true"  >
                </asp:DropDownList>
            </td>
            <td style="width: 550px; height: 25px;" align="left" valign="middle"> 
    <asp:ImageButton ID="btnGrabar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
        onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
        onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
        onclick="btnGrabar_Click" /> 
            </td>   
        </tr>
        <tr><td align="left" valign="middle" colspan="3"><br /></td></tr>            
        <tr>
            <td align="left"  valign="middle" colspan="3" style="margin: 0; padding: 0;"> 
<div id="miGridviewMantActualizacion_Ficha" style="width: 647px; height: 26px; margin: 0; padding: 0; border-bottom: 0;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 647px; height: 26px; color:White; background-color: #0a0f14; 
            font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;" class="miGVBusquedaFicha_Header">
        <tr>
            <td style="width:  200px; height: 26px;" align="center" valign="middle">
                <span>Tipo Reporte</span>                                                                 
            </td>
            <td style="width:  200px; height: 26px;" align="center" valign="middle">
                <span>Reporte</span>                                                                 
            </td>
            <td style="width: 200px; height: 26px;" align="center" valign="middle">
                <span>Presentación</span>                                                                 
            </td>
            <td style="width: 30px; height: 26px;" align="center" valign="middle">                                                               
            </td>  
            <td style="width:  17px; height: 26px;" align="center" valign="middle">   
            </td>                      
        </tr>
    </table>      
</div>                                         

<div id="miGridviewMant" style="overflow-y: scroll; overflow-x: hidden; width: 647px; height: 295px; margin: 0; padding: 0;">          
    <asp:GridView ID="GridView1" runat="server" 
        CssClass="miGridviewBusqueda" 
        Width="630px"
        GridLines="None" 
        AutoGenerateColumns="False"
        AllowPaging="False" 
        AllowSorting="False"
        ShowFooter="false"
        ShowHeader="false"
        EmptyDataText=" - No se encontraron resultados - "          
        OnRowDataBound="GridView1_RowDataBound">
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                                                
    <Columns>    
                                 
<asp:TemplateField ItemStyle-Width="200px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <asp:Label ID="lblDescTipoReporte" runat="server" Text='<%# Bind("DescTipoReporte") %>' style="padding-left: 10px;" />
    </ItemTemplate>
</asp:TemplateField>             
<asp:TemplateField ItemStyle-Width="200px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <asp:Label ID="lblDescReporte" runat="server" Text='<%# Bind("DescReporte") %>' style="padding-left: 10px;" />
    </ItemTemplate>
</asp:TemplateField>           
<asp:TemplateField ItemStyle-Width="200px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <asp:Label ID="lblDescPresentacionReporte" runat="server" Text='<%# Bind("DescPresentacionReporte") %>' style="padding-left: 10px;" />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width="30px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <asp:CheckBox ID="chk" runat="server" />
    </ItemTemplate>
</asp:TemplateField>     
                         
<asp:TemplateField ItemStyle-Width="0" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle">
    <ItemTemplate>
        <asp:Label ID="lblCodigoTipoReporte" runat="server" Text='<%# Bind("CodigoTipoReporte") %>' />
    </ItemTemplate>
</asp:TemplateField>      
<asp:TemplateField ItemStyle-Width="0" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle">
    <ItemTemplate>
        <asp:Label ID="lblCodigoReporte" runat="server" Text='<%# Bind("CodigoReporte") %>' />
    </ItemTemplate>
</asp:TemplateField>    
<asp:TemplateField ItemStyle-Width="0" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle">
    <ItemTemplate>
        <asp:Label ID="lblCodigoPresentacionReporte" runat="server" Text='<%# Bind("CodigoPresentacionReporte") %>' />
    </ItemTemplate>
</asp:TemplateField>     
<asp:TemplateField ItemStyle-Width="0" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle">
    <ItemTemplate>
        <asp:Label ID="lblCodigoAsignacionPermisoReporte" runat="server" Text='<%# Bind("CodigoAsignacionPermisoReporte") %>' />
    </ItemTemplate>
</asp:TemplateField>   
<asp:TemplateField ItemStyle-Width="0" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle">
    <ItemTemplate>
        <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>' />
    </ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField ItemStyle-Width="0" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle">
    <ItemTemplate>
        <asp:Label ID="lblTipoAccion" runat="server" Text='<%# Bind("TipoAccion") %>' />
    </ItemTemplate>
</asp:TemplateField>  
    </Columns>
    </asp:GridView>
</div>             
            </td>
        </tr>
        <tr><td align="left" valign="middle" colspan="3"><br /></td></tr> 
    </table>
                       
                    </div>           
                </div>    
            </ContentTemplate> 
    </atk:TabPanel>
    </atk:TabContainer>     
    
    <asp:Panel ID="pnlImpresion" runat="server" BackColor="White" Height="71px" Width="388px"  style="display:none">
                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="text-align:right; ">
                                                        <asp:ImageButton ID="btnVolver" runat="server" 
                                                            ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10px; color: #000080; text-align: center;">
                                                        
                                                            <img alt="" src="../App_Themes/Imagenes/bigrotation2.gif" 
                                                            style="width: 32px; height: 32px" />
                                                        
                                                            <br />
                                                    
                                                    Exportando, espere unos segundos ...
                                                                                                
                                                    </td>
                                                </tr>    
                                        
                                            </table>
                                        </asp:Panel>
                            
    <atk:ModalPopupExtender ID="ModalPopupExtender1" 
                                        runat="server"
                                        DynamicServicePath="" 
                                        Enabled="True" 
                                        BackgroundCssClass="FondoAplicacion"
                                        DropShadow="True"
                                        PopupControlID="pnlImpresion"                    
                                        TargetControlID="lblAccionExportar"
                                        >
                                        </atk:ModalPopupExtender>
                                        
    <asp:Label ID="lblAccionExportar" runat="server" ForeColor="White" Text="..."></asp:Label>
           
    </div>        
    </ContentTemplate>
    </asp:UpdatePanel>
</div>  


<script type="text/javascript">

    $(document).ready(function() {

        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);

    });
    
</script>

</asp:Content>