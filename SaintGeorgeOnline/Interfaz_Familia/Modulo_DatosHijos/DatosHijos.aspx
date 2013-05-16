<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="DatosHijos.aspx.vb" Inherits="Interfaz_Familia_Modulo_DatosAlumnos_DatosAlumnos" title="Página sin título" %>

<%@ MasterType VirtualPath="/SaintGeorgeOnline/Interfaz_Familia/Plantilla_Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css" >

.miHiddenStyle{
    display:none;
}

</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="border-style: none; border-width: 0px; border-color: inherit; width:100%;" 
        border="0" cellpadding="0" cellspacing="0">
        
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style ="width:730px ;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/datosfamilia_contenedor_cab.jpg');background-repeat:no-repeat ">
                    &nbsp;
                    </td>
                </tr>
                    
                <tr>
                    <td style="padding-left:20px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/datosfamilia_contenedor_centr.jpg');background-repeat:repeat-y; ">
                        <asp:GridView ID="dgv_DatosHijos" 
                                      runat="server" 
                                      AutoGenerateColumns="False" 
                                      BorderStyle="None" 
                                      BorderWidth="0px"
                                      OnRowDataBound="dgv_DatosHijos_RowDataBound" 
                                      GridLines="None" Width="660px">
                            <Columns>
                                <asp:BoundField DataField="CodigoFamilia" >
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle"/>
                                    <ItemStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                            <tr>
                                                <td style="font-family:Arial;font-size:13px;padding-left:20px;width:550px;height:25px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/datosfamilia_barratitulo.jpg');background-repeat:no-repeat; " >
                                                    <asp:Image ID="img_iconofamilia" runat="server" ImageAlign="Bottom" 
                                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/icono_familia.gif" 
                                                        Height="22px" Width="22px" /> &nbsp;&nbsp;
                                                    <span style="vertical-align:bottom ;font-weight:bold;  "><b>Familia:  <asp:Label ID="Label1" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left:40px;" >
                                                    <asp:GridView ID="dgv_Hijos" 
                                                                  runat="server"
                                                                  AutoGenerateColumns="False"
                                                                  BorderStyle="None" 
                                                                  BorderWidth="0px"
                                                                  GridLines="None"
                                                                  >
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                                                        <tr>
                                                                            <td style="width:127px;height:127px ;font-family:Arial;font-size:13px;font-weight:bold;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorfondo2.jpg');background-repeat:no-repeat;  " 
                                                                                rowspan="3">
                                                                                <span style="padding-left:20px;  ">
                                                                                <asp:Image ID="img_Foto" runat="server" Height="74" Width="64" ImageUrl='<%# Bind("RutaFoto") %>' />
                                                                                <br />
                                                                                </span> 
                                                                            </td>
                                                                            
                                                                            <td style="height:127px;font-family: Arial; font-size: 13px; font-weight: bold;  width: 489px; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorfondo2_izq.jpg'); background-repeat: no-repeat;">
                                                                                <table style="height:100px" border="0" cellpadding="0" cellspacing="0" >
                                                                                    <tr>
                                                                                        <td style="padding-top:10px;padding-left: 15px; ">
                                                                                            <span style="font-weight:bold ;font-family:Arial;font-size:13px"><asp:Label ID="Label2" runat="server" Text='<%# Bind("NombreCompleto") %>'></asp:Label></span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 15px;font-family:Arial;font-size:11px;font-weight:normal;">
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
                                                                            <td style="padding-left: 15px;padding-top:5px ">
                                                                                
                                                                                <asp:ImageButton ID="btn_Consultar" runat="server" 
                                                                                    ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Consultar_f.gif" 
                                                                                    onclick="btn_Consultar_Click" ValidationGroup='<%# Bind("CodigoAlumno") %>'  />
                                                                                &nbsp;&nbsp;
                                                                                <asp:ImageButton ID="btn_SolicitarActualizarDatos" 
                                                                                    ValidationGroup='<%# Bind("CodigoAlumno") %>' runat="server" Visible="false"
                                                                                    ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Solicitar_f.gif" 
                                                                                    onclick="btn_SolicitarActualizarDatos_Click" />
                                                                            </td>
                                                                        </tr>
                                                                                </table>                                                                               
                                                                                
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                        
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                </asp:GridView>
                        &nbsp;
                    </td>
                </tr>
                
                <tr>
                    <td style="background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/datosfamilia_contenedor_pie.jpg');background-repeat:no-repeat; " >
                    &nbsp;
                    </td>
                </tr>
                </table> 
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

