
Namespace XmlSettings

    ''' <summary>
    ''' ADD COMMENT!!!
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class DataFileNode
        Inherits ComplexSettingsNode

        Protected Sub New(parentNode As Xml.XmlNode, dataFileNode As Xml.XmlNode)
            MyBase.New(parentNode, dataFileNode)
        End Sub

        Private unknownFeeds As New List(Of UnknownFeedNode)

        Public Property DATA_LIST As New List(Of DataInfo)

        Public MustOverride Function verifyTag(tagName As String, isSubColumn As Boolean) As Tag

        Public MustOverride Function getUnitByTag(tagName As Tag) As Unit

        Public Property MassUnit As Unit
            Get
                Dim commonMassUnit As Unit = Nothing

                For Each _dataInfo As DataInfo In Me.DATA_LIST

                    If (Unit.MASS_UNITS.Contains(_dataInfo.TAG.DEFAULT_UNIT)) Then

                        If (IsNothing(commonMassUnit)) Then

                            commonMassUnit = _dataInfo.UNIT
                        ElseIf (Not commonMassUnit.Equals(_dataInfo.UNIT)) Then
                            Return Nothing
                        End If
                    End If
                Next

                Return commonMassUnit
            End Get
            Set(value As Unit)

                For Each _dataInfo As DataInfo In Me.DATA_LIST

                    If (Unit.MASS_UNITS.Contains(_dataInfo.TAG.DEFAULT_UNIT)) Then

                        _dataInfo.UNIT = value
                    End If
                Next
            End Set
        End Property

        Public Property TemperatureUnit As Unit
            Get
                Dim commonTemperatureUnit As Unit = Nothing

                For Each _dataInfo As DataInfo In Me.DATA_LIST

                    If (Unit.TEMPERATURE_UNITS.Contains(_dataInfo.TAG.DEFAULT_UNIT)) Then

                        If (IsNothing(commonTemperatureUnit)) Then

                            commonTemperatureUnit = _dataInfo.UNIT
                        ElseIf (Not commonTemperatureUnit.Equals(_dataInfo.UNIT)) Then
                            Return Nothing
                        End If
                    End If
                Next

                Return commonTemperatureUnit
            End Get
            Set(value As Unit)

                For Each _dataInfo As DataInfo In Me.DATA_LIST

                    If (Unit.TEMPERATURE_UNITS.Contains(_dataInfo.TAG.DEFAULT_UNIT)) Then

                        _dataInfo.UNIT = value
                    End If
                Next
            End Set
        End Property

        Public Property PercentageUnit As Unit
            Get
                Dim commonPercentageUnit As Unit = Nothing

                For Each _dataInfo As DataInfo In Me.DATA_LIST

                    If (Unit.PERCENT_UNITS.Contains(_dataInfo.TAG.DEFAULT_UNIT)) Then

                        If (IsNothing(commonPercentageUnit)) Then

                            commonPercentageUnit = _dataInfo.UNIT
                        ElseIf (Not commonPercentageUnit.Equals(_dataInfo.UNIT)) Then
                            Return Nothing
                        End If
                    End If
                Next

                Return commonPercentageUnit
            End Get
            Set(value As Unit)

                For Each _dataInfo As DataInfo In Me.DATA_LIST

                    If (Unit.PERCENT_UNITS.Contains(_dataInfo.TAG.DEFAULT_UNIT)) Then

                        _dataInfo.UNIT = value
                    End If
                Next
            End Set
        End Property

        Public Property ProductionRateUnit As Unit
            Get
                Dim commonProductionRateUnit As Unit = Nothing

                For Each _dataInfo As DataInfo In Me.DATA_LIST

                    If (Unit.PRODUCTION_SPEED_UNITS.Contains(_dataInfo.TAG.DEFAULT_UNIT)) Then

                        If (IsNothing(commonProductionRateUnit)) Then

                            commonProductionRateUnit = _dataInfo.UNIT
                        ElseIf (Not commonProductionRateUnit.Equals(_dataInfo.UNIT)) Then
                            Return Nothing
                        End If
                    End If
                Next

                Return commonProductionRateUnit
            End Get
            Set(value As Unit)

                For Each _dataInfo As DataInfo In Me.DATA_LIST

                    If (Unit.PRODUCTION_SPEED_UNITS.Contains(_dataInfo.TAG.DEFAULT_UNIT)) Then

                        _dataInfo.UNIT = value
                    End If
                Next
            End Set
        End Property

        Public ReadOnly Property UNKNOWN_FEEDS As List(Of UnknownFeedNode)
            Get
                Return Me.unknownFeeds
            End Get
        End Property

    End Class

End Namespace