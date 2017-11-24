using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TodoList.Abstractions;
using TodoList.Handler;

namespace TodoList.Services
{
    public class AzureCloudService : ICloudService
    {
        private MobileServiceClient _client;
        private const string _serverUrl = "https://dmax-chapter1-demo.azurewebsites.net";

        public AzureCloudService()
        {
#if DEBUG
            _client = new MobileServiceClient(_serverUrl, new LoggingHandler(true));
#else
            _client = new MobileServiceClient(_serverUrl);
#endif

        }

        public ICloudTable<T> GetTable<T>() where T : TableData
        {
            return new AzureCloudTable<T>(_client);
        }
    }
}
