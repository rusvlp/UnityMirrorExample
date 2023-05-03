using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectManager : MonoBehaviour
{

   
    public CustomNetworkManager manager;

    public TMP_InputField ip_InputField; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Connect()
    {
        print("trying to connect");
        manager.networkAddress = ip_InputField.text;
        manager.StartClient();
    }

    public void StartServer()
    {
        print("Going to start server");
        manager.StartServer();
    }

    public void StartHost()
    {
        print("Going to start host");
        manager.StartHost();
    }
}
