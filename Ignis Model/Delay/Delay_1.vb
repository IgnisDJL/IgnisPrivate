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
    ''' Permet de lier un délais a un code de délais
    ''' </summary>
    ''' <remarks></remarks>
    Private idJustification As Guid

    ''' <summary>
    ''' Permet de lier un délais a une catégorie de code de délais
    ''' </summary>
    ''' <remarks></remarks>
    Private idCategorie As Guid

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

    '' *************************************************************************************************
    ''                                          Constructeur 
    '' *************************************************************************************************

    Sub New(startDelay As Date, endDelay As Date)
        Me.startDelay = startDelay
        Me.endDelay = endDelay

        '' GUID
        Me.idDelay = Guid.NewGuid()
        Me.idDailyReport = Nothing
        Me.idCategorie = Nothing
        Me.idJustification = Nothing
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
    Public Function getIdJustification() As Guid
        Return idJustification
    End Function

    Public Sub setIdJustification(idJustification As Guid)
        Me.idJustification = idJustification
    End Sub


    Function getIdCategorie() As Guid
        Return idCategorie
    End Function

    Public Sub setIdCategorie(idCategorie As Guid)
        Me.idCategorie = idCategorie
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
        Return startDelay.Subtract(endDelay)
    End Function
End Class
