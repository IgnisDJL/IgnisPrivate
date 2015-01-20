Namespace UI

    Public Class FeedsSettingsView
        Inherits SettingsView

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Réservoirs"


        ' Components
        ' !LAYOUT!
        Private feedManager1 As FeedInfoManagementView
        Private feedManager2 As FeedInfoManagementView
        Private feedManager3 As FeedInfoManagementView
        ' !LAYOUT!

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
            Me.feedManager1 = New FeedInfoManagementView("", FeedsLayout.LayoutType.RECYCLED_ONLY)
            AddHandler Me.feedManager1.AddNewFeedInfo, AddressOf Me._feedsSettings.addNewFeed1
            AddHandler Me.feedManager1.AddNewFeedInfo, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.feedManager1.AddNewFeedInfo, AddressOf Me.feedManager1.FeedListView.selectLastItem
            AddHandler Me.feedManager1.FeedListView.DeleteFeedInfo, AddressOf Me._feedsSettings.deleteFeed1
            AddHandler Me.feedManager1.FeedListView.DeleteFeedInfo, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.feedManager1.FeedListView.UpdateFeedInfo, AddressOf Me._feedsSettings.updateFeed
            AddHandler Me.feedManager1.FeedListView.UpdateFeedInfo, AddressOf Me.raiseSettingChangedEvent

            Me.feedManager2 = New FeedInfoManagementView("", FeedsLayout.LayoutType.RECYCLED_ONLY)
            AddHandler Me.feedManager2.AddNewFeedInfo, AddressOf Me._feedsSettings.addNewFeed2
            AddHandler Me.feedManager2.AddNewFeedInfo, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.feedManager2.AddNewFeedInfo, AddressOf Me.feedManager2.FeedListView.selectLastItem
            AddHandler Me.feedManager2.FeedListView.DeleteFeedInfo, AddressOf Me._feedsSettings.deleteFeed2
            AddHandler Me.feedManager2.FeedListView.DeleteFeedInfo, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.feedManager2.FeedListView.UpdateFeedInfo, AddressOf Me._feedsSettings.updateFeed
            AddHandler Me.feedManager2.FeedListView.UpdateFeedInfo, AddressOf Me.raiseSettingChangedEvent

            Me.feedManager3 = New FeedInfoManagementView("", FeedsLayout.LayoutType.RECYCLED_ONLY)
            AddHandler Me.feedManager3.AddNewFeedInfo, AddressOf Me._feedsSettings.addNewFeed3
            AddHandler Me.feedManager3.AddNewFeedInfo, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.feedManager3.AddNewFeedInfo, AddressOf Me.feedManager3.FeedListView.selectLastItem
            AddHandler Me.feedManager3.FeedListView.DeleteFeedInfo, AddressOf Me._feedsSettings.deleteFeed3
            AddHandler Me.feedManager3.FeedListView.DeleteFeedInfo, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.feedManager3.FeedListView.UpdateFeedInfo, AddressOf Me._feedsSettings.updateFeed
            AddHandler Me.feedManager3.FeedListView.UpdateFeedInfo, AddressOf Me.raiseSettingChangedEvent

            Me.feedManager1.TabIndex = 1
            Me.feedManager2.TabIndex = 2
            Me.feedManager1.TabIndex = 3
            Me.RedoButton.TabIndex = 4
            Me.UndoButton.TabIndex = 5
            Me.BackButton.TabIndex = 6

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Dim layout = DirectCast(Me.layout, FeedsSettingsViewLayout)

            Me.feedManager1.Location = layout.FeedManager1_Location
            Me.feedManager1.ajustLayout(layout.FeedManager1_Size)

            Me.feedManager2.Location = layout.FeedManager2_Location
            Me.feedManager2.ajustLayout(layout.FeedManager2_Size)

            Me.feedManager3.Location = layout.FeedManager3_Location
            Me.feedManager3.ajustLayout(layout.FeedManager3_Size)

        End Sub


        Protected Overloads Overrides Sub ajustLayoutFinal()

            Dim layout = DirectCast(Me.layout, FeedsSettingsViewLayout)

            Me.feedManager1.ajustLayoutFinal(layout.FeedManager1_Size)
            Me.feedManager2.ajustLayoutFinal(layout.FeedManager2_Size)
            Me.feedManager3.ajustLayoutFinal(layout.FeedManager3_Size)

        End Sub

        Public Overrides Sub updateFields()

            Me.feedManager1.updateLists(Me._feedsSettings.Feeds1, Me._feedsSettings.UnknownsFeeds1)
            Me.feedManager2.updateLists(Me._feedsSettings.Feeds2, Me._feedsSettings.UnknownsFeeds2)
            Me.feedManager3.updateLists(Me._feedsSettings.Feeds3, Me._feedsSettings.UnknownsFeeds3)

        End Sub

        Protected Overloads Overrides Sub beforeShow()

            Me.Controls.Remove(Me.feedManager1)
            Me.Controls.Remove(Me.feedManager2)
            Me.Controls.Remove(Me.feedManager3)

            Select Case Me._feedsSettings.UsineType

                Case Constants.Settings.UsineType.HYBRID

                    Me.feedManager1.Title = "Bennes chaudes (.csv)"
                    Me.feedManager1.LayoutType = FeedsLayout.LayoutType.RECYCLED_AND_FILLER

                    Me.feedManager2.Title = "Bennes froides (.csv)"
                    Me.feedManager2.LayoutType = FeedsLayout.LayoutType.RECYCLED_ONLY

                    Me.feedManager3.Title = "Bennes froides (.log)"
                    Me.feedManager3.LayoutType = FeedsLayout.LayoutType.RECYCLED_AND_FILLER

                    Me.Controls.Add(Me.feedManager1)
                    Me.Controls.Add(Me.feedManager2)
                    Me.Controls.Add(Me.feedManager3)

                Case Constants.Settings.UsineType.CSV

                    Me.feedManager1.Title = "Bennes chaudes (.csv)"
                    Me.feedManager1.LayoutType = FeedsLayout.LayoutType.RECYCLED_AND_FILLER

                    Me.feedManager2.Title = "Bennes froides (.csv)"
                    Me.feedManager2.LayoutType = FeedsLayout.LayoutType.RECYCLED_ONLY

                    Me.Controls.Add(Me.feedManager1)
                    Me.Controls.Add(Me.feedManager2)

                Case Constants.Settings.UsineType.LOG

                    Me.feedManager1.Title = "Bennes froides (.log)"
                    Me.feedManager1.LayoutType = FeedsLayout.LayoutType.RECYCLED_AND_FILLER

                    Me.Controls.Add(Me.feedManager1)

                Case Constants.Settings.UsineType.MDB

                    Me.feedManager1.Title = "Bennes chaudes (.mdb)"
                    Me.feedManager1.LayoutType = FeedsLayout.LayoutType.RECYCLED_FILLER_AND_ASPHALT

                    Me.feedManager2.Title = "Bennes froides (.mdb)"
                    Me.feedManager2.LayoutType = FeedsLayout.LayoutType.RECYCLED_ONLY

                    Me.Controls.Add(Me.feedManager1)
                    Me.Controls.Add(Me.feedManager2)

                Case Else
                    Throw New NotImplementedException

            End Select

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

