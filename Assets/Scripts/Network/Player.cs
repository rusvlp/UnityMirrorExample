using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;
    NetworkMatch networkMatch;

    public string MatchID;

    private void Start()
    {
        
        
       // print("Local Player Start() is Called");
        
        print("Is local player: " + isLocalPlayer);
        if (isLocalPlayer)
        {
            localPlayer = this;
        }
        networkMatch = GetComponent<NetworkMatch>();
    }

    public void Host()
    {
        print("Player's method Host() is called");
        string matchId = MatchMaker.GetRandomMatchID();
        CmdHostGame(matchId);
    }

    [Command]
    void CmdHostGame(string _matchID)
    {
        this.MatchID = _matchID;
       if (MatchMaker.Instance.HostGame(_matchID, gameObject))
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

    [TargetRpc]
    void TargetHostGame(bool success, string _matchID)
    {
        print("Match id is: " + _matchID);
        ConnectManager.Instance.HostSuccess(success);
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
        if (MatchMaker.Instance.JoinGame(_matchID, gameObject))
        {
            networkMatch.matchId = _matchID.toGuid();
            print("Game joined successfully");
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
        ConnectManager.Instance.JoinSuccess(success);
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
