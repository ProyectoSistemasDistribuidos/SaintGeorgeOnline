/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package edu.upc.bancoLibros.model;

/**
 *
 * @author Mishishita
 */
public class beanPrestamo {
    
    private String libro;
    private String tipoLibro;
    private String curso;
    private String estado;
    private String precio;
    private String codigoAlumno;

    public String getCodigoAlumno() {
        return codigoAlumno;
    }

    public void setCodigoAlumno(String codigoAlumno) {
        this.codigoAlumno = codigoAlumno;
    }
    

    
    
    
    public String getLibro() {
        return libro;
    }

    public void setLibro(String libro) {
        this.libro = libro;
    }

    public String getTipoLibro() {
        return tipoLibro;
    }

    public void setTipoLibro(String tipoLibro) {
        this.tipoLibro = tipoLibro;
    }

    public String getCurso() {
        return curso;
    }

    public void setCurso(String curso) {
        this.curso = curso;
    }

    public String getEstado() {
        return estado;
    }

    public void setEstado(String estado) {
        this.estado = estado;
    }

    public String getPrecio() {
        return precio;
    }

    public void setPrecio(String precio) {
        this.precio = precio;
    }
    
    
    
    
    
    
}
