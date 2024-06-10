//using Braintree;
using MelodyFusion.DLL.Infrastructure;
using MelodyFusion.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MelodyFusion.DLL.Service
{

    public class PaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LocalStorage _localStorage;

        public PaymentService(IHttpClientFactory httpClientFactory, LocalStorage localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
        }
        public async Task GetPaymentMethodNonce()
        {
           
        }
    }
}
