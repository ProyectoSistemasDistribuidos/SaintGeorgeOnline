<%@ Page Title="" Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="Matricula.aspx.vb" Inherits="Interfaz_Familia_Modulo_Matricula_Matricula" %>

<%@ MasterType VirtualPath="~/Interfaz_Familia/Plantilla_Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
             <Triggers>
                    <asp:PostBackTrigger ControlID="GridView1" /> 
                    <asp:PostBackTrigger ControlID="btnDescargaManual" /> 
                </Triggers>
<ContentTemplate>

<table border="0" cellpadding="0" cellspacing="0" style="width: 700px;">
        <tr>
            <td rowspan ="4" style="width: 100px; height:auto;"><span>&nbsp;&nbsp;<br />&nbsp;&nbsp;<br /></span>
            </td>
            <td align="center" valign="middle" style="width:600px; height:20px; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_cab5.png'); background-repeat:no-repeat;">         
            </td>
            
        </tr>
        <tr>
            <td align="right" valign="top" style="width: 600px; height:auto; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_centro5.png');background-repeat:repeat-y;">
               <table cellpadding="0" cellspacing="0" border="0" style="width:600px; height:auto;">
                    <tr>
                         <td style=" padding-left :20px; width: 600px; height: 20px " align="left" valign="middle">
                            <span style="font-family:Arial;font-size:13px;font-weight:bold;color:red;" ><b>Importante , tener en cuenta</b></span>
                         </td>
                    </tr>
                    <tr>
                        <td style=" padding-left :20px;  padding-right:20px; width: 600px; height: 26px;" align="left" valign="middle">
                        <fieldset style="background:#ffffcc;   -moz-border-radius: 10px 10px 0px 0px;">
                            <span style="font-size: 8pt; font-family: Arial; color:DarkRed;">La matrícula se realizará en 2 etapas las cuales se detallarán a continuación:</span>
                             <ul style ="font-size: 8pt; font-family: Arial; color:DarkRed; text-align:justify; padding-left :20px; padding-right:20px;"> 
                               <li>En la <b>1era etapa</b> se realizará la <b>Actualización de Datos</b>, la cual consta de 3 módulos:<b> Datos de Familiares, Datos del Alumno y Datos Médicos.</b></li> 
                               <li>En la <b>2da etapa</b> se realizará el <b>Proceso de Matrícula</b>, considerar que no podrá realizar esta etapa sino cumple con la <b>1era</b> y con los requisitos previos antes de la matrícula.</li>
                               <li>Finalizado la 2da etapa usted podrá visualizar la constancia de matrícula.</li>
                             </ul> 
                        </fieldset>
                        </td>
                    </tr>
                    <tr>
                      <td style="padding-top:10px;  padding-left:20px; width: 600px; height:30px; " align="left" valign="middle">
                      <asp:ImageButton ID="btnDescargaManual" runat="server" Width="130px" Height="30px" OnClick ="btnDescargaManual_Click"
                       ImageUrl="~/App_Themes/Imagenes/btn_DescargaManual.jpg" ToolTip="Descargar Manual de Matrícula"/>
                      </td> 
                    </tr> 
                    <tr>
                      <td style="padding-left :20px; padding-right :20px; width: 600px;" align="right" valign="middle">
                            <table  cellpadding="0" cellspacing="0" border="0" style="width:600px; height: auto; 
                                  font-size:10px; font-family: Verdana, Arial, Helvetica, sans-serif;" >
                               <tr>
                                    <td colspan="3" style="width: 320px; height: 26px;" align="center" valign="middle">                                                                      
                                    </td>        
                                    <td colspan ="4" style="color:White; border: solid 1px #a6a3a3; background-color: #41576f; width: 280px; height: 26px;" align="center" valign="middle">
                                        <span>Matricula</span>                                                                 
                                    </td>                      
                               </tr>  
                               <tr>
                                    <td  style="color:White; border-left: solid 1px #a6a3a3; border-right: solid 1px #a6a3a3; border-top: solid 1px #a6a3a3; background-color: #41576f; width:190px; height: 26px;" align="center" valign="middle">
                                        <span>Alumno</span>                                                                 
                                    </td>
                                    <td  style="color:White; border-right: solid 1px #a6a3a3; border-top: solid 1px #a6a3a3;  background-color: #41576f; width:70px; height: 26px;" align="center" valign="middle">
                                        <span>Grado</span>                                                                 
                                    </td>        
                                    <td  style="color:White; border-right: solid 1px #a6a3a3; border-top: solid 1px #a6a3a3;  background-color: #41576f; width: 60px; height: 26px;" align="center" valign="middle">
                                        <span>Año</span>                                                                 
                                    </td>  
                                    <td style="color:White; border-right: solid 1px #a6a3a3;  background-color: #41576f; width: 63px; height: 26px;" align="center" valign="middle">
                                        <span>Actualización de datos</span>                                                                 
                                    </td>  
                                    <td style="color:White; border-right: solid 1px #a6a3a3;  background-color: #41576f; width:63px; height: 26px;" align="center" valign="middle">
                                        <span>Proceso de Matrícula</span>                                                                 
                                    </td>  
                                    <td style="color:White;  border-right: solid 1px #a6a3a3; background-color: #41576f; width: 63px; height: 26px;" align="center" valign="middle">
                                        <span>Constancia de Matrícula</span>                                                                 
                                    </td>     
                                    <td style="color:White;  border-right: solid 1px #a6a3a3;  background-color: #41576f; width: 91px; height: 26px;" align="center" valign="middle">
                                        <span>Estado de Matrícula</span>                                                                 
                                    </td>                      
                               </tr>        
                               <tr>
                                    <td colspan="7" align="right" valign="top" style=" width :600px; height :auto;">
                                        <div id="miGridviewMantActualizacion_Ficha" style="border: solid 1px #a6a3a3; width:600px; padding: 0; margin: 0;"> 

                                          <asp:GridView ID="GridView1" runat="server" 
                                            CssClass="miGridviewBusqueda" 
                                            Width="600px"
                                            GridLines="Both" 
                                            AutoGenerateColumns="False"
                                            AllowPaging="False" 
                                            AllowSorting="False"
                                            ShowFooter="false"
                                            ShowHeader="false"
                                            EmptyDataText=" - No se encontraron resultados - "           
                                            OnRowDataBound="GridView1_RowDataBound"
                                            OnRowCommand="GridView1_RowCommand">
                                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                                                
                                            <Columns>     
                                                
                                                <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                
                                                <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRutaFoto" runat="server" Text='<%# Bind("RutaFoto") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                
                                                 <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoGradoMatricula" runat="server" Text='<%# Bind("CodigoGradoMatricula") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                 
                                                                 
                                                 <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoAnioMatricula" runat="server" Text='<%# Bind("CodigoAnioMatricula") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>   
                                                
                                                <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoNivelMatricula" runat="server" Text='<%# Bind("CodigoNivelMatricula") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                
                                                              
                                                <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNivelMatricula" runat="server" Text='<%# Bind("NivelMatricula") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                
                                                 <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoMatricula" runat="server" Text='<%# Bind("CodigoMatricula") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                
                                                <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGradoMatricula" runat="server" Text='<%# Bind("GradoMatricula") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                                                
                                                    <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConfirmacion" runat="server" Text='<%# Bind("Confirmacion") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>   
                                                               
                                                <asp:TemplateField ItemStyle-Width="190px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                                    ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            
                                                <asp:TemplateField ItemStyle-Width="70px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGradoAcad" runat="server" Text='<%# Bind("GradoMatricula") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                 
                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblAnioAcad" runat="server" Text='<%# Bind("AnioMatricula") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                <asp:TemplateField ItemStyle-Width="63px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                      <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                      CommandName="Actualizar" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Actualizar datos" />
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField ItemStyle-Width="63px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                     <asp:ImageButton ID="btnProcesoMatricula" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                      CommandName="ProcesoMatricula" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Proceso de Matrícula" />
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField ItemStyle-Width="63px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                     <asp:ImageButton ID="btnConstancia" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_ver.png"
                                                                      CommandName="Constancia" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Constancia de Matrícula" />
                                                     </ItemTemplate>
                                                </asp:TemplateField>  
                                                
                                                <asp:TemplateField ItemStyle-Width="91px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblEstadoMatricula" runat="server" Text='<%# Bind("EstadoMatricula") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                
                                                
                                                 <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CodigoPasoLogMatriculaEtapa1" runat="server" Text='<%# Bind("CodigoPasoLogMatriculaEtapa1") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>          
                                                
                                                 <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="miHiddenStyle"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CodigoPasoLogMatriculaEtapa2" runat="server" Text='<%# Bind("CodigoPasoLogMatriculaEtapa2") %>' />
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
                </table>   
             </td>     
        </tr>               
        <tr>
            <td style="width: 600px; height:30px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_inferior5.png');background-repeat:no-repeat;">
        </tr>
    </table>

</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

