using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

using System.Security.Cryptography;
using System.Text;

public class MatchMaker : NetworkBehaviour
{
    public static MatchMaker Instance;

    public Match matchPrefab;
    
    public SyncListMatch matches = new SyncListMatch();
    public SyncListString matchIDs = new SyncListString();

    public GameObject TurnManagerPrefab;
    
    public bool HostGame(string _matchID, GameObject _player, out int playerIndex)
    {
        playerIndex = -1;
        if (!matchIDs.Contains(_matchID))
        {
            matchIDs.Add(_matchID);
            Match match = Instantiate(matchPrefab);
            match.GetComponent<NetworkMatch>().matchId = _matchID.toGuid();
            match.matchID = _matchID;
            match.players.Add(_player);
            matches.Add(match);
            print("Match added " + match);
            print("Match created");
            playerIndex = 1;
            return true;
        } else
        {
            print("Match is already exists");
            return false;
        }
        
    }

    public bool JoinGame(string _matchID, GameObject _player, out int playerIndex)
    {
        playerIndex = -1;
        if (matchIDs.Contains(_matchID))
        {
            Match match = matches.Find(match => match.matchID == _matchID);
            match.players.Add(_player);
            print("Match joined");
            print(match.GetInfoAboutMatch());
            playerIndex = match.players.Count;
            return true;
        } else
        {
            print("Match does not exists");
            return false;
        }
        
    }

    

    //// Start is called before the first frame update
    void Start()
    {
        Instance = this;   
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public static string GetRandomMatchID()
    {
        string _id = string.Empty;

        for (int i = 0; i < 5; i++)
        {
            int random = UnityEngine.Random.Range(0, 36);
            if (random < 26)
            {
                _id += (char)(random + 65);
            } else
            {
                _id += (random - 26).ToString();
            }
        }

        print("Random Match ID iz: " + _id);
        return _id;

    }

    public void BeginGame(string _matchId)
    {
        
        GameObject newTurnManager = Instantiate(TurnManagerPrefab);
        NetworkServer.Spawn(newTurnManager);
        newTurnManager.GetComponent<NetworkMatch>().matchId = _matchId.toGuid();
        TurnManager turnManager = newTurnManager.GetComponent<TurnManager>();


        int index = 0;
        foreach (Match m in matches)
        {
            if (m.matchID == _matchId)
            {
                m.GetComponent<NetworkIdentity>().sceneId = Convert.ToUInt64(index+1);
                
                foreach (GameObject p in m.players)
                {
                    Player _player = p.GetComponent<Player>();
                    turnManager.AddPlayer(_player);
                    
                    _player.StartGame();
                }
                
                break;
            }

            index++;
        }
    }
    
}

public static class MatchExtensions
{
    public static Guid toGuid(this string id)
    {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

        byte[] inputBytes = Encoding.Default.GetBytes(id);
        byte[] hashBytes = provider.ComputeHash(inputBytes);

        return new Guid(hashBytes);
    }
}

[System.Serializable]
public class SyncListString : SyncList<string>
{

}