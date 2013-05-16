<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="CompromisoPagos.aspx.vb" Inherits="Interfaz_Familia_Copia_de_Modulo_CronogramaPagos_CompromisoPago" title="Página sin título" %>
<%@ MasterType VirtualPath="~/Interfaz_Familia/Plantilla_Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript">

 function MostrarImpresionCompromisoPago_html() {

     window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_CompromisoPago_html.aspx', '_blank', 'width=5,height=5');
 }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <table border="0" cellpadding="0" cellspacing="0" style="width: 565px;">
        <tr>
            <td align="center" valign="middle" style="width:565px; height:20px; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_cab4.png'); background-repeat:no-repeat;">         
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" style="width: 565px; height:auto; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_centro4.png');background-repeat:repeat-y;">
<br />       
<table cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td style="width: 413px; height: 26px;" align="left" valign="middle">
<span style="font-size: 8pt; font-family: Arial;">Listado de compromisos de pago :&nbsp;</span>
        </td>
    </tr>
</table>   
<br />
<table  cellpadding="0" cellspacing="0" border="0" 
    style="width: 413px; height: 26px; color:White; border-left: solid 1px #a6a3a3; border-right: solid 1px #a6a3a3; 
            font-size:10px; font-family: Verdana, Arial, Helvetica, sans-serif;" class="miGVBusquedaFicha_Header">
    <tr>
        <td style="width: 30px; height: 26px;" align="center" valign="middle">
            <span>E</span>                                                                 
        </td>
        <td style="width: 183px; height: 26px;" align="center" valign="middle">
            <span>Familiar Compromiso</span>                                                                 
        </td>        
        <td style="width: 100px; height: 26px;" align="center" valign="middle">
            <span>Fecha</span>                                                                 
        </td>  
        <td style="width: 100px; height: 26px;" align="center" valign="middle">
            <span>Familia</span>                                                                 
        </td>                      
    </tr>    
    <tr>
        <td colspan="4" align="left" valign="top" style=" width :554px; height :auto;">

    <div id="miGridviewMantActualizacion_Ficha" style="border: solid 1px #a6a3a3; width: 410px; padding: 0; margin: 0;"> 

       <asp:GridView ID="GridView1" runat="server" 
        Width="410px" 
        CssClass="miGridviewBusqueda"
        GridLines="None" 
        AutoGenerateColumns="False" 
        AllowPaging="True" 
        AllowSorting="True"
        ShowHeader="false"
        PageSize="10" 
        EmptyDataText=" - No se encontraron resultados - " 
        OnRowCommand="GridView1_RowCommand">
            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="CodCompPago" Visible="False" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnExpWord" runat="server" 
                            CommandArgument='<%# Bind("CodCompPago") %>' CommandName="ExportarWord" 
                            ImageUrl="~/App_Themes/Imagenes/opc_exp_Word.png" 
                            ToolTip="Exportar Registro" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="FamiliarCompromiso">
                    <ItemTemplate>
                        <asp:Label ID="lbFamiliarComp" runat="server" Text='<%# Bind("Familiar") %>' style="color: #000000" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="183px" />
                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="183px" />
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="FechaCompromiso">
                    <ItemTemplate>
                        <asp:Label ID="lbFechaCompromiso" runat="server" Text='<%# Bind("FechaCompromiso") %>' style="color: #000000" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Familia">
                    <ItemTemplate>
                        <asp:Label ID="lbEstado" runat="server" Text='<%# Bind("Familia") %>' style="color: #000000" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
    </div>     
       
        </td>
    </tr>        
</table>                      
                      
            </td>        
        </tr>               
        <tr>
            <td style="width: 565px; height:30px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_inferior4.png');background-repeat:no-repeat;">
        </tr>
    </table> 
         
    
    
    </ContentTemplate>    
</asp:UpdatePanel>
     
   
    
</asp:Content>

