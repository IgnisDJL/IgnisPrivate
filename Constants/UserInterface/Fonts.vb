Namespace Constants.UI

    Public Class Fonts

        Public Shared ReadOnly DEFAULT_FONT_FAMILY As String = "Calibri"

        Public Shared ReadOnly DEFAULT_FONT As Font = New Font(DEFAULT_FONT_FAMILY, 14)

        ' Variants
        Public Shared ReadOnly BIGGER_DEFAULT_FONT As Font = New Font(DEFAULT_FONT_FAMILY, 15)

        Public Shared ReadOnly BIGGER_DEFAULT_FONT_BOLD As Font = New Font(DEFAULT_FONT_FAMILY, 15, FontStyle.Bold)

        Public Shared ReadOnly SMALL_DEFAULT_FONT As Font = New Font(DEFAULT_FONT_FAMILY, 12)

        Public Shared ReadOnly SMALLER_DEFAULT_FONT As Font = New Font(DEFAULT_FONT_FAMILY, 11)

        Public Shared ReadOnly SMALLEST_DEFAULT_FONT As Font = New Font(DEFAULT_FONT_FAMILY, 9)

        Public Shared ReadOnly DEFAULT_FONT_BOLD As Font = New Font(DEFAULT_FONT_FAMILY, 14, FontStyle.Bold)

        Public Shared ReadOnly DEFAULT_FONT_UNDERLINED As Font = New Font(DEFAULT_FONT_FAMILY, 14, FontStyle.Underline)

        Public Shared ReadOnly DEFAULT_FONT_ITALIC As Font = New Font(DEFAULT_FONT_FAMILY, 14, FontStyle.Italic)

    End Class

End Namespace
