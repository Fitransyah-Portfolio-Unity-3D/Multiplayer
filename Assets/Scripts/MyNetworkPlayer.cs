using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNametext = null;
    [SerializeField] private Renderer displayColorRenderer = null;
    
    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    private Color displayColor;

    #region Server
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

    [Command]
    public void CmdSetDisplayName(string newDisplayName)
    {             
        RpcLogNewName(newDisplayName);
        
        SetDisplayName(newDisplayName);
    }
    #endregion


    #region Client
    public void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNametext.text = newName;
    }
    public void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    [ClientRpc]
    public void RpcLogNewName (string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    [ContextMenu ("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("Fitransyah Rusman");
    }
    #endregion

    
}
