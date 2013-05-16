<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Confirmacion_Asistencia_Mensaje.aspx.vb" Inherits="Confirmaciones_Confirmacion_Asistencia_Mensaje" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notificación</title>
    
    <style>
        .tblPrincipal
{
    FONT-SIZE: 12px;
    FONT-FAMILY: Arial, Tahoma, Verdana;
    BACKGROUND-COLOR: #f5f5f5;
            height: 241px;
        }
.tdSeccion
{
    BORDER-RIGHT: #18498c 1px solid;
    BORDER-TOP: #18498c 1px solid;
    FONT-WEIGHT: bolder;
    FONT-SIZE: 12px;
    BORDER-LEFT: #18498c 1px solid;
    COLOR: #ffffff;
    BORDER-BOTTOM: #18498c 1px solid;
    FONT-FAMILY: Verdana, Tahoma, Arial;
    BACKGROUND-COLOR: #18498c
}
.tdBorde
{
    BORDER-RIGHT: #808080 1px solid;
    BORDER-TOP: #808080 1px;
    BORDER-LEFT: #808080 1px solid;
    BORDER-BOTTOM: #808080 1px
}
.tdBoton
{
    BORDER-RIGHT: #808080 1pt solid;
    BORDER-TOP: #808080 1pt solid;
    BORDER-LEFT: #808080 1pt solid;
    BORDER-BOTTOM: #808080 1pt solid;
    BACKGROUND-COLOR: #dfdfdf
}
.btnBoton
{
    BORDER-RIGHT: #808080 1px solid;
    BORDER-TOP: #808080 1px solid;
    BORDER-LEFT: #808080 1px solid;
    WIDTH: 100px;
    BORDER-BOTTOM: #808080 1px solid;
    FONT-FAMILY: Tahoma, Verdana, Arial
}
.TituloForm
{
    FONT-WEIGHT: bolder;
    FONT-SIZE: 14px;
    COLOR: #666666;
    FONT-FAMILY: Verdana, Tahoma, Arial
}
.tdSuperior
{
    BACKGROUND-COLOR: #94a2b5
}
.tdInferior
{
    BORDER-RIGHT: #FF0000 4px;
    BORDER-TOP: #FF0000 4px solid;
    BORDER-LEFT: #FF0000 4px;
    BORDER-BOTTOM: #f79208 4px;
    BACKGROUND-COLOR: #18498c
}
.TituloPro
{
    BORDER-RIGHT: #94a2b5 1px solid;
    BORDER-TOP: #94a2b5 1px solid;
    FONT-WEIGHT: bolder;
    BORDER-LEFT: #94a2b5 1px solid;
    COLOR: #ffffff;
    BORDER-BOTTOM: #94a2b5 1px solid;
    FONT-FAMILY: Verdana, Tahoma, Arial
}
.TextoGeneral
{
    FONT-SIZE: 12px;
    COLOR: #333333;
    FONT-FAMILY: Arial
}
.TextoInferior
{
    FONT-SIZE: 11px;
    COLOR: #ffffff;
    FONT-FAMILY: Verdana, Tahoma, Arial;
            height: 16px;
        }
.TextoInferior A
{
    COLOR: #ffffff
}
.TextoAyuda
{
    FONT-SIZE: 11px;
    COLOR: #808080
}
.ctrControl
{
    FONT-SIZE: 11px;
    FONT-FAMILY: Tahoma, Verdana, sans-serif
}
.TextoCamObl
{
    FONT-SIZE: 11px;
    COLOR: #800000
}

.tfvHighlight
{    
    COLOR: red
}
.tfvNormal
{    
    COLOR: black
}
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <center>
        <table border="0" cellpadding="0" cellspacing="0" width="555" class="tblPrincipal">
	    <tr>
		  <!--<td class="tdSuperior"><img border="0" src="img/top_I.jpg" width="500" height="80"></td>-->
	      <td class="tdSuperior" style="background-color: #FFFFFF">
              <img border="0" 
                  src="http://www.sanjorge.edu.pe/Logos_Documentos_DSJ/Escudo_SG.jpg" 
                  style="height: 90px; width: 86px"></td>
	    </tr>
	    <tr>
	      <td align="center" class="tdSuperior">
	        <table border="0" cellpadding="8" cellspacing="0" width="100%">
	          <tr>
	            <td width="100%" align="center" class="TituloPro">&nbsp;</td>
	          </tr>
	        </table>
	      </td>
	    </tr>
	    <tr>
	      <td align="center" class="tdBorde">
	          <table border="0" cellpadding="12" cellspacing="0" width="98%" class="TextoGeneral">
	            <tr>
	              <td width="100%">
	                  <p align=justify>
                        Estimado Padre de Familia, se ha producido un error en la confirmación de dicha 
                        entrevista, el área de sistemas está trabajando en ello para solucionar el 
                        desperfecto.</p>
	                  <p align=justify>
                        Muchas gracias&nbsp;</p>
                        </td>
	            </tr>
	          </table>
	      </td>
	    </tr>
	    
	    <tr>
	      <td align="center" class="tdInferior">
	          &nbsp;</td>
	    </tr>
	  </table>
	    </center>
    </div>
    </form>
</body>
</html>
