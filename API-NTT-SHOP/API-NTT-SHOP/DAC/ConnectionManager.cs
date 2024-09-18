using API_NTT_SHOP.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.DAC
{
    public static class ConnectionManager
    {
        public static string getConnectionString()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var dbConnectionInfo = builder.Build().GetSection("ConnectionStringS").GetSection("NTTSHOP").Value;

            return dbConnectionInfo;
        }
    }
}
