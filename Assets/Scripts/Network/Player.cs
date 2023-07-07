using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;


public class Player : NetworkBehaviour
{
    public static Player localPlayer;
    NetworkMatch networkMatch;

    public GameObject PlayerPrefab;
    
    [SyncVar]
    public string MatchID;

    [SyncVar] 
    public int playerIndex;

    private void Start()
    {
        
        networkMatch = GetComponent<NetworkMatch>();
        CustomNetworkManager.Instance.spawnPrefabs.Add(PlayerPrefab);
        
        
       // print("Local Player Start() is Called");
        
        print("Is local player: " + isLocalPlayer);
        if (isLocalPlayer)
        {
            localPlayer = this;
           // ConnectManager.Instance.SpawnPlayerUIPrefab(this);
        }
        else
        {
            
            //ConnectManager.Instance.SpawnPlayerUIPrefab(this);
        }
        
    }

    
    
    
    #region HostGame
    public void Host()
    {
        print("Player's method Host() is called");
        string matchId = MatchMaker.GetRandomMatchID();
        CmdHostGame(matchId);
    }

    /*
     * Запрос от клиента на сервер для соеднинения
     */
    
    [Command]
    void CmdHostGame(string _matchID)
    {
        this.MatchID = _matchID;
       if (MatchMaker.Instance.HostGame(_matchID, this.gameObject, out this.playerIndex))
        {
            networkMatch.matchId = _matchID.toGuid();
            print("Game hosted successfully");
            TargetHostGame(true, _matchID);
        } else
        {
            print("Game hosted failed");
            TargetHostGame(false, _matchID);
        }
    }

    /*
     * Отправка ответа на клиент о результате подключения
     */
    
    [TargetRpc]
    void TargetHostGame(bool success, string _matchID)
    {
        print("Match id is: " + _matchID);
        ConnectManager.Instance.HostSuccess(success, _matchID);
        ConnectManager.Instance.SpawnPlayerUIPrefab(this);
    }
    #endregion

    #region JoinGame
    public void Join(string _matchId)
    {
        print("Player's method Join() is called");
        
        CmdJoinGame(_matchId);
    }

    [Command]
    void CmdJoinGame(string _matchID)
    {
        List<Player> players;
        
        this.MatchID = _matchID;
        if (MatchMaker.Instance.JoinGame(_matchID, gameObject, out this.playerIndex))
        {
            TargetPrint("Current player Index is: " + this.playerIndex);
            players = MatchMaker.Instance.matches
                .Find(match => match.matchID == _matchID)
                .players
                .Select(go =>
                {
                    return go.GetComponent<Player>();
                })
                .ToList();
            

            networkMatch.matchId = _matchID.toGuid();
            print("Game joined successfully");
            
            //ConnectManager.Instance.AddInfoAboutPlayerToServerCanvas(MatchMaker.Instance.matches.Find(match => match.matchID == _matchID).players.Count + "");
            ConnectManager.Instance.SpawnPlayerUIPrefab(this);
            TargetJoinGame(true, _matchID, players);
        } else
        {
            print("Game joined failed");
            TargetJoinGame(false, _matchID, null);
        }
    }

    
    [TargetRpc]
    void TargetJoinGame(bool success, string _matchID, List<Player> players)
    {
        print("Match id is: " + _matchID);
        ConnectManager.Instance.JoinSuccess(success, _matchID);

        players[players.Count - 1].playerIndex = players.Count;
        
        foreach (Player p in players)
        {
            //print("Spawning Player Prefab with Index: " + p.playerIndex); 
            ConnectManager.Instance.SpawnPlayerUIPrefab(p);
        }
       
    }    
    #endregion
    
    
    #region BeginGame

    public void BeginGame()
    {
        CmdBeginGame();
    }
    
    [Command]
    void CmdBeginGame()
    {

        MatchMaker.Instance.BeginGame(MatchID);
        print("Game is beginning");
        
    }

    public void StartGame()
    {
        TargetBeginGame();
    }
    
    [TargetRpc]
    void TargetBeginGame()
    {
        //Загрузка сцены будет тут
        
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        ConnectManager.Instance.DisableLobbyUi();

        Vector3 spawnPosition = new Vector3(0, 1, 0);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);
        GameObject player = Instantiate(PlayerPrefab, spawnPosition, spawnRotation);
        //this.GetComponent<NetworkIdentity>().sceneId = Convert.ToUInt64(GetHashCode());
        player.GetComponent<NetworkIdentity>().sceneId = Convert.ToUInt64(this.playerIndex);

    }    
    
    #endregion
    
    
    [TargetRpc]
    public void TargetPrint(string message)
    {
        print(message);
    }

    
    
    #region Commented
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    #endregion


}
