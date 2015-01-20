Namespace UI

    Public Class DataFilesSettingsView
        Inherits SettingsView

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Fichiers de données"


        ' Components
        Private usbPathPanel As Panel
        Private usbPathLabel As Label
        Private usbPathTextBox As TextBox
        Private WithEvents modifyPathButton As Common.EditButton
        Private directoryExplorer As FolderBrowserDialog

        Private WithEvents unitsPanel1 As UnitsPanel
        Private WithEvents unitsPanel2 As UnitsPanel

        ' Attributes
        Private _dataFilesSettings As DataFilesSettingsController

        Public Sub New()
            MyBase.New()

            Me.layout = New DataFilesSettingsViewLayout

            Me._dataFilesSettings = ProgramController.SettingsControllers.DataFilesSettingsController

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.usbPathPanel = New Panel
            Me.usbPathPanel.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Me.usbPathLabel = New Label
            Me.usbPathLabel.Text = "Emplacement de la clé USB :"
            Me.usbPathLabel.AutoSize = False
            Me.usbPathLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.usbPathTextBox = New TextBox
            Me.usbPathTextBox.ReadOnly = True
            Me.usbPathTextBox.AutoSize = False

            Me.modifyPathButton = New Common.EditButton
            Me.modifyPathButton.TextAlign = ContentAlignment.MiddleCenter
            Me.modifyPathButton.Text = ImportFilesLayout.MODIFY_PATH_BUTTON_TEXT

            Me.usbPathPanel.Controls.Add(usbPathLabel)
            Me.usbPathPanel.Controls.Add(usbPathTextBox)
            Me.usbPathPanel.Controls.Add(modifyPathButton)

            Me.unitsPanel1 = New UnitsPanel()
            Me.unitsPanel2 = New UnitsPanel()

            Me.Controls.Add(Me.usbPathPanel)

        End Sub


        Protected Overloads Overrides Sub ajustLayout()

            Dim layout = DirectCast(Me.layout, DataFilesSettingsViewLayout)

            Me.usbPathPanel.Location = layout.USBPathPanel_Location
            Me.usbPathPanel.Size = layout.USBPathPanel_Size

            Me.usbPathLabel.Location = layout.USBPathLabel_Location
            Me.usbPathLabel.Size = layout.USBPathLabel_Size

            Me.usbPathTextBox.Location = layout.USBPathTextBox_Location
            Me.usbPathTextBox.Size = layout.USBPathTextBox_Size

            Me.modifyPathButton.Location = layout.ModifyPathButton_Location
            Me.modifyPathButton.Size = layout.ModifyPathButton_Size

            Me.unitsPanel1.Location = layout.UnitsPanel1_Location
            Me.unitsPanel1.ajustLayout(layout.UnitsPanel1_Size)

            Me.unitsPanel2.Location = layout.UnitsPanel2_Location
            Me.unitsPanel2.ajustLayout(layout.UnitsPanel2_Size)

        End Sub


        Protected Overloads Overrides Sub ajustLayoutFinal()

        End Sub

        Public Overrides Sub updateFields()

            Me.usbPathTextBox.Text = Me._dataFilesSettings.USBPath

            With Me._dataFilesSettings

                Me.unitsPanel1.updateUnits(.MassUnit1, .TemperatureUnit1, .PercentageUnit1, .ProductionRateUnit1)

                If (.UsineType = Constants.Settings.UsineType.HYBRID) Then
                    Me.unitsPanel2.updateUnits(.MassUnit2, .TemperatureUnit2, .PercentageUnit2, .ProductionRateUnit2)
                End If
            End With

            Me.usbPathPanel.Focus()

        End Sub

        Protected Overloads Overrides Sub beforeShow()

            Select Case Me._dataFilesSettings.UsineType

                Case Constants.Settings.UsineType.HYBRID

                    Me.unitsPanel1.Title = "Unités des données (.csv)"
                    Me.unitsPanel2.Title = "Unités des données (.log)"

                    If (IsNothing(Me.unitsPanel1.Parent)) Then
                        Me.Controls.Add(Me.unitsPanel1)
                    End If

                    If (IsNothing(Me.unitsPanel2.Parent)) Then
                        Me.Controls.Add(Me.unitsPanel2)
                    End If

                Case Constants.Settings.UsineType.CSV
                    Me.unitsPanel1.Title = "Unités des données (.csv)"

                    If (IsNothing(Me.unitsPanel1.Parent)) Then
                        Me.Controls.Add(Me.unitsPanel1)
                    End If

                    Me.Controls.Remove(Me.unitsPanel2)

                Case Constants.Settings.UsineType.LOG
                    Me.unitsPanel1.Title = "Unités des données (.log)"

                    If (IsNothing(Me.unitsPanel1.Parent)) Then
                        Me.Controls.Add(Me.unitsPanel1)
                    End If

                    Me.Controls.Remove(Me.unitsPanel2)

                Case Constants.Settings.UsineType.MDB
                    Me.unitsPanel1.Title = "Unités des données (.mdb)"

                    If (IsNothing(Me.unitsPanel1.Parent)) Then
                        Me.Controls.Add(Me.unitsPanel1)
                    End If

                    Me.Controls.Remove(Me.unitsPanel2)

                Case Constants.Settings.UsineType.UNKNOWN

                    Me.Controls.Remove(Me.unitsPanel1)
                    Me.Controls.Remove(Me.unitsPanel2)

                Case Else
                    Throw New NotImplementedException

            End Select
        End Sub

        Public Overrides Sub afterShow()

            Me.usbPathPanel.Focus()
        End Sub

        Public Overrides Sub onHide()

        End Sub

        Private Sub modifyUSBPath() Handles modifyPathButton.Click

            If (IsNothing(directoryExplorer)) Then
                directoryExplorer = New FolderBrowserDialog
                directoryExplorer.ShowNewFolderButton = False
                directoryExplorer.RootFolder = Environment.SpecialFolder.MyComputer
                ' #language
                directoryExplorer.Description = "Selectionnez la clé USB IGNIS et appuyez sur OK"
            End If

            If (directoryExplorer.ShowDialog = DialogResult.OK) Then

                Try

                    Me._dataFilesSettings.USBPath = directoryExplorer.SelectedPath
                    Me.raiseSettingChangedEvent()

                Catch ex As InvalidUSBPathException

                    Console.WriteLine(ex.Message)
                    Beep()
                Catch ex As Exception

                    Throw New NotImplementedException
                End Try

            End If

        End Sub

        Public Sub changeUnits1(newUnit As Unit) Handles unitsPanel1.MassUnitChanged, _
                                                         unitsPanel1.TemperatureUnitChanged, _
                                                         unitsPanel1.PercentageUnitChanged, _
                                                         unitsPanel1.ProductionRateUnitChanged
            With Me.unitsPanel1

                Me._dataFilesSettings.setUnits1(.MassUnit, .TemperatureUnit, .PercentageUnit, .ProductionRateUnit)
            End With

            raiseSettingChangedEvent()
        End Sub

        Public Sub changeUnits2(newUnit As Unit) Handles unitsPanel2.MassUnitChanged, _
                                                         unitsPanel2.TemperatureUnitChanged, _
                                                         unitsPanel2.PercentageUnitChanged, _
                                                         unitsPanel2.ProductionRateUnitChanged
            With Me.unitsPanel2

                Me._dataFilesSettings.setUnits2(.MassUnit, .TemperatureUnit, .PercentageUnit, .ProductionRateUnit)
            End With

            raiseSettingChangedEvent()
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Protected Overrides ReadOnly Property Controller As SettingsController
            Get
                Return Me._dataFilesSettings
            End Get
        End Property
    End Class
End Namespace

