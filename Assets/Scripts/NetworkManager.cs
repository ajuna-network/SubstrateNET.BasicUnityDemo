using Ajuna.NetApi.Model.Rpc;
using AjunaExample.NetApiExt.Generated;
using AjunaExample.RestClient;
using System;
using System.Net.Http;
using System.Net.WebSockets;
using Ajuna.NetApi.Model.Extrinsics;

namespace Assets.Scripts
{
    public delegate void ExtrinsicStateUpdate(string subscriptionId, ExtrinsicStatus extrinsicUpdate);

    public class NetworkManager
    {
         public string NodeUrl = "ws://127.0.0.1:9944";
         public string RestUrl = "http://127.0.0.1:61752";
         public event ExtrinsicStateUpdate ExtrinsicStateUpdateEvent;

        private SubstrateClientExt _client;
        public SubstrateClientExt Client => _client;
        private HttpClient _httpClient;
        private BaseSubscriptionClient _subscriptionClient;
        private Client _serviceClient;

        // Start is called before the first frame update
        public void InitializeClient()
        {
            if (_client != null && _serviceClient != null)
            {
                return;
            } 
            
            _client = new SubstrateClientExt(new Uri(NodeUrl), ChargeAssetTxPayment.Default());

            _subscriptionClient = new BaseSubscriptionClient(new ClientWebSocket());

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(RestUrl)
            };
            _serviceClient = new Client(httpClient, _subscriptionClient);
        }
    }
}