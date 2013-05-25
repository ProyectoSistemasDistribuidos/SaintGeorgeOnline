/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package edu.upc.bancoLibros.ws;

import edu.upc.bancoLibros.model.beanPrestamo;
import edu.upc.bancoLibros.model.beanValidacionAlumno;
import edu.upc.bancoLibros.service.PrestamoLibroService;
import java.util.ArrayList;
import javax.jws.WebService;
import javax.jws.WebMethod;
import javax.jws.WebParam;

/**
 *
 * @author Mishishita
 */
@WebService(serviceName = "BancoLibrosWebService")
public class BancoLibrosWebService {
    
    PrestamoLibroService servicio= new PrestamoLibroService();

    /**
     * Web service operation
     */
    @WebMethod(operationName = "consultaPrestamo")
    public BancoLibrosResponse consultaPrestamo(@WebParam(name = "codigoAlumno") String codigoAlumno, @WebParam(name = "anio") int anio) throws Exception {
        BancoLibrosResponse response = new BancoLibrosResponse();
        
        ArrayList<beanPrestamo> prestamos= new ArrayList<beanPrestamo>();
        prestamos=servicio.consultaPrestamo(codigoAlumno, anio);
        response.setPrestamos(prestamos);
        
        
        //TODO write your implementation code here:
        return response;
    }

    /**
     * Web service operation
     */
    @WebMethod(operationName = "validaDOI")
    public BancoLibrosResponse validaDOI(@WebParam(name = "codigoAlumno") String codigoAlumno) throws Exception {
       BancoLibrosResponse response = new BancoLibrosResponse();
        
        beanValidacionAlumno validar= new beanValidacionAlumno();
        validar=servicio.validaDOI(codigoAlumno);
        response.setStrCodigo(validar.getCodigo());
        response.setStrMensaje(validar.getMensaje());
        
        
        //TODO write your implementation code here:
        return response;
    }

    /**
     * Web service operation
     */
    @WebMethod(operationName = "validacionSituacionFinal")
    public BancoLibrosResponse validacionSituacionFinal(@WebParam(name = "codigoAlumno") String codigoAlumno, @WebParam(name = "anioAnterior") String anioAnterior) throws Exception {
        BancoLibrosResponse response = new BancoLibrosResponse();
        
        beanValidacionAlumno validar= new beanValidacionAlumno();
        validar=servicio.validacionSituacionFinal(codigoAlumno, anioAnterior);
        response.setStrCodigo(validar.getCodigo());
        response.setStrMensaje(validar.getMensaje());
        
        
        //TODO write your implementation code here:
        return response;
    }
}
