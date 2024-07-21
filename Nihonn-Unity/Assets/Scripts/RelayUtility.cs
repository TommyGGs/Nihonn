using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Net.Http;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class RelayUtility : MonoBehaviour
{
    private string joinStr = "";

    public async void StartGame()
    {
        GetJoinCode();

        if (joinStr == "")
        {
            await UnityServices.InitializeAsync();
            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
            };
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            CreateRelayHost();
        }
        else
        {
            JoinRelay(joinStr);
            PostJoinCode(joinStr);
            joinStr = "";
        }

        Debug.Log("Button Clicked!");
        SceneManager.LoadScene("GamePage");
    }

    // [SerializeField] TMP_InputField joinCodeInput;
    // private async void Start()
    // {
    //     await UnityServices.InitializeAsync();

    //     AuthenticationService.Instance.SignedIn += () =>
    //     {
    //         Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
    //     };
    //     await AuthenticationService.Instance.SignInAnonymouslyAsync();
    // }

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

    // public void JoinRelayButton()
    // {
    //     JoinRelay(joinCodeInput.text);
    // }

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
    }

    private async void GetJoinCode()
    {
        using (HttpClient client = new HttpClient())
        {

            HttpResponseMessage response = await client.GetAsync("https://nihonn.onrender.com/join-code");
            string responseBody = await response.Content.ReadAsStringAsync();
            this.joinStr = responseBody;
        }
    }

    private async void PostJoinCode(string joinCode)
    {
        using (HttpClient client = new HttpClient())
        {
            var values = new Dictionary<string, string>
            {
                { "joinCode", joinCode }
            };

            var content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await client.PostAsync("https://nihonn.onrender.com/join-code", content);
            string responseBody = await response.Content.ReadAsStringAsync();
        }
    }
}
