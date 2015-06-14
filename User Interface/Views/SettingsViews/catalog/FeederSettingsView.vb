Namespace UI

    Public Class FeederSettingsView
        Inherits SettingsView

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Bennes"


        ' Components
        ' !LAYOUT!
        Private catalog As FeederListView

        ' Attributes
        Private _feedsSettings As FeedsSettingsController


        Public Sub New()
            MyBase.New()

            Me.layout = New FeedsSettingsViewLayout

            Me._feedsSettings = ProgramController.SettingsControllers.FeedsSettingsController

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.AutoScroll = True

            ' Not the real values, just place holders
            Me.catalog = New FeederListView(Plant.feederCatalog)
            Me.catalog.TabIndex = 1

            Me.RedoButton.TabIndex = 4
            Me.UndoButton.TabIndex = 5
            Me.BackButton.TabIndex = 6

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Dim layout = DirectCast(Me.layout, FeedsSettingsViewLayout)

            Me.catalog.Location = layout.FeedManager1_Location
            'Me.catalog.ajustLayout(layout.FeedManager1_Size)


        End Sub


        Protected Overloads Overrides Sub ajustLayoutFinal()

            Dim layout = DirectCast(Me.layout, FeedsSettingsViewLayout)

            'Me.feedManager1.ajustLayoutFinal(layout.FeedManager1_Size)
            'Me.feedManager2.ajustLayoutFinal(layout.FeedManager2_Size)
            'Me.feedManager3.ajustLayoutFinal(layout.FeedManager3_Size)

        End Sub

        Public Overrides Sub updateFields()

            'Me.feedManager1.updateLists(Me._feedsSettings.Feeds1, Me._feedsSettings.UnknownsFeeds1)
            'Me.feedManager2.updateLists(Me._feedsSettings.Feeds2, Me._feedsSettings.UnknownsFeeds2)
            'Me.feedManager3.updateLists(Me._feedsSettings.Feeds3, Me._feedsSettings.UnknownsFeeds3)

        End Sub

        Protected Overloads Overrides Sub beforeShow()

            'Me.Controls.Remove(Me.feedManager1)
            'Me.Controls.Remove(Me.feedManager2)
            'Me.Controls.Remove(Me.feedManager3)

            'Select Case Me._feedsSettings.UsineType

            '    Case Constants.Settings.UsineType.HYBRID

            '        Me.feedManager1.Title = "Bennes chaudes (.csv)"
            '        Me.feedManager1.LayoutType = FeedsLayout.LayoutType.RECYCLED_AND_FILLER

            '        Me.feedManager2.Title = "Bennes froides (.csv)"
            '        Me.feedManager2.LayoutType = FeedsLayout.LayoutType.RECYCLED_ONLY

            '        Me.feedManager3.Title = "Bennes froides (.log)"
            '        Me.feedManager3.LayoutType = FeedsLayout.LayoutType.RECYCLED_AND_FILLER

            '        Me.Controls.Add(Me.feedManager1)
            '        Me.Controls.Add(Me.feedManager2)
            '        Me.Controls.Add(Me.feedManager3)

            '    Case Constants.Settings.UsineType.CSV

            '        Me.feedManager1.Title = "Bennes chaudes (.csv)"
            '        Me.feedManager1.LayoutType = FeedsLayout.LayoutType.RECYCLED_AND_FILLER

            '        Me.feedManager2.Title = "Bennes froides (.csv)"
            '        Me.feedManager2.LayoutType = FeedsLayout.LayoutType.RECYCLED_ONLY

            '        Me.Controls.Add(Me.feedManager1)
            '        Me.Controls.Add(Me.feedManager2)

            '    Case Constants.Settings.UsineType.LOG

            '        Me.feedManager1.Title = "Bennes froides (.log)"
            '        Me.feedManager1.LayoutType = FeedsLayout.LayoutType.RECYCLED_AND_FILLER

            '        Me.Controls.Add(Me.feedManager1)

            '    Case Constants.Settings.UsineType.MDB

            '        Me.feedManager1.Title = "Bennes chaudes (.mdb)"
            '        Me.feedManager1.LayoutType = FeedsLayout.LayoutType.RECYCLED_FILLER_AND_ASPHALT

            '        Me.feedManager2.Title = "Bennes froides (.mdb)"
            '        Me.feedManager2.LayoutType = FeedsLayout.LayoutType.RECYCLED_ONLY

            '        Me.Controls.Add(Me.feedManager1)
            '        Me.Controls.Add(Me.feedManager2)

            '    Case Else
            '        Throw New NotImplementedException

            'End Select

            Me.Controls.Add(Me.catalog)

            Me.Focus()

        End Sub

        Public Overrides Sub afterShow()

            Me.Focus()

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Protected Overrides ReadOnly Property Controller As SettingsController
            Get
                Return Me._feedsSettings
            End Get
        End Property
    End Class
End Namespace

