<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="AprobacionActividadesDireccion.aspx.vb" Inherits="Modulo_Actividades_AprobacionActividadesDireccion" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style type="text/css">  
.mibotonGrid{
    text-decoration: none; 
    background-color: #4b8efa;
    color: #ffffff; 
    border: 0;
    font-size: 12px;  
    display: block; line-height: 30px;

    padding: 0; /*0 5px;*/  
    margin: 0; /*0 5px 0 0;*/ 
    width: 60px; 
    vertical-align: middle; text-align: center;  
    border: solid 1px #3079ed;
    }
.mibotonGrid:hover{ 
    text-decoration: none; 
    background-color: #0072bb;
    color: #ffffff; 
    border: 0;
    font-size: 12px; 
    display: block; line-height: 30px;
    padding: 0; /*0 5px;*/ 
    margin: 0; /*0 5px 0 0;*/
    width: 60px;
    vertical-align: middle; text-align: center;    
    border: solid 1px #2f5bb7;
    }   
.miboton{
    text-decoration: none; 
    background-color: #4b8efa; 
    color: #ffffff; 
    border: 0;
    font-size: 12px;  
    display: block; line-height: 20px; float: left;
    padding: 0 5px; 
    margin: 0 5px 0 0;
    width: 80px;
    vertical-align: middle; text-align: center;   
    border: solid 1px #3079ed;   
    }
.miboton:hover{
    text-decoration: none; 
    background-color: #0072bb;
    color: #ffffff; 
    border: 0;font-size: 12px; 
    display: block; line-height: 20px; float: left; 
    padding: 0 5px; 
    margin: 0 5px 0 0;
    width: 80px;
    vertical-align: middle; text-align: center;   
    border: solid 1px #2f5bb7; 
    } 
     
.miGridRow {
    color: #000000; background-color: #ffffff; /*#f7f6f3; */ margin: 0; padding: 0; font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif;   
    } 
.miGridRow .row{
    /*height: 20px;*/
    border-bottom: solid 1px #a6a3a3; 
    border-right: solid 1px #a6a3a3; 
}  .miGridRow .rowb{
    /*height: 20px;*/
    border-bottom: solid 1px #a6a3a3; 
}        
    </style>

<div id="miContainerMantenimiento">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">      
    <ContentTemplate> 
         
<div style="border: solid 0px blue; width: 980px; font-family: Arial, Helvetica, sans-serif; font-size: 10px;">
<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 980px;">
<tr><td colspan="7" style="height: 10px;"></td></tr>

<tr>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Year:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">   
<asp:DropDownList ID="ddlPeriodo" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList> 
    </td>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Month:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">     
<asp:DropDownList ID="ddlMes" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList>  
    </td>   
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle"> 
    </td>
    <td style="width: 500px; height: 25px;" align="left" valign="middle">   
    </td> 
</tr>
<tr>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>State:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">  
<asp:DropDownList ID="ddlEstado" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;">
    <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
    <asp:ListItem Text="Approved" Value="2"></asp:ListItem>
    <asp:ListItem Text="Disapproved" Value="3"></asp:ListItem>
</asp:DropDownList> 
    </td>
    <td style="width: 160px; height: 25px;" align="left" valign="middle" colspan="2">
<asp:LinkButton ID="btnBuscar" runat="server" ToolTip="Search" Text="Search" class="miboton" OnClick="btnBuscar_Click" />    
    </td> 
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle"> 
    </td>
    <td style="width: 500px; height: 25px;" align="left" valign="middle">     
    </td>
</tr>    

<tr><td colspan="7" style="height: 10px;"></td></tr>

<tr>
    <td colspan="7" style="height: 25px;" align="left" valign="middle">    
<div style="border: solid 1px #a6a3a3; margin: 0; padding:0;">    
<table cellpadding="0" cellspacing="0" border="0" style="width: 980px; height: 26px; color:White; background-color: #555555; font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">
    <tr>
        <td style="width:  30px; height: 26px;" align="center" valign="middle">   
            <span></span>                                              
        </td>   
        <td style="width:  130px; height: 26px;" align="center" valign="middle">
            <span>Date</span>                                                                 
        </td>
        <td style="width:  330px; height: 26px;" align="center" valign="middle">
            <span>Activity</span>                                                                 
        </td>
        <td style="width:  100px; height: 26px;" align="center" valign="middle">  
            <span>State</span>                                             
        </td> 
        <td style="width:  250px; height: 26px;" align="center" valign="middle">  
            <span>Coments</span>                                             
        </td>    
        <td style="width:  70px; height: 26px;" align="center" valign="middle">  
            <span></span>                                             
        </td>
        <td style="width:  70px; height: 26px;" align="center" valign="middle">  
            <span></span>                                             
        </td>                   
    </tr>
</table> 
</div>    
    </td>
</tr>

<tr>
    <td colspan="7" style="height: 25px;" align="left" valign="middle">  
<div style="width:980px; height: 315px; margin: 0; padding: 0; border: solid 1px #a6a3a3;">   
<asp:GridView ID="GridView1" runat="server" 
    Width="980px" 
    CssClass="miGridviewBusqueda" 
    GridLines="None"     
    AutoGenerateColumns="False"
    ShowHeader="False"
    OnRowDataBound="GridView1_RowDataBound">
<EmptyDataRowStyle HorizontalAlign="Center" Font-Size="12px" Font-Bold="true" ForeColor="Red" />   
<RowStyle CssClass="miGridRow" /> 
<Columns>

<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" ItemStyle-CssClass="row">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" /> 
    </ItemTemplate>
</asp:TemplateField> 

<asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="130px" ItemStyle-CssClass="row">                                                                                 
    <ItemTemplate>
<table cellpadding="0" cellspacing="0" border="0" style="width: 130px;">
<tr>
    <td style="width: 50px; height: 20px;" align="left" valign="middle">Year:</td>
    <td style="width: 80px; height: 20px;" align="left" valign="middle"><asp:Label ID="lblPeriodo" runat="server" Text='<%# Bind("Periodo") %>' style="font-weight: bold;" /></td>
</tr>

<tr>
    <td style="width: 50px; height: 20px;" align="left" valign="middle">Month:</td>
    <td style="width: 80px; height: 20px;" align="left" valign="middle"><asp:Label ID="lblMes" runat="server" Text='<%# Bind("Mes") %>' style="font-weight: bold;" /></td>
</tr>

<tr>
    <td style="width: 50px; height: 20px;" align="left" valign="middle">Date:</td>
    <td style="width: 80px; height: 20px;" align="left" valign="middle"><asp:Label ID="lblFecha" runat="server" Text='<%# Bind("Fecha") %>' style="font-weight: bold;" /></td>
</tr>
</table>    
    </ItemTemplate>
</asp:TemplateField> 

<asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="330px" ItemStyle-CssClass="row">                                         
    <ItemTemplate>
<table cellpadding="0" cellspacing="0" border="0" style="width: 330px;">
<tr>
    <td style="width: 80px; height: 20px;" align="left" valign="middle">Activity:</td>
    <td style="width: 250px; height: 20px;" align="left" valign="middle"><asp:Label ID="lblActividad" runat="server" Text='<%# Bind("Actividad") %>' style="font-weight: bold;" /></td>
</tr>
<tr>
    <td style="width: 80px; height: 20px;" align="left" valign="middle">Type:</td>
    <td style="width: 250px; height: 20px;" align="left" valign="middle"><asp:Label ID="lbltipoAct" runat="server" Text='<%# Bind("tipoAct") %>' style="font-weight: bold;" /></td>
</tr>
<tr>
    <td style="width: 80px; height: 20px;" align="left" valign="middle">Organiser:</td>
    <td style="width: 250px; height: 20px;" align="left" valign="middle"><asp:Label ID="lblOrganizador" runat="server" Text='<%# Bind("Organizador") %>' style="font-weight: bold;" /></td>
</tr>
<tr>
    <td style="width: 80px; height: 20px;" align="left" valign="middle">Grades:</td>
    <td style="width: 250px; height: 20px;" align="left" valign="middle"><asp:Label ID="lblGrades" runat="server" style="font-weight: bold;" /></td>
</tr>

</table>     
    </ItemTemplate>
</asp:TemplateField> 

<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" ItemStyle-CssClass="row" ItemStyle-Height="50px">
    <ItemTemplate>
<asp:DropDownList ID="ddlEstado" runat="server" style="width: 90px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList>        
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px" ItemStyle-CssClass="row" ItemStyle-Height="50px">
    <ItemTemplate>
        <asp:TextBox ID="tbComentario" runat="server" Text='<%# Bind("Comentario") %>' MaxLength="500" Rows="4" TextMode="MultiLine" style="width: 240px; font-size: 8pt; font-family: Arial;" />
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px"  ItemStyle-CssClass="row">                                                                                 
    <ItemTemplate>
<asp:LinkButton ID="btnGrabar" runat="server" CommandName="Editar" CommandArgument='<%# Bind("cProgAct") %>' ToolTip="Save" Text="Save" class="mibotonGrid" onclick="btnGrabar_Click" />             
    </ItemTemplate>
</asp:TemplateField> 

<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px"  ItemStyle-CssClass="rowb">                                                                                 
    <ItemTemplate>
<asp:LinkButton ID="btnImprimir" runat="server" CommandName="Imprimir" CommandArgument='<%# Bind("cProgAct") %>' ToolTip="Print" Text="Print" class="mibotonGrid" onclick="btnImprimir_Click" />  
    </ItemTemplate>
</asp:TemplateField> 

<asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-CssClass="miHiddenStyle" ItemStyle-Width="0">                                                                            
    <ItemTemplate>
<asp:Label ID="lblcProgAct" runat="server" Text='<%# Bind("cProgAct") %>' style="display: none;" />   
<asp:Label ID="lblcRegApro" runat="server" Text='<%# Bind("cRegApro") %>' style="display: none;" />             
    </ItemTemplate>
</asp:TemplateField> 

</Columns>
</asp:GridView> 
</div>
    </td>
</tr>  

</table>     
</div>

    </ContentTemplate>
</asp:UpdatePanel> 
 
</div>

<script type="text/javascript" src="../App_Themes/Javascript/jquery-1.4.1.min.js" ></script> 
<script type="text/javascript">
    $(document).ready(function() {
        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);
    });
    function FormatoImpresion() {
        window.open('/SaintGeorgeOnline/Modulo_Actividades/formatoRegistroActividades.aspx', '_blank', 'menubar=0,resizable=0,width=500,height=200');
    }
</script>

</asp:Content>

