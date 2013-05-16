<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="RegistroDeNotasExamenes.aspx.vb" Inherits="Modulo_Academico_RegistroDeNotasExamenes" %>

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
    
    .miboton {
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
    .miboton:hover {
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
</style>

<div id="miContainerMantenimiento">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">    
<ContentTemplate>

<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 930px;">
<tr><td colspan="7" style="height: 10px;"></td></tr>
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
<span>Grado:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">  
<asp:DropDownList ID="ddlGrado" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;"
    OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList> 
    </td>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Aula:</span>      
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">   
<asp:DropDownList ID="ddlAula" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;"
    OnSelectedIndexChanged="ddlAula_SelectedIndexChanged" AutoPostBack="true">  
</asp:DropDownList>  
    </td>
    <td style="width: 450px; height: 25px;" align="left" valign="middle">  
<asp:LinkButton ID="btnGrabar" runat="server" Text="Grabar" class="miboton" OnClick="btnGrabar_Click" />  
    </td>
</tr>
<tr>
    <td style="width: 930px; height: 25px;" align="left" valign="top" colspan="7">  


<div style="border: solid 1px #a6a3a3; margin: 0; padding:0; width: 930px;">    
    <table cellpadding="0" cellspacing="0" border="0" style="width: 930px; height: 26px; color:White; background-color: #555555; font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">  
      
                    
            <tr>
                <td style="width: 20px; height: 25px;" align="center" valign="middle">
                    <span>&nbsp;&nbsp;</span>
                </td>
                
                <td style="width: 80px; height: 25px;" align="center" valign="middle">
                    <span>Grado</span>
                </td>
                <td style="width: 80px; height: 25px;" align="center" valign="middle">
                    <span>Aula</span>
                </td>
                
                <td style="width: 100px; height: 25px;" align="center" valign="middle">
                    <span>Tipo</span>
                </td>
                <td style="width: 143px; height: 25px;" align="center" valign="middle">
                    <span>Curso</span>
                </td>
                <td style="width: 70px; height: 25px;" align="center" valign="middle">
                    <span>Fecha</span>
                </td>              
                <td style="width: 70px; height: 25px;" align="center" valign="middle">
                    <span>Código</span>
                </td>
                <td style="width: 280px; height: 25px;" align="center" valign="middle">  
                    <span>Nombre Completo</span>
                </td>
                <td style="width: 40px; height: 25px;" align="center" valign="middle"> 
                    <span>Nota</span>
                </td>   
                <td style="width:  30px; height: 25px;" align="center" valign="middle"> 
                <asp:CheckBox ID="chkAll" runat="server" />                             
                </td> 
                <td style="width:  17px; height: 25px;" align="center" valign="middle"> 
                    <span>&nbsp;&nbsp;&nbsp;</span>
                </td>                    
            </tr>
        </table>  
    </div>    
    <div style="overflow-y: scroll; overflow-x: hidden; width:930px; height: 315px; margin: 0; padding: 0; border: solid 1px #a6a3a3;"> 
        <asp:GridView ID="GridView1" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="913px"
            GridLines="None" 
            ShowFooter="false"
            ShowHeader="false"
            AutoGenerateColumns="False"
            EmptyDataText=" - No se encontraron resultados - "                         
            OnRowDataBound="GridView1_RowDataBound">
        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataRowStyle ForeColor="#da4a38" HorizontalAlign="Center" />                                                                                                                                      
            <Columns>
<asp:TemplateField HeaderText="idx" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" />
    </ItemTemplate>   
</asp:TemplateField> 

<asp:TemplateField HeaderText="Grado" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
    <ItemTemplate>
        <asp:Label ID="lblnGrado" runat="server" Text='<%# Bind("nGrado") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Aula" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
    <ItemTemplate>
        <asp:Label ID="lblnAula" runat="server" Text='<%# Bind("nAula") %>' />
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
    <ItemTemplate>
        <asp:Label ID="lblTipoExamen" runat="server" Text='<%# Bind("TipoExamen") %>' />
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Curso" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="143px">
    <ItemTemplate>
        <asp:Label ID="lblnCurso" runat="server" Text='<%# Bind("nCurso") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lblfechaExamen" runat="server" Text='<%# Bind("fechaExamen") %>' />
    </ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField HeaderText="Código" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lblcAlumno" runat="server" Text='<%# Bind("cAlumno") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Alumno" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px">
    <ItemTemplate>
        <asp:Label ID="lblnAlumno" runat="server" Text='<%# Bind("nAlumno") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Nota" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
    <ItemTemplate>
        <asp:TextBox ID="tbNota" runat="server" MaxLength="2" style="width: 30px; font-size: 8pt; font-family: Arial; text-align: right;" />
        <asp:Label ID="lblTipoReg" runat="server" Text='<%# Bind("TipoReg") %>' style="display: none;"/>
        <asp:Label ID="lblcodRegistroCargo" runat="server" Text='<%# Bind("codRegistroCargo") %>' style="display: none;"/>
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">                                                                                 
    <ItemTemplate>
        <asp:CheckBox ID="chkG" runat="server" />
    </ItemTemplate>
</asp:TemplateField> 

            </Columns>
        </asp:GridView> 
    </div>    
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

