using NUnit.Framework;

namespace TaxCalculator.Central.Tests
{
    [TestFixture]
    public class TaxCalculation
    {
        //we stored the postalcodes with its values in the DB the following is a mapping of this:

        //RowGuid                               PostalCode  TaxCalculationTypeId
        //04934A29-9A54-4669-9A11-57D515245B8A	7441    	1
        //26B6B1AC-8722-437F-943E-0B21C08B186D A100	        2
        //CE81A5A4-746F-4AB7-9257-83E4D8585104	7000	    3
        //4F459C4E-42AB-4E4E-A22F-301471D21723	1000	    1
//ta calculation types
//RowId Name
//1	Progressive  
//2	FlatValue   
//3	FlatRate   
        [Test]
        public void TestProgressiveTaxAboveLimit()
        {
            TaxCalculator.Central.Models.TaxInputDTO taxInputDTO = new Models.TaxInputDTO();

            taxInputDTO.AnnualAmount = 400000;
            taxInputDTO.PostalCodeGuid = Guid.Parse("04934A29-9A54-4669-9A11-57D515245B8A");
            taxInputDTO.Year = 2022;
            BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic("Data Source=localhost;initial catalog=Dev_TaxCalculator;Integrated security=True;TrustServerCertificate=True");

            var taxCalculation = taxLogic.CalculateTax(taxInputDTO);
            Assert.That(taxCalculation.TaxPayableAmount == (decimal)117682.14);
        }

        [Test]
        public void TestProgressiveTaxBelowLimit()
        {
            TaxCalculator.Central.Models.TaxInputDTO taxInputDTO = new Models.TaxInputDTO();

            taxInputDTO.AnnualAmount = 100000;
            taxInputDTO.PostalCodeGuid = Guid.Parse("04934A29-9A54-4669-9A11-57D515245B8A");
            taxInputDTO.Year = 2022;
            BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic("Data Source=localhost;initial catalog=Dev_TaxCalculator;Integrated security=True;TrustServerCertificate=True");

            var taxCalculation = taxLogic.CalculateTax(taxInputDTO);
            Assert.That(taxCalculation.TaxPayableAmount == (decimal)21719.32);
        }

        [Test]
        public void TestFlatRateTaxAboveLimit()
        {
            TaxCalculator.Central.Models.TaxInputDTO taxInputDTO = new Models.TaxInputDTO();

            taxInputDTO.AnnualAmount = 400000;
            taxInputDTO.PostalCodeGuid = Guid.Parse("26B6B1AC-8722-437F-943E-0B21C08B186D");
            taxInputDTO.Year = 2022;
            BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic("Data Source=localhost;initial catalog=Dev_TaxCalculator;Integrated security=True;TrustServerCertificate=True");

            var taxCalculation = taxLogic.CalculateTax(taxInputDTO);
            Assert.That(taxCalculation.TaxPayableAmount == 10000);
        }


        [Test]
        public void TestFlatRateTaxBelowLimit()
        {
            TaxCalculator.Central.Models.TaxInputDTO taxInputDTO = new Models.TaxInputDTO();

            taxInputDTO.AnnualAmount = 199999;
            taxInputDTO.PostalCodeGuid = Guid.Parse("26B6B1AC-8722-437F-943E-0B21C08B186D");
            taxInputDTO.Year = 2022;
            BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic("Data Source=localhost;initial catalog=Dev_TaxCalculator;Integrated security=True;TrustServerCertificate=True");

            var taxCalculation = taxLogic.CalculateTax(taxInputDTO);
            Assert.That(taxCalculation.TaxPayableAmount == (decimal)9999.95);
        }

        [Test]
        public void TestFlatAmountTax()
        {
            TaxCalculator.Central.Models.TaxInputDTO taxInputDTO = new Models.TaxInputDTO();

            taxInputDTO.AnnualAmount = 20000;
            taxInputDTO.PostalCodeGuid = Guid.Parse("CE81A5A4-746F-4AB7-9257-83E4D8585104");
            taxInputDTO.Year = 2022;
            BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic("Data Source=localhost;initial catalog=Dev_TaxCalculator;Integrated security=True;TrustServerCertificate=True");

            var taxCalculation = taxLogic.CalculateTax(taxInputDTO);
            Assert.That(taxCalculation.TaxPayableAmount == 3500);
        }
    }
}