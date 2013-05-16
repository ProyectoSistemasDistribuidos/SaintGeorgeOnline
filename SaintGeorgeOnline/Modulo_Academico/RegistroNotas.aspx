<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="RegistroNotas.aspx.vb" Inherits="Modulo_Notas_RegistroNotas" %>

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
 <ContentTemplate>
    <atk:TabContainer ID="TabContainer1" runat="server" Width="850px" ActiveTabIndex="1" AutoPostBack="false" ScrollBars="None" >
        <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Busqueda" />
            </HeaderTemplate>
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
<span>Grado:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">     
<asp:DropDownList ID="ddlGrado" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;"
    OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>  
    </td>
    <td style="width: 490px; height: 25px;" align="left" valign="middle">   
    </td> 
</tr>
<tr><td colspan="5" style="height: 10px;"></td></tr>
<tr>
    <td colspan="5" style="height: 25px;" align="left" valign="middle">         
<table cellpadding="0" cellspacing="0" border="0" 
    style="width: 810px; height: 25px; 
           color: #23527e; background-color: #a1b5cd; margin: 0; padding: 0;
           font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">
    <tr>
            <td style="width:  20px; height: 25px;" align="center" valign="middle">
            </td>
            <td style="width:  40px; height: 25px;" align="center" valign="middle">
                <span>Periodo</span>                                                                 
            </td>
            <td style="width:  80px; height: 25px;" align="center" valign="middle">
                <span>Grado</span>                                                                 
            </td>
            <td style="width:  100px; height: 25px;" align="center" valign="middle">
                <span>Aula</span>                                                                 
            </td>
            <td style="width:  80px; height: 25px;" align="center" valign="middle">   
                <span>Código</span>                                              
            </td>
            <td style="width:  260px; height: 25px;" align="center" valign="middle">   
                <span>Nombre Completo</span>                                              
            </td>
            <td style="width:  50px; height: 25px;" align="center" valign="middle">   
                <span></span>                                              
            </td> 
            <td style="width:  163px; height: 25px;" align="center" valign="middle">  
            </td>
            <td style="width:  17px; height: 25px;" align="center" valign="middle">   
                <span></span>                                              
            </td>                     
    </tr>
</table> 
    </td>
</tr>
<tr>
    <td colspan="5" style="height: 25px;" align="left" valign="middle">  
<div style="overflow-y: scroll; overflow-x: hidden; width:810px; height: 315px; margin: 0; padding: 0;">   
<asp:GridView ID="GridView1" runat="server" 
    Width="793px" 
    CssClass="miGridviewBusqueda" 
    GridLines="None" 
    AutoGenerateColumns="false"
    AllowPaging="false" 
    AllowSorting="false"
    ShowFooter="false"
    ShowHeader="false"
    EmptyDataText=" - No se encontraron resultados - "
    OnRowDataBound="GridView1_RowDataBound"
    OnRowCommand="GridView1_RowCommand">
<Columns>
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblPeriodo" runat="server" Text='<%# Bind("Periodo") %>' /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblGrado" runat="server" Text='<%# Bind("Grado") %>' /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblAula" runat="server" Text='<%# Bind("Aula") %>' /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="260px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">                                                                                 
    <ItemTemplate>
<asp:LinkButton ID="btnSeleccionar" runat="server" CommandName="seleccionar" CommandArgument='<%# Bind("CodigoAlumno") %>' 
    ToolTip="Editar Registro"  CausesValidation="false" Text="Editar" class="mibotonGrid" />  
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="163px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblCodigoAula" runat="server" Text='<%# Bind("CodigoAula") %>' style="display: none;" /> 
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
        </atk:TabPanel>  
        <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="false">
            <HeaderTemplate>
                 <asp:Label ID="lbTab2" runat="server" Text="Registro" />
            </HeaderTemplate>
            <ContentTemplate>
<div style="border: solid 0px blue; width: 810px; font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px;">
<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 810px;">
<tr><td colspan="8" style="width: 810px; height: 10px;"></td></tr> 
<tr>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Periodo:</span>
    </td>
    <td style="width: 70px; height: 25px;" align="left" valign="middle">
<asp:Label ID="lblPeriodoAlumno" runat="server" style="font-weight: bold;" />   
    </td>  
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Grado:</span>
    </td>
    <td style="width: 100px; height: 25px;" align="left" valign="middle">   
<asp:Label ID="lblGradoAlumno" runat="server" style="font-weight: bold;" />   
    </td>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">  
<span>Aula:</span>
    </td>  
    <td style="width: 100px; height: 25px;" align="left" valign="middle">  
<asp:Label ID="lblAulaAlumno" runat="server" style="font-weight: bold;" />   
    </td>  
    <td style="width: 110px; height: 25px;" align="left" valign="middle">  
    </td>  
    <td style="width: 280px; height: 25px;" align="left" valign="middle">  
    </td>   
</tr>
<tr>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Código:</span>
    </td>
    <td style="width: 70px; height: 25px;" align="left" valign="middle"> 
<asp:Label ID="lblCodigoAlumno" runat="server" style="font-weight: bold;" />  
<asp:HiddenField id="hiddenCodigoAula" runat="server" /> 
    </td> 
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Alumno:</span>
    </td>
    <td style="width: 640px; height: 25px;" align="left" valign="middle" colspan="5"> 
<asp:Label ID="lblNombreAlumno" runat="server" style="font-weight: bold;" />   
    </td>   
</tr>  
<tr>
    <td colspan="7" style="width: 530px; height: 25px;" align="left" valign="middle">      
<table cellpadding="0" cellspacing="0" border="0" 
    style="width: 520px; height: 25px; border: solid 1px #a6a3a3;  
           color: #23527e; background-color: #a1b5cd; margin: 0; padding: 0;
           font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">
    <tr>
            <td style="width:  30px; height: 25px;" align="center" valign="middle">
            </td>
            <td style="width:  200px; height: 25px;" align="center" valign="middle">
                <span>Curso</span>                                                                 
            </td>
            <td style="width:  40px; height: 25px;" align="center" valign="middle">
                <span>Tipo</span>                                                                 
            </td>
            <td style="width:  50px; height: 25px;" align="center" valign="middle">   
                <span>Nota 1</span>                                              
            </td> 
            <td style="width:  50px; height: 25px;" align="center" valign="middle">   
                <span>Nota 2</span>                                              
            </td> 
            <td style="width:  50px; height: 25px;" align="center" valign="middle">   
                <span>Nota 3</span>                                              
            </td> 
            <td style="width:  50px; height: 25px;" align="center" valign="middle">   
                <span>Nota 4</span>                                              
            </td> 
            <td style="width:  50px; height: 25px;" align="center" valign="middle">   
                <span>Nota F</span>                                              
            </td>            
    </tr>
</table> 
    </td>    
    <td style="width: 280px; height: 25px;" align="left" valign="middle">   
<asp:LinkButton ID="btnGrabar" runat="server" CausesValidation="false" Text="Grabar" class="miboton" OnClick="btnGrabar_Click" />   
&nbsp;   
<asp:LinkButton ID="btnCancelar" runat="server" CausesValidation="false" Text="Cancelar" class="miboton" OnClick="btnCancelar_Click" />  
    </td>
</tr>   
<tr>
    <td colspan="8" style="width: 810px" align="left" valign="middle"> 
<asp:GridView ID="GridView2" runat="server"
    Width="520px" 
    CssClass="miGridviewBusqueda"
    GridLines="none" 
    AutoGenerateColumns="false" 
    AllowPaging="false" 
    AllowSorting="false"
    ShowFooter="false" 
    ShowHeader="false" 
    EmptyDataText=" - No se encontraron resultados - "
    OnRowDataBound="GridView2_RowDataBound" 
    OnRowCommand="GridView2_RowCommand">
<Columns>   

<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" ItemStyle-Height="25px" 
    ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#a6a3a3">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" /> 
    </ItemTemplate>
</asp:TemplateField> 
<asp:BoundField DataField="DescNombreCurso"
    ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" ItemStyle-Height="25px" 
    ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#a6a3a3" />
<asp:BoundField DataField="DescTipoCurso" HeaderText="Tipo" 
    ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px" ItemStyle-Height="25px"  
    ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#a6a3a3" />
     
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-Height="25px" 
    ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#a6a3a3" ItemStyle-BackColor="#f3d569">                                                                                 
    <ItemTemplate>
        <asp:TextBox ID="tbNota1" runat="server" style="width: 40px; height: 16px; text-align: right; margin:0; padding:0;" MaxLength="2" />   
        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="tbNota1" Enabled="True" />   
    </ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-Height="25px" 
    ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#a6a3a3" ItemStyle-BackColor="#f3d569">                                                                                 
    <ItemTemplate>
        <asp:TextBox ID="tbNota2" runat="server" style="width: 40px; height: 16px; text-align: right; margin:0; padding:0;" MaxLength="2" />  
        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="tbNota2" Enabled="True" />   
    </ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-Height="25px" 
    ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#a6a3a3" ItemStyle-BackColor="#f3d569">                                                                                 
    <ItemTemplate>
        <asp:TextBox ID="tbNota3" runat="server" style="width: 40px; height: 16px; text-align: right; margin:0; padding:0;" MaxLength="2" />
        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="tbNota3" Enabled="True" />      
    </ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-Height="25px" 
    ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#a6a3a3" ItemStyle-BackColor="#f3d569">                                                                                 
    <ItemTemplate>
        <asp:TextBox ID="tbNota4" runat="server" style="width: 40px; height: 16px; text-align: right; margin:0; padding:0;" MaxLength="2" /> 
        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="tbNota4" Enabled="True" />     
    </ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-Height="25px" 
    ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#a6a3a3" ItemStyle-BackColor="#e4e4e4">                                                                                 
    <ItemTemplate>
        <asp:TextBox ID="tbNotaFinal" runat="server" style="width: 40px; height: 16px; text-align: right; margin:0; padding:0;" MaxLength="2" /> 
        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="tbNotaFinal" Enabled="True" />     
    </ItemTemplate>
</asp:TemplateField>  

<asp:TemplateField ItemStyle-CssClass="miHiddenStyle" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="0">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblCodigoRegistroNotaAnual" runat="server" Text='<%# Bind("CodigoRegistroNotaAnual") %>' /> 
        <asp:Label ID="lblCodigoRegistroBimestral1" runat="server" Text='<%# Bind("CodigoRegistroBimestral1") %>' /> 
        <asp:Label ID="lblCodigoRegistroBimestral2" runat="server" Text='<%# Bind("CodigoRegistroBimestral2") %>' /> 
        <asp:Label ID="lblCodigoRegistroBimestral3" runat="server" Text='<%# Bind("CodigoRegistroBimestral3") %>' /> 
        <asp:Label ID="lblCodigoRegistroBimestral4" runat="server" Text='<%# Bind("CodigoRegistroBimestral4") %>' /> 
        <asp:Label ID="lblCodigoAsignacionGrupo" runat="server" Text='<%# Bind("CodigoAsignacionGrupo") %>' /> 
        <asp:Label ID="lblCodigoAnioAcademico" runat="server" Text='<%# Bind("CodigoAnioAcademico") %>' /> 
        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' /> 
    </ItemTemplate>
</asp:TemplateField>  
                                                                                
</Columns>                 
</asp:GridView>   
    </td>
</tr>   
</table>     
</div>
            </ContentTemplate> 
        </atk:TabPanel>   
    </atk:TabContainer>    
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

