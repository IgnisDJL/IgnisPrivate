
''' <summary>
''' ADD COMMENT!!!
''' </summary>
''' <remarks></remarks>
Public MustInherit Class DataFileNode

    Public MustOverride Function verifyTag(tagName As String, isSubColumn As Boolean) As Tag

    Public MustOverride Function getUnitByTag(tagName As Tag) As Unit

End Class
