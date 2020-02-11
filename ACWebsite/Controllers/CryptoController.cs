using ACWebsite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
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

            returnStats EmpInfo = new returnStats();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("products/LTC-GBP/stats");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmpInfo = JsonConvert.DeserializeObject<returnStats>(EmpResponse);

                }
                //returning the employee list to view  
                return View(EmpInfo);
            }
        }
    }
}