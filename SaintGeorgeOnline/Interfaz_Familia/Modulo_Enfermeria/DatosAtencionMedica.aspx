<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="DatosAtencionMedica.aspx.vb" Inherits="Interfaz_Familia_Modulo_Enfermeria_DatosAtencionMedica" title="Página sin título" %>

<%@ MasterType VirtualPath="/SaintGeorgeOnline/Interfaz_Familia/Plantilla_Principal.master" %>

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
                                               
                                                            <table id="Table1" runat="server" cellpadding="0" cellspacing="0" border="0" style="width: 230px;background-color:#17c4fc  ">
                                                                <tr>
                                                                    <td rowspan="4" style="padding-left:5px; width:50px; " valign="middle">                                                             
                                                                        <asp:Image ID="img_Foto_dl" runat="server" Width="40" Height="50" Style=" border: #7f9db9 1px solid" />
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
                                       <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                        
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
                                     <div id="miGridviewMant" style=" width: 500px; ">
                                   <asp:GridView ID="GridView1" runat="server" Width="500px" CssClass="miGridviewBusqueda"
                                    GridLines="None" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="true"
                                    PageSize="10" ShowFooter="false" ShowHeader="true" 
                                    EmptyDataText=" - No se encontraron resultados - "
                                    OnPageIndexChanging="GridView1_PageIndexChanging" 
                                    OnRowDataBound="GridView1_RowDataBound"
                                    OnRowCommand="GridView1_RowCommand"
                                    OnRowCreated="GridView1_RowCreated"
                                    OnSorting="GridView1_Sorting">
                      <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White"
                            HorizontalAlign="Center" />
                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                        <Columns>                 
                            <asp:TemplateField HeaderText="Fecha de Registro">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:65px;" align="right" valign="middle">Fecha de Registro&nbsp;</td>
                                            <td style="width:35px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_FechaRegistro" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="FechaRegistro"/></td>
                                        </tr>
                                    </table>                                    
                                </HeaderTemplate>      
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaAtencion" runat="server" Text='<%# Bind("FechaAtencion") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hora de Ingreso">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:65px;" align="right" valign="middle">Hora de Ingreso&nbsp;</td>
                                            <td style="width:35px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_HoraIngreso" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="HoraIngreso"/></td>
                                        </tr>
                                    </table>                                    
                                </HeaderTemplate>    
                                <ItemTemplate>
                                    <asp:Label ID="lbHoraIngreso" runat="server" Text='<%# Bind("HoraIngreso") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hora Salida">
                                 <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:30px;" align="right" valign="middle">Hora de Salida&nbsp;</td>
                                            <td style="width:30px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_HoraSalida" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="HoraSalida"/></td>
                                        </tr>
                                    </table>                                    
                                </HeaderTemplate>    
                                <ItemTemplate>
                                    <asp:Label ID="lbHoraSalida" runat="server" Text='<%# Bind("HoraSalida") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Diagnostico">
                              <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:30px;" align="right" valign="middle">Diagnóstico&nbsp;</td>
                                            <td style="width:30px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Diagnostico" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Diagnostico"/></td>
                                        </tr>
                                    </table>                                    
                                </HeaderTemplate>  
                                <ItemTemplate>
                                    <asp:Label ID="lbDiagnostico" runat="server" Text='<%# Bind("Diagnostico") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnVer" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_ver.png" 
                                        CommandName="Visualizar" CommandArgument='<%# Bind("CodigoFichaAtencion") %>' ToolTip="Ver Registro" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>  
                        </Columns>
                        <PagerTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 500px;">
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
               
                <asp:Panel ID="pnlImpresion" runat="server" BackColor="White" Height="340px" Width="600px"  style="display:none">
                                            <table style=" Width:600px; margin-left:0; margin-top:0;" border="0" cellpadding="0" cellspacing="0"  >
                                                <tr  id="ImprimirHeader" style =" background-color:Black; cursor :pointer; ">
                                                        <td style="width: 580px; height: 26px" align="left" valign="middle" class="" >                
                                                            <b><span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;"></span></asp:Label></b>

                                                        </td>
                                                        <td style="width: 20px; height: 26px" align="right" valign="middle" class="">
                                                            <asp:ImageButton ID="btnCerrarModalDocumento" runat="server" Width="16" Height="15"
                                                                ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                                                                ToolTip="Cerrar Panel"/>
                                                        </td>
                                                    </tr>
                                                   <%-- <tr>
                                                       <td colspan="2" style="height: 15px;" align="right">
                                                       </td>
                                                </tr>   --%>
                                                <tr>
                                                  
                                                        <td colspan="2" style=" width:600; font-family: Arial, Helvetica, sans-serif; font-size: 10px; color: #000080; text-align: center;">
                                                        
                                                                  <div  id ="panelFA" runat ="server" style ="padding-left:3px; width:600; " >
                                                                  </div>
                                                                                                
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
                                        PopupDragHandleControlID="ImprimirHeader"
                                        >
                                        </atk:ModalPopupExtender>
                                        
                                        
               <%-- DropShadow="True"   <atk:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                        TargetControlID="VerAgregarRegistroDocumento"
                        PopupControlID="pnlAgregarDocumento"
                        BackgroundCssClass="MiModalBackground" 
                        DropShadow="true" 
                        OkControlID="OKAgregarRegistroDocumento" 
                        CancelControlID="CancelAgregarRegistroDocumento"
                        Drag="true" 
                        PopupDragHandleControlID="AgregarDocumentoRegistroHeader" />           

             <asp:panel id="pnlAgregarDocumento" BackColor="White" BorderColor="Black" runat="server" >
                          <table cellpadding="0" cellspacing="0" border="0" style ="width:600px; height :600px"  id="panelRegistroDocumento">    
                            <tr  id="AgregarDocumentoRegistroHeader" style ="cursor :pointer; ">
                                <td style="width: 580px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="3">                
                                    <b><span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Agregar Archivos</span></asp:Label></b>

                                </td>
                                <td style="width: 20px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                                    <asp:ImageButton ID="btnCerrarModalDocumento" runat="server" Width="16" Height="15"
                                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                                         ToolTip="Cerrar Panel"/>
                                </td>
                            </tr>
                            <tr>
                               <td colspan="4" style="height: 15px;" align="right">
                                      
                               </td>
                            </tr>   
                             
                            <tr>
                                <td colspan="4" align="left" style="width:600px; height: 25px;"  valign="middle">
                                     <div  id ="panelFA" runat ="server" style ="width:600; height:600;" >
                                                                  </div>
                                </td>
                                                               
                             </tr>         
                                      
                        </table>  
                          <div id="Div1" style="display:none">
                                <input type="button" id="VerAgregarRegistroDocumento" runat="server" />
                                <input type="button" id="OKAgregarRegistroDocumento" />
                                <input type="button" id="CancelAgregarRegistroDocumento" />
                          </div>       
                        </asp:panel>     --%>                   
                                        
                <asp:Label ID="lblAccionExportar" runat="server" ForeColor="White" Text="..."></asp:Label>
            </ContentTemplate>    
        </asp:UpdatePanel>
    </div>

</asp:Content>

