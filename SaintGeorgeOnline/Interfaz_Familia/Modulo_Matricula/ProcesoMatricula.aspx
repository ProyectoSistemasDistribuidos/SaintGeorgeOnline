<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="ProcesoMatricula.aspx.vb" Inherits="Interfaz_Familia_Modulo_Matricula_ProcesoMatricula" title="Página sin título" %>

<%@ MasterType VirtualPath="~/Interfaz_Familia/Plantilla_Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
      <Triggers>
        <asp:PostBackTrigger ControlID="GridView1" />
         <asp:PostBackTrigger ControlID="btn_DocumentoDireccion" />
      </Triggers>         
                
<ContentTemplate>

<table border="0" cellpadding="0" cellspacing="0" style="width: 758px;">
    <tr>
        <td style="width:480px;height:20px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_cab.jpg');background-repeat:no-repeat;" >&nbsp;&nbsp;</td>
        <td style="width:278px;vertical-align:top;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_inferior3.jpg');background-repeat:repeat-y;" rowspan="2" >
            <table border="0" cellpadding="0" cellspacing="0" style="width:278px;">
                <tr>
                    <td style="width:36px;height:20px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_cab1.jpg');background-repeat:no-repeat;" >&nbsp;&nbsp;</td>
                    <td style="width:242px;height:20px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_cab2.jpg');background-repeat:no-repeat;" >&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td style="background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_centro1.jpg');background-repeat:repeat-y;">&nbsp;&nbsp;</td>
                    <td style="padding-left:9px;vertical-align:top;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_centro2.jpg');background-repeat:repeat-y;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:237px">
                <tr>
                    <td style="vertical-align:top;height:48px;padding-left:1px;">
                        <table style="height:45px;width:220px;font-family:Arial;font-size:11px;font-weight:bold;" border="0" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td style="vertical-align:top;">   
                                    <asp:ImageButton ID="btn_MenuPaso1" runat="server" 
                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso1Matricula.jpg" 
                                        Height="47px" Width="202px"/>
                                </td>   
                                
                            </tr>
                            </table>                                              
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;height:48px;padding-left:1px;">
                        <table style="height:45px;width:220px;cursor:pointer;font-family:Arial;font-size:11px;font-weight:bold;" border="0" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td style="vertical-align:top;">
                                    <asp:ImageButton ID="btn_MenuPaso2" runat="server" 
                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso2Matricula_D.jpg" 
                                        Height="47px" Width="202px"/>
                                </td>   
                                
                            </tr>
                            </table> 
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;height:48px;padding-left:1px;">
                        <table style="height:45px;width:220px;cursor:pointer;font-family:Arial;font-size:11px;font-weight:bold;" border="0" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td style="vertical-align:top;">
                                    <asp:ImageButton ID="btn_MenuPaso3" runat="server" 
                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso3Matricula_D.jpg" 
                                        Height="47px" Width="202px"/>
                                </td>                                   
                            </tr>
                            </table> 
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;height:48px;padding-left:1px;">
                        <table style="height:45px;width:220px;cursor:pointer;font-family:Arial;font-size:11px;font-weight:bold;" border="0" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td style="vertical-align:top;">
                                    <asp:ImageButton ID="btn_MenuPaso4" runat="server" 
                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso4Matricula_D.jpg" 
                                        Height="47px" Width="202px"/>
                                </td>                                   
                            </tr>
                        </table> 
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;height:48px;padding-left:1px;">
                        <table style="height:45px;width:220px;cursor:pointer;font-family:Arial;font-size:11px;font-weight:bold;" border="0" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td style="vertical-align:top;">
                                    <asp:ImageButton ID="btn_MenuPaso5" runat="server" 
                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Paso5Matricula_D.jpg" 
                                        Height="47px" Width="202px"/>
                                </td>   
                                
                            </tr>
                        </table> 
                    </td>
                </tr>          
            </table>
                    </td>
                </tr>
                <tr>
                    <td style="height:23px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_pie1.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td>
                    <td style="height:23px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_pie2.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height:380px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_centro.jpg');background-repeat:repeat-y;">
            <table border="0" cellpadding="0" cellspacing="0" style="width:470px;padding-left:15px ">
                        <tr>
                            <td style="background-color:White;height:40px;width:40px ;padding-left:2px;">
                                <asp:Image ID="img_FotoAlumno" Width="35px" runat="server" ImageUrl="/SaintGeorgeOnline/Fotos/noPhoto.gif" />
                            </td>
                            <td style="background-color:White;height:40px; width:430px ; padding-left:2px;">
                                <span style="font-family:Arial;font-weight:normal ;font-size:12px;">Matriculando a:</span>  <br> 
                                    <asp:Label ID="lbl_NombreAlumno" runat="server" 
                                    Text="" Font-Size="12px" Font-Bold="True"></asp:Label>
                                    
<asp:HiddenField ID="hiddenCodigoAlumno" runat="server" Value="" />                                  
<asp:HiddenField ID="hiddenCodigoAnioAcademico" runat="server" Value="0" />
<asp:HiddenField ID="hiddenCodigoFamiliar" runat="server" Value="0" />
<asp:HiddenField ID="hiddenCodigoNivel" runat="server" Value="0" />
<asp:HiddenField ID="hiddenCodigoGrado" runat="server" Value="0" />
<asp:HiddenField ID="hiddenNivel" runat="server" Value="0" />
<asp:HiddenField ID="hiddenGrado" runat="server" Value="0" />       
<asp:HiddenField ID="hiddenNombreCompleto" runat="server" Value="0" />
<asp:HiddenField ID="hiddenFoto" runat="server" Value="0" />                                
                            </td>
                        </tr>                        
            </table>
            <asp:MultiView ID="mv_PasosMatricula" runat="server">
                <asp:View ID="v_Paso1" runat="server" >
                    <table border="0" cellpadding="0" cellspacing="0" style="width:470px;padding-left:15px ">
                        <tr>
                            <td colspan="2" style="background-color:#41576f;height:18px;padding-left:5px;">
                                <span style="font-family:Arial;font-size:11px;font-weight:bold;color:White; ">Paso 1: Requisitos para la Matrícula.</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="background-color:White;height:100px; border: solid 0px red;" valign="top" align="left">
                                <div id="View1_Div1" runat="server" visible="false">
                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 450px;">
<tr>
    <td style="width: 450px; height: 10px;" colspan="2" align="left" valign="middle"><span>&nbsp;</span></td>
</tr>    

<tr>
    <td style="width: 25px; height: 25px;" align="center" valign="top">
        <asp:Image ID="img_Icono0" Width="20px" height="20px" runat="server" ImageUrl="../../App_Themes/Imagenes/AlertIcon.gif" />
    </td>
    <td style="width: 400px; height: 25px; text-align: justify; " align="left" valign="top">
<asp:Label ID="View1_lblMensaje0" runat="server" style="font-family:Arial;font-size:10px;font-weight:bold;color:#000000;" />    
    </td>
</tr>

<tr>
    <td style="width: 25px; height: 25px;" align="center" valign="top">
        <asp:Image ID="img_Icono1" Width="20px" height="20px" runat="server" ImageUrl="../../App_Themes/Imagenes/AlertIcon.gif" />
    </td>
    <td style="width: 400px; height: 25px; text-align: justify; " align="left" valign="top">
<asp:Label ID="View1_lblMensaje1" runat="server" style="font-family:Arial;font-size:10px;font-weight:bold;color:#000000;" />    
    </td>
</tr>
<tr>
    <td style="width:  25px; height: 25px;" align="center" valign="top">
        <asp:Image ID="img_Icono2" Width="20px" height="20px" runat="server" ImageUrl="../../App_Themes/Imagenes/AlertIcon.gif" />
    </td>
    <td style="width: 400px; height: 25px; text-align: justify; " align="left" valign="top">
<asp:Label ID="View1_lblMensaje2" runat="server" style="font-family:Arial;font-size:10px;font-weight:bold;color:#000000;" />    
    </td>
</tr>
<tr>
    <td style="width:  25px; height: 25px;" align="center" valign="top">
        <asp:Image ID="img_Icono3" Width="20px" height="20px" runat="server" ImageUrl="../../App_Themes/Imagenes/AlertIcon.gif" />
    </td>
    <td style="width: 400px; height: 25px; text-align: justify; " align="left" valign="top">
<asp:Label ID="View1_lblMensaje3" runat="server" style="font-family:Arial;font-size:10px;font-weight:bold;color:#000000;" />    
    <asp:Panel ID="pnl_DetLibros" runat ="server" visible="false" >
                <asp:GridView ID="GridView2" runat="server" 
    CssClass="miGridviewBusqueda" 
    Width="350px"
    GridLines="None" 
    AutoGenerateColumns="False"
    AllowPaging="False" 
    AllowSorting="False"
    ShowFooter="false"
    ShowHeader="false"
  >
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                                                
    <Columns>     
                
        <asp:TemplateField ItemStyle-Width="300px" ItemStyle-CssClass=""
            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Titulo") %>' />
            </ItemTemplate>
        </asp:TemplateField> 
            </Columns>
        </asp:GridView>
    </asp:Panel>
    </td>
</tr>
<tr>
    <td style="width: 450px; height: 10px;" colspan="2" align="left" valign="middle"><span>&nbsp;</span></td>
</tr> 
</table>                                
                                </div>
                            </td>
                        </tr>
                        <tr>                           
                             <td style="text-align:right;height:240px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_SiguienteEtapa1" OnClick="btn_SiguienteEtapa1_Click"  runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_SiguienteMatricula.gif" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="v_Paso2" runat="server" >
                    <table border="0" cellpadding="0" cellspacing="0" style="width:470px;padding-left:15px;">
                        <tr>
                            <td colspan="2" style="background-color:#41576f;height:18px;padding-left:5px;">
                                <span style="font-family:Arial;font-size:11px;font-weight:bold;color:White; ">Paso 2: Obligaciones Económicas.</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="background-color:White;height:100px; border: solid 0px red;" valign="top" align="left">
                                <div id="View2_Div1" runat="server" visible="false">
<table border="0" cellpadding="0" cellspacing="0" style="width: 450px;">
<tr>
    <td style="width: 450px; height: 10px;" colspan="2" align="left" valign="middle"><span>&nbsp;</span></td>
</tr>    
<tr>
    <td style="width: 50px; height: 30px;" align="center" valign="middle">
<img src="../../App_Themes/Imagenes/AlertIcon.gif" alt=""  style="border: 0" height="24px" width="24px"/>    
    </td>
    <td style="width: 400px; height: 30px;" align="left" valign="middle">
<span style="font-family:Arial;font-size:10px;font-weight:bold;color:#000000;">Requisito pendiente:</span>
    </td>
</tr>
<tr>
    <td style="width: 50px; height: 70px;" align="center" valign="middle">
    </td>
    <td style="width: 400px; height: 70px; text-align: justify; " align="left" valign="top">
<asp:Label ID="View2_lblMensaje1" runat="server" style="font-family:Arial;font-size:10px;font-weight:bold;color:#000000;" />    
    </td>
</tr>
<tr>
    <td style="width: 450px; height: 10px;" colspan="2" align="left" valign="middle"><span>&nbsp;</span></td>
</tr> 
</table>                                
                                </div>

                                <div id="View2_Div2" runat="server" visible="false">
<table border="0" cellpadding="0" cellspacing="0" style="width: 450px;">
<tr>
    <td style="width: 450px; height: 10px;" align="left" valign="middle"><span>&nbsp;</span></td>
</tr> 
<tr>
    <td style="width: 450px; height: 30px;" align="left" valign="middle">
<asp:Label ID="View2_lblMensaje2" runat="server" style="font-family:Arial;font-size:10px;font-weight:bold;color:#000000; padding-left: 50px;" />  
    </td>
</tr>
<tr>
    <td style="width: 450px; height: 60px;" align="left" valign="middle">  
<table border="0" cellpadding="0" cellspacing="0" style="width: 450px;">
<tr>
    <th style="width: 130px; height: 20px; background-color: #41576f;" align="center" valign="middle">
<span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;">Concepto</span>
    </th>
    <th style="width: 120px; height: 20px; background-color: #41576f" align="center" valign="middle">
<span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;">Lugar</span>
    </th>
    <th style="width: 100px; height: 20px; background-color: #41576f" align="center" valign="middle">
<span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;">Monto</span>
    </th>   
    <th style="width: 100px; height: 20px; background-color: #41576f" align="center" valign="middle">
<span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;">Fecha de Pago</span>
    </th>       
</tr>
<tr>
    <td style="width: 130px; height: 40px; border-left: solid 1px #41576f; border-bottom: solid 1px #41576f;" align="left" valign="middle">
<asp:Label ID="View2_lblPago1" runat="server" style="font-family:Arial; font-size:10px; font-weight:bold; color:#000000; padding-left: 10px;" />  
    </td>
    <td style="width: 120px; height: 40px; border-left: solid 1px #41576f; border-bottom: solid 1px #41576f;" align="left" valign="middle">
<asp:Label ID="View2_lblPago2" runat="server" style="font-family:Arial; font-size:10px; font-weight:bold; color:#000000; padding-left: 10px;" />  
    </td>
    <td style="width: 100px; height: 40px; border-left: solid 1px #41576f; border-bottom: solid 1px #41576f;" align="center" valign="middle">
<asp:Label ID="View2_lblPago3" runat="server" style="font-family:Arial;font-size:10px;font-weight:bold;color:#000000;" />  
    </td>
    <td style="width: 100px; height: 40px; border-left: solid 1px #41576f; border-bottom: solid 1px #41576f; border-right: solid 1px #41576f;" align="center" valign="middle">
<asp:Label ID="View2_lblPago4" runat="server" style="font-family:Arial;font-size:10px;font-weight:bold;color:#000000;" />  
    </td>
</tr>
</table>     
    </td>
</tr>
</table>                                
                                </div>
                                
                            </td>
                        </tr>
                        <tr>
                             <td style="text-align:left;height:240px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_RetrocederEtapa2" OnClick="btn_RetrocederEtapa2_Click"  runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Atras.gif" />
                            </td>
                            <td style="text-align:right;height:240px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_SiguienteEtapa2" OnClick="btn_SiguienteEtapa2_Click"  runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_SiguienteMatricula.gif" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="v_Paso3" runat="server" >
                    <table border="0" cellpadding="0" cellspacing="0" style="width:470px;padding-left:15px">
                        <tr>
                            <td colspan="2" style="background-color:#41576f;height:18px;padding-left:5px;">
                                <span style="font-family:Arial;font-size:11px;font-weight:bold;color:White; ">Paso 3: Entregar Documentos Internos.</span>
                            </td>
                        </tr>
                            <td colspan="2" style="background-color:White;height:30px;Width:350px; padding-left:2px;" >
                                 <span style =" font-size :10px; color  :DarkRed;" >El estado de los documentos debe ser <b>“Entregado”.</b><br /> En el caso del documento de <b>"Declaración de Responsabilidad ante Accidentes (A - Con Seguro,B - Sin Seguro) y Recibo de Hermes"</b> solo es necesario que uno de los documentos sea entregado para continuar con el siguiente paso.<br />Los documentos deben ser firmados y entregados en recepción.</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:100px; width: 470px; background-color:White;" align="center" valign="top">
                                <br />
                                <div style="width: 455px;  border: solid 1px #b4b8bb;">
                                   <table border="0" cellpadding="0" cellspacing="0" style="width: 455px;">
<tr>
    <th style="width: 300px; height: 20px; background-color: #41576f;" align="center" valign="middle">
<span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;">Documento</span>
    </th>
    <th style="width: 55px; height: 20px; background-color: #41576f" align="center" valign="middle">
<span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;">Ver</span>
    </th>
    <th style="width: 100px; height: 20px; background-color: #41576f" align="center" valign="middle">
<span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;">Estado</span>
    </th>      
</tr>
</table>   
                                </div>       

                                <div style="width: 455px; height: auto; border: solid 1px #b4b8bb;">                 
                                   <asp:GridView ID="GridView1" runat="server" 
    CssClass="miGridviewBusqueda" 
    Width="455px"
    GridLines="None" 
    AutoGenerateColumns="False"
    AllowPaging="False" 
    AllowSorting="False"
    ShowFooter="false"
    ShowHeader="false"
    EmptyDataText=" - No se encontraron resultados - "
    OnRowCommand="GridView1_RowCommand"
    OnRowDataBound="GridView1_RowDataBound">
    <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                                                
    <Columns>     
                
        <asp:TemplateField ItemStyle-Width="300px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Descripcion") %>' />
            </ItemTemplate>
        </asp:TemplateField> 
                                 
        <asp:TemplateField ItemStyle-Width="55px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:ImageButton ID="btnVisualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_ver.png"
                    style="width: 16px; height: 16px; cursor: pointer;"                 
                    CommandName="Exportar" CommandArgument='<%# Bind("CodigoDocumento") %>' />            
            </ItemTemplate>
        </asp:TemplateField>                                 
                                 
        <asp:TemplateField ItemStyle-Width="100px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>' />
            </ItemTemplate>
        </asp:TemplateField>
                    
            </Columns>
        </asp:GridView>                            
                                </div>
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align:left;height:240px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_RetrocederEtapa3" OnClick="btn_RetrocederEtapa3_Click"  runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Atras.gif" />
                            </td>
                            <td style="text-align:right;height:240px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_SiguienteEtapa3" OnClick="btn_SiguienteEtapa3_Click"  runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_SiguienteMatricula.gif" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="v_Paso4" runat="server" >
                    <table border="0" cellpadding="0" cellspacing="0" style="width:470px;padding-left:15px ">
                        <tr>
                            <td colspan ="2" style="background-color:#41576f;height:18px;padding-left:5px;  "><span style="font-family:Arial;font-size:11px;font-weight:bold;color:White; ">Paso 4: Aceptar el Documento de Dirección.</span>                            
                            </td>
                        </tr>
                        <tr>
                          <td colspan ="2" style="background-color:White;height:30px;Width:470px; padding-left:2px;" >
                                 <span style ="color :DarkRed;" >Para continuar con la matrícula debe estar seleccionado el <b>check</b> de aceptación del documento. </span>
                          </td>
                        </tr>
                        <tr>
                          <td  style="background-color:White;height:30px;Width:350px; padding-left:2px;" >
                                 <span style ="color :DarkRed;" >Si desea descargar el documento de compromiso de los padres de familia, dar clic al boton "Descargar". </span>
                          </td>
                            <td  style="background-color:White;height:30px;Width:120px; padding-left:2px;" >
                                 <asp:ImageButton ID="btn_DocumentoDireccion" runat="server" Width="100px" Height="26px" OnClick="btn_DocumentoDireccion_Click"
                                 ImageUrl="~/App_Themes/Imagenes/btn_DescargarDD.jpg" ToolTip="Desccargar el documento de compromiso de los padres de familia"/>
                           </td>
                           
                        </tr>
                        <tr>
                            <td colspan ="2" style="background-color:White;height:400px;padding-left:2px;" >
                            <br />
                           <div id="Doc_Paso4" runat="server" style="border: 1px solid #000000;width:450px; "  >     </div>
                           </td>
                        </tr>
                        <tr>
                            <td  colspan ="2" style="background-color:#C3D6CB;height:18px;padding-left:5px;  " >
                              <asp:CheckBox ID="Ck_Etapa4" runat="server" OnCheckedChanged ="Ck_Etapa4_CheckedChanged" AutoPostBack=true />
                              <span style="font-family:Arial;font-size:11px;font-weight:bold;color:Red; "> &nbsp;&nbsp;He leído y acepto los terminos del documento</span>
                            </td>
                        </tr>
                        <tr>
                             <td style="text-align:left;height:40px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_RetrocederEtapa4" OnClick="btn_RetrocederEtapa4_Click"  runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Atras.gif" />
                            </td>
                            <td style="text-align:right;height:40px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_SiguienteEtapa4" OnClick="btn_SiguienteEtapa4_Click"  runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_SiguienteMatricula.gif" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="v_Paso5" runat="server" >
                    <table border="0" cellpadding="0" cellspacing="0" style="width:470px;padding-left:15px ">
                        <tr>
                            <td colspan ="2" style="background-color:#41576f;height:18px;padding-left:5px;  "><span style="font-family:Arial;font-size:11px;font-weight:bold;color:White; ">
                                Paso 5: Finalizar Proceso de Matrícula.</span>
                            </td>
                        </tr>
                         <tr>
                          <td colspan ="2" style="background-color:White;height:30px;Width:470px; padding-left:2px;" >
                                 <span style ="color :DarkRed;" >Si está conforme con la información, seleccionar el botón “Finalizar” y así se dará por terminada el proceso de Matrícula en Línea.</span>
                          </td>
                        </tr>
                      <tr>
                          <td colspan ="2" style="height:100px; width: 470px; height:400px; background-color:White;" align="center" valign="top">
                            <br />
                            <table border ="0" cellpadding="0" cellspacing="0" style="width: 455px;">
                                <tr>
                                    <th style="width: 200px; height: 20px; background-color: #41576f;" align="center" valign="middle">
                                   <span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;"> Fecha de Matrícula</span>
                                    </th>
                                    <th style="width: 100px; height: 20px; background-color: #41576f" align="center" valign="middle">
                                    <span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;"> Nivel</span>
                                     </th> 
                                     <th style="width: 155px; height: 20px; background-color: #41576f" align="center" valign="middle">
                                     <span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;">Grado</span>
                                    </th> 
                                     <%--<th style="width: 155px; height: 20px; background-color: #41576f" align="center" valign="middle">
                                     <span style="font-family:Arial;font-size:10px;font-weight:bold;color:#FFFFFF;">Consolidado de Matrícula</span>
                                    </th> --%>
                                </tr>
                                <tr>
                                    <td style="width: 200px; height: 40px; border-left: solid 1px #41576f; border-bottom: solid 1px #41576f;" align="center" valign="middle">
                                    <asp:Label runat="server" ID ="lblFecha"></asp:Label>
                                    </td>
                                     <td style="width: 100px; height: 40px; border-left: solid 1px #41576f; border-bottom: solid 1px #41576f;" align="center" valign="middle">
                                     <asp:Label runat="server" ID ="lblNivel"></asp:Label>
                                    </td>
                                     <td style="width: 155px; height: 40px; border-left: solid 1px #41576f; border-bottom: solid 1px #41576f;" align="center" valign="middle">
                                     <asp:Label runat="server" ID ="lblGrado"></asp:Label>
                                    </td>
                                    <%--<td style="width: 100px; height: 40px; border-left: solid 1px #41576f; border-bottom: solid 1px #41576f;" align="left" valign="middle">
                                    <asp:Label runat="server" ></asp:Label>
                                    </td>--%>
                                </tr>
                            </table>
                            </div>  
                            
                             
                            
                              <%--<div id="Doc_Paso5" runat="server" style="border: 1px solid #000000;width:450px; "  >     </div>
                            --%></td>
                        </tr>
                         <%--<tr>
                            <td  colspan ="2" style="background-color:#C3D6CB;height:18px;padding-left:5px;  " >
                              <asp:CheckBox ID="Ck_Etapa5" runat="server" OnCheckedChanged ="Ck_Etapa5_CheckedChanged" AutoPostBack=true />
                              <span style="font-family:Arial;font-size:11px;font-weight:bold;color:Red; "> &nbsp;&nbsp;He leido y acepto</span>
                            </td>
                        </tr>--%>
                        <tr>
                             <td style="text-align:left;height:40px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_RetrocederEtapa5" OnClick="btn_RetrocederEtapa5_Click"  runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Atras.gif" />
                            </td>
                            <td style="text-align:right;height:40px;vertical-align:bottom; ">
                             <asp:ImageButton ID="btn_SiguienteEtapa5" OnClick="btn_SiguienteEtapa5_Click"  runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Finalizar.jpg" />
                           
                               </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="v_Paso6" runat="server" >
                    <table border="0" cellpadding="0" cellspacing="0" style="width:470px;padding-left:15px ">
                        <tr>
                            <td colspan ="2" style="background-color:#41576f;height:18px;padding-left:5px;  "><span style="font-family:Arial;font-size:11px;font-weight:bold;color:White; ">
                                Paso 6: Actualizar Datos del Seguro de Accidentes.</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan ="2" style="background-color:White; height:5px;"> <br /></td>
                        </tr>
                       <%-- <tr>
                           <td colspan ="2" style=" padding-left:5px; font-family:Arial ; font-size :11px;  background-color:#F2F5A9; height:20px;">
                              <em > Nota: En caso de que algún dato este desactualizado <strong > El Padre de Familia o Apoderado </strong> podrá enviar una solicitud
                                        de actualización después de realizar el proceso de matrícula.</em> 
                            </td>
                        </tr> --%>
                         <tr>
                           <td colspan ="2" style=" padding-left:5px; font-family:Arial ; font-size :11px;  background-color:#F2F5A9; height:20px;">
                              <em > Nota: El padre de familia o apoderado se compromete en la “Solicitud de Declaración Jurada” de enviar un correo electrónico a Tesorería adjuntando la copia de Carnet de Seguro.</em> 
                            </td>
                        </tr> 
                        <tr>
                            <td colspan ="2" style="background-color:White;height:400px;padding-left:2px;vertical-align:top;padding-right:2px;font-size:11px;font-family:Arial;">
                                <BR>
                                <asp:Panel Font-Bold="true"  ID="pnl_DatosEmergencia"  runat="server" 
                                    GroupingText="Información de Emergencia">
                                    <table cellpadding="0" cellspacing="0"  border="0">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="dgv_DatosEmergenciaFamiliares" 
                                                              runat="server" 
                                                              BorderStyle="None" 
                                                              BorderWidth="0px"
                                                              GridLines="None" 
                                                              ShowHeader="False"
                                                    AutoGenerateColumns="False">
                                                
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="">
                                                                        <asp:Image style="width:17px;height:17px " ID="Image1" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/iconos/persona.gif" />
                                                                    </td>
                                                                    <td style="width:70px ;text-align:left;font-family:Arial;font-size:11px;font-weight:normal; ">
                                                                         <asp:Label ID="Label2" runat="server" Text='<%# Bind("Parentesco") %>'></asp:Label> 
                                                                         : 
                                                                    </td>
                                                                    <td style="text-align: left; font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("NombreCompleto") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;&nbsp;
                                                                    </td>
                                                                    <td style="text-align:left;font-family:Arial;font-size:10px;">
                                                                        &nbsp;</td>
                                                                    <td style="text-align: left; font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                        <span style="font-weight:normal;">Telf. de Casa:</span>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("TelfCasa") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td style="text-align:left;font-family:Arial;font-size:10px;">
                                                                        &nbsp;</td>
                                                                    <td style="text-align: left; font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                        <span style="font-weight:normal;">Telf. Oficina:</span>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("TelfOficina") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td style="text-align:left;font-family:Arial;font-size:10px;">
                                                                        &nbsp;</td>
                                                                    <td style="text-align: left; font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                        <span style="font-weight:normal;">Telf. Celular:</span>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("TelfCelular") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>                          
                                                    </asp:TemplateField>
                                                
                                                    </Columns>
                
                                                </asp:GridView>
                                                <asp:GridView ID="dgv_DatosEmergenciaOtros" 
                                                              runat="server" 
                                                              BorderStyle="None" 
                                                              BorderWidth="0px"
                                                              GridLines="None" 
                                                              ShowHeader="False"
                                                    AutoGenerateColumns="False">
                                                
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:Image style="width:17px;height:17px " ID="Image1" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/iconos/persona.gif" />
                                                                        <span style="font-weight:normal;">Llamar en caso de emergencia al no encontrar a los familiares registrados:</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:30px; ">&#160;</td>
                                                                    <td style="text-align:left;font-family:Arial;font-size:10px;">
                                                                        &nbsp;</td>
                                                                    <td style="text-align: left; font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                        <span style="font-weight:normal;">Persona:</span>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("NombreContactoEmergencia") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &#160;&#160;
                                                                    </td>
                                                                    <td style="text-align:left;font-family:Arial;font-size:10px;">
                                                                        &#160;</td>
                                                                    <td 
                                                                        style="text-align: left; font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                        <span style="font-weight:normal;">Telf. de Casa:</span>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("TelfCasaContactoEmergencia") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td style="text-align:left;font-family:Arial;font-size:10px;">
                                                                        &nbsp;</td>
                                                                    <td style="text-align: left; font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                        <span style="font-weight:normal;">Telf. Oficina:</span>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("TelfOficinaContactoEmergencia") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td style="text-align:left;font-family:Arial;font-size:10px;">
                                                                        &nbsp;</td>
                                                                    <td style="text-align: left; font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                        <span style="font-weight:normal;">Telf. Celular:</span>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("TelfCellContactoEmergencia") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>                          
                                                    </asp:TemplateField>
                                                
                                                    </Columns>
                
                                                </asp:GridView>
                                            </td>
                                        </tr>                                         
                                    </table>
                                </asp:Panel>                                
                            </td></tr><tr>
                            <td colspan ="2" style="background-color:White;padding-left:2px;vertical-align:top;padding-right:2px;font-size:11px;font-family:Arial;">
                                
                               <asp:Panel Font-Bold="true"  ID="pnl_SeguroAccidentes"  runat="server" 
                                    GroupingText="Información de Seguro de Accidentes">
                                    <table cellpadding="0" cellspacing="0"  border="0">
                                        <%--<tr>
                                           <td  >
                                           <BR/>
                                              <asp:Panel Font-Bold="true"  ID="pnlSeguroColegio"  runat="server" >
                                                 <table cellpadding="0" cellspacing="0"  border="0">
                                                    <tr>
                                                       <td colspan ="2" style=" height: 25px; color: #a51515;" align="left">
                                                                        <span >El Alumno cuenta con el Seguro de Accidentes del Colegio</span>
                                                       </td>
                                                    </tr>                               
                                                </table>
                                              </asp:Panel>
                                           </td>
                                        </tr>--%>
                                                                             
                                        <tr>
                                           <td >
                                           <BR/>
                                              <asp:Panel Font-Bold="true"  ID="pnlPreguntaSeguroParticular"  runat="server" >
                                                 <table cellpadding="0" cellspacing="0"  border="0">
                                                    <tr>
                                                       <td style=" height: 25px; color: #a51515;" align="left">
                                                                        <span >¿Su Hijo cuenta con seguro particular?</span>
                                                       </td>
                                                       
                                                    </tr>   
                                                    <tr >
                                                         <td style=" height: 25px" align="left">
                                                           <asp:RadioButtonList ID="rdRespuesta" runat="server" RepeatDirection="Vertical">
                                                                 <asp:ListItem Value="1" Text="Si" Selected="True"  />
                                                                <asp:ListItem Value="0" Text="No" />                                                                          
                                                             </asp:RadioButtonList>  
                                                       </td>
                                                    </tr>                            
                                                </table>
                                              </asp:Panel>
                                           </td>
                                        </tr> 
                                        <tr>
                                           <td >
                                           <br />
                                              <asp:Panel Font-Bold="true"  ID="pnl_Clinicas"  runat="server" >
                                                 <table cellpadding="0" cellspacing="0"  border="0">
                                                    <tr>
                                                    <td colspan ="2" style="height: 8px">
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan ="2" style=" padding-left:5px; font-family:Arial ; font-size :11px;  background-color:#F2F2F2; color :#a51515; height:20px;  margin-right: 7px;">
                                                         <u><strong> El Alumno No cuenta con el Seguro de Accidentes del Colegio.</strong></u> <br /> <i>LLenar los siguientes campos:</i>
                                                        </td> 
                                                    </tr>
                                                     <tr>
                                                    <td colspan ="2" style="height: 8px">
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                       <td align="left" style="height: 25px; padding-left :20px;">
                                                                        <span>Clinica 1 :</span>
                                                       </td>
                                                        <td style="height: 25px; " align="left">
                                                            <asp:DropDownList ID="ddlClinica1" runat="server" Width="121px" Height="18px">
                                                            </asp:DropDownList>
                                                       </td>
                                                    </tr>      
                                                    <tr>
                                                       <td align="left" style="height: 25px; padding-left :20px;">
                                                                        <span>Clinica 2 :</span>
                                                       </td>
                                                        <td style="height: 25px;" align="left">
                                                            <asp:DropDownList ID="ddlClinica2" runat="server" Width="121px" Height="18px">
                                                            </asp:DropDownList>
                                                       </td>
                                                    </tr>       
                                                    <tr>
                                                        <td align="left" style="height: 25px; padding-left :20px;">
                                                                        <span>Clinica 3 :</span>
                                                       </td>
                                                          <td style=" height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlClinica3" runat="server" Width="121px" Height="18px">
                                                            </asp:DropDownList>
                                                       </td>
                                                    </tr>                                        
                                                    <tr>
                                                       <td align="left" style="height: 25px; padding-left :20px;">
                                                            <span>Otra Clínica :</span>
                                                       </td>
                                                        <td style=" height: 25px" align="left">
                                                           <asp:TextBox ID="TextBox4" runat="server" 
                                                                Enabled="true" 
                                                                Width="150px" ></asp:TextBox></td></tr></table></asp:Panel></td></tr><tr>
                                           <td >
                                           <BR/>
                                              <asp:Panel Font-Bold="true"  ID="pnl_SinSeguro"  runat="server" >
                                                 <table cellpadding="0" cellspacing="0"  border="0">
                                                    <%--<tr>
                                                     <td style="padding-left:5px; width: 230px; height: 25px" align="left">
                                                            <span>Compañia :</span><span style="color :#a51515px;  font-weight: bold;">(*)</span>
                                                       </td>
                                                     
                                                    </tr>--%>    
                                                     
                                                     
                                                                              
                                                </table>
                                              </asp:Panel>
                                           </td>
                                        </tr>
                                                                             
                                        <tr>
                                           <td >
                                           <BR/>
                                              <asp:Panel Font-Bold="true"  ID="pnlSeguroParticular"  runat="server" >
                                                 <table cellpadding="0" cellspacing="0"  border="0">
                                                    <tr>
                                                     <td style="padding-left:5px; width: 230px; height: 25px" align="left">
                                                            <span>Compañia :</span>
                                                       </td>
                                                        <td style=" height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlCompañia" runat="server" Width="121px" Height="18px">
                                                            </asp:DropDownList>
                                                       </td>
                                                    </tr>    
                                                    <tr>
                                                        <td style="padding-left:5px; width: 230px; height: 25px" align="left">
                                                            <span>Otra Compañia :</span>
                                                       </td>
                                                        <td style=" height: 25px" align="left">
                                                           <asp:TextBox ID="tbCompañia" runat="server" 
                                                                Enabled="true"  
                                                                Width="150px" ></asp:TextBox></td></tr><tr>
                                                        <td style="padding-left:5px; width: 230px; height: 25px" align="left">
                                                            <span>Nro de Póliza :</span>
                                                       </td>
                                                        <td style=" height: 25px" align="left">
                                                           <asp:TextBox ID="TextBox1" runat="server" 
                                                               Enabled="true"
                                                               Width="150px" ></asp:TextBox></td></tr><tr>
                                                        <td style="padding-left:5px; width: 230px; height: 25px" align="left">
                                                            <span>Fecha de Inicio :</span>
                                                       </td>
                                                        <td style=" height: 25px" align="left">
                                                           <asp:TextBox ID="TextBox2" runat="server" 
                                                               Enabled="true" 
                                                                Width="150px" ></asp:TextBox></td></tr><tr>
                                                        <td style="padding-left:5px; width: 230px; height: 25px" align="left">
                                                            <span>Fecha de Vencimiento :</span>
                                                       </td>
                                                        <td style=" height: 25px" align="left">
                                                           <asp:TextBox ID="TextBox3" runat="server" 
                                                                 Enabled="true" 
                                                                 Width="150px" ></asp:TextBox></td></tr></table></asp:Panel></td></tr></table></asp:Panel></td></tr><tr>
                             <td style="text-align:left;height:40px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_RetrocederEtapa6"   runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Atras.gif" />
                            </td>
                            <td style="text-align:right;height:40px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_SiguienteEtapa6"   runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_SiguienteMatricula.gif" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="v_Paso7" runat="server" >
                    <table border="0" cellpadding="0" cellspacing="0" style="width:470px;padding-left:15px ">
                        <tr>
                            <td colspan ="2" style="background-color:#41576f;height:18px;padding-left:5px;  "><span style="font-family:Arial;font-size:11px;font-weight:bold;color:White; ">Paso 7: Finalizar Proceso de Matrícula.</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan ="2" style="background-color:White;height:100px;" >
                            
                            </td>
                        </tr>
                        <tr>
                             <td style="text-align:left;height:240px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_RetrocederEtapa7"  runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_Atras.gif" />
                            </td>
                            <td style="text-align:right;height:240px;vertical-align:bottom; ">
                                <asp:ImageButton ID="btn_SiguienteEtapa7"   runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/Familia/botones/btn_SiguienteMatricula.gif" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </td>
    </tr>
    <tr>
        <td style="height:30px;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_inferior.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td><td style="background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupomatricula_contenedor_inferior2.jpg');background-repeat:no-repeat;">&nbsp;&nbsp;</td></tr></table></ContentTemplate></asp:UpdatePanel></asp:Content>