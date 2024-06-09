using Braintree;
using MelodyFusionAdnroid.Infrastructure;
using MelodyFusionAdnroid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MelodyFusionAdnroid.Service
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
