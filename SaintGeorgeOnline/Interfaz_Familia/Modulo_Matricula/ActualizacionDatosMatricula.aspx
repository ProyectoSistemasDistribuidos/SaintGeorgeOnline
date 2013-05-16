<%@ Page Title="" Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="ActualizacionDatosMatricula.aspx.vb" Inherits="Interfaz_Familia_Modulo_Matricula_ActualizacionDatosMatricula" %>

<%@ MasterType VirtualPath="~/Interfaz_Familia/Plantilla_Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
.MiModalBackground{
	background-color: black;    
	filter:alpha(opacity=70);
	opacity:0.7;
}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 900px;">
        <tr>
           <%-- <td rowspan="4" style="width: 10px; height:auto;"><span>&nbsp;&nbsp;<br />&nbsp;&nbsp;<br /></span>
            </td>--%>
            <td align="center" valign="middle" style="width:900px; height:20px; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_cab6.png'); background-repeat:no-repeat;">         
            </td>
            
        </tr>
        <tr>
            <td align="left" valign="top" style="padding-left:20px;  padding-bottom:20px; width: 900px; height:auto; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_centro6.png');background-repeat:repeat-y;">
                            
<br />
<span style="font-family:Arial;font-size:13px;font-weight:bold;color:red;" ><b>Importante , tener en cuenta</b></span>   
<br />            
           
<fieldset style="background:#ffffcc; -moz-border-radius: 10px 10px 0px 0px; width: 795px;">
    <span style="font-size: 8pt; font-family: Arial; color:#b30000;">Pasos a seguir:</span>
    <ul style="font-size: 8pt; font-family: Arial; color:#b30000; text-align:justify; padding-left :20px; padding-right:20px;"> 
        
<li>1) Actualización de <b>Datos de los Familiares</b>, usted podrá verificar y/o actualizar los datos de cada familiar, una vez finalizada la actualización de los datos, seleccionar el botón <b>Enviar</b>.</li>
<li>2) Actualización de <b>Datos del Alumnos</b>, usted podrá verificar y/o actualizar los datos del alumno, una vez finalizada la actualización de los datos, seleccionar el botón <b>Enviar</b>.</li>
<li>3) Actualización de <b>Datos Médicos</b>, usted podrá verificar y/o actualizar los datos médicos del alumno, una vez finalizada la actualización de los datos, seleccionar el botón <b>Enviar</b>.</li>
                
    </ul> 
</fieldset>               
               
<asp:HiddenField ID="hiddenCodigoFamilia" runat="server" />
<asp:HiddenField ID="hiddenCodigoAlumno" runat="server" Value="" />                                  
<asp:HiddenField ID="hiddenCodigoAnioAcademico" runat="server" Value="0" />
<asp:HiddenField ID="hiddenCodigoFamiliar" runat="server" Value="0" />
<asp:HiddenField ID="hiddenCodigoNivel" runat="server" Value="0" />
<asp:HiddenField ID="hiddenCodigoGrado" runat="server" Value="0" />
<asp:HiddenField ID="hiddenNivel" runat="server" Value="0" />
<asp:HiddenField ID="hiddenGrado" runat="server" Value="0" />       
<asp:HiddenField ID="hiddenNombreCompleto" runat="server" Value="0" />      
<asp:HiddenField ID="hiddenFoto" runat="server" Value="0" />                 
               
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="padding-left:10px; width: 900px; height:auto; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_centro6.png');background-repeat:repeat-y;">
            
<asp:Panel ID="pnlActualizacionDatos" runat="server">            
                <div id="miContainerMantenimiento">
                       <atk:TabContainer ID="TabContainer1" runat="server" Width="800px" 
                            ActiveTabIndex="0"
                            AutoPostBack="false" 
                            ScrollBars="None" >
                            <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
                                <HeaderTemplate>
                                    <asp:Label ID="lbTab1" runat="server" Text="Paso 1: Datos de los Familiares" />
                                 </HeaderTemplate>
                                <ContentTemplate> 
                                        <br />    
<asp:Panel ID="pnlLoadFrame1" runat="server" style="width: 770px; height: 450px; border: 0;" CssClass="MiModalBackground">
            <center>
            <table cellpadding="0" cellspacing="0" border="0" style="width:250px; border: solid 1px #000000; background-color:#FFFFFF;">
                <tr><td colspan="3"></td>
                    <td style="width: 20px;" align="center" valign="middle">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px;" align="left" valign="middle"></td>
                    <td style="width: 80px;" align="left" valign="middle">
                        <img alt="Procesando..." src="../../App_Themes/Imagenes/ajax-loader.gif" />  
                    </td>
                    <td style="width: 130px;" align="left" valign="middle">
                        <span style="color: #6684b7; font-family: Arial; font-size: 9pt; font-weight: bold;">Procesando...<br />Espere un momento.</span>
                    </td>
                    <td style="width: 20px;" align="left" valign="middle">
                    </td>
                </tr>
                <tr><td colspan="4"><br /></td></tr>
            </table>                              
            </center>
</asp:Panel>                                          
<asp:Panel ID="pnlFrame1" runat="server" style="width: 770px; height: 1900px; border: 0;" >                 
                                        <iframe id="Iframe1" runat="server" style="width: 770px; height: 100%; border: 0;" 
                                            frameborder="0">                                        
                                        </iframe>  
</asp:Panel>                                      
                                                      
                                </ContentTemplate>
                            </atk:TabPanel>  
                            <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="true">
                                <HeaderTemplate>
                                     <asp:Label ID="lbTab2" runat="server" Text="Paso 2: Datos del Alumno" />
                                </HeaderTemplate>
                                <ContentTemplate>
<asp:Panel ID="pnlLoadFrame2" runat="server" style="width: 770px; height: 450px; border: 0;" CssClass="MiModalBackground">
            <center>    
            <table cellpadding="0" cellspacing="0" border="0" style="width:250px; border: solid 1px #000000; background-color:#FFFFFF;">
                <tr><td colspan="3"></td>
                    <td style="width: 20px;" align="center" valign="middle">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px;" align="left" valign="middle"></td>
                    <td style="width: 80px;" align="left" valign="middle">
                        <img alt="Procesando..." src="../../App_Themes/Imagenes/ajax-loader.gif" />  
                    </td>
                    <td style="width: 130px;" align="left" valign="middle">
                        <span style="color: #6684b7; font-family: Arial; font-size: 9pt; font-weight: bold;">Procesando...<br />Espere un momento.</span>
                    </td>
                    <td style="width: 20px;" align="left" valign="middle">
                    </td>
                </tr>
                <tr><td colspan="4"><br /></td></tr>
            </table>          
            </center>
</asp:Panel>                                          
<asp:Panel ID="pnlFrame2" runat="server" style="width: 770px; height: 1900px; border: 0;" >                                     
                                          <iframe id="Iframe2" runat="server" style="width: 770px; height: 100%; border: 0;" 
                                            frameborder="0">
                                         </iframe>
</asp:Panel>                                      
                                </ContentTemplate> 
                            </atk:TabPanel>
                            <atk:TabPanel ID="miTab3" runat="server" HeaderText="Tab2" Enabled="true">
                                <HeaderTemplate>
                                     <asp:Label ID="Label1" runat="server" Text="Paso 3: Datos de Médicos" />
                                </HeaderTemplate>
                                <ContentTemplate>
<asp:Panel ID="pnlLoadFrame3" runat="server" style="width: 770px; height: 450px; border: 0;" CssClass="MiModalBackground">
            <center>    
            <table cellpadding="0" cellspacing="0" border="0" style="width:250px; border: solid 1px #000000; background-color:#FFFFFF;">
                <tr><td colspan="3"></td>
                    <td style="width: 20px;" align="center" valign="middle">
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px;" align="left" valign="middle"></td>
                    <td style="width: 80px;" align="left" valign="middle">
                        <img alt="Procesando..." src="../../App_Themes/Imagenes/ajax-loader.gif" />  
                    </td>
                    <td style="width: 130px;" align="left" valign="middle">
                        <span style="color: #6684b7; font-family: Arial; font-size: 9pt; font-weight: bold;">Procesando...<br />Espere un momento.</span>
                    </td>
                    <td style="width: 20px;" align="left" valign="middle">
                    </td>
                </tr>
                <tr><td colspan="4"><br /></td></tr>
            </table>          
            </center>
</asp:Panel>                                          
<asp:Panel ID="pnlFrame3" runat="server" style="width: 770px; height: 1900px; border: 0;" >                                    
                                        <iframe id="Iframe3" runat="server" style="width: 770px; height: 100%; border: 0;" 
                                            frameborder="0">
                                         </iframe>
</asp:Panel>                                      
                                </ContentTemplate> 
                            </atk:TabPanel>
                        </atk:TabContainer>                 
                </div>  
</asp:Panel>  
                              
<asp:Panel ID="pnlFinActualizacion" runat="server" style="width: 100%; text-align: center" > 
    <asp:ImageButton ID="btnSalir" runat="server" Width="221px" Height="19px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnProcesoMatricula_1.png"
        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnProcesoMatricula_2.png'"
        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnProcesoMatricula_1.png'"
        ToolTip="Regresar al Proceso de Matrícula" OnClick="btnSalir_Click" />                   
</asp:Panel>

             </td>     
        </tr>               
        <tr>
            <td style="width: 900px; height:30px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoCronogramaPago_contenedor_inferior6.png');background-repeat:no-repeat;">
            </td> 
        </tr>
    </table>        

</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

