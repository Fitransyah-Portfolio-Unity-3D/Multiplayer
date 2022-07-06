using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    private string displayName = "Missing name";

    [SyncVar]
    [SerializeField]
    private Color displayColor;

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }
    [Server]
    public void SetDisplayColor(Color newColor)
    {
        displayColor = newColor;
    }

}
