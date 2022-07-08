using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNametext = null;
    [SerializeField] private Renderer displayColorRenderer = null;
    [SerializeField] private string requestedName;


    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    private Color displayColor;

    #region Server
    [Server] // metohd only happen in server
    public void SetDisplayName(string newDisplayName)
    {
        // have server authority here

        displayName = newDisplayName;
    }

    [Server] // metohd only happen in server
    public void SetDisplayColor(Color newColor)
    {
        displayColor = newColor;
    }

    [Command] // metohd can be called from client
    public void CmdSetDisplayName(string newDisplayName)
    {
        // have server authority here

        if (newDisplayName.Length >= 5) 
        {
            Debug.Log("Name is too long");
            return; 
        }
        else
        {
            RpcLogNewName(newDisplayName);
            SetDisplayName(newDisplayName);
        }
    }
    #endregion


    #region Client
    // hook called when variables are syncing
    public void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNametext.text = newName;
    }
    public void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    [ClientRpc] // method can be called from server
    public void RpcLogNewName (string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    // some function can called from inspector or repsentation of client
    [ContextMenu ("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName(requestedName);
    }
    #endregion

    
}
