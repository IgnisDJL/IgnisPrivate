Imports System.Text.RegularExpressions

Public Class EventsFile
    Inherits DataFile

    ' Constants
    Private EVENTS_FILES_CULTURE As Globalization.CultureInfo = Globalization.CultureInfo.CreateSpecificCulture("en-US")

    ' #language
    Public Shared ReadOnly GENERIC_NAME As String = "Fichier d'évenements (.log)"

    ' Attributes
    Private _date As Date

    ' Events
    Public Event AnalysisStartedEvent(eventsFile As EventsFile)
    Public Event AnalysisProgress(current As Integer, total As Integer)
    Public Event AnalysisStopedEvent(eventsFile As EventsFile)

    Public Sub New(pathToEventsLOGFile As String)

        MyBase.New(pathToEventsLOGFile)
        Me._date = getDateFromFileName(Me.getFileInfo.Name)

    End Sub

    Public Function getEvents() As EventsCollection

        Dim defaultCulture = Threading.Thread.CurrentThread.CurrentCulture
        Threading.Thread.CurrentThread.CurrentCulture = EVENTS_FILES_CULTURE

        RaiseEvent AnalysisStartedEvent(Me)

        Dim eventsCollection As New EventsCollection

        Dim readingStream As System.IO.StreamReader

        Try

            ' Open a stream to the file
            readingStream = New System.IO.StreamReader(Me.getFileInfo.FullName)

            Dim fileLines = readingStream.ReadToEnd.Split(Environment.NewLine)

            readingStream.Close()

            Dim nbLinesToAnalyse As Integer = fileLines.Length - 2

            ' -2 because last line is useless
            For i = 0 To nbLinesToAnalyse

                RaiseEvent AnalysisProgress(i, nbLinesToAnalyse)

                Dim splitLine = fileLines(i).Split(";")

                eventsCollection.addEventFromLog(CDbl(splitLine(0)), splitLine(1))

            Next

        Catch ex As IO.IOException

            ' #exception
            Debugger.Break()

        Catch ex As Threading.ThreadAbortException

            If (Not IsNothing(readingStream)) Then
                readingStream.Close()
            End If

            Return Nothing

        End Try

        RaiseEvent AnalysisStopedEvent(Me)

        Threading.Thread.CurrentThread.CurrentCulture = defaultCulture

        Return eventsCollection

    End Function

    Public Overrides ReadOnly Property Date_ As Date
        Get
            Return Me._date
        End Get
    End Property

    ''' <summary>
    ''' Returns the date of the csv file based on the file name
    ''' </summary>
    ''' <param name="fileName">The csv file name. The short name with the extension, not the full path</param>
    Public Shared Function getDateFromFileName(fileName As String) As Date

        Dim regex As New Regex(Constants.Input.Events.FILE_NAME_REGEX)
        Dim match As Match = regex.Match(fileName)

        If (match.Success) Then

            Dim day As Integer = Integer.Parse(match.Groups(Constants.Input.Events.EventsFileNameRegexDateIndex.DAY).Value)
            Dim month As Integer = Integer.Parse(match.Groups(Constants.Input.Events.EventsFileNameRegexDateIndex.MONTH).Value)
            Dim year As Integer = Integer.Parse(match.Groups(Constants.Input.Events.EventsFileNameRegexDateIndex.YEAR).Value)

            Return New Date(year, month, day)

        Else

            ' #exception

            Return Nothing
        End If

    End Function

End Class
