' <copyright file="CultureInfoAttributeTests.vb" company="Guerche TI">
'     Copyright (c) 2018 All Rights Reserved
' </copyright>
' <author>Luciano Evaristo Guerche</author>
' <email>guercheLE@gmail.com</email>
' <date>August 25th, 2018 00:56</date>
' <summary>
' </summary>
Imports Microsoft.VisualStudio.TestTools.UnitTesting

''' <summary>
''' Culture Info attribute class tests
''' </summary>
<TestClass>
Public Class CultureInfoAttributeTests

#Region "Public Methods"

    ''' <summary>
    ''' Constructor and culture property test.
    ''' </summary>
    <TestMethod>
    Public Sub ConstructorAndCulturePropertyTest()
        Dim cultureAttribute As CultureAttribute = New CultureAttribute("en-US")
        Assert.AreEqual("en-US", cultureAttribute.Culture)
    End Sub

#End Region

End Class