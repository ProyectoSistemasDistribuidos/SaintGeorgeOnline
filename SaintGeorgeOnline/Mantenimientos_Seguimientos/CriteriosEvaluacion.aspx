﻿<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="CriteriosEvaluacion.aspx.vb" Inherits="Mantenimientos_Seguimientos_CriteriosEvaluacion" title="Página sin título" %>

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
//'Updated 2
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
                                        <span>Descripción</span>
                                    </td>
                                    <td style="width: 410px; height: 25px; padding-left:10px" align="left" valign="middle">
                                        <asp:TextBox ID="tbBuscarDescripcion" runat="server" CssClass="miTextBox" Width="400px" MaxLength="100" />
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbBuscarDescripcion" 
                                            ValidChars="' ','.','á','é','í','ó','ú','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
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
                              
                                <asp:TemplateField HeaderText="Descripción en Ingles">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="170px">
                                        <tr>
                                            <td style="width:140px;" align="right" valign="middle">Descripción en Ingles&nbsp;</td>
                                            <td style="width:30px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Descripcion" runat="server" 
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
                                    <HeaderStyle HorizontalAlign="Center" Width="170px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="170px" />
                                </asp:TemplateField>   
                                
                                    <asp:TemplateField HeaderText="Descripción en Español">                                                                       
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcionEspaniol" runat="server" Text='<%# Bind("DescripcionEspaniol") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="170px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="170px" />
                                </asp:TemplateField>   
                                
                                <asp:TemplateField HeaderText="Abreviatura">                                                                       
                                    <ItemTemplate>
                                        <asp:Label ID="lblAbreviatura" runat="server" Text='<%# Bind("Abreviatura") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="60px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>                                
                                
                                <asp:TemplateField HeaderText="Tipo">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100px;">
                                        <tr>
                                            <td style="width:60px;" align="right" valign="middle">Tipo&nbsp;</td>                                            
                                            <td style="width:40px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Tipo" runat="server" 
                                                ToolTip="Tipo"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Tipo"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>   
                                
                                <asp:TemplateField HeaderText="Orden">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:40px;" align="right" valign="middle">Orden&nbsp;</td>                                        
                                            <td style="width:20px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Orden" runat="server" 
                                                ToolTip="Orden"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Orden"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrden" runat="server" Text='<%# Bind("Orden") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>   
                                
                                <asp:BoundField DataField="Estado">
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
                                        <span>Descripción en Ingles :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <asp:HiddenField ID="hd_Codigo" runat="server" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                                            TargetControlID="tbDescripcion" ValidChars="' ','-','.','á','é','í','ó','ú','(',')',':','/'">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 50px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbDescripcion" runat="server" CssClass="miTextBox" Width="450px"
                                            Height="35px" Rows="2" TextMode="MultiLine" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 50px;" align="left" valign="middle">
                                        <span>Descripción en Español :&nbsp;</span>
                                     <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                                            TargetControlID="tbDescripcionEspaniol" ValidChars="' ','-','.','á','é','í','ó','ú','(',')',':','/'">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 50px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbDescripcionEspaniol" runat="server" CssClass="miTextBox" Width="450px"
                                            Height="35px" Rows="2" TextMode="MultiLine" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Abreviatura :&nbsp;</span>
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                                            TargetControlID="tbAbreviatura" ValidChars="' ','.','á','é','í','ó','ú','(',')',':','/'">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="width: 460px; height: 25px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbAbreviatura" runat="server" CssClass="miTextBox" Width="100px" MaxLength="10" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Tipo :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="middle">
                                        <asp:RadioButtonList ID="rbTipo" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Text="Letras" Selected="True"/>
                                            <asp:ListItem Value="1" Text="Números"/>
                                    </asp:RadioButtonList> 
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Orden :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" 
                                            FilterType="Numbers" TargetControlID="tbOrden">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbOrden" runat="server" CssClass="miTextBox" Width="50px" MaxLength="3" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>    
                    <div class="miEspacio"></div>            
                    <div id="miFooterDetalleMant">
                        <asp:ImageButton ID="btnGrabar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                            onclick="btnGrabar_Click" />&nbsp;
                        <asp:ImageButton ID="btnCancelar" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                            onclick="btnCancelar_Click" CausesValidation="false"/>
                    </div>          
                </div>    
            </ContentTemplate> 
    </atk:TabPanel>
    
    </atk:TabContainer>
    
    <asp:Panel ID="pnlImpresion" runat="server" BackColor="White" Height="71px" Width="388px"
         Style="display: none">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: right;">
                     <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10px; color: #000080;
                     text-align: center;">
                     <img alt="" src="../App_Themes/Imagenes/bigrotation2.gif" style="width: 32px; height: 32px" />
                     <br />Exportando, espere unos segundos ...
                </td>
            </tr>
        </table>
    </asp:Panel>
    <atk:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
         Enabled="True" BackgroundCssClass="FondoAplicacion" DropShadow="True" PopupControlID="pnlImpresion"
         TargetControlID="lblAccionExportar">
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
