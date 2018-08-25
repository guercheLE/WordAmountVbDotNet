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
''' Word Amount implementation in German.
''' </summary>
''' <seealso cref="WordAmount.Common.WordAmount"/>
<Culture("de")>
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

    ''' <summary>
    ''' Initializes the <see cref="WordAmount"/> class.
    ''' </summary>
    Shared Sub New()
        conjunctionForParts = "und"

        partWord(CInt(PartCategory.Cents), CInt(NumberType.Singular)) = "Pfennig"
        partWord(CInt(PartCategory.Cents), CInt(NumberType.Plural)) = "Pfennige"
        partWord(CInt(PartCategory.Units), CInt(NumberType.Singular)) = String.Empty
        partWord(CInt(PartCategory.Units), CInt(NumberType.Plural)) = String.Empty
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Singular)) = "Tausend"
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Plural)) = "Tausend"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Singular)) = "Million"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Plural)) = "Millionen"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Singular)) = "Milliarde"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Plural)) = "Milliarden"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Singular)) = "Billion"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Plural)) = "Billionen"

        amountWord(0) = "null"
        amountWord(1) = "ein"
        amountWord(2) = "zwei"
        amountWord(3) = "drei"
        amountWord(4) = "vier"
        amountWord(5) = "fünf"
        amountWord(6) = "sechs"
        amountWord(7) = "sieben"
        amountWord(8) = "acht"
        amountWord(9) = "neun"
        amountWord(10) = "zehn"
        amountWord(11) = "elf"
        amountWord(12) = "zwölf"
        amountWord(13) = "dreizehn"
        amountWord(14) = "vierzehn"
        amountWord(15) = "fünfzehn"
        amountWord(16) = "sechzehn"
        amountWord(17) = "siebzehn"
        amountWord(18) = "achtzehn"
        amountWord(19) = "neunzehn"
        amountWord(20) = "zwanzig"
        amountWord(30) = "dreißig"
        amountWord(40) = "vierzig"
        amountWord(50) = "fünfzig"
        amountWord(60) = "sechzig"
        amountWord(70) = "siebzig"
        amountWord(80) = "achtzig"
        amountWord(90) = "neunzig"
        amountWord(100) = "einhundert"
        amountWord(200) = "zweihundert"
        amountWord(300) = "dreihundert"
        amountWord(400) = "vierhundert"
        amountWord(500) = "fünfhundert"
        amountWord(600) = "sechshundert"
        amountWord(700) = "siebenhundert"
        amountWord(800) = "achthundert"
        amountWord(900) = "neunhundert"

        For amountIndex As Integer = 21 To 100 - 1
            If amountWord(amountIndex) Is Nothing Then
                amountWord(amountIndex) = amountWord(amountIndex Mod 10) & conjunctionForParts & amountWord((amountIndex \ 10) * 10)
            End If
        Next

        For amountIndex As Integer = 101 To 1000 - 1
            If amountWord(amountIndex) Is Nothing Then
                amountWord(amountIndex) = amountWord((amountIndex \ 100) * 100) & amountWord(amountIndex - ((amountIndex \ 100) * 100))
            End If
        Next

        conjunctionForParts = " und "
        amountWord(1) = "eine"
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="WordAmount"/> class.
    ''' </summary>
    Public Sub New()
        MyBase.ConjunctionForCents = " und "
        MyBase.currencyWord(CInt(NumberType.Singular)) = "Mark"
        MyBase.currencyWord(CInt(NumberType.Plural)) = "Mark"
    End Sub

#End Region

#Region "Protected Methods"

    ''' <summary>
    ''' Gets the specified value.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="parts">The parts.</param>
    ''' <param name="firstLetterUppercase">if set to <c>true</c> [first letter uppercase].</param>
    ''' <returns></returns>
    Protected Overrides Function [Get](ByVal value As Double, ByVal parts As IList(Of Part), ByVal Optional firstLetterUppercase As Boolean = False) As String
        Dim addCurrency As Boolean = False
        Dim currencyAdded As Boolean = False
        Dim valueToReturn As StringBuilder = New StringBuilder()
        Dim valueAmountWord As String = String.Empty

        For partIndex As Integer = 0 To parts.Count - 1
            valueAmountWord = amountWord(parts(partIndex).Value)
            addCurrency = Not currencyAdded AndAlso ((partIndex = parts.Count - 1 AndAlso parts(partIndex).Category <> CInt(PartCategory.Cents)) OrElse (partIndex < parts.Count - 1 AndAlso parts(partIndex + 1).Category = CInt(PartCategory.Cents)))

            If valueAmountWord Is "eine" AndAlso (addCurrency OrElse parts(partIndex).Category = CInt(PartCategory.Cents)) Then
                valueAmountWord = "ein"
            End If

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