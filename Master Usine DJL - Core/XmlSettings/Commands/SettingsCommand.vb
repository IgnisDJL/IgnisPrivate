Namespace Commands.Settings

    Public MustInherit Class SettingsCommand
        Implements Command

        Private _settings As XmlSettings.Settings

        Protected Sub New()

            Me._settings = XmlSettings.Settings.instance
        End Sub

        Protected ReadOnly Property Settings As XmlSettings.Settings
            Get
                Return Me._settings
            End Get
        End Property

        Public MustOverride Sub execute() Implements Command.execute

        Public MustOverride Sub undo() Implements Command.undo
    End Class
End Namespace

