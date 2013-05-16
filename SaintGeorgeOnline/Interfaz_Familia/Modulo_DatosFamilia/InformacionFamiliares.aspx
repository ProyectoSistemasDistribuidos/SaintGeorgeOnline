<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="InformacionFamiliares.aspx.vb" Inherits="Interfaz_Familia_Modulo_DatosFamilia_InformacionFamiliares" title="Página sin título" %>

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
    font-family:Arial;
    font-size:13px; 
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
        
        <tr>
            <td style="padding-left:5px;" colspan="2">    
                <table>
                    <tr>
                        <td style="width:500px;height:25px ;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/datosfamilia_barratitulo3.jpg');background-repeat:no-repeat; ">
                            <span style="padding-left:5px; " >
                                  <asp:Label ID="lbl_NombreFamilia" runat="server" Text="" Font-Bold="true" Font-Size="13px" ForeColor="White"  ></asp:Label>
                            </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <!--FONDO CABECERA -->           
        <tr>
            <td style="width:520px;padding-left:5px;">
                &nbsp;&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width:520px;padding-left:5px;">
                <span>Familiar:</span>
                <asp:DropDownList ID="ddl_Familiar" runat="server" style="width: 321px; font-size: 8pt; font-family: Arial;"
                    OnSelectedIndexChanged="ddl_Familiar_SelectedIndexChanged" AutoPostBack="True">
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
            <td style="width:520px;padding-left:5px;">
                &nbsp;&nbsp;<span class="titulo_datos">Fecha ultima modificacion&nbsp;&nbsp;:&nbsp; </span>
                <asp:Label ID="lblFechaModificacion" runat="server" Font-Size="Small" Text="--"></asp:Label>
                &nbsp;&nbsp;&nbsp; <span class="titulo_datos">Nombre y Ap.: </span>
                <asp:Label ID="lblNombreFamiliarModificacion" runat="server" Text="--"></asp:Label>
              </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width:520px;height:45px ;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorcabV2.jpg');background-repeat:no-repeat; ">
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%" >
                    <tr>
                        <td style="padding-top:5px;width:320px;padding-left:20px;font-family:Arial;font-weight:bold;font-size:14px" >
                            <asp:Label ID="lbl_NombreCompleto" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="text-align:right;padding-right:20px;padding-top:5px;width:200px">
                            <asp:ImageButton ID="btn_SolicitarActualizarDatos" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Solicitar_f.gif" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:200px;height:45px ; ">
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
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosPersonales" runat="server" Text="Personales" />
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
                                            <span class="titulo_datos" >Apellido Paterno:</span>
                                        </td>
                                        <td>                                            
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ApePaterno" runat="server" 
                                                Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Apellido Materno:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ApeMaterno" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Nombres:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_Nombres" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Sexo:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_Sexo" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Tipo Documento:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_TipoDocIdentidad" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Nro. Documento:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_NumeroDocIdentidad" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Estado Civil:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EstadoCivil" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Vive:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_Vive" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Fecha defunción:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_FechaDefuncion" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>                                    
                                   </table>
                               </td>
                            </tr>   
          
          
                            <!--CABECERA -->
                            <tr>
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosNacimiento"  runat="server" Text="Nacimiento" />
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
                                            <span class="titulo_datos" >Fecha de Nacimiento:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_FechaNacimiento" runat="server" Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Nacionalidad:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_Nacionalidad" runat="server" Text="---"></asp:Label></span>
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
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques" >
                                        <asp:Label ID="lbl_Bloque_DatosDomicilio"  runat="server" Text="Domicilio" />
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
                                            <span class="titulo_datos" >País:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_PaisDomicilio" runat="server" 
                                                Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Departamento:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_DepartamentoDomicilio" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Provincia:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ProvinciaDomicilio" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Distrito:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_DistritoDomicilio" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Urbanización:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_UrbanizacionDomicilio" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Dirección:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_DireccionDomicilio" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Referencia domiciliaria:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ReferenciaDomicilio" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Teléfono:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_TelefonoDomicilio" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Tiene acceso a internet?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_AccesoInternetDomicilio" 
                                                runat="server" Text="---"></asp:Label></span>
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
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosLaborales"  runat="server" Text="Laborales" />
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
                                            <span class="titulo_datos" >Situación Laboral:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_SituacionLaboral" 
                                                runat="server" Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Ocupación / cargo:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_OpcupacionCargo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Centro de Trabajo:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_CentroTrabajo" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Dirección de Trabajo:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_DireccionTrabajo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">País:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_PaisCentroTrabajo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Departamento:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_DepartamentoCentroTrabajo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Provincia:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ProvinciaCentroTrabajo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Distrito:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_DistritoCentroTrabajos" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Teléfono:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_TelefonoCentroTrabajo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Celular:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_CelularCentroTrabajo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Servicio Radio:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ServicioRadioCentroTrabajo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Número Radio:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_NumeroRadioCentroTrabajo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Correo electrónico:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EmailCentroTrabajo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Tiene acceso a internet?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_TieneInternetCentroTrabajo" 
                                                runat="server" Text="---"></asp:Label></span>
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
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosEstudios"  runat="server" Text="Estudio" />
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
                                            <span class="titulo_datos" >¿Es un Ex-Alumno?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EsExalumno" runat="server" 
                                                Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Colegio de egreso:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ColegioEgreso" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Año que egreso:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_AnioEgreso" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Donde continuo estudios:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ContinuoEstudios" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Nivel de Instrucción:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_NivelInstruccion" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Escolaridad Ministerio:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EscolaridadMinisterio" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Profesion(es):</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_Profesiones" runat="server" 
                                                Text="---"></asp:Label></span>
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
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosAdicionales"  runat="server" Text="Adicionales" />
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
                                            <span class="titulo_datos" >¿Profesa alguna religión?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ProfesaReligion" 
                                                runat="server" Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Religión:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_Religion" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Nombre de la Iglesia:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_NombreIglesia" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Celular Personal:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_Celular" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Servicio Radio Personal:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ServicioRadioPersonal" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Numero Radio Personal:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_NumeroRadioPersonal" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Correo electrónico:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EmailPersonal" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Idiomas:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_IdiomasPersonal" 
                                                runat="server" Text="---"></asp:Label></span>
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
            <td style="vertical-align:top;width:200px;text-align:right; font-family:Arial;font-size:10px;">
            
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

