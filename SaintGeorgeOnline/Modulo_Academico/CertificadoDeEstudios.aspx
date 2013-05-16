<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="CertificadoDeEstudios.aspx.vb" Inherits="Modulo_Notas_CertificadoDeEstudios" %>

<%@ OutputCache Location ="None" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style type="text/css"> 
         
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
    width: 80px;
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
    width: 80px;
    vertical-align: middle; text-align: center;    
    }     
    
    </style>

<div id="miContainerMantenimiento">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">    
    <Triggers>    
        <asp:PostBackTrigger ControlID="btnExportar" />
    </Triggers>
<ContentTemplate>

<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 810px;">
<tr><td colspan="7" style="height: 10px;"></td></tr>
<tr>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Periodo:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">   
<asp:DropDownList ID="ddlPeriodo" runat="server" style="width: 100px; font-size: 8pt; font-family: Courier New, Arial;"
    OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList> 
    </td>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Grado:</span>
    </td>
    <td style="width: 250px; height: 25px;" align="left" valign="middle">      
<asp:DropDownList ID="ddlGrado" runat="server" style="width: 240px; font-size: 8pt; font-family: Courier New, Arial;"
    OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>  
    </td>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Aula:</span>
    </td>
    <td style="width: 150px; height: 25px;" align="left" valign="middle">      
<asp:DropDownList ID="ddlAula" runat="server" style="width: 140px; font-size: 8pt; font-family: Courier New, Arial;"
    OnSelectedIndexChanged="ddlAula_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>  
    </td>
    <td style="width: 150px; height: 25px;" align="left" valign="middle">  
    </td>
</tr>
<tr>
    <td style="width: 510px; height: 25px;" align="left" valign="top" colspan="5">  
    <div id="miGridviewMantActualizacion_Ficha" style="width: 480px; height: 25px; margin: 0; padding: 0; border: solid 1px #a6a3a3;">     
    <table cellpadding="0" cellspacing="0" border="0" 
                    style="width: 480px; height: 25px; 
                    color: #23527e; background-color: #a1b5cd; 
                    font-size: 10px; font-weight: bold; 
                    font-family: Verdana, Arial, Helvetica, sans-serif;">
            <tr>
                <td style="width: 20px; height: 25px;" align="center" valign="middle">
                    <span>&nbsp;&nbsp;</span>
                </td>
                <td style="width: 90px; height: 25px;" align="center" valign="middle">
                    <span>Código</span>
                </td>
                <td style="width: 323px; height: 25px;" align="center" valign="middle">  
                    <span>Apellidos y Nombres</span>
                </td>
                <td style="width:  30px; height: 25px;" align="left" valign="middle"> 
                    <asp:CheckBox ID="chkAll" runat="server" />
                </td>   
                <td style="width:  17px; height: 25px;" align="center" valign="middle"> 
                    <span>&nbsp;&nbsp;&nbsp;</span>
                </td>                    
            </tr>
        </table>      
    </div>    
    <div style="overflow-y: scroll; overflow-x: hidden; width:480px; height: 315px; margin: 0; padding: 0; border: solid 1px #a6a3a3; ">   
        <asp:GridView ID="GridView1" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="463px"
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
            
<asp:TemplateField HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px">
    <ItemTemplate>
        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
    </ItemTemplate>
</asp:TemplateField>                
<asp:TemplateField HeaderText="Nombre de Alumno" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="left" ItemStyle-Width="323px">
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
    <td style="width: 300px; height: 25px;" align="left" valign="top" colspan="2">  
<asp:LinkButton ID="btnExportar" runat="server" CausesValidation="false" 
    Text="Exportar" class="miboton" OnClick="btnExportar_Click" /> 
    </td>
</tr>

</table>      

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

        var gridAlumnos = document.getElementById("<%= GridView1.ClientID %>");
        $('input:checkbox[id$=chkAll]').click(function(e) {
            if (gridAlumnos != undefined) {
                if (gridAlumnos.rows.length > 0) {
                    var estadoCheck = $(this).attr('checked');
                    $(gridAlumnos).find("input:checkbox").each(function(e, checkbox) {
                        if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                        else { $(checkbox).removeAttr('checked'); }
                    });
                }
            }
        });
        
    });

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            var gridAlumnos = document.getElementById("<%= GridView1.ClientID %>");
            $('input:checkbox[id$=chkAll]').click(function(e) {
                if (gridAlumnos != undefined) {
                    if (gridAlumnos.rows.length > 0) {
                        var estadoCheck = $(this).attr('checked');
                        $(gridAlumnos).find("input:checkbox").each(function(e, checkbox) {
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

