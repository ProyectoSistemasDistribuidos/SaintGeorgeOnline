<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="SubbloquesMenus.aspx.vb" Inherits="Modulo_Permisos_SubbloquesMenus" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 150px;
        }
        .style2
        {
            height: 21px;
            }
        .style4
        {
            height: 21px;
            width: 150px;
        }
    
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
        .style5
        {
            height: 26px;
            width: 291px;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    
    <Triggers>
        <asp:PostBackTrigger ControlID="TabContainer1$miTab1$btnExportar" />
        <asp:PostBackTrigger ControlID="TabContainer1$miTab2$btnSubir" />
        
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
                <div style="border: solid 0px blue; width: 670px;">
                    <div id="miBusquedaMant">
                        <fieldset>
                            <legend>Criterios de busqueda</legend>
                            <table cellpadding="0" cellspacing="0" border="0" 
                                style="min-width: 610px; width: 610px;">
                                <tr><td colspan="3" style="height:10px"></td></tr>
                                <tr>
                                   <td align="left" class="style1">
                                        Menu
                                    <td style="min-width: 300px; " align="left" class="style2">
                                        <asp:DropDownList ID="ddlBuscarBloqueMenu" runat="server" Width="200px" 
                                            Height="18px">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
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
                                     <td align="left" class="style1">
                                        Descripción
                                    </td>
                                    <td style="min-width: 300px; " align="left" class="style2">
                                        <asp:TextBox ID="tbBuscarDescripcion" runat="server" CssClass="miTextBox" 
                                            Width="380px" Height="18px" />
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style1">
                                        Tipo de SubBloque</td>
                                    <td align="left" style="min-width: 300px; " class="style2">
                                        <asp:RadioButtonList ID="rbBuscarTipoSubBloque" runat="server" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Todos" Value="-1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Normal" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Padre" Value="2"></asp:ListItem>
                                            <asp:ListItem Value="3">Hijo</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style1">
                                        Estado de Avance</td>
                                    <td style="min-width: 300px; " align="left" class="style2">
                                        <asp:DropDownList ID="ddlBuscarEstadoProceso" runat="server" Width="200px" 
                                            Height="18px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style1">
                                        Estado
                                    </td>
                                    <td align="left" style="min-width: 300px; " class="style2">
                                        <asp:RadioButtonList ID="rbEstados" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Todos" Value="-1"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="Activo" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Inactivo" Value="0"></asp:ListItem>
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
                             <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 610px;">
                                <tr>
                                    <td style="width: 100px; height: 21px;" align="left" valign="middle">
                                        <asp:ImageButton ID="btnExportar" runat="server" Width="84px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                            ToolTip="Exportar" 
                                            OnClick="btnExportar_Click" />
                                    </td>
                                    <td style="width: 410px; height: 21px;" align="left" valign="middle">                                  
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
                            OnPageIndexChanging="GridView1_PageIndexChanging" 
                            OnRowDataBound="GridView1_RowDataBound"
                            OnRowCommand="GridView1_RowCommand"
                            OnRowCreated="GridView1_RowCreated"
                            OnSorting="GridView1_Sorting">
                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" 
                                ForeColor="White" HorizontalAlign="Center"  />
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
                                
                                <asp:TemplateField HeaderText="Sub Menú">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:150px;" align="right" valign="middle">Descripción&nbsp;</td>
                                            <td style="width:155px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Descripcion" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Descripcion"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Descripcion_Grilla" runat="server" Text='<%# Bind("Descripcion") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="250px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="250px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Menú">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:150px;" align="right" valign="middle">Menú&nbsp;</td>
                                            <td style="width:155px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_BloqueMenu" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="BloqueMenu"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblBloqueMenu_grilla" runat="server" Text='<%# Bind("BloqueMenu") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Tipo">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:150px;" align="right" valign="middle">Tipo&nbsp;</td>
                                            <td style="width:155px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_TipoSubBloque" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="TipoSubBloque"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_TipoSubBloque_grilla" runat="server" Text='<%# Bind("TipoSubBloque") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="70px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Estado de Proceso">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:150px;" align="right" valign="middle">Estado de Proceso&nbsp;</td>
                                            <td style="width:155px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_EstadoProceso" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="EstadoProceso"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("EstadoProceso") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="305px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="305px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Documentación">
                                    <ItemTemplate>
                                        
                                        <asp:ImageButton ID="imglink" runat="server" ImageUrl="~/App_Themes/Imagenes/Visio.png" Width="28px" Height="29px"
                                            CommandName="ExportarDocu" CommandArgument='<%# Bind("LinkDocumentacion") %>' ToolTip="Exportar Documentación" />
                                            
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" CssClass="miGridviewBusqueda_Rows" Width="28px" />
                                    
                                </asp:TemplateField>
                                                               
                                
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
                                            <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Prim. Pag" CommandArgument="First"
                                                CssClass="pagfirst" />
                                            <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                                CommandArgument="Prev" CssClass="pagprev" />
                                            <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Sig. página"
                                                CommandArgument="Next" CssClass="pagnext" />
                                            <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
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
                 <asp:Label ID="lbTab2" runat="server" Text="Inserción de Registro" />
            </HeaderTemplate>
            <ContentTemplate>
                <div style="border: solid 0px blue; width: 650px;">
                    <div id="miDetalleMant">
                        <fieldset>
                            <legend>Datos del Registro</legend>
                            <table cellpadding="0" cellspacing="0" border="0" 
                                style="min-width: 610px;">
                                <tr>
                                    <td colspan="2" style="height: 15px;" align="right">
                                        <em>Campos Obligatorios (*)</em>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px; width: 150px;" align="left" valign="middle">
                                        Menú:&nbsp;<asp:HiddenField ID="hd_Codigo" runat="server" />
                                    </td>
                                    <td style="min-width: 460px; height: 21px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlMenu" runat="server" Width="299px" Height="18px" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>                                        
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style="padding-left: 10px; width: 150px;" align="left" valign="middle">
                                        Descripción<span class="camposObligatorios">: (*)</span>
                                    </td>
                                    <td style="min-width: 300px; height: 21px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbDescripcion" runat="server" CssClass="miTextBox" 
                                            Width="400px" Height="18px" />
                                           
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px; width: 150px;" align="left" valign="middle">
                                         Tipo:</td>
                                    <td style="min-width: 300px; height: 21px;" align="left" valign="middle">
                                        <asp:RadioButtonList ID="rbTipoSubBloque" runat="server" 
                                            RepeatDirection="Horizontal" 
                                            OnSelectedIndexChanged="rbTipoSubBloque_SelectedIndexChanged" 
                                            AutoPostBack="True" >
                                            <asp:ListItem Selected="True" Text="Normal" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Padre" Value="2"></asp:ListItem>
                                            <asp:ListItem Value="3">Hijo</asp:ListItem>
                                        </asp:RadioButtonList>
                                     </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" valign="middle" style="height: 0px;">
                                        <asp:Panel ID="pnl_ListaPadres" runat="server" Visible="False">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                                <tr>
                                                    <td style="padding-left: 10px; width: 150px;" align="left" valign="middle">
                                                        Sub Bloque Padre:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSubMenuPadre" runat="server" Height="18px" 
                                                            Width="300px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px; width: 150px;" align="left" valign="middle">
                                        Link:
                                    </td>
                                    <td style="min-width: 460px; height: 21px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbLink" runat="server" CssClass="miTextBox" Width="400px" 
                                            Height="18px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px; width: 150px;" align="left" valign="middle">
                                         Ruta de Documentación:
                                    </td>
                                    <td style="min-width: 460px; height: 21px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbRutaDocumentacion" runat="server" CssClass="miTextBox" 
                                            Width="400px" BackColor="#CCCCCC" Height="18px" ReadOnly="True" />
                                    </td>
                                </tr>
                                <tr>
                                     <td align="left" valign="middle" style="height: 21px;" colspan="2">
                                         <asp:Panel ID="pnl_Imagen" runat="server" BorderStyle="None" 
                                             HorizontalAlign="Left">
                                             <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                                 <tr>
                                                     <td  style="padding-left: 10px; width: 150px;" align="left" valign="top">
                                                         Buscar Archivo</td>
                                                     <td style="min-width: 460px; height: 21px;" align="left" valign="middle">
                                                         <asp:FileUpload ID="FileUpload1" runat="server" />
                                                         &nbsp;<asp:ImageButton ID="btnSubir" runat="server" Height="18px" 
                                                             ImageUrl="~/App_Themes/Imagenes/Add-icon.png" OnClick="btn_SubirImagen_Click" 
                                                             style="vertical-align:middle" ToolTip="Clic para Adjuntar archivo" ValidationGroup="ValidarGrabar" 
                                                             Width="18px" />
                                                         <br>
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="FileUpload1" ErrorMessage="Solo documentos .vsd" 
                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.vsd)$"></asp:RegularExpressionValidator>
                                                     </td>
                                                 </tr>
                                             </table>
                                         </asp:Panel>
                                     </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px; width: 150px;" align="left" valign="middle">
                                        Estado de Proceso:&nbsp;<span class="camposObligatorios">: (*)</span></td>
                                    <td style="min-width: 460px; height: 21px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlEstadoProceso" runat="server" Width="205px" 
                                            Height="18px">
                                        </asp:DropDownList>                                        
                                    </td>
                                </tr>
                                
                            </table>
                        </fieldset>
                        
                        <div class="miEspacio">
                        </div>   
                        
                        <fieldset>
                            <legend>Relación de Bloques de Información</legend>
                            <table cellpadding="0" cellspacing="0" border="0" width="610px">
                                <tr>
                                    <td colspan="2" height="10px">
                                    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>                                      
                                        <atk:ModalPopupExtender ID="pnModalBloqueInformacion" runat="server" 
                                            TargetControlID="btnAgregarDetalleBloqueInformacion"
                                            PopupControlID="pnlBloqueInformacion" 
                                            BackgroundCssClass="MiModalBackground" 
                                            OkControlID="OKBloqueInformacion" 
                                            CancelControlID="CancelBloqueInformacion" Drag="True" 
                                            PopupDragHandleControlID="BloqueInformacionHeader" DynamicServicePath="" 
                                            Enabled="True" />
                                        <asp:Panel ID="pnlBloqueInformacion" BackColor="White" BorderColor="Black" runat="server">
                                            <table cellpadding="0" cellspacing="0" border="0" width="500px">
                                                <tr>
                                                    <td style="width: 500px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                        <span id="BloqueInformacionHeader" style="padding-left: 20px; font-weight: bold; font-size: 11px;
                                                            font-family: Arial; cursor: pointer">Agregar Bloque de Información</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" height="10px">
                                                        <asp:HiddenField ID="hfTotalRegsListaBloqueInformacion" runat="server" Value="0" />
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="2" style="width: 500px;" align="center" valign="middle">
<div style="border: solid 1px #a6a3a3; width: 500px">
<asp:GridView ID="GVListaBloqueInformacion" runat="server" 
    CssClass="miGridviewBusqueda"
    Width="500px" 
    GridLines="None" 
    AutoGenerateColumns="False"
    AllowPaging="True" AllowSorting="True"
    EmptyDataText=" - No se encontraron resultados - "
    OnRowDataBound="GVListaBloqueInformacion_RowDataBound"  
    OnPageIndexChanging="GVListaBloqueInformacion_PageIndexChanging"                                                                     
    OnRowCreated="GVListaBloqueInformacion_RowCreated"
    OnSorting="GVListaBloqueInformacion_Sorting">
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
    <Columns>     
        <asp:TemplateField>                                                             
        <ItemTemplate>
            <asp:CheckBox ID="chkSeleccionar" runat="server" OnCheckedChanged="chkSeleccionar_CheckedChanged" AutoPostBack="true" />
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
                <td style="width:250px;" align="right" valign="middle">Descripción&nbsp;</td>
                <td style="width:200px;" align="left" valign="middle">
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
            <HeaderStyle HorizontalAlign="Center" Width="450px"/>
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="450px" />
        </asp:TemplateField>
        
    </Columns>
    <PagerTemplate>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 500px;">
            <tr>
                <td style="height: 20px; width: 159px;" align="left" valign="middle">
                    <span class="miFooterMantLabelLeft">Ir a página </span>
                    <asp:DropDownList ID="ddlPageSelectorListaBloqueInformacion" runat="server" CssClass="letranormal" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPageSelectorListaBloqueInformacion_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp; de
                    <asp:Label ID="lblNumPaginas_ListaBloqueInformacion" runat="server" />
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
                    <asp:Label ID="lblRegistrosActuales_ListaBloqueInformacion" runat="server" CssClass="miFooterMantLabelRight" />
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
                                                        <asp:ImageButton ID="btnModalAceptarBloqueInformacion" runat="server" 
                                                            Width="84px" Height="19px"
                                                            ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                                                            OnClick="btnModalAceptarBloqueInformacion_Click"
                                                            ToolTip="Aceptar" />&nbsp;
                                                        <asp:ImageButton ID="btnModalCancelarBloqueInformacion" runat="server" 
                                                            Width="84px" Height="19px"
                                                            ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                                            OnClick="btnModalCancelarBloqueInformacion_Click"
                                                            ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" height="10px">
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="controlCampoInformacion" style="display: none">
                                                <input type="button" id="OKBloqueInformacion" />
                                                <input type="button" id="CancelBloqueInformacion" />                                                
                                                <asp:Button ID="btnAgregarDetalleBloqueInformacion" runat="server" />      
                                            </div>
                                        </asp:Panel>
                                        
                   
                                        <atk:ModalPopupExtender ID="pnModalAcciones" runat="server" TargetControlID="btnAgregarDetalleAcciones"
                                            PopupControlID="pnlAcciones" BackgroundCssClass="MiModalBackground" 
                                            OkControlID="OKAcciones" CancelControlID="CancelAcciones" Drag="True" 
                                            PopupDragHandleControlID="AccionesHeader" DynamicServicePath="" 
                                            Enabled="True" />     
                                        <asp:Panel ID="pnlAcciones" BackColor="White" BorderColor="Black" runat="server">
                                            <table cellpadding="0" cellspacing="0" border="0" width="250px">
                                                <tr>
                                                    <td style="width: 250px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                        <span id="AccionesHeader" style="padding-left: 20px; font-weight: bold; font-size: 11px;
                                                            font-family: Arial; cursor: pointer">Agregar Acciones</span>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="2" height="10px">
                                                        <asp:HiddenField ID="hfTotalRegsListaAcciones" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hiddenCodigoBloqueInformacion" runat="server" Value="0" />
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="2" style="width: 250px;" align="center" valign="middle">
<div style="border: solid 1px #a6a3a3; width: 210px">
<asp:GridView ID="GVListaAcciones" runat="server" 
    CssClass="miGridviewBusqueda"
    Width="210px" 
    GridLines="None" 
    AutoGenerateColumns="False"
    AllowPaging="false" AllowSorting="false"
    EmptyDataText=" - No se encontraron resultados - "
    OnRowDataBound="GVListaAcciones_RowDataBound">
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
    <Columns>     
        <asp:TemplateField>  
        <HeaderTemplate>
            <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />                                    
        </HeaderTemplate>                                                              
        <ItemTemplate>
            <asp:CheckBox ID="chkSeleccionar" runat="server" OnCheckedChanged="chkSeleccionarListaAcciones_CheckedChanged" AutoPostBack="true" />
        </ItemTemplate>                                   
            <ItemStyle CssClass="miGridviewBusqueda_Rows" Width="50px" />
        </asp:TemplateField> 
                                                                       
        <asp:BoundField DataField="Codigo" HeaderText="Código" >        
            <HeaderStyle CssClass="miHiddenStyle" />
            <ItemStyle CssClass="miHiddenStyle" />
        </asp:BoundField>
        
        <asp:TemplateField HeaderText="Descripcion">                                                                      
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Descripcion") %>' />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="160px"/>
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="160px" />
        </asp:TemplateField>
        
    </Columns>
</asp:GridView>
</div>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="2" height="10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 250px; height: 25px" align="center" valign="middle" colspan="2">
                                                        <asp:ImageButton ID="btnModalAceptarAcciones" runat="server" Width="84px" Height="19px"
                                                            ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                                                            OnClick="btnModalAceptarAcciones_Click"
                                                            ToolTip="Aceptar" />&nbsp;
                                                        <asp:ImageButton ID="btnModalCancelarAcciones" runat="server" Width="84px" Height="19px"
                                                            ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                                            OnClick="btnModalCancelarAcciones_Click"
                                                            ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" height="10px">
                                                    </td>
                                                </tr>                                                
                                            </table>
                                            <div id="controlAcciones" style="display: none">
                                                <input type="button" id="OKAcciones" />
                                                <input type="button" id="CancelAcciones" />
                                                <asp:Button ID="btnAgregarDetalleAcciones" runat="server" /> 
                                            </div>
                                        </asp:Panel>  
    </ContentTemplate>
    </asp:UpdatePanel>                                                                                       
                                          
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 610px;" align="center" valign="top" colspan="2">
                                        <table cellpadding="0" cellspacing="0" border="0" width="610px">
                                            <tr>
                                                <td style="width: 610px; height: 26px;" align="right">
                                                    <asp:ImageButton ID="btnAgregarBloqueInformacion" runat="server" Width="84px" Height="19px"
                                                        ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" 
                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'" 
                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" 
                                                        OnClick="btnAgregarBloqueInformacion_Click"
                                                        ToolTip="Agregar" />
                                                        <asp:HiddenField ID="hfTotalRegsDetalleBloqueInformacion" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 610px; height: 25px" align="center" valign="top" colspan="3">
                                                
    <asp:UpdatePanel ID="upBloqueInformacion" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div id="miGVMantFichaRegitros_Detalle">
    <asp:GridView ID="GVDetalleBloqueInformacion" runat="server" CssClass="miGridviewBusqueda"
        Width="610px" GridLines="None" 
        AutoGenerateColumns="False" 
        ShowHeader="true" ShowFooter="false"
        AllowPaging="false" AllowSorting="True"
        OnRowDataBound="GVDetalleBloqueInformacion_RowDataBound"
        OnRowCommand="GVDetalleBloqueInformacion_RowCommand"
        OnPageIndexChanging="GVDetalleBloqueInformacion_PageIndexChanging"                                                                     
        OnRowCreated="GVDetalleBloqueInformacion_RowCreated"
        OnSorting="GVDetalleBloqueInformacion_Sorting">
        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
        <Columns> 
                                                                           
        <asp:TemplateField>
            <ItemTemplate>
                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                    CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Quitar Registro" />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="30px" />
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
    
        <asp:TemplateField HeaderText="Codigo">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("CodigoBloqueInformacion") %>' />
            </ItemTemplate>
            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="CodigoRelacion">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("CodigoRelacion") %>' />
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
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Descripcion") %>' />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="300px"/>
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="300px" />
        </asp:TemplateField>        
        
        
        <asp:TemplateField>                                                                                 
            <ItemTemplate>
                <asp:ImageButton ID="btnAcciones" runat="server" ImageUrl="~/App_Themes/Imagenes/Add-icon.png" width="16px" Height="16"
                    CommandName="AsignarAccion" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Agregar Acciones" />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="30px" />
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
       </asp:TemplateField>   
                                
        <asp:TemplateField HeaderText="Acciones">                                                                      
            <ItemTemplate>
                <asp:Label ID="lblDescripAcciones" runat="server" Text='<%# Bind("DescripcionAcciones") %>' />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="250px"/>
            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="250px" />
        </asp:TemplateField>
        
        <asp:BoundField DataField="Descripcion" HeaderText="CodigoAcciones" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0" />
        
        
    </Columns>
                                                                    
    <PagerTemplate>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 500px;">
            <tr>
                <td style="height: 20px; width: 159px;" align="left" valign="middle">
                    <span class="miFooterMantLabelLeft">Ir a página </span>
                    <asp:DropDownList ID="ddlPageSelectorDetalleBloqueInformacion" runat="server" CssClass="letranormal" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPageSelectorDetalleBloqueInformacion_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp; de
                    <asp:Label ID="lblNumPaginas_DetalleBloqueInformacion" runat="server" />
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
                    <asp:Label ID="lblRegistrosActuales_DetalleBloqueInformacion" runat="server" CssClass="miFooterMantLabelRight" />
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
                    <div class="miEspacio">
                        
                        <asp:Label ID="lbl_llamadorPopupIngreso" runat="server"></asp:Label>
                        
                    </div>  
                              
                    <div id="miFooterDetalleMant">
                        <asp:ImageButton ID="btnGrabar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                            onclick="btnGrabar_Click" ValidationGroup="ValidarGrabar"/>&nbsp;
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

