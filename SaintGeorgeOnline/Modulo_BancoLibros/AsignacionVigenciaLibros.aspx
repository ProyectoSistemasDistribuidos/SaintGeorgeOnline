<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="AsignacionVigenciaLibros.aspx.vb" Inherits="Modulo_BancoLibros_AsignacionVigenciaLibros" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">     
    <ContentTemplate>   
    <div id="miContainerMantenimiento">
     
    <atk:TabContainer ID="TabContainer1" runat="server" Width="1180px" ActiveTabIndex="0" AutoPostBack="false" ScrollBars="None">        
    <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
        <HeaderTemplate>
            <asp:Label ID="lbTab1" runat="server" Text="Asignación de Vigencia de Libros" />
        </HeaderTemplate>
        <ContentTemplate> 
            <div style="border: solid 0px blue; width: 1140px;">            
                <div id="miBusquedaActualizacion_Ficha" style ="width :720px">
                    <!-- 650px -->
                     <table cellpadding="0" cellspacing="0" border="0" width="840px" style="margin: 0;">
                            <tr>
                                <td style="width: 740px;" align="left" valign="middle">
                                   <fieldset style ="width :740px">
                                     <legend>Criterios de Búsqueda</legend>
                                        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 720px;">
                                           <tr>                                  
                                              <td style="padding-left :5px; width:200px; height: 25px;" align="left" valign="middle" >
                                                      <span>Año Académico de Inicio :&nbsp;</span>
                                                    </td>
                                                    <td style=" width:520px; height: 25px;" align="left" valign="middle">
                                                       <asp:DropDownList ID="ddlAnioAcademico1" runat="server" Width="150px" style="font-size: 8pt; font-family: Courier New;" >
                                                      </asp:DropDownList>
                                                     </td>  
                                            </tr>
                                             <tr>                                                        
                                               <td style="padding-left :5px; width:200px; height: 25px;" align="left" valign="middle" >
                                                      <span>Año Académico de Fin:&nbsp;</span>
                                                    </td>
                                              <td style=" width:520px; height: 25px;" align="left" valign="middle">
                                                  <asp:DropDownList ID="ddlAnioAcademico2" runat="server" Width="150px" style="font-size: 8pt; font-family: Courier New;" >
                                                 </asp:DropDownList>    
                                            </td>                                
                                          </tr>                               
                                       </table>
                                   </fieldset>
                                </td>
                                <td style="width: 100px;" align="center" valign="middle">
                                
                                    <table cellpadding="0" cellspacing="0" border="0" width="84px" style="margin: 0;">
                                        
                                        <tr>
                                            <td style="width: 84px;" align="center" valign="middle">
                                                <asp:ImageButton ID="btnBuscar" runat="server" Width="84" Height="19" 
                                                ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'" 
                                                onclick="btnBuscar_click"
                                                ToolTip="Buscar"
                                                />                                            
                                            </td>
                                        </tr>
                                        <tr><td style="height:10px;"></td></tr>
                                        <tr>
                                            <td style="width: 84px;" align="center" valign="middle">
                                                 <asp:ImageButton ID="btnLimpiar" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                    onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/>   
                                            </td>
                                        </tr>
                                    </table>    
                                                                  
                                </td>
                            </tr>
                            <tr>
                            <td colspan ="2" style="width: 840px; height :10px;" ></td></tr>
                            <tr>
                                 <td colspan ="2" style="width: 840px;" align="left" valign="middle">
                                  <fieldset style ="width :840px">
                                    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width:840px;">
                                           <tr>                                  
                                              <td  style="width: 200px; height: 26px;" align="left" valign="middle">
                                                    
                                             </td>  
                                             <td  style="width: 640px; height: 26px;" align="right" valign="middle">
                                                    <asp:ImageButton ID="btnGrabar" runat="server" Width="84" Height="19" 
                                                    ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabarV2_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabarV2_1.png'" 
                                                    onclick="btnGrabar_click"
                                                    ToolTip="Grabar"
                                                    />    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                     <asp:ImageButton ID="btnCancelar" runat="server" Width="84" Height="19"
                                                    ImageUrl="~/App_Themes/Imagenes/btnCancelarV2_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelarV2_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelarV2_1.png'" 
                                                    onclick="btnCancelar_Click" 
                                                    ToolTip="Cancelar"
                                                    CausesValidation="false"/> 
                                             </td>                              
                                          </tr>   
                                       </table>
                                  </fieldset>    
                               </td>                             
                            </tr>
                        </table>
                </div>                                                                          
                <div class="miEspacio"></div>   
                                
                          <div id="miGridviewMant"  style=" border-bottom: 0; overflow-y: scroll; overflow-x: hidden; width: 840px; height: 210px;">                         
                        <asp:GridView ID="GridView1" runat="server" 
                            width="840px"
                            CssClass="miGridviewBusqueda"
                            GridLines="None" 
                            AutoGenerateColumns="False" 
                            AllowPaging="False"
                            AllowSorting="True" 
                            EmptyDataText=" - No se encontraron resultados - "
                            OnRowDataBound="GridView1_RowDataBound"
                            ShowHeader="true" >
                           <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="35px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                       <asp:Label ID="lblIdFila" runat="server" Text='<%# Bind("IdFila") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            
                                <asp:TemplateField HeaderText="Titulo" ItemStyle-Width="150px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                       <asp:Label ID="lblTitulo" runat="server" Text='<%# Bind("Titulo") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Editorial" ItemStyle-Width="250px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                       <asp:Label ID="lblEditorial" runat="server" Text='<%# Bind("Editorial") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Autor" ItemStyle-Width="150px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                       <asp:Label ID="lblAutor" runat="server" Text='<%# Bind("Autor") %>' style="padding-left: 10px;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Idioma" ItemStyle-Width="150px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                       <asp:Label ID="lblIdioma" runat="server" Text='<%# Bind("Idioma") %>' style="padding-left: 10px;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="DataList" ItemStyle-Width="250px" HeaderStyle-Width="300px"    
                                    ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <div style="margin: 0; padding: 0; width:250px;">
                                        <asp:DataList ID="ListaAniosAcademicos" runat="server" RepeatColumns="13" RepeatDirection="Horizontal">
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width:50px;">
                                                    <tr>
                                                        <td style="width:50px;" align="center" valign="middle">
                                                            <asp:label ID="dlHeader" runat="server" style="font-size: 8pt; font-family: Arial;" Text='<%# Bind("AniosAcademicos") %>' />
                                                            <asp:HiddenField ID="hiddenCodigoCriterioHeader" runat="server" Value='<%# Bind("CodigoAnioAcademico") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                               
                                        </asp:DataList>  
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="padding-left:25px; width: 250px; border: solid 0px red; vertical-align:middle">  
                                         <asp:CheckBoxList ID="chk_Libros_grilla" runat="server" Enabled="true" RepeatDirection="Horizontal" CssClass="miClaseCheckBox">
                                         </asp:CheckBoxList>   
                                        <span style="display: none;">&nbsp;</span>                                      
                                       </div>   
                                    </ItemTemplate>
                                     <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="250px" />  
                                </asp:TemplateField>
                                
                                <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoLibro" runat="server" Text='<%# Bind("CodigoLibro") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                           </Columns>
                       </asp:GridView>
                         </div>                          
                      
                   
        </ContentTemplate>
    </atk:TabPanel> 
   </atk:TabContainer>
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

