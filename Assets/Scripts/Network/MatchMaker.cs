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

    public bool HostGame(string _matchID, GameObject _player)
    {
        if (!matchIDs.Contains(_matchID))
        {
            matchIDs.Add(_matchID);
            Match match = Instantiate(matchPrefab);
            match.matchID = _matchID;
            match.players.Add(_player);
            matches.Add(match);
            print("Match added " + match);
            print("Match created");
            return true;
        } else
        {
            print("Match is already exists");
            return false;
        }
        
    }

    public bool JoinGame(string _matchID, GameObject _player)
    {
        if (matchIDs.Contains(_matchID))
        {
            Match match = matches.Find(match => match.matchID == _matchID);
            match.players.Add(_player);
            print("Match joined");
            print(match.GetInfoAboutMatch());
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