Module Plant
    Private _feederCatalog As CatalogContainer
    Private _asphaltCatalog As CatalogContainer
    Private _recipeCatalog As CatalogRecipe

    Sub New()
    End Sub

    Public ReadOnly Property feederCatalog() As CatalogContainer
        Get
            If (IsNothing(_feederCatalog)) Then
                _feederCatalog = New CatalogContainer()
            End If

            Return _feederCatalog
        End Get
    End Property

    Public ReadOnly Property asphaltCatalog() As CatalogContainer
        Get
            If (IsNothing(_asphaltCatalog)) Then
                _asphaltCatalog = New CatalogContainer()
            End If

            Return _asphaltCatalog
        End Get
    End Property

    Public ReadOnly Property recipeCatalog() As CatalogRecipe
        Get
            If (IsNothing(_recipeCatalog)) Then
                _recipeCatalog = New CatalogRecipe()
            End If

            Return _recipeCatalog
        End Get
    End Property
End Module
