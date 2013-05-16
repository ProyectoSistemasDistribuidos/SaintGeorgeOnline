<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="Precedencias.aspx.vb" Inherits="Mantenimientos_Colegio_Precedencias" title="Página sin título" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
                
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
        
    .panelRegistro span{
        font-size: 11px;
        font-family: Arial;
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
    <atk:TabContainer ID="TabContainer1" runat="server" Width="670px" ActiveTabIndex="0"
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
                                    <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                        <span>Grado a Llevar</span>
                                    </td>
                                    <td style="width: 410px; height: 25px; padding-left:10px" align="left" valign="middle">
                                       <asp:DropDownList ID="ddlBuscarGradoALlevar" runat="server" Width="200px">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                        
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
                            AllowPaging="false" 
                            AllowSorting="True"
                            EmptyDataText=" - No se encontraron resultados - "
                            OnPageIndexChanging="GridView1_PageIndexChanging" 
                            OnRowDataBound="GridView1_RowDataBound"
                            OnRowCommand="GridView1_RowCommand"
                            OnRowCreated="GridView1_RowCreated"
                            OnSorting="GridView1_Sorting">
                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
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
                                
                                <asp:TemplateField HeaderText="NivelALLevar">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="135">
                                        <tr>
                                            <td style="width:115px;" align="right" valign="middle">Nivel A LLevar&nbsp;</td>
                                             <td style="width:25px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NivelALLevar" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="NivelALLevar"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblNivelALLevar" runat="server" Text='<%# Bind("NivelALLevar") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="135px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="135px" />
                                </asp:TemplateField> 
                                
                                
                                <asp:TemplateField HeaderText="Grado A Llevar">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="150px">
                                        <tr>
                                            <td style="width:125px;" align="right" valign="middle">Grado A LLevar&nbsp;</td>
                                            <td style="width:25px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_CodigoGradoALlevar" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="CodigoGradoALlevar"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblGradoALlevar" runat="server" Text='<%# Bind("GradoALlevar") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="NivelRequisito">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="135px">
                                        <tr>
                                            <td style="width:115px;" align="right" valign="middle">Nivel Requisito&nbsp;</td>
                                             <td style="width:25px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NivelRequisito" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="NivelRequisito"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblNivelRequisito" runat="server" Text='<%# Bind("NivelRequisito") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="135px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="135px" />
                                </asp:TemplateField>
                                                              
                                <asp:TemplateField HeaderText="GradoRequisito">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="150">
                                        <tr>
                                            <td style="width:125px;" align="right" valign="middle">Grado Requisito&nbsp;</td>
                                             <td style="width:25px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_CodigoGradoRequisito" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="CodigoGradoRequisito"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblGradoRequisito" runat="server" Text='<%# Bind("GradoRequisito") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="150px" />
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
                                    </td>                                    
                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                   </td>
                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                  </td>                                      
                                <td style="width: 200px"></td>                                                                     
                            </tr>                        
                        </table>
                    </div>  
                           
    <atk:ModalPopupExtender ID="pnModalPrecedencia" runat="server"
        TargetControlID="VerPrecedencia"
        PopupControlID="pnlPrecedencia"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="true" 
        OkControlID="OKPrecedencia" 
        CancelControlID="CancelPrecedencia"
        Drag="true" PopupDragHandleControlID="PrecedenciaHeader" />  

    <asp:Panel id="pnlPrecedencia" BackColor="White" BorderColor="Black" BorderWidth="1px" runat="server" style="width: 400px; display: none;">

        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px #c6c6c6; width: 400px;" class="panelRegistro">
            <tr>
                <td style="width: 30px; height: 25px;" class="miGVBusquedaFicha_Header_V2">&nbsp;</td>  
                <td style="width: 340px; height: 26px; cursor: pointer;" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="2" id="PrecedenciaHeader">                
                    <span style="font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Registro de Precedencia</span>
                </td>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                    <asp:ImageButton ID="btnCerrarPanelPrecedencia" runat="server" Width="16" Height="15"
                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        onclick="btnCerrarPanelPrecedencia_Click" ToolTip="Cerrar Panel"/>
                </td>
            </tr>
            
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
    <ContentTemplate>               
            
            <tr><td style="height: 15px;" colspan="4"></td></tr>     
            
            <tr>
                <td style="width: 30px; height: 15px;" align="left" valign="middle">&nbsp;</td>
                <td colspan="2" style="height: 15px;" align="right">
                    <em>Campos Obligatorios (*)</em>
                </td>
                <td style="width: 30px; height: 15px;" align="left" valign="middle">&nbsp;</td>
            </tr>
               
            <tr>
                <td style="width: 30px; height: 25px;" align="left" valign="middle" rowspan="4">&nbsp;</td>
                <td style="width: 120px; height: 25px;" align="left" valign="middle">
                    <span>Grado a Llevar :&nbsp;</span><span class="camposObligatorios">(*)</span>
                </td>
                <td style="width: 240px; height: 25px;" align="left" valign="middle">
                    <asp:HiddenField ID="hd_Codigo" runat="server" />
                    <asp:DropDownList ID="ddlGradoALlevar" runat="server" Width="200px">
                    </asp:DropDownList>                    
                </td>
                <td style="width: 30px; height: 25px;" align="left" valign="middle" rowspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 120px; height: 25px;" align="left" valign="middle">
                    <span>Grado Requisito :&nbsp;</span><span class="camposObligatorios">(*)</span>
                </td>
                <td style="width: 240px; height: 25px;" align="left" valign="middle">
                    <asp:DropDownList ID="ddlGradoRequisito" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td style="height: 15px;" colspan="2"></td></tr>        
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
            <tr><td style="height: 15px;" colspan="4"></td></tr>  

    </ContentTemplate>        
    </asp:UpdatePanel>         
                  
        </table>            
    <div id="controlPrecedencia" style="display:none">
        <input type="button" id="VerPrecedencia" runat="server" />
        <input type="button" id="OKPrecedencia" />
        <input type="button" id="CancelPrecedencia" />
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

