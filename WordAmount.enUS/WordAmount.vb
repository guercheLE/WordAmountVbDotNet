' <copyright file="WordAmount.vb" company="Guerche TI">
'     Copyright (c) 2018 All Rights Reserved
' </copyright>
' <author>Luciano Evaristo Guerche</author>
' <email>guercheLE@gmail.com</email>
' <date>August 25th, 2018 00:56</date>
' <summary>
' </summary>
Imports System.Text
Imports WordAmount.Common

''' <summary>
''' The word amount class implementation for American English.
''' </summary>
''' <seealso cref="WordAmount.Common.WordAmount"/>
<Culture("en-US")>
Public Class WordAmount
    Inherits Common.WordAmount

#Region "Protected Fields"

    ''' <summary>
    ''' The part word
    ''' </summary>
    Protected Shared ReadOnly partWord As String(,) = New String(5, 1) {}

    ''' <summary>
    ''' The amount word
    ''' </summary>
    Protected Shared ReadOnly amountWord As String() = New String(999) {}

    ''' <summary>
    ''' The conjunction for parts
    ''' </summary>
    Protected Shared ReadOnly conjunctionForParts As String

#End Region

#Region "Public Constructors"

    Shared Sub New()
        conjunctionForParts = " and "

        partWord(CInt(PartCategory.Cents), CInt(NumberType.Singular)) = "cent"
        partWord(CInt(PartCategory.Cents), CInt(NumberType.Plural)) = "cents"
        partWord(CInt(PartCategory.Units), CInt(NumberType.Singular)) = String.Empty
        partWord(CInt(PartCategory.Units), CInt(NumberType.Plural)) = String.Empty
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Singular)) = "thousand"
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Plural)) = "thousand"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Singular)) = "million"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Plural)) = "million"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Singular)) = "billion"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Plural)) = "billion"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Singular)) = "trillion"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Plural)) = "trillion"

        amountWord(0) = "zero"
        amountWord(1) = "one"
        amountWord(2) = "two"
        amountWord(3) = "three"
        amountWord(4) = "four"
        amountWord(5) = "five"
        amountWord(6) = "six"
        amountWord(7) = "seven"
        amountWord(8) = "eight"
        amountWord(9) = "nine"
        amountWord(10) = "ten"
        amountWord(11) = "eleven"
        amountWord(12) = "twelve"
        amountWord(13) = "thirteen"
        amountWord(14) = "fourteen"
        amountWord(15) = "fifteen"
        amountWord(16) = "sixteen"
        amountWord(17) = "seventeen"
        amountWord(18) = "eighteen"
        amountWord(19) = "nineteen"
        amountWord(20) = "twenty"
        amountWord(30) = "thirty"
        amountWord(40) = "forty"
        amountWord(50) = "fifty"
        amountWord(60) = "sixty"
        amountWord(70) = "seventy"
        amountWord(80) = "eighty"
        amountWord(90) = "ninety"
        amountWord(100) = "one hundred"
        amountWord(200) = "two hundred"
        amountWord(300) = "three hundred"
        amountWord(400) = "four hundred"
        amountWord(500) = "five hundred"
        amountWord(600) = "six hundred"
        amountWord(700) = "seven hundred"
        amountWord(800) = "eight hundred"
        amountWord(900) = "nine hundred"

        For amountIndex As Integer = 21 To 100 - 1
            If amountWord(amountIndex) Is Nothing Then
                amountWord(amountIndex) = amountWord((amountIndex \ 10) * 10) & "-" & amountWord(amountIndex Mod 10)
            End If
        Next

        For amountIndex As Integer = 101 To 1000 - 1
            If amountWord(amountIndex) Is Nothing Then
                amountWord(amountIndex) = amountWord((amountIndex \ 100) * 100) & " " & amountWord(amountIndex - ((amountIndex \ 100) * 100))
            End If
        Next
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="WordAmount"/> class.
    ''' </summary>
    Public Sub New()
        MyBase.ConjunctionForCents = " and "
        MyBase.currencyWord(CInt(NumberType.Singular)) = "dollar"
        MyBase.currencyWord(CInt(NumberType.Plural)) = "dollars"
    End Sub

#End Region

#Region "Protected Methods"

    ''' <summary>
    ''' Gets word amount for the specified value.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="parts">The parts.</param>
    ''' <param name="firstLetterUppercase">if set to <c>true</c> [first letter uppercase].</param>
    ''' <returns>Word amount for the specified value.</returns>
    Protected Overrides Function [Get](ByVal value As Double, ByVal parts As IList(Of Part), ByVal Optional firstLetterUppercase As Boolean = False) As String
        Dim addCurrency As Boolean = False
        Dim currencyAdded As Boolean = False
        Dim valueToReturn As StringBuilder = New StringBuilder()
        Dim valueAmountWord As String = String.Empty

        For partIndex As Integer = 0 To parts.Count - 1
            valueAmountWord = amountWord(parts(partIndex).Value)
            addCurrency = Not currencyAdded AndAlso ((partIndex = parts.Count - 1 AndAlso parts(partIndex).Category <> CInt(PartCategory.Cents)) OrElse (partIndex < parts.Count - 1 AndAlso parts(partIndex + 1).Category = CInt(PartCategory.Cents)))

            If partIndex = 0 AndAlso firstLetterUppercase Then
                valueAmountWord = valueAmountWord.Substring(0, 1).ToUpperInvariant() & valueAmountWord.Substring(1, valueAmountWord.Length - 1)
            End If

            valueToReturn.Append(valueAmountWord).Append(If(parts(partIndex).Value <> 1, (" " & partWord(CInt(parts(partIndex).Category), CInt(NumberType.Plural))).TrimEnd(), (" " & partWord(CInt(parts(partIndex).Category), CInt(NumberType.Singular))).TrimEnd()))

            If addCurrency Then
                valueToReturn.Append(" ").Append(If(value > 1.99, currencyWord(CInt(NumberType.Plural)), currencyWord(CInt(NumberType.Singular))))
                currencyAdded = True
            End If

            If partIndex = parts.Count - 3 AndAlso parts(partIndex + 1).Category = PartCategory.Units Then
                valueToReturn.Append(conjunctionForParts)
            ElseIf partIndex = parts.Count - 2 Then
                valueToReturn.Append(If(parts(partIndex + 1).Category = CInt(PartCategory.Cents), ConjunctionForCents, conjunctionForParts))
            ElseIf partIndex < parts.Count - 2 Then
                valueToReturn.Append(", ")
            End If
        Next

        Return valueToReturn.ToString()
    End Function

#End Region

End Class