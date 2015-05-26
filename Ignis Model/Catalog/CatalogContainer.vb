Public MustInherit Class CatalogContainer
    Private _catalogContainer As Dictionary(Of String, CatalogContainerItem)

    Sub New()
        _catalogContainer = New Dictionary(Of String, CatalogContainerItem)

    End Sub

    Public Sub addNewContainerToCatalog(containerId As String, effectiveDate As Date, informationAdditionnelList As List(Of String))
        If _catalogContainer.Keys.Contains(containerId) Then
            addInformationAdditionnelListToContainer(containerId, effectiveDate, informationAdditionnelList)
        Else
            _catalogContainer.Add(containerId, createCatalogContainerItem(effectiveDate, informationAdditionnelList))
        End If

    End Sub

    Protected MustOverride Function createCatalogContainerItem(effectiveDate As Date, informationAdditionnelList As List(Of String)) As CatalogContainerItem

    Public Sub removeContainerFromCatalog(containerId As String)
        _catalogContainer.Remove(containerId)
    End Sub

    Private Function getCatalogContainerItem(containerId As String) As CatalogContainerItem
        Return _catalogContainer.Item(containerId)
    End Function

    Public Function getDescriptionFromContainer(containerId As String, productionDate As Date) As List(Of String)
        If (_catalogContainer.Keys.Contains(containerId)) Then
            Return getCatalogContainerItem(containerId).getInformationAdditionnelList(productionDate)
        Else
            Return String.Empty
        End If
    End Function

    Public Sub addInformationAdditionnelListToContainer(containerId As String, effectiveDate As Date, informationAdditionnelList As List(Of String))
        If getCatalogContainerItem(containerId).getAllEffectiveDate.Contains(effectiveDate) Then
            updateInformationAdditionnelListFromContainer(containerId, effectiveDate, informationAdditionnelList)
        Else
            getCatalogContainerItem(containerId).addInformationAdditionnelList(effectiveDate, informationAdditionnelList)
        End If
    End Sub

    Public Sub updateInformationAdditionnelListFromContainer(containerId As String, effectiveDate As Date, newInformationAdditionnelList As List(Of String))
        getCatalogContainerItem(containerId).updateInformationAdditionnelList(effectiveDate, newInformationAdditionnelList)
    End Sub

    Public Sub removeInformationAdditionnelListFromContainer(containerId As String, effectiveDate As Date)
        getCatalogContainerItem(containerId).removeInformationAdditionnelList(effectiveDate)
    End Sub

    Public Function getAllContainerId() As List(Of String)
        Return New List(Of String)(_catalogContainer.Keys)
    End Function

    Public Function getContainerItemAllEffectiveDate(containerId As String) As List(Of Date)
        Return getCatalogContainerItem(containerId).getAllEffectiveDate()
    End Function
End Class
