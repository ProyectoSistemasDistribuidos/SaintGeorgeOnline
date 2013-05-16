<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ComunicadosHome.ascx.vb" Inherits="Controles_ComunicadosHome" %>

  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
  
  <Triggers>
        <asp:PostBackTrigger ControlID="GVConsultaComunicado" />
  </Triggers>
    
  <ContentTemplate>
<table border="0" cellpadding="0" cellspacing="0" style="width: 190px;">
                                    <tr>
                                        <td colspan ="3" style="width:190px;">
                                            <asp:GridView ID="GVConsultaComunicado" runat="server"                 
                                                    Width="190px" 
                                                    ShowHeader="False"
                                                    CssClass="miGridviewBusquedaPersona" 
                                                    GridLines="None" 
                                                    AutoGenerateColumns="False"  
                                                    AllowPaging="false" 
                                                    AllowSorting="false"                   
                                                    EmptyDataText=" - No se encontraron resultados - "
                                                    OnRowDataBound="GVConsultaComunicado_RowDataBound"
                                                    >
                                                    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                    <Columns>            
                                                    
                                                    <asp:TemplateField >  
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCodigoComunicadoGrilla" runat="server" Text='<%# Bind("CodigoComunicado") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="miHiddenStyle"  HorizontalAlign="Left" Width="0px" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField >  
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNuevoGrilla" runat="server" Text='<%# Bind("Nuevo") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="miHiddenStyle"  HorizontalAlign="Left" Width="0px" />
                                                    </asp:TemplateField>
                                                       
                                                    <asp:TemplateField >  
                                                        <ItemTemplate >
                                                            <li><asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Descripcion") %>' /> 
                                                            </li>
                                                            <asp:Label ID="lblCodigoComAdj" visible="false"  runat="server" Text='<%# Bind("CodigoComunicado") %>' />
                                                             <asp:ImageButton ID="btnNuevo" runat="server"  
                                                                    ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/gif-new.gif" /> 
                                                            <asp:DataList ID="dlVersiones" runat="server" RepeatDirection="Horizontal"  >
                                                            <ItemTemplate>    
                                                                                                                   
                                                                <asp:LinkButton style="text-decoration:none;font-size:9px;color:#82868a " 
                                                                    ValidationGroup='<%# Bind("RutaAdjunto") %>'   ID="lb_Adjunto" 
                                                                    Text='<%# Bind("Idioma") %>'   runat="server" onclick="lb_Adjunto_Click">
                                                                    </asp:LinkButton>&nbsp;|&nbsp;
                                                            </ItemTemplate>                                                                                                                        
                                                            </asp:DataList>
                                                           
                                                            <br />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="miGridviewBusqueda_Rows"  HorizontalAlign="Left" Width="190px" />
                                                    </asp:TemplateField>
                                                                                                                                     
                                                </Columns>  
                                                               
                                            </asp:GridView>
                                        </td>
                                    </tr>
</table>         
  </ContentTemplate>  
  </asp:UpdatePanel>