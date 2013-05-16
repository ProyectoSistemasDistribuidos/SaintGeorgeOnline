<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="NominaMatricula.aspx.vb" Inherits="Modulo_Matricula_NominaMatricula" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
        .FondoAplicacion
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        #miTablaFiltros span
        {
            margin: 0;
            padding: 0;
            font-size: 11px;
            font-family: Arial;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <script type="text/javascript">

          function ShowMyModalPopup() {
              var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
              modal.show();
          }
      
    </script>
    <div id="miBusquedaArchivo">
        <fieldset>                     
            <legend>Selección de Plantilla a llenar</legend>
            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 810px;">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="130px">
                        <span>Nombre del Archivo:</span> <span>(*)&nbsp; </span>&nbsp;
                    </td>
                    <td width="680px">
                        <asp:FileUpload ID="FiUpNominaSIAGE" runat="server" Width="680px"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblArchivo" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div class="miEspacio">
    </div>
    <div id="miBusquedaMant_Alumno">
       <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <fieldset>
            <legend>Selección de salón a exportar</legend>
            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 810px;">
                <tr>
                    <td style="width: 30px; height: 25px;" align="left" valign="middle">
                        <span>Sede :</span>
                    </td>
                    <td style="width: 460px; height: 25px;" align="left" valign="middle">
                        <asp:DropDownList ID="ddlSede" runat="server" Width="110px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 310px; padding-top: 6px" align="right" valign="top" rowspan="2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                        <asp:PostBackTrigger ControlID ="btnBuscar"/>
                        </Triggers>
                            <ContentTemplate>
                                <asp:ImageButton ID="btnBuscar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                    onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                    OnClick="btnBuscar_Click" ToolTip="Exportar Registros" />
                                <br />
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; height: 25px;" align="left" valign="middle">
                        <span>Año :</span>
                    </td>
                    <td colspan="2" style="width: 770px; height: 25px;" align="left" valign="middle">
                        <asp:DropDownList ID="ddlAnioAcademico1" runat="server" Width="110px">
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td colspan ="3" style="width: 810px; height: 25px;" align="left" valign="middle">
                        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 810px;">
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Aulas :&nbsp;</span>
                                </td>
                                <td style="width: 320px; height: 25px;" align="left" valign="middle">
                                    <asp:DropDownList ID="ddlAulas" runat="server" Width="313px" style="font-size: 8pt; font-family: Courier New;" OnSelectedIndexChanged="ddlAulas_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList> 
                                    <asp:HiddenField ID="hiddenCodigoAnioAcademico" runat="server" Value="0" />
                                    <asp:HiddenField ID="hiddenCodigoGrado" runat="server" Value="0" />                                    
                                    <asp:HiddenField ID="hiddenCodigoAula" runat="server" Value="0" />                                    
                                </td>
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Nivel :&nbsp;</span>
                                </td>
                                <td style="width: 80px; height: 25px;" align="left" valign="middle">
                                    <asp:TextBox ID="tbNivel" runat="server" Width="70px" style="font-size: 8pt; font-family: Arial;" Enabled="false" />
                                </td>         
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Grado :&nbsp;</span>
                                </td>                                                  
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <asp:TextBox ID="tbGrado" runat="server" Width="40px" style="font-size: 8pt; font-family: Arial;" Enabled="false"/>
                                </td>
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Sección :&nbsp;</span>
                                </td>
                                <td style="width: 70px; height: 25px;" align="left" valign="middle">
                                    <asp:TextBox ID="tbSeccion" runat="server" Width="40px" style="font-size: 8pt; font-family: Arial;" Enabled="false"/>
                                </td>
                                <td style="width: 80px; height: 25px;" align="center" valign="middle">
                                </td>
                        </table>  
                    </td>    
                </tr>
               <%-- <tr>
                    <td style="width: 180px; height: 25px;" align="left" valign="middle">
                        <span>Nivel</span>
                    </td>
                    <td colspan="2" style="width: 630px; height: 25px;" align="left" valign="middle">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlNivelesMinisterio" runat="server" Width="110px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlNivelesMinisterio_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 180px; height: 25px;" align="left" valign="middle">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblGrado" runat="server" Text="Grado"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td colspan="2" style="width: 630px; height: 25px;" align="left" valign="middle">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlGradosMinisterios" runat="server" Width="110px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlGradosMinisterios_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 180px; height: 25px;" align="left" valign="middle">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblSecc" runat="server" Text="Sección"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td colspan="2" style="width: 630px; height: 25px;" align="left" valign="middle">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlAulasMinisterio" runat="server" Width="110px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlAulasMinisterio_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;<asp:Label ID="lblNomSecc" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 50px; height: 25px;" align="left" valign="middle">
                        &nbsp;
                    </td>
                    <td colspan="2" style="width: 750px; height: 25px;" align="left" valign="middle">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>    
    </div>
    <div class="miEspacio">
    </div>
    <atk:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
        Enabled="True" BackgroundCssClass="FondoAplicacion" DropShadow="True" PopupControlID="pnlImpresion"
        TargetControlID="lblAccionExportar">
    </atk:ModalPopupExtender>
    <asp:Panel ID="pnlImpresion" runat="server" BackColor="White" Height="71px" Width="388px"
        Style="display: none">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: right;">
                    <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10px; color: #000080;
                    text-align: center;">
                    <img alt="" src="../App_Themes/Imagenes/bigrotation2.gif" style="width: 32px; height: 32px" />
                    <br />
                    Exportando, espere unos segundos ...
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Label ID="lblAccionExportar" runat="server" ForeColor="White" Text="..." />
    <script type="text/javascript">

        $(document).ready(function() {

            $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
            $("#menu").hide('fast');
            $("#menu").width(0);
            $("#contenido").width(893);

        });

    </script>

</asp:Content>

