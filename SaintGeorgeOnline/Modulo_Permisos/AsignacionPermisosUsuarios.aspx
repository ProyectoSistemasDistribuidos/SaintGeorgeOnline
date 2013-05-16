<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="AsignacionPermisosUsuarios.aspx.vb" Inherits="Modulo_Permisos_AsignacionPermisosUsuarios" title="Página sin título" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style type="text/css">
               
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    .miClaseCheckBox td{ 
    width: 30px;
    height :26px;
    text-align: center;
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

<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="udp_Formulario" runat="server">
        <ContentTemplate>
            <div id="miContainerActualizacion_Ficha">
                <atk:TabContainer ID="TabContainer1" runat="server" 
                    Width="1217px" ActiveTabIndex="1"
                    AutoPostBack="false" ScrollBars="None"> 
                    <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
                        <HeaderTemplate>
                            <asp:Label ID="lbTab1" runat="server" Text="Configuración" />
                        </HeaderTemplate>
                        <ContentTemplate> 
    <table style="width:1197px; border: solid 0px red;" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td style="width: 377px; height: 25px;" align="left" valign="middle">
            <span style="font-size: 8pt; font-family: Arial;">Relación de Usuarios</span>
        </td>
        <td style="width: 820px; height: 25px;" align="left" valign="middle">
            <span style="font-size: 8pt; font-family: Arial;">Relación de Permisos</span>
        </td>
    </tr>
    <tr>            
        <td style="width: 377px; height: 25px;" align="left" valign="middle">
        </td>
        <td style="width: 820px; height: 25px;" align="left" valign="middle">
           <table cellpadding="0" cellspacing="0" border="0" style="width: 820px;">
                <tr>
                    <td style="width: 300px; height: 25px; font-size: 8pt; font-family: Arial;" align="left" valign="middle">    
<span>Trabajador :&nbsp;</span><asp:label id="lblNombreCompletoActual" runat="server" style="font-weight: bold; font-style: italic; color: Red;" /> 
<asp:HiddenField ID="hiddenCodigoPerfilActual" runat="server" Value="0" />
<asp:HiddenField ID="hiddenCodigoTrabajadorActual" runat="server" Value="0" />   
<asp:HiddenField ID="hiddenCodigoAccionGrabar" runat="server" Value="0" />                     
                    </td>
                    <td style="width: 150px; height: 25px; font-size: 8pt; font-family: Arial;" align="left" valign="middle">                      
<span>Perfil :&nbsp;</span><asp:label id="lblPerfilActual" runat="server" style="font-weight: bold; font-style: italic; color: Red;" />                    
                    </td>  
                    <td style="width: 150px; height: 25px; font-size: 8pt; font-family: Arial;" align="left" valign="middle">                      
<span>Modo :&nbsp;</span><asp:label id="lblModoActual" runat="server" style="font-weight: bold; font-style: italic; color: Red;" />                    
                    </td>   
                    <td style="width: 220px; height: 25px; font-size: 8pt; font-family: Arial;" align="left" valign="middle"> 

<asp:ImageButton ID="btnGrabar" runat="server" Width="84px" Height="19px" Visible="false" 
    ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png" 
    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabarV2_2.png'" 
    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabarV2_1.png'" ToolTip="Grabar"
    onclick="btnGrabar_Click"/>
&nbsp;&nbsp;                                                        
<asp:ImageButton ID="btnCancelar" runat="server" Width="84px" Height="19px" Visible="false"  
    ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
    onclick="btnCancelar_Click" CausesValidation="False"/>                    
                     
                    </td>                                    
                </tr>
            </table>        
        </td>
    </tr>
    <tr>
        <td style="width: 377px;" align="left" valign="top">
    <div id="miGridviewMantActualizacion_Ficha" style="width: 377px; height: 35px; margin: 0; padding: 0; border-bottom: 0;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 377px; height: 35px; color:White; background-color: #0a0f14; 
           font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;" class="miGVBusquedaFicha_HeaderAux">
        <tr>
    <td style="width:  30px; height: 35px;" align="center" valign="middle">
        <span>#</span>                                                                 
    </td>
    <td style="width: 230px; height: 35px;" align="center" valign="middle">
        <span>Nombre Completo</span>                                                                 
    </td> 
    <td style="width: 80px; height: 35px;" align="center" valign="middle">
        <span>Perfil</span>                                                                 
    </td> 
    <td style="width:  37px; height: 35px;" align="center" valign="middle">   
    </td>                      
        </tr>
    </table>  
    </div>  
    <div id="miGridviewMant" style="overflow-y: scroll; overflow-x: hidden; width: 377px; height: 400px; margin: 0; padding: 0;">   
    <asp:GridView ID="gv_Usuarios" runat="server" 
        CssClass="miGridviewBusqueda_AsignacionPermisosPerfiles" 
        GridLines="None" 
        AutoGenerateColumns="False"
        Width="360px"
        ShowHeader="false"
        AllowPaging="false" 
        AllowSorting="false"
        EmptyDataText=" - No se encontraron resultados - "
        OnRowDataBound="gv_Usuarios_RowDataBound"
        OnRowCommand="gv_Usuarios_RowCommand">
        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
        <Columns>                            
                                
        <asp:TemplateField HeaderText="#" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">                                                                                 
            <ItemTemplate>
                <div style="width: 30px; border: solid 0px red;">
                    <asp:Label ID="lblIdx" runat="server" style="font-size: 9px;" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>                                                      
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="230px">                                                                       
            <HeaderTemplate>
                <div style="width: 230px; border: solid 0px red;">
                    <span style="margin-left: 20px;">Nombre Completo</span>
                </div>    
            </HeaderTemplate>
            <ItemTemplate>
                <div style="width: 230px; border: solid 0px red;">
                    <asp:Label ID="lblNombreTrabajador" runat="server" Text='<%# Bind("NombreTrabajador") %>' style="font-size: 9px;" />
                </div>     
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Perfil" headerStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80px">                                                                       
            <ItemTemplate>
                <div style="width: 80px; border: solid 0px red;">
                    <asp:Label ID="lblPerfil" runat="server" Text='<%# Bind("Perfil") %>' style="font-size: 9px;" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>  
        
        <asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-Width="20px">
            <ItemTemplate>
                <div style="width: 20px; border: solid 0px red;">
                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoPerfil") %>' ToolTip="Actualizar Registro" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>                 
        
        <asp:TemplateField HeaderText="CodigoTrabajador" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">                                                                       
            <ItemTemplate>
                <div style="width: 0; border: solid 0px red;">
                    <asp:Label ID="lblCodigoTrabajador" runat="server" Text='<%# Bind("CodigoTrabajador") %>' />
                </div>    
            </ItemTemplate>
        </asp:TemplateField>          
        <asp:TemplateField HeaderText="CodigoPerfil" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">                                                                       
            <ItemTemplate>
                <div style="width: 0; border: solid 0px red;">
                    <asp:Label ID="lblCodigoPerfil" runat="server" Text='<%# Bind("CodigoPerfil") %>' />
                </div>
            </ItemTemplate>
        </asp:TemplateField>                    
        </Columns>
    </asp:GridView>    
    </div>
        </td>
        <td style="width: 820px;" align="left" valign="top">

    <div id="miGridviewMantActualizacion_Ficha" style="width: 820px; margin: 0; padding: 0; border-bottom: 0;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 820px; height: 35px; color:White; background-color: #0a0f14; 
            font-size:10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;" class="miGVBusquedaFicha_HeaderAux">
        <tr>
    <td style="width: 100px; height: 35px;" align="center" valign="middle" rowspan="2">
        <span>Menú</span>                                                                 
    </td> 
    <td style="width: 150px; height: 35px;" align="center" valign="middle" rowspan="2">
        <span>Opción</span>                                                                 
    </td> 
    <td style="width: 150px; height: 35px;" align="center" valign="middle" rowspan="2">
        <span>Sub Opción</span>                                                                 
    </td> 
    <td style="width: 150px; height: 35px;" align="center" valign="middle" rowspan="2">
        <span>Grupo de Información</span>                                                                 
    </td> 
    <td style="width: 27px;  height: 35px;" align="center" valign="middle" rowspan="2">
        <span>ST</span>                                                                 
    </td> 
    <td style="width: 233px; height: 18px;" align="center" valign="middle">
        <span>Acciones</span>                                                                 
    </td>     
    <td style="width: 10px;  height: 35px;" align="center" valign="middle" rowspan="2">   
    </td>   
        </tr>
        <tr>
    <td style="width:233px; height: 17px; text-align: center; ">                                                
        <asp:DataList ID="dl_NombreAcciones" runat="server" RepeatDirection="Horizontal">                                                                  
            <ItemTemplate>
                <div style="width: 30px; height: 17px; margin: 0; padding: 0; text-align: center;">
                <asp:Label ID="lblNombreAccion" runat="server" style="font-weight:bold; font-size: 9px;" Text='<%# Eval("Abreviatura") %>' />
                </div>
            </ItemTemplate>
        </asp:DataList>    
    </td>
        </tr>  
    </table>  
    </div>          
    <div id="miGridviewMant" style="overflow-y: scroll; overflow-x: scroll; width: 820px; height: 400px; margin: 0; padding: 0;">       
<asp:GridView ID="gv_ConfigPermisosPerfil" runat="server" 
    GridLines="None" 
    ShowHeader="False"  
    Width="803px"               
    CssClass="miGridviewBusqueda_AsignacionPermisosPerfiles" 
    OnRowDataBound="gv_ConfigPermisosPerfil_RowDataBound"
    AutoGenerateColumns="False">
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" /> 
    <Columns>                                                                                     
        <asp:TemplateField HeaderText="Menú" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">                                                                                                         
            <ItemTemplate>
                <div style="width: 100px; border: solid 0px red;">
                    <asp:Label ID="lblDescBloqueMenu" runat="server" Text='<%# Bind("DescBloqueMenu") %>' Font-Bold="true" />                    
                </div>
            </ItemTemplate>
        </asp:TemplateField>
                                                
        <asp:TemplateField HeaderText="Opción" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">                                                                                                         
            <ItemTemplate>
                <div style="width: 150px; border: solid 0px red;">
                    <asp:Label ID="lblDescSubBloqueMenu" runat="server" Text='<%# Bind("DescSubBloqueMenu") %>' Font-Bold="true" />                    
                </div>
            </ItemTemplate>
        </asp:TemplateField>
                                                
        <asp:TemplateField HeaderText="Sub Opción" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">                                                                                                         
            <ItemTemplate>
                <div style="width: 150px; border: solid 0px red;">
                    <asp:Label ID="lblDescSubBloqueMenuHijo" runat="server" Text='<%# Bind("DescSubBloqueMenuHijo") %>' Font-Bold="true" />               
                </div>
            </ItemTemplate>
        </asp:TemplateField>
                                                
        <asp:TemplateField HeaderText="Grupo de Información" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">                                                                                                                                                                                                                  
            <ItemTemplate>
                <div style="width: 150px; border: solid 0px red;">
                    <asp:Label ID="lblDescAsignacionBloquesInformacion" runat="server" Text='<%# Bind("DescAsignacionBloquesInformacion") %>' />               
                </div>
            </ItemTemplate>
        </asp:TemplateField>
                                                
        <asp:TemplateField HeaderText="ST" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">                                                                                                                                                                                                                  
            <ItemTemplate>
                <div style="width: 20px; border: solid 0px red;">
                    <asp:CheckBox ID="chkAccesoTotal" runat="server" />               
                </div>
            </ItemTemplate>
        </asp:TemplateField>  
                                                
        <asp:TemplateField ItemStyle-CssClass="miGVBusquedaAsignacionPermisos_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="233px">                                                                                                                                                                                                                  
            <ItemTemplate>
                <div style="width: auto; border: solid 0px red;">
                    <asp:CheckBoxList ID="chkAccionesAcceso" runat="server" Enabled="true" RepeatDirection="Horizontal" CssClass="miClaseCheckBox">
                    </asp:CheckBoxList>            
                </div>
            </ItemTemplate>
        </asp:TemplateField>
                          
        <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                      
            <ItemTemplate>
                <asp:Label ID="lblCodigoBloqueMenu" runat="server" Text='<%# Bind("CodigoBloqueMenu") %>' />
            </ItemTemplate>
        </asp:TemplateField>   
        <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                                                                                            
            <ItemTemplate>
                <asp:Label ID="lblCodigoSubBloque" runat="server" Text='<%# Bind("CodigoSubBloque") %>' />                                                                                                      
            </ItemTemplate>
        </asp:TemplateField>    
        <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                                                                                           
            <ItemTemplate>                     
                <asp:Label ID="lblCodigoSubBloquePadre" runat="server" Text='<%# Bind("CodigoSubBloquePadre") %>' />        
            </ItemTemplate>
        </asp:TemplateField>      
        <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                                                                                            
            <ItemTemplate>                    
                <asp:Label ID="lblCodigoAsignacionBloqueInformacion" runat="server" Text='<%# Bind("CodigoAsignacionBloqueInformacion") %>' />    
            </ItemTemplate>
        </asp:TemplateField>  
        <asp:TemplateField HeaderText="TipoRegistro" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                                                                                            
            <ItemTemplate>                    
                <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>' />    
            </ItemTemplate>
        </asp:TemplateField>
                                                               
    </Columns>
</asp:GridView>     
    </div>  
        
        </td>
    </tr>
    </table>
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
    <atk:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
        DynamicServicePath="" Enabled="True" BackgroundCssClass="FondoAplicacion"
        DropShadow="True" PopupControlID="pnlImpresion" TargetControlID="lblAccionExportar">
    </atk:ModalPopupExtender>
    <asp:Label ID="lblAccionExportar" runat="server" ForeColor="White" Text="..."></asp:Label>                
                 

    <atk:ModalPopupExtender ID="pnModalOpciones" runat="server"
        TargetControlID="VerOpciones"
        PopupControlID="pnlOpciones"
        BackgroundCssClass="MiModalBackground" 
        OkControlID="OKOpciones" 
        CancelControlID="CancelOpciones"
        Drag="True" 
        PopupDragHandleControlID="OpcionesHeader" DynamicServicePath="" 
        Enabled="True" />      
    <asp:panel id="pnlOpciones" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" style="width: 440px; display: none;">
        <table cellpadding="0" cellspacing="0" border="0" class="panelRegistro">          
            <tr>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">                    
                    <span style="width:30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> 
                </td>
                <td style="width: 380px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="2" id="OpcionesHeader">                
                    <span style="font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Configuración de Opciones de Permisos</span>
                </td>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                    <asp:ImageButton ID="btnCerrarPermisos" runat="server" Width="16px" Height="15px"
                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        onclick="btnCancelarPermisos_Click" ToolTip="Cerrar"/>
                </td>
            </tr>
            <tr><td colspan="4"><br /></td></tr>              
            <tr>   
                <td style="width: 30px;" rowspan="2"></td>                        
                <td style="width: 380px; height: 25px;" align="left" valign="middle" colspan="2">
                    <span>Nombre Completo :&nbsp;</span><asp:label id="lblNombreCompleto" runat="server" style="font-weight: bold; font-style: italic" />
                    <asp:HiddenField ID="hiddenCodigoPerfil" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenCodigoTrabajador" runat="server" Value="0" />
                </td>               
                <td style="width: 30px;" rowspan="2"></td> 
            </tr>   
            <tr>
                <td style="width: 380px; height: 25px;" align="left" valign="middle" colspan="2">
                    <span>Perfil :&nbsp;</span><asp:label id="lblPerfil" runat="server" style="font-weight: bold; font-style: italic" />
                </td>
            </tr>   
            <tr><td colspan="4"><br /></td></tr>
            <tr>
                <td colspan="4" align="center" valign="middle">   
                    <asp:ImageButton ID="btnVerPermisos" runat="server" Width="91px" Height="19px" 
                            ImageUrl="~/App_Themes/Imagenes/btnVisualizar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnVisualizar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnVisualizar_1.png'" ToolTip="Visualizar Permisos"
                            onclick="btnVerPermisos_Click" />&nbsp;              
                    <asp:ImageButton ID="btnAgregarPermisos" runat="server" Width="84px" Height="19px" 
                            ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" ToolTip="Agregar Permisos"
                            onclick="btnAgregarPermisos_Click" />&nbsp;
                    <asp:ImageButton ID="btnQuitarPermisos" runat="server" Width="84px" Height="19px" 
                            ImageUrl="~/App_Themes/Imagenes/btnQuitar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnQuitar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnQuitar_1.png'" ToolTip="Quitar Permisos"
                            onclick="btnQuitarPermisos_Click" />&nbsp;                            
                    <asp:ImageButton ID="btnCancelarOpciones" runat="server" Width="84px" Height="19px" 
                            ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                            onclick="btnCancelarPermisos_Click" CausesValidation="False"/>
                </td>
            </tr>   
            <tr><td colspan="4"><br /></td></tr>             
        </table>  
        <div id="controlOpciones" style="display:none">
            <input type="button" id="VerOpciones" runat="server" />
            <input type="button" id="OKOpciones" />
            <input type="button" id="CancelOpciones" />
        </div>       
    </asp:panel> 
               
                       
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

        var gridU = document.getElementById("<%= gv_Usuarios.ClientID %>");
        var grid = document.getElementById("<%= gv_ConfigPermisosPerfil.ClientID %>");

        //Evento del checkbox "All"
        $('input:checkbox[id$=chkAccesoTotal]', grid).click(function(e) {
            if (grid.rows.length > 0) {
                var estadoCheck = $(this).attr('checked');
                var rowIndex = $(this).closest('tr').prevAll().length;
                var cell = grid.rows[rowIndex].cells[5];

                var miDiv = $(cell).find('div')[0];
                var child = $(miDiv).find('table')[0]; // CheckBoxList
                var ChecksList = $('table#' + $(child).attr('id')).find('input:checkbox');

                if (estadoCheck == true) {
                    ChecksList.attr('checked', true);
                } else {
                    ChecksList.removeAttr('checked');
                }
            }
        });

    });
    
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            var gridU = document.getElementById("<%= gv_Usuarios.ClientID %>");
            var grid = document.getElementById("<%= gv_ConfigPermisosPerfil.ClientID %>");

            //Evento del checkbox "All"
            $('input:checkbox[id$=chkAccesoTotal]', grid).click(function(e) {
                if (grid.rows.length > 0) {
                    var estadoCheck = $(this).attr('checked');
                    var rowIndex = $(this).closest('tr').prevAll().length;
                    var cell = grid.rows[rowIndex].cells[5];

                    var miDiv = $(cell).find('div')[0];
                    var child = $(miDiv).find('table')[0]; // CheckBoxList
                    var ChecksList = $('table#' + $(child).attr('id')).find('input:checkbox');

                    if (estadoCheck == true) {

                        $.each(ChecksList, function(e, checkbox) {

                            var miAttr = $(checkbox).attr('disabled');
                            // (typeof attr !== 'undefined' && attr !== false)

                            if (typeof miAttr !== 'undefined' && miAttr !== false) {
                                //$(checkbox).attr('checked', true);
                            } else {
                                $(checkbox).attr('checked', true);
                            }
                        });
                        //ChecksList.attr('checked', true);

                    } else {

                        $.each(ChecksList, function(e, checkbox) {

                            var miAttr = $(checkbox).attr('disabled');

                            if (typeof miAttr !== 'undefined' && miAttr !== false) {
                                //$(checkbox).removeAttr('checked');
                            } else {
                                $(checkbox).removeAttr('checked');
                            }
                        });
                        //ChecksList.removeAttr('checked');

                    }
                }


            });

        }
    }
    
    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }
</script>

</asp:Content>

