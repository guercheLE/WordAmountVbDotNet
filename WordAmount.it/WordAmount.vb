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
''' Word amount class implementation for Italian.
''' </summary>
''' <seealso cref="WordAmount.Common.WordAmount"/>
<Culture("it")>
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
        conjunctionForParts = " e "

        partWord(CInt(PartCategory.Cents), CInt(NumberType.Singular)) = "centesimo"
        partWord(CInt(PartCategory.Cents), CInt(NumberType.Plural)) = "centesimi"
        partWord(CInt(PartCategory.Units), CInt(NumberType.Singular)) = String.Empty
        partWord(CInt(PartCategory.Units), CInt(NumberType.Plural)) = String.Empty
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Singular)) = "mille"
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Plural)) = "mila"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Singular)) = "milione"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Plural)) = "milioni"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Singular)) = "miliardo"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Plural)) = "miliardi"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Singular)) = "trilione"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Plural)) = "trilioni"

        amountWord(0) = "zero"
        amountWord(1) = "uno"
        amountWord(2) = "due"
        amountWord(3) = "tre"
        amountWord(4) = "quattro"
        amountWord(5) = "cinque"
        amountWord(6) = "sei"
        amountWord(7) = "sette"
        amountWord(8) = "otto"
        amountWord(9) = "nove"
        amountWord(10) = "dieci"
        amountWord(11) = "undici"
        amountWord(12) = "dodici"
        amountWord(13) = "tredici"
        amountWord(14) = "quattordici"
        amountWord(15) = "quindici"
        amountWord(16) = "sedici"
        amountWord(17) = "diciassette"
        amountWord(18) = "diciotto"
        amountWord(19) = "diciannove"
        amountWord(20) = "venti"
        amountWord(30) = "trenta"
        amountWord(40) = "quaranta"
        amountWord(50) = "cinquanta"
        amountWord(60) = "sessanta"
        amountWord(70) = "settanta"
        amountWord(80) = "ottanta"
        amountWord(90) = "novanta"
        amountWord(100) = "cento"
        amountWord(200) = "duecento"
        amountWord(300) = "trecento"
        amountWord(400) = "quattrocento"
        amountWord(500) = "cinquecento"
        amountWord(600) = "seicento"
        amountWord(700) = "settecento"
        amountWord(800) = "ottocento"
        amountWord(900) = "novecento"
        Dim tenthWord As String
        Dim hundredWord As String

        For amountIndex As Integer = 21 To 100 - 1
            If amountWord(amountIndex) Is Nothing Then
                tenthWord = amountWord(amountIndex Mod 10)
                hundredWord = amountWord((amountIndex \ 10) * 10)
                amountWord(amountIndex) = (If("aeiou".Contains(tenthWord.Substring(0, 1)), hundredWord.Substring(0, hundredWord.Length - 1), hundredWord)) & tenthWord
            End If
        Next

        For amountIndex As Integer = 101 To 1000 - 1
            If amountWord(amountIndex) Is Nothing Then
                amountWord(amountIndex) = amountWord((amountIndex \ 100) * 100) & amountWord(amountIndex - ((amountIndex \ 100) * 100))
            End If
        Next
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="WordAmount"/> class.
    ''' </summary>
    Public Sub New()
        MyBase.ConjunctionForCents = " e "
        MyBase.currencyWord(CInt(NumberType.Singular)) = "lira"
        MyBase.currencyWord(CInt(NumberType.Plural)) = "lire"
    End Sub

#End Region

#Region "Protected Methods"

    ''' <summary>
    ''' Gets the word amount for the specified value.
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
        Dim valuePartWord As String = String.Empty
        Dim forceConjunctionForParts As Boolean = False

        For partIndex As Integer = 0 To parts.Count - 1
            valueAmountWord = amountWord(parts(partIndex).Value)
            addCurrency = Not currencyAdded AndAlso ((partIndex = parts.Count - 1 AndAlso parts(partIndex).Category <> CInt(PartCategory.Cents)) OrElse (partIndex < parts.Count - 1 AndAlso parts(partIndex + 1).Category = CInt(PartCategory.Cents)))

            If addCurrency Then
                If valueAmountWord Is "uno" AndAlso parts(partIndex).Category = PartCategory.Units Then
                    valueAmountWord = "una"
                End If
            End If

            If partIndex = 0 AndAlso firstLetterUppercase Then
                valueAmountWord = valueAmountWord.Substring(0, 1).ToUpperInvariant() & valueAmountWord.Substring(1, valueAmountWord.Length - 1)
            End If

            If (CInt(parts(partIndex).Category) - 4) Mod 2 = 0 AndAlso partIndex + 1 < parts.Count - 1 AndAlso parts(partIndex).Category = parts(partIndex + 1).Category + 1 Then
                valuePartWord = If(parts(partIndex).Value <> 1, partWord(CInt(PartCategory.Thousands), CInt(NumberType.Plural)), partWord(CInt(PartCategory.Thousands), CInt(NumberType.Singular)))
                forceConjunctionForParts = True
            Else
                valuePartWord = If(parts(partIndex).Value <> 1, partWord(CInt(parts(partIndex).Category), CInt(NumberType.Plural)), partWord(CInt(parts(partIndex).Category), CInt(NumberType.Singular)))
                forceConjunctionForParts = False
            End If

            valueToReturn.Append(valueAmountWord).Append((" " & valuePartWord).TrimEnd())

            If addCurrency Then
                valueToReturn.Append(" ").Append(If(value > 1.99, (If(parts(partIndex).Category >= PartCategory.Millions, "di ", "")) & currencyWord(CInt(NumberType.Plural)), currencyWord(CInt(NumberType.Singular))))
                currencyAdded = True
            End If

            If forceConjunctionForParts OrElse (partIndex = parts.Count - 3 AndAlso parts(partIndex + 1).Category = PartCategory.Units) Then
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