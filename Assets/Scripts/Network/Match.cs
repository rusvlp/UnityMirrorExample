using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[System.Serializable]
public class Match : NetworkBehaviour
{
    public string matchID;
    
    public SyncListGameObject players = new();
    public int sceneId = 0;

    public Match(string matchID, GameObject playerHost)
    {
       // print("Match ctor is called " + this);
        this.matchID = matchID;
        this.players.Add(playerHost);
    }

    public Match() {}


    public string GetInfoAboutMatch()
    {
        string result = $"Match id is {this.matchID}, players:\n";

        int index = 0;
        foreach (GameObject go in players)
        {
            result += $"Player {index}, hashCode: {go.GetHashCode()}\n";
        }
        
        return result;
    }
    
    #region StartAndUpdate
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}


[System.Serializable]
public class SyncListGameObject : SyncList<GameObject>
{

}

[System.Serializable]
public class SyncListMatch : SyncList<Match>
{

}