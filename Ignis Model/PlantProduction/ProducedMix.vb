Public Class ProducedMix
    Implements IEquatable(Of ProducedMix)

    Private mixNumber As String
    Private mixName As String
    Private recordedTemperature As Double
    Private targetTemperature As Double


    '' TODO
    '' Aller récupérer la valeur TargetTemperature pour le ProducedMix pour les fichiers sources: log, csv, marcotte

    'Temps en secondes'
    Private tempsDeProduction As TimeSpan
    Private hotFeederList As List(Of HotFeeder)
    Private virginAsphaltConcrete As VirginAsphaltConcrete
    Private rapAsphaltConcreteList As List(Of RapAsphaltConcrete)

    Sub New(mixNumber As String, mixName As String, recordedTemperature As Double, hotFeederList As List(Of HotFeeder), virginAsphaltConcrete As VirginAsphaltConcrete,
        rapAsphaltConcreteList As List(Of RapAsphaltConcrete), tempsDeProduction As TimeSpan)

        Me.mixNumber = mixNumber
        Me.mixName = mixName
        Me.recordedTemperature = recordedTemperature
        Me.tempsDeProduction = tempsDeProduction
        Me.hotFeederList = hotFeederList
        Me.virginAsphaltConcrete = virginAsphaltConcrete
        Me.rapAsphaltConcreteList = rapAsphaltConcreteList
    End Sub

    Sub New(producedMix As ProducedMix)

        Me.mixNumber = producedMix.getMixNumber
        Me.mixName = producedMix.getMixName
        Me.recordedTemperature = producedMix.getRecordedTemperature
        Me.tempsDeProduction = producedMix.getTempsDeProduction
        Me.hotFeederList = producedMix.getHotFeederList()
        Me.virginAsphaltConcrete = producedMix.getVirginAsphaltConcrete()
        Me.rapAsphaltConcreteList = producedMix.getRapAsphaltConcreteList()
    End Sub

    Public ReadOnly Property getHotFeederList As List(Of HotFeeder)
        Get
            Return hotFeederList
        End Get
    End Property

    Public ReadOnly Property getMixNumber As String
        Get
            Return mixNumber
        End Get
    End Property

    Public ReadOnly Property getMixName As String
        Get
            Return mixName
        End Get
    End Property

    Public ReadOnly Property getTempsDeProduction As TimeSpan
        Get
            Return tempsDeProduction
        End Get
    End Property

    Public ReadOnly Property getRecordedTemperature As Double
        Get
            Return recordedTemperature
        End Get
    End Property

    Public ReadOnly Property getTargetTemperature As Double
        Get
            Return targetTemperature
        End Get
    End Property


    Public ReadOnly Property getMixMass As Double
        Get
            If Me.virginAsphaltConcrete.getTargetPercentage = 0 Then
                Return 0
            Else
                Return getHotFeederMass() + Me.virginAsphaltConcrete.getMass
            End If

        End Get
    End Property

    Public ReadOnly Property getMixDebit As Double
        Get
            Return getMixMass() / getTempsDeProduction.TotalHours
        End Get
    End Property

    Public Sub addMass(hotFeederList As List(Of HotFeeder), virginAsphaltConcrete As VirginAsphaltConcrete, tempsDeProduction As TimeSpan)
        addVirginAsphaltConcrete(virginAsphaltConcrete.getMass)
        addFeederMass(hotFeederList)
        addTempsDeProduction(tempsDeProduction)
    End Sub


    Private Function getHotFeederMass() As Double

        Dim hotFeederMass As Double
        For Each hotFeeder As HotFeeder In Me.hotFeederList

            hotFeederMass += hotFeeder.getMass
        Next

        Return hotFeederMass
    End Function

    Private Sub addFeederMass(hotFeederList As List(Of HotFeeder))
        For Each hotFeeder As HotFeeder In hotFeederList
            If Me.hotFeederList.Contains(hotFeeder) Then
                Me.hotFeederList.ElementAt(Me.hotFeederList.IndexOf(hotFeeder)).addMass(hotFeeder.getMass())
            End If
        Next
    End Sub

    Private Sub addVirginAsphaltConcrete(mass As Double)
        Me.virginAsphaltConcrete.addMass(mass)
    End Sub

    Private Sub addTempsDeProduction(tempsDeProduction As TimeSpan)
        Me.tempsDeProduction += tempsDeProduction
    End Sub

    Public ReadOnly Property getRapAsphaltConcreteList As List(Of RapAsphaltConcrete)
        Get
            Return rapAsphaltConcreteList
        End Get
    End Property

    Public ReadOnly Property getVirginAsphaltConcrete As VirginAsphaltConcrete
        Get
            Return virginAsphaltConcrete
        End Get
    End Property

    Public Overloads Function Equals(ByVal producedMix As ProducedMix) As Boolean Implements IEquatable(Of ProducedMix).Equals
        If Me.mixNumber = producedMix.getMixNumber Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function isHotFeederEmpty() As Boolean
        Dim totalMass As Double

        For Each hotFeeder As HotFeeder In getHotFeederList()
            totalMass += hotFeeder.getMass()
        Next
        If totalMass <= 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
