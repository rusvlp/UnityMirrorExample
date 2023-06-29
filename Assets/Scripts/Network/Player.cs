using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;
    NetworkMatch networkMatch;

    [SyncVar]
    public string MatchID;

    [SyncVar] public int playerIndex;

    private void Start()
    {
        networkMatch = GetComponent<NetworkMatch>();
        
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


    public void Join(string _matchId)
    {
        print("Player's method Join() is called");
        
        CmdJoinGame(_matchId);
    }

    [Command]
    void CmdJoinGame(string _matchID)
    {
        this.MatchID = _matchID;
        if (MatchMaker.Instance.JoinGame(_matchID, gameObject, out this.playerIndex))
        {
            networkMatch.matchId = _matchID.toGuid();
            print("Game joined successfully");
            
            //ConnectManager.Instance.AddInfoAboutPlayerToServerCanvas(MatchMaker.Instance.matches.Find(match => match.matchID == _matchID).players.Count + "");
            ConnectManager.Instance.SpawnPlayerUIPrefab(this);
            TargetJoinGame(true, _matchID);
        } else
        {
            print("Game joined failed");
            TargetJoinGame(false, _matchID);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string _matchID)
    {
        print("Match id is: " + _matchID);
        ConnectManager.Instance.JoinSuccess(success, _matchID);
        foreach (GameObject p in MatchMaker.Instance.matches.Find(
                     match => match.matchID == _matchID
                     ).players)
        {
            ConnectManager.Instance.SpawnPlayerUIPrefab(p.GetComponent<Player>());
        }
       
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
