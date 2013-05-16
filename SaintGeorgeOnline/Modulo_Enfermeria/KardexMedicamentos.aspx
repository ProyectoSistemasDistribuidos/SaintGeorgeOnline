<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="KardexMedicamentos.aspx.vb" Inherits="Modulo_Enfermeria_KardexMedicamentos" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<%@ Register src="../Controles/ingresarMedicamento.ascx" tagname="ingresarMedicamento" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .FondoAplicacion
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .style1
        {
            height: 26px;
        }
        .style2
        {
            height: 26px;
            width: 118px;
        }
        .style4
        {
            height: 30px;
            width: 148px;
        }
        .style5
        {
            height: 30px;
            width: 118px;
        }
        .style6
        {
            width: 150px;
        }
    </style>

    <script type="text/javascript">

        function ShowMyModalPopup() {
            var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
            modal.show();
        }
      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="miPaginaMantenimiento">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="TabContainer1$miTab1$btnExportar" />
            </Triggers>
            <ContentTemplate>
                <div id="miContainerMantenimiento">
                    <atk:TabContainer ID="TabContainer1" runat="server" Width="670px" ActiveTabIndex="0"
                        AutoPostBack="false" ScrollBars="None">
                        <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
                            <HeaderTemplate>
                                <asp:Label ID="lbTab1" runat="server" Text="Busqueda de Stocks" />
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div style="border: solid 0px blue; width: 650px;">
                                    <div id="miBusquedaMant">
                                        <fieldset>
                                            <legend>Criterios de busqueda</legend>
                                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 610px;">
                                                <tr>
                                                    <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                                        Medicamento
                                                    </td>
                                                    <td style="width: 410px; height: 25px; padding-left: 10px" align="left" valign="bottom">
                                                        <asp:TextBox ID="tbBuscarMedicamentoDescripcion" runat="server" CssClass="miTextBox"
                                                            Width="400px" MaxLength="100" />
                                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hfTotalRegsHist" runat="server" Value="0" />
                                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                            TargetControlID="tbBuscarMedicamentoDescripcion" ValidChars="' ','á','é','í','ó','ú','(',')'"
                                                            Enabled="True">
                                                        </atk:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="width: 100px; padding-top: 6px" align="right" valign="top" rowspan="2">
                                                        <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                            onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                            OnClick="btnBuscar_Click" ToolTip="Buscar Registros" CausesValidation="False" />
                                                        <br />
                                                        <br />
                                                        <asp:ImageButton ID="btnLimpiar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                            onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                            OnClick="btnLimpiar_Click" ToolTip="Limpiar Filtros" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                                        Sede
                                                    </td>
                                                    <td style="min-width: 410px; height: 25px; padding-left: 10px;" align="left" valign="bottom">
                                                        <asp:DropDownList ID="ddlBuscarSede" runat="server" Height="18px" Width="235px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                    <div class="miEspacio">
                                    </div>
                                    <div id="misRegistrosEncontrados">
                                        <fieldset>
                                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 610px;">
                                                <tr>
                                                    <td style="width: 100px; height: 21px;" align="left" valign="middle">
                                                        <asp:ImageButton ID="btnExportar" runat="server" Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                                            onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                                            ToolTip="Exportar" OnClick="btnExportar_Click" CausesValidation="False" />
                                                    </td>
                                                    <td style="width: 410px; height: 21px;" align="left" valign="middle">
                                                        <asp:RadioButtonList ID="rbExportar" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0" Text="Word" />
                                                            <asp:ListItem Value="1" Text="Excel" Selected="True" />
                                                            <asp:ListItem Value="2" Text="Pdf" />
                                                            <asp:ListItem Value="3" Text="Html" />
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td style="width: 100px; height: 21px;" align="right" valign="middle">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                    <div class="miEspacio">
                                    </div>
                                    <div id="miGridviewMant">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="miGridviewBusqueda" GridLines="None"
                                            AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" EmptyDataText=" - No se encontraron resultados - "
                                            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                                            OnRowCommand="GriedView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnSorting="GridView1_Sorting">
                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Medicamento">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNombreMedicamento_grilla" runat="server" Text='<%# Bind("DescripcionNombre") %>' />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td style="width: 190px;" align="right" valign="middle">
                                                                    Medicamento&nbsp;
                                                                </td>
                                                                <td style="width: 193px;" align="left" valign="middle">
                                                                    <asp:ImageButton ID="btnSorting_DescripcionNombre" runat="server" ToolTip="Descendente"
                                                                        ImageUrl="~/App_Themes/Imagenes/DOWN_A.png" CommandName="Sort" CommandArgument="DescripcionNombre" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unidad M./Presentación">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUniMedPre_grilla" runat="server" Text='<%# Bind("DescripcionRelacion") %>' />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td style="width: 100px;" align="right" valign="middle">
                                                                    Unidad M./Presentación
                                                                </td>
                                                                <td style="width: 55px;" align="left" valign="middle">
                                                                    <asp:ImageButton ID="btnSorting_DescripcionRelacion" runat="server" ToolTip="Descendente"
                                                                        ImageUrl="~/App_Themes/Imagenes/DOWN.png" CommandName="Sort" CommandArgument="DescripcionRelacion" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCantidadActual_grilla" runat="server" Text='<%# Bind("CantidadActual") %>' />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td style="width: 100px;" align="right" valign="middle">
                                                                    Stock
                                                                </td>
                                                                <td style="width: 55px;" align="left" valign="middle">
                                                                    <asp:ImageButton ID="btnSorting_CantidadActual" runat="server" ToolTip="Descendente"
                                                                        ImageUrl="~/App_Themes/Imagenes/DOWN.png" CommandName="Sort" CommandArgument="CantidadActual" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock Mínimo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStockminimo_grilla" runat="server" Text='<%# Bind("StockMinimo") %>' />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td style="width: 100px;" align="right" valign="middle">
                                                                    Stock Mínimo
                                                                </td>
                                                                <td style="width: 55px;" align="left" valign="middle">
                                                                    <asp:ImageButton ID="btnSorting_StockMinimo" runat="server" ToolTip="Descendente"
                                                                        ImageUrl="~/App_Themes/Imagenes/DOWN.png" CommandName="Sort" CommandArgument="StockMinimo" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sede">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSede_grilla" runat="server" Text='<%# Bind("Sede") %>' />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td style="width: 100px;" align="right" valign="middle">
                                                                    Sede
                                                                </td>
                                                                <td style="width: 55px;" align="left" valign="middle">
                                                                    <asp:ImageButton ID="btnSorting_Sede" runat="server" ToolTip="Descendente" ImageUrl="~/App_Themes/Imagenes/DOWN.png"
                                                                        CommandName="Sort" CommandArgument="Sede" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="70px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="F. Actualización">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFActualizacion_grilla" runat="server" Text='<%# Bind("FechaActualizacionStock") %>' />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td style="width: 100px;" align="right" valign="middle">
                                                                    F. Actualización
                                                                </td>
                                                                <td style="width: 55px;" align="left" valign="middle">
                                                                    <asp:ImageButton ID="btnSorting_FechaActualizacionStockReal" runat="server" ToolTip="Descendente"
                                                                        ImageUrl="~/App_Themes/Imagenes/DOWN.png" CommandName="Sort" CommandArgument="FechaActualizacionStockReal" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="70px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaReal_grilla" runat="server" Text='<%# Bind("FechaActualizacionStockReal") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoSede_grilla" runat="server" Text='<%# Bind("CodigoSede") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoMedicamento_grilla" runat="server" Text='<%# Bind("CodigoMedicamento") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnVerHistorial" runat="server" ImageUrl="~/App_Themes/Imagenes/icolorfolder_icono.png"
                                                            Visible="true" CommandName="VerHistorial" CommandArgument='<%# Bind("CodigoMedicamento") %>'
                                                            ToolTip="Ver Historial de Ingresos y Salidas" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White"
                                                HorizontalAlign="Center" />
                                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                                            <PagerTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 628px;">
                                                    <tr>
                                                        <td style="height: 20px; width: 200px;" align="left" valign="middle">
                                                            <span class="miFooterMantLabelLeft">Ir a página </span>
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" CssClass="letranormal" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            &nbsp; de
                                                            <asp:Label ID="lblNumPaginas" runat="server" />
                                                        </td>
                                                        <td style="height: 20px; width: 228px;" align="center" valign="middle">
                                                            <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Prim. Pag" CommandArgument="First"
                                                                CssClass="pagfirst" />
                                                            <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                                                CommandArgument="Prev" CssClass="pagprev" />
                                                            <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Sig. página"
                                                                CommandArgument="Next" CssClass="pagnext" />
                                                            <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                                                CssClass="paglast" />
                                                        </td>
                                                        <td style="height: 20px; width: 200px;" align="right" valign="middle">
                                                            <asp:Label ID="lblRegistrosActuales" runat="server" CssClass="miFooterMantLabelRight" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </div>
                                    <div class="miEspacio">
                                    </div>
                                    <asp:Panel ID="pnl_Historico" BackColor="White" BorderColor="Black" runat="server"
                                        Width="650px">
                                        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 650px;">
                                            <tr id="HistorialHeader" style="cursor: pointer;">
                                                <td align="center" class="miGVBusquedaFicha_Header">
                                                    <span id="Span1" style="font-weight: bold; font-size: 11px; font-family: Arial; width: 650px;
                                                        padding-left: 5px;">Movimiento de E/S Histórico de Medicamentos </span>
                                                </td>
                                                <td style="text-align: right; padding-right: 5px;" class="miGVBusquedaFicha_Header">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Del:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbBuscarFechaHistInicial" runat="server"></asp:TextBox>
                                                                <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="tbBuscarFechaHistInicial"
                                                                    UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </atk:MaskedEditExtender>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton runat="server" ID="imageBF1" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                    AlternateText="Elija una fecha del calendario" />
                                                                <atk:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbBuscarFechaHistInicial"
                                                                    PopupButtonID="imageBF1" Format="dd/MM/yyyy" CssClass="MyCalendar" Enabled="True" />
                                                            </td>
                                                            <td>
                                                                Al:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbBuscarFechaHistFinal" runat="server"></asp:TextBox>
                                                                <atk:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="tbBuscarFechaHistFinal"
                                                                    UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </atk:MaskedEditExtender>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton runat="server" ID="imageBF2" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                    AlternateText="Elija una fecha del calendario" />
                                                                <atk:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbBuscarFechaHistFinal"
                                                                    PopupButtonID="imageBF2" Format="dd/MM/yyyy" CssClass="MyCalendar" Enabled="True" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnBuscarHistorico" runat="server" Width="74px" Height="19px"
                                                                    ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'"
                                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'" OnClick="btnBuscarHistorial_Click"
                                                                    ToolTip="Buscar Registro Historico de movimientos" CausesValidation="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_Hist_CodigoMedicamento" runat="server" Text="0" Visible="False"></asp:Label>
                                                    <asp:Label ID="lbl_Hist_CodigoSede" runat="server" Text="0" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Medicamento:
                                                    <asp:Label ID="lbl_MedicamentoHistorial" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Sede:
                                                    <asp:Label ID="lbl_SedeHistorial" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:GridView ID="gv_Historico" CssClass="miGVBusquedaFicha" GridLines="None" AutoGenerateColumns="False"
                                                        AllowPaging="True" OnPageIndexChanging="gv_Historico_PageIndexChanging" OnRowDataBound="gv_Historico_RowDataBound"
                                                        OnRowCreated="gv_Historico_RowCreated" Width="630px" EmptyDataText=" - No se encontraron resultados - "
                                                        runat="server">
                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Fecha">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaHist_grilla" runat="server" Text='<%# Bind("FechaRegistro") %>' />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td style="width: 120px;" align="left" valign="middle">
                                                                                Fecha&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo de Movimiento">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipoMovimientoHist_grilla" runat="server" Text='<%# Bind("TipoMovimiento") %>' />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td style="width: 190px;" align="left" valign="middle">
                                                                                Tipo de Movimiento&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cantidad">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCantidadHist_grilla" runat="server" Text='<%# Bind("Cantidad") %>' />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td style="width: 190px;" align="left" valign="middle">
                                                                                Cantidad&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MotivoSalida">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMotivoSalidaHist_grilla" runat="server" Text='<%# Bind("MotivoSalida") %>' />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td style="width: 190px;" align="left" valign="middle">
                                                                                Motivo Salida&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Observacion">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblObservacionHist_grilla" runat="server" Text='<%# Bind("Observacion") %>' />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td style="width: 190px;" align="left" valign="middle">
                                                                                Observación&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Observacion">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblObservacionHist_grilla" runat="server" Text='<%# Bind("Observacion") %>' />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td style="width: 190px;" align="left" valign="middle">
                                                                                Observación&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Usuario">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUsuarioHist_grilla" runat="server" Text='<%# Bind("TrabajadorRegistro") %>' />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td style="width: 190px;" align="left" valign="middle">
                                                                                Usuario&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White"
                                                            HorizontalAlign="Center" />
                                                        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                                                        <PagerTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 628px;">
                                                                <tr>
                                                                    <td style="height: 20px; width: 200px;" align="left" valign="middle">
                                                                        <span class="miFooterMantLabelLeft">Ir a página </span>
                                                                        <asp:DropDownList ID="ddlHistPageSelector" runat="server" CssClass="letranormal"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlHistPageSelector_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        &nbsp; de
                                                                        <asp:Label ID="lblHistNumPaginas" runat="server" />
                                                                    </td>
                                                                    <td style="height: 20px; width: 228px;" align="center" valign="middle">
                                                                        <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Prim. Pag" CommandArgument="First"
                                                                            CssClass="pagfirst" />
                                                                        <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                                                            CommandArgument="Prev" CssClass="pagprev" />
                                                                        <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Sig. página"
                                                                            CommandArgument="Next" CssClass="pagnext" />
                                                                        <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                                                            CssClass="paglast" />
                                                                    </td>
                                                                    <td style="height: 20px; width: 200px;" align="right" valign="middle">
                                                                        <asp:Label ID="lblHistRegistrosActuales" runat="server" CssClass="miFooterMantLabelRight" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </PagerTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Label ID="lbl_Historico" runat="server"></asp:Label>
                                    <atk:ModalPopupExtender ID="mpe_Historico" PopupControlID="pnl_Historico" TargetControlID="lbl_Historico"
                                        DynamicServicePath="" Enabled="True" BackgroundCssClass="FondoAplicacion" DropShadow="True"
                                        PopupDragHandleControlID="HistorialHeader" runat="server">
                                    </atk:ModalPopupExtender>
                                </div>
                            </ContentTemplate>
                        </atk:TabPanel>
                        <atk:TabPanel ID="miTab3" runat="server" HeaderText="Tab3" Enabled="true">
                            <HeaderTemplate>
                                <asp:Label ID="Label2" runat="server" Text="Ingresos / Salidas de Medicamentos" />
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div style="border: solid 0px blue; width: 450px;">
                                    <div class="miEspacio">
                                    </div>
                                    <div id="miDetalleMant">
                                        <asp:UpdatePanel ID="upPanelMedicamento" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnl_Mant_Registro_Kardex_IS" runat="server">
                                                    <fieldset>
                                                        <legend>Datos del Registro</legend>
                                                        <asp:Panel ID="pnlIngresoSalida" BackColor="White" BorderColor="Black" runat="server"
                                                            Width="450px">
                                                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 450px;
                                                                padding-right: 20px">
                                                                <tr>
                                                                    <td colspan="2" align="center" class="miGVBusquedaFicha_Header" style="width: 450px;
                                                                        height: 26px; cursor: pointer;">
                                                                        <span id="AlergiaHeader" style="padding-left: 20px; font-weight: bold; font-size: 11px;
                                                                            font-family: Arial;" width="450px"">Agregar Entrada / Salida de Medicame </span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="height: 15px;" align="right">
                                                                        <em>Campos Obligatorios (*)</em>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" style="padding-left: 10px; width: 100px">
                                                                        Tipo de Acción:
                                                                    </td>
                                                                    <td align="left" valign="bottom">
                                                                        <asp:RadioButtonList ID="rblTipoAccion_IS" runat="server" OnSelectedIndexChanged="rblTipoAccion_SelectedIndexChanged"
                                                                            RepeatDirection="Horizontal" AutoPostBack="True">
                                                                            <asp:ListItem Selected="True" Value="1">Ingresos</asp:ListItem>
                                                                            <asp:ListItem Value="2">Salidas</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" style="padding-left: 10px">
                                                                        Sede:
                                                                    </td>
                                                                    <td align="left" style="padding-left: 10px; height: 30px" valign="bottom">
                                                                        <asp:DropDownList ID="ddlSede_IS" runat="server" Height="20px" Width="110px" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlSede_IS_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" style="padding-left: 10px">
                                                                        Medicamento:
                                                                    </td>
                                                                    <td style="padding-left: 10px; height: 30px" align="left" valign="bottom">
                                                                        <asp:DropDownList ID="ddlMedicamento_IS" runat="server" Height="20px" Width="200px"
                                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlMedicamento_IS_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <b>
                                                                            <asp:ImageButton ID="btnAgregarRegistroMedicamento" runat="server" Height="19px"
                                                                                ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" OnClick="btnAgregarRegistroMedicamento_Click"
                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                                                                                ToolTip="Agregar Nuevo Medicamento" Width="84px" />
                                                                            <br />
                                                                            Stock Actual:
                                                                            <asp:Label ID="lbl_StockActual_IS" runat="server" Text="0"></asp:Label>
                                                                        </b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" colspan="2" style="height: 0px">
                                                                        <asp:Panel ID="pnl_MotivoSalida" runat="server">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td style="width: 100px; padding-left: 10px; height: 25px">
                                                                                        Motivo de Salida:
                                                                                    </td>
                                                                                    <td style="padding-left: 10px">
                                                                                        <asp:DropDownList ID="ddlMotivoSalidaMedicamento_IS" runat="server" Height="20px"
                                                                                            Width="200px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" style="padding-left: 10px; height: 30px">
                                                                        Cantidad
                                                                    </td>
                                                                    <td align="left" style="padding-left: 10px;" valign="bottom">
                                                                        <asp:TextBox ID="tb_Cantidad_IS" runat="server" Height="18px" Width="60px"></asp:TextBox>
                                                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers"
                                                                            TargetControlID="tb_Cantidad_IS" Enabled="True">
                                                                        </atk:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 100px; height: 50px; padding-left: 10px;" valign="middle">
                                                                        Observación<span>&nbsp;</span><span class="camposObligatorios" style="padding-left: 10px">(*)</span>
                                                                    </td>
                                                                    <td align="left" style="height: 50px; padding-left: 10px;" valign="bottom">
                                                                        <asp:TextBox ID="tb_Observacion_IS" runat="server" CssClass="miTextBox" Height="40px"
                                                                            Rows="2" TextMode="MultiLine" Width="270px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="center" style="height: 25px" valign="middle">
                                                                        &nbsp;<asp:ImageButton ID="popup_btnAgregar_Medicamento" runat="server" Height="19px"
                                                                            ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" OnClick="popup_btnAgregar_Medicamento_Click"
                                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                            ToolTip="Aceptar" Width="84px" />
                                                                        &nbsp;&nbsp;
                                                                        <asp:ImageButton ID="popup_btnCancelar_Medicamento" runat="server" Height="19px"
                                                                            ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" OnClick="popup_btnCancelar_Medicamento_Click"
                                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                            ToolTip="Cancelar" Width="84px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" height="10px">
                                                                        <asp:HiddenField ID="hidencodigoIS_PopUp" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div id="controlProcedimiento" style="display: none">
                                                                <input type="button" id="OKProcedimiento" />
                                                                <input type="button" id="CancelProcedimiento" />
                                                            </div>
                                                        </asp:Panel>
                                                        <atk:ModalPopupExtender ID="mpe_ModalIngresoSalida" runat="server" DynamicServicePath=""
                                                            Enabled="True" BackgroundCssClass="FondoAplicacion" DropShadow="True" PopupControlID="pnlIngresoSalida"
                                                            TargetControlID="btnAgregarDetalleIngresoSalida" Drag="true">
                                                        </atk:ModalPopupExtender>
                                                        <div style="font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #800000;
                                                            padding-left: 10px; padding-right: 10px; padding-top: 5px; height: 35px;">
                                                            <b>Importante:</b> Ingresar la informacion de c/u de los medicamentos a ingresar
                                                            o sacar del stock actual, despues de ello, hacer clic en el boton "Agregar" por
                                                            c/u de ellos. Finalmente haga clic en el boton "Grabar"
                                                        </div>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="590px">
                                                            <tr>
                                                                <td style="width: 150px; text-align: center; color: White; font-size: 10px;" align="center"
                                                                    class="miGVBusquedaFicha_Header">
                                                                    Medicamento
                                                                </td>
                                                                <td style="width: 50px; text-align: center; color: White; font-size: 10px;" align="center"
                                                                    class="miGVBusquedaFicha_Header">
                                                                    Cantidad
                                                                </td>
                                                                <td style="width: 70px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                    align="center" class="miGVBusquedaFicha_Header">
                                                                    Acción
                                                                </td>
                                                                <td style="width: 100px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                    align="center" class="miGVBusquedaFicha_Header">
                                                                    Sede
                                                                </td>
                                                                <td style="width: 100px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                    align="center" class="miGVBusquedaFicha_Header">
                                                                    Motivo Salida
                                                                </td>
                                                                <td style="width: 30px; height: 26px;" align="right" class="miGVBusquedaFicha_Header">
                                                                    <asp:ImageButton ID="btnAgregarDetalleIngresoSalida" runat="server" Width="24px"
                                                                        Height="24px" ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                                        OnClick="btnAgregarDetalleIngresoSalida_Click" ToolTip="Agregar" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 25px" align="center" valign="top" colspan="5">
                                                                    <asp:UpdatePanel ID="upKardex_IS" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div id="Div2">
                                                                                <asp:GridView ID="gvDetalleKardex_IS" runat="server" CssClass="miGVBusquedaFicha"
                                                                                    GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                    EmptyDataText=" - No se encontraron resultados - " OnRowCommand="gvDetalleKardex_ES_RowCommand"
                                                                                    OnRowDataBound="gvDetalleKardex_ES_RowDataBound" ShowHeader="False" ShowFooter="False"
                                                                                    Width="573px">
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                    Visible="true" CommandName="Actualizar" CommandArgument='<%# Bind("Correlativo") %>'
                                                                                                    ToolTip="Actualizar Registro" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                    Visible="true" CommandName="Eliminar" CommandArgument='<%# Bind("Correlativo") %>'
                                                                                                    ToolTip="Eliminar Registro" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Correlativo">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCorrelativo_grilla" runat="server" Text='<%# Bind("Correlativo") %>' />
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCodigoSede_grilla" runat="server" Text='<%# Bind("CodigoSede") %>' />
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCodigoMedicamento_grilla" runat="server" Text='<%# Bind("CodigoMedicamento") %>' />
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCodigoTipoAccion_grilla" runat="server" Text='<%# Bind("CodigoTipoAccion") %>' />
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCodigoMotivoSalida_grilla" runat="server" Text='<%# Bind("CodigoMotivoSalida") %>' />
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblMedicamento_grilla" runat="server" Text='<%# Bind("Medicamento") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCantidad_grilla" runat="server" Text='<%# Bind("Cantidad") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="50px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblTipoAccion_grilla" runat="server" Text='<%# Bind("TipoAccion") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblSede_grilla" runat="server" Text='<%# Bind("Sede") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblMotivoSalida_grilla" runat="server" Text='<%# Bind("MotivoSalida") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblObservaciones_grilla" runat="server" Text='<%# Bind("Observaciones") %>' />
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="miEspacio">
                                    </div>
                                    <div id="Div3">
                                        <asp:ImageButton ID="btnNuevo_Kardex_IS" runat="server" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnNuevo_1.png"
                                            OnClick="btnNuevo_Kardex_IS_Click" onmouseout="this.src = '../App_Themes/Imagenes/btnNuevo_1.png'"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnNuevo_2.png'" ToolTip="Nuevo Registro"
                                            Width="74px" />
                                        &nbsp;&nbsp;<asp:ImageButton ID="btn_Grabar_Kardex_IS" runat="server" Width="74px"
                                            Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'"
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                            OnClick="btn_Grabar_Kardex_IS_Click" />&nbsp;&nbsp;<asp:ImageButton ID="btn_Cancelar_Kardex_IS"
                                                runat="server" Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                                onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'"
                                                ToolTip="Cancelar" OnClick="btnCancelar_Kardex_IS_Click" CausesValidation="False" />
                                        &nbsp;&nbsp;</div>
                                </div>
                            </ContentTemplate>
                        </atk:TabPanel>
                    </atk:TabContainer>
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
                    <atk:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                        Enabled="True" BackgroundCssClass="FondoAplicacion" DropShadow="True" PopupControlID="pnlImpresion"
                        TargetControlID="lblAccionExportar">
                    </atk:ModalPopupExtender>
                    <asp:Label ID="lblAccionExportar" runat="server" ForeColor="White" Text="..."></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <script type="text/javascript">

            $(document).ready(function() {

                $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
                $("#menu").hide('fast');
                $("#menu").width(0);
                $("#contenido").width(893);

            });

        </script>

        <uc1:ingresarMedicamento ID="ucIngresarMedicamento" runat="server" />
    </div>
</asp:Content>

