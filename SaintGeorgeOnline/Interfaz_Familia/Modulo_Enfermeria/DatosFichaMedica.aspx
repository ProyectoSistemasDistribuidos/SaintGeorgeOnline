<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master"  AutoEventWireup="false" CodeFile="DatosFichaMedica.aspx.vb" Inherits="Interfaz_Familia_Modulo_Enfermeria_DatosFichaMedica" title="Página sin título" %>

<%@ MasterType VirtualPath="/SaintGeorgeOnline/Interfaz_Familia/Plantilla_Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css" >

.respuesta_datos
{
	font-weight:bold;  
	font-size:11px; 
	font-family:Arial;  
}

.titulo_datos
{
    font-size:12px;	
    font-family:Arial;
    
}

.titulo_grupoInfo
{
    font-weight:bold;
    font-family:Helvetica;
    font-size:16px; 
    color:#083264
}

.titulo_datos_contenedor
{
	vertical-align:top; 
	font-family:Arial;
}
.titulo_Bloques
{
    font-size:13px;	
    font-family:Arial;
    font-weight: bold;
    color: White;
    height: 20px;
    width: 440px;
    margin-top: 10px;
    padding: 3px 0 0 20px;
    vertical-align: middle;
    text-align: left;
    display: block;
    background-color: #41576f;    
    border: solid 1px black;
}
</style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
    
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_SolicitarActualizarDatos" />
    </Triggers>
    
    <ContentTemplate>
    <table style="border-style: none; border-width: 0px; border-color: inherit; width:720px;height:100%" 
        border="0" cellpadding="0" cellspacing="0">
        
       <%-- <tr>
            <td style="padding-left:5px;" colspan="2">    
                <table>
                    <tr>
                        <td style="width:500px;height:25px ;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/datosfamilia_barratitulo3.jpg');background-repeat:no-repeat; ">
                            <span style="padding-left:5px; " >
                                  <asp:Label ID="lbl_NombreAlumno" runat="server" Text="" Font-Bold="true" Font-Size="13px" ForeColor="White"  ></asp:Label>
                            </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <!--FONDO CABECERA -->           
        <tr>
            <td style="width:520px;padding-left:5px;">
                &nbsp;&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width:520px;padding-left:5px;">
                Alumno:
                <asp:DropDownList ID="ddl_Hijo" runat="server" Height="20px" style="width: 321px; font-size: 8pt; font-family: Arial;"
                    OnSelectedIndexChanged ="ddl_Hijo_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width:520px;padding-left:5px;">
                &nbsp;&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width:520px;height:45px ;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorcabV2.jpg');background-repeat:no-repeat; ">
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%" >
                    <tr>
                        <td style="padding-top:5px;width:320px;padding-left:20px;font-family:Arial;font-weight:bold;font-size:14px      " >
                            <asp:Label ID="lbl_NombreCompleto" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="text-align:right;padding-right:20px;padding-top:5px;width:200px">
                            <asp:ImageButton ID="btn_SolicitarActualizarDatos" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Solicitar_f.gif" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:200px;height:45px ;">
                &nbsp;
            </td>
        </tr>
        <!--FONDO CUERPO (Aqui se encuentra el menu de opciones y la coleccion de vistas) -->           
        <tr>
            <!--COLECCION DE VIEWS CON LOS GRUPOS DE INFORMACION -->
            <td style="height:100%;width:520px;vertical-align:top;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorfondoV2.jpg');background-repeat:repeat-y;">

                         <table style="height:100%;vertical-align:top;text-align:left;padding-left:20px;width:460px;" border="0" cellpadding="0" cellspacing="0">
                            
                            <!--CABECERA -->
                            <tr>
                                <td>
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DesarrolloInfantil" runat="server" Text="Desarrollo Infantil" />
                                    </span>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td>
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:150px;" class="titulo_datos_contenedor">
                                            <span class="titulo_datos" >Tipo de Nacimiento:</span>
                                        </td>
                                        <td>                                            
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_TipoNacimiento" runat="server" 
                                                Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Observaciones:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ObservacionesNacimiento" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿A qué edad levantó la cabeza?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EdadLevCabeza" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿A qué edad se sentó?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EdadSento" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿A qué edad se paró?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EdadParo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿A qué edad caminó?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EdadCamino" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿A qué edad controló sus esfínteres?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EdadControloEsfinteres" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿A que edad pronunció las primeras palabras?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EdadHabloPrimerasPalabras" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿A que edad se comunicó con fluídez?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EdadHabloFluidez" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>                                    
                                   </table>
                               </td>
                            </tr>    
                                     
                                     
                            <!--CABECERA -->
                            <tr>
                                <td>
                                    <span class="titulo_Bloques" >
                                        <asp:Label ID="lbl_Bloque_EstadoSalud"  runat="server" Text="Estado de Salud" />
                                    </span>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td>
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:150px " class="titulo_datos_contenedor">
                                            <span class="titulo_datos" >Tipo de Sangre:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_TipoSangre" runat="server" Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;" class="titulo_datos_contenedor" >
                                            <br />
                                            <span class="titulo_datos" >Enfermedades:</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td align="center" colspan="2" style="width: 460px;" valign="top">
                                            <table border="1px" cellpadding="0" cellspacing="0" width="460px" style="border-color:#0c3668;border-style:solid;">
                                                <tr>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:400px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Descripción</span>
                                                    </td>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width: 60px; height: 20px; text-align: center; color: Black; font-size: 10px;background-color:#0c3668;">
                                                        <span style="color:White " >Edad</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="background-color:White;border:1px;border-color:#0c3668;border-style:solid;width: 460px; height: 25px" valign="top">
                                                        <asp:GridView ID="gvDetalleEnfermedad" runat="server" AllowPaging="False" 
                                                        EmptyDataText=" - No se encontraron resultados - "
                                                            AllowSorting="False" AutoGenerateColumns="False" CssClass="miGVBusquedaFicha" 
                                                            GridLines="Both" ShowFooter="False" ShowHeader="False" width="460px">
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="CodigoEnfermedad">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoEnfermedad" runat="server" 
                                                                            Text='<%# Bind("CodigoEnfermedad") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEnfermedad" runat="server" Text='<%# Bind("Enfermedad") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="400px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEdadEnfermedad_grilla" runat="server" 
                                                                            Text='<%# Bind("Edad") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="60px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <br />
                                            <span class="titulo_datos">Vacunas:</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td align="center" colspan="2" style="width: 460px;" valign="top">
                                            <table border="1px" cellpadding="0" cellspacing="0" width="460px" style="border-color:#0c3668;border-style:solid;">
                                                <tr>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:300px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Descripción</span>
                                                    </td>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width: 100px; height: 20px; text-align: center; color: Black; font-size: 10px;background-color:#0c3668;">
                                                        <span style="color:White " >Dosis</span>
                                                    </td>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width: 60px; height: 20px; text-align: center; color: Black; font-size: 10px;background-color:#0c3668;">
                                                        <span style="color:White " >Edad</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3" style="border:1px;border-color:#0c3668;border-style:solid;width: 460px; height: 25px;background-color:White;" valign="top">
                                                        <asp:GridView ID="gvDetalleVacuna" runat="server" AllowPaging="False" 
                                                        EmptyDataText=" - No se encontraron resultados - "
                                                            AllowSorting="False" AutoGenerateColumns="False" CssClass="miGVBusquedaFicha" 
                                                            GridLines="Both" ShowFooter="False" ShowHeader="False" width="460px">
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <Columns>                                                                
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVacuna_grilla" runat="server" Text='<%# Bind("Vacuna") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="400px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDosis_grilla" runat="server" 
                                                                            Text='<%# Bind("Dosis") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="60px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEdadVacuna_grilla" runat="server" 
                                                                            Text='<%# Bind("Edad") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="60px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <br />
                                            <span class="titulo_datos">Alergías:</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td align="center" colspan="2" style="width: 460px;" valign="top">
                                            <table border="1px" cellpadding="0" cellspacing="0" width="460px" style="border-color:#0c3668;border-style:solid;">
                                                <tr>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:400px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Descripción</span>
                                                    </td>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width: 60px; height: 20px; text-align: center; color: Black; font-size: 10px;background-color:#0c3668;">
                                                        <span style="color:White " >Tipo</span>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="border:1px;border-color:#0c3668;border-style:solid;width: 460px; height: 25px;background-color:White;" valign="top">
                                                        <asp:GridView ID="gvDetalleAlergia" runat="server" AllowPaging="False" 
                                                        EmptyDataText=" - No se encontraron resultados - "
                                                            AllowSorting="False" AutoGenerateColumns="False" CssClass="miGVBusquedaFicha" 
                                                            GridLines="Both" ShowFooter="False" ShowHeader="False" width="460px">
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <Columns>                                                                
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAlergia_grilla" runat="server" Text='<%# Bind("Alergia") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="400px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTipoAlergia_grilla" runat="server" 
                                                                            Text='<%# Bind("TipoAlergia") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="60px" />
                                                                </asp:TemplateField>                                                               
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="titulo_datos_contenedor">
                                            <br />
                                            <span class="titulo_datos">Carácteristicas de la Piel:</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td align="center" colspan="2" style="width: 460px;" valign="top">
                                            <table border="1px" cellpadding="0" cellspacing="0" width="460px" style="border-color:#0c3668;border-style:solid;">
                                                <tr>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:400px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Descripción</span>
                                                    </td> 
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="border:1px;border-color:#0c3668;border-style:solid;width: 460px; height: 25px;background-color:White;" valign="top">
                                                        <asp:GridView ID="gvDetalleCaracteristicaPiel" runat="server" AllowPaging="False" 
                                                        EmptyDataText=" - No se encontraron resultados - "
                                                            AllowSorting="False" AutoGenerateColumns="False" CssClass="miGVBusquedaFicha" 
                                                            GridLines="Both" ShowFooter="False" ShowHeader="False" width="460px">
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <Columns>                                                                
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAlergia_grilla" runat="server" Text='<%# Bind("CaracteristicaPiel") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="460px" />
                                                                </asp:TemplateField>                                                                                                                           
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                       <tr>
                                           <td class="titulo_datos_contenedor">
                                            <br />
                                               <span class="titulo_datos">Medicamentos:</span>
                                           </td>
                                           <td>
                                               &nbsp;
                                           </td>
                                       </tr>
                                       <tr>                                        
                                        <td align="center" colspan="2" style="width: 460px;" valign="top">
                                            <table border="1px" cellpadding="0" cellspacing="0" width="460px" style="border-color:#0c3668;border-style:solid;">
                                                <tr>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:150px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Descripción</span>
                                                    </td> 
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:100px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Presentación</span>
                                                    </td> 
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:100px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Dosis</span>
                                                    </td> 
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:110px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Observación</span>
                                                    </td> 
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="4" style="border:1px;border-color:#0c3668;border-style:solid;width: 460px; height: 25px;background-color:White;" valign="top">
                                                        <asp:GridView ID="gvDetalleMedicamento" runat="server" AllowPaging="False" 
                                                        EmptyDataText=" - No se encontraron resultados - "
                                                            AllowSorting="False" AutoGenerateColumns="False" CssClass="miGVBusquedaFicha" 
                                                            GridLines="Both" ShowFooter="False" ShowHeader="False" width="460px">
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <Columns>                                                                
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAlergia_grilla" runat="server" Text='<%# Bind("Medicamento") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="150px" />
                                                                </asp:TemplateField>    
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPresentCant_grilla" runat="server" Text='<%# Bind("PresentCant") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="100px" />
                                                                </asp:TemplateField>   
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDosisMedicamento_grilla" runat="server" Text='<%# Bind("DosisMedicamento") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="100px" />
                                                                </asp:TemplateField>      
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblObservaciones_grilla" runat="server" Text='<%# Bind("Observaciones") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="110px" />
                                                                </asp:TemplateField>                                                                                                                           
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                   </table>
                               </td>
                            </tr>     
                                     
                                     
                            <!--CABECERA -->
                            <tr>
                                <td>
                                    <span class="titulo_Bloques" >
                                        <asp:Label ID="lbl_Bloque_OtrosDatosMedicos"  runat="server" text="Otros Datos Médicos" />
                                    </span>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td>
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:150px " class="titulo_datos_contenedor">
                                            <span class="titulo_datos" >¿Tiene el Tabique desviado?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_TabiqueDesviado" runat="server" 
                                                Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Ha presentado Sangrado Nasal?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_SangradoNasal" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Observación Oftalmológico:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ObservacionesOftamologicas" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Usa lentes?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_UsaLentes" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Observación Dental:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ObservacionesDental" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Usa Aparatos de Ortodoncia?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_UsaOrtodoncia" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <br />
                                            <span class="titulo_datos">Hospitalizaciones:</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td align="center" colspan="2" style="width: 460px;" valign="top">
                                    <table border="1px" cellpadding="0" cellspacing="0" width="460px" style="border-color:#0c3668;border-style:solid;">
                                    <tr>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:300px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Hospitalizacion</span>
                                                    </td> 
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:160px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Fecha de Hospitalizacion</span>
                                                    </td> 
                                                    
                                                </tr>
                                    <tr>
                                                    <td align="center" colspan="4" style="border:1px;border-color:#0c3668;border-style:solid;width: 460px; height: 25px;background-color:White;" valign="top">
                                                        <asp:GridView ID="gvDetalleHospitalizacion" runat="server" AllowPaging="False" 
                                                        EmptyDataText=" - No se encontraron resultados - "
                                                            AllowSorting="False" AutoGenerateColumns="False" CssClass="miGVBusquedaFicha" 
                                                            GridLines="Both" ShowFooter="False" ShowHeader="False" width="460px">
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <Columns>                                                                
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHospitalizacion_grilla" runat="server" Text='<%# Bind("Hospitalizacion") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="150px" />
                                                                </asp:TemplateField>    
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFechaHospitalizacion_grilla" runat="server" Text='<%# Bind("FechaHospitalizacion") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="100px" />
                                                                </asp:TemplateField>                                                                                                 
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                    </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <br />
                                            <span class="titulo_datos">Operaciones:</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td align="center" colspan="2" style="width: 460px;" valign="top">
                                    <table border="1px" cellpadding="0" cellspacing="0" width="460px" style="border-color:#0c3668;border-style:solid;">
                                    <tr>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:300px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Operacion</span>
                                                    </td> 
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:160px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Fecha de Operacion</span>
                                                    </td> 
                                                    
                                                </tr>
                                    <tr>
                                                    <td align="center" colspan="4" style="border:1px;border-color:#0c3668;border-style:solid;width: 460px; height: 25px;background-color:White;" valign="top">
                                                        <asp:GridView ID="gvDetalleOperacion" runat="server" AllowPaging="False" 
                                                        EmptyDataText=" - No se encontraron resultados - "
                                                            AllowSorting="False" AutoGenerateColumns="False" CssClass="miGVBusquedaFicha" 
                                                            GridLines="Both" ShowFooter="False" ShowHeader="False" width="460px">
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <Columns>                                                                
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOperacion_grilla" runat="server" Text='<%# Bind("Operacion") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="300px" />
                                                                </asp:TemplateField>    
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFechaOperacion_grilla" runat="server" Text='<%# Bind("FechaOperacion") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="160px" />
                                                                </asp:TemplateField>                                                                                                 
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                    </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                   </table>
                               </td>
                            </tr>   
                    
                            <!--CABECERA -->
                            <tr>
                                <td>
                                    <span class="titulo_Bloques" >
                                        <asp:Label ID="lbl_Bloque_ControlSalud"  runat="server" text="Controles de Salud" />
                                    </span>                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td>
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:150px " class="titulo_datos_contenedor">
                                            <br />
                                            <span class="titulo_datos" >Control de Peso - Talla:</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td align="center" colspan="2" style="width: 460px;" valign="top">
                                    <table border="1px" cellpadding="0" cellspacing="0" width="460px" style="border-color:#0c3668;border-style:solid;">
                                    <tr>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:300px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Talla</span>
                                                    </td> 
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:160px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Peso</span>
                                                    </td> 
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:160px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Fecha de Control</span>
                                                    </td> 
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:160px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Observaciones</span>
                                                    </td> 
                                                    
                                                </tr>
                                    <tr>
                                                    <td align="center" colspan="4" style="border:1px;border-color:#0c3668;border-style:solid;width: 460px; height: 25px;background-color:White;" valign="top">
                                                        <asp:GridView ID="gvDetalleControlPesoTalla" runat="server" AllowPaging="False" 
                                                        EmptyDataText=" - No se encontraron resultados - "
                                                            AllowSorting="False" AutoGenerateColumns="False" CssClass="miGVBusquedaFicha" 
                                                            GridLines="Both" ShowFooter="False" ShowHeader="False" width="460px">
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <Columns>                                                                
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTalla_grilla" runat="server" Text='<%# Bind("Talla") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="300px" />
                                                                </asp:TemplateField>    
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPeso_grilla" runat="server" Text='<%# Bind("Peso") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="160px" />
                                                                </asp:TemplateField>   
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFechaControl_grilla" runat="server" Text='<%# Bind("FechaControl") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="160px" />
                                                                </asp:TemplateField>   
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblObservaciones_grilla" runat="server" Text='<%# Bind("Observaciones") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="160px" />
                                                                </asp:TemplateField>                                                                                                 
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                    </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <br />
                                            <span class="titulo_datos">Otros de Controles:</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td align="center" colspan="2" style="width: 460px;" valign="top">
                                    <table border="1px" cellpadding="0" cellspacing="0" width="460px" style="border-color:#0c3668;border-style:solid;">
                                    <tr>
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:100px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Tipo de Control</span>
                                                    </td> 
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:260px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Resultado</span>
                                                    </td> 
                                                    <td align="center" 
                                                        style="border:1px;border-color:#0c3668;border-style:solid;width:100px; height: 20px; text-align: center; color: Black;
                                                        font-size: 10px;background-color:#0c3668; ">
                                                        <span style="color:White " >Fecha de Control</span>
                                                    </td> 
                                                </tr>
                                    <tr>
                                                    <td align="center" colspan="4" style="border:1px;border-color:#0c3668;border-style:solid;width: 460px; height: 25px;background-color:White;" valign="top">
                                                        <asp:GridView ID="gvDetalleTipoControl" runat="server" AllowPaging="False" 
                                                        EmptyDataText=" - No se encontraron resultados - "
                                                            AllowSorting="False" AutoGenerateColumns="False" CssClass="miGVBusquedaFicha" 
                                                            GridLines="Both" ShowFooter="False" ShowHeader="False" width="460px">
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <Columns>                                                                
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTalla_grilla" runat="server" Text='<%# Bind("TipoControl") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="100px" />
                                                                </asp:TemplateField>    
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPeso_grilla" runat="server" Text='<%# Bind("Resultado") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="260px" />
                                                                </asp:TemplateField>   
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFechaControl_grilla" runat="server" Text='<%# Bind("FechaControl") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" 
                                                                        Width="100px" />
                                                                </asp:TemplateField>                                                                        
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                    </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                   </table>
                               </td>
                            </tr>       
                                                 
                         </table>
                         
            </td>
            <!--MENU DE GRUPOS DE INFORMACION -->
            <td style="vertical-align:top;width:200px;text-align:right; font-family:Arial;font-size:10px; ">
                               
            </td>
             
        </tr>  
        <!--FONDO PIE -->                  
        <tr>
            <td style="width: 520px;height:24px; text-align: top; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorfondo_pieV2.jpg'); background-repeat: no-repeat ">
                &nbsp;</td>
            <td style="width: 200px;height:24px; text-align: right; font-family: Arial; font-size: 10px; ">
                &nbsp;</td>
        </tr>
        
    </table>    
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

