Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess

Public Class BL_FichaEnfermeriaTemp
    Public Function F_ActualizarEnfermeriaTmpEstado(ByVal oBE_FichaEnfermeriaTemp As BE_FichaEnfermeriaTemp, ByVal oBE_FichaEnfermeria As BE_FichaEnfermeria, _
            ByVal lstBE_EN_RelacionFichaMedicasAlergias As List(Of BE_EN_RelacionFichaMedicasAlergias), _
            ByVal lstBE_EN_RelacionFichaMedicasAlergias_Temp As List(Of BE_EN_RelacionFichaMedicasAlergias_Temp), _
            ByVal lstBE_EN_RelacionFichaMedicasCarecteristicasPiel As List(Of BE_EN_RelacionFichaMedicasCarecteristicasPiel), _
            ByVal lstBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp As List(Of BE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp), _
            ByVal lstBE_EN_RelacionFichaMedicasEnfermedades_Temp As List(Of BE_EN_RelacionFichaMedicasEnfermedades_Temp), _
            ByVal lstBE_EN_RelacionFichaMedicasEnfermedades As List(Of BE_EN_RelacionFichaMedicasEnfermedades), _
            ByVal lstBE_EN_RelacionFichaMedicasMedicamentos As List(Of BE_EN_RelacionFichaMedicasMedicamentos), _
            ByVal lstBE_EN_RelacionFichaMedicasMedicamentos_Temp As List(Of BE_EN_RelacionFichaMedicasMedicamentos_Temp), _
            ByVal lstBE_EN_RelacionFichaMedicasMotivoHospitalizacion As List(Of BE_EN_RelacionFichaMedicasMotivoHospitalizacion), _
            ByVal lstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp As List(Of BE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp), _
            ByVal lstBE_EN_RelacionFichaMedicasOperaciones As List(Of BE_EN_RelacionFichaMedicasOperaciones), _
            ByVal lstBE_EN_RelacionFichaMedicasOperaciones_Temp As List(Of BE_EN_RelacionFichaMedicasOperaciones_Temp), _
            ByVal lstBE_EN_RelacionFichaMedicasTiposControles As List(Of BE_EN_RelacionFichaMedicasTiposControles), _
            ByVal lstBE_EN_RelacionFichaMedicasTiposControles_Temp As List(Of BE_EN_RelacionFichaMedicasTiposControles_Temp), _
            ByVal lstBE_EN_RelacionFichaMedicasVacunas As List(Of BE_EN_RelacionFichaMedicasVacunas), _
            ByVal lstBE_EN_RelacionFichaMedicasVacunas_Temp As List(Of BE_EN_RelacionFichaMedicasVacunas_Temp)) As Integer


        Try
            Dim oDA_FichaEnfermeriaTemp As New DA_FichaEnfermeriaTemp
            Return oDA_FichaEnfermeriaTemp.F_ActualizarEnfermeriaTmpEstado(oBE_FichaEnfermeriaTemp, oBE_FichaEnfermeria, _
                    lstBE_EN_RelacionFichaMedicasAlergias, _
                    lstBE_EN_RelacionFichaMedicasAlergias_Temp, _
                    lstBE_EN_RelacionFichaMedicasCarecteristicasPiel, _
                    lstBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp, _
                    lstBE_EN_RelacionFichaMedicasEnfermedades_Temp, _
                    lstBE_EN_RelacionFichaMedicasEnfermedades, _
                    lstBE_EN_RelacionFichaMedicasMedicamentos, _
                    lstBE_EN_RelacionFichaMedicasMedicamentos_Temp, _
                    lstBE_EN_RelacionFichaMedicasMotivoHospitalizacion, _
                    lstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp, _
                    lstBE_EN_RelacionFichaMedicasOperaciones, _
                    lstBE_EN_RelacionFichaMedicasOperaciones_Temp, _
                    lstBE_EN_RelacionFichaMedicasTiposControles, _
                    lstBE_EN_RelacionFichaMedicasTiposControles_Temp, _
                    lstBE_EN_RelacionFichaMedicasVacunas, _
                    lstBE_EN_RelacionFichaMedicasVacunas_Temp)
        Catch ex As Exception

        End Try
    End Function
End Class
