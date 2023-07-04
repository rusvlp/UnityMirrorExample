using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ConnectManager : MonoBehaviour
{
    /* TODO: Выключить интерактивность кнопок при подключении/старте хоста или сервера
     *       Также изменять интерактивность в зависимости от удачи/неудачи создания хоста/сервера или подключения к нему
     */

    public static ConnectManager Instance;

    public CustomNetworkManager manager;

    public GameObject playerInfoPrefab;
    public TMP_Text matchIdTag;
    
    [Header("Host Join")]
    
    public TMP_InputField ip_InputField;
    public TMP_InputField matchId_InputField;

    public Canvas LobbyCanvas;

    [Header("Lobby")] 
    public Transform UIPlayerParent;
    public GameObject UIPlayerPrefab;
    public GameObject BeginGameButton;

    private bool isHostStarting = false;
    private bool isServerStarting = false;
    private bool isMatchJoining = false;

    [Header("Scene Management")] 
    [Scene]
    // [FormerlySerializedAs("OnlineScene")]
    public string OnlineScene;
    
    

    // Start is called before the first frame update
    void Start()
    {
        Instance = this; 
   //     Test();
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

        if (isMatchJoining && Player.localPlayer != null)
        {
            print("Calling method Join() from LocalPlayer");
            Player.localPlayer.Join(matchId_InputField.text.ToUpper());
            isMatchJoining = false;
        }
        
        
    }


    private void Test()
    {
        print("Test message for breakpoint testing");
    }
    
    public void Connect()
    {
        print("trying to connect");
        manager.networkAddress = ip_InputField.text;
        manager.StartClient();
        isMatchJoining = true;

    }

    public void StartServer()
    {
        /*
        print("Going to start server");
        manager.StartServer();
        */
    }

    
    
    public void AddInfoAboutPlayerToServerCanvas(string _identifier)
    {

        Vector3 position = new Vector3(LobbyCanvas.transform.position.x, LobbyCanvas.transform.position.y, 0);
        Quaternion rotation = new Quaternion(0,0,0,0);
        TMP_Text playerInfo = Instantiate(playerInfoPrefab, position, rotation, LobbyCanvas.transform).GetComponent<TMP_Text>();
        playerInfo.text = _identifier;
        print(playerInfo);
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
        
    }

    public void HostSuccess(bool success, string _matchId)
    {
        if (success)
        {
            //print("hosted succeed. loading canvas");
            LobbyCanvas.enabled = true;
            matchIdTag.text = _matchId;
            BeginGameButton.SetActive(true);

        } 
    }

    public void JoinSuccess(bool success, string _matchId)
    {
        if (success)
        {
            LobbyCanvas.enabled = true;
            matchIdTag.text = _matchId;
        }
    }

    public void SpawnPlayerUIPrefab(Player player)
    {
        print("Spawning UI Player Prefab"); 
        GameObject UIPlayer = Instantiate(UIPlayerPrefab, UIPlayerParent);   
        UIPlayer.GetComponent<UIPlayer>().SetPlayer(player);
    }

    public void BeginGame()
    {
        Player.localPlayer.BeginGame();
    }
}
