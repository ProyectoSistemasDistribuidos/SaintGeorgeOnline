<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="situacionFinalAlumno.aspx.vb" Inherits="Modulo_Academico_situacionFinalAlumno" %>


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



<div style="border: solid 0px blue; width: 810px; font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px;">
<asp:UpdatePanel  runat="server">

<ContentTemplate>
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
        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" />
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
                Situacion alumno</td>
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
    AutoGenerateColumns="False"
    ShowHeader="False"
    EmptyDataText=" - No se encontraron resultados - "
    OnRowDataBound="GridView1_RowDataBound"
    OnRowCommand="GridView1_RowCommand">
<Columns>
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" /> 
    </ItemTemplate>
    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
        Width="20px" />
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblPeriodo" runat="server" Text='<%# Bind("Periodo") %>' /> 
    </ItemTemplate>
    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
        Width="40px" />
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblGrado" runat="server" Text='<%# Bind("Grado") %>' /> 
    </ItemTemplate>
    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
        Width="80px" />
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblAula" runat="server" Text='<%# Bind("Aula") %>' /> 
    </ItemTemplate>
    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
        Width="100px" />
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' /> 
    </ItemTemplate>
    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
        Width="80px" />
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="260px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' /> 
    </ItemTemplate>
    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" 
        Width="260px" />
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">                                                                                 
    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
        Width="50px" />
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="163px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblCodigoAula" runat="server" Text='<%# Bind("CodigoAula") %>' style="display: none;" /> 
        <asp:DropDownList ID="cmbSituacionAlumno" runat="server">
        </asp:DropDownList>
    </ItemTemplate>
    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
        Width="163px" />
</asp:TemplateField> 

    <asp:TemplateField HeaderText="codMatricula" Visible="False">
        <ItemTemplate>
            <asp:Label ID="lblCodMraticula" runat="server" 
                Text='<%# Eval("codMatricula") %>'></asp:Label>
                <asp:Label ID="lblCodSituacionMatricula" runat="server" 
                Text='<%# Eval("codSituacionMatricula") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>

</Columns>
</asp:GridView> 
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
</div>
    </td>
</tr>  
</table>     
</ContentTemplate>
</asp:UpdatePanel>


</div>
          
            
    
</div>

<script type="text/javascript">
    $(document).ready(function() {
        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);
    });
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
    function beginReq(sender, args) {
        // muestra el popup
        $.blockUI({
            message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Editando...</h4>'
        });

        // $find(ModalProgress).show();
    } $(document).ready(function() {
        $("#menu").hide('fast');
    });

    function endReq(sender, args) {
        //  esconde el popup
        // $find(ModalProgress).hide();
        $.unblockUI();
    } 
</script>

</asp:Content>
