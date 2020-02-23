using ACWebsite.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACWebsite.Controllers
{
    public class CryptoController : Controller
    {
        // GET: Crpyto
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult LTC()
        {
            // Calling the API method from the BTC controller method, and passing in new parameter
            returnStats result = Task.Run(async () => await CallAPI("products/LTC-GBP/stats")).GetAwaiter().GetResult();
            return View(result);
        }

        public ActionResult BTC()
        {
            // Calling the API method from the BTC controller method, and passing in new parameter
            returnStats result = Task.Run(async () => await CallAPI("products/BTC-GBP/stats")).GetAwaiter().GetResult();
            return View(result);
        }

        //Created a single API call method for easy reuse.
        public async Task<returnStats> CallAPI(string productlist){

            returnStats Crypto = new returnStats();

            using (var client = new HttpClient())
            {
                //Passing service base url - the Coinbase Pro API
                client.BaseAddress = new Uri("https://api.pro.coinbase.com/");

                client.DefaultRequestHeaders.Clear();
                //Define request data format - this is JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Coinbase Pro API requires a User-Agent defined
                client.DefaultRequestHeaders.Add("User-Agent", "Anthony Cook");
                //Sending request to find the stats which are passed in from the relevant method call.  
                HttpResponseMessage Res = await client.GetAsync(productlist);
                //Checking the response is successful or not
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from the API 
                    var CryptoResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the CryptoResponse recieved from the API and storing it in Crypto Object
                    Crypto = JsonConvert.DeserializeObject<returnStats>(CryptoResponse);

                    //adding value to the Model.
                    //Q: Why is this price calculation working the wrong way around?
                    Crypto.growthtoday = Crypto.last - Crypto.open;

                    //TODO: add the percentage of Day Growth / Loss

                    // Convert the data to be 2 decimal places only
                    Crypto.growthtoday = Math.Round(Crypto.growthtoday, 2);
                    Crypto.volume = Math.Round(Crypto.volume, 2);
                    Crypto.volume_30day = Math.Round(Crypto.volume_30day, 2);

                }
                //returning the employee list to view  
                return Crypto;
            }

        }
    }
}