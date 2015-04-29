﻿''' <summary>
'''  La classe Delay est utilisé pour représenter les périodes d'une journée de travail où il n'y a pas de production
''' </summary>
''' <remarks>
''' La production n'est pas calculé de la même façon pour une usine en continue et une usine en discontinue
''' </remarks>

Public Class Delay_1

    ''' <summary>
    ''' Permet d'identifier de manière unique un délais dans le sysème, ainsi que dans la futur base de donnée
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private idDelay As Guid
    ''' <summary>
    ''' Date et heure du début du délais
    ''' </summary>
    ''' <remarks></remarks>
    Private startDelay As Date

    ''' <summary>
    ''' Date et heure de la fin du délais
    ''' </summary>
    ''' <remarks></remarks>
    Private endDelay As Date

    ''' <summary>
    ''' Identifiant unique d'un rapport périodique court (journalier)
    ''' </summary>
    ''' <remarks>
    ''' Ce paramètre peut être vide
    ''' </remarks>
    Private idDailyReport As Guid


    Private delayCode As Integer
    Private delayCategorie As Integer

    Private delayName As String
    Private delayDescription As String
    Private delayComment As String

    Private color As Color
    '' *************************************************************************************************
    ''                                          Constructeur 
    '' *************************************************************************************************

    Sub New(startDelay As Date, endDelay As Date)
        Me.startDelay = startDelay
        Me.endDelay = endDelay

        Me.delayCode = 0
        Me.delayCategorie = 0

        Me.delayName = String.Empty
        Me.delayDescription = String.Empty
        Me.delayComment = String.Empty
        Me.color = Drawing.Color.White

        '' GUID
        Me.idDelay = Guid.NewGuid()
        Me.idDailyReport = Nothing

    End Sub

    '' *************************************************************************************************
    ''                                          Get 
    '' *************************************************************************************************
    Public Function getStartDelay() As Date
        Return startDelay
    End Function

    Public Function getEndDelay() As Date
        Return endDelay
    End Function

    Public Function getIdDelay() As Guid
        Return idDelay
    End Function

    '' *************************************************************************************************
    ''                                          Get / Set 
    '' *************************************************************************************************
    Public Function getDelayCode() As Integer
        Return delayCode
    End Function

    Public Sub setDelayCode(delayCode As Integer)
        Me.delayCode = delayCode
    End Sub

    Public Function getColor() As Color
        Return color
    End Function

    Public Sub setColor(color As Color)
        Me.color = color
    End Sub

    Function getDelayName() As String
        Return delayName
    End Function

    Public Sub setDelayName(delayName As String)
        Me.delayName = delayName
    End Sub

    Function getDelayDescription() As String
        Return delayDescription
    End Function

    Public Sub setDelayDescription(delayDescription As String)
        Me.delayDescription = delayDescription
    End Sub

    Function getDelayComment() As String
        Return delayComment
    End Function

    Public Sub setDelayComment(delayComment As String)
        Me.delayComment = delayComment
    End Sub


    Function getDelayCategorie() As Integer
        Return delayCategorie
    End Function

    Public Sub setDelayCategorie(delayCategorie As Integer)
        Me.delayCategorie = delayCategorie
    End Sub


    Function getIdDailyReport() As Guid
        Return idDailyReport
    End Function

    Public Sub setIdDailyReport(idDailyReport As Guid)
        Me.idDailyReport = idDailyReport
    End Sub

    '' *************************************************************************************************
    ''                                      Fonction Publique
    '' *************************************************************************************************
    Public Function getDuration() As TimeSpan
        Return endDelay.Subtract(startDelay)
    End Function
End Class
