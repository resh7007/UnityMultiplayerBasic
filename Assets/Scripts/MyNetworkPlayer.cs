using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText= null;
    [SerializeField] private Renderer playerRenderer = null;
    [SyncVar(hook = nameof(HandleDisplayNameUpdated))] [SerializeField] private string displayName = "Missing Name";
    [SyncVar(hook = nameof(HandleDisplayColorUpdated))] [SerializeField] private Color playerColor = Color.black;

    #region Server
    [Server]
    //this function is used only on a server
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }
    [Server]
    public void SetPlayerColor(Color color)
    {
        playerColor = color;

    }

    [Command]
    //this function is called from a client  and is executed on a server
    private void CmdSetDisplayName(string newDisplayName)
    {
        if (newDisplayName.Contains(" ") || newDisplayName.Length < 2)
            return;
        RpcSetMessage("I won!");
        SetDisplayName(newDisplayName);
   
    }
    #endregion

    #region Client
    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        playerRenderer.material.SetColor("_Color",newColor); 
    }
    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;

    }
    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My new name");
    }

    [ClientRpc]
    private void RpcSetMessage(string msg)
    {
        Debug.Log(msg);
    }

    #endregion
}
