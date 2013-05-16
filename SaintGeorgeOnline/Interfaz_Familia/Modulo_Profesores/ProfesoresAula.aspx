<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="ProfesoresAula.aspx.vb" Inherits="Interfaz_Familia_Modulo_Profesores_ProfesoresAula" title="Página sin título" %>
<%@ MasterType VirtualPath="~/Interfaz_Familia/Plantilla_Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="miBusquedaActualizacion_Ficha">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional"  >
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 840px;">
              <tr>
                <td style="width:554px; height:20px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_cab.jpg');background-repeat:no-repeat;" >&nbsp;&nbsp;</td>
                <td style="width:286px; vertical-align:top;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_inferior3.jpg');background-repeat:repeat-y;" rowspan="2" >
                    <table border="0" cellpadding="0" cellspacing="0" style="width:286px;">
                        <tr>
                            <td style="width:36px; height:20px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_cab1.jpg');background-repeat:no-repeat;" >&nbsp;&nbsp;</td>
                            <td style="width:250px;height:20px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_cab2.jpg');background-repeat:no-repeat;" >&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:36px; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_centro1.jpg');background-repeat:repeat-y;">&nbsp;&nbsp;</td>
                            <td style="width:250px; padding-left:9px;vertical-align:top;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_centro2.jpg');background-repeat:repeat-y;">
                              <asp:DataList ID="dl_DatosAlumno" runat="server" RepeatDirection="Vertical"
                                                        OnItemCommand ="dl_DatosAlumno_ItemCommand"
                                                        OnItemDataBound="dl_DatosAlumno_ItemDataBound"> 
                                               <ItemStyle Width="230px" />                                                                                                                                       
                                               <ItemTemplate> 
                                                       <table id="Contenedor_Hijo" runat="server"  cellpadding="0" cellspacing="0" border="0" style="width: 230px;background-color:#17c4fc  ">
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
                                                  
                                                       <table cellpadding="0" cellspacing="0" border="0" style="width: 230px; ">
                                                                <tr>
                                                                    <td style ="background-color :White ; height :1px;font-size:2px; ">
                                                                    &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        
                                               </ItemTemplate>
                                            </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:36px; height:23px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_pie1.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td>
                            <td style="width:250px; height:23px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_pie2.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td>
                        </tr>                
                    </table>
                </td>
             </tr>
             <tr>
                <td  style="width: 554px; height:250px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_centro.jpg');background-repeat:repeat-y;">
                     <table cellpadding="0" cellspacing="0" border="0" style="padding-left :15px; width: 554px; ">
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
                                    <td style=" width :434px;  text-align:left; color:Black; font-size:10px; " align="left" valign="middle"> 
                                       <asp:Label ID="lblCodigoAlumno"  runat="server" style="display: none;" />    
                                        <asp:Label ID="lblCodigoGrado"  runat="server" style="display: none;" />
                                        <asp:Label ID="lblCodigoAula"  runat="server" style="display: none;" />                                                                                  
                                       <asp:Label ID="lblNombre" ForeColor="Black"  runat="server" Font-Bold="true" Text=''  />                                                                                       
                                     </td>
                                </tr>
                                <tr>
                                    <td style=" width :50px;  text-align:left; color:Black; font-size:10px; " align="right" valign="middle">                                                                                  
                                        <span>Grado:</span>
                                     </td>
                                    <td style=" width :434px;  text-align:left  ; color:Black; font-size:10px; " align="left" valign="middle">                                                                                         
                                       <div style="width: 434px;"  >
                                        <asp:Label ID="lblGrado"  runat="server"  Text=''  />  
                                       </div>                                                                                     
                                    </td>
                                </tr>
                                <tr>
                                     <td style=" width :50px;  text-align:left  ; color:Black; font-size:10px; " align="right" valign="middle">                                                                                    
                                        <span>Sección:</span>
                                     </td>
                                    <td style=" width :434px;  text-align:left  ; color:Black; font-size:10px; " align="left" valign="middle">                                                                   
                                       <asp:Label ID="lblSeccion"  runat="server"  Text=''  />                                                                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan ="4" style=" width :554px; height :10px; ">
                                    <div style="BORDER-TOP: #6fa4d4 1px solid;width:520px"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan ="4" valign ="top"  style=" width :554px; height :400px;  ">
                                        <center>
                                        <asp:DataList ID="dl_CompanierosAula" runat="server" RepeatDirection="Vertical"
                                                       OnItemDataBound="dl_CompanierosAula_ItemDataBound"  RepeatColumns="2" > 
                                               <ItemStyle Width="250px"/>                                                                                                                                       
                                               <ItemTemplate> 
                                                        <table>
                                                          <tr>
                                                            <td>
                                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 250px;background-color:#d8ce35;border:1px;border-color:#79742c;border-style:solid ;     ">
                                                                <tr>
                                                                    <td rowspan="5" style="padding-left:5px; width:60px; Height:60; "   valign="middle">                                                             
                                                                        <asp:Image ID="img_Foto_dlCompanierosAula" runat="server" Width="40" Height="50" Style=" border: #7f9db9 1px solid" />
                                                                    </td>
                                                                    <td colspan ="2" style=" width:180px; height: 15px; text-align:left ; color:White; font-size:10px; " align="left" valign="bottom">                                                                                    
                                                                        <b> <asp:Label ID="lblNombre_dlCompanierosAula" ForeColor="Black"  runat="server" Font-Bold="false" Text='<%# Eval("Profesor") %>'  /> </b>                                                                                       
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td colspan ="2" style =" height :3px;">
                                                                     <div style="BORDER-TOP: #efe989 1px solid;width:180px"></div>
                                                                     </td>
                                                                     
                                                                </tr>
                                                               <tr>                                                                    
                                                                    <td colspan ="2" style="width:155px; height: 15px; text-align:left ; color:White; font-size:10px;" align="left" valign="middle">                                                                                    
                                                                       <asp:Label ID="lblFechaNacimiento_dlCompanierosAula" ForeColor="Black"  runat="server" Font-Bold="true" Text='<%# Eval("CursoCompuesto") %>'  />                                                                                      
                                                                    </td>
                                                                </tr> 
                                                                <tr>
                                                                    <td colspan ="2" style =" height :3px;">
                                                                     <div style="BORDER-TOP: #efe989 1px solid;width:180px"></div>
                                                                     </td>
                                                                     
                                                                </tr>
                                                                <tr>                                                                    
                                                                    <td colspan ="2" style="width:155px; height: 15px; text-align:left ; color:White; font-size:10px;" align="left" valign="middle">                                                                                    
                                                                                                                                                             
                                                                    </td>
                                                                </tr>                                                                
                                                            </table>
                                                            </td>
                                                            
                                                            <td style ="background-color :#efe989;width:2px;height :1px;">
                                                            &nbsp;
                                                            </td>
                                                          </tr>
                                                        </table>
                                                            
                                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 250px; ">
                                                                <tr>
                                                                    <td style ="background-color :#efe989 ; height :1px;font-size:2px; ">
                                                                    &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                       
                                               </ItemTemplate>
                                            </asp:DataList>
                                        </center> 
                                    </td>
                                </tr>
                               </table> 
                </td>
             </tr>
             <tr>
                <td style="width: 554px; height:30px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_inferior.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td>
                <td style="width:286px; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_inferior2.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td>
             </tr>
         </table>
            </ContentTemplate>    
        </asp:UpdatePanel>
    </div>
    
</asp:Content>

