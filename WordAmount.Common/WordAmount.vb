' <copyright file="WordAmount.vb" company="Guerche TI">
'     Copyright (c) 2018 All Rights Reserved
' </copyright>
' <author>Luciano Evaristo Guerche</author>
' <email>guercheLE@gmail.com</email>
' <date>August 25th, 2018 00:56</date>
' <summary>
' </summary>

''' <summary>
''' Word Amount abstract class.
''' </summary>
''' <seealso cref="WordAmount.Common.IWordAmount"/>
Public MustInherit Class WordAmount
    Implements IWordAmount

#Region "Protected Fields"

    ''' <summary>
    ''' The currency word
    ''' </summary>
    Protected currencyWord As String() = New String(1) {}

    ''' <summary>
    ''' The conjunction for cents
    ''' </summary>
    Protected _conjunctionForCents As String

#End Region

#Region "Public Properties"

    ''' <summary>
    ''' Gets or sets the currency word singular.
    ''' </summary>
    ''' <value>The currency word singular.</value>
    Public Property CurrencyWordSingular As String Implements IWordAmount.CurrencyWordSingular
        Get
            Return currencyWord(CInt(NumberType.Singular))
        End Get
        Set(ByVal value As String)
            currencyWord(CInt(NumberType.Singular)) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the currency word plural.
    ''' </summary>
    ''' <value>The currency word plural.</value>
    Public Property CurrencyWordPlural As String Implements IWordAmount.CurrencyWordPlural
        Get
            Return currencyWord(CInt(NumberType.Plural))
        End Get
        Set(ByVal value As String)
            currencyWord(CInt(NumberType.Plural)) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the conjunction for cents.
    ''' </summary>
    ''' <value>The conjunction for cents.</value>
    Public Property ConjunctionForCents As String Implements IWordAmount.ConjunctionForCents
        Get
            Return _conjunctionForCents
        End Get
        Set(ByVal value As String)
            _conjunctionForCents = value
        End Set
    End Property

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Gets word amount for the specified value.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="firstLetterUppercase">if set to <c>true</c> [first letter uppercase].</param>
    ''' <returns>Word Amount for the specified value</returns>
    Public Function [Get](ByVal value As Double, ByVal Optional firstLetterUppercase As Boolean = False) As String Implements IWordAmount.Get
        If value = 0.00 Then
            Return String.Empty
        End If

        Dim parts As IList(Of Part) = ParseValueIntoParts(value)
        Return [Get](value, parts, firstLetterUppercase)
    End Function

#End Region

#Region "Protected Methods"

    ''' <summary>
    ''' Parses the value into parts.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns></returns>
    Protected Function ParseValueIntoParts(ByVal value As Double) As IList(Of Part)
        Dim valueToReturn As List(Of Part) = New List(Of Part)()
        Dim formattedValue As String = value.ToString("#,##0.00")
        Dim paddingLength As Integer = (4 - ((formattedValue.Length - 2) Mod 4)) Mod 4

        If paddingLength <> 0 Then
            formattedValue = formattedValue.PadLeft(formattedValue.Length + paddingLength, "0"c)
        End If

        Dim integerPartsIndexCount As Integer = (formattedValue.Length - 2) / 4

        For index As Integer = 0 To integerPartsIndexCount * 4 - 1 Step 4

            If formattedValue.Substring(index, 3) <> "000" Then
                valueToReturn.Add(New Part With {
                    .Category = CType((integerPartsIndexCount - (index / 4)), PartCategory),
                    .Value = Integer.Parse(formattedValue.Substring(index, 3))
                })
            End If
        Next

        If formattedValue.Substring(formattedValue.Length - 2, 2) <> "00" Then
            valueToReturn.Add(New Part With {
                .Category = PartCategory.Cents,
                .Value = Integer.Parse(formattedValue.Substring(formattedValue.Length - 2, 2))
            })
        End If

        Return valueToReturn
    End Function

    ''' <summary>
    ''' Gets Word Amount for the specified value..
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="parts">The parts.</param>
    ''' <param name="firstLetterUppercase">if set to <c>true</c> [first letter uppercase].</param>
    ''' <returns>Word Amount for the specified value.</returns>
    Protected MustOverride Function [Get](ByVal value As Double, ByVal parts As IList(Of Part), ByVal Optional firstLetterUppercase As Boolean = False) As String

#End Region

End Class