<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="FichaAlumnos.aspx.vb" Inherits="Modulo_Matricula_FichaAlumnos" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">

    function MostrarImpresionFichaAlumno_html() {

        window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FichaAlumno_html.aspx', '_blank', 'scrollbars=yes,resizable=no,width=860');
    }
    
</script>

<style type="text/css">
               
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
</style>

<script type="text/javascript">

    function abrirPopupParams(url, tbTipo) {

        var urlaux = url + '&Tipo=' + tbTipo;
        window.showModalDialog(urlaux, "#1", "dialogHeight: 500px ; dialogWidth: 800px; center: Yes; help: No; resizable: No; status: No; scroll: Si");

    }

    function abrirPopupRegistroEmpresa(url) {

        var urlaux = url ;
        window.showModalDialog(urlaux, "#1", "dialogHeight: 450px ; dialogWidth: 800px; center: Yes; help: No; resizable: No; status: No; scroll: Si");

    }

//    function abrirPopupRegistroRetiro(url) {
//        var urlaux = url;
//        window.showModalDialog(urlaux, "#1", "dialogHeight: 450px ; dialogWidth: 800px; center: Yes; help: No; resizable: No; status: No; scroll: Si;");
//    }

    </script>   
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="miPaginaMantenimiento_Alumno">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
     
     <Triggers>
        <asp:PostBackTrigger ControlID="btnImprimir_Excel" />
     </Triggers>
    
        <ContentTemplate>
            <div id="miContainerMantenimiento_Alumno">
            
               <atk:TabContainer ID="TabContainer1" 
                                 runat="server" 
                                 Width="870px" 
                                 ActiveTabIndex="0"
                                 AutoPostBack="false" 
                                 ScrollBars="None" >
               
                    <atk:TabPanel ID="miTab1" 
                                  runat="server" 
                                  HeaderText="Tab1" 
                                  Enabled="true">
                        <HeaderTemplate>
                            <asp:Label ID="lbTab1" runat="server" Text="Busqueda" />
                        </HeaderTemplate>
                        <ContentTemplate> 
                            <div style="border: solid 0px blue; width: 850px;">
                            
                                <div id="miBusquedaMant_Alumno"><!-- 850px -->
                                    <fieldset>
                                        <legend>Criterios de busqueda</legend>
                                        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red;
                                            min-width: 810px;">
                                            <tr>
                                                <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                                    <span>Código</span>
                                                </td>
                                                <td style="width: 320px; height: 25px;"  align="left" valign="middle">
                                                    <asp:TextBox ID="tbBuscarCodigo" runat="server" CssClass="miTextBox" Width="320px" MaxLength="8" Height="18px" />
                                                    <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                                   
                                                </td>
                                                <td style="width: 310px; padding-top:6px" align="right" valign="top">
                                                    
                                                            <asp:ImageButton ID="btnBuscar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                                onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                                onclick="btnBuscar_Click" ToolTip="Buscar Registros"/>
                                                    
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                                    <span>Apellido Paterno</span>
                                                </td>
                                                <td style="width: 320px; height: 25px;" align="left" valign="middle">
                                                    <asp:TextBox ID="tbBuscarApellidoPaterno" runat="server" CssClass="miTextBox" 
                                                        Width="320px" MaxLength="100" Height="18px" />
                                                                                        
                                                </td>
                                                  <td style="width: 310px; height: 25px;" align="right" valign="middle">       
                                                     <asp:ImageButton ID="btnLimpiar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                                onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                                onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/> 
                                                            
                                                   </td>          
                                            </tr>
                                            <tr>
                                                <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                                    <span>Apellido Materno</span>
                                                </td>
                                                <td  style="min-width:320px; height: 25px;" align="left" valign="middle">
                                                    <asp:TextBox ID="tbBuscarApellidoMaterno" runat="server" CssClass="miTextBox" 
                                                        Width="320px" MaxLength="100" Height="18px" />
                                                                                        
                                                </td>
                                                 <td style="width: 310px; height: 25px;" align="right" valign="middle">
                                                <asp:ImageButton ID="btnExportar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                                                onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                                                onclick="btnExportar_Click" ToolTip="Limpiar Filtros"/>     
                                                   </td>   
                                            </tr> 
                                             <tr>
                                                <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                                    <span>Nombre</span>
                                                </td>
                                                <td colspan="2" style="min-width: 630px; height: 25px;" align="left" valign="middle">
                                                    <asp:TextBox ID="tbBuscarNombre" runat="server" CssClass="miTextBox" 
                                                        Width="320px" MaxLength="100" Height="18px" />
                                                                                        
                                                </td>
                                            </tr> 
                                            <tr>
                                                <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                                    <span>Año Academico</span>
                                                </td>
                                                <td colspan="2" style="width: 630px; height: 25px;" align="left" valign="middle">
                                                    <asp:DropDownList ID="ddlAnioAcademico1" runat="server" Width="100px">
                                                    </asp:DropDownList>  
                                                    &nbsp;<span>Hasta</span> &nbsp;
                                                     <asp:DropDownList ID="ddlAnioAcademico2" runat="server" Width="100px">
                                                    </asp:DropDownList>                                       
                                                </td>                                     
                                            </tr>  
                                            <tr>
                                                <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                                    <span>Situación de alumno</span>
                                                </td>
                                                <td colspan="2" style="width: 630px; height: 25px;" align="left" valign="middle">
                                                    <asp:DropDownList ID="ddlEstadoAlumno" runat="server" Width="100px">
                                                    </asp:DropDownList>                                       
                                                </td>
                                            </tr>   
                                            <tr>
                                                <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                                    <span>Sede</span>
                                                </td>
                                                <td colspan="2" style="width: 630px; height: 25px;" align="left" valign="middle">
                                                    <asp:DropDownList ID="ddlSede" runat="server" Width="100px">                                        
                                                    </asp:DropDownList>                                       
                                                </td>
                                            </tr> 
                                            <tr>
                                                 <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                                                <span>Nivel</span>
                                                            </td>
                                                 <td colspan="2" style="width: 630px; height: 25px;" align="left" valign="middle">
                                                                <asp:DropDownList ID="ddlNiveles" runat="server" Width="100px" 
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlNiveles_SelectedIndexChanged">
                                                                </asp:DropDownList>                                             
                                                 </td>
                                            </tr>   
                                            <tr>
                                                <td align="left" style="width: 180px; height: 25px;" valign="middle">
                                                    <span>SubNivel</span>
                                                </td>
                                                <td colspan="2" align="left" style="width: 630px; height: 25px;" valign="middle">
                                                    <asp:DropDownList ID="ddlSubniveles" runat="server" Width="100px"  
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubniveles_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr> 
                                            <tr>
                                                            <td style="width:180px; height: 25px;" align="left" valign="middle">
                                                                <span>Grado</span>
                                                            </td>
                                                            <td colspan="2" style="width: 630px; height: 25px;" align="left" valign="middle">
                                                                <asp:DropDownList ID="ddlGrados" runat="server" Width="100px" 
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlGrados_SelectedIndexChanged">
                                                                </asp:DropDownList>                                             
                                                            </td>
                                           </tr>
                                            <tr>
                                                            <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                                                <span>Aula</span>
                                                            </td>
                                                            <td colspan="2" style="width:630px; height: 25px;" align="left" valign="middle">
                                                                <asp:DropDownList ID="ddlAulas" runat="server" Width="100px">
                                                                </asp:DropDownList>                                             
                                                            </td>
                                           </tr>
                                                                                       
                                        </table>
                                    </fieldset>
                                </div>
                                
                                <div class="miEspacio">
                                </div>                    
                                
                                <div id="miGridviewMant_Alumno">
                                    <asp:GridView ID="GridView1" runat="server" 
                                        CssClass="miGridviewBusqueda_Alumno" 
                                        GridLines="None" 
                                        AutoGenerateColumns="False"
                                        AllowPaging="True" 
                                        AllowSorting="True"
                                        EmptyDataText=" - No se encontraron resultados - "
                                        OnPageIndexChanging="GridView1_PageIndexChanging" 
                                        OnRowDataBound="GridView1_RowDataBound"
                                        OnRowCommand="GridView1_RowCommand"
                                        OnRowCreated="GridView1_RowCreated"
                                        OnSorting="GridView1_Sorting">
                                        <HeaderStyle CssClass="miGridviewBusqueda_Header_Alumno" Font-Underline="False" 
                                            ForeColor="White" HorizontalAlign="Center" />
                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                        <PagerStyle CssClass="miGridviewBusqueda_Footer_Alumno" HorizontalAlign="Center" />                                                                                 
                                        <Columns>    
                                                               
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Actualizar Registro" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows_Alumno" HorizontalAlign="Center" Width="30px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_ver.png" 
                                                        CommandName="Ver" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Ver Registro" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows_Alumno" HorizontalAlign="Center" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnActivar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_printer.png" 
                                                        CommandName="Imprimir" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Imprimir Registro" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows_Alumno" HorizontalAlign="Center" Width="30px" />
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Codigo">  
                                                <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="width:100px;" align="center" valign="middle">Codigo&nbsp;</td>
                                                        <%--<td style="width:50px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_CodigoAlumno" runat="server" 
                                                            ToolTip="Descendente"    
                                                            ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                            CommandName="Sort" 
                                                            CommandArgument="CodigoAlumno"/></td>--%>
                                                    </tr>
                                                </table>                                    
                                                </HeaderTemplate>                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows_Alumno" HorizontalAlign="Center" Width="100px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="NombreCompleto">  
                                                <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="width:200px;" align="right" valign="middle">NombreCompleto&nbsp;</td>
                                                        <td style="width:265px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreCompleto" runat="server" 
                                                            ToolTip="Descendente"    
                                                            ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                            CommandName="Sort" 
                                                            CommandArgument="NombreCompleto"/></td>
                                                    </tr>
                                                </table>                                    
                                                </HeaderTemplate>                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="Label" runat="server" Text='<%# Bind("NombreCompleto") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="540px"/>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows_Alumno" HorizontalAlign="Left" Width="540px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Estado/Nivel/SubNivel/Grado/Aula">  
                                                <HeaderTemplate>
                                                     <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="width:350px;" align="center" valign="middle">
                                                            Estado/Nivel/SubNivel/Grado/Aula&nbsp;</td>
                                                       
                                                    </tr>
                                                </table>                                    
                                                </HeaderTemplate>                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblENSnGS" runat="server" Text='<%# Bind("ENSnGS") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="540px"/>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows_Alumno" HorizontalAlign="Left" Width="540px" />
                                                
                                            </asp:TemplateField>
                                               <asp:TemplateField >  
                                                 <ItemTemplate>
                                                    <asp:Label ID="lblAnioGrilla" runat="server" Text='<%# Bind("AnioAcademico") %>' />
                                                </ItemTemplate>
                                                   <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                   <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 628px;">
                                                <tr>                                        
                                                    <td style="height: 20px; width: 200px;" align="left" valign="middle">
                                                        <span class="miFooterMantLabelLeft_Alumno">Ir a página   </span>                                         
                                                        <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                            CssClass="letranormal" 
                                                            AutoPostBack="true" 
                                                            OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                                        </asp:DropDownList>&nbsp; de
                                                        <asp:Label ID="lblNumPaginas" runat="server" />                                         
                                                    </td>                                        
                                                    <td style="height: 20px; width: 228px;" align="center" valign="middle">                                           
                                                        <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Primera Pagina" CommandArgument="First"
                                                            CssClass="pagfirst" />
                                                        <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Página anterior"
                                                            CommandArgument="Prev" CssClass="pagprev" />
                                                        <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Página siguiente"
                                                            CommandArgument="Next" CssClass="pagnext" />
                                                        <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Última Pagina" CommandArgument="Last"
                                                            CssClass="paglast" />
                                                    </td>                                        
                                                    <td style="height: 20px; width: 200px;" align="right" valign="middle">
                                                        <asp:Label ID="lblRegistrosActuales" runat="server" CssClass="miFooterMantLabelRight_Alumno" />
                                                    </td>                                        
                                                </tr>
                                            </table>
                                        </PagerTemplate>
                                    </asp:GridView>
                                </div>
                                <div class="miEspacio">
                                </div>
                                <div id="GVLegenda_Alumno">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 830px;">
                                        <tr>
                                            <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                <img alt="Actualizar Registro" src="../App_Themes/Imagenes/opc_actualizar.png"/></td>
                                            <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                <span>Actualizar Registro</span></td>                              
                                            <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                <span>|</span></td>      
                                            <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                <img alt="Eliminar Registro" src="../App_Themes/Imagenes/opc_ver.png"/></td>
                                            <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                <span>Ver Registro</span></td>  
                                            <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                <span>|</span></td>                                    
                                            <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                <img alt="Activar Registro" src="../App_Themes/Imagenes/opc_printer.png"/></td>
                                            <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                <span>Imprimir Registro</span></td>                                      
                                            <td style="width: 200px"></td>                                                                     
                                        </tr>                        
                                    </table>
                                </div>   
                            </div>
                        </ContentTemplate>
                    </atk:TabPanel>     
        
        <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="False">
            <HeaderTemplate>
                 <asp:Label ID="lbTab2" runat="server" Text="Actualización" />
            </HeaderTemplate>
            <ContentTemplate> 

    <div id="miPaginaFichaAlumno">
    
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            
                <div id="miContenidoFichaAlumno">
                
                    <div id="miCabeceraFichaAlumno">
                     <table width="800" cellpadding="0" cellspacing="0" border="0" >
                    <tr >
                        <td   style ="width: 400px;" rowspan ="5">
                        <fieldset id="Bloque_SituacionActual" runat="server">
                            <legend>Situación Actual</legend>
                            <table cellpadding="0" cellspacing="0" border="0" width="600px">
                                
                                <tr>
                                    <td style="width: 74px; height: 100px; background: #FFFFFF url(../App_Themes/Imagenes/img_bg.gif) no-repeat; background-position: center center;"
                                        align="center" valign="middle" rowspan="4">
                                        <asp:Image ID="imgFotoAlumno" runat="server" Width="54" Height="64" Style="border: #7f9db9 1px solid"
                                            ImageUrl="~/Fotos/noPhoto.gif" />
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="9">
                                    </td>
                                    <td style="width: 110px; height: 25px;" align="left" valign="middle">
                                        <span>Nombre Completo:&nbsp;</span>
                                        <asp:HiddenField ID="hd_Codigo" runat="server" />
                                        <asp:HiddenField ID="hd_AnioGrilla" runat="server" />
                                        <asp:HiddenField ID="hd_CodigoPersona" runat="server" />
                                        <asp:HiddenField ID="hd_CodigoRelacionIdiomasPersonas1" runat="server" />
                                        <asp:HiddenField ID="hd_CodigoRelacionIdiomasPersonas2" runat="server" />
                                        <asp:HiddenField ID="hd_CodigoRelacionNacionalidadesPersonas1" runat="server" />
                                        <asp:HiddenField ID="hd_CodigoRelacionNacionalidadesPersonas2" runat="server" />
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="9">
                                    </td>
                                    <td style="width: 321px; height: 25px; " align="left" valign="middle" >  
                                       
                                         <asp:Label  ID="lblNombreCompleto" runat="server" Width="321px" ></asp:Label>
                                    </td>
                                    
                                    <td style="width: 84px;" align="right" valign="middle" >
                                        
                                    </td>
                                     
                                </tr>
                                
                                <tr>
                                    <td style="width: 110px; height: 25px;" align="left" valign="middle">
                                        <span>Estado/Año Académico :&nbsp;</span> 
                                    </td>
                                    <td style="width: 321px; height: 25px;" align="left" valign="middle">
                                        <asp:Label  Text="Activo/2010" ID="lblSituacionAnio" runat="server" Width="250px" ></asp:Label>
                                    </td>
                                  
                                </tr>
                                <tr>
                              
                                    <td style="width: 110px; height: 25px;" align="left" valign="middle">                                        
                                        <span> Nivel/SubNivel/Grado/Aula :&nbsp;</span>
                                    </td>
                                    <td style="width: 321px; height: 25px;" align="left" valign="middle">
                                        <asp:Label ID="lblFormENSnGS"  Text=" Junior School / Early Year / Pre - Kinder / Beavers "  runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 84px;" align="right" valign="middle">
                                        
                                    </td>
                                </tr>
                                 <tr>
                                   
                                    <td style="width: 110px; height: 25px;" align="left" valign="middle">                                        
                                        <span>House :&nbsp;</span>
                                    </td>
                                    <td style="width: 321px; height: 25px;" align="left" valign="middle">
                                        <asp:Label  Text="Rojo" ID="lblHouse" runat="server" Width="100px" ></asp:Label>
                                    </td>
                                    
                                </tr>
                            </table>
                        </fieldset>
                        </td >
                    </tr> 
                         <tr >
                           <td style ="width: 150px; height:25px; " align="right"  >
                              </td>    
                        </tr>
                        <tr>
                            <td align="right" style="width: 250px; height:20px;">
                                <asp:ImageButton ID="btnGrabar" runat="server" Width="84" Height="19" 
                                ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png"
                                onmouseover="this.src = '../App_Themes/Imagenes/btnGrabarV2_2.png'" 
                                onmouseout="this.src = '../App_Themes/Imagenes/btnGrabarV2_1.png'" 
                                ToolTip="Grabar"
                                onclick="btnGrabar_click"/>
                             </td>    
                        </tr>
                        <tr>
                            <td align="right" style="width: 250px; height:20px;">
                                 <asp:ImageButton ID="btnCancelar" 
                                runat="server" Width="84" Height="19"
                                ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                ToolTip="Cancelar"
                                onclick="btnCancelar_Click" 
                                CausesValidation="false"/>   </td>
                        </tr>
                        <tr>
                             <td   style ="width: 150px; height:20px;" align="right">
                               
                            </td>
                        </tr>
                        </table>
                    </div>
                    
                    <div class="miEspacio">
                    </div>
                    
                    <div id="miContainerFichaAlumno">
                        <atk:TabContainer ID="TabContainer2" runat="server" Width="850px" ActiveTabIndex="0"
                            AutoPostBack="false" ScrollBars="Vertical">
                            
                            <atk:TabPanel ID="miFichaTab1" runat="server" HeaderText="Tab1">
                                <HeaderTemplate>
                                    Datos Personales
                                </HeaderTemplate>
                                <ContentTemplate>
                                <div id="Bloque_DatosPersonales" runat="server" style="border:0; margin:0;">
                                    <fieldset id="miFichaTab1_1" >                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Codigo :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:Label  Text="20090017" ID="lblCodigo" runat="server" Width="250px" ></asp:Label>
                                                </td>
                                            </tr> 
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Usuario :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:Label  Text="" ID="lblUsuario" runat="server" Width="250px" ></asp:Label>
                                                </td>
                                            </tr> 
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Codigo Educando :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                  <asp:TextBox   ID="tbCodigoEducando" runat="server" CssClass="miTextBox" Width="250px" MaxLength="14"  Height="18px" />
                                                  <asp:Label ID="lblCodigoEducando" runat="server" Width="150px" Visible="False"></asp:Label>
                                              </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Apellido Paterno :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbApellidoPaterno" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />      
                                                    <asp:Label ID="lblApellidoPaterno" runat="server" Width="150px" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Apellido Materno :&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:TextBox ID="tbApellidoMaterno" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />    
                                                   <asp:Label ID="lblApellidoMaterno" runat="server" Width="150px" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Nombre :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbNombre" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />    
                                                    <asp:Label ID="lblNombre" runat="server" Width="150px" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Sexo :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:RadioButtonList ID="rbSexo" runat="server" RepeatDirection="Horizontal">   
                                                        <asp:ListItem Value="2" >Masculino</asp:ListItem>                                                                             
                                                        <asp:ListItem Value="1" Selected="True">Femenino</asp:ListItem> 
                                                    </asp:RadioButtonList> 
                                                    <asp:Label ID="lblSexo" runat="server" Width="150px" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Tipo Documento :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:DropDownList ID="ddlTipoDocumento" runat="server" Width="255px">
                                                   </asp:DropDownList> 
                                                   <asp:Label ID="lblTipoDocumento" runat="server" Width="150px" Visible="False"></asp:Label> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Nro. Documento :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbNumDocumento" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblNumDocumento" runat="server" Width="150px" Visible="False"></asp:Label> 
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                </div>    
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab2" runat="server" HeaderText="Tab2">
                                <HeaderTemplate>
                                    Datos de Nacimiento
                                </HeaderTemplate>
                                <ContentTemplate>  
                                 <div id="Bloque_DatosNacimiento" runat="server" style="border:0; margin:0;">                              
                                    <fieldset id="miFichaTab2_1">                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Nacimiento Registrado :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 610px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbNacRegistrado" runat="server" RepeatDirection="Horizontal">   
                                                        <asp:ListItem Value="0">No</asp:ListItem>                                                                             
                                                        <asp:ListItem Value="1">Si</asp:ListItem> 
                                                    </asp:RadioButtonList> 
                                                    <asp:Label ID="lblNacRegistrado" runat="server" Width="150px" Visible="False"></asp:Label>    
                                                </td>
                                            </tr> 
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Fecha &nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td   valign="middle" align="left" style="width: 100px; height: 25px;">
                                                    <asp:TextBox ID="tbFechaNacimiento" runat="server" CssClass="miTextBoxCalendar" />
                                                    <asp:Label ID="lblFechaNacimiento" runat="server" Width="150px" Visible="False"></asp:Label>    
                                                </td>
                                                <td valign="middle" align="left" style="width: 510px; height: 25px;">
                                                    <asp:ImageButton runat="server" ID="imageBF5" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                                    <atk:CalendarExtender ID="CalendarExtender4" runat="server" 
                                                        TargetControlID="tbFechaNacimiento"
                                                        PopupButtonID="imageBF5" 
                                                        Format="dd/MM/yyyy" 
                                                        CssClass="MyCalendar" />
                                                </td>
                                               
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Pais&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td  colspan="2"  style="width: 610px; height: 25px" align="left"  valign="middle">
                                                    <asp:DropDownList ID="ddlPais" runat="server" Width="180px"  OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>  
                                                    <asp:Label ID="lblPais" runat="server" Width="150px" Visible="False"></asp:Label>   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Departamento&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td  colspan="2"  style="width: 610px; height: 25px" align="left"  valign="middle">
                                                    <asp:DropDownList ID="ddlDepartamento" runat="server" Width="180px"
                                                    OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>  
                                                    <asp:Label ID="lblDepartamento" runat="server" Width="150px" Visible="False"></asp:Label>   
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Provincia&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 610px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlProvincia" runat="server" Width="180px"
                                                    OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>  
                                                    <asp:Label ID="lblProvincia" runat="server" Width="150px" Visible="False"></asp:Label>   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Distrito&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 610px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDistrito" runat="server" Width="180px">
                                                    </asp:DropDownList> 
                                                    <asp:Label ID="lblDistrito" runat="server" Width="150px" Visible="False"></asp:Label>    
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>1era Nacionalidad&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 610px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlNacionalidad1" runat="server" Width="180px">
                                                    </asp:DropDownList>  
                                                    <asp:Label ID="lblNacionalidad1" runat="server" Width="150px" Visible="False"></asp:Label> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>2da Nacionalidad&nbsp;</span>
                                                </td>
                                                <td style="width: 610px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlNacionalidad2" runat="server" Width="180px">
                                                    </asp:DropDownList> 
                                                    <asp:Label ID="lblNacionalidad2" runat="server" Width="150px" Visible="False"></asp:Label>  
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                 </div>   
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab3" runat="server" HeaderText="Tab3">
                                <HeaderTemplate>
                                    Datos Adicionales
                                </HeaderTemplate>
                                <ContentTemplate>
                                <div id="Bloque_OtrosDatos" runat="server" style="border:0; margin:0;">                              
                                    <fieldset id ="Bloque_DatosAdicionales" runat="server"> 
                                     <legend>Otros Datos</legend> 
                                       <table cellpadding="0" cellspacing="0" border="0" width="790">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                                 
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>1era Lengua Materna&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlLenguaMaterna1" runat="server" Width="254px">
                                                    </asp:DropDownList> 
                                                    <asp:Label ID="lblLenguaMaterna1" runat="server" Width="150px" Visible="False"></asp:Label>  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>2da Lengua Materna&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlLenguaMaterna2" runat="server" Width="255px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblLenguaMaterna2" runat="server" Width="150px" Visible="False"></asp:Label>  
                                                </td>
                                            </tr>
                                                    
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Estado Civil&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:DropDownList ID="ddlEstadocivil" runat="server" Width="255px">
                                                   </asp:DropDownList>
                                                   <asp:Label ID="lblEstadocivil" runat="server" Width="150px" Visible="False"></asp:Label>  
                                                </td>
                                            </tr>
                                                                                                                                                                               
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Cantidad de Hermanos&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbCantidadHermanos" runat="server" CssClass="miTextBox" Width="250px" MaxLength="2" Height="18px" />     
                                                    <asp:Label ID="lblCantidadHermanos" runat="server" Width="150px" Visible="False"></asp:Label>  
                                                </td>
                                                     <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbCantidadHermanos"  Enabled="True">
                                                    </atk:FilteredTextBoxExtender>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Posición entre los Hermanos&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbPosicionHermanos" runat="server" CssClass="miTextBox" Width="250px" MaxLength="2" Height="18px" />     
                                                    <asp:Label ID="lblPosicionHermanos" runat="server" Width="150px" Visible="False"></asp:Label>  
                                                </td>
                                                    <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbPosicionHermanos"  Enabled="True">
                                                    </atk:FilteredTextBoxExtender>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Correo electrónico&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbCorreoElectronico" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblCorreoElectronico" runat="server" Width="150px" Visible="False"></asp:Label>  
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Correo electrónico Institucional&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbCorreoElectronicoInstitucional" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblCorreoElectronicoInstitucional" runat="server" Width="150px" Visible="False"></asp:Label>  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Celular&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbCelular" runat="server" CssClass="miTextBox" Width="250px" MaxLength="9" Height="18px" />     
                                                    <asp:Label ID="lblCelular" runat="server" Width="150px" Visible="False"></asp:Label>  
                                                </td>
                                                 <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbCelular"  Enabled="True">
                                                 </atk:FilteredTextBoxExtender>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    
                                    <div class="miEspacio">
                                    </div>
                    
                                     <fieldset id ="Bloque_DatosReligiosos" runat="server" >  
                                      <legend>Datos Religiosos</legend>                                     
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                          <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>¿Profesa alguna Religión?&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:RadioButtonList ID="rbReligion" runat="server"     RepeatDirection="Horizontal" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rbReligion_SelectedIndexChanged" > 
                                                    <asp:ListItem Value="0">No</asp:ListItem>                                                                             
                                                    <asp:ListItem Value="1">Si</asp:ListItem> 
                                                    </asp:RadioButtonList>
                                                    <asp:Label ID="lblProfesaAlgunaReligion" runat="server" Width="150px" Visible="False"></asp:Label>  
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Religión que profesa :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle" > 
                                                    <asp:DropDownList ID="ddlReligion" runat="server" Width="180px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged">
                                                    </asp:DropDownList> 
                                                    <asp:Label ID="lblReligionProfesa" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>¿Ha sido bautizado? :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbBautizo" runat="server" RepeatDirection="Horizontal">   
                                                    <asp:ListItem Value="0">No</asp:ListItem>                                                                             
                                                    <asp:ListItem Value="1">Si</asp:ListItem> 
                                                    </asp:RadioButtonList>
                                                    <asp:Label ID="lblBautizo" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Lugar:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbLugarBautizo" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />      
                                                    <asp:Label ID="lblLugarBautizo" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                                <td style="width: 70px; height: 25px" align="center">
                                                    <span>Año:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left"  valign="middle">
                                                     <asp:TextBox ID="tbAnioBautizo" runat="server" CssClass="miTextBox" Width="80px" MaxLength="4" Height="18px" />            
                                                     <asp:Label ID="lblAnioBautizo" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                     <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbAnioBautizo" Enabled="True">
                                                    </atk:FilteredTextBoxExtender>             
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>¿Ha dado la primera comunión? :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbPriComunion" runat="server" RepeatDirection="Horizontal">   
                                                    <asp:ListItem Value="0">No</asp:ListItem>                                                                             
                                                    <asp:ListItem Value="1">Si</asp:ListItem> 
                                                    </asp:RadioButtonList>
                                                    <asp:Label ID="lblPriComunion" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Lugar:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbLugarPriComunion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />      
                                                    <asp:Label ID="lblLugarPriComunion" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                                <td style="width: 70px; height: 25px" align="center">
                                                    <span>Año:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left"  valign="middle">
                                                  <asp:TextBox ID="tbAnioPriComunion" runat="server" CssClass="miTextBox" Width="80px" MaxLength="4" Height="18px" />            
                                                  <asp:Label ID="lblAnioPriComunion" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                     <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbAnioPriComunion" Enabled="True">
                                                    </atk:FilteredTextBoxExtender>      
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>¿Se ha confirmado? :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbConfirmado" runat="server" RepeatDirection="Horizontal">   
                                                    <asp:ListItem Value="0">No</asp:ListItem>                                                                             
                                                    <asp:ListItem Value="1">Si</asp:ListItem> </asp:RadioButtonList>
                                                    <asp:Label ID="lblConfirmado" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Lugar:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbLugarConfirmado" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />      
                                                    <asp:Label ID="lblLugarConfirmado" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                                <td style="width: 70px; height: 25px" align="center">
                                                    <span>Año:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left"  valign="middle">
                                                    <asp:TextBox ID="tbAnioConfirmado" runat="server" CssClass="miTextBox" Width="80px" MaxLength="4" Height="18px" />            
                                                     <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbAnioConfirmado" Enabled="True">
                                                    </atk:FilteredTextBoxExtender>
                                                    <asp:Label ID="lblAnioConfirmado" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                            
                                        </table>
                                    </fieldset>
                                    
                                     <div class="miEspacio">
                                    </div>
                    
                                     <fieldset id ="Bloque_DatosRetiro" runat="server" >  
                                      <legend>Datos Retiro</legend>                                
                                           <table cellpadding="0" cellspacing="0" border="0" width="590">
                                                       
                                                        <tr>
                                                            <td colspan="2" height="10px">
                                                                <atk:ModalPopupExtender ID="pnRetiro" 
                                                                                            runat="server" 
                                                                                            TargetControlID="btnMostrarRetiro"
                                                                                            PopupControlID="pnl_PopUp_Retiro"
                                                                                            BackgroundCssClass="MiModalBackground" 
                                                                                            Drag="True" 
                                                                                            OkControlID="OKRetiro" 
                                                                                            CancelControlID="CancelRetiro"
                                                                                            DynamicServicePath="" Enabled="True"
                                                                                            PopupDragHandleControlID="RetiroHeader"
                                                                                            ></atk:ModalPopupExtender>
                                                                
                                                                <asp:Panel ID="pnl_PopUp_Retiro" BackColor="White" BorderColor="Black" runat="server">
                                                                         <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                                                            <tr>
                                                                                                <td style="width: 360px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                                                                    <span id="RetiroHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial">
                                                                                                    Agregar Registro</span>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr><td colspan="2" height="10px"></td></tr>
                                                                                            <tr>
                                                                                                   <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Año:&nbsp;</span>
                                                                                                    <asp:HiddenField ID="hidencodigoRetiro" runat="server" />
                                                                                                </td>
                                                                                               <td style="width: 230px; height: 25px" align="left">
                                                                                                    <asp:DropDownList ID="ddlAnioRetiro" runat="server" Width="200px">
                                                                                                    </asp:DropDownList>
                                                                                                </td> 
                                                                                             </tr>
                                                                                            <tr>
                                                                                                <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Fecha :&nbsp;</span>
                                                                                                </td>
                                                                                                  <td style="width: 230px; height: 25px" align="left">
                                                                                                    <asp:TextBox ID="tbFechaRegistroRetiro" runat="server" 
                                                                                                        CssClass="miTextBoxCalendar" Height="18px" />    
                                                                                                     <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                                                                        TargetControlID="tbFechaRegistroRetiro"
                                                                                                        UserDateFormat="DayMonthYear"                                                                    
                                                                                                        Mask="99/99/9999" 
                                                                                                        MaskType="Date" 
                                                                                                        PromptCharacter="-" CultureAMPMPlaceholder="" 
                                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                                                                    </atk:MaskedEditExtender>
                                                                                                    <asp:ImageButton runat="server" ID="imageFrIni1" 
                                                                                                        ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de Registro de Retiro." />
                                                                                                    <atk:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                                                                        TargetControlID="tbFechaRegistroRetiro"
                                                                                                        PopupButtonID="imageFrIni1" 
                                                                                                        Format="dd/MM/yyyy" 
                                                                                                        CssClass="MyCalendar" Enabled="True" />
                                                                                                </td>      
                                                                                                
                                                                                            </tr> 
                                                                                             <tr>
                                                                                                   <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Motivo:&nbsp;</span>
                                                                                                </td>
                                                                                               <td style="width: 230px; height: 25px" align="left">
                                                                                                    <asp:DropDownList ID="ddlMotivoRetiro" runat="server" Width="200px">
                                                                                                    </asp:DropDownList>
                                                                                                </td> 
                                                                                             </tr>
                                                                                             <tr>
                                                                                                   <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Colegio Traslado:&nbsp;</span>
                                                                                                </td>
                                                                                               <td style="width: 230px; height: 25px" align="left">
                                                                                                    <asp:DropDownList ID="ddlColegioTraslado" runat="server" Width="200px">
                                                                                                    </asp:DropDownList>
                                                                                                </td> 
                                                                                             </tr>
                                                                                             <tr>
                                                                                                   <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Codigo Modular:&nbsp;</span>
                                                                                                </td>
                                                                                               <td style="width: 230px; height: 25px" align="left">
                                                                                                    <asp:TextBox ID="tbCodigoModular" runat="server" CssClass="miTextBox" 
                                                                                                            Width="100px" MaxLength="7" Height="18px" />
                                                                                                </td> 
                                                                                             </tr>
                                                                                              <tr>
                                                                                                   <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Observaciones:&nbsp;</span>
                                                                                                </td>
                                                                                               <td style="width: 230px; height: 25px" align="left">
                                                                                                    <asp:TextBox ID="tbObservacionRetiro" runat="server" CssClass="miTextBox" 
                                                                                                              Rows="4" TextMode="MultiLine"  Width="200px" MaxLength="300"  Height="60px" />
                                                                                                </td> 
                                                                                             </tr>
                                                                                             <tr>
                                                                                                <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                                                                    <asp:ImageButton ID="popup_btnAgregar_Retiro" runat="server" Width="84px" Height="19px"
                                                                                                        ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                                                                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                                                                                                        OnClick="popup_btnAgregar_Retiro_Click"
                                                                                                        ToolTip="Aceptar" />&nbsp;
                                                                                                    <asp:ImageButton ID="popup_btnCancelar_Retiro" runat="server" Width="84px" Height="19px"
                                                                                                        ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                                                                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                                                                                        OnClick="popup_btnCancelar_Retiro_Click"
                                                                                                        ToolTip="Cancelar" />
                                                                                                </td>
                                                                                            </tr>      
                                                                                            <tr><td colspan="2" height="10px"></td></tr>     
                                                                                                                                       
                                                                                                 </table>  
                                                                                                 <div id="controlRetiro" style="display:none">
                                                                                            <input type="button" id="OKRetiro" />
                                                                                            <input type="button" id="CancelRetiro" />
                                                                                        </div>
                                                                          </asp:Panel>
                                                            </td> 
                                                        </tr> 
                                                        
                                                        <tr>
                                                            <td style="width: 590px;" align="center" valign="top" >
                                                    
                                                                   <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                  <tr>
                                                                    <td  style=" width:110px; height: 26px; text-align: right; color: White;
                                                                        font-size: 10px;  " align="center" class="miGVBusquedaFicha_Header">
                                                                        Año 
                                                                    </td>
                                                                    <td style="width: 150px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                        Fecha de registro
                                                                    </td>
                                                                    <td style="width: 170px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        Motivo 
                                                                    </td>
                                                                     <td style="width: 150px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        Observación
                                                                    </td>
                                                                    <td style="width: 180px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        Colegio Procedencia
                                                                    </td>
                                                                    <td style="width: 100px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        Código Modular
                                                                    </td>
                                                                     <td style="width: 30px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        <asp:ImageButton ID="btn_Add_Retiro" runat="server" Width="20px" Height="20px"
                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" 
                                                                            OnClick="btn_Add_Retiro_Click"      
                                                                            ToolTip="Agregar" />
                                                                    </td>
                                                                </tr>
                                                                  <tr>
                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="7">
                                                                        <asp:UpdatePanel ID="upDatosRetiro" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <div id="miGridviewMantDetalle_Alumno">
                                                                                     <asp:GridView ID="gvDetalleRetiro" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleRetiro_RowDataBound"
                                                                                                                        ShowHeader="False" ShowFooter="False"
                                                                                                                        OnRowCommand="gvDetalleRetiro_RowCommand"> 
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        Visible="true" CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelacion") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        Visible="true" CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoMotivo">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoMotivo_grilla" runat="server" Text='<%# Bind("CodigoMotivo") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoAnio">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoAnio_grilla" runat="server" Text='<%# Bind("CodigoAnio") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                             <asp:TemplateField HeaderText="CodigoColegioTraslado">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoColegioTraslado_grilla" runat="server" Text='<%# Bind("CodigoColegioTraslado") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblAnio_grilla" runat="server" Text='<%# Bind("Anio") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="50px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaRegistroRetiro_grilla" runat="server" Text='<%# Bind("FechaRegistroRetiro") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="100px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                             <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblMotivoRetiro_grilla" runat="server" Text='<%# Bind("Motivo") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="150px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblObservacion_grilla" runat="server" Text='<%# Bind("Observacion") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="130px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblColegio_grilla" runat="server" Text='<%# Bind("Colegio") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="150px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label  ID="lblCodigoModular_grilla" runat="server" Text='<%# Bind("CodigoModular") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="120px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                           </table> 
                                                               
                                                                    <div style="display:none">
                                                                     <asp:Button ID="btnmostrarRetiro" runat="server" Text="Button" />      
                                                                    </div>
                                                    
                                                            </td>
                                                        </tr>                                                                   
                                                      
                                                    </table>
                                    </fieldset>
                                    
                                     <div class="miEspacio">
                                    </div>
                    
                                     <fieldset id ="Bloque_DatosProcedencia" runat="server" >  
                                      <legend>Datos Procedencia</legend>                                
                                           <table cellpadding="0" cellspacing="0" border="0" width="590">
                                                       
                                                        <tr>
                                                            <td colspan="2" height="10px">
                                                                <atk:ModalPopupExtender ID="pnProcedencia" 
                                                                                            runat="server" 
                                                                                            TargetControlID="btnMostrarProcedencia"
                                                                                            PopupControlID="pnl_PopUp_Procedencia"
                                                                                            BackgroundCssClass="MiModalBackground" 
                                                                                            Drag="True" 
                                                                                            OkControlID="OKProcedencia" 
                                                                                            CancelControlID="CancelProcedencia"
                                                                                            DynamicServicePath="" Enabled="True"
                                                                                            PopupDragHandleControlID="ProcedenciaHeader"
                                                                                            ></atk:ModalPopupExtender>
                                                                
                                                                <asp:Panel ID="pnl_PopUp_Procedencia" BackColor="White" BorderColor="Black" runat="server">
                                                                         <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                                                            <tr>
                                                                                                <td style="width: 360px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                                                                    <span id="ProcedenciaHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial">
                                                                                                    Agregar Registro</span>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr><td colspan="2" height="10px"></td></tr>
                                                                                            <tr>
                                                                                                   <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Año:&nbsp;</span>
                                                                                                    <asp:HiddenField ID="hidencodigoProcedencia" runat="server" />
                                                                                                </td>
                                                                                               <td style="width: 230px; height: 25px" align="left">
                                                                                                    <asp:DropDownList ID="ddlAnioProcedencia" runat="server" Width="200px">
                                                                                                    </asp:DropDownList>
                                                                                                </td> 
                                                                                             </tr>
                                                                                            <tr>
                                                                                                <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Colegio :&nbsp;</span>
                                                                                                </td>
                                                                                                  <td style="width: 230px; height: 25px" align="left">
                                                                                                   <asp:DropDownList ID="ddlColegioProcedencia" runat="server" Width="200px">
                                                                                                    </asp:DropDownList>
                                                                                                </td>      
                                                                                                
                                                                                            </tr> 
                                                                                             <tr>
                                                                                                   <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Codigo Modular:&nbsp;</span>
                                                                                                </td>
                                                                                               <td style="width: 230px; height: 25px" align="left">
                                                                                                    <asp:DropDownList ID="ddlCodigoModular" runat="server" Width="200px">
                                                                                                    </asp:DropDownList>
                                                                                                </td> 
                                                                                             </tr>
                                                                                             <tr>
                                                                                                <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                                                                    <asp:ImageButton ID="popup_btnAgregar_Procedencia" runat="server" Width="84px" Height="19px"
                                                                                                        ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                                                                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                                                                                                        OnClick="popup_btnAgregar_Procedencia_Click"
                                                                                                        ToolTip="Aceptar" />&nbsp;
                                                                                                    <asp:ImageButton ID="popup_btnCancelar_Procedencia" runat="server" Width="84px" Height="19px"
                                                                                                        ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                                                                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                                                                                        OnClick="popup_btnCancelar_Procedencia_Click"
                                                                                                        ToolTip="Cancelar" />
                                                                                                </td>
                                                                                            </tr>      
                                                                                            <tr><td colspan="2" height="10px"></td></tr>     
                                                                                                                                       
                                                                                                 </table>  
                                                                                                 <div id="controlProcedencia" style="display:none">
                                                                                            <input type="button" id="OKProcedencia" />
                                                                                            <input type="button" id="CancelProcedencia" />
                                                                                        </div>
                                                                          </asp:Panel>
                                                            </td> 
                                                        </tr> 
                                                        
                                                        <tr>
                                                            <td style="width: 590px;" align="center" valign="top" >
                                                    
                                                                   <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                  <tr>
                                                                    <td  style=" width:110px; height: 26px; text-align: right; color: White;
                                                                        font-size: 10px;  " align="center" class="miGVBusquedaFicha_Header">
                                                                        Año 
                                                                    </td>
                                                                    <td style="width: 450px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                        align="center" class="miGVBusquedaFicha_Header">
                                                                        Colegio
                                                                    </td>
                                                                    <td style="width: 200px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        Codigo Modular 
                                                                    </td>
                                                                     <td style="width: 30px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        <asp:ImageButton ID="btn_Add_Procedencia" runat="server" Width="20px" Height="20px"
                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" 
                                                                            OnClick="btn_Add_Procedencia_Click"      
                                                                            ToolTip="Agregar" />
                                                                    </td>
                                                                </tr>
                                                                  <tr>
                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="7">
                                                                        <asp:UpdatePanel ID="upDatosProcedencia" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <div id="miGridviewMantDetalle_Alumno">
                                                                                     <asp:GridView ID="gvDetalleProcedencia" runat="server" CssClass="miGVBusquedaFicha"
                                                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleProcedencia_RowDataBound"
                                                                                                                        ShowHeader="False" ShowFooter="False"
                                                                                                                        OnRowCommand="gvDetalleProcedencia_RowCommand"
                                                                                                                        > 
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        Visible="true" CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelacion") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        Visible="true" CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="CodigoAnio">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoAnioProcedencia_grilla" runat="server" Text='<%# Bind("CodigoAnio") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                             <asp:TemplateField HeaderText="CodigoAnio">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoColegioProcedencia_grilla" runat="server" Text='<%# Bind("CodigoColegioProcedencia") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblAnioProcedencia_grilla" runat="server" Text='<%# Bind("Anio") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="50px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblColegioProcedencia_grilla" runat="server" Text='<%# Bind("ColegioProcedencia") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="400px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                             <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblCodigoModularProcedencia_grilla" runat="server"  />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="250px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <%--Text='<%# Bind("ColegioProcedencia") %>'--%>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                           </table> 
                                                               
                                                                    <div style="display:none">
                                                                     <asp:Button ID="btnmostrarProcedencia" runat="server" Text="Button" />      
                                                                    </div>
                                                    
                                                            </td>
                                                        </tr>                                                                   
                                                      
                                                    </table>
                                    </fieldset>
                                                                         
    <div class="miEspacio">
    </div>
                                    
    <fieldset id ="Fieldset1" runat="server" >  
    <legend>Datos Matrícula</legend>
    <table cellpadding="0" cellspacing="0" border="0" width="590px">
    <tr>
        <td style="width: 340px;" align="left" valign="top">
<table cellpadding="0" cellspacing="0" border="0" width="410px">
<tr>
    <td  style=" width:70px; height: 26px; text-align: center; color: White; font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
        Año</td>
    <td  style=" width:70px; height: 26px; text-align: center; color: White; font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
        Fecha</td>
    <td style="width: 70px; height: 26px; text-align: center; color: White; font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
        Oficial</td>
    <td style="width: 100px; height: 26px; text-align: center; color: White; font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
        Grado</td>
    <td style="width: 100px; height: 26px; text-align: center; color: White; font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
        Aula</td>
</tr>                                       
<tr>
    <td style="width: 410px; height: 25px" align="center" valign="top" colspan="5">
    
    <div id="miGridviewMantDetalle_Alumno" style='width: 410px;'>
        <asp:GridView ID="gvDetalleMatriculas" runat="server" CssClass="miGVBusquedaFicha" Width="410px"
            GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
            OnRowDataBound="gvDetalleMatriculas_RowDataBound" ShowHeader="False" ShowFooter="False">
            <Columns>
<asp:TemplateField HeaderText="CodigoAnio" ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lblanio" runat="server" Text='<%# Bind("anio") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Fecha" ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lblFechaMatr" runat="server" Text='<%# Bind("Fecha") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Oficial" ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lbloficial" runat="server" Text='<%# Bind("oficial") %>' />
    </ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField HeaderText="Grado" ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
    <ItemTemplate>
        <asp:Label ID="lblgrado" runat="server" Text='<%# Bind("grado") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Aula" ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
    <ItemTemplate>
        <asp:Label ID="lblaula" runat="server" Text='<%# Bind("aula") %>' />
    </ItemTemplate>
</asp:TemplateField>                                                                                              
                                                                                                                        
            </Columns>
        </asp:GridView>
    </div>    
    
    </td>
</tr>
</table>                                       
                                       
        </td>
    </tr>                              
    </table>                                      
    </fieldset>   
       
                                </div>    
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab4" runat="server" HeaderText="Tab4">
                                <HeaderTemplate>
                                    Datos Familiares
                                </HeaderTemplate>
                                <ContentTemplate>
                                     <div id="Bloque_DatosFamiliares" runat="server" style="border:0; margin:0;">                                
                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                               <tr>
                                                    <td style="width: 250px; height: 26px; text-align:center;  color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Nombre Completo</td>
                                                    <td  style="width: 80px; height: 26px;  text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Parentesco</td>
                                                    <td    style="width: 80px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Usuario</td>
                                                    <td style="width: 80px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Telf. Casa</td>
                                                    <td style="width: 80px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Telf. Celular</td>
                                                   <td style="width: 60px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Apoderado</td>
                                                   <td style="width: 140px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Email</td>
                                                   <td style="width: 20px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                    <%--   <asp:ImageButton ID="btn_Add_Familiar" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" 
                                                                                                            ToolTip="Agregar" />--%>
                                                  </td>
                                                </tr>
                                                            
                                               <tr>  
                                                   <td style="width: 790px; height: 25px" align="center" valign="top" colspan="8">
                                                      <div style="width: 790px;  border: solid 1px #a6a3a3; " >
                                                        <asp:GridView ID="gvFamiliares" runat="server"
                                                            CssClass="miGridviewBusqueda"
                                                            Width="790px" 
                                                            GridLines="None" 
                                                            AutoGenerateColumns="false" 
                                                            AllowPaging="false" 
                                                            AllowSorting="false"
                                                            ShowFooter="false" 
                                                            ShowHeader="false" 
                                                            EmptyDataText=" - No se encontraron resultados - "
                                                            OnRowDataBound="gvFamiliares_RowDataBound">
                                                        
                                                        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                                                        <Columns>  
                                                          <asp:TemplateField HeaderText="CodigoFamiliar">                                                                      
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigoFamiliar" runat="server" Text='<%# Bind("CodigoFamiliar") %>' />
                                                               
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0"/>
                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                        </asp:TemplateField>               
                                                        <asp:TemplateField >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="250px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParentesco" runat="server" Text='<%# Bind("Parentesco") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="80px" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("Usuario") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="80px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTelfCasa" runat="server" Text='<%# Bind("TelfCasa") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="80px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCelular" runat="server" Text='<%# Bind("Celular") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="100px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApoderado" runat="server" Text="" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="60px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'  />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="110px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnVer" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_ver.png" 
                                                                    CommandName="Visualizar"
                                                                    CommandArgument='<%# Bind("CodigoFamiliar") %>' ToolTip="Ver Registro"
                                                                  /> 
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                                        </asp:TemplateField>  
                                                         <asp:BoundField DataField="CodigoFamiliar" HeaderText="CodigoFamiliar" ItemStyle-Width="0" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" />    
                                                        </Columns>
                                                        </asp:GridView>
                                                       </div>
                                                    </td>                                                        
                                               </tr>    
                                               
                                               <tr>
                                                  <td  height="10px">
                                                      <atk:ModalPopupExtender ID="pnl_ModalFichaFamiliares" 
                                                                            runat="server" 
                                                                            TargetControlID="btnMostrarFF"
                                                                            PopupControlID="pnl_PopUp_FF"
                                                                            BackgroundCssClass="MiModalBackground" 
                                                                            DropShadow="True"
                                                                            Drag="True" 
                                                                            OkControlID="OKFF" 
                                                                            CancelControlID="CancelFF"
                                                                            DynamicServicePath="" Enabled="True" 
                                                                            PopupDragHandleControlID="FichaFamiliarHeader" >
                                                    </atk:ModalPopupExtender> 
                                                    
                                                    <asp:Panel ID="pnl_PopUp_FF" BackColor="White" BorderColor="Black" runat="server" style="width: 881px; height: 530px; border: 0;" >  
                                                            <table cellpadding="0" cellspacing="0" border="0" width="881px">
                                                                <tr id="FichaFamiliarHeader" style="cursor: pointer;">
                                                                    <td style="width: 851px; height: 26px"  align="center" class="miGVBusquedaFicha_Header_V2">
                                                                        <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial">
                                                                        </span>
                                                                    </td>
                                                                    <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                                                                        <asp:ImageButton ID="btnCerraFF" runat="server" Width="16" Height="15"
                                                                            ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                                                                            onclick="btnCerraFF_Click" ToolTip="Cerrar Panel"/>
                                                                    </td>
                                                                </tr>
                                                                <tr><td colspan="2" height="10px"></td></tr>
                                                                <tr>
                                                                   <td colspan="2" style="width: 130px; height: 25px" align="left" valign="middle">
                                                                     <iframe id="Iframe1" runat="server" style="width: 881px; height:500px; border: 0;" 
                                                                        frameborder="0">    
                                                                                                            
                                                                      </iframe>
                                                                   </td>
                                                               </tr> 
                                                          </table>     
                                                         <div id="controlFF" style="display:none">
                                                            <input type="button" id="OKFF" />
                                                            <input type="button" id="CancelFF" />
                                                        </div>
                                                         <div style="display:none">
                                                             <asp:Button ID="btnmostrarFF" runat="server" Text="Button" />      
                                                        </div>
                                                   </asp:Panel>
                                                    
                                                 </td>  
                                              </tr>     
                                           </table>
                                    </div>
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab5" runat="server" HeaderText="Tab5">
                                <HeaderTemplate>
                                    Datos de Domicilio
                                </HeaderTemplate>
                                <ContentTemplate>
                                <div id="Bloque_DatosDomicilio" runat="server" style="border:0; margin:0;">                                
                                    <fieldset id="DatosDomicilio" runat="server">                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>                                     
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Departamento&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDomicilioDepartamento" runat="server" Width="255px" 
                                                    OnSelectedIndexChanged="ddlDomicilioDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblDomicilioDepartamento" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Provincia&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDomicilioProvincia" runat="server" Width="255px"
                                                    OnSelectedIndexChanged="ddlDomicilioProvincia_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblDomicilioProvincia" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Distrito&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDomicilioDistrito" runat="server" Width="255px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblDomicilioDistrito" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Urbanización&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioUrbanizacion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblDomicilioUrbanizacion" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Dirección&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioDireccion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                                                    <asp:Label ID="lblDomicilioDireccion" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Referencia domiciliaria&nbsp;</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioReferencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblDomicilioReferencia" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Teléfono&nbsp;</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioTelefono" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblDomicilioTelefono" runat="server" Width="150px" Visible="False"></asp:Label>     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>¿Tiene acceso a internet?&nbsp;</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbDomicilioAccesoInternet" runat="server" RepeatDirection="Horizontal"> 
                                                        <asp:ListItem Value="0">No</asp:ListItem>       
                                                        <asp:ListItem Value="1">Si</asp:ListItem>                                                                                                                                                       
                                                    </asp:RadioButtonList>  
                                                    <asp:Label ID="lblDomicilioAccesoInternet" runat="server" Width="150px" Visible="False"></asp:Label>       
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                </div>    
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab6" runat="server" HeaderText="Tab6">
                                <HeaderTemplate>
                                   Datos Médicos
                                </HeaderTemplate>
                                <ContentTemplate>
                                <div id="Bloque_DatosMedico" runat="server" style="border:0; margin:0;">                                
                                     <fieldset id="Bloque_DatosSeguroMedico" runat="server" >
                                            <legend >Datos de Seguro Médico</legend>
                                                 <table cellpadding="0" cellspacing="0" border="0" width="590">
                                                   
                                                    <tr>
                                                        <td  height="10px">
                                                        </td> 
                                                    </tr> 
                                                    
                                                    <tr>
                                                        <td style="width: 590px;" align="center" valign="top" >
                                                
                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                  <tr>
                                                                    <td  style="width:100px; height: 26px; text-align: center; color: White;
                                                                        font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                        Año Matrícula
                                                                    </td>
                                                                    <td style="width: 100px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        Tipo 
                                                                    </td>
                                                                    <td style="width: 140px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        compañia
                                                                    </td>
                                                                     <td style="width: 100px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        Numero Poliza
                                                                    </td>
                                                                     <td style="width: 200px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                       Clínicas
                                                                    </td>
                                                                      <td style="width: 150px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                       Vigencia
                                                                    </td>
                                                                </tr>
                                                                  <tr>
                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="6">
                                                                        <asp:UpdatePanel ID="upDatosSeguro" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <div id="miGVMantFichaRegitros">
                                                                                    <asp:GridView ID="gvDetalleDatosSeguro" runat="server" CssClass="miGVBusquedaFicha"
                                                                                        GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                        <Columns>                                                                                            
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
                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="100px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>' />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="100px" />
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
                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="210px" />
                                                                                            </asp:TemplateField>
                                                                                             <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblVigenciaTime" runat="server" Text='<%# Bind("VigenciaTime") %>' />
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
                                                        </td>
                                                    </tr>                                                                   
                                                  
                                                </table>
                                                
                                     </fieldset>
                                  
                                 <div class="miEspacio">
                                 </div>
                                  
                                     <fieldset id="Bloque_CasoEmergencia" runat="server">
                                         
                                                    <legend>Aviso en caso de emergencia y no poder ubicar a familiares registrados</legend>
                                                         <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                <tr>
                                                                    <td colspan="3" style="height: 15px;" align="right">
                                                                        <em>Campos Obligatorios (*)</em>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Nombre Completo :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbNombreCompletoEmergencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />      
                                                    <asp:Label ID="lblNombreCompletoEmergencia" runat="server" Width="150px" Visible="False"></asp:Label>       
                                                </td>
                                            </tr>
                                                                                        
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Telefono Casa :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTelfCasaEmergencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />    
                                                    <asp:Label ID="lblTelfCasaEmergencia" runat="server" Width="150px" Visible="False"></asp:Label>       
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Telefono Oficina :&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTelfOficinaEmergencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />    
                                                    <asp:Label ID="lblTelfOficinaEmergencia" runat="server" Width="150px" Visible="False"></asp:Label>       
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Telefono Móvil :&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTelfMovilEmergencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />    
                                                    <asp:Label ID="lblTelfMovilEmergencia" runat="server" Width="150px" Visible="False"></asp:Label>       
                                                </td>
                                            </tr>
                                          </table>   
                                       </fieldset>
                                     
                                 <div class="miEspacio">
                                 </div>
                                       
                                       <fieldset id="Bloque_DatosEspeciales" runat="server">
                                             <legend >Datos Especiales</legend>
                                                    
                                                     <table cellpadding="0" cellspacing="0" border="0" width="590">
                                                      
                                                        <tr>
                                                            <td style="width: 590px;" align="center" valign="top" colspan="8">
                                                    
                                                                <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                     <tr>
                                                                        <td colspan="3" style="height: 10px;" align="right">
                                                                            
                                                                            </td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td style="width: 180px; height: 25px" align="left">
                                                                            <span>Experiencias Traumaticas&nbsp;</span>
                                                                        </td>
                                                                        <td  colspan ="2" style="width: 610px; height: 25px" align="left" valign="middle">
                                                                            <asp:TextBox ID="tbExperienciasTraumaticas" runat="server" CssClass="miTextBoxMultiLine" Width="360px" 
                                                                                Height="35px" Rows="3" TextMode="MultiLine" />
                                                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                                            TargetControlID="tbExperienciasTraumaticas" 
                                                                            ValidChars="' ','-','.','á','é','í','ó','ú','(',')','Á','É','Í','Ó','Ú'" 
                                                                            Enabled="True">
                                                                            </atk:FilteredTextBoxExtender>
                                                                            <asp:Label ID="lblExperienciasTraumaticas" runat="server" Width="150px" Visible="False"></asp:Label>       
                                                                        </td>
                                                                    </tr>  
                                                                    
                                                                        
                                                               </table> 
                                                               
                                                                <div style="display:none">
                                                                 <asp:Button ID="Button3" runat="server" Text="Button" />      
                                                                </div>
                                                    
                                                            </td>
                                                        </tr>                                                                   
                                                      
                                                    </table>
                                                    
                                      
                                 
                                  <div class="miEspacio">
                                 </div>
                                       
                                       
                                                   <table cellpadding="0" cellspacing="0" border="0" width="590">
                                                       
                                                        <tr>
                                                            <td colspan="2" height="10px">
                                                                <atk:ModalPopupExtender ID="pnDiscapacidad" 
                                                                                            runat="server" 
                                                                                            TargetControlID="btnMostrarDiscapacidad"
                                                                                            PopupControlID="pnl_PopUp_Discapacidad"
                                                                                            BackgroundCssClass="MiModalBackground" 
                                                                                            DropShadow="True"
                                                                                            Drag="True" 
                                                                                            OkControlID="OKDiscapacidad" 
                                                                                            CancelControlID="CancelDiscapacidad"
                                                                                            DynamicServicePath="" Enabled="True"
                                                                                            ></atk:ModalPopupExtender>
                                                                
                                                                <asp:Panel ID="pnl_PopUp_Discapacidad" BackColor="White" BorderColor="Black" runat="server">
                                                                         <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                                                            <tr>
                                                                                                <td style="width: 360px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                                                                    <span id="DiscapacidadHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial">
                                                                                                    Agregar Discapacidad</span>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr><td colspan="2" height="10px"></td></tr>
                                                                                            <tr>
                                                                                                   <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Tipo de Discapacidad :&nbsp;</span>
                                                                                                    <asp:HiddenField ID="hidencodigoDiscapacidad" runat="server" />
                                                                                                </td>
                                                                                               <td style="width: 230px; height: 25px" align="left">
                                                                                                    <asp:DropDownList ID="ddlTipoDiscapacidad" runat="server" Width="200px">
                                                                                                    </asp:DropDownList>
                                                                                                </td> 
                                                                                             </tr>
                                                                                            <tr>
                                                                                                <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                    <span style="padding-left:10px">Descripcion :&nbsp;</span>
                                                                                                </td>
                                                                                                  <td style="width: 230px; height: 25px" align="left">
                                                                                                     <asp:TextBox ID="tbDescipcionDiscapacidad" runat="server" CssClass="miTextBoxMultiLine" Width="200px" 
                                                                                                        Height="35px" Rows="3" TextMode="MultiLine" />
                                                                                                    <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                                                                    TargetControlID="tbDescipcionDiscapacidad" 
                                                                                                    ValidChars="' ','-','.','á','é','í','ó','ú','(',')','Á','É','Í','Ó','Ú'" 
                                                                                                    Enabled="True">
                                                                                                    </atk:FilteredTextBoxExtender>
                                                                                                </td>      
                                                                                                
                                                                                            </tr> 
                                                                                             <tr>
                                                                                                <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                                                                    <asp:ImageButton ID="popup_btnAgregar_Discapacidad" runat="server" Width="84px" Height="19px"
                                                                                                        ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                                                                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                                                                                                        OnClick="popup_btnAgregar_Discapacidad_Click"
                                                                                                        ToolTip="Aceptar" />&nbsp;
                                                                                                    <asp:ImageButton ID="popup_btnCancelar_Discapacidad" runat="server" Width="84px" Height="19px"
                                                                                                        ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                                                                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                                                                                        OnClick="popup_btnCancelar_Discapacidad_Click"
                                                                                                        ToolTip="Cancelar" />
                                                                                                </td>
                                                                                            </tr>      
                                                                                            <tr><td colspan="2" height="10px"></td></tr>     
                                                                                                                                       
                                                                                                 </table>  
                                                                                                 <div id="controlDiscapacidad" style="display:none">
                                                                                            <input type="button" id="OKDiscapacidad" />
                                                                                            <input type="button" id="CancelDiscapacidad" />
                                                                                        </div>
                                                                          </asp:Panel>
                                                            </td> 
                                                        </tr> 
                                                        
                                                        <tr>
                                                            <td style="width: 590px;" align="center" valign="top" colspan="8">
                                                    
                                                                <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                    <tr>
                                                                    <td colspan="2" style="width: 300px; height: 26px; text-align:center;  color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                       Tipo de Discapacidad</td>
                                                                    <td  style="width: 150px; height: 26px;  text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                        Descripcion</td>
                                                                                                                                                                                                            
                                                                    <td style="width: 30px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                    <asp:ImageButton ID="btn_Add_Discapacidad" runat="server" Width="20px" Height="20px"
                                                                                ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
                                                                                OnClick="btn_Add_Discapacidad_Click"                                                    
                                                                                ToolTip="Agregar"/> 
                                                                    </td>
                                                                  
                                                                </tr>
                                                                
                                                                    <tr>  
                                                               <td style="width: 800px; height: 25px" align="center" valign="top" colspan="5">
                                                               
                                                                <asp:UpdatePanel ID="upDiscapacidad" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>                                                                                 
                                                                                <div id="miGridviewMantDetalle_Alumno">
                                                                                    <asp:GridView ID="gvDetalleDiscapacidad" runat="server" 
                                                                                        CssClass="miGridviewBusquedaDetalle_Alumno" 
                                                                                        GridLines="None" 
                                                                                        AutoGenerateColumns="False"
                                                                                        AllowPaging="False" 
                                                                                        AllowSorting="False"
                                                                                        EmptyDataText=" - No se encontraron resultados - "
                                                                                        OnRowDataBound="gvDetalleDiscapacidad_RowDataBound"
                                                                                        OnRowCommand="gvDetalleDiscapacidad_RowCommand"
                                                                                        ShowHeader="False"
                                                                                        ShowFooter="False"
                                                                                        >
                                                                                        <Columns>     
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelTipoDiscapAlum") %>' ToolTip="Actualizar Registro" />
                                                                                                 </ItemTemplate>
                                                                                               
                                                                                                <ItemStyle CssClass="miGridviewBusquedaDetalle_Rows_Alumno" HorizontalAlign="Center" Width="30px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelTipoDiscapAlum") %>' ToolTip="Eliminar Registro" />
                                                                                                </ItemTemplate>
                                                                                               
                                                                                                <ItemStyle CssClass="miGridviewBusquedaDetalle_Rows_Alumno" HorizontalAlign="Center" Width="30px" />
                                                                                            </asp:TemplateField>
                                                                                           
                                                                                                    <asp:TemplateField HeaderText="CodigoDiscapacidad">                                                                      
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCodigoDiscapacidad" runat="server" Text='<%# Bind("CodigoTipoDiscapacidad") %>' />
                                                                                                           
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0"/>
                                                                                                        <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                                                    </asp:TemplateField>
                                                                                                      
                                                                                                    <asp:TemplateField >                                     
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblDiscapacidad" runat="server" Text='<%# Bind("TipoDiscapacidad") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle CssClass="miGridviewBusquedaDetalle_Rows_Alumno"  Width="180" />
                                                                                                </asp:TemplateField>                                                                         
                                                                                                                   
                                                                                                <asp:TemplateField >                                                                                                         
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblDescripcionDiscapacidad" runat="server" Text='<%# Bind("DescripcionDiscapacidad") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle CssClass="miGridviewBusquedaDetalle_Rows_Alumno"  Width="180px" />
                                                                                                </asp:TemplateField>                                           
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                              </div> 
 
                                                               </ContentTemplate>
                                                                </asp:UpdatePanel>   
                                                                
                                                                    </td>                                                        
                                                                </tr>          
                                                               </table> 
                                                               
                                                                    <div style="display:none">
                                                                     <asp:Button ID="btnmostrarDiscapacidad" runat="server" Text="Button" />      
                                                                    </div>
                                                    
                                                            </td>
                                                        </tr>                                                                   
                                                      
                                                    </table>
                                                    
                                       </fieldset>
                                </div>  
                                </ContentTemplate>
                            </atk:TabPanel>  
                              
                            <atk:TabPanel ID="miFichaTab7" runat="server" HeaderText="Tab6">
                                <HeaderTemplate>
                                   Datos de Facturación
                                </HeaderTemplate> 
                                <ContentTemplate>
                                
                            
                                       <fieldset id="Bloque_DatosFacturacion" runat="server">
                                             <legend>Datos Facturación</legend>
                                                    
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Emitir factura :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbEmitirFactura" runat="server" RepeatDirection="Horizontal">   
                                                        <asp:ListItem Value="1">Si</asp:ListItem>                                                                             
                                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem> 
                                                    </asp:RadioButtonList> 
                                                    <asp:Label ID="lblEmitirFactura" runat="server" Width="150px" Visible="False"></asp:Label>       
                                                </td>
                                            </tr>
                                                                                        
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Empresa :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 520px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:DropDownList ID="ddlEmpresa" runat="server" style="width: 450px; font-size: 8pt; font-family: Arial;">
                                                   </asp:DropDownList> 
                                                   <asp:Label ID="lblEmpresa" runat="server" Width="150px" Visible="False" />       
                                                </td>
            <td align="left" style="width: 150px; height: 25px">                                                
                <asp:ImageButton ID="popup_btnAgregar_NuevaEmpresa" runat="server" 
                    Width="141px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnRegistrarEmpresa_1.png" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnRegistrarEmpresa_1.png'" 
                    onmouseover="this.src = '../App_Themes/Imagenes/btnRegistrarEmpresa_2.png'" 
                    ToolTip="Registrar Empresa" />
            </td>
                                                
                                            </tr>
                                                                                        
                                          </table>   
                                                                                          
                                                    
                                       </fieldset>                              
                                        
                                        
                                </ContentTemplate>                                
                            </atk:TabPanel>  
                                                          
                        </atk:TabContainer>
                    </div>
                    
                </div>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
            </ContentTemplate> 
    </atk:TabPanel>
        
        
               </atk:TabContainer>
    
               <asp:Panel ID="pnlImpresion" runat="server" BackColor="White" Height="130px" Width="300px"  style="display:none">
                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                             <td style="text-align:right;">
                                <asp:ImageButton ID="btn_SalirImpresion" 
                                                 runat="server"                                                  
                                                 ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/cross_icon_normal.png" />
                             </td>
                        </tr>
                        <tr>
                             <td style="font-family:Arial;font-size:11px;font-weight:bold;padding-left:2px;  ">
                                Selección de Tipo de Reporte
                             </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                             <td style="font-family:Arial;font-size:10px;padding-left:2px;">
                                <asp:RadioButtonList ID="rb_TipoExportacion" runat="server">
                                <asp:ListItem Selected="True" >Ficha de Alumno</asp:ListItem>
                                <asp:ListItem>Ficha Única de Matrícula</asp:ListItem>
                                 <asp:ListItem>Listado de alumnos Retirados</asp:ListItem>
                                </asp:RadioButtonList>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                             <td style="text-align:center; " >
                                 <asp:Button ID="btnImprimir_Excel" runat="server" Text="Exportar" OnClick="btnImprimir_Click" />
                             </td>
                        </tr>
                    </table>
               </asp:Panel>
               
               <atk:ModalPopupExtender ID="ModalPopupExtender1" 
                                        runat="server"
                                        DynamicServicePath="" 
                                        Enabled="True" 
                                        BackgroundCssClass="FondoAplicacion"
                                        DropShadow="True"
                                        PopupControlID="pnlImpresion"                    
                                        TargetControlID="lblAccionExportar"
                                        >
               </atk:ModalPopupExtender>
                                        
               <asp:Label ID="lblAccionExportar" 
                          runat="server" 
                          ForeColor="White" 
                          Text="..">
               </asp:Label>
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

 </div>        

</asp:Content>

