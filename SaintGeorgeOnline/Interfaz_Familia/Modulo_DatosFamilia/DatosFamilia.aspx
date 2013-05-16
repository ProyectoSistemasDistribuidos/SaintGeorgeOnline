<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="DatosFamilia.aspx.vb" Inherits="Interfaz_Familia_Modulo_DatosFamilia_DatosFamilia" title="Página sin título" %>

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
                        <asp:GridView ID="dgv_DatosFamilia" 
                                      runat="server" 
                                      AutoGenerateColumns="False" 
                                      BorderStyle="None" 
                                      BorderWidth="0px"
                                      OnRowDataBound="dgv_DatosFamilia_RowDataBound" 
                                      GridLines="None">
                            <Columns>
                                                                
                                <asp:BoundField DataField="CodigoFamilia" >
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle"/>
                                    <ItemStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle" />
                                </asp:BoundField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                            <tr>
                                                <td style="font-family:Arial;font-size:13px;padding-left:20px;width:489px;height:25px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/datosfamilia_barratitulo.jpg');background-repeat:no-repeat; " >
                                                    <asp:Image ID="img_iconofamilia" runat="server" ImageAlign="Bottom" 
                                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/icono_familia.gif" 
                                                        Height="22px" Width="22px" /> &nbsp;&nbsp;
                                                    <span style="vertical-align:bottom ;font-weight:bold;  "><b>Familia:  <asp:Label ID="Label1" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left:40px;" >
                                                    <asp:GridView ID="dgv_Familiares" 
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
                                                                            <td style="padding-top:10px ;font-family:Arial;font-size:13px;font-weight:bold;padding-left:20px;width:489px;height:157px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorfondo3.jpg');background-repeat:no-repeat;">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span style="font-weight:bold ;font-family:Arial;font-size:13px"><asp:Label  ID="Label2" runat="server" Text='<%# Bind("NombreCompleto") %>'></asp:Label></span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span style="font-weight:normal  ;font-family:Arial;font-size:11px">Parentesco:</span><span style="font-weight:bold  ;font-family:Arial;font-size:11px"><asp:Label ID="Label3" runat="server" Text='<%# Bind("Parentesco") %>'></asp:Label></span></td>
                                                                                    </tr>
                                                                                   
                                                                                    <tr>
                                                                                        <td style="padding-left:70px;padding-top:30px; " >
                                                                                            <asp:ImageButton ID="btn_Consultar" runat="server" 
                                                                                                 ValidationGroup='<%# Bind("CodigoFamiliar") %>'                                                                                                 
                                                                                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Consultar_f.gif" 
                                                                                                onclick="btn_Consultar_Click" />
                                                                                            &nbsp;&nbsp;&nbsp;
                                                                                            <asp:ImageButton ID="btn_SolicitarActualizarDatos" runat="server" 
                                                                                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Solicitar_f.gif" 
                                                                                                ValidationGroup='<%# Bind("CodigoFamiliar") %>' 
                                                                                                onclick="btn_SolicitarActualizarDatos_Click"  />
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

