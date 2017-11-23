using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TodoList.Abstractions;

namespace TodoList.Services
{
    public class AzureCloudService : ICloudService
    {
        private MobileServiceClient _client;

        public AzureCloudService()
        {
            _client = new MobileServiceClient("https://dmax-chapter1-demo.azurewebsites.net");
        }

        public ICloudTable<T> GetTable<T>() where T : TableData
        {
            return new AzureCloudTable<T>(_client);
        }
    }
}
