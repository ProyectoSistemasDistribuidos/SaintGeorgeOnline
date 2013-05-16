<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="FichaFamilia.aspx.vb" Inherits="Modulo_Matricula_FichaFamilia" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">

    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }

  .miTextBox{
        border: solid 1px #a6a3a3;
        height: 15px;
    }
    .miTextBox:hover{
        border: solid 1px #7F9DB9;
        height: 15px;
    }    
    .modalBackground{
	    background-color: black;    
	    filter:alpha(opacity=70);
	    opacity:0.7;
    }    
    #panelRegistro span{
        font-size: 11px;
        font-family: Arial;
    }
    #panelRegistro em{
        font-size: 10px;
        font-family: Arial;
        color: #a51515;
        margin-right: 7px;
        padding: 0;
    }   
    #panelRegistro .header{        
        background: #0a0f14 url(/SaintGeorgeOnline/App_Themes/Imagenes/legend_header.gif) repeat-x;
        text-align: left;
        color: black;
        height: 26px;        
        border-bottom: solid 1px black;
    }           
</style>

<script type="text/javascript">

    function abrirPopupFichaFamiliar(url) {

        var urlaux = url;  //+ '?tipo=' + tipo + '&Padre=' + tbPadre;
        window.showModalDialog(urlaux, "#1", "dialogHeight: 500px ; dialogWidth: 910px; center: Yes; help: No; resizable: No; status: No; scroll: Yes;");

        //var modal = $find('ctl00_ContentPlaceHolder1_TabContainer1_miTab2_ModalPopupExtender_IntegrantesFamilia');
        //modal.show();
    }

    function abrirPopupFichaAlumno(url) {

        var urlaux = url;  //+ '?tipo=' + tipo + '&Padre=' + tbPadre;
        window.showModalDialog(urlaux, "#1", "dialogHeight: 500px ; dialogWidth: 910px; center: Yes; help: No; resizable: No; status: No; scroll: Yes;");

        //var modal = $find('ctl00_ContentPlaceHolder1_TabContainer1_miTab2_ModalPopupExtender_IntegrantesFamilia');
        //modal.show();
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="miContainerActualizacion_Ficha">
            <atk:TabContainer ID="TabContainer1" runat="server" Width="670px" ActiveTabIndex="1" AutoPostBack="false" ScrollBars="None"> 
                             
                <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
                    <HeaderTemplate>
                        <asp:Label ID="lbTab1" runat="server" Text="Busqueda" />
                    </HeaderTemplate>
                    <ContentTemplate> 
                            <div style="border: solid 0px blue; width: 650px;">                               
                                
                                <div id="miBusquedaMant">
                                    <fieldset>
                                        <legend>Criterios de busqueda</legend>
                                        <table  cellpadding="0" 
                                                cellspacing="0" 
                                                border="0" 
                                                style="border: solid 0x red;
                                                min-width: 610px;">
                                                <tr>
                                                    <td style="width: 100px; height: 25px;" align="left" valign="bottom">
                                                        <span>Descripción</span>
                                                    </td>
                                                    <td style="width: 410px; height: 25px; padding-left:10px" align="left" valign="bottom">
                                                        <asp:TextBox ID="tbBuscarDescripcion" runat="server" CssClass="miTextBox" Width="400px" MaxLength="100" />
                                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                                    </td>
                                                    <td style="width: 100px; padding-top:6px" align="right" valign="top" 
                                                        rowspan="2">
                                                        
                                                                <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                                    onclick="btnBuscar_Click" ToolTip="Buscar Registros"/><br />
                                                                 
                                                                <asp:ImageButton ID="btnNuevo" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnNuevo_1.png"
                                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnNuevo_2.png'" 
                                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnNuevo_1.png'"
                                                                    onclick="btnNuevo_Click" ToolTip="Limpiar Filtros"/>
                                                            
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100px; height: 25px;" valign="bottom">
                                                        &nbsp;</td>
                                                    <td align="left" style="width: 410px; height: 25px; padding-left:10px" 
                                                        valign="bottom">
                                                        &nbsp; &nbsp;</td>
                                                </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                
                                <div class="miEspacio">
                                </div>
                                <div id="miGridviewMant">
                                    <asp:GridView ID="GridView1" runat="server" 
                                    CssClass="miGridviewBusqueda" 
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
                                    
                                    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" 
                                        ForeColor="White" HorizontalAlign="Center" />
                                    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
                                    <Columns>                            
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                                                    CommandName="Actualizar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Actualizar Registro" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                                                              
                                        <asp:TemplateField HeaderText="Codigo">  
                                            <HeaderTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td style="width:275px;" align="right" valign="middle">Codigo&nbsp;</td>
                                                    <td style="width:265px;" align="left" valign="middle">
                                                    </td>
                                                </tr>
                                            </table>                                    
                                            </HeaderTemplate>                                                                      
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_CodigoFamilia_grilla" runat="server" Text='<%# Bind("Codigo") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Descripción">  
                                            <HeaderTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td style="width:275px;" align="right" valign="middle">Descripción&nbsp;</td>
                                                    <td style="width:265px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting" runat="server" 
                                                        ToolTip="Descendente"    
                                                        ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                        CommandName="Sort" 
                                                        CommandArgument="Descripcion"/></td>
                                                </tr>
                                            </table>                                    
                                            </HeaderTemplate>                                                                      
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Descripcion_grilla" runat="server" Text='<%# Bind("Descripcion") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="540px"/>
                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="540px" />
                                        </asp:TemplateField>
                                                                               
                                    </Columns>
                                    <PagerTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 628px;">
                                            <tr>                                        
                                                <td style="height: 20px; width: 200px;" align="left" valign="middle">
                                                    <span class="miFooterMantLabelLeft">Ir a página   </span>                                         
                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                        CssClass="letranormal" 
                                                        AutoPostBack="true" 
                                                        OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                                    </asp:DropDownList>&nbsp;
                                                    de
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
                                                    <asp:Label ID="lblRegistrosActuales" runat="server" CssClass="miFooterMantLabelRight" />
                                                </td>                                        
                                            </tr>
                                        </table>
                                    </PagerTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                    </ContentTemplate>
                </atk:TabPanel> 
                  
                <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="false">
                    <HeaderTemplate>
                        <asp:Label ID="lbTab2" runat="server" Text="Inserción" />
                    </HeaderTemplate>
                    <ContentTemplate> 
                            <div id="miPaginaFicha">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div id="miContenidoFicha">
                                        
<div id="miCabeceraFicha"> 
    <table cellpadding="0" cellspacing="0" border="0" width="650px" style="margin: 0;">
    <tr>
        <td align="left" valign="middle" colspan="2">
    <fieldset style="width:600px;margin: 0;" id="Fieldset1" runat="server">
    <legend style="width:400px">Datos de la Familia</legend>
    <table cellpadding="0" cellspacing="0" border="0" width="600px">
        <tr>
            <td style="width:100px;" align="left" valign="middle">
                <b>Familia:</b></td>
            <td style="width:500px;" align="left" valign="middle">
                <asp:Label ID="lblNombreFamilia_Cab" runat="server" style="display: none;" />            
                <asp:TextBox ID="tbNombreFamilia_Cab" runat="server" style="width: 300px; font-size: 8pt; font-family: Arial;" />
            </td>
        </tr>
    </table>
    </fieldset>
        </td>
    </tr>
    <tr>
        <td style="width: 505px;" align="left" valign="middle">
            &nbsp;&nbsp;
            <asp:HiddenField ID="hiddenCodigoFamilia" runat="server" />
            <asp:HiddenField ID="hiddenCodigoFamiliar" runat="server" />
            <asp:HiddenField ID="hiddenCodigoAlumno" runat="server" />
        </td>
        <td style="width: 144px;" align="center" valign="middle">
        </td>
    </tr>
                                                    
                                                    <tr>
                                                        <td align="left" valign="middle" colspan="2">
                                                            <fieldset style="width:600px;margin: 0;" id="Fieldset2" runat="server">
                                                                <legend style="width:400px">Integrantes de la Familia</legend>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="580px">
                                                                        <tr>
                                                                            <td style="width: 580px;" align="center" valign="middle">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 540px; margin: 0; padding: 0">
    <tr>
        <td style="height: 26px; width: 60px; text-align:left; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                                         
        </td>
        <td style="height: 26px; width: 150px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
            Familia
        </td>
        <td style="height: 26px; width: 200px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
            Familiar
        </td>  
        <td style="height: 26px; width: 140px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
            Parentesco
        </td>                                                                                         
        <td style="height: 26px; width: 30px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">   
    <asp:ImageButton ID="btn_Add_Integrante" runat="server" Width="20px" Height="20px"
        ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
        OnClick="btn_Add_Integrante_Click"                                                    
        ToolTip="Agregar Integrante Familiar"/>        
        </td>                                                                                                      
    </tr>
    <tr>  
        <td style="width: 580px; height: 25px" align="center" valign="top" colspan="5">                                                                                         
<asp:UpdatePanel ID="UpdatePanel_ListaFamiliares" runat="server" UpdateMode="Conditional">
    <ContentTemplate>                                                                                 
    <div id="miGridviewMant" style="width: 560px; margin: 0; padding: 0">
    <asp:GridView ID="gvListaFamiliares" runat="server" 
        width="560px"
        CssClass="miGVBusquedaFicha" 
        GridLines="None" 
        AutoGenerateColumns="False"
        EmptyDataText=" - No se encontraron resultados - "
        OnRowCommand="gvListaFamiliares_RowCommand"
        OnRowDataBound="gvListaFamiliares_RowDataBound"
        ShowHeader="False">
            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />      
            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />                                                  
        <Columns>     
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" Visible="true" 
                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoIntegranteFamilia") %>' ToolTip="Actualizar Registro" />
                </ItemTemplate>                                                                                           
                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"  Visible="true" 
                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoIntegranteFamilia") %>' ToolTip="Eliminar Registro" />
                </ItemTemplate>                                                                                           
                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>                      
                                                                                                                                                       
            <asp:TemplateField ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">                                                                                                         
                <ItemTemplate>
                    <asp:Label ID="lblDescripcionFamilia" runat="server" Text='<%# Bind("DescripcionFamilia") %>' />
                </ItemTemplate>
            </asp:TemplateField>                                                                                                   
            <asp:TemplateField ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">                                                                                                           
                <ItemTemplate>
                    <asp:Label ID="lblNombreFamiliar" runat="server" Text='<%# Bind("NombreFamiliar") %>' />
                </ItemTemplate>
            </asp:TemplateField>                                                                                            
            <asp:TemplateField ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">                                                                                                         
                <ItemTemplate>
                    <asp:Label ID="lblParentesco" runat="server" Text='<%# Bind("Parentesco") %>' />
                </ItemTemplate>
            </asp:TemplateField>                                                                                  
            
            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                  
                <ItemTemplate>
                    <asp:Label ID="lblIdx" runat="server" Text='<%# Bind("idx") %>' />                                  
                </ItemTemplate>
            </asp:TemplateField>                                                                                                                                                   
            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                      
                <ItemTemplate>
                    <asp:Label ID="lblCodigoIntegranteFamilia" runat="server" Text='<%# Bind("CodigoIntegranteFamilia") %>' />       
                </ItemTemplate>
            </asp:TemplateField>                           
            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                       
                <ItemTemplate>
                    <asp:Label ID="lblCodigoFamilia" runat="server" Text='<%# Bind("CodigoFamilia") %>' />                    
                </ItemTemplate>
            </asp:TemplateField>                                  
            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                   
                <ItemTemplate>
                    <asp:Label ID="lblCodigoFamiliar" runat="server" Text='<%# Bind("CodigoFamiliar") %>' />
                </ItemTemplate>
            </asp:TemplateField>                                    
            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                   
                <ItemTemplate>
                    <asp:Label ID="lblCodigoParentesco" runat="server" Text='<%# Bind("CodigoParentesco") %>' />
                </ItemTemplate>
            </asp:TemplateField>                                   
            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                   
                <ItemTemplate>
                    <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>' />
                </ItemTemplate>
            </asp:TemplateField>                                 
            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                   
                <ItemTemplate>
                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>' />
                </ItemTemplate>
            </asp:TemplateField>                       
                                                                                                                                          
        </Columns>
    </asp:GridView>
    </div>           
    </ContentTemplate>
</asp:UpdatePanel>                                                                                                 
        </td>                                                        
    </tr> 
    <tr><td colspan="5"></td></tr>   
    <tr><td colspan="5"></td></tr>   
    </table> 
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                            </fieldset>

                               </td>
                                                    </tr>                                                 
    <tr>
        <td style="width: 505px;" align="left" valign="middle">
            &nbsp;&nbsp;
        </td>
        <td style="width: 144px;" align="center" valign="middle">
        </td>
    </tr>      
                                                    
                                                     <tr>
                                                        <td align="left" valign="middle" colspan="2">
                                                            <fieldset style="width:600px;margin: 0;" id="Fieldset4" runat="server">
                                                                <legend style="width:400px">Alumnos de la Familia</legend>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="580px">
                                                                        <tr>
                                                                            <td style="width: 580px;" align="center" valign="middle">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 540px; margin: 0; padding: 0">
    <tr>
        <td style="height: 26px; width: 40px; text-align:left; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
        </td>        
        <td style="height: 26px; width: 60px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
            Código
        </td>
        <td style="height: 26px; width: 250px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
            Alumno
        </td>
        <td style="height: 26px; width: 180px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">  
            Responsable de Pago          
        </td>                                                                                 
        <td style="height: 26px; width: 30px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
            <asp:ImageButton ID="btn_Add_Alumno" runat="server" Width="20px" Height="20px"
                ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
                OnClick="btn_Add_Alumno_Click"                                                    
                ToolTip="Agregar Alumno"/> 
        </td>                                                                                                      
    </tr>
    <tr>  
        <td style="width: 580px; height: 25px" align="center" valign="top" colspan="5">                                                                                         
<asp:UpdatePanel ID="UpdatePanel_ListaAlumnos" runat="server" UpdateMode="Conditional">
    <ContentTemplate>                                                                                 
    <div id="miGridviewMant" style="width: 560px; margin: 0; padding: 0">    
    <asp:GridView ID="gvListaAlumnos" runat="server" 
        width="560px"
        CssClass="miGVBusquedaFicha" 
        GridLines="None" 
        AutoGenerateColumns="False"
        OnRowCommand="gvListaAlumnos_RowCommand"
        OnRowDataBound="gvListaAlumnos_RowDataBound"
        EmptyDataText=" - No se encontraron resultados - "
        ShowHeader="False">
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" /> 
        <Columns>  
        
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" Visible="true" 
                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Actualizar Registro" />
                </ItemTemplate>                                                                                           
                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="20px" />
            </asp:TemplateField>
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"  Visible="true" 
                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Eliminar Registro" />
                </ItemTemplate>                                                                                           
                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="20px" />
            </asp:TemplateField>             
                                                                                                                                                              
            <asp:TemplateField ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80px">                                                                                                         
                <ItemTemplate>
                    <asp:Label  ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                </ItemTemplate>
            </asp:TemplateField>  
                                                                                                                                           
            <asp:TemplateField ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px">                                                                                                         
                <ItemTemplate>
                    <asp:Label  ID="lblNombreAlumno" runat="server" Text='<%# Bind("NombreAlumno") %>' />
                </ItemTemplate>
            </asp:TemplateField>   
                                                                                                                                        
            <asp:TemplateField ItemStyle-CssClass="miGVBusquedaFicha_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="240px">                                                                                                         
                <ItemTemplate>
                    <asp:Label  ID="lblNombreFamiliarResponsablePago" runat="server" Text='<%# Bind("NombreFamiliarResponsablePago") %>' />
                </ItemTemplate>
            </asp:TemplateField>   
            
            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                   
                <ItemTemplate>
                    <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>' />
                </ItemTemplate>
            </asp:TemplateField>                                 
            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                   
                <ItemTemplate>
                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>' />
                </ItemTemplate>
            </asp:TemplateField>                               
            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-Width="0" HeaderStyle-Width="0">                                                                   
                <ItemTemplate>
                    <asp:Label ID="lblCodigoFamiliarResponsablePago" runat="server" Text='<%# Bind("CodigoFamiliarResponsablePago") %>' />
                </ItemTemplate>
            </asp:TemplateField>        
                                                                                            
        </Columns>
    </asp:GridView>
    </div>           
    </ContentTemplate>
</asp:UpdatePanel>          
        </td>                                                        
    </tr> 
    <tr><td colspan="5"></td></tr>   
    <tr><td colspan="5"></td></tr>   
    </table>     
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                            </fieldset>
     
                  
                                                        </td>
                                                    </tr>
                                                                                              
                                                </table>
                                            </div>
                                       
<div class="miEspacio" style="text-align:right" >                                                            
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>   
    <asp:Label ID="lblAbrePopUpIntegranteFamilia" runat="server" /> 
    <atk:ModalPopupExtender ID="ModalPopupExtender_IntegrantesFamilia" runat="server"
        Enabled="True" 
        BackgroundCssClass="FondoAplicacion"
        PopupControlID="pnl_PopUp_IntegrantesFamilia"                    
        TargetControlID="lblAbrePopUpIntegranteFamilia">
    </atk:ModalPopupExtender>                                                            
    <asp:Panel ID="pnl_PopUp_IntegrantesFamilia" runat="server" BackColor="White" BorderColor="Black" width="700px">
        <table border="0" cellpadding="0" cellspacing="0" width="700px" style="margin:0; padding: 0;">
        <tr> 
            <td align="left" style="width:30px "  class="miGVBusquedaFicha_Header" >
            <td align="left" class="miGVBusquedaFicha_Header" colspan="3" style="width: 640px; height: 26px">
               <span ID="Span1" style="text-align:center;font-weight:bold; font-size:11px; font-family:Arial">
                Agregar Integrante de la Familia</span>
            </td>
            <td align="left" style="width:30px "  class="miGVBusquedaFicha_Header" >    
        </tr>
        <tr>
            <td colspan="5" height="10px">
            </td>
        </tr>
        <tr>
            <td align="left" style="width:30px " valign="middle" rowspan="5">
            </td>
            <td align="left" style="width:80px " valign="middle">
                <span style="font-size: 8pt; font-family: Arial;">Familia:&nbsp;</span>
            </td>
            <td align="left" style="width: 560px; height: 25px" colspan="2">
                <asp:Label ID="lbl_FamiliaPopUp" runat="server" style="font-size: 9pt; font-family: Arial; font-weight: bold;" />
            </td>
            <td align="left" style="width:30px " valign="middle" rowspan="5">
            </td>
        </tr>
        <tr>
            <td align="left" style="width:80px " valign="middle">
                <span style="font-size: 8pt; font-family: Arial;">Familiar:&nbsp;</span></td>
            <td align="left" style="width: 410px; height: 25px">
                <asp:DropDownList ID="ddl_Familiar_Popup" runat="server" style="width: 400px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
            <td align="left" style="width: 150px; height: 25px">
                <asp:ImageButton ID="popup_btnAgregar_NuevoFamiliar" runat="server" 
                    Width="141px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnRegistrarFamiliar_1.png" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnRegistrarFamiliar_1.png'" 
                    onmouseover="this.src = '../App_Themes/Imagenes/btnRegistrarFamiliar_2.png'" 
                    ToolTip="Registrar Nuevo Familiar" />
            </td>
        </tr>
        <tr>
            <td align="left" style="width:80px " valign="middle">
                <span style="font-size: 8pt; font-family: Arial;">Parentesco:&nbsp;</span>
            </td>
            <td align="left" style="width: 560px; height: 25px" colspan="2">
                <asp:DropDownList ID="ddl_Parentesco_Popup" runat="server" style="width: 150px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td align="left" colspan="3" valign="middle">&nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="width: 700px; height: 25px" valign="middle">                
                <asp:ImageButton ID="popup_btnAgregar_IntegranteFamilia" runat="server" 
                    Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                    OnClick="popup_btnAgregar_IntegranteFamilia_Click" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                    onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'" 
                    ToolTip="Aceptar" />
                &nbsp;
                <asp:ImageButton ID="popup_btnCancelar_IntegranteFamilia" runat="server" 
                    Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                    OnClick="popup_btnCancelar_IntegranteFamilia_Click" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                    ToolTip="Cancelar" />
            </td>
        </tr>
        <tr>
            <td colspan="5" height="10px">
            </td>
        </tr>
        </table>
    </asp:Panel>     
    </ContentTemplate>
</asp:UpdatePanel>                                                              

<asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>                     
    <asp:Label ID="lblAbrePopUpAlumnosFamilia" runat="server" /> 
    <atk:ModalPopupExtender ID="ModalPopupExtender_AlumnosFamilia" runat="server"
        Enabled="True" 
        BackgroundCssClass="FondoAplicacion"
      
        PopupControlID="pnl_PopUp_AlumnosFamilia"                    
        TargetControlID="lblAbrePopUpAlumnosFamilia">
    </atk:ModalPopupExtender>  
    <asp:Panel ID="pnl_PopUp_AlumnosFamilia" runat="server" BackColor="White" BorderColor="Black" width="700px">      
        <table border="0" cellpadding="0" cellspacing="0" width="700px" style="margin:0; padding: 0;">
        <tr> 
            <td align="left" style="width:30px "  class="miGVBusquedaFicha_Header" >
            <td align="left" class="miGVBusquedaFicha_Header" colspan="3" style="width: 640px; height: 26px">
               <span ID="Cab_IntegrantesFamilia" style="text-align:center;font-weight:bold; font-size:11px; font-family:Arial">
                Agregar Alumnos de la Familia</span>
            </td>
            <td align="left" style="width:30px "  class="miGVBusquedaFicha_Header" >    
        </tr>
        <tr>
            <td colspan="5" height="10px">
            </td>
        </tr>
        <tr>
            <td align="left" style="width:30px " valign="middle" rowspan="6">
            </td>
            <td align="left" style="width:80px " valign="middle">
                <span style="font-size: 8pt; font-family: Arial;">Familia:&nbsp;</span>
            </td>
            <td align="left" style="width: 560px; height: 25px" colspan="2">
                <asp:Label ID="lbl_FamiliaPopUp2" runat="server" style="font-size: 9pt; font-family: Arial; font-weight: bold;" />
            </td>
            <td align="left" style="width:30px " valign="middle" rowspan="6">
            </td>
        </tr>
        <tr>
            <td align="left" style="width:80px " valign="middle">
                <span style="font-size: 8pt; font-family: Arial;">Alumno:&nbsp;</span></td>
            <td align="left" style="width: 410px; height: 25px">
                <asp:DropDownList ID="ddl_Alumno_Popup" runat="server" style="width: 400px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
            <td align="left" style="width: 150px; height: 25px">
                <asp:ImageButton ID="popup_btnAgregar_NuevoAlumno" runat="server" 
                    Width="141px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnRegistrarAlumno_1.png" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnRegistrarAlumno_1.png'" 
                    onmouseover="this.src = '../App_Themes/Imagenes/btnRegistrarAlumno_2.png'" 
                    ToolTip="Registrar Nuevo Alumno" />
            </td>
        </tr>
        
        <tr>        
            <td align="left" style="width:80px " valign="middle">
                <span style="font-size: 8pt; font-family: Arial;">Responsable Pago:&nbsp;</span></td>
            <td align="left" style="width: 410px; height: 25px">
                <asp:DropDownList ID="ddl_FamiliarParentesco_Popup" runat="server" style="width: 400px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>           
            </td>
            <td align="left" style="width: 150px; height: 25px"> 
            </td>
        </tr>
        
        <tr>
            <td align="left" colspan="3" valign="middle">&nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="width: 700px; height: 25px" valign="middle">                
                <asp:ImageButton ID="popup_btnAgregar_AlumnoFamilia" runat="server" 
                    Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                    OnClick="popup_btnAgregar_AlumnoFamilia_Click" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                    onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'" 
                    ToolTip="Aceptar" />
                &nbsp;
                <asp:ImageButton ID="popup_btnCancelar_AlumnoFamilia" runat="server" 
                    Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                    OnClick="popup_btnCancelar_AlumnoFamilia_Click" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                    ToolTip="Cancelar" />
            </td>
        </tr>
        <tr>
            <td colspan="5" height="10px">
            </td>
        </tr>
        </table>
    </asp:Panel>       
    </ContentTemplate>  
</asp:UpdatePanel>                            
</div>  
                                            <div id="miFooterDetalleMant">
                                                <asp:ImageButton ID="btnGrabar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                                    onclick="btnGrabar_Click" />&nbsp;
                                                <asp:ImageButton ID="btnCancelar" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                                                    onclick="btnCancelar_Click" CausesValidation="false"/>
                                            </div>   
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
        
    <asp:UpdatePanel ID="UpdatePanel_IngresarFamilia" runat="server">
    <ContentTemplate>      
    <atk:ModalPopupExtender ID="ModalPopupExtender_IngresarFamilia" runat="server"
        PopupControlID="Panel_IngresarFamilia"
        TargetControlID="btnVer_IngresarFamilia"
        OkControlID="btnOK_IngresarFamilia" 
        CancelControlID="btnCancel_IngresarFamilia"
        BackgroundCssClass="modalBackground"
        Drag="True" PopupDragHandleControlID="dragCtrl_IngresarFamilia" Enabled="True" />      
    <asp:panel id="Panel_IngresarFamilia" BackColor="White" BorderColor="Black" BorderWidth="1" runat="server" style="width: 500px; display: none;">
        <table cellpadding="0" cellspacing="0" border="0" id="panelRegistro" style="width: 500px;">          
            <tr>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">                    
                    <span style="width:30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> 
                </td>
                <td style="width: 440px; height: 26px; cursor: pointer;" align="left" valign="middle" class="header" colspan="2" id="dragCtrl_IngresarFamilia">                
                    <span style="font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">
                        <asp:Label ID="lbltitulo" runat="server" text="Registro de Familias" />
                    </span>
                </td>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">
                    <asp:ImageButton ID="btnCerrar_IngresarFamilia" runat="server" Width="16px" Height="15px"
                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        onclick="btnCerrar_IngresarFamilia_Click" ToolTip="Cerrar Panel"/>                                        
                </td>
            </tr>
            <tr><td colspan="4">
                <br />
            </td></tr>  
            <tr>
                <td style="width: 30px;" rowspan="7"></td>  
                <td style="width: 440px; height: 25px;" valign="middle" align="right" colspan="2">
                    <em>Campos Obligatorios (*)</em>
                </td>
                <td style="width: 30px;" rowspan="7"></td>  
            </tr> 
                        
            <tr>
                <td style="width: 80px; height: 25px;" align="left" valign="middle">
                    <span>Anio Ingreso&nbsp;</span><span class="camposObligatorios">(*)</span>
                </td>
                <td style="width: 360px; height: 25px;" align="left" valign="top">
                    <asp:DropDownList ID="ddlAnioAcademicoIngreso" runat="server" Width="100px" style="font-size: 8pt; font-family: Arial;">
                    </asp:DropDownList>    
                </td>
            </tr>              
            
            <tr>
                <td style="width: 80px; height: 25px;" align="left" valign="middle">
                    <span>Familia&nbsp;</span><span class="camposObligatorios">(*)</span>
                </td>
                <td style="width: 360px; height: 25px;" align="left" valign="top">
                    <asp:TextBox ID="tbNombreFamilia" runat="server" CssClass="miTextBox" Width="350px" />
                </td>
            </tr>                 
            <tr><td colspan="2"><br /></td></tr>
            <tr>
                <td colspan="2" align="right" valign="middle"> 
                    <asp:ImageButton ID="btnVerificar_IngresarFamilia" runat="server" Width="94px" Height="19px" 
                            ImageUrl="~/App_Themes/Imagenes/btnVerificar_1.png"
                            onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnVerificar_2.png'" 
                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnVerificar_1.png'" ToolTip="Verificar"
                            onclick="btnVerificar_IngresarFamilia_Click" />&nbsp;
                    <asp:ImageButton ID="btnGrabar_IngresarFamilia" runat="server" Width="84px" Height="19px" 
                            ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png"
                            onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnGrabarV2_2.png'" 
                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnGrabarV2_1.png'" ToolTip="Grabar"
                            onclick="btnGrabar_IngresarFamilia_Click" />&nbsp;
                    <asp:ImageButton ID="btnCancelar_IngresarFamilia" runat="server" Width="84px" Height="19px" 
                            ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                            onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'" 
                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                            onclick="btnCerrar_IngresarFamilia_Click" CausesValidation="False"/>
                </td>
            </tr>   
            <tr><td colspan="2"><br /></td></tr>   
            <tr>
                <td colspan="2" align="center" valign="middle">  

<div id="miGridviewMant" style="width: 440px; height: 26px; margin: 0; padding: 0; border-bottom: 0;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 440px; height: 26px; color:White; background-color: #0a0f14; 
            font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;" class="miGVBusquedaFicha_Header">
        <tr>
            <td style="width:  30px; height: 26px;" align="center" valign="middle">         
            </td>
            <td style="width:  393px; height: 26px;" align="center" valign="middle">
                <span>Nombre de la Familia</span>                                                                 
            </td>
            <td style="width:  17px; height: 26px;" align="center" valign="middle">   
            </td>                      
        </tr>
    </table>      
</div> 

<div id="miGridviewMant" style="overflow-y: scroll; overflow-x: hidden; width: 440px; height: 170px; margin:0; padding:0;">
    <asp:GridView ID="GridView2" runat="server" 
            width="423px"
            CssClass="miGridviewBusqueda" 
            GridLines="None" 
            ShowFooter="false"
            ShowHeader="false"
            AutoGenerateColumns="False"
            AllowPaging="false" 
            AllowSorting="false"
            EmptyDataText=" - No se encontraron resultados - "
            OnRowDataBound="GridView2_RowDataBound"
            OnRowCommand="GridView2_RowCommand">
    
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
    <Columns>         
                       
    <asp:TemplateField>
        <ItemTemplate>
            <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                CommandName="Seleccionar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Seleccionar" />
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" Width="30px" />
        <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
    </asp:TemplateField>
                   
    <asp:TemplateField>                                                                    
        <ItemTemplate>
            <asp:Label ID="lbl_NombreFamilia" runat="server" Text='<%# Bind("Descripcion") %>' />
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" Width="410px"/>
        <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="393px" />
    </asp:TemplateField>  
                                                                              
    <asp:TemplateField HeaderText="Codigo">                                                                 
        <ItemTemplate>
            <asp:Label ID="lbl_CodigoFamilia" runat="server" Text='<%# Bind("Codigo") %>' />
        </ItemTemplate>
        <HeaderStyle CssClass="miHiddenStyle" />
        <ItemStyle CssClass="miHiddenStyle" Width="0px" />   
    </asp:TemplateField>
                                                                             
    </Columns>
    </asp:GridView>
</div>
                
                </td>
            </tr>
            <tr><td colspan="4"><br /></td></tr> 
        </table>  
        <div id="controlIngresarFamilia" style="display:none">
            <input type="button" id="btnVer_IngresarFamilia" runat="server" />
            <input type="button" id="btnOK_IngresarFamilia" />
            <input type="button" id="btnCancel_IngresarFamilia" />
        </div>       
    </asp:panel> 
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

