<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="WeeklyReport.aspx.vb" Inherits="Interfaz_Familia_Modulo_Seguimiento_WeeklyReport" title="Página sin título" %>

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

//    function MostrarImpresionFichaAtencion_html() {

//        window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FichaAtencion_html.aspx', '_blank', '');
//    }
      
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miBusquedaActualizacion_Ficha">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional"  >
                <Triggers>
                    <asp:PostBackTrigger ControlID="GridView1" /> 
                </Triggers>
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
                                       <asp:Label ID="lblCodigoGrado"  runat="server" style="display: none;" /> 
                                       <asp:Label ID="lblCodigoSeccion"  runat="server" style="display: none;" />                                                                                   
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
                         <asp:BoundField DataField="CodigoBimestre" HeaderText="CodigoBimestre" ItemStyle-Width="0" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" />                                
                        <asp:BoundField DataField="CodigoSemanaAcademica" HeaderText="CodigoSemanaAcademica" ItemStyle-Width="0" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" />                                
                        
                           <%-- <asp:TemplateField>
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblCodigoBimestre" runat="server" Text='<%# Bind("CodigoBimestre") %>' />                                                                                    
                                </ItemTemplate>
                                <HeaderStyle CssClass="miHiddenStyle" Width="0px" />
                                <ItemStyle CssClass="miHiddenStyle" Width="0px" />
                            </asp:TemplateField> 
                            <asp:TemplateField>
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblCodigoSemanaAcademica" runat="server" Text='<%# Bind("CodigoSemanaAcademica") %>' />                                                                                    
                                </ItemTemplate>
                                <HeaderStyle CssClass="miHiddenStyle" Width="0px" />
                                <ItemStyle CssClass="miHiddenStyle" Width="0px" />
                            </asp:TemplateField>  --%>        
                            <asp:TemplateField HeaderText="Bimestre">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:65px;" align="right" valign="middle">Bimestre&nbsp;</td>
                                            <td style="width:35px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Bimestre" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Bimestre"/></td>
                                        </tr>
                                    </table>                                    
                                </HeaderTemplate>      
                                <ItemTemplate>
                                    <asp:Label ID="lblBimestre" runat="server" Text='<%# Bind("Bimestre") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Semana">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:65px;" align="right" valign="middle">Semana&nbsp;</td>
                                            <td style="width:35px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Semana" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Semana"/></td>
                                        </tr>
                                    </table>                                    
                                </HeaderTemplate>      
                                <ItemTemplate>
                                    <asp:Label ID="lblSemana" runat="server" Text='<%# Bind("Semana") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnVer" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_ver.png" 
                                        CommandName="Visualizar" CommandArgument='<%# Bind("CodigoAlumnoWeekly") %>' ToolTip="Ver Registro" />
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
                                                        
                                                            <img alt="" src="~/App_Themes/Imagenes/bigrotation2.gif" 
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
                                        PopupControlID="pnlImpresion"                    
                                        TargetControlID="lblAccionExportar"
                                        >
                                        </atk:ModalPopupExtender>     
                                        
                <asp:Label ID="lblAccionExportar" runat="server" ForeColor="White" Text="..."></asp:Label>
            </ContentTemplate>    
        </asp:UpdatePanel>
    </div>

</asp:Content>

