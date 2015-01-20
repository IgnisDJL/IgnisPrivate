Imports System.Text.RegularExpressions

Namespace Constants

    Public Class AsphaltTemperature

        Private Shared asphaltNameRegex = New Regex("[^\d]*([\d][\d])[\-]([\d][\d])")
        Public Shared ReadOnly Property ASPHALT_NAME_FORMAT_REGEX As Regex
            Get
                Return asphaltNameRegex
            End Get
        End Property

        ''' <summary>
        ''' #comment
        ''' </summary>
        ''' <param name="asphaltName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getAsphaltSetPointTemperature(asphaltName As String) As Double

            If (Not IsNothing(asphaltName)) Then


                ' value cannot be null exception
                Dim match = ASPHALT_NAME_FORMAT_REGEX.Match(asphaltName)

                If (match.Success) Then

                    If ("52".Equals(match.Groups(1).Value) And "34".Equals(match.Groups(2).Value)) Or _
                       ("58".Equals(match.Groups(1).Value) And "28".Equals(match.Groups(2).Value)) Or _
                       ("64".Equals(match.Groups(1).Value) And "22".Equals(match.Groups(2).Value)) Then

                        Return 150

                    ElseIf ("64".Equals(match.Groups(1).Value) And "28".Equals(match.Groups(2).Value)) Or _
                           ("58".Equals(match.Groups(1).Value) And "34".Equals(match.Groups(2).Value)) Or _
                           ("52".Equals(match.Groups(1).Value) And "40".Equals(match.Groups(2).Value)) Then

                        Return 157

                    ElseIf ("70".Equals(match.Groups(1).Value) And "28".Equals(match.Groups(2).Value)) Or _
                           ("64".Equals(match.Groups(1).Value) And "34".Equals(match.Groups(2).Value)) Or _
                           ("58".Equals(match.Groups(1).Value) And "40".Equals(match.Groups(2).Value)) Then

                        Return 163

                    End If

                End If

            End If

            Return Double.NaN

        End Function

    End Class

End Namespace
