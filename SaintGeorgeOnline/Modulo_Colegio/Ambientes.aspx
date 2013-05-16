<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="Ambientes.aspx.vb" Inherits="Mantenimientos_Colegio_Ambientes" title="Página sin título" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
               
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
    .style1
    {
        width: 200px;
        height: 25px;
    }
    .style2
    {
        width: 300px;
        height: 25px;
    }
    
    .panelRegistro em{
        font-size: 10px;
        font-family: Arial;
        color: #a51515;
        margin-right: 7px;
        padding: 0;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
     
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
                                    <td style="width: 200px; height: 25px" align="left" valign="middle">
                                        <span style="padding-left:10px">Sede:&nbsp;</span>
                                    </td>
                                      <td style="width: 310px; height: 25px" align="left">
                                      <asp:HiddenField ID="hfTotalRegs" runat="server" Value="1" />
                                        <asp:DropDownList ID="ddlBuscarSede" runat="server" Width="200px">
                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                        </asp:DropDownList>                              
                                    </td>  
                                   <td style="width: 100px; padding-top:6px" align="right" valign="top" rowspan="2">
                                        
                                                <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                    onclick="btnBuscar_Click" ToolTip="Buscar Registros"/><br /><br />
                                                <asp:ImageButton ID="btnLimpiar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                    onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/>     
                                            
                                    </td>     
                                </tr> 
                                
                                   <tr>
                                  <td style="width: 200px; height: 25px" align="left" valign="middle">
                                        <span style="padding-left:10px">Nombre </span>
                                    </td>
                                    <td style="width: 410px; height: 25px" align="left"  valign="middle">
                                        <asp:TextBox ID="tbBuscarNombre" runat="server" border="solid 1px #a6a3a3" height= "20px" Width="200px"></asp:TextBox>
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" 
                                            Enabled="True" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" 
                                            TargetControlID="tbBuscarNombre" 
                                            ValidChars="' ','.','á','é','í','ó','ñ','ú','(',')'">
                                        </atk:FilteredTextBoxExtender>
                                       </td>                 
                                </tr>                                
                                   <tr>
                                    <td style="width: 200px; height: 25px" align="left" valign="middle">
                                        <span style="padding-left:10px">Tipo de Ambiente :&nbsp;</span>
                                    </td>
                                      <td style="width: 410px; height: 25px" align="left"  valign="middle">
                                        
                                        <asp:DropDownList ID="ddlBuscarTipoAmbiente" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>         
                                </tr> 
                                   <tr>
                                    <td style="width:200px; height: 25px" align="left" valign="middle">
                                        <span style="padding-left:10px">Pabellon :&nbsp;</span>
                                    </td>
                                      <td style="width: 410px; height: 25px" align="left"  valign="middle">
                                        
                                        <asp:DropDownList ID="ddlBuscarPabellon" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>         
                                </tr>   
                                   <tr>
                                    <td style="width: 200px; height: 25px" align="left" valign="middle">
                                        <span style="padding-left:10px">Piso :&nbsp;</span>
                                    </td>
                                      <td style="width: 410px; height: 25px" align="left"  valign="middle">
                                         
                                        <asp:DropDownList ID="ddlBuscarPiso" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>         
                                </tr> 
                                 <tr>
                                      <td style="width: 200px; height: 25px" align="left">
                                        <span style="padding-left:10px">¿Es reservable? :&nbsp;</span>
                                    </td>
                                    <td style="width: 410px; height: 25px" align="left" colspan="2" valign="middle">
                                          <asp:RadioButtonList ID="rbBuscarReservaAmbiente" runat="server" RepeatDirection="Horizontal">   
                                           <asp:ListItem Value="-1" Text="Todos"  Selected="True" />
                                            <asp:ListItem Value="0" >No</asp:ListItem>                                                                             
                                            <asp:ListItem Value="1" >Si</asp:ListItem> 
                                          </asp:RadioButtonList>    
                                    </td>
                                </tr>
                                   <tr>
                                      <td style="width: 200px; height: 25px" align="left">
                                        <span style="padding-left:10px">¿Tiene multimedia? :&nbsp;</span>
                                    </td>
                                    <td style="width: 410px; height: 25px" align="left" colspan="2" valign="middle">
                                          <asp:RadioButtonList ID="rbBuscarMultimedia" runat="server" RepeatDirection="Horizontal">   
                                            <asp:ListItem Value="-1" Text="Todos" Selected="True" />
                                            <asp:ListItem Value="0" >No</asp:ListItem>                                                                             
                                            <asp:ListItem Value="1" >Si</asp:ListItem> 
                                          </asp:RadioButtonList>    
                                    </td>
                                </tr>
                              <%--  <tr>
                                    <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                        <span style="padding-left:10px">Estado</span>
                                    </td>
                                    <td style="min-width: 410px; height: 25px;" align="left" valign="bottom">
                                        <asp:RadioButtonList ID="rbEstados" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="-1" Text="Todos" />
                                            <asp:ListItem Value="1" Text="Activo" Selected="True" />
                                            <asp:ListItem Value="0" Text="Inactivo" />                                        
                                        </asp:RadioButtonList>                                        
                                    </td>
                                </tr>--%>
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
                                
                                <asp:TemplateField HeaderText="NombreAmbiente">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:250px;" align="right" valign="middle">Nombre&nbsp;</td>
                                            <td style="width:50px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreAmbiente" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="NombreAmbiente"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombreAmbiente" runat="server" Text='<%# Bind("NombreAmbiente") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="300px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="300px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="TipoAmbiente">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:150px;" align="right" valign="middle">TipoAmbiente&nbsp;</td>
                                            <td style="width:25px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_TipoAmbiente" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="TipoAmbiente"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoAmbiente" runat="server" Text='<%# Bind("TipoAmbiente") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="175px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="175px" />
                                </asp:TemplateField>
                                                              
                                
                                <asp:TemplateField HeaderText="R">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:25px;" align="center" valign="middle">R&nbsp;</td>
                                            <td style="width:25px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Reservable" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Reservable"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblReservable" runat="server" Text='<%# Bind("Reservable") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="M">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:25px;" align="right" valign="middle">M&nbsp;</td>
                                            <td style="width:25px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Multimedia" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Multimedia"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblM" runat="server" Text='<%# Bind("Multimedia") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Bloque">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:175px;" align="right" valign="middle">Bloque&nbsp;</td>
                                            <td style="width:25px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Bloque" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Bloque"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblBloque" runat="server" Text='<%# Bind("Bloque") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="200px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="200px" />
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Piso">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:250px;" align="right" valign="middle">Piso&nbsp;</td>
                                            <td style="width:25px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Piso" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Piso"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblPiso" runat="server" Text='<%# Bind("Piso") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="275px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="275px" />
                                </asp:TemplateField>
                                                                                                 
                                  <asp:TemplateField HeaderText="Sede">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:175px;" align="right" valign="middle">Sede &nbsp;</td>
                                            <td style="width:25px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Sede" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Sede"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblSede" runat="server" Text='<%# Bind("Sede") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="200px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="200px" />
                                </asp:TemplateField>
                                
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
                                            </asp:DropDownList>&nbsp; de
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
                                    <span>|</span></td>      
                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                    <img alt="Eliminar Registro" src="../App_Themes/Imagenes/opc_eliminar.png"/></td>
                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                    <span>Eliminar Registro</span></td>  
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <span>|</span></td> 
                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                    <span>R: Reservable</span></td>   
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <span>|</span></td> 
                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                    <span>M: Multimedia</span></td>                                          
                                <td style="width: 100px"></td>                                                                     
                            </tr>                        
                        </table>
                    </div>   
                    
                    <atk:ModalPopupExtender ID="pnModalAmbientes" runat="server"
                    TargetControlID="VerAmbiente"
                    PopupControlID="pnlAmbiente"
                    BackgroundCssClass="MiModalBackground" 
                    DropShadow="true" 
                    OkControlID="OKAmbiente" 
                    CancelControlID="CancelAmbiente"
                    Drag="true" PopupDragHandleControlID="PrecedenciaHeader" />  
        
                    <asp:Panel id="pnlAmbiente" 
                               BackColor="White" 
                               BorderColor="Black" 
                               BorderWidth="1px" 
                               runat="server" 
                               style="width: 550px; display: none;">
                    
                    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px #c6c6c6; width: 550px;" class="panelRegistro">
                    <tr>
                        <td style="width: 30px; height: 25px;" class="miGVBusquedaFicha_Header_V2">&nbsp;</td>  
                        <td style="width: 500px; height: 26px; cursor: pointer;" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="2" id="PrecedenciaHeader">                
                            <span style="font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Registro de Ambiente</span>
                        </td>
                        <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                            <asp:ImageButton ID="btnCerrarPanelPrecedencia" runat="server" Width="16" Height="15"
                                ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                                onclick="btnCerrarPanelAmbiente_Click" ToolTip="Cerrar Panel"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left:10px;padding-right:10px  " colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 500px;">                                
                                <tr>
                                    <td colspan="2" style="height: 15px;" align="right">
                                        <em>Campos Obligatorios (*)</em>
                                    </td>
                                </tr>
                                
                                <tr>
                                  <td style="width: 300px; height: 25px" align="left" valign="middle">
                                      <span>Código Ambiente:&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                  </td>
                                  <td style="width: 330px;height: 25px;" align="left">
                                      <asp:TextBox ID="tbCodigoAmbienteAlfanumerico" CssClass="miTextBox" runat="server" MaxLength="4"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td style="width: 300px; height: 25px" align="left" valign="middle">
                                            <span>Sede:&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                  </td>
                                  <td style="width: 330px;height: 25px;" align="left">
                                        <asp:DropDownList ID="ddlSede" runat="server" Width="200px">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hd_Codigo" runat="server" />
                                  </td>                                
                                </tr>
                                <tr>
                                  <td style="width: 300px; height: 25px;" align="left" valign="middle">
                                        <span>Nombre :&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                    </td>
                                  <td style="width: 330px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbNombre" runat="server" CssClass="miTextBox" Width="300px"   Height="35px" Rows="2" TextMode="MultiLine" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbNombre" 
                                            ValidChars="' ','.','á','é','í','ó','ñ','ú','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                  </td>                 
                                </tr>  
                                 <tr>
                                  <td style="width: 300px; height: 25px;" align="left" valign="middle">
                                        <span>Tipo de Ambiente Actual:&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                  </td>
                                  <td style="width: 330px; height: 25px;" align="left"  valign="middle">
                                        
                                        <asp:DropDownList ID="ddlTipoAmbiente" runat="server" Width="200px">
                                        </asp:DropDownList>
                                  </td>         
                                </tr> 
                                <tr>
                                  <td style="width: 300px; height: 25px;" align="left" valign="middle">
                                        <span>Tipo de Ambiente Proyectado:&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                  </td>
                                  <td style="width: 330px; height: 25px;" align="left"  valign="middle">                                        
                                        <asp:DropDownList ID="ddlTipoAmbienteProyectado" runat="server" Width="200px">
                                        </asp:DropDownList>
                                  </td>         
                                </tr> 
                                <tr>
                                  <td style="width: 300px; height: 25px" align="left" valign="middle">
                                        <span>Bloque :&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                  </td>
                                  <td style="width: 330px;height: 25px;" align="left"  valign="middle">                                        
                                        <asp:DropDownList ID="ddlBloque" runat="server" Width="200px">
                                        </asp:DropDownList>
                                  </td>         
                                </tr>   
                                <tr>
                                  <td style="width: 300px; height: 25px" align="left" valign="middle">
                                        <span>Piso :&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                  </td>
                                  <td style="width: 330px;height: 25px;" align="left"  valign="middle">
                                         
                                        <asp:DropDownList ID="ddlPiso" runat="server" Width="200px">
                                        </asp:DropDownList>
                                  </td>         
                                </tr> 
                                <tr>
                                  <td style="width: 300px; height: 25px;" align="left" valign="middle">
                                        <span>Referencia :&nbsp;</span>
                                    </td>
                                  <td style="width: 330px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbReferencia" runat="server" CssClass="miTextBox" Width="300px"   Height="35px" Rows="2" TextMode="MultiLine" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbReferencia" 
                                            ValidChars="' ','.','á','é','í','ó','ú','ñ','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                  </td>                 
                                </tr>
                                <tr>
                                  <td style="width: 300px; height: 25px;" align="left" valign="middle">
                                        <span>Aforo :&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                    </td>
                                  <td style="width: 330px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbCapacidad" runat="server" CssClass="miTextBox" Width="50px" 
                                            MaxLength="100" ></asp:TextBox>
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                            TargetControlID="tbCapacidad" 
                                            ValidChars="' ','.','á','é','í','ó','ú','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                  </td>                      
                                </tr>     
                                <tr>
                                  <td style="width: 300px; height: 25px;" align="left" valign="middle">
                                        <span>Área (dimensión) :&nbsp;</span>
                                    </td>
                                  <td style="width: 330px; height: 25px;" align="left" >
                                        <asp:TextBox ID="tbArea" runat="server" CssClass="miTextBox" Width="300px" 
                                            MaxLength="100"  >0</asp:TextBox>                                        
                                  </td>                      
                                </tr>                              
                                <tr>
                                  <td style="width: 300px; height: 25px" align="left" valign="middle">
                                        <span>¿Es reservable? :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                  </td>
                                  <td style="width: 330px;height: 25px;" align="left" colspan="2" valign="middle">
                                          <asp:RadioButtonList ID="rbReserva" runat="server" RepeatDirection="Horizontal">   
                                            <asp:ListItem Value="0" Selected="True" >No</asp:ListItem>                                                                             
                                            <asp:ListItem Value="1" >Si</asp:ListItem> 
                                          </asp:RadioButtonList>    
                                  </td>
                                </tr>
                                <tr>
                                  <td style="width: 300px; height: 25px" align="left" valign="middle">
                                        <span>¿Tiene multimedia? :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                  </td>
                                  <td style="width: 330px; height: 25px;" align="left" colspan="2" valign="middle">
                                          <asp:RadioButtonList ID="rbMultimedia" runat="server" RepeatDirection="Horizontal">   
                                            <asp:ListItem Value="0" Selected="True" >No</asp:ListItem>                                                                             
                                            <asp:ListItem Value="1" >Si</asp:ListItem> 
                                          </asp:RadioButtonList>    
                                  </td>
                                </tr>
                                <tr> 
                                  <td style="width: 340px; height: 25px;" align="center" valign="middle" colspan="2">
                                        <asp:ImageButton ID="btnGrabar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                                onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                                onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                                onclick="btnGrabar_Click" />&nbsp;
                                        <asp:ImageButton ID="btnCancelar" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                                onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                                                onclick="btnCancelar_Click" CausesValidation="false"/>
                                  </td>
                                </tr>
            
                             </table>
                        </td>
                    </tr>
                    </table> 
                    
                    
                    <div id="controlAmbiente" style="display:none">
                        <input type="button" id="VerAmbiente" runat="server" />
                        <input type="button" id="OKAmbiente" />
                        <input type="button" id="CancelAmbiente" />
                    </div>   
                    
                    </asp:Panel>                               
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

