/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package edu.upc.bancoLibros.conexion;

import java.sql.Connection;
import java.sql.DriverManager;

/**
 *
 * @author Mishishita
 */
public class DBConexionSQL {
    
    
    public DBConexionSQL(){}
    
     public static Connection obtenerConexion() throws Exception {

         Connection con = null;
        try {
          Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
          String connectionUrl = "jdbc:sqlserver://localhost:1434;databaseName=BANCO_LIBROS;user=sa;password=sql;";
          con =  DriverManager.getConnection(connectionUrl);
        } catch (Exception e) {
            System.out.println("SQL Exception: "+ e.toString());
        } 
                return con;
        }
     
     
     
    
}
