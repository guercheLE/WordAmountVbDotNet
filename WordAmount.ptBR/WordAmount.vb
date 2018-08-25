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
''' Word Amount class implementation for Brazilian Portuguese.
''' </summary>
''' <seealso cref="WordAmount.Common.WordAmount"/>
<Culture("pt-BR")>
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

        partWord(CInt(PartCategory.Cents), CInt(NumberType.Singular)) = "centavo"
        partWord(CInt(PartCategory.Cents), CInt(NumberType.Plural)) = "centavos"
        partWord(CInt(PartCategory.Units), CInt(NumberType.Singular)) = String.Empty
        partWord(CInt(PartCategory.Units), CInt(NumberType.Plural)) = String.Empty
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Singular)) = "mil"
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Plural)) = "mil"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Singular)) = "milhão"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Plural)) = "milhões"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Singular)) = "bilhão"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Plural)) = "bilhões"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Singular)) = "trilhão"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Plural)) = "trilhões"

        amountWord(0) = "zero"
        amountWord(1) = "um"
        amountWord(2) = "dois"
        amountWord(3) = "três"
        amountWord(4) = "quatro"
        amountWord(5) = "cinco"
        amountWord(6) = "seis"
        amountWord(7) = "sete"
        amountWord(8) = "oito"
        amountWord(9) = "nove"
        amountWord(10) = "dez"
        amountWord(11) = "onze"
        amountWord(12) = "doze"
        amountWord(13) = "treze"
        amountWord(14) = "quatorze"
        amountWord(15) = "quinze"
        amountWord(16) = "dezesseis"
        amountWord(17) = "dezessete"
        amountWord(18) = "dezoito"
        amountWord(19) = "dezenove"
        amountWord(20) = "vinte"
        amountWord(30) = "trinta"
        amountWord(40) = "quarenta"
        amountWord(50) = "cinqüenta"
        amountWord(60) = "sessenta"
        amountWord(70) = "setenta"
        amountWord(80) = "oitenta"
        amountWord(90) = "noventa"
        amountWord(100) = "cento"
        amountWord(200) = "duzentos"
        amountWord(300) = "trezentos"
        amountWord(400) = "quatrocentos"
        amountWord(500) = "quinhentos"
        amountWord(600) = "seiscentos"
        amountWord(700) = "setecentos"
        amountWord(800) = "oitocentos"
        amountWord(900) = "novecentos"

        For amountIndex As Integer = 21 To 100 - 1
            If amountWord(amountIndex) Is Nothing Then
                amountWord(amountIndex) = amountWord((amountIndex \ 10) * 10) & conjunctionForParts & amountWord(amountIndex Mod 10)
            End If
        Next

        For amountIndex As Integer = 101 To 1000 - 1
            If amountWord(amountIndex) Is Nothing Then
                amountWord(amountIndex) = amountWord((amountIndex \ 100) * 100) & conjunctionForParts & amountWord(amountIndex - ((amountIndex \ 100) * 100))
            End If
        Next

        amountWord(100) = "cem"
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="WordAmount"/> class.
    ''' </summary>
    Public Sub New()
        MyBase.ConjunctionForCents = " e "
        MyBase.currencyWord(CInt(NumberType.Singular)) = "real"
        MyBase.currencyWord(CInt(NumberType.Plural)) = "reais"
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

        For partIndex As Integer = 0 To parts.Count - 1
            valueAmountWord = amountWord(parts(partIndex).Value)
            addCurrency = Not currencyAdded AndAlso ((partIndex = parts.Count - 1 AndAlso parts(partIndex).Category <> CInt(PartCategory.Cents)) OrElse (partIndex < parts.Count - 1 AndAlso parts(partIndex + 1).Category = CInt(PartCategory.Cents)))

            If partIndex = 0 AndAlso firstLetterUppercase Then
                valueAmountWord = valueAmountWord.Substring(0, 1).ToUpperInvariant() & valueAmountWord.Substring(1, valueAmountWord.Length - 1)
            End If

            valueToReturn.Append(valueAmountWord).Append(If(parts(partIndex).Value <> 1, (" " & partWord(CInt(parts(partIndex).Category), CInt(NumberType.Plural))).TrimEnd(), (" " & partWord(CInt(parts(partIndex).Category), CInt(NumberType.Singular))).TrimEnd()))

            If addCurrency Then
                valueToReturn.Append(" ").Append(If(value > 1.99, (If(parts(partIndex).Category >= PartCategory.Millions, "de ", "")) & currencyWord(CInt(NumberType.Plural)), currencyWord(CInt(NumberType.Singular))))
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