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
''' Word amount class implementation for French.
''' </summary>
''' <seealso cref="WordAmount.Common.WordAmount"/>
<Culture("fr")>
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
        conjunctionForParts = " et "

        partWord(CInt(PartCategory.Cents), CInt(NumberType.Singular)) = "centime"
        partWord(CInt(PartCategory.Cents), CInt(NumberType.Plural)) = "centimes"
        partWord(CInt(PartCategory.Units), CInt(NumberType.Singular)) = String.Empty
        partWord(CInt(PartCategory.Units), CInt(NumberType.Plural)) = String.Empty
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Singular)) = "mille"
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Plural)) = "mille"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Singular)) = "milliard"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Plural)) = "milliards"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Singular)) = "mil millones"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Plural)) = "mil millones"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Singular)) = "billion"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Plural)) = "billions"

        amountWord(0) = "zéro"
        amountWord(1) = "un"
        amountWord(2) = "deux"
        amountWord(3) = "trois"
        amountWord(4) = "quatre"
        amountWord(5) = "cinq"
        amountWord(6) = "six"
        amountWord(7) = "sept"
        amountWord(8) = "huit"
        amountWord(9) = "neuf"
        amountWord(10) = "dix"
        amountWord(11) = "onze"
        amountWord(12) = "douze"
        amountWord(13) = "treize"
        amountWord(14) = "quatorze"
        amountWord(15) = "quinze"
        amountWord(16) = "seize"
        amountWord(17) = "dix-sept"
        amountWord(18) = "dix-huit"
        amountWord(19) = "dix-neuf"
        amountWord(20) = "vingt"
        amountWord(21) = "vingt et un"
        amountWord(22) = "vingt-deux"
        amountWord(23) = "vingt-trois"
        amountWord(24) = "vingt-quatre"
        amountWord(25) = "vingt-cinq"
        amountWord(26) = "vingt-six"
        amountWord(27) = "vingt-sept"
        amountWord(28) = "vingt-huit"
        amountWord(29) = "vingt-neuf"
        amountWord(30) = "trente"
        amountWord(31) = "trente et un"
        amountWord(32) = "trente-deux"
        amountWord(33) = "trente-trois"
        amountWord(34) = "trente-quatre"
        amountWord(35) = "trente-cinq"
        amountWord(36) = "trente-six"
        amountWord(37) = "trente-sept"
        amountWord(38) = "trente-huit"
        amountWord(39) = "trente-neuf"
        amountWord(40) = "quarante"
        amountWord(41) = "quarante et un"
        amountWord(42) = "quarante-deux"
        amountWord(43) = "quarante-trois"
        amountWord(44) = "quarante-quatre"
        amountWord(45) = "quarante-cinq"
        amountWord(46) = "quarante-six"
        amountWord(47) = "quarante-sept"
        amountWord(48) = "quarante-huit"
        amountWord(49) = "quarante-neuf"
        amountWord(50) = "cinquante"
        amountWord(51) = "cinquante et un"
        amountWord(52) = "cinquante-deux"
        amountWord(53) = "cinquante-trois"
        amountWord(54) = "cinquante-quatre"
        amountWord(55) = "cinquante-cinq"
        amountWord(56) = "cinquante-six"
        amountWord(57) = "cinquante-sept"
        amountWord(58) = "cinquante-huit"
        amountWord(59) = "cinquante-neuf"
        amountWord(60) = "soixante"
        amountWord(61) = "soixante et un"
        amountWord(62) = "soixante-deux"
        amountWord(63) = "soixante-trois"
        amountWord(64) = "soixante-quatre"
        amountWord(65) = "soixante-cinq"
        amountWord(66) = "soixante-six"
        amountWord(67) = "soixante-sept"
        amountWord(68) = "soixante-huit"
        amountWord(69) = "soixante-neuf"
        amountWord(70) = "soixante-dix"
        amountWord(71) = "soixante et onze"
        amountWord(72) = "soixante-douze"
        amountWord(73) = "soixante-treize"
        amountWord(74) = "soixante-quatorze"
        amountWord(75) = "soixante-quinze"
        amountWord(76) = "soixante-seize"
        amountWord(77) = "soixante-dix-sept"
        amountWord(78) = "soixante-dix-huit"
        amountWord(79) = "soixante-dix-neuf"
        amountWord(80) = "quatre-vingts"
        amountWord(81) = "quatre-vingt et un"
        amountWord(82) = "quatre-vingt-deux"
        amountWord(83) = "quatre-vingt-trois"
        amountWord(84) = "quatre-vingt-quatre"
        amountWord(85) = "quatre-vingt-cinq"
        amountWord(86) = "quatre-vingt-six"
        amountWord(87) = "quatre-vingt-sept"
        amountWord(88) = "quatre-vingt-huit"
        amountWord(89) = "quatre-vingt-neuf"
        amountWord(90) = "quatre-vingt-dix"
        amountWord(91) = "quatre-vingt-onze"
        amountWord(92) = "quatre-vingt-douze"
        amountWord(93) = "quatre-vingt-treize"
        amountWord(94) = "quatre-vingt-quatorze"
        amountWord(95) = "quatre-vingt-quinze"
        amountWord(96) = "quatre-vingt-seize"
        amountWord(97) = "quatre-vingt-dix-sept"
        amountWord(98) = "quatre-vingt-dix-huit"
        amountWord(99) = "quatre-vingt-dix-neuf"
        amountWord(100) = "cent"
        amountWord(200) = "deux cents"
        amountWord(300) = "trois cents"
        amountWord(400) = "quatre cents"
        amountWord(500) = "cinq cents"
        amountWord(600) = "six cents"
        amountWord(700) = "sept cents"
        amountWord(800) = "huit cents"
        amountWord(900) = "neuf cents"
        Dim hundredWord As String

        For amountIndex As Integer = 101 To 1000 - 1
            If amountWord(amountIndex) Is Nothing Then
                hundredWord = amountWord((amountIndex \ 100) * 100)
                amountWord(amountIndex) = (If(hundredWord.EndsWith("s"c), hundredWord.Substring(0, hundredWord.Length - 1), hundredWord)) & " " & amountWord(amountIndex - ((amountIndex \ 100) * 100))
            End If
        Next
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="WordAmount"/> class.
    ''' </summary>
    Public Sub New()
        MyBase.ConjunctionForCents = " et "
        MyBase.currencyWord(CInt(NumberType.Singular)) = "franc"
        MyBase.currencyWord(CInt(NumberType.Plural)) = "francs"
    End Sub

#End Region

#Region "Protected Methods"

    ''' <summary>
    ''' Gets the word amount for specified value.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="parts">The parts.</param>
    ''' <param name="firstLetterUppercase">if set to <c>true</c> [first letter uppercase].</param>
    ''' <returns>Word amount for specified value.</returns>
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
                valueToReturn.Append(" ").Append(If(value > 1.99, (If(parts(partIndex).Category >= PartCategory.Millions, (If(currencyWord(CInt(NumberType.Plural)) Is "Euros", "d'", "de ")), "")) & currencyWord(CInt(NumberType.Plural)), currencyWord(CInt(NumberType.Singular))))
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