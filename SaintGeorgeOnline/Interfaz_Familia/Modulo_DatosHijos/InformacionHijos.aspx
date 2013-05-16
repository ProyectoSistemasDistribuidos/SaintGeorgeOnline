<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="InformacionHijos.aspx.vb" Inherits="Interfaz_Familia_Modulo_DatosAlumnos_InformacionAlumno" title="Página sin título" %>

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
    <ContentTemplate>
    
    <table style="width:720px;height:100%; border: solid 0px red;" border="0" cellpadding="0" cellspacing="0">

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
                <span>Alumno:</span>
                <asp:DropDownList ID="ddl_Alumno" runat="server" style="width: 321px; font-size: 8pt; font-family: Arial;"
                    OnSelectedIndexChanged="ddl_Alumno_SelectedIndexChanged" AutoPostBack="True">
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
                &nbsp;&nbsp;<span class="titulo_datos">Fecha Ultima Actualización:</span><asp:Label 
                    ID="lblFechaActualizacion" runat="server"   Text="--" Font-Size="Small"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <span class="titulo_datos">Nombre Ap.</span>
                <asp:Label ID="lblNombreActualizacion" runat="server" Text="--" 
                    Font-Size="Small"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>                    
        <tr>
            <td style="width:520px;height:45px ;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorcabV2.jpg');background-repeat:no-repeat; ">
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%" >
                    <tr>
                        <td style="padding-top:5px;width:320px;padding-left:20px;font-family:Arial;font-weight:bold;font-size:14px; " >
                            <asp:Label ID="lbl_NombreCompleto" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="text-align:right;padding-right:20px;padding-top:5px;width:200px">
                            <asp:ImageButton ID="btn_SolicitarActualizarDatos" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Solicitar_f.gif"  Visible="false" />
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
                                            <span class="titulo_datos" >Código de Alumno:</span>
                                        </td>
                                        <td>                                            
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_CodigoAlumno" runat="server" 
                                                Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Código del Educando:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_CodigoEducando" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Apellido Paterno:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_ApePaterno" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Apellido Materno:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_ApeMaterno" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Nombres:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_Nombres" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Sexo:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_Sexo" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Tipo de Documento:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_TipoDocIdentidad" runat="server" 
                                                Text="---"></asp:Label></span>
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
                                            <span class="titulo_datos" >Nacimiento Registrado:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_NacimientoRegistrado" runat="server" Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
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
                                            <span class="titulo_datos">Pais:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_PaisNacimiento" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Departamento:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_DepartamentoNacimiento" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Provincia:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ProvinciaNacimiento" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Distrito:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_DistritoNacimiento" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">1° Nacionalidad:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_Nacionalidad_1" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">2° Nacionalidad:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_Nacionalidad_2" 
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
                                        <asp:Label ID="lbl_Bloque_DatosSeguro"  runat="server" Text="Seguro" />
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
                                            <span class="titulo_datos" >Año de Matrícula:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_AnioMatriculaSeguro" 
                                                runat="server" Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Tipo de Seguro:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_TipoSeguro" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Compañia de Seguro:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_CompañiaSeguro" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Número de Poliza:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_NumeroPolizaSeguro" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Vigencia de Seguro:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_VigenciaSeguro" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Fechas de Vigencia:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_FechaVigenciaSeguro" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Clinicas:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ClinicaSeguro" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Compañia de Ambulancia:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_AmbulanciaSeguro" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Teléfono de Compañia de Ambulancia:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_TelefonoAmbulanciaSeguro" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Copia de Carnet de Seguro:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_CopiaCarnetSeguro" 
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
                                        <asp:Label ID="lbl_Bloque_DatosReligiosos" runat="server" Text="Religioso" />
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
                                            <span class="titulo_datos" >¿Pofesa alguna religión?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_ProfesaReligion" runat="server" 
                                                Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Religión que profesa:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_Religion" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Ha sido bautizado?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_SeBautizo" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Lugar de Bautizo:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_LugarBautizo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Año de Bautizo:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_AnioBautizo" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Ha dado la primera comunión?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_SePrimeraComunion" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Lugar de Primera Comunión:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_PrimeraComunionLugar" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Año de Primera Comunión:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_PrimeraComunionAnio" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Se ha confirmado?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_SeConfirmo" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Lugar de Confirmación:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_LugarConfirmacion" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Año de Confirmación:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_ConfirmacionAnio" runat="server" 
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
                                        <asp:Label ID="lbl_Bloque_DatosFacturacion" runat="server" Text="Facturación" />
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
                                            <span class="titulo_datos" >Apoderado(s):</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_Apoderados" 
                                                runat="server" Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Responsable Económico:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_ResponsableEconomico" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Emitir Factura?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_EmitirFactura" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Razón Social:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_RazonSocial" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Ruc:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_Ruc" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Dirección de Empresa a facturar:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_DirecEmpresaFacturar" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Familiares Registrados con quien vive:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_FamiliaresRegistradosVive" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Otra persona con quien vive:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_OtrasPersonasVive" 
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
                                        <asp:Label ID="lbl_Bloque_DatosEmergencia" runat="server" Text="Emergencia" />
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
                                            <span class="titulo_datos" >Nombres y Apellidos:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_ContactoEmergencia" 
                                                runat="server" Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Teléfono:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_TelefonosEmergencia" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Celular:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_CelularEmergencia" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Telefono Oficina:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lbl_TelfOficinaEmergencia" 
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
                                        <asp:Label ID="lbl_Bloque_DatosOtros" runat="server" Text="Otros" />
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
                                            <span class="titulo_datos" >Lengua Materna:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_LenguaMaterna" runat="server" 
                                                Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Segunda Lengua:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_SegundaLengua" runat="server" 
                                                Text="---"></asp:Label></span>
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
                                            <span class="titulo_datos">Cantidad de Hermanos:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_CantidadHermanos" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Posición entre los Hermanos:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_PosicionEntreHermanos" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Correo electónico Personal:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EmailPersonal" runat="server" 
                                                Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Telefono Móvil:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_CelularPersonal" 
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
                                        <asp:Label ID="lbl_Bloque_DatosEspeciales" runat="server" Text="Especiales" />
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
                                            <span class="titulo_datos" >¿Tiene discapacidad?:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="Label8" runat="server" Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Descripción:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="Label9" runat="server" Text="---"></asp:Label></span>
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
                                        <asp:Label ID="lbl_Bloque_DatosSituacionActual" runat="server" Text="Situación Actual" />
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
                                            <span class="titulo_datos" >Estado:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_EstadoActual" runat="server" 
                                                Text="---" ></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Año Académico:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_AnioAcademicoActual" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Nivel/Grado/Sección:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_NivelGradoSeccionActual" 
                                                runat="server" Text="---"></asp:Label></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_datos_contenedor">
                                            <span class="titulo_datos">House:</span>
                                        </td>
                                        <td>
                                            <span class="respuesta_datos" ><asp:Label ID="lbl_house" runat="server" 
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
                                  <tr>
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="Label1" runat="server" Text="Informacion adicional" />
                                    </span>
                                   <br />
                                   
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                          <%--          
                        nombre
                        AL_CodigoAlumno
                        PT_Descripcion	
                        responsablePago
                        viveConElla
	
	--%>
                                    
                                    
                                    <asp:GridView ID="grwInformacionAdicional" runat="server" 
                                        AutoGenerateColumns="False"
                                        Width="460px" 
                                        GridLines="Horizontal">
                          <HeaderStyle BackColor="#41576f" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" Height="26px" />
                          <RowStyle Height="26px" />
                                   <Columns>
                                      <asp:TemplateField HeaderText="Nombre del Familiar" ItemStyle-Width="220px" ItemStyle-HorizontalAlign="Left">                                                                      
                                                <ItemTemplate >
                                                    <asp:Label ID="lblListaDescActualizado1" runat="server" Text='<%# Bind("nombre")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                               <asp:TemplateField HeaderText="Parentesco" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left">                                                                      
                                                <ItemTemplate >
                                                    <asp:Label ID="lblListaDescActualizado2" runat="server" Text='<%# Bind("PT_Descripcion")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                               <asp:TemplateField HeaderText="Responsable de pago" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">                                                                      
                                                <ItemTemplate >
                                                    <asp:Label ID="lblListaDescActualizado3" runat="server" Text='<%# Bind("responsablePago")%>' Font-Bold="true" />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>  
                                               <asp:TemplateField HeaderText="Vive con" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">                                                                      
                                                <ItemTemplate >
                                                    <asp:Label ID="lblListaDescActualizado4" runat="server" Text='<%# Bind("viveConElla")%>' Font-Bold="true" />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                   </Columns>
                                    </asp:GridView>
                                   
                                    
                                    
                                    </td>
                            </tr>
                                                     
                         </table>
                         
            </td>
            <!--MENU DE GRUPOS DE INFORMACION -->            
            <td style="border: solid 0px red; vertical-align:top;width:200px;text-align:right; font-family:Arial;font-size:10px;">            
              
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

