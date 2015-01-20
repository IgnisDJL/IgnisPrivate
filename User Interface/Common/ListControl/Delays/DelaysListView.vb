Namespace UI

    Public Class DelaysListView
        Inherits Common.ListControlTemplate(Of DelayCode)

        ' Constants

        ' Components

        ' Attributes
        Private settingsController As EventsSettingsController

        ' Events
        Public Event DeleteDelayCode(code As DelayCode)
        Public Event UpdateDelayCode(code As DelayCode, newDelayCode As String, newDelayDescription As String, newDelayType As DelayType)


        Public Sub New(settingsController As EventsSettingsController)
            MyBase.New("Délais")

            Me.settingsController = settingsController
        End Sub

        Public Overrides Sub addObject(obj As DelayCode)

            Dim newItem As New DelaysListItem(obj, settingsController)

            Me.addItem(newItem)

            AddHandler newItem.DeleteDelayCode, AddressOf Me.raiseDeleteDelayCode
            AddHandler newItem.UpdateDelayCode, AddressOf Me.raiseUpdateDelayCode

        End Sub

        Private Sub raiseDeleteDelayCode(code As DelayCode)

            RaiseEvent DeleteDelayCode(code)

        End Sub

        Private Sub raiseUpdateDelayCode(code As DelayCode, newDelayCode As String, newDelayDescription As String, newDelayType As DelayType)

            RaiseEvent UpdateDelayCode(code, newDelayCode, newDelayDescription, newDelayType)

        End Sub

        ''' <summary>
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>Because the ComboBox doesn't resize</remarks>
        Protected Overrides ReadOnly Property ItemsHeight As Integer
            Get
                Return 31
            End Get
        End Property

    End Class
End Namespace
