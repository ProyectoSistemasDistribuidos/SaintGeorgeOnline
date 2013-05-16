<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ReportesLibros.aspx.vb" Inherits="Modulo_Reportes_ReportesLibros" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style type="text/css">
               
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=75);
        opacity: 0.7;
    }
    
    .style1
    {
        width: 101px;
    }
    
    .style2
    {
        height: 25px;
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
    
</style>

 <script type="text/javascript">

      function MostrarImpresionFichaMedica_html() {

          window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FotosLibros_html.aspx', '_blank', '');
     }

    </script>

<div id="miPaginaMantenimiento">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
  
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExportar" />
    </Triggers>
 
    
    <ContentTemplate>    
 
        <div style="border: solid 0px blue; width: 860px;">
        <div ="miBusquedaActualizacion_Ficha">
            <table cellpadding="0" cellspacing="0" style="border-style: solid; border-width: 1px;
                border-color: #a6a3a3;" width="850px;">
                <%--<tr>
                    <td style="border-style: solid; border-width: 1px; border-color: #a6a3a3; width: 250px;
                        height: 26px; text-align: center; color: White; font-size: 10px; background-color: #0a0f14;"
                        align="left">
                        Tipo de Reportes
                    </td>
                    <td colspan="2" style="border-style: solid; border-width: 1px; border-color: #a6a3a3;
                        width: 570px; height: 26px; text-align: center; color: White; font-size: 10px;
                        background-color: #0a0f14;" align="left">
                        Parametros de Entrada
                    </td>
                </tr>--%>
                 <tr>
                    <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#0a0f14; 
                        width: 200px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="left" valign="middle">
                    <span style="padding-left: 30px;">Reporte</span>
                    </td>
                    <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#0a0f14; 
                        width: 200px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="left" valign="middle">
                    <span style="padding-left: 30px;">Presentación</span>
                    </td>
                    <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#0a0f14; 
                        width: 300px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="left" valign="middle">
                    <span style="padding-left: 30px;">Filtros</span>
                    </td>
                    <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#0a0f14; 
                        width: 120px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="center" valign="middle">                                    
                    </td>
                </tr>
                <tr>
                
                   <%-- <td valign="top" style="border-style: solid; border-width: 1px; border-color: #a6a3a3;
                        width: 250px; height: 26px; text-align: center; color: White; font-size: 10px;">
                        <asp:ListBox ID="lstReportes" runat="server" Height="161px" Width="250px" Style="border: solid 0px red"
                            Font-Underline="True" Font-Bold="True" Font-Italic="False" AutoPostBack="True"
                            OnSelectedIndexChanged="lstReportes_SelectedIndexChanged">
                            <asp:ListItem Value="0">Consolidado de Prestamos</asp:ListItem>
                            <asp:ListItem Value="1">Consolidado de Devoluciones</asp:ListItem>
                            <asp:ListItem Value="2">Estadisticas de prestamos de Libros</asp:ListItem>
                            <asp:ListItem Value="3">Años de Utilidad</asp:ListItem>
                            <asp:ListItem Value="4">Listado de Libros con imagenes</asp:ListItem>
                        </asp:ListBox>
                    </td>--%>
                    <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size:10px;" valign="top" align="left">
                        <asp:ListBox ID="lstReportes" runat="server" style="Height: 250px; Width: 200px; border: 0; font-weight: bold; font-size:11px; font-family: Arial;"
                            OnSelectedIndexChanged="lstReportes_SelectedIndexChanged"  AutoPostBack="True">                 
                        </asp:ListBox>   
                                </td>
                                
                                <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size:10px;" valign="top" align="left">
                         <asp:ListBox ID="lstPresentacion" runat="server" style="Height: 250px; Width: 200px; border: 0; font-weight: bold; font-size:11px; font-family: Arial;">                            
                        </asp:ListBox>    
                    </td>  
                    <%--<td valign="top" style="border-style: solid; border-width: 1px; border-color: #a6a3a3;
                        width: 420px;">--%>
                      <td style="border-style :solid; border-width:1px; border-color:#a6a3a3; width: 300px; height: auto; " valign="top" align="left">
                 
                        <asp:Panel ID="pnlPrestamos" runat="server" BackColor="White" style="width: 300px; height: 200px;">
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
                                <tr>
                                    <td style="width: 80px; height: 25px;" align="left" valign="middle">
                                        <span>Año Académico: </span>
                                    </td>
                                    <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlAnioAcademico" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80px; height: 25px;" align="left" valign="middle">
                                        <span>Grado: </span>
                                    </td>
                                    <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlGrado" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"  AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80px; height: 25px;" align="left" valign="middle">
                                        <span>Aula: </span>
                                    </td>
                                    <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlAula" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlAniosUtilidad" runat="server" BackColor="White" style="width: 300px; height: 200px;">
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="Table1">
                                <tr>
                                    <td style="width: 80px; height: 25px;" align="left" valign="middle">
                                        <span>Periodo de Inicio: </span>
                                    </td>
                                    <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlPeriodoInicio" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                     <td style="width: 80px; height: 25px;" align="left" valign="middle">
                                        <span>Periodo Fin: </span>
                                    </td>
                                    <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlPeriodoFin" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <%--<td valign="top" align="center" style="border-style: solid; border-width: 1px; border-color: #a6a3a3;
                        width: 150px;">--%>
                      <td style="border-style :solid; border-width:1px; border-color:#a6a3a3; width: 150px; height: auto; " valign="top" align="center">                                                                
                          <%--<table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td valign="middle" width="100px" align="center">--%>
                                    <asp:ImageButton ID="btnExportar" runat="server" Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                        onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                        ToolTip="Exportar" OnClick="btnExportar_click" />
                               <%-- </td>
                            </tr>
                            <tr>
                                <td valign="middle" width="100px">
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                </tr>
            </table>
       </div>
       </div>
        <br />                     
           
    <asp:Panel ID="pnlImpresion" runat="server" Height="120px" Width="494px"  >
                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                               <tr>                                                    
                                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10px;">
                                                        <table  border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="height:15px;width:494px;background-image: url('/SaintGeorgeOnline/App_Themes/ImagenesSAB/bg-box-top.png'); background-repeat:no-repeat;">
                                                                &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-left:20px ;height:82px;width:494px;background-image: url('/SaintGeorgeOnline/App_Themes/ImagenesSAB/bg-box-body.png'); background-repeat:no-repeat;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Image ID="img_Alerta" runat="server" Width="48px" Height="48px" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Alerta" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height:24px;width:494px;background-image: url('/SaintGeorgeOnline/App_Themes/ImagenesSAB/bg-box-bottom.png'); background-repeat:no-repeat; ">
                                                                &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>                                            
                                                    </td>
                                                </tr>    
                                        
                                            </table>
                                        </asp:Panel>
                            
    <atk:ModalPopupExtender ID="ModalPopupExtender1" 
                                        runat="server"
                                        DynamicServicePath="" 
                                        Enabled="True" 
                                        BackgroundCssClass="FondoAplicacion"
                                        DropShadow="false"
                                        PopupControlID="pnlImpresion"                    
                                        TargetControlID="lblAccionExportar"
                                        >
                                        </atk:ModalPopupExtender>
                                        
    <asp:Label ID="lblAccionExportar" runat="server" ForeColor="White" Text="..."></asp:Label>
              
              
    
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
<ContentTemplate>
        <atk:ModalPopupExtender id="ModalPopupExtender2" runat="server" 
            TargetControlID="Label1" 
            PopupControlID="Panel1" 
            BackgroundCssClass="MiModalBackground" 
            DropShadow="false" />
         
        <asp:Panel ID="Panel1" runat="server" style="background-color:#FFFFFF;display:none;width:250px">
            <div style="margin: auto;">
            <table cellpadding="0" cellspacing="0" border="0" style="width:250px; border: solid 1px #000000">
                <tr><td colspan="3"></td>
                    <td style="width: 20px;" align="center" valign="middle">
                        <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" OnClick="btnVolver_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px;" align="left" valign="middle"></td>
                    <td style="width: 80px;" align="left" valign="middle">
                        <img alt="Procesando..." src="../App_Themes/Imagenes/ajax-loader.gif" />  
                    </td>
                    <td style="width: 130px;" align="left" valign="middle">
                        <span style="color: #6684b7; font-family: Arial; font-size: 9pt; font-weight: bold;">Procesando...<br />Espere un momento.</span>
                    </td>
                    <td style="width: 20px;" align="left" valign="middle">
                        
                    </td>
                </tr>
                <tr><td colspan="4"><br /></td></tr>
            </table>                              
            </div>
        </asp:Panel>        
    
        <asp:Label ID="Label1" runat="server" Text="MiModalHandler" style="display:none;"/> 
</ContentTemplate>    
</asp:UpdatePanel>             
              
 <%--   </div>--%>
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

    function ShowMyModalPopup() {

        var ddlAnioAcademico = document.getElementById("<%=ddlAnioAcademico.ClientID%>");
        var codAnioAcademico = ddlAnioAcademico.options[ddlAnioAcademico.selectedIndex].value;   
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender2');
        if (codAnioAcademico > 0) {
            modal.show();
        }

    }
</script>

</asp:Content>

