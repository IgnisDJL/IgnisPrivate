
Namespace Constants.UI.Images

    Public Class IconFactory

        Private Shared ReadOnly _csvFile As New IconInformation(_32x32.CSV_FILE, _
                                                                _24x24.CSV_FILE, _
                                                                _16x16.CSV_FILE, _
                                                                Icons2.CSV_FILE, _
                                                                CSVFile.GENERIC_NAME)

        Private Shared ReadOnly _logFile As New IconInformation(_32x32.LOG_FILE, _
                                                                _24x24.LOG_FILE, _
                                                                _16x16.LOG_FILE, _
                                                                Icons2.LOG_FILE, _
                                                                LOGFile.GENERIC_NAME)

        Private Shared ReadOnly _mdbFile As New IconInformation(_32x32.MDB_FILE, _
                                                                _24x24.MDB_FILE, _
                                                                _16x16.MDB_FILE, _
                                                                Icons2.MDB_FILE, _
                                                                MDBFile.GENERIC_NAME)

        Private Shared ReadOnly _eventsFile As New IconInformation(_32x32.EVENT_FILE, _
                                                                   _24x24.EVENT_FILE, _
                                                                   _16x16.EVENT_FILE, _
                                                                   Icons2.EVENT_FILE, _
                                                                   EventsFile.GENERIC_NAME)

        Private Shared ReadOnly _readOnlySummaryDailyReport As New IconInformation(_32x32.READONLY_SUMMARY_DAILY_REPORT, _
                                                                                    _24x24.READONLY_SUMMARY_DAILY_REPORT, _
                                                                                    _16x16.READONLY_SUMMARY_DAILY_REPORT, _
                                                                                    Icons2.READONLY_SUMMARY_DAILY_REPORT, _
                                                                                    SummaryDailyReport.GENERIC_NAME & SummaryDailyReport.READONLY_EXTENSION)


        Private Shared ReadOnly _writableSummaryDailyReport As New IconInformation(_32x32.SUMMARY_DAILY_REPORT, _
                                                                                   _24x24.SUMMARY_DAILY_REPORT, _
                                                                                   _16x16.SUMMARY_DAILY_REPORT, _
                                                                                   Icons2.SUMMARY_DAILY_REPORT, _
                                                                                   SummaryDailyReport.GENERIC_NAME & SummaryDailyReport.WRITABLE_EXTENSION)

        Private Shared ReadOnly _readOnlySummaryPeriodicReport As New IconInformation(_32x32.READONLY_SUMMARY_PERIODIC_REPORT, _
                                                                                       _24x24.READONLY_SUMMARY_PERIODIC_REPORT, _
                                                                                       _16x16.READONLY_SUMMARY_PERIODIC_REPORT, _
                                                                                       Icons2.READONLY_SUMMARY_PERIODIC_REPORT, _
                                                                                       SummaryPeriodicReport.GENERIC_NAME & SummaryPeriodicReport.READONLY_EXTENSION)

        Private Shared ReadOnly _writableSummaryPeriodicReport As New IconInformation(_32x32.SUMMARY_PERIODIC_REPORT, _
                                                                                       _24x24.SUMMARY_PERIODIC_REPORT, _
                                                                                       _16x16.SUMMARY_PERIODIC_REPORT, _
                                                                                       Icons2.SUMMARY_PERIODIC_REPORT, _
                                                                                       SummaryPeriodicReport.GENERIC_NAME & SummaryPeriodicReport.WRITABLE_EXTENSION)

        Private Shared ReadOnly _completeDailyReport As New IconInformation(_32x32.COMPLETE_DAILY_REPORT, _
                                                                           _24x24.COMPLETE_DAILY_REPORT, _
                                                                           _16x16.COMPLETE_DAILY_REPORT, _
                                                                           Icons2.COMPLETE_DAILY_REPORT, _
                                                                           CompleteDailyReport.GENERIC_NAME & CompleteDailyReport.EXTENSION)

        Private Shared ReadOnly _completePeriodicReport As New IconInformation(_32x32.COMPLETE_PERIODIC_REPORT, _
                                                                               _24x24.COMPLETE_PERIODIC_REPORT, _
                                                                               _16x16.COMPLETE_PERIODIC_REPORT, _
                                                                               Icons2.COMPLETE_PERIODIC_REPORT, _
                                                                               CompletePeriodicReport.GENERIC_NAME & CompletePeriodicReport.EXTENSION)

        Public Overloads Shared Function getIconFor(file As File) As IconInformation

            If (TypeOf file Is DataFile) Then

                If (TypeOf file Is CSVFile) Then
                    Return _csvFile

                ElseIf (TypeOf file Is LOGFile) Then
                    Return _logFile

                ElseIf (TypeOf file Is MDBFile) Then
                    Return _mdbFile

                ElseIf (TypeOf file Is EventsFile) Then
                    Return _eventsFile

                End If

            ElseIf (TypeOf file Is ReportFile) Then

                If (TypeOf file Is SummaryDailyReport) Then

                    If (DirectCast(file, SummaryDailyReport).IS_READ_ONLY) Then
                        Return _readOnlySummaryDailyReport
                    Else
                        Return _writableSummaryDailyReport
                    End If

                ElseIf (TypeOf file Is SummaryPeriodicReport) Then

                    If (DirectCast(file, SummaryPeriodicReport).IS_READ_ONLY) Then
                        Return _readOnlySummaryPeriodicReport
                    Else
                        Return _writableSummaryPeriodicReport
                    End If

                ElseIf (TypeOf file Is CompleteDailyReport) Then
                    Return _completeDailyReport
                ElseIf (TypeOf file Is CompletePeriodicReport) Then
                    Return _completePeriodicReport
                End If

            End If

            Debugger.Break()
            Return Nothing
        End Function

        Public Overloads Shared Function getIconFor(reportGenericName As String) As IconInformation

            If (reportGenericName.Equals(CompleteDailyReport.GENERIC_NAME)) Then
                Return _completeDailyReport

            ElseIf (reportGenericName.Equals(CompletePeriodicReport.GENERIC_NAME)) Then
                Return _completePeriodicReport

            ElseIf (reportGenericName.Equals(SummaryDailyReport.GENERIC_NAME)) Then
                Return _writableSummaryDailyReport

            ElseIf (reportGenericName.Equals(SummaryPeriodicReport.GENERIC_NAME)) Then
                Return _writableSummaryPeriodicReport
            End If

            Debugger.Break()
            Return Nothing
        End Function

        Public Class IconInformation

            Private _32x32_Icon As Image
            Private _24x24_Icon As Image
            Private _16x16_Icon As Image
            Private _icon As Icon
            Private _caption As String

            Public Sub New(_32x32_Icon As Image, _
                            _24x24_Icon As Image, _
                            _16x16_Icon As Image, _
                            icon As Icon, _
                            caption As String)

                Me._32x32_Icon = _32x32_Icon
                Me._24x24_Icon = _24x24_Icon
                Me._16x16_Icon = _16x16_Icon
                Me._icon = icon
                Me._caption = caption

            End Sub

            Public ReadOnly Property _32x32 As Image
                Get
                    Return _32x32_Icon
                End Get
            End Property
            Public ReadOnly Property _24x24 As Image
                Get
                    Return _24x24_Icon
                End Get
            End Property
            Public ReadOnly Property _16x16 As Image
                Get
                    Return _16x16_Icon
                End Get
            End Property
            Public ReadOnly Property Icon As Icon
                Get
                    Return _icon
                End Get
            End Property
            Public ReadOnly Property Caption As String
                Get
                    Return _caption
                End Get
            End Property
        End Class

    End Class

End Namespace