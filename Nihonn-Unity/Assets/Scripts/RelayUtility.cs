// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RelayUtility : MonoBehaviour
// {
//     public async Task<string> StartHostWithRelay(int maxConnections = 5)
//     {
//         await UnityServices.InitializeAsync();
//         if (!AuthenticationService.Instance.IsSignedIn)
//         {
//             await AuthenticationService.Instance.SignInAnonymouslyAsync();
//         }
//         Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
//         NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
//         var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
//         return NetworkManager.Singleton.StartHost() ? joinCode : null;
//     }

//     public async Task<bool> StartClientWithRelay(string joinCode)
//     {
//         await UnityServices.InitializeAsync();
//         if (!AuthenticationService.Instance.IsSignedIn)
//         {
//             await AuthenticationService.Instance.SignInAnonymouslyAsync();
//         }

//         var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
//         NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
//         return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
//     }

// }
