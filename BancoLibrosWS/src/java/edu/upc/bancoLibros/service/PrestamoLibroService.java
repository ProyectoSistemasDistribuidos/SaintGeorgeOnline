/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package edu.upc.bancoLibros.service;

import edu.upc.bancoLibros.dao.PrestamoDAO;
import edu.upc.bancoLibros.model.beanPrestamo;
import edu.upc.bancoLibros.model.beanValidacionAlumno;
import java.util.ArrayList;

/**
 *
 * @author Mishishita
 */
public class PrestamoLibroService {
    
    PrestamoDAO dao = new PrestamoDAO();
    
    public ArrayList<beanPrestamo> consultaPrestamo(String codigoAlumno,int anio) throws Exception {
        return dao.consultaPrestamo(codigoAlumno, anio);
    }
    
    public beanValidacionAlumno validaDOI(String codigoAlumno) throws Exception{
        return dao.validaDOI(codigoAlumno);
    }
    
    public beanValidacionAlumno validacionSituacionFinal(String codigoAlumno,String anioAnterior) throws Exception{
        return dao.validacionSituacionFinal(codigoAlumno, anioAnterior);
    }
}
