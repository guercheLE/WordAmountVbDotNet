' <copyright file="CultureInfoAttribute.vb" company="Guerche TI">
'     Copyright (c) 2018 All Rights Reserved
' </copyright>
' <author>Luciano Evaristo Guerche</author>
' <email>guercheLE@gmail.com</email>
' <date>August 25th, 2018 00:56</date>
' <summary>
' </summary>

''' <summary>
''' Culture attribute class.
''' </summary>
''' <seealso cref="System.Attribute"/>
<AttributeUsage(AttributeTargets.[Class], AllowMultiple:=False)>
Public Class CultureAttribute
    Inherits Attribute

#Region "Public Properties"

    ''' <summary>
    ''' Gets the culture.
    ''' </summary>
    ''' <value>The culture.</value>
    Public Property Culture As String

#End Region

#Region "Public Constructors"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="CultureAttribute"/> class.
    ''' </summary>
    ''' <param name="CultureName">Name of the culture.</param>
    Public Sub New(ByVal CultureName As String)
        Culture = CultureName
    End Sub

#End Region

#Region "Private Constructors"

    ''' <summary>
    ''' Prevents a default instance of the <see cref="CultureAttribute"/> class from being created.
    ''' </summary>
    Private Sub New()
    End Sub

#End Region

End Class