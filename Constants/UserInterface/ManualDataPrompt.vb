Namespace Constants.UI

    Public Class ManualDataPrompt

        Public Shared ReadOnly BUTTONS_Y_CONDENSED As Integer = 404
        Public Shared ReadOnly BUTTONS_Y_EXPANDED As Integer = 650

        Public Shared ReadOnly WINDOWS_HEIGHT_CONDENSED As Integer = 440
        Public Shared ReadOnly WINDOWS_HEIGHT_EXPANDED As Integer = 684

        Public Shared ReadOnly TOGGLE_OPTIONNAL_DATA_BUTTON_TEXT_CONDENSED As String = "▼ Données optionnelles"
        Public Shared ReadOnly TOGGLE_OPTIONNAL_DATA_BUTTON_TEXT_EXPANDED As String = "▲ Données optionnelles"

        Public Shared ReadOnly CORRECT_TEXT_FIELD_COLOR As Drawing.Color = Drawing.Color.FromArgb(50, 255, 50) ' Greenish
        Public Shared ReadOnly INCORRECT_TEXT_FIELD_COLOR As Drawing.Color = Drawing.Color.FromArgb(255, 50, 50) ' Redish

        Public Enum WINDOW_STATE
            CONDENSED = 0
            EXPANDED = 1
        End Enum

        Public Shared ReadOnly START_LABEL_TEXT As String = "Début"
        Public Shared ReadOnly END_LABEL_TEXT As String = "Fin"
        Public Shared ReadOnly PAUSE_LABEL_TEXT As String = "Pause"
        Public Shared ReadOnly PLANNED_MAINTENANCE_LABEL_TEXT As String = "Entretien plannifié"
        Public Shared ReadOnly SILO_CONTENT_AT_START_LABEL_TEXT As String = "Quantité silo début de journée (approx)"
        Public Shared ReadOnly QUANTITY_LABEL_TEXT As String = "Quantité produite"
        Public Shared ReadOnly SILO_CONTENT_AT_END_LABEL_TEXT As String = "Quantité silo fin de journée (approx)"
        Public Shared ReadOnly REJECTED_MIX_QUANTITY_LABEL_TEXT As String = "Quantité d'enrobé rejeté"
        Public Shared ReadOnly WEIGHTED_QUANTITY_LABEL_TEXT As String = "Quantité poste de pesée"
        Public Shared ReadOnly FIRST_LOADING_TIME_LABEL_TEXT As String = "Heure premier chargement"
        Public Shared ReadOnly LAST_LOADING_TIME_LABEL_TEXT As String = "Heure dernier chargement"
        Public Shared ReadOnly FUEL_QUANTITY_LABEL_TEXT As String = "Carburant"
        Public Shared ReadOnly NORMAL_RECYCLED_MIX_QUANTITY_LABEL_TEXT As String = "Quantité de recyclé utilisés"
        Public Shared ReadOnly SPECIAL_RECYCLED_MIX_QUANTITY_LABEL_TEXT As String = "Quantité de recyclé + bardeaux utilisés"
        Public Shared ReadOnly REJECTED_AGGREGATES_LABEL_TEXT As String = "Quantité de granulats rejetés"
        Public Shared ReadOnly REJECTED_FILLER_LABEL_TEXT As String = "Quantité de filler rejetés"
        Public Shared ReadOnly DRUM_QUANTITY_AT_START_LABEL_TEXT As String = "Quantité tambour début"
        Public Shared ReadOnly DRUM_QUANTITY_AT_END_LABEL_TEXT As String = "Quantité tambour fin"
        Public Shared ReadOnly FUEL_QUANTITY_AT_START_1_LABEL_TEXT As String = "Quantité carburant 1 début"
        Public Shared ReadOnly FUEL_QUANTITY_AT_END_1_LABEL_TEXT As String = "Quantité carburant 1 fin"
        Public Shared ReadOnly FUEL_QUANTITY_AT_START_2_LABEL_TEXT As String = "Quantité carburant 2 début"
        Public Shared ReadOnly FUEL_QUANTITY_AT_END_2_LABEL_TEXT As String = "Quantité carburant 2 fin"
        Public Shared ReadOnly BOILER_QUANTITY_AT_START_LABEL_TEXT As String = "Quantité bouilloire début"
        Public Shared ReadOnly BOILER_QUANTITY_AT_END_LABEL_TEXT As String = "Quantité bouilloire fin"

    End Class

End Namespace

