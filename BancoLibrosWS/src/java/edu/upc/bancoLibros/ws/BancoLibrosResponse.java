/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package edu.upc.bancoLibros.ws;

import edu.upc.bancoLibros.model.beanPrestamo;
import java.util.ArrayList;

/**
 *
 * @author Mishishita
 */
public class BancoLibrosResponse {
    
    private ArrayList<beanPrestamo> prestamos;
    private String strMensaje;
    private String strCodigo;

    public ArrayList<beanPrestamo> getPrestamos() {
        return prestamos;
    }

    public void setPrestamos(ArrayList<beanPrestamo> prestamos) {
        this.prestamos = prestamos;
    }

    public String getStrMensaje() {
        return strMensaje;
    }

    public void setStrMensaje(String strMensaje) {
        this.strMensaje = strMensaje;
    }

    public String getStrCodigo() {
        return strCodigo;
    }

    public void setStrCodigo(String strCodigo) {
        this.strCodigo = strCodigo;
    }
    
    
    
    
}
