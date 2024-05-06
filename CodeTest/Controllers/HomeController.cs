using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Quote.Contracts;
using Quote.Models;
using PruebaIngreso.Models;
using LightInject;

namespace PruebaIngreso.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuoteEngine quote;

        public HomeController(IQuoteEngine quote)
        {
            this.quote = quote;
           
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            var request = new TourQuoteRequest
            {
                adults = 1,
                ArrivalDate = DateTime.Now.AddDays(1),
                DepartingDate = DateTime.Now.AddDays(2),
                getAllRates = true,
                GetQuotes = true,
                RetrieveOptions = new TourQuoteRequestOptions
                {
                    GetContracts = true,
                    GetCalculatedQuote = true,
                },
                TourUri= "E-U10-PRVPARKTRF",
                Language = Language.Spanish
            };

            var result = this.quote.Quote(request);
            var tour = result.Tours.FirstOrDefault();
            ViewBag.Message = "Test 1 Correcto";
            return View(tour);
        }

        public ActionResult Test2()
        {
            ViewBag.Message = "Test 2 Correcto";
            return View();
        }

        public async Task<ActionResult> Test3( string code="")
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
           // string code = "E-E10-PF2SHOW 500"; //"E-U10-DSCVCOVE 404"; //"E-U10-UNILATIN 204";
            string apiUrl = "https://refactored-pancake.free.beeceptor.com/margin/";
            string fullUrl = apiUrl + code;
            if( !string.IsNullOrEmpty(code))
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(fullUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        ViewBag.Margin = responseBody;
                    }
                    else
                    {
                        ViewBag.Margin = "{ margin: 0.0 }";
                    }
                }
                catch (HttpRequestException e)
                {

                    ViewBag.Margin = "{ margin: 0.0 }";
                }
            }

            

            return View();
        }

        public ActionResult Test4()
        {
             var request = new TourQuoteRequest
            {
                adults = 1,
                ArrivalDate = DateTime.Now.AddDays(1),
                DepartingDate = DateTime.Now.AddDays(2),
                getAllRates = true,
                GetQuotes = true,
                RetrieveOptions = new TourQuoteRequestOptions
                {
                    GetContracts = true,
                    GetCalculatedQuote = true,
                },
                Language = Language.Spanish
            };

            var result = this.quote.Quote(request);
            /*
            decimal margin = marginProvider.GetMargin("E-U10-UNILATIN");*/
            return View(result.TourQuotes);
        }
    }
}