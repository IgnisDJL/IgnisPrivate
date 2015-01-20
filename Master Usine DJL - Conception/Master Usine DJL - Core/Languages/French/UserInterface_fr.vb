Public Class UserInterface_fr
    Implements UserInterface

    Private mainWindow_ As New MainWindowLang_fr
    Public ReadOnly Property MainWindow As MainWindowLang Implements UserInterface.MainWindow
        Get
            Return mainWindow_
        End Get
    End Property

    Private manualDataPrompt_ As New ManualDataPromptLang_fr
    Public ReadOnly Property ManualDataPrompt As ManualDataPromptLang Implements UserInterface.ManualDataPrompt
        Get
            Return manualDataPrompt_
        End Get
    End Property
End Class
