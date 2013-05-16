<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="Principal.aspx.vb" Inherits="Interfaz_Familia_Principal" title="Página sin título" %>

<%@ MasterType VirtualPath="~/Interfaz_Familia/Plantilla_Principal.master" %>

<%@ Register src="../Controles/ComunicadosHome.ascx" tagname="ComunicadosHome" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 850px;">
    <tr>
        <td style="height:19px;width:237px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_cab.jpg');background-repeat:no-repeat;" >
           
        </td>
        
        <td style="height:19px;width:39px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_cab2.jpg');background-repeat:no-repeat;" >
        </td>
        
        <td style="height:19px;width:574px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_cab3.jpg');background-repeat:no-repeat;" >
        &nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;width:237px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_centro.jpg');background-repeat:repeat-y;" >
        
        <table border="0" cellpadding="0" cellspacing="0" style="text-align: center;margin-left:auto;margin-right:auto;">
            <tr>
                <td style="font-size:12px;font-weight:bold;text-align:center;font-family:Arial;width:224px;height:25px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_familia_cab.jpg');background-repeat:no-repeat; ">
                <asp:Label ID="lbl_NombreFamilia_Principal" runat="server" Text=""></asp:Label>                
                </td>
            </tr>
            
            <tr>
                <td style="padding-left:15px ;text-align:center;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_familia_centro.jpg');background-repeat:repeat-y; ">
                <asp:GridView ID="gv_Familia" 
                          AutoGenerateColumns="False" 
                          BorderStyle="None" 
                          BorderWidth="0px"
                          OnRowDataBound="dgv_Familia_RowDataBound" 
                          GridLines="None" 
                          
                          runat="server" ShowHeader="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="">
                                        <asp:Image style="width:17px;height:17px " ID="Image1" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/iconos/persona.gif" />
                                    </td>
                                    <td style="text-align:left;font-family:Arial;font-size:11px;font-weight:bold; ">
                                         <asp:Label ID="Label2" runat="server" Text='<%# Bind("Parentesco") %>'></asp:Label> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td style="text-align:left;font-family:Arial;font-size:10px;">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("NombreCompleto") %>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>                          
                    </asp:TemplateField>
                </Columns>
                </asp:GridView>
                
                </td>
            </tr>
            
            <tr>
                <td style="background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_familia_pie.jpg');background-repeat:no-repeat; " >
                &nbsp;
                </td>
            </tr>
        </table>
        
        <table cellpadding="0" cellspacing="0" border="0" style="text-align: center;margin-left:auto;margin-right:auto;">
                                
            <tr>
                <td style="padding-left:5px;">
                        
                   <asp:GridView ID="dgv_Hijos" 
                                 runat="server"
                                 AutoGenerateColumns="False"
                                 BorderStyle="None" 
                                 BorderWidth="0px"
                                 GridLines="None" ShowHeader="False"
                                 >
                   <Columns>
                      <asp:TemplateField>
                           <ItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                     <tr>
                                         <td style="text-align:left;width:84px;height:75px ;font-family:Arial;font-size:13px;font-weight:bold;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_hijos_1.jpg');background-repeat:no-repeat;">
                                             <span style="padding-left:10px ">
                                                  <asp:Image ID="img_Foto" runat="server" Height="54" Width="44" ImageUrl='<%# Bind("RutaFoto") %>' />
                                                  
                                             </span> 
                                         </td>
                                                                            
                                         <td style="height:75px;font-family: Arial; font-size: 13px; font-weight: bold;  width: 140px; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_hijos_2.jpg'); background-repeat: no-repeat;">
                                             <table style="height:80px" border="0" cellpadding="0" cellspacing="0" >
                                                   <tr>
                                                       <td style="height:12px ;text-align:left;padding-top:10px;font-size:10px ">
                                                           <span style="text-align:left;font-weight:bold ;font-family:Arial;font-size:10px"><asp:Label ID="Label2" runat="server" Text='<%# Bind("Nombres") %>'></asp:Label></span>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td style="text-align:left;font-family:Arial;font-size:9px;font-weight:normal;">
                                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("AnioAcad") %>'></asp:Label> - 
                                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("NivelAcad") %>'></asp:Label> -
                                                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("GradoAcad") %>'></asp:Label> -
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("AulaAcad") %>'></asp:Label>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                   <tr>
                                                                            <td style="padding-top:5px ">
                                                                                
                                                                                
                                                                            </td>
                                                                        </tr>
                                             </table>                                                                               
                                                                                
                                         </td>
                                                                            
                                     </tr>                                     
                                </table>
                           </ItemTemplate>
                      </asp:TemplateField>
                   </Columns>
                   </asp:GridView>
                                                
                </td>
            </tr>               
            
        </table> 
        
        </td>
        
        <td style="height:100px;width:39px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_centro2.jpg');background-repeat:repeat-y;" >
        </td>
        
        <td align ="center"  style="vertical-align:top;width:449px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_centro3.jpg');background-repeat:repeat-y; " 
            rowspan="3">
           <table border="0" cellpadding="0" cellspacing="0" style="padding-left:50px; width:528px;" >
                <tr>
                    <td style="vertical-align:top; ">
                        <table>
                            <tr>
                                <td >
                                    <img src="/saintGeorgeOnline/App_Themes/Imagenes/calen_2.png" 
                                        style="height: 50px; width: 48px" />
                                </td>
                                <td>
                                    <a href ="/saintGeorgeOnline/Interfaz_Familia/Modulo_Tareas/Tareas.aspx">Agenda del Alumno</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                
                                </td>
                            </tr>
                        </table>
                    </td>
                    
                    <td>
                        <table>
                        <tr>
                    <td style="background-color:#41576f;height:18px; width:228px; padding-left:5px; " valign ="top ">
                        <span style="font-family:Arial;font-size:11px;font-weight:bold;color:White; ">Comunicados</span>
                    </td>
                </tr>
                <tr>
                    <%--<td style ="height:200px; width:449px;" valign ="top">
                         <uc1:ComunicadosHome  ID="ComunicadosHome1" runat="server"  />
                    </td>--%>
                    
                    <td align ="left"  style =" padding-left:10px ;vertical-align:top ;width:228px;height:204px ;background-color:White; background-repeat:repeat-y ; ">
                    <div id="DetalleEventos" style="height: 204px; width:210px";>
                    
                    <marquee id="iescroller" direction="up" width="285" height="204px" scrollamount="2" scrolldelay="80" ststyle="border:0 solid #787878;background-color:#FFFFFF" aling="center" onmouseout="this.start()" onmouseover="this.stop()"> 
                                                                 
                    <uc1:ComunicadosHome onmouseup="stop()" onmouseout="start()" ID="ComunicadosHome1" runat="server" /></layer></marquee> 
                     
                       </div> 
                    </td>
                   
                </tr>
                        </table>
                      
                    </td>
                </tr>
            </table>
        
        </td>
    </tr>
    <tr>
        <td style="background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_pie.jpg');background-repeat:no-repeat;">
            &nbsp;</td>
        
        <td style="height:25px ;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_pie2.jpg');background-repeat:no-repeat;">
        &nbsp;
        </td>
        
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        
        <td style="height:21px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_medio.jpg');background-repeat:no-repeat;">
        &nbsp;
        </td>
        
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        
        <td style="height:21px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_inferior.jpg');background-repeat:no-repeat;">
        &nbsp;
        </td>
        
        <td style="background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoprincipal_contenedor_inferior2.jpg');background-repeat:no-repeat;">&nbsp;
        &nbsp;
        </td>
    </tr>
    </table>
</asp:Content>

