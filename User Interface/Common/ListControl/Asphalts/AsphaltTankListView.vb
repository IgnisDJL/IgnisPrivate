Namespace UI

    Public Class AsphaltTankListView
        Inherits Common.ListControlTemplate(Of XmlSettings.AsphaltNode.TankInfo)

        ' Events
        Public Event deleteTankInfo(asphaltTank As XmlSettings.AsphaltNode.TankInfo)
        Public Event updateTankInfo(asphaltTank As XmlSettings.AsphaltNode.TankInfo, tankName As String, asphaltName As String, mixTargetTemperature As Double)

        Public Sub New()
            MyBase.New("Bennes de bitume")

            Me.SortMethod = Function(x As XmlSettings.AsphaltNode.TankInfo, y As XmlSettings.AsphaltNode.TankInfo)
                                Return x.TANK_NAME.CompareTo(y.TANK_NAME)
                            End Function
        End Sub

        Public Overrides Sub addObject(obj As XmlSettings.AsphaltNode.TankInfo)

            Dim newItem = New AsphaltTankListItem(obj)

            ' #todo - when clear() is called, these should be unbound
            ' or #refactor, find a better way to pass the events
            AddHandler newItem.deleteTankInfo, AddressOf Me.raiseDeleteEvent
            AddHandler newItem.updateTankInfo, AddressOf Me.raiseUpdateEvent

            Me.addItem(newItem)

        End Sub

        Private Sub raiseDeleteEvent(asphaltTank As XmlSettings.AsphaltNode.TankInfo)

            RaiseEvent deleteTankInfo(asphaltTank)

        End Sub

        Private Sub raiseUpdateEvent(asphaltTank As XmlSettings.AsphaltNode.TankInfo, tankName As String, asphaltName As String, mixTargetTemperature As Double)

            RaiseEvent updateTankInfo(asphaltTank, tankName, asphaltName, mixTargetTemperature)

        End Sub

    End Class
End Namespace
