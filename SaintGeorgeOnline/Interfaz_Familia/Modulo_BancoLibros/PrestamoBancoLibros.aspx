<%@ Page Title="" Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="PrestamoBancoLibros.aspx.vb" Inherits="Interfaz_Familia_Modulo_BancoLibros_PrestamoBancoLibros" %>
<%@ MasterType VirtualPath="~/Interfaz_Familia/Plantilla_Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <script type="text/javascript" >

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            imagePreview();
        }
    }
    
</script>

<style type="text/css">
    #preview{
	    position:absolute;
	    border:1px solid #ccc;
	    background:#333;
	    padding:5px;
	    display:none;
	    color:#fff;
	}	      
	   
</style>
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
                                                                        ToolTip="Ver libros Prestados"/>  
                                                                       
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
                                    <td colspan ="4" style=" width :540px; height :10px; ">
                                    <span style="text-align:left ; color:Black; font-size:11px;">Libros Prestados en el Año Académico </span><asp:Label style="text-align:left  ; color:Black; font-size:11px;" id="lblAnioActualLP" runat ="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <span style="text-align:right  ; color:Black; font-size:11px;">Cantidad (<asp:Label style="text-align:left  ; color:Black; font-size:11px;" id="lblCantidadAnioActual" runat ="server" />) </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan ="4" style=" width :540px; height :10px; ">
                                    <div style="BORDER-TOP: #6fa4d4 1px solid;width:520px"></div>
                                    </td>
                                </tr>
                                <tr>                                
                                <tr>
                                    <td colspan ="4" valign ="top"  style=" width :540px; height :180px; ">
<div id="miGridviewMantActualizacion_Ficha" style="width: 520px; height: 26px; margin: 0; padding: 0; border-bottom: 0;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 520px; height: 26px; color:White; background-color: #0a0f14; 
            font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;" >
       <tr>
            <td style="width:  220px; height: 26px;" align="center" valign="middle">
                <span>Libro</span>                                                                 
            </td>
            <td style="width:  80px; height: 26px;" align="center" valign="middle">
                <span>Tipo de Libro</span>                                                                 
            </td>
            <td style="width:  80px; height: 26px;" align="center" valign="middle">
                <span>Curso</span>                                                                 
            </td>
            <td style="width:  60px; height: 26px;" align="center" valign="middle">
                <span>Estado</span>                                                                 
            </td>
          <%--  <td style="width:  80px; height: 26px;" align="center" valign="middle">
                <span>Fecha de Prestamo</span>                                       
            </td>--%>
            <td style="width:  80px; height: 26px;" align="left" valign="middle">   
                 <span>Precio de Reposición</span>   
            </td>                                
        </tr>
    </table>      
</div> 

<div id="miGridviewMant" style="overflow-y: scroll; overflow-x: hidden; width: 520px; height: 150px; margin: 0; padding: 0;">   
    <asp:GridView ID="GridView1" runat="server"
        CssClass="miGridviewBusqueda"
        Width="500px" 
        GridLines="None" 
        AutoGenerateColumns="false" 
        AllowPaging="false" 
        AllowSorting="false"
        ShowFooter="false" 
        ShowHeader="false"
        OnRowDataBound="GridView1_RowDataBound"
        EmptyDataText=" - No se encontraron resultados - ">
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
    <Columns>  
     <asp:TemplateField>
            <ItemTemplate>                                    
                <asp:Label ID="lblRutaFoto" runat="server" Text='<%# Bind("RutaPortada") %>' />                                                                                    
            </ItemTemplate>
            <HeaderStyle CssClass="miHiddenStyle" Width="0px" />
            <ItemStyle CssClass="miHiddenStyle" Width="0px" />
        </asp:TemplateField>
       <asp:TemplateField >                                                                      
            <ItemTemplate>
                <asp:Label ID="lblCodigoLibro" runat="server" Text='<%# Bind("CodigoLibro") %>' />
            </ItemTemplate>
            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
        </asp:TemplateField>
      <asp:TemplateField>
      
        <ItemTemplate>
            <a onclick ="return false;" class="preview" id="btnVerPortada" runat="server" >
                <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
        
        </ItemTemplate>
        <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="25px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Libro">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 60px;" align="center" valign="middle">
                                                Libro&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbLibro" runat="server" Text='<%# Bind("Libro") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="210px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="210px" />
    </asp:TemplateField> 
    
    <asp:TemplateField HeaderText="Tipo de Libro">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 60px;" align="center" valign="middle">
                                                Tipo de Libro&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbTipoLibro" runat="server" Text='<%# Bind("TipoLibro") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="90px" />
    </asp:TemplateField>              
    <asp:TemplateField HeaderText="Curso">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 100px;" align="center" valign="middle">
                                                Curso&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbCurso" runat="server" Text='<%# Bind("Curso") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="70px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Estado">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 100px;" align="center" valign="middle">
                                                Estado&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbEstado" runat="server" Text='<%# Bind("Estado") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="70px" />
    </asp:TemplateField>
    <%--<asp:TemplateField HeaderText="Fecha ">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 60px;" align="center" valign="middle">
                                                Fecha&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbFecha" runat="server" Text='<%# Bind("Fecha") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>--%>
     <asp:TemplateField HeaderText="Precio ">
                               <ItemTemplate>
                                    <asp:Label ID="lbPrecio" runat="server" Text='<%# Bind("PrecioReposicion") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
    </Columns>
    </asp:GridView>
</div> 
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan ="4" style=" width :540px; height :10px; ">
                                    <div style="BORDER-TOP: #6fa4d4 1px solid;width:520px"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan ="4" style=" width :540px; height :10px; ">
                                    <span style="text-align:left  ; color:Black; font-size:11px;">Libros Pendientes en el Año Académico </span><asp:Label style="text-align:left  ; color:Black; font-size:11px;" id="lblAnioAnteriorLP" runat ="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <span style="text-align:left  ; color:Black; font-size:11px;">Cantidad ( <asp:Label style="text-align:left  ; color:Black; font-size:11px;" id="lblCantidadAnioAnterior" runat ="server" />)</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan ="4" style=" width :540px; height :10px; ">
                                    <div style="BORDER-TOP: #6fa4d4 1px solid;width:520px"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan ="4" valign ="top"  style=" width :540px; height :180px; ">
<div id="miGridviewMantActualizacion_Ficha" style="width: 520px; height: 26px; margin: 0; padding: 0; border-bottom: 0;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 520px; height: 26px; color:White; background-color: #0a0f14; 
            font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;" >
        <tr>
            <td style="width:  220px; height: 26px;" align="center" valign="middle">
                <span>Libro</span>                                                                 
            </td>
            <td style="width:  80px; height: 26px;" align="center" valign="middle">
                <span>Tipo de Libro</span>                                                                 
            </td>
            <td style="width:  80px; height: 26px;" align="center" valign="middle">
                <span>Curso</span>                                                                 
            </td>
            <td style="width:  60px; height: 26px;" align="center" valign="middle">
                <span>Estado</span>                                                                 
            </td>
            <%--<td style="width:  80px; height: 26px;" align="center" valign="middle">
                <span>Fecha de Prestamo</span>                                       
            </td>--%>
            <td style="width:  80px; height: 26px;" align="left" valign="middle">   
                 <span>Precio de Reposición</span>   
            </td>                                
        </tr>
    </table>      
</div> 

<div id="miGridviewMant" style="overflow-y: scroll; overflow-x: hidden; width: 520px; height: 150px; margin: 0; padding: 0;">   
    <asp:GridView ID="GridView2" runat="server"
        CssClass="miGridviewBusqueda"
        Width="500px" 
        GridLines="None" 
        AutoGenerateColumns="false" 
        AllowPaging="false" 
        AllowSorting="false"
        ShowFooter="false" 
        ShowHeader="false" 
        OnRowDataBound="GridView2_RowDataBound"
        EmptyDataText=" - No se encontraron resultados - ">
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
    <Columns>    
    <asp:TemplateField>
            <ItemTemplate>                                    
                <asp:Label ID="lblRutaFoto2" runat="server" Text='<%# Bind("RutaPortada") %>' />                                                                                    
            </ItemTemplate>
            <HeaderStyle CssClass="miHiddenStyle" Width="0px" />
            <ItemStyle CssClass="miHiddenStyle" Width="0px" />
        </asp:TemplateField>
       <asp:TemplateField >                                                                      
            <ItemTemplate>
                <asp:Label ID="lblCodigoLibro2" runat="server" Text='<%# Bind("CodigoLibro") %>' />
            </ItemTemplate>
            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
        </asp:TemplateField>    
         <asp:TemplateField>
      
        <ItemTemplate>
            <a onclick ="return false;" class="preview" id="btnVerPortada2" runat="server" >
                <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
        
        </ItemTemplate>
        <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="25px" />
    </asp:TemplateField>         
    <asp:TemplateField HeaderText="Libro">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 70px;" align="center" valign="middle">
                                                Libro&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbLibro2" runat="server" Text='<%# Bind("Libro") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="210px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="210px" />
    </asp:TemplateField> 
    <asp:TemplateField HeaderText="Tipo de Libro">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 50px;" align="center" valign="middle">
                                                Tipo de Libro&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbTipoLibro2" runat="server" Text='<%# Bind("TipoLibro") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="90px" />
    </asp:TemplateField>              
    <asp:TemplateField HeaderText="Curso">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 100px;" align="center" valign="middle">
                                                Curso&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbCurso2" runat="server" Text='<%# Bind("Curso") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="70px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Estado">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 100px;" align="center" valign="middle">
                                                Estado&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbEstado2" runat="server" Text='<%# Bind("Estado") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="70px" />
    </asp:TemplateField>
    <%--<asp:TemplateField HeaderText="Fecha ">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 60px;" align="center" valign="middle">
                                                Fecha&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbFecha2" runat="server" Text='<%# Bind("Fecha") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>--%>
                              <asp:TemplateField HeaderText="Precio ">
                               <ItemTemplate>
                                    <asp:Label ID="lbPrecio2" runat="server" Text='<%# Bind("PrecioReposicion") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="60px" />
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

