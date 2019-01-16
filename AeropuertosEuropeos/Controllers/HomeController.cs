using Airports.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Threading;
using System.Web.UI.WebControls;

namespace AeropuertosEuropeos.Controllers
{


    public class HomeController : Controller
    {
        List<Aeropuerto> AirportInfo = new List<Aeropuerto>();
        private static System.Timers.Timer aTimer;

        [HttpGet]
        public async Task <ActionResult> Index() //carga la página la primera vez
        {  
            //tarea 
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 10000;//segundos para cargar la página
            aTimer.Elapsed += OnTimedEventAsync; //disparador
            aTimer.AutoReset = true; //repite el disparador
            aTimer.Enabled = true;//Iniciar el temporizador

            using (var client = new HttpClient())
            {
                //pasando la url base de servicio
                client.BaseAddress = new Uri("https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json");

                client.DefaultRequestHeaders.Accept.Clear();
                // Definir formato de datos de solicitud 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                // Enviando una solicitud
                HttpResponseMessage Res = await client.GetAsync("airports.json");
                
                //se añade un encabezado de respuesta personalizado
                Res.Headers.Add("from-feed", "1");
                
                // si solicitud aceptada
                if (Res.IsSuccessStatusCode)
                {
                    //Almacenando la respuesta  
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializando la respuesta recibida de la web y quedandonos solo con los aeropuertos europeos

                    AirportInfo = JsonConvert.DeserializeObject<List<Aeropuerto>>(EmpResponse);

                }

                AirportInfo.RemoveAll(x => !x.Continent.Equals("EU"));
                AirportInfo.RemoveAll(x => !x.Type.Equals("airport"));

                return View(AirportInfo);
            }
        }

		[HttpPost]
		public async Task<ActionResult> Index(string searchString) //carga la página la primera vez
		{

            using (var client = new HttpClient())
            {
                //tarea 
                aTimer = new System.Timers.Timer();
                aTimer.Interval = 10000;//segundos para cargar la página
                aTimer.Elapsed += OnTimedEventAsync; //disparador
                aTimer.AutoReset = true; //repite el disparador
                aTimer.Enabled = true;//Iniciar el temporizador

                //pasando la url base de servicio
                client.BaseAddress = new Uri("https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json");

                client.DefaultRequestHeaders.Accept.Clear();
                // Definir formato de datos de solicitud 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Enviando una solicitud
                HttpResponseMessage Res = await client.GetAsync("airports.json");

                //se añade un encabezado de respuesta personalizado
                Res.Headers.Add("from-feed", "1");

                // si solicitud aceptada
                if (Res.IsSuccessStatusCode)
                {
                    //Almacenando la respuesta  
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializando la respuesta recibida de la web y quedandonos solo con los aeropuertos europeos

                    AirportInfo = JsonConvert.DeserializeObject<List<Aeropuerto>>(EmpResponse);

                }

                AirportInfo.RemoveAll(x => !x.Continent.Equals("EU"));
                AirportInfo.RemoveAll(x => !x.Type.Equals("airport"));

                //filtro, si la variable de busqueda es no nula o no vacia solo se muestran los aeropuertos seleccionados(sirve para filtrar cualquier campo)
                if (!String.IsNullOrEmpty(searchString))
                {
                    AirportInfo = (AirportInfo.Where(x => x.Iso.Contains(searchString))).ToList();
                }
                
                return View(AirportInfo);
            }
        }

        private void OnTimedEventAsync(Object source, System.Timers.ElapsedEventArgs e)
        {
            Thread.Sleep(300000);//no se solicita la carga de página hasta pasados cinco minutos
        }
    }
}