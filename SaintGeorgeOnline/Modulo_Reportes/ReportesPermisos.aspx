<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ReportesPermisos.aspx.vb" Inherits="Modulo_Reportes_ReportesPermisos" title="P�gina sin t�tulo" %>
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
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 2px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

        function ShowMyModalPopup() {
            var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
            modal.show();
        }
      
    </script>
    <div id="miPaginaMantenimiento">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReporteExportar" />
            </Triggers>
            <ContentTemplate>
                <div style="border: solid 0px blue; width: 860px;">
                    <div id="miBusquedaActualizacion_Ficha">
                        <!-- 650px -->
                        <table style="width: 850px;" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="border-style: solid; border-width: 1px; border-color: #a6a3a3; background-color: #0a0f14;
                                    width: 200px; height: 26px; color: White; font-size: 10px; font-family: Arial;"
                                    align="left" valign="middle">
                                    <span style="padding-left: 30px;">Reporte</span>
                                </td>
                                <td style="border-style: solid; border-width: 1px; border-color: #a6a3a3; background-color: #0a0f14;
                                    width: 200px; height: 26px; color: White; font-size: 10px; font-family: Arial;"
                                    align="left" valign="middle">
                                    <span style="padding-left: 30px;">Presentaci�n</span>
                                </td>
                                <td style="border-style: solid; border-width: 1px; border-color: #a6a3a3; background-color: #0a0f14;
                                    width: 300px; height: 26px; color: White; font-size: 10px; font-family: Arial;"
                                    align="left" valign="middle">
                                    <span style="padding-left: 30px;">Filtros</span>
                                </td>
                                <td style="border-style: solid; border-width: 1px; border-color: #a6a3a3; background-color: #0a0f14;
                                    width: 150px; height: 26px; color: White; font-size: 10px; font-family: Arial;"
                                    align="center" valign="middle">
                                </td>
                            </tr>
                            <tr>
                                <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size: 10px;"
                                    valign="top" align="left">
                                    <asp:ListBox ID="lstReportes" runat="server" Style="height: 250px; width: 200px;
                                        border: 0; font-weight: bold; font-size: 11px; font-family: Arial;" OnSelectedIndexChanged="lstReportes_SelectedIndexChanged"
                                        AutoPostBack="True"></asp:ListBox>
                                </td>
                                <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size: 10px;"
                                    valign="top" align="left">
                                    <asp:ListBox ID="lstPresentacion" runat="server" Style="height: 250px; width: 200px;
                                        border: 0; font-weight: bold; font-size: 11px; font-family: Arial;"></asp:ListBox>
                                </td>
                                <td style="border-style: solid; border-width: 1px; border-color: #a6a3a3; width: 300px;
                                    height: auto;" valign="top" align="left">
                                    <asp:Panel ID="pnlReporte1" runat="server" BackColor="White" Style="width: 300px;
                                        height: 200px;" Height="149px" Width="396px">
                                        <%--<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 420px;">
                                            <tr>
                                                <td style="padding-left: 10px; width: 200px; height: 25px;" align="left" valign="middle">
                                                    <span>De:&nbsp;
                                                    <br />
                                                    <asp:Label ID="lblVerFechaInicio" runat="server" />
                                                    </span>
                                                </td>
                                                <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                    <asp:TextBox ID="tbFechaInicio" runat="server" CssClass="miTextBoxCalendar" />
                                                    <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="tbFechaInicio"
                                                        UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True">
                                                    </atk:MaskedEditExtender>
                                                </td>
                                                <td valign="middle" align="left" style="width: 280px; height: 25px;">
                                                    <atk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbFechaInicio"
                                                        PopupButtonID="Image1" Format="dd/MM/yyyy" CssClass="MyCalendar" Enabled="True" />
                                                    <asp:ImageButton ID="Image1" runat="server" 
                                                        AlternateText="Elija una fecha del calendario" 
                                                        ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; width: 200px; height: 25px;" align="left" valign="middle">
                                                    <span>Hasta: 
                                                    <asp:Label ID="lblVerFechaFin" runat="server" />
                                                    </span>
                                                </td>
                                                <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                    &nbsp;<asp:TextBox ID="tbFechaFin" runat="server" CssClass="miTextBoxCalendar" />
                                                    <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="tbFechaFin"
                                                        UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True">
                                                    </atk:MaskedEditExtender>
                                                </td>
                                                <td valign="middle" align="left" style="width: 280px; height: 25px;">
                                                    <atk:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbFechaFin"
                                                        PopupButtonID="Image2" Format="dd/MM/yyyy" CssClass="MyCalendar" Enabled="True" />
                                                    <asp:ImageButton ID="Image2" runat="server" 
                                                        AlternateText="Elija una fecha del calendario" 
                                                        ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; width: 200px; height: 25px;" align="left" valign="middle">
                                                    <span>Tipo Usuario: </span>
                                                </td>
                                                <td valign="middle" align="left" style="width: 200px; height: 25px;">
                                                    <asp:DropDownList ID="ddlTipoUsuario" runat="server" Width="120px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>--%>
                                        
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="De:"></asp:Label>
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="tbFechaInicio" runat="server" CssClass="miTextBoxCalendar" />
                                                    <asp:ImageButton ID="Image1" runat="server" 
                                                        AlternateText="Elija una fecha del calendario" 
                                                        ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" />
                                                    <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="tbFechaInicio"
                                                        UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True">
                                                    </atk:MaskedEditExtender></td>
                                                <td>
                                                    <atk:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                        CssClass="MyCalendar" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image1" 
                                                        TargetControlID="tbFechaInicio" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblVerFechaInicio" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Hasta:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbFechaFin" runat="server" CssClass="miTextBoxCalendar" />
                                                    <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                        Mask="99/99/9999" MaskType="Date" PromptCharacter="-" 
                                                        TargetControlID="tbFechaFin" UserDateFormat="DayMonthYear">
                                                    </atk:MaskedEditExtender>
                                                    <asp:ImageButton ID="Image2" runat="server" 
                                                        AlternateText="Elija una fecha del calendario" 
                                                        ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" />
                                                </td>
                                                <td>
                                                    <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                        CssClass="MyCalendar" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image2" 
                                                        TargetControlID="tbFechaFin" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblVerFechaFin" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    </td>
                                                <td height= "6">
                                                </td>
                                                <td class="style2">
                                                    </td>
                                                <td class="style2">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Tipo Usuario:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoUsuario" runat="server" Width="120px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                        
                                        
                                        
                                    </asp:Panel>
                                </td>
                                <td style="border-style: solid; border-width: 1px; border-color: #a6a3a3; width: 150px;
                                    height: auto;" valign="top" align="center">
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 150px; height: 26px;">
                                        <tr>
                                            <td style="width: 150px; height: 26px;" valign="middle" align="center">
                                                <asp:ImageButton ID="btnReporteExportar" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                                    ToolTip="Exportar" OnClick="btnReporteExportar_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">

        $(document).ready(function() {

            $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
            $("#menu").hide('fast');
            $("#menu").width(0);
            $("#contenido").width(893);

        });
    
    </script>
</asp:Content>

