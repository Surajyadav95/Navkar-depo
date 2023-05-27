Imports Microsoft.VisualBasic

Public Class dbAmountInWords
    Public Total As Object ' String
    Public WORDS As String

    Public Challan As Boolean

    Public Crores As String
    Public Lacs As String
    Public Thousands As String
    Public Hundreds As String
    Public Tens As String
    Public Ones As String

    Dim One(9) As String
    Dim Teen(8) As String
    Dim Ten(9) As String
    Dim PART(6) As Integer

    Dim Str_Total As String

    Dim CTR As Integer
    Dim Len_Total As Integer
    Dim abc As Integer
    Dim MSD As Integer

    Public Function RupeesConvert(ByVal Ch_Value As Double)
        Try


            Dim blnPaise As Boolean

            ' One(1) = " ONE"
            ' One(2) = " TWO"
            ' One(3) = " THREE"
            ' One(4) = " FOUR"
            ' One(5) = " FIVE"
            ' One(6) = " SIX"
            ' One(7) = " SEVEN"
            ' One(8) = " EIGHT"
            ' One(9) = " NINE"
            '
            ' Teen(0) = " ELEVEN"
            ' Teen(1) = " TWELVE"
            ' Teen(2) = " THIRTEEN"
            ' Teen(3) = " FOURTEEN"
            ' Teen(4) = " FIFTEEN"
            ' Teen(5) = " SIXTEEN"
            ' Teen(6) = " SEVENTEEN"
            ' Teen(7) = " EIGHTEEN"
            ' Teen(8) = " NINETEEN"
            '
            ' Ten(0) = " TEN"
            ' Ten(1) = " TEN"
            ' Ten(2) = " TWENTY"
            ' Ten(3) = " THIRTY"
            ' Ten(4) = " FORTY"
            ' Ten(5) = " FIFTY"
            ' Ten(6) = " SIXTY"
            ' Ten(7) = " SEVENTY"
            ' Ten(8) = " EIGHTY"
            ' Ten(9) = " NINETY"

            blnPaise = False

            One(0) = " "
            One(1) = " One"
            One(2) = " Two"
            One(3) = " Three"
            One(4) = " Four"
            One(5) = " Five"
            One(6) = " Six"
            One(7) = " Seven"
            One(8) = " Eight"
            One(9) = " Nine"

            Teen(0) = " Eleven"
            Teen(1) = " Twelve"
            Teen(2) = " Thirteen"
            Teen(3) = " Fourteen"
            Teen(4) = " Fifteen"
            Teen(5) = " Sixteen"
            Teen(6) = " Seventeen"
            Teen(7) = " Eighteen"
            Teen(8) = " Nineteen"

            Ten(0) = " Ten"
            Ten(1) = " Ten"
            Ten(2) = " Twenty"
            Ten(3) = " Thirty"
            Ten(4) = " Forty"
            Ten(5) = " Fifty"
            Ten(6) = " Sixty"
            Ten(7) = " Seventy"
            Ten(8) = " Eighty"
            Ten(9) = " Ninety"

            Crores = ""
            Lacs = ""
            Thousands = ""
            Hundreds = ""
            Tens = ""
            Ones = ""

            ' Total = "990106020.99"
            If Ch_Value >= 0 Then
                Total = Ch_Value
            Else
                Total = Mid(Ch_Value, 2)
            End If

            If (Total Is DBNull.Value.ToString) = True Or Len(Trim(Total)) = 0 Then
                'If DBNull(Total) = True Or Len(Trim(Total)) = 0 Then
                WORDS = "ZERO ONLY"
                'Exit Function
            End If

            Total = Format(Total, "#0.00")

            Len_Total = Len(CStr(Total))

            Str_Total = Space(12 - Len_Total) & Total

            If Total = "#0.00" Then
                RupeesConvert = "Zero "
            Else


                If Not CDbl(Total) Then

                    PART(0) = Val(Mid(Str_Total, 1, 2))
                    PART(1) = Val(Mid(Str_Total, 3, 2))
                    PART(2) = Val(Mid(Str_Total, 5, 2))
                    PART(3) = Val(Mid(Str_Total, 7, 1))
                    PART(4) = Val(Mid(Str_Total, 8, 2))
                    PART(5) = Val(Mid(Str_Total, 11, 2))

                    If Not Val(Total) Then
                        WORDS = " "
                        CTR = "0"
                    Else
                        WORDS = " "
                        CTR = 5
                    End If

                    Do While CTR < 6

                        If PART(CTR) <> 0 Then

                            If Challan = False Then        ' challan printing

                                If CTR <> 3 Then
                                    abc = Mid(PART(CTR), 1, 2)
                                Else
                                    abc = Mid((PART(CTR)), 1, 1)
                                End If

                                If PART(CTR) > 0 And PART(CTR) < 10 Then
                                    WORDS = WORDS + (One(abc))
                                ElseIf PART(CTR) > 10 And PART(CTR) < 20 Then
                                    WORDS = WORDS + (Teen(abc - 11))
                                Else
                                    MSD = Mid((PART(CTR)), 1, 1)
                                    WORDS = WORDS + (Ten(MSD))
                                    If Not (PART(CTR)) Then
                                        MSD = Mid((PART(CTR)), 2, 1)
                                        WORDS = WORDS + (One(MSD))
                                    End If
                                End If

                                Select Case CTR
                                    Case "0" : WORDS = WORDS + " Crore"
                                    Case "1" : WORDS = WORDS + " Lakh"
                                    Case "2" : WORDS = WORDS + " Thousand"
                                    Case "3" : WORDS = WORDS + " Hundred"
                                    Case "4"
                                        If PART(5) > 0 Then
                                            blnPaise = True
                                            WORDS = WORDS & " &"
                                        Else
                                            blnPaise = False
                                        End If
                                End Select
                            Else
                                Call StoreValue()
                            End If


                        Else
                            If CTR = 3 Then
                                Hundreds = " Zero"
                            End If
                        End If
                        CTR = Val(CTR + 1)

                    Loop
                Else
                    RupeesConvert = "Zero "
                End If
            End If
            If blnPaise = False Then
                If Ch_Value > 0 Then
                    RupeesConvert = WORDS + " Only"
                Else
                    RupeesConvert = "Minus " + WORDS + " Only"
                End If
            Else
                If Ch_Value > 0 Then
                    RupeesConvert = WORDS + " Fils Only"
                Else
                    RupeesConvert = "Minus " + WORDS + " Fils Only"
                End If
            End If
            Challan = False

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Function
    Sub StoreValue()

        If CTR <> 3 Then
            abc = Mid(PART(CTR), 1, 2)
        Else
            abc = Mid((PART(CTR)), 1, 1)
        End If

        If PART(CTR) > 0 And PART(CTR) < 10 Then
            WORDS = (One(abc))
            Call valOnes()

        ElseIf PART(CTR) > 10 And PART(CTR) < 20 Then
            WORDS = (Teen(abc - 11))
            Call valOnes()
            Call valTens()
        Else
            MSD = Mid((PART(CTR)), 1, 1)
            WORDS = (Ten(MSD))

            Call valTens()

            If Not (PART(CTR)) Then
                MSD = Mid((PART(CTR)), 2, 1)
                If MSD <> 0 Then
                    WORDS = WORDS + (One(MSD))
                End If
            End If

            If CTR = 4 Then
                Ones = One(MSD)
            End If

        End If


        Select Case CTR
            Case 0 : Crores = WORDS
            Case 1 : Lacs = WORDS
            Case 2 : Thousands = WORDS
            Case 3 : Hundreds = WORDS
        End Select

    End Sub

    Sub valTens()

        If CTR = 4 Then
            Tens = One(Left(abc, 1))
        End If

    End Sub

    Sub valOnes()
        If CTR = 4 Then
            If Len(abc) = 2 Then
                Tens = " Zero"
            End If


            If abc > 10 Then
                Ones = One(abc - 10)
            Else
                Ones = One(abc)
            End If
        End If
    End Sub

    Public Function TimeConvert(ByVal Ch_Value As Double)

        One(0) = " "
        One(1) = " One"
        One(2) = " Two"
        One(3) = " Three"
        One(4) = " Four"
        One(5) = " Five"
        One(6) = " Six"
        One(7) = " Seven"
        One(8) = " Eight"
        One(9) = " Nine"

        Teen(0) = " Eleven"
        Teen(1) = " Twelve"
        Teen(2) = " Thirteen"
        Teen(3) = " Fourteen"
        Teen(4) = " Fifteen"
        Teen(5) = " Sixteen"
        Teen(6) = " Seventeen"
        Teen(7) = " Eighteen"
        Teen(8) = " Nineteen"

        Ten(0) = " Ten"
        Ten(1) = " Ten"
        Ten(2) = " Twenty"
        Ten(3) = " Thirty"
        Ten(4) = " Forty"
        Ten(5) = " Fifty"
        Ten(6) = " Sixty"
        Ten(7) = " Seventy"
        Ten(8) = " Eighty"
        Ten(9) = " Ninety"

        Crores = ""
        Lacs = ""
        Thousands = ""
        Hundreds = ""
        Tens = ""
        Ones = ""

        ' Total = "990106020.99"
        If Ch_Value >= 0 Then
            Total = Ch_Value
        Else
            Total = Mid(Ch_Value, 2)
        End If

        If (Total Is DBNull.Value.ToString) = True Or Len(Trim(Total)) = 0 Then
            'If IsNull(Total) = True Or Len(Trim(Total)) = 0 Then
            WORDS = "ZERO "
            'Exit Function
        End If

        Total = Format(Total, "#0.00")

        Len_Total = Len(CStr(Total))

        Str_Total = Space(12 - Len_Total) & Total


        If Not CDbl(Total) Then

            PART(0) = Val(Mid(Str_Total, 1, 2))
            PART(1) = Val(Mid(Str_Total, 3, 2))
            PART(2) = Val(Mid(Str_Total, 5, 2))
            PART(3) = Val(Mid(Str_Total, 7, 1))
            PART(4) = Val(Mid(Str_Total, 8, 2))
            PART(5) = Val(Mid(Str_Total, 11, 2))

            If Not Val(Total) Then
                WORDS = " "
                CTR = "0"
            Else
                WORDS = " "
                CTR = 5
            End If

            Do While CTR < 6

                If PART(CTR) <> 0 Then

                    If Challan = False Then        ' challan printing

                        If CTR <> 3 Then
                            abc = Mid(PART(CTR), 1, 2)
                        Else
                            abc = Mid((PART(CTR)), 1, 1)
                        End If

                        If PART(CTR) > 0 And PART(CTR) < 10 Then
                            WORDS = WORDS + (One(abc))
                        ElseIf PART(CTR) > 10 And PART(CTR) < 20 Then
                            WORDS = WORDS + (Teen(abc - 11))
                        Else
                            MSD = Mid((PART(CTR)), 1, 1)
                            WORDS = WORDS + (Ten(MSD))
                            If Not (PART(CTR)) Then
                                MSD = Mid((PART(CTR)), 2, 1)
                                WORDS = WORDS + (One(MSD))
                            End If
                        End If

                        Select Case CTR
                            Case "0" : WORDS = WORDS + " Crore"
                            Case "1" : WORDS = WORDS + " Lakh"
                            Case "2" : WORDS = WORDS + " Thousand"
                            Case "3" : WORDS = WORDS + " Hundred"
                            Case "4"
                                If PART(5) > 0 Then
                                    WORDS = WORDS + " & "
                                End If
                        End Select
                    Else
                        Call StoreValue()
                    End If


                Else
                    If CTR = 3 Then
                        Hundreds = " Zero"
                    End If
                End If
                CTR = Val(CTR + 1)

            Loop
        Else
            TimeConvert = "Zero "
        End If

        If Ch_Value > 0 Then
            TimeConvert = WORDS + " "
        Else
            TimeConvert = "Minus " + WORDS + " Only"
        End If
        Challan = False

    End Function
End Class
