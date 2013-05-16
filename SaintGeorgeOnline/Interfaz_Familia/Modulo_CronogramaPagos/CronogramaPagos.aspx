<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="CronogramaPagos.aspx.vb" Inherits="Interfaz_Familia_Modulo_CronogramaPagos_CronogramaPagos" title="Página sin título" %>
<%@ MasterType VirtualPath="~/Interfaz_Familia/Plantilla_Principal.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div id="miBusquedaActualizacion_Ficha">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional"  >
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 840px;">
              <tr>
                <td style="width:554px;height:20px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_cab.jpg');background-repeat:no-repeat;" >&nbsp;&nbsp;</td>
                <td style="width:286px;vertical-align:top;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_inferior3.jpg');background-repeat:repeat-y;" rowspan="2" >
                    <table border="0" cellpadding="0" cellspacing="0" style="width:286px;">
                        <tr>
                            <td style="width:36px;height:20px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_cab1.jpg');background-repeat:no-repeat;" >&nbsp;&nbsp;</td>
                            <td style="width:250px;height:20px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_cab2.jpg');background-repeat:no-repeat;" >&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_centro1.jpg');background-repeat:repeat-y;">&nbsp;&nbsp;</td>
                            <td style="padding-left:9px;vertical-align:top;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_centro2.jpg');background-repeat:repeat-y;">
                              <asp:DataList ID="dl_DatosAlumno" runat="server" RepeatDirection="Vertical"
                                                        OnItemCommand ="dl_DatosAlumno_ItemCommand"
                                                        OnItemDataBound="dl_DatosAlumno_ItemDataBound"> 
                                               <ItemStyle Width="230px" />                                                                                                                                       
                                               <ItemTemplate> 
                                               
                                                            <table runat="server" cellpadding="0" cellspacing="0" border="0" style="width: 230px;background-color:#17c4fc  ">
                                                                <tr>
                                                                    <td rowspan="4" style="padding-left:5px; width:50px; " valign="middle">                                                             
                                                                        <asp:Image ID="img_Foto_dl" runat="server" Width="40" Height="50" Style=" border: #7f9db9 1px solid"
                                            />
                                                                    </td>
                                                                    <td colspan ="2" style=" width:180px; height: 15px; text-align:left ; color:White; font-size:10px; " align="left" valign="bottom">                                                                                    
                                                                        <b> <asp:Label ID="lblNombre_dl" ForeColor="White"  runat="server" Font-Bold="true" Text='<%# Eval("NombreCompleto") %>'  /> </b>                                                                                       
                                                                    </td>
                                                                </tr>
                                                               <tr>
                                                                    <td style=" width:25px; height: 15px; text-align:left ; color:White; font-size:10px;" align="left" valign="bottom">                                                                                    
                                                                       <span >Grado:&nbsp;</span>                                                                   
                                                                    </td>
                                                                    <td style="width:155px; height: 15px; text-align:left ; color:White; font-size:10px;" align="left" valign="bottom">                                                                                    
                                                                       <asp:Label ID="lblGrado_dl" ForeColor="White"  runat="server" Font-Bold="true" Text='<%# Eval("GradoAcad") %>'  />                                                                                      
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:25px; height: 15px; text-align:left ; color:White; font-size:10px;" align="left" valign="bottom">                                                                                    
                                                                       <span >Sección:&nbsp;</span>                                                                   
                                                                    </td>
                                                                    <td style="width:155px; height: 15px; text-align:left ; color:White; font-size:10px; " align="left" valign="bottom">                                                                                    
                                                                        <asp:Label ID="lblSeccion_dl" ForeColor="White"  runat="server" Font-Bold="true" Text='<%# Eval("AulaAcad") %>'  />                                                                                       
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:25px; height: 15px; text-align:left ; color:White; font-size:10px;" align="left" valign="bottom">                                                                                    
                                                                                                                                     
                                                                    </td>
                                                                    <td style=" padding-right:5px;  width :155px; height: 15px; text-align:right  ; color:Black; font-size:10px; " align="right" valign="bottom">                                                                                    
                                                                       <asp:Label ID="lblCodigoAlumno_dl" runat="server" style="display: none;" Font-Bold="true" Text='<%# Eval("CodigoAlumno")%> '  />       
                                                                       <span >Ver</span> 
                                                                       <asp:ImageButton ID="btnVer_dl" runat="server" Width="15px"  
                                                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/Ver_selected.png"
                                                                        CommandName="Ver" 
                                                                        CommandArgument='<%# Bind("CodigoAlumno") %>' 
                                                                        ToolTip="Ver Cronograma de Pagos"/>  
                                                                       
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            
                                                            <table>                                                                
                                                                <tr>
                                                                <td colspan ="3" style ="background-color :White ; height :3px;">
                                                                </td>
                                                                </tr>
                                                            </table>
                                                        
                                               </ItemTemplate>
                                            </asp:DataList>
                                     
                            </td>
                        </tr>
                        <tr>
                            <td style="height:23px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_pie1.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td>
                            <td style="height:23px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_pie2.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td>
                        </tr>                
                    </table>
                </td>
             </tr>
             <tr>
                <td style="height:250px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_centro.jpg');background-repeat:repeat-y;">
                     <table cellpadding="0" cellspacing="0" border="0" style="padding-left :15px; width: 540px; ">
                                <tr>
                                    <td rowspan="3" style=" width :20px;  text-align:center  ;  " align="center" valign="middle">                                                                                    
                                        <asp:ImageButton ID="btnVer" runat="server" Width="15px"  
                                                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/Ver_selected.png"
                                                                        ToolTip="Ver Cronograma de Pagos"/>   
                                    </td>
                                     <td rowspan="3" style=" width :50px;  text-align:center; " align="center" valign="middle">                                                                                    
                                        <asp:Image ID="img_Foto" runat="server"  Width="40" Height="50" Style="border: #7f9db9 1px solid"
                                            ImageUrl="~/Fotos/noPhoto.gif" />
                                    </td>
                                     <td style=" width :50px;  text-align:left; color:Black; font-size:10px; " align="right" valign="middle">                                                                                    
                                        <span>Alumno:</span>
                                     </td>
                                    <td style=" width :420px;  text-align:left; color:Black; font-size:10px; " align="left" valign="middle"> 
                                       <asp:Label ID="lblCodigoAlumno"  runat="server" style="display: none;" />                                                                                      
                                       <asp:Label ID="lblNombre" ForeColor="Black"  runat="server" Font-Bold="true" Text=''  />                                                                                       
                                     </td>
                                </tr>
                                <tr>
                                    <td style=" width :50px;  text-align:left  ; color:Black; font-size:10px; " align="right" valign="middle">                                                                                   
                                        <span>Grado:</span>
                                     </td>
                                    <td style=" width :420px;  text-align:left  ; color:Black; font-size:10px; " align="left" valign="middle">                                                                                       
                                       <asp:Label ID="lblGrado"  runat="server"  Text=''  />                                                                                       
                                    </td>
                                </tr>
                                <tr>
                                     <td style=" width :50px;  text-align:left  ; color:Black; font-size:10px; " align="right" valign="middle">                                                                                    
                                        <span>Sección:</span>
                                     </td>
                                    <td style=" width :420px;  text-align:left  ; color:Black; font-size:10px; " align="left" valign="middle">                                                                   
                                       <asp:Label ID="lblSeccion"  runat="server"  Text=''  />                                                                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan ="4" style=" width :540px; height :10px; ">
                                    <div style="BORDER-TOP: #6fa4d4 1px solid;width:520px"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan ="4" valign ="top"  style=" width :540px; height :400px; ">
                                    
<div style="border: solid 1px #a6a3a3; margin: 0; padding:0; width: 520px;">    
    <table cellpadding="0" cellspacing="0" border="0" style="width: 520px; height: 26px; color:White; background-color: #555555; font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">                              
        <tr>
            <td style="width:  160px; height: 26px;" align="center" valign="middle">
                <span>Concepto</span>                                                                 
            </td>
            <td style="width:  70px; height: 26px;" align="center" valign="middle">
                <span>Monto</span>                                                                 
            </td>
            <td style="width:  90px; height: 26px;" align="center" valign="middle">
                <span>Fecha de Emisión</span>                                                                 
            </td>
            <td style="width:  90px; height: 26px;" align="center" valign="middle">
                <span>Fecha de Vencimiento</span>                                                                 
            </td>
            <td style="width:  90px; height: 26px;" align="center" valign="middle">
                <span>Estado</span>                                       
            </td>
            <td style="width:  20px; height: 26px;" align="center" valign="middle">   
            </td>                      
        </tr>
    </table>      
</div> 
<div id="miGridviewMant" style="overflow-y: scroll; overflow-x: hidden; width: 520px; height: 295px; margin: 0; padding: 0; background-color: #ffffff;">   
    <asp:GridView ID="GridView1" runat="server"
        CssClass="miGridviewBusqueda"
        Width="500px" 
        GridLines="None" 
        AutoGenerateColumns="false" 
        AllowPaging="false" 
        AllowSorting="false"
        ShowFooter="false" 
        ShowHeader="false" 
        EmptyDataText=" - No se encontraron resultados - ">
    <%-- OnRowCommand="GridView1_RowCommand"--%>
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
    <Columns>                 
    <asp:TemplateField HeaderText="Concepto" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="160px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="160px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescComptoCobro" runat="server" Text='<%# Bind("DescConceptoCobro") %>' style="padding-left:20px;" />
                                </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Monto" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="70px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label ID="lbMonto" runat="server" Text='<%# Bind("Monto") %>' />
                                </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Fecha Emision" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label ID="lbFechaEmisi" runat="server" Text='<%# Bind("FechaEmisionStr") %>' />
                                </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Fecha Vencimiento" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label ID="lbFechaVencim" runat="server" Text='<%# Bind("FechaVencimientoStr") %>' />
                                </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label ID="lbEstado" runat="server" Text='<%# Bind("Estado") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
    </Columns>
    </asp:GridView>
</div> 

                                    </td>
                                </tr>
                               </table> 
                </td>
             </tr>
             <tr>
                <td style="height:30px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_inferior.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td>
                <td style="background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_inferior2.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td>
             </tr>
         </table>
            </ContentTemplate>    
        </asp:UpdatePanel>
    </div>
   
</asp:Content>

