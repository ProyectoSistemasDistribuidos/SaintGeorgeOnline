﻿Namespace ModuloMatricula

    Public Class be_SolicitudActualizacionFichaMedicaAlumno

#Region "Atributos"

        Private int_CodigoSolicitud As Integer
        Private int_CodigoPersonaSolicitante As Integer
        Private dt_FechaRegistro As Date
        Private int_EstadoSolicitud As Integer
        Private dt_FechaActualizacion As Date

#End Region

#Region "Propiedades"

        Public Property CodigoSolicitud() As Integer
            Get
                Return int_CodigoSolicitud
            End Get
            Set(ByVal value As Integer)
                int_CodigoSolicitud = value
            End Set
        End Property

        Public Property CodigoPeronsaSolicitante() As Integer
            Get
                Return int_CodigoPersonaSolicitante
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersonaSolicitante = value
            End Set
        End Property

        Public Property FechaRegistro() As Date
            Get
                Return dt_FechaRegistro
            End Get
            Set(ByVal value As Date)
                dt_FechaRegistro = value
            End Set
        End Property

        Public Property EstadoSolicitud() As Integer
            Get
                Return int_EstadoSolicitud
            End Get
            Set(ByVal value As Integer)
                int_EstadoSolicitud = value
            End Set
        End Property

        Public Property FechaActualizacion() As Date
            Get
                Return dt_FechaActualizacion
            End Get
            Set(ByVal value As Date)
                dt_FechaActualizacion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoSolicitud As Integer, _
                ByVal CodigoPersonaSolicitante As Integer, _
                ByVal FechaRegistro As Date, _
                ByVal EstadoSolicitud As Integer, _
                ByVal FechaActualizacion As Date)

            int_CodigoSolicitud = CodigoSolicitud
            int_CodigoPersonaSolicitante = CodigoPersonaSolicitante
            dt_FechaRegistro = FechaRegistro
            int_EstadoSolicitud = EstadoSolicitud
            dt_FechaActualizacion = FechaActualizacion

        End Sub

#End Region

    End Class

End Namespace
