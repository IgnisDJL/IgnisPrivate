''' <summary>
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



    Private delayCategorieName As String
    Private delayCode As String
    Private delayDescription As String
    Private delayJustification As String

    Private color As Color
    '' *************************************************************************************************
    ''                                          Constructeur 
    '' *************************************************************************************************

    Sub New(startDelay As Date, endDelay As Date)
        Me.startDelay = startDelay
        Me.endDelay = endDelay

        Me.delayCode = String.Empty
        Me.delayCategorieName = String.Empty
        Me.delayDescription = String.Empty
        Me.delayJustification = String.Empty
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
    Public Function getDelayCode() As String
        Return delayCode
    End Function

    Public Sub setDelayCode(delayCode As String)
        Me.delayCode = delayCode
    End Sub

    Public Function getColor() As Color
        Return color
    End Function

    Public Sub setColor(color As Color)
        Me.color = color
    End Sub

    'Function getDelayName() As String
    '    Return delayName
    'End Function

    'Public Sub setDelayName(delayName As String)
    '    Me.delayName = delayName
    'End Sub

    Function getDelayDescription() As String
        Return delayDescription
    End Function

    Public Sub setDelayDescription(delayDescription As String)
        Me.delayDescription = delayDescription
    End Sub

    Function getDelayJustification() As String
        Return delayJustification
    End Function

    Public Sub setDelayJustification(delayJustification As String)
        Me.delayJustification = delayJustification
    End Sub

    Function getDelayCategorieName() As String
        Return delayCategorieName
    End Function

    Public Sub setDelayCategorieName(delayCategorieName As String)
        Me.delayCategorieName = delayCategorieName
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
