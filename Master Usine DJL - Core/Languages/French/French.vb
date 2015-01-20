Public Class French
    Implements Language

    Public Sub New()
        Me.cultureInfo.NumberFormat.NumberGroupSeparator = " "
    End Sub

    Private cultureInfo As Globalization.CultureInfo = Globalization.CultureInfo.CreateSpecificCulture("fr-FR")
    Public ReadOnly Property Culture As Globalization.CultureInfo Implements Language.Culture
        Get
            Return cultureInfo
        End Get
    End Property

    Public ReadOnly Property DisplayName As String Implements Language.DisplayName
        Get
            Return "Français"
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me.DisplayName
    End Function

    Private excelReport_ As New ExcelReport_fr
    Public ReadOnly Property ExcelReport As ExcelReport Implements Language.ExcelReport
        Get
            Return excelReport_
        End Get
    End Property

    Private userInterface_ As New UserInterface_fr
    Public ReadOnly Property UserInterface As UserInterface Implements Language.UserInterface
        Get
            Return userInterface_
        End Get
    End Property

    Private wordReport_ As New WordReport_fr(Me)
    Public ReadOnly Property WordReport As WordReport Implements Language.WordReport
        Get
            Return wordReport_
        End Get
    End Property

    Private graphics_ As New Graphics_fr
    Public ReadOnly Property Graphics As Graphics Implements Language.Graphics
        Get
            Return graphics_
        End Get
    End Property

    Private general_ As New General_fr
    Public ReadOnly Property General As General Implements Language.General
        Get
            Return general_
        End Get
    End Property

End Class
