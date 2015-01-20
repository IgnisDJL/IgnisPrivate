Namespace Constants.Input

    Public Class Events

        Public Enum EventType

            IMPORTANT = 0
            START = 1
            STOP_ = 2

        End Enum

        Public Shared ReadOnly DEFAULT_START_MESSAGE As String = "Départ"
        Public Shared ReadOnly DEFAULT_STOP_MESSAGE As String = "Arrêt"

        ''' <summary>
        ''' Regex to match and extract information from the name of events log files
        ''' </summary>
        ''' <remarks>Exemple : 20081108.log</remarks>
        Public Shared ReadOnly FILE_NAME_REGEX As String = "([\d]{4})([\d]{2})([\d]{2})\.log"

        ''' <summary>Index of date components in the name regex of log files</summary>
        Public Enum EventsFileNameRegexDateIndex
            DAY = 3
            MONTH = 2
            YEAR = 1
        End Enum

    End Class

End Namespace
