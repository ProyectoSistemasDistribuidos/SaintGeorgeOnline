<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ReportesRelacionAlumno.aspx.vb" Inherits="ModuloReportes_ReportesRelacionAlumno" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }      
    #miTablaFiltros span{
        margin: 0;
        padding: 0;
        font-size: 11px;
        font-family: Arial;    
    }      
        .style1
        {
            height: 25px;
            width: 127px;
        }
        .style2
        {
            height: 25px;
            width: 102px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" >

        function MostrarImpresionFotoAlumno_html() {

            window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FotosAlumnos_html.aspx', '_blank', '');
        }

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }

    function MostrarImpresionFichaMedica_html() {

        window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FichaAtencionHistorialClinico_html.aspx', '_blank', '');
    }


    function listar(url) {
        $("#link").html("<a href='" + url + "'>descargar</a>")
    }
      /*
      
      listar


 <td id="link">
             
            </td>
      */
</script>

<div id="miPaginaMantenimiento">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>    
        <asp:PostBackTrigger ControlID="btnReporteExportar" />
    </Triggers>
<ContentTemplate>

    <div style="border: solid 0px blue; width: 860px;">
                
    <div id="miBusquedaActualizacion_Ficha"><!-- 650px -->
    <table style="width: 990px;" cellpadding="0" cellspacing="0" border="0">
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
                width: 440px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="left" valign="middle">
            <span style="padding-left: 30px;">Filtros</span>
            </td>
            <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#0a0f14; 
                width: 150px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="center" valign="middle">                                    
            </td>
        </tr>
        
        <tr>
            <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size:10px;" valign="top" align="left">
    <asp:ListBox ID="lstReportes" runat="server" style="Height: 250px; Width: 200px; border: 0; font-weight: bold; font-size:11px; font-family: Arial;" 
        OnSelectedIndexChanged="lstReportes_SelectedIndexChanged"  AutoPostBack="True">                 
    </asp:ListBox>   
            </td>
            
            <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size:10px;" valign="top" align="left">
    <asp:ListBox ID="lstPresentacion" runat="server" style="Height: 250px; Width: 200px; border: 0; font-weight: bold; font-size:11px; font-family: Arial;" 
        OnSelectedIndexChanged="lstPresentacion_SelectedIndexChanged"  AutoPostBack="True">                            
    </asp:ListBox>   
            </td>            
            
            <td style="border-style :solid; border-width:1px; border-color:#a6a3a3; width: 440px; height: auto; " valign="top" align="left">
                                
<asp:Panel ID="pnlReporte1" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Año Académico :</span></td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAnioAcademico1" runat="server" Width="100px">
                <asp:ListItem Value="0"> 2010 </asp:ListItem>
                <asp:ListItem Value="1"> 2009 </asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Nivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Nivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_Nivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>SubNivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_SubNivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_SubNivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>     
    <tr>
        <td align="left" valign="middle" class="style2" >                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Grado" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_Grado_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>    
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Aula" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true">
            </asp:DropDownList>  
        </td>
    </tr>     
    </table>                            
</asp:Panel> 
<asp:Panel ID="pnlReporte2" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table ID="miTablaFiltros0" border="0" cellpadding="0" cellspacing="0" 
        style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" valign="middle" class="style2">                                
            <span>Año Académico :</span></td>
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlAnioAcademico2" runat="server" Width="100px">
                    <asp:ListItem Value="0"> 2010 </asp:ListItem>
                    <asp:ListItem Value="1"> 2009 </asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" class="style1">
                <span>Mes :</span>
            </td>
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlMes" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" class="style2">                                
            <span>Nivel :</span>  
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlRep1_Nivel1" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlRep1_Nivel1_SelectedIndexChanged"
                    style="width: 190px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" class="style2">                                
            <span>SubNivel :</span>                            
        </td>
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlRep1_SubNivel1" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlRep1_SubNivel1_SelectedIndexChanged"
                    style="width: 190px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" class="style2" >                                
            <span>Grado :</span>                            
        </td>
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlRep1_Grado1" runat="server" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlRep1_Grado1_SelectedIndexChanged" 
                    style="width: 190px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" class="style2">                                
            <span>Aula :</span>                            
        </td>
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlRep1_Aula1" runat="server" AutoPostBack="true" 
                    style="width: 190px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Panel> 
<asp:Panel ID="pnlReporte3" runat="server" BackColor="White" style="width: 300px; height: 200px;">

    <table ID="miTablaFiltros" border="0" cellpadding="0" cellspacing="0" style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">                                
            <span>Salón:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="ddlAsignacionAula3" runat="server" Width="220px" style="font-size: 8pt; font-family: Courier New;">
</asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">      
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="ddlBimestre3" runat="server" style="width: 125px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList> 

              
            </td>
        </tr>
    </table>

</asp:Panel> 
<asp:Panel Visible="false" ID="pnlReporteComparacion" runat="server" BackColor="White" style="width: 300px; height: 200px;">

    <table ID="Table3" border="0" cellpadding="0" cellspacing="0" style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">                                
                Grado<span>:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="cmbPimariaComparacionBimestre" runat="server" Width="220px" style="font-size: 8pt; font-family: Courier New;">
</asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">      
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="cmbBimestreA" runat="server" style="width: 125px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList> 

              
            </td>
        </tr>
        
         <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">      
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="cmbBimestreB" runat="server" style="width: 125px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList> 

              
            </td>
        </tr>
    </table>

</asp:Panel> 
<asp:Panel ID="pnlReporteGradoPrimariaBimestre" runat="server" BackColor="White" style="width: 300px; height: 200px;">

    <table ID="Table4" border="0" cellpadding="0" cellspacing="0" style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">                                
                Grado<span>:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="cmbReportePrimariaGradoBimestre" runat="server" Width="220px" style="font-size: 8pt; font-family: Courier New;">
</asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">      
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="cmbBimestreReportePrimariaGradoBimestre" runat="server" style="width: 125px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList> 

              
            </td>
        </tr>
        
         
    </table>

</asp:Panel>
<asp:Panel ID="pnlReporte4" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table ID="miTablaFiltros1" border="0" cellpadding="0" cellspacing="0" 
        style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">   
                <span>Salón:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="ddlAsignacionAula4" runat="server" 
                    style="font-size: 8pt; font-family: Courier New;" Width="220px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="ddlBimestre4" runat="server" 
                    style="width: 125px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
          <td id="link">
            </td>
            
            
            
        </tr>
    </table>
</asp:Panel>  
 <asp:Panel ID="pnlReporteAsistencia" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltrosAsistencia">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Año :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAnioAcademico_Asist" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"  >
            </asp:DropDownList>                                       
        </td>
    </tr>
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Bimestre :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlBimestre_Asist" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" >
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAula_Asist" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"
             AutoPostBack="true" OnSelectedIndexChanged="ddlAula_Asist_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Alumno :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAlumno_Asist" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
               >
            </asp:DropDownList>                             
        </td>
    </tr>    
    </table>                            
</asp:Panel>
 <asp:Panel ID="pnlReporteRetiro" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltrosRetiro">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Año :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAnio_ret" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"  >
            </asp:DropDownList>                                       
        </td>
    </tr>   
    </table>                            
</asp:Panel>
<asp:Panel ID="pnlReporteFotos" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="Table5">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Año :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlBuscarAnioAcademicoFoto" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"  >
            </asp:DropDownList>                                       
        </td>
    </tr>   
     <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlBuscarGradoFoto" runat="server" OnSelectedIndexChanged="ddlBuscarGradoFoto_SelectedIndexChanged" AutoPostBack="true" style="width: 190px; font-size: 8pt; font-family: Arial;"  >
            </asp:DropDownList>                                       
        </td>
    </tr>
     <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlBuscarAulaFoto" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"  >
            </asp:DropDownList>                                       
        </td>
    </tr>
    </table>                            
</asp:Panel>
<asp:Panel ID="pnlPrimaria" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table ID="miTablaFiltros1" border="0" cellpadding="0" cellspacing="0" 
        style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Salón:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="ddlSalonRepPrimaria" runat="server" 
                    style="font-size: 8pt; font-family: Courier New;" Width="220px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="cmbBimestrePrimaria" runat="server" 
                    style="width: 125px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
          <td id="Td1">
            </td>
            
            
            
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlDescriptores" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table ID="Table1" border="0" cellpadding="0" cellspacing="0" 
        style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Salón:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="dpsSalonDescriptores" runat="server" 
                    style="width: 220px; font-size: 8pt; font-family: Courier New;"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
<tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Curso:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="dpdCursosDescriptores" runat="server" 
                    style="width: 220px; font-size: 8pt; font-family: Courier New;">
                </asp:DropDownList>
          <td id="Td3">
            </td>
            
            
            
        </tr>
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="dpdBimestreDescriptores" runat="server" 
                    style="width: 125px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
          <td id="Td2">
            </td>
            
            
            
        </tr>
    </table>
</asp:Panel>
 <asp:Panel ID="pnlExportarRegistroNotas" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table ID="Table2" border="0" cellpadding="0" cellspacing="0" 
        style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Salón:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="cmbSalonPrimariaInicial" runat="server" 
                    style="width: 220px; font-size: 8pt; font-family: Courier New;"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
<tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Curso:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="cmbCurso" runat="server" 
                    style="width: 220px; font-size: 8pt; font-family: Courier New;">
                </asp:DropDownList>
          <td id="Td4">
            </td>
            
            
            
        </tr>
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="DropDownList3" runat="server" 
                    style="width: 125px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
          <td id="Td5">
            </td>
            
            
            
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlReporteMatricula" runat="server" BackColor="White" style="width: 300px; height: 200px;">

    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Año Académico :</span></td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAnioAcademico_RepMatricula" runat="server" Width="100px">
            </asp:DropDownList>
        </td>
    </tr>    
    
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Estado:</span></td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlTipoMatricula_RepMatricula" runat="server" Width="100px">
                <asp:ListItem Text="No Oficial" Selected="true" Value="2" ></asp:ListItem>
                <asp:ListItem Text="Oficial" Value="0" ></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    </table>
    
</asp:Panel>    
<asp:Panel ID="pnlReporteLibretas" runat="server" BackColor="White" style="width: 400px; height: 200px;">

<table cellpadding="0" cellspacing="0" border="0" style="width: 400px; border: solid 0px red; margin: 0 0 0 20px; padding: 0; font-size: 11px; font-family: Arial;">    
    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle">Periodo:</td>
        <td style="width: 330px; height: 25px;" align="left" valign="middle">
            <asp:DropDownList ID="ddlAnioAcademicoLibretas" runat="server" style="width: 90px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                  
        </td>    
    </tr>  

    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle">Bimestre:</td>
        <td style="width: 330px; height: 25px;" align="left" valign="middle">
<asp:DropDownList ID="ddlBimestreLibretas" runat="server" style="width: 320px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList>                      
        </td>
    </tr>

    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle">Salon:</td>
        <td style="width: 330px; height: 25px;" align="left" valign="middle">
<asp:DropDownList ID="ddlSalonLibretas" runat="server" style="width: 320px; font-size: 8pt; font-family: Courier New;"
    OnSelectedIndexChanged="ddlSalonLibretas_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>      
        </td>
    </tr>
   
    <tr>
        <td style="width: 400px; height: 25px;" align="left" valign="top" colspan="2">    
    
    

    <div id="miGridviewMantActualizacion_Ficha" style="width: 400px; height: 26px; margin: 0; padding: 0; border: 0;">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 400px; height: 26px; color:White; background-color: #0a0f14; 
               font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;" class="miGVBusquedaFicha_Header">
            <tr>
                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                    <span>&nbsp;&nbsp;</span>
                </td>
                <td style="width: 70px; height: 26px;" align="center" valign="middle">
                    <span>Codigo</span>
                </td>
                <td style="width: 260px; height: 26px;" align="center" valign="middle">  
                    <span>Apellidos y Nombres</span>
                </td>
                <td style="width:  30px; height: 26px;" align="center" valign="middle"> 
                    <asp:CheckBox ID="chkAll1" runat="server" /><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </td>   
                <td style="width:  20px; height: 26px;" align="center" valign="middle"> 
                    <span>&nbsp;&nbsp;&nbsp;</span>
                </td>                    
            </tr>
        </table>      
    </div>     
              
        </td> 
    </tr>
    <tr>
        <td style="width: 400px; height: 25px;" align="left" valign="top" colspan="2">       

    <div style="overflow-y: scroll; overflow-x: hidden; width:397px; height: 146px; margin: 0; padding: 0; border: solid 1px #a6a3a3; ">   
        <asp:GridView ID="GridView1" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="380px"
            GridLines="None" 
            ShowFooter="false"
            ShowHeader="false"
            AutoGenerateColumns="False"
            EmptyDataText=" - No se encontraron resultados - "                         
            OnRowDataBound="GridView1_RowDataBound">
        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                                                
            <Columns>
<asp:TemplateField HeaderText="idx" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" />
    </ItemTemplate>   
</asp:TemplateField>         
            
<asp:TemplateField HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
    </ItemTemplate>
</asp:TemplateField>                
<asp:TemplateField HeaderText="Nombre de Alumno" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="left" ItemStyle-Width="260px">
    <ItemTemplate>
        <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' />
    </ItemTemplate>
</asp:TemplateField>                 
<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30px">
    <ItemTemplate>
        <asp:CheckBox ID="chk" runat="server" />
    </ItemTemplate>
</asp:TemplateField>     
            </Columns>
        </asp:GridView>     
    </div>            
                
        </td> 
    </tr>
</table>

</asp:Panel>  

     
            </td>
            <td style="border-style :solid; border-width:1px; border-color:#a6a3a3; width: 150px; height: auto; " valign="top" align="center">                                                                
            <table cellpadding="0" cellspacing="0" border="0" style="width: 150px; height: 26px;">
            <tr>
                <td style="width: 150px; height: 26px;" valign="middle" align="center">
                    <asp:ImageButton ID="btnReporteExportar" runat="server" Width="84" Height="19" 
                        ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                        onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'" ToolTip="Exportar"
                        onclick="btnReporteExportar_Click" />                                               
                </td>
            </tr>
            </table>
            </td>
        </tr>                                                     
    </table>                        
    </div>             
                
    </div>

<br />

    <atk:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
        DynamicServicePath=""
        Enabled="True" 
        PopupControlID="pnlImpresion"
        TargetControlID="lblAccionExportar">
    </atk:ModalPopupExtender>
    <asp:Panel ID="pnlImpresion" runat="server" BackColor="White" Height="71px" Width="388px" Style="display: none">
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
<div>
 
<div id="link">
<span style="display: none;">ghjghjghj</span>
</div>
</div>
</div>

<script type="text/javascript">

    $(document).ready(function() {

        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);

        var grid1 = document.getElementById("<%= GridView1.ClientID %>");
        $('input:checkbox[id$=chkAll1]').click(function(e) {
            if (grid1 != undefined) {
                if (grid1.rows.length > 0) {
                    var estadoCheck = $(this).is(':checked');
                    $(grid1).find("input:checkbox").each(function(e, checkbox) {
                        if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                        else { $(checkbox).removeAttr('checked'); }
                    });
                }
            }
        }); 

    });

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            var grid1 = document.getElementById("<%= GridView1.ClientID %>");
            $('input:checkbox[id$=chkAll1]').click(function(e) {
                if (grid1 != undefined) {
                    if (grid1.rows.length > 0) {
                        var estadoCheck = $(this).is(':checked');
                        $(grid1).find("input:checkbox").each(function(e, checkbox) {
                            if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                            else { $(checkbox).removeAttr('checked'); }
                        });
                    }
                }
            });
        }
    }        
    
</script>

</asp:Content>

