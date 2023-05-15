using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaxCalculator.Central.WebApplication.Models;

namespace TaxCalculator.Central.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Models.APIModels.GetPostalCodeList getPostalCodeList = new Models.APIModels.GetPostalCodeList();
            getPostalCodeList.RequestUri = "api/tax/getPostalCodeList";
            ViewBag.PostalCodeList = getPostalCodeList.GetEncodedContent("http://localhost:5178/");
            return View(new Central.Models.TaxInputDTO());
        }

        [HttpPost]
        public IActionResult GetTaxCalculation(Central.Models.TaxInputDTO taxInputDTO)
        {
            if (ModelState.IsValid)
            {
                Models.APIModels.GetTaxCalculationDTO getTaxCalculationDTO = new Models.APIModels.GetTaxCalculationDTO();
                getTaxCalculationDTO.RequestUri = "api/tax/calculateTax";
                var result = getTaxCalculationDTO.PostJsonEncodedContent("http://localhost:5178/", taxInputDTO);
                return View("TaxCalculation", result);
            }
            else
            {
                Models.APIModels.GetPostalCodeList getPostalCodeList = new Models.APIModels.GetPostalCodeList();
                getPostalCodeList.RequestUri = "api/tax/getPostalCodeList";
                ViewBag.PostalCodeList = getPostalCodeList.GetEncodedContent("http://localhost:5178/");
                return View("Index", taxInputDTO);

            }
        }

        public IActionResult SubmitTax()
        {
            Models.APIModels.GetPostalCodeList getPostalCodeList = new Models.APIModels.GetPostalCodeList();
            getPostalCodeList.RequestUri = "api/tax/getPostalCodeList";
            ViewBag.PostalCodeList = getPostalCodeList.GetEncodedContent("http://localhost:5178/");
            return View(new Central.Models.TaxInputDTO());
        }

        [HttpPost]
        public IActionResult SubmitTaxCalculation(Central.Models.TaxInputDTO taxInputDTO)
        {
            if (ModelState.IsValid)
            {
                //this is very bad i know
                if(taxInputDTO.RowGuid == Guid.Empty)
                {
                    Models.APIModels.GetTaxCalculationDTO getTaxCalculationDTO = new Models.APIModels.GetTaxCalculationDTO();
                    getTaxCalculationDTO.RequestUri = "api/tax/SubmitTax";
                    var result = getTaxCalculationDTO.PostJsonEncodedContent("http://localhost:5178/", taxInputDTO);
                    return RedirectToAction("SubmittedTax", "Home", new { rowGuid = result.TaxInputGuid });
                }
                else
                {
                    Models.APIModels.GetTaxCalculationDTO getTaxCalculationDTO = new Models.APIModels.GetTaxCalculationDTO();
                    getTaxCalculationDTO.RequestUri = "api/tax/UpdateSubmittedTax";
                    var result = getTaxCalculationDTO.PostJsonEncodedContent("http://localhost:5178/", taxInputDTO);
                    return View("SubmittedTaxCalculation", result);
                }
            }
            else
            {
                Models.APIModels.GetPostalCodeList getPostalCodeList = new Models.APIModels.GetPostalCodeList();
                getPostalCodeList.RequestUri = "api/tax/getPostalCodeList";
                ViewBag.PostalCodeList = getPostalCodeList.GetEncodedContent("http://localhost:5178/");
                return View("SubmitTax", taxInputDTO);
            }
        }
        
        public IActionResult SubmittedTax(Guid rowGuid)
        {
            Models.APIModels.GetSubmittedTax getSubmittedTax = new Models.APIModels.GetSubmittedTax();
            getSubmittedTax.RequestUri = "api/tax/GetSubmittedTax";
            var returnVal = getSubmittedTax.GetEncodedContent("http://localhost:5178/", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("rowGuid", rowGuid.ToString()) });
            return View("SubmittedTaxCalculation", returnVal);
        }

        public IActionResult EditTaxSubmission(Guid rowGuid)
        {
            Models.APIModels.GetPostalCodeList getPostalCodeList = new Models.APIModels.GetPostalCodeList();
            getPostalCodeList.RequestUri = "api/tax/getPostalCodeList";
            ViewBag.PostalCodeList = getPostalCodeList.GetEncodedContent("http://localhost:5178/");
                Models.APIModels.GetSubmittedTax getSubmittedTax = new Models.APIModels.GetSubmittedTax();
            getSubmittedTax.RequestUri = "api/tax/GetSubmittedTax";
            var returnVal = getSubmittedTax.GetEncodedContent("http://localhost:5178/", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("rowGuid", rowGuid.ToString()) });
            Central.Models.TaxInputDTO taxInputDTO = new Central.Models.TaxInputDTO();
            taxInputDTO.Year = returnVal.Year;
            taxInputDTO.RowGuid = rowGuid;
            taxInputDTO.PostalCodeGuid = returnVal.PostalCode.RowGuid;
            taxInputDTO.AnnualAmount = returnVal.AnnualAmount;
            return View("SubmitTax", taxInputDTO);
        }

        
        public IActionResult SubmittedTaxList()
        {
            Models.APIModels.GetTaxCalculationList getTaxCalculationDTO = new Models.APIModels.GetTaxCalculationList();
            getTaxCalculationDTO.RequestUri = "api/tax/GetSubmittedTaxList";
            var result = getTaxCalculationDTO.GetEncodedContent("http://localhost:5178/");
            return View(result);
        }

        public IActionResult DeleteSubmittedTax(Guid rowGuid)
        {
            Models.APIModels.DeleteSubmittedTax getTaxCalculationDTO = new Models.APIModels.DeleteSubmittedTax();
            getTaxCalculationDTO.RequestUri = "api/tax/DeleteSubmittedTax";
            getTaxCalculationDTO.GetEncodedContent("http://localhost:5178/", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("rowGuid", rowGuid.ToString()) });
            return RedirectToAction("SubmittedTaxList", "Home");
        }
    }
}