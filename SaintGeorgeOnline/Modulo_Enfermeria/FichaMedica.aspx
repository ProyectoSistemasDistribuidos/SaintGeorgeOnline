<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false"
    CodeFile="FichaMedica.aspx.vb" Inherits="Modulo_Enfermeria_FichaMedica" Title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>
<%@ Register src="../Controles/ingresarEnfermedad.ascx" tagname="ingresarEnfermedad" tagprefix="uc1" %>
<%@ Register src="../Controles/IngresarVacuna.ascx" tagname="ingresarVacuna" tagprefix="uc2" %>
<%@ Register src="../Controles/IngresarDosis.ascx" tagname="IngresarDosis" tagprefix="uc3" %>
<%@ Register src="../Controles/IngresarAlergia.ascx" tagname="IngresarAlergia" tagprefix="uc4" %>
<%@ Register src="../Controles/IngresarTiposCaracteristicasPiel.ascx" tagname="ingresarTiposCaracteristicasPiel" tagprefix="uc5" %>
<%@ Register src="../Controles/IngresarTipoAlergia.ascx" tagname="ingresarTipoAlergia" tagprefix="uc6" %>
<%@ Register src="../Controles/IngresarHospitalizacion.ascx" tagname="ingresarHospitalizacion" tagprefix="uc7" %>
<%@ Register src="../Controles/IngresarOperaciones.ascx" tagname="ingresarOperaciones" tagprefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style3
        {
            width: 54px;
        }
        .style4
        {
            width: 100px;
        }
    </style>

    <script type="text/javascript">

        //        <%--function abrirPopup(url) {

        //            window.showModalDialog(url, "#1", "dialogHeight: 155px ; dialogWidth: 500px; edge: Raised; center: Yes; help: No; resizable: No; status: No;");

        //        }
        //        OnClientClick="abrirPopup('../Popups/AgregarTipoSangre.aspx');" /> 
        //--%>
        function MostrarImpresionFichaMedica_html() {

            window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FichaMedica_html.aspx', '_blank', '');
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="miPaginaMantenimiento">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="miContainerMantenimiento">
                    <atk:TabContainer ID="TabContainer2" runat="server" Width="881px" ActiveTabIndex="1"
                        AutoPostBack="false" ScrollBars="None">
                        <atk:TabPanel ID="miTab1_1" runat="server" HeaderText="Tab1" Enabled="true">
                            <HeaderTemplate>
                                <asp:Label ID="lbTab1_1" runat="server" Text="Busqueda" />
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div style="border: solid 0px blue; width: 650px;">
                                    <div id="miBusquedaActualizacion_Ficha">
                                        <!-- 650px -->
                                        <fieldset>
                                            <legend>Criterios de busqueda</legend>
                                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 800px;">
                                                <tr>
                                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                                        <span>Apellido Paterno</span>
                                                    </td>
                                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                                        <asp:TextBox ID="tbBuscarApellidoPaterno" runat="server" CssClass="miTextBox" Width="320px"
                                                            MaxLength="100" Height="18px" />
                                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                                       
                                                    </td>
                                                    <td style="width: 100px; padding-top: 6px" align="right" valign="top" rowspan="2">
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="btnBuscar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                                    OnClick="btnBuscar_Click" ToolTip="Buscar Registros" /><br />
                                                                <br />
                                                                <asp:ImageButton ID="btnLimpiar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                                    OnClick="btnLimpiar_Click" ToolTip="Limpiar Filtros" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                                        <span>Apellido Materno</span>
                                                    </td>
                                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                                        <asp:TextBox ID="tbBuscarApellidoMaterno" runat="server" CssClass="miTextBox" Width="320px"
                                                            MaxLength="100" Height="18px" />
                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                                        <span>Nombre</span>
                                                    </td>
                                                    <td style="min-width: 360px; height: 25px;" align="left" valign="middle">
                                                        <asp:TextBox ID="tbBuscarNombre" runat="server" CssClass="miTextBox" Width="320px"
                                                            MaxLength="100" Height="18px" />
                                                      
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                                        <span>Año Academico</span>
                                                    </td>
                                                    <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                                        <asp:DropDownList ID="ddlAnioAcademico1" runat="server" Width="100px">
                                                            <asp:ListItem Value="0"> 2010 </asp:ListItem>
                                                            <asp:ListItem Value="1"> 2009 </asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;<span>Hasta</span> &nbsp;
                                                        <asp:DropDownList ID="ddlAnioAcademico2" runat="server" Width="100px">
                                                            <asp:ListItem Value="0"> 2010 </asp:ListItem>
                                                            <asp:ListItem Value="1"> 2009 </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                                        <span>Situación de alumno</span>
                                                    </td>
                                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                                        <asp:DropDownList ID="ddlEstadoAlumno" runat="server" Width="100px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                                        <span>Sede</span>
                                                    </td>
                                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                                        <asp:DropDownList ID="ddlSede" runat="server" Width="100px" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                                        <span>Nivel</span>
                                                    </td>
                                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                                        <asp:DropDownList ID="ddlNiveles" runat="server" Width="100px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlNiveles_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 150px; height: 25px;" valign="middle">
                                                        <span>SubNivel</span>
                                                    </td>
                                                    <td align="left" style="width: 360px; height: 25px;" valign="middle">
                                                        <asp:DropDownList ID="ddlSubniveles" runat="server" Width="100px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlSubniveles_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <tr>
                                                        <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                                            <span>Grado</span>
                                                        </td>
                                                        <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlGrados" runat="server" Width="100px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlGrados_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                                            <span>Aula</span>
                                                        </td>
                                                        <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlAulas" runat="server" Width="100px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                    <div class="miEspacio">
                                    </div>
                                    <div id="miGridviewMantActualizacion_Ficha">
                                        <asp:GridView ID="GridView1" runat="server" Width="840" CssClass="miGridviewBusqueda"
                                            GridLines="None" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
                                            EmptyDataText=" - No se encontraron resultados - " OnPageIndexChanging="GridView1_PageIndexChanging"
                                            OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated"
                                            OnSorting="GridView1_Sorting">
                                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White"
                                                HorizontalAlign="Center" />
                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                            CommandName="Actualizar" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Actualizar Registro" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnVisualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_ver.png"
                                                            CommandName="Visualizar" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Ver Registro" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_printer.png"
                                                            CommandName="Imprimir" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Imprimir Registro" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NombreCompleto">
                                                    <HeaderTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td style="width: 200px;" align="right" valign="middle">
                                                                    NombreCompleto&nbsp;
                                                                </td>
                                                                <td style="width: 265px;" align="left" valign="middle">
                                                                    <asp:ImageButton ID="btnSorting_NombreCompleto" runat="server" ToolTip="Descendente"
                                                                        ImageUrl="~/App_Themes/Imagenes/DOWN_A.png" CommandName="Sort" CommandArgument="NombreCompleto" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:TemplateField>
                                                            <itemtemplate>
                                            <asp:Label ID="lblCodigoEstadoAlumno" runat="server" Text='<%# Bind("EstadoAlumno") %>' />
                                           
                                                        </itemtemplate>
                                                            <headerstyle horizontalalign="Center" width="0" />
                                                            <itemstyle cssclass="miGridviewBusqueda_Rows" horizontalalign="Center" width="0" />
                                                        </asp:TemplateField>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label" runat="server" Text='<%# Bind("NombreCompleto") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="540px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="540px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Estado/Nivel/SubNivel/Grado/Aula">
                                                    <HeaderTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td style="width: 350px;" align="center" valign="middle">
                                                                    Estado/Nivel/SubNivel/Grado/Aula&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENSnGS" runat="server" Text='<%# Bind("ENSnGS") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="540px" />
                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="540px" />
                                                </asp:TemplateField>
                                            </Columns>
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
                                                            <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Primera Pagina"
                                                                CommandArgument="First" CssClass="pagfirst" />
                                                            <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Página anterior"
                                                                CommandArgument="Prev" CssClass="pagprev" />
                                                            <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Página siguiente"
                                                                CommandArgument="Next" CssClass="pagnext" />
                                                            <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Última Pagina"
                                                                CommandArgument="Last" CssClass="paglast" />
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
                                    <div id="GVLegenda">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 840px;">
                                            <tr>
                                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                    <img alt="Actualizar Registro" src="../App_Themes/Imagenes/opc_actualizar.png" />
                                                </td>
                                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                    <span>Actualizar Registro</span>
                                                </td>
                                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span>|</span>
                                                </td>
                                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                    <img alt="Eliminar Registro" src="../App_Themes/Imagenes/opc_ver.png" />
                                                </td>
                                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                    <span>Ver Registro</span>
                                                </td>
                                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span>|</span>
                                                </td>
                                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                    <img alt="Activar Registro" src="../App_Themes/Imagenes/opc_printer.png" />
                                                </td>
                                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                    <span>Imprimir Registro</span>
                                                </td>
                                                <td style="width: 200px">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </atk:TabPanel>
                        <atk:TabPanel ID="miTab2_2" runat="server" HeaderText="Tab2_2" Enabled="false">
                            <HeaderTemplate>
                                <asp:Label ID="lbTab2_2" runat="server" Text="Actualización" />
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="miPagina">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div id="miContenidoFichaMedica">
                                                <div id="miCabeceraFichaMedica">
                                                    <table width="840" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td style="width: 690px;" rowspan="5">
                                                                <asp:UpdatePanel ID="updPanel_DatosGenerales" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <fieldset id="Bloque_DatosAlumno" runat="server" style="width: 670px;">
                                                                            <legend>Datos del Alumno</legend>
                                                                            <table cellpadding="0" cellspacing="0" border="0" width="650px">
                                                                                <tr>
                                                                                    <td style="width: 74px; height: 100px; background: #FFFFFF url(../App_Themes/Imagenes/img_bg.gif) no-repeat;
                                                                                        background-position: center center;" align="center" valign="middle" rowspan="4">
                                                                                        <asp:Image ID="img_FotoUsuario" runat="server" Width="54" Height="64" Style="border: #7f9db9 1px solid"
                                                                                            ImageUrl="~/Fotos/noPhoto.gif" />
                                                                                    </td>
                                                                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="4">
                                                                                    </td>
                                                                                    <td style="width: 110px; height: 25px; font-weight: bold; font-family: Arial;" align="left"
                                                                                        valign="middle">
                                                                                        <span>Nombre Completo :&nbsp;</span>
                                                                                    </td>
                                                                                    <td style="width: 456px; height: 25px; font-family: Arial;" align="left" valign="middle">
                                                                                        <asp:Label ID="lblNombreAlumno" runat="server"></asp:Label>
                                                                                        <asp:HiddenField ID="hd_Codigo" runat="server" />
                                                                                        <asp:HiddenField ID="hd_Grado" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 110px; height: 25px; font-weight: bold;" align="left" valign="middle">
                                                                                        <span>Sede:&nbsp;</span>
                                                                                    </td>
                                                                                    <td style="width: 456px; height: 25px;" align="left">
                                                                                        <asp:Label ID="lblSede" Text="Miraflores" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 110px; height: 25px; font-weight: bold; font-family: Arial;" align="left"
                                                                                        valign="middle">
                                                                                        <span>Estado/Año Académico :&nbsp;</span>
                                                                                    </td>
                                                                                    <td style="width: 446px; height: 25px; font-family: Arial;" align="left">
                                                                                        <asp:Label ID="lblSituacionAnio" Text="Activo/2010" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 110px; height: 25px; font-weight: bold; font-family: Arial;" align="left"
                                                                                        valign="middle">
                                                                                        <span>Nivel/SubNivel/Grado/Aula :&nbsp;</span>
                                                                                    </td>
                                                                                    <td style="width: 446px; height: 25px; font-family: Arial;" align="left">
                                                                                        <asp:Label ID="lblENSnGS" Text="" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </fieldset>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 150px; height: 25px;" align="right">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="width: 150px; height: 20px;">
                                                                <asp:ImageButton ID="btnGrabar" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png"
                                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabarV2_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnGrabarV2_1.png'"
                                                                    ToolTip="Grabar" OnClick="btnGrabar_click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="width: 150px; height: 20px;">
                                                                <asp:ImageButton ID="btnFichaCancelar" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'"
                                                                    ToolTip="Cancelar" OnClick="btnFichaCancelar_Click" CausesValidation="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 150px; height: 20px;" align="right">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="miEspacio">
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div id="miContainerFicha">
                                                            <atk:TabContainer ID="TabContainer1" runat="server" Width="850px" ActiveTabIndex="1"
                                                                AutoPostBack="false" ScrollBars="Vertical">
                                                                <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1">
                                                                    <HeaderTemplate>
                                                                        Desarrollo Infantil
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <div id="Bloque_DesarrolloInfantil" runat="server" style="border: 0; margin: 0;">
                                                                            <fieldset id="FM_DI_Nacimiento" runat="server">
                                                                                <legend>Nacimiento</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                    <tr>
                                                                                        <td colspan="2" style="height: 15px;" align="right">
                                                                                            <em>Campos Obligatorios (*)</em>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                            <span>Tipo de Nacimiento :</span> <span class="camposObligatorios">(*)</span>
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 25px" align="left">
                                                                                            <asp:Label ID="lblTipoNacimiento" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:DropDownList ID="ddlTipoNacimiento" runat="server" Width="150px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                            <span>Observaciones :</span>
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 25px" align="left" valign="middle">
                                                                                            <asp:TextBox ID="tbObservaciones" runat="server" CssClass="miTextBoxMultiLine" Width="360px"
                                                                                                Height="35px" Rows="3" TextMode="MultiLine" />
                                                                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                                                                TargetControlID="tbObservaciones" ValidChars="' ','.','á','é','í','ó','ú','(',')','Á','É','Í','Ó','Ú'"
                                                                                                Enabled="True">
                                                                                            </atk:FilteredTextBoxExtender>
                                                                                            <asp:Label ID="lblObservaciones" runat="server" Text="" Width="360px" Height="35px"
                                                                                                Rows="3" TextMode="MultiLine" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_DI_DesarrolloMotor" runat="server">
                                                                                <legend>Desarrollo Motor</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                            <span>¿A qué edad levantó la cabeza? :</span><span class="camposObligatorios">(*)</span>
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 25px" align="left">
                                                                                            <asp:DropDownList ID="ddlEdadLevCabeza" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblEdadLevCabeza" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:Label ID="lblAñoLevCabeza" Text=" Año(s) " runat="server" Width="40px" Visible="False"></asp:Label>
                                                                                            <asp:DropDownList ID="ddlMesesLevCabeza" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblMesesLevCabeza" Text="  Mes(es) " runat="server" Width="40px" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                            <span>¿A qué edad se sentó? :</span><span class="camposObligatorios">(*)</span>
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 25px" align="left">
                                                                                            <asp:DropDownList ID="ddlEdadSento" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblEdadSento" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:Label ID="lblAñoSento" Text=" Año(s) " runat="server" Width="40px" Visible="False"></asp:Label>
                                                                                            <asp:DropDownList ID="ddlMesesSento" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblMesesSento" Text="  Mes(es) " runat="server" Width="40px" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                            <span>¿A qué edad se paró? :</span><span class="camposObligatorios">(*)</span>
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 25px" align="left">
                                                                                            <asp:DropDownList ID="ddlEdadParo" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblEdadParo" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:Label ID="lblAñoParo" Text=" Año(s) " runat="server" Width="40px" Visible="False"></asp:Label>
                                                                                            <asp:DropDownList ID="ddlMesesParo" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblMesesParo" Text="  Mes(es) " runat="server" Width="40px" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                            <span>¿A qué edad caminó? :</span><span class="camposObligatorios">(*)</span>
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 25px" align="left">
                                                                                            <asp:DropDownList ID="ddlEdadCamino" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblEdadCamino" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:Label ID="lblAñoCamino" Text=" Año(s) " runat="server" Width="40px" Visible="False"></asp:Label>
                                                                                            <asp:DropDownList ID="ddlMesesCamino" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblMesesCamino" Text="  Mes(es) " runat="server" Width="40px" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_DI_ControlEsfinteres" runat="server">
                                                                                <legend>Control de Esfinteres</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                            <span>¿A qué edad controló sus esfínteres? :</span><span class="camposObligatorios">(*)</span>
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 25px" align="left">
                                                                                            <asp:DropDownList ID="ddlEdadControloEsfinteres" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblEdadControloEsfinteres" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:Label ID="lblAñoControloEsfinteres" Text=" Año(s) " runat="server" Width="40px"
                                                                                                Visible="False"></asp:Label>
                                                                                            <asp:DropDownList ID="ddlMesesControloEsfinteres" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblMesesControloEsfinteres" Text="  Mes(es) " runat="server" Width="40px"
                                                                                                Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_DI_Lenguaje" runat="server">
                                                                                <legend>Lenguaje</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                            <span>¿A que edad pronunció las primeras palabras? :</span><span class="camposObligatorios">(*)</span>
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 25px" align="left">
                                                                                            <asp:DropDownList ID="ddlEdadHabloPrimerasPalabras" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblEdadHabloPrimerasPalabras" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:Label ID="lblAñoHabloPrimerasPalabras" Text=" Año(s) " runat="server" Width="40px"
                                                                                                Visible="False"></asp:Label>
                                                                                            <asp:DropDownList ID="ddlMesesHabloPrimerasPalabras" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblMesesHabloPrimerasPalabras" Text="  Mes(es) " runat="server" Width="40px"
                                                                                                Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                            <span>¿A que edad se comunicó con fluídez? :</span><span class="camposObligatorios">(*)</span>
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 25px" align="left">
                                                                                            <asp:DropDownList ID="ddlEdadHabloFluidez" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblEdadHabloFluidez" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:Label ID="lblAñoHabloFluidez" Text=" Año(s) " runat="server" Width="40px" Visible="False"></asp:Label>
                                                                                            <asp:DropDownList ID="ddlMesesHabloFluidez" runat="server" Width="45px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblMesesHabloFluidez" Text="  Mes(es) " runat="server" Width="40px"
                                                                                                Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </atk:TabPanel>
                                                                <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2">
                                                                    <HeaderTemplate>
                                                                        Estado Salud
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <div id="Bloque_EstadoSalud" runat="server" style="border: 0; margin: 0;">
                                                                            <fieldset id="FM_ES_TipoSangre" runat="server">
                                                                                <legend>Tipo de Sangre</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                            <span>Tipo de Sangre :</span><span class="camposObligatorios">(*)</span>
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 25px" align="left">
                                                                                            <asp:Label ID="lblTipoSangre_ver" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:DropDownList ID="ddlTipoSangre" runat="server" Width="121px" Height="18px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_ES_Enfermedades" runat="server">
                                                                                <legend>Enfermedades</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                    <tr>
                                                                                        <td colspan="2" height="10px">
                                                                                            <atk:ModalPopupExtender ID="modal_xxx" runat="server" TargetControlID="btnMostrarEnfermedad"
                                                                                                PopupControlID="pnl_PopUp_Enfermedad" BackgroundCssClass="MiModalBackground"
                                                                                                  OkControlID="OKVacuna" CancelControlID="CancelVacuna" DynamicServicePath=""
                                                                                                Enabled="True" PopupDragHandleControlID="EnfermedadHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_Enfermedad" BackColor="White" BorderColor="Black" runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                                                                    <tr id="EnfermedadHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 460px; height: 26px" colspan="3" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Enfermedad</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr><td colspan="3" height="10px"></td></tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Enfermedad&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidencodigoEnfermedad" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 260px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlEnfermedad" runat="server" Width="250px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                          <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <asp:ImageButton ID="btnAgregarEnfermedad" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" OnClick="btnAgregarEnfermedad_Click"
                                                                                                                ToolTip="Agregar Enfermedad" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Edad&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 260px; height: 25px" align="left">
                                                                                                            <asp:TextBox ID="tbEdad" runat="server" CssClass="miTextBox" Text="0" Width="50px" />
                                                                                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers"
                                                                                                                TargetControlID="tbEdad" Enabled="True">
                                                                                                            </atk:FilteredTextBoxExtender>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 460px; height: 25px" align="center" valign="middle" colspan="3">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_Enfermedad" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_Enfermedad_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_Enfermedad" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_Enfermedad_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="controlEnfermedad" style="display: none">
                                                                                                    <input type="button" id="okEnfermedad" />
                                                                                                    <input type="button" id="CancelEnfermedad" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td colspan="2" style="width:427px; height: 26px; text-align: center; color: White;
                                                                                                        font-size: 10px;" align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Enfermedad
                                                                                                    </td>
                                                                                                    <td style="width: 333px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Edad
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_Enfermedad" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btn_Add_Enfermedad_Click"
                                                                                                            ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="4">
                                                                                                        <asp:UpdatePanel ID="upEnfermedad" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantFichaRegitros">
                                                                                                                    <asp:GridView ID="gvDetalleEnfermedad" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleEnfermedad_RowDataBound" OnRowCommand="gvDetalleEnfermedad_RowCommand"
                                                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        Visible="true" CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelFichaMedEnEnfermedades") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        Visible="true" CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelFichaMedEnEnfermedades") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoEnfermedad">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoEnfermedad" runat="server" Text='<%# Bind("CodigoEnfermedad") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblEnfermedad" runat="server" Text='<%# Bind("Enfermedad") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="380px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblEdadEnfermedad_grilla" runat="server" Text='<%# Bind("Edad") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="300px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%--<asp:Label  ID="lblEdadEnfermedad_grilla" runat="server" Text='<%# Bind("Edad") %>' />--%>
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="50px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnMostrarEnfermedad" runat="server" Text="Button" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_ES_Vacunas" runat="server">
                                                                                <legend>Vacunas</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                    <tr>
                                                                                        <td colspan="2" height="10px">
                                                                                            <atk:ModalPopupExtender ID="pnModalVacuna" runat="server" TargetControlID="btnMostrarVacuna"
                                                                                                PopupControlID="pnl_PopUp_Vacuna" BackgroundCssClass="MiModalBackground"  
                                                                                                Drag="True" OkControlID="OKVacuna" CancelControlID="CancelVacuna" DynamicServicePath=""
                                                                                                Enabled="True" PopupDragHandleControlID="VacunaHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_Vacuna" BackColor="White" BorderColor="Black" runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                                                                    <tr id="VacunaHeader" style="cursor: pointer;">                                                                                                     
                                                                                                        <td style="width: 460px; height: 26px" colspan="3" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Vacuna</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Fecha de Registro :&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidencodigoVacuna" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                                                                <tr>
                                                                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                                                                        <asp:TextBox ID="tbFechaVacunacion" runat="server" Enabled="False" 
                                                                                                                            CssClass="miTextBoxCalendar" />
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                                                                        <asp:ImageButton Enabled="False" runat="server" ID="imageBF" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                            AlternateText="Elija una fecha del calendario" />
                                                                                                                        <atk:CalendarExtender ID="calendario" runat="server" TargetControlID="tbFechaVacunacion"
                                                                                                                            PopupButtonID="imageBF" Format="dd/MM/yyyy" CssClass="MyCalendar" 
                                                                                                                            Enabled="True" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Vacuna : &nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlTipoVacuna" runat="server" Width="200px">
                                                                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                         <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <asp:ImageButton ID="btnAgregarVacuna" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" OnClick="btnAgregarVacuna_Click"
                                                                                                                ToolTip="Agregar Vacuna" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Dosis :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlDosis" runat="server" Width="200px">
                                                                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <asp:ImageButton ID="btnAgregarDosis" runat="server" Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png"
                                                                                                                onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'"
                                                                                                                OnClick="btnAgregarDosis_Click" ToolTip="Agregar Dosis" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Edad&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:TextBox ID="tbEdadVacuna" runat="server" CssClass="miTextBox" Text="0" Width="50px"
                                                                                                                MaxLength="2" />
                                                                                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                                                                                                TargetControlID="tbEdadVacuna" Enabled="True">
                                                                                                            </atk:FilteredTextBoxExtender>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="3">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_Vacuna" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_Vacuna_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_Vacuna" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_Vacuna_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="controlVacuna" style="display: none">
                                                                                                    <input type="button" id="OKVacuna" />
                                                                                                    <input type="button" id="CancelVacuna" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td style="width: 180px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        Fecha de Registro
                                                                                                    </td>
                                                                                                    <td style="width: 240px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        class="miGVBusquedaFicha_Header">
                                                                                                        Vacuna
                                                                                                    </td>
                                                                                                    <td style="width: 240px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        class="miGVBusquedaFicha_Header">
                                                                                                        Dosis
                                                                                                    </td>
                                                                                                    <td style="width: 100px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        class="miGVBusquedaFicha_Header">
                                                                                                        Edad
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_Vacuna" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                                                                            OnClick="btn_Add_Vacuna_Click" ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="5">
                                                                                                        <asp:UpdatePanel ID="upVacuna" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantFichaRegitros">
                                                                                                                    <asp:GridView ID="gvDetalleVacuna" runat="server" CssClass="miGVBusquedaFicha" GridLines="None"
                                                                                                                        AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False" OnRowDataBound="gvDetalleVacuna_RowDataBound"
                                                                                                                        OnRowCommand="gvDetalleVacuna_RowCommand" ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelVacunasFichaMed") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelVacunasFichaMed") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoVacuna">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoVacuna" runat="server" Text='<%# Bind("CodigoVacuna") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoDosis" runat="server" Text='<%# Bind("CodigoDosis") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaVacunacion" runat="server" Text='<%# Bind("FechaVacunacion") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="120px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblVacuna" runat="server" Text='<%# Bind("Vacuna") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="240px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblDosis" runat="server" Text='<%# Bind("Dosis") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="240px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblEdadVacuna" runat="server" Text='<%# Bind("Edad") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="100px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%--<asp:Label ID="lblEdadVacuna" runat="server" Text='<%# Bind("Edad") %>' />--%>
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnMostrarVacuna" runat="server" Text="Button" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_ES_Alergias" runat="server">
                                                                                <legend>Alergias</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td colspan="3" height="10px">
                                                                                            <atk:ModalPopupExtender ID="pnModalAlergia" runat="server" TargetControlID="btnMostrarAlergia"
                                                                                                PopupControlID="pnl_PopUp_Alergia" BackgroundCssClass="MiModalBackground"  
                                                                                                Drag="True" OkControlID="OKAlergia" CancelControlID="CancelAlergia" DynamicServicePath=""
                                                                                                Enabled="True" PopupDragHandleControlID="AlergiaHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_Alergia" BackColor="White" BorderColor="Black" runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                                                                    <tr id="AlergiaHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 360px; height: 26px" colspan="3" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Alergia</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Fecha de Registro&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidencodigoAlergia" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                                                                <tr>
                                                                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                                                                        <asp:TextBox ID="tbFechaRegistroAlergia" runat="server" CssClass="miTextBoxCalendar"
                                                                                                                            Enabled="False" />
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                                                                        <asp:ImageButton Enabled="False" runat="server" ID="imageBF1" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                            AlternateText="Elija una fecha del calendario" />
                                                                                                                        <atk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbFechaRegistroAlergia"
                                                                                                                            PopupButtonID="imageBF1" Format="dd/MM/yyyy" CssClass="MyCalendar" Enabled="True" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Tipo de Alergía : &nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlTipoAlergia" runat="server" Width="200px" OnSelectedIndexChanged="ddlTipoAlergia_SelectedIndexChanged"
                                                                                                                AutoPostBack="True">
                                                                                                            </asp:DropDownList>                                                                                                            
                                                                                                        </td>                  
                                                                                                         <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <asp:ImageButton ID="btnAgregarTipoAlergia" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" OnClick="btnAgregarTipoAlergia_Click"
                                                                                                                ToolTip="Agregar Tipo de Alergia" />
                                                                                                        </td>                                                                                      
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Alergías : &nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlAlergia" runat="server" Width="200px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                         <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <asp:ImageButton ID="btnAgregarAlergia" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" OnClick="btnAgregarAlergia_Click"
                                                                                                                ToolTip="Agregar Alergia" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="3">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_Alergia" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_Alergia_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_Alergia" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_Alergia_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="controlAlergia" style="display: none">
                                                                                                    <input type="button" id="OKAlergia" />
                                                                                                    <input type="button" id="CancelAlergia" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="3">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td colspan="2" style="width: 240px; height: 26px; text-align: center; color: White;
                                                                                                        font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                                                        Fecha de Registro
                                                                                                    </td>
                                                                                                    <td style="width: 320px; height: 26px; color: White; font-size: 10px;" align="center"
                                                                                                        class="miGVBusquedaFicha_Header">
                                                                                                        Alergías
                                                                                                    </td>
                                                                                                    <td style="width: 200px; height: 26px; color: White; font-size: 10px;" align="center"
                                                                                                        class="miGVBusquedaFicha_Header">
                                                                                                        Tipo de Alergías
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_Alergia" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                                                                            OnClick="btn_Add_Alergia_Click" ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 590px; height: 25px" align="center" valign="top" colspan="5">
                                                                                                        <asp:UpdatePanel ID="upAlergia" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantFichaRegitros">
                                                                                                                    <asp:GridView ID="gvDetalleAlergia" runat="server" CssClass="miGVBusquedaFicha" GridLines="None"
                                                                                                                        AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False" OnRowDataBound="gvDetalleAlergia_RowDataBound"
                                                                                                                        OnRowCommand="gvDetalleAlergia_RowCommand" ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelFichaMedAlergias") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelFichaMedAlergias") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoAlergia">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoAlergia" runat="server" Text='<%# Bind("CodigoAlergia") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoTipoAlergia" runat="server" Text='<%# Bind("CodigoTipoAlergia") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaRegistroAlergia" runat="server" Text='<%# Bind("FechaRegistro") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="180px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblAlergia" runat="server" Text='<%# Bind("Alergia") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="320px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblTipoAlergia" runat="server" Text='<%# Bind("TipoAlergia") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="230px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnMostrarAlergia" runat="server" Text="Button" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_ES_CaracteristicasPiel" runat="server">
                                                                                <legend>Caracteristicas de la Piel</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td colspan="4" height="10px">
                                                                                            <atk:ModalPopupExtender ID="pnModalCaracteristicasPiel" runat="server" TargetControlID="btnMostrarCaracteristicaPiel"
                                                                                                PopupControlID="pnl_PopUp_CaracteristicasPiel" BackgroundCssClass="MiModalBackground"
                                                                                                  Drag="True" OkControlID="OKCaracteristicaPiel" CancelControlID="CancelCaracteristicaPiel"
                                                                                                DynamicServicePath="" Enabled="True" PopupDragHandleControlID="CaracteristicasPielHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_CaracteristicasPiel" BackColor="White" BorderColor="Black"
                                                                                                runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                                                                    <tr id="CaracteristicasPielHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 360px; height: 26px" colspan="4" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Caracteristicas de la Piel</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Fecha de Registro&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hdCodigoCaracteristicasPiel" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                                                                <tr>
                                                                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                                                                        <asp:TextBox ID="tbFechaRegistroCaracteristicasPiel" runat="server" CssClass="miTextBoxCalendar"
                                                                                                                            Enabled="False" />
                                                                                                                             <atk:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                                                                                                                                TargetControlID="tbFechaRegistroCaracteristicasPiel"
                                                                                                                                UserDateFormat="DayMonthYear"                                                                    
                                                                                                                                Mask="99/99/9999" 
                                                                                                                                MaskType="Date" 
                                                                                                                                PromptCharacter="-" CultureAMPMPlaceholder="" 
                                                                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                                                                                            </atk:MaskedEditExtender> 
                                                                                                                                                                                                </td>
                                                                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                                                                        <asp:ImageButton Enabled="False" runat="server" ID="imageBF3" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                            AlternateText="Elija una fecha del calendario" />
                                                                                                                        <atk:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbFechaRegistroCaracteristicasPiel"
                                                                                                                            PopupButtonID="imageBF3" Format="dd/MM/yyyy" CssClass="MyCalendar" Enabled="True" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Tipo de carácteristicas :</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlCaracteristicaPiel" runat="server" Width="200px">
                                                                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <asp:ImageButton ID="btnAgregarTipoCaracteristica" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" OnClick="btnAgregarTipoCaracteristica_Click"
                                                                                                                ToolTip="Agregar TipoCaracteristicas" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="4">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_CaracteristicaPiel" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_CaracteristicaPiel_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_CaracteristicaPiel" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_CaracteristicaPiel_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="Div1" style="display: none">
                                                                                                    <input type="button" id="OKCaracteristicaPiel" />
                                                                                                    <input type="button" id="CancelCaracteristicaPiel" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td colspan="2" style="width: 360px; height: 26px; text-align: center; color: White;
                                                                                                        font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                                                        Fecha de Registro
                                                                                                    </td>
                                                                                                    <td style="width: 400px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Tipo de carácteristicas en la Piel
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_CaracteristicasPiel" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btn_Add_CaracteristicasPiel_Click"
                                                                                                            ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="4">
                                                                                                        <asp:UpdatePanel ID="upCaracteristicaPiel" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantFichaRegitros">
                                                                                                                    <asp:GridView ID="gvDetalleCaracteristicaPiel" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleCaracteristicaPiel_RowDataBound" OnRowCommand="gvDetalleCaracteristicaPiel_RowCommand"
                                                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelFichaMedCaractPiel") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelFichaMedCaractPiel") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoCaracteristicapiel">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoCaracteristicapiel" runat="server" Text='<%# Bind("CodigoCaracteristicapiel") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaRegistroCaracteristicapiel" runat="server" Text='<%# Bind("FechaRegistro") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="300px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCaracteristicaPiel" runat="server" Text='<%# Bind("CaracteristicaPiel") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="430px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnMostrarCaracteristicaPiel" runat="server" Text="Button" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_ES_MedicamentosUsoActual" runat="server">
                                                                                <legend>Medicamentos</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td colspan="2" height="10px">
                                                                                            <atk:ModalPopupExtender ID="pnModalMedicamentos" runat="server" TargetControlID="btnMostrarMedicamentos"
                                                                                                PopupControlID="pnl_PopUp_Medicamentos" BackgroundCssClass="MiModalBackground"
                                                                                                  Drag="True" OkControlID="OKMedicamentos" CancelControlID="CancelMedicamentos"
                                                                                                DynamicServicePath="" Enabled="True" PopupDragHandleControlID="MedicamentosHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_Medicamentos" BackColor="White" BorderColor="Black" runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="520px">
                                                                                                    <tr id="MedicamentosHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 520px; height: 26px" colspan="4" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Medicamentos</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80px; height: 26px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Medicamento :&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidencodigoMedicamento" runat="server" />
                                                                                                        </td>
                                                                                                        <td colspan="3" style="width: 147px; height: 26px;" align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlMedicamento" runat="server" Width="170px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80px; height: 26px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Presentación :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 180px; height: 26px" align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlPresentacion" runat="server" Width="170px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td colspan="2" style="width: 260px; height: 26px" align="left" valign="middle">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="260px">
                                                                                                                <tr>
                                                                                                                    <td style="width: 160px; height: 26px" align="left" valign="middle">
                                                                                                                        <span style="padding-left: 10px">Cantidad de la presentación : &nbsp;</span>
                                                                                                                    </td>
                                                                                                                    <td style="width: 100px; height: 26px" align="left" valign="middle">
                                                                                                                        <asp:TextBox ID="tbCantidadPres" runat="server" CssClass="miTextBox" Width="70px"
                                                                                                                            MaxLength="150" Height="18px" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80px; height: 26px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Dosis :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 180px; height: 51px" align="left" valign="middle">
                                                                                                            <asp:TextBox ID="tbDosisMedi" runat="server" CssClass="miTextBox" Width="170px" Height="35px"
                                                                                                                Rows="2" TextMode="MultiLine" />
                                                                                                        </td>
                                                                                                        <td style="width: 80px; height: 26px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Observaciones :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="padding-right: 10px; width: 180px; height: 51px" align="left" valign="middle">
                                                                                                            <asp:TextBox ID="tbObservacionMedi" runat="server" CssClass="miTextBox" Width="170px"
                                                                                                                Height="35px" Rows="2" TextMode="MultiLine" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 520px; height: 26px" align="center" valign="middle" colspan="4">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_Medicamentos" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_Medicamentos_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_Medicamentos" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_Medicamentos_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4" height="5px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="controlMedicamentos" style="display: none">
                                                                                                    <input type="button" id="OKMedicamentos" />
                                                                                                    <input type="button" id="CancelMedicamentos" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td colspan="2" style="width: 180px; height: 26px; text-align: center; color: White;
                                                                                                        font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                                                        Medicamento
                                                                                                    </td>
                                                                                                    <td style="width: 200px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Presentación / Cantidad
                                                                                                    </td>
                                                                                                    <td style="width: 150px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Dosis
                                                                                                    </td>
                                                                                                    <td style="width: 230px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Observaciones
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_Medicamentos" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btn_Add_Medicamentos_Click"
                                                                                                            ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="6">
                                                                                                        <asp:UpdatePanel ID="upMedicamentos" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantFichaRegitros">
                                                                                                                    <asp:GridView ID="gvDetalleMedicamento" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleMedicamento_RowDataBound" OnRowCommand="gvDetalleMedicamento_RowCommand"
                                                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelFichaAtenMedicamentos") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelFichaAtenMedicamentos") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoMedicamento">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoMedicamento" runat="server" Text='<%# Bind("CodigoMedicamento") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoPresentacion" runat="server" Text='<%# Bind("CodigoPresentacion") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCantidadPresentacion" runat="server" Text='<%# Bind("CantidadPresentacion") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblMedicamento" runat="server" Text='<%# Bind("Medicamento") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="140px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblPresentCant" runat="server" Text='<%# Bind("PresentCant") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="190px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblDosisMedicamento" runat="server" Text='<%# Bind("DosisMedicamento") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="150px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblObservaciones" runat="server" Text='<%# Bind("Observaciones") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="250px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnMostrarMedicamentos" runat="server" Text="Button" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </atk:TabPanel>
                                                                <atk:TabPanel ID="miTab3" runat="server" HeaderText="Tab3">
                                                                    <HeaderTemplate>
                                                                        Otros Datos Medicos
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <div id="Bloque_OtrosDatosMedicos" runat="server" style="border: 0; margin: 0;">
                                                                            <fieldset id="FM_OD_Otorrino" runat="server">
                                                                                <legend>Otorrino</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td style="width: 230px;" align="left">
                                                                                            ¿Tiene el Tabique desviado? :
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 21px" align="left">
                                                                                            <asp:Label ID="lblTabiqueDesviado" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:RadioButtonList ID="rbTabiqueDesviado" runat="server" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" style="width: 230px;">
                                                                                            ¿Ha presentado Sangrado Nasal? :
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 21px" align="left">
                                                                                            <asp:Label ID="lblSangradoNasal" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:RadioButtonList ID="rbSangradoNasal" runat="server" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_OD_Oftalmologico" runat="server">
                                                                                <legend>Oftalmológico</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td style="width: 230px;" align="left">
                                                                                            Descripción :&nbsp;
                                                                                        </td>
                                                                                        <td style="width: 560px;" align="left" valign="middle">
                                                                                            <asp:TextBox ID="tbObservacionesOftalmologicas" runat="server" CssClass="miTextBoxMultiLine"
                                                                                                Enabled="true" Height="42px" Rows="3" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                                                                            <asp:Label ID="lblObservacionesOftamologicas" runat="server" Text="" Width="360px"
                                                                                                Height="35px" Rows="3" TextMode="MultiLine" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 230px;" align="left">
                                                                                            ¿Usa lentes? :
                                                                                        </td>
                                                                                        <td style="width: 560px;" align="left">
                                                                                            <asp:Label ID="lblUsaLentes" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:RadioButtonList ID="rbUsaLentes" runat="server" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_OD_Dental" runat="server">
                                                                                <legend>Dental</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 21px" align="left">
                                                                                            Descripción :
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 21px" align="left">
                                                                                            <asp:TextBox ID="tbObservacionesDental" runat="server" CssClass="miTextBoxMultiLine"
                                                                                                Enabled="true" Height="42px" Rows="3" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                                                                            <asp:Label ID="lblObservacionesDental" runat="server" Text="" Width="360px" Height="35px"
                                                                                                Rows="3" TextMode="MultiLine" Visible="False"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 230px; height: 21px" align="left">
                                                                                            ¿Usa Aparatos de Ortodoncia? :
                                                                                        </td>
                                                                                        <td style="width: 560px; height: 21px" align="left">
                                                                                            <asp:Label ID="lblUsaOrtodoncia" runat="server" Width="150px" Visible="False"></asp:Label>
                                                                                            <asp:RadioButtonList ID="rbUsaOrtodoncia" runat="server" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_OD_Hospitalizaciones" runat="server">
                                                                                <legend>Hospitalizaciones</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td colspan="3" height="10px">
                                                                                            <atk:ModalPopupExtender ID="pnModalHospitalizacion" runat="server" TargetControlID="btnmostrarHospitalizacion"
                                                                                                PopupControlID="pnl_PopUp_Hospitalizacion" BackgroundCssClass="MiModalBackground"
                                                                                                  Drag="True" OkControlID="OKHospitalizacion" CancelControlID="CancelHospitalizacion"
                                                                                                DynamicServicePath="" Enabled="True" PopupDragHandleControlID="HospitalizacionHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_Hospitalizacion" BackColor="White" BorderColor="Black" runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                                                                    <tr id="HospitalizacionHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 360px; height: 26px" colspan="3" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Hospitalizacion</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Fecha de Hospitalización&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidencodigoHospitalizacion" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                                                                <tr>
                                                                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                                                                        <asp:TextBox ID="tbFechaHospitalizacion" runat="server" CssClass="miTextBoxCalendar" />
                                                                                                                         <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                                                                                                TargetControlID="tbFechaHospitalizacion"
                                                                                                                                UserDateFormat="DayMonthYear"                                                                    
                                                                                                                                Mask="99/99/9999" 
                                                                                                                                MaskType="Date" 
                                                                                                                                PromptCharacter="-">
                                                                                                                            </atk:MaskedEditExtender> 
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                                                                        <asp:ImageButton runat="server" ID="imageBF5" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                            AlternateText="Elija una fecha del calendario" />
                                                                                                                        <atk:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tbFechaHospitalizacion"
                                                                                                                            PopupButtonID="imageBF5" Format="dd/MM/yyyy" CssClass="MyCalendar" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Hospitalización :</span>
                                                                                                        </td>
                                                                                                        <td style="width: 200px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlHospitalizacion" runat="server" Width="170px">
                                                                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                         <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <asp:ImageButton ID="btnHospitalizacion" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" OnClick="btnHospitalizacion_Click"
                                                                                                                ToolTip="Agregar Hospitalizacion" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="3">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_Hospitalizacion" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_Hospitalizacion_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_Hospitalizacion" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_Hospitalizacion_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="controlHospitalizacion" style="display: none">
                                                                                                    <input type="button" id="OKHospitalizacion" />
                                                                                                    <input type="button" id="CancelHospitalizacion" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                    </td>
                                                                                                    <td style="width: 300px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        Fecha de Hospitalización
                                                                                                    </td>
                                                                                                    <td style="width: 400px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Motivo de Hospitalización
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_Hospitalizacion" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btn_Add_Hospitalizacion_Click"
                                                                                                            ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="5">
                                                                                                        <asp:UpdatePanel ID="upHospitalizacion" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantFichaRegitros">
                                                                                                                    <asp:GridView ID="gvDetalleHospitalizacion" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleHospitalizacion_RowDataBound" OnRowCommand="gvDetalleHospitalizacion_RowCommand"
                                                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelFichaMedMotivoHosp") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelFichaMedMotivoHosp") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoMotivoHospitalizacion">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoMotivoHospitalizacion" runat="server" Text='<%# Bind("CodigoMotivoHospitalizacion") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaHospitalizacion" runat="server" Text='<%# Bind("FechaHospitalizacion") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="300px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblHospitalizacion" runat="server" Text='<%# Bind("Hospitalizacion") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="430px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnmostrarHospitalizacion" runat="server" Text="Button" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_OD_Operaciones" runat="server">
                                                                                <legend>Operaciones</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td colspan="3" height="10px">
                                                                                            <atk:ModalPopupExtender ID="pnModalOperacion" runat="server" TargetControlID="btnmostrarOperacion"
                                                                                                PopupControlID="pnl_PopUp_Operaciones" BackgroundCssClass="MiModalBackground"
                                                                                                  Drag="True" OkControlID="OKOperacion" CancelControlID="CancelOperacion"
                                                                                                DynamicServicePath="" Enabled="True" PopupDragHandleControlID="OperacionesHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_Operaciones" BackColor="White" BorderColor="Black" runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                                                                    <tr id="OperacionesHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 360px; height: 26px" colspan="3" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Operaciones</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Fecha de Operación&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidencodigoOperacion" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                                                                <tr>
                                                                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                                                                        <asp:TextBox ID="tbFechaOperacion" runat="server" CssClass="miTextBoxCalendar" />
                                                                                                                         <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                                                                                                TargetControlID="tbFechaOperacion"
                                                                                                                                UserDateFormat="DayMonthYear"                                                                    
                                                                                                                                Mask="99/99/9999" 
                                                                                                                                MaskType="Date" 
                                                                                                                                PromptCharacter="-">
                                                                                                                            </atk:MaskedEditExtender> 
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                                                                        <asp:ImageButton runat="server" ID="imageBF6" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                            AlternateText="Elija una fecha del calendario" />
                                                                                                                        <atk:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="tbFechaOperacion"
                                                                                                                            PopupButtonID="imageBF6" Format="dd/MM/yyyy" CssClass="MyCalendar" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Operación :</span>
                                                                                                        </td>
                                                                                                        <td style="width: 200px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlOperacion" runat="server" Width="170px">
                                                                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                          <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <asp:ImageButton ID="btnAgregarOperaciones" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" OnClick="btnAgregarOperaciones_Click"
                                                                                                                ToolTip="Agregar Operaciones" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="3">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_Operacion" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_Operacion_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_Operacion" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_Operacion_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="controlOperacion" style="display: none">
                                                                                                    <input type="button" id="OKOperacion" />
                                                                                                    <input type="button" id="CancelOperacion" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                    </td>
                                                                                                    <td style="width: 300px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        Fecha de Operación
                                                                                                    </td>
                                                                                                    <td style="width: 400px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Operación
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_Operacion" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btn_Add_Operacion_Click"
                                                                                                            ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 590px; height: 25px" align="center" valign="top" colspan="5">
                                                                                                        <asp:UpdatePanel ID="upOperacion" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantFichaRegitros">
                                                                                                                    <asp:GridView ID="gvDetalleOperacion" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleOperacion_RowDataBound" OnRowCommand="gvDetalleOperacion_RowCommand"
                                                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelFichaMedOperaciones") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelFichaMedOperaciones") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoTipoOperaciones">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoTipoOperaciones" runat="server" Text='<%# Bind("CodigoTipoOperaciones") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaOperacion" runat="server" Text='<%# Bind("FechaOperacion") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="300px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblOperacion" runat="server" Text='<%# Bind("Operacion") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="430px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnmostrarOperacion" runat="server" Text="Button" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </atk:TabPanel>
                                                                <atk:TabPanel ID="miTab4" runat="server" HeaderText="Tab4">
                                                                    <HeaderTemplate>
                                                                        Controles de salud
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <div id="Bloque_ControlSalud" runat="server" style="border: 0; margin: 0;">
                                                                            <fieldset id="FM_CS_ControlPesoTalla" runat="server">
                                                                                <legend>Control de Peso - Talla </legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td colspan="2" height="10px" width="790px">
                                                                                            <atk:ModalPopupExtender ID="pnModalControlPesoTalla" runat="server" TargetControlID="btnMostrarControlPesoTalla"
                                                                                                PopupControlID="pnl_PopUp_ControlPesoTalla" BackgroundCssClass="MiModalBackground"
                                                                                                  Drag="True" OkControlID="OKControlPesoTalla" CancelControlID="CancelControlPesoTalla"
                                                                                                DynamicServicePath="" Enabled="True" PopupDragHandleControlID="ControlPesoTallaHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_ControlPesoTalla" BackColor="White" BorderColor="Black"
                                                                                                runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                                                                    <tr id="ControlPesoTallaHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 360px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Control de Peso y Talla</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Fecha de Registro&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidenCodigoControlPesoTalla" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                                                                <tr>
                                                                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                                                                        <asp:TextBox ID="tbFechaControlPesoTalla" runat="server" CssClass="miTextBoxCalendar"
                                                                                                                            Enabled="False" />
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                                                                        <asp:ImageButton Enabled="False" runat="server" ID="imageBF7" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                            AlternateText="Elija una fecha del calendario" />
                                                                                                                        <atk:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="tbFechaControlPesoTalla"
                                                                                                                            PopupButtonID="imageBF7" Format="dd/MM/yyyy" CssClass="MyCalendar" 
                                                                                                                            Enabled="True" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Talla : &nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:TextBox ID="tbTalla" runat="server" CssClass="miTextBox" Text="0.00" Width="50px"
                                                                                                                MaxLength="4" />
                                                                                                            0.00 cm.
                                                                                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="tbTalla" ValidChars="'.'" Enabled="True">
                                                                                                            </atk:FilteredTextBoxExtender>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Peso :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:TextBox ID="tbPeso" runat="server" CssClass="miTextBox" Text="0.00" Width="50px"
                                                                                                                MaxLength="6" />
                                                                                                            00.00 kg.
                                                                                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="tbPeso" Enabled="True" ValidChars="'.'">
                                                                                                            </atk:FilteredTextBoxExtender>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px;" align="left">
                                                                                                            <span style="padding-left: 10px">Observaciones :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px;" align="left" valign="middle">
                                                                                                            <asp:TextBox ID="tbObservacionTallaPeso" runat="server" 
                                                                                                                CssClass="miTextBoxMultiLine" Height="42px" Rows="3" TextMode="MultiLine" 
                                                                                                                Width="200px" MaxLength="600"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_ControlPesoTalla" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_ControlPesoTalla_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_ControlPesoTalla" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_ControlPesoTalla_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="ControlPesoTalla" style="display: none">
                                                                                                    <input type="button" id="OKControlPesoTalla" />
                                                                                                    <input type="button" id="CancelControlPesoTalla" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td colspan="2" style="width: 250px; height: 26px; text-align: center; color: White;
                                                                                                        font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                                                        Fecha de Registro
                                                                                                    </td>
                                                                                                    <td style="width: 80px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Talla
                                                                                                    </td>
                                                                                                    <td style="width: 80px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Peso
                                                                                                    </td>
                                                                                                    <td style="width: 350px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Observaciones
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_ControlPesoTalla" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btn_Add_ControlPesoTalla_Click"
                                                                                                            ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="6">
                                                                                                        <asp:UpdatePanel ID="upControlPesoTalla" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantFichaRegitros">
                                                                                                                    <asp:GridView ID="gvDetalleControlPesoTalla" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleControlPesoTalla_RowDataBound" OnRowCommand="gvDetalleControlPesoTalla_RowCommand"
                                                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoControlPesoTalla") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoControlPesoTalla") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaControlPesoTalla" runat="server" Text='<%# Bind("FechaControl") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="150px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblTalla" runat="server" Text='<%# Bind("Talla") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="100px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblPeso" runat="server" Text='<%# Bind("Peso") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="100px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblObservacionesPesoTalla" runat="server" Text='<%# Bind("Observaciones") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="380px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                               </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnMostrarControlPesoTalla" runat="server" Text="Button" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                            <fieldset id="FM_CS_OtrosControles" runat="server">
                                                                                <legend>Otros de Controles</legend>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td colspan="2" height="10px">
                                                                                            <atk:ModalPopupExtender ID="pnModalOtrosControles" runat="server" TargetControlID="btnMostrarTipoControl"
                                                                                                PopupControlID="pnl_PopUp_OtrosControles" BackgroundCssClass="MiModalBackground"
                                                                                                  Drag="True" OkControlID="OKTipoControl" CancelControlID="CancelTipoControl"
                                                                                                DynamicServicePath="" Enabled="True" PopupDragHandleControlID="OtrosControlesHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_OtrosControles" BackColor="White" BorderColor="Black" runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                                                                    <tr id="OtrosControlesHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 360px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Otros Controles</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Fecha de Registro&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidenCodigoTipoControl" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                                                                <tr>
                                                                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                                                                        <asp:TextBox ID="tbFechaTipoControl" runat="server" CssClass="miTextBoxCalendar"
                                                                                                                            Enabled="False" />
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                                                                        <asp:ImageButton Enabled="False" runat="server" ID="imageBF0" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                            AlternateText="Elija una fecha del calendario" />
                                                                                                                        <atk:CalendarExtender ID="calendario2" runat="server" TargetControlID="tbFechaTipoControl"
                                                                                                                            PopupButtonID="imageBF0" Format="dd/MM/yyyy" CssClass="MyCalendar" 
                                                                                                                            Enabled="True" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Tipo de Control : &nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlTipoControl" runat="server" Width="200px">
                                                                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Resultado :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px;" align="left" valign="middle">
                                                                                                            <asp:TextBox ID="tbResultadoTipoControl" runat="server" 
                                                                                                                CssClass="miTextBoxMultiLine" Height="42px" Rows="3" TextMode="MultiLine" 
                                                                                                                Width="200px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_OtrosControles" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_OtrosControles_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_OtrosControles" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_OtrosControles_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="controlTipoControl" style="display: none">
                                                                                                    <input type="button" id="OKTipoControl" />
                                                                                                    <input type="button" id="CancelTipoControl" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td colspan="2" style="width: 210px; height: 26px; text-align: center; color: White;
                                                                                                        font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                                                        Fecha de Registro
                                                                                                    </td>
                                                                                                    <td style="width: 230px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Tipo de Control
                                                                                                    </td>
                                                                                                    <td style="width: 420px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Resultado
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_OtrosControles" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btn_Add_OtrosControles_Click"
                                                                                                            ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 590px; height: 25px" align="center" valign="top" colspan="5">
                                                                                                        <asp:UpdatePanel ID="upOtrosControles" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantFichaRegitros">
                                                                                                                    <asp:GridView ID="gvDetalleTipoControl" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleTipoControl_RowDataBound" OnRowCommand="gvDetalleTipoControl_RowCommand"
                                                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelFichaMedTiposControles") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelFichaMedTiposControles") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoTipoControl">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoTipoControl" runat="server" Text='<%# Bind("CodigoTipoControl") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaControl" runat="server" Text='<%# Bind("FechaControl") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="130px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblTipoControl" runat="server" Text='<%# Bind("TipoControl") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="200px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblResultado" runat="server" Text='<%# Bind("Resultado") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="400px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnMostrarTipoControl" runat="server" Text="Button" />                                                                                                
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                            <div class="miEspacio">
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </atk:TabPanel>
                                                                 <atk:TabPanel ID="miTab5" runat="server" HeaderText="Tab5">
                                                                    <HeaderTemplate>
                                                                        Datos del Seguro
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                            <div id="Bloque_DatosSeguro" runat="server" style="border: 0; margin: 0;">
                                                                                <fieldset id="FM_DS_DatosSeguro" runat="server">
                                                                                    <legend>Ficha de Seguro</legend>
                                                                                       <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                    <tr>
                                                                                        <td colspan="2" height="10px">
                                                                                            <atk:ModalPopupExtender ID="pnModalDatosSeguro" runat="server" TargetControlID="btnMostrarDatosSeguro"
                                                                                                PopupControlID="pnl_PopUp_DatosSeguro" BackgroundCssClass="MiModalBackground"
                                                                                                  Drag="True" OkControlID="OKDatosSeguro" CancelControlID="CancelDatosSeguro"
                                                                                                DynamicServicePath="" Enabled="True" PopupDragHandleControlID="DatosSeguroHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_DatosSeguro" BackColor="White" BorderColor="Black" runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="520px">
                                                                                                    <tr id="DatosSeguroHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 520px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Datos del Seguro</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                     <tr>
                                                                                                        <td colspan="2" style="height: 15px;" align="right">
                                                                                                            <em>Campos Obligatorios (*)</em>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Año Matrícula&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidenCodigoDatosSeguro" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                                                                                             <asp:DropDownList ID="ddlAnioDatosSeguro" runat="server" Width="200px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Tipo: &nbsp;</span><span class="camposObligatorios">(*)</span>
                                                                                                        </td>
                                                                                                        <td style="width: 360px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlTipoSeguro" runat="server" Width="200px"   AutoPostBack="True" OnSelectedIndexChanged="ddlTipoSeguro_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Compañia :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlCompania" runat="server" Width="200px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                     <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Número de Poliza :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                          <asp:TextBox id="tbNumeroPoliza" runat ="Server" Width ="150px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Tipo Vigencia:&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                              <asp:RadioButtonList ID="rbVigencia" runat="server" RepeatDirection="Horizontal">   
                                                                                                                    <asp:ListItem Value="0"  Selected="True">Indefinida</asp:ListItem>                                                                             
                                                                                                                    <asp:ListItem Value="1">Según Contrato</asp:ListItem> 
                                                                                                                      <asp:ListItem Value="2" >Por fechas</asp:ListItem>      
                                                                                                              </asp:RadioButtonList> 
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Fecha de Vigencia:&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                            <table>
                                                                                                                 <tr>
                                                                                                            
                                                                                                                    <td style="width: 180px; height: 25px;" align="left" valign="middle">                       
                                                                                                                      <table cellpadding="0" cellspacing="0" border="0" style="width: 150px">    
                                                                                                                <tr>
                                                                                                                <td style="width: 110px; height: 25px;" align="left" valign="middle">
                                                                                                                    <asp:TextBox ID="tbRep1_FechaInicio" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
                                                                                                                    
                                                                                                                </td>
                                                                                                                <td style="width: 40px; height: 25px;" align="left" valign="middle">
                                                                                                                    <asp:ImageButton runat="server" ID="imgRep1_FechaInicio" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                        AlternateText="Elija una fecha del calendario" />
                                                                                                                    <atk:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="tbRep1_FechaInicio"
                                                                                                                        PopupButtonID="imgRep1_FechaInicio" Format="dd/MM/yyyy" CssClass="MyCalendar" />
                                                                                                                </td>
                                                                                                                </tr>
                                                                                                                </table>                                     
                                                                                                                    </td>
                                                                                                               
                                                                                                                 
                                                                                                                    <td style="width: 180px; height: 25px;" align="left" valign="middle"> 
                                                                                                                       <table cellpadding="0" cellspacing="0" border="0" style="width: 150px">
                                                                                                                <tr>
                                                                                                                <td style="width: 110px; height: 25px;" align="left" valign="middle">
                                                                                                                    <asp:TextBox ID="tbRep1_FechaFin" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
                                                                                                                    <atk:MaskedEditExtender ID="MaskedEditExtender11" runat="server" TargetControlID="tbRep1_FechaFin"
                                                                                                                        UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
                                                                                                                    </atk:MaskedEditExtender>
                                                                                                                </td>
                                                                                                                <td style="width: 40px; height: 25px;" align="left" valign="middle">
                                                                                                                    <asp:ImageButton runat="server" ID="imgRep1_FechaFin" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                        AlternateText="Elija una fecha del calendario" />
                                                                                                                    <atk:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="tbRep1_FechaFin"
                                                                                                                        PopupButtonID="imgRep1_FechaFin" Format="dd/MM/yyyy" CssClass="MyCalendar" />
                                                                                                                </td>
                                                                                                                </tr>
                                                                                                                </table>  
                                                                                                                    </td>
                                                                                                                </tr>   
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>  
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Clinicas 1:&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                                                                            
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlClinica1" runat="server" Width="200px">
                                                                                                            </asp:DropDownList> 
                                                                                                        </td>
                                                                                                    </tr>    
                                                                                                     <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Clinicas 2:&nbsp;</span>
                                                                                                            
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlClinica2" runat="server" Width="200px">
                                                                                                            </asp:DropDownList> 
                                                                                                        </td>
                                                                                                    </tr>    
                                                                                                     <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Clinicas 3:&nbsp;</span>
                                                                                                            
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlClinica3" runat="server" Width="200px">
                                                                                                            </asp:DropDownList> 
                                                                                                        </td>
                                                                                                    </tr>                                                                                           
                                                                                                     <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Compañia de Ambulancia :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                          <asp:TextBox id="tbNombCompaniaAmb" runat ="Server" Width ="250px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Telefono de Ambulancia :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                          <asp:TextBox id="tbTelfAmbulancia" runat ="Server" Width ="250px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">¿Tiene copia de cárnet de seguro? :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                              <asp:RadioButtonList ID="rdCarnetSeguro" runat="server" RepeatDirection="Horizontal">   
                                                                                                                    <asp:ListItem Value="1" >Si</asp:ListItem>                                                                             
                                                                                                                    <asp:ListItem Value="0" Selected="True">No</asp:ListItem> 
                                                                                                              </asp:RadioButtonList> 
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_DatosSeguro" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_DatosSeguro_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_DatosSeguro" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_DatosSeguro_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="DatosSeguro" style="display: none">
                                                                                                    <input type="button" id="OKDatosSeguro" />
                                                                                                    <input type="button" id="CancelDatosSeguro" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td  style="width: 200px; height: 26px; text-align: center; color: White;
                                                                                                        font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                                                        Año Matrícula
                                                                                                    </td>
                                                                                                    <td style="width: 130px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Tipo 
                                                                                                    </td>
                                                                                                    <td style="width: 150px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        compañia
                                                                                                    </td>
                                                                                                     <td style="width: 100px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        Numero Poliza
                                                                                                    </td>
                                                                                                     <td style="width: 180px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                       Clínicas
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_DatosSeguro" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btn_Add_DatosSeguro_Click"
                                                                                                            ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="6">
                                                                                                        <asp:UpdatePanel ID="upDatosSeguro" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantFichaRegitros">
                                                                                                                    <asp:GridView ID="gvDetalleDatosSeguro" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleDatosSeguro_RowDataBound" OnRowCommand="gvDetalleDatosSeguro_RowCommand"
                                                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelFichaMedDatosSeguro") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelFichaMedDatosSeguro") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>    
                                                                                                                             <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoTipoSeguro" runat="server" Text='<%# Bind("CodigoTipoSeguro") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>                                                                                                                      
                                                                                                                            <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoCompania" runat="server" Text='<%# Bind("CodigoCompania") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoAnio" runat="server" Text='<%# Bind("CodigoAnio") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblVigencia" runat="server" Text='<%# Bind("Vigencia") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaInicio" runat="server" Text='<%# Bind("FechaInicio") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaFin" runat="server" Text='<%# Bind("FechaFin") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoClinica" runat="server" Text='<%# Bind("CodigoClinica") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblAmbulanciaCompania" runat="server" Text='<%# Bind("AmbulanciaCompania") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblTelefonoAmbulancia" runat="server" Text='<%# Bind("TelefonoAmbulancia") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCopiaCarnetSeguro" runat="server" Text='<%# Bind("CopiaCarnetSeguro") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                             <%-- <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoRelacionClinica" runat="server" Text='<%# Bind("CodigoRelacionClinica") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>--%>
                                                                                                                              <asp:TemplateField HeaderText="Anio Matricula">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblAnioMatricula" runat="server" Text='<%# Bind("AnioMatricula") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="100px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="150px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCompania" runat="server" Text='<%# Bind("Compania") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="150px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblNumeroPoliza" runat="server" Text='<%# Bind("NumeroPoliza") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="100px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblClinicas" runat="server" Text='<%# Bind("Clinica") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="200px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnMostrarDatosSeguro" runat="server" Text="Button" />                                                                                                
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                </fieldset>
                                                                                <div class="miEspacio">
                                                                                </div>
                                                                                 <fieldset id="FM_RE_RentaEstudiantil" runat="server">
                                                                                    <legend>Renta Estudiantil</legend>
                                                                                       <table cellpadding="0" cellspacing="0" border="0" width="790">
                                                                                          <tr>
                                                                                        <td colspan="2" height="10px">
                                                                                            <atk:ModalPopupExtender ID="pnModalRentaEstudiantil" runat="server" TargetControlID="btnMostrarRentaEstudiantil"
                                                                                                PopupControlID="pnl_PopUp_RentaEstudiantil" BackgroundCssClass="MiModalBackground"
                                                                                                  Drag="True" OkControlID="OKRentaEstudiantil" CancelControlID="CancelRentaEstudiantil"
                                                                                                DynamicServicePath="" Enabled="True" PopupDragHandleControlID="RentaEstudiantilHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_RentaEstudiantil" BackColor="White" BorderColor="Black" runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="520px">
                                                                                                    <tr id="RentaEstudiantilHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 520px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Datos de la Renta Estudiantil</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                     <tr>
                                                                                                        <td colspan="2" style="height: 15px;" align="right">
                                                                                                            <em>Campos Obligatorios (*)</em>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Año Matrícula&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidenCodigoSeguroRentaEstudiantil" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                                                                                             <asp:DropDownList ID="ddlAnioRentaEst" runat="server" Width="200px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">1er Titular : &nbsp;</span><span class="camposObligatorios">(*)</span>
                                                                                                        </td>
                                                                                                        <td style="width: 360px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddl_Familiar_PriTitular" runat="server" Width="200px" >
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 160px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px"> :&nbsp;</span>2do Titular : <span class="camposObligatorios">(*)</span>
                                                                                                        </td>
                                                                                                        <td style="width: 360px;" align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddl_Familiar_SegTitular" runat="server" Width="200px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_RentaEstudiantil" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_RentaEstudiantil_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_RentaEstudiantil" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_RentaEstudiantil_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="DatosRentaEstudiantil" style="display: none">
                                                                                                    <input type="button" id="OKRentaEstudiantil" />
                                                                                                    <input type="button" id="CancelRentaEstudiantil" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                          <tr>
                                                                                        <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                                                <tr>
                                                                                                    <td  style="width: 200px; height: 26px; text-align: center; color: White;
                                                                                                        font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                                                        Año Matrícula
                                                                                                    </td>
                                                                                                    <td style="width: 130px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        1er Titular 
                                                                                                    </td>
                                                                                                    <td style="width: 150px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                                                        2do Titular
                                                                                                    </td>
                                                                                                    <td style="width: 30px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                                                        <asp:ImageButton ID="btn_Add_RentaEstudiantil" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btn_Add_RentaEstudiantil_Click"
                                                                                                            ToolTip="Agregar" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="6">
                                                                                                        <asp:UpdatePanel ID="upRentaEstudiantil" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <div id="miGVMantRentaEstudiantil">
                                                                                                                    <asp:GridView ID="gvDetalleRentaEstudiantil" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleRentaEstudiantil_RowDataBound" OnRowCommand="gvDetalleRentaEstudiantil_RowCommand"
                                                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelFichaMedRentaEstudiantil") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelFichaMedRentaEstudiantil") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>    
                                                                                                                             <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoRentaEstudiantil" runat="server" Text='<%# Bind("CodigoRelFichaMedRentaEstudiantil") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>                                                                                                                      
                                                                                                                            <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoPrimerTitular" runat="server" Text='<%# Bind("CodigoFamiliarPrimerTitular") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoSegundoTitular" runat="server" Text='<%# Bind("CodigoFamiliarSegundoTitular") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                                 <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoAnioAcademico" runat="server" Text='<%# Bind("CodigoAnioAcademico") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                             <asp:TemplateField HeaderText="">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblAnioAcademico" runat="server" Text='<%# Bind("AnioAcademico") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                              <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="100px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField >
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblPrimerTitular" runat="server" Text='<%# Bind("FamiliarPrimerTitular") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="150px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                              <asp:TemplateField >
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblSegundoTitular" runat="server" Text='<%# Bind("FamiliarSegundoTitular") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                               <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="150px" />
                                                                                                                            </asp:TemplateField>                                                                                     
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div style="display: none">
                                                                                                <asp:Button ID="btnMostrarRentaEstudiantil" runat="server" Text="Button" />                                                                                                
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                       </table>
                                                                                </fieldset>
                                                                            </div>
                                                                    </ContentTemplate>
                                                                </atk:TabPanel>
                                                            </atk:TabContainer>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </ContentTemplate>
                        </atk:TabPanel>
                    </atk:TabContainer>
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

        <uc1:ingresarEnfermedad id="ucIngresarEnfermedad" runat="server" />  
        <uc2:ingresarVacuna id="ucIngresarVacuna" runat="server" />  
        <uc3:ingresarDosis id="ucIngresarDosis" runat="server" />  
        <uc4:ingresarAlergia id="ucIngresarAlergia" runat="server" /> 
        <uc5:ingresarTiposCaracteristicasPiel id="ucIngresarTiposCaracteristicasPiel" runat="server" /> 
        <uc6:ingresarTipoAlergia id="ucIngresarTipoAlergia" runat="server" />
        <uc7:ingresarHospitalizacion id="ucIngresarHospitalizacion" runat="server" /> 
        <uc8:ingresarOperaciones id="ucIngresarOperaciones" runat="server" /> 
    </div>
</asp:Content>
