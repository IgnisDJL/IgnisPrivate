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
        Dim closestEffectiveDate As Date = New Date(1, 1, 1)
        Dim minEffectiveDate As Date

        If _catalogContainerItem.Keys.Count > 0 Then
            minEffectiveDate = _catalogContainerItem.Keys(0)

            For Each effectiveDate As Date In _catalogContainerItem.Keys

                If effectiveDate < minEffectiveDate Then
                    minEffectiveDate = effectiveDate
                End If

                If effectiveDate <= productionDate Then

                    If closestEffectiveDate = New Date(1, 1, 1) Then
                        closestEffectiveDate = effectiveDate

                    ElseIf effectiveDate > closestEffectiveDate Then
                        closestEffectiveDate = effectiveDate
                    End If
                End If
            Next

            If closestEffectiveDate = New Date(1, 1, 1) Then
                closestEffectiveDate = minEffectiveDate
            End If

        End If
        Return closestEffectiveDate
    End Function

    Public Function getAllDescription() As List(Of String)
        Return New List(Of String)(_catalogContainerItem.Values)
    End Function

    Public Function getAllEffectiveDate() As List(Of Date)
        Return New List(Of Date)(_catalogContainerItem.Keys)
    End Function

End Class
