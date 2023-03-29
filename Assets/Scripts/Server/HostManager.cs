using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class HostManager : MonoBehaviour
{
    public NetworkManager manager;
    public TMP_Text serverStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartHost()
    {
        manager.StartHost();
        serverStatus.text = "Хост активен";
        serverStatus.color = new Color(10, 244, 10, 255);
    }

    public void StartServer()
    {
        manager.StartServer();
        serverStatus.text = "Сервер активен";
        serverStatus.color = new Color(10, 244, 10, 255);
    }
}
