using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    // Start is called before the first frame update
    private bool isPlayerConnected;
    private bool isMessageSent;

    //Сруктура для сообщения
    public struct FingerprintMessage : NetworkMessage 
    {
        public string fingerprint;
    }

    public GameObject DevicesListController;

    //Метод подключения очков на сервере
    public void OnGlassesConnect(NetworkConnectionToClient conn, FingerprintMessage message)
    {
        print("OnGlassesConnect is called");
        DevicesListController.GetComponent<DevicesListController>().AddConnection(conn, message.fingerprint);
    }

    //Метод регистрации обработчика сетевого сообщения 
    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<FingerprintMessage>(OnGlassesConnect); 
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

    // Метод подключения клиента к серверу
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        isPlayerConnected = true;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
       base.OnServerAddPlayer(conn);
       // DevicesListController.GetComponent<DevicesListController>().AddConnection(conn);
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


}