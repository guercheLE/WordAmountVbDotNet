' <copyright file="WordAmountFactory.vb" company="Guerche TI">
'     Copyright (c) 2018 All Rights Reserved
' </copyright>
' <author>Luciano Evaristo Guerche</author>
' <email>guercheLE@gmail.com</email>
' <date>August 25th, 2018 00:56</date>
' <summary>
' </summary>
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Loader
Imports WordAmount.Common

''' <summary>
''' Word Amount factory class.
''' </summary>
Public Module WordAmountFactory

    Private ReadOnly availableWordAmount As Dictionary(Of String, Type)

    ''' <summary>
    ''' Initializes the <see cref="WordAmountFactory"/> WordAmountFactory class.
    ''' </summary>
    Sub New()
        availableWordAmount = New Dictionary(Of String, Type)()

        Dim codeBase As String = Assembly.GetExecutingAssembly().CodeBase
        Dim assemblyPath As String = Path.GetDirectoryName(New Uri(codeBase).LocalPath)

        For Each libraryName As String In Directory.GetFiles(assemblyPath, "*.dll")
            Dim assembly As Assembly = AssemblyLoadContext.[Default].LoadFromAssemblyPath(libraryName)

            For Each wordAmountType As Type In assembly.GetTypes().Where(Function(t) GetType(IWordAmount).IsAssignableFrom(t) AndAlso t.CustomAttributes.Any(Function(ca) ca.AttributeType Is GetType(CultureAttribute)))
                Dim culture As CustomAttributeData = wordAmountType.CustomAttributes.First(Function(ca) ca.AttributeType Is GetType(CultureAttribute))
                availableWordAmount.Add(culture.ConstructorArguments(0).Value.ToString(), wordAmountType)
            Next
        Next
    End Sub

    ''' <summary>
    ''' Available cultures.
    ''' </summary>
    ''' <returns>Available cultures</returns>
    Public Function AvailableCultures() As CultureInfo()
        Return availableWordAmount.[Select](Function(awa) New CultureInfo(awa.Key)).ToArray()
    End Function

    ''' <summary>
    ''' Creates the specified culture.
    ''' </summary>
    ''' <param name="culture">The culture.</param>
    ''' <returns>Word Amount class in the specified culture.</returns>
    ''' <exception cref="ArgumentException"></exception>
    Public Function Create(ByVal culture As CultureInfo) As IWordAmount
        Dim type As Type = Nothing
        Dim awaFound As Boolean = availableWordAmount.TryGetValue(culture.Name, type)

        If Not awaFound Then
            Throw New ArgumentException($"Word Amount for culture '{culture.Name}' not found/available")
        End If

        Return TryCast(Activator.CreateInstance(type), IWordAmount)
    End Function

End Module