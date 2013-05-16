<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="registroEquipos.aspx.vb" Inherits="Modulo_Permisos_registroEquipos" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css"> 
.miboton
{
    text-decoration: none; 
    background-color: #0088cc; /*#2a5e97;*/
    color: #ffffff; /*#23527e;*/ 
    /*font-weight: bold;*/ font-size: 12px;  
    display: block; line-height: 25px; float: left;
    -webkit-border-radius: 5px;
    -moz-border-radius: 5px;
    border-radius: 5px;   
    padding: 0 5px; 
    margin: 0 5px 0 0;
    width: 70px;
    vertical-align: middle; text-align: center;   
    }
.miboton:hover
{
    text-decoration: none; 
    background-color: #0062b8; /*#a1b5cd;*/
    color: #ffffff; /*#23527e;*/ 
    /*background: rgba(0,0,0,.2);*/
    /*font-weight: bold;*/ 
    font-size: 12px; 
    display: block; line-height: 25px; float: left; 
    -webkit-border-radius: 5px;
    -moz-border-radius: 5px;
    border-radius: 5px;   
    padding: 0 5px; 
    margin: 0 5px 0 0;
    width: 70px;
    vertical-align: middle; text-align: center;    
    }   
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miPaginaMantenimiento" style="margin-left: 10px;">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">  
    <ContentTemplate>    

<div style="border: solid 0px blue; width: 840px; font-family: Arial, Helvetica, sans-serif; font-size: 10px;">
<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 840px;">
<tr><td colspan="4" style="height: 10px;"></td></tr>

<tr>
    <td style="width: 100px; height: 25px;" align="left" valign="middle">
<span>Tipo Persona:</span>
    </td>
    <td style="width: 200px; height: 25px;" align="left" valign="middle">   
<asp:DropDownList ID="ddlTipoPersona" runat="server" style="width: 190px; font-size: 10px; font-family: Arial;">
    <asp:ListItem Text="Alumnos" Value="1" Selected="True"></asp:ListItem>
    <asp:ListItem Text="Trabajadores" Value="2"></asp:ListItem>
</asp:DropDownList>
    </td>
    <td style="width: 200px; height: 25px;" align="left" valign="middle">       
<asp:LinkButton ID="btnBuscar" runat="server" Text="Buscar" class="miboton" 
    OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx');" />    

&nbsp;

<asp:LinkButton ID="btnGrabar" runat="server" Text="Grabar" class="miboton" OnClick="btnGrabar_Click" />       
    
    </td>
    <td style="width: 340px; height: 25px;" align="left" valign="middle">   
    </td> 
</tr>

<tr>
    <td style="width: 100px; height: 25px;" align="left" valign="middle">
<span>Persona:</span>
    </td>
    <td style="width: 400px; height: 25px;" align="left" valign="middle" colspan="2"> 
<asp:HiddenField ID="hiddenCodigoRegistro" runat="server" value="0" />  
<asp:HiddenField ID="hiddenCodigoPersona" runat="server" value="0" />
<asp:HiddenField ID="hiddenCodigoTipoPersona" runat="server" value="0" />
<asp:TextBox ID="tbNombre" runat="server" style="width:390px; font-size: 10px; font-family: Arial;" Enabled="False" />
    </td>
    <td style="width: 340px; height: 25px;" align="left" valign="middle">   
    </td> 
</tr>

<tr><td colspan="4" style="height: 10px; border-bottom: solid 2px #000000"></td></tr>

<tr>
    <td colspan="4" align="center" valign="top">
          
    </td>
</tr>
<tr>
    <td style="width: 500px; height: 25px;" colspan="3" align="left" valign="top">
    
    <div class="contenido" style="margin: 0; padding: 0; border-left: solid 1px #a6a3a3; border-right: solid 1px #a6a3a3; border-bottom: solid 1px #a6a3a3; width:400px; height: auto;">   
<asp:GridView ID="GridView1" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="400px"
            GridLines="None" 
            AutoGenerateColumns="False"                   
            OnRowDataBound="GridView1_RowDataBound" 
            OnRowCommand="GridView1_RowCommand"           
            OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating"            
            OnPreRender="GridView1_PreRender">
            <HeaderStyle  BackColor="#000000" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Height="26px" />                       
            <Columns>       
                                    
    <asp:CommandField ButtonType="Image" ShowEditButton="True"
        EditImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
        CancelImageUrl="~/App_Themes/Imagenes/opc_cancelar.png" 
        UpdateImageUrl="~/App_Themes/Imagenes/opc_activar.png">
        <HeaderStyle HorizontalAlign="Center" Width="40px" />
        <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="40px" />
    </asp:CommandField>
    
    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
        <ItemTemplate>
            <asp:ImageButton ID="btnAgregarFila" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_agregar.png" CommandName="Agregar" ToolTip="Agregar Registro" Height="16px" Width="16px" />
            </ItemTemplate>
    </asp:TemplateField>    
                                   
    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
        <ItemTemplate>
            <asp:ImageButton ID="btnEliminarFila" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" CommandName="Eliminar" ToolTip="Eliminar Registro" Height="16px" Width="16px" />
            </ItemTemplate>
    </asp:TemplateField>                                       
                             
    <asp:TemplateField HeaderText="Tipo Equipo" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px">
        <ItemTemplate>
            <asp:DropDownList ID="ddlTipoDispositivo" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;" Enabled="false">
                <asp:ListItem Text="--Seleccione--" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Laptop" Value="1"></asp:ListItem>
                <asp:ListItem Text="Ipad" Value="2"></asp:ListItem>
            </asp:DropDownList>  
        </ItemTemplate>
    </asp:TemplateField>                 
                                  
    <asp:TemplateField HeaderText="Direccion MAC" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
        <ItemTemplate>
            <asp:Label ID="lblDireccionMAC" runat="server" Text='<%# Bind("DireccionMAC") %>' style="width: 190px;"/> 
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="tbDireccionMAC" runat="server" Text='<%# Bind("DireccionMAC") %>' style="width: 190px; text-align: left; font-size: 8pt; font-family: Arial;" MaxLength="50"/>
        </EditItemTemplate>
    </asp:TemplateField>
            
    <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle" ItemStyle-Width="0px">
        <ItemTemplate>
            <asp:Label ID="lblcRegistro" runat="server" Text='<%# Bind("cRegistro") %>' />
            <asp:Label ID="lblcDetalle" runat="server" Text='<%# Bind("cDetalle") %>' /> 
            <asp:Label ID="lblcTipoDispositivo" runat="server" Text='<%# Bind("cTipoDispositivo") %>' />
        </ItemTemplate>
    </asp:TemplateField>     
            </Columns>
        </asp:GridView>        
    </div>
    </td>
    <td style="width: 340px; height: 25px;" align="left" valign="top">
<asp:ImageButton ID="btnAgregar" runat="server" Width="84px" Height="19px" 
            ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png"
            onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'" 
            onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" 
            onclick="btnAgregar_Click" ToolTip="Agregar Registro"/>  
     &nbsp;
<asp:ImageButton ID="btnEliminar" runat="server" Width="84px" Height="19px" 
            ImageUrl="~/App_Themes/Imagenes/btnEliminar_1.png"
            onmouseover="this.src = '../App_Themes/Imagenes/btnEliminar_2.png'" 
            onmouseout="this.src = '../App_Themes/Imagenes/btnEliminar_1.png'" 
            onclick="btnEliminar_Click" ToolTip="Eliminar Todo"/>      
    </td>
</tr>




</table>     
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

    function abrirPopupParams(url) {
        var tipo = parseInt($("#<%= ddlTipoPersona.ClientID %> option:selected").val())        
        var tbPadre='Padre';
        var urlaux = url + '?tipo=' + tipo + '&Padre=' + tbPadre;
        window.showModalDialog(urlaux, "#1", "dialogHeight: 485px ; dialogWidth: 759px; center: Yes; help: No; resizable: No; status: No; scroll: No");       
    }

</script>

</asp:Content>

