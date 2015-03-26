Namespace UI.Common

    Public Class DatesListControl
        Inherits ListControlTemplate(Of ProductionDay_1)

        Public Sub New(title As String)
            MyBase.New(title)

        End Sub

        Public Overrides Sub addObject(obj As ProductionDay_1)

            Me.addItem(New DatesListItem(obj))

        End Sub

        Public Sub showAllDates()

            Me.FilterMethod = Function(day As ProductionDay_1)
                                  Return True
                              End Function

            Me.refreshList()
        End Sub

        Public Sub showOnlyReportReadyDates()

            Me.FilterMethod = Function(day As ProductionDay_1)
                                  Return True
                                  'Return day.IsReportReady
                              End Function

            Me.refreshList()

        End Sub

    End Class
End Namespace
