Imports IGNIS.Commands.Settings

Public Class DataFilesSettingsController
    Inherits SettingsController

    Private _dataFileInfo1 As XmlSettings.DataFileNode
    Private _dataFileInfo2 As XmlSettings.DataFileNode

    Public Sub New()
        MyBase.New()

        Try
            refreshDataFileInfos()
        Catch e As Exception
            ' Do nothing
        End Try

    End Sub

    Public ReadOnly Property UsineType As Constants.Settings.UsineType
        Get
            Return XmlSettings.Settings.instance.Usine.TYPE
        End Get
    End Property

    Public Property USBPath As String
        Get
            Return XmlSettings.Settings.instance.Usine.USB_DIRECTORY
        End Get
        Set(value As String)

            If (ProgramController.ImportController.isValidUSBDirectory(New IO.DirectoryInfo(value))) Then
                Me.executeCommand(New SetUSBPath(value))
            Else
                Throw New InvalidUSBPathException(value)
            End If
        End Set
    End Property

    Public Sub setUnits1(massUnit As Unit, temperatureUnit As Unit, percentageUnit As Unit, productionRateUnit As Unit)

        Select Case Me.UsineType

            Case Constants.Settings.UsineType.HYBRID, Constants.Settings.UsineType.CSV
                Me.executeCommand(New SetCSVUnits(massUnit, temperatureUnit, percentageUnit, productionRateUnit))

            Case Constants.Settings.UsineType.LOG
                Me.executeCommand(New SetLOGUnits(massUnit, temperatureUnit, percentageUnit, productionRateUnit))

            Case Constants.Settings.UsineType.MDB
                Me.executeCommand(New SetMDBUnits(massUnit, temperatureUnit, percentageUnit, productionRateUnit))

            Case Else
                Throw New NotImplementedException

        End Select
    End Sub

    Public Sub setUnits2(massUnit As Unit, temperatureUnit As Unit, percentageUnit As Unit, productionRateUnit As Unit)

        Select Case Me.UsineType

            Case Constants.Settings.UsineType.HYBRID
                Me.executeCommand(New SetLOGUnits(massUnit, temperatureUnit, percentageUnit, productionRateUnit))

            Case Else
                Throw New NotImplementedException

        End Select
    End Sub

    Public ReadOnly Property MassUnit1 As Unit
        Get

            refreshDataFileInfos()

            Return Me._dataFileInfo1.MassUnit
        End Get
    End Property

    Public ReadOnly Property TemperatureUnit1 As Unit
        Get

            refreshDataFileInfos()

            Return Me._dataFileInfo1.TemperatureUnit
        End Get
    End Property

    Public ReadOnly Property PercentageUnit1 As Unit
        Get

            refreshDataFileInfos()

            Return Me._dataFileInfo1.PercentageUnit
        End Get
    End Property

    Public ReadOnly Property ProductionRateUnit1 As Unit
        Get

            refreshDataFileInfos()

            Return Me._dataFileInfo1.ProductionRateUnit
        End Get
    End Property

    Public ReadOnly Property MassUnit2 As Unit
        Get

            refreshDataFileInfos()

            Return Me._dataFileInfo2.MassUnit
        End Get
    End Property

    Public ReadOnly Property TemperatureUnit2 As Unit
        Get

            refreshDataFileInfos()

            Return Me._dataFileInfo2.TemperatureUnit
        End Get
    End Property

    Public ReadOnly Property PercentageUnit2 As Unit
        Get

            refreshDataFileInfos()

            Return Me._dataFileInfo2.PercentageUnit
        End Get
    End Property

    Public ReadOnly Property ProductionRateUnit2 As Unit
        Get

            refreshDataFileInfos()

            Return Me._dataFileInfo2.ProductionRateUnit
        End Get
    End Property

    Private Sub refreshDataFileInfos()
        Select Case Me.UsineType

            Case Constants.Settings.UsineType.HYBRID
                Me._dataFileInfo1 = XmlSettings.Settings.instance.Usine.DataFiles.CSV
                Me._dataFileInfo2 = XmlSettings.Settings.instance.Usine.DataFiles.LOG

            Case Constants.Settings.UsineType.CSV
                Me._dataFileInfo1 = XmlSettings.Settings.instance.Usine.DataFiles.CSV

            Case Constants.Settings.UsineType.LOG
                Me._dataFileInfo1 = XmlSettings.Settings.instance.Usine.DataFiles.LOG

            Case Constants.Settings.UsineType.MDB
                Me._dataFileInfo1 = XmlSettings.Settings.instance.Usine.DataFiles.MDB

            Case Else
                Throw New Exception("Il faut choisir un type de production. Vous pouvez le faire dans la section 'Usine' des paramètres.")

        End Select
    End Sub

End Class
