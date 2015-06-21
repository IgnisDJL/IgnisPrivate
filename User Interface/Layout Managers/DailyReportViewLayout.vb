Namespace UI

    Public Class DailyReportViewLayout
        Inherits ReportViewLayout

        ' Constantes
        Private Shared ReadOnly erreurHoraire_DATE_MESSAGE_PANEL_SIZE As Size = New Size(300, 100)

        ' Components Attributes
        Private _erreurHoraireDateMessagePanel_location As Point
        Private _erreurHoraireDateMessagePanel_size As Size

        Public Sub New()
            MyBase.New()

        End Sub

        Protected Overrides Sub computeLayout()
            MyBase.computeLayout()

            Me._erreurHoraireDateMessagePanel_location = New Point(Me.Width / 2 - Me.erreurHoraireDateMessagePanel_Size.Width / 2, Me.Height / 2 - Me.erreurHoraireDateMessagePanel_Size.Height / 2)

        End Sub

        '
        ' No Last Report Ready Date Message Panel
        '
        Public ReadOnly Property erreurHoraireDateMessagePanel_Location As Point
            Get
                Return Me._erreurHoraireDateMessagePanel_location
            End Get
        End Property
        Public ReadOnly Property erreurHoraireDateMessagePanel_Size As Size
            Get
                Return erreurHoraire_DATE_MESSAGE_PANEL_SIZE
            End Get
        End Property

    End Class

End Namespace
