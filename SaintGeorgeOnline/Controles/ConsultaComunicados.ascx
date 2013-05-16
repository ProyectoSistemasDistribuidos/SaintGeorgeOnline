<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ConsultaComunicados.ascx.vb" Inherits="Controles_ConsultaComunicados" %>


<div id="miBusquedaActualizacion_Ficha">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
 
    <Triggers>
        <asp:PostBackTrigger ControlID="GVConsultaComunicado" />
    </Triggers>
    
    <ContentTemplate>
        <div id="miContainerMantenimiento">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 840px;">
                    <tr>
                        <td align="left" valign ="middle">
                        <asp:HiddenField ID="hiddenAnio" runat ="server" />
                        <asp:HiddenField ID="hiddenPersona" runat ="server" />
                        <asp:HiddenField ID="hiddenTipoPersona" runat ="server" />
                        
                            <asp:ImageButton ID="btnMes01" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes01.gif"
                                            onclick="btnMes01_Click" ToolTip="Enero"/>&nbsp;&nbsp;
                                            
                           <asp:ImageButton ID="btnMes02" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes02.gif"
                                            onclick="btnMes02_Click" ToolTip="Febrero"/>&nbsp;&nbsp;
                                            
                           <asp:ImageButton ID="btnMes03" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes03.gif"
                                            onclick="btnMes03_Click" ToolTip="Marzo"/>&nbsp; &nbsp;
                                            
                            <asp:ImageButton ID="btnMes04" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes04.gif"
                                            onclick="btnMes04_Click" ToolTip="Abril"/>&nbsp;&nbsp; 
                                            
                            <asp:ImageButton ID="btnMes05" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes05.gif"
                                            onclick="btnMes05_Click" ToolTip="Mayo"/>&nbsp;&nbsp; 
                                            
                            <asp:ImageButton ID="btnMes06" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes06.gif"
                                            onclick="btnMes06_Click" ToolTip="Junio"/>&nbsp;&nbsp; 
                                            
                            <asp:ImageButton ID="btnMes07" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes07.gif"
                                            onclick="btnMes07_Click" ToolTip="Julio"/>&nbsp;&nbsp;
                                            
                           <asp:ImageButton ID="btnMes08" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes08.gif"
                                            onclick="btnMes08_Click" ToolTip="Agosto"/>&nbsp;&nbsp; 
                                            
                           <asp:ImageButton ID="btnMes09" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes09.gif"
                                            onclick="btnMes09_Click" ToolTip="Setiembre"/>&nbsp;&nbsp;  
                                            
                        <asp:ImageButton ID="btnMes10" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes10.gif"
                                            onclick="btnMes10_Click" ToolTip="Octubre"/>&nbsp;&nbsp; 
                                            
                         <asp:ImageButton ID="btnMes11" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes11.gif"
                                            onclick="btnMes11_Click" ToolTip="Noviembre"/>&nbsp;&nbsp; 
                                            
                           <asp:ImageButton ID="btnMes12" runat="server" Width="45" Height="39" ImageUrl="~/App_Themes/Imagenes/BotonesMeses/mes12.gif"
                                            onclick="btnMes12_Click" ToolTip="Diciembre"/> 
                                            
                        </td>
                    </tr>
                    <tr>
                         <td style ="height :20px;font-size:2px; ">
                             &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:640px;">
                             
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 644px;height:25px;background-color:#e6e6e6;">
                                  <%-- nuevo --%>
                                   
                                   <tr>
                                        <td style="width:370px;" align="right" valign ="middle">                                            
                                            <asp:LinkButton ID="btnPrev" runat="server" Font-Bold="true" Font-Size="14px"  style="text-decoration:none;color:#34b9e7; "><<</asp:LinkButton>
                                        </td >
                                        <td style="font-size:16px; width:100px;" align="center" valign ="middle">
                                             <b><asp:Label ID="lblAnio"  runat="server" style="color:#019cd2" /></b>  
                                              <asp:Label ID="Label2"  runat="server" style="display: none;" />  
                                        </td>
                                        <td style="width:370px;" align="left" valign ="middle">
                                            
                                            <asp:LinkButton ID="btnNext" runat="server" Font-Bold="true" Font-Size="14px"  style="text-decoration:none;color:#34b9e7; ">>></asp:LinkButton>                                        
                                        </td>
                                    </tr>
                                   <%--fin--%> 
                                    <tr>
                                        <td style="width:370px;" align="right" valign ="middle">                                            
                                            <asp:LinkButton ID="btnAtras" runat="server" Font-Bold="true" Font-Size="14px"  style="text-decoration:none;color:#34b9e7; "><<</asp:LinkButton>
                                        </td >
                                        <td style="font-size:16px; width:100px;" align="center" valign ="middle">
                                             <b><asp:Label ID="lblNombreMesGV"  runat="server" style="color:#019cd2" /></b>  
                                              <asp:Label ID="lblCodigoMesGV"  runat="server" style="display: none;" />  
                                        </td>
                                        <td style="width:370px;" align="left" valign ="middle">
                                            
                                            <asp:LinkButton ID="btnAvanzar" runat="server" Font-Bold="true" Font-Size="14px"  style="text-decoration:none;color:#34b9e7; ">>></asp:LinkButton>                                        
                                        </td>
                                    </tr>
                                    
                                </table>
                                <div style="border:2px;border-color:#1ac4ff;border-style:solid;width:640px  "> 
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 640px;">
                                    <tr>
                                        <td style="width:640px;">
                                            <asp:GridView ID="GVConsultaComunicado" runat="server"                 
                                                    Width="640px" 
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
                                                        <ItemTemplate >
                                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Descripcion") %>' />
                                                            <asp:Label ID="lblCodigoComAdj" visible="false"  runat="server" Text='<%# Bind("CodigoComunicado") %>' />
                                                            <br>
                                                            <asp:DataList ID="dlVersiones" runat="server" RepeatDirection="Horizontal"  >
                                                            <ItemTemplate>    
                                                                <asp:Image ID="img_link" Width="10px" Height="10px" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_ver.png"  />                                                            
                                                                <asp:LinkButton style="text-decoration:none;" 
                                                                    ValidationGroup='<%# Bind("RutaAdjunto") %>'   ID="lb_Adjunto" 
                                                                    Text='<%# Bind("Idioma") %>'   runat="server" onclick="lb_Adjunto_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                            </ItemTemplate>                                                            
                                                            </asp:DataList>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="miGridviewBusqueda_Rows"  HorizontalAlign="Left" Width="810px" />
                                                    </asp:TemplateField>
                                                                                                                                     
                                                </Columns>  
                                                               
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>                
                                                                                
                             </div>
                        </td>
                    </tr>
                 
         </table>
        </div>
     </ContentTemplate>    
  </asp:UpdatePanel>
</div>
