/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package edu.upc.bancoLibros.dao;

import edu.upc.bancoLibros.conexion.DBConexionSQL;
import edu.upc.bancoLibros.model.beanPrestamo;
import edu.upc.bancoLibros.model.beanValidacionAlumno;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

/**
 *
 * @author Mishishita
 */
public class PrestamoDAO {
    
    
        public ArrayList<beanPrestamo> consultaPrestamo(String codigoAlumno,int anio) throws Exception{
        
            ArrayList<beanPrestamo> resultadoPrestamo= new ArrayList<beanPrestamo>();
            beanPrestamo bean= new beanPrestamo();

        
            Connection con = null;
            PreparedStatement stmt = null;
            ResultSet rs = null;        
        
        try {
                con = DBConexionSQL.obtenerConexion();//ConnectionSql.obtenerConexion();
                String  query="select distinct p.PM_CodigoPrestamo as CodigoPrestamo, "+         
                    "pd.PMD_CodigoDetallePrestamo as CodigoDetallePrestamo,"   +                
                    "l.LB_CodigoLibro  as CodigoLibro,      "+
                    "  l.LB_Titulo as Libro,          "+
                    " p.AL_CodigoAlumno as CodigoAlumno,          "+
                    " p.AC_CodigoAnioAcademico as CodigoAnioAcademico,          "+
                    " aa.AC_Descripcion as Anio ,          "+
                    " tl.TL_CodigoTipoLibro as CodigoTipoLibro,          "+
                    " isnull(tl.TL_Descripcion,'')  as TipoLibro,          "+
                    " isnull(cl.CL_Descripcion,'') as Curso,        "+
                    " pd.PMD_EstadoPrestamo as  CodigoEstadoPrestamo,          "+
                    " case when pd.PMD_EstadoPrestamo = 1 then 'Devuelto' else 'En uso' end   as Estado,          "+
                    " convert(varchar(10),PMD_FechaPrestamo,103) as Fecha,       "+
                    "'S/. '+ convert(varchar(100),isnull(l.lb_PrecioReposicion,0)) as PrecioReposicion          "+

                    " from BL_Prestamos p              "+
                    "  inner join CO_AniosAcademicos aa on aa.AC_CodigoAnioAcademico=p.AC_CodigoAnioAcademico"                        +
                    "  inner join BL_PrestamoDetalle pd on pd.PM_CodigoPrestamo=p.PM_CodigoPrestamo     "                         +
                      " inner join BL_Libros l on l.LB_CodigoLibro=pd.LB_CodigoLibro           "+
                      " left join BL_TipoLibro tl on tl.TL_CodigoTipoLibro =l.TL_CodigoTipoLibro"         +
                      " left join BL_CursosLibros cl on cl.CL_CodigoCurso = l.CL_CodigoCurso     "  +
                     " where aa.AC_Descripcion=?  and                               "+
                      " p.AL_CodigoAlumno=?  and  l.LB_Estado=1  and l.TL_CodigoTipoLibro not in (4)  and pd.PMD_Estado=1    ";
              
                
                //String query1="select PM_CodigoPrestamo,LB_CodigoLibro from BL_PrestamoDetalle";
                System.out.println("query:"+query);
                stmt = con.prepareStatement(query);
                stmt.setInt(1, anio);
                stmt.setString(2, codigoAlumno);
                rs = stmt.executeQuery();

                while (rs.next()) {
                    //ALB_Codigo,ALB_Nombre,ALB_Anio,ALB_Thumbnail
                    bean.setCodigoAlumno(rs.getString("CodigoAlumno") );
                    bean.setCurso(rs.getString("Curso"));
                    bean.setEstado(rs.getString("Estado"));
                    bean.setLibro(rs.getString("Libro"));
                    bean.setPrecio(rs.getString("PrecioReposicion"));
                    bean.setTipoLibro(rs.getString("TipoLibro"));
                    
                    System.out.println("getCodigoAlumno:"+bean.getCodigoAlumno());
                    System.out.println("getCurso:"+bean.getCurso());
                    System.out.println("getEstado:"+bean.getEstado());
                    System.out.println("getLibro:"+bean.getLibro());
                    System.out.println("getPrecio:"+bean.getPrecio());
                    System.out.println("getTipoLibro:"+bean.getTipoLibro());
                    resultadoPrestamo.add(bean);
                }
                
                

        } catch (SQLException e) {
            e.printStackTrace();
                System.err.println(e.getMessage());                       
        } finally {
                rs.close();
               stmt.close();
              con.close();
        }   
      
       //jajaja
        /*
        for(beanPrestamo prestamo: beanPrestamo){
            if(jugador.getNumero()==numero){
                resultadoJugador.add(jugador);
            
            }
        }
        */
        return resultadoPrestamo;
    }
    

        
       public beanValidacionAlumno validaDOI(String codigoAlumno) throws Exception{
        
           
            beanValidacionAlumno bean= new beanValidacionAlumno();

        
            Connection con = null;
            PreparedStatement stmt = null;
            ResultSet rs = null;        
        
        try {
                con = DBConexionSQL.obtenerConexion();//ConnectionSql.obtenerConexion();
                String  query=	"select distinct isnull(p.PE_NumeroDocIdentidad,'') as PE_NumeroDocIdentidad, "
                        + "isnull(p.TDI_CodigoTipoDocIdentidad,0) as  TDI_CodigoTipoDocIdentidad  from MA_Personas p  "+
                    "inner join MA_Alumnos a on a.PE_CodigoPersona=p.PE_CodigoPersona   "+
                    "left  join MA_TiposDocumentosIdentidad  di on    "+
                    "(di.TDI_CodigoTipoDocIdentidad=p.TDI_CodigoTipoDocIdentidad and di.TDI_CodigoTipoDocIdentidad in (1,2,3) )  "+
                    "where a.AL_CodigoAlumno=?   "+
                    "and p.PE_Estado=1   ";
              
                
                //String query1="select PM_CodigoPrestamo,LB_CodigoLibro from BL_PrestamoDetalle";
                System.out.println("query:"+query);
                stmt = con.prepareStatement(query);
                stmt.setString(1, codigoAlumno);
                rs = stmt.executeQuery();
                String strDoi="";
                int strTipo=0;
                
                while (rs.next()) {
                    //ALB_Codigo,ALB_Nombre,ALB_Anio,ALB_Thumbnail
                    
                    strDoi=rs.getString("PE_NumeroDocIdentidad");
                    strTipo=rs.getInt("TDI_CodigoTipoDocIdentidad");
                    System.out.println("strDoi:"+strDoi);
                    System.out.println("strTipo:"+strTipo);
                    
                    if(strTipo>0 && !strDoi.equals("")){
                        bean.setMensaje("Requisito Cumplido.Tiene su tipo de documento y número registrado");
                        bean.setCodigo("1");
                
                    }else if(strTipo==0 && strDoi.equals("")){
                    
                        bean.setMensaje("Falta especificar el tipo de documento(DNI,Carnet de Extranjería o Pasaporte) y su número.<br>                \n" +
                        "       Si ha registrado en la actualización de Datos de su hijo porfavor esperar que validen sus datos durante un lapso de 48 de horas.");
                        bean.setCodigo("0");
                    }else if(strTipo==0 ){
                    bean.setMensaje("Falta especificar el tipo de documento(DNI,Carnet de Extranjería o Pasaporte).<br>                \n" +
                        "       Si ha registrado en la actualización de Datos de su hijo porfavor esperar que validen sus datos durante un lapso de 48 de horas");
                        bean.setCodigo("0");
                    
                    }else if(strDoi.equals("")){
                      bean.setMensaje("Falta especificar el número de documento(DNI,Carnet de Extranjería o Pasaporte).<br>                \n" +
                        "       Si ha registrado en la actualización de Datos de su hijo porfavor esperar que validen sus datos durante un lapso máximo de 48 de horas");
                        bean.setCodigo("0");
                    
                    }
                    
                           System.out.println("getMensaje:"+bean.getMensaje());
                    System.out.println("getCodigo:"+bean.getCodigo());
                
                    
                }
                
                

        } catch (SQLException e) {
            e.printStackTrace();
                System.err.println(e.getMessage());                       
        } finally {
                rs.close();
               stmt.close();
              con.close();
        }  
            return bean;
}
        
        
       public beanValidacionAlumno validacionSituacionFinal(String codigoAlumno,String anioAnterior) throws Exception{
        
           
            beanValidacionAlumno bean= new beanValidacionAlumno();

        
            Connection con = null;
            PreparedStatement stmt = null;
            ResultSet rs = null;        
        
        try {
                con = DBConexionSQL.obtenerConexion();//ConnectionSql.obtenerConexion();
                String  query=	
                    "select isnull(SMF_CodigoSituacionFinalMatricula,0) as SMF_CodigoSituacionFinalMatricula from "+
                    " MA_Matriculas ma inner join CO_AniosAcademicos anio on ma.AC_CodigoAnioAcademico=anio.AC_CodigoAnioAcademico"+
                    " where AL_CodigoAlumno=? and"+
                    " anio.AC_Descripcion=?";
              
                
                //String query1="select PM_CodigoPrestamo,LB_CodigoLibro from BL_PrestamoDetalle";
                System.out.println("query:"+query);
                stmt = con.prepareStatement(query);
                stmt.setString(1, codigoAlumno);
                stmt.setString(2, anioAnterior);
                rs = stmt.executeQuery();
                int intSituacion;
                String strFlag="0";
                
                
                while (rs.next()) {
                    intSituacion=rs.getInt("SMF_CodigoSituacionFinalMatricula");
                    System.out.println("intSituacion:"+intSituacion);                    
                    
                    if(intSituacion>0 && intSituacion<8 && intSituacion!=6){ 
                        bean.setMensaje("Requisito Cumplido.Tiene situación final definido");
                        bean.setCodigo("1");
                
                    }else if(intSituacion==6){
                        bean.setMensaje("Su situación final es Requiere Recuperación.<br> Debe participar en el Programa de Recuperación Académica y esperar el resultado de los exámenes");
                        bean.setCodigo("0");
                    }else{
                        bean.setMensaje("Falta definir su Situación Final");
                        bean.setCodigo("0");
                    }
                    strFlag="1";
                    System.out.println("getMensaje:"+bean.getMensaje());
                    System.out.println("getCodigo:"+bean.getCodigo());
                
                    
                }
                if(strFlag.equals("0")){
                     bean.setMensaje("Requisito Cumplido.Tiene situación final definido'");
                     bean.setCodigo("1");
                }
                

        } catch (SQLException e) {
            e.printStackTrace();
                System.err.println(e.getMessage());                       
        } finally {
                rs.close();
               stmt.close();
              con.close();
        }  
            return bean;
}
       /*
        public static void main (String args[]) throws Exception{
        
           PrestamoDAO dao =  new PrestamoDAO();
             ArrayList<beanPrestamo> resultadoPrestamo =     dao.consultaPrestamo("20030102",2);
             System.out.println("MA_Alumnos:"+resultadoPrestamo.size());
           //beanValidacionAlumno bean=dao.validaDOI("20030102");
           dao.validacionSituacionFinal("20030102", "2012");
           System.out.println("probando ...");
           
        
        }*/
        

}
