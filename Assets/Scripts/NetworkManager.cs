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
        private string _nodeUrl = "ws://127.0.0.1:9944";

        private SubstrateClientExt _client;
        public SubstrateClientExt Client => _client;

        // Start is called before the first frame update
        public void InitializeClient()
        {
            if (_client != null)
            {
                return;
            } 
            
            _client = new SubstrateClientExt(new Uri(_nodeUrl), ChargeAssetTxPayment.Default());

        }
    }
}