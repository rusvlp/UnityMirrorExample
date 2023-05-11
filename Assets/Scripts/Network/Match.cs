using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[System.Serializable]
public class Match : NetworkBehaviour
{
    public string matchID;

    public SyncListGameObject players = new SyncListGameObject();

    public Match(string matchID, GameObject playerHost)
    {
        this.matchID = matchID;
        players.Add(playerHost);
    }

    public Match() {}

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