' <copyright file="IWordAmount.vb" company="Guerche TI">
'     Copyright (c) 2018 All Rights Reserved
' </copyright>
' <author>Luciano Evaristo Guerche</author>
' <email>guercheLE@gmail.com</email>
' <date>August 25th, 2018 00:56</date>
' <summary>
' </summary>

''' <summary>
''' Word Amount interface.
''' </summary>
Public Interface IWordAmount

#Region "Public Properties"

    ''' <summary>
    ''' Gets or sets the currency word singular.
    ''' </summary>
    ''' <value>The currency word singular.</value>
    Property CurrencyWordSingular As String

    ''' <summary>
    ''' Gets or sets the currency word plural.
    ''' </summary>
    ''' <value>The currency word plural.</value>
    Property CurrencyWordPlural As String

    ''' <summary>
    ''' Gets or sets the conjunction for cents.
    ''' </summary>
    ''' <value>The conjunction for cents.</value>
    Property ConjunctionForCents As String

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Gets word amount for the specified value.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="firstLetterUppercase">if set to <c>true</c> [first letter uppercase].</param>
    ''' <returns>Word Amount for the specified value</returns>
    Function [Get](ByVal value As Double, ByVal Optional firstLetterUppercase As Boolean = False) As String

#End Region

End Interface