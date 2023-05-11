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
        if (isLocalPlayer)
        {
            localPlayer = this;
            
        }
        networkMatch = GetComponent<NetworkMatch>();
    }

    public void Host()
    {
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
            print("<color = green>Game hosted successfully</color>");
            TargetHostGame(true, _matchID);
        } else
        {
            print("<color = red>Game hosted failed</color>");
            TargetHostGame(false, _matchID);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string _matchID)
    {
        print("Match id is: " + _matchID);
        ConnectManager.Instance.HostSuccess(success);
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
