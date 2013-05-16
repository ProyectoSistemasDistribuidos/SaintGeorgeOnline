<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="DistribucionLibrosAnual.aspx.vb" Inherits="Modulo_BancoLibros_DistribucionLibrosAnual" title="Página sin título" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript" >

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }

 

</script>

<style type="text/css">
   
               
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
   
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="Udp_PaginaMantenimiento" runat="server" UpdateMode="Conditional"  >
    <ContentTemplate>
    
        <div id="miContainerMantenimiento">
            <atk:TabContainer ID="Tc_PaginaMantenimiento" 
                              runat="server" 
                              Width="881px" 
                              ActiveTabIndex="0"
                              AutoPostBack="false" 
                              ScrollBars="None" >
                <atk:TabPanel ID="Tp_Registro" runat="server" HeaderText="Tab1" Enabled="true">
                    <HeaderTemplate>
                        <asp:Label ID="lbl_TituloPanel" runat="server" Text="Registro" />
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="border: solid 0px blue; width: 870px;">
                            <div id="miBusquedaActualizacion_Ficha">
                                <fieldset>
                                    <legend>Criterios de busqueda</legend>
                                    <table cellpadding="0" cellspacing="0" style="border: solid 0px red; min-width: 820px;">
                                        <tr>
                                            <td>Periodo: </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Periodo" runat="server" OnSelectedIndexChanged ="ddl_Periodo_SelectedIndexChanged" AutoPostBack ="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td>Grado: </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Grado" runat="server" OnSelectedIndexChanged ="ddl_Grado_SelectedIndexChanged" AutoPostBack ="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td>Idioma:</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Idioma" runat="server" OnSelectedIndexChanged ="ddl_Idioma_SelectedIndexChanged" AutoPostBack ="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>   
                                        <tr>
                                            <td colspan="6">&nbsp;</td>
                                        </tr> 
                                        <tr>
                                            <td colspan="6">
                                            <div id="miGridviewMant2">
                                            <table  cellpadding="0" cellspacing="0" style="border: solid 0px red; min-width: 800px;">                                
                                               <tr >
                                
                                                    <td id="miHeaderDetCPago1" rowspan ="2" style="border-top:solid; border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width: 59px; height: 26px; text-align:left ; color:White; font-size:10px; background-color:#0a0f14;"  align="left" >
                                                        Grado</td>
                                                    <td id="miHeaderDetCPago1" rowspan ="2" style="border-top:solid; border-bottom:solid;  border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width:75px; height: 26px; text-align:left ; color:White; font-size:10px; background-color:#0a0f14;"  align="left" >
                                                        Sección</td>
                                                    <td id="miHeaderDetCPago1" rowspan ="2" style="border-top:solid; border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:25px; height: 26px; text-align:left ; color:White; font-size:10px; background-color:#0a0f14;"  align="left" >
                                                        Agregar</td>
                                                                                   
                                                    <td id="miHeaderDetCPago1" colspan ="2"  style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width:156px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                     1° Bimestre
                                                     </td>
                                                     <td id="miHeaderDetCPago1" colspan ="2"  style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width:156px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                     2° Bimestre
                                                     </td>
                                                     <td id="miHeaderDetCPago1" colspan ="2"  style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width:156px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                     3° Bimestre
                                                     </td>
                                                     <td id="miHeaderDetCPago1" colspan ="2"  style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width:156px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                     4° Bimestre
                                                     </td>
                                                </tr>
                                            </table> 
                                            <asp:GridView   ID="dgv_AsignacionAnual" 
                                                                Width="800px" 
                                                                CssClass="miGridviewBusqueda" 
                                                                GridLines="Both" 
                                                                ShowHeader="False" 
                                                                AutoGenerateColumns="False"
                                                                runat="server">
                                                <Columns> 
                                                    <asp:TemplateField >                                                                      
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCodigoGrado" runat="server" Text='<%# Bind("CodigoGrado") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                    </asp:TemplateField> 
                                                    
                                                    <asp:TemplateField >                                                                      
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCodigoAula" runat="server" Text='<%# Bind("CodigoAula") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                    </asp:TemplateField> 
                                                    
                                                    <asp:TemplateField>  
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrado" runat="server" Text='<%# Bind("Grado") %>' />
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="miGridviewBusqueda_Rows" Font-Size="12px" HorizontalAlign="Left" Font-Names="Arial" Width="59px" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField>  
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAula" runat="server" Text='<%# Bind("Aula") %>' />
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="miGridviewBusqueda_Rows" Font-Size="12px" HorizontalAlign="Left" Font-Names="Arial" Width="75px" />
                                                    </asp:TemplateField>
                                                    
                                                     <asp:TemplateField>  
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btn_AgregarAsignacion" Width="20px" Height="20px" AlternateText='<%# Bind("IdFila") %>'
                                                                runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Add-icon.png" 
                                                                style="cursor:pointer;" onclick="btn_AgregarAsignacion_Click" />                                                            
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="35px" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField>  
                                                        <ItemTemplate>
                                                            <asp:BulletedList  style="padding-left :15px; list-style-type:disc;" ID="bl_Lista1" runat="server">
                                                            </asp:BulletedList>
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="miGridviewBusqueda_Rows" Font-Size="10px" Font-Names="Arial" VerticalAlign="Top"   HorizontalAlign="Left" Width="160px" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField>  
                                                        <ItemTemplate>
                                                            <asp:BulletedList style="padding-left :15px; list-style-type:disc;" ID="bl_Lista2" runat="server">
                                                            </asp:BulletedList>
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="miGridviewBusqueda_Rows" Font-Size="10px" Font-Names="Arial" VerticalAlign="Top" HorizontalAlign="Left" Width="160px" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField>  
                                                        <ItemTemplate>
                                                            <asp:BulletedList style="padding-left :15px; list-style-type:disc;" ID="bl_Lista3" runat="server">
                                                            </asp:BulletedList>
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="miGridviewBusqueda_Rows" Font-Size="10px" Font-Names="Arial" VerticalAlign="Top" HorizontalAlign="Left" Width="160px" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField>  
                                                        <ItemTemplate>
                                                            <asp:BulletedList style="padding-left :15px; list-style-type:disc;" ID="bl_Lista4" runat="server">
                                                            </asp:BulletedList>
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="miGridviewBusqueda_Rows" Font-Size="10px" Font-Names="Arial" VerticalAlign="Top" HorizontalAlign="Left" Width="160px" />
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                                </asp:GridView>
                                            </div>
                                           
                                                <asp:Panel ID="pnl_AsignarLibros" runat="server"  >
                                                <table cellpadding="0"  cellspacing="0" border="0" style="background-color:White; " >
                                                    <tr>
                                                        <td ID="AgregarRegistroHeader" align="left" class="miGVBusquedaFicha_Header_V2" 
                                                            colspan="2" style="width: 370px; height: 26px" valign="middle">
                                                            <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">
                                                           
                                                                <asp:Label ID="lbl_TituloAsignacionLibros" runat="server" Text=" Asignación de Libros"></asp:Label>
                                                                <asp:HiddenField ID="hd_codigoAnioAcademico" runat="server" /> 
                                                                 <asp:HiddenField ID="hd_codigoGrado" runat="server" /> 
                                                                  <asp:HiddenField ID="hd_codigoAula" runat="server" /> 
                                                            </span>
                                                        </td>
                                                        <td align="right" class="miGVBusquedaFicha_Header_V2" 
                                                            style="width: 30px; height: 26px" valign="middle">
                                                            <asp:ImageButton ID="btnCerrarPanelEnvioCorreo" runat="server" Height="15px" 
                                                                ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" ToolTip="Cerrar Panel" 
                                                                Width="16px" />
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                        <td> 
                                                            &nbsp;&nbsp;</td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td style="width:140px;padding-left:5px;font-size:12px;">
                                                                   <table border="0">
                                                                    <tr>
                                                                        <td style="width:60px; padding-left:5px;font-size:12px;"> <span>Periodo:</span>
                                                                        </td>
                                                                         <td  style="width:80px; font-size:12px;" align ="left" >                
                                                                            <asp:Label ID="lbl_Anio" runat="server" Font-Bold="True"  ></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                               </table>
                                                        </td>
                                                        <td></td>
                                                                 
                                                     </tr>     
                                                    <tr>
                                                            <td style="width:140px;padding-left:5px;font-size:12px;">
                                                               <table border="0">
                                                                    <tr>
                                                                        <td style="width:60px; padding-left:5px;font-size:12px;"> <span>Grado:</span>
                                                                        </td>
                                                                         <td  style="width:80px; font-size:12px;" >                
                                                                            <asp:Label ID="lbl_NombreGrado" runat="server" Font-Bold=True></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                               </table>
                                                            </td>
                                                            <td rowspan ="4" style="padding-left :100px; font-weight:bold;text-align:center;" align ="right" valign ="bottom"  >  
                                                                 <table  cellpadding="0" cellspacing="0" border="0" style=" border: solid 0x red; width:230px; ">
                                                                <tr>
                                                                   <td id="miHeaderDetCPago1" style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width: 70px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                                     Bimestres</td>
                                                                   <td id="miHeaderDetCPago1" style="border-bottom:solid; border-top:solid;   border-width:1px;  border-color:#a6a3a3;  width:80px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                                     Fecha de Inicio</td>
                                                                   <td id="miHeaderDetCPago1" style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width: 80px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                                     Fecha de Fin</td>
                                                                </tr>
                                                                 <tr>
                                                                   <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 40px; font-family :Arial; font-size :12px; " align="center" valign="middle"><span> 1°</span></td>
                                                                     <td style="border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                          <asp:Label ID="lblFecInicio1" runat="server"></asp:Label>
                                                                     </td>
                                                                    <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                      <asp:Label ID="lblFecFin1"  runat="server"></asp:Label>
                                                                     </td>
                                                                </tr>
                                                                <tr>
                                                                   <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 70px; font-family :Arial; font-size :12px; " align="center" valign="middle"><span> 2°</span></td>
                                                                     <td style="border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                          <asp:Label ID="lblFecInicio2"  runat="server"></asp:Label>
                                                                     </td>
                                                                    <td style="border-right:solid; border-left:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                      <asp:Label ID="lblFecFin2"  runat="server"></asp:Label>
                                                                     </td>
                                                                </tr>
                                                                <tr>
                                                                   <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 70px; font-family :Arial; font-size :12px; " align="center" valign="middle"><span> 3°</span></td>
                                                                     <td style="border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                          <asp:Label ID="lblFecInicio3"  runat="server"></asp:Label>
                                                                     </td>
                                                                    <td style="border-left:solid; border-right:solid; border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                      <asp:Label ID="lblFecFin3"  runat="server"></asp:Label>
                                                                     </td>
                                                                </tr>
                                                                <tr>
                                                                   <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 70px; font-family :Arial; font-size :12px; " align="center" valign="middle"><span> 4°</span></td>
                                                                     <td style="border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                          <asp:Label ID="lblFecInicio4"  runat="server"></asp:Label>
                                                                     </td>
                                                                    <td style="border-bottom:solid; border-left:solid; border-right:solid;border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                      <asp:Label ID="lblFecFin4"  runat="server"></asp:Label>
                                                                     </td>
                                                                </tr>
                                                              
                                                            </table>
                                                           </td>
                                                            
                                                    </tr>
                                                    <tr>
                                                        <td style="width:140px;padding-left:5px;font-size:12px; ">
                                                           <table  border="0">
                                                                <tr>
                                                                    <td style="width:60px; padding-left:5px;font-size:12px;"><span>Sección:</span>
                                                                     </td>
                                                                     <td style="width:80px; font-size:12px;" >     
                                                                        <asp:Label ID="lbl_NombreSeccion" runat="server" Font-Bold=True ></asp:Label>
                                                                    </td>
                                                                 </tr>
                                                            </table>
                                                       </td>
                                                    </tr>
                                                                 
                                                     <tr>
                                                        <td> 
                                                            &nbsp;&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td  style="padding-left:5px"> 
                                                            <asp:ImageButton ID="btnAgregarLibro" Height="20px" Width="20px" OnClick ="btnAgregarLibro_click"  ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Add-icon.png" runat="server" /> <span style="padding-bottom:3px;height:25px " >Agregar Libro</span></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan ="2"> 
                                                            &nbsp;&nbsp;</td>
                                                       
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left:5px" >
                                                           <div id="miGridviewMant2" style ="width :500px;">
                                                            <table width="500px" cellpadding ="0" cellspacing="0" > 
                                                                <tr  >
                                                                    <td id="miHeaderDetCPago2" style="font-size :12px; font-family :Arial ; text-align:center;width:250px; height: 26px;" ><b>Libros</b></td>
                                                                    <td id="miHeaderDetCPago2" style="font-size :12px; font-family :Arial ; text-align:center;width:140px; height: 26px; "><b>Inicio de Utilización</b></td>
                                                                    <td id="miHeaderDetCPago2"style="font-size :12px; font-family :Arial ; text-align:center;width:100px; height: 26px; "><b>Fin de Utilización</b></td>
                                                                </tr>
                                                            </table>
                                                            <div style="margin :0px; padding :0px; border-bottom: 0; overflow-y: scroll; overflow-x: hidden; width: 500px; height: 200px; ">    
                                                                 <asp:GridView ID="dgv_ConsolidadoLibros"  Width="485px" 
                                                                CssClass="miGridviewBusqueda" 
                                                                GridLines="None" 
                                                                ShowHeader="False" 
                                                                AutoGenerateColumns="False"
                                                                OnRowCommand="dgv_ConsolidadoLibros_RowCommand"
                                                                OnRowDataBound="dgv_ConsolidadoLibros_RowDataBound"
                                                                runat="server">
                                                                <Columns> 
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                                                                CommandName="Eliminar" CommandArgument='<%# Bind("IdFila") %>' ToolTip="Eliminar Registro" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField > 
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCodigoAsignacionLibro" runat="server" Text='<%# Bind("CodigoAsignacionLibro") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                    </asp:TemplateField> 
                                                                    
                                                                   <asp:TemplateField > 
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblTipoDato" runat="server" Text='<%# Bind("TipoDato") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                    </asp:TemplateField> 
                                                                    
                                                                    <asp:TemplateField > 
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblCodigoLibro" runat="server" Text='<%# Bind("CodigoLibro") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                    </asp:TemplateField> 
                                                                    
                                                                     <asp:TemplateField > 
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCodigoAnioAcademico" runat="server" Text='<%# Bind("CodigoAnioAcademico") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                    </asp:TemplateField> 
                                                                    
                                                                    <asp:TemplateField >                                                                    
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCodigoGrado" runat="server" Text='<%# Bind("CodigoGrado") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                    </asp:TemplateField> 
                                                                    
                                                                    <asp:TemplateField >                                                                      
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCodigoAula" runat="server" Text='<%# Bind("CodigoAula") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                    </asp:TemplateField> 
                                                                    
                                                                    <asp:TemplateField>  
                                                                        <ItemTemplate>
                                                                             <asp:Label ID="lblTitulo" runat="server" Text='<%# Bind("Titulo") %>' />
                                                                        </ItemTemplate>
                                                                       <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="215px" />
                                                                    </asp:TemplateField>
                                                                    
                                                                     <asp:TemplateField >
                                                                        <ItemTemplate>

                                                                            <table cellpadding="0" cellspacing="0" border="0" width="120px">
                                                                                <tr>
                                                                                    <td valign="middle" align="right" style="width: 90px; height: 25px;">
                                                                                        <asp:TextBox ID="tbFechaInicio" runat="server"   CssClass="miTextBoxCalendar" Width="80" />
                                                                                        <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                                                            TargetControlID="tbFechaInicio"
                                                                                            UserDateFormat="DayMonthYear"                                                                    
                                                                                            Mask="99/99/9999" 
                                                                                            MaskType="Date" 
                                                                                            PromptCharacter="-">
                                                                                            
                                                                                        </atk:MaskedEditExtender>                                                                                                                          
                                                                                    </td>
                                                                                    <td valign="middle" align="left" style="width: 30px; height: 25px;">
                                                                                        <asp:ImageButton runat="server" ID="imgFechaInicio" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                                                                        <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                                                            TargetControlID="tbFechaInicio"
                                                                                            PopupButtonID="imgFechaInicio" 
                                                                                            Format="dd/MM/yyyy" 
                                                                                            CssClass="MyCalendar" Enabled="True" />       
                                                                                    </td>
                                                                                </tr>
                                                                            </table>         
                                                                        
                                                                       </ItemTemplate>
                                                                        <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField >
                                                                        <ItemTemplate>

                                                                            <table cellpadding="0" cellspacing="0" border="0" width="120px">
                                                                                <tr>
                                                                                    <td valign="middle" align="right" style="width: 90px; height: 25px;">
                                                                                        <asp:TextBox ID="tbFechaFin" runat="server"   CssClass="miTextBoxCalendar" Width="80" />
                                                                                        <atk:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                                                                                            TargetControlID="tbFechaFin"
                                                                                            UserDateFormat="DayMonthYear"                                                                    
                                                                                            Mask="99/99/9999" 
                                                                                            MaskType="Date" 
                                                                                            PromptCharacter="-">
                                                                                            
                                                                                        </atk:MaskedEditExtender>                                                                                                                          
                                                                                    </td>
                                                                                    <td valign="middle" align="left" style="width: 30px; height: 25px;">
                                                                                        <asp:ImageButton runat="server" ID="imgFechaFin" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                                                                        <atk:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                                                            TargetControlID="tbFechaFin"
                                                                                            PopupButtonID="imgFechaFin" 
                                                                                            Format="dd/MM/yyyy" 
                                                                                            CssClass="MyCalendar" Enabled="True" />       
                                                                                    </td>
                                                                                </tr>
                                                                            </table>         
                                                                        
                                                                       </ItemTemplate>
                                                                        <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                                                    </asp:TemplateField>
                                                                    
                                                                </Columns> 
                                                            </asp:GridView>
                                                            </div>
                                                           </div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td> 
                                                            &nbsp;&nbsp;</td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align:center; " >
                                                        <asp:ImageButton ID="btnGrabarDetalle" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                                        onclick="btnGrabarDetalle_click"
                                                        />
                                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:ImageButton ID="btnCancelarDetalle" runat="server" 
                                                                CausesValidation="False" Height="19px" 
                                                                ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                                                onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                                                ToolTip="Cancelar" Width="84px" />
                    
                                                        </td>
                                                    </tr>
                                                  
                                                    </table>
                                                </asp:Panel>
                                                
                                                <atk:ModalPopupExtender ID="mpe_AsignarLibros" runat="server"
                                                    TargetControlID="lbl_ActivarAsignacion"
                                                    PopupControlID="pnl_AsignarLibros"
                                                    BackgroundCssClass="MiModalBackground" 
                                                    Drag="true" 
                                                    PopupDragHandleControlID="lbl_TituloAsignacionLibros"
                                                     DynamicServicePath="" Enabled="True" />   
                                                    
                                                <atk:ModalPopupExtender ID="mpe_AgregarLibro" runat="server"
                                                    TargetControlID="VerAgregarLibro"
                                                    PopupControlID="pnlAgregarLibro"
                                                    BackgroundCssClass="MiModalBackground" 
                                                    OkControlID="OKAgregarLibro" 
                                                    CancelControlID="CancelAgregarLibro"
                                                    Drag="true" 
                                                    PopupDragHandleControlID="AgregarLibroHeader" />
    
                                                <asp:panel id="pnlAgregarLibro" BackColor="White" BorderColor="Black" runat="server" style ="height:600; z-index:51;">
                                                <table cellpadding="0" cellspacing="0" border="0"  width="500px" id="panelRegistro">    
                                                    <tr>
                                                        <td style="width: 480px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" id="AgregarLibroHeader" >                
                                                            <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Consultar Libros</span>
                                                        </td>
                                                        <td style="width: 20px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                                                            <asp:ImageButton ID="btnCerraAgregarLibro" runat="server" Width="16" Height="15"
                                                                ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                                                                onclick="btnCerraAgregarLibro_Click" ToolTip="Cerrar Panel"/>
                                                        </td>
                                                    </tr>
                                                    <tr><td height="5px" colspan="2"></td></tr>   
                                                    <tr>
                                                        <td colspan="2" align="left" valign="top" style="padding-left:10px; width:500px">   
                                                         <table border="0" >
                                                                
                                                            <tr>
                                                                <td style="width:80px; height: 25px;" align="left" valign="bottom">
                                                                    <span>Título</span>
                                                                     <asp:HiddenField ID="hfTotalRegsGVTodos" runat="server" Value="0" />
                                                                </td>
                                                                <td style="width:300px; height: 25px;"  align="left" valign="bottom">
                                                                    <asp:TextBox ID="tbTitulo" runat="server" CssClass="miTextBox" Width="280px" 
                                                                        MaxLength="100" BackColor="#FFFFCC" />
                                                                 </td>
                                                                 <td style="width: 100px; height: 25px;" align="left" valign="top">
                                                                       <asp:ImageButton ID="btnBuscar" runat="server" Width="94" Height="19" 
                                                                            ImageUrl="~/App_Themes/Imagenes/btnBuscarV2_1.png"
                                                                            onmouseover="this.src = '../App_Themes/Imagenes/btnBuscarV2_2.png'" 
                                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnBuscarV2_1.png'"
                                                                            OnClick="btnBuscar_Click"/>
                                                                 </td>
                                                            </tr>
                                                             <tr>
                                                                <td style=" width: 80px; height: 25px;" align="left" valign="bottom">
                                                                    <span>Idioma</span>
                                                                </td>
                                                                <td align="left" style="width:300px; height: 25px;" valign="bottom">
                                                                   <asp:RadioButtonList ID="rdBuscarIdioma"  runat="server" RepeatDirection="Horizontal" Width ="280px" Height="5px">
                                                                        <asp:ListItem Value="0" Text="Todos" Selected="True" />
                                                                        <asp:ListItem Value="1" Text="Ingles" />
                                                                        <asp:ListItem Value="2" Text="Español" />
                                                                        <asp:ListItem Value="3" Text="Frances" />                                        
                                                                        <asp:ListItem Value="4" Text="Otro" />  
                                                                    </asp:RadioButtonList>     
                                                                </td>
                                                                <td style="width: 100px; height: 25px;" align="left" valign="top">
                                                                      <asp:ImageButton ID="btnLimpiar" runat="server" Width="74" Height="19" 
                                                                            ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                                            onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                                            onclick="btnLimpiar_Click" 
                                                                            ToolTip="Limpiar Filtros" CausesValidation="false"/>        
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan ="3" style="width: 100px; height:5px;" align="left" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 500px; margin: auto;" align="left" valign="top" colspan="3">
                                                                    <div id="miGridviewMant2" style="Width:500px; "> 
                                                                       <asp:GridView ID="GVListaTodos" runat="server"                 
                                                                        Width="500px" 
                                                                        CssClass="miGridviewBusquedaPersona" 
                                                                        GridLines="none" 
                                                                        AutoGenerateColumns="False"                      
                                                                        ShowFooter="false"
                                                                        ShowHeader="true"                             
                                                                        AllowPaging="True" 
                                                                        AllowSorting="true"
                                                                        PageSize="10"
                                                                        EmptyDataText=" - No se encontraron resultados - "
                                                                        EmptyDataRowStyle-ForeColor="#a51515"
                                                                        EmptyDataRowStyle-HorizontalAlign="Center"                              
                                                                        OnRowDataBound="GVListaTodos_RowDataBound"
                                                                        OnRowCommand="GVListaTodos_RowCommand"
                                                                        OnPageIndexChanging="GVListaTodos_PageIndexChanging"                             
                                                                        OnSorting="GVListaTodos_Sorting" 
                                                                        OnRowCreated="GVListaTodos_RowCreated">
                                                                        <HeaderStyle CssClass="miGridviewBusquedaPersona_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                                                                        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                                                                        
                                                                        <Columns>
                                                                        
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                
                                                                                    <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                                                                                        CommandName="Seleccionar" CommandArgument='<%# Bind("CodigoLibro") %>' ToolTip="Seleccionar Persona" />
                                                                                        
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:TemplateField HeaderText="Titulo">  
                                                                                <HeaderTemplate>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                    <tr>
                                                                                        <td style="width:250px;" align="right" valign="middle">Titulo&nbsp;</td>
                                                                                        <td style="width:220px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Titulo" runat="server" 
                                                                                            ToolTip="Descendente"    
                                                                                            ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                                                            CommandName="Sort" 
                                                                                            CommandArgument="Titulo"/></td>
                                                                                    </tr>
                                                                                </table>                                    
                                                                                </HeaderTemplate>                                                                      
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTitulo" runat="server" Text='<%# Bind("Titulo") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="200px"/>
                                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="left" Width="200px" />
                                                                            </asp:TemplateField>
                                                                            
                                                                           <asp:BoundField DataField="Editorial" HeaderText="Editorial" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" ItemStyle-CssClass="miGridviewBusqueda_Rows" />
                                
                                                                            <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                                                                <ItemTemplate>                                    
                                                                                    <asp:Label ID="lbRutaPortada" runat="server" Text='<%# Bind("RutaPortada") %>' />                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                                                                <ItemTemplate>                                    
                                                                                    <asp:Label ID="lbCodigoIdioma" runat="server" Text='<%# Bind("CodigoIdioma") %>' />                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                              <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                                                                <ItemTemplate>                                    
                                                                                    <asp:Label ID="lbCodigoGrado" runat="server" Text='<%# Bind("CodigoGrado") %>' />                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                        </Columns> 
                                                                        
                                                                        <PagerTemplate>
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 500px;">
                                                                                <tr>                                        
                                                                                    <td style="height: 20px; width: 240px;" align="left" valign="middle">
                                                                                        <span class="miFooterMantLabelLeft">Ir a página</span>                                     
                                                                                        <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                                                            CssClass="letranormal" 
                                                                                            AutoPostBack="true" 
                                                                                            OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                                                                        </asp:DropDownList>&nbsp;de&nbsp;
                                                                                        <asp:Label ID="lblNumPaginas" runat="server" />                                         
                                                                                    </td>                                        
                                                                                    <td style="height: 20px; width: 300px;" align="center" valign="middle">                                           
                                                                                        <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Prim. Pag" CommandArgument="First"
                                                                                            CssClass="pagfirst" />
                                                                                        <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                                                                            CommandArgument="Prev" CssClass="pagprev" />
                                                                                        <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Sig. página"
                                                                                            CommandArgument="Next" CssClass="pagnext" />
                                                                                        <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
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
                                                     <tr><td  height="5px" colspan="2"></td></tr>  
                                                        
                                                    
                                                </table>  
                                                <div id="controlAgregarRegistro" style="display:none">
                                                    <input type="button" id="VerAgregarLibro" runat="server"   />
                                                    <input type="button" id="OKAgregarLibro" />
                                                    <input type="button" id="CancelAgregarLibro" />
                                                </div>       
                                                </asp:panel>
                                                    
                                                <asp:Label ID="lbl_ActivarAsignacion" runat="server"></asp:Label>    
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
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

