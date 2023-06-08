using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectManager : MonoBehaviour
{
    /* TODO: Выключить интерактивность кнопок при подключении/старте хоста или сервера
     *       Также изменять интерактивность в зависимости от удачи/неудачи создания хоста/сервера или подключения к нему
     */

    public static ConnectManager Instance;

    public CustomNetworkManager manager;

    public TMP_InputField ip_InputField;
    public TMP_InputField matchId_InputField;

    public GameObject LobbyCanvas;


    private bool isHostStarting = false;
    private bool isServerStarting = false;
    private bool isMatchJoining = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isHostStarting && Player.localPlayer != null)
        {
            print("Calling method Host() from LocalPlayer");
            Player.localPlayer.Host();
            isHostStarting = false;
        }
    }

    public void Connect()
    {
        print("trying to connect");
        manager.networkAddress = ip_InputField.text;
        manager.StartClient();
    }

    public void StartServer()
    {
        /*
        print("Going to start server");
        manager.StartServer();
        */
    }

    public void StartHost()
    {
        isHostStarting = true;
        print("Going to start host");
        manager.StartHost();
        
        
        //Player.localPlayer.Host();
      
        
    }

    public void JoinMatch()
    {
        Player.localPlayer.Join(matchId_InputField.text);
    }

    public void HostSuccess(bool success)
    {
        if (success)
        {
            print("hosted succeed. loading canvas");
            LobbyCanvas.SetActive(true);
        } 
    }

    public void JoinSuccess(bool success)
    {
        if (success)
        {

        }
    }
    
    
}
