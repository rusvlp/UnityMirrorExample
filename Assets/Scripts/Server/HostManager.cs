using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class HostManager : NetworkBehaviour
{
    public NetworkManager manager;
    public TMP_Text serverStatus;
    public GameObject StopHostBtn;
    public GameObject StopServerBtn;

    public TMP_Text statusSign;

    // Start is called before the first frame update
    void Start()
    {
        manager = CustomNetworkManager.Instance;

        if (isServer)
        {
            statusSign.text = "Хост активен";
            StopHostBtn.SetActive(true);
            StopServerBtn.SetActive(false);
        }
        else if (isServerOnly)
        {
            statusSign.text = "Сервер активен";
            StopServerBtn.SetActive(true);
            StopHostBtn.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (isServer)
        //{
        //    StopHostBtn.SetActive(true);
        //    StopServerBtn.SetActive(false);
        //} else if (isServerOnly)
        //{
        //    StopServerBtn.SetActive(true);
        //    StopHostBtn.SetActive(false);
        //}
    }

    public void StopHost()
    {
        manager.StopHost();
        /*manager.StartHost();
        serverStatus.text = "Хост активен";
        serverStatus.color = new Color(10, 244, 10, 255); */
    }

    public void StopServer()
    {
        manager.StopServer();
        /*manager.StartServer();
        serverStatus.text = "Сервер активен";
        serverStatus.color = new Color(10, 244, 10, 255); */
    }
}
