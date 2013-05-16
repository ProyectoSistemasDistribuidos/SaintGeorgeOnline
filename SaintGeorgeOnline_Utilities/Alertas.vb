Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Collections.Generic
Imports SaintGeorgeOnline_Utilities

Public Class Alertas
    Inherits System.Web.UI.Page

#Region "Sexy Alert Box Alerts"

    ''' <summary>
    ''' Obtiene la estructura y contenido del mensaje a mostrar.
    ''' </summary>
    ''' <param name="str_Mensaje">Mensaje a mostrar</param>
    ''' <param name="str_TipoMensaje">Tipo de mensaje referente al icono a mostrar</param>
    ''' <returns>Retorna la estructura en JQuery de los mensajes y tipos de mensajes recibidos por el formulario quien lo invoque.</returns>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    'Public Shared Function ObtenerMensaje(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String) As String
    '    Dim str_Script As String = ""
    '    Select Case str_TipoMensaje
    '        Case "Alert"
    '            str_Script = "Sexy.alert('<br />" & str_Mensaje & "');"
    '        Case "Info"
    '            str_Script = "Sexy.info('<br />" & str_Mensaje & "');"
    '        Case "Error"
    '            str_Script = "Sexy.error('<br />" & str_Mensaje & "');"
    '    End Select
    '    Return str_Script
    'End Function

#End Region

#Region "Mensajes de Alertas"

    ''' <summary>
    ''' Obtiene la descripción de la alerta (multiples validaciones a la vez) a mostrar en las validaciones del formulario.
    ''' </summary>
    ''' <param name="str_Cadena">Cadena de caracteres, la cual contiene los mensajes de validaciones a enviar</param>
    ''' <param name="str_TipoAlerta">Tipo de Alerta (Referido al tipo de mensaje a agregar)</param>
    ''' <param name="str_NombreCampo">Nombre del campo que genera la alerta</param>
    ''' <returns>Estructura del mesaje de validación a mostrar en el formulario</returns>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ObtenerAlerta(ByVal str_Cadena As String, ByVal str_TipoAlerta As Integer, ByVal str_NombreCampo As String) As String
        Dim str_MensajeAlerta As String = str_Cadena

        If str_MensajeAlerta.Trim.Length = 0 Then

            str_MensajeAlerta = "Los siguientes campos presentan errores:"
            str_MensajeAlerta += "<ul>"

        End If

        str_MensajeAlerta = Replace(str_MensajeAlerta, "</ul>", "") ' Si la lista de errores tiene mas de 1 item elimino el cierre de la lista

        Select Case str_TipoAlerta
            Case 1 ' Sin descripcion
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> requiere información. No puede estar vacia.</li>"
            Case 2 ' Palabras con mas de 50 caracteres seguidos
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. No puede ingresar palabras que tengan más de 50 caracteres seguidos.</li>"
            Case 3 ' Elije "Seleccione" de una combobox
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> requiere información. Debe seleccionar un item valido.</li>"
            Case 4 ' Fecha de Atención mayor a fecha actual
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La fecha de atención no puede ser mayor a la fecha actual.</li>"
            Case 5 ' Hora A mayor que hora B
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La hora de ingreso no puede ser mayor a la hora de salida.</li>"
            Case 6 ' Fecha con formato incorrecto
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. El formato de la fecha es incorrecto.</li>"
            Case 7 ' Fecha inicial mayor a fecha final
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La fecha inicial no puede ser mayor a la fecha final.</li>"
            Case 8 ' Fecha de xxx mayor a la fecha actual
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La " & str_NombreCampo & " no puede ser mayor a la fecha actual.</li>"
            Case 9 ' DropDownList Nacionalidad con el mismo valor
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La " & str_NombreCampo & " no se puede repetir la Nacionalidad.</li>"
            Case 10 ' DropDownList Lengua Materna con el mismo valor
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La " & str_NombreCampo & " no se puede repetir la Lengua Materna.</li>"
            Case 11 ' Correo electronico invalido
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. El " & str_NombreCampo & " debe ser valido.</li>"
            Case 12 ' No selecciono ningun item de la lista (checkbox)
                str_MensajeAlerta += "<li><em>Lista de " & str_NombreCampo & "</em> requiere almenos 1 item seleccionado.</li>"
            Case 13 '  No puede ser menor que 2000
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. No puede ser menor que 2000.</li>"
            Case 14 ' No se pueden repetir datos
                str_MensajeAlerta += "<li>El dato <em>" & str_NombreCampo & "</em> no se puede repetir.</li>"
            Case 15 ' No se puede aver el grupo "Todos" y otro grupo "XYZ"
                str_MensajeAlerta += "<li>El dato <em>" & str_NombreCampo & "</em> no puede tener el grupo <em>Todos</em> y otro grupo diferente en el mismo Salon.</li>"
            Case 16 ' Debe seleccionar 1 archivo para cargar
                str_MensajeAlerta += "<li>No se ha seleccionado ningún <em> archivo de " & str_NombreCampo & "</em> para leer.</li>"
            Case 17 ' Debe seleccionar realizar los pagos de las deudas respetando el orden de las fechas de vencimiento
                str_MensajeAlerta += "<li>No se ha podido realizar el pago de la <em>" & str_NombreCampo & "</em>, ya que el alumno(a) presenta una deuda anterior pendiente.</li>"
            Case 18 ' Las boletas emitidas tienen diferente fecha de vencimiento
                str_MensajeAlerta += "<li>Las <em>" & str_NombreCampo & "</em> tienen diferente fecha de vencimiento.</li>"
            Case 19 ' Uno de los conceptos ya tiene boleta emitida
                str_MensajeAlerta += "<li>Uno de los conceptos ya tiene <em>" & str_NombreCampo & "</em>.</li>"
            Case 20 ' Correlativa de Talonarios
                str_MensajeAlerta += "<li><em>El Número Correlativo de la " & str_NombreCampo & "</em> es incorrecto(requiere 7 digitos).</li>"
            Case 21 'Debe de ingresar al menos un registro en el detalle de Compromiso de Pago
                str_MensajeAlerta += "<li><em>No se ha registrado en la lista " & str_NombreCampo & "</em>.</li>"
            Case 22 ' Fecha de Pago no puede ser  menor a fecha actual
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. No puede ser menor a la fecha actual.</li>"
            Case 23 ' Fecha de Pago no puede ser  menor a la fecha de vencimiento
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. No puede ser menor a la fecha de vencimiento.</li>"
            Case 24 ' Debe seleccionar el check de un casillero
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. Debe dar check al casillero.</li>"
            Case 25 ' El numero de Talonario no puede ser menor al actual
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. No puede ser menor al Número Correlativo actual.</li>"
            Case 26 ' Monto mayor a 0
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecto. Debe ser mayor a 0.</li>"
            Case 27 ' Palabras con menos de 2 caracteres
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. No puede ingresar palabras que tengan menos de 2 caracteres .</li>"
            Case 28 ' Monto Actualizado no puede ser mayor al Monto Original
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecto. El <em>" & str_NombreCampo & "</em> actual no puede ser mayor al Monto original.</li>"
            Case 29 ' Fecha de Emisión no puede ser  menor a la Fecha de Pago
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. No puede ser menor a la Fecha de Pago.</li>"
            Case 30 ' Debe buscar 1 Pago para grabar una Nota de Crédito
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecto. Debe buscar un <em>" & str_NombreCampo & "</em> para poder grabar una Nota de Crédito.</li>"
            Case 31 ' El monto de la Nota de Crédito no puede ser mayor al monto del Pago Original
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecto. El <em>" & str_NombreCampo & " no puede ser mayor al monto del Pago Original.</li>"
            Case 32 ' El mes de inicio no puede ser mayor al mes de fin
                str_MensajeAlerta += "<li>Selección de <em>" & str_NombreCampo & "</em> incorrecta. El mes de <em>Inicio</em> no puede ser mayor al mes de <em>Fin</em>.</li>"
            Case 33 ' La diferencia entre las letras debe de ser de almenos 30 dias
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La diferencia de días entre las fechas no puede ser menor a 30 días.</li>"
            Case 34 ' No se puede ingresar fechas fuera del rango de las fechas de la asignacion de bimestres
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La fecha no se encuentra en el intervalo de Bimestre asignado.</li>"
            Case 35 ' No se puede asignar un mismo libro que coincida en el rango de fecha asignada anteriormente.
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La asignaciòn de un mismo libro no debe encontrarse en el intervalo del otro.</li>"
            Case 36 ' Las fecha de un bimestre no puede ser menor a la del bimestre que la precede.
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La <em>" & str_NombreCampo & "</em> de un bimestre no puede ser menor a la del bimestre que la precede.</li>"
            Case 37 ' La fecha que se ingreso ya esta contenida en otro registro
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La <em>" & str_NombreCampo & "</em> No se puede ingresar, cuando se encuentra contenida en otro registro.</li>"
            Case 38 'Talla formato de ingreso no es correcto. Ejm: 1.56.
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. El formato de ingreso no es correcto. Ejm: 1.56 </li>"
            Case 39 'Peso El punto decimal no es correcto.Ejm: 50.35 
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. El punto decimal no es correcto.</li>"
            Case 40 'Edad ,la edad no puede ser mayor a 30
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. La <em>" & str_NombreCampo & "</em>  No puede ser mayor a 30 años</li>"
            Case 41 'talla
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. El formato de ingreso no es correcto </li>"
            Case 42 'Peso
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. No se encuentra en el rango de pesos permitidos </li>"
            Case 43 'No existe criterio ni calificativo
                str_MensajeAlerta += "<li>No se presentan registros de <em>" & str_NombreCampo & "</em> para el aula seleccionada.</li>"
            Case 44 'No adjunto el documento
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em>Falta adjuntar .</li>"
            Case 45 ' Archivo adjunto no puede pasar de 1 MB
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em>El Documento no puede pasar de 1 MB.</li>"
            Case 46 ' Periodo de xxx mayor que el final
                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. El " & str_NombreCampo & " no puede ser mayor al final.</li>"
        End Select

        str_MensajeAlerta += "</ul>" ' Agrego el cierre de la lista

        Return str_MensajeAlerta
    End Function

#End Region

#Region "Mensajes de Error"

    Public Shared Function EnviarMensajeErrorEmail(ByVal int_Modulo As Integer, ByVal int_Opcion As Integer, ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String, ByVal str_nombreUsuario As String, ByVal int_codigotipoUsuario As Integer) As String

        Dim str_TituloError As String = ""
        Dim str_DescripcionError As String = ""
        Dim str_NombreModulo As String = ""
        Dim str_tipoUsuario As String = ""

        'DEFINICION DE TIPO DE USUARIO
        Select Case int_codigotipoUsuario
            Case 0
                str_tipoUsuario = "Sin definir"
            Case 1
                str_tipoUsuario = "Alumno"
            Case 2
                str_tipoUsuario = "Trabajador"
            Case 3
                str_tipoUsuario = "Familia"
        End Select

        'DEFINICION DE TIPO DE ERROR
        Select Case int_CodigoAccion
            Case 0 'load
                str_DescripcionError = "Error al Cargar datos del Formulario."
            Case 1 'Grabar
                str_DescripcionError = "Error al Registrar."
            Case 2 'Activar
                str_DescripcionError = "Error al activar."
            Case 3 'Eliminar
                str_DescripcionError = "Error al eliminar."
            Case 4 'Exportar
                str_DescripcionError = "Error al exportar."
            Case 5 'Visualizar
                str_DescripcionError = "Error al Visualizar."
            Case 6 'Actualizar
                str_DescripcionError = "Error al Actualizar."
            Case 7 'Nuevo
                str_DescripcionError = "Error al Nuevo."
            Case 8 'Buscar
                str_DescripcionError = "Error al Buscar."
            Case 9 'Imprimir
                str_DescripcionError = "Error al Imprimir."
            Case 10 'Validar
                str_DescripcionError = "Error al Validar."
            Case 111 'Cambio de Página
                str_DescripcionError = "Error al Cambiar de página."
            Case 112 'Ordenar Listado de Página
                str_DescripcionError = "Error al ordenar el resultado de la búsqueda."
            Case 113 'Agregar registro en grilla
                str_DescripcionError = "Error al agregar el registro solicitado."
            Case 114 'Generar Contraseña
                str_DescripcionError = "Error al generar la contraseña solicitada."
            Case 115 'Enviar Contraseña
                str_DescripcionError = "Error al enviar la contraseña solicitada."
            Case 116 'PASO 2 Matricula
                str_DescripcionError = "Error al cargar los datos del paso 2 de la Matrícula."
            Case 117 'PASO 3 Matricula
                str_DescripcionError = "Error al cargar los datos del paso 3 de la Matrícula."
            Case 118 'PASO 4 Matricula
                str_DescripcionError = "Error al cargar los datos del paso 4 de la Matrícula."
            Case 119 'PASO 5 Matricula
                str_DescripcionError = "Error al cargar los datos del paso 5 de la Matrícula."
            Case 120 'PASO 6 Matricula
                str_DescripcionError = "Error al cargar los datos del paso 6 de la Matrícula."
            Case 121 'PASO 7 Matricula
                str_DescripcionError = "Error al cargar los datos del paso 7 de la Matrícula."
            Case 122 'TERMINAR Matricula
                str_DescripcionError = "Error al registrar la mátricula."
            Case 200 'Agregar 1 Registro al detalle (Temporal)
                str_DescripcionError = "Error al Agregar un registro a detalle."
            Case 201 'Actualizar 1 Registro del detalle (Temporal)
                str_DescripcionError = "Error al Actualizar un registro de detalle."
            Case 202 'Eliminar 1 Registro del detalle (Temporal)
                str_DescripcionError = "Error al Eliminar un registro de detalle."
            Case 203 'Listar Datos de detalle (Temporal)
                str_DescripcionError = "Error al Listar los registros del detalle."
            Case 204 'Cargar Datos de detalle (Temporal)
                str_DescripcionError = "Error al Cargar los registros del detalle al Formulario."
            Case 205 'Mostrar Ficha Medica en el formulario de Atencion
                str_DescripcionError = "Error de obteción de informacion de la Ficha de Atención, comuniquese con el área de sistemas."
        End Select

        'DEFINICION DE NOMBRE DE OPCION Y MODULO
        Select Case int_Modulo
            Case 0
                str_NombreModulo = "Modulo de Logueo y Opciones Extras"

                Select Case int_Opcion
                    Case 0
                        str_TituloError = "Loguin"
                    Case 1
                        str_TituloError = "Interfaz Trabajador"
                    Case 2
                        str_TituloError = "Interfaz Padre"
                    Case 3
                        str_TituloError = "Interfaz Alumno"
                    Case 4
                        str_TituloError = "Olvido Contraseña"
                    Case 5
                        str_TituloError = "MasterPage_Trabajadores"
                    Case 6
                        str_TituloError = "MasterPage_Familiares"
                    Case 7
                        str_TituloError = "PopUp - Buscar Persona"
                    Case 8
                        str_TituloError = "Principal_Familiares"
                    Case 9
                        str_TituloError = "PopUp - Ingreso de Alergias"
                    Case 10
                        str_TituloError = "PopUp - Ingreso de Vacunas"

                    Case 11
                        str_TituloError = "Solicitud Actualización Datos Ficha Familiares"
                    Case 12
                        str_TituloError = "Solicitud Actualización Datos Ficha Alumnos"
                    Case 13
                        str_TituloError = "Solicitud Actualización Datos Ficha Médica de Alumnos"
                    Case 14
                        str_TituloError = "Reporte de Accesos"

                End Select

            Case 1

                Select Case int_Opcion

                    Case 1
                        str_TituloError = "Ficha Médica del Alumno"
                    Case 2
                        str_TituloError = "Ficha de Atención Médica"
                    Case 3
                        str_TituloError = "Consulta de Datos de Ficha Médica del Alumno"
                    Case 4
                        str_TituloError = "Reportes de Enfermeria"
                    Case 5
                        str_TituloError = "Configuración de Enfermeria"
                    Case 6
                        str_TituloError = "Configuración de Stocks minimos de medicamentos"
                    Case 7
                        str_TituloError = "Diagnósticos"
                    Case 8
                        str_TituloError = "Procedimientos de enfermería"
                    Case 9
                        str_TituloError = "Indicaciones Médicas"
                    Case 10
                        str_TituloError = "Presentaciones  Médicas"
                    Case 11
                        str_TituloError = "Unidades de médidas de medicamentos"
                    Case 12
                        str_TituloError = "Nombres de medicamentos"
                    Case 13
                        str_TituloError = "Enfermedades"
                    Case 14
                        str_TituloError = "Vacunas"
                    Case 15
                        str_TituloError = "Dosis de Vacunas"
                    Case 16
                        str_TituloError = "Tipos de Alergias"
                    Case 17
                        str_TituloError = "Características de la piel"
                    Case 18
                        str_TituloError = "Frecuencia de uso de medicamentos"
                    Case 19
                        str_TituloError = "Controles de salud"
                    Case 20
                        str_TituloError = "Motivos de hospitalización"
                    Case 21
                        str_TituloError = "Operaciones médicas"
                    Case 22
                        str_TituloError = "Tipos de Sangre"
                    Case 23
                        str_TituloError = "Tipos de Nacimiento"
                    Case 25
                        str_TituloError = "Motivo de salida"
                    Case 26
                        str_TituloError = "Medicamentos"
                    Case 46
                        str_TituloError = "Ingresos y Salidas de Medicamentos"
                    Case 51
                        str_TituloError = "Alergias"
                    Case 59
                        str_TituloError = "Validación de Actualización de Ficha Médica"
                    Case 60
                        str_TituloError = "Asignar Unidades de Medidas a Presentaciones de Medicamentos"
                End Select

            Case 2

                Select Case int_Opcion

                    Case 27
                        str_TituloError = "Configuración de Matrícula"
                    Case 28
                        str_TituloError = "Houses"
                    Case 29
                        str_TituloError = "Paises"
                    Case 30
                        str_TituloError = "Parentescos"
                    Case 31
                        str_TituloError = "Profesiones"
                    Case 32
                        str_TituloError = "Servicios de Radio"
                    Case 33
                        str_TituloError = "Situaciones Laborales"
                    Case 34
                        str_TituloError = "Tipos de Discapacidades"
                    Case 35
                        str_TituloError = "Paises Ministerio"
                    Case 36
                        str_TituloError = "Niveles de Instruccion"
                    Case 37
                        str_TituloError = "Escolaridades Ministerio"
                    Case 41
                        str_TituloError = "Religiones"
                    Case 42
                        str_TituloError = "Clinicas"
                    Case 43
                        str_TituloError = "Tipos de documentos de identidad"
                    Case 44
                        str_TituloError = "Tipos de Seguro"
                    Case 47
                        str_TituloError = "Ficha de Alumnos"
                    Case 48
                        str_TituloError = "Ficha de Familiares"
                    Case 49
                        str_TituloError = "Estados Civiles"
                    Case 50
                        str_TituloError = "Idiomas"
                    Case 57
                        str_TituloError = "Validación Actualización de Ficha Familiares"
                    Case 58
                        str_TituloError = "Validación Actualización de Ficha Alumnos"
                    Case 61
                        str_TituloError = "Idiomas Ministerio"
                    Case 63
                        str_TituloError = "Ficha de Familia"
                    Case 62
                        str_TituloError = "Nacionalidades"
                    Case 75
                        str_TituloError = "Ambientes"
                    Case 76
                        str_TituloError = "Tipo de Ambientes"
                    Case 77
                        str_TituloError = "Pabellones"
                    Case 78
                        str_TituloError = "Pisos"
                End Select
            Case 3

                Select Case int_Opcion

                    Case 38
                        str_TituloError = "Configuración de Menú"
                    Case 39
                        str_TituloError = "Bloques de Menus"
                    Case 40
                        str_TituloError = "Sub Bloques de Menus"
                    Case 45
                        str_TituloError = "Bloqueo de Modificación de Fichas de Atenciones Medicas"
                    Case 55
                        str_TituloError = "Asignación de Perfiles a Personas"
                    Case 53
                        str_TituloError = "Perfiles"
                    Case 54
                        str_TituloError = "Nombres de Acciones"
                    Case 56
                        str_TituloError = "Asignación de Permisos a Perfiles"
                End Select

            Case 4

                Select Case int_Opcion

                    Case 64
                        str_TituloError = "Datos de la Familia"
                    Case 66
                        str_TituloError = "Cronogramas de Pagos"
                    Case 71
                        str_TituloError = "Consulta de Circulares"
                    Case 72
                        str_TituloError = "Consejo Educativo"
                    Case 73
                        str_TituloError = "Solicitud de Actualización de Ficha de Familiares"
                    Case 74
                        str_TituloError = "Matricula"
                End Select

            Case 5

                Select Case int_Opcion

                    Case 65
                        str_TituloError = "Datos de Hijos - Consulta"
                    Case 67
                        str_TituloError = "Libreta de Notas"
                    Case 68
                        str_TituloError = "Compañeros de Aula"
                    Case 69
                        str_TituloError = "Sylabus"
                    Case 70
                        str_TituloError = "Ficha Médica - Consulta"
                    Case 79
                        str_TituloError = "Solicitud de Actualización de Ficha de Alumnos"
                End Select

            Case 6
                Select Case int_Opcion
                    Case 80
                        str_TituloError = "Criterio de Conducta"
                End Select
            Case 7

        End Select

        Dim str_MensajeError As New StringBuilder
        Dim str_EmailError As New StringBuilder

        'Estructura de Mensaje a mostrar
        str_MensajeError.Append("<h1>" & str_TituloError & "</h1><br>")
        str_MensajeError.Append(str_DescripcionError)

        'Estructura de Email a enviar
        str_EmailError.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: solid 0x red; width: 600px;'>")
        str_EmailError.Append("<tr><td><br /></td><tr>")

        str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Módulo:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & str_NombreModulo & "</td><tr>")
        str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Opción:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & str_TituloError & "</td><tr>")
        str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Evento:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & str_DescripcionError & "</td><tr>")
        str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Descripción del error:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & str_DetalleError & "</td><tr>")
        str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Usuario:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & str_nombreUsuario & " (" & str_tipoUsuario & ")" & "</td><tr>")
        str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Fecha y Hora:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & Today.ToShortDateString & " " & Now.TimeOfDay.Hours & ":" & Now.TimeOfDay.Minutes & "</td><tr>")

        str_EmailError.Append("<tr><td><br /></td><td></td><tr>")
        str_EmailError.Append("</table>")

        Dim EmailSoporte As String()
        Dim Emails As String = "jmatta@sanjorge.edu.pe;jjvento@sanjorge.edu.pe;echang@sanjorge.edu.pe;fsalinas@sanjorge.edu.pe"

        EmailSoporte = Emails.Split(";")

        'Dim obj_Email As New EnvioEmail
        'obj_Email.SendEmail(EmailSoporte, str_EmailError.ToString, "Error de Sistema SaintGeorgeOnline")

        Return str_MensajeError.ToString
    End Function

#End Region

#Region "Sexy Alert Box Alerts"

    ''' <summary>
    ''' Obtiene la estructura y contenido del mensaje a mostrar.
    ''' </summary>
    ''' <param name="str_Mensaje">Mensaje a mostrar</param>
    ''' <param name="str_TipoMensaje">Tipo de mensaje referente al icono a mostrar</param>
    ''' <returns>Retorna la estructura en JQuery de los mensajes y tipos de mensajes recibidos por el formulario quien lo invoque.</returns>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ObtenerMensaje(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String) As String
        Dim str_Script As String = ""
        Select Case str_TipoMensaje
            Case "Alert"
                str_Script = "Sexy.alert('<br />" & str_Mensaje & "');"
            Case "Info"
                str_Script = "Sexy.info('<br />" & str_Mensaje & "');"
            Case "Error"
                str_Script = "Sexy.error('<br />" & str_Mensaje & "');"
        End Select
        Return str_Script
    End Function

#End Region

    '#Region "Mensajes de Alertas"

    '    ''' <summary>
    '    ''' Obtiene la descripción de la alerta (Solo 1 alerta de validación a la vez) a mostrar en las validaciones de los formularios.
    '    ''' </summary>
    '    ''' <param name="str_TipoAlerta">Tipo de Alerta (Referido al tipo de mensaje a mostrar)</param>
    '    ''' <param name="str_NombreCampo">Nombre del campo que genera la alerta</param>
    '    ''' <returns>Estructura del mesaje de validación a mostrar en el formulario</returns>
    '    ''' <remarks>
    '    ''' Creador:               Juan Jose
    '    ''' Fecha de Creación:     06/01/2011
    '    ''' Modificado por:        _____________
    '    ''' Fecha de modificación: _____________ 
    '    ''' </remarks>
    '    Public Shared Function ObtenerAlerta(ByVal str_TipoAlerta As Integer, ByVal str_NombreCampo As String) As String

    '        Dim str_MensajeAlerta As String = ""

    '        Select Case str_TipoAlerta
    '            Case 1 ' Sin descripcion
    '                str_MensajeAlerta = "Los siguientes campos requieren información:"
    '                str_MensajeAlerta += "<ul>"
    '                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em></li>"
    '                str_MensajeAlerta += "</ul>"
    '            Case 2 ' Palabras con mas de 50 caracteres seguidos
    '                str_MensajeAlerta = "<ul>"
    '                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. No puede ingresar palabras que tengan más de 50 caracteres seguidos.</li>"
    '                str_MensajeAlerta += "</ul>"
    '            Case 3 ' No selecciona ningun checkbox
    '                str_MensajeAlerta = "<ul>"
    '                str_MensajeAlerta += "<li>Debe marcar con check por lo menos 1 registro para poder grabar.</li>"
    '                str_MensajeAlerta += "</ul>"
    '            Case 4 ' No actualiza ningun dato
    '                str_MensajeAlerta = "<ul>"
    '                str_MensajeAlerta += "<li>Debe actualizar por lo menos 1 dato para poder grabar.</li>"
    '                str_MensajeAlerta += "</ul>"
    '        End Select

    '        Return str_MensajeAlerta

    '    End Function

    '    ''' <summary>
    '    ''' Obtiene la descripción de la alerta (multiples validaciones a la vez) a mostrar en las validaciones del formulario.
    '    ''' </summary>
    '    ''' <param name="str_Cadena">Cadena de caracteres, la cual contiene los mensajes de validaciones a enviar</param>
    '    ''' <param name="str_TipoAlerta">Tipo de Alerta (Referido al tipo de mensaje a agregar)</param>
    '    ''' <param name="str_NombreCampo">Nombre del campo que genera la alerta</param>
    '    ''' <returns>Estructura del mesaje de validación a mostrar en el formulario</returns>
    '    ''' <remarks>
    '    ''' Creador:               Juan Jose
    '    ''' Fecha de Creación:     06/01/2011
    '    ''' Modificado por:        _____________
    '    ''' Fecha de modificación: _____________ 
    '    ''' </remarks>
    '    Public Shared Function ObtenerAlerta(ByVal str_Cadena As String, ByVal str_TipoAlerta As Integer, ByVal str_NombreCampo As String) As String
    '        Dim str_MensajeAlerta As String = str_Cadena

    '        If str_MensajeAlerta.Trim.Length = 0 Then

    '            str_MensajeAlerta = "Los siguientes campos presentan errores:"
    '            str_MensajeAlerta += "<ul>"

    '        End If

    '        str_MensajeAlerta = Replace(str_MensajeAlerta, "</ul>", "") ' Si la lista de errores tiene mas de 1 item elimino el cierre de la lista

    '        Select Case str_TipoAlerta
    '            Case 1 ' Sin descripcion
    '                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> requiere información. No puede estar vacia.</li>"
    '            Case 2 ' Palabras con mas de 50 caracteres seguidos
    '                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecta. No puede ingresar palabras que tengan más de 50 caracteres seguidos.</li>"
    '            Case 3 ' Elije "Seleccione" de una combobox
    '                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> requiere información. Debe seleccionar un item valido.</li>"
    '            Case 4 ' Selecciona Clase
    '                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> requiere información. Debe seleccionar un item válido.</li>"
    '            Case 5 ' Selecciona Categoria
    '                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> requiere información. Debe seleccionar un item valido.</li"
    '            Case 6 ' Monto no mayor a 0
    '                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecto. Debe ingresar un valor mayor a 0.</li>"
    '            Case 7
    '                str_MensajeAlerta += "<li><em>La <em>" & str_NombreCampo & "</em>, no es igual a la <em>" & str_NombreCampo & "</em> de los meses seleccionados.</li>"
    '            Case 8 ' Monto no mayor a 0
    '                str_MensajeAlerta += "<li><em>" & str_NombreCampo & "</em> incorrecto. Debe ingresar un valor válido.<br />Formato: xxxxxx.xx<br />Ejemplo: 123456.12</li>"
    '        End Select

    '        str_MensajeAlerta += "</ul>" ' Agrego el cierre de la lista

    '        Return str_MensajeAlerta
    '    End Function

    '#End Region

#Region "Mensajes de Error"


    Public Shared Function ObtenerMensajaError(ByVal str_TipoError As Integer) As String

        Dim str_MensajeError As String = ""

        Select Case str_TipoError

            Case 1 ' Ficha Atencion - Grabar
                str_MensajeError = "Error al grabar la Ficha de Atención, comuniquese con el área de sistemas."

            Case 2 ' Ficha Atencion - Obtener
                str_MensajeError = "Error de obteción de informacion de la Ficha de Atención, comuniquese con el área de sistemas."

            Case 3 ' Ficha del Familiar - Grabar
                str_MensajeError = "Error al grabar la Ficha del Familiar, comuniquese con el área de sistemas."

            Case 4 ' Ficha del Familiar - Obtener
                str_MensajeError = "Error de obteción de informacion de la Ficha del Familiar, comuniquese con el área de sistemas."

            Case 5 ' Ficha del Familiar Actualizar - Grabar
                str_MensajeError = "Error al actualizar la Ficha del Familiar, comuniquese con el área de sistemas."

        End Select

        Return str_MensajeError

    End Function


    'Public Shared Function EnviarMensajeErrorEmail(ByVal int_Modulo As Integer, ByVal int_Opcion As Integer, ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String, ByVal str_nombreUsuario As String, ByVal int_codigotipoUsuario As Integer) As String

    '    Dim str_TituloError As String = ""
    '    Dim str_DescripcionError As String = ""
    '    Dim str_NombreModulo As String = ""
    '    Dim str_tipoUsuario As String = ""

    '    Select Case int_codigotipoUsuario
    '        Case 0
    '            str_tipoUsuario = "Sin definir"
    '        Case 1
    '            str_tipoUsuario = "Alumno"
    '        Case 2 'Grabar
    '            str_tipoUsuario = "Trabajador"
    '        Case 3 'Grabar
    '            str_tipoUsuario = "Familia"

    '    End Select


    '    Select Case int_CodigoAccion
    '        Case 0 'load
    '            str_DescripcionError = "Error al Cargar datos del Formulario."
    '        Case 1 'Grabar
    '            str_DescripcionError = "Error al Registrar."
    '        Case 2 'Activar
    '            str_DescripcionError = "Error al activar."
    '        Case 3 'Eliminar
    '            str_DescripcionError = "Error al eliminar."
    '        Case 4 'Exportar
    '            str_DescripcionError = "Error al exportar."
    '        Case 5 'Visualizar
    '            str_DescripcionError = "Error al Visualizar."
    '        Case 6 'Actualizar
    '            str_DescripcionError = "Error al Actualizar."
    '        Case 7 'Nuevo
    '            str_DescripcionError = "Error al Nuevo."
    '        Case 8 'Buscar
    '            str_DescripcionError = "Error al Buscar."
    '        Case 9 'Imprimir
    '            str_DescripcionError = "Error al Imprimir."
    '        Case 10 'Validar
    '            str_DescripcionError = "Error al Validar."
    '        Case 11 'Cambio de Página
    '            str_DescripcionError = "Error al cambiar de página."
    '        Case 12 'Enviar presupuesto
    '            str_DescripcionError = "Error al enviar presupuesto."

    '        Case 110 'Ordenar Listado de Página
    '            str_DescripcionError = "Error al ordenar el resultado de la búsqueda."
    '        Case 111 'Agregar registro en grilla
    '            str_DescripcionError = "Error al agregar el registro solicitado."
    '        Case 112 'Generar Contraseña
    '            str_DescripcionError = "Error al generar la contraseña solicitada."
    '        Case 113 'Enviar Contraseña
    '            str_DescripcionError = "Error al enviar la contraseña solicitada."

    '        Case 200 'Agregar 1 Registro al detalle (Temporal)
    '            str_DescripcionError = "Error al Agregar un registro a detalle."
    '        Case 201 'Actualizar 1 Registro del detalle (Temporal)
    '            str_DescripcionError = "Error al Actualizar un registro de detalle."
    '        Case 202 'Eliminar 1 Registro del detalle (Temporal)
    '            str_DescripcionError = "Error al Eliminar un registro de detalle."
    '        Case 203 'Listar Datos de detalle (Temporal)
    '            str_DescripcionError = "Error al Listar los registros del detalle."
    '        Case 204 'Cargar Datos de detalle (Temporal)
    '            str_DescripcionError = "Error al Cargar los registros del detalle al Formulario."

    '    End Select

    '    Select Case int_Modulo
    '        Case 0
    '            str_NombreModulo = "Modulo de Logueo y Opciones Extras"

    '            Select Case int_Opcion
    '                Case 0
    '                    str_TituloError = "Login"
    '                Case 1
    '                    str_TituloError = "Interfaz Trabajador"
    '                Case 2
    '                    str_TituloError = "Interfaz Padre"
    '                Case 3
    '                    str_TituloError = "Interfaz Alumno"
    '                Case 4
    '                    str_TituloError = "Olvido Contraseña"
    '                Case 5
    '                    str_TituloError = "MasterPage_Trabajadores"
    '                Case 6
    '                    str_TituloError = "MasterPage_Familiares"
    '                Case 7
    '                    str_TituloError = "PopUp - Buscar Persona"
    '                Case 8
    '                    str_TituloError = "Principal_Familiares"
    '            End Select

    '        Case 1

    '            Select Case int_Opcion

    '                Case 1
    '                    str_TituloError = "Ficha Médica del Alumno"
    '                Case 2
    '                    str_TituloError = "Ficha de Atención Médica"
    '                Case 3
    '                    str_TituloError = "Consulta de Datos de Ficha Médica del Alumno"
    '                Case 4
    '                    str_TituloError = "Reportes de Enfermeria"
    '                Case 5
    '                    str_TituloError = "Configuración de Enfermeria"
    '                Case 6
    '                    str_TituloError = "Configuración de Stocks minimos de medicamentos"
    '                Case 7
    '                    str_TituloError = "Diagnósticos"
    '                Case 8
    '                    str_TituloError = "Procedimientos de enfermería"
    '                Case 9
    '                    str_TituloError = "Indicaciones Médicas"
    '                Case 10
    '                    str_TituloError = "Presentaciones  Médicas"
    '                Case 11
    '                    str_TituloError = "Unidades de médidas de medicamentos"
    '                Case 12
    '                    str_TituloError = "Nombres de medicamentos"
    '                Case 13
    '                    str_TituloError = "Enfermedades"
    '                Case 14
    '                    str_TituloError = "Vacunas"
    '                Case 15
    '                    str_TituloError = "Dosis de Vacunas"
    '                Case 16
    '                    str_TituloError = "Tipos de Alergias"
    '                Case 17
    '                    str_TituloError = "Características de la piel"
    '                Case 18
    '                    str_TituloError = "Frecuencia de uso de medicamentos"
    '                Case 19
    '                    str_TituloError = "Controles de salud"
    '                Case 20
    '                    str_TituloError = "Motivos de hospitalización"
    '                Case 21
    '                    str_TituloError = "Operaciones médicas"
    '                Case 22
    '                    str_TituloError = "Tipos de Sangre"
    '                Case 23
    '                    str_TituloError = "Tipos de Nacimiento"
    '                Case 25
    '                    str_TituloError = "Motivo de salida"
    '                Case 26
    '                    str_TituloError = "Medicamentos"
    '                Case 46
    '                    str_TituloError = "Ingresos y Salidas de Medicamentos"
    '                Case 51
    '                    str_TituloError = "Alergias"
    '                Case 59
    '                    str_TituloError = "Validación de Actualización de Ficha Médica"
    '                Case 60
    '                    str_TituloError = "Asignar Unidades de Medidas a Presentaciones de Medicamentos"
    '            End Select

    '        Case 2

    '            Select Case int_Opcion

    '                Case 27
    '                    str_TituloError = "Configuración de Matrícula"
    '                Case 28
    '                    str_TituloError = "Houses"
    '                Case 29
    '                    str_TituloError = "Paises"
    '                Case 30
    '                    str_TituloError = "Parentescos"
    '                Case 31
    '                    str_TituloError = "Profesiones"
    '                Case 32
    '                    str_TituloError = "Servicios de Radio"
    '                Case 33
    '                    str_TituloError = "Situaciones Laborales"
    '                Case 34
    '                    str_TituloError = "Tipos de Discapacidades"
    '                Case 35
    '                    str_TituloError = "Paises Ministerio"
    '                Case 36
    '                    str_TituloError = "Niveles de Instruccion"
    '                Case 37
    '                    str_TituloError = "Escolaridades Ministerio"
    '                Case 41
    '                    str_TituloError = "Religiones"
    '                Case 42
    '                    str_TituloError = "Clinicas"
    '                Case 43
    '                    str_TituloError = "Tipos de documentos de identidad"
    '                Case 44
    '                    str_TituloError = "Tipos de Seguro"
    '                Case 47
    '                    str_TituloError = "Ficha de Alumnos"
    '                Case 48
    '                    str_TituloError = "Ficha de Familiares"
    '                Case 49
    '                    str_TituloError = "Estados Civiles"
    '                Case 50
    '                    str_TituloError = "Idiomas"
    '                Case 57
    '                    str_TituloError = "Validación Actualización de Ficha Familiares"
    '                Case 58
    '                    str_TituloError = "Validación Actualización de Ficha Alumnos"
    '                Case 61
    '                    str_TituloError = "Idiomas Ministerio"
    '                Case 63
    '                    str_TituloError = "Ficha de Familia"
    '                Case 62
    '                    str_TituloError = "Nacionalidades"
    '                Case 75
    '                    str_TituloError = "Ambientes"
    '                Case 76
    '                    str_TituloError = "Tipo de Ambientes"
    '                Case 77
    '                    str_TituloError = "Pabellones"
    '                Case 78
    '                    str_TituloError = "Pisos"
    '            End Select
    '        Case 3

    '            Select Case int_Opcion

    '                Case 38
    '                    str_TituloError = "Configuración de Menú"
    '                Case 39
    '                    str_TituloError = "Bloques de Menus"
    '                Case 40
    '                    str_TituloError = "Sub Bloques de Menus"
    '                Case 45
    '                    str_TituloError = "Bloqueo de Modificación de Fichas de Atenciones Medicas"
    '                Case 55
    '                    str_TituloError = "Asignación de Perfiles a Personas"
    '                Case 53
    '                    str_TituloError = "Perfiles"
    '                Case 54
    '                    str_TituloError = "Nombres de Acciones"
    '                Case 56
    '                    str_TituloError = "Asignación de Permisos a Perfiles"
    '            End Select

    '        Case 4

    '            Select Case int_Opcion

    '                Case 64
    '                    str_TituloError = "Datos de la Familia"
    '                Case 66
    '                    str_TituloError = "Cronogramas de Pagos"
    '                Case 71
    '                    str_TituloError = "Consulta de Circulares"
    '                Case 72
    '                    str_TituloError = "Consejo Educativo"
    '                Case 73
    '                    str_TituloError = "Solicitud de Actualización de Ficha de Familiares"
    '                Case 74
    '                    str_TituloError = "Matricula"
    '            End Select

    '        Case 5

    '            Select Case int_Opcion

    '                Case 65
    '                    str_TituloError = "Datos de Hijos - Consulta"
    '                Case 67
    '                    str_TituloError = "Libreta de Notas"
    '                Case 68
    '                    str_TituloError = "Compañeros de Aula"
    '                Case 69
    '                    str_TituloError = "Sylabus"
    '                Case 70
    '                    str_TituloError = "Ficha Médica - Consulta"
    '                Case 79
    '                    str_TituloError = "Solicitud de Actualización de Ficha de Alumnos"
    '            End Select

    '        Case 6

    '    End Select

    '    Dim str_MensajeError As New StringBuilder
    '    Dim str_EmailError As New StringBuilder

    '    'Estructura de Mensaje a mostrar
    '    str_MensajeError.Append("<h1>" & str_TituloError & "</h1><br>")
    '    str_MensajeError.Append(str_DescripcionError)

    '    'Estructura de Email a enviar
    '    str_EmailError.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: solid 0x red; width: 600px;'>")
    '    str_EmailError.Append("<tr><td><br /></td><tr>")

    '    str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Módulo:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & str_NombreModulo & "</td><tr>")
    '    str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Opción:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & str_TituloError & "</td><tr>")
    '    str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Evento:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & str_DescripcionError & "</td><tr>")
    '    str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Descripción del error:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & str_DetalleError & "</td><tr>")
    '    str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Usuario:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & str_nombreUsuario & " (" & str_tipoUsuario & ")" & "</td><tr>")
    '    str_EmailError.Append("<tr><td style='width:200px;font-weight:bold;vertical-align:top;font-size:13px;font-family:Arial, Helvetica, sans-serif;' align='left'>Fecha y Hora:&nbsp;</td><td style='font-size:12px;font-family:Arial, Helvetica, sans-serif;'>" & Today.ToShortDateString & " " & Today.ToShortTimeString & "</td><tr>")

    '    str_EmailError.Append("<tr><td><br /></td><td></td><tr>")
    '    str_EmailError.Append("</table>")

    '    Dim obj_Email As New EnvioEmail
    '    obj_Email.SendEmail("jjvento@sanjorge.edu.pe", str_EmailError.ToString, "Error de Sistema SaintGeorgeOnline")

    '    Return str_MensajeError.ToString
    'End Function

#End Region


#Region "Asuntos de Email"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="str_TipoAlerta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObtenerAsuntoEmail(ByVal str_TipoAlerta As Integer) As String

        Dim str_MensajeAsunto As String = ""

        Select Case str_TipoAlerta

            Case 1 ' Error de grabacion
                str_MensajeAsunto = "Error de grabacion."

            Case 2 ' Error de consulta
                str_MensajeAsunto = "Error de consulta."

        End Select

        Return str_MensajeAsunto

    End Function

#End Region

End Class
