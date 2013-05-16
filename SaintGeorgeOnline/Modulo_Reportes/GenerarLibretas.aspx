<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="GenerarLibretas.aspx.vb" Inherits="Modulo_Reportes_GenerarLibretas" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }      
    #miTablaFiltros span{
        margin: 0;
        padding: 0;
        font-size: 11px;
        font-family: Arial;    
    }      
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miPaginaMantenimiento">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>    
            <asp:PostBackTrigger ControlID="btnReporteExportar" />
        </Triggers>
    <ContentTemplate>

<div style="border: solid 0px blue; width: 880px;">
    <div id="miBusquedaMant">
    <fieldset style="width: 840px;">

<table cellpadding="0" cellspacing="0" border="0" style="width: 820px; border: solid 0px red; margin: 0 0 0 10px; padding: 0; font-size: 11px; font-family: Arial;">    

    <tr>
        <td style="width: 820px; height: 25px;" align="left" valign="middle" colspan="3">
Configuración Excel
        </td>
    </tr>   
    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle"></td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">
            <asp:RadioButtonList ID="rbList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="2" Width="150px">
                <asp:ListItem Text="Ingles" Value="2"></asp:ListItem>
                <asp:ListItem Text="Español" Value="1" Selected="True"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td style="width: 550px; height: 25px;" align="left" valign="middle">
    <asp:ImageButton ID="btnReporteExportar" runat="server" Width="84px" Height="19px" 
        ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
        onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
        onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'" ToolTip="Exportar"
        onclick="btnExportar_Click" />   
        </td>   
    </tr>
    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle">Periodo:</td>
        <td style="width: 750px; height: 25px;" align="left" valign="middle" colspan="2">
            <asp:DropDownList ID="ddlAnioAcademico" runat="server" style="width: 90px; font-size: 8pt; font-family: Arial;" TabIndex="1">
            </asp:DropDownList>                  
        </td>    
    </tr>  
    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle">Bimestre:</td>
        <td style="width: 750px; height: 25px;" align="left" valign="middle" colspan="2">
<asp:DropDownList ID="ddlBimestre" runat="server" style="width: 330px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList>                      
        </td>
    </tr>
    
    
    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle">Presentacion:</td>
        <td style="width: 750px; height: 25px;" align="left" valign="middle" colspan="2">
<asp:DropDownList ID="lstPresentacion" runat="server" style="width: 330px; font-size: 8pt; font-family: Arial;" TabIndex="2"
    OnSelectedIndexChanged="lstPresentacion_SelectedIndexChanged" AutoPostBack="true">
    
    <asp:ListItem Value="3" Text="Inicial"></asp:ListItem>
    <asp:ListItem Value="4" Text="Primaria"></asp:ListItem>
    <asp:ListItem Value="2" Text="Secundaria"></asp:ListItem>
    
</asp:DropDownList>      
        </td>
    </tr>
        
        
    
    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle">Salon:</td>
        <td style="width: 750px; height: 25px;" align="left" valign="middle" colspan="2">
<asp:DropDownList ID="ddlSalon" runat="server" style="width: 330px; font-size: 8pt; font-family: Courier New;" TabIndex="3"
    OnSelectedIndexChanged="ddlSalon_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>      
        </td>
    </tr>
    <tr>
        <td style="width: 820px; height: 25px;" align="left" valign="top" colspan="3">

    <div id="miGridviewMantActualizacion_Ficha" style="width: 400px; height: 26px; margin: 0; padding: 0; border: 0;">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 400px; height: 26px; color:White; background-color: #0a0f14; 
               font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;" class="miGVBusquedaFicha_Header">
            <tr>
                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                    <span>&nbsp;&nbsp;</span>
                </td>
                <td style="width: 70px; height: 26px;" align="center" valign="middle">
                    <span>Codigo</span>
                </td>
                <td style="width: 260px; height: 26px;" align="center" valign="middle">  
                    <span>Apellidos y Nombres</span>
                </td>
                <td style="width:  30px; height: 26px;" align="center" valign="middle"> 
                    <asp:CheckBox ID="chkAll1" runat="server" /><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </td>   
                <td style="width:  20px; height: 26px;" align="center" valign="middle"> 
                    <span>&nbsp;&nbsp;&nbsp;</span>
                </td>                    
            </tr>
        </table>      
    </div>      
        
        </td> 
    </tr>
    <tr>
        <td style="width: 820px; height: 25px;" align="left" valign="top" colspan="3">
        
    <div style="overflow-y: scroll; overflow-x: hidden; width:397px; height: 210px; margin: 0; padding: 0; border: solid 1px #a6a3a3; ">   
        <asp:GridView ID="GridView1" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="380px"
            GridLines="None" 
            ShowFooter="false"
            ShowHeader="false"
            AutoGenerateColumns="False"
            EmptyDataText=" - No se encontraron resultados - "                         
            OnRowDataBound="GridView1_RowDataBound">
        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                                                
            <Columns>


<asp:TemplateField HeaderText="idx" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" />
    </ItemTemplate>   
</asp:TemplateField>         
            
<asp:TemplateField HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
    </ItemTemplate>
</asp:TemplateField>                
<asp:TemplateField HeaderText="Nombre de Alumno" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="left" ItemStyle-Width="260px">
    <ItemTemplate>
        <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' />
    </ItemTemplate>
</asp:TemplateField>                 
<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30px">
    <ItemTemplate>
        <asp:CheckBox ID="chk" runat="server" />
    </ItemTemplate>
</asp:TemplateField>     
            </Columns>
        </asp:GridView>     
    </div>         
        
        </td> 
    </tr>
</table>

     </fieldset>
    </div>   
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

        var grid1 = document.getElementById("<%= GridView1.ClientID %>");   
        $('input:checkbox[id$=chkAll1]').click(function(e) {
            if (grid1 != undefined) {
                if (grid1.rows.length > 0) {
                    var estadoCheck = $(this).is(':checked');
                    $(grid1).find("input:checkbox").each(function(e, checkbox) {
                        if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                        else { $(checkbox).removeAttr('checked'); }
                    });
                }
            }
        });  
              
    });

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            var grid1 = document.getElementById("<%= GridView1.ClientID %>");
            $('input:checkbox[id$=chkAll1]').click(function(e) {
                if (grid1 != undefined) {
                    if (grid1.rows.length > 0) {
                        var estadoCheck = $(this).is(':checked');
                        $(grid1).find("input:checkbox").each(function(e, checkbox) {
                            if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                            else { $(checkbox).removeAttr('checked'); }
                        });
                    }
                }
            });
        }
    }    
    
</script>

</asp:Content>

