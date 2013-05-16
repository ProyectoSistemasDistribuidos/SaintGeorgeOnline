<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="SedesColegio.aspx.vb" Inherits="Mantenimientos_Colegio_SedesColegio" title="Página sin título" %>

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

    function abrirPopupParams(url, tipo, tbPadre) {

        var urlaux = url + '?tipo=' + tipo + '&Padre=' + tbPadre;
        window.showModalDialog(urlaux, "#1", "dialogHeight: 485px ; dialogWidth: 759px; center: Yes; help: No; resizable: No; status: No; scroll: No");

    }

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }
      
</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional"  >
    
     <Triggers>
        <asp:PostBackTrigger ControlID="TabContainer1$miTab1$btnExportar" />
    </Triggers>
    
    <ContentTemplate>
    
 <div id="miContainerMantenimiento">
    <atk:TabContainer ID="TabContainer1" runat="server" Width="881px" ActiveTabIndex="1"
        AutoPostBack="false" ScrollBars="None" >
        <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Busqueda" />
            </HeaderTemplate>
            <ContentTemplate> 
                <div style="border: solid 0px blue; width: 650px;">
                    <div id="miBusquedaActualizacion_Ficha"><!-- 650px -->
                        <fieldset>
                            <legend>Criterios de busqueda</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red;
                                min-width: 800px;">
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Nombre Sede</span>
                                    </td>
                                    <td style="width: 550px; height: 25px; padding-left:10px" align="left" valign="middle">
                                        <asp:TextBox ID="tbBuscarDescripcion" runat="server" CssClass="miTextBox" Width="400px" MaxLength="100" />
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                        <atk:FilteredTextBoxExtender ID="ftbBuscarDescripcion" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="tbBuscarDescripcion">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="width: 100px; padding-top:6px" align="right" valign="top" rowspan="2">
                                        
                                                <asp:ImageButton ID="btnBuscar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                    onclick="btnBuscar_Click" ToolTip="Buscar Registros"/><br /><br />
                                                <asp:ImageButton ID="btnLimpiar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                    onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/>     
                                            
                                    </td>
                                </tr>
                              
                            </table>
                        </fieldset>
                    </div>
                    <div class="miEspacio">
                    </div>                    
                    <div id="misRegistrosEncontrados" style="width: 840px;">
                        <fieldset style="width: 840px;">
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 820px;">
                                <tr>
                                    <td style="width: 100px; height: 21px;" align="left" valign="middle">
                                        <asp:ImageButton ID="btnExportar" runat="server" Width="84px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                            ToolTip="Exportar"
                                            OnClick="btnExportar_Click" />
                                    </td>
                                    <td style="width: 610px; height: 21px;" align="left" valign="bottom">                                  
                                    <asp:RadioButtonList ID="rbExportar" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Text="Word"/>
                                        <asp:ListItem Value="1" Text="Excel" Selected="True"/>
                                        <asp:ListItem Value="2" Text="Pdf"/>
                                        <asp:ListItem Value="3" Text="Html"/>
                                    </asp:RadioButtonList>                                    
                                </td>                                
                                <td style="width: 100px; height: 21px;" align="right" valign="middle">
                                    <asp:ImageButton ID="btnNuevo" runat="server" Width="74px" Height="19px" 
                                        ImageUrl="~/App_Themes/Imagenes/btnNuevo_1.png"
                                        onmouseover="this.src = '../App_Themes/Imagenes/btnNuevo_2.png'" 
                                        onmouseout="this.src = '../App_Themes/Imagenes/btnNuevo_1.png'" 
                                        onclick="btnNuevo_Click" 
                                        ToolTip="Nuevo Registro"/>
                                 </td>                                                                     
                                </tr>
                            </table>
                           
                        </fieldset>                    
                    </div>                    
                    <div class="miEspacio">
                    </div>
                   <div id="miGridviewMantActualizacion_Ficha">
                        <asp:GridView ID="GridView1" runat="server" 
                            Width="840" 
                            CssClass="miGridviewBusqueda" 
                            GridLines="None" 
                            AutoGenerateColumns="False"
                            AllowPaging="True" 
                            AllowSorting="True"
                            EmptyDataText=" - No se encontraron resultados - "
                            OnPageIndexChanging="GridView1_PageIndexChanging" 
                            OnRowDataBound="GridView1_RowDataBound"
                            OnRowCommand="GridView1_RowCommand"
                            OnRowCreated="GridView1_RowCreated"
                            OnSorting="GridView1_Sorting">
                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" 
                                ForeColor="White" HorizontalAlign="Center" />
                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
                            <Columns>                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                                            CommandName="Actualizar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Actualizar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                            CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Eliminar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnActivar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_activar.png" 
                                            CommandName="Activar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Activar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Nombre Sede">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:80px;" align="right" valign="middle">Nombre Sede&nbsp;</td>
                                            <td style="width:20px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="NombreSede"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("NombreSede") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="NombreColegio" HeaderText="Nombre Colegio" >
                                    <HeaderStyle HorizontalAlign="Center" Width="160px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="160px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="NombrePersonaDirectorGeneral" HeaderText="Director General" >
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="120px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="NombrePersonaDirectorNacional" HeaderText="Director Nacional" >
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="120px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="NombreUgel" HeaderText="Nombre de UGEL" >
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="80px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="NumeroResolucion" HeaderText="Número de Resolución" >
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="80px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="NombrePersonaResponsableMatricula" HeaderText="Responsable de Matrícula" >
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="120px" />
                                </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="Estado" >
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle"/>
                                    <ItemStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle" />
                                </asp:BoundField>
                            </Columns>
                            <PagerTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 628px;">
                                    <tr>                                        
                                        <td style="height: 20px; width: 200px;" align="left" valign="middle">
                                            <span class="miFooterMantLabelLeft">Ir a página   </span>                                         
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                CssClass="letranormal" 
                                                AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;
                                            de
                                            <asp:Label ID="lblNumPaginas" runat="server" />                                         
                                        </td>                                        
                                        <td style="height: 20px; width: 228px;" align="center" valign="middle">                                           
                                            <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Primera Pagina" CommandArgument="First"
                                                CssClass="pagfirst" />
                                            <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Página anterior"
                                                CommandArgument="Prev" CssClass="pagprev" />
                                            <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Página siguiente"
                                                CommandArgument="Next" CssClass="pagnext" />
                                            <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Última Pagina" CommandArgument="Last"
                                                CssClass="paglast" />
                                        </td>                                        
                                        <td style="height: 20px; width: 200px;" align="right" valign="middle">
                                            <asp:Label ID="lblRegistrosActuales" runat="server" CssClass="miFooterMantLabelRight" />
                                        </td>                                        
                                    </tr>
                                </table>
                            </PagerTemplate>
                        </asp:GridView>
                    </div>
                    <div class="miEspacio">
                    </div>
                    <div id="GVLegenda" >
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 840px;">
                            <tr>
                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                    <img alt="Actualizar Registro" src="../App_Themes/Imagenes/opc_actualizar.png"/></td>
                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                    <span>Actualizar Registro</span></td>                              
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <span>&#124;</span></td>      
                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                    <img alt="Eliminar Registro" src="../App_Themes/Imagenes/opc_eliminar.png"/></td>
                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                    <span>Eliminar Registro</span></td>  
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <span>&#124;</span></td>                                    
                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                    <img alt="Activar Registro" src="../App_Themes/Imagenes/opc_activar.png"/></td>
                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                    <span>Activar Registro</span></td>                                      
                                <td style="width: 200px"></td>                                                                     
                            </tr>                        
                        </table>
                    </div>   
                </div>
            </ContentTemplate>
        </atk:TabPanel>  
        <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="false">
            <HeaderTemplate>
                 <asp:Label ID="lbTab2" runat="server" Text="Inserción" />
            </HeaderTemplate>
            <ContentTemplate>
                <div style="border: solid 0px blue; width: 650px;">
                    <div id="miDetalleMant">
                        <fieldset>
                            <legend>Datos del Registro</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 610px;">                                
                                <tr>
                                    <td colspan="2" style="height: 15px;" align="right">
                                        <em>Campos Obligatorios (*)</em>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Nombre Sede :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <asp:HiddenField ID="hd_Codigo" runat="server" />                                                                        
                                        <atk:FilteredTextBoxExtender ID="ftbNombreSede" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="tbNombreSede">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbNombreSede" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Colegio :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:DropDownList  ID="ddlColegio" runat="server" Width="255px">
                                        </asp:DropDownList>   
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Departamento :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:DropDownList ID="ddlDepartamento" runat="server" Width="255px"
                                            OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Provincia :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:DropDownList ID="ddlProvincia" runat="server" Width="255px"
                                            OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Distrito :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:DropDownList ID="ddlDistrito" runat="server" Width="255px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Responsable de Matrícula :&nbsp;</span><span class="camposObligatorios">(*)</span>           
                                        <asp:HiddenField ID="hidenCodigoPersonaResponsableMatricula" runat="server" />                               
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <table style="width:460px;" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td align="left" valign="middle" style="width:250px;">
                                                    <asp:TextBox ID="tbPersonaResponsableMatricula" runat="server" 
                                                        CssClass="miTextBox" Width="250px" MaxLength="100" Enabled="False" />                                                
                                                </td>
                                                <td align="left" valign="middle" style="width:250px;">
                                                    <asp:ImageButton ID="btnBuscarResponsableMatricula" runat="server" Width="74px" Height="19px"
                                                        ImageUrl="~/App_Themes/Imagenes/btnBuscarPersona_1.png"                                                       
                                                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_2.png'"
                                                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_1.png'" 
                                                        Tooltip="Buscar Responsable de Matrícula"
                                                        OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx','2','matricula');" />                                                
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Director General :&nbsp;</span><span class="camposObligatorios">(*)</span>           
                                        <asp:HiddenField ID="hidenCodigoPersonaDirectorGeneral" runat="server" />                               
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <table style="width:460px;" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td align="left" valign="middle" style="width:250px;">
                                                    <asp:TextBox ID="tbPersonaDirectorGeneral" runat="server" 
                                                        CssClass="miTextBox" Width="250px" MaxLength="100" Enabled="False" />                                                
                                                </td>
                                                <td align="left" valign="middle" style="width:250px;">
                                                    <asp:ImageButton ID="btnBuscarDirectorGeneral" runat="server" Width="74px" Height="19px"
                                                        ImageUrl="~/App_Themes/Imagenes/btnBuscarPersona_1.png"                        
                                                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_2.png'"
                                                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_1.png'" 
                                                        Tooltip="Buscar Nombre de Director General"
                                                        OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx','2','directorGeneral');" />                                                
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                </tr>       
                                
                                    <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Director Nacional:&nbsp;</span><span class="camposObligatorios">(*)</span>           
                                        <asp:HiddenField ID="hidenCodigoPersonaDirectorNacional" runat="server" />                               
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <table style="width:460px;" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td align="left" valign="middle" style="width:250px;">
                                                    <asp:TextBox ID="tbPersonaDirectorNacional" runat="server" 
                                                        CssClass="miTextBox" Width="250px" MaxLength="100" Enabled="False" />                                                
                                                </td>
                                                <td align="left" valign="middle" style="width:250px;">
                                                    <asp:ImageButton ID="btnBuscarDirectorNacional" runat="server" Width="74px" Height="19px"
                                                        ImageUrl="~/App_Themes/Imagenes/btnBuscarPersona_1.png"                        
                                                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_2.png'"
                                                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_1.png'" 
                                                        Tooltip="Buscar Nombre de Director Nacional"
                                                        OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx','2','directorNacional');" />                                                
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                </tr> 
                                
                                 <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Sub Director :&nbsp;</span>           
                                        <asp:HiddenField ID="hidenCodigoPersonaSubDirector" runat="server" />                               
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <table style="width:460px;" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td align="left" valign="middle" style="width:250px;">
                                                    <asp:TextBox ID="tbPersonaSubDirector" runat="server" 
                                                        CssClass="miTextBox" Width="250px" MaxLength="100" Enabled="False" />                                                
                                                </td>
                                                <td align="left" valign="middle" style="width:250px;">
                                                    <asp:ImageButton ID="btnBuscarSubDirector" runat="server" Width="74px" Height="19px"
                                                        ImageUrl="~/App_Themes/Imagenes/btnBuscarPersona_1.png"                        
                                                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_2.png'"
                                                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_1.png'" 
                                                        Tooltip="Buscar Nombre de Sub Director"
                                                        OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx','2','subdirector');" />                                                
                                                     <asp:ImageButton ID="btnEliminarSubDirector" runat="server" Width="74px" Height="19px"
                                                        ImageUrl="~/App_Themes/Imagenes/btnEliminar_1.png"                        
                                                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnEliminar_2.png'"
                                                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnEliminar_1.png'" 
                                                        Tooltip="Eliminar Nombre de Persona Sub Director"
                                                        />                                                
                                                 </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                </tr>     
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Dirección :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <atk:FilteredTextBoxExtender ID="ftbDireccion" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="tbDireccion">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbDireccion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="200"/>
                                    </td>
                                </tr>  
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Nombre Ugel&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <atk:FilteredTextBoxExtender ID="ftbNombreUgel" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="tbNombreUgel">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbNombreUgel" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100"/>
                                    </td>
                                </tr>  
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Codigo Ugel :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <atk:FilteredTextBoxExtender ID="ftbCodigoUgel" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="tbCodigoUgel">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbCodigoUgel" runat="server" CssClass="miTextBox" Width="250px" MaxLength="6"/>
                                    </td>
                                </tr> 
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Número Ugel :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <atk:FilteredTextBoxExtender ID="ftbNumeroUgel" runat="server" 
                                            FilterType="Numbers" TargetControlID="tbNumeroUgel">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbNumeroUgel" runat="server" CssClass="miTextBox" Width="250px" MaxLength="10"/>
                                    </td>
                                </tr>  
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Urbanización :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <atk:FilteredTextBoxExtender ID="ftbUrbanizacion" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="tbUrbanizacion">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbUrbanizacion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100"/>
                                    </td>
                                </tr>   
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Número Resolución :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <atk:FilteredTextBoxExtender ID="ftbNumeroResolucion" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="tbNumeroResolucion">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbNumeroResolucion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="10"/>
                                    </td>
                                </tr>   
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Gestion :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                    </td>
                                     <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <table style="width:460px;" cellpadding="0" cellspacing="0" border="0">
                                           <tr>
                                                <td style="min-width: 270px; height: 25px;" align="left" valign="bottom">
                                                    <asp:TextBox ID="tbGestion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100"/>
                                                    <atk:FilteredTextBoxExtender ID="ftbGestion" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" 
                                                        TargetControlID="tbGestion">
                                                    </atk:FilteredTextBoxExtender>
                                                </td>
                                                <td style="min-width: 90px; height: 25px;" align="left" valign="middle">
                                                     <span> Abreviatura :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                    <atk:FilteredTextBoxExtender ID="ftbGestionAbrv" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbGestionAbrv">
                                                    </atk:FilteredTextBoxExtender>
                                                 </td>
                                                 <td style="min-width: 100px; height: 25px;" align="left" valign="bottom">
                                                    <asp:TextBox ID="tbGestionAbrv" runat="server" CssClass="miTextBox" Width="50px" MaxLength="10"/>
                                                </td>
                                            </tr>
                                        </table>  
                                     </td> 
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Forma :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <table style="width:460px;" cellpadding="0" cellspacing="0" border="0">
                                           <tr>
                                                <td style="min-width: 270px; height: 25px;" align="left" valign="bottom">
                                                    <asp:TextBox ID="tbForma" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100"/>
                                                    <atk:FilteredTextBoxExtender ID="ftbForma" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbForma">
                                                    </atk:FilteredTextBoxExtender>
                                                </td>
                                                <td style="min-width: 90px; height: 25px;" align="left" valign="middle">
                                                     <span> Abreviatura :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                    <atk:FilteredTextBoxExtender ID="ftbFormaAbrv" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbFormaAbrv">
                                                    </atk:FilteredTextBoxExtender>
                                                 </td>
                                                 <td style="min-width: 100px; height: 25px;" align="left" valign="bottom">
                                                    <asp:TextBox ID="tbFormaAbrv" runat="server" CssClass="miTextBox" Width="50px" MaxLength="10"/>
                                                </td>
                                            </tr>
                                        </table>  
                                     </td> 
                                </tr>   
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Modalidad :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <table style="width:460px;" cellpadding="0" cellspacing="0" border="0">
                                           <tr>
                                                <td style="min-width: 270px; height: 25px;" align="left" valign="bottom">
                                                    <asp:TextBox ID="tbModalidad" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100"/>
                                                    <atk:FilteredTextBoxExtender ID="ftbModalidad" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbModalidad">
                                                    </atk:FilteredTextBoxExtender>
                                                </td>
                                                <td style="min-width: 90px; height: 25px;" align="left" valign="middle">
                                                     <span> Abreviatura :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                    <atk:FilteredTextBoxExtender ID="ftbModalidadAbrv" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbModalidadAbrv">
                                                    </atk:FilteredTextBoxExtender>
                                                 </td>
                                                 <td style="min-width: 100px; height: 25px;" align="left" valign="bottom">
                                                    <asp:TextBox ID="tbModalidadAbrv" runat="server" CssClass="miTextBox" Width="50px" MaxLength="10"/>
                                                </td>
                                            </tr>
                                        </table>  
                                     </td> 
                                </tr>                                                  
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Programa :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <table style="width:460px;" cellpadding="0" cellspacing="0" border="0">
                                           <tr>
                                                <td style="min-width: 270px; height: 25px;" align="left" valign="bottom">
                                                    <asp:TextBox ID="tbPrograma" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100"/>
                                                    <atk:FilteredTextBoxExtender ID="ftbPrograma" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbPrograma">
                                                    </atk:FilteredTextBoxExtender>
                                                </td>
                                                <td style="min-width: 90px; height: 25px;" align="left" valign="middle">
                                                    <span> Abreviatura :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                    <atk:FilteredTextBoxExtender ID="ftbProgramaAbrv" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbProgramaAbrv">
                                                    </atk:FilteredTextBoxExtender>
                                                 </td>
                                                 <td style="min-width: 100px; height: 25px;" align="left" valign="bottom">
                                                    <asp:TextBox ID="tbProgramaAbrv" runat="server" CssClass="miTextBox" Width="50px" MaxLength="10"/>
                                                </td>
                                            </tr>
                                        </table>  
                                     </td> 
                                </tr> 
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Turno :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <table style="width:460px;" cellpadding="0" cellspacing="0" border="0">
                                           <tr>
                                                <td style="min-width: 270px; height: 25px;" align="left" valign="bottom">
                                                    <asp:TextBox ID="tbTurno" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100"/>
                                                    <atk:FilteredTextBoxExtender ID="ftbTurno" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbTurno">
                                                    </atk:FilteredTextBoxExtender>
                                                </td>
                                                <td style="min-width: 90px; height: 25px;" align="left" valign="middle">
                                                    <span> Abreviatura :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                    <atk:FilteredTextBoxExtender ID="ftbTurnoAbrv" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbTurnoAbrv">
                                                    </atk:FilteredTextBoxExtender>
                                                 </td>
                                                 <td style="min-width: 100px; height: 25px;" align="left" valign="bottom">
                                                    <asp:TextBox ID="tbTurnoAbrv" runat="server" CssClass="miTextBox" Width="50px" MaxLength="10"/>
                                                </td>
                                            </tr>
                                        </table>  
                                     </td> 
                                </tr> 
                            </table>
                        </fieldset>
                    </div>    
                    <div class="miEspacio"></div>            
                    <div id="miFooterDetalleMant">
                        <asp:ImageButton ID="btnGrabar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                            onclick="btnGrabar_Click" />&nbsp;
                        <asp:ImageButton ID="btnCancelar" runat="server" Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                            onclick="btnCancelar_Click" CausesValidation="False"/>
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
    
    <script type="text/javascript">

    $(document).ready(function() {

        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);
            
    });

</script>

</div>  

</asp:Content>



