' <copyright file="WordAmountFactoryTests.vb" company="Guerche TI">
'     Copyright (c) 2018 All Rights Reserved
' </copyright>
' <author>Luciano Evaristo Guerche</author>
' <email>guercheLE@gmail.com</email>
' <date>August 25th, 2018 00:56</date>
' <summary>
' </summary>
Imports System.Globalization
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WordAmount.Common

''' <summary>
''' Word Amount factory tests
''' </summary>
<TestClass>
Public Class WordAmountFactoryTests

#Region "Public Methods"

    ''' <summary>
    ''' Availables the cultures test.
    ''' </summary>
    <TestMethod>
    Public Sub AvailableCulturesTest()
        Dim availableCultures As CultureInfo() = WordAmountFactory.AvailableCultures()
        Assert.IsTrue(availableCultures.Length > 0)
    End Sub

    ''' <summary>
    ''' Creates the test with invalid culture.
    ''' </summary>
    <TestMethod>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub CreateTestWithInvalidCulture()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("il"))
    End Sub

    ''' <summary>
    ''' Creates the test with available culture.
    ''' </summary>
    <TestMethod>
    Public Sub CreateTestWithAvailableCulture()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(WordAmountFactory.AvailableCultures()(0))
        Assert.IsNotNull(wordAmount)
    End Sub

    ''' <summary>
    ''' Gets the test with culture DE #001.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureDE001()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("de"))
        Dim actual As String = wordAmount.[Get](0.00, True)
        Assert.AreEqual(String.Empty, actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture DE #002.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureDE002()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("de"))
        Dim actual As String = wordAmount.[Get](0.01, True)
        Assert.AreEqual("Ein Pfennig", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture DE #003.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureDE003()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("de"))
        Dim actual As String = wordAmount.[Get](1.0, True)
        Assert.AreEqual("Ein Mark", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture DE #004.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureDE004()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("de"))

        wordAmount.CurrencyWordSingular = "Euro"
        Assert.AreEqual("Euro", wordAmount.CurrencyWordSingular)

        wordAmount.CurrencyWordPlural = "Euros"
        Assert.AreEqual("Euros", wordAmount.CurrencyWordPlural)

        Dim actual As String = wordAmount.[Get](1.0, True)
        Assert.AreEqual("Ein Euro", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture DE #005.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureDE005()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("de"))
        Dim actual As String = wordAmount.[Get](1000.0, True)
        Assert.AreEqual("Ein Tausend Mark", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture DE #006.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureDE006()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("de"))
        Dim actual As String = wordAmount.[Get](1000000.0, True)
        Assert.AreEqual("Ein Million Mark", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture DE #007.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureDE007()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("de"))
        Dim actual As String = wordAmount.[Get](1001001.01, True)
        Assert.AreEqual("Eine Million, eine Tausend und ein Mark und ein Pfennig", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture DE #008.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureDE008()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("de"))
        Dim actual As String = wordAmount.[Get](254743062813.0, True)
        Assert.AreEqual("Zweihundertvierundfünfzig Milliarden, siebenhundertdreiundvierzig Millionen, zweiundsechzig Tausend und achthundertdreizehn Mark", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ENUS #001.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureENUS001()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("en-US"))
        Dim actual As String = wordAmount.[Get](0.01, True)
        Assert.AreEqual("One cent", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ENUS #002.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureENUS002()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("en-US"))
        Dim actual As String = wordAmount.[Get](1.0, True)
        Assert.AreEqual("One dollar", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ENUS #003.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureENUS003()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("en-US"))
        Dim actual As String = wordAmount.[Get](1000.0, True)
        Assert.AreEqual("One thousand dollars", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ENUS #004.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureENUS004()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("en-US"))
        Dim actual As String = wordAmount.[Get](1000000.0, True)
        Assert.AreEqual("One million dollars", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ENUS #005.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureENUS005()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("en-US"))
        Dim actual As String = wordAmount.[Get](1001001.01, True)
        Assert.AreEqual("One million, one thousand and one dollars and one cent", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ENUS #006.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureENUS006()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("en-US"))
        Dim actual As String = wordAmount.[Get](254743062813.0, True)
        Assert.AreEqual("Two hundred fifty-four billion, seven hundred forty-three million, sixty-two thousand and eight hundred thirteen dollars", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ES #001.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureES001()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("es"))
        Dim actual As String = wordAmount.[Get](0.01, True)
        Assert.AreEqual("Uno céntimo", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ES #002.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureES002()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("es"))
        Dim actual As String = wordAmount.[Get](1.0, True)
        Assert.AreEqual("Una peseta", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ES #003.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureES003()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("es"))
        Dim actual As String = wordAmount.[Get](1000.0, True)
        Assert.AreEqual("Uno mil pesetas", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ES #004.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureES004()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("es"))
        Dim actual As String = wordAmount.[Get](1000000.0, True)
        Assert.AreEqual("Uno millón pesetas", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ES #005.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureES005()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("es"))
        Dim actual As String = wordAmount.[Get](1001001.01, True)
        Assert.AreEqual("Uno millón, uno mil y una pesetas con uno céntimo", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ES #006.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureES006()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("es"))
        Dim actual As String = wordAmount.[Get](1002034056.0, True)
        Assert.AreEqual("Uno mil y dos millones, treinta y cuatro mil y cincuenta y seises pesetas", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ES #007.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureES007()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("es"))
        Dim actual As String = wordAmount.[Get](2001034056.0, True)
        Assert.AreEqual("Dos mil y uno millón, treinta y cuatro mil y cincuenta y seises pesetas", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture ES #008.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureES008()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("es"))
        Dim actual As String = wordAmount.[Get](254743062813.0, True)
        Assert.AreEqual("Doscientos cincuenta y cuatro mil y setecientos cuarenta y tres millones, sesenta y dos mil y ochocientos trece pesetas", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture FR #001.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureFR001()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("fr"))
        Dim actual As String = wordAmount.[Get](0.01, True)
        Assert.AreEqual("Un centime", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture FR #002.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureFR002()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("fr"))
        Dim actual As String = wordAmount.[Get](1.0, True)
        Assert.AreEqual("Un franc", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture FR #003.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureFR003()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("fr"))
        Dim actual As String = wordAmount.[Get](1000.0, True)
        Assert.AreEqual("Un mille francs", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture FR #004.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureFR004()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("fr"))
        Dim actual As String = wordAmount.[Get](1000000.0, True)
        Assert.AreEqual("Un milliard de francs", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture FR #005.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureFR005()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("fr"))
        Dim actual As String = wordAmount.[Get](1001001.01, True)
        Assert.AreEqual("Un milliard, un mille et un francs et un centime", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture FR #006.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureFR006()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("fr"))
        Dim actual As String = wordAmount.[Get](254743062813.0, True)
        Assert.AreEqual("Deux cent cinquante-quatre mil millones, sept cent quarante-trois milliards, soixante-deux mille et huit cent treize francs", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture FR #007.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureFR007()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("fr"))
        wordAmount.CurrencyWordSingular = "Euro"
        wordAmount.CurrencyWordPlural = "Euros"

        Dim actual As String = wordAmount.[Get](1000000.0, True)
        Assert.AreEqual("Un milliard d'Euros", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture IT #001.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureIT001()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("it"))
        Dim actual As String = wordAmount.[Get](0.01, True)
        Assert.AreEqual("Uno centesimo", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture IT #002.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureIT002()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("it"))
        Dim actual As String = wordAmount.[Get](1.0, True)
        Assert.AreEqual("Una lira", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture IT #003.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureIT003()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("it"))
        Dim actual As String = wordAmount.[Get](1000.0, True)
        Assert.AreEqual("Uno mille lire", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture IT #004.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureIT004()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("it"))
        Dim actual As String = wordAmount.[Get](1000000.0, True)
        Assert.AreEqual("Uno milione di lire", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture IT #005.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureIT005()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("it"))
        Dim actual As String = wordAmount.[Get](1001001.01, True)
        Assert.AreEqual("Uno milione, uno mille e una lire e uno centesimo", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture IT #006.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureIT006()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("it"))
        Dim actual As String = wordAmount.[Get](1002034056.0, True)
        Assert.AreEqual("Uno mille e due milioni, trentaquattro mila e cinquantasei lire", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture IT #007.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureIT007()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("it"))
        Dim actual As String = wordAmount.[Get](2001034056.0, True)
        Assert.AreEqual("Due mila e uno milione, trentaquattro mila e cinquantasei lire", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture IT #008.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCultureIT008()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("it"))
        Dim actual As String = wordAmount.[Get](254743062813.0, True)
        Assert.AreEqual("Duecentocinquantaquattro mila e settecentoquarantatre milioni, sessantadue mila e ottocentotredici lire", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture PTBR #001.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCulturePTBR001()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("pt-BR"))
        Dim actual As String = wordAmount.[Get](0.01, True)
        Assert.AreEqual("Um centavo", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture PTBR #002.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCulturePTBR002()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("pt-BR"))
        Dim actual As String = wordAmount.[Get](1.0, True)
        Assert.AreEqual("Um real", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture PTBR #003.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCulturePTBR003()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("pt-BR"))
        Dim actual As String = wordAmount.[Get](1000.0, True)
        Assert.AreEqual("Um mil reais", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture PTBR #004.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCulturePTBR004()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("pt-BR"))
        Dim actual As String = wordAmount.[Get](1000000.0, True)
        Assert.AreEqual("Um milhão de reais", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture PTBR #005.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCulturePTBR005()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("pt-BR"))
        Dim actual As String = wordAmount.[Get](1001001.01, True)
        Assert.AreEqual("Um milhão, um mil e um reais e um centavo", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture PTBR #006.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCulturePTBR006()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("pt-BR"))
        Dim actual As String = wordAmount.[Get](254743062813.0, True)
        Assert.AreEqual("Duzentos e cinqüenta e quatro bilhões, setecentos e quarenta e três milhões, sessenta e dois mil e oitocentos e treze reais", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture PTBR #007.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCulturePTBR007()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("pt-BR"))
        Dim actual As String = wordAmount.[Get](1.01, True)
        Assert.AreEqual("Um real e um centavo", actual)
    End Sub

    ''' <summary>
    ''' Gets the test with culture PTBR #008.
    ''' </summary>
    <TestMethod>
    Public Sub GetTestWithCulturePTBR008()
        Dim wordAmount As IWordAmount = WordAmountFactory.Create(CultureInfo.GetCultureInfo("pt-BR"))

        wordAmount.ConjunctionForCents = " com "
        Assert.AreEqual(" com ", wordAmount.ConjunctionForCents)

        Dim actual As String = wordAmount.[Get](1.01, True)
        Assert.AreEqual("Um real com um centavo", actual)
    End Sub

#End Region

End Class