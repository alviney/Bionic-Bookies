using UnityEngine;

public class SteamworksInitialiser : MonoBehaviour
{
    private void Awake()
    {
        try
        {
            Steamworks.SteamClient.Init(2065720);
        }
        catch (System.Exception e)
        {
            Debug.Log("Steamworks failed to initialise: " + e);
            // Something went wrong - it's one of these:
            //
            //     Steam is closed?
            //     Can't find steam_api dll?
            //     Don't have permission to play app?
            //
        }
    }

    void Update()
    {
        // Steamworks.SteamClient.RunCallbacks();
    }

    private void OnDestroy()
    {
        Steamworks.SteamClient.Shutdown();
    }
}
