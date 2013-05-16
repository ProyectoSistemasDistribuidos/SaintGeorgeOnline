<%@ Page Title="" Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="HomeAlumnos.aspx.vb" Inherits="Interfaz_Familia_HomeAlumnos" %>

<%@ MasterType VirtualPath="~/Interfaz_Familia/Plantilla_Principal.master" %>

<%@ Register src="../Controles/ComunicadosHome.ascx" tagname="ComunicadosHome" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table>
    <tr>
        <td>
            <table  width ="800px" >
            <tr>
                                <td align ="center">
                                    <table style =" border-right:solid 1px #a6a3a3; border-bottom:solid 1px #a6a3a3; border-top:solid 1px #a6a3a3; border-left:solid 1px #a6a3a3;">
                                        <tr>
                                            <td style="background-color:#41576f;height:18px; width:228px; padding-left:5px; " valign ="top ">
                                                <span style="font-family:Arial;font-size:11px;font-weight:bold;color:White; ">Comunicados</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align ="left"  style =" padding-left:10px ;vertical-align:top ;width:228px;height:204px ;background-color:White; background-repeat:repeat-y ; ">
                                            <div id="DetalleEventos"  style=" height: 204px; width:210px";>
                                            
                                            <marquee id="iescroller" direction="up" width="228" height="204px" scrollamount="2" scrolldelay="80" style="border:0 solid #787878;background-color:#FFFFFF" aling="center" onmouseout="this.start()" onmouseover="this.stop()"> 
                                                                                         
                                            <uc1:ComunicadosHome onmouseup="stop()" onmouseout="start()" ID="ComunicadosHome1" runat="server" /></layer></marquee> 
                                             
                                               </div> 
                                            </td>
                                           
                                        </tr>
                                    </table>
                                 </td>
                                  <td   align  ="right" valign ="top" >
                                    <img src="/saintGeorgeOnline/App_Themes/Imagenes/calen_2.png" 
                                        style="height: 50px; width: 48px" />
                                </td>
                                <td valign ="top" >
                                    <a href ="/saintGeorgeOnline/Interfaz_Familia/Modulo_Tareas/Tareas.aspx">Agenda del Alumno</a>
                                </td>
                            </tr>
            </table>
        </td>
    </tr>
</table>

</asp:Content>

