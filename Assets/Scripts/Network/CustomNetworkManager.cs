using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    // Start is called before the first frame update
    private bool isPlayerConnected;
    private bool isMessageSent;

    public struct FingerprintMessage : NetworkMessage 
    {
        public string fingerprint;
    }

    public GameObject DevicesListController;


    public void OnGlassesConnect(NetworkConnectionToClient conn, FingerprintMessage message)
    {

        DevicesListController.GetComponent<DevicesListController>().AddConnection(conn, message.fingerprint);
    }


    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<FingerprintMessage>(OnGlassesConnect); 
    }

    public void SendFingerprint()
    {
        FingerprintMessage fMsg = new FingerprintMessage()
        {
            fingerprint = SystemInfo.deviceUniqueIdentifier
        };
        NetworkClient.Send(fMsg);

        isMessageSent = true;
    }

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


}