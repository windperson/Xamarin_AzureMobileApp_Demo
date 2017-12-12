using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SharedInterface;
using TodoList.Abstractions;
using TodoList.Handler;

namespace TodoList.Services
{
    public class AzureCloudService : ICloudService
    {
        private readonly MobileServiceClient _client;
        private const string ServerUrl = @"https://windperson-mobileapp-demo.azurewebsites.net";

        public AzureCloudService()
        {
#if DEBUG   
            _client = new MobileServiceClient(ServerUrl, new LoggingHandler(true));
#else
            _client = new MobileServiceClient(_serverUrl);
#endif

        }

        ITodoItemService<T> ICloudService.GetTable<T>()
        {
            return new AzureCloudTable<T>(_client);
        }
    }
}
