using System.Threading;
using System.Threading.Tasks;
using Ajuna.NetApi;
using Ajuna.NetApi.Model.Extrinsics;
using Ajuna.NetApi.Model.Rpc;
using Ajuna.NetApi.Model.Types;
using Ajuna.NetApi.Model.Types.Primitive;
using Assets.Scripts;
using Serilog;
using AjunaExample.NetApiExt.Generated.Storage;
using Schnorrkel.Keys;
using TMPro;
using UnityEngine;

public class ConnectionSceneController : MonoBehaviour
{
    public static MiniSecret MiniSecretAlice => new MiniSecret(
        Utils.HexToByteArray("0xe5be9a5092b81bca64be81d212e7f2f9eba183bb7a90954f7b76361f6edb5c0a"),
        ExpandMode.Ed25519);

    public static Account Alice => Account.Build(KeyType.Sr25519, MiniSecretAlice.ExpandToSecret().ToBytes(),
        MiniSecretAlice.GetPair().Public.Key);
    
    public TextMeshProUGUI ButtonText;
    public TextMeshProUGUI BlockNumberText;
    public TextMeshProUGUI ConnectionStatusText;
    
    private Task _connectTask;
    private static NetworkManager _networkManager = new NetworkManager();

    private bool _queryBlockNumberFlag, _isSubscribedToStorageChanges;
    
    /// <summary>
    /// Set to true for subscribing to changes and false for polling for the storage changes 
    /// </summary>
    private bool _useSubscription = false;
    
    void Awake()
    {
        SetButtonToDisconnectedState();
        _networkManager.InitializeClient();
    }

    public async void OnDoSomethingClickBtn()
    {
        var doNumberU32 = new U32();
        doNumberU32.Create(32);

        var somethingMethod = TemplateModuleCalls.DoSomething(doNumberU32);

        await _networkManager.Client.Author.SubmitExtrinsicAsync(somethingMethod, Alice, new ChargeTransactionPayment(0), 128,
            CancellationToken.None);
    }

    // Update is called once per frame
    void Update()
    {
        if (_connectTask != null && _connectTask.IsCompleted)
        {
            if (_networkManager.Client.IsConnected)
            {
                SetButtonToConnectedState();
            }
            else
            {
              SetButtonToDisconnectedState();
            }
            _connectTask = null;
        }
    }
    public void OnClickBtnConnect()
    {
        if (_connectTask != null)
        {
            Debug.Log("Incomplete Connect Task.");
            return;
        }

        if (_networkManager.Client.IsConnected)
        {
            _connectTask = _networkManager.Client.CloseAsync();
            
            _isSubscribedToStorageChanges = false;    
            CancelInvoke(); 
        }
        else
        {
            // Connect and get block number
            _connectTask = _networkManager.Client.ConnectAsync();
            StartQueryingForBlockNumber(); 
        }
    }

    public void StartQueryingForBlockNumber()
    {
        if (_useSubscription)
        {
            InvokeRepeating("SubscribeToStorageChanges", 0, 1f);
        }
        else
        {
            InvokeRepeating("QueryForBlockNumber", 0, 1f);    
        }
        
    }
    
    
    public void StopQueryingForBlockNumber()
    {
        CancelInvoke("QueryForBlockNumber");
        _queryBlockNumberFlag = false;
    }
    
    public async Task QueryForBlockNumber()
    {
        if (_networkManager.Client.IsConnected && ! _queryBlockNumberFlag)
        {
            _queryBlockNumberFlag = true;
            var something = await _networkManager.Client.TemplateModuleStorage.Something(CancellationToken.None);
            BlockNumberText.text = something.Value.ToString();
            _queryBlockNumberFlag = false;
        } 
    }

    private async Task SubscribeToStorageChanges()
    {
        if (_networkManager.Client.IsConnected && !_isSubscribedToStorageChanges)
        {
            _isSubscribedToStorageChanges = true;
            await _networkManager.Client.SubscribeStorageKeyAsync(TemplateModuleStorage.SomethingParams(),
                CallBackNumberChange, CancellationToken.None);
          
        } 
    }

    /// <summary>
    /// Called on any number change.
    /// </summary>
    /// <param name="subscriptionId">The subscription identifier.</param>
    /// <param name="storageChangeSet">The storage change set.</param>
    private  void CallBackNumberChange(string subscriptionId, StorageChangeSet storageChangeSet)
    {
        if (storageChangeSet.Changes == null 
            || storageChangeSet.Changes.Length == 0 
            || storageChangeSet.Changes[0].Length < 2)
        {
            Log.Error("Couldn't update account information. Please check 'CallBackAccountChange'");
            return;
        }

            
        var hexString = storageChangeSet.Changes[0][1];

        if (string.IsNullOrEmpty(hexString))
        {
            return;
        }
            
        var primitiveBlockNumber = new U32();
        primitiveBlockNumber.Create(Utils.HexToByteArray(hexString));

        var newBlockNumber = primitiveBlockNumber.Value.ToString();

        UnityMainThreadDispatcher.Instance().Enqueue(
                () =>{
            BlockNumberText.text = newBlockNumber;
        });
    }

    void SetButtonToConnectedState()
    {
        ConnectionStatusText.SetText("Connected");
        ButtonText.SetText("Disconnect");
    }
    
    void SetButtonToDisconnectedState()
    {
        ConnectionStatusText.SetText("Disconnected");
        ButtonText.SetText("Connect");
    }
}
