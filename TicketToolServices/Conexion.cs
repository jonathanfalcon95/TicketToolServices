using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TicketToolServices
{
    class Conexion
    {
        private const string url = "https://tmconsulting.freshdesk.com/api/v2";
        public static string GetConnectionString(ExecutionContext context)
        {

            var config = new ConfigurationBuilder()
                            .SetBasePath(context.FunctionAppDirectory)
                            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();

            return config.GetConnectionString("SQLConnectionString");
        }
        //        public static GetObjectApi <T> {
        //}}

        public static T GetDataApiGeneric<T>(string settingName)
        {

            object value = ConfigurationManager.AppSettings[settingName];
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static HttpResponseMessage GetDataApi(string route, string urlParameters = "")
        {



            //InstanciaHttpClient
            HttpClient client = new HttpClient();

            //AsignacionDeURL
            client.BaseAddress = new Uri(url + route);

            //Credenciales del API de FreshDesk
            string yourusername = "UNGq0cadwojfirXm6U7o";
            string yourpwd = "X";

            //Authorization al API


            client.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue(
                  "Basic", Convert.ToBase64String(
                      System.Text.ASCIIEncoding.ASCII.GetBytes(
                         $"{yourusername}:{yourpwd}")));



            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;

            return response;

        }
    }


}