<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="AsignacionPermisosPerfiles.aspx.vb" Inherits="Modulo_Permisos_AsignacionPermisosPerfiles" title="Página sin título" %>

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
        
    <ContentTemplate>
    
 <div id="miContainerMantenimiento">
    <atk:TabContainer ID="TabContainer1" runat="server" Width="881px" ActiveTabIndex="0"
        AutoPostBack="false" ScrollBars="None" >        
        <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="true">
            <HeaderTemplate>
                 <asp:Label ID="lbTab2" runat="server" Text="Inserción" />
            </HeaderTemplate>
            <ContentTemplate>
                <div style="border: solid 0px blue; width: 850px;">
                    <div id="miDetalleMant">
                        
                            <table cellpadding="0" cellspacing="0" border="0" width="850px" >                                
                                
                                <tr>
                                    <td align="left" valign="middle" >
                                        Perfil:&nbsp;<asp:DropDownList ID="ddl_Perfiles" runat="server" Height="20px" 
                                            Width="300px" OnSelectedIndexChanged="ddl_Perfiles_SelectedIndexChanged" AutoPostBack="true"  >
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" valign="middle" >
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left" valign="bottom">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"  valign="middle" colspan="2" >
                                        &nbsp;</td>
                                    <td align="left"  style="min-width: 460px; " valign="bottom">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"  valign="middle" colspan="3" 
                                        style="margin-left :0px;margin-right:0px ;padding-left:0px;padding-right:0px; "> 
                                        <div style="border: solid 1px black; width :850px;">
                                        
                                        <table cellpadding="0" border="0" cellspacing="0" class="miGridviewBusqueda_Header"   >
                                            <tr>
                                                <td style="width:100px;text-align:center;vertical-align:top;font-weight:bold;   ">
                                                Menú
                                                </td>                                            
                                                <td style="width:150px;text-align:center;vertical-align:top;font-weight:bold;   ">
                                                Opción
                                                </td>
                                                <td style="width:150px;text-align:center;vertical-align:top;font-weight:bold;   ">
                                                Sub Opción
                                                </td>
                                                <td style="width:150px;text-align:center;vertical-align:top;font-weight:bold;   ">
                                                Grupo de Información
                                                </td>
                                                <td style="width:20px;text-align:center;vertical-align:top;font-weight:bold;   ">
                                                ST
                                                </td>
                                                <td style="width:300px;text-align:center;font-weight:bold;    ">
                                                Acciones                                                     
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:100px;text-align:center;vertical-align:top; ">
                                                &nbsp;
                                                </td>                                            
                                                <td style="width:150px;text-align:center;vertical-align:top;   ">
                                                &nbsp;
                                                </td>
                                                <td style="width:150px;text-align:center;vertical-align:top;   ">
                                                &nbsp;
                                                </td>
                                                <td style="width:150px;text-align:center;vertical-align:top;   ">
                                                &nbsp;
                                                </td>
                                                <td style="width:20px;text-align:center;vertical-align:top;   ">
                                                &nbsp;
                                                </td>
                                                <td style="width:300px;text-align:center;padding-left:5px;     ">
                                                
                                                    <asp:DataList ID="dl_NombreAcciones" 
                                                                  RepeatDirection="Horizontal" 
                                                                  
                                                                  runat="server">
                                                                  
                                                                  <ItemTemplate>
  
                                                                  <asp:Label ID="ProductNameLabel" 
                                                                    runat="server" 
                                                                    Font-Bold="true"  
                                                                    Text='<%# Eval("NAA_Abrev") %>' >
                                                                  </asp:Label><br />
                                                                </ItemTemplate>


                                                    </asp:DataList>    
                                                </td>
                                            </tr>
                                            
                                        </table>
                                        <div style="height:400px;overflow:scroll;" >
                                        <asp:GridView ID="gv_ConfigPermisosPerfil" 
                                            runat="server" 
                                            GridLines="None" 
                                             
                                            CssClass="miGridviewBusqueda_AsignacionPermisosPerfiles" 
                                            OnRowDataBound="gv_ConfigPermisosPerfil_RowDataBound"
                                            AutoGenerateColumns="False" Width="850px" ShowHeader="False">
                                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" 
                                                        ForeColor="White" HorizontalAlign="Center" />
                                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" /> 
                                            <Columns>
                                                                                            
                                                <asp:TemplateField HeaderText="CodigoBloque">                                                                      
                                                     <ItemTemplate>
                                                     
                                                         <asp:Label ID="lbl_CodigoBloque_grilla" runat="server" Text='<%# Bind("BM_CodigoBloque") %>' />
                                                                                                      
                                                     </ItemTemplate>
                                                     <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px"/>
                                                     <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                </asp:TemplateField>
                                                                                                
                                                <asp:TemplateField HeaderText="Menú"  >                                                                                                         
                                                     <ItemTemplate  >
                                                          <asp:Label  ID="lbl_Descripcion_Bloque_grilla" runat="server" Text='<%# Bind("Descripcion_Bloque") %>' Font-Bold="true"   />
                                                     </ItemTemplate>
                                                     <ItemStyle  CssClass="miGVBusquedaAsignacionPermisos_Rows_2" HorizontalAlign="Left" Width="100px"  />
                                                </asp:TemplateField>
                                                                                            
                                                <asp:TemplateField HeaderText="CodigoSubBloque">                                                                      
                                                     <ItemTemplate>
                                                     
                                                         <asp:Label ID="lbl_CodigoSubBloque_grilla" runat="server" Text='<%# Bind("SBM_CodigoSubBloque") %>' />
                                                                                                      
                                                     </ItemTemplate>
                                                     <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px"/>
                                                     <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Opción"  >                                                                                                         
                                                     <ItemTemplate  >
                                                          <asp:Label  ID="lbl_Descripcion_SubBloque_grilla" runat="server" Text='<%# Bind("Descripcion_SubBloque") %>' Font-Bold="true"  />
                                                     </ItemTemplate>
                                                     <ItemStyle  CssClass="miGVBusquedaAsignacionPermisos_Rows_2" HorizontalAlign="Left" Width="150px"  />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="CodigoSubBloque_Hijo">                                                                      
                                                     <ItemTemplate>
                                                     
                                                         <asp:Label ID="lbl_CodigoSubBloque_Hijo_grilla" runat="server" Text='<%# Bind("SBM_CodigoSubBloque_Hijo") %>' />
                                                                                                      
                                                     </ItemTemplate>
                                                     <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px"/>
                                                     <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Sub Opción"  >                                                                                                         
                                                     <ItemTemplate  >
                                                          <asp:Label  ID="lbl_Descripcion_SubBloque_Hijo_grilla" runat="server" Text='<%# Bind("Descripcion_SubBloque_Hijo") %>' Font-Bold="true" />
                                                     </ItemTemplate>
                                                     <ItemStyle  CssClass="miGVBusquedaAsignacionPermisos_Rows_2" HorizontalAlign="Left" Width="150px"  />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="CodigoBloqueInformacion">                                                                      
                                                     <ItemTemplate>
                                                     
                                                         <asp:Label ID="lbl_CodigoBloqueInformacion_grilla" runat="server" Text='<%# Bind("BI_CodigoBloqueInformacion") %>' />
                                                                                                      
                                                     </ItemTemplate>
                                                     <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px"/>
                                                     <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Grupo de Información"  >                                                                                                         
                                                     <ItemTemplate  >
                                                          <asp:Label  ID="lbl_Descripcion_BloqueInformacion_grilla" runat="server" Text='<%# Bind("Descripcion_BloqueInformacion") %>' />
                                                     </ItemTemplate>
                                                     <ItemStyle  CssClass="miGVBusquedaAsignacionPermisos_Rows" HorizontalAlign="Left" Width="140px"  />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="ST">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_AccesoTotal_grilla" 
                                                                      runat="server" 
                                                                      Font-Size="8pt" 
                                                                      ValidationGroup='<%# Bind("CodigoAsignacion") %>'  
                                                                      OnCheckedChanged="chk_Habilitar_grilla_CheckedChanged" 
                                                                      AutoPostBack="true"   />
                                                    </ItemTemplate> 
                                                    <ItemStyle  CssClass="miGVBusquedaAsignacionPermisos_Rows"  Width="20px"  HorizontalAlign="Center" />
                                                </asp:TemplateField>  
                                                
                                                <asp:TemplateField >
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                            <td style ="padding-left:5px " >
                                                            <asp:CheckBoxList ID="chk_AccionesAcceso_grilla" runat="server" Font-Size="8pt" 
                                                            Enabled="true" RepeatDirection="Horizontal">
                                                            </asp:CheckBoxList>
                                                            </td> 
                                                            </tr>
                                                        </table>
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle  CssClass="miGVBusquedaAsignacionPermisos_Rows" HorizontalAlign="Left" Width="310px"  />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="CodigoAsignacion">                                                                      
                                                     <ItemTemplate>
                                                     
                                                         <asp:Label ID="lbl_CodigoAsignacion_grilla" runat="server" Text='<%# Bind("CodigoAsignacion") %>' />
                                                                                                      
                                                     </ItemTemplate>
                                                     <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px"/>
                                                     <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                </asp:TemplateField>
                                                 
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                       
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

