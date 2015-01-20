Namespace UI

    Public Interface FeedsLayout

        Enum LayoutType
            RECYCLED_ONLY = 0
            RECYCLED_AND_FILLER = 1
            RECYCLED_FILLER_AND_ASPHALT = 3
        End Enum

        Sub ajustLayoutRecycledOnly()

        Sub ajustLayoutRecycledAndFiller()

        Sub ajustLayoutRecycledFillerAndAsphalt()

        Sub refreshLayout()

        WriteOnly Property Layout As LayoutType

    End Interface
End Namespace
