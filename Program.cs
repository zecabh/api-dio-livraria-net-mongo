using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

namespace livrariaDIOAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();

                    string kvURL = builtConfig["keyVaultConfig:KVURL"];
                    string tenantId = builtConfig["keyVaultConfig:TenantId"];
                    string clientId = builtConfig["keyVaultConfig:ClientId"];
                    string clientSecret = builtConfig["keyVaultConfig:ClientSecret"];

                    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                    var client = new SecretClient(new Uri(kvURL), credential);                     
                    config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
                    
                })
                
                .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
    }
}
