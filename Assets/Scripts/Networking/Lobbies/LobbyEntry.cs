using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using TMPro;

public class LobbyEntry : MonoBehaviour
{
    // Data
    public SteamId lobbyID;
    public string lobbyName;
    public TextMeshProUGUI lobbyNameText;

    public void SetLobbyData()
    {
        if (lobbyName == "")
        {
            lobbyNameText.text = "Empty";
        }
        else
        {
            lobbyNameText.text = lobbyName;
        }
    }

    public async void JoinLobby()
    {
        await SteamMatchmaking.JoinLobbyAsync(lobbyID);
    }

}
