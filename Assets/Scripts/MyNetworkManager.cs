using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
   public override void OnServerAddPlayer (NetworkConnectionToClient conn)
   {
      base.OnServerAddPlayer(conn);

      MyNetworkPlayer player= conn.identity.GetComponent<MyNetworkPlayer>();
      player.SetDisplayName($"Player {numPlayers}");
      Color color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
      player.SetPlayerColor(color);
 
   }
 
}
