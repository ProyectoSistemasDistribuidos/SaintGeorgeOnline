<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ProgramacionDeExamenes.aspx.vb" Inherits="Modulo_Academico_ProgramacionDeExamenes" %>

<%@ OutputCache Location ="None" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css"> 
.mibotonGrid
{
    text-decoration: none; 
    background-color: #2a5e97;
    color: #ffffff; 
    font-weight: bold; font-size: 10px;  
    display: block; line-height: 18px; float: left;
    -webkit-border-radius: 5px;
    -moz-border-radius: 5px;
    border-radius: 5px;   
    padding: 0 10px; 
    margin: 0 5px 0 0;
    /*width: 40px;*/ 
    vertical-align: middle; text-align: center;      
    }
.mibotonGrid:hover
{
    text-decoration: none; 
    background-color: #a1b5cd;
    color: #23527e; 
    /*background: rgba(0,0,0,.2);*/
    font-weight: bold; font-size: 10px; 
    display: block; line-height: 18px; float: left; 
    -webkit-border-radius: 5px;
    -moz-border-radius: 5px;
    border-radius: 5px;   
    padding: 0 10px; 
    margin: 0 5px 0 0;
    /*width: 40px;*/
    vertical-align: middle; text-align: center;    
    }   
    .miboton {
        text-decoration: none; 
        background-color: #4b8efa; 
        color: #ffffff; 
        border: 0;
        font-size: 12px;  
        display: block; line-height: 20px; float: left;
        padding: 0 5px; 
        margin: 0 5px 0 0;
        width: 80px;
        vertical-align: middle; text-align: center;   
        border: solid 1px #3079ed;   
    }
    .miboton:hover {
        text-decoration: none; 
        background-color: #0072bb;
        color: #ffffff; 
        border: 0;font-size: 12px; 
        display: block; line-height: 20px; float: left; 
        padding: 0 5px; 
        margin: 0 5px 0 0;
        width: 80px;
        vertical-align: middle; text-align: center;   
        border: solid 1px #2f5bb7; 
    }  
.miLink
{
    text-decoration: none; color: /*#23527e*/ #4b8efa; /*font-weight: bold;*/ 
    margin:0; padding:0; line-height: 25px; vertical-align: middle; text-align: left;
    }
.miLink:hover
{
    text-decoration: underline; color: #da4a38 /*#e04c2a*/ ; /*font-weight: bold;*/ 
    margin:0; padding:0; line-height: 25px; vertical-align: middle; text-align: left;
    }
    
</style>

<div id="miContainerMantenimiento">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">    
<ContentTemplate>

<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 930px;">
<tr><td colspan="7" style="height: 10px;"></td></tr>
<tr>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Periodo:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">   
<asp:DropDownList ID="ddlPeriodo" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;"
    OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList> 
    </td>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Grado:</span>
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">  
<asp:DropDownList ID="ddlGrado" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;"
    OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList> 
    </td>
    <td style="width: 50px; height: 25px;" align="left" valign="middle">
<span>Aula:</span>      
    </td>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">   
<asp:DropDownList ID="ddlAula" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;"
    OnSelectedIndexChanged="ddlAula_SelectedIndexChanged" AutoPostBack="true">  
</asp:DropDownList>  
    </td>
    <td style="width: 450px; height: 25px;" align="left" valign="middle">  
    </td>
</tr>
<tr>
    <td style="width: 930px; height: 25px;" align="left" valign="top" colspan="7">      

<div style="border: solid 1px #a6a3a3; margin: 0; padding:0; width: 930px;">    
    <table cellpadding="0" cellspacing="0" border="0" style="width: 930px; height: 26px; color:White; background-color: #555555; font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">  

            <tr>
                <td style="width: 20px; height: 25px;" align="center" valign="middle">
                    <span>&nbsp;&nbsp;</span>
                </td>
                <td style="width: 40px; height: 25px;" align="center" valign="middle">
                    <span>Año</span>
                </td>
                <td style="width: 70px; height: 25px;" align="center" valign="middle">
                    <span>Nivel</span>
                </td>
                <td style="width: 70px; height: 25px;" align="center" valign="middle">
                    <span>Grado</span>
                </td>
                <td style="width: 80px; height: 25px;" align="center" valign="middle">
                    <span>Aula</span>
                </td>                
                <td style="width: 70px; height: 25px;" align="center" valign="middle">
                    <span>Código</span>
                </td>
                <td style="width: 283px; height: 25px;" align="center" valign="middle">  
                    <span>Alumno</span>
                </td>
                <td style="width: 140px; height: 25px;" align="center" valign="middle"> 
                    <span>Curso</span>
                </td>   
                <td style="width: 40px; height: 25px;" align="center" valign="middle">
                    <span>Nota</span>
                </td>    
                <td style="width: 50px; height: 25px;" align="center" valign="middle">
                    <span>Nuevo</span>
                </td>   
                <td style="width: 50px; height: 25px;" align="center" valign="middle">
                    <span>Detalle</span>
                </td>   
                <td style="width:  17px; height: 25px;" align="center" valign="middle"> 
                    <span>&nbsp;&nbsp;&nbsp;</span>
                </td>                    
            </tr>
        </table>  
    </div>    
    <div style="overflow-y: scroll; overflow-x: hidden; width:930px; height: 210px; margin: 0; padding: 0; border: solid 1px #a6a3a3;"> 
        <asp:GridView ID="GridView1" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="913px"
            GridLines="None" 
            ShowFooter="false"
            ShowHeader="false"
            AutoGenerateColumns="False"
            EmptyDataText=" - No se encontraron resultados - "                         
            OnRowDataBound="GridView1_RowDataBound"
            OnRowCommand="GridView1_RowCommand">
        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataRowStyle ForeColor="#da4a38" HorizontalAlign="Center" />                                                                                
            <Columns>
<asp:TemplateField HeaderText="idx" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" />
    </ItemTemplate>   
</asp:TemplateField>  
<asp:TemplateField HeaderText="Año" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
    <ItemTemplate>
        <asp:Label ID="lbldAnio" runat="server" Text='<%# Bind("dAnio") %>' />
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField HeaderText="Nivel" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lbldNivel" runat="server" Text='<%# Bind("dNivel") %>' />
    </ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField HeaderText="Grado" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lbldGrado" runat="server" Text='<%# Bind("dGrado") %>' />
        <asp:Label ID="lblcGrado" runat="server" Text='<%# Bind("cGrado") %>' style="display: none;" />
    </ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField HeaderText="Aula" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
    <ItemTemplate>
        <asp:Label ID="lbldAula" runat="server" Text='<%# Bind("dAula") %>' />
        <asp:Label ID="lblcAula" runat="server" Text='<%# Bind("cAula") %>' style="display: none;" />
    </ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField HeaderText="Código" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lblcAlumno" runat="server" Text='<%# Bind("cAlumno") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Alumno" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="283px">
    <ItemTemplate>
        <asp:Label ID="lblnAlumno" runat="server" Text='<%# Bind("nAlumno") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Curso" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="140px">
    <ItemTemplate>
        <asp:Label ID="lblnCurso" runat="server" Text='<%# Bind("nCurso") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Nota" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
    <ItemTemplate>
        <asp:Label ID="lblnota" runat="server" Text='<%# Bind("nota") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
    <ItemTemplate>
<asp:LinkButton ID="btnSeleccionar" runat="server" CommandName="nuevo" CommandArgument='<%# Bind("cRNA") %>' 
    ToolTip="Agregar Registro" CausesValidation="false" Text="Nuevo" class="miLink" />  
<asp:Label ID="lbltipoReg" runat="server" Text='<%# Bind("tipoReg") %>' style="display: none;" />  
<asp:Label ID="lblcRNCC" runat="server" Text='<%# Bind("cRNCC") %>' style="display: none;" />      
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
    <ItemTemplate>
<asp:LinkButton ID="btnDetalle" runat="server" CommandName="detalle" CommandArgument='<%# Bind("cRNA") %>' 
    ToolTip="Ver Detalle" CausesValidation="false" Text="Detalle" class="miLink" /> 
    </ItemTemplate>
</asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </div>    
    </td>
</tr>
<tr>
    <td style="width: 930px; height: 25px;" align="left" valign="top" colspan="7">  
            
    <div id="miGridviewMantActualizacion_Ficha" style="width: 930px; height: 25px; margin: 0; padding: 0; border: solid 1px #a6a3a3;"> 
        <table cellpadding="0" cellspacing="0" border="0" 
                    style="width: 930px; height: 25px; 
                    color: #265589; background-color: #d3eefa; 
                    font-size: 10px; font-weight: bold; 
                    font-family: Verdana, Arial, Helvetica, sans-serif;">
                    
            <tr>
                <td style="width: 20px; height: 25px;" align="center" valign="middle">
                    <asp:label ID="lblidx_d" runat="server" />
                </td>
                <td style="width: 40px; height: 25px;" align="center" valign="middle">
                    <asp:label ID="lblanio_d" runat="server" />
                </td>
                <td style="width: 70px; height: 25px;" align="center" valign="middle">
                    <asp:label ID="lblnivel_d" runat="server" />
                </td>
                <td style="width: 70px; height: 25px;" align="center" valign="middle">
                    <asp:label ID="lblgrado_d" runat="server" />
                </td>
                <td style="width: 80px; height: 25px;" align="center" valign="middle">
                    <asp:label ID="lblaula_d" runat="server" />
                </td>                
                <td style="width: 70px; height: 25px;" align="center" valign="middle">
                    <asp:label ID="lblcalumno_d" runat="server" />
                </td>
                <td style="width: 283px; height: 25px;" align="left" valign="middle">  
                    <asp:label ID="lblnalumno_d" runat="server" />
                </td>
                <td style="width: 140px; height: 25px;" align="center" valign="middle"> 
                    <asp:label ID="lblcurso_d" runat="server" />
                </td>   
                <td style="width: 40px; height: 25px;" align="center" valign="middle">
                    <asp:label ID="lblnota_d" runat="server" />
                </td>    
                <td style="width: 50px; height: 25px;" align="center" valign="middle">
                    <span>&nbsp;</span>
                </td>   
                <td style="width: 50px; height: 25px;" align="center" valign="middle">
                    <span>&nbsp;</span>
                </td>   
                <td style="width:  17px; height: 25px;" align="center" valign="middle"> 
                    <span>&nbsp;</span>
                </td>                    
            </tr>
        </table>  
    </div>    
    

                    
<div style="border: solid 1px #a6a3a3; margin: 0; padding:0; width: 930px;">    
    <table cellpadding="0" cellspacing="0" border="0" style="width: 930px; height: 26px; color:White; background-color: #555555; font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">                 
                    
            <tr>
                <td style="width: 20px; height: 25px;" align="center" valign="middle">
                    <span>&nbsp;&nbsp;</span>
                </td>
                <td style="width: 200px; height: 25px;" align="center" valign="middle">
                    <span>Profesor</span>
                </td>
                <td style="width: 80px; height: 25px;" align="center" valign="middle">
                    <span>Fecha</span>
                </td>
                <td style="width: 100px; height: 25px;" align="center" valign="middle">
                    <span>Tipo Examen</span>
                </td>    
                <td style="width: 40px; height: 25px;" align="center" valign="middle">
                    <span>Nota</span>
                </td>    
                <td style="width: 50px; height: 25px;" align="center" valign="middle">
                    <span>Editar</span>
                </td>
                <td style="width: 50px; height: 25px;" align="center" valign="middle">
                    <span>Eliminar</span>
                </td>    
                <td style="width: 373px; height: 25px;" align="center" valign="middle">
                    <span>&nbsp;</span>
                </td>  
                <td style="width:  17px; height: 25px;" align="center" valign="middle"> 
                    <span>&nbsp;&nbsp;&nbsp;</span>
                </td>                    
            </tr>
        </table>  
    </div> 
        
    <div style="overflow-y: scroll; overflow-x: hidden; width:930px; height: 105px; margin: 0; padding: 0; border: solid 1px #a6a3a3;"> 
        <asp:GridView ID="GridView2" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="913px"
            GridLines="None" 
            ShowFooter="false"
            ShowHeader="false"
            AutoGenerateColumns="False"                      
            OnRowDataBound="GridView2_RowDataBound"
            OnRowCommand="GridView2_RowCommand">
        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataRowStyle ForeColor="#da4a38" HorizontalAlign="Center" />                                                         
            <Columns>
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" />
    </ItemTemplate>   
</asp:TemplateField>  
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
    <ItemTemplate>
        <asp:Label ID="lblnProfesor" runat="server" Text='<%# Bind("nProfesor") %>' />
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
    <ItemTemplate>
        <asp:Label ID="lblstrFechaExamen" runat="server" Text='<%# Bind("strFechaExamen") %>' />
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
    <ItemTemplate>
        <asp:Label ID="lblTipoExamen" runat="server" Text='<%# Bind("TipoExamen") %>' />
    </ItemTemplate>
</asp:TemplateField> 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
    <ItemTemplate>
        <asp:Label ID="lblnota" runat="server" Text='<%# Bind("nota") %>' />
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
    <ItemTemplate>
<asp:LinkButton ID="btnSeleccionar" runat="server" CommandName="editar" CommandArgument='<%# Bind("cRNA") %>' 
    ToolTip="Editar Registro" CausesValidation="false" Text="Editar" class="miLink" />  
<asp:Label ID="lbltipoReg" runat="server" Text='<%# Bind("tipoReg") %>' style="display: none;" />  
<asp:Label ID="lblcRC" runat="server" Text='<%# Bind("cRC") %>' style="display: none;" />   
<asp:Label ID="lblcRNA" runat="server" Text='<%# Bind("cRNA") %>' style="display: none;" />    
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
    <ItemTemplate>
<asp:LinkButton ID="btnEliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Bind("cRC") %>' 
    ToolTip="Eliminar Registro" CausesValidation="false" Text="Eliminar" class="miLink" />  
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="373px">
    <ItemTemplate>
    </ItemTemplate>
</asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </div>    
    </td>
</tr>

</table>

    <atk:ModalPopupExtender ID="pnModalRegistro" runat="server"
        TargetControlID="VerRegistroRegistro"
        PopupControlID="pnlRegistroRegistro"
        BackgroundCssClass="MiModalBackground" 
        OkControlID="OKRegistroRegistro" 
        CancelControlID="CancelRegistroRegistro"
        Drag="True" 
        PopupDragHandleControlID="RegistroRegistroHeader" Enabled="True" />  
    <asp:panel id="pnlRegistroRegistro" BackColor="White" BorderColor="Black" runat="server" style="display: none;">
<table cellpadding="0" cellspacing="0" border="0" style="width: 520px; border: solid 1px #000000;" id="panelRegistro">          
<tr>
    <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
        <span></span> 
    </td>
    <td style="width: 460px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="4" id="RegistroRegistroHeader">                
        <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Actualizar Registro de Examen</span>
    </td>
    <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
        <asp:ImageButton ID="btnModalCerrarRegistroRegistro" runat="server" Width="16px" Height="15px"
            ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
            onclick="btnModalCancelarRegistro_Click" ToolTip="Cerrar Panel"/>
    </td>
</tr>
<tr><td colspan="6"><br /></td></tr>
<tr>
    <td style="width: 30px; height: 20px"></td>  
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Año:</span>    
    </td>  
    <td style="width: 150px; height: 20px;" align="left" valign="middle">
<asp:Label ID="lblAnioModal" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial; font-weight: bold;" />   
<asp:HiddenField ID="hiddenCodigoSubsa" runat="server" Value="0" /> 
<asp:HiddenField ID="hiddenCodigoNotaAnual" runat="server" Value="0" /> 
<asp:HiddenField ID="hiddenTipoNota" runat="server" Value="0" /> 
    </td>   
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Nivel:</span>    
    </td>  
    <td style="width: 150px; height: 20px;" align="left" valign="middle">
<asp:Label ID="lblNivelModal" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial; font-weight: bold;" />    
    </td>      
    <td style="width: 30px; height: 20px"></td> 
</tr> 
<tr>
    <td style="width: 30px; height: 20px"></td>  
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Grado:</span>    
    </td>  
    <td style="width: 150px; height: 20px;" align="left" valign="middle">
<asp:Label ID="lblGradoModal" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial; font-weight: bold;" />    
    </td>   
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Aula:</span>    
    </td>  
    <td style="width: 150px; height: 20px;" align="left" valign="middle">
<asp:Label ID="lblAulaModal" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial; font-weight: bold;" />    
    </td>      
    <td style="width: 30px; height: 20px"></td> 
</tr> 
<tr>
    <td style="width: 30px; height: 20px"></td>  
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Alumno:</span>    
    </td>  
    <td style="width: 380px; height: 20px;" align="left" valign="middle" colspan="3">
<asp:Label ID="lblAlumnoModal" runat="server" style="width: 370px; font-size: 8pt; font-family: Arial; font-weight: bold;" />    
    </td>       
    <td style="width: 30px; height: 20px"></td> 
</tr> 
<tr>
    <td style="width: 30px; height: 20px"></td>  
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Curso:</span>    
    </td>  
    <td style="width: 150px; height: 20px;" align="left" valign="middle">
<asp:Label ID="lblCursoModal" runat="server" style="width: 370px; font-size: 8pt; font-family: Arial; font-weight: bold;" />    
    </td>  
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Nota Des.:</span>    
    </td>  
    <td style="width: 150px; height: 20px;" align="left" valign="middle">
<asp:Label ID="lblNotaModal" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial; font-weight: bold;" />    
    </td>          
    <td style="width: 30px; height: 20px"></td> 
</tr> 
<tr>
    <td style="width: 30px; height: 20px"></td>  
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Fecha:</span>    
    </td>  
    <td style="width: 150px; height: 20px;" align="left" valign="middle">
<asp:TextBox ID="tbFechaModal" runat="server" style="width: 70px; font-size: 8pt; font-family: Arial; font-weight: bold; text-align: left;" />
<atk:MaskedEditExtender ID="ME_tbFecha" runat="server" 
    TargetControlID="tbFechaModal"
    UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" 
    PromptCharacter="-">
</atk:MaskedEditExtender>   
    </td>   
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
    </td>  
    <td style="width: 150px; height: 20px;" align="left" valign="middle">
    </td>      
    <td style="width: 30px; height: 20px"></td> 
</tr> 
<tr>
    <td style="width: 30px; height: 20px"></td>  
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Profesor Cargo:</span>    
    </td>  
    <td style="width: 380px; height: 20px;" align="left" valign="middle" colspan="3">
<asp:DropDownList ID="ddlProfesorModal" runat="server" style="width: 370px; font-size: 8pt; font-family: Arial; font-weight: bold;" >
</asp:DropDownList>  
    </td>       
    <td style="width: 30px; height: 20px"></td> 
</tr> 

<tr>
    <td style="width: 30px; height: 20px"></td>  
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Tipo Ex.:</span>    
    </td>  
    <td style="width: 380px; height: 20px;" align="left" valign="middle" colspan="3">
<asp:DropDownList ID="ddlTipoExamenModal" runat="server" style="width: 370px; font-size: 8pt; font-family: Arial; font-weight: bold;" >
</asp:DropDownList>  
    </td>       
    <td style="width: 30px; height: 20px"></td> 
</tr>

<tr>
    <td style="width: 30px; height: 20px"></td>  
    <td style="width: 80px; height: 20px;" align="left" valign="middle">
<span>Nota:</span>    
    </td>  
    <td style="width: 380px; height: 20px;" align="left" valign="middle" colspan="3">
<asp:TextBox ID="tbNotaModal" runat="server" MaxLength="2" style="width: 30px; font-size: 8pt; font-family: Arial; text-align: right;" />
    </td>       
    <td style="width: 30px; height: 20px"></td> 
</tr>

<tr><td colspan="6"><br /></td></tr>
<tr>
    <td style="width: 30px; height: 20px"></td>  
    <td colspan="4" align="center" valign="middle"> 
<asp:LinkButton ID="btnModalRegistro" runat="server" CausesValidation="false" Text="Grabar" class="miboton" OnClick="btnModalRegistro_Click" /> 
&nbsp;
<asp:LinkButton ID="btnModalCancelarRegistro" runat="server" CausesValidation="false" Text="Cancelar" class="miboton" OnClick="btnModalCancelarRegistro_Click" /> 
    </td>
    <td style="width: 30px; height: 20px"></td>  
</tr>   
<tr><td colspan="6"><br /></td></tr>             
</table>  
        <div id="controlRegistroRegistro" style="display:none">
            <input type="button" id="VerRegistroRegistro" runat="server" />
            <input type="button" id="OKRegistroRegistro" />
            <input type="button" id="CancelRegistroRegistro" />
        </div>       
    </asp:panel> 
    
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
    
    function confirm_delete() {
        if (confirm('¿Esta seguro que desea eliminar el registro seleccionado?') == true)
            return true;
        else
            return false;
    }  
</script>
</asp:Content>

