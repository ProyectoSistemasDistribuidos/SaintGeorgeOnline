<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="RestriccionMatricula.aspx.vb" Inherits="Modulo_Matricula_RestriccionMatricula" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div id="miPaginaMantenimiento" style="margin-left: 10px;">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
    <ContentTemplate>    
    
    <atk:TabContainer ID="TabContainer1" runat="server" Width="900px" ActiveTabIndex="0" AutoPostBack="false" ScrollBars="None" >
        <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Parámetros de Búsqueda" />
            </HeaderTemplate>
            <ContentTemplate>    
            
    <table cellpadding="0" cellspacing="0" border="0" style="width: 882px; border: solid 0px red; margin: 0; padding: 0; font-size: 11px; font-family: Arial;">
       <tr>
            <td style="width: 80px; height: 25px;" align="left" valign="middle">
                <span>Periodo :</span>
            </td>   
            <td style="width: 280px; height: 25px;" align="left" valign="middle">
                <asp:DropDownList ID="ddlPeriodo" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td style="width: 80px; height: 25px;" align="left" valign="middle">
                <span>Buscar Por :</span>
            </td>    
            <td style="width: 280px; height: 25px;" align="left" valign="middle">
                <asp:RadioButtonList ID="rbListFiltro" runat="server" 
                    style="font-size: 8pt; font-family: Arial;" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Text="Nombres" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Codigo" Value="1" Selected="False"></asp:ListItem>                    
                </asp:RadioButtonList>
            </td>
            <td style="width: 522px; height: 25px;" align="left" valign="middle" colspan="2">
            </td>
        </tr>            
        <tr>
            <td style="width: 80px; height: 25px;" align="left" valign="middle">
                <span>Filtro :</span>
            </td>    
            <td style="width: 280px; height: 25px;" align="left" valign="middle">
                <asp:TextBox ID="tbFiltro" runat="server" style="width:270px; font-size: 8pt; font-family: Arial;" />
            </td>            
            <td style="width: 300px; height: 25px; border: solid 1px #FFFFFF" align="left" valign="middle">
                <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" 
                    ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'" 
                    onclick="btnBuscar_Click" 
                    ToolTip="Buscar"/>   
            </td>
            <td style="width: 222px; height: 25px; border: solid 1px #FFFFFF" align="right" valign="middle">    
                <asp:ImageButton ID="btnGrabar" runat="server" Width="74px" Height="19px" 
                    ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" 
                    onclick="btnGrabar_Click" 
                    ToolTip="Grabar"/>                            
            </td>
        </tr>               
        <tr>
            <td style="width: 880px; height: 25px;" align="left" valign="middle" colspan="4">    
    <div id="miGridviewMantActualizacion_Ficha" style="width: 882px; height: 50px; margin: 0; padding: 0; border-bottom: 0;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 882px; height: 50px; color:White; background-color: #0a0f14; 
            font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;"
        <tr>
            <td style="width:  30px; height: 26px;" align="center" valign="middle">
                <span>#</span>                                                                 
            </td>
            <td style="width:  70px; height: 26px;" align="center" valign="middle">
                <span>Código</span>                                                                 
            </td>
            <td style="width:  400px; height: 26px;" align="center" valign="middle">
                <span>Apellidos y Nombres</span>                                                                 
            </td>
            <td style="width:  360px; height: 26px;" align="left" valign="middle">  
                <div style="width:  340px; height: 26px; border: solid 0px red; padding:0 ; margin: 0;"> 
            <table cellpadding="0" cellspacing="0" border="0" style="height: 26px; padding:0 ; margin: 0; display: none;">
            <tr>
    <asp:DataList ID="dl_Documentos" runat="server" RepeatDirection="Horizontal" BorderWidth="0">                                                                                   
    <ItemTemplate> 
        <td style="width:150px; height: 26px; padding:0 ; margin: 0;" align="center" valign="middle">
            <asp:Label ID="lblDescripcion" runat="server" ForeColor="white" Font-Bold="true" Text='<%# Eval("Descripcion") %>' />                                                                             
        </td>                                                                                                                            
    </ItemTemplate>
    </asp:DataList> 
            </tr>
            </table> 
</div>   
            </td>
           <%-- <td style="width:  180px; height: 26px;" align="center" valign="middle">  
                <span>No debe libros del Banco de Libros</span>      
            </td>
            <td style="width:  170px; height: 26px;" align="center" valign="middle">  
                <span>DNI o carnet de Extranjería registrado</span>      
            </td>--%>
            <td style="width:  22px; height: 26px;" align="center" valign="middle">   
            </td>                      
        </tr>
    </table>      
    </div> 
     
    <div id="miGridviewMant" style="overflow-y: scroll; overflow-x: hidden; width: 882px; height: 295px; margin: 0; padding: 0;">          
        <asp:GridView ID="GridView1" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="865px"
            GridLines="None" 
            AutoGenerateColumns="False"
            AllowPaging="False" 
            AllowSorting="False"
            ShowFooter="false"
            ShowHeader="false"
            EmptyDataText=" - No se encontraron resultados - "           
            OnRowDataBound="GridView1_RowDataBound">
            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                                                
            <Columns>     
                
                <asp:TemplateField ItemStyle-Width="30px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField> 
                                 
                <asp:TemplateField ItemStyle-Width="70px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                    </ItemTemplate>
                </asp:TemplateField>                                 
                                 
                <asp:TemplateField ItemStyle-Width="400px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            
                    <asp:TemplateField ItemStyle-Width="620px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:CheckBoxList Width="300px" ID="chk_Documentos" runat="server" RepeatDirection="Horizontal" CssClass="miClaseCheckBox">
                    </asp:CheckBoxList>    
                    </ItemTemplate>
                </asp:TemplateField>  
                
                <%--<asp:TemplateField ItemStyle-Width="320px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDes1" runat="server" Width="100" />
                    </ItemTemplate>
                </asp:TemplateField>  --%>
                   
            </Columns>
        </asp:GridView>
    </div>
            </td>
        </tr>                                 
    </table>                              

            </ContentTemplate> 
        </atk:TabPanel>
    </atk:TabContainer>    
    
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

