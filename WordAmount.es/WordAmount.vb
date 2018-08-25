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

<Culture("es")>
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
        conjunctionForParts = " y "

        partWord(CInt(PartCategory.Cents), CInt(NumberType.Singular)) = "céntimo"
        partWord(CInt(PartCategory.Cents), CInt(NumberType.Plural)) = "céntimos"
        partWord(CInt(PartCategory.Units), CInt(NumberType.Singular)) = String.Empty
        partWord(CInt(PartCategory.Units), CInt(NumberType.Plural)) = String.Empty
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Singular)) = "mil"
        partWord(CInt(PartCategory.Thousands), CInt(NumberType.Plural)) = "mil"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Singular)) = "millón"
        partWord(CInt(PartCategory.Millions), CInt(NumberType.Plural)) = "millones"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Singular)) = "mil millones"
        partWord(CInt(PartCategory.Billions), CInt(NumberType.Plural)) = "mil millones"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Singular)) = "billón"
        partWord(CInt(PartCategory.Trillions), CInt(NumberType.Plural)) = "billones"

        amountWord(0) = "cero"
        amountWord(1) = "uno"
        amountWord(2) = "dos"
        amountWord(3) = "tres"
        amountWord(4) = "cuatro"
        amountWord(5) = "cinco"
        amountWord(6) = "seises"
        amountWord(7) = "siete"
        amountWord(8) = "ocho"
        amountWord(9) = "nueve"
        amountWord(10) = "diez"
        amountWord(11) = "once"
        amountWord(12) = "doce"
        amountWord(13) = "trece"
        amountWord(14) = "catorce"
        amountWord(15) = "quince"
        amountWord(16) = "dieciséis"
        amountWord(17) = "diecisiete"
        amountWord(18) = "dieciocho"
        amountWord(19) = "diecinueve"
        amountWord(20) = "veinte"
        amountWord(21) = "veintiuno"
        amountWord(22) = "veintidós"
        amountWord(23) = "veintitrés"
        amountWord(24) = "veinticuatro"
        amountWord(25) = "veinticinco"
        amountWord(26) = "veintiséis"
        amountWord(27) = "veintisiete"
        amountWord(28) = "veintiocho"
        amountWord(29) = "veintinueve"
        amountWord(30) = "treinta"
        amountWord(40) = "cuarenta"
        amountWord(50) = "cincuenta"
        amountWord(60) = "sesenta"
        amountWord(70) = "setenta"
        amountWord(80) = "ochenta"
        amountWord(90) = "noventa"
        amountWord(100) = "ciento"
        amountWord(200) = "doscientos"
        amountWord(300) = "trescientos"
        amountWord(400) = "cuatrocientos"
        amountWord(500) = "quinientos"
        amountWord(600) = "seiscientos"
        amountWord(700) = "setecientos"
        amountWord(800) = "ochocientos"
        amountWord(900) = "novecientos"

        For amountIndex As Integer = 21 To 100 - 1
            If amountWord(amountIndex) Is Nothing Then
                amountWord(amountIndex) = amountWord((amountIndex \ 10) * 10) & conjunctionForParts & amountWord(amountIndex Mod 10)
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
        MyBase.ConjunctionForCents = " con "
        MyBase.currencyWord(CInt(NumberType.Singular)) = "peseta"
        MyBase.currencyWord(CInt(NumberType.Plural)) = "pesetas"
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
        Dim valueCurrencyWord As String = String.Empty
        Dim forceConjunctionForParts As Boolean = False

        For partIndex As Integer = 0 To parts.Count - 1
            valueAmountWord = amountWord(parts(partIndex).Value)
            addCurrency = Not currencyAdded AndAlso ((partIndex = parts.Count - 1 AndAlso parts(partIndex).Category <> CInt(PartCategory.Cents)) OrElse (partIndex < parts.Count - 1 AndAlso parts(partIndex + 1).Category = CInt(PartCategory.Cents)))

            If addCurrency Then
                valueCurrencyWord = If(value > 1.99, currencyWord(CInt(NumberType.Plural)), currencyWord(CInt(NumberType.Singular)))

                If valueAmountWord Is "uno" AndAlso parts(partIndex).Category = PartCategory.Units Then
                    valueAmountWord = "una"
                End If
            End If

            If partIndex = 0 AndAlso firstLetterUppercase Then
                valueAmountWord = valueAmountWord.Substring(0, 1).ToUpperInvariant() & valueAmountWord.Substring(1, valueAmountWord.Length - 1)
            End If

            If parts(partIndex).Category >= PartCategory.Billions AndAlso (CInt(parts(partIndex).Category) - 4) Mod 2 = 0 AndAlso partIndex + 1 < parts.Count - 1 AndAlso parts(partIndex).Category = parts(partIndex + 1).Category + 1 Then
                valuePartWord = If(parts(partIndex).Value <> 1, partWord(CInt(PartCategory.Thousands), CInt(NumberType.Plural)), partWord(CInt(PartCategory.Thousands), CInt(NumberType.Singular)))
                forceConjunctionForParts = True
            Else
                valuePartWord = If(parts(partIndex).Value <> 1, partWord(CInt(parts(partIndex).Category), CInt(NumberType.Plural)), partWord(CInt(parts(partIndex).Category), CInt(NumberType.Singular)))
                forceConjunctionForParts = False
            End If

            valueToReturn.Append(valueAmountWord).Append((" " & valuePartWord).TrimEnd())

            If addCurrency Then
                valueToReturn.Append(" ").Append(valueCurrencyWord)
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