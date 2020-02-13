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

        public async Task<ActionResult> LTC()
        {
            string Baseurl = "https://api.pro.coinbase.com/";

            returnStats LTCInfo = new returnStats();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
            
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "AC Website C# MVC");
                //Sending request to find the LTC GBP stats  
                HttpResponseMessage Res = await client.GetAsync("products/LTC-GBP/stats");
                //Checking the response is successful or not
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var LTCResponse = Res.Content.ReadAsStringAsync().Result;
            
                    //Deserializing the response recieved from web api and storing into the Object 
                    LTCInfo = JsonConvert.DeserializeObject<returnStats>(LTCResponse);
            
                }
                //returning the employee list to view  
                return View(LTCInfo);
            }
        }
    }
}