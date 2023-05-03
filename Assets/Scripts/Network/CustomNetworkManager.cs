using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CustomNetworkManager : NetworkManager
{
    public string ObjectIdentifier;
    
    private bool isPlayerConnected;
    private bool isMessageSent;

    public Scene OnlineServerScene;
    //public string Something;


    #region Singleton
    private static CustomNetworkManager _instance;

    public static CustomNetworkManager Instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    private void Start()
    {
        _instance = this;
    }


    [Scene]
    [FormerlySerializedAs("c_OnlineScene")]
   // [Tooltip("Scene that Mirror will switch to when the server is started. Clients will recieve a Scene Message to load the server's current scene when they connect.")]
    public string ServerOnlineScene = "";

    [Scene]
    [FormerlySerializedAs("c_OfflineScene")]
    public string ServerOfflineScene = "";
    //Сруктура для сообщения
    public struct FingerprintMessage : NetworkMessage 
    {
        public string fingerprint;
    }

    public DevicesListController devicesListController;

    //Метод подключения очков на сервере
    public void OnGlassesConnect(NetworkConnectionToClient conn, FingerprintMessage message)
    {
        print("OnGlassesConnect is called");
        devicesListController.AddConnection(conn, message.fingerprint);
    }

    //Метод регистрации обработчика сетевого сообщения 
    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<FingerprintMessage>(OnGlassesConnect);
        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
       
    }

    // Метод отпрвыки фингерпринта
    public void SendFingerprint()
    {
        FingerprintMessage fMsg = new FingerprintMessage()
        {
            fingerprint = SystemInfo.deviceUniqueIdentifier
        };
        NetworkClient.Send(fMsg);

        isMessageSent = true;
    }

    // Метод подключения клиента к серверу (на клиенте (?))
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        isPlayerConnected = true;   
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        NetworkServer.SetClientReady(conn);
       // DevicesListController.GetComponent<DevicesListController>().AddConnection(conn);
    }

    public override void OnStartHost()
    {
        base.OnStartHost();
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
    
    }


    private void Update()
    {
        if(!isMessageSent && isPlayerConnected)
        {
            SendFingerprint();
        }
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        isPlayerConnected = false;
        isMessageSent = false;
    }

    public override void OnDestroy()
    {
        print("Net Manager Destroyed");
    }
    
}