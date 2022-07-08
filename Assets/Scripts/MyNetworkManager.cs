using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        // grabbing player component when entering server
        MyNetworkPlayer player =  conn.identity.GetComponent<MyNetworkPlayer>();

        #region Player Setting on Starting Game
        player.SetDisplayName($"Player {numPlayers}");
        
        Color displayColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));
        
        player.SetDisplayColor(displayColor);

        Debug.Log($"There are {numPlayers} players");
        #endregion
    }
}
