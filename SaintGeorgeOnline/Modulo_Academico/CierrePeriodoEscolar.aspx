<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="CierrePeriodoEscolar.aspx.vb" Inherits="Modulo_Academico_CierrePeriodoEscolar" %>

<%@ OutputCache Location ="None" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style type="text/css"> 
.mibotonGrid
{
    text-decoration: none; 
    background-color: #2a5e97;
    color: #ffffff; 
    font-weight: bold; font-size: 10px;  
    display: block; line-height: 18px; float: left;
    -webkit-border-radius: 5px;
    -moz-border-radius: 5px;
    border-radius: 5px;   
    padding: 0 10px; 
    margin: 0 5px 0 0;
    /*width: 40px;*/ 
    vertical-align: middle; text-align: center;         
     
    }
.mibotonGrid:hover
{
    text-decoration: none; 
    background-color: #a1b5cd;
    color: #23527e; 
    /*background: rgba(0,0,0,.2);*/
    font-weight: bold; font-size: 10px; 
    display: block; line-height: 18px; float: left; 
    -webkit-border-radius: 5px;
    -moz-border-radius: 5px;
    border-radius: 5px;   
    padding: 0 10px; 
    margin: 0 5px 0 0;
    /*width: 40px;*/
    vertical-align: middle; text-align: center;    
    }     
.miboton
{
    text-decoration: none; 
    background-color: #2a5e97;
    color: #ffffff; 
    font-weight: bold; font-size: 12px;  
    display: block; line-height: 25px; float: left;
    -webkit-border-radius: 5px;
    -moz-border-radius: 5px;
    border-radius: 5px;   
    padding: 0 5px; 
    margin: 0 5px 0 0;
    /*width: 80px;*/ 
    vertical-align: middle; text-align: center;         
     
    }
.miboton:hover
{
    text-decoration: none; 
    background-color: #a1b5cd;
    color: #23527e; 
    /*background: rgba(0,0,0,.2);*/
    font-weight: bold; font-size: 12px; 
    display: block; line-height: 25px; float: left; 
    -webkit-border-radius: 5px;
    -moz-border-radius: 5px;
    border-radius: 5px;   
    padding: 0 5px; 
    margin: 0 5px 0 0;
    /*width: 80px;*/
    vertical-align: middle; text-align: center;    
    }     
    
</style>

<div id="miContainerMantenimiento">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<div style="border: solid 0px blue; width: 810px; font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px;">
<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 810px;">
<tr><td colspan="5" style="height: 10px;"></td></tr>
<tr>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Periodo:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">   
<asp:DropDownList ID="ddlPeriodo" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;"
    OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList> 
    </td>
    
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Nivel:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">     
<asp:DropDownList ID="ddlNivel" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;"
    OnSelectedIndexChanged="ddlNivel_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>  
    </td>
    
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Grado:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">     
<asp:DropDownList ID="ddlGrado" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;"
    OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>  
    </td>
    
    <td style="width: 330px; height: 25px;" align="left" valign="middle">    
    </td>
</tr>


<tr>
    <td style="width: 210px; height: 35px;" align="left" valign="middle" colspan="3">   
<asp:LinkButton ID="btnCalcularNota" runat="server" CausesValidation="false" 
    Text="Calcular Nota Final (1)" class="miboton" OnClick="btnCalcularNota_Click" />  
    </td>
    <td style="width: 600px; height: 35px;" align="left" valign="middle" colspan="4">   
<span style="color: Red; font-weight: bold;">Nota: Se calcula la nota de cursos oficiales sin internos, internos y complementarios</span>     
    </td>
</tr>

<tr>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Bimestre:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle"> 
<asp:DropDownList ID="ddlBimestre" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;">
    <asp:ListItem Value="1" Text="1ero"></asp:ListItem>
    <asp:ListItem Value="2" Text="2do"></asp:ListItem>
    <asp:ListItem Value="3" Text="3ero"></asp:ListItem>
    <asp:ListItem Value="4" Text="4to"></asp:ListItem>
    <asp:ListItem Value="5" Text="Anual"></asp:ListItem>
</asp:DropDownList>  
    </td>
    <td style="width: 650px; height: 25px;" align="left" valign="middle" colspan="5"> 
    </td>
</tr>

<tr>
    <td style="width: 210px; height: 35px;" align="left" valign="middle" colspan="3">   
<asp:LinkButton ID="btnCalcularNotaOf" runat="server" CausesValidation="false" 
    Text="Calcular Nota Final (2)" class="miboton" OnClick="btnCalcularNotaOf_Click" />  
    </td>
    <td style="width: 600px; height: 35px;" align="left" valign="middle" colspan="4">   
<span style="color: Red; font-weight: bold;">Nota: Se calcula solo la nota de cursos oficiales con internos</span>     
    </td>
</tr>


<tr><td colspan="7" style="height: 10px;"></td></tr>
<tr>
    <td colspan="6" style="height: 25px;" align="left" valign="middle">         
<table cellpadding="0" cellspacing="0" border="0" 
    style="width: 382px; height: 25px; border: solid 1px #a6a3a3;
           color: #23527e; background-color: #a1b5cd; margin: 0; padding: 0;
           font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">
    <tr>
            <td style="width:  30px; height: 25px;" align="center" valign="middle">
            </td>
            <td style="width:  70px; height: 25px;" align="center" valign="middle">
                <span>Periodo</span>                                                                 
            </td>
            <td style="width:  100px; height: 25px;" align="center" valign="middle">
                <span>Grado</span>                                                                 
            </td>
            <td style="width:  130px; height: 25px;" align="center" valign="middle">
                <span>Aula</span>                                                                 
            </td>
            <td style="width:  52px; height: 25px;" align="center" valign="middle">   
                <asp:CheckBox ID="chkAll" runat="server" />                                                
            </td>                    
    </tr>
</table> 
    </td>
    <td style="width: 330px; height: 25px;" align="left" valign="middle">  
    </td>
</tr>
<tr>
    <td colspan="6" style="height: 25px;" align="left" valign="middle">  
<div style=" width:380px; height: auto; margin: 0; padding: 0; border: solid 1px #a6a3a3;">   
<asp:GridView ID="GridView1" runat="server" 
    Width="380px" 
    CssClass="miGridviewBusqueda" 
    GridLines="None" 
    AutoGenerateColumns="false"
    AllowPaging="false" 
    AllowSorting="false"
    ShowFooter="false"
    ShowHeader="false"
    EmptyDataText=" - No se encontraron resultados - "
    OnRowDataBound="GridView1_RowDataBound">
<Columns>
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblPeriodo" runat="server" Text='<%# Bind("DescAnioAcademico") %>' /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblGrado" runat="server" Text='<%# Bind("DescGrado") %>' /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="130px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblAula" runat="server" Text='<%# Bind("DescAula") %>' /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">                                                                                 
    <ItemTemplate>
        <asp:CheckBox ID="chkG" runat="server" />
        <asp:Label ID="lblCodigoPeriodoAcademico" runat="server" Text='<%# Bind("CodigoAnioAcademico") %>' style="display: none;" /> 
        <asp:Label ID="lblCodigoGrado" runat="server" Text='<%# Bind("CodigoGrado") %>' style="display: none;" /> 
        <asp:Label ID="lblCodigoAsignacionAula" runat="server" Text='<%# Bind("Codigo") %>' style="display: none;" /> 
    </ItemTemplate>
</asp:TemplateField> 

</Columns>
</asp:GridView> 
</div>
    </td>
    <td style="width: 330px; height: 25px;" align="left" valign="top">   

<asp:UpdateProgress ID="upLoading" runat="server">
<ProgressTemplate>
        <div style="width: 330px; height: 40px; font-weight: bold; border: solid 1px #6e8aba;">             
            <div style="width: 40px; height: 35px; margin: 0; padding: 0; float: left;">  
                <img src="../App_Themes/Imagenes/ajax-loader.gif" alt="PROCESANDO" style="width: 35px; height: 35px;" />
            </div>
            <div style="width: 270px; height: 35px; margin: 0; padding: 0; float: left;">  
                <span style="font-family: Arial; font-size: 22pt; color: #6e8aba; margin: 0; padding: 0;">PROCESANDO... </span>  
            </div>
        </div>
</ProgressTemplate>
</asp:UpdateProgress>
    
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

        var gridAulas = document.getElementById("<%= GridView1.ClientID %>");
        $('input:checkbox[id$=chkAll]').click(function(e) {
            if (gridAulas != undefined) {
                if (gridAulas.rows.length > 0) {
                    var estadoCheck = $(this).attr('checked');
                    $(gridAulas).find("input:checkbox").each(function(e, checkbox) {
                        if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                        else { $(checkbox).removeAttr('checked'); }
                    });
                }
            }
        });

        $('input:checkbox[id$=chkG]').click(function(e) {
            if ($(this).attr('checked') == true) { console.log('cAsigAula: ' + $(this).next().next().next().text()); }
        });

    });

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            var gridAulas = document.getElementById("<%= GridView1.ClientID %>");
            $('input:checkbox[id$=chkAll]').click(function(e) {
                if (gridAulas != undefined) {
                    if (gridAulas.rows.length > 0) {
                        var estadoCheck = $(this).attr('checked');
                        $(gridAulas).find("input:checkbox").each(function(e, checkbox) {
                            if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                            else { $(checkbox).removeAttr('checked'); }
                        });
                    }
                }
            });

            $('input:checkbox[id$=chkG]').click(function(e) {
                if ($(this).attr('checked') == true) { console.log('cAsigAula: ' + $(this).next().next().next().text()); }
            });

        }
    }

    function confirm_procesar() {
        if (confirm('¿Está seguro que desea generar el proceso de Cierre de Año?') == true) {
            return true;
        }
        else {
            return false; 
        }
    }

    
</script>

</asp:Content>

