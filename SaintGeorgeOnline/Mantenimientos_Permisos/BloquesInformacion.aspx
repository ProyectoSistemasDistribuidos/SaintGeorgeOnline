<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="BloquesInformacion.aspx.vb" Inherits="Mantenimientos_Permisos_BloquesInformacion" title="Página sin título" %>

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
    <atk:TabContainer ID="TabContainer1" runat="server" Width="670px" ActiveTabIndex="1"
        AutoPostBack="false" ScrollBars="None" >
        <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Busqueda" />
            </HeaderTemplate>
            <ContentTemplate> 
                <div style="border: solid 0px blue; width: 650px;">
                    <div id="miBusquedaMant"><!-- 650px -->
                        <fieldset>
                            <legend>Criterios de busqueda</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red;
                                min-width: 610px;">
                                <tr>
                                    <td style="width: 100px; height: 25px;" align="left" valign="bottom">
                                        <span>Descripción</span>
                                    </td>
                                    <td style="width: 410px; height: 25px; padding-left:10px" align="left" valign="bottom">
                                        <asp:TextBox ID="tbBuscarDescripcion" runat="server" CssClass="miTextBox" Width="400px" MaxLength="100" />
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbBuscarDescripcion" 
                                            ValidChars="' ','.','á','é','í','ó','ú','(',')'" Enabled="True">
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
                                <tr>
                                    <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                        <span>Estado</span>
                                    </td>
                                    <td style="min-width: 410px; height: 25px;" align="left" valign="bottom">
                                        <asp:RadioButtonList ID="rbEstados" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="-1" Text="Todos" />
                                            <asp:ListItem Value="1" Text="Activo" Selected="True" />
                                            <asp:ListItem Value="0" Text="Inactivo" />                                        
                                        </asp:RadioButtonList>                                        
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <div class="miEspacio">
                    </div>                    
                    <div id="misRegistrosEncontrados">
                        <fieldset>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 610px;">
                                <tr>
                                    <td style="width: 100px; height: 21px;" align="left" valign="middle">
                                        <asp:ImageButton ID="btnExportar" runat="server" Width="84px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                            ToolTip="Exportar"
                                            OnClick="btnExportar_Click" />
                                    </td>
                                    <td style="width: 410px; height: 21px;" align="left" valign="bottom">                                  
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
                    <div id="miGridviewMant">
                        <asp:GridView ID="GridView1" runat="server" 
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
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Nombre">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:140px;" align="right" valign="middle">Descripción&nbsp;</td>
                                            <td style="width:100px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Descripcion"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Descripcion") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="240px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="240px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" >
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="150px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="Entidad" HeaderText="Entidad" >
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="150px" />
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
                    <div id="GVLegenda">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 630px;">
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
                                    <td style="width: 150px; height: 50px;" align="left" valign="middle">
                                        <span>Descripción :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <asp:HiddenField ID="hd_Codigo" runat="server" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbDescripcion" 
                                            ValidChars="' ','.','á','é','í','ó','ú','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 50px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbDescripcion" runat="server" CssClass="miTextBox" Width="450px"
                                            Height="35px" Rows="2" TextMode="MultiLine" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Código Programación :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbCodigoProgramacion" 
                                            ValidChars="' ','.','á','é','í','ó','ú','(',')',':','/','_'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbCodigoProgramacion" runat="server" CssClass="miTextBox" Width="450px" MaxLength="100"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Tipo :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:RadioButtonList ID="rbTipo" runat="server" RepeatDirection="Horizontal">                                         
                                            <asp:ListItem Value="1" Text="Módulo" Selected="True" />
                                            <asp:ListItem Value="2" Text="Grupo" />                                        
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Entidad :&nbsp;</span></td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbEntidad" runat="server" CssClass="miTextBox" Width="450px" MaxLength="100"/>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                                                
                        <fieldset>
                            <legend>Relación de Campos de Información</legend>
                            <table cellpadding="0" cellspacing="0" border="0" width="610px">
                                <tr>
                                    <td colspan="2" height="10px">
                                    
                                        <atk:ModalPopupExtender ID="pnModalCampoInformacion" runat="server" TargetControlID="btnAgregarDetalleCampoInformacion"
                                            PopupControlID="pnlCampoInformacion" 
                                            BackgroundCssClass="MiModalBackground" 
                                            OkControlID="OKCampoInformacion" CancelControlID="CancelCampoInformacion" 
                                            Drag="True" PopupDragHandleControlID="CampoInformacionHeader" 
                                            DynamicServicePath="" Enabled="True" />
                                        <asp:Panel ID="pnlCampoInformacion" BackColor="White" BorderColor="Black" runat="server">
                                            <table cellpadding="0" cellspacing="0" border="0" width="500px">
                                                <tr>
                                                    <td style="width: 500px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                        <span id="CampoInformacionHeader" style="padding-left: 20px; font-weight: bold; font-size: 11px;
                                                            font-family: Arial; cursor: pointer">Agregar Campo de Información</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" height="10px">
                                                        <asp:HiddenField ID="hfTotalRegsListaCampoInformacion" runat="server" Value="0" />
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="2" style="width: 500px;" align="center" valign="middle">
<div style="border: solid 1px #a6a3a3; width: 500px">
<asp:GridView ID="GVListaCampoInformacion" runat="server" 
    CssClass="miGridviewBusqueda"
    Width="500px" 
    GridLines="None" 
    AutoGenerateColumns="False"
    AllowPaging="True" AllowSorting="True"
    EmptyDataText=" - No se encontraron resultados - "
    OnRowDataBound="GVListaCampoInformacion_RowDataBound"  
    OnPageIndexChanging="GVListaCampoInformacion_PageIndexChanging"                                                                     
    OnRowCreated="GVListaCampoInformacion_RowCreated"
    OnSorting="GVListaCampoInformacion_Sorting">
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
    <Columns>     
        <asp:TemplateField>                                                             
        <ItemTemplate>
            <asp:CheckBox ID="chkSeleccionar" runat="server" OnCheckedChanged="chkSeleccionarListaCampoInformacion_CheckedChanged" AutoPostBack="true" />
        </ItemTemplate>                                   
            <ItemStyle CssClass="miGridviewBusqueda_Rows" Width="50px" />
        </asp:TemplateField>                                                                
        <asp:BoundField DataField="Codigo" HeaderText="Código" >
        
            <HeaderStyle CssClass="miHiddenStyle" />
            <ItemStyle CssClass="miHiddenStyle" />
        </asp:BoundField>
        
        <asp:TemplateField HeaderText="Nombre">  
            <HeaderTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="width:150px;" align="right" valign="middle">Descripción&nbsp;</td>
                <td style="width:100px;" align="left" valign="middle">
                    <asp:ImageButton ID="btnSorting" runat="server" ToolTip="Descendente"    
                        ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                        CommandName="Sort" 
                        CommandArgument="Descripcion"/></td>
            </tr>
            </table>                                    
            </HeaderTemplate>                                                                      
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Descripcion") %>' />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="250px"/>
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="250px" />
        </asp:TemplateField>
        <asp:BoundField DataField="CampoBD" HeaderText="Descripcion BD" >                     
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" 
                Width="200px" />
        </asp:BoundField>
    </Columns>
    <PagerTemplate>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 500px;">
            <tr>
                <td style="height: 20px; width: 159px;" align="left" valign="middle">
                    <span class="miFooterMantLabelLeft">Ir a página </span>
                    <asp:DropDownList ID="ddlPageSelectorListaCampoInformacion" runat="server" CssClass="letranormal" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPageSelectorListaCampoInformacion_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp; de
                    <asp:Label ID="lblNumPaginas_ListaCampoInformacion" runat="server" />
                </td>
                <td style="height: 20px; width: 192px;" align="center" valign="middle">
                    <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Primera Pagina"
                        CommandArgument="First" CssClass="pagfirst" />
                    <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Página anterior"
                        CommandArgument="Prev" CssClass="pagprev" />
                    <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Página siguiente"
                        CommandArgument="Next" CssClass="pagnext" />
                    <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Última Pagina"
                        CommandArgument="Last" CssClass="paglast" />
                </td>
                <td style="height: 20px; width: 149px;" align="right" valign="middle">
                    <asp:Label ID="lblRegistrosActuales_ListaCampoInformacion" runat="server" CssClass="miFooterMantLabelRight" />
                </td>
            </tr>
        </table>
    </PagerTemplate>
</asp:GridView>
</div>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="2" height="10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 460px; height: 25px" align="center" valign="middle" colspan="2">
                                                        <asp:ImageButton ID="btnModalAceptarCampoInformacion" runat="server" 
                                                            Width="84px" Height="19px"
                                                            ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="btnModalAceptarCampoInformacion_Click"
                                                            ToolTip="Aceptar" />&nbsp;
                                                        <asp:ImageButton ID="btnModalCancelarCampoInformacion" runat="server" 
                                                            Width="84px" Height="19px"
                                                            ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="btnModalCancelarCampoInformacion_Click"
                                                            ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" height="10px">
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="controlCampoInformacion" style="display: none">
                                                <input type="button" id="OKCampoInformacion" />
                                                <input type="button" id="CancelCampoInformacion" />
                                                <asp:Button ID="btnAgregarDetalleCampoInformacion" runat="server" />      
                                            </div>
                                        </asp:Panel>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 610px;" align="center" valign="top" colspan="2">
                                        <table cellpadding="0" cellspacing="0" border="0" width="610px">
                                            <tr>
                                                <td style="width: 610px; height: 26px;" align="right">
                                                    <asp:ImageButton ID="btnAgregarCampoInformacion" runat="server" Width="84px" Height="19px"
                                                        ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" 
                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'" 
                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" 
                                                        OnClick="btnAgregarCampoInformacion_Click"
                                                        ToolTip="Agregar" />
                                                        <asp:HiddenField ID="hfTotalRegsDetalleCampoInformacion" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 610px; height: 25px" align="center" valign="top" colspan="3">
                                                
    <asp:UpdatePanel ID="upCampoInformacion" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div id="miGVMantFichaRegitros_Detalle">
    <asp:GridView ID="GVDetalleCampoInformacion" runat="server" CssClass="miGridviewBusqueda"
        Width="610px" GridLines="None" 
        AutoGenerateColumns="False" 
        ShowHeader="true" ShowFooter="false"
        AllowPaging="True" AllowSorting="True"
        OnRowDataBound="GVDetalleCampoInformacion_RowDataBound"
        OnRowCommand="GVDetalleCampoInformacion_RowCommand"
        OnPageIndexChanging="GVDetalleCampoInformacion_PageIndexChanging"                                                                     
        OnRowCreated="GVDetalleCampoInformacion_RowCreated"
        OnSorting="GVDetalleCampoInformacion_Sorting">
        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
        <Columns>                                                                    
        <asp:TemplateField>
            <ItemTemplate>
                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                    CommandName="Eliminar" CommandArgument='<%# Bind("CodigoCampoInformacion") %>' ToolTip="Quitar Registro" />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="30px" />
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
    
        <asp:TemplateField HeaderText="Codigo">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("CodigoCampoInformacion") %>' />
            </ItemTemplate>
            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
        </asp:TemplateField>
                  
        <asp:TemplateField HeaderText="Nombre">  
            <HeaderTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="width:170px;" align="right" valign="middle">Descripción&nbsp;</td>
                <td style="width:130px;" align="left" valign="middle">
                    <asp:ImageButton ID="btnSorting" runat="server" ToolTip="Descendente"    
                        ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                        CommandName="Sort" 
                        CommandArgument="Descripcion"/></td>
            </tr>
            </table>                                    
            </HeaderTemplate>                                                                      
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Descripcion") %>' />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="350px"/>
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="350px" />
        </asp:TemplateField>        
        
        <asp:TemplateField HeaderText="Descripcion BD">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("CampoBD") %>' />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="230px" />
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="230px" />
        </asp:TemplateField>        
    </Columns>
                                                                    
    <PagerTemplate>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 500px;">
            <tr>
                <td style="height: 20px; width: 159px;" align="left" valign="middle">
                    <span class="miFooterMantLabelLeft">Ir a página </span>
                    <asp:DropDownList ID="ddlPageSelectorDetalleCampoInformacion" runat="server" CssClass="letranormal" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPageSelectorDetalleCampoInformacion_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp; de
                    <asp:Label ID="lblNumPaginas_DetalleCampoInformacion" runat="server" />
                </td>
                <td style="height: 20px; width: 192px;" align="center" valign="middle">
                    <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Primera Pagina"
                        CommandArgument="First" CssClass="pagfirst" />
                    <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Página anterior"
                        CommandArgument="Prev" CssClass="pagprev" />
                    <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Página siguiente"
                        CommandArgument="Next" CssClass="pagnext" />
                    <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Última Pagina"
                        CommandArgument="Last" CssClass="paglast" />
                </td>
                <td style="height: 20px; width: 149px;" align="right" valign="middle">
                    <asp:Label ID="lblRegistrosActuales_DetalleCampoInformacion" runat="server" CssClass="miFooterMantLabelRight" />
                </td>
            </tr>
        </table>
    </PagerTemplate>
                                                                </asp:GridView>
                                                            </div>
                                                            <div class="miEspacio">
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    
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



