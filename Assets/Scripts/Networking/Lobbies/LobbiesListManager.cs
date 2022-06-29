using UnityEngine;
using System.Collections.Generic;
// using Steamworks;
// using Steamworks.Data;

public class LobbiesListManager : MonoBehaviour
{
    public GameObject lobbiesMenu;
    public GameObject lobbyEntryPrefab;
    public GameObject lobbieListContent;

    public GameObject lobbiesButton, hostButton;

    public List<GameObject> listOfLobbies = new List<GameObject>();

    // public void DisplayLobbies(List<SteamId> lobbyIDs, lobb  result) {
    //     for (int i = 0; i < lobbyIDs.Count; i++)
    //     {
    //         SteamMatchmaking.LobbyList()
    //         if (lobbyIDs[i].Value == result.stea

    //     }
    // }

    public void DestroyLobbies()
    {
        listOfLobbies.ForEach(l => Destroy(l));

        listOfLobbies.Clear();
    }

}
