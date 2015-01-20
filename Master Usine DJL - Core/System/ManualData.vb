Public Class ManualData

    ' Constants
    Public Shared ReadOnly MASS_UNIT As Unit = Tons.UNIT
    Public Shared ReadOnly FUEL_1_UNIT As String = "L"
    Public Shared ReadOnly FUEL_2_UNIT As String = "m³" ' ^3 : ASCII CODE 252
    Public Shared ReadOnly HOUR_COUNTERS_UNIT As String = "h"

    Public Shared ReadOnly UNKNOWN_QUANTITY As Double = Double.NegativeInfinity
    Public Shared ReadOnly INVALID_QUANTITY As Double = Double.NaN

    ' ReadOnly
    Private _date As Date
    Private productionStartTime As Date
    Private productionStopTime As Date
    Private producedQuantity As Integer

    ' Mandatory
    Private operationStartTime As Date ' Default in constructor
    Private operationEndTime As Date ' Default in constructor
    Private siloQuantityAtStart As Double = INVALID_QUANTITY
    Private siloQuantityAtEnd As Double = INVALID_QUANTITY
    Private rejectedMixQuantity As Double = INVALID_QUANTITY
    Private rejectedAggregatesQuantity As Double = INVALID_QUANTITY
    Private rejectedFillerQuantity As Double = INVALID_QUANTITY
    Private rejectedRecycledQuantity As Double = INVALID_QUANTITY

    ' Optionnal
    Private weightedQuantity As Double = INVALID_QUANTITY
    Private firstLoadingTime As Date ' Default in constructor
    Private lastLoadingTime As Date ' Default in constructor
    Private fuelQuantityAtStart1 As Double = INVALID_QUANTITY
    Private fuelQuantityAtEnd1 As Double = INVALID_QUANTITY
    Private fuelQuantityAtStart2 As Double = INVALID_QUANTITY
    Private fuelQuantityAtEnd2 As Double = INVALID_QUANTITY
    Private drumsHourCounterAtStart As Double = INVALID_QUANTITY
    Private drumsHourCounterAtEnd As Double = INVALID_QUANTITY
    Private boilersHourCounterAtStart As Double = INVALID_QUANTITY
    Private boilerHourCounterAtEnd As Double = INVALID_QUANTITY

    ' Validators
    Private isOperationStartTimeValid As Boolean = True
    Private isOperationEndTimeValid As Boolean = True
    Private isSiloQuantityAtStartValid As Boolean = False
    Private isSiloQuantityAtEndValid As Boolean = False
    Private isRejectedMixQuantityValid As Boolean = False
    Private isRejectedAggregatesQuantityValid As Boolean = False
    Private isRejectedFillerQuantityValid As Boolean = False
    Private isRejectedRecycledQuantityValid As Boolean = False

    Public Sub New(_date As Date, productionStartTime As Date, productionStopTime As Date, producedQuantity As Integer)

        Me._date = _date
        Me.productionStartTime = productionStartTime.Subtract(TimeSpan.FromSeconds(productionStartTime.Second))
        Me.productionStopTime = productionStopTime.Subtract(TimeSpan.FromSeconds(productionStopTime.Second))
        Me.producedQuantity = producedQuantity

        ' Default values
        Me.operationStartTime = Me.productionStartTime
        Me.operationEndTime = Me.productionStopTime

        Me.firstLoadingTime = Me.productionStartTime
        Me.lastLoadingTime = Me.productionStopTime

    End Sub

    ''' <remarks>Should only be called from Persistence</remarks>
    Public Sub New(_date As Date, _
                   productionStartTime As Date, _
                   productionStopTime As Date, _
                   producedQuantity As Integer, _
                   factoryOperator As FactoryOperator, _
                   operationStartTime As Date, _
                   operationStopTime As Date, _
                   siloQuantityAtStart As Double, _
                   siloQuantityAtEnd As Double, _
                   rejectedMix As Double, _
                   rejectedAggs As Double, _
                   rejectedFiller As Double, _
                   rejectedRecycled As Double, _
                   weightedQuantity As Double, _
                   firstLoadTime As Date, _
                   lastLoadTime As Date, _
                   fuelAtStart1 As Double, _
                   fuelAtEnd1 As Double, _
                   fuelAtStart2 As Double, _
                   fuelAtEnd2 As Double, _
                   drumsAtStart As Double, _
                   drumsAtEnd As Double, _
                   boilerAtStart As Double, _
                   boilerAtEnd As Double)


        Me._date = _date
        Me.productionStartTime = productionStartTime
        Me.productionStopTime = productionStopTime
        Me.producedQuantity = producedQuantity
        Me.FACTORY_OPERATOR = factoryOperator
        Me.operationStartTime = operationStartTime
        Me.operationEndTime = operationStopTime
        Me.siloQuantityAtStart = siloQuantityAtStart
        Me.siloQuantityAtEnd = siloQuantityAtEnd
        Me.rejectedMixQuantity = rejectedMix
        Me.rejectedAggregatesQuantity = rejectedAggs
        Me.rejectedFillerQuantity = rejectedFiller
        Me.rejectedRecycledQuantity = rejectedRecycled
        Me.weightedQuantity = weightedQuantity
        Me.firstLoadingTime = firstLoadTime
        Me.lastLoadingTime = lastLoadTime
        Me.fuelQuantityAtStart1 = fuelAtStart1
        Me.fuelQuantityAtEnd1 = fuelAtEnd1
        Me.fuelQuantityAtStart2 = fuelAtStart2
        Me.fuelQuantityAtEnd2 = fuelAtEnd2
        Me.drumsHourCounterAtStart = drumsAtStart
        Me.drumsHourCounterAtEnd = drumsAtEnd
        Me.boilersHourCounterAtStart = boilerAtStart
        Me.boilerHourCounterAtEnd = boilerAtEnd

        isOperationStartTimeValid = True
        isOperationEndTimeValid = True
        isSiloQuantityAtStartValid = Not siloQuantityAtStart.Equals(INVALID_QUANTITY)
        isSiloQuantityAtEndValid = Not siloQuantityAtEnd.Equals(INVALID_QUANTITY)
        isRejectedMixQuantityValid = Not rejectedMixQuantity.Equals(INVALID_QUANTITY)
        isRejectedAggregatesQuantityValid = Not rejectedAggregatesQuantity.Equals(INVALID_QUANTITY)
        isRejectedFillerQuantityValid = Not rejectedFillerQuantity.Equals(INVALID_QUANTITY)
        isRejectedRecycledQuantityValid = Not rejectedRecycledQuantity.Equals(INVALID_QUANTITY)

    End Sub

    Public Function isComplete() As Boolean

        Return isOperationStartTimeValid AndAlso _
               isOperationEndTimeValid AndAlso _
               isSiloQuantityAtStartValid AndAlso _
               isSiloQuantityAtEndValid AndAlso _
               isRejectedMixQuantityValid AndAlso _
               isRejectedAggregatesQuantityValid AndAlso _
               isRejectedFillerQuantityValid AndAlso _
               isRejectedRecycledQuantityValid

    End Function



    Public Property FACTORY_OPERATOR As FactoryOperator = FactoryOperator.DEFAULT_OPERATOR

    Public ReadOnly Property DATE_ As Date
        Get
            Return Me._date
        End Get
    End Property

    Public ReadOnly Property PRODUCTION_START_TIME As Date
        Get
            Return Me.productionStartTime
        End Get
    End Property

    Public ReadOnly Property PRODUCTION_END_TIME As Date
        Get
            Return Me.productionStopTime
        End Get
    End Property

    Public ReadOnly Property PRODUCED_QUANTITY As Integer
        Get
            Return Me.producedQuantity
        End Get
    End Property

    ' Mandatory
    Public Property OPERATION_START_TIME As Date
        Get
            Return operationStartTime
        End Get
        Set(value As Date)

            Dim oldValue = operationStartTime

            operationStartTime = value

            Me.isOperationStartTimeValid = True

            If (value.CompareTo(operationEndTime) > 0 OrElse value.CompareTo(Me.PRODUCTION_START_TIME) > 0) Then

                Me.isOperationStartTimeValid = False

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property OPERATION_END_TIME As Date
        Get
            Return operationEndTime
        End Get
        Set(value As Date)

            Dim oldValue = operationEndTime

            operationEndTime = value

            Me.isOperationEndTimeValid = True

            If (value.CompareTo(operationStartTime) < 0 OrElse value.CompareTo(Me.PRODUCTION_END_TIME) < 0) Then

                Me.isOperationEndTimeValid = False

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property SILO_QUANTITY_AT_START As Double
        Get
            Return siloQuantityAtStart
        End Get
        Set(value As Double)

            If (value >= 0 OrElse value.Equals(UNKNOWN_QUANTITY)) Then

                siloQuantityAtStart = value

                Me.isSiloQuantityAtStartValid = True
            Else

                Me.isSiloQuantityAtStartValid = False

                Throw New IncorrectDataException(value, siloQuantityAtStart)
            End If
        End Set
    End Property

    Public Property SILO_QUANTITY_AT_END As Double
        Get
            Return siloQuantityAtEnd
        End Get
        Set(value As Double)

            If (value >= 0 OrElse value.Equals(UNKNOWN_QUANTITY)) Then
                siloQuantityAtEnd = value

                Me.isSiloQuantityAtEndValid = True
            Else

                Me.isSiloQuantityAtEndValid = False

                Throw New IncorrectDataException(value, siloQuantityAtEnd)
            End If
        End Set
    End Property

    Public Property REJECTED_MIX_QUANTITY As Double
        Get
            Return rejectedMixQuantity
        End Get
        Set(value As Double)

            Dim oldValue = rejectedMixQuantity

            rejectedMixQuantity = value

            If (value >= 0 OrElse value.Equals(UNKNOWN_QUANTITY)) Then

                Me.isRejectedMixQuantityValid = True
            Else

                Me.isRejectedMixQuantityValid = False

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property REJECTED_AGGREGATES_QUANTITY As Double
        Get
            Return rejectedAggregatesQuantity
        End Get
        Set(value As Double)

            Dim oldValue = rejectedAggregatesQuantity

            rejectedAggregatesQuantity = value

            If (value >= 0 OrElse value.Equals(UNKNOWN_QUANTITY)) Then

                Me.isRejectedAggregatesQuantityValid = True
            Else

                Me.isRejectedAggregatesQuantityValid = False

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property REJECTED_FILLER_QUANTITY As Double
        Get
            Return rejectedFillerQuantity
        End Get
        Set(value As Double)

            Dim oldValue = rejectedFillerQuantity

            rejectedFillerQuantity = value

            If (value >= 0 OrElse value.Equals(UNKNOWN_QUANTITY)) Then

                Me.isRejectedFillerQuantityValid = True
            Else

                Me.isRejectedFillerQuantityValid = False

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property REJECTED_RECYCLED_QUANTITY As Double
        Get
            Return rejectedRecycledQuantity
        End Get
        Set(value As Double)

            Dim oldValue = rejectedRecycledQuantity

            rejectedRecycledQuantity = value

            If (value >= 0 OrElse value.Equals(UNKNOWN_QUANTITY)) Then

                Me.isRejectedRecycledQuantityValid = True
            Else

                Me.isRejectedRecycledQuantityValid = False

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    ' Optional
    Public Property WEIGHTED_QUANTITY As Double
        Get
            Return weightedQuantity
        End Get
        Set(value As Double)

            Dim oldValue = rejectedFillerQuantity

            weightedQuantity = value

            If (value < 0 OrElse value.Equals(INVALID_QUANTITY)) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public ReadOnly Property DIFFERENCE_PERCENTAGE As Integer
        Get
            Return (WEIGHTED_QUANTITY - (PRODUCED_QUANTITY + SILO_QUANTITY_AT_START)) / WEIGHTED_QUANTITY * 100
        End Get
    End Property

    Public Property FIRST_LOADING_TIME As Date
        Get
            Return firstLoadingTime
        End Get
        Set(value As Date)

            Dim oldValue = firstLoadingTime

            firstLoadingTime = value

            If (value.CompareTo(operationStartTime) < 0 OrElse value.CompareTo(lastLoadingTime) > 0) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property LAST_LOADING_TIME As Date
        Get
            Return lastLoadingTime
        End Get
        Set(value As Date)

            Dim oldValue = lastLoadingTime

            lastLoadingTime = value

            If (value.CompareTo(operationEndTime) > 0 OrElse value.CompareTo(firstLoadingTime) < 0) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property FUEL_QUANTITY_AT_START_1 As Double
        Get
            Return fuelQuantityAtStart1
        End Get
        Set(value As Double)

            Dim oldValue = fuelQuantityAtStart1

            fuelQuantityAtStart1 = value

            If (value < 0 OrElse value.Equals(INVALID_QUANTITY)) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property FUEL_QUANTITY_AT_END_1 As Double
        Get
            Return fuelQuantityAtEnd1
        End Get
        Set(value As Double)

            Dim oldValue = fuelQuantityAtEnd1

            fuelQuantityAtEnd1 = value

            If (value < 0 OrElse value.Equals(INVALID_QUANTITY)) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property FUEL_QUANTITY_AT_START_2 As Double
        Get
            Return fuelQuantityAtStart2
        End Get
        Set(value As Double)

            Dim oldValue = fuelQuantityAtStart2

            fuelQuantityAtStart2 = value

            If (value < 0 OrElse value.Equals(INVALID_QUANTITY)) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property FUEL_QUANTITY_AT_END_2 As Double
        Get
            Return fuelQuantityAtEnd2
        End Get
        Set(value As Double)

            Dim oldValue = fuelQuantityAtEnd2

            fuelQuantityAtEnd2 = value

            If (value < 0 OrElse value.Equals(INVALID_QUANTITY)) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property DRUMS_HOURS_COUNTER_AT_START As Double
        Get
            Return drumsHourCounterAtStart
        End Get
        Set(value As Double)

            Dim oldValue = drumsHourCounterAtStart

            drumsHourCounterAtStart = value

            If (value < 0 OrElse value.Equals(INVALID_QUANTITY)) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property DRUMS_HOURS_COUNTER_AT_END As Double
        Get
            Return drumsHourCounterAtEnd
        End Get
        Set(value As Double)

            Dim oldValue = drumsHourCounterAtEnd

            drumsHourCounterAtEnd = value

            If (value < 0 OrElse value.Equals(INVALID_QUANTITY)) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property BOILERS_HOUR_COUNTER_AT_START As Double
        Get
            Return boilersHourCounterAtStart
        End Get
        Set(value As Double)

            Dim oldValue = boilersHourCounterAtStart

            boilersHourCounterAtStart = value

            If (value < 0 OrElse value.Equals(INVALID_QUANTITY)) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    Public Property BOILERS_HOUR_COUNTER_AT_END As Double
        Get
            Return boilerHourCounterAtEnd
        End Get
        Set(value As Double)

            Dim oldValue = boilerHourCounterAtEnd

            boilerHourCounterAtEnd = value

            If (value < 0 OrElse value.Equals(INVALID_QUANTITY)) Then

                Throw New IncorrectDataException(value, oldValue)
            End If
        End Set
    End Property

    '
    ' DERIVED VALUES
    '

    ''' <summary>The rejected mix percentage of the total quantity produced</summary>
    ''' <remarks>
    ''' 
    ''' [Rejected mix percentage] = [Rejected mix quantity] / [Produced quantity] * 100
    ''' 
    ''' </remarks>
    Public ReadOnly Property REJECTED_MIX_PERCENTAGE As Double
        Get

            If (Me.REJECTED_MIX_QUANTITY.Equals(INVALID_QUANTITY)) Then
                Return INVALID_QUANTITY
            ElseIf (Me.REJECTED_MIX_QUANTITY.Equals(UNKNOWN_QUANTITY)) Then
                Return UNKNOWN_QUANTITY
            Else
                Return Me.REJECTED_MIX_QUANTITY / Me.PRODUCED_QUANTITY * 100
            End If
        End Get
    End Property

    ''' <summary>The rejected aggregates percentage of the total quantity produced</summary>
    ''' <remarks>
    ''' 
    ''' [Rejected aggregates percentage] = [Rejected aggregates quantity] / [Produced quantity] * 100
    ''' 
    ''' </remarks>
    Public ReadOnly Property REJECTED_AGGREGATES_PERCENTAGE As Double
        Get

            If (Me.REJECTED_AGGREGATES_QUANTITY.Equals(INVALID_QUANTITY)) Then
                Return INVALID_QUANTITY
            ElseIf (Me.REJECTED_AGGREGATES_QUANTITY.Equals(UNKNOWN_QUANTITY)) Then
                Return UNKNOWN_QUANTITY
            Else
                Return Me.REJECTED_AGGREGATES_QUANTITY / Me.PRODUCED_QUANTITY * 100
            End If
        End Get
    End Property

    ''' <summary>The rejected filler percentage of the total quantity produced</summary>
    ''' <remarks>
    ''' 
    ''' [Rejected filler percentage] = [Rejected filler quantity] / [Produced quantity] * 100
    ''' 
    ''' </remarks>
    Public ReadOnly Property REJECTED_FILLER_PERCENTAGE As Double
        Get

            If (Me.REJECTED_FILLER_QUANTITY.Equals(INVALID_QUANTITY)) Then
                Return INVALID_QUANTITY
            ElseIf (Me.REJECTED_FILLER_QUANTITY.Equals(UNKNOWN_QUANTITY)) Then
                Return UNKNOWN_QUANTITY
            Else
                Return Me.REJECTED_FILLER_QUANTITY / Me.PRODUCED_QUANTITY * 100
            End If
        End Get
    End Property

    ''' <summary>The rejected recycled percentage of the total quantity produced</summary>
    ''' <remarks>
    ''' 
    ''' [Rejected recycled percentage] = [Rejected recycled quantity] / [Produced quantity] * 100
    ''' 
    ''' </remarks>
    Public ReadOnly Property REJECTED_RECYCLED_PERCENTAGE As Double
        Get

            If (Me.REJECTED_RECYCLED_QUANTITY.Equals(INVALID_QUANTITY)) Then
                Return INVALID_QUANTITY
            ElseIf (Me.REJECTED_RECYCLED_QUANTITY.Equals(UNKNOWN_QUANTITY)) Then
                Return UNKNOWN_QUANTITY
            Else
                Return Me.REJECTED_RECYCLED_QUANTITY / Me.PRODUCED_QUANTITY * 100
            End If
        End Get
    End Property


    ''' <summary>The salable quantity</summary>
    ''' <remarks>
    ''' 
    ''' [Salable quantity] = [Produced quantity] + [Silo quantity at start] - [Silo quantity at end]
    ''' 
    ''' </remarks>
    Public ReadOnly Property SALABLE_QUANTITY As Double
        Get

            If (Not Me.SILO_QUANTITY_AT_START.Equals(INVALID_QUANTITY) AndAlso _
                Not Me.SILO_QUANTITY_AT_END.Equals(INVALID_QUANTITY) AndAlso _
                Not Me.SILO_QUANTITY_AT_START.Equals(UNKNOWN_QUANTITY) AndAlso _
                Not Me.SILO_QUANTITY_AT_END.Equals(UNKNOWN_QUANTITY)) Then

                Return Me.PRODUCED_QUANTITY + Me.SILO_QUANTITY_AT_START - Me.SILO_QUANTITY_AT_END

            Else

                Return Me.PRODUCED_QUANTITY
            End If
        End Get
    End Property

    ''' <summary>The payable quantity</summary>
    ''' <remarks>
    ''' 
    ''' [Payable quantity] = [Salable quantity] - [Rejected quantity]
    ''' 
    ''' </remarks>
    Public ReadOnly Property PAYABLE_QUANTITY As Double
        Get

            If (Not Me.REJECTED_MIX_QUANTITY.Equals(INVALID_QUANTITY) AndAlso _
                Not Me.REJECTED_MIX_QUANTITY.Equals(UNKNOWN_QUANTITY)) Then

                Return Me.SALABLE_QUANTITY - Me.REJECTED_MIX_QUANTITY

            Else

                Return Me.SALABLE_QUANTITY
            End If
        End Get
    End Property

    ''' <summary>The total quantity sold difference percentage</summary>
    ''' <remarks>
    ''' I have no idea what this is...
    ''' 
    ''' [Weighted quantity difference percentage] = ([Weighted quantity] - [Produced quantity]) / [Weighted quanity] * 100
    ''' 
    ''' </remarks>
    Public ReadOnly Property WEIGHTED_QUANTITY_DIFFERENCE_PERCENTAGE As Double
        Get

            If (Me.WEIGHTED_QUANTITY.Equals(INVALID_QUANTITY)) Then
                Return INVALID_QUANTITY
            ElseIf (Me.WEIGHTED_QUANTITY.Equals(UNKNOWN_QUANTITY)) Then
                Return UNKNOWN_QUANTITY
            Else
                Return (Me.WEIGHTED_QUANTITY - Me.PRODUCED_QUANTITY) / Me.WEIGHTED_QUANTITY * 100
            End If
        End Get
    End Property

    ''' <summary>The consumption quantity of the first fuel</summary>
    ''' <remarks>
    ''' 
    ''' [Fuel quantity] = [Fuel quantity at end] - [Fuel quantity at start]
    ''' 
    ''' </remarks>
    Public ReadOnly Property FUEL_CONSUMED_QUANTITY_1 As Double
        Get
            If (Me.FUEL_QUANTITY_AT_START_1.Equals(INVALID_QUANTITY) OrElse _
               Me.FUEL_QUANTITY_AT_END_1.Equals(INVALID_QUANTITY)) Then

                Return INVALID_QUANTITY
            Else

                Return Me.FUEL_QUANTITY_AT_END_1 - Me.FUEL_QUANTITY_AT_START_1
            End If
        End Get
    End Property

    ''' <summary>The consumption quantity of the second fuel</summary>
    ''' <remarks>
    ''' 
    ''' [Fuel quantity] = [Fuel quantity at end] - [Fuel quantity at start]
    ''' 
    ''' </remarks>
    Public ReadOnly Property FUEL_CONSUMED_QUANTITY_2 As Double
        Get
            If (Me.FUEL_QUANTITY_AT_START_2.Equals(INVALID_QUANTITY) OrElse _
               Me.FUEL_QUANTITY_AT_END_2.Equals(INVALID_QUANTITY)) Then

                Return INVALID_QUANTITY
            Else

                Return Me.FUEL_QUANTITY_AT_END_2 - Me.FUEL_QUANTITY_AT_START_2
            End If
        End Get
    End Property

    ''' <summary>The consumption rate of the first fuel</summary>
    ''' <remarks>
    ''' 
    ''' [Fuel consumption rate] = [Fuel quantity] / [Produced quantity]
    ''' 
    ''' </remarks>
    Public ReadOnly Property FUEL_CONSUMPTION_RATE_1 As Double
        Get

            If (Me.FUEL_CONSUMED_QUANTITY_1.Equals(INVALID_QUANTITY)) Then

                Return INVALID_QUANTITY
            Else

                Return Me.FUEL_CONSUMED_QUANTITY_1 / Me.PRODUCED_QUANTITY
            End If
        End Get
    End Property

    ''' <summary>The consumption rate of the second fuel</summary>
    ''' <remarks>
    ''' 
    ''' [Fuel consumption rate] = [Fuel quantity] / [Produced quantity]
    ''' 
    ''' </remarks>
    Public ReadOnly Property FUEL_CONSUMPTION_RATE_2 As Double
        Get

            If (Me.FUEL_CONSUMED_QUANTITY_2.Equals(INVALID_QUANTITY)) Then

                Return INVALID_QUANTITY
            Else

                Return Me.FUEL_CONSUMED_QUANTITY_2 / Me.PRODUCED_QUANTITY
            End If
        End Get
    End Property

End Class
