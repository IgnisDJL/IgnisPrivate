Namespace UI

    Public Class ReportsToGenerateListControl
        Inherits Common.ListControlTemplate(Of ReportFile.ReportTypes)

        ' Constants

        ' Components

        ' Attributes
        Public Delegate Function itemIsDelegate(item As ReportFile.ReportTypes) As Boolean
        Private _itemIsCheckedMethod As itemIsDelegate
        Private _itemIsEnabledMethod As itemIsDelegate

        Private refreshingList As Boolean = False

        ' Events
        Public Event ItemChecked(reportType As ReportFile.ReportTypes, checked As Boolean)

        Public Sub New()
            ' #language
            MyBase.New("Rapports à générer")

            Me.ShowNumberOfItemsInTitle = False

        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Dim summaryDaily As New ReportsToGenerateListItem(ReportFile.ReportTypes.SummaryDailyReport)
            AddHandler summaryDaily.CheckedChange, AddressOf Me.onItemChecked
            Me.addItem(summaryDaily)

            Dim summaryNightShift As New ReportsToGenerateListItem(ReportFile.ReportTypes.SummaryNightShiftReport)
            AddHandler summaryNightShift.CheckedChange, AddressOf Me.onItemChecked
            Me.addItem(summaryNightShift)

        End Sub

        Public Overrides Sub refreshList()
            MyBase.refreshList()

            Me.refreshingList = True

            For Each item As ReportsToGenerateListItem In Me.initialItemList

                item.IsChecked = Me._itemIsCheckedMethod(item.ItemObject)

                If (Me._itemIsEnabledMethod(item.ItemObject)) Then

                    item.Enabled = True
                Else

                    item.IsChecked = False
                    item.Enabled = False
                End If
            Next

            Me.refreshingList = False
        End Sub

        Private Sub onItemChecked(item As ReportsToGenerateListItem, checked As Boolean)

            If (Not Me.refreshingList) Then

                RaiseEvent ItemChecked(item.ItemObject, checked)

            End If
        End Sub

        Public WriteOnly Property ItemIsCheckedMethod As itemIsDelegate
            Set(value As itemIsDelegate)
                Me._itemIsCheckedMethod = value
            End Set
        End Property

        Public WriteOnly Property ItemIsEnabledMethod As itemIsDelegate
            Set(value As itemIsDelegate)
                Me._itemIsEnabledMethod = value
            End Set
        End Property

        Public Overrides Sub addObject(reportType As ReportFile.ReportTypes)
            ' Do nothing or throw exception
        End Sub

    End Class
End Namespace
