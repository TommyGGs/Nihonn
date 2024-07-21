// using Unity.Netcode;
// using UnityEngine;

// public class TurnManager : NetworkBehaviour
// {
//     // private NetworkVariable<int> currentPlayerIndex = new NetworkVariable<int>(0);

    
//     public bool IsCurrentPlayerTurn()
//     {
//         return NetworkManager.Singleton.LocalClientId == currentPlayerIndex.Value;
//     }

//     [ServerRpc(RequireOwnership = false)]
//     public void EndTurnServerRpc()
//     {
//         currentPlayerIndex.Value = (currentPlayerIndex.Value + 1) % NetworkManager.Singleton.ConnectedClientsList.Count;
//     }

//     public void EndTurn()
//     {
//         if (IsServer)
//         {
//             EndTurnServerRpc();
//         }
//     }
// }
