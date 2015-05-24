Public Class CatalogContainerItem

    Private _catalogContainerItem As Dictionary(Of Date, String)

    Sub New(effectiveDate As Date, containerDescrition As String)
        _catalogContainerItem = New Dictionary(Of Date, String)
        _catalogContainerItem.Add(effectiveDate, containerDescrition)
    End Sub

    Public Sub addDescription(effectiveDate As Date, containerDescrition As String)
        _catalogContainerItem.Add(effectiveDate, containerDescrition)
    End Sub

    Public Sub removeDescription(effectiveDate As Date)
        _catalogContainerItem.Remove(effectiveDate)
    End Sub

    Public Sub updateDescription(effectiveDate As Date, containerDescrition As String)
        _catalogContainerItem.Item(effectiveDate) = containerDescrition
    End Sub

    Public Function getDescription(productionDate As Date) As String
        Return _catalogContainerItem.Item(getClosestEffectiveDate(productionDate))
    End Function

    Private Function getClosestEffectiveDate(productionDate As Date) As Date
        Dim closestEffectiveDate As Date

        For Each effectiveDate As Date In _catalogContainerItem.Keys

            If effectiveDate.Date <= productionDate.Date Then

                If IsNothing(closestEffectiveDate) Then
                    closestEffectiveDate = effectiveDate

                ElseIf effectiveDate.Date > closestEffectiveDate Then
                    closestEffectiveDate = effectiveDate
                End If
            End If

        Next

        Return closestEffectiveDate
    End Function

    Public Function getAllDescription() As List(Of String)
        Return New List(Of String)(_catalogContainerItem.Values)
    End Function

    Public Function getAllEffectiveDate() As List(Of Date)
        Return New List(Of Date)(_catalogContainerItem.Keys)
    End Function

End Class
