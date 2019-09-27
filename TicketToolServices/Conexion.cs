using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
namespace TicketToolServices
{
    class Conexion
    {
        public static string GetConnectionString(ExecutionContext context)
        {

            var config = new ConfigurationBuilder()
                            .SetBasePath(context.FunctionAppDirectory)
                            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();

            return config.GetConnectionString("SQLConnectionString");
        }

        
    }
}
