<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ProcedimientosRealizados.aspx.vb" Inherits="Mantenimientos_Enfermeria_ProcedimientosRealizados" title="P�gina sin t�tulo" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 21px;
            width: 400px;
        }
        .style3
        {
            height: 25px;
            width: 400px;
        }
    </style>
    
    <style type="text/css">
               
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" >

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }
    
</script>

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
                    <div id="miBusquedaMant">
                        <fieldset>
                            <legend>Criterios de busqueda</legend>
                            <table cellpadding="0" cellspacing="0" border="0" 
                                style="min-width: 610px;">
                                <tr>
                                    <td style="width: 100px; height: 25px;" align="left" valign="bottom">
                                        <span>Descripci�n</span>
                                    </td>
                                    <td style="min-width: 410px; padding-left:10px " align="left" class="style2">
                                        <asp:TextBox ID="tbBuscarDescripcion" runat="server" CssClass="miTextBox" 
                                            Width="400px" MaxLength="100" Height="18px" />
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbBuscarDescripcion" 
                                            ValidChars="' ','�','�','�','�','�','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="width: 100px; padding-top:6px" align="right" valign="top" rowspan="2">
                                        
                                                <asp:ImageButton ID="btnBuscar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                    onclick="btnBuscar_Click" ToolTip="Buscar Registros"/>
                                                <br /><br />    
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
                                    <td style="min-width: 410px; " align="left" valign="bottom" class="style3" >
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
                            EmptyDataText=" - No se encontraron resultados - "
                            EmptyDataRowStyle-ForeColor="#a51515"
                            EmptyDataRowStyle-HorizontalAlign="Center"
                            OnPageIndexChanging="GridView1_PageIndexChanging" 
                            OnRowDataBound="GridView1_RowDataBound"
                            OnRowCommand="GridView1_RowCommand"
                            OnRowCreated="GridView1_RowCreated"
                            OnSorting="GridView1_Sorting">
                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" 
                                ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
                            <Columns>                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                                            CommandName="Actualizar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Actualizar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
                                        Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                            CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Eliminar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
                                        Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnActivar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_activar.png" 
                                            CommandName="Activar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Activar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
                                        Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Descripci�n">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:150px;" align="right" valign="middle">Descripci�n&nbsp;</td>
                                            <td style="width:155px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting" runat="server"                                    
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
                                    <HeaderStyle HorizontalAlign="Center" Width="305px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="305px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField HeaderText="Estado" DataField="Estado" >
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle"/>
                                    <ItemStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle" />
                                </asp:BoundField>
                                
                            </Columns>
                            <PagerTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 628px;">
                                    <tr>                                        
                                        <td style="height: 20px; width: 200px;" align="left" valign="middle">
                                            <span class="miFooterMantLabelLeft">Ir a p�gina   </span>                                         
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
                                            <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="P�gina anterior"
                                                CommandArgument="Prev" CssClass="pagprev" />
                                            <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="P�gina siguiente"
                                                CommandArgument="Next" CssClass="pagnext" />
                                            <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="�ltima Pagina" CommandArgument="Last"
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
                                <td style="width: 120px; height: 26px;" align="left" valign="middle">
                                    <span>Actualizar Registro&nbsp;</span>&#124;
                                </td>                                    
                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                    <img alt="Eliminar Registro" src="../App_Themes/Imagenes/opc_eliminar.png"/></td>
                                <td style="width: 120px; height: 26px;" align="left" valign="middle">
                                    <span>Eliminar Registro&nbsp;</span>&#124;
                                </td>                                     
                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                    <img alt="Activar Registro" src="../App_Themes/Imagenes/opc_activar.png"/></td>
                                <td style="width: 120px; height: 26px;" align="left" valign="middle">
                                    <span>Activar Registro</span>   
                                <td style="width: 80px"></td>                                                                     
                            </tr>                        
                        </table>
                    </div>
                    <div class="miEspacio">
                    </div>    
                </div>
            </ContentTemplate>
        </atk:TabPanel>  
        <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="false">
            <HeaderTemplate>
                 <asp:Label ID="lbTab2" runat="server" Text="Inserci�n" />
            </HeaderTemplate>
            <ContentTemplate>
                <div style="border: solid 0px blue; width: 650px;">
                    <div id="miDetalleMant">
                        <fieldset>
                            <legend>Datos del Registro</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 610px;">                                
                                <tr><td colspan="2" style="height:15px" align="right">
                                        <em>Campos Obligatorios (*)</em>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 21px;" align="left">
                                        <span>Descripci�n&nbsp;</span><span class="camposObligatorios">(*)</span>
                                        <asp:HiddenField ID="hd_Codigo" runat="server" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbDescripcion" 
                                            ValidChars="' ','�','�','�','�','�','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style="min-width: 460px; " align="left" class="style1">
                                        <asp:TextBox ID="tbDescripcion" runat="server" CssClass="miTextBox" 
                                            Width="450px" MaxLength="100" Height="35px" TextMode="MultiLine" />
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



