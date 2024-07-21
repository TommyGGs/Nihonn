using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class RelayUtility : MonoBehaviour
{
    private string joinStr = "";
    private SettingUpGame settingUpGame;
    
    public async void StartGame()
    {
        if (joinStr == "")
        {
            await UnityServices.InitializeAsync();
            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
            };
            settingUpGame.PlayerID = 0;
            Debug.Log("ur id is 0");
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            CreateRelayHost();
        }
        else
        {
            settingUpGame.PlayerID = 1;
            Debug.Log("ur id is 1");
            JoinRelay(joinStr);
        }
    }

    [SerializeField] TMP_InputField joinCodeInput;
    private async void Start()
    {
        settingUpGame = GameObject.Find("Main Camera").GetComponent<SettingUpGame>();

        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateRelayHost()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
                );

            Debug.Log(joinCode);
            NetworkManager.Singleton.StartHost();

            joinStr = joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    public void JoinRelayButton()
    {
        JoinRelay(joinCodeInput.text);
    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            Debug.Log("JoinRelay code = " + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
                );

            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }

        joinStr = "";
    }
}
