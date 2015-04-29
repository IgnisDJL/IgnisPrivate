Public Class CatalogContainer
    Private _catalogContainer As Dictionary(Of String, CatalogContainerItem)

    Sub New()
        _catalogContainer = New Dictionary(Of String, CatalogContainerItem)
    End Sub

    Public Sub addNewContainerToCatalog(containerId As String, effectiveDate As Date, containerDescription As String)
        _catalogContainer.Add(containerId, New CatalogContainerItem(effectiveDate, containerDescription))
    End Sub

    Public Sub removeContainerFromCatalog(containerId As String)
        _catalogContainer.Remove(containerId)
    End Sub

    Private Function getCatalogContainerItem(containerId As String) As CatalogContainerItem
        Return _catalogContainer.Item(containerId)
    End Function

    Public Function getDescriptionFromContainer(containerId As String, productionDate As Date) As String
        If (_catalogContainer.Keys.Contains(containerId)) Then
            Return getCatalogContainerItem(containerId).getDescription(productionDate)
        Else
            Return String.Empty
        End If
    End Function

    Public Sub addDescriptionToContainer(containerId As String, effectiveDate As Date, containerDescription As String)
        getCatalogContainerItem(containerId).addDescription(effectiveDate, containerDescription)
    End Sub

    Public Sub updateDescriptionFromContainer(containerId As String, effectiveDate As Date, newDescription As String)
        getCatalogContainerItem(containerId).updateDescription(effectiveDate, newDescription)
    End Sub

    Public Sub removeDescriptionFromContainer(containerId As String, effectiveDate As Date)
        getCatalogContainerItem(containerId).removeDescription(effectiveDate)
    End Sub

    Public Function getAllContainerId() As List(Of String)
        Return New List(Of String)(_catalogContainer.Keys)
    End Function

    Public Function getAllContainerDescription(effectiveDate) As List(Of String)

        Dim actualDescriptionList As List(Of String) = New List(Of String)

        For Each containerId As String In getAllContainerId()
            actualDescriptionList.Add(getDescriptionFromContainer(containerId, effectiveDate))
        Next
        Return actualDescriptionList
    End Function
End Class
